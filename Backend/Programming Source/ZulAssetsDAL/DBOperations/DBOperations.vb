Imports System
Imports System.Data
Imports System.Data.OleDb
Namespace ZulAssetsDAL
    Public Class DBOperations

#Region " -- Data Members -- "
        'Dim Conn As OleDbConnection
        Dim Cmd As OleDbCommand
        Private ObjDBConnection As DBConnection
        ' Public _entity As IEntity
#End Region

#Region " -- Constructor --"
        Public Sub New()
            ObjDBConnection = New DBConnection

        End Sub
#End Region
        '**** CRUD Functions *****

#Region " -- Update -- "
        Public Sub update(ByVal _Entity As IEntity)
            Dim Conn As OleDbConnection = Nothing
            Try
                Cmd = New OleDbCommand
                Conn = ObjDBConnection.GetConnection()
                Conn.Open()
                Cmd = _Entity.Update()
                Cmd.Connection = Conn

                Cmd.ExecuteNonQuery()
            Catch e As Exception
                Throw e
            Finally
                If Conn IsNot Nothing Then
                    Conn.Close()
                    Conn.Dispose()
                End If
            End Try
        End Sub
        Public Sub updateTemp(ByVal _Entity As IEntity)
            Dim Conn As OleDbConnection = Nothing
            Try
                Cmd = New OleDbCommand
                Conn = ObjDBConnection.GetConnection_Temp()

                Cmd = _Entity.Update()
                Cmd.Connection = Conn

                Cmd.ExecuteNonQuery()
            Catch e As Exception
                Throw e
           
            End Try
        End Sub
#End Region

#Region " -- Delete -- "
        Public Sub delete(ByVal _Entity As IEntity)
            Dim Conn As OleDbConnection = Nothing
            Try
                Cmd = New OleDbCommand
                Conn = ObjDBConnection.GetConnection()
                Conn.Open()
                Cmd = _Entity.Delete()
                Cmd.Connection = Conn

                Cmd.ExecuteNonQuery()
            Catch e As Exception
                Throw e
            Finally
                If Conn IsNot Nothing Then
                    Conn.Close()
                    Conn.Dispose()
                End If
            End Try
        End Sub
    
        Public Sub deleteTemp(ByVal _Entity As IEntity)
            Dim Conn As OleDbConnection = Nothing
            Try
                Cmd = New OleDbCommand
                Conn = ObjDBConnection.GetConnection_Temp()

                Cmd = _Entity.Delete()
                Cmd.Connection = Conn

                Cmd.ExecuteNonQuery()
            Catch e As Exception
                Throw e
            End Try
        End Sub
#End Region
        Public Function Get_OracleDate_Format() As String
            Dim conn1 As OleDbConnection = Nothing
            Dim cmd1 As New OleDbCommand
            Try

                conn1 = ObjDBConnection.GetConnection()
                conn1.Open()
                cmd1.Connection = conn1
                cmd1.CommandText = "SELECT Value FROM NLS_DATABASE_PARAMETERS WHERE Parameter='NLS_DATE_FORMAT'"
                Dim Da As OleDbDataAdapter = New OleDbDataAdapter(cmd1)
                Dim DS As DataTable = New DataTable

                Da.Fill(DS)
                If Not DS Is Nothing Then
                    ' If Not ds Is Nothing Then
                    If DS.Rows.Count > 0 Then
                        Return DS.Rows(0)(0).ToString()
                    End If
                    'nd If
                End If

                Return ""
            Catch e As Exception
                Throw e
            Finally
                If conn1 IsNot Nothing Then
                    conn1.Close()
                    conn1.Dispose()
                End If
            End Try
        End Function


#Region " -- Insert --"
        Public Sub Insert(ByVal _Entity As IEntity)
            Dim Conn As OleDbConnection = Nothing
            Try
                Cmd = New OleDbCommand
                Conn = ObjDBConnection.GetConnection()
                Conn.Open()
                Cmd = _Entity.Insert()
                Cmd.Connection = Conn

                Cmd.ExecuteNonQuery()
            Catch e As Exception
                Throw e
            Finally
                If Conn IsNot Nothing Then
                    Conn.Close()
                    Conn.Dispose()
                End If
            End Try
        End Sub

        Public Sub Insert_Temp(ByVal _Entity As IEntity)
            Dim Conn As OleDbConnection = Nothing
            Try
                Cmd = New OleDbCommand
                Conn = ObjDBConnection.GetConnection_Temp()

                Cmd = _Entity.Insert()
                Cmd.Connection = Conn

                Cmd.ExecuteNonQuery()
            Catch e As Exception
                Throw e
            
            End Try
        End Sub
