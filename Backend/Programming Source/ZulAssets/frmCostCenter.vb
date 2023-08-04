Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Drawing
Public Class frmCostCenter
    Dim objattCostCenter As attCostCenter
    Dim objBALCostCenter As New BALCostCenter
    Dim isEdit As Boolean = False

    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    Private CostCenterID As String

    Private Sub frmCostCenter_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmCostCenter = Nothing
    End Sub


    Private Sub frmCostCenter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.ActiveControl = txtNumber
        Try
            valProvMain.SetValidationRule(txtNumber, valRulenotEmpty)
            valProvMain.SetValidationRule(txtDesc, valRulenotEmpty)
            valProvMain.SetValidationRule(cmbComp.TextBox, valRulenotEmpty)
            grd.DataSource = objBALCostCenter.GetAll_CostCenter(New attCostCenter)
            btnNewCompany.Enabled = Check_Auth("frmCompany")
            format_Grid()
            btnNew_Click(sender, e)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmCostCenter = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
#Region " -- Method -- "
    Private Sub format_Grid()
        GetGridColumn(grdView, "CostID").Visible = False
        GetGridColumn(grdView, "CostNumber").Caption = "Cost Number"
        GetGridColumn(grdView, "CostNumber").Width = 100
        GetGridColumn(grdView, "CostNumber").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        GetGridColumn(grdView, "CostName").Caption = "Cost Center Description"
        GetGridColumn(grdView, "CostName").Width = 200
        GetGridColumn(grdView, "CompanyName").Width = 200
        GetGridColumn(grdView, "CompanyID").Visible = False
        GetGridColumn(grdView, "Isdeleted").Visible = False

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

        addGridMenu(grd)
    End Sub

    Private Function CheckExistValues(ByVal objatt As attCostCenter, ByVal IsInsertStatus As Boolean) As Boolean
        If objBALCostCenter.CostNameExist(objatt, IsInsertStatus) Then
            errProv.SetError(txtDesc, "Cost Description already exists, change it and try again.")
            txtDesc.SelectAll()
            txtDesc.Focus()
            Return True
        Else
            Return False
        End If
    End Function

    Private Function AddNewRecord() As Boolean
        Try
            objattCostCenter = New attCostCenter
            objattCostCenter.PKeyCode = CostCenterID
            objattCostCenter.CostNumber = RemoveUnnecessaryChars(txtNumber.Text)
            objattCostCenter.CostName = RemoveUnnecessaryChars(txtDesc.Text)
            objattCostCenter.CompanyID = cmbComp.SelectedValue
            If Not CheckExistValues(objattCostCenter, True) Then
                If objBALCostCenter.Insert_CostCenter(objattCostCenter) Then
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
    Private Function UpdateRecord() As Boolean
        Try
            objattCostCenter = New attCostCenter
            objattCostCenter.PKeyCode = CostCenterID
            objattCostCenter.CostNumber = RemoveUnnecessaryChars(txtNumber.Text)
            objattCostCenter.CostName = RemoveUnnecessaryChars(txtDesc.Text)
            objattCostCenter.CompanyID = cmbComp.SelectedValue
            If Not CheckExistValues(objattCostCenter, False) Then
                If objBALCostCenter.Update_CostCenter(objattCostCenter) Then
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

#End Region


    Private Sub grdView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdView.KeyDown
        If e.KeyValue = Keys.Delete And btnDelete.Visible Then 'Press Delete
            btnDelete_Click(sender, e)
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim FocRow As Integer = grdView.FocusedRowHandle
            If FocRow >= 0 Then
                If Not check_Child_CostCenter(GetGridRowCellValue(grdView, FocRow, "CostID").ToString(), 1) Then
                    If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        objattCostCenter = New attCostCenter
                        objattCostCenter.PKeyCode = GetGridRowCellValue(grdView, FocRow, "CostID").ToString()
                        If objBALCostCenter.Delete_CostCenter(objattCostCenter) Then
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

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        isEdit = False
        btnDelete.Visible = False
        txtDesc.Text = ""
        txtNumber.Text = ""
        txtNumber.Focus()
        CostCenterID = objBALCostCenter.GetNextPKey_CostCenter
        cmbComp.SelectedText = ""
        cmbComp.SelectedValue = ""

        grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        grdView.FocusedColumn = grdView.Columns(0)

        valProvMain.RemoveControlError(txtNumber)
        valProvMain.RemoveControlError(txtDesc)

        errProv.ClearErrors()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate Then
                Dim FocRow As Integer
                If isEdit Then
                    If UpdateRecord() Then
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "CostID", CostCenterID)
                        SetGridRowCellValue(grdView, FocRow, "CostNumber", txtNumber.Text)
                        SetGridRowCellValue(grdView, FocRow, "CostName", txtDesc.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "CompanyName", cmbComp.SelectedText)
                        SetGridRowCellValue(grdView, FocRow, "CompanyID", cmbComp.SelectedValue)
                        btnNew_Click(sender, e)
                    End If
                Else
                    If AddNewRecord() Then
                        grdView.AddNewRow()
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "CostID", CostCenterID)
                        SetGridRowCellValue(grdView, FocRow, "CostNumber", txtNumber.Text)
                        SetGridRowCellValue(grdView, FocRow, "CostName", txtDesc.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "CompanyName", cmbComp.SelectedText)
                        SetGridRowCellValue(grdView, FocRow, "CompanyID", cmbComp.SelectedValue)
                        btnNew_Click(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub grdView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FocRow As Integer = grdView.FocusedRowHandle
        If FocRow >= 0 Then
            CostCenterID = GetGridRowCellValue(grdView, FocRow, "CostID").ToString
            txtNumber.Text = GetGridRowCellValue(grdView, FocRow, "CostNumber").ToString
            txtDesc.Text = GetGridRowCellValue(grdView, FocRow, "CostName").ToString
            cmbComp.SelectedText = GetGridRowCellValue(grdView, FocRow, "CompanyName").ToString
            cmbComp.SelectedValue = GetGridRowCellValue(grdView, FocRow, "CompanyID").ToString

            isEdit = True
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