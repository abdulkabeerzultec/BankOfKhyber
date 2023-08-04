Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Windows.Forms
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO

Public Class ctlImportData
    Dim MyConnection As System.Data.OleDb.OleDbConnection
    Dim strConnection As String = String.Empty
    Private BreakImport As Boolean = False
 

    Private dtAssetCreation As New DataTable
    Dim SuccessMessage As String = "Success."
    Dim ExistMessage As String = "AssetTag Already Exist. Ignored"
    Dim AssetTagNotExistMessage As String = "AssetTag not exist. Ignored"

    Private Sub ctlImportData_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ParentForm.Text = "Import data from Excel"
        Me.ParentForm.Size = New System.Drawing.Size(722, 532)
        Me.ParentForm.WindowState = FormWindowState.Maximized
        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
    End Sub

    Private Sub ctlImportData_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        'To Stop the loops of the import process.
        BreakImport = True
    End Sub

    Private Sub btnOpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFile.Click
        Dim fileChooser As New OpenFileDialog
        With fileChooser

            .CheckFileExists = True
            .CheckPathExists = True
            .FileName = ""
            .ShowReadOnly = True
            .DefaultExt = "xlsx"
            .Filter = "Excel files (*.xls;*.xlsx)|*.xls;*.xlsx"
            .Multiselect = False
            .Title = "Excel Files"

            If .ShowDialog() = DialogResult.OK Then
                txtFilePath.Text = fileChooser.FileName
                If IO.Path.GetExtension(txtFilePath.Text) = ".xls" Then
                    GetConnected("2003")
                ElseIf IO.Path.GetExtension(txtFilePath.Text) = ".xlsx" Then
                    GetConnected("2007")
                End If
            End If
        End With
    End Sub

    Private Sub GetConnected(ByVal ExcelVerion As String)
        Dim strSourceFile As String = txtFilePath.Text

        If ExcelVerion = "2003" Then
            strConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strSourceFile + ";Extended Properties=""Excel 8.0;IMEX=1;HDR=YES;"""
        Else 'if ExcelVerion = "2007"
            strConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strSourceFile & ";Extended Properties=""Excel 12.0;HDR=Yes"";"
        End If

        MyConnection = New System.Data.OleDb.OleDbConnection(strConnection)
        Try
            MyConnection.Open()
            Dim SchemaDT As DataTable = MyConnection.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, _
                              New Object() {Nothing, Nothing, Nothing, "TABLE"})
            lstTables.DataSource = SchemaDT
            lstTables.ValueMember = "TABLE_NAME"
            lstTables.DisplayMember = "TABLE_NAME"
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            MyConnection.Close()
        End Try
    End Sub

    Private Function CheckColumnsMasterData(ByVal dt As DataTable) As String

        Dim MissingColumns As String = ""
        If dt.Columns("Barcode") Is Nothing Then
            MissingColumns &= "Barcode"
        End If

        If dt.Columns("Brand") Is Nothing Then
            MissingColumns &= ",Brand"
        End If
        If dt.Columns("Model") Is Nothing Then
            MissingColumns &= ",Model"
        End If
        If dt.Columns("Serial Number") Is Nothing Then
            MissingColumns &= ",Serial Number"
        End If
        If dt.Columns("Custodian") Is Nothing Then
            MissingColumns &= ",Custodian"
        End If

        If dt.Columns("Custodian ID") Is Nothing Then
            MissingColumns &= ",Custodian ID"
        End If

        If dt.Columns("Department") Is Nothing Then
            MissingColumns &= ",Department"
        End If

        If dt.Columns("Building") Is Nothing Then
            MissingColumns &= ",Building"
        End If

        If dt.Columns("Floor") Is Nothing Then
            MissingColumns &= ",Floor"
        End If

        If dt.Columns("Room") Is Nothing Then
            MissingColumns &= ",Room"
        End If

        If dt.Columns("Item") Is Nothing Then
            MissingColumns &= ",Item"
        End If

        If dt.Columns("Asset full Description") Is Nothing Then
            MissingColumns &= ",Asset full Description"
        End If

        If dt.Columns("MAC Address") Is Nothing Then
            MissingColumns &= ",MAC Address"
        End If
        If dt.Columns("IP Address") Is Nothing Then
            MissingColumns &= ",IP Address"
        End If


        If dt.Columns("CPU") Is Nothing Then
            MissingColumns &= ",CPU"
        End If

        If dt.Columns("Memory") Is Nothing Then
            MissingColumns &= ",Memory"
        End If

        If dt.Columns("Hard Drive") Is Nothing Then
            MissingColumns &= ",Hard Drive"
        End If
        If dt.Columns("Computer Name") Is Nothing Then
            MissingColumns &= ",Computer Name"
        End If
        If dt.Columns("TelExt") Is Nothing Then
            MissingColumns &= ",TelExt"
        End If



        If dt.Columns("Supplier") Is Nothing Then
            MissingColumns &= ",Supplier"
        End If
        If dt.Columns("Date Purchased") Is Nothing Then
            MissingColumns &= ",Date Purchased"
        End If
        If dt.Columns("Warranty Expired") Is Nothing Then
            MissingColumns &= ",Warranty Expired"
        End If

        If dt.Columns("WExtension") Is Nothing Then
            MissingColumns &= ",WExtension"
        End If
        If dt.Columns("License Number") Is Nothing Then
            MissingColumns &= ",License Number"
        End If
        If dt.Columns("CompanyCode") Is Nothing Then
            MissingColumns &= ",CompanyCode"
        End If

        If dt.Columns("Job Title") Is Nothing Then
            MissingColumns &= ",Job Title"
        End If

        If MissingColumns.StartsWith(",") Then
            MissingColumns = MissingColumns.Substring(1, MissingColumns.Length - 1)
        End If

        Return MissingColumns
    End Function


    Private Function ImportAssetCreation() As String
        Dim importedcount As Integer = 0
        Dim Start As Integer = Environment.TickCount
        Dim objAssets As New BALItems
        Dim dtAssets As DataTable = objAssets.GetAllData_Joined(New attItems)
        Dim ObjAssetsDetails As New BALAssetDetails
        Dim dtAssetsDetails As DataTable = ObjAssetsDetails.GetAsset_Details(New attAssetDetails)

        pbImport.Visible = True
        pbImport.Position = 1
        pbImport.Properties.Step = 1
        pbImport.Properties.Maximum = grdView.RowCount
        pbImport.Refresh()
        pbImport.Visible = True

        layoutImportProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Dim objBALAst_INV_Schedule As New BALAst_INV_Schedule
        Dim dtInvSch As DataTable = objBALAst_INV_Schedule.Getcombo_Schedule()

        btnImport.Enabled = False
        Dim ErrorCount As Integer = 0

        Try

            For i As Integer = 0 To grdView.RowCount - 1
                'Import Category.
                Dim CatID As String = 1
                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("Item")).ToString.Trim) Then
                    CatID = ImportCategory(grdView.GetRowCellValue(i, ("Item")).ToString.Trim, "")
                End If
                'Import location.

                Dim LocID As String = 1
                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("Building")).ToString.Trim) Then
                    LocID = ImportLocation(grdView.GetRowCellValue(i, ("Building")).ToString.Trim, grdView.GetRowCellValue(i, ("Floor")).ToString.Trim, grdView.GetRowCellValue(i, ("Room")).ToString.Trim)
                End If
                'Import Employee


                Dim EmpID As String = 1
                'Dim TCNumber As String = grdView.GetRowCellValue(i, ("TC Number")).ToString.Trim()
                'If Not String.IsNullOrEmpty(TCNumber) And Not TCNumber.Contains("_") Then
                '    EmpID = TCNumber
                'Else
                '    If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("Custodian")).ToString.Trim.Replace("'", "")) Then
                '        EmpID = grdView.GetRowCellValue(i, ("Custodian")).ToString.Trim.Replace("'", "")
                '    Else
                '        EmpID = 1
                '    End If
                'End If

                EmpID = grdView.GetRowCellValue(i, ("Custodian ID")).ToString.Trim.Replace("'", "")

                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("Custodian")).ToString.Trim.Replace("'", "")) And Not String.IsNullOrEmpty(EmpID) Then
                    EmpID = ImportEmp(EmpID, grdView.GetRowCellValue(i, ("Custodian")).ToString.Trim.Replace("'", ""), grdView.GetRowCellValue(i, ("TelExt")).ToString.Trim, grdView.GetRowCellValue(i, ("Job Title")).ToString.Trim)
                End If

                'Import Company
                Dim CompanyID As String = ImportCompany(grdView.GetRowCellValue(i, ("CompanyCode")).ToString.Trim)
                'Import(CostCenter)
                Dim CostCenterID As String = 1
                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("Department")).ToString.Trim) Then
                    If grdView.GetRowCellValue(i, ("Department")).ToString.Trim.Length > 50 Then
                        CostCenterID = ImportCostCenter(grdView.GetRowCellValue(i, ("Department")).ToString.Trim.Substring(0, 50).Replace("'", ""))
                    Else
                        CostCenterID = ImportCostCenter(grdView.GetRowCellValue(i, ("Department")).ToString.Trim.Replace("'", ""))
                    End If
                End If

                Dim AstBrandID As String = 1
                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("Brand")).ToString.Trim) Then
                    AstBrandID = ImportBrand(grdView.GetRowCellValue(i, ("Brand")).ToString.Trim)
                End If

                Dim Supplier As String = 1
                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("Supplier")).ToString.Trim) Then
                    Supplier = ImportSupplier(grdView.GetRowCellValue(i, ("Supplier")).ToString.Trim)
                End If


                Dim focusrowhandel As Integer = i
                'Import(Assets)


                If ImportAsset(focusrowhandel, CatID, LocID, EmpID, CostCenterID, CompanyID, AstBrandID, Supplier, dtAssets, dtAssetsDetails, dtInvSch) Then
                    importedcount += 1
                End If
                pbImport.PerformStep()
                pbImport.Update()
                Application.DoEvents()
            Next

            Dim time As String = (Environment.TickCount - Start) / 1000 & " S"
            Dim msg As String = "Import Completed, imported Asset count = " & importedcount & ". Time(" & time & ")"

            SaveToLogFile(msg, False)
            pbImport.Visible = False

            Return msg
        Catch ex As Exception
            Return ex.Message
        Finally
            layoutImportProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            btnImport.Enabled = True
        End Try
    End Function



    Private Function ImportAsset(ByVal FocuseRowHandel As Integer, ByVal CatID As String, ByVal LocationID As String, ByVal CustodianID As String, ByVal CostCenterID As String, ByVal CompanyID As String, ByVal AstBrandID As String, ByVal SuppID As String, ByRef dtAssets As DataTable, ByVal dtAssetDetails As DataTable, ByVal dtInvSch As DataTable) As Boolean
        'Add Asset Item
        Try
            Dim ItemDesc As String = grdView.GetRowCellValue(FocuseRowHandel, ("Asset full Description")).ToString.Trim
            Dim AssetItemID As String = 1
            If Not String.IsNullOrEmpty(ItemDesc) Then
                ItemDesc = ItemDesc.ToString.Replace("'", "")
                Dim AssetRows As DataRow() = dtAssets.Select("AstDesc = '" & ItemDesc & "' and AstCatID = '" & CatID & "'")
                If AssetRows.Length > 0 Then
                    AssetItemID = AssetRows(0)(0)
                Else
                    Dim objBALAssets As New BALItems
                    Dim objattAssets As New attItems
                    objattAssets.AstCatID = CatID
                    objattAssets.AstDesc = ItemDesc
                    AssetItemID = objBALAssets.GetNextPKey_Item()
                    objattAssets.PKeyCode = AssetItemID
                    objBALAssets.Insert_Item(objattAssets)
                    Dim row As DataRow = dtAssets.NewRow()
                    row("AstCatID") = CatID
                    row("AstDesc") = ItemDesc
                    row("itemcode") = AssetItemID
                    dtAssets.Rows.Add(row)
                End If
            Else
                MessageBox.Show("Asset full Description is empty")
            End If


            Dim objattAssetDetails As New attAssetDetails
            Dim objBALAssetDetails As New BALAssetDetails
            Dim AssetID As String = objBALAssetDetails.Generate_AssetID()

            objattAssetDetails.BaseCost = 0
            objattAssetDetails.CustodianID = CustodianID
            objattAssetDetails.InsID = 0
            objattAssetDetails.POCode = 0
            objattAssetDetails.SerailNo = grdView.GetRowCellValue(FocuseRowHandel, ("Serial Number")).ToString.Trim
            objattAssetDetails.Discount = 0
            objattAssetDetails.ItemCode = AssetItemID
            objattAssetDetails.Tax = 0
            objattAssetDetails.TransRemarks = String.Empty
            objattAssetDetails.DispDate = Nothing
            objattAssetDetails.CompanyID = CompanyID
            objattAssetDetails.AstBrandID = AstBrandID
            objattAssetDetails.AstDesc = grdView.GetRowCellValue(FocuseRowHandel, ("Asset full Description")).ToString.Trim
            objattAssetDetails.AstDesc2 = ""
            objattAssetDetails.AstModel = grdView.GetRowCellValue(FocuseRowHandel, ("Model")).ToString.Trim
            objattAssetDetails.GLCode = 1

            objattAssetDetails.SuppID = SuppID
            objattAssetDetails.InvNumber = ""
            objattAssetDetails.Capex = ""
            objattAssetDetails.PoErp = ""
            objattAssetDetails.Plate = ""
            objattAssetDetails.GRN = ""
            objattAssetDetails.RefCode = ""
            objattAssetDetails.PONumber = ""

            objattAssetDetails.LocID = LocationID
            objattAssetDetails.NoPiece = 1
            objattAssetDetails.Disposed = "0"
            objattAssetDetails.IsSold = 0


            objattAssetDetails.CostCenterID = CostCenterID
            objattAssetDetails.InventoryNumber = String.Empty
            objattAssetDetails.BussinessArea = String.Empty

            objattAssetDetails.RefNo = grdView.GetRowCellValue(FocuseRowHandel, ("Barcode")).ToString.Trim

            objattAssetDetails.AstNum = grdView.GetRowCellValue(FocuseRowHandel, ("Barcode")).ToString.Trim
            objattAssetDetails.PKeyCode = AssetID
            objattAssetDetails.BarCode = objattAssetDetails.AstNum
            objattAssetDetails.IsDataChanged = False

            objattAssetDetails.CustomFld1 = grdView.GetRowCellValue(FocuseRowHandel, ("MAC Address")).ToString.Trim
            objattAssetDetails.CustomFld2 = grdView.GetRowCellValue(FocuseRowHandel, ("IP Address")).ToString.Trim
            objattAssetDetails.CustomFld3 = grdView.GetRowCellValue(FocuseRowHandel, ("License Number")).ToString.Trim
            objattAssetDetails.CustomFld4 = grdView.GetRowCellValue(FocuseRowHandel, ("WExtension")).ToString.Trim
            objattAssetDetails.CustomFld5 = grdView.GetRowCellValue(FocuseRowHandel, ("CPU")).ToString.Trim

            'objattAssetDetails.EvaluationGroup2 = grdView.GetRowCellValue(FocuseRowHandel, ("MHZ")).ToString.Trim
            objattAssetDetails.EvaluationGroup2 = grdView.GetRowCellValue(FocuseRowHandel, ("Memory")).ToString.Trim
            objattAssetDetails.EvaluationGroup3 = grdView.GetRowCellValue(FocuseRowHandel, ("Hard Drive")).ToString.Trim
            objattAssetDetails.EvaluationGroup4 = grdView.GetRowCellValue(FocuseRowHandel, ("Computer Name")).ToString.Trim
            Dim PDate As DateTime
            Try
                DateTime.TryParse(grdView.GetRowCellValue(FocuseRowHandel, ("Date Purchased")).ToString.Trim, PDate)
            Catch ex As Exception
                PDate = Now.Date
            End Try
            objattAssetDetails.PurDate = PDate
            objattAssetDetails.SrvDate = objattAssetDetails.PurDate

            If Not String.IsNullOrEmpty(grdView.GetRowCellValue(FocuseRowHandel, ("Warranty Expired")).ToString.Trim) Then
                Dim EDate As DateTime
                Try
                    DateTime.TryParse(grdView.GetRowCellValue(FocuseRowHandel, ("Warranty Expired")).ToString.Trim, EDate)
                Catch ex As Exception
                    EDate = Now.Date
                End Try
                Dim totalMonths As Integer = Math.Round((EDate - PDate).TotalDays / 30)
                objattAssetDetails.Warranty = totalMonths
            Else
                objattAssetDetails.Warranty = 0
                'objattAssetDetails.Warranty = Date. grdView.GetRowCellValue(FocuseRowHandel, ("Date Purchased")).ToString.Trim  - grdView.GetRowCellValue(FocuseRowHandel, ("Warranty Years")) * 12
            End If

            Dim UsefulLife As Integer = 0

            Dim DetailsRows As DataRow() = dtAssetDetails.Select("AstNum = '" & objattAssetDetails.AstNum & "'")
            If DetailsRows.Length > 0 Then
                objattAssetDetails.PKeyCode = DetailsRows(0)("AstID")
                'objattAssetDetails.PurDate = DetailsRows(0)("PurDate")
                'objattAssetDetails.SrvDate = DetailsRows(0)("SrvDate")
                If Not DetailsRows(0).IsNull("CapitalizationDate") Then
                    objattAssetDetails.CapitalizationDate = DetailsRows(0)("CapitalizationDate")
                End If
                objattAssetDetails.CreatedBY = DetailsRows(0)("Createdby").ToString
                objattAssetDetails.InStockAsset = DetailsRows(0)("InStockAsset")
                objattAssetDetails.IsDataChanged = False
                objattAssetDetails.LastEditBY = String.Empty
                objattAssetDetails.LastEditDate = Nothing

                If objBALAssetDetails.Update_AssetDetails(objattAssetDetails) Then
                    objBALAssetDetails.Update_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, 0, UsefulLife, 0, objattAssetDetails.SrvDate, False)
                End If
            Else
          
                objattAssetDetails.CapitalizationDate = Nothing

                objattAssetDetails.CreatedBY = "Import Process"
                objattAssetDetails.LastEditBY = String.Empty

                objattAssetDetails.LastEditDate = Nothing
                objattAssetDetails.CreationDate = Now

                objattAssetDetails.InStockAsset = False
                objattAssetDetails.InvSchCode = 1
                objattAssetDetails.InvStatus = 0
                objattAssetDetails.StatusID = 1

                If objBALAssetDetails.Insert_AssetDetails(objattAssetDetails, False) Then
                    objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, 0, UsefulLife, 0, objattAssetDetails.SrvDate, False)

                    Dim objattAstHistory As attAstHistory
                    Dim objBALAst_History As New BALAst_History
                    objattAstHistory = New attAstHistory()
                    objattAstHistory.AstID = AssetID
                    'Insert the history for all the schedules 
                    For Each dr As DataRow In dtInvSch.Rows
                        objattAstHistory.Status = 0
                        objattAstHistory.InvSchCode = dr("InvSchCode")
                        objattAstHistory.HisDate = DateTime.Now.Date
                        objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                        objattAstHistory.Fr_loc = LocationID
                        objattAstHistory.To_Loc = LocationID
                        objattAstHistory.NoPiece = 0
                        objBALAst_History.Insert_Ast_History(objattAstHistory)
                    Next

                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click

        Try
            Dim SheetName As String = lstTables.SelectedValue
            If Not String.IsNullOrEmpty(SheetName) Then
                If Messages.QuestionMessage("Are you sure to start importing master data?") = DialogResult.Yes Then
                    'Dim dt As DataTable = grd.DataSource
                    Dim msg As String = ImportAssetCreation()
                    Messages.InfoMessage(msg)
                    Me.ParentForm.Close()
                End If
            Else
                Messages.ErrorMessage("Please select sheet from the list first.")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            MyConnection.Close()
        End Try
    End Sub


    Public Function GetLeft(ByVal Value As String, ByVal Length As Integer) As String
        If Value.Length >= Length Then
            Return Value.Substring(0, Length)
        Else
            Return Value
        End If
    End Function

    Private Function ProcessAssetCreationFile() As Boolean
        Application.DoEvents()

        MyConnection = New System.Data.OleDb.OleDbConnection(strConnection)
        Dim Sheetdt As New DataTable

        Try
            Dim SheetName As String = lstTables.SelectedValue
            If Not String.IsNullOrEmpty(SheetName) Then
                MyConnection.Open()

                Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
                MyCommand = New System.Data.OleDb.OleDbDataAdapter(String.Format("Select * FROM [{0}]", SheetName), MyConnection)
                MyCommand.TableMappings.Add("Table", "TestTable")

                MyCommand.Fill(Sheetdt)
                MyConnection.Close()
            Else
                Messages.ErrorMessage("Please select sheet from the list first.")
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return False
        Finally
            MyConnection.Close()
        End Try
        Dim msg As String = CheckColumnsMasterData(Sheetdt)
        If String.IsNullOrEmpty(msg) Then
            grd.DataSource = Sheetdt
            Return True
        Else
            Messages.ErrorMessage(String.Format("Columns are not correct, Excel sheet must contains following columns({0})", msg))
            Return False
        End If
    End Function



    Private Function ImportLocation(ByVal Location1 As String, ByVal Location2 As String, ByVal Location3 As String) As String
        Dim objattLocation As New attLocation
        Dim objBALLocation As New BALLocation

        Dim RoomID As String = ""
        Dim RoomDesc As String = ""


        RoomID = objBALLocation.GetLocIDByCompleteDesc(Location1 & " \ " & Location2 & " \ " & Location3)
        RoomDesc = objBALLocation.GetLocDescByID(RoomID)

        Dim CityID As String
        Dim BuildingID As String

        If Not String.IsNullOrEmpty(RoomDesc) And RoomDesc <> "0" Then
            Return RoomID
        Else
            objattLocation = New attLocation
            CityID = objBALLocation.GetLocIDByCompleteDesc(Location1)
            If CityID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = Location1
                CityID = objBALLocation.GetRootLocID()
                objattLocation.HierCode = CityID
                objattLocation.Code = CityID
                objattLocation.locLevel = 0
                objBALLocation.Insert_Location(objattLocation)
            End If
        End If
        If String.IsNullOrEmpty(Location2) And String.IsNullOrEmpty(Location3) Then
            Return CityID
        Else
            objattLocation = New attLocation
            BuildingID = objBALLocation.GetLocIDByCompleteDesc(Location1 & " \ " & Location2)
            If BuildingID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = Location2
                BuildingID = objBALLocation.GetChildLocID(CityID)
                objattLocation.HierCode = BuildingID
                objattLocation.Code = BuildingID
                objattLocation.locLevel = 1
                objBALLocation.Insert_Location(objattLocation)
            End If
            If String.IsNullOrEmpty(Location3) Then
                Return BuildingID
            Else
                objattLocation = New attLocation
                If RoomID = "0" Then
                    objattLocation = New attLocation
                    objattLocation.Description = Location3
                    RoomID = objBALLocation.GetChildLocID(BuildingID)
                    objattLocation.HierCode = RoomID
                    objattLocation.Code = RoomID
                    objattLocation.locLevel = 2
                    objBALLocation.Insert_Location(objattLocation)
                End If
                Return RoomID
            End If

        End If


    End Function


    Public Function Generate_AssetNumber(ByVal ComPID As String) As String
        Dim objBALAssetDetails As New BALAssetDetails
        Dim AstNum As Long = objBALAssetDetails.GetNextPKey_AssetDetails()
        AstNum = AstNum + 1
        If AstNum < 100000 Then
            AstNum = 100000
        End If
        Return AstNum
    End Function

    Private Function ImportEmp(ByVal EmpID As String, ByVal EmpName As String, ByVal CustodianPhone As String, ByVal Designation As String) As String
        Dim objattCustodian As New attCustodian
        Dim objBALCustodian As New BALCustodian

        Dim DesignationID As Integer = 1
        If Not String.IsNullOrEmpty(Designation) Then
            DesignationID = ImportDesignation(Designation)
        End If
        objattCustodian.PKeyCode = EmpID
        Dim dt As DataTable = objBALCustodian.CheckID(objattCustodian)
        Dim CustodianID As String = ""
        If dt.Rows.Count > 0 Then
            CustodianID = dt.Rows(0)("CustodianID")
            objattCustodian.PKeyCode = CustodianID
            objattCustodian.CustodianName = EmpName
            objattCustodian.DepartmentID = 1
            objattCustodian.DesignationID = DesignationID
            objattCustodian.CustodianPhone = CustodianPhone
            objattCustodian.CustodianCode = ""
            objBALCustodian.Update_Custodian(objattCustodian)
        Else
            objattCustodian.PKeyCode = EmpID
            objattCustodian.CustodianName = EmpName
            objattCustodian.DepartmentID = 1
            objattCustodian.DesignationID = DesignationID
            objattCustodian.CustodianPhone = CustodianPhone
            objattCustodian.CustodianCode = ""
            objBALCustodian.Insert_Custodian(objattCustodian)
            CustodianID = objattCustodian.PKeyCode
        End If
        Return CustodianID
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


    Private Function ImportBrand(ByVal Brand As String) As String
        Dim objattBrand As New attBrand
        Dim objBALBrand As New BALbrand
        objattBrand.AstBrandName = Brand
        Dim BrandID As String = ""
        Dim dt As DataTable = objBALBrand.GetAll_Brand(objattBrand)
        If dt.Rows.Count > 0 Then
            BrandID = dt.Rows(0)("AstBrandID")
        Else

            objattBrand.PKeyCode = objBALBrand.GetNextPKey_Brand()
            objattBrand.AstBrandName = Brand
            objBALBrand.Insert_Brand(objattBrand)
            BrandID = objattBrand.PKeyCode
        End If
        Return BrandID
    End Function

    Private Function ImportSupplier(ByVal Supplier As String) As String
        Dim objattSupplier As New attSupplier
        Dim objBALSupplier As New BALSupplier
        objattSupplier.SuppName = Supplier
        Dim SupplierID As String = ""
        Dim dt As DataTable = objBALSupplier.GetAll_Supplier(objattSupplier)
        If dt.Rows.Count > 0 Then
            SupplierID = dt.Rows(0)("SuppID")
        Else
            objattSupplier.PKeyCode = objBALSupplier.GetNextPKey_Supplier()
            objattSupplier.SuppName = Supplier
            objBALSupplier.Insert_Supplier(objattSupplier)
            SupplierID = objattSupplier.PKeyCode
        End If
        Return SupplierID
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
            objattCostCenter.CompanyID = 0
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

            Dim objattBookTempl As New attBook
            Dim objBALBookTempl As New BALBooks
            objattBookTempl.PKeyCode = objBALBookTempl.GetNextPKey_Book()
            objattBookTempl.DepCode = 1
            objattBookTempl.Description = CompanyCode & " Book"
            objattBookTempl.CompanyID = CompanyID
            objBALBookTempl.Insert_Book(objattBookTempl)


            Dim objGLCode As New attGLCode
            Dim objBALGLCode As New BALGLCode
            objGLCode.GLDesc = "GL " & CompanyCode
            objGLCode.PKeyCode = objBALGLCode.GetNextPKey_GL()
            objGLCode.CompanyId = objattCompany.PKeyCode
            objBALGLCode.Insert_GLCode(objGLCode)

        End If
        Return CompanyID
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

        If String.IsNullOrEmpty(SubCat) Then
            Return MainCatID
        Else
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

    Public Function CheckConvertDate(ByVal Str) As Boolean
        Try
            Date.ParseExact(Str, "dd.MM.yyyy", System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ConvertDate(ByVal Str) As DateTime
        Try
            Return Date.ParseExact(Str, "dd.MM.yyyy", System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub SaveToLogFile(ByVal Message As String, ByVal IsErrorMessage As Boolean)
        If Not Directory.Exists(AppConfig.AppDataFolder & "\ErrorsReports\") Then
            Directory.CreateDirectory(AppConfig.AppDataFolder & "\ErrorsReports\")
        End If

        Dim ErrFile As String = AppConfig.AppDataFolder & "\ErrorsReports\" & "ZulAssetsImport-ExportErr.Log"
        Dim sr As StreamWriter = File.AppendText(ErrFile)
        Dim ErrTxt As String
        If IsErrorMessage Then
            ErrTxt = "Date  : " & Format(Now, "MMM dd, yyyy HH:mm") & Environment.NewLine & _
                     "Error : " & Message & Environment.NewLine & _
                     "-----------------------------------------------------------" & _
                     "-----------------------------------------------------------" & _
                     Environment.NewLine & Environment.NewLine
        Else
            ErrTxt = "Date  : " & Format(Now, "MMM dd, yyyy HH:mm") & Environment.NewLine & _
         "Information : " & Message & Environment.NewLine & _
         "-----------------------------------------------------------" & _
         "-----------------------------------------------------------" & _
         Environment.NewLine & Environment.NewLine
        End If

        sr.WriteLine(ErrTxt)
        sr.Close()
        sr.Dispose()
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        If ProcessAssetCreationFile() Then
            btnImport.Enabled = True
        Else
            btnImport.Enabled = False
            grd.DataSource = Nothing
        End If
    End Sub
End Class
