Public Class ctlTreeDataEditing
    Public Delegate Sub MySubEvent(ByVal ParentGuid As Guid)
    Public Event OnLoadData As System.EventHandler
    Public Event OnbtnAddSubItemClicked As MySubEvent
    Public Event OnbtnDeleteClicked As MySubEvent

    Private WithEvents PopupForm As New frmPopup
    Private _DataEditingControl As ctlDataEditing
    Protected ActionResult As Integer


    Public Property DataEditingControl() As ctlDataEditing
        Get
            Return _DataEditingControl
        End Get
        Set(ByVal value As ctlDataEditing)
            If value IsNot Nothing Then
                _DataEditingControl = value
                PopupForm.Controls.Add(DataEditingControl)
                PopupForm.Size = New System.Drawing.Size(DataEditingControl.Width, DataEditingControl.Height)
                DataEditingControl.Dock = System.Windows.Forms.DockStyle.Fill
                ChangeCloseEventHandler()
            End If
            'Get the left/top point of the treelist and assign it to the form.
            Dim p As New System.Drawing.Point(treeList.Left, treeList.Top)
            p = Me.PointToScreen(p)
            PopupForm.Top = p.Y
            PopupForm.Left = p.X
        End Set
    End Property

    Private Sub ctlTreeDataEditing_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If DataEditingControl.ParentForm IsNot Nothing Then
            DataEditingControl.ParentForm.Dispose()
        Else
            DataEditingControl.Dispose()
        End If
    End Sub


    Private Sub ctlTreeDataEditing_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        treeList.Dock = System.Windows.Forms.DockStyle.Fill
        treeList.OptionsBehavior.Editable = False
        treeList.OptionsView.ShowIndicator = False
        treeList.OptionsSelection.EnableAppearanceFocusedCell = False
        treeList.OptionsBehavior.AllowIncrementalSearch = True
        RaiseEvent OnLoadData(sender, e)
    End Sub

    Public Property ParentFieldName() As String
        Get
            Return treeList.ParentFieldName
        End Get
        Set(ByVal value As String)
            treeList.ParentFieldName = value
        End Set
    End Property

    Public Property ValueMember() As String
        Get
            Return treeList.KeyFieldName
        End Get
        Set(ByVal value As String)
            treeList.KeyFieldName = value
        End Set
    End Property

    Public Property DisplayMember() As String
        Get
            Return treeList.PreviewFieldName
        End Get

        Set(ByVal value As String)
            treeList.PreviewFieldName = value
        End Set
    End Property
    Public Property DataSource() As DataTable
        Get
            Return treeList.DataSource
        End Get
        Set(ByVal value As DataTable)
            treeList.DataSource = value
        End Set
    End Property

    Private Sub btnClose_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnClose.ItemClick
        Parent.Dispose()
    End Sub


    Private Sub btnRefresh_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRefresh.ItemClick
        Dim SelectedValue As Object = Nothing
        
        If treeList.FocusedNode IsNot Nothing Then
            SelectedValue = treeList.FocusedNode.GetValue(treeList.KeyFieldName).ToString()
        End If
        RaiseEvent OnLoadData(sender, e)
        If Not IsNullOrEmptyValue(SelectedValue) Then
            Dim LocateValue As Guid
            If TryStrToGuid(SelectedValue.ToString, LocateValue) Then
                SelectedValue = LocateValue
            End If
            Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode

            node = treeList.FindNodeByKeyID(SelectedValue)
            If node IsNot Nothing Then
                treeList.SetFocusedNode(node)
                treeList.FocusedNode.ExpandAll()
            End If

            'If TryStrToGuid(SelectedValue.ToString, LocateValue) Then
            '    node = treeList.FindNodeByKeyID(LocateValue)
            '    If node IsNot Nothing Then
            '        treeList.SetFocusedNode(node)
            '        treeList.FocusedNode.ExpandAll()
            '    End If
            'Else
            '    node = treeList.FindNodeByKeyID(SelectedValue)
            '    If node IsNot Nothing Then
            '        treeList.SetFocusedNode(node)
            '        treeList.FocusedNode.ExpandAll()
            '    End If
            'End If
        End If
    End Sub

    Private Sub btnDelete_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDelete.ItemClick
        Try
            If treeList.FocusedNode IsNot Nothing Then
                If ShowDeleteConfirmation() = Windows.Forms.DialogResult.Yes Then
                    Dim nodekey As Guid = treeList.FocusedNode.GetValue(treeList.KeyFieldName)
                    RaiseEvent OnbtnDeleteClicked(nodekey)
                    If ActionResult = 0 Then
                        If treeList.FocusedNode.PrevVisibleNode IsNot Nothing Then
                            treeList.SetFocusedNode(treeList.FocusedNode.PrevVisibleNode)
                        End If
                        btnRefresh.PerformClick()
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnEdit_ItemPress(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnEdit.ItemPress
        If treeList.FocusedNode IsNot Nothing Then
            ShowPopup(btnEdit)
        End If
    End Sub

    Private Sub btnAddRoot_ItemPress(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnAddRoot.ItemPress
        ShowPopup(btnAddRoot)
    End Sub

    Private Sub btnAddSub_ItemPress(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnAddSub.ItemPress
        If treeList.FocusedNode IsNot Nothing Then
            ShowPopup(btnAddSub)
        End If
    End Sub

    Private Sub ShowPopup(ByVal sender As Object)
        If DataEditingControl IsNot Nothing Then
            PopupForm.BringToFront()
            PopupForm.TopLevel = True
            If Not PopupForm.Visible Then
                PopupForm.Show(Me.ParentForm)
            End If

            If sender Is btnAddRoot Then
                DataEditingControl.NewData()
            ElseIf sender Is btnAddSub Then
                DataEditingControl.NewData()
                Dim SelectedValue As Guid = treeList.FocusedNode.GetValue(treeList.KeyFieldName)
                RaiseEvent OnbtnAddSubItemClicked(SelectedValue)
            ElseIf sender Is btnEdit Then
                Dim SelectedValue As Guid = treeList.FocusedNode.GetValue(treeList.KeyFieldName)
                DataEditingControl.RecordGUID = SelectedValue
                DataEditingControl.LoadData()
            End If
        End If
    End Sub

    Private Sub PopupForm_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PopupForm.VisibleChanged
        If Not PopupForm.Visible Then
            btnRefresh.PerformClick()
        End If
    End Sub

    Private Sub btnClosePopup_Clicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'To be able to close the main form we have to assign the owner and parent to Null.
        PopupForm.Owner = Nothing
        PopupForm.Parent = Nothing
        PopupForm.TopLevel = False
        PopupForm.Hide()
    End Sub

    Private Sub ChangeCloseEventHandler()
        RemoveHandler Me.DataEditingControl.btnClose.ItemClick, AddressOf Me.DataEditingControl.btnClose_ItemClick
        AddHandler Me.DataEditingControl.btnClose.ItemClick, AddressOf btnClosePopup_Clicked
    End Sub

    Private Sub btnCollapse_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCollapse.ItemClick
        treeList.CollapseAll()
    End Sub

    Private Sub btnExpand_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnExpand.ItemClick
        treeList.ExpandAll()
    End Sub

    Private Sub treeList_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles treeList.KeyDown
        If e.KeyData = Windows.Forms.Keys.Delete Or e.KeyData = Windows.Forms.Keys.Back Then
            If treeList.FocusedNode IsNot Nothing Then
                btnDelete.PerformClick()
            End If
        End If
    End Sub

    Private Sub treeList_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles treeList.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            PopupMenu1.ShowPopup(Windows.Forms.Control.MousePosition)
        End If
    End Sub

  
End Class
