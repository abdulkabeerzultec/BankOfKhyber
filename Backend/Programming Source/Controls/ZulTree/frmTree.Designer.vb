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
        Me.trv = New System.Windows.Forms.TreeView
        Me.SuspendLayout()
        '
        'trv
        '
        Me.trv.BackColor = System.Drawing.SystemColors.Window
        Me.trv.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trv.HideSelection = False
        Me.trv.Location = New System.Drawing.Point(0, 0)
        Me.trv.Name = "trv"
        Me.trv.Size = New System.Drawing.Size(270, 146)
        Me.trv.TabIndex = 61
        '
        'frmTree
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(270, 146)
        Me.ControlBox = False
        Me.Controls.Add(Me.trv)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MinimumSize = New System.Drawing.Size(100, 100)
        Me.Name = "frmTree"
        Me.ShowInTaskbar = False
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents trv As System.Windows.Forms.TreeView
End Class
