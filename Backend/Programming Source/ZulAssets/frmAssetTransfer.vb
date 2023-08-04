Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraReports.UI
Imports System.IO


Public Class frmAssetTransfer
#Region " -- Decleration --"
    Dim SelNode As TreeNode
    Dim objattCustodian As attCustodian
    Dim objBALCustodian As New BALCustodian
    Dim objattLocation As attLocation
    Dim objBALLocation As New BALLocation
    Dim objattAssetDetails As attAssetDetails
    Dim objBALAssetDetails As New BALAssetDetails

    Dim dtAssets As New DataTable
    Private RitmAssets As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit

#End Region

    Private Sub frmAssetTransfer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmAssetTransfer = Nothing
    End Sub

    Private Sub InitDTAssets()
        grv.OptionsBehavior.Editable = True
        dtAssets.Columns.Add("Barcode", System.Type.GetType("System.String"))
        dtAssets.Columns.Add("AstID", System.Type.GetType("System.String"))
        dtAssets.Columns.Add("AstNum", System.Type.GetType("System.String"))
        dtAssets.Columns.Add("AstDesc", System.Type.GetType("System.String"))
        dtAssets.Columns.Add("SerailNo", System.Type.GetType("System.String"))
        dtAssets.Columns.Add("AstCustodian", System.Type.GetType("System.String"))
        dtAssets.Columns.Add("AstLocation", System.Type.GetType("System.String"))
        dtAssets.Columns.Add("AstCategory", System.Type.GetType("System.String"))
        dtAssets.Columns.Add("CustodianID", System.Type.GetType("System.String"))
        dtAssets.Columns.Add("LocID", System.Type.GetType("System.String"))
        dtAssets.Columns.Add("ToCustodian", System.Type.GetType("System.String"))
        dtAssets.Columns.Add("ToCustodianID", System.Type.GetType("System.String"))

        dtAssets.Columns.Add("ToLocation", System.Type.GetType("System.String"))
        dtAssets.Columns.Add("ToLocID", System.Type.GetType("System.String"))
        dtAssets.AcceptChanges()
        grd.DataSource = dtAssets

        grv.Columns("Barcode").OptionsColumn.AllowEdit = True
        grv.Columns("Barcode").ColumnEdit = RitmAssets
        RemoveHandler RitmAssets.CloseUp, AddressOf RitmAssets_CloseUp
        AddHandler RitmAssets.CloseUp, AddressOf RitmAssets_CloseUp
        RemoveHandler RitmAssets.KeyDown, AddressOf RitmAssets_KeyDown
        AddHandler RitmAssets.KeyDown, AddressOf RitmAssets_KeyDown

        RitmAssets.View.OptionsView.ShowIndicator = False
        RitmAssets.PopupFormMinSize = New Size(700, 400)
        RitmAssets.DataSource = objBALAssetDetails.GetAssetData_RepItemList
        GetGridColumn(RitmAssets.View, "LocID").Visible = False
        GetGridColumn(RitmAssets.View, "CustodianID").Visible = False

        RitmAssets.DisplayMember = GetGridColumnName("Barcode")
        RitmAssets.NullText = String.Empty
        RitmAssets.ValueMember = GetGridColumnName("Barcode")

        'RitmAssets.View.Columns("GUID").Visible = False

        RitmAssets.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        RitmAssets.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never
        RitmAssets.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.StartsWith
        RitmAssets.ValidateOnEnterKey = True

        grv.Columns("AstCategory").OptionsColumn.AllowEdit = False
        grv.Columns("AstLocation").OptionsColumn.AllowEdit = False
        grv.Columns("AstCustodian").OptionsColumn.AllowEdit = False
        grv.Columns("AstDesc").OptionsColumn.AllowEdit = False
        grv.Columns("AstID").OptionsColumn.AllowEdit = False
        grv.Columns("AstNum").OptionsColumn.AllowEdit = False
        dtAssets.Columns("SerailNo").Caption = "Serial#"
        grv.Columns("LocID").Visible = False
        grv.Columns("CustodianID").Visible = False

        grv.Columns("ToCustodian").Visible = False
        grv.Columns("ToCustodianID").Visible = False

        grv.Columns("ToLocation").Visible = False
        grv.Columns("ToLocID").Visible = False
        grv.BestFitMaxRowCount = 100
        grv.BestFitColumns()
    End Sub

    Private Sub RitmAssets_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            Dim FocRow As Integer = grv.FocusedRowHandle

            Dim obj As Object = CType(sender, DevExpress.XtraEditors.GridLookUpEdit).EditValue
            If obj IsNot DBNull.Value Then
                'If TypeOf (obj) Is DataRowView Then
                '    obj = CType(obj, DataRowView).Row("Barcode")
                'End If
                If LoadGridItemInfo(obj, FocRow) Then
                    'SaveGridItemInfo(FocRow)
                    'Grid_ReadonlyCells()
                    'grvItems.FocusedColumn = grvItems.VisibleColumns(4)
                    'grvItems.ShowEditor()
                    grv.PostEditor()
                    grv.UpdateCurrentRow()
                    grv.AddNewRow()
                End If
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub RitmAssets_CloseUp(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.CloseUpEventArgs)
        Dim FocRow As Integer = grv.FocusedRowHandle
        Dim obj As Object = e.Value
        If obj IsNot DBNull.Value Then
            'If TypeOf (obj) Is DataRowView Then
            '    obj = CType(obj, DataRowView).Row("Barcode")
            'End If
            If LoadGridItemInfo(obj, FocRow) Then
                'SaveGridItemInfo(FocRow)
                'Grid_ReadonlyCells()
                'grvItems.FocusedColumn = grvItems.VisibleColumns(4)
                'grvItems.ShowEditor()
                grv.PostEditor()
                grv.UpdateCurrentRow()
                grv.AddNewRow()
            End If
            e.AcceptValue = True
        End If
    End Sub

    Private Function LoadGridItemInfo(ByVal obj As Object, ByVal FocRow As Integer) As Boolean
        Try
            grv.SetColumnError(grv.Columns("Barcode"), Nothing)
            'Check if Assets exists in the Grid.
            Dim grvhandle As Long = grv.LocateByValue(0, grv.Columns("Barcode"), obj)
            If grvhandle <> DevExpress.XtraGrid.GridControl.InvalidRowHandle Then
                grv.SetRowCellValue(FocRow, "Barcode", Nothing)
                grv.SetRowCellValue(FocRow, "AstID", String.Empty)
                grv.SetRowCellValue(FocRow, "AstNum", String.Empty)
                grv.SetRowCellValue(FocRow, "AstDesc", String.Empty)
                grv.SetRowCellValue(FocRow, "AstCustodian", String.Empty)
                grv.SetRowCellValue(FocRow, "AstLocation", String.Empty)
                grv.SetRowCellValue(FocRow, "AstCategory", String.Empty)
                grv.SetRowCellValue(FocRow, "CustodianID", String.Empty)
                grv.SetRowCellValue(FocRow, "LocID", String.Empty)
                grv.SetRowCellValue(FocRow, "SerailNo", String.Empty)
                'SerailNo
                grv.SetColumnError(grv.Columns("Barcode"), "Asset already exists!")
                Return False
            Else
                Dim Rowhandle As Long = RitmAssets.View.LocateByValue(0, GetGridColumn(RitmAssets.View, "Barcode"), obj)
                If Rowhandle <> DevExpress.XtraGrid.GridControl.InvalidRowHandle Then
                    grv.SetRowCellValue(FocRow, "Barcode", GetGridRowCellValue(RitmAssets.View, Rowhandle, "Barcode"))
                    grv.SetRowCellValue(FocRow, "AstID", GetGridRowCellValue(RitmAssets.View, Rowhandle, "AstID"))
                    grv.SetRowCellValue(FocRow, "AstNum", GetGridRowCellValue(RitmAssets.View, Rowhandle, "AstNum"))
                    grv.SetRowCellValue(FocRow, "AstDesc", GetGridRowCellValue(RitmAssets.View, Rowhandle, "AstDesc"))
                    grv.SetRowCellValue(FocRow, "AstCustodian", GetGridRowCellValue(RitmAssets.View, Rowhandle, "CustodianName"))
                    grv.SetRowCellValue(FocRow, "AstLocation", GetGridRowCellValue(RitmAssets.View, Rowhandle, "LocationFullPath"))
                    grv.SetRowCellValue(FocRow, "AstCategory", GetGridRowCellValue(RitmAssets.View, Rowhandle, "CatFullPath"))
                    grv.SetRowCellValue(FocRow, "CustodianID", GetGridRowCellValue(RitmAssets.View, Rowhandle, "CustodianID"))
                    grv.SetRowCellValue(FocRow, "SerailNo", GetGridRowCellValue(RitmAssets.View, Rowhandle, "SerailNo"))
                    grv.SetRowCellValue(FocRow, "LocID", GetGridRowCellValue(RitmAssets.View, Rowhandle, "LocID"))

                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub frmAssetTransfer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        cmbLocation.Enabled = False

        InitDTAssets()

    End Sub

