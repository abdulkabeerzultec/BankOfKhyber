Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class frmDevExpGrid
    Public frmLov As ZulLOV
    Protected DtSource As DataTable
    Protected _ValueMember As String = ""
    Protected _DisplayMember As String = ""

    Public Property DataSource() As DataTable
        Get
            Return DtSource
        End Get
        Set(ByVal value As DataTable)
            DtSource = value
        End Set
    End Property

    Public Property ValueMember() As String
        Get
            Return _ValueMember
        End Get

        Set(ByVal value As String)
            _ValueMember = value
        End Set

    End Property

    Public Property DisplayMember() As String
        Get
            Return _DisplayMember
        End Get

        Set(ByVal value As String)
            _DisplayMember = value
        End Set

    End Property

    Public Sub ShowMe()
        Me.Visible = Not (Me.Visible)
    End Sub

    Private Function Get_Form_Top(ByVal FrameName As Windows.Forms.Control)
        Dim intTop As Integer = FrameName.Top
        While FrameName.Parent Is Nothing = False
            FrameName = FrameName.Parent
            intTop = intTop + FrameName.Top
        End While
        Return intTop
    End Function

    Private Function Get_Form_Left(ByVal FrameName As Windows.Forms.Control)
        Dim intTop As Integer = FrameName.Left
        While FrameName.Parent Is Nothing = False
            FrameName = FrameName.Parent
            intTop = intTop + FrameName.Left
        End While
        Return intTop
    End Function

    Public Sub ChangeGridPlace()
        Me.Left = Get_Form_Left(frmLov.txtLOV) + 10
        Me.Top = Get_Form_Top(frmLov.txtLOV) + frmLov.Height + 55
        Me.Invalidate()
    End Sub

   

    Private Sub DevExpGrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Visible = False
        DtSource = frmLov.DataSource

        If DtSource IsNot Nothing Then
            grdLOV.DataSource = DataSource
            Dim k As Integer = 0
            For i As Integer = 0 To grdLOVMainView.Columns.Count - 1
                k = k + grdLOVMainView.Columns(i).Width
                grdLOVMainView.Columns(i).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                grdLOVMainView.Columns(i).AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
            Next
            If grdLOVMainView.RowCount > 6 Then
                Me.Width = k + 22
            Else
                Me.Width = k
            End If

            If Me.frmLov.Width > Me.Width Then
                Me.Width = Me.frmLov.Width
            End If
            grdLOVMainView.OptionsView.ColumnAutoWidth = True
            grdLOV.UseEmbeddedNavigator = True

            grdLOV.EmbeddedNavigator.Buttons.Append.Visible = False
            grdLOV.EmbeddedNavigator.Buttons.Edit.Visible = False
            grdLOV.EmbeddedNavigator.Buttons.EndEdit.Visible = False
            grdLOV.EmbeddedNavigator.Buttons.Remove.Visible = False
            grdLOV.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Else
            grdLOV.DataSource = Nothing
        End If

        frmLov.txtLOV.Cursor = Cursors.Default


        frmLov.m_RowsCount = grdLOVMainView.RowCount - 1
        Me.Left = Get_Form_Left(frmLov.txtLOV) + 10
        Me.Top = Get_Form_Top(frmLov.txtLOV) + frmLov.Height + 60
        Me.Invalidate()
        Dim rec As Rectangle

        rec = Screen.PrimaryScreen.WorkingArea
        If grdLOVMainView.RowCount > 0 Then
            Me.Left = Get_Form_Left(frmLov.txtLOV) + 10
            Me.Top = Get_Form_Top(frmLov.txtLOV) + frmLov.Height + 60
            If (Me.Width + Me.Left) > rec.Width Then
                Me.Left = Me.Left - (Me.Width + Me.Left - rec.Width) - 60
            End If
            If Me.Left < 0 Then
                Me.Left = 10
            End If

            If (Me.Height + Me.Top) > rec.Height Then
                Me.Top = Get_Form_Top(frmLov.txtLOV) - Me.Height + 60
            End If
        End If
    End Sub

    Private Sub frmDevExpGrid_Deactivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Deactivate
        Me.Close()
        'Me.Dispose()
    End Sub

    Private Sub grdLOVMainView_ShowFilterPopupListBox(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.FilterPopupListBoxEventArgs) Handles grdLOVMainView.ShowFilterPopupListBox
        'to remove showing the custom from the filter menu.
        Dim index As Integer = -1
        Dim custom As String = DevExpress.XtraGrid.Localization.GridLocalizer.Active.GetLocalizedString(DevExpress.XtraGrid.Localization.GridStringId.PopupFilterCustom)
        For i As Integer = 0 To e.ComboBox.Items.Count
            If (e.ComboBox.Items(i).ToString = custom) Then
                index = i
                Exit For
            End If
        Next
        If index > -1 Then
            e.ComboBox.Items.RemoveAt(index)
        End If
    End Sub

    Private Sub grdLOVMainView_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdLOVMainView.DoubleClick
        'disable double click on the footer and other parts.
        Dim GHI As New GridHitInfo()
        GHI = grdLOVMainView.CalcHitInfo(CType(e, DevExpress.Utils.DXMouseEventArgs).Location)
        If GHI.InRow Then
            If grdLOVMainView.RowCount > 0 Then
                Dim pos As Integer = BindingContext(DtSource).Position
                'frmLov.SelectedValue = DtSource.Rows(pos)(ValueMember).ToString
                'frmLov.SelectedText = DtSource.Rows(pos)(DisplayMember).ToString
                frmLov.txttag = DtSource.Rows(pos)(ValueMember).ToString
                frmLov.txttext = DtSource.Rows(pos)(DisplayMember).ToString
                frmLov.FocusedRowHandle = grdLOVMainView.FocusedRowHandle
                Visible = False
            End If
        End If
    End Sub

    Private Sub grdLOV_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdLOV.KeyDown
        If e.KeyData = Keys.Enter Then
            If grdLOVMainView.RowCount > 0 Then
                Dim pos As Integer = BindingContext(DtSource).Position
                'frmLov.SelectedValue = DtSource.Rows(pos)(ValueMember).ToString
                'frmLov.SelectedText = DtSource.Rows(pos)(DisplayMember).ToString
                frmLov.txttag = DtSource.Rows(pos)(ValueMember).ToString
                frmLov.txttext = DtSource.Rows(pos)(DisplayMember).ToString
                frmLov.FocusedRowHandle = grdLOVMainView.FocusedRowHandle

                Visible = False
            End If
        ElseIf e.KeyData = Keys.Escape Then
            Visible = False
        End If
    End Sub

    Private Sub grdLOV_ProcessGridKey(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdLOV.ProcessGridKey
        If e.KeyData = Keys.Escape Then
            Visible = False
        End If
    End Sub
End Class