

Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALAst_Sold
        Inherits BLBase

#Region "Data Members"
        Private objAst_Sold As Ast_Sold
#End Region


        Public Sub New()
            objAst_Sold = New Ast_Sold
        End Sub

#Region "Functions"
        Public Function Insert_Ast_Sold(ByVal objattAst_Sold As attAst_Sold) As String
            Try
                objAst_Sold.Attribute = objattAst_Sold
                Me.Insert(objAst_Sold)
                Return Me.GetNextPKey_Ast_Sold()
            Catch ex As Exception

            End Try
            Return ""
        End Function


        Public Function Update_Ast_Sold(ByVal objattAst_Sold As attAst_Sold) As Boolean
            Try
                objAst_Sold.Attribute = objattAst_Sold
                Me.Update(objAst_Sold)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function GetNextPKey_Ast_Sold() As String
            Return Me.GetNextPKey(objAst_Sold)
        End Function

        Public Function GetAll_Ast_Sold(ByVal objattAst_Sold As attAst_Sold) As DataTable

            objAst_Sold.Attribute = objattAst_Sold
            Return GetAllData(objAst_Sold)

        End Function

#End Region
    End Class

End Namespace