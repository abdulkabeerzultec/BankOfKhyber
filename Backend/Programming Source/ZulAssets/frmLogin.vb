Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports TaskScheduler
Imports System.OperatingSystem
Imports System.Environment
Imports System.Data.OleDb
Imports System.IO


Public Class frmLogin
    Dim strFontFolder As String
    Dim objBALConfig As New BALAppConfig
    Public sendingform As Form
    Public Shared gDBType As Integer
    Dim objattUsers As ZulAssetsDAL.ZulAssetsDAL.attUsers
    Dim objBALUsers As New ZulAssetsBL.ZulAssetBAL.BALUsers


    Private Sub frmLogin_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            Application.Exit()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub GetIntegrationType()

        If File.Exists(Application.StartupPath & "\CMAIntegrationSettings.xml") Then
            IntegrationName = IntegrationType.CMAIntegration
        ElseIf File.Exists(Application.StartupPath & "\ABBIntegrationSettings.xml") Then
            IntegrationName = IntegrationType.ABBIntegration
        ElseIf File.Exists(Application.StartupPath & "\AlhadaIntegrationSettings.xml") Then
            IntegrationName = IntegrationType.AlhadaIntegration
        ElseIf File.Exists(Application.StartupPath & "\FairMontIntegrationSettings.xml") Then
            IntegrationName = IntegrationType.FairMontIntegration
        ElseIf File.Exists(Application.StartupPath & "\KPMGIntegrationSettings.xml") Then
            IntegrationName = IntegrationType.KPMGIntegration
        ElseIf File.Exists(Application.StartupPath & "\NahdiIntegrationSettings.xml") Then
            IntegrationName = IntegrationType.NahdiIntegration
        ElseIf File.Exists(Application.StartupPath & "\RetajIntegrationSettings.xml") Then
            IntegrationName = IntegrationType.RetajIntegration
        Else 'No Integratoin found
            IntegrationName = IntegrationType.None
        End If
    End Sub

    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.LoginScreen
        Me.BackgroundImageLayout = ImageLayout.Stretch
        If chkDefaultPass.Checked Then
            txtAppUname.Text = "Admin"
            txtPass.Text = "pass"
        End If

        'Kabeer Registration
        'If Not AppConfig.ISRegistered Or AppConfig.AppEdition = ApplicationEditions.NotRegistered Then
        '    ZulMessageBox.ShowMe("InvalidRegisfound")
        '    Application.Exit()
        'End If

    

        Try
            Dim splash As New frmSplash
            splash.IsSplash = True
            splash.ShowDialog()

            ' load the sysconfig settings.
            objBALConfig.LoadSettings()
            txtDBPass.Text = AppConfig.DbPass
            cmbDBServer.Text = AppConfig.DbServer
            txtDBUname.Text = AppConfig.DbUname
            txtSysPass.Text = AppConfig.SysPass
            txtSqlPort.Text = AppConfig.DBSQLPort
            cmbDatabase.Text = AppConfig.DbName

            cmbCommServ.Text = AppConfig.ComServer
            txtComUname.Text = AppConfig.ComUname
            txtComPass.Text = AppConfig.ComPass
            txtComSerPort.Text = AppConfig.CommPort
            cmbComDB.Text = AppConfig.ComName


            cmbExpServ.Text = AppConfig.ExpServer
            txtExpUname.Text = AppConfig.ExpUname
            txtExpPass.Text = AppConfig.ExpPass
            txtExpSerPort.Text = AppConfig.ExpPort
            cmbExpDB.Text = AppConfig.ExpName

            If AppConfig.DbType <> "" Then
                cmbdbType.SelectedIndex = CInt(AppConfig.DbType) - 1
                If cmbdbType.SelectedIndex + 1 = 1 Then
                    Label7.Visible = True
                    txtSqlPort.Visible = True
                Else
                    Label7.Visible = False
                    txtSqlPort.Visible = False
                End If
            Else
                cmbdbType.SelectedIndex = 0
                Label7.Visible = True
                txtSqlPort.Visible = True
            End If

            TabControl1.TabIndex = 0

            GetIntegrationType()

            If objBALConfig.GetConnected(cmbDBServer.Text, cmbDatabase.Text, txtDBUname.Text, txtDBPass.Text, Trim(txtSqlPort.Text), cmbdbType.SelectedIndex + 1, Trim(txtComUname.Text), Trim(txtComPass.Text), Trim(txtComSerPort.Text), Trim(cmbCommServ.Text), Trim(cmbComDB.Text)) Then
                ShowHideAdvanceTabs(False)
                'txtDBConnect.ErrorIcon = My.Resources.Icons.Connected
                'txtDBConnect.ErrorText = "Connected to " & cmbDatabase.Text
                'txtComServerConnect.ErrorIcon = My.Resources.Icons.Connected
                'txtComServerConnect.ErrorText = "Connected to " & cmbComDB.Text
            Else
                ShowHideAdvanceTabs(True)
                'txtDBConnect.ErrorIcon = My.Resources.Icons.ConnectError
                'txtDBConnect.ErrorText = "Not Connected"
                'txtComServerConnect.ErrorIcon = My.Resources.Icons.ConnectError
                'txtComServerConnect.ErrorText = "Not Connected"
            End If
            'txtExportServerConnect.ErrorIcon = My.Resources.Icons.ConnectError
            'txtExportServerConnect.ErrorText = "Not Connected"

            txtAppUname.Focus()

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub ShowHideAdvanceTabs(ByVal Show As Boolean)
        If Show Then
            If Not TabControl1.TabPages.Contains(TabPage2) Then
                TabControl1.TabPages.Add(TabPage2)
            End If
            If Not TabControl1.TabPages.Contains(TabPage3) Then
                TabControl1.TabPages.Add(TabPage3)
            End If

            'Show Export Server tab only if AlhadaIntegration is there.
            If Not TabControl1.TabPages.Contains(TabPage4) Then
                If IntegrationName = IntegrationType.AlhadaIntegration Then
                    TabControl1.TabPages.Add(TabPage4)
                End If
            Else
                If IntegrationName <> IntegrationType.AlhadaIntegration Then
                    TabControl1.TabPages.Remove(TabPage4)
                End If
            End If
        Else
            If TabControl1.TabPages.Contains(TabPage2) Then
                TabControl1.TabPages.Remove(TabPage2)
            End If
            If TabControl1.TabPages.Contains(TabPage3) Then
                TabControl1.TabPages.Remove(TabPage3)
            End If
            If TabControl1.TabPages.Contains(TabPage4) Then
                TabControl1.TabPages.Remove(TabPage4)
            End If
        End If
    End Sub


    Private Function CreateVirtualDir(ByVal WebSite As String, ByVal AppName As String, ByVal Path As String) As Boolean

        Try
            Dim IISSchema As New System.DirectoryServices.DirectoryEntry("IIS://" & WebSite & "/Schema/AppIsolated")
            Dim CanCreate As Boolean = Not IISSchema.Properties("Syntax").Value.ToString.ToUpper() = "BOOLEAN"
            IISSchema.Dispose()

            If CanCreate Then
                Dim PathCreated As Boolean
                Try
                    Dim IISAdmin As New System.DirectoryServices.DirectoryEntry("IIS://" & WebSite & "/W3SVC/1/Root")

                    'make sure folder exists
                    If Not System.IO.Directory.Exists(Path) Then
                        System.IO.Directory.CreateDirectory(Path)
                        PathCreated = True
                    End If

                    'If the virtual directory already exists then delete it
                    For Each VD As System.DirectoryServices.DirectoryEntry In IISAdmin.Children
                        If VD.Name = AppName Then
                            IISAdmin.Invoke("Delete", New String() {VD.SchemaClassName, AppName})
                            IISAdmin.CommitChanges()
                            Exit For
                        End If
                    Next VD
                    'Create and setup new virtual directory
                    Dim VDir As System.DirectoryServices.DirectoryEntry = IISAdmin.Children.Add(AppName, "IIsWebVirtualDir")
                    VDir.Properties("Path").Item(0) = Path
                    VDir.Properties("AppFriendlyName").Item(0) = AppName
                    VDir.Properties("EnableDirBrowsing").Item(0) = False
                    VDir.Properties("AccessRead").Item(0) = True
                    VDir.Properties("AccessExecute").Item(0) = True
                    VDir.Properties("AccessWrite").Item(0) = True
                    VDir.Properties("AccessScript").Item(0) = True
                    VDir.Properties("AuthNTLM").Item(0) = True
                    VDir.Properties("EnableDefaultDoc").Item(0) = True
                    VDir.Properties("DefaultDoc").Item(0) = "default.htm,default.aspx,default.asp"
                    VDir.Properties("AspEnableParentPaths").Item(0) = True
                    VDir.CommitChanges()


                    VDir.Invoke("AppCreate", 1)

                Catch Ex As Exception
                    If PathCreated Then
                        System.IO.Directory.Delete(Path)
                    End If
                    Throw Ex
                    Return False
                End Try
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Function

    Private Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal hProcess As IntPtr, ByVal dwMinimumWorkingSetSize As Int32, ByVal dwMaximumWorkingSetSize As Int32) As Int32

    Public Sub ReduceMemoryUsage()
        Try
            GC.Collect()
            GC.Collect()
            SetProcessWorkingSetSize(Diagnostics.Process.GetCurrentProcess.Handle, -1, -1)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub ExecuteBatFile(ByVal batchfilename As String)
        Try
            Dim m_Process As New System.Diagnostics.Process()
            With m_Process.StartInfo
                .FileName = batchfilename
                .UseShellExecute = False

                .CreateNoWindow = True


            End With


            m_Process.Start()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Public Sub CreateDefaultRecords()


        Dim objRole As New BALRoles
        objRole.CreateDefaultRecord()

        Dim objBALUsers As New BALUsers
        objBALUsers.CreateDefaultRecord()

        Dim objBALBarCode_Struct As New BALBarCode_Struct
        objBALBarCode_Struct.CreateDefaultRecord()
    End Sub

    Private Sub LoginMe()
        'Kabeer Registration
        'If Not AppConfig.ISRegistered Or AppConfig.AppEdition = ApplicationEditions.NotRegistered Then
        '    ZulMessageBox.ShowMe("InvalidRegisfound")
        '    Application.Exit()
        'End If

        Dim DBServer As String = cmbDBServer.Text.Trim
        Dim DBName As String = cmbDatabase.Text.Trim
        Dim DBUserName As String = txtDBUname.Text.Trim
        Dim DBPass As String = txtDBPass.Text.Trim
        Dim sysPass As String = txtSysPass.Text.Trim
        Dim DBSQLPort As String = txtSqlPort.Text.Trim
        Dim DBType As String = cmbdbType.SelectedIndex + 1
        Dim ComDBUserName As String = txtComUname.Text.Trim
        Dim ComDBPass As String = txtComPass.Text.Trim
        Dim ComDBSQLPort As String = txtComSerPort.Text.Trim
        Dim ComDBServer As String = cmbCommServ.Text.Trim
        Dim ComDBName As String = cmbComDB.Text.Trim


        If objBALConfig.GetConnected(DBServer, DBName, DBUserName, DBPass, DBSQLPort, DBType, ComDBUserName, ComDBPass, ComDBSQLPort, ComDBServer, ComDBName) Then
            If Trim(txtAppUname.Text) <> "" And Trim(txtPass.Text) <> "" Then
                objBALConfig.Update_dbConfig(DBServer, DBName, DBUserName, DBPass, sysPass, DBSQLPort, DBType, ComDBUserName, ComDBPass, ComDBSQLPort, ComDBServer, ComDBName, Trim(txtExpUname.Text), Trim(txtExpPass.Text), Trim(txtExpSerPort.Text), Trim(cmbExpServ.Text), Trim(cmbExpDB.Text))
                objattUsers = New attUsers()
                objattUsers.LoginName = RemoveUnnecessaryChars(txtAppUname.Text)
                objattUsers.Password = RemoveUnnecessaryChars(txtPass.Text)
                Dim RoleID As String = "0"
                If objBALUsers.VerifyDesktop_User(objattUsers, cmbDBServer.Text, cmbDatabase.Text, txtDBUname.Text, txtDBPass.Text, psEncrypt(Trim(txtSysPass.Text)), Trim(txtSqlPort.Text), cmbdbType.SelectedIndex + 1, Trim(txtComUname.Text), Trim(txtComPass.Text), Trim(txtComSerPort.Text), Trim(cmbCommServ.Text), Trim(cmbComDB.Text)) = True Then
                    Dim dt As DataTable
                    dt = objBALUsers.get_user_Role(objattUsers)
                    If dt Is Nothing = False Then
                        If dt.Rows.Count > 0 Then
                            RoleID = dt.Rows(0)("RoleID")
                        End If
                    End If
                    AppConfig.Set_User(txtAppUname.Text, "")
                    ReduceMemoryUsage()
                    Try
                        ' load the sysconfig settings.
                        objBALConfig.Get_SysSettings()
                    Catch ex As Exception
                        GenericExceptionHandler(ex, WhoCalledMe)
                    End Try

                    Dim frm As New frmMain
                    frm.RoleID = RoleID
                    frm.Show()
                    Me.Hide()
                Else
                    txtPass.Text = ""
                    TabControl1.SelectedIndex = 0
                    ZulMessageBox.ShowMe("UserLoginFail")
                End If '//User Login
            Else '//User mandatory
                TabControl1.SelectedIndex = 0
                ZulMessageBox.ShowMe("UserLoginMandatory")

            End If '//User mandatory
        Else '//Database Login Test
            txtDBUname.Text = ""
            txtDBPass.Text = ""
            TabControl1.SelectedIndex = 1
            ZulMessageBox.ShowMe("DBLoginFail")
        End If '//Database Login Test
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub txtPass_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPass.KeyPress
        Try
            If e.KeyChar = Chr(13) Then
                btnLogin_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub dbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdbType.SelectedIndexChanged
        If cmbdbType.SelectedIndex = 0 Then
            Label7.Visible = True
            txtSqlPort.Visible = True

            btnGetservers.Visible = True
            btnGetDatabases.Visible = True
            'cmbDBServer.DropDownStyle = ComboBoxStyle.DropDown
            'cmbDBServer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Else
            Label7.Visible = False
            txtSqlPort.Visible = False

            cmbDatabase.Items.Clear()
            cmbDatabase.Refresh()

            btnGetservers.Visible = False
            btnGetDatabases.Visible = False
            'cmbDBServer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard

            'cmbDBServer.DropDownStyle = ComboBoxStyle.Simple
        End If
    End Sub

    Private Sub chkSame_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSame.CheckedChanged
        If chkSame.Checked = True Then
            cmbCommServ.Text = cmbDBServer.Text
            txtComSerPort.Text = txtSqlPort.Text
            txtComPass.Text = txtDBPass.Text
            txtComUname.Text = txtDBUname.Text
        Else

            cmbCommServ.Text = ""
            txtComSerPort.Text = ""
            txtComPass.Text = ""
            txtComUname.Text = ""
        End If
    End Sub

    Private Sub txtDBServer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDBPass.TextChanged, txtDBUname.TextChanged, cmbDBServer.TextChanged
        cmbDatabase.Text = String.Empty
        cmbDatabase.Items.Clear()
        cmbDatabase.Refresh()
    End Sub

    Private Sub txtCommServ_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtComPass.TextChanged, txtComUname.TextChanged, cmbCommServ.TextChanged
        cmbComDB.Text = String.Empty
        cmbComDB.Items.Clear()
        cmbComDB.Refresh()
    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        LoginMe()
    End Sub


    Private Sub chkExpSame_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkExpSame.CheckedChanged
        If chkExpSame.Checked = True Then
            cmbExpServ.Text = cmbDBServer.Text
            txtExpSerPort.Text = txtSqlPort.Text
            txtExpPass.Text = txtDBPass.Text
            txtExpUname.Text = txtDBUname.Text
        Else
            cmbExpServ.Text = ""
            txtExpSerPort.Text = ""
            txtExpPass.Text = ""
            txtExpUname.Text = ""
        End If
    End Sub

    Private Sub chkShowServerConfig_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowServerConfig.CheckedChanged
        If chkShowServerConfig.Checked Then
            If objBALConfig.GetConnected(cmbDBServer.Text, cmbDatabase.Text, txtDBUname.Text, txtDBPass.Text, Trim(txtSqlPort.Text), cmbdbType.SelectedIndex + 1, Trim(txtComUname.Text), Trim(txtComPass.Text), Trim(txtComSerPort.Text), Trim(cmbCommServ.Text), Trim(cmbComDB.Text)) Then
                If Trim(txtAppUname.Text) <> "" And Trim(txtPass.Text) <> "" Then
                    objBALConfig.Update_dbConfig(cmbDBServer.Text, cmbDatabase.Text, txtDBUname.Text, txtDBPass.Text, Trim(txtSysPass.Text), Trim(txtSqlPort.Text), cmbdbType.SelectedIndex + 1, Trim(txtComUname.Text), Trim(txtComPass.Text), Trim(txtComSerPort.Text), Trim(cmbCommServ.Text), Trim(cmbComDB.Text), Trim(txtExpUname.Text), Trim(txtExpPass.Text), Trim(txtExpSerPort.Text), Trim(cmbExpServ.Text), Trim(cmbExpDB.Text))
                    objattUsers = New attUsers()
                    objattUsers.LoginName = RemoveUnnecessaryChars(txtAppUname.Text)
                    objattUsers.Password = RemoveUnnecessaryChars(txtPass.Text)
                    Dim RoleID As String = "0"
                    If objBALUsers.VerifyDesktop_User(objattUsers, cmbDBServer.Text, cmbDatabase.Text, txtDBUname.Text, txtDBPass.Text, psEncrypt(Trim(txtSysPass.Text)), Trim(txtSqlPort.Text), cmbdbType.SelectedIndex + 1, Trim(txtComUname.Text), Trim(txtComPass.Text), Trim(txtComSerPort.Text), Trim(cmbCommServ.Text), Trim(cmbComDB.Text)) = True Then
                        Dim dt As DataTable
                        dt = objBALUsers.get_user_Role(objattUsers)
                        If dt IsNot Nothing Then
                            If dt.Rows.Count > 0 Then
                                RoleID = dt.Rows(0)("RoleID")
                            End If
                        End If
                    End If
                    If RoleID = 1 Then
                        ShowHideAdvanceTabs(True)
                    Else
                        ShowHideAdvanceTabs(False)
                        MessageBox.Show("Insufficient privileges. Contact system administrator!", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        chkShowServerConfig.Checked = False

                    End If
                Else
                    ShowHideAdvanceTabs(False)
                    MessageBox.Show("Insufficient privileges. Contact system administrator!", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    chkShowServerConfig.Checked = False
                End If
            Else
                ShowHideAdvanceTabs(True)
            End If
        Else
            ShowHideAdvanceTabs(False)
        End If
    End Sub



    Private Sub btnGetDatabases_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDatabases.Click
        Dim txt As String = cmbDatabase.Text
        cmbDatabase.Items.Clear()
        cmbDatabase.Text = String.Empty
        Dim dt As DataTable = GetDatabasesList(cmbDBServer.Text, Val(txtSqlPort.Text), txtDBUname.Text, txtDBPass.Text)
        For Each dr As DataRow In dt.Rows
            cmbDatabase.Items.Add(dr(0))
        Next
        If cmbDatabase.Items.Contains(txt) Then
            cmbDatabase.Text = txt
        End If
    End Sub

    'Private Sub FillServerList(ByVal cmb As ComboBox)
    '    If cmb.Items.Count = 0 Then
    '        Dim oTable As New Data.DataTable
    '        Try
    '            If cmb.Items.Count = 0 Then
    '                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    '                oTable = System.Data.Sql.SqlDataSourceEnumerator.Instance.GetDataSources
    '                cmb.BeginUpdate()
    '                For Each oRow As DataRow In oTable.Rows
    '                    If oRow("InstanceName").ToString = "" Then
    '                        cmb.Items.Add(oRow("ServerName"))
    '                    Else
    '                        cmb.Items.Add(oRow("ServerName").ToString & "\" & oRow("InstanceName").ToString)
    '                    End If
    '                Next oRow
    '                cmb.EndUpdate()
    '            End If
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message, "ZulAsset", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Finally
    '            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '            oTable.Dispose()
    '        End Try
    '    End If
    'End Sub

    Private Sub btnGetServers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetservers.Click
        FillServerList(cmbDBServer)
    End Sub


    Private Sub getComServers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngetComServers.Click
        FillServerList(cmbCommServ)
    End Sub

    Private Sub btnGetComDatabase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetComDatabase.Click
        Dim txt As String = cmbComDB.Text
        cmbComDB.Items.Clear()
        cmbComDB.Text = String.Empty

        Dim dt As DataTable = GetDatabasesList(cmbCommServ.Text, Val(txtComSerPort.Text), txtComUname.Text, txtComPass.Text)
        For Each dr As DataRow In dt.Rows
            cmbComDB.Items.Add(dr(0))
        Next
        If cmbComDB.Items.Contains(txt) Then
            cmbComDB.Text = txt
        End If
    End Sub

    Private Sub btnGetExportServers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetExportServers.Click
        FillServerList(cmbExpServ)
    End Sub

    Private Sub btnGetExportDatabases_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetExportDatabases.Click
        Dim txt As String = cmbExpDB.Text
        cmbExpDB.Items.Clear()
        cmbExpDB.Text = String.Empty

        Dim dt As DataTable = GetDatabasesList(cmbExpServ.Text, txtExpSerPort.Text, txtExpUname.Text, txtExpPass.Text)
        For Each dr As DataRow In dt.Rows
            cmbExpDB.Items.Add(dr(0))
        Next
        If cmbExpDB.Items.Contains(txt) Then
            cmbExpDB.Text = txt
        End If
    End Sub

    Private Sub txtExportServer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtExpPass.TextChanged, txtExpUname.TextChanged, cmbExpServ.TextChanged
        cmbExpDB.Text = String.Empty
        cmbExpDB.Items.Clear()
        cmbExpDB.Refresh()
    End Sub

    Private Sub btnDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefault.Click
        If cmbdbType.SelectedIndex = 0 Then
            txtDBUname.Text = "sa"
            cmbDBServer.Text = Environment.MachineName
            cmbDatabase.Text = "ZulAssetsBE"
            txtSqlPort.Text = "1433"
        Else
            cmbDBServer.Text = Environment.MachineName
            cmbDatabase.Text = "ZulAssetsBE"
        End If

    End Sub

End Class