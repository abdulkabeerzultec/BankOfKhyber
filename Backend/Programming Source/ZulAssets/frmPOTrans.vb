Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO
Public Class frmPOTrans

#Region " -- My Decleration -- "
    Dim objattAssetsCoding As attAssetsCoding
    Dim objBALAssetsCoding As New BALAssetsCoding
    Dim objAlHadaIntegration As New BALAlhadaIntegration
    Dim objattPODetails As attPODetails
    Dim objBALPODetails As New BALPODetails
    Dim AstCount As Integer
    Dim objattAstBooks As attAstBooks
    Dim objBALAstBooks As New BALAstBooks
    Dim objattLocation As attLocation
    Dim objBALLocation As New BALLocation
    Dim objattAssetDetails As attAssetDetails
    Dim objBALAssetDetails As New BALAssetDetails
    Dim objattAssets As attItems
    Dim objBALAssets As New BALItems
    Dim objattCompany As attcompany
    Dim objBALCompany As New BALCompany
    Dim objattCustodian As attCustodian
    Dim objBALCustodian As New BALCustodian
    Dim objattBrand As attBrand
    Dim objBALbrand As New BALbrand
    Public FGRCollection As DataTable
    Public Sendfrm As frmPOApproved
    Public CycleCount As Double
    Public Delegate Sub Update1()
    Public RowCount As Integer = 1
    Public Qty As Integer = 1
    Dim SelNode As TreeNode
    Public CurrentCount As Double

    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Dim valProvNotNigative As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider

