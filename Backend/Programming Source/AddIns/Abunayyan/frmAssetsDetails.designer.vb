<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAssetsDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAssetsDetails))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox12 = New System.Windows.Forms.GroupBox
        Me.Label59 = New System.Windows.Forms.Label
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.Label61 = New System.Windows.Forms.Label
        Me.Label62 = New System.Windows.Forms.Label
        Me.Label63 = New System.Windows.Forms.Label
        Me.Label64 = New System.Windows.Forms.Label
        Me.Label65 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnNewAsset = New System.Windows.Forms.Button
        Me.tabControl = New DevExpress.XtraTab.XtraTabControl
        Me.tabAssetInfo = New DevExpress.XtraTab.XtraTabPage
        Me.MainGroup = New System.Windows.Forms.Panel
        Me.pnlInventoryStatus = New DevExpress.XtraEditors.PanelControl
        Me.lblInvStatus = New System.Windows.Forms.Label
        Me.imgInvStatus = New System.Windows.Forms.PictureBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtSource = New DevExpress.XtraEditors.TextEdit
        Me.Label13 = New System.Windows.Forms.Label
        Me.pnlStatus = New DevExpress.XtraEditors.PanelControl
        Me.lblStatus = New System.Windows.Forms.Label
        Me.imgStatus = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtLastInventory = New DevExpress.XtraEditors.TextEdit
        Me.txtCreation = New DevExpress.XtraEditors.TextEdit
        Me.cmbComp = New ZulLOV.ZulLOV
        Me.txtAssetID = New ZulLOV.ZulLOV
        Me.Label7 = New System.Windows.Forms.Label
        Me.grpImage = New System.Windows.Forms.GroupBox
        Me.btnDelImg = New System.Windows.Forms.Button
        Me.PBLogo = New System.Windows.Forms.PictureBox
        Me.pnlNavigation = New DevExpress.XtraEditors.PanelControl
        Me.btnLast = New System.Windows.Forms.Button
        Me.btnFirst = New System.Windows.Forms.Button
        Me.btnNext = New System.Windows.Forms.Button
        Me.btnPre = New System.Windows.Forms.Button
        Me.btnNewAsset1 = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtCreatedBY = New DevExpress.XtraEditors.TextEdit
        Me.txtSubNumber = New DevExpress.XtraEditors.TextEdit
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtLabelPrintedCnt = New DevExpress.XtraEditors.TextEdit
        Me.Label71 = New System.Windows.Forms.Label
        Me.txtAssetNumber = New DevExpress.XtraEditors.TextEdit
        Me.Label2 = New System.Windows.Forms.Label
        Me.trvAssetClass = New ZulTree.ZulTree
        Me.txtSerialNo = New DevExpress.XtraEditors.TextEdit
        Me.Label15 = New System.Windows.Forms.Label
        Me.Sear = New System.Windows.Forms.Label
        Me.cmbCostCenter = New ZulLOV.ZulLOV
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtAstDesc2 = New DevExpress.XtraEditors.TextEdit
        Me.Label68 = New System.Windows.Forms.Label
        Me.txtAstDesc = New DevExpress.XtraEditors.TextEdit
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.cmbEmployee = New ZulLOV.ZulLOV
        Me.Label45 = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.grpLocation = New System.Windows.Forms.GroupBox
        Me.txtPlant = New DevExpress.XtraEditors.TextEdit
        Me.Label18 = New System.Windows.Forms.Label
        Me.trvLocation = New ZulTree.ZulTree
        Me.txtLocation = New DevExpress.XtraEditors.TextEdit
        Me.Label8 = New System.Windows.Forms.Label
        Me.TabPage3 = New DevExpress.XtraTab.XtraTabPage
        Me.grdAstHist = New DevExpress.XtraGrid.GridControl
        Me.grdAstHistView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.TabPage4 = New DevExpress.XtraTab.XtraTabPage
        Me.grdCustHist = New DevExpress.XtraGrid.GridControl
        Me.grdCustHistView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.tabAssetLog = New DevExpress.XtraTab.XtraTabPage
        Me.grdLog = New DevExpress.XtraGrid.GridControl
        Me.grdLogView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.chkAssetWithValue = New System.Windows.Forms.CheckBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.btn = New System.Windows.Forms.Button
        Me.dlgFile = New System.Windows.Forms.OpenFileDialog
        Me.lblEvaluation = New System.Windows.Forms.Label
        Me.txtAstNum = New DevExpress.XtraEditors.TextEdit
        Me.GroupBox12.SuspendLayout()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControl.SuspendLayout()
        Me.tabAssetInfo.SuspendLayout()
        Me.MainGroup.SuspendLayout()
        CType(Me.pnlInventoryStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlInventoryStatus.SuspendLayout()
        CType(Me.imgInvStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSource.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlStatus.SuspendLayout()
        CType(Me.imgStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLastInventory.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCreation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpImage.SuspendLayout()
        CType(Me.PBLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlNavigation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlNavigation.SuspendLayout()
        CType(Me.txtCreatedBY.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLabelPrintedCnt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAssetNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSerialNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAstDesc2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAstDesc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLocation.SuspendLayout()
        CType(Me.txtPlant.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.grdAstHist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAstHistView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.grdCustHist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCustHistView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabAssetLog.SuspendLayout()
        CType(Me.grdLog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdLogView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAstNum.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox12
        '
        Me.GroupBox12.Controls.Add(Me.Label59)
        Me.GroupBox12.Controls.Add(Me.ComboBox1)
        Me.GroupBox12.Controls.Add(Me.Label61)
        Me.GroupBox12.Controls.Add(Me.Label62)
        Me.GroupBox12.Controls.Add(Me.Label63)
        Me.GroupBox12.Controls.Add(Me.Label64)
        Me.GroupBox12.Controls.Add(Me.Label65)
        Me.GroupBox12.Location = New System.Drawing.Point(22, 19)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(343, 192)
        Me.GroupBox12.TabIndex = 0
        Me.GroupBox12.TabStop = False
        Me.GroupBox12.Text = "ERP Information"
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(21, 161)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(47, 13)
        Me.Label59.TabIndex = 30
        Me.Label59.Text = "GL Code"
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.Location = New System.Drawing.Point(124, 155)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(126, 21)
        Me.ComboBox1.TabIndex = 5
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(21, 81)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(21, 13)
        Me.Label61.TabIndex = 3
        Me.Label61.Text = "PO"
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(21, 107)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(50, 13)
        Me.Label62.TabIndex = 2
        Me.Label62.Text = "CAPEX #"
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(21, 55)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(42, 13)
        Me.Label63.TabIndex = 1
        Me.Label63.Text = "Plate #"
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(21, 133)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(39, 13)
        Me.Label64.TabIndex = 0
        Me.Label64.Text = "GRN #"
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(21, 28)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(85, 13)
        Me.Label65.TabIndex = 0
        Me.Label65.Text = "Reference Code"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Image = Global.AbunayyanPlugin.My.Resources.Resources.Save16x16
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(488, 510)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(87, 31)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "&Save"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Image = Global.AbunayyanPlugin.My.Resources.Resources.Close16x16
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(606, 510)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(91, 31)
        Me.btnExit.TabIndex = 6
        Me.btnExit.Text = "&Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Image = Global.AbunayyanPlugin.My.Resources.Resources.Delete16x16
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(375, 510)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(82, 31)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "&Delete"
        '
        'btnNewAsset
        '
        Me.btnNewAsset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNewAsset.Image = Global.AbunayyanPlugin.My.Resources.Resources.New16x16
        Me.btnNewAsset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNewAsset.Location = New System.Drawing.Point(12, 510)
        Me.btnNewAsset.Name = "btnNewAsset"
        Me.btnNewAsset.Size = New System.Drawing.Size(97, 31)
        Me.btnNewAsset.TabIndex = 2
        Me.btnNewAsset.Text = "&New Assets"
        Me.btnNewAsset.UseVisualStyleBackColor = True
        '
        'tabControl
        '
        Me.tabControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabControl.Location = New System.Drawing.Point(3, 68)
        Me.tabControl.LookAndFeel.UseDefaultLookAndFeel = False
        Me.tabControl.LookAndFeel.UseWindowsXPTheme = True
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedTabPage = Me.tabAssetInfo
        Me.tabControl.Size = New System.Drawing.Size(700, 436)
        Me.tabControl.TabIndex = 1
        Me.tabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabAssetInfo, Me.TabPage3, Me.TabPage4, Me.tabAssetLog})
        '
        'tabAssetInfo
        '
        Me.tabAssetInfo.Appearance.PageClient.BackColor = System.Drawing.Color.White
        Me.tabAssetInfo.Appearance.PageClient.Options.UseBackColor = True
        Me.tabAssetInfo.Controls.Add(Me.MainGroup)
        Me.tabAssetInfo.Name = "tabAssetInfo"
        Me.tabAssetInfo.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAssetInfo.Size = New System.Drawing.Size(691, 405)
        Me.tabAssetInfo.Text = "Asset Information"
        '
        'MainGroup
        '
        Me.MainGroup.AutoScroll = True
        Me.MainGroup.BackColor = System.Drawing.Color.White
        Me.MainGroup.Controls.Add(Me.pnlInventoryStatus)
        Me.MainGroup.Controls.Add(Me.txtSource)
        Me.MainGroup.Controls.Add(Me.Label13)
        Me.MainGroup.Controls.Add(Me.pnlStatus)
        Me.MainGroup.Controls.Add(Me.Label11)
        Me.MainGroup.Controls.Add(Me.txtLastInventory)
        Me.MainGroup.Controls.Add(Me.txtCreation)
        Me.MainGroup.Controls.Add(Me.cmbComp)
        Me.MainGroup.Controls.Add(Me.txtAssetID)
        Me.MainGroup.Controls.Add(Me.Label7)
        Me.MainGroup.Controls.Add(Me.grpImage)
        Me.MainGroup.Controls.Add(Me.pnlNavigation)
        Me.MainGroup.Controls.Add(Me.btnNewAsset1)
        Me.MainGroup.Controls.Add(Me.Label6)
        Me.MainGroup.Controls.Add(Me.txtCreatedBY)
        Me.MainGroup.Controls.Add(Me.txtSubNumber)
        Me.MainGroup.Controls.Add(Me.Label4)
        Me.MainGroup.Controls.Add(Me.txtLabelPrintedCnt)
        Me.MainGroup.Controls.Add(Me.Label71)
        Me.MainGroup.Controls.Add(Me.txtAssetNumber)
        Me.MainGroup.Controls.Add(Me.Label2)
        Me.MainGroup.Controls.Add(Me.trvAssetClass)
        Me.MainGroup.Controls.Add(Me.txtSerialNo)
        Me.MainGroup.Controls.Add(Me.Label15)
        Me.MainGroup.Controls.Add(Me.Sear)
        Me.MainGroup.Controls.Add(Me.cmbCostCenter)
        Me.MainGroup.Controls.Add(Me.Label5)
        Me.MainGroup.Controls.Add(Me.Label21)
        Me.MainGroup.Controls.Add(Me.txtAstDesc2)
        Me.MainGroup.Controls.Add(Me.Label68)
        Me.MainGroup.Controls.Add(Me.txtAstDesc)
        Me.MainGroup.Controls.Add(Me.Label9)
        Me.MainGroup.Controls.Add(Me.Label12)
        Me.MainGroup.Controls.Add(Me.cmbEmployee)
        Me.MainGroup.Controls.Add(Me.Label45)
        Me.MainGroup.Controls.Add(Me.Label44)
        Me.MainGroup.Controls.Add(Me.grpLocation)
        Me.MainGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainGroup.Location = New System.Drawing.Point(3, 3)
        Me.MainGroup.Name = "MainGroup"
        Me.MainGroup.Size = New System.Drawing.Size(685, 399)
        Me.MainGroup.TabIndex = 1
        Me.MainGroup.Text = "Details"
        '
        'pnlInventoryStatus
        '
        Me.pnlInventoryStatus.Controls.Add(Me.lblInvStatus)
        Me.pnlInventoryStatus.Controls.Add(Me.imgInvStatus)
        Me.pnlInventoryStatus.Controls.Add(Me.Label16)
        Me.pnlInventoryStatus.Location = New System.Drawing.Point(452, 39)
        Me.pnlInventoryStatus.Name = "pnlInventoryStatus"
        Me.pnlInventoryStatus.Size = New System.Drawing.Size(208, 25)
        Me.pnlInventoryStatus.TabIndex = 91
        '
        'lblInvStatus
        '
        Me.lblInvStatus.AutoSize = True
        Me.lblInvStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvStatus.Location = New System.Drawing.Point(97, 7)
        Me.lblInvStatus.Name = "lblInvStatus"
        Me.lblInvStatus.Size = New System.Drawing.Size(44, 13)
        Me.lblInvStatus.TabIndex = 90
        Me.lblInvStatus.Text = "Status"
        '
        'imgInvStatus
        '
        Me.imgInvStatus.Location = New System.Drawing.Point(73, 5)
        Me.imgInvStatus.Name = "imgInvStatus"
        Me.imgInvStatus.Size = New System.Drawing.Size(16, 16)
        Me.imgInvStatus.TabIndex = 89
        Me.imgInvStatus.TabStop = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(4, 8)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(69, 13)
        Me.Label16.TabIndex = 87
        Me.Label16.Text = "Inv Status:"
        '
        'txtSource
        '
        Me.txtSource.Location = New System.Drawing.Point(450, 314)
        Me.txtSource.Name = "txtSource"
        Me.txtSource.Properties.ReadOnly = True
        Me.txtSource.Size = New System.Drawing.Size(210, 19)
        Me.txtSource.TabIndex = 20
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(341, 317)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(40, 13)
        Me.Label13.TabIndex = 90
        Me.Label13.Text = "Source"
        '
        'pnlStatus
        '
        Me.pnlStatus.Controls.Add(Me.lblStatus)
        Me.pnlStatus.Controls.Add(Me.imgStatus)
        Me.pnlStatus.Controls.Add(Me.Label1)
        Me.pnlStatus.Location = New System.Drawing.Point(452, 66)
        Me.pnlStatus.Name = "pnlStatus"
        Me.pnlStatus.Size = New System.Drawing.Size(208, 25)
        Me.pnlStatus.TabIndex = 3
        Me.pnlStatus.Visible = False
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(97, 8)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(44, 13)
        Me.lblStatus.TabIndex = 90
        Me.lblStatus.Text = "Status"
        '
        'imgStatus
        '
        Me.imgStatus.Location = New System.Drawing.Point(73, 5)
        Me.imgStatus.Name = "imgStatus"
        Me.imgStatus.Size = New System.Drawing.Size(16, 16)
        Me.imgStatus.TabIndex = 89
        Me.imgStatus.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 87
        Me.Label1.Text = "Status:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label11.Location = New System.Drawing.Point(5, 253)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(56, 13)
        Me.Label11.TabIndex = 88
        Me.Label11.Text = "Location"
        '
        'txtLastInventory
        '
        Me.txtLastInventory.Location = New System.Drawing.Point(119, 345)
        Me.txtLastInventory.Name = "txtLastInventory"
        Me.txtLastInventory.Properties.MaxLength = 25
        Me.txtLastInventory.Properties.ReadOnly = True
        Me.txtLastInventory.Size = New System.Drawing.Size(216, 19)
        Me.txtLastInventory.TabIndex = 26
        '
        'txtCreation
        '
        Me.txtCreation.Location = New System.Drawing.Point(119, 372)
        Me.txtCreation.Name = "txtCreation"
        Me.txtCreation.Properties.MaxLength = 25
        Me.txtCreation.Properties.ReadOnly = True
        Me.txtCreation.Size = New System.Drawing.Size(216, 19)
        Me.txtCreation.TabIndex = 23
        '
        'cmbComp
        '
        Me.cmbComp.BackColor = System.Drawing.Color.White
        Me.cmbComp.DataSource = Nothing
        Me.cmbComp.DisplayMember = ""
        Me.cmbComp.Location = New System.Drawing.Point(120, 40)
        Me.cmbComp.Name = "cmbComp"
        Me.cmbComp.SelectedIndex = -1
        Me.cmbComp.SelectedText = ""
        Me.cmbComp.SelectedValue = ""
        Me.cmbComp.Size = New System.Drawing.Size(215, 24)
        Me.cmbComp.TabIndex = 2
        Me.cmbComp.TextReadOnly = False
        Me.cmbComp.ValueMember = ""
        '
        'txtAssetID
        '
        Me.txtAssetID.BackColor = System.Drawing.Color.White
        Me.txtAssetID.DataSource = Nothing
        Me.txtAssetID.DisplayMember = ""
        Me.txtAssetID.Location = New System.Drawing.Point(120, 15)
        Me.txtAssetID.Name = "txtAssetID"
        Me.txtAssetID.SelectedIndex = -1
        Me.txtAssetID.SelectedText = ""
        Me.txtAssetID.SelectedValue = ""
        Me.txtAssetID.Size = New System.Drawing.Size(215, 24)
        Me.txtAssetID.TabIndex = 0
        Me.txtAssetID.TextReadOnly = False
        Me.txtAssetID.ValueMember = ""
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(5, 348)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 13)
        Me.Label7.TabIndex = 87
        Me.Label7.Text = "Last Inventory Date"
        '
        'grpImage
        '
        Me.grpImage.Controls.Add(Me.btnDelImg)
        Me.grpImage.Controls.Add(Me.PBLogo)
        Me.grpImage.Location = New System.Drawing.Point(450, 141)
        Me.grpImage.Name = "grpImage"
        Me.grpImage.Size = New System.Drawing.Size(210, 165)
        Me.grpImage.TabIndex = 27
        Me.grpImage.TabStop = False
        Me.grpImage.Text = "Asset Image"
        '
        'btnDelImg
        '
        Me.btnDelImg.Image = Global.AbunayyanPlugin.My.Resources.Resources.Delete16x16
        Me.btnDelImg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelImg.Location = New System.Drawing.Point(6, 137)
        Me.btnDelImg.Name = "btnDelImg"
        Me.btnDelImg.Size = New System.Drawing.Size(117, 25)
        Me.btnDelImg.TabIndex = 1
        Me.btnDelImg.Text = "Delete Image"
        Me.btnDelImg.UseVisualStyleBackColor = True
        Me.btnDelImg.Visible = False
        '
        'PBLogo
        '
        Me.PBLogo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PBLogo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PBLogo.Location = New System.Drawing.Point(6, 19)
        Me.PBLogo.Name = "PBLogo"
        Me.PBLogo.Size = New System.Drawing.Size(199, 116)
        Me.PBLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBLogo.TabIndex = 43
        Me.PBLogo.TabStop = False
        '
        'pnlNavigation
        '
        Me.pnlNavigation.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.pnlNavigation.Appearance.Options.UseBackColor = True
        Me.pnlNavigation.Controls.Add(Me.btnLast)
        Me.pnlNavigation.Controls.Add(Me.btnFirst)
        Me.pnlNavigation.Controls.Add(Me.btnNext)
        Me.pnlNavigation.Controls.Add(Me.btnPre)
        Me.pnlNavigation.Location = New System.Drawing.Point(452, 9)
        Me.pnlNavigation.Name = "pnlNavigation"
        Me.pnlNavigation.Size = New System.Drawing.Size(208, 27)
        Me.pnlNavigation.TabIndex = 1
        '
        'btnLast
        '
        Me.btnLast.Image = Global.AbunayyanPlugin.My.Resources.Resources.BtnLast
        Me.btnLast.Location = New System.Drawing.Point(144, 1)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(35, 25)
        Me.btnLast.TabIndex = 5
        Me.btnLast.UseVisualStyleBackColor = True
        '
        'btnFirst
        '
        Me.btnFirst.Image = Global.AbunayyanPlugin.My.Resources.Resources.BtnFirst
        Me.btnFirst.Location = New System.Drawing.Point(30, 1)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(35, 25)
        Me.btnFirst.TabIndex = 2
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Image = Global.AbunayyanPlugin.My.Resources.Resources.BtnNext
        Me.btnNext.Location = New System.Drawing.Point(106, 1)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(35, 25)
        Me.btnNext.TabIndex = 4
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnPre
        '
        Me.btnPre.Image = Global.AbunayyanPlugin.My.Resources.Resources.BtnPrev
        Me.btnPre.Location = New System.Drawing.Point(68, 1)
        Me.btnPre.Name = "btnPre"
        Me.btnPre.Size = New System.Drawing.Size(35, 25)
        Me.btnPre.TabIndex = 3
        Me.btnPre.UseVisualStyleBackColor = True
        '
        'btnNewAsset1
        '
        Me.btnNewAsset1.BackColor = System.Drawing.SystemColors.Control
        Me.btnNewAsset1.Image = Global.AbunayyanPlugin.My.Resources.Resources.New16x16
        Me.btnNewAsset1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNewAsset1.Location = New System.Drawing.Point(336, 15)
        Me.btnNewAsset1.Name = "btnNewAsset1"
        Me.btnNewAsset1.Size = New System.Drawing.Size(71, 24)
        Me.btnNewAsset1.TabIndex = 1
        Me.btnNewAsset1.Text = "&New"
        Me.btnNewAsset1.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(237, 70)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 13)
        Me.Label6.TabIndex = 83
        Me.Label6.Text = "Sub No"
        '
        'txtCreatedBY
        '
        Me.txtCreatedBY.Location = New System.Drawing.Point(450, 372)
        Me.txtCreatedBY.Name = "txtCreatedBY"
        Me.txtCreatedBY.Properties.MaxLength = 25
        Me.txtCreatedBY.Properties.ReadOnly = True
        Me.txtCreatedBY.Size = New System.Drawing.Size(210, 19)
        Me.txtCreatedBY.TabIndex = 24
        '
        'txtSubNumber
        '
        Me.txtSubNumber.EditValue = ""
        Me.txtSubNumber.Location = New System.Drawing.Point(283, 67)
        Me.txtSubNumber.Name = "txtSubNumber"
        Me.txtSubNumber.Properties.MaxLength = 5
        Me.txtSubNumber.Properties.ReadOnly = True
        Me.txtSubNumber.Size = New System.Drawing.Size(52, 19)
        Me.txtSubNumber.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(5, 70)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 13)
        Me.Label4.TabIndex = 81
        Me.Label4.Text = "Asset Number"
        '
        'txtLabelPrintedCnt
        '
        Me.txtLabelPrintedCnt.Location = New System.Drawing.Point(450, 345)
        Me.txtLabelPrintedCnt.Name = "txtLabelPrintedCnt"
        Me.txtLabelPrintedCnt.Properties.ReadOnly = True
        Me.txtLabelPrintedCnt.Size = New System.Drawing.Size(210, 19)
        Me.txtLabelPrintedCnt.TabIndex = 22
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(341, 375)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(61, 13)
        Me.Label71.TabIndex = 10
        Me.Label71.Text = "Created BY"
        '
        'txtAssetNumber
        '
        Me.txtAssetNumber.EditValue = ""
        Me.txtAssetNumber.Location = New System.Drawing.Point(120, 67)
        Me.txtAssetNumber.Name = "txtAssetNumber"
        Me.txtAssetNumber.Properties.MaxLength = 10
        Me.txtAssetNumber.Properties.ReadOnly = True
        Me.txtAssetNumber.Size = New System.Drawing.Size(111, 19)
        Me.txtAssetNumber.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(341, 348)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 61
        Me.Label2.Text = "Labels Printed"
        '
        'trvAssetClass
        '
        Me.trvAssetClass.BackColor = System.Drawing.Color.White
        Me.trvAssetClass.DataSource = Nothing
        Me.trvAssetClass.DisplayMember = ""
        Me.trvAssetClass.Location = New System.Drawing.Point(120, 191)
        Me.trvAssetClass.Name = "trvAssetClass"
        Me.trvAssetClass.SelectedText = ""
        Me.trvAssetClass.SelectedValue = ""
        Me.trvAssetClass.Size = New System.Drawing.Size(215, 24)
        Me.trvAssetClass.TabIndex = 11
        Me.trvAssetClass.TextReadOnly = False
        Me.trvAssetClass.ValueMember = ""
        '
        'txtSerialNo
        '
        Me.txtSerialNo.Location = New System.Drawing.Point(120, 142)
        Me.txtSerialNo.Name = "txtSerialNo"
        Me.txtSerialNo.Properties.MaxLength = 50
        Me.txtSerialNo.Size = New System.Drawing.Size(216, 19)
        Me.txtSerialNo.TabIndex = 8
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label15.Location = New System.Drawing.Point(5, 20)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(54, 13)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "Barcode"
        '
        'Sear
        '
        Me.Sear.AutoSize = True
        Me.Sear.Location = New System.Drawing.Point(5, 145)
        Me.Sear.Name = "Sear"
        Me.Sear.Size = New System.Drawing.Size(73, 13)
        Me.Sear.TabIndex = 71
        Me.Sear.Text = "Serial Number"
        '
        'cmbCostCenter
        '
        Me.cmbCostCenter.BackColor = System.Drawing.Color.White
        Me.cmbCostCenter.DataSource = Nothing
        Me.cmbCostCenter.DisplayMember = ""
        Me.cmbCostCenter.Location = New System.Drawing.Point(120, 217)
        Me.cmbCostCenter.Name = "cmbCostCenter"
        Me.cmbCostCenter.SelectedIndex = -1
        Me.cmbCostCenter.SelectedText = ""
        Me.cmbCostCenter.SelectedValue = ""
        Me.cmbCostCenter.Size = New System.Drawing.Size(215, 24)
        Me.cmbCostCenter.TabIndex = 12
        Me.cmbCostCenter.TextReadOnly = False
        Me.cmbCostCenter.ValueMember = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(5, 196)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 13)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Asset Class"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(5, 222)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(65, 13)
        Me.Label21.TabIndex = 79
        Me.Label21.Text = "Cost Center"
        '
        'txtAstDesc2
        '
        Me.txtAstDesc2.Location = New System.Drawing.Point(120, 116)
        Me.txtAstDesc2.Name = "txtAstDesc2"
        Me.txtAstDesc2.Properties.MaxLength = 200
        Me.txtAstDesc2.Size = New System.Drawing.Size(540, 19)
        Me.txtAstDesc2.TabIndex = 7
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(5, 119)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(69, 13)
        Me.Label68.TabIndex = 76
        Me.Label68.Text = "Description 2"
        '
        'txtAstDesc
        '
        Me.txtAstDesc.Location = New System.Drawing.Point(120, 92)
        Me.txtAstDesc.Name = "txtAstDesc"
        Me.txtAstDesc.Properties.MaxLength = 200
        Me.txtAstDesc.Size = New System.Drawing.Size(540, 19)
        Me.txtAstDesc.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(5, 375)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(87, 13)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Date of Creation"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label12.Location = New System.Drawing.Point(5, 169)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(61, 13)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Employee"
        '
        'cmbEmployee
        '
        Me.cmbEmployee.BackColor = System.Drawing.Color.White
        Me.cmbEmployee.DataSource = Nothing
        Me.cmbEmployee.DisplayMember = ""
        Me.cmbEmployee.Location = New System.Drawing.Point(120, 166)
        Me.cmbEmployee.Name = "cmbEmployee"
        Me.cmbEmployee.SelectedIndex = -1
        Me.cmbEmployee.SelectedText = ""
        Me.cmbEmployee.SelectedValue = ""
        Me.cmbEmployee.Size = New System.Drawing.Size(215, 24)
        Me.cmbEmployee.TabIndex = 10
        Me.cmbEmployee.TextReadOnly = False
        Me.cmbEmployee.ValueMember = ""
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(5, 45)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(95, 13)
        Me.Label45.TabIndex = 57
        Me.Label45.Text = "Company Name"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(5, 95)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(69, 13)
        Me.Label44.TabIndex = 55
        Me.Label44.Text = "Description 1"
        '
        'grpLocation
        '
        Me.grpLocation.Controls.Add(Me.txtPlant)
        Me.grpLocation.Controls.Add(Me.Label18)
        Me.grpLocation.Controls.Add(Me.trvLocation)
        Me.grpLocation.Controls.Add(Me.txtLocation)
        Me.grpLocation.Controls.Add(Me.Label8)
        Me.grpLocation.Location = New System.Drawing.Point(119, 241)
        Me.grpLocation.Name = "grpLocation"
        Me.grpLocation.Size = New System.Drawing.Size(216, 98)
        Me.grpLocation.TabIndex = 9
        Me.grpLocation.TabStop = False
        '
        'txtPlant
        '
        Me.txtPlant.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPlant.Location = New System.Drawing.Point(80, 36)
        Me.txtPlant.Name = "txtPlant"
        Me.txtPlant.Size = New System.Drawing.Size(129, 19)
        Me.txtPlant.TabIndex = 98
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(13, 39)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(36, 13)
        Me.Label18.TabIndex = 99
        Me.Label18.Text = "Plant"
        '
        'trvLocation
        '
        Me.trvLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.trvLocation.BackColor = System.Drawing.Color.White
        Me.trvLocation.DataSource = Nothing
        Me.trvLocation.DisplayMember = ""
        Me.trvLocation.Location = New System.Drawing.Point(6, 10)
        Me.trvLocation.Name = "trvLocation"
        Me.trvLocation.SelectedText = ""
        Me.trvLocation.SelectedValue = ""
        Me.trvLocation.Size = New System.Drawing.Size(203, 24)
        Me.trvLocation.TabIndex = 7
        Me.trvLocation.TextReadOnly = False
        Me.trvLocation.ValueMember = ""
        '
        'txtLocation
        '
        Me.txtLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLocation.Location = New System.Drawing.Point(80, 62)
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(129, 19)
        Me.txtLocation.TabIndex = 88
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(13, 65)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 13)
        Me.Label8.TabIndex = 90
        Me.Label8.Text = "Location"
        '
        'TabPage3
        '
        Me.TabPage3.Appearance.PageClient.BackColor = System.Drawing.Color.White
        Me.TabPage3.Appearance.PageClient.Options.UseBackColor = True
        Me.TabPage3.Controls.Add(Me.grdAstHist)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(691, 454)
        Me.TabPage3.Text = "Tracking History"
        '
        'grdAstHist
        '
        Me.grdAstHist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdAstHist.Location = New System.Drawing.Point(0, 0)
        Me.grdAstHist.MainView = Me.grdAstHistView
        Me.grdAstHist.Name = "grdAstHist"
        Me.grdAstHist.Size = New System.Drawing.Size(691, 454)
        Me.grdAstHist.TabIndex = 10
        Me.grdAstHist.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdAstHistView})
        '
        'grdAstHistView
        '
        Me.grdAstHistView.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAstHistView.Appearance.FooterPanel.Options.UseFont = True
        Me.grdAstHistView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAstHistView.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdAstHistView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grdAstHistView.GridControl = Me.grdAstHist
        Me.grdAstHistView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.grdAstHistView.Name = "grdAstHistView"
        Me.grdAstHistView.OptionsBehavior.Editable = False
        Me.grdAstHistView.OptionsCustomization.AllowGroup = False
        Me.grdAstHistView.OptionsNavigation.UseTabKey = False
        Me.grdAstHistView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdAstHistView.OptionsView.ShowAutoFilterRow = True
        Me.grdAstHistView.OptionsView.ShowGroupPanel = False
        Me.grdAstHistView.OptionsView.ShowIndicator = False
        Me.grdAstHistView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
        Me.grdAstHistView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'TabPage4
        '
        Me.TabPage4.Appearance.PageClient.BackColor = System.Drawing.Color.White
        Me.TabPage4.Appearance.PageClient.Options.UseBackColor = True
        Me.TabPage4.Controls.Add(Me.grdCustHist)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(691, 454)
        Me.TabPage4.Text = "Employee  History"
        '
        'grdCustHist
        '
        Me.grdCustHist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdCustHist.Location = New System.Drawing.Point(0, 0)
        Me.grdCustHist.MainView = Me.grdCustHistView
        Me.grdCustHist.Name = "grdCustHist"
        Me.grdCustHist.Size = New System.Drawing.Size(691, 454)
        Me.grdCustHist.TabIndex = 11
        Me.grdCustHist.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdCustHistView})
        '
        'grdCustHistView
        '
        Me.grdCustHistView.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdCustHistView.Appearance.FooterPanel.Options.UseFont = True
        Me.grdCustHistView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdCustHistView.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdCustHistView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grdCustHistView.GridControl = Me.grdCustHist
        Me.grdCustHistView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.grdCustHistView.Name = "grdCustHistView"
        Me.grdCustHistView.OptionsBehavior.Editable = False
        Me.grdCustHistView.OptionsCustomization.AllowGroup = False
        Me.grdCustHistView.OptionsNavigation.UseTabKey = False
        Me.grdCustHistView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdCustHistView.OptionsView.ShowAutoFilterRow = True
        Me.grdCustHistView.OptionsView.ShowGroupPanel = False
        Me.grdCustHistView.OptionsView.ShowIndicator = False
        Me.grdCustHistView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
        Me.grdCustHistView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'tabAssetLog
        '
        Me.tabAssetLog.Controls.Add(Me.grdLog)
        Me.tabAssetLog.Name = "tabAssetLog"
        Me.tabAssetLog.Size = New System.Drawing.Size(691, 454)
        Me.tabAssetLog.Text = "Asset Log"
        '
        'grdLog
        '
        Me.grdLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdLog.Location = New System.Drawing.Point(0, 0)
        Me.grdLog.MainView = Me.grdLogView
        Me.grdLog.Name = "grdLog"
        Me.grdLog.Size = New System.Drawing.Size(691, 454)
        Me.grdLog.TabIndex = 12
        Me.grdLog.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdLogView})
        '
        'grdLogView
        '
        Me.grdLogView.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdLogView.Appearance.FooterPanel.Options.UseFont = True
        Me.grdLogView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdLogView.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdLogView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grdLogView.GridControl = Me.grdLog
        Me.grdLogView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.grdLogView.Name = "grdLogView"
        Me.grdLogView.OptionsBehavior.Editable = False
        Me.grdLogView.OptionsCustomization.AllowGroup = False
        Me.grdLogView.OptionsNavigation.UseTabKey = False
        Me.grdLogView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdLogView.OptionsView.ColumnAutoWidth = False
        Me.grdLogView.OptionsView.ShowAutoFilterRow = True
        Me.grdLogView.OptionsView.ShowGroupPanel = False
        Me.grdLogView.OptionsView.ShowIndicator = False
        Me.grdLogView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
        Me.grdLogView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'chkAssetWithValue
        '
        Me.chkAssetWithValue.AutoSize = True
        Me.chkAssetWithValue.Location = New System.Drawing.Point(468, 24)
        Me.chkAssetWithValue.Name = "chkAssetWithValue"
        Me.chkAssetWithValue.Size = New System.Drawing.Size(107, 17)
        Me.chkAssetWithValue.TabIndex = 4
        Me.chkAssetWithValue.Text = "Asset With Value"
        Me.chkAssetWithValue.UseVisualStyleBackColor = True
        Me.chkAssetWithValue.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(5, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(460, 63)
        Me.PictureBox1.TabIndex = 77
        Me.PictureBox1.TabStop = False
        '
        'btn
        '
        Me.btn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn.Location = New System.Drawing.Point(624, 661)
        Me.btn.Name = "btn"
        Me.btn.Size = New System.Drawing.Size(115, 36)
        Me.btn.TabIndex = 78
        Me.btn.Text = "Save && New"
        '
        'dlgFile
        '
        Me.dlgFile.Filter = "JPEG Images (*.jpg,*.jpeg)|*.jpg;*.jpeg|Gif Images (*.gif)|*.gif|Bitmaps (*.bmp)|" & _
            "*.bmp|PNG (*.png)|*.png|All Images(*.jpg,*.jpeg,*.gif,*.bmp,*.png)|*.jpg;*.jpeg;" & _
            "*.gif;*.bmp;*.png"
        Me.dlgFile.FilterIndex = 5
        Me.dlgFile.Title = "Choose Image file"
        '
        'lblEvaluation
        '
        Me.lblEvaluation.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEvaluation.AutoSize = True
        Me.lblEvaluation.BackColor = System.Drawing.Color.Transparent
        Me.lblEvaluation.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEvaluation.ForeColor = System.Drawing.Color.Red
        Me.lblEvaluation.Location = New System.Drawing.Point(471, 5)
        Me.lblEvaluation.Name = "lblEvaluation"
        Me.lblEvaluation.Size = New System.Drawing.Size(58, 16)
        Me.lblEvaluation.TabIndex = 78
        Me.lblEvaluation.Text = "Label70"
        Me.lblEvaluation.Visible = False
        '
        'txtAstNum
        '
        Me.txtAstNum.EditValue = ""
        Me.txtAstNum.Location = New System.Drawing.Point(468, 43)
        Me.txtAstNum.Name = "txtAstNum"
        Me.txtAstNum.Properties.MaxLength = 12
        Me.txtAstNum.Properties.ReadOnly = True
        Me.txtAstNum.Size = New System.Drawing.Size(132, 19)
        Me.txtAstNum.TabIndex = 91
        Me.txtAstNum.Visible = False
        '
        'frmAssetsDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(704, 545)
        Me.Controls.Add(Me.txtAstNum)
        Me.Controls.Add(Me.lblEvaluation)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.chkAssetWithValue)
        Me.Controls.Add(Me.btnNewAsset)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.tabControl)
        Me.Controls.Add(Me.PictureBox1)
        Me.DoubleBuffered = True
        Me.MinimumSize = New System.Drawing.Size(670, 500)
        Me.Name = "frmAssetsDetails"
        Me.Text = "Asset Details & Maintenance"
        Me.GroupBox12.ResumeLayout(False)
        Me.GroupBox12.PerformLayout()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControl.ResumeLayout(False)
        Me.tabAssetInfo.ResumeLayout(False)
        Me.MainGroup.ResumeLayout(False)
        Me.MainGroup.PerformLayout()
        CType(Me.pnlInventoryStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlInventoryStatus.ResumeLayout(False)
        Me.pnlInventoryStatus.PerformLayout()
        CType(Me.imgInvStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSource.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlStatus.ResumeLayout(False)
        Me.pnlStatus.PerformLayout()
        CType(Me.imgStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLastInventory.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCreation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpImage.ResumeLayout(False)
        CType(Me.PBLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlNavigation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlNavigation.ResumeLayout(False)
        CType(Me.txtCreatedBY.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLabelPrintedCnt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAssetNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSerialNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAstDesc2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAstDesc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpLocation.ResumeLayout(False)
        Me.grpLocation.PerformLayout()
        CType(Me.txtPlant.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.grdAstHist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAstHistView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        CType(Me.grdCustHist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCustHistView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabAssetLog.ResumeLayout(False)
        CType(Me.grdLog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdLogView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAstNum.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnNewAsset As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents tabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tabAssetInfo As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents MainGroup As System.Windows.Forms.Panel
    Friend WithEvents trvLocation As ZulTree.ZulTree
    Friend WithEvents cmbEmployee As ZulLOV.ZulLOV
    Friend WithEvents btnNewAsset1 As System.Windows.Forms.Button
    Friend WithEvents grpImage As System.Windows.Forms.GroupBox
    Friend WithEvents PBLogo As System.Windows.Forms.PictureBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtAstDesc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TabPage4 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnDelImg As System.Windows.Forms.Button
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnPre As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents pnlNavigation As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents txtAstDesc2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents btn As System.Windows.Forms.Button
    Friend WithEvents grdAstHist As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdAstHistView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdCustHist As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdCustHistView As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents dlgFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblEvaluation As System.Windows.Forms.Label
    Friend WithEvents cmbCostCenter As ZulLOV.ZulLOV
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtLabelPrintedCnt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtSerialNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Sear As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkAssetWithValue As System.Windows.Forms.CheckBox
    Friend WithEvents txtCreatedBY As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents trvAssetClass As ZulTree.ZulTree
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtSubNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtAssetNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents pnlStatus As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents imgStatus As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAstNum As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents grpLocation As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtLocation As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbComp As ZulLOV.ZulLOV
    Friend WithEvents txtAssetID As ZulLOV.ZulLOV
    Friend WithEvents txtCreation As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtLastInventory As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtSource As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents pnlInventoryStatus As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblInvStatus As System.Windows.Forms.Label
    Friend WithEvents imgInvStatus As System.Windows.Forms.PictureBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents tabAssetLog As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grdLog As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdLogView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtPlant As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label18 As System.Windows.Forms.Label
End Class
