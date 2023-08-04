Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class ctlTreeList
    Public Event SelectTextChanged As System.EventHandler
    Public Event OnbtnDownClicked As System.EventHandler

    Private WithEvents PopupTree As New DevExpress.XtraEditors.PopupContainerControl
    Private WithEvents treeList As New DevExpress.XtraTreeList.TreeList

    Private WithEvents PopupDataAdding As DevExpress.XtraEditors.PopupContainerControl
    Private WithEvents btnSelectRecord As DevExpress.XtraBars.BarButtonItem

    Private DataAddingControl As ctlDataEditing
    Private _SelectedNodeText As String
    Private pathSeparator = " \ "

    Private Sub btnSelectRecord_Clicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectRecord.ItemClick
        If DataAddingControl.cmbList.EditValue IsNot Nothing Then
            txtData.Tag = DataAddingControl.cmbList.EditValue
            txtData.Text = DataAddingControl.cmbList.Edit.GetDisplayText(txtData.Tag)
        End If
        ClosePopup()
    End Sub

    Public Property btnAddDataVisible() As Boolean
        Get
            Return txtData.Properties.Buttons(1).Visible
        End Get
        Set(ByVal value As Boolean)
            txtData.Properties.Buttons(1).Visible = value
        End Set
    End Property
    Public Sub SetDataAddingControl(ByVal ctl As ctlDataEditing)
        PopupDataAdding = New DevExpress.XtraEditors.PopupContainerControl
        btnSelectRecord = New DevExpress.XtraBars.BarButtonItem
        DataAddingControl = ctl
        ctl.isControl = True
        PopupDataAdding.Size = New System.Drawing.Size(DataAddingControl.Width, DataAddingControl.Height)
        PopupDataAdding.Controls.Add(DataAddingControl)
        DataAddingControl.Dock = System.Windows.Forms.DockStyle.Fill
        AddSelectButtonToControl()
    End Sub

    Private Sub AddSelectButtonToControl()
        Me.btnSelectRecord.Caption = My.Resources.Strings.btnSelectRecordCaption
        Me.btnSelectRecord.Glyph = My.Resources.Resources.check
        Me.btnSelectRecord.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectRecord.Hint = My.Resources.Strings.btnSelectRecordHint
        Me.btnSelectRecord.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.F2))
        btnSelectRecord.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        'ForceInitialize to Insert the button at the end 
        DataAddingControl.BarManager1.ForceInitialize()
        DataAddingControl.barStatus.AddItem(btnSelectRecord)

    End Sub


    Private Sub ctlLov_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtData.Properties.CloseOnOuterMouseClick = False
        Me.Controls.Add(PopupTree)
        PopupTree.Controls.Add(treeList)
        PopupTree.Size = New System.Drawing.Size(txtData.Width, 250)
        txtData.Properties.PopupControl = PopupTree
        TreeFormat()
    End Sub

    Private Sub TreeFormat()
        treeList.Dock = System.Windows.Forms.DockStyle.Fill
        treeList.OptionsBehavior.Editable = False
        treeList.OptionsView.ShowIndicator = False
        treeList.OptionsSelection.EnableAppearanceFocusedCell = False
        treeList.OptionsBehavior.AllowIncrementalSearch = True
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

    Private Function SelectNode(ByVal Node As DevExpress.XtraTreeList.Nodes.TreeListNode) As Boolean
        If Node IsNot Nothing Then
            txtData.Tag = Node.GetValue(treeList.KeyFieldName)
            SelectedNodeText = Node.GetValue(treeList.PreviewFieldName)
            FullPathText = GetFullPath(Node, pathSeparator)
            Return True
        Else
            Return False
        End If
    End Function

    Public Property SelectedValue() As Object
        Get
            Return txtData.Tag
        End Get

        Set(ByVal value As Object)
            If IsNullOrEmptyValue(value) Then
                txtData.Text = Nothing
                txtData.Tag = Nothing
            Else
                Dim rowGUID As Guid
                If TryStrToGuid(value.ToString, rowGUID) Then
                    value = rowGUID
                End If
                txtData.Tag = value

                If DataSource Is Nothing Then
                    RaiseEvent OnbtnDownClicked(Nothing, Nothing)
                    'Update the datasource because it's not filling the tree nodes after setting the datasource by code.
                    treeList.RefreshDataSource()
                End If
                If Not DataSource Is Nothing Then
                    Dim index As Integer = -1
                    Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode
                    node = treeList.FindNodeByKeyID(value)
                    If SelectNode(node) Then
                        treeList.SetFocusedNode(node)
                    End If
                End If
            End If
        End Set
    End Property

    Public Property SelectedNodeText() As String
        Get
            Return _SelectedNodeText
        End Get

        Set(ByVal value As String)
            _SelectedNodeText = value
        End Set
    End Property


    Public Property FullPathText() As String
        Get
            Return txtData.Text
        End Get

        Set(ByVal value As String)
            txtData.Text = value
        End Set
    End Property
    Public ReadOnly Property TextBox() As DevExpress.XtraEditors.TextEdit
        Get
            Return txtData
        End Get
    End Property


    Public Property DataSource() As DataTable
        Get
            Return treeList.DataSource
        End Get
        Set(ByVal value As DataTable)
            treeList.DataSource = value
        End Set
    End Property

    Private Sub txtData_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtData.ButtonClick
        If e.Button.Index = 0 Then
            txtData.Properties.PopupControl = PopupTree
        Else
            txtData.Properties.PopupControl = PopupDataAdding
        End If
        txtData.Properties.ActionButtonIndex = e.Button.Index
        txtData.ShowPopup()
    End Sub

    Private Sub txtData_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtData.Popup
        If Not IsNullOrEmptyValue(SelectedValue) Then
            Dim LocateValue As Guid
            If TryStrToGuid(SelectedValue.ToString, LocateValue) Then
                SelectedValue = LocateValue
            End If
            Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode
            node = treeList.FindNodeByKeyID(LocateValue)
            If node IsNot Nothing Then
                treeList.SetFocusedNode(node)
            End If

            'If TryStrToGuid(SelectedValue, LocateValue) Then
            '    node = treeList.FindNodeByKeyID(LocateValue)
            '    If node IsNot Nothing Then
            '        treeList.SetFocusedNode(node)
            '    End If
            'Else
            '    node = treeList.FindNodeByKeyID(LocateValue)
            '    If node IsNot Nothing Then
            '        treeList.SetFocusedNode(node)
            '    End If
            'End If
        End If
    End Sub

    Private Sub txtData_QueryPopUp(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtData.QueryPopUp

        If txtData.Properties.ActionButtonIndex = 0 Then
            RaiseEvent OnbtnDownClicked(sender, e)
            treeList.ExpandAll()

            'Dim k As Integer = 0
            'For i As Integer = 0 To grdLOVMainView.Columns.Count - 1
            '    k = k + grdLOVMainView.Columns(i).Width
            '    grdLOVMainView.Columns(i).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            '    grdLOVMainView.Columns(i).AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
            'Next

            'If PopupTree.Width < k Then
            '    PopupTree.Width = k
            'End If

            'If HideValueMember Then
            '    If grdLOVMainView.Columns.ColumnByFieldName(ValueMember) IsNot Nothing Then
            '        grdLOVMainView.Columns(ValueMember).Visible = False
            '    End If
            'End If

        End If
    End Sub

    Private Sub txtData_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtData.TextChanged
        RaiseEvent SelectTextChanged(sender, e)
    End Sub

    ' call PopupContainerEdit.ClosePopup
    Private Sub ClosePopup()
        txtData.ClosePopup()
    End Sub

    Private Sub treeList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles treeList.KeyDown
        If e.KeyData = Keys.Enter Then
            If SelectNode(treeList.FocusedNode) Then
                ClosePopup()
            End If
            'If treeList.FocusedNode IsNot Nothing Then
            '    SelectedValue = treeList.FocusedNode.GetValue(treeList.KeyFieldName).ToString
            '    SelectedNodeText = treeList.FocusedNode.GetValue(treeList.PreviewFieldName)
            '    FullPathText = GetFullPath(treeList.FocusedNode, pathSeparator)
            '    ClosePopup()
            'End If
        End If
    End Sub

    Private Sub txtData_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtData.KeyDown
        If e.KeyData = Keys.Delete Or e.KeyData = Keys.Back Then
            SelectedValue = Nothing
            SelectedNodeText = String.Empty
            FullPathText = String.Empty
        End If
    End Sub

    Public Function GetFullPath(ByVal node As DevExpress.XtraTreeList.Nodes.TreeListNode, ByVal pathSeparator As String) As String
        If node Is Nothing Then
            Return ""
        End If
        Dim result As String = ""
        While node IsNot Nothing
            result = pathSeparator & node.GetDisplayText(treeList.PreviewFieldName) & result
            node = node.ParentNode
        End While
        'Remove the path Separator form the bigging of the path.
        Return result.Remove(0, pathSeparator.Length)
    End Function

    Private Sub treeList_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles treeList.MouseClick
        'disable click on the footer and other parts.
        Dim hi As DevExpress.XtraTreeList.TreeListHitInfo = treeList.CalcHitInfo(New Drawing.Point(e.X, e.Y))
        If hi.HitInfoType = DevExpress.XtraTreeList.HitInfoType.Cell Then
            If SelectNode(treeList.FocusedNode) Then
                ClosePopup()
            End If
            'If treeList.FocusedNode IsNot Nothing Then
            '    SelectedValue = treeList.FocusedNode.GetValue(treeList.KeyFieldName).ToString
            '    SelectedNodeText = treeList.FocusedNode.GetValue(treeList.PreviewFieldName)
            '    FullPathText = GetFullPath(treeList.FocusedNode, pathSeparator)
            'End If
        End If
    End Sub
End Class
