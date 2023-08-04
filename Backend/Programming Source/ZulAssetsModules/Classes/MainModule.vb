Imports System.IO
Imports ZultecScreenCapture
Imports System.Net.Mail
Imports System.Drawing.Printing
Imports System.Reflection
Imports System.Diagnostics
Imports System.Resources
Imports System.Runtime.InteropServices
Imports System.Globalization
Imports DevExpress.XtraEditors.DXErrorProvider
Imports System.Data.OleDb
Imports System.Collections.Generic
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports System.Windows.Forms
Imports System.Drawing

Module MainModule


    Public ISRegistered As Boolean
    'Public strRoleID As String

    Public valRulenotEmpty As New ConditionValidationRule
    Public valRuleGreaterThanZero As New ConditionValidationRule
    Public valRuleGreaterOrEqualZero As New ConditionValidationRule
    Public valRuleNotContainMinus As New ConditionValidationRule


    Public NumberMask As String = "###,###,###,##0.00"
    Public NumberMaskType As DevExpress.XtraEditors.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
    Public MainForm As frmMain
    'Public LogoFilePath As String
    Public CompanyLogoImage As Image

    Public Function ShowSaveConfirmation() As DialogResult
        Return MessageBox.Show("Do you want to save changes?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
    End Function

    Public Function ShowCancelConfirmation() As DialogResult
        Return MessageBox.Show("Do you want to cancel your changes?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
    End Function

    Public Function ShowDeleteConfirmation() As DialogResult
        Return MessageBox.Show("Do you want to delete the selected item?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
    End Function

    Public Function ShowCanNotDelete() As DialogResult
        ZulMessageBox.ShowMe("CantDelete")
    End Function

    Public Function ShowRecordNotFound() As DialogResult
        ZulMessageBox.ShowMe("RecordNotFound")
    End Function

    Public Function ShowSaveParent() As DialogResult
        ZulMessageBox.ShowMe("ShowSaveParentFirst")
    End Function


    Public Function OpenTabForm(ByVal controlName As String, ByVal NewCaption As String, ByVal EditCaption As String, ByVal ParentTabPageName As String) As Control

        Dim page As New DevExpress.XtraTab.XtraTabPage

        MainForm.tabControlMain.TabPages.Add(page)

        Dim ctlName = controlName
        ctlName = [Assembly].GetEntryAssembly.GetName.Name & "." & ctlName
        Dim ctl As ctlBaseControl
        ctl = DirectCast([Assembly].GetEntryAssembly.CreateInstance(ctlName), Control)
        ctl.Tag = ParentTabPageName
        ctl.NewFormCaption = NewCaption
        ctl.EditFormCaption = EditCaption

        page.Controls.Add(ctl)

        ctl.Dock = DockStyle.Fill
        MainForm.tabControlMain.SelectedTabPage = page
        Return ctl
    End Function

    Public Function GetImageAsByteArray(ByVal img As Image) As Byte()
        Dim ms As New MemoryStream()
        If img IsNot Nothing Then
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
        End If
        Return ms.ToArray()
    End Function

    Public Function GetImageFromByteArray(ByVal arr As Byte()) As Image
        If arr IsNot Nothing And arr.Length > 0 Then
            Dim ms As New MemoryStream(arr, arr.Length)
            Return Image.FromStream(ms)
        Else
            Return Nothing
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

    Public Sub TrimControl(ByVal MainControl As Control)
        Dim ctl As Control
        For Each ctl In MainControl.Controls
            If TypeOf (ctl) Is GridLookUpEdit Then
                Continue For
            ElseIf TypeOf (ctl) Is LookUpEdit Then
                Continue For
            ElseIf TypeOf (ctl) Is TextEdit Then
                ctl.Text = ctl.Text.Trim
            End If
            If ctl.HasChildren() Then
                TrimControl(ctl)
            End If
        Next
    End Sub

    Public Sub ClearControl(ByVal MainControl As Control)
        Dim ctl As Control
        For Each ctl In MainControl.Controls
            If TypeOf (ctl) Is GridLookUpEdit Then
                CType(ctl, GridLookUpEdit).EditValue = Nothing
            ElseIf TypeOf (ctl) Is LookUpEdit Then
                CType(ctl, LookUpEdit).EditValue = Nothing
            ElseIf TypeOf (ctl) Is TextEdit Then
                ctl.Text = Nothing
            ElseIf TypeOf (ctl) Is PictureEdit Then
                CType(ctl, PictureEdit).EditValue = Nothing
            ElseIf TypeOf (ctl) Is CheckEdit Then
                CType(ctl, CheckEdit).EditValue = Nothing
            End If
            If ctl.HasChildren() Then
                ClearControl(ctl)
            End If
        Next
    End Sub

    Public Sub ClearFrame(ByVal FrameName As Object)
        Dim ctl As Object
        For Each ctl In FrameName.Controls
            If TypeOf (ctl) Is TextBox Then
                ctl.Text = ""
            ElseIf TypeOf (ctl) Is DateTimePicker Then

                ctl.value = DateTime.Now
            ElseIf TypeOf (ctl) Is CheckBox Then

                ctl.Checked = 0
            ElseIf TypeOf (ctl) Is DevExpress.XtraEditors.TextEdit Then
                ctl.Text = ""
            ElseIf TypeOf (ctl) Is ComboBoxEdit Then

                ctl.SelectedIndex = 0
            ElseIf TypeOf (ctl) Is Label Then
                If ctl.Tag <> "" Then
                    ctl.Text = ""
                End If
            End If

            If ctl.TabIndex = 0 Then ctl.Focus()
        Next
    End Sub

    Public Sub ClearTab(ByVal TabControl As Object)
        Dim ctl As Object

        For Each ctl In TabControl.Controls
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
#Region " Enum Declaration "

    Public Enum ReportType
        AssetDetails = 1
        NonBarCodedItems = 2
        AssetHistory = 3

    End Enum
#End Region

    Public Sub GenericExceptionHandler(ByVal ex As Exception, _
                                       ByVal ErrSource As String)
        If Not Directory.Exists(Application.StartupPath & "\ErrorsReports\") Then
            Directory.CreateDirectory(Application.StartupPath & "\ErrorsReports\")
        End If

        Dim ErrFile As String = Application.StartupPath & "\ErrorsReports\" & "ZulHRMSErr.Log"
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
        'Dim frm As New frmError
        'frm.Ex = ex
        'frm.StartPosition = FormStartPosition.CenterScreen
        'frm.ShowDialog()

    End Sub
    Public Function Check_Auth(ByVal formName As String) As Boolean
        Try
            'If formName = "frmAddress" Then
            '    Return MainForm.mnuAddressTemplates.Enabled
            'ElseIf formName = "frmOrgHier" Then
            '    Return MainForm.mnuOrgHier.Enabled
            'ElseIf formName = "frmDesig" Then
            '    Return MainForm.mnuDesgination.Enabled
            'ElseIf formName = "frmCat" Then
            '    Return MainForm.mnuAssetCat.Enabled
            'ElseIf formName = "frmAssets" Then
            '    Return MainForm.mnuAssetItems.Enabled
            'ElseIf formName = "frmCustodian" Then
            '    Return MainForm.mnuCustodians.Enabled
            'ElseIf formName = "frmBrand" Then
            '    Return MainForm.mnuBrands.Enabled
            'ElseIf formName = "frmSupplier" Then
            '    Return MainForm.mnuSuppliers.Enabled
            'ElseIf formName = "frmInsurer" Then
            '    Return MainForm.mnuInsurers.Enabled
            'ElseIf formName = "frmPOMaster" Then
            '    Return MainForm.mnuPOPrepar.Enabled
            'ElseIf formName = "frmLocation" Then
            '    Return MainForm.mnuLocations.Enabled
            'ElseIf formName = "frmInvSch" Then
            '    Return MainForm.mnuInvSch.Enabled
            'ElseIf formName = "frmCompany" Then
            '    Return MainForm.mnuCompanies.Enabled
            'ElseIf formName = "frmGLCode" Then
            '    Return MainForm.mnuGLCode.Enabled
            'End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function

    'Public Sub Check_ImageFolder()
    '    If Not Directory.Exists(Application.StartupPath & "\AssetImages") Then
    '        Directory.CreateDirectory(Application.StartupPath & "\AssetImages")
    '    End If
    'End Sub


    Private Sub SenDEmail(ByVal frm As Form)
        Try
            Dim sc As ZultecScreenCapture.ScreenCapture = New ZultecScreenCapture.ScreenCapture()
            ' capture entire screen, and save it to a file
            Dim img As Image = sc.CaptureScreen()
            ' display image in a Picture control named imageDisplay
            'this.imageDisplay.Image = img;
            ' capture this window, and save it
            If File.Exists(Application.StartupPath & "\temp.gif") Then
                File.Delete(Application.StartupPath & "\temp.gif")
            End If
            sc.CaptureWindowToFile(frm.Handle, Application.StartupPath & "\temp.gif", System.Drawing.Imaging.ImageFormat.Gif)

            Dim message As MailMessage = New MailMessage()
            Dim mailatt As New Attachment(Application.StartupPath & "\temp.gif")

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


    Declare Function WriteProfileString Lib "Kernel" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String) As Integer

    Declare Function CreateScalableFontResource% Lib "GDI" (ByVal fHidden%, ByVal lpszResourceFile$, ByVal lpszFontFile$, ByVal lpszCurrentPath$)
    Declare Function AddFontResource Lib "GDI" (ByVal lpFilename As Object) As Integer
    Declare Function SendMessage Lib "User" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Object) As Long






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
        CntMnu.Items.Add("Export to Excel", My.Resources.ExportToExcel)
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
        Return value.Trim
    End Function

    'Function GetMainConnectionStr() As String
    '    Dim _ConnectionString As String = ""
    '    If AppConfig.DbType = "2" Then
    '        Return _ConnectionString = "PROVIDER=msdaora;Data Source=" & AppConfig.DbServer & ";User Id=" & AppConfig.DbUname & ";Password=" & AppConfig.DbPass & ""
    '    Else
    '        'Return _ConnectionString = "PROVIDER=sqloledb;DATA SOURCE=" & AppConfig.DbServer & ";UID=" & AppConfig.DbUname & ";PWD=" & AppConfig.DbPass & ";DATABASE=" & AppConfig.DbName & ""
    '        Return _ConnectionString = "PROVIDER=sqloledb;DATA SOURCE=" & AppConfig.DbServer & "," & Val(AppConfig.SQLPort) & ";UID=" & AppConfig.DbUname & ";PWD=" & AppConfig.DbPass & ";DATABASE=" & AppConfig.DbName & ""
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

End Module
