Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Drawing
Public Class frmUnits
    Dim objattUnits As attUnits
    Dim objBALUnits As New BALUnits
    Dim isEdit As Boolean = False

    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider


    Private Sub frmUnits_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmUnits = Nothing
    End Sub


    Private Sub frmUnits_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.ActiveControl = txtUnitName
        Try
            valProvMain.SetValidationRule(txtUnitID, valRulenotEmpty)
            valProvMain.SetValidationRule(txtUnitName, valRulenotEmpty)
            grd.DataSource = objBALUnits.GetAll_Units(New attUnits)
            format_Grid()
            btnNew_Click(sender, e)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmUnits = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
#Region " -- Method -- "
    Private Sub format_Grid()
        grdView.Columns(0).Caption = "Unit ID"
        grdView.Columns(0).Width = 115
        grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdView.Columns(1).Caption = "Unit Description"
        grdView.Columns(1).Width = 255

        addGridMenu(grd)
    End Sub

    Private Function CheckExistValues(ByVal objatt As attUnits, ByVal IsInsertStatus As Boolean) As Boolean
        If objBALUnits.UnitDescExist(objatt, IsInsertStatus) Then
            errProv.SetError(txtUnitName, "Unit Description already exists, change it and try again.")
            txtUnitName.SelectAll()
            txtUnitName.Focus()
            Return True
        Else
            Return False
        End If
    End Function

    Private Function AddNewRecord() As Boolean
        Try
            objattUnits = New attUnits
            objattUnits.UnitDesc = RemoveUnnecessaryChars(txtUnitName.Text)
            objattUnits.PKeyCode = txtUnitID.Text
            If Not CheckExistValues(objattUnits, True) Then
                If objBALUnits.Insert_Units(objattUnits) Then
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
            objattUnits = New attUnits
            objattUnits.UnitDesc = RemoveUnnecessaryChars(txtUnitName.Text)
            objattUnits.PKeyCode = txtUnitID.Text
            If Not CheckExistValues(objattUnits, False) Then
                If objBALUnits.Update_Units(objattUnits) Then
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
                If Not check_Child_Unit(GetGridRowCellValue(grdView, FocRow, "UnitID").ToString(), 1) Then
                    If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        objattUnits = New attUnits
                        objattUnits.PKeyCode = GetGridRowCellValue(grdView, FocRow, "UnitID").ToString()
                        If objBALUnits.Delete_Units(objattUnits) Then
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
        txtUnitName.Text = ""
        txtUnitID.Text = objBALUnits.GetNextPKey_Units
        txtUnitName.Focus()

        grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        grdView.FocusedColumn = grdView.Columns(0)

        valProvMain.RemoveControlError(txtUnitID)
        valProvMain.RemoveControlError(txtUnitName)

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
                        SetGridRowCellValue(grdView, FocRow, "UnitID", txtUnitID.Text)
                        SetGridRowCellValue(grdView, FocRow, "UnitDesc", txtUnitName.Text.Trim)
                        btnNew_Click(sender, e)
                    End If
                Else
                    If AddNewRecord() Then
                        grdView.AddNewRow()
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "UnitID", txtUnitID.Text)
                        SetGridRowCellValue(grdView, FocRow, "UnitDesc", txtUnitName.Text.Trim)
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
            txtUnitID.Text = GetGridRowCellValue(grdView, FocRow, "UnitID").ToString
            txtUnitName.Text = GetGridRowCellValue(grdView, FocRow, "UnitDesc").ToString
            isEdit = True
            btnDelete.Visible = True
            valProvMain.Validate()
            errProv.ClearErrors()
        Else
            btnDelete.Visible = False
        End If
    End Sub

End Class