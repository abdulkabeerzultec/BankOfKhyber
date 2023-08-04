Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO
Imports System.Drawing.Printing
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports

Public Class frmDashboard

#Region "Declerations"
    Dim objattCustodian As attCustodian
    Dim objBALCustodian As New BALCustodian

    Dim objattAssets As attItems
    Dim objBALAssets As New BALItems
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
    Dim dsAssets As New DataTable

#End Region
    Private Sub frmDashboard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        Try

            'greating temp datatable to assign it to assetDetails grid.
            Dim dt As New DataTable
            dt.Columns.Add("AssetDetail", Type.GetType("System.String"))
            dt.Columns.Add("Description", Type.GetType("System.String"))
            grdAssetDetails.DataSource = dt
            FormatAssetDetails()

            FillCategoryTree(trvAssetCat)
            FillCategoryTree(trvItemCat)
            trvItemCat.CollapseAll()
            FillLocationTree(TrvLocations)

            Get_Cust()
            Get_AssetsItems()

            Select Case IntegrationName
                Case IntegrationType.CMAIntegration
                    Get_AssetsDetails()
                    format_grdAssetsCMAIntegration()
                Case IntegrationType.ABBIntegration
                    Get_AssetsDetailsABB()
                    tabControl.TabPages.Remove(tabPageItemCategory)
                    format_grdAssetsABBIntegration()
                    tabPageAssetCategory.Text = "Asset By Class"
                    grdAstItems.Visible = False
                    grdAssetDetails.Visible = False
                    grdAssets.Dock = DockStyle.Fill
                    'grdAssets.Height += grdAssetDetails.Height
                Case IntegrationType.FairMontIntegration
                    Get_AssetsDetails()
                    format_grdAssets()
                    FormatGridFairMont()
                Case Else
                    Get_AssetsDetails()
                    format_grdAssets()
            End Select

            grdAssets.Visible = True
            grdAstItems.Visible = False
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub FormatGridFairMont()
        Dim objCustomFields As New BALCustomFields

        grdAssetsView.OptionsView.ColumnAutoWidth = False
        GetGridColumn(grdAssetsView, "CostName").Visible = False

        GetGridColumn(grdAssetsView, "AstModel").Visible = True

        GetGridColumn(grdAssetsView, "EvaluationGroup1").Visible = True
        GetGridColumn(grdAssetsView, "EvaluationGroup2").Visible = True
        GetGridColumn(grdAssetsView, "EvaluationGroup3").Visible = True
        GetGridColumn(grdAssetsView, "EvaluationGroup4").Visible = True

        GetGridColumn(grdAssetsView, "EvaluationGroup1").Caption = objCustomFields.GetFieldCaption("lblCustomField6")
        GetGridColumn(grdAssetsView, "EvaluationGroup2").Caption = objCustomFields.GetFieldCaption("lblCustomField7")
        GetGridColumn(grdAssetsView, "EvaluationGroup3").Caption = objCustomFields.GetFieldCaption("lblCustomField8")
        GetGridColumn(grdAssetsView, "EvaluationGroup4").Caption = objCustomFields.GetFieldCaption("lblCustomField9")

        GetGridColumn(grdAssetsView, "CustomFld1").Visible = True
        GetGridColumn(grdAssetsView, "CustomFld2").Visible = True
        GetGridColumn(grdAssetsView, "CustomFld3").Visible = True
        GetGridColumn(grdAssetsView, "CustomFld4").Visible = True
        GetGridColumn(grdAssetsView, "CustomFld5").Visible = True

        GetGridColumn(grdAssetsView, "CustomFld1").Caption = objCustomFields.GetFieldCaption("lblCustomField1")
        GetGridColumn(grdAssetsView, "CustomFld2").Caption = objCustomFields.GetFieldCaption("lblCustomField2")
        GetGridColumn(grdAssetsView, "CustomFld3").Caption = objCustomFields.GetFieldCaption("lblCustomField3")
        GetGridColumn(grdAssetsView, "CustomFld4").Caption = objCustomFields.GetFieldCaption("lblCustomField4")
        GetGridColumn(grdAssetsView, "CustomFld5").Caption = objCustomFields.GetFieldCaption("lblCustomField5")

        GetGridColumn(grdAssetsView, "CustodianID").Visible = True

        GetGridColumn(grdAssetsView, "SerailNo").Visible = True
    End Sub

    Private Sub Get_AssetsDetailsABB()
        Try
            Application.DoEvents()
            Dim obj As New BALABBIntegration
            dsAssetsDetails = obj.GetAssetAdminGridABB(New attAssetDetails)
            For Each row As DataRow In dsAssetsDetails.Rows
                Dim FulLoc As String = row("LocationFullPath").ToString
                Dim strFulLoc As String() = FulLoc.Split("\")
                If strFulLoc(0) = "UnKnown" Then
                    row("Plant") = String.Empty
                Else
                    row("Plant") = strFulLoc(0).Trim
                End If

                If strFulLoc.Length > 1 Then
                    If strFulLoc(1) = "UnKnown" Then
                        row("Location") = String.Empty
                    Else
                        row("Location") = strFulLoc(1).Trim
                    End If
                Else
                    row("Location") = String.Empty
                End If
                row.AcceptChanges()
            Next

            grdAssets.DataSource = dsAssetsDetails
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub Get_AssetsDetails()
        Try
            Application.DoEvents()
            dsAssetsDetails = objBALAssetDetails.GetAsset_Details(New attAssetDetails)
            grdAssets.DataSource = dsAssetsDetails
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub
    Private Sub frmDashboard_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmDashboard = Nothing
    End Sub


#Region "Loading locations"
    Dim ht As New Hashtable
    Public Sub FillLocationTree(ByVal trv As TreeView)
        Try
            Dim ds As New DataTable
            Dim rcount As Integer
            objattLocation = New attLocation
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


    Private Sub format_grdAstItems()
        grdAstItemsView.Columns(0).Caption = "Item Code"
        grdAstItemsView.Columns(0).Width = 80
        grdAstItemsView.Columns(0).Name = "itemcode"
        grdAstItemsView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdAstItemsView.Columns(1).Visible = False
        grdAstItemsView.Columns(2).Caption = "Description"
        grdAstItemsView.Columns(2).Name = "AstDesc"
        grdAstItemsView.Columns(2).Width = 150

        grdAstItemsView.Columns(3).Caption = "Model"
        grdAstItemsView.Columns(3).Name = "AstModel"
        grdAstItemsView.Columns(3).Visible = False

        grdAstItemsView.Columns(4).Caption = "Quantity"
        grdAstItemsView.Columns(4).Name = "AstQty"
        grdAstItemsView.Columns(4).Visible = False

        grdAstItemsView.Columns(5).Caption = "Brand"
        grdAstItemsView.Columns(5).Visible = False
        grdAstItemsView.Columns(6).Caption = "Category"
        grdAstItemsView.Columns(6).Name = "AstCatDesc"
        grdAstItemsView.Columns(6).Width = 150
        grdAstItemsView.Columns(7).Caption = "AstCatID"
        grdAstItemsView.Columns(7).Name = "AstCatID"
        grdAstItemsView.Columns(7).Visible = False
        grdAstItemsView.Columns(8).Visible = False


        grdAstItems.UseEmbeddedNavigator = True
        grdAstItems.EmbeddedNavigator.Buttons.Append.Visible = False
        grdAstItems.EmbeddedNavigator.Buttons.Edit.Visible = False
        grdAstItems.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grdAstItems.EmbeddedNavigator.Buttons.Remove.Visible = False
        grdAstItems.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        addGridMenu(grdAstItems)
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

    Private Sub FormatAssets()
        CType(grdAssetDetails.DataSource, DataTable).Rows.Clear()
        With grdAssetDetailsView
            .OptionsBehavior.Editable = True
            .Columns(0).Caption = "Asset Items Detail"
            .Columns(0).Width = 120
            .Columns(0).AppearanceCell.Font = New Font(grdAssetDetails.Font, FontStyle.Bold)
            .Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(0).OptionsColumn.AllowEdit = False
            .Columns(1).Caption = "Description"
            .Columns(1).Width = 380
            .Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(1).OptionsColumn.AllowEdit = True
            .Columns(1).OptionsColumn.ReadOnly = True
            Dim idx As Integer = 0
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Item Code")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Brand")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Category")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Description")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Model")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Quantity")
            .UpdateCurrentRow()
            .FocusedRowHandle = 0
        End With
    End Sub
    Private Sub FormatAssetDetails()
        CType(grdAssetDetails.DataSource, DataTable).Rows.Clear()
        With grdAssetDetailsView

            .OptionsBehavior.Editable = True
            .Columns(0).Caption = "Asset Detail"
            .Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(0).Width = 120
            .Columns(0).AppearanceCell.Font = New Font(grdAssetDetails.Font, FontStyle.Bold)
            .Columns(0).OptionsColumn.AllowEdit = False
            .Columns(1).Caption = "Description"
            .Columns(1).Width = 380
            .Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(1).OptionsColumn.AllowEdit = True
            .Columns(1).OptionsColumn.ReadOnly = True
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Asset ID")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Asset #")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Oracle Reference #")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Description")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Brand")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Custodian")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Location")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Date Purchased")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Total Cost")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Item Cost")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Add Cost")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Model")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Supplier Name")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Purchase Order")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Service Date")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Invoice")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Insurer Name")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Disposed")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Disposal Method")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Disposal Date")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Sale Date")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Sale Price")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Sold To")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Discount")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "BarCode")
            .AddNewRow()
            .SetRowCellValue(.FocusedRowHandle, "AssetDetail", "Serial #")
            .UpdateCurrentRow()
            .FocusedRowHandle = 0
        End With
        addGridMenu(grdAssetDetails)
    End Sub
    Private Sub format_grdAssetsCMAIntegration()
        grdAssetsView.Columns(0).Caption = "Asset #"
        grdAssetsView.Columns(0).Name = "AstNum"
        grdAssetsView.Columns(0).Width = 90
        grdAssetsView.Columns(0).Visible = False
        grdAssetsView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdAssetsView.Columns(1).Caption = "Item Code"
        grdAssetsView.Columns(1).Visible = False
        grdAssetsView.Columns(1).Name = "itemcode"
        grdAssetsView.Columns(2).Caption = "Description"
        grdAssetsView.Columns(2).Name = "AstDesc"
        grdAssetsView.Columns(2).Width = 100
        grdAssetsView.Columns(3).Visible = True
        grdAssetsView.Columns(3).Name = "AstID"
        grdAssetsView.Columns(3).Caption = "Asset Tag"
        grdAssetsView.Columns(3).VisibleIndex = 0
        grdAssetsView.Columns(3).MaxWidth = 120
        grdAssetsView.Columns(3).MinWidth = 90

        grdAssetsView.Columns(4).Caption = "Location"
        grdAssetsView.Columns(4).Width = 100
        grdAssetsView.Columns(4).Name = "LocDesc"
        grdAssetsView.Columns(4).Visible = False

        grdAssetsView.Columns(5).Caption = "Custodian"
        grdAssetsView.Columns(5).Width = 100
        grdAssetsView.Columns(5).Name = "CustodianName"
        grdAssetsView.Columns(5).Visible = False

        grdAssetsView.Columns(6).Visible = False
        grdAssetsView.Columns(6).Name = "LocID"
        grdAssetsView.Columns(7).Visible = False
        grdAssetsView.Columns(7).Name = "AstCatID"

        grdAssetsView.Columns(8).Visible = False
        grdAssetsView.Columns(8).Name = "CustodianID"

        grdAssetsView.Columns(9).Caption = "Brand"
        grdAssetsView.Columns(9).Name = "AstBrandName"
        grdAssetsView.Columns(9).Width = 75
        grdAssetsView.Columns(9).Visible = False
        grdAssetsView.Columns(10).Caption = "Company"
        grdAssetsView.Columns(10).Width = 100
        grdAssetsView.Columns(10).Visible = False
        grdAssetsView.Columns(11).Caption = "BarCode"
        grdAssetsView.Columns(11).Width = 100
        grdAssetsView.Columns(11).Name = "BarCode"
        grdAssetsView.Columns(11).Visible = False

        grdAssetsView.Columns(12).Visible = False
        grdAssetsView.Columns(13).Visible = False
        grdAssetsView.Columns(14).Visible = False
        grdAssetsView.Columns(15).Visible = False
        grdAssetsView.Columns(16).Visible = False
        grdAssetsView.Columns(17).Visible = False
        grdAssetsView.Columns(18).Visible = False
        grdAssetsView.Columns(19).Visible = False
        grdAssetsView.Columns(20).Visible = False
        grdAssetsView.Columns(21).Visible = False
        grdAssetsView.Columns(22).Visible = False
        grdAssetsView.Columns(23).Visible = False
        grdAssetsView.Columns(24).Visible = False
        grdAssetsView.Columns(25).Visible = False
        grdAssetsView.Columns(26).Visible = False
        grdAssetsView.Columns(27).Visible = False
        grdAssetsView.Columns(28).Visible = False
        grdAssetsView.Columns(29).Visible = False
        grdAssetsView.Columns(30).Visible = False
        grdAssetsView.Columns(31).Visible = False
        grdAssetsView.Columns(32).Visible = False
        grdAssetsView.Columns(33).Visible = False
        grdAssetsView.Columns(34).Visible = False
        grdAssetsView.Columns(35).Visible = False
        grdAssetsView.Columns(36).Visible = False
        grdAssetsView.Columns(37).Visible = False
        grdAssetsView.Columns(38).Visible = False
        grdAssetsView.Columns(39).Visible = False
        'GetGridColumn(grdAssetsView,"AstCatDesc").Visible = True
        'GetGridColumn(grdAssetsView,"AstCatDesc").Caption = "Category"
        grdAssetsView.Columns(40).Caption = "Label Count"
        grdAssetsView.Columns(40).Width = 90
        grdAssetsView.Columns(40).MaxWidth = 90
        grdAssetsView.Columns(40).Name = "LabelCount"
        grdAssetsView.Columns(40).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        grdAssetsView.Columns(40).VisibleIndex = grdAssetsView.Columns.Count - 1
        grdAssetsView.Columns(41).Visible = False
        grdAssetsView.Columns(42).Visible = False

        GetGridColumn(grdAssetsView, "CapitalizationDate").Visible = False
        GetGridColumn(grdAssetsView, "BussinessArea").Visible = False
        GetGridColumn(grdAssetsView, "InventoryNumber").Visible = False
        GetGridColumn(grdAssetsView, "CostCenterID").Visible = False
        GetGridColumn(grdAssetsView, "InStockAsset").Visible = False
        GetGridColumn(grdAssetsView, "EvaluationGroup1").Visible = False
        GetGridColumn(grdAssetsView, "EvaluationGroup2").Visible = False
        GetGridColumn(grdAssetsView, "EvaluationGroup3").Visible = False
        GetGridColumn(grdAssetsView, "EvaluationGroup4").Visible = False
        GetGridColumn(grdAssetsView, "CreatedBY").Visible = False
        GetGridColumn(grdAssetsView, "CapitalizationDate").Visible = False

        GetGridColumn(grdAssetsView, "CustomFld1").Visible = False
        GetGridColumn(grdAssetsView, "CustomFld2").Visible = False
        GetGridColumn(grdAssetsView, "CustomFld3").Visible = False
        GetGridColumn(grdAssetsView, "CustomFld4").Visible = False
        GetGridColumn(grdAssetsView, "CustomFld5").Visible = False

        GetGridColumn(grdAssetsView, "CostName").Visible = False
        GetGridColumn(grdAssetsView, "CostNumber").Visible = False
        GetGridColumn(grdAssetsView, "Warranty").Visible = False


        GetGridColumn(grdAssetsView, "CatFullPath").Caption = "Category"
        GetGridColumn(grdAssetsView, "LocationFullPath").Caption = "Location"
        GetGridColumn(grdAssetsView, "SerailNo").Visible = True
        GetGridColumn(grdAssetsView, "SerailNo").MaxWidth = 70
        GetGridColumn(grdAssetsView, "AstModel").Visible = True
        GetGridColumn(grdAssetsView, "AstModel").MaxWidth = 70
        GetGridColumn(grdAssetsView, "TransRemarks").Visible = True
        GetGridColumn(grdAssetsView, "TransRemarks").MaxWidth = 80
        GetGridColumn(grdAssetsView, "TransRemarks").Caption = "Source"

        GetGridColumn(grdAssetsView, "SrvDate").Visible = True
        GetGridColumn(grdAssetsView, "SrvDate").MaxWidth = 70
        GetGridColumn(grdAssetsView, "SrvDate").Caption = "Create Date"

        grdAssets.UseEmbeddedNavigator = True
        grdAssets.EmbeddedNavigator.Buttons.Append.Visible = False
        grdAssets.EmbeddedNavigator.Buttons.Edit.Visible = False
        grdAssets.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grdAssets.EmbeddedNavigator.Buttons.Remove.Visible = False
        grdAssets.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        addGridMenu(grdAssets)
    End Sub

    Private Sub format_grdAssetsABBIntegration()

        GetGridColumn(grdAssetsView, "CustodianName").Caption = "Employee"
        GetGridColumn(grdAssetsView, "Assetdetailsdesc1").Caption = "Description1"
        GetGridColumn(grdAssetsView, "Assetdetailsdesc2").Caption = "Description2"
        GetGridColumn(grdAssetsView, "CatFullPath").Caption = "Class"
        GetGridColumn(grdAssetsView, "LocationFullPath").Caption = "Location"
        GetGridColumn(grdAssetsView, "LabelCount").Caption = "Label Count"
        GetGridColumn(grdAssetsView, "LabelCount").Width = 90
        GetGridColumn(grdAssetsView, "LabelCount").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        GetGridColumn(grdAssetsView, "InvStatus").Visible = False
        GetGridColumn(grdAssetsView, "AstNum").Visible = False
        GetGridColumn(grdAssetsView, "itemcode").Visible = False
        GetGridColumn(grdAssetsView, "CostCenterID").Visible = False
        GetGridColumn(grdAssetsView, "CostName").Visible = False
        GetGridColumn(grdAssetsView, "AstDesc").Visible = False
        GetGridColumn(grdAssetsView, "AstID").Visible = False
        GetGridColumn(grdAssetsView, "LocDesc").Visible = False
        GetGridColumn(grdAssetsView, "LocID").Visible = False
        GetGridColumn(grdAssetsView, "AstCatID").Visible = False
        GetGridColumn(grdAssetsView, "CustodianID").Visible = False
        GetGridColumn(grdAssetsView, "CompanyName").Visible = False
        GetGridColumn(grdAssetsView, "Refno").Visible = False
        GetGridColumn(grdAssetsView, "CompanyID").Visible = False
        GetGridColumn(grdAssetsView, "AstModel").Visible = False
        GetGridColumn(grdAssetsView, "AstCatDesc").Visible = False
        GetGridColumn(grdAssetsView, "TransRemarks").Visible = False
        GetGridColumn(grdAssetsView, "BaseCost").Visible = False
        GetGridColumn(grdAssetsView, "Tax").Visible = False
        GetGridColumn(grdAssetsView, "SrvDate").Visible = False
        GetGridColumn(grdAssetsView, "InsID").Visible = False
        GetGridColumn(grdAssetsView, "InvNumber").Visible = False
        GetGridColumn(grdAssetsView, "POCode").Visible = False
        GetGridColumn(grdAssetsView, "SuppID").Visible = False
        GetGridColumn(grdAssetsView, "Disposed").Visible = False
        GetGridColumn(grdAssetsView, "Discount").Visible = False
        GetGridColumn(grdAssetsView, "RefCode").Visible = False
        GetGridColumn(grdAssetsView, "Plate").Visible = False
        GetGridColumn(grdAssetsView, "Poerp").Visible = False
        GetGridColumn(grdAssetsView, "Capex").Visible = False
        GetGridColumn(grdAssetsView, "Grn").Visible = False
        GetGridColumn(grdAssetsView, "NoPiece").Visible = False
        GetGridColumn(grdAssetsView, "GLCode").Visible = False
        GetGridColumn(grdAssetsView, "PONumber").Visible = False
        GetGridColumn(grdAssetsView, "LocationFullPath").Visible = False

        grdAssetsView.OptionsView.ColumnAutoWidth = False
        grdAssets.UseEmbeddedNavigator = True
        grdAssets.EmbeddedNavigator.Buttons.Append.Visible = False
        grdAssets.EmbeddedNavigator.Buttons.Edit.Visible = False
        grdAssets.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grdAssets.EmbeddedNavigator.Buttons.Remove.Visible = False
        grdAssets.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        addGridMenu(grdAssets)
    End Sub

    Private Sub format_grdAssets()
        grdAssetsView.Columns(0).Caption = "Asset #"
        grdAssetsView.Columns(0).Name = "AstNum"
        grdAssetsView.Columns(0).Width = 90
        grdAssetsView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdAssetsView.Columns(1).Caption = "Item Code"
        grdAssetsView.Columns(1).Visible = False
        grdAssetsView.Columns(1).Name = "itemcode"
        grdAssetsView.Columns(2).Caption = "Description"
        grdAssetsView.Columns(2).Name = "AstDesc"
        grdAssetsView.Columns(2).Width = 100
        grdAssetsView.Columns(3).Visible = False
        grdAssetsView.Columns(3).Name = "AstID"

        grdAssetsView.Columns(4).Caption = "Location"
        grdAssetsView.Columns(4).Width = 100
        grdAssetsView.Columns(4).Name = "LocDesc"
        grdAssetsView.Columns(4).Visible = False

        grdAssetsView.Columns(5).Caption = "Custodian"
        grdAssetsView.Columns(5).Width = 100
        grdAssetsView.Columns(5).Name = "CustodianName"

        grdAssetsView.Columns(6).Visible = False
        grdAssetsView.Columns(6).Name = "LocID"
        grdAssetsView.Columns(7).Visible = False
        grdAssetsView.Columns(7).Name = "AstCatID"

        grdAssetsView.Columns(8).Visible = False
        grdAssetsView.Columns(8).Name = "CustodianID"

        grdAssetsView.Columns(9).Caption = "Brand"
        grdAssetsView.Columns(9).Name = "AstBrandName"
        grdAssetsView.Columns(9).Width = 75
        grdAssetsView.Columns(10).Caption = "Company"
        grdAssetsView.Columns(10).Width = 100
        grdAssetsView.Columns(11).Caption = "BarCode"
        grdAssetsView.Columns(11).Width = 100
        grdAssetsView.Columns(11).Name = "BarCode"

        grdAssetsView.Columns(12).Visible = False
        grdAssetsView.Columns(13).Visible = False
        grdAssetsView.Columns(14).Visible = False
        grdAssetsView.Columns(15).Visible = False
        grdAssetsView.Columns(16).Visible = False
        grdAssetsView.Columns(17).Visible = False
        grdAssetsView.Columns(18).Visible = False
        grdAssetsView.Columns(19).Visible = False
        grdAssetsView.Columns(20).Visible = False
        grdAssetsView.Columns(21).Visible = False
        grdAssetsView.Columns(22).Visible = False
        grdAssetsView.Columns(23).Visible = False
        grdAssetsView.Columns(24).Visible = False
        grdAssetsView.Columns(25).Visible = False
        grdAssetsView.Columns(26).Visible = False
        grdAssetsView.Columns(27).Visible = False
        grdAssetsView.Columns(28).Visible = False
        grdAssetsView.Columns(29).Visible = False
        grdAssetsView.Columns(30).Visible = False
        grdAssetsView.Columns(31).Visible = False
        grdAssetsView.Columns(32).Visible = False
        grdAssetsView.Columns(33).Visible = False
        grdAssetsView.Columns(34).Visible = False
        grdAssetsView.Columns(35).Visible = False
        grdAssetsView.Columns(36).Visible = False
        grdAssetsView.Columns(37).Visible = False
        grdAssetsView.Columns(38).Visible = False
        grdAssetsView.Columns(39).Visible = False

        GetGridColumn(grdAssetsView, "CapitalizationDate").Visible = False
        GetGridColumn(grdAssetsView, "BussinessArea").Visible = False
        GetGridColumn(grdAssetsView, "InventoryNumber").Visible = False
        GetGridColumn(grdAssetsView, "CostCenterID").Visible = False
        GetGridColumn(grdAssetsView, "InStockAsset").Visible = False
        GetGridColumn(grdAssetsView, "EvaluationGroup1").Visible = False
        GetGridColumn(grdAssetsView, "EvaluationGroup2").Visible = False
        GetGridColumn(grdAssetsView, "EvaluationGroup3").Visible = False
        GetGridColumn(grdAssetsView, "EvaluationGroup4").Visible = False
        GetGridColumn(grdAssetsView, "CreatedBY").Visible = False
        GetGridColumn(grdAssetsView, "CapitalizationDate").Visible = False

        GetGridColumn(grdAssetsView, "CustomFld1").Visible = False
        GetGridColumn(grdAssetsView, "CustomFld2").Visible = False
        GetGridColumn(grdAssetsView, "CustomFld3").Visible = False
        GetGridColumn(grdAssetsView, "CustomFld4").Visible = False
        GetGridColumn(grdAssetsView, "CustomFld5").Visible = False
        GetGridColumn(grdAssetsView, "SerailNo").Visible = True
        GetGridColumn(grdAssetsView, "SerailNo").Caption = "Serial#"

        'GetGridColumn(grdAssetsView,"AstCatDesc").Visible = True
        'GetGridColumn(grdAssetsView,"AstCatDesc").Caption = "Category"
        grdAssetsView.Columns(40).Caption = "Label Count"
        grdAssetsView.Columns(40).Width = 90
        grdAssetsView.Columns(40).Name = "LabelCount"
        grdAssetsView.Columns(40).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        grdAssetsView.Columns(40).VisibleIndex = grdAssetsView.Columns.Count - 1
        grdAssetsView.Columns(41).Visible = False
        grdAssetsView.Columns(42).Visible = False

        GetGridColumn(grdAssetsView, "CatFullPath").Caption = "Full Category"
        GetGridColumn(grdAssetsView, "LocationFullPath").Caption = "Full Location"

        grdAssets.UseEmbeddedNavigator = True
        grdAssets.EmbeddedNavigator.Buttons.Append.Visible = False
        grdAssets.EmbeddedNavigator.Buttons.Edit.Visible = False
        grdAssets.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grdAssets.EmbeddedNavigator.Buttons.Remove.Visible = False
        grdAssets.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        addGridMenu(grdAssets)
    End Sub

