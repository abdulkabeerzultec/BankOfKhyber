Imports System.Runtime.InteropServices
Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.XtraLayout
Imports GenericDAL
Imports System.Xml
Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing

Public Module GlobalFunctions
    Public valRulenotEmpty As New ConditionValidationRule
    Public valRuleGreaterThanZero As New ConditionValidationRule
    Public valRuleGreaterOrEqualZero As New ConditionValidationRule
    Public valRuleNotContainMinus As New ConditionValidationRule

    Private dsAppConfig As New DataSet
    Public Sub LoadContolSettings(ByVal ParentCtl As ZulLib.ctlDataEditing)
        'get data from xml file.
        Try
            Dim Arr As DataRow() = dsAppConfig.Tables("Module").Select("ControlName = '" & ParentCtl.Name & "'")
            If Arr.Length > 0 Then
                Dim dr As DataRow = Arr(0)
                Select Case SelectedLanguage
                    Case TLanguage.English
                        ParentCtl.NewFormCaption = dr("NewFormCaption").ToString
                        ParentCtl.EditFormCaption = dr("EditFormCaption").ToString
                    Case TLanguage.Arabic
                        ParentCtl.NewFormCaption = dr("ArabicNewFormCaption").ToString
                        ParentCtl.EditFormCaption = dr("ArabicEditFormCaption").ToString
                    Case TLanguage.Urdu
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
            ZulLib.Messages.ErrorMessage(ex.Message, WhoCalledMe)

        End Try
    End Sub


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
            ShowErrorMessage("Report not found!, restore all reports from Report Designer first.")
            Return Nothing
        End If
    End Function


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

    Public Function Generate_BarCode(ByVal CompanyID As String, ByVal AstID As String, ByVal AstNum As String, ByVal REF As String, ByVal Cat1 As String, ByVal Cat2 As String, ByVal Loc1 As String, ByVal Loc2 As String) As String
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
                                        If idx2 < 0 Then idx = 0
                                        If strBarCode = "" Then
                                            strBarCode += Trim(AstID.Substring(idx2))
                                        Else
                                            strBarCode += ValueSep + Trim(AstID.Substring(idx2))
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
                                    If idx2 < 0 Then idx = 0
                                    If strBarCode = "" Then
                                        strBarCode += Trim(AstNum.Substring(idx2))
                                    Else
                                        strBarCode += ValueSep + Trim(AstNum.Substring(idx2))
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
                                    If idx2 < 0 Then idx = 0
                                    If strBarCode = "" Then
                                        strBarCode += Trim(REF.Substring(idx))
                                    Else
                                        strBarCode += ValueSep + Trim(REF.Substring(idx))
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
                                str3 = Loc1.Split("\")
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
                                str3 = Loc2.Split("\")
                                If str3.Length > 2 Then

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

        Dim filepath As String = Application.StartupPath & "\SABBApplicationConfig.xml"
        Dim xmlFile As XmlReader

        If System.IO.File.Exists(filepath) Then
            xmlFile = XmlReader.Create(filepath, New XmlReaderSettings())
            dsAppConfig.ReadXml(xmlFile)
        Else
            Throw New Exception("ApplicationConfig file not found in the application path.")
        End If

    End Sub

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

    Dim DbType As String = "1"
    ' In Oracle All fields are uppercase, this is causing problem in the grid.
    Public Function GetGridRowCellDisplayValue(ByVal grv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal RowIndex As Integer, ByVal ColumnName As String) As Object

        If DbType = "1" Then 'SQL Server DBMS
            Return grv.GetRowCellDisplayText(RowIndex, ColumnName)
        Else 'Oracle DBMS
            Return grv.GetRowCellDisplayText(RowIndex, ColumnName.ToUpper)
        End If
    End Function

    Public Function GetGridRowCellValue(ByVal grv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal RowIndex As Integer, ByVal ColumnName As String) As Object

        If DbType = "1" Then 'SQL Server DBMS
            Return grv.GetRowCellValue(RowIndex, ColumnName)
        Else 'Oracle DBMS
            Return grv.GetRowCellValue(RowIndex, ColumnName.ToUpper)
        End If
    End Function

    Public Sub SetGridRowCellValue(ByVal grv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal RowIndex As Integer, ByVal ColumnName As String, ByVal Value As Object)
        If DbType = "1" Then 'SQL Server DBMS
            grv.SetRowCellValue(RowIndex, ColumnName, Value)
        Else 'Oracle DBMS
            grv.SetRowCellValue(RowIndex, ColumnName.ToUpper, Value)
        End If
    End Sub
    Public Function GetGridColumnName(ByVal ColumnName As String) As String
        If DbType = "1" Then 'SQL Server DBMS
            Return ColumnName
        Else 'Oracle DBMS
            Return ColumnName.ToUpper
        End If
    End Function

    Public Function GetGridColumn(ByVal grv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal ColumnName As String) As DevExpress.XtraGrid.Columns.GridColumn
        If DbType = "1" Then 'SQL Server DBMS
            Return grv.Columns(ColumnName)
        Else 'Oracle DBMS
            Return grv.Columns(ColumnName.ToUpper)
        End If
    End Function

    Public Function Generate_AssetNumber(ByVal ComPID As String) As String
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
                        StartRange = CLng(ds.Rows(0)("StartSerial"))
                        EndRange = CLng(ds.Rows(0)("EndSerial"))
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
                        objBALAssetsCoding.CloseRange(objattAssetsCoding)
                        Return -1
                    End If
                Else
                    Return -1
                End If

            End If
        Else 'Use Incremental Coding

            Dim intID As Integer = objBALAssetDetails.GetNextPKey_AssetDetails()
            AstNum = intID + 1
        End If
        Return AstNum
    End Function
    Public Function GenerateAssetID(ByVal CompanyCode As String, ByVal AssetNumber As String, ByVal SubNumber As String) As Long
        Dim AssetTag As String = CompanyCode & AssetNumber & SubNumber
        If AssetTag.Length <= 0 Then
            Return 0
        End If
        Try

            Dim Num As Long = Long.Parse(AssetTag)
            Return Num
        Catch ex As Exception
            Dim sb As New System.Text.StringBuilder
            For Each ch As Char In AssetTag
                If Char.IsDigit(ch) Then
                    sb.Append(ch)
                End If
            Next
            Return Long.Parse(sb.ToString)
        End Try
    End Function

End Module
