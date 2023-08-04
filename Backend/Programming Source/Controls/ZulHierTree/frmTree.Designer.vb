<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTree
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
        Me.trvHier = New System.Windows.Forms.TreeView
        Me.SuspendLayout()
        '
        'trvHier
        '
        Me.trvHier.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvHier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvHier.HideSelection = False
        Me.trvHier.Location = New System.Drawing.Point(0, 0)
        Me.trvHier.Name = "trvHier"
        Me.trvHier.Size = New System.Drawing.Size(263, 210)
        Me.trvHier.TabIndex = 0
        '
        'frmTree
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(263, 210)
        Me.ControlBox = False
        Me.Controls.Add(Me.trvHier)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MinimumSize = New System.Drawing.Size(100, 100)
        Me.Name = "frmTree"
        Me.ShowInTaskbar = False
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents trvHier As System.Windows.Forms.TreeView
End Class
