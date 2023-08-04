Imports System.Math
Imports System.Management

Public Class clsLicense

    Public Enum LicType
        Server
        Client
    End Enum

    Dim RDate As String = CLng((DateTime.Now.DayOfYear * 747) / 7)
    Dim Ascii As String = 65 + CLng(RDate.Substring(2, 1))
    Dim RStr As String = RDate.Replace(RDate.Substring(2, 1), Chr(Ascii))

    Private _ConnectionString As String
    Friend _DBName As String
    Friend _DBPass As String
    Private _SoftwareVersion As String
    Private _ServerCharacter As String
    Private _ClientCharacter As String

    Private _SerialNo As String
    Private _AppPath As String

    Private DBPath As String
    Private SysDBPath As String


    Private _RegisteredTo As String
    Private _LicenseType As LicType



    Private m_strMBoardID As String

    Private Const MAX_PATH = 260

    Private Declare Function GetVolumeInformation Lib "kernel32" Alias _
    "GetVolumeInformationA" (ByVal lpRootPathName As String, _
    ByVal lpVolumeNameBuffer As String, ByVal nVolumeNameSize As Integer, _
    ByRef lpVolumeSerialNumber As Integer, ByRef lpMaximumComponentLength As Integer, _
    ByRef lpFileSystemFlags As Integer, ByVal lpFileSystemNameBuffer As String, _
    ByVal nFileSystemNameSize As Integer) As Integer

    Private objDAL As New clsData.DataAccess

    Public Sub New()
        _ConnectionString = ""
        _SoftwareVersion = "1101"
        _ServerCharacter = "A"
        _ClientCharacter = "B"
        Try
            Dim objMB As New ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard")

            For Each objMgmt As ManagementObject In objMB.Get
                m_strMBoardID = objMgmt("SerialNumber").ToString().Trim
            Next
        Catch
            m_strMBoardID = String.Empty
        End Try
    End Sub

    Public Property ConnectionString() As String
        Get
            Return _ConnectionString
        End Get

        Set(ByVal Value As String)
            Dim arr
            _ConnectionString = Value

            arr = _ConnectionString.Substring(_ConnectionString.IndexOf("Password"), _
                  _ConnectionString.IndexOf(";", _ConnectionString.IndexOf("Password")) - _
                  _ConnectionString.IndexOf("Password")).Split("=")
            _DBPass = arr(1)

            arr = _ConnectionString.Substring(_ConnectionString.IndexOf("Source"), _
                  _ConnectionString.IndexOf(";", _ConnectionString.IndexOf("Source")) - _
                  _ConnectionString.IndexOf("Source")).Split("=")
            _DBName = arr(1)
        End Set
    End Property

    Public Property SystemDatabasePath() As String
        Get
            Return SysDBPath
        End Get

        Set(ByVal Value As String)
            SysDBPath = Value
        End Set
    End Property

    Private Function GetDecimalLeftDigit(ByVal Number As Integer) As Integer
        If Number.ToString.Length > 1 Then
            Return Number.ToString.Substring(0, 1)
        Else
            Return Number
        End If
        'Dim Digit As Int16
        'Do
        '    Digit = Number
        '    Number = Number / 10

        'Loop While Number >= 1

        'Return Digit
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

    Public Property ServerCharacter() As String
        Get
            Return _ServerCharacter
        End Get

        Set(ByVal Value As String)
            _ServerCharacter = Value
        End Set
    End Property

    Public Property ClientCharacter() As String
        Get
            Return _ClientCharacter
        End Get

        Set(ByVal Value As String)
            _ClientCharacter = Value
        End Set
    End Property

    Private ReadOnly Property MotherBoardID() As String
        Get
            MotherBoardID = m_strMBoardID
        End Get
    End Property

    Private ReadOnly Property HardwareID() As String
        Get
            If String.IsNullOrEmpty(m_strMBoardID) Then
                Return Me.HDSerial.Trim
            Else
                If m_strMBoardID.Trim.Length > 6 AndAlso m_strMBoardID.ToLower <> "none" AndAlso Not m_strMBoardID.ToLower.Contains("o.e.m.") AndAlso m_strMBoardID.ToUpper <> "OEM" Then
                    Return m_strMBoardID.Trim
                Else
                    Return Me.HDSerial.Trim
                End If
            End If
        End Get
    End Property

    Public Property RegisteredTo() As String
        Get
            Return _RegisteredTo
        End Get

        Set(ByVal Value As String)
            _RegisteredTo = Value
        End Set
    End Property

    Public ReadOnly Property LicenseType() As LicType
        Get
            Return _LicenseType
        End Get
    End Property

    Private Function HDSerial() As String
        Dim lngReturn As Long, lngDummy1 As Long, lngDummy2 As Long, lngSerial As Long
        Dim strDummy1 As String, strDummy2 As String, strSerial As String
        strDummy1 = Space(MAX_PATH)
        strDummy2 = Space(MAX_PATH)
        lngReturn = GetVolumeInformation("C:\", strDummy1, Len(strDummy1), lngSerial, lngDummy1, lngDummy2, strDummy2, Len(strDummy2))
        strSerial = Trim(Hex(lngSerial))
        HDSerial = Trim(strSerial)
    End Function

    Public Function IsDemoLic() As String
        Dim DataRead As IDataReader
        Dim qry As String


        'open security database to verify software license 
        objDAL.ConnectionString = _ConnectionString
        objDAL.Provider = clsData.EnumProviders.OLEDB
        Try
            objDAL.OpenConn()
            qry = "SELECT Lickey FROM LICINFO"

            DataRead = objDAL.ExecDataReader(qry, CommandType.Text)

            If DataRead.Read Then
                'if record is found, retrieve serialno and license key
                If DataRead("Lickey").ToString = DemoKey Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
            DataRead.Close()
            objDAL.CloseConn()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "System Message")
            Return False
        End Try

    End Function
    Public Function GetEdition() As String
        Dim DataRead As IDataReader
        Dim qry As String

        Dim SerialNo As String

        'open security database to verify software license 
        objDAL.ConnectionString = _ConnectionString
        objDAL.Provider = clsData.EnumProviders.OLEDB
        Try
            objDAL.OpenConn()
            qry = "SELECT SerialNo FROM LICINFO"

            DataRead = objDAL.ExecDataReader(qry, CommandType.Text)

            If DataRead.Read Then
                'if record is found, retrieve serialno and license key
                SerialNo = DataRead("SerialNo")
                Dim Editionchar = Mid(SerialNo, 6, 1)
                Select Case Editionchar
                    Case "I"
                        Return "Inventory"
                    Case "T"
                        Return "Tracking"
                    Case "F"
                        Return "Financial"
                    Case "E"
                        Return "Enterprise"
                    Case Else
                        Return ""
                End Select
            Else
                Return ""
            End If
            DataRead.Close()
            objDAL.CloseConn()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "System Message")
            Return ""
        End Try

        'if database is opened successfully, then verify license info
    End Function

    Public Function isRegisteredVersion() As String

        Dim DataRead As IDataReader
        Dim qry As String
        Dim frmRegister As New frmRegister

        '************* Variables to hold license info from local database ************

        Dim SerialNo As String
        Dim LicKey As String

        '*****************************************************************************                

        'show error message if hardware id is not ok
        If Me.HardwareID.Length < 6 Then
            MsgBox("Incompatible processor found to run this application. Please contact your vendor to " & _
                   "resolve this issue", vbCritical, "System Message")
            Return ""
            Exit Function
        End If
        'open security database to verify software license 
        objDAL.ConnectionString = _ConnectionString
        objDAL.Provider = clsData.EnumProviders.OLEDB
        Try
            objDAL.OpenConn()
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.Critical, "System Message")
            Return ""
            Exit Function
        End Try

        'if database is opened successfully, then verify license info
        qry = "SELECT SerialNo, LicKey, ServerPath, CompName FROM LICINFO"

        DataRead = objDAL.ExecDataReader(qry, CommandType.Text)

        Try
            If DataRead.Read() Then
                'if record is found, retrieve serialno, company name and license key
                _RegisteredTo = IIf(IsDBNull(DataRead("CompName")), "", DataRead("CompName"))
                SerialNo = DataRead("SerialNo").ToString
                LicKey = DataRead("LicKey").ToString

                'check if processor id exists in the database serial no
                If InStr(SerialNo, Me.HardwareID, CompareMethod.Text) <> 0 Then
                    'validate database serial no and license key
                    If ValidateKey(SerialNo, LicKey) Then
                        'if license type is client
                        If SerialType(SerialNo) = _ClientCharacter Then
                            _LicenseType = LicType.Client     'set license type to client
                            DBPath = DataRead("ServerPath")  'retrieve server db path
                            Me.SystemDatabasePath = DBPath
                            'check if server database path is valid
                            If PathName(DBPath) = "" Then
                                MsgBox("Server database could not be found at " & vbCrLf & _
                                        DBPath, MsgBoxStyle.Critical, "System Message")

                                DBPath = ""
                                DataRead.Close()
                                objDAL.CloseConn()
                                frmRegister.objClass = Me
                                frmRegister.HardwareID = Me.HardwareID
                                frmRegister.ShowDialog()
                            Else 'If PathName(SDbPath) = "" Then

                                objDAL.CloseConn()

                                'if server database found, then open it to validate server license
                                objDAL.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                                                          "Data Source=" & DBPath & _
                                                          ";Jet OLEDB:Database Password=" & _DBPass & ";"
                                objDAL.Provider = clsData.EnumProviders.OLEDB
                                Try
                                    objDAL.OpenConn()
                                Catch e As Exception
                                    MsgBox(e.Message, MsgBoxStyle.Critical, "System Message")
                                    Return ""
                                    Exit Function
                                End Try

                                DataRead = objDAL.ExecDataReader(qry, CommandType.Text)

                                'if there is any record, check it is server license
                                If DataRead.Read() Then
                                    'if record is found, retrieve serialno and license key
                                    SerialNo = DataRead("SerialNo")
                                    LicKey = DataRead("LicKey")

                                    'validate database serial no and license key
                                    If ValidateKey(SerialNo, LicKey) Then
                                        'license type must be server
                                        If SerialType(SerialNo) = _ServerCharacter Then

                                            'Everything is ok....proceed for login
                                            DataRead.Close()
                                            objDAL.CloseConn()
                                            isRegisteredVersion = RStr

                                        Else  'If SerialType(SerialNo) = "A" Then
                                            MsgBox("Server license could not be verified", _
                                                    MsgBoxStyle.Critical, "System Message")

                                            DataRead.Close()
                                            objDAL.CloseConn()
                                            frmRegister.objClass = Me
                                            frmRegister.HardwareID = Me.HardwareID
                                            frmRegister.ShowDialog()
                                        End If
                                    Else  'If vSec.ValidateKey(SerialNo, LicKey) Then
                                        MsgBox("Software registration could not be verified", _
                                                MsgBoxStyle.Critical, "System Message")

                                        DataRead.Close()
                                        objDAL.CloseConn()
                                        frmRegister.objClass = Me
                                        frmRegister.HardwareID = Me.HardwareID
                                        frmRegister.ShowDialog()
                                    End If
                                Else  'If DataRead.Read() Then
                                    MsgBox("No server registration found at " & _
                                           "following location" & vbCrLf & DBPath, _
                                            MsgBoxStyle.Critical, "System Message")

                                    DataRead.Close()
                                    objDAL.CloseConn()
                                    frmRegister.objClass = Me
                                    frmRegister.HardwareID = Me.HardwareID
                                    frmRegister.ShowDialog()
                                End If
                            End If
                        ElseIf SerialType(SerialNo) = _ServerCharacter Then  'If SerialType(SerialNo) = "B" Then
                            'No need of additional checks if license type is server
                            _LicenseType = LicType.Server
                            DBPath = DataRead("ServerPath")
                            Me.SystemDatabasePath = DBPath
                            DataRead.Close()
                            objDAL.CloseConn()
                            isRegisteredVersion = RStr
                        Else
                            MsgBox("Software registration could not be verified", _
                                    MsgBoxStyle.Critical, "System Message")

                            DataRead.Close()
                            objDAL.CloseConn()
                            frmRegister.objClass = Me
                            frmRegister.HardwareID = Me.HardwareID
                            frmRegister.ShowDialog()
                        End If
                    Else   'If vSec.ValidateKey(SerialNo, LicKey) Then
                        MsgBox("Software registration could not be verified", _
                            MsgBoxStyle.Critical, "System Message")

                        DataRead.Close()
                        objDAL.CloseConn()
                        frmRegister.objClass = Me
                        frmRegister.HardwareID = Me.HardwareID
                        frmRegister.ShowDialog()
                    End If

                Else 'If InStr(SerialNo, PID, CompareMethod.Text) <> 0 Then
                    MsgBox("Software registration could not be verified", _
                            MsgBoxStyle.Critical, "System Message")

                    DataRead.Close()
                    objDAL.CloseConn()
                    frmRegister.objClass = Me
                    frmRegister.HardwareID = Me.HardwareID
                    frmRegister.ShowDialog()
                End If
            Else     'If DataRead.Read() Then
                DataRead.Close()
                objDAL.CloseConn()
                frmRegister.objClass = Me
                frmRegister.HardwareID = Me.HardwareID
                frmRegister.ShowDialog()
            End If
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.Critical, "System Message")
        End Try
    End Function

    Private Function PathName(ByVal PName As String) As String
        Try
            PathName = Dir(PName)
        Catch ex As Exception
            PathName = ""
        End Try
    End Function

End Class
