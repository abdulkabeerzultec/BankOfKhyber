Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO
Imports System.Drawing.Printing
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Text
Imports DevExpress.XtraPrinting

Public Class frmAdmin

#Region "Declerations"
    Dim objattCustodian As attCustodian
    Dim objBALCustodian As New BALCustodian

    Dim objattLocation As attLocation
    Dim objBALLocation As New BALLocation
    Dim objattCategory As attCategory
    Dim objBALCategory As New BALCategory
    Dim objattAssetDetails As attAssetDetails
    Dim objBALAssetDetails As New BALAssetDetails
    Dim SelNode As TreeNode
    Dim objattAstHistory As attAstHistory
    Private objBALAst_History As New BALAst_History
    Dim Loc_BCodeCount As Integer = 0
    Dim dsAssetsDetails As New DataTable

    Public IsDemokey As Boolean = False
    Private _RoleID As Integer
    Public Property RoleID() As Integer
        Get
            Return _RoleID
        End Get
        Set(ByVal value As Integer)
            _RoleID = value
        End Set
    End Property

#End Region
    Private Sub frmDashboard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Try
            GetCompleteFormData()

            Dim objBALCompanyInfo As New BALCompanyInfo
            Dim ds As DataTable
            ds = objBALCompanyInfo.GetAll_CompanyInfo(New attCompanyInfo)
            If (ds.Rows.Count > 0) Then
                AppConfig.CompanyName = ds.Rows(0)("Name")
                Try
                    If ds.Rows(0)("Image").ToString <> "" Then
                        Dim bits As Byte() = CType(ds.Rows(0)("Image"), Byte())
                        Dim ms As New MemoryStream(bits, 0, bits.Length)
                        CompanyLogoImage = Image.FromStream(ms, True)
                    End If
                Catch ex As Exception

                End Try
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub GetCompleteFormData()
        FillCategoryTree(trvAssetCat)
        FillLocationTree(TrvLocations)

        Get_Cust()
        Get_AssetsDetails()
        format_grdAssets()
        tabPageAssetCategory.Text = "Asset By Class"
        grdAssets.Dock = DockStyle.Fill
        grdAssets.Visible = True
    End Sub
    Public Function GetAssetsAdministartionGrid(ByVal objattAssetDetails As attAssetDetails) As IDbCommand

        Dim strQuery As New StringBuilder
        Dim objCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
        strQuery.Append(" select AssetDetails.AstID, AssetDetails.BarCode,astnum as AssetNumber,AssetDetails.SrvDate as ReceiveDate")
        strQuery.Append(" ,Assets.AstDesc as Description,AssetDetails.SerailNo as 'Serial Number',PurchaseOrder.Quotation as DoNumber,CustomFld1, AssetStatus.Status , CustomFld3")
        strQuery.Append(" ,Custodian.CustodianName,Location.LocationFullPath,Category.CatFullPath,PurchaseOrder.ModeDelivery as DoDescription, CustomFld2, EvaluationGroup1")
        strQuery.Append(" ,Supplier.SuppName as SupplierName,AssetDetails.BaseCost + Tax - AssetDetails.Discount as TotalCost, CustomFld5")
        strQuery.Append(" , Assetdetails.AstDesc as AssetDescription1, Brand.AstBrandName,AssetDetails.Warranty,(select max(LastEditDate) from StockTransferItems where StockTransferItems.AstID = AssetDetails.AstID) as LastTransDate")
        'strQuery.Append(" ,CreatedBY,CostCenter.CostNumber")
        'strQuery.Append(" ,LastInventoryDate,AssetDetails.LabelCount ")
        'strQuery.Append(" ,InvStatus,AstNum,Cast (Assets.ItemCode as bigint) as itemcode,CostCenterID,CostCenter.CostName,Assets.AstDesc,AssetDetails.AstID,Location.LocDesc,AssetDetails.LocID,Assets.AstCatID,AssetDetails.CustodianID,Companies.CompanyName,AssetDetails.Refno,AssetDetails.CompanyID,AssetDetails.AstModel,Category.AstCatDesc,AssetDetails.BaseCost,AssetDetails.Tax,AssetDetails.InsID,AssetDetails.InvNumber,AssetDetails.POCode,AssetDetails.SuppID,AssetDetails.Disposed,AssetDetails.Discount,RefCode,Plate,Poerp,Capex,Grn,AssetDetails.NoPiece,AssetDetails.GLCode,AssetDetails.PONumber,Companies.CompanyCode")
        'strQuery.Append(" , CustomFld4, EvaluationGroup2, EvaluationGroup3, EvaluationGroup4 ")
        strQuery.Append(" from Assets")
        strQuery.Append("       inner join(AssetDetails ")
        strQuery.Append("           left outer join Location on AssetDetails.LocID = Location.LocID ")
        strQuery.Append("        left outer join Companies on AssetDetails.CompanyID = Companies.CompanyID ")
        strQuery.Append("               left outer join AstBooks on AssetDetails.AstID = AstBooks.AstID ")
        strQuery.Append("           left outer join Custodian on AssetDetails.CustodianID = custodian.CustodianID ")
        strQuery.Append("        left outer join CostCenter on AssetDetails.CostCenterID = CostCenter.CostID )")
        strQuery.Append("           on Assets.ItemCode = AssetDetails.ItemCode ")
        strQuery.Append(" left outer join Category on Assets.AstCatID = Category.AstCatID ")
        strQuery.Append(" left outer join Brand on AssetDetails.AstBrandId = Brand.AstBrandID ")
        strQuery.Append(" left outer join AssetStatus on AssetDetails.StatusID = AssetStatus.ID ")
        strQuery.Append(" left outer join Supplier on AssetDetails.SuppID = Supplier.SuppID ")
        strQuery.Append(" left outer join PurchaseOrder on AssetDetails.POCode = PurchaseOrder.POCode ")

        strQuery.Append("  where AssetDetails.IsDeleted = 0 and Disposed = 0")

        If Not String.IsNullOrEmpty(AppConfig.CompanyIDS) And AppConfig.CompanyIDS <> "0" Then
            strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
        End If
        strQuery.Append("  order by Barcode")

        objCommand.CommandText = strQuery.ToString()
        Return objCommand
    End Function

    Private Sub Get_AssetsDetails()
        Try
            Application.DoEvents()
            dsAssetsDetails = GenericDAL.DBOperations.ExecuteReader(GetAssetsAdministartionGrid(New attAssetDetails))
            grdAssets.DataSource = dsAssetsDetails
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


