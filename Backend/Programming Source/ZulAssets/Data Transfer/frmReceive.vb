Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO
Imports System.Text
Imports Xceed.Ftp
Imports System.Net
Imports PingLib.Icmp
Imports OpenNETCF.Desktop.Communication

Public Class frmReceive
    Dim IsProcessNow As Boolean = True
    Dim ftp As FtpClient
    'Dim Fields() As String

    Public Delegate Sub UpUI(ByVal Rowupdated As Integer, ByVal flag As Integer, ByVal FileName As String)
    Public Delegate Sub ErrorMessage(ByVal str As String, ByVal Row As Integer)
    Public Updatehandle As UpUI = AddressOf UpdateUIHandler
    Public Errorhandler As ErrorMessage = AddressOf ErrorHandle

    Dim objattDevices As ZulAssetsDAL.ZulAssetsDAL.attDevices
    Dim objBALDevices As New ZulAssetsBL.ZulAssetBAL.BALDevices

    'Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider

    Dim objattAst_INV_Schedule As attInvSchedule
    Dim objBALAst_INV_Schedule As New BALAst_INV_Schedule


    Dim tempFolder As String = AppConfig.AppDataFolder & "\Temp"
    Dim tempFolderFS As String = AppConfig.AppDataFolder & "\FWatch"


    Dim tmpFolderFWDv As String = "\My Documents\ZulAssets\FWatch"
    Dim tmpFolderDv As String = "\My Documents\ZulAssets\Temp"

    Dim DeviceCount, updDeviceCount As Integer


    Public Sub WritePullPush(ByVal FileName As String, ByVal TabList As String, _
                            ByVal WriteMode As Boolean)
        Dim fs As StreamWriter = Nothing
        Try
            If WriteMode = False Then
                If File.Exists(FileName) Then
                    File.Delete(FileName)
                End If
            End If
            fs = New StreamWriter(FileName, WriteMode)

            fs.WriteLine(TabList)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally
            fs.Close()
        End Try
    End Sub

    Private Function CreateString() As String
        'Return "1" & "," & CInt(chkAsset.Checked) & "," & CInt(chkUsers.Checked) & "," & _
        ' CInt(chkLoc.Checked) & "," & CInt(chkCat.Checked) & "," & IIf(chkAsset.Checked, Val(txtLoc.Tag), "0")
        Return "0,0,0,0,0,0,0,0"
    End Function


    Private Sub btnReceive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReceive.Click
        Try
            updTimer.Enabled = True
            DeviceCount = 0
            updDeviceCount = 0
            Dim IPChkCount As Int16
            Dim str(0) As String
            For i As Int16 = 0 To grdView.RowCount - 1
                If CBool(grdView.GetRowCellValue(i, "Selection")) <> False Then
                    Dim curDevId As String = Trim(grdView.GetRowCellValue(i, "DeviceID"))

                    DeviceCount = DeviceCount + 1
                    IsProcessNow = True
                    grdView.SetRowCellValue(i, "status", "")
                    grdView.SetRowCellValue(i, "Progress", "")

                    WritePullPush(tempFolder & "\" & curDevId & ".txt", curDevId, False)

                    WritePullPush(tempFolder & "\" & curDevId & ".cmd", CreateString, False)

                    If CInt(grdView.GetRowCellValue(i, "ComType")) = 1 Then
                        Dim objRapi As New RAPI
                        If objRapi.DevicePresent = True Then
                            objRapi.Connect()
                        Else
                            grdView.SetRowCellValue(i, "Progress", "Device not connected")
                            Continue For
                        End If
                        WritePullPush(tempFolder & "\" & curDevId & ".cmd", curDevId & ",0", True)


                        'WritePullPush(tempFolder & "\" & curDevID & ".cmd", BALAppConfig.DbServer & "," & Environment.MachineName.ToString, True)

                        '*******************************************************
                        Try

                            If AppConfig.TransMethod = "1" Then
                                WritePullPush(tempFolder & "\" & curDevId & ".cmd", AppConfig.ComServer & "," & AppConfig.ComName & "," & AppConfig.CommPort & "," & AppConfig.ComPass & "," & AppConfig.ComUname, True)
                            Else
                                WritePullPush(tempFolder & "\" & curDevId & ".cmd", AppConfig.ComServer & "," & AppConfig.ComName & "," & AppConfig.CommPort & "," & AppConfig.ComPass & "," & AppConfig.ComUname, True)
                            End If

                        Catch ex As Exception

                            If AppConfig.TransMethod = "1" Then
                                WritePullPush(tempFolder & "\" & curDevId & ".cmd", AppConfig.ComServer & "," & AppConfig.ComName & "," & AppConfig.CommPort & "," & AppConfig.ComPass & "," & AppConfig.ComUname, True)
                                AppConfig.TransMethod = 2
                                'BALAppConfig.Upd_TransMethod("2")
                            Else
                                WritePullPush(tempFolder & "\" & curDevId & ".cmd", AppConfig.ComServer & "," & AppConfig.ComName & "," & AppConfig.CommPort & "," & AppConfig.ComPass & "," & AppConfig.ComUname, True)
                                AppConfig.TransMethod = 1
                                'BALAppConfig.Upd_TransMethod("1")
                            End If

                        End Try



                        'Computer Name    
                        '   

                        '*******************************************************

                        If objRapi.DeviceFileExists(tmpFolderDv & "\" & curDevId & ".st") = False Then
                            grdView.SetRowCellValue(i, "Progress", "Start ZulAssets on Mobile Device")
                            Continue For
                        Else
                            objRapi.CopyFileFromDevice(tempFolder & "\" & curDevId & ".st", _
                            tmpFolderDv & "\" & curDevId & ".st", True)
                            Dim status As String = ReadFile(tempFolder & "\" & curDevId & ".st", False)
                            If Trim(status) = "0" Then
                                grdView.SetRowCellValue(i, "Progress", "Switch To Login Screen On Mobile Device")
                                Continue For
                            Else
                                grdView.SetRowCellValue(i, "Progress", "Please Wait . . .")
                                If objRapi.DeviceFileExists(tmpFolderFWDv & "\" & curDevId & ".cmd") Then
                                    objRapi.DeleteDeviceFile(tmpFolderFWDv & "\" & curDevId & ".cmd")
                                End If
                                objRapi.CopyFileToDevice(tempFolder & "\" & curDevId & ".cmd", _
                                                        tmpFolderFWDv & "\" & curDevId & ".cmd", True)
                            End If

                        End If
                        objRapi.Disconnect()
                    Else
                        'WritePullPush(tempFolder & "\" & curDevId & ".cmd", CreateString, False)
                        CreateCommandString(curDevId)

                        Dim Ftpth As New FtpThreadRcv
                        IPChkCount = IPChkCount + 1
                        str(0) = tempFolder & "\" & curDevId & ".cmd"
                        Ftpth.SetSource(str, 0)
                        Ftpth.SetForm(Me)
                        Ftpth.SetRow(i)
                        Ftpth.SetIpAddress(grdView.GetRowCellValue(i, "DeviceIP"))
                        Ftpth.StartThread()

                    End If
                Else
                    grdView.SetRowCellValue(i, "Progress", "")
                    grdView.SetRowCellValue(i, "status", "")
                End If
            Next
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub CreateCommandString(ByVal deviceID As String)
        WritePullPush(tempFolder & "\" & deviceID & ".cmd", CreateString, False)
        WritePullPush(tempFolder & "\" & deviceID & ".cmd", deviceID & ",0", True)


        '*******************************************************
        Try
            If AppConfig.TransMethod = "1" Then
                WritePullPush(tempFolder & "\" & deviceID & ".cmd", AppConfig.ComServer & "," & AppConfig.ComName & "," & AppConfig.CommPort & "," & AppConfig.ComPass & "," & AppConfig.ComUname, True)
            Else
                WritePullPush(tempFolder & "\" & deviceID & ".cmd", AppConfig.ComServer & "," & AppConfig.ComName & "," & AppConfig.CommPort & "," & AppConfig.ComPass & "," & AppConfig.ComUname, True)
            End If
        Catch ex As Exception
            If AppConfig.TransMethod = "1" Then
                WritePullPush(tempFolder & "\" & deviceID & ".cmd", AppConfig.ComServer & "," & AppConfig.ComName & "," & AppConfig.CommPort & "," & AppConfig.ComPass & "," & AppConfig.ComUname, True)
                AppConfig.TransMethod = 2
                'BALAppConfig.Upd_TransMethod("2")
            Else
                WritePullPush(tempFolder & "\" & deviceID & ".cmd", AppConfig.ComServer & "," & AppConfig.ComName & "," & AppConfig.CommPort & "," & AppConfig.ComPass & "," & AppConfig.ComUname, True)
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

    Public Sub UpdateUIHandler(ByVal Rowupdation As Integer, ByVal flag As Integer, ByVal FileName As String)
        Try
            If flag = 1 Then
                grdView.SetRowCellValue(Rowupdation, 6, "Sending File " + FileName)
            ElseIf flag = 0 Then
                grdView.SetRowCellValue(Rowupdation, 6, "Please Wait . . .")
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub


    Public Sub ErrorHandle(ByVal str As String, ByVal Row As Integer)
        Try
            grdView.SetRowCellValue(Row, "Progress", str)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

   
    Private Sub GetAllDevices(ByVal updStatus As Boolean)
        Dim objattDevicess As New attDevices
        objattDevicess.Status = updStatus
        Dim dt As DataTable = objBALDevices.GetAll_Devices(objattDevicess)
        Dim DC As DataColumn = dt.Columns.Add("Selection", Type.GetType("System.Boolean"))
        DC.SetOrdinal(0)
        For i As Integer = 0 To dt.Rows.Count - 1
            dt.Rows(i)("Selection") = False
        Next
        grd.DataSource = dt
    End Sub

    Private Sub FormatGrid()
        Dim RIType As New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
        RIType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() _
                                                    {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("MS Active Sync", 1, -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Wireless", 2, -1)})

        With grdView
            .OptionsBehavior.Editable = True

            .Columns(0).Caption = ""
            .Columns(0).Width = 20
            .Columns(0).OptionsColumn.AllowEdit = True


            .Columns(1).Caption = "Device ID"
            .Columns(1).Width = 90
            .Columns(1).OptionsColumn.AllowEdit = False
            .Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

            .Columns(2).Caption = "Description"
            .Columns(2).Width = 75
            .Columns(2).OptionsColumn.AllowEdit = False


            .Columns(3).Caption = "Type"
            .Columns(3).ColumnEdit = RIType
            .Columns(3).OptionsColumn.AllowEdit = False

            .Columns(4).Caption = "Device IP"
            .Columns(4).Width = 75
            .Columns(4).OptionsColumn.AllowEdit = False

            .Columns(5).OptionsColumn.AllowEdit = False
            .Columns(5).Caption = "Status"
            .Columns(5).Width = 0
            .Columns(5).Visible = False


            .Columns(6).Visible = False

            .Columns(7).Caption = "Progress"
            .Columns(7).Width = 150
            .Columns(7).OptionsColumn.AllowEdit = False

            .Columns(8).Visible = False
            .Columns(9).Visible = False
        End With
    addGridMenu(grd)
    End Sub

    Private Sub frmReceive_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Down Then

        End If
    End Sub

    Private Sub frmReceive_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Icon = My.Resources.MainIcon
            Me.BackgroundImage = My.Resources.Background
            Me.BackgroundImageLayout = ImageLayout.Stretch

            Control.CheckForIllegalCrossThreadCalls = False
            GetAllDevices(True)
            FormatGrid()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Public Function ReadFile(ByVal FileName As String, ByVal FileWatch As Boolean) As String

        Dim sr As StreamReader = Nothing
        Try
            ' Create an instance of StreamReader to read from a file.
            If FileWatch = True Then
                sr = New StreamReader(AppConfig.AppDataFolder & "\FWatch\" & FileName)
            Else
                sr = New StreamReader(FileName)

            End If

            ' Read and display the lines from the file until the end 
            ' of the file is reached.
            Return sr.ReadLine()

        Catch E As Exception
            ' Let the user know what went wrong.
            'MsgBox("The file could not be read: " & E.Message)
            Return ""
        Finally
            If Not IsNothing(sr) Then sr.Close()

        End Try

    End Function

    Private Sub fsw_Created(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles fsw.Created
        Try
            Dim str As String = ReadFile(e.Name, True)
            Dim file As String = e.Name.Substring(0, e.Name.LastIndexOf("."))
            For count As Int16 = 0 To grdView.RowCount - 1
                If CBool(grdView.GetRowCellValue(count, "Selection")) <> "0" Then
                    If Trim(grdView.GetRowCellValue(count, "DeviceID")) = Trim(file) Then
                        grdView.SetRowCellValue(count, "Progress", "Completed")
                        grdView.SetRowCellValue(count, "status", str)
                        If grdView.GetRowCellValue(count, "Progress").ToString = "Completed" And str = "Success" Then
                            'Dim objBalProcess As New BALprocess
                            'objBalProcess.Process_Data(cmbSch.Text, Trim(grdView.GetRowCellValue(count, "DeviceID")), pb)
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    'Private Sub btnProceed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProceed.Click
    '    Try
    '        For i As Int16 = 0 To grdView.RowCount - 1
    '            If valProvMain.Validate Then
    '                Dim objBalProcess As New BALprocess
    '                objBalProcess.Process_Data(cmbSch.SelectedValue, grdView.GetRowCellValue(i, "DeviceID"), pb)
    '            End If
    '        Next
    '        MessageBox.Show("Data processed successfully")
    '    Catch ex As Exception
    '        GenericExceptionHandler(ex, WhoCalledMe)
    '    End Try
    'End Sub

    Private Sub updTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles updTimer.Tick
        Try
            updTimer.Enabled = False
            Try
                Dim dtTempMessage As DataTable = objBALDevices.GetTempDevicesMessages
                If dtTempMessage.Rows.Count > 0 Then
                    For Each row As DataRow In dtTempMessage.Rows
                        For i As Int16 = 0 To grdView.RowCount - 1
                            If grdView.GetRowCellValue(i, "DeviceID").ToString = row("DeviceID").ToString Then
                                grdView.SetRowCellValue(i, "Progress", row("StatusMsg"))
                                objBALDevices.UpdateTempDevicesMessages(row("DeviceID"))
                                Exit For
                            End If
                        Next
                    Next
                End If
            Catch
            End Try


            For i As Int16 = 0 To grdView.RowCount - 1
                If grdView.GetRowCellValue(i, "Progress").ToString = "Data Received Sucessfully" Then
                    updDeviceCount = updDeviceCount + 1
                End If
            Next

            If DeviceCount = updDeviceCount And DeviceCount <> 0 And updDeviceCount <> 0 Then
                updTimer.Enabled = False
                If MessageBox.Show("Data received sucessfully, do you want to proceed the data?", " ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    ShowDataProcessingForm()
                    'Dim frmProcess As New frmDataProcessing
                    'frmProcess.MdiParent = Me.MdiParent
                    'frmProcess.WindowState = FormWindowState.Normal
                    'frmProcess.StartPosition = FormStartPosition.CenterScreen
                    'frmProcess.Show()
                    'frmProcess.BringToFront()
                    ''frmProcess.ShowDialog()
                    ''frm.Dispose()
                End If


                'If IsProcessNow Then
                '    If valProvMain.Validate Then
                '        For i As Int16 = 0 To grdView.RowCount - 1
                '            Dim objBalProcess As New BALprocess
                '            objBalProcess.Process_Data(cmbSch.SelectedValue, grdView.GetRowCellValue(i, "DeviceID"), pb)
                '        Next
                '        IsProcessNow = False
                '    End If
                'End If
            Else
                updTimer.Enabled = True

            End If
            updDeviceCount = 0
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    'Private Sub btnAddInvSch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddInvSch.Click
    '    Dim frm As New frmAstInvSch
    '    frm.WindowState = FormWindowState.Normal
    '    frm.StartPosition = FormStartPosition.CenterScreen
    '    frm.ShowDialog()
    'End Sub

    Private Sub frmReceive_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        FormController.objfrmReceive = Nothing
    End Sub

End Class