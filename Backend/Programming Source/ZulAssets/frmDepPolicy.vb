Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports DevExpress.XtraEditors
Public Class frmDepPolicy
    Dim objattCategory As attCategory
    Dim objBALCategory As New BALCategory
    Dim objattDepMeth As attDepreciationMethod
    Dim objBALDepMeth As New BALDepreciationMethod
    Dim objattDepPolicy As attDepPolicy
    Dim objBALDepPolicy As New BALDepPolicy

    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    Dim SelNode As TreeNode
    Dim isEdit As Boolean
    Dim ht As New Hashtable

    Private Sub Get_Category()
        Try
            Dim ds As New DataTable
            Dim rcount As Integer
            objattCategory = New attCategory
            ds = objBALCategory.GetAll_Category(objattCategory)
            If ds Is Nothing = 0 Then
                rcount = ds.Rows.Count
                If rcount > 0 Then
                    For Each dr As DataRow In ds.Rows
                        Create_TreeNode(dr("AstCatID"), dr("AstCatDesc"), trv)
                    Next
                End If
            End If
            trv.CollapseAll()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
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
    '        GenericExceptionHandler(ex, WhoCalledMe)
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
    '        GenericExceptionHandler(ex, WhoCalledMe)
    '    End Try
    'End Sub

    Private Sub frmDepPolicy_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmDepPolicy = Nothing
    End Sub

    Private Sub frmDepPolicy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        valProvMain.SetValidationRule(txtCategory, valRulenotEmpty)
        valProvMain.SetValidationRule(txtSalYr, valRulenotEmpty)
        valProvMain.SetValidationRule(txtSalPercent, valRulenotEmpty)
        valProvMain.SetValidationRule(txtSalMonth, valRulenotEmpty)
        valProvMain.SetValidationRule(txtSalVal, valRulenotEmpty)


        'txtSalVal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        'txtSalVal.Properties.Mask.EditMask = "[01]"
        txtSalMonth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        txtSalMonth.Properties.Mask.EditMask = "[0-9]|10|11"

        txtSalYr.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        txtSalYr.Properties.Mask.EditMask = "([0-9][0-9]?)|100"

        txtSalPercent.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        txtSalPercent.Properties.Mask.EditMask = "\d+(\R.\d{0,2})?"


        '
        Get_Category()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmDepPolicy = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

