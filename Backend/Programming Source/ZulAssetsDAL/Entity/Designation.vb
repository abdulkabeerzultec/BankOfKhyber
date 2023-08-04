Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class Designation
        Implements IEntity

#Region "Data Members"
        Private objattDesignation As attDesignation
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattDesignation = New attDesignation

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattDesignation
            End Get
            Set(ByVal Value As IAttribute)
                objattDesignation = CType(Value, attDesignation)
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
            strQuery.Append("insert into Designation")
            strQuery.Append(" (DesignationID,Description,Isdeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" (" & Convert.ToString(objattDesignation.PKeyCode) & ",'" & objattDesignation.Description & "',0) ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Designation")
            strQuery.Append(" set")
            strQuery.Append(" Description='" & objattDesignation.Description & "'")
            strQuery.Append(" where DesignationID =" & objattDesignation.PKeyCode)
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
 

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DbType = "1" Then
                strQuery.Append("Select DesignationID as 'DesignationID',Description from Designation where IsDeleted = 0")
                If objattDesignation.PKeyCode <> 0 Then
                    strQuery.Append(" and DesignationID =" & objattDesignation.PKeyCode)
                End If
                strQuery.Append(" order by DesignationID")
            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append("Select DesignationID as DesignationID,Description from Designation where IsDeleted = 0")
                If objattDesignation.PKeyCode <> 0 Then
                    strQuery.Append(" and DesignationID =" & objattDesignation.PKeyCode)
                End If
                strQuery.Append(" order by DesignationID")
            Else
                strQuery.Append("Select DesignationID as 'DesignationID',Description from Designation where IsDeleted = 0")
                If objattDesignation.PKeyCode <> 0 Then
                    strQuery.Append(" and DesignationID =" & objattDesignation.PKeyCode)
                End If
                strQuery.Append(" order by DesignationID")
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
                strQuery.Append("delete from Designation")
                strQuery.Append(" where DesignationID =" & objattDesignation.PKeyCode)
            Else
                strQuery.Append("update Designation")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where DesignationID =" & objattDesignation.PKeyCode)

            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetDesignationID() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select DesignationID,Description from Designation")
            If objattDesignation.Description <> "" Then
                strQuery.Append(" where Description = '" & objattDesignation.Description & "'")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(DesignationID)+1 from Designation")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
#End Region

    End Class

End Namespace