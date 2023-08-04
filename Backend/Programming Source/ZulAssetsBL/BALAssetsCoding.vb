Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL

Namespace ZulAssetBAL
    Public Class BALAssetsCoding
        Inherits BLBase

#Region "Data Members"
        Private objAssetCoding As AssetCoding
#End Region
        Public Sub New()
            objAssetCoding = New AssetCoding
        End Sub

#Region "Functions"


        Public Function CloseRange(ByVal objattAssetCoding As attAssetsCoding) As Boolean
            Try
                objAssetCoding.Attribute = objattAssetCoding
                Me.TransactionExecuter(objAssetCoding.CloseRange())
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Verify_Range(ByVal Start As Long, ByVal EndRange As Long, ByVal ID As String) As DataTable
            Try
                Return Me.GeneralExecuter(objAssetCoding.VerfiY_Range(Start, EndRange, ID))
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Insert_AssetCoding(ByVal objattAssetCoding As attAssetsCoding) As String
            Try
                objAssetCoding.Attribute = objattAssetCoding
                Me.Insert(objAssetCoding)
                Return Me.GetNextPKey_AssetCoding()
            Catch ex As Exception
                Throw ex
            End Try
            Return ""
        End Function


        Public Function Update_AssetCoding(ByVal objattAssetCoding As attAssetsCoding) As Boolean
            Try
                objAssetCoding.Attribute = objattAssetCoding
                Me.Update(objAssetCoding)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_AssetCoding() As String
            Try
                Return Me.GetNextPKey(objAssetCoding)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAll_AssetCoding(ByVal objattAssetCoding As attAssetsCoding) As DataTable
            Try
                objAssetCoding.Attribute = objattAssetCoding
                Return GetAllData(objAssetCoding)

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function Delete_AssetCoding(ByVal objattAssetCoding As attAssetsCoding) As Boolean
            Try
                objAssetCoding.Attribute = objattAssetCoding
                Me.Delete(objAssetCoding)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAllCompanyCodingDefsIDs(ByVal CompanyID As Integer) As DataTable
            Try
                Return Me.GeneralExecuter(objAssetCoding.GetCompanyCodingDefsIDs(CompanyID))
            Catch ex As Exception
                Throw ex
            End Try

        End Function

#End Region
    End Class

End Namespace
