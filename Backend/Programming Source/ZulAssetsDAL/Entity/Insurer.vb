Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL

Public Class Insurer
        Implements IEntity

#Region "Data Members"
        Private objattInsurer As attInsurer
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattInsurer = New attInsurer
        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattInsurer
            End Get
            Set(ByVal Value As IAttribute)
                objattInsurer = CType(Value, attInsurer)
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
            strQuery.Append("insert into Insurer")
            strQuery.Append(" (InsCode,InsName,Isdeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" (" & Convert.ToString(objattInsurer.PKeyCode) & ",N'" & objattInsurer.InsName & "',0) ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Insurer")
            strQuery.Append(" set")
            strQuery.Append(" InsName=N'" & objattInsurer.InsName & "'")
            strQuery.Append(" where InsCode =" & objattInsurer.PKeyCode)
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand


            Dim strQuery As New StringBuilder
            strQuery.Append("Select InsCode,InsName from Insurer where IsDeleted = 0")
            If objattInsurer.PKeyCode <> 0 Then
                strQuery.Append(" and HierCode = '" & objattInsurer.PKeyCode & "'")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
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


        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from Insurer")
                strQuery.Append(" where InsCode =" & objattInsurer.PKeyCode)
            Else
                strQuery.Append("update Insurer")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where InsCode =" & objattInsurer.PKeyCode)
            End If



            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk

            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(InsCode)+1 from Insurer")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

            Return objCommand
        End Function
#End Region

End Class

End Namespace