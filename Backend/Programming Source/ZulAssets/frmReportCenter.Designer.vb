<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportCenter
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReportCenter))
        Me.chkExclude = New System.Windows.Forms.CheckBox
        Me.btnHierLOV = New System.Windows.Forms.Button
        Me.cmbDepart = New DevExpress.XtraEditors.TextEdit
        Me.chkSubLevels = New System.Windows.Forms.CheckBox
        Me.lblHierarchy = New System.Windows.Forms.Label
        Me.ZTCategory = New ZulTree.ZulTree
        Me.ZTLocation = New ZulTree.ZulTree
        Me.chkDisposed = New System.Windows.Forms.CheckBox
        Me.lblBrand = New System.Windows.Forms.Label
        Me.cmbSupp = New ZulLOV.ZulLOV
        Me.cmbBrand = New ZulLOV.ZulLOV
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblExpectedDepreciation = New System.Windows.Forms.Label
        Me.dtToDT = New System.Windows.Forms.DateTimePicker
        Me.dtFrmDt = New System.Windows.Forms.DateTimePicker
        Me.dtExpectedDepreciation = New System.Windows.Forms.DateTimePicker
        Me.cmbBook = New ZulLOV.ZulLOV
        Me.cmbComp = New ZulLOV.ZulLOV
        Me.cmbCust = New ZulLOV.ZulLOV
        Me.lblCompany = New System.Windows.Forms.Label
        Me.lblBook = New System.Windows.Forms.Label
        Me.lblCust = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnGen = New System.Windows.Forms.Button
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.nvControl = New DevExpress.XtraNavBar.NavBarControl
        Me.nvGroupAuditStatus = New DevExpress.XtraNavBar.NavBarGroup
        Me.nvItemAnonymous = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemMissingAssets = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemFoundAssets = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemMisplaced = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemTransferredAssets = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemAllocatedAssets = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemAllAssets = New DevExpress.XtraNavBar.NavBarItem
        Me.nvCostCenterAudit = New DevExpress.XtraNavBar.NavBarItem
        Me.nvGroupStandard = New DevExpress.XtraNavBar.NavBarGroup
        Me.nvItemCompanyAsset = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemDepreciationBooks = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemExpectedDepr = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemAssetTag = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemAssetLedger = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemInventory = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemAssetRegister = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemNewTags = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemAssetDetails = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemDisposed = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemAssetByCat = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemAssetsBySubCat = New DevExpress.XtraNavBar.NavBarItem
        Me.ntAssetLog = New DevExpress.XtraNavBar.NavBarItem
        Me.nvGroupExtended = New DevExpress.XtraNavBar.NavBarGroup
        Me.nvGroupMaster = New DevExpress.XtraNavBar.NavBarGroup
        Me.navItemDesignation = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemCustodian = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemBrand = New DevExpress.XtraNavBar.NavBarItem
        Me.nvitemInsurer = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemSupplier = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemDisposalMethods = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemDepreciationMethods = New DevExpress.XtraNavBar.NavBarItem
        Me.nvAssetItems = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemAssetBooks = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemInventSch = New DevExpress.XtraNavBar.NavBarItem
        Me.nvItemAddressBook = New DevExpress.XtraNavBar.NavBarItem
        Me.NavBarItem4 = New DevExpress.XtraNavBar.NavBarItem
        Me.imgListLarge = New DevExpress.Utils.ImageCollection(Me.components)
        Me.tabControl = New DevExpress.XtraTab.XtraTabControl
        Me.tabFilter = New DevExpress.XtraTab.XtraTabPage
        Me.btnRefreshExtended = New System.Windows.Forms.Button
        Me.grpDisposedReport = New System.Windows.Forms.GroupBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.chkFilterByDisposeDate = New System.Windows.Forms.CheckBox
        Me.dtFromDisposal = New System.Windows.Forms.DateTimePicker
        Me.dtToDisposal = New System.Windows.Forms.DateTimePicker
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmdDisposal = New ZulLOV.ZulLOV
        Me.Label33 = New System.Windows.Forms.Label
        Me.grpFilterByData = New System.Windows.Forms.GroupBox
        Me.cmbDataSource = New DevExpress.XtraEditors.ComboBoxEdit
        Me.cmbInventoryStatus = New DevExpress.XtraEditors.ImageComboBoxEdit
        Me.imgAssetStatus = New System.Windows.Forms.ImageList(Me.components)
        Me.Label12 = New System.Windows.Forms.Label
        Me.cmbAssetStatus = New ZulLOV.ZulLOV
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtTotalCost = New DevExpress.XtraEditors.SpinEdit
        Me.cmbTotalCostFilterType = New DevExpress.XtraEditors.ComboBoxEdit
        Me.lblCostPrice = New System.Windows.Forms.Label
        Me.cmbItemCode = New ZulLOV.ZulLOV
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblDataSource = New System.Windows.Forms.Label
        Me.lblsupplier = New System.Windows.Forms.Label
        Me.grpFilterByDate = New System.Windows.Forms.GroupBox
        Me.chkFilterByDate = New System.Windows.Forms.CheckBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lblReportName = New DevExpress.XtraEditors.LabelControl
        Me.tabPreviewReport = New DevExpress.XtraTab.XtraTabPage
        Me.PreviewControl1 = New ZulAssets.PreviewControl
        Me.tabAuditStatus = New DevExpress.XtraTab.XtraTabPage
        Me.imgAuditStatus = New System.Windows.Forms.PictureBox
        Me.lblAuditStatus = New DevExpress.XtraEditors.LabelControl
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnGenAuditStatus = New System.Windows.Forms.Button
        Me.grpInventory = New System.Windows.Forms.GroupBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.cmbCatAudit = New ZulTree.ZulTree
        Me.Label3 = New System.Windows.Forms.Label
        Me.chkIncludeSubAudit = New System.Windows.Forms.CheckBox
        Me.cmbLocationAudit = New ZulTree.ZulTree
        Me.Label6 = New System.Windows.Forms.Label
        Me.cmbSch = New ZulLOV.ZulLOV
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblEnd = New DevExpress.XtraEditors.TextEdit
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblStart = New DevExpress.XtraEditors.TextEdit
        Me.tabCategory = New DevExpress.XtraTab.XtraTabPage
        Me.imgCategory = New System.Windows.Forms.PictureBox
        Me.lblCategory = New DevExpress.XtraEditors.LabelControl
        Me.btnGenCatReport = New System.Windows.Forms.Button
        Me.btnRefreshCat = New System.Windows.Forms.Button
        Me.btnCloseCat = New System.Windows.Forms.Button
        Me.Groupbox1 = New System.Windows.Forms.GroupBox
        Me.dtFrom = New ZulLOV.ZulLOV
        Me.dtTo = New ZulLOV.ZulLOV
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.tabAssetDetails = New DevExpress.XtraTab.XtraTabPage
        Me.Splitter1 = New System.Windows.Forms.Splitter
        CType(Me.cmbDepart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nvControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgListLarge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControl.SuspendLayout()
        Me.tabFilter.SuspendLayout()
        Me.grpDisposedReport.SuspendLayout()
        Me.grpFilterByData.SuspendLayout()
        CType(Me.cmbDataSource.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbInventoryStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalCost.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTotalCostFilterType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFilterByDate.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPreviewReport.SuspendLayout()
        Me.tabAuditStatus.SuspendLayout()
        CType(Me.imgAuditStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpInventory.SuspendLayout()
        CType(Me.lblEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabCategory.SuspendLayout()
        CType(Me.imgCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Groupbox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkExclude
        '
        Me.chkExclude.AutoSize = True
        Me.chkExclude.BackColor = System.Drawing.Color.Transparent
        Me.chkExclude.Location = New System.Drawing.Point(287, 286)
        Me.chkExclude.Name = "chkExclude"
        Me.chkExclude.Size = New System.Drawing.Size(144, 17)
        Me.chkExclude.TabIndex = 11
        Me.chkExclude.Text = "Exclude Disposed Assets"
        Me.chkExclude.UseVisualStyleBackColor = False
        '
        'btnHierLOV
        '
        Me.btnHierLOV.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnHierLOV.BackColor = System.Drawing.Color.Transparent
        Me.btnHierLOV.Image = CType(resources.GetObject("btnHierLOV.Image"), System.Drawing.Image)
        Me.btnHierLOV.Location = New System.Drawing.Point(583, 84)
        Me.btnHierLOV.MaximumSize = New System.Drawing.Size(24, 25)
        Me.btnHierLOV.Name = "btnHierLOV"
        Me.btnHierLOV.Size = New System.Drawing.Size(24, 25)
        Me.btnHierLOV.TabIndex = 6
        Me.btnHierLOV.UseVisualStyleBackColor = False
        '
        'cmbDepart
        '
        Me.cmbDepart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbDepart.Location = New System.Drawing.Point(419, 86)
        Me.cmbDepart.Name = "cmbDepart"
        Me.cmbDepart.Properties.MaxLength = 50
        Me.cmbDepart.Properties.ReadOnly = True
        Me.cmbDepart.Size = New System.Drawing.Size(163, 19)
        Me.cmbDepart.TabIndex = 5
        '
        'chkSubLevels
        '
        Me.chkSubLevels.AutoSize = True
        Me.chkSubLevels.Location = New System.Drawing.Point(91, 259)
        Me.chkSubLevels.Name = "chkSubLevels"
        Me.chkSubLevels.Size = New System.Drawing.Size(244, 17)
        Me.chkSubLevels.TabIndex = 9
        Me.chkSubLevels.Text = "Include Sub  Levels of Locations && Categories"
        Me.chkSubLevels.UseVisualStyleBackColor = True
        '
        'lblHierarchy
        '
        Me.lblHierarchy.AutoSize = True
        Me.lblHierarchy.BackColor = System.Drawing.Color.Transparent
        Me.lblHierarchy.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHierarchy.Location = New System.Drawing.Point(360, 87)
        Me.lblHierarchy.Name = "lblHierarchy"
        Me.lblHierarchy.Size = New System.Drawing.Size(53, 13)
        Me.lblHierarchy.TabIndex = 81
        Me.lblHierarchy.Text = "Hierarchy"
        '
        'ZTCategory
        '
        Me.ZTCategory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ZTCategory.BackColor = System.Drawing.Color.White
        Me.ZTCategory.DataSource = Nothing
        Me.ZTCategory.DisplayMember = ""
        Me.ZTCategory.ForeColor = System.Drawing.Color.White
        Me.ZTCategory.Location = New System.Drawing.Point(86, 144)
        Me.ZTCategory.Name = "ZTCategory"
        Me.ZTCategory.SelectedText = ""
        Me.ZTCategory.SelectedValue = ""
        Me.ZTCategory.Size = New System.Drawing.Size(522, 25)
        Me.ZTCategory.TabIndex = 8
        Me.ZTCategory.TextReadOnly = False
        Me.ZTCategory.ValueMember = ""
        '
        'ZTLocation
        '
        Me.ZTLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ZTLocation.BackColor = System.Drawing.Color.White
        Me.ZTLocation.DataSource = Nothing
        Me.ZTLocation.DisplayMember = ""
        Me.ZTLocation.ForeColor = System.Drawing.Color.White
        Me.ZTLocation.Location = New System.Drawing.Point(86, 175)
        Me.ZTLocation.Name = "ZTLocation"
        Me.ZTLocation.SelectedText = ""
        Me.ZTLocation.SelectedValue = ""
        Me.ZTLocation.Size = New System.Drawing.Size(522, 25)
        Me.ZTLocation.TabIndex = 7
        Me.ZTLocation.TextReadOnly = False
        Me.ZTLocation.ValueMember = ""
        '
        'chkDisposed
        '
        Me.chkDisposed.AutoSize = True
        Me.chkDisposed.Location = New System.Drawing.Point(91, 286)
        Me.chkDisposed.Name = "chkDisposed"
        Me.chkDisposed.Size = New System.Drawing.Size(158, 17)
        Me.chkDisposed.TabIndex = 10
        Me.chkDisposed.Text = "Show Only Disposed Assets"
        Me.chkDisposed.UseVisualStyleBackColor = True
        '
        'lblBrand
        '
        Me.lblBrand.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBrand.AutoSize = True
        Me.lblBrand.BackColor = System.Drawing.Color.Transparent
        Me.lblBrand.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBrand.Location = New System.Drawing.Point(378, 56)
        Me.lblBrand.Name = "lblBrand"
        Me.lblBrand.Size = New System.Drawing.Size(35, 13)
        Me.lblBrand.TabIndex = 73
        Me.lblBrand.Text = "Brand"
        '
        'cmbSupp
        '
        Me.cmbSupp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbSupp.BackColor = System.Drawing.Color.White
        Me.cmbSupp.DataSource = Nothing
        Me.cmbSupp.DisplayMember = ""
        Me.cmbSupp.ForeColor = System.Drawing.Color.White
        Me.cmbSupp.Location = New System.Drawing.Point(419, 20)
        Me.cmbSupp.Name = "cmbSupp"
        Me.cmbSupp.SelectedIndex = -1
        Me.cmbSupp.SelectedText = ""
        Me.cmbSupp.SelectedValue = ""
        Me.cmbSupp.Size = New System.Drawing.Size(188, 24)
        Me.cmbSupp.TabIndex = 1
        Me.cmbSupp.TextReadOnly = False
        Me.cmbSupp.ValueMember = ""
        '
        'cmbBrand
        '
        Me.cmbBrand.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbBrand.BackColor = System.Drawing.Color.White
        Me.cmbBrand.DataSource = Nothing
        Me.cmbBrand.DisplayMember = ""
        Me.cmbBrand.ForeColor = System.Drawing.Color.White
        Me.cmbBrand.Location = New System.Drawing.Point(419, 54)
        Me.cmbBrand.Name = "cmbBrand"
        Me.cmbBrand.SelectedIndex = -1
        Me.cmbBrand.SelectedText = ""
        Me.cmbBrand.SelectedValue = ""
        Me.cmbBrand.Size = New System.Drawing.Size(187, 24)
        Me.cmbBrand.TabIndex = 3
        Me.cmbBrand.TextReadOnly = False
        Me.cmbBrand.ValueMember = ""
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(442, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(21, 13)
        Me.Label8.TabIndex = 71
        Me.Label8.Text = "To"
        '
        'lblExpectedDepreciation
        '
        Me.lblExpectedDepreciation.AutoSize = True
        Me.lblExpectedDepreciation.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExpectedDepreciation.Location = New System.Drawing.Point(88, 314)
        Me.lblExpectedDepreciation.Name = "lblExpectedDepreciation"
        Me.lblExpectedDepreciation.Size = New System.Drawing.Size(42, 13)
        Me.lblExpectedDepreciation.TabIndex = 71
        Me.lblExpectedDepreciation.Text = "As On:"
        Me.lblExpectedDepreciation.Visible = False
        '
        'dtToDT
        '
        Me.dtToDT.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtToDT.Location = New System.Drawing.Point(472, 12)
        Me.dtToDT.Name = "dtToDT"
        Me.dtToDT.Size = New System.Drawing.Size(121, 20)
        Me.dtToDT.TabIndex = 14
        '
        'dtFrmDt
        '
        Me.dtFrmDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFrmDt.Location = New System.Drawing.Point(274, 12)
        Me.dtFrmDt.Name = "dtFrmDt"
        Me.dtFrmDt.Size = New System.Drawing.Size(121, 20)
        Me.dtFrmDt.TabIndex = 13
        '
        'dtExpectedDepreciation
        '
        Me.dtExpectedDepreciation.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtExpectedDepreciation.Location = New System.Drawing.Point(147, 311)
        Me.dtExpectedDepreciation.Name = "dtExpectedDepreciation"
        Me.dtExpectedDepreciation.Size = New System.Drawing.Size(121, 20)
        Me.dtExpectedDepreciation.TabIndex = 12
        Me.dtExpectedDepreciation.Visible = False
        '
        'cmbBook
        '
        Me.cmbBook.BackColor = System.Drawing.Color.White
        Me.cmbBook.DataSource = Nothing
        Me.cmbBook.DisplayMember = ""
        Me.cmbBook.Location = New System.Drawing.Point(85, 50)
        Me.cmbBook.Name = "cmbBook"
        Me.cmbBook.SelectedIndex = -1
        Me.cmbBook.SelectedText = ""
        Me.cmbBook.SelectedValue = ""
        Me.cmbBook.Size = New System.Drawing.Size(201, 24)
        Me.cmbBook.TabIndex = 2
        Me.cmbBook.TextReadOnly = False
        Me.cmbBook.ValueMember = ""
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
        Me.cmbComp.Size = New System.Drawing.Size(201, 24)
        Me.cmbComp.TabIndex = 0
        Me.cmbComp.TextReadOnly = False
        Me.cmbComp.ValueMember = ""
        '
        'cmbCust
        '
        Me.cmbCust.BackColor = System.Drawing.Color.White
        Me.cmbCust.DataSource = Nothing
        Me.cmbCust.DisplayMember = ""
        Me.cmbCust.Location = New System.Drawing.Point(85, 82)
        Me.cmbCust.Name = "cmbCust"
        Me.cmbCust.SelectedIndex = -1
        Me.cmbCust.SelectedText = ""
        Me.cmbCust.SelectedValue = ""
        Me.cmbCust.Size = New System.Drawing.Size(201, 25)
        Me.cmbCust.TabIndex = 4
        Me.cmbCust.TextReadOnly = False
        Me.cmbCust.ValueMember = ""
        '
        'lblCompany
        '
        Me.lblCompany.AutoSize = True
        Me.lblCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblCompany.Location = New System.Drawing.Point(28, 21)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(51, 13)
        Me.lblCompany.TabIndex = 61
        Me.lblCompany.Text = "Company"
        '
        'lblBook
        '
        Me.lblBook.AutoSize = True
        Me.lblBook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblBook.Location = New System.Drawing.Point(47, 55)
        Me.lblBook.Name = "lblBook"
        Me.lblBook.Size = New System.Drawing.Size(32, 13)
        Me.lblBook.TabIndex = 61
        Me.lblBook.Text = "Book"
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblCust.Location = New System.Drawing.Point(25, 86)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.Size = New System.Drawing.Size(54, 13)
        Me.lblCust.TabIndex = 61
        Me.lblCust.Text = "Custodian"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(34, 178)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(47, 13)
        Me.Label19.TabIndex = 59
        Me.Label19.Text = "Location"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(29, 148)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Category"
        '
        'btnGen
        '
        Me.btnGen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGen.Location = New System.Drawing.Point(369, 543)
        Me.btnGen.Name = "btnGen"
        Me.btnGen.Size = New System.Drawing.Size(115, 27)
        Me.btnGen.TabIndex = 2
        Me.btnGen.Text = "&Generate Report"
        Me.btnGen.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Image = Global.ZulAssets.My.Resources.Icons.Refresh
        Me.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefresh.Location = New System.Drawing.Point(230, 543)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(115, 27)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "&Refresh Form"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Image = Global.ZulAssets.My.Resources.Icons.Close
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(529, 543)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(84, 27)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "&Close"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'nvControl
        '
        Me.nvControl.ActiveGroup = Me.nvGroupAuditStatus
        Me.nvControl.AllowSelectedLink = True
        Me.nvControl.Appearance.Item.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nvControl.Appearance.Item.Options.UseFont = True
        Me.nvControl.ContentButtonHint = Nothing
        Me.nvControl.Dock = System.Windows.Forms.DockStyle.Left
        Me.nvControl.Groups.AddRange(New DevExpress.XtraNavBar.NavBarGroup() {Me.nvGroupStandard, Me.nvGroupAuditStatus, Me.nvGroupExtended, Me.nvGroupMaster})
        Me.nvControl.Items.AddRange(New DevExpress.XtraNavBar.NavBarItem() {Me.nvItemCompanyAsset, Me.nvItemAnonymous, Me.nvItemDepreciationBooks, Me.nvItemExpectedDepr, Me.nvItemAssetTag, Me.nvItemAssetLedger, Me.nvItemInventory, Me.nvItemAssetRegister, Me.NavBarItem4, Me.navItemDesignation, Me.nvItemCustodian, Me.nvItemBrand, Me.nvitemInsurer, Me.nvItemSupplier, Me.nvItemDisposalMethods, Me.nvItemDepreciationMethods, Me.nvAssetItems, Me.nvItemAssetBooks, Me.nvItemInventSch, Me.nvItemAddressBook, Me.nvItemNewTags, Me.nvItemAssetDetails, Me.nvItemDisposed, Me.nvItemAssetByCat, Me.nvItemAssetsBySubCat, Me.nvItemMissingAssets, Me.nvItemFoundAssets, Me.nvItemMisplaced, Me.nvItemTransferredAssets, Me.nvItemAllocatedAssets, Me.nvItemAllAssets, Me.ntAssetLog, Me.nvCostCenterAudit})
        Me.nvControl.Location = New System.Drawing.Point(0, 0)
        Me.nvControl.Name = "nvControl"
        Me.nvControl.OptionsNavPane.ExpandedWidth = 140
        Me.nvControl.Size = New System.Drawing.Size(203, 602)
        Me.nvControl.SmallImages = Me.imgListLarge
        Me.nvControl.StoreDefaultPaintStyleName = True
        Me.nvControl.TabIndex = 77
        Me.nvControl.Text = "Select Report"
        '
        'nvGroupAuditStatus
        '
        Me.nvGroupAuditStatus.Caption = "Audit Status"
        Me.nvGroupAuditStatus.Expanded = True
        Me.nvGroupAuditStatus.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemAnonymous), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemMissingAssets), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemFoundAssets), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemMisplaced), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemTransferredAssets), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemAllocatedAssets), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemAllAssets), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvCostCenterAudit)})
        Me.nvGroupAuditStatus.Name = "nvGroupAuditStatus"
        Me.nvGroupAuditStatus.SelectedLinkIndex = 7
        '
        'nvItemAnonymous
        '
        Me.nvItemAnonymous.Caption = "Anonymous Assets"
        Me.nvItemAnonymous.LargeImageIndex = 3
        Me.nvItemAnonymous.Name = "nvItemAnonymous"
        Me.nvItemAnonymous.SmallImageIndex = 3
        '
        'nvItemMissingAssets
        '
        Me.nvItemMissingAssets.Caption = "Missing Assets"
        Me.nvItemMissingAssets.Name = "nvItemMissingAssets"
        Me.nvItemMissingAssets.SmallImageIndex = 3
        '
        'nvItemFoundAssets
        '
        Me.nvItemFoundAssets.Caption = "Found Assets"
        Me.nvItemFoundAssets.Name = "nvItemFoundAssets"
        Me.nvItemFoundAssets.SmallImageIndex = 3
        '
        'nvItemMisplaced
        '
        Me.nvItemMisplaced.Caption = "Misplaced Assets"
        Me.nvItemMisplaced.Name = "nvItemMisplaced"
        Me.nvItemMisplaced.SmallImageIndex = 3
        '
        'nvItemTransferredAssets
        '
        Me.nvItemTransferredAssets.Caption = "Transferred Assets"
        Me.nvItemTransferredAssets.Name = "nvItemTransferredAssets"
        Me.nvItemTransferredAssets.SmallImageIndex = 3
        '
        'nvItemAllocatedAssets
        '
        Me.nvItemAllocatedAssets.Caption = "Allocated Assets"
        Me.nvItemAllocatedAssets.Name = "nvItemAllocatedAssets"
        Me.nvItemAllocatedAssets.SmallImageIndex = 3
        '
        'nvItemAllAssets
        '
        Me.nvItemAllAssets.Caption = "All Assets"
        Me.nvItemAllAssets.Name = "nvItemAllAssets"
        Me.nvItemAllAssets.SmallImageIndex = 3
        '
        'nvCostCenterAudit
        '
        Me.nvCostCenterAudit.Caption = "Cost Center Audit"
        Me.nvCostCenterAudit.Name = "nvCostCenterAudit"
        Me.nvCostCenterAudit.SmallImageIndex = 3
        '
        'nvGroupStandard
        '
        Me.nvGroupStandard.Caption = "Standard Reports"
        Me.nvGroupStandard.Expanded = True
        Me.nvGroupStandard.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.SmallIconsText
        Me.nvGroupStandard.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemCompanyAsset), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemDepreciationBooks), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemExpectedDepr), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemAssetTag), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemAssetLedger), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemInventory), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemAssetRegister), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemNewTags), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemAssetDetails), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemDisposed), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemAssetByCat), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemAssetsBySubCat), New DevExpress.XtraNavBar.NavBarItemLink(Me.ntAssetLog)})
        Me.nvGroupStandard.Name = "nvGroupStandard"
        '
        'nvItemCompanyAsset
        '
        Me.nvItemCompanyAsset.Caption = "Company Assets"
        Me.nvItemCompanyAsset.Name = "nvItemCompanyAsset"
        Me.nvItemCompanyAsset.SmallImageIndex = 3
        '
        'nvItemDepreciationBooks
        '
        Me.nvItemDepreciationBooks.Caption = "Depreciation Books"
        Me.nvItemDepreciationBooks.LargeImageIndex = 4
        Me.nvItemDepreciationBooks.Name = "nvItemDepreciationBooks"
        Me.nvItemDepreciationBooks.SmallImageIndex = 3
        Me.nvItemDepreciationBooks.Tag = "InvReport"
        '
        'nvItemExpectedDepr
        '
        Me.nvItemExpectedDepr.Caption = "Expected Depreciation"
        Me.nvItemExpectedDepr.LargeImageIndex = 4
        Me.nvItemExpectedDepr.Name = "nvItemExpectedDepr"
        Me.nvItemExpectedDepr.SmallImageIndex = 3
        Me.nvItemExpectedDepr.Tag = "InvReport"
        '
        'nvItemAssetTag
        '
        Me.nvItemAssetTag.Caption = "Assets Tagging"
        Me.nvItemAssetTag.LargeImageIndex = 4
        Me.nvItemAssetTag.Name = "nvItemAssetTag"
        Me.nvItemAssetTag.SmallImageIndex = 3
        Me.nvItemAssetTag.Tag = "InvReport"
        '
        'nvItemAssetLedger
        '
        Me.nvItemAssetLedger.Caption = "Assets Ledger"
        Me.nvItemAssetLedger.Name = "nvItemAssetLedger"
        Me.nvItemAssetLedger.SmallImageIndex = 3
        '
        'nvItemInventory
        '
        Me.nvItemInventory.Caption = "Items Inventory"
        Me.nvItemInventory.Name = "nvItemInventory"
        Me.nvItemInventory.SmallImageIndex = 3
        '
        'nvItemAssetRegister
        '
        Me.nvItemAssetRegister.Caption = "Assets Register"
        Me.nvItemAssetRegister.Name = "nvItemAssetRegister"
        Me.nvItemAssetRegister.SmallImageIndex = 3
        '
        'nvItemNewTags
        '
        Me.nvItemNewTags.Caption = "New Tags"
        Me.nvItemNewTags.Name = "nvItemNewTags"
        Me.nvItemNewTags.SmallImageIndex = 3
        '
        'nvItemAssetDetails
        '
        Me.nvItemAssetDetails.Caption = "Asset Details"
        Me.nvItemAssetDetails.Name = "nvItemAssetDetails"
        Me.nvItemAssetDetails.SmallImageIndex = 3
        '
        'nvItemDisposed
        '
        Me.nvItemDisposed.Caption = "Disposed Assets"
        Me.nvItemDisposed.Name = "nvItemDisposed"
        Me.nvItemDisposed.SmallImageIndex = 3
        '
        'nvItemAssetByCat
        '
        Me.nvItemAssetByCat.Caption = "Assets by Category"
        Me.nvItemAssetByCat.Name = "nvItemAssetByCat"
        Me.nvItemAssetByCat.SmallImageIndex = 3
        '
        'nvItemAssetsBySubCat
        '
        Me.nvItemAssetsBySubCat.Caption = "Assets by Subcategory"
        Me.nvItemAssetsBySubCat.Name = "nvItemAssetsBySubCat"
        Me.nvItemAssetsBySubCat.SmallImageIndex = 3
        '
        'ntAssetLog
        '
        Me.ntAssetLog.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ntAssetLog.Caption = "Assets Log"
        Me.ntAssetLog.Name = "ntAssetLog"
        Me.ntAssetLog.SmallImageIndex = 3
        '
        'nvGroupExtended
        '
        Me.nvGroupExtended.Caption = "Extended Reports"
        Me.nvGroupExtended.Expanded = True
        Me.nvGroupExtended.Name = "nvGroupExtended"
        '
        'nvGroupMaster
        '
        Me.nvGroupMaster.Caption = "Master Reports"
        Me.nvGroupMaster.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.navItemDesignation), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemCustodian), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemBrand), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvitemInsurer), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemSupplier), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemDisposalMethods), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemDepreciationMethods), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvAssetItems), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemAssetBooks), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemInventSch), New DevExpress.XtraNavBar.NavBarItemLink(Me.nvItemAddressBook)})
        Me.nvGroupMaster.Name = "nvGroupMaster"
        '
        'navItemDesignation
        '
        Me.navItemDesignation.Caption = "Designations"
        Me.navItemDesignation.Name = "navItemDesignation"
        Me.navItemDesignation.SmallImageIndex = 4
        '
        'nvItemCustodian
        '
        Me.nvItemCustodian.Caption = "Custodians"
        Me.nvItemCustodian.Name = "nvItemCustodian"
        Me.nvItemCustodian.SmallImageIndex = 4
        '
        'nvItemBrand
        '
        Me.nvItemBrand.Caption = "Brands"
        Me.nvItemBrand.Name = "nvItemBrand"
        Me.nvItemBrand.SmallImageIndex = 4
        '
        'nvitemInsurer
        '
        Me.nvitemInsurer.Caption = "Insurers"
        Me.nvitemInsurer.Name = "nvitemInsurer"
        Me.nvitemInsurer.SmallImageIndex = 4
        '
        'nvItemSupplier
        '
        Me.nvItemSupplier.Caption = "Suppliers"
        Me.nvItemSupplier.Name = "nvItemSupplier"
        Me.nvItemSupplier.SmallImageIndex = 4
        '
        'nvItemDisposalMethods
        '
        Me.nvItemDisposalMethods.Caption = "Disposal Methods"
        Me.nvItemDisposalMethods.Name = "nvItemDisposalMethods"
        Me.nvItemDisposalMethods.SmallImageIndex = 4
        '
        'nvItemDepreciationMethods
        '
        Me.nvItemDepreciationMethods.Caption = "Depreciation Methods"
        Me.nvItemDepreciationMethods.Name = "nvItemDepreciationMethods"
        Me.nvItemDepreciationMethods.SmallImageIndex = 4
        '
        'nvAssetItems
        '
        Me.nvAssetItems.Caption = "Asset Items"
        Me.nvAssetItems.Name = "nvAssetItems"
        Me.nvAssetItems.SmallImageIndex = 4
        '
        'nvItemAssetBooks
        '
        Me.nvItemAssetBooks.Caption = "Asset Books"
        Me.nvItemAssetBooks.Name = "nvItemAssetBooks"
        Me.nvItemAssetBooks.SmallImageIndex = 4
        '
        'nvItemInventSch
        '
        Me.nvItemInventSch.Caption = "Inventory Schedules"
        Me.nvItemInventSch.Name = "nvItemInventSch"
        Me.nvItemInventSch.SmallImageIndex = 4
        '
        'nvItemAddressBook
        '
        Me.nvItemAddressBook.Caption = "Address Book"
        Me.nvItemAddressBook.Name = "nvItemAddressBook"
        Me.nvItemAddressBook.SmallImageIndex = 4
        '
        'NavBarItem4
        '
        Me.NavBarItem4.Caption = "NavBarItem4"
        Me.NavBarItem4.Name = "NavBarItem4"
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
        'tabControl
        '
        Me.tabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabControl.Location = New System.Drawing.Point(203, 0)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedTabPage = Me.tabFilter
        Me.tabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[True]
        Me.tabControl.Size = New System.Drawing.Size(634, 602)
        Me.tabControl.TabIndex = 78
        Me.tabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabFilter, Me.tabPreviewReport, Me.tabAuditStatus, Me.tabCategory, Me.tabAssetDetails})
        '
        'tabFilter
        '
        Me.tabFilter.Controls.Add(Me.btnRefreshExtended)
        Me.tabFilter.Controls.Add(Me.grpDisposedReport)
        Me.tabFilter.Controls.Add(Me.grpFilterByData)
        Me.tabFilter.Controls.Add(Me.grpFilterByDate)
        Me.tabFilter.Controls.Add(Me.PictureBox1)
        Me.tabFilter.Controls.Add(Me.lblReportName)
        Me.tabFilter.Controls.Add(Me.btnExit)
        Me.tabFilter.Controls.Add(Me.btnGen)
        Me.tabFilter.Controls.Add(Me.btnRefresh)
        Me.tabFilter.Name = "tabFilter"
        Me.tabFilter.Size = New System.Drawing.Size(625, 571)
        Me.tabFilter.Text = "Filter"
        '
        'btnRefreshExtended
        '
        Me.btnRefreshExtended.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefreshExtended.Image = Global.ZulAssets.My.Resources.Icons.Refresh
        Me.btnRefreshExtended.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefreshExtended.Location = New System.Drawing.Point(88, 543)
        Me.btnRefreshExtended.Name = "btnRefreshExtended"
        Me.btnRefreshExtended.Size = New System.Drawing.Size(136, 27)
        Me.btnRefreshExtended.TabIndex = 107
        Me.btnRefreshExtended.Text = "&Refresh Reports"
        Me.btnRefreshExtended.UseVisualStyleBackColor = True
        '
        'grpDisposedReport
        '
        Me.grpDisposedReport.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpDisposedReport.BackColor = System.Drawing.Color.White
        Me.grpDisposedReport.Controls.Add(Me.Label14)
        Me.grpDisposedReport.Controls.Add(Me.Label7)
        Me.grpDisposedReport.Controls.Add(Me.chkFilterByDisposeDate)
        Me.grpDisposedReport.Controls.Add(Me.dtFromDisposal)
        Me.grpDisposedReport.Controls.Add(Me.dtToDisposal)
        Me.grpDisposedReport.Controls.Add(Me.Label4)
        Me.grpDisposedReport.Controls.Add(Me.cmdDisposal)
        Me.grpDisposedReport.Controls.Add(Me.Label33)
        Me.grpDisposedReport.Location = New System.Drawing.Point(3, 439)
        Me.grpDisposedReport.Name = "grpDisposedReport"
        Me.grpDisposedReport.Size = New System.Drawing.Size(614, 100)
        Me.grpDisposedReport.TabIndex = 106
        Me.grpDisposedReport.TabStop = False
        Me.grpDisposedReport.Text = "Filter Disposed Report"
        Me.grpDisposedReport.Visible = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(109, 66)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(87, 13)
        Me.Label14.TabIndex = 110
        Me.Label14.Text = "Disposed date"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(308, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(237, 13)
        Me.Label7.TabIndex = 109
        Me.Label7.Text = "Select a method or leave it empty for all."
        Me.Label7.Visible = False
        '
        'chkFilterByDisposeDate
        '
        Me.chkFilterByDisposeDate.AutoSize = True
        Me.chkFilterByDisposeDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFilterByDisposeDate.Location = New System.Drawing.Point(213, 65)
        Me.chkFilterByDisposeDate.Name = "chkFilterByDisposeDate"
        Me.chkFilterByDisposeDate.Size = New System.Drawing.Size(55, 17)
        Me.chkFilterByDisposeDate.TabIndex = 108
        Me.chkFilterByDisposeDate.Text = "From"
        Me.chkFilterByDisposeDate.UseVisualStyleBackColor = True
        '
        'dtFromDisposal
        '
        Me.dtFromDisposal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFromDisposal.Location = New System.Drawing.Point(274, 62)
        Me.dtFromDisposal.Name = "dtFromDisposal"
        Me.dtFromDisposal.Size = New System.Drawing.Size(121, 20)
        Me.dtFromDisposal.TabIndex = 105
        '
        'dtToDisposal
        '
        Me.dtToDisposal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtToDisposal.Location = New System.Drawing.Point(472, 62)
        Me.dtToDisposal.Name = "dtToDisposal"
        Me.dtToDisposal.Size = New System.Drawing.Size(121, 20)
        Me.dtToDisposal.TabIndex = 106
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(442, 66)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(21, 13)
        Me.Label4.TabIndex = 107
        Me.Label4.Text = "To"
        '
        'cmdDisposal
        '
        Me.cmdDisposal.BackColor = System.Drawing.Color.White
        Me.cmdDisposal.DataSource = Nothing
        Me.cmdDisposal.DisplayMember = ""
        Me.cmdDisposal.Location = New System.Drawing.Point(103, 19)
        Me.cmdDisposal.Name = "cmdDisposal"
        Me.cmdDisposal.SelectedIndex = -1
        Me.cmdDisposal.SelectedText = ""
        Me.cmdDisposal.SelectedValue = ""
        Me.cmdDisposal.Size = New System.Drawing.Size(197, 24)
        Me.cmdDisposal.TabIndex = 102
        Me.cmdDisposal.TextReadOnly = False
        Me.cmdDisposal.ValueMember = ""
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(12, 24)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(85, 13)
        Me.Label33.TabIndex = 104
        Me.Label33.Text = "Disposal Method"
        '
        'grpFilterByData
        '
        Me.grpFilterByData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpFilterByData.BackColor = System.Drawing.Color.White
        Me.grpFilterByData.Controls.Add(Me.cmbDataSource)
        Me.grpFilterByData.Controls.Add(Me.cmbInventoryStatus)
        Me.grpFilterByData.Controls.Add(Me.Label12)
        Me.grpFilterByData.Controls.Add(Me.cmbAssetStatus)
        Me.grpFilterByData.Controls.Add(Me.Label5)
        Me.grpFilterByData.Controls.Add(Me.txtTotalCost)
        Me.grpFilterByData.Controls.Add(Me.cmbTotalCostFilterType)
        Me.grpFilterByData.Controls.Add(Me.lblCostPrice)
        Me.grpFilterByData.Controls.Add(Me.cmbItemCode)
        Me.grpFilterByData.Controls.Add(Me.Label2)
        Me.grpFilterByData.Controls.Add(Me.lblDataSource)
        Me.grpFilterByData.Controls.Add(Me.lblsupplier)
        Me.grpFilterByData.Controls.Add(Me.btnHierLOV)
        Me.grpFilterByData.Controls.Add(Me.lblExpectedDepreciation)
        Me.grpFilterByData.Controls.Add(Me.dtExpectedDepreciation)
        Me.grpFilterByData.Controls.Add(Me.chkExclude)
        Me.grpFilterByData.Controls.Add(Me.cmbComp)
        Me.grpFilterByData.Controls.Add(Me.lblCompany)
        Me.grpFilterByData.Controls.Add(Me.lblBook)
        Me.grpFilterByData.Controls.Add(Me.cmbCust)
        Me.grpFilterByData.Controls.Add(Me.lblCust)
        Me.grpFilterByData.Controls.Add(Me.cmbDepart)
        Me.grpFilterByData.Controls.Add(Me.Label19)
        Me.grpFilterByData.Controls.Add(Me.cmbBook)
        Me.grpFilterByData.Controls.Add(Me.Label1)
        Me.grpFilterByData.Controls.Add(Me.chkSubLevels)
        Me.grpFilterByData.Controls.Add(Me.cmbBrand)
        Me.grpFilterByData.Controls.Add(Me.lblHierarchy)
        Me.grpFilterByData.Controls.Add(Me.cmbSupp)
        Me.grpFilterByData.Controls.Add(Me.ZTCategory)
        Me.grpFilterByData.Controls.Add(Me.lblBrand)
        Me.grpFilterByData.Controls.Add(Me.ZTLocation)
        Me.grpFilterByData.Controls.Add(Me.chkDisposed)
        Me.grpFilterByData.Location = New System.Drawing.Point(3, 51)
        Me.grpFilterByData.Name = "grpFilterByData"
        Me.grpFilterByData.Size = New System.Drawing.Size(614, 335)
        Me.grpFilterByData.TabIndex = 101
        Me.grpFilterByData.TabStop = False
        Me.grpFilterByData.Text = "Filter by Data"
        '
        'cmbDataSource
        '
        Me.cmbDataSource.Location = New System.Drawing.Point(419, 238)
        Me.cmbDataSource.Name = "cmbDataSource"
        Me.cmbDataSource.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbDataSource.Properties.Items.AddRange(New Object() {"ALL", "ZulAssets", "Oracle System"})
        Me.cmbDataSource.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cmbDataSource.Size = New System.Drawing.Size(188, 19)
        Me.cmbDataSource.TabIndex = 96
        '
        'cmbInventoryStatus
        '
        Me.cmbInventoryStatus.Location = New System.Drawing.Point(419, 207)
        Me.cmbInventoryStatus.Name = "cmbInventoryStatus"
        Me.cmbInventoryStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "Remove", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing)})
        Me.cmbInventoryStatus.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Missing", "0", 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Found", "1", 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Misplaced", "2", 2), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Transfer", "3", 3), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Allocated", "4", 4), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Anonymous", "5", 5)})
        Me.cmbInventoryStatus.Properties.SmallImages = Me.imgAssetStatus
        Me.cmbInventoryStatus.Size = New System.Drawing.Size(188, 19)
        Me.cmbInventoryStatus.TabIndex = 95
        '
        'imgAssetStatus
        '
        Me.imgAssetStatus.ImageStream = CType(resources.GetObject("imgAssetStatus.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgAssetStatus.TransparentColor = System.Drawing.Color.Transparent
        Me.imgAssetStatus.Images.SetKeyName(0, "Wrong.bmp")
        Me.imgAssetStatus.Images.SetKeyName(1, "Right.bmp")
        Me.imgAssetStatus.Images.SetKeyName(2, "Miplaced.bmp")
        Me.imgAssetStatus.Images.SetKeyName(3, "Transfered.bmp")
        Me.imgAssetStatus.Images.SetKeyName(4, "Allocated.bmp")
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(301, 210)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(112, 13)
        Me.Label12.TabIndex = 94
        Me.Label12.Text = "Last Inventory Status"
        '
        'cmbAssetStatus
        '
        Me.cmbAssetStatus.BackColor = System.Drawing.Color.White
        Me.cmbAssetStatus.DataSource = Nothing
        Me.cmbAssetStatus.DisplayMember = ""
        Me.cmbAssetStatus.Location = New System.Drawing.Point(85, 206)
        Me.cmbAssetStatus.Name = "cmbAssetStatus"
        Me.cmbAssetStatus.SelectedIndex = -1
        Me.cmbAssetStatus.SelectedText = ""
        Me.cmbAssetStatus.SelectedValue = ""
        Me.cmbAssetStatus.Size = New System.Drawing.Size(200, 24)
        Me.cmbAssetStatus.TabIndex = 93
        Me.cmbAssetStatus.TextReadOnly = False
        Me.cmbAssetStatus.ValueMember = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 210)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 13)
        Me.Label5.TabIndex = 92
        Me.Label5.Text = "Asset Status"
        '
        'txtTotalCost
        '
        Me.txtTotalCost.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtTotalCost.Location = New System.Drawing.Point(249, 235)
        Me.txtTotalCost.Name = "txtTotalCost"
        Me.txtTotalCost.Properties.Appearance.Options.UseTextOptions = True
        Me.txtTotalCost.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.txtTotalCost.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.txtTotalCost.Properties.MaxValue = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTotalCost.Size = New System.Drawing.Size(93, 19)
        Me.txtTotalCost.TabIndex = 91
        '
        'cmbTotalCostFilterType
        '
        Me.cmbTotalCostFilterType.Location = New System.Drawing.Point(85, 235)
        Me.cmbTotalCostFilterType.Name = "cmbTotalCostFilterType"
        Me.cmbTotalCostFilterType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTotalCostFilterType.Properties.Items.AddRange(New Object() {"None", "Equals", "Does not equal", "Is greater than", "Is greater than or equal to", "Is less than", "Is less than or equal to"})
        Me.cmbTotalCostFilterType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cmbTotalCostFilterType.Size = New System.Drawing.Size(163, 19)
        Me.cmbTotalCostFilterType.TabIndex = 90
        '
        'lblCostPrice
        '
        Me.lblCostPrice.AutoSize = True
        Me.lblCostPrice.Location = New System.Drawing.Point(23, 238)
        Me.lblCostPrice.Name = "lblCostPrice"
        Me.lblCostPrice.Size = New System.Drawing.Size(56, 13)
        Me.lblCostPrice.TabIndex = 89
        Me.lblCostPrice.Text = "Total Cost"
        '
        'cmbItemCode
        '
        Me.cmbItemCode.BackColor = System.Drawing.Color.White
        Me.cmbItemCode.DataSource = Nothing
        Me.cmbItemCode.DisplayMember = ""
        Me.cmbItemCode.Location = New System.Drawing.Point(85, 113)
        Me.cmbItemCode.Name = "cmbItemCode"
        Me.cmbItemCode.SelectedIndex = -1
        Me.cmbItemCode.SelectedText = ""
        Me.cmbItemCode.SelectedValue = ""
        Me.cmbItemCode.Size = New System.Drawing.Size(201, 25)
        Me.cmbItemCode.TabIndex = 85
        Me.cmbItemCode.TextReadOnly = False
        Me.cmbItemCode.ValueMember = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label2.Location = New System.Drawing.Point(52, 117)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 86
        Me.Label2.Text = "Item"
        '
        'lblDataSource
        '
        Me.lblDataSource.AutoSize = True
        Me.lblDataSource.Location = New System.Drawing.Point(373, 241)
        Me.lblDataSource.Name = "lblDataSource"
        Me.lblDataSource.Size = New System.Drawing.Size(40, 13)
        Me.lblDataSource.TabIndex = 83
        Me.lblDataSource.Text = "Source"
        '
        'lblsupplier
        '
        Me.lblsupplier.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblsupplier.AutoSize = True
        Me.lblsupplier.BackColor = System.Drawing.Color.Transparent
        Me.lblsupplier.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsupplier.Location = New System.Drawing.Point(368, 22)
        Me.lblsupplier.Name = "lblsupplier"
        Me.lblsupplier.Size = New System.Drawing.Size(45, 13)
        Me.lblsupplier.TabIndex = 82
        Me.lblsupplier.Text = "Supplier"
        '
        'grpFilterByDate
        '
        Me.grpFilterByDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpFilterByDate.BackColor = System.Drawing.Color.White
        Me.grpFilterByDate.Controls.Add(Me.chkFilterByDate)
        Me.grpFilterByDate.Controls.Add(Me.dtFrmDt)
        Me.grpFilterByDate.Controls.Add(Me.dtToDT)
        Me.grpFilterByDate.Controls.Add(Me.Label8)
        Me.grpFilterByDate.Location = New System.Drawing.Point(3, 390)
        Me.grpFilterByDate.Name = "grpFilterByDate"
        Me.grpFilterByDate.Size = New System.Drawing.Size(614, 43)
        Me.grpFilterByDate.TabIndex = 100
        Me.grpFilterByDate.TabStop = False
        Me.grpFilterByDate.Text = "Filter by purchase Date"
        '
        'chkFilterByDate
        '
        Me.chkFilterByDate.AutoSize = True
        Me.chkFilterByDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFilterByDate.Location = New System.Drawing.Point(213, 15)
        Me.chkFilterByDate.Name = "chkFilterByDate"
        Me.chkFilterByDate.Size = New System.Drawing.Size(55, 17)
        Me.chkFilterByDate.TabIndex = 72
        Me.chkFilterByDate.Text = "From"
        Me.chkFilterByDate.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PictureBox1.Location = New System.Drawing.Point(228, 20)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(25, 25)
        Me.PictureBox1.TabIndex = 99
        Me.PictureBox1.TabStop = False
        '
        'lblReportName
        '
        Me.lblReportName.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblReportName.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportName.Appearance.ForeColor = System.Drawing.Color.Black
        Me.lblReportName.Appearance.Options.UseFont = True
        Me.lblReportName.Appearance.Options.UseForeColor = True
        Me.lblReportName.Location = New System.Drawing.Point(258, 16)
        Me.lblReportName.Name = "lblReportName"
        Me.lblReportName.Size = New System.Drawing.Size(161, 29)
        Me.lblReportName.TabIndex = 98
        Me.lblReportName.Text = "Select Report"
        '
        'tabPreviewReport
        '
        Me.tabPreviewReport.Controls.Add(Me.PreviewControl1)
        Me.tabPreviewReport.Name = "tabPreviewReport"
        Me.tabPreviewReport.Size = New System.Drawing.Size(625, 571)
        Me.tabPreviewReport.Text = "Preview Report"
        '
        'PreviewControl1
        '
        Me.PreviewControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PreviewControl1.Location = New System.Drawing.Point(0, 0)
        Me.PreviewControl1.Name = "PreviewControl1"
        Me.PreviewControl1.Size = New System.Drawing.Size(625, 571)
        Me.PreviewControl1.TabIndex = 0
        '
        'tabAuditStatus
        '
        Me.tabAuditStatus.Controls.Add(Me.imgAuditStatus)
        Me.tabAuditStatus.Controls.Add(Me.lblAuditStatus)
        Me.tabAuditStatus.Controls.Add(Me.btnClose)
        Me.tabAuditStatus.Controls.Add(Me.btnGenAuditStatus)
        Me.tabAuditStatus.Controls.Add(Me.grpInventory)
        Me.tabAuditStatus.Name = "tabAuditStatus"
        Me.tabAuditStatus.Size = New System.Drawing.Size(625, 571)
        Me.tabAuditStatus.Text = "AuditStatusReports"
        '
        'imgAuditStatus
        '
        Me.imgAuditStatus.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.imgAuditStatus.Location = New System.Drawing.Point(164, 26)
        Me.imgAuditStatus.Name = "imgAuditStatus"
        Me.imgAuditStatus.Size = New System.Drawing.Size(25, 25)
        Me.imgAuditStatus.TabIndex = 102
        Me.imgAuditStatus.TabStop = False
        '
        'lblAuditStatus
        '
        Me.lblAuditStatus.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAuditStatus.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAuditStatus.Appearance.ForeColor = System.Drawing.Color.Black
        Me.lblAuditStatus.Appearance.Options.UseFont = True
        Me.lblAuditStatus.Appearance.Options.UseForeColor = True
        Me.lblAuditStatus.Location = New System.Drawing.Point(194, 22)
        Me.lblAuditStatus.Name = "lblAuditStatus"
        Me.lblAuditStatus.Size = New System.Drawing.Size(161, 29)
        Me.lblAuditStatus.TabIndex = 101
        Me.lblAuditStatus.Text = "Select Report"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Image = Global.ZulAssets.My.Resources.Icons.Close
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(523, 529)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 27)
        Me.btnClose.TabIndex = 100
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnGenAuditStatus
        '
        Me.btnGenAuditStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenAuditStatus.Location = New System.Drawing.Point(363, 529)
        Me.btnGenAuditStatus.Name = "btnGenAuditStatus"
        Me.btnGenAuditStatus.Size = New System.Drawing.Size(115, 27)
        Me.btnGenAuditStatus.TabIndex = 99
        Me.btnGenAuditStatus.Text = "&Generate Report"
        Me.btnGenAuditStatus.UseVisualStyleBackColor = True
        '
        'grpInventory
        '
        Me.grpInventory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpInventory.BackColor = System.Drawing.Color.White
        Me.grpInventory.Controls.Add(Me.Label15)
        Me.grpInventory.Controls.Add(Me.cmbCatAudit)
        Me.grpInventory.Controls.Add(Me.Label3)
        Me.grpInventory.Controls.Add(Me.chkIncludeSubAudit)
        Me.grpInventory.Controls.Add(Me.cmbLocationAudit)
        Me.grpInventory.Controls.Add(Me.Label6)
        Me.grpInventory.Controls.Add(Me.cmbSch)
        Me.grpInventory.Controls.Add(Me.Label9)
        Me.grpInventory.Controls.Add(Me.lblEnd)
        Me.grpInventory.Controls.Add(Me.Label10)
        Me.grpInventory.Controls.Add(Me.lblStart)
        Me.grpInventory.Location = New System.Drawing.Point(3, 80)
        Me.grpInventory.Name = "grpInventory"
        Me.grpInventory.Size = New System.Drawing.Size(612, 212)
        Me.grpInventory.TabIndex = 98
        Me.grpInventory.TabStop = False
        Me.grpInventory.Text = "Select Inventory Schedule"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(33, 161)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(52, 13)
        Me.Label15.TabIndex = 96
        Me.Label15.Text = "Category"
        '
        'cmbCatAudit
        '
        Me.cmbCatAudit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbCatAudit.BackColor = System.Drawing.Color.White
        Me.cmbCatAudit.DataSource = Nothing
        Me.cmbCatAudit.DisplayMember = ""
        Me.cmbCatAudit.ForeColor = System.Drawing.Color.White
        Me.cmbCatAudit.Location = New System.Drawing.Point(163, 158)
        Me.cmbCatAudit.Name = "cmbCatAudit"
        Me.cmbCatAudit.SelectedText = ""
        Me.cmbCatAudit.SelectedValue = ""
        Me.cmbCatAudit.Size = New System.Drawing.Size(443, 25)
        Me.cmbCatAudit.TabIndex = 95
        Me.cmbCatAudit.TextReadOnly = False
        Me.cmbCatAudit.ValueMember = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(32, 132)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 94
        Me.Label3.Text = "Location"
        '
        'chkIncludeSubAudit
        '
        Me.chkIncludeSubAudit.AutoSize = True
        Me.chkIncludeSubAudit.Location = New System.Drawing.Point(163, 189)
        Me.chkIncludeSubAudit.Name = "chkIncludeSubAudit"
        Me.chkIncludeSubAudit.Size = New System.Drawing.Size(244, 17)
        Me.chkIncludeSubAudit.TabIndex = 93
        Me.chkIncludeSubAudit.Text = "Include Sub  Levels of Locations && Categories"
        Me.chkIncludeSubAudit.UseVisualStyleBackColor = True
        '
        'cmbLocationAudit
        '
        Me.cmbLocationAudit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbLocationAudit.BackColor = System.Drawing.Color.White
        Me.cmbLocationAudit.DataSource = Nothing
        Me.cmbLocationAudit.DisplayMember = ""
        Me.cmbLocationAudit.ForeColor = System.Drawing.Color.White
        Me.cmbLocationAudit.Location = New System.Drawing.Point(163, 129)
        Me.cmbLocationAudit.Name = "cmbLocationAudit"
        Me.cmbLocationAudit.SelectedText = ""
        Me.cmbLocationAudit.SelectedValue = ""
        Me.cmbLocationAudit.Size = New System.Drawing.Size(443, 25)
        Me.cmbLocationAudit.TabIndex = 92
        Me.cmbLocationAudit.TextReadOnly = False
        Me.cmbLocationAudit.ValueMember = ""
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
        'cmbSch
        '
        Me.cmbSch.BackColor = System.Drawing.Color.White
        Me.cmbSch.DataSource = Nothing
        Me.cmbSch.DisplayMember = ""
        Me.cmbSch.Location = New System.Drawing.Point(163, 22)
        Me.cmbSch.Name = "cmbSch"
        Me.cmbSch.SelectedIndex = -1
        Me.cmbSch.SelectedText = ""
        Me.cmbSch.SelectedValue = ""
        Me.cmbSch.Size = New System.Drawing.Size(190, 24)
        Me.cmbSch.TabIndex = 84
        Me.cmbSch.TextReadOnly = False
        Me.cmbSch.ValueMember = ""
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
        'tabCategory
        '
        Me.tabCategory.Controls.Add(Me.imgCategory)
        Me.tabCategory.Controls.Add(Me.lblCategory)
        Me.tabCategory.Controls.Add(Me.btnGenCatReport)
        Me.tabCategory.Controls.Add(Me.btnRefreshCat)
        Me.tabCategory.Controls.Add(Me.btnCloseCat)
        Me.tabCategory.Controls.Add(Me.Groupbox1)
        Me.tabCategory.Name = "tabCategory"
        Me.tabCategory.Size = New System.Drawing.Size(625, 571)
        Me.tabCategory.Text = "Category"
        '
        'imgCategory
        '
        Me.imgCategory.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.imgCategory.Location = New System.Drawing.Point(168, 28)
        Me.imgCategory.Name = "imgCategory"
        Me.imgCategory.Size = New System.Drawing.Size(25, 25)
        Me.imgCategory.TabIndex = 104
        Me.imgCategory.TabStop = False
        '
        'lblCategory
        '
        Me.lblCategory.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblCategory.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategory.Appearance.ForeColor = System.Drawing.Color.Black
        Me.lblCategory.Appearance.Options.UseFont = True
        Me.lblCategory.Appearance.Options.UseForeColor = True
        Me.lblCategory.Location = New System.Drawing.Point(198, 24)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(161, 29)
        Me.lblCategory.TabIndex = 103
        Me.lblCategory.Text = "Select Report"
        '
        'btnGenCatReport
        '
        Me.btnGenCatReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenCatReport.Location = New System.Drawing.Point(365, 538)
        Me.btnGenCatReport.Name = "btnGenCatReport"
        Me.btnGenCatReport.Size = New System.Drawing.Size(124, 27)
        Me.btnGenCatReport.TabIndex = 80
        Me.btnGenCatReport.Text = "&Generate Report"
        Me.btnGenCatReport.UseVisualStyleBackColor = True
        '
        'btnRefreshCat
        '
        Me.btnRefreshCat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefreshCat.Image = Global.ZulAssets.My.Resources.Icons.Refresh
        Me.btnRefreshCat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefreshCat.Location = New System.Drawing.Point(235, 538)
        Me.btnRefreshCat.Name = "btnRefreshCat"
        Me.btnRefreshCat.Size = New System.Drawing.Size(124, 27)
        Me.btnRefreshCat.TabIndex = 81
        Me.btnRefreshCat.Text = "&Refresh Form"
        Me.btnRefreshCat.UseVisualStyleBackColor = True
        '
        'btnCloseCat
        '
        Me.btnCloseCat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCloseCat.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCloseCat.Image = Global.ZulAssets.My.Resources.Icons.Close
        Me.btnCloseCat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCloseCat.Location = New System.Drawing.Point(495, 538)
        Me.btnCloseCat.Name = "btnCloseCat"
        Me.btnCloseCat.Size = New System.Drawing.Size(124, 27)
        Me.btnCloseCat.TabIndex = 82
        Me.btnCloseCat.Text = "&Close"
        '
        'Groupbox1
        '
        Me.Groupbox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Groupbox1.BackColor = System.Drawing.Color.White
        Me.Groupbox1.Controls.Add(Me.dtFrom)
        Me.Groupbox1.Controls.Add(Me.dtTo)
        Me.Groupbox1.Controls.Add(Me.Label11)
        Me.Groupbox1.Controls.Add(Me.Label13)
        Me.Groupbox1.Location = New System.Drawing.Point(3, 77)
        Me.Groupbox1.Name = "Groupbox1"
        Me.Groupbox1.Size = New System.Drawing.Size(587, 139)
        Me.Groupbox1.TabIndex = 77
        Me.Groupbox1.TabStop = False
        Me.Groupbox1.Text = "Report Filter Criteria"
        '
        'dtFrom
        '
        Me.dtFrom.BackColor = System.Drawing.Color.White
        Me.dtFrom.DataSource = Nothing
        Me.dtFrom.DisplayMember = ""
        Me.dtFrom.Location = New System.Drawing.Point(67, 35)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.SelectedIndex = -1
        Me.dtFrom.SelectedText = ""
        Me.dtFrom.SelectedValue = ""
        Me.dtFrom.Size = New System.Drawing.Size(504, 24)
        Me.dtFrom.TabIndex = 82
        Me.dtFrom.TextReadOnly = False
        Me.dtFrom.ValueMember = ""
        '
        'dtTo
        '
        Me.dtTo.BackColor = System.Drawing.Color.White
        Me.dtTo.DataSource = Nothing
        Me.dtTo.DisplayMember = ""
        Me.dtTo.Location = New System.Drawing.Point(67, 69)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.SelectedIndex = -1
        Me.dtTo.SelectedText = ""
        Me.dtTo.SelectedValue = ""
        Me.dtTo.Size = New System.Drawing.Size(504, 24)
        Me.dtTo.TabIndex = 81
        Me.dtTo.TextReadOnly = False
        Me.dtTo.ValueMember = ""
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(23, 72)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(26, 13)
        Me.Label11.TabIndex = 78
        Me.Label11.Text = "To "
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(23, 35)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(38, 13)
        Me.Label13.TabIndex = 77
        Me.Label13.Text = "From "
        '
        'tabAssetDetails
        '
        Me.tabAssetDetails.Name = "tabAssetDetails"
        Me.tabAssetDetails.Size = New System.Drawing.Size(625, 571)
        Me.tabAssetDetails.Text = "Asset Details"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(3, 25)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 571)
        Me.Splitter1.TabIndex = 2
        Me.Splitter1.TabStop = False
        '
        'frmReportCenter
        '
        Me.AcceptButton = Me.btnGen
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(837, 602)
        Me.Controls.Add(Me.tabControl)
        Me.Controls.Add(Me.nvControl)
        Me.DoubleBuffered = True
        Me.MinimumSize = New System.Drawing.Size(600, 400)
        Me.Name = "frmReportCenter"
        Me.Text = "Reports"
        CType(Me.cmbDepart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nvControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgListLarge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControl.ResumeLayout(False)
        Me.tabFilter.ResumeLayout(False)
        Me.tabFilter.PerformLayout()
        Me.grpDisposedReport.ResumeLayout(False)
        Me.grpDisposedReport.PerformLayout()
        Me.grpFilterByData.ResumeLayout(False)
        Me.grpFilterByData.PerformLayout()
        CType(Me.cmbDataSource.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbInventoryStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalCost.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTotalCostFilterType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFilterByDate.ResumeLayout(False)
        Me.grpFilterByDate.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPreviewReport.ResumeLayout(False)
        Me.tabAuditStatus.ResumeLayout(False)
        Me.tabAuditStatus.PerformLayout()
        CType(Me.imgAuditStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpInventory.ResumeLayout(False)
        Me.grpInventory.PerformLayout()
        CType(Me.lblEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabCategory.ResumeLayout(False)
        Me.tabCategory.PerformLayout()
        CType(Me.imgCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Groupbox1.ResumeLayout(False)
        Me.Groupbox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCust As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnGen As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblCompany As System.Windows.Forms.Label
    Friend WithEvents lblBook As System.Windows.Forms.Label
    Friend WithEvents cmbBook As ZulLOV.ZulLOV
    Friend WithEvents cmbComp As ZulLOV.ZulLOV
    Friend WithEvents cmbCust As ZulLOV.ZulLOV
    Friend WithEvents lblExpectedDepreciation As System.Windows.Forms.Label
    Friend WithEvents dtExpectedDepreciation As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbBrand As ZulLOV.ZulLOV
    Friend WithEvents cmbSupp As ZulLOV.ZulLOV
    Friend WithEvents lblBrand As System.Windows.Forms.Label
    Friend WithEvents chkDisposed As System.Windows.Forms.CheckBox
    Friend WithEvents ZTCategory As ZulTree.ZulTree
    Friend WithEvents ZTLocation As ZulTree.ZulTree
    Friend WithEvents lblHierarchy As System.Windows.Forms.Label
    Friend WithEvents chkSubLevels As System.Windows.Forms.CheckBox
    Friend WithEvents btnHierLOV As System.Windows.Forms.Button
    Friend WithEvents cmbDepart As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtToDT As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtFrmDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkExclude As System.Windows.Forms.CheckBox
    Friend WithEvents imgListLarge As DevExpress.Utils.ImageCollection
    Friend WithEvents nvGroupStandard As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nvItemCompanyAsset As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemAnonymous As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemDepreciationBooks As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemExpectedDepr As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemAssetTag As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemAssetLedger As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemInventory As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemAssetRegister As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvGroupExtended As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nvGroupMaster As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents navItemDesignation As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents NavBarItem4 As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemCustodian As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemBrand As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvitemInsurer As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemSupplier As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemDisposalMethods As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemDepreciationMethods As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvAssetItems As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemAssetBooks As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemInventSch As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemAddressBook As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents tabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tabFilter As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabPreviewReport As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PreviewControl1 As ZulAssets.PreviewControl
    Friend WithEvents nvItemNewTags As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemAssetDetails As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemDisposed As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemAssetByCat As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemAssetsBySubCat As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblReportName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents grpFilterByData As System.Windows.Forms.GroupBox
    Friend WithEvents grpFilterByDate As System.Windows.Forms.GroupBox
    Friend WithEvents cmdDisposal As ZulLOV.ZulLOV
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents nvGroupAuditStatus As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nvItemMissingAssets As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemFoundAssets As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemMisplaced As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemTransferredAssets As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nvItemAllocatedAssets As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents tabAuditStatus As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnGenAuditStatus As System.Windows.Forms.Button
    Friend WithEvents grpInventory As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbSch As ZulLOV.ZulLOV
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblEnd As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblStart As DevExpress.XtraEditors.TextEdit
    Friend WithEvents nvItemAllAssets As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents imgAuditStatus As System.Windows.Forms.PictureBox
    Friend WithEvents lblAuditStatus As DevExpress.XtraEditors.LabelControl
    Public WithEvents nvControl As DevExpress.XtraNavBar.NavBarControl
    Friend WithEvents chkFilterByDate As System.Windows.Forms.CheckBox
    Friend WithEvents grpDisposedReport As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkFilterByDisposeDate As System.Windows.Forms.CheckBox
    Friend WithEvents dtFromDisposal As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtToDisposal As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tabCategory As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Groupbox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtFrom As ZulLOV.ZulLOV
    Friend WithEvents dtTo As ZulLOV.ZulLOV
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnGenCatReport As System.Windows.Forms.Button
    Friend WithEvents btnRefreshCat As System.Windows.Forms.Button
    Friend WithEvents btnCloseCat As System.Windows.Forms.Button
    Friend WithEvents imgCategory As System.Windows.Forms.PictureBox
    Friend WithEvents lblCategory As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tabAssetDetails As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblsupplier As System.Windows.Forms.Label
    Friend WithEvents btnRefreshExtended As System.Windows.Forms.Button
    Friend WithEvents lblDataSource As System.Windows.Forms.Label
    Friend WithEvents cmbItemCode As ZulLOV.ZulLOV
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblCostPrice As System.Windows.Forms.Label
    Friend WithEvents cmbTotalCostFilterType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtTotalCost As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbAssetStatus As ZulLOV.ZulLOV
    Friend WithEvents cmbInventoryStatus As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbDataSource As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents imgAssetStatus As System.Windows.Forms.ImageList
    Friend WithEvents ntAssetLog As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkIncludeSubAudit As System.Windows.Forms.CheckBox
    Friend WithEvents cmbLocationAudit As ZulTree.ZulTree
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cmbCatAudit As ZulTree.ZulTree
    Friend WithEvents nvCostCenterAudit As DevExpress.XtraNavBar.NavBarItem
End Class
