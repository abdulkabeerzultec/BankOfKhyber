Imports System.Runtime.InteropServices
Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.XtraLayout
Imports GenericDAL
Imports System.Xml
Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing

Public Module GlobalFunctions
    Public valRulenotEmpty As New ConditionValidationRule
    Public valRuleGreaterThanZero As New ConditionValidationRule
    Public valRuleGreaterOrEqualZero As New ConditionValidationRule
    Public valRuleNotContainMinus As New ConditionValidationRule
    'Public MainForm As Form
    Private dsAppConfig As New DataSet

  

    Public Sub InitVariables()
        valRulenotEmpty.ConditionOperator = ConditionOperator.IsNotBlank
        valRulenotEmpty.ErrorText = My.Resources.valRulenotEmpty
        valRulenotEmpty.ErrorType = ErrorType.Critical

        valRuleGreaterThanZero.ConditionOperator = ConditionOperator.Greater
        valRuleGreaterThanZero.ErrorText = My.Resources.valRuleGreaterThanZero
        valRuleGreaterThanZero.ErrorType = ErrorType.Critical
        valRuleGreaterThanZero.Value1 = 0

        valRuleGreaterOrEqualZero.ConditionOperator = ConditionOperator.GreaterOrEqual
        valRuleGreaterOrEqualZero.Value1 = 0
        valRuleGreaterOrEqualZero.ErrorText = My.Resources.valRuleGreaterOrEqualZero
        valRuleGreaterOrEqualZero.ErrorType = ErrorType.Critical

        valRuleNotContainMinus.ConditionOperator = ConditionOperator.NotContains
        valRuleNotContainMinus.Value1 = "-"
        valRuleNotContainMinus.ErrorText = My.Resources.valRuleNotContainMinus
        valRuleNotContainMinus.ErrorType = ErrorType.Critical
        Dim filepath As String = Application.StartupPath & "\ApplicationConfig.xml"
        Dim xmlFile As XmlReader

        If System.IO.File.Exists(filepath) Then
            xmlFile = XmlReader.Create(filepath, New XmlReaderSettings())
            dsAppConfig.ReadXml(xmlFile)
        Else
            Throw New Exception("ApplicationConfig file not found in the application path.")
        End If
    End Sub

    Public Sub UpdateStatusbarInfo(ByVal CreatedBy As Guid, ByVal CreatedDate As DateTime, ByVal LastEditBY As Guid, ByVal LastEditDate As DateTime, ByVal lblCreated As DevExpress.XtraBars.BarStaticItem, ByVal lblModified As DevExpress.XtraBars.BarStaticItem)
        Dim CreateUser As String = "Unknown"
        Dim LastEditUser As String = "Unknown"
        If CreatedBy <> Guid.Empty Then
            If AppConfigData.IsForeignLanguage Then
                CreateUser = DBOperations.Executer_Scalar("Name", "GUID", CreatedBy.ToString, "AppUser")
            Else
                CreateUser = DBOperations.Executer_Scalar("EngName", "GUID", CreatedBy.ToString, "AppUser")
            End If
        End If

        If LastEditBY <> Guid.Empty Then
            If AppConfigData.IsForeignLanguage Then
                LastEditUser = DBOperations.Executer_Scalar("Name", "GUID", LastEditBY.ToString, "AppUser")
            Else
                LastEditUser = DBOperations.Executer_Scalar("EngName", "GUID", LastEditBY.ToString, "AppUser")
            End If
        End If

        lblCreated.Caption = String.Format("{0} By {1}", CreatedDate.ToString, CreateUser)
        lblModified.Caption = String.Format("{0} By {1}", LastEditDate.ToString, LastEditUser)
    End Sub

    Public Function GetLayoutControl(ByVal ctlName As String, ByVal ParentCtl As ZulLib.ctlDataEditing) As LayoutControlItem
        Try
            For Each ctl As Control In ParentCtl.Controls
                If TypeOf ctl Is LayoutControl Then
                    For Each item As BaseLayoutItem In CType(ctl, LayoutControl).Items
                        If TypeOf item Is LayoutControlItem Then

                            If CType(item, LayoutControlItem).Control IsNot Nothing AndAlso CType(item, LayoutControlItem).Control.Name = ctlName Then
                                Return CType(item, LayoutControlItem)
                            End If
                        End If
                    Next item
                End If
            Next
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub LoadContolSettings(ByVal ParentCtl As ZulLib.ctlDataEditing)
        'get data from xml file.
        Try
            Dim Arr As DataRow() = dsAppConfig.Tables("Module").Select("ControlName = '" & ParentCtl.Name & "'")
            If Arr.Length > 0 Then
                Dim dr As DataRow = Arr(0)
                Select Case AppConfigData.SelectedLanguage
                    Case AppConfigData.TLanguage.English
                        ParentCtl.NewFormCaption = dr("NewFormCaption").ToString
                        ParentCtl.EditFormCaption = dr("EditFormCaption").ToString
                    Case AppConfigData.TLanguage.Arabic
                        ParentCtl.NewFormCaption = dr("ArabicNewFormCaption").ToString
                        ParentCtl.EditFormCaption = dr("ArabicEditFormCaption").ToString
                    Case AppConfigData.TLanguage.Urdu
                        ParentCtl.NewFormCaption = dr("UrduNewFormCaption").ToString
                        ParentCtl.EditFormCaption = dr("UrduEditFormCaption").ToString
                End Select
            End If
            Dim ControlsArr As DataRow() = dsAppConfig.Tables("control").Select("ParentControl = '" & ParentCtl.Name & "'")
            Dim item As LayoutControlItem = Nothing

            For Each dr As DataRow In ControlsArr
                Dim controlName As String = dr("ControlName").ToString
                Dim ChildCtlArray() As Control = ParentCtl.Controls.Find(controlName, True)
                If ChildCtlArray.Length > 0 Then
                    'Get the Layout Item for the control to use it if field is unique or the field don't allow null.
                    If dr("AllowNull").ToString = "No" Or dr("Unique").ToString = "Yes" Or Not String.IsNullOrEmpty(dr("MaxLength")) Then
                        item = GetLayoutControl(controlName, ParentCtl)
                    End If

                    Dim ChildCtl As Control = ChildCtlArray(0)
                    If dr("AllowNull").ToString = "No" Then

                        If TypeOf ChildCtl Is ZulLib.ctlEditValue Then
                            ParentCtl.valProvMain.SetValidationRule(CType(ChildCtl, ZulLib.ctlEditValue).TextBox, valRulenotEmpty)
                        End If
                        If TypeOf ChildCtl Is ZulLib.ctlLov Then
                            ParentCtl.valProvMain.SetValidationRule(CType(ChildCtl, ZulLib.ctlLov).TextBox, valRulenotEmpty)
                        End If

                        If TypeOf ChildCtl Is ZulLib.ctlTreeList Then
                            ParentCtl.valProvMain.SetValidationRule(CType(ChildCtl, ZulLib.ctlTreeList).TextBox, valRulenotEmpty)
                        End If

                        ParentCtl.valProvMain.SetValidationRule(ChildCtl, valRulenotEmpty)

                        If item IsNot Nothing Then
                            'item.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                            item.AppearanceItemCaption.ForeColor = Color.Maroon
                            'item.Image = My.Resources.Required_field
                            item.OptionsToolTip.ToolTip &= "Empty not allowed."
                            item.OptionsToolTip.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
                            item.OptionsToolTip.ToolTipTitle = "Field Constraint"
                        End If
                    End If
                    If dr("Unique").ToString = "Yes" Then
                        If Not String.IsNullOrEmpty(dr("FieldName")) Then
                            If item IsNot Nothing Then
                                item.Image = My.Resources.key
                                item.OptionsToolTip.ToolTip &= " Field value must be unique."
                                item.OptionsToolTip.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
                                item.OptionsToolTip.ToolTipTitle = "Field Constraint"
                            End If
                            If TypeOf ChildCtl Is ZulLib.ctlEditValue Then
                                ParentCtl.UniqueFields.Add(New ZulLib.ctlDataEditing.TUniqueField(dr("FieldName"), CType(ChildCtl, ZulLib.ctlEditValue).TextBox))
                            Else
                                ParentCtl.UniqueFields.Add(New ZulLib.ctlDataEditing.TUniqueField(dr("FieldName"), ChildCtl))
                            End If

                        End If
                    End If


                    If Not String.IsNullOrEmpty(dr("MaxLength")) Then
                        If TypeOf ChildCtl Is DevExpress.XtraEditors.TextEdit Then
                            TryCast(ChildCtl, DevExpress.XtraEditors.TextEdit).Properties.MaxLength = dr("MaxLength")
                            If item IsNot Nothing Then
                                item.OptionsToolTip.ToolTip &= " Max length = " & dr("MaxLength").ToString
                                item.OptionsToolTip.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
                                item.OptionsToolTip.ToolTipTitle = "Field Constraint"
                            End If
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            Messages.ErrorMessage(ex.Message, WhoCalledMe)

        End Try
    End Sub


    'Public Sub UpdateCustomFieldsCaption(ByVal FormName As String, ByVal ParentControl As Control)
    '    Dim objCustomFields As New RouteBLL.CustomFieldsBLL
    '    Dim dt As DataTable = objCustomFields.GetListDataByFormName(FormName)
    '    For Each row As DataRow In dt.Rows
    '        'Get the layout of the control
    '        Dim item As LayoutControlItem = GetLayoutControl(row(objCustomFields.ListDisplayMember), ParentControl)
    '        Select Case AppConfigData.SelectedLanguage
    '            Case AppConfigData.TLanguage.English
    '                item.Text = row("EngCaption")
    '                item.CustomizationFormText = row("EngCaption")
    '            Case AppConfigData.TLanguage.Arabic
    '                item.Text = row("ArabicCaption")
    '                item.CustomizationFormText = row("ArabicCaption")
    '            Case AppConfigData.TLanguage.Urdu
    '                item.Text = row("UrduCaption")
    '                item.CustomizationFormText = row("UrduCaption")
    '            Case Else
    '                item.Text = row("EngCaption")
    '                item.CustomizationFormText = row("EngCaption")
    '        End Select
    '    Next
    'End Sub

    Public Function WhoCalledMe() As String
        'StackFrame:
        'Provides information about the function call on the stack for the current process.

        'StackTrace:
        'Is a collection of all the StackFrames on the stack.

        'MethodBase:
        'Is an abstract class that contains information about currently executing method. 
        Dim st As StackTrace = New StackTrace()
        Dim sf As StackFrame = st.GetFrame(1)
        Dim mb As Reflection.MethodBase = sf.GetMethod()
        Return mb.Name
    End Function

    'Public Sub GenericExceptionHandler(ByVal msg As String, ByVal ErrSource As String, ByVal UserName As String)
    '    If Not Directory.Exists(AppConfig.AppDataFolder  & "\ErrorsReports\") Then
    '        Directory.CreateDirectory(AppConfig.AppDataFolder  & "\ErrorsReports\")
    '    End If

    '    Dim ErrFile As String = AppConfig.AppDataFolder  & "\ErrorsReports\" & "ZulRouteErr.Log"
    '    Dim sr As StreamWriter = File.AppendText(ErrFile)
    '    Dim ErrTxt As String

    '    ErrTxt = "Date  : " & Format(Now, "MMM dd, yyyy HH:mm") & Environment.NewLine & _
    '             "Error : " & msg & Environment.NewLine & _
    '             "Source: " & ErrSource & Environment.NewLine & Environment.NewLine & _
    '             "UserName: " & UserName & Environment.NewLine & Environment.NewLine & _
    '             "-----------------------------------------------------------" & _
    '             "-----------------------------------------------------------" & _
    '             Environment.NewLine & Environment.NewLine

    '    sr.WriteLine(ErrTxt)
    '    sr.Close()
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

    Public Function IsGuid(ByVal guidVal As Object) As Boolean
        Try
            If IsDBNull(guidVal) Then
                Return False
            End If

            Dim g As Guid = New Guid(guidVal.ToString)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
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
