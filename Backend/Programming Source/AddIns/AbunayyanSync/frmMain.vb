Imports AbunayyanPlugin
Public Class frmMain

    Dim objSettings As New frmImportSettings
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
                If SchName = "-SchLocation" Then
                    msgBody = frmImp.SilentImport(lblMessage, PB, objSettings.OutLocation, frmImport.TFileImportType.Location)
                    objSettings.SendEmail(msgSub, msgBody, False)
                End If
                If SchName = "-SchCostCenter" Then
                    msgBody = frmImp.SilentImport(lblMessage, PB, objSettings.OutCostCenter, frmImport.TFileImportType.CostCenter)
                    objSettings.SendEmail(msgSub, msgBody, False)
                End If
                If SchName = "-SchEmployee" Then
                    msgBody = frmImp.SilentImport(lblMessage, PB, objSettings.OutEmployee, frmImport.TFileImportType.Employees)
                    objSettings.SendEmail(msgSub, msgBody, False)
                End If
                If SchName = "-SchOutAssets" Then
                    msgBody = frmImp.SilentImport(lblMessage, PB, objSettings.OutAsset, frmImport.TFileImportType.Asset)
                    objSettings.SendEmail(msgSub, msgBody, False)
                End If

                If SchName = "-SchExportData" Then
                    msgBody = frmExp.SilentExport(objSettings.InExport, PB)
                    objSettings.SendEmail(msgSub, msgBody, False)
                End If
            Else
                msgBody = frmImp.SilentImport(lblMessage, PB, objSettings.OutLocation, frmImport.TFileImportType.Location)
                objSettings.SendEmail(msgSub, msgBody, False)
                msgBody = frmImp.SilentImport(lblMessage, PB, objSettings.OutCostCenter, frmImport.TFileImportType.CostCenter)
                objSettings.SendEmail(msgSub, msgBody, False)
                msgBody = frmImp.SilentImport(lblMessage, PB, objSettings.OutEmployee, frmImport.TFileImportType.Employees)
                objSettings.SendEmail(msgSub, msgBody, False)
                msgBody = frmImp.SilentImport(lblMessage, PB, objSettings.OutAsset, frmImport.TFileImportType.Asset)
                objSettings.SendEmail(msgSub, msgBody, False)

                msgBody = frmExp.SilentExport(objSettings.InExport, PB)
                objSettings.SendEmail(msgSub, msgBody, False)
            End If
        Catch ex As Exception
            objSettings.SaveErrorToLogFile(ex.Message, True)
        Finally
            frmImp.Dispose()
            frmExp.Dispose()
            Me.Close()
        End Try
    End Sub
End Class
