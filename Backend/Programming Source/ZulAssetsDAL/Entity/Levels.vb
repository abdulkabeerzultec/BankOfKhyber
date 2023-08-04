Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class Levels
        Implements IEntity

#Region "Data Members"
        Private objattLevels As attLevels
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattLevels = New attLevels
        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattLevels
            End Get
            Set(ByVal Value As IAttribute)
                objattLevels = CType(Value, attLevels)
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
            strQuery.Append("insert into levels")
            strQuery.Append(" (LvlCode,LvlDesc,Isdeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" (" & Convert.ToString(objattLevels.LvlCode) & ",' " & objattLevels.LvlDesc & "',0 )")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update levels")
            strQuery.Append(" set")
            strQuery.Append(" LvlDesc='" & objattLevels.LvlDesc & "'")
            strQuery.Append(" where lvlCode =" & objattLevels.PKeyCode)
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select lvlCode as Code,LvlDesc as Description from levels where IsDeleted = 0")
            If objattLevels.PKeyCode <> "" Then
                strQuery.Append(" and lvlCode = " & objattLevels.PKeyCode)
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function GetDatabyID(ByVal Id As String) As IDbCommand Implements IEntity.GetDatabyID
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select lvlCode as Code,LvlDesc as Description from levels where lvlCode = '" & Id & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(LvlCode)+1 from levels")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from levels")
                strQuery.Append(" where lvlCode =" & objattLevels.PKeyCode)
            Else
                strQuery.Append("update levels")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=true")
                strQuery.Append(" where lvlCode =" & objattLevels.PKeyCode)
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
#End Region

    End Class
End Namespace



