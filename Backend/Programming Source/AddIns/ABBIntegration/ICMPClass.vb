Option Strict On
Imports System.Net
Imports System.Net.Sockets
Imports System.Runtime.InteropServices

Public Structure ICMPReplyStructure
    Public ReplyFrom As IPAddress
    Public RoundTripTime As Integer
    Public Data As String
    Public Status As ICMPClass.ICMPStatusEnum
    Public Message As String
    Public TTL As Byte
End Structure

Public Class ICMPClass
    'Enumerated list of icmp echo reply response codes

    Public Enum ICMPStatusEnum
        Success = 0
        BufferTooSmall = 11001 'Buffer Too Small 
        DestinationNetUnreachable = 11002 'Destination Net Unreachable 
        DestinationHostUnreachable = 11003 'Destination Host Unreachable 
        DestinationProtocolUnreachable = 11004 'Destination Protocol Unreachable 
        DestinationPortUnreachable = 11005 'Destination Port Unreachable 
        NoResource = 11006 'No Resources 
        BadOption = 11007 'Bad Option 
        HardwareError = 11008 'Hardware Error 
        LargePacket = 11009 'Packet Too Big 
        RequestTimedOUT = 11010 'Request Timed Out 
        BadRequest = 11011 'Bad Request 
        BadROUTE = 11012 'Bad Route 
        TtlExpiredInTransit = 11013 'TimeToLive Expired Transit 
        TtlExpiredInReassembly = 11014 'TimeToLive Expired Reassembly 
        Parameter = 11015 'Parameter Problem 
        SourceQuench = 11016 'Source Quench 
        OptionTooBig = 11017 'Option Too Big 
        BadDestination = 11018 'Bad Destination 
        NegotiatingIPSEC = 11032 'Negotiating IPSEC 
        GeneralFailure = 11050 'General Failure 
    End Enum

    ''User Friendly ping command that acceptsa host as either an ip addressor a resolvable name. 
    '//     'Usage:
    ''ex 1: console.WriteLine Ping("192.168.0
    '//     .1").RoundTripTime
    '//     'ex 2: dim reply as ICMPReplyStructure
    '//     ' reply = Ping("192.168.0.1", 1000,16)
    '' console.WriteLine reply.ReplyFrom.ToSt
    '//     ring & " replied in " & reply.RoundTripT
    '//     ime & _
    ''"ms. status:" & reply.status & ":" & re
    '//     ply.message & ". TTL=" & reply.TTL

    Public Function Ping(ByVal Host As String, Optional ByVal Timeout As Integer = 2000, Optional ByVal TTL As Byte = 32, Optional ByVal DataSize As Long = 32) As ICMPReplyStructure
        Dim IP As IPAddress
        Dim opt As ICMPOptions
        Try
            IP = Dns.GetHostEntry(Host).AddressList(0)
        Catch ex As Exception
            Dim r As ICMPReplyStructure
            r.Message = ex.ToString
            r.ReplyFrom = New IPAddress(0)
            r.Status = ICMPStatusEnum.GeneralFailure
            r.TTL = 255
            Return r
        End Try
        opt.Timeout = Timeout
        opt.TimeToLive = TTL
        opt.DatSize = DataSize
        Return Ping(IP, opt)
    End Function


    Public Function Ping(ByVal IP As IPAddress, ByVal opt As ICMPOptions) As ICMPReplyStructure
        Dim ICMPHandle As IntPtr
        Dim iIP As Int32
        Dim sData As String
        Dim oICMPOptions As New ICMPClass.ICMP_OPTIONS
        Dim ICMPReply As ICMP_ECHO_REPLY
        Dim iReplies As Int32
        ICMPHandle = IcmpCreateFile
        iIP = System.BitConverter.ToInt32(IP.GetAddressBytes, 0)
        sData = "X"
        oICMPOptions.Ttl = opt.TimeToLive
        iReplies = IcmpSendEcho(ICMPHandle, iIP, sData, sData.Length, oICMPOptions, ICMPReply, Marshal.SizeOf(ICMPReply), opt.Timeout)
        Dim reply As ICMPReplyStructure
        reply = FillReplyStructure(ICMPReply)
        Return reply
    End Function
    'return a people friendly message from the status code

    Public Function GetMessage(ByVal status As ICMPStatusEnum) As String
        Select Case status
            Case ICMPStatusEnum.Success
                Return "Device Found"
            Case ICMPStatusEnum.BufferTooSmall
                Return "Buffer Too Small"
            Case ICMPStatusEnum.DestinationNetUnreachable
                Return "Destination Net Unreachable"
            Case ICMPStatusEnum.DestinationHostUnreachable
                Return "Destination Host Unreachable"
            Case ICMPStatusEnum.DestinationProtocolUnreachable
                Return "Destination Protocol Unreachable"
            Case ICMPStatusEnum.DestinationPortUnreachable
                Return "Destination Port Unreachable"
            Case ICMPStatusEnum.NoResource
                Return "No Resources"
            Case ICMPStatusEnum.BadOption
                Return "Bad Option"
            Case ICMPStatusEnum.HardwareError
                Return "Hardware Error"
            Case ICMPStatusEnum.LargePacket
                Return "Packet Too Big"
            Case ICMPStatusEnum.RequestTimedOUT
                Return "Request Timed Out"
            Case ICMPStatusEnum.BadRequest
                Return "Bad Request"
            Case ICMPStatusEnum.BadROUTE
                Return "Bad Route"
            Case ICMPStatusEnum.TtlExpiredInTransit
                Return "TimeToLive Expired Transit"
            Case ICMPStatusEnum.TtlExpiredInReassembly
                Return "TimeToLive Expired Reassembly"
            Case ICMPStatusEnum.Parameter
                Return "Parameter Problem"
            Case ICMPStatusEnum.SourceQuench
                Return "Source Quench"
            Case ICMPStatusEnum.OptionTooBig
                Return "Option Too Big"
            Case ICMPStatusEnum.BadDestination
                Return "Bad Destination"
            Case ICMPStatusEnum.NegotiatingIPSEC
                Return "Negotiating IPSEC"
            Case ICMPStatusEnum.GeneralFailure
                Return "General Failure"
            Case Else
                Return "Unknown Error"
        End Select
    End Function
    'fill the reply structure which will be passed back to the user with data from the ICMP_ECHO_REPLY structure.

    Private Function FillReplyStructure(ByVal IER As ICMP_ECHO_REPLY) As ICMPReplyStructure
        Dim irs As ICMPReplyStructure
        irs.RoundTripTime = IER.RoundTripTime
        irs.Message = GetMessage(IER.Status)
        irs.ReplyFrom = New IPAddress(IER.Address)
        irs.Data = IER.Data
        irs.Status = IER.Status
        irs.TTL = IER.Options.Ttl
        Return irs
    End Function

    'API functions contained in ICMP.DLL these functions perform the actual pinging

    Private Declare Auto Function IcmpCreateFile Lib "ICMP.DLL" () As IntPtr
    Private Declare Auto Function IcmpSendEcho Lib "ICMP.DLL" (ByVal IcmpHandle As IntPtr, _
    ByVal DestinationAddress As Integer, _
    ByVal RequestData As String, ByVal RequestSize As Integer, _
    ByRef RequestOptions As ICMP_OPTIONS, ByRef ReplyBuffer As ICMP_ECHO_REPLY, _
    ByVal ReplySize As Integer, ByVal Timeout As Integer) As Integer

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
    Public Structure ICMP_OPTIONS
        Public Ttl As Byte
        Public Tos As Byte
        Public Flags As Byte
        Public OptionsSize As Byte
        Public OptionsData As IntPtr
    End Structure

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
    Public Structure ICMP_ECHO_REPLY
        Public Address As UInteger
        Public Status As ICMPStatusEnum
        Public RoundTripTime As Integer
        Public DataSize As Short
        Public Reserved As Short
        Public DataPtr As IntPtr
        Public Options As ICMP_OPTIONS
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=250)> _
        Public Data As String
    End Structure

    Public Structure ICMPReplyStructure
        Public ReplyFrom As IPAddress
        Public RoundTripTime As Integer
        Public Data As String
        Public Status As ICMPStatusEnum
        Public Message As String
        Public TTL As Byte
    End Structure

    Public Structure ICMPOptions
        Public Timeout As Integer
        Public TimeToLive As Byte
        Public DatSize As Long
    End Structure
End Class
