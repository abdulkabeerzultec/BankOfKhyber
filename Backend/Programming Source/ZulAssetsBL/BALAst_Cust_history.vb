

Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALAst_Cust_history
        Inherits BLBase

#Region "Data Members"
        Private objAst_Cust_history As Ast_Cust_history
#End Region


        Public Sub New()
            objAst_Cust_history = New Ast_Cust_history
        End Sub

#Region "Functions"
        Public Sub Insert_Ast_Cust_history(ByVal objattAst_Cust_history As attAst_Cust_history)
            Try
                objAst_Cust_history.Attribute = objattAst_Cust_history
                Me.Insert(objAst_Cust_history)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function Delete_Ast_Cust_history(ByVal objattAst_Cust_history As attAst_Cust_history) As Boolean
            Try
                objAst_Cust_history.Attribute = objattAst_Cust_history
                Me.Delete(objAst_Cust_history)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Update_Ast_Cust_history(ByVal objattAst_Cust_history As attAst_Cust_history) As Boolean
            Try
                objAst_Cust_history.Attribute = objattAst_Cust_history
                Me.Update(objAst_Cust_history)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_Ast_Cust_history(ByVal objattAst_Cust_history As attAst_Cust_history) As DataTable
            Try
                objAst_Cust_history.Attribute = objattAst_Cust_history
                Return GetAllData(objAst_Cust_history)
            Catch ex As Exception
                Throw ex
            End Try


        End Function

        


        Public Function GetNextPKey_AstHistory() As String
            Try
                Return Me.GetNextPKey(objAst_Cust_history)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region
    End Class

End Namespace