#End Region


    Private Sub dtService_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtService.ValueChanged
        dtpur.Value = dtService.Value
    End Sub

    Private Sub cmbComp_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComp.SelectTextChanged
        If AppConfig.CodingMode Then
            txtAstNum.Text = Generate_AssetNumber(cmbComp.SelectedValue)
            If txtAstNum.Text = "" Then
                errProv.SetError(cmbComp.TextBox, My.MessagesResource.Messages.DefineRange)
            Else
                errProv.ClearErrors()
            End If
            txtRef.Text = txtAstNum.Text
        End If
        cmbGLCode.SelectedText = ""
    End Sub

    Private Sub frmPOTrans_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Sendfrm.Enabled = True
            Me.Dispose()
            FormController.objfrmPOTrans = Nothing
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub frmPOTrans_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        valProvMain.SetValidationRule(itmCode.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(ZTLocation.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(cmbCust.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(cmbComp.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(cmbGLCode.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(txtBrandID.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(txtAstNum, valRulenotEmpty)
        valProvMain.SetValidationRule(txtbase, valRuleGreaterThanZero)

        valProvNotNigative.SetValidationRule(txtbase, valRuleNotContainMinus)
        valProvNotNigative.SetValidationRule(txtSales, valRuleNotContainMinus)
        valProvNotNigative.SetValidationRule(txtDiscount, valRuleNotContainMinus)

        txtbase.Properties.Mask.MaskType = NumberMaskType
        txtbase.Properties.Mask.EditMask = NumberMask
        txtbase.Properties.Mask.UseMaskAsDisplayFormat = True
        txtSales.Properties.Mask.MaskType = NumberMaskType
        txtSales.Properties.Mask.EditMask = NumberMask
        txtSales.Properties.Mask.UseMaskAsDisplayFormat = True
        txtDiscount.Properties.Mask.MaskType = NumberMaskType
        txtDiscount.Properties.Mask.EditMask = NumberMask
        txtDiscount.Properties.Mask.UseMaskAsDisplayFormat = True
        txtTotal.Properties.Mask.MaskType = NumberMaskType
        txtTotal.Properties.Mask.EditMask = "###,###,###,###,###.00"
        txtTotal.Properties.Mask.UseMaskAsDisplayFormat = True

        Sendfrm.Enabled = False








        If Not AppConfig.CodingMode Then
            txtAstNum.Text = Generate_AssetNumber("")
            txtRef.Text = txtAstNum.Text
        End If

        btnAddCust.Enabled = Check_Auth("frmCustodian")
        btnAddBrand.Enabled = Check_Auth("frmBrand")
        btnAddLoc.Enabled = Check_Auth("frmLocation")
        btnAddCompany.Enabled = Check_Auth("frmCompany")
        btnAddGLCode.Enabled = Check_Auth("frmGLCode")
        btnAddSupplier.Enabled = Check_Auth("frmSupplier")

        dtpur.CustomFormat = AppConfig.MaindateFormat
        dtService.CustomFormat = AppConfig.MaindateFormat

        AstCount = Sendfrm.grdItemsView.RowCount
        cmbType.SelectedIndex = 0
        For i As Integer = 0 To Sendfrm.grdItemsView.RowCount - 1
            If CBool(GetGridRowCellValue(Sendfrm.grdItemsView, i, "TRANSFER")) = True And CBool(GetGridRowCellValue(Sendfrm.grdItemsView, i, "IsTrans")) = False And CInt(GetGridRowCellValue(Sendfrm.grdItemsView, i, "POItmQty")) > 0 Then
                LoadAssetData(RowCount)
                Exit For
            End If


            RowCount = RowCount + 1
        Next

        If AppConfig.ImgType = "Asset Images" Then
            grpImage.Visible = True
        Else
            grpImage.Visible = False
        End If

    End Sub

#Region " -- Methods -- "
    Public Sub LoadAssetData(ByVal ID As String)
        ID = RowCount - 1
        RowCount = RowCount + 1

        Try
            If ID > Sendfrm.grdItemsView.RowCount - 1 Then
                Sendfrm.Enabled = True
                Sendfrm.BringToFront()
                Me.Dispose()

            Else

                txtAstId.Text = objBALAssetDetails.Generate_AssetID()
                txtbase.Text = GetGridRowCellValue(Sendfrm.grdItemsView, ID, "POItmBaseCost").ToString
                txtTotal.Text = CInt(txtbase.Text) + CInt(txtSales.Text)
                '

                itmCode.FindRow(GetGridRowCellValue(Sendfrm.grdItemsView, ID, "ItemCode").ToString, GetGridRowCellValue(Sendfrm.grdItemsView, ID, "ItemCode").ToString)

                If GetGridRowCellValue(Sendfrm.grdItemsView, ID, "ItemCode").ToString <> "" Then
                    Get_Assets_ById(GetGridRowCellValue(Sendfrm.grdItemsView, ID, "ItemCode").ToString)
                End If
                txtPO.Text = GetGridRowCellValue(Sendfrm.grdItemsView, ID, "POCode").ToString
                txtSales.Text = GetGridRowCellValue(Sendfrm.grdItemsView, ID, "AddCharges").ToString 'Additional Cost is here
                txtQty.Tag = GetGridRowCellValue(Sendfrm.grdItemsView, ID, "POItmQty").ToString
                Qty = GetGridRowCellValue(Sendfrm.grdItemsView, ID, "POItmQty").ToString
                Dim RecQty As Integer = 0
                If GetGridRowCellValue(Sendfrm.grdItemsView, ID, "PORecQty").ToString <> "" Then
                    RecQty = GetGridRowCellValue(Sendfrm.grdItemsView, ID, "PORecQty").ToString
                End If

                txtQty.Maximum = GetGridRowCellValue(Sendfrm.grdItemsView, ID, "POItmQty").ToString - RecQty
                txtQty.Minimum = 1
                txtQty.Text = GetGridRowCellValue(Sendfrm.grdItemsView, ID, "POItmQty").ToString

                Dim objAttSupp As New attSupplier
                Dim objBalSupp As New BALSupplier
                objAttSupp.PKeyCode = Sendfrm.suppId
                Dim ds As DataTable = objBalSupp.GetAll_Supplier(objAttSupp)
                If ds Is Nothing = False Then
                    If ds.Rows.Count > 0 Then
                        cmbSupp.FindRow(Sendfrm.suppId, ds.Rows(0)("SuppName"))
                    End If
                End If
                txtItemDesc.Text = GetGridRowCellValue(Sendfrm.grdItemsView, ID, "AstDesc")
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

#End Region

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Sendfrm.Enabled = True
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub ItmCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If itmCode.Text <> "" Then
                Get_Assets_ById(itmCode.SelectedValue)
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnSkip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSkip.Click
        CurrentCount = CurrentCount + 1
        If CurrentCount >= CycleCount Then
            Sendfrm.Enabled = True
            Sendfrm.BringToFront()
            Me.Dispose()
        Else
            For i As Integer = RowCount To Sendfrm.grdItemsView.RowCount - 1
                If CBool(GetGridRowCellValue(Sendfrm.grdItemsView, i, "TRANSFER")) = True And CBool(GetGridRowCellValue(Sendfrm.grdItemsView, i, "IsTrans")) = False And CInt(GetGridRowCellValue(Sendfrm.grdItemsView, i, "POItmQty")) > 0 Then
                    LoadAssetData(RowCount)
                    Exit For
                End If
                RowCount = RowCount + 1
            Next
        End If
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        errProv.ClearErrors()
        If valProvMain.Validate And valProvNotNigative.Validate Then
            Dim objattBookTemp As New attBook
            Dim objBALBookTemp As New BALBooks
            Dim objattAstBooks As New attAstBooks
            Dim ds As DataTable
            objattBookTemp.CompanyID = cmbComp.SelectedValue
            ds = objBALBookTemp.GetAll_Book(objattBookTemp)
            If ds Is Nothing Or ds.Rows.Count < 1 Then
                ZulMessageBox.ShowMe("NoCompanyBookSelected")
            Else
                AddNew_AssetDetails()
            End If
        End If
    End Sub
    Private Sub SaveImage(ByVal AssetsID As String)
        If File.Exists(PBLogo.ImageLocation) Then
            If AppConfig.ImgType = "Asset Images" Then
                If AppConfig.ImgStorgeLoc = "Shared Folder" Then
                    Dim fname As String = ""
                    fname = AssetsID & ".jpg"
                    If PBLogo.ImageLocation <> AppConfig.ImgPath & "\" & fname Then
                        File.Copy(PBLogo.ImageLocation, AppConfig.ImgPath & "\" & fname, True)
                    End If
                ElseIf AppConfig.ImgStorgeLoc = "Database" Then
                    Dim objattAstdtl As attAssetDetails = New attAssetDetails
                    objattAstdtl.PKeyCode = AssetsID
                    Dim fs As New FileStream(PBLogo.ImageLocation, FileMode.Open)
                    Dim Data() As Byte = New [Byte](fs.Length) {}
                    fs.Read(Data, 0, fs.Length)
                    objattAstdtl.Image = Data
                    objBALAssetDetails.UpdateAssetImage(objattAstdtl)
                    fs.Dispose()
                End If
            End If
        End If
    End Sub

    Private Sub AddNew_AssetDetails()
        Try
            If Not objBALAssetDetails.Check_referenceID(txtRef.Text.ToString(), "") Then
                For i As Integer = 0 To CInt(txtQty.Text) - 1
                    objattAssetDetails = New attAssetDetails
                    objattAssetDetails.BaseCost = txtbase.Text
                    objattAssetDetails.CustodianID = cmbCust.SelectedValue
                    objattAssetDetails.SerailNo = RemoveUnnecessaryChars(txtSerialNo.Text)
                    objattAssetDetails.InvNumber = RemoveUnnecessaryChars(txtInvoice.Text)
                    objattAssetDetails.ItemCode = itmCode.SelectedValue
                    objattAssetDetails.POCode = RemoveUnnecessaryChars(txtPO.Text)
                    objattAssetDetails.PurDate = dtpur.Value
                    objattAssetDetails.SuppID = cmbSupp.SelectedValue
                    objattAssetDetails.Tax = RemoveUnnecessaryChars(txtSales.Text)
                    objattAssetDetails.DispDate = Date.MinValue
                    objattAssetDetails.CompanyID = cmbComp.SelectedValue
                    objattAssetDetails.SrvDate = dtService.Value


                    objattAssetDetails.AstBrandID = txtBrandID.SelectedValue
                    objattAssetDetails.AstDesc = RemoveUnnecessaryChars(txtAstDesc1.Text)
                    objattAssetDetails.AstDesc2 = RemoveUnnecessaryChars(txtAstDesc2.Text)
                    objattAssetDetails.AstModel = RemoveUnnecessaryChars(txtAstModel.Text)
                    objattAssetDetails.Discount = RemoveUnnecessaryChars(txtDiscount.Text)
                    objattAssetDetails.IsDataChanged = True

                    If txtPieceNo.Visible = True Then
                        objattAssetDetails.NoPiece = txtPieceNo.Text
                    Else
                        objattAssetDetails.NoPiece = 1
                    End If
                    objattAssetDetails.LocID = ZTLocation.SelectedValue
                    Dim LocCompCode As String = objBALLocation.GetLocCompCode(objattAssetDetails.LocID)
                    objattAssetDetails.Disposed = "0"
                    objattAssetDetails.IsSold = 0
                    objattAssetDetails.AstNum = txtAstNum.Text

                    If txtRef.Text = "" Then
                        objattAssetDetails.RefNo = objattAssetDetails.AstNum
                    Else
                        If i = 0 Then
                            objattAssetDetails.RefNo = RemoveUnnecessaryChars(txtRef.Text)
                        Else
                            objattAssetDetails.RefNo = RemoveUnnecessaryChars(txtRef.Text) & "-" & (i).ToString
                        End If
                    End If

                    objattAssetDetails.PKeyCode = txtAstId.Text
                    txtBarcode.Text = Generate_BarCode(Trim(cmbComp.SelectedValue), Trim(txtAstId.Text), Trim(txtAstNum.Text), Trim(txtRef.Text), Trim(txtCategory.Text), Trim(txtCategory.Text), Trim(ZTLocation.SelectedText), Trim(ZTLocation.SelectedText), LocCompCode)
                    objattAssetDetails.BarCode = txtBarcode.Text
                    objattAssetDetails.CreatedBY = AppConfig.LoginName
                    objattAssetDetails.LastEditBY = AppConfig.LoginName
                    objattAssetDetails.LastEditDate = Now.Date
                    objattAssetDetails.CreationDate = Now.Date

                    objattAssetDetails.CostCenterID = String.Empty
                    objattAssetDetails.BussinessArea = String.Empty
                    objattAssetDetails.InventoryNumber = String.Empty

                    objattAssetDetails.EvaluationGroup1 = String.Empty
                    objattAssetDetails.EvaluationGroup2 = String.Empty
                    objattAssetDetails.EvaluationGroup3 = String.Empty
                    objattAssetDetails.EvaluationGroup4 = String.Empty
                    objattAssetDetails.CustomFld1 = String.Empty
                    objattAssetDetails.CustomFld2 = String.Empty
                    objattAssetDetails.CustomFld3 = String.Empty
                    objattAssetDetails.CustomFld4 = String.Empty
                    objattAssetDetails.CustomFld5 = String.Empty
                    objattAssetDetails.StatusID = 3 '3 In Stock

                    If objBALAssetDetails.Insert_AssetDetails(objattAssetDetails, True) Then
                        If AppConfig.ExportToServer Then
                            Dim Division As String = Get_Cust_DeptID(objattAssetDetails.CustodianID)
                            Dim Category As String = Get_Assets_AstCatID(objattAssetDetails.ItemCode)
                            objAlHadaIntegration.Insert_AssetDetails_ExportServer(objattAssetDetails, Division, Category)
                        End If

                        'Create history for all inventory schedules.
                        Dim objBALAst_INV_Schedule As New BALAst_INV_Schedule
                        Dim dtInvSch As DataTable = objBALAst_INV_Schedule.Getcombo_Schedule()
                        Dim objattAstHistory As attAstHistory
                        Dim objBALAst_History As New BALAst_History
                        For Each dr As DataRow In dtInvSch.Rows
                            objattAstHistory = New attAstHistory
                            objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                            objattAstHistory.AstID = txtAstId.Text
                            objattAstHistory.Status = 0
                            objattAstHistory.InvSchCode = dr("InvSchCode")
                            objattAstHistory.HisDate = Now.Date
                            objattAstHistory.Fr_loc = ZTLocation.SelectedValue
                            objattAstHistory.To_Loc = ZTLocation.SelectedValue
                            objattAstHistory.NoPiece = txtPieceNo.Value
                            objBALAst_History.Insert_Ast_History(objattAstHistory)
                        Next
                    End If


                    SaveImage(txtAstId.Text)


                    Dim ESalValue As Decimal = 0
                    Dim ESalMonth As Decimal = 0
                    Dim ESalYear As Decimal = 0
                    Dim EDepValue As Decimal = 0

                    Dim objBALAssets As New BALItems
                    Dim ds As DataTable = objBALAssets.Get_DepPolicy(objattAssetDetails.ItemCode)
                    If Not ds Is Nothing Then
                        If ds.Rows.Count > 0 Then
                            objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, ds.Rows(0)("SalvageValue"), ds.Rows(0)("SalvageYear"), ds.Rows(0)("SalvageMonth"), dtService.Value, ds.Rows(0)("IsSalvageValuePercent"))
                            ESalValue = ds.Rows(0)("SalvageValue")
                            ESalMonth = ds.Rows(0)("SalvageMonth")
                            ESalYear = ds.Rows(0)("SalvageYear")
                        End If
                    End If

                    If AppConfig.ExportToServer Then
                        objattAstBooks = New attAstBooks
                        objattAstBooks.SalvageYear = ESalYear
                        objattAstBooks.SalvageMonth = ESalMonth
                        EDepValue = ((objattAssetDetails.BaseCost + objattAssetDetails.Tax - ESalValue) / ESalYear) / 12

                        objBALAstBooks.Insert_AstBooks_ExportServer(objattAstBooks, objattAssetDetails.AstNum, objattAssetDetails.SrvDate, objattAssetDetails.BaseCost, 0, EDepValue)
                    End If

                    txtAstId.Text = objBALAssetDetails.Generate_AssetID()

                    txtAstNum.Text = Generate_AssetNumber(cmbComp.SelectedValue)
                    If txtAstNum.Text = "" Then
                        txtQty.Value = i + 1
                        Exit For
                    End If
                    objattAssetDetails.ItemCode = itmCode.SelectedValue
                Next

                If txtQty.Maximum = txtQty.Value Then
                    objattPODetails = New attPODetails
                    objattPODetails.PKeyCode = GetGridRowCellValue(Sendfrm.grdItemsView, RowCount - 2, "POItmID")
                    objattPODetails.PORecQty = Qty
                    objBALPODetails.Transfer(objattPODetails)
                    Sendfrm.Updatehandle.Invoke(RowCount - 2, True, Me)
                    Sendfrm.UpdatePODetails(RowCount - 2, Qty)
                Else
                    objattPODetails = New attPODetails
                    objattPODetails.PKeyCode = GetGridRowCellValue(Sendfrm.grdItemsView, RowCount - 2, "POItmID")
                    objattPODetails.PORecQty = Qty - txtQty.Maximum + txtQty.Value
                    objBALPODetails.UpdateReceivedQty(objattPODetails)
                    Sendfrm.UpdatePODetails(RowCount - 2, objattPODetails.PORecQty)
                End If

                MessageBox.Show(CStr(txtQty.Value) & " Assets transferred successfully," & Chr(13) _
                                 & CStr(txtQty.Maximum - txtQty.Value) & " Assets still in this order.", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)

                CurrentCount = CurrentCount + 1
                If CurrentCount >= CycleCount Then
                    Sendfrm.Enabled = True
                    Sendfrm.BringToFront()
                    Me.Dispose()

                Else
                    For i As Integer = RowCount To Sendfrm.grdItemsView.RowCount - 1
                        If CBool(GetGridRowCellValue(Sendfrm.grdItemsView, i, "TRANSFER")) = True And CBool(GetGridRowCellValue(Sendfrm.grdItemsView, i, "IsTrans")) = False And CInt(GetGridRowCellValue(Sendfrm.grdItemsView, i, "POItmQty")) > 0 Then
                            LoadAssetData(RowCount)
                            Exit For
                        End If
                        RowCount = RowCount + 1
                    Next
                End If
            Else
                txtRef.Focus()
                txtRef.Text = ""
                ZulMessageBox.ShowMe("RefIDExist")
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Function Get_Cust_DeptID(ByVal CustID As String) As String
        Try
            Dim ds As New DataTable
            objattCustodian = New attCustodian
            objattCustodian.PKeyCode = CustID
            ds = objBALCustodian.GetAll_Custodian(objattCustodian)
            If ds IsNot Nothing Then
                Dim deptID As String = CStr(ds.Rows(0)("DeptID"))
                Dim DeptIDSplit() As String = deptID.Split("-"c)
                Return Format(CInt(DeptIDSplit(0)), "0#") & DeptIDSplit(1)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
            Return Nothing
        End Try
    End Function

    Private Function Get_Assets_AstCatID(ByVal itemCode As String) As String
        Try
            Dim ds As New DataTable
            objattAssets = New attItems
            objattAssets.PKeyCode = itemCode
            ds = objBALAssets.GetAll_Items(objattAssets)
            If ds Is Nothing = False Then
                'If ds.Tables.Count > 0 Then
                If ds.Rows.Count > 0 Then
                    Return ds.Rows(0)("AstCatID").ToString
                Else
                    Return Nothing
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
        Return Nothing
    End Function

    Private Sub PBLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PBLogo.Click
        Try
            If dlgFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
                PBLogo.ImageLocation = dlgFile.FileName
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnDelImg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelImg.Click
        Try
            If ZulMessageBox.ShowMe("BeforeDeletedImg", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                btnDelImg.Visible = False
                PBLogo.Image = Nothing
                PBLogo.ImageLocation = Nothing
                ZulMessageBox.ShowMe("AfterDeleteImg")
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub txtbase_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtbase.TextChanged
        Try
            If Trim(txtSales.Text) = "" Then
                txtSales.Text = "0.0"
            End If
            If Trim(txtbase.Text) = "" Then
                txtbase.Text = "0.0"
            End If
            Try
                txtTotal.Text = Format(CType(txtbase.Text, Double) + CType(txtSales.Text, Double), "###,###,###,###,###.00")
            Catch ex As Exception
                txtSales.Text = "0.0"
                txtbase.Text = "0.0"
            End Try
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub txtSales_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSales.TextChanged
        Try
            If Trim(txtSales.Text) = "" Then
                txtSales.Text = "0"
            End If
            If Trim(txtbase.Text) = "" Then
                txtbase.Text = "0"
            End If
            Try
                txtTotal.Text = Format(CType(txtbase.Text, Double) + CType(txtSales.Text, Double), "###,###,###,###,###.00")
            Catch ex As Exception
                txtSales.Text = "0"
                txtbase.Text = "0.0"
            End Try
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub itmCode_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles itmCode.SelectTextChanged
        Try
            If itmCode.SelectedText <> "" Then
                Get_Assets_ById(itmCode.SelectedValue)
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Function Get_Assets_ById(ByVal _Id As String) As DataTable
        Try
            Dim ds As New DataTable
            Dim dt As New DataTable
            objattAssets = New attItems
            objattAssets.PKeyCode = _Id
            ds = objBALAssets.GetAllData_Joined(objattAssets)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    dt = ds

                    txtCategory.Text = Convert.ToString(dt.Rows(0)(6))

                    If Not (Convert.ToString(dt.Rows(0)(7)) Is Nothing) Then
                        txtCategory.Tag = Convert.ToString(dt.Rows(0)(7))

                        Dim objBALCategory As New BALCategory

                        txtCategory.Text = objBALCategory.Comp_Path(txtCategory.Tag)
                    End If

                    txtItemDesc.Text = Convert.ToString(dt.Rows(0)(2))

                    Return Nothing
                End If
            End If

            txtCategory.Text = ""
            txtItemDesc.Text = ""

            Return Nothing
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
            Return Nothing
        End Try
    End Function

    Private Sub cmbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbType.SelectedIndexChanged
        If cmbType.SelectedIndex = 0 Then
            Label3.Visible = False
            txtPieceNo.Visible = False
            txtPieceNo.Value = 1
        Else
            Label3.Visible = True
            txtPieceNo.Visible = True
        End If
    End Sub

    Private Sub txtQty_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtQty.Validating, txtPieceNo.Validating
        If CType(sender, NumericUpDown).Value > CType(sender, NumericUpDown).Maximum Then
            CType(sender, NumericUpDown).Value = CType(sender, NumericUpDown).Maximum
        End If
    End Sub

    Private Sub btnAddBrand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddBrand.Click
        Dim frm As New frmBrand
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
    End Sub

    Private Sub btnAddCust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCust.Click
        Dim frm As New frmCustodian
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
    End Sub

    Private Sub btnAddLoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddLoc.Click
        Dim frm As New frmLocation
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
    End Sub


    Private Sub btnAddGLCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddGLCode.Click
        Dim frm As New frmGLCodes
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
    End Sub

    Private Sub btnAddCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCompany.Click
        Dim frm As New frmCompanies
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
    End Sub

    Private Sub btnAddSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSupplier.Click
        Dim frm As New frmSupplier
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
    End Sub

    Private Sub cmbSupp_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSupp.LovBtnClick
        Try
            cmbSupp.ValueMember = "SuppID"
            cmbSupp.DisplayMember = "SuppName"
            Dim objBALSupplier As New BALSupplier
            cmbSupp.DataSource = objBALSupplier.GetAllData_GetCombo()
            cmbSupp.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmbCust_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCust.LovBtnClick
        Try
            Dim objBALCustodian As New BALCustodian
            cmbCust.ValueMember = "ID"
            cmbCust.DisplayMember = "Name"
            cmbCust.DataSource = objBALCustodian.GetAllData_GetCombo()
            cmbCust.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub itmCode_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles itmCode.LovBtnClick
        Try
            itmCode.ValueMember = "ItemCode"
            itmCode.DisplayMember = "ItemCode"
            Dim objBALAssets As New BALItems
            itmCode.DataSource = objBALAssets.GetAllData_GetCombo(New attItems)
            itmCode.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub txtBrandID_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBrandID.LovBtnClick
        Try
            txtBrandID.ValueMember = "AstBrandID"
            txtBrandID.DisplayMember = "AstBrandName"
            Dim objBALBrand As New BALbrand
            txtBrandID.DataSource = objBALBrand.GetAll_Brand(New attBrand)
            txtBrandID.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmbComp_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbComp.LovBtnClick
        Try
            cmbComp.ValueMember = "CompanyID"
            cmbComp.DisplayMember = "CompanyName"
            Dim objBALCompany As New BALCompany
            cmbComp.DataSource = objBALCompany.GetAllData_GetCombo(New attcompany)
            cmbComp.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmbGLCode_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGLCode.LovBtnClick
        Try
            cmbGLCode.ValueMember = "GLcode"
            cmbGLCode.DisplayMember = "GLcode"
            Dim objBALGLCode As New BALGLCode
            cmbGLCode.DataSource = objBALGLCode.GetCompanyGLCodes(cmbComp.SelectedValue)
            cmbGLCode.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub ZTLocation_TreeBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZTLocation.TreeBtnClick
        Try
            Dim ds As New DataTable
            Dim objBALLocation As New BALLocation
            ZTLocation.DataSource = objBALLocation.GetComboLocations(New attLocation)
            ZTLocation.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

End Class