Imports System
Imports System.Data.OleDb
Imports System.Configuration.ConfigurationSettings
Imports System.Windows.Forms
Imports System.Text
Namespace ZulAssetsDAL
    Public Class AppConfig
        Public Enum ApplicationRegistration
            Inventory
            Tracking
            Financial
            Enterprise
            NotRegistered
            Demo
        End Enum

        Private Shared dsAppDatabases As New DataSet
        Public Shared ISRegistered As Boolean = False
        Public Shared AppEdition As ApplicationRegistration = ApplicationRegistration.NotRegistered

        Private Shared _IsDemokey As Boolean = False
        Private Shared _ServerCharacter As String
        Private Shared _ClientCharacter As String
        Private Shared _SelectedEdition As String
        Private Shared _DatabaseConfigFilePath As String
        Private Shared _DBVersion As String

        Private Shared _AppDataFolder As String
        Public Shared Property AppDataFolder() As String
            Get
                Return _AppDataFolder
            End Get
            Set(ByVal value As String)
                _AppDataFolder = value
            End Set
        End Property


        Public Shared Property DBVersion() As String
            Get
                Return _DBVersion
            End Get
            Set(ByVal value As String)
                _DBVersion = value
            End Set
        End Property


        Public Shared Property DatabaseConfigFilePath() As String
            Get
                Return _DatabaseConfigFilePath
            End Get
            Set(ByVal value As String)
                _DatabaseConfigFilePath = value
            End Set
        End Property

        Private Shared _CompanyEmail As String
        Public Shared Property CompanyEmail() As String
            Get
                Return _CompanyEmail
            End Get
            Set(ByVal value As String)
                _CompanyEmail = value
            End Set
        End Property

        Private Shared _CompanyName As String
        Public Shared Property CompanyName() As String
            Get
                Return _CompanyName
            End Get
            Set(ByVal value As String)
                _CompanyName = value
            End Set
        End Property


        Public Shared Property IsDemoKey() As Boolean
            Get
                Return _IsDemokey
            End Get
            Set(ByVal value As Boolean)
                _IsDemokey = value
                If _IsDemokey Then
                    AppEdition = ApplicationRegistration.Demo
                End If
            End Set
        End Property

        Public Shared Property SelectedEdition() As String
            Get
                Return _SelectedEdition
            End Get

            Set(ByVal Value As String)
                _SelectedEdition = Value
            End Set
        End Property

        Public Shared Property ServerCharacter() As String
            Get
                Return _ServerCharacter
            End Get

            Set(ByVal Value As String)
                _ServerCharacter = Value
            End Set
        End Property

        Public Shared Property ClientCharacter() As String
            Get
                Return _ClientCharacter
            End Get

            Set(ByVal Value As String)
                _ClientCharacter = Value
            End Set
        End Property

        Private Shared _TransMethod As Integer
        Public Shared Property TransMethod() As Integer
            Get
                Return _TransMethod
            End Get
            Set(ByVal value As Integer)
                _TransMethod = value
            End Set
        End Property

        Public Shared CompanyIDS As String = ""
        Public Shared firstLogin As Boolean = True

        Public Shared DbServer As String = ""
        Public Shared DbUname As String = ""
        Public Shared DbName As String = ""
        Public Shared DbPass As String = ""
        Public Shared DBSQLPort As String = ""

        Public Shared ComServer As String = ""
        Public Shared ComUname As String = ""
        Public Shared ComPass As String = ""
        Public Shared ComName As String = ""
        Public Shared CommPort As String = ""

        Public Shared ExpServer As String = ""
        Public Shared ExpUname As String = ""
        Public Shared ExpPass As String = ""
        Public Shared ExpName As String = ""
        Public Shared ExpPort As String = ""

        Public Shared SysPass As String = ""
        Public Shared DepDate As String = ""
        Public Shared DepRunType As String = "0"
        Public Shared LoginName As String = ""
        Public Shared UserName As String = ""
        Public Shared DbType As String = ""
        Public Shared htSysMsg As New Hashtable
        Public Shared DeletePermt As Integer
        Public Shared ReportPrinter As String = ""
        Public Shared LabelPrinter As String = ""
        Public Shared CodingMode As Boolean = True
        Public Shared ExportToServer As Boolean = False
        Public Shared MaindateFormat As String = ""
        Public Shared IsOfflineMachine As Boolean = False
        Public Shared ShowAlarmOnStartup As Boolean = False
        Public Shared AlarmBeforeDays As Integer = 0


        Public Shared DescForRpt As String = ""
        Public Shared ImgStorgeLoc As String = ""
        Public Shared ImgType As String = ""
        Public Shared ImgPath As String = ""


        Shared _ConnectionString As String
        Shared _ConnectionStringComm As String
        Shared Conn As OleDbConnection
        Shared conn1, connComm As OleDbConnection

        Private Shared strQuery As New StringBuilder

        Public Shared Sub Set_User(ByVal LName As String, ByVal UName As String)
            LoginName = LName
            UserName = UName
        End Sub

        Public Shared Function testConnection(ByVal DbServer As String, ByVal Dbname As String, ByVal DbUname As String, ByVal DbPass As String, ByVal SQLPort As String, ByVal DbType As String, ByVal txtComUname As String, ByVal txtComPass As String, ByVal txtComSerPort As String, ByVal txtCommServ As String, ByVal DbComname As String) As Boolean
            If DbType = "2" Then


                'DbServer = "(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = IMRANLAP)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = orclpdb) ))"




                _ConnectionString = "Provider=msdaora;Data Source=" & DbServer & ";User Id=" & DbUname & ";Password=" & DbPass & ";"

            Else
                '_ConnectionString = "Provider=SQLNCLI11;DATA SOURCE=" & DbServer & ";UID=" & DbUname & ";PWD=" & DbPass & ";DATABASE=" & Dbname & ""
                _ConnectionString = "Provider=SQLNCLI11;DATA SOURCE=" & DbServer & "," & Val(SQLPort) & ";UID=" & DbUname & ";PWD=" & DbPass & ";DATABASE=" & Dbname & "" & ";Connect Timeout=3"
            End If

            '_ConnectionStringComm = "Provider=SQLNCLI11;DATA SOURCE=" & txtCommServ & ";UID=" & txtComUname & ";PWD=" & txtComPass & ";DATABASE=" & DbComname & ""
            _ConnectionStringComm = "Provider=SQLNCLI11;DATA SOURCE=" & txtCommServ & "," & Val(txtComSerPort) & ";UID=" & txtComUname & ";PWD=" & txtComPass & ";DATABASE=" & DbComname & "" & ";Connect Timeout=3"
            'Shared Conn As OleDbConnection
            conn1 = New OleDbConnection(_ConnectionString)
            connComm = New OleDbConnection(_ConnectionStringComm)

            Try
                conn1.Open()
                conn1.Close()
                conn1.Dispose()

                connComm.Open()
                connComm.Close()
                connComm.Dispose()

                Return True
            Catch ex As Exception
                MessageBox.Show("Connecting to database failed, please check the connection parameters and check again." & Chr(13) & ex.Message, "ZulAsset", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try

            Return True
        End Function



        Public Shared Function Insert_Settings(ByVal strdt As String, ByVal DepRunType As Int16, ByVal DeletePermt1 As Int16, ByVal ExportToServ As Boolean, ByVal codemod As Boolean, ByVal dtformat As String, ByVal DescForRpt As String, ByVal ImgStorgeLoc As String, ByVal ImgType As String, ByVal ImgPath As String, ByVal ShowAlarmOnStartup As Boolean, ByVal AlarmBeforeDays As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into SysSettings")
            'strQuery.Append(" (FYDate,DepRunType,DeletePermt,ReportPrinter,LabelPrinter,ExportToServ,CodingMode,dtFormat,DescForRpt,ImgStorgeLoc,ImgType,ImgPath )")
            strQuery.Append(" (FYDate,DepRunType,DeletePermt,ExportToServ,CodingMode,dtFormat,DescForRpt,ImgStorgeLoc,ImgType,ImgPath,ShowAlarmOnStartup,Alarmbeforedays )")
            strQuery.Append(" Values")
            'strQuery.Append(" ('" & strdt & "'," & DepRunType & " ," & DeletePermt1 & ",'" & ReportPrinter & "','" & LabelPrinter & "'," & ExportToServer & " ," & codemod & " ,'" & dtformat & "','" & DescForRpt & "','" & ImgStorgeLoc & "','" & ImgType & "','" & ImgPath & "')")
            strQuery.Append(" ('" & strdt & "'," & DepRunType & " ," & DeletePermt1 & "," & ExportToServer & " ," & codemod & " ,'" & dtformat & "','" & DescForRpt & "','" & ImgStorgeLoc & "','" & ImgType & "','" & ImgPath & "'," & ShowAlarmOnStartup & "," & AlarmBeforeDays & ")")
            Dim str As String = strQuery.ToString()
            str = str.Replace("True", "1")
            str = str.Replace("False", "0")
            objCommand.CommandText = str
            Return objCommand
        End Function

        Public Shared Function Update_Settings(ByVal strdt As String, ByVal DepRunType As Int16, ByVal DeletePermt1 As Int16, ByVal ExportToServ As Boolean, ByVal codemod As Boolean, ByVal dtformat As String, ByVal DescForRpt As String, ByVal ImgStorgeLoc As String, ByVal ImgType As String, ByVal ImgPath As String, ByVal ShowAlarmOnStartup As Boolean, ByVal AlarmBeforeDays As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update SysSettings")
            strQuery.Append(" set FYDate = '" & strdt & "'")
            strQuery.Append(" ,DeletePermt = " & DeletePermt1 & "")
            'strQuery.Append(" ,ReportPrinter = '" & strReportPrinter & "'")
            'strQuery.Append(" ,LabelPrinter = '" & strLabelReport & "'")
            strQuery.Append(" ,ExportToServ = " & ExportToServ & "")
            strQuery.Append(" ,CodingMode = " & codemod & "")
            strQuery.Append(" ,dtFormat = '" & dtformat & "'")
            strQuery.Append(" ,DescForRpt = '" & DescForRpt & "'")
            strQuery.Append(" ,ImgStorgeLoc = '" & ImgStorgeLoc & "'")
            strQuery.Append(" ,ImgType = '" & ImgType & "'")
            strQuery.Append(" ,ImgPath = '" & ImgPath & "'")
            strQuery.Append(" ,DepRunType = " & DepRunType) '& " where ID = 1")
            strQuery.Append(" ,ShowAlarmOnStartup = " & ShowAlarmOnStartup & "")
            strQuery.Append(" ,AlarmBeforeDays = " & AlarmBeforeDays & "")
            Dim str As String = strQuery.ToString()
            str = str.Replace("True", "1")
            str = str.Replace("False", "0")
            objCommand.CommandText = str
            Return objCommand
        End Function

        Public Shared Function Get_Settings() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select FYDate,DepRunType ,DeletePermt,ExportToServ,CodingMode,dtformat,DescForRpt,ImgStorgeLoc,ImgType,ImgPath,IsOfflineMachine,ShowAlarmOnStartup,Alarmbeforedays from SysSettings")
            'strQuery.Append("Select FYDate,DepRunType ,DeletePermt,ReportPrinter,LabelPrinter,ExportToServ,CodingMode,dtformat,DescForRpt,ImgStorgeLoc,ImgType,ImgPath from SysSettings")
            objCommand.CommandText = strQuery.ToString()
            Return objCommand
        End Function


        Public Shared Function Get_DatabaseVersion() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If DbType = 1 Then
                strQuery.Append("Select [Version] from [DB Version]")
            Else
                strQuery.Append("Select Version from DBVersion")
            End If
            objCommand.CommandText = strQuery.ToString()
            Return objCommand
        End Function

#Region "database Server Config"


        'Public Shared Sub Update_TransMethod(ByVal TMethod As String)
        '    Try
        '        Dim tcmd As New OleDbCommand
        '        strQuery = New StringBuilder
        '        strQuery.Append("update dbConfig")
        '        strQuery.Append(" set TransMethod = " & TransMethod)
        '        tcmd.CommandText = strQuery.ToString
        '        tcmd.Connection = GetConnection()
        '        tcmd.ExecuteNonQuery()
        '        TransMethod = TMethod
        '        'Get_DbConfig_New()
        '    Catch ex As Exception

        '    End Try
        'End Sub

        Public Shared Sub Set_dbConfig(ByVal DbServer1 As String, ByVal Dbname1 As String, ByVal DbUname1 As String, ByVal DbPass1 As String, ByVal SysPass1 As String, ByVal SQLPort1 As String, ByVal DbType1 As String, ByVal txtComUname1 As String, ByVal txtComPass1 As String, ByVal txtComSerPort1 As String, ByVal txtCommServ1 As String, ByVal DbComname1 As String, ByVal txtExpUname1 As String, ByVal txtExpPass1 As String, ByVal txtExpSerPort1 As String, ByVal txtExpServ1 As String, ByVal DbExpName1 As String)

            Try

                If dsAppDatabases.Tables("Database").Rows.Count > 0 Then
                    Dim Removerow As DataRow = dsAppDatabases.Tables("Database").Rows(0)
                    dsAppDatabases.Tables("Database").Rows.Remove(Removerow)
                End If

                Dim row As DataRow = dsAppDatabases.Tables("Database").NewRow

                row("DbPass") = psEncrypt(DbPass1)
                row("DbServer") = DbServer1
                row("DbUname") = DbUname1
                row("SysPass") = psEncrypt(SysPass1)
                row("DBSQLPort") = SQLPort1
                row("DbName") = Dbname1
                row("DbType") = DbType1

                row("ComServer") = txtCommServ1
                row("ComUname") = txtComUname1
                row("ComPass") = psEncrypt(txtComPass1)
                row("CommPort") = txtComSerPort1
                row("ComName") = DbComname1


                row("ExpServer") = txtExpServ1
                row("ExpUname") = txtExpUname1
                row("ExpPass") = psEncrypt(txtExpPass1)
                row("ExpPort") = txtExpSerPort1
                row("ExpName") = DbExpName1

                row("ReportPrinter") = ReportPrinter
                row("LabelPrinter") = LabelPrinter

                'dsAppDatabases.Tables("Database").Rows.Add(row)
                dsAppDatabases.Tables("Database").Rows.InsertAt(row, 0)
                dsAppDatabases.WriteXml(DatabaseConfigFilePath, XmlWriteMode.WriteSchema)
                LoadDatabases()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "ZulAsset", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Shared Sub LoadDatabases()
            Try

                dsAppDatabases.Clear()
                dsAppDatabases.ReadXml(AppConfig.DatabaseConfigFilePath)
                Dim dt As DataTable = dsAppDatabases.Tables("Database")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    AppConfig.DbPass = psDecrypt(dt.Rows(0)("DbPass").ToString)
                    AppConfig.DbServer = dt.Rows(0)("DbServer").ToString
                    AppConfig.DbUname = dt.Rows(0)("DbUname").ToString
                    AppConfig.SysPass = psDecrypt(dt.Rows(0)("SysPass").ToString)
                    AppConfig.DBSQLPort = dt.Rows(0)("DBSQLPort").ToString
                    AppConfig.DbName = dt.Rows(0)("DbName").ToString
                    AppConfig.DbType = dt.Rows(0)("DbType").ToString

                    AppConfig.ComServer = dt.Rows(0)("ComServer").ToString
                    AppConfig.ComUname = dt.Rows(0)("ComUname").ToString
                    AppConfig.ComPass = psDecrypt(dt.Rows(0)("ComPass").ToString)
                    AppConfig.CommPort = dt.Rows(0)("CommPort").ToString
                    AppConfig.ComName = dt.Rows(0)("ComName").ToString


                    AppConfig.ExpServer = dt.Rows(0)("ExpServer").ToString
                    AppConfig.ExpUname = dt.Rows(0)("ExpUname").ToString
                    AppConfig.ExpPass = psDecrypt(dt.Rows(0)("ExpPass").ToString)
                    AppConfig.ExpPort = dt.Rows(0)("ExpPort").ToString
                    AppConfig.ExpName = dt.Rows(0)("ExpName").ToString

                    AppConfig.ReportPrinter = dt.Rows(0)("ReportPrinter")
                    AppConfig.LabelPrinter = dt.Rows(0)("LabelPrinter")
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Public Shared Sub Update_PrinterSettings(ByVal strReportPrinter As String, ByVal strLabelReport As String)
            Try
                If dsAppDatabases.Tables("Database").Rows.Count > 0 Then
                    Dim Removerow As DataRow = dsAppDatabases.Tables("Database").Rows(0)
                    dsAppDatabases.Tables("Database").Rows.Remove(Removerow)
                End If

                Dim row As DataRow = dsAppDatabases.Tables("Database").NewRow
                row("DbPass") = psEncrypt(DbPass)
                row("DbServer") = DbServer
                row("DbUname") = DbUname
                row("SysPass") = psEncrypt(SysPass)
                row("DBSQLPort") = DBSQLPort
                row("DbName") = DbName
                row("DbType") = DbType

                row("ComServer") = ComServer
                row("ComUname") = ComUname
                row("ComPass") = psEncrypt(ComPass)
                row("CommPort") = CommPort
                row("ComName") = ComName

                row("ExpServer") = ExpServer
                row("ExpUname") = ExpUname
                row("ExpPass") = psEncrypt(ExpPass)
                row("ExpPort") = ExpPort
                row("ExpName") = ExpName
                row("ReportPrinter") = strReportPrinter
                row("LabelPrinter") = strLabelReport

                'dsAppDatabases.Tables("Database").Rows.Add(row)
                dsAppDatabases.Tables("Database").Rows.InsertAt(row, 0)
                dsAppDatabases.WriteXml(DatabaseConfigFilePath, XmlWriteMode.WriteSchema)
                Get_PrinterSettings()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "ZulAsset", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Shared Sub Get_PrinterSettings()
            dsAppDatabases.Clear()
            dsAppDatabases.ReadXml(AppConfig.DatabaseConfigFilePath)
            Dim dt As DataTable = dsAppDatabases.Tables("Database")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                AppConfig.ReportPrinter = dt.Rows(0)("ReportPrinter")
                AppConfig.LabelPrinter = dt.Rows(0)("LabelPrinter")
            End If
        End Sub

        Public Shared Sub Get_DbConfig_Login()
            If System.IO.File.Exists(AppConfig.DatabaseConfigFilePath) Then
                'Check if the file is Readonly then change to Normal.
                'If IO.File.GetAttributes(AppConfig.DatabaseConfigFilePath) And IO.FileAttributes.ReadOnly = IO.FileAttributes.ReadOnly Then
                '    IO.File.SetAttributes(AppConfig.DatabaseConfigFilePath, IO.FileAttributes.Normal)
                'End If

                LoadDatabases()
            Else

                Dim sw As IO.StreamWriter = IO.File.CreateText(AppConfig.DatabaseConfigFilePath)
                Try
                    sw.Write(My.Resources.Data)
                Catch ex As Exception
                    Throw New Exception("Data.xml file not found in the (" & Application.CommonAppDataPath & ")")   'Feb 28
                Finally
                    sw.Close()
                End Try
                LoadDatabases()
            End If
        End Sub
#End Region


    End Class
End Namespace