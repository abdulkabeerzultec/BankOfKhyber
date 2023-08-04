Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors.Controls

Public Class frmPOMaster

#Region " My Decleration"
    Dim objattAssets As attItems
    Dim objBALAssets As New BALItems

    Dim objBALUnits As New BALUnits

    Dim objattAssetDetails As attAssetDetails
    Dim objBALAssetDetails As New BALAssetDetails
    Dim objattSupplier As attSupplier
    Dim objBALSupplier As New BALSupplier
    Dim objattPurchaseOrder As attPurchaseOrder
    Dim BALPurchaseOrder As New BALPurchaseOrder

    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    Dim objattPODetails As attPODetails
    Dim objBALPODetails As New BALPODetails
    Dim objattCustodian As attCustodian
    Dim objBALCustodian As New BALCustodian
    Dim isEdit As Boolean = False
    Dim EditItemID As String
    Dim objBALOrgHier As New BALOrgHier

#End Region

    Private Sub frmPOMaster_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmPOMaster = Nothing
    End Sub

    Private Sub frmPOMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        Me.WindowState = FormWindowState.Maximized
        Try
            valProvMain.SetValidationRule(CmbPO.TextBox, valRulenotEmpty)
            valProvMain.SetValidationRule(cmbSupp.TextBox, valRulenotEmpty)
            valProvMain.SetValidationRule(txtAmount, valRulenotEmpty)

            txtAddCharges.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
            txtAddCharges.Properties.DisplayFormat.FormatString = "###,###,###,###,###.00"
            txtAddCharges.Properties.Mask.EditMask = "\d{1,8}"

            txtDiscount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
            txtDiscount.Properties.DisplayFormat.FormatString = "###,###,###,###,###.00"
            txtDiscount.Properties.Mask.EditMask = "\d{1,8}"


            CmbPO.SelectedText = CInt(BALPurchaseOrder.GetNextPKey_PurchaseOrder())


            txtUser.Text = AppConfig.LoginName

            Get_Items(CmbPO.SelectedText)
            FormatItemGrid()

            dtDate.CustomFormat = AppConfig.MaindateFormat
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub


