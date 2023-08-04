Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Public Class frmDesignation
    Dim objattDesignation As attDesignation
    Dim objBALDesignation As New BALDesignation
    Dim isEdit As Boolean = False
    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    Private Sub frmDesig_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            FormController.objfrmDesig = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub frmDesig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.ActiveControl = txtDisgDesc

        Try
            valProvMain.SetValidationRule(txtDisgDesc, valRulenotEmpty)
            grd.DataSource = objBALDesignation.GetAll_Designation(New attDesignation)
            format_Grid()

            btnNew_Click(sender, e)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

#Region "Method"

    Private Function CheckExistValues(ByVal objatt As attDesignation, ByVal IsInsertStatus As Boolean) As Boolean
        If objBALDesignation.DesignationDescExist(objatt, IsInsertStatus) Then
            errProv.SetError(txtDisgDesc, "Designation Description already exists, change it and try again.")
            txtDisgDesc.SelectAll()
            txtDisgDesc.Focus()
            Return True
        Else
            Return False
        End If
    End Function


    Private Function AddNewRecord() As Boolean
        Try
            objattDesignation = New attDesignation
            'assign the Primary key again to avoid having same PK on the network if there are two users adding at the same time.
            txtDisgCode.Text = objBALDesignation.GetNextPKey_Designation()
            objattDesignation.PKeyCode = txtDisgCode.Text
            objattDesignation.Description = RemoveUnnecessaryChars(txtDisgDesc.Text)
            If Not CheckExistValues(objattDesignation, True) Then
                If objBALDesignation.Insert_Designation(objattDesignation) Then
                    ZulMessageBox.ShowMe("Saved")
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox("Designation Already Exists", MsgBoxStyle.Critical, Application.ProductName)
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Function
    Private Function UpdateRecord() As Boolean
        Try
            objattDesignation = New attDesignation
            objattDesignation.Description = RemoveUnnecessaryChars(txtDisgDesc.Text)
            objattDesignation.PKeyCode = txtDisgCode.Text
            If Not CheckExistValues(objattDesignation, False) Then
                If objBALDesignation.Update_Designation(objattDesignation) Then
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

    Private Sub format_Grid()
        grdView.Columns(0).Caption = "Designation Code"
        grdView.Columns(0).Width = 115
        grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdView.Columns(1).Caption = "Designation Name"
        grdView.Columns(1).Width = 255

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

        addGridMenu(grd)
    End Sub

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate Then
                Dim FocRow As Integer
                If isEdit Then
                    If UpdateRecord() Then
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "DesignationID", txtDisgCode.Text)
                        SetGridRowCellValue(grdView, FocRow, "Description", txtDisgDesc.Text.Trim)
                        btnNew_Click(sender, e)
                    End If
                Else
                    If AddNewRecord() Then
                        grdView.AddNewRow()
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "DesignationID", txtDisgCode.Text)
                        SetGridRowCellValue(grdView, FocRow, "Description", txtDisgDesc.Text.Trim)
                        btnNew_Click(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmDesig = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        txtDisgCode.Text = objBALDesignation.GetNextPKey_Designation()
        isEdit = False
        btnDelete.Visible = False
        txtDisgDesc.Text = ""
        txtDisgDesc.Focus()


        grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        grdView.FocusedColumn = grdView.Columns(0)

        valProvMain.RemoveControlError(txtDisgDesc)

        errProv.ClearErrors()
    End Sub

    Private Sub grdView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FocRow As Integer = grdView.FocusedRowHandle
        If FocRow >= 0 Then
            txtDisgCode.Text = GetGridRowCellValue(grdView, FocRow, "DesignationID")
            txtDisgDesc.Text = GetGridRowCellValue(grdView, FocRow, "Description")
            isEdit = True
            btnDelete.Visible = True
            valProvMain.Validate()
            errProv.ClearErrors()
        Else
            btnDelete.Visible = False
        End If
    End Sub

    Private Sub grdView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdView.KeyDown
        If e.KeyValue = Keys.Delete And btnDelete.Visible Then 'Press Delete
            btnDelete_Click(sender, e)
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim FocRow As Integer = grdView.FocusedRowHandle
            If FocRow >= 0 Then
                If Not check_Child_Custodian(GetGridRowCellValue(grdView, FocRow, "DesignationID").ToString(), 7) Then
                    If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then

                        objattDesignation = New attDesignation
                        objattDesignation.PKeyCode = GetGridRowCellValue(grdView, FocRow, "DesignationID").ToString()
                        If objBALDesignation.Delete_Designation(objattDesignation) Then
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
End Class
