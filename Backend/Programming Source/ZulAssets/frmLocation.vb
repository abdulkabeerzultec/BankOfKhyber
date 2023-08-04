Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class frmLocation
    Private Enum UTypeMode
        AddNew = 0
        AddNewChild = 1
        EditLocation = 2
        DeleteLocation = 3
    End Enum

    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider

    Public addNew_fromBulk As Integer = 0
    Public frmAstBulk As frmPOTrans
    Dim objattLocation As attLocation
    Dim objBALLocation As New BALLocation
    Dim X, Y As Integer
    Public IsEdit As Boolean = False
    Private WorkingMode As New UTypeMode


    Private Sub frmLocation_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If addNew_fromBulk = 1 Then
            frmAstBulk.BringToFront()
        End If
    End Sub

    Private Sub frmLocation_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmLocation = Nothing
    End Sub

    Private Sub ContextMenuStrip1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If trv.SelectedNode Is Nothing Then ' When Node is added on Root
            mnuNewChildLoc.Visible = False
            DeleteLocationToolStripMenuItem.Visible = False
            mnuEditLocation.Visible = False
            mnuNewLoc.Visible = True
            RefeshToolStripMenuItem.Visible = True
        Else
            'Check the system Parameter, and activate adding sub location.
            If trv.SelectedNode.Level + 1 >= SysParam.MaxLocationLevel Then
                mnuNewChildLoc.Visible = False
            Else
                mnuNewChildLoc.Visible = True
            End If
            'mnuNewChildLoc.Visible = True
            DeleteLocationToolStripMenuItem.Visible = True
            mnuEditLocation.Visible = True
            mnuNewLoc.Visible = True
            RefeshToolStripMenuItem.Visible = True
        End If

    End Sub

    Private Sub mnuNewLoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewLoc.Click
        'Add Node on Root
        errProv.ClearErrors()

        WorkingMode = UTypeMode.AddNew
        Panel1.Top = (trv.Bottom - trv.Top) / 2 '- 100
        Panel1.Left = (trv.Right - trv.Left) / 2 - 120 ' 60
        Panel1.Visible = True
        Panel1.BringToFront()
        Panel1.Tag = 1

        btnUp.Enabled = False
        btnDwn.Enabled = False
        IsEdit = False
        txtID.Text = objBALLocation.GetRootLocID()
        'txtID.Text = trv.Nodes.Count.ToString() + 1
        If HideTreeCodes Then
            txtCode.Visible = False
            lblCode.Visible = False
            txtCode.Text = txtID.Text
        Else
            txtCode.Text = ""
        End If
        txtDesc.Text = ""
        cmbComp.SelectedValue = ""
        cmbComp.SelectedText = ""
        txtCode.Focus()
    End Sub

    Private Sub mnuNewChildLoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewChildLoc.Click
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
        txtID.Text = objBALLocation.GetChildLocID(trv.SelectedNode.Tag)
        'txtID.Text = trv.SelectedNode.Tag & "-" & GetChild_Count(trv.SelectedNode.Tag)
        If HideTreeCodes Then
            txtCode.Visible = False
            lblCode.Visible = False
            txtCode.Text = txtID.Text
        Else
            txtCode.Text = ""
        End If
        txtDesc.Text = ""
        cmbComp.SelectedValue = ""
        cmbComp.SelectedText = ""
        txtCode.Focus()
    End Sub



    Private Sub mnuEditLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEditLocation.Click
        errProv.ClearErrors()
        WorkingMode = UTypeMode.EditLocation
        objattLocation = New attLocation
        objattLocation.HierCode = trv.SelectedNode.Tag
        Dim dt As DataTable = objBALLocation.GetAll_Locations(objattLocation)

        'Dim str() As String
        'str = trv.SelectedNode.Text.Split("-")
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
        txtDesc.Text = dt.Rows(0)("LocDesc").ToString()
        If Not dt.Rows(0).IsNull("CompanyID") Then
            cmbComp.SelectedValue = dt.Rows(0)("CompanyID")
        End If

        If Not dt.Rows(0).IsNull("CompanyName") Then
            cmbComp.SelectedText = dt.Rows(0)("CompanyName")
        End If


        txtCode.Focus()
    End Sub

    Private Sub DeleteLocationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteLocationToolStripMenuItem.Click
        If Not check_Child_AssetDetail(trv.SelectedNode.Tag, 11) Then
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
        Dim CompanyID As Integer = -1
        If Not String.IsNullOrEmpty(cmbComp.SelectedValue) Then
            CompanyID = cmbComp.SelectedValue
        Else
            CompanyID = -1
        End If

        If valProvMain.Validate Then
            If WorkingMode = UTypeMode.AddNew Then ' Adding Node to Root
                txtID.Text = objBALLocation.GetRootLocID()
                If AddNew_Location(txtID.Text, txtDesc.Text, txtCode.Text, 0, "", CompanyID) Then
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
                    cmbComp.SelectedValue = ""
                    cmbComp.SelectedText = ""
                    txtID.Text = ""
                    SaveSuccess = True
                Else
                    SaveSuccess = False
                End If
            ElseIf WorkingMode = UTypeMode.AddNewChild Then
                txtID.Text = objBALLocation.GetChildLocID(trv.SelectedNode.Tag)
                If AddNew_Location(txtID.Text, txtDesc.Text, txtCode.Text, trv.SelectedNode.Level + 1, trv.SelectedNode.Tag, CompanyID) Then
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
                    cmbComp.SelectedValue = ""
                    cmbComp.SelectedText = ""
                    txtID.Text = ""
                    SaveSuccess = True
                Else
                    SaveSuccess = False
                End If
            ElseIf WorkingMode = UTypeMode.EditLocation Then
                Dim ParentID As String = ""
                If trv.SelectedNode.Level > 0 Then
                    ParentID = trv.SelectedNode.Parent.Tag
                End If
                If Update_Location(txtID.Text, txtDesc.Text, txtCode.Text, trv.SelectedNode.Level, ParentID, CompanyID) Then
                    trv.SelectedNode.Tag = txtID.Text
                    If HideTreeCodes Then
                        trv.SelectedNode.Text = txtDesc.Text
                    Else
                        trv.SelectedNode.Text = txtCode.Text + " - " + txtDesc.Text
                    End If
                    txtCode.Text = ""
                    txtDesc.Text = ""
                    cmbComp.SelectedValue = ""
                    cmbComp.SelectedText = ""
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
            Delete_Location(SelNode.Tag)
            Dim aNode As TreeNode
            For Each aNode In SelNode.Nodes
                Delete_TrvNode(aNode)
            Next
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub


    Private Function AddNew_Location(ByVal Id As String, ByVal Name As String, ByVal Code As String, ByVal level As Integer, ByVal ParentID As String, ByVal CompanyID As Integer) As Boolean
        Try
            If objBALLocation.CheckLocNameFound(Name, ParentID, Id) > 0 Then
                errProv.SetError(txtDesc, My.MessagesResource.Messages.LocDescAlreadyExist)
                Return False
            ElseIf objBALLocation.CheckLocCodeFound(Code, ParentID, Id) > 0 Then
                errProv.SetError(txtCode, My.MessagesResource.Messages.LocCodeAlreadyExist)
                Return False
            Else
                objattLocation = New attLocation
                objattLocation.Description = Name.Trim
                objattLocation.HierCode = Id.Trim
                objattLocation.Code = Code.Trim
                objattLocation.locLevel = level
                objattLocation.CompanyID = CompanyID
                If objBALLocation.Insert_Location(objattLocation) Then
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
    Private Sub Delete_Location(ByVal Id As String)
        Try
            objattLocation = New attLocation
            objattLocation.HierCode = Id
            objBALLocation.Delete_Location(objattLocation)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Function Update_Location(ByVal Id As String, ByVal Name As String, ByVal Code As String, ByVal level As Integer, ByVal ParentID As String, ByVal CompanyID As Integer) As Boolean
        Try
            If objBALLocation.CheckLocNameFound(Name, ParentID, Id) > 0 Then
                errProv.SetError(txtDesc, My.MessagesResource.Messages.LocDescAlreadyExist)
                Return False
            ElseIf objBALLocation.CheckLocCodeFound(Code, ParentID, Id) > 0 Then
                errProv.SetError(txtCode, My.MessagesResource.Messages.LocCodeAlreadyExist)
                Return False
            Else

                objattLocation = New attLocation
                objattLocation.Description = Name.Trim
                objattLocation.HierCode = Id.Trim
                objattLocation.Code = Code.Trim
                objattLocation.locLevel = level
                objattLocation.CompanyID = CompanyID
                If objBALLocation.Update_Location(objattLocation) Then
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

    Private Sub Get_Location()
        trv.BeginUpdate()
        Try
            Dim ds As New DataTable
            Dim rcount As Integer
            objattLocation = New attLocation
            ds = objBALLocation.GetComboLocations(objattLocation)
            If ds Is Nothing = False Then
                rcount = ds.Rows.Count
                If rcount > 0 Then
                    For Each dr As DataRow In ds.Rows
                        ' Create_Tree(dr("LocID"), dr("LocDesc"), dr("Code"))
                        Create_TreeNode(dr("LocID"), dr("LocDesc"), dr("Code"), trv)

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

    Private Sub frmLocationNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Get_Location()
        If trv.Nodes.Count > 0 Then
            trv.SelectedNode = trv.Nodes(0)
            trv.Nodes(0).Expand()
        End If
        valProvMain.SetValidationRule(txtID, valRulenotEmpty)
        valProvMain.SetValidationRule(txtCode, valRulenotEmpty)
        valProvMain.SetValidationRule(txtDesc, valRulenotEmpty)
    End Sub

    Private Sub RefeshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefeshToolStripMenuItem.Click
        txtCode.Text = ""
        txtDesc.Text = ""
        txtID.Text = ""
        cmbComp.SelectedValue = ""
        cmbComp.SelectedText = ""
        IsEdit = False
        Panel1.Visible = False
        trv.Nodes.Clear()
        Get_Location()
    End Sub


    Private Sub btnHidePanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHidePanel.Click
        txtCode.Text = ""
        txtDesc.Text = ""
        txtID.Text = ""
        cmbComp.SelectedValue = ""
        cmbComp.SelectedText = ""
        IsEdit = False

        valProvMain.RemoveControlError(txtCode)
        valProvMain.RemoveControlError(txtDesc)
        valProvMain.RemoveControlError(txtID)

        Panel1.Visible = False
        Panel1.Tag = 0
        trv.Focus()
    End Sub


    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        If trv.SelectedNode Is Nothing = False Then
            Dim nodeID As String = trv.SelectedNode.Tag
            objBALLocation.Move_Up(trv.SelectedNode.Tag, trv.SelectedNode.PrevNode.Tag)
            trv.Nodes.Clear()
            Get_Location()
            SelectTreeNodeByTag(trv, trv.Nodes, nodeID)
            trv.SelectedNode.Expand()
            trv.Focus()
        End If
    End Sub

    Private Sub btnDwn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDwn.Click
        If trv.SelectedNode Is Nothing = False Then
            Dim nodeID As String = trv.SelectedNode.Tag
            objBALLocation.Move_Down(trv.SelectedNode.Tag, trv.SelectedNode.NextNode.Tag)
            trv.Nodes.Clear()
            Get_Location()
            SelectTreeNodeByTag(trv, trv.Nodes, nodeID)
            trv.SelectedNode.Expand()
            trv.Focus()
        End If
    End Sub


    Private Sub txtDesc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDesc.KeyDown, txtCode.KeyDown, txtID.KeyDown
        If e.KeyData = Keys.Escape Then
            txtCode.Text = ""
            txtDesc.Text = ""
            txtID.Text = ""
            IsEdit = False
            Panel1.Visible = False
            cmbComp.SelectedValue = ""
            cmbComp.SelectedText = ""
        End If
    End Sub



    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmLocation = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
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

    Private Sub frmLocation_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyValue = Keys.Escape And CInt(Panel1.Tag) = 1) Then
            btnHidePanel_Click(sender, e)
        ElseIf (e.KeyValue = Keys.Escape) Then
            btnExit_Click(sender, e)
        ElseIf (e.KeyValue = Keys.Enter And CInt(Panel1.Tag) = 1) Then
            btnSave_Click(sender, e)
        End If
    End Sub

    Private Sub mnuCollapse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCollapse.Click
        If trv.SelectedNode Is Nothing = False Then
            If trv.SelectedNode.Parent Is Nothing = False Then
                trv.SelectedNode.Parent.Collapse()
            End If
        End If
    End Sub


    Private Sub trv_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trv.KeyDown
        If trv.SelectedNode Is Nothing = False Then
            If e.KeyCode = Keys.Enter Then
                mnuEditLocation_Click(sender, e)
            End If
        End If
    End Sub

    Private Sub trv_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trv.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim n As TreeNode = Me.trv.GetNodeAt(e.X, e.Y)
            If Not n Is Nothing Then
                Me.trv.SelectedNode = n
            End If
        End If
    End Sub

    Private Sub cmbComp_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbComp.LovBtnClick
        Try
            cmbComp.ValueMember = "CompanyID"
            cmbComp.DisplayMember = "CompanyName"
            Dim objBALCompany As New BALCompany
            cmbComp.DataSource = objBALCompany.GetAllData_GetCombo(New attcompany)
            cmbComp.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
End Class
