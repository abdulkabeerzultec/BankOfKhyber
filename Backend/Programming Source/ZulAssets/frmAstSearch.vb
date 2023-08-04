Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports DevExpress.XtraReports.UI
Public Class frmAstSearch


#Region "-- My Decleration --"

    Dim objattBrand As attBrand
    Dim objBALbrand As New BALbrand
    Dim objattAssets As attItems
    Dim objBALAssets As New BALItems
    Dim objattCategory As attCategory
    Dim objBALCategory As New BALCategory
    Dim SelNode As TreeNode
    Dim SelNodeCat As TreeNode
    Dim objBALCustodian As New BALCustodian
    Dim objattLocation As attLocation
    Dim objBALLocation As New BALLocation
    Dim objattAssetDetails As attAssetDetails
    Dim objBALAssetDetails As New BALAssetDetails

    Dim objBALAssetsDetails As New BALAssetDetails
    Dim objattAssetsDetails As attAssetDetails

    Public isReport As Int16 = 0

#End Region


    Private Sub frmAstSearch_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmAstSearch = Nothing
    End Sub

    Private Sub frmAstSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        If isReport = 1 Then
            btnPrv.Visible = True
            Me.AcceptButton = btnPrv
        Else
            Me.AcceptButton = btnSearch
        End If
        'Me.format_Grid_AssetsDet()
    End Sub

    Private Sub btnRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRef.Click
        AssetsID.SelectedText = ""
        txtAstNum.Text = ""
        txtBrandID.SelectedIndex = -1
        ZTLocation.SelectedText = ""
        ZTLocation.SelectedValue = ""
        ZTCategory.SelectedText = ""
        ZTCategory.SelectedValue = ""
        txtDesc.Text = ""
        itmCode.SelectedText = ""
        cmbCust.SelectedIndex = -1
        cmbHier.Text = ""
        txtSerialNumber.Text = ""
        grd.DataSource = Nothing
    End Sub

#Region " -- Methods --"
    Private Sub Get_AssetsDetails(ByVal LocId As String, ByVal CustID As String, ByVal AstId As String, ByVal AstNum As String, ByVal AstBrandID As String, ByVal ItemCode As String, ByVal AstCatID As String, ByVal DeptID As String, ByVal strDesc As String, ByVal SerialNumber As String)
        Try
            Dim ds As New DataTable
            Dim objattAssets As New attItems

            objattAssetDetails = New attAssetDetails
            If strDesc <> "" Then
                objattAssetDetails.AstDesc = strDesc
            End If
            If LocId <> "" Then
                objattAssetDetails.LocID = LocId
            End If
            If CustID <> "" Then
                objattAssetDetails.CustodianID = CustID
            End If
            If AstId <> "" Then
                objattAssetDetails.PKeyCode = AstId
            End If
            If AstNum <> "" Then
                objattAssetDetails.AstNum = AstNum
            End If
            If AstCatID <> "" Then
                objattAssets.AstCatID = AstCatID
            End If
            If AstBrandID <> "" Then
                objattAssets.AstBrandID = AstBrandID
            End If
            If ItemCode <> "" Then
                objattAssets.PKeyCode = ItemCode
            End If

            If SerialNumber <> "" Then
                objattAssetDetails.SerailNo = SerialNumber
            End If

            grd.DataSource = objBALAssetDetails.Asset_Search(objattAssetDetails, objattAssets, chkSubLevels.Checked, DeptID)
            format_Grid_AssetsDet()

            'If ds Is Nothing = False Then
            'If ds.Tables.Count > 0 Then
            '    If ds.Rows.Count > 0 Then

            '        'dgAssets.DataSource = ds
            '        FillDSToGrid(ds, dgAssets, pb)
            '        dgAssets.Tree.Column = 5

            '        dgAssets.SubtotalPosition = SubtotalPositionEnum.BelowData

            '        dgAssets.Tree.Style = TreeStyleFlags.Simple

            '        dgAssets.Subtotal(AggregateEnum.Sum, 0, 0, 10, "Grand Total")

            '        format_Grid_AssetsDet()
            '        Exit Sub

            '    End If
            'End If
            'grd.DataSource = Nothing
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub
    Private Sub format_Grid_AssetsDet()
        'Dim cs As CellStyle

        'cs = dgAssets.Styles(CellStyleEnum.GrandTotal)

        'cs.BackColor = Color.Yellow
        'cs.ForeColor = Color.Black

        'cs.Font = New Font(Font, 3)


        grdView.Columns(0).Visible = False
        grdView.Columns(0).Caption = ""

        grdView.Columns(1).Caption = "Asset #"
        grdView.Columns(1).Width = 60
        grdView.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdView.Columns(2).Caption = "Item Code"
        grdView.Columns(2).Width = 70
        grdView.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdView.Columns(3).Caption = "Description"
        grdView.Columns(3).Width = 105

        grdView.Columns(4).Caption = "Asset ID"
        grdView.Columns(4).Width = 105

        grdView.Columns(5).Caption = "Location"
        grdView.Columns(5).Width = 160
        grdView.Columns(6).Visible = False
        grdView.Columns(7).Visible = False
        grdView.Columns(8).Visible = False
        grdView.Columns(9).Visible = False
        grdView.Columns(10).Width = 80

        grdView.Columns(10).Caption = "Total Cost"
        grdView.Columns(10).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(11).Visible = False
        GetGridColumn(grdView, "Serial#").Width = 80
        'grdView.Columns("Serial#").Width = 80

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

        grdView.OptionsView.ShowFooter = True
        grdView.Columns(5).SummaryItem.FieldName = grdView.Columns(10).FieldName
        grdView.Columns(5).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        grdView.Columns(5).SummaryItem.DisplayFormat = "Grand Total= {0:n0}"
        addGridMenu(grd)

        'grdView.Columns(10).coFormat = "###,###,###,###,###.00"
        'If dgAssets.Rows.Count > 1 Then
        'dgAssets.Rows(dgAssets.Rows.Count - 1).Style = cs
        'End If
    End Sub
