Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports
Imports DevExpress.XtraNavBar
Public Class frmReportCenter

#Region "Declerations"
    Dim valProvInventory As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim valProvCategory As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Private ReportName As String = ""
    Dim objBALStandardReport As New BALStandardReports
    Dim SelectedTabPageIndex As Integer = -1
#End Region
#Region "Form Events"

    Private Sub frmRptFilter_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If tabControl.SelectedTabPage.Name = tabPreviewReport.Name Then
            e.Cancel = True
            nvControl.Visible = True
            tabControl.SelectedTabPageIndex = TabIndex
        Else
            FormController.objfrmRptFilter = Nothing
        End If
    End Sub


    Private Sub frmRptFilter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        valProvInventory.SetValidationRule(cmbSch.TextBox, valRulenotEmpty)
        valProvCategory.SetValidationRule(dtFrom.TextBox, valRulenotEmpty)
        valProvCategory.SetValidationRule(dtTo.TextBox, valRulenotEmpty)
        tabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
        tabControl.SelectedTabPage = tabFilter

        RegisterAppEditionReports()
        RegisterIntegrationReports()

        GetExtendedReportList()
        SetReportsPermissions()

        dtFrmDt.CustomFormat = AppConfig.MaindateFormat
        dtExpectedDepreciation.CustomFormat = AppConfig.MaindateFormat
        dtToDT.CustomFormat = AppConfig.MaindateFormat
        'Expanded all groups in the navigation menu.
        For Each nvGroup As NavBarGroup In nvControl.Groups
            nvGroup.Expanded = True
        Next
    End Sub

#End Region
    Private Sub SetReportsPermissions()
        'Enable Report Items.
        Dim RoleID As Integer = MainForm.RoleID
        Dim objBALReport As New BALReports
        Dim dsRoleReports As DataTable = objBALReport.GetRoleReports(RoleID)

        For Each itm As NavBarItem In nvControl.Items
            If RoleID = 1 Then ' enable all items for admin Role.
                If itm.Visible Then
                    itm.Visible = True
                End If
            Else
                'Show the report only if it's visible, because it may be hidden according to the edtion and integration.
                If dsRoleReports.Select("ReportName = '" & itm.Caption & "'").Length > 0 Then
                    If itm.Visible Then
                        itm.Visible = True
                    End If
                Else
                    itm.Visible = False
                End If
            End If
        Next
    End Sub
    Private Sub RegisterAppEditionReports()
        Select Case AppConfig.AppEdition
            Case ApplicationEditions.Inventory
                nvItemDepreciationMethods.Visible = False
                nvItemAssetBooks.Visible = False
                nvItemDepreciationBooks.Visible = False
                nvItemExpectedDepr.Visible = False
                nvItemAssetLedger.Visible = False
                nvItemAssetRegister.Visible = False
                nvItemAssetByCat.Visible = False
                nvItemAssetsBySubCat.Visible = False
                'mnuRptInterCompanyTrans.Visible = False
            Case ApplicationEditions.Tracking
                nvItemDepreciationMethods.Visible = False
                nvItemAssetBooks.Visible = False
                nvItemDepreciationBooks.Visible = False
                nvItemExpectedDepr.Visible = False
                nvItemAssetLedger.Visible = False
                nvItemAssetRegister.Visible = False
                nvItemAssetByCat.Visible = False
                nvItemAssetsBySubCat.Visible = False
            Case ApplicationEditions.Financial

            Case ApplicationEditions.Enterprise

        End Select
    End Sub

    Private Sub RegisterIntegrationReports()
        If IntegrationName = IntegrationType.CMAIntegration Then
            nvGroupMaster.Visible = False
            nvItemAssetTag.Visible = False
            nvItemAssetDetails.Visible = False
            nvItemInventory.Visible = False
            'nvItemDisposed.Visible = False
            nvItemCompanyAsset.Visible = False
            nvItemDepreciationMethods.Visible = False
            nvItemAssetBooks.Visible = False
            nvItemDepreciationBooks.Visible = False
            nvItemExpectedDepr.Visible = False
            nvItemAssetLedger.Visible = False
            nvItemAssetRegister.Visible = False
            nvItemAssetByCat.Visible = False
            nvItemAssetsBySubCat.Visible = False

            lblCompany.Visible = False
            lblsupplier.Visible = False
            lblBook.Visible = False
            lblBrand.Visible = False
            lblCust.Visible = False
            lblHierarchy.Visible = False

            cmbComp.Visible = False
            cmbSupp.Visible = False
            cmbBook.Visible = False
            cmbBrand.Visible = False
            cmbCust.Visible = False
            cmbDepart.Visible = False
            btnHierLOV.Visible = False
            cmbDataSource.SelectedIndex = 0
        Else
            lblDataSource.Visible = False
            cmbDataSource.Visible = False
        End If
    End Sub

    Private Sub GetExtendedReportList()
        'Remove all Extended items
        For i As Integer = nvGroupExtended.ItemLinks.Count - 1 To 0 Step -1
            nvGroupExtended.ItemLinks.RemoveAt(i)
        Next
        Dim dt As DataTable
        Dim objBALRptFile As New BALReports
        dt = objBALRptFile.GetExtendedReports()
        For Each row As DataRow In dt.Rows
            Dim itm As New DevExpress.XtraNavBar.NavBarItem
            itm.Caption = row("ReportName").ToString
            itm.Tag = row("Query")
            itm.SmallImageIndex = 3
            'itm.Enabled = False
            AddHandler itm.LinkClicked, AddressOf ExtendedReport_Click
            nvGroupExtended.ItemLinks.Add(itm)
        Next

        'Show extended group if the extended report count > 0
        nvGroupExtended.Visible = nvGroupExtended.ItemLinks.Count > 0
        'If nvGroupExtended.ItemLinks.Count > 0 Then
        '    nvGroupExtended.Visible = True
        'Else
        '    nvGroupExtended.Visible = False
        'End If
    End Sub

    Public Sub ExtendedReport_Click(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs)
        'show the report
        Dim repName As String = CType(sender, NavBarItem).Caption
        Dim repQuery As String = CType(sender, NavBarItem).Tag
        Dim DbOp As New DBOperations
        Dim cmd As New OleDb.OleDbCommand
        cmd.CommandText = repQuery
        Dim dt As DataTable = DbOp.General_Executer(cmd) 'it will not work if there are parameters.

        Dim rpt As XtraReport = LoadReports.LoadMasterReport(repName, dt)
        If rpt IsNot Nothing Then
            ShowReport(rpt)
            tabControl.SelectedTabPage = tabPreviewReport
            Me.Text = String.Format("{0} - {1}", e.Link.Group.Caption, e.Link.Caption)
        End If
    End Sub

    Public Shared Function Round(ByVal Number As Double, Optional ByVal NumDigitsAfterDecimal As Integer = 0) As Double
        Return CDbl(FormatNumber(Number, NumDigitsAfterDecimal))
    End Function

    Private Sub chkExclude_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkExclude.CheckedChanged
        If chkExclude.Checked Then
            chkDisposed.Checked = False
        End If
    End Sub

    Private Sub chkDisposed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDisposed.CheckedChanged
        If chkDisposed.Checked Then
            chkExclude.Checked = False
        End If
    End Sub

    Private Sub cmbDepart_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbDepart.KeyDown
        If e.KeyData = Keys.Back Or e.KeyData = Keys.Delete Then
            cmbDepart.Text = ""
            cmbDepart.Tag = ""
        ElseIf e.Alt And e.KeyValue = Keys.Down Then
            btnLOV_Click(sender, e)
        End If
    End Sub