#Region "Method"

    Private Sub Get_PO_Open(ByVal _id As String)
        Try
            Dim ds As New DataTable
            objattPurchaseOrder = New attPurchaseOrder
            objattPurchaseOrder.POStatus = 0
            objattPurchaseOrder.PKeyCode = _id
            ds = BALPurchaseOrder.GetAll_PurchaseOrder(objattPurchaseOrder)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Dim dt As DataTable = ds
                    txtQuot.Text = dt.Rows(0)("Quotation").ToString()
                    txtAmount.Text = dt.Rows(0)("Amount").ToString()
                    txtMode.Text = dt.Rows(0)("ModeDelivery").ToString()
                    txtPTerms.Text = dt.Rows(0)("Payterm").ToString()
                    txtRemarks.Text = dt.Rows(0)("Remarks").ToString()
                    txtAppr.Text = dt.Rows(0)("Approvedby").ToString()
                    txtUser.Text = dt.Rows(0)("PreparedBy").ToString()
                    txtRef.Text = dt.Rows(0)("ReferenceNo").ToString()
                    txtDiscount.Text = dt.Rows(0)("Discount").ToString()
                    txtTerms.Text = dt.Rows(0)("TermnCon").ToString() 'CustodianName CostName
                    cmbCustodian.FindRow(Convert.ToString(dt.Rows(0)("Requestedby").ToString()), Convert.ToString(dt.Rows(0)("CustodianName").ToString()))
                    'Cool
                    cmbCostCenter.Text = objBALOrgHier.HierName(dt.Rows(0)("CostID").ToString())
                    ' cmbCostCenter.FindRow(Convert.ToString(dt.Rows(0)("CostID").ToString()), Convert.ToString(dt.Rows(0)("CostName").ToString()))
                    cmbSupp.FindRow(Convert.ToString(dt.Rows(0)("SuppID").ToString()), Convert.ToString(dt.Rows(0)("SuppName").ToString()))

                    If dt.Rows(0)("POStatus").ToString = "1" Then
                        txtStatus.Text = "Open"
                        grdView.OptionsBehavior.Editable = True
                        grd.EmbeddedNavigator.Buttons.Append.Visible = True
                        grd.EmbeddedNavigator.Buttons.Edit.Visible = True
                        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = True
                        grd.EmbeddedNavigator.Buttons.Remove.Visible = True
                        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = True
                    ElseIf dt.Rows(0)("POStatus").ToString = "2" Then
                        txtStatus.Text = "Approved"
                        grdView.OptionsBehavior.Editable = False
                        grd.EmbeddedNavigator.Buttons.Append.Visible = False
                        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
                        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
                        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
                        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
                    ElseIf dt.Rows(0)("POStatus").ToString = "3" Then
                        txtStatus.Text = "Transfered"
                        grdView.OptionsBehavior.Editable = False
                        grd.EmbeddedNavigator.Buttons.Append.Visible = False
                        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
                        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
                        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
                        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
                    End If

                    txtAddCharges.Text = dt.Rows(0)("AddCharges").ToString()
                End If
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub
#Region "GridMinpulating"


    Private Sub grdView_InitNewRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles grdView.InitNewRow
        SetGridRowCellValue(grdView, e.RowHandle, "UnitID", "1")
        SetGridRowCellValue(grdView, e.RowHandle, "POItmBaseCost", "1")
        SetGridRowCellValue(grdView, e.RowHandle, "AddCharges", "0")
        SetGridRowCellValue(grdView, e.RowHandle, "POItmQty", "1")
        'SetGridRowCellValue(grdView,e.RowHandle, "POItmID", )
        SetGridRowCellValue(grdView, e.RowHandle, "ISNEWITEM", "true")
    End Sub

    Private Sub grdView_ValidateRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles grdView.ValidateRow
        grdView.ClearColumnErrors()
        If GetGridRowCellValue(grdView, e.RowHandle, "ItemCode").ToString = "" Then
            grdView.SetColumnError(GetGridColumn(grdView, "ItemCode"), "Please enter a value")
            e.Valid = False
        ElseIf GetGridRowCellValue(grdView, e.RowHandle, "POItmBaseCost").ToString < 1 Then
            grdView.SetColumnError(GetGridColumn(grdView, "POItmBaseCost"), "Value should be bigger than 0")
            e.Valid = False
        ElseIf GetGridRowCellValue(grdView, e.RowHandle, "POItmQty").ToString < 1 Then
            grdView.SetColumnError(GetGridColumn(grdView, "POItmQty"), "Value should be bigger than 0")
            e.Valid = False
        ElseIf GetGridRowCellValue(grdView, e.RowHandle, "UnitID").ToString = "" Then
            grdView.SetColumnError(GetGridColumn(grdView, "UnitID"), "Please enter a value")
            e.Valid = False
        Else
            e.Valid = True
        End If
    End Sub

    Private Sub grdView_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles grdView.InvalidRowException
        'Suppress displaying the error message box
        e.ExceptionMode = ExceptionMode.NoAction
    End Sub



    Private Sub FormatItemGrid()
        Dim RitmItemCode As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
        RitmItemCode.View.OptionsView.ShowIndicator = False

        Dim RitmUnit As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
        Dim RitmUnitView As New DevExpress.XtraGrid.Views.Grid.GridView
        RitmUnitView.OptionsView.ShowIndicator = False

        grdView.Columns(0).Caption = "PO Item ID"
        grdView.Columns(0).Width = 75
        grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(0).OptionsColumn.AllowEdit = False


        grdView.Columns(1).Visible = False

        grdView.Columns(2).Caption = "Item Code"
        grdView.Columns(2).Width = 170
        grdView.Columns(2).ColumnEdit = RitmItemCode
        RitmItemCode.DataSource = objBALAssets.GetAllData_GetCombo(New attItems)
        RitmItemCode.DisplayMember = GetGridColumnName("AstDesc")
        RitmItemCode.ValueMember = GetGridColumnName("itemcode")
        RitmItemCode.NullText = ""

        grdView.Columns(3).Visible = False

        grdView.Columns(4).Caption = "Item Cost"
        grdView.Columns(4).Width = 95
        grdView.Columns(4).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        grdView.Columns(4).DisplayFormat.FormatString = "###,###,###,###,###.00"
        grdView.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdView.Columns(5).Caption = "Add. Charges"
        grdView.Columns(5).Width = 80
        grdView.Columns(5).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        grdView.Columns(5).DisplayFormat.FormatString = "###,###,###,###,###.00"
        grdView.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdView.Columns(6).Caption = "Item Quantity"
        grdView.Columns(6).Width = 95
        grdView.Columns(6).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        grdView.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdView.Columns(7).Visible = False
        grdView.Columns(8).Visible = False
        grdView.Columns(9).Visible = False

        grdView.Columns(10).Visible = False
        grdView.Columns(11).Visible = False
        grdView.Columns(12).Visible = False
        grdView.Columns(13).Visible = False
        grdView.Columns(14).Visible = False
        grdView.Columns(15).Visible = False
        grdView.Columns(16).Visible = False
        grdView.Columns(17).Visible = False
        grdView.Columns(18).Visible = False
        grdView.Columns(19).Visible = False
        grdView.Columns(20).Visible = False

        grdView.Columns(21).Width = 100
        grdView.Columns(21).ColumnEdit = RitmUnit
        grdView.Columns(21).Caption = "Unit"
        RitmUnit.DataSource = objBALUnits.GetAll_Units(New attUnits)
        RitmUnit.DisplayMember = GetGridColumnName("UnitDesc")
        RitmUnit.ValueMember = GetGridColumnName("UnitID")
        RitmUnit.NullText = ""

        grdView.Columns(22).Visible = False
        grdView.Columns(23).Visible = False

        grdView.Columns(24).Caption = "Total Cost"
        grdView.Columns(24).Width = 100
        grdView.Columns(24).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        grdView.Columns(24).DisplayFormat.FormatString = "###,###,###,###,###.00"
        grdView.Columns(24).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(24).OptionsColumn.AllowEdit = False

        grdView.OptionsView.ShowFooter = True

        grdView.Columns(24).SummaryItem.FieldName = grdView.Columns(24).FieldName
        grdView.Columns(24).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        grdView.Columns(24).SummaryItem.DisplayFormat = "Grand Total= {0:n0}"

        GetGridColumn(grdView, "AddCharges").SummaryItem.FieldName = GetGridColumnName("AddCharges")
        GetGridColumn(grdView, "AddCharges").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        GetGridColumn(grdView, "AddCharges").SummaryItem.DisplayFormat = "Sum= {0:n0}"

        grdView.Columns(25).Visible = False 'IsNewItem Column

        addGridMenu(grd)
    End Sub



