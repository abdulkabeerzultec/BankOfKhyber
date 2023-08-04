Imports System
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient

Public Class ConnectionString

    Public Shared ServerName As String = ""
    Public Shared DbName As String = ""
    Public Shared UserName As String = ""
    Public Shared UserPass As String = ""
    Public Shared SQLPort As String = ""


    Private Shared ConnectionString As String = ""
    Private Shared _Connected As Boolean = False
    Private Shared _ErrorMessage As String = ""

    Public Shared Property ErrorMessage() As String
        Get
            Return _ErrorMessage
        End Get
        Set(ByVal value As String)
            _ErrorMessage = value
        End Set
    End Property

    Public Shared Property Connected() As Boolean
        Get
            Return _Connected
        End Get
        Set(ByVal value As Boolean)
            _Connected = value
        End Set
    End Property

    Public Shared Function GetConStr() As String
        Return "DATA SOURCE=" & ServerName & "," & SQLPort & ";UID=" & UserName & ";PWD=" & UserPass & ";Initial Catalog=" & DbName & "" & ";Connect Timeout=3"
    End Function

    Public Shared Function ConnectToDb() As Boolean
        ConnectionString = GetConStr()

        If TestConStr(ConnectionString) Then
            'ChangeDefaultConStr(ConnectionString)
            Connected = True
        Else
            Connected = False
        End If
        Return Connected
    End Function
    Private Shared Function TestConStr(ByVal ConStr As String) As Boolean
        Dim conn1 As SqlConnection = New SqlConnection(ConStr)
        Try
            conn1.Open()
            conn1.Close()
            conn1.Dispose()
            Return True
        Catch ex As Exception
            ErrorMessage = ex.Message
            Return False
        End Try
    End Function

    'Public Shared Sub ChangeDefaultConStr(ByVal constr As String)
    '    My.MySettings.Default("ZulRouteConnectionString") = constr
    'End Sub


End Class
