Imports System
Imports System.Windows.Forms
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL
Imports Microsoft.VisualBasic
Imports ZulAssetsDAL
Namespace ZulAssetBAL
    Public Class BALAlhadaIntegration
        Inherits ZulAssetBAL.BLBase
        Private objALHadaInteg As AlHadaIntegration

        Public Sub New()
            objALHadaInteg = New AlHadaIntegration
        End Sub

        Public Function Insert_AssetDetails_ExportServer(ByVal objattAssetDetails As attAssetDetails, ByVal Division As String, ByVal Category As String) As String
            Try
                Return GeneralExecuter_Scalar_ExportServer(objALHadaInteg.Insert_ExportServer(objattAssetDetails, Division, Category))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Update_AssetDetails_ExportServer(ByVal objattAssetDetails As attAssetDetails, ByVal Division As String, ByVal Category As String) As String
            Try
                Return GeneralExecuter_Scalar_ExportServer(objALHadaInteg.Update_ExportServer(objattAssetDetails, Division, Category))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Dispose_AssetDetails_ExportServer(ByVal objattAssetDetails As attAssetDetails) As String
            Try
                Return GeneralExecuter_Scalar_ExportServer(objALHadaInteg.Dispose_ExportServer(objattAssetDetails))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Insert_AssetChange_ExportServer(ByVal objattAssetDetails As attAssetDetails, ByVal Division As String, ByVal PrevDivision As String, ByVal Location As String, ByVal PrevLocation As String, ByVal Category As String, ByVal TransferDate As Date) As String
            Try
                Return GeneralExecuter_Scalar_ExportServer(objALHadaInteg.Insert_AstHistoryExportServer(objattAssetDetails, Division, PrevDivision, Location, PrevLocation, Category, TransferDate))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Update_AssetMaster_ExportServer(ByVal RefNo As String, ByVal Division As String, ByVal PrevDivision As String, ByVal Location As String, ByVal PrevLocation As String, ByVal TransferDate As Date) As String
            Try
                Return GeneralExecuter_Scalar_ExportServer(objALHadaInteg.Update_AstHistoryExportServer(RefNo, Division, PrevDivision, Location, PrevLocation, TransferDate))
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class
End Namespace