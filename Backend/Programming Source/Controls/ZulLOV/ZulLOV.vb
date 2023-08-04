Imports System.Drawing
Imports System.Environment

Public Class ZulLOV
    Protected _valueMember As String = ""
    Protected _displayMember As String = ""
    Protected _selectedIndex As Integer = -1
    Protected DtSource As DataTable

    Friend txttext As String = ""
    Friend txttag As String = ""
    Friend FocusedRowHandle As Integer = DevExpress.XtraGrid.GridControl.InvalidRowHandle

    
    Public m_RowsCount As Long
    Public frm As frmDevExpGrid
    'Public InFillingProcess As Boolean = False
    Private _ReadOnly As Boolean = False
    Public Event SelectTextChanged As System.EventHandler
    Public Event LovBtnClick As System.EventHandler


    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Function FindRow(ByVal strID As String, ByVal strText As String) As Integer
        Me.txtLOV.Tag = strID
        Me.txtLOV.Text = strText
    End Function

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

    Public Property SelectedIndex() As Integer
        Get
            Return _selectedIndex
        End Get
        Set(ByVal value As Integer)
            _selectedIndex = value
            If _selectedIndex < 0 Then
                Me.txtLOV.Tag = ""
                Me.txtLOV.Text = ""
            End If
        End Set
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
            If txtLOV.Text <> "" Then
                Return txtLOV.Tag
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            Me.txtLOV.Tag = value
        End Set

    End Property

    Public ReadOnly Property TextBox() As DevExpress.XtraEditors.TextEdit
        Get
            Return txtLOV
        End Get
    End Property

    Public Property SelectedText() As String
        Get
            Return Me.txtLOV.Text
        End Get

        Set(ByVal value As String)
            Me.txtLOV.Text = value
        End Set

    End Property

    Public Property DataSource() As DataTable
        Get
            Return DtSource
        End Get
        Set(ByVal value As DataTable)
            DtSource = value
        End Set
    End Property

    Private Sub ZulLOV_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.BackColor = Me.Parent.BackColor
    End Sub

    Public Sub EnabledME(ByVal stat As Boolean)
        If stat = False Then
            If Me.Size = New Size(270, 180) = True Then
                Me.Size = New Size(209, 24)
                Me.BorderStyle = Windows.Forms.BorderStyle.None
                frm.Visible = False
                Me.Refresh()
            End If
        End If
    End Sub

    Private Sub LOVControl_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.Width < 40 Then
            Me.Width = 35
            Exit Sub
        End If
        Me.Invalidate()
    End Sub

    Private Sub txtLOV_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLOV.KeyDown
        If Not TextReadOnly Then
            If e.KeyData = Keys.Back Or e.KeyData = Keys.Delete Then
                txtLOV.Text = ""
                txtLOV.Tag = ""
                txttag = ""
                txttext = ""
            ElseIf e.Alt And e.KeyValue = Keys.Down Then
                btnLOV_Click(sender, e)
            End If
        End If
    End Sub

    Private Sub txtLOV_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLOV.TextChanged
        RaiseEvent SelectTextChanged(sender, e)
    End Sub

    Private Sub btnLOV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLOV.Click
        If Not TextReadOnly Then
            RaiseEvent LovBtnClick(sender, e)
        End If
    End Sub

    Public Sub OpenLOV()
        frm = New frmDevExpGrid
        Try
            frm.DataSource = Me.DtSource
            frm.DisplayMember = Me._displayMember
            frm.ValueMember = Me._valueMember
            frm.frmLov = Me
            AddHandler frm.Deactivate, AddressOf LovDeactivated
            frm.ChangeGridPlace()
            frm.Show()
            frm.grdLOVMainView.Focus()

            'select the row if there is key in the tag
            Dim rowKeyVal As String = ""
            If frm.grdLOVMainView.GetRowCellValue(FocusedRowHandle, frm.grdLOVMainView.Columns(0).FieldName) IsNot Nothing Then
                rowKeyVal = frm.grdLOVMainView.GetRowCellValue(FocusedRowHandle, frm.grdLOVMainView.Columns(0).FieldName)
            End If
            If TextBox.Text <> "" And FocusedRowHandle >= 0 And rowKeyVal <> "" Then
                frm.grdLOVMainView.FocusedRowHandle = FocusedRowHandle
            Else ' select filter row
                FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle
                frm.grdLOVMainView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
                frm.grdLOVMainView.FocusedColumn = frm.grdLOVMainView.VisibleColumns(0)
                frm.grdLOVMainView.ShowEditor()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnLOV_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnLOV.KeyDown
        If e.Alt And e.KeyValue = Keys.Down Then
            btnLOV_Click(sender, e)
        End If
    End Sub

    Private Sub LovDeactivated()
        If txttext <> "" Then
            SelectedValue = txttag
            SelectedText = txttext
        End If
    End Sub

End Class