#Region "Loading locations"
    Dim ht As New Hashtable
    Public Sub FillLocationTree(ByVal trv As TreeView)
        Try
            Dim ds As New DataTable
            Dim rcount As Integer
            objattLocation = New attLocation
            trv.Nodes.Clear()
            ds = objBALLocation.GetComboLocations(objattLocation)
            If ds Is Nothing = False Then
                rcount = ds.Rows.Count
                If rcount > 0 Then
                    For Each dr As DataRow In ds.Rows
                        'Create_Tree_Loc(dr("LocID"), dr("LocDesc"))
                        Create_TreeNode(dr("LocID"), dr("LocDesc"), trv)
                    Next
                End If
            End If

            trv.CollapseAll()
            trv.Refresh()
            Me.Invalidate()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally
            ht.Clear()
        End Try
    End Sub

    Public Sub Create_TreeNode(ByVal NID As String, ByVal NDesc As String, ByVal trv As TreeView)
        If NID.IndexOf("-") < 0 Then
            Dim NNode As New TreeNode(NDesc)
            NNode.Tag = NID
            trv.Nodes.Add(NNode)
            ht.Add(NID, NNode)
        Else
            Dim ParentID As String = NID.Substring(0, NID.LastIndexOf("-"))
            Dim NNode As New TreeNode(NDesc)
            NNode.Tag = NID
            Dim PNode As TreeNode = CType(ht.Item(ParentID), TreeNode)

            If PNode IsNot Nothing Then
                PNode.Nodes.Add(NNode)
                ht.Add(NID, NNode)
            End If
        End If
    End Sub

#End Region

