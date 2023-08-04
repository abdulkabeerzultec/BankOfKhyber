Imports ZulAssetsDAL.ZulAssetsDAL

Public Class frmChangePassword

    Dim objattUsers As ZulAssetsDAL.ZulAssetsDAL.attUsers
    Dim objBALUsers As New ZulAssetsBL.ZulAssetBAL.BALUsers
    Private _SelectedUser As String
    Private IsAdminUser As Boolean = False

    Public Property SelectedUser() As String
        Get
            Return _SelectedUser
        End Get
        Set(ByVal value As String)
            _SelectedUser = value
            lblUserName.Text = value
            'Check if the loged user is adminstrator.
            If IsUserRoleAdminstrator(value) Then
                IsAdminUser = True
                lblOldPass.Visible = False
                txtOldPassword.Visible = False
                txtPassword.Focus()
            Else
                IsAdminUser = False
                lblOldPass.Visible = True
                txtOldPassword.Visible = True
                txtOldPassword.Focus()
            End If
        End Set
    End Property

    Private Function IsUserRoleAdminstrator(ByVal LoginName As String) As Boolean
        objattUsers = New attUsers
        objattUsers.LoginName = LoginName
        Dim DT As DataTable = objBALUsers.CheckID(objattUsers)
        If DT IsNot Nothing Then
            If DT.Rows.Count > 0 Then
                Dim RoleID As String = DT.Rows(0)("RoleID")
                If RoleID = 1 Then Return True Else Return False
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Sub ChangePassword(ByVal NewPass As String)
        objattUsers = New ZulAssetsDAL.ZulAssetsDAL.attUsers
        objattUsers.LoginName = SelectedUser
        objattUsers.Password = NewPass

        If objBALUsers.Update_UserPassword(objattUsers) Then
            ZulMessageBox.ShowMe("PasswordChangedSucc", MessageBoxButtons.OK, MessageBoxIcon.Information, True)
            btnClose.PerformClick()
        Else
            ZulMessageBox.ShowMe("CantDelete")
        End If
    End Sub



    Private Sub txtConfirmPass_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtConfirmPass.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnSave.PerformClick()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If valProvider.Validate Then
            If txtConfirmPass.Text <> txtPassword.Text Then
                errProv.SetError(txtConfirmPass, My.MessagesResource.Messages.PasswordAndConfirmNotMatched)
                Return
            End If

            If IsAdminUser Then
                ChangePassword(txtPassword.Text)
            Else
                objattUsers = New ZulAssetsDAL.ZulAssetsDAL.attUsers
                objattUsers.LoginName = SelectedUser
                objattUsers.Password = txtOldPassword.Text
                If objBALUsers.Verify_User_Desktop(objattUsers) Then
                    ChangePassword(txtPassword.Text)
                Else
                    errProv.SetError(txtOldPassword, My.MessagesResource.Messages.OldPasswordNotCorrect)
                End If
            End If
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmChangePassword_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        valProvider.SetValidationRule(txtConfirmPass, valRulenotEmpty)
        valProvider.SetValidationRule(txtPassword, valRulenotEmpty)

    End Sub
End Class