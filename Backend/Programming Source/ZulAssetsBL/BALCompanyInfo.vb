Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALCompanyInfo
        Inherits BLBase
#Region "Data Members"
        Private objCompanyInfo As CompanyInfo
#End Region


        Public Sub New()
            objCompanyInfo = New CompanyInfo
        End Sub

#Region "Functions"
        Public Sub Insert_CompanyInfo(ByVal objattCompanyInfo As attCompanyInfo)
            Try
                objCompanyInfo.Attribute = objattCompanyInfo
                Me.Insert(objCompanyInfo)
            Catch ex As Exception
                Throw ex
            End Try

            'Return Me.GetNextPKey(objCompanyInfo)
        End Sub


        Public Function Update_CompanyInfo(ByVal objattCompanyInfo As attCompanyInfo) As Boolean
            Try
                objCompanyInfo.Attribute = objattCompanyInfo
                Me.Update(objCompanyInfo)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_CompanyInfo() As String
            Try
                Return Me.GetNextPKey(objCompanyInfo)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetAll_CompanyInfo(ByVal objattCompanyInfo As attCompanyInfo) As DataTable
            Try
                objCompanyInfo.Attribute = objattCompanyInfo
                Return GetAllData(objCompanyInfo)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetAllData_DesignationName() As DataTable
            Try
                Return Me.GeneralExecuter(objCompanyInfo.GetAllData_DesignationName())
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetAllData_GetCombo() As DataTable
            Try
                Return Me.GeneralExecuter(objCompanyInfo.GetAllData_Combo())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Delete_CompanyInfo(ByVal objattCompanyInfo As attCompanyInfo) As Boolean
            Try
                objCompanyInfo.Attribute = objattCompanyInfo
                Me.Delete(objCompanyInfo)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
    End Class

End Namespace
