
Imports System
Imports System.Data
Imports System.Data.OleDb.OleDbConnection

Namespace ZulAssetsDAL
    Public Interface IEntity
        Property ObjCommand() As IDbCommand
        Property ObjAttribute() As IAttribute
#Region "Methods"
        Function Insert() As IDbCommand
        Function Delete() As IDbCommand
        Function Update() As IDbCommand
        Function GetAllData() As IDbCommand
        Function GetDatabyID(ByVal ID As String) As IDbCommand
        Function GetNextPk() As IDbCommand
#End Region
    End Interface
End Namespace


