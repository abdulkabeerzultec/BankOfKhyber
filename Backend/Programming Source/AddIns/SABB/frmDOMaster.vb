Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors.Controls
Imports System.Windows.Forms

Public Class frmDOMaster

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

    Dim RitmItemCode As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit

#End Region

    Private Sub frmPOMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG
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
                    txtAmount.Text = CDbl(dt.Rows(0)("Amount")).ToString(NumericFormat)
                    txtMode.Text = dt.Rows(0)("ModeDelivery").ToString()
                    txtPTerms.Text = dt.Rows(0)("Payterm").ToString()
                    txtRemarks.Text = dt.Rows(0)("Remarks").ToString()
                    txtAppr.Text = dt.Rows(0)("Approvedby").ToString()
                    txtUser.Text = dt.Rows(0)("PreparedBy").ToString()
                    txtRef.Text = dt.Rows(0)("ReferenceNo").ToString()
                    txtDiscount.Text = CDbl(dt.Rows(0)("Discount")).ToString(NumericFormat)
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
                        grdView.OptionsBehavior.Editable = True
                        grd.EmbeddedNavigator.Buttons.Append.Visible = True
                        grd.EmbeddedNavigator.Buttons.Edit.Visible = True
                        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = True
                        grd.EmbeddedNavigator.Buttons.Remove.Visible = True
                        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = True
                    ElseIf dt.Rows(0)("POStatus").ToString = "3" Then
                        txtStatus.Text = "PartialReceived"
                        grdView.OptionsBehavior.Editable = False
                        grd.EmbeddedNavigator.Buttons.Append.Visible = False
                        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
                        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
                        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
                        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
                    ElseIf dt.Rows(0)("POStatus").ToString = "4" Then
                        txtStatus.Text = "Received"
                        grdView.OptionsBehavior.Editable = False
                        grd.EmbeddedNavigator.Buttons.Append.Visible = False
                        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
                        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
                        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
                        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
                    End If

                    txtAddCharges.Text = CDbl(dt.Rows(0)("AddCharges")).ToString(NumericFormat)
                End If
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub
#Region "GridMinpulating"
    Private Sub Row_Delete_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs)
        If e.Button.ButtonType = DevExpress.XtraEditors.NavigatorButtonType.Remove Then
            UpdateSubTotalAmount()
        End If
    End Sub

    Private Sub grdView_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grdView.CellValueChanged
        If e.Column.FieldName = "POItmBaseCost" Or e.Column.FieldName = "POItmQty" Or e.Column.FieldName = "AddCharges" Then
            If grdView.GetRowCellValue(e.RowHandle, "POItmBaseCost").ToString <> "" And grdView.GetRowCellValue(e.RowHandle, "POItmQty").ToString <> "" Then
                Dim lTotal As Double = grdView.GetRowCellValue(e.RowHandle, "POItmBaseCost") * grdView.GetRowCellValue(e.RowHandle, "POItmQty")
                grdView.SetRowCellValue(e.RowHandle, "TOTALCOST", lTotal)
            Else
                grdView.SetRowCellValue(e.RowHandle, "TOTALCOST", 0)
            End If
            grdView.UpdateCurrentRow()
        End If
    End Sub

    Private Sub grdView_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles grdView.RowUpdated
        UpdateSubTotalAmount()
    End Sub


    Private Sub grdView_InitNewRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles grdView.InitNewRow
        SetGridRowCellValue(grdView, e.RowHandle, "UnitID", "1")
        SetGridRowCellValue(grdView, e.RowHandle, "POItmBaseCost", "1")
        SetGridRowCellValue(grdView, e.RowHandle, "AddCharges", "0")
        SetGridRowCellValue(grdView, e.RowHandle, "POItmQty", "1")
        'SetGridRowCellValue(grdView,e.RowHandle, "POItmID", )
        SetGridRowCellValue(grdView, e.RowHandle, "ISNEWITEM", "true")
    End Sub

    Private Sub grd_EditorKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.EditorKeyDown
        If e.KeyCode = Keys.Enter Then
            If grdView.FocusedColumn.FieldName = "POItmBaseCost" Then
                grdView.FocusedColumn = grdView.Columns("POItmQty")
                grdView.ShowEditor()
                e.Handled = True
            ElseIf grdView.FocusedColumn.FieldName = "POItmQty" Then
                grdView.FocusedColumn = grdView.Columns("UnitID")
                grdView.ShowEditor()
                e.Handled = True
            ElseIf grdView.FocusedColumn.FieldName = "UnitID" Then
                grdView.PostEditor()
                grdView.UpdateCurrentRow()
                grdView.AddNewRow()
                e.Handled = False
            End If
        End If

    End Sub

    Private Sub grdView_ValidateRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles grdView.ValidateRow
        grdView.ClearColumnErrors()
        e.Valid = True
        'Dim Barcode As Object = grdView.GetRowCellValue(e.RowHandle, "ItemCode")
        'If Barcode Is DBNull.Value OrElse String.IsNullOrEmpty(Barcode) Then
        '    Try
        '        grdView.DeleteRow(e.RowHandle)
        '        Return
        '    Catch ex As Exception
        '        GenericExceptionHandler(ex, WhoCalledMe)
        '    End Try
        'End If
        If GetGridRowCellValue(grdView, e.RowHandle, "ItemCode").ToString = "" Then
            'grdView.SetColumnError(GetGridColumn(grdView, "ItemCode"), "Please enter a value")
            grdView.DeleteRow(e.RowHandle)
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

    Private Sub grv_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles grdView.InitNewRow
        Dim View As ColumnView = grd.FocusedView
        Dim column As GridColumn = View.Columns("ItemCode")

        If Not column Is Nothing Then
            View.FocusedRowHandle = e.RowHandle
            View.FocusedColumn = column
        End If
    End Sub

    Private Sub grdView_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles grdView.InvalidRowException
        'Suppress displaying the error message box
        e.ExceptionMode = ExceptionMode.NoAction
    End Sub

    Private Sub RitmItemCode_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            Dim FocRow As Integer = grdView.FocusedRowHandle

            Dim obj As Object = CType(sender, DevExpress.XtraEditors.GridLookUpEdit).EditValue
            If obj IsNot DBNull.Value Then
                If LoadGridItemInfo(obj, FocRow) Then
                    grdView.PostEditor()
                    grdView.UpdateCurrentRow()
                    grdView.FocusedColumn = grdView.Columns(4)
                    grdView.ShowEditor()
                End If
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub RitmItemCode_CloseUp(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.CloseUpEventArgs)
        Dim FocRow As Integer = grdView.FocusedRowHandle
        Dim obj As Object = e.Value
        If obj IsNot DBNull.Value Then
            If LoadGridItemInfo(obj, FocRow) Then
                grdView.PostEditor()
                grdView.UpdateCurrentRow()
                grdView.FocusedColumn = grdView.Columns(4)
                grdView.ShowEditor()
            End If
            e.AcceptValue = True
        End If
    End Sub

    Private Function LoadGridItemInfo(ByVal obj As Object, ByVal FocRow As Integer) As Boolean
        Try
            grdView.SetColumnError(grdView.Columns("itemcode"), Nothing)
            'Check if Assets exists in the Grid.
            Dim grdViewhandle As Long = grdView.LocateByValue(0, grdView.Columns("itemcode"), obj)
            If grdViewhandle <> DevExpress.XtraGrid.GridControl.InvalidRowHandle Then
                grdView.SetRowCellValue(FocRow, "itemcode", Nothing)
                grdView.SetRowCellValue(FocRow, "POItmDesc", String.Empty)
                grdView.SetRowCellValue(FocRow, "UnitID", "1")
                grdView.SetRowCellValue(FocRow, "POItmBaseCost", "1")
                grdView.SetRowCellValue(FocRow, "AddCharges", "0")
                grdView.SetRowCellValue(FocRow, "POItmQty", "1")
                grdView.SetRowCellValue(FocRow, "ISNEWITEM", "true")
                grdView.SetColumnError(grdView.Columns("ItemCode"), "Item code already exists!")
                Return False
            Else
                Dim Rowhandle As Long = RitmItemCode.View.LocateByValue(0, RitmItemCode.View.Columns("itemcode"), obj)
                If Rowhandle <> DevExpress.XtraGrid.GridControl.InvalidRowHandle Then
                    grdView.SetRowCellValue(FocRow, "ItemCode", RitmItemCode.View.GetRowCellValue(Rowhandle, "itemcode"))
                    grdView.SetRowCellValue(FocRow, "POItmDesc", RitmItemCode.View.GetRowCellValue(Rowhandle, "AstDesc"))
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub FormatItemGrid()

        Dim RitmUnit As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
        Dim RitmUnitView As New DevExpress.XtraGrid.Views.Grid.GridView
        RitmUnitView.OptionsView.ShowIndicator = False

        grdView.Columns(0).Caption = "DO Item ID"
        grdView.Columns(0).Width = 75
        grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(0).OptionsColumn.AllowEdit = False
        grdView.Columns(0).Visible = False


        grdView.Columns(1).Visible = False

        grdView.Columns(2).Caption = "Item Code"
        grdView.Columns(2).Width = 85
        grdView.Columns(2).ColumnEdit = RitmItemCode
        RitmItemCode.DataSource = objBALAssets.GetAllData_GetCombo(New attItems)
        RitmItemCode.DisplayMember = GetGridColumnName("AstDesc") 'AstDesc
        RitmItemCode.ValueMember = GetGridColumnName("itemcode")
        RitmItemCode.NullText = ""
        RitmItemCode.View.OptionsView.ShowIndicator = False
        RitmItemCode.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        RitmItemCode.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never
        RitmItemCode.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.StartsWith
        RitmItemCode.View.OptionsView.ShowAutoFilterRow = True
        RitmItemCode.View.OptionsView.ShowIndicator = False
        RitmItemCode.View.Columns(0).Caption = "Item Code"
        RitmItemCode.View.Columns(1).Caption = "Item Description"


        RemoveHandler RitmItemCode.CloseUp, AddressOf RitmItemCode_CloseUp
        AddHandler RitmItemCode.CloseUp, AddressOf RitmItemCode_CloseUp
        RemoveHandler RitmItemCode.KeyDown, AddressOf RitmItemCode_KeyDown
        AddHandler RitmItemCode.KeyDown, AddressOf RitmItemCode_KeyDown

        AddHandler grd.EmbeddedNavigator.ButtonClick, AddressOf Row_Delete_ButtonClick


        grdView.Columns(3).Caption = "Item Desc"
        grdView.Columns(3).OptionsColumn.AllowEdit = False

        grdView.Columns(4).Caption = "Item Cost"
        grdView.Columns(4).Width = 95
        grdView.Columns(4).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        grdView.Columns(4).DisplayFormat.FormatString = "###,###,###,###,###.00"
        'grdView.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdView.Columns(5).Caption = "Add. Charges"
        grdView.Columns(5).Visible = False
        grdView.Columns(5).Width = 80
        grdView.Columns(5).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        grdView.Columns(5).DisplayFormat.FormatString = "###,###,###,###,###.00"
        'grdView.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

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
        ' grdView.Columns(24).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(24).OptionsColumn.AllowEdit = False

        grdView.OptionsView.ShowFooter = True

        grdView.Columns(24).SummaryItem.FieldName = grdView.Columns(24).FieldName
        grdView.Columns(24).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        grdView.Columns(24).SummaryItem.DisplayFormat = "Grand Total= {0:n2}"

        GetGridColumn(grdView, "AddCharges").SummaryItem.FieldName = GetGridColumnName("AddCharges")
        GetGridColumn(grdView, "AddCharges").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        GetGridColumn(grdView, "AddCharges").SummaryItem.DisplayFormat = "Sum= {0:n2}"


        grdView.Columns(25).Visible = False 'IsNewItem Column
        grdView.Columns("POItmDesc").Visible = False
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


            Dim TotalCost As DataColumn = dt.Columns.Add("TOTALCOST", System.Type.GetType("System.Double"), "(POItmBaseCost * POItmQty) + AddCharges")
            'Dim TotalCost As DataColumn = dt.Columns.Add("TOTALCOST", System.Type.GetType("System.Double"))
            Dim IsNewItem As DataColumn = dt.Columns.Add("ISNEWITEM", System.Type.GetType("System.Boolean"))

            grd.DataSource = dt
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub Update_Supp()
        Try
            If Trim(txtStatus.Text) <> "Received" And Trim(txtStatus.Text) <> "PartialReceived" Then

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
                objattPurchaseOrder.POStatus = 2
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
                objattPurchaseOrder.CostID = cmbCostCenter.SelectedValue
                objattPurchaseOrder.Discount = RemoveUnnecessaryChars(txtDiscount.Text)
                objattPurchaseOrder.Date1 = dtDate.Value
                objattPurchaseOrder.POStatus = 2
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
            txtStatus.Text = "Approved"
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
                    objattPODetails.POItmDesc = GetGridRowCellDisplayValue(grdView, i, "POItmDesc").ToString
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
                        If txtStatus.Text <> "PartialReceived" And txtStatus.Text <> "Received" Then
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

            If txtStatus.Text <> "PartialReceived" And txtStatus.Text <> "Received" Then

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
            CmbPO.ValueMember = "POCode"
            CmbPO.DisplayMember = "POCode"
            Dim objattPurchaseOrder As New attPurchaseOrder
            Dim objBALPurchaseOrder As New BALPurchaseOrder
            Dim dt As DataTable = objBALPurchaseOrder.GetAllData_GetCombo(objattPurchaseOrder)
            dt.Columns("Quotation").Caption = "DO Number"
            CmbPO.DataSource = dt
            CmbPO.OpenLOV()
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

    Private Sub cmbCostCenter_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCostCenter.LovBtnClick
        Try
            Dim objBALCost As New BALCostCenter
            cmbCostCenter.ValueMember = "CostID"
            cmbCostCenter.DisplayMember = "CostName"
            cmbCostCenter.DataSource = objBALCost.GetAllData_GetCombo(New attCostCenter)
            cmbCostCenter.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub UpdateSubTotalAmount()
        Dim Amount As Double = CType(grd.DataSource, DataTable).Compute("Sum(TOTALCOST)", "")
        txtAmount.Text = Amount.ToString("N2")
    End Sub

End Class
