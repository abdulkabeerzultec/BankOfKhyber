Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALbrand
        Inherits BLBase

#Region "Data Members"
        Private objBrand As Brand
#End Region


        Public Sub New()
            objBrand = New Brand
        End Sub

#Region "Functions"
        Public Function Insert_Brand(ByVal objattBrand As attBrand) As String
            Try
                objBrand.Attribute = objattBrand
                Me.Insert(objBrand)
                Return Me.GetNextPKey_Brand()
            Catch ex As Exception
                Throw ex
            End Try
            Return ""
        End Function


        Public Function Update_Brand(ByVal objattBrand As attBrand) As Boolean
            Try
                objBrand.Attribute = objattBrand
                Me.Update(objBrand)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetNextPKey_Brand() As String
            Try
                Return Me.GetNextPKey(objBrand)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAll_Brand(ByVal objattBrand As attBrand) As DataTable
            Try
                objBrand.Attribute = objattBrand
                Return GetAllData(objBrand)

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function Delete_Brand(ByVal objattBrand As attBrand) As Boolean
            Try
                objBrand.Attribute = objattBrand
                Me.Delete(objBrand)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function BrandExist(ByVal objatt As attBrand, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("AstBrandName", objatt.AstBrandName, "Brand", IsInsertStatus, "AstBrandID", objatt.PKeyCode)
        End Function

#End Region
    End Class

End Namespace