#Region "NavBarItem Clicks"

    Private Sub nvItemStandard_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nvItemNewTags.LinkClicked, nvItemInventory.LinkClicked, nvItemExpectedDepr.LinkClicked, nvItemDisposed.LinkClicked, nvItemDepreciationBooks.LinkClicked, nvItemCompanyAsset.LinkClicked, nvItemAssetTag.LinkClicked, nvItemAssetRegister.LinkClicked, nvItemAssetLedger.LinkClicked, nvItemAssetDetails.LinkClicked, ntAssetLog.LinkClicked
        tabControl.SelectedTabPage = tabFilter
        lblReportName.Text = e.Link.Caption
        PictureBox1.Image = e.Link.GetImage
        Me.Text = String.Format("{0} - {1}", e.Link.Group.Caption, e.Link.Caption)
        ReportName = e.Link.Caption


        chkExclude.Visible = True
        chkDisposed.Visible = True
        grpDisposedReport.Visible = False

        'lblCostPrice.Visible = True
        'cmbTotalCostFilterType.Visible = True
        'txtTotalCost.Visible = True

        lblExpectedDepreciation.Visible = False
        dtExpectedDepreciation.Visible = False
        lblDataSource.Visible = False
        cmbDataSource.Visible = False

        Select Case ReportName
            Case ReportNames.ExpectedDepreciation
                lblExpectedDepreciation.Visible = True
                dtExpectedDepreciation.Visible = True
                grpFilterByDate.Visible = True
            Case ReportNames.DepreciationBooks
                grpFilterByDate.Visible = True
            Case ReportNames.AssetsRegister
                grpFilterByDate.Visible = False
            Case ReportNames.ItemsInventory
                grpFilterByDate.Visible = False
                'lblCostPrice.Visible = False
                'cmbTotalCostFilterType.Visible = False
                'txtTotalCost.Visible = False

            Case ReportNames.NewTags
                If IntegrationName = IntegrationType.CMAIntegration Then
                    lblDataSource.Visible = True
                    cmbDataSource.Visible = True
                End If
            Case ReportNames.DisposedAssets
                grpDisposedReport.Visible = True
                chkExclude.Visible = False
                chkDisposed.Visible = False
                grpFilterByDate.Visible = False
            Case Else
                grpFilterByDate.Visible = True
        End Select
    End Sub

    Private Sub navItemMaster_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navItemDesignation.LinkClicked, nvItemSupplier.LinkClicked, nvitemInsurer.LinkClicked, nvItemBrand.LinkClicked, nvItemCustodian.LinkClicked, nvItemAddressBook.LinkClicked, nvItemInventSch.LinkClicked, nvItemAssetBooks.LinkClicked, nvAssetItems.LinkClicked, nvItemDepreciationMethods.LinkClicked, nvItemDisposalMethods.LinkClicked
        Dim ds As DataTable = GetMasterReportDataTable(e.Link.Caption)
        Dim rpt As XtraReport = LoadReports.LoadMasterReport(e.Link.Caption, ds)
        'LoadMasterReport(e.Link.Caption, ds)
        If rpt IsNot Nothing Then
            ShowReport(rpt)
            tabControl.SelectedTabPage = tabPreviewReport
            Me.Text = String.Format("{0} - {1}", e.Link.Group.Caption, e.Link.Caption)
        End If
    End Sub

    Private Sub nvItemAudit_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nvItemTransferredAssets.LinkClicked, nvItemMissingAssets.LinkClicked, nvItemMisplaced.LinkClicked, nvItemFoundAssets.LinkClicked, nvItemAnonymous.LinkClicked, nvItemAllocatedAssets.LinkClicked, nvItemAllAssets.LinkClicked, nvCostCenterAudit.LinkClicked
        tabControl.SelectedTabPage = tabAuditStatus
        lblAuditStatus.Text = e.Link.Caption
        imgAuditStatus.Image = e.Link.GetImage
        Me.Text = String.Format("{0} - {1}", e.Link.Group.Caption, e.Link.Caption)
        ReportName = e.Link.Caption
    End Sub

    Private Sub nvItemAssetByCat_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nvItemAssetByCat.LinkClicked, nvItemAssetsBySubCat.LinkClicked
        tabControl.SelectedTabPage = tabCategory
        lblCategory.Text = e.Link.Caption
        imgCategory.Image = e.Link.GetImage
        Me.Text = String.Format("{0} - {1}", e.Link.Group.Caption, e.Link.Caption)
        ReportName = e.Link.Caption
    End Sub