#End Region

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            MainForm.showLoading()
            Dim strDepartment As String

            If cmbHier.Text <> "" Then
                strDepartment = cmbHier.Tag
            Else
                strDepartment = ""
            End If

            If txtAstNum.Text <> "" Then
                Try
                    Dim intA As Integer = CInt(txtAstNum.Text)
                    Get_AssetsDetails(ZTLocation.SelectedValue, cmbCust.SelectedValue, AssetsID.SelectedText, txtAstNum.Text, txtBrandID.SelectedValue, itmCode.SelectedText, ZTCategory.SelectedValue, strDepartment, txtDesc.Text, txtSerialNumber.Text)
                Catch ex As Exception
                    txtAstNum.Text = ""
                    ZulMessageBox.ShowMe("InvalidAstNum")
                End Try
            Else
                Get_AssetsDetails(ZTLocation.SelectedValue, cmbCust.SelectedValue, AssetsID.SelectedText, Trim(txtAstNum.Text), txtBrandID.SelectedValue, itmCode.SelectedText, ZTCategory.SelectedValue, strDepartment, txtDesc.Text, txtSerialNumber.Text)

            End If
        Finally
            MainForm.hideLoading()
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        FormController.objfrmAstSearch = Nothing
        Me.Dispose()
    End Sub

    Private Sub btnPrv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrv.Click
        Try
            MainForm.showLoading()
            If isReport = 1 Then
                Dim FocRow As Integer = grdView.FocusedRowHandle
                If FocRow >= 0 Then
                    Dim ds As New DataTable
                    objattAssetDetails = New attAssetDetails
                    ds = Me.Get_AssetDetails(GetGridRowCellValue(grdView, FocRow, "AstID"))
                    'CType(Me.MdiParent, frmMain).LoadReport("Asset Details", ds)
                End If
            End If
        Finally
            MainForm.hideLoading()
        End Try
    End Sub

    Private Function Get_AssetDetails(ByVal strAstID As String) As DataTable
        objattAssetsDetails = New attAssetDetails
        objattAssetsDetails.PKeyCode = strAstID
        Dim ds As DataTable

        ds = objBALAssetsDetails.GetAsset_Details(objattAssetsDetails)
        If ds Is Nothing = False Then
            If ds.Rows.Count > 0 Then
                Return ds
            End If
        End If
        Return New DataTable
    End Function
    Private Sub btnLOV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLOV.Click
        Dim obj As New ZulHierTree.clsTree

        If AppConfig.DbType = 1 Then
            obj.DBType = 2
        ElseIf AppConfig.DbType = 2 Then
            obj.DBType = 1
        End If


        obj.SelectOnlyLastNode = False
        obj.DBName = AppConfig.DbName
        obj.DBPass = AppConfig.DbPass
        If AppConfig.DbType = 1 Then
            obj.DBServer = AppConfig.DbServer & "," & AppConfig.DBSQLPort
            'obj.DBServer = AppConfig.DbServer
        Else
            obj.DBServer = AppConfig.DbServer
        End If
        obj.DBUName = AppConfig.DbUname
        obj.OpenTree(cmbHier)
    End Sub


    Private Sub cmbHier_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbHier.KeyDown
        If e.KeyData = Keys.Back Or e.KeyData = Keys.Delete Then
            cmbHier.Text = ""
            cmbHier.Tag = ""
        ElseIf e.Alt And e.KeyValue = Keys.Down Then
            btnLOV_Click(sender, e)
        End If
    End Sub

    Private Sub btnLOV_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnLOV.KeyDown
        If e.Alt And e.KeyValue = Keys.Down Then
            btnLOV_Click(sender, e)
        End If
    End Sub

    Private Sub grdView_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdView.DoubleClick
        If isReport = 0 Then
            Dim FocRow As Integer = grdView.FocusedRowHandle
            If FocRow >= 0 Then
                If Not (GetGridRowCellValue(grdView, FocRow, "AstID") Is Nothing) Then
                    'Dim frm As New frmAssetsDetails
                    ''frm.AssetDetId = GetGridRowCellValue( grdView,FocRow, "AstID")
                    'frm.MdiParent = Me.MdiParent
                    'frm.StartPosition = FormStartPosition.CenterParent
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    'frm.BringToFront()
                    'frm.LocateAsset(GetGridRowCellValue( grdView,FocRow, "AstID"))
                    ShowAssetDetailsForm(GetGridRowCellValue(grdView, FocRow, "AstID"))
                End If
            End If
        End If
    End Sub

    Private Sub txtBrandID_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBrandID.LovBtnClick
        Try
            txtBrandID.ValueMember = "AstBrandID"
            txtBrandID.DisplayMember = "AstBrandName"
            Dim objBALBrand As New BALbrand
            txtBrandID.DataSource = objBALBrand.GetAll_Brand(New attBrand)
            txtBrandID.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub cmbCust_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCust.LovBtnClick
        Try
            Dim objBALCustodian As New BALCustodian
            cmbCust.ValueMember = "ID"
            cmbCust.DisplayMember = "Name"
            cmbCust.DataSource = objBALCustodian.GetAllData_GetCombo()
            cmbCust.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub itmCode_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles itmCode.LovBtnClick
        Try
            itmCode.ValueMember = "ItemCode"
            itmCode.DisplayMember = "ItemCode"
            Dim objBALAssets As New BALItems
            itmCode.DataSource = objBALAssets.GetAllData_GetCombo(New attItems)
            itmCode.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub AssetsID_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssetsID.LovBtnClick
        Try
            AssetsID.ValueMember = "AstId"
            AssetsID.DisplayMember = "AstId"
            Dim objattAssetDetails As attAssetDetails = New attAssetDetails
            Dim objBALAssetDetails As BALAssetDetails = New BALAssetDetails
            objattAssetDetails.Disposed = True
            AssetsID.DataSource = objBALAssetDetails.GetAsset_DetailsCombo(objattAssetDetails)
            AssetsID.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub ZTLocation_TreeBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZTLocation.TreeBtnClick
        Try
            Dim ds As New DataTable
            Dim objBALLocation As New BALLocation
            ZTLocation.DataSource = objBALLocation.GetComboLocations(New attLocation)
            ZTLocation.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub ZTCategory_TreeBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZTCategory.TreeBtnClick
        Try
            Dim ds As New DataTable
            Dim objBALCategory As New BALCategory
            ZTCategory.DataSource = objBALCategory.GetAll_Category(New attCategory)
            ZTCategory.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

End Class
