'**************************************************************************************************************
'* A Generic Data Access Class Written by
'* IMRAN SHAFEEQ AHMAD (isahmad@gmail.com)
'* + 966 55 300 5187
'**************************************************************************************************************

Imports System.Data.OracleClient
Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Enum EnumProviders
    OracleClient
    SQLClient
    OLEDB
End Enum

Friend Class ProviderFactory
    Private Sub New()
    End Sub

    Public Shared Function GetCommand(ByVal provider As EnumProviders) As IDbCommand
        Select Case provider
            Case EnumProviders.OracleClient
                Return New OracleCommand
            Case EnumProviders.SQLClient
                Return New SqlCommand
            Case EnumProviders.OLEDB
                Return New OleDbCommand
            Case Else
                Return Nothing
        End Select
    End Function

    Public Shared Function GetCommand(ByVal strCmdText As String, ByVal cmdType As CommandType, _
                                      ByVal cmdTimeout As Integer, ByVal provider As EnumProviders) As IDbCommand

        Dim cmd As IDbCommand = GetCommand(provider)

        cmd.CommandType = cmdType
        cmd.CommandTimeout = cmdTimeout
        cmd.CommandText = strCmdText
        Return cmd
    End Function

    Public Shared Function GetConnection(ByVal provider As EnumProviders) As IDbConnection
        Select Case provider
            Case EnumProviders.OracleClient
                Return New OracleConnection
            Case EnumProviders.SQLClient
                Return New SqlConnection
            Case EnumProviders.OLEDB
                Return New OleDbConnection
            Case Else
                Return Nothing
        End Select
    End Function

    Public Shared Function GetConnection(ByVal strConnString As String, ByVal provider As EnumProviders) As IDbConnection
        Dim con As IDbConnection = GetConnection(provider)
        con.ConnectionString = strConnString
        Return con
    End Function

    Public Shared Function GetAdapter(ByVal provider As EnumProviders) As IDbDataAdapter
        Select Case provider
            Case EnumProviders.OracleClient
                Return New OracleDataAdapter
            Case EnumProviders.SQLClient
                Return New SqlDataAdapter
            Case EnumProviders.OLEDB
                Return New OleDbDataAdapter
            Case Else
                Return Nothing
        End Select
    End Function
End Class

Public Class DataAccess
    Private _conn As IDbConnection
    Private _cmdTimeout As Int32
    Private _connString As String
    Private _provider As EnumProviders
    Private Const COMMAND_TIMEOUT = 100
    Private _commandBehavior As CommandBehavior

    Public Sub New()
        _commandBehavior = CommandBehavior.CloseConnection
    End Sub

    Private Sub PrepareCommand(ByRef cmd As IDbCommand, ByVal strSQL As String, ByVal cmdType As CommandType)
        cmd = ProviderFactory.GetCommand(strSQL, cmdType, _cmdTimeout, _provider)
        cmd.Connection = _conn
    End Sub

    Public Property Provider() As EnumProviders
        Get
            Return _provider
        End Get
        Set(ByVal Value As EnumProviders)
            _provider = Value
        End Set
    End Property

    Public Property ConnectionString() As String
        Get
            Return _connString
        End Get
        Set(ByVal Value As String)
            _connString = Value
        End Set
    End Property

    Public Property CmdTimeout() As Int32
        Get
            If _cmdTimeout = 0 Then
                Return COMMAND_TIMEOUT
            End If
            Return _cmdTimeout
        End Get
        Set(ByVal Value As Int32)
            _cmdTimeout = Value
        End Set
    End Property

    Public Property ReaderCommandBehavior() As CommandBehavior
        Get
            Return _commandBehavior
        End Get
        Set(ByVal Value As CommandBehavior)
            _commandBehavior = Value
        End Set
    End Property

    Public Sub OpenConn()
        _conn = ProviderFactory.GetConnection(_connString, _provider)
        _conn.Open()
    End Sub

    Public Sub CloseConn()
        If Not _conn Is Nothing Then
            _conn.Close()
            _conn.Dispose()
        End If
    End Sub

    Public Function ExecDataSet(ByVal strSQL As String, ByVal cmdtype As CommandType) As DataSet
        Dim ds As New DataSet
        ExecDataSet(ds, strSQL, cmdtype)
        Return ds
    End Function

    Public Sub ExecDataSet(ByVal ds As DataSet, ByVal strSQL As String, ByVal cmdtype As CommandType)
        Dim da As IDbDataAdapter
        Dim cmd As IDbCommand = Nothing

        da = ProviderFactory.GetAdapter(_provider)
        PrepareCommand(cmd, strSQL, cmdtype)
        da.SelectCommand = cmd
        da.Fill(ds)
        cmd.Dispose()

        CType(da, IDisposable).Dispose()
    End Sub

    Public Sub SaveDataSet(ByVal ds As DataSet, _
                           ByVal InsertSQL As String, _
                           ByVal DeleteSQL As String, _
                           ByVal UpdateSQL As String)

        Dim da As IDbDataAdapter
        Dim cmd As IDbCommand = Nothing

        da = ProviderFactory.GetAdapter(Provider)

        If InsertSQL <> "" Then
            PrepareCommand(cmd, InsertSQL, CommandType.Text)
            da.InsertCommand = cmd
            da.InsertCommand.Connection = _conn
        End If
        If UpdateSQL <> "" Then
            PrepareCommand(cmd, UpdateSQL, CommandType.Text)
            da.UpdateCommand = cmd
            da.UpdateCommand.Connection = _conn
        End If
        If DeleteSQL <> "" Then
            PrepareCommand(cmd, DeleteSQL, CommandType.Text)
            da.DeleteCommand.Connection = _conn
        End If

        da.Update(ds)

        If InsertSQL <> "" Then da.InsertCommand.Dispose()
        If UpdateSQL <> "" Then da.UpdateCommand.Dispose()
        If DeleteSQL <> "" Then da.DeleteCommand.Dispose()

        CType(da, IDisposable).Dispose()
    End Sub

    Public Function ExecNonQuery(ByVal strSQL As String, ByVal cmdType As CommandType) As Integer

        Dim cmd As IDbCommand = Nothing

        PrepareCommand(cmd, strSQL, cmdType)
        Return cmd.ExecuteNonQuery()
        cmd.Dispose()

    End Function

    Public Function ExecDataReader(ByVal strSQL As String, _
                                   ByVal cmdtype As CommandType) _
                                   As IDataReader

        Dim cmd As IDbCommand = Nothing

        PrepareCommand(cmd, strSQL, cmdtype)
        Return cmd.ExecuteReader(ReaderCommandBehavior)
        cmd.Dispose()

    End Function

    Public Function ExecScalar(ByVal strSQL As String, ByVal cmdtype As CommandType) As String
        Dim cmd As IDbCommand = Nothing

        PrepareCommand(cmd, strSQL, cmdtype)
        Return cmd.ExecuteScalar
        cmd.Dispose()
    End Function
End Class