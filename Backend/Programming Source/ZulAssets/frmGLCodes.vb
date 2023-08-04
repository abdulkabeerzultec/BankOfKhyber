Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Drawing

Public Class frmGLCodes

    Dim objGLCode As attGLCode
    Dim objBALGLCode As New BALGLCode

    Dim isEdit As Boolean = False
    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Dim frm As frmMain

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate() Then
                Dim FocRow As Integer
                If isEdit Then
                    If UpdateRecord() Then
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "GLcode", txtCode.Text)
                        SetGridRowCellValue(grdView, FocRow, "CompanyName", cmbComp.SelectedText)
                        SetGridRowCellValue(grdView, FocRow, "CompanyID", cmbComp.SelectedValue)
                        SetGridRowCellValue(grdView, FocRow, "GLDesc", txtDesc.Text.Trim)
                        btnNew_Click(sender, e)
                    End If
                Else
                    If AddNewRecord() Then
                        grdView.AddNewRow()
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "GLcode", txtCode.Text)
                        SetGridRowCellValue(grdView, FocRow, "CompanyName", cmbComp.SelectedText)
                        SetGridRowCellValue(grdView, FocRow, "CompanyID", cmbComp.SelectedValue)
                        SetGridRowCellValue(grdView, FocRow, "GLDesc", txtDesc.Text.Trim)
                        btnNew_Click(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub frmGLCode_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmGLCode = Nothing
    End Sub

    Private Sub frmGLCodes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch


        Try
            Me.WindowState = FormWindowState.Normal

            valProvMain.SetValidationRule(txtCode, valRulenotEmpty)
            valProvMain.SetValidationRule(cmbComp.TextBox, valRulenotEmpty)
            valProvMain.SetValidationRule(txtDesc, valRulenotEmpty)

            btnNewCompany.Enabled = Check_Auth("frmCompany")

            txtCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
            txtCode.Properties.Mask.EditMask = "\d{1,8}"

            grd.DataSource = objBALGLCode.GetAll_GLCodes(New attGLCode)
            format_Grid()
            btnNew_Click(sender, e)

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmGLCode = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub format_Grid()
        grdView.Columns(0).Caption = "GL Code"
        grdView.Columns(0).Width = 100
        grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdView.Columns(1).Caption = "Company Name"
        grdView.Columns(1).Width = 150

        grdView.Columns(2).Caption = "Description"
        grdView.Columns(2).Width = 255

        grdView.Columns(3).Visible = False

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

        addGridMenu(grd)
    End Sub

    Private Function AddNewRecord() As Boolean
        Try
            objGLCode = New attGLCode
            objGLCode.GLDesc = RemoveUnnecessaryChars(txtDesc.Text)
            objGLCode.PKeyCode = txtCode.Text
            objGLCode.CompanyId = cmbComp.SelectedValue

            If objBALGLCode.GLCodePKExist(objGLCode, True) Then
                errProv.SetError(txtCode, "GL Code already exists, Change it and try again.")
                txtCode.SelectAll()
                txtCode.Focus()
                Return False
            End If

            If objBALGLCode.Insert_GLCode(objGLCode) Then
                ZulMessageBox.ShowMe("Saved")
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function

    Private Function UpdateRecord() As Boolean
        Try
            objGLCode = New attGLCode
            objGLCode.GLDesc = RemoveUnnecessaryChars(txtDesc.Text)
            objGLCode.PKeyCode = txtCode.Text
            objGLCode.CompanyId = cmbComp.SelectedValue
            If objBALGLCode.Update_GLCode(objGLCode) Then
                ZulMessageBox.ShowMe("Saved")
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If grdView.FocusedRowHandle >= 0 Then
            Dim intRow As Integer = grdView.GetSelectedRows(0)
            Dim GLCodeID As Long = GetGridRowCellValue(grdView, intRow, "GLcode").ToString
            Try
                If Not check_Child_AssetDetail(GLCodeID, 12) Then
                    If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        objGLCode = New attGLCode
                        objGLCode.PKeyCode = GLCodeID
                        If objBALGLCode.Delete_GLCode(objGLCode) Then
                            grdView.DeleteSelectedRows()
                            ZulMessageBox.ShowMe("Deleted")
                            btnNew_Click(sender, e)
                        End If
                    End If
                Else
                    ZulMessageBox.ShowMe("CantDelete")
                End If

            Catch ex As Exception
                GenericExceptionHandler(ex, WhoCalledMe)
            End Try
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        isEdit = False
        btnDelete.Visible = False
        txtDesc.Text = ""
        txtCode.Text = ""
        cmbComp.SelectedText = ""
        cmbComp.SelectedValue = ""
        txtCode.Properties.ReadOnly = False
        txtCode.Focus()

        grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        grdView.FocusedColumn = grdView.Columns(0)


        valProvMain.RemoveControlError(txtCode)
        valProvMain.RemoveControlError(cmbComp.TextBox)
        errProv.ClearErrors()
    End Sub


    Private Sub grdGLCodeView_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdView.KeyDown
        If e.KeyValue = Keys.Delete Then
            btnDelete_Click(sender, e)
        End If
    End Sub

    Private Sub grdGLCodeView_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FoucRow As Integer = grdView.FocusedRowHandle
        If FoucRow >= 0 Then
            txtCode.Text = GetGridRowCellValue(grdView, FoucRow, "GLcode").ToString
            cmbComp.SelectedText = GetGridRowCellValue(grdView, FoucRow, "CompanyName").ToString
            cmbComp.SelectedValue = GetGridRowCellValue(grdView, FoucRow, "CompanyID").ToString
            txtDesc.Text = GetGridRowCellValue(grdView, FoucRow, "GLDesc").ToString
            isEdit = True
            txtCode.Properties.ReadOnly = True
            btnDelete.Visible = True
            valProvMain.Validate()
            errProv.ClearErrors()
        Else
            btnDelete.Visible = False
        End If
    End Sub

    Private Sub btnNewCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewCompany.Click
        Dim frm As New frmCompanies
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
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

End Class