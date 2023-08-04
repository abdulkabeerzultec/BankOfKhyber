Public Class frmDepProgress
    Public CanClose As Boolean = False

    Private Sub frmDepProgress_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not CanClose Then
            e.Cancel = True
        End If
    End Sub

    Private Sub frmDepProgress_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
    End Sub
End Class