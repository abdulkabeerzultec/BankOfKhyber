<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblServerName = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblDBName = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblINS = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblNUM = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblCAPS = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel4 = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblMessages = New System.Windows.Forms.ToolStripStatusLabel
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuImportFile = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuExportFile = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSecurity = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuApplicationUsers = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuChangePassword = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuUserRoles = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMasterData = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuDesgination = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuAddressTemplates = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuCustodians = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSep1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuBrands = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuInsurers = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSuppliers = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuGLCode = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuCostCenter = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuUnits = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSep2 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuDisposalMethods = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuDepreciationMethods = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSep3 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuAssetItems = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuAssetBooks = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuAssetCat = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuInvSch = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuCompanyProfile = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuCompanies = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSep4 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuLocations = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuDepPolicy = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuBarcodePolicy = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuAssetCoding = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSep5 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuOrgLevels = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuOrgGroups = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuOrgHier = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuAssets = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSearch = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuDetailsMainten = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSep7 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuLocCustTrans = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuInterCompTrans = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuAdmin = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuPurchOrder = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuPOPrepar = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuPOApprovals = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuAssetsInTransit = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuTools = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuDeviceConfig = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuSystemConfig = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuCompanyInfo = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuDepEngine = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuBarcodeStruct = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuOfflineMachines = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuBackendInventory = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReports = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportCenter = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuRptDesigner = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuCreateReport = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuCommunication = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSend = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReceive = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuDataProcess = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuUserGuide = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.mnuWindow = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuCascad = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuTileHorizontal = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuTileVertical = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuCloseALL = New System.Windows.Forms.ToolStripMenuItem
        Me.imgAssetStatus = New System.Windows.Forms.ImageList(Me.components)
        Me.mnuWarrantyAlarm = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel3, Me.lblServerName, Me.lblDBName, Me.lblINS, Me.lblNUM, Me.lblCAPS, Me.ToolStripStatusLabel4, Me.lblMessages})
        Me.StatusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 548)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StatusStrip1.Size = New System.Drawing.Size(792, 25)
        Me.StatusStrip1.TabIndex = 7
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(10, 20)
        Me.ToolStripStatusLabel3.Text = " "
        '
        'lblServerName
        '
        Me.lblServerName.BorderSides = CType(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblServerName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.lblServerName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServerName.ForeColor = System.Drawing.Color.Black
        Me.lblServerName.Image = Global.ZulAssets.My.Resources.Icons.server
        Me.lblServerName.Name = "lblServerName"
        Me.lblServerName.Size = New System.Drawing.Size(97, 20)
        Me.lblServerName.Text = "ServerName"
        '
        'lblDBName
        '
        Me.lblDBName.BorderSides = CType(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblDBName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.lblDBName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDBName.ForeColor = System.Drawing.Color.Black
        Me.lblDBName.Image = Global.ZulAssets.My.Resources.Icons.DB
        Me.lblDBName.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.lblDBName.Name = "lblDBName"
        Me.lblDBName.Size = New System.Drawing.Size(69, 20)
        Me.lblDBName.Text = "DBName"
        '
        'lblINS
        '
        Me.lblINS.BorderSides = CType(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblINS.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.lblINS.DoubleClickEnabled = True
        Me.lblINS.Name = "lblINS"
        Me.lblINS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblINS.Size = New System.Drawing.Size(41, 20)
        Me.lblINS.Text = "  INS  "
        '
        'lblNUM
        '
        Me.lblNUM.BorderSides = CType(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblNUM.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.lblNUM.DoubleClickEnabled = True
        Me.lblNUM.Name = "lblNUM"
        Me.lblNUM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNUM.Size = New System.Drawing.Size(51, 20)
        Me.lblNUM.Text = "  NUM  "
        '
        'lblCAPS
        '
        Me.lblCAPS.BorderSides = CType(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblCAPS.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.lblCAPS.DoubleClickEnabled = True
        Me.lblCAPS.Name = "lblCAPS"
        Me.lblCAPS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCAPS.Size = New System.Drawing.Size(46, 20)
        Me.lblCAPS.Text = " CAPS "
        '
        'ToolStripStatusLabel4
        '
        Me.ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
        Me.ToolStripStatusLabel4.Size = New System.Drawing.Size(61, 20)
        Me.ToolStripStatusLabel4.Text = "Messages:"
        Me.ToolStripStatusLabel4.Visible = False
        '
        'lblMessages
        '
        Me.lblMessages.Name = "lblMessages"
        Me.lblMessages.Size = New System.Drawing.Size(0, 20)
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuImportFile, Me.mnuExportFile, Me.ToolStripSeparator8, Me.mnuExit})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuFile.Text = "&File"
        '
        'mnuImportFile
        '
        Me.mnuImportFile.Name = "mnuImportFile"
        Me.mnuImportFile.Size = New System.Drawing.Size(162, 22)
        Me.mnuImportFile.Text = "&Import From File"
        '
        'mnuExportFile
        '
        Me.mnuExportFile.Name = "mnuExportFile"
        Me.mnuExportFile.Size = New System.Drawing.Size(162, 22)
        Me.mnuExportFile.Text = "&Export To File"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(159, 6)
        '
        'mnuExit
        '
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.Size = New System.Drawing.Size(162, 22)
        Me.mnuExit.Text = "&Exit"
        '
        'mnuSecurity
        '
        Me.mnuSecurity.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuApplicationUsers, Me.mnuChangePassword, Me.ToolStripSeparator6, Me.mnuUserRoles})
        Me.mnuSecurity.Name = "mnuSecurity"
        Me.mnuSecurity.Size = New System.Drawing.Size(61, 20)
        Me.mnuSecurity.Text = "&Security"
        '
        'mnuApplicationUsers
        '
        Me.mnuApplicationUsers.Name = "mnuApplicationUsers"
        Me.mnuApplicationUsers.Size = New System.Drawing.Size(168, 22)
        Me.mnuApplicationUsers.Text = "Application Users"
        '
        'mnuChangePassword
        '
        Me.mnuChangePassword.Name = "mnuChangePassword"
        Me.mnuChangePassword.Size = New System.Drawing.Size(168, 22)
        Me.mnuChangePassword.Text = "Change Password"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(165, 6)
        '
        'mnuUserRoles
        '
        Me.mnuUserRoles.Name = "mnuUserRoles"
        Me.mnuUserRoles.Size = New System.Drawing.Size(168, 22)
        Me.mnuUserRoles.Text = "User Roles"
        '
        'mnuMasterData
        '
        Me.mnuMasterData.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDesgination, Me.mnuAddressTemplates, Me.mnuCustodians, Me.mnuSep1, Me.mnuBrands, Me.mnuInsurers, Me.mnuSuppliers, Me.mnuGLCode, Me.mnuCostCenter, Me.mnuUnits, Me.mnuSep2, Me.mnuDisposalMethods, Me.mnuDepreciationMethods, Me.mnuSep3, Me.mnuAssetItems, Me.mnuAssetBooks, Me.mnuAssetCat, Me.mnuInvSch})
        Me.mnuMasterData.Name = "mnuMasterData"
        Me.mnuMasterData.Size = New System.Drawing.Size(82, 20)
        Me.mnuMasterData.Text = "&Master Data"
        '
        'mnuDesgination
        '
        Me.mnuDesgination.Name = "mnuDesgination"
        Me.mnuDesgination.Size = New System.Drawing.Size(191, 22)
        Me.mnuDesgination.Text = "Designations"
        '
        'mnuAddressTemplates
        '
        Me.mnuAddressTemplates.Name = "mnuAddressTemplates"
        Me.mnuAddressTemplates.Size = New System.Drawing.Size(191, 22)
        Me.mnuAddressTemplates.Text = "Address Templates"
        '
        'mnuCustodians
        '
        Me.mnuCustodians.Name = "mnuCustodians"
        Me.mnuCustodians.Size = New System.Drawing.Size(191, 22)
        Me.mnuCustodians.Text = "Custodians"
        '
        'mnuSep1
        '
        Me.mnuSep1.Name = "mnuSep1"
        Me.mnuSep1.Size = New System.Drawing.Size(188, 6)
        '
        'mnuBrands
        '
        Me.mnuBrands.Name = "mnuBrands"
        Me.mnuBrands.Size = New System.Drawing.Size(191, 22)
        Me.mnuBrands.Text = "Brands"
        '
        'mnuInsurers
        '
        Me.mnuInsurers.Name = "mnuInsurers"
        Me.mnuInsurers.Size = New System.Drawing.Size(191, 22)
        Me.mnuInsurers.Text = "Insurers"
        '
        'mnuSuppliers
        '
        Me.mnuSuppliers.Name = "mnuSuppliers"
        Me.mnuSuppliers.Size = New System.Drawing.Size(191, 22)
        Me.mnuSuppliers.Text = "Suppliers"
        '
        'mnuGLCode
        '
        Me.mnuGLCode.Name = "mnuGLCode"
        Me.mnuGLCode.Size = New System.Drawing.Size(191, 22)
        Me.mnuGLCode.Text = "GL Codes"
        '
        'mnuCostCenter
        '
        Me.mnuCostCenter.Name = "mnuCostCenter"
        Me.mnuCostCenter.Size = New System.Drawing.Size(191, 22)
        Me.mnuCostCenter.Text = "Cost Centers"
        '
        'mnuUnits
        '
        Me.mnuUnits.Name = "mnuUnits"
        Me.mnuUnits.Size = New System.Drawing.Size(191, 22)
        Me.mnuUnits.Text = "Units"
        '
        'mnuSep2
        '
        Me.mnuSep2.Name = "mnuSep2"
        Me.mnuSep2.Size = New System.Drawing.Size(188, 6)
        '
        'mnuDisposalMethods
        '
        Me.mnuDisposalMethods.Name = "mnuDisposalMethods"
        Me.mnuDisposalMethods.Size = New System.Drawing.Size(191, 22)
        Me.mnuDisposalMethods.Text = "Disposal Methods"
        '
        'mnuDepreciationMethods
        '
        Me.mnuDepreciationMethods.Name = "mnuDepreciationMethods"
        Me.mnuDepreciationMethods.Size = New System.Drawing.Size(191, 22)
        Me.mnuDepreciationMethods.Text = "Depreciation Methods"
        '
        'mnuSep3
        '
        Me.mnuSep3.Name = "mnuSep3"
        Me.mnuSep3.Size = New System.Drawing.Size(188, 6)
        '
        'mnuAssetItems
        '
        Me.mnuAssetItems.Name = "mnuAssetItems"
        Me.mnuAssetItems.Size = New System.Drawing.Size(191, 22)
        Me.mnuAssetItems.Text = "Asset Items"
        '
        'mnuAssetBooks
        '
        Me.mnuAssetBooks.Name = "mnuAssetBooks"
        Me.mnuAssetBooks.Size = New System.Drawing.Size(191, 22)
        Me.mnuAssetBooks.Text = "Asset Books"
        '
        'mnuAssetCat
        '
        Me.mnuAssetCat.Name = "mnuAssetCat"
        Me.mnuAssetCat.Size = New System.Drawing.Size(191, 22)
        Me.mnuAssetCat.Text = "Asset Categories"
        '
        'mnuInvSch
        '
        Me.mnuInvSch.Name = "mnuInvSch"
        Me.mnuInvSch.Size = New System.Drawing.Size(191, 22)
        Me.mnuInvSch.Text = "Inventory Schedules"
        '
        'mnuCompanyProfile
        '
        Me.mnuCompanyProfile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCompanies, Me.mnuSep4, Me.mnuLocations, Me.mnuDepPolicy, Me.mnuBarcodePolicy, Me.mnuAssetCoding, Me.mnuSep5, Me.mnuOrgLevels, Me.mnuOrgGroups, Me.mnuOrgHier})
        Me.mnuCompanyProfile.Name = "mnuCompanyProfile"
        Me.mnuCompanyProfile.Size = New System.Drawing.Size(108, 20)
        Me.mnuCompanyProfile.Text = "&Company Profile"
        '
        'mnuCompanies
        '
        Me.mnuCompanies.Name = "mnuCompanies"
        Me.mnuCompanies.Size = New System.Drawing.Size(205, 22)
        Me.mnuCompanies.Text = "Companies"
        '
        'mnuSep4
        '
        Me.mnuSep4.Name = "mnuSep4"
        Me.mnuSep4.Size = New System.Drawing.Size(202, 6)
        '
        'mnuLocations
        '
        Me.mnuLocations.Name = "mnuLocations"
        Me.mnuLocations.Size = New System.Drawing.Size(205, 22)
        Me.mnuLocations.Text = "Locations"
        '
        'mnuDepPolicy
        '
        Me.mnuDepPolicy.Name = "mnuDepPolicy"
        Me.mnuDepPolicy.Size = New System.Drawing.Size(205, 22)
        Me.mnuDepPolicy.Text = "Depreciation Policy"
        '
        'mnuBarcodePolicy
        '
        Me.mnuBarcodePolicy.Name = "mnuBarcodePolicy"
        Me.mnuBarcodePolicy.Size = New System.Drawing.Size(205, 22)
        Me.mnuBarcodePolicy.Text = "Barcoding Policy"
        '
        'mnuAssetCoding
        '
        Me.mnuAssetCoding.Name = "mnuAssetCoding"
        Me.mnuAssetCoding.Size = New System.Drawing.Size(205, 22)
        Me.mnuAssetCoding.Text = "Assets Coding Definition"
        '
        'mnuSep5
        '
        Me.mnuSep5.Name = "mnuSep5"
        Me.mnuSep5.Size = New System.Drawing.Size(202, 6)
        '
        'mnuOrgLevels
        '
        Me.mnuOrgLevels.Name = "mnuOrgLevels"
        Me.mnuOrgLevels.Size = New System.Drawing.Size(205, 22)
        Me.mnuOrgLevels.Text = "Organization Levels"
        '
        'mnuOrgGroups
        '
        Me.mnuOrgGroups.Name = "mnuOrgGroups"
        Me.mnuOrgGroups.Size = New System.Drawing.Size(205, 22)
        Me.mnuOrgGroups.Text = "Organization Groups"
        '
        'mnuOrgHier
        '
        Me.mnuOrgHier.Name = "mnuOrgHier"
        Me.mnuOrgHier.Size = New System.Drawing.Size(205, 22)
        Me.mnuOrgHier.Text = "Organizational Hierarchy"
        '
        'mnuAssets
        '
        Me.mnuAssets.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSearch, Me.mnuDetailsMainten, Me.mnuWarrantyAlarm, Me.mnuSep7, Me.mnuLocCustTrans, Me.mnuInterCompTrans, Me.ToolStripSeparator2, Me.mnuAdmin})
        Me.mnuAssets.Name = "mnuAssets"
        Me.mnuAssets.Size = New System.Drawing.Size(52, 20)
        Me.mnuAssets.Text = "&Assets"
        '
        'mnuSearch
        '
        Me.mnuSearch.Name = "mnuSearch"
        Me.mnuSearch.Size = New System.Drawing.Size(221, 22)
        Me.mnuSearch.Text = "Search"
        '
        'mnuDetailsMainten
        '
        Me.mnuDetailsMainten.Name = "mnuDetailsMainten"
        Me.mnuDetailsMainten.Size = New System.Drawing.Size(221, 22)
        Me.mnuDetailsMainten.Text = "Details && Maintenance"
        '
        'mnuSep7
        '
        Me.mnuSep7.Name = "mnuSep7"
        Me.mnuSep7.Size = New System.Drawing.Size(218, 6)
        '
        'mnuLocCustTrans
        '
        Me.mnuLocCustTrans.Name = "mnuLocCustTrans"
        Me.mnuLocCustTrans.Size = New System.Drawing.Size(221, 22)
        Me.mnuLocCustTrans.Text = "Location \ Custody Transfer"
        '
        'mnuInterCompTrans
        '
        Me.mnuInterCompTrans.Name = "mnuInterCompTrans"
        Me.mnuInterCompTrans.Size = New System.Drawing.Size(221, 22)
        Me.mnuInterCompTrans.Text = "Inter Company Transfer"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(218, 6)
        '
        'mnuAdmin
        '
        Me.mnuAdmin.Name = "mnuAdmin"
        Me.mnuAdmin.Size = New System.Drawing.Size(221, 22)
        Me.mnuAdmin.Text = "Administration"
        '
        'mnuPurchOrder
        '
        Me.mnuPurchOrder.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPOPrepar, Me.mnuPOApprovals, Me.ToolStripSeparator7, Me.mnuAssetsInTransit})
        Me.mnuPurchOrder.Name = "mnuPurchOrder"
        Me.mnuPurchOrder.Size = New System.Drawing.Size(100, 20)
        Me.mnuPurchOrder.Text = "&Purchase Order"
        '
        'mnuPOPrepar
        '
        Me.mnuPOPrepar.Name = "mnuPOPrepar"
        Me.mnuPOPrepar.Size = New System.Drawing.Size(159, 22)
        Me.mnuPOPrepar.Text = "PO Preparation"
        '
        'mnuPOApprovals
        '
        Me.mnuPOApprovals.Name = "mnuPOApprovals"
        Me.mnuPOApprovals.Size = New System.Drawing.Size(159, 22)
        Me.mnuPOApprovals.Text = "PO Approvals"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(156, 6)
        '
        'mnuAssetsInTransit
        '
        Me.mnuAssetsInTransit.Name = "mnuAssetsInTransit"
        Me.mnuAssetsInTransit.Size = New System.Drawing.Size(159, 22)
        Me.mnuAssetsInTransit.Text = "Assets In Transit"
        '
        'mnuTools
        '
        Me.mnuTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDeviceConfig, Me.ToolStripSeparator4, Me.mnuSystemConfig, Me.mnuCompanyInfo, Me.mnuDepEngine, Me.ToolStripSeparator5, Me.mnuBarcodeStruct, Me.ToolStripSeparator10, Me.mnuOfflineMachines, Me.mnuBackendInventory})
        Me.mnuTools.Name = "mnuTools"
        Me.mnuTools.Size = New System.Drawing.Size(48, 20)
        Me.mnuTools.Text = "&Tools"
        '
        'mnuDeviceConfig
        '
        Me.mnuDeviceConfig.Name = "mnuDeviceConfig"
        Me.mnuDeviceConfig.Size = New System.Drawing.Size(191, 22)
        Me.mnuDeviceConfig.Text = "Devices Configuration"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(188, 6)
        '
        'mnuSystemConfig
        '
        Me.mnuSystemConfig.Name = "mnuSystemConfig"
        Me.mnuSystemConfig.Size = New System.Drawing.Size(191, 22)
        Me.mnuSystemConfig.Text = "System Configuration"
        '
        'mnuCompanyInfo
        '
        Me.mnuCompanyInfo.Name = "mnuCompanyInfo"
        Me.mnuCompanyInfo.Size = New System.Drawing.Size(191, 22)
        Me.mnuCompanyInfo.Text = "Company Info"
        '
        'mnuDepEngine
        '
        Me.mnuDepEngine.Name = "mnuDepEngine"
        Me.mnuDepEngine.Size = New System.Drawing.Size(191, 22)
        Me.mnuDepEngine.Text = "Depreciation Engine"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(188, 6)
        '
        'mnuBarcodeStruct
        '
        Me.mnuBarcodeStruct.Name = "mnuBarcodeStruct"
        Me.mnuBarcodeStruct.Size = New System.Drawing.Size(191, 22)
        Me.mnuBarcodeStruct.Text = "BarCode Structure"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(188, 6)
        '
        'mnuOfflineMachines
        '
        Me.mnuOfflineMachines.Name = "mnuOfflineMachines"
        Me.mnuOfflineMachines.Size = New System.Drawing.Size(191, 22)
        Me.mnuOfflineMachines.Text = "Offline Machines"
        '
        'mnuBackendInventory
        '
        Me.mnuBackendInventory.Name = "mnuBackendInventory"
        Me.mnuBackendInventory.Size = New System.Drawing.Size(191, 22)
        Me.mnuBackendInventory.Text = "Backend Inventory"
        '
        'mnuReports
        '
        Me.mnuReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReportCenter, Me.mnuRptDesigner, Me.mnuCreateReport})
        Me.mnuReports.Name = "mnuReports"
        Me.mnuReports.ShowShortcutKeys = False
        Me.mnuReports.Size = New System.Drawing.Size(59, 20)
        Me.mnuReports.Text = "&Reports"
        '
        'mnuReportCenter
        '
        Me.mnuReportCenter.Name = "mnuReportCenter"
        Me.mnuReportCenter.Size = New System.Drawing.Size(158, 22)
        Me.mnuReportCenter.Text = "Report Center"
        '
        'mnuRptDesigner
        '
        Me.mnuRptDesigner.Name = "mnuRptDesigner"
        Me.mnuRptDesigner.Size = New System.Drawing.Size(158, 22)
        Me.mnuRptDesigner.Text = "Report Designer"
        '
        'mnuCreateReport
        '
        Me.mnuCreateReport.Name = "mnuCreateReport"
        Me.mnuCreateReport.Size = New System.Drawing.Size(158, 22)
        Me.mnuCreateReport.Text = "Create Report"
        '
        'mnuCommunication
        '
        Me.mnuCommunication.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSend, Me.mnuReceive, Me.mnuDataProcess})
        Me.mnuCommunication.Name = "mnuCommunication"
        Me.mnuCommunication.Size = New System.Drawing.Size(106, 20)
        Me.mnuCommunication.Text = "&Communication"
        '
        'mnuSend
        '
        Me.mnuSend.Name = "mnuSend"
        Me.mnuSend.Size = New System.Drawing.Size(161, 22)
        Me.mnuSend.Text = "Data Transfer"
        '
        'mnuReceive
        '
        Me.mnuReceive.Name = "mnuReceive"
        Me.mnuReceive.Size = New System.Drawing.Size(161, 22)
        Me.mnuReceive.Text = "Data Acquisition"
        '
        'mnuDataProcess
        '
        Me.mnuDataProcess.Name = "mnuDataProcess"
        Me.mnuDataProcess.Size = New System.Drawing.Size(161, 22)
        Me.mnuDataProcess.Text = "Data Processing"
        '
        'mnuHelp
        '
        Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuUserGuide, Me.mnuAbout})
        Me.mnuHelp.Name = "mnuHelp"
        Me.mnuHelp.Size = New System.Drawing.Size(44, 20)
        Me.mnuHelp.Text = "&Help"
        '
        'mnuUserGuide
        '
        Me.mnuUserGuide.Name = "mnuUserGuide"
        Me.mnuUserGuide.Size = New System.Drawing.Size(184, 22)
        Me.mnuUserGuide.Text = "ZulAssets User Guide"
        '
        'mnuAbout
        '
        Me.mnuAbout.Name = "mnuAbout"
        Me.mnuAbout.Size = New System.Drawing.Size(184, 22)
        Me.mnuAbout.Text = "About ZulAssets"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.White
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuSecurity, Me.mnuMasterData, Me.mnuCompanyProfile, Me.mnuAssets, Me.mnuPurchOrder, Me.mnuTools, Me.mnuCommunication, Me.mnuReports, Me.mnuWindow, Me.mnuHelp})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(792, 24)
        Me.MenuStrip1.TabIndex = 1
        '
        'mnuWindow
        '
        Me.mnuWindow.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCascad, Me.mnuTileHorizontal, Me.mnuTileVertical, Me.ToolStripSeparator9, Me.mnuCloseALL})
        Me.mnuWindow.Name = "mnuWindow"
        Me.mnuWindow.Size = New System.Drawing.Size(63, 20)
        Me.mnuWindow.Text = "&Window"
        '
        'mnuCascad
        '
        Me.mnuCascad.Name = "mnuCascad"
        Me.mnuCascad.Size = New System.Drawing.Size(154, 22)
        Me.mnuCascad.Text = "Cascade"
        '
        'mnuTileHorizontal
        '
        Me.mnuTileHorizontal.Name = "mnuTileHorizontal"
        Me.mnuTileHorizontal.Size = New System.Drawing.Size(154, 22)
        Me.mnuTileHorizontal.Text = "Tile Horizontal "
        '
        'mnuTileVertical
        '
        Me.mnuTileVertical.Name = "mnuTileVertical"
        Me.mnuTileVertical.Size = New System.Drawing.Size(154, 22)
        Me.mnuTileVertical.Text = "Tile Vertical "
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(151, 6)
        '
        'mnuCloseALL
        '
        Me.mnuCloseALL.Name = "mnuCloseALL"
        Me.mnuCloseALL.Size = New System.Drawing.Size(154, 22)
        Me.mnuCloseALL.Text = "&Close ALL"
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
        'mnuWarrantyAlarm
        '
        Me.mnuWarrantyAlarm.Name = "mnuWarrantyAlarm"
        Me.mnuWarrantyAlarm.Size = New System.Drawing.Size(221, 22)
        Me.mnuWarrantyAlarm.Text = "Warranty Alarm"
        '
        'frmMain
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(792, 573)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = True
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ZulAssets  - Assets Management Solution"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSecurity As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuApplicationUsers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuUserRoles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMasterData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDesgination As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAddressTemplates As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCustodians As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuBrands As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuInsurers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSuppliers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGLCode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUnits As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuDisposalMethods As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDepreciationMethods As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSep3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuAssetItems As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAssetBooks As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAssetCat As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuInvSch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCompanyProfile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCompanies As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSep4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuLocations As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDepPolicy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBarcodePolicy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAssetCoding As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSep5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuOrgLevels As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOrgGroups As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOrgHier As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAssets As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSearch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDetailsMainten As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSep7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuLocCustTrans As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuInterCompTrans As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuAdmin As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPurchOrder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPOPrepar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPOApprovals As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuAssetsInTransit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDeviceConfig As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuSystemConfig As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCompanyInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDepEngine As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuBarcodeStruct As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCommunication As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReceive As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSend As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUserGuide As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuWindow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCloseALL As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblINS As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblNUM As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblCAPS As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblServerName As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblDBName As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel4 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblMessages As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mnuCascad As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTileHorizontal As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTileVertical As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuOfflineMachines As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDataProcess As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuImportFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuExportFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuReportCenter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRptDesigner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCreateReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCostCenter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBackendInventory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents imgAssetStatus As System.Windows.Forms.ImageList
    Friend WithEvents mnuChangePassword As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuWarrantyAlarm As System.Windows.Forms.ToolStripMenuItem
End Class
