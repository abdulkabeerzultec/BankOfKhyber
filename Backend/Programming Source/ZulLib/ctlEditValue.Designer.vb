<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlEditValue
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
        Me.txtData = New DevExpress.XtraEditors.ButtonEdit
        CType(Me.txtData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtData
        '
        Me.txtData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtData.Location = New System.Drawing.Point(0, 0)
        Me.txtData.Name = "txtData"
        Me.txtData.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Edit Value", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.ZulLib.My.Resources.Resources.Editor_Edit, New DevExpress.Utils.KeyShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E)), "Click to edit the value of the Textbox")})
        Me.txtData.Size = New System.Drawing.Size(191, 19)
        Me.txtData.TabIndex = 0
        '
        'ctlEditValue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.txtData)
        Me.Name = "ctlEditValue"
        Me.Size = New System.Drawing.Size(191, 19)
        CType(Me.txtData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtData As DevExpress.XtraEditors.ButtonEdit

End Class
