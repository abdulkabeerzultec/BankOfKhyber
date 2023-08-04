Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid
Imports DevExpress.XtraReports.UI

Imports System.Windows.Forms
Imports System.IO
Imports SABBPlugin.StockTransferBLL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports ZulAssetsBL.ZulAssetBAL

<System.ComponentModel.DesignTimeVisible(False)> _
Public Class ctlStockTransfer
    Inherits ZulLib.ctlDataEditing

    Dim objBALUnits As New BALUnits
    Dim ObjItems As New ItemsBLL

    Private objStockTransfer As New StockTransferBLL
    Private objStockTransferItem As New StockTransferItemBLL
    Private objStockTransferItemDetail As New StockTransferItemDetails
    Dim objattPurchaseOrder As New attPurchaseOrder
    Dim objBALPurchaseOrder As New BALPurchaseOrder

    Private ReportType As String
    Public TransferType As StockTransferTypes = StockTransferTypes.DirectReceiving
    Private _TransferStatus As String = "Prepared"
    Private objDocType As New DocumentNumberBLL


    Private Property TransferStatus()
        Get
            Return _TransferStatus
        End Get
        Set(ByVal value)
            _TransferStatus = value.ToString
            RefershStatusCaption(_TransferStatus)
        End Set
    End Property

    Private WithEvents btnPost As New DevExpress.XtraBars.BarButtonItem
    Private WithEvents btnPrintLabels As New DevExpress.XtraBars.BarButtonItem
    Private _deletingGuid As Guid
    Private RitmItemCode As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Private RitmSerial As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Private RitmAssetID As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit

    Private RitmQTY As New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Private RitmCostPrice As New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Private RitmDiscount As New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit

    Private RitmFromLocation As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Private RitmToLocation As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Private RitmCustodian As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Private RitmStatus As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit

    Private Sub ListDataLoad()
        Me.ListDataSource = objStockTransfer.GetListDataByTransferType(TransferType.ToString)
    End Sub

    Private Sub ctlStockTransfer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ShowProgressDialog()
        Try
            If Not String.IsNullOrEmpty(Me.Tag) Then
                If Me.Tag.Equals("DR") Then
                    TransferType = StockTransferTypes.DirectReceiving
                    ReportType = My.Resources.AppInterface.DirectReceiving
                End If

                If Me.Tag.Equals("PR") Then
                    TransferType = StockTransferTypes.DOReceiving
                    ReportType = My.Resources.AppInterface.DOReceiving
                End If

                If Me.Tag.Equals("IR") Then
                    TransferType = StockTransferTypes.ItemReturn
                    ReportType = My.Resources.AppInterface.ItemReturn
                End If


                If Me.Tag.Equals("SI") Then
                    TransferType = StockTransferTypes.StockIssuance
                    ReportType = My.Resources.AppInterface.StockIssuance
                End If
                If Me.Tag.Equals("CR") Then
                    TransferType = StockTransferTypes.CustodianReturn
                    ReportType = My.Resources.AppInterface.CustomerReturn
                End If
            End If

            LoadContolSettings(Me)

            RecordChanged = False
            NewData()
            cmbPO.Enabled = False

            valProvMain.SetValidationRule(txtCode, valRuleGreaterOrEqualZero)

            btnPost.Caption = My.Resources.Strings.btnPostCaption
            btnPost.Glyph = My.Resources.Resources.check
            btnPost.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.F2))

            btnPost.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
            barPrint.AddItem(btnPost)

            btnPrintLabels.Caption = "Print Labels"
            btnPrintLabels.Glyph = My.Resources.Resources.Print

            btnPrintLabels.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
            barPrint.AddItem(btnPrintLabels)

            btnPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            btnPreview.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            PopulateFromWarehouse()
            PopulateToWarehouse()
            PopulateCustodian()
            PopulateAssetsStatus()
            InitInterface(TransferType)
            ListDataLoad()
            NavigationFilter = "TransferType='" & TransferType.ToString & "'"
            BusnissLayerObject = objStockTransfer
            cmbListRep.View.Columns("TransferType").Visible = False
            ShowBarPrint = True
            'Clear All the items from the toolbar.
            barStatus.ItemLinks.Clear()

        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, "")
        Finally
            CloseProgressDialog()
        End Try
    End Sub

    Private Sub PopulateAssetsStatus()
        Dim objBALAssetDetails As New BALAssetDetails
        cmbAssetsStatus.Properties.ValueMember = "ID"
        cmbAssetsStatus.Properties.DisplayMember = "Status"
        If TransferType = StockTransferTypes.ItemReturn Then
            cmbAssetsStatus.Properties.DataSource = objBALAssetDetails.GetAssetStatus(True, False)
        Else
            cmbAssetsStatus.Properties.DataSource = objBALAssetDetails.GetAssetStatus(False, False)
        End If
    End Sub
    Private Sub cmbAssetsStatus_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbAssetsStatus.QueryPopUp
        PopulateAssetsStatus()
    End Sub

    Private Sub cmbCustodian_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCustodian.EditValueChanged
        'Dim bm As BindingManagerBase = cmbCustodian.BindingContext(cmbCustodian.Properties.DataSource)
        'bm.Position = cmbCustodian.Properties.GetIndexByKeyValue(cmbCustodian.EditValue)
    End Sub

    Private Sub cmbCustodian_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbCustodian.QueryPopUp
        PopulateCustodian()
    End Sub
    Private Sub PopulateCustodian()
        Dim objBALCustodian As New BALCustodian
        cmbCustodian.Properties.ValueMember = "ID"
        cmbCustodian.Properties.DisplayMember = "ID"
        cmbCustodian.Properties.DataSource = objBALCustodian.GetAllData_GetCombo()
    End Sub

    Private Sub RefershStatusCaption(ByVal Status As String)
        Dim StatusCaption As String = My.Resources.AppInterface.ResourceManager.GetString(Status, My.Resources.AppInterface.Culture)
        'if status not exist in the Resources then show it as its
        If String.IsNullOrEmpty(StatusCaption) Then
            lblStatus.Text = Status
        Else
            lblStatus.Text = StatusCaption
        End If

        If Status = "Posted" Then
            btnPrintLabels.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            btnPrintLabels.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub

    Private Sub UpdateFormCaption()
        Dim TransferCaption As String = My.Resources.AppInterface.ResourceManager.GetString(TransferType.ToString, My.Resources.AppInterface.Culture)
        'If RecordStatus = TRecordStates.NewRecord Then
        Me.NewFormCaption = String.Format("{0} {1}", My.Resources.AppInterface.Add, TransferCaption)
        'Else
        Me.EditFormCaption = String.Format("{0} {1}", My.Resources.AppInterface.Edit, TransferCaption)
        'End If
    End Sub

    Private Sub InitInterface(ByVal TransType As StockTransferTypes)
        LayoutControl1.AllowCustomizationMenu = False
        TransferType = TransType
        lciPO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        LayoutBatchNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        LayoutInvoiceNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        grvItems.Columns("Total").Width = 100
        grvItems.Columns("QTY").Width = 95
        grvItems.Columns("Discount").Width = 105
        UpdateFormCaption()
        grdItems.EmbeddedNavigator.Buttons.Append.Enabled = True
        Select Case TransType
            Case StockTransferTypes.DirectReceiving
                cmbFromLocation.Enabled = True
                cmbPO.Enabled = False
                grdItems.UseEmbeddedNavigator = True
                grvItems.Columns("FromLocID").Visible = False
                grvItems.Columns("ToLocID").Visible = True
                'grvItems.Columns("CustodianID").Visible = False
                grvItems.Columns("Price").Visible = True
                grvItems.Columns("Discount").Visible = True
                grvItems.Columns("Total").Visible = True
                grvItems.Columns("AssetSerialNumber").Visible = False
                grvItems.Columns("AstID").Visible = False
                grvItems.Columns("ItemCode").Visible = True
                grvItems.Columns("SerialNumber").Visible = True
                ' lciToWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Case StockTransferTypes.DOReceiving
                'cmbPO.TextBox.Text = String.Empty
                'cmbFromWarehouse.TextBox.Text = String.Empty
                'grdItems.UseEmbeddedNavigator = False
                grdItems.EmbeddedNavigator.Buttons.Append.Enabled = False
                cmbPO.Enabled = True
                cmbFromLocation.Enabled = False
                lciPO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LayoutBatchNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LayoutInvoiceNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                grvItems.Columns("FromLocID").Visible = False
                grvItems.Columns("ToLocID").Visible = True
                ' grvItems.Columns("CustodianID").Visible = False
                grvItems.Columns("Price").Visible = True
                grvItems.Columns("Discount").Visible = True
                grvItems.Columns("Total").Visible = True
                grvItems.Columns("AssetSerialNumber").Visible = False
                grvItems.Columns("AstID").Visible = False
                grvItems.Columns("ItemCode").Visible = True
                grvItems.Columns("SerialNumber").Visible = True

                ' lciToWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Case StockTransferTypes.CustodianReturn
                lciDoc.Text = My.Resources.AppInterface.MRNNum
                lciSupp.Text = My.Resources.AppInterface.FromCustomer
                cmbFromLocation.Enabled = True
                grvItems.Columns("FromLocID").Visible = False
                grvItems.Columns("ToLocID").Visible = True
                'grvItems.Columns("CustodianID").Visible = True
                grvItems.Columns("Price").Visible = False
                grvItems.Columns("Discount").Visible = False
                grvItems.Columns("Total").Visible = False
                lciToWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlAddCharges.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlFreight.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlSubTotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlTotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                grvItems.Columns("AssetSerialNumber").Visible = True
                grvItems.Columns("AstID").Visible = True
                grvItems.Columns("ItemCode").Visible = False
                grvItems.Columns("SerialNumber").Visible = False
                grvItems.Columns("QTY").ColumnEdit.ReadOnly = True
            Case StockTransferTypes.ItemReturn
                lciDoc.Text = My.Resources.AppInterface.MRNNum
                lciSupp.Text = My.Resources.AppInterface.ToSupplier
                lciToWarehouse.Text = My.Resources.AppInterface.ToSupplier
                lciTransferDate.Text = My.Resources.AppInterface.ReturnDate
                ' grvItems.Columns("Discount").Visible = False
                grvItems.Columns("FromLocID").Visible = False
                'grvItems.Columns("ToLocID").OptionsColumn.ReadOnly = True
                grvItems.Columns("ToLocID").Visible = False
                'grvItems.Columns("CustodianID").OptionsColumn.ReadOnly = True
                grvItems.Columns("CustodianID").Visible = False
                grvItems.Columns("Price").Visible = False
                grvItems.Columns("Discount").Visible = False
                grvItems.Columns("Total").Visible = False
                lciToWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutCustodian.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                ' grvItems.Columns("CustodianID").Visible = False
                LayoutControlAddCharges.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlFreight.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlSubTotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlTotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                grvItems.Columns("AssetSerialNumber").Visible = True
                grvItems.Columns("AstID").Visible = True
                grvItems.Columns("ItemCode").Visible = False
                grvItems.Columns("SerialNumber").Visible = False
                grvItems.Columns("QTY").ColumnEdit.ReadOnly = True
            Case StockTransferTypes.StockIssuance
                lciDoc.Text = My.Resources.AppInterface.DocNum
                lciSupp.Text = My.Resources.AppInterface.ToCustodian
                lciTransferDate.Text = My.Resources.AppInterface.IssuanceDate
                grvItems.Columns("FromLocID").Visible = False
                grvItems.Columns("ToLocID").Visible = True
                grvItems.Columns("Price").Visible = False
                grvItems.Columns("Discount").Visible = False
                grvItems.Columns("Total").Visible = False
                lciToWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lciToWarehouse.Text = "To Location"
                LayoutCustodian.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                cmbFromLocation.Properties.Buttons(1).Visible = True
                LayoutControlAddCharges.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlFreight.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlSubTotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlTotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                ' grvItems.Columns("CustodianID").Visible = True
                grvItems.Columns("AssetSerialNumber").Visible = True
                grvItems.Columns("AstID").Visible = True
                grvItems.Columns("ItemCode").Visible = False
                grvItems.Columns("SerialNumber").Visible = False
                grvItems.Columns("QTY").ColumnEdit.ReadOnly = True
        End Select

        'If TypeOf (cmbToWarehouse.EditValue) Is Guid Then
        '    _WarehouseType = objWarehouse.GetWHSType(cmbToWarehouse.EditValue)
        '    If _WarehouseType = WarehouseType.Van Or _WarehouseType = WarehouseType.Warehouse Then
        '        grvItems.Columns("ToLocID").Visible = False
        '    ElseIf _WarehouseType = WarehouseType.LocationWarehouse Then
        '        grvItems.Columns("ToLocID").Visible = True
        '    End If
        'End If

        'If TypeOf (cmbFromWarehouse.EditValue) Is Guid Then
        '    _WarehouseType = objWarehouse.GetWHSType(cmbFromWarehouse.EditValue)
        '    If _WarehouseType = WarehouseType.Van Or _WarehouseType = WarehouseType.Warehouse Then
        '        grvItems.Columns("FromLocID").Visible = False
        '    ElseIf _WarehouseType = WarehouseType.LocationWarehouse Then
        '        grvItems.Columns("FromLocID").Visible = True
        '    End If
        'End If
    End Sub

    Private Sub ShowBtnPost(ByVal Visible As Boolean)
        ShowBarPrint = True
        If Visible Then

            btnPost.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            btnPost.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub

    Private Sub btnPost_Clicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.ItemClick
        grvItems.PostEditor()
        grvItems.UpdateCurrentRow()


        Dim msg As String = My.Resources.Strings.TransferPost
        If TransferType = StockTransferTypes.DirectReceiving Or TransferType = StockTransferTypes.DOReceiving Then
            If Not ValidateSerializedItems() Then
                Exit Sub
            End If
        End If

        If String.IsNullOrEmpty(txtIncidentNum.Text.Trim) Then
            ZulLib.Messages.ErrorMessage("Please specify Incident Number before posting!", UserName)
            txtIncidentNum.Focus()
            Exit Sub
        End If

        If grvItems.RowCount <= 0 Then
            ZulLib.Messages.ErrorMessage(My.Resources.BLLStrings.TransferItem(), UserName)
            grdItems.Focus()
            grvItems.AddNewRow()
            Exit Sub
        End If

        If SaveData() = 0 Then
            If ZulLib.Messages.WarningMessage(msg) = DialogResult.Yes Then
                ShowProgressDialog()
                Try
                    msg = objStockTransfer.Post(RecordGUID)
                    If Not String.IsNullOrEmpty(msg) Then
                        ZulLib.Messages.ErrorMessage(msg, UserName)
                    Else
                        TransferStatus = "Posted"
                        grdItems_OnLoadData(TransferStatus)
                        ShowBtnPost(False)
                        RecordChanged = False
                    End If
                Catch ex As Exception
                    ZulLib.Messages.ErrorMessage(ex.Message, UserName)
                Finally
                    CloseProgressDialog()
                End Try
            End If
        End If

    End Sub

    Public Function ValidateSerializedItems() As Boolean
        Dim ObjDetails As New StockTransferItemDetails
        Dim objBALAssetDetails As New BALAssetDetails
        For row As Integer = 0 To grvItems.RowCount - 1
            If CInt(grvItems.GetRowCellValue(row, "SerialNumber")) <> CInt(grvItems.GetRowCellValue(row, "QTY")) Then
                ZulLib.Messages.ErrorMessage(My.Resources.Strings.EnterSerialNumber, "")
                grvItems.FocusedRowHandle = row
                If grvItems.FocusedColumn.FieldName <> "SerialNumber" Then
                    grvItems.FocusedColumn = grvItems.Columns("SerialNumber")
                Else
                    ShowSerialNumberPopupForm()
                End If
                Return False
            Else
                'Validate all new serial number, it should not be repeated.
                If TransferType = StockTransferTypes.DirectReceiving Or TransferType = StockTransferTypes.DOReceiving Then
                    Dim dtItemDetails As DataTable = ObjDetails.GetDataByStockTransferItemGUID(grvItems.GetRowCellValue(row, "GUID"))
                    For Each ItemDetail As DataRow In dtItemDetails.Rows
                        If ItemDetail("OldSerialNumber").ToString <> ItemDetail("SerialNumber").ToString Then
                            Dim objattAssetDetails As New attAssetDetails
                            objattAssetDetails.SerailNo = ItemDetail("SerialNumber")
                            'objattAssetDetails.ItemCode = Item("ItemCode")
                            Dim dt As DataTable = objBALAssetDetails.GetAstIDBySerialNum(objattAssetDetails)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                ZulLib.Messages.ErrorMessage("Asset already exists for SN", "")
                                grvItems.FocusedRowHandle = row
                                If grvItems.FocusedColumn.FieldName <> "SerialNumber" Then
                                    grvItems.FocusedColumn = grvItems.Columns("SerialNumber")
                                Else
                                    ShowSerialNumberPopupForm()
                                End If
                                Return False
                            End If
                        End If

                    Next
                End If
            End If

        Next
        Return True
    End Function
    Private Sub ctlStockTransfer_OnDeleteData() Handles Me.OnDeleteData
        Try
            If TransferStatus <> "Prepared" Then
                ZulLib.Messages.ErrorMessage(My.Resources.Strings.TransferPosted, UserName)
                Exit Sub
            End If

            Dim msg As String = objStockTransferItem.DeleteByStockTransferGUID(RecordGUID)
            If String.IsNullOrEmpty(msg) Then
                msg = objStockTransfer.DeleteByRowGUID(RecordGUID)
                If String.IsNullOrEmpty(msg) Then
                    ListDataLoad()
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

    Private Sub ctlStockTransfer_OnLoadData() Handles Me.OnLoadData

        Try
            Dim Msg As String = objStockTransfer.Edit(RecordGUID)
            If String.IsNullOrEmpty(Msg) Then
                If Not objStockTransfer.Attributes.IsNotesNull Then
                    txtNotes.Text = objStockTransfer.Attributes.Notes
                End If
                dpTransferDate.Text = objStockTransfer.Attributes.TransDate
                If Not objStockTransfer.Attributes.IsCodeNull Then
                    txtCode.Text = objStockTransfer.Attributes.Code
                    txtCode.Tag = String.Empty
                End If
                TransferStatus = objStockTransfer.Attributes.Status

                'If Not objStockTransfer.Attributes.IsNull("CurrencyGUID") Then
                '    cmbCurrency.EditValue = objStockTransfer.Attributes.CurrencyGUID
                'End If
                txtAddCharges.Value = objStockTransfer.Attributes.AddCharges.ToString(NumericFormat)
                txtSubTotal.Value = objStockTransfer.Attributes.TotalAmount.ToString(NumericFormat)
                txtTotalDiscount.Value = objStockTransfer.Attributes.TotalDiscount.ToString(NumericFormat)
                txtFreightCharge.Value = objStockTransfer.Attributes.FreightCharge.ToString(NumericFormat)

                If Not objStockTransfer.Attributes.IsInvoiceNumberNull Then
                    txtInvNo.Text = objStockTransfer.Attributes.InvoiceNumber
                End If

                If Not objStockTransfer.Attributes.IsBatchIDNull Then
                    txtBatchID.Text = objStockTransfer.Attributes.BatchID
                End If

                If Not objStockTransfer.Attributes.IsAWBNumberNull Then
                    txtIncidentNum.Text = objStockTransfer.Attributes.AWBNumber
                End If

                Select Case TransferType
                    Case StockTransferTypes.DirectReceiving
                        If Not objStockTransfer.Attributes.IsSuppIDNull Then
                            cmbFromLocation.EditValue = objStockTransfer.Attributes.SuppID
                        End If

                        If Not objStockTransfer.Attributes.IsToLocIDNull Then
                            cmbToLocation.EditValue = objStockTransfer.Attributes.ToLocID
                        End If

                        If Not objStockTransfer.Attributes.IsCustodianIDNull Then
                            cmbCustodian.EditValue = objStockTransfer.Attributes.CustodianID
                        End If

                    Case StockTransferTypes.CustodianReturn
                        If Not objStockTransfer.Attributes.IsCustodianIDNull Then
                            cmbFromLocation.EditValue = objStockTransfer.Attributes.CustodianID
                        End If
                        If Not objStockTransfer.Attributes.IsToLocIDNull Then
                            cmbToLocation.EditValue = objStockTransfer.Attributes.ToLocID
                        End If
                    Case StockTransferTypes.DOReceiving
                        If Not objStockTransfer.Attributes.IsPOCodeNull Then
                            cmbPO.SelectedValue = objStockTransfer.Attributes.POCode
                        End If

                        If Not objStockTransfer.Attributes.IsSuppIDNull Then
                            cmbFromLocation.EditValue = objStockTransfer.Attributes.SuppID
                        End If

                        If Not objStockTransfer.Attributes.IsToLocIDNull Then
                            cmbToLocation.EditValue = objStockTransfer.Attributes.ToLocID
                        End If
                        If Not objStockTransfer.Attributes.IsCustodianIDNull Then
                            cmbCustodian.EditValue = objStockTransfer.Attributes.CustodianID
                        End If

                    Case StockTransferTypes.ItemReturn
                        If Not objStockTransfer.Attributes.IsSuppIDNull Then
                            cmbFromLocation.EditValue = objStockTransfer.Attributes.SuppID
                        End If
                        'If Not objStockTransfer.Attributes.IsSuppIDNull Then
                        '    cmbToLocation.EditValue = objStockTransfer.Attributes.SuppID
                        'End If

                    Case StockTransferTypes.StockIssuance
                        If Not objStockTransfer.Attributes.IsToLocIDNull Then
                            cmbToLocation.EditValue = objStockTransfer.Attributes.ToLocID
                        End If
                        If Not objStockTransfer.Attributes.IsCustodianIDNull Then
                            cmbFromLocation.EditValue = objStockTransfer.Attributes.CustodianID
                        End If
                End Select
                ShowBtnPost(TransferStatus = "Prepared")
                grdItems_OnLoadData(TransferStatus)

                InitInterface(TransferType)

                'UpdateStatusbarInfo(objStockTransfer.Attributes.CreatedBy, objStockTransfer.Attributes.CreationDate, objStockTransfer.Attributes.LastEditBY, objStockTransfer.Attributes.LastEditDate, lblCreated, lblModified)
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
    Private Sub ctlStockTransfer_OnNewData() Handles Me.OnNewData
        If grvItems.DataSource IsNot Nothing Then
            grvItems.DataSource.Table.Rows.Clear()
        End If

        ShowBtnPost(False)
        LayoutgrdItems.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        dpTransferDate.DateTime = DateTime.Now.Date
        TransferStatus = "Prepared"
        txtCode.Focus()
        ActionResult = 0
        grdItems_OnLoadData(TransferStatus)
        grvItems.OptionsBehavior.Editable = False
        UpdateFormCaption()
        GenerateDocNumber()
        If TransferType = StockTransferTypes.DirectReceiving Or TransferType = StockTransferTypes.DOReceiving Then
            'Select OAD Store as Custodian
            'Select OAD Store as location
            cmbCustodian.EditValue = "OADStore"
            Dim objloc As New BALLocation
            cmbToLocation.EditValue = objloc.GetLocID("OAD-s")
        End If
        'cmbCurrency.EditValue = objCurrency.GetDefaultCurrencyGUID
    End Sub

    Private Sub grdItems_OnLoadData(ByVal Status As String)
        Dim Editable As Boolean = False
        'if the status is not 
        If Status = "Posted" Then
            Editable = False
        ElseIf Status = "Prepared" Then
            Editable = True
        Else
            Editable = False
        End If

        Dim ldatatable As DataTable = objStockTransfer.GetGRNItemByTransferGUID(RecordGUID)
        If ldatatable IsNot Nothing Then
            AddHandler ldatatable.RowDeleting, New DataRowChangeEventHandler(AddressOf Row_Deleting)
            AddHandler ldatatable.RowDeleted, New DataRowChangeEventHandler(AddressOf Row_Deleted)
            grvItems.Columns.Clear()

            grdItems.DataSource = ldatatable
            grdGRItems_FormatGrid(Editable)
            InitInterface(TransferType)
            LayoutgrdItems.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    Private Sub Row_Deleting(ByVal sender As Object, ByVal e As DataRowChangeEventArgs)
        _deletingGuid = grvItems.GetRowCellValue(grvItems.FocusedRowHandle, "GUID")
    End Sub

    Private Sub Row_Deleted(ByVal sender As Object, ByVal e As DataRowChangeEventArgs)
        Try
            grdItems_OnbtnDeleteClicked(_deletingGuid)
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, UserName, WhoCalledMe)
        End Try
    End Sub

    Private Sub grdItems_OnbtnDeleteClicked(ByVal ParentGuid As System.Guid)
        Try
            If ParentGuid <> Guid.Empty Then
                Dim Msg As String = objStockTransferItem.DeleteByRowGUID(ParentGuid)
                If String.IsNullOrEmpty(Msg) Then
                    ActionResult = 0
                    UpdateSubTotalAmount()
                Else
                    ZulLib.Messages.ErrorMessage(Msg, UserName, WhoCalledMe)
                    ActionResult = -1
                End If
            Else
                ActionResult = -1
            End If
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, UserName, WhoCalledMe)
            ActionResult = -1
        End Try
    End Sub

    Private Sub ctlStockTransfer_OnSaveData() Handles Me.OnSaveData
        If TransferStatus <> "Prepared" Then
            ZulLib.Messages.ErrorMessage(My.Resources.Strings.TransferPosted, UserName)
            ActionResult = -1
            Exit Sub
        End If

        If txtTotalDiscount.Value > txtSubTotal.Value Then
            errProv.SetError(txtTotalDiscount, My.Resources.Strings.TotalDiscountMoreThanTotalAmount)
            txtTotalDiscount.Focus()
            ActionResult = -1
            Exit Sub
        End If


        ''StockMove From/To Warehouses are same.
        'If cmbFromLocation.EditValue = cmbToLocation.EditValue And TransferType <> StockTransferTypes.DOReceiving Then
        '    errProv.SetError(cmbToLocation, My.Resources.Strings.FromToWarehouseSame)
        '    ActionResult = -1
        '    Return
        'End If

        If RecordStatus = TRecordStates.NewRecord Then
            objStockTransfer.NewRecord()
            RecordGUID = objStockTransfer.Attributes.GUID
            objStockTransfer.Attributes.CreationDate = Now.Date
            objStockTransfer.Attributes.CreatedBy = UserGUID
        Else
            objStockTransfer.Edit(RecordGUID)
        End If




        objStockTransfer.Attributes.Notes = txtNotes.Text
        objStockTransfer.Attributes.TransDate = dpTransferDate.Text
        objStockTransfer.Attributes.Code = txtCode.Text
        objStockTransfer.Attributes.InvoiceNumber = txtInvNo.Text
        objStockTransfer.Attributes.BatchID = txtBatchID.Text
        'objStockTransfer.Attributes.CurrencyGUID = cmbCurrency.EditValue
        objStockTransfer.Attributes.AddCharges = txtAddCharges.Value
        objStockTransfer.Attributes.TotalAmount = txtSubTotal.Value
        objStockTransfer.Attributes.TotalDiscount = txtTotalDiscount.Value
        objStockTransfer.Attributes.FreightCharge = txtFreightCharge.Value
        objStockTransfer.Attributes.AWBNumber = txtIncidentNum.Text

        If TransferType = StockTransferTypes.DirectReceiving Or TransferType = StockTransferTypes.DOReceiving Then
            If cmbFromLocation.EditValue IsNot DBNull.Value Then
                objStockTransfer.Attributes.SuppID = cmbFromLocation.EditValue
            End If
            If cmbToLocation.EditValue IsNot DBNull.Value Then
                objStockTransfer.Attributes.ToLocID = cmbToLocation.EditValue
            End If
            If cmbCustodian.EditValue IsNot DBNull.Value Then
                objStockTransfer.Attributes.CustodianID = cmbCustodian.EditValue
            End If

        ElseIf TransferType = StockTransferTypes.ItemReturn Then
            'If cmbFromLocation.EditValue IsNot DBNull.Value Then
            '    objStockTransfer.Attributes.FromLocID = cmbFromLocation.EditValue
            'End If
            If cmbFromLocation.EditValue IsNot DBNull.Value Then
                objStockTransfer.Attributes.SuppID = cmbFromLocation.EditValue
            End If

        ElseIf TransferType = StockTransferTypes.CustodianReturn Then
            'If cmbToLocation.EditValue IsNot DBNull.Value Then
            '    objStockTransfer.Attributes.ToLocID = cmbToLocation.EditValue
            'End If
            If cmbCustodian.EditValue IsNot DBNull.Value Then
                objStockTransfer.Attributes.CustodianID = cmbFromLocation.EditValue
            End If
        ElseIf TransferType = StockTransferTypes.StockIssuance Then
            If cmbToLocation.EditValue IsNot DBNull.Value Then
                objStockTransfer.Attributes.ToLocID = cmbToLocation.EditValue
            End If
            If cmbCustodian.EditValue IsNot DBNull.Value Then
                objStockTransfer.Attributes.CustodianID = cmbFromLocation.EditValue
            End If
        End If

        If TransferType = StockTransferTypes.DOReceiving Then
            If Not IsNullOrEmptyValue(cmbPO.SelectedValue) Then
                objStockTransfer.Attributes.POCode = cmbPO.SelectedValue
            Else
                objStockTransfer.Attributes.SetPOCodeNull()
            End If
        End If


        objStockTransfer.Attributes.Status = TransferStatus
        objStockTransfer.Attributes.TransferType = TransferType.ToString
        objStockTransfer.Attributes.LastEditDate = Now.Date
        objStockTransfer.Attributes.LastEditBY = UserGUID

        'For Admin user after clearing the database there will be no Emp in the database.
        'If AppConfig.EmpGuid IsNot DBNull.Value Then
        '    objStockTransfer.Attributes.EmpGUID = AppConfig.EmpGuid
        'Else
        '    objStockTransfer.Attributes.SetEmpGUIDNull()
        'End If

        'UpdateStatusbarInfo(objStockTransfer.Attributes.CreatedBy, objStockTransfer.Attributes.CreationDate, objStockTransfer.Attributes.LastEditBY, objStockTransfer.Attributes.LastEditDate, lblCreated, lblModified)

        Try
            If Not objStockTransfer.Attributes.IsFromLocIDNull AndAlso Not objStockTransfer.Attributes.IsToLocIDNull AndAlso objStockTransfer.Attributes.FromLocID = objStockTransfer.Attributes.ToLocID Then
                ZulLib.Messages.ErrorMessage(My.Resources.Strings.FromToWarehouseSame, WhoCalledMe)
                Exit Sub
            End If
            Dim Msg As String = objStockTransfer.Save()
            ListDataLoad()
            If String.IsNullOrEmpty(Msg) Then
                Dim DocSerial As String = ""
                If objDocType.GetDocumentCodeNumber(TransferType.ToString, DocSerial) = txtCode.Text Then
                    objDocType.UpdateSerialNumber(DocSerial, TransferType.ToString)
                End If
            Else
                Throw New Exception(Msg)
            End If

            UpdateFormCaption()

            If TransferType = StockTransferTypes.DOReceiving And RecordStatus = TRecordStates.NewRecord Then
                AddPOItemsToTransfer()
                grdItems_OnLoadData(TransferStatus)
            End If

            grvItems.OptionsBehavior.Editable = True
            LayoutgrdItems.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            ShowBtnPost(True)
            ActionResult = 0
        Catch ex As Exception

            ZulLib.Messages.ErrorMessage(ex.Message, UserName, WhoCalledMe)
            ActionResult = -1
        End Try
    End Sub


    Private Sub AddPOItemsToTransfer()
        'objBALPurchaseOrder
        Dim msg As String = ""
        objattPurchaseOrder.PKeyCode = cmbPO.SelectedValue
        Dim dt As DataTable = objBALPurchaseOrder.GetAll_PurchaseOrder(objattPurchaseOrder)
        'Dim CustodianID As String = ""
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            msg = objStockTransfer.Edit(RecordGUID)
            If String.IsNullOrEmpty(msg) Then
                txtSubTotal.Value = dt.Rows(0)("Amount")
                txtTotalDiscount.Value = dt.Rows(0)("Discount")
                txtAddCharges.Value = dt.Rows(0)("AddCharges")
                'CustodianID = dt.Rows(0)("Requestedby")
                objStockTransfer.Attributes.AddCharges = txtAddCharges.Value
                objStockTransfer.Attributes.TotalAmount = txtSubTotal.Value
                objStockTransfer.Attributes.TotalDiscount = txtTotalDiscount.Value
                objStockTransfer.Attributes.FreightCharge = txtFreightCharge.Value
                msg = objStockTransfer.Save()
                If Not String.IsNullOrEmpty(msg) Then Throw New Exception(msg)
            End If
        End If

        Dim objPOItems As New PurchaseOrderItemsBLL

        Dim dtDetails As DataTable = objPOItems.GetPONotReceivedItems(cmbPO.SelectedValue)
        For Each row As DataRow In dtDetails.Rows
            objStockTransferItem.NewRecord()
            objStockTransferItem.Attributes.GUID = Guid.NewGuid
            objStockTransferItem.Attributes.CreationDate = Now.Date
            objStockTransferItem.Attributes.CreatedBy = UserGUID
            objStockTransferItem.Attributes.StockTransferGUID = RecordGUID
            objStockTransferItem.Attributes.ItemCode = row("ItemCode")
            objStockTransferItem.Attributes.Price = row("POItmBaseCost")
            objStockTransferItem.Attributes.Discount = 0  'row("Discount")
            objStockTransferItem.Attributes.QTY = row("POItmQty")

            objStockTransferItem.Attributes.CustodianID = cmbCustodian.EditValue
            objStockTransferItem.Attributes.ToLocID = cmbToLocation.EditValue
            objStockTransferItem.Attributes.AssetStatusID = 3

            objStockTransferItem.Attributes.SupplierRef = "" 'row("SupplierRef")
            objStockTransferItem.Attributes.Number = row("POItmID") 'objStockTransferItem.GetMAXNumberByGUID(RecordGUID)
            objStockTransferItem.Attributes.Notes = String.Empty
            objStockTransferItem.Attributes.LotNumber = String.Empty
            objStockTransferItem.Attributes.SerialNumber = String.Empty
            objStockTransferItem.Attributes.AstID = ""
            'objStockTransferItem.Attributes.SetFromLocIDNull()
            'objStockTransferItem.Attributes.SetToLocIDNull()
            objStockTransferItem.Save()
        Next

    End Sub

    Private Sub cmbPO_OnbtnDownClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPO.OnbtnDownClicked
        Dim objPO As New PurchaseOrderBLL
        If TransferType = StockTransferTypes.DOReceiving Then
            cmbPO.ValueMember = "POCode"
            cmbPO.DisplayMember = "Quotation"
            Dim dt As New DataTable
            If TransferStatus = "Posted" Then
                dt = objPO.GetPurchseOrderByStatus(3, 4) 'PartialReceived,Received
                dt.Columns("Quotation").Caption = "DO Number"
                cmbPO.DataSource = dt
            ElseIf TransferStatus = "Prepared" Then
                dt = objPO.GetPurchseOrderByStatus(2, 3) 'Approved,PartialReceived
                dt.Columns("Quotation").Caption = "DO Number"
                cmbPO.DataSource = dt
            End If
        End If
    End Sub

    Private Sub cmbPO_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPO.SelectTextChanged
        Try
            If String.IsNullOrEmpty(cmbPO.SelectedText) Or cmbPO.SelectedValue Is Nothing Then
                Exit Sub
            End If

            If TransferStatus = "Posted" Then
                Exit Sub
            End If
            If TransferType = StockTransferTypes.DOReceiving Then
                objattPurchaseOrder.PKeyCode = cmbPO.SelectedValue
                Dim dt As DataTable = objBALPurchaseOrder.GetAll_PurchaseOrder(objattPurchaseOrder)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    txtInvNo.Text = dt.Rows(0)("ModeDelivery")
                    txtBatchID.Text = dt.Rows(0)("ReferenceNo")

                    If RecordStatus = TRecordStates.NewRecord Then
                        cmbFromLocation.EditValue = dt.Rows(0)("SuppID")
                        'If Not lPo.Attributes.IsSupplierGUIDNull Then
                        '    cmbFromLocation.EditValue= lPo.Attributes.SupplierGUID
                        'End If
                        'If Not lPo.Attributes.IsWarehouseGUIDNull Then
                        '    cmbToLocation.EditValue = lPo.Attributes.WarehouseGUID
                        'End If
                    Else
                        'If objStockTransfer.Attributes.IsPOGUIDNull Then
                        '    objStockTransfer.Attributes.POGUID = Guid.Empty
                        'End If

                        If objStockTransfer.Attributes.POCode <> cmbPO.SelectedValue Then
                            cmbFromLocation.EditValue = dt.Rows(0)("SuppID")
                            'Delete all stock transfer items.
                            'If Not lPo.Attributes.IsSupplierGUIDNull Then
                            '    cmbFromLocation.EditValue = lPo.Attributes.SupplierGUID
                            'End If

                            'If Not lPo.Attributes.IsWarehouseGUIDNull Then
                            '    cmbToLocation.EditValue = lPo.Attributes.WarehouseGUID
                            'End If

                            objStockTransfer.Attributes.POCode = cmbPO.SelectedValue
                            'objStockTransfer.Attributes.SuppID = lPo.Attributes.SupplierGUID
                            'objStockTransfer.Attributes.ToLocID = lPo.Attributes.WarehouseGUID
                            Dim msg As String = ""
                            msg = objStockTransfer.Save()

                            If IsNullOrEmptyValue(msg) Then
                                msg = objStockTransferItem.DeleteByStockTransferGUID(RecordGUID)
                                If String.IsNullOrEmpty(msg) Then
                                    AddPOItemsToTransfer()
                                    grdItems_OnLoadData(TransferStatus)
                                End If
                            End If

                        End If
                    End If
                End If


            End If

        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, UserName, WhoCalledMe)
            ActionResult = -1
        End Try
    End Sub

    Private Sub grdGRItems_FormatGrid(ByVal Editable As Boolean)
        grvItems.OptionsBehavior.Editable = Editable
        grvItems.OptionsNavigation.EnterMoveNextColumn = True
        grvItems.OptionsView.ColumnAutoWidth = True
        grvItems.Columns("GUID").Visible = False
        grvItems.Columns("SupplierRef").Visible = False

        If grvItems.Columns("AstDesc") IsNot Nothing Then
            grvItems.Columns("AstDesc").OptionsColumn.ReadOnly = True
        End If

        grvItems.Columns("AstDesc").Visible = False

        grvItems.Columns("Total").OptionsColumn.ReadOnly = True
        grvItems.Columns("Total").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        grvItems.Columns("Total").DisplayFormat.FormatString = NumericFormat
        grvItems.Columns("Total").MaxWidth = 90
        grvItems.Columns("Total").Width = 70
        grvItems.Columns("Price").ColumnEdit = RitmCostPrice
        grvItems.Columns("Price").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        grvItems.Columns("Price").DisplayFormat.FormatString = NumericFormat
        grvItems.Columns("Price").MaxWidth = 90
        grvItems.Columns("Price").Width = 70
        RitmCostPrice.EditMask = NumericFormat
        RitmCostPrice.MinValue = 0
        RitmCostPrice.MaxValue = Int32.MaxValue
        RemoveHandler RitmCostPrice.EditValueChanged, AddressOf Price_EditValueChanged
        AddHandler RitmCostPrice.EditValueChanged, AddressOf Price_EditValueChanged


        grvItems.Columns("QTY").ColumnEdit = RitmQTY
        grvItems.Columns("QTY").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        grvItems.Columns("QTY").DisplayFormat.FormatString = NumericFormat
        grvItems.Columns("QTY").MaxWidth = 70
        RitmQTY.EditMask = NumericFormat
        RitmQTY.MaxValue = Int32.MaxValue
        RitmQTY.MinValue = 0.0001
        RemoveHandler RitmQTY.EditValueChanged, AddressOf QTY_EditValueChanged
        AddHandler RitmQTY.EditValueChanged, AddressOf QTY_EditValueChanged

        grvItems.Columns("Discount").ColumnEdit = RitmDiscount
        grvItems.Columns("Discount").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        grvItems.Columns("Discount").DisplayFormat.FormatString = NumericFormat
        grvItems.Columns("Discount").MaxWidth = 90
        grvItems.Columns("Discount").Width = 70
        RitmDiscount.EditMask = NumericFormat
        RitmDiscount.MaxValue = Int32.MaxValue
        RitmDiscount.MinValue = 0
        RemoveHandler RitmDiscount.EditValueChanged, AddressOf Discount_EditValueChanged
        AddHandler RitmDiscount.EditValueChanged, AddressOf Discount_EditValueChanged


        RemoveHandler RitmItemCode.CloseUp, AddressOf cmbSKU_CloseUp
        AddHandler RitmItemCode.CloseUp, AddressOf cmbSKU_CloseUp
        RemoveHandler RitmItemCode.KeyDown, AddressOf cmbSKU_KeyDown
        AddHandler RitmItemCode.KeyDown, AddressOf cmbSKU_KeyDown

        grvItems.Columns("ItemCode").ColumnEdit = RitmItemCode
        grvItems.Columns("ItemCode").OptionsColumn.AllowEdit = True
        grvItems.Columns("ItemCode").MaxWidth = 200
        grvItems.Columns("ItemCode").MinWidth = 120
        grvItems.Columns("ItemCode").Caption = "Item"

        RitmItemCode.View.OptionsView.ShowIndicator = False
        RitmItemCode.View.OptionsView.ShowAutoFilterRow = True

        If TransferType = StockTransferTypes.DOReceiving Then
            RitmItemCode.DataSource = ObjItems.GetItemList(cmbPO.SelectedValue)
            grvItems.Columns("ItemCode").OptionsColumn.ReadOnly = True
        Else
            RitmItemCode.DataSource = ObjItems.GetItemList("")
            grvItems.Columns("ItemCode").OptionsColumn.ReadOnly = False
        End If
        RitmItemCode.DisplayMember = GetGridColumnName("ItemDescription") 'AstDesc
        RitmItemCode.ValueMember = GetGridColumnName("ItemCode")
        RitmItemCode.NullText = String.Empty

        'RitmItemCode.DisplayMember = objItemSKU.ListDisplayMember
        'RitmItemCode.ValueMember = objItemSKU.ListValueMember
        'RitmItemCode.PopulateViewColumns()

        'RitmItemCode.View.Columns("GUID").Visible = False
        'RitmItemCode.View.Columns("Name").Visible = False
        'RitmItemCode.View.Columns("SellingPrice").Visible = False

        RitmItemCode.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        RitmItemCode.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never
        RitmItemCode.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.StartsWith
        RitmItemCode.ValidateOnEnterKey = True

        grvItems.Columns("ToLocID").ColumnEdit = RitmToLocation
        grvItems.Columns("ToLocID").OptionsColumn.AllowEdit = True

        grvItems.Columns("FromLocID").ColumnEdit = RitmFromLocation
        grvItems.Columns("FromLocID").OptionsColumn.AllowEdit = True



        PopulateFromLocation()
        PopulateToLocation()

        Dim objBALCustodian As New BALCustodian
        RitmCustodian.ValueMember = "ID"
        RitmCustodian.DisplayMember = "ID"
        RitmCustodian.DataSource = objBALCustodian.GetAllData_GetCombo()
        grvItems.Columns("CustodianID").ColumnEdit = RitmCustodian
        grvItems.Columns("CustodianID").OptionsColumn.AllowEdit = True
        RemoveHandler RitmCustodian.EditValueChanged, AddressOf Custodian_EditValueChanged
        AddHandler RitmCustodian.EditValueChanged, AddressOf Custodian_EditValueChanged
        RitmCustodian.NullText = String.Empty


        Dim objBALAssetDetails As New BALAssetDetails
        RitmStatus.ValueMember = "ID"
        RitmStatus.DisplayMember = "Status"
        If TransferType = StockTransferTypes.ItemReturn Then
            RitmStatus.DataSource = objBALAssetDetails.GetAssetStatus(True, False)
        Else
            RitmStatus.DataSource = objBALAssetDetails.GetAssetStatus(False, False)
        End If
        grvItems.Columns("AssetStatusID").ColumnEdit = RitmStatus
        grvItems.Columns("AssetStatusID").OptionsColumn.AllowEdit = True
        RemoveHandler RitmStatus.EditValueChanged, AddressOf AssetStatus_EditValueChanged
        AddHandler RitmStatus.EditValueChanged, AddressOf AssetStatus_EditValueChanged
        RitmStatus.NullText = String.Empty
        grvItems.Columns("AssetStatusID").Width = 80
        grvItems.Columns("AssetStatusID").MaxWidth = 120

        grvItems.Columns("SerialNumber").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grvItems.Columns("SerialNumber").MaxWidth = 110
        grvItems.Columns("SerialNumber").MinWidth = 90
        grvItems.Columns("SerialNumber").Caption = "SN Selected"


        If TransferType = StockTransferBLL.StockTransferTypes.StockIssuance Or TransferType = StockTransferBLL.StockTransferTypes.ItemReturn Then
            grvItems.Columns("AssetSerialNumber").ColumnEdit = RitmSerial
            grvItems.Columns("AssetSerialNumber").OptionsColumn.AllowEdit = True

            grvItems.Columns("AstID").ColumnEdit = RitmAssetID
            grvItems.Columns("AstID").OptionsColumn.AllowEdit = True

            RitmAssetID.View.OptionsView.ShowIndicator = False
            RitmAssetID.View.OptionsView.ShowAutoFilterRow = True


            RitmSerial.View.OptionsView.ShowIndicator = False
            RitmSerial.View.OptionsView.ShowAutoFilterRow = True
            Dim objItems As New ItemsBLL
            If TransferType = StockTransferBLL.StockTransferTypes.StockIssuance Then
                RitmSerial.DataSource = objItems.GetSerialNumberByItemCode()
                RitmAssetID.DataSource = objItems.GetAstIDByItemCode
                'If TransferStatus = "Posted" Then
                '    RitmSerial.DataSource = objItems.GetSerialNumberByItemCode()
                'Else
                '    RitmSerial.DataSource = objItems.GetSerialNumberByItemCodeAndStatus(3) '3 In Stock
                'End If
            ElseIf TransferType = StockTransferBLL.StockTransferTypes.ItemReturn Then
                RitmSerial.DataSource = objItems.GetSerialNumberByItemCode()
                RitmAssetID.DataSource = objItems.GetAstIDByItemCode
                'If TransferStatus = "Posted" Then
                '    RitmSerial.DataSource = objItems.GetSerialNumberByItemCode()
                'Else
                '    RitmSerial.DataSource = objItems.GetItemReturnSerials()
                'End If
            End If

            RitmSerial.DisplayMember = "SerialNumber"
            RitmSerial.ValueMember = "SerialNumber"
            RitmSerial.NullText = String.Empty
            RitmSerial.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
            RitmSerial.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never
            RitmSerial.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.StartsWith
            RitmSerial.ValidateOnEnterKey = True

            RemoveHandler RitmSerial.CloseUp, AddressOf cmbSerial_CloseUp
            AddHandler RitmSerial.CloseUp, AddressOf cmbSerial_CloseUp
            RemoveHandler RitmSerial.KeyDown, AddressOf cmbSerial_KeyDown
            AddHandler RitmSerial.KeyDown, AddressOf cmbSerial_KeyDown



            RitmAssetID.DisplayMember = "AstID"
            RitmAssetID.ValueMember = "AstID"
            RitmAssetID.NullText = String.Empty
            RitmAssetID.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
            RitmAssetID.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never
            RitmAssetID.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.StartsWith
            RitmAssetID.ValidateOnEnterKey = True

            RemoveHandler RitmAssetID.CloseUp, AddressOf cmbAstID_CloseUp
            AddHandler RitmAssetID.CloseUp, AddressOf cmbAstID_CloseUp
            RemoveHandler RitmAssetID.KeyDown, AddressOf cmbAstID_KeyDown
            AddHandler RitmAssetID.KeyDown, AddressOf cmbAstID_KeyDown

        End If
        grvItems.Columns("Number").Visible = False
        grdItems.UseEmbeddedNavigator = True
    End Sub

    Public Function LoadGridInfoBySerial(ByVal obj As Object, ByVal FocRow As Integer, ByVal GetByAstID As Boolean) As Boolean
        Try
            Dim dt As DataTable
            If GetByAstID Then
                dt = ObjItems.GetItemDetailsByAstID(obj)
            Else
                dt = ObjItems.GetItemDetailsBySerial(obj)
            End If

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    grvItems.SetRowCellValue(FocRow, "AssetSerialNumber", dt.Rows(0)("SerailNo"))
                    grvItems.SetRowCellValue(FocRow, "AstID", dt.Rows(0)("AstID"))
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
                    If TransferType = StockTransferTypes.StockIssuance Or TransferType = StockTransferTypes.ItemReturn Or TransferType = StockTransferTypes.CustodianReturn Then
                        Dim DocCode As String = objStockTransferItem.GetSerialNumberPreparedDocCode(grvItems.GetRowCellValue(FocRow, "AssetSerialNumber"), grvItems.GetRowCellValue(FocRow, "GUID"))
                        If Not String.IsNullOrEmpty(DocCode) Then
                            grvItems.SetColumnError(grvItems.Columns("AssetSerialNumber"), String.Format("Serial Number is already exists in prepared document({0})", DocCode))
                            Return False
                        Else
                            grvItems.ClearColumnErrors()
                        End If
                    End If
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


    Private Function LoadGridItemInfo(ByVal obj As Object, ByVal FocRow As Integer) As Boolean
        Try
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

    Private Sub cmbSerial_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
                Dim FocRow As Integer = grvItems.FocusedRowHandle

                Dim obj As Object = CType(sender, DevExpress.XtraEditors.GridLookUpEdit).EditValue
                If obj IsNot DBNull.Value Then
                    If LoadGridInfoBySerial(obj, FocRow, False) Then
                        SaveGridItemInfo(FocRow)
                        If TransferType = StockTransferTypes.StockIssuance Then
                            grvItems.PostEditor()
                            grvItems.UpdateCurrentRow()
                            grvItems.AddNewRow()
                        Else
                            grvItems.FocusedColumn = grvItems.VisibleColumns(1)
                            grvItems.ShowEditor()
                        End If
                    End If
                    e.Handled = True
                End If
            End If
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, "")
        End Try
    End Sub
    Private Sub cmbSerial_CloseUp(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.CloseUpEventArgs)
        Try
            Dim FocRow As Integer = grvItems.FocusedRowHandle
            Dim obj As Object = e.Value
            If obj IsNot DBNull.Value Then
                If LoadGridInfoBySerial(obj, FocRow, False) Then
                    SaveGridItemInfo(FocRow)
                    If TransferType = StockTransferTypes.StockIssuance Then
                        grvItems.PostEditor()
                        grvItems.UpdateCurrentRow()
                        grvItems.AddNewRow()
                    Else
                        grvItems.FocusedColumn = grvItems.VisibleColumns(1)
                        grvItems.ShowEditor()
                    End If
                End If
                e.AcceptValue = True
            End If
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, "")
        End Try
    End Sub

    Private Sub cmbAstID_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
                Dim FocRow As Integer = grvItems.FocusedRowHandle

                Dim obj As Object = CType(sender, DevExpress.XtraEditors.GridLookUpEdit).EditValue
                If obj IsNot DBNull.Value Then
                    If LoadGridInfoBySerial(obj, FocRow, True) Then
                        SaveGridItemInfo(FocRow)
                        If TransferType = StockTransferTypes.StockIssuance Then
                            grvItems.PostEditor()
                            grvItems.UpdateCurrentRow()
                            grvItems.AddNewRow()
                        Else
                            grvItems.FocusedColumn = grvItems.VisibleColumns(1)
                            grvItems.ShowEditor()
                        End If
                    End If
                    e.Handled = True
                End If
            End If
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, "")
        End Try
    End Sub

    Private Sub cmbAstID_CloseUp(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.CloseUpEventArgs)
        Try
            Dim FocRow As Integer = grvItems.FocusedRowHandle
            Dim obj As Object = e.Value
            If obj IsNot DBNull.Value Then
                If LoadGridInfoBySerial(obj, FocRow, True) Then
                    SaveGridItemInfo(FocRow)
                    If TransferType = StockTransferTypes.StockIssuance Then
                        grvItems.PostEditor()
                        grvItems.UpdateCurrentRow()
                        grvItems.AddNewRow()
                    Else
                        grvItems.FocusedColumn = grvItems.VisibleColumns(1)
                        grvItems.ShowEditor()
                    End If
                End If
                e.AcceptValue = True
            End If
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, "")
        End Try
    End Sub

    Private Sub cmbSKU_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
                Dim FocRow As Integer = grvItems.FocusedRowHandle

                Dim obj As Object = CType(sender, DevExpress.XtraEditors.GridLookUpEdit).EditValue
                If obj IsNot DBNull.Value Then
                    If LoadGridItemInfo(obj, FocRow) Then
                        SaveGridItemInfo(FocRow)
                        grvItems.FocusedColumn = grvItems.VisibleColumns(1)
                        grvItems.ShowEditor()
                    End If
                    e.Handled = True
                End If
            End If
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, "")
        End Try
    End Sub
    Private Sub cmbSKU_CloseUp(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.CloseUpEventArgs)
        Try
            Dim FocRow As Integer = grvItems.FocusedRowHandle
            Dim obj As Object = e.Value
            If obj IsNot DBNull.Value Then
                If LoadGridItemInfo(obj, FocRow) Then
                    SaveGridItemInfo(FocRow)
                    grvItems.FocusedColumn = grvItems.VisibleColumns(1)
                    grvItems.ShowEditor()
                End If
                e.AcceptValue = True
            End If
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, "")
        End Try
    End Sub

    Private Sub cmbFromLocation_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FocRow As Integer = grvItems.FocusedRowHandle
        Dim obj As Object = CType(sender, DevExpress.XtraEditors.GridLookUpEdit).EditValue

        If obj IsNot DBNull.Value And obj IsNot Nothing Then
            grvItems.SetRowCellValue(FocRow, "FromLocID", obj)
            grvItems.ShowEditor()
            SaveGridItemInfo(FocRow)
            FocusNextColumn()
        End If
    End Sub

    Private Sub cmbToLocation_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FocRow As Integer = grvItems.FocusedRowHandle
        Dim obj As Object = CType(sender, DevExpress.XtraEditors.GridLookUpEdit).EditValue
        If obj IsNot DBNull.Value AndAlso obj IsNot Nothing Then
            grvItems.SetRowCellValue(FocRow, "ToLocID", obj)
            SaveGridItemInfo(FocRow)
            FocusNextColumn()
        End If
    End Sub

    Private Sub grdItems_EditorKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdItems.EditorKeyDown
        If e.KeyCode = Keys.Enter Then
          
            If grvItems.FocusedColumn.FieldName = "Price" Then
                grvItems.FocusedColumn = grvItems.Columns("Discount")
                grvItems.ShowEditor()
                e.Handled = True
            ElseIf grvItems.FocusedColumn.FieldName = "SerialNumber" And (TransferType = StockTransferTypes.DOReceiving Or TransferType = StockTransferTypes.DirectReceiving) Then
                grvItems.PostEditor()
                grvItems.UpdateCurrentRow()
                'Add new row if Transfer_Type <> DOReceiving
                If TransferType <> StockTransferTypes.DOReceiving Then
                    grvItems.AddNewRow()
                End If
                e.Handled = False
            ElseIf grvItems.FocusedColumn.FieldName = "AssetStatusID" And (TransferType = StockTransferTypes.ItemReturn Or TransferType = StockTransferTypes.CustodianReturn Or TransferType = StockTransferTypes.StockIssuance) Then
                grvItems.PostEditor()
                grvItems.UpdateCurrentRow()
                grvItems.AddNewRow()
            Else
                FocusNextColumn()
            End If
        End If
    End Sub
    Public Sub FocusNextColumn()
        Dim index As Integer = grvItems.VisibleColumns.IndexOf(grvItems.FocusedColumn)
        If (index < grvItems.VisibleColumns.Count - 1) Then
            index += 1
        Else
            index = 0
        End If
        grvItems.FocusedColumn = grvItems.VisibleColumns(index)
    End Sub

    Public Sub SaveGridItemInfo(ByVal pRowHandler As Integer)
        Try
            Dim msg As String = String.Empty
            If TransferType = StockTransferTypes.DirectReceiving Or TransferType = StockTransferTypes.DOReceiving Then
                If grvItems.GetRowCellValue(pRowHandler, "ItemCode") Is DBNull.Value OrElse grvItems.GetRowCellValue(pRowHandler, "ItemCode") = String.Empty Then
                    msg = My.Resources.Strings.ItemsNotSelected
                    ZulLib.Messages.ErrorMessage(msg, UserName, WhoCalledMe)
                    Exit Sub
                End If
            Else
                If grvItems.GetRowCellValue(pRowHandler, "AssetSerialNumber") Is DBNull.Value OrElse grvItems.GetRowCellValue(pRowHandler, "AssetSerialNumber") = String.Empty Then
                    msg = "Serial Number not selected."
                    ZulLib.Messages.ErrorMessage(msg, UserName, WhoCalledMe)
                    Exit Sub
                End If
            End If

            Dim lRowItemGuid As Guid = grvItems.GetRowCellValue(pRowHandler, "GUID")
            'msg = objStockTransferItem.DeleteByRowGUID(lRowItemGuid)

            msg = objStockTransferItem.Edit(lRowItemGuid)
            If Not String.IsNullOrEmpty(msg) Then 'If Record not found
                objStockTransferItem.NewRecord()
                objStockTransferItem.Attributes.GUID = lRowItemGuid
                objStockTransferItem.Attributes.CreationDate = Now.Date
                objStockTransferItem.Attributes.CreatedBy = UserGUID

                If TransferType = StockTransferTypes.DOReceiving Then
                    'if it's PO Receiving, then we are getting the Number on LoadGridInfo, and no need to generate here.
                    objStockTransferItem.Attributes.Number = grvItems.GetRowCellValue(pRowHandler, "Number")
                Else
                    objStockTransferItem.Attributes.Number = objStockTransferItem.GetMAXNumberByGUID(RecordGUID)
                End If
            End If
            'Save items to database.
            objStockTransferItem.Attributes.StockTransferGUID = RecordGUID
            objStockTransferItem.Attributes.ItemCode = grvItems.GetRowCellValue(pRowHandler, "ItemCode")
            objStockTransferItem.Attributes.Price = grvItems.GetRowCellValue(pRowHandler, "Price")
            objStockTransferItem.Attributes.Discount = grvItems.GetRowCellValue(pRowHandler, "Discount")
            objStockTransferItem.Attributes.QTY = grvItems.GetRowCellValue(pRowHandler, "QTY")
            objStockTransferItem.Attributes.SerialNumber = grvItems.GetRowCellValue(pRowHandler, "AssetSerialNumber").ToString
            objStockTransferItem.Attributes.AstID = grvItems.GetRowCellValue(pRowHandler, "AstID").ToString

            objStockTransferItem.Attributes.Notes = ""
            If grvItems.GetRowCellValue(pRowHandler, "FromLocID") Is DBNull.Value OrElse grvItems.GetRowCellValue(pRowHandler, "FromLocID") = "" Then
                objStockTransferItem.Attributes.SetFromLocIDNull()
            Else
                objStockTransferItem.Attributes.FromLocID = grvItems.GetRowCellValue(pRowHandler, "FromLocID")
            End If

            If grvItems.GetRowCellValue(pRowHandler, "ToLocID") Is DBNull.Value OrElse grvItems.GetRowCellValue(pRowHandler, "ToLocID") = "" Then
                objStockTransferItem.Attributes.SetToLocIDNull()
            Else
                objStockTransferItem.Attributes.ToLocID = grvItems.GetRowCellValue(pRowHandler, "ToLocID")
            End If


            If grvItems.GetRowCellValue(pRowHandler, "CustodianID") Is DBNull.Value OrElse grvItems.GetRowCellValue(pRowHandler, "CustodianID") = "" Then
                objStockTransferItem.Attributes.SetCustodianIDNull()
            Else
                objStockTransferItem.Attributes.CustodianID = grvItems.GetRowCellValue(pRowHandler, "CustodianID")
            End If


            If grvItems.GetRowCellValue(pRowHandler, "AssetStatusID") Is DBNull.Value OrElse grvItems.GetRowCellValue(pRowHandler, "AssetStatusID") < 0 Then
                objStockTransferItem.Attributes.SetAssetStatusIDNull()
            Else
                objStockTransferItem.Attributes.AssetStatusID = grvItems.GetRowCellValue(pRowHandler, "AssetStatusID")
            End If

            msg = objStockTransferItem.Save()
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
            Dim msg As String = objStockTransferItem.Edit(grvItems.GetRowCellValue(FocRow, "GUID"))
            If String.IsNullOrEmpty(msg) Then
                objStockTransferItem.Attributes.Discount = Discount
                objStockTransferItem.Attributes.Price = Price
                objStockTransferItem.Attributes.QTY = QTY
                msg = objStockTransferItem.Save()
                If String.IsNullOrEmpty(msg) Then
                    UpdateSubTotalAmount()
                End If
            End If
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, UserName, WhoCalledMe)
        End Try
    End Sub

    Private Sub Custodian_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim FocRow As Integer = grvItems.FocusedRowHandle
            Dim obj As Object = CType(sender, DevExpress.XtraEditors.TextEdit).EditValue
            If obj IsNot DBNull.Value And obj IsNot Nothing Then
                Dim msg As String = objStockTransferItem.Edit(grvItems.GetRowCellValue(FocRow, "GUID"))
                If String.IsNullOrEmpty(msg) Then
                    grvItems.SetRowCellValue(FocRow, "CustodianID", obj)
                    grvItems.ShowEditor()

                    SaveGridItemInfo(FocRow)
                End If
            End If
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, UserName, WhoCalledMe)
        End Try
    End Sub

    Private Sub AssetStatus_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim FocRow As Integer = grvItems.FocusedRowHandle
            Dim obj As Object = CType(sender, DevExpress.XtraEditors.TextEdit).EditValue
            If obj IsNot DBNull.Value And obj IsNot Nothing Then
                Dim msg As String = objStockTransferItem.Edit(grvItems.GetRowCellValue(FocRow, "GUID"))
                If String.IsNullOrEmpty(msg) Then
                    grvItems.SetRowCellValue(FocRow, "AssetStatusID", obj)
                    grvItems.ShowEditor()

                    SaveGridItemInfo(FocRow)
                End If
            End If
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, UserName, WhoCalledMe)
        End Try
    End Sub


    Private Sub grdView_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles grvItems.InitNewRow
        Try
            Dim View As ColumnView = grdItems.FocusedView
            grvItems.SetRowCellValue(e.RowHandle, "GUID", Guid.NewGuid)
            grvItems.SetRowCellValue(e.RowHandle, "QTY", 1)
            grvItems.SetRowCellValue(e.RowHandle, "Price", 0)
            grvItems.SetRowCellValue(e.RowHandle, "Discount", 0)
            grvItems.SetRowCellValue(e.RowHandle, "Total", 0)
            grvItems.SetRowCellValue(e.RowHandle, "SerialNumber", 0)
            Dim column As GridColumn = View.Columns("ItemCode")
            If TransferType = StockTransferTypes.DirectReceiving Or TransferType = StockTransferTypes.DOReceiving Then
                grvItems.SetRowCellValue(e.RowHandle, "ToLocID", cmbToLocation.EditValue) 'Warehouse
                grvItems.SetRowCellValue(e.RowHandle, "AssetStatusID", 3) 'In Stock
                grvItems.SetRowCellValue(e.RowHandle, "CustodianID", cmbCustodian.EditValue) 'Others
                column = View.Columns("ItemCode")
            ElseIf TransferType = StockTransferTypes.StockIssuance Then
                grvItems.SetRowCellValue(e.RowHandle, "ToLocID", cmbToLocation.EditValue)
                grvItems.SetRowCellValue(e.RowHandle, "AssetStatusID", 2) 'Live
                grvItems.SetRowCellValue(e.RowHandle, "CustodianID", cmbFromLocation.EditValue)
                column = View.Columns("AssetSerialNumber")
            ElseIf TransferType = StockTransferTypes.ItemReturn Or TransferType = StockTransferTypes.CustodianReturn Then
                column = View.Columns("AssetSerialNumber")
            End If


            If Not column Is Nothing Then
                View.FocusedRowHandle = e.RowHandle
                View.FocusedColumn = column
            End If
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, UserName, WhoCalledMe)
        End Try
    End Sub

    Private Sub grdViewItemsItems_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles grvItems.InvalidRowException
        'Suppress displaying the error message box
        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction
    End Sub

    Private Sub ctlStockTransfer_OnPrintClicked() Handles Me.OnPrintClicked
        If ZulLib.Messages.QuestionMessage(My.Resources.Strings.PrintReportConfirmation) = DialogResult.Yes Then
            grdItems.Print()
        End If
    End Sub

    Private Sub ctlStockTransfer_OnPreviewClicked() Handles Me.OnPreviewClicked
        grdItems.ShowPrintPreview()
    End Sub


    Public Function RowValidation() As Boolean
        Dim FromLocationID As New Object
        Dim ToLocationID As New Object
        Dim focrow As Integer = grvItems.FocusedRowHandle

        'We are clearing columns from grdItems after posting
        'therefore it is necessary to check view of column 
        'otherwise exception is coming in checking the visiblity of columns
        'This condition is defined only for those columns that are shown/hide based on stocktransfer type,selected warehouse
        'Check this function here we are clearing columns from grid: grdItems_OnLoadData

        ' If grvItems.Columns.Count > 0 AndAlso grvItems.Columns("Price").Visible Then




        If grvItems.Columns("Price") IsNot Nothing AndAlso grvItems.Columns("Price").Visible Then
            Dim Price As Object = grvItems.GetRowCellValue(focrow, "Price")
            If Price Is DBNull.Value OrElse Price < 0 Then
                grvItems.SetColumnError(grvItems.Columns("Price"), My.Resources.Strings.PriceEqualOrGreaterThanZero)
                Return True
            Else
                grvItems.SetColumnError(grvItems.Columns("Price"), String.Empty, DXErrorProvider.ErrorType.None)
            End If
        End If

        Dim Qty As Object = grvItems.GetRowCellValue(focrow, "QTY")
        If Qty Is DBNull.Value OrElse Qty <= 0 Then
            grvItems.SetColumnError(grvItems.Columns("QTY"), My.Resources.Strings.QTYCreaterThanZero)
            Return True
        Else
            grvItems.SetColumnError(grvItems.Columns("QTY"), String.Empty, DXErrorProvider.ErrorType.None)
        End If

        Dim Discount As Object = grvItems.GetRowCellValue(focrow, "Discount")
        If Discount Is DBNull.Value OrElse Discount < 0 Then
            grvItems.SetColumnError(grvItems.Columns("Discount"), My.Resources.Strings.DiscountEqualOrGreaterThanZero)
            Return True
        Else
            grvItems.SetColumnError(grvItems.Columns("Discount"), String.Empty, DXErrorProvider.ErrorType.None)
        End If

        Dim Total As Object = grvItems.GetRowCellValue(focrow, "Total")
        If Total Is DBNull.Value OrElse Total < 0 Then
            grvItems.SetColumnError(grvItems.Columns("Total"), My.Resources.Strings.TotalEqualOrCreaterThanZero)
            Return True
        Else
            grvItems.SetColumnError(grvItems.Columns("Total"), String.Empty, DXErrorProvider.ErrorType.None)
        End If
        If grvItems.Columns("FromLocID") IsNot Nothing AndAlso grvItems.Columns("FromLocID").Visible Then
            FromLocationID = grvItems.GetRowCellValue(focrow, "FromLocID")
            If FromLocationID Is DBNull.Value And TransferType <> StockTransferTypes.DOReceiving And TransferType <> StockTransferTypes.DirectReceiving Then
                grvItems.SetColumnError(grvItems.Columns("FromLocID"), My.Resources.Strings.valRulenotEmpty)
                Return True
            Else
                grvItems.SetColumnError(grvItems.Columns("FromLocID"), String.Empty, DXErrorProvider.ErrorType.None)
            End If
        End If

        If grvItems.Columns("ToLocID") IsNot Nothing AndAlso grvItems.Columns("ToLocID").Visible Then
            ToLocationID = grvItems.GetRowCellValue(focrow, "ToLocID")
            If ToLocationID Is DBNull.Value And TransferType <> StockTransferTypes.StockIssuance And TransferType <> StockTransferTypes.ItemReturn Then
                grvItems.SetColumnError(grvItems.Columns("ToLocID"), My.Resources.Strings.valRulenotEmpty)
                Return True
            Else
                grvItems.SetColumnError(grvItems.Columns("ToLocID"), String.Empty, DXErrorProvider.ErrorType.None)
            End If
        End If

        If grvItems.Columns("FromLocID") IsNot Nothing AndAlso grvItems.Columns("ToLocID") IsNot Nothing AndAlso grvItems.Columns("FromLocID").Visible AndAlso grvItems.Columns("ToLocID").Visible Then
            If FromLocationID IsNot DBNull.Value AndAlso ToLocationID IsNot DBNull.Value AndAlso FromLocationID = ToLocationID Then
                grvItems.SetColumnError(grvItems.Columns("ToLocID"), My.Resources.Strings.FromToLocationsSame)
                Return True
            Else
                grvItems.SetColumnError(grvItems.Columns("ToLocID"), String.Empty, DXErrorProvider.ErrorType.None)
            End If
        End If

        If grvItems.Columns("CustodianID") IsNot Nothing AndAlso grvItems.Columns("CustodianID").Visible Then
            Dim CustodianID As Object = grvItems.GetRowCellValue(focrow, "CustodianID")
            If CustodianID Is DBNull.Value Then
                grvItems.SetColumnError(grvItems.Columns("CustodianID"), My.Resources.Strings.valRulenotEmpty)
                Return True
            Else
                grvItems.SetColumnError(grvItems.Columns("CustodianID"), String.Empty, DXErrorProvider.ErrorType.None)
            End If
        End If

        If grvItems.Columns("AssetStatusID") IsNot Nothing AndAlso grvItems.Columns("AssetStatusID").Visible Then
            Dim AssetStatusID As Object = grvItems.GetRowCellValue(focrow, "AssetStatusID")
            If AssetStatusID Is DBNull.Value Then
                grvItems.SetColumnError(grvItems.Columns("AssetStatusID"), My.Resources.Strings.valRulenotEmpty)
                Return True
            Else
                grvItems.SetColumnError(grvItems.Columns("AssetStatusID"), String.Empty, DXErrorProvider.ErrorType.None)
            End If
        End If

      
        'If grvItems.GetRowCellValue(focrow, "SerialNumber") < grvItems.GetRowCellValue(focrow, "QTY") Then
        '    grvItems.SetColumnError(grvItems.Columns("SerialNumber"), My.Resources.Strings.valRulenotEmpty)
        '    Return True
        'Else
        '    grvItems.SetColumnError(grvItems.Columns("SerialNumber"), String.Empty, DXErrorProvider.ErrorType.None)
        'End If
    End Function

    Private Sub grdViewItems_ValidateRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles grvItems.ValidateRow
        e.Valid = True
        Dim ItemCode As Object = grvItems.GetRowCellValue(e.RowHandle, "ItemCode")
        If ItemCode Is DBNull.Value OrElse ItemCode = String.Empty Then
            e.Valid = True
            grvItems.DeleteRow(e.RowHandle)
            Return
        End If

        If TransferType = StockTransferTypes.StockIssuance Or TransferType = StockTransferTypes.ItemReturn Or TransferType = StockTransferTypes.CustodianReturn Then
            Dim SerialNumber As String = grvItems.GetRowCellValue(e.RowHandle, "AssetSerialNumber").ToString
            If Not String.IsNullOrEmpty(SerialNumber) Then
                Dim DocCode As String = objStockTransferItem.GetSerialNumberPreparedDocCode(SerialNumber, grvItems.GetRowCellValue(e.RowHandle, "GUID"))
                If Not String.IsNullOrEmpty(DocCode) Then
                    grvItems.SetColumnError(grvItems.Columns("AssetSerialNumber"), String.Format("Serial Number is already exists in prepared document({0})", DocCode))
                    e.Valid = False
                End If
                'Dim focusedRowHandel As Integer = grvItems.LocateByValue(0, grvItems.Columns("AssetSerialNumber"), grvItems.GetRowCellValue(e.RowHandle, "AssetSerialNumber"))
                'If focusedRowHandel <> e.RowHandle Then
                '    grvItems.SetColumnError(grvItems.Columns("AssetSerialNumber"), "Serial Number is already exists in grid")
                '    e.Valid = False
                'End If

                'focusedRowHandel = grvItems.LocateByValue(e.RowHandle + 1, grvItems.Columns("AssetSerialNumber"), grvItems.GetRowCellValue(e.RowHandle, "AssetSerialNumber"))
                'If grvItems.IsValidRowHandle(focusedRowHandel) Then
                '    grvItems.SetColumnError(grvItems.Columns("AssetSerialNumber"), "Serial Number is already exists in grid")
                '    e.Valid = False
                'End If
                'End If
            Else
                e.Valid = True
                grvItems.DeleteRow(e.RowHandle)
                Return
            End If
        End If


        If RowValidation() Then
            e.Valid = False
        End If
    End Sub
    Private Sub cmbToLocation_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbToLocation.QueryPopUp
        PopulateToWarehouse()
    End Sub

    Private Sub cmbFromLocation_QueryPopUp(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbFromLocation.QueryPopUp
        PopulateFromWarehouse()
    End Sub

    Public Sub PopulateFromWarehouse()
        If TransferType = StockTransferTypes.DirectReceiving Or TransferType = StockTransferTypes.DOReceiving Or TransferType = StockTransferTypes.ItemReturn Then
            Dim objBALSupplier As New BALSupplier
            cmbFromLocation.Properties.ValueMember = "SuppID"
            cmbFromLocation.Properties.DisplayMember = "SuppName"
            cmbFromLocation.Properties.DataSource = objBALSupplier.GetAllData_GetCombo()
        ElseIf TransferType = StockTransferTypes.StockIssuance Then
            Dim objBALCustodian As New BALCustodian
            cmbFromLocation.Properties.ValueMember = "ID"
            cmbFromLocation.Properties.DisplayMember = "ID"
            cmbFromLocation.Properties.DataSource = objBALCustodian.GetAllData_GetCombo()

            '    'cmbFromWarehouse.Properties.PopulateViewColumns()
            'ElseIf TransferType = StockTransferTypes.CustomerReturn Then
            '    Dim objCustomer As New MartBLL.CustomerBLL
            '    cmbFromWarehouse.Properties.ValueMember = objCustomer.ListValueMember
            '    cmbFromWarehouse.Properties.DisplayMember = objCustomer.ListDisplayMember
            '    cmbFromWarehouse.Properties.DataSource = objCustomer.GetListData()
            '    'cmbFromWarehouse.Properties.PopulateViewColumns()

            'Else
            '    Dim objFromWarehouse As New MartBLL.WarehouseBLL
            '    cmbFromWarehouse.Properties.ValueMember = objFromWarehouse.ListValueMember
            '    cmbFromWarehouse.Properties.DisplayMember = objFromWarehouse.ListDisplayMember
            '    cmbFromWarehouse.Properties.DataSource = objFromWarehouse.GetListData()
            '    'cmbFromWarehouse.Properties.PopulateViewColumns()
            '    cmbFromWarehouse.Properties.View.Columns("GUID").Visible = False
        End If
        'cmbFromWarehouse.Properties.View.Columns("GUID").Visible = False
    End Sub
    Public Sub PopulateToWarehouse()
        'If TransferType = StockTransferTypes.ItemReturn Then
        '    Dim objSupplier As New MartBLL.SupplierBLL
        '    cmbToWarehouse.Properties.ValueMember = objSupplier.ListValueMember
        '    cmbToWarehouse.Properties.DisplayMember = objSupplier.ListDisplayMember
        '    cmbToWarehouse.Properties.DataSource = objSupplier.GetListData()
        '    'cmbToWarehouse.Properties.PopulateViewColumns()

        'ElseIf TransferType = StockTransferTypes.StockIssuance Or TransferType = StockTransferTypes.WOIssuance Then
        '    Dim objCustomer As New MartBLL.CustomerBLL
        '    cmbToWarehouse.Properties.ValueMember = objCustomer.ListValueMember
        '    cmbToWarehouse.Properties.DisplayMember = objCustomer.ListDisplayMember
        '    cmbToWarehouse.Properties.DataSource = objCustomer.GetListData()
        '    'cmbToWarehouse.Properties.PopulateViewColumns()

        'Else
        'If TransferType = StockTransferTypes.DirectReceiving Or TransferType = StockTransferTypes.DOReceiving Or TransferType = StockTransferTypes.ItemReturn Then

        'ElseIf TransferType = StockTransferTypes.StockIssuance Then
        Dim objBALLocation As New BALLocation
        cmbToLocation.Properties.ValueMember = "LocID"
        cmbToLocation.Properties.DisplayMember = "LocationFullPath"
        cmbToLocation.Properties.DataSource = objBALLocation.GetComboLocationsSortedByCode(New attLocation)
        cmbToLocation.Properties.View.Columns("Code").Visible = False
        cmbToLocation.Properties.View.Columns("locLevel").Visible = False
        cmbToLocation.Properties.View.Columns("CompanyID").Visible = False
        cmbToLocation.Properties.View.Columns("CompanyName").Visible = False

        cmbToLocation.Properties.View.Columns("LocID").Visible = False
        cmbToLocation.Properties.View.Columns("CompCode").Caption = "Loc Code"
        cmbToLocation.Properties.View.Columns("CompCode").VisibleIndex = 0
        cmbToLocation.Properties.View.Columns("LocDesc").VisibleIndex = 1
        cmbToLocation.Properties.View.Columns("LocDesc").Visible = False

        'cmbToWarehouse.Properties.PopulateViewColumns()
        'End If

        'End If
        'cmbToWarehouse.Properties.View.Columns("GUID").Visible = False
    End Sub

    Public Sub PopulateFromLocation()

        Dim objBALLocation As New BALLocation
        RitmFromLocation.ValueMember = "LocID"
        RitmFromLocation.DisplayMember = "LocationFullPath"
        RitmFromLocation.NullText = String.Empty
        RitmFromLocation.DataSource = objBALLocation.GetComboLocationsSortedByCode(New attLocation)
        RitmFromLocation.View.Columns("Code").Visible = False
        RitmFromLocation.View.Columns("locLevel").Visible = False
        RitmFromLocation.View.Columns("CompanyID").Visible = False
        RitmFromLocation.View.Columns("CompanyName").Visible = False

        RitmFromLocation.View.Columns("LocID").Visible = False
        RitmFromLocation.View.Columns("CompCode").Caption = "Loc Code"
        RitmFromLocation.View.Columns("CompCode").VisibleIndex = 0
        RitmFromLocation.View.Columns("LocDesc").VisibleIndex = 1
        RitmFromLocation.View.Columns("LocDesc").Visible = False
        RitmFromLocation.View.OptionsView.ShowAutoFilterRow = True
        RitmFromLocation.View.OptionsView.ShowIndicator = False

        RemoveHandler RitmFromLocation.EditValueChanged, AddressOf cmbFromLocation_EditValueChanged
        AddHandler RitmFromLocation.EditValueChanged, AddressOf cmbFromLocation_EditValueChanged
        'RemoveHandler RitmFromLocation.QueryPopUp, AddressOf cmbFromLocation_QueryPopUp
        'AddHandler RitmFromLocation.QueryPopUp, AddressOf cmbFromLocation_QueryPopUp
    End Sub

    Public Sub PopulateToLocation()
        Dim objBALLocation As New BALLocation
        RitmToLocation.ValueMember = "LocID"
        RitmToLocation.DisplayMember = "LocationFullPath"
        RitmToLocation.NullText = String.Empty
        RitmToLocation.DataSource = objBALLocation.GetComboLocationsSortedByCode(New attLocation)
        RitmToLocation.View.Columns("Code").Visible = False
        RitmToLocation.View.Columns("locLevel").Visible = False
        RitmToLocation.View.Columns("CompanyID").Visible = False
        RitmToLocation.View.Columns("CompanyName").Visible = False

        RitmToLocation.View.Columns("LocID").Visible = False
        RitmToLocation.View.Columns("CompCode").Caption = "Loc Code"
        RitmToLocation.View.Columns("CompCode").VisibleIndex = 0
        RitmToLocation.View.Columns("LocDesc").VisibleIndex = 1
        RitmToLocation.View.Columns("LocDesc").Visible = False
        RitmToLocation.View.OptionsView.ShowAutoFilterRow = True
        RitmToLocation.View.OptionsView.ShowIndicator = False

        RemoveHandler RitmToLocation.EditValueChanged, AddressOf cmbToLocation_EditValueChanged
        AddHandler RitmToLocation.EditValueChanged, AddressOf cmbToLocation_EditValueChanged
    End Sub


    Private Sub txtCode_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtCode.ButtonClick
        Try
            GenerateDocNumber()
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, "")
        End Try

    End Sub

    Public Sub GenerateDocNumber()
        Try
            txtCode.Text = objDocType.GetDocumentCodeNumber(TransferType.ToString, String.Empty)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    'Public Sub PopulateCurrency()
    '    cmbCurrency.Properties.DataSource = objCurrency.GetListData()
    '    cmbCurrency.Properties.DisplayMember = objCurrency.ListDisplayMember
    '    cmbCurrency.Properties.ValueMember = objCurrency.ListValueMember
    '    cmbCurrency.Properties.PopulateColumns()

    '    If cmbCurrency.Properties.Columns.Count > 0 Then
    '        cmbCurrency.Properties.Columns.RemoveAt(0)
    '    End If
    'End Sub

    Private Sub UpdateSubTotalAmount()
        If RecordGUID <> Nothing AndAlso RecordGUID <> Guid.Empty Then
            If TransferType = StockTransferTypes.DOReceiving Or TransferType = StockTransferTypes.DirectReceiving Then
                objStockTransfer.UpdateStockTransTotalAmount(RecordGUID)
                txtSubTotal.Value = objStockTransfer.GetStockTransTotalByGUID(RecordGUID).ToString(NumericFormat)
            End If
        End If
    End Sub

    Private Sub txtSubTotal_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSubTotal.EditValueChanged, txtTotalDiscount.EditValueChanged, txtAddCharges.EditValueChanged, txtFreightCharge.EditValueChanged
        txtTotal.Value = (txtSubTotal.Value + txtAddCharges.Value + txtFreightCharge.Value - txtTotalDiscount.Value).ToString(NumericFormat)
    End Sub



    Private Sub grvItems_FocusedColumnChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles grvItems.FocusedColumnChanged
        ShowSerialNumberPopupForm()
    End Sub
    Private Sub grvItems_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvItems.FocusedRowChanged
        ShowSerialNumberPopupForm()
    End Sub

    Public Sub ShowSerialNumberPopupForm()
        Dim FocRow As Integer = grvItems.FocusedRowHandle

        Try
            'AndAlso grvItems.GetRowCellValue(FocRow, "SerialNumber") < grvItems.GetRowCellValue(FocRow, "QTY")
            If grvItems.GetRowCellValue(FocRow, "ItemCode") IsNot DBNull.Value AndAlso grvItems.GetRowCellValue(FocRow, "ItemCode") IsNot Nothing Then
                If grvItems.Columns.Count > 0 AndAlso grvItems.FocusedColumn IsNot Nothing AndAlso grvItems.FocusedColumn.FieldName = "SerialNumber" AndAlso grvItems.GetRowCellValue(FocRow, "QTY") > 0 Then
                    Dim ctlSNumber As New ctlSerialNumber
                    ctlSNumber.UserGUID = UserGUID
                    If RowValidation() Then
                        Exit Sub
                    End If
                    ctlSNumber.StockTransferItemRow = grvItems.GetDataRow(FocRow)
                    ctlSNumber.ShowPopupForm(TransferStatus, TransferType)
                    If ctlSNumber.MessageResult = ctlSerialNumber.MessageReturn.MSG_OK Then
                        Dim SNCount As Integer = objStockTransferItemDetail.GetCountByStockTransferItemGUID(grvItems.GetRowCellValue(FocRow, "GUID"))
                        grvItems.SetRowCellValue(FocRow, "SerialNumber", SNCount)
                        'grdItems_OnLoadData(TransferStatus)
                        ' grvItems.FocusedColumn = grvItems.Columns("SerialNumber")
                        'FocusNextColumn()
                        'grvItems.MoveLast()

                    End If
                End If
            End If

        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnPrintLabels_Clicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintLabels.ItemClick
        Dim objCtlPrint As New ctlPrintLabels
        objCtlPrint.StockTransferGUID = RecordGUID
        objCtlPrint.TrnasferType = TransferType
        objCtlPrint.ShowPopupForm()
    End Sub

 
    Private Sub cmbCustodian_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmbCustodian.ButtonClick, cmbFromLocation.ButtonClick
        If e.Button.Index = 1 Then
            Dim frm As New frmCustodian
            frm.WindowState = FormWindowState.Normal
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.ShowDialog()
        End If
    End Sub
End Class

