
Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALAst_History
        Inherits BLBase

#Region "Data Members"
        Private objAst_History As Ast_History
#End Region


        Public Sub New()
            objAst_History = New Ast_History
        End Sub

#Region "Functions"
        Public Function Check_Child(ByVal objattAstHistory As attAstHistory, ByVal JustChanged As Boolean) As String
            Try
                objAst_History.Attribute = objattAstHistory
                Return Me.GeneralExecuter_Scalar(objAst_History.Check_Child(JustChanged), "")
            Catch ex As Exception
                Throw ex
            End Try


        End Function

        Public Sub Insert_Ast_History(ByVal objattAstHistory As attAstHistory)
            Try
                objAst_History.Attribute = objattAstHistory
                Me.Insert(objAst_History)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

  

        Public Function delete_Ast_History(ByVal objattAstHistory As attAstHistory) As Boolean
            Try
                objAst_History.Attribute = objattAstHistory
                Me.Delete(objAst_History)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function delete_Ast_HistoryByInvSch(ByVal objattAstHistory As attAstHistory) As Boolean
            Try
                objAst_History.Attribute = objattAstHistory
                Me.GeneralExecuter(objAst_History.DeleteByInvSch)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Update_Ast_History(ByVal objattAstHistory As attAstHistory) As Boolean
            Try
                objAst_History.Attribute = objattAstHistory
                Me.Update(objAst_History)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_Ast_History(ByVal objattAstHistory As attAstHistory) As DataTable
            Try
                objAst_History.Attribute = objattAstHistory
                Return GetAllData(objAst_History)
            Catch ex As Exception
                Throw ex
            End Try


        End Function

        Public Function GetAllData_GetCombo() As DataTable
            Try
                Return Me.GeneralExecuter(objAst_History.GetAllData_Combo())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetNextPKey_AstHistory() As String
            Try
                Return Me.GetNextPKey(objAst_History)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region
    End Class

End Namespace

