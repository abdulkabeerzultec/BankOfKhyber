Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Windows.Forms

Public Class frmRoles
    Dim objattRole As attRoles
    Dim objBALRole As New BALRoles
    Dim objattCompany As attcompany
    Dim objBALCompany As New BALCompany
    Dim objBALReport As New BALReports

    Dim isEdit As Boolean = False


    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

#Region " -- Check Box Event--"
    Private Sub chkAllComm_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllComm.CheckedChanged, chkAllTools.CheckedChanged, chkAllPO.CheckedChanged, chkAllAssets.CheckedChanged, chkAllComp.CheckedChanged, chkAllSec.CheckedChanged, chkselAllMasterData.CheckedChanged
        If TypeOf (sender) Is CheckBox Then
            SetCheckBoxValue(CType(sender, CheckBox).Parent, CType(sender, CheckBox).Checked)
        End If
    End Sub

    Private Sub UnCheckAllCheckBoxs()
        SetCheckBoxValue(tbReports, False)
        SetCheckBoxValue(tbAssets, False)
        SetCheckBoxValue(tbComm, False)
        SetCheckBoxValue(tbCompProfile, False)
        SetCheckBoxValue(tbMasData, False)
        SetCheckBoxValue(tbPO, False)
        SetCheckBoxValue(tbTools, False)
        SetCheckBoxValue(tbSecurity, False)
    End Sub

    ''' <summary>
    ''' Sets all CheckBoxs contains in the FrameName to the value specified
    ''' </summary>
    ''' <param name="FrameName"></param>
    ''' <param name="value"></param>
    ''' <remarks>by Wael Dalloul</remarks>
    Public Sub SetCheckBoxValue(ByVal FrameName As Object, ByVal value As Boolean)

        For Each myctrl As Control In FrameName.Controls
            If TypeOf (myctrl) Is CheckBox Then
                CType(myctrl, CheckBox).Checked = value
            End If
        Next
    End Sub