#Region "Method"

    

    Private Sub AddNew_DepPolicy()
        Try
            objattDepPolicy = New ZulAssetsDAL.ZulAssetsDAL.attDepPolicy

            objattDepPolicy.AstCatID = txtCategory.Tag
            objattDepPolicy.DepCode = 1
            If rdoFixedValue.Checked Then
                objattDepPolicy.SalvageValue = txtSalVal.Text
            Else
                objattDepPolicy.SalvageValue = txtSalValPercent.Text
            End If

            objattDepPolicy.SalvageYear = txtSalYr.Text
            objattDepPolicy.IsSalvageValuePercent = rdoPercentageValue.Checked
            objBALDepPolicy.Insert_DepPolicy(objattDepPolicy)
            ZulMessageBox.ShowMe("Saved")
            ClearFrame(GroupBox1)


        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Function Get_DepPolicy(ByVal _id As String) As Boolean
        Try
            Dim ds As New DataTable

            txtSalVal.Text = ""
            txtSalYr.Text = ""
            txtSalValPercent.Text = ""
            rdoFixedValue.Checked = True
            errProv.SetError(txtSalValPercent, "")

            objattDepPolicy = New ZulAssetsDAL.ZulAssetsDAL.attDepPolicy
            objattDepPolicy.AstCatID = _id
            ds = objBALDepPolicy.GetAll_DepPolicy(objattDepPolicy)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Dim percentVal As String = ds.Rows(0)("SalvagePercent").ToString()
                    txtSalYr.Text = ds.Rows(0)("SalvageYear").ToString()
                    txtSalMonth.Text = ds.Rows(0)("SalvageMonth").ToString()
                    txtSalPercent.Text = ds.Rows(0)("SalvagePercent").ToString()
                    If txtSalPercent.Text = "" Then
                        txtSalPercent.Text = CalculatePercent(txtSalYr.Text, txtSalMonth.Text)
                    End If

                    'rdoFixedValue.Checked = Not ds.Rows(0)("IsSalvageValuePercent")
                    'rdoPercentageValue.Checked = ds.Rows(0)("IsSalvageValuePercent")
                    If ds.Rows(0)("IsSalvageValuePercent") Then
                        txtSalValPercent.Text = ds.Rows(0)("SalvageValue").ToString()
                        rdoPercentageValue.Checked = True
                        rdoFixedValue.Checked = False
                    Else
                        txtSalVal.Text = ds.Rows(0)("SalvageValue").ToString()
                        rdoPercentageValue.Checked = False
                        rdoFixedValue.Checked = True
                    End If

                    isEdit = True
                    errProv.ClearErrors()
                    valProvMain.Validate()
                    Return True
                End If
            End If
            isEdit = False

            Return False
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Function

    Private Sub Update_DepPolicy()
        Try
            objattDepPolicy = New ZulAssetsDAL.ZulAssetsDAL.attDepPolicy

            objattDepPolicy.AstCatID = txtCategory.Tag
            objattDepPolicy.DepCode = 1
            If rdoFixedValue.Checked Then
                objattDepPolicy.SalvageValue = txtSalVal.Text
            Else
                objattDepPolicy.SalvageValue = txtSalValPercent.Text
            End If

            objattDepPolicy.SalvageYear = txtSalYr.Text
            objattDepPolicy.SalvageMonth = txtSalMonth.Text
            objattDepPolicy.SalvagePercent = txtSalPercent.Text
            objattDepPolicy.IsSalvageValuePercent = rdoPercentageValue.Checked
            objBALDepPolicy.Update_DepPolicy(objattDepPolicy)
            ZulMessageBox.ShowMe("Saved")
            ClearFrame(GroupBox1)

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub
#End Region

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        errProv.ClearErrors()
        Try
            If valProvMain.Validate Then
                Dim intVal As Integer
                txtSalPercent.Text = CalculatePercent(txtSalYr.Text, txtSalMonth.Text)
                intVal = CInt(txtSalPercent.Text)
                If intVal < 0 Or intVal > 1200 Then
                    errProv.SetError(txtSalPercent, My.MessagesResource.Messages.OutPercentRange)
                    Return
                End If

                If isEdit Then
                    Me.Update_DepPolicy()
                Else
                    Me.AddNew_DepPolicy()
                End If

                isEdit = True
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub




    Private Sub trv_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trv.AfterSelect
        Try
            SelNode = e.Node
            If Not SelNode Is Nothing Then
                txtCategory.Tag = SelNode.Tag
                txtCategory.Text = SelNode.FullPath.Replace("\", " \ ")
                Me.Get_DepPolicy(SelNode.Tag)
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub trv_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trv.MouseUp
        If e.Button = MouseButtons.Right Then
            Dim n As TreeNode = Me.trv.GetNodeAt(e.X, e.Y)
            If Not n Is Nothing Then
                Me.trv.SelectedNode = n
            End If
        End If
    End Sub



    Private Sub txtSalPercent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSalPercent.TextChanged
        Try
            If txtSalPercent.ContainsFocus Then
                Dim intVal As Integer = CInt(txtSalPercent.Text)
                If intVal < 0 Or intVal > 1200 Then
                    errProv.SetError(txtSalPercent, My.MessagesResource.Messages.OutPercentRange)
                Else
                    errProv.ClearErrors()
                    Dim perValue As Decimal = CType(txtSalPercent.Text.Trim, Double)
                    Dim YearCost As Double = 100 / perValue

                    txtSalYr.Text = Math.Truncate(YearCost)
                    txtSalMonth.Text = Math.Ceiling(12 * (YearCost - Math.Truncate(YearCost)))
                End If
            End If
        Catch ex As Exception
            errProv.SetError(txtSalPercent, My.MessagesResource.Messages.OutPercentRange)
        End Try
    End Sub
    Private Sub txtSalYr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSalYr.KeyPress
        If e.KeyChar = "." Then
            txtSalMonth.Focus()
        End If
    End Sub

    Private Sub txtSalYr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSalYr.TextChanged, txtSalMonth.TextChanged
        If txtSalYr.ContainsFocus Or txtSalMonth.ContainsFocus Then
            errProv.ClearErrors()
            valProvMain.Validate()
            txtSalPercent.Text = CalculatePercent(txtSalYr.Text, txtSalMonth.Text)
        End If
    End Sub

    Private Sub rdoFixedValue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoFixedValue.CheckedChanged
        txtSalVal.Enabled = rdoFixedValue.Checked
        txtSalValPercent.Enabled = Not rdoFixedValue.Checked
        If rdoFixedValue.Checked Then
            txtSalValPercent.Text = ""
        Else
            txtSalVal.Text = "0"
        End If
    End Sub

    Private Sub rdoPercentageValue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoPercentageValue.CheckedChanged
        txtSalVal.Enabled = Not rdoPercentageValue.Checked
        txtSalValPercent.Enabled = rdoPercentageValue.Checked
        If rdoFixedValue.Checked Then
            txtSalValPercent.Text = ""
        Else
            txtSalVal.Text = "0"
        End If
    End Sub

    Private Sub txtSalValPercent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSalValPercent.TextChanged
        Try
            If txtSalValPercent.ContainsFocus Then
                Dim intVal As Double = CDbl(txtSalValPercent.Text)
                If (intVal < 0 Or intVal > 100) And rdoPercentageValue.Checked Then
                    errProv.SetError(txtSalValPercent, "Entered value is out of percentage range(1,100)")
                Else
                    errProv.SetError(txtSalValPercent, "")
                End If
            End If
        Catch ex As Exception
            errProv.SetError(txtSalValPercent, "Entered value is out of percentage range(1,100)")
        End Try
    End Sub


    Private Sub txtSalVal_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSalVal.KeyPress
        e.Handled = Not (IsNumber(e.KeyChar))
    End Sub
End Class