Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class frmAstBooks
#Region " Me Decleration "
    Dim objattDepMeth As attDepreciationMethod
    Dim objBALDepMeth As New BALDepreciationMethod
    Dim objattBookTempl As attBook
    Dim objBALBookTempl As New BALBooks
    Dim isEdit As Boolean = False
    Dim BookId As String

    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
#End Region

    Private Sub frmBookTempl_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmBookTempl = Nothing
    End Sub

    Private Sub frmBookTempl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        Try
            valProvMain.SetValidationRule(txtDesc, valRulenotEmpty)
            valProvMain.SetValidationRule(txtDepCode.TextBox, valRulenotEmpty)
            valProvMain.SetValidationRule(cmbComp.TextBox, valRulenotEmpty)

            btnNewCompany.Enabled = Check_Auth("frmCompany")

            grd.DataSource = objBALBookTempl.GetAll_Book(New attBook)
            format_Grid()

            btnNew_Click(sender, e)

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate Then
                If isEdit Then
                    ZulMessageBox.ShowMe("BeforeBookUpdate", MessageBoxButtons.OK)
                    If UpdateRecord(BookId) Then
                        grd.DataSource = objBALBookTempl.GetAll_Book(New attBook)
                        btnNew_Click(sender, e)
                    End If
                Else
                    If AddNewRecord() Then
                        grd.DataSource = objBALBookTempl.GetAll_Book(New attBook)
                        btnNew_Click(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Function AddNewRecord() As Boolean
        Try
            objattBookTempl = New attBook
            objattBookTempl.DepCode = txtDepCode.SelectedValue
            objattBookTempl.Description = RemoveUnnecessaryChars(txtDesc.Text)

            objattBookTempl.CompanyID = cmbComp.SelectedValue
            objattBookTempl.PKeyCode = objBALBookTempl.GetNextPKey_Book()

            If objBALBookTempl.Insert_Book(objattBookTempl) Then
                Import_Assets(objattBookTempl.PKeyCode, objattBookTempl.DepCode, objattBookTempl.Description, cmbComp.SelectedValue)
                ZulMessageBox.ShowMe("Saved")
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function

    Private Function UpdateRecord(ByVal _ID As String) As Boolean
        Try
            objattBookTempl = New attBook
            objattBookTempl.DepCode = txtDepCode.SelectedValue
            objattBookTempl.Description = RemoveUnnecessaryChars(txtDesc.Text)
            objattBookTempl.CompanyID = cmbComp.SelectedValue
            objattBookTempl.PKeyCode = _ID
            If objBALBookTempl.Update_Book(objattBookTempl) Then
                Import_Assets(_ID, objattBookTempl.DepCode, objattBookTempl.Description, cmbComp.SelectedValue)
                ZulMessageBox.ShowMe("Saved")
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmBookTempl = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub format_Grid()
        grdView.Columns(0).Caption = "Book ID"
        grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(0).Width = 100

        grdView.Columns(1).Caption = "Description"
        grdView.Columns(1).Width = 160
        grdView.Columns(2).Caption = "Depreciation Method"
        grdView.Columns(2).Width = 160
        grdView.Columns(3).Visible = False
        grdView.Columns(4).Visible = False
        grdView.Columns(5).Caption = "Company"
        grdView.Columns(5).Width = 100

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

        addGridMenu(grd)

    End Sub



    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            cmbComp.SelectedIndex = -1
            txtDepCode.SelectedIndex = -1
            txtDesc.Text = ""
            isEdit = False
            txtDepCode.Focus()

            valProvMain.RemoveControlError(cmbComp.TextBox)
            valProvMain.RemoveControlError(txtDepCode.TextBox)
            valProvMain.RemoveControlError(txtDesc)
            errProv.ClearErrors()

            grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
            grdView.FocusedColumn = grdView.Columns(0)
            cmbComp.Enabled = True

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub Import_Assets(ByVal BookID As String, ByVal DepCode As String, ByVal Desc As String, ByVal Comp As String)
        Dim astCount As Integer
        pb.Visible = True
        pb.Value = 0
        Dim dtAssets As DataTable
        dtAssets = Get_AssetsDetails(Comp)
        If dtAssets Is Nothing = False Then
            Dim objattAstBooks As attAstBooks
            Dim objBALAstBooks As New BALAstBooks
            astCount = dtAssets.Rows.Count
            pb.Maximum = astCount
            For Each Dr As DataRow In dtAssets.Rows
                objattAstBooks = New attAstBooks
                objattAstBooks.PKeyCode = BookID
                objattAstBooks.AstID = Dr("AstID").ToString
                objattAstBooks.DepCode = CInt(DepCode)
                objattAstBooks.BookDescription = Desc
                objattAstBooks.LastBookValue = 0.0
                objattAstBooks.CurrentBookValue = (Dr("TAX") + Dr("BaseCost"))

                objattAstBooks.BVUpdate = Dr("SrvDate")
                Dim objBALAssets As New BALItems
                Dim ds As DataTable = objBALAssets.Get_DepPolicy(Dr("ItemCode"))
                If Not ds Is Nothing Then
                    If Not ds Is Nothing Then
                        If ds.Rows.Count > 0 Then
                            objattAstBooks.SalvageValue = ds.Rows(0)("SalvageValue")
                            objattAstBooks.SalvageYear = ds.Rows(0)("SalvageYear")
                        End If
                    End If
                End If

                If Check_BookExist(objattAstBooks.PKeyCode, objattAstBooks.AstID) Then
                    objBALAstBooks.Update_AstBooks(objattAstBooks)
                Else
                    objBALAstBooks.Insert_AstBooks(objattAstBooks)
                End If
                pb.Value = pb.Value + 1
            Next
        End If
        pb.Visible = False
    End Sub

    Private Function Check_BookExist(ByVal _id As String, ByVal _Astid As String) As Boolean
        Try
            Dim objBALAstBooks As New BALAstBooks
            Dim ds As New DataTable
            Dim objattAstBooks1 As New attAstBooks
            objattAstBooks1.PKeyCode = _id
            objattAstBooks1.AstID = _Astid
            ds = objBALAstBooks.CheckID_AstBooks(objattAstBooks1)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If

            Else
                Return False
            End If

            Return False
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function

    Private Function Get_AssetsDetails(ByVal Comp As String) As DataTable
        Try
            Dim ds As New DataTable
            Dim objattAssetDetails As New attAssetDetails
            Dim objBALAssetDetails As New BALAssetDetails
            objattAssetDetails.CompanyID = Comp
            ds = objBALAssetDetails.GetAll_AssetDetails(objattAssetDetails)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Return ds
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
        Return Nothing
    End Function

    Private Sub btnNewCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewCompany.Click
        Dim frm As New frmCompanies
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
    End Sub

    Private Sub grdView_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdView.KeyDown
        If e.KeyValue = Keys.Delete And btnDelete.Visible Then
            btnDelete_Click(sender, e)
        End If
    End Sub

    Private Sub grdView_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FocRow As Integer = grdView.FocusedRowHandle
        If FocRow >= 0 Then
            txtDesc.Text = GetGridRowCellValue(grdView, FocRow, "Description").ToString()
            cmbComp.FindRow(GetGridRowCellValue(grdView, FocRow, "CompanyID").ToString(), GetGridRowCellValue(grdView, FocRow, "CompanyName").ToString())
            txtDepCode.FindRow(GetGridRowCellValue(grdView, FocRow, "DepCode").ToString(), GetGridRowCellValue(grdView, FocRow, "DepDesc").ToString())

            BookId = GetGridRowCellValue(grdView, FocRow, "BookID").ToString

            isEdit = True
            btnDelete.Visible = True
            cmbComp.Enabled = False
            valProvMain.Validate()
            errProv.ClearErrors()
        Else
            btnDelete.Visible = False
        End If
    End Sub
    'strQuery.Append(" Select '' as Selection,Books.BookID,Books.Description,Depreciation_Method.DepDesc,Depreciation_Method.DepCode,Books.CompanyID,Companies.CompanyName from Books ")
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim FocRow As Integer = grdView.FocusedRowHandle
            If FocRow >= 0 Then
                If Not check_Child_AstBooks(GetGridRowCellValue(grdView, FocRow, "BookID").ToString, 12) Then
                    If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then

                        objattBookTempl = New attBook()
                        objattBookTempl.PKeyCode = GetGridRowCellValue(grdView, FocRow, "BookID").ToString
                        If objBALBookTempl.Delete_Book(objattBookTempl) Then
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

    Private Sub txtDepCode_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDepCode.LovBtnClick
        Try
            txtDepCode.ValueMember = "DepCode"
            txtDepCode.DisplayMember = "DepDesc"
            Dim objBALDepMeth As New BALDepreciationMethod
            txtDepCode.DataSource = objBALDepMeth.GetAll_DepreciationMethod(New attDepreciationMethod)
            txtDepCode.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
End Class