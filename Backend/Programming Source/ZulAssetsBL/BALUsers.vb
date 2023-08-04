Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALUsers
        Inherits BLBase

#Region "Data Members"
        Private objUsers As Users
#End Region


        Public Sub New()
            objUsers = New Users
        End Sub

#Region "Functions"
        Public Function Check_Child(ByVal _id As String, ByVal formid As Integer) As DataTable
            Try
                Return Me.GeneralExecuter(objUsers.Check_Child(_id, formid))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub CreateDefaultRecord()
            Dim ds As DataTable
            Dim deletenw As Boolean = False
            Dim objatt As New attUsers
            ds = GetAll_Users(objatt)
            If ds Is Nothing = False Then
                If ds Is Nothing = False Then
                    If ds.Rows.Count < 1 Then
                        If deletenw = False Then
                            Delete_Users(objatt)
                            deletenw = True
                        End If
                        objatt = New attUsers
                        objatt.LoginName = "Admin"
                        objatt.UserName = "Admin"
                        objatt.Useraccess = "3"
                        objatt.RoleID = "1"
                        objatt.Password = "Pass"
                        Insert_Users(objatt)
                    End If
                End If
            End If

        End Sub
        Public Function Insert_Users(ByVal objattUsers As attUsers) As Boolean
            Try
                objUsers.Attribute = objattUsers
                Me.Insert(objUsers)
                Return True
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Update_Users(ByVal objattUsers As attUsers) As Boolean
            Try
                objUsers.Attribute = objattUsers
                Me.Update(objUsers)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Update_UserPassword(ByVal objattUsers As attUsers) As Boolean
            Try
                objUsers.Attribute = objattUsers
                Me.GeneralExecuter(objUsers.UpdateUserPassword())
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_Users(ByVal objattUsers As attUsers) As DataTable
            Try
                objUsers.Attribute = objattUsers
                Return GetAllData(objUsers)

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Delete_Users(ByVal objattUsers As attUsers) As Boolean
            Try
                If objattUsers.LoginName <> "admin" Then
                    objUsers.Attribute = objattUsers
                    Me.Delete(objUsers)
                    Return True
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function VerifyDesktop_User(ByVal objattUsers As attUsers, ByVal DbServer As String, ByVal Dbname As String, ByVal DbUname As String, ByVal DbPass As String, ByVal SysPass As String, ByVal SQLPort As String, ByVal DbType As String, ByVal txtComUname As String, ByVal txtComPass As String, ByVal txtComSerPort As String, ByVal txtCommServ As String, ByVal DbComname As String) As Boolean
            objUsers.Attribute = objattUsers
            Return objUsers.VerifyDesktop_User(DbServer, Dbname, DbUname, DbPass, SysPass, SQLPort, DbType, txtComUname, txtComPass, txtComSerPort, txtCommServ, DbComname)
        End Function
        Public Function Verify_User_Desktop(ByVal objattUsers As attUsers) As Boolean
            Dim dtUser As New DataTable
            Try
                objUsers.Attribute = objattUsers
                dtUser = Me.GeneralExecuter(objUsers.Verify_User())

                If Not dtUser Is Nothing Then
                    '   If Not dtUser.Tables(0) Is Nothing Then
                    If dtUser.Rows.Count > 0 Then
                        Return True
                        'End If
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return False
        End Function

        Public Function get_user_Role(ByVal objattUsers As attUsers) As DataTable
            Dim dtUser As New DataTable
            Try
                objUsers.Attribute = objattUsers
                dtUser = Me.GeneralExecuter(objUsers.Verify_User())

                If Not dtUser Is Nothing Then
                    If dtUser.Rows.Count > 0 Then
                        Return dtUser
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return New DataTable
        End Function

        Public Function CheckID(ByVal objattUsers As attUsers) As DataTable
            Try
                objUsers.Attribute = objattUsers
                Return Me.GeneralExecuter(objUsers.CheckID())
            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region
    End Class

End Namespace