#Region "Loading Categories"
    Private Sub FillCategoryTree(ByVal trv As TreeView)
        Try
            Dim ds As New DataTable
            Dim rcount As Integer
            objattCategory = New attCategory
            ds = objBALCategory.GetAll_Category(objattCategory)
            trv.Nodes.Clear()
            If ds Is Nothing = False Then
                rcount = ds.Rows.Count
                If rcount > 0 Then
                    For Each dr As DataRow In ds.Rows
                        Create_TreeNode(dr("AstCatID"), dr("AstCatDesc"), trv)
                    Next
                End If
            End If

            trv.ExpandAll()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally
            ht.Clear()
        End Try

    End Sub

    Private Sub format_Custgrd()
        grdCustView.Columns(0).Caption = "ID"
        grdCustView.Columns(0).Width = 75
        grdCustView.Columns(0).Name = "CustodianID"
        grdCustView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdCustView.Columns(1).Caption = "Code"
        grdCustView.Columns(1).Width = 75
        grdCustView.Columns(1).Name = "CustodianCode"

        grdCustView.Columns(2).Caption = "Name"
        grdCustView.Columns(2).Width = 200
        grdCustView.Columns(2).Name = "CustodianName"

        grdCust.UseEmbeddedNavigator = True
        grdCust.EmbeddedNavigator.Buttons.Append.Visible = False
        grdCust.EmbeddedNavigator.Buttons.Edit.Visible = False
        grdCust.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grdCust.EmbeddedNavigator.Buttons.Remove.Visible = False
        grdCust.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        addGridMenu(grdCust)
    End Sub


    Private Sub format_grdAssets()
        GetGridColumn(grdAssetsView, "AstID").Visible = False
        'GetGridColumn(grdAssetsView, "CustodianName").Caption = "Employee"
        'GetGridColumn(grdAssetsView, "LocationFullPath").Caption = "Location"
        'GetGridColumn(grdAssetsView, "LabelCount").Caption = "Label Count"
        'GetGridColumn(grdAssetsView, "LabelCount").Width = 90
        'GetGridColumn(grdAssetsView, "LabelCount").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        'GetGridColumn(grdAssetsView, "InvStatus").Visible = False
        'GetGridColumn(grdAssetsView, "AstNum").Visible = False
        'GetGridColumn(grdAssetsView, "itemcode").Visible = False
        'GetGridColumn(grdAssetsView, "CostNumber").Visible = False
        'GetGridColumn(grdAssetsView, "CostCenterID").Visible = False
        'GetGridColumn(grdAssetsView, "CostName").Visible = False
        'GetGridColumn(grdAssetsView, "AstDesc").Visible = False

        'GetGridColumn(grdAssetsView, "LocDesc").Visible = False
        'GetGridColumn(grdAssetsView, "LocID").Visible = False
        'GetGridColumn(grdAssetsView, "AstCatID").Visible = False
        'GetGridColumn(grdAssetsView, "CustodianID").Visible = False
        'GetGridColumn(grdAssetsView, "CompanyName").Visible = False
        'GetGridColumn(grdAssetsView, "Refno").Visible = False
        'GetGridColumn(grdAssetsView, "CompanyID").Visible = False
        'GetGridColumn(grdAssetsView, "AstModel").Visible = False
        'GetGridColumn(grdAssetsView, "AstCatDesc").Visible = False
        'GetGridColumn(grdAssetsView, "BaseCost").Visible = False
        'GetGridColumn(grdAssetsView, "Tax").Visible = False
        'GetGridColumn(grdAssetsView, "SrvDate").Visible = False
        'GetGridColumn(grdAssetsView, "InsID").Visible = False
        'GetGridColumn(grdAssetsView, "InvNumber").Visible = False
        'GetGridColumn(grdAssetsView, "POCode").Visible = False
        'GetGridColumn(grdAssetsView, "SuppID").Visible = False
        'GetGridColumn(grdAssetsView, "Disposed").Visible = False
        'GetGridColumn(grdAssetsView, "Discount").Visible = False
        'GetGridColumn(grdAssetsView, "RefCode").Visible = False
        'GetGridColumn(grdAssetsView, "Plate").Visible = False
        'GetGridColumn(grdAssetsView, "Poerp").Visible = False
        'GetGridColumn(grdAssetsView, "Capex").Visible = False
        'GetGridColumn(grdAssetsView, "Grn").Visible = False
        'GetGridColumn(grdAssetsView, "NoPiece").Visible = False
        'GetGridColumn(grdAssetsView, "GLCode").Visible = False
        'GetGridColumn(grdAssetsView, "PONumber").Visible = False

        Dim objCustomFields As New BALCustomFields
        'GetGridColumn(grdAssetsView, "CustomFld2").Caption = objCustomFields.GetFieldCaption("lblCustomField2")
        GetGridColumn(grdAssetsView, "CustomFld1").Visible = True

        'GetGridColumn(grdAssetsView, "CustomFld1").Caption = "Old DO#"
        GetGridColumn(grdAssetsView, "CustomFld1").Caption = objCustomFields.GetFieldCaption("lblCustomField1")
        GetGridColumn(grdAssetsView, "CustomFld2").Visible = False

        GetGridColumn(grdAssetsView, "CustomFld3").Caption = objCustomFields.GetFieldCaption("lblCustomField3")
        'GetGridColumn(grdAssetsView, "CustomFld4").Caption = objCustomFields.GetFieldCaption("lblCustomField4")
        GetGridColumn(grdAssetsView, "CustomFld5").Caption = objCustomFields.GetFieldCaption("lblCustomField5")
        'AssetDetails.AstID, AssetDetails.BarCode,astnum as AssetNumber
        GetGridColumn(grdAssetsView, "AstID").Visible = False
        GetGridColumn(grdAssetsView, "BarCode").Visible = False
        GetGridColumn(grdAssetsView, "AssetNumber").Visible = False
        GetGridColumn(grdAssetsView, "EvaluationGroup1").Caption = objCustomFields.GetFieldCaption("lblCustomField6")
        'GetGridColumn(grdAssetsView, "EvaluationGroup2").Caption = objCustomFields.GetFieldCaption("lblCustomField7")
        'GetGridColumn(grdAssetsView, "EvaluationGroup3").Caption = objCustomFields.GetFieldCaption("lblCustomField8")
        'GetGridColumn(grdAssetsView, "EvaluationGroup4").Caption = objCustomFields.GetFieldCaption("lblCustomField9")
        grdAssetsView.OptionsView.ColumnAutoWidth = False
        grdAssetsView.BestFitMaxRowCount = 50
        grdAssetsView.BestFitColumns()

        grdAssets.UseEmbeddedNavigator = True
        grdAssets.EmbeddedNavigator.Buttons.Append.Visible = False
        grdAssets.EmbeddedNavigator.Buttons.Edit.Visible = False
        grdAssets.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grdAssets.EmbeddedNavigator.Buttons.Remove.Visible = False
        grdAssets.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        addGridMenu(grdAssets)

        grdAssets.ContextMenuStrip.Items.Add("Change Status To", My.Resources.ListItems16x16)
        AddHandler grdAssets.ContextMenuStrip.Items(3).Click, AddressOf ChangeStatusTO
        grdAssets.ContextMenuStrip.Items.Add("Change Brand To", My.Resources.ListItems16x16)
        AddHandler grdAssets.ContextMenuStrip.Items(4).Click, AddressOf ChangeBrandTo
        grdAssets.ContextMenuStrip.Items.Add("Create Stock Issuance Doc", My.Resources.ListItems16x16)
        AddHandler grdAssets.ContextMenuStrip.Items(5).Click, AddressOf CreateStockIssuanceDoc
        '

    End Sub

    Private Sub TrvLocations_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TrvLocations.AfterSelect
        Try
            Dim selNode As TreeNode
            selNode = TrvLocations.SelectedNode
            If Not selNode Is Nothing Then
                If chkShowSubLocationAssets.Checked Then
                    grdAssetsView.ActiveFilterString = GetGridColumnName("LocID") & " like'" & selNode.Tag & "%'"
                Else
                    grdAssetsView.ActiveFilterString = GetGridColumnName("LocID") & " ='" & selNode.Tag & "'"
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Function GetLoc_ById(ByVal _id As String) As String
        Try
            Dim ds As DataTable
            Dim rcount As Integer
            objattLocation = New attLocation
            objattLocation.Description = _id
            ds = objBALLocation.GetComboLocations(objattLocation)
            If ds Is Nothing = False Then
                rcount = ds.Rows.Count
                If rcount > 0 Then
                    For Each dr As DataRow In ds.Rows
                        Return dr(0).ToString()
                    Next
                End If
            End If
            Return ""
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
        Return Nothing
    End Function

    Private Function Get_AssetsDetails_byID(ByVal _id As String) As DataTable

        Try
            Dim ds As New DataTable

            objattAssetDetails = New attAssetDetails
            objattAssetDetails.PKeyCode = _id
            ds = objBALAssetDetails.GetAll_AssetDetails(objattAssetDetails)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Return ds
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
        Return Nothing
    End Function

