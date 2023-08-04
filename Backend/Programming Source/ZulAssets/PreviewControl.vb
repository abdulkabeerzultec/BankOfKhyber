Imports DevExpress.XtraReports.UI
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO
Imports ZulAssetsBL.ZulAssetBAL

Public Class PreviewControl

    Private Sub PreviewControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Disable Export to PDF, because it's not supporting Arabic.
        If IntegrationName = IntegrationType.CMAIntegration Then
            'PrintPreviewBarCheckItem1.Enabled = False
        End If
    End Sub
End Class

