﻿Imports DevExpress.XtraGrid
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
        Items
        Employees
    End Enum

    Structure AssetLocation
        Dim City As String
        'Dim Building As String
        'Dim Floor As String
        'Dim Room As String
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

            Case TFileImportType.Items
                CreateItemsTempTable()
                If ProcessItemsFile(ImportFileName, False, pb) Then
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

        dtImport.Columns.Add("AssetNum", Type.GetType("System.String"))
        dtImport.Columns.Add("OracleAssetNumber", Type.GetType("System.String"))
        dtImport.Columns.Add("AssetDescription", Type.GetType("System.String"))
        dtImport.Columns.Add("ModelNum", Type.GetType("System.String"))

        dtImport.Columns.Add("DONum", Type.GetType("System.String")) '
        dtImport.Columns.Add("DODescription", Type.GetType("System.String")) '
        dtImport.Columns.Add("Incedent_No", Type.GetType("System.String")) '
        dtImport.Columns.Add("DO_Project_Type", Type.GetType("System.String")) '
        dtImport.Columns.Add("DO_Qty", Type.GetType("System.String")) '

        dtImport.Columns.Add("VendorCode", Type.GetType("System.String"))
        dtImport.Columns.Add("VendorName", Type.GetType("System.String"))

        dtImport.Columns.Add("PurchaseDate", Type.GetType("System.String"))
        dtImport.Columns.Add("City", Type.GetType("System.String"))
        dtImport.Columns.Add("WarrantyMonth", Type.GetType("System.String"))
        dtImport.Columns.Add("AssetStatus", Type.GetType("System.String")) '

        dtImport.Columns.Add("MainCat", Type.GetType("System.String"))
        dtImport.Columns.Add("CustodianID", Type.GetType("System.String"))
        dtImport.Columns.Add("CustodianName", Type.GetType("System.String"))
        dtImport.Columns.Add("Comments", Type.GetType("System.String"))

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

    Private Sub CreateItemsTempTable()
        dtImport = New DataTable
        dtImport.Columns.Add("Selection", Type.GetType("System.Boolean"))
        dtImport.Columns.Add("RowNumber", Type.GetType("System.Int32"))
        '
        dtImport.Columns.Add("ItemCode", Type.GetType("System.String"))
        dtImport.Columns.Add("ItemDescription", Type.GetType("System.String"))
        dtImport.Columns.Add("CategoryCode", Type.GetType("System.String"))

        dtImport.Columns.Add("Status", Type.GetType("System.String"))
        dtImport.Columns.Add("Message", Type.GetType("System.String"))
    End Sub


    Private Sub CreateEmployeeTempTable()
        dtImport = New DataTable
        dtImport.Columns.Add("Selection", Type.GetType("System.Boolean"))
        dtImport.Columns.Add("RowNumber", Type.GetType("System.Int32"))
        '
        dtImport.Columns.Add("Code", Type.GetType("System.String"))
        dtImport.Columns.Add("Name", Type.GetType("System.String"))
        dtImport.Columns.Add("Designation", Type.GetType("System.String"))
        dtImport.Columns.Add("Address", Type.GetType("System.String"))
        dtImport.Columns.Add("CostCenter", Type.GetType("System.String"))
        dtImport.Columns.Add("Hirarchy", Type.GetType("System.String"))

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


    Private Function ProcessItemsFile(ByVal file As String, ByVal SilentImport As Boolean, ByVal pb As DevExpress.XtraEditors.ProgressBarControl) As Boolean
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

                        ImportRow("ItemCode") = currentRow(0).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("ItemCode")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "ItemCode is Empty."
                        ElseIf ImportRow("ItemCode").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "ItemCode > 50.")
                        End If

                        ImportRow("ItemDescription") = currentRow(1).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("ItemDescription")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "ItemDescription is Empty."
                        ElseIf ImportRow("ItemDescription").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "ItemDescription > 50.")
                        End If

                        ImportRow("CategoryCode") = currentRow(2).ToString
                        If String.IsNullOrEmpty(ImportRow("CategoryCode")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "CategoryCode is Empty."
                        ElseIf ImportRow("CategoryCode").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CategoryCode > 50.")
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
                    If currentRow.Length <> 6 Then
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

                        ImportRow("Name") = currentRow(1).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("Name")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "Name is Empty."
                        ElseIf ImportRow("Name").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Name > 50.")
                        End If

                        ImportRow("Designation") = currentRow(2).ToString
                        If String.IsNullOrEmpty(ImportRow("Designation")) Then
                            ImportRow("Designation") = "Unknown"
                        ElseIf ImportRow("Designation").ToString.Length > 50 Then
                            ImportRow("Designation") = ImportRow("Designation").ToString.Substring(0, 50)
                        End If

                        ImportRow("Address") = currentRow(3).ToString
                        If ImportRow("Address").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Address > 50.")
                        End If

                        ImportRow("CostCenter") = currentRow(4).ToString
                        If String.IsNullOrEmpty(ImportRow("CostCenter")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "CostCenter is Empty."
                        ElseIf ImportRow("CostCenter").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CostCenter > 50.")
                        End If


                        ImportRow("Hirarchy") = currentRow(5).ToString
                        If String.IsNullOrEmpty(ImportRow("Hirarchy")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "CostCenter is Empty."
                        ElseIf ImportRow("Hirarchy").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Hirarchy > 50.")
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
                    If currentRow.Length <> 22 Then
                        ImportRow("Status") = ("Line (" & RowNumber & ") is invalid and will be skipped.")
                    Else
                        ImportRow("RowNumber") = RowNumber
                        ImportRow("Status") = SuccessMessage

                        ImportRow("AssetNum") = currentRow(0).ToString.Trim
                        Dim AssetNum As Double = 0
                        If String.IsNullOrEmpty(ImportRow("AssetNum")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "AssetNum is Empty."
                        ElseIf Not Double.TryParse(ImportRow("AssetNum"), AssetNum) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Invalid AssetNum.")
                        End If
                        ImportRow("OracleAssetNumber") = currentRow(1).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("OracleAssetNumber")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Product_No is Empty.")
                        ElseIf ImportRow("OracleAssetNumber").ToString.Length > 50 Then
                            ImportRow("OracleAssetNumber") = ImportRow("OracleAssetNumber").ToString.Substring(0, 50)
                        End If

                        ImportRow("AssetDescription") = currentRow(2).ToString.Trim.Replace("'", " ")
                        If String.IsNullOrEmpty(ImportRow("AssetDescription")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "Product_Name is Empty."
                        ElseIf ImportRow("AssetDescription").ToString.Length > 100 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Product_Name > 100.")
                        End If

                        ImportRow("ModelNum") = currentRow(3).ToString.Trim
                        If ImportRow("ModelNum").ToString.Length > 50 Then
                            ImportRow("ModelNum") = ImportRow("ModelNum").ToString.Substring(0, 50)
                        End If

                        ImportRow("DONum") = currentRow(4).ToString.Trim
                        If ImportRow("DONum").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "DONum > 50.")
                        End If


                        ImportRow("DODescription") = currentRow(5).ToString.Trim
                        If ImportRow("DODescription").ToString.Length > 255 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "DODescription > 255.")
                        End If

                        ImportRow("Incedent_No") = currentRow(6).ToString.Trim
                        If ImportRow("Incedent_No").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Incedent_No > 50.")
                        End If

                        ImportRow("DO_Project_Type") = currentRow(7).ToString.Trim
                        If ImportRow("DO_Project_Type").ToString.Length > 255 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "DO_Project_Type > 255.")
                        End If


                        ImportRow("DO_Qty") = currentRow(8).ToString.Trim
                        If ImportRow("DO_Qty").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "DO_Qty > 50.")
                        End If

                        ImportRow("VendorCode") = currentRow(9).ToString.Trim
                        If ImportRow("VendorCode").ToString.Length > 25 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "VendorCode > 25.")
                        End If

                        ImportRow("VendorName") = currentRow(10).ToString.Trim
                        If ImportRow("VendorName").ToString.Length > 100 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "VendorName > 100.")
                        End If


                        ImportRow("PurchaseDate") = currentRow(11).ToString.Trim
                        Dim PurchaseDate As Date = Now.Date
                        If String.IsNullOrEmpty(ImportRow("PurchaseDate")) Then
                            ImportRow("PurchaseDate") = Now.Date
                        End If
                        If Not Date.TryParse(ImportRow("PurchaseDate").ToString, PurchaseDate) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Invalid PurchaseDate.")
                        End If


                        ImportRow("City") = currentRow(13).ToString
                        If ImportRow("City").ToString.Length > 100 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "City > 100.")
                        End If

                        ImportRow("WarrantyMonth") = currentRow(14).ToString
                        If String.IsNullOrEmpty(ImportRow("WarrantyMonth").ToString) Then
                            ImportRow("WarrantyMonth") = 0
                        Else
                            ImportRow("WarrantyMonth") = CInt(ImportRow("WarrantyMonth").ToString) * 12
                        End If

                        ImportRow("AssetStatus") = currentRow(15).ToString
                        If ImportRow("AssetStatus").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "AssetStatus > 50.")
                        End If


                        ImportRow("MainCat") = currentRow(16).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("MainCat")) Then
                            ImportRow("MainCat") = "Unknown"
                        ElseIf ImportRow("MainCat").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "MainCat > 50.")
                        End If

                        ImportRow("CustodianID") = currentRow(17).ToString
                        If String.IsNullOrEmpty(ImportRow("CustodianID")) Then
                            ImportRow("CustodianID") = "1"
                        ElseIf ImportRow("CustodianID").ToString.Length > 25 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CustodianID > 25.")
                        End If


                        ImportRow("CustodianName") = currentRow(18).ToString
                        If String.IsNullOrEmpty(ImportRow("CustodianName")) Then
                            ImportRow("CustodianName") = "Others"
                        ElseIf ImportRow("CustodianName").ToString.Length > 50 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CustodianName > 50.")
                        End If


                        ImportRow("Comments") = currentRow(21).ToString
                        If ImportRow("Comments").ToString.Length > 255 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Comments > 255.")
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

            Case TFileImportType.Items
                lbl.Text = "Processing Data Cost Center file..."
                lbl.Update()
                If CheckFile(FileName) Then
                    CreateItemsTempTable()
                    ProcessItemsFile(FileName, True, pb)
                    Application.DoEvents()
                    lbl.Text = "Importing Cost Center file..."
                    lbl.Update()
                    Return ImportItemsData(True, pb)
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
        For i As Integer = 0 To grdView.RowCount - 1
            'Import Selected and Success rows only.

            If grdView.GetRowCellValue(i, ("Status")).ToString.Contains(SuccessMessage) And CBool(grdView.GetRowCellValue(i, ("Selection"))) = True Then
                Dim CompanyID As String = "1"
                'Import Category.
                Dim CatID As String = ImportCategory("IT Equipments", grdView.GetRowCellValue(i, ("MainCat")))
                'Import location.
                Dim loc As AssetLocation
                loc.City = grdView.GetRowCellValue(i, ("City"))
                'loc.Building = grdView.GetRowCellValue(i, ("Building"))
                'loc.Floor = grdView.GetRowCellValue(i, ("Floor"))
                'loc.Room = grdView.GetRowCellValue(i, ("Room"))
                loc.CompanyCode = CompanyID
                Dim LocID As String = ImportLocation(loc, False)


                Dim CustodianID As String = grdView.GetRowCellValue(i, ("CustodianID"))
                Dim CustodianName As String = grdView.GetRowCellValue(i, ("CustodianName"))
                CustodianID = ImportEmp(CustodianName, CustodianID, "", "Unknown", "", False)

                Dim SuppID As String = ImportSupplier(grdView.GetRowCellValue(i, ("VendorCode")), grdView.GetRowCellValue(i, ("VendorName")))
                Dim ItemCode As String = ImportItems(grdView.GetRowCellValue(i, ("AssetDescription")), CatID, False, grdView.GetRowCellValue(i, ("WarrantyMonth")))

                If ImportAsset(grdView.GetRowCellValue(i, ("AssetNum")), CompanyID, ItemCode, CustodianID, LocID, CatID, grdView.GetRowCellValue(i, ("AssetDescription")), grdView.GetRowCellValue(i, ("OracleAssetNumber")), SuppID, 1, "", grdView.GetRowCellValue(i, ("PurchaseDate")), grdView.GetRowCellValue(i, ("PurchaseDate")), 0, 0, 0, grdView.GetRowCellValue(i, ("ModelNum")), grdView.GetRowCellValue(i, ("DONum")), grdView.GetRowCellValue(i, ("DODescription")), grdView.GetRowCellValue(i, ("Incedent_No")), grdView.GetRowCellValue(i, ("DO_Project_Type")), grdView.GetRowCellValue(i, ("DO_Qty")), grdView.GetRowCellValue(i, ("WarrantyMonth")), grdView.GetRowCellValue(i, ("AssetStatus")), grdView.GetRowCellValue(i, ("Comments"))) Then
                    importedcount += 1
                End If
            End If
            pb.PerformStep()
            Application.DoEvents()
        Next
        Dim msg As String = "Import Assets file Completed, imported Asset count = (" & importedcount & "), Ignored asset count = (" & lblExistIgnore.Text & "), Invalid asset count = (" & lblInvalid.Text & ")"
        SaveToLogFile(msg, False)
        pb.Visible = False
        If Not SilentImport Then
            Dim time As String = (Environment.TickCount - Start) / 1000 & " S"
            MessageBox.Show("Import Completed, imported Asset count = " & importedcount & ". Time(" & time & ")", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
        Return msg
    End Function

    Private Function ImportSupplier(ByVal SupplierID As String, ByVal SupplierName As String) As String
        Dim objattSupplier As New attSupplier
        Dim objBALSupplier As New BALSupplier
        objattSupplier.PKeyCode = SupplierID
        Dim dt As DataTable = objBALSupplier.GetAll_Supplier(objattSupplier)
        If dt.Rows.Count > 0 Then
            SupplierID = dt.Rows(0)("SuppID")
        Else
            objattSupplier.PKeyCode = SupplierID
            objattSupplier.SuppName = SupplierName
            objBALSupplier.Insert_Supplier(objattSupplier)
            SupplierID = objattSupplier.PKeyCode
        End If
        Return SupplierID
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
                loc.City = grdView.GetRowCellValue(i, ("City"))
                'loc.Building = grdView.GetRowCellValue(i, ("Building"))
                'loc.Floor = grdView.GetRowCellValue(i, ("Floor"))
                'loc.Room = grdView.GetRowCellValue(i, ("Room"))
                loc.CompanyCode = grdView.GetRowCellValue(i, ("CompanyID"))
                Dim LocID As String = ImportLocation(loc, True)
                importedcount += 1
            End If
            pb.PerformStep()
            Application.DoEvents()

        Next
        Dim msg As String = "Import Location file Completed, imported Location count = (" & importedcount & "), Ignored Location count = (" & lblExistIgnore.Text & "), Invalid Location count = (" & lblInvalid.Text & ")"
        SaveToLogFile(msg, False)
        pb.Visible = False
        If Not SilentProcess Then
            Dim time As String = (Environment.TickCount - Start) / 1000 & " S"
            MessageBox.Show("Import Completed, imported Location count = " & importedcount & ". Time(" & time & ")", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
        Return msg
    End Function

    Private Function ImportItemsData(ByVal SilentProcess As Boolean, ByVal pb As DevExpress.XtraEditors.ProgressBarControl) As String
        Dim Start As Integer = Environment.TickCount
        Dim importedcount As Integer = 0
        pb.Visible = True
        pb.Position = 1
        pb.Properties.Step = 1
        pb.Properties.Maximum = grdView.RowCount
        For i As Integer = 0 To grdView.RowCount - 1
            'Import Selected and Success rows only.
            If grdView.GetRowCellValue(i, ("Status")).ToString.Contains(SuccessMessage) And CBool(grdView.GetRowCellValue(i, ("Selection"))) = True Then
                Dim ItemCode As String = grdView.GetRowCellValue(i, ("ItemCode"))
                Dim ItemDesc As String = grdView.GetRowCellValue(i, ("ItemDescription"))
                Dim CategoryCode As String = grdView.GetRowCellValue(i, ("CategoryCode"))
                ImportItems(ItemDesc, CategoryCode, True, 0)
                importedcount += 1
            End If
            pb.PerformStep()
            Application.DoEvents()

        Next
        Dim msg As String = "Import Items file Completed, imported Items count = (" & importedcount & "), Ignored  Cost Center count = (" & lblExistIgnore.Text & "), Invalid Items count = (" & lblInvalid.Text & ")"
        SaveToLogFile(msg, False)
        pb.Visible = False
        If Not SilentProcess Then
            Dim time As String = (Environment.TickCount - Start) / 1000 & " S"
            MessageBox.Show("Import Completed, imported Items count = " & importedcount & ". Time(" & time & ")", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                ImportEmp(grdView.GetRowCellValue(i, ("Name")), grdView.GetRowCellValue(i, ("Code")), grdView.GetRowCellValue(i, ("Hirarchy")), grdView.GetRowCellValue(i, ("Designation")), grdView.GetRowCellValue(i, ("Address")), True)
                importedcount += 1
            End If
            pb.PerformStep()
            Application.DoEvents()
        Next
        Dim msg As String = "Import Employees file Completed, imported Employees count = (" & importedcount & "), Ignored Employees count = (" & lblExistIgnore.Text & "), Invalid Employees count = (" & lblInvalid.Text & ")"
        SaveToLogFile(msg, False)
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
                    Case TFileImportType.Items
                        ImportItemsData(False, pb)
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

    Private Function ImportItems(ByVal ItemDesc As String, ByVal CategoryCode As String, ByVal ReplaceIfFound As Boolean, ByVal Warranty As Integer) As String
        If String.IsNullOrEmpty(ItemDesc) Then
            Return "1"
        Else
            Dim objattItem As New attItems
            Dim objBALItem As New BALItems
            Dim objCompany As New BALCompany
            Dim ItemCode As String = String.Empty
            objattItem.AstDesc = ItemDesc
            Dim dt As DataTable = objBALItem.CheckID(objattItem)
            If dt.Rows.Count > 0 Then
                ItemCode = dt.Rows(0)("ItemCode")
                If ReplaceIfFound Then
                    objattItem.PKeyCode = ItemCode
                    objattItem.AstDesc = ItemDesc
                    objattItem.AstCatID = CategoryCode
                    objattItem.AstBrandID = 0
                    objattItem.AstModel = ""
                    objattItem.AstQty = 0
                    objattItem.POItmID = ""
                    objattItem.Warranty = Warranty
                    objBALItem.Update_Item(objattItem)
                End If
            Else
                objattItem.PKeyCode = objBALItem.GetNextPKey_Item()
                objattItem.AstDesc = ItemDesc
                objattItem.AstCatID = CategoryCode
                objattItem.AstBrandID = 0
                objattItem.AstModel = ""
                objattItem.AstQty = 0
                objattItem.POItmID = ""
                objattItem.Warranty = Warranty
                objBALItem.Insert_Item(objattItem)
                ItemCode = objattItem.PKeyCode
            End If
            Return ItemCode
        End If
    End Function

    Private Function ImportDesignation(ByVal Designation As String) As String
        Dim objattDesignation As New attDesignation
        Dim objBALDesignation As New BALDesignation

        Dim DesignationID As String = ""
        objattDesignation.Description = Designation
        Dim dt As DataTable = objBALDesignation.GetDesignationID(objattDesignation)
        If dt.Rows.Count > 0 Then
            DesignationID = dt.Rows(0)("DesignationID")
        End If
        If DesignationID = "" Then 'CatDesc not found
            objattDesignation.Description = Designation
            DesignationID = objBALDesignation.GetNextPKey_Designation
            objattDesignation.PKeyCode = DesignationID
            objBALDesignation.Insert_Designation(objattDesignation)
        End If
        Return DesignationID
    End Function

    Private Function ImportCostCenter(ByVal Hirarchy As String, ByVal CostCenter As String) As String
        Dim objattCompGroup As New attCompGroups
        Dim objBalCompGroup As New BALCompGroups
        objattCompGroup.GrpDesc = Hirarchy
        Dim HirearchyID As String = 0
        Dim dt As DataTable = objBalCompGroup.GetAll_CompGroups(objattCompGroup)
        If dt.Rows.Count > 0 Then
            HirearchyID = dt.Rows(0)("GrpID")
        Else
            objattCompGroup.LvlID = 1
            objattCompGroup.PKeyCode = objBalCompGroup.GetNextPKey_CompGroupsByLevel(objattCompGroup.LvlID)
            objattCompGroup.GrpDesc = Hirarchy
            objBalCompGroup.Insert_CompGroups(objattCompGroup)
            HirearchyID = objattCompGroup.PKeyCode
        End If


        objattCompGroup = New attCompGroups
        objattCompGroup.GrpDesc = CostCenter
        Dim CostID As String = 0
        Dim dtCost As DataTable = objBalCompGroup.GetAll_CompGroups(objattCompGroup)
        If dtCost.Rows.Count > 0 Then
            CostID = dtCost.Rows(0)("GrpID")
        Else
            objattCompGroup.LvlID = 2
            objattCompGroup.PKeyCode = objBalCompGroup.GetNextPKey_CompGroupsByLevel(objattCompGroup.LvlID)
            objattCompGroup.GrpDesc = CostCenter
            objBalCompGroup.Insert_CompGroups(objattCompGroup)
            CostID = objattCompGroup.PKeyCode
        End If

        Dim objattOrg As New attOrgHier
        Dim objBalOrg As New BALOrgHier
        objattOrg.PKeyCode = HirearchyID & "-" & CostID
        Dim dtOrg As DataTable = objBalOrg.GetAll_OrgHier(objattOrg)
        If dtOrg.Rows.Count > 0 Then
            Return dtOrg.Rows(0)("HierCode")
        Else
            objattOrg.GrpID = CostID
            objattOrg.IsWareHouse = 0
            objBalOrg.Insert_OrgHier(objattOrg)
            Return objattOrg.PKeyCode
        End If
    End Function

    Private Function ImportEmp(ByVal EmpName As String, ByVal EmpID As String, ByVal Hirarchy As String, ByVal Designation As String, ByVal Address As String, ByVal ReplaceIfFound As Boolean) As String
        If String.IsNullOrEmpty(EmpID) Then
            Return "1"
        Else
            Dim objattCustodian As New attCustodian
            Dim objBALCustodian As New BALCustodian
            objattCustodian.PKeyCode = EmpID

            Dim objDesig As New BALDesignation
            Dim objattDesig As New attDesignation
            objattDesig.Description = Designation
            Dim dtDesign As DataTable = objDesig.GetDesignationID(objattDesig)
            Dim DesignationID As String = 0

            If dtDesign.Rows.Count > 0 Then
                DesignationID = dtDesign.Rows(0)("DesignationID")
            Else
                DesignationID = ImportDesignation(Designation)
            End If

            Dim dt As DataTable = objBALCustodian.CheckID(objattCustodian)
            Dim CustodianID As String = ""
            If dt.Rows.Count > 0 Then
                CustodianID = dt.Rows(0)("CustodianID")
                If ReplaceIfFound Then
                    objattCustodian.PKeyCode = EmpID
                    objattCustodian.CustodianName = EmpName
                    objattCustodian.DepartmentID = 1
                    objattCustodian.DesignationID = DesignationID
                    objattCustodian.CustodianAddress = Address
                    objattCustodian.CustodianPhone = dt.Rows(0)("CustodianPhone")
                    objattCustodian.CustodianEmail = dt.Rows(0)("CustodianEmail")
                    objattCustodian.CustodianFax = dt.Rows(0)("CustodianFax")
                    objattCustodian.CustodianCell = dt.Rows(0)("CustodianCell")
                    objattCustodian.CustodianCode = ""
                    objBALCustodian.Update_Custodian(objattCustodian)
                End If
            Else
                objattCustodian.PKeyCode = EmpID
                objattCustodian.CustodianAddress = Address
                objattCustodian.CustodianName = EmpName
                objattCustodian.DepartmentID = 1
                objattCustodian.DesignationID = DesignationID
                objattCustodian.CustodianCode = ""
                objBALCustodian.Insert_Custodian(objattCustodian)
                CustodianID = objattCustodian.PKeyCode
            End If
            Return CustodianID
        End If

    End Function

    Private Function GetCompleteLocationCode(ByVal loc As AssetLocation) As String
        Dim CompleteCode As String = "SABB \ " & loc.City
        'If loc.Building <> "" Then
        '    CompleteCode &= " \ " & loc.Building
        'End If

        'If loc.Floor <> "" Then
        '    CompleteCode &= " \ " & loc.Floor
        'End If

        'If loc.Room <> "" Then
        '    CompleteCode &= " \ " & loc.Room
        'End If
        Return CompleteCode
    End Function


    Private Function ImportLocation(ByVal loc As AssetLocation, ByVal ReplaceIfFound As Boolean) As String
        Dim objattLocation As New attLocation
        Dim objBALLocation As New BALLocation
        Dim objCompany As New BALCompany

        Dim CityID As String = ""
        'Dim BuildingID As String = ""
        'Dim FloorID As String = ""
        'Dim RoomID As String = ""

        Dim CompleteLocCode As String = GetCompleteLocationCode(loc)
        If String.IsNullOrEmpty(CompleteLocCode) Then
            Return "1"
        End If

        CityID = objBALLocation.GetLocIDByCompleteCode(CompleteLocCode)
        If Not String.IsNullOrEmpty(loc.City) Then
            If CityID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = loc.City
                CityID = objBALLocation.GetRootLocID()
                objattLocation.HierCode = CityID
                objattLocation.Code = loc.City
                objattLocation.locLevel = 0
                objattLocation.CompanyID = loc.CompanyCode
                objBALLocation.Insert_Location(objattLocation)
            Else 'update the location
                If ReplaceIfFound Then
                    objattLocation = New attLocation
                    objattLocation.HierCode = CityID
                    objattLocation.Description = loc.City
                    objattLocation.Code = loc.City
                    objattLocation.CompanyID = loc.CompanyCode
                    objattLocation.locLevel = 0
                    objBALLocation.Update_Location(objattLocation)
                End If
            End If
            Return CityID
        Else
            Return "1" 'Unknown location
        End If

        'BuildingID = objBALLocation.GetLocIDByCompleteCode(loc.City & " \ " & loc.Building)
        'If Not String.IsNullOrEmpty(loc.Building) Then
        '    If BuildingID = "0" Then
        '        objattLocation = New attLocation
        '        objattLocation.Description = loc.Building
        '        BuildingID = objBALLocation.GetChildLocID(CityID)
        '        objattLocation.HierCode = BuildingID
        '        objattLocation.Code = loc.Building
        '        objattLocation.locLevel = 1
        '        objattLocation.CompanyID = loc.CompanyCode
        '        objBALLocation.Insert_Location(objattLocation)
        '    Else
        '        If ReplaceIfFound Then
        '            objattLocation = New attLocation
        '            objattLocation.HierCode = BuildingID
        '            objattLocation.Description = loc.Building
        '            objattLocation.Code = loc.Building
        '            objattLocation.locLevel = 1
        '            objattLocation.CompanyID = loc.CompanyCode
        '            objBALLocation.Update_Location(objattLocation)
        '        End If
        '    End If
        'Else
        '    Return CityID
        'End If


        'FloorID = objBALLocation.GetLocIDByCompleteCode(loc.City & " \ " & loc.Building & " \ " & loc.Floor)
        'If Not String.IsNullOrEmpty(loc.Floor) Then
        '    If FloorID = "0" Then
        '        objattLocation = New attLocation
        '        objattLocation.Description = loc.Floor
        '        FloorID = objBALLocation.GetChildLocID(BuildingID)
        '        objattLocation.HierCode = FloorID
        '        objattLocation.Code = loc.Floor
        '        objattLocation.locLevel = 2
        '        objattLocation.CompanyID = loc.CompanyCode
        '        objBALLocation.Insert_Location(objattLocation)
        '    Else
        '        If ReplaceIfFound Then
        '            objattLocation = New attLocation
        '            objattLocation.HierCode = FloorID
        '            objattLocation.Description = loc.Floor
        '            objattLocation.Code = loc.Floor
        '            objattLocation.locLevel = 2
        '            objattLocation.CompanyID = loc.CompanyCode
        '            objBALLocation.Update_Location(objattLocation)
        '        End If
        '    End If
        'Else
        '    Return BuildingID
        'End If

        'RoomID = objBALLocation.GetLocIDByCompleteCode(loc.City & " \ " & loc.Building & " \ " & loc.Floor & " \ " & loc.Room)
        'If Not String.IsNullOrEmpty(loc.Room) Then
        '    If RoomID = "0" Then
        '        objattLocation = New attLocation
        '        objattLocation.Description = loc.Room
        '        RoomID = objBALLocation.GetChildLocID(FloorID)
        '        objattLocation.HierCode = RoomID
        '        objattLocation.Code = loc.Room
        '        objattLocation.locLevel = 3
        '        objattLocation.CompanyID = loc.CompanyCode
        '        objBALLocation.Insert_Location(objattLocation)
        '    Else
        '        If ReplaceIfFound Then
        '            objattLocation = New attLocation
        '            objattLocation.HierCode = RoomID
        '            objattLocation.Description = loc.Room
        '            objattLocation.Code = loc.Room
        '            objattLocation.locLevel = 3
        '            objattLocation.CompanyID = loc.CompanyCode
        '            objBALLocation.Update_Location(objattLocation)
        '        End If
        '    End If
        'Else
        '    Return FloorID
        'End If
        'Return RoomID
    End Function


    Private Function ImportAsset(ByVal AstNum As String, ByVal CompanyID As String, ByVal AssetItemID As String, ByVal CustodianID As String, ByVal LocID As String, ByVal CatID As String, ByVal AssetDesc As String, ByVal OracleRefNumber As String, ByVal SupplierID As String, ByVal ItemQTY As Double, ByVal PONo As String, ByVal PurchaseDate As Date, ByVal ServiceDate As Date, ByVal BaseCost As Double, ByVal AdditionalCost As Double, ByVal CurrentBookValue As Double, ByVal ModelNum As String, ByVal DONum As String, ByVal DODescription As String, ByVal Incedent_No As String, ByVal DO_Project_Type As String, ByVal DO_Qty As String, ByVal WarrantyMonth As Integer, ByVal AssetStatus As String, ByVal Comments As String) As Boolean
        'Add Asset Item
        Try
            Dim objattAssetDetails As New attAssetDetails
            Dim objBALAssetDetails As New BALAssetDetails

            For i As Integer = 1 To ItemQTY
                Dim AssetID As String = objBALAssetDetails.Generate_AssetID()
                objattAssetDetails.BaseCost = Math.Round(BaseCost / ItemQTY, 2)
                objattAssetDetails.CostCenterID = ""
                objattAssetDetails.CustodianID = CustodianID
                objattAssetDetails.InsID = 0
                objattAssetDetails.SuppID = SupplierID
                objattAssetDetails.POCode = PONo
                objattAssetDetails.SerailNo = ""
                objattAssetDetails.Discount = 0
                objattAssetDetails.InvNumber = ""
                objattAssetDetails.ItemCode = AssetItemID
                objattAssetDetails.Tax = Math.Round(AdditionalCost / ItemQTY, 2)
                objattAssetDetails.TransRemarks = ""
                objattAssetDetails.DispDate = Date.MinValue
                objattAssetDetails.CompanyID = CompanyID
                objattAssetDetails.AstBrandID = 1
                objattAssetDetails.AstDesc = AssetDesc
                objattAssetDetails.AstDesc2 = ""
                objattAssetDetails.AstModel = ModelNum
                objattAssetDetails.Capex = ""
                objattAssetDetails.PoErp = ""
                objattAssetDetails.Plate = ""
                objattAssetDetails.GRN = ""
                objattAssetDetails.RefCode = OracleRefNumber
                objattAssetDetails.GLCode = 1
                objattAssetDetails.PONumber = ""
                objattAssetDetails.LocID = LocID
                objattAssetDetails.NoPiece = 1
                objattAssetDetails.IsSold = 0
                objattAssetDetails.IsDataChanged = False

                objattAssetDetails.AstNum = AstNum
                objattAssetDetails.PKeyCode = AssetID
                objattAssetDetails.BarCode = OracleRefNumber
                If i = 1 Then
                    objattAssetDetails.RefNo = OracleRefNumber
                Else
                    objattAssetDetails.RefNo = OracleRefNumber & "-" & i
                End If

                objattAssetDetails.SrvDate = ServiceDate
                objattAssetDetails.PurDate = PurchaseDate
                objattAssetDetails.InvSchCode = 1
                objattAssetDetails.InvStatus = 0
                objattAssetDetails.InStockAsset = True

                objattAssetDetails.CreatedBY = AppConfig.LoginName
                objattAssetDetails.LastEditBY = AppConfig.LoginName
                objattAssetDetails.LastEditDate = Now.Date
                objattAssetDetails.CreationDate = Now.Date
                objattAssetDetails.Warranty = WarrantyMonth
                objattAssetDetails.CustomFld1 = DONum
                objattAssetDetails.CustomFld2 = DODescription
                objattAssetDetails.CustomFld3 = Incedent_No
                objattAssetDetails.CustomFld4 = DO_Project_Type
                objattAssetDetails.CustomFld5 = Comments
                objattAssetDetails.EvaluationGroup1 = DO_Qty
                If AssetStatus = "Disposed" Then
                    objattAssetDetails.Disposed = 1
                Else
                    objattAssetDetails.Disposed = 0
                End If
                objattAssetDetails.StatusID = objBALAssetDetails.GetAssetStatusByDesc(AssetStatus)

                If objBALAssetDetails.Insert_AssetDetails(objattAssetDetails, True) Then
                    objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, Math.Round(CurrentBookValue / ItemQTY, 2), objattAssetDetails.CompanyID, 0, 1, 0, objattAssetDetails.SrvDate, False)
                End If
            Next

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
        Dim CatID As String = objBALCategory.GetCatID(MainCat & "-" & SubCat)
        If Not String.IsNullOrEmpty(CatID) And CatID <> "0" Then
            Return CatID
        Else
            Dim MainCatID As String = objBALCategory.GetCatIDByDesc(MainCat, "")
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
            If Not String.IsNullOrEmpty(SubCat) Then
                SubCatID = objBALCategory.GetCatIDByDesc(SubCat, MainCatID)
                objattCategory = New attCategory
                If SubCatID = "" Then
                    objattCategory.AstCatDesc = SubCat
                    SubCatID = objBALCategory.GetChildCatID(MainCatID)
                    objattCategory.AstCatID = SubCatID
                    objattCategory.Code = SubCatID
                    objattCategory.catLevel = 1
                    If objBALCategory.Insert_Category(objattCategory) Then
                        AddDepPolicyForCat(objattCategory.AstCatID)
                    End If
                End If
                Return SubCatID
            Else
                Return MainCatID
            End If
        End If


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