Imports DevExpress.XtraGrid
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Windows.Forms

Public Class frmImport

    Dim dtImport As New DataTable
    Dim SuccessMessage As String = "Success."
    Dim ExistMessage As String = "AssetTag Already Exist. Ignore"
    Dim ExistSuccessMessage As String = String.Format("{0} {1}", "AssetTag Already Exist. ", SuccessMessage)

    Private _ImportFileName As String
    Private _ImportType As New TFileImportType

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


    Public Enum TFileImportType
        Asset
        Location
        CostCenter
        Employees
    End Enum

    Structure AssetLocation
        Dim PlantCode As String
        Dim PlantDesc As String
        Dim LocationCode As String
        Dim LocationDesc As String
        Dim CompanyCode As String
    End Structure

    Private Sub frmImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub frmImport_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Select Case ImportType
            Case TFileImportType.Asset
                'If process for the file success then format the grid.
                CreateAssetTempTable()
                If ProcessAssetFile(ImportFileName, False, pb) Then
                    grdAssetFormat()
                    Me.BringToFront()
                Else
                    Me.Close()
                End If
            Case TFileImportType.Location
                CreateLocationTempTable()
                If ProcessLocationFile(ImportFileName, False, pb) Then
                    grdAssetFormat()
                    Me.BringToFront()
                Else
                    Me.Close()
                End If

            Case TFileImportType.CostCenter
                CreateCostCenterTempTable()
                If ProcessCostCenterFile(ImportFileName, False, pb) Then
                    grdAssetFormat()
                    Me.BringToFront()
                Else
                    Me.Close()
                End If
            Case TFileImportType.Employees
                CreateEmployeeTempTable()
                If ProcessEmployeeFile(ImportFileName, False, pb) Then
                    grdAssetFormat()
                    Me.BringToFront()
                Else
                    Me.Close()
                End If
        End Select
    End Sub

    Private Sub grdAssetFormat()
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


        grdView.BestFitColumns()

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
        styleFormatCondition2.Value1 = ExistSuccessMessage
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

            Dim CountryName As String = ""
            Dim CityName As String = ""
            Dim BuildingName As String = ""
            Dim FloorName As String = ""
            Dim RoomName As String = ""

            Dim MainCategory As String = ""
            Dim SubCategory As String = ""
            Dim astDesc As String = FoundRow("Assetdetailsdesc1").ToString.Replace("'", "")
            Dim AstModel As String = FoundRow("AstModel")
            Dim SerailNo As String = FoundRow("SerailNo")
            CountryName = strFulLoc(0).Trim

            If strFulLoc.Length > 1 Then
                CityName = strFulLoc(1).Trim
            End If


            If strFulLoc.Length > 2 Then
                BuildingName = strFulLoc(2).Trim
            End If

            If strFulLoc.Length > 3 Then
                FloorName = strFulLoc(3).Trim
            End If

            If strFulLoc.Length > 4 Then
                RoomName = strFulLoc(4).Trim
            End If
            ImportDataRow("AssetDescription") = ImportDataRow("AssetDescription").ToString.Replace("'", "")
            MainCategory = strFulCat(0).Trim
            If strFulCat.Length > 1 Then
                SubCategory = strFulCat(1).Trim
            End If
            If astDesc.ToUpper <> ImportDataRow("AssetDescription").ToString.ToUpper Then
                Message = "AssetDescription,"
            End If
            If SerailNo.ToUpper <> ImportDataRow("SerialNo").ToString.ToUpper Then
                Message += "SerialNo,"
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
    Private Sub CreateAssetTempTable()
        dtImport = New DataTable
        dtImport.Columns.Add("Selection", Type.GetType("System.Boolean"))
        dtImport.Columns.Add("RowNumber", Type.GetType("System.Int32"))
        '
        dtImport.Columns.Add("CompanyCode", Type.GetType("System.String"))
        dtImport.Columns.Add("AssetNo", Type.GetType("System.String"))
        dtImport.Columns.Add("SubAssetNo", Type.GetType("System.String"))
        dtImport.Columns.Add("AssetTag", Type.GetType("System.String"))
        dtImport.Columns.Add("AssetClass", Type.GetType("System.String"))
        dtImport.Columns.Add("AssetDescription", Type.GetType("System.String"))
        dtImport.Columns.Add("SerialNo", Type.GetType("System.String"))
        dtImport.Columns.Add("CostCenter", Type.GetType("System.String"))
        dtImport.Columns.Add("Plant", Type.GetType("System.String"))
        dtImport.Columns.Add("Location", Type.GetType("System.String"))
        dtImport.Columns.Add("PersonnelNumber", Type.GetType("System.String"))

        dtImport.Columns.Add("Status", Type.GetType("System.String"))
        dtImport.Columns.Add("Message", Type.GetType("System.String"))
    End Sub

    Private Sub CreateLocationTempTable()
        dtImport = New DataTable
        dtImport.Columns.Add("Selection", Type.GetType("System.Boolean"))
        dtImport.Columns.Add("RowNumber", Type.GetType("System.Int32"))
        '
        dtImport.Columns.Add("CompanyCode", Type.GetType("System.String"))
        dtImport.Columns.Add("PlantCode", Type.GetType("System.String"))
        dtImport.Columns.Add("PlantDesc", Type.GetType("System.String"))
        dtImport.Columns.Add("LocationCode", Type.GetType("System.String"))
        dtImport.Columns.Add("LocationDesc", Type.GetType("System.String"))

        dtImport.Columns.Add("Status", Type.GetType("System.String"))
        dtImport.Columns.Add("Message", Type.GetType("System.String"))
    End Sub

    Private Sub CreateCostCenterTempTable()
        dtImport = New DataTable
        dtImport.Columns.Add("Selection", Type.GetType("System.Boolean"))
        dtImport.Columns.Add("RowNumber", Type.GetType("System.Int32"))
        '
        dtImport.Columns.Add("Code", Type.GetType("System.String"))
        dtImport.Columns.Add("CompanyCode", Type.GetType("System.String"))
        dtImport.Columns.Add("Description", Type.GetType("System.String"))

        dtImport.Columns.Add("Status", Type.GetType("System.String"))
        dtImport.Columns.Add("Message", Type.GetType("System.String"))
    End Sub


    Private Sub CreateEmployeeTempTable()
        dtImport = New DataTable
        dtImport.Columns.Add("Selection", Type.GetType("System.Boolean"))
        dtImport.Columns.Add("RowNumber", Type.GetType("System.Int32"))
        '
        dtImport.Columns.Add("Code", Type.GetType("System.String"))
        dtImport.Columns.Add("CompanyCode", Type.GetType("System.String"))
        dtImport.Columns.Add("Description", Type.GetType("System.String"))

        dtImport.Columns.Add("Status", Type.GetType("System.String"))
        dtImport.Columns.Add("Message", Type.GetType("System.String"))
    End Sub

    Private Function ProcessLocationFile(ByVal file As String, ByVal SilentImport As Boolean, ByVal pb As DevExpress.XtraEditors.ProgressBarControl) As Boolean
        pb.Visible = True
        pb.Position = 1
        pb.Properties.Step = 1
        pb.Refresh()
        pb.Properties.Maximum = GetFileLineCount(file)
        Application.DoEvents()
        Dim RowNumber As Integer = 1
        Dim MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(file, System.Text.Encoding.GetEncoding("windows-1256"))
        Try
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(Chr(9))
            MyReader.HasFieldsEnclosedInQuotes = True ' to remove error "Line # cannot be parsed using the current Delimiters"
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Dim ImportRow As DataRow = dtImport.Rows.Add()
                Try
                    currentRow = MyReader.ReadFields()
                    ' if the count of the fields is less than 14 then we need to skip the row.
                    If currentRow.Length <> 5 Then
                        ImportRow("Status") = ("Line (" & RowNumber & ") is invalid and will be skipped.")
                    Else
                        ImportRow("RowNumber") = RowNumber
                        ImportRow("Status") = SuccessMessage

                        ImportRow("CompanyCode") = currentRow(0).ToString
                        If ImportRow("CompanyCode").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CompanyCode > 50.")
                        End If

                        ImportRow("PlantCode") = currentRow(1).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("PlantCode")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "PlantCode is Empty."
                        ElseIf ImportRow("PlantCode").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "PlantCode > 50.")
                        End If

                        ImportRow("PlantDesc") = currentRow(2).ToString
                        If String.IsNullOrEmpty(ImportRow("PlantDesc")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "PlantDesc is Empty."
                        ElseIf ImportRow("PlantDesc").ToString.Length > 100 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "PlantDesc > 100.")
                        End If

                        ImportRow("LocationCode") = currentRow(3).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("LocationCode")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "LocationCode is Empty."
                        ElseIf ImportRow("LocationCode").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "LocationCode > 50.")
                        End If


                        ImportRow("LocationDesc") = currentRow(4).ToString
                        If String.IsNullOrEmpty(ImportRow("LocationDesc")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "LocationDesc is Empty.")
                        ElseIf ImportRow("LocationDesc").ToString.Length > 100 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "LocationDesc > 100.")
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
                Application.DoEvents()
                pb.PerformStep()
            End While
            'End Using
            dtImport.AcceptChanges()

            ''''
            grd.DataSource = dtImport
            Dim TotalRowsCount As Integer = grdView.RowCount
            Dim ValidRowsCount As Integer = GetGridValidRows(grdView)
            Dim ExistIgroredCount As Integer = GetGridExistIgnoredRows(grdView)
            Dim ExistCount As Integer = GetGridExistRows(grdView)
            lblTotal.Text = String.Format("Total: {0}", TotalRowsCount)
            lblValid.Text = String.Format("Valid: {0}", ValidRowsCount)
            lblExist.Text = String.Format("Already Exist: {0}", ExistCount)
            lblExistIgnore.Text = String.Format("Ignored: {0}", ExistIgroredCount)
            lblInvalid.Text = String.Format("Invalid: {0}", TotalRowsCount - ValidRowsCount - ExistIgroredCount - ExistCount)
            Return True
        Catch ex As Exception
            If SilentImport Then
                SaveToLogFile(ex.Message, True)
            Else
                MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        Finally
            MyReader.Close()
            MyReader.Dispose()
            pb.Visible = False
        End Try
    End Function


    Private Function ProcessCostCenterFile(ByVal file As String, ByVal SilentImport As Boolean, ByVal pb As DevExpress.XtraEditors.ProgressBarControl) As Boolean
        pb.Visible = True
        pb.Position = 1
        pb.Properties.Step = 1
        pb.Refresh()
        pb.Properties.Maximum = GetFileLineCount(file)
        Application.DoEvents()
        Dim RowNumber As Integer = 1
        Dim MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(file, System.Text.Encoding.GetEncoding("windows-1256"))
        Try
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(Chr(9))
            MyReader.HasFieldsEnclosedInQuotes = True ' to remove error "Line # cannot be parsed using the current Delimiters"
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Dim ImportRow As DataRow = dtImport.Rows.Add()
                Try
                    currentRow = MyReader.ReadFields()
                    ' if the count of the fields is less than 14 then we need to skip the row.
                    If currentRow.Length <> 4 Then
                        ImportRow("Status") = ("Line (" & RowNumber & ") is invalid and will be skipped.")
                    Else
                        ImportRow("RowNumber") = RowNumber
                        ImportRow("Status") = SuccessMessage

                        ImportRow("Code") = currentRow(0).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("Code")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "Code is Empty."
                        ElseIf ImportRow("Code").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Code > 50.")
                        End If

                        ImportRow("CompanyCode") = currentRow(1).ToString
                        If String.IsNullOrEmpty(ImportRow("CompanyCode")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "CompanyCode is Empty."
                        ElseIf ImportRow("CompanyCode").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CompanyCode > 50.")
                        End If

                        ImportRow("Description") = currentRow(2).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("Description")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "Description is Empty."
                        ElseIf ImportRow("Description").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Description > 50.")
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
                Application.DoEvents()
                pb.PerformStep()
            End While
            'End Using
            dtImport.AcceptChanges()

            ''''
            grd.DataSource = dtImport
            Dim TotalRowsCount As Integer = grdView.RowCount
            Dim ValidRowsCount As Integer = GetGridValidRows(grdView)
            Dim ExistIgroredCount As Integer = GetGridExistIgnoredRows(grdView)
            Dim ExistCount As Integer = GetGridExistRows(grdView)
            lblTotal.Text = String.Format("Total: {0}", TotalRowsCount)
            lblValid.Text = String.Format("Valid: {0}", ValidRowsCount)
            lblExist.Text = String.Format("Already Exist: {0}", ExistCount)
            lblExistIgnore.Text = String.Format("Ignored: {0}", ExistIgroredCount)
            lblInvalid.Text = String.Format("Invalid: {0}", TotalRowsCount - ValidRowsCount - ExistIgroredCount - ExistCount)
            Return True
        Catch ex As Exception
            If SilentImport Then
                SaveToLogFile(ex.Message, True)
            Else
                MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False

        Finally
            MyReader.Close()
            MyReader.Dispose()
            pb.Visible = False
        End Try
    End Function

    Private Function ProcessEmployeeFile(ByVal file As String, ByVal SilentImport As Boolean, ByVal pb As DevExpress.XtraEditors.ProgressBarControl) As Boolean
        pb.Visible = True
        pb.Position = 1
        pb.Properties.Step = 1
        pb.Refresh()
        pb.Properties.Maximum = GetFileLineCount(file)
        Application.DoEvents()
        Dim RowNumber As Integer = 1
        Dim MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(file, System.Text.Encoding.GetEncoding("windows-1256"))
        Try
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(Chr(9))
            MyReader.HasFieldsEnclosedInQuotes = True ' to remove error "Line # cannot be parsed using the current Delimiters"
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Dim ImportRow As DataRow = dtImport.Rows.Add()
                Try
                    currentRow = MyReader.ReadFields()
                    ' if the count of the fields is less than 14 then we need to skip the row.
                    If currentRow.Length <> 3 Then
                        ImportRow("Status") = ("Line (" & RowNumber & ") is invalid and will be skipped.")
                    Else
                        ImportRow("RowNumber") = RowNumber
                        ImportRow("Status") = SuccessMessage

                        ImportRow("Code") = currentRow(0).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("Code")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "Code is Empty."
                        ElseIf ImportRow("Code").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Code > 50.")
                        End If

                        ImportRow("CompanyCode") = currentRow(1).ToString
                        If String.IsNullOrEmpty(ImportRow("CompanyCode")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "CompanyCode is Empty."
                        ElseIf ImportRow("CompanyCode").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CompanyCode > 50.")
                        End If

                        ImportRow("Description") = currentRow(2).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("Description")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "Description is Empty."
                        ElseIf ImportRow("Description").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Description > 50.")
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
                Application.DoEvents()
                pb.PerformStep()
            End While
            'End Using
            dtImport.AcceptChanges()

            ''''
            grd.DataSource = dtImport
            Dim TotalRowsCount As Integer = grdView.RowCount
            Dim ValidRowsCount As Integer = GetGridValidRows(grdView)
            Dim ExistIgroredCount As Integer = GetGridExistIgnoredRows(grdView)
            Dim ExistCount As Integer = GetGridExistRows(grdView)
            lblTotal.Text = String.Format("Total: {0}", TotalRowsCount)
            lblValid.Text = String.Format("Valid: {0}", ValidRowsCount)
            lblExist.Text = String.Format("Already Exist: {0}", ExistCount)
            lblExistIgnore.Text = String.Format("Ignored: {0}", ExistIgroredCount)
            lblInvalid.Text = String.Format("Invalid: {0}", TotalRowsCount - ValidRowsCount - ExistIgroredCount - ExistCount)
            Return True
        Catch ex As Exception
            If SilentImport Then
                SaveToLogFile(ex.Message, True)
            Else
                MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        Finally
            MyReader.Close()
            MyReader.Dispose()
            pb.Visible = False
        End Try
    End Function
    Private Function ProcessAssetFile(ByVal file As String, ByVal SilentImport As Boolean, ByVal pb As DevExpress.XtraEditors.ProgressBarControl) As Boolean
        pb.Visible = True
        pb.Position = 1
        pb.Properties.Step = 1
        pb.Refresh()
        pb.Properties.Maximum = GetFileLineCount(file)
        Application.DoEvents()
        Dim objBALAssetDetails As New BALAssetDetails
        Dim RowNumber As Integer = 1
        Dim AssetDataTable As DataTable = objBALAssetDetails.GetAsset_Details(New attAssetDetails)
        Dim MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(file, System.Text.Encoding.GetEncoding("windows-1256"))
        Try
            'Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(file, System.Text.Encoding.GetEncoding("windows-1256"))
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(Chr(9))
            MyReader.HasFieldsEnclosedInQuotes = False ' to remove error "Line # cannot be parsed using the current Delimiters"
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Dim ImportRow As DataRow = dtImport.Rows.Add()
                Try
                    currentRow = MyReader.ReadFields()
                    ' if the count of the fields is less than 14 then we need to skip the row.
                    If currentRow.Length <> 10 Then
                        ImportRow("Status") = ("Line (" & RowNumber & ") is invalid and will be skipped.")
                    Else
                        ImportRow("RowNumber") = RowNumber
                        ImportRow("Status") = SuccessMessage

                        ImportRow("CompanyCode") = currentRow(0).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("CompanyCode")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "CompanyCode is Empty."
                        ElseIf ImportRow("CompanyCode").ToString.Length > 4 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CompanyCode > 4.")
                        End If

                        ImportRow("AssetNo") = currentRow(1).ToString.TrimStart("0").Trim
                        If String.IsNullOrEmpty(ImportRow("AssetNo")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "AssetNo is Empty."
                        ElseIf ImportRow("AssetNo").ToString.Length > 8 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "AssetNo > 8.")
                        End If

                        ImportRow("SubAssetNo") = currentRow(2).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("SubAssetNo")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "SubAssetNo is Empty."
                        ElseIf ImportRow("SubAssetNo").ToString.Length > 4 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "SubAssetNo > 4.")
                        End If
                        Dim AssetTag As String = ImportRow("CompanyCode") & ImportRow("AssetNo") & ImportRow("SubAssetNo")
                        'AssetTag
                        ImportRow("AssetTag") = AssetTag

                        ImportRow("AssetClass") = currentRow(3).ToString.TrimStart("0").Trim
                        If String.IsNullOrEmpty(ImportRow("AssetClass")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "AssetClass is Empty.")
                        ElseIf ImportRow("AssetClass").ToString.Length > 4 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "AssetClass > 4.")
                        End If


                        ImportRow("AssetDescription") = currentRow(4).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("AssetDescription")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "AssetDescription is Empty."
                        ElseIf ImportRow("AssetDescription").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "AssetDescription > 50.")
                        End If

                        ImportRow("SerialNo") = currentRow(5).ToString.Trim
                        If ImportRow("SerialNo").ToString.Length > 18 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "SerialNo > 12.")
                        End If

                        ImportRow("CostCenter") = currentRow(6).ToString.TrimStart("0").Trim
                        If ImportRow("CostCenter").ToString.Length > 10 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CostCenter > 10.")
                        End If


                        ImportRow("Plant") = currentRow(7).ToString.Trim
                        If ImportRow("Plant").ToString.Length > 4 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Plant > 4.")
                        End If


                        ImportRow("Location") = currentRow(8).ToString.Trim
                        If ImportRow("Location").ToString.Length > 10 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Location > 10.")
                        End If


                        If String.IsNullOrEmpty(ImportRow("Plant")) And Not String.IsNullOrEmpty(ImportRow("Location")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "Plant is Empty."
                        End If

                        ImportRow("PersonnelNumber") = currentRow(9).ToString.TrimStart("0").Trim
                        If ImportRow("PersonnelNumber").ToString.Length > 8 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "PersonnelNumber > 8.")
                        End If
                        'Always we need to replace the data, according to customer requirements.

                        'Check if record exist in the database if the record pass the validation.
                        'If ImportRow("Status").ToString = SuccessMessage Then
                        '    If objBALAssetDetails.Check_AstID(AssetTag, True) Then
                        '        Dim message As String = CheckExistRecord(AssetDataTable, AssetTag, ImportRow)
                        '        If message <> "" Then
                        '            ImportRow("Message") = message
                        '            ImportRow("Status") = ExistSuccessMessage
                        '        Else
                        '            ImportRow("Status") = String.Format("{0}", ExistMessage)
                        '        End If
                        '    End If
                        'End If

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
                Application.DoEvents()
                pb.PerformStep()
            End While
            'End Using
            dtImport.AcceptChanges()

            ''''
            grd.DataSource = dtImport
            Dim TotalRowsCount As Integer = grdView.RowCount
            Dim ValidRowsCount As Integer = GetGridValidRows(grdView)
            Dim ExistIgroredCount As Integer = GetGridExistIgnoredRows(grdView)
            Dim ExistCount As Integer = GetGridExistRows(grdView)
            lblTotal.Text = String.Format("Total: {0}", TotalRowsCount)
            lblValid.Text = String.Format("Valid: {0}", ValidRowsCount)
            lblExist.Text = String.Format("Already Exist: {0}", ExistCount)
            lblExistIgnore.Text = String.Format("Ignored: {0}", ExistIgroredCount)
            lblInvalid.Text = String.Format("Invalid: {0}", TotalRowsCount - ValidRowsCount - ExistIgroredCount - ExistCount)
            Return True
        Catch ex As Exception
            If SilentImport Then
                SaveToLogFile(ex.Message, True)
            Else
                MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False

        Finally
            MyReader.Close()
            MyReader.Dispose()
            pb.Visible = False
        End Try
    End Function

    Public Function SilentImport(ByVal lbl As DevExpress.XtraEditors.LabelControl, ByVal pb As DevExpress.XtraEditors.ProgressBarControl, ByVal FileName As String, ByVal ImportType As frmImport.TFileImportType) As String
        ImportFileName = FileName
        Select Case ImportType
            Case TFileImportType.Location
                lbl.Text = "Processing Data Location Master Change file..."
                lbl.Update()
                If CheckFile(FileName) Then
                    CreateLocationTempTable()
                    ProcessLocationFile(FileName, True, pb)
                    Application.DoEvents()
                    lbl.Text = "Importing Location Master Change file..."
                    lbl.Update()
                    Return ImportLocationData(True, pb)
                Else
                    Dim msg As String = String.Format("File not found, Incorrect path({0})", FileName)
                    SaveToLogFile(msg, True)
                    lbl.Text = msg
                    lbl.Update()
                    Return msg
                End If

            Case TFileImportType.CostCenter
                lbl.Text = "Processing Data Cost Center file..."
                lbl.Update()
                If CheckFile(FileName) Then
                    CreateCostCenterTempTable()
                    ProcessCostCenterFile(FileName, True, pb)
                    Application.DoEvents()
                    lbl.Text = "Importing Cost Center file..."
                    lbl.Update()
                    Return ImportCostCenterData(True, pb)
                Else
                    Dim msg As String = String.Format("File not found, Incorrect path({0})", FileName)
                    SaveToLogFile(msg, True)
                    lbl.Text = msg
                    lbl.Update()
                    Return msg
                End If

            Case TFileImportType.Employees
                lbl.Text = "Processing Data Employees file..."
                lbl.Update()
                If CheckFile(FileName) Then
                    CreateEmployeeTempTable()
                    ProcessEmployeeFile(FileName, True, pb)
                    Application.DoEvents()
                    lbl.Text = "Importing Employees file..."
                    lbl.Update()
                    Return ImportEmployeesData(True, pb)
                Else
                    Dim msg As String = String.Format("File not found, Incorrect path({0})", FileName)
                    SaveToLogFile(msg, True)
                    lbl.Text = msg
                    lbl.Update()
                    Return msg
                End If
            Case TFileImportType.Asset
                lbl.Text = "Processing Data Assets Master file..."
                lbl.Update()
                If CheckFile(FileName) Then
                    CreateAssetTempTable()
                    ProcessAssetFile(FileName, True, pb)
                    Application.DoEvents()
                    lbl.Text = "Importing Asset Master Creation file..."
                    lbl.Update()
                    Return ImportAssetData(True, pb)
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
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Function GetGridSelectedRows(ByVal grd As DevExpress.XtraGrid.Views.Grid.GridView) As Integer
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Selection", True)
        Dim Count As Integer = grd.RowCount
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Selection", Nothing)
        Return Count
    End Function
    Private Function GetGridExistRows(ByVal grd As DevExpress.XtraGrid.Views.Grid.GridView) As Integer
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Status", "%" & ExistSuccessMessage)
        Dim Count As Integer = grd.RowCount
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Status", Nothing)
        Return Count
    End Function
    Private Function GetGridExistIgnoredRows(ByVal grd As DevExpress.XtraGrid.Views.Grid.GridView) As Integer
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Status", "%" & ExistMessage)
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

    Private Function GetGridExistSelectedRows(ByVal grd As DevExpress.XtraGrid.Views.Grid.GridView) As Integer
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Selection", True)
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Status", "%" & ExistSuccessMessage)
        Dim Count As Integer = grd.RowCount
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Status", Nothing)
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Selection", Nothing)
        Return Count
    End Function

    Private Function ImportAssetData(ByVal SilentImport As Boolean, ByVal pb As DevExpress.XtraEditors.ProgressBarControl) As String
        Dim Start As Integer = Environment.TickCount
        Dim importedcount As Integer = 0
        pb.Visible = True
        pb.Position = 1
        pb.Properties.Step = 1
        pb.Properties.Maximum = grdView.RowCount
        Dim objBALAst_INV_Schedule As New BALAst_INV_Schedule
        Dim dtInvSch As DataTable = objBALAst_INV_Schedule.Getcombo_Schedule()

        Dim objAssets As New BALItems
        Dim dtAssets As DataTable = objAssets.GetAllData_Joined(New attItems)
        Dim ObjAssetsDetails As New BALAssetDetails
        Dim dtAssetsDetails As DataTable = ObjAssetsDetails.GetAsset_Details(New attAssetDetails)

        For i As Integer = 0 To grdView.RowCount - 1
            'Import Selected and Success rows only.

            If grdView.GetRowCellValue(i, ("Status")).ToString.Contains(SuccessMessage) And CBool(grdView.GetRowCellValue(i, ("Selection"))) = True Then
                Dim CompanyID As String = ImportCompany(grdView.GetRowCellValue(i, ("CompanyCode")), "Abunayyan " & grdView.GetRowCellValue(i, ("CompanyCode")))
                'Import Category.
                Dim CatID As String = ImportCategory(grdView.GetRowCellValue(i, ("AssetClass")), grdView.GetRowCellValue(i, ("AssetClass")))
                'Import location.
                Dim loc As AssetLocation
                Dim LocationStr As String = grdView.GetRowCellValue(i, ("Location"))

                loc.PlantCode = grdView.GetRowCellValue(i, ("Plant"))
                loc.PlantDesc = grdView.GetRowCellValue(i, ("Plant"))
                loc.LocationCode = LocationStr
                loc.LocationDesc = LocationStr
                loc.CompanyCode = grdView.GetRowCellValue(i, ("CompanyCode"))

                Dim LocID As String = ImportLocation(loc, False)
                'Import Assets
                Dim CustodianID As String = ImportEmp(grdView.GetRowCellValue(i, ("PersonnelNumber")), grdView.GetRowCellValue(i, ("PersonnelNumber")), grdView.GetRowCellValue(i, ("CompanyCode")), False) '
                Dim CostCenterID As String = ImportCostCenter(grdView.GetRowCellValue(i, ("CostCenter")), grdView.GetRowCellValue(i, ("CostCenter")), grdView.GetRowCellValue(i, ("CompanyCode")), False)

                If ImportAsset(grdView.GetRowCellValue(i, ("AssetTag")), grdView.GetRowCellValue(i, ("AssetNo")), grdView.GetRowCellValue(i, ("SubAssetNo")), grdView.GetRowCellValue(i, ("CompanyCode")), CompanyID, grdView.GetRowCellValue(i, ("AssetDescription")), grdView.GetRowCellValue(i, ("SerialNo")), "", CostCenterID, CustodianID, LocID, CatID, dtAssets, dtAssetsDetails, dtInvSch) Then
                    importedcount += 1
                End If
            End If
            pb.PerformStep()
            Application.DoEvents()

        Next
        Dim msg As String = "Import Assets file Completed, imported Asset count = (" & importedcount & "), Ignored asset count = (" & lblExistIgnore.Text & "), Invalid asset count = (" & lblInvalid.Text & ")"
        SaveToLogFile(msg, False)
        DeleteFileAfterImport()
        pb.Visible = False
        If Not SilentImport Then
            Dim time As String = (Environment.TickCount - Start) / 1000 & " S"
            MessageBox.Show("Import Completed, imported Asset count = " & importedcount & ". Time(" & time & ")", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
        Return msg
    End Function

    Private Function DeleteFileAfterImport() As Boolean
        Try
            If IO.File.Exists(ImportFileName) Then
                IO.File.Delete(ImportFileName)
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ImportLocationData(ByVal SilentProcess As Boolean, ByVal pb As DevExpress.XtraEditors.ProgressBarControl) As String
        Dim Start As Integer = Environment.TickCount
        Dim importedcount As Integer = 0
        pb.Visible = True
        pb.Position = 1
        pb.Properties.Step = 1
        pb.Properties.Maximum = grdView.RowCount
        For i As Integer = 0 To grdView.RowCount - 1
            'Import Selected and Success rows only.
            If grdView.GetRowCellValue(i, ("Status")).ToString.Contains(SuccessMessage) And CBool(grdView.GetRowCellValue(i, ("Selection"))) = True Then
                Dim loc As AssetLocation
                loc.PlantCode = grdView.GetRowCellValue(i, ("PlantCode"))
                loc.PlantDesc = grdView.GetRowCellValue(i, ("PlantDesc"))

                loc.LocationCode = grdView.GetRowCellValue(i, ("LocationCode"))
                loc.LocationDesc = grdView.GetRowCellValue(i, ("LocationDesc"))
                loc.CompanyCode = grdView.GetRowCellValue(i, ("CompanyCode"))
                Dim LocID As String = ImportLocation(loc, True)
                importedcount += 1
            End If
            pb.PerformStep()
            Application.DoEvents()

        Next
        Dim msg As String = "Import Location file Completed, imported Location count = (" & importedcount & "), Ignored Location count = (" & lblExistIgnore.Text & "), Invalid Location count = (" & lblInvalid.Text & ")"
        SaveToLogFile(msg, False)
        DeleteFileAfterImport()
        pb.Visible = False
        If Not SilentProcess Then
            Dim time As String = (Environment.TickCount - Start) / 1000 & " S"
            MessageBox.Show("Import Completed, imported Location count = " & importedcount & ". Time(" & time & ")", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
        Return msg
    End Function

    Private Function ImportCostCenterData(ByVal SilentProcess As Boolean, ByVal pb As DevExpress.XtraEditors.ProgressBarControl) As String
        Dim Start As Integer = Environment.TickCount
        Dim importedcount As Integer = 0
        pb.Visible = True
        pb.Position = 1
        pb.Properties.Step = 1
        pb.Properties.Maximum = grdView.RowCount
        For i As Integer = 0 To grdView.RowCount - 1
            'Import Selected and Success rows only.
            If grdView.GetRowCellValue(i, ("Status")).ToString.Contains(SuccessMessage) And CBool(grdView.GetRowCellValue(i, ("Selection"))) = True Then
                Dim CostCenterCode As String = grdView.GetRowCellValue(i, ("Code"))
                Dim CostCenterDesc As String = grdView.GetRowCellValue(i, ("Description"))
                Dim CostCenterCompanyCode As String = grdView.GetRowCellValue(i, ("CompanyCode"))
                ImportCostCenter(CostCenterCode, CostCenterDesc, CostCenterCompanyCode, True)
                importedcount += 1
            End If
            pb.PerformStep()
            Application.DoEvents()

        Next
        Dim msg As String = "Import Cost Center file Completed, imported  Cost Center count = (" & importedcount & "), Ignored  Cost Center count = (" & lblExistIgnore.Text & "), Invalid  Cost Center count = (" & lblInvalid.Text & ")"
        SaveToLogFile(msg, False)
        DeleteFileAfterImport()
        pb.Visible = False
        If Not SilentProcess Then
            Dim time As String = (Environment.TickCount - Start) / 1000 & " S"
            MessageBox.Show("Import Completed, imported  Cost Center count = " & importedcount & ". Time(" & time & ")", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
        Return msg
    End Function

    Private Function ImportEmployeesData(ByVal SilentProcess As Boolean, ByVal pb As DevExpress.XtraEditors.ProgressBarControl) As String
        Dim Start As Integer = Environment.TickCount
        Dim importedcount As Integer = 0
        pb.Visible = True
        pb.Position = 1
        pb.Properties.Step = 1
        pb.Properties.Maximum = grdView.RowCount
        For i As Integer = 0 To grdView.RowCount - 1
            'Import Selected and Success rows only.
            If grdView.GetRowCellValue(i, ("Status")).ToString.Contains(SuccessMessage) And CBool(grdView.GetRowCellValue(i, ("Selection"))) = True Then
                Dim EmpCode As String = grdView.GetRowCellValue(i, ("Code"))
                Dim EmpDesc As String = grdView.GetRowCellValue(i, ("Description"))
                Dim EmpCompanyCode As String = grdView.GetRowCellValue(i, ("CompanyCode"))
                ImportEmp(EmpDesc, EmpCode, EmpCompanyCode, True)
                importedcount += 1
            End If
            pb.PerformStep()
            Application.DoEvents()
        Next
        Dim msg As String = "Import Employees file Completed, imported Employees count = (" & importedcount & "), Ignored Employees count = (" & lblExistIgnore.Text & "), Invalid Employees count = (" & lblInvalid.Text & ")"
        SaveToLogFile(msg, False)
        DeleteFileAfterImport()
        pb.Visible = False
        If Not SilentProcess Then
            Dim time As String = (Environment.TickCount - Start) / 1000 & " S"
            MessageBox.Show("Import Completed, imported Employees count = " & importedcount & ". Time(" & time & ")", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
        Return msg
    End Function
    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        'Show confirmation message.
        Dim ValidSelectedRows As Integer = GetGridValidSelectedRows(grdView)
        Dim ValidRows As Integer = GetGridValidRows(grdView)
        Dim ExistSelectedRows As Integer = GetGridExistSelectedRows(grdView)
        Dim ExistRows As Integer = GetGridExistRows(grdView)

        If ValidSelectedRows + ExistSelectedRows < 1 Then
            MessageBox.Show("There is no valid selected rows!", " ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1)
            Return
        End If
        If MessageBox.Show("Are you sure you want to import? Only valid selected rows will be imported.", " ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1).ToString().Equals("Yes") Then
            If MessageBox.Show(String.Format("You are going to import ({0}) out of ({1}) valid rows, and ({2}) out of ({3}) Already Exist rows, Continue?", ValidSelectedRows, ValidRows, ExistSelectedRows, ExistRows), " ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1).ToString().Equals("Yes") Then
                Select Case ImportType
                    Case TFileImportType.Asset
                        ImportAssetData(False, pb)
                    Case TFileImportType.Location
                        ImportLocationData(False, pb)
                    Case TFileImportType.CostCenter
                        ImportCostCenterData(False, pb)
                    Case TFileImportType.Employees
                        ImportEmployeesData(False, pb)
                End Select
                Me.Close()

            End If
        End If
    End Sub

    Private Function ImportCompany(ByVal CompanyCode As String, ByVal CompanyName As String) As String
        Dim objattCompany As New attcompany
        Dim objBALCompany As New BALCompany
        Dim companyID As Long = objBALCompany.GetCompanyIDByCompanyCode(CompanyCode)
        If companyID > 0 Then
            Return companyID
        Else
            objattCompany.PKeyCode = objBALCompany.GetNextPKey_Company()
            objattCompany.CompanyCode = CompanyCode
            objattCompany.CompanyName = CompanyName
            objattCompany.BarStructID = 1
            objattCompany.HierCode = 1
            objBALCompany.Insert_Company(objattCompany)

            Dim objattBookTempl As New attBook
            Dim objBALBookTempl As New BALBooks
            objattBookTempl.PKeyCode = objBALBookTempl.GetNextPKey_Book()
            objattBookTempl.DepCode = 1
            objattBookTempl.Description = CompanyCode & " Book"
            objattBookTempl.CompanyID = objattCompany.PKeyCode
            objBALBookTempl.Insert_Book(objattBookTempl)

            Return objattCompany.PKeyCode
        End If
    End Function

    Private Function ImportCostCenter(ByVal CostNumber As String, ByVal CostDesc As String, ByVal CompanyCode As String, ByVal ReplaceIfFound As Boolean) As String
        If String.IsNullOrEmpty(CostNumber) Then
            Return "1"
        Else
            Dim objattCostCenter As New attCostCenter
            Dim objBALCostCenter As New BALCostCenter
            Dim objCompany As New BALCompany

            objattCostCenter.CostNumber = CostNumber
            Dim CostCenterID As String = ""
            Dim dt As DataTable = objBALCostCenter.CheckID(objattCostCenter)
            If dt.Rows.Count > 0 Then
                CostCenterID = dt.Rows(0)("CostID")
                If ReplaceIfFound Then
                    Dim CompanyID As Integer = objCompany.GetCompanyIDByCompanyCode(CompanyCode)
                    objattCostCenter.PKeyCode = CostCenterID
                    objattCostCenter.CostNumber = CostNumber
                    objattCostCenter.CostName = CostDesc
                    objattCostCenter.CompanyID = CompanyID
                    objBALCostCenter.Update_CostCenter(objattCostCenter)
                End If
            Else
                Dim CompanyID As Integer = objCompany.GetCompanyIDByCompanyCode(CompanyCode)
                objattCostCenter.PKeyCode = objBALCostCenter.GetNextPKey_CostCenter()
                objattCostCenter.CostNumber = CostNumber
                objattCostCenter.CostName = CostDesc
                objattCostCenter.CompanyID = CompanyID
                objBALCostCenter.Insert_CostCenter(objattCostCenter)
                CostCenterID = objattCostCenter.PKeyCode
            End If
            Return CostCenterID
        End If
    End Function

    Private Function ImportEmp(ByVal EmpName As String, ByVal EmpID As String, ByVal CompanyCode As String, ByVal ReplaceIfFound As Boolean) As String
        If String.IsNullOrEmpty(EmpID) Then
            Return "1"
        Else
            Dim objattCustodian As New attCustodian
            Dim objBALCustodian As New BALCustodian
            objattCustodian.PKeyCode = EmpID
            Dim dt As DataTable = objBALCustodian.CheckID(objattCustodian)
            Dim CustodianID As String = ""
            If dt.Rows.Count > 0 Then
                CustodianID = dt.Rows(0)("CustodianID")
                If ReplaceIfFound Then
                    objattCustodian.PKeyCode = EmpID
                    objattCustodian.CustodianName = EmpName
                    objattCustodian.DepartmentID = CompanyCode
                    objattCustodian.DesignationID = dt.Rows(0)("DesignationID")
                    objattCustodian.CustodianAddress = dt.Rows(0)("CustodianAddress")
                    objattCustodian.CustodianPhone = dt.Rows(0)("CustodianPhone")
                    objattCustodian.CustodianEmail = dt.Rows(0)("CustodianEmail")
                    objattCustodian.CustodianFax = dt.Rows(0)("CustodianFax")
                    objattCustodian.CustodianCell = dt.Rows(0)("CustodianCell")
                    objattCustodian.CustodianCode = ""
                    objBALCustodian.Update_Custodian(objattCustodian)
                End If
            Else
                objattCustodian.PKeyCode = EmpID
                objattCustodian.CustodianAddress = ""
                objattCustodian.CustodianName = EmpName
                objattCustodian.DepartmentID = CompanyCode
                objattCustodian.DesignationID = 1
                objattCustodian.CustodianCode = ""
                objBALCustodian.Insert_Custodian(objattCustodian)
                CustodianID = objattCustodian.PKeyCode
            End If
            Return CustodianID
        End If

    End Function

    Private Function GetCompleteLocationCode(ByVal loc As AssetLocation) As String
        Dim CompleteCode As String = loc.PlantCode
        If loc.LocationCode <> "" Then
            CompleteCode &= " \ " & loc.LocationCode
        End If
        Return CompleteCode
    End Function


    Private Function ImportLocation(ByVal loc As AssetLocation, ByVal ReplaceIfFound As Boolean) As String
        Dim objattLocation As New attLocation
        Dim objBALLocation As New BALLocation
        Dim objCompany As New BALCompany

        Dim PlantID As String = ""
        Dim LocationID As String = ""
        Dim PlantDesc As String = ""

        Dim CompleteLocCode As String = GetCompleteLocationCode(loc)
        If String.IsNullOrEmpty(CompleteLocCode) Then
            Return "1"
        End If

        PlantID = objBALLocation.GetLocIDByCompleteCode(loc.PlantCode)
        If Not String.IsNullOrEmpty(loc.PlantCode) Then
            If PlantID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = loc.PlantDesc
                PlantID = objBALLocation.GetRootLocID()
                objattLocation.HierCode = PlantID
                objattLocation.Code = loc.PlantCode
                objattLocation.locLevel = 0
                objattLocation.CompanyID = objCompany.GetCompanyIDByCompanyCode(loc.CompanyCode)
                objBALLocation.Insert_Location(objattLocation)
            Else 'update the location
                If ReplaceIfFound Then
                    objattLocation = New attLocation
                    objattLocation.HierCode = PlantID
                    objattLocation.Description = loc.PlantDesc
                    objattLocation.Code = loc.PlantCode
                    objattLocation.CompanyID = objCompany.GetCompanyIDByCompanyCode(loc.CompanyCode)
                    objattLocation.locLevel = 0
                    objBALLocation.Update_Location(objattLocation)
                End If
            End If
        Else
            Return "1" 'Unknown location
        End If

        LocationID = objBALLocation.GetLocIDByCompleteCode(CompleteLocCode)
        If Not String.IsNullOrEmpty(loc.LocationCode) Then
            If LocationID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = loc.LocationDesc
                LocationID = objBALLocation.GetChildLocID(PlantID)
                objattLocation.HierCode = LocationID
                objattLocation.Code = loc.LocationCode
                objattLocation.locLevel = 1
                objattLocation.CompanyID = objCompany.GetCompanyIDByCompanyCode(loc.CompanyCode)
                objBALLocation.Insert_Location(objattLocation)
            Else
                If ReplaceIfFound Then
                    objattLocation = New attLocation
                    objattLocation.HierCode = LocationID
                    objattLocation.Description = loc.LocationDesc
                    objattLocation.Code = loc.LocationCode
                    objattLocation.locLevel = 1
                    objattLocation.CompanyID = objCompany.GetCompanyIDByCompanyCode(loc.CompanyCode)
                    objBALLocation.Update_Location(objattLocation)
                End If
            End If
        Else
            Return PlantID
        End If
        Return LocationID
    End Function


    Private Function ImportAsset(ByVal AssetTag As String, ByVal AssetNumber As String, ByVal SubNumber As String, ByVal CompanyCode As String, ByVal CompanyID As String, ByVal AssetDesc As String, ByVal Serial As String, ByVal Modle As String, ByVal CostCenterID As String, ByVal CustodianID As String, ByVal LocID As String, ByVal CatID As String, ByRef dtAssets As DataTable, ByVal dtAssetDetails As DataTable, ByVal dtInvSch As DataTable) As Boolean
        'Add Asset Item
        Try
            Dim AssetItemID As String = ""
            AssetDesc = AssetDesc.ToString.Replace("'", "")
            Dim AssetRows As DataRow() = dtAssets.Select("AstDesc = '" & AssetDesc & "' and AstCatID = '" & CatID & "'")
            If AssetRows.Length > 0 Then
                AssetItemID = AssetRows(0)(0)
            Else
                Dim objBALAssets As New BALItems
                Dim objattAssets As New attItems
                objattAssets.AstCatID = CatID
                objattAssets.AstDesc = AssetDesc
                AssetItemID = objBALAssets.GetNextPKey_Item()
                objattAssets.PKeyCode = AssetItemID
                objBALAssets.Insert_Item(objattAssets)
                Dim row As DataRow = dtAssets.NewRow()
                row("AstCatID") = CatID
                row("AstDesc") = AssetDesc
                row("itemcode") = AssetItemID
                dtAssets.Rows.Add(row)
            End If

            Dim objattAssetDetails As New attAssetDetails
            Dim objBALAssetDetails As New BALAssetDetails
            Dim AssetID As String = GenerateAssetID(CompanyCode, AssetNumber, SubNumber)
            objattAssetDetails.BaseCost = 0
            objattAssetDetails.CostCenterID = CostCenterID
            objattAssetDetails.CustodianID = CustodianID
            objattAssetDetails.InsID = 0
            objattAssetDetails.SuppID = ""
            objattAssetDetails.POCode = 0
            objattAssetDetails.SerailNo = Serial
            objattAssetDetails.Discount = 0
            objattAssetDetails.InvNumber = ""
            objattAssetDetails.ItemCode = AssetItemID
            objattAssetDetails.Tax = 0
            objattAssetDetails.TransRemarks = "SAP"
            objattAssetDetails.DispDate = Date.MinValue
            objattAssetDetails.CompanyID = CompanyID
            objattAssetDetails.AstBrandID = 1
            objattAssetDetails.AstDesc = AssetDesc
            objattAssetDetails.AstDesc2 = ""
            objattAssetDetails.AstModel = Modle
            objattAssetDetails.Capex = ""
            objattAssetDetails.PoErp = ""
            objattAssetDetails.Plate = ""
            objattAssetDetails.GRN = ""
            objattAssetDetails.RefCode = ""
            objattAssetDetails.GLCode = 1
            objattAssetDetails.PONumber = ""
            objattAssetDetails.LocID = LocID
            objattAssetDetails.NoPiece = 1
            objattAssetDetails.Disposed = "0"
            objattAssetDetails.IsSold = 0
            objattAssetDetails.IsDataChanged = False

            objattAssetDetails.AstNum = AssetID
            objattAssetDetails.PKeyCode = AssetID
            objattAssetDetails.BarCode = AssetTag

            objattAssetDetails.RefNo = String.Format("{0}-{1}-{2}", CompanyCode, AssetNumber, SubNumber)
            Dim DetailsRows As DataRow() = dtAssetDetails.Select("AstID = '" & AssetID & "'")
            If DetailsRows.Length > 0 Then
                Dim objattAsset As New attAssetDetails
                objattAssetDetails.PurDate = DetailsRows(0)("PurDate")
                objattAssetDetails.SrvDate = DetailsRows(0)("SrvDate")
                objattAssetDetails.InStockAsset = DetailsRows(0)("InStockAsset")
                objattAssetDetails.IsDataChanged = False

                objattAssetDetails.LastEditBY = AppConfig.LoginName
                objattAssetDetails.LastEditDate = Now.Date
                objBALAssetDetails.Update_AssetDetails(objattAssetDetails)
            Else
                objattAssetDetails.SrvDate = Now.Date
                objattAssetDetails.PurDate = Now.Date
                objattAssetDetails.InvSchCode = 1
                objattAssetDetails.InvStatus = 0
                objattAssetDetails.InStockAsset = True

                objattAssetDetails.CreatedBY = AppConfig.LoginName
                objattAssetDetails.LastEditBY = AppConfig.LoginName
                objattAssetDetails.LastEditDate = Now.Date
                objattAssetDetails.CreationDate = Now.Date
                objattAssetDetails.StatusID = 1

                If objBALAssetDetails.Insert_AssetDetails(objattAssetDetails, False) Then
                    objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, 0, 1, 0, objattAssetDetails.SrvDate, False)
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function GetAssetNumberFromAssetTag(ByVal AssetTag As String) As Long
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

    ' This function will import the Main and sub categories and return subcat ID
    Private Function ImportCategory(ByVal MainCat As String, ByVal SubCat As String) As String
        Dim objattCategory As attCategory
        Dim objBALCategory As New BALCategory
        Dim SubCatID As String = ""
        Dim MainCatID As String = objBALCategory.GetCatID(MainCat)
        objattCategory = New attCategory
        If MainCatID = "" Then 'CatDesc not found
            objattCategory.AstCatDesc = MainCat
            MainCatID = objBALCategory.GetRootCatID()
            objattCategory.AstCatID = MainCatID
            objattCategory.Code = MainCat
            objattCategory.catLevel = 0
            If objBALCategory.Insert_Category(objattCategory) Then
                AddDepPolicyForCat(objattCategory.AstCatID)
            End If
        End If

        'SubCatID = objBALCategory.GetCatIDByDesc(SubCat, MainCatID)
        'objattCategory = New attCategory
        'If SubCatID = "" Then
        '    objattCategory.AstCatDesc = SubCat
        '    SubCatID = objBALCategory.GetChildCatID(MainCatID)
        '    objattCategory.AstCatID = SubCatID
        '    objattCategory.Code = SubCatID
        '    objattCategory.catLevel = 1
        '    If objBALCategory.Insert_Category(objattCategory) Then
        '        AddDepPolicyForCat(objattCategory.AstCatID)
        '    End If
        'End If
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

   

    Private Sub grdView_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FocRow As Integer = grdView.FocusedRowHandle
        If FocRow >= 0 Then
            If grdView.GetRowCellValue(FocRow, "Status").ToString = ExistSuccessMessage Then
                lblMessage.Visible = True
                lblMessage.Text = grdView.GetRowCellValue(FocRow, "Message")
            Else
                lblMessage.Visible = False
                lblMessage.Text = ""
            End If
        Else
            lblMessage.Visible = False
            lblMessage.Text = ""
        End If
    End Sub

End Class