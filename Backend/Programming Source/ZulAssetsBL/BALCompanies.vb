Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL

Namespace ZulAssetBAL
    Public Class BALCompany
        Inherits BLBase

#Region "Data Members"
        Private objCompany As Company
        ''Private objBALRoles As BALRoles
#End Region


        Public Sub New()
            objCompany = New Company
        End Sub

#Region "Functions"
        Public Function Insert_Company(ByVal objattCompany As attcompany) As String
            Try

                objCompany.Attribute = objattCompany
                Me.Insert(objCompany)


                Return Me.GetNextPKey_Company()

            Catch ex As Exception
                Throw ex
            End Try
            Return ""
        End Function


        Public Function Update_Company(ByVal objattCompany As attcompany) As Boolean
            Try
                objCompany.Attribute = objattCompany
                Me.Update(objCompany)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetNextPKey_Company() As String
            Try
                Return Me.GetNextPKey(objCompany)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAll_Company(ByVal objattCompany As attcompany) As DataTable
            Try
                objCompany.Attribute = objattCompany
                Return GetAllData(objCompany)

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAllData_GetCombo(ByVal objattCompany As attcompany) As DataTable
            Try
                objCompany.Attribute = objattCompany
                Return Me.GeneralExecuter(objCompany.GetAllData_Combo())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetCompanyLastAssetNumber(ByVal CompanyID As String) As Int64
            Try
                Dim ds As DataTable = Me.GeneralExecuter(objCompany.GetLastAssetNumber(CompanyID))
                If ds IsNot Nothing Then
                    If ds.Rows.Count > 0 Then
                        Return ds.Rows(0)("LastAssetNumber")
                    End If
                End If
                Return ""
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetCompanyIDByCompanyCode(ByVal CompanyCode As String) As Int64
            Try
                Dim ds As DataTable = Me.GeneralExecuter(objCompany.GetCompanyIDByCode(CompanyCode))
                If ds IsNot Nothing Then
                    If ds.Rows.Count > 0 Then
                        Return ds.Rows(0)("CompanyId")
                    End If
                End If
                Return -1
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetCompanyCodeByCompanyID(ByVal CompanyID As String) As String
            Try
                Dim ds As DataTable = Me.GeneralExecuter(objCompany.GetCompanyCodeByID(CompanyID))
                If ds IsNot Nothing Then
                    If ds.Rows.Count > 0 Then
                        Return ds.Rows(0)("CompanyCode")
                    End If
                End If
                Return -1
            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Public Function SetCompanyLastAssetNumber(ByVal CompanyID As String, ByVal LastAssetNumber As Int64) As Boolean
            Try
                Me.GeneralExecuter(objCompany.SetLastAssetNumber(CompanyID, LastAssetNumber))
                Return True
            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Public Function GetAllData_Grid(ByVal objattCompany As attcompany) As DataTable
            Try
                objCompany.Attribute = objattCompany
                Return Me.GeneralExecuter(objCompany.GetAllData_Grid())
            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Public Function BarCode_Assign(ByVal objattCompany As attcompany) As DataTable
            Try
                objCompany.Attribute = objattCompany
                Return Me.GeneralExecuter(objCompany.BarCode_Assign())
            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Public Function IsThereRelatedBooks(ByVal objattCompany As attcompany) As Boolean
            Try
                objCompany.Attribute = objattCompany

                Dim ds As DataTable = Me.GeneralExecuter(objCompany.CheckRelatedBooks())
                If ds IsNot Nothing Then
                    If ds.Rows.Count > 0 Then
                        If ds.Rows(0)(0).ToString > 0 Then
                            Return True
                        End If
                    End If
                End If
                Return False

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function IsThereRelatedAssets(ByVal objattCompany As attcompany) As Boolean
            Try
                objCompany.Attribute = objattCompany

                Dim ds As DataTable = Me.GeneralExecuter(objCompany.CheckRelatedAssets())
                If ds IsNot Nothing Then
                    If ds.Rows.Count > 0 Then
                        If ds.Rows(0)(0).ToString > 0 Then
                            Return True
                        End If
                    End If
                End If
                Return False

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Private Function DeleteCompanyAssets(ByVal companyID As Integer) As Boolean
            Try

                Dim objattAssetDetails As New attAssetDetails
                Dim objBALAssetDetails As New BALAssetDetails

                For Each dr As DataRow In objBALAssetDetails.GetAllCompanyAssetsIDs(companyID).Rows

                    objattAssetDetails.PKeyCode = dr("AstID")
                    objBALAssetDetails.Delete_Details(objattAssetDetails)
                Next
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Private Function DeleteCompanyBooks(ByVal companyID As Integer) As Boolean
            Try

                Dim objattBooks As New attBook
                Dim objBALBooks As New BALBooks

                For Each dr As DataRow In objBALBooks.GetAllCompanyBooksIDs(companyID).Rows

                    objattBooks.PKeyCode = dr("BookID")
                    objBALBooks.Delete_Book(objattBooks)
                Next
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Private Function DeleteAssetCodingDefinitions(ByVal companyID As Integer) As Boolean
            Try
                Dim objattAssetsCoding As New attAssetsCoding
                Dim objBALAssetsCoding As New BALAssetsCoding

                For Each dr As DataRow In objBALAssetsCoding.GetAllCompanyCodingDefsIDs(companyID).Rows
                    objattAssetsCoding.PKeyCode = dr("AssetCodingID")
                    objBALAssetsCoding.Delete_AssetCoding(objattAssetsCoding)
                Next
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Delete_Company(ByVal objattCompany As attcompany) As Boolean
            Try
                objCompany.Attribute = objattCompany
                Dim CompanyID As Integer = objattCompany.PKeyCode
                Dim objBALRoles As New BALRoles
                objBALRoles.Update_Role_CompaniesID(CompanyID)
                'Delete Company Assets
                If DeleteCompanyAssets(CompanyID) Then
                    'Delete Company Books
                    If DeleteCompanyBooks(CompanyID) Then
                        'Delete AssetCoding Definitions
                        If DeleteAssetCodingDefinitions(CompanyID) Then
                            Me.Delete(objCompany)
                        End If
                    End If
                End If
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Public Function CompanyCodeExist(ByVal objatt As attcompany, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("CompanyCode", objatt.CompanyCode, "Companies", IsInsertStatus, "CompanyId", objatt.PKeyCode)
        End Function

        Public Function CompanyNameExist(ByVal objatt As attcompany, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("CompanyName", objatt.CompanyName, "Companies", IsInsertStatus, "CompanyId", objatt.PKeyCode)
        End Function
#End Region
    End Class

End Namespace