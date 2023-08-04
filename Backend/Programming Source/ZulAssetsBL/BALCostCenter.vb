

Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALCostCenter
        Inherits BLBase

#Region "Data Members"
        Private objCostCenter As CostCenter
#End Region


        Public Sub New()
            objCostCenter = New CostCenter
        End Sub

#Region "Functions"
        Public Function Check_Child(ByVal _id As String, ByVal formid As Integer) As DataTable
            Try

                Return Me.GeneralExecuter(objCostCenter.Check_CostCenterChilds(_id, formid))
            Catch ex As Exception
                Throw ex
            End Try


        End Function

        Public Function Insert_CostCenter(ByVal objattCostCenter As attCostCenter) As String
            Try
                objCostCenter.Attribute = objattCostCenter
                Me.Insert(objCostCenter)
                Return Me.GetNextPKey_CostCenter()
            Catch ex As Exception
                Throw ex
            End Try
            Return ""
        End Function


        Public Function Update_CostCenter(ByVal objattCostCenter As attCostCenter) As Boolean
            Try
                objCostCenter.Attribute = objattCostCenter
                Me.Update(objCostCenter)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_CostCenter() As String
            Try
                Return Me.GetNextPKey(objCostCenter)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAll_CostCenter(ByVal objattCostCenter As attCostCenter) As DataTable
            Try
                objCostCenter.Attribute = objattCostCenter
                Return GetAllData(objCostCenter)

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetAllData_GetCombo(ByVal objattCostCenter As attCostCenter) As DataTable
            Try
                objCostCenter.Attribute = objattCostCenter
                Return Me.GeneralExecuter(objCostCenter.GetCombo())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetComboList(ByVal objattCostCenter As attCostCenter) As DataTable
            Try
                objCostCenter.Attribute = objattCostCenter
                Return Me.GeneralExecuter(objCostCenter.GetComboList)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function CheckID(ByVal objattCostCenter As attCostCenter) As DataTable
            Try
                objCostCenter.Attribute = objattCostCenter
                Return Me.GeneralExecuter(objCostCenter.CheckID())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Delete_CostCenter(ByVal objattCostCenter As attCostCenter) As Boolean
            Try
                objCostCenter.Attribute = objattCostCenter
                Me.Delete(objCostCenter)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCodeIDByNumber(ByVal CostNumber As String) As String
            Return Me.GeneralExecuter_Scalar(objCostCenter.GetCodeIDByNumber(CostNumber), "")
        End Function


        Public Function CostNumberExist(ByVal objatt As attCostCenter, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("CostNumber", objatt.CostNumber, "CostCenter", IsInsertStatus, "CostID", objatt.PKeyCode)
        End Function

        Public Function CostNameExist(ByVal objatt As attCostCenter, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("CostName", objatt.CostName, "CostCenter", IsInsertStatus, "CostID", objatt.PKeyCode)
        End Function

#End Region
    End Class

End Namespace