#End Region
    Private Sub Get_Items(ByVal id As String)
        Try
            objattPODetails = New attPODetails
            objattPODetails.POCode = id
            Dim dt As DataTable = objBALPODetails.GetAll_PODetails(objattPODetails)
            dt.Columns("ItemCode").AllowDBNull = False
            dt.Columns("UnitID").AllowDBNull = False
            dt.Columns("POItmID").AutoIncrement = True
            dt.Columns("POItmID").AutoIncrementSeed = 1


            ' add virtual column(total cost) to the datatable
            Dim TotalCost As DataColumn = dt.Columns.Add("TOTALCOST", System.Type.GetType("System.Double"), "(POItmBaseCost * POItmQty) + AddCharges")
            Dim IsNewItem As DataColumn = dt.Columns.Add("ISNEWITEM", System.Type.GetType("System.Boolean"))

            grd.DataSource = dt
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub Update_Supp()
        Try
            If Trim(txtStatus.Text) <> "Approved" And Trim(txtStatus.Text) <> "Transfered" Then

                objattPurchaseOrder = New attPurchaseOrder

                objattPurchaseOrder.Amount = RemoveUnnecessaryChars(txtAmount.Text)
                objattPurchaseOrder.Approvedby = RemoveUnnecessaryChars(txtAppr.Text)
                objattPurchaseOrder.ModeDelivery = RemoveUnnecessaryChars(txtMode.Text)
                objattPurchaseOrder.PKeyCode = RemoveUnnecessaryChars(CmbPO.SelectedText)
                objattPurchaseOrder.Preparedby = RemoveUnnecessaryChars(txtUser.Text)
                objattPurchaseOrder.Payterm = RemoveUnnecessaryChars(txtPTerms.Text)
                objattPurchaseOrder.Quotation = RemoveUnnecessaryChars(txtQuot.Text)
                objattPurchaseOrder.Discount = RemoveUnnecessaryChars(txtDiscount.Text)
                objattPurchaseOrder.Remarks = RemoveUnnecessaryChars(txtRemarks.Text)
                objattPurchaseOrder.SuppID = cmbSupp.SelectedValue
                objattPurchaseOrder.AddCharges = RemoveUnnecessaryChars(txtAddCharges.Text)
                objattPurchaseOrder.ReferenceNo = RemoveUnnecessaryChars(txtRef.Text)
                objattPurchaseOrder.Requestedby = cmbCustodian.SelectedValue
                objattPurchaseOrder.TermnCon = RemoveUnnecessaryChars(txtTerms.Text)

                If cmbCostCenter.Text <> "" Then
                    objattPurchaseOrder.CostID = cmbCostCenter.Tag
                End If

                objattPurchaseOrder.Date1 = dtDate.Value
                objattPurchaseOrder.POStatus = 1
                BALPurchaseOrder.Update_PurchaseOrder(objattPurchaseOrder)
            Else
                ZulMessageBox.ShowMe("PONoChange")
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub


    Private Sub AddNew_PO()
        Try
            If CheckID(CmbPO.SelectedText) Then
                objattPurchaseOrder = New attPurchaseOrder
                objattPurchaseOrder.Amount = RemoveUnnecessaryChars(txtAmount.Text)
                objattPurchaseOrder.Approvedby = RemoveUnnecessaryChars(txtAppr.Text)
                objattPurchaseOrder.ModeDelivery = RemoveUnnecessaryChars(txtMode.Text)
                objattPurchaseOrder.PKeyCode = RemoveUnnecessaryChars(CmbPO.SelectedText)
                objattPurchaseOrder.Preparedby = RemoveUnnecessaryChars(txtUser.Text)
                objattPurchaseOrder.Payterm = RemoveUnnecessaryChars(txtPTerms.Text)
                objattPurchaseOrder.Quotation = RemoveUnnecessaryChars(txtQuot.Text)
                objattPurchaseOrder.Remarks = RemoveUnnecessaryChars(txtRemarks.Text)
                objattPurchaseOrder.SuppID = cmbSupp.SelectedValue
                objattPurchaseOrder.AddCharges = RemoveUnnecessaryChars(txtAddCharges.Text)
                objattPurchaseOrder.ReferenceNo = RemoveUnnecessaryChars(txtRef.Text)
                objattPurchaseOrder.Requestedby = cmbCustodian.SelectedValue
                objattPurchaseOrder.TermnCon = RemoveUnnecessaryChars(txtTerms.Text)
                If cmbCostCenter.Text <> "" Then
                    objattPurchaseOrder.CostID = cmbCostCenter.Tag
                End If
                objattPurchaseOrder.Discount = RemoveUnnecessaryChars(txtDiscount.Text)
                objattPurchaseOrder.Date1 = dtDate.Value
                objattPurchaseOrder.POStatus = 1
                BALPurchaseOrder.Insert_PurchaseOrder(objattPurchaseOrder)
            Else
                MessageBox.Show("Purchase ID already exists, please try again")
                CmbPO.SelectedIndex = -1
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub
    Private Function CheckID(ByVal _Id As String) As Boolean
        Try
            Dim ds As New DataTable
            objattPurchaseOrder = New attPurchaseOrder
            objattPurchaseOrder.PKeyCode = _Id
            ds = BALPurchaseOrder.GetAll_PurchaseOrder(objattPurchaseOrder)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Function

    Private Function CheckItem(ByVal _Id As String) As Boolean

        Try
            Dim ds As New DataTable
            Dim objattPODetails1 As attPODetails
            objattPODetails1 = New attPODetails
            objattPODetails1.PKeyCode = _Id
            ds = objBALPODetails.GetAll_PODetails(objattPODetails1)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Function


