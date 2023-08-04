Imports ABBIntegration
Public Class frmMain

    Dim objSettings As New frmABBSettings
    Dim msgSub As String = "ZulAssets Sync Data Log"
    Private _msgBody As String = String.Empty
    Private Property msgBody() As String
        Get
            Return _msgBody
        End Get
        Set(ByVal value As String)
            _msgBody &= "Date  : " & Format(Now, "MMM dd, yyyy HH:mm") & Environment.NewLine & _
                        "Information : " & value & Environment.NewLine & _
                        "-----------------------------------------------------------" & _
                        "-----------------------------------------------------------" & _
                        Environment.NewLine & Environment.NewLine
        End Set
    End Property
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Visible = False
        Me.Hide()
        Me.Refresh()
        objSettings.LoadDatabaseConnection()
        objSettings.LoadSettings()
        objSettings.RoleID = 1

        Dim frmImp As New frmImport
        Dim frmExp As New frmExport
        Try
            If My.Application.CommandLineArgs.Count > 0 Then
                Dim SchName As String = My.Application.CommandLineArgs(0)
                If SchName = "-SchOutMasterCreate" Then
                    msgBody = frmImp.SilentImport(lblMessage, PB, objSettings.OutboundAssetMasterCreation, frmImport.TFileImportType.AssetCreate)
                    objSettings.SendEmail(msgSub, msgBody, False)
                End If
                If SchName = "-SchOutMasterChange" Then
                    msgBody = frmImp.SilentImport(lblMessage, PB, objSettings.OutboundAssetMasterChange, frmImport.TFileImportType.AssetChange)
                    objSettings.SendEmail(msgSub, msgBody, False)
                End If
                If SchName = "-SchOutRetire" Then
                    msgBody = frmImp.SilentImport(lblMessage, PB, objSettings.OutboundAssetRetirement, frmImport.TFileImportType.AssetRetirment)
                    objSettings.SendEmail(msgSub, msgBody, False)
                End If
                If SchName = "-SchOutValue" Then
                    msgBody = frmImp.SilentImport(lblMessage, PB, objSettings.OutboundAssetswithValue, frmImport.TFileImportType.AssetWithValue)
                    objSettings.SendEmail(msgSub, msgBody, False)
                End If
                If SchName = "-SchOutCapitalize" Then
                    msgBody = frmImp.SilentImport(lblMessage, PB, objSettings.OutboundAssetCapitalization, frmImport.TFileImportType.AssetCapitalization)
                    objSettings.SendEmail(msgSub, msgBody, False)
                End If
                If SchName = "-SchInAssetChange" Then
                    msgBody = frmExp.ExportDataFile(lblMessage, PB, objSettings.InboundAssetChange, frmExport.TFileExportType.AssetChange, True, False)
                    objSettings.SendEmail(msgSub, msgBody, False)
                End If
                If SchName = "-SchInPhysicalInv" Then
                    msgBody = frmExp.ExportDataFile(lblMessage, PB, objSettings.InboundPhysicalInventory, frmExport.TFileExportType.PhysicalInventory, True, False)
                    objSettings.SendEmail(msgSub, msgBody, False)
                End If
                If SchName = "-SchInGR" Then
                    Dim FileName As String = frmExp.GetExportFileName(objSettings.InboundGoodsReceiving, frmExport.TFileExportType.GoodsReceive)
                    msgBody = frmExp.ExportDataFile(lblMessage, PB, FileName, frmExport.TFileExportType.GoodsReceive, True, True)
                    objSettings.SendEmail(msgSub, msgBody, True)
                End If

                If SchName = "-SchInGI" Then
                    Dim FileName As String = frmExp.GetExportFileName(objSettings.InboundGoodsReceiving, frmExport.TFileExportType.GoodsIssuance)
                    msgBody = frmExp.ExportDataFile(lblMessage, PB, FileName, frmExport.TFileExportType.GoodsIssuance, True, True)
                    objSettings.SendEmail(msgSub, msgBody, True)
                End If
                If SchName = "-SchInReversalGR" Then
                    Dim FileName As String = frmExp.GetExportFileName(objSettings.InboundReversalDoc, frmExport.TFileExportType.ReversalGRDoc)
                    msgBody = frmExp.ExportDataFile(lblMessage, PB, FileName, frmExport.TFileExportType.ReversalGRDoc, True, True)
                    objSettings.SendEmail(msgSub, msgBody, True)
                End If
                If SchName = "-SchInReversalGI" Then
                    Dim FileName As String = frmExp.GetExportFileName(objSettings.InboundReversalDoc, frmExport.TFileExportType.ReversalGIDoc)
                    msgBody = frmExp.ExportDataFile(lblMessage, PB, FileName, frmExport.TFileExportType.ReversalGIDoc, True, True)
                    objSettings.SendEmail(msgSub, msgBody, True)
                End If
                If SchName = "-SchInVendorReturn" Then
                    Dim FileName As String = frmExp.GetExportFileName(objSettings.InboundVerndorReturn, frmExport.TFileExportType.VendorReturn)
                    msgBody = frmExp.ExportDataFile(lblMessage, PB, FileName, frmExport.TFileExportType.VendorReturn, True, True)
                    objSettings.SendEmail(msgSub, msgBody, True)
                End If
            Else

            End If
        Catch ex As Exception
            objSettings.SaveErrorToLogFile(ex.Message, True)
        Finally
            frmExp.Dispose()
            frmImp.Dispose()
            Try

            Catch ex As Exception

            End Try
            Me.Close()
        End Try
    End Sub


End Class
