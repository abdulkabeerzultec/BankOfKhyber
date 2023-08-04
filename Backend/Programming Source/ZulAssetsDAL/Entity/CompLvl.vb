Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL


    Public Class CompLvl
        Implements IEntity
#Region "Data Members"
        Private objattCompLvl As attCompLvl
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattCompLvl = New attCompLvl

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattCompLvl
            End Get
            Set(ByVal Value As IAttribute)
                objattCompLvl = CType(Value, attCompLvl)
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
            strQuery.Append("insert into CompLvl")
            strQuery.Append(" (LvlID,LvlDesc,Isdeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" (" & Convert.ToString(objattCompLvl.PKeyCode) & ",'" & objattCompLvl.LvlDesc & "',0) ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update CompLvl")
            strQuery.Append(" set")
            strQuery.Append(" LvlDesc='" & objattCompLvl.LvlDesc & "'")
            strQuery.Append(" where LvlID =" & objattCompLvl.PKeyCode & "")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select LvlID,LvlDesc  from CompLvl where IsDeleted = 0")
            If objattCompLvl.PKeyCode <> 0 Then
                strQuery.Append(" and LvlID = " & objattCompLvl.PKeyCode)
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from CompLvl")
                strQuery.Append(" where LvlID =" & objattCompLvl.PKeyCode)
            Else

                strQuery.Append("update CompLvl")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where LvlID =" & objattCompLvl.PKeyCode)
            End If



            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(LvlID)+1 from CompLvl")
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