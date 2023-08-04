<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmABBReports
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmABBReports))
        Me.imgListLarge = New DevExpress.Utils.ImageCollection(Me.components)
        Me.nvControl = New DevExpress.XtraNavBar.NavBarControl
        Me.nvGroupStandard = New DevExpress.XtraNavBar.NavBarGroup
        Me.nvItemCompanyAsset = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemPhysicalInventory = New DevExpress.XtraNavBar.NavBarItem
        Me.nvFoundAssets = New DevExpress.XtraNavBar.NavBarItem
        Me.nvMissingAssets = New DevExpress.XtraNavBar.NavBarItem
        Me.nvTransferedAssets = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemMisplaced = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemAnonymous = New DevExpress.XtraNavBar.NavBarItem
        Me.nvAssetswithvlaue = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemDisposed = New DevExpress.XtraNavBar.NavBarItem
        Me.ntAssetLog = New DevExpress.XtraNavBar.NavBarItem
        Me.nvInventoryReports = New DevExpress.XtraNavBar.NavBarGroup
        Me.nvGoodsReciveDetail = New DevExpress.XtraNavBar.NavBarItem
        Me.nvGoodsReceiveStanderd = New DevExpress.XtraNavBar.NavBarItem
        Me.nvIssuanceReport = New DevExpress.XtraNavBar.NavBarItem
        Me.nvReversal = New DevExpress.XtraNavBar.NavBarItem
        Me.nvVendorReturn = New DevExpress.XtraNavBar.NavBarItem
        Me.nvWarrantyClaim = New DevExpress.XtraNavBar.NavBarItem
        Me.nvWarrantyRecevingSame = New DevExpress.XtraNavBar.NavBarItem
        Me.nvWarrantyRecevingReplace = New DevExpress.XtraNavBar.NavBarItem
        Me.NavBarItem4 = New DevExpress.XtraNavBar.NavBarItem
        Me.tabControl = New DevExpress.XtraTab.XtraTabControl
        Me.tabMainFilter = New DevExpress.XtraTab.XtraTabPage
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.chkFilterByCapitalize = New System.Windows.Forms.CheckBox
        Me.dtFromCapitalize = New System.Windows.Forms.DateTimePicker
        Me.dtToCapitalize = New System.Windows.Forms.DateTimePicker
        Me.Label13 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnGen = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chkHideReportFooter = New System.Windows.Forms.CheckBox
        Me.chkHideReportHeader = New System.Windows.Forms.CheckBox
        Me.chkExcludeCapitalize = New System.Windows.Forms.CheckBox
        Me.chkExcludeAssetValue = New System.Windows.Forms.CheckBox
        Me.chkExcludeRetired = New System.Windows.Forms.CheckBox
        Me.cmbSch = New ZulLOV.ZulLOV
        Me.lblInventory = New System.Windows.Forms.Label
        Me.chkShowOnlyCapitalization = New System.Windows.Forms.CheckBox
        Me.chkShowOnlyAssetWithValue = New System.Windows.Forms.CheckBox
        Me.chkShowRetired = New System.Windows.Forms.CheckBox
        Me.chkShowSubLevels = New System.Windows.Forms.CheckBox
        Me.lblDataSource = New System.Windows.Forms.Label
        Me.cmbDataSource = New System.Windows.Forms.ComboBox
        Me.grpRetirement = New System.Windows.Forms.GroupBox
        Me.chkFilterByDisposeDate = New System.Windows.Forms.CheckBox
        Me.dtFromDisposal = New System.Windows.Forms.DateTimePicker
        Me.dtToDisposal = New System.Windows.Forms.DateTimePicker
        Me.Label4 = New System.Windows.Forms.Label
        Me.grpFilterByData = New System.Windows.Forms.GroupBox
        Me.txtGroup4 = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtGroup3 = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtGroup2 = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtToAsetNumber = New System.Windows.Forms.TextBox
        Me.txtFromAssetNumber = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtGroup1 = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtBusinessArea = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtInvestmentNo = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.grpLocation = New System.Windows.Forms.GroupBox
        Me.trvLocation = New ZulTree.ZulTree
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtPlant = New DevExpress.XtraEditors.TextEdit
        Me.txtLocation = New DevExpress.XtraEditors.TextEdit
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbComp = New ZulLOV.ZulLOV
        Me.lblCompany = New System.Windows.Forms.Label
        Me.lblBook = New System.Windows.Forms.Label
        Me.cmbCust = New ZulLOV.ZulLOV
        Me.lblCust = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.cmbCostCenter = New ZulLOV.ZulLOV
        Me.Label1 = New System.Windows.Forms.Label
        Me.trvCategory = New ZulTree.ZulTree
        Me.grpFilterCreation = New System.Windows.Forms.GroupBox
        Me.chkFilterByCreation = New System.Windows.Forms.CheckBox
        Me.dtFromCreation = New System.Windows.Forms.DateTimePicker
        Me.dtToCreation = New System.Windows.Forms.DateTimePicker
        Me.Label8 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lblReportName = New DevExpress.XtraEditors.LabelControl
        Me.tabPreviewReport = New DevExpress.XtraTab.XtraTabPage
        Me.PreviewControl1 = New ABBIntegration.PreviewControl
        Me.tabAnonymousFilter = New DevExpress.XtraTab.XtraTabPage
        Me.imgAuditStatus = New System.Windows.Forms.PictureBox
        Me.lblAuditStatus = New DevExpress.XtraEditors.LabelControl
        Me.btnCloseAnon = New System.Windows.Forms.Button
        Me.btnGenerateAnon = New System.Windows.Forms.Button
        Me.grpInventory = New System.Windows.Forms.GroupBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.cmbSchAnon = New ZulLOV.ZulLOV
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblEnd = New DevExpress.XtraEditors.TextEdit
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblStart = New DevExpress.XtraEditors.TextEdit
        Me.tabGRFilter = New DevExpress.XtraTab.XtraTabPage
        Me.txtReceiveSAPMatNo = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.chkReceiveDate = New System.Windows.Forms.CheckBox
        Me.dtReceiveFrom = New System.Windows.Forms.DateTimePicker
        Me.dtReceiveTo = New System.Windows.Forms.DateTimePicker
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtReceiveManPartNo = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtReceiveSerialNo = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.txtReceivePOTo = New System.Windows.Forms.TextBox
        Me.txtReceivePOFrom = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnGenerateReceive = New System.Windows.Forms.Button
        Me.imgReceiving = New System.Windows.Forms.PictureBox
        Me.lblReciving = New DevExpress.XtraEditors.LabelControl
        Me.tabGIFilter = New DevExpress.XtraTab.XtraTabPage
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.Label31 = New System.Windows.Forms.Label
        Me.txtDocNo = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.txtIssueSerialNumber = New System.Windows.Forms.TextBox
        Me.txtIssueAssetNo = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.lblAssetNumber = New System.Windows.Forms.Label
        Me.txtIssueManPartNo = New System.Windows.Forms.TextBox
        Me.txtIssueSAPMat = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.grpIssuanceFilter = New System.Windows.Forms.GroupBox
        Me.rdoIssueTypePOR = New System.Windows.Forms.RadioButton
        Me.rdoIssueTypeInvPr = New System.Windows.Forms.RadioButton
        Me.rdoIssueTypeBoth = New System.Windows.Forms.RadioButton
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.chkIssueDate = New System.Windows.Forms.CheckBox
        Me.dtIssueFrom = New System.Windows.Forms.DateTimePicker
        Me.dtIssueTo = New System.Windows.Forms.DateTimePicker
        Me.Label27 = New System.Windows.Forms.Label
        Me.Button3 = New System.Windows.Forms.Button
        Me.btnGenerateIssuance = New System.Windows.Forms.Button
        Me.imgIssuance = New System.Windows.Forms.PictureBox
        Me.lblIssuance = New DevExpress.XtraEditors.LabelControl
        CType(Me.imgListLarge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nvControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControl.SuspendLayout()
        Me.tabMainFilter.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grpRetirement.SuspendLayout()
        Me.grpFilterByData.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpLocation.SuspendLayout()
        CType(Me.txtPlant.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFilterCreation.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPreviewReport.SuspendLayout()
        Me.tabAnonymousFilter.SuspendLayout()
        CType(Me.imgAuditStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpInventory.SuspendLayout()
        CType(Me.lblEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabGRFilter.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.imgReceiving, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabGIFilter.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.grpIssuanceFilter.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.imgIssuance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'imgListLarge
        '
        Me.imgListLarge.ImageSize = New System.Drawing.Size(24, 24)
        Me.imgListLarge.ImageStream = CType(resources.GetObject("imgListLarge.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.imgListLarge.Images.SetKeyName(0, "BO_Appointment_Large.png")
        Me.imgListLarge.Images.SetKeyName(1, "calendar.png")
        Me.imgListLarge.Images.SetKeyName(2, "id_card_view.png")
        Me.imgListLarge.Images.SetKeyName(3, "preferences.png")
        Me.imgListLarge.Images.SetKeyName(4, "document_chart.png")
        '
        'nvControl
        '
        Me.nvControl.ActiveGroup = Me.nvGroupStandard
        Me.nvControl.AllowSelectedLink = True
        Me.nvControl.Appearance.Item.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nvControl.Appearance.Item.Options.UseFont = True
        Me.nvControl.ContentButtonHint = Nothing
        Me.nvControl.Dock = System.Windows.Forms.DockStyle.Left
        Me.nvControl.Groups.AddRange(New DevExpress.XtraNavBar.NavBarGroup() {Me.nvGroupStandard, Me.nvInventoryReports})
        Me.nvControl.Items.AddRange(New DevExpress.XtraNavBar.NavBarItem() {Me.nvItemCompanyAsset, Me.nvItemAnonymous, Me.NavBarItem4, Me.nvAssetswithvlaue, Me.nvItemDisposed, Me.nvItemPhysicalInventory, Me.nvItemMisplaced, Me.nvMissingAssets, Me.nvFoundAssets, Me.nvTransferedAssets, Me.nvGoodsReciveDetail, Me.nvGoodsReceiveStanderd, Me.nvIssuanceReport, Me.nvReversal, Me.nvVendorReturn, Me.nvWarrantyClaim, Me.nvWarrantyRecevingSame, Me.nvWarrantyRecevingReplace, Me.ntAssetLog})
        Me.nvControl.Location = New System.Drawing.Point(0, 0)
        Me.nvControl.Name = "nvControl"
        Me.nvControl.OptionsNavPane.ExpandedWidth = 140
        Me.nvControl.Size = New System.Drawing.Size(210, 580)
        Me.nvControl.SmallImages = Me.imgListLarge
        Me.nvControl.StoreDefaultPaintStyleName = True
        Me.nvControl.TabIndex = 78
        Me.nvControl.Text = "Select Report"
        '
        'nvGroupStandard
        '
        Me.nvGroupStandard.Caption = "Standard Reports"
        Me.nvGroupStandard.Expanded = True
        Me.nvGroupStandard.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.SmallIconsText
        Me.nvGroupStandard.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemCompanyAsset), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemPhysicalInventory), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvFoundAssets), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvMissingAssets), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvTransferedAssets), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemMisplaced), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemAnonymous), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvAssetswithvlaue), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemDisposed), New DevExpress.XtraNavBar.NavBarItemLink(Me.ntAssetLog)})
        Me.nvGroupStandard.Name = "nvGroupStandard"
        Me.nvGroupStandard.SelectedLinkIndex = 9
        '
        'nvItemCompanyAsset
        '
        Me.nvItemCompanyAsset.Caption = "Company Assets"
        Me.nvItemCompanyAsset.Name = "nvItemCompanyAsset"
        Me.nvItemCompanyAsset.SmallImageIndex = 3
        '
        'nvItemPhysicalInventory
        '
        Me.nvItemPhysicalInventory.Caption = "Physical Inventory"
        Me.nvItemPhysicalInventory.Name = "nvItemPhysicalInventory"
        Me.nvItemPhysicalInventory.SmallImageIndex = 3
        '
        'nvFoundAssets
        '
        Me.nvFoundAssets.Caption = "Found Assets"
        Me.nvFoundAssets.Name = "nvFoundAssets"
        Me.nvFoundAssets.SmallImageIndex = 3
        '
        'nvMissingAssets
        '
        Me.nvMissingAssets.Caption = "Missing Assets"
        Me.nvMissingAssets.Name = "nvMissingAssets"
        Me.nvMissingAssets.SmallImageIndex = 3
        '
        'nvTransferedAssets
        '
        Me.nvTransferedAssets.Caption = "Transferred Assets"
        Me.nvTransferedAssets.Name = "nvTransferedAssets"
        Me.nvTransferedAssets.SmallImageIndex = 3
        '
        'nvItemMisplaced
        '
        Me.nvItemMisplaced.Caption = "Misplaced Assets"
        Me.nvItemMisplaced.Name = "nvItemMisplaced"
        Me.nvItemMisplaced.SmallImageIndex = 3
        '
        'nvItemAnonymous
        '
        Me.nvItemAnonymous.Caption = "Anonymous Assets"
        Me.nvItemAnonymous.LargeImageIndex = 3
        Me.nvItemAnonymous.Name = "nvItemAnonymous"
        Me.nvItemAnonymous.SmallImageIndex = 3
        '
        'nvAssetswithvlaue
        '
        Me.nvAssetswithvlaue.Caption = "Assets with vlaue"
        Me.nvAssetswithvlaue.Name = "nvAssetswithvlaue"
        Me.nvAssetswithvlaue.SmallImageIndex = 3
        '
        'nvItemDisposed
        '
        Me.nvItemDisposed.Caption = "Asset Retirement"
        Me.nvItemDisposed.Name = "nvItemDisposed"
        Me.nvItemDisposed.SmallImageIndex = 3
        '
        'ntAssetLog
        '
        Me.ntAssetLog.Caption = "Assets Log"
        Me.ntAssetLog.Name = "ntAssetLog"
        Me.ntAssetLog.SmallImageIndex = 3
        '
        'nvInventoryReports
        '
        Me.nvInventoryReports.Caption = "Inventory Reports"
        Me.nvInventoryReports.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nvGoodsReciveDetail), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvGoodsReceiveStanderd), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvIssuanceReport), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvReversal), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvVendorReturn), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvWarrantyClaim), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvWarrantyRecevingSame), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvWarrantyRecevingReplace)})
        Me.nvInventoryReports.Name = "nvInventoryReports"
        '
        'nvGoodsReciveDetail
        '
        Me.nvGoodsReciveDetail.Caption = "Goods Receive Detail"
        Me.nvGoodsReciveDetail.Name = "nvGoodsReciveDetail"
        Me.nvGoodsReciveDetail.SmallImageIndex = 4
        '
        'nvGoodsReceiveStanderd
        '
        Me.nvGoodsReceiveStanderd.Caption = "Goods Receive Standard"
        Me.nvGoodsReceiveStanderd.Name = "nvGoodsReceiveStanderd"
        Me.nvGoodsReceiveStanderd.SmallImageIndex = 4
        '
        'nvIssuanceReport
        '
        Me.nvIssuanceReport.Caption = "Goods Issuance"
        Me.nvIssuanceReport.Name = "nvIssuanceReport"
        Me.nvIssuanceReport.SmallImageIndex = 4
        '
        'nvReversal
        '
        Me.nvReversal.Caption = "Reversal GI/GR"
        Me.nvReversal.Name = "nvReversal"
        Me.nvReversal.SmallImageIndex = 4
        '
        'nvVendorReturn
        '
        Me.nvVendorReturn.Caption = "Vendor Return"
        Me.nvVendorReturn.Name = "nvVendorReturn"
        Me.nvVendorReturn.SmallImageIndex = 4
        '
        'nvWarrantyClaim
        '
        Me.nvWarrantyClaim.Caption = "Warranty Claim"
        Me.nvWarrantyClaim.Name = "nvWarrantyClaim"
        Me.nvWarrantyClaim.SmallImageIndex = 4
        '
        'nvWarrantyRecevingSame
        '
        Me.nvWarrantyRecevingSame.Caption = "Warranty Receving Same"
        Me.nvWarrantyRecevingSame.Name = "nvWarrantyRecevingSame"
        Me.nvWarrantyRecevingSame.SmallImageIndex = 4
        '
        'nvWarrantyRecevingReplace
        '
        Me.nvWarrantyRecevingReplace.Caption = "Warranty Receving Replace"
        Me.nvWarrantyRecevingReplace.Name = "nvWarrantyRecevingReplace"
        Me.nvWarrantyRecevingReplace.SmallImageIndex = 4
        '
        'NavBarItem4
        '
        Me.NavBarItem4.Caption = "NavBarItem4"
        Me.NavBarItem4.Name = "NavBarItem4"
        '
        'tabControl
        '
        Me.tabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabControl.Location = New System.Drawing.Point(210, 0)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedTabPage = Me.tabMainFilter
        Me.tabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[True]
        Me.tabControl.Size = New System.Drawing.Size(572, 580)
        Me.tabControl.TabIndex = 79
        Me.tabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabMainFilter, Me.tabPreviewReport, Me.tabAnonymousFilter, Me.tabGRFilter, Me.tabGIFilter})
        '
        'tabMainFilter
        '
        Me.tabMainFilter.Controls.Add(Me.GroupBox3)
        Me.tabMainFilter.Controls.Add(Me.btnExit)
        Me.tabMainFilter.Controls.Add(Me.btnGen)
        Me.tabMainFilter.Controls.Add(Me.btnClear)
        Me.tabMainFilter.Controls.Add(Me.GroupBox2)
        Me.tabMainFilter.Controls.Add(Me.grpRetirement)
        Me.tabMainFilter.Controls.Add(Me.grpFilterByData)
        Me.tabMainFilter.Controls.Add(Me.grpFilterCreation)
        Me.tabMainFilter.Controls.Add(Me.PictureBox1)
        Me.tabMainFilter.Controls.Add(Me.lblReportName)
        Me.tabMainFilter.Name = "tabMainFilter"
        Me.tabMainFilter.Size = New System.Drawing.Size(563, 549)
        Me.tabMainFilter.Text = "Filter"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.White
        Me.GroupBox3.Controls.Add(Me.chkFilterByCapitalize)
        Me.GroupBox3.Controls.Add(Me.dtFromCapitalize)
        Me.GroupBox3.Controls.Add(Me.dtToCapitalize)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Location = New System.Drawing.Point(5, 297)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(552, 43)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Filter by Capitalization"
        '
        'chkFilterByCapitalize
        '
        Me.chkFilterByCapitalize.AutoSize = True
        Me.chkFilterByCapitalize.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFilterByCapitalize.Location = New System.Drawing.Point(135, 19)
        Me.chkFilterByCapitalize.Name = "chkFilterByCapitalize"
        Me.chkFilterByCapitalize.Size = New System.Drawing.Size(55, 17)
        Me.chkFilterByCapitalize.TabIndex = 72
        Me.chkFilterByCapitalize.Text = "From"
        Me.chkFilterByCapitalize.UseVisualStyleBackColor = True
        '
        'dtFromCapitalize
        '
        Me.dtFromCapitalize.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFromCapitalize.Location = New System.Drawing.Point(196, 16)
        Me.dtFromCapitalize.Name = "dtFromCapitalize"
        Me.dtFromCapitalize.Size = New System.Drawing.Size(121, 20)
        Me.dtFromCapitalize.TabIndex = 0
        '
        'dtToCapitalize
        '
        Me.dtToCapitalize.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtToCapitalize.Location = New System.Drawing.Point(394, 16)
        Me.dtToCapitalize.Name = "dtToCapitalize"
        Me.dtToCapitalize.Size = New System.Drawing.Size(121, 20)
        Me.dtToCapitalize.TabIndex = 1
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(364, 20)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(21, 13)
        Me.Label13.TabIndex = 71
        Me.Label13.Text = "To"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Image = Global.ABBIntegration.My.Resources.Resources.Close16x16
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(466, 521)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(84, 27)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "&Close"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnGen
        '
        Me.btnGen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGen.Image = Global.ABBIntegration.My.Resources.Resources.Import
        Me.btnGen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGen.Location = New System.Drawing.Point(306, 521)
        Me.btnGen.Name = "btnGen"
        Me.btnGen.Size = New System.Drawing.Size(142, 27)
        Me.btnGen.TabIndex = 7
        Me.btnGen.Text = "&Generate Report"
        Me.btnGen.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Image = Global.ABBIntegration.My.Resources.Resources.MenuBar_Clear
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.Location = New System.Drawing.Point(167, 521)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(115, 27)
        Me.btnClear.TabIndex = 6
        Me.btnClear.Text = "&Clear Form"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.White
        Me.GroupBox2.Controls.Add(Me.chkHideReportFooter)
        Me.GroupBox2.Controls.Add(Me.chkHideReportHeader)
        Me.GroupBox2.Controls.Add(Me.chkExcludeCapitalize)
        Me.GroupBox2.Controls.Add(Me.chkExcludeAssetValue)
        Me.GroupBox2.Controls.Add(Me.chkExcludeRetired)
        Me.GroupBox2.Controls.Add(Me.cmbSch)
        Me.GroupBox2.Controls.Add(Me.lblInventory)
        Me.GroupBox2.Controls.Add(Me.chkShowOnlyCapitalization)
        Me.GroupBox2.Controls.Add(Me.chkShowOnlyAssetWithValue)
        Me.GroupBox2.Controls.Add(Me.chkShowRetired)
        Me.GroupBox2.Controls.Add(Me.chkShowSubLevels)
        Me.GroupBox2.Controls.Add(Me.lblDataSource)
        Me.GroupBox2.Controls.Add(Me.cmbDataSource)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 388)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(552, 131)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Filter Options"
        '
        'chkHideReportFooter
        '
        Me.chkHideReportFooter.AutoSize = True
        Me.chkHideReportFooter.Location = New System.Drawing.Point(421, 37)
        Me.chkHideReportFooter.Name = "chkHideReportFooter"
        Me.chkHideReportFooter.Size = New System.Drawing.Size(118, 17)
        Me.chkHideReportFooter.TabIndex = 87
        Me.chkHideReportFooter.Text = "Hide Report Footer"
        Me.chkHideReportFooter.UseVisualStyleBackColor = True
        '
        'chkHideReportHeader
        '
        Me.chkHideReportHeader.AutoSize = True
        Me.chkHideReportHeader.Location = New System.Drawing.Point(421, 19)
        Me.chkHideReportHeader.Name = "chkHideReportHeader"
        Me.chkHideReportHeader.Size = New System.Drawing.Size(121, 17)
        Me.chkHideReportHeader.TabIndex = 86
        Me.chkHideReportHeader.Text = "Hide Report Header"
        Me.chkHideReportHeader.UseVisualStyleBackColor = True
        '
        'chkExcludeCapitalize
        '
        Me.chkExcludeCapitalize.AutoSize = True
        Me.chkExcludeCapitalize.Location = New System.Drawing.Point(218, 37)
        Me.chkExcludeCapitalize.Name = "chkExcludeCapitalize"
        Me.chkExcludeCapitalize.Size = New System.Drawing.Size(150, 17)
        Me.chkExcludeCapitalize.TabIndex = 5
        Me.chkExcludeCapitalize.Text = "Exclude Capitaliztion Date"
        Me.chkExcludeCapitalize.UseVisualStyleBackColor = True
        '
        'chkExcludeAssetValue
        '
        Me.chkExcludeAssetValue.AutoSize = True
        Me.chkExcludeAssetValue.Location = New System.Drawing.Point(218, 19)
        Me.chkExcludeAssetValue.Name = "chkExcludeAssetValue"
        Me.chkExcludeAssetValue.Size = New System.Drawing.Size(145, 17)
        Me.chkExcludeAssetValue.TabIndex = 1
        Me.chkExcludeAssetValue.Text = "Exclude Asset with value"
        Me.chkExcludeAssetValue.UseVisualStyleBackColor = True
        '
        'chkExcludeRetired
        '
        Me.chkExcludeRetired.AutoSize = True
        Me.chkExcludeRetired.Location = New System.Drawing.Point(218, 55)
        Me.chkExcludeRetired.Name = "chkExcludeRetired"
        Me.chkExcludeRetired.Size = New System.Drawing.Size(136, 17)
        Me.chkExcludeRetired.TabIndex = 3
        Me.chkExcludeRetired.Text = "Exclude Retired Assets"
        Me.chkExcludeRetired.UseVisualStyleBackColor = True
        '
        'cmbSch
        '
        Me.cmbSch.BackColor = System.Drawing.Color.White
        Me.cmbSch.DataSource = Nothing
        Me.cmbSch.DisplayMember = ""
        Me.cmbSch.Location = New System.Drawing.Point(329, 102)
        Me.cmbSch.Name = "cmbSch"
        Me.cmbSch.SelectedIndex = -1
        Me.cmbSch.SelectedText = ""
        Me.cmbSch.SelectedValue = ""
        Me.cmbSch.Size = New System.Drawing.Size(200, 23)
        Me.cmbSch.TabIndex = 8
        Me.cmbSch.TextReadOnly = False
        Me.cmbSch.ValueMember = ""
        '
        'lblInventory
        '
        Me.lblInventory.AutoSize = True
        Me.lblInventory.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInventory.Location = New System.Drawing.Point(212, 108)
        Me.lblInventory.Name = "lblInventory"
        Me.lblInventory.Size = New System.Drawing.Size(118, 13)
        Me.lblInventory.TabIndex = 85
        Me.lblInventory.Text = "Inventory Schedule"
        '
        'chkShowOnlyCapitalization
        '
        Me.chkShowOnlyCapitalization.AutoSize = True
        Me.chkShowOnlyCapitalization.Location = New System.Drawing.Point(8, 37)
        Me.chkShowOnlyCapitalization.Name = "chkShowOnlyCapitalization"
        Me.chkShowOnlyCapitalization.Size = New System.Drawing.Size(164, 17)
        Me.chkShowOnlyCapitalization.TabIndex = 4
        Me.chkShowOnlyCapitalization.Text = "Show Only Capitaliztion Date"
        Me.chkShowOnlyCapitalization.UseVisualStyleBackColor = True
        '
        'chkShowOnlyAssetWithValue
        '
        Me.chkShowOnlyAssetWithValue.AutoSize = True
        Me.chkShowOnlyAssetWithValue.Location = New System.Drawing.Point(8, 19)
        Me.chkShowOnlyAssetWithValue.Name = "chkShowOnlyAssetWithValue"
        Me.chkShowOnlyAssetWithValue.Size = New System.Drawing.Size(159, 17)
        Me.chkShowOnlyAssetWithValue.TabIndex = 0
        Me.chkShowOnlyAssetWithValue.Text = "Show Only Asset with value"
        Me.chkShowOnlyAssetWithValue.UseVisualStyleBackColor = True
        '
        'chkShowRetired
        '
        Me.chkShowRetired.AutoSize = True
        Me.chkShowRetired.Location = New System.Drawing.Point(8, 55)
        Me.chkShowRetired.Name = "chkShowRetired"
        Me.chkShowRetired.Size = New System.Drawing.Size(150, 17)
        Me.chkShowRetired.TabIndex = 2
        Me.chkShowRetired.Text = "Show Only Retired Assets"
        Me.chkShowRetired.UseVisualStyleBackColor = True
        '
        'chkShowSubLevels
        '
        Me.chkShowSubLevels.AutoSize = True
        Me.chkShowSubLevels.Location = New System.Drawing.Point(8, 79)
        Me.chkShowSubLevels.Name = "chkShowSubLevels"
        Me.chkShowSubLevels.Size = New System.Drawing.Size(244, 17)
        Me.chkShowSubLevels.TabIndex = 6
        Me.chkShowSubLevels.Text = "Include Sub  Levels of Locations && Categories"
        Me.chkShowSubLevels.UseVisualStyleBackColor = True
        '
        'lblDataSource
        '
        Me.lblDataSource.AutoSize = True
        Me.lblDataSource.Location = New System.Drawing.Point(6, 107)
        Me.lblDataSource.Name = "lblDataSource"
        Me.lblDataSource.Size = New System.Drawing.Size(66, 13)
        Me.lblDataSource.TabIndex = 83
        Me.lblDataSource.Text = "Data Source"
        '
        'cmbDataSource
        '
        Me.cmbDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDataSource.FormattingEnabled = True
        Me.cmbDataSource.Items.AddRange(New Object() {"ALL", "ZulAssets", "SAP"})
        Me.cmbDataSource.Location = New System.Drawing.Point(84, 104)
        Me.cmbDataSource.Name = "cmbDataSource"
        Me.cmbDataSource.Size = New System.Drawing.Size(121, 21)
        Me.cmbDataSource.TabIndex = 7
        '
        'grpRetirement
        '
        Me.grpRetirement.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpRetirement.BackColor = System.Drawing.Color.White
        Me.grpRetirement.Controls.Add(Me.chkFilterByDisposeDate)
        Me.grpRetirement.Controls.Add(Me.dtFromDisposal)
        Me.grpRetirement.Controls.Add(Me.dtToDisposal)
        Me.grpRetirement.Controls.Add(Me.Label4)
        Me.grpRetirement.Location = New System.Drawing.Point(4, 342)
        Me.grpRetirement.Name = "grpRetirement"
        Me.grpRetirement.Size = New System.Drawing.Size(552, 43)
        Me.grpRetirement.TabIndex = 3
        Me.grpRetirement.TabStop = False
        Me.grpRetirement.Text = "Filter By Retirement Date"
        '
        'chkFilterByDisposeDate
        '
        Me.chkFilterByDisposeDate.AutoSize = True
        Me.chkFilterByDisposeDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFilterByDisposeDate.Location = New System.Drawing.Point(135, 18)
        Me.chkFilterByDisposeDate.Name = "chkFilterByDisposeDate"
        Me.chkFilterByDisposeDate.Size = New System.Drawing.Size(55, 17)
        Me.chkFilterByDisposeDate.TabIndex = 108
        Me.chkFilterByDisposeDate.Text = "From"
        Me.chkFilterByDisposeDate.UseVisualStyleBackColor = True
        '
        'dtFromDisposal
        '
        Me.dtFromDisposal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFromDisposal.Location = New System.Drawing.Point(196, 15)
        Me.dtFromDisposal.Name = "dtFromDisposal"
        Me.dtFromDisposal.Size = New System.Drawing.Size(121, 20)
        Me.dtFromDisposal.TabIndex = 0
        '
        'dtToDisposal
        '
        Me.dtToDisposal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtToDisposal.Location = New System.Drawing.Point(394, 15)
        Me.dtToDisposal.Name = "dtToDisposal"
        Me.dtToDisposal.Size = New System.Drawing.Size(121, 20)
        Me.dtToDisposal.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(364, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(21, 13)
        Me.Label4.TabIndex = 107
        Me.Label4.Text = "To"
        '
        'grpFilterByData
        '
        Me.grpFilterByData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpFilterByData.BackColor = System.Drawing.Color.White
        Me.grpFilterByData.Controls.Add(Me.txtGroup4)
        Me.grpFilterByData.Controls.Add(Me.Label17)
        Me.grpFilterByData.Controls.Add(Me.txtGroup3)
        Me.grpFilterByData.Controls.Add(Me.Label18)
        Me.grpFilterByData.Controls.Add(Me.txtGroup2)
        Me.grpFilterByData.Controls.Add(Me.GroupBox1)
        Me.grpFilterByData.Controls.Add(Me.Label12)
        Me.grpFilterByData.Controls.Add(Me.Label16)
        Me.grpFilterByData.Controls.Add(Me.txtGroup1)
        Me.grpFilterByData.Controls.Add(Me.Label15)
        Me.grpFilterByData.Controls.Add(Me.txtBusinessArea)
        Me.grpFilterByData.Controls.Add(Me.Label14)
        Me.grpFilterByData.Controls.Add(Me.txtInvestmentNo)
        Me.grpFilterByData.Controls.Add(Me.Label5)
        Me.grpFilterByData.Controls.Add(Me.grpLocation)
        Me.grpFilterByData.Controls.Add(Me.cmbComp)
        Me.grpFilterByData.Controls.Add(Me.lblCompany)
        Me.grpFilterByData.Controls.Add(Me.lblBook)
        Me.grpFilterByData.Controls.Add(Me.cmbCust)
        Me.grpFilterByData.Controls.Add(Me.lblCust)
        Me.grpFilterByData.Controls.Add(Me.Label19)
        Me.grpFilterByData.Controls.Add(Me.cmbCostCenter)
        Me.grpFilterByData.Controls.Add(Me.Label1)
        Me.grpFilterByData.Controls.Add(Me.trvCategory)
        Me.grpFilterByData.Location = New System.Drawing.Point(4, 30)
        Me.grpFilterByData.Name = "grpFilterByData"
        Me.grpFilterByData.Size = New System.Drawing.Size(552, 222)
        Me.grpFilterByData.TabIndex = 0
        Me.grpFilterByData.TabStop = False
        Me.grpFilterByData.Text = "Filter by Data"
        '
        'txtGroup4
        '
        Me.txtGroup4.Location = New System.Drawing.Point(85, 197)
        Me.txtGroup4.Name = "txtGroup4"
        Me.txtGroup4.Size = New System.Drawing.Size(168, 20)
        Me.txtGroup4.TabIndex = 10
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(6, 200)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(57, 13)
        Me.Label17.TabIndex = 98
        Me.Label17.Text = "Ev Group4"
        '
        'txtGroup3
        '
        Me.txtGroup3.Location = New System.Drawing.Point(330, 197)
        Me.txtGroup3.Name = "txtGroup3"
        Me.txtGroup3.Size = New System.Drawing.Size(215, 20)
        Me.txtGroup3.TabIndex = 11
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(254, 200)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(57, 13)
        Me.Label18.TabIndex = 96
        Me.Label18.Text = "Ev Group3"
        '
        'txtGroup2
        '
        Me.txtGroup2.Location = New System.Drawing.Point(330, 171)
        Me.txtGroup2.Name = "txtGroup2"
        Me.txtGroup2.Size = New System.Drawing.Size(215, 20)
        Me.txtGroup2.TabIndex = 9
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtToAsetNumber)
        Me.GroupBox1.Controls.Add(Me.txtFromAssetNumber)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Location = New System.Drawing.Point(330, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(215, 62)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'txtToAsetNumber
        '
        Me.txtToAsetNumber.Location = New System.Drawing.Point(54, 36)
        Me.txtToAsetNumber.Name = "txtToAsetNumber"
        Me.txtToAsetNumber.Size = New System.Drawing.Size(154, 20)
        Me.txtToAsetNumber.TabIndex = 94
        '
        'txtFromAssetNumber
        '
        Me.txtFromAssetNumber.Location = New System.Drawing.Point(54, 9)
        Me.txtFromAssetNumber.Name = "txtFromAssetNumber"
        Me.txtFromAssetNumber.Size = New System.Drawing.Size(155, 20)
        Me.txtFromAssetNumber.TabIndex = 92
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(13, 39)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(21, 13)
        Me.Label7.TabIndex = 91
        Me.Label7.Text = "To"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(12, 12)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(36, 13)
        Me.Label11.TabIndex = 90
        Me.Label11.Text = "From"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(253, 23)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(74, 13)
        Me.Label12.TabIndex = 89
        Me.Label12.Text = "Asset Number"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(254, 174)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(57, 13)
        Me.Label16.TabIndex = 94
        Me.Label16.Text = "Ev Group2"
        '
        'txtGroup1
        '
        Me.txtGroup1.Location = New System.Drawing.Point(85, 171)
        Me.txtGroup1.Name = "txtGroup1"
        Me.txtGroup1.Size = New System.Drawing.Size(168, 20)
        Me.txtGroup1.TabIndex = 8
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 174)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(57, 13)
        Me.Label15.TabIndex = 92
        Me.Label15.Text = "Ev Group1"
        '
        'txtBusinessArea
        '
        Me.txtBusinessArea.Location = New System.Drawing.Point(85, 147)
        Me.txtBusinessArea.Name = "txtBusinessArea"
        Me.txtBusinessArea.Size = New System.Drawing.Size(168, 20)
        Me.txtBusinessArea.TabIndex = 7
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 150)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(74, 13)
        Me.Label14.TabIndex = 90
        Me.Label14.Text = "Business Area"
        '
        'txtInvestmentNo
        '
        Me.txtInvestmentNo.Location = New System.Drawing.Point(85, 122)
        Me.txtInvestmentNo.Name = "txtInvestmentNo"
        Me.txtInvestmentNo.Size = New System.Drawing.Size(168, 20)
        Me.txtInvestmentNo.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 122)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 13)
        Me.Label5.TabIndex = 86
        Me.Label5.Text = "Inv Number"
        '
        'grpLocation
        '
        Me.grpLocation.Controls.Add(Me.trvLocation)
        Me.grpLocation.Controls.Add(Me.Label2)
        Me.grpLocation.Controls.Add(Me.txtPlant)
        Me.grpLocation.Controls.Add(Me.txtLocation)
        Me.grpLocation.Controls.Add(Me.Label3)
        Me.grpLocation.Location = New System.Drawing.Point(330, 77)
        Me.grpLocation.Name = "grpLocation"
        Me.grpLocation.Size = New System.Drawing.Size(215, 86)
        Me.grpLocation.TabIndex = 5
        Me.grpLocation.TabStop = False
        '
        'trvLocation
        '
        Me.trvLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.trvLocation.BackColor = System.Drawing.Color.White
        Me.trvLocation.DataSource = Nothing
        Me.trvLocation.DisplayMember = ""
        Me.trvLocation.Location = New System.Drawing.Point(7, 10)
        Me.trvLocation.Name = "trvLocation"
        Me.trvLocation.SelectedText = ""
        Me.trvLocation.SelectedValue = ""
        Me.trvLocation.Size = New System.Drawing.Size(202, 24)
        Me.trvLocation.TabIndex = 7
        Me.trvLocation.TextReadOnly = False
        Me.trvLocation.ValueMember = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 91
        Me.Label2.Text = "Location"
        '
        'txtPlant
        '
        Me.txtPlant.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPlant.Location = New System.Drawing.Point(80, 37)
        Me.txtPlant.Name = "txtPlant"
        Me.txtPlant.Properties.ReadOnly = True
        Me.txtPlant.Size = New System.Drawing.Size(128, 19)
        Me.txtPlant.TabIndex = 88
        '
        'txtLocation
        '
        Me.txtLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLocation.Location = New System.Drawing.Point(80, 62)
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Properties.ReadOnly = True
        Me.txtLocation.Size = New System.Drawing.Size(128, 19)
        Me.txtLocation.TabIndex = 89
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 90
        Me.Label3.Text = "Plant"
        '
        'cmbComp
        '
        Me.cmbComp.BackColor = System.Drawing.Color.White
        Me.cmbComp.DataSource = Nothing
        Me.cmbComp.DisplayMember = ""
        Me.cmbComp.Location = New System.Drawing.Point(85, 19)
        Me.cmbComp.Name = "cmbComp"
        Me.cmbComp.SelectedIndex = -1
        Me.cmbComp.SelectedText = ""
        Me.cmbComp.SelectedValue = ""
        Me.cmbComp.Size = New System.Drawing.Size(168, 24)
        Me.cmbComp.TabIndex = 0
        Me.cmbComp.TextReadOnly = False
        Me.cmbComp.ValueMember = ""
        '
        'lblCompany
        '
        Me.lblCompany.AutoSize = True
        Me.lblCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblCompany.Location = New System.Drawing.Point(6, 21)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(51, 13)
        Me.lblCompany.TabIndex = 61
        Me.lblCompany.Text = "Company"
        '
        'lblBook
        '
        Me.lblBook.AutoSize = True
        Me.lblBook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblBook.Location = New System.Drawing.Point(6, 47)
        Me.lblBook.Name = "lblBook"
        Me.lblBook.Size = New System.Drawing.Size(59, 13)
        Me.lblBook.TabIndex = 61
        Me.lblBook.Text = "CostCenter"
        '
        'cmbCust
        '
        Me.cmbCust.BackColor = System.Drawing.Color.White
        Me.cmbCust.DataSource = Nothing
        Me.cmbCust.DisplayMember = ""
        Me.cmbCust.Location = New System.Drawing.Point(85, 70)
        Me.cmbCust.Name = "cmbCust"
        Me.cmbCust.SelectedIndex = -1
        Me.cmbCust.SelectedText = ""
        Me.cmbCust.SelectedValue = ""
        Me.cmbCust.Size = New System.Drawing.Size(168, 25)
        Me.cmbCust.TabIndex = 3
        Me.cmbCust.TextReadOnly = False
        Me.cmbCust.ValueMember = ""
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblCust.Location = New System.Drawing.Point(6, 74)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.Size = New System.Drawing.Size(53, 13)
        Me.lblCust.TabIndex = 61
        Me.lblCust.Text = "Employee"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(254, 86)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(47, 13)
        Me.Label19.TabIndex = 59
        Me.Label19.Text = "Location"
        '
        'cmbCostCenter
        '
        Me.cmbCostCenter.BackColor = System.Drawing.Color.White
        Me.cmbCostCenter.DataSource = Nothing
        Me.cmbCostCenter.DisplayMember = ""
        Me.cmbCostCenter.Location = New System.Drawing.Point(85, 44)
        Me.cmbCostCenter.Name = "cmbCostCenter"
        Me.cmbCostCenter.SelectedIndex = -1
        Me.cmbCostCenter.SelectedText = ""
        Me.cmbCostCenter.SelectedValue = ""
        Me.cmbCostCenter.Size = New System.Drawing.Size(168, 24)
        Me.cmbCostCenter.TabIndex = 2
        Me.cmbCostCenter.TextReadOnly = False
        Me.cmbCostCenter.ValueMember = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 99)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Asset Class"
        '
        'trvCategory
        '
        Me.trvCategory.BackColor = System.Drawing.Color.White
        Me.trvCategory.DataSource = Nothing
        Me.trvCategory.DisplayMember = ""
        Me.trvCategory.ForeColor = System.Drawing.Color.White
        Me.trvCategory.Location = New System.Drawing.Point(85, 96)
        Me.trvCategory.Name = "trvCategory"
        Me.trvCategory.SelectedText = ""
        Me.trvCategory.SelectedValue = ""
        Me.trvCategory.Size = New System.Drawing.Size(168, 25)
        Me.trvCategory.TabIndex = 4
        Me.trvCategory.TextReadOnly = False
        Me.trvCategory.ValueMember = ""
        '
        'grpFilterCreation
        '
        Me.grpFilterCreation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpFilterCreation.BackColor = System.Drawing.Color.White
        Me.grpFilterCreation.Controls.Add(Me.chkFilterByCreation)
        Me.grpFilterCreation.Controls.Add(Me.dtFromCreation)
        Me.grpFilterCreation.Controls.Add(Me.dtToCreation)
        Me.grpFilterCreation.Controls.Add(Me.Label8)
        Me.grpFilterCreation.Location = New System.Drawing.Point(5, 253)
        Me.grpFilterCreation.Name = "grpFilterCreation"
        Me.grpFilterCreation.Size = New System.Drawing.Size(552, 43)
        Me.grpFilterCreation.TabIndex = 1
        Me.grpFilterCreation.TabStop = False
        Me.grpFilterCreation.Text = "Filter by Creation Date"
        '
        'chkFilterByCreation
        '
        Me.chkFilterByCreation.AutoSize = True
        Me.chkFilterByCreation.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFilterByCreation.Location = New System.Drawing.Point(135, 20)
        Me.chkFilterByCreation.Name = "chkFilterByCreation"
        Me.chkFilterByCreation.Size = New System.Drawing.Size(55, 17)
        Me.chkFilterByCreation.TabIndex = 72
        Me.chkFilterByCreation.Text = "From"
        Me.chkFilterByCreation.UseVisualStyleBackColor = True
        '
        'dtFromCreation
        '
        Me.dtFromCreation.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFromCreation.Location = New System.Drawing.Point(196, 17)
        Me.dtFromCreation.Name = "dtFromCreation"
        Me.dtFromCreation.Size = New System.Drawing.Size(121, 20)
        Me.dtFromCreation.TabIndex = 0
        '
        'dtToCreation
        '
        Me.dtToCreation.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtToCreation.Location = New System.Drawing.Point(394, 17)
        Me.dtToCreation.Name = "dtToCreation"
        Me.dtToCreation.Size = New System.Drawing.Size(121, 20)
        Me.dtToCreation.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(364, 21)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(21, 13)
        Me.Label8.TabIndex = 71
        Me.Label8.Text = "To"
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(199, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(25, 25)
        Me.PictureBox1.TabIndex = 110
        Me.PictureBox1.TabStop = False
        '
        'lblReportName
        '
        Me.lblReportName.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportName.Appearance.ForeColor = System.Drawing.Color.Black
        Me.lblReportName.Appearance.Options.UseFont = True
        Me.lblReportName.Appearance.Options.UseForeColor = True
        Me.lblReportName.Location = New System.Drawing.Point(229, 1)
        Me.lblReportName.Name = "lblReportName"
        Me.lblReportName.Size = New System.Drawing.Size(161, 29)
        Me.lblReportName.TabIndex = 109
        Me.lblReportName.Text = "Select Report"
        '
        'tabPreviewReport
        '
        Me.tabPreviewReport.Controls.Add(Me.PreviewControl1)
        Me.tabPreviewReport.Name = "tabPreviewReport"
        Me.tabPreviewReport.Size = New System.Drawing.Size(563, 549)
        Me.tabPreviewReport.Text = "Preview Report"
        '
        'PreviewControl1
        '
        Me.PreviewControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PreviewControl1.Location = New System.Drawing.Point(0, 0)
        Me.PreviewControl1.Name = "PreviewControl1"
        Me.PreviewControl1.Size = New System.Drawing.Size(563, 549)
        Me.PreviewControl1.TabIndex = 1
        '
        'tabAnonymousFilter
        '
        Me.tabAnonymousFilter.Controls.Add(Me.imgAuditStatus)
        Me.tabAnonymousFilter.Controls.Add(Me.lblAuditStatus)
        Me.tabAnonymousFilter.Controls.Add(Me.btnCloseAnon)
        Me.tabAnonymousFilter.Controls.Add(Me.btnGenerateAnon)
        Me.tabAnonymousFilter.Controls.Add(Me.grpInventory)
        Me.tabAnonymousFilter.Name = "tabAnonymousFilter"
        Me.tabAnonymousFilter.Size = New System.Drawing.Size(563, 549)
        Me.tabAnonymousFilter.Text = "Anonymous Report Filter"
        '
        'imgAuditStatus
        '
        Me.imgAuditStatus.Location = New System.Drawing.Point(199, 3)
        Me.imgAuditStatus.Name = "imgAuditStatus"
        Me.imgAuditStatus.Size = New System.Drawing.Size(25, 25)
        Me.imgAuditStatus.TabIndex = 112
        Me.imgAuditStatus.TabStop = False
        '
        'lblAuditStatus
        '
        Me.lblAuditStatus.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAuditStatus.Appearance.ForeColor = System.Drawing.Color.Black
        Me.lblAuditStatus.Appearance.Options.UseFont = True
        Me.lblAuditStatus.Appearance.Options.UseForeColor = True
        Me.lblAuditStatus.Location = New System.Drawing.Point(229, 1)
        Me.lblAuditStatus.Name = "lblAuditStatus"
        Me.lblAuditStatus.Size = New System.Drawing.Size(161, 29)
        Me.lblAuditStatus.TabIndex = 111
        Me.lblAuditStatus.Text = "Select Report"
        '
        'btnCloseAnon
        '
        Me.btnCloseAnon.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCloseAnon.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCloseAnon.Image = Global.ABBIntegration.My.Resources.Resources.Close16x16
        Me.btnCloseAnon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCloseAnon.Location = New System.Drawing.Point(441, 505)
        Me.btnCloseAnon.Name = "btnCloseAnon"
        Me.btnCloseAnon.Size = New System.Drawing.Size(84, 27)
        Me.btnCloseAnon.TabIndex = 101
        Me.btnCloseAnon.Text = "&Close"
        Me.btnCloseAnon.UseVisualStyleBackColor = True
        '
        'btnGenerateAnon
        '
        Me.btnGenerateAnon.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenerateAnon.Image = Global.ABBIntegration.My.Resources.Resources.Import
        Me.btnGenerateAnon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerateAnon.Location = New System.Drawing.Point(281, 505)
        Me.btnGenerateAnon.Name = "btnGenerateAnon"
        Me.btnGenerateAnon.Size = New System.Drawing.Size(142, 27)
        Me.btnGenerateAnon.TabIndex = 100
        Me.btnGenerateAnon.Text = "&Generate Report"
        Me.btnGenerateAnon.UseVisualStyleBackColor = True
        '
        'grpInventory
        '
        Me.grpInventory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpInventory.BackColor = System.Drawing.Color.White
        Me.grpInventory.Controls.Add(Me.Label6)
        Me.grpInventory.Controls.Add(Me.cmbSchAnon)
        Me.grpInventory.Controls.Add(Me.Label9)
        Me.grpInventory.Controls.Add(Me.lblEnd)
        Me.grpInventory.Controls.Add(Me.Label10)
        Me.grpInventory.Controls.Add(Me.lblStart)
        Me.grpInventory.Location = New System.Drawing.Point(3, 46)
        Me.grpInventory.Name = "grpInventory"
        Me.grpInventory.Size = New System.Drawing.Size(554, 130)
        Me.grpInventory.TabIndex = 99
        Me.grpInventory.TabStop = False
        Me.grpInventory.Text = "Select Inventory Schedule"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(31, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(118, 13)
        Me.Label6.TabIndex = 85
        Me.Label6.Text = "Inventory Schedule"
        '
        'cmbSchAnon
        '
        Me.cmbSchAnon.BackColor = System.Drawing.Color.White
        Me.cmbSchAnon.DataSource = Nothing
        Me.cmbSchAnon.DisplayMember = ""
        Me.cmbSchAnon.Location = New System.Drawing.Point(163, 22)
        Me.cmbSchAnon.Name = "cmbSchAnon"
        Me.cmbSchAnon.SelectedIndex = -1
        Me.cmbSchAnon.SelectedText = ""
        Me.cmbSchAnon.SelectedValue = ""
        Me.cmbSchAnon.Size = New System.Drawing.Size(190, 24)
        Me.cmbSchAnon.TabIndex = 84
        Me.cmbSchAnon.TextReadOnly = False
        Me.cmbSchAnon.ValueMember = ""
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(31, 63)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 13)
        Me.Label9.TabIndex = 90
        Me.Label9.Text = "Start Date"
        '
        'lblEnd
        '
        Me.lblEnd.Location = New System.Drawing.Point(163, 91)
        Me.lblEnd.Name = "lblEnd"
        Me.lblEnd.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.lblEnd.Properties.Appearance.Options.UseBackColor = True
        Me.lblEnd.Properties.ReadOnly = True
        Me.lblEnd.Size = New System.Drawing.Size(189, 19)
        Me.lblEnd.TabIndex = 87
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(31, 95)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(57, 13)
        Me.Label10.TabIndex = 91
        Me.Label10.Text = "End Date"
        '
        'lblStart
        '
        Me.lblStart.Location = New System.Drawing.Point(163, 56)
        Me.lblStart.Name = "lblStart"
        Me.lblStart.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.lblStart.Properties.Appearance.Options.UseBackColor = True
        Me.lblStart.Properties.ReadOnly = True
        Me.lblStart.Size = New System.Drawing.Size(189, 19)
        Me.lblStart.TabIndex = 86
        '
        'tabGRFilter
        '
        Me.tabGRFilter.Controls.Add(Me.txtReceiveSAPMatNo)
        Me.tabGRFilter.Controls.Add(Me.Label23)
        Me.tabGRFilter.Controls.Add(Me.GroupBox5)
        Me.tabGRFilter.Controls.Add(Me.txtReceiveManPartNo)
        Me.tabGRFilter.Controls.Add(Me.Label25)
        Me.tabGRFilter.Controls.Add(Me.txtReceiveSerialNo)
        Me.tabGRFilter.Controls.Add(Me.Label26)
        Me.tabGRFilter.Controls.Add(Me.GroupBox4)
        Me.tabGRFilter.Controls.Add(Me.Button1)
        Me.tabGRFilter.Controls.Add(Me.btnGenerateReceive)
        Me.tabGRFilter.Controls.Add(Me.imgReceiving)
        Me.tabGRFilter.Controls.Add(Me.lblReciving)
        Me.tabGRFilter.Name = "tabGRFilter"
        Me.tabGRFilter.Size = New System.Drawing.Size(563, 549)
        Me.tabGRFilter.Text = "Goods Receving Filter"
        '
        'txtReceiveSAPMatNo
        '
        Me.txtReceiveSAPMatNo.Location = New System.Drawing.Point(137, 213)
        Me.txtReceiveSAPMatNo.Name = "txtReceiveSAPMatNo"
        Me.txtReceiveSAPMatNo.Size = New System.Drawing.Size(166, 20)
        Me.txtReceiveSAPMatNo.TabIndex = 122
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(3, 216)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(110, 13)
        Me.Label23.TabIndex = 125
        Me.Label23.Text = "SAP Mat Part Number"
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.BackColor = System.Drawing.Color.White
        Me.GroupBox5.Controls.Add(Me.chkReceiveDate)
        Me.GroupBox5.Controls.Add(Me.dtReceiveFrom)
        Me.GroupBox5.Controls.Add(Me.dtReceiveTo)
        Me.GroupBox5.Controls.Add(Me.Label24)
        Me.GroupBox5.Location = New System.Drawing.Point(311, 56)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(249, 88)
        Me.GroupBox5.TabIndex = 119
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Filter by Date"
        '
        'chkReceiveDate
        '
        Me.chkReceiveDate.AutoSize = True
        Me.chkReceiveDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkReceiveDate.Location = New System.Drawing.Point(19, 28)
        Me.chkReceiveDate.Name = "chkReceiveDate"
        Me.chkReceiveDate.Size = New System.Drawing.Size(55, 17)
        Me.chkReceiveDate.TabIndex = 72
        Me.chkReceiveDate.Text = "From"
        Me.chkReceiveDate.UseVisualStyleBackColor = True
        '
        'dtReceiveFrom
        '
        Me.dtReceiveFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtReceiveFrom.Location = New System.Drawing.Point(86, 25)
        Me.dtReceiveFrom.Name = "dtReceiveFrom"
        Me.dtReceiveFrom.Size = New System.Drawing.Size(121, 20)
        Me.dtReceiveFrom.TabIndex = 0
        '
        'dtReceiveTo
        '
        Me.dtReceiveTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtReceiveTo.Location = New System.Drawing.Point(86, 51)
        Me.dtReceiveTo.Name = "dtReceiveTo"
        Me.dtReceiveTo.Size = New System.Drawing.Size(121, 20)
        Me.dtReceiveTo.TabIndex = 1
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(42, 55)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(21, 13)
        Me.Label24.TabIndex = 71
        Me.Label24.Text = "To"
        '
        'txtReceiveManPartNo
        '
        Me.txtReceiveManPartNo.Location = New System.Drawing.Point(137, 187)
        Me.txtReceiveManPartNo.Name = "txtReceiveManPartNo"
        Me.txtReceiveManPartNo.Size = New System.Drawing.Size(166, 20)
        Me.txtReceiveManPartNo.TabIndex = 121
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(3, 190)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(131, 13)
        Me.Label25.TabIndex = 124
        Me.Label25.Text = "Manufacture Part Number"
        '
        'txtReceiveSerialNo
        '
        Me.txtReceiveSerialNo.Location = New System.Drawing.Point(137, 163)
        Me.txtReceiveSerialNo.Name = "txtReceiveSerialNo"
        Me.txtReceiveSerialNo.Size = New System.Drawing.Size(166, 20)
        Me.txtReceiveSerialNo.TabIndex = 120
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(3, 166)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(73, 13)
        Me.Label26.TabIndex = 123
        Me.Label26.Text = "Serial Number"
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.White
        Me.GroupBox4.Controls.Add(Me.txtReceivePOTo)
        Me.GroupBox4.Controls.Add(Me.txtReceivePOFrom)
        Me.GroupBox4.Controls.Add(Me.Label20)
        Me.GroupBox4.Controls.Add(Me.Label21)
        Me.GroupBox4.Location = New System.Drawing.Point(3, 56)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(300, 88)
        Me.GroupBox4.TabIndex = 117
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "PO"
        '
        'txtReceivePOTo
        '
        Me.txtReceivePOTo.Location = New System.Drawing.Point(55, 51)
        Me.txtReceivePOTo.Name = "txtReceivePOTo"
        Me.txtReceivePOTo.Size = New System.Drawing.Size(154, 20)
        Me.txtReceivePOTo.TabIndex = 94
        '
        'txtReceivePOFrom
        '
        Me.txtReceivePOFrom.Location = New System.Drawing.Point(54, 26)
        Me.txtReceivePOFrom.Name = "txtReceivePOFrom"
        Me.txtReceivePOFrom.Size = New System.Drawing.Size(155, 20)
        Me.txtReceivePOFrom.TabIndex = 92
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(14, 54)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(21, 13)
        Me.Label20.TabIndex = 91
        Me.Label20.Text = "To"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(12, 29)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(36, 13)
        Me.Label21.TabIndex = 90
        Me.Label21.Text = "From"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button1.Image = Global.ABBIntegration.My.Resources.Resources.Close16x16
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(463, 516)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(84, 27)
        Me.Button1.TabIndex = 116
        Me.Button1.Text = "&Close"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnGenerateReceive
        '
        Me.btnGenerateReceive.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenerateReceive.Image = Global.ABBIntegration.My.Resources.Resources.Import
        Me.btnGenerateReceive.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerateReceive.Location = New System.Drawing.Point(303, 516)
        Me.btnGenerateReceive.Name = "btnGenerateReceive"
        Me.btnGenerateReceive.Size = New System.Drawing.Size(142, 27)
        Me.btnGenerateReceive.TabIndex = 115
        Me.btnGenerateReceive.Text = "&Generate Report"
        Me.btnGenerateReceive.UseVisualStyleBackColor = True
        '
        'imgReceiving
        '
        Me.imgReceiving.Location = New System.Drawing.Point(167, 5)
        Me.imgReceiving.Name = "imgReceiving"
        Me.imgReceiving.Size = New System.Drawing.Size(25, 25)
        Me.imgReceiving.TabIndex = 114
        Me.imgReceiving.TabStop = False
        '
        'lblReciving
        '
        Me.lblReciving.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReciving.Appearance.ForeColor = System.Drawing.Color.Black
        Me.lblReciving.Appearance.Options.UseFont = True
        Me.lblReciving.Appearance.Options.UseForeColor = True
        Me.lblReciving.Location = New System.Drawing.Point(197, 3)
        Me.lblReciving.Name = "lblReciving"
        Me.lblReciving.Size = New System.Drawing.Size(161, 29)
        Me.lblReciving.TabIndex = 113
        Me.lblReciving.Text = "Select Report"
        '
        'tabGIFilter
        '
        Me.tabGIFilter.Controls.Add(Me.GroupBox7)
        Me.tabGIFilter.Controls.Add(Me.grpIssuanceFilter)
        Me.tabGIFilter.Controls.Add(Me.GroupBox6)
        Me.tabGIFilter.Controls.Add(Me.Button3)
        Me.tabGIFilter.Controls.Add(Me.btnGenerateIssuance)
        Me.tabGIFilter.Controls.Add(Me.imgIssuance)
        Me.tabGIFilter.Controls.Add(Me.lblIssuance)
        Me.tabGIFilter.Name = "tabGIFilter"
        Me.tabGIFilter.Size = New System.Drawing.Size(563, 549)
        Me.tabGIFilter.Text = "Good Issuance Filter"
        '
        'GroupBox7
        '
        Me.GroupBox7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox7.BackColor = System.Drawing.Color.White
        Me.GroupBox7.Controls.Add(Me.Label31)
        Me.GroupBox7.Controls.Add(Me.txtDocNo)
        Me.GroupBox7.Controls.Add(Me.Label29)
        Me.GroupBox7.Controls.Add(Me.txtIssueSerialNumber)
        Me.GroupBox7.Controls.Add(Me.txtIssueAssetNo)
        Me.GroupBox7.Controls.Add(Me.Label28)
        Me.GroupBox7.Controls.Add(Me.lblAssetNumber)
        Me.GroupBox7.Controls.Add(Me.txtIssueManPartNo)
        Me.GroupBox7.Controls.Add(Me.txtIssueSAPMat)
        Me.GroupBox7.Controls.Add(Me.Label22)
        Me.GroupBox7.Location = New System.Drawing.Point(9, 102)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(539, 100)
        Me.GroupBox7.TabIndex = 136
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Filter By Data"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(6, 27)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(95, 13)
        Me.Label31.TabIndex = 136
        Me.Label31.Text = "Document Number"
        '
        'txtDocNo
        '
        Me.txtDocNo.Location = New System.Drawing.Point(140, 24)
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(116, 20)
        Me.txtDocNo.TabIndex = 135
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(6, 53)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(73, 13)
        Me.Label29.TabIndex = 130
        Me.Label29.Text = "Serial Number"
        '
        'txtIssueSerialNumber
        '
        Me.txtIssueSerialNumber.Location = New System.Drawing.Point(140, 50)
        Me.txtIssueSerialNumber.Name = "txtIssueSerialNumber"
        Me.txtIssueSerialNumber.Size = New System.Drawing.Size(116, 20)
        Me.txtIssueSerialNumber.TabIndex = 127
        '
        'txtIssueAssetNo
        '
        Me.txtIssueAssetNo.Location = New System.Drawing.Point(395, 77)
        Me.txtIssueAssetNo.Name = "txtIssueAssetNo"
        Me.txtIssueAssetNo.Size = New System.Drawing.Size(138, 20)
        Me.txtIssueAssetNo.TabIndex = 133
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(6, 77)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(131, 13)
        Me.Label28.TabIndex = 131
        Me.Label28.Text = "Manufacture Part Number"
        '
        'lblAssetNumber
        '
        Me.lblAssetNumber.AutoSize = True
        Me.lblAssetNumber.Location = New System.Drawing.Point(261, 80)
        Me.lblAssetNumber.Name = "lblAssetNumber"
        Me.lblAssetNumber.Size = New System.Drawing.Size(74, 13)
        Me.lblAssetNumber.TabIndex = 134
        Me.lblAssetNumber.Text = "Asset Number"
        '
        'txtIssueManPartNo
        '
        Me.txtIssueManPartNo.Location = New System.Drawing.Point(140, 74)
        Me.txtIssueManPartNo.Name = "txtIssueManPartNo"
        Me.txtIssueManPartNo.Size = New System.Drawing.Size(116, 20)
        Me.txtIssueManPartNo.TabIndex = 128
        '
        'txtIssueSAPMat
        '
        Me.txtIssueSAPMat.Location = New System.Drawing.Point(395, 50)
        Me.txtIssueSAPMat.Name = "txtIssueSAPMat"
        Me.txtIssueSAPMat.Size = New System.Drawing.Size(138, 20)
        Me.txtIssueSAPMat.TabIndex = 129
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(261, 53)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(110, 13)
        Me.Label22.TabIndex = 132
        Me.Label22.Text = "SAP Mat Part Number"
        '
        'grpIssuanceFilter
        '
        Me.grpIssuanceFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpIssuanceFilter.BackColor = System.Drawing.Color.White
        Me.grpIssuanceFilter.Controls.Add(Me.rdoIssueTypePOR)
        Me.grpIssuanceFilter.Controls.Add(Me.rdoIssueTypeInvPr)
        Me.grpIssuanceFilter.Controls.Add(Me.rdoIssueTypeBoth)
        Me.grpIssuanceFilter.Location = New System.Drawing.Point(9, 208)
        Me.grpIssuanceFilter.Name = "grpIssuanceFilter"
        Me.grpIssuanceFilter.Size = New System.Drawing.Size(539, 58)
        Me.grpIssuanceFilter.TabIndex = 135
        Me.grpIssuanceFilter.TabStop = False
        Me.grpIssuanceFilter.Text = "Filter by Issue Type"
        '
        'rdoIssueTypePOR
        '
        Me.rdoIssueTypePOR.AutoSize = True
        Me.rdoIssueTypePOR.Location = New System.Drawing.Point(405, 19)
        Me.rdoIssueTypePOR.Name = "rdoIssueTypePOR"
        Me.rdoIssueTypePOR.Size = New System.Drawing.Size(124, 17)
        Me.rdoIssueTypePOR.TabIndex = 2
        Me.rdoIssueTypePOR.Text = "Purchase Requisition"
        Me.rdoIssueTypePOR.UseVisualStyleBackColor = True
        '
        'rdoIssueTypeInvPr
        '
        Me.rdoIssueTypeInvPr.AutoSize = True
        Me.rdoIssueTypeInvPr.Location = New System.Drawing.Point(181, 19)
        Me.rdoIssueTypeInvPr.Name = "rdoIssueTypeInvPr"
        Me.rdoIssueTypeInvPr.Size = New System.Drawing.Size(124, 17)
        Me.rdoIssueTypeInvPr.TabIndex = 1
        Me.rdoIssueTypeInvPr.Text = "Investment Proposal"
        Me.rdoIssueTypeInvPr.UseVisualStyleBackColor = True
        '
        'rdoIssueTypeBoth
        '
        Me.rdoIssueTypeBoth.AutoSize = True
        Me.rdoIssueTypeBoth.Checked = True
        Me.rdoIssueTypeBoth.Location = New System.Drawing.Point(34, 19)
        Me.rdoIssueTypeBoth.Name = "rdoIssueTypeBoth"
        Me.rdoIssueTypeBoth.Size = New System.Drawing.Size(47, 17)
        Me.rdoIssueTypeBoth.TabIndex = 0
        Me.rdoIssueTypeBoth.TabStop = True
        Me.rdoIssueTypeBoth.Text = "Both"
        Me.rdoIssueTypeBoth.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.BackColor = System.Drawing.Color.White
        Me.GroupBox6.Controls.Add(Me.chkIssueDate)
        Me.GroupBox6.Controls.Add(Me.dtIssueFrom)
        Me.GroupBox6.Controls.Add(Me.dtIssueTo)
        Me.GroupBox6.Controls.Add(Me.Label27)
        Me.GroupBox6.Location = New System.Drawing.Point(8, 38)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(539, 58)
        Me.GroupBox6.TabIndex = 126
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Filter by Date"
        '
        'chkIssueDate
        '
        Me.chkIssueDate.AutoSize = True
        Me.chkIssueDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIssueDate.Location = New System.Drawing.Point(62, 22)
        Me.chkIssueDate.Name = "chkIssueDate"
        Me.chkIssueDate.Size = New System.Drawing.Size(55, 17)
        Me.chkIssueDate.TabIndex = 72
        Me.chkIssueDate.Text = "From"
        Me.chkIssueDate.UseVisualStyleBackColor = True
        '
        'dtIssueFrom
        '
        Me.dtIssueFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtIssueFrom.Location = New System.Drawing.Point(129, 19)
        Me.dtIssueFrom.Name = "dtIssueFrom"
        Me.dtIssueFrom.Size = New System.Drawing.Size(121, 20)
        Me.dtIssueFrom.TabIndex = 0
        '
        'dtIssueTo
        '
        Me.dtIssueTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtIssueTo.Location = New System.Drawing.Point(386, 19)
        Me.dtIssueTo.Name = "dtIssueTo"
        Me.dtIssueTo.Size = New System.Drawing.Size(121, 20)
        Me.dtIssueTo.TabIndex = 1
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(341, 23)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(21, 13)
        Me.Label27.TabIndex = 71
        Me.Label27.Text = "To"
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button3.Image = Global.ABBIntegration.My.Resources.Resources.Close16x16
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.Location = New System.Drawing.Point(463, 516)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(84, 27)
        Me.Button3.TabIndex = 118
        Me.Button3.Text = "&Close"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'btnGenerateIssuance
        '
        Me.btnGenerateIssuance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenerateIssuance.Image = Global.ABBIntegration.My.Resources.Resources.Import
        Me.btnGenerateIssuance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerateIssuance.Location = New System.Drawing.Point(303, 516)
        Me.btnGenerateIssuance.Name = "btnGenerateIssuance"
        Me.btnGenerateIssuance.Size = New System.Drawing.Size(142, 27)
        Me.btnGenerateIssuance.TabIndex = 117
        Me.btnGenerateIssuance.Text = "&Generate Report"
        Me.btnGenerateIssuance.UseVisualStyleBackColor = True
        '
        'imgIssuance
        '
        Me.imgIssuance.Location = New System.Drawing.Point(165, 5)
        Me.imgIssuance.Name = "imgIssuance"
        Me.imgIssuance.Size = New System.Drawing.Size(25, 25)
        Me.imgIssuance.TabIndex = 116
        Me.imgIssuance.TabStop = False
        '
        'lblIssuance
        '
        Me.lblIssuance.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIssuance.Appearance.ForeColor = System.Drawing.Color.Black
        Me.lblIssuance.Appearance.Options.UseFont = True
        Me.lblIssuance.Appearance.Options.UseForeColor = True
        Me.lblIssuance.Location = New System.Drawing.Point(195, 3)
        Me.lblIssuance.Name = "lblIssuance"
        Me.lblIssuance.Size = New System.Drawing.Size(161, 29)
        Me.lblIssuance.TabIndex = 115
        Me.lblIssuance.Text = "Select Report"
        '
        'frmABBReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(782, 580)
        Me.Controls.Add(Me.tabControl)
        Me.Controls.Add(Me.nvControl)
        Me.MinimumSize = New System.Drawing.Size(600, 400)
        Me.Name = "frmABBReports"
        Me.Text = "Reports"
        CType(Me.imgListLarge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nvControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControl.ResumeLayout(False)
        Me.tabMainFilter.ResumeLayout(False)
        Me.tabMainFilter.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grpRetirement.ResumeLayout(False)
        Me.grpRetirement.PerformLayout()
        Me.grpFilterByData.ResumeLayout(False)
        Me.grpFilterByData.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpLocation.ResumeLayout(False)
        Me.grpLocation.PerformLayout()
        CType(Me.txtPlant.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFilterCreation.ResumeLayout(False)
        Me.grpFilterCreation.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPreviewReport.ResumeLayout(False)
        Me.tabAnonymousFilter.ResumeLayout(False)
        Me.tabAnonymousFilter.PerformLayout()
        CType(Me.imgAuditStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpInventory.ResumeLayout(False)
        Me.grpInventory.PerformLayout()
        CType(Me.lblEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabGRFilter.ResumeLayout(False)
        Me.tabGRFilter.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.imgReceiving, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabGIFilter.ResumeLayout(False)
        Me.tabGIFilter.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.grpIssuanceFilter.ResumeLayout(False)
        Me.grpIssuanceFilter.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.imgIssuance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents imgListLarge As DevExpress.Utils.ImageCollection
    Public WithEvents nvControl As DevExpress.XtraNavBar.NavBarControl
    Friend WithEvents nvGroupStandard As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nvItemCompanyAsset As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvAssetswithvlaue As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemMisplaced As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemPhysicalInventory As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemAnonymous As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemDisposed As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents NavBarItem4 As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents tabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tabMainFilter As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnGen As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkShowOnlyAssetWithValue As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowRetired As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowSubLevels As System.Windows.Forms.CheckBox
    Friend WithEvents lblDataSource As System.Windows.Forms.Label
    Friend WithEvents cmbDataSource As System.Windows.Forms.ComboBox
    Friend WithEvents grpRetirement As System.Windows.Forms.GroupBox
    Friend WithEvents chkFilterByDisposeDate As System.Windows.Forms.CheckBox
    Friend WithEvents dtFromDisposal As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtToDisposal As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents grpFilterByData As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtInvestmentNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents grpLocation As System.Windows.Forms.GroupBox
    Friend WithEvents trvLocation As ZulTree.ZulTree
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPlant As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtLocation As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbComp As ZulLOV.ZulLOV
    Friend WithEvents lblCompany As System.Windows.Forms.Label
    Friend WithEvents lblBook As System.Windows.Forms.Label
    Friend WithEvents cmbCust As ZulLOV.ZulLOV
    Friend WithEvents lblCust As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cmbCostCenter As ZulLOV.ZulLOV
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents trvCategory As ZulTree.ZulTree
    Friend WithEvents grpFilterCreation As System.Windows.Forms.GroupBox
    Friend WithEvents chkFilterByCreation As System.Windows.Forms.CheckBox
    Friend WithEvents dtFromCreation As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtToCreation As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblReportName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tabPreviewReport As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PreviewControl1 As ABBIntegration.PreviewControl
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents chkFilterByCapitalize As System.Windows.Forms.CheckBox
    Friend WithEvents dtFromCapitalize As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtToCapitalize As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtGroup4 As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtGroup3 As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtGroup2 As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtGroup1 As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtBusinessArea As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtToAsetNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtFromAssetNumber As System.Windows.Forms.TextBox
    Friend WithEvents chkShowOnlyCapitalization As System.Windows.Forms.CheckBox
    Friend WithEvents cmbSch As ZulLOV.ZulLOV
    Friend WithEvents lblInventory As System.Windows.Forms.Label
    Friend WithEvents nvFoundAssets As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvMissingAssets As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvTransferedAssets As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvInventoryReports As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nvGoodsReciveDetail As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvGoodsReceiveStanderd As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvIssuanceReport As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents chkExcludeCapitalize As System.Windows.Forms.CheckBox
    Friend WithEvents chkExcludeAssetValue As System.Windows.Forms.CheckBox
    Friend WithEvents chkExcludeRetired As System.Windows.Forms.CheckBox
    Friend WithEvents chkHideReportFooter As System.Windows.Forms.CheckBox
    Friend WithEvents chkHideReportHeader As System.Windows.Forms.CheckBox
    Friend WithEvents tabAnonymousFilter As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents btnCloseAnon As System.Windows.Forms.Button
    Friend WithEvents btnGenerateAnon As System.Windows.Forms.Button
    Friend WithEvents grpInventory As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbSchAnon As ZulLOV.ZulLOV
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblEnd As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblStart As DevExpress.XtraEditors.TextEdit
    Friend WithEvents imgAuditStatus As System.Windows.Forms.PictureBox
    Friend WithEvents lblAuditStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tabGRFilter As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents imgReceiving As System.Windows.Forms.PictureBox
    Friend WithEvents lblReciving As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tabGIFilter As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents imgIssuance As System.Windows.Forms.PictureBox
    Friend WithEvents lblIssuance As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtReceivePOTo As System.Windows.Forms.TextBox
    Friend WithEvents txtReceivePOFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnGenerateReceive As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents btnGenerateIssuance As System.Windows.Forms.Button
    Friend WithEvents txtReceiveSAPMatNo As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents chkReceiveDate As System.Windows.Forms.CheckBox
    Friend WithEvents dtReceiveFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtReceiveTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtReceiveManPartNo As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtReceiveSerialNo As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtIssueAssetNo As System.Windows.Forms.TextBox
    Friend WithEvents lblAssetNumber As System.Windows.Forms.Label
    Friend WithEvents txtIssueSAPMat As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents chkIssueDate As System.Windows.Forms.CheckBox
    Friend WithEvents dtIssueFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtIssueTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtIssueManPartNo As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtIssueSerialNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents grpIssuanceFilter As System.Windows.Forms.GroupBox
    Friend WithEvents rdoIssueTypePOR As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIssueTypeInvPr As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIssueTypeBoth As System.Windows.Forms.RadioButton
    Friend WithEvents nvReversal As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvVendorReturn As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvWarrantyClaim As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvWarrantyRecevingSame As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvWarrantyRecevingReplace As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtDocNo As System.Windows.Forms.TextBox
    Friend WithEvents ntAssetLog As DevExpress.XtraNavBar.NavBarItem
End Class