#Region "-- Methods --"

    Private Function Get_Location_ById(ByVal _Id As String) As Boolean
        Try
            Dim ds As New DataTable

            objattLocation = New attLocation
            objattLocation.PKeyCode = _Id
            ds = objBALLocation.GetComboLocations(objattLocation)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function
#End Region


    Private Sub ZTLocation_TreeBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLocation.TreeBtnClick
        Try
            Dim ds As New DataTable
            Dim objBALLocation As New BALLocation
            cmbLocation.DataSource = objBALLocation.GetComboLocations(New attLocation)
            cmbLocation.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        FormController.objfrmAssetTransfer = Nothing
        Me.Dispose()
    End Sub
    Private Sub chkLoc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLoc.CheckedChanged
        cmbLocation.Enabled = chkLoc.Checked
        If Not cmbLocation.Enabled Then
            cmbLocation.SelectedText = ""
            cmbLocation.SelectedValue = ""
        End If
    End Sub

    Private Sub chkCust_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCust.CheckedChanged
        cmbCust.Enabled = chkCust.Checked
        If Not cmbCust.Enabled Then
            cmbCust.SelectedText = ""
            cmbCust.SelectedValue = ""
        End If
    End Sub

    Private Sub chkChangeStatus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkChangeStatus.CheckedChanged
        cmbAssetStatus.Enabled = chkChangeStatus.Checked
    End Sub
    Private Function Get_AssetsDetails_byID(ByVal _id As String) As DataTable
        Try
            Dim ds As New DataTable
            objattAssetDetails = New attAssetDetails
            objattAssetDetails.PKeyCode = _id

            ds = objBALAssetDetails.GetAsset_Details(objattAssetDetails)
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

