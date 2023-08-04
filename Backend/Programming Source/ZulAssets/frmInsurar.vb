Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Public Class frmInsurar
    Dim objattInsurer As attInsurer
    Dim objBALInsurer As New BALInsurer
    Dim isEdit As Boolean = False

    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    Private Sub frmInsurar_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmInsurar = Nothing
    End Sub

    Private Sub frmInsurar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.ActiveControl = txtInsDesc

        Try
            valProvMain.SetValidationRule(txtInsDesc, valRulenotEmpty)
            grd.DataSource = objBALInsurer.GetAll_Insurer(New attInsurer)
            format_Grid()
            btnNew_Click(sender, e)

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmInsurar = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub
#Region "Method"

    Private Function CheckExistValues(ByVal objatt As attInsurer, ByVal IsInsertStatus As Boolean) As Boolean
        If objBALInsurer.InsNameExist(objatt, IsInsertStatus) Then
            errProv.SetError(txtInsDesc, "Insurar Name already exists, change it and try again.")
            txtInsDesc.SelectAll()
            txtInsDesc.Focus()
            Return True
        Else
            Return False
        End If
    End Function

    Private Function AddNewRecord() As Boolean
        Try
            objattInsurer = New attInsurer
            'assign the Primary key again to avoid having same PK on the network if there are two users adding at the same time.
            txtInsCode.Text = objBALInsurer.GetNextPKey_Insurer
            objattInsurer.InsName = RemoveUnnecessaryChars(txtInsDesc.Text)
            objattInsurer.PKeyCode = txtInsCode.Text
            If Not CheckExistValues(objattInsurer, True) Then
                If txtInsCode.Text = objBALInsurer.Insert_Insurer(objattInsurer) Then
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
            objattInsurer = New attInsurer
            objattInsurer.InsName = RemoveUnnecessaryChars(txtInsDesc.Text)
            objattInsurer.PKeyCode = txtInsCode.Text
            If Not CheckExistValues(objattInsurer, False) Then
                If txtInsCode.Text = objBALInsurer.Update_Insurer(objattInsurer) Then
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
        grdView.Columns(0).Caption = "Insurar Code"
        grdView.Columns(0).Width = 115
        grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdView.Columns(1).Caption = "Insurar Name"
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
                If isEdit Then 'InsCode,InsName
                    If UpdateRecord() Then
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "InsCode", txtInsCode.Text)
                        SetGridRowCellValue(grdView, FocRow, "InsName", txtInsDesc.Text.Trim)
                        btnNew_Click(sender, e)
                    End If
                Else
                    If AddNewRecord() Then
                        grdView.AddNewRow()
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "InsCode", txtInsCode.Text)
                        SetGridRowCellValue(grdView, FocRow, "InsName", txtInsDesc.Text.Trim)
                        btnNew_Click(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim FocRow As Integer = grdView.FocusedRowHandle
            If FocRow >= 0 Then
                If Not check_Child_AssetDetail(GetGridRowCellValue(grdView, FocRow, "InsCode").ToString(), 3) Then
                    If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        objattInsurer = New attInsurer
                        objattInsurer.PKeyCode = GetGridRowCellValue(grdView, FocRow, "InsCode")
                        If objBALInsurer.Delete_Insurer(objattInsurer) Then
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
        txtInsCode.Text = objBALInsurer.GetNextPKey_Insurer()
        txtInsDesc.Text = ""
        btnDelete.Visible = False
        isEdit = False
        txtInsDesc.Focus()
        grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        grdView.FocusedColumn = grdView.Columns(0)

        valProvMain.RemoveControlError(txtInsDesc)

        errProv.ClearErrors()
    End Sub


    Private Sub grdView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FocRow As Integer = grdView.FocusedRowHandle
        If FocRow >= 0 Then
            txtInsCode.Text = GetGridRowCellValue(grdView, FocRow, "InsCode").ToString
            txtInsDesc.Text = GetGridRowCellValue(grdView, FocRow, "InsName").ToString
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
End Class
