Imports NMCIntegration

Public Class frmNMCSync

    Dim objSettings As New frmNMCSettings
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
        'objImportExport.RoleID = 1

        Dim frmImp As New frmImport
        'Dim frmExp As New frmExport
        Try
            If My.Application.CommandLineArgs.Count > 0 Then
                Dim SchName As String = My.Application.CommandLineArgs(0)
                If SchName = "-SchNMCImportData" Then
                    frmImp.Query = objSettings.Query
                    frmImp.ConnectionString = objSettings.ConnectionString
                    msgBody = frmImp.SilentImport(lblMessage, PB)
                    objSettings.SendEmail(msgSub, msgBody, False)
                End If
            End If
        Catch ex As Exception
            objSettings.SaveErrorToLogFile(ex.Message, True)
        Finally
            'frmExp.Dispose()
            frmImp.Dispose()
            Try

            Catch ex As Exception

            End Try
            Me.Close()
        End Try
    End Sub
End Class
