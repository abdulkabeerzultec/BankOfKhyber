Public Class ZulTree

    Protected _valueMember As String = ""
    Protected _displayMember As String = ""
    Protected _selectedIndex As Integer = -1
    Public m_RowsCount As Long
    Protected DtSource As DataTable
    Dim frm As frmTree
    Private _ReadOnly As Boolean = False
    Public Event SelectTextChanged As System.EventHandler
    Public Event TreeBtnClick As System.EventHandler

    Public Property TextReadOnly() As Boolean
        Get
            Return _ReadOnly
        End Get
        Set(ByVal value As Boolean)
            _ReadOnly = value
        End Set
    End Property

    Public ReadOnly Property ListCount() As Integer
        Get
            Return m_RowsCount
        End Get
    End Property

    Public Property ValueMember() As String
        Get
            Return _valueMember
        End Get

        Set(ByVal value As String)
            _valueMember = value
        End Set
    End Property

    Public Property DisplayMember() As String
        Get
            Return _displayMember
        End Get

        Set(ByVal value As String)
            _displayMember = value
        End Set
    End Property

    Public Property SelectedValue() As String
        Get
            If txt.Text <> "" Then
                Return txt.Tag
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Me.txt.Tag = value
        End Set
    End Property

    Public Property SelectedText() As String
        Get
            Return txt.Text
        End Get
        Set(ByVal value As String)
            Me.txt.Text = value
        End Set
    End Property
    Public ReadOnly Property TextBox() As DevExpress.XtraEditors.TextEdit
        Get
            Return txt
        End Get
    End Property

    Public Function SetValue(ByVal strID As String, ByVal strText As String) As Integer
        Me.txt.Tag = strID
        Me.txt.Text = strText
    End Function

    Public Property DataSource() As DataTable
        Get
            Return DtSource
        End Get
        Set(ByVal value As DataTable)
            DtSource = value
        End Set
    End Property

    Private Sub ZulTree_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.BackColor = Me.Parent.BackColor
    End Sub

    Private Sub btnLOV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTree.Click
        If Not TextReadOnly Then
            RaiseEvent TreeBtnClick(sender, e)
        End If
        'OpenLOV(btnTree)
    End Sub

    Public Sub OpenLOV()
        frm = New frmTree
        With frm
            .DataSource = Me.DtSource
            .DisplayMember = Me._displayMember
            .ValueMember = Me._valueMember
            .frmTree = Me
            .Show()
        End With
    End Sub

    Private Sub txt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt.KeyDown
        If Not TextReadOnly Then
            If e.KeyData = Keys.Back Or e.KeyData = Keys.Delete Then
                txt.Text = ""
                txt.Tag = ""
            ElseIf e.Alt And e.KeyValue = Keys.Down Then
                btnLOV_Click(sender, e)
            End If
        End If
    End Sub

    Private Sub btnTree_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnTree.KeyDown
        If e.Alt And e.KeyValue = Keys.Down Then
            btnLOV_Click(sender, e)
        End If
    End Sub

    Private Sub txt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt.TextChanged
        RaiseEvent SelectTextChanged(sender, e)
    End Sub
End Class
