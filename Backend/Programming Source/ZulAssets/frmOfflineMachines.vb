Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports DevExpress.XtraEditors.DXErrorProvider


Public Class frmOfflineMachines
    Dim objattOfflineMachine As AttOfflineMachines
    Dim objBALOfflineMachine As New BALOfflineMachines
    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Dim CompanyStartSerial As Int64
    Dim CompanyEndSerial As Int64
    Dim isEdit As Boolean

    Private Sub frmOfflineMachines_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmOfflineMachines = Nothing
    End Sub

    Private Sub frmOfflineMachines_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If AppConfig.DbType = "2" Then
            MessageBox.Show("Offline Machines not supported when Connecting to Oracle Database.", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        txtStartSerial.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        txtStartSerial.Properties.Mask.EditMask = "\d{1,15}"

        txtEndSerial.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        txtEndSerial.Properties.Mask.EditMask = "\d{1,15}"

        valProvMain.SetValidationRule(txtMachineID, valRulenotEmpty)
        valProvMain.SetValidationRule(txtMachineDesc, valRulenotEmpty)
        valProvMain.SetValidationRule(txtStartSerial, valRulenotEmpty)
        valProvMain.SetValidationRule(txtEndSerial, valRulenotEmpty)
        valProvMain.SetValidationRule(txtDBPass, valRulenotEmpty)
        valProvMain.SetValidationRule(txtDBUname, valRulenotEmpty)
        valProvMain.SetValidationRule(txtSqlPort, valRulenotEmpty)
        valProvMain.SetValidationRule(cmbDBServer, valRulenotEmpty)
        valProvMain.SetValidationRule(cmbComp, valRulenotEmpty)

        grd.DataSource = objBALOfflineMachine.GetAll_OfflineMachine(New AttOfflineMachines)
        FormatGrid()
        btnNew_Click(sender, e)
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
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

    Private Sub FormatGrid()
        grdView.Columns(0).Caption = "Machine ID"
        grdView.Columns(0).Width = 50
        grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(1).Caption = "Description"
        'grdView.Columns(1).Width = 130

        grdView.Columns(2).Caption = "Server Name"

        grdView.Columns(3).Visible = False
        grdView.Columns(4).Visible = False
        grdView.Columns(5).Visible = False
        grdView.Columns(6).Visible = False

        grdView.Columns(7).Caption = "Last Asset Number"
        grdView.Columns(7).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(8).Caption = "Start Serial"
        grdView.Columns(8).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(8).Visible = False
        grdView.Columns(9).Visible = False
        grdView.Columns(10).Visible = False

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

        addGridMenu(grd)
    End Sub

    Private Function UpdateRecord() As Boolean
        objattOfflineMachine = New AttOfflineMachines
        objattOfflineMachine.MachineID = txtMachineID.Text
        objattOfflineMachine.MachineDesc = txtMachineDesc.Text

        objattOfflineMachine.DatabaseName = "ZulAssetsBE"
        objattOfflineMachine.ServerName = cmbDBServer.Text
        objattOfflineMachine.UserName = txtDBUname.Text
        objattOfflineMachine.Password = txtDBPass.Text
        objattOfflineMachine.Port = txtSqlPort.Text
        objattOfflineMachine.StartSerial = txtStartSerial.Text
        objattOfflineMachine.EndSerial = txtEndSerial.Text
        objattOfflineMachine.CompanyID = cmbComp.SelectedValue


        If Not ValidateData(objattOfflineMachine, False) Then
            If objBALOfflineMachine.Update_OfflineMachine(objattOfflineMachine) Then
                ZulMessageBox.ShowMe("Saved")
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Private Function AddNewRecord() As Boolean
        objattOfflineMachine = New AttOfflineMachines
        objattOfflineMachine.MachineID = txtMachineID.Text
        objattOfflineMachine.MachineDesc = txtMachineDesc.Text

        objattOfflineMachine.DatabaseName = "ZulAssetsBE"
        objattOfflineMachine.ServerName = cmbDBServer.Text
        objattOfflineMachine.UserName = txtDBUname.Text
        objattOfflineMachine.Password = txtDBPass.Text
        objattOfflineMachine.Port = txtSqlPort.Text
        objattOfflineMachine.StartSerial = txtStartSerial.Text
        objattOfflineMachine.EndSerial = txtEndSerial.Text
        objattOfflineMachine.CompanyID = cmbComp.SelectedValue

        If Not ValidateData(objattOfflineMachine, True) Then
            If objBALOfflineMachine.Insert_OfflineMachine(objattOfflineMachine) Then
                ZulMessageBox.ShowMe("Saved")
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Function ValidateData(ByVal objatt As AttOfflineMachines, ByVal IsInsertStatus As Boolean) As Boolean
        'Get the servername without the instance name
        Dim Machineservername As String = ""
        If objatt.ServerName.IndexOf("\") > 0 Then
            Machineservername = objatt.ServerName.Remove(objatt.ServerName.IndexOf("\"), objatt.ServerName.Length - objatt.ServerName.IndexOf("\"))
        Else
            Machineservername = objatt.ServerName
        End If

        Dim CurrentServerName As String = ""
        If AppConfig.DbServer.IndexOf("\") > 0 Then
            CurrentServerName = AppConfig.DbServer.Remove(AppConfig.DbServer.IndexOf("\"), AppConfig.DbServer.Length - AppConfig.DbServer.IndexOf("\"))
        Else
            CurrentServerName = AppConfig.DbServer
        End If

        'Check if another machine have the same serial.
        If objBALOfflineMachine.MachineDescExist(objatt, IsInsertStatus) Then
            errProv.SetError(txtMachineDesc, "Machine Description already exists, change it and try again.")
            txtMachineDesc.SelectAll()
            txtMachineDesc.Focus()
            Return True
        ElseIf objBALOfflineMachine.MachineServerNameExist(objatt, IsInsertStatus) Then
            errProv.SetError(cmbDBServer, "Machine Server Name already exists, change it and try again.")
            cmbDBServer.SelectAll()
            cmbDBServer.Focus()
            Return True
        ElseIf Machineservername = CurrentServerName Then
            errProv.SetError(cmbDBServer, "Machine Server Name must not be equal to current connected server name, change it and try again.")
            cmbDBServer.SelectAll()
            cmbDBServer.Focus()
            Return True
        ElseIf CLng(txtStartSerial.Text) >= CLng(txtEndSerial.Text) Then
            errProv.SetError(txtStartSerial, My.MessagesResource.Messages.InvalidRange)
        ElseIf txtStartSerial.Text < CompanyStartSerial Or txtStartSerial.Text > CompanyEndSerial Then
            errProv.SetError(txtStartSerial, "Start Serial not the company range.")
            txtStartSerial.SelectAll()
            txtStartSerial.Focus()
            Return True
        ElseIf txtEndSerial.Text < CompanyStartSerial Or txtEndSerial.Text > CompanyEndSerial Then
            errProv.SetError(txtEndSerial, "End Serial not the company range.")
            txtEndSerial.SelectAll()
            txtEndSerial.Focus()
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btnGetservers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetservers.Click
        FillServerList(cmbDBServer)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate Then
                Dim FocRow As Integer
                If isEdit Then
                    If UpdateRecord() Then
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "MachineID", txtMachineID.Text)
                        SetGridRowCellValue(grdView, FocRow, "MachineDesc", txtMachineDesc.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "ServerName", cmbDBServer.Text)
                        SetGridRowCellValue(grdView, FocRow, "LastAssetNumber", txtLastAssetNum.Text)
                        SetGridRowCellValue(grdView, FocRow, "StartSerial", txtStartSerial.Text)
                        SetGridRowCellValue(grdView, FocRow, "EndSerial", txtEndSerial.Text)
                        SetGridRowCellValue(grdView, FocRow, "CompanyID", cmbComp.SelectedValue)
                        SetGridRowCellValue(grdView, FocRow, "CompanyName", cmbComp.SelectedText)
                        SetGridRowCellValue(grdView, FocRow, "Password", txtDBPass.Text)
                        SetGridRowCellValue(grdView, FocRow, "UserName", txtDBUname.Text)
                        SetGridRowCellValue(grdView, FocRow, "Port", txtSqlPort.Text)
                        btnNew_Click(sender, e)
                    End If
                Else
                    If AddNewRecord() Then
                        grdView.AddNewRow()
                        FocRow = grdView.FocusedRowHandle
                        SetGridRowCellValue(grdView, FocRow, "MachineID", txtMachineID.Text)
                        SetGridRowCellValue(grdView, FocRow, "MachineDesc", txtMachineDesc.Text.Trim)
                        SetGridRowCellValue(grdView, FocRow, "ServerName", cmbDBServer.Text)
                        SetGridRowCellValue(grdView, FocRow, "LastAssetNumber", txtLastAssetNum.Text)
                        SetGridRowCellValue(grdView, FocRow, "StartSerial", txtStartSerial.Text)
                        SetGridRowCellValue(grdView, FocRow, "EndSerial", txtEndSerial.Text)
                        SetGridRowCellValue(grdView, FocRow, "CompanyID", cmbComp.SelectedValue)
                        SetGridRowCellValue(grdView, FocRow, "CompanyName", cmbComp.SelectedText)
                        SetGridRowCellValue(grdView, FocRow, "Password", txtDBPass.Text)
                        SetGridRowCellValue(grdView, FocRow, "UserName", txtDBUname.Text)
                        SetGridRowCellValue(grdView, FocRow, "Port", txtSqlPort.Text)

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

                If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    objattOfflineMachine = New AttOfflineMachines
                    objattOfflineMachine.MachineID = GetGridRowCellValue(grdView, FocRow, "MachineID").ToString()
                    If objBALOfflineMachine.Delete_OfflineMachine(objattOfflineMachine) Then
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

    Private Sub grdView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FocRow As Integer = grdView.FocusedRowHandle
        If FocRow >= 0 Then
            txtMachineID.Text = GetGridRowCellValue(grdView, FocRow, "MachineID")
            txtMachineDesc.Text = GetGridRowCellValue(grdView, FocRow, "MachineDesc")
            cmbDBServer.Text = GetGridRowCellValue(grdView, FocRow, "ServerName")
            txtLastAssetNum.Text = GetGridRowCellValue(grdView, FocRow, "LastAssetNumber")
            txtStartSerial.Text = GetGridRowCellValue(grdView, FocRow, "StartSerial")
            txtEndSerial.Text = GetGridRowCellValue(grdView, FocRow, "EndSerial")
            cmbComp.SelectedValue = GetGridRowCellValue(grdView, FocRow, "CompanyID").ToString
            cmbComp.SelectedText = GetGridRowCellValue(grdView, FocRow, "CompanyName").ToString
            txtDBPass.Text = GetGridRowCellValue(grdView, FocRow, "Password")
            txtDBUname.Text = GetGridRowCellValue(grdView, FocRow, "UserName")
            txtSqlPort.Text = GetGridRowCellValue(grdView, FocRow, "Port")
            lblConnectionStatus.Visible = False


            isEdit = True
            btnDelete.Visible = True
            btnImport.Visible = True
            btnExport.Visible = True
            valProvMain.Validate()
            errProv.ClearErrors()
        Else
            btnDelete.Visible = False
            btnImport.Visible = False
            btnExport.Visible = False

        End If
    End Sub

    Private Sub grdView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdView.KeyDown
        If e.KeyValue = Keys.Delete And btnDelete.Visible Then 'Press Delete
            btnDelete_Click(sender, e)
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        txtMachineID.Text = objBALOfflineMachine.GetNextPKey_OfflineMachine()
        txtMachineDesc.Text = ""
        txtMachineDesc.Focus()
        cmbDBServer.Text = ""
        txtLastAssetNum.Text = "0"
        txtEndSerial.Text = ""
        txtStartSerial.Text = ""
        cmbComp.SelectedText = ""
        cmbComp.SelectedValue = ""
        txtCompanyRange.Text = ""
        txtDBPass.Text = ""
        txtDBUname.Text = "sa"
        txtSqlPort.Text = "1433"
        lblConnectionStatus.Visible = False

        isEdit = False
        btnDelete.Visible = False
        btnImport.Visible = False
        btnExport.Visible = False

        grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        grdView.FocusedColumn = grdView.Columns(0)

        valProvMain.RemoveControlError(txtMachineID)
        valProvMain.RemoveControlError(txtMachineDesc)

        errProv.ClearErrors()
    End Sub

    Private Sub btnCheckConnection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckConnection.Click
        Dim connected As Boolean = RemoteMachines.CheckConnectionString(cmbDBServer.Text, txtSqlPort.Text, txtDBUname.Text, txtDBPass.Text)
        If connected Then
            lblConnectionStatus.Text = "Connected"
            lblConnectionStatus.ForeColor = Color.Green
        Else
            lblConnectionStatus.Text = "Not Connected"
            lblConnectionStatus.ForeColor = Color.Red
        End If
        lblConnectionStatus.Visible = True
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim msg As String = "All data in destination tables will be deleted." & vbCrLf & _
         "Do you want to proceed?"
        If MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim connected As Boolean = RemoteMachines.CheckConnectionString(cmbDBServer.Text, txtSqlPort.Text, txtDBUname.Text, txtDBPass.Text)
            If connected Then
                lblConnectionStatus.Text = "Connected"
                lblConnectionStatus.ForeColor = Color.Green
                Dim MachineID As Integer = txtMachineID.Text
                ' Check database version it should be same on both current server and remote machine.
                If RemoteMachines.DatabaseVersionSame() Then
                    Dim companyid As Integer = cmbComp.SelectedValue
                    If RemoteMachines.ExportData(MachineID, companyid) Then
                        MessageBox.Show("Exporting data completed successfully", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("Error while exporting the data.", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else
                    MessageBox.Show("Local database version not like remote database version, update the database and try again.", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                lblConnectionStatus.Text = "Not Connected"
                lblConnectionStatus.ForeColor = Color.Red
            End If
            lblConnectionStatus.Visible = True
        End If
    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Dim msg As String = "All assets data will be imported from remote machine, Do you want to proceed?"
        If MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            Dim connected As Boolean = RemoteMachines.CheckConnectionString(cmbDBServer.Text, txtSqlPort.Text, txtDBUname.Text, txtDBPass.Text)
            If connected Then
                lblConnectionStatus.Text = "Connected"
                lblConnectionStatus.ForeColor = Color.Green
                Dim MachineID As Integer = txtMachineID.Text
                ' Check database version it should be same on both current server and remote machine.
                If RemoteMachines.DatabaseVersionSame() Then
                    Dim FocRow As Integer = grdView.FocusedRowHandle
                    If RemoteMachines.ImportData(MachineID, pbAllStatus, lblAllStatus) Then
                        grd.DataSource = objBALOfflineMachine.GetAll_OfflineMachine(New AttOfflineMachines)
                        grdView.FocusedRowHandle = FocRow
                        grdView_FocusedRowChanged(Nothing, Nothing)
                        msg = String.Format("Importing data completed successfully.{0}Total records count({1}), Inserted({2}), Updated({3}), Ignored({4}).", Environment.NewLine, RemoteMachines.TotalCount, RemoteMachines.TotalInserted, RemoteMachines.TotalUpdated, RemoteMachines.TotalIgnored)
                        MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("Error while Importing the data.", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else
                    MessageBox.Show("Local database version not like remote database version, update the database and try again.", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Else
                lblConnectionStatus.Text = "Not Connected"
                lblConnectionStatus.ForeColor = Color.Red
            End If
                lblConnectionStatus.Visible = True
            End If
    End Sub

    Private Sub cmbComp_SelectTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbComp.SelectTextChanged
        Dim objattAssetsCoding As New attAssetsCoding
        Dim objBALAssetsCoding As New BALAssetsCoding
        objattAssetsCoding.CompanyID = cmbComp.SelectedValue
        objattAssetsCoding.Status = True

        Dim dt As DataTable = objBALAssetsCoding.GetAll_AssetCoding(objattAssetsCoding)
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                CompanyStartSerial = dt.Rows(0)("StartSerial")
                CompanyEndSerial = dt.Rows(0)("EndSerial")
                txtCompanyRange.Text = CompanyStartSerial & " - " & CompanyEndSerial
            End If
        End If
    End Sub
End Class