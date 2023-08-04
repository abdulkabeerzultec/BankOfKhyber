Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Windows.Forms

Public Class frmABBDataProcessing

    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider

    Private _RoleID As Integer
    Dim objattRole As attRoles = New attRoles
    Dim objBALRole As New BALRoles
    Dim Permission As Integer = 0

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

            valRulenotEmpty.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank
            valRulenotEmpty.ErrorText = "Please enter a value"
            valRulenotEmpty.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical

            Dim ds As DataTable
            objattRole.PKeyCode = RoleID
            ds = objBALRole.GetAll_Roles(objattRole)
            Dim ShowInventoryItems As Boolean = ds.Rows(0)("AssetItems")
            Dim ShowAuditingItems As Boolean = ds.Rows(0)("AssetsBooks")
            If ShowInventoryItems And Not ShowAuditingItems Then
                rdoAnonymous.Visible = False
                Permission = 1
            ElseIf Not ShowInventoryItems And ShowAuditingItems Then
                Permission = 2
            ElseIf ShowInventoryItems And ShowAuditingItems Then
                Permission = 3
            Else
                Permission = 0
            End If

            valProvMain.SetValidationRule(cmbSch.TextBox, valRulenotEmpty)
            'check the checkbox will load the data
            rdoIdentified.Checked = True
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub GetAnonymousAssets()
        Dim objBALIntegration As New BALABBIntegration
        Dim objBALLocation As New BALLocation
        Dim objBALDevices As New BALDevices
        Dim objBALCategory As New BALCategory

        Dim dt As DataTable = objBALIntegration.GetAll_AnonymousAsset()

        Dim dcSel As DataColumn = dt.Columns.Add("Selection", Type.GetType("System.Boolean"))
        dcSel.SetOrdinal(0)

        Dim dcCurrentPlant As DataColumn = dt.Columns.Add("Plant", Type.GetType("System.String"))
        Dim dcCurrentLoc As DataColumn = dt.Columns.Add("Location", Type.GetType("System.String"))
        Dim dcCat As DataColumn = dt.Columns.Add("Class", Type.GetType("System.String"))
        Dim dcDeviceDesc As DataColumn = dt.Columns.Add("DeviceDesc", Type.GetType("System.String"))
        For i As Integer = 0 To dt.Rows.Count - 1
            dt.Rows(i)("Selection") = False

            If Not String.IsNullOrEmpty(dt.Rows(i)("LocID").ToString) Then
                Dim Fulllocation As String = objBALLocation.GetLocFullPath(dt.Rows(i)("LocID").ToString)
                Dim strFulLoc As String() = Fulllocation.Split("\")
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
            dt.Rows(i)("Class") = objBALCategory.GetCatFullPath(dt.Rows(i)("AstCatID").ToString)
            dt.Rows(i)("DeviceDesc") = objBALDevices.GetDeviceDescription(dt.Rows(i)("DeviceID").ToString)
        Next
        grdView.Columns.Clear()
        grd.DataSource = dt

        FormatGridAnonymousAssetsABB()
  
    End Sub

    Private Sub FormatGridAnonymousAssetsABB()
        With grdView
            .OptionsBehavior.Editable = True
            For Each col As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                col.OptionsColumn.AllowEdit = False
            Next
            .Columns("Selection").Caption = "#"
            .Columns("Selection").Width = 25
            .Columns("Selection").OptionsColumn.AllowEdit = True
            .Columns("LocID").Visible = False
            .Columns("NonBCode").Visible = False
            .Columns("AstCatID").Visible = False
            .Columns("DeviceID").Visible = False

            .Columns("Remarks").VisibleIndex = .Columns.Count - 1
            .Columns("Remarks").OptionsColumn.AllowEdit = True
            .Columns("Remarks").MinWidth = 125

        End With
    End Sub


    Private Sub GetIdentifiedAssets()
        Dim objBALIntegration As New BALABBIntegration
        Dim objBALLocation As New BALLocation
        Dim objBALDevices As New BALDevices
        Dim objBALCategory As New BALCategory

        Dim dt As DataTable = objBALIntegration.GetAll_AssetsDataProcessingGrid()


        Dim dcSel As DataColumn = dt.Columns.Add("Selection", Type.GetType("System.Boolean"))
        dt.Columns.Add("AssetCode", Type.GetType("System.String"))
        dt.Columns.Add("SubAsset", Type.GetType("System.String"))
        dt.Columns.Add("CompanyCode", Type.GetType("System.String"))

        Dim dcPrevLoc As DataColumn = dt.Columns.Add("PreviousLocation", Type.GetType("System.String"))
        Dim dcCurrentLoc As DataColumn = dt.Columns.Add("CurrentLocation", Type.GetType("System.String"))
        Dim dcDeviceDesc As DataColumn = dt.Columns.Add("DeviceDesc", Type.GetType("System.String"))
        Dim dcCat As DataColumn = dt.Columns.Add("Category", Type.GetType("System.String"))
        Dim dcPermission As DataColumn = dt.Columns.Add("Permission", Type.GetType("System.String"))

        dcSel.SetOrdinal(0)
        dt.Columns("CompanyCode").SetOrdinal(1)
        dt.Columns("AssetCode").SetOrdinal(2)
        dt.Columns("SubAsset").SetOrdinal(3)
        For i As Integer = 0 To dt.Rows.Count - 1
            dt.Rows(i)("Selection") = False

            Dim CompleteCode As String = dt.Rows(i)("RefNo")
            Dim strCompleteCode As String() = CompleteCode.Split("-")

            dt.Rows(i)("CompanyCode") = strCompleteCode(0)
            If strCompleteCode.Length > 1 Then
                dt.Rows(i)("AssetCode") = strCompleteCode(1)
                If strCompleteCode.Length > 2 Then
                    dt.Rows(i)("SubAsset") = strCompleteCode(2)
                End If
            End If

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
            If Not dt.Rows(i).IsNull("LastEditBY") Then
                dt.Rows(i)("Permission") = GetUserPermission(dt.Rows(i)("LastEditBY"))
            Else
                dt.Rows(i)("Permission") = 2
            End If
        Next
        grdView.Columns.Clear()
        If Permission <> 3 Then ' 3= both
            grd.DataSource = New DataView(dt, "Permission = " & Permission.ToString, "AssetCode", DataViewRowState.CurrentRows)
        Else
            grd.DataSource = dt
        End If



        FormatGridIdentifiedAssets()

    End Sub

    ' This function check the given user role and return 4 values
    '0 None, 1 Invntory only, 2 Assets Only , 3 Both
    Private Function GetUserPermission(ByVal UserName As String) As Integer
        Dim objusr As New BALUsers
        Dim objattUser As New attUsers
        objattUser.LoginName = UserName
        Dim dt As DataTable = objusr.CheckID(objattUser)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objattRole.PKeyCode = dt.Rows(0)("RoleID")
            Dim ds As DataTable = objBALRole.GetAll_Roles(objattRole)
            Dim ShowInventoryItems As Boolean = ds.Rows(0)("AssetItems")
            Dim ShowAuditingItems As Boolean = ds.Rows(0)("AssetsBooks")
            If ShowInventoryItems And Not ShowAuditingItems Then
                Return 1
            ElseIf Not ShowInventoryItems And ShowAuditingItems Then
                Return 2
            ElseIf ShowInventoryItems And ShowAuditingItems Then
                Return 2
            Else
                Return 0
            End If
        Else
            Return 0
        End If
    End Function

    Private Sub FormatGridIdentifiedAssets()
        Dim RIType As New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
        RIType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() _
                                                    {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Missing Assets", 0, -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Found Assets", 1, -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Misplaced Assets", 2, -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Transferred Assets", 3, -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Allocated Assets", 4, -1)})

        With grdView
            .OptionsView.ColumnAutoWidth = False
            .OptionsBehavior.Editable = True
            For Each col As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                col.OptionsColumn.AllowEdit = False
            Next

            .Columns(0).Caption = "#"
            .Columns(0).Width = 25
            .Columns(0).OptionsColumn.AllowEdit = True


            .Columns("AstID").Caption = "Barcode"
            .Columns("AstID").Width = 100
            .Columns("AstID").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

            .Columns("AstDesc").Caption = "Description"
            .Columns("AstDesc").Width = 100

            .Columns("DeviceID").Visible = False
            .Columns("LocID").Visible = False
            .Columns("ToLoc").Visible = False
            .Columns("FromLoc").Visible = False
            .Columns("AstCatID").Visible = False
            .Columns("AstTransID").Visible = False
            .Columns("RefNo").Visible = False

            .Columns("Status").Caption = "Status"
            .Columns("Status").Width = 75
            .Columns("Status").Visible = True
            .Columns("Status").ColumnEdit = RIType

            .Columns("CustodianID").Visible = False '9

            .Columns("PreviousLocation").Caption = "Previous Location"

            .Columns("CurrentLocation").Caption = "Current Location"

            .Columns("DeviceDesc").Caption = "Device"

            .Columns("Category").Caption = "Class"
            .Columns("IsDataChanged").Visible = False
            .Columns("Permission").Visible = False
            .Columns("LastEditBY").Visible = True
        End With

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
            'FormatGridAnonymousAssets()
        Else
            GetIdentifiedAssets()
            'FormatGridIdentifiedAssets()
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
            Process_IdentifiedData(cmbSch.SelectedValue, pb)
        End If

        If rdoAnonymous.Checked AndAlso valProvMain.Validate Then
            Process_AnonymousData()
        End If
    End Sub
    Private Function Process_AnonymousData() As Boolean
        For i As Integer = 0 To grdView.RowCount - 1
            If CBool(grdView.GetRowCellValue(i, "Selection")) = True Then
                Dim AssetDesc As String = grdView.GetRowCellValue(i, "Description").ToString
                Dim AnonymousId As String = grdView.GetRowCellValue(i, "NonBCode").ToString
                Dim DeviceID As String = grdView.GetRowCellValue(i, "DeviceID").ToString
                Dim TransDate As DateTime = CDate(grdView.GetRowCellValue(i, "HisDate"))
                Dim serial As String = grdView.GetRowCellValue(i, "SerailNo").ToString
                Dim LocID As String = grdView.GetRowCellValue(i, "LocID").ToString
                Dim CatID As String = grdView.GetRowCellValue(i, "AstCatID").ToString
                Dim Remarks As String = grdView.GetRowCellValue(i, "Remarks").ToString
                Dim InvSchID As Integer = cmbSch.SelectedValue

                Dim objAnon As New BALAnonymousAsset
                objAnon.Update_AnonymousRemarks(AnonymousId, DeviceID, TransDate, Remarks, InvSchID)
            End If
        Next
        MessageBox.Show("Data Processed Sucessfully", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
        GetAnonymousAssets()
    End Function

    Private Function GetCustodianID(ByVal AstID As String) As String
        Dim objBALAssetDetails As New BALAssetDetails
        Dim objattAssetDetails As New attAssetDetails
        objattAssetDetails.PKeyCode = AstID
        Dim dt As DataTable = objBALAssetDetails.GetAsset_Details(objattAssetDetails)
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("CustodianID")
            Else
                Return String.Empty
            End If
        Else
            Return String.Empty
        End If
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
                        Dim AssetID As String = grdView.GetRowCellValue(i, "AstID").ToString
                        objattAstHistory.AstID = AssetID
                        objattAstHistory.Status = CInt(grdView.GetRowCellValue(i, "Status"))
                        objattAstHistory.InvSchCode = CType(strInvCode, Long)
                        If grdView.GetRowCellValue(i, "LastEditBY") IsNot Nothing Then
                            objattAstHistory.Remarks = grdView.GetRowCellValue(i, "LastEditBY").ToString
                        End If

                        Dim LastInventoryDate As DateTime = Nothing
                        ' LastInventory Date may come nothing from the frontend if the asset changed by GI form, after assign the serial number
                        If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, "InventoryDate").ToString) Then
                            LastInventoryDate = grdView.GetRowCellValue(i, "InventoryDate")

                            objattAstHistory.HisDate = LastInventoryDate
                            objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()

                            'We need to know the new location in Transfered and Misplaced status.
                            If objattAstHistory.Status = 3 Or objattAstHistory.Status = 2 Then ' 3 Transfered Asset, 2 Misplaced Asset.
                                Dim dtTransfTemp As DataTable = objBALTransDataTemp.GetAll_AssetTransferTemp(AssetID)
                                For Each drtran As DataRow In dtTransfTemp.Rows
                                    objattAstHistory.Fr_loc = drtran("FromLoc").ToString
                                    objattAstHistory.To_Loc = drtran("ToLoc").ToString
                                Next
                            Else
                                objattAstHistory.Fr_loc = grdView.GetRowCellValue(i, "LocID").ToString()
                                objattAstHistory.To_Loc = grdView.GetRowCellValue(i, "LocID").ToString()
                            End If


                            Dim str As Integer = objBALAst_History.Check_Child(objattAstHistory, False)
                            If str > 0 Then
                                objBALAst_History.Update_Ast_History(objattAstHistory)
                            Else
                                objBALAst_History.Insert_Ast_History(objattAstHistory)
                            End If

                        End If



                        objattAssetDetails = New attAssetDetails
                        objattAssetDetails.LocID = grdView.GetRowCellValue(i, "LocID").ToString()
                        objattAssetDetails.PKeyCode = grdView.GetRowCellValue(i, "AstID").ToString()

                        objattAssetDetails.AstDesc = grdView.GetRowCellValue(i, "AstDesc")

                        If Not String.IsNullOrEmpty(grdView.GetRowCellValue(i, "CustodianID")) Then
                            Dim OldCust As String = GetCustodianID(AssetID)
                            Dim NewCust As String = ImportEmp(grdView.GetRowCellValue(i, "CustodianID"))
                            If OldCust <> NewCust Then
                                Dim objCustHistory As New BALAst_Cust_history
                                Dim objattCustHist As New attAst_Cust_history
                                objattCustHist.PKeyCode = objCustHistory.GetNextPKey_AstHistory()
                                objattCustHist.AstID = AssetID
                                objattCustHist.HisDate = LastInventoryDate
                                objattCustHist.Fr_Cust = OldCust
                                objattCustHist.To_Cust = NewCust
                                objCustHistory.Insert_Ast_Cust_history(objattCustHist)
                            End If
                            objattAssetDetails.CustodianID = NewCust
                        End If

                        objattAssetDetails.SerailNo = grdView.GetRowCellValue(i, "SerailNo")

                        objattAssetDetails.IsDataChanged = grdView.GetRowCellValue(i, "IsDataChanged")
                        'If Annually Inventory then update the lastInventorydate otherwise don't update,According to ABB Requirements.
                        Dim objBALSch As New BALAst_INV_Schedule
                        Dim objattSch As New attInvSchedule
                        objattSch.PKeyCode = strInvCode
                        'If objBALSch.Get_ScheduleType(objattSch) = 1 Then 'Annually Inventory
                        If LastInventoryDate <> Nothing Then
                            objattAssetDetails.InvSchCode = strInvCode
                            objattAssetDetails.InvStatus = CInt(grdView.GetRowCellValue(i, "Status"))
                            objattAssetDetails.LastInventoryDate = LastInventoryDate
                            If grdView.GetRowCellValue(i, "LastEditBY") IsNot Nothing Then
                                objattAssetDetails.LastEditBY = grdView.GetRowCellValue(i, "LastEditBY").ToString
                            End If
                        Else ' if the lastInventory Date is nothing because of updating the serialNo from GI, then don't update the status
                            objattAssetDetails.InvSchCode = -1
                            objattAssetDetails.InvStatus = -1
                            objattAssetDetails.LastInventoryDate = Nothing
                            If grdView.GetRowCellValue(i, "LastEditBY") IsNot Nothing Then
                                objattAssetDetails.LastEditBY = grdView.GetRowCellValue(i, "LastEditBY").ToString
                            End If
                        End If
                        'Else
                        'objattAssetDetails.InvSchCode = -1
                        'objattAssetDetails.InvStatus = -1
                        'objattAssetDetails.LastInventoryDate = Nothing
                        'If grdView.GetRowCellValue(i, "LastEditBY") IsNot Nothing Then
                        '    objattAssetDetails.LastEditBY = grdView.GetRowCellValue(i, "LastEditBY").ToString
                        'End If
                        'End If

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


    Private Sub rdoIdentified_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoIdentified.CheckedChanged
        If rdoIdentified.Checked Then
            GetIdentifiedAssets()
        End If
    End Sub

    Private Sub rdoAnonymous_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoAnonymous.CheckedChanged
        If rdoAnonymous.Checked Then
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

    Private Sub frmDataProcessing_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Me.Dispose()
    End Sub

End Class