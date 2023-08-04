<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRegister
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRegister))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtKey = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtSerial2 = New System.Windows.Forms.TextBox
        Me.txtSerial1 = New System.Windows.Forms.TextBox
        Me.txtYID3 = New System.Windows.Forms.TextBox
        Me.txtYID2 = New System.Windows.Forms.TextBox
        Me.txtYID1 = New System.Windows.Forms.TextBox
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.txtComp = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.txtDBPath = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.optClient = New System.Windows.Forms.RadioButton
        Me.optServer = New System.Windows.Forms.RadioButton
        Me.btnRegister = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnEmail = New System.Windows.Forms.Button
        Me.lblNote = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.ofd1 = New System.Windows.Forms.OpenFileDialog
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.optFinancial = New System.Windows.Forms.RadioButton
        Me.optInventory = New System.Windows.Forms.RadioButton
        Me.optTracking = New System.Windows.Forms.RadioButton
        Me.optEnterprise = New System.Windows.Forms.RadioButton
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtKey)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtSerial2)
        Me.GroupBox1.Controls.Add(Me.txtSerial1)
        Me.GroupBox1.Controls.Add(Me.txtYID3)
        Me.GroupBox1.Controls.Add(Me.txtYID2)
        Me.GroupBox1.Controls.Add(Me.txtYID1)
        Me.GroupBox1.Controls.Add(Me.txtEmail)
        Me.GroupBox1.Controls.Add(Me.txtComp)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 32)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(448, 192)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "License Information"
        '
        'txtKey
        '
        Me.txtKey.BackColor = System.Drawing.Color.White
        Me.txtKey.Location = New System.Drawing.Point(116, 160)
        Me.txtKey.Name = "txtKey"
        Me.txtKey.Size = New System.Drawing.Size(292, 20)
        Me.txtKey.TabIndex = 7
        Me.txtKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 164)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "License Key:"
        '
        'txtSerial2
        '
        Me.txtSerial2.BackColor = System.Drawing.Color.White
        Me.txtSerial2.Location = New System.Drawing.Point(212, 110)
        Me.txtSerial2.Name = "txtSerial2"
        Me.txtSerial2.ReadOnly = True
        Me.txtSerial2.Size = New System.Drawing.Size(196, 20)
        Me.txtSerial2.TabIndex = 6
        Me.txtSerial2.TabStop = False
        Me.txtSerial2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSerial1
        '
        Me.txtSerial1.BackColor = System.Drawing.Color.White
        Me.txtSerial1.Location = New System.Drawing.Point(116, 110)
        Me.txtSerial1.Name = "txtSerial1"
        Me.txtSerial1.ReadOnly = True
        Me.txtSerial1.Size = New System.Drawing.Size(100, 20)
        Me.txtSerial1.TabIndex = 5
        Me.txtSerial1.TabStop = False
        Me.txtSerial1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtYID3
        '
        Me.txtYID3.BackColor = System.Drawing.Color.White
        Me.txtYID3.Location = New System.Drawing.Point(308, 84)
        Me.txtYID3.Name = "txtYID3"
        Me.txtYID3.ReadOnly = True
        Me.txtYID3.Size = New System.Drawing.Size(100, 20)
        Me.txtYID3.TabIndex = 4
        Me.txtYID3.TabStop = False
        Me.txtYID3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtYID2
        '
        Me.txtYID2.BackColor = System.Drawing.Color.White
        Me.txtYID2.Location = New System.Drawing.Point(212, 84)
        Me.txtYID2.Name = "txtYID2"
        Me.txtYID2.ReadOnly = True
        Me.txtYID2.Size = New System.Drawing.Size(100, 20)
        Me.txtYID2.TabIndex = 3
        Me.txtYID2.TabStop = False
        Me.txtYID2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtYID1
        '
        Me.txtYID1.BackColor = System.Drawing.Color.White
        Me.txtYID1.Location = New System.Drawing.Point(116, 84)
        Me.txtYID1.Name = "txtYID1"
        Me.txtYID1.ReadOnly = True
        Me.txtYID1.Size = New System.Drawing.Size(100, 20)
        Me.txtYID1.TabIndex = 2
        Me.txtYID1.TabStop = False
        Me.txtYID1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(116, 58)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(292, 20)
        Me.txtEmail.TabIndex = 1
        '
        'txtComp
        '
        Me.txtComp.Location = New System.Drawing.Point(116, 32)
        Me.txtComp.MaxLength = 50
        Me.txtComp.Name = "txtComp"
        Me.txtComp.Size = New System.Drawing.Size(292, 20)
        Me.txtComp.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 114)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Serial #:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Customer ID:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Email:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Company Name:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnBrowse)
        Me.GroupBox2.Controls.Add(Me.txtDBPath)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.optClient)
        Me.GroupBox2.Controls.Add(Me.optServer)
        Me.GroupBox2.Location = New System.Drawing.Point(472, 32)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(360, 192)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "License Type"
        '
        'btnBrowse
        '
        Me.btnBrowse.Enabled = False
        Me.btnBrowse.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(320, 95)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(32, 23)
        Me.btnBrowse.TabIndex = 11
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'txtDBPath
        '
        Me.txtDBPath.BackColor = System.Drawing.Color.White
        Me.txtDBPath.Enabled = False
        Me.txtDBPath.Location = New System.Drawing.Point(24, 96)
        Me.txtDBPath.Multiline = True
        Me.txtDBPath.Name = "txtDBPath"
        Me.txtDBPath.ReadOnly = True
        Me.txtDBPath.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDBPath.Size = New System.Drawing.Size(292, 80)
        Me.txtDBPath.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(24, 80)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(131, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Server Database Path" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'optClient
        '
        Me.optClient.AutoSize = True
        Me.optClient.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optClient.Location = New System.Drawing.Point(168, 40)
        Me.optClient.Name = "optClient"
        Me.optClient.Size = New System.Drawing.Size(57, 17)
        Me.optClient.TabIndex = 9
        Me.optClient.Text = "Client"
        Me.optClient.UseVisualStyleBackColor = True
        '
        'optServer
        '
        Me.optServer.AutoSize = True
        Me.optServer.Checked = True
        Me.optServer.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optServer.Location = New System.Drawing.Point(24, 40)
        Me.optServer.Name = "optServer"
        Me.optServer.Size = New System.Drawing.Size(63, 17)
        Me.optServer.TabIndex = 8
        Me.optServer.TabStop = True
        Me.optServer.Text = "Server"
        Me.optServer.UseVisualStyleBackColor = True
        '
        'btnRegister
        '
        Me.btnRegister.Location = New System.Drawing.Point(570, 383)
        Me.btnRegister.Name = "btnRegister"
        Me.btnRegister.Size = New System.Drawing.Size(75, 30)
        Me.btnRegister.TabIndex = 12
        Me.btnRegister.Text = "Register"
        Me.btnRegister.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(757, 383)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 30)
        Me.btnCancel.TabIndex = 14
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnEmail
        '
        Me.btnEmail.Location = New System.Drawing.Point(664, 383)
        Me.btnEmail.Name = "btnEmail"
        Me.btnEmail.Size = New System.Drawing.Size(75, 30)
        Me.btnEmail.TabIndex = 13
        Me.btnEmail.Text = "Send Serial"
        Me.btnEmail.UseVisualStyleBackColor = True
        '
        'lblNote
        '
        Me.lblNote.Location = New System.Drawing.Point(12, 18)
        Me.lblNote.Name = "lblNote"
        Me.lblNote.Size = New System.Drawing.Size(516, 158)
        Me.lblNote.TabIndex = 5
        Me.lblNote.Text = "Label6"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblNote)
        Me.GroupBox3.Location = New System.Drawing.Point(16, 232)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(548, 181)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        '
        'ofd1
        '
        Me.ofd1.FileName = "OpenFileDialog1"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.optFinancial)
        Me.GroupBox4.Controls.Add(Me.optInventory)
        Me.GroupBox4.Controls.Add(Me.optTracking)
        Me.GroupBox4.Controls.Add(Me.optEnterprise)
        Me.GroupBox4.Location = New System.Drawing.Point(570, 232)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(264, 138)
        Me.GroupBox4.TabIndex = 15
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Application Edition"
        '
        'optFinancial
        '
        Me.optFinancial.AutoSize = True
        Me.optFinancial.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFinancial.Location = New System.Drawing.Point(20, 49)
        Me.optFinancial.Name = "optFinancial"
        Me.optFinancial.Size = New System.Drawing.Size(74, 17)
        Me.optFinancial.TabIndex = 11
        Me.optFinancial.Text = "Financial"
        Me.optFinancial.UseVisualStyleBackColor = True
        '
        'optInventory
        '
        Me.optInventory.AutoSize = True
        Me.optInventory.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optInventory.Location = New System.Drawing.Point(20, 95)
        Me.optInventory.Name = "optInventory"
        Me.optInventory.Size = New System.Drawing.Size(82, 17)
        Me.optInventory.TabIndex = 10
        Me.optInventory.Text = "Inventory"
        Me.optInventory.UseVisualStyleBackColor = True
        Me.optInventory.Visible = False
        '
        'optTracking
        '
        Me.optTracking.AutoSize = True
        Me.optTracking.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTracking.Location = New System.Drawing.Point(20, 72)
        Me.optTracking.Name = "optTracking"
        Me.optTracking.Size = New System.Drawing.Size(74, 17)
        Me.optTracking.TabIndex = 9
        Me.optTracking.Text = "Tracking"
        Me.optTracking.UseVisualStyleBackColor = True
        '
        'optEnterprise
        '
        Me.optEnterprise.AutoSize = True
        Me.optEnterprise.Checked = True
        Me.optEnterprise.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optEnterprise.Location = New System.Drawing.Point(20, 26)
        Me.optEnterprise.Name = "optEnterprise"
        Me.optEnterprise.Size = New System.Drawing.Size(83, 17)
        Me.optEnterprise.TabIndex = 8
        Me.optEnterprise.TabStop = True
        Me.optEnterprise.Text = "Enterprise"
        Me.optEnterprise.UseVisualStyleBackColor = True
        '
        'frmRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(841, 421)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnEmail)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnRegister)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRegister"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Registration"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSerial2 As System.Windows.Forms.TextBox
    Friend WithEvents txtSerial1 As System.Windows.Forms.TextBox
    Friend WithEvents txtYID3 As System.Windows.Forms.TextBox
    Friend WithEvents txtYID2 As System.Windows.Forms.TextBox
    Friend WithEvents txtYID1 As System.Windows.Forms.TextBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtComp As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents optClient As System.Windows.Forms.RadioButton
    Friend WithEvents optServer As System.Windows.Forms.RadioButton
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents txtDBPath As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnRegister As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnEmail As System.Windows.Forms.Button
    Friend WithEvents lblNote As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtKey As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ofd1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents optFinancial As System.Windows.Forms.RadioButton
    Friend WithEvents optInventory As System.Windows.Forms.RadioButton
    Friend WithEvents optTracking As System.Windows.Forms.RadioButton
    Friend WithEvents optEnterprise As System.Windows.Forms.RadioButton
End Class
