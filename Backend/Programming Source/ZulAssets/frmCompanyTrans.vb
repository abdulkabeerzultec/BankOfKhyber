Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO
Public Class frmCompanyTrans

#Region " -- Declerations --"
    Dim objattAssetsCoding As attAssetsCoding
    Dim objBALAssetsCoding As New BALAssetsCoding
    Dim objattAssetDetails As attAssetDetails
    Dim objBALAssetDetails As New BALAssetDetails
    Dim objattCompany As attcompany
    Dim objBALCompany As New BALCompany
    Dim objattAstBooks As attAstBooks
    Dim objBALAstBooks As New BALAstBooks
    Dim objAlHadaIntegration As New BALAlhadaIntegration

    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Dim valProvNotNigative As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider

#End Region
#Region " -- Methods -- "
    Private Sub format_Grid_ExpHist()

        With grdView

            .Columns(0).Caption = "Book ID"
            .Columns(0).Width = 70

            .Columns(1).Caption = "Asset ID"
            .Columns(1).Width = 100

            .Columns(2).Width = 80
            .Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(3).Width = 80
            .Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(4).Width = 80
            .Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(5).Width = 80
            .Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(6).Width = 80
            .Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(7).Width = 80
            .Columns(7).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

            .Columns(8).Visible = False
            .Columns(9).Visible = False
            .Columns(10).Visible = False
            .Columns(11).Visible = False

            .Columns(12).Caption = "Description"
            .Columns(12).Width = 100
            .Columns(13).Visible = False
            .Columns(14).Visible = False
            .Columns(15).Visible = False
        End With
        addGridMenu(grd)
    End Sub

    Private Sub Get_AstBooks(ByVal _id As String)
        Try
            Dim ds As New DataTable
            objattAstBooks = New attAstBooks
            objattAstBooks.AstID = _id
            ds = objBALAstBooks.GetAllData_Detail(objattAstBooks)
            grd.DataSource = ds
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Function Get_AssetsDetails_byID(ByVal _id As String) As DataTable
        Dim ds As New DataTable
        Try
            objattAssetDetails = New attAssetDetails
            objattAssetDetails.PKeyCode = _id
            ds = objBALAssetDetails.GetAll_AssetDetails(objattAssetDetails)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Return ds
                End If
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
        Return Nothing
    End Function

    Private Sub Search_Asset()
        Try
            Dim dt As New DataTable
            Dim ds As New DataTable
            If AssetsID.SelectedText = "0" Then AssetsID.SelectedText = ""
            If Trim(AssetsID.SelectedText) <> "" Or Trim(AssetsID.SelectedText) <> "" Then
                dt = Get_AssetsDetails_byID(AssetsID.SelectedText)

                If dt.Rows(0)("Disposed").ToString() Then
                    errProv.SetError(AssetsID.TextBox, My.MessagesResource.Messages.CanNotTransferDisposes)
                Else
                    errProv.ClearErrors()
                End If

                If dt Is Nothing Then
                    txtAstDesc.Text = ""
                    txtRef.Text = ""
                    txtAstNum.Text = ""
                    AssetsID.SelectedText = ""
                    AssetsID.SelectedValue = ""
                    dtpur.Text = ""
                    cmbComp.SelectedIndex = -1
                    txtReFIDNew.Text = ""
                    txtBookVal.Text = ""
                    txtSalVal.Text = ""
                    txtSalYr.Text = ""
                    CType(grd.DataSource, DataTable).Rows.Clear()
                    '
                Else
                    txtAstDesc.Text = dt.Rows(0)("AstDesc").ToString()
                    txtRef.Text = dt.Rows(0)("RefNo").ToString()
                    txtAstNum.Text = dt.Rows(0)("AstNum").ToString()
                    AssetsID.SelectedText = dt.Rows(0)("AstID").ToString()
                    AssetsID.SelectedValue = dt.Rows(0)("AstID").ToString()
                    txtCName.Text = dt.Rows(0)("CompanyName").ToString()
                    txtCName.Tag = dt.Rows(0)("CompanyID").ToString()
                    dtpur.Text = dt.Rows(0)("PurDate").ToString()
                    txtGLCode.Text = dt.Rows(0)("GLCode").ToString()
                    Get_AstBooks(AssetsID.SelectedValue)
                    format_Grid_ExpHist()
                End If
            Else
                txtAstDesc.Text = ""
                txtRef.Text = ""
                txtAstNum.Text = ""
                AssetsID.SelectedText = ""
                AssetsID.SelectedValue = ""
                dtpur.Text = ""
                txtCName.Text = ""
                If grd.DataSource IsNot Nothing Then
                    CType(grd.DataSource, DataTable).Rows.Clear()
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

