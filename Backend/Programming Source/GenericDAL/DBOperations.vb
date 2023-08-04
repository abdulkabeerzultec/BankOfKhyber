Imports System.Data
Imports System
Imports System.Data.SqlClient
Imports System.Text

Public Class DBOperations
    Public Shared Function FieldValueExisted(ByVal FieldName As String, ByVal FieldValue As String, ByVal TableName As String, _
                                       ByVal IsInsertStatus As Boolean, ByVal PKField As String, _
                                      ByVal PKFieldVal As String, ByVal NavigationFilter As String) As Boolean
        Dim objCommand As SqlCommand = New SqlCommand
        Dim strQuery As New StringBuilder
        strQuery.Append("select " & PKField & " from " & TableName & " where " & FieldName)
        strQuery.Append(" = @FieldValue")

        If Not String.IsNullOrEmpty(NavigationFilter) Then
            strQuery.Append(" and " & NavigationFilter)
        End If
        objCommand.Parameters.Add(New SqlParameter("@FieldValue", FieldValue))
        objCommand.CommandText = strQuery.ToString()
        Try

            Dim str As String = General_Executer_Scalar(objCommand)
            If str = "0" Then
                str = ""
            End If

            If str <> "" And IsInsertStatus Then
                Return True
            ElseIf str <> "" And Not IsInsertStatus And str <> PKFieldVal Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Shared Function General_Executer_Scalar(ByVal cmd As SQLCommand) As String
        Dim Conn As New SqlConnection(ConnectionString.GetConStr)
        Dim strKey As String = ""
        Try
            'Conn.ConnectionString = ConnectionString.GetConStr
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

    Public Shared Function ExecuteNonQuery(ByVal cmd As SqlCommand) As Integer
        Dim Conn As New SqlConnection(ConnectionString.GetConStr)
        Dim strKey As String = ""
        Try
            'Conn.ConnectionString = ConnectionString.GetConStr
            Conn.Open()

            cmd.Connection = Conn
            Return cmd.ExecuteNonQuery
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

    Public Shared Function ExecuteReader(ByVal cmd As SqlCommand) As DataTable
        Dim Conn As New SqlConnection(ConnectionString.GetConStr)
        Dim strKey As String = ""
        Try
            'Conn.ConnectionString = ConnectionString.GetConStr
            Conn.Open()

            cmd.Connection = Conn
            Dim Da As SqlDataAdapter = New SqlDataAdapter(cmd)
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

    Public Shared Function Executer_Scalar(ByVal ResultCol As String, ByVal FilterField As String, ByVal FilterValue As String, ByVal TableName As String) As String
        Dim objCommand As SqlCommand = New SqlCommand
        Dim strQuery As New StringBuilder
        strQuery.Append("Select " & ResultCol & " from " & TableName + " where " & FilterField & " = '" & FilterValue & "'")
        objCommand.CommandText = strQuery.ToString()
        Dim rslt As String = General_Executer_Scalar(objCommand)
        Return rslt
    End Function

    Public Shared Function Executer_Scalar(ByVal Query As String) As String
        Dim objCommand As SQLCommand = New SQLCommand
        Dim strQuery As New StringBuilder
        strQuery.Append(Query)
        objCommand.CommandText = strQuery.ToString()
        Dim rslt As String = General_Executer_Scalar(objCommand)
        Return rslt
    End Function

    Public Shared Function GetNextPKey(ByVal PKName As String, ByVal PKValue As String, ByVal FilterField As String, ByVal TableName As String, ByVal Filterstr As String) As Guid
        Dim objCommand As SqlCommand = New SqlCommand
        Dim strQuery As New StringBuilder
        String.Format("")
        If Filterstr = "" Then
            strQuery.Append(String.Format("Select Top 1 {0} from {1} where {2} = (select min({2}) from {1} where {2} > (select {2} from {1} where {0} =  @PKValue))", PKName, TableName, FilterField))
            'strQuery.Append("Select Top 1 " & ResultCol & " from " & TableName + " where " & FilterField & " = (select min(" & FilterField & ") from [Role] where " & FilterField & " > @FilterValue)")
        Else
            strQuery.Append(String.Format("Select Top 1 {0} from {1} where {2} = (select min({2}) from {1} where {2} > (select {2} from {1} where {0} =  @PKValue )and {3}) ", PKName, TableName, FilterField, Filterstr))
            'strQuery.Append("Select Top 1 " & ResultCol & " from " & TableName + " where " & FilterField & " = (select min(" & FilterField & ") from [Role] where " & FilterField & " > @FilterValue)" & " and " & Filterstr)
        End If
        objCommand.Parameters.Add(New SqlParameter("@PKValue", PKValue))
        objCommand.CommandText = strQuery.ToString()
        Dim rslt As String = General_Executer_Scalar(objCommand)
        If String.IsNullOrEmpty(rslt) Or rslt = "0" Then
            Return Guid.Empty
        Else
            Return New Guid(rslt)
        End If
    End Function

    Public Shared Function GetPrevPKey(ByVal PKName As String, ByVal PKValue As String, ByVal FilterField As String, ByVal TableName As String, ByVal Filterstr As String) As Guid
        Dim objCommand As SqlCommand = New SqlCommand
        Dim strQuery As New StringBuilder
        If Filterstr = "" Then
            strQuery.Append(String.Format("Select Top 1 {0} from {1} where {2} = (select max({2}) from {1} where {2} < (select {2} from {1} where {0} =  @PKValue))", PKName, TableName, FilterField))
            'strQuery.Append("Select Top 1  " & ResultCol & " from " & TableName + " where CreationDate = (select max(CreationDate) from [Role] where CreationDate < (select CreationDate from [role] where GUID =  @FilterValue))")
        Else
            strQuery.Append(String.Format("Select Top 1 {0} from {1} where {2} = (select max({2}) from {1} where {2} < (select {2} from {1} where {0} =  @PKValue )and {3}) ", PKName, TableName, FilterField, Filterstr))
            'strQuery.Append("Select Top 1 " & ResultCol & " from " & TableName + " where " & FilterField & " = (select max(" & FilterField & ") from [Role] where " & FilterField & " < @FilterValue)" & " and " & Filterstr)
        End If
        objCommand.Parameters.Add(New SqlParameter("@PKValue", PKValue))
        objCommand.CommandText = strQuery.ToString()
        Dim rslt As String = General_Executer_Scalar(objCommand)
        If String.IsNullOrEmpty(rslt) Or rslt = "0" Then
            Return Guid.Empty
        Else
            Return New Guid(rslt)
        End If
    End Function
    Public Shared Function GetFirstPKey(ByVal PKName As String, ByVal FilterField As String, ByVal TableName As String, ByVal Filterstr As String) As Guid
        Dim objCommand As SQLCommand = New SQLCommand
        Dim strQuery As New StringBuilder
        If Filterstr = "" Then
            strQuery.Append("Select " & PKName & " from " & TableName & " where " & FilterField & " = (select min(" & FilterField & ") from " & TableName & " ) ")
        Else
            strQuery.Append("Select " & PKName & " from " & TableName & " where " & FilterField & " = (select min(" & FilterField & ") from " & TableName & " where " & Filterstr & " ) ")
        End If

        objCommand.CommandText = strQuery.ToString()
        Dim rslt As String = General_Executer_Scalar(objCommand)
        If String.IsNullOrEmpty(rslt) Or rslt = "0" Then
            Return Guid.Empty
        Else
            Return New Guid(rslt)
        End If
    End Function

    Public Shared Function GetLastPKey(ByVal PKName As String, ByVal FilterField As String, ByVal TableName As String, ByVal Filterstr As String) As Guid
        Dim objCommand As SQLCommand = New SQLCommand
        Dim strQuery As New StringBuilder
        If Filterstr = "" Then
            strQuery.Append("Select " & PKName & " from " & TableName & " where " & FilterField & " = (select max(" & FilterField & ") from " & TableName & " ) ")
        Else
            strQuery.Append("Select " & PKName & " from " & TableName & " where " & FilterField & " = (select max(" & FilterField & ") from " & TableName & " where " & Filterstr & " ) ")
        End If
        objCommand.CommandText = strQuery.ToString()
        Dim rslt As String = General_Executer_Scalar(objCommand)
        If String.IsNullOrEmpty(rslt) Or rslt = "0" Then
            Return Guid.Empty
        Else
            Return New Guid(rslt)
        End If
    End Function

    Public Shared Function GetRecordCount(ByVal TableName As String, ByVal Filterstr As String) As String
        Dim objCommand As SQLCommand = New SQLCommand
        Dim strQuery As New StringBuilder
        If Filterstr = "" Then
            strQuery.Append("Select Count(*) from " + TableName)
        Else
            strQuery.Append("Select Count(*) from " + TableName & " where " & Filterstr)
        End If
        objCommand.CommandText = strQuery.ToString()
        Dim rslt As String = General_Executer_Scalar(objCommand)
        Return rslt
    End Function

    Public Shared Function CheckRecordFound(ByVal FieldName As String, ByVal FieldValue As String, ByVal TableName As String) As String
        Dim objCommand As SQLCommand = New SQLCommand
        Dim strQuery As New StringBuilder
        strQuery.Append("Select Count(*) from " & TableName & " where " & FieldName + " = '" & FieldValue & "'")
        objCommand.CommandText = strQuery.ToString()
        Dim rslt As String = General_Executer_Scalar(objCommand)
        Return rslt
    End Function

    'Public Shared Function GetNewPKey(ByVal FieldName As String, ByVal TableName As String) As Guid
    '    'Dim objCommand As SQLCommand = New SQLCommand
    '    'Dim strQuery As New StringBuilder
    '    'strQuery.Append("Select max(" + FieldName + ") + 1 from " + TableName)
    '    'objCommand.CommandText = strQuery.ToString()
    '    'Dim rslt As String = General_Executer_Scalar(objCommand)
    '    'If rslt <= "0" Then
    '    '    Return 1
    '    'Else
    '    '    Return rslt
    '    'End If
    '    Return Guid.NewGuid()
    'End Function
End Class
