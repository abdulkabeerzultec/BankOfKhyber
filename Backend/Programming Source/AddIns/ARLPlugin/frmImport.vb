Imports DevExpress.XtraGrid
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Windows.Forms

Public Class frmImport
    Dim ObjIntegration As New BALNMCIntegration
    Dim MyConnection As System.Data.OleDb.OleDbConnection
    Private BreakImport As Boolean = False

    Private _ConnectionString As String
    Private _Query As String

    Dim SuccessMessage As String = "Success."
    Dim ExistMessage As String = "AssetTag Already Exist. Ignored"
    Dim AssetTagNotExistMessage As String = "AssetTag not exist. Ignored"

    Structure AssetLocation
        Dim CityName As String
        Dim BuildingName As String
        Dim FloorName As String
        Dim RoomName As String
        Dim SectionName As String
        Dim LocationID As String
    End Structure

    Public Property Query() As String
        Get
            Return _Query
        End Get
        Set(ByVal value As String)
            _Query = value
        End Set
    End Property

    Public Property ConnectionString() As String
        Get
            Return _ConnectionString
        End Get
        Set(ByVal value As String)
            _ConnectionString = value
        End Set
    End Property

    Public Function SilentImport(ByVal lbl As DevExpress.XtraEditors.LabelControl, ByVal pb As DevExpress.XtraEditors.ProgressBarControl) As String
        lbl.Text = "Processing Data Import file..."
        lbl.Update()
        Dim msg As String = ProcessAssetCreationFile()
        If String.IsNullOrEmpty(msg) Then
            Application.DoEvents()
            lbl.Text = "Importing Asset Master Data from oracle..."
            lbl.Update()
            Return ImportAssetCreation()
        Else
            SaveToLogFile(msg, True)
            lbl.Text = msg
            lbl.Update()
            Return msg
        End If
    End Function


    Private Function CheckColumnsMasterData(ByVal dt As DataTable) As Boolean
        Dim Result As Boolean = True
        If dt.Columns("ASSET_ID") Is Nothing Then
            Result = False
        End If
        If dt.Columns("MAJOR_CATEGORY") Is Nothing Then
            Result = False
        End If

        If dt.Columns("MAJ_CAT_DESC") Is Nothing Then
            Result = False
        End If

        If dt.Columns("MIN_CAT_DESC") Is Nothing Then
            Result = False
        End If

        If dt.Columns("SUB_CATEGORY") Is Nothing Then
            Result = False
        End If
        If dt.Columns("DESCRIPTION") Is Nothing Then
            Result = False
        End If
        If dt.Columns("COST") Is Nothing Then
            Result = False
        End If
        If dt.Columns("DATE_PLACED_IN_SERVICE") Is Nothing Then
            Result = False
        End If

        If dt.Columns("PHYSICAL_LOCATION") Is Nothing Then
            Result = False
        End If

        If dt.Columns("DEPARTMENT") Is Nothing Then
            Result = False
        End If
        If dt.Columns("SECTION") Is Nothing Then
            Result = False
        End If
        If dt.Columns("EMPLOYEE_NUMBER") Is Nothing Then
            Result = False
        End If

        If dt.Columns("FULL_NAME") Is Nothing Then
            Result = False
        End If
        If dt.Columns("SERIAL_NO") Is Nothing Then
            Result = False
        End If
        If dt.Columns("ASSET_TYPE") Is Nothing Then
            Result = False
        End If
      
        Return Result
    End Function

    Private Function ProcessAssetCreationFile() As String
        Application.DoEvents()
        MyConnection = New System.Data.OleDb.OleDbConnection(ConnectionString)
        Dim dtOracle As New DataTable
        ShowProgressDialog()
        Try
            If Not String.IsNullOrEmpty(Query) Then
                MyConnection.Open()

                Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
                MyCommand = New System.Data.OleDb.OleDbDataAdapter(Query, MyConnection)
                MyCommand.Fill(dtOracle)
                MyConnection.Close()
            Else
                Return "Please check your query."
            End If
        Catch ex As Exception
            Return ex.Message
        Finally
            MyConnection.Close()
            CloseProgressDialog()
        End Try
        ShowProgressDialog("Please Wait...", "Validating grid values...")
        pbImport.Position = 1
        pbImport.Properties.Step = 1
        pbImport.Properties.Maximum = dtOracle.Rows.Count
        pbImport.Refresh()
        pbImport.Visible = True

        Try
            If CheckColumnsMasterData(dtOracle) Then
                dtOracle.Columns.Add("Selection", Type.GetType("System.Boolean"))
                dtOracle.Columns("Selection").SetOrdinal(0)
                dtOracle.Columns.Add("Message", Type.GetType("System.String"))
                grd.DataSource = dtOracle
                grdView.BestFitColumns()
                For i As Integer = 0 To grdView.RowCount - 1
                    Dim AstNum As String = grdView.GetRowCellValue(i, ("ASSET_ID")).ToString.Trim
                    Dim AstNumber As Integer

                    If Not Integer.TryParse(AstNum, AstNumber) Then
                        grdView.SetRowCellValue(i, "Message", "Asset Number not valid.")
                        grdView.SetRowCellValue(i, "Selection", False)
                    Else
                        grdView.SetRowCellValue(i, "Message", SuccessMessage)
                        grdView.SetRowCellValue(i, "Selection", True)
                    End If

                    grdView.UpdateCurrentRow()
                    pbImport.PerformStep()
                    pbImport.Update()
                    Application.DoEvents()
                Next
                grd.UseEmbeddedNavigator = True
                grd.EmbeddedNavigator.Buttons.Remove.Visible = False
                grd.EmbeddedNavigator.Buttons.Append.Visible = False
                grd.EmbeddedNavigator.Buttons.Edit.Visible = False
                grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
                grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

                Dim styleFormatCondition1 As StyleFormatCondition = New StyleFormatCondition()
                styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.OrangeRed
                styleFormatCondition1.Appearance.Options.UseBackColor = True
                styleFormatCondition1.ApplyToRow = True
                styleFormatCondition1.Column = grdView.Columns("Message")
                styleFormatCondition1.Condition = FormatConditionEnum.NotEqual
                styleFormatCondition1.Value1 = SuccessMessage
                grdView.FormatConditions.Add(styleFormatCondition1)
                Dim TotalRowsCount As Integer = grdView.RowCount
                Dim ValidRowsCount As Integer = GetGridValidRows(grdView)
                lblTotal.Text = String.Format("Total: {0}", TotalRowsCount)
                lblValid.Text = String.Format("Valid: {0}", ValidRowsCount)
                lblInvalid.Text = String.Format("Invalid: {0}", TotalRowsCount - ValidRowsCount)
                Return Nothing
            Else
                Return "Columns are not correct, data must contains Import columns"
            End If
        Catch ex As Exception
            Return ex.Message
        Finally
            pbImport.Visible = False
            CloseProgressDialog()
        End Try

    End Function

    Private Function GetGridValidRows(ByVal grd As DevExpress.XtraGrid.Views.Grid.GridView) As Integer
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Message", SuccessMessage)
        Dim SuccessCount As Integer = grd.RowCount
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Message", Nothing)
        Return SuccessCount
    End Function

    Private Sub frmImport_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim msg As String = ProcessAssetCreationFile()
        If String.IsNullOrEmpty(msg) Then
            btnImport.Enabled = True
        Else
            Messages.ErrorMessage(msg)
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

        Dim objBALAst_INV_Schedule As New BALAst_INV_Schedule
        Dim dtInvSch As DataTable = objBALAst_INV_Schedule.Getcombo_Schedule()

        pbImport.Position = 1
        pbImport.Properties.Step = 1
        pbImport.Properties.Maximum = grdView.RowCount
        pbImport.Refresh()
        pbImport.Visible = True
        btnImport.Enabled = False
        Dim ErrorCount As Integer = 0
        Dim msg As String = String.Empty
        Try

            If grdView.RowCount > 0 Then
                'ObjIntegration.DisableAssetTriggers()

                'Dim objCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
                'Dim strQuery As New System.Text.StringBuilder
                'strQuery.Append("UPDATE AssetDetails SET Disposed = 1, DispDate=sysdate")
                'objCommand.CommandText = strQuery.ToString()
                'GenericDAL.DBOperations.ExecuteNonQuery(objCommand)
                ObjIntegration.DisposeAllAssets()
                For i As Integer = 0 To grdView.RowCount - 1
                    If grdView.GetRowCellValue(i, ("Message")).ToString.Contains(SuccessMessage) And CBool(grdView.GetRowCellValue(i, ("Selection"))) = True Then
                        'Import Company
                        Dim CompanyID As String = 1
                        'Import Category.
                        Dim CatID As String = 1
                        If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, "MAJOR_CATEGORY").ToString.Trim) Then
                            CatID = ImportCategory(grdView.GetRowCellValue(i, "MAJOR_CATEGORY"), grdView.GetRowCellValue(i, "SUB_CATEGORY"), grdView.GetRowCellValue(i, "MAJ_CAT_DESC"), grdView.GetRowCellValue(i, "MIN_CAT_DESC"))
                        End If
                        'Import location.
                        Dim loc As AssetLocation

                        loc.CityName = grdView.GetRowCellValue(i, ("DEPARTMENT")).ToString
                        loc.BuildingName = grdView.GetRowCellValue(i, ("SECTION")).ToString
                        loc.FloorName = grdView.GetRowCellValue(i, ("PHYSICAL_LOCATION")).ToString
                        loc.RoomName = ""
                        loc.SectionName = ""
                        loc.LocationID = ""
                        Dim LocID As String = ImportLocation(loc, CompanyID)

                        'Import GLCode
                        Dim GLCode As String = 1
                        Dim BookName As String = ""
                        If Not String.IsNullOrEmpty(BookName) Then
                            If BookName.Length > 50 Then
                                BookName = BookName.Substring(0, 50)
                            End If
                            GLCode = ImportGLCode(BookName, 1)
                        End If

                        'Import Employee
                        Dim EmpID As String = 1
                        If Not grdView.GetRowCellValue(i, ("EMPLOYEE_NUMBER")) Is Nothing AndAlso Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("EMPLOYEE_NUMBER")).ToString.Trim.Replace("'", "")) Then
                            EmpID = ImportEmp(grdView.GetRowCellValue(i, ("FULL_NAME")).ToString.Trim.Replace("'", ""), grdView.GetRowCellValue(i, ("EMPLOYEE_NUMBER")).ToString.Trim, grdView.GetRowCellValue(i, ("EMPLOYEE_NUMBER")).ToString.Trim)
                        End If


                        'Import(CostCenter)
                        Dim CostCenterID As String = 1
                        If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, ("DEPARTMENT")).ToString.Trim) Then
                            If grdView.GetRowCellValue(i, ("DEPARTMENT")).ToString.Trim.Length > 50 Then
                                CostCenterID = ImportCostCenter(grdView.GetRowCellValue(i, ("DEPARTMENT")).ToString.Trim.Substring(0, 50).Replace("'", ""))
                            Else
                                CostCenterID = ImportCostCenter(grdView.GetRowCellValue(i, ("DEPARTMENT")).ToString.Trim.Replace("'", ""))
                            End If
                        End If

                        Dim focusrowhandel As Integer = i
                        'Import(Assets)
                        If ImportAsset(focusrowhandel, CatID, LocID, EmpID, CostCenterID, CompanyID, GLCode, dtAssets, dtAssetsDetails, True, dtInvSch) Then
                            importedcount += 1
                        End If
                        pbImport.PerformStep()
                        pbImport.Update()
                        Application.DoEvents()
                    End If
                Next

                Dim time As String = (Environment.TickCount - Start) / 1000 & " S"
                msg = "Import Completed, imported Asset count = " & importedcount & ". Time(" & time & ")"
                pbImport.Visible = False
                Return msg
            Else
                msg = "No data to import."
                pbImport.Visible = False
                Return msg
            End If
        Catch ex As Exception
            msg = ex.Message
            Return ex.Message
        Finally
            SaveToLogFile(msg, False)
            pbImport.Visible = False
            btnImport.Enabled = True
            'ObjIntegration.EnabbleAssetTriggers()
        End Try
    End Function

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Try
            If Messages.QuestionMessage("Are you sure to start importing master data?") = DialogResult.Yes Then
                Dim msg As String = ImportAssetCreation()
                Messages.InfoMessage(msg)
                Me.Close()
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
        Return CompleteDesc
    End Function


    Private Function ImportAsset(ByVal FocuseRowHandel As Integer, ByVal CatID As String, ByVal LocationID As String, ByVal CustodianID As String, ByVal CostCenterID As String, ByVal CompanyID As String, ByVal GLCode As String, ByRef dtAssets As DataTable, ByVal dtAssetDetails As DataTable, ByVal IsNewAsset As Boolean, ByVal dtInvSch As DataTable) As Boolean
        'Add Asset Item
        Try
            Dim ItemDesc As String = grdView.GetRowCellValue(FocuseRowHandel, ("DESCRIPTION")).ToString.Trim
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
            objattAssetDetails.SerailNo = grdView.GetRowCellValue(FocuseRowHandel, ("SERIAL_NO")).ToString.Trim
            objattAssetDetails.Discount = 0
            objattAssetDetails.ItemCode = AssetItemID
            objattAssetDetails.Tax = 0
            objattAssetDetails.TransRemarks = String.Empty
            objattAssetDetails.DispDate = Nothing
            objattAssetDetails.CompanyID = CompanyID
            objattAssetDetails.AstBrandID = 1
            objattAssetDetails.AstDesc = grdView.GetRowCellValue(FocuseRowHandel, ("DESCRIPTION")).ToString.Trim
            objattAssetDetails.AstModel = ""
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
            objattAssetDetails.NoPiece = 1
            objattAssetDetails.Disposed = "0"
            objattAssetDetails.IsSold = 0
            objattAssetDetails.EvaluationGroup1 = ""
            objattAssetDetails.EvaluationGroup2 = ""
            objattAssetDetails.EvaluationGroup3 = ""
            objattAssetDetails.EvaluationGroup4 = ""
            objattAssetDetails.CustomFld1 = ""
            objattAssetDetails.CustomFld2 = ""
            objattAssetDetails.CustomFld3 = ""
            objattAssetDetails.CustomFld4 = ""
            objattAssetDetails.CustomFld5 = ""


            objattAssetDetails.CostCenterID = CostCenterID
            objattAssetDetails.InventoryNumber = String.Empty
            objattAssetDetails.BussinessArea = String.Empty
            Dim AstNum As String = grdView.GetRowCellValue(FocuseRowHandel, ("ASSET_ID")).ToString.Trim
            objattAssetDetails.RefNo = AstNum
            objattAssetDetails.AstNum = AstNum
            objattAssetDetails.PKeyCode = AssetID
            If String.IsNullOrEmpty(grdView.GetRowCellValue(FocuseRowHandel, ("ASSET_ID")).ToString.Trim) Then
                objattAssetDetails.BarCode = grdView.GetRowCellValue(FocuseRowHandel, ("ASSET_ID")).ToString.Trim
            Else
                objattAssetDetails.BarCode = grdView.GetRowCellValue(FocuseRowHandel, ("ASSET_ID")).ToString.Trim
            End If
            objattAssetDetails.IsDataChanged = False
            objattAssetDetails.PurDate = grdView.GetRowCellValue(FocuseRowHandel, ("DATE_PLACED_IN_SERVICE")).ToString.Trim
            objattAssetDetails.SrvDate = grdView.GetRowCellValue(FocuseRowHandel, ("DATE_PLACED_IN_SERVICE")).ToString.Trim

            Dim UsefulLife As Integer = 0

            Dim DetailsRows As DataRow() = dtAssetDetails.Select("AstNum = '" & objattAssetDetails.AstNum & "'")
            If DetailsRows.Length > 0 Then
                objattAssetDetails.PKeyCode = DetailsRows(0)("AstID")
                objattAssetDetails.LastEditBY = "Import Process"
                objattAssetDetails.LastEditDate = Now.Date
                If objBALAssetDetails.Update_AssetDetails(objattAssetDetails) Then
                    objBALAssetDetails.Update_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, 0, UsefulLife, 0, objattAssetDetails.SrvDate, False)
                End If
            Else

                objattAssetDetails.CreatedBY = "Import Process"
                objattAssetDetails.LastEditBY = "Import Process"
                objattAssetDetails.LastEditDate = Now.Date
                objattAssetDetails.CreationDate = Now.Date
                objattAssetDetails.InStockAsset = False
                objattAssetDetails.InvSchCode = 1
                objattAssetDetails.InvStatus = 0
                objattAssetDetails.StatusID = 1
                objattAssetDetails.Disposed = False

                If objBALAssetDetails.Insert_AssetDetails(objattAssetDetails, False) Then
                    objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, 0, UsefulLife, 0, objattAssetDetails.SrvDate, False)
                    Dim objattAstHistory As attAstHistory
                    Dim objBALAst_History As New BALAst_History


                    objattAstHistory = New attAstHistory()
                    objattAstHistory.AstID = objattAssetDetails.PKeyCode


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
                    'Dim dt As DataTable = objBALAst_History.GetAll_Ast_History(objattAstHistory)
                    'If dt.Rows.Count = 0 Then

                    'End If
                End If
            End If
            Return True
        Catch ex As Exception
            'Messages.ErrorMessage(ex.Message)
            Return False
        End Try
    End Function

    Private Function ImportLocation(ByVal loc As AssetLocation, ByVal CompanyID As String) As String
        Dim objattLocation As New attLocation
        Dim objBALLocation As New BALLocation

        Dim CityID As String = ""
        Dim BuildingID As String = ""
        Dim FloorID As String = ""
        Dim RoomID As String = ""
        Dim SectionID As String = ""

        Dim CityDesc As String = ""
        Dim BuildingDesc As String = ""
        Dim FloorDesc As String = ""
        Dim RoomDesc As String = ""
        Dim SectionDesc As String = ""

        Dim CompleteLocDesc As String = GetCompleteLocationDescription(loc)

        SectionID = objBALLocation.GetLocIDByCompleteDesc(CompleteLocDesc)
        'SectionDesc = objBALLocation.GetLocDescByID(SectionID)

        If SectionID <> "0" Then
            Return SectionID
        Else
            CityID = objBALLocation.GetLocIDByCompleteDesc(loc.CityName)
            If CityID = "0" Then
                objattLocation = New attLocation
                objattLocation.Description = loc.CityName
                CityID = objBALLocation.GetRootLocID()
                objattLocation.HierCode = CityID
                objattLocation.Code = CityID
                objattLocation.locLevel = 0
                objattLocation.CompanyID = CompanyID
                objBALLocation.Insert_Location(objattLocation)
            Else
                If String.IsNullOrEmpty(loc.BuildingName) Then
                    Return CityID
                End If
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
                objattLocation.CompanyID = CompanyID
                objBALLocation.Insert_Location(objattLocation)
            Else
                If String.IsNullOrEmpty(loc.FloorName) Then
                    Return BuildingID
                End If
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
                objattLocation.CompanyID = CompanyID
                objBALLocation.Insert_Location(objattLocation)
            End If
            Return FloorID
        End If
    End Function


    Private Function ImportEmp(ByVal EmpName As String, ByVal EmpID As String, ByVal EmpNumber As String) As String
        Dim objattCustodian As New attCustodian
        Dim objBALCustodian As New BALCustodian
        objattCustodian.PKeyCode = EmpID
        Dim dt As DataTable = objBALCustodian.CheckID(objattCustodian)
        Dim CustodianID As String = ""
        If dt.Rows.Count > 0 Then
            CustodianID = dt.Rows(0)("CustodianID")
        Else
            objattCustodian.PKeyCode = EmpID
            objattCustodian.CustodianAddress = EmpNumber
            objattCustodian.CustodianName = EmpName
            objattCustodian.CustodianCell = ""
            objattCustodian.CustodianPhone = ""
            objattCustodian.CustodianFax = ""
            objattCustodian.CustodianEmail= ""
            objattCustodian.DepartmentID = 1
            objattCustodian.DesignationID = 1
            objattCustodian.CustodianCode = ""
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

            Return CompanyID
        End If
    End Function

    ' This function will import the Main and sub categories and return subcat ID
    Private Function ImportCategory(ByVal MainCat As String, ByVal SubCat As String, ByVal MainCatDesc As String, ByVal SubCatDesc As String) As String
        Dim objattCategory As attCategory
        Dim objBALCategory As New BALCategory
        Dim SubCatID As String = ""

        Dim MainCatID As String = objBALCategory.GetCatIDByDesc(MainCatDesc, "")
        objattCategory = New attCategory
        If MainCatID = "" Then 'CatDesc not found
            objattCategory.AstCatDesc = MainCatDesc
            MainCatID = objBALCategory.GetRootCatID()
            objattCategory.AstCatID = MainCatID
            objattCategory.Code = MainCat
            objattCategory.catLevel = 0
            If objBALCategory.Insert_Category(objattCategory) Then
                AddDepPolicyForCat(objattCategory.AstCatID)
            End If
        End If

        If String.IsNullOrEmpty(SubCat) Or SubCat = "N/A" Then
            Return MainCatID
        Else
            SubCatID = objBALCategory.GetCatIDByDesc(SubCatDesc, MainCatID)
            objattCategory = New attCategory
            If SubCatID = "" Then
                objattCategory.AstCatDesc = SubCatDesc
                SubCatID = objBALCategory.GetChildCatID(MainCatID)
                objattCategory.AstCatID = SubCatID
                objattCategory.Code = SubCat
                objattCategory.catLevel = 1
                If objBALCategory.Insert_Category(objattCategory) Then
                    AddDepPolicyForCat(objattCategory.AstCatID)
                End If
            End If
        End If
        Return SubCatID
    End Function


    Private Sub AddDepPolicyForCat(ByVal CatID As String)
        'add asset book for the company if it's tracking or inventory edition.
        Dim objattDepPolicy As New attDepPolicy
        Dim objBALDepPolicy As New BALDepPolicy
        objattDepPolicy.CatDepID = CatID
        Dim dt As DataTable = objBALDepPolicy.CheckID(objattDepPolicy)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            objattDepPolicy.AstCatID = CatID
            objattDepPolicy.DepCode = 1
            objattDepPolicy.SalvageValue = 0
            objattDepPolicy.SalvageYear = 1
            objattDepPolicy.IsSalvageValuePercent = False
            objBALDepPolicy.Insert_DepPolicy(objattDepPolicy)
        End If

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