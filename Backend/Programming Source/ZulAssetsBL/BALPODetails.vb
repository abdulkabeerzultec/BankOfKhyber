Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL

Namespace ZulAssetBAL
    Public Class BALPODetails
        Inherits BLBase
#Region "Data Members"
        Private objPODetails As PODetails
#End Region

        Public Sub New()
            objPODetails = New PODetails
        End Sub

#Region "Functions"
        Public Function Insert_PODetails(ByVal objattPODetails As attPODetails) As String
            Try
                objPODetails.Attribute = objattPODetails
                Me.Insert(objPODetails)
                Return Me.GetNextPKey(objPODetails)
            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Public Function Update_PODetails(ByVal objattPODetails As attPODetails) As Boolean
            Try
                objPODetails.Attribute = objattPODetails
                Me.Update(objPODetails)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Delete_PODetails(ByVal objattPODetails As attPODetails) As Boolean

            Try
                objPODetails.Attribute = objattPODetails
                Me.Delete(objPODetails)
            Catch ex As Exception
                Throw ex
            End Try


        End Function

        Public Function GetNextPKey_PODetails() As String
            Try
                Return Me.GetNextPKey(objPODetails)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetAll_PODetails(ByVal objattPODetails As attPODetails) As DataTable
            Try
                objPODetails.Attribute = objattPODetails
                Return GetAllData(objPODetails)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Sub GetAll_Delete(ByVal objattPODetails As attPODetails)
            Try
                objPODetails.Attribute = objattPODetails
                Delete(objPODetails)
            Catch ex As Exception
                Throw ex
            End Try

        End Sub
        Public Sub Transfer(ByVal objattPODetails As attPODetails)
            Try
                objPODetails.Attribute = objattPODetails
                Me.TransactionExecuter(objPODetails.Transfer)
            Catch ex As Exception
                Throw ex
            End Try

        End Sub
        Public Sub UpdateReceivedQty(ByVal objattPODetails As attPODetails)
            Try
                objPODetails.Attribute = objattPODetails
                Me.TransactionExecuter(objPODetails.UpdateRecQty())
            Catch ex As Exception
                Throw ex
            End Try

        End Sub
#End Region
    End Class
End Namespace