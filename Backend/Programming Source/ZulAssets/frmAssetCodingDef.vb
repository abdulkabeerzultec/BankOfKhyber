Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO
Public Class frmAssetCodingDef
    Dim objBALCompany As New BALCompany
    Dim objattCompany As attcompany

    Dim objattAssetsCoding As attAssetsCoding
    Dim objBALAssetsCoding As New BALAssetsCoding

    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    Dim isEdit As Boolean = False
    Dim ID As String
    Dim ISClosed As Boolean = False

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmAssetCodingDef = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub format_Grid()
        grdView.Columns(0).Visible = False
        grdView.Columns(1).Visible = False
        grdView.Columns(2).Caption = "Company Name"
        grdView.Columns(2).Width = 200
        grdView.Columns(3).Caption = "Start Serial"
        grdView.Columns(3).Width = 150
        grdView.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(4).Caption = "End Serial"
        grdView.Columns(4).Width = 150
        grdView.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(5).Caption = "Status"

        Dim RepositoryItemImageComboBox1 As New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
        RepositoryItemImageComboBox1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() _
                                                    {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Close", "0", -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Open", "1", -1)})

        grdView.Columns(5).ColumnEdit = RepositoryItemImageComboBox1
        grdView.Columns(5).Width = 150

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

        addGridMenu(grd)
        
    End Sub

    Private Sub frmAssetCodingDef_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmAssetCodingDef = Nothing
    End Sub

    Private Sub frmAssetCodingDef_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        valProvMain.SetValidationRule(cmbComp.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(txtStart, valRulenotEmpty)
        valProvMain.SetValidationRule(txtEnd, valRulenotEmpty)

        txtStart.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        txtStart.Properties.Mask.EditMask = "\d{1,15}"

        txtEnd.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        txtEnd.Properties.Mask.EditMask = "\d{1,15}"


        grd.DataSource = objBALAssetsCoding.GetAll_AssetCoding(New attAssetsCoding)
        format_Grid()

        btnNew_Click(sender, e)
    End Sub

    Private Function Verify_Data(ByVal str As Integer) As Boolean
        Try
            'Check Range Validity
            If CLng(txtStart.Text) >= CLng(txtEnd.Text) Then
                errProv.SetError(txtStart, My.MessagesResource.Messages.InvalidRange)
                'ZulMessageBox.ShowMe("InvalidRange")
                Return False
            Else
                If str = 1 Then
                    Dim ds As New DataTable
                    objattAssetsCoding = New attAssetsCoding
                    objattAssetsCoding.CompanyID = cmbComp.SelectedValue
                    objattAssetsCoding.Status = True
                    ds = objBALAssetsCoding.GetAll_AssetCoding(objattAssetsCoding)
                    If ds Is Nothing = False Then
                        If ds.Rows.Count > 0 Then
                            errProv.SetError(cmbComp.TextBox, My.MessagesResource.Messages.RangeComExist)
                            'ZulMessageBox.ShowMe("RangeComExist")
                            Return False
                        End If
                    Else

                    End If
                ElseIf str = 0 Then

                    Dim ds As New DataTable
                    Dim objBALAssetDetails As New BALAssetDetails
                    ds = objBALAssetDetails.Verify_Range(txtStart.Text, txtEnd.Text)
                    If ds Is Nothing = False Then
                        If ds.Rows.Count > 0 Then
                            ZulMessageBox.ShowMe("CantDelete")
                            Return False
                        End If
                    Else

                    End If
                End If
            End If
            Dim ds1 As New DataTable

            ds1 = objBALAssetsCoding.Verify_Range(txtStart.Text, txtEnd.Text, ID)
            If ds1 Is Nothing = False Then
                If ds1.Rows.Count > 0 Then
                    errProv.SetError(txtStart, My.MessagesResource.Messages.RangeExist)
                    errProv.SetError(txtEnd, My.MessagesResource.Messages.RangeExist)
                    'ZulMessageBox.ShowMe("RangeExist")
                    Return False
                End If
            End If
            errProv.ClearErrors()
            Return True
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function

    Private Sub Get_AssetsCoding()
        grd.DataSource = objBALAssetsCoding.GetAll_AssetCoding(New attAssetsCoding)
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate Then
                If ISClosed Then
                    ZulMessageBox.ShowMe("ClosedCannotDelete")
                    Exit Sub
                End If
                If isEdit Then
                    If Verify_Data(3) Then
                        If UpdateRecord() Then
                            isEdit = False
                            ISClosed = False
                            ID = ""
                            Get_AssetsCoding()
                        End If
                    End If
                Else
                    If Verify_Data(1) Then
                        If AddNewRecord() Then
                            Get_AssetsCoding()
                            isEdit = False
                            ISClosed = False
                            ID = ""
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Function AddNewRecord() As Boolean
        Try
            objattAssetsCoding = New attAssetsCoding
            objattAssetsCoding.CompanyID = cmbComp.SelectedValue
            objattAssetsCoding.PKeyCode = objBALAssetsCoding.GetNextPKey_AssetCoding()
            objattAssetsCoding.EndSerial = txtEnd.Text
            objattAssetsCoding.StartSerial = txtStart.Text
            objattAssetsCoding.Status = True
            If objBALAssetsCoding.Insert_AssetCoding(objattAssetsCoding) Then
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
            objattAssetsCoding = New attAssetsCoding
            objattAssetsCoding.CompanyID = cmbComp.SelectedValue
            objattAssetsCoding.PKeyCode = ID
            objattAssetsCoding.EndSerial = txtEnd.Text
            objattAssetsCoding.StartSerial = txtStart.Text
            objattAssetsCoding.Status = True
            If objBALAssetsCoding.Update_AssetCoding(objattAssetsCoding) Then
                ZulMessageBox.ShowMe("Saved")
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim FoucRow As Integer = grdView.FocusedRowHandle
            If FoucRow >= 0 Then
                If ISClosed Then
                    ZulMessageBox.ShowMe("ClosedCannotDelete")
                    Exit Sub
                Else

                    Dim ds As New DataTable
                    Dim objBALAssetDetails As New BALAssetDetails
                    ds = objBALAssetDetails.Verify_Range(txtStart.Text, txtEnd.Text)
                    If ds Is Nothing = False Then
                        If ds.Rows.Count > 0 Then
                            ZulMessageBox.ShowMe("CantDelete")
                            Exit Sub
                        End If
                    End If
                End If

                If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    objattAssetsCoding = New attAssetsCoding
                    objattAssetsCoding.PKeyCode = GetGridRowCellValue(grdView, FoucRow, "AssetCodingID")
                    If objBALAssetsCoding.Delete_AssetCoding(objattAssetsCoding) Then
                        grdView.DeleteSelectedRows()
                        ZulMessageBox.ShowMe("Deleted")
                        btnNew_Click(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        cmbComp.SelectedValue = ""
        cmbComp.SelectedText = ""
        txtEnd.Text = ""
        txtStart.Text = ""
        ID = ""
        isEdit = False

        btnSave.Visible = True
        txtStart.Properties.ReadOnly = False
        txtEnd.Properties.ReadOnly = False
        ISClosed = False
        cmbComp.Focus()

        grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        grdView.FocusedColumn = grdView.Columns(0)

        valProvMain.RemoveControlError(txtStart)
        valProvMain.RemoveControlError(txtEnd)
        valProvMain.RemoveControlError(cmbComp.TextBox)
        errProv.ClearErrors()

    End Sub

    Private Sub grdView_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FoucRow As Integer = grdView.FocusedRowHandle
        If FoucRow >= 0 Then
            cmbComp.SelectedText = GetGridRowCellValue(grdView, FoucRow, "CompanyName").ToString
            cmbComp.SelectedValue = GetGridRowCellValue(grdView, FoucRow, "CompanyID").ToString
            ID = GetGridRowCellValue(grdView, FoucRow, "AssetCodingID")

            txtEnd.Text = GetGridRowCellValue(grdView, FoucRow, "EndSerial")
            txtStart.Text = GetGridRowCellValue(grdView, FoucRow, "StartSerial")
            ISClosed = Not CType(GetGridRowCellValue(grdView, FoucRow, "Status"), Boolean)
            If ISClosed Then
                btnDelete.Visible = False
                btnSave.Visible = False
                txtStart.Properties.ReadOnly = True
                txtEnd.Properties.ReadOnly = True
            Else
                btnDelete.Visible = True
                btnSave.Visible = True
                txtStart.Properties.ReadOnly = False
                txtEnd.Properties.ReadOnly = False
            End If

            isEdit = True
            valProvMain.Validate()
            errProv.ClearErrors()
        Else
            btnDelete.Visible = False
        End If
    End Sub

    Private Sub grdView_CustomDrawCell(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles grdView.CustomDrawCell
        If e.Column.FieldName = GetGridColumnName("Status") Then
            If e.CellValue = True Then
                e.Appearance.ForeColor = Color.Black
            Else
                e.Appearance.ForeColor = Color.Red
            End If
            e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
        End If
    End Sub

    Private Sub grdView_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdView.KeyDown
        If e.KeyValue = Keys.Delete And btnDelete.Visible = True Then
            btnDelete_Click(sender, e)
        End If
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
End Class