#End Region


    Private Sub TrvLocations_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TrvLocations.AfterSelect
        Try
            Dim selNode As TreeNode
            selNode = TrvLocations.SelectedNode
            If Not selNode Is Nothing Then
                If chkShowSubLocationAssets.Checked Then
                    grdAssetsView.ActiveFilterString = GetGridColumnName("LocID") & " like'" & selNode.Tag & "-%'"
                Else
                    grdAssetsView.ActiveFilterString = GetGridColumnName("LocID") & " ='" & selNode.Tag & "'"
                End If
            End If
            FormatAssetDetails()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

   

#Region " Mix Database Functions"

    Private Sub Get_AssetsItems()
        Try
            objattAssets = New attItems
            Cursor.Current = Cursors.WaitCursor
            dsAssets = objBALAssets.GetAllData_Joined(objattAssets)
            Cursor.Current = Cursors.Default

            grdAstItems.DataSource = dsAssets
            format_grdAstItems()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

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

#Region "Tree View Events - Categories"

    Private Sub TrvCat_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvItemCat.AfterSelect
        Try
            Dim selNode As TreeNode
            selNode = trvItemCat.SelectedNode 'GetNodeAt(e.Location)
            If Not selNode Is Nothing Then
                grdAstItemsView.ActiveFilterString = GetGridColumnName("AstCatDesc") & " ='" & selNode.Text & "'"
                'SetGridRowCellValue(grdAstItemsView,DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "AstCatDesc", selNode.Text)
            Else
                grdAstItemsView.ClearColumnsFilter()
            End If
            FormatAssets()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

