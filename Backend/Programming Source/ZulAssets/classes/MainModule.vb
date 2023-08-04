Imports System.IO
Imports ZultecScreenCapture
Imports System.Net.Mail
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Drawing.Printing
Imports System.Reflection
Imports System.Diagnostics
Imports System.Resources
Imports System.Runtime.InteropServices
Imports System.Globalization
Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.UserDesigner
Imports System.Data.OleDb
Imports System.Collections.Generic

Module MainModule
    Public Enum ApplicationEditions
        Inventory
        Tracking
        Financial
        Enterprise
        NotRegistered
    End Enum

    Public Enum IntegrationType
        None
        CMAIntegration
        AlhadaIntegration
        KPMGIntegration
        ABBIntegration
        FairMontIntegration
        NahdiIntegration
        RetajIntegration
        AbunayyanPlugin
        ElafPlugin
        GSFMOPlugin
        SABBPlugin
        SanofiPlugin
        CITCPlugin
        PGPlugin
    End Enum

    Private _HideTreeCodes As Boolean = False
    Private _IntegrationName As IntegrationType = IntegrationType.None

    Public MainForm As frmMain

    Public DemoAssetsCount As Integer = 10
    Public CompanyLogoImage As Image

    Public valRulenotEmpty As New ConditionValidationRule
    Public valRuleGreaterThanZero As New ConditionValidationRule
    Public valRuleGreaterOrEqualZero As New ConditionValidationRule
    Public valRuleNotContainMinus As New ConditionValidationRule

    Public NumberMask As String = "###,###,###,##0.00"
    Public NumberMaskType As DevExpress.XtraEditors.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric

    Public Class ReportNames
        'Master Reports
        Public Const Designations As String = "Designations"
        Public Const Custodians As String = "Custodians"
        Public Const Brands As String = "Brands"
        Public Const Insurers As String = "Insurers"
        Public Const Suppliers As String = "Suppliers"
        Public Const DisposalMethods As String = "Disposal Methods"
        Public Const DepreciationMethods As String = "Depreciation Methods"
        Public Const AssetItems As String = "Asset Items"
        Public Const AssetBooks As String = "Asset Books"
        Public Const InventorySchedules As String = "Inventory Schedules"
        Public Const AddressBook As String = "Address Book"
        'Standard reports
        Public Const NewTags As String = "New Tags"
        Public Const CompanyAssets As String = "Company Assets"
        Public Const AssetsLedger As String = "Assets Ledger"
        Public Const AssetsTagging As String = "Assets Tagging"
        Public Const ItemsInventory As String = "Items Inventory"
        Public Const DepreciationBooks As String = "Depreciation Books"
        Public Const AssetRegister As String = "Asset Register"
        Public Const ExpectedDepreciation As String = "Expected Depreciation"
        Public Const AssetDetails As String = "Asset Details"
        Public Const DisposedAssets As String = "Disposed Assets"
        Public Const AuditStatus As String = "Audit Status"
        Public Const AssetsRegister As String = "Assets Register"
        Public Const AssetsbyCategory As String = "Assets by Category"
        Public Const AssetsbySubCategory As String = "Assets by SubCategory"
        Public Const LocationBarcode As String = "Location Barcode"
        Public Const AssetsBarcode As String = "Assets Barcode"
        Public Const AssetsLog As String = "Assets Log"
        'Audit Reports
        Public Const MissingAssets As String = "Missing Assets"
        Public Const FoundAssets As String = "Found Assets"
        Public Const MisplacedAssets As String = "Misplaced Assets"
        Public Const TransferredAssets As String = "Transferred Assets"
        Public Const AllocatedAssets As String = "Allocated Assets"
        Public Const AnonymousAssets As String = "Anonymous Assets"
        Public Const AllAssets As String = "All Assets"
        Public Const CostCenterAudit As String = "Cost Center Audit"


        Public Const AssetIssuance As String = "Asset Issuance"

    End Class 'ReportNames

    Public Property IntegrationName() As IntegrationType
        Get
            Return _IntegrationName
        End Get
        Set(ByVal value As IntegrationType)
            _IntegrationName = value
        End Set
    End Property


    Public Property HideTreeCodes() As Boolean
        Get
            Return _HideTreeCodes
        End Get
        Set(ByVal value As Boolean)
            _HideTreeCodes = value
        End Set
    End Property

    Private _frmPluginDataProcess As Form

    Public Property frmPluginDataProcess() As Form
        Get
            Return _frmPluginDataProcess
        End Get
        Set(ByVal value As Form)
            _frmPluginDataProcess = value
        End Set
    End Property

    Public Function ShowDataRolesForm() As Form
        If IntegrationName = IntegrationType.ABBIntegration Then
            Dim frm As New ABBIntegration.frmABBRoles
            frm.MdiParent = MainForm
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.Show()
            frm.BringToFront()
            Return frm
        Else
            Dim frm As New frmRoles
            frm.MdiParent = MainForm
            frm.WindowState = FormWindowState.Maximized
            frm.Show()
            frm.BringToFront()
            Return frm
        End If
    End Function


    Public Function ShowDataProcessingForm() As Form
        If IntegrationName = IntegrationType.ABBIntegration Then
            Dim frm As New ABBIntegration.frmABBDataProcessing
            FormController.objfrmDataProcessing = frm
            frm.RoleID = MainForm.RoleID
            frm.MdiParent = MainForm
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.Show()
            frm.BringToFront()
            Return frm
        ElseIf IntegrationName = IntegrationType.AbunayyanPlugin Then
            FormController.objfrmDataProcessing = frmPluginDataProcess
            frmPluginDataProcess.MdiParent = MainForm
            frmPluginDataProcess.WindowState = FormWindowState.Normal
            frmPluginDataProcess.StartPosition = FormStartPosition.CenterScreen
            frmPluginDataProcess.Show()
            frmPluginDataProcess.BringToFront()
            Return frmPluginDataProcess
        Else
            Dim frm As New frmDataProcessing
            FormController.objfrmDataProcessing = frm
            frm.MdiParent = MainForm
            frm.WindowState = FormWindowState.Maximized
            frm.Show()
            frm.BringToFront()
            Return frm
        End If
    End Function
    Public Function ShowDataSendingForm() As Form
        If IntegrationName = IntegrationType.ABBIntegration Then
            Dim frm As New ABBIntegration.frmABBSend
            FormController.objfrmSend = frm
            frm.MdiParent = MainForm
            frm.WindowState = FormWindowState.Maximized
            frm.Show()
            frm.BringToFront()
            Return frm
        Else
            Dim frm As New frmSend
            FormController.objfrmSend = frm
            frm.MdiParent = MainForm
            frm.WindowState = FormWindowState.Maximized
            frm.Show()
            frm.BringToFront()
            Return frm
        End If
    End Function
    ' This function will check the integration(CMA,KPMG Companies) and show realted asset details
    Public Function ShowAssetDetailsForm(ByVal AstID As String) As Form
        If IntegrationName = IntegrationType.CMAIntegration Then
            Dim frm As New CMAIntegration.frmAssets
            frm.MdiParent = MainForm
            frm.WindowState = FormWindowState.Maximized
            frm.Show()
            frm.BringToFront()
            frm.LocateAsset(AstID)
            Return frm
        ElseIf IntegrationName = IntegrationType.ABBIntegration Then
            Dim frm As New ABBIntegration.frmAssetsDetails
            frm.MdiParent = MainForm
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.IsDemokey = AppConfig.IsDemoKey
            frm.RoleID = MainForm.RoleID
            frm.Show()
            frm.BringToFront()
            frm.LocateAsset(AstID)
            Return frm
        Else
            Dim frm As New frmAssetsDetails
            frm.MdiParent = MainForm
            frm.WindowState = FormWindowState.Maximized
            frm.Show()
            frm.BringToFront()
            frm.LocateAsset(AstID)
            Return frm
        End If
    End Function

    Public Function ShowAssetDetailsAnonymous(ByVal AnonymousId As String, ByVal Desc As String, ByVal LocID As String, ByVal CatID As String, ByVal Modle As String, ByVal serial As String, ByVal DeviceID As String, ByVal TransDate As DateTime, ByVal InvID As Integer) As Boolean
        If IntegrationName = IntegrationType.CMAIntegration Then
            Dim frm As New CMAIntegration.frmAssets
            frm.WindowState = FormWindowState.Maximized
            frm.ShowAnonymousInfo(AnonymousId, Desc, LocID, CatID, Modle, serial, DeviceID, TransDate, InvID)
            frm.ShowDialog()
            Dim AnonymousSaved As Boolean = frm.AnonymousSaved
            frm.Dispose()
            Return AnonymousSaved
        ElseIf IntegrationName = IntegrationType.ABBIntegration Then
            Dim frm As New ABBIntegration.frmAssetsDetails
            frm.WindowState = FormWindowState.Maximized
            frm.ShowAnonymousInfo(AnonymousId, Desc, LocID, CatID, Modle, serial, DeviceID, TransDate, InvID)
            frm.ShowDialog()
            Dim AnonymousSaved As Boolean = frm.AnonymousSaved
            frm.Dispose()
            Return AnonymousSaved
        Else
            Dim frm As New frmAssetsDetails
            'frm.MdiParent = MainForm
            frm.WindowState = FormWindowState.Maximized
            frm.ShowAnonymousInfo(AnonymousId, Desc, LocID, CatID, Modle, serial, DeviceID, TransDate, InvID)
            frm.ShowDialog()
            Dim AnonymousSaved As Boolean = frm.AnonymousSaved
            frm.Dispose()
            Return AnonymousSaved
        End If
    End Function

    Public Function Search_Printer(ByVal strPrinterName As String) As Boolean
        For Each strPrinter As String In PrinterSettings.InstalledPrinters
            If strPrinter = strPrinterName Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Sub ClearFrame(ByVal FrameName As Object)
        For Each ctl As Object In FrameName.Controls
            If TypeOf (ctl) Is TextBox Then
                ctl.Text = ""
            ElseIf TypeOf (ctl) Is DateTimePicker Then

                ctl.value = DateTime.Now
            ElseIf TypeOf (ctl) Is CheckBox Then

                ctl.Checked = 0
            ElseIf TypeOf (ctl) Is DevExpress.XtraEditors.TextEdit Then
                ctl.Text = ""
            ElseIf TypeOf (ctl) Is ComboBox Then

                ctl.SelectedIndex = 0
            ElseIf TypeOf (ctl) Is ZulLOV.ZulLOV Then

                ctl.SelectedIndex = -1
            ElseIf TypeOf (ctl) Is Label Then
                If ctl.Tag <> "" Then
                    ctl.Text = ""
                End If
            ElseIf TypeOf (ctl) Is iptb.IPTextBox Then
                ctl.Text = ""
            End If
            If ctl.TabIndex = 0 Then ctl.Focus()
        Next
    End Sub

    Public Sub ClearTab(ByVal TabControl As Object)
        For Each ctl As Object In TabControl.Controls
            If TypeOf (ctl) Is TextBox Then
                ctl.Text = ""
            ElseIf TypeOf (ctl) Is DateTimePicker Then
                ctl.value = DateTime.Now
            ElseIf TypeOf (ctl) Is CheckBox Then
                ctl.Checked = 0
            ElseIf TypeOf (ctl) Is Label Then
                If ctl.Tag <> "" Then
                    ctl.Text = ""
                End If
            End If
            If ctl.TabIndex = 0 Then ctl.Focus()
        Next
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
        'MainForm.hideLoading()
        Dim frm As New frmError
        frm.Ex = ex
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()

    End Sub
    Public Function Check_Auth(ByVal formName As String) As Boolean
        Try
            If formName = "frmAddress" Then
                Return MainForm.mnuAddressTemplates.Enabled
            ElseIf formName = "frmOrgHier" Then
                Return MainForm.mnuOrgHier.Enabled
            ElseIf formName = "frmDesig" Then
                Return MainForm.mnuDesgination.Enabled
            ElseIf formName = "frmCat" Then
                Return MainForm.mnuAssetCat.Enabled
            ElseIf formName = "frmAssets" Then
                Return MainForm.mnuAssetItems.Enabled
            ElseIf formName = "frmCustodian" Then
                Return MainForm.mnuCustodians.Enabled
            ElseIf formName = "frmBrand" Then
                Return MainForm.mnuBrands.Enabled
            ElseIf formName = "frmSupplier" Then
                Return MainForm.mnuSuppliers.Enabled
            ElseIf formName = "frmInsurer" Then
                Return MainForm.mnuInsurers.Enabled
            ElseIf formName = "frmPOMaster" Then
                Return MainForm.mnuPOPrepar.Enabled
            ElseIf formName = "frmLocation" Then
                Return MainForm.mnuLocations.Enabled
            ElseIf formName = "frmInvSch" Then
                Return MainForm.mnuInvSch.Enabled
            ElseIf formName = "frmCompany" Then
                Return MainForm.mnuCompanies.Enabled
            ElseIf formName = "frmCostCenter" Then
                Return MainForm.mnuCostCenter.Enabled
            ElseIf formName = "frmGLCode" Then

                Return MainForm.mnuGLCode.Enabled
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function

    Private Sub SenDEmail(ByVal frm As Form)
        Try
            Dim sc As ZultecScreenCapture.ScreenCapture = New ZultecScreenCapture.ScreenCapture()
            ' capture entire screen, and save it to a file
            Dim img As Image = sc.CaptureScreen()
            ' display image in a Picture control named imageDisplay
            'this.imageDisplay.Image = img;
            ' capture this window, and save it
            If File.Exists(AppConfig.AppDataFolder & "\temp.gif") Then
                File.Delete(AppConfig.AppDataFolder & "\temp.gif")
            End If
            sc.CaptureWindowToFile(frm.Handle, AppConfig.AppDataFolder & "\temp.gif", System.Drawing.Imaging.ImageFormat.Gif)

            Dim message As MailMessage = New MailMessage()
            Dim mailatt As New Attachment(AppConfig.AppDataFolder & "\temp.gif")

            '' message.From = New MailAddress(txtFrom.Text)



            ''   message.To.Add(New MailAddress(txtTo.Text))

            'message.To.Add(New MailAddress("recipient2@foo.bar.com"))
            'message.To.Add(New MailAddress("recipient3@foo.bar.com"))
            'message.Cc.Add(New MailAddress("carboncopy@foo.bar.com"))

            '' message.Subject = txtSubject.Text


            '' message.Body = txtMessage.Text
            message.Attachments.Add(mailatt)


            Dim client As SmtpClient = New SmtpClient()
            ''  client.Host = txtSMTPServer.Text

            client.Send(message)
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub

    Public Function check_Child_Levels(ByVal _id As String, ByVal formid As Integer) As Boolean
        Dim objBALCompGroups As New BALCompGroups
        Dim ds As DataTable = objBALCompGroups.Check_Child(_id)
        If Not ds Is Nothing Then
            If Not ds Is Nothing Then
                If ds.Rows.Count > 0 Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function

    Public Function check_Child_AssetDetail(ByVal _id As String, ByVal formid As Integer) As Boolean
        Dim objAssetDetails As New BALAssetDetails
        Dim rsult As Integer = objAssetDetails.Check_Child(_id, formid)
        If rsult > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function check_Child_Group(ByVal _id As String, ByVal formid As Integer) As Boolean
        Dim objBALOrgHier As New BALOrgHier
        Dim ds As DataTable = objBALOrgHier.Check_Child(_id)
        If Not ds Is Nothing Then
            If ds.Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function check_Child_Roles(ByVal _id As String, ByVal formid As Integer) As Boolean
        Dim objBALUsers As New BALUsers
        Dim ds As DataTable = objBALUsers.Check_Child(_id, formid)
        If Not ds Is Nothing Then
            If ds.Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function



    Public Function check_Child_BarCode(ByVal _id As String, ByVal formid As Integer) As Boolean
        Dim objBALBarCode_Struct As New BALBarCode_Struct
        Dim ds As DataTable = objBALBarCode_Struct.Check_Child(_id, formid)
        If Not ds Is Nothing Then
            If ds.Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function check_Child_Unit(ByVal _id As String, ByVal formid As Integer) As Boolean
        Dim objBALUnits As New BALUnits
        Dim ds As DataTable = objBALUnits.Check_Child(_id, formid)
        If Not ds Is Nothing Then
            If ds.Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function check_Child_CostCenter(ByVal _id As String, ByVal formid As Integer) As Boolean
        Dim objBALCostCenter As New BALCostCenter
        Dim ds As DataTable = objBALCostCenter.Check_Child(_id, formid)
        If Not ds Is Nothing Then
            If ds.Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function check_Child_Custodian(ByVal _id As String, ByVal formid As Integer) As Boolean
        Dim objBALCustodian As New BALCustodian
        Dim ds As DataTable = objBALCustodian.Check_Child(_id, formid)
        If Not ds Is Nothing Then
            If ds.Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function
    Public Function check_Child_AstHistory(ByVal objattAstHistory As attAstHistory, ByVal JustChanged As Boolean) As Boolean
        Dim objBALAst_History As New BALAst_History
        Dim str As String = objBALAst_History.Check_Child(objattAstHistory, JustChanged)
        If str > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function check_Child_AstBooks(ByVal _id As String, ByVal formid As Integer) As Boolean
        Dim objBALAstBooks As New BALAstBooks
        Dim ds As DataTable = objBALAstBooks.Check_Child(_id, formid)
        If Not ds Is Nothing Then
            If ds.Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function
    Public Function check_Child_AssetCategory(ByVal _id As String) As Boolean
        Dim objBALAssets As New BALItems
        Dim ds As DataTable = objBALAssets.Check_Child(_id)
        If Not ds Is Nothing Then
            If ds.Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function check_Child_AddCostHistory(ByVal _id As String) As Boolean
        Dim objBALAddCostHist As New BALAddCostHistory
        If objBALAddCostHist.Check_Child(_id) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function check_Child_CostCenter(ByVal _id As String) As Boolean
        Dim objBALPO As New BALPurchaseOrder
        If objBALPO.Check_Child(_id) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function check_Child_CostType(ByVal _id As String) As Boolean
        Dim objBAL As New BALAddCostHistory
        If objBAL.Check_ChildCostType(_id) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Declare Function WriteProfileString Lib "Kernel" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String) As Integer

    Declare Function CreateScalableFontResource% Lib "GDI" (ByVal fHidden%, ByVal lpszResourceFile$, ByVal lpszFontFile$, ByVal lpszCurrentPath$)
    Declare Function AddFontResource Lib "GDI" (ByVal lpFilename As Object) As Integer
    Declare Function SendMessage Lib "User" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Object) As Long


