Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class CompGroups
        Implements IEntity
#Region "Data Members"
        Private objattCompGroups As attCompGroups
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattCompGroups = New attCompGroups

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattCompGroups
            End Get
            Set(ByVal Value As IAttribute)
                objattCompGroups = CType(Value, attCompGroups)
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
            strQuery.Append("insert into CompGroups")
            strQuery.Append(" (GrpId,GrpDesc,LvlID,IsDeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" ('" & Convert.ToString(objattCompGroups.PKeyCode) & "','" & objattCompGroups.GrpDesc & "'," & objattCompGroups.LvlID & ",0) ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update CompGroups")
            strQuery.Append(" set")
            strQuery.Append(" GrpDesc='" & objattCompGroups.GrpDesc & "',")
            strQuery.Append(" LvlID= " & objattCompGroups.LvlID & " ")
            strQuery.Append(" where GrpId =" & objattCompGroups.PKeyCode & "")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Verify_Location(ByVal lvl As Integer, ByVal plvl As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append(" select GrpID,GrpDesc,LvlID from CompGroups")
            strQuery.Append(" where lvlID <" & lvl & " and lvlID >" & plvl)
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Check_Child(ByVal _Id As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append(" select GrpID  from CompGroups where  ")
            strQuery.Append("  CompGroups.LvlID = '" & _Id & "' and  isdeleted = 0")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append(" select  GrpID,GrpDesc,CompLvl.LvlID,LvlDesc from CompLvl inner join CompGroups on ")
            strQuery.Append("   CompLvl.LvlID  = CompGroups.LvlID where  CompGroups.IsDeleted = 0 and CompLvl.IsDeleted = 0  ")

            If objattCompGroups.PKeyCode <> "" Then
                strQuery.Append(" and CompGroups.GrpId = " & objattCompGroups.PKeyCode)
            End If

            If objattCompGroups.GrpDesc <> "" Then
                strQuery.Append(" and CompGroups.GrpDesc = '" & objattCompGroups.GrpDesc & "'")
            End If

            If AppConfig.DbType = "1" Then
                strQuery.Append(" order by CONVERT(bigint, GrpID)")
            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append(" order by Cast(GrpID as Number)")
            Else
                strQuery.Append(" order by (CONVERT(bigint, GrpID)")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from CompGroups")
                strQuery.Append(" where GrpId =" & objattCompGroups.PKeyCode)
            Else

                strQuery.Append("update CompGroups")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where GrpId =" & objattCompGroups.PKeyCode)
            End If



            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(GrpId)+1 from CompGroups")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function GetNextPKey(ByVal LevelID As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DbType = "1" Then
                strQuery.Append("Select ISNULL(max( CONVERT(bigint, GrpID)),0)+1 from CompGroups ")
            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append("Select max(Cast(GrpID as Number))+1 from CompGroups ")
            Else
                strQuery.Append("Select  ISNULL(max( CONVERT(bigint, GrpID)),0)+1 ")
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
            Return objCommand
        End Function
#End Region

    End Class
End Namespace

