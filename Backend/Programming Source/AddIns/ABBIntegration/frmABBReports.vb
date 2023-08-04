Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports
Imports DevExpress.XtraNavBar
Imports System.Windows.Forms
Imports ABBIntegration.LoadReports
Imports System.Drawing

Public Class frmABBReports
    Private ReportName As String = ""
    Dim objBALStandardReport As New BALABBIntegration
    Public CompanyLogoImage As Image
    Private _RoleID As Integer
    Public Property RoleID() As Integer
        Get
            Return _RoleID
        End Get
        Set(ByVal value As Integer)
            _RoleID = value
        End Set
    End Property


    Private Sub frmABBReports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG
        Me.BackgroundImageLayout = ImageLayout.Stretch

        tabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
        tabControl.SelectedTabPage = tabMainFilter

        SetReportsPermissions()

        dtFromCreation.CustomFormat = AppConfig.MaindateFormat
        dtToCreation.CustomFormat = AppConfig.MaindateFormat
        'Expanded all groups in the navigation menu.
        For Each nvGroup As NavBarGroup In nvControl.Groups
            nvGroup.Expanded = True
        Next
        nvGroupStandard.SelectedLinkIndex = 0
    End Sub


    Private Sub SetReportsPermissions()
        Dim ds As DataTable
        Dim objattRole As attRoles = New attRoles
        Dim objBALRole As New BALRoles
        objattRole.PKeyCode = RoleID
        ds = objBALRole.GetAll_Roles(objattRole)
        Dim ShowInventoryReports As Boolean = ds.Rows(0)("Units")
        Dim ShowAuditingReports As Boolean = ds.Rows(0)("Insurer")

        If Not ShowAuditingReports Then
            nvGroupStandard.Visible = False
            nvItemStandard_LinkClicked(nvGoodsReciveDetail, New DevExpress.XtraNavBar.NavBarLinkEventArgs(nvGoodsReciveDetail.Links(0)))
        Else
            nvItemStandard_LinkClicked(nvItemCompanyAsset, New DevExpress.XtraNavBar.NavBarLinkEventArgs(nvItemCompanyAsset.Links(0)))
        End If

        If Not ShowInventoryReports Then
            nvInventoryReports.Visible = False
        End If
    End Sub

    Public Shared Function Round(ByVal Number As Double, Optional ByVal NumDigitsAfterDecimal As Integer = 0) As Double
        Return CDbl(FormatNumber(Number, NumDigitsAfterDecimal))
    End Function


    Private Sub nvItemStandard_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nvItemCompanyAsset.LinkClicked, nvItemMisplaced.LinkClicked, nvItemPhysicalInventory.LinkClicked, nvTransferedAssets.LinkClicked, nvMissingAssets.LinkClicked, nvFoundAssets.LinkClicked, ntAssetLog.LinkClicked
        tabControl.SelectedTabPage = tabMainFilter
        lblReportName.Text = e.Link.Caption
        PictureBox1.Image = e.Link.GetImage
        Me.Text = String.Format("{0} - {1}", e.Link.Group.Caption, e.Link.Caption)
        ReportName = e.Link.Caption
        If ReportName = ReportsName.AssetsLog Then
            lblInventory.Visible = False
            cmbSch.Visible = False
        Else
            lblInventory.Visible = True
            cmbSch.Visible = True
        End If
        'cmbSch.Visible = True
        'lblInventory.Visible = True
        chkShowRetired.Checked = False
        chkShowOnlyAssetWithValue.Checked = False
    End Sub

    Private Sub nvAssetswithvlaue_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nvItemDisposed.LinkClicked, nvAssetswithvlaue.LinkClicked
        tabControl.SelectedTabPage = tabMainFilter
        lblReportName.Text = e.Link.Caption
        PictureBox1.Image = e.Link.GetImage
        Me.Text = String.Format("{0} - {1}", e.Link.Group.Caption, e.Link.Caption)
        ReportName = e.Link.Caption
        cmbSch.SelectedValue = String.Empty
        cmbSch.SelectedText = String.Empty
        cmbSch.Visible = False
        lblInventory.Visible = False
        If ReportName = ReportsName.AssetWithValue Then
            chkShowOnlyAssetWithValue.Checked = True
        Else
            chkShowOnlyAssetWithValue.Checked = False
        End If

        If ReportName = ReportsName.AssetRetirement Then
            chkShowRetired.Checked = True
        Else
            chkShowRetired.Checked = False
        End If
    End Sub

    Private Sub nvItemAnonymous_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nvItemAnonymous.LinkClicked
        tabControl.SelectedTabPage = tabAnonymousFilter
        lblAuditStatus.Text = e.Link.Caption
        imgAuditStatus.Image = e.Link.GetImage
        Me.Text = String.Format("{0} - {1}", e.Link.Group.Caption, e.Link.Caption)
        ReportName = e.Link.Caption
    End Sub


