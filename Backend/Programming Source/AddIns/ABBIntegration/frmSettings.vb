Imports System.Windows.Forms
Imports ZulAssetsDAL.ZulAssetsDAL
Imports ZulAssetsBL.ZulAssetBAL

Public Class frmABBSettings

    Private _RoleID As Integer
    Private _AdminPassword As String
    Private AllowAssetAudit As Boolean = False
    Private AllowInventory As Boolean = False
    Private IsFormLoad As Boolean = False

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
            tabMain.SelectedTabPage = layoutOutboundFiles
            If Not AllowAssetAudit Then
                layoutOutboundFiles.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                layoutInboundAssetChange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                layoutInboundPhysicalInv.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutAssetToAddress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                tabMain.SelectedTabPage = layoutInboundFiles
            End If
            If Not AllowInventory Then
                layoutInboundGI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                layoutInboundGR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutInvToAddress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                tabMain.SelectedTabPage = layoutOutboundFiles
            End If

            If Not AllowInventory And Not AllowAssetAudit Then
                tabMain.SelectedTabPage = layoutMailSettings
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

    Public ReadOnly Property OutboundAssetCapitalization() As String
        Get
            Return txtOutAssetCapitalization.Text
        End Get
    End Property


    Public ReadOnly Property OutboundAssetMasterChange() As String
        Get
            Return txtOutAssetMasterChange.Text
        End Get
    End Property

    Public ReadOnly Property OutboundAssetMasterCreation() As String
        Get
            Return txtOutAssetMasterCreation.Text
        End Get
    End Property

    Public ReadOnly Property OutboundAssetRetirement() As String
        Get
            Return txtOutAssetRetirment.Text
        End Get
    End Property

    Public ReadOnly Property OutboundAssetswithValue() As String
        Get
            Return txtOutAssetWithValue.Text
        End Get
    End Property
    Public ReadOnly Property InboundAssetChange() As String
        Get
            Return txtInAssetChange.Text
        End Get
    End Property

    Public ReadOnly Property InboundPhysicalInventory() As String
        Get
            Return txtInPhysicalInventory.Text()
        End Get
    End Property

    Public ReadOnly Property InboundGoodsReceiving() As String
        Get
            Return txtGoodReceive.Text()
        End Get
    End Property
    Public ReadOnly Property InboundGoodsIssuance() As String
        Get
            Return txtGoodsIssuance.Text()
        End Get
    End Property

    Public ReadOnly Property InboundReversalDoc() As String
        Get
            Return txtReversalGR.Text
        End Get
    End Property

    Public ReadOnly Property InboundVerndorReturn() As String
        Get
            Return txtVendorReturn.Text
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
        Dim regKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("ABBIntegration", True)
        Try
            If regKey IsNot Nothing Then
                regKey.SetValue("OutboundAssetMasterChange", txtOutAssetMasterChange.Text)
                regKey.SetValue("OutboundAssetMasterCreation", txtOutAssetMasterCreation.Text)
                regKey.SetValue("OutboundAssetRetirement", txtOutAssetRetirment.Text)
                regKey.SetValue("OutboundAssetswithValue", txtOutAssetWithValue.Text)
                regKey.SetValue("OutboundAssetCapitalization", txtOutAssetCapitalization.Text)
                regKey.SetValue("InboundAssetChange", txtInAssetChange.Text)
                regKey.SetValue("InboundPhysicalInventory", txtInPhysicalInventory.Text)
                regKey.SetValue("InboundGoodsReceive", txtGoodReceive.Text)
                regKey.SetValue("InboundGoodsIssuance", txtGoodsIssuance.Text)

                regKey.SetValue("InboundReversalGR", txtReversalGR.Text)
                regKey.SetValue("InboundReversalGI", txtReversalGI.Text)
                regKey.SetValue("InboundVendorReturn", txtVendorReturn.Text)

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

        AppConfig.AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\ZulAssets"
        AppConfig.DatabaseConfigFilePath = AppConfig.AppDataFolder & "\Data.xml"
        ZulLib.Messages.AppDataFolder = AppConfig.AppDataFolder

        objBalappConfig.LoadSettings()
    End Sub

    Public Sub LoadSettings()
        Dim regKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("ABBIntegration", True)
        Try
            If regKey Is Nothing Then
                My.Computer.Registry.CurrentUser.CreateSubKey("ABBIntegration")

            Else
                txtOutAssetMasterChange.Text = regKey.GetValue("OutboundAssetMasterChange")
                txtOutAssetMasterCreation.Text = regKey.GetValue("OutboundAssetMasterCreation")
                txtOutAssetRetirment.Text = regKey.GetValue("OutboundAssetRetirement")
                txtOutAssetWithValue.Text = regKey.GetValue("OutboundAssetswithValue")
                txtOutAssetCapitalization.Text = regKey.GetValue("OutboundAssetCapitalization")
                txtInAssetChange.Text = regKey.GetValue("InboundAssetChange")
                txtInPhysicalInventory.Text = regKey.GetValue("InboundPhysicalInventory")

                txtGoodReceive.Text = regKey.GetValue("InboundGoodsReceive")
                txtGoodsIssuance.Text = regKey.GetValue("InboundGoodsIssuance")

                txtReversalGR.Text = regKey.GetValue("InboundReversalGR")
                txtReversalGI.Text = regKey.GetValue("InboundReversalGI")

                txtVendorReturn.Text = regKey.GetValue("InboundVendorReturn")

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
    End Sub

    Private Sub txtOutAssetMasterChange_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOutAssetMasterChange.DoubleClick, txtOutAssetWithValue.DoubleClick, txtOutAssetRetirment.DoubleClick, txtOutAssetMasterCreation.DoubleClick, txtInPhysicalInventory.DoubleClick, txtInAssetChange.DoubleClick, txtOutAssetCapitalization.DoubleClick
        With OpenFileDialog1

            .CheckFileExists = True
            .CheckPathExists = True
            .FileName = ""
            .ShowReadOnly = True
            .DefaultExt = "txt"

            .Filter = "Text Files (*.txt)|*.txt"

            .Multiselect = False
            .Title = "Text Files"

            If .ShowDialog() = DialogResult.OK Then
                CType(sender, DevExpress.XtraEditors.MemoEdit).Text = .FileName
            End If
        End With
    End Sub
    Private Sub txtGoodReceive_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGoodReceive.DoubleClick, txtGoodsIssuance.DoubleClick, txtReversalGR.DoubleClick, txtVendorReturn.DoubleClick, txtReversalGI.DoubleClick
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            CType(sender, DevExpress.XtraEditors.MemoEdit).Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub


    Private Sub btnOpenDialog1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDialog1.Click
        txtOutAssetMasterChange_DoubleClick(txtOutAssetMasterCreation, e)
    End Sub

    Private Sub btnOpenDialog2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDialog2.Click
        txtOutAssetMasterChange_DoubleClick(txtOutAssetMasterChange, e)
    End Sub

    Private Sub btnOpenDialog3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDialog3.Click
        txtOutAssetMasterChange_DoubleClick(txtOutAssetRetirment, e)
    End Sub

    Private Sub btnOpenDialog4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDialog4.Click
        txtOutAssetMasterChange_DoubleClick(txtOutAssetCapitalization, e)
    End Sub

    Private Sub btnOpenDialog5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDialog5.Click
        txtOutAssetMasterChange_DoubleClick(txtInAssetChange, e)
    End Sub

    Private Sub btnOpenDialog6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDialog6.Click
        txtOutAssetMasterChange_DoubleClick(txtInPhysicalInventory, e)
    End Sub

    Private Sub btnImportAssetCapitalization_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportAssetCapitalization.Click
        ShowImportForm(txtOutAssetCapitalization, frmImport.TFileImportType.AssetCapitalization)
    End Sub

    Private Sub btnOpenDialog7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDialog7.Click
        txtOutAssetMasterChange_DoubleClick(txtOutAssetWithValue, e)
    End Sub
    Private Sub btnImportAssetCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportAssetCreate.Click
        ShowImportForm(txtOutAssetMasterCreation, frmImport.TFileImportType.AssetCreate)
    End Sub

    Private Sub btnImportAssetChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportAssetChange.Click
        ShowImportForm(txtOutAssetMasterChange, frmImport.TFileImportType.AssetChange)
    End Sub

    Private Sub btnImportAssetRetirement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportAssetRetirement.Click
        ShowImportForm(txtOutAssetRetirment, frmImport.TFileImportType.AssetRetirment)
    End Sub

    Private Sub btnImportAssetValue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportAssetValue.Click
        ShowImportForm(txtOutAssetWithValue, frmImport.TFileImportType.AssetWithValue)
    End Sub

    Private Sub btnExportAssetChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportAssetChange.Click
        ShowExportForm(txtInAssetChange, frmExport.TFileExportType.AssetChange)
    End Sub

    Private Sub btnExportPhysicalInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportPhysicalInventory.Click
        'If String.IsNullOrEmpty(cmbInventorySchedule.Text) Then
        '    cmbInventorySchedule.ErrorIcon = My.Resources.Invalid
        '    cmbInventorySchedule.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
        '    cmbInventorySchedule.ErrorText = "Inventory Schedule not selected!."
        'Else
        '    cmbInventorySchedule.ErrorIcon = Nothing
        '    cmbInventorySchedule.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
        '    cmbInventorySchedule.ErrorText = String.Empty
        '    ShowExportForm(txtInPhysicalInventory, frmExport.TFileExportType.PhysicalInventory)
        'End If
        ShowExportForm(txtInPhysicalInventory, frmExport.TFileExportType.PhysicalInventory)
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


    Public Sub ShowExportForm(ByVal txt As DevExpress.XtraEditors.MemoEdit, ByVal ExportType As frmExport.TFileExportType)
        Select Case ExportType
            Case frmExport.TFileExportType.GoodsIssuance, frmExport.TFileExportType.GoodsReceive, frmExport.TFileExportType.ReversalGRDoc, frmExport.TFileExportType.ReversalGIDoc, frmExport.TFileExportType.VendorReturn
                If CheckFolderName(txt) Then
                    SaveSettings()
                    Dim frm As New frmExport
                    frm.ExportFolderName = txt.Text
                    frm.ExportType = ExportType
                    frm.ShowDialog()
                    frm.Dispose()
                End If
            Case Else
                If CheckFileName(txt) Then
                    SaveSettings()
                    Dim frm As New frmExport
                    frm.ExportFileName = txt.Text
                    frm.ExportType = ExportType
                    frm.ShowDialog()
                    frm.Dispose()
                End If
        End Select
    End Sub

    Private Sub btnImportAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportAll.Click
        ImportAll()
    End Sub
    Public Sub ExportAll()
        layoutExportProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        layoutExportProgressLabel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        SaveSettings()
        Dim frm As New frmExport
        If AllowAssetAudit Then
            frm.ExportDataFile(lblExportMessages, pbExport, txtInAssetChange.Text, frmExport.TFileExportType.AssetChange, True, False)
            frm.ExportDataFile(lblExportMessages, pbExport, txtInPhysicalInventory.Text, frmExport.TFileExportType.PhysicalInventory, True, False)
        End If

        If AllowInventory Then
            Dim FileName As String = frm.GetExportFileName(txtGoodReceive.Text, frmExport.TFileExportType.GoodsReceive)
            frm.ExportDataFile(lblExportMessages, pbExport, FileName, frmExport.TFileExportType.GoodsReceive, True, True)

            FileName = frm.GetExportFileName(txtGoodsIssuance.Text, frmExport.TFileExportType.GoodsIssuance)
            frm.ExportDataFile(lblExportMessages, pbExport, FileName, frmExport.TFileExportType.GoodsIssuance, True, True)


            FileName = frm.GetExportFileName(txtReversalGR.Text, frmExport.TFileExportType.ReversalGRDoc)
            frm.ExportDataFile(lblExportMessages, pbExport, FileName, frmExport.TFileExportType.ReversalGRDoc, True, True)

            FileName = frm.GetExportFileName(txtReversalGI.Text, frmExport.TFileExportType.ReversalGIDoc)
            frm.ExportDataFile(lblExportMessages, pbExport, FileName, frmExport.TFileExportType.ReversalGIDoc, True, True)

            FileName = frm.GetExportFileName(txtVendorReturn.Text, frmExport.TFileExportType.VendorReturn)
            frm.ExportDataFile(lblExportMessages, pbExport, FileName, frmExport.TFileExportType.VendorReturn, True, True)
        End If

        frm.Dispose()
        layoutExportProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        layoutExportProgressLabel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub
    Public Sub ImportAll()
        layoutImportProgressLabel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        layoutImportProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lblImportMessages.Visible = True
        SaveSettings()
        Dim frm As New frmImport
        frm.SilentImport(lblImportMessages, PBImport, txtOutAssetMasterCreation.Text, frmImport.TFileImportType.AssetCreate)
        frm.SilentImport(lblImportMessages, PBImport, txtOutAssetMasterChange.Text, frmImport.TFileImportType.AssetChange)
        frm.SilentImport(lblImportMessages, PBImport, txtOutAssetRetirment.Text, frmImport.TFileImportType.AssetRetirment)
        frm.SilentImport(lblImportMessages, PBImport, txtOutAssetWithValue.Text, frmImport.TFileImportType.AssetWithValue)
        frm.SilentImport(lblImportMessages, PBImport, txtOutAssetCapitalization.Text, frmImport.TFileImportType.AssetCapitalization)
        frm.Dispose()
        layoutImportProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        layoutImportProgressLabel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub
    Private Sub btnExportAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportAll.Click
        ExportAll()
    End Sub


    Private Sub btnOpenDialog12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDialog12.Click
        txtGoodReceive_DoubleClick(txtGoodReceive, e)
    End Sub

    Private Sub btnOpenDialog11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDialog11.Click
        txtGoodReceive_DoubleClick(txtGoodsIssuance, e)
    End Sub

    Private Sub btnExportGR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportGR.Click
        ShowExportForm(txtGoodReceive, frmExport.TFileExportType.GoodsReceive)
    End Sub

    Private Sub btnExportGI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportGI.Click
        ShowExportForm(txtGoodsIssuance, frmExport.TFileExportType.GoodsIssuance)
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
        SetCheckStatus(chkSchOutMasterCreate, txtTimeMasterCreate)
        SetCheckStatus(chkSchOutMasterChange, txtTimeMasterChange)
        SetCheckStatus(chkSchOutRetire, txtTimeRetire)
        SetCheckStatus(chkSchOutValue, txtTimeValue)
        SetCheckStatus(chkSchOutCapitalize, txtTimeCapitalize)
        SetCheckStatus(chkSchInAssetChange, txtTimeInAssetChange)
        SetCheckStatus(chkSchInPhysicalInv, txtTimeInPhysicalInventory)
        SetCheckStatus(chkSchInGR, txtTimeInGR)
        SetCheckStatus(chkSchInGI, txtTimeInGI)
        SetCheckStatus(chkSchInReversalGR, txtTimeInReversalGR)

        SetCheckStatus(chkSchInReversalGI, txtTimeInReversalGI)

        SetCheckStatus(chkSchInVendorReturn, txtTimeInVendorReturn)

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
    Private Sub chkSchOutMasterCreate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchOutMasterCreate.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeMasterCreate)
    End Sub

    Private Sub chkOutMasterChange_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchOutMasterChange.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeMasterChange)
    End Sub

    Private Sub chkOutRetire_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchOutRetire.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeRetire)
    End Sub

    Private Sub chkOutValue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchOutValue.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeValue)
    End Sub

    Private Sub chkOutCapitalize_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSchOutCapitalize.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeCapitalize)
    End Sub

    Private Sub chkSchInAssetChange_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchInAssetChange.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeInAssetChange)
    End Sub

    Private Sub chkSchInPhysicalInv_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchInPhysicalInv.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeInPhysicalInventory)
    End Sub

    Private Sub chkSchInGR_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchInGR.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeInGR)
    End Sub

    Private Sub chkSchInGI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchInGI.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeInGI)
    End Sub

    Private Sub chkSchInReversalGR_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchInReversalGR.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeInReversalGR)
    End Sub

    Private Sub chkSchInVendorReturn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchInVendorReturn.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeInVendorReturn)
    End Sub


    Private Sub btnOpenFolderReversalGR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFolderReversalGR.Click
        txtGoodReceive_DoubleClick(txtReversalGR, e)
    End Sub

    Private Sub btnOpenFolderVendor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFolderVendor.Click
        txtGoodReceive_DoubleClick(txtVendorReturn, e)
    End Sub

    Private Sub btnExportReversalGR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportReversalGR.Click
        ShowExportForm(txtReversalGR, frmExport.TFileExportType.ReversalGRDoc)
    End Sub

    Private Sub btnExportVendorReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportVendorReturn.Click
        ShowExportForm(txtVendorReturn, frmExport.TFileExportType.VendorReturn)
    End Sub

    Private Sub btnOpenFolderReversalGI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFolderReversalGI.Click
        txtGoodReceive_DoubleClick(txtReversalGI, e)
    End Sub

    Private Sub btnExportReversalGI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportReversalGI.Click
        ShowExportForm(txtReversalGI, frmExport.TFileExportType.ReversalGIDoc)
    End Sub

    Private Sub chkSchInReversalGI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchInReversalGI.CheckedChanged
        Schedule_CheckedChanged(sender, txtTimeInReversalGI)
    End Sub
End Class