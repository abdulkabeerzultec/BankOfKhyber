Imports ZulAssetsDAL.ZulAssetsDAL
Namespace ZulAssetBAL
    Public Class BALNMCIntegration
        Inherits BLBase
        Private objIntegrationQuery As New NahdiIntegration

        Public Function GetExportData(ByVal InvSchCode As Integer) As DataTable
            Try
                Return Me.GeneralExecuter(objIntegrationQuery.GetData(InvSchCode))
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DisableAssetTriggers() As DataTable
            Try
                Return Me.GeneralExecuter(objIntegrationQuery.DisableAssetTriggers())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function EnabbleAssetTriggers() As DataTable
            Try
                Return Me.GeneralExecuter(objIntegrationQuery.EnableAssetTriggers())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DisposeAllAssets() As DataTable
            Try
                Return Me.GeneralExecuter(objIntegrationQuery.DisposeAllAssets)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class
End Namespace
