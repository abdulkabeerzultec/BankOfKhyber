Imports System.IO
Imports System.Security.Cryptography
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class frmOrgHier

    Dim objattCompLvl As attCompLvl
    Dim objBALCompLvl As New BALCompLvl
    Dim objattCompGroups As attCompGroups
    Dim objBALCompGroups As New BALCompGroups
    Dim objattOrgHier As attOrgHier
    Dim objBALOrgHier As New BALOrgHier
    Dim GrpBound As Short
    Dim TempNode() As TreeNode  'variable to temporarily hold nodes of a tree
    Dim NodeNum As Short = 0
    Dim HitNode As TreeNode

    Private Sub frmOrgHier_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmOrgHier = Nothing
    End Sub

    Private Sub frmOrgHier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        FillGrps()  'Fill trvGrp
        FillHier() 'Fill trvHier
        If FormController.objfrmOrgHier Is Nothing Then
            FormController.objfrmOrgHier = Nothing
        End If

        'Expand all nodes of both the trees
        trvGrp.ExpandAll()
        trvHier.ExpandAll()
    End Sub

    'Private Sub Get_Hirarchy()
    '    Dim ds As DataTable
    '    Dim ParentID
    '    objattCompGroups = New attCompGroups
    '    Try
    '        ds = objBALOrgHier.GetAll_OrgHier(New attOrgHier)
    '        If ds Is Nothing = False Then
    '            If ds Is Nothing = False Then
    '                For Each dr As DataRow In ds.Rows
    '                    ParentID = dr("HierCode")
    '                Next
    '            End If
    '        End If
    '    Catch ex As Exception
    '    End Try

    'End Sub

    Private Sub FillGrps()
        'Dim qry As String
        Dim DataRead As DataTable
        Dim PNode As New TreeNode("Levels")     'Parent node for groups tree
        Dim CPNode As New TreeNode          'Parent node for child nodes   

        'Add parent node "Levels"
        trvGrp.Nodes.Add(PNode)


        Try
            DataRead = objBALCompLvl.GetAll_CompLvl(New attCompLvl)
        Catch ex As Exception
            '   objDAL.CloseConn()
            GenericExceptionHandler(ex, WhoCalledMe)
            Exit Sub
        End Try

        'Add levels to trvGrp. Nodes(0) represent "Levels"
        Try

            If DataRead Is Nothing = False Then
                'If DataRead.Tables(0) Is Nothing = False Then
                If DataRead.Rows.Count > 0 Then
                    For Each dr As DataRow In DataRead.Rows
                        trvGrp.Nodes(0).Nodes.Add(dr("LvlID").ToString & " - " & dr("LvlDesc").ToString)
                    Next
                End If

            End If
            'End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
            Exit Sub
        Finally

        End Try


        Try
            DataRead = Nothing
            DataRead = objBALCompGroups.GetAll_CompGroups(New attCompGroups)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
            Exit Sub
        End Try

        Try
            If DataRead Is Nothing = False Then
                '  If DataRead.Tables(0) Is Nothing = False Then
                If DataRead.Rows.Count > 0 Then
                    For Each dr As DataRow In DataRead.Rows
                        CPNode = NodeExists(dr("LvlID"), trvGrp)
                        If Not CPNode Is Nothing Then
                            CPNode.Nodes.Add(dr("GrpID").ToString & " - " & dr("GrpDesc").ToString)
                        End If
                    Next
                End If
            End If
            '  End If


        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
            Exit Sub
        Finally
            '  DataRead.Close() 'close data reader and db connection. 
        End Try
    End Sub

    'this function returs tree node for the specified NodeValue
    Private Function GetNode(ByVal NodeValue As String, _
                             ByVal TreeViewObject As TreeView) As TreeNode

        Dim ndNodes As TreeNodeCollection, ndNode As TreeNode
        Dim arr() As String

        'Take all nodes of the tree in one collection
        ndNodes = TreeViewObject.Nodes(0).Nodes

        For Each ndNode In ndNodes  'Traverse all nodes on by one
            'split each node text in 2 parts. first part contains level code to 
            'be searched.
            arr = Split(ndNode.Text, " - ")
            'if value matches, return the found node
            If arr(0) = NodeValue Then
                Return ndNode
            End If
        Next
        'if no node found matching the value, then return nothing
        Return Nothing
    End Function

    'When the user begins the drag action on a TreeNode, the ItemDrag event is fired
    Private Sub trvGrp_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles trvGrp.ItemDrag
        'only if a node has 3 parts i.e. grpbound=2, it is allowed to be dragged 
        If GrpBound = 2 Then DoDragDrop(e.Item, DragDropEffects.Copy)
    End Sub

    Private Sub trvHier_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trvHier.DragEnter
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
            'TreeNode found allow move effect
            e.Effect = DragDropEffects.Copy
        Else
            'No TreeNode found, prevent move
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub trvHier_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trvHier.DragOver
        Dim TNode As TreeNode 'Target Node 
        Dim lvl As Integer     'Level 
        Dim GrpNodeTxt As String = "" 'Parent of selected node of group tree
        Dim GrpCode As String = "" 'Code of the node of trvHier
        Dim DataRead As DataTable = Nothing
        'Dim qry As String

        'Check that there is a TreeNode being dragged 
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", _
                                  True) = False Then Exit Sub

        'if hierarchy tree is empty then just copy the node and exit
        If trvHier.Nodes.Count = 0 Then
            e.Effect = DragDropEffects.Copy
            Exit Sub
        End If

        'if hier tree has nodes, get the node on which group from the
        'group tree is being dropped
        Dim pt As Point = trvHier.PointToClient(New Point(e.X, e.Y))
        TNode = trvHier.GetNodeAt(pt)
        'if group is being dropped on empty space, then take group code of first
        'node of hier tree. else take group from current node over which we want
        'to drop the node from group tree.
        If Not TNode Is Nothing Then
            'group code of current node of hier tree over which mouse is
            GrpCode = Mid$(TNode.Text, 1, InStr(TNode.Text, " - ") - 1)
        ElseIf trvHier.Nodes.Count > 0 And TNode Is Nothing Then
            'group code of first node of hier tree
            GrpCode = Mid$(trvHier.Nodes(0).Text, 1, InStr(trvHier.Nodes(0).Text, " - ") - 1)
        End If

        'get the node of group tree that has been dragged over hier tree
        Dim GrpNode As TreeNode = _
                         CType(e.Data.GetData("System.Windows.Forms.TreeNode"),  _
                               TreeNode)
        'Get level Id from the parent of selected node of TrvGrp.
        GrpNodeTxt = GrpNode.Parent.Text
        lvl = Mid$(GrpNodeTxt, 1, InStr(GrpNodeTxt, " - ") - 1)


        'obtain level code for the group code that we obtained above from trvHier()
        Try

            objattCompGroups = New attCompGroups
            objattCompGroups.PKeyCode = GrpCode
            DataRead = objBALCompGroups.GetAll_CompGroups(objattCompGroups)

            ' DataRead = objDAL.ExecDataReader(qry, CommandType.Text)
        Catch ex As Exception
            '   objDAL.CloseConn()
            GenericExceptionHandler(ex, WhoCalledMe)
            Exit Sub
        End Try
        'Parent_NodeLevel(TNode)
        If DataRead Is Nothing = False Then
            If DataRead Is Nothing = False Then
                If DataRead.Rows.Count > 0 Then
                    Dim ParentLvl As Integer = DataRead.Rows(0)("LvlID")
                    If lvl > ParentLvl And Not TNode Is Nothing Then
                        If objBALCompGroups.Verify_Location(lvl, ParentLvl) Then
                            e.Effect = DragDropEffects.Copy
                        End If

                    ElseIf lvl < ParentLvl And Not TNode Is Nothing Then
                        e.Effect = DragDropEffects.None
                    ElseIf lvl = ParentLvl And TNode Is Nothing Then
                        If objBALCompGroups.Verify_Location(lvl, ParentLvl) Then
                            e.Effect = DragDropEffects.Copy
                        End If
                    Else
                        e.Effect = DragDropEffects.None
                    End If
                End If

            End If

        End If

    End Sub
  
    Private Sub trvHier_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trvHier.DragDrop

        Dim TNode As TreeNode   'target node of hier tree
        Dim lvl As Integer     'Level of parent of dragged group tree node 
        Dim GrpKey As String = ""  'code of group tree node
        Dim GCode As String = ""  'Code of the node of trvHier
        Dim DataRead As DataTable = Nothing
        'Dim qry As String
        Dim HierCode As String = ""   'complete hierarchy code
        Dim HierFPath As String = "" 'full path of trvHier node on which group node is droped
        Dim arr() As String   'variable used for split function
        Dim i As Short
        Dim arrBound As Short
        Dim TempPath As String = "" 'string to hold temporary data

        'Check that there is a TreeNode being dragged
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", _
                                  True) = False Then Exit Sub

        'Get the TreeNode being dragged from the group tree
        Dim DropNode As TreeNode = _
                         CType(e.Data.GetData("System.Windows.Forms.TreeNode"),  _
                               TreeNode)

        'if hier tree has nodes, get the node on which group from the
        'group tree is being dropped
        Dim pt As Point = trvHier.PointToClient(New Point(e.X, e.Y))
        TNode = trvHier.GetNodeAt(pt)

        TempPath = ""
        'MessageBox.Show(TNode.Tag & "  " & TNode.Text)

        'if target node is not nothing, then find complete hierachy upto that node
        If Not TNode Is Nothing Then
            HierFPath = TNode.FullPath 'retrieve full path of target node
            arr = Split(HierFPath, "\") 'split to separate node text from hier code
            arrBound = UBound(arr) 'get bound to check if node is parent or child
            If arrBound = 0 Then 'if node is parent then just get hier code
                arr = Split(HierFPath, " - ")
                HierFPath = Trim(arr(0))
            Else  'otherwise run a loop to get hier code for each node
                For i = 0 To UBound(arr) 'get hiercode for each node and concatenate it
                    TempPath = TempPath & "-" & _
                               Mid$(arr(i), 1, InStr(arr(i), " - ") - 1)
                Next
                TempPath = Trim(TempPath)
                'remove first "-" in hierarchy path
                If Mid(TempPath, 1, 1) = "-" Then TempPath = Mid(TempPath, 2)
                HierFPath = TempPath 'assign value to hierarchy variable
            End If
        End If

        'group code of the node of group tree being dragged
        GrpKey = Mid$(DropNode.Text, 1, InStr(DropNode.Text, " - ") - 1)

        'if trvHier has no nodes, just add the group node
        If trvHier.Nodes.Count = 0 Then
            trvHier.Nodes.Add(DropNode.Text)
            Add2DB(GrpKey, GrpKey)
            Exit Sub
        End If

        If TNode Is Nothing Then
            trvHier.Nodes.Add(DropNode.Text)
            Add2DB(GrpKey, GrpKey)
            Exit Sub
        End If

        'Get level Id from the parent of selected node of TrvGrp.
        lvl = Mid$(DropNode.Parent.Text, 1, _
              InStr(DropNode.Parent.Text, " - ") - 1)

        If Not TNode Is Nothing Then
            HierCode = HierFPath & "-" & GrpKey  'get hierarchy code
            'get code of the trvHier node on which drop occur
            GCode = Mid$(TNode.Text, 1, InStr(TNode.Text, " - ") - 1)
        ElseIf TNode Is Nothing Then
            'get code of the trvHier node on which drop occur
            GCode = Mid$(trvHier.Nodes(0).Text, 1, InStr(trvHier.Nodes(0).Text, " - ") - 1)
            HierCode = GrpKey 'get hierarchy code
        End If



        Try

            objattCompGroups = New attCompGroups
            objattCompGroups.PKeyCode = GCode
            DataRead = objBALCompGroups.GetAll_CompGroups(objattCompGroups)
        Catch ex As Exception

            GenericExceptionHandler(ex, WhoCalledMe)
            Exit Sub
        End Try


        If DataRead Is Nothing = False Then
            ' If DataRead.Tables(0) Is Nothing = False Then

            If DataRead.Rows.Count > 0 Then
                If lvl > CInt(DataRead.Rows(0)("LvlID")) And Not TNode Is Nothing Then
                    If objBALCompGroups.Verify_Location(lvl, DataRead.Rows(0)("LvlID")) Then
                        If Add2DB(GrpKey, HierCode) Then
                            TNode.Nodes.Add(DropNode.Text)
                            trvHier.ExpandAll()
                            ' LogEvent("New hierarchy created - " & DropNode.Text)
                        End If
                    End If

                ElseIf lvl = CInt(DataRead.Rows(0)("LvlID")) And Not TNode Is Nothing Then
                    If objBALCompGroups.Verify_Location(lvl, DataRead.Rows(0)("LvlID")) Then
                        If Add2DB(GrpKey, HierCode) Then
                            TNode.Nodes.Add(DropNode.Text)
                            trvHier.ExpandAll()
                            ' LogEvent("New hierarchy created - " & DropNode.Text)
                        End If
                    End If
                End If
            End If
            'End If
        End If

    End Sub

    Private Sub trvGrp_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvGrp.MouseDown

        Dim arr() As String
        Dim SelNode As TreeNode

        'take the node on whice mouse down has occurred
        SelNode = trvGrp.GetNodeAt(e.X, e.Y)
        If Not SelNode Is Nothing Then  'if mouse down is not on blank space
            'split selnode in parts. full path is the path of node from the 
            'Parent(node)
            arr = Split(SelNode.FullPath, "\")
            'see in how many parts it has been splited. only if a node has
            '3 parts i.e. grpbound=2, it is alloed to be dragged in the 
            'trvGrp_ItemDrag event
            GrpBound = UBound(arr)
        Else
            GrpBound = 0   'if mouse down in on blank space
        End If
    End Sub

    Private Function Add2DB(ByVal GCode As String, ByVal HStr As String) As Boolean
        Try
            Dim DataRead As DataTable
            objattOrgHier = New attOrgHier
            objattOrgHier.GrpID = GCode
            objattOrgHier.PKeyCode = HStr
            DataRead = objBALOrgHier.GetAll_OrgHier(objattOrgHier)
            If DataRead Is Nothing = False Then
                ' If DataRead.Tables(0) Is Nothing = False Then
                If DataRead.Rows.Count > 0 Then
                    Return False
                    'End If
                End If
            End If
            objBALOrgHier.Insert_OrgHier(objattOrgHier)
        Catch ex As Exception
            Return True
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
        Return True
    End Function

    Private Sub FillHier()
        Dim DataRead As DataTable
        Dim ds As DataTable
        'Dim qry As String
        Dim HStr As String
        Dim arr() As String
        Dim i As Long
        Dim j As Long
        Dim CurKey As String
        Dim LastKey As String
        Dim KeyExist As Boolean
        Dim msg As String
        Dim isWarehouse As Boolean
        KeyExist = False
        objattCompGroups = New attCompGroups
        Try
            ds = objBALOrgHier.GetAll_OrgHier(New attOrgHier)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
            Exit Sub
        End Try





        For j = 0 To ds.Rows.Count - 1
            HStr = ds.Rows(j).Item("HierCode")
            arr = Split(HStr, "-")
            '       isWarehouse = CBool(ds.Rows(j).Item("isWarehouse"))

            CurKey = ""
            LastKey = ""

            For i = 0 To UBound(arr)
                CurKey = CurKey & "-" & arr(i)
                If i <> 0 Then
                    LastKey = LastKey & "-" & arr(i - 1)
                End If


                'current hierarchy
                If Mid$(CurKey, 1, 1) = "-" Then CurKey = Mid$(CurKey, 2)
                'last hierarchy. it will be parent for next hierarchy
                If Mid$(LastKey, 1, 1) = "-" Then LastKey = Mid$(LastKey, 2)

                'check if current node already exists
                If Not NodeExists(CurKey, trvHier) Is Nothing Then
                    KeyExist = True
                    'ds.Dispose()
                    'objDAL.CloseConn()
                End If


                If Not KeyExist Then 'if node is not already present

                    'Try
                    '    modDB.OpenDB()    'open database conenction
                    'Catch ex As Exception
                    '            GenericExceptionHandler(ex, WhoCalledMe)

                    '    ds.Dispose()
                    '    objDAL.CloseConn()
                    '    Exit Sub
                    'End Try

                    'get group description
                    objattCompGroups.PKeyCode = arr(i)
                    DataRead = objBALCompGroups.GetAll_CompGroups(objattCompGroups)



                    'execute query 
                    msg = "A group required to fill the hierarchy is missing" & _
                          Environment.NewLine & Environment.NewLine & "Group: " & arr(i)



                    If DataRead Is Nothing = False Then
                        'If DataRead.Tables(0) Is Nothing = False Then
                        If DataRead.Rows.Count > 0 Then
                            If i = 0 Then 'if it is first node, add it as parent 
                                HitNode = trvHier.Nodes.Add(arr(i) & " - " & DataRead.Rows(0)("GrpDesc").ToString)
                                HitNode.ImageIndex = IIf(isWarehouse = True, 1, 0)
                                HitNode.SelectedImageIndex = IIf(isWarehouse = True, 1, 0)
                                HitNode = Nothing
                                'else find parent of node from lastkey value
                            ElseIf i <> 0 Then
                                'decalre variable to hold parent node
                                Dim PNode As TreeNode
                                'get parent node
                                PNode = NodeExists(LastKey, trvHier)
                                If PNode Is Nothing = False Then
                                    'if parent node found, add current node to it
                                    HitNode = PNode.Nodes.Add(arr(i) & " - " & DataRead.Rows(0)("GrpDesc").ToString)
                                    HitNode.ImageIndex = IIf(isWarehouse = True, 1, 0)
                                    HitNode.SelectedImageIndex = IIf(isWarehouse = True, 1, 0)
                                    HitNode = Nothing
                                End If

                            End If

                        End If
                        KeyExist = False
                    Else
                        MsgBox(msg, MsgBoxStyle.Critical, Application.ProductName)
                        DataRead.Dispose()
                        Exit Sub
                    End If
                    'Else
                    '    MsgBox(msg, MsgBoxStyle.Critical, Application.ProductName)

                    '    DataRead.Dispose()
                    '    Exit Sub
                    '    ' End If
                Else
                    'MsgBox(msg, MsgBoxStyle.Critical, Application.ProductName)
                    '
                    '   DataRead.Dispose()
                    '   Exit Sub
                End If

                KeyExist = False
            Next
        Next
        ds.Dispose()
    End Sub

    ' this function checks if a node exists or not based on nodevalue
    ' fullpath indicates wether to seach for node text or node full path
    Private Function NodeExists(ByVal NodeValue As String, _
                                ByVal TreeViewObject As TreeView) As TreeNode

        Dim ndNodes As TreeNodeCollection = Nothing, ndNode As TreeNode = Nothing
        Dim arr() As String
        Dim FPath As String 'full path of the node
        Dim arrBound As Short
        Dim i As Short
        Dim j As Short
        Dim TempPath As String = ""

        'if tree has no nodes then exit
        If TreeViewObject.GetNodeCount(True) = 0 Then
            Return Nothing
        Else
            'Take all nodes of the tree in one collection
            Try
                ndNodes = TreeViewObject.Nodes
            Catch ex As Exception
                GenericExceptionHandler(ex, WhoCalledMe)
            End Try

            ReDim TempNode(TreeViewObject.GetNodeCount(True))
            RecurseNodes(ndNodes) 'traverse all nodes and put them in TempNode array
            NodeNum = 0

            For j = 0 To UBound(TempNode) 'run a loop for all nodes in the array
                If Not TempNode(j) Is Nothing Then
                    ndNode = TempNode(j) 'take node on treenode variable
                    FPath = ndNode.FullPath 'retrieve full path of target node
                    arr = Split(FPath, "\") 'split to separate node text from hier code
                    arrBound = UBound(arr) 'get bound to get number of nodes

                    If arrBound = 0 Then 'if node is parent then just get hier code
                        arr = Split(FPath, " - ")
                        If NodeValue = Trim(arr(0)) Then
                            Return ndNode
                        End If
                    Else  'otherwise run a loop to get hier code for each node
                        For i = 0 To UBound(arr) 'get hiercode for each node and concatenate it
                            If InStr(arr(i), " - ") > 0 Then
                                TempPath = TempPath & "-" & _
                                            Mid$(arr(i), 1, InStr(arr(i), " - ") - 1)
                            End If
                        Next
                        TempPath = Trim(TempPath)
                        'remove first "-" in hierarchy path
                        If Mid(TempPath, 1, 1) = "-" Then TempPath = Mid(TempPath, 2)
                        If NodeValue = TempPath Then
                            Return ndNode
                        Else
                            TempPath = ""
                        End If
                    End If
                End If
            Next
        End If
        Return Nothing
    End Function

    'recursive method to traverse all nodes of a tree
    Private Sub RecurseNodes(ByVal ndNodes As TreeNodeCollection)
        Dim ndNode As TreeNode
        For Each ndNode In ndNodes
            TempNode(NodeNum) = ndNode
            NodeNum = NodeNum + 1
            RecurseNodes(ndNode.Nodes)
        Next
    End Sub

    Private Sub trvHier_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trvHier.KeyDown
        Dim msg As String = ""
        Dim qry As String = ""

        If e.KeyCode = Keys.Delete And Not trvHier.SelectedNode Is Nothing Then
            If trvHier.SelectedNode.Nodes.Count > 0 Then

                msg = "All dependent groups will be deleted. Do you want " & _
                      "to proceed ?"
            Else
                msg = "Are you sure you want to delete this group ?"
            End If
            If MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = _
                              Windows.Forms.DialogResult.Yes Then



                Try
                    objattOrgHier = New attOrgHier
                    '   objattOrgHier.GrpID = GCode
                    objattOrgHier.PKeyCode = GetNodeKey(trvHier.SelectedNode) & "%"
                    If Not objBALOrgHier.Check_Child_Custodian(objattOrgHier.PKeyCode) Then


                        objBALOrgHier.Delete_OrgHier(objattOrgHier)


                        trvHier.Nodes.Remove(trvHier.SelectedNode)
                        MessageBox.Show("Record deleted successfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else

                        ZulMessageBox.ShowMe("CantDelete")

                    End If
                Catch ex As Exception
                    GenericExceptionHandler(ex, WhoCalledMe)
                Finally
                    '    objDAL.CloseConn()
                End Try
            End If
        End If
    End Sub

    Private Function GetNodeKey(ByVal ndNode As TreeNode) As String
        Dim FPath As String
        Dim arr() As String
        Dim arrBound As Short
        Dim i As Integer
        Dim TempPath As String = ""

        FPath = ndNode.FullPath 'retrieve full path of target node
        arr = Split(FPath, "\") 'split to separate node text from hier code
        arrBound = UBound(arr) 'get bound to get number of nodes

        If arrBound = 0 Then 'if node is parent then just get hier code
            arr = Split(FPath, " - ")
            Return arr(0).Trim
        Else  'otherwise run a loop to get hier code for each node
            For i = 0 To UBound(arr) 'get hiercode for each node and concatenate it
                If InStr(arr(i), " - ") > 0 Then
                    TempPath = TempPath & "-" & _
                                Mid$(arr(i), 1, InStr(arr(i), " - ") - 1)
                End If
            Next
            TempPath = Trim(TempPath)
            'remove first "-" in hierarchy path
            If Mid(TempPath, 1, 1) = "-" Then TempPath = Mid(TempPath, 2)

            Return TempPath.Trim
        End If
    End Function

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub trvHier_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim info As TreeViewHitTestInfo = trvHier.HitTest(e.X, e.Y)
        If Not (info.Node Is Nothing) And e.Button = Windows.Forms.MouseButtons.Right Then
            HitNode = info.Node
            '     HitNode.ContextMenuStrip = mnuWH
            '     mnuWH.Show()
            ' If HitNode.ImageIndex = 1 And mnuWH.Items.Count = 1 Then
            '        mnuWH.Items.Add("Unmark As Warehouse")
            '  ElseIf HitNode.ImageIndex = 0 And mnuWH.Items.Count = 2 Then
            '       mnuWH.Items.RemoveAt(1)
            'End If
            trvHier.SelectedNode = HitNode
        Else
            HitNode = Nothing
        End If
    End Sub
    'Danish Dont need it 

    'Private Sub mnuMark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMark.Click
    '    If Not HitNode Is Nothing Then
    '        clsSingle.frmWareHouse = New frmWareHouse
    '        clsSingle.frmWareHouse.WHID = GetWHID(GetNodeKey(HitNode))
    '        clsSingle.frmWareHouse.txtHier.Text = HierName(GetNodeKey(HitNode))
    '        clsSingle.frmWareHouse.txtHier.Tag = GetNodeKey(HitNode)
    '        clsSingle.frmWareHouse.ShowDialog()
    '        If clsSingle.frmWareHouse.isSaved Then
    '            HitNode.ImageIndex = 1
    '            HitNode.SelectedImageIndex = 1
    '            ' HitNode.ImageIndex = IIf(mnuMark.Checked, 1, 0)
    '            'HitNode.SelectedImageIndex = IIf(mnuMark.Checked, 1, 0)
    '        Else
    '            HitNode.ImageIndex = 0
    '            HitNode.SelectedImageIndex = 0
    '        End If
    '    End If
    'End Sub

    'Private Function GetWHID(ByVal HierCode As String) As Long
    '    Dim qry As String
    '    Dim DataRead As IDataReader = Nothing

    '    Try
    '        modDB.OpenDB()
    '    Catch ex As Exception
    '            GenericExceptionHandler(ex, WhoCalledMe)

    '    End Try

    '    qry = "SELECT WHID FROM WAREHOUSE WHERE HierCode='" & HierCode & "'"

    '    Try
    '        DataRead = objDAL.ExecDataReader(qry, CommandType.Text)
    '    Catch ex As Exception
    '        objDAL.CloseConn()
    '                    GenericExceptionHandler(ex, WhoCalledMe)

    '    End Try

    '    If DataRead.Read Then
    '        GetWHID = DataRead("WHID")
    '    Else 'If DataRead.Read Then
    '        GetWHID = GetNextSequence("WAREHOUSE", "WHID")
    '    End If
    '    DataRead.Close()
    'End Function


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim msg As String = ""
        Dim qry As String = ""

        If Not trvHier.SelectedNode Is Nothing Then
            If trvHier.SelectedNode.Nodes.Count > 0 Then

                msg = "All dependent groups will be deleted. Do you want " & _
                      "to proceed ?"
            Else
                msg = "Are you sure you want to delete this group ?"
            End If
            If MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = _
                              Windows.Forms.DialogResult.Yes Then

                Try
                    objattOrgHier = New attOrgHier
                    '   objattOrgHier.GrpID = GCode
                    objattOrgHier.PKeyCode = GetNodeKey(trvHier.SelectedNode) & "%"
                    If Not objBALOrgHier.Check_Child_Custodian(objattOrgHier.PKeyCode) Then


                        objBALOrgHier.Delete_OrgHier(objattOrgHier)


                        trvHier.Nodes.Remove(trvHier.SelectedNode)
                        MessageBox.Show("Record deleted successfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else

                        ZulMessageBox.ShowMe("CantDelete")

                    End If
                Catch ex As Exception
                    GenericExceptionHandler(ex, WhoCalledMe)
                Finally
                    '    objDAL.CloseConn()
                End Try
            End If
        End If
    End Sub

    Private Sub trvHier_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvHier.MouseUp
        If e.Button = MouseButtons.Right Then
            Dim n As TreeNode = Me.trvHier.GetNodeAt(e.X, e.Y)
            If Not n Is Nothing Then
                Me.trvHier.SelectedNode = n
            End If
        End If
    End Sub

    Private Sub trvGrp_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvGrp.MouseUp
        If e.Button = MouseButtons.Right Then
            Dim n As TreeNode = Me.trvGrp.GetNodeAt(e.X, e.Y)
            If Not n Is Nothing Then
                Me.trvGrp.SelectedNode = n
            End If
        End If
    End Sub
End Class