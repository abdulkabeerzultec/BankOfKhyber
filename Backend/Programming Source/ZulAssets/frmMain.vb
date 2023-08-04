Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO
Imports System.Data.OleDb
Imports System.Diagnostics
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Design
Imports DevExpress.XtraReports.UserDesigner
Imports DevExpress.XtraReports.UserDesigner.Native
Imports System.ComponentModel.Design
Imports DevExpress.XtraReports.Native

Public Class frmMain
    Inherits System.Windows.Forms.Form
    Public Delegate Sub HideEvent()
    Public RoleID As Integer


    Dim objattRole As attRoles
    Dim objBALRole As New BALRoles

    Public EnableMenu As HideEvent = AddressOf funcEnableMenu
    Public thrLoading As Threading.Thread

    Private Sub funcEnableMenu()
        Me.MainMenuStrip = MenuStrip1
        Me.MenuStrip1.Visible = True
        Me.Invalidate()
    End Sub

    Private Sub frmMain_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            Application.Exit()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not AppConfig.IsDemoKey Then
            If AppConfig.ISRegistered Then
                If Not MessageBox.Show("Do you really want to exit ZulAssets?", " ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1).ToString().Equals("Yes") Then
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    Public Sub CreateDefaultRecords()
        Dim objBALDepreciationMethod As New BALDepreciationMethod
        objBALDepreciationMethod.CreateDefaultRecord()

        Dim objBALDisposalMethod As New BALDisposalMethod
        objBALDisposalMethod.CreateDefaultRecord()

        Dim objAstInv As New BALAst_INV_Schedule
        objAstInv.CreateDefaultRecord()

        Dim objRole As New BALRoles
        objRole.CreateDefaultRecord()

        Dim objBALUsers As New BALUsers
        objBALUsers.CreateDefaultRecord()

        Dim objBALBarCode_Struct As New BALBarCode_Struct
        objBALBarCode_Struct.CreateDefaultRecord()
    End Sub

    Private Sub InitValidationRules()
        valRulenotEmpty.ConditionOperator = ConditionOperator.IsNotBlank
        valRulenotEmpty.ErrorText = "Please enter a value"
        valRulenotEmpty.ErrorType = ErrorType.Critical

        valRuleGreaterThanZero.ConditionOperator = ConditionOperator.Greater
        valRuleGreaterThanZero.ErrorText = "Please enter a value greater than zero"
        valRuleGreaterThanZero.ErrorType = ErrorType.Critical
        valRuleGreaterThanZero.Value1 = 0

        valRuleGreaterOrEqualZero.ConditionOperator = ConditionOperator.GreaterOrEqual
        valRuleGreaterOrEqualZero.Value1 = 0
        valRuleGreaterOrEqualZero.ErrorText = "Please enter a value greater or equal zero"
        valRuleGreaterOrEqualZero.ErrorType = ErrorType.Critical

        valRuleNotContainMinus.ConditionOperator = ConditionOperator.NotContains
        valRuleNotContainMinus.Value1 = "-"
        valRuleNotContainMinus.ErrorText = "Please enter a positive value"
        valRuleNotContainMinus.ErrorType = ErrorType.Critical

    End Sub

    Private Sub RegisterEditionItems()
        'Kabeer Registration
        'If AppConfig.AppEdition = ApplicationEditions.NotRegistered Then
        '    Application.Exit()
        'End If

        Select Case AppConfig.AppEdition
            Case ApplicationEditions.Inventory
                mnuGLCode.Visible = False
                mnuDepreciationMethods.Visible = False
                mnuAssetBooks.Visible = False
                mnuDepPolicy.Visible = False
                mnuInterCompTrans.Visible = False
                mnuPurchOrder.Visible = False
                mnuDepEngine.Visible = False
                mnuRptDesigner.Visible = False
                mnuCreateReport.Visible = False
                ToolStripSeparator10.Visible = False
                ' To add more items from the menue
            Case ApplicationEditions.Tracking
                'mnuGLCode.Visible = False
                mnuAssetBooks.Visible = False
                mnuDepreciationMethods.Visible = False
                mnuDepPolicy.Visible = False
                mnuInterCompTrans.Visible = False
                mnuPurchOrder.Visible = False
                mnuDepEngine.Visible = False
                ToolStripSeparator10.Visible = False
                'mnuRptDesigner.Visible = False
                'mnuCreateReport.Visible = False
            Case ApplicationEditions.Financial
                mnuPurchOrder.Visible = False
            Case ApplicationEditions.Enterprise
                'don't hide anything
        End Select
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.DesktopScreen
        Me.BackgroundImageLayout = ImageLayout.Stretch
        MainForm = Me

        Try
            'Kabeer Registration
            'If Not AppConfig.ISRegistered Or AppConfig.AppEdition = ApplicationEditions.NotRegistered Then
            '    ZulMessageBox.ShowMe("InvalidRegisfound")
            '    Application.Exit()
            'End If

            InitValidationRules()

            RegisterEditionItems()
            ' read the current status of the specified keys
            UpdateKeys()


            Me.Text += " <" & AppConfig.LoginName & ">"
            If AppConfig.IsDemoKey Then
                Me.Text &= " <Evaluation Version>"
                'if asset details recrod count more than 10 then close the application
                Dim obj As New BALAssetDetails
                If obj.GetAssetsCount(New attAssetDetails) > DemoAssetsCount Then
                    ZulMessageBox.ShowMe("DemoAssetCount")
                    Application.Exit()
                End If
            End If

            'CreateExtendedReportMenu()
            CreateIntegrationMenu()


            'Load System Parameters.
            Dim objBALSysParam As New BALSysParam
            objBALSysParam.ReadSysParameters()

            If AppConfig.DbType = 1 Then 'SQL Server
                lblServerName.Text = AppConfig.DbServer.ToUpper
                lblDBName.Text = AppConfig.DbName.ToUpper
            Else 'Oracle
                lblServerName.Text = "ORACLE"
                lblDBName.Text = AppConfig.DbServer.ToUpper
            End If

            SET_Role_menu(RoleID)


            CrVer_Folders()

            CreateDefaultRecords()

            Dim ds As DataTable
            Dim objBALCompanyInfo As New BALCompanyInfo

            ds = objBALCompanyInfo.GetAll_CompanyInfo(New attCompanyInfo)
            If Not (ds.Rows.Count > 0) Then
                Dim frm As New frmCompany
                frm.Mode = "2"
                frm.WindowState = FormWindowState.Normal
                frm.StartPosition = FormStartPosition.CenterScreen
                frm.ShowDialog()
                frm.BringToFront()
            Else
                AppConfig.CompanyName = ds.Rows(0)("Name")
                Try
                    If ds.Rows(0)("Image").ToString <> "" Then
                        Dim bits As Byte() = CType(ds.Rows(0)("Image"), Byte())
                        Dim ms As New MemoryStream(bits, 0, bits.Length)
                        CompanyLogoImage = Image.FromStream(ms, True)
                    End If
                Catch ex As Exception

                End Try
            End If

            'Register Available Plugins in the applicationdir\Addins.
            Dim str As String = AddInsManager.RegisterAvailablePlugins()
            If Not String.IsNullOrEmpty(str) Then

                ZulMessageBox.ShowErrorMessage(str)
            End If

            If AppConfig.ShowAlarmOnStartup And mnuWarrantyAlarm.Enabled Then
                mnuWarrantyAlarm_Click(Nothing, Nothing)
            End If

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = True
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
#Region "ABB Menu Items"
    Public Sub mnuAnonymousRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        showLoading()
        Dim frm As New ABBIntegration.frmAnonymousRemarks
        frm.MdiParent = Me
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.Show()
        frm.BringToFront()
        hideLoading()
    End Sub

    Public Sub ShowForm(ByVal ctl As Control)
        showLoading()
        Dim PopupForm As New frmPopup
        PopupForm.Controls.Add(ctl)
        PopupForm.Size = New System.Drawing.Size(ctl.Width + 10, ctl.Height)
        ctl.Dock = System.Windows.Forms.DockStyle.Fill
        PopupForm.BringToFront()
        PopupForm.MdiParent = Me
        PopupForm.StartPosition = FormStartPosition.CenterScreen
        PopupForm.Show()
        hideLoading()
    End Sub

    Public Sub mnuWarrantyReceiveReplace_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ctl As New ABBIntegration.ctlWarrantyReceiving
        ctl.ReciveType = ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.WarrantyReceivingReplace
        ShowForm(ctl)
    End Sub

    Public Sub mnuWarrantyReceiveSame_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ctl As New ABBIntegration.ctlWarrantyReceiving
        ctl.ReciveType = ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.WarrantyReceivingSame
        ShowForm(ctl)
    End Sub

    Public Sub mnuWarrantyClaim_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ShowForm(New ABBIntegration.ctlWarrantyClaim)
    End Sub

    Public Sub mnuWarrantyStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ShowForm(New ABBIntegration.ctlWarrantyStatus)
    End Sub

    Public Sub mnuReversal_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ShowForm(New ABBIntegration.ctlReversalDoc)
    End Sub

    Public Sub mnuVendorReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ShowForm(New ABBIntegration.ctlVendorReturn)
    End Sub

    Public Sub mnuMatCofnig_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ShowForm(New ABBIntegration.ctlSAPItems)
    End Sub

    Public Sub mnuGoodsReceving_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ShowForm(New ABBIntegration.ctlGoodsReceiving)
    End Sub

    Public Sub mnuGoodsIssuingPur_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ctl As New ABBIntegration.ctlGoodsIssue
        ctl.IssueType = ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.GIPOR
        ShowForm(ctl)
    End Sub

    Public Sub mnuGoodsIssuingInv_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ctl As New ABBIntegration.ctlGoodsIssue
        ctl.IssueType = ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.GINV
        ShowForm(ctl)
    End Sub

#End Region

    Private Sub CreateIntegrationMenu()
        Select Case IntegrationName
            Case IntegrationType.RetajIntegration
                mnuImportFile.Visible = True
                mnuImportFile.Text = "&Import File"
                mnuExportFile.Visible = False
                HideTreeCodes = False
                IntegrationName = IntegrationType.RetajIntegration
            Case IntegrationType.ABBIntegration
                IntegrationName = IntegrationType.ABBIntegration
                mnuImportFile.Visible = True
                mnuImportFile.Text = "&Import/Export File"
                mnuExportFile.Visible = False
                mnuBrands.Visible = False
                mnuInsurers.Visible = False
                mnuAssetItems.Visible = False
                mnuSuppliers.Visible = False
                mnuGLCode.Visible = False
                mnuSearch.Visible = False
                mnuBarcodeStruct.Visible = False
                mnuOfflineMachines.Visible = False
                ToolStripSeparator10.Visible = False
                mnuAssetCoding.Visible = False
                ToolStripSeparator5.Visible = False
                mnuBarcodePolicy.Visible = False
                mnuLocCustTrans.Visible = False
                ToolStripSeparator2.Visible = False
                mnuSep7.Visible = False
                mnuDisposalMethods.Visible = False
                mnuSep1.Visible = False
                mnuSep2.Visible = False
                mnuSep3.Visible = False
                mnuUnits.Visible = False
                mnuCreateReport.Visible = False
                mnuCustodians.Text = "Employees"
                HideTreeCodes = False

                Dim ds As DataTable
                Dim objattRole As attRoles = New attRoles
                Dim objBALRole As New BALRoles
                objattRole.PKeyCode = RoleID
                ds = objBALRole.GetAll_Roles(objattRole)
                Dim ShowInventoryReports As Boolean = ds.Rows(0)("Units")
                Dim ShowAuditingReports As Boolean = ds.Rows(0)("Insurer")
                Dim ShowAnnoymousRemarks As Boolean = ds.Rows(0)("Supplier")
                If Not ShowAuditingReports And Not ShowInventoryReports Then
                    mnuReportCenter.Enabled = False
                End If

                'We are storing the AnonymousRemarks Permission in Supplier Permission field.
                'chkAnonymousRemarks.Checked = ds.Rows(0)("Supplier")
                If ShowAnnoymousRemarks Then
                    Dim itm As ToolStripMenuItem = mnuCommunication.DropDownItems.Add("Anonymous Remarks", Nothing, New EventHandler(AddressOf Me.mnuAnonymousRemarks_Click))
                End If

                Dim itmInventory As New ToolStripMenuItem("Inventory")
                MenuStrip1.Items.Insert(5, itmInventory)
                itmInventory.DropDownItems.Add("Material Configuration", Nothing, AddressOf Me.mnuMatCofnig_Click)
                itmInventory.DropDownItems.Add("Goods Receiving", Nothing, AddressOf Me.mnuGoodsReceving_Click)
                itmInventory.DropDownItems.Add("Goods Issuance Purchase", Nothing, AddressOf mnuGoodsIssuingPur_Click)
                itmInventory.DropDownItems.Add("Goods Issuance Inv.Pro", Nothing, AddressOf mnuGoodsIssuingInv_Click)

                itmInventory.DropDownItems.Add("Reversal GI/GR", Nothing, AddressOf mnuReversal_Click)
                itmInventory.DropDownItems.Add("Vendor Return", Nothing, AddressOf mnuVendorReturn_Click)

                itmInventory.DropDownItems.Add("Warranty Status", Nothing, AddressOf mnuWarrantyStatus_Click)

                itmInventory.DropDownItems.Add("Warranty Claim", Nothing, AddressOf mnuWarrantyClaim_Click)
                itmInventory.DropDownItems.Add("Warranty Receiving", Nothing, AddressOf mnuWarrantyReceiveSame_Click)
                itmInventory.DropDownItems.Add("Replacement Receiving", Nothing, AddressOf mnuWarrantyReceiveReplace_Click)

                'TODO: Init the Datalayer connection string.
                ABBIntegration.AppConfigData.DBServerName = AppConfig.ComServer
                ABBIntegration.AppConfigData.DbName = AppConfig.ComName
                ABBIntegration.AppConfigData.DbUserName = AppConfig.ComUname
                ABBIntegration.AppConfigData.DbUserPass = AppConfig.ComPass
                ABBIntegration.AppConfigData.DbSQLPort = AppConfig.DBSQLPort
                ABBIntegration.AppConfigData.CompanyFileName = AppConfig.CompanyName

                Dim connStr As String = ABBIntegration.AppConfigData.GetConnectionString()
                If Not GenericDAL.ConnectionString.ConnectToDb() Then
                    ZulMessageBox.ShowErrorMessage(GenericDAL.ConnectionString.ErrorMessage)
                Else
                    'Put the connection string in the data layer.
                    Dim objDBCon As New ZulAssetsDAL.ZulAssetsDAL.DBConnection
                    'TODO: Check this code.
                    ZulAssetsDAL.ConnectionString.ChangeDefaultConStr(objDBCon.GetConnectionString)
                    ZulAssetsDAL.ConnectionString.ChangeDefaultTempConStr(objDBCon.GetConnectionString_Temp)
                End If

                ABBIntegration.InitVariables()

            Case IntegrationType.AlhadaIntegration
                mnuImportFile.Visible = False
                mnuExportFile.Visible = False
                ToolStripSeparator8.Visible = False
                HideTreeCodes = False
                IntegrationName = IntegrationType.AlhadaIntegration
            Case IntegrationType.CMAIntegration
                mnuImportFile.Visible = True
                mnuExportFile.Visible = True
                mnuLocCustTrans.Visible = False
                mnuInterCompTrans.Visible = False
                mnuOrgHier.Visible = False
                mnuOrgGroups.Visible = False
                mnuOrgLevels.Visible = False
                mnuCustodians.Visible = False
                mnuDesgination.Visible = False
                mnuAddressTemplates.Visible = False
                mnuBrands.Visible = False
                mnuInsurers.Visible = False
                mnuSuppliers.Visible = False
                mnuGLCode.Visible = False
                mnuDepreciationMethods.Visible = False
                mnuUnits.Visible = False
                mnuAssetItems.Visible = False
                mnuCompanies.Visible = False
                mnuOfflineMachines.Visible = False
                mnuAssetBooks.Visible = False
                mnuDepPolicy.Visible = False
                mnuPurchOrder.Visible = False
                mnuDepEngine.Visible = False
                mnuSep1.Visible = False
                mnuSep2.Visible = False
                mnuSep3.Visible = False
                mnuSep4.Visible = False
                mnuSep5.Visible = False
                mnuSep7.Visible = False
                HideTreeCodes = True
                IntegrationName = IntegrationType.CMAIntegration
            Case IntegrationType.FairMontIntegration
                mnuExportFile.Visible = False
                IntegrationName = IntegrationType.FairMontIntegration
            Case IntegrationType.NahdiIntegration
                mnuImportFile.Visible = True
                mnuImportFile.Text = "&Import/Export File"
                mnuExportFile.Visible = False
                IntegrationName = IntegrationType.NahdiIntegration
            Case IntegrationType.KPMGIntegration
                mnuImportFile.Visible = False
                mnuExportFile.Visible = False
                ToolStripSeparator8.Visible = False
                HideTreeCodes = False
                IntegrationName = IntegrationType.KPMGIntegration
            Case IntegrationType.None
                mnuImportFile.Visible = False
                mnuExportFile.Visible = False
                ToolStripSeparator8.Visible = False
                HideTreeCodes = False
                IntegrationName = IntegrationType.None
        End Select
    End Sub


    Public Sub OpenForm(ByVal frm1 As Form, ByVal ReportName As String)
        'Dim FirstOpen As Boolean = False
        showLoading()
        Try
            '  If FormController.objfrmRptFilter Is Nothing Then
            FormController.objfrmRptFilter = New frmReportCenter
            'FirstOpen = True
            'End If
            'FormController.objfrmRptFilter.ReportName = ReportName
            FormController.objfrmRptFilter.MdiParent = Me
            FormController.objfrmRptFilter.WindowState = FormWindowState.Normal
            FormController.objfrmRptFilter.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmRptFilter.Show()
            FormController.objfrmRptFilter.BringToFront()
            MakeAllFormsNormalState()
            'If FirstOpen Then
            'End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally
            hideLoading()
        End Try
    End Sub

    Public Sub OpenForm(ByVal frm As Form)
        showLoading()
        Try
            frm.MdiParent = Me
            frm.WindowState = FormWindowState.Normal
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.Show()
            MakeAllFormsNormalState()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
        hideLoading()
    End Sub


    Public Sub OpenForm(ByVal dt As DataTable)
        showLoading()
        Try
            Dim frm As New frmPOTrans
            frm.FGRCollection = dt
            frm.MdiParent = Me
            frm.WindowState = FormWindowState.Normal
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.Show()
            frm.BringToFront()
            MakeAllFormsNormalState()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
        hideLoading()
    End Sub

    Private Sub CrVer_Folders()
        Try
            If Not Directory.Exists(AppConfig.AppDataFolder) Then
                Directory.CreateDirectory(AppConfig.AppDataFolder)
                Directory.CreateDirectory(AppConfig.AppDataFolder & "\Temp")
                Directory.CreateDirectory(AppConfig.AppDataFolder & "\FWatch")
            Else
                If Not Directory.Exists(AppConfig.AppDataFolder & "\Temp") Then
                    Directory.CreateDirectory(AppConfig.AppDataFolder & "\Temp")
                End If

                If Not Directory.Exists(AppConfig.AppDataFolder & "\FWatch") Then
                    Directory.CreateDirectory(AppConfig.AppDataFolder & "\FWatch")
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub mnuAstAdmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        showLoading()
        Try
            Dim frm1 As New frmDashboard
            frm1.MdiParent = Me
            frm1.WindowState = FormWindowState.Maximized
            frm1.Show()
            frm1.BringToFront()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
        hideLoading()
    End Sub

    Private Sub mnuSysConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        showLoading()
        Try
            Dim frm1 As New frmSysConfig
            frm1.MdiParent = Me
            frm1.WindowState = FormWindowState.Maximized
            frm1.Show()
            frm1.BringToFront()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
        hideLoading()
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLocations.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmLocation Is Nothing Then
                showLoading()
                FormController.objfrmLocation = New frmLocation
                FirstOpen = True
            End If
            FormController.objfrmLocation.MdiParent = Me
            'FormController.objfrmLocation.WindowState = FormWindowState.Maximized
            FormController.objfrmLocation.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmLocation.Show()
            FormController.objfrmLocation.BringToFront()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub BrandsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBrands.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmBrand Is Nothing Then
                showLoading()
                FormController.objfrmBrand = New frmBrand
                FirstOpen = True
            End If
            FormController.objfrmBrand.MdiParent = Me
            FormController.objfrmBrand.WindowState = FormWindowState.Normal
            FormController.objfrmBrand.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmBrand.Show()
            FormController.objfrmBrand.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub CustodianToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCustodians.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmCustodian Is Nothing Then
                showLoading()
                FormController.objfrmCustodian = New frmCustodian
                FirstOpen = True
            End If
            FormController.objfrmCustodian.MdiParent = Me
            FormController.objfrmCustodian.WindowState = FormWindowState.Maximized
            FormController.objfrmCustodian.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmCustodian.Show()
            FormController.objfrmCustodian.BringToFront()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub DesginationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDesgination.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmDesig Is Nothing Then
                showLoading()
                FormController.objfrmDesig = New frmDesignation
                FirstOpen = True

            End If
            FormController.objfrmDesig.MdiParent = Me
            FormController.objfrmDesig.WindowState = FormWindowState.Normal
            FormController.objfrmDesig.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmDesig.Show()
            FormController.objfrmDesig.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub InsurerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInsurers.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmInsurar Is Nothing Then
                showLoading()
                FormController.objfrmInsurar = New frmInsurar
                FirstOpen = True
            End If
            FormController.objfrmInsurar.MdiParent = Me
            FormController.objfrmInsurar.WindowState = FormWindowState.Normal
            FormController.objfrmInsurar.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmInsurar.Show()
            FormController.objfrmInsurar.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub DisposalMethosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDisposalMethods.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmDisp Is Nothing Then
                showLoading()
                FormController.objfrmDisp = New frmDisp
                FirstOpen = True
            End If
            FormController.objfrmDisp.MdiParent = Me
            FormController.objfrmDisp.WindowState = FormWindowState.Normal
            FormController.objfrmDisp.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmDisp.Show()
            FormController.objfrmDisp.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub SupplierToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSuppliers.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmSupplier Is Nothing Then
                showLoading()
                FormController.objfrmSupplier = New frmSupplier
                FirstOpen = True
            End If
            FormController.objfrmSupplier.MdiParent = Me
            FormController.objfrmSupplier.WindowState = FormWindowState.Normal
            FormController.objfrmSupplier.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmSupplier.Show()
            FormController.objfrmSupplier.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub DepreciationMethodsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDepreciationMethods.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmDep Is Nothing Then
                showLoading()
                FormController.objfrmDep = New frmDep
                FirstOpen = True
            End If
            FormController.objfrmDep.MdiParent = Me
            FormController.objfrmDep.WindowState = FormWindowState.Normal
            FormController.objfrmDep.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmDep.Show()
            FormController.objfrmDep.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub InventorySchedulesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInvSch.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmAstInvSch Is Nothing Then
                showLoading()
                FormController.objfrmAstInvSch = New frmAstInvSch
                FirstOpen = True
            End If
            FormController.objfrmAstInvSch.MdiParent = Me
            FormController.objfrmAstInvSch.WindowState = FormWindowState.Normal
            FormController.objfrmAstInvSch.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmAstInvSch.Show()
            FormController.objfrmAstInvSch.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub BookTempelatesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAssetBooks.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmBookTempl Is Nothing Then
                showLoading()
                FormController.objfrmBookTempl = New frmAstBooks
                FirstOpen = True
            End If
            FormController.objfrmBookTempl.MdiParent = Me
            FormController.objfrmBookTempl.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmBookTempl.WindowState = FormWindowState.Normal
            FormController.objfrmBookTempl.Show()
            FormController.objfrmBookTempl.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub AssetsToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.OpenForm(New frmItems)
    End Sub

    Private Sub AssetDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDetailsMainten.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmAssetsDetails Is Nothing Then
                showLoading()
                FirstOpen = True
            End If
            ShowAssetDetailsForm("")
            If FirstOpen Then
                hideLoading()
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub PurchaseOrdersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPOPrepar.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmPOMaster Is Nothing Then
                showLoading()
                FormController.objfrmPOMaster = New frmPOMaster
                FirstOpen = True
            End If
            FormController.objfrmPOMaster.MdiParent = Me
            FormController.objfrmPOMaster.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmPOMaster.WindowState = FormWindowState.Maximized
            FormController.objfrmPOMaster.Show()
            FormController.objfrmPOMaster.BringToFront()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeviceConfig.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmDevices Is Nothing Then
                showLoading()
                FormController.objfrmDevices = New frmDevices
                FirstOpen = True
            End If
            FormController.objfrmDevices.MdiParent = Me
            FormController.objfrmDevices.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmDevices.WindowState = FormWindowState.Normal

            FormController.objfrmDevices.Show()
            FormController.objfrmDevices.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub SystemConfigurationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSystemConfig.Click
        Try
            Dim frmconfig As New frmSysConfig
            frmconfig.StartPosition = FormStartPosition.CenterScreen
            frmconfig.ShowDialog()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub mnuReceive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReceive.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmReceive Is Nothing Then
                showLoading()
                FormController.objfrmReceive = New frmReceive
                FirstOpen = True
            End If
            FormController.objfrmReceive.MdiParent = Me
            FormController.objfrmReceive.WindowState = FormWindowState.Normal
            FormController.objfrmReceive.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmReceive.Show()
            FormController.objfrmReceive.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub mnuSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSend.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmSend Is Nothing OrElse FormController.objfrmSend.IsDisposed Then
                showLoading()
                ShowDataSendingForm()
                FirstOpen = True
            Else
                FormController.objfrmSend.MdiParent = Me
                FormController.objfrmSend.WindowState = FormWindowState.Maximized
                FormController.objfrmSend.StartPosition = FormStartPosition.CenterScreen
                FormController.objfrmSend.Show()
                FormController.objfrmSend.BringToFront()
            End If
            If FirstOpen Then
                hideLoading()
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub MakeAllFormsNormalState()
        For Each frm As Form In Me.MdiChildren
            frm.WindowState = FormWindowState.Normal
        Next
        Me.Refresh()
    End Sub
    Private Sub ApplicationUsersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuApplicationUsers.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmUser Is Nothing Then
                showLoading()
                FormController.objfrmUser = New frmUser
                FirstOpen = True
            End If
            FormController.objfrmUser.MdiParent = Me
            FormController.objfrmUser.WindowState = FormWindowState.Normal
            FormController.objfrmUser.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmUser.Show()
            FormController.objfrmUser.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub POApprovalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPOApprovals.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmPOApproval Is Nothing Then
                showLoading()
                FormController.objfrmPOApproval = New frmPOApproval
                FirstOpen = True
            End If
            FormController.objfrmPOApproval.MdiParent = Me
            FormController.objfrmPOApproval.WindowState = FormWindowState.Normal
            FormController.objfrmPOApproval.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmPOApproval.Show()
            FormController.objfrmPOApproval.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Try
            Application.Exit()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub AboutZulAssetsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAbout.Click
        Try
            Dim frm As New frmSplash
            frm.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            frm.MaximizeBox = False
            frm.MinimizeBox = False
            frm.Text = "About ZulAssets"
            frm.ShowDialog()
            frm.Dispose()

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub TransferPOItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmApproved Is Nothing Then
                showLoading()
                FormController.objfrmApproved = New frmPOApproved
                FirstOpen = True
            End If
            FormController.objfrmApproved.MdiParent = Me

            FormController.objfrmApproved.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmApproved.Show()
            FormController.objfrmApproved.BringToFront()
            If FirstOpen Then
                hideLoading()
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub POTransferToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAssetsInTransit.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmApproved Is Nothing Then
                showLoading()
                FormController.objfrmApproved = New frmPOApproved
                FirstOpen = True
            End If
            FormController.objfrmApproved.MdiParent = Me

            FormController.objfrmApproved.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmApproved.Show()
            FormController.objfrmApproved.BringToFront()
            If FirstOpen Then
                hideLoading()
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub AddressBookToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAddressTemplates.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objAddress Is Nothing Then
                showLoading()
                FormController.objAddress = New frmAddress
                FirstOpen = True
            End If
            FormController.objAddress.MdiParent = Me
            FormController.objAddress.WindowState = FormWindowState.Normal
            FormController.objAddress.StartPosition = FormStartPosition.CenterScreen
            FormController.objAddress.Show()
            FormController.objAddress.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub MenuStrip2_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)
        Me.OpenForm(New frmRoles)
    End Sub

    Private Sub UserRolesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUserRoles.Click

        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmAssetsDetails Is Nothing Then
                showLoading()
                FirstOpen = True
            End If
            ShowDataRolesForm()
            If FirstOpen Then
                hideLoading()
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try



        'Dim FirstOpen As Boolean = False
        'Try
        '    If FormController.objfrmRoles Is Nothing Then
        '        showLoading()
        '        FormController.objfrmRoles = New frmRoles
        '        FirstOpen = True
        '    End If
        '    FormController.objfrmRoles.MdiParent = Me
        '    FormController.objfrmRoles.WindowState = FormWindowState.Maximized
        '    FormController.objfrmRoles.StartPosition = FormStartPosition.CenterScreen
        '    FormController.objfrmRoles.Show()
        '    FormController.objfrmRoles.BringToFront()
        '    If FirstOpen Then
        '        hideLoading()
        '    End If
        'Catch ex As Exception
        '    GenericExceptionHandler(ex, WhoCalledMe)
        'End Try
    End Sub

    Private Sub AssetCategoriesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAssetCat.Click
        Dim FirstOpen As Boolean = False

        Try
            If FormController.objfrmCat Is Nothing Then
                showLoading()
                FormController.objfrmCat = New frmCategory
                FirstOpen = True
            End If
            FormController.objfrmCat.MdiParent = Me
            FormController.objfrmCat.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmCat.FormCaption = mnuAssetCat.Text
            FormController.objfrmCat.Show()
            FormController.objfrmCat.BringToFront()

            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub AssetTransferToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLocCustTrans.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmAssetTransfer Is Nothing Then
                showLoading()
                FormController.objfrmAssetTransfer = New frmAssetTransfer
                FirstOpen = True
            End If
            FormController.objfrmAssetTransfer.MdiParent = Me
            FormController.objfrmAssetTransfer.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmAssetTransfer.Show()
            FormController.objfrmAssetTransfer.BringToFront()

            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub AssToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSearch.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmAstSearch Is Nothing Then
                showLoading()
                FormController.objfrmAstSearch = New frmAstSearch
                FirstOpen = True
            End If
            FormController.objfrmAstSearch.MdiParent = Me
            FormController.objfrmAstSearch.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmAstSearch.Show()
            FormController.objfrmAstSearch.BringToFront()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub AssetItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAssetItems.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmItems Is Nothing Then
                showLoading()
                FormController.objfrmItems = New frmItems
                FirstOpen = True
            End If
            FormController.objfrmItems.MdiParent = Me

            FormController.objfrmItems.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmItems.Show()
            FormController.objfrmItems.BringToFront()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub CompanyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.OpenForm(New frmCompanies)
    End Sub

    Private Sub DepreciationPolicyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDepPolicy.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmDepPolicy Is Nothing Then
                showLoading()
                FormController.objfrmDepPolicy = New frmDepPolicy
                FirstOpen = True
            End If
            FormController.objfrmDepPolicy.MdiParent = Me

            FormController.objfrmDepPolicy.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmDepPolicy.Show()
            FormController.objfrmDepPolicy.BringToFront()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub AssetInterCompanyTransferToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInterCompTrans.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmCompanyTrans Is Nothing Then
                showLoading()
                FormController.objfrmCompanyTrans = New frmCompanyTrans
                FirstOpen = True
            End If
            FormController.objfrmCompanyTrans.MdiParent = Me

            FormController.objfrmCompanyTrans.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmCompanyTrans.Show()
            FormController.objfrmCompanyTrans.BringToFront()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub ASSETToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAdmin.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmDashboard Is Nothing Then
                showLoading()
                FormController.objfrmDashboard = New frmDashboard
                FirstOpen = True
            End If
            FormController.objfrmDashboard.MdiParent = Me
            FormController.objfrmDashboard.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmDashboard.WindowState = FormWindowState.Maximized
            FormController.objfrmDashboard.Show()
            FormController.objfrmDashboard.BringToFront()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub




    Private Sub AssetDetailsToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        showLoading()

        Dim frm As New frmAstSearch
        frm.MdiParent = Me
        frm.isReport = 1
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.Show()
        Me.Refresh()
        MakeAllFormsNormalState()
        hideLoading()
    End Sub

    Private Sub AssetDetailsToolStripMenuItem1_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs)
        showLoading()
        Dim frm As New frmAstSearch
        frm.MdiParent = Me
        frm.isReport = 1
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.Show()
        MakeAllFormsNormalState()
        hideLoading()
    End Sub
    Private Sub SisterCompaniesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCompanies.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmCompanies Is Nothing Then
                showLoading()
                FormController.objfrmCompanies = New frmCompanies
                FirstOpen = True
            End If
            FormController.objfrmCompanies.MdiParent = Me
            FormController.objfrmCompanies.WindowState = FormWindowState.Normal
            FormController.objfrmCompanies.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmCompanies.Show()
            FormController.objfrmCompanies.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub CompanyInfoToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCompanyInfo.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmCompany Is Nothing Then
                showLoading()
                FormController.objfrmCompany = New frmCompany
                FirstOpen = True
            End If
            FormController.objfrmCompany.MdiParent = Me
            FormController.objfrmCompany.WindowState = FormWindowState.Normal
            FormController.objfrmCompany.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmCompany.Show()
            FormController.objfrmCompany.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub SET_Role_menu(ByVal RoleID As Integer)
        Dim ds As DataTable
        objattRole = New attRoles
        objattRole.PKeyCode = RoleID

        ds = objBALRole.GetAll_Roles(objattRole)

        mnuApplicationUsers.Enabled = ds.Rows(0)("AppUser")
        mnuCompanyInfo.Enabled = ds.Rows(0)("CompanyInfo")
        mnuLocations.Enabled = ds.Rows(0)("Location")
        mnuDepPolicy.Enabled = ds.Rows(0)("DepPolicy")
        mnuBrands.Enabled = ds.Rows(0)("Brands")
        mnuDesgination.Enabled = ds.Rows(0)("Designation")
        mnuCustodians.Enabled = ds.Rows(0)("Custodian")
        mnuInsurers.Enabled = ds.Rows(0)("Insurer")
        mnuDisposalMethods.Enabled = ds.Rows(0)("DisposalMethod")
        mnuSuppliers.Enabled = ds.Rows(0)("Supplier")
        mnuDepreciationMethods.Enabled = ds.Rows(0)("DepreciationMethod")
        mnuInvSch.Enabled = ds.Rows(0)("InvSch")
        mnuAssetBooks.Enabled = ds.Rows(0)("AssetsBooks")
        mnuAddressTemplates.Enabled = ds.Rows(0)("AddressBook")
        mnuAssetCoding.Enabled = ds.Rows(0)("AssetCoding")
        mnuAssetCat.Enabled = ds.Rows(0)("AssetsCat")
        mnuAssetItems.Enabled = ds.Rows(0)("AssetItems")
        mnuCompanies.Enabled = ds.Rows(0)("Company")
        mnuPOPrepar.Enabled = ds.Rows(0)("PO")
        mnuPOApprovals.Enabled = ds.Rows(0)("POApproval")
        mnuAssetsInTransit.Enabled = ds.Rows(0)("POTrans")
        mnuDeviceConfig.Enabled = ds.Rows(0)("DeviceConfig")
        mnuSystemConfig.Enabled = ds.Rows(0)("SysConfig")
        mnuSend.Enabled = ds.Rows(0)("DataSend")
        mnuReceive.Enabled = ds.Rows(0)("DataRecieve")
        mnuAdmin.Enabled = ds.Rows(0)("AstAdmin")
        mnuDetailsMainten.Enabled = ds.Rows(0)("AstDetail")
        mnuLocCustTrans.Enabled = ds.Rows(0)("AstTranS")
        mnuSearch.Enabled = ds.Rows(0)("AstSrc")
        mnuInterCompTrans.Enabled = ds.Rows(0)("InterComTrans")
        'mnuAnonAssets.Enabled = ds.Rows(0)("Anonym")
        mnuUserRoles.Enabled = ds.Rows(0)("Roles")
        mnuUnits.Enabled = ds.Rows(0)("Units")
        mnuDepEngine.Enabled = ds.Rows(0)("DepMan")
        mnuBarcodeStruct.Enabled = ds.Rows(0)("BarStuct")
        mnuBarcodePolicy.Enabled = ds.Rows(0)("BarCodePolicy")
        mnuOrgLevels.Enabled = ds.Rows(0)("CompLvl")
        mnuOrgGroups.Enabled = ds.Rows(0)("CompGroup")
        mnuOrgHier.Enabled = ds.Rows(0)("GroupHier")
        mnuGLCode.Enabled = ds.Rows(0)("GLCode")
        mnuRptDesigner.Enabled = ds.Rows(0)("ReportDesigner")
        mnuCreateReport.Enabled = ds.Rows(0)("CreateReport")

        mnuExportFile.Enabled = ds.Rows(0)("CMAExport")
        mnuImportFile.Enabled = ds.Rows(0)("CMAImport")
        mnuDataProcess.Enabled = ds.Rows(0)("DataProcessing")
        mnuCostCenter.Enabled = ds.Rows(0)("CostCenter")

        mnuOfflineMachines.Enabled = ds.Rows(0)("OfflineMachine")
        mnuBackendInventory.Enabled = ds.Rows(0)("BackendInventory")

        mnuWarrantyAlarm.Enabled = ds.Rows(0)("Custom1").ToString
        If RoleID = 1 Then
            mnuOfflineMachines.Enabled = True
            'incase Role is admin then we are setting the companyIDs to be 0 so related user will have access to all companies.
            AppConfig.CompanyIDS = "0"
        Else
            mnuOfflineMachines.Enabled = False
            AppConfig.CompanyIDS = ds.Rows(0)("Companies")
        End If
    End Sub


    Private Sub DepreciationManagerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDepEngine.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmDepMan Is Nothing Then
                showLoading()
                FormController.objfrmDepMan = New frmDepMan
                FirstOpen = True
            End If
            FormController.objfrmDepMan.MdiParent = Me
            FormController.objfrmDepMan.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmDepMan.WindowState = FormWindowState.Normal

            FormController.objfrmDepMan.Show()
            FormController.objfrmDepMan.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub UnitsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUnits.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmUnits Is Nothing Then
                showLoading()
                FormController.objfrmUnits = New frmUnits
                FirstOpen = True
            End If
            FormController.objfrmUnits.MdiParent = Me
            FormController.objfrmUnits.WindowState = FormWindowState.Normal
            FormController.objfrmUnits.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmUnits.Show()
            FormController.objfrmUnits.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub ZulAssetsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUserGuide.Click
        System.Diagnostics.Process.Start(Application.StartupPath & "\ZulAssets User Guide.pdf")
    End Sub

    Private Sub BarCodeStructureToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBarcodeStruct.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmBarStuct Is Nothing Then
                showLoading()
                FormController.objfrmBarStuct = New frmBarStuct
                FirstOpen = True
            End If
            FormController.objfrmBarStuct.MdiParent = Me
            FormController.objfrmBarStuct.WindowState = FormWindowState.Normal
            FormController.objfrmBarStuct.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmBarStuct.Show()
            FormController.objfrmBarStuct.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub BarCodePolicyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBarcodePolicy.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmBarcodePolicy Is Nothing Then
                showLoading()
                FormController.objfrmBarcodePolicy = New frmBarcodePolicy
                FirstOpen = True
            End If
            FormController.objfrmBarcodePolicy.MdiParent = Me
            FormController.objfrmBarcodePolicy.WindowState = FormWindowState.Normal
            FormController.objfrmBarcodePolicy.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmBarcodePolicy.Show()
            FormController.objfrmBarcodePolicy.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub AssetsCodingDefinitionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAssetCoding.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmAssetCodingDef Is Nothing Then
                showLoading()
                FormController.objfrmAssetCodingDef = New frmAssetCodingDef
                FirstOpen = True
            End If
            FormController.objfrmAssetCodingDef.MdiParent = Me
            FormController.objfrmAssetCodingDef.WindowState = FormWindowState.Normal
            FormController.objfrmAssetCodingDef.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmAssetCodingDef.Show()
            FormController.objfrmAssetCodingDef.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub CompanyLevelsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOrgLevels.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objFrmLevels Is Nothing Then
                showLoading()
                FormController.objFrmLevels = New frmLevels
                FirstOpen = True
            End If
            FormController.objFrmLevels.MdiParent = Me
            FormController.objFrmLevels.WindowState = FormWindowState.Normal
            FormController.objFrmLevels.StartPosition = FormStartPosition.CenterScreen
            FormController.objFrmLevels.Show()
            FormController.objFrmLevels.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub OrganizationGroupsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOrgGroups.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmOrgGroups Is Nothing Then
                showLoading()
                FormController.objfrmOrgGroups = New frmOrgGroups
                FirstOpen = True
            End If
            FormController.objfrmOrgGroups.MdiParent = Me
            FormController.objfrmOrgGroups.WindowState = FormWindowState.Normal
            FormController.objfrmOrgGroups.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmOrgGroups.Show()
            FormController.objfrmOrgGroups.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub OrganizationalHierarchyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOrgHier.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmOrgHier Is Nothing Then
                showLoading()
                FormController.objfrmOrgHier = New frmOrgHier
                FirstOpen = True
            End If
            FormController.objfrmOrgHier.MdiParent = Me
            FormController.objfrmOrgHier.WindowState = FormWindowState.Maximized
            FormController.objfrmOrgHier.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmOrgHier.Show()
            FormController.objfrmOrgHier.BringToFront()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Dim frmLoad As frmLoading

    Friend Sub showLoading()
        Try
            thrLoading = New Threading.Thread(AddressOf LoadingThreadMethod)
            frmLoad = New frmLoading
            thrLoading.Start()
            Application.DoEvents()
        Catch
        End Try
    End Sub

    Friend Sub hideLoading()
        Try
            If frmLoad IsNot Nothing Then
                frmLoad.CloseLoadingForm = True
            End If
            'thrLoading.Abort()
            'thrLoading = Nothing
        Catch
        End Try
    End Sub

    Public Sub LoadingThreadMethod()
        Try
            'Me.Opacity = 0.5
            'Me.Refresh()
            frmLoad.Timer1.Enabled = True
            frmLoad.ShowDialog()
        Catch
        End Try
    End Sub

    Private Sub mnuGLCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGLCode.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmGLCode Is Nothing Then
                showLoading()
                FormController.objfrmGLCode = New frmGLCodes
                FirstOpen = True
            End If
            FormController.objfrmGLCode.MdiParent = Me

            FormController.objfrmGLCode.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmGLCode.WindowState = FormWindowState.Normal
            FormController.objfrmGLCode.Show()
            FormController.objfrmGLCode.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub mnuCloseAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCloseALL.Click
        For Each frm As Form In Me.MdiChildren
            frm.Close()
        Next
    End Sub

    Private Sub frmMain_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyData = Keys.Insert Then
            UpdateInsert()
        ElseIf e.KeyData = Keys.NumLock Then
            UpdateNUMLock()
        ElseIf e.KeyData = Keys.CapsLock Then
            UpdateCAPSLock()
        End If
    End Sub

    Private Sub lblINS_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lblINS.DoubleClick
        PressKeyboardButton(Keys.Insert)
        UpdateInsert()
    End Sub

    Private Sub lblNUM_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lblNUM.DoubleClick
        PressKeyboardButton(Keys.NumLock)
        UpdateNUMLock()
    End Sub

    Private Sub lblCAPS_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lblCAPS.DoubleClick
        PressKeyboardButton(Keys.CapsLock)
        UpdateCAPSLock()
    End Sub

    Private Sub frmMain_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        UpdateKeys()
    End Sub

    Private Sub UpdateKeys()
        UpdateInsert()
        UpdateNUMLock()
        UpdateCAPSLock()
    End Sub

    ''' <summary>
    ''' Updates the Form according to the status of INSERT key 
    ''' </summary>
    Private Sub UpdateInsert()
        Dim NumLock As Boolean = (GetKeyState(CInt(Keys.Insert))) <> 0
        If NumLock Then
            lblINS.Text = "  INS  "
        Else
            lblINS.Text = "  OVR  "
        End If
    End Sub

    ''' <summary>
    ''' Updates the Form according to the status of NUM Lock key 
    ''' </summary>
    Private Sub UpdateNUMLock()
        Dim NumLock As Boolean = (GetKeyState(CInt(Keys.NumLock))) <> 0
        If NumLock Then
            lblNUM.ForeColor = Color.Black
        Else
            lblNUM.ForeColor = Color.Gray
        End If
    End Sub

    ''' <summary>
    ''' Updates the Form according to the status of CAPS Lock key 
    ''' </summary>
    Private Sub UpdateCAPSLock()
        Dim CapsLock As Boolean = (GetKeyState(CInt(Keys.CapsLock))) <> 0
        If CapsLock Then
            lblCAPS.ForeColor = Color.Black
        Else
            lblCAPS.ForeColor = Color.Gray
        End If
    End Sub

    ''' <summary>
    ''' Simulate the Key Press Event
    ''' </summary>
    ''' <param name="keyCode">The code of the Key to be simulated</param>
    Private Sub PressKeyboardButton(ByVal keyCode As Keys)
        Const KEYEVENTF_EXTENDEDKEY As Integer = &H1
        Const KEYEVENTF_KEYUP As Integer = &H2
        keybd_event(CByte(keyCode), &H45, KEYEVENTF_EXTENDEDKEY, 0)
        keybd_event(CByte(keyCode), &H45, KEYEVENTF_EXTENDEDKEY Or KEYEVENTF_KEYUP, 0)
    End Sub

    Private Sub CascadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCascad.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub mnuTileVertical_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTileVertical.Click
        Me.LayoutMdi(MdiLayout.TileVertical)

    End Sub

    Private Sub mnuTileHorizontal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTileHorizontal.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub mnuReportDesign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRptDesigner.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmRptDesigner Is Nothing Then
                showLoading()
                FormController.objfrmRptDesigner = New frmReportDesigner
                FirstOpen = True
            End If
            FormController.objfrmRptDesigner.MdiParent = Me
            FormController.objfrmRptDesigner.WindowState = FormWindowState.Normal
            FormController.objfrmRptDesigner.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmRptDesigner.Show()
            FormController.objfrmRptDesigner.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub mnuCreateReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCreateReport.Click
        Dim frm As New frmReportWizard
        frm.IsNewReport = True
        frm.CreateNewReport()
        frm.ShowDialog()
        frm.Dispose()
    End Sub


    Private Sub mnuOfflineMachines_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOfflineMachines.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmOfflineMachines Is Nothing Then
                showLoading()
                FormController.objfrmOfflineMachines = New frmOfflineMachines
                FirstOpen = True
            End If
            FormController.objfrmOfflineMachines.MdiParent = Me
            FormController.objfrmOfflineMachines.WindowState = FormWindowState.Normal
            FormController.objfrmOfflineMachines.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmOfflineMachines.Show()
            FormController.objfrmOfflineMachines.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub mnuDataProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDataProcess.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmDataProcessing Is Nothing OrElse FormController.objfrmDataProcessing.IsDisposed Then
                showLoading()
                ShowDataProcessingForm()
                FirstOpen = True
            Else
                FormController.objfrmDataProcessing.MdiParent = Me
                FormController.objfrmDataProcessing.WindowState = FormWindowState.Normal
                FormController.objfrmDataProcessing.StartPosition = FormStartPosition.CenterScreen
                FormController.objfrmDataProcessing.Show()
                FormController.objfrmDataProcessing.BringToFront()
                MakeAllFormsNormalState()
            End If
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub mnuImportFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImportFile.Click
        Select Case IntegrationName
            Case IntegrationType.CMAIntegration
                Dim frm As New CMAIntegration.frmImport
                frm.ShowDialog()
                frm.Dispose()
            Case IntegrationType.ABBIntegration
                Dim frm As New ABBIntegration.frmABBSettings
                frm.RoleID = RoleID
                If AppConfig.SysPass <> "" Then
                    frm.AdminPassword = psDecrypt(AppConfig.SysPass)
                Else
                    frm.AdminPassword = ""
                End If
                frm.ShowDialog()
                frm.Dispose()
            Case IntegrationType.FairMontIntegration
                ShowForm(New FairmontIntegration.ctlImportData)
            Case IntegrationType.NahdiIntegration
                Dim frm As New NMCIntegration.frmNMCSettings
                If AppConfig.SysPass <> "" Then
                    frm.AdminPassword = psDecrypt(AppConfig.SysPass)
                Else
                    frm.AdminPassword = ""
                End If
                frm.ShowDialog()
                frm.Dispose()
            Case IntegrationType.RetajIntegration
                ShowForm(New RetajIntegration.ctlImportData)
        End Select
    End Sub

    Private Sub mnuCMAExportFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExportFile.Click
         Select IntegrationName
            Case IntegrationType.CMAIntegration
                Dim frm As New CMAIntegration.frmExport
                frm.ShowDialog()
                frm.Dispose()
        End Select
    End Sub

    Private Sub mnuNewTags_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.OpenForm(frmReportCenter, 1)
    End Sub

    Private Sub mnuReportCenter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportCenter.Click
        showLoading()
        Try
            If IntegrationName = IntegrationType.ABBIntegration Then
                Dim frm As New ABBIntegration.frmABBReports
                frm.MdiParent = MainForm
                frm.WindowState = FormWindowState.Maximized
                frm.RoleID = RoleID
                frm.CompanyLogoImage = CompanyLogoImage
                frm.Show()
                frm.BringToFront()

            Else
                Dim frm As New frmReportCenter
                frm.MdiParent = MainForm
                frm.StartPosition = FormStartPosition.CenterScreen
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
                frm.BringToFront()
            End If

            'If IntegrationName = IntegrationType.ABBIntegration Then
            '    FormController.objfrmRptFilter = New ABBIntegration.frmReportCenter
            'Else
            '    FormController.objfrmRptFilter = New frmReportCenter
            'End If
            'FormController.objfrmRptFilter.MdiParent = Me
            'FormController.objfrmRptFilter.WindowState = FormWindowState.Maximized
            'FormController.objfrmRptFilter.StartPosition = FormStartPosition.CenterScreen
            'FormController.objfrmRptFilter.Show()
            'FormController.objfrmRptFilter.BringToFront()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally
            hideLoading()
        End Try
    End Sub

    Private Sub mnuCostCenter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCostCenter.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmCostCenter Is Nothing Then
                showLoading()
                FormController.objfrmCostCenter = New frmCostCenter
                FirstOpen = True
            End If
            FormController.objfrmCostCenter.MdiParent = Me

            FormController.objfrmCostCenter.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmCostCenter.WindowState = FormWindowState.Normal
            FormController.objfrmCostCenter.Show()
            FormController.objfrmCostCenter.BringToFront()
            MakeAllFormsNormalState()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub mnuBackendInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBackendInventory.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmBackendInventory Is Nothing Then
                showLoading()
                FormController.objfrmBackendInventory = New frmBackendInventory
                FirstOpen = True
            End If
            FormController.objfrmBackendInventory.MdiParent = Me
            FormController.objfrmBackendInventory.WindowState = FormWindowState.Maximized
            FormController.objfrmBackendInventory.Show()
            FormController.objfrmBackendInventory.BringToFront()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub mnuChangePassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuChangePassword.Click
        Dim frm As New frmChangePassword
        frm.SelectedUser = AppConfig.LoginName
        frm.ShowDialog()
        frm.Dispose()
    End Sub

    Private Sub mnuWarrantyAlarm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuWarrantyAlarm.Click
        Dim FirstOpen As Boolean = False
        Try
            If FormController.objfrmAstWarrantyAlarm Is Nothing Then
                showLoading()
                FormController.objfrmAstWarrantyAlarm = New frmAssetWarrantyAlarm
                FirstOpen = True
            End If
            FormController.objfrmAstWarrantyAlarm.MdiParent = Me
            FormController.objfrmAstWarrantyAlarm.StartPosition = FormStartPosition.CenterScreen
            FormController.objfrmAstWarrantyAlarm.Show()
            FormController.objfrmAstWarrantyAlarm.BringToFront()
            If FirstOpen Then
                hideLoading()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
End Class
