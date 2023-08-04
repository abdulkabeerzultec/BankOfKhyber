Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Public Class frmUser

    Dim objattUsers As ZulAssetsDAL.ZulAssetsDAL.attUsers
    Dim objBALUsers As New ZulAssetsBL.ZulAssetBAL.BALUsers
    Dim isEdit As Boolean = False
    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider




    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate Then
                If (txtPass.Text.Equals(txtconPass.Text)) Then
                    If isEdit Then
                        If Update_User() Then
                            Get_User(MainForm.RoleID)
                            btnNew_Click(sender, e)
                            'If MainForm.RoleID = 1 Then

                            'Else
                            '    isEdit = True
                            'End If

                        End If
                    Else
                        If AddNew_User() Then
                            Get_User(MainForm.RoleID)
                            btnNew_Click(sender, e)
                            'If MainForm.RoleID = 1 Then
                            '    btnNew_Click(sender, e)
                            'Else
                            '    isEdit = True
                            'End If
                        End If
                    End If
                    errProv.ClearErrors()
                Else
                    errProv.SetError(txtPass, My.MessagesResource.Messages.PasswordAndConfirmNotMatched)
                    errProv.SetError(txtconPass, My.MessagesResource.Messages.PasswordAndConfirmNotMatched)
                    txtPass.Text = ""
                    txtconPass.Text = ""
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub
#Region "Method"

    Private Function AddNew_User() As Boolean
        Try

            If (CheckID(RemoveUnnecessaryChars(txtLoginName.Text))) Then
                objattUsers = New ZulAssetsDAL.ZulAssetsDAL.attUsers
                objattUsers.LoginName = RemoveUnnecessaryChars(txtLoginName.Text)
                objattUsers.UserName = RemoveUnnecessaryChars(txtUserName.Text)
                objattUsers.Password = RemoveUnnecessaryChars(txtPass.Text)
                objattUsers.RoleID = txtRoleID.SelectedValue
                If rdbDesk.Checked Then
                    objattUsers.Useraccess = 1
                ElseIf rdbMobile.Checked Then
                    objattUsers.Useraccess = 2
                ElseIf rdbBoth.Checked Then
                    objattUsers.Useraccess = 3
                End If
                If objBALUsers.Insert_Users(objattUsers) Then
                    ZulMessageBox.ShowMe("Saved")
                    Return True
                Else
                    Return False
                End If
            Else
                MessageBox.Show("Login Name already exists, please try again.")
                Return False
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Function

    Private Sub format_Grid()
        Dim RIUserType As New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
        RIUserType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() _
                                {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Desktop Application", 1), _
                                 New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Mobile Application", 2), _
                                 New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Both", 3)})
        grdView.Columns(0).Caption = "Login Name"
        grdView.Columns(0).Width = 148

        grdView.Columns(1).Caption = "User Name"
        grdView.Columns(1).Width = 200

        grdView.Columns(3).Visible = False

        grdView.Columns(4).Caption = "User Roles"
        grdView.Columns(4).Width = 100
        'grdView.Columns(4).Visible = False
        grdView.Columns(5).Visible = False

        grdView.Columns("UserAccesstxt").Caption = "User Access"
        grdView.Columns("UserAccesstxt").Width = 120
        grdView.Columns("UserAccesstxt").Visible = True
        grdView.Columns("UserAccesstxt").ColumnEdit = RIUserType
        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        addGridMenu(grd)
    End Sub

    Private Sub Get_User(ByVal RoleID As String)
        Try
            Dim ds As New DataTable
            ds = objBALUsers.GetAll_Users(New attUsers)
            'If RoleID = "1" Then
            '    ds = objBALUsers.GetAll_Users(New attUsers)
            'Else
            '    objattUsers = New ZulAssetsDAL.ZulAssetsDAL.attUsers
            '    objattUsers.LoginName = RemoveUnnecessaryChars(AppConfig.LoginName)
            '    ds = objBALUsers.GetAll_Users(objattUsers)
            'End If
            'adding new column to hold the user permission text field 
            Dim UserAccessDC As DataColumn = ds.Columns.Add("UserAccesstxt", System.Type.GetType("System.String"))
            UserAccessDC.SetOrdinal(2)

            For Each row As DataRow In ds.Rows
                If row("UserAccess") = 1 Then
                    row("UserAccesstxt") = "Desktop Application"
                ElseIf row("UserAccess") = 2 Then
                    row("UserAccesstxt") = "Mobile Application"
                Else
                    row("UserAccesstxt") = "Both"
                End If
            Next
            grd.DataSource = ds
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Function Update_User() As Boolean
        Try
            objattUsers = New ZulAssetsDAL.ZulAssetsDAL.attUsers
            objattUsers.LoginName = RemoveUnnecessaryChars(txtLoginName.Text)
            objattUsers.UserName = RemoveUnnecessaryChars(txtUserName.Text)
            objattUsers.Password = RemoveUnnecessaryChars(txtPass.Text)
            objattUsers.RoleID = txtRoleID.SelectedValue
            If rdbDesk.Checked Then
                objattUsers.Useraccess = 1
            ElseIf rdbMobile.Checked Then
                objattUsers.Useraccess = 2
            ElseIf rdbBoth.Checked Then
                objattUsers.Useraccess = 3
            End If
            If objBALUsers.Update_Users(objattUsers) Then
                isEdit = False
                btnDelete.Visible = False
                ZulMessageBox.ShowMe("Saved")
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Function
    Private Function CheckID(ByVal _Id As String) As Boolean
        Try
            Dim ds As New DataTable
            objattUsers = New ZulAssetsDAL.ZulAssetsDAL.attUsers
            objattUsers.LoginName = _Id
            ds = objBALUsers.CheckID(objattUsers)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Function

#End Region

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try

            FormController.objfrmUser = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub frmUser_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmUser = Nothing
    End Sub



    Private Sub frmUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        Try
            txtRoleID.Enabled = True
            Get_User(MainForm.RoleID)
            format_Grid()


            valProvMain.SetValidationRule(txtLoginName, valRulenotEmpty)
            valProvMain.SetValidationRule(txtUserName, valRulenotEmpty)
            valProvMain.SetValidationRule(txtRoleID.TextBox, valRulenotEmpty)

            'This code has been Removed because now we have password change
            'before the form was being used to change the password only.

            'If MainForm.RoleID <> 1 Then
            '    valProvMain.SetValidationRule(txtPass, valRulenotEmpty)
            '    valProvMain.RemoveControlError(txtPass)
            '    txtLoginName.Properties.ReadOnly = True
            '    txtUserName.Properties.ReadOnly = True
            '    txtRoleID.Enabled = False
            '    btnNew.Visible = False
            '    btnDelete.Visible = False
            '    GroupBox1.Enabled = False
            '    Me.ActiveControl = txtPass
            'Else
            'btnNew_Click(sender, e)
            'End If
            btnNew_Click(sender, e)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub


    Private Sub grdView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdView.KeyDown
        If e.KeyValue = Keys.Delete And btnDelete.Visible Then 'Press Delete
            btnDelete_Click(sender, e)
        End If
    End Sub


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim FocRow As Integer = grdView.FocusedRowHandle
            If FocRow >= 0 Then
                If Not check_Child_AddCostHistory(GetGridRowCellValue(grdView, FocRow, "LoginName").ToString) Then
                    If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        objattUsers = New attUsers
                        objattUsers.LoginName = GetGridRowCellValue(grdView, FocRow, "LoginName").ToString
                        If objBALUsers.Delete_Users(objattUsers) Then
                            grdView.DeleteSelectedRows()
                            ZulMessageBox.ShowMe("Deleted")
                            btnNew_Click(sender, e)
                        End If
                    End If
                Else
                    ZulMessageBox.ShowMe("CantDelete")
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub


    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        txtRoleID.Enabled = True
        txtLoginName.Properties.ReadOnly = False
        txtLoginName.Text = ""
        txtPass.Text = ""
        txtUserName.Text = ""
        txtconPass.Text = ""
        txtRoleID.SelectedText = ""
        txtRoleID.SelectedValue = ""
        rdbBoth.Checked = True

        isEdit = False
        txtLoginName.Properties.ReadOnly = False
        btnDelete.Visible = False
        btnSave.Visible = True
        txtLoginName.Focus()

        grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        grdView.FocusedColumn = grdView.Columns(0)
        valProvMain.RemoveControlError(txtLoginName)
        valProvMain.RemoveControlError(txtUserName)
        valProvMain.RemoveControlError(txtPass)
        valProvMain.RemoveControlError(txtRoleID.TextBox)

        errProv.ClearErrors()
    End Sub


    Private Sub grdView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Try
            Dim FocRow As Integer = grdView.FocusedRowHandle
            If FocRow >= 0 Then
                txtLoginName.Text = GetGridRowCellValue(grdView, FocRow, "LoginName").ToString
                txtUserName.Text = GetGridRowCellValue(grdView, FocRow, "UserName").ToString
                txtRoleID.FindRow(GetGridRowCellValue(grdView, FocRow, "RoleID").ToString, GetGridRowCellValue(grdView, FocRow, "Description").ToString)

                txtPass.Text = ""
                txtconPass.Text = ""
                Dim i As Integer = GetGridRowCellValue(grdView, FocRow, "UserAccess").ToString
                If i = 1 Then
                    rdbDesk.Checked = True
                ElseIf i = 2 Then
                    rdbMobile.Checked = True
                ElseIf i = 3 Then
                    rdbBoth.Checked = True
                End If
                txtLoginName.Properties.ReadOnly = True

                'If MainForm.RoleID = 1 Then
                txtRoleID.Enabled = True
                If GetGridRowCellValue(grdView, FocRow, "LoginName").ToString.ToUpper = "ADMIN" Then
                    btnDelete.Visible = False
                    isEdit = True
                    txtRoleID.Enabled = False
                    'GroupBox1.Enabled = False

                Else
                    txtRoleID.Enabled = True
                    'GroupBox1.Enabled = True
                    btnDelete.Visible = True

                    isEdit = True
                End If
                'Else
                isEdit = True
                'txtRoleID.Enabled = False
                'End If
                valProvMain.Validate()
                errProv.ClearErrors()
            Else
                btnDelete.Visible = False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub txtRoleID_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRoleID.LovBtnClick
        Try
            txtRoleID.ValueMember = "RoleID"
            txtRoleID.DisplayMember = "Description"
            Dim objBALRole As New BALRoles
            txtRoleID.DataSource = objBALRole.Getcombo_Roles
            txtRoleID.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
End Class
