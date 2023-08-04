Imports System
Imports System.Data.OleDb
Imports System.Configuration.ConfigurationSettings

Namespace ZulAssetsDAL
    Public Class DBConnection
        'This Connection is used for connecting the Temp Database
        Public Function GetConnection_Temp() As OleDbConnection
            Dim CsTemp As String
            Dim ConnTemp As OleDbConnection
            CsTemp = "Provider=SQLNCLI11;DATA SOURCE=" & AppConfig.ComServer & "," & Val(AppConfig.CommPort) & ";UID=" & AppConfig.ComUname & ";PWD=" & AppConfig.ComPass & ";DATABASE=" & AppConfig.ComName & ""
            ConnTemp = New OleDbConnection(CsTemp)
            ConnTemp.Open()
            Return ConnTemp
        End Function

        Public Function GetConnectionString_Temp() As String
            Return "Provider=SQLNCLI11;DATA SOURCE=" & AppConfig.ComServer & "," & Val(AppConfig.CommPort) & ";UID=" & AppConfig.ComUname & ";PWD=" & AppConfig.ComPass & ";DATABASE=" & AppConfig.ComName & ""
        End Function

        Public Function GetConnectionString() As String
            If AppConfig.DbType = "1" Then
                Return ("Provider=SQLNCLI11;DATA SOURCE=" & AppConfig.DbServer & "," & Val(AppConfig.DBSQLPort) & ";UID=" & AppConfig.DbUname & ";PWD=" & AppConfig.DbPass & ";DATABASE=" & AppConfig.DbName & ";Connect Timeout=60")
            Else
                Return "Provider=msdaora;Data Source=" & AppConfig.DbServer & ";User Id=" & AppConfig.DbUname & ";Password=" & AppConfig.DbPass & ""
            End If
        End Function
        Public Function GetSQLConnection_Temp() As SqlClient.SqlConnection
            Dim CsTemp As String
            Dim ConnTemp As SqlClient.SqlConnection
            CsTemp = "DATA SOURCE=" & AppConfig.ComServer & "," & Val(AppConfig.CommPort) & ";UID=" & AppConfig.ComUname & ";PWD=" & AppConfig.ComPass & ";DATABASE=" & AppConfig.ComName & ""
            ConnTemp = New SqlClient.SqlConnection(CsTemp)
            ConnTemp.Open()
            Return ConnTemp
        End Function

        Public Function GetConnection() As OleDbConnection
            Dim Conn As OleDbConnection
            Dim _ConnectionString As String = Nothing
            If AppConfig.DbType = "1" Then
                _ConnectionString = "Provider=SQLNCLI11;DATA SOURCE=" & AppConfig.DbServer & "," & Val(AppConfig.DBSQLPort) & ";UID=" & AppConfig.DbUname & ";PWD=" & AppConfig.DbPass & ";DATABASE=" & AppConfig.DbName & ";Connect Timeout=60"
            ElseIf AppConfig.DbType = "2" Then
                _ConnectionString = "Provider=msdaora;Data Source=" & AppConfig.DbServer & ";User Id=" & AppConfig.DbUname & ";Password=" & AppConfig.DbPass
            End If
            Conn = New OleDbConnection(_ConnectionString)
            Return Conn
        End Function

        Public Function GetConnection_ExportServer() As OleDbConnection
            Dim Conn As OleDbConnection
            Dim _ConnectionString As String
            _ConnectionString = "Provider=SQLNCLI11;DATA SOURCE=" & AppConfig.ExpServer & "," & Val(AppConfig.ExpPort) & ";UID=" & AppConfig.ExpUname & ";PWD=" & AppConfig.ExpPass & ";DATABASE=" & AppConfig.ExpName & ""
            Conn = New OleDbConnection(_ConnectionString)
            Return Conn
        End Function
    End Class
End Namespace

