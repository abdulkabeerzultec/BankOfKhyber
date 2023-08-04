Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class frmOrgGroups

    Dim objattCompGroups As attCompGroups
    Dim objBALCompGroups As New BALCompGroups
    Dim objattCompLvl As attCompLvl
    Dim objBALCompLvl As New BALCompLvl
    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    Dim isEdit As Boolean
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmOrgGroups_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmOrgGroups = Nothing
    End Sub

    Private Sub frmOrgGroups_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        valProvMain.SetValidationRule(txtGrpID, valRulenotEmpty)
        valProvMain.SetValidationRule(txtGrpDesc, valRulenotEmpty)
        valProvMain.SetValidationRule(cmbLevels.TextBox, valRulenotEmpty)

        txtGrpID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        txtGrpID.Properties.Mask.EditMask = "\d{1,8}"

        'If FormController.objfrmOrgGroups Is Nothing Then
        '    FormController.objfrmOrgGroups = Nothing
        'End If
        grd.DataSource = objBALCompGroups.GetAll_CompGroups(New attCompGroups)
        FormatGrid()


        cmbLevels.SelectedText = ""
        cmbLevels.SelectedValue = ""

        btnNew_Click(sender, e)
    End Sub

    Private Sub FormatGrid()
        grdView.Columns(0).Caption = "Group Code"
        grdView.Columns(0).Width = 80
        grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(1).Caption = "Description"
        grdView.Columns(1).Width = 130
        grdView.Columns(2).Caption = "Level Code"
        grdView.Columns(2).Width = 80
        grdView.Columns(2).Visible = False
        grdView.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(3).Caption = "Level Description"
        grdView.Columns(3).Width = 130

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

        addGridMenu(grd)
    End Sub

    Private Sub txtGrpID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGrpID.GotFocus
        txtGrpID.SelectAll()
    End Sub

    Private Sub txtGrpID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGrpID.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(39) Then e.Handled = True
    End Sub

    Private Sub txtGrpDesc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGrpDesc.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(39) Then e.Handled = True
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then btnSave_Click(New Object, New System.EventArgs)
    End Sub

    Private Function CheckExistValues(ByVal objatt As attCompGroups, ByVal IsInsertStatus As Boolean) As Boolean
        If objBALCompGroups.GrpIdExist(objatt, IsInsertStatus) Then
            errProv.SetError(txtGrpID, "Group Code already exists, change it and try again.")
            txtGrpID.SelectAll()
            txtGrpID.Focus()
            Return True
        ElseIf objBALCompGroups.GrpDescExist(objatt, IsInsertStatus) Then
            errProv.SetError(txtGrpDesc, "Group Description already exists, change it and try again.")
            txtGrpDesc.SelectAll()
            txtGrpDesc.Focus()
            Return True
        Else
            Return False
        End If
    End Function

    Private Function UpdateRecord() As Boolean
        objattCompGroups = New attCompGroups
        objattCompGroups.PKeyCode = txtGrpID.Text
        objattCompGroups.LvlID = cmbLevels.SelectedValue
        objattCompGroups.GrpDesc = RemoveUnnecessaryChars(txtGrpDesc.Text)

        If Not CheckExistValues(objattCompGroups, False) Then
            If objBALCompGroups.Update_CompGroups(objattCompGroups) Then
                ZulMessageBox.ShowMe("Saved")
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Private Function AddNewRecord() As Boolean
        objattCompGroups = New attCompGroups
        objattCompGroups.PKeyCode = txtGrpID.Text
        objattCompGroups.LvlID = cmbLevels.SelectedValue
        objattCompGroups.GrpDesc = RemoveUnnecessaryChars(txtGrpDesc.Text)
        If Not CheckExistValues(objattCompGroups, True) Then
            If objBALCompGroups.Insert_CompGroups(objattCompGroups) Then
                ZulMessageBox.ShowMe("Saved")
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate Then
                Dim FocRow As Integer
                If isEdit Then
                    If UpdateRecord() Then
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "GrpID", txtGrpID.Text)
                        SetGridRowCellValue(grdView, FocRow, "GrpDesc", txtGrpDesc.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "LvlID", cmbLevels.SelectedValue)
                        SetGridRowCellValue(grdView, FocRow, "LvlDesc", cmbLevels.SelectedText)
                        btnNew_Click(sender, e)
                    End If
                Else
                    If AddNewRecord() Then
                        grdView.AddNewRow()
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "GrpID", txtGrpID.Text)
                        SetGridRowCellValue(grdView, FocRow, "GrpDesc", txtGrpDesc.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "LvlID", cmbLevels.SelectedValue)
                        SetGridRowCellValue(grdView, FocRow, "LvlDesc", cmbLevels.SelectedText)
                        btnNew_Click(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Try
            Dim FocRow As Integer = grdView.FocusedRowHandle
            If FocRow >= 0 Then
                If Not check_Child_Group(GetGridRowCellValue(grdView, FocRow, "GrpID").ToString(), 0) Then
                    If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        objattCompGroups = New attCompGroups
                        objattCompGroups.PKeyCode = GetGridRowCellValue(grdView, FocRow, "GrpID").ToString()
                        If objBALCompGroups.Delete_CompGroups(objattCompGroups) Then
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



    Private Sub grdView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FocRow As Integer = grdView.FocusedRowHandle
        If FocRow >= 0 Then
            txtGrpID.Text = GetGridRowCellValue(grdView, FocRow, "GrpID").ToString
            txtGrpDesc.Text = GetGridRowCellValue(grdView, FocRow, "GrpDesc").ToString
            cmbLevels.SelectedValue = GetGridRowCellValue(grdView, FocRow, "LvlID").ToString
            cmbLevels.SelectedText = GetGridRowCellValue(grdView, FocRow, "LvlDesc").ToString
            isEdit = True
            btnDel.Visible = True
            valProvMain.Validate()
            errProv.ClearErrors()
        Else
            btnDel.Visible = False
        End If
    End Sub

    Private Sub grdView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdView.KeyDown
        If e.KeyValue = Keys.Delete And btnDel.Visible Then 'Press Delete
            btnDel_Click(sender, e)
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        txtGrpID.Text = ""
        txtGrpDesc.Text = ""
        txtGrpID.Focus()
        'cmbLevels.SelectedText = ""
        'cmbLevels.SelectedValue = ""

        isEdit = False
        btnDel.Visible = False
        grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        grdView.FocusedColumn = grdView.Columns(0)

        valProvMain.RemoveControlError(txtGrpDesc)
        valProvMain.RemoveControlError(txtGrpID)
        valProvMain.RemoveControlError(cmbLevels.TextBox)

        errProv.ClearErrors()
    End Sub

    Private Sub cmbLevels_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLevels.LovBtnClick
        Try
            cmbLevels.DisplayMember = "LvlDesc"
            cmbLevels.ValueMember = "LvlID"
            Dim objBALCompLvl As New BALCompLvl
            cmbLevels.DataSource = objBALCompLvl.GetAll_CompLvl(New attCompLvl)
            cmbLevels.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
End Class