#Region "Update functions"


    Private Sub UpdateAssetLocations(ByVal oldLoc As String, ByVal NewLoc As String, ByVal AstID As String)
        Try
            Dim objattAstHistory As New attAstHistory
            Dim objBALAst_History As New BALAst_History
            objattAssetDetails = New attAssetDetails
            If AstID <> "" Then
                objattAstHistory.AstID = AstID
                objattAstHistory.Status = 3
                objattAstHistory.InvSchCode = 1
                objattAstHistory.HisDate = Now.Date
                objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                objattAstHistory.Fr_loc = oldLoc
                objattAstHistory.To_Loc = NewLoc
                objBALAst_History.Insert_Ast_History(objattAstHistory)

                objattAssetDetails.PKeyCode = AstID
                objattAssetDetails.LocID = NewLoc
                objBALAssetDetails.Update_Location(objattAssetDetails)

            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub UpdateExportServer(ByVal oldLoc As String, ByVal NewLoc As String, ByVal oldCust As String, ByVal NewCust As String, ByVal AstID As String)
        Try
            Dim objAlHadaIntegration As New BALAlhadaIntegration
            Dim objattAssetDetails1 As attAssetDetails
            Dim objBALAst_History As New BALAst_History

            If AstID <> "" Then

                objattAssetDetails1 = New attAssetDetails
                objattAssetDetails1.PKeyCode = AstID
                Dim ds As DataTable = objBALAssetDetails.GetAll_AssetDetails(objattAssetDetails)
                objattAssetDetails1.RefNo = ds.Rows(0)("RefNo").ToString
                If ds.Rows(0)("SerailNo").ToString <> "" Then
                    objattAssetDetails1.SerailNo = ds.Rows(0)("SerailNo").ToString
                End If

                If ds.Rows(0)("PONumber").ToString <> "" Then
                    objattAssetDetails1.PONumber = ds.Rows(0)("PONumber").ToString
                End If

                If ds.Rows(0)("AstDesc").ToString <> "" Then
                    objattAssetDetails1.AstDesc = ds.Rows(0)("AstDesc").ToString
                End If

                If ds.Rows(0)("DispDate").ToString <> "" Then
                    objattAssetDetails1.DispDate = CDate(ds.Rows(0)("DispDate"))
                End If

                objattAssetDetails1.GLCode = CInt(ds.Rows(0)("GLCode"))
                objattAssetDetails1.PurDate = CDate(ds.Rows(0)("PurDate"))
                objattAssetDetails1.Disposed = CBool(ds.Rows(0)("Disposed"))


                Dim Division As String = objBALAssetDetails.Get_Cust_DeptID(NewCust)
                Dim PrevDivision As String = objBALAssetDetails.Get_Cust_DeptID(oldCust)
                Dim Category As String = objBALAssetDetails.Get_Assets_AstCatID(objattAssetDetails1.ItemCode)

                objAlHadaIntegration.Insert_AssetChange_ExportServer(objattAssetDetails1, Division, PrevDivision, NewLoc, oldLoc, Category, Now.Date)
                objAlHadaIntegration.Update_AssetMaster_ExportServer(objattAssetDetails1.RefNo, Division, PrevDivision, NewLoc, oldLoc, Now.Date)
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub


    Private Sub UpdateAssetCustodian(ByVal oldCust As String, ByVal NewCust As String, ByVal AstID As String)
        Try
            Dim objattAst_Cust_history As New attAst_Cust_history
            Dim objBALAst_Cust_history As New BALAst_Cust_history
            objattAssetDetails = New attAssetDetails

            If AstID <> "" Then
                objattAssetDetails.PKeyCode = AstID
                objattAssetDetails.CustodianID = NewCust
                objattAst_Cust_history.AstID = AstID
                objattAst_Cust_history.HisDate = Now.Date
                objattAst_Cust_history.PKeyCode = objBALAst_Cust_history.GetNextPKey_AstHistory()
                objattAst_Cust_history.Fr_Cust = oldCust
                objattAst_Cust_history.To_Cust = NewCust
                objBALAst_Cust_history.Insert_Ast_Cust_history(objattAst_Cust_history)
                objBALAssetDetails.Update_Custodian(objattAssetDetails)
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub


