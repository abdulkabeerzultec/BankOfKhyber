<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZulLOV
    Inherits System.Windows.Forms.UserControl

    'UserControl1 overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZulLOV))
        Me.btnLOV = New System.Windows.Forms.Button
        Me.txtLOV = New DevExpress.XtraEditors.TextEdit
        CType(Me.txtLOV.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnLOV
        '
        Me.btnLOV.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLOV.Image = CType(resources.GetObject("btnLOV.Image"), System.Drawing.Image)
        Me.btnLOV.Location = New System.Drawing.Point(172, 0)
        Me.btnLOV.Name = "btnLOV"
        Me.btnLOV.Size = New System.Drawing.Size(24, 24)
        Me.btnLOV.TabIndex = 1
        '
        'txtLOV
        '
        Me.txtLOV.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLOV.Location = New System.Drawing.Point(0, 1)
        Me.txtLOV.Name = "txtLOV"
        Me.txtLOV.Properties.ReadOnly = True
        Me.txtLOV.Size = New System.Drawing.Size(170, 19)
        Me.txtLOV.TabIndex = 0
        '
        'ZulLOV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.btnLOV)
        Me.Controls.Add(Me.txtLOV)
        Me.Name = "ZulLOV"
        Me.Size = New System.Drawing.Size(196, 23)
        CType(Me.txtLOV.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnLOV As System.Windows.Forms.Button
    Friend WithEvents txtLOV As DevExpress.XtraEditors.TextEdit

End Class
