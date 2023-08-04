Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class Brand
        Implements IEntity

#Region "Data Members"
        Private objattBrand As attBrand
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattBrand = New attBrand

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattBrand
            End Get
            Set(ByVal Value As IAttribute)
                objattBrand = CType(Value, attBrand)
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
            strQuery.Append("insert into Brand")
            strQuery.Append(" (AstBrandID,AstBrandName,Isdeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" (" & Convert.ToString(objattBrand.PKeyCode) & ",N'" & objattBrand.AstBrandName & "',0) ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Brand")
            strQuery.Append(" set")
            strQuery.Append(" AstBrandName=N'" & objattBrand.AstBrandName & "'")
            strQuery.Append(" where AstBrandID =" & objattBrand.PKeyCode & "")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select AstBrandID,AstBrandName from Brand where IsDeleted = 0")
            If objattBrand.PKeyCode <> "" Then
                strQuery.Append(" and AstBrandID = " & objattBrand.PKeyCode)
            End If

            If objattBrand.AstBrandName <> "" Then
                strQuery.Append(" and AstBrandName = '" & objattBrand.AstBrandName & "'")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from Brand")
                strQuery.Append(" where AstBrandID =" & objattBrand.PKeyCode)
            Else

                strQuery.Append("update Brand")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where AstBrandID =" & objattBrand.PKeyCode)
            End If



            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(AstBrandID)+1 from Brand")
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
