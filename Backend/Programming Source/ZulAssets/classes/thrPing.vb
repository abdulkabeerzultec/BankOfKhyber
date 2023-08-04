Imports System.Threading

Public Class thrPing
    Private IpAddress As String
    Private FtpTh As Thread
    Dim row As Integer
    Dim frmFtp As FtpAccessInterface
    'Dim Sourcecount As Integer

    Public Sub New(ByVal ip As String, ByVal callingForm As FtpAccessInterface, ByVal currentRow As Integer)
        IpAddress = ip
        frmFtp = callingForm
        row = currentRow
        FtpTh = New Thread(AddressOf Initiate)
        FtpTh.Start()
    End Sub

    Sub Initiate()
        Dim icmpObj As New ICMPClass()
        frmFtp.ErrorHandle(icmpObj.Ping(IpAddress).Message, row)
    End Sub
End Class
'Public Class thrPing
'    Dim IpAddress As String
'    Dim Sourcecount As Integer
'    Public Sub StartThread()
'        Dim FtpTh As New System.Threading.Thread(AddressOf Initiate)
'        FtpTh.Start()
'    End Sub
'    Public Sub SetSource(ByVal Src As String)
'        IpAddress = Src
'    End Sub
'    Sub Initiate()
'        If Ping(IpAddress) Then
'            MessageBox.Show(IpAddress + "Found")
'        Else
'            MessageBox.Show(IpAddress + " Not Found")

'        End If
'    End Sub
'    Function Ping(ByVal host As String) As Boolean
'        Dim objPingHost As New clsPing
'        Dim lngPingReply As Long

'        objPingHost.TimeOut = 5000               ' --> 5000 msec  Time Out
'        objPingHost.DataSize = 32                ' -->   32 bytes Package Size
'        objPingHost.Host = host                  ' --> Host to Ping

'        lngPingReply = objPingHost.Ping()


'        If (lngPingReply = clsPing.PING_ERROR) Then

'            Return False
'        Else
'            Return True
'        End If
'    End Function
'End Class