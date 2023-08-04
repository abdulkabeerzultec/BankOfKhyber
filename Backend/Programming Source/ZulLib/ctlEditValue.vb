Public Class ctlEditValue
    Private EnterValue As String = String.Empty
    Private _EditValueChanged As Boolean = False
    Public Property UserChangedValue() As Boolean
        Get
            Return _EditValueChanged
        End Get
        Set(ByVal value As Boolean)
            _EditValueChanged = value
        End Set
    End Property
    Private Sub ctlEditValue_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtData.Properties.ReadOnly = True
    End Sub

    Private Sub txtData_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtData.ButtonClick
        txtData.Properties.ReadOnly = False
        EnterValue = txtData.Text
    End Sub

    Private Sub txtData_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtData.Leave
        txtData.Properties.ReadOnly = True
        If EnterValue <> txtData.Text Then
            UserChangedValue = True
        Else
            UserChangedValue = False
        End If
    End Sub

    Public Overrides Property Text() As String
        Get
            Return txtData.Text
        End Get

        Set(ByVal value As String)
            txtData.Text = value
            EnterValue = txtData.Text
            UserChangedValue = False
        End Set
    End Property

    Public ReadOnly Property TextBox() As DevExpress.XtraEditors.TextEdit
        Get
            Return txtData
        End Get
    End Property

End Class
