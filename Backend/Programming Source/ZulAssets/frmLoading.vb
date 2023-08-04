Public Class frmLoading
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        '        Control.CheckForIllegalCrossThreadCalls = False
    End Sub
    Public CloseLoadingForm As Boolean = False
    'Dim MarqueeProgressBarControl1 As DevExpress.XtraEditors.MarqueeProgressBarControl

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If CloseLoadingForm Then
            'MarqueeProgressBarControl1.Dispose()
            Timer1.Enabled = False
            Me.Close()
            'CloseLoadingForm = False
        End If
    End Sub

    'Private Sub frmLoading_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    '    picLoading.Refresh()

    '    MarqueeProgressBarControl1 = New DevExpress.XtraEditors.MarqueeProgressBarControl
    '    Me.Controls.Add(MarqueeProgressBarControl1)
    '    MarqueeProgressBarControl1.EditValue = "Loading..."
    '    MarqueeProgressBarControl1.Location = New System.Drawing.Point(24, 79)
    '    MarqueeProgressBarControl1.Name = "MarqueeProgressBarControl1"
    '    MarqueeProgressBarControl1.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    MarqueeProgressBarControl1.Properties.Appearance.Options.UseFont = True
    '    MarqueeProgressBarControl1.Properties.LookAndFeel.SkinName = "Money Twins"
    '    MarqueeProgressBarControl1.Properties.LookAndFeel.UseDefaultLookAndFeel = False
    '    MarqueeProgressBarControl1.Properties.ShowTitle = True
    '    MarqueeProgressBarControl1.Size = New System.Drawing.Size(104, 22)
    '    MarqueeProgressBarControl1.TabIndex = 4
    '    MarqueeProgressBarControl1.Refresh()
    'End Sub
End Class