#End Region

#Region "Get Reports Data"
    Private Function GetMasterReportDataTable(ByVal ReportName As String) As DataTable
        Select Case ReportName
            Case ReportNames.Designations
                Return (New BALDesignation).GetAll_Designation(New attDesignation)
            Case ReportNames.Custodians
                Return (New BALCustodian).GetAllData_Custodian()
            Case ReportNames.Brands
                Return (New BALbrand).GetAll_Brand(New attBrand)
            Case ReportNames.Insurers
                Return (New BALInsurer).GetAll_Insurer(New attInsurer)
            Case ReportNames.Suppliers
                Return (New BALSupplier).GetAll_Supplier(New attSupplier)
            Case ReportNames.DisposalMethods
                Return (New BALDisposalMethod).GetAll_DisposalMethod(New attDisposalMethod)
            Case ReportNames.DepreciationMethods
                Return (New BALDepreciationMethod).GetAll_DepreciationMethod(New attDepreciationMethod)
            Case ReportNames.AssetItems
                Return (New BALItems).GetAll_Items(New attItems)
            Case ReportNames.AssetBooks
                Return (New BALBooks).GetAll_Book(New attBook)
            Case ReportNames.InventorySchedules
                Return (New BALAst_INV_Schedule).GetAll_invSch(New attInvSchedule)
            Case ReportNames.AddressBook
                Return (New BALAddress).GetAll_Address(New attAddress)
            Case Else
                Return Nothing
        End Select
    End Function
    Private Function GenAssetsRegisterDataTable(ByVal objattAssetDetails As attAssetDetails, ByVal objattAssets As attItems, ByVal DeptID As String, ByVal chkChild As Boolean, ByVal FrmDt As String, ByVal ToDT As String, ByVal FrmDtEnabled As Boolean, ByVal CostFilterCriteria As String, ByVal CostFilterValue As Double) As DataTable
        Try
            Dim ds As New DataTable
            Dim objBALStandardReport As New BALStandardReports

            objBALStandardReport.Empty_CompanyAssets()
            ds = objBALStandardReport.CompanyAssets_Report_From(objattAssetDetails, objattAssets, DeptID, chkChild, FrmDt, ToDT, FrmDtEnabled, CostFilterCriteria, CostFilterValue)

            If ds.Rows.Count > 0 Then
                objBALStandardReport.Insert_CompanyAssets_Report(ds)
            End If

            ds = objBALStandardReport.Get_CompAsset()
            Return ds
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
            Return Nothing
        End Try
    End Function

    Private Function GetExpectedDepReportDataTable(ByVal ItemCode As String, ByVal CostFilterCriteria As String, ByVal CostFilterValue As Double, ByVal StatusID As Integer, ByVal InvStatus As Integer, ByVal FromDate As Date, ByVal ToDate As Date, ByVal DateFilterEnabled As Boolean) As DataTable
        Try
            Dim dt As New DataTable
            Dim Todate1 As Date
            Todate1 = dtExpectedDepreciation.Value
            'Dim dtFiscal As Date = New Date(Todate1.Year, Todate1.Month, Date.DaysInMonth(Todate1.Year, Todate1.Month))
            Dim dtFiscal As Date = Todate1.Date


            Dim dtSrv As Date
            Dim depValue As Double = 0.0
            Dim Price As Double
            'Dim CurBook  As Double

            dt = objBALStandardReport.ExpectedDepreciation_Book(cmbComp.SelectedValue, cmbBook.SelectedValue, _
                                       cmbCust.SelectedValue, ZTLocation.SelectedValue, _
                                        ZTCategory.SelectedValue, _
                                       chkSubLevels.Checked, cmbDepart.Tag, _
                                       IIf(chkDisposed.Checked, 1, 0), IIf(chkExclude.Checked, "MUST", ""), cmbBrand.SelectedValue, cmbSupp.SelectedValue, ItemCode, CostFilterCriteria, CostFilterValue, StatusID, InvStatus, FromDate, ToDate, DateFilterEnabled)

            dt.Columns.Add("CurrentBV", Type.GetType("System.Double"))
            dt.Columns.Add("acc", Type.GetType("System.Double"))

            For Each dtrAstBook As DataRow In dt.Rows
                Dim LocationFullCode As String = dtrAstBook("LocationFullPath")
                Dim LocCodeArr As String() = LocationFullCode.Split("\")

                If LocCodeArr.Length > 0 Then
                    dtrAstBook("Loc1") = LocCodeArr(0).Trim
                End If
                If LocCodeArr.Length > 1 Then
                    dtrAstBook("Loc2") = LocCodeArr(1).Trim
                End If
                If LocCodeArr.Length > 2 Then
                    dtrAstBook("Loc3") = LocCodeArr(2).Trim
                End If
                If LocCodeArr.Length > 3 Then
                    dtrAstBook("Loc4") = LocCodeArr(3).Trim
                End If
                If LocCodeArr.Length > 4 Then
                    dtrAstBook("Loc5") = LocCodeArr(4).Trim
                End If

                Dim CatFullCode As String = dtrAstBook("CatFullPath")
                Dim CatCodeArr As String() = CatFullCode.Split("\")
                If CatCodeArr.Length > 0 Then
                    dtrAstBook("Cat1") = CatCodeArr(0).Trim
                End If
                If CatCodeArr.Length > 1 Then
                    dtrAstBook("Cat2") = CatCodeArr(1).Trim
                End If
                If CatCodeArr.Length > 2 Then
                    dtrAstBook("Cat3") = CatCodeArr(2).Trim
                End If


                If Not dtrAstBook("SrvDate").ToString Is "" Then
                    dtSrv = dtrAstBook("SrvDate").ToString()
                End If

                If Date.Compare(dtSrv, dtFiscal).ToString() <= 0 Then
                    If dtrAstBook("SalvageYear") <> "0" Then
                        If dtrAstBook("SalvageYear") * 12 > DateDiff(DateInterval.Month, dtSrv, dtFiscal) Then
                            depValue = CalcDep(dtrAstBook("SalvageYear"), dtrAstBook("SalvageMonth"), dtrAstBook("DepCode"), dtrAstBook("CurBook"), dtrAstBook("SalvageValue"), dtFiscal, CDate(dtrAstBook("BVUpdate")), dtrAstBook("SrvDate"), dtrAstBook("tot"))
                        Else
                            depValue = CalcDep(dtrAstBook("SalvageYear"), dtrAstBook("SalvageMonth"), dtrAstBook("DepCode"), dtrAstBook("CurBook"), dtrAstBook("SalvageValue"), dtSrv.AddYears(dtrAstBook("SalvageYear")), CDate(dtrAstBook("BVUpdate")), dtrAstBook("SrvDate"), dtrAstBook("tot"))
                        End If

                        'if the assets depreciated for more than asset life then, depvalue will come in minus, that's why we need to remove it.
                        If depValue < 0 Then
                            depValue = 0
                        End If
                        Dim CurrentBV As Decimal = Round(dtrAstBook("CurBook") - depValue, 2)
                        If CurrentBV < dtrAstBook("SalvageValue") Then
                            CurrentBV = dtrAstBook("SalvageValue")
                        End If
                        Price = dtrAstBook("tot")
                        dtrAstBook("CurrentBV") = CurrentBV
                        dtrAstBook("acc") = Price - CurrentBV
                    End If
                End If
            Next
            Return dt
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
            Return Nothing
        End Try
    End Function

    Private Function GetStandardReportDataTable(ByVal ReportName As String) As DataTable
        Dim objattAssetDetails As New attAssetDetails
        Dim objattAssets As New attItems

        If ZTLocation.SelectedValue <> "" Then
            objattAssetDetails.LocID = ZTLocation.SelectedValue
        End If

        If cmbCust.SelectedValue <> "" Then
            objattAssetDetails.CustodianID = cmbCust.SelectedValue
        End If

        If cmbComp.SelectedValue <> "" Then
            objattAssetDetails.CompanyID = cmbComp.SelectedValue
        Else
            objattAssetDetails.CompanyID = 0
        End If

        If ZTCategory.SelectedValue <> "" Then
            objattAssets.AstCatID = ZTCategory.SelectedValue
        End If

        If cmbSupp.SelectedValue <> "" Then
            objattAssetDetails.SuppID = cmbSupp.SelectedValue
        End If

        If cmbBrand.SelectedValue <> "" Then
            objattAssets.AstBrandID = cmbBrand.SelectedValue
        Else
            objattAssets.AstBrandID = 0
        End If

        If chkDisposed.Checked Then
            objattAssetDetails.Disposed = 1
        Else
            objattAssetDetails.Disposed = 0
        End If

        If chkExclude.Checked Then
            objattAssetDetails.Disposed = 0
            objattAssetDetails.ExcludeDisposed = "MUST"
        End If

        If cmbDataSource.Text <> "ALL" Then
            objattAssetDetails.TransRemarks = cmbDataSource.Text
        Else
            objattAssetDetails.TransRemarks = ""
        End If

        If cmbItemCode.SelectedValue <> "" Then
            objattAssets.PKeyCode = cmbItemCode.SelectedValue
        Else
            objattAssets.PKeyCode = 0
        End If

        If cmbAssetStatus.SelectedValue <> "" Then
            objattAssetDetails.StatusID = cmbAssetStatus.SelectedValue
        Else
            objattAssetDetails.StatusID = 0
        End If

        If cmbInventoryStatus.EditValue Is Nothing Then
            objattAssetDetails.InvStatus = -1
        Else
            objattAssetDetails.InvStatus = cmbInventoryStatus.EditValue
        End If

        Dim CostFilterWay As String = cmbTotalCostFilterType.Text
        Dim CostFilterCriteria As String = ""
        Dim CostFilterValue As Double = txtTotalCost.Value

        Select Case CostFilterWay
            Case "None"
                CostFilterCriteria = 0
            Case "Equals"
                CostFilterCriteria = "="
            Case "Does not equal"
                CostFilterCriteria = "<>"
            Case "Is greater than"
                CostFilterCriteria = ">"
            Case "Is greater than or equal to"
                CostFilterCriteria = ">="
            Case "Is less than"
                CostFilterCriteria = "<"
            Case "Is less than or equal to"
                CostFilterCriteria = "<="
            Case Else
                CostFilterCriteria = 0
        End Select


        objattAssetDetails.BookID = cmbBook.SelectedValue
        Dim DeptID As String = cmbDepart.Tag
        Dim IncludeSubLevels As Boolean = chkSubLevels.Checked
        Select Case ReportName
            Case ReportNames.NewTags, ReportNames.CompanyAssets, ReportNames.AssetsLedger, ReportNames.AssetsTagging, ReportNames.AssetDetails
                Return objBALStandardReport.CompanyAssetReport(objattAssetDetails, objattAssets, DeptID, IncludeSubLevels, dtFrmDt.Value, dtToDT.Value, chkFilterByDate.Checked, CostFilterCriteria, CostFilterValue)
            Case ReportNames.AssetsLog
                Return objBALStandardReport.AssetsLogReport(objattAssetDetails, objattAssets, DeptID, IncludeSubLevels, dtFrmDt.Value, dtToDT.Value, chkFilterByDate.Checked, CostFilterCriteria, CostFilterValue)
            Case ReportNames.DisposedAssets
                objattAssetDetails.Disposed = 1
                Return objBALStandardReport.DisposedAssetReport(objattAssetDetails, objattAssets, DeptID, IncludeSubLevels, dtFromDisposal.Value, dtToDisposal.Value, cmdDisposal.SelectedValue, chkFilterByDisposeDate.Checked, CostFilterCriteria, CostFilterValue)
            Case ReportNames.ItemsInventory
                Return objBALStandardReport.Item_Inventory("", cmbComp.SelectedValue, cmbBook.SelectedValue, _
                                                   cmbCust.SelectedValue, ZTLocation.SelectedValue, _
                                                   ZTCategory.SelectedValue, _
                                                   chkSubLevels.Checked, cmbBrand.SelectedValue, _
                                                   cmbSupp.SelectedValue, cmbDepart.Tag, objattAssets.PKeyCode, CostFilterCriteria, CostFilterValue, objattAssetDetails.StatusID, objattAssetDetails.InvStatus)
            Case ReportNames.DepreciationBooks
                Return objBALStandardReport.Depreciation_Book("", cmbComp.SelectedValue, cmbBook.SelectedValue, _
                                                                          cmbCust.SelectedValue, ZTLocation.SelectedValue, _
                                                                          ZTCategory.SelectedValue, _
                                                                          IncludeSubLevels, cmbDepart.Tag, _
                                                                          IIf(chkDisposed.Checked, 1, 0), IIf(chkExclude.Checked, "MUST", ""), cmbBrand.SelectedValue, cmbSupp.SelectedValue, objattAssets.PKeyCode, CostFilterCriteria, CostFilterValue, objattAssetDetails.StatusID, objattAssetDetails.InvStatus, dtFrmDt.Value, dtToDT.Value, chkFilterByDate.Checked)
            Case ReportNames.ExpectedDepreciation
                Return GetExpectedDepReportDataTable(objattAssets.PKeyCode, CostFilterCriteria, CostFilterValue, objattAssetDetails.StatusID, objattAssetDetails.InvStatus, dtFrmDt.Value, dtToDT.Value, chkFilterByDate.Checked)
            Case ReportNames.AssetRegister
                Return GenAssetsRegisterDataTable(objattAssetDetails, objattAssets, DeptID, IncludeSubLevels, dtFrmDt.Value, dtToDT.Value, chkFilterByDate.Checked, CostFilterCriteria, CostFilterValue)
            Case ReportNames.AssetsbySubCategory
                Return objBALStandardReport.Get_Report_AssetbySubCategory()
            Case ReportNames.AssetsbyCategory
                Return objBALStandardReport.Get_Report_AssestsByCategroy()
            Case Else
                Return Nothing
        End Select

    End Function

    Private Sub Insert_CompanyAssets_SubCategory()
        objBALStandardReport.Empty_Report_AssetsBySubCategory()
        objBALStandardReport.Insert_Report_AssetsBySubCategory(dtFrom.SelectedText, dtTo.SelectedText)
    End Sub
    Private Sub Insert_CompanyAssets_MainCategory()
        objBALStandardReport.Empty_Report_AssetsByCategory()
        objBALStandardReport.Insert_Report_AssetsByCategory(dtFrom.SelectedText, dtTo.SelectedText)
    End Sub
#End Region

#Region "Button Clicks"

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click, btnClose.Click, btnCloseCat.Click
        FormController.objfrmRptFilter = Nothing
        Me.Dispose()
    End Sub

    Private Sub btnLOV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHierLOV.Click
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
        Else
            obj.DBServer = AppConfig.DbServer
        End If
        obj.DBUName = AppConfig.DbUname
        obj.OpenTree(cmbDepart)

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        cmbBook.SelectedIndex = -1
        cmbComp.SelectedIndex = -1
        cmbCust.SelectedIndex = -1
        cmbSupp.SelectedIndex = -1
        cmbBrand.SelectedIndex = -1

        cmbDepart.Text = ""
        cmbDepart.Tag = ""

        ZTLocation.SelectedText = ""
        ZTLocation.SelectedValue = ""
        ZTCategory.SelectedText = ""
        ZTCategory.SelectedValue = ""
        dtFrmDt.Enabled = False

    End Sub

    Private Sub txtBrandID_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBrand.LovBtnClick
        Try
            cmbBrand.ValueMember = "AstBrandID"
            cmbBrand.DisplayMember = "AstBrandName"
            Dim objBALBrand As New BALbrand
            cmbBrand.DataSource = objBALBrand.GetAll_Brand(New attBrand)
            cmbBrand.OpenLOV()
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
    Private Sub cmbComp_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbComp.LovBtnClick
        Try
            cmbComp.ValueMember = "CompanyID"
            cmbComp.DisplayMember = "CompanyName"
            Dim objBALCompany As New BALCompany
            cmbComp.DataSource = objBALCompany.GetAllData_GetCombo(New attcompany)
            cmbComp.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmbSupp_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSupp.LovBtnClick
        Try
            cmbSupp.ValueMember = "SuppID"
            cmbSupp.DisplayMember = "SuppName"
            Dim objBALSupplier As New BALSupplier
            cmbSupp.DataSource = objBALSupplier.GetAllData_GetCombo()
            cmbSupp.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmbBook_LovBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBook.LovBtnClick
        Try
            cmbBook.ValueMember = "BookID"
            cmbBook.DisplayMember = "Description"

            Dim objattBookTempl As New attBook
            If cmbComp.SelectedValue <> "" Then
                objattBookTempl.CompanyID = cmbComp.SelectedValue
            End If
            Dim objBALBookTempl As New BALBooks
            cmbBook.DataSource = objBALBookTempl.GetAll_Book(objattBookTempl)
            cmbBook.OpenLOV()
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

    Private Sub cmbLocationAudit_TreeBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLocationAudit.TreeBtnClick
        Try
            Dim ds As New DataTable
            Dim objBALLocation As New BALLocation

            cmbLocationAudit.DataSource = objBALLocation.GetComboLocations(New attLocation)
            cmbLocationAudit.OpenLOV()
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

    Private Sub cmbCatAuditBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCatAudit.TreeBtnClick
        Try
            Dim ds As New DataTable
            Dim objBALCategory As New BALCategory
            cmbCatAudit.DataSource = objBALCategory.GetAll_Category(New attCategory)
            cmbCatAudit.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub btnGenAuditStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenAuditStatus.Click
        If valProvInventory.Validate Then
            If ReportName <> "" Then
                Dim rpt As XtraReport = LoadReports.LoadAuditReport(ReportName, cmbSch.SelectedValue, cmbSch.SelectedText, lblStart.Text, lblEnd.Text, False, cmbLocationAudit.SelectedValue, cmbCatAudit.SelectedValue, chkIncludeSubAudit.Checked)
                If rpt IsNot Nothing Then
                    ShowReport(rpt)
                    tabControl.SelectedTabPage = tabPreviewReport
                End If
            End If
        End If
    End Sub

    Private Sub btnGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGen.Click
        If ReportName <> "" Then
            Dim ds As DataTable = GetStandardReportDataTable(ReportName)
            LoadStandardReport(ReportName, ds)
            tabControl.SelectedTabPage = tabPreviewReport
        Else
            ZulMessageBox.ShowMe("SelectReportFirst", MessageBoxButtons.OK, MessageBoxIcon.Error, True)
        End If
    End Sub

    Private Sub cmbSch_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSch.LovBtnClick
        Try
            Dim objBALAst_INV_Schedule As New BALAst_INV_Schedule
            cmbSch.ValueMember = "InvSchCode"
            cmbSch.DisplayMember = "InvDesc"
            cmbSch.DataSource = objBALAst_INV_Schedule.Getcombo_Schedule()
            cmbSch.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub btnRefreshCat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshCat.Click
        valProvInventory.RemoveControlError(dtFrom.TextBox)
        valProvInventory.RemoveControlError(dtTo.TextBox)
        dtFrom.SelectedText = ""
        dtFrom.SelectedValue = ""
        dtTo.SelectedText = ""
        dtTo.SelectedValue = ""
    End Sub

    Private Sub btnGenCatReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenCatReport.Click
        If valProvCategory.Validate Then
            If ReportName = ReportNames.AssetsbyCategory Then
                Insert_CompanyAssets_MainCategory()
            ElseIf ReportName = ReportNames.AssetsbySubCategory Then
                Insert_CompanyAssets_SubCategory()
            End If
            Dim ds As DataTable = GetStandardReportDataTable(ReportName)
            LoadStandardReport(ReportName, ds)
            tabControl.SelectedTabPage = tabPreviewReport
        End If
    End Sub

    Private Sub dtFrom_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtFrom.LovBtnClick
        Try
            dtFrom.ValueMember = "updDate"
            dtFrom.DisplayMember = "updDate"
            Dim objBALDepLogs As New BALDepLogs
            dtFrom.DataSource = objBALDepLogs.GetAll_DepLogs(New attDepLogs)
            dtFrom.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub dtTo_LovBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtTo.LovBtnClick
        Try
            dtTo.ValueMember = "updDate"
            dtTo.DisplayMember = "updDate"
            Dim objBALDepLogs As New BALDepLogs
            dtTo.DataSource = objBALDepLogs.GetAll_DepLogs(New attDepLogs)
            dtTo.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmdDisposal_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDisposal.LovBtnClick
        Try
            cmdDisposal.ValueMember = "DispCode"
            cmdDisposal.DisplayMember = "DispDesc"
            Dim objBALDisposalMethod As New BALDisposalMethod
            cmdDisposal.DataSource = objBALDisposalMethod.GetAll_DisposalMethod(New attDisposalMethod)
            cmdDisposal.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmbSch_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSch.SelectTextChanged
        Try
            valProvInventory.RemoveControlError(cmbSch.TextBox)
            If cmbSch.SelectedValue <> "" Then
                Dim dt As New DataTable
                Dim objattInvSchedule As New attInvSchedule
                Dim objBALAst_INV_Schedule1 As New BALAst_INV_Schedule
                objattInvSchedule.PKeyCode = cmbSch.SelectedValue
                dt = objBALAst_INV_Schedule1.GetAll_invSchID(cmbSch.SelectedValue)
                lblStart.Text = ""
                lblEnd.Text = ""
                If dt Is Nothing = False Then
                    If dt.Rows.Count > 0 Then
                        lblStart.Text = dt.Rows(0)("InvStartDate")
                        lblEnd.Text = dt.Rows(0)("InvEndDate")
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

#End Region

#Region "Load Reports"


    Public Function LoadStandardReport(ByVal ReportName As String, ByVal rptDS As DataTable) As XtraReport
        Dim objBALOrgHier As New BALOrgHier
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
            rpt.DataSource = rptDS
            rpt.PrinterName = AppConfig.ReportPrinter
            rpt.PrintingSystem.ShowMarginsWarning = False
            rpt.FindControl("lblCompName", False).Text = AppConfig.CompanyName
            CType(rpt.FindControl("picLogo", False), XRPictureBox).Image = CompanyLogoImage

            Dim fmt As String
            If AppConfig.MaindateFormat = "dd/MM/yyyy" Then
                fmt = "{0:dd/MM/yyyy}"
            Else
                fmt = "{0:MM/dd/yyyy}"
            End If
            Select Case ReportName
                Case ReportNames.DepreciationBooks
                    rpt.FindControl("lblRptHead", False).Text = "Actual Depreciation"
                    rpt.FindControl("lblFooter", False).Text = "Actual Depreciation"
                Case ReportNames.ItemsInventory
                    rpt.FindControl("lblRptHead", False).Text = "Items Inventory"
                    rpt.FindControl("lblFooter", False).Text = "Items Inventory"
                Case ReportNames.AssetsTagging
                    If rpt.FindControl("tblCellPurDate", False) IsNot Nothing Then
                        Dim binding As New XRBinding("Text", rpt.DataSource, "AssetTag.PurDate", fmt)
                        rpt.FindControl("tblCellPurDate", False).DataBindings.Add(binding)
                    End If
                Case ReportNames.AssetsLedger
                    If rpt.FindControl("tblCellPurDate", False) IsNot Nothing Then
                        Dim binding As New XRBinding("Text", rpt.DataSource, "AssetLedger.PurDate", fmt)
                        rpt.FindControl("tblCellPurDate", False).DataBindings.Add(binding)
                    End If
                Case ReportNames.AssetRegister
                    If rpt.FindControl("tblCellPurDate", False) IsNot Nothing Then
                        Dim binding As New XRBinding("Text", rpt.DataSource, "CompanyAssets.PurDate", fmt)
                        rpt.FindControl("tblCellPurDate", False).DataBindings.Add(binding)
                    End If
                Case ReportNames.ExpectedDepreciation
                    rpt.FindControl("lblRptHead", False).Text = "Expected Depreciation"
                    rpt.FindControl("lblFooter", False).Text = "Expected Depreciation"
                Case ReportNames.AssetsbyCategory, ReportNames.AssetsbySubCategory
                    If Trim(dtFrom.SelectedText) = "" And Trim(dtTo.SelectedText) = "" Then
                        rpt.FindControl("pnlDate", False).Visible = False
                    Else
                        rpt.FindControl("lblSDate", False).Text = CDate(dtFrom.SelectedText).Month.ToString() & " - " & CDate(dtFrom.SelectedText).Year.ToString()
                        rpt.FindControl("lblEDate", False).Text = CDate(dtTo.SelectedText).Month.ToString() & " - " & CDate(dtTo.SelectedText).Year.ToString()
                    End If
                Case ReportNames.NewTags
                    'ZulAssetsBEDataSet1 - CompanyAssets.PurDate
                    If rpt.FindControl("tblCellPurDate", False) IsNot Nothing Then
                        Dim binding As New XRBinding("Text", rpt.DataSource, "CompanyAssets.PurDate", fmt)
                        rpt.FindControl("tblCellPurDate", False).DataBindings.Add(binding)
                    End If
                Case ReportNames.DisposedAssets
                    If rpt.FindControl("tblCellDispDate", False) IsNot Nothing Then
                        Dim binding As New XRBinding("Text", rpt.DataSource, "DisposedAssets.DispDate", fmt)
                        rpt.FindControl("tblCellDispDate", False).DataBindings.Add(binding)
                    End If
                    If rpt.FindControl("tblCelSel_Date", False) IsNot Nothing Then
                        Dim binding As New XRBinding("Text", rpt.DataSource, "DisposedAssets.Sel_Date", fmt)
                        rpt.FindControl("tblCelSel_Date", False).DataBindings.Add(binding)
                    End If
            End Select

            If rpt.FindControl("lblSubCompName", False) IsNot Nothing Then
                If cmbComp.SelectedText <> "" Then ' sub company
                    rpt.FindControl("lblSubCompName", False).Text = cmbComp.SelectedText
                Else
                    rpt.FindControl("lblSubCompName", False).Text = ""
                End If
            End If

            If rpt.FindControl("lblFilterText", False) IsNot Nothing Then

                rpt.FindControl("lblFilterText", False).Text = ""
                rpt.FindControl("lblFilterValue", False).Text = ""


                If cmbBook.SelectedText <> "" Then ' book
                    rpt.FindControl("lblFilterText", False).Text += "Book: " & Environment.NewLine
                    rpt.FindControl("lblFilterValue", False).Text += cmbBook.SelectedText & Environment.NewLine
                End If

                If cmbDepart.Text <> "" Then 'Department
                    rpt.FindControl("lblFilterText", False).Text += "Department: " & Environment.NewLine
                    rpt.FindControl("lblFilterValue", False).Text += objBALOrgHier.HierName(cmbDepart.Tag) & Environment.NewLine
                End If

                If cmbCust.SelectedText <> "" Then ' Custodian
                    rpt.FindControl("lblFilterText", False).Text += "Custodian: " & Environment.NewLine
                    rpt.FindControl("lblFilterValue", False).Text += cmbCust.SelectedText & Environment.NewLine
                End If

                If Trim(ZTLocation.SelectedText) <> "" Then 'Location
                    rpt.FindControl("lblFilterText", False).Text += "Location: " & Environment.NewLine
                    rpt.FindControl("lblFilterValue", False).Text += ZTLocation.SelectedText & Environment.NewLine
                End If

                If ZTCategory.SelectedText <> "" Then 'Category
                    rpt.FindControl("lblFilterText", False).Text += "Category: " & Environment.NewLine
                    rpt.FindControl("lblFilterValue", False).Text += ZTCategory.SelectedText & Environment.NewLine
                End If

                If Trim(cmbBrand.SelectedText) <> "" Then 'Brand
                    rpt.FindControl("lblFilterText", False).Text += "Brand: " & Environment.NewLine
                    rpt.FindControl("lblFilterValue", False).Text += cmbBrand.SelectedText & Environment.NewLine
                End If

                If chkFilterByDate.Checked Then
                    rpt.FindControl("lblFilterText", False).Text += "From Date: " & Environment.NewLine
                    rpt.FindControl("lblFilterValue", False).Text += dtFrmDt.Value.Date & Environment.NewLine

                    rpt.FindControl("lblFilterText", False).Text += "To Date: " & Environment.NewLine
                    rpt.FindControl("lblFilterValue", False).Text += dtToDT.Value.Date & Environment.NewLine
                End If

                If ReportName = ReportNames.DisposedAssets Then
                    If Trim(cmdDisposal.SelectedText) <> "" Then 'Disposal Method
                        rpt.FindControl("lblFilterText", False).Text += "Method: " & Environment.NewLine
                        rpt.FindControl("lblFilterValue", False).Text += cmdDisposal.SelectedText & Environment.NewLine
                    End If


                    If chkFilterByDisposeDate.Checked Then
                        rpt.FindControl("lblFilterText", False).Text += "From Date: " & Environment.NewLine
                        rpt.FindControl("lblFilterValue", False).Text += dtFromDisposal.Value.Date & Environment.NewLine

                        rpt.FindControl("lblFilterText", False).Text += "To Date: " & Environment.NewLine
                        rpt.FindControl("lblFilterValue", False).Text += dtToDisposal.Value.Date & Environment.NewLine
                    End If
                End If

                If Trim(cmbSupp.SelectedText) <> "" Then ' Supplier
                    rpt.FindControl("lblFilterText", False).Text += "Supplier: " & Environment.NewLine
                    rpt.FindControl("lblFilterValue", False).Text += cmbSupp.SelectedText
                End If

            End If

            ShowReport(rpt)
            Return rpt
        Else
            ZulMessageBox.ShowMe("ReportNotFound", MessageBoxButtons.OK, MessageBoxIcon.Error, True)
            Return Nothing
        End If
    End Function



    Private Sub ShowReport(ByVal rpt As DevExpress.XtraReports.UI.XtraReport)
        PreviewControl1.PrintingSystem1.ClearContent()
        rpt.PrintingSystem = PreviewControl1.PrintingSystem1
        rpt.CreateDocument(True)
        'Dim i As String = PreviewControl1.PrintingSystem1.PageMargins.Right
        'To be able to handle the Close Preview.
        SelectedTabPageIndex = tabControl.SelectedTabPageIndex
        PreviewControl1.PrintingSystem1.AddCommandHandler(New MyCommandHandler(nvControl, tabControl, tabControl.SelectedTabPageIndex))
        nvControl.Visible = False
    End Sub

    Public Class MyCommandHandler
        Implements DevExpress.XtraPrinting.Native.ICommandHandler
        Dim nvBarControl As DevExpress.XtraNavBar.NavBarControl
        Dim TabControl As DevExpress.XtraTab.XtraTabControl
        Dim tabIndex As Integer
        Public Sub New(ByVal nvControl As DevExpress.XtraNavBar.NavBarControl, ByVal tab As DevExpress.XtraTab.XtraTabControl, ByVal index As Integer)
            nvBarControl = nvControl
            TabControl = tab
            tabIndex = index
        End Sub

        Public Function CanHandleCommand(ByVal command As DevExpress.XtraPrinting.PrintingSystemCommand, ByVal printControl As DevExpress.XtraPrinting.Control.PrintControl) As Boolean Implements DevExpress.XtraPrinting.Native.ICommandHandler.CanHandleCommand
            Return True
        End Function

        Public Sub HandleCommand(ByVal command As DevExpress.XtraPrinting.PrintingSystemCommand, ByVal args() As Object, ByVal printControl As DevExpress.XtraPrinting.Control.PrintControl, ByRef handled As Boolean) Implements DevExpress.XtraPrinting.Native.ICommandHandler.HandleCommand
            If command = DevExpress.XtraPrinting.PrintingSystemCommand.ClosePreview Then
                nvBarControl.Visible = True
                TabControl.SelectedTabPageIndex = tabIndex
                handled = True
            End If
        End Sub
    End Class
#End Region

    Private Sub btnRefreshExtended_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshExtended.Click
        GetExtendedReportList()
    End Sub

    Private Sub itmCode_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbItemCode.LovBtnClick
        Try
            cmbItemCode.ValueMember = "ItemCode"
            cmbItemCode.DisplayMember = "AstDesc"
            Dim objBALAssets As New BALItems
            cmbItemCode.DataSource = objBALAssets.GetAllData_GetCombo(New attItems)
            cmbItemCode.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub ItmCode_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItemCode.SelectTextChanged
        Try
            If cmbItemCode.SelectedText <> "" Then
                Dim objBALAssets As New BALItems
                Dim objattAssets As attItems
                objattAssets = New attItems
                objattAssets.PKeyCode = cmbItemCode.SelectedValue
                Dim dt As DataTable = objBALAssets.GetAllData_Joined(objattAssets)
                If dt Is Nothing = False Then
                    If dt.Rows.Count > 0 Then
                        If Not (Convert.ToString(dt.Rows(0)(7)) Is Nothing) Then
                            ZTCategory.TextBox.Tag = Convert.ToString(dt.Rows(0)(7))
                            Dim objBALCategory As New BALCategory
                            ZTCategory.TextBox.Text = objBALCategory.Comp_Path(ZTCategory.TextBox.Tag)
                        End If
                        'txtItemDesc.Text = Convert.ToString(dt.Rows(0)(2))
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub txtAssetStatus_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAssetStatus.LovBtnClick
        Try
            Dim objBALAssetDetails As New BALAssetDetails
            cmbAssetStatus.ValueMember = "ID"
            cmbAssetStatus.DisplayMember = "Status"
            cmbAssetStatus.DataSource = objBALAssetDetails.GetAssetStatus(False, True)
            cmbAssetStatus.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmbInventoryStatus_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmbInventoryStatus.ButtonClick
        If e.Button.Index = 1 Then
            cmbInventoryStatus.SelectedIndex = -1
        End If
    End Sub
End Class