Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class frmSupplier
    Dim objattSupplier As attSupplier
    Dim objBALSupplier As New BALSupplier
    Dim isEdit As Boolean = False



    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmSupplier = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

#Region "Method"

    Private Function AddNewRecord() As Boolean
        Try
            If CheckID(RemoveUnnecessaryChars(txtSuppID.Text)) Then
                objattSupplier = New attSupplier
                objattSupplier.SuppCell = RemoveUnnecessaryChars(txtSuppCell.Text)
                objattSupplier.SuppEmail = RemoveUnnecessaryChars(txtSuppEmail.Text)
                objattSupplier.SuppFax = RemoveUnnecessaryChars(txtSuppFax.Text)
                objattSupplier.SuppName = RemoveUnnecessaryChars(txtSuppName.Text)
                objattSupplier.SuppPhone = RemoveUnnecessaryChars(txtSuppPhone.Text)
                objattSupplier.PKeyCode = RemoveUnnecessaryChars(txtSuppID.Text)
                If objBALSupplier.Insert_Supplier(objattSupplier) Then
                    ZulMessageBox.ShowMe("Saved")
                    Return True
                Else
                    Return False
                End If
            Else
                errProv.SetError(txtSuppID, "Supplier ID already Exists , Please try Again")
                Return False
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Function
    Private Sub format_Grid()
        grdView.Columns(0).Caption = "Supplier ID"
        grdView.Columns(0).Width = 100
        grdView.Columns(1).Caption = "Supplier Name"
        grdView.Columns(1).Width = 160
        grdView.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdView.Columns(2).Caption = "Supplier Cell"
        grdView.Columns(2).Width = 160
        grdView.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdView.Columns(3).Caption = "Supplier Fax"
        grdView.Columns(3).Width = 160
        grdView.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdView.Columns(4).Caption = "Supplier Phone"
        grdView.Columns(4).Width = 160
        grdView.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdView.Columns(5).Visible = False
        grdView.Columns(6).Visible = False

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

            objattSupplier = New attSupplier
            objattSupplier.PKeyCode = _Id
            ds = objBALSupplier.CheckID(objattSupplier)
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
            objattSupplier = New attSupplier
            objattSupplier.SuppCell = RemoveUnnecessaryChars(txtSuppCell.Text)
            objattSupplier.SuppEmail = RemoveUnnecessaryChars(txtSuppEmail.Text)
            objattSupplier.SuppFax = RemoveUnnecessaryChars(txtSuppFax.Text)
            objattSupplier.SuppName = RemoveUnnecessaryChars(txtSuppName.Text)
            objattSupplier.SuppPhone = RemoveUnnecessaryChars(txtSuppPhone.Text)
            objattSupplier.PKeyCode = RemoveUnnecessaryChars(txtSuppID.Text)
            If objBALSupplier.Update_Supplier(objattSupplier) Then
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

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate Then
                Dim FocRow As Integer
                If isEdit Then
                    If UpdateRecord() Then
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "SuppID", txtSuppID.Text)
                        SetGridRowCellValue(grdView, FocRow, "SuppName", txtSuppName.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "SuppCell", txtSuppCell.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "SuppEmail", txtSuppEmail.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "SuppFax", txtSuppFax.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "SuppPhone", txtSuppPhone.Text.Trim)
                        btnNew_Click(sender, e)
                    End If
                Else
                    If AddNewRecord() Then
                        grdView.AddNewRow()
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "SuppID", txtSuppID.Text)
                        SetGridRowCellValue(grdView, FocRow, "SuppName", txtSuppName.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "SuppCell", txtSuppCell.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "SuppEmail", txtSuppEmail.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "SuppFax", txtSuppFax.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "SuppPhone", txtSuppPhone.Text.Trim)
                        btnNew_Click(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub frmSupplier_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmSupplier = Nothing
    End Sub

    Private Sub frmSupplier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Try
            valProvMain.SetValidationRule(txtSuppID, valRulenotEmpty)
            valProvMain.SetValidationRule(txtSuppName, valRulenotEmpty)
            'txtSuppID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
            'txtSuppID.Properties.Mask.EditMask = "\d{1,8}"
            grd.DataSource = objBALSupplier.GetAll_Supplier(New attSupplier)
            format_Grid()
            btnNew_Click(sender, e)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub grdView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FocRow As Integer = grdView.FocusedRowHandle
        If FocRow >= 0 Then
            txtSuppID.Text = GetGridRowCellValue(grdView, FocRow, "SuppID").ToString()
            txtSuppName.Text = GetGridRowCellValue(grdView, FocRow, "SuppName").ToString()
            txtSuppCell.Text = GetGridRowCellValue(grdView, FocRow, "SuppCell").ToString()
            txtSuppEmail.Text = GetGridRowCellValue(grdView, FocRow, "SuppEmail").ToString()
            txtSuppFax.Text = GetGridRowCellValue(grdView, FocRow, "SuppFax").ToString()
            txtSuppPhone.Text = GetGridRowCellValue(grdView, FocRow, "SuppPhone").ToString()
            isEdit = True
            txtSuppID.Properties.ReadOnly = True
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

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        txtSuppID.Properties.ReadOnly = False
        btnDelete.Visible = False
        isEdit = False
        txtSuppID.Text = "" 'objBALSupplier.GetNextPKey_Supplier
        txtSuppName.Text = ""
        txtSuppCell.Text = ""
        txtSuppEmail.Text = ""
        txtSuppFax.Text = ""
        txtSuppPhone.Text = ""

        txtSuppID.Focus()
        grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        grdView.FocusedColumn = grdView.Columns(0)

        valProvMain.RemoveControlError(txtSuppID)
        valProvMain.RemoveControlError(txtSuppName)

        errProv.ClearErrors()

    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim FocRow As Integer = grdView.FocusedRowHandle
            If FocRow >= 0 Then
                If Not check_Child_AssetDetail(GetGridRowCellValue(grdView, FocRow, "SuppID").ToString(), 5) Then
                    If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        objattSupplier = New attSupplier
                        objattSupplier.PKeyCode = GetGridRowCellValue(grdView, FocRow, "SuppID").ToString()
                        If objBALSupplier.Delete_Supplier(objattSupplier) Then
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
