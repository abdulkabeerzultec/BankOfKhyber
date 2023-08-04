Imports DevExpress.XtraGrid
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Windows.Forms

Public Class frmImport
    Dim ObjIntegration As New BALABBIntegration

    Public Enum TFileImportType
        AssetCreate
        AssetChange
        AssetRetirment
        AssetWithValue
        AssetCapitalization
    End Enum

    Private dtAssetCreation As New DataTable
    Private dtAssetChange As New DataTable
    Private dtAssetRetirement As New DataTable
    Private dtAssetCapitalization As New DataTable
    Private dtAssetWithValue As New DataTable

    Private _ImportFileName As String
    Private _ImportType As New TFileImportType

    Dim SuccessMessage As String = "Success."
    Dim ExistMessage As String = "AssetTag Already Exist. Ignored"
    Dim AssetTagNotExistMessage As String = "AssetTag not exist. Ignored"
    Structure AssetLocation
        Dim PlantCode As String
        Dim LocationCode As String
        Dim PlantName As String
        Dim LocationName As String
    End Structure

    Public Property ImportType() As TFileImportType
        Get
            Return _ImportType
        End Get
        Set(ByVal value As TFileImportType)
            _ImportType = value
        End Set
    End Property

    Public Property ImportFileName() As String
        Get
            Return _ImportFileName
        End Get
        Set(ByVal value As String)
            _ImportFileName = value
        End Set
    End Property

    Public Function SilentImport(ByVal lbl As DevExpress.XtraEditors.LabelControl, ByVal pb As DevExpress.XtraEditors.ProgressBarControl, ByVal FileName As String, ByVal ImportType As frmImport.TFileImportType) As String
        Select Case ImportType
            Case TFileImportType.AssetCreate
                lbl.Text = "Processing Data Asset Master Creation file..."
                lbl.Update()
                If CheckFile(FileName) Then
                    CreateAssetCreationDataTable()
                    ProcessAssetCreationFile(FileName, True, pb)
                    Application.DoEvents()
                    lbl.Text = "Importing Asset Master Creation file..."
                    lbl.Update()
                    Return ImportAssetCreation(True, pb)
                Else
                    Dim msg As String = String.Format("File not found, Incorrect path({0})", FileName)
                    SaveToLogFile(msg, True)
                    lbl.Text = msg
                    lbl.Update()
                    Return msg
                End If
            Case TFileImportType.AssetChange
                lbl.Text = "Processing Data  Asset Master Change file..."
                lbl.Update()
                If CheckFile(FileName) Then
                    CreateAssetChangeDataTable()
                    ProcessAssetChangeFile(FileName, True, pb)
                    Application.DoEvents()
                    lbl.Text = "Importing Asset Master Change file..."
                    lbl.Update()
                    Return ImportAssetChange(True, pb)
                Else
                    Dim msg As String = String.Format("File not found, Incorrect path({0})", FileName)
                    SaveToLogFile(msg, True)
                    lbl.Text = msg
                    lbl.Update()
                    Return msg
                End If

            Case TFileImportType.AssetRetirment
                lbl.Text = "Processing Data Asset Retirement file..."
                lbl.Update()
                If CheckFile(FileName) Then
                    CreateAssetRetirementDataTable()
                    ProcessAssetRetirementFile(FileName, True, pb)
                    Application.DoEvents()
                    lbl.Text = "Importing Asset Retirement file..."
                    lbl.Update()
                    Return ImportAssetRetirement(True, pb)
                Else
                    Dim msg As String = String.Format("File not found, Incorrect path({0})", FileName)
                    SaveToLogFile(msg, True)
                    lbl.Text = msg
                    lbl.Update()
                    Return msg
                End If

            Case TFileImportType.AssetCapitalization
                lbl.Text = "Processing Data Asset Capitalization file..."
                lbl.Update()
                If CheckFile(FileName) Then
                    CreateAssetCapitalizationDataTable()
                    ProcessAssetCapitalizationFile(FileName, True, pb)
                    Application.DoEvents()
                    lbl.Text = "Importing Asset Capitalization file..."
                    lbl.Update()
                    Return ImportAssetCapitalization(True, pb)
                Else
                    Dim msg As String = String.Format("File not found, Incorrect path({0})", FileName)
                    SaveToLogFile(msg, True)
                    lbl.Text = msg
                    lbl.Update()
                    Return msg
                End If
            Case TFileImportType.AssetWithValue
                lbl.Text = "Processing Data Assets with Value file..."
                lbl.Update()
                If CheckFile(FileName) Then
                    CreateAssetWithValueDataTable()
                    ProcessAssetWithValueFile(FileName, True, pb)
                    Application.DoEvents()
                    lbl.Text = "Importing Assets with Value file..."
                    lbl.Update()
                    Return ImportAssetWithValue(True, pb)
                Else
                    Dim msg As String = String.Format("File not found, Incorrect path({0})", FileName)
                    SaveToLogFile(msg, True)
                    lbl.Text = msg
                    lbl.Update()
                    Return msg
                End If
            Case Else
                Return String.Empty
        End Select
    End Function

    Private Sub frmImport_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Select Case ImportType
            Case TFileImportType.AssetCreate
                CreateAssetCreationDataTable()
                If ProcessAssetCreationFile(ImportFileName, False, pb) Then
                    FormatAssetGrid()
                    Me.BringToFront()
                Else
                    Me.Close()
                End If
            Case TFileImportType.AssetChange
                CreateAssetChangeDataTable()
                If ProcessAssetChangeFile(ImportFileName, False, pb) Then
                    FormatAssetGrid()
                    Me.BringToFront()
                Else
                    Me.Close()
                End If
            Case TFileImportType.AssetRetirment
                CreateAssetRetirementDataTable()
                If ProcessAssetRetirementFile(ImportFileName, False, pb) Then
                    FormatAssetValuesGrid()
                    Me.BringToFront()
                Else
                    Me.Close()
                End If
            Case TFileImportType.AssetCapitalization
                CreateAssetCapitalizationDataTable()
                If ProcessAssetCapitalizationFile(ImportFileName, False, pb) Then
                    FormatAssetValuesGrid()
                    Me.BringToFront()
                Else
                    Me.Close()
                End If
            Case TFileImportType.AssetWithValue
                CreateAssetWithValueDataTable()
                If ProcessAssetWithValueFile(ImportFileName, False, pb) Then
                    FormatAssetValuesGrid()
                    Me.BringToFront()
                Else
                    Me.Close()
                End If
        End Select
    End Sub

    Private Sub frmImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub FormatAssetGrid()
        grdView.OptionsBehavior.Editable = True
        'Disallow editing for all the fields
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In grdView.Columns
            col.OptionsColumn.AllowEdit = False
        Next
        grdView.Columns("Selection").Caption = ""
        grdView.Columns("Selection").MaxWidth = 20
        'allow editing for selection field.
        grdView.Columns("Selection").OptionsColumn.AllowEdit = True

        grdView.Columns("RowNumber").Caption = "#"
        grdView.Columns("RowNumber").MaxWidth = 35


        grdView.Columns("EvaluationGroup1").Visible = False
        grdView.Columns("EvaluationGroup2").Visible = False
        grdView.Columns("EvaluationGroup3").Visible = False
        grdView.Columns("EvaluationGroup4").Visible = False


        grdView.Columns("Message").Visible = False
        'grdView.BestFitColumns()

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

        Dim styleFormatCondition1 As StyleFormatCondition = New StyleFormatCondition()
        styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.OrangeRed
        styleFormatCondition1.Appearance.Options.UseBackColor = True
        styleFormatCondition1.ApplyToRow = True
        styleFormatCondition1.Column = grdView.Columns("Status")
        styleFormatCondition1.Condition = FormatConditionEnum.NotEqual
        styleFormatCondition1.Value1 = SuccessMessage
        grdView.FormatConditions.Add(styleFormatCondition1)
        Dim styleFormatCondition2 As StyleFormatCondition = New StyleFormatCondition()
        styleFormatCondition2.Appearance.BackColor = System.Drawing.Color.Aqua
        styleFormatCondition2.ApplyToRow = True
        styleFormatCondition2.Appearance.Options.UseBackColor = True
        styleFormatCondition2.Column = grdView.Columns("Status")
        styleFormatCondition2.Condition = FormatConditionEnum.Equal
        styleFormatCondition2.Value1 = AssetTagNotExistMessage
        grdView.FormatConditions.Add(styleFormatCondition2)

        Dim styleFormatCondition3 As StyleFormatCondition = New StyleFormatCondition()
        styleFormatCondition3.Appearance.BackColor = System.Drawing.Color.Silver
        styleFormatCondition3.ApplyToRow = True
        styleFormatCondition3.Appearance.Options.UseBackColor = True
        styleFormatCondition3.Column = grdView.Columns("Status")
        styleFormatCondition3.Condition = FormatConditionEnum.Equal
        styleFormatCondition3.Value1 = String.Format("{0}", ExistMessage)
        grdView.FormatConditions.Add(styleFormatCondition3)
    End Sub

    Private Sub FormatAssetValuesGrid()
        grdView.OptionsBehavior.Editable = True
        'Disallow editing for all the fields
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In grdView.Columns
            col.OptionsColumn.AllowEdit = False
        Next
        grdView.Columns("Selection").Caption = ""
        grdView.Columns("Selection").MaxWidth = 20
        'allow editing for selection field.
        grdView.Columns("Selection").OptionsColumn.AllowEdit = True

        grdView.Columns("RowNumber").Caption = "#"
        grdView.Columns("RowNumber").MaxWidth = 35

        grdView.Columns("Message").Visible = False
        'grdView.BestFitColumns()

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

        Dim styleFormatCondition1 As StyleFormatCondition = New StyleFormatCondition()
        styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.OrangeRed
        styleFormatCondition1.Appearance.Options.UseBackColor = True
        styleFormatCondition1.ApplyToRow = True
        styleFormatCondition1.Column = grdView.Columns("Status")
        styleFormatCondition1.Condition = FormatConditionEnum.NotEqual
        styleFormatCondition1.Value1 = SuccessMessage
        grdView.FormatConditions.Add(styleFormatCondition1)
        Dim styleFormatCondition2 As StyleFormatCondition = New StyleFormatCondition()
        styleFormatCondition2.Appearance.BackColor = System.Drawing.Color.Aqua
        styleFormatCondition2.ApplyToRow = True
        styleFormatCondition2.Appearance.Options.UseBackColor = True
        styleFormatCondition2.Column = grdView.Columns("Status")
        styleFormatCondition2.Condition = FormatConditionEnum.Equal
        styleFormatCondition2.Value1 = AssetTagNotExistMessage
        grdView.FormatConditions.Add(styleFormatCondition2)

        Dim styleFormatCondition3 As StyleFormatCondition = New StyleFormatCondition()
        styleFormatCondition3.Appearance.BackColor = System.Drawing.Color.Silver
        styleFormatCondition3.ApplyToRow = True
        styleFormatCondition3.Appearance.Options.UseBackColor = True
        styleFormatCondition3.Column = grdView.Columns("Status")
        styleFormatCondition3.Condition = FormatConditionEnum.Equal
        styleFormatCondition3.Value1 = String.Format("{0}", ExistMessage)
        grdView.FormatConditions.Add(styleFormatCondition3)
    End Sub

    Private Function GetFileLineCount(ByVal file As String) As Integer
        Dim array As String() = System.IO.File.ReadAllLines(file)
        Return array.Length
    End Function

    Private Function CheckExistRecord(ByVal AssetDataTable As DataTable, ByVal assetTag As String, ByVal ImportDataRow As DataRow) As String
        Dim Message As String = ""
        Dim Rows As DataRow() = AssetDataTable.Select("AstID = '" & assetTag & "'")
        Dim FoundRow As DataRow
        If Rows.Length > 0 Then
            FoundRow = Rows(0)
            Dim FulLoc As String = FoundRow("LocationFullPath")
            Dim strFulLoc As String() = FulLoc.Split("\")
            Dim FulCat As String = FoundRow("CatFullPath")
            Dim strFulCat As String() = FulCat.Split("\")
            Dim CityName As String = ""
            Dim BuildingName As String = ""
            Dim FloorName As String = ""
            Dim RoomName As String = ""
            Dim MainCategory As String = ""
            Dim SubCategory As String = ""
            Dim astDesc As String = FoundRow("Assetdetailsdesc1").ToString.Replace("'", "")
            Dim AstModel As String = FoundRow("AstModel")
            Dim SerailNo As String = FoundRow("SerailNo")

            CityName = strFulLoc(0).Trim
            If strFulLoc.Length > 1 Then
                BuildingName = strFulLoc(1).Trim
            End If

            If strFulLoc.Length > 2 Then
                FloorName = strFulLoc(2).Trim
            End If

            If strFulLoc.Length > 3 Then
                RoomName = strFulLoc(3).Trim
            End If
            ImportDataRow("AssetDescription") = ImportDataRow("AssetDescription").ToString.Replace("'", "")
            MainCategory = strFulCat(0).Trim
            SubCategory = strFulCat(1).Trim
            If astDesc.ToUpper <> ImportDataRow("AssetDescription").ToString.ToUpper Then
                Message = "AssetDescription,"
            End If
            If AstModel.ToUpper <> ImportDataRow("Model").ToString.ToUpper Then
                Message += "Model,"
            End If
            If SerailNo.ToUpper <> ImportDataRow("Serial").ToString.ToUpper Then
                Message += "Serial,"
            End If
            If MainCategory.ToUpper <> ImportDataRow("MainCategory").ToString.ToUpper Then
                Message += "MainCategory,"
            End If
            If SubCategory.ToUpper <> ImportDataRow("SubCategory").ToString.ToUpper Then
                Message += "SubCategory,"
            End If
            If CityName.ToUpper <> ImportDataRow("CityName").ToString.ToUpper Then
                Message += "CityName,"
            End If
            If BuildingName.ToUpper <> ImportDataRow("BuildingName").ToString.ToUpper Then
                Message += "BuildingName,"
            End If
            If FloorName.ToUpper <> ImportDataRow("FloorName").ToString.ToUpper Then
                Message += "FloorName,"
            End If
            If RoomName.ToUpper <> ImportDataRow("RoomName").ToString.ToUpper Then
                Message += "RoomName,"
            End If
            If Message.Length > 0 Then
                Message = Message.Remove(Message.Length - 1, 1)
                Message = String.Format("Difference in({0})", Message)
            End If
            Return Message
        Else
            Return ""
        End If
    End Function
    Private Sub CreateAssetChangeDataTable()
        dtAssetChange.Columns.Add("Selection", Type.GetType("System.Boolean"))
        dtAssetChange.Columns.Add("RowNumber", Type.GetType("System.Int32"))

        dtAssetChange.Columns.Add("AssetNumber", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("SubNumber", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("CompanyCode", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("AssetClass", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("AssetDescription1", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("AssetDescription2", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("RefNumber", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("InventoryNumber", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("SerialNumber", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("BusinessArea", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("CostCenter", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("Location", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("Plant", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("EmployeeNumber", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("EvaluationGroup1", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("EvaluationGroup2", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("EvaluationGroup3", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("EvaluationGroup4", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("UsefulLife", Type.GetType("System.String"))

        dtAssetChange.Columns.Add("Status", Type.GetType("System.String"))
        dtAssetChange.Columns.Add("Message", Type.GetType("System.String"))
    End Sub

    Private Sub CreateAssetCreationDataTable()
        dtAssetCreation.Columns.Add("Selection", Type.GetType("System.Boolean"))
        dtAssetCreation.Columns.Add("RowNumber", Type.GetType("System.Int32"))

        dtAssetCreation.Columns.Add("AssetNumber", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("SubNumber", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("CompanyCode", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("AssetClass", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("AssetDescription1", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("AssetDescription2", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("RefNumber", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("InventoryNumber", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("CreationDate", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("CreatedBy", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("SerialNumber", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("CapitalizationDate", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("BusinessArea", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("CostCenter", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("Location", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("Plant", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("EmployeeNumber", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("EvaluationGroup1", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("EvaluationGroup2", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("EvaluationGroup3", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("EvaluationGroup4", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("UsefulLife", Type.GetType("System.String"))

        dtAssetCreation.Columns.Add("Status", Type.GetType("System.String"))
        dtAssetCreation.Columns.Add("Message", Type.GetType("System.String"))
    End Sub

    Private Sub CreateAssetRetirementDataTable()
        dtAssetRetirement.Columns.Add("Selection", Type.GetType("System.Boolean"))
        dtAssetRetirement.Columns.Add("RowNumber", Type.GetType("System.Int32"))

        dtAssetRetirement.Columns.Add("AssetNumber", Type.GetType("System.String"))
        dtAssetRetirement.Columns.Add("SubNumber", Type.GetType("System.String"))
        dtAssetRetirement.Columns.Add("CompanyCode", Type.GetType("System.String"))
        dtAssetRetirement.Columns.Add("RetirementDate", Type.GetType("System.String"))

        dtAssetRetirement.Columns.Add("Status", Type.GetType("System.String"))
        dtAssetRetirement.Columns.Add("Message", Type.GetType("System.String"))
    End Sub

    Private Sub CreateAssetCapitalizationDataTable()
        dtAssetCapitalization.Columns.Add("Selection", Type.GetType("System.Boolean"))
        dtAssetCapitalization.Columns.Add("RowNumber", Type.GetType("System.Int32"))

        dtAssetCapitalization.Columns.Add("AssetNumber", Type.GetType("System.String"))
        dtAssetCapitalization.Columns.Add("SubNumber", Type.GetType("System.String"))
        dtAssetCapitalization.Columns.Add("CompanyCode", Type.GetType("System.String"))
        dtAssetCapitalization.Columns.Add("CapitalizationDate", Type.GetType("System.String"))

        dtAssetCapitalization.Columns.Add("Status", Type.GetType("System.String"))
        dtAssetCapitalization.Columns.Add("Message", Type.GetType("System.String"))
    End Sub

    Private Sub CreateAssetWithValueDataTable()
        dtAssetWithValue.Columns.Add("Selection", Type.GetType("System.Boolean"))
        dtAssetWithValue.Columns.Add("RowNumber", Type.GetType("System.Int32"))

        dtAssetWithValue.Columns.Add("AssetNumber", Type.GetType("System.String"))
        dtAssetWithValue.Columns.Add("SubNumber", Type.GetType("System.String"))
        dtAssetWithValue.Columns.Add("CompanyCode", Type.GetType("System.String"))

        dtAssetWithValue.Columns.Add("Status", Type.GetType("System.String"))
        dtAssetWithValue.Columns.Add("Message", Type.GetType("System.String"))
    End Sub

    Private Function ProcessAssetCreationFile(ByVal file As String, ByVal SilentProcess As Boolean, ByVal ProgBar As DevExpress.XtraEditors.ProgressBarControl) As Boolean
        ProgBar.Visible = True
        ProgBar.Position = 1
        ProgBar.Properties.Step = 1
        ProgBar.Update()
        ProgBar.Properties.Maximum = GetFileLineCount(file)
        Application.DoEvents()
        Dim hTable As Hashtable = New Hashtable()
        Dim objBALAssetDetails As New BALAssetDetails
        Dim RowNumber As Integer = 1
        Dim AssetDataTable As DataTable = objBALAssetDetails.GetAsset_Details(New attAssetDetails)
        Dim MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(file, System.Text.Encoding.GetEncoding("windows-1256"))
        Try
            'Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(file, System.Text.Encoding.GetEncoding("windows-1256"))
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(Chr(Keys.Tab))
            MyReader.HasFieldsEnclosedInQuotes = False ' to remove error "Line # cannot be parsed using the current Delimiters"
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Dim ImportRow As DataRow = dtAssetCreation.Rows.Add()
                Try
                    currentRow = MyReader.ReadFields()
                    ' if the count of the fields is less than 14 then we need to skip the row.
                    If currentRow.Length <> 22 Then
                        ImportRow("Status") = ("Line (" & RowNumber & ") is invalid and will be skipped.")
                    Else
                        ImportRow("RowNumber") = RowNumber
                        ImportRow("Status") = SuccessMessage

                        Dim AssetNumber As String = currentRow(0).ToString.Trim
                        Dim AssetSubNumber As String = currentRow(1).ToString.Trim
                        Dim ComapanyCode As String = currentRow(2).ToString.Trim

                        Dim AssetTag As String = GenerateAssetID(ComapanyCode, AssetNumber, AssetSubNumber)

                        If String.IsNullOrEmpty(AssetNumber) Then
                            ImportRow("Status") = "AssetNumber is empty."
                        Else
                            ' check for doublicated AssetTag
                            'Add list of all the unique item value to hashtable, which stores combination of key, value pair.
                            'And add duplicate item value in arraylist.
                            If hTable.Contains(AssetTag) Then
                                'Locate all rows, and mark them as duplicated.
                                'because we should reject All dublicated asset tags.
                                Dim dra As DataRow() = dtAssetCreation.Select("AssetNumber+SubNumber+CompanyCode = '" & AssetNumber & AssetSubNumber & ComapanyCode & "'")
                                For Each row As DataRow In dra
                                    row("Status") = "AssetNumber duplicated."
                                    'unSelect the old dublicated rows.
                                    row("Selection") = False
                                Next
                                ImportRow("Status") = "AssetNumber duplicated."
                            Else
                                hTable.Add(AssetTag, String.Empty)
                            End If
                        End If

                        ImportRow("AssetNumber") = AssetNumber

                        ImportRow("SubNumber") = AssetSubNumber

                        ImportRow("CompanyCode") = ComapanyCode
                        If String.IsNullOrEmpty(ImportRow("CompanyCode")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CompanyCode is Empty.")
                        ElseIf ImportRow("AssetClass").ToString.Length > 40 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CompanyCode > 40.")
                        End If

                        ImportRow("AssetClass") = currentRow(3).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("AssetClass")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "AssetClass is Empty.")
                        ElseIf ImportRow("AssetClass").ToString.Length > 40 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "AssetClass > 40.")
                        End If

                        ImportRow("AssetDescription1") = currentRow(4).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("AssetDescription1")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "AssetDescription1 is Empty.")
                        ElseIf ImportRow("AssetDescription1").ToString.Length > 80 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "AssetDescription1 > 80.")
                        End If

                        ImportRow("AssetDescription2") = currentRow(5).ToString.Trim
                        If ImportRow("AssetDescription2").ToString.Length > 80 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "AssetDescription2 > 80.")
                        End If

                        ImportRow("RefNumber") = currentRow(6).ToString.Trim
                       If ImportRow("RefNumber").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "RefNumber > 50.")
                        End If

                        ImportRow("InventoryNumber") = currentRow(7).ToString.Trim
                        If ImportRow("InventoryNumber").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "InventoryNumber > 50.")
                        End If

                        ImportRow("CreationDate") = currentRow(8).ToString.Trim
                        If Not CheckConvertDate(ImportRow("CreationDate").ToString) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Date of Creation is invalid.")
                        End If

                        ImportRow("CreatedBy") = currentRow(9).ToString.Trim
                        If ImportRow("CreatedBy").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CreatedBy > 50.")
                        End If

                        ImportRow("SerialNumber") = currentRow(10).ToString.Trim
                        If ImportRow("SerialNumber").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "SerialNumber > 50.")
                        End If

                        ImportRow("CapitalizationDate") = currentRow(11).ToString.Trim

                        ImportRow("BusinessArea") = currentRow(12).ToString.Trim
                        If ImportRow("BusinessArea").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "BusinessArea > 50.")
                        End If

                        ImportRow("CostCenter") = currentRow(13).ToString.Trim
                        If ImportRow("CostCenter").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CostCenter > 50.")
                        End If


                        ImportRow("Location") = currentRow(14).ToString.Trim
                        If ImportRow("Location").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Location > 50.")
                        End If

                        ImportRow("Plant") = currentRow(15).ToString.Trim
                        If ImportRow("Plant").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Plant > 50.")
                        End If

                        ImportRow("EmployeeNumber") = currentRow(16).ToString.Trim
                        If ImportRow("EmployeeNumber").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "EmployeeNumber > 50.")
                        End If

                        ImportRow("EvaluationGroup1") = currentRow(17).ToString.Trim
                        If ImportRow("EvaluationGroup1").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "EvaluationGroup1 > 50.")
                        End If
                        ImportRow("EvaluationGroup2") = currentRow(18).ToString.Trim
                        If ImportRow("EvaluationGroup2").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "EvaluationGroup2 > 50.")
                        End If
                        ImportRow("EvaluationGroup3") = currentRow(19).ToString.Trim
                        If ImportRow("EvaluationGroup3").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "EvaluationGroup3 > 50.")
                        End If
                        ImportRow("EvaluationGroup4") = currentRow(20).ToString.Trim
                        If ImportRow("EvaluationGroup4").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "EvaluationGroup4 > 50.")
                        End If

                        ImportRow("UsefulLife") = currentRow(21).ToString.Trim
                        If Not Integer.TryParse(ImportRow("UsefulLife"), -1) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "UsefulLife invalid.")
                        End If


                        'Check if record exist in the database if the record pass the validation.
                        If ImportRow("Status").ToString = SuccessMessage Then
                            If objBALAssetDetails.Check_AstID(AssetTag, True) Then
                                ImportRow("Status") = String.Format("{0}", ExistMessage)
                            End If
                        End If

                    End If
                    If ImportRow("Status").ToString = SuccessMessage Then
                        ImportRow("Selection") = True
                    Else
                        ImportRow("Selection") = False
                    End If
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    ImportRow("Status") = ("Line (" & RowNumber & ") is invalid and will be skipped.")
                End Try
                ImportRow.AcceptChanges()
                RowNumber += 1

                ProgBar.PerformStep()
                ProgBar.Update()
            End While
            'End Using
            dtAssetCreation.AcceptChanges()

            ''''
            grd.DataSource = dtAssetCreation
            Dim TotalRowsCount As Integer = grdView.RowCount
            Dim ValidRowsCount As Integer = GetGridValidRows(grdView)
            Dim ExistIgroredCount As Integer = GetGridExistIgnoredRows(grdView)
            lblTotal.Text = String.Format("Total: {0}", TotalRowsCount)
            lblValid.Text = String.Format("Valid: {0}", ValidRowsCount)
            lblExistIgnore.Text = String.Format("Ignored: {0}", ExistIgroredCount)
            lblInvalid.Text = String.Format("Invalid: {0}", TotalRowsCount - ValidRowsCount - ExistIgroredCount)
            Return True
        Catch ex As Exception
            If SilentProcess Then
                SaveToLogFile(ex.Message, True)
            Else
                MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        Finally
            MyReader.Close()
            MyReader.Dispose()
            hTable.Clear()
            ProgBar.Visible = False
        End Try
    End Function

    Private Function ProcessAssetChangeFile(ByVal file As String, ByVal SilentProcess As Boolean, ByVal ProgBar As DevExpress.XtraEditors.ProgressBarControl) As Boolean
        ProgBar.Visible = True
        ProgBar.Position = 1
        ProgBar.Properties.Step = 1
        ProgBar.Refresh()
        ProgBar.Properties.Maximum = GetFileLineCount(file)
        Application.DoEvents()
        Dim hTable As Hashtable = New Hashtable()
        Dim objBALAssetDetails As New BALAssetDetails
        Dim RowNumber As Integer = 1
        Dim AssetDataTable As DataTable = objBALAssetDetails.GetAsset_Details(New attAssetDetails)
        Dim MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(file, System.Text.Encoding.GetEncoding("windows-1256"))
        Try
            'Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(file, System.Text.Encoding.GetEncoding("windows-1256"))
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(Chr(Keys.Tab))
            MyReader.HasFieldsEnclosedInQuotes = False ' to remove error "Line # cannot be parsed using the current Delimiters"
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Dim ImportRow As DataRow = dtAssetChange.Rows.Add()
                Try
                    currentRow = MyReader.ReadFields()
                    ' if the count of the fields is less than 14 then we need to skip the row.
                    If currentRow.Length <> 19 Then
                        ImportRow("Status") = ("Line (" & RowNumber & ") is invalid and will be skipped.")
                    Else
                        ImportRow("RowNumber") = RowNumber
                        ImportRow("Status") = SuccessMessage

                        Dim AssetNumber As String = currentRow(0).ToString.Trim
                        Dim AssetSubNumber As String = currentRow(1).ToString.Trim
                        Dim ComapanyCode As String = currentRow(2).ToString.Trim

                        Dim AssetTag As String = GenerateAssetID(ComapanyCode, AssetNumber, AssetSubNumber)

                        If String.IsNullOrEmpty(AssetNumber) Then
                            ImportRow("Status") = "AssetNumber is empty."
                        Else
                            ' check for doublicated AssetTag
                            'Add list of all the unique item value to hashtable, which stores combination of key, value pair.
                            'And add duplicate item value in arraylist.
                            If hTable.Contains(AssetTag) Then
                                'Locate all rows, and mark them as duplicated.
                                'because we should regect All dublicated asset tags.
                                Dim dra As DataRow() = dtAssetCreation.Select("AssetNumber+SubNumber+CompanyCode = '" & AssetNumber & AssetSubNumber & ComapanyCode & "'")

                                For Each row As DataRow In dra
                                    row("Status") = "AssetNumber duplicated."
                                    'unSelect the old dublicated rows.
                                    row("Selection") = False
                                Next
                                ImportRow("Status") = "AssetNumber duplicated."
                            Else
                                hTable.Add(AssetTag, String.Empty)
                            End If
                        End If

                        ImportRow("AssetNumber") = AssetNumber

                        ImportRow("SubNumber") = AssetSubNumber

                        ImportRow("CompanyCode") = ComapanyCode
                        If String.IsNullOrEmpty(ImportRow("CompanyCode")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CompanyCode is Empty.")
                        ElseIf ImportRow("CompanyCode").ToString.Length > 40 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CompanyCode > 40.")
                        End If

                        ImportRow("AssetClass") = currentRow(3).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("AssetClass")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "AssetClass is Empty.")
                        ElseIf ImportRow("AssetClass").ToString.Length > 40 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "AssetClass > 40.")
                        End If

                        ImportRow("AssetDescription1") = currentRow(4).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("AssetDescription1")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "AssetDescription1 is Empty.")
                        ElseIf ImportRow("AssetDescription1").ToString.Length > 80 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "AssetDescription1 > 80.")
                        End If

                        ImportRow("AssetDescription2") = currentRow(5).ToString.Trim
                        If ImportRow("AssetDescription2").ToString.Length > 80 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "AssetDescription2 > 80.")
                        End If

                        ImportRow("RefNumber") = currentRow(6).ToString.Trim
                        If ImportRow("RefNumber").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "InventoryNumber > 50.")
                        End If

                        ImportRow("InventoryNumber") = currentRow(7).ToString.Trim
                        If ImportRow("InventoryNumber").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "InventoryNumber > 50.")
                        End If

                        ImportRow("SerialNumber") = currentRow(8).ToString.Trim
                        If ImportRow("SerialNumber").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "SerialNumber > 50.")
                        End If

                        ImportRow("BusinessArea") = currentRow(9).ToString.Trim
                        If ImportRow("BusinessArea").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "BusinessArea > 50.")
                        End If


                        ImportRow("CostCenter") = currentRow(10).ToString.Trim
                        If ImportRow("CostCenter").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CostCenter > 50.")
                        End If


                        ImportRow("Location") = currentRow(11).ToString.Trim
                        If ImportRow("Location").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Location > 50.")
                        End If

                        ImportRow("Plant") = currentRow(12).ToString.Trim
                        If ImportRow("Plant").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Plant > 50.")
                        End If

                        ImportRow("EmployeeNumber") = currentRow(13).ToString.Trim
                        If ImportRow("EmployeeNumber").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "EmployeeNumber > 50.")
                        End If

                        ImportRow("EvaluationGroup1") = currentRow(14).ToString.Trim
                        If ImportRow("EvaluationGroup1").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "EvaluationGroup1 > 50.")
                        End If
                        ImportRow("EvaluationGroup2") = currentRow(15).ToString.Trim
                        If ImportRow("EvaluationGroup2").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "EvaluationGroup2 > 50.")
                        End If
                        ImportRow("EvaluationGroup3") = currentRow(16).ToString.Trim
                        If ImportRow("EvaluationGroup3").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "EvaluationGroup3 > 50.")
                        End If
                        ImportRow("EvaluationGroup4") = currentRow(17).ToString.Trim
                        If ImportRow("EvaluationGroup4").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "EvaluationGroup4 > 50.")
                        End If

                        ImportRow("UsefulLife") = currentRow(18).ToString.Trim
                        If Not Integer.TryParse(ImportRow("UsefulLife"), -1) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "UsefulLife invalid.")
                        End If


                        ''Check if record exist in the database if the record pass the validation.
                        If ImportRow("Status").ToString = SuccessMessage Then
                            If Not objBALAssetDetails.Check_AstID(AssetTag, True) Then
                                ImportRow("Status") = String.Format("{0}", AssetTagNotExistMessage)
                            End If
                        End If

                    End If
                    If ImportRow("Status").ToString = SuccessMessage Then
                        ImportRow("Selection") = True
                    Else
                        ImportRow("Selection") = False
                    End If
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    ImportRow("Status") = ("Line (" & RowNumber & ") is invalid and will be skipped.")
                End Try
                ImportRow.AcceptChanges()
                RowNumber += 1

                ProgBar.PerformStep()
                ProgBar.Update()
            End While
            'End Using
            dtAssetChange.AcceptChanges()

            ''''
            grd.DataSource = dtAssetChange
            Dim TotalRowsCount As Integer = grdView.RowCount
            Dim ValidRowsCount As Integer = GetGridValidRows(grdView)
            Dim ExistIgroredCount As Integer = GetGridExistIgnoredRows(grdView)
            lblTotal.Text = String.Format("Total: {0}", TotalRowsCount)
            lblValid.Text = String.Format("Valid: {0}", ValidRowsCount)
            lblExistIgnore.Text = String.Format("Ignored: {0}", ExistIgroredCount)
            lblInvalid.Text = String.Format("Invalid: {0}", TotalRowsCount - ValidRowsCount - ExistIgroredCount)
            Return True
        Catch ex As Exception
            If SilentProcess Then
                SaveToLogFile(ex.Message, True)
            Else
                MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        Finally
            MyReader.Close()
            MyReader.Dispose()
            hTable.Clear()
            ProgBar.Visible = False
        End Try
    End Function


    Private Function ProcessAssetRetirementFile(ByVal file As String, ByVal SilentProcess As Boolean, ByVal ProgBar As DevExpress.XtraEditors.ProgressBarControl) As Boolean
        ProgBar.Visible = True
        ProgBar.Position = 1
        ProgBar.Properties.Step = 1
        ProgBar.Refresh()
        ProgBar.Properties.Maximum = GetFileLineCount(file)
        Application.DoEvents()
        Dim hTable As Hashtable = New Hashtable()
        Dim objBALAssetDetails As New BALAssetDetails
        Dim RowNumber As Integer = 1
        Dim AssetDataTable As DataTable = objBALAssetDetails.GetAsset_Details(New attAssetDetails)
        Dim MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(file, System.Text.Encoding.GetEncoding("windows-1256"))
        Try
            'Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(file, System.Text.Encoding.GetEncoding("windows-1256"))
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(Chr(Keys.Tab))
            MyReader.HasFieldsEnclosedInQuotes = False ' to remove error "Line # cannot be parsed using the current Delimiters"
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Dim ImportRow As DataRow = dtAssetRetirement.Rows.Add()
                Try
                    currentRow = MyReader.ReadFields()
                    ' if the count of the fields is less than 14 then we need to skip the row.
                    If currentRow.Length <> 4 Then
                        ImportRow("Status") = ("Line (" & RowNumber & ") is invalid and will be skipped.")
                    Else
                        ImportRow("RowNumber") = RowNumber
                        ImportRow("Status") = SuccessMessage

                        Dim AssetNumber As String = currentRow(0).ToString.Trim
                        Dim AssetSubNumber As String = currentRow(1).ToString.Trim
                        Dim ComapanyCode As String = currentRow(2).ToString.Trim

                        Dim AssetTag As String = GenerateAssetID(ComapanyCode, AssetNumber, AssetSubNumber)


                        If String.IsNullOrEmpty(AssetNumber) Then
                            ImportRow("Status") = "AssetNumber is empty."
                        Else
                            ' check for doublicated AssetTag
                            'Add list of all the unique item value to hashtable, which stores combination of key, value pair.
                            'And add duplicate item value in arraylist.
                            If hTable.Contains(AssetTag) Then
                                'Locate all rows, and mark them as duplicated.
                                'because we should regect All dublicated asset tags.
                                Dim dra As DataRow() = dtAssetRetirement.Select("AssetNumber+SubNumber+CompanyCode = '" & AssetTag & "'")
                                For Each row As DataRow In dra
                                    row("Status") = "AssetNumber duplicated."
                                    'unSelect the old dublicated rows.
                                    row("Selection") = False
                                Next
                                ImportRow("Status") = "AssetNumber duplicated."
                            Else
                                hTable.Add(AssetTag, String.Empty)
                            End If
                        End If

                        ImportRow("AssetNumber") = AssetNumber

                        ImportRow("SubNumber") = AssetSubNumber

                        ImportRow("CompanyCode") = ComapanyCode


                        ImportRow("RetirementDate") = currentRow(3).ToString.Trim
                        If Not CheckConvertDate(ImportRow("RetirementDate").ToString) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "RetirementDate is invalid.")
                        End If



                        ''Check if record exist in the database if the record pass the validation.
                        If ImportRow("Status").ToString = SuccessMessage Then
                            If Not objBALAssetDetails.Check_AstID(AssetTag, True) Then
                                ImportRow("Status") = String.Format("{0}", AssetTagNotExistMessage)
                            End If
                        End If


                    End If
                    If ImportRow("Status").ToString = SuccessMessage Then
                        ImportRow("Selection") = True
                    Else
                        ImportRow("Selection") = False
                    End If
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    ImportRow("Status") = ("Line (" & RowNumber & ") is invalid and will be skipped.")
                End Try
                ImportRow.AcceptChanges()
                RowNumber += 1

                ProgBar.PerformStep()
                ProgBar.Update()
            End While
            'End Using
            dtAssetRetirement.AcceptChanges()

            ''''
            grd.DataSource = dtAssetRetirement
            Dim TotalRowsCount As Integer = grdView.RowCount
            Dim ValidRowsCount As Integer = GetGridValidRows(grdView)
            Dim ExistIgroredCount As Integer = GetGridExistIgnoredRows(grdView)
            lblTotal.Text = String.Format("Total: {0}", TotalRowsCount)
            lblValid.Text = String.Format("Valid: {0}", ValidRowsCount)
            lblExistIgnore.Text = String.Format("Ignored: {0}", ExistIgroredCount)
            lblInvalid.Text = String.Format("Invalid: {0}", TotalRowsCount - ValidRowsCount - ExistIgroredCount)
            Return True
        Catch ex As Exception
            If SilentProcess Then
                SaveToLogFile(ex.Message, True)
            Else
                MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        Finally
            MyReader.Close()
            MyReader.Dispose()
            hTable.Clear()
            ProgBar.Visible = False
        End Try
    End Function

    Private Function ProcessAssetCapitalizationFile(ByVal file As String, ByVal SilentProcess As Boolean, ByVal ProgBar As DevExpress.XtraEditors.ProgressBarControl) As Boolean
        ProgBar.Visible = True
        ProgBar.Position = 1
        ProgBar.Properties.Step = 1
        ProgBar.Refresh()
        ProgBar.Properties.Maximum = GetFileLineCount(file)
        Application.DoEvents()
        Dim hTable As Hashtable = New Hashtable()
        Dim objBALAssetDetails As New BALAssetDetails
        Dim RowNumber As Integer = 1
        Dim AssetDataTable As DataTable = objBALAssetDetails.GetAsset_Details(New attAssetDetails)
        Dim MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(file, System.Text.Encoding.GetEncoding("windows-1256"))
        Try
            'Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(file, System.Text.Encoding.GetEncoding("windows-1256"))
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(Chr(Keys.Tab))
            MyReader.HasFieldsEnclosedInQuotes = False ' to remove error "Line # cannot be parsed using the current Delimiters"
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Dim ImportRow As DataRow = dtAssetCapitalization.Rows.Add()
                Try
                    currentRow = MyReader.ReadFields()
                    ' if the count of the fields is less than 14 then we need to skip the row.
                    If currentRow.Length <> 4 Then
                        ImportRow("Status") = ("Line (" & RowNumber & ") is invalid and will be skipped.")
                    Else
                        ImportRow("RowNumber") = RowNumber
                        ImportRow("Status") = SuccessMessage

                        Dim AssetNumber As String = currentRow(0).ToString.Trim
                        Dim AssetSubNumber As String = currentRow(1).ToString.Trim
                        Dim ComapanyCode As String = currentRow(2).ToString.Trim

                        Dim AssetTag As String = GenerateAssetID(ComapanyCode, AssetNumber, AssetSubNumber)


                        If String.IsNullOrEmpty(AssetNumber) Then
                            ImportRow("Status") = "AssetNumber is empty."
                        Else
                            ' check for doublicated AssetTag
                            'Add list of all the unique item value to hashtable, which stores combination of key, value pair.
                            'And add duplicate item value in arraylist.
                            If hTable.Contains(AssetTag) Then
                                'Locate all rows, and mark them as duplicated.
                                'because we should regect All dublicated asset tags.
                                Dim dra As DataRow() = dtAssetCapitalization.Select("AssetNumber+SubNumber+CompanyCode = '" & AssetTag & "'")
                                For Each row As DataRow In dra
                                    row("Status") = "AssetNumber duplicated."
                                    'unSelect the old dublicated rows.
                                    row("Selection") = False
                                Next
                                ImportRow("Status") = "AssetNumber duplicated."
                            Else
                                hTable.Add(AssetTag, String.Empty)
                            End If
                        End If

                        ImportRow("AssetNumber") = AssetNumber

                        ImportRow("SubNumber") = AssetSubNumber

                        ImportRow("CompanyCode") = ComapanyCode


                        ImportRow("CapitalizationDate") = currentRow(3).ToString.Trim
                        If Not CheckConvertDate(ImportRow("CapitalizationDate").ToString) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CapitalizationDate is invalid.")
                        End If



                        ''Check if record exist in the database if the record pass the validation.
                        If ImportRow("Status").ToString = SuccessMessage Then
                            If Not objBALAssetDetails.Check_AstID(AssetTag, True) Then
                                ImportRow("Status") = String.Format("{0}", AssetTagNotExistMessage)
                            End If
                        End If


                    End If
                    If ImportRow("Status").ToString = SuccessMessage Then
                        ImportRow("Selection") = True
                    Else
                        ImportRow("Selection") = False
                    End If
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    ImportRow("Status") = ("Line (" & RowNumber & ") is invalid and will be skipped.")
                End Try
                ImportRow.AcceptChanges()
                RowNumber += 1

                ProgBar.PerformStep()
                ProgBar.Update()
            End While
            'End Using
            dtAssetCapitalization.AcceptChanges()

            ''''
            grd.DataSource = dtAssetCapitalization
            Dim TotalRowsCount As Integer = grdView.RowCount
            Dim ValidRowsCount As Integer = GetGridValidRows(grdView)
            Dim ExistIgroredCount As Integer = GetGridExistIgnoredRows(grdView)
            lblTotal.Text = String.Format("Total: {0}", TotalRowsCount)
            lblValid.Text = String.Format("Valid: {0}", ValidRowsCount)
            lblExistIgnore.Text = String.Format("Ignored: {0}", ExistIgroredCount)
            lblInvalid.Text = String.Format("Invalid: {0}", TotalRowsCount - ValidRowsCount - ExistIgroredCount)
            Return True
        Catch ex As Exception
            If SilentProcess Then
                SaveToLogFile(ex.Message, True)
            Else
                MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        Finally
            MyReader.Close()
            MyReader.Dispose()
            hTable.Clear()
            ProgBar.Visible = False
        End Try
    End Function


    Private Function ProcessAssetWithValueFile(ByVal file As String, ByVal SilentProcess As Boolean, ByVal ProgBar As DevExpress.XtraEditors.ProgressBarControl) As Boolean
        ProgBar.Visible = True
        ProgBar.Position = 1
        ProgBar.Properties.Step = 1
        ProgBar.Refresh()
        ProgBar.Properties.Maximum = GetFileLineCount(file)
        Application.DoEvents()
        Dim hTable As Hashtable = New Hashtable()
        Dim objBALAssetDetails As New BALAssetDetails
        Dim RowNumber As Integer = 1
        Dim AssetDataTable As DataTable = objBALAssetDetails.GetAsset_Details(New attAssetDetails)
        Dim MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(file, System.Text.Encoding.GetEncoding("windows-1256"))
        Try
            'Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(file, System.Text.Encoding.GetEncoding("windows-1256"))
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(Chr(Keys.Tab))
            MyReader.HasFieldsEnclosedInQuotes = False ' to remove error "Line # cannot be parsed using the current Delimiters"
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Dim ImportRow As DataRow = dtAssetWithValue.Rows.Add()
                Try
                    currentRow = MyReader.ReadFields()
                    ' if the count of the fields is less than 14 then we need to skip the row.
                    If currentRow.Length <> 3 Then
                        ImportRow("Status") = ("Line (" & RowNumber & ") is invalid and will be skipped.")
                    Else
                        ImportRow("RowNumber") = RowNumber
                        ImportRow("Status") = SuccessMessage

                        Dim AssetNumber As String = currentRow(0).ToString.Trim
                        Dim AssetSubNumber As String = currentRow(1).ToString.Trim
                        Dim ComapanyCode As String = currentRow(2).ToString.Trim

                        Dim AssetTag As String = GenerateAssetID(ComapanyCode, AssetNumber, AssetSubNumber)


                        If String.IsNullOrEmpty(AssetNumber) Then
                            ImportRow("Status") = "AssetNumber is empty."
                        Else
                            ' check for doublicated AssetTag
                            'Add list of all the unique item value to hashtable, which stores combination of key, value pair.
                            'And add duplicate item value in arraylist.
                            If hTable.Contains(AssetTag) Then
                                'Locate all rows, and mark them as duplicated.
                                'because we should regect All dublicated asset tags.
                                Dim dra As DataRow() = dtAssetWithValue.Select("AssetNumber+SubNumber+CompanyCode = '" & AssetTag & "'")
                                For Each row As DataRow In dra
                                    row("Status") = "AssetNumber duplicated."
                                    'unSelect the old dublicated rows.
                                    row("Selection") = False
                                Next
                                ImportRow("Status") = "AssetNumber duplicated."
                            Else
                                hTable.Add(AssetTag, String.Empty)
                            End If
                        End If

                        ImportRow("AssetNumber") = AssetNumber

                        ImportRow("SubNumber") = AssetSubNumber

                        ImportRow("CompanyCode") = ComapanyCode

                        ''Check if record exist in the database if the record pass the validation.
                        If ImportRow("Status").ToString = SuccessMessage Then
                            If Not objBALAssetDetails.Check_AstID(AssetTag, True) Then
                                ImportRow("Status") = String.Format("{0}", AssetTagNotExistMessage)
                            End If
                        End If

                        If ImportRow("Status").ToString = SuccessMessage Then
                            ImportRow("Selection") = True
                        Else
                            ImportRow("Selection") = False
                        End If
                    End If

                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    ImportRow("Status") = ("Line (" & RowNumber & ") is invalid and will be skipped.")
                End Try
                ImportRow.AcceptChanges()
                RowNumber += 1

                ProgBar.PerformStep()
                ProgBar.Update()
            End While
            'End Using
            dtAssetWithValue.AcceptChanges()

            ''''
            grd.DataSource = dtAssetWithValue
            Dim TotalRowsCount As Integer = grdView.RowCount
            Dim ValidRowsCount As Integer = GetGridValidRows(grdView)
            Dim ExistIgroredCount As Integer = GetGridExistIgnoredRows(grdView)
            lblTotal.Text = String.Format("Total: {0}", TotalRowsCount)
            lblValid.Text = String.Format("Valid: {0}", ValidRowsCount)
            lblExistIgnore.Text = String.Format("Ignored: {0}", ExistIgroredCount)
            lblInvalid.Text = String.Format("Invalid: {0}", TotalRowsCount - ValidRowsCount - ExistIgroredCount)
            Return True
        Catch ex As Exception
            If SilentProcess Then
                SaveToLogFile(ex.Message, True)
            Else
                MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        Finally
            MyReader.Close()
            MyReader.Dispose()
            hTable.Clear()
            ProgBar.Visible = False
        End Try
    End Function

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Function GetGridSelectedRows(ByVal grd As DevExpress.XtraGrid.Views.Grid.GridView) As Integer
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Selection", True)
        Dim Count As Integer = grd.RowCount
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Selection", Nothing)
        Return Count
    End Function
    Private Function GetGridAssetTagNotExistRows(ByVal grd As DevExpress.XtraGrid.Views.Grid.GridView) As Integer
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Status", "%" & AssetTagNotExistMessage)
        Dim Count As Integer = grd.RowCount
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Status", Nothing)
        Return Count
    End Function
    Private Function GetGridExistIgnoredRows(ByVal grd As DevExpress.XtraGrid.Views.Grid.GridView) As Integer
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Status", "%Ignored")
        Dim Count As Integer = grd.RowCount
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Status", Nothing)
        Return Count
    End Function

    Private Function GetGridValidRows(ByVal grd As DevExpress.XtraGrid.Views.Grid.GridView) As Integer
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Status", SuccessMessage)
        Dim SuccessCount As Integer = grd.RowCount
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Status", Nothing)
        Return SuccessCount
    End Function

    Private Function GetGridValidSelectedRows(ByVal grd As DevExpress.XtraGrid.Views.Grid.GridView) As Integer
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Selection", True)
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Status", SuccessMessage)
        Dim Count As Integer = grd.RowCount
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Status", Nothing)
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Selection", Nothing)
        Return Count
    End Function

    'Private Function GetGridExistSelectedRows(ByVal grd As DevExpress.XtraGrid.Views.Grid.GridView) As Integer
    '    grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Selection", True)
    '    grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Status", "%" & AssetTagNotExistMessage)
    '    Dim Count As Integer = grd.RowCount
    '    grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Status", Nothing)
    '    grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Selection", Nothing)
    '    Return Count
    'End Function


    Private Function ImportAssetCreation(ByVal SilentProcess As Boolean, ByVal ProgBar As DevExpress.XtraEditors.ProgressBarControl) As String
        Dim importedcount As Integer = 0
        Dim Start As Integer = Environment.TickCount
        Dim objAssets As New BALItems
        Dim dtAssets As DataTable = objAssets.GetAllData_Joined(New attItems)
        Dim ObjAssetsDetails As New BALAssetDetails
        Dim dtAssetsDetails As DataTable = ObjAssetsDetails.GetAsset_Details(New attAssetDetails)

        ProgBar.Visible = True
        ProgBar.Position = 1
        ProgBar.Properties.Step = 1
        ProgBar.Properties.Maximum = grdView.RowCount

        For i As Integer = 0 To grdView.RowCount - 1
            'Import Selected and Success rows only.

            If grdView.GetRowCellValue(i, ("Status")).ToString.Contains(SuccessMessage) And CBool(grdView.GetRowCellValue(i, ("Selection"))) = True Then
                'Import Category.
                Dim CompanyID As String = ImportCompany(grdView.GetRowCellValue(i, ("CompanyCode")))

                Dim CatID As String = ImportCategory(grdView.GetRowCellValue(i, ("AssetClass")))
                'Import location.
                Dim loc As AssetLocation
                loc.PlantCode = grdView.GetRowCellValue(i, ("Plant"))
                loc.LocationCode = grdView.GetRowCellValue(i, ("Location"))
                loc.PlantName = grdView.GetRowCellValue(i, ("Plant"))
                loc.LocationName = grdView.GetRowCellValue(i, ("Location"))
                Dim LocID As String = ImportLocation(loc, CompanyID)
                'Import Employee
                Dim EmpID As String = ImportEmp(grdView.GetRowCellValue(i, ("EmployeeNumber")))
                'Import Company
                'Import CostCenter
                Dim CostCenterID As String = String.Empty
                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("CostCenter"))) Then
                    CostCenterID = ImportCostCenter(grdView.GetRowCellValue(i, ("CostCenter")))
                End If


                'Import Assets
                If ImportAsset(grdView.GetRowCellValue(i, ("AssetNumber")), grdView.GetRowCellValue(i, ("SubNumber")), grdView.GetRowCellValue(i, ("AssetDescription1")), grdView.GetRowCellValue(i, ("AssetDescription2")), ConvertDate(grdView.GetRowCellValue(i, ("CreationDate"))), grdView.GetRowCellValue(i, ("CreatedBy")), grdView.GetRowCellValue(i, ("CapitalizationDate")), grdView.GetRowCellValue(i, ("InventoryNumber")), grdView.GetRowCellValue(i, ("SerialNumber")), grdView.GetRowCellValue(i, ("BusinessArea")), CostCenterID, CompanyID, grdView.GetRowCellValue(i, ("CompanyCode")), CatID, LocID, EmpID, grdView.GetRowCellValue(i, ("EvaluationGroup1")), grdView.GetRowCellValue(i, ("EvaluationGroup2")), grdView.GetRowCellValue(i, ("EvaluationGroup3")), grdView.GetRowCellValue(i, ("EvaluationGroup4")), grdView.GetRowCellValue(i, ("UsefulLife")), dtAssets, dtAssetsDetails, True, grdView.GetRowCellValue(i, ("RefNumber"))) Then
                    importedcount += 1
                End If
            End If
            ProgBar.PerformStep()
            ProgBar.Update()

        Next
        Dim msg As String = "Import AssetCreation file Completed, imported Asset count = (" & importedcount & "), Ignored asset count = (" & lblExistIgnore.Text & "), Invalid asset count = (" & lblInvalid.Text & ")"
        SaveToLogFile(msg, False)
        ProgBar.Visible = False

        If Not SilentProcess Then
            Dim time As String = (Environment.TickCount - Start) / 1000 & " S"
            MessageBox.Show("Import Completed, imported Asset count = " & importedcount & ". Time(" & time & ")", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
        Return msg
    End Function

    Private Function ImportAssetRetirement(ByVal SilentProcess As Boolean, ByVal ProgBar As DevExpress.XtraEditors.ProgressBarControl) As String
        Dim importedcount As Integer = 0
        Dim Start As Integer = Environment.TickCount
        Dim ObjAssetsDetails As New BALAssetDetails
        Dim ObjattAssetDetails As New attAssetDetails
        ProgBar.Visible = True
        ProgBar.Position = 1
        ProgBar.Properties.Step = 1
        ProgBar.Properties.Maximum = grdView.RowCount

        ObjattAssetDetails.Disposed = False

        ObjAssetsDetails.DisableAssetTriggers() 'Disallow the logging tringgers, because huge data will come from SetDisposedAsset
        ObjAssetsDetails.SetDisposedAsset(ObjattAssetDetails)

        For i As Integer = 0 To grdView.RowCount - 1
            'Import Selected and Success rows only.
            Dim AssetID As String = GenerateAssetID(grdView.GetRowCellValue(i, ("CompanyCode")), grdView.GetRowCellValue(i, ("AssetNumber")), grdView.GetRowCellValue(i, ("SubNumber")))
            ObjattAssetDetails = New attAssetDetails
            ObjattAssetDetails.Disposed = True
            ObjattAssetDetails.DispCode = 2
            ObjattAssetDetails.DispDate = ConvertDate(grdView.GetRowCellValue(i, ("RetirementDate")))
            ObjattAssetDetails.PKeyCode = AssetID
            If grdView.GetRowCellValue(i, ("Status")).ToString.Contains(SuccessMessage) And CBool(grdView.GetRowCellValue(i, ("Selection"))) = True Then
                If ObjAssetsDetails.SetDisposedAsset(ObjattAssetDetails) Then
                    importedcount += 1
                End If
            End If
            ProgBar.PerformStep()
            ProgBar.Update()
        Next
        ObjAssetsDetails.EnabbleAssetTriggers()

        Dim msg As String = "Import AssetRetirement file Completed, imported Asset count = (" & importedcount & "), Ignored asset count = (" & lblExistIgnore.Text & "), Invalid asset count = (" & lblInvalid.Text & ")"
        SaveToLogFile(msg, False)
        ProgBar.Visible = False
        If Not SilentProcess Then
            Dim time As String = (Environment.TickCount - Start) / 1000 & " S"
            MessageBox.Show("Import Completed, imported Asset count = " & importedcount & ". Time(" & time & ")", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
        Return msg
    End Function


    Private Function ImportAssetCapitalization(ByVal SilentProcess As Boolean, ByVal ProgBar As DevExpress.XtraEditors.ProgressBarControl) As String
        Dim importedcount As Integer = 0
        Dim Start As Integer = Environment.TickCount
        Dim ObjAssetsDetails As New BALAssetDetails
        Dim ObjattAssetDetails As New attAssetDetails
        ProgBar.Visible = True
        ProgBar.Position = 1
        ProgBar.Properties.Step = 1
        ProgBar.Properties.Maximum = grdView.RowCount

        For i As Integer = 0 To grdView.RowCount - 1
            'Import Selected and Success rows only.
            Dim AssetID As String = GenerateAssetID(grdView.GetRowCellValue(i, ("CompanyCode")), grdView.GetRowCellValue(i, ("AssetNumber")), grdView.GetRowCellValue(i, ("SubNumber")))
            ObjattAssetDetails.PKeyCode = AssetID
            ObjattAssetDetails.CapitalizationDate = ConvertDate(grdView.GetRowCellValue(i, ("CapitalizationDate")))
            If grdView.GetRowCellValue(i, ("Status")).ToString.Contains(SuccessMessage) And CBool(grdView.GetRowCellValue(i, ("Selection"))) = True Then
                If ObjAssetsDetails.SetCapitalizationDateValue(ObjattAssetDetails) Then
                    importedcount += 1
                End If
            End If
            ProgBar.PerformStep()
            ProgBar.Update()
        Next
        Dim msg As String = "Import AssetCapitalization file Completed, imported Asset count = (" & importedcount & "), Ignored asset count = (" & lblExistIgnore.Text & "), Invalid asset count = (" & lblInvalid.Text & ")"
        SaveToLogFile(msg, False)
        ProgBar.Visible = False
        If Not SilentProcess Then
            Dim time As String = (Environment.TickCount - Start) / 1000 & " S"
            MessageBox.Show("Import Completed, imported Asset count = " & importedcount & ". Time(" & time & ")", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
        Return msg
    End Function

    Private Function ImportAssetWithValue(ByVal SilentProcess As Boolean, ByVal ProgBar As DevExpress.XtraEditors.ProgressBarControl) As String
        Dim importedcount As Integer = 0
        Dim Start As Integer = Environment.TickCount
        Dim ObjAssetsDetails As New BALAssetDetails
        Dim ObjattAssetDetails As attAssetDetails

        ProgBar.Visible = True
        ProgBar.Position = 1
        ProgBar.Properties.Step = 1
        ProgBar.Properties.Maximum = grdView.RowCount

        'Reset all the assets to be without value(InstockAssets)
        ObjattAssetDetails = New attAssetDetails()
        ObjattAssetDetails.InStockAsset = True

        ObjAssetsDetails.DisableAssetTriggers() 'Disallow the logging tringgers, because huge data will come from SetAssetWithValue
        ObjAssetsDetails.SetAssetWithValue(ObjattAssetDetails)

        For i As Integer = 0 To grdView.RowCount - 1
            'Import Selected and Success rows only.
            Dim AssetID As String = GenerateAssetID(grdView.GetRowCellValue(i, ("CompanyCode")), grdView.GetRowCellValue(i, ("AssetNumber")), grdView.GetRowCellValue(i, ("SubNumber")))
            ObjattAssetDetails = New attAssetDetails()
            ObjattAssetDetails.PKeyCode = AssetID
            ObjattAssetDetails.InStockAsset = False
            If grdView.GetRowCellValue(i, ("Status")).ToString.Contains(SuccessMessage) And CBool(grdView.GetRowCellValue(i, ("Selection"))) = True Then
                If ObjAssetsDetails.SetAssetWithValue(ObjattAssetDetails) Then
                    importedcount += 1
                End If
            End If
            ProgBar.PerformStep()
            ProgBar.Update()
        Next
        ObjAssetsDetails.EnabbleAssetTriggers()
        Dim msg As String = "Import AssetWithValue file Completed, imported Asset count = (" & importedcount & "), Ignored asset count = (" & lblExistIgnore.Text & "), Invalid asset count = (" & lblInvalid.Text & ")"
        SaveToLogFile(msg, False)
        ProgBar.Visible = False
        If Not SilentProcess Then
            Dim time As String = (Environment.TickCount - Start) / 1000 & " S"
            MessageBox.Show("Import Completed, imported Asset count = " & importedcount & ". Time(" & time & ")", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
        Return msg
    End Function

    Private Function ImportAssetChange(ByVal SilentProcess As Boolean, ByVal ProgBar As DevExpress.XtraEditors.ProgressBarControl) As String
        Dim importedcount As Integer = 0

        Dim Start As Integer = Environment.TickCount
        Dim objAssets As New BALItems
        Dim dtAssets As DataTable = objAssets.GetAllData_Joined(New attItems)
        Dim ObjAssetsDetails As New BALAssetDetails
        Dim dtAssetsDetails As DataTable = ObjAssetsDetails.GetAsset_Details(New attAssetDetails)
        ProgBar.Visible = True
        ProgBar.Position = 1
        ProgBar.Properties.Step = 1
        ProgBar.Properties.Maximum = grdView.RowCount

        For i As Integer = 0 To grdView.RowCount - 1
            'Import Selected and Success rows only.

            If grdView.GetRowCellValue(i, ("Status")).ToString.Contains(SuccessMessage) And CBool(grdView.GetRowCellValue(i, ("Selection"))) = True Then
                'Import Company
                Dim CompanyID As String = ImportCompany(grdView.GetRowCellValue(i, ("CompanyCode")))

                'Import Category.
                Dim CatID As String = ImportCategory(grdView.GetRowCellValue(i, ("AssetClass")))
                'Import location.
                Dim loc As AssetLocation
                loc.PlantCode = grdView.GetRowCellValue(i, ("Plant"))
                loc.LocationCode = grdView.GetRowCellValue(i, ("Location"))
                loc.PlantName = grdView.GetRowCellValue(i, ("Plant"))
                loc.LocationName = grdView.GetRowCellValue(i, ("Location"))

                Dim LocID As String = ImportLocation(loc, CompanyID)
                'Import Employee
                Dim EmpID As String = ImportEmp(grdView.GetRowCellValue(i, ("EmployeeNumber")))
                'Import CostCenter
                Dim CostCenterID As String = ImportCostCenter(grdView.GetRowCellValue(i, ("CostCenter")))
                'Import Assets
                If ImportAsset(grdView.GetRowCellValue(i, ("AssetNumber")), grdView.GetRowCellValue(i, ("SubNumber")), grdView.GetRowCellValue(i, ("AssetDescription1")), grdView.GetRowCellValue(i, ("AssetDescription2")), Nothing, Nothing, Nothing, grdView.GetRowCellValue(i, ("InventoryNumber")), grdView.GetRowCellValue(i, ("SerialNumber")), grdView.GetRowCellValue(i, ("BusinessArea")), CostCenterID, CompanyID, grdView.GetRowCellValue(i, ("CompanyCode")), CatID, LocID, EmpID, grdView.GetRowCellValue(i, ("EvaluationGroup1")), grdView.GetRowCellValue(i, ("EvaluationGroup2")), grdView.GetRowCellValue(i, ("EvaluationGroup3")), grdView.GetRowCellValue(i, ("EvaluationGroup4")), grdView.GetRowCellValue(i, ("UsefulLife")), dtAssets, dtAssetsDetails, False, grdView.GetRowCellValue(i, ("RefNumber"))) Then
                    importedcount += 1
                End If
            End If
            ProgBar.PerformStep()
            ProgBar.Update()

        Next
        Dim msg As String = "Import AssetChange file Completed, imported Asset count = (" & importedcount & "), Ignored asset count = (" & lblExistIgnore.Text & "), Invalid asset count = (" & lblInvalid.Text & ")"
        SaveToLogFile(msg, False)
        ProgBar.Visible = False
        If Not SilentProcess Then
            Dim time As String = (Environment.TickCount - Start) / 1000 & " S"
            MessageBox.Show("Import Completed, imported Asset count = " & importedcount & ". Time(" & time & ")", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
        Return msg
    End Function

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        'Show confirmation message.
        Dim ValidSelectedRows As Integer = GetGridValidSelectedRows(grdView)
        Dim ValidRows As Integer = GetGridValidRows(grdView)
        'Dim ExistSelectedRows As Integer = GetGridExistSelectedRows(grdView)
        Dim IgnoredRows As Integer = GetGridExistIgnoredRows(grdView)

        If ValidSelectedRows < 1 Then
            MessageBox.Show("There is no valid selected rows!", " ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1)
            Return
        End If

        If MessageBox.Show("Are you sure you want to import? Only valid selected rows will be imported.", " ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1).ToString().Equals("Yes") Then
            If MessageBox.Show(String.Format("You are going to import ({0}) out of ({1}) valid rows, ({2}) ignored rows, do you want to continue?", ValidSelectedRows, ValidRows, IgnoredRows), " ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1).ToString().Equals("Yes") Then
                Select Case ImportType
                    Case TFileImportType.AssetCreate
                        ImportAssetCreation(False, pb)
                    Case TFileImportType.AssetChange
                        ImportAssetChange(False, pb)
                    Case TFileImportType.AssetRetirment
                        ImportAssetRetirement(False, pb)
                    Case TFileImportType.AssetCapitalization
                        ImportAssetCapitalization(False, pb)
                    Case TFileImportType.AssetWithValue
                        ImportAssetWithValue(False, pb)
                End Select
                Me.Close()
            End If
        End If

    End Sub

    Private Function GetCompleteLocationCode(ByVal loc As AssetLocation) As String
        Dim CompleteCode As String = loc.PlantCode
        If loc.LocationCode <> "" Then
            CompleteCode &= " \ " & loc.LocationCode
        End If
        Return CompleteCode
    End Function

    Private Function GetCompleteLocationDescription(ByVal loc As AssetLocation) As String
        Dim CompleteDesc As String = loc.PlantName
        If loc.LocationCode <> "" Then
            CompleteDesc &= " \ " & loc.LocationName
        End If
        Return CompleteDesc
    End Function

    Private Function ImportLocation(ByVal loc As AssetLocation, ByVal CompanyID As Integer) As String
        Dim objattLocation As New attLocation
        Dim objBALLocation As New BALLocation

        Dim PlantID As String = ""
        Dim LocationID As String = ""


        Dim PlantDesc As String = ""
        Dim LocationDesc As String = ""


        Dim CompleteLocDesc As String = GetCompleteLocationDescription(loc)

        LocationID = objBALLocation.GetLocIDByCompleteDesc(CompleteLocDesc)
        LocationDesc = objBALLocation.GetLocDescByID(LocationID)

        If String.IsNullOrEmpty(loc.PlantName) Then
            Return 1
        Else
            PlantID = objBALLocation.GetLocIDByCompleteDesc(loc.PlantName)
            If PlantID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = loc.PlantName
                PlantID = objBALLocation.GetRootLocID()
                objattLocation.HierCode = PlantID
                objattLocation.Code = loc.PlantCode
                objattLocation.locLevel = 0
                objattLocation.CompanyID = CompanyID
                objBALLocation.Insert_Location(objattLocation)
            End If
            If String.IsNullOrEmpty(loc.LocationName) Then
                Return PlantID
            Else
                objattLocation = New attLocation
                LocationID = objBALLocation.GetLocIDByCompleteDesc(loc.PlantName & " \ " & loc.LocationName)
                If LocationID = "0" Then
                    objattLocation = New attLocation
                    objattLocation.Description = loc.LocationName
                    LocationID = objBALLocation.GetChildLocID(PlantID)
                    objattLocation.HierCode = LocationID
                    objattLocation.Code = loc.LocationCode
                    objattLocation.Compcode = 1
                    objattLocation.locLevel = 1
                    objattLocation.CompanyID = CompanyID
                    objBALLocation.Insert_Location(objattLocation)
                End If
                Return LocationID
            End If
        End If
    End Function

    Private Function ImportAsset(ByVal AssetNumber As String, ByVal SubNumber As String, ByVal AssetDescription1 As String, ByVal AssetDescription2 As String, ByVal CreationDate As String, ByVal Createdby As String, ByVal CapitalizationDate As String, ByVal InventoryNumber As String, ByVal SerialNumber As String, ByVal BusinessArea As String, ByVal CostCenterID As String, ByVal CompanyID As String, ByVal CompanyCode As String, ByVal CatID As String, ByVal LocationID As String, ByVal CustodianID As String, ByVal EvaluationGroup1 As String, ByVal EvaluationGroup2 As String, ByVal EvaluationGroup3 As String, ByVal EvaluationGroup4 As String, ByVal UsefulLife As String, ByRef dtAssets As DataTable, ByVal dtAssetDetails As DataTable, ByVal IsNewAsset As Boolean, ByVal RefNumber As String) As Boolean
        'Add Asset Item
        Try
            Dim AssetItemID As String = ""
            AssetDescription1 = AssetDescription1.ToString.Replace("'", "")
            Dim AssetRows As DataRow() = dtAssets.Select("AstDesc = '" & AssetDescription1 & "' and AstCatID = '" & CatID & "'")
            If AssetRows.Length > 0 Then
                AssetItemID = AssetRows(0)(0)
            Else
                Dim objBALAssets As New BALItems
                Dim objattAssets As New attItems
                objattAssets.AstCatID = CatID
                objattAssets.AstDesc = AssetDescription1
                AssetItemID = objBALAssets.GetNextPKey_Item()
                objattAssets.PKeyCode = AssetItemID
                objBALAssets.Insert_Item(objattAssets)
                Dim row As DataRow = dtAssets.NewRow()
                row("AstCatID") = CatID
                row("AstDesc") = AssetDescription1
                row("itemcode") = AssetItemID
                dtAssets.Rows.Add(row)
            End If

            Dim objattAssetDetails As New attAssetDetails
            Dim objBALAssetDetails As New BALAssetDetails
            Dim AssetID As String = GenerateAssetID(CompanyCode, AssetNumber, SubNumber)

            objattAssetDetails.BaseCost = 0
            objattAssetDetails.CustodianID = CustodianID
            objattAssetDetails.InsID = 0
            objattAssetDetails.SuppID = ""
            objattAssetDetails.POCode = 0
            objattAssetDetails.SerailNo = SerialNumber
            objattAssetDetails.Discount = 0
            objattAssetDetails.InvNumber = ""
            objattAssetDetails.ItemCode = AssetItemID
            objattAssetDetails.Tax = 0
            objattAssetDetails.TransRemarks = "SAP"
            objattAssetDetails.DispDate = Nothing
            objattAssetDetails.CompanyID = CompanyID
            objattAssetDetails.AstBrandID = 1
            objattAssetDetails.AstDesc = AssetDescription1
            objattAssetDetails.AstDesc2 = AssetDescription2
            objattAssetDetails.AstModel = ""
            objattAssetDetails.Capex = ""
            objattAssetDetails.PoErp = ""
            objattAssetDetails.Plate = ""
            objattAssetDetails.GRN = ""
            objattAssetDetails.RefCode = ""
            objattAssetDetails.GLCode = 1
            objattAssetDetails.PONumber = ""
            objattAssetDetails.LocID = LocationID
            objattAssetDetails.NoPiece = 1
            objattAssetDetails.Disposed = "0"
            objattAssetDetails.IsSold = 0


            objattAssetDetails.EvaluationGroup1 = EvaluationGroup1
            objattAssetDetails.EvaluationGroup2 = EvaluationGroup2
            objattAssetDetails.EvaluationGroup3 = EvaluationGroup3
            objattAssetDetails.EvaluationGroup4 = EvaluationGroup4
            objattAssetDetails.CostCenterID = CostCenterID
            objattAssetDetails.InventoryNumber = InventoryNumber
            objattAssetDetails.BussinessArea = BusinessArea

            objattAssetDetails.RefNo = String.Format("{0}-{1}-{2}", CompanyCode, AssetNumber, SubNumber)
            objattAssetDetails.AstNum = AssetID
            objattAssetDetails.PKeyCode = AssetID
            objattAssetDetails.BarCode = AssetID

            objattAssetDetails.IsDataChanged = False
            objattAssetDetails.CustomFld1 = RefNumber
            If Not String.IsNullOrEmpty(RefNumber) Then
                objattAssetDetails.CustomFld2 = GenerateOldAssetID(RefNumber)
            Else
                objattAssetDetails.CustomFld2 = ""
            End If
            objattAssetDetails.CustomFld3 = String.Empty
            objattAssetDetails.CustomFld4 = String.Empty
            objattAssetDetails.CustomFld5 = String.Empty
            If IsNewAsset Then

            Else

            End If

            Dim DetailsRows As DataRow() = dtAssetDetails.Select("AstID = '" & AssetID & "'")
            If Not IsNewAsset And DetailsRows.Length > 0 Then
                objattAssetDetails.PurDate = DetailsRows(0)("PurDate")
                objattAssetDetails.SrvDate = DetailsRows(0)("SrvDate")
                If Not DetailsRows(0).IsNull("CapitalizationDate") Then
                    objattAssetDetails.CapitalizationDate = DetailsRows(0)("CapitalizationDate")
                End If
                objattAssetDetails.CreatedBY = DetailsRows(0)("Createdby").ToString
                objattAssetDetails.InStockAsset = DetailsRows(0)("InStockAsset")
                objattAssetDetails.IsDataChanged = False
                'When we are importing from SAP, because they are not sending the user from their files, we are setting the user to be 'SAP User'
                objattAssetDetails.LastEditBY = "admin"
                objattAssetDetails.LastEditDate = Now.Date

                If objBALAssetDetails.Update_AssetDetails(objattAssetDetails) Then
                    objBALAssetDetails.Update_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, 0, UsefulLife, 0, objattAssetDetails.SrvDate, False)
                End If
            ElseIf IsNewAsset Then
                objattAssetDetails.PurDate = CreationDate
                objattAssetDetails.SrvDate = CreationDate

                If CheckConvertDate(CapitalizationDate) Then
                    objattAssetDetails.CapitalizationDate = ConvertDate(CapitalizationDate)
                Else
                    objattAssetDetails.CapitalizationDate = Nothing
                End If

                objattAssetDetails.CreationDate = Now.Date
                objattAssetDetails.CreatedBY = Createdby.ToString

                objattAssetDetails.LastEditDate = Now.Date
                objattAssetDetails.LastEditBY = Createdby.ToString

                objattAssetDetails.InStockAsset = True
                objattAssetDetails.InvSchCode = 1
                objattAssetDetails.InvStatus = 0
                objattAssetDetails.StatusID = 1

                If objBALAssetDetails.Insert_AssetDetails(objattAssetDetails, False) Then
                    objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, 0, UsefulLife, 0, objattAssetDetails.SrvDate, False)
                End If
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function GenerateAssetID(ByVal CompanyCode As String, ByVal AssetNumber As String, ByVal SubNumber As String) As Long
        Dim AssetTag As String = CompanyCode & AssetNumber & SubNumber
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

    Private Function GenerateOldAssetID(ByVal OldRefNumber As String) As Long
        Try

            Dim Num As Long = Long.Parse(OldRefNumber)
            Return Num
        Catch ex As Exception
            Dim sb As New System.Text.StringBuilder
            For Each ch As Char In OldRefNumber
                If Char.IsDigit(ch) Then
                    sb.Append(ch)
                End If
            Next
            Return Long.Parse(sb.ToString)
        End Try
    End Function


    Private Function ImportEmp(ByVal EmpCode As String) As String
        Dim objattCustodian As New attCustodian
        Dim objBALCustodian As New BALCustodian
        objattCustodian.PKeyCode = EmpCode
        Dim dt As DataTable = objBALCustodian.CheckID(objattCustodian)
        Dim CustodianID As String = ""
        If dt.Rows.Count > 0 Then
            CustodianID = dt.Rows(0)("CustodianID")
        Else
            objattCustodian.CustodianName = EmpCode
            objattCustodian.DepartmentID = 3
            objattCustodian.DesignationID = 1
            objattCustodian.CustodianCode = ""
            objBALCustodian.Insert_Custodian(objattCustodian)
            CustodianID = objattCustodian.PKeyCode
        End If
        Return CustodianID
    End Function

    Private Function ImportCostCenter(ByVal CostNumber As String) As String
        Dim objattCostCenter As New attCostCenter
        Dim objBALCostCenter As New BALCostCenter
        objattCostCenter.CostNumber = CostNumber
        Dim CostCenterID As String = ""
        Dim dt As DataTable = objBALCostCenter.GetAll_CostCenter(objattCostCenter)
        If dt.Rows.Count > 0 Then
            CostCenterID = dt.Rows(0)("CostID")
        Else

            objattCostCenter.PKeyCode = objBALCostCenter.GetNextPKey_CostCenter()
            objattCostCenter.CostNumber = CostNumber
            objattCostCenter.CostName = CostNumber
            objattCostCenter.CompanyID = 1
            objBALCostCenter.Insert_CostCenter(objattCostCenter)
            CostCenterID = objattCostCenter.PKeyCode
        End If
        Return CostCenterID
    End Function

    Private Function ImportCompany(ByVal CompanyCode As String) As String
        Dim objattCompany As New attcompany
        Dim objBALCompany As New BALCompany
        Dim CompanyID As Int64 = objBALCompany.GetCompanyIDByCompanyCode(CompanyCode)
        If CompanyID > 0 Then
            Return CompanyID
        Else
            objattCompany.PKeyCode = objBALCompany.GetNextPKey_Company()
            objattCompany.CompanyCode = CompanyCode
            objattCompany.CompanyName = CompanyCode
            objattCompany.HierCode = "1"
            objattCompany.BarStructID = 1
            objBALCompany.Insert_Company(objattCompany)
            CompanyID = objattCompany.PKeyCode
        End If
        Return CompanyID
    End Function

    ' This function will import the Main and sub categories and return subcat ID
    Private Function ImportCategory(ByVal MainCat As String) As String
        Dim objattCategory As attCategory
        Dim objBALCategory As New BALCategory
        Dim MainCatID As String = objBALCategory.GetCatIDByDesc(MainCat, "")
        objattCategory = New attCategory
        If MainCatID = "" Then 'CatDesc not found
            objattCategory.AstCatDesc = MainCat
            MainCatID = objBALCategory.GetRootCatID()
            objattCategory.AstCatID = MainCatID
            objattCategory.Code = MainCatID
            objattCategory.catLevel = 0
            If objBALCategory.Insert_Category(objattCategory) Then
                AddDepPolicyForCat(objattCategory.AstCatID)
            End If
        End If
        Return MainCatID
    End Function

    Private Sub AddDepPolicyForCat(ByVal CatID As String)
        'add asset book for the company if it's tracking or inventory edition.
        Dim objattDepPolicy As New attDepPolicy
        Dim objBALDepPolicy As New BALDepPolicy

        objattDepPolicy.AstCatID = CatID
        objattDepPolicy.DepCode = 1
        objattDepPolicy.SalvageValue = 0
        objattDepPolicy.SalvageYear = 1
        objattDepPolicy.IsSalvageValuePercent = False

        objBALDepPolicy.Insert_DepPolicy(objattDepPolicy)
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        For i As Integer = 0 To grdView.RowCount - 1
            grdView.SetRowCellValue(i, "Selection", True)
        Next
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        With SaveFileDialog1
            .CheckPathExists = True
            .FileName = ""
            .DefaultExt = "xls"
            .Filter = "Excel Sheet (*.xls)|*.xls"
            .Title = "Excel Sheet File"

            If .ShowDialog() = DialogResult.OK Then
                grdView.ExportToXls(.FileName)
            Else
                Me.Close()
            End If
            .Dispose()
        End With


    End Sub
End Class