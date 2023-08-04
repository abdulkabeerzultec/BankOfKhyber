Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Windows.Forms
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO

Public Class frmImportData
    Dim MyConnection As System.Data.OleDb.OleDbConnection
    Dim strConnection As String = String.Empty
    Private BreakImport As Boolean = False

    Structure AssetLocation
        Dim City As String
        Dim Building As String
        Dim Floor As String
        Dim Room As String
        Dim SubRoom As String

        Dim CityID As String
        Dim BuildingID As String
        Dim FloorID As String
        Dim RoomID As String
        Dim SubRoomID As String

        Dim CompanyCode As String
    End Structure

    Private dtAssetCreation As New DataTable
    Dim SuccessMessage As String = "Success."
    Dim ExistMessage As String = "AssetTag Already Exist. Ignored"
    Dim AssetTagNotExistMessage As String = "AssetTag not exist. Ignored"

    Private Sub ctlImportData_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Import data from Excel"
        Me.Size = New System.Drawing.Size(722, 532)
        Me.WindowState = FormWindowState.Maximized

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

    Private Function CheckColumnsMasterData(ByVal dt As DataTable) As Boolean
        Dim Result As Boolean = True
        If dt.Columns("Ref #") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Asset Category") Is Nothing Then
            Result = False
        End If

        If dt.Columns("Asset Type") Is Nothing Then
            Result = False
        End If

        If dt.Columns("Brand") Is Nothing Then
            Result = False
        End If

        If dt.Columns("Model") Is Nothing Then
            Result = False
        End If


        If dt.Columns("Description") Is Nothing Then
            Result = False
        End If

        If dt.Columns("Asset Description") Is Nothing Then
            Result = False
        End If

        If dt.Columns("S/N") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Service Date") Is Nothing Then
            Result = False
        End If

        If dt.Columns("Invoice Date") Is Nothing Then
            Result = False
        End If

        If dt.Columns("Location 1 Name") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Location 2 Name") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Location 3 Name") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Location 4 Name") Is Nothing Then
            Result = False
        End If

        If dt.Columns("Location 5 Name") Is Nothing Then
            Result = False
        End If

        If dt.Columns("Location 1 ID") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Location 2 ID") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Location 3 ID") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Location 4 ID") Is Nothing Then
            Result = False
        End If

        If dt.Columns("Location 5 ID") Is Nothing Then
            Result = False
        End If

        If dt.Columns("Name") Is Nothing Then
            Result = False
        End If
        If dt.Columns("Emp # / Login ID") Is Nothing Then
            Result = False
        End If

        'If dt.Columns("Login ID") Is Nothing Then
        '    Result = False
        'End If

        If dt.Columns("Email Address") Is Nothing Then
            Result = False
        End If

        If dt.Columns("Vendor Name") Is Nothing Then
            Result = False
        End If
        Return Result
    End Function


    Private Function ImportAssetCreation() As String
        Dim importedcount As Integer = 0
        Dim Errorimportedcount As Integer = 0
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
            'Import Company
            Dim CompanyID As String = ImportCompany("Sanofi")
            For i As Integer = 0 To grdView.RowCount - 1

                Dim CatID As String = 1
                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("Asset Category")).ToString.Trim) Then
                    CatID = ImportCategory(grdView.GetRowCellValue(i, ("Asset Category")).ToString.Trim, grdView.GetRowCellValue(i, ("Asset Type")).ToString.Trim)
                End If
                'Import location.

                Dim LocID As String = 1
                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("Location 1 Name")).ToString.Trim) Then
                    Dim loc As New AssetLocation
                    loc.City = grdView.GetRowCellValue(i, ("Location 1 Name")).ToString.Trim
                    loc.Building = grdView.GetRowCellValue(i, ("Location 2 Name")).ToString.Trim
                    loc.Floor = grdView.GetRowCellValue(i, ("Location 3 Name")).ToString.Trim
                    loc.Room = grdView.GetRowCellValue(i, ("Location 4 Name")).ToString.Trim
                    loc.SubRoom = grdView.GetRowCellValue(i, ("Location 5 Name")).ToString.Trim

                    loc.CityID = grdView.GetRowCellValue(i, ("Location 1 Id")).ToString.Trim
                    loc.BuildingID = grdView.GetRowCellValue(i, ("Location 2 ID")).ToString.Trim
                    loc.FloorID = grdView.GetRowCellValue(i, ("Location 3 ID")).ToString.Trim
                    loc.RoomID = grdView.GetRowCellValue(i, ("Location 4 ID")).ToString.Trim
                    loc.SubRoomID = grdView.GetRowCellValue(i, ("Location 5 ID")).ToString.Trim

                    loc.CompanyCode = CompanyID

                    LocID = ImportLocationFromAssets(loc, False)
                End If

                'Import Employee
                Dim EmpID As String = ""
                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("Name")).ToString.Trim.Replace("'", "")) Then
                    EmpID = ImportEmp(grdView.GetRowCellValue(i, ("Custodian ID")).ToString.Trim.Replace("'", ""), grdView.GetRowCellValue(i, ("Emp # / Login ID")).ToString.Trim.Replace("'", ""), grdView.GetRowCellValue(i, ("Name")).ToString.Trim.Replace("'", ""), grdView.GetRowCellValue(i, ("Email Address")).ToString.Trim)
                End If


                'Import(CostCenter)
                Dim CostCenterID As String = 1
                'If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("Department")).ToString.Trim) Then
                '    If grdView.GetRowCellValue(i, ("Department")).ToString.Trim.Length > 50 Then
                '        CostCenterID = ImportCostCenter(grdView.GetRowCellValue(i, ("Department")).ToString.Trim.Substring(0, 50).Replace("'", ""))
                '    Else
                '        CostCenterID = ImportCostCenter(grdView.GetRowCellValue(i, ("Department")).ToString.Trim.Replace("'", ""))
                '    End If
                'End If

                Dim AstBrandID As String = 1
                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("Brand")).ToString.Trim) Then
                    AstBrandID = ImportBrand(grdView.GetRowCellValue(i, ("Brand")).ToString.Trim)
                End If

                Dim focusrowhandel As Integer = i
                'Import(Assets)
                Dim SupplierID As String = 1
                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("Vendor Name")).ToString.Trim) Then
                    SupplierID = ImportSupplier(grdView.GetRowCellValue(i, ("Vendor Name")).ToString.Trim)
                End If

                If ImportAsset(focusrowhandel, CatID, LocID, EmpID, CostCenterID, CompanyID, AstBrandID, dtAssets, dtAssetsDetails, dtInvSch, SupplierID) Then
                    importedcount += 1
                Else
                    Errorimportedcount += 1
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


    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click

        Try
            Dim SheetName As String = lstTables.SelectedValue
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

        If CheckColumnsMasterData(Sheetdt) Then
            'Sheetdt.Columns("BaseCost").DataType = System.Type.GetType("System.Decimal")
            'Sheetdt.Columns("Base Accumulated Depn#").DataType = System.Type.GetType("System.Decimal")
            'Sheetdt.Columns("Current Book Value").DataType = System.Type.GetType("System.Decimal")
            grd.DataSource = Sheetdt
            'grdView.Columns("Price(in SAR)").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            'grdView.Columns("Price").DisplayFormat.FormatString = "#.##"
            'grdView.Columns("Base Accumulated Depn#").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            'grdView.Columns("Base Accumulated Depn#").DisplayFormat.FormatString = "#.##"
            'grdView.Columns("Current Book Value").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            'grdView.Columns("Current Book Value").DisplayFormat.FormatString = "#.##"
            Return True
        Else
            Messages.ErrorMessage("Columns are not correct, Excel sheet must contains Import columns")
            Return False
        End If
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

    Private Function GetCompleteLocationCode(ByVal loc As AssetLocation) As String
        Dim CompleteCode As String = loc.City
        If loc.Building <> "" Then
            CompleteCode &= " \ " & loc.Building
        End If

        If loc.Floor <> "" Then
            CompleteCode &= " \ " & loc.Floor
        End If

        If loc.Room <> "" Then
            CompleteCode &= " \ " & loc.Room
        End If

        If loc.SubRoom <> "" Then
            CompleteCode &= " \ " & loc.SubRoom
        End If

        'If loc.Station <> "" Then
        '    CompleteCode &= " \ " & loc.StationID
        'End If
        Return CompleteCode
    End Function

    Private Function ImportLocationFromAssets(ByVal loc As AssetLocation, ByVal ReplaceIfFound As Boolean) As String
        Dim objattLocation As New attLocation
        Dim objBALLocation As New BALLocation
        Dim objCompany As New BALCompany

        Dim CityID As String = ""
        Dim BuildingID As String = ""
        Dim FloorID As String = ""
        Dim RoomID As String = ""
        Dim StationID As String = ""

        Dim CompleteLocCode As String = GetCompleteLocationCode(loc)
        If String.IsNullOrEmpty(CompleteLocCode) Then
            Return "1"
        End If

        CityID = objBALLocation.GetLocIDByCompleteDesc(loc.City)
        If Not String.IsNullOrEmpty(loc.City) Then
            If CityID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = loc.City
                CityID = objBALLocation.GetRootLocID()
                objattLocation.HierCode = CityID
                objattLocation.Code = loc.CityID
                objattLocation.locLevel = 0
                objattLocation.CompanyID = loc.CompanyCode
                objBALLocation.Insert_Location(objattLocation)
            Else 'update the location
                If ReplaceIfFound Then
                    objattLocation = New attLocation
                    objattLocation.HierCode = CityID
                    objattLocation.Description = loc.City
                    objattLocation.Code = loc.CityID
                    objattLocation.CompanyID = loc.CompanyCode
                    objattLocation.locLevel = 0
                    objBALLocation.Update_Location(objattLocation)
                End If
            End If
        Else
            Return "1" 'Unknown location
        End If

        BuildingID = objBALLocation.GetLocIDByCompleteDesc(loc.City & " \ " & loc.Building)
        If Not String.IsNullOrEmpty(loc.Building) Then
            If BuildingID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = loc.Building
                BuildingID = objBALLocation.GetChildLocID(CityID)
                objattLocation.HierCode = BuildingID
                objattLocation.Code = loc.BuildingID
                objattLocation.locLevel = 1
                objattLocation.CompanyID = loc.CompanyCode
                objBALLocation.Insert_Location(objattLocation)
            Else
                If ReplaceIfFound Then
                    objattLocation = New attLocation
                    objattLocation.HierCode = BuildingID
                    objattLocation.Description = loc.Building
                    objattLocation.Code = loc.BuildingID
                    objattLocation.locLevel = 1
                    objattLocation.CompanyID = loc.CompanyCode
                    objBALLocation.Update_Location(objattLocation)
                End If
            End If
        Else
            Return CityID
        End If


        FloorID = objBALLocation.GetLocIDByCompleteDesc(loc.City & " \ " & loc.Building & " \ " & loc.Floor)
        If Not String.IsNullOrEmpty(loc.Floor) Then
            If FloorID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = loc.Floor
                FloorID = objBALLocation.GetChildLocID(BuildingID)
                objattLocation.HierCode = FloorID
                objattLocation.Code = loc.FloorID
                objattLocation.locLevel = 2
                objattLocation.CompanyID = loc.CompanyCode
                objBALLocation.Insert_Location(objattLocation)
            Else
                If ReplaceIfFound Then
                    objattLocation = New attLocation
                    objattLocation.HierCode = FloorID
                    objattLocation.Description = loc.Floor
                    objattLocation.Code = loc.FloorID
                    objattLocation.locLevel = 2
                    objattLocation.CompanyID = loc.CompanyCode
                    objBALLocation.Update_Location(objattLocation)
                End If
            End If
        Else
            Return BuildingID
        End If

        RoomID = objBALLocation.GetLocIDByCompleteDesc(loc.City & " \ " & loc.Building & " \ " & loc.Floor & " \ " & loc.Room)
        If Not String.IsNullOrEmpty(loc.Room) Then
            If RoomID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = loc.Room
                RoomID = objBALLocation.GetChildLocID(FloorID)
                objattLocation.HierCode = RoomID
                objattLocation.Code = loc.RoomID
                objattLocation.locLevel = 3
                objattLocation.CompanyID = loc.CompanyCode
                objBALLocation.Insert_Location(objattLocation)
            Else
                If ReplaceIfFound Then
                    objattLocation = New attLocation
                    objattLocation.HierCode = RoomID
                    objattLocation.Description = loc.Room
                    objattLocation.Code = loc.RoomID
                    objattLocation.locLevel = 3
                    objattLocation.CompanyID = loc.CompanyCode
                    objBALLocation.Update_Location(objattLocation)
                End If
            End If
        Else
            Return FloorID
        End If

        StationID = objBALLocation.GetLocIDByCompleteDesc(loc.City & " \ " & loc.Building & " \ " & loc.Floor & " \ " & loc.Room & " \ " & loc.SubRoom)
        If Not String.IsNullOrEmpty(loc.SubRoom) Then
            If StationID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = loc.SubRoom
                StationID = objBALLocation.GetChildLocID(RoomID)
                objattLocation.HierCode = StationID
                objattLocation.Code = loc.SubRoomID
                objattLocation.locLevel = 4
                objattLocation.CompanyID = loc.CompanyCode
                objBALLocation.Insert_Location(objattLocation)
            Else
                If ReplaceIfFound Then
                    objattLocation = New attLocation
                    objattLocation.HierCode = StationID
                    objattLocation.Description = loc.SubRoom
                    objattLocation.Code = loc.SubRoomID
                    objattLocation.locLevel = 4
                    objattLocation.CompanyID = loc.CompanyCode
                    objBALLocation.Update_Location(objattLocation)
                End If
            End If
        Else
            Return RoomID
        End If
        'Return RoomID
        Return StationID
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


    Private Function ImportAsset(ByVal FocuseRowHandel As Integer, ByVal CatID As String, ByVal LocationID As String, ByVal CustodianID As String, ByVal CostCenterID As String, ByVal CompanyID As String, ByVal AstBrandID As String, ByRef dtAssets As DataTable, ByVal dtAssetDetails As DataTable, ByVal dtInvSch As DataTable, ByVal SupplierID As String) As Boolean
        'Add Asset Item
        Try
            Dim ItemDesc As String = grdView.GetRowCellValue(FocuseRowHandel, ("Description")).ToString.Trim
            Dim AssetRefNumber As String = grdView.GetRowCellValue(FocuseRowHandel, ("Ref #")).ToString.Trim
            Dim AssetItemID As String = String.Empty
            If Not String.IsNullOrEmpty(ItemDesc) Then
                ItemDesc = ItemDesc.ToString.Replace("'", "")
                Dim AssetRows As DataRow() = dtAssets.Select("AstDesc = '" & ItemDesc & "' and AstCatID = '" & CatID & "'")
                If AssetRows.Length <= 0 Then
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
                Else
                    AssetItemID = AssetRows(0)("itemcode")
                End If
            End If


            Dim objattAssetDetails As New attAssetDetails
            Dim objBALAssetDetails As New BALAssetDetails
            Dim AssetID As String = objBALAssetDetails.Generate_AssetID()
            Dim AssetBaseCost As Decimal = 0
            Try
                If Not Decimal.TryParse(grdView.GetRowCellDisplayText(FocuseRowHandel, ("Price")).ToString.Trim, AssetBaseCost) Then
                    AssetBaseCost = 0
                End If
            Catch ex As Exception
                AssetBaseCost = 0
            End Try
            objattAssetDetails.BaseCost = AssetBaseCost
            objattAssetDetails.CustodianID = CustodianID
            objattAssetDetails.InsID = 0
            objattAssetDetails.POCode = 0
            objattAssetDetails.SerailNo = grdView.GetRowCellDisplayText(FocuseRowHandel, ("S/N")).ToString.Trim
            objattAssetDetails.Discount = 0
            objattAssetDetails.ItemCode = AssetItemID
            objattAssetDetails.Tax = 0
            objattAssetDetails.TransRemarks = String.Empty
            objattAssetDetails.DispDate = Nothing
            objattAssetDetails.CompanyID = CompanyID
            objattAssetDetails.AstBrandID = AstBrandID
            objattAssetDetails.AstDesc = grdView.GetRowCellDisplayText(FocuseRowHandel, ("Asset Description")).ToString.Trim '
            objattAssetDetails.AstDesc2 = String.Empty
            objattAssetDetails.AstModel = grdView.GetRowCellDisplayText(FocuseRowHandel, ("Model")).ToString.Trim
            objattAssetDetails.GLCode = "1"

            objattAssetDetails.SuppID = SupplierID
            objattAssetDetails.Capex = ""
            objattAssetDetails.PoErp = ""
            objattAssetDetails.Plate = ""
            objattAssetDetails.GRN = ""
            objattAssetDetails.RefCode = ""

            objattAssetDetails.LocID = LocationID
            objattAssetDetails.NoPiece = 1
            objattAssetDetails.Disposed = "0"
            objattAssetDetails.IsSold = 0


            objattAssetDetails.CostCenterID = CostCenterID
            objattAssetDetails.InventoryNumber = String.Empty
            objattAssetDetails.BussinessArea = String.Empty


            Dim AstNum As Int64 = 0
            'Extract Asset number from the barcode field: TOD015702
            objattAssetDetails.AstNum = AssetRefNumber
            objattAssetDetails.RefNo = AssetRefNumber
            If String.IsNullOrEmpty(AssetRefNumber) Then
                objattAssetDetails.RefNo = objattAssetDetails.AstNum
            End If

            objattAssetDetails.PKeyCode = AssetID
            objattAssetDetails.BarCode = AssetRefNumber
            objattAssetDetails.IsDataChanged = False


            objattAssetDetails.InvNumber = grdView.GetRowCellDisplayText(FocuseRowHandel, ("Invoice No#")).ToString.Trim
            objattAssetDetails.PONumber = grdView.GetRowCellDisplayText(FocuseRowHandel, ("PO #")).ToString.Trim

            objattAssetDetails.CustomFld1 = grdView.GetRowCellDisplayText(FocuseRowHandel, ("Expiry Date")).ToString.Trim
            objattAssetDetails.CustomFld2 = grdView.GetRowCellDisplayText(FocuseRowHandel, ("ePR#")).ToString.Trim
            objattAssetDetails.CustomFld3 = grdView.GetRowCellValue(FocuseRowHandel, ("Service Tag")).ToString.Trim
            objattAssetDetails.CustomFld4 = String.Empty
            objattAssetDetails.CustomFld5 = String.Empty

            objattAssetDetails.EvaluationGroup1 = String.Empty
            objattAssetDetails.EvaluationGroup2 = String.Empty
            objattAssetDetails.EvaluationGroup3 = String.Empty
            objattAssetDetails.EvaluationGroup4 = String.Empty


            Dim UsefulLife As Integer = 5

            Dim DetailsRows As DataRow() = dtAssetDetails.Select("RefNo = '" & objattAssetDetails.RefNo & "'")
            If DetailsRows.Length > 0 Then
                objattAssetDetails.PurDate = DetailsRows(0)("PurDate")
                objattAssetDetails.SrvDate = DetailsRows(0)("SrvDate")
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
                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(FocuseRowHandel, ("Invoice Date")).ToString.Trim) Then
                    objattAssetDetails.PurDate = grdView.GetRowCellValue(FocuseRowHandel, ("Invoice Date")).ToString.Trim
                Else
                    objattAssetDetails.PurDate = Date.MinValue
                End If
                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(FocuseRowHandel, ("Service Date")).ToString.Trim) Then
                    objattAssetDetails.SrvDate = grdView.GetRowCellValue(FocuseRowHandel, ("Service Date")).ToString.Trim
                Else
                    objattAssetDetails.SrvDate = Date.MinValue
                End If
                'objattAssetDetails.SrvDate = 

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
                    objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, AssetBaseCost, objattAssetDetails.CompanyID, 1, UsefulLife, 0, objattAssetDetails.SrvDate, False)

                    Dim objattAstHistory As attAstHistory
                    Dim objBALAst_History As New BALAst_History
                    objattAstHistory = New attAstHistory()
                    objattAstHistory.AstID = AssetID
                    'Insert the history for all the schedules 
                    For Each dr As DataRow In dtInvSch.Rows
                        objattAstHistory.Status = 0
                        objattAstHistory.InvSchCode = dr("InvSchCode")
                        objattAstHistory.HisDate = DateTime.Now
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

    Private Function ImportGLCode(ByVal GLCode As String) As String
        Dim objattGLCode As New attGLCode
        Dim objBALGLCode As New BALGLCode
        objattGLCode.GLDesc = GLCode
        Dim dt As DataTable = objBALGLCode.GetAll_GLCodes(objattGLCode)
        If dt.Rows.Count > 0 Then
            GLCode = dt.Rows(0)("GLCode")
        Else

            objattGLCode.PKeyCode = objBALGLCode.GetNextPKey_GL()
            objattGLCode.GLDesc = GLCode
            objattGLCode.CompanyId = 1
            objBALGLCode.Insert_GLCode(objattGLCode)
            GLCode = objattGLCode.PKeyCode
        End If
        Return GLCode
    End Function

    Private Function ImportEmp(ByVal EmpID As String, ByVal EmpCode As String, ByVal EmpName As String, ByVal EmpEmail As String) As String
        Dim objattCustodian As New attCustodian
        Dim objBALCustodian As New BALCustodian

        objattCustodian.CustodianCode = EmpCode
        Dim dt As DataTable = objBALCustodian.CheckID(objattCustodian)
        EmpID = objBALCustodian.GetNextPKey_Custodian()

        Dim CustodianID As String = ""
        If dt.Rows.Count > 0 Then
            CustodianID = dt.Rows(0)("CustodianID")
        Else
            objattCustodian.PKeyCode = EmpID
            objattCustodian.CustodianName = EmpName
            objattCustodian.DepartmentID = 1
            objattCustodian.DesignationID = 1
            objattCustodian.CustodianPhone = ""
            objattCustodian.CustodianCode = EmpCode
            objattCustodian.CustodianEmail = EmpEmail
            objBALCustodian.Insert_Custodian(objattCustodian)
            CustodianID = objattCustodian.PKeyCode
        End If
        Return CustodianID
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


            'Dim objGLCode As New attGLCode
            'Dim objBALGLCode As New BALGLCode
            'objGLCode.GLDesc = "GL " & CompanyCode
            'objGLCode.PKeyCode = objBALGLCode.GetNextPKey_GL()
            'objGLCode.CompanyId = objattCompany.PKeyCode
            'objBALGLCode.Insert_GLCode(objGLCode)

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
        objattDepPolicy.SalvageValue = 1
        objattDepPolicy.SalvageYear = 5
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
