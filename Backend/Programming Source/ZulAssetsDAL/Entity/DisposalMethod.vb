Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class DisposalMethod
        Implements IEntity

#Region "Data Members"
        Private objattDisposalMethod As attDisposalMethod
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattDisposalMethod = New attDisposalMethod
        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattDisposalMethod
            End Get
            Set(ByVal Value As IAttribute)
                objattDisposalMethod = CType(Value, attDisposalMethod)
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
            strQuery.Append("insert into Disposal_Method")
            strQuery.Append(" (DispCode,DispDesc,Isdeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" (" & Convert.ToString(objattDisposalMethod.PKeyCode) & ",'" & objattDisposalMethod.DispDesc & "',0) ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Disposal_Method")
            strQuery.Append(" set")
            strQuery.Append(" DispDesc='" & objattDisposalMethod.DispDesc & "'")
            strQuery.Append(" where DispCode =" & objattDisposalMethod.PKeyCode)
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand


            Dim strQuery As New StringBuilder
            strQuery.Append("Select DispCode,DispDesc from Disposal_Method where IsDeleted = 0")
            If objattDisposalMethod.PKeyCode <> 0 Then
                strQuery.Append(" and DispCode = " & objattDisposalMethod.PKeyCode)
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
                strQuery.Append("delete from Disposal_Method")
                strQuery.Append(" where DispCode =" & objattDisposalMethod.PKeyCode)
            Else
                strQuery.Append("update Disposal_Method")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where DispCode =" & objattDisposalMethod.PKeyCode)
            End If



            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(DispCode)+1 from Disposal_Method")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
            Return objCommand
        End Function
#End Region

    End Class

End Namespace