#End Region

    Private Sub CmbPO_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbPO.SelectTextChanged
        Try
            If CheckID(CmbPO.SelectedText) Then
                btnNew_Click(sender, e)
            Else
                Get_PO_Open(CmbPO.SelectedText)
                Get_Items(CmbPO.SelectedText)
                isEdit = True
                btnDelete.Visible = True
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            txtQuot.Text = ""
            txtAmount.Text = ""
            txtStatus.Text = ""
            txtDiscount.Text = "0"
            txtMode.Text = ""
            txtPTerms.Text = ""
            txtRemarks.Text = ""
            txtStatus.Text = "Open"
            txtUser.Text = ""
            txtRef.Text = ""
            cmbCustodian.Text = ""
            txtTerms.Text = ""
            txtAppr.Text = ""
            txtDiscount.Text = ""

            cmbCustodian.SelectedIndex = -1
            cmbCostCenter.Text = ""
            cmbSupp.SelectedIndex = -1
            cmbCustodian.SelectedIndex = -1

            CmbPO.SelectedText = CInt(BALPurchaseOrder.GetNextPKey_PurchaseOrder())

            isEdit = False
            btnDelete.Visible = False

            grdView.OptionsBehavior.Editable = True
            grd.EmbeddedNavigator.Buttons.Append.Visible = True
            grd.EmbeddedNavigator.Buttons.Edit.Visible = True
            grd.EmbeddedNavigator.Buttons.EndEdit.Visible = True
            grd.EmbeddedNavigator.Buttons.Remove.Visible = True
            grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = True
            Get_Items(CmbPO.SelectedText)
            txtQuot.Focus()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub add_Details()
        Try
            objattPODetails = New attPODetails
            For i As Integer = 0 To grdView.RowCount - 1
                If CmbPO.SelectedText <> "" Then
                    objattPODetails.ItemCode = GetGridRowCellValue(grdView, i, "ItemCode").ToString
                    objattPODetails.POItmDesc = GetGridRowCellDisplayValue(grdView, i, "ItemCode").ToString
                    objattPODetails.unit = GetGridRowCellValue(grdView, i, "UnitID").ToString
                    objattPODetails.POItmBaseCost = GetGridRowCellValue(grdView, i, "POItmBaseCost").ToString
                    objattPODetails.AddCharges = GetGridRowCellValue(grdView, i, "AddCharges").ToString
                    objattPODetails.POItmQty = GetGridRowCellValue(grdView, i, "POItmQty").ToString

                    objattPODetails.POCode = CmbPO.SelectedText
                    If GetGridRowCellValue(grdView, i, "ISNEWITEM").ToString = String.Empty Then
                        objattPODetails.PKeyCode = GetGridRowCellValue(grdView, i, "POItmID").ToString
                        objBALPODetails.Update_PODetails(objattPODetails)
                    Else
                        objattPODetails.PKeyCode = objBALPODetails.GetNextPKey_PODetails()
                        objBALPODetails.Insert_PODetails(objattPODetails)
                    End If
                End If
            Next


            Me.Get_Items(CmbPO.SelectedText)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate Then
                Dim rCount As Integer = grdView.RowCount
                If rCount > 0 Then
                    If isEdit Then
                        If txtStatus.Text <> "Approved" And txtStatus.Text <> "Transfered" Then
                            Me.Update_Supp()
                            Me.add_Details()

                            ZulMessageBox.ShowMe("Saved")
                            btnNew_Click(sender, e)
                        Else
                            ZulMessageBox.ShowMe("PONoChange")
                        End If
                    Else
                        Me.AddNew_PO()
                        Me.add_Details()
                        ZulMessageBox.ShowMe("Saved")
                        btnNew_Click(sender, e)
                    End If
                Else
                    ZulMessageBox.ShowMe("AtLeastPOError")
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub


    Private Sub txtAddCharges_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAddCharges.TextChanged
        Try

            If Trim(txtAmount.Text) = "" Then
                txtAmount.Text = "0.0"
            Else

            End If
            If Trim(txtAddCharges.Text) = "" Then
                txtAddCharges.Text = "0.0"
            Else

            End If
            If Trim(txtDiscount.Text) = "" Then
                txtDiscount.Text = "0"
            End If
            Try
                txtTotalCost.Text = Format(CType(txtAmount.Text, Double) + CType(txtAddCharges.Text, Double), "###,###,###,###,###.00")
            Catch ex As Exception
                txtAddCharges.Text = "0.0"
            End Try
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub txtAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmount.TextChanged, txtAmount.LostFocus
        Try

            If Trim(txtAmount.Text) = "" Then
                txtAmount.Text = "0.0"

            Else

            End If
            If Trim(txtAddCharges.Text) = "" Then
                txtAddCharges.Text = "0.0"
            Else

            End If
            If Trim(txtDiscount.Text) = "" Then
                txtDiscount.Text = "0"
            End If
            Try
                txtTotalCost.Text = Format(CType(CType(txtAmount.Text, Long) + CType(txtAddCharges.Text, Long), Double), "###,###,###,###,###.00")
            Catch ex As Exception
                txtAddCharges.Text = "0.0"
            End Try
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub lblUser_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUser.TextChanged, txtAppr.TextChanged
        Try
            If Trim(txtUser.Text) = "" Then
                txtUser.Text = AppConfig.LoginName
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try

            If txtStatus.Text <> "Approved" And txtStatus.Text <> "Transfered" Then

                If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    Dim objPODet As New BALPODetails
                    Dim objatt As New attPODetails
                    objatt.POCode = CmbPO.SelectedText
                    objPODet.Delete_PODetails(objatt)

                    Dim objPO As New BALPurchaseOrder
                    Dim objattPO As New attPurchaseOrder
                    objattPO.PKeyCode = CmbPO.SelectedText
                    objPO.Delete_PurchaseOrder(objattPO)

                    ZulMessageBox.ShowMe("Deleted")
                    btnNew_Click(sender, e)
                End If
            Else
                ZulMessageBox.ShowMe("PONoChange")
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub


    Private Sub btnLOV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLOV.Click
        Dim obj As New ZulHierTree.clsTree

        If AppConfig.DbType = 1 Then
            obj.DBType = 2
        ElseIf AppConfig.DbType = 2 Then
            obj.DBType = 1
        End If

        obj.DBName = AppConfig.DbName
        obj.DBPass = AppConfig.DbPass
        If AppConfig.DbType = 1 Then
            obj.DBServer = AppConfig.DbServer & "," & AppConfig.DBSQLPort
            'obj.DBServer = AppConfig.DbServer
        Else
            obj.DBServer = AppConfig.DbServer
        End If
        obj.DBUName = AppConfig.DbUname
        obj.OpenTree(cmbCostCenter)

    End Sub

    Private Sub cmbCostCenter_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbCostCenter.KeyDown
        If e.KeyData = Keys.Back Or e.KeyData = Keys.Delete Then
            cmbCostCenter.Text = ""
            cmbCostCenter.Tag = ""
        ElseIf e.Alt And e.KeyValue = Keys.Down Then
            btnLOV_Click(sender, e)
        End If
    End Sub

    Private Sub btnLOV_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnLOV.KeyDown
        If e.Alt And e.KeyValue = Keys.Down Then
            btnLOV_Click(sender, e)
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub cmbCust_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCustodian.LovBtnClick
        Try
            Dim objBALCustodian As New BALCustodian
            cmbCustodian.ValueMember = "ID"
            cmbCustodian.DisplayMember = "Name"
            cmbCustodian.DataSource = objBALCustodian.GetAllData_GetCombo()
            cmbCustodian.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub cmbPO_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbPO.LovBtnClick
        Try
            cmbPO.ValueMember = "POCode"
            cmbPO.DisplayMember = "POCode"
            Dim objattPurchaseOrder As New attPurchaseOrder
            Dim objBALPurchaseOrder As New BALPurchaseOrder
            cmbPO.DataSource = objBALPurchaseOrder.GetAllData_GetCombo(objattPurchaseOrder)
            cmbPO.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

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

    Private Sub grdView_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles grdView.RowUpdated
        txtAmount.Text = grdView.Columns(24).SummaryItem.SummaryValue
    End Sub
End Class
