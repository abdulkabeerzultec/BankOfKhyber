Public Class frmTree
    Public frmTree As ZulTree
    Protected DtSource As DataTable
    Protected _ValueMember As String = ""
    Protected _DisplayMember As String = ""
    Dim SelNode As TreeNode
    Dim ColIndex As Integer
    Dim trvDisposed As Boolean = False

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

    Private Sub frmTree_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        Me.Close()
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

    Private Sub frmTree_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        trv.Nodes.Clear()
        DtSource = frmTree.DataSource
        If DtSource IsNot Nothing Then
            FillTree()
        End If

        Me.Left = Get_Form_Left(frmTree) + 10
        Me.Top = Get_Form_Top(frmTree) + frmTree.Height + 65
        Me.Width = frmTree.txt.Width
        Me.trv.Width = frmTree.txt.Width
        Me.Invalidate()
        Dim rec As Rectangle

        rec = Screen.PrimaryScreen.WorkingArea


        Me.Left = Get_Form_Left(frmTree.txt) + 10
        Me.Top = Get_Form_Top(frmTree.txt) + frmTree.txt.Height + 65

        If (Me.Width + Me.Left) > rec.Width Then
            Me.Left = Me.Left - (Me.Width + Me.Left - rec.Width) - 10
        End If

        If Me.Left < 0 Then
            Me.Left = 10
        End If


        If (Me.Height + Me.Top) > rec.Height Then
            Me.Top = Get_Form_Top(frmTree.txt) - Me.Height + 65
        End If
    End Sub

    Dim ht As New Hashtable

    Private Sub FillTree()
        Try
            Dim rcount As Integer
            If DtSource Is Nothing = False Then
                rcount = DtSource.Rows.Count
                If rcount > 0 Then
                    For Each dr As DataRow In DtSource.Rows
                        Create_TreeNode(dr(0), dr(1), trv)
                    Next
                End If
            End If
            trv.ExpandAll()
            trv.CollapseAll()
            If frmTree.TextBox.Tag <> "" Then
                trv.SelectedNode = CType(ht.Item(frmTree.TextBox.Tag), TreeNode)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            ht.Clear()
        End Try
    End Sub
    Public Sub Create_TreeNode(ByVal NID As String, ByVal NDesc As String, ByVal trv As TreeView)
        If NID.IndexOf("-") < 0 Then
            Dim NNode As New TreeNode(NDesc)
            NNode.Tag = NID
            trv.Nodes.Add(NNode)
            ht.Add(NID, NNode)
        Else
            Dim ParentID As String = NID.Substring(0, NID.LastIndexOf("-"))
            Dim NNode As New TreeNode(NDesc)
            NNode.Tag = NID
            Dim PNode As TreeNode = CType(ht.Item(ParentID), TreeNode)

            If PNode IsNot Nothing Then
                PNode.Nodes.Add(NNode)
                ht.Add(NID, NNode)
            End If
        End If
    End Sub

    'Private Sub Create_Tree(ByVal strAstID As String, ByVal strAstDesc As String)
    '    Try
    '        Dim Id, ParentID As String
    '        If strAstID.IndexOf("-") < 0 Then
    '            Dim NNode As New TreeNode(strAstDesc)
    '            NNode.Tag = strAstID
    '            trv.Nodes.Add(NNode)
    '        Else
    '            Dim arrAstCat() As String = strAstID.Split("-")
    '            Id = arrAstCat(arrAstCat.Length - 1)
    '            ParentID = strAstID.Substring(0, strAstID.LastIndexOf("-"))
    '            If Not trv.TopNode Is Nothing Then
    '                Dim NNode As New TreeNode(strAstDesc)
    '                NNode.Tag = ParentID & "-" & Id
    '                FindNode_byTag(ParentID, trv.Nodes, NNode)
    '            End If

    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    'Private Sub FindNode_byTag(ByVal AstID As String, ByVal FNode As TreeNodeCollection, ByVal NewNode As TreeNode)
    '    Try
    '        For Each nNode As TreeNode In FNode
    '            If nNode.Tag = AstID Then
    '                nNode.Nodes.Add(NewNode)
    '            Else
    '                FindNode_byTag(AstID, nNode.Nodes, NewNode)
    '            End If
    '        Next
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub trv_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trv.KeyDown
        If e.KeyData = Keys.Escape Then
            trvDisposed = True
            Me.Dispose()
        ElseIf e.KeyData = Keys.Enter Then
            If Not SelNode Is Nothing Then
                frmTree.txt.Tag = SelNode.Tag
                frmTree.txt.Text = SelNode.FullPath.Replace("\", " \ ")
                trvDisposed = True
                Me.Dispose()
            End If
        End If
    End Sub

    Private Sub trv_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trv.NodeMouseDoubleClick
        Try
            'If e.Button = Windows.Forms.MouseButtons.Left Then
            If Not SelNode Is Nothing Then
                frmTree.txt.Tag = SelNode.Tag
                frmTree.txt.Text = SelNode.FullPath.Replace("\", " \ ")
            End If
            'End If
            trvDisposed = True
            Me.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub trv_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trv.AfterSelect
        SelNode = trv.SelectedNode
    End Sub

    Private Sub trv_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trv.MouseUp
        If e.Button = MouseButtons.Right Then
            If Not trvDisposed Then
                Dim n As TreeNode = Me.trv.GetNodeAt(e.X, e.Y)
                If Not n Is Nothing Then
                    Me.trv.SelectedNode = n
                End If
            End If
        End If
    End Sub
End Class