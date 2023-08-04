Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL


Public Class frmLevels
    Dim objattCompLvl As attCompLvl
    Dim objBALCompLvl As New BALCompLvl

    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    Dim isEdit As Boolean

    Private Sub frmProductGroup_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objFrmLevels = Nothing
    End Sub

    Private Sub frmProductGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.ActiveControl = txtLvlDesc


        If FormController.objFrmLevels Is Nothing Then
            FormController.objFrmLevels = New frmLevels
        End If

        valProvMain.SetValidationRule(txtLvlID, valRulenotEmpty)
        valProvMain.SetValidationRule(txtLvlDesc, valRulenotEmpty)

        txtLvlID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        txtLvlID.Properties.Mask.EditMask = "\d{1,8}"

        grd.DataSource = objBALCompLvl.GetAll_CompLvl(New attCompLvl)
        FormatGrid()

        btnNew_Click(sender, e)
    End Sub

    Private Sub FormatGrid()
        grdView.Columns(0).Caption = "Level ID"
        grdView.Columns(0).Width = 100
        grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdView.Columns(1).Caption = "Level Description"
        grdView.Columns(1).Width = 200

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

        addGridMenu(grd)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub


    Private Function CheckExistValues(ByVal objatt As attCompLvl, ByVal IsInsertStatus As Boolean) As Boolean
        If objBALCompLvl.LvlDescExist(objatt, IsInsertStatus) Then
            errProv.SetError(txtLvlDesc, "Description already exists, change it and try again.")
            txtLvlDesc.SelectAll()
            txtLvlDesc.Focus()
            Return True
        Else
            Return False
        End If
    End Function
    Private Function UpdateRecord() As Boolean
        Try
            objattCompLvl = New attCompLvl
            objattCompLvl.PKeyCode = txtLvlID.Text
            objattCompLvl.LvlDesc = RemoveUnnecessaryChars(txtLvlDesc.Text)

            If Not CheckExistValues(objattCompLvl, False) Then
                If objBALCompLvl.Update_CompLvl(objattCompLvl) Then
                    ZulMessageBox.ShowMe("Saved")
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

    Private Function AddNewRecord() As Boolean
        Try
            objattCompLvl = New attCompLvl
            txtLvlID.Text = objBALCompLvl.GetNextPKey_CompLvl()

            objattCompLvl.PKeyCode = txtLvlID.Text
            objattCompLvl.LvlDesc = RemoveUnnecessaryChars(txtLvlDesc.Text)
            If Not CheckExistValues(objattCompLvl, True) Then
                If objBALCompLvl.Insert_CompLvl(objattCompLvl) Then
                    ZulMessageBox.ShowMe("Saved")
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


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate Then
                Dim FocRow As Integer
                If isEdit Then
                    If UpdateRecord() Then
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "LvlID", txtLvlID.Text)
                        SetGridRowCellValue(grdView, FocRow, "LvlDesc", txtLvlDesc.Text.Trim)
                        btnNew_Click(sender, e)
                    End If
                Else
                    If AddNewRecord() Then
                        grdView.AddNewRow()
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "LvlID", txtLvlID.Text)
                        SetGridRowCellValue(grdView, FocRow, "LvlDesc", txtLvlDesc.Text.Trim)
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
                If Not check_Child_Levels(GetGridRowCellValue(grdView, FocRow, "LvlID"), 0) Then
                    If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        objattCompLvl = New attCompLvl
                        objattCompLvl.PKeyCode = GetGridRowCellValue(grdView, FocRow, "LvlID").ToString()
                        If objBALCompLvl.Delete_CompLvl(objattCompLvl) Then
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
            txtLvlID.Text = GetGridRowCellValue(grdView, FocRow, "LvlID").ToString
            txtLvlDesc.Text = GetGridRowCellValue(grdView, FocRow, "LvlDesc").ToString
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
        isEdit = False
        txtLvlDesc.Text = ""
        txtLvlID.Text = objBALCompLvl.GetNextPKey_CompLvl()
        txtLvlDesc.Focus()
        btnDel.Visible = False
        grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        grdView.FocusedColumn = grdView.Columns(0)

        valProvMain.RemoveControlError(txtLvlDesc)
        valProvMain.RemoveControlError(txtLvlID)

        errProv.ClearErrors()
    End Sub

   
End Class