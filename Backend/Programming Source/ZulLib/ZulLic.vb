Imports System.Math
Imports System.Management

Public Class ZulLic

    Public DemoKey As String = "1111-1111-1111-1111"
    Private _SoftwareVersion As String
    Private objMB As ManagementObjectSearcher
    Private m_strMBoardID As String

    Private Const MAX_PATH As Integer = 260

    Private Declare Function GetVolumeInformation Lib "kernel32" Alias _
    "GetVolumeInformationA" (ByVal lpRootPathName As String, _
    ByVal lpVolumeNameBuffer As String, ByVal nVolumeNameSize As Integer, _
    ByRef lpVolumeSerialNumber As Integer, ByRef lpMaximumComponentLength As Integer, _
    ByRef lpFileSystemFlags As Integer, ByVal lpFileSystemNameBuffer As String, _
    ByVal nFileSystemNameSize As Integer) As Integer
    Private Function GetDecimalLeftDigit(ByVal Number As Integer) As Integer
        If Number.ToString.Length > 1 Then
            Return Number.ToString.Substring(0, 1)
        Else
            Return Number
        End If
    End Function

    Public Property SoftwareVersion() As String
        Get
            Return _SoftwareVersion
        End Get

        Set(ByVal Value As String)
            Dim arr As String() = Value.Split(".")
            _SoftwareVersion = ""
            For i As Integer = 0 To arr.Length - 1
                _SoftwareVersion += GetDecimalLeftDigit(CInt(arr(i))).ToString
            Next
            If String.IsNullOrEmpty(_SoftwareVersion) Then
                _SoftwareVersion = "1000"
            End If
        End Set
    End Property

    Public Function GenSerial(ByVal LicType As String, ByVal LicEdition As String) As String
        Try
            SoftwareVersion = My.Application.Info.Version.ToString()
            GenSerial = SoftwareVersion & LicType & LicEdition & GetHardwareID()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Function GetHardwareID() As String
        Try

            objMB = New ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard")

            For Each objMgmt As ManagementObject In objMB.Get
                m_strMBoardID = objMgmt("SerialNumber").ToString().Trim
            Next

            'To be filled by O.E.M.
            If m_strMBoardID.Trim.Length > 6 And m_strMBoardID.ToLower <> "none" And m_strMBoardID.ToUpper <> "OEM" And Not m_strMBoardID.ToLower.Contains("o.e.m.") And Not String.IsNullOrEmpty(m_strMBoardID.Trim) Then
                Return m_strMBoardID.Trim
            Else
                Return GetHDSerial.Trim
            End If
        Catch ex As Exception
            Return GetHDSerial.Trim
        End Try
    End Function

    Private Function GetHDSerial() As String
        Dim lngReturn As Long, lngDummy1 As Long, lngDummy2 As Long, lngSerial As Long
        Dim strDummy1 As String, strDummy2 As String, strSerial As String
        strDummy1 = Space(MAX_PATH)
        strDummy2 = Space(MAX_PATH)
        lngReturn = GetVolumeInformation("C:\", strDummy1, Len(strDummy1), lngSerial, lngDummy1, lngDummy2, strDummy2, Len(strDummy2))
        strSerial = Trim(Hex(lngSerial))
        GetHDSerial = Trim(strSerial)
    End Function

    Public Function ValidateKey(ByVal AccNum As String, ByVal Key As String) As Boolean

        Dim Key2Validate(4) As Int32, StartPos As Int32
        Dim FinalKey As String
        Dim EnValue As String
        Dim NegKey As String       ' if key is -ive, put it in NegKey
        Dim arr As String()

        If Len(Trim(Key)) = 0 Then
            Return False
            Exit Function
        End If


        If Key = DemoKey Then
            Return True
        End If

        Try
            arr = Split(Key, "-")
            arr(3) = Replace(arr(3), "*", "-")

            EnValue = Encrypt("ABTAK56", Mid$(AccNum, 5), 1)
            StartPos = CInt(Right$(CStr(arr(3) + 197), 2))

            Key2Validate(0) = Fix(CLng("&H" & Left$(EnValue, 5)) / 121)
            Key2Validate(1) = Fix(CLng("&H" & Mid$(EnValue, 6, 5)) / 123)
            Key2Validate(2) = Fix(CLng("&H" & Mid$(EnValue, StartPos, 5)) / 127)
            Key2Validate(3) = CLng("&H" & Right$(EnValue, 2))
            Key2Validate(3) = CLng(CStr(Key2Validate(3)) & _
                              SetStr(CStr(StartPos), 2, "0", "L")) - 197

            If Key2Validate(3) < 0 Then
                NegKey = "*" & Abs(Key2Validate(3))
            Else
                NegKey = Abs(Key2Validate(3))
            End If

            FinalKey = Abs(Key2Validate(0)) & "-" & Abs(Key2Validate(1)) & _
                              "-" & Abs(Key2Validate(2)) & "-" & NegKey
            If Key = FinalKey Then
                Return True
            Else
                Return False
            End If
        Catch e As Exception
            Return False
        End Try
    End Function
    Private Function Encrypt(ByVal CodeKey As String, ByVal DataIn As String, ByVal EncLevel As Integer) As String

        Dim StartPos As Long
        Dim lonDataPtr As Long
        Dim strDataOut As String = ""
        Dim temp As Integer
        Dim tempstring As String
        Dim intXOrValue1 As Integer
        Dim intXOrValue2 As Integer
        Dim Keys(4) As Long
        Dim k As String = ""

        Randomize()

        Try
            For lonDataPtr = 1 To Len(DataIn)
                'The first value to be XOr-ed comes from the data to be encrypted
                intXOrValue1 = Asc(Mid$(DataIn, lonDataPtr, 1))
                'The second value comes from the code key
                intXOrValue2 = Asc(Mid$(CodeKey, ((lonDataPtr Mod Len(CodeKey)) + 1), 1))

                temp = (intXOrValue1 Xor intXOrValue2)
                tempstring = Hex(temp)
                If Len(tempstring) = 1 Then tempstring = "0" & tempstring

                strDataOut = strDataOut + tempstring
            Next lonDataPtr
            Encrypt = strDataOut
            If EncLevel = 1 Then Exit Function

            Keys(0) = Fix(CLng("&H" & Left$(strDataOut, 5)) / 121)
            Keys(1) = Fix(CLng("&H" & Mid$(strDataOut, 6, 5)) / 123)


            '***********************************

            StartPos = ((Len(strDataOut) - 6) * Rnd()) + 1
            'MsgBox Mid$(KeyIs.Text, StartPos, 6)

            'StartPos = 7


            Keys(2) = Fix(CLng("&H" & Mid$(strDataOut, StartPos, 5)) / 127)
            Keys(3) = CLng("&H" & Right$(strDataOut, 2))
            Keys(3) = CLng(CStr(Keys(3)) & SetStr(CStr(StartPos), 2, "0", "L")) - 197

            If Keys(3) < 0 Then
                k = "*" & Abs(Keys(3))
            Else
                k = Abs(Keys(3))
            End If
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.Critical, "System Message")
        End Try

        Encrypt = Abs(Keys(0)) & "-" & Abs(Keys(1)) & "-" & Abs(Keys(2)) & "-" & k
        '***********************************
    End Function

    Private Function SetStr(ByVal str As String, ByVal StrLen As Integer, _
                            ByVal PadChr As String, ByVal PadLoc As String) As String

        Dim i As Integer
        Dim ActualLen As Integer
        Dim AddSpace As Integer

        ActualLen = Len(str)

        If ActualLen >= StrLen Then
            str = Mid$(str, 1, StrLen)
        Else
            AddSpace = StrLen - ActualLen
            For i = 1 To AddSpace
                If PadLoc = "R" Then
                    str = str & PadChr
                Else
                    str = PadChr & str
                End If
            Next i
        End If

        SetStr = str
    End Function

    Public Function SerialType(ByVal SNo As String) As String
        SerialType = Mid(SNo, 5, 1)
    End Function

    Public Function isRegisteredVersion(ByVal SerialNo As String, ByVal LicKey As String) As Boolean
        'check if processor id exists in the database serial no
        If InStr(SerialNo, GetHardwareID, CompareMethod.Text) <> 0 Then
            'validate database serial no and license key
            If ValidateKey(SerialNo, LicKey) Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Public Function IsDemoLic(ByVal LicKey As String) As Boolean
        If LicKey = DemoKey Then
            Return True
        Else
            Return False
        End If
    End Function

End Class