#End Region

    Private Sub btnTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrans.Click
        Try
            If grv.RowCount > 0 Then
                If chkCust.Checked Or chkLoc.Checked Or chkChangeStatus.Checked Then
                    If (chkCust.Checked And cmbCust.SelectedValue <> "") Or (chkLoc.Checked And cmbLocation.SelectedText <> "") Or (chkChangeStatus.Checked And cmbAssetStatus.SelectedText <> "") Then
                        'If (chkLoc.Checked = True And ZTLocation.SelectedValue = lblLoc.Tag) Then
                        '    'ZulMessageBox.ShowMe("TransferSame")
                        '    errProv.SetError(ZTLocation.TextBox, My.MessagesResource.Messages.TransferSame)
                        'ElseIf (chkCust.Checked = True And cmbCust.SelectedValue = lblCust.Tag) Then
                        '    errProv.SetError(cmbCust.TextBox, My.MessagesResource.Messages.TransferSame)
                        'Else
                        'End If
                        If MessageBox.Show("Are you sure to transfer the assets?", " ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                            grv.PostEditor()
                            grv.UpdateCurrentRow()
                            If Tranfer_Now() Then
                                dtAssets.Rows.Clear()
                                grd.DataSource = dtAssets
                                RitmAssets.DataSource = objBALAssetDetails.GetAssetData_RepItemList
                                chkCust.Checked = False
                                chkLoc.Checked = False
                                ZulMessageBox.ShowMe("AssetTransferred")
                            End If
                        End If
                    Else
                        ZulMessageBox.ShowMe("TransferReqInfo")
                    End If
                Else
                    ZulMessageBox.ShowMe("SelectTranfer")
                End If
            Else
                ZulMessageBox.ShowMe("AssetFirst")
                ' errProv.SetError(AssetsID.TextBox, My.MessagesResource.Messages.AssetFirst)
                'ZulMessageBox.ShowMe("AssetFirst")
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Function Tranfer_Now() As Boolean
        Dim OldLoc As String = ""
        Dim NewLoc As String = ""
        Dim OldCust As String = ""
        Dim NewCust As String = ""
        Dim AssetID As String = ""
        Try

            For i As Integer = 0 To grv.RowCount - 1
                AssetID = grv.GetRowCellValue(i, "AstID")
                If AppConfig.ExportToServer Then
                    If cmbCust.SelectedValue <> "" And chkCust.Checked Then
                        NewCust = cmbCust.SelectedValue
                        OldCust = grv.GetRowCellValue(i, "CustodianID")
                    End If

                    If cmbLocation.SelectedText <> "" And chkLoc.Checked Then
                        NewLoc = cmbLocation.SelectedValue
                        OldLoc = grv.GetRowCellValue(i, "LocID")
                    End If

                    UpdateExportServer(OldLoc, NewLoc, OldCust, NewCust, AssetID)
                End If

                If cmbCust.SelectedValue <> "" And chkCust.Checked Then
                    NewCust = cmbCust.SelectedValue
                    OldCust = grv.GetRowCellValue(i, "CustodianID")
                    UpdateAssetCustodian(OldCust, NewCust, AssetID)
                End If

                If cmbLocation.SelectedText <> "" And chkLoc.Checked Then
                    OldLoc = grv.GetRowCellValue(i, "LocID")
                    NewLoc = cmbLocation.SelectedValue
                    UpdateAssetLocations(OldLoc, NewLoc, AssetID)
                End If

                If cmbAssetStatus.SelectedText <> "" And chkChangeStatus.Checked Then
                    objattAssetDetails = New attAssetDetails
                    objattAssetDetails.PKeyCode = AssetID
                    objattAssetDetails.StatusID = cmbAssetStatus.SelectedValue
                    objBALAssetDetails.Update_Status(objattAssetDetails)
                End If
            Next
            Return True
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
            Return False
        End Try
    End Function

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

    Public Sub SaveReportToDatabase(ByVal ReportName As String, ByVal report As DevExpress.XtraReports.UI.XtraReport)
        Dim objattRptFile As attReports
        Dim objBALRptFile As New BALReports
        objattRptFile = New attReports
        objattRptFile.ReportName = ReportName

        ' Save the report to a stream.
        Dim stream As New MemoryStream()
        report.SaveLayout(stream)
        ' Prepare the stream for reading.
        stream.Position = 0
        ' Insert the report to a database.
        Using sr As New StreamReader(stream)
            ' Read the report from the stream to a string variable.
            objattRptFile.ReportData = sr.ReadToEnd()
            If objBALRptFile.ReportNameExist(objattRptFile, True) Then
                objBALRptFile.Update_ReportFile(objattRptFile)
            Else
                objBALRptFile.Insert_ReportFile(objattRptFile)
            End If
        End Using
    End Sub

    Private Function LoadReport() As XtraReport
        Dim objBALOrgHier As New BALOrgHier
        'make sure the report is found before showing it, and show message.
        Dim objBALRptFile As New BALReports
        Dim rpt As New XtraReport
        ' Retrieve a string which contains the report.
        Dim ds As DataTable = objBALRptFile.GetReportFileData(ReportNames.AssetIssuance)
        If ds.Rows.Count <= 0 Then
            SaveReportToDatabase(ReportNames.AssetIssuance, New rptAssetIssuance)
            ds = objBALRptFile.GetReportFileData(ReportNames.AssetIssuance)
        End If

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
        End If

        rpt.FindControl("lblCompName", False).Text = AppConfig.CompanyName
        CType(rpt.FindControl("picLogo", False), DevExpress.XtraReports.UI.XRPictureBox).Image = CompanyLogoImage
        rpt.PrinterName = AppConfig.ReportPrinter
        rpt.PrintingSystem.ShowMarginsWarning = False
        rpt.DataSource = CType(grd.DataSource, DataTable)
        rpt.FindControl("txtToLocation", False).Text = cmbLocation.SelectedText
        Return rpt
        'rpt.txtToLocation.Text = cmbLocation.SelectedText
        'rpt.txtReceiverName.Text = Me.cmbCust.SelectedText
    End Function

    Private Sub btnPrintReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintReport.Click
        If grv.RowCount > 0 And (Not String.IsNullOrEmpty(cmbLocation.SelectedText) Or Not String.IsNullOrEmpty(cmbCust.SelectedText)) Then
            Dim rpt As XtraReport = LoadReport()
            rpt.Print()
        End If
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If grv.RowCount > 0 And (Not String.IsNullOrEmpty(cmbLocation.SelectedText) Or Not String.IsNullOrEmpty(cmbCust.SelectedText)) Then
            Dim rpt As XtraReport = LoadReport()
            rpt.ShowPreviewDialog()
        End If
    End Sub

    Private Sub grv_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles grv.InitNewRow
        Dim View As ColumnView = grd.FocusedView
        Dim column As GridColumn = View.Columns("Barcode")

        If Not column Is Nothing Then
            View.FocusedRowHandle = e.RowHandle
            View.FocusedColumn = column
        End If
    End Sub

    Private Sub grv_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles grv.InvalidRowException
        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction
    End Sub

    Private Sub grv_ValidateRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles grv.ValidateRow
        Try
            e.Valid = True
            Dim Barcode As Object = grv.GetRowCellValue(e.RowHandle, "Barcode")
            If Barcode Is DBNull.Value OrElse String.IsNullOrEmpty(Barcode) Then
                Try
                    grv.DeleteRow(e.RowHandle)
                    Return
                Catch ex As Exception
                    GenericExceptionHandler(ex, WhoCalledMe)
                End Try
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub txtAssetStatus_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAssetStatus.LovBtnClick
        Try
            cmbAssetStatus.ValueMember = "ID"
            cmbAssetStatus.DisplayMember = "Status"
            cmbAssetStatus.DataSource = objBALAssetDetails.GetAssetStatus(False, True)
            cmbAssetStatus.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    'Private Sub cmbCustodian_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
    '    PopulateCustodian()
    'End Sub
    'Private Sub PopulateCustodian()
    '    Dim objBALCustodian As New BALCustodian
    '    cmbCustodian.Properties.ValueMember = "CustodianID"
    '    cmbCustodian.Properties.DisplayMember = "CustodianID"
    '    cmbCustodian.Properties.DataSource = objBALCustodian.GetAllData_GetCombo()
    'End Sub

    'Private Sub cmbCustodian_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'txtCustHier.Text =  GetCustHier(cmbCustodian.EditValue)
    'End Sub

    Private Sub cmbCust_SelectTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCust.SelectTextChanged
        For FocRow As Integer = 0 To grv.RowCount - 1
            grv.SetRowCellValue(FocRow, "ToCustodian", cmbCust.SelectedText)
            grv.SetRowCellValue(FocRow, "ToCustodianID", cmbCust.SelectedValue)
            grv.UpdateCurrentRow()
            grv.PostEditor()
        Next
    End Sub

    Private Sub cmbLocation_SelectTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLocation.SelectTextChanged
        For FocRow As Integer = 0 To grv.RowCount - 1
            grv.SetRowCellValue(FocRow, "ToLocation", cmbLocation.SelectedText)
            grv.SetRowCellValue(FocRow, "ToLocID", cmbLocation.SelectedValue)
            grv.UpdateCurrentRow()
            grv.PostEditor()
        Next
    End Sub
End Class