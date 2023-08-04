Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class frmCategory
    Dim objattCategory As attCategory
    Dim objBALCategory As New BALCategory
    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Dim X, Y As Integer
    Private Enum UTypeMode
        AddNew = 0
        AddNewChild = 1
        EditCategory = 2
        DeleteCategory = 3
    End Enum
    Public IsEdit As Boolean = False
    Private WorkingMode As New UTypeMode

    Public Property FormCaption() As String
        Get
            Return Me.Text
        End Get
        Set(ByVal value As String)
            If value <> "Asset Categories" Then
                PictureBox1.Visible = False
            End If
            Me.Text = value
        End Set
    End Property


    Private Sub frmCat_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmCat = Nothing
    End Sub

    Private Sub ContextMenuStrip1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If trv.SelectedNode Is Nothing Then ' When Node is added on Root
            mnuAddNewChild.Visible = False
            DeleteLocationToolStripMenuItem.Visible = False
            mnuEditCat.Visible = False
            mnuNewCategory.Visible = True
            RefeshToolStripMenuItem.Visible = True
        Else
            'Check the system Parameter, and activate adding sub category.
            If trv.SelectedNode.Level + 1 >= SysParam.MaxCategoryLevel Then
                mnuAddNewChild.Visible = False
            Else
                mnuAddNewChild.Visible = True
            End If
            'mnuAddNewChild.Visible = True
            DeleteLocationToolStripMenuItem.Visible = True
            mnuEditCat.Visible = True
            mnuNewCategory.Visible = True
            RefeshToolStripMenuItem.Visible = True
        End If
    End Sub

    Private Sub mnuNewCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewCategory.Click
        'Add Node on Root
        errProv.ClearErrors()
        WorkingMode = UTypeMode.AddNew
        Panel1.Top = (trv.Bottom - trv.Top) / 2 '- 100
        Panel1.Left = (trv.Right - trv.Left) / 2 - 120 ' 60
        Panel1.Visible = True
        Panel1.Tag = 1
        Panel1.BringToFront()
        btnUp.Enabled = False
        btnDwn.Enabled = False
        IsEdit = False
        txtID.Text = objBALCategory.GetRootCatID()
        'txtID.Text = trv.Nodes.Count.ToString + 1
        If HideTreeCodes Then
            txtCode.Visible = False
            lblCode.Visible = False
            txtCode.Text = txtID.Text
        Else
            txtCode.Text = ""
        End If
        txtDesc.Text = ""
        txtCode.Focus()
    End Sub

    Private Sub mnuAddNewChild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAddNewChild.Click
        'Add New Child
        errProv.ClearErrors()

        WorkingMode = UTypeMode.AddNewChild

        Panel1.Top = (trv.Bottom - trv.Top) / 2 '- 100
        Panel1.Left = (trv.Right - trv.Left) / 2 - 120 ' 60
        Panel1.Visible = True
        Panel1.Tag = 1

        Panel1.BringToFront()
        btnUp.Enabled = False
        btnDwn.Enabled = False
        IsEdit = False
        txtID.Text = objBALCategory.GetChildCatID(trv.SelectedNode.Tag)
        'txtID.Text = trv.SelectedNode.Tag & "-" & GetChild_Count(trv.SelectedNode.Tag)
        If HideTreeCodes Then
            txtCode.Visible = False
            lblCode.Visible = False
            txtCode.Text = txtID.Text
        Else
            txtCode.Text = ""
        End If

        txtDesc.Text = ""
        txtCode.Focus()
    End Sub

    Private Sub mnuEditCat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEditCat.Click
        errProv.ClearErrors()
        WorkingMode = UTypeMode.EditCategory
        objattCategory = New attCategory
        objattCategory.AstCatID = trv.SelectedNode.Tag
        Dim dt As DataTable = objBALCategory.GetAll_Category(objattCategory)

        Panel1.Top = (trv.Bottom - trv.Top) / 2 '- 100
        Panel1.Left = (trv.Right - trv.Left) / 2 - 120 ' 60
        Panel1.Visible = True
        Panel1.Tag = 1

        Panel1.BringToFront()
        btnUp.Enabled = False
        btnDwn.Enabled = False
        IsEdit = True
        txtID.Text = trv.SelectedNode.Tag
        If HideTreeCodes Then
            txtCode.Visible = False
            lblCode.Visible = False
        End If
        txtCode.Text = dt.Rows(0)("Code").ToString()
        txtDesc.Text = dt.Rows(0)("AstCatDesc").ToString()
        txtCode.Focus()
    End Sub

    Private Sub DeleteLocationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteLocationToolStripMenuItem.Click
        If Not check_Child_AssetCategory(trv.SelectedNode.Tag) Then
            If MessageBox.Show(" Do you really want to delete?", " ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1).ToString().Equals("Yes") Then
                Delete_TrvNode(trv.SelectedNode)
                trv.SelectedNode.Nodes.Clear()
                trv.Nodes.Remove(trv.SelectedNode)
                ZulMessageBox.ShowMe("Deleted")
            End If
        Else
            ZulMessageBox.ShowMe("CantDelete")
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim SaveSuccess As Boolean = True
        If valProvMain.Validate Then
            If WorkingMode = UTypeMode.AddNew Then ' Adding Node to Root
                txtID.Text = objBALCategory.GetRootCatID()
                If AddNew_Category(txtID.Text, txtDesc.Text, txtCode.Text, 0, "") Then 'the levels start from the first node = 0
                    Dim FNode As New TreeNode("")
                    FNode.Tag = txtID.Text
                    If HideTreeCodes Then
                        FNode.Text = txtDesc.Text
                    Else
                        FNode.Text = txtCode.Text + " - " + txtDesc.Text
                    End If
                    trv.Nodes.Add(FNode)
                    trv.SelectedNode = FNode
                    txtCode.Text = ""
                    txtDesc.Text = ""
                    txtID.Text = ""
                    SaveSuccess = True
                Else
                    SaveSuccess = False
                End If
            ElseIf WorkingMode = UTypeMode.AddNewChild Then
                txtID.Text = objBALCategory.GetChildCatID(trv.SelectedNode.Tag)
                If AddNew_Category(txtID.Text, txtDesc.Text, txtCode.Text, trv.SelectedNode.Level + 1, trv.SelectedNode.Tag) Then 'trv.SelectedNode.Level + 1 is parent level
                    Dim FNode As New TreeNode("")
                    FNode.Tag = txtID.Text
                    If HideTreeCodes Then
                        FNode.Text = txtDesc.Text
                    Else
                        FNode.Text = txtCode.Text + " - " + txtDesc.Text
                    End If
                    trv.SelectedNode.Nodes.Add(FNode)
                    trv.SelectedNode = FNode
                    txtCode.Text = ""
                    txtDesc.Text = ""
                    txtID.Text = ""
                    SaveSuccess = True
                Else
                    SaveSuccess = False
                End If
            ElseIf WorkingMode = UTypeMode.EditCategory Then
                Dim ParentID As String = ""
                If trv.SelectedNode.Level > 0 Then
                    ParentID = trv.SelectedNode.Parent.Tag
                End If
                If Update_Category(txtID.Text, txtDesc.Text, txtCode.Text, trv.SelectedNode.Level, ParentID) Then
                    trv.SelectedNode.Tag = txtID.Text
                    If HideTreeCodes Then
                        trv.SelectedNode.Text = txtDesc.Text
                    Else
                        trv.SelectedNode.Text = txtCode.Text + " - " + txtDesc.Text
                    End If
                    txtCode.Text = ""
                    txtDesc.Text = ""
                    txtID.Text = ""
                    SaveSuccess = True
                Else
                    SaveSuccess = False
                End If
            End If

            If SaveSuccess Then
                IsEdit = False
                Panel1.Visible = False
                Panel1.Tag = 0
                trv.Focus()
                'trv.ExpandAll()
            End If
        End If
    End Sub

#Region "Method"
    Private Sub Delete_TrvNode(ByVal SelNode As TreeNode)
        Try
            Delete_Category(SelNode.Tag)
            Dim aNode As TreeNode
            For Each aNode In SelNode.Nodes
                Delete_TrvNode(aNode)
            Next
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub AddDepPolicyForCat(ByVal CatID As String)
        'add asset book for the company if it's tracking or inventory edition.
        If AppConfig.AppEdition = ApplicationEditions.Inventory Or AppConfig.AppEdition = ApplicationEditions.Tracking Then
            Dim objattDepPolicy As New attDepPolicy
            Dim objBALDepPolicy As New BALDepPolicy

            objattDepPolicy.AstCatID = CatID
            objattDepPolicy.DepCode = 1
            objattDepPolicy.SalvageValue = 0
            objattDepPolicy.SalvageYear = 1
            objattDepPolicy.IsSalvageValuePercent = False

            objBALDepPolicy.Insert_DepPolicy(objattDepPolicy)
        End If
    End Sub

    Private Function AddNew_Category(ByVal Id As String, ByVal Name As String, ByVal Code As String, ByVal level As Integer, ByVal ParentID As String) As Boolean
        Try
            If objBALCategory.CheckCatNameFound(Name, ParentID, Id) > 0 Then
                errProv.SetError(txtDesc, My.MessagesResource.Messages.CatDescAlreadyExist)
                Return False
            ElseIf objBALCategory.CheckCatCodeFound(Code, ParentID, Id) > 0 Then
                errProv.SetError(txtCode, My.MessagesResource.Messages.CatCodeAlreadyExist)
                Return False
            Else
                objattCategory = New attCategory
                objattCategory.AstCatDesc = Name.Trim
                objattCategory.AstCatID = Id.Trim
                objattCategory.Code = Code.Trim
                objattCategory.catLevel = level
                If objBALCategory.Insert_Category(objattCategory) Then
                    AddDepPolicyForCat(Id)
                    ZulMessageBox.ShowMe("Saved")
                    Return True
                Else
                    Return False
                End If
                errProv.ClearErrors()
                Return True
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function
    Private Sub Delete_Category(ByVal Id As String)
        Try
            objattCategory = New attCategory
            objattCategory.AstCatID = RemoveUnnecessaryChars(Id)
            objBALCategory.Delete_Category(objattCategory)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Function Update_Category(ByVal Id As String, ByVal Name As String, ByVal Code As String, ByVal level As Integer, ByVal ParentID As String) As Boolean
        Try
            If objBALCategory.CheckCatNameFound(Name, ParentID, Id) > 0 Then
                errProv.SetError(txtDesc, My.MessagesResource.Messages.CatDescAlreadyExist)
                Return False
            ElseIf objBALCategory.CheckCatCodeFound(Code, ParentID, Id) > 0 Then
                errProv.SetError(txtCode, My.MessagesResource.Messages.CatCodeAlreadyExist)
                Return False
            Else
                objattCategory = New attCategory
                objattCategory.AstCatDesc = Name.Trim
                objattCategory.AstCatID = Id.Trim
                objattCategory.Code = Code.Trim
                objattCategory.catLevel = level
                If objBALCategory.Update_Category(objattCategory) Then
                    ZulMessageBox.ShowMe("Saved")
                    Return True
                Else
                    Return False
                End If
                errProv.ClearErrors()
                Return True
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function

    Dim ht As New Hashtable
    Private Sub Get_Category()
        trv.BeginUpdate()
        Try
            Dim ds As New DataTable
            Dim rcount As Integer
            objattCategory = New attCategory
            ds = objBALCategory.GetAll_Category(objattCategory)
            If ds Is Nothing = False Then
                rcount = ds.Rows.Count
                If rcount > 0 Then
                    For Each dr As DataRow In ds.Rows
                        'Create_Tree(dr("AstCatID"), dr("AstCatDesc"), dr("Code"))
                        Create_TreeNode(dr("AstCatID"), dr("AstCatDesc"), dr("Code"), trv)
                    Next
                End If
            End If
            trv.EndUpdate()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally
            ht.Clear()
        End Try

    End Sub

    Public Sub Create_TreeNode(ByVal NID As String, ByVal NDesc As String, ByVal NCode As String, ByVal trv As TreeView)
        Dim NNode As New TreeNode
        If HideTreeCodes Then
            NNode.Text = NDesc
        Else
            NNode.Text = NCode + " - " + NDesc
        End If

        If NID.IndexOf("-") < 0 Then
            NNode.Tag = NID
            trv.Nodes.Add(NNode)
            ht.Add(NID, NNode)
        Else
            Dim ParentID As String = NID.Substring(0, NID.LastIndexOf("-"))
            NNode.Tag = NID
            Dim PNode As TreeNode = CType(ht.Item(ParentID), TreeNode)

            If PNode IsNot Nothing Then
                PNode.Nodes.Add(NNode)
                ht.Add(NID, NNode)
            End If
        End If
    End Sub

#End Region

    Private Sub frmCat_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyValue = Keys.Escape And Panel1.Tag = 1) Then
            btnHidePanel_Click(sender, e)
        ElseIf (e.KeyValue = Keys.Escape) Then
            btnExit_Click(sender, e)
        ElseIf (e.KeyValue = Keys.Enter And Panel1.Tag = 1) Then
            btnSave_Click(sender, e)
        End If
    End Sub

    Private Sub frmLocationNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        Get_Category()
        If trv.Nodes.Count > 0 Then
            trv.SelectedNode = trv.Nodes(0)
            trv.Nodes(0).Expand()
        End If
        valProvMain.SetValidationRule(txtID, valRulenotEmpty)
        valProvMain.SetValidationRule(txtCode, valRulenotEmpty)
        valProvMain.SetValidationRule(txtDesc, valRulenotEmpty)
    End Sub

    Private Sub RefeshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefeshToolStripMenuItem.Click
        btnHidePanel_Click(sender, e)
        trv.Nodes.Clear()
        Get_Category()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmCat = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        If trv.SelectedNode Is Nothing = False Then
            Dim nodeID As String = trv.SelectedNode.Tag
            objBALCategory.Move_Up(trv.SelectedNode.Tag, trv.SelectedNode.PrevNode.Tag)
            trv.Nodes.Clear()
            Get_Category()
            SelectTreeNodeByTag(trv, trv.Nodes, nodeID)
            trv.SelectedNode.Expand()
            trv.Focus()
        End If
    End Sub

    Private Sub btnDwn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDwn.Click
        If trv.SelectedNode Is Nothing = False Then
            Dim nodeID As String = trv.SelectedNode.Tag
            objBALCategory.Move_Down(trv.SelectedNode.Tag, trv.SelectedNode.NextNode.Tag)
            trv.Nodes.Clear()
            Get_Category()
            SelectTreeNodeByTag(trv, trv.Nodes, nodeID)
            trv.SelectedNode.Expand()
            trv.Focus()
        End If
    End Sub

    Private Sub btnHidePanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHidePanel.Click
        txtCode.Text = ""
        txtDesc.Text = ""
        txtID.Text = ""
        IsEdit = False


        valProvMain.RemoveControlError(txtCode)
        valProvMain.RemoveControlError(txtDesc)
        valProvMain.RemoveControlError(txtID)

        Panel1.Visible = False
        Panel1.Tag = 0
        trv.Focus()
    End Sub

    Private Sub trv_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trv.AfterSelect
        If trv.SelectedNode Is Nothing = False Then
            If trv.SelectedNode.PrevNode Is Nothing = False Then
                btnUp.Enabled = True
            Else
                btnUp.Enabled = False
            End If

            If trv.SelectedNode.NextNode Is Nothing = False Then
                btnDwn.Enabled = True
            Else
                btnDwn.Enabled = False
            End If

        End If
    End Sub

    'Private Sub trv_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trv.MouseUp
    '    If e.Button = MouseButtons.Right Then
    '        Dim n As TreeNode = Me.trv.GetNodeAt(e.X, e.Y)
    '        If Not n Is Nothing Then
    '            Me.trv.SelectedNode = n
    '        End If
    '    End If
    'End Sub
    Private Sub trv_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trv.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim n As TreeNode = Me.trv.GetNodeAt(e.X, e.Y)
            If Not n Is Nothing Then
                Me.trv.SelectedNode = n
            End If
        End If
    End Sub
    Private Sub frmCat_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If trv.Nodes.Count > 0 Then
            trv.SelectedNode = trv.Nodes(0)
        End If
        trv.Focus()
    End Sub

    Private Sub mnuCollapse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCollapse.Click
        If trv.SelectedNode Is Nothing = False Then
            If trv.SelectedNode.Parent Is Nothing = False Then
                trv.SelectedNode.Parent.Collapse()
            End If
        End If
    End Sub

    Private Sub trv_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trv.KeyDown
        If trv.SelectedNode Is Nothing = False Then
            If e.KeyCode = Keys.Enter Then
                mnuEditCat_Click(sender, e)
            End If
        End If
    End Sub
End Class
