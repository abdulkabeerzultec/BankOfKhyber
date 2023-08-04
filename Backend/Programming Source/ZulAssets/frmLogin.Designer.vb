<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogin
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
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.txtPass = New System.Windows.Forms.TextBox
        Me.txtAppUname = New System.Windows.Forms.TextBox
        Me.chkShowServerConfig = New System.Windows.Forms.CheckBox
        Me.txtPass1 = New DevExpress.XtraEditors.TextEdit
        Me.txtAppUname1 = New DevExpress.XtraEditors.TextEdit
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.btnDefault = New System.Windows.Forms.Button
        Me.btnGetservers = New System.Windows.Forms.Button
        Me.btnGetDatabases = New System.Windows.Forms.Button
        Me.cmbDatabase = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.cmbdbType = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtSqlPort = New DevExpress.XtraEditors.TextEdit
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtSysPass = New DevExpress.XtraEditors.TextEdit
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtDBPass = New DevExpress.XtraEditors.TextEdit
        Me.txtDBUname = New DevExpress.XtraEditors.TextEdit
        Me.cmbDBServer = New DevExpress.XtraEditors.ComboBoxEdit
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.btnGetComDatabase = New System.Windows.Forms.Button
        Me.btngetComServers = New System.Windows.Forms.Button
        Me.cmbCommServ = New DevExpress.XtraEditors.ComboBoxEdit
        Me.cmbComDB = New System.Windows.Forms.ComboBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtComPass = New DevExpress.XtraEditors.TextEdit
        Me.txtComUname = New DevExpress.XtraEditors.TextEdit
        Me.chkSame = New System.Windows.Forms.CheckBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtComSerPort = New DevExpress.XtraEditors.TextEdit
        Me.Label14 = New System.Windows.Forms.Label
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.cmbExpServ = New DevExpress.XtraEditors.ComboBoxEdit
        Me.btnGetExportDatabases = New System.Windows.Forms.Button
        Me.btnGetExportServers = New System.Windows.Forms.Button
        Me.cmbExpDB = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtExpPass = New DevExpress.XtraEditors.TextEdit
        Me.txtExpUname = New DevExpress.XtraEditors.TextEdit
        Me.chkExpSame = New System.Windows.Forms.CheckBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtExpSerPort = New DevExpress.XtraEditors.TextEdit
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtExportServerConnect = New DevExpress.XtraEditors.TextEdit
        Me.txtComServerConnect = New DevExpress.XtraEditors.TextEdit
        Me.txtDBConnect = New DevExpress.XtraEditors.TextEdit
        Me.MainLookAndFeel = New DevExpress.LookAndFeel.DefaultLookAndFeel(Me.components)
        Me.btnLogin = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkDefaultPass = New System.Windows.Forms.CheckBox
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.txtPass1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAppUname1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.txtSqlPort.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSysPass.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDBPass.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDBUname.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbDBServer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.cmbCommServ.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComPass.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComUname.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComSerPort.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.cmbExpServ.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExpPass.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExpUname.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExpSerPort.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExportServerConnect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComServerConnect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDBConnect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(34, 45)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(388, 251)
        Me.TabControl1.TabIndex = 0
        Me.TabControl1.TabStop = False
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.White
        Me.TabPage1.Controls.Add(Me.txtPass)
        Me.TabPage1.Controls.Add(Me.txtAppUname)
        Me.TabPage1.Controls.Add(Me.chkShowServerConfig)
        Me.TabPage1.Controls.Add(Me.txtPass1)
        Me.TabPage1.Controls.Add(Me.txtAppUname1)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(380, 225)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Application Login "
        '
        'txtPass
        '
        Me.txtPass.Location = New System.Drawing.Point(151, 98)
        Me.txtPass.Name = "txtPass"
        Me.txtPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPass.Size = New System.Drawing.Size(139, 20)
        Me.txtPass.TabIndex = 1
        '
        'txtAppUname
        '
        Me.txtAppUname.Location = New System.Drawing.Point(151, 67)
        Me.txtAppUname.Name = "txtAppUname"
        Me.txtAppUname.Size = New System.Drawing.Size(139, 20)
        Me.txtAppUname.TabIndex = 0
        '
        'chkShowServerConfig
        '
        Me.chkShowServerConfig.AutoSize = True
        Me.chkShowServerConfig.Location = New System.Drawing.Point(151, 133)
        Me.chkShowServerConfig.Name = "chkShowServerConfig"
        Me.chkShowServerConfig.Size = New System.Drawing.Size(155, 17)
        Me.chkShowServerConfig.TabIndex = 2
        Me.chkShowServerConfig.Text = "Show Server Configuration"
        Me.chkShowServerConfig.UseVisualStyleBackColor = True
        '
        'txtPass1
        '
        Me.txtPass1.EditValue = ""
        Me.txtPass1.Location = New System.Drawing.Point(151, 187)
        Me.txtPass1.Name = "txtPass1"
        Me.txtPass1.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPass1.Size = New System.Drawing.Size(139, 19)
        Me.txtPass1.TabIndex = 1
        Me.txtPass1.Visible = False
        '
        'txtAppUname1
        '
        Me.txtAppUname1.EditValue = ""
        Me.txtAppUname1.Location = New System.Drawing.Point(151, 156)
        Me.txtAppUname1.Name = "txtAppUname1"
        Me.txtAppUname1.Size = New System.Drawing.Size(139, 19)
        Me.txtAppUname1.TabIndex = 0
        Me.txtAppUname1.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(82, 101)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Password"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(82, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "User Name"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.White
        Me.TabPage2.Controls.Add(Me.btnDefault)
        Me.TabPage2.Controls.Add(Me.btnGetservers)
        Me.TabPage2.Controls.Add(Me.btnGetDatabases)
        Me.TabPage2.Controls.Add(Me.cmbDatabase)
        Me.TabPage2.Controls.Add(Me.Label13)
        Me.TabPage2.Controls.Add(Me.cmbdbType)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.txtSqlPort)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.txtSysPass)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.txtDBPass)
        Me.TabPage2.Controls.Add(Me.txtDBUname)
        Me.TabPage2.Controls.Add(Me.cmbDBServer)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(380, 225)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Database Login"
        '
        'btnDefault
        '
        Me.btnDefault.Location = New System.Drawing.Point(190, 198)
        Me.btnDefault.Name = "btnDefault"
        Me.btnDefault.Size = New System.Drawing.Size(73, 23)
        Me.btnDefault.TabIndex = 18
        Me.btnDefault.Text = "Default"
        Me.btnDefault.UseVisualStyleBackColor = True
        '
        'btnGetservers
        '
        Me.btnGetservers.Image = Global.ZulAssets.My.Resources.Icons.server
        Me.btnGetservers.Location = New System.Drawing.Point(341, 94)
        Me.btnGetservers.Name = "btnGetservers"
        Me.btnGetservers.Size = New System.Drawing.Size(25, 23)
        Me.btnGetservers.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.btnGetservers, "Get servers list")
        Me.btnGetservers.UseVisualStyleBackColor = True
        '
        'btnGetDatabases
        '
        Me.btnGetDatabases.Image = Global.ZulAssets.My.Resources.Icons.DB
        Me.btnGetDatabases.Location = New System.Drawing.Point(341, 119)
        Me.btnGetDatabases.Name = "btnGetDatabases"
        Me.btnGetDatabases.Size = New System.Drawing.Size(25, 23)
        Me.btnGetDatabases.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.btnGetDatabases, "Connect and get databases list")
        Me.btnGetDatabases.UseVisualStyleBackColor = True
        '
        'cmbDatabase
        '
        Me.cmbDatabase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbDatabase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbDatabase.FormattingEnabled = True
        Me.cmbDatabase.Location = New System.Drawing.Point(151, 120)
        Me.cmbDatabase.Name = "cmbDatabase"
        Me.cmbDatabase.Size = New System.Drawing.Size(186, 21)
        Me.cmbDatabase.TabIndex = 4
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(20, 125)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(83, 13)
        Me.Label13.TabIndex = 14
        Me.Label13.Text = "Database Name"
        '
        'cmbdbType
        '
        Me.cmbdbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbdbType.FormattingEnabled = True
        Me.cmbdbType.Items.AddRange(New Object() {"MS SQL Server", "Oracle "})
        Me.cmbdbType.Location = New System.Drawing.Point(151, 15)
        Me.cmbdbType.Name = "cmbdbType"
        Me.cmbdbType.Size = New System.Drawing.Size(186, 21)
        Me.cmbdbType.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(20, 19)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 13)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "Database Type"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(20, 177)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Sql Server Port"
        Me.Label7.Visible = False
        '
        'txtSqlPort
        '
        Me.txtSqlPort.Location = New System.Drawing.Point(151, 173)
        Me.txtSqlPort.Name = "txtSqlPort"
        Me.txtSqlPort.Size = New System.Drawing.Size(186, 19)
        Me.txtSqlPort.TabIndex = 6
        Me.txtSqlPort.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(20, 151)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(119, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "System Login Password"
        '
        'txtSysPass
        '
        Me.txtSysPass.Location = New System.Drawing.Point(151, 147)
        Me.txtSysPass.Name = "txtSysPass"
        Me.txtSysPass.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSysPass.Size = New System.Drawing.Size(186, 19)
        Me.txtSysPass.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(20, 98)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Database Server"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Password"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "User Name"
        '
        'txtDBPass
        '
        Me.txtDBPass.Location = New System.Drawing.Point(151, 68)
        Me.txtDBPass.Name = "txtDBPass"
        Me.txtDBPass.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtDBPass.Size = New System.Drawing.Size(186, 19)
        Me.txtDBPass.TabIndex = 2
        '
        'txtDBUname
        '
        Me.txtDBUname.Location = New System.Drawing.Point(151, 42)
        Me.txtDBUname.Name = "txtDBUname"
        Me.txtDBUname.Size = New System.Drawing.Size(186, 19)
        Me.txtDBUname.TabIndex = 1
        '
        'cmbDBServer
        '
        Me.cmbDBServer.Location = New System.Drawing.Point(151, 95)
        Me.cmbDBServer.Name = "cmbDBServer"
        Me.cmbDBServer.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbDBServer.Size = New System.Drawing.Size(186, 19)
        Me.cmbDBServer.TabIndex = 16
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.White
        Me.TabPage3.Controls.Add(Me.btnGetComDatabase)
        Me.TabPage3.Controls.Add(Me.btngetComServers)
        Me.TabPage3.Controls.Add(Me.cmbCommServ)
        Me.TabPage3.Controls.Add(Me.cmbComDB)
        Me.TabPage3.Controls.Add(Me.Label16)
        Me.TabPage3.Controls.Add(Me.Label11)
        Me.TabPage3.Controls.Add(Me.Label12)
        Me.TabPage3.Controls.Add(Me.txtComPass)
        Me.TabPage3.Controls.Add(Me.txtComUname)
        Me.TabPage3.Controls.Add(Me.chkSame)
        Me.TabPage3.Controls.Add(Me.Label15)
        Me.TabPage3.Controls.Add(Me.txtComSerPort)
        Me.TabPage3.Controls.Add(Me.Label14)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(380, 225)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Communication Server"
        '
        'btnGetComDatabase
        '
        Me.btnGetComDatabase.Image = Global.ZulAssets.My.Resources.Icons.DB
        Me.btnGetComDatabase.Location = New System.Drawing.Point(347, 94)
        Me.btnGetComDatabase.Name = "btnGetComDatabase"
        Me.btnGetComDatabase.Size = New System.Drawing.Size(25, 23)
        Me.btnGetComDatabase.TabIndex = 26
        Me.ToolTip1.SetToolTip(Me.btnGetComDatabase, "Connect and get databases list")
        Me.btnGetComDatabase.UseVisualStyleBackColor = True
        '
        'btngetComServers
        '
        Me.btngetComServers.Image = Global.ZulAssets.My.Resources.Icons.server
        Me.btngetComServers.Location = New System.Drawing.Point(347, 69)
        Me.btngetComServers.Name = "btngetComServers"
        Me.btngetComServers.Size = New System.Drawing.Size(25, 23)
        Me.btngetComServers.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.btngetComServers, "Get servers list")
        Me.btngetComServers.UseVisualStyleBackColor = True
        '
        'cmbCommServ
        '
        Me.cmbCommServ.Location = New System.Drawing.Point(152, 70)
        Me.cmbCommServ.Name = "cmbCommServ"
        Me.cmbCommServ.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCommServ.Size = New System.Drawing.Size(189, 19)
        Me.cmbCommServ.TabIndex = 24
        '
        'cmbComDB
        '
        Me.cmbComDB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbComDB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbComDB.FormattingEnabled = True
        Me.cmbComDB.Location = New System.Drawing.Point(152, 96)
        Me.cmbComDB.Name = "cmbComDB"
        Me.cmbComDB.Size = New System.Drawing.Size(189, 21)
        Me.cmbComDB.TabIndex = 3
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(25, 99)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(83, 13)
        Me.Label16.TabIndex = 23
        Me.Label16.Text = "Database Name"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(25, 48)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 13)
        Me.Label11.TabIndex = 19
        Me.Label11.Text = "Password"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(25, 23)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(59, 13)
        Me.Label12.TabIndex = 18
        Me.Label12.Text = "User Name"
        '
        'txtComPass
        '
        Me.txtComPass.Location = New System.Drawing.Point(152, 44)
        Me.txtComPass.Name = "txtComPass"
        Me.txtComPass.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtComPass.Size = New System.Drawing.Size(189, 19)
        Me.txtComPass.TabIndex = 1
        '
        'txtComUname
        '
        Me.txtComUname.Location = New System.Drawing.Point(152, 18)
        Me.txtComUname.Name = "txtComUname"
        Me.txtComUname.Size = New System.Drawing.Size(189, 19)
        Me.txtComUname.TabIndex = 0
        '
        'chkSame
        '
        Me.chkSame.AutoSize = True
        Me.chkSame.Location = New System.Drawing.Point(28, 161)
        Me.chkSame.Name = "chkSame"
        Me.chkSame.Size = New System.Drawing.Size(163, 17)
        Me.chkSame.TabIndex = 17
        Me.chkSame.Text = "Use same as Database Login"
        Me.chkSame.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(25, 128)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(62, 13)
        Me.Label15.TabIndex = 15
        Me.Label15.Text = "Server Port"
        '
        'txtComSerPort
        '
        Me.txtComSerPort.Location = New System.Drawing.Point(152, 124)
        Me.txtComSerPort.Name = "txtComSerPort"
        Me.txtComSerPort.Size = New System.Drawing.Size(189, 19)
        Me.txtComSerPort.TabIndex = 4
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(25, 74)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(114, 13)
        Me.Label14.TabIndex = 11
        Me.Label14.Text = "Communication Server"
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.Color.White
        Me.TabPage4.Controls.Add(Me.cmbExpServ)
        Me.TabPage4.Controls.Add(Me.btnGetExportDatabases)
        Me.TabPage4.Controls.Add(Me.btnGetExportServers)
        Me.TabPage4.Controls.Add(Me.cmbExpDB)
        Me.TabPage4.Controls.Add(Me.Label8)
        Me.TabPage4.Controls.Add(Me.Label9)
        Me.TabPage4.Controls.Add(Me.Label17)
        Me.TabPage4.Controls.Add(Me.txtExpPass)
        Me.TabPage4.Controls.Add(Me.txtExpUname)
        Me.TabPage4.Controls.Add(Me.chkExpSame)
        Me.TabPage4.Controls.Add(Me.Label18)
        Me.TabPage4.Controls.Add(Me.txtExpSerPort)
        Me.TabPage4.Controls.Add(Me.Label19)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TabPage4.Size = New System.Drawing.Size(380, 225)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Export Server"
        '
        'cmbExpServ
        '
        Me.cmbExpServ.Location = New System.Drawing.Point(152, 69)
        Me.cmbExpServ.Name = "cmbExpServ"
        Me.cmbExpServ.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbExpServ.Size = New System.Drawing.Size(189, 19)
        Me.cmbExpServ.TabIndex = 37
        '
        'btnGetExportDatabases
        '
        Me.btnGetExportDatabases.Image = Global.ZulAssets.My.Resources.Icons.DB
        Me.btnGetExportDatabases.Location = New System.Drawing.Point(347, 94)
        Me.btnGetExportDatabases.Name = "btnGetExportDatabases"
        Me.btnGetExportDatabases.Size = New System.Drawing.Size(25, 23)
        Me.btnGetExportDatabases.TabIndex = 36
        Me.ToolTip1.SetToolTip(Me.btnGetExportDatabases, "Connect and get databases list")
        Me.btnGetExportDatabases.UseVisualStyleBackColor = True
        '
        'btnGetExportServers
        '
        Me.btnGetExportServers.Image = Global.ZulAssets.My.Resources.Icons.server
        Me.btnGetExportServers.Location = New System.Drawing.Point(347, 69)
        Me.btnGetExportServers.Name = "btnGetExportServers"
        Me.btnGetExportServers.Size = New System.Drawing.Size(25, 23)
        Me.btnGetExportServers.TabIndex = 35
        Me.ToolTip1.SetToolTip(Me.btnGetExportServers, "Get servers list")
        Me.btnGetExportServers.UseVisualStyleBackColor = True
        '
        'cmbExpDB
        '
        Me.cmbExpDB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbExpDB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbExpDB.FormattingEnabled = True
        Me.cmbExpDB.Location = New System.Drawing.Point(152, 96)
        Me.cmbExpDB.Name = "cmbExpDB"
        Me.cmbExpDB.Size = New System.Drawing.Size(189, 21)
        Me.cmbExpDB.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(25, 104)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(83, 13)
        Me.Label8.TabIndex = 34
        Me.Label8.Text = "Database Name"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(25, 48)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 13)
        Me.Label9.TabIndex = 31
        Me.Label9.Text = "Password"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(25, 23)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(59, 13)
        Me.Label17.TabIndex = 30
        Me.Label17.Text = "User Name"
        '
        'txtExpPass
        '
        Me.txtExpPass.Location = New System.Drawing.Point(152, 44)
        Me.txtExpPass.Name = "txtExpPass"
        Me.txtExpPass.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtExpPass.Size = New System.Drawing.Size(189, 19)
        Me.txtExpPass.TabIndex = 1
        '
        'txtExpUname
        '
        Me.txtExpUname.Location = New System.Drawing.Point(152, 18)
        Me.txtExpUname.Name = "txtExpUname"
        Me.txtExpUname.Size = New System.Drawing.Size(189, 19)
        Me.txtExpUname.TabIndex = 0
        '
        'chkExpSame
        '
        Me.chkExpSame.AutoSize = True
        Me.chkExpSame.Location = New System.Drawing.Point(28, 161)
        Me.chkExpSame.Name = "chkExpSame"
        Me.chkExpSame.Size = New System.Drawing.Size(163, 17)
        Me.chkExpSame.TabIndex = 5
        Me.chkExpSame.Text = "Use same as Database Login"
        Me.chkExpSame.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(25, 128)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(62, 13)
        Me.Label18.TabIndex = 27
        Me.Label18.Text = "Server Port"
        '
        'txtExpSerPort
        '
        Me.txtExpSerPort.Location = New System.Drawing.Point(152, 124)
        Me.txtExpSerPort.Name = "txtExpSerPort"
        Me.txtExpSerPort.Size = New System.Drawing.Size(189, 19)
        Me.txtExpSerPort.TabIndex = 4
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(25, 74)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(114, 13)
        Me.Label19.TabIndex = 25
        Me.Label19.Text = "Communication Server"
        '
        'txtExportServerConnect
        '
        Me.txtExportServerConnect.CausesValidation = False
        Me.txtExportServerConnect.EditValue = "Export Server"
        Me.txtExportServerConnect.Location = New System.Drawing.Point(265, 13)
        Me.txtExportServerConnect.Name = "txtExportServerConnect"
        Me.txtExportServerConnect.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.txtExportServerConnect.Properties.Appearance.Options.UseBackColor = True
        Me.txtExportServerConnect.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.txtExportServerConnect.Properties.ReadOnly = True
        Me.txtExportServerConnect.Size = New System.Drawing.Size(116, 17)
        Me.txtExportServerConnect.TabIndex = 5
        Me.txtExportServerConnect.Visible = False
        '
        'txtComServerConnect
        '
        Me.txtComServerConnect.CausesValidation = False
        Me.txtComServerConnect.EditValue = "Communication Server"
        Me.txtComServerConnect.Location = New System.Drawing.Point(131, 13)
        Me.txtComServerConnect.Name = "txtComServerConnect"
        Me.txtComServerConnect.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.txtComServerConnect.Properties.Appearance.Options.UseBackColor = True
        Me.txtComServerConnect.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.txtComServerConnect.Properties.ReadOnly = True
        Me.txtComServerConnect.Size = New System.Drawing.Size(119, 17)
        Me.txtComServerConnect.TabIndex = 4
        Me.txtComServerConnect.Visible = False
        '
        'txtDBConnect
        '
        Me.txtDBConnect.CausesValidation = False
        Me.txtDBConnect.EditValue = "ZulAssets Database ..."
        Me.txtDBConnect.Location = New System.Drawing.Point(4, 13)
        Me.txtDBConnect.Name = "txtDBConnect"
        Me.txtDBConnect.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.txtDBConnect.Properties.Appearance.Options.UseBackColor = True
        Me.txtDBConnect.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.txtDBConnect.Properties.ReadOnly = True
        Me.txtDBConnect.Size = New System.Drawing.Size(130, 17)
        Me.txtDBConnect.TabIndex = 3
        Me.txtDBConnect.Visible = False
        '
        'MainLookAndFeel
        '
        Me.MainLookAndFeel.LookAndFeel.SkinName = "Money Twins"
        '
        'btnLogin
        '
        Me.btnLogin.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLogin.Image = Global.ZulAssets.My.Resources.Icons.keys
        Me.btnLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLogin.Location = New System.Drawing.Point(240, 315)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(84, 27)
        Me.btnLogin.TabIndex = 1
        Me.btnLogin.Text = "Login"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Image = Global.ZulAssets.My.Resources.Icons.Close
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(334, 315)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(84, 27)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.txtComServerConnect)
        Me.Panel1.Controls.Add(Me.txtDBConnect)
        Me.Panel1.Controls.Add(Me.txtExportServerConnect)
        Me.Panel1.Location = New System.Drawing.Point(38, 292)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(384, 33)
        Me.Panel1.TabIndex = 3
        Me.Panel1.Visible = False
        '
        'chkDefaultPass
        '
        Me.chkDefaultPass.AutoSize = True
        Me.chkDefaultPass.Location = New System.Drawing.Point(237, 15)
        Me.chkDefaultPass.Name = "chkDefaultPass"
        Me.chkDefaultPass.Size = New System.Drawing.Size(86, 17)
        Me.chkDefaultPass.TabIndex = 4
        Me.chkDefaultPass.Text = "Default Pass"
        Me.chkDefaultPass.UseVisualStyleBackColor = True
        Me.chkDefaultPass.Visible = False
        '
        'frmLogin
        '
        Me.AcceptButton = Me.btnLogin
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(458, 349)
        Me.Controls.Add(Me.chkDefaultPass)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Zulassets Login"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.txtPass1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAppUname1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.txtSqlPort.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSysPass.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDBPass.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDBUname.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbDBServer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.cmbCommServ.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComPass.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComUname.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComSerPort.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        CType(Me.cmbExpServ.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExpPass.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExpUname.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExpSerPort.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExportServerConnect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComServerConnect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDBConnect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents txtPass1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtAppUname1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDBPass As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDBUname As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtSysPass As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtSqlPort As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbdbType As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtComSerPort As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents chkSame As System.Windows.Forms.CheckBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtComPass As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtComUname As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents cmbComDB As System.Windows.Forms.ComboBox
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtExpPass As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtExpUname As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkExpSame As System.Windows.Forms.CheckBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtExpSerPort As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cmbExpDB As System.Windows.Forms.ComboBox
    Friend WithEvents chkShowServerConfig As System.Windows.Forms.CheckBox
    Friend WithEvents MainLookAndFeel As DevExpress.LookAndFeel.DefaultLookAndFeel
    Friend WithEvents txtExportServerConnect As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtComServerConnect As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDBConnect As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbDatabase As System.Windows.Forms.ComboBox
    Friend WithEvents btnGetDatabases As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnGetservers As System.Windows.Forms.Button
    Friend WithEvents cmbDBServer As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btngetComServers As System.Windows.Forms.Button
    Friend WithEvents cmbCommServ As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnGetComDatabase As System.Windows.Forms.Button
    Friend WithEvents btnGetExportDatabases As System.Windows.Forms.Button
    Friend WithEvents btnGetExportServers As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmbExpServ As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnDefault As System.Windows.Forms.Button
    Friend WithEvents chkDefaultPass As System.Windows.Forms.CheckBox
    Friend WithEvents txtPass As System.Windows.Forms.TextBox
    Friend WithEvents txtAppUname As System.Windows.Forms.TextBox
End Class
