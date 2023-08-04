Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Public Class frmCompanies
    Dim objattCompany As attcompany
    Dim objBALCompany As New BALCompany

    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    Dim isEdit As Boolean = False


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try

            FormController.objfrmCompanies = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

#Region " -- Method -- "
    Private Sub format_Grid()
        grdView.Columns(0).Caption = "Company ID"
        grdView.Columns(0).Width = 115
        grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(1).Caption = "Company Code"
        grdView.Columns(1).Width = 255
        grdView.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(2).Caption = "Company Name"
        grdView.Columns(2).Width = 255
        grdView.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(3).Visible = False
        grdView.Columns(4).Visible = False
        grdView.Columns(5).Visible = False
        grdView.Columns(6).Visible = False
        grdView.Columns(7).Visible = False


        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

        addGridMenu(grd)
    End Sub

    Private Function CheckExistValues(ByVal objatt As attcompany, ByVal IsInsertStatus As Boolean) As Boolean
        If objBALCompany.CompanyCodeExist(objatt, IsInsertStatus) Then
            errProv.SetError(txtCCode, "Company Code already exists, change it and try again.")
            txtCCode.SelectAll()
            txtCCode.Focus()
            Return True
        ElseIf objBALCompany.CompanyNameExist(objatt, IsInsertStatus) Then
            errProv.SetError(cmbDepart, "Company Name already exists, change it and try again.")
            cmbDepart.SelectAll()
            cmbDepart.Focus()
            Return True
        Else
            Return False
        End If
    End Function

    Private Function AddNewRecord() As Boolean
        Try
            objattCompany = New attcompany
            txtCID.Text = objBALCompany.GetNextPKey_Company()
            objattCompany.PKeyCode = txtCID.Text
            objattCompany.CompanyCode = RemoveUnnecessaryChars(txtCCode.Text)
            objattCompany.CompanyName = RemoveUnnecessaryChars(cmbDepart.Text)
            objattCompany.HierCode = cmbDepart.Tag
            objattCompany.BarStructID = 1
            If Not CheckExistValues(objattCompany, True) Then
                If objBALCompany.Insert_Company(objattCompany) Then
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
            objattCompany = New attcompany
            objattCompany.PKeyCode = txtCID.Text
            objattCompany.CompanyCode = RemoveUnnecessaryChars(txtCCode.Text)
            objattCompany.CompanyName = RemoveUnnecessaryChars(cmbDepart.Text)

            objattCompany.HierCode = cmbDepart.Tag
            objattCompany.BarStructID = 1
            If Not CheckExistValues(objattCompany, False) Then
                If objBALCompany.Update_Company(objattCompany) Then
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


    Private Sub frmCompanies_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmCompanies = Nothing
    End Sub

    Private Sub frmCompanies_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.ActiveControl = txtCCode

        Try
            valProvMain.SetValidationRule(txtCCode, valRulenotEmpty)
            valProvMain.SetValidationRule(cmbDepart, valRulenotEmpty)

            grd.DataSource = objBALCompany.GetAll_Company(New attcompany)
            format_Grid()
            btnNew_Click(sender, e)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub AddAssetBookForCompany()
        'add asset book for the company if it's tracking or inventory edition.
        If AppConfig.AppEdition = ApplicationEditions.Inventory Or AppConfig.AppEdition = ApplicationEditions.Tracking Then
            Dim objattBookTempl As New attBook
            Dim objBALBookTempl As New BALBooks
            objattBookTempl.PKeyCode = objBALBookTempl.GetNextPKey_Book()
            objattBookTempl.DepCode = 1
            objattBookTempl.Description = RemoveUnnecessaryChars(cmbDepart.Text) & " Book"
            objattBookTempl.CompanyID = txtCID.Text
            objBALBookTempl.Insert_Book(objattBookTempl)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate Then
                Dim FocRow As Integer
                If isEdit Then
                    If UpdateRecord() Then
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "CompanyID", txtCID.Text)
                        SetGridRowCellValue(grdView, FocRow, "CompanyCode", txtCCode.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "CompanyName", cmbDepart.Text.Trim)
                        btnNew_Click(sender, e)
                    End If
                Else
                    If AddNewRecord() Then
                        AddAssetBookForCompany()
                        grdView.AddNewRow()
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "CompanyID", txtCID.Text)
                        SetGridRowCellValue(grdView, FocRow, "CompanyCode", txtCCode.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "CompanyName", cmbDepart.Text.Trim)
                        btnNew_Click(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Function DeleteCompany() As Boolean
        If objBALCompany.Delete_Company(objattCompany) Then
            grdView.DeleteSelectedRows()
            ZulMessageBox.ShowMe("Deleted")
            btnNew_Click(Nothing, Nothing)
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim FocRow As Integer = grdView.FocusedRowHandle
            If FocRow >= 0 Then
                If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    objattCompany = New attcompany
                    objattCompany.PKeyCode = GetGridRowCellValue(grdView, FocRow, "CompanyID").ToString()

                    If objBALCompany.IsThereRelatedAssets(objattCompany) Then
                        If ZulMessageBox.ShowMe("BeforeDeleteCompanyHaveAsset", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                            DeleteCompany()
                        End If
                    Else
                        DeleteCompany()
                    End If

                    'If Not objBALCompany.IsThereRelatedAssets(objattCompany) And Not objBALCompany.IsThereRelatedBooks(objattCompany) Then
                    'Else
                    '    ZulMessageBox.ShowMe("CompanyRelatedRecords")
                    'End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        isEdit = False
        btnDelete.Visible = False
        txtCCode.Text = ""
        cmbDepart.Text = ""
        txtCID.Text = objBALCompany.GetNextPKey_Company()
        txtCCode.Focus()

        valProvMain.RemoveControlError(txtCCode)
        valProvMain.RemoveControlError(cmbDepart)
        errProv.ClearErrors()

        grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        grdView.FocusedColumn = grdView.Columns(0)
    End Sub


    Private Sub btnLOV_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLOV.Click
        Dim obj As New ZulHierTree.clsTree

        If AppConfig.DbType = 1 Then
            obj.DBType = 2
        ElseIf AppConfig.DbType = 2 Then
            obj.DBType = 1
        End If

        obj.SelectOnlyLastNode = False
        obj.DBName = AppConfig.DbName
        obj.DBPass = AppConfig.DbPass
        If AppConfig.DbType = 1 Then
            obj.DBServer = AppConfig.DbServer & "," & AppConfig.DBSQLPort
            'obj.DBServer = AppConfig.DbServer
        Else
            obj.DBServer = AppConfig.DbServer
        End If

        obj.DBUName = AppConfig.DbUname
        obj.OpenTree(cmbDepart)
    End Sub

    Private Sub cmbDepart_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbDepart.KeyDown
        If e.Alt And e.KeyValue = Keys.Down Then
            btnLOV_Click_1(sender, e)
        End If
    End Sub

    Private Sub btnLOV_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnLOV.KeyDown
        If e.Alt And e.KeyValue = Keys.Down Then
            btnLOV_Click_1(sender, e)
        End If
    End Sub

    Private Sub grdView_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdView.KeyDown
        If e.KeyValue = Keys.Delete And btnDelete.Visible Then 'Press Delete
            btnDelete_Click(sender, e)
        End If
    End Sub

    Private Sub grdView_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FocRow As Integer = grdView.FocusedRowHandle
        If FocRow >= 0 Then
            txtCID.Text = GetGridRowCellValue(grdView, FocRow, "CompanyID").ToString
            txtCCode.Text = GetGridRowCellValue(grdView, FocRow, "CompanyCode").ToString
            cmbDepart.Text = GetGridRowCellValue(grdView, FocRow, "CompanyName").ToString
            cmbDepart.Tag = GetGridRowCellValue(grdView, FocRow, "BarStructID").ToString
            isEdit = True
            btnDelete.Visible = True


            valProvMain.Validate()
            errProv.ClearErrors()
        Else
            btnDelete.Visible = False
        End If
    End Sub
End Class