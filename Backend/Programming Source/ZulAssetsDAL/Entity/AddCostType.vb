
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class AddCostType
        Implements IEntity

#Region "Data Members"
        Private objattAddCostType As attAddCostType
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattAddCostType = New attAddCostType
        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattAddCostType
            End Get
            Set(ByVal Value As IAttribute)
                objattAddCostType = CType(Value, attAddCostType)
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
            strQuery.Append("insert into AddCostType")
            strQuery.Append(" (TypeID,TypeDesc,Isdeleted)")
            strQuery.Append(" Values(?,?,0)")
            objCommand.Parameters.Add(New OleDbParameter("@TypeID", objattAddCostType.TypeID))
            objCommand.Parameters.Add(New OleDbParameter("@TypeDesc", objattAddCostType.TypeDesc))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AddCostType")
            strQuery.Append(" set TypeDesc=?")
            strQuery.Append(" where TypeID =?")
            objCommand.Parameters.Add(New OleDbParameter("@TypeDesc", objattAddCostType.TypeDesc))
            objCommand.Parameters.Add(New OleDbParameter("@TypeID", objattAddCostType.TypeID))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select TypeID,TypeDesc from AddCostType where IsDeleted = 0")
            If objattAddCostType.TypeID <> 0 Then
                strQuery.Append(" and TypeID = " & objattAddCostType.TypeID)
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from AddCostType")
                strQuery.Append(" where TypeID =" & objattAddCostType.TypeID)
            Else
                strQuery.Append("update AddCostType")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where TypeID =" & objattAddCostType.TypeID)
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(TypeID) + 1 from AddCostType")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function
        Public Function GetDatabyName(ByVal TypeDesc As String) As IDbCommand Implements IEntity.GetDatabyID
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select TypeID from AddCostType where TypeDesc = ?")
            objCommand.Parameters.Add(New OleDbParameter("@TypeDesc", TypeDesc))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
#End Region

    End Class

End Namespace
