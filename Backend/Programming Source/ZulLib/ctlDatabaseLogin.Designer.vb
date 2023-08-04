<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlDatabaseLogin
    Inherits System.Windows.Forms.UserControl

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
        Me.lblErrorMessage = New System.Windows.Forms.Label
        Me.txtSqlPort = New DevExpress.XtraEditors.TextEdit
        Me.txtDBPass = New DevExpress.XtraEditors.TextEdit
        Me.txtUserName = New DevExpress.XtraEditors.TextEdit
        Me.btnDefault = New DevExpress.XtraEditors.SimpleButton
        Me.cmbDatabase = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbDBServer = New System.Windows.Forms.ComboBox
        Me.txtCompanyName = New DevExpress.XtraEditors.TextEdit
        Me.Label1 = New System.Windows.Forms.Label
        Me.DxErrorProvider1 = New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(Me.components)
        Me.btnConnect = New DevExpress.XtraEditors.SimpleButton
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton
        Me.btnGetservers = New System.Windows.Forms.Button
        Me.btnGetDatabases = New System.Windows.Forms.Button
        CType(Me.txtSqlPort.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDBPass.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUserName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCompanyName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DxErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblErrorMessage
        '
        Me.lblErrorMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblErrorMessage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblErrorMessage.ForeColor = System.Drawing.Color.Red
        Me.lblErrorMessage.Location = New System.Drawing.Point(20, 132)
        Me.lblErrorMessage.Name = "lblErrorMessage"
        Me.lblErrorMessage.Size = New System.Drawing.Size(321, 59)
        Me.lblErrorMessage.TabIndex = 50
        Me.lblErrorMessage.Text = "Label1"
        Me.lblErrorMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblErrorMessage.Visible = False
        '
        'txtSqlPort
        '
        Me.txtSqlPort.Location = New System.Drawing.Point(135, 194)
        Me.txtSqlPort.Name = "txtSqlPort"
        Me.txtSqlPort.Size = New System.Drawing.Size(186, 19)
        Me.txtSqlPort.TabIndex = 7
        Me.txtSqlPort.Visible = False
        '
        'txtDBPass
        '
        Me.txtDBPass.Location = New System.Drawing.Point(124, 82)
        Me.txtDBPass.Name = "txtDBPass"
        Me.txtDBPass.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtDBPass.Size = New System.Drawing.Size(186, 19)
        Me.txtDBPass.TabIndex = 4
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(124, 57)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(186, 19)
        Me.txtUserName.TabIndex = 3
        '
        'btnDefault
        '
        Me.btnDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDefault.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDefault.Appearance.Options.UseFont = True
        Me.btnDefault.Location = New System.Drawing.Point(18, 217)
        Me.btnDefault.Name = "btnDefault"
        Me.btnDefault.Size = New System.Drawing.Size(107, 28)
        Me.btnDefault.TabIndex = 8
        Me.btnDefault.Text = "&Default"
        '
        'cmbDatabase
        '
        Me.cmbDatabase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbDatabase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbDatabase.FormattingEnabled = True
        Me.cmbDatabase.Location = New System.Drawing.Point(124, 110)
        Me.cmbDatabase.Name = "cmbDatabase"
        Me.cmbDatabase.Size = New System.Drawing.Size(186, 21)
        Me.cmbDatabase.TabIndex = 5
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(17, 113)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(83, 13)
        Me.Label13.TabIndex = 49
        Me.Label13.Text = "Database Name"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(28, 197)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 13)
        Me.Label7.TabIndex = 48
        Me.Label7.Text = "Sql Server Port"
        Me.Label7.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 33)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 13)
        Me.Label5.TabIndex = 47
        Me.Label5.Text = "Database Server"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 85)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = "Password"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "User Name"
        '
        'cmbDBServer
        '
        Me.cmbDBServer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbDBServer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbDBServer.FormattingEnabled = True
        Me.cmbDBServer.Location = New System.Drawing.Point(124, 30)
        Me.cmbDBServer.Name = "cmbDBServer"
        Me.cmbDBServer.Size = New System.Drawing.Size(186, 21)
        Me.cmbDBServer.TabIndex = 1
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Location = New System.Drawing.Point(124, 6)
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(186, 19)
        Me.txtCompanyName.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 52
        Me.Label1.Text = "Company Name"
        '
        'DxErrorProvider1
        '
        Me.DxErrorProvider1.ContainerControl = Me
        '
        'btnConnect
        '
        Me.btnConnect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConnect.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConnect.Appearance.Options.UseFont = True
        Me.btnConnect.Image = Global.ZulLib.My.Resources.Resources.check
        Me.btnConnect.Location = New System.Drawing.Point(150, 217)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(107, 28)
        Me.btnConnect.TabIndex = 9
        Me.btnConnect.Text = "C&onnect"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Image = Global.ZulLib.My.Resources.Resources.Close
        Me.btnCancel.Location = New System.Drawing.Point(278, 217)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(107, 28)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "&Close"
        '
        'btnGetservers
        '
        Me.btnGetservers.Image = Global.ZulLib.My.Resources.Resources.server_network
        Me.btnGetservers.Location = New System.Drawing.Point(316, 29)
        Me.btnGetservers.Name = "btnGetservers"
        Me.btnGetservers.Size = New System.Drawing.Size(25, 23)
        Me.btnGetservers.TabIndex = 2
        Me.btnGetservers.UseVisualStyleBackColor = True
        '
        'btnGetDatabases
        '
        Me.btnGetDatabases.Image = Global.ZulLib.My.Resources.Resources.DB
        Me.btnGetDatabases.Location = New System.Drawing.Point(316, 107)
        Me.btnGetDatabases.Name = "btnGetDatabases"
        Me.btnGetDatabases.Size = New System.Drawing.Size(25, 23)
        Me.btnGetDatabases.TabIndex = 6
        Me.btnGetDatabases.UseVisualStyleBackColor = True
        '
        'ctlDatabaseLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.txtCompanyName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblErrorMessage)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.txtSqlPort)
        Me.Controls.Add(Me.txtDBPass)
        Me.Controls.Add(Me.txtUserName)
        Me.Controls.Add(Me.btnDefault)
        Me.Controls.Add(Me.btnGetservers)
        Me.Controls.Add(Me.btnGetDatabases)
        Me.Controls.Add(Me.cmbDatabase)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbDBServer)
        Me.Name = "ctlDatabaseLogin"
        Me.Size = New System.Drawing.Size(395, 259)
        CType(Me.txtSqlPort.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDBPass.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUserName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCompanyName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DxErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblErrorMessage As System.Windows.Forms.Label
    Friend WithEvents btnConnect As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtSqlPort As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnDefault As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnGetservers As System.Windows.Forms.Button
    Friend WithEvents btnGetDatabases As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtDBPass As DevExpress.XtraEditors.TextEdit
    Public WithEvents txtUserName As DevExpress.XtraEditors.TextEdit
    Public WithEvents cmbDatabase As System.Windows.Forms.ComboBox
    Public WithEvents cmbDBServer As System.Windows.Forms.ComboBox
    Public WithEvents txtCompanyName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents DxErrorProvider1 As DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

End Class