#End Region


    Private Sub tbBrowseCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabControl.SelectedIndexChanged
        Try
            If tabControl.SelectedTab Is tabPageLocations Then
                grdAssets.Visible = True
                grdAstItems.Visible = False
                grdAssetsView.ClearSelection()
                grdAssetsView.ClearColumnsFilter()
                FormatAssetDetails()
                btnAssetsBarcode.Enabled = True
                btnLocationBarcode.Enabled = True
            ElseIf tabControl.SelectedTab Is tabPageAssetCategory Then
                grdAssets.Visible = True
                grdAstItems.Visible = False
                grdAssetsView.ClearSelection()
                grdAssetsView.ClearColumnsFilter()
                FormatAssetDetails()
                btnAssetsBarcode.Enabled = True
                btnLocationBarcode.Enabled = False
            ElseIf tabControl.SelectedTab Is tabPageItemCategory Then
                grdAssets.Visible = False
                grdAstItems.Visible = True
                grdAstItemsView.ClearSelection()
                grdAstItemsView.ClearColumnsFilter()
                FormatAssets()
                btnAssetsBarcode.Enabled = False
                btnLocationBarcode.Enabled = False
            ElseIf tabControl.SelectedTab Is tabPageCustodians Then
                grdAssets.Visible = True
                grdAstItems.Visible = False
                grdAssetsView.ClearSelection()
                grdAssetsView.ClearColumnsFilter()
                FormatAssetDetails()
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

    Private Sub Load_Details(ByVal _id As String, ByVal _Name As String, ByVal _Brand As String)
        Try
            Dim dt As New DataTable
            Dim ds As New DataTable

            Dim base, tax As Double
            If _id <> "" Then
                dt = Get_AssetsDetails_byID(_id)
                If dt IsNot Nothing Then
                    SetGridRowCellValue(grdAssetDetailsView, 0, "Description", _id)

                    SetGridRowCellValue(grdAssetDetailsView, 1, "Description", dt.Rows(0)("AstNum").ToString())
                    SetGridRowCellValue(grdAssetDetailsView, 2, "Description", dt.Rows(0)("RefNo").ToString())
                    SetGridRowCellValue(grdAssetDetailsView, 23, "Description", dt.Rows(0)("Discount").ToString())
                    SetGridRowCellValue(grdAssetDetailsView, 24, "Description", dt.Rows(0)("BarCode").ToString())

                    SetGridRowCellValue(grdAssetDetailsView, 3, "Description", dt.Rows(0)("AstDesc").ToString())
                    SetGridRowCellValue(grdAssetDetailsView, 4, "Description", _Brand)
                    SetGridRowCellValue(grdAssetDetailsView, 5, "Description", _Name)

                    SetGridRowCellValue(grdAssetDetailsView, 12, "Description", dt.Rows(0)("SuppID").ToString())
                    SetGridRowCellValue(grdAssetDetailsView, 11, "Description", dt.Rows(0)("AstModel").ToString())
                    If dt.Rows(0)("POCOde").ToString() <> "AssetDetail" Then
                        SetGridRowCellValue(grdAssetDetailsView, 13, "Description", dt.Rows(0)("POCOde").ToString())
                    Else
                        SetGridRowCellValue(grdAssetDetailsView, 8, "Description", "")
                    End If
                    If dt.Rows(0)("BaseCost").ToString() <> "" Then
                        Try
                            base = CDbl(dt.Rows(0)("BaseCost").ToString())
                        Catch ex As Exception
                            base = 0.0
                        End Try
                    Else
                        base = 0.0
                    End If

                    If dt.Rows(0)("Tax").ToString() <> "" Then
                        Try
                            tax = CDbl(dt.Rows(0)("Tax").ToString())
                        Catch ex As Exception
                            tax = 0.0
                        End Try
                    Else
                        tax = 0.0
                    End If

                    SetGridRowCellValue(grdAssetDetailsView, 25, "Description", dt.Rows(0)("SerailNo").ToString())
                    Dim SrvDate As Date = dt.Rows(0)("SrvDate").ToString
                    SetGridRowCellValue(grdAssetDetailsView, 14, "Description", SrvDate.ToString(AppConfig.MaindateFormat))
                    SetGridRowCellValue(grdAssetDetailsView, 15, "Description", dt.Rows(0)("InvNumber").ToString())
                    SetGridRowCellValue(grdAssetDetailsView, 16, "Description", dt.Rows(0)("InsID").ToString())
                    SetGridRowCellValue(grdAssetDetailsView, 9, "Description", Format(CType(base, Double), "###,###,###,###,###.00"))
                    Dim PurDate As Date = dt.Rows(0)("PurDate").ToString
                    SetGridRowCellValue(grdAssetDetailsView, 7, "Description", PurDate.ToString(AppConfig.MaindateFormat))
                    SetGridRowCellValue(grdAssetDetailsView, 8, "Description", Format(CType(base + tax, Double), "###,###,###,###,###.00"))
                    SetGridRowCellValue(grdAssetDetailsView, 10, "Description", Format(CType(tax, Double), "###,###,###,###,###.00"))
                    If Not dt.Rows(0)("Disposed") Then
                        SetGridRowCellValue(grdAssetDetailsView, 17, "Description", "")
                        SetGridRowCellValue(grdAssetDetailsView, 18, "Description", "")
                        SetGridRowCellValue(grdAssetDetailsView, 19, "Description", "")
                        SetGridRowCellValue(grdAssetDetailsView, 20, "Description", "")
                        SetGridRowCellValue(grdAssetDetailsView, 21, "Description", "")
                        SetGridRowCellValue(grdAssetDetailsView, 22, "Description", "")
                    Else
                        SetGridRowCellValue(grdAssetDetailsView, 17, "Description", dt.Rows(0)("Disposed"))
                        SetGridRowCellValue(grdAssetDetailsView, 18, "Description", dt.Rows(0)("DispDesc"))
                        If Not dt.Rows(0).IsNull("DispDate") Then
                            Dim DispDate As Date = dt.Rows(0)("DispDate").ToString
                            SetGridRowCellValue(grdAssetDetailsView, 19, "Description", DispDate.ToString(AppConfig.MaindateFormat))
                        Else
                            SetGridRowCellValue(grdAssetDetailsView, 19, "Description", "")
                        End If

                        If Not dt.Rows(0).IsNull("Sel_date") Then
                            Dim Sel_date As Date = dt.Rows(0)("Sel_date").ToString
                            SetGridRowCellValue(grdAssetDetailsView, 20, "Description", Sel_date.ToString(AppConfig.MaindateFormat))
                        End If
                        'dgAssetDetails.Rows(21)(1) = dt.Rows(0)("Book Value")
                        SetGridRowCellValue(grdAssetDetailsView, 21, "Description", dt.Rows(0)("Sel_Price"))
                        SetGridRowCellValue(grdAssetDetailsView, 22, "Description", dt.Rows(0)("SoldTo"))
                    End If

                    If Not (dt.Rows(0)("LocId") Is Nothing) Then
                        If dt.Rows(0)("LocId").ToString() <> "" Then
                            'txtLoc2.Tag = dt.Rows(0)("LocId").ToString()
                            objattLocation = New attLocation
                            objattLocation.HierCode = dt.Rows(0)("LocId").ToString()
                            SetGridRowCellValue(grdAssetDetailsView, 6, "Description", objBALLocation.Comp_Path(objattLocation.HierCode))
                        Else
                            SetGridRowCellValue(grdAssetDetailsView, 6, "Description", "")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Public Sub OPenForm(ByVal frm As Form)
        Try
            frm.MdiParent = Me
            frm.Show()
            frm.BringToFront()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmDashboard = Nothing
            Dim frm As frmMain
            frm = Me.MdiParent
            frm.EnableMenu.Invoke()

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
            If ZulMessageBox.ShowMe("You are going to print " & grdAssetsView.SelectedRowsCount.ToString & " Asset Labels , Do you want to Continue ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information, False) = Windows.Forms.DialogResult.Yes Then
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
                            If GetGridRowCellValue(grdAssetsView, i, "LabelCount").ToString = "" Then
                                SetGridRowCellValue(grdAssetsView, i, "LabelCount", 1)
                            Else
                                SetGridRowCellValue(grdAssetsView, i, "LabelCount", CInt(GetGridRowCellValue(grdAssetsView, i, "LabelCount")) + 1)
                            End If
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
                        If ZulMessageBox.ShowMe("Do you want to print the Labels for related Locations", MessageBoxButtons.YesNo, MessageBoxIcon.Information, False) = Windows.Forms.DialogResult.Yes Then
                            b = True
                        Else
                            b = False
                        End If

                        Loc_childCount(TrvLocations.SelectedNode, b)
                        If ZulMessageBox.ShowMe("You are going to print " & Loc_BCodeCount & " Location Labels, Do you want to Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Information, False) = Windows.Forms.DialogResult.Yes Then
                            Create_LocBCode(TrvLocations.SelectedNode, b, strComName)
                        End If
                        Loc_BCodeCount = 0
                        Exit Sub
                    Else
                        ZulMessageBox.ShowMe("LocBCodeError")
                    End If
                Else
                    ZulMessageBox.ShowMe("LocBCodeError")
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
   

    Private Sub Create_LocBCode(ByVal SelNode As TreeNode, ByVal IncludeChilds As Boolean, ByVal strComName As String)
        Try
            Dim objLoc As New ZulAssetsBL.ZulAssetBAL.BALLocation

            Dim CName() As String = SelNode.FullPath.Split("\")
            Dim textcnt As Integer = SelNode.FullPath.IndexOf("\")
            Dim rpt As New XtraReport
            rpt = LoadReport("Location Barcode")
            rpt.DataSource = objLoc.GetLocationLabelData(SelNode.Tag, IncludeChilds)
            'If textcnt >= 0 Then
            rpt.FindControl("lblHeading1", False).Text = strComName
            ' rpt.FindControl("barCode", False).Text = "LOC" & SelNode.Tag
            ' rpt.FindControl("lbltext", False).Text = Trim(SelNode.FullPath.Substring(textcnt + 1))

            rpt.Print()

            'If IncludeChilds Then
            '    Dim aNode As TreeNode
            '    For Each aNode In SelNode.Nodes
            '        Create_LocBCode(aNode, True, strComName)
            '    Next
            'End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub TrvCat_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvItemCat.MouseUp
        If e.Button = MouseButtons.Right Then
            Dim n As TreeNode = Me.trvItemCat.GetNodeAt(e.X, e.Y)
            If Not n Is Nothing Then
                Me.trvItemCat.SelectedNode = n
            End If
        End If
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
            Dim custName As String = GetGridRowCellValue(grdCustView, e.FocusedRowHandle, "Name")
            'SetGridRowCellValue(grdAssetsView,DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "CustodianName", custName)
            grdAssetsView.ActiveFilterString = GetGridColumnName("CustodianName") & " ='" & custName & "'"
            'FormatAssetDetails()
        End If
    End Sub

#Region "Export to Server, Insert in AssetChangeHistory, and update AssetMaster"
    Private Sub UpdateExportServer(ByVal AstID As String, ByVal oldLoc As String, ByVal NewLoc As String, ByVal oldCust As String, ByVal NewCust As String, ByVal isLocation As Boolean)
        Try

            Dim objAlHadaIntegration As New BALAlhadaIntegration
            Dim objattAssetDetails1 As attAssetDetails
            Dim objBALAst_History As New BALAst_History

            If AstID <> "" Then

                objattAssetDetails1 = New attAssetDetails
                objattAssetDetails1.PKeyCode = AstID
                Dim ds As DataTable = objBALAssetDetails.GetAll_AssetDetails(objattAssetDetails1)
                objattAssetDetails1.AstNum = ds.Rows(0)("AstNum")
                If ds.Rows(0)("SerailNo").ToString <> "" Then
                    objattAssetDetails1.SerailNo = ds.Rows(0)("SerailNo")
                End If

                If ds.Rows(0)("PONumber").ToString <> "" Then
                    objattAssetDetails1.PONumber = ds.Rows(0)("PONumber")
                End If

                If ds.Rows(0)("AstDesc").ToString <> "" Then
                    objattAssetDetails1.AstDesc = ds.Rows(0)("AstDesc")
                End If

                If ds.Rows(0)("DispDate").ToString <> "" Then
                    objattAssetDetails1.DispDate = ds.Rows(0)("DispDate")
                End If

                objattAssetDetails1.GLCode = ds.Rows(0)("GLCode")
                objattAssetDetails1.PurDate = ds.Rows(0)("PurDate")
                objattAssetDetails1.Disposed = ds.Rows(0)("Disposed")

                If isLocation Then
                    NewCust = ds.Rows(0)("CustodianID")
                    oldCust = ds.Rows(0)("CustodianID")
                Else
                    NewLoc = ds.Rows(0)("LocID")
                    oldLoc = ds.Rows(0)("LocID")
                End If

                Dim Division As String = Get_Cust_DeptID(NewCust)
                Dim PrevDivision As String = Get_Cust_DeptID(oldCust)
                Dim Category As String = Get_Assets_AstCatID(objattAssetDetails1.ItemCode)

                objAlHadaIntegration.Insert_AssetChange_ExportServer(objattAssetDetails1, Division, PrevDivision, NewLoc, oldLoc, Category, Now.Date)
                objAlHadaIntegration.Update_AssetMaster_ExportServer(objattAssetDetails1.AstNum, Division, PrevDivision, NewLoc, oldLoc, Now.Date)
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Function Get_Assets_AstCatID(ByVal itemCode As String) As String
        Try
            Dim ds As New DataTable
            Dim objattAssets As New attItems
            Dim objBALAssets As New BALItems

            objattAssets.PKeyCode = itemCode
            ds = objBALAssets.GetAll_Items(objattAssets)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Return ds.Rows(0)("AstCatID").ToString
                Else
                    Return Nothing
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, "Get_Assets")
        End Try
        Return Nothing
    End Function

    Private Function Get_Cust_DeptID(ByVal CustID As String) As String
        Try
            Dim ds As New DataTable
            objattCustodian = New attCustodian
            objattCustodian.PKeyCode = CustID
            ds = objBALCustodian.GetAll_Custodian(objattCustodian)
            If ds IsNot Nothing Then
                Dim deptID As String = CStr(ds.Rows(0)("DeptID"))
                Dim DeptIDSplit() As String = deptID.Split("-"c)
                Return Format(CInt(DeptIDSplit(0)), "0#") & DeptIDSplit(1)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, "Get_Cust")
            Return Nothing
        End Try
    End Function
#End Region

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

    Private Sub grdAssetsView_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAssetsView.DoubleClick
        Try
            'disable double click on the footer and other parts.
            Dim GHI As New GridHitInfo()
            GHI = grdAssetsView.CalcHitInfo(CType(e, DevExpress.Utils.DXMouseEventArgs).Location)
            If GHI.InRow Then
                If grdAssetsView.SelectedRowsCount > 0 Then
                    Dim intRow As Integer = grdAssetsView.GetSelectedRows(0)
                    If intRow >= 0 Then
                        'Dim frm As New frmAssetsDetails
                        'frm.MdiParent = Me.MdiParent
                        ''frm.AssetDetId = GetGridRowCellValue( grdAssetsView,intRow, "AstID")
                        'frm.WindowState = FormWindowState.Maximized
                        'frm.Show()
                        'frm.BringToFront()
                        'frm.LocateAsset(GetGridRowCellValue( grdAssetsView,intRow, "AstID"))
                        ShowAssetDetailsForm(GetGridRowCellValue(grdAssetsView, intRow, "AstID"))
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub grdViewAstItems_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAstItemsView.DoubleClick
        'disable double click on the footer and other parts.
        Dim GHI As New GridHitInfo()
        GHI = grdAstItemsView.CalcHitInfo(CType(e, DevExpress.Utils.DXMouseEventArgs).Location)
        If GHI.InRow Then
            If grdAstItemsView.SelectedRowsCount > 0 Then
                Dim intRow As Integer = grdAstItemsView.GetSelectedRows(0)
                If intRow >= 0 Then
                    Dim frm As New frmItems
                    frm.AssetId = GetGridRowCellValue(grdAstItemsView, intRow, "itemcode")
                    frm.ShowDialog()
                    frm.BringToFront()
                End If
            End If
        End If
    End Sub

    Private Sub grdAssetsView_SelectionChanged(ByVal sender As System.Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles grdAssetsView.SelectionChanged
        Try
            If grdAssetsView.SelectedRowsCount > 0 Then
                Dim intRow As Integer = grdAssetsView.GetSelectedRows(0)
                If intRow >= 0 Then
                    'If Not (GetGridRowCellValue( grdAssetsView,intRow, "AstID") Is Nothing And GetGridRowCellValue( grdAssetsView,intRow, "CustodianName") Is Nothing And GetGridRowCellValue( grdAssetsView,intRow, "Brand") Is Nothing) Then
                    Load_Details(GetGridRowCellValue(grdAssetsView, intRow, "AstID"), GetGridRowCellValue(grdAssetsView, intRow, "CustodianName"), GetGridRowCellValue(grdAssetsView, intRow, "Brand"))
                    'End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub grdViewAstItems_SelectionChanged(ByVal sender As System.Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles grdAstItemsView.SelectionChanged
        Try
            If grdAstItemsView.SelectedRowsCount > 0 Then
                Dim intRow As Integer = grdAstItemsView.GetSelectedRows(0)
                If intRow >= 0 Then ' And grdViewAstItems.Columns.Count > 0 

                    If Not (GetGridRowCellValue(grdAstItemsView, intRow, "itemcode") Is Nothing) Then
                        SetGridRowCellValue(grdAssetDetailsView, 0, "Description", GetGridRowCellValue(grdAstItemsView, intRow, "itemcode").ToString())

                        If Not (GetGridRowCellValue(grdAstItemsView, intRow, "AstDesc") Is Nothing) Then SetGridRowCellValue(grdAssetDetailsView, 3, "Description", GetGridRowCellValue(grdAstItemsView, intRow, "AstDesc").ToString())
                        If Not (GetGridRowCellValue(grdAstItemsView, intRow, "AstModel") Is Nothing) Then SetGridRowCellValue(grdAssetDetailsView, 4, "Description", GetGridRowCellValue(grdAstItemsView, intRow, "AstModel").ToString())

                        If Not (GetGridRowCellValue(grdAstItemsView, intRow, "AstCatID") Is Nothing) Then
                            If GetGridRowCellValue(grdAstItemsView, intRow, "AstCatID").ToString() <> "" Then
                                SetGridRowCellValue(grdAssetDetailsView, 2, "Description", objBALCategory.Comp_Path(GetGridRowCellValue(grdAstItemsView, intRow, "AstCatID").ToString()))
                            End If
                        End If
                        Dim objattAssetDetails As New attAssetDetails
                        objattAssetDetails.ItemCode = GetGridRowCellValue(grdAstItemsView, intRow, "itemcode").ToString()
                        Dim assetcount As Integer = objBALAssetDetails.GetAssetsCount(objattAssetDetails)
                        SetGridRowCellValue(grdAssetDetailsView, 5, "Description", assetcount)
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Dim downHitInfo As GridHitInfo = Nothing
    Private Sub grdAssetsView_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdAssetsView.MouseDown, grdAstItemsView.MouseDown
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

    Private Sub grdAssetsView_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdAssetsView.MouseMove, grdAstItemsView.MouseMove
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

    Private Sub TrvCat_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trvItemCat.DragOver
        Try
            Dim TNode As TreeNode 'Target Node 
            Dim pt As Point = trvItemCat.PointToClient(New Point(e.X, e.Y))
            TNode = trvItemCat.GetNodeAt(pt)

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
                    ZulMessageBox.ShowMe("AssetlocationMinlevel")
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
                        If AppConfig.ExportToServer Then
                            UpdateExportServer(GetGridRowCellValue(grdAssetsView, index, "AstID"), GetGridRowCellValue(grdAssetsView, index, "LocID"), NewLocID, "", "", True)
                        End If

                        objattAstHistory = New attAstHistory()
                        objattAstHistory.AstID = GetGridRowCellValue(grdAssetsView, index, "AstID")
                        objattAstHistory.Status = 3
                        objattAstHistory.InvSchCode = 1
                        objattAstHistory.HisDate = Now.Date
                        objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                        objattAstHistory.Fr_loc = GetGridRowCellValue(grdAssetsView, index, "LocID")
                        objattAstHistory.To_Loc = NewLocID
                        objBALAst_History.Insert_Ast_History(objattAstHistory)
                        objattAssetDetails.PKeyCode = GetGridRowCellValue(grdAssetsView, index, "AstID")
                        objattAssetDetails.LocID = NewLocID
                        objattAssetDetails.LastEditBY = AppConfig.LoginName

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

                        If AppConfig.ExportToServer Then
                            UpdateExportServer(GetGridRowCellValue(grdAssetsView, i, "AstID").ToString, "", "", GetGridRowCellValue(grdAssetsView, i, "CustodianID").ToString, newcustID, False)
                        End If

                        objattAst_Cust_history = New attAst_Cust_history()
                        objattAst_Cust_history.AstID = GetGridRowCellValue(grdAssetsView, i, "AstID").ToString
                        objattAst_Cust_history.HisDate = Now.Date
                        objattAst_Cust_history.PKeyCode = objBALAst_Cust_history.GetNextPKey_AstHistory()
                        objattAst_Cust_history.Fr_Cust = GetGridRowCellValue(grdAssetsView, i, "CustodianID").ToString
                        objattAst_Cust_history.To_Cust = newcustID
                        objBALAst_Cust_history.Insert_Ast_Cust_history(objattAst_Cust_history)

                        objattAssetDetails.PKeyCode = GetGridRowCellValue(grdAssetsView, i, "AstID").ToString
                        objattAssetDetails.CustodianID = newcustID
                        objattAssetDetails.LastEditBY = AppConfig.LoginName
                        objBALAssetDetails.Update_Custodian(objattAssetDetails)

                        'update the grid values after drag drop is made.
                        SetGridRowCellValue(grdAssetsView, i, "CustodianID", newcustID)
                        SetGridRowCellValue(grdAssetsView, i, "CustodianName", newcustName)
                    End If
                    pb.PerformStep()
                    Application.DoEvents()
                Next
                SetGridRowCellValue(grdAssetsView, DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "CustodianName", GetGridRowCellValue(grdCustView, grdCustView.GetSelectedRows(0), "Name"))

            Catch ex As Exception
                GenericExceptionHandler(ex, WhoCalledMe)
            Finally
                pb.Visible = False
            End Try

        End If
    End Sub
    Private Sub TrvCat_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trvItemCat.DragDrop
        If grdAstItemsView.SelectedRowsCount < 1 Then
            Exit Sub
        End If

        Try
            Dim pt As Point = trvItemCat.PointToClient(New Point(e.X, e.Y))
            SelNode = trvItemCat.GetNodeAt(pt)

            If Not SelNode Is Nothing Then
                'Check the system parameters and disallow drop the Assets based on AssetCategoryMinLevel Parameters.
                If SelNode.Level + 1 < SysParam.AssetCategoryMinlevel Then
                    ZulMessageBox.ShowMe("AssetCategoryminlevel")
                    Exit Sub
                End If

                Dim NewCatID As String = SelNode.Tag
                Dim NewCatName As String = SelNode.Text
                Dim NewCatFullName As String = objBALCategory.GetCatFullPath(NewCatID)
                Dim OldCatName As String = GetGridRowCellValue(grdAstItemsView, DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "AstCatDesc")
                pb.Visible = True
                pb.Value = 1
                pb.Step = 1
                pb.Maximum = grdAstItemsView.SelectedRowsCount
                grdAstItemsView.ClearColumnsFilter()
                For Each i As Integer In grdAstItemsView.GetSelectedRows()
                    If GetGridRowCellValue(grdAstItemsView, i, "itemcode").ToString <> "" Then
                        objattAssets.PKeyCode = GetGridRowCellValue(grdAstItemsView, i, "itemcode")
                        objattAssets.AstCatID = NewCatID
                        objBALAssets.Update_Item_Cat(objattAssets)
                        'update the grid values after drag drop is made.
                        SetGridRowCellValue(grdAstItemsView, i, "AstCatID", NewCatID)
                        SetGridRowCellValue(grdAstItemsView, i, "AstCatDesc", NewCatName)
                        SetGridRowCellValue(grdAstItemsView, i, "CatFullPath", NewCatFullName)
                    End If
                    pb.PerformStep()
                    Application.DoEvents()
                Next
                SetGridRowCellValue(grdAstItemsView, DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "AstCatDesc", OldCatName)
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally
            pb.Visible = False
        End Try

    End Sub

    Private Sub trvAssetCat_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvAssetCat.AfterSelect
        Try
            Dim selNode As TreeNode
            selNode = trvAssetCat.SelectedNode
            If Not selNode Is Nothing Then
                grdAssetsView.ActiveFilterString = GetGridColumnName("AstCatID") & " ='" & selNode.Tag & "'"
            End If
            FormatAssetDetails()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub chkShowSubLocationAssets_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowSubLocationAssets.CheckedChanged
        Try
            Dim selNode As TreeNode
            selNode = TrvLocations.SelectedNode
            If Not selNode Is Nothing Then
                If chkShowSubLocationAssets.Checked Then
                    grdAssetsView.ActiveFilterString = GetGridColumnName("LocID") & " like'" & selNode.Tag & "-%'"
                Else
                    grdAssetsView.ActiveFilterString = GetGridColumnName("LocID") & " ='" & selNode.Tag & "'"
                End If
            End If
            FormatAssetDetails()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
End Class
