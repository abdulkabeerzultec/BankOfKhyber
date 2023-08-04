Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class CustomFields
        Implements IEntity

#Region "Data Members"
        Private objattCustomFields As attCustomFields
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattCustomFields = New attCustomFields

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattCustomFields
            End Get
            Set(ByVal Value As IAttribute)
                objattCustomFields = CType(Value, attCustomFields)
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
        Public Function Check_Unit(ByVal _id As String, ByVal formid As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Return objCommand
        End Function

        Public Function Insert() As IDbCommand Implements IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select ID,FormName,ControlName,EngCaption,ArabicCaption,UrduCaption,Example from CustomFields")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetFieldCaption(ByVal ControlName As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select EngCaption from CustomFields where ControlName=?")
            objCommand.Parameters.Add(New OleDbParameter("@ControlName", ControlName))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Return objCommand
        End Function
        Public Function GetDatabyID(ByVal Id As String) As IDbCommand Implements IEntity.GetDatabyID
            Dim objCommand As OleDbCommand = New OleDbCommand
            Return objCommand
        End Function
#End Region

    End Class

End Namespace
