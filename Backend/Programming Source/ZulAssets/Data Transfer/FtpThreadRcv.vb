Imports Xceed.Ftp
Imports System.Net
Imports PingLib.Icmp
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class FtpThreadRcv
    Dim Ip As String
    Dim IPArray() As IPAddress
    Dim Row As Integer
    Dim ftp As FtpClient
    Dim Source() As String
    Dim Sourcecount As Integer
    Dim Destination As String
    Dim frmFtp As New frmReceive

    Dim tempFolder As String = AppConfig.AppDataFolder & "\Temp"
    Dim tempFolderFS As String = AppConfig.AppDataFolder & "\FWatch"
    Dim tmpFolderFWDv As String = "\My Documents\ZulAssets\FWatch"
    Dim tmpFolderDv As String = "\My Documents\ZulAssets\Temp"

    Public Sub SetForm(ByVal f As frmReceive)
        frmFtp = f
    End Sub
    Public Sub SetIpAddress(ByVal Ipaddress As String)
        Ip = Ipaddress
    End Sub
    Public Sub SetIPAddressArray(ByVal ips() As IPAddress)
        IPArray = ips
    End Sub
    Public Sub SetRow(ByVal Rowupdation As Integer)
        Row = Rowupdation
    End Sub
    Public Sub StartThread()
        Dim FtpTh As New System.Threading.Thread(AddressOf Initiate)
        FtpTh.Start()
    End Sub

    Public Sub SetSource(ByVal Src() As String, ByVal items As Integer)
        Sourcecount = items
        Source = Src
    End Sub

    Sub Initiate()

        '  Dim counter As Integer = 0
        Xceed.Ftp.Licenser.LicenseKey = "FTN10-K8FS0-ARMXE-MGGA"
        ftp = New FtpClient
        frmFtp.Errorhandler("Searching for " + Ip, Row)

        Dim pingClass As New ICMPClass()

        If pingClass.Ping(Ip).Status = ICMPClass.ICMPStatusEnum.Success Then

            System.Threading.Thread.Sleep(50)
            Try

                '*****Create Text File Based on Device ID

                'frmFtp.WritePullPush(tempFolder & "\" & Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")) & ".cmd", _
                '                     Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")) & "," & _
                '                     Trim(frmFtp.dgDevices.Item(Row, 3)), True)
                '' '' ''frmFtp.WritePullPush(tempFolder & "\" & Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")) & ".cmd", BALAppConfig.DbServer & "," & Environment.MachineName.ToString, True)
                '' '' ''Addthisline
                'frmFtp.WritePullPush(tempFolder & "\" & Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")) & ".cmd", BALAppConfig.DbServer & "," & Environment.MachineName.ToString & "," & BALAppConfig.SQLPort & "," & BALAppConfig.DbPass & "," & BALAppConfig.DbUname, True)

                '*****************************************

                ftp.Connect(Ip)
                ftp.Login()
                frmFtp.Updatehandle.Invoke(Row, 1, Source(0))

                Dim status As String = "0"
                Try
                    ftp.ReceiveFile(tmpFolderDv & "\" & Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")) & ".st", tempFolder & "\" & Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")) & ".st")
                    status = frmFtp.ReadFile(tempFolder & "\" & Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")) & ".st", False)
                    System.IO.File.Delete(tempFolder & "\" & Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")) & ".st")
                Catch e As System.IO.IOException
                    'frmFtp.WritePullPush(tempFolder & "\" & Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")) & ".txt", Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")), False)
                    ftp.SendFile(tempFolder & "\" & Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")) & ".txt", tmpFolderDv & "\DeviceID.txt")
                    System.IO.File.Delete(tempFolder & "\" & Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")) & ".txt")
                    frmFtp.Updatehandle.Invoke(Row, 2, Nothing)
                    ftp.Disconnect()
                    Exit Sub
                Catch ex As Exception
                    'frmFtp.WritePullPush(tempFolder & "\" & Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")) & ".txt", Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")), False)
                    ftp.SendFile(tempFolder & "\" & Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")) & ".txt", tmpFolderDv & "\DeviceID.txt")
                    System.IO.File.Delete(tempFolder & "\" & Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")) & ".txt")
                    frmFtp.Updatehandle.Invoke(Row, 2, Nothing)
                    ftp.Disconnect()
                    Exit Sub
                End Try
                'ftp.SendFile(Source(0), "\Program Files\test\Assets.db")
                ftp.SendFile(tempFolder & "\" & Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")) & ".cmd", tmpFolderFWDv & "\" & Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")) & ".cmd")
                'ftp.ReceiveFile("\Program Files\test\Assets.db", "C:\imran007.txt")
                frmFtp.Updatehandle.Invoke(Row, 0, Nothing)
                ftp.Disconnect()
                Try
                    '  Dim objBalProcess As New BALprocess
                    '  objBalProcess.Process_Data(frmFtp.cmbSch.Text, Trim(frmFtp.grdView.GetRowCellValue(Row, "DeviceID")))
                Catch ex As Exception
                    GenericExceptionHandler(ex, WhoCalledMe)
                End Try

            Catch ex As Exception
                frmFtp.Errorhandler.Invoke("Anonymous Access Denied", Me.Row)
            End Try
        Else
            frmFtp.Errorhandler.Invoke("Device doest not Exists", Row)
        End If
    End Sub

    'Function Ping(ByVal host As String) As Boolean
    '    Dim objPingHost As New clsPing
    '    Dim lngPingReply As Long

    '    '*
    '    '* Is there any host in the TextBox?
    '    '*
    '    'If (Trim(Me.txtHost.Text) = "") Then
    '    '    Call MsgBox("Please, type a host name or IP.", MsgBoxStyle.Exclamation, Application.ProductName)
    '    '    Exit Function
    '    'End If

    '    '*
    '    '* Prepares to Ping a Host
    '    '*
    '    objPingHost.TimeOut = 5000               ' --> 5000 msec  Time Out
    '    objPingHost.DataSize = 32                ' -->   32 bytes Package Size
    '    objPingHost.Host = host                  ' --> Host to Ping
    '    '*
    '    '* Writes in the Log
    '    '*
    '    ' Me.txtLog.Text = "Trying to ping " & Trim(Me.txtHost.Text) & " ..."

    '    '*
    '    '* Ping the Host and get the reply time
    '    '*
    '    lngPingReply = objPingHost.Ping()

    '    '*
    '    '* Check for error
    '    '*

    '    If (lngPingReply = clsPing.PING_ERROR) Then
    '        'Me.txtLog.Text &= Environment.NewLine & "Error : " & objPingHost.GetLastError.Description
    '        Return False
    '    Else
    '        'Me.txtLog.Text &= Environment.NewLine & "Ping Reply " & lngPingReply & " msec."
    '        Return True
    '    End If
    'End Function
End Class
