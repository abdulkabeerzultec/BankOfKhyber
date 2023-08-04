Imports DevExpress.XtraGrid
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class frmImport
    Dim fileName As String = ""
    Dim dtImport As New DataTable
    Dim SuccessMessage As String = "Success."
    Dim ExistMessage As String = "AssetTag Already Exist. Ignore"
    Dim ExistSuccessMessage As String = String.Format("{0} {1}", "AssetTag Already Exist. ", SuccessMessage)
    Structure AssetLocation
        Dim CityCode As String
        Dim BuildingCode As String
        Dim FloorCode As String
        Dim RoomCode As String
        Dim CityName As String
        Dim BuildingName As String
        Dim FloorName As String
        Dim RoomName As String
    End Structure

    Private Sub frmImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub grdFormat()
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

        grdView.Columns("MainCategory").Caption = "Major Category"
        grdView.Columns("SubCategory").Caption = "Minor Category"

        grdView.Columns("CityCode").Visible = False
        grdView.Columns("BuildingCode").Visible = False
        grdView.Columns("FloorCode").Visible = False
        grdView.Columns("RoomCode").Visible = False
        grdView.Columns("Message").Visible = False
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

    Private Sub CreateTempDataTable()
        dtImport.Columns.Add("Selection", Type.GetType("System.Boolean"))
        dtImport.Columns.Add("RowNumber", Type.GetType("System.Int32"))
        dtImport.Columns.Add("AssetTag", Type.GetType("System.String"))
        dtImport.Columns.Add("AssetDescription", Type.GetType("System.String"))
        dtImport.Columns.Add("Model", Type.GetType("System.String"))
        dtImport.Columns.Add("Serial", Type.GetType("System.String"))
        dtImport.Columns.Add("MainCategory", Type.GetType("System.String"))
        dtImport.Columns.Add("SubCategory", Type.GetType("System.String"))
        dtImport.Columns.Add("CityCode", Type.GetType("System.String"))
        dtImport.Columns.Add("CityName", Type.GetType("System.String"))
        dtImport.Columns.Add("BuildingCode", Type.GetType("System.String"))
        dtImport.Columns.Add("BuildingName", Type.GetType("System.String"))
        dtImport.Columns.Add("FloorCode", Type.GetType("System.String"))
        dtImport.Columns.Add("FloorName", Type.GetType("System.String"))
        dtImport.Columns.Add("RoomCode", Type.GetType("System.String"))
        dtImport.Columns.Add("RoomName", Type.GetType("System.String"))
        dtImport.Columns.Add("Status", Type.GetType("System.String"))
        dtImport.Columns.Add("Message", Type.GetType("System.String"))
    End Sub
    Private Function GetFileLineCount(ByVal file As String) As Integer
        Dim array As String() = System.IO.File.ReadAllLines(file)
        Return array.Length
    End Function

    Private Function CheckExistRecord(ByVal AssetDataTable As DataTable, ByVal assetTag As String, ByVal ImportDataRow As DataRow) As String
        'Dim objattAssetDetails As New attAssetDetails
        'Dim objBALAssetDetails As New BALAssetDetails
        'objattAssetDetails.PKeyCode = assetTag
        Dim Message As String = ""
        ' Dim Row As DataTable = objBALAssetDetails.GetAsset_Details(objattAssetDetails)
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


    'Private Function
    Private Function CSVFileProcess(ByVal file As String) As Boolean
        pb.Visible = True
        pb.Value = 1
        pb.Step = 1
        pb.Refresh()
        pb.Maximum = GetFileLineCount(file)
        Application.DoEvents()
        Dim hTable As Hashtable = New Hashtable()
        Dim objBALAssetDetails As New BALAssetDetails
        Dim RowNumber As Integer = 1
        Dim AssetDataTable As DataTable = objBALAssetDetails.GetAsset_Details(New attAssetDetails)
        Dim MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(file, System.Text.Encoding.GetEncoding("windows-1256"))
        Try
            'Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(file, System.Text.Encoding.GetEncoding("windows-1256"))
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            MyReader.HasFieldsEnclosedInQuotes = False ' to remove error "Line # cannot be parsed using the current Delimiters"
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Dim ImportRow As DataRow = dtImport.Rows.Add()
                Try
                    currentRow = MyReader.ReadFields()
                    ' if the count of the fields is less than 14 then we need to skip the row.
                    If currentRow.Length <> 14 Then
                        ImportRow("Status") = ("Line (" & RowNumber & ") is invalid and will be skipped.")
                    Else
                        ImportRow("RowNumber") = RowNumber
                        ImportRow("Status") = SuccessMessage
                        Dim AssetTag As String = currentRow(0).ToString.Trim
                        If String.IsNullOrEmpty(AssetTag) Then
                            ImportRow("Status") = "AssetTag is empty."
                        ElseIf AssetTag.Length > 15 Then
                            ImportRow("Status") = "AssetTag size > 15."
                        Else
                            ' check for doublicated AssetTag
                            'Add list of all the unique item value to hashtable, which stores combination of key, value pair.
                            'And add duplicate item value in arraylist.
                            If hTable.Contains(AssetTag) Then
                                'Locate all rows, and mark them as duplicated.
                                'because we should regect All dublicated asset tags.
                                Dim dra As DataRow() = dtImport.Select("AssetTag = '" & AssetTag & "'")
                                For Each row As DataRow In dra
                                    row("Status") = "AssetTag duplicated."
                                    'unSelect the old dublicated rows.
                                    row("Selection") = False
                                Next
                                ImportRow("Status") = "AssetTag duplicated."
                            Else
                                hTable.Add(AssetTag, String.Empty)
                            End If
                        End If
                        ImportRow("AssetTag") = AssetTag
                        ImportRow("AssetDescription") = currentRow(1).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("AssetDescription")) Then
                            ImportRow("AssetDescription") = "Empty"
                        ElseIf ImportRow("AssetDescription").ToString.Length > 80 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "AssetDescription > 80.")
                        End If

                        ImportRow("Model") = currentRow(2).ToString.Trim
                        If ImportRow("Model").ToString.Length > 40 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Model > 40.")
                        End If
                        ImportRow("Serial") = currentRow(3).ToString.Trim
                        If ImportRow("Serial").ToString.Length > 35 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "Serial > 35.")
                        End If

                        ImportRow("MainCategory") = currentRow(4).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("MainCategory")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "MainCategory is Empty.")
                        ElseIf ImportRow("MainCategory").ToString.Length > 30 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "MainCategory > 30.")
                        End If

                        ImportRow("SubCategory") = currentRow(5).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("SubCategory")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "SubCategory is Empty.")
                        ElseIf ImportRow("SubCategory").ToString.Length > 30 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "SubCategory > 30.")
                        End If

                        ImportRow("CityCode") = currentRow(6).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("CityCode")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = "CityCode is Empty."
                        ElseIf ImportRow("CityCode").ToString.Length > 15 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CityCode > 15.")
                        End If

                        ImportRow("BuildingCode") = currentRow(7).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("BuildingCode")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "BuildingCode is Empty.")
                        ElseIf ImportRow("BuildingCode").ToString.Length > 15 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "BuildingCode > 15.")
                        End If
                        ImportRow("FloorCode") = currentRow(8).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("FloorCode")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "FloorCode is Empty.")
                        ElseIf ImportRow("FloorCode").ToString.Length > 15 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "FloorCode > 15.")
                        End If
                        ImportRow("RoomCode") = currentRow(9).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("RoomCode")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "RoomCode is Empty.")
                        ElseIf ImportRow("RoomCode").ToString.Length > 15 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "RoomCode > 15.")
                        End If
                        ImportRow("CityName") = currentRow(10).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("CityName")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CityName is Empty.")
                        ElseIf ImportRow("CityName").ToString.Length > 30 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "CityName > 30.")
                            'ImportRow("CityName") = "Empty"
                        End If
                        ImportRow("BuildingName") = currentRow(11).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("BuildingName")) Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "BuildingName is Empty.")
                        ElseIf ImportRow("BuildingName").ToString.Length > 30 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "BuildingName > 30.")
                            'ImportRow("BuildingName") = "Empty"
                        End If
                        ImportRow("FloorName") = currentRow(12).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("FloorName")) Then
                            'ImportRow("FloorName") = "Empty"
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "FloorName is Empty.")
                        ElseIf ImportRow("FloorName").ToString.Length > 30 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "FloorName > 30.")
                        End If
                        ImportRow("RoomName") = currentRow(13).ToString.Trim
                        If String.IsNullOrEmpty(ImportRow("RoomName")) Then
                            'ImportRow("RoomName") = "Empty"
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "RoomName is Empty.")
                        ElseIf ImportRow("RoomName").ToString.Length > 30 Then
                            ImportRow("Status") = ImportRow("Status").ToString.Replace(SuccessMessage, "")
                            ImportRow("Status") = String.Concat(ImportRow("Status"), "RoomName > 30.")
                        End If
                        'Check if record exist in the database if the record pass the validation.
                        If ImportRow("Status").ToString = SuccessMessage Then
                            If objBALAssetDetails.Check_AstID(AssetTag, True) Then
                                Dim message As String = CheckExistRecord(AssetDataTable, AssetTag, ImportRow)
                                If message <> "" Then
                                    ImportRow("Message") = message
                                    ImportRow("Status") = ExistSuccessMessage
                                Else
                                    ImportRow("Status") = String.Format("{0}", ExistMessage)
                                End If
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
            MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            MyReader.Close()
            MyReader.Dispose()
            hTable.Clear()
            pb.Visible = False
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
                Dim Start As Integer = Environment.TickCount
                Dim importedcount As Integer = 0
                pb.Visible = True
                pb.Value = 1
                pb.Step = 1
                'pb.Maximum = dtImport.Rows.Count
                pb.Maximum = grdView.RowCount
                Dim objBALAst_INV_Schedule As New BALAst_INV_Schedule
                Dim dtInvSch As DataTable = objBALAst_INV_Schedule.Getcombo_Schedule()

                Dim objAssets As New BALItems
                Dim dtAssets As DataTable = objAssets.GetAllData_Joined(New attItems)
                Dim ObjAssetsDetails As New BALAssetDetails
                Dim dtAssetsDetails As DataTable = ObjAssetsDetails.GetAsset_Details(New attAssetDetails)

                For i As Integer = 0 To grdView.RowCount - 1
                    'Import Selected and Success rows only.

                    If grdView.GetRowCellValue(i, ("Status")).ToString.Contains(SuccessMessage) And CBool(grdView.GetRowCellValue(i, ("Selection"))) = True Then
                        'Import Category.
                        Dim CatID As String = ImportCategory(grdView.GetRowCellValue(i, ("MainCategory")), grdView.GetRowCellValue(i, ("SubCategory")))
                        'Import location.
                        Dim loc As AssetLocation
                        loc.CityCode = grdView.GetRowCellValue(i, ("CityCode"))
                        loc.BuildingCode = grdView.GetRowCellValue(i, ("BuildingCode"))
                        loc.FloorCode = grdView.GetRowCellValue(i, ("FloorCode"))
                        loc.RoomCode = grdView.GetRowCellValue(i, ("RoomCode"))
                        loc.CityName = grdView.GetRowCellValue(i, ("CityName"))
                        loc.BuildingName = grdView.GetRowCellValue(i, ("BuildingName"))
                        loc.FloorName = grdView.GetRowCellValue(i, ("FloorName"))
                        loc.RoomName = grdView.GetRowCellValue(i, ("RoomName"))
                        Dim LocID As String = ImportLocation(loc)
                        'Import Assets
                        If ImportAsset(grdView.GetRowCellValue(i, ("AssetTag")), grdView.GetRowCellValue(i, ("AssetDescription")), grdView.GetRowCellValue(i, ("Serial")), grdView.GetRowCellValue(i, ("Model")), LocID, CatID, dtAssets, dtAssetsDetails, dtInvSch) Then
                            importedcount += 1
                        End If
                    End If
                    pb.PerformStep()
                    Application.DoEvents()

                Next
                Dim time As String = (Environment.TickCount - Start) / 1000 & " S"
                MessageBox.Show("Import Completed, imported Asset count = " & importedcount & ". Time(" & time & ")", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
                pb.Visible = False
            End If
        End If
    End Sub

    Private Function GetCompleteLocationCode(ByVal loc As AssetLocation) As String
        Dim CompleteCode As String = loc.CityCode
        If loc.BuildingCode <> "" Then
            CompleteCode &= " \ " & loc.BuildingCode
        End If
        If loc.FloorCode <> "" Then
            CompleteCode &= " \ " & loc.FloorCode
        End If
        If loc.RoomCode <> "" Then
            CompleteCode &= " \ " & loc.RoomCode
        End If
        Return CompleteCode
    End Function

    Private Function GetCompleteLocationDescription(ByVal loc As AssetLocation) As String
        Dim CompleteDesc As String = loc.CityName
        If loc.BuildingCode <> "" Then
            CompleteDesc &= " \ " & loc.BuildingName
        End If
        If loc.FloorCode <> "" Then
            CompleteDesc &= " \ " & loc.FloorName
        End If
        If loc.RoomCode <> "" Then
            CompleteDesc &= " \ " & loc.RoomName
        End If
        Return CompleteDesc
    End Function

    Private Function ImportLocation(ByVal loc As AssetLocation) As String
        Dim objattLocation As New attLocation
        Dim objBALLocation As New BALLocation
        Dim CityID As String = ""
        Dim BuildingID As String = ""
        Dim FloorID As String = ""
        Dim RoomID As String = ""

        Dim CityDesc As String = ""
        Dim BuildingDesc As String = ""
        Dim FloorDesc As String = ""
        Dim RoomDesc As String = ""

        Dim CompleteLocDesc As String = GetCompleteLocationDescription(loc)

        RoomID = objBALLocation.GetLocIDByCompleteDesc(CompleteLocDesc)
        RoomDesc = objBALLocation.GetLocDescByID(RoomID)

        If RoomID <> "0" Then
            Return RoomID
        Else
            CityID = objBALLocation.GetLocIDByCompleteDesc(loc.CityName)
            If CityID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = loc.CityName
                CityID = objBALLocation.GetRootLocID()
                objattLocation.HierCode = CityID
                objattLocation.Code = loc.CityCode
                objattLocation.locLevel = 0
                objattLocation.CompanyID = 1
                objBALLocation.Insert_Location(objattLocation)
            End If
            objattLocation = New attLocation
            BuildingID = objBALLocation.GetLocIDByCompleteDesc(loc.CityName & " \ " & loc.BuildingName)
            If BuildingID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = loc.BuildingName
                BuildingID = objBALLocation.GetChildLocID(CityID)
                objattLocation.HierCode = BuildingID
                objattLocation.Code = loc.BuildingCode
                objattLocation.locLevel = 1
                objattLocation.CompanyID = 1
                objBALLocation.Insert_Location(objattLocation)
            End If

            objattLocation = New attLocation
            FloorID = objBALLocation.GetLocIDByCompleteDesc(loc.CityName & " \ " & loc.BuildingName & " \ " & loc.FloorName)
            If FloorID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = loc.FloorName
                FloorID = objBALLocation.GetChildLocID(BuildingID)
                objattLocation.HierCode = FloorID
                objattLocation.Code = loc.FloorCode
                objattLocation.locLevel = 2
                objattLocation.CompanyID = 1
                objBALLocation.Insert_Location(objattLocation)
            End If
            objattLocation = New attLocation
            'RoomID we got it first to speed up the process.
            If RoomID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = loc.RoomName
                RoomID = objBALLocation.GetChildLocID(FloorID)
                objattLocation.HierCode = RoomID
                objattLocation.Code = loc.RoomCode
                objattLocation.locLevel = 3
                objattLocation.CompanyID = 1
                objBALLocation.Insert_Location(objattLocation)
            End If
            Return RoomID
        End If
    End Function

    'Private Function ImportLocationOld(ByVal loc As AssetLocation) As String
    '    Dim objattLocation As New attLocation
    '    Dim objBALLocation As New BALLocation
    '    Dim CityID As String = ""
    '    Dim BuildingID As String = ""
    '    Dim FloorID As String = ""
    '    Dim RoomID As String = ""

    '    Dim CityDesc As String = ""
    '    Dim BuildingDesc As String = ""
    '    Dim FloorDesc As String = ""
    '    Dim RoomDesc As String = ""

    '    Dim CompleteLocCode As String = GetCompleteLocationCode(loc)
    '    RoomID = objBALLocation.GetLocIDByCompleteCode(CompleteLocCode)
    '    RoomDesc = objBALLocation.GetLocDescByID(RoomID)

    '    If RoomID <> "0" And RoomDesc = loc.RoomName Then
    '        Return RoomID
    '    Else
    '        CityID = objBALLocation.GetLocID(loc.CityCode)
    '        If CityID = "0" Then
    '            objattLocation = New attLocation
    '            objattLocation.Description = loc.CityName
    '            CityID = objBALLocation.GetRootLocID()
    '            objattLocation.HierCode = CityID
    '            objattLocation.Code = loc.CityCode
    '            objattLocation.locLevel = 0
    '            objBALLocation.Insert_Location(objattLocation)
    '        Else
    '            CityDesc = objBALLocation.GetLocDescByID(CityID)
    '            If CityDesc <> loc.CityName Then
    '                'update the location name if the name is different
    '                objattLocation = New attLocation
    '                objattLocation.Description = loc.CityName
    '                objattLocation.HierCode = CityID
    '                objattLocation.Code = loc.CityCode
    '                objattLocation.locLevel = 0
    '                objBALLocation.Update_Location(objattLocation)
    '            End If
    '        End If
    '        objattLocation = New attLocation
    '        BuildingID = objBALLocation.GetLocIDByCompleteCode(loc.CityCode & " \ " & loc.BuildingCode)
    '        If BuildingID = "0" Then
    '            objattLocation = New attLocation
    '            objattLocation.Description = loc.BuildingName
    '            BuildingID = objBALLocation.GetChildLocID(CityID)
    '            objattLocation.HierCode = BuildingID
    '            objattLocation.Code = loc.BuildingCode
    '            objattLocation.locLevel = 1
    '            objBALLocation.Insert_Location(objattLocation)
    '        Else
    '            BuildingDesc = objBALLocation.GetLocDescByID(BuildingID)
    '            If BuildingDesc <> loc.BuildingName Then
    '                objattLocation = New attLocation
    '                objattLocation.Description = loc.BuildingName
    '                objattLocation.HierCode = BuildingID
    '                objattLocation.Code = loc.BuildingCode
    '                objattLocation.locLevel = 1
    '                objBALLocation.Update_Location(objattLocation)
    '            End If
    '        End If

    '        objattLocation = New attLocation
    '        FloorID = objBALLocation.GetLocIDByCompleteCode(loc.CityCode & " \ " & loc.BuildingCode & " \ " & loc.FloorCode)
    '        If FloorID = "0" Then
    '            objattLocation = New attLocation
    '            objattLocation.Description = loc.FloorName
    '            FloorID = objBALLocation.GetChildLocID(BuildingID)
    '            objattLocation.HierCode = FloorID
    '            objattLocation.Code = loc.FloorCode
    '            objattLocation.locLevel = 2
    '            objBALLocation.Insert_Location(objattLocation)
    '        Else
    '            FloorDesc = objBALLocation.GetLocDescByID(FloorID)
    '            If FloorDesc <> loc.FloorName Then
    '                objattLocation = New attLocation
    '                objattLocation.Description = loc.FloorName
    '                objattLocation.HierCode = FloorID
    '                objattLocation.Code = loc.FloorCode
    '                objattLocation.locLevel = 2
    '                objBALLocation.Update_Location(objattLocation)
    '            End If
    '        End If
    '        objattLocation = New attLocation
    '        'RoomID we got it first to speed up the process.
    '        If RoomID = "0" Then
    '            objattLocation = New attLocation
    '            objattLocation.Description = loc.RoomName
    '            RoomID = objBALLocation.GetChildLocID(FloorID)
    '            objattLocation.HierCode = RoomID
    '            objattLocation.Code = loc.RoomCode
    '            objattLocation.locLevel = 3
    '            objBALLocation.Insert_Location(objattLocation)
    '        Else
    '            RoomDesc = objBALLocation.GetLocDescByID(RoomID)
    '            If RoomDesc <> loc.RoomName Then
    '                objattLocation = New attLocation
    '                objattLocation.Description = loc.RoomName
    '                objattLocation.HierCode = RoomID
    '                objattLocation.Code = loc.RoomCode
    '                objattLocation.locLevel = 3
    '                objBALLocation.Update_Location(objattLocation)
    '            End If
    '        End If
    '        Return RoomID
    '    End If
    'End Function
    Private Function ImportAsset(ByVal AssetTag As String, ByVal AssetDesc As String, ByVal Serial As String, ByVal Modle As String, ByVal LocID As String, ByVal CatID As String, ByRef dtAssets As DataTable, ByVal dtAssetDetails As DataTable, ByVal dtInvSch As DataTable) As Boolean
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

            objattAssetDetails.BaseCost = 0
            objattAssetDetails.CustodianID = 1
            objattAssetDetails.InsID = 0
            objattAssetDetails.SuppID = ""
            objattAssetDetails.POCode = 0
            objattAssetDetails.SerailNo = Serial
            objattAssetDetails.Discount = 0
            objattAssetDetails.InvNumber = ""
            objattAssetDetails.ItemCode = AssetItemID
            objattAssetDetails.Tax = 0
            objattAssetDetails.TransRemarks = "Oracle System"
            objattAssetDetails.DispDate = Date.MinValue
            objattAssetDetails.CompanyID = 1
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
            objattAssetDetails.PKeyCode = AssetTag
            objattAssetDetails.BarCode = AssetTag

            objattAssetDetails.BussinessArea = ""
            objattAssetDetails.InventoryNumber = ""
            objattAssetDetails.CostCenterID = ""
            objattAssetDetails.InStockAsset = True
            objattAssetDetails.EvaluationGroup1 = ""
            objattAssetDetails.EvaluationGroup2 = ""
            objattAssetDetails.EvaluationGroup3 = ""
            objattAssetDetails.EvaluationGroup4 = ""

            Dim DetailsRows As DataRow() = dtAssetDetails.Select("AstID = '" & AssetTag & "'")
            If DetailsRows.Length > 0 Then
                objattAssetDetails.AstNum = DetailsRows(0)("AstNum")
                objattAssetDetails.RefNo = DetailsRows(0)("RefNo")
                Dim objattAsset As New attAssetDetails
                objattAsset.PKeyCode = AssetTag
                'Get the old SrvDate, we don't want to overwrite it when update.
                'Dim SrvDate As Date = Now.Date
                'Dim PurDate As Date = DetailsRows(0)("PurDate")
                objattAssetDetails.SrvDate = Now.Date
                objattAssetDetails.PurDate = DetailsRows(0)("PurDate")
                objattAssetDetails.LastEditBY = AppConfig.LoginName
                objattAssetDetails.LastEditDate = Now
                objBALAssetDetails.Update_AssetDetails(objattAssetDetails)
            Else
                objattAssetDetails.AstNum = GetAssetNumberFromAssetTag(AssetTag)
                'objattAssetDetails.AstNum = Generate_AssetNumber(objattAssetDetails.CompanyID)
                objattAssetDetails.RefNo = objattAssetDetails.AstNum
                objattAssetDetails.SrvDate = Now.Date
                objattAssetDetails.PurDate = Now.Date
                objattAssetDetails.InvSchCode = 1
                objattAssetDetails.InvStatus = 1

                objattAssetDetails.CreatedBY = AppConfig.LoginName
                objattAssetDetails.LastEditBY = AppConfig.LoginName
                objattAssetDetails.LastEditDate = Now
                objattAssetDetails.CreationDate = Now
                objattAssetDetails.StatusID = 1

                If objBALAssetDetails.Insert_AssetDetails(objattAssetDetails, False) Then
                    objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, 0, 1, 0, objattAssetDetails.SrvDate, False)
                    ' we again remove this according to CMA Requirements. They don't want to create history for the newly imported assets.
                    'Dim objattAstHistory As attAstHistory
                    'Dim objBALAst_History As New BALAst_History
                    'objattAstHistory = New attAstHistory()
                    'objattAstHistory.AstID = objattAssetDetails.PKeyCode
                    ''Insert the history for all the schedules 
                    'For Each dr As DataRow In dtInvSch.Rows
                    '    objattAstHistory.Status = 0
                    '    objattAstHistory.InvSchCode = dr("InvSchCode")
                    '    objattAstHistory.HisDate = DateTime.Now
                    '    objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                    '    objattAstHistory.Fr_loc = LocID
                    '    objattAstHistory.To_Loc = LocID
                    '    objattAstHistory.NoPiece = 0
                    '    objBALAst_History.Insert_Ast_History(objattAstHistory)
                    'Next
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

    Private Sub grdView_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grdView.CellValueChanged
        'If e.Column Is grdView.Columns("Selection") Then
        '    If grdView.GetRowCellValue(e.RowHandle, "Status") <> SuccessMessage Then
        '        grdView.SetRowCellValue(e.RowHandle, "Selection", False)
        '    Else
        '        grdView.SetRowCellValue(e.RowHandle, "Selection", True)
        '    End If
        'End If
    End Sub

    Private Sub frmImport_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        With OpenFileDialog1

            .CheckFileExists = True
            .CheckPathExists = True
            .FileName = ""
            .ShowReadOnly = True
            .DefaultExt = "csv"

            .Filter = "Comma-separated values (*.csv)|*.csv"

            .Multiselect = False
            .Title = "Comma-separated values File"

            If .ShowDialog() = DialogResult.OK Then
                fileName = .FileName
                CreateTempDataTable()
                'If process for the file success then format the grid.
                If CSVFileProcess(fileName) Then
                    grdFormat()
                    Me.BringToFront()
                Else
                    Me.Close()
                End If
            Else
                Me.Close()
            End If
            .Dispose()
        End With
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