#Region "Get Reports Data"

    Private Function GetAnonymousReportDataTable(ByVal InvSchID As String) As DataTable
        Dim objBALIntegration As New BALABBIntegration
        Dim objBALLocation As New BALLocation
        Dim objBALDevices As New BALDevices
        Dim objBALCategory As New BALCategory
        Dim dt As DataTable
        dt = objBALIntegration.GetAll_AnonymousAssetRemarks(InvSchID)

        Dim dcPrevLoc As DataColumn = dt.Columns.Add("Plant", Type.GetType("System.String"))
        Dim dcCurrentLoc As DataColumn = dt.Columns.Add("Location", Type.GetType("System.String"))
        Dim dcCat As DataColumn = dt.Columns.Add("CatFullPath", Type.GetType("System.String"))
        For i As Integer = 0 To dt.Rows.Count - 1
            If Not String.IsNullOrEmpty(dt.Rows(i)("LocID").ToString) Then
                Dim FullLocation As String = objBALLocation.GetLocFullPath(dt.Rows(i)("LocID").ToString)
                Dim strFulLoc As String() = FullLocation.Split("\")
                If strFulLoc(0) = "UnKnown" Then
                    dt.Rows(i)("Plant") = String.Empty
                Else
                    dt.Rows(i)("Plant") = strFulLoc(0).Trim
                End If

                If strFulLoc.Length > 1 Then
                    If strFulLoc(1) = "UnKnown" Then
                        dt.Rows(i)("Location") = String.Empty
                    Else
                        dt.Rows(i)("Location") = strFulLoc(1).Trim
                    End If
                Else
                    dt.Rows(i)("Location") = String.Empty
                End If
            End If
            dt.Rows(i)("CatFullPath") = objBALCategory.GetCatFullPath(dt.Rows(i)("AstCatID").ToString)
        Next
        Return dt
    End Function

    Private Function GetStandardReportDataTable(ByVal ReportName As String) As DataTable
        Dim Filter As New ZulAssetsDAL.ZulAssetsDAL.ABBIntegration.FilterOptions
        If trvLocation.SelectedValue <> "" Then
            Filter.LocID = trvLocation.SelectedValue
        End If
        If cmbCust.SelectedValue <> "" Then
            Filter.CustodianID = cmbCust.SelectedValue
        End If

        If cmbComp.SelectedValue <> "" Then
            Filter.CompanyID = cmbComp.SelectedValue
        Else
            Filter.CompanyID = 0
        End If

        If cmbCostCenter.SelectedValue <> "" Then
            Filter.CostCenterID = cmbCostCenter.SelectedValue
        Else
            Filter.CostCenterID = String.Empty
        End If

        If trvCategory.SelectedValue <> "" Then
            Filter.AssetClassID = trvCategory.SelectedValue
        End If
        Filter.InventoryNumber = txtInvestmentNo.Text.Trim
        Filter.BussinessArea = txtBusinessArea.Text.Trim
        Filter.EvaluationGroup1 = txtGroup1.Text.Trim
        Filter.EvaluationGroup2 = txtGroup2.Text.Trim
        Filter.EvaluationGroup3 = txtGroup3.Text.Trim
        Filter.EvaluationGroup4 = txtGroup4.Text.Trim


        If cmbDataSource.Text <> "ALL" Then
            Filter.DataSource = cmbDataSource.Text
        Else
            Filter.DataSource = String.Empty
        End If

        Filter.IncludeSubLevels = chkShowSubLevels.Checked

        If Not String.IsNullOrEmpty(cmbSch.SelectedText) Then
            Filter.InvSchID = cmbSch.SelectedValue
        Else
            Filter.InvSchID = -1
        End If
        Filter.FilterByCreation = chkFilterByCreation.Checked
        Filter.CreationFromDate = dtFromCreation.Value.Date
        Filter.CreationToDate = dtToCreation.Value.Date
        Filter.FilterByCapitalize = chkFilterByCapitalize.Checked
        Filter.CapitalizestartDate = dtFromCapitalize.Value.Date
        Filter.CapitalizeEndDate = dtToCapitalize.Value.Date
        Filter.FilterByRetirement = chkFilterByDisposeDate.Checked
        Filter.RetirementstartDate = dtFromDisposal.Value.Date
        Filter.RetirementEndDate = dtToDisposal.Value.Date
        Filter.FromAssetNumber = txtFromAssetNumber.Text.Trim
        Filter.ToAssetNumber = txtToAsetNumber.Text.Trim

        If chkShowRetired.Checked Then
            Filter.ShowOnlyRetiredAssets = True
        ElseIf chkExcludeRetired.Checked Then
            Filter.ExcludeRetiredAssets = True
        End If

        If chkShowOnlyAssetWithValue.Checked Then
            Filter.ShowOnlyAssetValue = True
        ElseIf chkExcludeAssetValue.Checked Then
            Filter.ExcludeAssetValue = True
        End If

        If chkShowOnlyCapitalization.Checked Then
            Filter.ShowOnlyCapitalizedAssets = True
        ElseIf chkExcludeCapitalize.Checked Then
            Filter.ExcludeCapitalizedAssets = True
        End If



        Dim dt As DataTable

        Select Case ReportName
            Case ReportsName.CompanyAssets, ReportsName.AssetWithValue, ReportsName.AssetRetirement
                Filter.Status = String.Empty
            Case ReportsName.PhysicalInventory
                Filter.Status = "0,1,2,3"
            Case ReportsName.FoundAssets
                Filter.Status = "1"
            Case ReportsName.MissingAssets
                Filter.Status = "0"
            Case ReportsName.MisplacedAssets
                Filter.Status = "2"
            Case ReportsName.TransferredAssets
                Filter.Status = "3"
            Case Else
                Filter.Status = String.Empty
        End Select

        If ReportName = ReportsName.AssetsLog Then
            dt = objBALStandardReport.AssetsLogReport(Filter)
            If dt IsNot Nothing Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("Field") = "LocID" Then
                        dt.Rows(i)("OldValue") = dt.Rows(i)("OldLocationDesc")
                        dt.Rows(i)("NewValue") = dt.Rows(i)("NewLocationDesc")
                    End If
                Next
            End If
        Else
            dt = objBALStandardReport.CompanyAssetReportABB(Filter)
            'Get the locations according to ABB Format.
            If dt IsNot Nothing Then
                dt.Columns.Add("FromPlant", Type.GetType("System.String"))
                dt.Columns.Add("ToPlant", Type.GetType("System.String"))
                dt.Columns.Add("FromLocation", Type.GetType("System.String"))
                dt.Columns.Add("ToLocation", Type.GetType("System.String"))
                dt.Columns.Add("OldCustodianID", Type.GetType("System.String"))
                For i As Integer = 0 To dt.Rows.Count - 1
                    If Filter.InvSchID > -1 And ReportName <> ReportsName.AssetsLog Then
                        If Not String.IsNullOrEmpty(dt.Rows(i)("ToFullLocation").ToString) Then
                            Dim ToFullLocation As String = dt.Rows(i)("ToFullLocation").ToString

                            dt.Rows(i)("OldCustodianID") = String.Empty

                            Dim strToFulLoc As String() = ToFullLocation.Split("\")
                            If strToFulLoc(0) = "UnKnown" Then
                                dt.Rows(i)("ToPlant") = String.Empty
                            Else
                                dt.Rows(i)("ToPlant") = strToFulLoc(0).Trim
                            End If

                            If strToFulLoc.Length > 1 Then
                                If strToFulLoc(1) = "UnKnown" Then
                                    dt.Rows(i)("ToLocation") = String.Empty
                                Else
                                    dt.Rows(i)("ToLocation") = strToFulLoc(1).Trim
                                End If
                            Else
                                dt.Rows(i)("ToLocation") = String.Empty
                            End If
                        End If

                        Dim FromFullLocation As String = dt.Rows(i)("FromFullLocation").ToString
                        If Not String.IsNullOrEmpty(FromFullLocation) Then
                            Dim strFromFulLoc As String() = FromFullLocation.Split("\")
                            If strFromFulLoc(0) = "UnKnown" Then
                                dt.Rows(i)("FromPlant") = String.Empty
                            Else
                                dt.Rows(i)("FromPlant") = strFromFulLoc(0).Trim
                            End If

                            If strFromFulLoc.Length > 1 Then
                                If strFromFulLoc(1) = "UnKnown" Then
                                    dt.Rows(i)("FromLocation") = String.Empty
                                Else
                                    dt.Rows(i)("FromLocation") = strFromFulLoc(1).Trim
                                End If
                            Else
                                dt.Rows(i)("FromLocation") = String.Empty
                            End If
                        End If
                    Else
                        If Not String.IsNullOrEmpty(dt.Rows(i)("LocationFullPath").ToString) Then
                            Dim ToFullLocation As String = dt.Rows(i)("LocationFullPath").ToString

                            dt.Rows(i)("OldCustodianID") = String.Empty

                            Dim strToFulLoc As String() = ToFullLocation.Split("\")
                            If strToFulLoc(0) = "UnKnown" Then
                                dt.Rows(i)("ToPlant") = String.Empty
                            Else
                                dt.Rows(i)("ToPlant") = strToFulLoc(0).Trim
                            End If

                            If strToFulLoc.Length > 1 Then
                                If strToFulLoc(1) = "UnKnown" Then
                                    dt.Rows(i)("ToLocation") = String.Empty
                                Else
                                    dt.Rows(i)("ToLocation") = strToFulLoc(1).Trim
                                End If
                            Else
                                dt.Rows(i)("ToLocation") = String.Empty
                            End If
                        End If
                    End If
                Next
            End If
        End If

        Return dt
    End Function

#End Region

#Region "Button Clicks"

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click, btnCloseAnon.Click, Button3.Click, Button1.Click
        Me.Close()
    End Sub


    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        cmbComp.SelectedIndex = -1
        cmbCostCenter.SelectedIndex = -1
        cmbCust.SelectedIndex = -1
        trvLocation.SelectedText = ""
        trvLocation.SelectedValue = ""
        trvCategory.SelectedText = ""
        trvCategory.SelectedValue = ""
        txtPlant.Text = String.Empty
        txtLocation.Text = String.Empty
        txtFromAssetNumber.Text = String.Empty
        txtToAsetNumber.Text = String.Empty
        txtInvestmentNo.Text = String.Empty
        txtBusinessArea.Text = String.Empty
        txtGroup1.Text = String.Empty
        txtGroup2.Text = String.Empty
        txtGroup3.Text = String.Empty
        txtGroup4.Text = String.Empty

        dtFromCapitalize.Value = Now.Date
        dtToCapitalize.Value = Now.Date
        dtFromCreation.Value = Now.Date
        dtToCreation.Value = Now.Date
        dtFromDisposal.Value = Now.Date
        dtToDisposal.Value = Now.Date

        chkExcludeAssetValue.Checked = False
        chkExcludeCapitalize.Checked = False
        chkExcludeRetired.Checked = False
        chkFilterByCapitalize.Checked = False
        chkFilterByCreation.Checked = False
        chkFilterByDisposeDate.Checked = False
        chkShowOnlyAssetWithValue.Checked = False
        chkShowOnlyCapitalization.Checked = False
        chkShowRetired.Checked = False
        chkShowSubLevels.Checked = False

        chkHideReportFooter.Checked = False
        chkHideReportHeader.Checked = False
        cmbDataSource.SelectedIndex = -1
        cmbSch.SelectedValue = String.Empty
        cmbSch.SelectedText = String.Empty
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


    Private Sub cmbCostCenter_LovBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCostCenter.LovBtnClick
        Try
            Dim objBALCost As New BALCostCenter
            cmbCostCenter.ValueMember = "CostID"
            cmbCostCenter.DisplayMember = "CostName"
            cmbCostCenter.DataSource = objBALCost.GetAllData_GetCombo(New attCostCenter)
            cmbCostCenter.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub trvLocation_TreeBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvLocation.TreeBtnClick
        Try
            Dim ds As New DataTable
            Dim objBALLocation As New BALLocation
            trvLocation.DataSource = objBALLocation.GetComboLocations(New attLocation)
            trvLocation.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub ZTCategory_TreeBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvCategory.TreeBtnClick
        Try
            Dim ds As New DataTable
            Dim objBALCategory As New BALCategory
            trvCategory.DataSource = objBALCategory.GetAll_Category(New attCategory)
            trvCategory.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub btnGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGen.Click
        If ReportName <> "" Then
            Dim ds As DataTable = GetStandardReportDataTable(ReportName)
            Select Case ReportName
                Case ReportsName.CompanyAssets
                    LoadReport(ds, New rptCompanyAssets, ReportName)
                Case ReportsName.AssetWithValue
                    LoadReport(ds, New rptAssetValue, ReportName)
                Case ReportsName.PhysicalInventory, ReportsName.MissingAssets, ReportsName.FoundAssets
                    LoadReport(ds, New rptPhysicalInventory, ReportName)
                Case ReportsName.AssetRetirement
                    LoadReport(ds, New rptAssetRetirement, ReportName)
                Case ReportsName.MisplacedAssets, ReportsName.TransferredAssets
                    LoadReport(ds, New rptMisplacedAssets, ReportName)
                Case ReportsName.AssetsLog
                    LoadReport(ds, New rptAssetsLog, ReportName)
            End Select
            tabControl.SelectedTabPage = tabPreviewReport
        End If
    End Sub



#End Region

#Region "Load Reports"

    Private Function LoadReport(ByVal rptDS As DataTable, ByVal rpt As XtraReport, ByVal ReportName As String) As XtraReport
        rpt.DataSource = rptDS
        rpt.PrinterName = AppConfig.ReportPrinter
        rpt.PrintingSystem.ShowMarginsWarning = False
        rpt.FindControl("lblCompName", False).Text = AppConfig.CompanyName

        rpt.FindControl("lblReportNameHeader", False).Text = ReportName
        rpt.FindControl("lblReportNameFooter", False).Text = ReportName

        If ReportName = ReportsName.MissingAssets AndAlso rpt.Bands("grpMissing") IsNot Nothing Then
            rpt.Bands("grpMissing").Visible = False
        End If

        If ReportName = ReportsName.MissingAssets AndAlso rpt.Bands("grpFound") IsNot Nothing Then
            rpt.Bands("grpFound").Visible = False
        End If

        If ReportName = ReportsName.MissingAssets AndAlso rpt.Bands("grpTrans") IsNot Nothing Then
            rpt.Bands("grpTrans").Visible = False
        End If

        If ReportName = ReportsName.MissingAssets AndAlso rpt.Bands("grpMissplaced") IsNot Nothing Then
            rpt.Bands("grpMissplaced").Visible = False
        End If

        CType(rpt.FindControl("picLogo", False), XRPictureBox).Image = CompanyLogoImage

        Dim fmt As String
        If AppConfig.MaindateFormat = "dd/MM/yyyy" Then
            fmt = "{0:dd/MM/yyyy}"
        Else
            fmt = "{0:MM/dd/yyyy}"
        End If

        If rpt.FindControl("lblSubCompName", False) IsNot Nothing Then
            If cmbComp.SelectedText <> "" Then
                rpt.FindControl("lblSubCompName", False).Text = cmbComp.SelectedText
            Else
                rpt.FindControl("lblSubCompName", False).Text = ""
            End If
        End If

        If rpt.FindControl("lblFilterText", False) IsNot Nothing Then

            rpt.FindControl("lblFilterText", False).Text = ""
            rpt.FindControl("lblFilterValue", False).Text = ""

            If cmbComp.SelectedText <> "" Then
                rpt.FindControl("lblFilterText", False).Text += "Company: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += cmbComp.SelectedText & Environment.NewLine
            End If

            If cmbCostCenter.SelectedText <> "" Then
                rpt.FindControl("lblFilterText", False).Text += "Cost Center: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += cmbCostCenter.SelectedText & Environment.NewLine
            End If

            If cmbCust.SelectedText <> "" Then '
                rpt.FindControl("lblFilterText", False).Text += "Employee: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += cmbCust.SelectedText & Environment.NewLine
            End If

            If Trim(trvLocation.SelectedText) <> "" Then
                rpt.FindControl("lblFilterText", False).Text += "Location: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += trvLocation.SelectedText & Environment.NewLine
            End If

            If trvCategory.SelectedText <> "" Then
                rpt.FindControl("lblFilterText", False).Text += "Class: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += trvCategory.SelectedText & Environment.NewLine
            End If

            If txtFromAssetNumber.Text <> "" Then
                rpt.FindControl("lblFilterText", False).Text += "From AssetNumber: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += txtFromAssetNumber.Text & Environment.NewLine
            End If
            If txtToAsetNumber.Text <> "" Then
                rpt.FindControl("lblFilterText", False).Text += "To AssetNumber: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += txtToAsetNumber.Text & Environment.NewLine
            End If

            If txtBusinessArea.Text <> "" Then
                rpt.FindControl("lblFilterText", False).Text += "BusinessArea: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += txtBusinessArea.Text & Environment.NewLine
            End If

            If txtInvestmentNo.Text <> "" Then
                rpt.FindControl("lblFilterText", False).Text += "Inv No: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += txtInvestmentNo.Text & Environment.NewLine
            End If
            If txtGroup1.Text <> "" Then
                rpt.FindControl("lblFilterText", False).Text += "Evaluation Group1: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += txtGroup1.Text & Environment.NewLine
            End If

            If txtGroup2.Text <> "" Then
                rpt.FindControl("lblFilterText", False).Text += "Evaluation Group2: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += txtGroup2.Text & Environment.NewLine
            End If

            If txtGroup3.Text <> "" Then
                rpt.FindControl("lblFilterText", False).Text += "Evaluation Group3: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += txtGroup3.Text & Environment.NewLine
            End If

            If txtGroup4.Text <> "" Then
                rpt.FindControl("lblFilterText", False).Text += "Evaluation Group4: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += txtGroup4.Text & Environment.NewLine
            End If

            If cmbDataSource.Text <> "" Then
                rpt.FindControl("lblFilterText", False).Text += "Data Source: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += cmbDataSource.Text & Environment.NewLine
            End If

            If cmbSch.SelectedText <> "" Then
                rpt.FindControl("lblFilterText", False).Text += "Inventory Schedule: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += cmbSch.SelectedText & Environment.NewLine
            End If

            If chkFilterByCreation.Checked Then
                rpt.FindControl("lblFilterText", False).Text += "Creation date: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += String.Format(" between ({0}) and ({1})", dtFromCreation.Value.ToString(AppConfig.MaindateFormat), dtToCreation.Value.ToString(AppConfig.MaindateFormat)) & Environment.NewLine
            End If
            If chkFilterByCapitalize.Checked Then
                rpt.FindControl("lblFilterText", False).Text += "Capitalize date: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += String.Format(" between ({0}) and ({1})", dtFromCapitalize.Value.ToString(AppConfig.MaindateFormat), dtToCapitalize.Value.ToString(AppConfig.MaindateFormat)) & Environment.NewLine
            End If
            If chkFilterByDisposeDate.Checked Then
                rpt.FindControl("lblFilterText", False).Text += "Retired date: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += String.Format(" between ({0}) and ({1})", dtFromDisposal.Value.ToString(AppConfig.MaindateFormat), dtToDisposal.Value.ToString(AppConfig.MaindateFormat)) & Environment.NewLine
            End If

            If chkExcludeAssetValue.Checked Then
                rpt.FindControl("lblFilterText", False).Text += "Exclude AssetValue: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += "Yes" & Environment.NewLine
            End If

            If chkShowOnlyAssetWithValue.Checked Then
                rpt.FindControl("lblFilterText", False).Text += "Show Only AssetValue: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += "Yes" & Environment.NewLine
            End If

            If chkExcludeCapitalize.Checked Then
                rpt.FindControl("lblFilterText", False).Text += "Exclude Capitalized: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += "Yes" & Environment.NewLine
            End If
            If chkShowOnlyCapitalization.Checked Then
                rpt.FindControl("lblFilterText", False).Text += "Show Only Capitalized: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += "Yes" & Environment.NewLine
            End If
            If chkExcludeRetired.Checked Then
                rpt.FindControl("lblFilterText", False).Text += "Exclude Retired: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += "Yes" & Environment.NewLine
            End If
            If chkShowRetired.Checked Then
                rpt.FindControl("lblFilterText", False).Text += "Show Only Retired: " & Environment.NewLine
                rpt.FindControl("lblFilterValue", False).Text += "Yes" & Environment.NewLine
            End If

        End If

        If chkHideReportHeader.Checked Then
            If rpt.FindControl("ReportHeader", True) IsNot Nothing Then
                rpt.Bands.Remove(rpt.FindControl("ReportHeader", True))
            End If
        End If

        If chkHideReportFooter.Checked Then
            If rpt.FindControl("ReportFooter", True) IsNot Nothing Then
                rpt.Bands.Remove(rpt.FindControl("ReportFooter", True))
            End If
            If rpt.FindControl("PageFooter", True) IsNot Nothing Then
                rpt.Bands.Remove(rpt.FindControl("PageFooter", True))
            End If
            If ReportName = ReportsName.PhysicalInventory Then
                If rpt.FindControl("grpMissing", True) IsNot Nothing Then
                    rpt.Bands.Remove(rpt.FindControl("grpMissing", True))
                End If
                If rpt.FindControl("grpFound", True) IsNot Nothing Then
                    rpt.Bands.Remove(rpt.FindControl("grpFound", True))
                End If
                If rpt.FindControl("grpTrans", True) IsNot Nothing Then
                    rpt.Bands.Remove(rpt.FindControl("grpTrans", True))
                End If
                If rpt.FindControl("grpMissplaced", True) IsNot Nothing Then
                    rpt.Bands.Remove(rpt.FindControl("grpMissplaced", True))
                End If
            End If
        End If
        ShowReport(rpt)
        Return rpt
    End Function

    Private Sub ShowReport(ByVal rpt As DevExpress.XtraReports.UI.XtraReport)
        PreviewControl1.PrintingSystem1.ClearContent()
        rpt.PrintingSystem = PreviewControl1.PrintingSystem1
        rpt.CreateDocument(True)
        'To be able to handle the Close Preview.
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





    Private Sub trvLocation_SelectTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trvLocation.SelectTextChanged
        If Not String.IsNullOrEmpty(trvLocation.SelectedText) Then
            Dim arr As String() = trvLocation.SelectedText.Split("\")
            If arr.Length > 0 Then
                txtPlant.Text = arr(0).Trim
            Else
                txtPlant.Text = String.Empty
            End If

            If arr.Length > 1 Then
                txtLocation.Text = arr(1).Trim
            Else
                txtLocation.Text = String.Empty
            End If
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

    Private Sub chkExcludeRetired_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkExcludeRetired.CheckedChanged
        If chkExcludeRetired.Checked Then
            chkShowRetired.Checked = False
            chkFilterByDisposeDate.Checked = False
        End If
    End Sub

    Private Sub chkShowRetired_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowRetired.CheckedChanged
        If chkShowRetired.Checked Then
            chkExcludeRetired.Checked = False
        End If
    End Sub

    Private Sub chkShowOnlyAssetWithValue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowOnlyAssetWithValue.CheckedChanged
        If chkShowOnlyAssetWithValue.Checked Then
            chkExcludeAssetValue.Checked = False
        End If
    End Sub

    Private Sub chkExcludeAssetValue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkExcludeAssetValue.CheckedChanged
        If chkExcludeAssetValue.Checked Then
            chkShowOnlyAssetWithValue.Checked = False
        End If
    End Sub

    Private Sub chkExcludeCapitalize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkExcludeCapitalize.CheckedChanged
        If chkExcludeCapitalize.Checked Then
            chkShowOnlyCapitalization.Checked = False
            chkFilterByCapitalize.Checked = False
        End If
    End Sub

    Private Sub chkCapitalization_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowOnlyCapitalization.CheckedChanged
        If chkShowOnlyCapitalization.Checked Then
            chkExcludeCapitalize.Checked = False
        End If
    End Sub

    Private Sub chkFilterByCapitalize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterByCapitalize.CheckedChanged
        If chkFilterByCapitalize.Checked Then
            chkExcludeCapitalize.Checked = False
        End If
    End Sub

    Private Sub chkFilterByDisposeDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterByDisposeDate.CheckedChanged
        If chkFilterByDisposeDate.Checked Then
            chkExcludeRetired.Checked = False
        End If
    End Sub

    Private Sub cmbSchAnon_LovBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSchAnon.LovBtnClick
        Try
            Dim objBALAst_INV_Schedule As New BALAst_INV_Schedule
            cmbSchAnon.ValueMember = "InvSchCode"
            cmbSchAnon.DisplayMember = "InvDesc"
            cmbSchAnon.DataSource = objBALAst_INV_Schedule.Getcombo_Schedule()
            cmbSchAnon.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmbSchAnon_SelectTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSchAnon.SelectTextChanged
        Try
            cmbSchAnon.TextBox.ErrorIcon = Nothing
            cmbSchAnon.TextBox.ErrorText = Nothing

            If cmbSchAnon.SelectedValue <> "" Then
                Dim dt As New DataTable
                Dim objattInvSchedule As New attInvSchedule
                Dim objBALAst_INV_Schedule1 As New BALAst_INV_Schedule
                objattInvSchedule.PKeyCode = cmbSchAnon.SelectedValue
                dt = objBALAst_INV_Schedule1.GetAll_invSchID(cmbSchAnon.SelectedValue)
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

    Private Sub btnGenerateAnon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateAnon.Click
        If Not String.IsNullOrEmpty(cmbSchAnon.SelectedText) Then

            Dim rpt As New rptAnonymousAssets
            rpt.RequestParameters = True
            rpt.PrinterName = AppConfig.ReportPrinter
            rpt.PrintingSystem.ShowMarginsWarning = False
            rpt.FindControl("lblCompName", False).Text = AppConfig.CompanyName
            CType(rpt.FindControl("picLogo", False), XRPictureBox).Image = CompanyLogoImage
            rpt.FindControl("lblFilterText", False).Text = ""
            rpt.FindControl("lblFilterValue", False).Text = ""
            rpt.FindControl("lblFilterText", False).Text = "Inventory Schedule: " & Environment.NewLine
            rpt.FindControl("lblFilterValue", False).Text = cmbSchAnon.SelectedText & Environment.NewLine

            If chkHideReportHeader.Checked Then
                If rpt.FindControl("ReportHeader", True) IsNot Nothing Then
                    rpt.Bands.Remove(rpt.FindControl("ReportHeader", True))
                End If
            End If

            If chkHideReportFooter.Checked Then
                If rpt.FindControl("ReportFooter", True) IsNot Nothing Then
                    rpt.Bands.Remove(rpt.FindControl("ReportFooter", True))
                End If
                If rpt.FindControl("PageFooter", True) IsNot Nothing Then
                    rpt.Bands.Remove(rpt.FindControl("PageFooter", True))
                End If
            End If

            rpt.DataSource = GetAnonymousReportDataTable(cmbSchAnon.SelectedValue)
            ShowReport(rpt)
            tabControl.SelectedTabPage = tabPreviewReport
        Else
            cmbSchAnon.TextBox.ErrorText = "Select Inventory Schedule First"
            cmbSchAnon.TextBox.ErrorIcon = My.Resources.Invalid
        End If
    End Sub

    Private Sub nvIssuanceReport_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nvIssuanceReport.LinkClicked, nvWarrantyRecevingSame.LinkClicked, nvWarrantyRecevingReplace.LinkClicked, nvWarrantyClaim.LinkClicked, nvVendorReturn.LinkClicked, nvReversal.LinkClicked
        tabControl.SelectedTabPage = tabGIFilter
        lblIssuance.Text = e.Link.Caption
        imgIssuance.Image = e.Link.GetImage
        Me.Text = String.Format("{0} - {1}", e.Link.Group.Caption, e.Link.Caption)
        ReportName = e.Link.Caption
        If ReportName = ReportsName.GoodsIssuanceDetail Then
            grpIssuanceFilter.Visible = True
            lblAssetNumber.Visible = True
            txtIssueAssetNo.Visible = True
        Else
            grpIssuanceFilter.Visible = False
            lblAssetNumber.Visible = False
            txtIssueAssetNo.Visible = False
        End If
    End Sub

    Private Sub nvGoodsReciveDetail_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nvGoodsReciveDetail.LinkClicked, nvGoodsReceiveStanderd.LinkClicked
        tabControl.SelectedTabPage = tabGRFilter
        lblReciving.Text = e.Link.Caption
        imgReceiving.Image = e.Link.GetImage
        Me.Text = String.Format("{0} - {1}", e.Link.Group.Caption, e.Link.Caption)
        ReportName = e.Link.Caption
    End Sub

    Private Sub LoadGoodsReceiveReport(ByVal ReportName As String)
        Dim rpt As New XtraReport
        If ReportName = ReportsName.GoodsReceveDetail Then
            rpt = New rptGoodsReceiveDetails()
        ElseIf ReportName = ReportsName.GoodsReceveStanderd Then
            rpt = New rptGoodsReceiveStandard
        End If

        rpt.RequestParameters = True
        rpt.PrinterName = AppConfig.ReportPrinter
        rpt.PrintingSystem.ShowMarginsWarning = False
        rpt.FindControl("lblCompName", False).Text = AppConfig.CompanyName
        CType(rpt.FindControl("picLogo", False), XRPictureBox).Image = CompanyLogoImage
        rpt.FindControl("lblFilterText", False).Text = ""
        rpt.FindControl("lblFilterValue", False).Text = ""

        If txtReceivePOFrom.Text <> "" Then
            rpt.FindControl("lblFilterText", False).Text += "From PO: " & Environment.NewLine
            rpt.FindControl("lblFilterValue", False).Text += txtReceivePOFrom.Text & Environment.NewLine
        End If
        If txtReceivePOTo.Text <> "" Then
            rpt.FindControl("lblFilterText", False).Text += "To PO: " & Environment.NewLine
            rpt.FindControl("lblFilterValue", False).Text += txtReceivePOTo.Text & Environment.NewLine
        End If

        If txtReceiveSerialNo.Text <> "" Then
            rpt.FindControl("lblFilterText", False).Text += "Serial Number: " & Environment.NewLine
            rpt.FindControl("lblFilterValue", False).Text += txtReceiveSerialNo.Text & Environment.NewLine
        End If

        If txtReceiveManPartNo.Text <> "" Then
            rpt.FindControl("lblFilterText", False).Text += "Manufacture Part Number: " & Environment.NewLine
            rpt.FindControl("lblFilterValue", False).Text += txtReceiveManPartNo.Text & Environment.NewLine
        End If
        If txtReceiveSAPMatNo.Text <> "" Then
            rpt.FindControl("lblFilterText", False).Text += "SAP Mat Part Number: " & Environment.NewLine
            rpt.FindControl("lblFilterValue", False).Text += txtReceiveSAPMatNo.Text & Environment.NewLine
        End If

        If chkReceiveDate.Checked Then
            rpt.FindControl("lblFilterText", False).Text += "Date: " & Environment.NewLine
            rpt.FindControl("lblFilterValue", False).Text += String.Format(" between ({0}) and ({1})", dtReceiveFrom.Value.ToString(AppConfig.MaindateFormat), dtReceiveTo.Value.ToString(AppConfig.MaindateFormat)) & Environment.NewLine
        End If

        Dim objReport As New ZulAssetsBL.SAPReportsBLL
        rpt.DataSource = objReport.GetSAPGoodsReceiveReport(txtReceivePOFrom.Text.Trim, txtReceivePOTo.Text.Trim, txtReceiveSerialNo.Text.Trim, txtReceiveManPartNo.Text.Trim, txtReceiveSAPMatNo.Text.Trim, chkReceiveDate.Checked, dtReceiveFrom.Value, dtReceiveTo.Value)
        ShowReport(rpt)
        tabControl.SelectedTabPage = tabPreviewReport
    End Sub

    Private Sub btnGenerateReceive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateReceive.Click
        LoadGoodsReceiveReport(ReportName)
    End Sub

    Public Sub PopulateReportHeader(ByVal rpt As DevExpress.XtraReports.UI.XtraReport)
        rpt.RequestParameters = True
        rpt.PrinterName = AppConfig.ReportPrinter
        rpt.PrintingSystem.ShowMarginsWarning = False
        rpt.FindControl("lblCompName", False).Text = AppConfig.CompanyName
        CType(rpt.FindControl("picLogo", False), XRPictureBox).Image = CompanyLogoImage
        rpt.FindControl("lblFilterText", False).Text = ""
        rpt.FindControl("lblFilterValue", False).Text = ""

        If txtDocNo.Text <> "" Then
            rpt.FindControl("lblFilterText", False).Text += "Document Number: " & Environment.NewLine
            rpt.FindControl("lblFilterValue", False).Text += txtDocNo.Text & Environment.NewLine
        End If

        If txtIssueSerialNumber.Text <> "" Then
            rpt.FindControl("lblFilterText", False).Text += "Serial Number: " & Environment.NewLine
            rpt.FindControl("lblFilterValue", False).Text += txtIssueSerialNumber.Text & Environment.NewLine
        End If

        If txtIssueManPartNo.Text <> "" Then
            rpt.FindControl("lblFilterText", False).Text += "Manufacture Part Number: " & Environment.NewLine
            rpt.FindControl("lblFilterValue", False).Text += txtIssueManPartNo.Text & Environment.NewLine
        End If
        If txtIssueSAPMat.Text <> "" Then
            rpt.FindControl("lblFilterText", False).Text += "SAP Mat Part Number: " & Environment.NewLine
            rpt.FindControl("lblFilterValue", False).Text += txtIssueSAPMat.Text & Environment.NewLine
        End If

        If txtIssueAssetNo.Text <> "" Then
            rpt.FindControl("lblFilterText", False).Text += "Asset Number: " & Environment.NewLine
            rpt.FindControl("lblFilterValue", False).Text += txtIssueAssetNo.Text & Environment.NewLine
        End If

        If chkIssueDate.Checked Then
            rpt.FindControl("lblFilterText", False).Text += "Date: " & Environment.NewLine
            rpt.FindControl("lblFilterValue", False).Text += String.Format(" between ({0}) and ({1})", dtIssueFrom.Value.ToString(AppConfig.MaindateFormat), dtIssueTo.Value.ToString(AppConfig.MaindateFormat)) & Environment.NewLine
        End If

        ShowReport(rpt)
        tabControl.SelectedTabPage = tabPreviewReport
    End Sub

    Private Sub LoadGoodsIssuanceReport()
        Dim rpt As New XtraReport
        Dim objReports As New ZulAssetsBL.SAPReportsBLL
        Dim dt As DataTable
        Dim SortExp As String

        If rdoIssueTypeBoth.Checked Then
            rpt = New rptGoodsIssuanceDetails
            SortExp = "DocDate,LineItemNo"
            dt = objReports.GetGoodsIssuanceABBReport(txtIssueSerialNumber.Text.Trim, txtIssueManPartNo.Text.Trim, txtIssueSAPMat.Text.Trim, txtIssueAssetNo.Text.Trim, chkIssueDate.Checked, dtIssueFrom.Value, dtIssueTo.Value, "", SortExp)
        ElseIf rdoIssueTypeInvPr.Checked Then
            rpt = New rptGoodsIssuance1
            SortExp = "SAPItemSerialsTrans.InvProposalNo,DocDate,LineItemNo"
            dt = objReports.GetGoodsIssuanceABBReport(txtIssueSerialNumber.Text.Trim, txtIssueManPartNo.Text.Trim, txtIssueSAPMat.Text.Trim, txtIssueAssetNo.Text.Trim, chkIssueDate.Checked, dtIssueFrom.Value, dtIssueTo.Value, "GINV", SortExp)
        Else 'rdoPOR checked.
            rpt = New rptGoodsIssuance2
            SortExp = "SAPItemSerialsTrans.PORequisitionNo,DocDate,LineItemNo"
            dt = objReports.GetGoodsIssuanceABBReport(txtIssueSerialNumber.Text.Trim, txtIssueManPartNo.Text.Trim, txtIssueSAPMat.Text.Trim, txtIssueAssetNo.Text.Trim, chkIssueDate.Checked, dtIssueFrom.Value, dtIssueTo.Value, "GIPOR", SortExp)
        End If
     
        rpt.DataSource = dt
        PopulateReportHeader(rpt)
    End Sub

    Private Sub LoadReversalReport()
        Dim rpt As New rptReversalDoc
        Dim objReports As New ZulAssetsBL.SAPReportsBLL
        Dim dt As DataTable
        dt = objReports.GetReversalReport(txtIssueSerialNumber.Text.Trim, txtIssueManPartNo.Text.Trim, txtIssueSAPMat.Text.Trim, txtDocNo.Text.Trim, chkIssueDate.Checked, dtIssueFrom.Value, dtIssueTo.Value)
        rpt.DataSource = dt
        PopulateReportHeader(rpt)
    End Sub
    Private Sub LoadVendorReturnReport()
        Dim rpt As New rptVendorReturn
        Dim objReports As New ZulAssetsBL.SAPReportsBLL
        Dim dt As DataTable
        dt = objReports.GetVendorReturnReport(txtIssueSerialNumber.Text.Trim, txtIssueManPartNo.Text.Trim, txtIssueSAPMat.Text.Trim, txtDocNo.Text.Trim, chkIssueDate.Checked, dtIssueFrom.Value, dtIssueTo.Value)
        rpt.DataSource = dt
        PopulateReportHeader(rpt)
    End Sub

    Private Sub LoadWarrantyClaimReport()
        Dim rpt As New rptWarrantyClaim
        Dim objReports As New ZulAssetsBL.SAPReportsBLL
        Dim dt As DataTable
        dt = objReports.GetWarrantyClaimReport(txtIssueSerialNumber.Text.Trim, txtIssueManPartNo.Text.Trim, txtIssueSAPMat.Text.Trim, txtDocNo.Text.Trim, chkIssueDate.Checked, dtIssueFrom.Value, dtIssueTo.Value)
        rpt.DataSource = dt
        PopulateReportHeader(rpt)
    End Sub

    Private Sub LoadWarrantyRecevingSameReport()
        Dim rpt As New rptWarrantyReceiveSame
        Dim objReports As New ZulAssetsBL.SAPReportsBLL
        Dim dt As DataTable
        dt = objReports.GetWarrantyReceiveSameReport(txtIssueSerialNumber.Text.Trim, txtIssueManPartNo.Text.Trim, txtIssueSAPMat.Text.Trim, txtDocNo.Text.Trim, chkIssueDate.Checked, dtIssueFrom.Value, dtIssueTo.Value)
        rpt.DataSource = dt
        PopulateReportHeader(rpt)
    End Sub

    Private Sub LoadWarrantyRecevingReplaceReport()
        Dim rpt As New rptWarrantyReceiveReplace
        Dim objReports As New ZulAssetsBL.SAPReportsBLL
        Dim dt As DataTable
        dt = objReports.GetWarrantyReceiveReplaceReport(txtIssueSerialNumber.Text.Trim, txtIssueManPartNo.Text.Trim, txtIssueSAPMat.Text.Trim, txtDocNo.Text.Trim, chkIssueDate.Checked, dtIssueFrom.Value, dtIssueTo.Value)
        rpt.DataSource = dt
        PopulateReportHeader(rpt)
    End Sub

    Private Sub btnGenerateIssuance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateIssuance.Click
        Select Case ReportName
            Case ReportsName.GoodsIssuanceDetail
                LoadGoodsIssuanceReport()
            Case ReportsName.ReversalGRGI
                LoadReversalReport()
            Case ReportsName.VendorReturn
                LoadVendorReturnReport()
            Case ReportsName.WarrantyClaim
                LoadWarrantyClaimReport()
            Case ReportsName.WarrantyRecevingSame
                LoadWarrantyRecevingSameReport()
            Case ReportsName.WarrantyRecevingReplace
                LoadWarrantyRecevingReplaceReport()
        End Select
    End Sub
End Class