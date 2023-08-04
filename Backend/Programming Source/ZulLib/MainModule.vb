Imports System.IO
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

Public Module MainModule


    Public ISRegistered As Boolean
    'Public strRoleID As String

    Public valRulenotEmpty As New ConditionValidationRule
    Public valRuleGreaterThanZero As New ConditionValidationRule
    Public valRuleGreaterOrEqualZero As New ConditionValidationRule
    Public valRuleNotContainMinus As New ConditionValidationRule


    Public NumberMask As String = "###,###,###,##0.00"
    Public NumberMaskType As DevExpress.XtraEditors.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
    'Public MainForm As frmMain
    Public CompanyLogoImage As Image

    Public Function ShowSaveConfirmation() As DialogResult
        Return XtraMessageBox.Show(My.Resources.Strings.SaveConfirmation, Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
    End Function


    Public Function ShowPrintConfirmation() As DialogResult
        Return XtraMessageBox.Show(My.Resources.Strings.PrintConfirmation, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
    End Function

    Public Function ShowCancelConfirmation() As DialogResult
        Return XtraMessageBox.Show(My.Resources.Strings.CancelConfirmation, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
    End Function

    Public Function ShowDeleteConfirmation() As DialogResult
        Return XtraMessageBox.Show(My.Resources.Strings.DeleteConfirmation, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
    End Function

    Public Sub ShowCanNotDelete()
        XtraMessageBox.Show(My.Resources.Strings.CanNotDelete, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
    End Sub

    Public Sub ShowCanNotSave()
        XtraMessageBox.Show(My.Resources.Strings.CanNotSave, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
    End Sub

    Public Sub ShowRecordNotFound()
        XtraMessageBox.Show(My.Resources.Strings.RecordNotFound, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
    End Sub

    Public Sub ShowSaveParent()
        XtraMessageBox.Show(My.Resources.Strings.SaveParentFirst, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
    End Sub


    Public Function OpenTabForm(ByVal controlName As String, ByVal NewCaption As String, ByVal EditCaption As String, ByVal ParentTabPageName As String) As Control

        Dim page As New DevExpress.XtraTab.XtraTabPage

        'MainForm.tabControlMain.TabPages.Add(page)

        Dim ctlName = controlName
        ctlName = [Assembly].GetEntryAssembly.GetName.Name & "." & ctlName
        Dim ctl As ctlDataEditing
        ctl = DirectCast([Assembly].GetEntryAssembly.CreateInstance(ctlName), Control)
        ctl.Tag = ParentTabPageName
        ctl.NewFormCaption = NewCaption
        ctl.EditFormCaption = EditCaption

        page.Controls.Add(ctl)

        ctl.Dock = DockStyle.Fill
        'MainForm.tabControlMain.SelectedTabPage = page
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
            ElseIf TypeOf (ctl) Is ctlGridDataEditing Then
                CType(ctl, ctlGridDataEditing).DataSource = Nothing
            ElseIf TypeOf (ctl) Is LookUpEdit Then
                CType(ctl, LookUpEdit).EditValue = Nothing
            ElseIf TypeOf (ctl) Is TextEdit Then
                ctl.Text = Nothing
            ElseIf TypeOf (ctl) Is PictureEdit Then
                CType(ctl, PictureEdit).EditValue = Nothing
            ElseIf TypeOf (ctl) Is CheckEdit Then
                CType(ctl, CheckEdit).EditValue = Nothing
            ElseIf TypeOf (ctl) Is ctlLov Then
                CType(ctl, ctlLov).SelectedValue = Nothing
                CType(ctl, ctlLov).SelectedText = Nothing
            ElseIf TypeOf (ctl) Is ctlTreeList Then
                CType(ctl, ctlTreeList).SelectedValue = Nothing
                CType(ctl, ctlTreeList).FullPathText = Nothing
                CType(ctl, ctlTreeList).SelectedNodeText = Nothing
            End If
            If ctl.HasChildren() Then
                ClearControl(ctl)
            End If
        Next
    End Sub

    'Public Sub ClearFrame(ByVal FrameName As Object)
    '    Dim ctl As Object
    '    For Each ctl In FrameName.Controls
    '        If TypeOf (ctl) Is TextBox Then
    '            ctl.Text = ""
    '        ElseIf TypeOf (ctl) Is DateTimePicker Then

    '            ctl.value = DateTime.Now
    '        ElseIf TypeOf (ctl) Is CheckBox Then

    '            ctl.Checked = 0
    '        ElseIf TypeOf (ctl) Is DevExpress.XtraEditors.TextEdit Then
    '            ctl.Text = ""
    '        ElseIf TypeOf (ctl) Is ComboBoxEdit Then

    '            ctl.SelectedIndex = 0
    '        ElseIf TypeOf (ctl) Is Label Then
    '            If ctl.Tag <> "" Then
    '                ctl.Text = ""
    '            End If
    '        End If

    '        If ctl.TabIndex = 0 Then ctl.Focus()
    '    Next
    'End Sub

    'Public Sub ClearTab(ByVal TabControl As Object)
    '    Dim ctl As Object

    '    For Each ctl In TabControl.Controls
    '        If TypeOf (ctl) Is TextBox Then
    '            ctl.Text = ""
    '        ElseIf TypeOf (ctl) Is DateTimePicker Then
    '            ctl.value = DateTime.Now
    '        ElseIf TypeOf (ctl) Is CheckBox Then
    '            ctl.Checked = 0
    '        ElseIf TypeOf (ctl) Is Label Then
    '            If ctl.Tag <> "" Then
    '                ctl.Text = ""
    '            End If
    '        End If
    '        If ctl.TabIndex = 0 Then ctl.Focus()
    '    Next
    'End Sub
    Public Function TryStrToGuid(ByVal s As String, ByRef value As Guid) As Boolean
        Try
            value = New Guid(s)
            Return True
        Catch ex As Exception
            value = Guid.Empty
            Return False
        End Try
    End Function

    Public Function IsNullOrEmptyValue(ByVal editValue As Object) As Boolean
        If ((editValue Is Nothing) OrElse TypeOf editValue Is DBNull) Then
            Return True
        End If
        If TypeOf editValue Is String AndAlso String.IsNullOrEmpty(editValue) Then
            Return True
        End If
        Return False
    End Function

    Public Function GetDisplayTextByKeyValue(ByVal table As DataTable, ByVal keyValue As Object, ByVal KeyFieldName As String, ByVal DisplayFieldName As String) As Object
        If Not IsNullOrEmptyValue(keyValue) Then
            Dim arr As DataRow() = table.Select(String.Format("{0} = '{1}'", KeyFieldName, keyValue))
            If arr.Length > 0 Then
                Return arr(0)(DisplayFieldName)
            Else
                Return String.Empty
            End If
        Else
            Return String.Empty
        End If
    End Function



End Module
