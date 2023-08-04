Imports System.Windows.Forms
Imports System.Drawing
Imports clsData.DataAccess
Imports clsData.EnumProviders

Public Class frmTree

    Dim TempNode() As TreeNode  'variable to temporarily hold nodes of a tree
    Dim NodeNum As Short = 0
    Dim CurrNode As TreeNode

    Friend objTree As clsTree
    Friend BuddyControl As Control
    Friend SelectOnlyLastNode As Boolean
    Friend _DBType As Byte
    Friend _DBUName As String       'User name for database
    Friend _DBPass As String        'Password for database
    Friend _DBServer As String      'Server on which database is running
    Friend _DBName As String        'Name of database (oracle or sql server)
    Friend _DBPath As String        'Path of microsoft access database
    Private objDAL As New clsData.DataAccess
    Dim trvDisposed As Boolean = False

    Private Sub OpenDB()

        Select Case _DBType
            Case 0
                objDAL.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                                          "Data Source=" & _DBPath & _
                                          ";Jet OLEDB:Database Password=" & _DBPass & ";"

                objDAL.Provider = clsData.EnumProviders.OLEDB
                objDAL.OpenConn()
            Case 1
                objDAL.ConnectionString = "Data Source=" & _DBServer & ";User Id=" & _DBUName & ";Password=" & _DBPass & ""

                objDAL.Provider = clsData.EnumProviders.OracleClient
                objDAL.OpenConn()
            Case 2
                objDAL.ConnectionString = "server=" & _DBServer & _
                                          ";uid=" & _DBUName & ";pwd=" & _
                                          _DBPass & ";database=" & _
                                          _DBName & ";Connect Timeout=100"

                objDAL.Provider = clsData.EnumProviders.SQLClient
                objDAL.OpenConn()
        End Select
    End Sub

    Private Sub FillHier()
        Dim DataRead As IDataReader = Nothing
        Dim ds As DataSet
        Dim qry As String
        Dim HStr As String
        Dim arr() As String
        Dim i As Long
        Dim j As Long
        Dim CurKey As String
        Dim LastKey As String
        Dim KeyExist As Boolean
        Dim msg As String

        KeyExist = False

        Try
            OpenDB()    'open database conenction
        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Sub
        End Try

        'open ORGHIER to fill hierarchy tree
        qry = "SELECT HierCode, GrpID FROM ORGHIER"

        'execute query and fill dataset
        Try
            ds = objDAL.ExecDataSet(qry, CommandType.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Sub
        Finally
            objDAL.CloseConn()
        End Try

        For j = 0 To ds.Tables(0).Rows.Count - 1
            HStr = ds.Tables(0).Rows(j).Item("HierCode")
            arr = Split(HStr, "-")

            CurKey = ""
            LastKey = ""

            For i = 0 To UBound(arr)
                CurKey = CurKey & "-" & arr(i)
                If i <> 0 Then LastKey = LastKey & "-" & arr(i - 1)

                'current hierarchy
                If Mid$(CurKey, 1, 1) = "-" Then CurKey = Mid$(CurKey, 2)
                'last hierarchy. it will be parent for next hierarchy
                If Mid$(LastKey, 1, 1) = "-" Then LastKey = Mid$(LastKey, 2)

                'check if current node already exists
                If Not NodeExists(CurKey, trvHier) Is Nothing Then
                    KeyExist = True
                    ds.Dispose()
                    objDAL.CloseConn()
                End If

                If Not KeyExist Then 'if node is not already present

                    Try
                        OpenDB()    'open database conenction
                    Catch ex As Exception
                        MsgBox(ex.Message)
                        ds.Dispose()
                        objDAL.CloseConn()
                        Exit Sub
                    End Try

                    'get group description
                    qry = "SELECT GrpDesc FROM COMPGROUPS WHERE GrpID='" & _
                           arr(i) & "'"

                    'execute query 
                    Try
                        DataRead = objDAL.ExecDataReader(qry, CommandType.Text)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                        objDAL.CloseConn()
                        ds.Dispose()
                        Exit Sub
                    End Try

                    msg = "A group required to fill the hierarcy is missing" & _
                          Environment.NewLine & Environment.NewLine & "Group: " & arr(i)

                    'if group was not found, give message
                    If Not DataRead.Read Then
                        MsgBox(msg, MsgBoxStyle.Critical, Application.ProductName)
                        DataRead.Close()
                        ds.Dispose()
                        Exit Sub
                    End If

                    If i = 0 Then 'if it is first node, add it as parent 
                        trvHier.Nodes.Add(arr(i) & " - " & DataRead("GrpDesc"))
                        'else find parent of node from lastkey value
                    ElseIf i <> 0 Then
                        'decalre variable to hold parent node
                        Dim PNode As TreeNode
                        'get parent node
                        PNode = NodeExists(LastKey, trvHier)

                        'if parent node found, add current node to it
                        PNode.Nodes.Add(arr(i) & " - " & DataRead("GrpDesc"))
                    End If
                    DataRead.Close()
                End If
                KeyExist = False
            Next
            DataRead.Close()
        Next

        ds.Dispose()

    End Sub

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
        End If

        'Take all nodes of the tree in one collection
        Try
            ndNodes = TreeViewObject.Nodes
        Catch ex As Exception
            MsgBox(ex.Message)
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

    Private Sub frmTree_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        objTree.frm.Dispose()
        trvDisposed = True
        Me.Dispose()
    End Sub

    Private Sub frmTree_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim rec As Rectangle

        FillHier()
        trvHier.ExpandAll()
        trvHier.CollapseAll()

        rec = Screen.PrimaryScreen.WorkingArea
        Me.Left = Get_Form_Left(BuddyControl) + 10
        Me.Top = Get_Form_Top(BuddyControl) + BuddyControl.Height + 65


        If (Me.Width + Me.Left) > rec.Width Then
            Me.Left = Me.Left - (Me.Width + Me.Left - rec.Width) - 10
        End If

        If Me.Left < 0 Then
            Me.Left = 10
        End If


        If (Me.Height + Me.Top) > rec.Height Then
            Me.Top = Get_Form_Top(BuddyControl) - Me.Height + 60
            '  Me.Top = Me.Top - (Me.Height + Me.Top - rec.Height) - 10
        End If
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

    Private Sub trvHier_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvHier.DoubleClick
        If Not CurrNode Is Nothing Then
            If SelectOnlyLastNode Then
                If CurrNode.Nodes.Count <= 0 Then
                    BuddyControl.Text = GetNodeVal(CurrNode)
                    BuddyControl.Tag = GetNodeKey(CurrNode)
                    trvDisposed = True
                    Me.Dispose()
                End If
            Else
                BuddyControl.Text = GetNodeVal(CurrNode)
                BuddyControl.Tag = GetNodeKey(CurrNode)
                trvDisposed = True
                Me.Dispose()
            End If

        Else
            trvDisposed = True
            Me.Dispose()
        End If
    End Sub

    Private Sub trvHier_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trvHier.KeyDown

        If e.KeyData = Keys.Enter Then
            If SelectOnlyLastNode Then
                If CurrNode.Nodes.Count <= 0 Then
                    BuddyControl.Text = GetNodeVal(CurrNode)
                    BuddyControl.Tag = GetNodeKey(CurrNode)
                    trvDisposed = True
                    Me.Dispose()
                End If
            Else
                BuddyControl.Text = GetNodeVal(CurrNode)
                BuddyControl.Tag = GetNodeKey(CurrNode)
                trvDisposed = True
                Me.Dispose()
            End If
        ElseIf e.KeyData = Keys.Escape Then
            trvDisposed = True
            Me.Dispose()
        End If
    End Sub

    Private Sub trvHier_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvHier.AfterSelect
        CurrNode = e.Node
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

    Private Function GetNodeVal(ByVal ndNode As TreeNode) As String

        Dim FPath As String
        Dim arr() As String
        Dim arr1() As String
        Dim arrBound As Short
        Dim i As Integer
        Dim TempPath As String = ""

        FPath = ndNode.FullPath 'retrieve full path of target node
        arr = Split(FPath, "\") 'split to separate node text from hier code
        arrBound = UBound(arr) 'get bound to get number of nodes

        If arrBound = 0 Then 'if node is parent then just get hier code
            arr = Split(FPath, " - ")
            Return arr(1).Trim
        Else  'otherwise run a loop to get hier code for each node
            For i = 0 To UBound(arr) 'get hiercode for each node and concatenate it
                If InStr(arr(i), " - ") > 0 Then
                    arr1 = Split(arr(i), " - ")
                    TempPath = TempPath & "-" & arr1(1)
                End If
            Next
            TempPath = Trim(TempPath)
            'remove first "-" in hierarchy path
            If Mid(TempPath, 1, 1) = "-" Then TempPath = Mid(TempPath, 2)

            Return TempPath.Trim
        End If
    End Function

    Private Sub trvHier_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvHier.MouseUp
        If e.Button = MouseButtons.Right Then
            If Not trvDisposed Then

                Dim n As TreeNode = Me.trvHier.GetNodeAt(e.X, e.Y)
                If Not n Is Nothing Then
                    Me.trvHier.SelectedNode = n
                End If
            End If

        End If
    End Sub
End Class