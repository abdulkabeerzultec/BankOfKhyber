Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL

Namespace ZulAssetBAL


Public Class BALPurchaseOrder
    Inherits BLBase

#Region "Data Members"
        Private objPurchaseOrder As ZulAssetsDAL.ZulAssetsDAL.PurchaseOrder
#End Region

    Public Sub New()
            objPurchaseOrder = New ZulAssetsDAL.ZulAssetsDAL.PurchaseOrder
    End Sub

#Region "Functions"
        Public Function Check_Child(ByVal _id As String) As String
            Try
                Return Me.GeneralExecuter_Scalar(objPurchaseOrder.Check_Child(_id), "")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Insert_PurchaseOrder(ByVal objattPurchaseOrder As attPurchaseOrder) As String
            Try
                objPurchaseOrder.Attribute = objattPurchaseOrder
                Me.Insert(objPurchaseOrder)
                Return Me.GetNextPKey(objPurchaseOrder)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Delete_PurchaseOrder(ByVal objattPurchaseOrder As attPurchaseOrder) As String
            Try
                objPurchaseOrder.Attribute = objattPurchaseOrder
                Me.Delete(objPurchaseOrder)
                Dim objattPODetails As New attPODetails
                Dim objBALPODetails As New BALPODetails
                objattPODetails.POCode = objattPurchaseOrder.PKeyCode
                objBALPODetails.Delete_PODetails(objattPODetails)
                Return Me.GetNextPKey(objPurchaseOrder)
            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Public Function Update_PurchaseOrder(ByVal objattPurchaseOrder As attPurchaseOrder) As Boolean
            Try
                objPurchaseOrder.Attribute = objattPurchaseOrder
                Me.Update(objPurchaseOrder)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function UpdatePOStatus(ByVal objattPurchaseOrder As attPurchaseOrder) As Boolean
            Try
                objPurchaseOrder.Attribute = objattPurchaseOrder
                Me.GeneralExecuter(objPurchaseOrder.UpdatePOStatus)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetNextPKey_PurchaseOrder() As String
            Try
                Return Me.GetNextPKey(objPurchaseOrder)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetAll_PurchaseOrder(ByVal objattPurchaseOrder As attPurchaseOrder) As DataTable
            objPurchaseOrder.Attribute = objattPurchaseOrder
            Return GetAllData(objPurchaseOrder)
        End Function
        Public Function GetAllData_GetCombo(ByVal objattPurchaseOrder As attPurchaseOrder) As DataTable
            Try
                objPurchaseOrder.Attribute = objattPurchaseOrder
                Return Me.GeneralExecuter(objPurchaseOrder.GetCombo())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Sub Transfer(ByVal objattPurchaseOrder As attPurchaseOrder)
            Try
                objPurchaseOrder.Attribute = objattPurchaseOrder
                Me.TransactionExecuter(objPurchaseOrder.Transfer)
            Catch ex As Exception
                Throw ex
            End Try

        End Sub

#End Region
End Class

End Namespace