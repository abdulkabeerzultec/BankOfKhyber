<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlStockTransfer
    Inherits ZulLib.ctlDataEditing

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctlStockTransfer))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl
        Me.cmbCustodian = New DevExpress.XtraEditors.GridLookUpEdit
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.cmbToLocation = New DevExpress.XtraEditors.GridLookUpEdit
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.cmbFromLocation = New DevExpress.XtraEditors.GridLookUpEdit
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.cmbAssetsStatus = New DevExpress.XtraEditors.GridLookUpEdit
        Me.GridView7 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.txtIncidentNum = New DevExpress.XtraEditors.TextEdit
        Me.txtBatchID = New DevExpress.XtraEditors.TextEdit
        Me.txtTotal = New DevExpress.XtraEditors.SpinEdit
        Me.txtTotalDiscount = New DevExpress.XtraEditors.SpinEdit
        Me.txtFreightCharge = New DevExpress.XtraEditors.SpinEdit
        Me.txtSubTotal = New DevExpress.XtraEditors.SpinEdit
        Me.txtAddCharges = New DevExpress.XtraEditors.SpinEdit
        Me.txtCode = New DevExpress.XtraEditors.ButtonEdit
        Me.grdItems = New ZulLib.ctlGrid
        Me.grvItems = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.txtInvNo = New DevExpress.XtraEditors.TextEdit
        Me.lblStatus = New DevExpress.XtraEditors.LabelControl
        Me.cmbPO = New ZulLib.ctlLov
        Me.txtNotes = New DevExpress.XtraEditors.MemoEdit
        Me.dpTransferDate = New DevExpress.XtraEditors.DateEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem
        Me.lciPO = New DevExpress.XtraLayout.LayoutControlItem
        Me.lciDoc = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlTotal = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlAddCharges = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlSubTotal = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlDiscount = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlFreight = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutInvoiceNumber = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutBatchNumber = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutStatus = New DevExpress.XtraLayout.LayoutControlItem
        Me.lciSupp = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutIncidentNumber = New DevExpress.XtraLayout.LayoutControlItem
        Me.lciTransferDate = New DevExpress.XtraLayout.LayoutControlItem
        Me.lciToWarehouse = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutCustodian = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutgrdItems = New DevExpress.XtraLayout.LayoutControlItem
        CType(Me.errProv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.valProvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbListRep, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.cmbCustodian.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbToLocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbFromLocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbAssetsStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIncidentNum.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBatchID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalDiscount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFreightCharge.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddCharges.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInvNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dpTransferDate.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dpTransferDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlAddCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlSubTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlFreight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutInvoiceNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutBatchNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciSupp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutIncidentNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciTransferDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciToWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutCustodian, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutgrdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbListRep
        '
        Me.cmbListRep.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = True
        Me.LayoutControl1.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LayoutControl1.Appearance.DisabledLayoutItem.Options.UseForeColor = True
        Me.LayoutControl1.Controls.Add(Me.cmbCustodian)
        Me.LayoutControl1.Controls.Add(Me.cmbToLocation)
        Me.LayoutControl1.Controls.Add(Me.cmbFromLocation)
        Me.LayoutControl1.Controls.Add(Me.cmbAssetsStatus)
        Me.LayoutControl1.Controls.Add(Me.txtIncidentNum)
        Me.LayoutControl1.Controls.Add(Me.txtBatchID)
        Me.LayoutControl1.Controls.Add(Me.txtTotal)
        Me.LayoutControl1.Controls.Add(Me.txtTotalDiscount)
        Me.LayoutControl1.Controls.Add(Me.txtFreightCharge)
        Me.LayoutControl1.Controls.Add(Me.txtSubTotal)
        Me.LayoutControl1.Controls.Add(Me.txtAddCharges)
        Me.LayoutControl1.Controls.Add(Me.txtCode)
        Me.LayoutControl1.Controls.Add(Me.grdItems)
        Me.LayoutControl1.Controls.Add(Me.txtInvNo)
        Me.LayoutControl1.Controls.Add(Me.lblStatus)
        Me.LayoutControl1.Controls.Add(Me.cmbPO)
        Me.LayoutControl1.Controls.Add(Me.txtNotes)
        Me.LayoutControl1.Controls.Add(Me.dpTransferDate)
        resources.ApplyResources(Me.LayoutControl1, "LayoutControl1")
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        '
        'cmbCustodian
        '
        resources.ApplyResources(Me.cmbCustodian, "cmbCustodian")
        Me.cmbCustodian.Name = "cmbCustodian"
        Me.cmbCustodian.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbCustodian.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines)), New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbCustodian.Properties.Buttons1"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cmbCustodian.Properties.NullText = resources.GetString("cmbCustodian.Properties.NullText")
        Me.cmbCustodian.Properties.View = Me.GridView1
        Me.cmbCustodian.StyleController = Me.LayoutControl1
        '
        'GridView1
        '
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'cmbToLocation
        '
        resources.ApplyResources(Me.cmbToLocation, "cmbToLocation")
        Me.cmbToLocation.Name = "cmbToLocation"
        Me.cmbToLocation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbToLocation.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cmbToLocation.Properties.NullText = resources.GetString("cmbToLocation.Properties.NullText")
        Me.cmbToLocation.Properties.View = Me.GridView4
        Me.cmbToLocation.StyleController = Me.LayoutControl1
        '
        'GridView4
        '
        Me.GridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView4.Name = "GridView4"
        Me.GridView4.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView4.OptionsView.ShowAutoFilterRow = True
        Me.GridView4.OptionsView.ShowGroupPanel = False
        '
        'cmbFromLocation
        '
        resources.ApplyResources(Me.cmbFromLocation, "cmbFromLocation")
        Me.cmbFromLocation.Name = "cmbFromLocation"
        Me.cmbFromLocation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbFromLocation.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines)), New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbFromLocation.Properties.Buttons1"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("cmbFromLocation.Properties.Buttons2"), CType(resources.GetObject("cmbFromLocation.Properties.Buttons3"), Integer), CType(resources.GetObject("cmbFromLocation.Properties.Buttons4"), Boolean), CType(resources.GetObject("cmbFromLocation.Properties.Buttons5"), Boolean), CType(resources.GetObject("cmbFromLocation.Properties.Buttons6"), Boolean), CType(resources.GetObject("cmbFromLocation.Properties.Buttons7"), DevExpress.XtraEditors.ImageLocation), Nothing)})
        Me.cmbFromLocation.Properties.NullText = resources.GetString("cmbFromLocation.Properties.NullText")
        Me.cmbFromLocation.Properties.View = Me.GridView3
        Me.cmbFromLocation.StyleController = Me.LayoutControl1
        '
        'GridView3
        '
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        '
        'cmbAssetsStatus
        '
        resources.ApplyResources(Me.cmbAssetsStatus, "cmbAssetsStatus")
        Me.cmbAssetsStatus.Name = "cmbAssetsStatus"
        Me.cmbAssetsStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbAssetsStatus.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cmbAssetsStatus.Properties.NullText = resources.GetString("cmbAssetsStatus.Properties.NullText")
        Me.cmbAssetsStatus.Properties.View = Me.GridView7
        Me.cmbAssetsStatus.StyleController = Me.LayoutControl1
        '
        'GridView7
        '
        Me.GridView7.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView7.Name = "GridView7"
        Me.GridView7.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView7.OptionsView.ShowAutoFilterRow = True
        Me.GridView7.OptionsView.ShowGroupPanel = False
        '
        'txtIncidentNum
        '
        resources.ApplyResources(Me.txtIncidentNum, "txtIncidentNum")
        Me.txtIncidentNum.Name = "txtIncidentNum"
        Me.txtIncidentNum.StyleController = Me.LayoutControl1
        '
        'txtBatchID
        '
        resources.ApplyResources(Me.txtBatchID, "txtBatchID")
        Me.txtBatchID.Name = "txtBatchID"
        Me.txtBatchID.Properties.ReadOnly = True
        Me.txtBatchID.StyleController = Me.LayoutControl1
        '
        'txtTotal
        '
        resources.ApplyResources(Me.txtTotal, "txtTotal")
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Properties.Appearance.Options.UseTextOptions = True
        Me.txtTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.txtTotal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.txtTotal.Properties.ReadOnly = True
        Me.txtTotal.StyleController = Me.LayoutControl1
        '
        'txtTotalDiscount
        '
        resources.ApplyResources(Me.txtTotalDiscount, "txtTotalDiscount")
        Me.txtTotalDiscount.Name = "txtTotalDiscount"
        Me.txtTotalDiscount.Properties.Appearance.Options.UseTextOptions = True
        Me.txtTotalDiscount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.txtTotalDiscount.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.txtTotalDiscount.StyleController = Me.LayoutControl1
        '
        'txtFreightCharge
        '
        resources.ApplyResources(Me.txtFreightCharge, "txtFreightCharge")
        Me.txtFreightCharge.Name = "txtFreightCharge"
        Me.txtFreightCharge.Properties.Appearance.Options.UseTextOptions = True
        Me.txtFreightCharge.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.txtFreightCharge.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.txtFreightCharge.StyleController = Me.LayoutControl1
        '
        'txtSubTotal
        '
        resources.ApplyResources(Me.txtSubTotal, "txtSubTotal")
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.Properties.Appearance.Options.UseTextOptions = True
        Me.txtSubTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.txtSubTotal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.txtSubTotal.Properties.ReadOnly = True
        Me.txtSubTotal.StyleController = Me.LayoutControl1
        '
        'txtAddCharges
        '
        resources.ApplyResources(Me.txtAddCharges, "txtAddCharges")
        Me.txtAddCharges.Name = "txtAddCharges"
        Me.txtAddCharges.Properties.Appearance.Options.UseTextOptions = True
        Me.txtAddCharges.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.txtAddCharges.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.txtAddCharges.StyleController = Me.LayoutControl1
        '
        'txtCode
        '
        resources.ApplyResources(Me.txtCode, "txtCode")
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.txtCode.StyleController = Me.LayoutControl1
        '
        'grdItems
        '
        resources.ApplyResources(Me.grdItems, "grdItems")
        Me.grdItems.MainView = Me.grvItems
        Me.grdItems.Name = "grdItems"
        Me.grdItems.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvItems})
        '
        'grvItems
        '
        Me.grvItems.GridControl = Me.grdItems
        Me.grvItems.Name = "grvItems"
        Me.grvItems.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvItems.OptionsView.ShowGroupPanel = False
        '
        'txtInvNo
        '
        resources.ApplyResources(Me.txtInvNo, "txtInvNo")
        Me.txtInvNo.Name = "txtInvNo"
        Me.txtInvNo.Properties.ReadOnly = True
        Me.txtInvNo.StyleController = Me.LayoutControl1
        '
        'lblStatus
        '
        Me.lblStatus.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStatus.Appearance.Options.UseFont = True
        Me.lblStatus.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblStatus, "lblStatus")
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.StyleController = Me.LayoutControl1
        '
        'cmbPO
        '
        Me.cmbPO.btnAddDataVisible = False
        Me.cmbPO.DataSource = Nothing
        Me.cmbPO.DisplayMember = ""
        resources.ApplyResources(Me.cmbPO, "cmbPO")
        Me.cmbPO.HideValueMember = True
        Me.cmbPO.Name = "cmbPO"
        Me.cmbPO.SelectedText = ""
        Me.cmbPO.SelectedValue = Nothing
        Me.cmbPO.ValueMember = ""
        '
        'txtNotes
        '
        resources.ApplyResources(Me.txtNotes, "txtNotes")
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.StyleController = Me.LayoutControl1
        '
        'dpTransferDate
        '
        resources.ApplyResources(Me.dpTransferDate, "dpTransferDate")
        Me.dpTransferDate.Name = "dpTransferDate"
        Me.dpTransferDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dpTransferDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dpTransferDate.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.dpTransferDate.StyleController = Me.LayoutControl1
        '
        'LayoutControlGroup1
        '
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem7, Me.LayoutControlItem13, Me.lciPO, Me.lciDoc, Me.LayoutControlGroup2, Me.EmptySpaceItem2, Me.LayoutInvoiceNumber, Me.LayoutBatchNumber, Me.LayoutStatus, Me.lciSupp, Me.LayoutIncidentNumber, Me.lciTransferDate, Me.lciToWarehouse, Me.LayoutCustodian})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(800, 470)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem7.Control = Me.lblStatus
        resources.ApplyResources(Me.LayoutControlItem7, "LayoutControlItem7")
        Me.LayoutControlItem7.Location = New System.Drawing.Point(682, 0)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(112, 31)
        Me.LayoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem7.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(38, 13)
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.grdItems
        resources.ApplyResources(Me.LayoutControlItem13, "LayoutControlItem13")
        Me.LayoutControlItem13.Location = New System.Drawing.Point(0, 151)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(794, 201)
        Me.LayoutControlItem13.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem13.TextToControlDistance = 0
        Me.LayoutControlItem13.TextVisible = False
        '
        'lciPO
        '
        Me.lciPO.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lciPO.AppearanceItemCaption.Options.UseFont = True
        Me.lciPO.Control = Me.cmbPO
        resources.ApplyResources(Me.lciPO, "lciPO")
        Me.lciPO.Location = New System.Drawing.Point(393, 0)
        Me.lciPO.MaxSize = New System.Drawing.Size(0, 31)
        Me.lciPO.MinSize = New System.Drawing.Size(191, 31)
        Me.lciPO.Name = "lciPO"
        Me.lciPO.Size = New System.Drawing.Size(267, 31)
        Me.lciPO.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.lciPO.TextLocation = DevExpress.Utils.Locations.Left
        Me.lciPO.TextSize = New System.Drawing.Size(72, 13)
        Me.lciPO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'lciDoc
        '
        Me.lciDoc.Control = Me.txtCode
        resources.ApplyResources(Me.lciDoc, "lciDoc")
        Me.lciDoc.Location = New System.Drawing.Point(0, 0)
        Me.lciDoc.MinSize = New System.Drawing.Size(133, 24)
        Me.lciDoc.Name = "lciDoc"
        Me.lciDoc.Size = New System.Drawing.Size(393, 31)
        Me.lciDoc.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.lciDoc.TextLocation = DevExpress.Utils.Locations.Left
        Me.lciDoc.TextSize = New System.Drawing.Size(72, 13)
        '
        'LayoutControlGroup2
        '
        resources.ApplyResources(Me.LayoutControlGroup2, "LayoutControlGroup2")
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlTotal, Me.LayoutControlAddCharges, Me.LayoutControlSubTotal, Me.LayoutControlDiscount, Me.LayoutControlFreight, Me.LayoutControlItem4})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 352)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(794, 112)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlTotal
        '
        Me.LayoutControlTotal.Control = Me.txtTotal
        resources.ApplyResources(Me.LayoutControlTotal, "LayoutControlTotal")
        Me.LayoutControlTotal.Location = New System.Drawing.Point(388, 60)
        Me.LayoutControlTotal.Name = "LayoutControlTotal"
        Me.LayoutControlTotal.Size = New System.Drawing.Size(400, 46)
        Me.LayoutControlTotal.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlTotal.TextSize = New System.Drawing.Size(72, 13)
        '
        'LayoutControlAddCharges
        '
        Me.LayoutControlAddCharges.Control = Me.txtAddCharges
        resources.ApplyResources(Me.LayoutControlAddCharges, "LayoutControlAddCharges")
        Me.LayoutControlAddCharges.Location = New System.Drawing.Point(0, 30)
        Me.LayoutControlAddCharges.Name = "LayoutControlAddCharges"
        Me.LayoutControlAddCharges.Size = New System.Drawing.Size(388, 30)
        Me.LayoutControlAddCharges.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlAddCharges.TextSize = New System.Drawing.Size(72, 13)
        '
        'LayoutControlSubTotal
        '
        Me.LayoutControlSubTotal.Control = Me.txtSubTotal
        resources.ApplyResources(Me.LayoutControlSubTotal, "LayoutControlSubTotal")
        Me.LayoutControlSubTotal.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlSubTotal.Name = "LayoutControlSubTotal"
        Me.LayoutControlSubTotal.Size = New System.Drawing.Size(388, 30)
        Me.LayoutControlSubTotal.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlSubTotal.TextSize = New System.Drawing.Size(72, 13)
        '
        'LayoutControlDiscount
        '
        Me.LayoutControlDiscount.Control = Me.txtTotalDiscount
        resources.ApplyResources(Me.LayoutControlDiscount, "LayoutControlDiscount")
        Me.LayoutControlDiscount.Location = New System.Drawing.Point(388, 0)
        Me.LayoutControlDiscount.Name = "LayoutControlDiscount"
        Me.LayoutControlDiscount.Size = New System.Drawing.Size(400, 30)
        Me.LayoutControlDiscount.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlDiscount.TextSize = New System.Drawing.Size(72, 13)
        '
        'LayoutControlFreight
        '
        Me.LayoutControlFreight.Control = Me.txtFreightCharge
        resources.ApplyResources(Me.LayoutControlFreight, "LayoutControlFreight")
        Me.LayoutControlFreight.Location = New System.Drawing.Point(388, 30)
        Me.LayoutControlFreight.Name = "LayoutControlFreight"
        Me.LayoutControlFreight.Size = New System.Drawing.Size(400, 30)
        Me.LayoutControlFreight.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlFreight.TextSize = New System.Drawing.Size(72, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.txtNotes
        resources.ApplyResources(Me.LayoutControlItem4, "LayoutControlItem4")
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(0, 46)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(101, 46)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(388, 46)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(72, 13)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        resources.ApplyResources(Me.EmptySpaceItem2, "EmptySpaceItem2")
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(660, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(22, 31)
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutInvoiceNumber
        '
        Me.LayoutInvoiceNumber.Control = Me.txtInvNo
        resources.ApplyResources(Me.LayoutInvoiceNumber, "LayoutInvoiceNumber")
        Me.LayoutInvoiceNumber.Location = New System.Drawing.Point(0, 31)
        Me.LayoutInvoiceNumber.Name = "lciInvoice"
        Me.LayoutInvoiceNumber.Size = New System.Drawing.Size(393, 30)
        Me.LayoutInvoiceNumber.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutInvoiceNumber.TextSize = New System.Drawing.Size(72, 13)
        '
        'LayoutBatchNumber
        '
        Me.LayoutBatchNumber.Control = Me.txtBatchID
        resources.ApplyResources(Me.LayoutBatchNumber, "LayoutBatchNumber")
        Me.LayoutBatchNumber.Location = New System.Drawing.Point(393, 31)
        Me.LayoutBatchNumber.Name = "LayoutBatchNumber"
        Me.LayoutBatchNumber.Size = New System.Drawing.Size(401, 30)
        Me.LayoutBatchNumber.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutBatchNumber.TextSize = New System.Drawing.Size(72, 13)
        '
        'LayoutStatus
        '
        Me.LayoutStatus.Control = Me.cmbAssetsStatus
        resources.ApplyResources(Me.LayoutStatus, "LayoutStatus")
        Me.LayoutStatus.Location = New System.Drawing.Point(393, 121)
        Me.LayoutStatus.Name = "LayoutStatus"
        Me.LayoutStatus.Size = New System.Drawing.Size(401, 30)
        Me.LayoutStatus.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutStatus.TextSize = New System.Drawing.Size(72, 13)
        Me.LayoutStatus.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'lciSupp
        '
        Me.lciSupp.Control = Me.cmbFromLocation
        resources.ApplyResources(Me.lciSupp, "lciSupp")
        Me.lciSupp.Location = New System.Drawing.Point(0, 91)
        Me.lciSupp.Name = "lciSupp"
        Me.lciSupp.Size = New System.Drawing.Size(393, 30)
        Me.lciSupp.TextLocation = DevExpress.Utils.Locations.Left
        Me.lciSupp.TextSize = New System.Drawing.Size(72, 13)
        '
        'LayoutIncidentNumber
        '
        Me.LayoutIncidentNumber.Control = Me.txtIncidentNum
        resources.ApplyResources(Me.LayoutIncidentNumber, "LayoutIncidentNumber")
        Me.LayoutIncidentNumber.Location = New System.Drawing.Point(393, 61)
        Me.LayoutIncidentNumber.Name = "LayoutIncidentNumber"
        Me.LayoutIncidentNumber.Size = New System.Drawing.Size(401, 30)
        Me.LayoutIncidentNumber.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutIncidentNumber.TextSize = New System.Drawing.Size(72, 13)
        '
        'lciTransferDate
        '
        Me.lciTransferDate.Control = Me.dpTransferDate
        resources.ApplyResources(Me.lciTransferDate, "lciTransferDate")
        Me.lciTransferDate.Location = New System.Drawing.Point(0, 61)
        Me.lciTransferDate.Name = "lciTransferDate"
        Me.lciTransferDate.Size = New System.Drawing.Size(393, 30)
        Me.lciTransferDate.TextLocation = DevExpress.Utils.Locations.Left
        Me.lciTransferDate.TextSize = New System.Drawing.Size(72, 13)
        '
        'lciToWarehouse
        '
        Me.lciToWarehouse.Control = Me.cmbToLocation
        resources.ApplyResources(Me.lciToWarehouse, "lciToWarehouse")
        Me.lciToWarehouse.Location = New System.Drawing.Point(393, 91)
        Me.lciToWarehouse.Name = "lciToWarehouse"
        Me.lciToWarehouse.Size = New System.Drawing.Size(401, 30)
        Me.lciToWarehouse.TextLocation = DevExpress.Utils.Locations.Left
        Me.lciToWarehouse.TextSize = New System.Drawing.Size(72, 13)
        '
        'LayoutCustodian
        '
        Me.LayoutCustodian.Control = Me.cmbCustodian
        resources.ApplyResources(Me.LayoutCustodian, "LayoutCustodian")
        Me.LayoutCustodian.Location = New System.Drawing.Point(0, 121)
        Me.LayoutCustodian.Name = "LayoutCustodian"
        Me.LayoutCustodian.Size = New System.Drawing.Size(393, 30)
        Me.LayoutCustodian.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutCustodian.TextSize = New System.Drawing.Size(72, 13)
        '
        'LayoutgrdItems
        '
        resources.ApplyResources(Me.LayoutgrdItems, "LayoutgrdItems")
        Me.LayoutgrdItems.Location = New System.Drawing.Point(0, 244)
        Me.LayoutgrdItems.Name = "LayoutgrdItems"
        Me.LayoutgrdItems.Size = New System.Drawing.Size(733, 218)
        Me.LayoutgrdItems.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutgrdItems.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutgrdItems.TextToControlDistance = 0
        Me.LayoutgrdItems.TextVisible = False
        '
        'ctlStockTransfer
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "ctlStockTransfer"
        Me.Controls.SetChildIndex(Me.LayoutControl1, 0)
        CType(Me.errProv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.valProvMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbListRep, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.cmbCustodian.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbToLocation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbFromLocation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbAssetsStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIncidentNum.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBatchID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalDiscount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFreightCharge.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddCharges.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInvNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dpTransferDate.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dpTransferDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciPO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlAddCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlSubTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlFreight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutInvoiceNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutBatchNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciSupp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutIncidentNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciTransferDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciToWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutCustodian, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutgrdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents dpTransferDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lciTransferDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtNotes As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cmbPO As ZulLib.ctlLov
    Friend WithEvents lciPO As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lblStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtInvNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutInvoiceNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdItems As ZulLib.ctlGrid
    Friend WithEvents grvItems As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutgrdItems As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents lciDoc As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtSubTotal As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtTotalDiscount As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtTotal As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtFreightCharge As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtAddCharges As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents txtBatchID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutBatchNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtIncidentNum As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutIncidentNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cmbAssetsStatus As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutStatus As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cmbFromLocation As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lciSupp As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cmbToLocation As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lciToWarehouse As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlTotal As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlAddCharges As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlSubTotal As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlDiscount As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlFreight As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cmbCustodian As DevExpress.XtraEditors.GridLookUpEdit

    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutCustodian As DevExpress.XtraLayout.LayoutControlItem

End Class
