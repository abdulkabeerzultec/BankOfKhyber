Imports DevExpress.XtraGrid
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Windows.Forms

Public Class frmImportExcel
    Dim ObjIntegration As New BALABBIntegration
    Dim MyConnection As System.Data.OleDb.OleDbConnection
    Private BreakImport As Boolean = False
    Private _strConnection As String

    Private _ImportFileName As String
    Private _SheetName As String

    Dim SuccessMessage As String = "Success."
    Dim ExistMessage As String = "AssetTag Already Exist. Ignored"
    Dim AssetTagNotExistMessage As String = "AssetTag not exist. Ignored"
    Structure AssetLocation
        Dim CityName As String
        Dim BuildingName As String
        Dim FloorName As String
        Dim RoomName As String
        Dim LocationID As String
    End Structure


    Private Property StrConnection() As String
        Get
            Return _strConnection
        End Get
        Set(ByVal value As String)
            _strConnection = value
        End Set
    End Property

    Public Property SheetName() As String
        Get
            Return _SheetName
        End Get
        Set(ByVal value As String)
            _SheetName = value
        End Set
    End Property

    Public Property ImportFileName() As String
        Get
            Return _ImportFileName
        End Get
        Set(ByVal value As String)
            Dim strSourceFile As String = value
            If IO.Path.GetExtension(value) = ".xls" Then
                StrConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strSourceFile + ";Extended Properties=""Excel 8.0;IMEX=1;HDR=YES;"""
            ElseIf IO.Path.GetExtension(value) = ".xlsx" Then
                StrConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strSourceFile & ";Extended Properties=""Excel 12.0;HDR=Yes"";"
            End If
            _ImportFileName = value
        End Set
    End Property

    'Public Function SilentImport(ByVal lbl As DevExpress.XtraEditors.LabelControl, ByVal pb As DevExpress.XtraEditors.ProgressBarControl, ByVal FileName As String, ByVal ImportType As frmImport.TFileImportType) As String
    '    lbl.Text = "Processing Data Import file..."
    '    lbl.Update()
    '    If CheckFile(FileName) Then
    '        CreateAssetChangeDataTable()
    '        ProcessAssetChangeFile(FileName, SheetName, True, pb)
    '        Application.DoEvents()
    '        lbl.Text = "Importing Asset Master Data file..."
    '        lbl.Update()
    '        Return ImportAssetChange(True, pb)
    '    Else
    '        Dim msg As String = String.Format("File not found, Incorrect path({0})", FileName)
    '        SaveToLogFile(msg, True)
    '        lbl.Text = msg
    '        lbl.Update()
    '        Return msg
    '    End If

    '    'Select Case ImportType
    '    '    Case TFileImportType.AssetCreate
    '    '        lbl.Text = "Processing Data Asset Master Creation file..."
    '    '        lbl.Update()
    '    '        If CheckFile(FileName) Then
    '    '            CreateAssetCreationDataTable()
    '    '            ProcessAssetCreationFile(FileName, True, pb)
    '    '            Application.DoEvents()
    '    '            lbl.Text = "Importing Asset Master Creation file..."
    '    '            lbl.Update()
    '    '            Return ImportAssetCreation(True, pb)
    '    '        Else
    '    '            Dim msg As String = String.Format("File not found, Incorrect path({0})", FileName)
    '    '            SaveToLogFile(msg, True)
    '    '            lbl.Text = msg
    '    '            lbl.Update()
    '    '            Return msg
    '    '        End If
    '    '    Case TFileImportType.AssetChange

    '    '    Case Else
    '    '        Return String.Empty
    '    'End Select
    'End Function


    Private Function CheckColumnsMasterData(ByVal dt As DataTable) As Boolean
        Dim Result As Boolean = True
        If dt.Columns("Asset Number") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Description") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Units(System)") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Units(Physical)") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Tag Number") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Serial Number") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Book") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Category Major") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Category Minor") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Country") Is Nothing Then
            Result = False
        End If
        If dt.Columns("City") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Building") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Floor") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Department Desc#") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Location ID") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Model") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Employee No") Is Nothing Then
            Result = False
        End If

        If dt.Columns("Employee Name") Is Nothing Then
            Result = False
        End If
        Return Result
    End Function
    Private Sub CreateAssetDataTable(ByVal Sheetdt As DataTable)
        Sheetdt.Columns.Add("Asset Number", Type.GetType("System.String"))
        Sheetdt.Columns.Add("Description", Type.GetType("System.String"))
        Sheetdt.Columns.Add("Units(System)", Type.GetType("System.String"))
        Sheetdt.Columns.Add("Units(Physical)", Type.GetType("System.String"))
        Sheetdt.Columns.Add("Tag Number", Type.GetType("System.String"))
        Sheetdt.Columns.Add("Serial Number", Type.GetType("System.String"))
        Sheetdt.Columns.Add("Book", Type.GetType("System.String"))
        Sheetdt.Columns.Add("Category Major", Type.GetType("System.String"))
        Sheetdt.Columns.Add("Category Minor", Type.GetType("System.String"))

        Sheetdt.Columns.Add("Country", Type.GetType("System.String"))
        Sheetdt.Columns.Add("City", Type.GetType("System.String"))
        Sheetdt.Columns.Add("Building", Type.GetType("System.String"))
        Sheetdt.Columns.Add("Floor", Type.GetType("System.String"))

        Sheetdt.Columns.Add("Department Desc#", Type.GetType("System.String"))
        Sheetdt.Columns.Add("Location ID", Type.GetType("System.String"))
        Sheetdt.Columns.Add("Model", Type.GetType("System.String"))
        Sheetdt.Columns.Add("Employee No", Type.GetType("System.String"))
        Sheetdt.Columns.Add("Employee Name", Type.GetType("System.String"))
    End Sub

    Private Function ProcessAssetCreationFile() As Boolean
        Application.DoEvents()

        MyConnection = New System.Data.OleDb.OleDbConnection(strConnection)
        Dim Sheetdt As New DataTable
        'CreateAssetDataTable(Sheetdt)
        Try
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

        If CheckColumnsMasterData(Sheetdt) Then
            'For Each col As DataColumn In Sheetdt.Columns
            '    col.DataType = System.Type.GetType("System.String")
            'Next
            grd.DataSource = Sheetdt
            grdView.BestFitColumns()
            Return True
        Else
            Messages.ErrorMessage("Columns are not correct, Excel sheet must contains Import columns")
            Return False
        End If
    End Function


    Private Sub frmImport_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ProcessAssetCreationFile() Then
            btnImport.Enabled = True
        Else
            btnImport.Enabled = False
            grd.DataSource = Nothing
        End If
    End Sub

    Private Sub frmImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.WindowState = FormWindowState.Maximized
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

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

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

        pbImport.Visible = True

        btnImport.Enabled = False
        Dim ErrorCount As Integer = 0

        Try

            For i As Integer = 0 To grdView.RowCount - 1
                'Import Category.
                Dim CatID As String = 1
                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("Category Major")).ToString.Trim) Then
                    CatID = ImportCategory(grdView.GetRowCellValue(i, ("Category Major")), grdView.GetRowCellValue(i, ("Category Minor")))
                End If
                'Import location.
                Dim loc As AssetLocation

                loc.CityName = grdView.GetRowCellValue(i, ("Country"))
                loc.BuildingName = grdView.GetRowCellValue(i, ("City"))
                loc.FloorName = grdView.GetRowCellValue(i, ("Building"))
                loc.RoomName = grdView.GetRowCellValue(i, ("Floor"))
                loc.LocationID = grdView.GetRowCellValue(i, ("Location ID"))
                Dim LocID As String = ImportLocation(loc)

                'Import GLCode
                Dim GLCode As String = 1
                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("Book")).ToString.Trim.Replace("'", "")) Then
                    GLCode = ImportGLCode(grdView.GetRowCellValue(i, ("Book")).ToString.Trim.Replace("'", ""), 1)
                End If

                'Import Employee
                Dim EmpID As String = 1
                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("Employee Name")).ToString.Trim.Replace("'", "")) Then
                    EmpID = ImportEmp(grdView.GetRowCellValue(i, ("Employee Name")).ToString.Trim.Replace("'", ""), grdView.GetRowCellValue(i, ("Employee No")).ToString.Trim)
                End If

                'Import Company
                Dim CompanyID As String = 1
                'Import(CostCenter)
                Dim CostCenterID As String = 1
                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("Department Desc#")).ToString.Trim) Then
                    If grdView.GetRowCellValue(i, ("Department Desc#")).ToString.Trim.Length > 50 Then
                        CostCenterID = ImportCostCenter(grdView.GetRowCellValue(i, ("Department Desc#")).ToString.Trim.Substring(0, 50).Replace("'", ""))
                    Else
                        CostCenterID = ImportCostCenter(grdView.GetRowCellValue(i, ("Department Desc#")).ToString.Trim.Replace("'", ""))
                    End If
                End If

                Dim focusrowhandel As Integer = i

                'Import(Assets)
                If ImportAsset(focusrowhandel, CatID, LocID, EmpID, CostCenterID, CompanyID, GLCode, dtAssets, dtAssetsDetails, True) Then
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
            pbImport.Visible = False
            btnImport.Enabled = True
        End Try
    End Function

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Try
            If Not String.IsNullOrEmpty(SheetName) Then
                If Messages.QuestionMessage("Are you sure to start importing master data?") = DialogResult.Yes Then
                    'Dim dt As DataTable = grd.DataSource
                    Dim msg As String = ImportAssetCreation()
                    Messages.InfoMessage(msg)
                    Me.Close()
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


    Private Function GetCompleteLocationDescription(ByVal loc As AssetLocation) As String
        Dim CompleteDesc As String = loc.CityName
        If loc.BuildingName <> "" Then
            CompleteDesc &= " \ " & loc.BuildingName
        End If
        If loc.FloorName <> "" Then
            CompleteDesc &= " \ " & loc.FloorName
        End If
        If loc.RoomName <> "" Then
            CompleteDesc &= " \ " & loc.RoomName
        End If
        Return CompleteDesc
    End Function


    Private Function ImportAsset(ByVal FocuseRowHandel As Integer, ByVal CatID As String, ByVal LocationID As String, ByVal CustodianID As String, ByVal CostCenterID As String, ByVal CompanyID As String, ByVal GLCode As String, ByRef dtAssets As DataTable, ByVal dtAssetDetails As DataTable, ByVal IsNewAsset As Boolean) As Boolean
        'Add Asset Item
        Try
            Dim ItemDesc As String = grdView.GetRowCellValue(FocuseRowHandel, ("Description")).ToString.Trim
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
            objattAssetDetails.AstBrandID = 1
            objattAssetDetails.AstDesc = grdView.GetRowCellValue(FocuseRowHandel, ("Description")).ToString.Trim
            objattAssetDetails.AstModel = grdView.GetRowCellValue(FocuseRowHandel, ("Model")).ToString.Trim
            objattAssetDetails.GLCode = GLCode

            objattAssetDetails.SuppID = ""
            objattAssetDetails.InvNumber = ""
            objattAssetDetails.Capex = ""
            objattAssetDetails.PoErp = ""
            objattAssetDetails.Plate = ""
            objattAssetDetails.GRN = ""
            objattAssetDetails.RefCode = ""
            objattAssetDetails.PONumber = ""

            objattAssetDetails.LocID = LocationID
            objattAssetDetails.NoPiece = grdView.GetRowCellValue(FocuseRowHandel, ("Units(System)")).ToString.Trim
            objattAssetDetails.Disposed = "0"
            objattAssetDetails.IsSold = 0


            objattAssetDetails.CostCenterID = CostCenterID
            objattAssetDetails.InventoryNumber = String.Empty
            objattAssetDetails.BussinessArea = String.Empty
            objattAssetDetails.RefNo = grdView.GetRowCellValue(FocuseRowHandel, ("Asset Number")).ToString.Trim
            objattAssetDetails.AstNum = grdView.GetRowCellValue(FocuseRowHandel, ("Asset Number")).ToString.Trim
            objattAssetDetails.PKeyCode = AssetID
            objattAssetDetails.BarCode = grdView.GetRowCellValue(FocuseRowHandel, ("Tag Number")).ToString.Trim
            objattAssetDetails.IsDataChanged = False
            objattAssetDetails.PurDate = Now.Date
            objattAssetDetails.SrvDate = Now.Date

            Dim UsefulLife As Integer = 0

            Dim DetailsRows As DataRow() = dtAssetDetails.Select("AstNum = '" & objattAssetDetails.AstNum & "'")
            If DetailsRows.Length > 0 Then
                objattAssetDetails.PKeyCode = DetailsRows(0)("AstID")
                objattAssetDetails.LastEditBY = "Import Process"
                objattAssetDetails.LastEditDate = Now.Date
                If objBALAssetDetails.Update_AssetDetails(objattAssetDetails) Then
                    objBALAssetDetails.Update_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, 0, UsefulLife, 0, objattAssetDetails.SrvDate)
                End If
            Else

                objattAssetDetails.CreatedBY = "Import Process"
                objattAssetDetails.LastEditBY = "Import Process"
                objattAssetDetails.LastEditDate = Now.Date
                objattAssetDetails.CreationDate = Now.Date
                objattAssetDetails.InStockAsset = False
                objattAssetDetails.InvSchCode = 1
                objattAssetDetails.InvStatus = 0

                If objBALAssetDetails.Insert_AssetDetails(objattAssetDetails, False) Then
                    objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, 0, UsefulLife, 0, objattAssetDetails.SrvDate)
                    Dim objattAstHistory As attAstHistory
                    Dim objBALAst_History As New BALAst_History


                    objattAstHistory = New attAstHistory()
                    objattAstHistory.AstID = objattAssetDetails.PKeyCode

                    Dim dt As DataTable = objBALAst_History.GetAll_Ast_History(objattAstHistory)
                    If dt.Rows.Count = 0 Then
                        objattAstHistory.Status = 0
                        objattAstHistory.InvSchCode = 1
                        objattAstHistory.HisDate = DateTime.Now
                        objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                        objattAstHistory.Fr_loc = LocationID
                        objattAstHistory.To_Loc = LocationID
                        objattAstHistory.NoPiece = 0
                        objBALAst_History.Insert_Ast_History(objattAstHistory)
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
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
                objattLocation.Code = CityID
                objattLocation.locLevel = 0
                objBALLocation.Insert_Location(objattLocation)
            End If
            objattLocation = New attLocation
            BuildingID = objBALLocation.GetLocIDByCompleteDesc(loc.CityName & " \ " & loc.BuildingName)
            If BuildingID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = loc.BuildingName
                BuildingID = objBALLocation.GetChildLocID(CityID)
                objattLocation.HierCode = BuildingID
                objattLocation.Code = BuildingID
                objattLocation.locLevel = 1
                objBALLocation.Insert_Location(objattLocation)
            End If

            objattLocation = New attLocation
            FloorID = objBALLocation.GetLocIDByCompleteDesc(loc.CityName & " \ " & loc.BuildingName & " \ " & loc.FloorName)
            If FloorID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = loc.FloorName
                FloorID = objBALLocation.GetChildLocID(BuildingID)
                objattLocation.HierCode = FloorID
                objattLocation.Code = FloorID
                objattLocation.locLevel = 2
                objBALLocation.Insert_Location(objattLocation)
            End If
            objattLocation = New attLocation
            'RoomID we got it first to speed up the process.
            If RoomID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = loc.RoomName
                RoomID = objBALLocation.GetChildLocID(FloorID)
                objattLocation.HierCode = RoomID
                objattLocation.Code = loc.LocationID
                objattLocation.locLevel = 3
                objBALLocation.Insert_Location(objattLocation)
            End If
            Return RoomID
        End If
    End Function


    Private Function ImportEmp(ByVal EmpName As String, ByVal EmpID As String) As String
        Dim objattCustodian As New attCustodian
        Dim objBALCustodian As New BALCustodian
        objattCustodian.CustodianName = EmpName
        Dim dt As DataTable = objBALCustodian.CheckID(objattCustodian)
        Dim CustodianID As String = ""
        If dt.Rows.Count > 0 Then
            CustodianID = dt.Rows(0)("CustodianID")
        Else
            objattCustodian.PKeyCode = EmpID
            objattCustodian.CustodianName = EmpName
            objattCustodian.DepartmentID = 1
            objattCustodian.DesignationID = 1
            objBALCustodian.Insert_Custodian(objattCustodian)
            CustodianID = objattCustodian.PKeyCode
        End If
        Return CustodianID
    End Function

    Private Function ImportBrand(ByVal Brand As String) As String
        Dim objattCostCenter As New attBrand
        Dim objBALCostCenter As New BALbrand
        objattCostCenter.AstBrandName = Brand
        Dim BrandID As String = ""
        Dim dt As DataTable = objBALCostCenter.GetAll_Brand(objattCostCenter)
        If dt.Rows.Count > 0 Then
            BrandID = dt.Rows(0)("AstBrandID")
        Else

            objattCostCenter.PKeyCode = objBALCostCenter.GetNextPKey_Brand()
            objattCostCenter.AstBrandName = Brand
            objBALCostCenter.Insert_Brand(objattCostCenter)
            BrandID = objattCostCenter.PKeyCode
        End If
        Return BrandID
    End Function

    Private Function ImportGLCode(ByVal GLDesc As String, ByVal CompanyID As String) As String
        Dim objattGL As New attGLCode
        Dim objBALGLCode As New BALGLCode
        objattGL.GLDesc = GLDesc
        Dim GLID As String = ""
        Dim dt As DataTable = objBALGLCode.GetAll_GLCodes(objattGL)
        If dt.Rows.Count > 0 Then
            GLID = dt.Rows(0)("GLCode")
        Else

            objattGL.PKeyCode = objBALGLCode.GetNextPKey_GL()
            objattGL.GLDesc = GLDesc
            objattGL.CompanyId = CompanyID
            objBALGLCode.Insert_GLCode(objattGL)
            GLID = objattGL.PKeyCode
        End If
        Return GLID
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

        objBALDepPolicy.Insert_DepPolicy(objattDepPolicy)
    End Sub

End Class