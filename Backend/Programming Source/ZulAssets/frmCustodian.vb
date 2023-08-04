Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class frmCustodian
    Dim objattCustodian As attCustodian
    Dim objBALCustodian As New BALCustodian
    Dim objattOrghier As attOrgHier
    Dim objBALOrgHier As New BALOrgHier
    Dim objattDesignation As attDesignation
    Dim objBALDesignation As New BALDesignation
    Dim objattDept As attDepartment
    Dim objBALDept As New BALDepartment
    Dim isEdit As Boolean = False
    Public Address As String = ""


    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Public addNew_fromBulk As Integer = 0
    Public frmAstBulk As frmPOTrans


    Public Delegate Sub UpDesignation()
    Public UpdateAddress As UpDesignation = AddressOf UpAddressHandler

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        FormController.objfrmCustodian = Nothing
        Me.Dispose()
    End Sub

    Private Sub frmCustodian_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmCustodian = Nothing
    End Sub

    Private Sub UpAddressHandler()
        txtCustAddress.Text = Address
    End Sub

    Private Sub frmCustodian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        Try
            valProvMain.SetValidationRule(txtCustID, valRulenotEmpty)
            valProvMain.SetValidationRule(txtCustName, valRulenotEmpty)
            valProvMain.SetValidationRule(cmbDesg.TextBox, valRulenotEmpty)
            valProvMain.SetValidationRule(cmbDepart, valRulenotEmpty)

            btnAdd.Enabled = Check_Auth("frmDesig")
            btnAddOrgHier.Enabled = Check_Auth("frmOrgHier")
            btnAddress.Enabled = Check_Auth("frmAddress")

            Me.Get_Cust()
            format_Grid()

            btnNew_Click(sender, e)

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
#Region "Method"



    Private Sub format_Grid()
        grdView.Columns(0).Caption = "Custodian ID"
        grdView.Columns(0).Width = 100
        grdView.Columns(1).Caption = "Custodian Name"
        grdView.Columns(1).Width = 150
        grdView.Columns(2).Caption = "Custodian Designation"
        grdView.Columns(2).Width = 200

        grdView.Columns(3).Visible = False
        grdView.Columns(4).Visible = False
        grdView.Columns(5).Visible = False
        grdView.Columns(6).Visible = False
        grdView.Columns(7).Visible = False
        grdView.Columns(8).Visible = False
        grdView.Columns(9).Visible = False

        grdView.Columns(10).Caption = "Custodian Department"
        grdView.Columns(10).Width = 200


        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        addGridMenu(grd)
    End Sub

    Private Function CheckExistValues(ByVal objatt As attCustodian, ByVal IsInsertStatus As Boolean) As Boolean
        If objBALCustodian.CustIDExist(objatt, IsInsertStatus) Then
            errProv.SetError(txtCustID, "Custodian ID already exists, change it and try again.")
            txtCustID.SelectAll()
            txtCustID.Focus()
            Return True
            'ElseIf objBALCustodian.CustNameExist(objatt, IsInsertStatus) Then
            '    errProv.SetError(txtCustName, "Custodian Name already exists, change it and try again.")
            '    txtCustName.SelectAll()
            '    txtCustName.Focus()
            '    Return True
        Else
            Return False
        End If
    End Function


    Private Sub Get_Cust()
        Try
            grd.DataSource = objBALCustodian.GetAllData_Custodian()
            For intRow As Integer = 0 To grdView.RowCount - 1
                SetGridRowCellValue(grdView, intRow, "HierName", objBALOrgHier.HierName(GetGridRowCellValue(grdView, intRow, "HierCode")))
            Next
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub



    Private Function AddNewRecord() As Boolean
        Try
            objattCustodian = New attCustodian
            ' No need to reassign new id to CustID because the user can write his own number
            objattCustodian.PKeyCode = txtCustID.Text.Trim

            objattCustodian.CustodianAddress = txtCustAddress.Text.Trim
            objattCustodian.CustodianCell = txtCustCell.Text.Trim
            objattCustodian.CustodianEmail = txtCustEmail.Text.Trim
            objattCustodian.CustodianFax = txtCustFax.Text.Trim
            objattCustodian.CustodianName = txtCustName.Text.Trim
            objattCustodian.CustodianPhone = txtCustPhone.Text.Trim
            objattCustodian.DesignationID = cmbDesg.SelectedValue
            objattCustodian.DepartmentID = cmbDepart.Tag
            objattCustodian.CustodianCode = txtCode.Text.Trim

            If Not CheckExistValues(objattCustodian, True) Then
                If objBALCustodian.Insert_Custodian(objattCustodian) Then
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
            objattCustodian = New attCustodian

            objattCustodian.PKeyCode = txtCustID.Text.Trim
            objattCustodian.CustodianAddress = txtCustAddress.Text.Trim
            objattCustodian.CustodianCell = txtCustCell.Text.Trim
            objattCustodian.CustodianEmail = txtCustEmail.Text.Trim
            objattCustodian.CustodianFax = txtCustFax.Text.Trim
            objattCustodian.CustodianName = txtCustName.Text.Trim
            objattCustodian.CustodianPhone = txtCustPhone.Text.Trim
            objattCustodian.DesignationID = cmbDesg.SelectedValue
            objattCustodian.DepartmentID = cmbDepart.Tag
            objattCustodian.CustodianCode = txtCode.Text.Trim

            If Not CheckExistValues(objattCustodian, False) Then
                If objBALCustodian.Update_Custodian(objattCustodian) Then
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
                        SetGridRowCellValue(grdView, FocRow, "CustodianID", txtCustID.Text)
                        SetGridRowCellValue(grdView, FocRow, "CustodianCode", txtCode.Text)
                        SetGridRowCellValue(grdView, FocRow, "CustodianName", txtCustName.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "CustodianAddress", txtCustAddress.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "CustodianCell", txtCustCell.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "CustodianEmail", txtCustEmail.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "CustodianFax", txtCustFax.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "CustodianPhone", txtCustPhone.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "Description", cmbDesg.SelectedText)
                        SetGridRowCellValue(grdView, FocRow, "DesignationId", cmbDesg.SelectedValue)
                        SetGridRowCellValue(grdView, FocRow, "HierCode", cmbDepart.Tag)
                        SetGridRowCellValue(grdView, FocRow, "HierName", cmbDepart.Text)

                        btnNew_Click(sender, e)
                    End If
                Else
                    If AddNewRecord() Then
                        grdView.AddNewRow()
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "CustodianID", txtCustID.Text)
                        SetGridRowCellValue(grdView, FocRow, "CustodianCode", txtCode.Text)
                        SetGridRowCellValue(grdView, FocRow, "CustodianName", txtCustName.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "CustodianAddress", txtCustAddress.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "CustodianCell", txtCustCell.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "CustodianEmail", txtCustEmail.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "CustodianFax", txtCustFax.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "CustodianPhone", txtCustPhone.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "Description", cmbDesg.SelectedText)
                        SetGridRowCellValue(grdView, FocRow, "DesignationId", cmbDesg.SelectedValue)
                        SetGridRowCellValue(grdView, FocRow, "HierCode", cmbDepart.Tag)
                        SetGridRowCellValue(grdView, FocRow, "HierName", cmbDepart.Text)
                        btnNew_Click(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        txtCustAddress.Text = ""
        txtCustCell.Text = ""
        txtCustEmail.Text = ""
        txtCustFax.Text = ""
        txtCustName.Text = ""
        txtCustPhone.Text = ""
        txtCustID.Text = ""

        cmbDesg.SelectedText = ""
        cmbDesg.SelectedValue = ""

        cmbDepart.Tag = ""
        cmbDepart.Text = ""
        txtCode.Text = ""

        'txtCustID.Text = objBALCustodian.GetNextPKey_Custodian
        txtCustID.Properties.ReadOnly = False
        isEdit = False
        btnDelete.Visible = False

        grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        grdView.FocusedColumn = grdView.Columns(0)

        valProvMain.RemoveControlError(txtCustID)
        valProvMain.RemoveControlError(txtCustName)
        valProvMain.RemoveControlError(cmbDesg.TextBox)
        valProvMain.RemoveControlError(cmbDepart)

        errProv.ClearErrors()
        txtCustID.Focus()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim FocRow As Integer = grdView.FocusedRowHandle
            If FocRow >= 0 Then
                If Not check_Child_AssetDetail(GetGridRowCellValue(grdView, FocRow, "CustodianID").ToString(), 2) Then
                    If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        objattCustodian = New attCustodian
                        objattCustodian.PKeyCode = GetGridRowCellValue(grdView, FocRow, "CustodianID").ToString()
                        If objBALCustodian.Delete_Custodian(objattCustodian) Then
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

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim frm As New frmDesignation
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
    End Sub

    Private Sub btnAddDept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddOrgHier.Click
        Dim frm As New frmOrgHier
        frm.ShowDialog()
    End Sub


    Private Sub btnAddress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddress.Click
        Dim frm As New frmAddress
        frm.addNew_fromCust = 1
        frm.frmCust = Me
        frm.ShowDialog()
    End Sub

    Private Sub btnLOV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLOV.Click
        Dim obj As New ZulHierTree.clsTree

        If AppConfig.DbType = 1 Then
            obj.DBType = 2
        ElseIf AppConfig.DbType = 2 Then
            obj.DBType = 1
        End If

        'obj.SelectOnlyLastNode = True
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
        If e.KeyData = Keys.Back Or e.KeyData = Keys.Delete Then
            cmbDepart.Text = ""
            cmbDepart.Tag = ""
        ElseIf e.Alt And e.KeyValue = Keys.Down Then
            btnLOV_Click(sender, e)
        End If
    End Sub

    Private Sub btnLOV_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnLOV.KeyDown
        If e.Alt And e.KeyValue = Keys.Down Then
            btnLOV_Click(sender, e)
        End If
    End Sub

    Private Sub grdView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FocRow As Integer = grdView.FocusedRowHandle
        If FocRow >= 0 Then
            txtCustID.Text = GetGridRowCellValue(grdView, FocRow, "CustodianID").ToString()
            txtCode.Text = GetGridRowCellValue(grdView, FocRow, "CustodianCode").ToString()
            txtCustName.Text = GetGridRowCellValue(grdView, FocRow, "CustodianName").ToString()
            txtCustAddress.Text = GetGridRowCellValue(grdView, FocRow, "CustodianAddress").ToString()
            txtCustCell.Text = GetGridRowCellValue(grdView, FocRow, "CustodianCell").ToString()
            txtCustEmail.Text = GetGridRowCellValue(grdView, FocRow, "CustodianEmail").ToString()
            txtCustFax.Text = GetGridRowCellValue(grdView, FocRow, "CustodianFax").ToString()
            txtCustPhone.Text = GetGridRowCellValue(grdView, FocRow, "CustodianPhone").ToString()

            cmbDesg.SelectedValue = GetGridRowCellValue(grdView, FocRow, "DesignationId").ToString
            cmbDesg.SelectedText = GetGridRowCellValue(grdView, FocRow, "Description").ToString

            cmbDepart.Tag = GetGridRowCellValue(grdView, FocRow, "HierCode").ToString()
            cmbDepart.Text = objBALOrgHier.HierName(GetGridRowCellValue(grdView, FocRow, "HierCode").ToString())
            isEdit = True
            btnDelete.Visible = True
            txtCustID.Properties.ReadOnly = True

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

    Private Sub cmbDesg_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDesg.LovBtnClick
        Try
            cmbDesg.ValueMember = "DesignationID"
            cmbDesg.DisplayMember = "Description"
            Dim objBALDesignation As New BALDesignation
            cmbDesg.DataSource = objBALDesignation.GetAll_Designation(New attDesignation)
            cmbDesg.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
End Class

   

