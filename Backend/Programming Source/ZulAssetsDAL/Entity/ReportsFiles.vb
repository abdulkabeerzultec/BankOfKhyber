Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class ReportsFiles
        Implements IEntity

#Region "Data Members"
        Private objRptFile As attReports
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objRptFile = New attReports
        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objRptFile
            End Get
            Set(ByVal Value As IAttribute)
                objRptFile = CType(Value, attReports)
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
        Public Function Insert() As IDbCommand Implements IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into ReportsFiles")
            strQuery.Append(" (ReportName,ReportData,Query,Type,IsDeleted)")
            strQuery.Append(" Values (?,?,?,?,0)")
            objCommand.Parameters.Add(New OleDbParameter("@ReportName", objRptFile.ReportName))
            objCommand.Parameters.Add(New OleDbParameter("@ReportData", objRptFile.ReportData))
            objCommand.Parameters.Add(New OleDbParameter("@Query", objRptFile.Query))
            If objRptFile.Type Then
                objCommand.Parameters.Add(New OleDbParameter("@Type", 1))
            Else
                objCommand.Parameters.Add(New OleDbParameter("@Type", 0))
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update ReportsFiles")
            strQuery.Append(" set")
            strQuery.Append(" ReportData=?, Query=?, Type=?")
            strQuery.Append(" where ReportName =?")

            objCommand.Parameters.Add(New OleDbParameter("@ReportData", objRptFile.ReportData))
            objCommand.Parameters.Add(New OleDbParameter("@Query", objRptFile.Query))
            If objRptFile.Type Then
                objCommand.Parameters.Add(New OleDbParameter("@Type", 1))
            Else
                objCommand.Parameters.Add(New OleDbParameter("@Type", 0))
            End If
            objCommand.Parameters.Add(New OleDbParameter("@ReportName", objRptFile.ReportName))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select ReportName,Query,CAST(Type as CHAR(1)) as Type,ReportData from ReportsFiles")
            If objRptFile.ReportName <> "" Then
                strQuery.Append(" where ReportName = '" & objRptFile.ReportName & "'")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetDatabyID(ByVal Id As String) As IDbCommand Implements IEntity.GetDatabyID
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select ReportData from ReportsFiles where ReportName = ?")
            objCommand.Parameters.Add(New OleDbParameter("@ReportName", Id))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetRoleDatabyID(ByVal Id As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select ReportName from RoleReports where RoleID = ?")
            objCommand.Parameters.Add(New OleDbParameter("@RoleID", Id))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function AddRoleReportData(ByVal RoleID As String, ByVal ReportName As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into RoleReports values(?,?,0)")
            objCommand.Parameters.Add(New OleDbParameter("@RoleID", RoleID))
            objCommand.Parameters.Add(New OleDbParameter("@ReportName", ReportName))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function DeleteRoleReportData(ByVal RoleID As String, ByVal ReportName As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete RoleReports  where ReportName = ?")
            Else
                strQuery.Append("update RoleReports set IsDeleted= 1 where ReportName = ?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@ReportName", ReportName))

            If RoleID <> "" Then
                strQuery.Append(" and RoleID = ? ")
                objCommand.Parameters.Add(New OleDbParameter("@RoleID", RoleID))
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function CheckExistRoleReportData(ByVal RoleID As String, ByVal ReportName As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select Count(*) from RoleReports  where RoleID = ? and ReportName = ?")
            objCommand.Parameters.Add(New OleDbParameter("@RoleID", RoleID))
            objCommand.Parameters.Add(New OleDbParameter("@ReportName", ReportName))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function GetExtended() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select ReportName,query from ReportsFiles where type = 1 and IsDeleted = 0")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
         
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from  ReportsFiles")
            Else
                strQuery.Append("update ReportsFiles set IsDeleted = 1")
            End If

            strQuery.Append(" where ReportName = ?")
            objCommand.Parameters.Add(New OleDbParameter("@ReportName", objRptFile.ReportName))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
#End Region

    End Class
End Namespace



