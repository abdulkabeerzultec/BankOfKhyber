Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO

Public Class frmItems
    Dim objattItems As attItems
    Dim objBALItems As New BALItems
    Dim objBALCategory As New BALCategory
    Dim objattDepPolicy As attDepPolicy
    Dim objBALDepPolicy As New BALDepPolicy
    Dim isEdit As Boolean = 0
    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    Dim SelNode As TreeNode
    Public AssetId As String

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmItems = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Function PolicyExist(ByVal _id As String) As Boolean
        Try
            Dim ds As New DataTable
            objattDepPolicy = New ZulAssetsDAL.ZulAssetsDAL.attDepPolicy
            objattDepPolicy.AstCatID = _id
            ds = objBALDepPolicy.GetAll_DepPolicy(objattDepPolicy)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate Then
                If PolicyExist(ZTCategory.SelectedValue) Then
                    If isEdit Then
                        UpdateRecord()
                        SaveImage()
                        Get_Items()
                        btnNew_Click(sender, e)
                    Else
                        If CheckID(txtItemID.Text) Then
                            AddNewRecord()
                            SaveImage()
                            Get_Items()
                            btnNew_Click(sender, e)
                        Else
                            MessageBox.Show("Item Code already exists, please try again")
                        End If
                    End If

                Else
                    errProv.SetError(ZTCategory.TextBox, My.MessagesResource.Messages.DefinePolicy)
                End If
            Else
            End If
        Catch ex As Exception
            txtItemID.Text = objBALItems.GetNextPKey_Item()
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Function CheckID(ByVal _Id As String) As Boolean
        Try
            Dim ds As New DataTable
            objattItems = New attItems
            objattItems.PKeyCode = _Id
            ds = objBALItems.CheckID(objattItems) ' Check ID Change
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

    Private Sub frmItems_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmItems = Nothing
    End Sub

    Private Sub frmItems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.ActiveControl = txtDesc

        valProvMain.SetValidationRule(txtItemID, valRulenotEmpty)
        valProvMain.SetValidationRule(ZTCategory.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(txtDesc, valRulenotEmpty)

        btnAddCat.Enabled = Check_Auth("frmCat")

        If AppConfig.ImgType = "Asset Images" Then
            grpImage.Visible = False
        ElseIf AppConfig.ImgType = "Item Images" Then
            grpImage.Visible = True
        End If

        Try
            Get_Items()
            format_Grid()
            If AssetId <> "" Then
                grdView.FocusedRowHandle = grdView.LocateByDisplayText(0, GetGridColumn(grdView, "ItemCode"), AssetId)
            Else
                btnNew_Click(sender, e)
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

#Region " -- Method -- "
    Private Sub format_Grid()
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In grdView.Columns
            col.Visible = False
        Next

        GetGridColumn(grdView, "ItemCode").Caption = "Item Code"
        GetGridColumn(grdView, "ItemCode").Width = 100
        GetGridColumn(grdView, "ItemCode").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        GetGridColumn(grdView, "ItemCode").Visible = True
        GetGridColumn(grdView, "ItemCode").VisibleIndex = 1

        GetGridColumn(grdView, "AstDesc").Caption = "Description"
        GetGridColumn(grdView, "AstDesc").Width = 200
        GetGridColumn(grdView, "AstDesc").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        GetGridColumn(grdView, "AstDesc").Visible = True
        GetGridColumn(grdView, "AstDesc").VisibleIndex = 2

        GetGridColumn(grdView, "CatFullPath").Caption = "Category"
        GetGridColumn(grdView, "CatFullPath").Width = 200
        GetGridColumn(grdView, "CatFullPath").Visible = True
        GetGridColumn(grdView, "CatFullPath").VisibleIndex = 3

        GetGridColumn(grdView, "Warranty").Visible = True
        GetGridColumn(grdView, "Warranty").VisibleIndex = 4
        GetGridColumn(grdView, "Warranty").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

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
            objattItems = New attItems
            objattItems.AstCatID = ZTCategory.SelectedValue
            txtItemID.Text = objBALItems.GetNextPKey_Item()
            objattItems.PKeyCode = txtItemID.Text
            objattItems.AstDesc = RemoveUnnecessaryChars(txtDesc.Text)
            objattItems.Warranty = txtWarranty.Value

            If objBALItems.Insert_Item(objattItems) Then
                ZulMessageBox.ShowMe("Saved")
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function

    Private Function UpdateRecord() As Boolean
        Try
            objattItems = New attItems
            objattItems.AstCatID = ZTCategory.SelectedValue
            objattItems.AstDesc = RemoveUnnecessaryChars(txtDesc.Text)
            objattItems.PKeyCode = RemoveUnnecessaryChars(txtItemID.Text)
            objattItems.AstBrandID = 0
            objattItems.AstModel = ""
            objattItems.AstQty = 0
            objattItems.Warranty = txtWarranty.Value

            If objBALItems.Update_Item(objattItems) Then
                ZulMessageBox.ShowMe("Saved")
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function


    Private Sub Get_Items()
        Try
            grd.DataSource = objBALItems.GetAll_Items(New attItems)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

#End Region

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            'ClearFrame(GroupBox1)
            ZTCategory.SelectedText = ""
            ZTCategory.SelectedValue = ""
            txtDesc.Text = ""
            txtItemID.Text = objBALItems.GetNextPKey_Item()
            txtWarranty.Value = 0
            btnDelete.Visible = False
            btnDelImg.Visible = False
            PBLogo.Image = Nothing
            PBLogo.ImageLocation = Nothing

            isEdit = False
            txtDesc.Focus()

            valProvMain.RemoveControlError(txtItemID)
            valProvMain.RemoveControlError(ZTCategory.TextBox)
            valProvMain.RemoveControlError(txtDesc)
            errProv.ClearErrors()

            grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
            grdView.FocusedColumn = grdView.Columns(0)

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim FocRow As Integer = grdView.FocusedRowHandle
            If FocRow >= 0 Then
                If Not check_Child_AssetDetail(GetGridRowCellValue(grdView, FocRow, "ItemCode").ToString, 6) Then
                    If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        objattItems = New attItems
                        objattItems.PKeyCode = GetGridRowCellValue(grdView, FocRow, "ItemCode").ToString
                        If objBALItems.Delete_Item(objattItems) Then
                            grdView.DeleteSelectedRows()
                            If AppConfig.ImgStorgeLoc = "Shared Folder" Then
                                DeleteImage()
                            End If
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

    Private Sub LoadImage()
        Try
            If AppConfig.ImgType = "Item Images" Then
                If AppConfig.ImgStorgeLoc = "Shared Folder" Then
                    Dim fname As String
                    fname = txtItemID.Text & ".jpg"
                    If File.Exists(AppConfig.ImgPath & "\" & fname) Then
                        PBLogo.ImageLocation = AppConfig.ImgPath & "\" & fname
                        btnDelImg.Visible = True
                    Else
                        btnDelImg.Visible = False
                        PBLogo.Image = Nothing
                        PBLogo.ImageLocation = Nothing
                    End If
                ElseIf AppConfig.ImgStorgeLoc = "Database" Then
                    If AppConfig.ImgType = "Item Images" Then
                        objattItems = New attItems
                        objattItems.PKeyCode = txtItemID.Text
                        Dim bits As Byte() = objBALItems.GetItemImage(objattItems)
                        If bits IsNot Nothing Then
                            Dim ms As New MemoryStream(bits, 0, bits.Length)
                            PBLogo.Image = Image.FromStream(ms, True)
                            btnDelImg.Visible = True
                        Else
                            btnDelImg.Visible = False
                            PBLogo.Image = Nothing
                            PBLogo.ImageLocation = Nothing
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub DeleteImage()
        Try
            If AppConfig.ImgType = "Item Images" Then
                If AppConfig.ImgStorgeLoc = "Shared Folder" Then
                    Dim fname As String = txtItemID.Text & ".jpg"
                    If File.Exists(AppConfig.ImgPath & "\" & fname) Then
                        File.Delete(AppConfig.ImgPath & "\" & fname)
                    End If
                ElseIf AppConfig.ImgStorgeLoc = "Database" Then
                    If AppConfig.ImgType = "Item Images" Then
                        objattItems = New attItems
                        objattItems.PKeyCode = txtItemID.Text
                        objBALItems.DeleteItemImage(objattItems)
                    End If
                End If
            End If
            btnDelImg.Visible = False
            PBLogo.Image = Nothing
            PBLogo.ImageLocation = Nothing
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub SaveImage()
        If File.Exists(PBLogo.ImageLocation) Then
            If AppConfig.ImgType = "Item Images" Then
                Dim fname As String = txtItemID.Text & ".jpg"
                If AppConfig.ImgStorgeLoc = "Shared Folder" Then
                    If PBLogo.ImageLocation <> AppConfig.ImgPath & "\" & fname Then
                        File.Copy(PBLogo.ImageLocation, AppConfig.ImgPath & "\" & fname, True)
                        PBLogo.ImageLocation = AppConfig.ImgPath & "\" & fname
                    End If
                ElseIf AppConfig.ImgStorgeLoc = "Database" Then
                    If AppConfig.ImgType = "Item Images" Then
                        objattItems = New attItems
                        objattItems.PKeyCode = txtItemID.Text
                        Dim fs As New FileStream(PBLogo.ImageLocation, FileMode.Open)
                        Dim Data() As Byte = New [Byte](fs.Length) {}
                        fs.Read(Data, 0, fs.Length)
                        objattItems.Image = Data
                        objBALItems.UpdateItemImage(objattItems)
                        fs.Dispose()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnAddCat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCat.Click
        Dim frm As New frmCategory
        frm.ShowDialog()
    End Sub

    Private Sub grdView_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FocRow As Integer = grdView.FocusedRowHandle
        If FocRow >= 0 Then
            txtItemID.Text = GetGridRowCellValue(grdView, FocRow, "ItemCode").ToString
            txtWarranty.Value = GetGridRowCellValue(grdView, FocRow, "Warranty").ToString

            If Not (GetGridRowCellValue(grdView, FocRow, "AstCatID") Is Nothing) Then
                ZTCategory.SetValue(GetGridRowCellValue(grdView, FocRow, "AstCatID").ToString(), objBALCategory.Comp_Path(GetGridRowCellValue(grdView, FocRow, "AstCatID").ToString()))
            End If
            If Not (GetGridRowCellValue(grdView, FocRow, "AstDesc") Is Nothing) Then txtDesc.Text = GetGridRowCellValue(grdView, FocRow, "AstDesc").ToString()
            isEdit = True
            btnDelete.Visible = True
            valProvMain.Validate()
            errProv.ClearErrors()
            LoadImage()
        Else
            btnDelete.Visible = False
        End If
    End Sub

    Private Sub grdView_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdView.KeyDown
        If e.KeyValue = Keys.Delete And btnDelete.Visible Then
            btnDelete_Click(sender, e)
        End If
    End Sub

    Private Sub PBLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PBLogo.Click
        Try
            If dlgFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
                PBLogo.ImageLocation = dlgFile.FileName
                btnDelImg.Visible = True
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnDelImg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelImg.Click
        Try
            If ZulMessageBox.ShowMe("BeforeDeletedImg", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                DeleteImage()
                ZulMessageBox.ShowMe("AfterDeleteImg")
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub ZTCategory_TreeBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZTCategory.TreeBtnClick
        Try
            Dim ds As New DataTable
            Dim objBALCategory As New BALCategory
            ZTCategory.DataSource = objBALCategory.GetAll_Category(New attCategory)
            ZTCategory.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub PBLogo_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PBLogo.MouseEnter
        PBLogo.BorderStyle = BorderStyle.FixedSingle
    End Sub

    Private Sub PBLogo_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PBLogo.MouseLeave
        PBLogo.BorderStyle = BorderStyle.None
    End Sub
End Class