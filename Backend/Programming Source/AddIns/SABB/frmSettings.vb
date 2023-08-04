Imports System.Windows.Forms
Imports ZulAssetsDAL.ZulAssetsDAL
Imports ZulAssetsBL.ZulAssetBAL

Public Class frmImportSettings

    Private _RoleID As Integer
    Private _AdminPassword As String
    Private AllowAssetAudit As Boolean = False
    Private AllowInventory As Boolean = False
    Private IsFormLoad As Boolean = False

    Dim AllowImport As Boolean = True
    Dim AllowExport As Boolean = True

    Public Property RoleID() As Integer
        Get
            Return _RoleID
        End Get
        Set(ByVal value As Integer)
            _RoleID = value
            Dim ds As DataTable
            Dim objattRole As attRoles = New attRoles
            Dim objBALRole As New BALRoles
            objattRole.PKeyCode = value
            ds = objBALRole.GetAll_Roles(objattRole)
            AllowAssetAudit = ds.Rows(0)("AssetsBooks")
            AllowInventory = ds.Rows(0)("AssetItems")
            AllowExport = ds.Rows(0)("CMAExport").ToString
            AllowImport = ds.Rows(0)("CMAImport").ToString
            If AllowImport Then
                layoutOutboundFiles.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                tabMain.SelectedTabPage = layoutOutboundFiles
            Else
                layoutOutboundFiles.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            If AllowExport Then
                LayoutExportData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                tabMain.SelectedTabPage = LayoutExportData
            Else
                LayoutExportData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            If AllowExport And AllowImport Then
                tabMain.SelectedTabPage = layoutOutboundFiles
            End If
        End Set
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


    Public ReadOnly Property OutLocation() As String
        Get
            Return txtOutLocation.Text
        End Get
    End Property

    Public ReadOnly Property OutAsset() As String
        Get
            Return txtOutAsset.Text
        End Get
    End Property

    Public ReadOnly Property OutCostCenter() As String
        Get
            Return txtOutItems.Text
        End Get
    End Property

    Public ReadOnly Property OutEmployee() As String
        Get
            Return txtOutEmployee.Text
        End Get
    End Property

    Public ReadOnly Property InExport() As String
        Get
            Return txtExportFile.Text
        End Get
    End Property


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
        Dim regKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("SABBPlugin", True)
        Try
            If regKey IsNot Nothing Then
                regKey.SetValue("OutboundLocation", txtOutLocation.Text)
                regKey.SetValue("OutboundAssets", txtOutAsset.Text)
                regKey.SetValue("OutboundCostCenter", txtOutItems.Text)
                regKey.SetValue("OutboundEmployee", txtOutEmployee.Text)
                regKey.SetValue("InboundExport", txtExportFile.Text)

                regKey.SetValue("ActivteEmail", ChkActivteEmail.Checked)
                regKey.SetValue("SMTP", txtSMTP.Text)
                regKey.SetValue("UserName", txtUserName.Text)
                regKey.SetValue("Password", txtPassword.Text)
                regKey.SetValue("FromAddress", txtFromAddress.Text)
                regKey.SetValue("ToAddress", txtAssetToAddress.Text)
                regKey.SetValue("InvToAddress", txtInventoryToAddress.Text)

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
        GenericDAL.ConnectionString.ServerName = ZulAssetsDAL.ZulAssetsDAL.AppConfig.DbServer
        GenericDAL.ConnectionString.DbName = ZulAssetsDAL.ZulAssetsDAL.AppConfig.DbName
        GenericDAL.ConnectionString.UserName = ZulAssetsDAL.ZulAssetsDAL.AppConfig.DbUname
        GenericDAL.ConnectionString.UserPass = ZulAssetsDAL.ZulAssetsDAL.AppConfig.DbPass
        GenericDAL.ConnectionString.SQLPort = ZulAssetsDAL.ZulAssetsDAL.AppConfig.DBSQLPort
        GenericDAL.ConnectionString.ConnectToDb()
    End Sub

    Public Sub LoadSettings()
        Dim regKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("SABBPlugin", True)
        Try
            If regKey Is Nothing Then
                My.Computer.Registry.CurrentUser.CreateSubKey("SABBPlugin")

            Else
                txtOutLocation.Text = regKey.GetValue("OutboundLocation")
                txtOutAsset.Text = regKey.GetValue("OutboundAssets")
                txtOutItems.Text = regKey.GetValue("OutboundCostCenter")
                txtOutEmployee.Text = regKey.GetValue("OutboundEmployee")
                txtExportFile.Text = regKey.GetValue("InboundExport")

                ChkActivteEmail.Checked = regKey.GetValue("ActivteEmail")
                txtSMTP.Text = regKey.GetValue("SMTP")
                txtUserName.Text = regKey.GetValue("UserName")
                txtPassword.Text = regKey.GetValue("Password")
                txtFromAddress.Text = regKey.GetValue("FromAddress")
                txtAssetToAddress.Text = regKey.GetValue("ToAddress")
                txtInventoryToAddress.Text = regKey.GetValue("InvToAddress")
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

    Private Sub txtOutAsset_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOutLocation.DoubleClick, txtOutEmployee.DoubleClick, txtOutItems.DoubleClick, txtOutAsset.DoubleClick
        With dlgOpen
            .CheckFileExists = True
            .CheckPathExists = True
            .FileName = ""
            .ShowReadOnly = True
            .DefaultExt = "txt"
            .Filter = "Tab-Delimited values (*.txt)|*.txt"
            .Multiselect = False
            .Title = "Tab-Delimited values File"

            If .ShowDialog() = DialogResult.OK Then
                CType(sender, DevExpress.XtraEditors.MemoEdit).Text = .FileName
            End If
        End With
    End Sub

    Private Sub btnOpenDialogAsset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDialogAsset.Click
        txtOutAsset_DoubleClick(txtOutAsset, e)
    End Sub

    Private Sub btnOpenDialogLoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDialogLoc.Click
        txtOutAsset_DoubleClick(txtOutLocation, e)
    End Sub

    Private Sub btnOpenDialogCostCenter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDialogItems.Click
        txtOutAsset_DoubleClick(txtOutItems, e)
    End Sub

    Private Sub btnOpenDialogEmp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDialogEmp.Click
        txtOutAsset_DoubleClick(txtOutEmployee, e)
    End Sub

    Private Sub btnImportAsset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportAsset.Click
        ShowImportForm(txtOutAsset, frmImport.TFileImportType.Asset)
    End Sub

    Private Sub btnImportLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportLocation.Click
        ShowImportForm(txtOutLocation, frmImport.TFileImportType.Location)
    End Sub

    Private Sub btnImportCostCenter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportItems.Click
        ShowImportForm(txtOutItems, frmImport.TFileImportType.Items)
    End Sub

    Private Sub btnImportEmployee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportEmployee.Click
        ShowImportForm(txtOutEmployee, frmImport.TFileImportType.Employees)
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
            txt.ErrorIcon = Nothing
            txt.ErrorText = String.Empty
            Return True
        End If
    End Function

    Public Sub ShowImportForm(ByVal txt As DevExpress.XtraEditors.MemoEdit, ByVal ImportType As frmImport.TFileImportType)
        If CheckFileName(txt) Then
            Dim frm As New frmImport
            SaveSettings()
            frm.ImportFileName = txt.Text
            frm.ImportType = ImportType
            frm.ShowDialog()
            frm.Dispose()
        End If
    End Sub

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

    Public Sub ShowExportForm(ByVal txt As DevExpress.XtraEditors.MemoEdit)
        If CheckFolderName(txt) Then
            SaveSettings()
            Dim frm As New frmExport
            frm.ExportFolderName = txt.Text
            frm.ShowDialog()
            frm.Dispose()
        End If
    End Sub

    Private Sub btnImportAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportAll.Click
        ImportAll()
    End Sub

    Public Sub ImportAll()
        layoutImportProgressLabel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        layoutImportProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lblImportMessages.Visible = True

        SaveSettings()

        Dim frm As New frmImport
        Dim Result As String = String.Empty
        Result = frm.SilentImport(lblImportMessages, PBImport, txtOutLocation.Text, frmImport.TFileImportType.Location)
        Result &= frm.SilentImport(lblImportMessages, PBImport, txtOutItems.Text, frmImport.TFileImportType.Items)
        Result &= frm.SilentImport(lblImportMessages, PBImport, txtOutEmployee.Text, frmImport.TFileImportType.Employees)
        Result &= frm.SilentImport(lblImportMessages, PBImport, txtOutAsset.Text, frmImport.TFileImportType.Asset)
        frm.Dispose()

        layoutImportProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        layoutImportProgressLabel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub


    Private Sub btnSendTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendTest.Click
        SaveSettings()
        Dim msgSub As String = "ZulAssets Email Setting Test"
        Dim msgBody As String = "This is a test email message from zulassets application to verify that email is working correctly. Email DateTime: " & Now.Date.ToString
        Dim msg1 As String = String.Empty
        Dim msg As String = String.Empty

        If AllowAssetAudit Then
            msg = SendMail(txtFromAddress.Text, txtAssetToAddress.Text, txtSMTP.Text, txtUserName.Text, txtPassword.Text, msgSub, msgBody)
        End If

        If AllowInventory Then
            msg1 = SendMail(txtFromAddress.Text, txtInventoryToAddress.Text, txtSMTP.Text, txtUserName.Text, txtPassword.Text, msgSub, msgBody)
        End If

        If msg = "Email sent successfully" Or msg1 = "Email sent successfully" Then
            MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Public Function SendEmail(ByVal msgSub As String, ByVal msgBody As String, ByVal IsInventoryRoleMail As Boolean) As String
        If ChkActivteEmail.Checked Then
            If IsInventoryRoleMail Then
                Dim msg As String = SendMail(txtFromAddress.Text, txtInventoryToAddress.Text, txtSMTP.Text, txtUserName.Text, txtPassword.Text, msgSub, msgBody)
                Return msg
            Else
                Dim msg As String = SendMail(txtFromAddress.Text, txtAssetToAddress.Text, txtSMTP.Text, txtUserName.Text, txtPassword.Text, msgSub, msgBody)
                Return msg
            End If
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
                Dim tr As TaskScheduler.DailyTrigger = task.Triggers(0)
                If tr IsNot Nothing Then
                    If Not String.IsNullOrEmpty(String.Format("{0}{1}", tr.StartHour, tr.StartMinute)) Then
                        txtTime.Time = String.Format("{0}:{1}", tr.StartHour, tr.StartMinute)
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
        SetCheckStatus(chkSchOutAssets, txtTimeAssets)
        SetCheckStatus(chkSchLocation, txtTimeLocation)
        SetCheckStatus(chkSchItems, txtTimeItems)
        SetCheckStatus(chkSchEmployee, txtTimeEmployee)
        SetCheckStatus(chkSchExportData, txtTimeExport)
        IsFormLoad = False
    End Sub

    Private Sub Schedule_CheckedChanged(ByVal sender As System.Object, ByVal txtTime As DevExpress.XtraEditors.TimeEdit)
        If Not IsFormLoad Then
            Dim SchName As String = CType(sender, DevExpress.XtraEditors.CheckEdit).Name.Remove(0, 3)
            If CType(sender, DevExpress.XtraEditors.CheckEdit).Checked Then
                If CreateTaskScheduler(SchName, AdminPassword, txtTime.Time.Hour, txtTime.Time.Minute) Then
                    CType(sender, DevExpress.XtraEditors.CheckEdit).ForeColor = Drawing.Color.Green
                    CType(sender, DevExpress.XtraEditors.CheckEdit).Text = "Running " & txtTime.Time.ToString
                    CType(sender, DevExpress.XtraEditors.CheckEdit).Text = String.Format("{0}({1}:{2}) ", "Running at", txtTime.Time.Hour, txtTime.Time.Minute)

                Else
                    CType(sender, DevExpress.XtraEditors.CheckEdit).Checked = False
                End If
            Else
                If DeleteTaskScheduler(SchName, AdminPassword) Then
                    CType(sender, DevExpress.XtraEditors.CheckEdit).ForeColor = Drawing.Color.Red
                    CType(sender, DevExpress.XtraEditors.CheckEdit).Text = "Not Running"
                End If
            End If
        End If
    End Sub
    Private Sub chkSchOutAssets_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchOutAssets.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeAssets)
    End Sub

    Private Sub chkSchLocation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchLocation.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeLocation)
    End Sub

    Private Sub chkSchCostCenter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchItems.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeItems)
    End Sub

    Private Sub chkSchEmployee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchEmployee.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeEmployee)
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ShowExportForm(txtExportFile)
    End Sub

    Private Sub chkSchExportData_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchExportData.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeExport)
    End Sub

    Private Sub btnOpenDialogExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDialogExport.Click
        txtExportFile_DoubleClick(txtExportFile, e)
    End Sub

    Private Sub txtExportFile_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtExportFile.DoubleClick
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            CType(sender, DevExpress.XtraEditors.MemoEdit).Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub
End Class