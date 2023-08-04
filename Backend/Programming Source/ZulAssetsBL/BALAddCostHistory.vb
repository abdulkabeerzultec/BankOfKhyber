Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALAddCostHistory
        Inherits BLBase

#Region "Data Members"
        Private objAddCostHistory As AddCostHistory
#End Region
        Public Sub New()
            objAddCostHistory = New AddCostHistory
        End Sub

#Region "Functions"
        Public Function Check_Child(ByVal _id As String) As String
            Try
                Return Me.GeneralExecuter_Scalar(objAddCostHistory.Check_Child(_id), "")
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function CopyAddCostHistory(ByVal OldAstID As String, ByVal NewAstID As String) As Boolean
            Try
                Me.GeneralExecuter(objAddCostHistory.CopyAddCost(Me.GetNextPKey_AddCostHistory, OldAstID, NewAstID))
                Return True
            Catch ex As Exception
                Throw ex
            End Try
            Return False
        End Function
        Public Function Insert_AddCostHistory(ByVal objattAddCostHistory As AttAddCostHistory) As String
            Try
                objAddCostHistory.Attribute = objattAddCostHistory
                Me.Insert(objAddCostHistory)
                Return Me.GetNextPKey_AddCostHistory()
            Catch ex As Exception
                Throw ex
            End Try
            Return ""
        End Function

        Public Function Update_AddCostHistory(ByVal objattAddCostHistory As AttAddCostHistory) As Boolean
            Try
                objAddCostHistory.Attribute = objattAddCostHistory
                Me.Update(objAddCostHistory)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_AddCostHistory() As String
            Try
                Return Me.GetNextPKey(objAddCostHistory)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_AddCostHistory(ByVal objattAddCostHistory As AttAddCostHistory) As DataTable
            Try
                objAddCostHistory.Attribute = objattAddCostHistory
                Return GetAllData(objAddCostHistory)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Delete_AddCostHistory(ByVal objattAddCostHistory As AttAddCostHistory) As Boolean
            Try
                objAddCostHistory.Attribute = objattAddCostHistory
                Me.Delete(objAddCostHistory)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Delete_AddCostHistoryByAstID(ByVal objattAddCostHistory As AttAddCostHistory) As Boolean
            Try
                objAddCostHistory.Attribute = objattAddCostHistory
                Me.GeneralExecuter(objAddCostHistory.DeleteByAstID())
                Return True
            Catch ex As Exception
                Throw ex
            End Try
            Return False
        End Function

        Public Function Check_ChildCostType(ByVal _id As String) As String
            Try
                Return Me.GeneralExecuter_Scalar(objAddCostHistory.Check_Child_CostType(_id), "")
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
    End Class

End Namespace
