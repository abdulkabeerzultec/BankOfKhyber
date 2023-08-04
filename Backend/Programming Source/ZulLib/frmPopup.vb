Public Class frmPopup

    Private Sub frmPopup_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.TopLevel = False
        Me.Owner = Nothing
        Me.Parent = Nothing
        Me.Hide()
    End Sub

    Private Sub frmPopup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyData = Windows.Forms.Keys.Escape Then
            Me.TopLevel = False
            Me.Owner = Nothing
            Me.Parent = Nothing
            Me.Hide()
        End If
    End Sub

End Class