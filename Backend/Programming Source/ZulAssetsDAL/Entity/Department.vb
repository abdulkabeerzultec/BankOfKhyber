
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class Department
        Implements IEntity

#Region "Data Members"
        Private objattDepartment As attDepartment
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattDepartment = New attDepartment

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattDepartment
            End Get
            Set(ByVal Value As IAttribute)
                objattDepartment = CType(Value, attDepartment)
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
            strQuery.Append("insert into Department")
            strQuery.Append(" (DeptId,DeptName,Isdeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" (" & Convert.ToString(objattDepartment.PKeyCode) & ",'" & objattDepartment.DeptName & "',0) ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Department")
            strQuery.Append(" set")
            strQuery.Append(" DeptName='" & objattDepartment.DeptName & "'")
            strQuery.Append(" where DeptId =" & objattDepartment.PKeyCode & "")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select DeptId,DeptName from Department where IsDeleted = 0")
            If objattDepartment.PKeyCode <> "" Then
                strQuery.Append(" and DeptId = " & objattDepartment.PKeyCode)
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from Department")
                strQuery.Append(" where DeptId =" & objattDepartment.PKeyCode)
            Else
                strQuery.Append("update Department")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where DeptId =" & objattDepartment.PKeyCode)

            End If




            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(DeptId)+1 from Department")
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
            Return objCommand
        End Function
#End Region

    End Class

End Namespace
