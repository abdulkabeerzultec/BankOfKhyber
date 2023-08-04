Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class frmAddress
    Dim objattAddress As attAddress
    Dim objBALAddress As New BALAddress
    Dim isEdit As Boolean = False

    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    Public addNew_fromCust As Integer = 0
    Public frmCust As frmCustodian

    Private Sub frmAddress_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objAddress = Nothing
    End Sub

    Private Sub frmAddress_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = My.Resources.MainIcon
            Me.BackgroundImage = My.Resources.Background
            Me.BackgroundImageLayout = ImageLayout.Stretch
            Me.ActiveControl = txtAddDesc

            valProvMain.SetValidationRule(txtAddCode, valRulenotEmpty)
            valProvMain.SetValidationRule(txtAddDesc, valRulenotEmpty)

            grd.DataSource = objBALAddress.GetAll_Address(New attAddress)
            format_Grid()

            btnNew_Click(sender, e)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


#Region " -- Method -- "

    Private Sub format_Grid()
        grdView.Columns(0).Caption = "Address Code"
        grdView.Columns(0).Width = 115
        grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdView.Columns(1).Caption = "Address Description"
        grdView.Columns(1).Width = 255

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

        addGridMenu(grd)
    End Sub



    Private Function CheckExistValues(ByVal objattAddress As attAddress, ByVal IsInsertStatus As Boolean) As Boolean
        If objBALAddress.AddressExist(objattAddress, IsInsertStatus) Then
            errProv.SetError(txtAddDesc, "Address already exists, change it and try again.")
            txtAddDesc.SelectAll()
            txtAddDesc.Focus()
            Return True
        Else
            Return False
        End If
    End Function

    Private Function AddNewRecord() As Boolean
        Try
            objattAddress = New attAddress
            txtAddCode.Text = objBALAddress.GetNextPKey_Address()
            objattAddress.PKeyCode = txtAddCode.Text
            objattAddress.AddressDesc = txtAddDesc.Text.Trim

            If Not CheckExistValues(objattAddress, True) Then
                If objBALAddress.Insert_Address(objattAddress) Then
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
            objattAddress = New attAddress
            objattAddress.PKeyCode = txtAddCode.Text
            objattAddress.AddressDesc = txtAddDesc.Text.Trim
            If Not CheckExistValues(objattAddress, False) Then
                If objBALAddress.Update_Address(objattAddress) Then
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

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate() Then
                Dim FocRow As Integer
                If isEdit Then
                    If UpdateRecord() Then
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "AddressID", txtAddCode.Text)
                        SetGridRowCellValue(grdView, FocRow, "AddressDesc", txtAddDesc.Text.Trim)
                        btnNew_Click(sender, e)
                    End If
                Else
                    If AddNewRecord() Then
                        grdView.AddNewRow()
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "AddressID", txtAddCode.Text)
                        SetGridRowCellValue(grdView, FocRow, "AddressDesc", txtAddDesc.Text.Trim)
                        btnNew_Click(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If grdView.FocusedRowHandle >= 0 Then
            Dim intRow As Integer = grdView.GetSelectedRows(0)
            Dim AddressID As Long = GetGridRowCellValue(grdView, intRow, "AddressID").ToString
            Try
                If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    objattAddress = New attAddress
                    objattAddress.PKeyCode = AddressID
                    If objBALAddress.Delete_Address(objattAddress) Then
                        grdView.DeleteSelectedRows()
                        ZulMessageBox.ShowMe("Deleted")
                        btnNew_Click(sender, e)
                    End If
                End If
            Catch ex As Exception
                GenericExceptionHandler(ex, WhoCalledMe)
            End Try
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        FormController.objAddress = Nothing
        Me.Dispose()
    End Sub

    Private Sub btnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If addNew_fromCust = 1 Then
            frmCust.Address = txtAddDesc.Text
            frmCust.UpdateAddress.Invoke()
            frmCust.BringToFront()
            Me.Dispose()
        Else
            Me.Dispose()
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        isEdit = False
        btnDelete.Visible = False
        txtAddDesc.Text = ""
        txtAddCode.Text = objBALAddress.GetNextPKey_Address()
        txtAddDesc.Focus()

        grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        grdView.FocusedColumn = grdView.Columns(0)

        valProvMain.RemoveControlError(txtAddDesc)
        errProv.ClearErrors()
    End Sub

    Private Sub grdView_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FoucRow As Integer = grdView.FocusedRowHandle
        If FoucRow >= 0 Then

            txtAddCode.Text = GetGridRowCellValue(grdView, FoucRow, "AddressID").ToString
            txtAddDesc.Text = GetGridRowCellValue(grdView, FoucRow, "AddressDesc").ToString
            isEdit = True
            btnDelete.Visible = True
            valProvMain.Validate()
            errProv.ClearErrors()
        Else
            btnDelete.Visible = False
        End If

    End Sub

    Private Sub grdView_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdView.DoubleClick
        If grdView.FocusedRowHandle >= 0 Then
            If Not frmCust Is Nothing Then
                Dim dtCust As New DataTable
                frmCust.Address = txtAddDesc.Text
                frmCust.UpdateAddress.Invoke()
                frmCust.BringToFront()
                Me.Dispose()
            End If
        End If
    End Sub

    Private Sub grdView_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdView.KeyDown
        If e.KeyValue = Keys.Delete Then
            btnDelete_Click(sender, e)
        End If
    End Sub

End Class