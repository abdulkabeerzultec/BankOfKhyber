Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Data.OleDb
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports OpenNETCF.Desktop.Communication


Public Class frmDevices
    Implements FtpAccessInterface

    Dim objattDevices As ZulAssetsDAL.ZulAssetsDAL.attDevices
    Dim objBALDevices As New ZulAssetsBL.ZulAssetBAL.BALDevices
    Dim isEdit As Boolean = False

    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    Dim cs As String
    Dim commCon As New OleDbConnection

    Dim tempFolder As String = AppConfig.AppDataFolder & "\Temp"
    Dim tempFolderFS As String = AppConfig.AppDataFolder & "\FWatch"

    Private FolderDv As String = "\My Documents\ZulAssets"
    Private tmpFolderDv As String = "\My Documents\ZulAssets\Temp"
    Private tmpFolderFWDv As String = "\My Documents\ZulAssets\FWatch"

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmDevices = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate Then
                If isEdit Then
                    If UpdateRecord() Then
                        GetAllDevices()
                        btnNew_Click(sender, e)
                    End If
                Else
                    If AddNewRecord() Then
                        GetAllDevices()
                        btnNew_Click(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
        'ClearInfo()
    End Sub

    Private Sub ClearInfo()
        isEdit = False
        btnDelete.Visible = False
        btnReg.Visible = False
        txtLic.Properties.ReadOnly = False
        txtDeviceID.Properties.ReadOnly = False

        txtDevDesc.Text = ""
        txtDeviceID.Text = ""
        txtDevIP1.Text = ""
        txtHardID.Text = ""
        txtLic.Text = ""
        txtDeviceID.Focus()
        valProvMain.RemoveControlError(txtDeviceID)
        errProv.ClearErrors()

        grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        grdView.FocusedColumn = grdView.Columns(0)
    End Sub

#Region "Method"

    Private Function AddNewRecord() As Boolean
        Try
            If (CheckID(RemoveUnnecessaryChars(txtDeviceID.Text))) Then
                objattDevices = New ZulAssetsDAL.ZulAssetsDAL.attDevices
                objattDevices.DeviceID = RemoveUnnecessaryChars(txtDeviceID.Text)
                objattDevices.DeviceDesc = RemoveUnnecessaryChars(txtDevDesc.Text)
                objattDevices.DeviceIP = RemoveUnnecessaryChars(txtDevIP1.Text)
                If cmbComType.SelectedIndex >= 0 Then
                    objattDevices.ComType = cmbComType.SelectedIndex + 1
                End If
                If cmbStatus.SelectedIndex >= 0 Then
                    If cmbStatus.SelectedIndex = 1 Then
                        objattDevices.Status = 0
                    Else
                        objattDevices.Status = 1

                    End If
                End If
                If objBALDevices.Insert_Devices(objattDevices) Then
                    ZulMessageBox.ShowMe("Saved")
                    Return True

                Else
                    Return False
                End If
            Else
                errProv.SetError(txtDeviceID, " Device ID Already Exists, please try Again")
                Return False
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Function

    Private Sub format_Grid()
        Dim RIType As New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
        RIType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() _
                                                    {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("MS Active Sync", 1, -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Wireless", 2, -1)})

        Dim RIStatus As New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
        RIStatus.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() _
                                                    {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Active", 1, -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("InActive", 2, -1)})

        With grdView
            .OptionsBehavior.Editable = True
            .Columns(0).Caption = ""
            .Columns(0).Width = 20
            .Columns(0).OptionsColumn.AllowEdit = True

            .Columns(1).Caption = "ID"
            .Columns(1).Width = 40
            .Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(1).OptionsColumn.AllowEdit = False

            .Columns(2).Caption = "Description"
            .Columns(2).Width = 75
            .Columns(2).OptionsColumn.AllowEdit = False

            .Columns(3).Caption = "Type"
            .Columns(3).Width = 60
            .Columns(3).ColumnEdit = RIType
            .Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(3).OptionsColumn.AllowEdit = False

            .Columns(4).Caption = "Device IP"
            .Columns(4).Width = 85
            .Columns(4).OptionsColumn.AllowEdit = False

            .Columns(5).Caption = "Status"
            .Columns(5).Width = 55
            .Columns(5).ColumnEdit = RIStatus
            .Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(5).OptionsColumn.AllowEdit = False

            .Columns(6).Caption = "Hardware ID"
            .Columns(6).Width = 80
            .Columns(6).OptionsColumn.AllowEdit = False

            .Columns(7).Caption = "Progress"
            .Columns(7).Width = 150
            .Columns(7).OptionsColumn.AllowEdit = False

            .Columns(8).Caption = "Serial"
            .Columns(8).Width = 20
            .Columns(8).Visible = False
            .Columns(8).OptionsColumn.AllowEdit = False

            .Columns(9).Caption = "IsRegistered"
            .Columns(9).Width = 20
            .Columns(9).Visible = False
            .Columns(9).OptionsColumn.AllowEdit = False
        End With
        addGridMenu(grd)
    End Sub

    Private Function UpdateRecord() As Boolean
        Try
            objattDevices = New ZulAssetsDAL.ZulAssetsDAL.attDevices
            objattDevices.DeviceID = RemoveUnnecessaryChars(txtDeviceID.Text)
            objattDevices.DeviceDesc = RemoveUnnecessaryChars(txtDevDesc.Text)
            objattDevices.DeviceIP = RemoveUnnecessaryChars(txtDevIP1.Text)

            If cmbComType.SelectedIndex >= 0 Then
                objattDevices.ComType = cmbComType.SelectedIndex + 1
            End If

            If cmbStatus.SelectedIndex >= 0 Then

                If cmbStatus.SelectedIndex = 1 Then
                    objattDevices.Status = 0
                Else
                    objattDevices.Status = 1

                End If
            End If

            If objBALDevices.Update_Devices(objattDevices) Then
                ZulMessageBox.ShowMe("Saved")
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function

    Private Function CheckID(ByVal _Id As String) As Boolean
        Try
            Dim ds As New DataTable
            objattDevices = New ZulAssetsDAL.ZulAssetsDAL.attDevices
            objattDevices.DeviceID = _Id
            ds = objBALDevices.CheckID(objattDevices)
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
#End Region


    Private Sub frmDevices_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmDevices = Nothing
    End Sub


    Private Sub GetAllDevices()
        Dim dt As DataTable = objBALDevices.GetAll_Devices(New attDevices)
        Dim DC As DataColumn = dt.Columns.Add("Selection", Type.GetType("System.Boolean"))
        DC.SetOrdinal(0)
        For i As Integer = 0 To dt.Rows.Count - 1
            dt.Rows(i)("Selection") = False
        Next
        grd.DataSource = dt
    End Sub

    Private Sub frmDevices_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch


        Try
            Control.CheckForIllegalCrossThreadCalls = False
            valProvMain.SetValidationRule(txtDeviceID, valRulenotEmpty)

            GetAllDevices()
            format_Grid()
            txtDeviceID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
            txtDeviceID.Properties.Mask.EditMask = "\d{1,8}"

            cmbStatus.SelectedIndex = 0
            cmbComType.SelectedIndex = 0

            Get_Connected()
            updTimer.Enabled = True
            btnNew_Click(sender, e)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub



    Private Sub grdView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdView.KeyDown
        If e.KeyValue = Keys.Delete And btnDelete.Visible Then 'Press Delete
            btnDelete_Click(sender, e)
        End If
    End Sub


    Private Sub grdView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Try
            Dim FocRow As Integer = grdView.FocusedRowHandle
            If FocRow >= 0 Then
                txtDeviceID.Properties.ReadOnly = True

                txtDeviceID.Text = grdView.GetRowCellValue(FocRow, "DeviceID").ToString()
                txtDevDesc.Text = grdView.GetRowCellValue(FocRow, "DeviceDesc").ToString()
                txtHardID.Text = grdView.GetRowCellValue(FocRow, "HardwareID").ToString()
                txtHardID.Tag = grdView.GetRowCellValue(FocRow, "HardwareID").ToString()
                If grdView.GetRowCellValue(FocRow, "DeviceIP").ToString() <> "" Then txtDevIP1.Text = grdView.GetRowCellValue(FocRow, "DeviceIP").ToString()
                If grdView.GetRowCellValue(FocRow, "ComType").ToString() <> "" Then cmbComType.SelectedIndex = grdView.GetRowCellValue(FocRow, "ComType").ToString() - 1
                If grdView.GetRowCellValue(FocRow, "Status").ToString <> "" Then
                    If CBool(grdView.GetRowCellValue(FocRow, "Status")) = True Then
                        cmbStatus.SelectedIndex = 0
                    Else
                        cmbStatus.SelectedIndex = 1
                    End If
                End If
                If CBool(grdView.GetRowCellValue(FocRow, "IsRegistered")) = True Then
                    txtLic.Text = grdView.GetRowCellValue(FocRow, "LicKey").ToString()
                    btnReg.Visible = True
                Else
                    If grdView.GetRowCellValue(FocRow, "HardwareID").ToString.Trim = "" Or grdView.GetRowCellValue(FocRow, "HardwareID").ToString.Trim Is Nothing Then
                        btnReg.Visible = False
                        txtLic.Text = ""
                    Else
                        btnReg.Visible = True
                        txtLic.Text = grdView.GetRowCellValue(FocRow, "LicKey").ToString.Trim
                    End If
                End If
                isEdit = True
                btnDelete.Visible = True

                valProvMain.RemoveControlError(txtDeviceID)
                errProv.ClearErrors()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub btnPing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPing.Click
        ClearGridStatus()
        Try
            For i As Integer = 0 To grdView.RowCount - 1
                If CBool(grdView.GetRowCellValue(i, "Selection")) = True Then
                    If CBool(grdView.GetRowCellValue(i, "Status")) = True Then
                        If Not CInt(grdView.GetRowCellValue(i, "ComType")) = 1 Then
                            If Not grdView.GetRowCellValue(i, "DeviceIP").ToString().Substring(0, 1) = "." Then
                                Dim objthrPing As New thrPing(grdView.GetRowCellValue(i, "DeviceIP").ToString(), Me, i)
                            Else
                                ErrorHandle("No IP Address available", i)
                            End If
                        Else
                            ErrorHandle("Device is on Active Sync", i)
                        End If
                    Else
                        ErrorHandle("Device Not Active", i)
                    End If
                End If
            Next
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub ClearGridStatus()
        Try
            For i As Integer = 1 To grdView.RowCount - 1
                grdView.SetRowCellValue(i, "Progress", "")
            Next
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim FocRow As Integer = grdView.FocusedRowHandle
            If FocRow >= 0 Then
                If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    Dim deviceid As String
                    deviceid = grdView.GetRowCellValue(FocRow, "DeviceID").ToString()
                    objattDevices = New attDevices
                    objattDevices.DeviceID = deviceid
                    If objBALDevices.Delete_Devices(objattDevices) Then
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
        ClearInfo()
    End Sub

    Public Sub ErrorHandle(ByVal str As String, ByVal Row As Integer) Implements FtpAccessInterface.ErrorHandle
        Try
            grdView.SetRowCellValue(Row, "Progress", str)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Public ReadOnly Property dgDevicesI() As DevExpress.XtraGrid.Views.Grid.GridView Implements FtpAccessInterface.grdDevicesI
        Get
            Return grdView
        End Get
    End Property

    Public Function ReadFile(ByVal FileName As String, ByVal FileWatch As Boolean) As String Implements FtpAccessInterface.ReadFile
        Dim sr As StreamReader = Nothing
        Try
            If FileWatch = True Then
                sr = New StreamReader(tempFolderFS & "\" & FileName)
            Else
                sr = New StreamReader(FileName)
            End If
            Return sr.ReadLine()

        Catch E As Exception
            Return ""
        Finally
            If Not IsNothing(sr) Then
                sr.Close()
            End If
        End Try
    End Function

    Public Sub UpdateUIHandler(ByVal Rowupdation As Integer, ByVal flag As Integer, ByVal FileName As String) Implements FtpAccessInterface.UpdateUIHandler
        Select Case flag
            Case 0
                grdView.SetRowCellValue(Rowupdation, "Progress", "Please Wait . . .")
            Case 1
                grdView.SetRowCellValue(Rowupdation, "Progress", "Sending Notification ") ' + FileName
            Case 2
                grdView.SetRowCellValue(Rowupdation, "Progress", "Device not Configured")
            Case 3
                grdView.SetRowCellValue(Rowupdation, "Progress", "Start ZulAssets on Mobile Device")
            Case 4
                grdView.SetRowCellValue(Rowupdation, "Progress", "Device Configured")
            Case 5
                grdView.SetRowCellValue(Rowupdation, "Progress", "Switch To Login Screen On Device")
            Case 6
                grdView.SetRowCellValue(Rowupdation, "Progress", "Checking Device Configuration")
        End Select
    End Sub

    Private Sub btnConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfig.Click
        ClearGridStatus()
        Try

            Dim IPChkCount As Int16
            Dim str(0) As String
            For i As Int16 = 0 To grdView.RowCount - 1
                If CBool(grdView.GetRowCellValue(i, "Selection")) <> False Then
                    'We must case and pass the correct integration name to the device, because after dotfuscation it will be increpted.
                    'The below line will not work.....
                    'WritePullPush(tempFolder & "\" & Trim(grdView.GetRowCellValue(i, "DeviceID")) & ".txt", String.Format("{0},{1}", Trim(grdView.GetRowCellValue(i, "DeviceID")), IntegrationName.ToString), False)

                    If IntegrationName = IntegrationType.ABBIntegration Then
                        WritePullPush(tempFolder & "\" & Trim(grdView.GetRowCellValue(i, "DeviceID")) & ".txt", String.Format("{0},{1}", Trim(grdView.GetRowCellValue(i, "DeviceID")), "ABBIntegration"), False)
                    ElseIf IntegrationName = IntegrationType.AbunayyanPlugin Then
                        WritePullPush(tempFolder & "\" & Trim(grdView.GetRowCellValue(i, "DeviceID")) & ".txt", String.Format("{0},{1}", Trim(grdView.GetRowCellValue(i, "DeviceID")), "AbunayyanIntegration"), False)
                    Else
                        WritePullPush(tempFolder & "\" & Trim(grdView.GetRowCellValue(i, "DeviceID")) & ".txt", Trim(grdView.GetRowCellValue(i, "DeviceID")), False)
                    End If

                    If CInt(grdView.GetRowCellValue(i, "ComType")) = 1 Then
                        Dim objRapi As New RAPI

                        If objRapi.DevicePresent = True Then
                            objRapi.Connect()
                        Else
                            grdView.SetRowCellValue(i, "Progress", "Device not connected")
                            Continue For
                        End If
                        grdView.SetRowCellValue(i, "Progress", "Please Wait . . .")

                        Try
                            objRapi.CreateDeviceDirectory(FolderDv)
                        Catch ex As Exception
                        End Try

                        Try
                            objRapi.CreateDeviceDirectory(tmpFolderDv)
                        Catch ex As Exception
                        End Try

                        Try
                            objRapi.CreateDeviceDirectory(tmpFolderFWDv)
                        Catch ex As Exception
                        End Try

                        If objRapi.DeviceFileExists(tmpFolderDv & "\DeviceID.txt") Then
                            objRapi.DeleteDeviceFile(tmpFolderDv & "\DeviceID.txt")
                        End If
                        objRapi.CopyFileToDevice(tempFolder & "\" & Trim(grdView.GetRowCellValue(i, "DeviceID")) & ".txt", _
                                                 tmpFolderDv & "\DeviceID.txt", True)
                        objRapi.Disconnect()
                        grdView.SetRowCellValue(i, "Progress", "Device Configured")
                    Else
                        Dim Ftpth As New FtpThread
                        IPChkCount = IPChkCount + 1
                        str(0) = tempFolder & "\" & Trim(grdView.GetRowCellValue(i, "DeviceID")) & ".cmd"
                        Ftpth.SetSource(str, 0)
                        Ftpth.SetForm(Me)
                        Ftpth.SetRow(i)
                        Ftpth.SetIpAddress(grdView.GetRowCellValue(i, "DeviceIP"))
                        Ftpth.isConfig = True
                        Ftpth.StartThread()
                    End If
                Else
                    grdView.SetRowCellValue(i, "Progress", "")
                End If
                Me.Update()
            Next
        Catch ex As Exception

            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Public Sub WritePullPush(ByVal FileName As String, ByVal writeData As String, _
                             ByVal WriteMode As Boolean)

        Dim fs As StreamWriter = Nothing
        Try
            If WriteMode = False Then
                If File.Exists(FileName) Then
                    File.Delete(FileName)
                End If
            End If
            fs = New StreamWriter(FileName, WriteMode)

            fs.WriteLine(writeData)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally
            fs.Close()
        End Try
    End Sub

    Private Sub btnSerial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSerial.Click
        ClearGridStatus()
        Try
            If valProvMain.Validate Then
                Dim IPChkCount As Int16
                Dim str(0) As String
                For i As Int16 = 0 To grdView.RowCount - 1
                    If CBool(grdView.GetRowCellValue(i, "Selection")) <> False Then

                        Dim curDeviceID As String = Trim(grdView.GetRowCellValue(i, "DeviceID"))

                        WritePullPush(tempFolder & "\" & curDeviceID & ".txt", curDeviceID, False)

                        If CInt(grdView.GetRowCellValue(i, "ComType")) = 1 Then
                            Dim objRapi As New RAPI
                            If objRapi.DevicePresent = True Then
                                objRapi.Connect()
                            Else
                                grdView.SetRowCellValue(i, "Progress", "Device not connected")
                                Continue For
                            End If

                            If objRapi.DeviceFileExists(tmpFolderDv & "\" & curDeviceID & ".st") = False Then
                                grdView.SetRowCellValue(i, "Progress", "Start ZulAssets on Mobile Device")
                                Continue For
                            Else
                                objRapi.CopyFileFromDevice(tempFolder & "\" & curDeviceID & ".st", _
                                tmpFolderDv & "\" & curDeviceID & ".st", True)

                                Dim status As String = ReadFile(tempFolder & "\" & curDeviceID & ".st", False)
                                If Trim(status) = "0" Then
                                    grdView.SetRowCellValue(i, "Progress", "Switch To Main Menu Screen On Device")
                                    Continue For
                                ElseIf Trim(status) = "2" Then
                                    grdView.SetRowCellValue(i, "Progress", "Start ZulAssets on Mobile Device")
                                    Continue For
                                Else
                                    If Not Directory.Exists(tempFolder) Then
                                        Directory.CreateDirectory(tempFolder)
                                    End If

                                    'WritePullPush(tempFolder & "\" & curDeviceID & ".cmd", CreateString(curDeviceID, "getSerial"), False)
                                    CreateCommandFile(curDeviceID, "1,0,0,0,0,0,-1,0")

                                    grdView.SetRowCellValue(i, "Progress", "Please Wait . . .")

                                    objRapi.CopyFileToDevice(tempFolder & "\" & curDeviceID & ".cmd", _
                                                             tmpFolderFWDv & "\" & curDeviceID & ".cmd", True)
                                End If
                            End If
                            objRapi.Disconnect()
                        Else
                            'WritePullPush(tempFolder & "\" & curDeviceID & ".cmd", CreateString(curDeviceID, "getSerial"), False)
                            CreateCommandFile(curDeviceID, "1,0,0,0,0,0,-1,0")

                            Dim Ftpth As New FtpThread
                            IPChkCount = IPChkCount + 1
                            str(0) = tempFolder & "\" & curDeviceID & ".cmd"
                            Ftpth.SetSource(str, 0)
                            Ftpth.SetForm(Me)
                            Ftpth.SetRow(i)
                            Ftpth.SetIpAddress(grdView.GetRowCellValue(i, "DeviceIP"))
                            Ftpth.isConfig = False
                            Ftpth.StartThread()
                        End If
                    Else
                        grdView.SetRowCellValue(i, "Progress", "")
                    End If
                Next
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub CreateCommandFile(ByVal deviceId As String, ByVal optionString As String)
        WritePullPush(tempFolder & "\" & deviceId & ".cmd", optionString, False)
        WritePullPush(tempFolder & "\" & deviceId & ".cmd", deviceId & ",0", True)

        Try
            If AppConfig.TransMethod = "1" Then
                WritePullPush(tempFolder & "\" & deviceId & ".cmd", AppConfig.ComServer & "," & AppConfig.ComName & "," & AppConfig.CommPort & "," & AppConfig.ComPass & "," & AppConfig.ComUname, True)
            Else
                WritePullPush(tempFolder & "\" & deviceId & ".cmd", AppConfig.ComServer & "," & AppConfig.ComName & "," & AppConfig.CommPort & "," & AppConfig.ComPass & "," & AppConfig.ComUname, True)
            End If

        Catch ex As Exception

            If AppConfig.TransMethod = "1" Then
                WritePullPush(tempFolder & "\" & deviceId & ".cmd", AppConfig.ComServer & "," & AppConfig.ComName & "," & AppConfig.CommPort & "," & AppConfig.ComPass & "," & AppConfig.ComUname, True)
                AppConfig.TransMethod = 2
                'BALAppConfig.Upd_TransMethod("2")
            Else
                WritePullPush(tempFolder & "\" & deviceId & ".cmd", AppConfig.ComServer & "," & AppConfig.ComName & "," & AppConfig.CommPort & "," & AppConfig.ComPass & "," & AppConfig.ComUname, True)
                AppConfig.TransMethod = 1
                'BALAppConfig.Upd_TransMethod("1")
            End If
        End Try
    End Sub

    Private Function NametoIP(ByVal Name As String) As String
        Dim IPHEntry As IPHostEntry
        Dim IPAdd() As IPAddress

        IPHEntry = Dns.GetHostEntry(Name)
        IPAdd = IPHEntry.AddressList
        For i As Integer = 0 To IPAdd.GetUpperBound(0)
            Return IPAdd(i).ToString()
        Next
        Return ""
    End Function

    Private Sub Get_Connected()
        cs = ""
        If AppConfig.DbType = "1" Then
            cs = "Provider=SQLNCLI11;DATA SOURCE=" & AppConfig.ComServer & "," & AppConfig.CommPort & ";UID=" & AppConfig.ComUname & ";PWD=" & AppConfig.ComPass & ";DATABASE=" & AppConfig.ComName & ""
        ElseIf AppConfig.DbType = "2" Then
            cs = "Provider=SQLNCLI11;DATA SOURCE=" & AppConfig.ComServer & "," & AppConfig.CommPort & ";UID=" & AppConfig.ComUname & ";PWD=" & AppConfig.ComPass & ";DATABASE=" & AppConfig.ComName & ""
        End If
        commCon = New OleDbConnection(cs)
        commCon.Open()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles updTimer.Tick
        updTimer.Enabled = False
        Dim qry As String
        Dim cmd As New OleDb.OleDbCommand
        Dim cmd1 As New OleDb.OleDbCommand

        Dim idReader As OleDb.OleDbDataReader
        Dim dataAvb As Boolean = True

        If Not commCon.State = ConnectionState.Closed Then
            commCon.Close()
        End If

        Try
            qry = "select * from Devices where Updated = 1"
            cmd.Connection = commCon
            cmd.CommandText = qry

            cmd1.Connection = commCon

            If Not commCon.State = ConnectionState.Closed Then
                commCon.Close()
            End If
            commCon.Open()

            idReader = cmd.ExecuteReader()

            If idReader.HasRows = True Then
                'dgDevices.DoubleBuffer = True

                dataAvb = idReader.Read

                Dim devId As String
                Dim devStatus As Integer
                Dim devMsg As String

                'GetDevices()

                While dataAvb
                    devId = idReader.Item("DeviceID")
                    devMsg = idReader.Item("StatusMsg")
                    devStatus = idReader.Item("StatusID")

                    'Status IDs
                    '5 - Serial Received
                    '6 - License Done

                    For i As Int16 = 0 To grdView.RowCount - 1
                        If grdView.GetRowCellValue(i, "DeviceID").ToString = devId Then
                            qry = "update Devices set Updated = 0 where DeviceID='" & devId & "'"
                            cmd1.CommandText = qry
                            cmd1.ExecuteNonQuery()

                            If devStatus = 5 Then
                                qry = "select HardwareID from Devices where DeviceID='" & devId & "'"
                                cmd1.CommandText = qry
                                Dim hardSerialNo As String = cmd1.ExecuteScalar()

                                grdView.SetRowCellValue(i, "HardwareID", hardSerialNo)
                            ElseIf devStatus = 6 Then
                                qry = "Update Devices set IsRegistered=1 where DeviceID='" & devId & "'"
                                cmd1.CommandText = qry
                                cmd1.ExecuteNonQuery()

                                grdView.SetRowCellValue(i, "IsRegistered", True)
                            End If

                            grdView.SetRowCellValue(i, "Progress", devMsg)
                            Exit For
                        End If
                    Next
                    dataAvb = idReader.Read
                End While
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

        If Not commCon.State = ConnectionState.Closed Then
            commCon.Close()
        End If

        updTimer.Enabled = True
    End Sub

    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click
        If txtLic.Text Is Nothing Or txtLic.Text = "" Then
            MsgBox("Enter License Key")
            Exit Sub
        End If

        If txtHardID.Text Is Nothing Or txtHardID.Text = "" Then
            MsgBox("Get serial no from device")
            Exit Sub
        End If

        Dim lic As New FrontEndLic()
        If grdView.GetRowCellValue(grdView.FocusedRowHandle, "Status") = True Then
            If lic.ValidateKey(txtHardID.Text.Trim, txtLic.Text.Trim) Then
                ClearGridStatus()
                Try
                    If valProvMain.Validate Then
                        Dim IPChkCount As Int16
                        Dim str(0) As String
                        Dim curDeviceID As String = ""

                        curDeviceID = Trim(grdView.GetRowCellValue(grdView.FocusedRowHandle, "DeviceID"))

                        enterLicenceInfo(curDeviceID, txtLic.Text.Trim)

                        'If Me.dgDevices.Item(dgDevices.RowSel, "Selection") <> False Then
                        WritePullPush(tempFolder & "\" & curDeviceID & ".txt", curDeviceID, False)

                        If CInt(grdView.GetRowCellValue(grdView.FocusedRowHandle, "ComType")) = 1 Then
                            Dim objRapi As New RAPI
                            If objRapi.DevicePresent = True Then
                                objRapi.Connect()
                            Else
                                grdView.SetRowCellValue(grdView.FocusedRowHandle, "Progress", "Device not connected")
                                Exit Sub
                            End If

                            If objRapi.DeviceFileExists(tmpFolderDv & "\" & curDeviceID & ".st") = False Then
                                grdView.SetRowCellValue(grdView.FocusedRowHandle, "Progress", "Start ZulAssets on Mobile Device")
                                Exit Sub
                            Else
                                objRapi.CopyFileFromDevice(tempFolder & "\" & curDeviceID & ".st", _
                                tmpFolderDv & "\" & curDeviceID & ".st", True)
                                Dim status As String = ReadFile(tempFolder & "\" & curDeviceID & ".st", False)
                                If Trim(status) = "0" Then
                                    grdView.SetRowCellValue(grdView.FocusedRowHandle, "Progress", "Switch To Main Menu Screen On Mobile Device")
                                    Exit Sub
                                ElseIf Trim(status) = "2" Then
                                    grdView.SetRowCellValue(grdView.FocusedRowHandle, "Progress", "Start ZulAssets on Mobile Device")
                                    Exit Sub
                                Else
                                    If Not Directory.Exists(tempFolder) Then
                                        Directory.CreateDirectory(tempFolder)
                                    End If

                                    CreateCommandFile(curDeviceID, "1,0,0,0,0,0,0,-1")

                                    grdView.SetRowCellValue(grdView.FocusedRowHandle, "Progress", "Please Wait . . .")

                                    objRapi.CopyFileToDevice(tempFolder & "\" & curDeviceID & ".cmd", _
                                                             tmpFolderFWDv & "\" & curDeviceID & ".cmd", True)
                                End If
                            End If
                            objRapi.Disconnect()
                        Else
                            'WritePullPush(tempFolder & "\" & curDeviceID & ".cmd", CreateString(curDeviceID, "setLic"), False)
                            CreateCommandFile(curDeviceID, "1,0,0,0,0,0,0,-1")
                            Dim Ftpth As New FtpThread
                            IPChkCount = IPChkCount + 1
                            str(0) = tempFolder & "\" & curDeviceID & ".cmd"
                            Ftpth.SetSource(str, 0)
                            Ftpth.SetForm(Me)
                            Ftpth.SetRow(grdView.FocusedRowHandle)
                            Ftpth.SetIpAddress(grdView.GetRowCellValue(grdView.FocusedRowHandle, "DeviceIP"))
                            Ftpth.StartThread()
                        End If
                        'Else
                        '    Me.dgDevices.Item(dgDevices.RowSel, "Progress") = ""
                        'End If

                    End If
                Catch ex As Exception
                    GenericExceptionHandler(ex, WhoCalledMe)
                End Try
            Else
                MsgBox("Invalid License Key")
            End If
        End If
    End Sub

    Private Sub enterLicenceInfo(ByVal DeviceID As String, ByVal LicKey As String)
        Try
            If Not commCon.State = ConnectionState.Open Then
                commCon.Open()
            End If

            Dim sqlCmd As New OleDbCommand
            sqlCmd.Connection = commCon
            sqlCmd.CommandText = "Update Devices set LicKey = '" & LicKey & "' Where DeviceID = '" & DeviceID & "'"
            sqlCmd.ExecuteNonQuery()

            If Not commCon.State = ConnectionState.Closed Then
                commCon.Close()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub SendSerialKeyEmail()
        Try

            Dim message As MailMessage = New MailMessage()
            message.From = New MailAddress(AppConfig.CompanyEmail, AppConfig.CompanyName)

            message.To.Add(New MailAddress("it-support@zultec.com"))
            message.CC.Add(New MailAddress("haseeb@zultec.com"))
            message.Bcc.Add(New MailAddress("wael.dalloul@zultec.com"))
            'message.To.Add(New MailAddress("arham.muslim@zultec.com"))

            message.Subject = "Device Serial Request for ZulAssets " & My.Application.Info.Version.ToString

            message.Body = formatSerialNumbers()

            Dim client As SmtpClient = New SmtpClient()
            client.Host = "smtp.zultec.com"

            client.Send(message)
            'MsgBox("License Key Send")
        Catch ex As Exception
            ClearGridStatus()
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Function formatSerialNumbers() As String
        Dim serials As String = ""
        ClearGridStatus()
        Try
            For i As Integer = 0 To grdView.RowCount - 1
                If CBool(grdView.GetRowCellValue(i, "Selection")) = True Then
                    If CBool(grdView.GetRowCellValue(i, "Status")) = True Then
                        If grdView.GetRowCellValue(i, "HardwareID") IsNot Nothing And Not IsDBNull(grdView.GetRowCellValue(i, "HardwareID")) Then
                            serials &= grdView.GetRowCellValue(i, "HardwareID").ToString & Environment.NewLine
                            grdView.SetRowCellValue(i, "Progress", "Serial Number Send")
                        End If
                        'Else
                        '    ErrorHandle("Device Not Active", i)
                    End If
                End If
            Next
            If serials <> "" Then
                serials = "Device Serial Numbers" & Environment.NewLine & Environment.NewLine & serials
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
        Return serials
    End Function

    Public Sub SendSerialKeyFile()
        Try
            Dim writeText As String = formatSerialNumbers()

            If writeText Is Nothing OrElse writeText = "" Then
                MsgBox("No Device Selected")
                Exit Sub
            End If

            'If saveFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim writer As StreamWriter = Nothing
            Try
                writer = New StreamWriter(AppConfig.AppDataFolder & "\DeviceSerials.txt")
                writer.WriteLine(writeText)
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            Finally
                If writer IsNot Nothing Then
                    writer.Close()
                    writer = Nothing
                End If
            End Try
            'End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnSendFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendFile.Click
        SendSerialKeyFile()
        SendSerialKeyEmail()
    End Sub

    Private Sub grdView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdView.Click
        grdView_FocusedRowChanged(Nothing, Nothing)
    End Sub
End Class