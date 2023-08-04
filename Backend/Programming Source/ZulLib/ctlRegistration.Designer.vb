<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlRegistration
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnCopy = New System.Windows.Forms.Button
        Me.lblNote = New System.Windows.Forms.Label
        Me.txtKey = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtSerial2 = New System.Windows.Forms.TextBox
        Me.txtSerial1 = New System.Windows.Forms.TextBox
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.txtComp = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnEmail = New System.Windows.Forms.Button
        Me.btnRegister = New System.Windows.Forms.Button
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.optFinancial = New System.Windows.Forms.RadioButton
        Me.optInventory = New System.Windows.Forms.RadioButton
        Me.optTracking = New System.Windows.Forms.RadioButton
        Me.optEnterprise = New System.Windows.Forms.RadioButton
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.btnCopy)
        Me.GroupBox1.Controls.Add(Me.txtKey)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtSerial2)
        Me.GroupBox1.Controls.Add(Me.txtSerial1)
        Me.GroupBox1.Controls.Add(Me.txtEmail)
        Me.GroupBox1.Controls.Add(Me.txtComp)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(509, 240)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "License Information"
        '
        'btnCopy
        '
        Me.btnCopy.Image = Global.ZulLib.My.Resources.Resources.Copy16x16
        Me.btnCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCopy.Location = New System.Drawing.Point(414, 84)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(76, 23)
        Me.btnCopy.TabIndex = 13
        Me.btnCopy.Text = "Copy"
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'lblNote
        '
        Me.lblNote.Location = New System.Drawing.Point(3, 243)
        Me.lblNote.Name = "lblNote"
        Me.lblNote.Size = New System.Drawing.Size(493, 89)
        Me.lblNote.TabIndex = 12
        Me.lblNote.Text = "Label6"
        '
        'txtKey
        '
        Me.txtKey.BackColor = System.Drawing.Color.White
        Me.txtKey.Location = New System.Drawing.Point(116, 115)
        Me.txtKey.Name = "txtKey"
        Me.txtKey.Size = New System.Drawing.Size(292, 20)
        Me.txtKey.TabIndex = 7
        Me.txtKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 119)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "License Key:"
        '
        'txtSerial2
        '
        Me.txtSerial2.BackColor = System.Drawing.Color.White
        Me.txtSerial2.Location = New System.Drawing.Point(212, 85)
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
        Me.txtSerial1.Location = New System.Drawing.Point(116, 85)
        Me.txtSerial1.Name = "txtSerial1"
        Me.txtSerial1.ReadOnly = True
        Me.txtSerial1.Size = New System.Drawing.Size(100, 20)
        Me.txtSerial1.TabIndex = 5
        Me.txtSerial1.TabStop = False
        Me.txtSerial1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        Me.Label4.Location = New System.Drawing.Point(16, 89)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Serial #:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Email:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Company Name:"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Image = Global.ZulLib.My.Resources.Resources.Close
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(390, 380)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 30)
        Me.btnCancel.TabIndex = 21
        Me.btnCancel.Text = "&Exit"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnEmail
        '
        Me.btnEmail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEmail.Image = Global.ZulLib.My.Resources.Resources.mail
        Me.btnEmail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEmail.Location = New System.Drawing.Point(266, 380)
        Me.btnEmail.Name = "btnEmail"
        Me.btnEmail.Size = New System.Drawing.Size(100, 30)
        Me.btnEmail.TabIndex = 20
        Me.btnEmail.Text = "Email &Serial"
        Me.btnEmail.UseVisualStyleBackColor = True
        '
        'btnRegister
        '
        Me.btnRegister.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRegister.Image = Global.ZulLib.My.Resources.Resources.check
        Me.btnRegister.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRegister.Location = New System.Drawing.Point(143, 380)
        Me.btnRegister.Name = "btnRegister"
        Me.btnRegister.Size = New System.Drawing.Size(100, 30)
        Me.btnRegister.TabIndex = 19
        Me.btnRegister.Text = "&Register"
        Me.btnRegister.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.optFinancial)
        Me.GroupBox4.Controls.Add(Me.optInventory)
        Me.GroupBox4.Controls.Add(Me.optTracking)
        Me.GroupBox4.Controls.Add(Me.optEnterprise)
        Me.GroupBox4.Location = New System.Drawing.Point(19, 156)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(389, 78)
        Me.GroupBox4.TabIndex = 22
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Application Edition"
        '
        'optFinancial
        '
        Me.optFinancial.AutoSize = True
        Me.optFinancial.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFinancial.Location = New System.Drawing.Point(141, 42)
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
        Me.optInventory.Location = New System.Drawing.Point(258, 42)
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
        Me.optTracking.Location = New System.Drawing.Point(258, 19)
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
        Me.optEnterprise.Location = New System.Drawing.Point(141, 19)
        Me.optEnterprise.Name = "optEnterprise"
        Me.optEnterprise.Size = New System.Drawing.Size(83, 17)
        Me.optEnterprise.TabIndex = 8
        Me.optEnterprise.TabStop = True
        Me.optEnterprise.Text = "Enterprise"
        Me.optEnterprise.UseVisualStyleBackColor = True
        '
        'ctlRegistration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblNote)
        Me.Controls.Add(Me.btnEmail)
        Me.Controls.Add(Me.btnRegister)
        Me.Name = "ctlRegistration"
        Me.Size = New System.Drawing.Size(509, 422)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtKey As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtSerial2 As System.Windows.Forms.TextBox
    Friend WithEvents txtSerial1 As System.Windows.Forms.TextBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtComp As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnEmail As System.Windows.Forms.Button
    Friend WithEvents btnRegister As System.Windows.Forms.Button
    Friend WithEvents lblNote As System.Windows.Forms.Label
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents optFinancial As System.Windows.Forms.RadioButton
    Friend WithEvents optInventory As System.Windows.Forms.RadioButton
    Friend WithEvents optTracking As System.Windows.Forms.RadioButton
    Friend WithEvents optEnterprise As System.Windows.Forms.RadioButton

End Class
