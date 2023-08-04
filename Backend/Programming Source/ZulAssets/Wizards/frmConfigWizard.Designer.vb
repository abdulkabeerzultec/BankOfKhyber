<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigWizard
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
        Me.WizardControl1 = New DevExpress.XtraWizard.WizardControl
        Me.WelcomeWizardPage1 = New DevExpress.XtraWizard.WelcomeWizardPage
        Me.WizardPage1 = New DevExpress.XtraWizard.WizardPage
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnDelImg = New System.Windows.Forms.Button
        Me.PBLogo = New System.Windows.Forms.PictureBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtEmail = New DevExpress.XtraEditors.TextEdit
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtPhone = New DevExpress.XtraEditors.TextEdit
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtFax = New DevExpress.XtraEditors.TextEdit
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtCountry = New DevExpress.XtraEditors.TextEdit
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPCode = New DevExpress.XtraEditors.TextEdit
        Me.txtState = New DevExpress.XtraEditors.TextEdit
        Me.txtCity = New DevExpress.XtraEditors.TextEdit
        Me.txtName = New DevExpress.XtraEditors.TextEdit
        Me.txtAddress = New DevExpress.XtraEditors.MemoEdit
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.CompletionWizardPage1 = New DevExpress.XtraWizard.CompletionWizardPage
        Me.WizardPage2 = New DevExpress.XtraWizard.WizardPage
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtMonthDay = New System.Windows.Forms.NumericUpDown
        Me.RdAuto = New System.Windows.Forms.RadioButton
        Me.rdManual = New System.Windows.Forms.RadioButton
        Me.Label11 = New System.Windows.Forms.Label
        Me.WizardPage3 = New DevExpress.XtraWizard.WizardPage
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.rdoIncrementalCoding = New System.Windows.Forms.RadioButton
        Me.rdoAssetsCoding = New System.Windows.Forms.RadioButton
        Me.WizardPage4 = New DevExpress.XtraWizard.WizardPage
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.cmbDateFormat = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.WizardPage5 = New DevExpress.XtraWizard.WizardPage
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.cmbRptLabel = New System.Windows.Forms.ComboBox
        Me.cmbRptPrint = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.WizardPage6 = New DevExpress.XtraWizard.WizardPage
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.rdoItmImg = New System.Windows.Forms.RadioButton
        Me.rdoAstImg = New System.Windows.Forms.RadioButton
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.btnDir = New System.Windows.Forms.Button
        Me.txtImgStorgePath = New DevExpress.XtraEditors.TextEdit
        Me.rdoSharedFolder = New System.Windows.Forms.RadioButton
        Me.rdoDatabase = New System.Windows.Forms.RadioButton
        CType(Me.WizardControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.WizardControl1.SuspendLayout()
        Me.WizardPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PBLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPhone.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCountry.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtState.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddress.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.WizardPage2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtMonthDay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.WizardPage3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.WizardPage4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.WizardPage5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.WizardPage6.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.txtImgStorgePath.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'WizardControl1
        '
        Me.WizardControl1.Controls.Add(Me.WelcomeWizardPage1)
        Me.WizardControl1.Controls.Add(Me.WizardPage1)
        Me.WizardControl1.Controls.Add(Me.CompletionWizardPage1)
        Me.WizardControl1.Controls.Add(Me.WizardPage2)
        Me.WizardControl1.Controls.Add(Me.WizardPage3)
        Me.WizardControl1.Controls.Add(Me.WizardPage4)
        Me.WizardControl1.Controls.Add(Me.WizardPage5)
        Me.WizardControl1.Controls.Add(Me.WizardPage6)
        Me.WizardControl1.Name = "WizardControl1"
        Me.WizardControl1.Pages.AddRange(New DevExpress.XtraWizard.BaseWizardPage() {Me.WelcomeWizardPage1, Me.WizardPage1, Me.WizardPage2, Me.WizardPage3, Me.WizardPage4, Me.WizardPage5, Me.WizardPage6, Me.CompletionWizardPage1})
        '
        'WelcomeWizardPage1
        '
        Me.WelcomeWizardPage1.IntroductionText = "This wizard simplifies system configuration through a series of simple steps"
        Me.WelcomeWizardPage1.Name = "WelcomeWizardPage1"
        Me.WelcomeWizardPage1.Size = New System.Drawing.Size(364, 276)
        Me.WelcomeWizardPage1.Text = "System Configuration"
        '
        'WizardPage1
        '
        Me.WizardPage1.Controls.Add(Me.GroupBox1)
        Me.WizardPage1.Controls.Add(Me.Label9)
        Me.WizardPage1.Controls.Add(Me.txtEmail)
        Me.WizardPage1.Controls.Add(Me.Label7)
        Me.WizardPage1.Controls.Add(Me.txtPhone)
        Me.WizardPage1.Controls.Add(Me.Label3)
        Me.WizardPage1.Controls.Add(Me.Label8)
        Me.WizardPage1.Controls.Add(Me.txtFax)
        Me.WizardPage1.Controls.Add(Me.Label6)
        Me.WizardPage1.Controls.Add(Me.Label5)
        Me.WizardPage1.Controls.Add(Me.txtCountry)
        Me.WizardPage1.Controls.Add(Me.Label4)
        Me.WizardPage1.Controls.Add(Me.txtPCode)
        Me.WizardPage1.Controls.Add(Me.txtState)
        Me.WizardPage1.Controls.Add(Me.txtCity)
        Me.WizardPage1.Controls.Add(Me.txtName)
        Me.WizardPage1.Controls.Add(Me.txtAddress)
        Me.WizardPage1.Controls.Add(Me.Label1)
        Me.WizardPage1.Controls.Add(Me.Label2)
        Me.WizardPage1.Name = "WizardPage1"
        Me.WizardPage1.Size = New System.Drawing.Size(549, 264)
        Me.WizardPage1.Text = "Company Information"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.btnDelImg)
        Me.GroupBox1.Controls.Add(Me.PBLogo)
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(350, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(186, 202)
        Me.GroupBox1.TabIndex = 22
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Company Logo"
        '
        'btnDelImg
        '
        Me.btnDelImg.Image = Global.ZulAssets.My.Resources.Icons.delete
        Me.btnDelImg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelImg.Location = New System.Drawing.Point(44, 173)
        Me.btnDelImg.Name = "btnDelImg"
        Me.btnDelImg.Size = New System.Drawing.Size(113, 23)
        Me.btnDelImg.TabIndex = 14
        Me.btnDelImg.Text = "&Delete Image"
        Me.btnDelImg.UseVisualStyleBackColor = True
        Me.btnDelImg.Visible = False
        '
        'PBLogo
        '
        Me.PBLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PBLogo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PBLogo.Location = New System.Drawing.Point(15, 19)
        Me.PBLogo.Name = "PBLogo"
        Me.PBLogo.Size = New System.Drawing.Size(160, 151)
        Me.PBLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBLogo.TabIndex = 9
        Me.PBLogo.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(282, 245)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 13)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Phone"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(350, 214)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Properties.MaxLength = 50
        Me.txtEmail.Size = New System.Drawing.Size(186, 19)
        Me.txtEmail.TabIndex = 19
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(282, 217)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 13)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Email"
        '
        'txtPhone
        '
        Me.txtPhone.Location = New System.Drawing.Point(350, 241)
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Properties.MaxLength = 50
        Me.txtPhone.Size = New System.Drawing.Size(186, 19)
        Me.txtPhone.TabIndex = 20
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(16, 124)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(26, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "City"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(16, 221)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(25, 13)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Fax"
        '
        'txtFax
        '
        Me.txtFax.Location = New System.Drawing.Point(105, 214)
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Properties.MaxLength = 50
        Me.txtFax.Size = New System.Drawing.Size(160, 19)
        Me.txtFax.TabIndex = 15
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(16, 188)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Country"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(16, 247)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Zip/Postal Code"
        '
        'txtCountry
        '
        Me.txtCountry.Location = New System.Drawing.Point(105, 184)
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.Properties.MaxLength = 50
        Me.txtCountry.Size = New System.Drawing.Size(160, 19)
        Me.txtCountry.TabIndex = 13
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(16, 154)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "State/Province"
        '
        'txtPCode
        '
        Me.txtPCode.Location = New System.Drawing.Point(105, 245)
        Me.txtPCode.Name = "txtPCode"
        Me.txtPCode.Properties.MaxLength = 50
        Me.txtPCode.Size = New System.Drawing.Size(160, 19)
        Me.txtPCode.TabIndex = 16
        '
        'txtState
        '
        Me.txtState.Location = New System.Drawing.Point(105, 152)
        Me.txtState.Name = "txtState"
        Me.txtState.Properties.MaxLength = 50
        Me.txtState.Size = New System.Drawing.Size(160, 19)
        Me.txtState.TabIndex = 10
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(105, 122)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Properties.MaxLength = 50
        Me.txtCity.Size = New System.Drawing.Size(160, 19)
        Me.txtCity.TabIndex = 8
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(105, 15)
        Me.txtName.Name = "txtName"
        Me.txtName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtName.Properties.Appearance.Options.UseBackColor = True
        Me.txtName.Properties.MaxLength = 50
        Me.txtName.Size = New System.Drawing.Size(160, 19)
        Me.txtName.TabIndex = 3
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(105, 49)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Properties.MaxLength = 200
        Me.txtAddress.Size = New System.Drawing.Size(160, 64)
        Me.txtAddress.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(20, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = " Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(20, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Address"
        '
        'CompletionWizardPage1
        '
        Me.CompletionWizardPage1.Name = "CompletionWizardPage1"
        Me.CompletionWizardPage1.Size = New System.Drawing.Size(364, 276)
        '
        'WizardPage2
        '
        Me.WizardPage2.Controls.Add(Me.GroupBox2)
        Me.WizardPage2.DescriptionText = "Depreciation Configuration provides user facility to control the assets depreciat" & _
            "ion period"
        Me.WizardPage2.Name = "WizardPage2"
        Me.WizardPage2.Size = New System.Drawing.Size(549, 264)
        Me.WizardPage2.Text = "Depreciation Configuration "
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.White
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.txtMonthDay)
        Me.GroupBox2.Controls.Add(Me.RdAuto)
        Me.GroupBox2.Controls.Add(Me.rdManual)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Location = New System.Drawing.Point(46, 45)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(339, 116)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Depreciation Configuration"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(239, 87)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 13)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "of the month"
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
        Me.RdAuto.Size = New System.Drawing.Size(111, 17)
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
        Me.rdManual.Size = New System.Drawing.Size(122, 17)
        Me.rdManual.TabIndex = 0
        Me.rdManual.TabStop = True
        Me.rdManual.Text = "Manual Depreciation"
        Me.rdManual.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(50, 87)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(130, 13)
        Me.Label11.TabIndex = 2
        Me.Label11.Text = "Depreciate Assets on day"
        '
        'WizardPage3
        '
        Me.WizardPage3.Controls.Add(Me.GroupBox4)
        Me.WizardPage3.DescriptionText = "Asset Coding Mode provides two ways to generate the Asset Number"
        Me.WizardPage3.Name = "WizardPage3"
        Me.WizardPage3.Size = New System.Drawing.Size(549, 264)
        Me.WizardPage3.Text = "Asset Coding Mode "
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.White
        Me.GroupBox4.CausesValidation = False
        Me.GroupBox4.Controls.Add(Me.rdoIncrementalCoding)
        Me.GroupBox4.Controls.Add(Me.rdoAssetsCoding)
        Me.GroupBox4.Location = New System.Drawing.Point(120, 75)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(310, 116)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Assets Coding Mode"
        '
        'rdoIncrementalCoding
        '
        Me.rdoIncrementalCoding.AutoSize = True
        Me.rdoIncrementalCoding.Location = New System.Drawing.Point(23, 53)
        Me.rdoIncrementalCoding.Name = "rdoIncrementalCoding"
        Me.rdoIncrementalCoding.Size = New System.Drawing.Size(139, 17)
        Me.rdoIncrementalCoding.TabIndex = 1
        Me.rdoIncrementalCoding.Text = "Use Incremental Coding"
        Me.rdoIncrementalCoding.UseVisualStyleBackColor = True
        '
        'rdoAssetsCoding
        '
        Me.rdoAssetsCoding.AutoSize = True
        Me.rdoAssetsCoding.Checked = True
        Me.rdoAssetsCoding.Location = New System.Drawing.Point(23, 21)
        Me.rdoAssetsCoding.Name = "rdoAssetsCoding"
        Me.rdoAssetsCoding.Size = New System.Drawing.Size(162, 17)
        Me.rdoAssetsCoding.TabIndex = 0
        Me.rdoAssetsCoding.TabStop = True
        Me.rdoAssetsCoding.Text = "Use Assets Coding Definition"
        Me.rdoAssetsCoding.UseVisualStyleBackColor = True
        '
        'WizardPage4
        '
        Me.WizardPage4.Controls.Add(Me.GroupBox5)
        Me.WizardPage4.Name = "WizardPage4"
        Me.WizardPage4.Size = New System.Drawing.Size(549, 264)
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.White
        Me.GroupBox5.Controls.Add(Me.cmbDateFormat)
        Me.GroupBox5.Controls.Add(Me.Label12)
        Me.GroupBox5.Location = New System.Drawing.Point(106, 100)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(339, 66)
        Me.GroupBox5.TabIndex = 3
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
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(14, 32)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(86, 13)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Set Date Format"
        '
        'WizardPage5
        '
        Me.WizardPage5.Controls.Add(Me.GroupBox3)
        Me.WizardPage5.DescriptionText = "select the printers that ZulAssets program will use to print the Data reports and" & _
            " Labels."
        Me.WizardPage5.Name = "WizardPage5"
        Me.WizardPage5.Size = New System.Drawing.Size(549, 264)
        Me.WizardPage5.Text = "Printing"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.White
        Me.GroupBox3.Controls.Add(Me.cmbRptLabel)
        Me.GroupBox3.Controls.Add(Me.cmbRptPrint)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Location = New System.Drawing.Point(14, 26)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(489, 177)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Printer Driver Settings"
        '
        'cmbRptLabel
        '
        Me.cmbRptLabel.FormattingEnabled = True
        Me.cmbRptLabel.Location = New System.Drawing.Point(16, 112)
        Me.cmbRptLabel.Name = "cmbRptLabel"
        Me.cmbRptLabel.Size = New System.Drawing.Size(455, 21)
        Me.cmbRptLabel.TabIndex = 3
        '
        'cmbRptPrint
        '
        Me.cmbRptPrint.FormattingEnabled = True
        Me.cmbRptPrint.Location = New System.Drawing.Point(16, 48)
        Me.cmbRptPrint.Name = "cmbRptPrint"
        Me.cmbRptPrint.Size = New System.Drawing.Size(455, 21)
        Me.cmbRptPrint.TabIndex = 1
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(16, 88)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(90, 13)
        Me.Label13.TabIndex = 2
        Me.Label13.Text = "For Label Printing"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(16, 24)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(116, 13)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "For Reporting Services"
        '
        'WizardPage6
        '
        Me.WizardPage6.Controls.Add(Me.GroupBox8)
        Me.WizardPage6.Controls.Add(Me.GroupBox6)
        Me.WizardPage6.DescriptionText = "Select the storage location"
        Me.WizardPage6.Name = "WizardPage6"
        Me.WizardPage6.Size = New System.Drawing.Size(549, 264)
        Me.WizardPage6.Text = "Asset Images"
        '
        'GroupBox8
        '
        Me.GroupBox8.BackColor = System.Drawing.Color.White
        Me.GroupBox8.Controls.Add(Me.rdoItmImg)
        Me.GroupBox8.Controls.Add(Me.rdoAstImg)
        Me.GroupBox8.Location = New System.Drawing.Point(22, 124)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(512, 119)
        Me.GroupBox8.TabIndex = 2
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Asset or Item based images"
        '
        'rdoItmImg
        '
        Me.rdoItmImg.AutoSize = True
        Me.rdoItmImg.Location = New System.Drawing.Point(17, 66)
        Me.rdoItmImg.Name = "rdoItmImg"
        Me.rdoItmImg.Size = New System.Drawing.Size(85, 17)
        Me.rdoItmImg.TabIndex = 4
        Me.rdoItmImg.TabStop = True
        Me.rdoItmImg.Text = "Item Images"
        Me.rdoItmImg.UseVisualStyleBackColor = True
        '
        'rdoAstImg
        '
        Me.rdoAstImg.AutoSize = True
        Me.rdoAstImg.Location = New System.Drawing.Point(17, 31)
        Me.rdoAstImg.Name = "rdoAstImg"
        Me.rdoAstImg.Size = New System.Drawing.Size(90, 17)
        Me.rdoAstImg.TabIndex = 0
        Me.rdoAstImg.TabStop = True
        Me.rdoAstImg.Text = "Asset Images"
        Me.rdoAstImg.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.White
        Me.GroupBox6.Controls.Add(Me.btnDir)
        Me.GroupBox6.Controls.Add(Me.txtImgStorgePath)
        Me.GroupBox6.Controls.Add(Me.rdoSharedFolder)
        Me.GroupBox6.Controls.Add(Me.rdoDatabase)
        Me.GroupBox6.Location = New System.Drawing.Point(22, 3)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(512, 105)
        Me.GroupBox6.TabIndex = 1
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Storage  Location"
        '
        'btnDir
        '
        Me.btnDir.Image = Global.ZulAssets.My.Resources.Icons.Open
        Me.btnDir.Location = New System.Drawing.Point(471, 59)
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
        Me.txtImgStorgePath.Size = New System.Drawing.Size(332, 19)
        Me.txtImgStorgePath.TabIndex = 2
        '
        'rdoSharedFolder
        '
        Me.rdoSharedFolder.AutoSize = True
        Me.rdoSharedFolder.Location = New System.Drawing.Point(17, 64)
        Me.rdoSharedFolder.Name = "rdoSharedFolder"
        Me.rdoSharedFolder.Size = New System.Drawing.Size(92, 17)
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
        'frmConfigWizard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(581, 409)
        Me.Controls.Add(Me.WizardControl1)
        Me.Name = "frmConfigWizard"
        Me.Text = "Configuration Wizard"
        CType(Me.WizardControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.WizardControl1.ResumeLayout(False)
        Me.WizardPage1.ResumeLayout(False)
        Me.WizardPage1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.PBLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPhone.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCountry.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtState.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddress.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.WizardPage2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txtMonthDay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.WizardPage3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.WizardPage4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.WizardPage5.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.WizardPage6.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.txtImgStorgePath.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WizardControl1 As DevExpress.XtraWizard.WizardControl
    Friend WithEvents WelcomeWizardPage1 As DevExpress.XtraWizard.WelcomeWizardPage
    Friend WithEvents WizardPage1 As DevExpress.XtraWizard.WizardPage
    Friend WithEvents CompletionWizardPage1 As DevExpress.XtraWizard.CompletionWizardPage
    Friend WithEvents txtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtAddress As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtFax As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCountry As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtState As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCity As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtPhone As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnDelImg As System.Windows.Forms.Button
    Friend WithEvents PBLogo As System.Windows.Forms.PictureBox
    Friend WithEvents WizardPage2 As DevExpress.XtraWizard.WizardPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtMonthDay As System.Windows.Forms.NumericUpDown
    Friend WithEvents RdAuto As System.Windows.Forms.RadioButton
    Friend WithEvents rdManual As System.Windows.Forms.RadioButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents WizardPage3 As DevExpress.XtraWizard.WizardPage
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoIncrementalCoding As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAssetsCoding As System.Windows.Forms.RadioButton
    Friend WithEvents WizardPage4 As DevExpress.XtraWizard.WizardPage
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbDateFormat As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents WizardPage5 As DevExpress.XtraWizard.WizardPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbRptLabel As System.Windows.Forms.ComboBox
    Friend WithEvents cmbRptPrint As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents WizardPage6 As DevExpress.XtraWizard.WizardPage
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents btnDir As System.Windows.Forms.Button
    Friend WithEvents txtImgStorgePath As DevExpress.XtraEditors.TextEdit
    Friend WithEvents rdoSharedFolder As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDatabase As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoItmImg As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAstImg As System.Windows.Forms.RadioButton
End Class
