Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class Users
        Implements IEntity

#Region "Data Members"
        Private objattUsers As attUsers
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattUsers = New attUsers
        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattUsers
            End Get
            Set(ByVal Value As IAttribute)
                objattUsers = CType(Value, attUsers)
            End Set
        End Property

        Public Property ObjCommand() As IDbCommand Implements IEntity.ObjCommand
            Get
                Return _Command
            End Get
            Set(ByVal Value As IDbCommand)
                _Command = CType(Value, IDbCommand)
            End Set
        End Property
#End Region

#Region "Methods"
        Public Function Check_Child(ByVal _id As String, ByVal formid As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select LoginName,AppUsers.RoleID from AppUsers ")
            strQuery.Append(" where AppUsers.IsDeleted = 0")
            If _id <> "" Then
                strQuery.Append(" and AppUsers.RoleID = '" & _id & "'")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function Insert() As IDbCommand Implements IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into AppUsers")
            If AppConfig.DbType = "1" Then
                strQuery.Append(" (LoginName,UserName,[Password],UserAccess,RoleID,Isdeleted)")
            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append(" (LoginName,UserName,Password,UserAccess,RoleID,Isdeleted)")
            Else
                strQuery.Append(" (LoginName,UserName,[Password],UserAccess,RoleID,Isdeleted)")
            End If

            strQuery.Append(" Values")
            strQuery.Append(" ('" & objattUsers.LoginName & "','" & objattUsers.UserName & "','" & objattUsers.Password & "'," & objattUsers.Useraccess & "," & objattUsers.RoleID & ",0) ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AppUsers")
            strQuery.Append(" set")
            strQuery.Append(" UserName='" & objattUsers.UserName & "',")
            strQuery.Append(" RoleID=" & objattUsers.RoleID & ",")
            If objattUsers.Password <> "" Then
                If AppConfig.DbType = "1" Then
                    strQuery.Append(" [Password]='" & objattUsers.Password & "',")
                ElseIf AppConfig.DbType = "2" Then
                    strQuery.Append(" Password='" & objattUsers.Password & "',")
                End If
            End If
            strQuery.Append(" UserAccess=" & objattUsers.Useraccess & "")
            strQuery.Append(" where LoginName ='" & objattUsers.LoginName & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function UpdateUserPassword() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AppUsers")
            strQuery.Append(" set")
            If AppConfig.DbType = "1" Then
                strQuery.Append(" [Password]='" & objattUsers.Password & "'")
            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append(" Password='" & objattUsers.Password & "'")
            End If
            strQuery.Append(" where LoginName ='" & objattUsers.LoginName & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select LoginName,UserName,UserAccess,Description,AppUsers.RoleID from AppUsers ")
            strQuery.Append(" inner join Roles on Roles.RoleID  = AppUsers.RoleID")
            strQuery.Append(" where AppUsers.IsDeleted = 0")
            If objattUsers.LoginName <> "" Then
                strQuery.Append(" and LoginName = '" & objattUsers.LoginName & "'")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function CheckID() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select  LoginName,UserName,UserAccess,RoleID  from AppUsers ")
            If objattUsers.LoginName <> "" Then
                strQuery.Append(" where LoginName = '" & objattUsers.LoginName & "'")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetDatabyID(ByVal Id As String) As IDbCommand Implements IEntity.GetDatabyID
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select * from GrpChild")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder


            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from AppUsers")
                strQuery.Append(" where LoginName =?")
            Else
                strQuery.Append("update AppUsers")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where LoginName =?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@LoginName", objattUsers.LoginName))



            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Return ObjCommand
        End Function

        Public Function VerifyDesktop_User(ByVal DbServer As String, ByVal Dbname As String, ByVal DbUname As String, ByVal DbPass As String, ByVal SysPass As String, ByVal SQLPort As String, ByVal DbType As String, ByVal txtComUname As String, ByVal txtComPass As String, ByVal txtComSerPort As String, ByVal txtCommServ As String, ByVal DbComname As String) As Boolean
            Try
                Dim objCommand As OleDbCommand = New OleDbCommand
                'Dim dtuser As DataTable
                Dim _ConnectionString As String
                If DbType = "2" Then
                    _ConnectionString = "Provider=msdaora;Data Source=" & DbServer & ";User Id=" & DbUname & ";Password=" & DbPass & ""
                Else
                    '_ConnectionString = "Provider=SQLNCLI11;DATA SOURCE=" & DbServer & ";UID=" & DbUname & ";PWD=" & DbPass & ";DATABASE=" & Dbname & ""
                    _ConnectionString = "Provider=SQLNCLI11;DATA SOURCE=" & DbServer & "," & Val(SQLPort) & ";UID=" & DbUname & ";PWD=" & DbPass & ";DATABASE=" & Dbname & ""
                End If
                Dim conn1 As New OleDbConnection(_ConnectionString)
                conn1.Open()
                Dim strQuery As New StringBuilder
                strQuery.Append("Select LoginName,UserName,RoleID from AppUsers where IsDeleted = 0")
                strQuery.Append(" and LoginName = '" & objattUsers.LoginName & "'")
                If AppConfig.DbType = "1" Then
                    strQuery.Append(" and [Password] = '" & objattUsers.Password & "'")
                ElseIf AppConfig.DbType = "2" Then
                    strQuery.Append(" and Password = '" & objattUsers.Password & "'")
                Else
                    strQuery.Append(" and [Password] = '" & objattUsers.Password & "'")
                End If
                strQuery.Append(" and ( UserAccess = 1")
                strQuery.Append(" or UserAccess = 3)")
                objCommand.CommandText = strQuery.ToString()
                objCommand.Connection = conn1
                Dim Da As OleDbDataAdapter = New OleDbDataAdapter(objCommand)
                Dim DS As DataTable = New DataTable
                Da.Fill(DS)
                If DS Is Nothing = False Then
                    If DS.Rows.Count > 0 Then
                        Return True
                    End If
                End If
                Return False
            Catch ex As Exception

            End Try

        End Function

        Public Function Verify_User() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select LoginName,UserName,RoleID from AppUsers where IsDeleted = 0")
            strQuery.Append(" and LoginName = '" & objattUsers.LoginName & "'")
            If AppConfig.DbType = "1" Then
                strQuery.Append(" and [Password] = '" & objattUsers.Password & "'")
            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append(" and Password = '" & objattUsers.Password & "'")
            Else
                strQuery.Append(" and [Password] = '" & objattUsers.Password & "'")
            End If
            strQuery.Append(" and ( UserAccess = 1")
            strQuery.Append(" or UserAccess = 3)")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

#End Region

    End Class

End Namespace
