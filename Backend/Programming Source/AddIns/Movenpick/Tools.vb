Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraEditors.DXErrorProvider
Imports System.Reflection
Imports ZulAssetsDAL.ZulAssetsDAL
Imports ZulAssetsBL.ZulAssetBAL
Imports TaskScheduler

Module Tools

    Public DemoAssetsCount As Integer = 10

    Public NumberMask As String = "###,###,###,##0.00"
    Public NumberMaskType As DevExpress.XtraEditors.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric

    Public Function CheckAndConvertDate(ByVal str) As Date
        Try
            Return Date.Parse(str)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CheckConvertDate(ByVal Str) As Boolean
        Try
            Date.ParseExact(Str, "dd.MM.yyyy", System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ConvertDate(ByVal Str) As DateTime
        Try
            Return Date.ParseExact(Str, "dd.MM.yyyy", System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Function RemoveUnnecessaryChars(ByVal value As String) As String
        Return value.Trim.Replace("'", "''")
    End Function

    Public Sub SaveToLogFile(ByVal Message As String, ByVal IsErrorMessage As Boolean)
        If Not Directory.Exists(AppConfig.AppDataFolder & "\ErrorsReports\") Then
            Directory.CreateDirectory(AppConfig.AppDataFolder & "\ErrorsReports\")
        End If

        Dim ErrFile As String = AppConfig.AppDataFolder & "\ErrorsReports\" & "ZulAssetsImport-ExportErr.Log"
        Dim sr As StreamWriter = File.AppendText(ErrFile)
        Dim ErrTxt As String
        If IsErrorMessage Then
            ErrTxt = "Date  : " & Format(Now, "MMM dd, yyyy HH:mm") & Environment.NewLine & _
                     "Error : " & Message & Environment.NewLine & _
                     "-----------------------------------------------------------" & _
                     "-----------------------------------------------------------" & _
                     Environment.NewLine & Environment.NewLine
        Else
            ErrTxt = "Date  : " & Format(Now, "MMM dd, yyyy HH:mm") & Environment.NewLine & _
         "Information : " & Message & Environment.NewLine & _
         "-----------------------------------------------------------" & _
         "-----------------------------------------------------------" & _
         Environment.NewLine & Environment.NewLine
        End If

        sr.WriteLine(ErrTxt)
        sr.Close()
        sr.Dispose()
    End Sub


    Public Sub GenericExceptionHandler(ByVal ex As Exception, _
                                       ByVal ErrSource As String)
        If Not Directory.Exists(AppConfig.AppDataFolder & "\ErrorsReports\") Then
            Directory.CreateDirectory(AppConfig.AppDataFolder & "\ErrorsReports\")
        End If

        Dim ErrFile As String = AppConfig.AppDataFolder & "\ErrorsReports\" & "ZulAssetsErr.Log"
        Dim sr As StreamWriter = File.AppendText(ErrFile)
        Dim ErrTxt As String

        ErrTxt = "Date  : " & Format(Now, "MMM dd, yyyy HH:mm") & Environment.NewLine & _
                 "Error : " & ex.Message & Environment.NewLine & _
                 "Source: " & ErrSource & Environment.NewLine & Environment.NewLine & _
                 "Stack : " & ex.StackTrace & Environment.NewLine & _
                 "-----------------------------------------------------------" & _
                 "-----------------------------------------------------------" & _
                 Environment.NewLine & Environment.NewLine

        sr.WriteLine(ErrTxt)
        sr.Close()
        sr.Dispose()
        ShowErrorMessage(ex.Message)
    End Sub

    Public Sub ShowErrorMessage(ByVal msg As String)
        MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Public Sub ShowInfoMessage(ByVal msg As String)
        MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Function DeleteTaskScheduler(ByVal ScheduleName As String, ByVal SystemPass As String) As Boolean
        Dim domainUsername As String = System.Windows.Forms.SystemInformation.UserDomainName & "\" & _
                  System.Windows.Forms.SystemInformation.UserName
        Dim usrPass As String = SystemPass
        Dim sched As New Scheduler
        Try
            sched.Tasks.Delete(ScheduleName)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CreateTaskScheduler(ByVal ScheduleName As String, ByVal SystemPass As String, ByVal RunHour As Integer, ByVal RunMinute As Integer) As Boolean

        Dim domainUsername As String = System.Windows.Forms.SystemInformation.UserDomainName & "\" & _
                   System.Windows.Forms.SystemInformation.UserName
        Dim usrPass As String = SystemPass
        Dim sched As New Scheduler

        Dim t2 As Task
        Try
            Try
                sched.Tasks.Delete(ScheduleName)
            Catch ex As Exception
            End Try

            t2 = sched.Tasks.NewTask(ScheduleName)

            t2.ApplicationName = Application.StartupPath & "\ElafSync.exe"
            t2.Parameters = "-" & ScheduleName
            t2.Comment = "Elaf Synchronization Agent " & ScheduleName
            t2.Creator = "Zultec - ZulAssets"
            t2.Flags = TaskFlags.Interactive
            t2.Hidden = True
            t2.IdleWaitDeadlineMinutes = 20
            t2.IdleWaitMinutes = 10
            t2.MaxRunTime = New TimeSpan(5, 0, 0)


            t2.Priority = System.Diagnostics.ProcessPriorityClass.High
            t2.WorkingDirectory = Application.StartupPath

            t2.Triggers.Add(New DailyTrigger(RunHour, RunMinute, 1))

            If Not String.IsNullOrEmpty(usrPass) Then
                t2.SetAccountInformation(domainUsername, usrPass)
            Else
                t2.Flags = TaskFlags.RunOnlyIfLoggedOn
                t2.SetAccountInformation(domainUsername, Nothing)
            End If
            t2.Save()
        Catch ex As Exception
            Try
                sched.Tasks.Delete(ScheduleName)
            Catch ex1 As Exception
            End Try
            If ex.Message.StartsWith("Access is denied") Then
                ShowErrorMessage("Incorrect System password, Reenter Password")
            End If
            Return False
        Finally
            t2 = Nothing
        End Try
        t2 = Nothing
        Return True
    End Function

    Public Function CheckFile(ByVal FileName As String) As Boolean
        If String.IsNullOrEmpty(FileName) Or Not System.IO.File.Exists(FileName) Then
            Return False
        End If
        Return True
    End Function



    Public Function CheckFolder(ByVal FolderName As String) As Boolean
        If String.IsNullOrEmpty(FolderName) Or Not System.IO.Directory.Exists(FolderName) Then
            Return False
        End If
        Return True
    End Function

    Public Function SendMail(ByVal FromAddress As String, ByVal ToAddress As String, ByVal SMTP As String, ByVal UserName As String, ByVal Password As String, ByVal MsgSub As String, ByVal MsgBody As String) As String
        Dim Mail As New ASPEMAILLib.MailSender
        If String.IsNullOrEmpty(SMTP) Then
            Return "SMTP Cannot be empty"
        End If
        If String.IsNullOrEmpty(FromAddress) Then
            Return "From Address Cannot be empty"
        End If
        If String.IsNullOrEmpty(ToAddress) Then
            Return "To Address Cannot be empty"
        End If

        Mail.Host = SMTP
        Mail.From = FromAddress
        Dim Toarr As String() = ToAddress.Split(";"c)
        If Toarr.Length > 0 Then
            Mail.AddAddress(Toarr(0))
            For i As Integer = 1 To Toarr.Length - 1
                Mail.AddCC(Toarr(i))
            Next
        End If
        Mail.Subject = MsgSub
        Mail.Body = MsgBody
        Mail.Username = UserName
        Mail.Password = Password

        Try
            Mail.Send()
            Return "Email sent successfully"
        Catch ex As Exception
            Return "Email send failed. Following error occurred." & Keys.Enter & ex.Message
        End Try
    End Function

    Public Sub addGridMenu(ByVal grd As DevExpress.XtraGrid.GridControl)
        Dim CntMnu As New ContextMenuStrip
        CntMnu.Items.Add("Export to Excel", My.Resources.ExportToExcel16x16)
        AddHandler CntMnu.Items(0).Click, AddressOf GridExportToExcel
        CntMnu.Items.Add("Print Preview", My.Resources.Preview16x16)
        AddHandler CntMnu.Items(1).Click, AddressOf GridPrint
        grd.ContextMenuStrip = CntMnu
        CntMnu.Tag = grd
    End Sub

    Public Sub GridExportToExcel(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim grd As DevExpress.XtraGrid.GridControl = CType(sender, ToolStripMenuItem).Owner.Tag
        Dim savedlg As New SaveFileDialog
        savedlg.DefaultExt = "xls"
        savedlg.Filter = "*.xls|*.xls"

        If savedlg.ShowDialog() = DialogResult.OK Then
            grd.ExportToXls(savedlg.FileName)
            System.Diagnostics.Process.Start(savedlg.FileName)
        End If
    End Sub

    Public Sub GridPrint(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim grd As DevExpress.XtraGrid.GridControl = CType(sender, ToolStripMenuItem).Owner.Tag
        grd.ShowPrintPreview()
    End Sub

End Module
