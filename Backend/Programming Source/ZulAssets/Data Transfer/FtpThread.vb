Imports Xceed.Ftp
Imports System.Net
Imports PingLib.Icmp
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class FtpThread
    Dim Ip As String
    Dim IPArray() As IPAddress
    Dim Row As Integer
    Dim ftp As FtpClient
    Dim Source() As String
    Dim Sourcecount As Integer
    Dim Destination As String

    Dim frmFtp As FtpAccessInterface

    Dim tempFolder As String = AppConfig.AppDataFolder & "\Temp"
    Dim tempFolderFS As String = AppConfig.AppDataFolder & "\FWatch"

    Dim FolderDv As String = "\My Documents\ZulAssets"
    Dim tmpFolderFWDv As String = "\My Documents\ZulAssets\FWatch"
    Dim tmpFolderDv As String = "\My Documents\ZulAssets\Temp"
    Friend isConfig As Boolean = False

    Public Sub SetForm(ByVal f As FtpAccessInterface)
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

    'isconfig = true when it is called from config button
    Sub Initiate()
        Xceed.Ftp.Licenser.LicenseKey = "FTN10-K8FS0-ARMXE-MGGA"
        ftp = New FtpClient
        frmFtp.ErrorHandle("Searching for " + Ip, Row)

        Dim pingClass As New ICMPClass()

        If pingClass.Ping(Ip).Status = ICMPClass.ICMPStatusEnum.Success Then
            System.Threading.Thread.Sleep(50)
            Try
                ftp.Connect(Ip)
                ftp.Login()

                frmFtp.UpdateUIHandler(Row, 1, Source(0))

                Try
                    ftp.CreateFolder(FolderDv)
                Catch exf As Exception
                End Try

                Try
                    ftp.CreateFolder(tmpFolderDv)
                Catch exf As Exception
                End Try

                Try
                    ftp.CreateFolder(tmpFolderFWDv)
                Catch exf As Exception
                End Try

                Dim curDeviceID As String = Trim(frmFtp.grdDevicesI.GetRowCellValue(Row, "DeviceID"))

                Try
                    frmFtp.UpdateUIHandler(Row, 6, Nothing)
                    ftp.SendFile(tempFolder & "\" & curDeviceID & ".txt", tmpFolderDv & "\DeviceID.txt")
                    System.IO.File.Delete(tempFolder & "\" & curDeviceID & ".txt")
                Catch ex As Exception
                    frmFtp.UpdateUIHandler(Row, 2, Nothing)
                    ftp.Disconnect()
                    Exit Sub
                End Try

                If isConfig = True Then
                    frmFtp.UpdateUIHandler(Row, 4, Nothing)
                    ftp.Disconnect()
                    Exit Sub
                End If

                Dim status As String = "0"
                Try
                    ftp.ReceiveFile(tmpFolderDv & "\" & curDeviceID & ".st", tempFolder & "\" & curDeviceID & ".st")
                    status = frmFtp.ReadFile(tempFolder & "\" & curDeviceID & ".st", False)
                    System.IO.File.Delete(tempFolder & "\" & curDeviceID & ".st")
                Catch e As System.IO.IOException
                  frmFtp.UpdateUIHandler(Row, 2, Nothing)
                    ftp.Disconnect()
                    Exit Sub
                Catch ex As Exception
                   frmFtp.UpdateUIHandler(Row, 2, Nothing)
                    ftp.Disconnect()
                    Exit Sub
                End Try

                If status = "1" Then
                    ftp.SendFile(tempFolder & "\" & curDeviceID & ".cmd", tmpFolderFWDv & "\" & curDeviceID & ".cmd")
                Else
                    frmFtp.UpdateUIHandler(Row, 3, Nothing)
                    ftp.Disconnect()
                    Exit Sub
                End If
                frmFtp.UpdateUIHandler(Row, 0, Nothing)
                ftp.Disconnect()
            Catch ex As Exception
                frmFtp.ErrorHandle("Anonymous Access Denied", Me.Row)
            End Try
        Else
            frmFtp.ErrorHandle("Device does not Exists", Row)
        End If

    End Sub
End Class
