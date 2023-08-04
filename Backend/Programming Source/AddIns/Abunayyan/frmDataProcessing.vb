Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Windows.Forms

Public Class frmDataProcessing

    Private valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider

    Private RIType As New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox

    Private Sub frmDataProcessing_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        btnRefresh.PerformClick()
    End Sub

    Private Sub frmDataProcessing_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

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

    Private Sub frmDataProcessing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = My.Resources.ZulAssets
            Me.BackgroundImage = My.Resources.Forms_BG
            Me.BackgroundImageLayout = ImageLayout.Stretch
            InitVariables()
            valProvMain.SetValidationRule(cmbSch.TextBox, valRulenotEmpty)
            'check the checkbox will load the data
            rdoIdentified.Checked = True
            RIType.SmallImages = imgAssetStatus
            RIType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() _
                                                        {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Missing Asset", 0, 0), _
                                                         New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Found Asset", 1, 1), _
                                                         New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Misplaced Asset", 2, 2), _
                                                         New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Transferred Asset", 3, 3), _
                                                         New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Allocated Asset", 4, 4)})
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub GetAnonymousAssets()

        Dim objBALAnonymAssets As New BALAnonymousAsset
        Dim objBALLocation As New BALLocation
        Dim objBALDevices As New BALDevices
        Dim objBALCategory As New BALCategory

        Dim dt As DataTable = objBALAnonymAssets.GetAll_AnonymousAsset()

        Dim dcCurrentLoc As DataColumn = dt.Columns.Add("Location", Type.GetType("System.String"))
        Dim dcCat As DataColumn = dt.Columns.Add("Category", Type.GetType("System.String"))
        Dim dcDeviceDesc As DataColumn = dt.Columns.Add("DeviceDesc", Type.GetType("System.String"))
        For i As Integer = 0 To dt.Rows.Count - 1
            If Not String.IsNullOrEmpty(dt.Rows(i)("LocID").ToString) Then
                dt.Rows(i)("Location") = objBALLocation.GetLocFullPath(dt.Rows(i)("LocID").ToString)
            End If
            dt.Rows(i)("Category") = objBALCategory.GetCatFullPath(dt.Rows(i)("AstCatID").ToString)
            dt.Rows(i)("DeviceDesc") = objBALDevices.GetDeviceDescription(dt.Rows(i)("DeviceID").ToString)
        Next
        grdView.Columns.Clear()
        grd.DataSource = dt
        FormatGridAnonymousAssets()
    End Sub

    Private Sub FormatGridAnonymousAssets()

        With grdView
            .OptionsBehavior.Editable = False
            .Columns(0).Visible = False
            .Columns(1).Visible = False
            .Columns(2).Visible = False
            .Columns(3).Visible = False
            .Columns("AstModel").Visible = False
            .Columns("RefNo").Visible = True
            .Columns("Remarks").Visible = False
        End With

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        addGridMenu(grd)
    End Sub



    Private Sub GetIdentifiedAssets()
        Dim objBALTransDataTemp As New BALTransDataTemp
        Dim objBALLocation As New BALLocation
        Dim objBALDevices As New BALDevices
        Dim objBALCategory As New BALCategory

        Dim dt As DataTable = objBALTransDataTemp.GetAll_AssetsTempGrid()

        Dim dcSel As DataColumn = dt.Columns.Add("Selection", Type.GetType("System.Boolean"))
        Dim dcPrevLoc As DataColumn = dt.Columns.Add("PreviousLocation", Type.GetType("System.String"))
        Dim dcCurrentLoc As DataColumn = dt.Columns.Add("CurrentLocation", Type.GetType("System.String"))
        Dim dcDeviceDesc As DataColumn = dt.Columns.Add("DeviceDesc", Type.GetType("System.String"))
        Dim dcCat As DataColumn = dt.Columns.Add("Category", Type.GetType("System.String"))
        dcSel.SetOrdinal(0)
        For i As Integer = 0 To dt.Rows.Count - 1
            dt.Rows(i)("Selection") = False

            If String.IsNullOrEmpty(dt.Rows(i)("FromLoc").ToString) Then
                dt.Rows(i)("PreviousLocation") = objBALLocation.GetLocFullPath(dt.Rows(i)("LocID").ToString)
            Else
                dt.Rows(i)("PreviousLocation") = objBALLocation.GetLocFullPath(dt.Rows(i)("FromLoc").ToString)
            End If

            If String.IsNullOrEmpty(dt.Rows(i)("ToLoc").ToString) Then
                dt.Rows(i)("CurrentLocation") = objBALLocation.GetLocFullPath(dt.Rows(i)("LocID").ToString)
            Else
                dt.Rows(i)("CurrentLocation") = objBALLocation.GetLocFullPath(dt.Rows(i)("ToLoc").ToString)
            End If
            dt.Rows(i)("DeviceDesc") = objBALDevices.GetDeviceDescription(dt.Rows(i)("DeviceID").ToString)
            dt.Rows(i)("Category") = objBALCategory.GetCatFullPath(dt.Rows(i)("AstCatID").ToString)
        Next
        grdView.Columns.Clear()
        grd.DataSource = dt
        FormatGridIdentifiedAssets()
    End Sub


    Private Sub FormatGridIdentifiedAssets()
        With grdView
            'grdView.Columns.Clear()
            .OptionsBehavior.Editable = True

            .Columns(0).Caption = "#"
            .Columns(0).Width = 25
            .Columns(0).OptionsColumn.AllowEdit = True


            .Columns(1).Caption = "AstID"
            .Columns(1).OptionsColumn.AllowEdit = False
            .Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

            .Columns(2).Caption = "Description"
            .Columns(2).OptionsColumn.AllowEdit = False

            .Columns(3).Visible = False
            .Columns(3).OptionsColumn.AllowEdit = False

            .Columns(4).Visible = False
            .Columns(4).OptionsColumn.AllowEdit = False

            .Columns(5).Visible = False
            .Columns(6).Visible = False
            .Columns(7).Visible = False

            .Columns(8).OptionsColumn.AllowEdit = False
            .Columns(8).Caption = "Status"
            .Columns(8).Width = 75
            .Columns(8).Visible = True
            .Columns(8).ColumnEdit = RIType
            .Columns(8).VisibleIndex = 1
            .Columns("IsDataChanged").Visible = False
            .Columns("Pieces").Visible = False

            .Columns("InventoryDate").OptionsColumn.AllowEdit = False

            .Columns("AstTransID").Visible = False

            .Columns("CustodianID").Visible = True
            .Columns("CustodianID").OptionsColumn.AllowEdit = False
            .Columns("CustodianID").Caption = "Emp"

            .Columns("SerailNo").Visible = True
            .Columns("SerailNo").OptionsColumn.AllowEdit = False
            .Columns("SerailNo").Caption = "SerialNo"

            .Columns("PreviousLocation").Caption = "Previous Location"
            .Columns("PreviousLocation").OptionsColumn.AllowEdit = False

            .Columns("CurrentLocation").Caption = "Current Location"
            .Columns("CurrentLocation").OptionsColumn.AllowEdit = False

            .Columns("DeviceDesc").Caption = "Device"
            .Columns("DeviceDesc").OptionsColumn.AllowEdit = False

            .Columns("Category").Caption = "Category"
            .Columns("Category").OptionsColumn.AllowEdit = False

            .Columns("CostCenter").OptionsColumn.AllowEdit = False
        End With

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        addGridMenu(grd)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
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

    Private Sub cmbSch_SelectTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSch.SelectTextChanged
        If cmbSch.SelectedText <> "" Then
            valProvMain.RemoveControlError(cmbSch.TextBox)
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        If rdoAnonymous.Checked Then
            GetAnonymousAssets()
        Else
            GetIdentifiedAssets()
        End If
        valProvMain.RemoveControlError(cmbSch.TextBox)
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        For i As Integer = 0 To grdView.RowCount - 1
            grdView.SetRowCellValue(i, "Selection", True)
        Next
    End Sub

    Private Sub DeleteAnonymousData(ByVal showMessage As Boolean)
        Dim FocRow As Integer = grdView.FocusedRowHandle
        If FocRow >= 0 Then
            Dim id As String = grdView.GetRowCellValue(FocRow, "NonBCode").ToString
            Dim DeviceID As String = grdView.GetRowCellValue(FocRow, "DeviceID").ToString
            Dim TransDate As DateTime = CDate(grdView.GetRowCellValue(FocRow, "HisDate"))
            Dim objatt As New BALAnonymousAsset

            If showMessage Then
                If MessageBox.Show("Do you really want to delete this record ?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    If objatt.Delete_AnonymousAsset(id, DeviceID, TransDate) Then
                        GetAnonymousAssets()
                        If showMessage Then
                            ShowInfoMessage("Record deleted successfully")
                        End If
                    End If
                End If
            Else
                If objatt.Delete_AnonymousAsset(id, DeviceID, TransDate) Then
                    GetAnonymousAssets()

                End If
            End If
        Else
            ShowErrorMessage("No Record(s) selected.")
        End If
    End Sub
    Private Function GetGridSelectedRows(ByVal grd As DevExpress.XtraGrid.Views.Grid.GridView) As Integer
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Selection", True)
        Dim Count As Integer = grd.RowCount
        grd.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Selection", Nothing)
        Return Count
    End Function

    Private Sub DeleteIdentifedData()
        Dim objBALTransDataTemp As New BALTransDataTemp
        Try
            If GetGridSelectedRows(grdView) > 0 Then
                If MessageBox.Show("Do you really want to delete this record ?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    For i As Integer = 0 To grdView.RowCount - 1
                        If CBool(grdView.GetRowCellValue(i, "Selection")) = True Then
                            Dim DeviceID As String = grdView.GetRowCellValue(i, "DeviceID")

                            If objBALTransDataTemp.Delete_AssetTemp(grdView.GetRowCellValue(i, "AstID").ToString, DeviceID) Then
                                If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, "AstTransID").ToString) Then
                                    objBALTransDataTemp.Delete_AssetTransferTemp(grdView.GetRowCellValue(i, "AstTransID").ToString, DeviceID)
                                End If
                            End If

                        End If
                    Next
                    GetIdentifiedAssets()
                    ShowInfoMessage("Record deleted successfully")
                End If
            Else
                ShowErrorMessage("No Record(s) selected.")
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If rdoIdentified.Checked Then
            DeleteIdentifedData()
        Else 'Delete Anonymous data.
            DeleteAnonymousData(True)
        End If
    End Sub

    Private Sub btnProceed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProceed.Click
        If rdoIdentified.Checked AndAlso valProvMain.Validate Then
            grdView.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Selection", True)
            Dim RowCount As Integer = grdView.RowCount
            grdView.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Selection", Nothing)
            If RowCount > 0 Then
                '
                If MessageBox.Show("Are you sure to process the selected assets data?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Process_IdentifiedData(cmbSch.SelectedValue, pb)
                End If
            Else
                ShowErrorMessage("No Record(s) selected.")
            End If
        End If

        If rdoAnonymous.Checked AndAlso valProvMain.Validate Then
            Process_AnonymousData(cmbSch.SelectedValue)
        End If

    End Sub
    Private Function Process_AnonymousData(ByVal strInvCode As String) As Boolean
        Dim FocRow As Integer = grdView.FocusedRowHandle
        If FocRow >= 0 Then
            Dim AssetDesc As String = grdView.GetRowCellValue(FocRow, "Description").ToString
            Dim AnonymousId As String = grdView.GetRowCellValue(FocRow, "NonBCode").ToString
            Dim DeviceID As String = grdView.GetRowCellValue(FocRow, "DeviceID").ToString
            Dim TransDate As DateTime = CDate(grdView.GetRowCellValue(FocRow, "HisDate"))
            Dim Modle As String = grdView.GetRowCellValue(FocRow, "AstModel").ToString
            Dim serial As String = grdView.GetRowCellValue(FocRow, "SerailNo").ToString
            Dim LocID As String = grdView.GetRowCellValue(FocRow, "LocID").ToString
            Dim CatID As String = grdView.GetRowCellValue(FocRow, "AstCatID").ToString
            Dim InvID As Integer = CInt(cmbSch.SelectedValue)

            If ShowAssetDetailsAnonymous(AnonymousId, AssetDesc, LocID, CatID, Modle, serial, DeviceID, TransDate, InvID) Then
                DeleteAnonymousData(False)
            End If
        End If
    End Function

    Public Sub ShowAssetDetailsForm(ByVal AstID As String)
        Dim frm As New frmAssetsDetails
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.IsDemokey = IsDemokey
        frm.RoleID = RoleID
        frm.MdiParent = Me.MdiParent
        frm.BringToFront()
        frm.Show()
        frm.LocateAsset(AstID)
    End Sub

    Public Function ShowAssetDetailsAnonymous(ByVal AnonymousId As String, ByVal Desc As String, ByVal LocID As String, ByVal CatID As String, ByVal Modle As String, ByVal serial As String, ByVal DeviceID As String, ByVal TransDate As DateTime, ByVal InvID As Integer) As Boolean
        Dim frm As New frmAssetsDetails
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.IsDemokey = IsDemokey
        frm.RoleID = RoleID
        frm.ShowAnonymousInfo(AnonymousId, Desc, LocID, CatID, Modle, serial, DeviceID, TransDate, InvID)
        frm.ShowDialog()
        Dim AnonymousSaved As Boolean = frm.AnonymousSaved
        frm.Dispose()
        Return AnonymousSaved
    End Function

    Private Function Process_IdentifiedData(ByVal strInvCode As String, ByVal pb As ProgressBar) As Boolean
        Try
            pb.Visible = True
            pb.Step = 1
            pb.Value = 0

            Dim objBALTransDataTemp As New BALTransDataTemp
            Dim objattAssetDetails As attAssetDetails
            Dim objattAstHistory As attAstHistory
            Dim objBALAssetDetails As New BALAssetDetails
            Dim objBALAst_History As New BALAst_History
            Dim ObjCostCenter As New BALCostCenter

            grdView.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Selection", True)
            pb.Maximum = grdView.RowCount
            grdView.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "Selection", Nothing)
            'pb.Maximum = DtAssetTemp.Rows.Count
            Dim ProcessCount As Integer = 0
            Dim ProcessSucceed As Boolean = True
            For i As Integer = 0 To grdView.RowCount - 1
                If CBool(grdView.GetRowCellValue(i, "Selection")) = True Then
                    Try
                        objattAstHistory = New attAstHistory()
                        objattAstHistory.AstID = grdView.GetRowCellValue(i, "AstID").ToString
                        objattAstHistory.Status = CInt(grdView.GetRowCellValue(i, "Status"))
                        objattAstHistory.NoPiece = CInt(grdView.GetRowCellValue(i, "Pieces"))
                        objattAstHistory.InvSchCode = CType(strInvCode, Long)
                        objattAstHistory.HisDate = DateTime.Now.Date
                        Dim LastInventoryDate As DateTime = Now.Date
                        objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()

                        'We need to know the new location in Transfered and Misplaced status.
                        If objattAstHistory.Status = 3 Or objattAstHistory.Status = 2 Then ' 3 Transfered Asset, 2 Misplaced Asset.
                            Dim dtTransfTemp As DataTable = objBALTransDataTemp.GetAll_AssetTransferTemp(objattAstHistory.AstID)
                            For Each drtran As DataRow In dtTransfTemp.Rows
                                objattAstHistory.Fr_loc = drtran("FromLoc").ToString
                                objattAstHistory.To_Loc = drtran("ToLoc").ToString
                                objattAstHistory.HisDate = CDate(drtran("TransDate"))
                                LastInventoryDate = objattAstHistory.HisDate
                            Next
                        Else
                            objattAstHistory.Fr_loc = grdView.GetRowCellValue(i, "LocID").ToString()
                            objattAstHistory.To_Loc = grdView.GetRowCellValue(i, "LocID").ToString()
                        End If

                        'If AppConfig.ExportToServer Then
                        '    UpdateExportServer(dr("AstID").ToString(), objattAstHistory.Fr_loc, objattAstHistory.To_Loc, "", "", True)
                        'End If

                        Dim str As Integer = objBALAst_History.Check_Child(objattAstHistory, False)
                        If str > 0 Then
                            objBALAst_History.Update_Ast_History(objattAstHistory)
                        Else
                            objBALAst_History.Insert_Ast_History(objattAstHistory)
                        End If



                        objattAssetDetails = New attAssetDetails
                        objattAssetDetails.LocID = grdView.GetRowCellValue(i, "LocID").ToString()
                        objattAssetDetails.PKeyCode = grdView.GetRowCellValue(i, "AstID").ToString()
                        objattAssetDetails.InvStatus = CInt(grdView.GetRowCellValue(i, "Status"))

                        objattAssetDetails.AstDesc = grdView.GetRowCellValue(i, "AstDesc")
                        If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, "CustodianID")) Then
                            objattAssetDetails.CustodianID = ImportEmp(grdView.GetRowCellValue(i, "CustodianID"))
                        End If
                        objattAssetDetails.SerailNo = grdView.GetRowCellValue(i, "SerailNo")

                        Dim CostCenterNumber As String = grdView.GetRowCellValue(i, "CostCenter")
                        Dim CostCenterID As String = ObjCostCenter.GetCodeIDByNumber(CostCenterNumber)

                        If String.IsNullOrEmpty(CostCenterID) Or CostCenterID = 0 Then
                            Dim objattCostCenter As New attCostCenter
                            objattCostCenter.PKeyCode = ObjCostCenter.GetNextPKey_CostCenter
                            objattCostCenter.CostNumber = CostCenterNumber
                            objattCostCenter.CostName = CostCenterNumber
                            objattCostCenter.CompanyID = 0
                            ObjCostCenter.Insert_CostCenter(objattCostCenter)
                            objattAssetDetails.CostCenterID = objattCostCenter.PKeyCode
                        Else
                            objattAssetDetails.CostCenterID = CostCenterID
                        End If

                        objattAssetDetails.InvSchCode = CType(strInvCode, Long)
                        objattAssetDetails.IsDataChanged = grdView.GetRowCellValue(i, "IsDataChanged")
                        objattAssetDetails.LastInventoryDate = LastInventoryDate
                        If grdView.GetRowCellValue(i, "LastEditBY") IsNot Nothing Then
                            objattAssetDetails.LastEditBY = grdView.GetRowCellValue(i, "LastEditBY").ToString
                        End If
                        objBALAssetDetails.Update_InvStatus(objattAssetDetails)
                        'Delete the data after processing it.
                        Dim DeviceID As String = grdView.GetRowCellValue(i, "DeviceID")
                        objBALTransDataTemp.Delete_AssetTemp(grdView.GetRowCellValue(i, "AstID").ToString, DeviceID)
                        If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, "AstTransID").ToString) Then
                            objBALTransDataTemp.Delete_AssetTransferTemp(grdView.GetRowCellValue(i, "AstTransID").ToString, DeviceID)
                        End If
                        pb.PerformStep()
                        Application.DoEvents()
                        ProcessCount += 1
                    Catch ex As Exception
                        ProcessSucceed = False
                        ProcessCount = 0
                        Exit For
                    End Try
                End If
            Next
            pb.Visible = False
            If ProcessSucceed Then
                GetIdentifiedAssets()
                MessageBox.Show("Data Processed Sucessfully", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If


            Return ProcessSucceed
        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Function

    Private Function ImportEmp(ByVal EmpCode As String) As String
        Dim objattCustodian As New attCustodian
        Dim objBALCustodian As New BALCustodian
        objattCustodian.PKeyCode = EmpCode
        Dim dt As DataTable = objBALCustodian.CheckID(objattCustodian)
        Dim CustodianID As String = ""
        If dt.Rows.Count > 0 Then
            CustodianID = dt.Rows(0)("CustodianID")
        Else
            objattCustodian.CustodianName = EmpCode
            objattCustodian.DepartmentID = 3
            objattCustodian.DesignationID = 1
            objattCustodian.CustodianCode = ""
            objBALCustodian.Insert_Custodian(objattCustodian)
            CustodianID = objattCustodian.PKeyCode
        End If
        Return CustodianID
    End Function

    Private Sub btnOpenAsset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenAsset.Click
        Dim intRow As Integer = grdView.FocusedRowHandle
        If intRow >= 0 Then
            ShowAssetDetailsForm(grdView.GetRowCellValue(intRow, "AstID").ToString)
        End If
    End Sub


    Private Sub grdView_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdView.DoubleClick
        btnOpenAsset.PerformClick()
    End Sub

    Private Sub rdoIdentified_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoIdentified.CheckedChanged
        If rdoIdentified.Checked Then
            btnOpenAsset.Visible = True
            btnSelectAll.Visible = True
            GetIdentifiedAssets()
        End If
    End Sub

    Private Sub rdoAnonymous_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoAnonymous.CheckedChanged
        If rdoAnonymous.Checked Then
            btnOpenAsset.Visible = False
            btnSelectAll.Visible = False
            GetAnonymousAssets()
        End If
    End Sub

    Private Sub btnPrintPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintPreview.Click
        With SaveFileDialog1
            .CheckPathExists = True
            .FileName = ""
            .DefaultExt = "xls"
            .Filter = "Excel Sheet (*.xls)|*.xls"
            .Title = "Excel Sheet File"

            If .ShowDialog() = DialogResult.OK Then
                grdView.ExportToXls(.FileName)
            End If
            .Dispose()
        End With
    End Sub

End Class