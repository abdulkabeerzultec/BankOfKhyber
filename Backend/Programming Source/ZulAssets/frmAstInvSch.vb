Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports Microsoft.VisualBasic
Public Class frmAstInvSch
    Dim objattAst_INV_Schedule As attInvSchedule
    Dim objBALAst_INV_Schedule As New BALAst_INV_Schedule
    Dim isEdit As Boolean = False

    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    Private Function CheckDate() As Boolean
        'ENDDATE SHOULD BE BIGGER OR EQUAL THAN START DATE.
        If Date.Compare(dtStart.Value, dtEnd.Value) > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate Then
                If CheckDate() Then
                    If isEdit Then
                        If UpdateRecord() Then
                            grd.DataSource = objBALAst_INV_Schedule.GetAll_invSch(New attInvSchedule)
                            btnNew_Click(sender, e)
                        End If
                    Else
                        If AddNewRecord() Then
                            grd.DataSource = objBALAst_INV_Schedule.GetAll_invSch(New attInvSchedule)
                            btnNew_Click(sender, e)
                        End If
                    End If
                Else
                    ZulMessageBox.ShowMe("StartEndDate")
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmAstInvSch = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


#Region "Method"

    Private Function AddNewRecord() As Boolean
        Try
            objattAst_INV_Schedule = New attInvSchedule
            objattAst_INV_Schedule.InvDesc = RemoveUnnecessaryChars(txtINVDesc.Text)
            objattAst_INV_Schedule.InvStartDate = dtStart.Text
            objattAst_INV_Schedule.InvEndDate = dtEnd.Text
            objattAst_INV_Schedule.InventoryType = cmbInvType.SelectedIndex
            txtInID.Text = objBALAst_INV_Schedule.GetNextPKey_invSch()
            objattAst_INV_Schedule.PKeyCode = txtInID.Text

            If objBALAst_INV_Schedule.Insert_invSch(objattAst_INV_Schedule) Then
                'insert all the data into ast history table for this schedule
                Dim objBALAst_History As New BALAst_History
                Dim objBalAssetDet As New BALAssetDetails
                Dim dt As DataTable = objBalAssetDet.GetAssetIDsLocations()
                pb.Visible = True
                pb.Maximum = dt.Rows.Count
                pb.Value = 0
                pb.Step = 1
                For Each dr As DataRow In dt.Rows
                    Dim objattAstHistory As New attAstHistory()
                    objattAstHistory.AstID = dr("AstID").ToString()
                    'InvStatus
                    If dr("InvStatus").ToString = "6" Then
                        objattAstHistory.Status = 6
                    Else
                        objattAstHistory.Status = 0
                    End If
                    objattAstHistory.Fr_loc = dr("LocID").ToString()
                    objattAstHistory.To_Loc = dr("LocID").ToString()
                    objattAstHistory.InvSchCode = objattAst_INV_Schedule.PKeyCode
                    objattAstHistory.HisDate = Nothing
                    objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                    objBALAst_History.Insert_Ast_History(objattAstHistory)
                    pb.PerformStep()
                    Application.DoEvents()
                Next
                ZulMessageBox.ShowMe("Saved")
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally
            pb.Visible = False
        End Try
    End Function

    Private Sub format_Grid()
        grdView.Columns(0).Caption = " Code"
        grdView.Columns(0).Width = 50
        grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(1).Caption = "Description"
        grdView.Columns(1).Width = 200
        grdView.Columns(2).Caption = "Start Date"
        grdView.Columns(2).Width = 100
        grdView.Columns(3).Caption = "End Date"
        grdView.Columns(3).Width = 100
        grdView.Columns(4).Visible = False
        grdView.Columns(5).Visible = False 'SCHTYPE

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        addGridMenu(grd)

    End Sub



    Private Function CheckID(ByVal _Id As String) As Boolean
        Try
            Dim ds As New DataTable

            objattAst_INV_Schedule = New attInvSchedule
            objattAst_INV_Schedule.PKeyCode = _Id
            ds = objBALAst_INV_Schedule.CheckID(objattAst_INV_Schedule)
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

    Private Function UpdateRecord() As Boolean
        Try
            objattAst_INV_Schedule = New attInvSchedule
            objattAst_INV_Schedule.InvDesc = RemoveUnnecessaryChars(txtINVDesc.Text)
            objattAst_INV_Schedule.InvStartDate = dtStart.Text
            objattAst_INV_Schedule.InvEndDate = dtEnd.Text
            objattAst_INV_Schedule.PKeyCode = txtInID.Text
            objattAst_INV_Schedule.InventoryType = cmbInvType.SelectedIndex

            If objBALAst_INV_Schedule.Update_invSch(objattAst_INV_Schedule) Then
                ZulMessageBox.ShowMe("Saved")
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function
#End Region

    Private Sub frmAstInvSch_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmAstInvSch = Nothing
    End Sub

    Private Sub frmAstInvSch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        Try
            Me.WindowState = FormWindowState.Normal


            valProvMain.SetValidationRule(txtInID, valRulenotEmpty)
            valProvMain.SetValidationRule(txtINVDesc, valRulenotEmpty)

            grd.DataSource = objBALAst_INV_Schedule.GetAll_invSch(New attInvSchedule)
            format_Grid()

            dtEnd.CustomFormat = AppConfig.MaindateFormat
            dtStart.CustomFormat = AppConfig.MaindateFormat


            btnNew_Click(sender, e)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub



    Private Sub txtInID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim KeyAscii As Integer
            KeyAscii = Asc(e.KeyChar)
            Select Case KeyAscii
                Case 48 To 57, 8, 13
                    e.Handled = False
                Case Else
                    KeyAscii = 0
                    e.Handled = True
            End Select
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            txtInID.Text = objBALAst_INV_Schedule.GetNextPKey_invSch()
            txtINVDesc.Text = ""
            isEdit = False
            btnDelete.Visible = False
            btnSave.Enabled = True
            dtEnd.Enabled = True
            dtStart.Enabled = True
            cmbInvType.Enabled = True
            cmbInvType.SelectedIndex = 0
            txtINVDesc.Properties.ReadOnly = False
            txtINVDesc.Focus()

            valProvMain.RemoveControlError(txtInID)
            valProvMain.RemoveControlError(txtINVDesc)

            errProv.ClearErrors()

            grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
            grdView.FocusedColumn = grdView.Columns(0)
            dtEnd.Value = Now.Date
            dtStart.Value = Now.Date


        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim FocRow As Integer = grdView.FocusedRowHandle
            If FocRow >= 0 Then
                objattAst_INV_Schedule = New attInvSchedule
                objattAst_INV_Schedule.PKeyCode = GetGridRowCellValue(grdView, FocRow, "InvSchCode").ToString()
                Dim objattAst_history As New attAstHistory
                objattAst_history.InvSchCode = objattAst_INV_Schedule.PKeyCode

                If Not check_Child_AstHistory(objattAst_history, True) Then
                    If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        If objBALAst_INV_Schedule.Delete_invSch(objattAst_INV_Schedule) Then
                            Dim objBALAst_History As New BALAst_History
                            objBALAst_History.delete_Ast_HistoryByInvSch(objattAst_history)
                            grdView.DeleteSelectedRows()
                            ZulMessageBox.ShowMe("Deleted")
                            btnNew_Click(sender, e)
                        End If
                    End If
                Else
                    ZulMessageBox.ShowMe("CantDelete")
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub grdView_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdView.KeyDown
        If e.KeyValue = Keys.Delete And btnDelete.Visible = True Then 'Press Delete
            btnDelete_Click(sender, e)
        End If
    End Sub

    Private Sub grdView_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FocRow As Integer = grdView.FocusedRowHandle

        If FocRow >= 0 Then
            If GetGridRowCellValue(grdView, FocRow, "InvSchCode").ToString() = "1" Then
                txtInID.Text = GetGridRowCellValue(grdView, FocRow, "InvSchCode").ToString()
                txtINVDesc.Text = GetGridRowCellValue(grdView, FocRow, "InvDesc").ToString()
                If (GetGridRowCellValue(grdView, FocRow, "InvStartDate").ToString() <> "") Then dtStart.Value = CType(GetGridRowCellValue(grdView, FocRow, "InvStartDate"), DateTime).ToShortDateString()
                If (GetGridRowCellValue(grdView, FocRow, "InvEndDate").ToString() <> "") Then dtEnd.Value = CType(GetGridRowCellValue(grdView, FocRow, "InvEndDate"), DateTime).ToShortDateString()
                If GetGridRowCellValue(grdView, FocRow, "SchType").ToString <> "" Then
                    cmbInvType.SelectedIndex = GetGridRowCellValue(grdView, FocRow, "SchType").ToString
                End If
                isEdit = True
                txtINVDesc.Properties.ReadOnly = True
                dtEnd.Enabled = False
                dtStart.Enabled = False
                btnDelete.Visible = False
                btnSave.Enabled = False
                cmbInvType.Enabled = False
            Else
                txtInID.Text = GetGridRowCellValue(grdView, FocRow, "InvSchCode").ToString()
                txtINVDesc.Text = GetGridRowCellValue(grdView, FocRow, "InvDesc").ToString()
                If (GetGridRowCellValue(grdView, FocRow, "InvStartDate").ToString() <> "") Then dtStart.Value = CType(GetGridRowCellValue(grdView, FocRow, "InvStartDate"), DateTime).ToShortDateString()
                If (GetGridRowCellValue(grdView, FocRow, "InvEndDate").ToString() <> "") Then dtEnd.Value = CType(GetGridRowCellValue(grdView, FocRow, "InvEndDate"), DateTime).ToShortDateString()
                If GetGridRowCellValue(grdView, FocRow, "SchType").ToString <> "" Then
                    cmbInvType.SelectedIndex = GetGridRowCellValue(grdView, FocRow, "SchType").ToString
                End If
                isEdit = True
                btnDelete.Visible = True
                btnSave.Enabled = True
                dtEnd.Enabled = True
                dtStart.Enabled = True
                cmbInvType.Enabled = True
                txtINVDesc.Properties.ReadOnly = False
            End If
            valProvMain.Validate()
            errProv.ClearErrors()
        Else
            btnDelete.Visible = False
        End If
    End Sub
End Class
