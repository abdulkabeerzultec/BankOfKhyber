<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSplash
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
        Me.lblVersion = New System.Windows.Forms.Label
        Me.lblLicensedTo = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblEdition = New System.Windows.Forms.Label
        Me.lblIntegration = New System.Windows.Forms.Label
        Me.lblDbVersion = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblVersion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.ForeColor = System.Drawing.Color.Black
        Me.lblVersion.Location = New System.Drawing.Point(13, 12)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(62, 13)
        Me.lblVersion.TabIndex = 0
        Me.lblVersion.Text = "lblVersion"
        '
        'lblLicensedTo
        '
        Me.lblLicensedTo.AutoSize = True
        Me.lblLicensedTo.BackColor = System.Drawing.Color.Transparent
        Me.lblLicensedTo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLicensedTo.ForeColor = System.Drawing.Color.Black
        Me.lblLicensedTo.Location = New System.Drawing.Point(13, 40)
        Me.lblLicensedTo.Name = "lblLicensedTo"
        Me.lblLicensedTo.Size = New System.Drawing.Size(56, 13)
        Me.lblLicensedTo.TabIndex = 1
        Me.lblLicensedTo.Text = "Licensed"
        '
        'Timer1
        '
        Me.Timer1.Interval = 800
        Me.Timer1.Tag = "1"
        '
        'lblEdition
        '
        Me.lblEdition.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblEdition.BackColor = System.Drawing.Color.Transparent
        Me.lblEdition.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEdition.ForeColor = System.Drawing.Color.Red
        Me.lblEdition.Location = New System.Drawing.Point(155, 4)
        Me.lblEdition.Name = "lblEdition"
        Me.lblEdition.Size = New System.Drawing.Size(214, 21)
        Me.lblEdition.TabIndex = 2
        Me.lblEdition.Text = "Licensed"
        Me.lblEdition.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblIntegration
        '
        Me.lblIntegration.AutoSize = True
        Me.lblIntegration.BackColor = System.Drawing.Color.Transparent
        Me.lblIntegration.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIntegration.ForeColor = System.Drawing.Color.Black
        Me.lblIntegration.Location = New System.Drawing.Point(13, 96)
        Me.lblIntegration.Name = "lblIntegration"
        Me.lblIntegration.Size = New System.Drawing.Size(107, 13)
        Me.lblIntegration.TabIndex = 3
        Me.lblIntegration.Text = "Integration Name"
        '
        'lblDbVersion
        '
        Me.lblDbVersion.AutoSize = True
        Me.lblDbVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblDbVersion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDbVersion.ForeColor = System.Drawing.Color.Black
        Me.lblDbVersion.Location = New System.Drawing.Point(13, 68)
        Me.lblDbVersion.Name = "lblDbVersion"
        Me.lblDbVersion.Size = New System.Drawing.Size(106, 13)
        Me.lblDbVersion.TabIndex = 4
        Me.lblDbVersion.Text = "Database Version"
        '
        'frmSplash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.ZulAssets.My.Resources.Resources.splash
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(524, 346)
        Me.Controls.Add(Me.lblDbVersion)
        Me.Controls.Add(Me.lblIntegration)
        Me.Controls.Add(Me.lblEdition)
        Me.Controls.Add(Me.lblLicensedTo)
        Me.Controls.Add(Me.lblVersion)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmSplash"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ZulAssets"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblLicensedTo As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblEdition As System.Windows.Forms.Label
    Friend WithEvents lblIntegration As System.Windows.Forms.Label
    Friend WithEvents lblDbVersion As System.Windows.Forms.Label
End Class
