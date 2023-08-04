<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZulTree
    Inherits System.Windows.Forms.UserControl

    'UserControl1 overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZulTree))
        Me.txt = New DevExpress.XtraEditors.TextEdit
        Me.btnTree = New System.Windows.Forms.Button
        CType(Me.txt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txt
        '
        Me.txt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt.Location = New System.Drawing.Point(0, 1)
        Me.txt.Name = "txt"
        Me.txt.Properties.ReadOnly = True
        Me.txt.Size = New System.Drawing.Size(170, 19)
        Me.txt.TabIndex = 0
        '
        'btnTree
        '
        Me.btnTree.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTree.Image = CType(resources.GetObject("btnTree.Image"), System.Drawing.Image)
        Me.btnTree.Location = New System.Drawing.Point(172, 0)
        Me.btnTree.Name = "btnTree"
        Me.btnTree.Size = New System.Drawing.Size(24, 24)
        Me.btnTree.TabIndex = 1
        '
        'ZulTree
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.txt)
        Me.Controls.Add(Me.btnTree)
        Me.Name = "ZulTree"
        Me.Size = New System.Drawing.Size(196, 23)
        CType(Me.txt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnTree As System.Windows.Forms.Button
    Friend WithEvents txt As DevExpress.XtraEditors.TextEdit
End Class
