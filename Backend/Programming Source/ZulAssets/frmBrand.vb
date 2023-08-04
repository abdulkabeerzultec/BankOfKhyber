Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Drawing
Public Class frmBrand
    Dim objattBrand As attBrand
    Dim objBALbrand As New BALbrand
    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Dim isEdit As Boolean = False
    Public addNew_fromAsset As Integer = 0
    Public frmAst As frmAssetsDetails

    Private Function CheckExistValues(ByVal objattBrand As attBrand, ByVal IsInsertStatus As Boolean) As Boolean
        If objBALbrand.BrandExist(objattBrand, IsInsertStatus) Then
            errProv.SetError(txtBrandName, "Brand already exists, change it and try again.")
            txtBrandName.SelectAll()
            txtBrandName.Focus()
            Return True
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
                        SetGridRowCellValue(grdView, FocRow, "AstBrandID", txtBrandCode.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "AstBrandName", txtBrandName.Text.Trim)
                        btnNew_Click(sender, e)
                    End If
                Else
                    If AddNewRecord() Then
                        grdView.AddNewRow()
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "AstBrandID", txtBrandCode.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "AstBrandName", txtBrandName.Text.Trim)
                        btnNew_Click(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub frmBrand_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmBrand = Nothing
    End Sub

    Private Sub frmBrand_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.ActiveControl = txtBrandName
        Try

            valProvMain.SetValidationRule(txtBrandName, valRulenotEmpty)
            grd.DataSource = objBALbrand.GetAll_Brand(New attBrand)
            format_Grid()
            btnNew_Click(sender, e)

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmBrand = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

#Region " -- Method -- "
    Private Sub format_Grid()
        grdView.Columns(0).Caption = "Brand Code"
        grdView.Columns(0).Width = 115
        grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdView.Columns(1).Caption = "Brand Name"
        grdView.Columns(1).Width = 255

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
            objattBrand = New attBrand
            objattBrand.AstBrandName = RemoveUnnecessaryChars(txtBrandName.Text)
            txtBrandCode.Text = objBALbrand.GetNextPKey_Brand()
            objattBrand.PKeyCode = txtBrandCode.Text

            If Not CheckExistValues(objattBrand, True) Then
                If objBALbrand.Insert_Brand(objattBrand) Then
                    ZulMessageBox.ShowMe("Saved")
                    Return True
                Else
                    Return False
                End If
                Return False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function

    Private Function UpdateRecord() As Boolean
        Try
            objattBrand = New attBrand
            objattBrand.AstBrandName = RemoveUnnecessaryChars(txtBrandName.Text)
            objattBrand.PKeyCode = txtBrandCode.Text
            If Not CheckExistValues(objattBrand, False) Then
                If objBALbrand.Update_Brand(objattBrand) Then
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


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim FocRow As Integer = grdView.FocusedRowHandle
            If FocRow >= 0 Then
                If Not check_Child_AssetDetail(GetGridRowCellValue(grdView, FocRow, "AstBrandID").ToString(), 1) Then
                    If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        objattBrand = New attBrand
                        objattBrand.PKeyCode = GetGridRowCellValue(grdView, FocRow, "AstBrandID").ToString()
                        If objBALbrand.Delete_Brand(objattBrand) Then
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
        txtBrandName.Text = ""
        txtBrandCode.Text = objBALbrand.GetNextPKey_Brand()
        txtBrandName.Focus()

        valProvMain.RemoveControlError(txtBrandName)
        errProv.ClearErrors()

        grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        grdView.FocusedColumn = grdView.Columns(0)
    End Sub

    Private Sub grdView_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FocRow As Integer = grdView.FocusedRowHandle
        If FocRow >= 0 Then
            txtBrandCode.Text = GetGridRowCellValue(grdView, FocRow, "AstBrandID").ToString
            txtBrandName.Text = GetGridRowCellValue(grdView, FocRow, "AstBrandName").ToString
            isEdit = True
            btnDelete.Visible = True

            valProvMain.Validate()
            errProv.ClearErrors()
        Else
            btnDelete.Visible = False
        End If

    End Sub

    Private Sub grdView_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdView.KeyDown
        If e.KeyValue = Keys.Delete And btnDelete.Visible Then 'Press Delete
            btnDelete_Click(sender, e)
        End If
    End Sub
End Class