#Region "Filling Component one Grid"
    'Public Sub FillDSToGrid(ByVal dt As DataTable, ByVal grd As C1FlexGrid, ByVal pb As ProgressBar)
    '    Try
    '        grd.Redraw = False
    '        pb.Visible = True
    '        pb.Step = 1
    '        pb.Value = 0
    '        pb.Maximum = dt.Rows.Count

    '        'clearing the data
    '        grd.Clear(ClearFlags.All)
    '        'Removing the Columns from the Grid
    '        If grd.Cols.Count > 0 Then
    '            grd.Cols.RemoveRange(0, grd.Cols.Count - 1)
    '        End If

    '        grd.Cols.Count = dt.Columns.Count
    '        grd.Rows.Count = dt.Rows.Count + 1

    '        'Adding the Columns to the Grid, Grid columns start from 1 because there is an indicator
    '        For i As Integer = 0 To dt.Columns.Count - 1
    '            grd.Cols(i).Name = dt.Columns(i).ColumnName
    '            grd.Cols(i).Caption = dt.Columns(i).Caption
    '            grd.Cols(i).TextAlign = TextAlignEnum.LeftTop
    '        Next

    '        'Adding the Records to the Grid, This is the fastest way to fill the Grid from Componant studio
    '        Dim rowIndex As Integer = 0
    '        Dim colIndex As Integer
    '        rowIndex = 0
    '        For Each dr As DataRow In dt.Rows
    '            rowIndex = rowIndex + 1
    '            colIndex = 0
    '            For Each dc As DataColumn In dt.Columns
    '                grd(rowIndex, colIndex) = dr(dc)
    '                colIndex = colIndex + 1
    '            Next
    '            pb.PerformStep()
    '        Next
    '    Finally
    '        pb.Visible = False
    '        grd.Redraw = True
    '    End Try
    'End Sub

    'Public Sub FillDSToGridWithIndicator(ByVal dt As DataTable, ByVal grd As C1FlexGrid, ByVal pb As ProgressBar)
    '    Try
    '        grd.Redraw = False
    '        pb.Visible = True
    '        pb.Step = 1
    '        pb.Value = 0
    '        pb.Maximum = dt.Rows.Count

    '        'clearing the data
    '        grd.Clear(ClearFlags.All)
    '        'Removing the Columns from the Grid
    '        If grd.Cols.Count > 0 Then
    '            grd.Cols.RemoveRange(1, grd.Cols.Count - 1)
    '        End If

    '        grd.Cols.Count = dt.Columns.Count + 1
    '        grd.Rows.Count = dt.Rows.Count + 1

    '        'Adding the Columns to the Grid, Grid columns start from 1 because there is an indicator
    '        For i As Integer = 0 To dt.Columns.Count - 1
    '            grd.Cols(i + 1).Name = dt.Columns(i).ColumnName
    '            grd.Cols(i + 1).Caption = dt.Columns(i).Caption
    '        Next

    '        'Adding the Records to the Grid, This is the fastest way to fill the Grid from Componant studio
    '        Dim rowIndex As Integer = 0
    '        Dim colIndex As Integer
    '        rowIndex = 0
    '        For Each dr As DataRow In dt.Rows
    '            rowIndex = rowIndex + 1
    '            colIndex = 0
    '            For Each dc As DataColumn In dt.Columns
    '                colIndex = colIndex + 1
    '                grd(rowIndex, colIndex) = dr(dc)
    '            Next
    '            pb.PerformStep()
    '        Next
    '    Finally
    '        pb.Visible = False
    '        grd.Redraw = True
    '    End Try
    'End Sub


    'Public Sub FillDSToGridWithIndicator(ByVal dt() As DataRow, ByVal grd As C1FlexGrid, ByVal pb As ProgressBar)
    '    Try
    '        grd.Redraw = False
    '        pb.Visible = True
    '        pb.Step = 1
    '        pb.Value = 0
    '        pb.Maximum = dt.Length

    '        'clearing the data
    '        grd.Clear(ClearFlags.All)
    '        'removing the Columns from the Grid
    '        If grd.Cols.Count > 0 Then
    '            grd.Cols.RemoveRange(1, grd.Cols.Count - 1)
    '        End If

    '        grd.Cols.Count = dt(0).ItemArray.Length + 1
    '        grd.Rows.Count = dt.Length + 1
    '        'Adding the Columns to the Grid, Grid columns start from 1 because there is an indicator
    '        For i As Integer = 0 To dt(0).Table.Columns.Count - 1
    '            grd.Cols(i + 1).Name = dt(0).Table.Columns(i).ColumnName
    '        Next

    '        'Adding the Records to the Grid, This is the fastest way to fill the Grid from Componant studio
    '        Dim rowIndex As Integer = 0
    '        Dim colIndex As Integer
    '        For Each dr As DataRow In dt
    '            rowIndex = rowIndex + 1
    '            colIndex = 0
    '            For i As Integer = 0 To dr.ItemArray.Length - 1
    '                colIndex = colIndex + 1
    '                grd(rowIndex, colIndex) = dr.Item(i)
    '            Next
    '            pb.PerformStep()
    '        Next
    '    Finally
    '        pb.Visible = False
    '        grd.Redraw = True
    '    End Try
    'End Sub

