<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSync
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
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl
        Me.PB = New DevExpress.XtraEditors.ProgressBarControl
        Me.btnSyncData = New DevExpress.XtraEditors.SimpleButton
        CType(Me.PB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblMessage
        '
        Me.lblMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblMessage.Location = New System.Drawing.Point(12, 91)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(717, 13)
        Me.lblMessage.TabIndex = 66
        Me.lblMessage.Text = "LabelControl1"
        '
        'PB
        '
        Me.PB.Location = New System.Drawing.Point(12, 12)
        Me.PB.Name = "PB"
        Me.PB.Properties.ShowTitle = True
        Me.PB.Size = New System.Drawing.Size(717, 50)
        Me.PB.TabIndex = 65
        '
        'btnSyncData
        '
        Me.btnSyncData.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSyncData.Appearance.Options.UseFont = True
        Me.btnSyncData.Location = New System.Drawing.Point(582, 115)
        Me.btnSyncData.Name = "btnSyncData"
        Me.btnSyncData.Size = New System.Drawing.Size(103, 23)
        Me.btnSyncData.TabIndex = 67
        Me.btnSyncData.Text = "Sync Data"
        '
        'frmNMCSync
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(757, 150)
        Me.Controls.Add(Me.btnSyncData)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.PB)
        Me.Name = "frmNMCSync"
        Me.Text = "Form1"
        CType(Me.PB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PB As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents btnSyncData As DevExpress.XtraEditors.SimpleButton

End Class