#End Region

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Search_Asset()
    End Sub


    Private Sub frmCompanyTrans_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmCompanyTrans = Nothing
    End Sub

    Private Sub frmCompanyTrans_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        valProvMain.SetValidationRule(cmbNewGLCode.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(cmbComp.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(AssetsID.TextBox, valRulenotEmpty)

        valProvMain.SetValidationRule(txtSalYr, valRuleGreaterThanZero)
        valProvMain.SetValidationRule(txtBookVal, valRuleGreaterThanZero)

        valProvMain.SetValidationRule(txtSalVal, valRulenotEmpty)

        valProvNotNigative.SetValidationRule(txtBookVal, valRuleNotContainMinus)

        txtSalVal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        txtSalVal.Properties.Mask.EditMask = "\d{1,8}"

        txtSalYr.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        txtSalYr.Properties.Mask.EditMask = "\d{1,2}"

        txtSalVal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        txtSalVal.Properties.Mask.EditMask = "[01]"

        txtBookVal.Properties.Mask.MaskType = NumberMaskType
        txtBookVal.Properties.Mask.EditMask = NumberMask
        txtBookVal.Properties.Mask.UseMaskAsDisplayFormat = True

        Get_AstBooks(-1)
        format_Grid_ExpHist()
    End Sub
  

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        FormController.objfrmCompanyTrans = Nothing
        Me.Dispose()
    End Sub

    Private Sub btnTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrans.Click
        If txtCName.Text <> "" Then
            If valProvMain.Validate And valProvNotNigative.Validate Then
                'disallow transfering disposed assets.
                If objBALAssetDetails.IsAssetDisposed(AssetsID.SelectedText) Then
                    errProv.SetError(AssetsID.TextBox, My.MessagesResource.Messages.CanNotTransferDisposes)
                Else
                    errProv.ClearErrors()
                    If AppConfig.CodingMode Then
                        Dim AstNum As String = Generate_AssetNumber(cmbComp.SelectedValue)
                        If AstNum = "" Then
                            errProv.SetError(cmbComp.TextBox, My.MessagesResource.Messages.DefineRange)
                        Else
                            errProv.ClearErrors()
                            If Not txtCName.Tag = cmbComp.SelectedValue Then
                                AddNew_AssetDetails_withMsgBox()
                            Else
                                errProv.SetError(cmbComp.TextBox, My.MessagesResource.Messages.SameCoError)
                            End If
                        End If
                    Else
                        If Not txtCName.Tag = cmbComp.SelectedValue Then
                            AddNew_AssetDetails_withMsgBox()
                        Else
                            errProv.SetError(cmbComp.TextBox, My.MessagesResource.Messages.SameCoError)
                        End If
                    End If
                End If
            End If
        End If
    End Sub


    Private Sub AddNew_AssetDetails_withMsgBox()
        Try
            Dim dt As DataTable
            Dim Location, Category, LocCompCode As String
            Dim objBALCategory As New BALCategory
            Dim objBALLocation As New BALLocation
            Dim objattAssetDetails As New attAssetDetails
            dt = Get_AssetsDetails_byID(AssetsID.SelectedText)
            If dt Is Nothing = False Then
                If dt.Rows.Count > 0 Then
                    objattAssetDetails.PKeyCode = objBALAssetDetails.Generate_AssetID()
                    objattAssetDetails.AstNum = Generate_AssetNumber(cmbComp.SelectedValue)
                    If txtReFIDNew.Text.Trim = "" Then
                        txtReFIDNew.Text = objattAssetDetails.AstNum
                    Else
                        objattAssetDetails.RefNo = txtReFIDNew.Text
                    End If

                    If objattAssetDetails.AstNum = 0 Then
                        Exit Sub
                    End If
                    If Not objBALAssetDetails.Check_referenceID(txtReFIDNew.Text.ToString(), objattAssetDetails.PKeyCode) Then
                        objattAssetDetails.BaseCost = txtBookVal.Text
                        objattAssetDetails.TransRemarks = txtRemarks.Text
                        objattAssetDetails.CustodianID = dt.Rows(0)("CustodianID").ToString()
                        objattAssetDetails.InsID = dt.Rows(0)("InsID").ToString()
                        objattAssetDetails.InvNumber = dt.Rows(0)("InvNumber").ToString()
                        objattAssetDetails.ItemCode = dt.Rows(0)("ItemCode").ToString()
                        objattAssetDetails.AstBrandID = dt.Rows(0)("AstBrandId").ToString()
                        objattAssetDetails.AstDesc = dt.Rows(0)("AstDesc").ToString()
                        objattAssetDetails.AstModel = dt.Rows(0)("AstModel").ToString()
                        Location = objBALLocation.Comp_Path(dt.Rows(0)("LocID").ToString())
                        LocCompCode = objBALLocation.Comp_Path(dt.Rows(0)("LocID").ToString())
                        Category = objBALCategory.Comp_Path(dt.Rows(0)("AstCatID").ToString())
                        objattAssetDetails.BarCode = Generate_BarCode(Trim(cmbComp.SelectedValue), Trim(AssetsID.SelectedText), Trim(txtAstNum.Text), Trim(txtRef.Text), Category, Category, Location, Location, LocCompCode)
                        objattAssetDetails.POCode = dt.Rows(0)("POCOde").ToString()
                        objattAssetDetails.PurDate = dt.Rows(0)("PurDate").ToString()
                        objattAssetDetails.SuppID = dt.Rows(0)("SuppID").ToString()
                        objattAssetDetails.SrvDate = dt.Rows(0)("SrvDate").ToString()
                        objattAssetDetails.Tax = CDbl(dt.Rows(0)("Tax").ToString())
                        objattAssetDetails.SuppID = dt.Rows(0)("SuppID").ToString()
                        objattAssetDetails.InsID = dt.Rows(0)("InsID").ToString()
                        objattAssetDetails.CompanyID = cmbComp.SelectedValue
                        objattAssetDetails.IsDataChanged = True

                        objattAssetDetails.GLCode = cmbNewGLCode.SelectedText
                        If (dt.Rows(0)("LocId").ToString <> "") Then
                            objattAssetDetails.LocID = dt.Rows(0)("LocId").ToString()
                        End If
                        If CType(dt.Rows(0)("Disposed"), Boolean) Then
                            objattAssetDetails.DepCode = dt.Rows(0)("Dispcode")
                            If dt.Rows(0)("DispDate").ToString() <> "" Then objattAssetDetails.DispDate = dt.Rows(0)("DispDate")
                            If dt.Rows(0)("Dispcode") = "1" Then
                                If dt.Rows(0)("Sel_date").ToString() <> "" Then objattAssetDetails.Sel_Date = dt.Rows(0)("Sel_date")
                                If dt.Rows(0)("Sel_Price").ToString() <> "" Then objattAssetDetails.Sel_Price = Format(CDbl(dt.Rows(0)("Sel_Price")), "###,###,###,###,###.00")
                                If dt.Rows(0)("Soldto").ToString() <> "" Then objattAssetDetails.SoldTo = dt.Rows(0)("Soldto")
                            End If
                        End If
                        objattAssetDetails.OldAssetID = AssetsID.SelectedValue
                        objattAssetDetails.CreatedBY = AppConfig.LoginName
                        objattAssetDetails.LastEditBY = AppConfig.LoginName
                        objattAssetDetails.LastEditDate = Now.Date
                        objattAssetDetails.CreationDate = Now.Date

                        objattAssetDetails.CostCenterID = dt.Rows(0)("CostID").ToString.Trim
                        objattAssetDetails.BussinessArea = dt.Rows(0)("BussinessArea").ToString.Trim
                        objattAssetDetails.InventoryNumber = dt.Rows(0)("InventoryNumber").ToString.Trim

                        objattAssetDetails.CustomFld1 = dt.Rows(0)("CustomFld1").ToString.Trim
                        objattAssetDetails.CustomFld2 = dt.Rows(0)("CustomFld2").ToString.Trim
                        objattAssetDetails.CustomFld3 = dt.Rows(0)("CustomFld3").ToString.Trim
                        objattAssetDetails.CustomFld4 = dt.Rows(0)("CustomFld4").ToString.Trim
                        objattAssetDetails.CustomFld5 = dt.Rows(0)("CustomFld5").ToString.Trim

                        objattAssetDetails.EvaluationGroup1 = dt.Rows(0)("EvaluationGroup1").ToString.Trim
                        objattAssetDetails.EvaluationGroup2 = dt.Rows(0)("EvaluationGroup2").ToString.Trim
                        objattAssetDetails.EvaluationGroup3 = dt.Rows(0)("EvaluationGroup3").ToString.Trim
                        objattAssetDetails.EvaluationGroup4 = dt.Rows(0)("EvaluationGroup4").ToString.Trim
                        objattAssetDetails.StatusID = 1

                        objBALAssetDetails.Insert_AssetDetails(objattAssetDetails, True)

                        txtNewAstID.Text = objattAssetDetails.PKeyCode
                        txtNewAstNum.Text = objattAssetDetails.AstNum
                        txtNewGLCode.Text = objattAssetDetails.GLCode

                        If AppConfig.ExportToServer Then
                            objattAssetDetails.Disposed = dt.Rows(0)("Disposed")
                            objattAssetDetails.SerailNo = dt.Rows(0)("SerailNo").ToString()
                            objattAssetDetails.PONumber = dt.Rows(0)("PONumber").ToString
                            objattAssetDetails.GLCode = cmbNewGLCode.SelectedText

                            Dim Division As String = objBALAssetDetails.Get_Cust_DeptID(objattAssetDetails.CustodianID)
                            Dim Cat As String = objBALAssetDetails.Get_Assets_AstCatID(objattAssetDetails.ItemCode)
                            objAlHadaIntegration.Insert_AssetDetails_ExportServer(objattAssetDetails, Division, Cat)
                        End If

                        CopyAssetImage(AssetsID.SelectedValue, objattAssetDetails.PKeyCode)

                        Dim objBALAssets As New BALItems
                        Dim ds As DataTable = objBALAssets.Get_DepPolicy(objattAssetDetails.ItemCode)
                        If Not ds Is Nothing Then
                            If ds.Rows.Count > 0 Then
                                objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, txtSalVal.Text, txtSalYr.Text, 0, DateTime.Today, False)
                            End If
                        End If

                        Dim objBALAssetDetails1 As New BALAssetDetails
                        Dim objattAssetDatails1 As New attAssetDetails
                        objattAssetDatails1.PKeyCode = AssetsID.SelectedValue
                        objattAssetDatails1.DispCode = 3
                        objattAssetDatails1.DispDate = Now.Date
                        objBALAssetDetails1.Dispose_Asset(objattAssetDatails1)

                        If AppConfig.ExportToServer Then
                            objattAssetDatails1.AstNum = objattAssetDetails.AstNum
                            objAlHadaIntegration.Dispose_AssetDetails_ExportServer(objattAssetDatails1)
                        End If

                        ZulMessageBox.ShowMe("AssetTransferred")
                        txtAstDesc.Text = ""
                        txtRef.Text = ""
                        txtAstNum.Text = ""
                        AssetsID.SelectedText = ""
                        AssetsID.SelectedValue = ""
                        dtpur.Text = ""
                        txtRemarks.Text = ""
                        txtCName.Text = ""
                        cmbComp.SelectedIndex = -1
                        txtReFIDNew.Text = ""
                        txtBookVal.Text = ""
                        txtSalVal.Text = ""
                        txtSalYr.Text = ""
                        txtGLCode.Text = ""
                        cmbNewGLCode.SelectedText = ""
                        CType(grd.DataSource, DataTable).Rows.Clear()

                    Else
                        txtReFIDNew.Focus()
                        txtReFIDNew.Text = ""
                        ZulMessageBox.ShowMe("RefIDExist")
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Function Get_AssetsDetails_byID(ByVal _id As String, ByVal _astNum As String) As DataTable
        Try
            Dim ds As New DataTable

            objattAssetDetails = New attAssetDetails
            objattAssetDetails.PKeyCode = _id
            If _astNum <> "" Then
                objattAssetDetails.AstNum = _astNum
            End If

            ds = objBALAssetDetails.GetAll_AssetDetails(objattAssetDetails)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Return ds
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
        Return Nothing
    End Function

    Private Sub CopyAssetImage(ByVal strAstIdOld As String, ByVal strAstIdNew As String)
        If strAstIdOld <> "" And strAstIdNew <> "" Then
            Try
                If AppConfig.ImgType = "Asset Images" Then
                    If AppConfig.ImgStorgeLoc = "Shared Folder" Then
                        If File.Exists(AppConfig.ImgPath & "\" & strAstIdOld & ".jpg") Then
                            File.Copy(AppConfig.ImgPath & "\" & strAstIdOld & ".jpg", AppConfig.ImgPath & "\" & strAstIdNew & ".jpg", True)
                        End If
                    ElseIf AppConfig.ImgStorgeLoc = "Database" Then
                        objattAssetDetails = New attAssetDetails
                        objattAssetDetails.PKeyCode = strAstIdOld
                        Dim ImageBytes As Byte() = objBALAssetDetails.GetAssetImage(objattAssetDetails)
                        objBALAssetDetails.CopyAssetImage(strAstIdNew, ImageBytes)
                    End If
                End If
            Catch ex As Exception
                GenericExceptionHandler(ex, WhoCalledMe)
            End Try
        End If

    End Sub


    Private Sub AssetsID_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles AssetsID.SelectTextChanged
        Me.Search_Asset()
    End Sub

    Private Sub cmbComp_SelectTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbComp.SelectTextChanged
        If cmbComp.SelectedValue <> "" Then
            If AppConfig.CodingMode Then
                Dim AstNum As String = Generate_AssetNumber(cmbComp.SelectedValue)
                If AstNum = "" Then
                    errProv.SetError(cmbComp.TextBox, My.MessagesResource.Messages.DefineRange)
                Else
                    errProv.ClearErrors()
                End If

            End If
        Else
            errProv.ClearErrors()
        End If
        cmbNewGLCode.SelectedText = ""
    End Sub

    Private Sub grdView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdView.Click
        grdView_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub grdView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Try
            Dim FocRow As Integer = grdView.FocusedRowHandle
            If FocRow >= 0 Then

                If Not (GetGridRowCellValue(grdView, FocRow, "CurrentBV") Is Nothing) Then txtBookVal.Text = Format(CDbl(GetGridRowCellValue(grdView, FocRow, "CurrentBV").ToString()), "###,###,###,###,###.00")
                If Not (GetGridRowCellValue(grdView, FocRow, "SalvageValue") Is Nothing) Then txtSalVal.Text = GetGridRowCellValue(grdView, FocRow, "SalvageValue").ToString()
                If Not (GetGridRowCellValue(grdView, FocRow, "SalvageYear") Is Nothing) Then
                    If CInt(GetGridRowCellValue(grdView, FocRow, "SalvageYear").ToString()) - DateDiff(DateInterval.Year, CDate(dtpur.Text), Now.Date) > 0 Then
                        txtSalYr.Text = CInt(GetGridRowCellValue(grdView, FocRow, "SalvageYear").ToString()) - DateDiff(DateInterval.Year, CDate(dtpur.Text), Now.Date)
                    Else
                        txtSalYr.Text = 0
                    End If
                End If


            End If
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

    Private Sub AssetsID_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssetsID.LovBtnClick
        Try
            AssetsID.ValueMember = "AstId"
            AssetsID.DisplayMember = "AstId"
            Dim objattAssetDetails As attAssetDetails = New attAssetDetails
            Dim objBALAssetDetails As BALAssetDetails = New BALAssetDetails
            objattAssetDetails.Disposed = False
            AssetsID.DataSource = objBALAssetDetails.GetAsset_DetailsCombo(objattAssetDetails)
            AssetsID.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmbNewGLCode_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNewGLCode.LovBtnClick
        Try
            cmbNewGLCode.ValueMember = "GLcode"
            cmbNewGLCode.DisplayMember = "GLcode"
            Dim objBALGLCode As New BALGLCode
            cmbNewGLCode.DataSource = objBALGLCode.GetCompanyGLCodes(cmbComp.SelectedValue)
            cmbNewGLCode.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
End Class