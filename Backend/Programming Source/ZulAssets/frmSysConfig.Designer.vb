<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSysConfig
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSysConfig))
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtMonthDay = New System.Windows.Forms.NumericUpDown
        Me.RdAuto = New System.Windows.Forms.RadioButton
        Me.rdManual = New System.Windows.Forms.RadioButton
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.cmbDateFormat = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.rdoIncrementalCoding = New System.Windows.Forms.RadioButton
        Me.rdoAssetsCoding = New System.Windows.Forms.RadioButton
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.cmbRptLabel = New System.Windows.Forms.ComboBox
        Me.cmbRptPrint = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chkExportServ = New System.Windows.Forms.CheckBox
        Me.chkDelete = New System.Windows.Forms.CheckBox
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.tabAssetImages = New System.Windows.Forms.TabPage
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.btnClearItmImg = New System.Windows.Forms.Button
        Me.btnClearAstImg = New System.Windows.Forms.Button
        Me.pb = New System.Windows.Forms.ProgressBar
        Me.btnExportItmImg = New System.Windows.Forms.Button
        Me.btnImportItmImg = New System.Windows.Forms.Button
        Me.btnExportAstImg = New System.Windows.Forms.Button
        Me.rdoItmImg = New System.Windows.Forms.RadioButton
        Me.btnImportAstImg = New System.Windows.Forms.Button
        Me.rdoAstImg = New System.Windows.Forms.RadioButton
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.btnDir = New System.Windows.Forms.Button
        Me.txtImgStorgePath = New DevExpress.XtraEditors.TextEdit
        Me.rdoSharedFolder = New System.Windows.Forms.RadioButton
        Me.rdoDatabase = New System.Windows.Forms.RadioButton
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.rdoAssDesc2 = New System.Windows.Forms.RadioButton
        Me.rdoAssDesc1 = New System.Windows.Forms.RadioButton
        Me.rdoItmDesc = New System.Windows.Forms.RadioButton
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.btnSelectFile = New System.Windows.Forms.Button
        Me.txtFilePath = New System.Windows.Forms.TextBox
        Me.btnImportAssetData = New System.Windows.Forms.Button
        Me.btnImportMasterData = New System.Windows.Forms.Button
        Me.chkRegenerateLoc = New System.Windows.Forms.CheckBox
        Me.chkRegenerateCat = New System.Windows.Forms.CheckBox
        Me.btnProcess = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.dlgSelectFolder = New System.Windows.Forms.FolderBrowserDialog
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.chkShowAlarmOnStartup = New System.Windows.Forms.CheckBox
        Me.txtWarrantyAlarmDays = New DevExpress.XtraEditors.SpinEdit
        Me.Label84 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtMonthDay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.tabAssetImages.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.txtImgStorgePath.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox10.SuspendLayout()
        CType(Me.txtWarrantyAlarmDays.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(50, 87)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(128, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Depreciate Assets on day"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtMonthDay)
        Me.GroupBox1.Controls.Add(Me.RdAuto)
        Me.GroupBox1.Controls.Add(Me.rdManual)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 20)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(339, 116)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Depreciation Configuration"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(239, 87)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "of the month"
        '
        'txtMonthDay
        '
        Me.txtMonthDay.Location = New System.Drawing.Point(186, 85)
        Me.txtMonthDay.Maximum = New Decimal(New Integer() {31, 0, 0, 0})
        Me.txtMonthDay.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtMonthDay.Name = "txtMonthDay"
        Me.txtMonthDay.Size = New System.Drawing.Size(47, 20)
        Me.txtMonthDay.TabIndex = 4
        Me.txtMonthDay.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'RdAuto
        '
        Me.RdAuto.AutoSize = True
        Me.RdAuto.Location = New System.Drawing.Point(22, 58)
        Me.RdAuto.Name = "RdAuto"
        Me.RdAuto.Size = New System.Drawing.Size(110, 17)
        Me.RdAuto.TabIndex = 1
        Me.RdAuto.TabStop = True
        Me.RdAuto.Text = "Auto Depreciation"
        Me.RdAuto.UseVisualStyleBackColor = True
        '
        'rdManual
        '
        Me.rdManual.AutoSize = True
        Me.rdManual.Location = New System.Drawing.Point(22, 28)
        Me.rdManual.Name = "rdManual"
        Me.rdManual.Size = New System.Drawing.Size(123, 17)
        Me.rdManual.TabIndex = 0
        Me.rdManual.TabStop = True
        Me.rdManual.Text = "Manual Depreciation"
        Me.rdManual.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.White
        Me.GroupBox5.Controls.Add(Me.cmbDateFormat)
        Me.GroupBox5.Controls.Add(Me.Label4)
        Me.GroupBox5.Location = New System.Drawing.Point(11, 142)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(339, 66)
        Me.GroupBox5.TabIndex = 2
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Personalize"
        '
        'cmbDateFormat
        '
        Me.cmbDateFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDateFormat.FormattingEnabled = True
        Me.cmbDateFormat.Items.AddRange(New Object() {"dd/MM/yyyy", "MM/dd/yyyy"})
        Me.cmbDateFormat.Location = New System.Drawing.Point(106, 29)
        Me.cmbDateFormat.Name = "cmbDateFormat"
        Me.cmbDateFormat.Size = New System.Drawing.Size(182, 21)
        Me.cmbDateFormat.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbDateFormat, "Specify the date format to use in the program")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Set Date Format"
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.White
        Me.GroupBox4.CausesValidation = False
        Me.GroupBox4.Controls.Add(Me.rdoIncrementalCoding)
        Me.GroupBox4.Controls.Add(Me.rdoAssetsCoding)
        Me.GroupBox4.Location = New System.Drawing.Point(355, 20)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(310, 116)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Assets Coding Mode"
        '
        'rdoIncrementalCoding
        '
        Me.rdoIncrementalCoding.AutoSize = True
        Me.rdoIncrementalCoding.Location = New System.Drawing.Point(23, 53)
        Me.rdoIncrementalCoding.Name = "rdoIncrementalCoding"
        Me.rdoIncrementalCoding.Size = New System.Drawing.Size(138, 17)
        Me.rdoIncrementalCoding.TabIndex = 1
        Me.rdoIncrementalCoding.Text = "Use Incremental Coding"
        Me.ToolTip1.SetToolTip(Me.rdoIncrementalCoding, "Check this option to use same series of assets numbers for all companies.")
        Me.rdoIncrementalCoding.UseVisualStyleBackColor = True
        '
        'rdoAssetsCoding
        '
        Me.rdoAssetsCoding.AutoSize = True
        Me.rdoAssetsCoding.Checked = True
        Me.rdoAssetsCoding.Location = New System.Drawing.Point(23, 21)
        Me.rdoAssetsCoding.Name = "rdoAssetsCoding"
        Me.rdoAssetsCoding.Size = New System.Drawing.Size(161, 17)
        Me.rdoAssetsCoding.TabIndex = 0
        Me.rdoAssetsCoding.TabStop = True
        Me.rdoAssetsCoding.Text = "Use Assets Coding Definition"
        Me.ToolTip1.SetToolTip(Me.rdoAssetsCoding, "Check this option to use Assets Coding Defined in Assets Coding Definition form t" & _
                "o generate asset numbers.")
        Me.rdoAssetsCoding.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.White
        Me.GroupBox3.Controls.Add(Me.cmbRptLabel)
        Me.GroupBox3.Controls.Add(Me.cmbRptPrint)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Location = New System.Drawing.Point(10, 20)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(644, 177)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Printer Driver Settings"
        '
        'cmbRptLabel
        '
        Me.cmbRptLabel.FormattingEnabled = True
        Me.cmbRptLabel.Location = New System.Drawing.Point(16, 112)
        Me.cmbRptLabel.Name = "cmbRptLabel"
        Me.cmbRptLabel.Size = New System.Drawing.Size(586, 21)
        Me.cmbRptLabel.TabIndex = 3
        '
        'cmbRptPrint
        '
        Me.cmbRptPrint.FormattingEnabled = True
        Me.cmbRptPrint.Location = New System.Drawing.Point(16, 48)
        Me.cmbRptPrint.Name = "cmbRptPrint"
        Me.cmbRptPrint.Size = New System.Drawing.Size(586, 21)
        Me.cmbRptPrint.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "For Label Printing"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "For Reporting Services"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.White
        Me.GroupBox2.Controls.Add(Me.chkExportServ)
        Me.GroupBox2.Controls.Add(Me.chkDelete)
        Me.GroupBox2.Location = New System.Drawing.Point(356, 142)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(309, 66)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Data Maintenance"
        '
        'chkExportServ
        '
        Me.chkExportServ.Location = New System.Drawing.Point(166, 24)
        Me.chkExportServ.Name = "chkExportServ"
        Me.chkExportServ.Size = New System.Drawing.Size(133, 24)
        Me.chkExportServ.TabIndex = 1
        Me.chkExportServ.Text = "Save to Export Server"
        Me.ToolTip1.SetToolTip(Me.chkExportServ, "After updating the data save it to remote server.")
        Me.chkExportServ.UseVisualStyleBackColor = True
        '
        'chkDelete
        '
        Me.chkDelete.Location = New System.Drawing.Point(16, 27)
        Me.chkDelete.Name = "chkDelete"
        Me.chkDelete.Size = New System.Drawing.Size(144, 18)
        Me.chkDelete.TabIndex = 0
        Me.chkDelete.Text = "Delete Data Permanently"
        Me.ToolTip1.SetToolTip(Me.chkDelete, "Check to delete the data permanently from the database after clicking the delete " & _
                "button.")
        Me.chkDelete.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.tabAssetImages)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(11, 89)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(684, 295)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.White
        Me.TabPage2.Controls.Add(Me.GroupBox10)
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Controls.Add(Me.GroupBox5)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(676, 269)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "General"
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.White
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(676, 269)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Printing"
        '
        'tabAssetImages
        '
        Me.tabAssetImages.BackColor = System.Drawing.Color.White
        Me.tabAssetImages.Controls.Add(Me.GroupBox8)
        Me.tabAssetImages.Controls.Add(Me.GroupBox6)
        Me.tabAssetImages.Location = New System.Drawing.Point(4, 22)
        Me.tabAssetImages.Name = "tabAssetImages"
        Me.tabAssetImages.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAssetImages.Size = New System.Drawing.Size(676, 269)
        Me.tabAssetImages.TabIndex = 2
        Me.tabAssetImages.Text = "Asset Images"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.btnClearItmImg)
        Me.GroupBox8.Controls.Add(Me.btnClearAstImg)
        Me.GroupBox8.Controls.Add(Me.pb)
        Me.GroupBox8.Controls.Add(Me.btnExportItmImg)
        Me.GroupBox8.Controls.Add(Me.btnImportItmImg)
        Me.GroupBox8.Controls.Add(Me.btnExportAstImg)
        Me.GroupBox8.Controls.Add(Me.rdoItmImg)
        Me.GroupBox8.Controls.Add(Me.btnImportAstImg)
        Me.GroupBox8.Controls.Add(Me.rdoAstImg)
        Me.GroupBox8.Location = New System.Drawing.Point(10, 132)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(652, 119)
        Me.GroupBox8.TabIndex = 1
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Asset or Item based images"
        '
        'btnClearItmImg
        '
        Me.btnClearItmImg.Location = New System.Drawing.Point(433, 63)
        Me.btnClearItmImg.Name = "btnClearItmImg"
        Me.btnClearItmImg.Size = New System.Drawing.Size(130, 23)
        Me.btnClearItmImg.TabIndex = 7
        Me.btnClearItmImg.Text = "Clear Items Images"
        Me.ToolTip1.SetToolTip(Me.btnClearItmImg, "Delete all items images from the database.")
        Me.btnClearItmImg.UseVisualStyleBackColor = True
        '
        'btnClearAstImg
        '
        Me.btnClearAstImg.Location = New System.Drawing.Point(433, 28)
        Me.btnClearAstImg.Name = "btnClearAstImg"
        Me.btnClearAstImg.Size = New System.Drawing.Size(130, 23)
        Me.btnClearAstImg.TabIndex = 3
        Me.btnClearAstImg.Text = "Clear Assets Images"
        Me.ToolTip1.SetToolTip(Me.btnClearAstImg, "Delete all assets images from the database.")
        Me.btnClearAstImg.UseVisualStyleBackColor = True
        '
        'pb
        '
        Me.pb.Location = New System.Drawing.Point(17, 100)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(625, 13)
        Me.pb.TabIndex = 8
        Me.pb.Visible = False
        '
        'btnExportItmImg
        '
        Me.btnExportItmImg.Location = New System.Drawing.Point(282, 63)
        Me.btnExportItmImg.Name = "btnExportItmImg"
        Me.btnExportItmImg.Size = New System.Drawing.Size(130, 23)
        Me.btnExportItmImg.TabIndex = 6
        Me.btnExportItmImg.Text = "Export Items Images"
        Me.ToolTip1.SetToolTip(Me.btnExportItmImg, "Export Items Images from the database to folder.")
        Me.btnExportItmImg.UseVisualStyleBackColor = True
        '
        'btnImportItmImg
        '
        Me.btnImportItmImg.Location = New System.Drawing.Point(133, 63)
        Me.btnImportItmImg.Name = "btnImportItmImg"
        Me.btnImportItmImg.Size = New System.Drawing.Size(130, 23)
        Me.btnImportItmImg.TabIndex = 5
        Me.btnImportItmImg.Text = "Import Items Images"
        Me.ToolTip1.SetToolTip(Me.btnImportItmImg, "Import Items images from folder to database.")
        Me.btnImportItmImg.UseVisualStyleBackColor = True
        '
        'btnExportAstImg
        '
        Me.btnExportAstImg.Location = New System.Drawing.Point(282, 28)
        Me.btnExportAstImg.Name = "btnExportAstImg"
        Me.btnExportAstImg.Size = New System.Drawing.Size(130, 23)
        Me.btnExportAstImg.TabIndex = 2
        Me.btnExportAstImg.Text = "Export Assets Images"
        Me.ToolTip1.SetToolTip(Me.btnExportAstImg, "Export assets Images from the database to folder.")
        Me.btnExportAstImg.UseVisualStyleBackColor = True
        '
        'rdoItmImg
        '
        Me.rdoItmImg.AutoSize = True
        Me.rdoItmImg.Location = New System.Drawing.Point(17, 66)
        Me.rdoItmImg.Name = "rdoItmImg"
        Me.rdoItmImg.Size = New System.Drawing.Size(82, 17)
        Me.rdoItmImg.TabIndex = 4
        Me.rdoItmImg.TabStop = True
        Me.rdoItmImg.Text = "Item Images"
        Me.rdoItmImg.UseVisualStyleBackColor = True
        '
        'btnImportAstImg
        '
        Me.btnImportAstImg.Location = New System.Drawing.Point(133, 28)
        Me.btnImportAstImg.Name = "btnImportAstImg"
        Me.btnImportAstImg.Size = New System.Drawing.Size(130, 23)
        Me.btnImportAstImg.TabIndex = 1
        Me.btnImportAstImg.Text = "Import Assets Images"
        Me.ToolTip1.SetToolTip(Me.btnImportAstImg, "Import assets images from folder to database.")
        Me.btnImportAstImg.UseVisualStyleBackColor = True
        '
        'rdoAstImg
        '
        Me.rdoAstImg.AutoSize = True
        Me.rdoAstImg.Location = New System.Drawing.Point(17, 31)
        Me.rdoAstImg.Name = "rdoAstImg"
        Me.rdoAstImg.Size = New System.Drawing.Size(88, 17)
        Me.rdoAstImg.TabIndex = 0
        Me.rdoAstImg.TabStop = True
        Me.rdoAstImg.Text = "Asset Images"
        Me.rdoAstImg.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.btnDir)
        Me.GroupBox6.Controls.Add(Me.txtImgStorgePath)
        Me.GroupBox6.Controls.Add(Me.rdoSharedFolder)
        Me.GroupBox6.Controls.Add(Me.rdoDatabase)
        Me.GroupBox6.Location = New System.Drawing.Point(10, 21)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(652, 105)
        Me.GroupBox6.TabIndex = 0
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Storage  Location"
        '
        'btnDir
        '
        Me.btnDir.Image = Global.ZulAssets.My.Resources.Icons.Open
        Me.btnDir.Location = New System.Drawing.Point(614, 61)
        Me.btnDir.Name = "btnDir"
        Me.btnDir.Size = New System.Drawing.Size(28, 23)
        Me.btnDir.TabIndex = 3
        Me.btnDir.UseVisualStyleBackColor = True
        '
        'txtImgStorgePath
        '
        Me.txtImgStorgePath.Location = New System.Drawing.Point(133, 63)
        Me.txtImgStorgePath.Name = "txtImgStorgePath"
        Me.txtImgStorgePath.Properties.ReadOnly = True
        Me.txtImgStorgePath.Size = New System.Drawing.Size(479, 20)
        Me.txtImgStorgePath.TabIndex = 2
        '
        'rdoSharedFolder
        '
        Me.rdoSharedFolder.AutoSize = True
        Me.rdoSharedFolder.Location = New System.Drawing.Point(17, 64)
        Me.rdoSharedFolder.Name = "rdoSharedFolder"
        Me.rdoSharedFolder.Size = New System.Drawing.Size(91, 17)
        Me.rdoSharedFolder.TabIndex = 1
        Me.rdoSharedFolder.TabStop = True
        Me.rdoSharedFolder.Text = "Shared Folder"
        Me.rdoSharedFolder.UseVisualStyleBackColor = True
        '
        'rdoDatabase
        '
        Me.rdoDatabase.AutoSize = True
        Me.rdoDatabase.Location = New System.Drawing.Point(17, 28)
        Me.rdoDatabase.Name = "rdoDatabase"
        Me.rdoDatabase.Size = New System.Drawing.Size(71, 17)
        Me.rdoDatabase.TabIndex = 0
        Me.rdoDatabase.TabStop = True
        Me.rdoDatabase.Text = "Database"
        Me.rdoDatabase.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.Color.White
        Me.TabPage4.Controls.Add(Me.GroupBox7)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(676, 269)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Asset Description"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.rdoAssDesc2)
        Me.GroupBox7.Controls.Add(Me.rdoAssDesc1)
        Me.GroupBox7.Controls.Add(Me.rdoItmDesc)
        Me.GroupBox7.Location = New System.Drawing.Point(10, 20)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(339, 182)
        Me.GroupBox7.TabIndex = 1
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Asset Description for reports"
        '
        'rdoAssDesc2
        '
        Me.rdoAssDesc2.AutoSize = True
        Me.rdoAssDesc2.Location = New System.Drawing.Point(34, 124)
        Me.rdoAssDesc2.Name = "rdoAssDesc2"
        Me.rdoAssDesc2.Size = New System.Drawing.Size(116, 17)
        Me.rdoAssDesc2.TabIndex = 3
        Me.rdoAssDesc2.TabStop = True
        Me.rdoAssDesc2.Text = "Asset Description 2"
        Me.rdoAssDesc2.UseVisualStyleBackColor = True
        '
        'rdoAssDesc1
        '
        Me.rdoAssDesc1.AutoSize = True
        Me.rdoAssDesc1.Location = New System.Drawing.Point(34, 83)
        Me.rdoAssDesc1.Name = "rdoAssDesc1"
        Me.rdoAssDesc1.Size = New System.Drawing.Size(116, 17)
        Me.rdoAssDesc1.TabIndex = 2
        Me.rdoAssDesc1.TabStop = True
        Me.rdoAssDesc1.Text = "Asset Description 1"
        Me.rdoAssDesc1.UseVisualStyleBackColor = True
        '
        'rdoItmDesc
        '
        Me.rdoItmDesc.AutoSize = True
        Me.rdoItmDesc.Location = New System.Drawing.Point(34, 45)
        Me.rdoItmDesc.Name = "rdoItmDesc"
        Me.rdoItmDesc.Size = New System.Drawing.Size(101, 17)
        Me.rdoItmDesc.TabIndex = 1
        Me.rdoItmDesc.TabStop = True
        Me.rdoItmDesc.Text = "Item Description"
        Me.rdoItmDesc.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.GroupBox9)
        Me.TabPage3.Controls.Add(Me.chkRegenerateLoc)
        Me.TabPage3.Controls.Add(Me.chkRegenerateCat)
        Me.TabPage3.Controls.Add(Me.btnProcess)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(676, 269)
        Me.TabPage3.TabIndex = 4
        Me.TabPage3.Text = "Tools"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.btnSelectFile)
        Me.GroupBox9.Controls.Add(Me.txtFilePath)
        Me.GroupBox9.Controls.Add(Me.btnImportAssetData)
        Me.GroupBox9.Controls.Add(Me.btnImportMasterData)
        Me.GroupBox9.Location = New System.Drawing.Point(19, 137)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(639, 107)
        Me.GroupBox9.TabIndex = 8
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Importing Data"
        Me.GroupBox9.Visible = False
        '
        'btnSelectFile
        '
        Me.btnSelectFile.Location = New System.Drawing.Point(585, 56)
        Me.btnSelectFile.Name = "btnSelectFile"
        Me.btnSelectFile.Size = New System.Drawing.Size(32, 23)
        Me.btnSelectFile.TabIndex = 11
        Me.btnSelectFile.Text = "..."
        Me.btnSelectFile.UseVisualStyleBackColor = True
        '
        'txtFilePath
        '
        Me.txtFilePath.Location = New System.Drawing.Point(139, 58)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(440, 20)
        Me.txtFilePath.TabIndex = 10
        '
        'btnImportAssetData
        '
        Me.btnImportAssetData.Location = New System.Drawing.Point(3, 58)
        Me.btnImportAssetData.Name = "btnImportAssetData"
        Me.btnImportAssetData.Size = New System.Drawing.Size(130, 23)
        Me.btnImportAssetData.TabIndex = 9
        Me.btnImportAssetData.Text = "Import Asset Data"
        Me.btnImportAssetData.UseVisualStyleBackColor = True
        '
        'btnImportMasterData
        '
        Me.btnImportMasterData.Location = New System.Drawing.Point(3, 28)
        Me.btnImportMasterData.Name = "btnImportMasterData"
        Me.btnImportMasterData.Size = New System.Drawing.Size(130, 23)
        Me.btnImportMasterData.TabIndex = 8
        Me.btnImportMasterData.Text = "Import Master Data"
        Me.btnImportMasterData.UseVisualStyleBackColor = True
        '
        'chkRegenerateLoc
        '
        Me.chkRegenerateLoc.AutoSize = True
        Me.chkRegenerateLoc.Location = New System.Drawing.Point(28, 53)
        Me.chkRegenerateLoc.Name = "chkRegenerateLoc"
        Me.chkRegenerateLoc.Size = New System.Drawing.Size(214, 17)
        Me.chkRegenerateLoc.TabIndex = 3
        Me.chkRegenerateLoc.Text = "Regenerate complete location hierarchy"
        Me.chkRegenerateLoc.UseVisualStyleBackColor = True
        '
        'chkRegenerateCat
        '
        Me.chkRegenerateCat.AutoSize = True
        Me.chkRegenerateCat.Location = New System.Drawing.Point(28, 20)
        Me.chkRegenerateCat.Name = "chkRegenerateCat"
        Me.chkRegenerateCat.Size = New System.Drawing.Size(218, 17)
        Me.chkRegenerateCat.TabIndex = 2
        Me.chkRegenerateCat.Text = "Regenerate complete category hierarchy"
        Me.chkRegenerateCat.UseVisualStyleBackColor = True
        '
        'btnProcess
        '
        Me.btnProcess.Location = New System.Drawing.Point(236, 108)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(75, 23)
        Me.btnProcess.TabIndex = 1
        Me.btnProcess.Text = "Process"
        Me.btnProcess.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Image = Global.ZulAssets.My.Resources.Icons.save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(479, 389)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 28)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "&Apply"
        '
        'btnExit
        '
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Image = Global.ZulAssets.My.Resources.Icons.Close
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(590, 389)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 28)
        Me.btnExit.TabIndex = 6
        Me.btnExit.Text = "&Close"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(11, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(353, 70)
        Me.PictureBox1.TabIndex = 71
        Me.PictureBox1.TabStop = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.txtWarrantyAlarmDays)
        Me.GroupBox10.Controls.Add(Me.Label84)
        Me.GroupBox10.Controls.Add(Me.chkShowAlarmOnStartup)
        Me.GroupBox10.Location = New System.Drawing.Point(11, 214)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(644, 49)
        Me.GroupBox10.TabIndex = 4
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Warranty Alarm"
        '
        'chkShowAlarmOnStartup
        '
        Me.chkShowAlarmOnStartup.Location = New System.Drawing.Point(21, 23)
        Me.chkShowAlarmOnStartup.Name = "chkShowAlarmOnStartup"
        Me.chkShowAlarmOnStartup.Size = New System.Drawing.Size(144, 18)
        Me.chkShowAlarmOnStartup.TabIndex = 1
        Me.chkShowAlarmOnStartup.Text = "Show Alarm On Startup"
        Me.chkShowAlarmOnStartup.UseVisualStyleBackColor = True
        '
        'txtWarrantyAlarmDays
        '
        Me.txtWarrantyAlarmDays.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtWarrantyAlarmDays.Location = New System.Drawing.Point(342, 20)
        Me.txtWarrantyAlarmDays.Name = "txtWarrantyAlarmDays"
        Me.txtWarrantyAlarmDays.Properties.Appearance.Options.UseTextOptions = True
        Me.txtWarrantyAlarmDays.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.txtWarrantyAlarmDays.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.txtWarrantyAlarmDays.Properties.MaxValue = New Decimal(New Integer() {730000, 0, 0, 0})
        Me.txtWarrantyAlarmDays.Size = New System.Drawing.Size(93, 20)
        Me.txtWarrantyAlarmDays.TabIndex = 95
        '
        'Label84
        '
        Me.Label84.AutoSize = True
        Me.Label84.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label84.Location = New System.Drawing.Point(215, 23)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(101, 13)
        Me.Label84.TabIndex = 94
        Me.Label84.Text = "Alarm Before(Days)"
        '
        'frmSysConfig
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(705, 431)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.PictureBox1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSysConfig"
        Me.Text = "System Configuration"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtMonthDay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.tabAssetImages.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.txtImgStorgePath.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        CType(Me.txtWarrantyAlarmDays.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents RdAuto As System.Windows.Forms.RadioButton
    Friend WithEvents rdManual As System.Windows.Forms.RadioButton
    Friend WithEvents chkDelete As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbRptLabel As System.Windows.Forms.ComboBox
    Friend WithEvents cmbRptPrint As System.Windows.Forms.ComboBox
    Friend WithEvents chkExportServ As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoIncrementalCoding As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAssetsCoding As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbDateFormat As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents tabAssetImages As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoAssDesc2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAssDesc1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoItmDesc As System.Windows.Forms.RadioButton
    Friend WithEvents rdoItmImg As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAstImg As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSharedFolder As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDatabase As System.Windows.Forms.RadioButton
    Friend WithEvents txtImgStorgePath As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnDir As System.Windows.Forms.Button
    Friend WithEvents dlgSelectFolder As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btnExportItmImg As System.Windows.Forms.Button
    Friend WithEvents btnImportItmImg As System.Windows.Forms.Button
    Friend WithEvents btnExportAstImg As System.Windows.Forms.Button
    Friend WithEvents btnImportAstImg As System.Windows.Forms.Button
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents btnClearItmImg As System.Windows.Forms.Button
    Friend WithEvents btnClearAstImg As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtMonthDay As System.Windows.Forms.NumericUpDown
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents chkRegenerateLoc As System.Windows.Forms.CheckBox
    Friend WithEvents chkRegenerateCat As System.Windows.Forms.CheckBox
    Friend WithEvents btnProcess As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSelectFile As System.Windows.Forms.Button
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents btnImportAssetData As System.Windows.Forms.Button
    Friend WithEvents btnImportMasterData As System.Windows.Forms.Button
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents chkShowAlarmOnStartup As System.Windows.Forms.CheckBox
    Friend WithEvents txtWarrantyAlarmDays As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents Label84 As System.Windows.Forms.Label
End Class