#End Region

#Region " -- General Executer -- "
        Public Function General_Executer_Scalar(ByVal cmd As OleDbCommand) As String
            Dim Conn As OleDbConnection = Nothing
            Dim strKey As String = ""
            Try
                'cmd = New OleDbCommand
                Conn = ObjDBConnection.GetConnection()
                Conn.Open()

                cmd.Connection = Conn
                Dim obj As Object = cmd.ExecuteScalar()
                If Not obj Is Nothing Then
                    strKey = Convert.ToString(obj)

                End If
                If Trim(strKey) = "" Then
                    strKey = "0"
                End If

            Catch e As Exception
                Throw e
            Finally
                If Conn IsNot Nothing Then
                    Conn.Close()
                    Conn.Dispose()
                End If
            End Try
            Return strKey
        End Function

        Public Function General_Executer(ByVal cmd As OleDbCommand) As DataTable
            Dim Conn As OleDbConnection = Nothing
            Try
                Conn = ObjDBConnection.GetConnection()
                Conn.Open()
                cmd.Connection = Conn
                Dim Da As OleDbDataAdapter = New OleDbDataAdapter(cmd)
                Dim DS As DataTable = New DataTable
                Da.MissingSchemaAction = MissingSchemaAction.Add ' to add the table schema

                Da.Fill(DS)
                Return DS
            Catch e As Exception
                Throw e
            Finally
                If Conn IsNot Nothing Then
                    Conn.Close()
                    Conn.Dispose()
                End If
            End Try
            Return Nothing
        End Function

        Public Function General_ExecuterTemp(ByVal cmd As OleDbCommand) As DataTable
            Dim Conn As OleDbConnection = Nothing
            Try
                Conn = ObjDBConnection.GetConnection_Temp
                cmd.Connection = Conn
                Dim Da As OleDbDataAdapter = New OleDbDataAdapter(cmd)
                Dim DS As DataTable = New DataTable
                Da.MissingSchemaAction = MissingSchemaAction.Add ' to add the table schema

                Da.Fill(DS)
                Return DS
            Catch e As Exception
                Throw e

            End Try
            Return Nothing
        End Function
#End Region

        Public Function General_ExecuteScaler_ExportServer(ByVal cmd As OleDbCommand) As String
            Dim Conn As OleDbConnection = Nothing
            Try
                '  cmd = New OleDbCommand

                Conn = ObjDBConnection.GetConnection_ExportServer
                Conn.Open()
                '  cmd = _entity.Insert()
                cmd.Connection = Conn
                Return cmd.ExecuteScalar()
            Catch e As Exception
                Throw e
            Finally
                If Conn IsNot Nothing Then
                    Conn.Close()
                    Conn.Dispose()
                End If
            End Try
            Return Nothing
        End Function

        Public Sub General_TransationExecuter(ByVal cmd As OleDbCommand)
            Dim Conn As OleDbConnection = Nothing
            Try
                Conn = ObjDBConnection.GetConnection()
                Conn.Open()
                cmd.Connection = Conn
                cmd.ExecuteNonQuery()
            Catch e As Exception
                Throw e
            Finally
                If Conn IsNot Nothing Then
                    Conn.Close()
                    Conn.Dispose()
                End If
            End Try
        End Sub

