Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Public Class frmDisp
    Dim objattDisposalMethod As attDisposalMethod
    Dim objBALDisposalMethod As New BALDisposalMethod
    Dim isEdit As Boolean = False
    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider


#Region "Method"
    Private Function CheckExistValues(ByVal objatt As attDisposalMethod, ByVal IsInsertStatus As Boolean) As Boolean
        If objBALDisposalMethod.DispDescExist(objatt, IsInsertStatus) Then
            errProv.SetError(txtDispDesc, "Disposal Method Name already exists, change it and try again.")
            txtDispDesc.SelectAll()
            txtDispDesc.Focus()
            Return True
        Else
            Return False
        End If
    End Function

    Private Function AddNewRecord() As Boolean
        Try
            objattDisposalMethod = New attDisposalMethod
            'assign the Primary key again to avoid having same PK on the network if there are two users adding at the same time.
            txtDipCode.Text = objBALDisposalMethod.GetNextPKey_DisposalMethod()
            objattDisposalMethod.PKeyCode = txtDipCode.Text
            objattDisposalMethod.DispDesc = RemoveUnnecessaryChars(txtDispDesc.Text)
            If Not CheckExistValues(objattDisposalMethod, True) Then
                If objBALDisposalMethod.Insert_DisposalMethod(objattDisposalMethod) Then
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
            objattDisposalMethod = New attDisposalMethod
            objattDisposalMethod.DispDesc = RemoveUnnecessaryChars(txtDispDesc.Text)
            objattDisposalMethod.PKeyCode = txtDipCode.Text
            If Not CheckExistValues(objattDisposalMethod, False) Then
                If objBALDisposalMethod.Update_DisposalMethod(objattDisposalMethod) Then
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
        grdView.Columns(0).Caption = "Disposal Method Code"
        grdView.Columns(0).Width = 150
        grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdView.Columns(1).Caption = "Disposal Method Name"
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


    Private Sub frmDisp_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmDisp = Nothing
    End Sub

    Private Sub frmDisp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = My.Resources.MainIcon
            Me.BackgroundImage = My.Resources.Background
            Me.BackgroundImageLayout = ImageLayout.Stretch

            Me.ActiveControl = txtDispDesc
            valProvMain.SetValidationRule(txtDispDesc, valRulenotEmpty)
            grd.DataSource = objBALDisposalMethod.GetAll_DisposalMethod(New attDisposalMethod)

            format_Grid()
            btnNew_Click(sender, e)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmDisp = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate Then
                Dim FocRow As Integer
                If isEdit Then 'DispCode,DispDesc
                    If UpdateRecord() Then
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "DispCode", txtDipCode.Text)
                        SetGridRowCellValue(grdView, FocRow, "DispDesc", txtDispDesc.Text)
                        btnNew_Click(sender, e)
                    End If
                Else
                    If AddNewRecord() Then
                        grdView.AddNewRow()
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "DispCode", txtDipCode.Text)
                        SetGridRowCellValue(grdView, FocRow, "DispDesc", txtDispDesc.Text)
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
                If Not check_Child_AssetDetail(GetGridRowCellValue(grdView, FocRow, "DispCode"), 4) Then
                    If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        objattDisposalMethod = New attDisposalMethod
                        objattDisposalMethod.PKeyCode = GetGridRowCellValue(grdView, FocRow, "DispCode")
                        If objBALDisposalMethod.Delete_DisposalMethod(objattDisposalMethod) Then
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
        Try
            isEdit = False
            btnSave.Enabled = True
            btnDelete.Visible = False
            txtDispDesc.Properties.ReadOnly = False
            txtDispDesc.Text = ""
            txtDipCode.Text = objBALDisposalMethod.GetNextPKey_DisposalMethod()
            txtDispDesc.Focus()

            grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
            grdView.FocusedColumn = grdView.Columns(0)

            valProvMain.RemoveControlError(txtDispDesc)

            errProv.ClearErrors()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub grdView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FocRow As Integer = grdView.FocusedRowHandle
        If FocRow >= 0 Then
            Dim DispCode As Integer = GetGridRowCellValue(grdView, FocRow, "DispCode")
            txtDipCode.Text = DispCode
            txtDispDesc.Text = GetGridRowCellValue(grdView, FocRow, "DispDesc")

            valProvMain.Validate()
            errProv.ClearErrors()
            If DispCode = 1 Or DispCode = 2 Or DispCode = 3 Then
                txtDispDesc.Properties.ReadOnly = True
                isEdit = False
                btnDelete.Visible = False
                btnSave.Enabled = False
            Else
                isEdit = True
                btnDelete.Visible = True
                txtDispDesc.Properties.ReadOnly = False
                btnSave.Enabled = True
            End If
        Else
            btnDelete.Visible = False
        End If
    End Sub


    Private Sub grdView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdView.KeyDown
        If e.KeyValue = Keys.Delete And btnDelete.Visible = True Then 'Press Delete
            btnDelete_Click(sender, e)
        End If
    End Sub

  

End Class
