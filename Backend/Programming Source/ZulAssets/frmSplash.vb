Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Public Class frmSplash
    Public IsSplash As Boolean = False
    Private Sub frmSplash_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Icon = My.Resources.MainIcon
        Dim CompanyName As String = AppConfig.CompanyName
        If CompanyName = "" Or CompanyName Is Nothing Then
            Me.Close()
        End If
        lblVersion.Text = My.Application.Info.ProductName & " " & My.Application.Info.Version.ToString
        lblLicensedTo.Text = "Licensed To: " & CompanyName

        lblEdition.Text = GetEdtionName(AppConfig.AppEdition) & " Edition"
        If AppConfig.IsDemoKey Then
            lblEdition.Text &= " <Evaluation Version>"
        End If

        If String.IsNullOrEmpty(AppConfig.DBVersion) Then
            lblDbVersion.Visible = False
        Else
            lblDbVersion.Visible = True
            lblDbVersion.Text = "Database version: " & AppConfig.DBVersion
        End If

        If IntegrationName <> IntegrationType.None Then
            lblIntegration.Text = String.Format("Integration Name: {0}", IntegrationName.ToString)
        Else
            lblIntegration.Visible = False
        End If
        If IsSplash Then
            Timer1.Enabled = True
        End If

    End Sub
    Private Function GetEdtionName(ByVal edition As ApplicationEditions) As String
        If AppConfig.AppEdition = ApplicationEditions.Enterprise Then
            Return "Enterprise"
        ElseIf AppConfig.AppEdition = ApplicationEditions.Financial Then
            Return "Financial"
        ElseIf AppConfig.AppEdition = ApplicationEditions.Tracking Then
            Return "Tracking"
        ElseIf AppConfig.AppEdition = ApplicationEditions.Inventory Then
            Return "Inventory"
        Else
            Return "Not Registered"
        End If

    End Function

    Private Sub frmSplash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Click
        If Not IsSplash Then
            Me.Close()
        End If
    End Sub

    Private Sub frmSplash_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub

  

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.Close()
    End Sub
End Class