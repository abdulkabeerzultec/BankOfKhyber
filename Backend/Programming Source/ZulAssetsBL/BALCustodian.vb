Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALCustodian
        Inherits BLBase
#Region "Data Members"
        Private objCustodian As Custodian
#End Region


        Public Sub New()
            objCustodian = New Custodian
        End Sub

#Region "Functions"
        Public Function Insert_Custodian(ByVal objattCustodian As attCustodian) As Boolean
            Try
                objCustodian.Attribute = objattCustodian
                Me.Insert(objCustodian)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function Update_Custodian(ByVal objattCustodian As attCustodian) As Boolean
            Try
                objCustodian.Attribute = objattCustodian
                Me.Update(objCustodian)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_Custodian() As String
            Try
                Return Me.GetNextPKey(objCustodian)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetAll_Custodian(ByVal objattCustodian As attCustodian) As DataTable
            Try
                objCustodian.Attribute = objattCustodian
                Return GetAllData(objCustodian)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function GetAllData_Custodian() As DataTable
            Try
                Return Me.GeneralExecuter(objCustodian.GetAllData_Custodian())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CheckID(ByVal objattCustodian As attCustodian) As DataTable
            Try
                objCustodian.Attribute = objattCustodian
                Return Me.GeneralExecuter(objCustodian.CheckID())
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetAllData_GetCombo() As DataTable
            Try
                Return Me.GeneralExecuter(objCustodian.GetAllData_Combo())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Delete_Custodian(ByVal objattCustodian As attCustodian) As Boolean
            Try
                objCustodian.Attribute = objattCustodian
                Me.Delete(objCustodian)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Check_Child(ByVal _id As String, ByVal formid As Integer) As DataTable
            Try

                Return Me.GeneralExecuter(objCustodian.Check_Custodian(_id, formid))
            Catch ex As Exception
                Throw ex
            End Try


        End Function

        Private Function Get_Cust_DeptID(ByVal CustID As String) As String
            Try
                Dim ds As New DataTable
                Dim objattCustodian As New attCustodian
                objattCustodian.PKeyCode = CustID
                ds = GetAll_Custodian(objattCustodian)
                If ds IsNot Nothing Then
                    Dim deptID As String = CStr(ds.Rows(0)("DeptID"))
                    Dim DeptIDSplit() As String = deptID.Split("-"c)
                    Return Format(CInt(DeptIDSplit(0)), "0#") & DeptIDSplit(1)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CustIDExist(ByVal objatt As attCustodian, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("CustodianID", objatt.PKeyCode, "Custodian", IsInsertStatus, "CustodianID", objatt.PKeyCode)
        End Function

        Public Function CustNameExist(ByVal objatt As attCustodian, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("CustodianName", objatt.CustodianName, "Custodian", IsInsertStatus, "CustodianID", objatt.PKeyCode)
        End Function
#End Region
    End Class
End Namespace