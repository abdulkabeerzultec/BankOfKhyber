Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class frmBackendInventory

    Private valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider

    Private objBALLocation As New BALLocation
    Private objBALCategory As New BALCategory
    Private objAssetDetails As New BALAssetDetails
    Private objAssetHistory As New BALAst_History

    Private RitmQTY As New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Private RIType As New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox

    Private Sub frmBackendInventory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        valProvMain.SetValidationRule(cmbSch.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(cmbLocation.TextBox, valRulenotEmpty)
        RIType.SmallImages = MainForm.imgAssetStatus
        RIType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() _
                                                    {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Missing Asset", "0", 0), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Found Asset", "1", 1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Misplaced Asset", "2", 2), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Transferred Asset", "3", 3), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Allocated Asset", "4", 4)})
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrintPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintPreview.Click
        If grvAssets.RowCount > 0 Then
            grdAssets.ShowPrintPreview()
        End If
    End Sub

    'Private Sub frmBackendInventory_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
   
    'End Sub

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


    Private Sub frmBackendInventory_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        FormController.objfrmBackendInventory = Nothing
    End Sub

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

    Private Sub cmbLocation_SelectTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLocation.SelectTextChanged
        If cmbLocation.SelectedText <> "" And cmbSch.SelectedText <> "" Then
            If valProvMain.Validate Then
                PopulateAssets(cmbLocation.SelectedValue, cmbSch.SelectedValue)
                grpAssets.Text = String.Format("Location Assets({0})", cmbLocation.SelectedText)
            Else
                grpAssets.Text = "Location Assets"
                ShowStatus(lblStatus, "Select Location from dropdown list first.", 1)
            End If
        End If
        ShowStatus(lblStatus, String.Empty, 1)
        imgInv.Image = Nothing
    End Sub

    Private Sub cmbSch_SelectTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSch.SelectTextChanged
        If cmbSch.SelectedText <> "" And cmbLocation.SelectedText <> "" Then
            If valProvMain.Validate Then
                PopulateAssets(cmbLocation.SelectedValue, cmbSch.SelectedValue)
            Else
                ShowStatus(lblStatus, "Select Inventory Schedule from the dropdownlist first.", 1)
            End If
        End If
        ShowStatus(lblStatus, String.Empty, 1)
        imgInv.Image = Nothing
    End Sub

    Private Sub PopulateAssets(ByVal LocationID As String, ByVal InvSchID As String)
        If Not String.IsNullOrEmpty(LocationID) And Not String.IsNullOrEmpty(InvSchID) Then
            Try

                Dim dt As DataTable = objAssetDetails.GetLocationAssets(LocationID, InvSchID)
                Dim dcPrevLoc As DataColumn = dt.Columns.Add("PreviousLocation", Type.GetType("System.String"))
                Dim dcCurrentLoc As DataColumn = dt.Columns.Add("CurrentLocation", Type.GetType("System.String"))
                Dim dcCat As DataColumn = dt.Columns.Add("Category", Type.GetType("System.String"))
                For i As Integer = 0 To dt.Rows.Count - 1
                    If String.IsNullOrEmpty(dt.Rows(i)("Fr_loc").ToString) Then
                        dt.Rows(i)("PreviousLocation") = objBALLocation.GetLocFullPath(dt.Rows(i)("LocID").ToString)
                    Else
                        dt.Rows(i)("PreviousLocation") = objBALLocation.GetLocFullPath(dt.Rows(i)("Fr_loc").ToString)
                    End If

                    If String.IsNullOrEmpty(dt.Rows(i)("To_Loc").ToString) Then
                        dt.Rows(i)("CurrentLocation") = objBALLocation.GetLocFullPath(dt.Rows(i)("LocID").ToString)
                    Else
                        dt.Rows(i)("CurrentLocation") = objBALLocation.GetLocFullPath(dt.Rows(i)("To_Loc").ToString)
                    End If
                    dt.Rows(i)("Category") = objBALCategory.GetCatFullPath(dt.Rows(i)("AstCatID").ToString)
                Next
                grvAssets.Columns.Clear()
                grdAssets.DataSource = dt
                FormatGrid()

            Catch ex As Exception
                lblStatus.Text = ex.Message
            End Try
        Else
            lblStatus.Text = "Location or inventory schedule is not selelcted, please select and try again."
        End If
    End Sub

    Private Sub FormatGrid()

        grvAssets.OptionsBehavior.Editable = True
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In grvAssets.Columns
            If col.FieldName <> GetGridColumnName("Pieces") Then
                col.OptionsColumn.AllowEdit = False
            End If
        Next

        GetGridColumn(grvAssets, "Fr_loc").Visible = False
        GetGridColumn(grvAssets, "To_Loc").Visible = False
        GetGridColumn(grvAssets, "LocID").Visible = False
        GetGridColumn(grvAssets, "AstCatID").Visible = False
        GetGridColumn(grvAssets, "Status").ColumnEdit = RIType
        GetGridColumn(grvAssets, "Status").MinWidth = 100
        GetGridColumn(grvAssets, "AssetDescription").MinWidth = 125
        RitmQTY.MinValue = 0
        RemoveHandler RitmQTY.EditValueChanged, AddressOf RitmQTYChanged
        AddHandler RitmQTY.EditValueChanged, AddressOf RitmQTYChanged
        RitmQTY.MaxValue = Integer.MaxValue
        GetGridColumn(grvAssets, "Pieces").ColumnEdit = RitmQTY
        grvAssets.BestFitColumns()
        addGridMenu(grdAssets)
    End Sub
    Private Sub RitmQTYChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FocRow As Integer = grvAssets.FocusedRowHandle
        Dim objNoPiece As Object = CType(sender, DevExpress.XtraEditors.SpinEdit).EditValue
        If objNoPiece IsNot DBNull.Value Then
            Dim Barcode As String = GetGridRowCellValue(grvAssets, FocRow, "Barcode")
            Dim dtAssetHisInfo As DataTable = objAssetDetails.GetAssetInventoryStatus(Barcode, cmbSch.SelectedValue)
            If dtAssetHisInfo.Rows.Count > 0 Then
                Dim Status As Int16 = dtAssetHisInfo.Rows(0)("Status")
                Dim AssetID As String = dtAssetHisInfo.Rows(0)("AstID")
                Dim NoPiece As Integer = dtAssetHisInfo.Rows(0)("NoPiece")
                Dim OldLocID As String = dtAssetHisInfo.Rows(0)("LocID")
                Dim To_Loc As String = dtAssetHisInfo.Rows(0)("To_Loc")
                Dim Fr_loc As String = dtAssetHisInfo.Rows(0)("Fr_loc")

                Dim attHistroy As New attAstHistory
                attHistroy.AstID = AssetID
                attHistroy.InvSchCode = cmbSch.SelectedValue
                attHistroy.Fr_loc = Fr_loc
                attHistroy.To_Loc = To_Loc
                attHistroy.NoPiece = objNoPiece
                attHistroy.Status = Status
                attHistroy.HisDate = Now.Date
                If objAssetHistory.Update_Ast_History(attHistroy) Then

                End If

            End If
        End If
    End Sub
    'Private Sub txtBarcode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBarcode.Leave
    '    If MainForm.ActiveMdiChild Is Me Then
    '        txtBarcode.Focus()
    '    End If
    'End Sub

    Private Sub txtBarcode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBarcode.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then

            Dim LocID As String = cmbLocation.SelectedValue
            Dim Barcode As String = txtBarcode.Text.Trim
            If (Barcode.StartsWith("LOC")) Then 'check for the Location
                LocID = Barcode.Replace("LOC", "").Trim

                If objBALLocation.GetLocFullPath(LocID).ToString <> "0" Then
                    cmbLocation.SelectedText = objBALLocation.GetLocFullPath(LocID)
                    cmbLocation.SelectedValue = LocID
                    lblStatus.Text = String.Empty
                Else ' When the location isinvalid
                    ShowStatus(lblStatus, "Invalid Location barcode.", 1)
                    cmbLocation.SelectedText = String.Empty
                    cmbLocation.SelectedValue = Nothing
                End If

            Else ' Check for the Item
                If valProvMain.Validate Then
                    If SetAssetStatus(Barcode, LocID, cmbSch.SelectedValue) Then
                        PopulateAssets(cmbLocation.SelectedValue, cmbSch.SelectedValue)
                        grvAssets.FocusedRowHandle = grvAssets.LocateByValue(0, GetGridColumn(grvAssets, "Barcode"), Barcode)
                    End If
                Else
                    lblStatus.Text = "Location or inventory schedule is not selelcted, please select and try again."
                End If
            End If
            txtBarcode.Text = String.Empty
            'Update Asset status.
            'Select the asset in the grid.
            'How to update the qty.
        End If
    End Sub

    Public Sub ShowStatus(ByVal ctl As System.Windows.Forms.Label, ByVal msg As String, ByVal msgType As Int16)
        Select Case msgType
            Case 1   'Critical Message
                ctl.ForeColor = System.Drawing.Color.Red
            Case 2   'Normal Message
                ctl.ForeColor = System.Drawing.Color.Green
            Case 3  'information message
                ctl.ForeColor = System.Drawing.Color.Blue
        End Select

        ctl.Text = msg
    End Sub

    Public Function SetAssetStatus(ByVal Barcode As String, ByVal LocationID As String, ByVal InvSchID As String) As Boolean
        Dim dtAssetHisInfo As DataTable = objAssetDetails.GetAssetInventoryStatus(Barcode, InvSchID)
        If dtAssetHisInfo.Rows.Count > 0 Then
            Dim Status As Int16 = dtAssetHisInfo.Rows(0)("Status")
            Dim AssetID As String = dtAssetHisInfo.Rows(0)("AstID")
            Dim NoPiece As Integer = dtAssetHisInfo.Rows(0)("PhysicalNoPiece")
            Dim OldLocID As String = dtAssetHisInfo.Rows(0)("LocID")
            Dim To_Loc As String = dtAssetHisInfo.Rows(0)("To_Loc")
            Dim Fr_loc As String = dtAssetHisInfo.Rows(0)("Fr_loc")
            Select Case Status
                Case 0, 1, 2, 3, 4
                    Dim attHistroy As New attAstHistory
                    If To_Loc = LocationID Then   'Item Belong to This Location
                        If Status = 3 Then
                            Status = 3 'Transfered
                            ShowStatus(lblStatus, "Already scanned as Transfered !.", 3)
                            imgInv.Image = MainForm.imgAssetStatus.Images(3)
                        Else
                            Status = 1
                            ShowStatus(lblStatus, "Asset Found.", 2)
                            imgInv.Image = MainForm.imgAssetStatus.Images(1)
                        End If
                    Else ' Item Does Not Belong to this Location
                        If ZulMessageBox.ShowMe("TransferAssetConfirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, True) = Windows.Forms.DialogResult.Yes Then
                            Status = 3 'Transfered
                            ShowStatus(lblStatus, "Asset Transfered.", 3)
                            imgInv.Image = MainForm.imgAssetStatus.Images(3)
                        Else
                            'If No, then cancel the operation.
                            Return False
                        End If
                    End If
                    attHistroy.AstID = AssetID
                    attHistroy.InvSchCode = InvSchID
                    attHistroy.Fr_loc = Fr_loc
                    attHistroy.To_Loc = LocationID
                    attHistroy.NoPiece = NoPiece
                    attHistroy.Status = Status
                    attHistroy.HisDate = Now.Date
                    If objAssetHistory.Update_Ast_History(attHistroy) Then
                        Dim attAsset As New attAssetDetails
                        attAsset.PKeyCode = AssetID
                        attAsset.LocID = LocationID
                        attAsset.InvSchCode = InvSchID
                        attAsset.InvStatus = Status
                        attAsset.LastInventoryDate = Now.Date
                        objAssetDetails.Update_InvStatus(attAsset)
                    End If
                    Return True
                Case -1
                    ShowStatus(lblStatus, "Asset Not Found !.", 1)
                    imgInv.Image = MainForm.imgAssetStatus.Images(0)
                    Return False
            End Select
        Else
            ShowStatus(lblStatus, "Asset Not Found !.", 1)
            imgInv.Image = MainForm.imgAssetStatus.Images(0)
            Return False
        End If
    End Function


End Class