#End Region


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Function validCompAccess() As Boolean
        If grdViewCompany.RowCount > 0 Then
            For i As Integer = 0 To grdViewCompany.RowCount - 1
                If GetGridRowCellValue(grdViewCompany, i, "SELECTION").ToString = True Then
                    Return True
                End If
            Next
        Else
            Return False
        End If
        Return False
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate Then
                If validCompAccess() Then
                    If isEdit Then
                        UpdateRecord()
                    Else
                        AddNewRecord()
                    End If
                    chkAll.Checked = False
                Else
                    ZulMessageBox.ShowMe("CompAccess")
                End If
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Function AddNewRecord() As Boolean
        Try
            objattRole = New attRoles
            objattRole.PKeyCode = lovRoleID.SelectedText
            objattRole.AppUser = chkUsers.Checked.ToString()
            objattRole.CompanyInfo = chkCompInfo.Checked.ToString()
            objattRole.Location = chkLoc.Checked.ToString()
            objattRole.DepPolicy = chkDepPolicy.Checked.ToString()
            objattRole.Brands = chkBrand.Checked.ToString()
            objattRole.Designation = chkDesig.Checked.ToString()
            objattRole.Department = chkDept.Checked.ToString()
            objattRole.Custodian = chkCustodian.Checked.ToString()
            objattRole.Insurer = chkIns.Checked.ToString()
            objattRole.DisposalMethod = chkDisposal.Checked.ToString()
            objattRole.Supplier = chkSupplier.Checked.ToString()
            objattRole.DepreciationMethod = chkDepre.Checked.ToString()
            objattRole.InvSch = chkInvSch.Checked.ToString()
            objattRole.AssetsBooks = chkAstBooks.Checked.ToString()
            objattRole.AddressBook = chkAddress.Checked.ToString()
            objattRole.AssetsCat = chkAstCat.Checked.ToString()
            objattRole.CostCenter = chkCostCenter.Checked.ToString()
            objattRole.AssetItems = chkAssetItem.Checked.ToString()
            objattRole.Company = chkCompanies.Checked.ToString()
            objattRole.PO = chkPO.Checked.ToString()
            objattRole.POApproval = chkApproval.Checked.ToString()
            objattRole.POTrans = chkPOTrans.Checked.ToString()
            objattRole.DeviceConfig = chkDevice.Checked.ToString()
            objattRole.SysConfig = chkSysConfig.Checked.ToString()
            objattRole.DataSend = chkDataSend.Checked.ToString()
            objattRole.DataRecieve = chkDataRec.Checked.ToString()
            objattRole.AstAdmin = chkAsstAdmin.Checked.ToString()
            objattRole.AstDetail = chkAstDet.Checked.ToString()
            objattRole.AstTrans = chkAstTrans.Checked.ToString()
            objattRole.AstSrch = chkAstSrch.Checked.ToString()
            objattRole.InterComTrans = chkInterCom.Checked.ToString()
            'objattRole.Anonym = chkAnonym.Checked.ToString()
            objattRole.Anonym = False
            objattRole.Roles = ChkRoles.Checked.ToString()
            objattRole.Description = RemoveUnnecessaryChars(txtRoleDesc.Text)
            objattRole.Units = chkUnits.Checked.ToString()
            objattRole.DepMan = chkDepMan.Checked.ToString()
            objattRole.BarStuct = chkBarStuct.Checked.ToString()
            objattRole.BarCodePolicy = chkBarCodePolicy.Checked.ToString()
            objattRole.AssetCoding = chkAssetCoding.Checked.ToString()
            objattRole.CompGroup = chkCompGroup.Checked.ToString()
            objattRole.CompLvl = chkCompLevel.Checked.ToString()
            objattRole.GroupHier = chkGroupHier.Checked.ToString()
            objattRole.GLCode = chkGLCodes.Checked.ToString()
            objattRole.ReportDesigner = chkReportDesigner.Checked.ToString
            objattRole.CreateReport = chkCreateReport.Checked.ToString

            objattRole.CMAExport = chkExportToFile.Checked.ToString
            objattRole.CMAImport = chkImportFrmFile.Checked.ToString
            objattRole.DataProcessing = chkDataProcessing.Checked.ToString

            objattRole.OfflineMachine = chkOfflineMachines.Checked.ToString
            objattRole.BackendInventory = chkBackendInventory.Checked.ToString

            objattRole.Custom1 = chkWarrantyAlarm.Checked
            objattRole.Custom2 = chkStockIssuance.Checked
            objattRole.Custom3 = chkSupplierReturn.Checked
            objattRole.Custom4 = 0
            objattRole.Custom5 = 0
            objattRole.Custom6 = 0
            objattRole.Custom7 = 0
            objattRole.Custom8 = 0
            objattRole.Custom9 = 0
            objattRole.Custom10 = 0
            objattRole.Custom11 = 0
            objattRole.Custom12 = 0
            objattRole.Custom13 = 0
            objattRole.Custom14 = 0
            objattRole.Custom15 = 0

            Dim strCompId As String
            strCompId = ""
            For i As Integer = 0 To grdViewCompany.RowCount - 1
                If CBool(GetGridRowCellValue(grdViewCompany, i, "SELECTION")) <> False Then
                    If strCompId <> "" Then
                        strCompId = strCompId + ","
                    End If
                    strCompId = strCompId + GetGridRowCellValue(grdViewCompany, i, "CompanyID").ToString
                End If
            Next

            objattRole.Companies = strCompId

            If Not CheckExistValues(objattRole, True) Then
                If objBALRole.Insert_Roles(objattRole) Then
                    'add RoleReports
                    For i As Integer = 0 To grdviewReports.RowCount - 1
                        If CBool(GetGridRowCellValue(grdviewReports, i, "PERMISSION")) = True Then
                            objBALReport.AddRoleReport(objattRole.PKeyCode, GetGridRowCellValue(grdviewReports, i, "ReportName"))
                        End If
                    Next

                    ZulMessageBox.ShowMe("Saved")
                    refreshme()
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function

    Private Sub formatCompanyGrid()
        grdViewCompany.Columns(0).Width = 115
        grdViewCompany.OptionsBehavior.Editable = True
        grdViewCompany.Columns(0).OptionsColumn.AllowEdit = True

        grdViewCompany.Columns(0).Caption = "SELECTION"
        grdViewCompany.Columns(0).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        grdViewCompany.Columns(1).Caption = "Company ID"
        grdViewCompany.Columns(1).Width = 115
        grdViewCompany.Columns(1).OptionsColumn.AllowEdit = False
        grdViewCompany.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdViewCompany.Columns(2).Caption = "Company Code"
        grdViewCompany.Columns(2).Width = 150
        grdViewCompany.Columns(2).OptionsColumn.AllowEdit = False
        grdViewCompany.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdViewCompany.Columns(3).Caption = "Company Name"
        grdViewCompany.Columns(3).Width = 300
        grdViewCompany.Columns(3).OptionsColumn.AllowEdit = False

        grdViewCompany.Columns(4).Visible = False
        grdViewCompany.Columns(5).Visible = False
        grdViewCompany.Columns(6).Visible = False
        grdViewCompany.Columns(7).Visible = False
        grdViewCompany.Columns(8).Visible = False

        grdcompany.UseEmbeddedNavigator = True
        grdcompany.EmbeddedNavigator.Buttons.Append.Visible = False
        grdcompany.EmbeddedNavigator.Buttons.Edit.Visible = False
        grdcompany.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grdcompany.EmbeddedNavigator.Buttons.Remove.Visible = False
        grdcompany.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

        addGridMenu(grdcompany)
    End Sub

    Private Sub formatReportGrid()
        Dim RIReportType As New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
        RIReportType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() _
                                                    {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Standard Report", False, -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Extended Report", True, -1)})
        grdviewReports.OptionsBehavior.Editable = True

        grdviewReports.Columns(0).Caption = "Report Name"
        grdviewReports.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdviewReports.Columns(0).OptionsColumn.AllowEdit = False

        grdviewReports.Columns(1).Visible = False

        grdviewReports.Columns(2).ColumnEdit = RIReportType
        grdviewReports.Columns(2).Caption = "Report Type"
        grdviewReports.Columns(2).OptionsColumn.AllowEdit = False
        grdviewReports.Columns(3).Visible = False

        grdviewReports.Columns(4).Caption = "Permission"
        grdviewReports.Columns(4).OptionsColumn.AllowEdit = True


        grdReports.UseEmbeddedNavigator = True
        grdReports.EmbeddedNavigator.Buttons.Append.Visible = False
        grdReports.EmbeddedNavigator.Buttons.Edit.Visible = False
        grdReports.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grdReports.EmbeddedNavigator.Buttons.Remove.Visible = False
        grdReports.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

        addGridMenu(grdReports)
    End Sub

    Private Sub chkCompAstRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.ActiveControl = txtRoleDesc

        Try
            Dim dt As DataTable = objBALCompany.GetAllData_Grid(New attcompany)
            Dim SelectionCol As DataColumn = dt.Columns.Add("SELECTION", System.Type.GetType("System.Boolean"))
            SelectionCol.SetOrdinal(0)
            grdcompany.DataSource = dt
            For i As Integer = 0 To grdViewCompany.RowCount - 1
                SetGridRowCellValue(grdViewCompany, i, "SELECTION", False)
            Next

            Dim dtRoleReports As DataTable = objBALReport.GetAll_ReportsFiles(New attReports)
            Dim PermissionCol As DataColumn = dtRoleReports.Columns.Add("PERMISSION", System.Type.GetType("System.Boolean"))
            grdReports.DataSource = dtRoleReports
            For i As Integer = 0 To grdviewReports.RowCount - 1
                SetGridRowCellValue(grdviewReports, i, "PERMISSION", False)
            Next


            formatReportGrid()
            formatCompanyGrid()


            valProvMain.SetValidationRule(txtRoleDesc, valRulenotEmpty)
            valProvMain.SetValidationRule(lovRoleID.TextBox, valRulenotEmpty)

            isEdit = False


            lovRoleID.SelectedText = objBALRole.GetNextPKey_Roles.ToString
        Catch ex As Exception

        End Try
    End Sub


    Private Sub HideTabCheckBoxes(ByVal tab As TabPage)
        For Each ctl As Control In tab.Controls

        Next
    End Sub
    Private Function CheckExistValues(ByVal objatt As attRoles, ByVal IsInsertStatus As Boolean) As Boolean
        If objBALRole.RoleExist(objatt, IsInsertStatus) Then
            errProv.SetError(txtRoleDesc, "Role Description already exists, change it and try again.")
            txtRoleDesc.SelectAll()
            txtRoleDesc.Focus()
            Return True
        Else
            Return False
        End If
    End Function

    Private Function UpdateRecord() As Boolean
        Try
            objattRole = New attRoles
            objattRole.PKeyCode = lovRoleID.SelectedValue
            objattRole.AppUser = chkUsers.Checked.ToString()
            objattRole.CompanyInfo = chkCompInfo.Checked.ToString()
            objattRole.Location = chkLoc.Checked.ToString()
            objattRole.DepPolicy = chkDepPolicy.Checked.ToString()
            objattRole.Brands = chkBrand.Checked.ToString()
            objattRole.Designation = chkDesig.Checked.ToString()
            objattRole.Department = chkDept.Checked.ToString()
            objattRole.Custodian = chkCustodian.Checked.ToString()
            objattRole.Insurer = chkIns.Checked.ToString()
            objattRole.DisposalMethod = chkDisposal.Checked.ToString()
            objattRole.Supplier = chkSupplier.Checked.ToString()
            objattRole.DepreciationMethod = chkDepre.Checked.ToString()
            objattRole.InvSch = chkInvSch.Checked.ToString()
            objattRole.AssetsBooks = chkAstBooks.Checked.ToString()
            objattRole.AddressBook = chkAddress.Checked.ToString()
            objattRole.AssetsCat = chkAstCat.Checked.ToString()
            objattRole.CostCenter = chkCostCenter.Checked.ToString()
            objattRole.AssetItems = chkAssetItem.Checked.ToString()
            objattRole.Company = chkCompanies.Checked.ToString()
            objattRole.PO = chkPO.Checked.ToString()
            objattRole.POApproval = chkApproval.Checked.ToString()
            objattRole.POTrans = chkPOTrans.Checked.ToString()
            objattRole.DeviceConfig = chkDevice.Checked.ToString()
            objattRole.SysConfig = chkSysConfig.Checked.ToString()
            objattRole.DataSend = chkDataSend.Checked.ToString()
            objattRole.DataRecieve = chkDataRec.Checked.ToString()
            objattRole.AstAdmin = chkAsstAdmin.Checked.ToString()
            objattRole.AstDetail = chkAstDet.Checked.ToString()
            objattRole.AstTrans = chkAstTrans.Checked.ToString()
            objattRole.AstSrch = chkAstSrch.Checked.ToString()
            objattRole.InterComTrans = chkInterCom.Checked.ToString()
            'objattRole.Anonym = chkAnonym.Checked.ToString()
            objattRole.Anonym = False
            objattRole.Roles = ChkRoles.Checked.ToString()
            objattRole.Description = RemoveUnnecessaryChars(txtRoleDesc.Text)
            objattRole.Units = chkUnits.Checked.ToString()
            objattRole.DepMan = chkDepMan.Checked.ToString()
            objattRole.BarStuct = chkBarStuct.Checked.ToString()
            objattRole.BarCodePolicy = chkBarCodePolicy.Checked.ToString()
            objattRole.AssetCoding = chkAssetCoding.Checked.ToString()
            objattRole.CompGroup = chkCompGroup.Checked.ToString()
            objattRole.CompLvl = chkCompLevel.Checked.ToString()
            objattRole.GroupHier = chkGroupHier.Checked.ToString()
            objattRole.GLCode = chkGLCodes.Checked.ToString()
            objattRole.ReportDesigner = chkReportDesigner.Checked.ToString
            objattRole.CreateReport = chkCreateReport.Checked.ToString

            objattRole.CMAExport = chkExportToFile.Checked.ToString
            objattRole.CMAImport = chkImportFrmFile.Checked.ToString
            objattRole.DataProcessing = chkDataProcessing.Checked.ToString

            objattRole.OfflineMachine = chkOfflineMachines.Checked.ToString
            objattRole.BackendInventory = chkBackendInventory.Checked.ToString

            objattRole.Custom1 = chkWarrantyAlarm.Checked
            objattRole.Custom2 = chkStockIssuance.Checked
            objattRole.Custom3 = chkSupplierReturn.Checked
            objattRole.Custom4 = 0
            objattRole.Custom5 = 0
            objattRole.Custom6 = 0
            objattRole.Custom7 = 0
            objattRole.Custom8 = 0
            objattRole.Custom9 = 0
            objattRole.Custom10 = 0
            objattRole.Custom11 = 0
            objattRole.Custom12 = 0
            objattRole.Custom13 = 0
            objattRole.Custom14 = 0
            objattRole.Custom15 = 0

            Dim strCompId As String
            strCompId = ""
            For i As Integer = 0 To grdViewCompany.RowCount - 1
                If CBool(GetGridRowCellValue(grdViewCompany, i, "SELECTION")) <> False Then
                    If strCompId <> "" Then
                        strCompId = strCompId + ","
                    End If
                    strCompId = strCompId + GetGridRowCellValue(grdViewCompany, i, "CompanyID").ToString
                End If
            Next

            objattRole.Companies = strCompId

            If Not CheckExistValues(objattRole, False) Then
                If objBALRole.Update_Roles(objattRole) Then

                    'add RoleReports
                    For i As Integer = 0 To grdviewReports.RowCount - 1
                        Dim ReportName As String = GetGridRowCellValue(grdviewReports, i, "ReportName")
                        Dim RoleID As String = objattRole.PKeyCode
                        If GetGridRowCellValue(grdviewReports, i, "PERMISSION") = True Then
                            If objBALReport.CheckExistRoleReport(RoleID, ReportName) < 1 Then
                                objBALReport.AddRoleReport(RoleID, ReportName)
                            End If
                        Else
                            objBALReport.DeleteRoleReport(RoleID, ReportName)
                        End If
                    Next

                    ZulMessageBox.ShowMe("Saved")
                    refreshme()
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function


    Private Sub txtRoleID_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lovRoleID.SelectTextChanged
        Try
            If lovRoleID.SelectedText <> "" Then
                If lovRoleID.SelectedValue <> "" Then
                    LoadData(lovRoleID.SelectedValue)
                End If
            End If

            valProvMain.RemoveControlError(txtRoleDesc)
            errProv.ClearErrors()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub LoadData(ByVal RoleID As String)
        Dim ds As DataTable
        objattRole = New attRoles
        objattRole.PKeyCode = RoleID

        chkAll.Checked = False

        If RoleID = "1" Then ' if it's admin Role.
            btnDelete.Visible = False
            btnSave.Enabled = False
            For idx As Integer = 0 To grdviewReports.RowCount
                SetGridRowCellValue(grdviewReports, idx, "PERMISSION", True)
            Next
            For idx As Integer = 0 To grdViewCompany.RowCount
                SetGridRowCellValue(grdViewCompany, idx, "SELECTION", True)
            Next
        Else
            btnDelete.Visible = True
            btnSave.Enabled = True
            ds = objBALRole.GetAll_Roles(objattRole)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    For i As Integer = 0 To grdViewCompany.RowCount - 1
                        SetGridRowCellValue(grdViewCompany, i, "SELECTION", False)
                    Next
                    chkUsers.Checked = ds.Rows(0)("AppUser")
                    chkCompInfo.Checked = ds.Rows(0)("CompanyInfo")
                    chkLoc.Checked = ds.Rows(0)("Location")
                    chkDepPolicy.Checked = ds.Rows(0)("DepPolicy")
                    chkBrand.Checked = ds.Rows(0)("Brands")
                    chkDesig.Checked = ds.Rows(0)("Designation")
                    chkDept.Checked = ds.Rows(0)("Department")
                    chkCustodian.Checked = ds.Rows(0)("Custodian")
                    chkIns.Checked = ds.Rows(0)("Insurer")
                    chkDisposal.Checked = ds.Rows(0)("DisposalMethod")
                    chkSupplier.Checked = ds.Rows(0)("Supplier")
                    chkDepre.Checked = ds.Rows(0)("DepreciationMethod")
                    chkInvSch.Checked = ds.Rows(0)("InvSch")
                    chkAstBooks.Checked = ds.Rows(0)("AssetsBooks")
                    chkAddress.Checked = ds.Rows(0)("AddressBook")
                    chkAstCat.Checked = ds.Rows(0)("AssetsCat")
                    chkCostCenter.Checked = ds.Rows(0)("CostCenter")
                    chkAssetItem.Checked = ds.Rows(0)("AssetItems")
                    chkCompanies.Checked = ds.Rows(0)("Company")
                    chkPO.Checked = ds.Rows(0)("PO")
                    chkApproval.Checked = ds.Rows(0)("POApproval")
                    chkPOTrans.Checked = ds.Rows(0)("POTrans")
                    chkDevice.Checked = ds.Rows(0)("DeviceConfig")
                    chkSysConfig.Checked = ds.Rows(0)("SysConfig")
                    chkDataSend.Checked = ds.Rows(0)("DataSend")
                    chkDataRec.Checked = ds.Rows(0)("DataRecieve")
                    chkUnits.Checked = ds.Rows(0)("Units")
                    chkAsstAdmin.Checked = ds.Rows(0)("AstAdmin")
                    chkAstDet.Checked = ds.Rows(0)("AstDetail")
                    chkAstTrans.Checked = ds.Rows(0)("AstTranS")
                    chkAstSrch.Checked = ds.Rows(0)("AstSrc")
                    chkInterCom.Checked = ds.Rows(0)("InterComTrans")
                    'chkAnonym.Checked = ds.Rows(0)("Anonym")
                    chkDepMan.Checked = ds.Rows(0)("DepMan")
                    ChkRoles.Checked = ds.Rows(0)("Roles")
                    txtRoleDesc.Text = ds.Rows(0)("description")
                    chkBarStuct.Checked = ds.Rows(0)("BarStuct")
                    chkBarCodePolicy.Checked = ds.Rows(0)("BarCodePolicy")
                    chkAssetCoding.Checked = ds.Rows(0)("AssetCoding")

                    chkCompLevel.Checked = ds.Rows(0)("CompLvl")
                    chkCompGroup.Checked = ds.Rows(0)("CompGroup")
                    chkGroupHier.Checked = ds.Rows(0)("GroupHier")
                    chkGLCodes.Checked = ds.Rows(0)("GLCode")
                    chkReportDesigner.Checked = ds.Rows(0)("ReportDesigner")
                    chkCreateReport.Checked = ds.Rows(0)("CreateReport")

                    chkExportToFile.Checked = ds.Rows(0)("CMAExport").ToString
                    chkImportFrmFile.Checked = ds.Rows(0)("CMAImport").ToString
                    chkDataProcessing.Checked = ds.Rows(0)("DataProcessing").ToString

                    chkOfflineMachines.Checked = ds.Rows(0)("OfflineMachine").ToString
                    chkBackendInventory.Checked = ds.Rows(0)("BackendInventory").ToString
                    chkWarrantyAlarm.Checked = ds.Rows(0)("Custom1").ToString
                    chkStockIssuance.Checked = ds.Rows(0)("Custom2").ToString
                    chkSupplierReturn.Checked = ds.Rows(0)("Custom3").ToString

                    Dim dsRoleReports As DataTable = objBALReport.GetRoleReports(RoleID)
                    For i As Integer = 0 To grdviewReports.RowCount - 1
                        SetGridRowCellValue(grdviewReports, i, "PERMISSION", False)
                    Next
                    For Each row As DataRow In dsRoleReports.Rows
                        Dim idx As Integer = grdviewReports.LocateByDisplayText(0, GetGridColumn(grdviewReports, "ReportName"), row("ReportName"))
                        If idx >= 0 Then
                            SetGridRowCellValue(grdviewReports, idx, "PERMISSION", True)
                        Else
                            SetGridRowCellValue(grdviewReports, idx, "PERMISSION", False)
                        End If
                    Next

                    If ds.Rows(0)("Companies").ToString <> "" Then
                        Dim strCompIds() As String
                        strCompIds = ds.Rows(0)("Companies").ToString().Split(",")
                        If strCompIds.Length > 0 Then
                            For i As Integer = 0 To grdViewCompany.RowCount - 1
                                SetGridRowCellValue(grdViewCompany, i, "SELECTION", False)
                            Next
                            Dim RowHandle As Integer = 0
                            For i As Integer = 0 To strCompIds.Length - 1
                                RowHandle = grdViewCompany.LocateByDisplayText(0, GetGridColumn(grdViewCompany, "CompanyID"), strCompIds(i))
                                If RowHandle >= 0 Then
                                    SetGridRowCellValue(grdViewCompany, RowHandle, "SELECTION", True)
                                Else
                                    SetGridRowCellValue(grdViewCompany, RowHandle, "SELECTION", False)
                                End If
                            Next
                        End If
                    End If
                    isEdit = True
                Else
                    UnCheckAllCheckBoxs()
                    isEdit = False
                    txtRoleDesc.Text = ""
                    lovRoleID.SelectedText = objBALRole.GetNextPKey_Roles
                    btnDelete.Visible = False
                    btnSave.Enabled = False
                    For i As Integer = 0 To grdViewCompany.RowCount - 1
                        SetGridRowCellValue(grdViewCompany, i, "SELECTION", False)
                    Next
                End If
            End If
        End If
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        lovRoleID.SelectedText = objBALRole.GetNextPKey_Roles
        lovRoleID.SelectedValue = objBALRole.GetNextPKey_Roles
        UnCheckAllCheckBoxs()
        isEdit = False
        chkAll.Checked = False
        txtRoleDesc.Text = ""
        chkAllSec.Checked = False
        btnDelete.Visible = False
        btnSave.Enabled = True
        txtRoleDesc.Focus()

        valProvMain.RemoveControlError(txtRoleDesc)
        errProv.ClearErrors()

        For i As Integer = 0 To grdViewCompany.RowCount - 1
            SetGridRowCellValue(grdViewCompany, i, "SELECTION", False)
        Next
        For i As Integer = 0 To grdviewReports.RowCount - 1
            SetGridRowCellValue(grdviewReports, i, "PERMISSION", False)
        Next
    End Sub

    Private Sub refreshme()
        lovRoleID.SelectedText = objBALRole.GetNextPKey_Roles
        lovRoleID.SelectedValue = objBALRole.GetNextPKey_Roles
        UnCheckAllCheckBoxs()
        isEdit = False
        txtRoleDesc.Text = ""
        chkBarStuct.Checked = False
        chkAll.Checked = False
        btnDelete.Visible = False
        btnSave.Enabled = True
        For i As Integer = 0 To grdViewCompany.RowCount - 1
            SetGridRowCellValue(grdViewCompany, i, "SELECTION", False)
        Next

        For i As Integer = 0 To grdviewReports.RowCount - 1
            SetGridRowCellValue(grdviewReports, i, "PERMISSION", False)
        Next
    End Sub


    Public Function check_Child_Roles(ByVal _id As String, ByVal formid As Integer) As Boolean
        Dim objBALUsers As New BALUsers
        Dim ds As DataTable = objBALUsers.Check_Child(_id, formid)
        If Not ds Is Nothing Then
            If ds.Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not check_Child_Roles(lovRoleID.SelectedText, 1) Then
                If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    objattRole = New attRoles
                    objattRole.PKeyCode = lovRoleID.SelectedText
                    If objBALRole.Delete_Roles(objattRole) Then
                        ' Delete Role Reports after deleting the Role.
                        For i As Integer = 0 To grdviewReports.RowCount - 1
                            Dim ReportName As String = GetGridRowCellValue(grdviewReports, i, "ReportName")
                            Dim RoleID As String = objattRole.PKeyCode
                            objBALReport.DeleteRoleReport(RoleID, ReportName)
                        Next

                        ZulMessageBox.ShowMe("Deleted")
                        refreshme()
                        chkAll.Checked = False
                        btnNew_Click(sender, e)
                    End If
                End If
            Else
                ZulMessageBox.ShowMe("CantDelete")
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        For i As Integer = 0 To grdViewCompany.RowCount - 1
            SetGridRowCellValue(grdViewCompany, i, "SELECTION", chkAll.Checked)
        Next
    End Sub

    Private Sub txtRoleID_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lovRoleID.LovBtnClick
        Try
            lovRoleID.ValueMember = "RoleID"
            lovRoleID.DisplayMember = "RoleID"
            Dim objBALRole As New BALRoles
            lovRoleID.DataSource = objBALRole.Getcombo_Roles
            lovRoleID.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub chkRptAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRptAll.CheckedChanged
        For i As Integer = 0 To grdviewReports.RowCount - 1
            SetGridRowCellValue(grdviewReports, i, "PERMISSION", chkRptAll.Checked)
        Next
        chkReportDesigner.Checked = chkRptAll.Checked
        chkCreateReport.Checked = chkRptAll.Checked
    End Sub
End Class
