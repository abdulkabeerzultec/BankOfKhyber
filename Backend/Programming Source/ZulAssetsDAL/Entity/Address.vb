
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class Address
        Implements IEntity

#Region "Data Members"


        Private objattAddress As attAddress
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattAddress = New attAddress

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattAddress
            End Get
            Set(ByVal Value As IAttribute)
                objattAddress = CType(Value, attAddress)
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
            strQuery.Append("insert into Address")
            strQuery.Append(" (AddressID,AddressDesc,Isdeleted)")
            strQuery.Append(" Values(?,?,0)")
            objCommand.Parameters.Add(New OleDbParameter("@AddressID", objattAddress.PKeyCode.ToString))
            objCommand.Parameters.Add(New OleDbParameter("@AddressDesc", objattAddress.AddressDesc.ToString))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Address")
            strQuery.Append(" set")
            strQuery.Append(" AddressDesc=?")
            strQuery.Append(" where AddressID =?")
            objCommand.Parameters.Add(New OleDbParameter("@AddressDesc", objattAddress.AddressDesc.ToString))
            objCommand.Parameters.Add(New OleDbParameter("@AddressID", objattAddress.PKeyCode.ToString))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select AddressID,AddressDesc from Address where IsDeleted = 0")
            If objattAddress.PKeyCode <> 0 Then
                strQuery.Append(" and AddressID = ?")
                objCommand.Parameters.Add(New OleDbParameter("@AddressID", objattAddress.PKeyCode.ToString))
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from Address")
                strQuery.Append(" where AddressID =?")
            Else
                strQuery.Append("update Address")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where AddressID =?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@AddressID", objattAddress.PKeyCode.ToString))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(AddressID)+1 from Address")
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
#End Region

    End Class

End Namespace
