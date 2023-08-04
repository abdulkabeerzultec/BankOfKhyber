Imports System.IO
Imports ZultecScreenCapture
Imports System.Net.Mail
Imports System.Text
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class frmError
    Public Ex As Exception
    Private strEmail As String
    Private strName As String
    Dim strError As New StringBuilder

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        Me.Close()
    End Sub

    Private Sub btnSent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSent.Click
        SenDEmail()
    End Sub

    Private Sub frmError_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        If MainForm IsNot Nothing Then
            MainForm.hideLoading()
        End If

        Try
            lblDesc.Text = Ex.Message
            lblErrCode.Text = DateTime.Now
            lblErrorType.Text = Ex.GetType().Name

            strError.Append("Following error occurred....." & Environment.NewLine & Environment.NewLine & _
                            "Error            : " & Ex.Message & Environment.NewLine)
            strError.Append("Date            : " & Format(Now, "MMM dd, yyyy HH:mm") & Environment.NewLine)
            strError.Append("Error Type   : " & Ex.GetType().Name & Environment.NewLine & Environment.NewLine)

            ' Str.Append("Source: " & ErrSource & Environment.NewLine & Environment.NewLine)
            If Ex.StackTrace IsNot Nothing Then
                strError.Append("Stack           : " & Environment.NewLine & Ex.StackTrace.Replace("  at ", Environment.NewLine) & Environment.NewLine)
            End If
            'strError.Append("-----------------------------------------------------------")
            strError.Append(Environment.NewLine)
            txtError.Text = strError.ToString()
            txtError.DeselectAll()
            txtError.Refresh()

            Dim ds As DataTable
            Dim dt As DataTable
            Dim objBALCompanyInfo As New BALCompanyInfo
            ds = objBALCompanyInfo.GetAll_CompanyInfo(New attCompanyInfo)
            'If ds.Tables.Count > 0 Then
            If ds.Rows.Count > 0 Then
                dt = ds
                If dt.Rows(0)("Name").ToString() <> "" Then strName = dt.Rows(0)("Name").ToString()
                If dt.Rows(0)("Email").ToString() <> "" Then strEmail = dt.Rows(0)("Email").ToString()
            End If
            '  End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub SenDEmail()
        Try
            TabControl1.Enabled = False
            btnContinue.Enabled = False
            btnSent.Enabled = False
            pnl1.Visible = True
            pnl1.Refresh()
            Dim sc As ZultecScreenCapture.ScreenCapture = New ZultecScreenCapture.ScreenCapture()
            ' capture entire screen, and save it to a file
            Dim img As Image = sc.CaptureScreen()
            ' display image in a Picture control named imageDisplay
            'this.imageDisplay.Image = img;
            ' capture this window, and save it
            If Not Directory.Exists(AppConfig.AppDataFolder & "\ErrorsReports\") Then
                Directory.CreateDirectory(AppConfig.AppDataFolder & "\ErrorsReports\")
            End If
            Dim strFName As String = AppConfig.AppDataFolder & "\ErrorsReports\" & DateString & Now.Hour & Now.Minute & Now.Second & ".gif"
            If File.Exists(strFName) Then
                File.Delete(strFName)
            End If
            sc.CaptureWindowToFile(Me.Handle, strFName, System.Drawing.Imaging.ImageFormat.Gif)

            Dim message As MailMessage = New MailMessage()
            Dim mailatt As New Attachment(strFName)

            message.From = New MailAddress(strEmail)

            message.To.Add(New MailAddress("it-support@zultec.com"))
            message.CC.Add(New MailAddress("Wael.Dalloul@zultec.com"))

            message.Subject = "Error Report for" & My.Application.Info.ProductName & My.Application.Info.Version.ToString & "(" & strName & ")"

            message.Body = strError.ToString()
            message.Attachments.Add(mailatt)

            Dim client As SmtpClient = New SmtpClient()
            client.Host = "smtp.zultec.com"
            ' Add credentials if the SMTP server requires them.
            client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials
            'client.UseDefaultCredentials = True
            client.Send(message)
            TabControl1.Enabled = True
            btnContinue.Enabled = True
            btnSent.Enabled = True
            pnl1.Visible = False
            pnl1.Refresh()
            ZulMessageBox.ShowMe("SendErConfrm")
            Me.Close()
        Catch ex As Exception
            ZulMessageBox.ShowMe("SendErrError")
            TabControl1.Enabled = True
            btnContinue.Enabled = True
            btnSent.Enabled = True
            pnl1.Visible = False
            pnl1.Refresh()
        End Try
    End Sub
End Class