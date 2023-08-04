Imports System.Windows.Forms
Imports ZulAssetsDAL.ZulAssetsDAL
Imports ZulAssetsBL.ZulAssetBAL

Public Class frmSettings
    Private _AdminPassword As String
    Private IsFormLoad As Boolean = False
    Dim MyConnection As System.Data.OleDb.OleDbConnection
    Dim strConnection As String = String.Empty

    Public ReadOnly Property Query() As String
        Get
            Return txtDBQuery.Text
        End Get
    End Property

    Public ReadOnly Property ConnectionString() As String
        Get
            Return GetOracleConnectionString(txtDatasource.Text, txtDBUserName.Text, txtDBPassword.Text)
        End Get
    End Property
    Public Property AdminPassword() As String
        Get
            Return _AdminPassword
        End Get
        Set(ByVal value As String)
            _AdminPassword = value
        End Set
    End Property

    Public Sub SaveErrorToLogFile(ByVal Message As String, ByVal IsErrorMessage As Boolean)
        SaveToLogFile(Message, IsErrorMessage)
    End Sub

    'Public ReadOnly Property InboundAssetChange() As String
    '    Get
    '        Return txtExportFilePath.Text
    '    End Get
    'End Property

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If SaveSettings() Then
            MessageBox.Show("Settings saved successfully.", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Error while saving.", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Public Function SaveSettings() As Boolean
        Dim regKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("ZulAssetsCITCPlugin", True)
        Try
            If regKey IsNot Nothing Then
                regKey.SetValue("DBDataSource", txtDatasource.Text)
                regKey.SetValue("DBUserName", txtDBUserName.Text)
                regKey.SetValue("DBPassword", txtDBPassword.Text)
                regKey.SetValue("DBQuery", txtDBQuery.Text)

                regKey.SetValue("ExportFilePath", txtExportFilePath.Text)

                regKey.SetValue("ActivteEmail", ChkActivteEmail.Checked)
                regKey.SetValue("SMTP", txtSMTP.Text)
                regKey.SetValue("UserName", txtUserName.Text)
                regKey.SetValue("Password", txtPassword.Text)
                regKey.SetValue("FromAddress", txtFromAddress.Text)
                regKey.SetValue("ToAddress", txtAssetToAddress.Text)
            End If
            Return True
        Catch ex As Exception
            Return False
        Finally
            If regKey IsNot Nothing Then
                regKey.Close()
            End If

        End Try
    End Function

    Public Sub LoadDatabaseConnection()
        Dim objBalappConfig As ZulAssetsBL.ZulAssetBAL.BALAppConfig
        objBalappConfig = New ZulAssetsBL.ZulAssetBAL.BALAppConfig
        objBalappConfig.LoadSettings()
    End Sub

    Public Sub LoadSettings()
        Dim regKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("ZulAssetsCITCPlugin", True)
        Try
            If regKey Is Nothing Then
                My.Computer.Registry.CurrentUser.CreateSubKey("ZulAssetsCITCPlugin")
            Else
                txtDatasource.Text = regKey.GetValue("DBDataSource")
                txtDBUserName.Text = regKey.GetValue("DBUserName")
                txtDBPassword.Text = regKey.GetValue("DBPassword")
                txtDBQuery.Text = regKey.GetValue("DBQuery")

                txtExportFilePath.Text = regKey.GetValue("ExportFilePath")

                ChkActivteEmail.Checked = regKey.GetValue("ActivteEmail")
                txtSMTP.Text = regKey.GetValue("SMTP")
                txtUserName.Text = regKey.GetValue("UserName")
                txtPassword.Text = regKey.GetValue("Password")
                txtFromAddress.Text = regKey.GetValue("FromAddress")
                txtAssetToAddress.Text = regKey.GetValue("ToAddress")


            End If
        Finally
            If regKey IsNot Nothing Then
                regKey.Close()
            End If
        End Try
    End Sub

    Private Sub frmSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG
        Me.BackgroundImageLayout = ImageLayout.Stretch
        LoadSettings()
        LoadScheduleInfo()
        tabMain.SelectedTabPage = layoutOutboundFiles
    End Sub

    'Private Sub txtImport_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    With OpenFileDialog1

    '        .CheckFileExists = True
    '        .CheckPathExists = True
    '        .FileName = ""
    '        .ShowReadOnly = True
    '        .DefaultExt = "txt"

    '        .DefaultExt = "xlsx"
    '        .Filter = "Excel files (*.xls;*.xlsx)|*.xls;*.xlsx"
    '        .Multiselect = False
    '        .Title = "Excel Files"
    '        If .ShowDialog() = DialogResult.OK Then
    '            txtImportFilePath.Text = .FileName
    '            If IO.Path.GetExtension(txtImportFilePath.Text) = ".xls" Then
    '                GetConnected("2003")
    '            ElseIf IO.Path.GetExtension(txtImportFilePath.Text) = ".xlsx" Then
    '                GetConnected("2007")
    '            End If
    '        End If
    '    End With
    'End Sub

    'Private Sub GetConnected(ByVal ExcelVerion As String)

    '    Dim strSourceFile As String = txtImportFilePath.Text

    '    If ExcelVerion = "2003" Then
    '        strConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strSourceFile + ";Extended Properties=""Excel 8.0;IMEX=1;HDR=YES;"""
    '    Else 'if ExcelVerion = "2007"
    '        strConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strSourceFile & ";Extended Properties=""Excel 12.0;HDR=Yes"";"
    '    End If

    '    MyConnection = New System.Data.OleDb.OleDbConnection(strConnection)
    '    Try
    '        MyConnection.Open()
    '        Dim SchemaDT As DataTable = MyConnection.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, _
    '                          New Object() {Nothing, Nothing, Nothing, "TABLE"})
    '        lstTables.DataSource = SchemaDT
    '        lstTables.ValueMember = "TABLE_NAME"
    '        lstTables.DisplayMember = "TABLE_NAME"
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    Finally
    '        MyConnection.Close()
    '    End Try
    'End Sub

    Private Sub txtExport_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtExportFilePath.DoubleClick
        With OpenFileDialog1

            .CheckFileExists = True
            .CheckPathExists = True
            .FileName = ""
            .ShowReadOnly = True
            .DefaultExt = "txt"

            .DefaultExt = "xlsx"
            .Filter = "Excel files (*.xls;*.xlsx)|*.xls;*.xlsx"
            .Multiselect = False
            .Title = "Excel Files"
            If .ShowDialog() = DialogResult.OK Then
                CType(sender, DevExpress.XtraEditors.MemoEdit).Text = .FileName
            End If
        End With
    End Sub

    Private Sub txtGoodReceive_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            CType(sender, DevExpress.XtraEditors.MemoEdit).Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Function GetOracleConnectionString(ByVal DataSource As String, ByVal UserName As String, ByVal Pasword As String) As String
        ' Good Referance to solve connection issues:
        'http://blogs.msdn.com/b/dataaccesstechnologies/archive/2010/06/30/ora-12154-tns-could-not-resolve-the-connect-identifier-specified-error-while-creating-a-linked-server-to-oracle.aspx


        'Finallay we give the complete information in Data Source like the following:
        '(DESCRIPTION=(CID=prod)(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST= 192.168.100.24)(PORT=1523)))(CONNECT_DATA=(SID=prod)(SERVER=DEDICATED)))

        'Return "PROVIDER=ORAOLEDB.ORACLE;DATA SOURCE=" & DataSource & ";User Id=" & UserName & ";Password=" & Pasword
        Return "PROVIDER=MSDAORA;DATA SOURCE=" & DataSource & ";User Id=" & UserName & ";Password=" & Pasword
    End Function


    Private Function CheckConnection() As String
        Dim ConStr As String = GetOracleConnectionString(txtDatasource.Text, txtDBUserName.Text, txtDBPassword.Text)
        Dim con As New OleDb.OleDbConnection(ConStr)
        Try
            con.Open()
            Return Nothing
        Catch ex As Exception
            Return ex.Message
        Finally
            con.Dispose()
        End Try
    End Function

    Private Sub btnOpenDialog1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDialogImport.Click
        Dim msg As String = CheckConnection()
        If String.IsNullOrEmpty(msg) Then
            Messages.InfoMessage("Connection open successfully.")
        Else
            Messages.ErrorMessage(msg)
        End If
    End Sub
    Private Sub btnImportAssetCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles l.Click
        Dim msg As String = CheckConnection()
        If String.IsNullOrEmpty(msg) Then
            Dim frm As New frmImport
            SaveSettings()
            Dim ConStr As String = GetOracleConnectionString(txtDatasource.Text, txtDBUserName.Text, txtDBPassword.Text)
            frm.ConnectionString = ConStr
            frm.Query = txtDBQuery.Text
            frm.ShowDialog()
            frm.Dispose()
        Else
            Messages.ErrorMessage(msg)
        End If

    End Sub


    Private Sub btnOpenDialog5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDialogExport.Click
        txtExport_DoubleClick(txtExportFilePath, e)
    End Sub

    Private Sub btnExportAssetChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ShowExportForm(txtExportFilePath)
    End Sub

    Private Function CheckFileName(ByVal txt As DevExpress.XtraEditors.MemoEdit) As Boolean
        If String.IsNullOrEmpty(txt.Text) Then
            txt.ErrorIcon = My.Resources.Invalid
            txt.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            txt.ErrorText = "File name must not be empty."
            Return False
        ElseIf Not IO.File.Exists(txt.Text) Then
            txt.ErrorIcon = My.Resources.Invalid
            txt.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            txt.ErrorText = "File not exists."
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CheckFolderName(ByVal txt As DevExpress.XtraEditors.MemoEdit) As Boolean
        If String.IsNullOrEmpty(txt.Text) Then
            txt.ErrorIcon = My.Resources.Invalid
            txt.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            txt.ErrorText = "Folder name must not be empty."
            Return False
        ElseIf Not IO.Directory.Exists(txt.Text) Then
            txt.ErrorIcon = My.Resources.Invalid
            txt.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            txt.ErrorText = "Folder not exists."
            Return False
        Else
            Return True
        End If
    End Function

    'Public Sub ShowImportForm(ByVal txt As DevExpress.XtraEditors.MemoEdit)
    '    If CheckFileName(txt) Then
    '        If Not String.IsNullOrEmpty(lstTables.SelectedValue) Then
    '            Dim frm As New frmImport
    '            SaveSettings()
    '            frm.ImportFileName = txt.Text
    '            frm.SheetName = lstTables.SelectedValue
    '            frm.ShowDialog()
    '            frm.Dispose()
    '        Else
    '            Messages.ErrorMessage("Excel Sheet not selected, Please select one from the list and click Import again.")
    '        End If
    '    End If
    'End Sub

    Public Sub ShowExportForm(ByVal txt As DevExpress.XtraEditors.MemoEdit)
        If CheckFileName(txt) Then
            SaveSettings()
            Dim frm As New frmExport
            frm.ExportFileName = txt.Text
            frm.ShowDialog()
            frm.Dispose()
        End If
    End Sub

    Private Sub btnSendTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendTest.Click
        SaveSettings()
        Dim msgSub As String = "ZulAssets Email Setting Test"
        Dim msgBody As String = "This is a test email message from zulassets application to verify that email is working correctly. Email DateTime: " & Now.Date.ToString
        Dim msg1 As String = String.Empty
        Dim msg As String = String.Empty

        msg = SendMail(txtFromAddress.Text, txtAssetToAddress.Text, txtSMTP.Text, txtUserName.Text, txtPassword.Text, msgSub, msgBody)

        If msg = "Email sent successfully" Or msg1 = "Email sent successfully" Then
            MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Public Function SendEmail(ByVal msgSub As String, ByVal msgBody As String, ByVal IsInventoryRoleMail As Boolean) As String
        If ChkActivteEmail.Checked Then
            Dim msg As String = SendMail(txtFromAddress.Text, txtAssetToAddress.Text, txtSMTP.Text, txtUserName.Text, txtPassword.Text, msgSub, msgBody)
            Return msg
        Else
            Return String.Empty
        End If
    End Function

    Private Sub SetCheckStatus(ByVal sender As DevExpress.XtraEditors.CheckEdit, ByVal txtTime As DevExpress.XtraEditors.TimeEdit)
        Dim sched As New TaskScheduler.Scheduler
        Dim SchName As String = CType(sender, DevExpress.XtraEditors.CheckEdit).Name.Remove(0, 3)
        Dim task As TaskScheduler.Task = sched.Tasks(SchName)

        If task IsNot Nothing Then
            If task.Triggers.Count > 0 Then
                Dim tr As TaskScheduler.WeeklyTrigger = task.Triggers(0)

                If tr IsNot Nothing Then
                    If Not String.IsNullOrEmpty(String.Format("{0}{1}", tr.StartHour, tr.StartMinute)) Then
                        txtTime.Time = String.Format("{0}:{1}", tr.StartHour, tr.StartMinute)
                        Dim values() As TaskScheduler.DaysOfTheWeek = [Enum].GetValues(GetType(TaskScheduler.DaysOfTheWeek))
                        For i As Integer = 0 To ChkWeekDays.Items.Count - 1
                            ' we are checking with "and" operator to check if day selected or not in the weekdays of the weeklyTrigger of the Schedule.
                            'If tr.WeekDays And TaskScheduler.DaysOfTheWeek.Sunday Then
                            If values(i) And tr.WeekDays Then
                                ChkWeekDays.Items(i).CheckState = CheckState.Checked
                            End If
                        Next

                    End If
                End If
            End If

            If task.Status = TaskScheduler.TaskStatus.Ready Then
                CType(sender, DevExpress.XtraEditors.CheckEdit).ForeColor = Drawing.Color.Green
                CType(sender, DevExpress.XtraEditors.CheckEdit).Text = String.Format("{0}({1}:{2}) ", "Running at", txtTime.Time.Hour, txtTime.Time.Minute)
                CType(sender, DevExpress.XtraEditors.CheckEdit).Checked = True
            ElseIf task.Status = TaskScheduler.TaskStatus.NotScheduled Then
                CType(sender, DevExpress.XtraEditors.CheckEdit).ForeColor = Drawing.Color.Red
                CType(sender, DevExpress.XtraEditors.CheckEdit).Text = "Not Running"
                CType(sender, DevExpress.XtraEditors.CheckEdit).Checked = False
            ElseIf task.Status = TaskScheduler.TaskStatus.Running Then
                CType(sender, DevExpress.XtraEditors.CheckEdit).ForeColor = Drawing.Color.Green
                CType(sender, DevExpress.XtraEditors.CheckEdit).Text = String.Format("{0}({1}:{2}) ", "Running at", txtTime.Time.Hour, txtTime.Time.Minute)
                CType(sender, DevExpress.XtraEditors.CheckEdit).Checked = True
            Else
                CType(sender, DevExpress.XtraEditors.CheckEdit).ForeColor = Drawing.Color.Green
                CType(sender, DevExpress.XtraEditors.CheckEdit).Text = String.Format("{0}({1}:{2}) ", "Running at", txtTime.Time.Hour, txtTime.Time.Minute)
                CType(sender, DevExpress.XtraEditors.CheckEdit).Checked = True
            End If
        Else
            CType(sender, DevExpress.XtraEditors.CheckEdit).ForeColor = Drawing.Color.Red
            CType(sender, DevExpress.XtraEditors.CheckEdit).Text = "Not Running"
            CType(sender, DevExpress.XtraEditors.CheckEdit).Checked = False
        End If
    End Sub

    Private Sub LoadScheduleInfo()
        IsFormLoad = True
        SetCheckStatus(chkSchNMCImportData, txtTimeMasterCreate)
        SetCheckStatus(chkSchInAssetChange, txtTimeInAssetChange)
        IsFormLoad = False
    End Sub

    Private Sub Schedule_CheckedChanged(ByVal sender As System.Object, ByVal txtTime As DevExpress.XtraEditors.TimeEdit)
        If Not IsFormLoad Then
            Dim SchName As String = CType(sender, DevExpress.XtraEditors.CheckEdit).Name.Remove(0, 3)
            If CType(sender, DevExpress.XtraEditors.CheckEdit).Checked Then
                Dim SelectedDays As TaskScheduler.DaysOfTheWeek
                Dim values() As TaskScheduler.DaysOfTheWeek = [Enum].GetValues(GetType(TaskScheduler.DaysOfTheWeek))

                For i As Integer = 0 To ChkWeekDays.Items.Count - 1
                    If ChkWeekDays.Items(i).CheckState = CheckState.Checked Then
                        SelectedDays = SelectedDays Or values(i)
                    End If
                Next
                If SelectedDays > 0 Then
                    If CreateTaskScheduler(SchName, AdminPassword, txtTime.Time.Hour, txtTime.Time.Minute, SelectedDays) Then
                        CType(sender, DevExpress.XtraEditors.CheckEdit).ForeColor = Drawing.Color.Green
                        CType(sender, DevExpress.XtraEditors.CheckEdit).Text = "Running " & txtTime.Time.ToString
                        CType(sender, DevExpress.XtraEditors.CheckEdit).Text = String.Format("{0}({1}:{2}) ", "Running at", txtTime.Time.Hour, txtTime.Time.Minute)

                    Else
                        CType(sender, DevExpress.XtraEditors.CheckEdit).Checked = False
                    End If
                Else
                    CType(sender, DevExpress.XtraEditors.CheckEdit).Checked = False
                    Messages.ErrorMessage("Please select one day at least.")
                End If

            Else
                If DeleteTaskScheduler(SchName, AdminPassword) Then
                    CType(sender, DevExpress.XtraEditors.CheckEdit).ForeColor = Drawing.Color.Red
                    CType(sender, DevExpress.XtraEditors.CheckEdit).Text = "Not Running"
                End If
            End If
        End If
    End Sub

    Private Sub chkSchNMCImportData_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchNMCImportData.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeMasterCreate)
    End Sub

    Private Sub chkSchInAssetChange_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchInAssetChange.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeInAssetChange)
    End Sub

End Class