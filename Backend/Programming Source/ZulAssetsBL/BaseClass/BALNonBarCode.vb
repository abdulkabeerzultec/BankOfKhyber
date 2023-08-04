Imports System
Imports ZulAssetsDAL.ZulAssetsDAL

Namespace ZulAssetBAL
    Public Class BALAnonymousAsset
        Inherits BLBase

#Region "Data Members"
        Private objAnonymousAsset As AnonymousAsset
#End Region


        Public Sub New()
            objAnonymousAsset = New AnonymousAsset
        End Sub

#Region "Functions"
        Public Function Insert_Brand() As String
            Return ""
        End Function


        Public Function Update_AnonymousAsset() As Boolean
            Return ""
        End Function

        Public Function GetNextPKey_AnonymousAsset() As String
            Try
                Return ""
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAll_AnonymousAsset() As DataTable
            Try
                Return objAnonymousAsset.GetAllData_Temp
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Delete_AnonymousAsset(ByVal id As String, ByVal DeviceID As String, ByVal TransDate As DateTime) As Boolean
            Try
                objAnonymousAsset.Delete_Temp(id, DeviceID, TransDate)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Update_AnonymousRemarks(ByVal id As String, ByVal DeviceID As String, ByVal TransDate As DateTime, ByVal Remarks As String, ByVal InvSchID As Integer) As Boolean
            Try
                objAnonymousAsset.Update_Remarks(id, DeviceID, TransDate, Remarks, InvSchID)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
    End Class

End Namespace