#End Region


    Private Sub tbBrowseCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabControl.SelectedIndexChanged
        Try
            If tabControl.SelectedTab Is tabPageLocations Then
                grdAssets.Visible = True
                grdAssetsView.ClearSelection()
                grdAssetsView.ClearColumnsFilter()
                btnAssetsBarcode.Enabled = True
                btnLocationBarcode.Enabled = True
            ElseIf tabControl.SelectedTab Is tabPageAssetCategory Then
                grdAssets.Visible = True
                grdAssetsView.ClearSelection()
                grdAssetsView.ClearColumnsFilter()
                btnAssetsBarcode.Enabled = True
                btnLocationBarcode.Enabled = False
            ElseIf tabControl.SelectedTab Is tabPageCustodians Then
                grdAssets.Visible = True
                grdAssetsView.ClearSelection()
                grdAssetsView.ClearColumnsFilter()
                grdCustView.Focus()
                grdCustView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
                grdCustView.FocusedColumn = grdCustView.Columns(0)
                grdCustView.ShowEditor()
                btnAssetsBarcode.Enabled = True
                btnLocationBarcode.Enabled = False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub Print_Asset()
        Try
            If grdAssetsView.SelectedRowsCount <= 0 Then
                Exit Sub
            End If

            Dim rpt As New XtraReport
            rpt = LoadReport("Assets Barcode")
            If MessageBox.Show("You are going to print " & grdAssetsView.SelectedRowsCount.ToString & " Asset Labels , Do you want to Continue ? ", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                Dim AssetIDs As String = ""
                'Get the Asset IDs from the Selected Rows in the grid.
                For Each i As Integer In grdAssetsView.GetSelectedRows
                    AssetIDs += String.Format("'{0}',", GetGridRowCellValue(grdAssetsView, i, "AstID"))
                Next
                AssetIDs = AssetIDs.Remove(AssetIDs.Length - 1, 1)
                'Get the data for the selected Rows from the database.
                Dim dt As DataTable = objBALAssetDetails.GetAssetLabelData(AssetIDs)
                rpt.DataSource = dt

                'Print the report 
                If chkShowPreview.Checked Then
                    rpt.ShowPreview()
                Else
                    rpt.Print()
                    For Each i As Integer In grdAssetsView.GetSelectedRows
                        Dim astID As String = GetGridRowCellValue(grdAssetsView, i, "AstID")
                        objBALAssetDetails = New BALAssetDetails
                        If objBALAssetDetails.Update_LabelCount(astID) Then
                            'If GetGridRowCellValue(grdAssetsView, i, "LabelCount").ToString = "" Then
                            '    SetGridRowCellValue(grdAssetsView, i, "LabelCount", 1)
                            'Else
                            '    SetGridRowCellValue(grdAssetsView, i, "LabelCount", CInt(GetGridRowCellValue(grdAssetsView, i, "LabelCount")) + 1)
                            'End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally

        End Try
    End Sub

    Private Sub btnAssetsBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssetsBarcode.Click
        Try
            Print_Asset()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnLocationBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocationBarcode.Click
        Print_Location()
    End Sub

    Private Sub Print_Location()
        Try
            Dim strComName As String = ""
            Dim objCompanyInfo As New BALCompanyInfo
            Dim dsCinfo As DataTable = objCompanyInfo.GetAll_CompanyInfo(New attCompanyInfo)
            If dsCinfo Is Nothing = False Then
                If dsCinfo.Rows.Count > 0 Then
                    strComName = dsCinfo.Rows(0)("Name")
                End If
            End If

            Dim b As Boolean
            Try
                If Not TrvLocations.SelectedNode Is Nothing Then
                    If TrvLocations.SelectedNode.Text <> "" Then
                        If MessageBox.Show("Do you want to print the Labels for related Locations", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                            b = True
                        Else
                            b = False
                        End If

                        Loc_childCount(TrvLocations.SelectedNode, b)
                        If MessageBox.Show("You are going to print " & Loc_BCodeCount & " Location Labels, Do you want to Continue?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                            Create_LocBCode(TrvLocations.SelectedNode, b, strComName)
                        End If
                        Loc_BCodeCount = 0
                        Exit Sub
                    Else
                        ShowErrorMessage("Please select Location")
                    End If
                Else
                    ShowErrorMessage("Please select Location")
                End If
                Exit Sub
            Catch ex As IOException
                GenericExceptionHandler(ex, WhoCalledMe)
            Catch ex As Exception
            Finally

            End Try
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub Loc_childCount(ByVal SelNode As TreeNode, ByVal printChild As Boolean)
        Try
            Dim CName() As String = SelNode.FullPath.Split("\")
            Dim textcnt As Integer = SelNode.FullPath.IndexOf("\")
            Loc_BCodeCount = Loc_BCodeCount + 1
            If printChild Then
                Dim aNode As TreeNode
                For Each aNode In SelNode.Nodes
                    Loc_childCount(aNode, True)
                Next
            Else
                Exit Sub
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
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

    Private Sub Create_LocBCode(ByVal SelNode As TreeNode, ByVal printChild As Boolean, ByVal strComName As String)
        Try

            Dim CName() As String = SelNode.FullPath.Split("\")
            Dim textcnt As Integer = SelNode.FullPath.IndexOf("\")

            Dim rpt As New XtraReport
            rpt = LoadReport("Location Barcode")
            'If textcnt >= 0 Then
            rpt.FindControl("lblHeading1", False).Text = strComName
            rpt.FindControl("barCode", False).Text = "LOC" & SelNode.Tag
            rpt.FindControl("lbltext", False).Text = Trim(SelNode.FullPath.Substring(textcnt + 1))

            rpt.Print()

            If printChild Then
                Dim aNode As TreeNode
                For Each aNode In SelNode.Nodes
                    Create_LocBCode(aNode, True, strComName)
                Next
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub


    Private Sub TrvLocations_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TrvLocations.MouseUp
        If e.Button = MouseButtons.Right Then
            Dim n As TreeNode = Me.TrvLocations.GetNodeAt(e.X, e.Y)
            If Not n Is Nothing Then
                Me.TrvLocations.SelectedNode = n
            End If
        End If
    End Sub

    Private Sub grdCustView_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdCustView.FocusedRowChanged
        If grdCustView.SelectedRowsCount > 0 And tabControl.SelectedIndex > 0 Then
            'Dim custName As String = GetGridRowCellValue(grdCustView, e.FocusedRowHandle, "CustodianName")
            Dim custName As String = GetGridRowCellValue(grdCustView, e.FocusedRowHandle, "Name")

            'SetGridRowCellValue(grdAssetsView,DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "CustodianName", custName)
            grdAssetsView.ActiveFilterString = GetGridColumnName("CustodianName") & " ='" & custName & "'"
        End If
    End Sub


    Private Sub Get_Cust()
        Try
            Dim ds As New DataTable
            objattCustodian = New attCustodian
            ds = objBALCustodian.GetAllData_GetCombo()
            grdCust.DataSource = ds
            format_Custgrd()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Dim downHitInfo As GridHitInfo = Nothing
    Private Sub grdAssetsView_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdAssetsView.MouseDown
        Dim view As GridView = CType(sender, GridView)
        downHitInfo = Nothing
        Dim hitInfo As GridHitInfo = view.CalcHitInfo(New Point(e.X, e.Y))
        If Not Control.ModifierKeys = Keys.None Then
            Exit Sub
        End If
        If e.Button = MouseButtons.Left And hitInfo.RowHandle >= 0 Then
            downHitInfo = hitInfo
        End If
    End Sub

    Private Sub grdAssetsView_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAssetsView.DoubleClick
        Try
            'disable double click on the footer and other parts.
            Dim GHI As New GridHitInfo()
            GHI = grdAssetsView.CalcHitInfo(CType(e, DevExpress.Utils.DXMouseEventArgs).Location)
            If GHI.InRow Then
                If grdAssetsView.SelectedRowsCount > 0 Then
                    Dim intRow As Integer = grdAssetsView.GetSelectedRows(0)
                    If intRow >= 0 Then
                        Dim frm As New frmAssetsDetails
                        frm.MdiParent = Me.MdiParent
                        frm.WindowState = FormWindowState.Maximized
                        frm.IsDemokey = IsDemokey
                        frm.RoleID = RoleID
                        frm.Show()
                        frm.BringToFront()
                        frm.LocateAsset(GetGridRowCellValue(grdAssetsView, intRow, "AstID"))
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub grdAssetsView_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdAssetsView.MouseMove
        Dim view As GridView = CType(sender, GridView)
        If e.Button = MouseButtons.Left And Not downHitInfo Is Nothing Then
            Dim dragSize As Size = SystemInformation.DragSize
            Dim DragRect As Rectangle = New Rectangle(New Point(downHitInfo.HitPoint.X - dragSize.Width / 2, _
                downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize)
            If Not DragRect.Contains(New Point(e.X, e.Y)) Then
                Dim row As DataRow = view.GetDataRow(downHitInfo.RowHandle)
                view.GridControl.DoDragDrop(row, DragDropEffects.Move)
                downHitInfo = Nothing
                DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = True
            End If
        End If
    End Sub

    Private Sub TrvLocations_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TrvLocations.DragOver
        Try
            Dim TNode As TreeNode 'Target Node 
            Dim pt As Point = TrvLocations.PointToClient(New Point(e.X, e.Y))
            TNode = TrvLocations.GetNodeAt(pt)
            If e.Data.GetDataPresent(GetType(DataRow)) Then
                e.Effect = DragDropEffects.Move
            Else
                e.Effect = DragDropEffects.None
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub grdCust_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles grdCust.DragOver
        If e.Data.GetDataPresent(GetType(DataRow)) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub TrvLocations_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TrvLocations.DragDrop
        If grdAssetsView.SelectedRowsCount < 1 Then
            Exit Sub
        End If

        Try
            Dim pt As Point = TrvLocations.PointToClient(New Point(e.X, e.Y))
            SelNode = TrvLocations.GetNodeAt(pt)

            If Not SelNode Is Nothing Then
                'Check the system parameters and disallow drop the Assets based on AssetLocationMinLevel Parameters.
                If SelNode.Level + 1 < SysParam.AssetlocationMinlevel Then
                    ShowErrorMessage("Assets can't be transferred to this level, because 'Asset Location Min Level' Parameter is bigger.")
                    Exit Sub
                End If

                Dim NewLocID As String = SelNode.Tag
                Dim NewLocName As String = SelNode.Text
                Dim NewLocFullName As String = objBALLocation.GetLocFullPath(NewLocID)
                'Dim OldLocID As String = GetGridRowCellValue( grdAssetsView,DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "LocID")
                pb.Visible = True
                pb.Value = 0
                pb.Step = 1
                pb.Maximum = grdAssetsView.SelectedRowsCount

                grdAssetsView.ClearColumnsFilter()
                For Each index As Integer In grdAssetsView.GetSelectedRows()
                    If GetGridRowCellValue(grdAssetsView, index, "AstID").ToString() <> "" Then
                        objattAstHistory = New attAstHistory()
                        objattAstHistory.AstID = GetGridRowCellValue(grdAssetsView, index, "AstID")
                        objattAstHistory.Status = 3
                        objattAstHistory.InvSchCode = 1
                        objattAstHistory.HisDate = DateTime.Now.Date
                        objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                        objattAstHistory.Fr_loc = GetGridRowCellValue(grdAssetsView, index, "LocID")
                        objattAstHistory.To_Loc = NewLocID
                        objBALAst_History.Insert_Ast_History(objattAstHistory)
                        objattAssetDetails = New attAssetDetails
                        objattAssetDetails.PKeyCode = GetGridRowCellValue(grdAssetsView, index, "AstID")
                        objattAssetDetails.LocID = NewLocID
                        objBALAssetDetails.Update_Location(objattAssetDetails)
                        'update the grid values after drag drop is made.
                        SetGridRowCellValue(grdAssetsView, index, "LocID", NewLocID)
                        SetGridRowCellValue(grdAssetsView, index, "LocDesc", NewLocName)
                        SetGridRowCellValue(grdAssetsView, index, "LocationFullPath", NewLocFullName)
                    End If
                    pb.PerformStep()
                    Application.DoEvents()
                Next
                If TrvLocations.SelectedNode IsNot Nothing Then
                    Dim SelectedLocationID As String = TrvLocations.SelectedNode.Tag
                    'SetGridRowCellValue(grdAssetsView,DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "LocID", SelectedLocationID)
                    grdAssetsView.ActiveFilterString = GetGridColumnName("LocID") & " ='" & SelectedLocationID & "'"
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally
            pb.Visible = False
        End Try
    End Sub

    Private Sub grdCust_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles grdCust.DragDrop
        If grdAssetsView.SelectedRowsCount < 1 Then
            Exit Sub
        End If

        Dim chi As New GridHitInfo()
        Dim pt As Point = grdCust.PointToClient(New Point(e.X, e.Y))
        chi = grdCustView.CalcHitInfo(New System.Drawing.Point(pt.X, pt.Y))
        If chi.InRow Then
            'Dim newcustID As String = GetGridRowCellValue(grdCustView, chi.RowHandle, "CustodianID")
            'Dim newcustName As String = GetGridRowCellValue(grdCustView, chi.RowHandle, "CustodianName")
            Dim newcustID As String = GetGridRowCellValue(grdCustView, chi.RowHandle, "ID")
            Dim newcustName As String = GetGridRowCellValue(grdCustView, chi.RowHandle, "Name")
            Try
                Dim objattAst_Cust_history As attAst_Cust_history
                Dim objBALAst_Cust_history As New BALAst_Cust_history
                pb.Visible = True
                pb.Value = 1
                pb.Step = 1
                pb.Maximum = grdAssetsView.SelectedRowsCount

                grdAssetsView.ClearColumnsFilter()

                For Each i As Integer In grdAssetsView.GetSelectedRows()
                    If GetGridRowCellValue(grdAssetsView, i, "itemcode").ToString <> "" Then '1 = "ItmCode"
                        objattAst_Cust_history = New attAst_Cust_history()
                        objattAst_Cust_history.AstID = GetGridRowCellValue(grdAssetsView, i, "AstID").ToString
                        objattAst_Cust_history.HisDate = DateTime.Now.Date
                        objattAst_Cust_history.PKeyCode = objBALAst_Cust_history.GetNextPKey_AstHistory()
                        objattAst_Cust_history.Fr_Cust = GetGridRowCellValue(grdAssetsView, i, "CustodianID").ToString
                        objattAst_Cust_history.To_Cust = newcustID
                        objBALAst_Cust_history.Insert_Ast_Cust_history(objattAst_Cust_history)
                        objattAssetDetails = New attAssetDetails
                        objattAssetDetails.PKeyCode = GetGridRowCellValue(grdAssetsView, i, "AstID").ToString
                        objattAssetDetails.CustodianID = newcustID
                        objBALAssetDetails.Update_Custodian(objattAssetDetails)

                        'update the grid values after drag drop is made.
                        SetGridRowCellValue(grdAssetsView, i, "CustodianID", newcustID)
                        SetGridRowCellValue(grdAssetsView, i, "CustodianName", newcustName)
                    End If
                    pb.PerformStep()
                    Application.DoEvents()
                Next
                'SetGridRowCellValue(grdAssetsView, DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "CustodianName", GetGridRowCellValue(grdCustView, grdCustView.GetSelectedRows(0), "CustodianName"))
                SetGridRowCellValue(grdAssetsView, DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "CustodianName", GetGridRowCellValue(grdCustView, grdCustView.GetSelectedRows(0), "Name"))

            Catch ex As Exception
                GenericExceptionHandler(ex, WhoCalledMe)
            Finally
                pb.Visible = False
            End Try

        End If
    End Sub


    Private Sub trvAssetCat_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvAssetCat.AfterSelect
        Try
            Dim selNode As TreeNode
            selNode = trvAssetCat.SelectedNode
            If Not selNode Is Nothing Then
                grdAssetsView.ActiveFilterString = GetGridColumnName("AstCatID") & " ='" & selNode.Tag & "'"
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub


    Private Sub PrintableComponentLink2_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs) Handles PrintableComponentLink2.CreateReportHeaderArea
        Dim reportHeader As String = "Company Name: SABB"
        e.Graph.StringFormat = New BrickStringFormat(StringAlignment.Near)
        e.Graph.Font = New Font("Tahoma", 14, FontStyle.Bold)
        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50)
        e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None)

        Dim recImage As RectangleF = New RectangleF(900, 1, 133, 133)
        e.Graph.DrawImage(CompanyLogoImage, recImage)
    End Sub

    Public Sub addGridMenu(ByVal grd As DevExpress.XtraGrid.GridControl)
        Dim CntMnu As New ContextMenuStrip
        CntMnu.Items.Add("Export to Excel", My.Resources.ExportToExcel16x16)
        AddHandler CntMnu.Items(0).Click, AddressOf GridExportToExcel
        CntMnu.Items.Add("Print Preview", My.Resources.Preview16x16)
        AddHandler CntMnu.Items(1).Click, AddressOf GridPrint
        CntMnu.Items.Add("Print Preview with Barcode", My.Resources.Preview16x16)
        AddHandler CntMnu.Items(2).Click, AddressOf GridPrintwithBarcode

      
        grd.ContextMenuStrip = CntMnu
        CntMnu.Tag = grd
    End Sub

    Public Sub GridExportToExcel(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim grd As DevExpress.XtraGrid.GridControl = CType(sender, ToolStripMenuItem).Owner.Tag
        Dim savedlg As New SaveFileDialog
        savedlg.DefaultExt = "xls"
        savedlg.Filter = "*.xls|*.xls"

        If savedlg.ShowDialog() = DialogResult.OK Then
            grd.ExportToXls(savedlg.FileName)
            System.Diagnostics.Process.Start(savedlg.FileName)
        End If
    End Sub

    Public Sub GridPrint(ByVal sender As System.Object, ByVal e As System.EventArgs)
        PrintableComponentLink2.CreateDocument()
        PrintableComponentLink2.ShowPreview()
    End Sub


    Public Sub GridPrintwithBarcode(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim rpt As New rptAssetAdmin
        LoadAssetWithBarcodeReport(rpt)
        rpt.ShowPreviewDialog()
    End Sub

    Private Sub LoadAssetWithBarcodeReport(ByVal rpt As rptAssetAdmin)
        rpt.lblCompName.Text = AppConfig.CompanyName
        rpt.PrinterName = AppConfig.ReportPrinter
        rpt.PrintingSystem.ShowMarginsWarning = False
        rpt.FindControl("lblCompName", False).Text = AppConfig.CompanyName
        CType(rpt.FindControl("picLogo", False), DevExpress.XtraReports.UI.XRPictureBox).Image = CompanyLogoImage


        'Dim view2 As DataView
        Dim FilteredTable As DataTable = CType(grdAssets.DataSource, DataTable).Clone()
        'view2 = New DataView(table)
        For i As Integer = 0 To grdAssetsView.RowCount - 1
            If (Not grdAssetsView.IsGroupRow(i) And grdAssetsView.GetDataRow(i) IsNot Nothing) Then
                FilteredTable.Rows.Add(grdAssetsView.GetDataRow(i).ItemArray)
            End If
        Next
        rpt.DataSource = FilteredTable
    End Sub



    Public Sub ChangeStatusTO(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New frmChangeValues
        frm.Caption = "Change Status To"
        frm.cmbAssetsStatus.Visible = True
        frm.lblStatus.Visible = True

        frm.cmbBrand.Visible = False
        frm.lblBrand.Visible = False

        frm.cmbCustodian.Visible = False
        frm.lblCustodian.Visible = False

        frm.cmbLocation.Visible = False
        frm.lblLocation.Visible = False


        frm.ShowDialog()
        Dim SelectedRecordID As String = frm.SelectedRecordID
        Dim SelectedRecordText As String = frm.SelectedRecordText


        If Not String.IsNullOrEmpty(SelectedRecordID) Then
            Try
                pb.Visible = True
                pb.Value = 0
                pb.Step = 1
                pb.Maximum = grdAssetsView.SelectedRowsCount
                grdAssetsView.ClearColumnsFilter()
                For Each index As Integer In grdAssetsView.GetSelectedRows()
                    If GetGridRowCellValue(grdAssetsView, index, "AstID").ToString() <> "" Then
                        objattAssetDetails = New attAssetDetails
                        objattAssetDetails.PKeyCode = GetGridRowCellValue(grdAssetsView, index, "AstID")
                        objattAssetDetails.StatusID = SelectedRecordID
                        objBALAssetDetails.Update_Status(objattAssetDetails)
                        'update the grid values after drag drop is made.
                        objBALAssetDetails.GetAssetStatus(False, True)
                        SetGridRowCellValue(grdAssetsView, index, "Status", SelectedRecordText)
                    End If
                    pb.PerformStep()
                    Application.DoEvents()
                Next
            Finally
                pb.Visible = False
            End Try
        End If
    End Sub
    Public Sub CreateStockIssuanceDoc(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New frmChangeValues
        frm.Caption = "Select custodian from below"
        frm.cmbAssetsStatus.Visible = False
        frm.lblStatus.Visible = False
        frm.cmbBrand.Visible = False
        frm.lblBrand.Visible = False

        frm.cmbCustodian.Visible = True
        frm.lblCustodian.Visible = True

        frm.cmbLocation.Visible = True
        frm.lblLocation.Visible = True



        frm.ShowDialog()
        Dim SelectedRecordID As String = frm.SelectedRecordID
        Dim SelectedRecordText As String = frm.SelectedRecordText
        If Not String.IsNullOrEmpty(SelectedRecordID) Then
            Dim ctlTrans As New ctlStockTransfer
            ctlTrans.Tag = "SI"
            ShowTransForm(ctlTrans)
            ctlTrans.cmbFromLocation.EditValue = SelectedRecordID
            ctlTrans.cmbToLocation.EditValue = frm.SelectedLocation

            ctlTrans.btnSave.PerformClick()
            Try
                pb.Visible = True
                pb.Value = 0
                pb.Step = 1
                pb.Maximum = grdAssetsView.SelectedRowsCount
                grdAssetsView.ClearColumnsFilter()
                For Each index As Integer In grdAssetsView.GetSelectedRows()
                    Dim obj As Object = GetGridRowCellValue(grdAssetsView, index, "Serial Number").ToString()
                    If obj <> "" Then
                        ctlTrans.grvItems.AddNewRow()
                        Dim FocRow As Integer = ctlTrans.grvItems.FocusedRowHandle
                        If ctlTrans.LoadGridInfoBySerial(obj, FocRow, False) Then
                            ctlTrans.SaveGridItemInfo(FocRow)
                            ctlTrans.grvItems.PostEditor()
                        End If
                        'objattAssetDetails = New attAssetDetails
                        'objattAssetDetails.PKeyCode = GetGridRowCellValue(grdAssetsView, index, "AstID")
                        'objattAssetDetails.AstBrandID = SelectedRecordID
                        'objBALAssetDetails.Update_Brand(objattAssetDetails)
                        'SetGridRowCellValue(grdAssetsView, index, "AstBrandName", SelectedRecordText)

                    End If
                    pb.PerformStep()
                    Application.DoEvents()
                Next
            Finally
                pb.Visible = False
            End Try
        End If

    End Sub

    Private Sub ShowTransForm(ByVal ctlStock As ctlStockTransfer)
        Dim popup As New frmPopup
        Try
            popup.Controls.Add(ctlStock)
            popup.Size = ctlStock.Size
            popup.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
            popup.MdiParent = MdiParent
            popup.WindowState = FormWindowState.Normal
            popup.StartPosition = FormStartPosition.CenterScreen
            ctlStock.Dock = DockStyle.Fill
            popup.WindowState = FormWindowState.Normal
            popup.Show()
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, "")
        End Try
    End Sub

    Public Sub ChangeBrandTo(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New frmChangeValues
        frm.Caption = "Change Brand To"
        frm.cmbAssetsStatus.Visible = False
        frm.lblStatus.Visible = False
        frm.cmbBrand.Visible = True
        frm.lblBrand.Visible = True

        frm.cmbCustodian.Visible = False
        frm.lblCustodian.Visible = False

        frm.cmbLocation.Visible = False
        frm.lblLocation.Visible = False

        frm.ShowDialog()
        Dim SelectedRecordID As String = frm.SelectedRecordID
        Dim SelectedRecordText As String = frm.SelectedRecordText

        If Not String.IsNullOrEmpty(SelectedRecordID) Then
            Try
                pb.Visible = True
                pb.Value = 0
                pb.Step = 1
                pb.Maximum = grdAssetsView.SelectedRowsCount
                grdAssetsView.ClearColumnsFilter()
                For Each index As Integer In grdAssetsView.GetSelectedRows()
                    If GetGridRowCellValue(grdAssetsView, index, "AstID").ToString() <> "" Then
                        objattAssetDetails = New attAssetDetails
                        objattAssetDetails.PKeyCode = GetGridRowCellValue(grdAssetsView, index, "AstID")
                        objattAssetDetails.AstBrandID = SelectedRecordID
                        objBALAssetDetails.Update_Brand(objattAssetDetails)
                        SetGridRowCellValue(grdAssetsView, index, "AstBrandName", SelectedRecordText)
                    End If
                    pb.PerformStep()
                    Application.DoEvents()
                Next
            Finally
                pb.Visible = False
            End Try
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        GetCompleteFormData()
    End Sub
End Class
