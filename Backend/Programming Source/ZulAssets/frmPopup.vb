Imports ZulLib

Public Class frmPopup
    Private Sub frmPopup_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim ctl As Control = Me.Controls(0)
        If TypeOf ctl Is ctlDataEditing Then
            CType(ctl, ctlDataEditing).btnClose.PerformClick()
            e.Cancel = True
        ElseIf TypeOf ctl Is ctlGridDataEditing Then
            CType(ctl, ctlGridDataEditing).btnClose.PerformClick()
            e.Cancel = True
        Else
            e.Cancel = False
        End If
    End Sub

    Private Sub frmPopup_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
        End If
    End Sub

End Class