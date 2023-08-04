<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlPurchaseOrder
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctlPurchaseOrder))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl
        Me.txtQuotation = New DevExpress.XtraEditors.MemoEdit
        Me.txtTotal = New DevExpress.XtraEditors.SpinEdit
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl
        Me.BarDockControl2 = New DevExpress.XtraBars.BarDockControl
        Me.BarDockControl3 = New DevExpress.XtraBars.BarDockControl
        Me.BarDockControl4 = New DevExpress.XtraBars.BarDockControl
        Me.txtFreightCharge = New DevExpress.XtraEditors.SpinEdit
        Me.txtTotalDiscount = New DevExpress.XtraEditors.SpinEdit
        Me.txtSubTotal = New DevExpress.XtraEditors.SpinEdit
        Me.txtPurchaseOrder = New DevExpress.XtraEditors.ButtonEdit
        Me.grdItems = New ZulLib.ctlGrid
        Me.grvItems = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.cmbCost = New DevExpress.XtraEditors.LookUpEdit
        Me.dpApproveDate = New DevExpress.XtraEditors.DateEdit
        Me.txtPayment = New DevExpress.XtraEditors.MemoEdit
        Me.txtMode = New DevExpress.XtraEditors.MemoEdit
        Me.txtReferenceNo = New DevExpress.XtraEditors.TextEdit
        Me.txtModeofDel = New DevExpress.XtraEditors.TextEdit
        Me.lblStatus = New DevExpress.XtraEditors.LabelControl
        Me.txtAddCharges = New DevExpress.XtraEditors.SpinEdit
        Me.cmbCustodian = New ZulLib.ctlLov
        Me.cmbSupplier = New ZulLib.ctlLov
        Me.txtRemarks = New DevExpress.XtraEditors.MemoEdit
        Me.dtOrder = New DevExpress.XtraEditors.DateEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutTabbedGroup = New DevExpress.XtraLayout.TabbedControlGroup
        Me.LayoutControlGroup7 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutgrdItems = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutSummary = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        CType(Me.errProv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.valProvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbListRep, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.txtQuotation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFreightCharge.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalDiscount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPurchaseOrder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCost.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dpApproveDate.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dpApproveDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPayment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReferenceNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtModeofDel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddCharges.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtOrder.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtOrder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutTabbedGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutgrdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.LayoutControl1.Controls.Add(Me.txtQuotation)
        Me.LayoutControl1.Controls.Add(Me.txtTotal)
        Me.LayoutControl1.Controls.Add(Me.txtFreightCharge)
        Me.LayoutControl1.Controls.Add(Me.txtTotalDiscount)
        Me.LayoutControl1.Controls.Add(Me.txtSubTotal)
        Me.LayoutControl1.Controls.Add(Me.txtPurchaseOrder)
        Me.LayoutControl1.Controls.Add(Me.grdItems)
        Me.LayoutControl1.Controls.Add(Me.cmbCost)
        Me.LayoutControl1.Controls.Add(Me.dpApproveDate)
        Me.LayoutControl1.Controls.Add(Me.txtPayment)
        Me.LayoutControl1.Controls.Add(Me.txtMode)
        Me.LayoutControl1.Controls.Add(Me.txtReferenceNo)
        Me.LayoutControl1.Controls.Add(Me.txtModeofDel)
        Me.LayoutControl1.Controls.Add(Me.lblStatus)
        Me.LayoutControl1.Controls.Add(Me.txtAddCharges)
        Me.LayoutControl1.Controls.Add(Me.cmbCustodian)
        Me.LayoutControl1.Controls.Add(Me.cmbSupplier)
        Me.LayoutControl1.Controls.Add(Me.txtRemarks)
        Me.LayoutControl1.Controls.Add(Me.dtOrder)
        resources.ApplyResources(Me.LayoutControl1, "LayoutControl1")
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        '
        'txtQuotation
        '
        resources.ApplyResources(Me.txtQuotation, "txtQuotation")
        Me.txtQuotation.Name = "txtQuotation"
        Me.txtQuotation.StyleController = Me.LayoutControl1
        '
        'txtTotal
        '
        resources.ApplyResources(Me.txtTotal, "txtTotal")
        Me.txtTotal.MenuManager = Me.BarManager1
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Properties.Appearance.Options.UseTextOptions = True
        Me.txtTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.txtTotal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.txtTotal.Properties.ReadOnly = True
        Me.txtTotal.StyleController = Me.LayoutControl1
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.BarDockControl1)
        Me.BarManager1.DockControls.Add(Me.BarDockControl2)
        Me.BarManager1.DockControls.Add(Me.BarDockControl3)
        Me.BarManager1.DockControls.Add(Me.BarDockControl4)
        Me.BarManager1.Form = Me
        Me.BarManager1.MaxItemId = 0
        '
        'BarDockControl1
        '
        Me.BarDockControl1.CausesValidation = False
        '
        'BarDockControl2
        '
        Me.BarDockControl2.CausesValidation = False
        '
        'BarDockControl3
        '
        Me.BarDockControl3.CausesValidation = False
        '
        'BarDockControl4
        '
        Me.BarDockControl4.CausesValidation = False
        '
        'txtFreightCharge
        '
        resources.ApplyResources(Me.txtFreightCharge, "txtFreightCharge")
        Me.txtFreightCharge.MenuManager = Me.BarManager1
        Me.txtFreightCharge.Name = "txtFreightCharge"
        Me.txtFreightCharge.Properties.Appearance.Options.UseTextOptions = True
        Me.txtFreightCharge.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.txtFreightCharge.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.txtFreightCharge.StyleController = Me.LayoutControl1
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
        'txtPurchaseOrder
        '
        resources.ApplyResources(Me.txtPurchaseOrder, "txtPurchaseOrder")
        Me.txtPurchaseOrder.MenuManager = Me.BarManager1
        Me.txtPurchaseOrder.Name = "txtPurchaseOrder"
        Me.txtPurchaseOrder.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.txtPurchaseOrder.StyleController = Me.LayoutControl1
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
        Me.grvItems.OptionsView.ShowGroupPanel = False
        resources.ApplyResources(Me.grvItems, "grvItems")
        '
        'cmbCost
        '
        resources.ApplyResources(Me.cmbCost, "cmbCost")
        Me.cmbCost.Name = "cmbCost"
        Me.cmbCost.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbCost.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cmbCost.Properties.NullText = resources.GetString("cmbCost.Properties.NullText")
        Me.cmbCost.StyleController = Me.LayoutControl1
        '
        'dpApproveDate
        '
        Me.dpApproveDate.EditValue = Nothing
        resources.ApplyResources(Me.dpApproveDate, "dpApproveDate")
        Me.dpApproveDate.Name = "dpApproveDate"
        Me.dpApproveDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dpApproveDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dpApproveDate.Properties.ReadOnly = True
        Me.dpApproveDate.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.dpApproveDate.StyleController = Me.LayoutControl1
        '
        'txtPayment
        '
        resources.ApplyResources(Me.txtPayment, "txtPayment")
        Me.txtPayment.Name = "txtPayment"
        Me.txtPayment.StyleController = Me.LayoutControl1
        '
        'txtMode
        '
        resources.ApplyResources(Me.txtMode, "txtMode")
        Me.txtMode.Name = "txtMode"
        Me.txtMode.StyleController = Me.LayoutControl1
        '
        'txtReferenceNo
        '
        resources.ApplyResources(Me.txtReferenceNo, "txtReferenceNo")
        Me.txtReferenceNo.Name = "txtReferenceNo"
        Me.txtReferenceNo.StyleController = Me.LayoutControl1
        '
        'txtModeofDel
        '
        resources.ApplyResources(Me.txtModeofDel, "txtModeofDel")
        Me.txtModeofDel.Name = "txtModeofDel"
        Me.txtModeofDel.StyleController = Me.LayoutControl1
        '
        'lblStatus
        '
        Me.lblStatus.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblStatus.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStatus.Appearance.Options.UseFont = True
        Me.lblStatus.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblStatus, "lblStatus")
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.StyleController = Me.LayoutControl1
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
        'cmbCustodian
        '
        Me.cmbCustodian.btnAddDataVisible = False
        Me.cmbCustodian.DataSource = Nothing
        Me.cmbCustodian.DisplayMember = ""
        Me.cmbCustodian.HideValueMember = True
        resources.ApplyResources(Me.cmbCustodian, "cmbCustodian")
        Me.cmbCustodian.Name = "cmbCustodian"
        Me.cmbCustodian.SelectedText = ""
        Me.cmbCustodian.SelectedValue = Nothing
        Me.cmbCustodian.ValueMember = ""
        '
        'cmbSupplier
        '
        Me.cmbSupplier.btnAddDataVisible = False
        Me.cmbSupplier.DataSource = Nothing
        Me.cmbSupplier.DisplayMember = ""
        Me.cmbSupplier.HideValueMember = True
        resources.ApplyResources(Me.cmbSupplier, "cmbSupplier")
        Me.cmbSupplier.Name = "cmbSupplier"
        Me.cmbSupplier.SelectedText = ""
        Me.cmbSupplier.SelectedValue = Nothing
        Me.cmbSupplier.ValueMember = ""
        '
        'txtRemarks
        '
        resources.ApplyResources(Me.txtRemarks, "txtRemarks")
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.StyleController = Me.LayoutControl1
        '
        'dtOrder
        '
        Me.dtOrder.EditValue = Nothing
        resources.ApplyResources(Me.dtOrder, "dtOrder")
        Me.dtOrder.Name = "dtOrder"
        Me.dtOrder.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtOrder.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtOrder.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.dtOrder.StyleController = Me.LayoutControl1
        '
        'LayoutControlGroup1
        '
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem16, Me.LayoutControlItem1, Me.LayoutTabbedGroup, Me.LayoutSummary, Me.LayoutControlItem9, Me.LayoutControlItem2, Me.LayoutControlItem7, Me.LayoutControlItem10, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem15})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(723, 596)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.cmbCustodian
        resources.ApplyResources(Me.LayoutControlItem4, "LayoutControlItem4")
        Me.LayoutControlItem4.Location = New System.Drawing.Point(356, 93)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(361, 31)
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(81, 13)
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.Control = Me.cmbCost
        resources.ApplyResources(Me.LayoutControlItem16, "LayoutControlItem16")
        Me.LayoutControlItem16.Location = New System.Drawing.Point(356, 62)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(361, 31)
        Me.LayoutControlItem16.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(81, 13)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.txtPurchaseOrder
        resources.ApplyResources(Me.LayoutControlItem1, "LayoutControlItem1")
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(356, 31)
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(81, 13)
        '
        'LayoutTabbedGroup
        '
        resources.ApplyResources(Me.LayoutTabbedGroup, "LayoutTabbedGroup")
        Me.LayoutTabbedGroup.Location = New System.Drawing.Point(0, 124)
        Me.LayoutTabbedGroup.Name = "LayoutTabbedGroup"
        Me.LayoutTabbedGroup.SelectedTabPage = Me.LayoutControlGroup7
        Me.LayoutTabbedGroup.SelectedTabPageIndex = 1
        Me.LayoutTabbedGroup.Size = New System.Drawing.Size(717, 367)
        Me.LayoutTabbedGroup.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.LayoutControlGroup7})
        '
        'LayoutControlGroup7
        '
        resources.ApplyResources(Me.LayoutControlGroup7, "LayoutControlGroup7")
        Me.LayoutControlGroup7.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem14, Me.LayoutControlItem19, Me.LayoutControlItem12, Me.EmptySpaceItem3, Me.LayoutControlItem13})
        Me.LayoutControlGroup7.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup7.Name = "LayoutControlGroup7"
        Me.LayoutControlGroup7.Size = New System.Drawing.Size(696, 324)
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.Control = Me.txtPayment
        resources.ApplyResources(Me.LayoutControlItem14, "LayoutControlItem14")
        Me.LayoutControlItem14.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem14.MaxSize = New System.Drawing.Size(0, 42)
        Me.LayoutControlItem14.MinSize = New System.Drawing.Size(124, 42)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(312, 42)
        Me.LayoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem14.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(81, 13)
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.Control = Me.txtQuotation
        resources.ApplyResources(Me.LayoutControlItem19, "LayoutControlItem19")
        Me.LayoutControlItem19.Location = New System.Drawing.Point(0, 42)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(312, 43)
        Me.LayoutControlItem19.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(81, 13)
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.txtRemarks
        resources.ApplyResources(Me.LayoutControlItem12, "LayoutControlItem12")
        Me.LayoutControlItem12.Location = New System.Drawing.Point(312, 0)
        Me.LayoutControlItem12.MaxSize = New System.Drawing.Size(0, 43)
        Me.LayoutControlItem12.MinSize = New System.Drawing.Size(124, 43)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(384, 43)
        Me.LayoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem12.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(81, 13)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        resources.ApplyResources(Me.EmptySpaceItem3, "EmptySpaceItem3")
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(0, 85)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(696, 239)
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlGroup2
        '
        resources.ApplyResources(Me.LayoutControlGroup2, "LayoutControlGroup2")
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutgrdItems})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(696, 324)
        '
        'LayoutgrdItems
        '
        Me.LayoutgrdItems.Control = Me.grdItems
        resources.ApplyResources(Me.LayoutgrdItems, "LayoutgrdItems")
        Me.LayoutgrdItems.Location = New System.Drawing.Point(0, 0)
        Me.LayoutgrdItems.Name = "LayoutgrdItems"
        Me.LayoutgrdItems.Size = New System.Drawing.Size(696, 324)
        Me.LayoutgrdItems.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutgrdItems.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutgrdItems.TextToControlDistance = 0
        Me.LayoutgrdItems.TextVisible = False
        '
        'LayoutSummary
        '
        resources.ApplyResources(Me.LayoutSummary, "LayoutSummary")
        Me.LayoutSummary.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem8, Me.LayoutControlItem17, Me.LayoutControlItem18, Me.EmptySpaceItem1, Me.LayoutControlItem11, Me.LayoutControlItem6})
        Me.LayoutSummary.Location = New System.Drawing.Point(0, 491)
        Me.LayoutSummary.Name = "LayoutSummary"
        Me.LayoutSummary.Size = New System.Drawing.Size(717, 99)
        Me.LayoutSummary.TextVisible = False
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.txtTotalDiscount
        resources.ApplyResources(Me.LayoutControlItem8, "LayoutControlItem8")
        Me.LayoutControlItem8.Location = New System.Drawing.Point(320, 0)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(391, 31)
        Me.LayoutControlItem8.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(81, 13)
        '
        'LayoutControlItem17
        '
        Me.LayoutControlItem17.Control = Me.txtFreightCharge
        resources.ApplyResources(Me.LayoutControlItem17, "LayoutControlItem17")
        Me.LayoutControlItem17.Location = New System.Drawing.Point(320, 31)
        Me.LayoutControlItem17.Name = "LayoutControlItem17"
        Me.LayoutControlItem17.Size = New System.Drawing.Size(391, 31)
        Me.LayoutControlItem17.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem17.TextSize = New System.Drawing.Size(81, 13)
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.Control = Me.txtTotal
        resources.ApplyResources(Me.LayoutControlItem18, "LayoutControlItem18")
        Me.LayoutControlItem18.Location = New System.Drawing.Point(320, 62)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Size = New System.Drawing.Size(391, 31)
        Me.LayoutControlItem18.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(81, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        resources.ApplyResources(Me.EmptySpaceItem1, "EmptySpaceItem1")
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 62)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(320, 31)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.txtAddCharges
        resources.ApplyResources(Me.LayoutControlItem11, "LayoutControlItem11")
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 31)
        Me.LayoutControlItem11.MinSize = New System.Drawing.Size(149, 24)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(320, 31)
        Me.LayoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem11.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(81, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.txtSubTotal
        resources.ApplyResources(Me.LayoutControlItem6, "LayoutControlItem6")
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(320, 31)
        Me.LayoutControlItem6.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(81, 13)
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.Control = Me.dpApproveDate
        resources.ApplyResources(Me.LayoutControlItem15, "LayoutControlItem15")
        Me.LayoutControlItem15.Location = New System.Drawing.Point(0, 93)
        Me.LayoutControlItem15.MinSize = New System.Drawing.Size(157, 31)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(356, 31)
        Me.LayoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem15.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(81, 13)
        Me.LayoutControlItem15.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.txtModeofDel
        resources.ApplyResources(Me.LayoutControlItem9, "LayoutControlItem9")
        Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 31)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(356, 31)
        Me.LayoutControlItem9.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(81, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.dtOrder
        resources.ApplyResources(Me.LayoutControlItem2, "LayoutControlItem2")
        Me.LayoutControlItem2.Location = New System.Drawing.Point(356, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(0, 24)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(149, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(226, 31)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(81, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LayoutControlItem7.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem7.Control = Me.lblStatus
        resources.ApplyResources(Me.LayoutControlItem7, "LayoutControlItem7")
        Me.LayoutControlItem7.Location = New System.Drawing.Point(582, 0)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(135, 31)
        Me.LayoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem7.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(61, 13)
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.txtReferenceNo
        resources.ApplyResources(Me.LayoutControlItem10, "LayoutControlItem10")
        Me.LayoutControlItem10.Location = New System.Drawing.Point(356, 31)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(361, 31)
        Me.LayoutControlItem10.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(81, 13)
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.txtMode
        resources.ApplyResources(Me.LayoutControlItem13, "LayoutControlItem13")
        Me.LayoutControlItem13.Location = New System.Drawing.Point(312, 43)
        Me.LayoutControlItem13.MaxSize = New System.Drawing.Size(0, 42)
        Me.LayoutControlItem13.MinSize = New System.Drawing.Size(124, 42)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(384, 42)
        Me.LayoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem13.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(81, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.cmbSupplier
        resources.ApplyResources(Me.LayoutControlItem3, "LayoutControlItem3")
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 62)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(356, 31)
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(81, 13)
        '
        'ctlPurchaseOrder
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.BarDockControl3)
        Me.Controls.Add(Me.BarDockControl4)
        Me.Controls.Add(Me.BarDockControl2)
        Me.Controls.Add(Me.BarDockControl1)
        Me.Name = "ctlPurchaseOrder"
        Me.Controls.SetChildIndex(Me.BarDockControl1, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl2, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl4, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl3, 0)
        Me.Controls.SetChildIndex(Me.LayoutControl1, 0)
        CType(Me.errProv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.valProvMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbListRep, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.txtQuotation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFreightCharge.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalDiscount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPurchaseOrder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCost.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dpApproveDate.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dpApproveDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPayment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReferenceNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtModeofDel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddCharges.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtOrder.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtOrder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutTabbedGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutgrdItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents cmbSupplier As ZulLib.ctlLov
    Friend WithEvents dtOrder As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cmbCustodian As ZulLib.ctlLov
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lblStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtModeofDel As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtReferenceNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtMode As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtRemarks As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtAddCharges As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtPayment As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents dpApproveDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cmbCost As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdItems As ZulLib.ctlGrid
    Friend WithEvents grvItems As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl2 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl3 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl4 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents txtPurchaseOrder As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtTotalDiscount As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtSubTotal As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtTotal As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtFreightCharge As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents txtQuotation As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutTabbedGroup As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutgrdItems As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutSummary As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup7 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem

End Class
