Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class DepreciationMethod
        Implements IEntity

#Region "Data Members"
        Private objattDepriciationMethod As attDepreciationMethod
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattDepriciationMethod = New attDepreciationMethod
        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattDepriciationMethod
            End Get
            Set(ByVal Value As IAttribute)
                objattDepriciationMethod = CType(Value, attDepreciationMethod)
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

#Region " Methods "
        Public Function Insert() As IDbCommand Implements IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into Depreciation_Method")
            strQuery.Append(" (DepCode,DepDesc,Isdeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" (" & Convert.ToString(objattDepriciationMethod.PKeyCode) & ",'" & objattDepriciationMethod.DepDesc & "',0) ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Depreciation_Method")
            strQuery.Append(" set")
            strQuery.Append(" DepDesc='" & objattDepriciationMethod.DepDesc & "'")
            strQuery.Append(" where DepCode =" & objattDepriciationMethod.PKeyCode)
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand


            Dim strQuery As New StringBuilder
            strQuery.Append("Select DepCode,DepDesc from Depreciation_Method where IsDeleted = 0")
            If objattDepriciationMethod.PKeyCode <> 0 Then
                strQuery.Append(" and DepCode = " & objattDepriciationMethod.PKeyCode)
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
                strQuery.Append("delete from Depreciation_Method")
                strQuery.Append(" where DepCode =" & objattDepriciationMethod.PKeyCode)
            Else

                strQuery.Append("update Depreciation_Method")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where DepCode =" & objattDepriciationMethod.PKeyCode)
            End If




            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(DepCode)+1 from Depreciation_Method")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
#End Region

    End Class

End Namespace