#Region " -- GetExecuter -- "

        Public Function GetAllData(ByVal _Entity As IEntity) As DataTable
            Dim Conn As OleDbConnection = Nothing
            Try
                Cmd = New OleDbCommand
                Conn = ObjDBConnection.GetConnection()
                Conn.Open()
                Cmd = _Entity.GetAllData()
                Cmd.Connection = Conn
                Dim Da As OleDbDataAdapter = New OleDbDataAdapter(Cmd)
                Dim DS As DataTable = New DataTable
                Da.Fill(DS)
                Return DS
            Catch e As Exception
                Throw e
            Finally
                If Conn IsNot Nothing Then
                    Conn.Close()
                    Conn.Dispose()
                End If
            End Try
        End Function

        Public Function GetAllDataTemp(ByVal _Entity As IEntity) As DataTable
            Dim Conn As OleDbConnection = Nothing
            Try
                Cmd = New OleDbCommand
                Conn = ObjDBConnection.GetConnection_Temp
                Cmd = _Entity.GetAllData()
                Cmd.Connection = Conn
                Dim Da As OleDbDataAdapter = New OleDbDataAdapter(Cmd)
                Dim DS As DataTable = New DataTable
                Da.Fill(DS)
                Return DS
            Catch e As Exception
                Throw e
            End Try
        End Function
        'Public Function GetAllData(ByVal _Entity As IEntity) As DataTable
        '    Dim Con As OleDbConnection
        '    Try
        '        Cmd = New OleDbCommand
        '        Con = ObjDBConnection.GetConnection()
        '        Con.Open()
        '        Cmd = _Entity.GetAllData()
        '        Cmd.Connection = Conn
        '        Dim Da As OleDbDataAdapter = New OleDbDataAdapter(Cmd)
        '        Dim DS As DataTable = New DataTable
        '        Da.Fill(DS)
        '        Return DS
        '    Catch e As Exception
        '        Throw e
        '    Finally
        '        Con.Close()
        '        Con.Dispose()
        '    End Try

        'End Function

#End Region

        '*************************
        '*** other standard func *

#Region " -- GetPKeyExecuter -- "
        Public Function GetNextPKey(ByVal _Entity As IEntity) As String
            Dim Conn As OleDbConnection = Nothing
            Dim strKey As String = ""
            Try
                Cmd = New OleDbCommand
                Conn = ObjDBConnection.GetConnection()
                Conn.Open()
                Cmd = _Entity.GetNextPk
                Cmd.Connection = Conn
                Dim obj As Object = Cmd.ExecuteScalar()
                If Not obj Is Nothing Then
                    strKey = Convert.ToString(obj)

                End If
                If Trim(strKey) = "" Then
                    strKey = "0"
                End If

            Catch e As Exception
                Throw e
            Finally
                If Conn IsNot Nothing Then
                    Conn.Close()
                    Conn.Dispose()
                End If
            End Try
            Return strKey
        End Function
#End Region

        '*************************
        ' Custom Executer
        'change the code here as the Get Executer will take command object 

#Region " -- Transcation Executer -- "


        Public Sub TransationExecuter(ByVal _Entity As IEntity)
            Dim Conn As OleDbConnection = Nothing
            Try
                Cmd = New OleDbCommand
                Conn = ObjDBConnection.GetConnection()
                Conn.Open()
                Cmd = _Entity.ObjCommand()
                Cmd.Connection = Conn

                Cmd.ExecuteNonQuery()
            Catch e As Exception
                Throw e
            Finally
                If Conn IsNot Nothing Then
                    Conn.Close()
                    Conn.Dispose()
                End If
            End Try
        End Sub
        Public Sub TransationExecuter(ByVal Cmd As OleDbCommand)
            Dim Conn As OleDbConnection = Nothing
            Try
                Conn = ObjDBConnection.GetConnection()
                Conn.Open()
                Cmd.Connection = Conn
                Cmd.ExecuteNonQuery()
            Catch e As Exception
                Throw e
            Finally
                If Conn IsNot Nothing Then
                    Conn.Close()
                    Conn.Dispose()
                End If
            End Try
        End Sub
#End Region

        'change the code here as the Get Executer will take command object 

#Region " -- GetExecuter -- "

        Public Function GetExecuter(ByVal _Entity As IEntity) As DataTable
            Dim Conn As OleDbConnection = Nothing
            Try
                Conn = ObjDBConnection.GetConnection()
                Conn.Open()
                Cmd.Connection = Conn
                Cmd = _Entity.ObjCommand
                Dim Da As OleDbDataAdapter = New OleDbDataAdapter(Cmd)
                Dim DS As DataTable = New DataTable
                Da.Fill(DS)
                Return DS
            Catch e As Exception
                Throw e
            Finally
                If Conn IsNot Nothing Then
                    Conn.Close()
                    Conn.Dispose()
                End If
            End Try

        End Function
#End Region

        '**********************************************

#Region " -- Transaction Rollback -- "



        Public Sub RollBackTrans()
            Try
                'Cmd.Transaction.Rollback()
            Catch ex As Exception

            End Try
        End Sub
#End Region
    End Class

End Namespace

