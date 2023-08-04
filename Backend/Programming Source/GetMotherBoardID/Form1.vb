Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lic As New ZulLib.ZulLic
        Me.TextBox1.Text = lic.GenSerial("G", "E")
    End Sub
End Class
