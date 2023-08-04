Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports System.Windows.Forms
Imports System.IO
Imports DevExpress.XtraReports.UI
Imports ZulAssetsBL.ZulAssetBAL

<System.ComponentModel.DesignTimeVisible(False)> _
Public Class ctlPurchaseOrder
    Inherits ZulLib.ctlDataEditing

    Private objPO As New PurchaseOrderBLL
    Private objPurchaseOrderItems As New PurchaseOrderItemsBLL
    Private POCode As Int64
    'Private objItemSKU As New ItemSKUBLL
    'Private objItem As New ItemBLL
    'Private objCurrency As New CurrencyBLL
    'Private objWarehouse As New WarehouseBLL
    'Private objDocType As New MartBLL.DocumentNumberBLL

    Private RitmItemCode As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Private RitmQTY As New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Private RitmPrice As New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Private RitmPackageTypeGUID As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Private RitmDiscount As New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Private RitmSuppRef As New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Private _OrderStatus As POStatus = POStatus.NewPO

    Private Enum POStatus
        NewPO
        Prepared
        Approved
        Canceled
        Received
        PartialReceived
        Closed
    End Enum

    Private Property OrderStatus() As POStatus
        Get
            Return _OrderStatus
        End Get
        Set(ByVal value As POStatus)
            _OrderStatus = value
            RefershStatusCaption(value.ToString)
        End Set
    End Property


    Private Sub grdItems_FormatGrid(ByVal Status As POStatus)
        Dim Editable As Boolean = False
        If Status = POStatus.Prepared Then
            Editable = True
        Else
            Editable = False
        End If

        grvItems.OptionsBehavior.Editable = Editable
        grvItems.Columns("GUID").Visible = False
        grvItems.Columns("Barcode").Visible = False
        If grvItems.Columns("EngName") IsNot Nothing Then
            grvItems.Columns("EngName").OptionsColumn.ReadOnly = True
        End If
        If grvItems.Columns("Name") IsNot Nothing Then
            grvItems.Columns("Name").OptionsColumn.ReadOnly = True
        End If

        grvItems.Columns("PackSize").OptionsColumn.ReadOnly = True

        grvItems.Columns("SellingPrice").OptionsColumn.ReadOnly = True
        grvItems.Columns("Number").Visible = False

        'Formating ItemPackage
        grvItems.Columns("ItemPackage").OptionsColumn.ReadOnly = True
        RitmPackageTypeGUID.View.OptionsView.ShowIndicator = False
        grvItems.Columns("ItemPackage").ColumnEdit = RitmPackageTypeGUID
        grvItems.Columns("ItemPackage").OptionsColumn.AllowEdit = True
        'Dim objPackageType As New MartBLL.ItemPackageTypeBLL
        'RitmPackageTypeGUID.DataSource = objPackageType.GetListData()
        'RitmPackageTypeGUID.DisplayMember = objPackageType.ListDisplayMember
        'RitmPackageTypeGUID.ValueMember = objPackageType.ListValueMember
        'RitmPackageTypeGUID.NullText = String.Empty
        'RitmPackageTypeGUID.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        'RitmPackageTypeGUID.AutoComplete = False
        'RitmPackageTypeGUID.View.OptionsView.ShowAutoFilterRow = True

        grvItems.Columns("Total").OptionsColumn.ReadOnly = True
        grvItems.Columns("Total").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        grvItems.Columns("Total").DisplayFormat.FormatString = NumericFormat

        grvItems.Columns("Price").ColumnEdit = RitmPrice
        grvItems.Columns("Price").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        grvItems.Columns("Price").DisplayFormat.FormatString = NumericFormat
        RitmPrice.EditMask = NumericFormat
        RitmPrice.MinValue = 0
        RitmPrice.MaxValue = Int32.MaxValue
        RemoveHandler RitmPrice.EditValueChanged, AddressOf Price_EditValueChanged
        AddHandler RitmPrice.EditValueChanged, AddressOf Price_EditValueChanged

        grvItems.Columns("QTY").ColumnEdit = RitmQTY
        grvItems.Columns("QTY").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        grvItems.Columns("QTY").DisplayFormat.FormatString = NumericFormat
        RitmQTY.MaxValue = Int32.MaxValue
        RitmQTY.MinValue = 0.0001
        RitmQTY.EditMask = NumericFormat
        RemoveHandler RitmQTY.EditValueChanged, AddressOf QTY_EditValueChanged
        AddHandler RitmQTY.EditValueChanged, AddressOf QTY_EditValueChanged

        grvItems.Columns("Discount").ColumnEdit = RitmDiscount
        grvItems.Columns("Discount").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        grvItems.Columns("Discount").DisplayFormat.FormatString = NumericFormat
        RitmDiscount.EditMask = NumericFormat
        RitmDiscount.MaxValue = Int32.MaxValue
        RitmDiscount.MinValue = 0
        RemoveHandler RitmItemCode.CloseUp, AddressOf cmbSKU_CloseUp
        AddHandler RitmItemCode.CloseUp, AddressOf cmbSKU_CloseUp
        RemoveHandler RitmItemCode.KeyDown, AddressOf cmbSKU_KeyDown
        AddHandler RitmItemCode.KeyDown, AddressOf cmbSKU_KeyDown

        AddHandler grdItems.EmbeddedNavigator.ButtonClick, AddressOf Row_Delete_ButtonClick

        grvItems.Columns("ItemCode").ColumnEdit = RitmItemCode
        grvItems.Columns("ItemCode").OptionsColumn.AllowEdit = True



        RemoveHandler RitmDiscount.EditValueChanged, AddressOf Discount_EditValueChanged
        AddHandler RitmDiscount.EditValueChanged, AddressOf Discount_EditValueChanged


        'RitmItemCode.DataSource = objItemSKU.GetStockableListData
        'RitmItemCode.DisplayMember = objItemSKU.ListDisplayMember
        'RitmItemCode.PopulateViewColumns()
        RitmItemCode.NullText = String.Empty
        'RitmItemCode.ValueMember = objItemSKU.ListValueMember
        RitmItemCode.View.Columns("GUID").Visible = False
        RitmItemCode.View.Columns("Name").Visible = False
        RitmItemCode.View.Columns("SellingPrice").Visible = False
        RitmItemCode.View.OptionsView.ShowIndicator = False
        RitmItemCode.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        RitmItemCode.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never
        RitmItemCode.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.StartsWith


        'grvItems.Columns("Total").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, My.Resources.AppInterface.GrandTotal & "{0:N2}")
        'grvItems.Columns("QTY").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, My.Resources.AppInterface.SumQTY & "{0:N2}")
        'grvItems.Columns("Discount").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Sum, My.Resources.AppInterface.SumDiscount & "{0:N2}")
        'grvItems.OptionsView.ShowFooter = True

        grdItems.UseEmbeddedNavigator = True
    End Sub


    Private Sub RefershStatusCaption(ByVal InvoiceStatus As String)
        Dim StatusCaption As String = My.Resources.AppInterface.ResourceManager.GetString(InvoiceStatus, My.Resources.AppInterface.Culture)
        lblStatus.Text = StatusCaption
    End Sub


    Private Sub ctlPurchaseOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ShowProgressDialog()
        Try
            'LoadContolSettings(Me)
            NewData()
            BusnissLayerObject = objPO
            cmbSupplier.btnAddDataVisible = False
            cmbCustodian.btnAddDataVisible = False

            btnPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            btnPreview.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            LayoutTabbedGroup.SelectedTabPageIndex = 0
            ShowBarPrint = True
            PopulateCurrency()
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, "")
        Finally
            CloseProgressDialog()
        End Try
    End Sub

    Private Sub ctlPurchaseOrder_OnDeleteData() Handles Me.OnDeleteData
        Try
            If OrderStatus <> POStatus.Prepared Then
                ZulLib.Messages.ErrorMessage(My.Resources.Strings.OrderPosted, UserName)
                Exit Sub
            End If
            Dim msg As String = objPurchaseOrderItems.DeleteByPOCode(POCode)
            If String.IsNullOrEmpty(msg) Then
                msg = objPO.DeleteByPOCode(POCode)
                If String.IsNullOrEmpty(msg) Then
                    ActionResult = 0
                Else
                    ZulLib.Messages.ErrorMessage(msg, UserName, WhoCalledMe)
                    ActionResult = -1
                End If
            Else
                ZulLib.Messages.ErrorMessage(msg, UserName, WhoCalledMe)
                ActionResult = -1
            End If
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, UserName, WhoCalledMe)
            ActionResult = -1
        End Try
    End Sub

    Private Sub ctlPurchaseOrder_OnLoadData() Handles Me.OnLoadData
        Try
            Dim Msg As String = objPO.Edit(POCode)
            If String.IsNullOrEmpty(Msg) Then


                If Not objPO.Attributes.IsNull("SuppID") Then
                    cmbSupplier.SelectedValue = objPO.Attributes.SuppID
                End If

                If Not objPO.Attributes.IsNull("Requestedby") Then
                    cmbCustodian.SelectedValue = objPO.Attributes.Requestedby
                End If

                txtReferenceNo.Text = objPO.Attributes.ReferenceNo
                txtModeofDel.Text = objPO.Attributes.Quotation
                txtMode.Text = objPO.Attributes.ModeDelivery
                txtPayment.Text = objPO.Attributes.Payterm
                txtPurchaseOrder.Text = objPO.Attributes.Quotation
                txtPurchaseOrder.Tag = String.Empty
                dtOrder.Text = objPO.Attributes.PODate
                txtQuotation.Text = objPO.Attributes.Quotation
                txtRemarks.Text = objPO.Attributes.Remarks

                txtAddCharges.Value = objPO.Attributes.AddCharges.ToString(NumericFormat)
                txtSubTotal.Value = objPO.Attributes.Amount.ToString(NumericFormat)
                txtTotalDiscount.Value = objPO.Attributes.Discount.ToString(NumericFormat)
                OrderStatus = DirectCast([Enum].Parse(GetType(POStatus), objPO.Attributes.POStatus), POStatus)
                grdItems_OnLoadData(OrderStatus) 'OrderStatus = "Prepared"
                'UpdateStatusbarInfo(objPO.Attributes.CreatedBy, objPO.Attributes.CreationDate, objPO.Attributes.LastEditBY, objPO.Attributes.LastEditDate, lblCreated, lblModified)
                ActionResult = 0
            Else
                ZulLib.Messages.ErrorMessage(Msg, UserName, WhoCalledMe)
                ActionResult = -1
            End If
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, UserName, WhoCalledMe)
            ActionResult = -1
        End Try
    End Sub

    Private Sub ctlPurchaseOrder_OnNewData() Handles Me.OnNewData
        LayoutgrdItems.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ActionResult = 0
        dtOrder.DateTime = Now.Date
        OrderStatus = POStatus.Approved
        grdItems_OnLoadData(OrderStatus) ' grdItems_OnLoadData(False)
        txtPurchaseOrder.Focus()
        GenerateDocNumber()
    End Sub

    Private Sub ctlPurchaseOrder_OnSaveData() Handles Me.OnSaveData
        If OrderStatus <> POStatus.Prepared And OrderStatus <> POStatus.NewPO Then
            ZulLib.Messages.ErrorMessage(My.Resources.Strings.OrderPosted, UserName)
            ActionResult = -1
            Exit Sub
        End If

        'Only Prepared PO can be saved, and if it's new then we need to save it as Prepared
        OrderStatus = POStatus.Prepared

        If txtTotalDiscount.Value > txtSubTotal.Value Then
            errProv.SetError(txtTotalDiscount, My.Resources.Strings.TotalDiscountMoreThanTotalAmount)
            txtTotalDiscount.Focus()
            ActionResult = -1
            Exit Sub
        End If

        If RecordStatus = TRecordStates.NewRecord Then
            objPO.NewRecord()
            POCode = objPO.Attributes.POCode
        Else 'Record status is ModifiedRecord
            'Because we are updating the Total Amount of PO, then we need to load on save otherwise update error will come
            objPO.Edit(POCode)
        End If

        Dim DocSerial As String = txtPurchaseOrder.Tag
        'If objDocType.GetDocumentCodeNumber("Purchase Order", DocSerial) = txtPurchaseOrder.Text Then
        '    objDocType.UpdateSerialNumber(DocSerial, "Purchase Order")
        'End If

        objPO.Attributes.Quotation = txtPurchaseOrder.Text
        objPO.Attributes.ModeDelivery = txtModeofDel.Text
        objPO.Attributes.ReferenceNo = txtReferenceNo.Text
        objPO.Attributes.PODate = dtOrder.Text
        objPO.Attributes.Quotation = txtQuotation.Text
        objPO.Attributes.ModeDelivery = txtMode.Text
        objPO.Attributes.Payterm = txtPayment.Text
        objPO.Attributes.Remarks = txtRemarks.Text
        objPO.Attributes.POStatus = OrderStatus.ToString
        objPO.Attributes.Requestedby = cmbCustodian.SelectedValue

        objPO.Attributes.AddCharges = txtAddCharges.Value
        objPO.Attributes.Amount = txtSubTotal.Value
        objPO.Attributes.Discount = txtTotalDiscount.Value

        If Not IsNullOrEmptyValue(cmbSupplier.SelectedValue) Then
            objPO.Attributes.SuppID = cmbSupplier.SelectedValue
        Else
            objPO.Attributes("SuppID") = DBNull.Value
        End If

        If Not IsNullOrEmptyValue(cmbCost.EditValue) Then
            objPO.Attributes.CostID = cmbCost.EditValue
        Else
            objPO.Attributes("CostID") = DBNull.Value
        End If

        ' UpdateStatusbarInfo(objPO.Attributes.CreatedBy, objPO.Attributes.CreationDate, objPO.Attributes.LastEditBY, objPO.Attributes.LastEditDate, lblCreated, lblModified)

        Try
            Dim Msg As String = objPO.Save()
            If String.IsNullOrEmpty(Msg) Then
                OrderStatus = POStatus.Prepared
                grdItems_OnLoadData(OrderStatus)
                LayoutgrdItems.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                ActionResult = 0
            Else
                ZulLib.Messages.ErrorMessage(Msg, UserName, WhoCalledMe)
                ActionResult = -1
            End If
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, UserName, WhoCalledMe)
            ActionResult = -1
        End Try
    End Sub

    Private Sub cmbSupplier_OnbtnDownClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupplier.OnbtnDownClicked
        'Dim objSupplier As New SupplierBLL
        'cmbSupplier.ValueMember = objSupplier.ListValueMember
        'cmbSupplier.DisplayMember = objSupplier.ListDisplayMember
        'cmbSupplier.DataSource = objSupplier.GetListData()
    End Sub

    Private Sub cmbReceivLoc_OnbtnDownClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCustodian.OnbtnDownClicked
        'Dim objWarehouse As New WarehouseBLL
        'cmbWarehouse.ValueMember = objWarehouse.ListValueMember
        'cmbWarehouse.DisplayMember = objWarehouse.ListDisplayMember
        'cmbWarehouse.DataSource = objWarehouse.GetListData()
    End Sub

    Private Sub cmbSKU_CloseUp(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.CloseUpEventArgs)
        Dim FocRow As Integer = grvItems.FocusedRowHandle
        Dim obj As Object = e.Value
        If obj IsNot DBNull.Value And TypeOf obj Is Guid Then
            If LoadGridItemInfo(obj, FocRow) Then
                SaveGridItemInfo(FocRow)
            End If
            e.AcceptValue = True
        End If
    End Sub

    Private Sub cmbSKU_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            Dim FocRow As Integer = grvItems.FocusedRowHandle
            Dim obj As Object = CType(sender, DevExpress.XtraEditors.GridLookUpEdit).EditValue
            If obj IsNot DBNull.Value Then
                If LoadGridItemInfo(obj, FocRow) Then
                    SaveGridItemInfo(FocRow)
                End If

                e.Handled = True
            End If
        End If
    End Sub

    Private Sub SaveGridItemInfo(ByVal pRowHandler As Integer)
        Try
            Dim msg As String = String.Empty
            If grvItems.GetRowCellValue(pRowHandler, "ItemCode") Is DBNull.Value OrElse grvItems.GetRowCellValue(pRowHandler, "ItemCode") = Guid.Empty Then
                msg = My.Resources.Strings.ItemsNotSelected
                ZulLib.Messages.ErrorMessage(msg, UserName, WhoCalledMe)
                Exit Sub
            End If

            Dim lRowItemID As Integer = grvItems.GetRowCellValue(pRowHandler, "POItmID")


            msg = objPurchaseOrderItems.EditByID(lRowItemID)
            If Not String.IsNullOrEmpty(msg) Then 'If Record not found
                objPurchaseOrderItems.NewRecord()
            End If

            'Save items to database.
            objPurchaseOrderItems.Attributes.POCode = POCode
            objPurchaseOrderItems.Attributes.ItemCode = grvItems.GetRowCellValue(pRowHandler, "ItemCode")
            objPurchaseOrderItems.Attributes.POItmBaseCost = grvItems.GetRowCellValue(pRowHandler, "Price")
            objPurchaseOrderItems.Attributes.POItmQty = grvItems.GetRowCellValue(pRowHandler, "QTY")
            objPurchaseOrderItems.Attributes.AddCharges = grvItems.GetRowCellValue(pRowHandler, "AddCharges")

            msg = objPurchaseOrderItems.Save()
            If String.IsNullOrEmpty(msg) Then
                grvItems.UpdateCurrentRow()
                UpdateSubTotalAmount()
            Else
                ZulLib.Messages.ErrorMessage(msg, UserName, WhoCalledMe)
            End If
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, UserName, WhoCalledMe)
        End Try
    End Sub

    Private Function LoadGridItemInfo(ByVal obj As Object, ByVal FocRow As Integer) As Boolean
        Try
            Dim ObjItems As New ItemsBLL
            Dim dt As DataTable = ObjItems.GetItemDetails(obj)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    grvItems.SetRowCellValue(FocRow, "ItemCode", dt.Rows(0)("ItemCode"))
                    grvItems.SetRowCellValue(FocRow, "AstDesc", dt.Rows(0)("ItemDescription"))
                    grvItems.SetRowCellValue(FocRow, "Price", 0)
                    grvItems.SetRowCellValue(FocRow, "Total", 0)
                    'grvItems.SetRowCellValue(FocRow, "ItemPackage", objItemSKU.Attributes.PackageTypeGUID.ToString)
                    'grvItems.SetRowCellValue(FocRow, "PiecesQty", objItemSKU.Attributes.PiecesQty)
                    grvItems.SetRowCellValue(FocRow, "Discount", 0.0)
                    grvItems.SetRowCellValue(FocRow, "QTY", 1)
                    'grvItems.SetRowCellValue(FocRow, "CustodianID", Nothing)
                    ' grvItems.SetRowCellValue(FocRow, "AssetStatusID", Nothing)
                    ' grvItems.SetRowCellValue(FocRow, "SerialNumber", 0)
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub Row_Delete_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs)
        If e.Button.ButtonType = DevExpress.XtraEditors.NavigatorButtonType.Remove Then
            objPurchaseOrderItems.DeleteByRowGUID(grvItems.GetRowCellValue(grvItems.FocusedRowHandle, "GUID"))
            UpdateSubTotalAmount()
        End If
    End Sub

    Private Sub grdItems_OnLoadData(ByVal status As POStatus)
        Dim dtPOItems As DataTable = objPurchaseOrderItems.GetDataByPOCode(POCode)
        If dtPOItems IsNot Nothing Then
            grvItems.Columns.Clear()

            grdItems.DataSource = dtPOItems
            grdItems_FormatGrid(status)
            LayoutgrdItems.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub



    Private Sub ctlPurchaseOrder_OnPrintClicked() Handles Me.OnPrintClicked
     
    End Sub

    Private Sub ctlPurchaseOrder_OnPreviewClicked() Handles Me.OnPreviewClicked
   
    End Sub



    Public Sub PopulateCurrency()
        Dim objBALCustodian As New BALCustodian
        cmbCustodian.ValueMember = "ID"
        cmbCustodian.DisplayMember = "ID"
        cmbCustodian.DataSource = objBALCustodian.GetAllData_GetCombo()
    End Sub

    Private Sub grdViewItems_ValidateRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles grvItems.ValidateRow
        e.Valid = True
        Dim ItemItemCode As Object = grvItems.GetRowCellValue(e.RowHandle, "ItemCode")
        If ItemItemCode Is DBNull.Value OrElse ItemItemCode = Guid.Empty Then
            e.Valid = True
            grvItems.DeleteRow(e.RowHandle)
            Return
        End If

        Dim Price As Object = grvItems.GetRowCellValue(e.RowHandle, "Price")
        If Price Is DBNull.Value OrElse Price < 0 Then
            grvItems.SetColumnError(grvItems.Columns("Price"), My.Resources.Strings.PriceEqualOrGreaterThanZero)
            e.Valid = False
        End If

        Dim Qty As Object = grvItems.GetRowCellValue(e.RowHandle, "QTY")
        If Qty Is DBNull.Value OrElse Qty <= 0 Then
            grvItems.SetColumnError(grvItems.Columns("QTY"), My.Resources.Strings.QTYCreaterThanZero)
            e.Valid = False
        End If

        Dim Discount As Object = grvItems.GetRowCellValue(e.RowHandle, "Discount")
        If Discount Is DBNull.Value OrElse Discount < 0 Then
            grvItems.SetColumnError(grvItems.Columns("Discount"), My.Resources.Strings.DiscountEqualOrGreaterThanZero)
            e.Valid = False
        End If

        Dim Total As Object = grvItems.GetRowCellValue(e.RowHandle, "Total")
        If Total Is DBNull.Value OrElse Total < 0 Then
            grvItems.SetColumnError(grvItems.Columns("Total"), My.Resources.Strings.TotalEqualOrCreaterThanZero)
            e.Valid = False
        End If
    End Sub

    Private Sub grdViewItemsItems_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles grvItems.InvalidRowException
        'Suppress displaying the error message box
        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction
    End Sub


    Private Sub grdView_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles grvItems.InitNewRow
        Try
            grvItems.SetRowCellValue(e.RowHandle, "GUID", Guid.NewGuid)
            grvItems.SetRowCellValue(e.RowHandle, "QTY", 1)
            grvItems.SetRowCellValue(e.RowHandle, "Price", 0)
            grvItems.SetRowCellValue(e.RowHandle, "Total", 0)


            Dim View As ColumnView = grdItems.FocusedView
            ' obtaining the column bound to the ItemCode field
            Dim column As GridColumn = View.Columns("ItemCode")
            If Not column Is Nothing Then
                View.FocusedRowHandle = e.RowHandle
                View.FocusedColumn = column
            End If


        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, UserName, WhoCalledMe)
        End Try
    End Sub


    Private Sub grdItems_EditorKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdItems.EditorKeyDown
        If e.KeyCode = Keys.Enter Then
            If grvItems.FocusedColumn.FieldName = "QTY" Then
                grvItems.FocusedColumn = grvItems.Columns("Price")
                grvItems.ShowEditor()
                e.Handled = True
            ElseIf grvItems.FocusedColumn.FieldName = "Price" Then
                grvItems.FocusedColumn = grvItems.Columns("Discount")
                grvItems.ShowEditor()
                e.Handled = True
            ElseIf grvItems.FocusedColumn.FieldName = "Discount" Then
                grvItems.FocusedColumn = grvItems.Columns("SupplierRef")
                grvItems.ShowEditor()
                e.Handled = True
            ElseIf grvItems.FocusedColumn.FieldName = "SupplierRef" Then
                grvItems.PostEditor()
                grvItems.UpdateCurrentRow()
                grvItems.AddNewRow()
                e.Handled = False
            End If

        End If
    End Sub

    Private Sub cmbSKU_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        Try
            If Asc(e.KeyChar) = Keys.Enter Then
                e.Handled = True
            End If
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, UserName, WhoCalledMe)
        End Try
    End Sub
    Private Sub cmbReceivLoc_SelectTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCustodian.SelectTextChanged
        'If cmbWarehouse.SelectedValue <> Guid.Empty Then
        '    cmbCurrency.EditValue = objWarehouse.GetWarehouseCurrencyGUID(cmbWarehouse.SelectedValue)
        'End If
    End Sub
   
    Private Sub txtPurchaseOrder_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtPurchaseOrder.ButtonClick
        Try
            GenerateDocNumber()
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, "")
        End Try
    End Sub
    Public Sub GenerateDocNumber()
        'Try
        '    Dim DocSerial As String = ""
        '    txtPurchaseOrder.Text = objDocType.GetDocumentCodeNumber("Purchase Order", DocSerial)
        '    txtPurchaseOrder.Tag = DocSerial
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub QTY_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FocRow As Integer = grvItems.FocusedRowHandle
        Dim objQTY As Object = CType(sender, DevExpress.XtraEditors.SpinEdit).EditValue
        GridSpinEditValueChanged(FocRow, grvItems.GetRowCellValue(FocRow, "Discount"), grvItems.GetRowCellValue(FocRow, "Price"), objQTY)
    End Sub
    Private Sub Price_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FocRow As Integer = grvItems.FocusedRowHandle
        Dim objPrice As Object = CType(sender, DevExpress.XtraEditors.SpinEdit).EditValue
        GridSpinEditValueChanged(FocRow, grvItems.GetRowCellValue(FocRow, "Discount"), objPrice, grvItems.GetRowCellValue(FocRow, "QTY"))
    End Sub
    Private Sub Discount_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FocRow As Integer = grvItems.FocusedRowHandle
        Dim objDiscount As Object = CType(sender, DevExpress.XtraEditors.SpinEdit).EditValue
        GridSpinEditValueChanged(FocRow, objDiscount, grvItems.GetRowCellValue(FocRow, "Price"), grvItems.GetRowCellValue(FocRow, "QTY"))
    End Sub

    Private Sub GridSpinEditValueChanged(ByVal FocRow As Integer, ByVal objDiscount As Object, ByVal objPrice As Object, ByVal objQTY As Object)
        Try
            Dim Discount As Double = 0
            If objDiscount IsNot DBNull.Value Then Double.TryParse(objDiscount, Discount)
            Dim Price As Double = 0
            If objPrice IsNot DBNull.Value Then Double.TryParse(objPrice, Price)
            Dim QTY As Double = 0
            If objQTY IsNot DBNull.Value Then Double.TryParse(objQTY, QTY)

            Dim lTotal As Double = (Price * QTY) - Discount

            grvItems.SetRowCellValue(FocRow, "Total", lTotal.ToString(NumericFormat))
            Dim msg As String = objPurchaseOrderItems.Edit(grvItems.GetRowCellValue(FocRow, "GUID"))
            If String.IsNullOrEmpty(msg) Then
                objPurchaseOrderItems.Attributes.POItmBaseCost = Price
                objPurchaseOrderItems.Attributes.AddCharges = Discount
                objPurchaseOrderItems.Attributes.POItmQty = QTY
                msg = objPurchaseOrderItems.Save()
                If String.IsNullOrEmpty(msg) Then
                    UpdateSubTotalAmount()
                End If
            End If

        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, UserName, WhoCalledMe)
        End Try
    End Sub

    Private Sub UpdateSubTotalAmount()
        If RecordGUID <> Nothing AndAlso RecordGUID <> Guid.Empty Then
            objPO.UpdateTotalAmount(POCode)
            txtSubTotal.Value = objPO.GetPOTotal(POCode).ToString(NumericFormat)
        End If
    End Sub
    Private Sub txtSubTotal_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSubTotal.EditValueChanged, txtTotalDiscount.EditValueChanged, txtAddCharges.EditValueChanged, txtFreightCharge.EditValueChanged
        txtTotal.Value = (txtSubTotal.Value + txtAddCharges.Value + txtFreightCharge.Value - txtTotalDiscount.Value).ToString(NumericFormat)
    End Sub
End Class