#End Region

    Public Function Generate_BarCode(ByVal CompanyID As String, ByVal AstID As String, ByVal AstNum As String, ByVal REF As String, ByVal Cat1 As String, ByVal Cat2 As String, ByVal Loc1 As String, ByVal Loc2 As String, ByVal LOCM As String) As String
        Dim ds As New DataTable
        Dim strBarCodeStruct, ValueSep As String
        Dim strBarCode As String = ""
        Dim Str() As String
        Dim objattCompany As New attcompany
        Dim objBALCompany As New BALCompany
        objattCompany.PKeyCode = CompanyID

        ds = objBALCompany.GetAll_Company(objattCompany)
        If ds Is Nothing = False Then
            If ds.Rows.Count > 0 Then
                strBarCodeStruct = ds.Rows(0)("barcode")
                ValueSep = ds.Rows(0)("ValueSep")
                If ValueSep = "None" Then
                    ValueSep = ""
                End If
                Str = strBarCodeStruct.Split(",")
                If Str.Length > 0 Then
                    For idx As Integer = 0 To Str.Length - 1
                        If Str(idx).StartsWith("AID") Then
                            Dim str2() As String
                            str2 = Str(idx).Split("-")
                            If str2.Length > 1 Then
                                If AstID <> "" Then
                                    If CInt(str2(1)) = 0 Then
                                        If strBarCode = "" Then
                                            strBarCode += Trim(AstID)
                                        Else
                                            strBarCode += ValueSep + Trim(AstID)
                                        End If

                                    Else
                                        Dim idx2 As Integer
                                        idx2 = AstID.Length - CInt(str2(1))
                                        If idx2 < 0 Then idx2 = 0
                                        If strBarCode = "" Then
                                            strBarCode += Trim(AstID.Substring(idx2))
                                        Else
                                            strBarCode += ValueSep + Trim(AstID.Substring(idx2))
                                        End If

                                    End If

                                End If

                            End If
                        ElseIf Str(idx).StartsWith("LOCM") Then
                            Dim str2() As String
                            str2 = Str(idx).Split("-")
                            If str2.Length > 1 Then
                                Dim str3() As String
                                str3 = LOCM.Split("\")
                                If str3.Length > 1 Then

                                    If CInt(str2(1)) = 0 Then
                                        Dim strCat As String
                                        If strBarCode = "" Then
                                            strCat = str3(str3.Length - 2)
                                            strCat = Trim(strCat)
                                            If strCat.Length < 4 Then
                                                strBarCode += strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += strCat.Substring(0, 4)
                                            End If
                                        Else
                                            strCat = str3(str3.Length - 2)
                                            strCat = Trim(strCat)
                                            If strCat.Length < 4 Then
                                                strBarCode += strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += ValueSep + strCat.Substring(0, 4)
                                            End If
                                        End If
                                    Else
                                        Dim strCat As String
                                        If strBarCode = "" Then
                                            strCat = str3(str3.Length - 3)
                                            strCat = Trim(strCat)
                                            If strCat.Length < CInt(str2(1)) Then
                                                strBarCode += strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += strCat.Substring(0, CInt(str2(1)))
                                            End If
                                        Else
                                            strCat = str3(str3.Length - 2)
                                            strCat = Trim(strCat)
                                            If strCat.Length < CInt(str2(1)) Then
                                                strBarCode += ValueSep + strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += ValueSep + strCat.Substring(0, CInt(str2(1)))
                                            End If
                                        End If
                                    End If

                                End If

                            End If
                        ElseIf Str(idx).StartsWith("ANM") Then
                            Dim str2() As String
                            str2 = Str(idx).Split("-")
                            If str2.Length > 1 Then
                                If CInt(str2(1)) = 0 Then
                                    If strBarCode = "" Then
                                        strBarCode += Trim(AstNum)
                                    Else
                                        strBarCode += ValueSep + Trim(AstNum)
                                    End If


                                Else
                                    Dim idx2 As Integer
                                    idx2 = AstNum.Length - CInt(str2(1))
                                    If idx2 < 0 Then idx2 = 0

                                    If strBarCode = "" Then
                                        strBarCode += Trim(AstNum.Substring(idx2)).PadLeft(CInt(str2(1)), "0")
                                    Else
                                        strBarCode += ValueSep + Trim(AstNum.Substring(idx2)).PadLeft(CInt(str2(1)), "0")
                                    End If


                                End If

                            End If
                        ElseIf Str(idx).StartsWith("REF") Then
                            Dim str2() As String
                            str2 = Str(idx).Split("-")
                            If str2.Length > 1 Then
                                If CInt(str2(1)) = 0 Then
                                    If strBarCode = "" Then
                                        strBarCode += Trim(REF)
                                    Else
                                        strBarCode += ValueSep + Trim(REF)
                                    End If



                                Else
                                    Dim idx2 As Integer
                                    idx2 = AstID.Length - CInt(str2(1))
                                    If idx2 < 0 Then idx2 = 0
                                    If strBarCode = "" Then
                                        strBarCode += Trim(REF.Substring(idx)).PadLeft(CInt(str2(1)), "0")
                                    Else
                                        strBarCode += ValueSep + Trim(REF.Substring(idx)).PadLeft(CInt(str2(1)), "0")
                                    End If


                                End If

                            End If
                        ElseIf Str(idx).StartsWith("CAT1") Then
                            Dim str2() As String
                            str2 = Str(idx).Split("-")
                            If str2.Length > 1 Then
                                Dim str3() As String
                                str3 = Cat1.Split("\")
                                If str3.Length >= 1 Then
                                    If CInt(str2(1)) = 0 Then
                                        Dim strCat As String
                                        If strBarCode = "" Then
                                            strCat = str3(str3.Length - 2)
                                            strCat = Trim(strCat)
                                            If strCat.Length < 4 Then
                                                strBarCode += strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += strCat.Substring(0, 4)
                                            End If
                                        Else
                                            strCat = str3(str3.Length - 2)
                                            strCat = Trim(strCat)
                                            If strCat.Length < 4 Then
                                                strBarCode += strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += ValueSep + strCat.Substring(0, 4)
                                            End If
                                        End If
                                    Else
                                        Dim strCat As String
                                        If strBarCode = "" Then
                                            strCat = str3(str3.Length - 2)
                                            strCat = Trim(strCat)
                                            If strCat.Length < CInt(str2(1)) Then
                                                strBarCode += strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += strCat.Substring(0, CInt(str2(1)))
                                            End If
                                        Else
                                            strCat = str3(str3.Length - 2)
                                            strCat = Trim(strCat)
                                            If strCat.Length < CInt(str2(1)) Then
                                                strBarCode += ValueSep + strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += ValueSep + strCat.Substring(0, CInt(str2(1)))
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        ElseIf Str(idx).StartsWith("CAT2") Then
                            Dim str2() As String
                            str2 = Str(idx).Split("-")
                            If str2.Length > 1 Then
                                Dim str3() As String
                                str3 = Cat2.Split("\")
                                If str3.Length >= 2 Then
                                    If CInt(str2(1)) = 0 Then
                                        Dim strCat As String
                                        If strBarCode = "" Then
                                            strCat = str3(str3.Length - 1)
                                            strCat = Trim(strCat)
                                            If strCat.Length < 4 Then
                                                strBarCode += strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += strCat.Substring(0, 4)
                                            End If
                                        Else
                                            strCat = str3(str3.Length - 1)
                                            strCat = Trim(strCat)
                                            If strCat.Length < 4 Then
                                                strBarCode += strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += ValueSep + strCat.Substring(0, 4)
                                            End If
                                        End If
                                    Else
                                        Dim strCat As String
                                        If strBarCode = "" Then
                                            strCat = str3(str3.Length - 1)
                                            strCat = Trim(strCat)
                                            If strCat.Length < CInt(str2(1)) Then
                                                strBarCode += strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += strCat.Substring(0, CInt(str2(1)))
                                            End If
                                        Else
                                            strCat = str3(str3.Length - 1)
                                            strCat = Trim(strCat)
                                            If strCat.Length < CInt(str2(1)) Then
                                                strBarCode += ValueSep + strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += ValueSep + strCat.Substring(0, CInt(str2(1)))
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        ElseIf Str(idx).StartsWith("LOC1") Then
                            Dim str2() As String
                            str2 = Str(idx).Split("-")
                            If str2.Length > 1 Then
                                Dim str3() As String
                                'str3 = Loc1.Split("\")
                                str3 = LOCM.Split("\")
                                If str3.Length > 1 Then
                                    If CInt(str2(1)) = 0 Then
                                        Dim strCat As String
                                        If strBarCode = "" Then
                                            strCat = str3(str3.Length - 2)
                                            strCat = Trim(strCat)
                                            If strCat.Length < 4 Then
                                                strBarCode += strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += strCat.Substring(0, 4)
                                            End If
                                        Else
                                            strCat = str3(str3.Length - 2)
                                            strCat = Trim(strCat)
                                            If strCat.Length < 4 Then
                                                strBarCode += strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += ValueSep + strCat.Substring(0, 4)
                                            End If
                                        End If
                                    Else
                                        Dim strCat As String
                                        If strBarCode = "" Then
                                            strCat = str3(str3.Length - 2)
                                            strCat = Trim(strCat)
                                            If strCat.Length < CInt(str2(1)) Then
                                                strBarCode += strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += strCat.Substring(0, CInt(str2(1)))
                                            End If
                                        Else
                                            strCat = str3(str3.Length - 2)
                                            strCat = Trim(strCat)
                                            If strCat.Length < CInt(str2(1)) Then
                                                strBarCode += ValueSep + strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += ValueSep + strCat.Substring(0, CInt(str2(1)))
                                            End If
                                        End If

                                    End If
                                End If

                            End If
                        ElseIf Str(idx).StartsWith("LOC2") Then
                            Dim str2() As String
                            str2 = Str(idx).Split("-")
                            If str2.Length > 1 Then
                                Dim str3() As String
                                str3 = LOCM.Split("\")
                                If str3.Length > 2 Then

                                    If CInt(str2(1)) = 0 Then
                                        Dim strCat As String
                                        If strBarCode = "" Then
                                            strCat = str3(str3.Length - 1)
                                            strCat = Trim(strCat)
                                            If strCat.Length < 4 Then
                                                strBarCode += strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += strCat.Substring(0, 4)
                                            End If
                                        Else
                                            strCat = str3(str3.Length - 1)
                                            strCat = Trim(strCat)
                                            If strCat.Length < 4 Then
                                                strBarCode += strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += ValueSep + strCat.Substring(0, 4)
                                            End If
                                        End If
                                    Else
                                        Dim strCat As String
                                        If strBarCode = "" Then
                                            strCat = str3(str3.Length - 1)
                                            strCat = Trim(strCat)
                                            If strCat.Length < CInt(str2(1)) Then
                                                strBarCode += strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += strCat.Substring(0, CInt(str2(1)))
                                            End If
                                        Else
                                            strCat = str3(str3.Length - 1)
                                            strCat = Trim(strCat)
                                            If strCat.Length < CInt(str2(1)) Then
                                                strBarCode += ValueSep + strCat.Substring(0, strCat.Length)
                                            Else
                                                strBarCode += ValueSep + strCat.Substring(0, CInt(str2(1)))
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        ElseIf Str(idx).StartsWith("FIX") Then
                            Dim strPre() As String
                            strPre = Str(idx).Split("-")
                            If strPre.Length > 1 Then
                                strBarCode = strPre(1)
                            End If
                        End If
                    Next
                End If

            End If
        End If

        Return strBarCode
    End Function
   

    Public Function Generate_AssetNumber(ByVal ComPID As String) As String
        If AppConfig.IsOfflineMachine Then
            Dim objBalOfflineMachine As New BALOfflineMachines
            Dim dt As DataTable = objBalOfflineMachine.GetAll_OfflineMachine(New AttOfflineMachines)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Dim StartSerial As Int64 = dt.Rows(0)("StartSerial")
                    Dim EndSerial As Int64 = dt.Rows(0)("EndSerial")
                    Dim LastAssetNumber As Int64 = dt.Rows(0)("LastAssetNumber")
                    If LastAssetNumber >= StartSerial Then
                        Return LastAssetNumber + 1
                    Else
                        Return StartSerial
                    End If
                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        Else
            'Main Database, Not Offline Machine:


            Dim AstNum As Long = 0
            Dim objBALAssetDetails As New BALAssetDetails
            If AppConfig.CodingMode Then ' Use Assets Coding Definition
                If ComPID <> "" Then
                    Dim StartRange, EndRange As Long
                    StartRange = 0
                    EndRange = 0
                    Dim ds As New DataTable
                    Dim objattAssetsCoding As New attAssetsCoding
                    Dim objBALAssetsCoding As New BALAssetsCoding
                    objattAssetsCoding.CompanyID = ComPID
                    objattAssetsCoding.Status = True
                    ds = objBALAssetsCoding.GetAll_AssetCoding(objattAssetsCoding)
                    If ds Is Nothing = False Then
                        If ds.Rows.Count > 0 Then
                            StartRange = ds.Rows(0)("StartSerial")
                            EndRange = ds.Rows(0)("EndSerial")
                        End If
                    End If
                    Dim objBALCompany As New BALCompany
                    If EndRange <> 0 And StartRange <> 0 Then
                        'get the AssetNumber from company last AssetNumber
                        Dim LastAssetNumber As Int64 = objBALCompany.GetCompanyLastAssetNumber(ComPID)
                        If LastAssetNumber >= StartRange Then
                            AstNum = LastAssetNumber + 1
                        Else
                            AstNum = StartRange
                        End If

                        If EndRange < AstNum Then
                            ZulMessageBox.ShowMe("RangeExceedCreateNew")
                            objBALAssetsCoding.CloseRange(objattAssetsCoding)
                            Return ""
                        End If
                    Else
                        ZulMessageBox.ShowMe("DefineRange")
                        Return ""
                    End If

                End If
            Else 'Use Incremental Coding

                Dim intID As Long = objBALAssetDetails.GetNextPKey_AssetDetails()
                AstNum = intID + 1
            End If
            Return AstNum
        End If
    End Function


    Public Function CalculatePercent(ByVal SalYearValue As String, ByVal SalMonthValue As String) As String
        If SalYearValue <> "" And SalMonthValue <> "" Then
            Dim percent As Double
            Dim SalMonth As Integer = CInt(SalMonthValue)
            Dim SalYear As Integer = CInt(SalYearValue)

            Dim perc As Double
            perc = SalYear + (SalMonth / 12)
            If perc = 0 Then
                percent = 0
            Else
                percent = Math.Round(100 / perc, 2)
            End If

            Return percent.ToString
        Else
            Return "0"
        End If
    End Function

    Public Sub addGridMenu(ByVal grd As DevExpress.XtraGrid.GridControl)
        Dim CntMnu As New ContextMenuStrip
        CntMnu.Items.Add("Export to Excel", My.Resources.Icons.ExportToExcel)
        AddHandler CntMnu.Items(0).Click, AddressOf GridExportToExcel
        CntMnu.Items.Add("Print Preview", My.Resources.Icons.Preview)
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

#Region "Tree view By Wael Dalloul"
    Public Sub SelectTreeNodeByTag(ByVal tree As TreeView, ByVal nodes As TreeNodeCollection, ByVal nodetag As String)
        For Each tn As TreeNode In nodes
            If tn.Tag = nodetag Then
                tree.SelectedNode = tn
                Return
            Else
                SelectTreeNodeByTag(tree, tn.Nodes, nodetag)
            End If
        Next
    End Sub
#End Region

#Region "Global Functions by Wael Dalloul"

    Public Function WhoCalledMe() As String
        'StackFrame:
        'Provides information about the function call on the stack for the current process.

        'StackTrace:
        'Is a collection of all the StackFrames on the stack.

        'MethodBase:
        'Is an abstract class that contains information about currently executing method. 
        Dim st As StackTrace = New StackTrace()
        Dim sf As StackFrame = st.GetFrame(1)
        Dim mb As MethodBase = sf.GetMethod()
        Return mb.Name
    End Function
    'Public Sub SetIcon(ByVal frm As Form)
    '    Dim rm As New ResourceManager("ZulAssets.Resources", Assembly.GetExecutingAssembly())
    '    frm.Icon = CType(rm.GetObject("MainIcon"), System.Drawing.Icon)
    'End Sub

    Function RemoveUnnecessaryChars(ByVal value As String) As String
        Return value.Trim.Replace("'", "''")
    End Function

    'Function GetMainConnectionStr() As String
    '    Dim _ConnectionString As String = ""
    '    If AppConfig.DbType = "2" Then
    '        Return _ConnectionString = "PROVIDER=msdaora;Data Source=" & AppConfig.DbServer & ";User Id=" & AppConfig.DbUname & ";Password=" & AppConfig.DbPass & ""
    '    Else
    '        'Return _ConnectionString = "Provider=SQLNCLI11;DATA SOURCE=" & AppConfig.DbServer & ";UID=" & AppConfig.DbUname & ";PWD=" & AppConfig.DbPass & ";DATABASE=" & AppConfig.DbName & ""
    '        Return _ConnectionString = "Provider=SQLNCLI11;DATA SOURCE=" & AppConfig.DbServer & "," & Val(AppConfig.SQLPort) & ";UID=" & AppConfig.DbUname & ";PWD=" & AppConfig.DbPass & ";DATABASE=" & AppConfig.DbName & ""
    '    End If

    'End Function

    Function IsNumber(ByVal ch As Char) As Boolean
        If Char.IsNumber(ch) Or Char.IsControl(ch) Then
            Return True
        Else
            Return False
        End If
    End Function

    Dim numberFormatInfo As NumberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat

    Function IsDecimal(ByVal ch As Char, ByVal text As String) As Boolean
        Dim decimalSeparator As String = numberFormatInfo.NumberDecimalSeparator
        If Char.IsNumber(ch) Or Char.IsControl(ch) Or (ch = decimalSeparator And text.IndexOf(decimalSeparator) < 0) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub FillServerList(ByVal cmb As DevExpress.XtraEditors.ComboBoxEdit)
        cmb.Properties.Items.Clear()
        Dim oTable As New Data.DataTable
        Try
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            oTable = System.Data.Sql.SqlDataSourceEnumerator.Instance.GetDataSources
            cmb.Properties.BeginUpdate()
            For Each oRow As DataRow In oTable.Rows
                If oRow("InstanceName").ToString = "" Then
                    cmb.Properties.Items.Add(oRow("ServerName"))
                Else
                    cmb.Properties.Items.Add(oRow("ServerName").ToString & "\" & oRow("InstanceName").ToString)
                End If
            Next oRow
            cmb.Properties.EndUpdate()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ZulAsset", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            oTable.Dispose()
        End Try
    End Sub

    Public Function GetDatabasesList(ByVal server As String, ByVal port As String, ByVal username As String, ByVal password As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New OleDbCommand
            Dim Conn As New OleDbConnection()
            If port <> "" Then
                Conn.ConnectionString = "Provider=SQLNCLI11;DATA SOURCE=" & server & "," & port & ";UID=" & username & ";PWD=" & password & ";DATABASE=Master" & ";Connect Timeout=3"
            Else
                Conn.ConnectionString = "Provider=SQLNCLI11;DATA SOURCE=" & server & ";UID=" & username & ";PWD=" & password & ";DATABASE=Master" & ";Connect Timeout=3"
            End If
            Conn.Open()
            cmd.Connection = Conn
            cmd.CommandText = "select name from sysdatabases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb')  order by name"
            Dim da As New OleDbDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As OleDbException
            MessageBox.Show(ex.Message, "ZulAsset", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
        Return dt
    End Function

#End Region

#Region "DLL Imports"

    ''' <summary>
    ''' This function retrieves the status of the specified virtual key.
    ''' The status specifies whether the key is up, down.
    ''' </summary>
    ''' <param name="keyCode">Specifies a key code for the button to me checked</param>
    ''' <returns>Return value will be 0 if off and 1 if on</returns>
    <DllImport("user32.dll")> _
    Public Function GetKeyState(ByVal keyCode As Integer) As Short
    End Function

    ''' <summary>
    ''' This function is useful to simulate Key presses to the window with focus.
    ''' </summary>
    ''' <param name="bVk">Specifies a virtual-key code. The code must be a value in the range 1 to 254.</param>
    ''' <param name="bScan">Specifies a hardware scan code for the key.</param>
    ''' <param name="dwFlags"> Specifies various aspects of function operation. This parameter can be one or more of the following values.
    ''' <code>KEYEVENTF_EXTENDEDKEY</code> or <code>KEYEVENTF_KEYUP</code>
    ''' If specified, the key is being released. If not specified, the key is being depressed.</param>
    ''' <param name="dwExtraInfo">Specifies an additional value associated with the key stroke</param>
    <DllImport("user32.dll")> _
    Public Sub keybd_event(ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As UInteger, ByVal dwExtraInfo As Integer)
    End Sub

#End Region


    ' In Oracle All fields are uppercase, this is causing problem in the grid.
    Public Function GetGridRowCellDisplayValue(ByVal grv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal RowIndex As Integer, ByVal ColumnName As String) As Object

        If AppConfig.DbType = "1" Then 'SQL Server DBMS
            Return grv.GetRowCellDisplayText(RowIndex, ColumnName)
        Else 'Oracle DBMS
            Return grv.GetRowCellDisplayText(RowIndex, ColumnName.ToUpper)
        End If
    End Function

    Public Function GetGridRowCellValue(ByVal grv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal RowIndex As Integer, ByVal ColumnName As String) As Object

        If AppConfig.DbType = "1" Then 'SQL Server DBMS
            Return grv.GetRowCellValue(RowIndex, ColumnName)
        Else 'Oracle DBMS
            Return grv.GetRowCellValue(RowIndex, ColumnName.ToUpper)
        End If
    End Function

    Public Sub SetGridRowCellValue(ByVal grv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal RowIndex As Integer, ByVal ColumnName As String, ByVal Value As Object)
        If AppConfig.DbType = "1" Then 'SQL Server DBMS
            grv.SetRowCellValue(RowIndex, ColumnName, Value)
        Else 'Oracle DBMS
            grv.SetRowCellValue(RowIndex, ColumnName.ToUpper, Value)
        End If
    End Sub

    Public Function GetGridColumn(ByVal grv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal ColumnName As String) As DevExpress.XtraGrid.Columns.GridColumn
        If AppConfig.DbType = "1" Then 'SQL Server DBMS
            Return grv.Columns(ColumnName)
        Else 'Oracle DBMS
            Return grv.Columns(ColumnName.ToUpper)
        End If
    End Function

    Public Function GetGridColumnName(ByVal ColumnName As String) As String
        If AppConfig.DbType = "1" Then 'SQL Server DBMS
            Return ColumnName
        Else 'Oracle DBMS
            Return ColumnName.ToUpper
        End If
    End Function


    Public Function LoadReport(ByVal ReportName As String) As XtraReport
        'make sure the report is found before showing it, and show message.
        Dim objBALRptFile As New BALReports
        Dim rpt As New XtraReport
        ' Retrieve a string which contains the report.
        Dim ds As DataTable = objBALRptFile.GetReportFileData(ReportName)
        If ds.Rows.Count > 0 Then
            Dim s As String = ds.Rows(0)("ReportData").ToString
            ' Obtain the report from the string.
            Dim sw As New StreamWriter(New MemoryStream())
            Try
                sw.Write(s)
                sw.Flush()
                rpt = XtraReport.FromStream(sw.BaseStream, True)
            Finally
                sw.Dispose()
            End Try
            rpt.PrintingSystem.ShowPrintStatusDialog = True
            If Trim(AppConfig.LabelPrinter) <> "" Then
                rpt.PrinterName = AppConfig.LabelPrinter
            Else
                rpt.PrinterName = (New PrinterSettings).PrinterName ' if there is no printer selected, then use the default printer.
            End If
            rpt.PrintingSystem.ShowMarginsWarning = False
            Return rpt
        Else
            ZulMessageBox.ShowMe("ReportNotFound", MessageBoxButtons.OK, MessageBoxIcon.Error, True)
            Return Nothing
        End If
    End Function

    Public Function ShowRegistration() As Boolean
        Dim ctlRegistration As New ZulLib.ctlRegistration

        'ServerCharacter Used to distinguish between the applications
        'G ZulAssets
        'R ZulRoute
        'M ZulMart Backend
        'I ZulMart Frontend
        'Z ZWMS

        ctlRegistration.ServerCharacter = "G"
        ctlRegistration.ClientCharacter = "H"
        ctlRegistration.SelectedEdition = "E"


        Dim popup As New frmPopup
        Try
            popup.Size = ctlRegistration.Size
            popup.Controls.Add(ctlRegistration)
            popup.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            popup.Text = Application.ProductName & " - Registration"
            ctlRegistration.Dock = DockStyle.Fill
            'popup.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            popup.MinimizeBox = False
            popup.MaximizeBox = False
            popup.StartPosition = FormStartPosition.CenterScreen
            popup.ShowDialog()
            Return ctlRegistration.Registered
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, AppConfig.LoginName, WhoCalledMe)
            Return False
        Finally
            ctlRegistration.Dispose()
            popup.Dispose()
        End Try

    End Function

    Public Function CheckRegistration() As Boolean
        'Check If Registered by using lickey and hardwareID
        Dim LicFile As String = Application.StartupPath & "\License.txt"
        If Not File.Exists(LicFile) Then
            Return False
        End If
        'open text file and save license information for future reference 
        Dim sr As New StreamReader(LicFile)
        Try
            Dim companyName As String = sr.ReadLine().Split(":"c)(1).Trim
            Dim ApplicationEdition As String = sr.ReadLine.Split(":"c)(1).Trim
            Dim Serial As String = sr.ReadLine.Split(":"c)(1).Trim
            Dim licKey As String = sr.ReadLine.Split(":"c)(1).Trim
            Dim Email As String = sr.ReadLine.Split(":"c)(1).Trim

            'Get Actual Application Serial, because maybe user will copy serial from zulmart to zwms...
            Serial = Serial.Substring(0, 4) + AppConfig.ServerCharacter + ApplicationEdition + Serial.Substring(6, Serial.Length - 6)

            If String.IsNullOrEmpty(licKey) Then
                Return False
            Else

                Dim lic As New ZulLib.ZulLic
                If lic.isRegisteredVersion(Serial, licKey) Then
                    Select Case ApplicationEdition
                        Case "I"
                            AppConfig.AppEdition = AppConfig.ApplicationRegistration.Inventory
                            'Return "Inventory"
                        Case "T"
                            AppConfig.AppEdition = AppConfig.ApplicationRegistration.Tracking
                            'Return "Tracking"
                        Case "F"
                            AppConfig.AppEdition = AppConfig.ApplicationRegistration.Financial
                            'Return "Financial"
                        Case "E"
                            AppConfig.AppEdition = AppConfig.ApplicationRegistration.Enterprise
                            'Return "Enterprise"
                        Case Else
                            AppConfig.AppEdition = AppConfig.ApplicationRegistration.NotRegistered
                            'Return ""
                    End Select

                    AppConfig.IsDemoKey = lic.IsDemoLic(licKey)
                    AppConfig.ISRegistered = True
                    AppConfig.CompanyName = companyName
                    AppConfig.CompanyEmail = Email
                    Return True
                Else
                    AppConfig.AppEdition = AppConfig.ApplicationRegistration.NotRegistered
                    AppConfig.ISRegistered = False
                    Return False
                End If
           
            End If
        Catch ex As Exception
            Return False
        Finally
            sr.Close()
        End Try
    End Function

End Module
