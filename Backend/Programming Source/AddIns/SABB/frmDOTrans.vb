Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO
Imports System.Windows.Forms

Public Class frmDOTrans

#Region " -- My Decleration -- "
    Dim objattPODetails As attPODetails
    Dim objBALPODetails As New BALPODetails
    Dim AstCount As Integer
    Dim objattLocation As attLocation
    Dim objBALLocation As New BALLocation
    Dim objattAssetDetails As attAssetDetails
    Dim objBALAssetDetails As New BALAssetDetails
    Dim objattAssets As attItems
    Dim objBALAssets As New BALItems
    Dim objattCompany As attcompany
    Dim objBALCompany As New BALCompany
    Dim objattBrand As attBrand
    Dim objBALbrand As New BALbrand
    Public Sendfrm As frmDOReceiving
    Public CycleCount As Double
    Public Delegate Sub Update1()
    Public RowCount As Integer = 0
    Public CurrentCount As Double
    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Dim valProvNotNigative As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider

    Dim SerialNumber_DT As New DataTable
    Dim CompanyID As Integer = 1 'Default Company ID
    Dim AstBrandID As Integer = 99999
#End Region

    Private Sub frmPOTrans_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Sendfrm.Enabled = True
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub frmPOTrans_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG
        Me.BackgroundImageLayout = ImageLayout.Stretch

        valProvMain.SetValidationRule(itmCode.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(ZTLocation.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(txtbase, valRuleGreaterThanZero)

        valProvNotNigative.SetValidationRule(txtbase, valRuleNotContainMinus)
        valProvNotNigative.SetValidationRule(txtSales, valRuleNotContainMinus)
        valProvNotNigative.SetValidationRule(txtDiscount, valRuleNotContainMinus)
        valProvMain.SetValidationRule(txtIncident, valRulenotEmpty)

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

        InitSerialGridItems()

        dtpur.CustomFormat = AppConfig.MaindateFormat

        AstCount = Sendfrm.grdItemsView.RowCount
        For i As Integer = 0 To Sendfrm.grdItemsView.RowCount - 1
            If CBool(GetGridRowCellValue(Sendfrm.grdItemsView, i, "TRANSFER")) = True And CBool(GetGridRowCellValue(Sendfrm.grdItemsView, i, "IsTrans")) = False And CInt(GetGridRowCellValue(Sendfrm.grdItemsView, i, "POItmQty")) > 0 Then
                LoadAssetData(RowCount)
                Exit For
            End If
            RowCount = RowCount + 1
        Next
    End Sub

    Private Sub InitSerialGridItems()
        SerialNumber_DT.Columns.Add("#", System.Type.GetType("System.Int32"))
        SerialNumber_DT.Columns.Add("SerialNumber", System.Type.GetType("System.String"))
        grdSerial.DataSource = SerialNumber_DT
        grvSerial.Columns("#").Width = 10
        grvSerial.Columns("#").OptionsColumn.AllowEdit = False
        grvSerial.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grvSerial.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
    End Sub

#Region " -- Methods -- "
    Public Sub LoadAssetData(ByVal ID As String)
        ID = RowCount
        RowCount = RowCount + 1

        Try
            If ID > Sendfrm.grdItemsView.RowCount - 1 Then
                Sendfrm.Enabled = True
                Sendfrm.BringToFront()
                Me.Dispose()

            Else

                txtbase.Text = GetGridRowCellValue(Sendfrm.grdItemsView, ID, "POItmBaseCost").ToString
                txtTotal.Text = CInt(txtbase.Text) + CInt(txtSales.Text)
                '

                itmCode.FindRow(GetGridRowCellValue(Sendfrm.grdItemsView, ID, "ItemCode").ToString, GetGridRowCellValue(Sendfrm.grdItemsView, ID, "ItemCode").ToString)

                If GetGridRowCellValue(Sendfrm.grdItemsView, ID, "ItemCode").ToString <> "" Then
                    Get_Assets_ById(GetGridRowCellValue(Sendfrm.grdItemsView, ID, "ItemCode").ToString)
                End If
                txtPO.Text = GetGridRowCellValue(Sendfrm.grdItemsView, ID, "POCode").ToString
                txtSales.Text = GetGridRowCellValue(Sendfrm.grdItemsView, ID, "AddCharges").ToString 'Additional Cost is here
                Dim RecQty As Integer = 0
                If GetGridRowCellValue(Sendfrm.grdItemsView, ID, "PORecQty").ToString <> "" Then
                    RecQty = GetGridRowCellValue(Sendfrm.grdItemsView, ID, "PORecQty").ToString
                End If

                txtOrderQty.Value = GetGridRowCellValue(Sendfrm.grdItemsView, ID, "POItmQty").ToString
                'Clear the rows before changing the received QTY, 
                SerialNumber_DT.Rows.Clear()
                txtReceivedQTY.Properties.MaxValue = GetGridRowCellValue(Sendfrm.grdItemsView, ID, "POItmQty").ToString - RecQty
                txtReceivedQTY.Value = GetGridRowCellValue(Sendfrm.grdItemsView, ID, "POItmQty").ToString - RecQty
                txtReceivedQTY.Properties.MinValue = 1


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
        Dim expression As String = "SerialNumber = ''"
        If SerialNumber_DT.Select(expression).Length > 0 Then
            ZulMessageBox.ShowErrorMessage("All serial numbers must be entered.")
        Else
            If valProvMain.Validate And valProvNotNigative.Validate Then
                Dim objattBookTemp As New attBook
                Dim objBALBookTemp As New BALBooks
                Dim objattAstBooks As New attAstBooks
                Dim ds As DataTable
                objattBookTemp.CompanyID = CompanyID
                ds = objBALBookTemp.GetAll_Book(objattBookTemp)
                If ds Is Nothing Or ds.Rows.Count < 1 Then
                    ZulMessageBox.ShowMe("NoCompanyBookSelected")
                Else
                    AddNew_AssetDetails()
                End If
            End If
        End If
    End Sub


    Private Sub AddNew_AssetDetails()
        Try
            For i As Integer = 0 To grvSerial.RowCount - 1
                objattAssetDetails = New attAssetDetails
                objattAssetDetails.BaseCost = txtbase.Text
                objattAssetDetails.CustodianID = 1
                objattAssetDetails.GLCode = 1
                objattAssetDetails.SerailNo = RemoveUnnecessaryChars(grvSerial.GetRowCellValue(i, "SerialNumber"))
                objattAssetDetails.InvNumber = RemoveUnnecessaryChars(txtInvoice.Text)
                objattAssetDetails.ItemCode = itmCode.SelectedValue
                objattAssetDetails.POCode = RemoveUnnecessaryChars(txtPO.Text)
                objattAssetDetails.PurDate = dtpur.Value
                objattAssetDetails.SuppID = cmbSupp.SelectedValue
                objattAssetDetails.Tax = RemoveUnnecessaryChars(txtSales.Text)
                objattAssetDetails.DispDate = Date.MinValue
                objattAssetDetails.CompanyID = CompanyID
                objattAssetDetails.SrvDate = dtpur.Value

                objattAssetDetails.AstBrandID = AstBrandID
                objattAssetDetails.AstDesc = txtItemDesc.Text.Trim
                objattAssetDetails.AstDesc2 = ""
                objattAssetDetails.AstModel = RemoveUnnecessaryChars(txtAstModel.Text)
                objattAssetDetails.Discount = RemoveUnnecessaryChars(txtDiscount.Text)
                objattAssetDetails.IsDataChanged = True
                objattAssetDetails.NoPiece = 1
                objattAssetDetails.LocID = ZTLocation.SelectedValue
                objattAssetDetails.Disposed = "0"
                objattAssetDetails.IsSold = 0
                objattAssetDetails.AstNum = Generate_AssetNumber(CompanyID)
                objattAssetDetails.RefNo = objattAssetDetails.AstNum

                objattAssetDetails.PKeyCode = objBALAssetDetails.Generate_AssetID()
                objattAssetDetails.BarCode = objattAssetDetails.SerailNo
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
                objattAssetDetails.CustomFld3 = txtIncident.Text.Trim
                objattAssetDetails.CustomFld4 = String.Empty
                objattAssetDetails.CustomFld5 = String.Empty
                objattAssetDetails.StatusID = 3 '3 In Stock

                If objBALAssetDetails.Insert_AssetDetails(objattAssetDetails, True) Then
                    'Create history for all inventory schedules.
                    Dim objBALAst_INV_Schedule As New BALAst_INV_Schedule
                    Dim dtInvSch As DataTable = objBALAst_INV_Schedule.Getcombo_Schedule()
                    Dim objattAstHistory As attAstHistory
                    Dim objBALAst_History As New BALAst_History
                    For Each dr As DataRow In dtInvSch.Rows
                        objattAstHistory = New attAstHistory
                        objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                        objattAstHistory.AstID = objattAssetDetails.PKeyCode
                        objattAstHistory.Status = 0
                        objattAstHistory.InvSchCode = dr("InvSchCode")
                        objattAstHistory.HisDate = Now.Date
                        objattAstHistory.Fr_loc = ZTLocation.SelectedValue
                        objattAstHistory.To_Loc = ZTLocation.SelectedValue
                        objattAstHistory.NoPiece = 1
                        objBALAst_History.Insert_Ast_History(objattAstHistory)
                    Next
                End If


                Dim ESalValue As Decimal = 0
                Dim ESalMonth As Decimal = 0
                Dim ESalYear As Decimal = 0
                Dim EDepValue As Decimal = 0

                Dim objBALAssets As New BALItems
                Dim ds As DataTable = objBALAssets.Get_DepPolicy(objattAssetDetails.ItemCode)
                If Not ds Is Nothing Then
                    If ds.Rows.Count > 0 Then
                        objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, ds.Rows(0)("SalvageValue"), ds.Rows(0)("SalvageYear"), ds.Rows(0)("SalvageMonth"), dtpur.Value, ds.Rows(0)("IsSalvageValuePercent"))
                        ESalValue = ds.Rows(0)("SalvageValue")
                        ESalMonth = ds.Rows(0)("SalvageMonth")
                        ESalYear = ds.Rows(0)("SalvageYear")
                    End If
                End If

                objattAssetDetails.ItemCode = itmCode.SelectedValue
            Next

            If txtReceivedQTY.Properties.MaxValue = txtReceivedQTY.Value Then
                objattPODetails = New attPODetails
                objattPODetails.PKeyCode = GetGridRowCellValue(Sendfrm.grdItemsView, RowCount - 1, "POItmID")
                objattPODetails.PORecQty = txtOrderQty.Value
                objBALPODetails.Transfer(objattPODetails)
                Sendfrm.Updatehandle.Invoke(RowCount - 1, True, Me)
                Sendfrm.UpdatePODetails(RowCount - 1, txtOrderQty.Value)
            Else
                objattPODetails = New attPODetails
                objattPODetails.PKeyCode = GetGridRowCellValue(Sendfrm.grdItemsView, RowCount - 1, "POItmID")
                objattPODetails.PORecQty = txtOrderQty.Value - txtReceivedQTY.Properties.MaxValue + txtReceivedQTY.Value
                objBALPODetails.UpdateReceivedQty(objattPODetails)
                Sendfrm.UpdatePODetails(RowCount - 1, objattPODetails.PORecQty)
            End If

            MessageBox.Show(CStr(txtReceivedQTY.Value) & " Assets received successfully," & Chr(13) _
                             & CStr(txtReceivedQTY.Properties.MaxValue - txtReceivedQTY.Value) & " Assets still in this order.", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)

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
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


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


    Private Sub ZTLocation_TreeBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZTLocation.TreeBtnClick
        Try
            Dim objBALLocation As New BALLocation
            ZTLocation.DataSource = objBALLocation.GetComboLocations(New attLocation)
            ZTLocation.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub txtReceivedQTY_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtReceivedQTY.EditValueChanged
        Dim SerialNumber_Row As DataRow
        'add rows to the grid.
        If txtReceivedQTY.Value > 0 Then
            For index As Integer = 0 To txtReceivedQTY.Value - 1 - SerialNumber_DT.Rows.Count
                SerialNumber_Row = SerialNumber_DT.NewRow
                SerialNumber_Row("#") = SerialNumber_DT.Rows.Count + 1
                SerialNumber_Row("SerialNumber") = String.Empty
                SerialNumber_DT.Rows.Add(SerialNumber_Row)
            Next
        End If
        'remove rows from the grid.
        If SerialNumber_DT.Rows.Count > txtReceivedQTY.Value Then
            For index As Integer = SerialNumber_DT.Rows.Count - 1 To txtReceivedQTY.Value Step -1
                SerialNumber_DT.Rows.RemoveAt(index)
            Next
        End If
    End Sub

    Private Sub txtReceivedQTY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtReceivedQTY.Validating
        If txtReceivedQTY.Value > txtReceivedQTY.Properties.MaxValue Then
            txtReceivedQTY.Value = txtReceivedQTY.Properties.MaxValue
        End If
    End Sub

    Private Sub grvSerial_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles grvSerial.InvalidRowException
        e.ErrorText = "Serial Number is repeated!, "
        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError
    End Sub
    Private Sub grdSerial_EditorKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSerial.EditorKeyDown
        If e.KeyCode = Keys.Enter Then
            If grvSerial.FocusedColumn.FieldName = "SerialNumber" Then
                If grvSerial.ValidateEditor() Then
                    grvSerial.MoveNext()
                Else
                    e.Handled = True
                End If
            End If
        End If

    End Sub

    Private Sub grvSerial_ValidateRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles grvSerial.ValidateRow
        Try
            objattAssetDetails = New attAssetDetails
            objattAssetDetails.SerailNo = grvSerial.GetRowCellValue(e.RowHandle, "SerialNumber")
            If Not String.IsNullOrEmpty(objattAssetDetails.SerailNo) Then
                Dim dt As DataTable = objBALAssetDetails.GetAstIDBySerialNum(objattAssetDetails)
                If dt.Rows.Count > 0 Then
                    grvSerial.SetColumnError(grvSerial.Columns("SerialNumber"), "Serial Number already exists")
                    e.Valid = False
                Else

                    Dim focusedRowHandel As Integer = grvSerial.LocateByValue(0, grvSerial.Columns("SerialNumber"), grvSerial.GetRowCellValue(e.RowHandle, "SerialNumber"))
                    If focusedRowHandel <> e.RowHandle Then
                        grvSerial.SetColumnError(grvSerial.Columns("SerialNumber"), "Serial Number is repeated")
                        e.Valid = False
                    End If

                    focusedRowHandel = grvSerial.LocateByValue(e.RowHandle + 1, grvSerial.Columns("SerialNumber"), grvSerial.GetRowCellValue(e.RowHandle, "SerialNumber"))
                    If grvSerial.IsValidRowHandle(focusedRowHandel) Then
                        grvSerial.SetColumnError(grvSerial.Columns("SerialNumber"), "Serial Number is repeated")
                        e.Valid = False
                    End If
                End If
            Else
                grvSerial.SetColumnError(grvSerial.Columns("SerialNumber"), "Serial Number can't be empty")
                e.Valid = False
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
End Class