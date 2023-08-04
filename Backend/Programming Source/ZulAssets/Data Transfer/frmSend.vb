Imports System.IO
Imports System.Text
Imports Xceed.Ftp
Imports System.Net
Imports PingLib.Icmp
Imports OpenNETCF.Desktop.Communication
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL


Public Class frmSend
    Implements FtpAccessInterface
    Dim objattDevices As ZulAssetsDAL.ZulAssetsDAL.attDevices
    Dim objBALDevices As New ZulAssetsBL.ZulAssetBAL.BALDevices

    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    Dim objattLocation As attLocation
    Dim objBALLocation As New BALLocation
    Dim objBALTransDataTemp As New BALTransDataTemp
    Dim ftp As FtpClient
    'Dim Fields() As String
    Public Delegate Sub UpUI(ByVal Rowupdated As Integer, ByVal flag As Integer, ByVal FileName As String)
    Public Delegate Sub ErrorMessage(ByVal str As String, ByVal Row As Integer)
    Public Updatehandle As UpUI = AddressOf UpdateUIHandler
    Public Errorhandler As ErrorMessage = AddressOf ErrorHandle

    Dim SelNode As TreeNode
    Dim tempFolder As String = AppConfig.AppDataFolder & "\Temp"
    Dim tempFolderFS As String = AppConfig.AppDataFolder & "\FWatch"


    Dim tmpFolderFWDv As String = "\My Documents\ZulAssets\FWatch"
    Dim tmpFolderDv As String = "\My Documents\ZulAssets\Temp"
    Dim isConfig As Boolean = False

    Private Sub frmSend_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = My.Resources.MainIcon
            Me.BackgroundImage = My.Resources.Background
            Me.BackgroundImageLayout = ImageLayout.Stretch

            Dim objBALCat As New BALCategory
            'cmbClass.Properties.ValueMember = GetGridColumnName("AstCatID")
            'cmbClass.Properties.DisplayMember = GetGridColumnName("AstCatDesc")
            'cmbClass.Properties.DataSource = objBALCat.GetCategoryList

            Dim objBALCost As New BALCostCenter
            Dim objattCost As New attCostCenter
            cmbCostCenter.Properties.ValueMember = GetGridColumnName("CostNumber")
            cmbCostCenter.Properties.DisplayMember = GetGridColumnName("CostName")
            cmbCostCenter.Properties.DataSource = objBALCost.GetComboList(objattCost)

            Dim objBALLocation As New BALLocation
            'cmbLocation.Properties.ValueMember = GetGridColumnName("LocID")
            'cmbLocation.Properties.DisplayMember = GetGridColumnName("LocDesc")
            'cmbLocation.Properties.DataSource = objBALLocation.GetComboLocations(New attLocation)


            updTimer.Enabled = True

            Control.CheckForIllegalCrossThreadCalls = False
            GetAllDevices(True)
            FormatGrid()
            chkAsset.Checked = True
            chkCat.Checked = True
            chkLoc.Checked = True
            chkUsers.Checked = True
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
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


            .Columns(1).Caption = "ID"
            .Columns(1).Width = 40
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
            .Columns(7).Width = 200
            .Columns(7).OptionsColumn.AllowEdit = False
            .Columns(8).Visible = False
            .Columns(9).Visible = False
        End With
        addGridMenu(grd)
    End Sub

    Public Sub UpdateUIHandler(ByVal Rowupdation As Integer, ByVal flag As Integer, ByVal FileName As String) Implements FtpAccessInterface.UpdateUIHandler
        If flag = 1 Then

            grdView.SetRowCellValue(Rowupdation, "Progress", "Sending File " + FileName)
        ElseIf flag = 0 Then
            grdView.SetRowCellValue(Rowupdation, "Progress", "Please Wait . . .")
        ElseIf flag = 2 Then
            grdView.SetRowCellValue(Rowupdation, "Progress", "Device not Configured")
        ElseIf flag = 3 Then
            grdView.SetRowCellValue(Rowupdation, "Progress", "Device not Ready")
        ElseIf flag = 4 Then
            grdView.SetRowCellValue(Rowupdation, "Progress", "Device Configured Now")
        End If
    End Sub


    Public Sub ErrorHandle(ByVal str As String, ByVal Row As Integer) Implements FtpAccessInterface.ErrorHandle
        Try
            grdView.SetRowCellValue(Row, "Progress", str)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub



    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click

        Dim Selection(3) As Boolean
        '0 - Assets
        '1 - Users
        '2 - Locations
        '3 - Categories
        Selection(0) = chkAsset.Checked
        Selection(1) = chkUsers.Checked
        Selection(2) = chkLoc.Checked
        Selection(3) = chkCat.Checked

        Try
            Dim IPChkCount As Int16
            Dim str(0) As String

            For i As Int16 = 0 To grdView.RowCount - 1

                If CBool(grdView.GetRowCellValue(i, "Selection")) <> False Then
                    Dim curDevID As String = grdView.GetRowCellValue(i, "DeviceID")
                    grdView.SetRowCellValue(i, "Progress", "")
                    ' This Function Transfers the Data from Database to Temp Table 
                    ' From Temp Table The Data will be Transferred to Frontend Data i.e. SQL CE Database

                    Dim CostCenter As String = String.Empty
                    If Not String.IsNullOrEmpty(cmbCostCenter.Text) Then
                        CostCenter = String.Format("'{0}'", cmbCostCenter.Text.Replace("'", "").Replace(", ", "','"))
                    End If

                    Dim cat As String = String.Empty
                    If Not String.IsNullOrEmpty(cmbCatAudit.SelectedValue) Then
                        cat = cmbCatAudit.SelectedValue
                    End If

                    Dim Loc As String = String.Empty
                    If Not String.IsNullOrEmpty(cmbLocationAudit.SelectedValue) Then
                        Loc = cmbLocationAudit.SelectedValue
                    End If

                    objBALTransDataTemp.Export_Data_To_Temp(curDevID, Loc, False, Selection, String.Empty, cat, String.Empty, CostCenter)

                    'objBALTransDataTemp.Export_Data_To_Temp(curDevID, ZTLocation.SelectedValue, False, Selection, String.Empty, String.Empty, String.Empty)

                    WritePullPush(tempFolder & "\" & curDevID & ".txt", curDevID, False)

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
                            objRapi.RemoveDeviceDirectory("\My Documents\ZulAssets")
                            objRapi.RemoveDeviceDirectory("\My Documents\ZulAssets\Temp")
                            objRapi.RemoveDeviceDirectory("\My Documents\ZulAssets\Fwatch")
                        Catch ex As Exception

                        End Try
                        Try
                            objRapi.CreateDeviceDirectory("\My Documents\ZulAssets")
                        Catch ex As Exception
                        End Try
                        Try
                            objRapi.CreateDeviceDirectory("\My Documents\ZulAssets\Temp")
                        Catch ex As Exception
                        End Try
                        Try
                            objRapi.CreateDeviceDirectory("\My Documents\ZulAssets\Fwatch")
                        Catch ex As Exception
                        End Try


                        If objRapi.DeviceFileExists(tmpFolderDv & "\DeviceID.txt") = False Then
                            objRapi.CopyFileToDevice(tempFolder & "\" & curDevID & ".txt", _
                                                    tmpFolderDv & "\DeviceID.txt", True)
                            objRapi.Disconnect()
                            grdView.SetRowCellValue(i, "Progress", "Device Configured")
                            Continue For
                        End If

                        If objRapi.DeviceFileExists(tmpFolderDv & "\" & curDevID & ".st") = False Then
                            grdView.SetRowCellValue(i, "Progress", "Start ZulAssets on Mobile Device")
                            Continue For
                        Else
                            objRapi.CopyFileFromDevice(tempFolder & "\" & curDevID & ".st", _
                            tmpFolderDv & "\" & curDevID & ".st", True)
                            Dim status As String = ReadFile(tempFolder & "\" & curDevID & ".st", False)
                            If Trim(status) = "0" Then
                                grdView.SetRowCellValue(i, "Progress", "Switch To Login Screen On The Mobile Device")
                                Continue For
                            Else
                                If ZulMessageBox.ShowMe("ConfirmSendData", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                                    grdView.SetRowCellValue(i, "Progress", "Checking Device Status")

                                    CreateCommandString(curDevID)

                                    grdView.SetRowCellValue(i, "Progress", "Sending Data ...")
                                    objRapi.CopyFileToDevice(tempFolder & "\" & curDevID & ".cmd", _
                                                            tmpFolderFWDv & "\" & _
                                                             curDevID & ".cmd", True)
                                Else
                                    grdView.SetRowCellValue(i, "Progress", "")
                                End If
                            End If
                        End If
                        objRapi.Disconnect()
                    Else
                        If ZulMessageBox.ShowMe("ConfirmSendData", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                            CreateCommandString(curDevID)
                            Dim Ftpth As New FtpThread
                            IPChkCount = IPChkCount + 1
                            str(0) = tempFolder & "\" & curDevID & ".cmd"
                            Ftpth.SetSource(str, 0)
                            Ftpth.SetForm(Me)
                            Ftpth.SetRow(i)
                            Ftpth.SetIpAddress(grdView.GetRowCellValue(i, "DeviceIP"))
                            Ftpth.isConfig = False
                            Ftpth.StartThread()
                        Else
                            grdView.SetRowCellValue(i, "Progress", "")
                        End If
                    End If
                Else
                    grdView.SetRowCellValue(i, "Progress", "")
                End If
            Next
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub CreateCommandString(ByVal devId As String)
        WritePullPush(tempFolder & "\" & devId & ".cmd", CreateString, False)
        WritePullPush(tempFolder & "\" & devId & ".cmd", devId & ",0", True)
        '*************************************
        Try

            If AppConfig.TransMethod = "1" Then
                WritePullPush(tempFolder & "\" & devId & ".cmd", AppConfig.ComServer & "," & AppConfig.ComName & "," & AppConfig.CommPort & "," & AppConfig.ComPass & "," & AppConfig.ComUname, True)
            Else
                WritePullPush(tempFolder & "\" & devId & ".cmd", AppConfig.ComServer & "," & AppConfig.ComName & "," & AppConfig.CommPort & "," & AppConfig.ComPass & "," & AppConfig.ComUname, True)
            End If

        Catch ex As Exception

            If AppConfig.TransMethod = "1" Then
                WritePullPush(tempFolder & "\" & devId & ".cmd", AppConfig.ComServer & "," & AppConfig.ComName & "," & AppConfig.CommPort & "," & AppConfig.ComPass & "," & AppConfig.ComUname, True)
                'BALAppConfig.Upd_TransMethod("2")
                AppConfig.TransMethod = 2
            Else
                WritePullPush(tempFolder & "\" & devId & ".cmd", AppConfig.ComServer & "," & AppConfig.ComName & "," & AppConfig.CommPort & "," & AppConfig.ComPass & "," & AppConfig.ComUname, True)
                'BALAppConfig.Upd_TransMethod("1")
                AppConfig.TransMethod = 1
            End If
        End Try
    End Sub

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

    Private Sub chkAsset_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAsset.CheckedChanged

        grpFilter.Visible = chkAsset.Checked

    End Sub

    Private Function CreateString() As String

        Return "1," & CInt(chkAsset.Checked) & "," & CInt(chkUsers.Checked) & "," & _
                        CInt(chkLoc.Checked) & "," & CInt(chkCat.Checked) & ",0,0,0"
    End Function

    'Private Function CreateString() As String

    '    Return "1," & CInt(chkAsset.Checked) & "," & CInt(chkUsers.Checked) & "," & _
    '                    CInt(chkLoc.Checked) & "," & CInt(chkCat.Checked) & "," & IIf(chkAsset.Checked, ZTLocation.SelectedValue, "0") & ",0,0"
    'End Function


    Private Sub fsw_Created(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles fsw.Created
        Try
            Dim str As String = ReadFile(e.Name, True)
            Dim file As String = e.Name.Substring(0, e.Name.LastIndexOf("."))
            For count As Int16 = 0 To grdView.RowCount - 1
                If CBool(grdView.GetRowCellValue(count, "Selection")) <> "0" Then
                    If Trim(grdView.GetRowCellValue(count, "DeviceID")) = Trim(file) Then
                        grdView.SetRowCellValue(count, "Progress", "Completed")
                        grdView.SetRowCellValue(count, "status", str)
                    End If
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Function ReadFile(ByVal FileName As String, ByVal FileWatch As Boolean) As String Implements FtpAccessInterface.ReadFile
        Dim sr As StreamReader = Nothing
        Try
            ' Create an instance of StreamReader to read from a file.
            If FileWatch = True Then
                sr = New StreamReader(tempFolderFS & "\" & FileName)
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
            If Not IsNothing(sr) Then
                sr.Close()
            End If

        End Try

    End Function

    Private Sub btnConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfig.Click
        Try
            Dim IPChkCount As Int16 = 0
            Dim str(0) As String
            For i As Int16 = 0 To grdView.RowCount - 1
                If CBool(grdView.GetRowCellValue(i, "Selection")) <> False Then
                    Dim curDevID As String = Trim(grdView.GetRowCellValue(i, "DeviceID"))

                    'If IntegrationName = IntegrationType.ABBIntegration Then
                    '    WritePullPush(tempFolder & "\" & curDevID & ".txt", String.Format("{0},{1}", curDevID, "ABBIntegration"), False)
                    'Else
                    WritePullPush(tempFolder & "\" & curDevID & ".txt", curDevID, False)
                    'End If

                    If CInt(grdView.GetRowCellValue(i, "ComType")) = 1 Then
                        Dim objRapi As New RAPI
                        If objRapi.DevicePresent = True Then
                            objRapi.Connect()
                        Else
                            grdView.SetRowCellValue(i, "status", "Device not connected")
                            Continue For
                        End If
                        grdView.SetRowCellValue(i, "Progress", "Please Wait . . .")
                        Try
                            objRapi.CreateDeviceDirectory("\My Documents\ZulAssets")
                        Catch ex As Exception
                        End Try
                        Try
                            objRapi.CreateDeviceDirectory("\My Documents\ZulAssets\Temp")
                        Catch ex As Exception
                        End Try
                        Try
                            objRapi.CreateDeviceDirectory("\My Documents\ZulAssets\Fwatch")
                        Catch ex As Exception
                        End Try

                        If objRapi.DeviceFileExists(tmpFolderDv & "\DeviceID.txt") Then
                            objRapi.DeleteDeviceFile(tmpFolderDv & "\DeviceID.txt")
                        End If
                        objRapi.CopyFileToDevice(tempFolder & "\" & curDevID & ".txt", _
                                                 tmpFolderDv & "\DeviceID.txt", True)
                        objRapi.Disconnect()
                        grdView.SetRowCellValue(i, "Progress", "Device Configured")
                    Else
                        Dim Ftpth As New FtpThread
                        IPChkCount = IPChkCount + 1
                        str(0) = tempFolder & "\" & curDevID & ".cmd"
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
            Next
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub updTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles updTimer.Tick
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
        updTimer.Enabled = True
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Public ReadOnly Property dgDevicesI() As DevExpress.XtraGrid.Views.Grid.GridView Implements FtpAccessInterface.grdDevicesI
        Get
            Return grdView
        End Get
    End Property

    Private Sub frmSend_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        FormController.objfrmSend = Nothing
    End Sub

    Private Sub cmbLocationAudit_TreeBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLocationAudit.TreeBtnClick
        Try
            Dim ds As New DataTable
            Dim objBALLocation As New BALLocation

            cmbLocationAudit.DataSource = objBALLocation.GetComboLocations(New attLocation)
            cmbLocationAudit.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub cmbCatAuditBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCatAudit.TreeBtnClick
        Try
            Dim ds As New DataTable
            Dim objBALCategory As New BALCategory
            cmbCatAudit.DataSource = objBALCategory.GetAll_Category(New attCategory)
            cmbCatAudit.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

End Class