Imports System.IO
Imports System.Windows.Forms


Friend Class frmRegister

    Friend objClass As clsLicense
    Friend HardwareID As String

    Dim objDAL As New clsData.DataAccess

    Private Sub frmRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim SerialNo As String
        Dim LicType As String = IIf(optServer.Checked = True, objClass.ServerCharacter, objClass.ClientCharacter)
        Dim YourID As String = YID()

        Me.Text = Application.ProductName & " - Registration"

        'Generate serial no based on license type (client or server)  
        SerialNo = GenSerial(LicType, GetSelectedEdition)

        'Break serial no into two parts
        txtSerial1.Text = Mid(SerialNo, 1, 6)
        txtSerial2.Text = Mid(SerialNo, 7)

        'Place generated YID in text boxes
        txtYID1.Text = Mid$(YourID, 1, 4)
        txtYID2.Text = Mid$(YourID, 5, 4)
        txtYID3.Text = Mid$(YourID, 9)

        lblNote.Text = "Please send the generated account # to Zultec Technical " & _
                       "Services by following ways to get the key:" & _
                       vbCrLf & vbCrLf & "By Tel:         +966-2-6700490  Ext. 242" & _
                       vbCrLf & "By Fax:        +966-2-6700341" & vbCrLf & _
                       "By Email:       it-support@zultec.com" & vbCrLf & vbCrLf & _
                       "WEB:     www.zultec.com"
    End Sub

    Private Function YID() As String
        'Initialize the random-number generator.
        Randomize()
        YID = CLng((99 * Rnd())) + 1 & Format(Now, "yymmddHHmm")
    End Function

    Private Function GenSerial(ByVal LicType As String, ByVal LicEdition As String) As String
        GenSerial = objClass.SoftwareVersion & LicType & LicEdition & HardwareID
    End Function


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Function GetSelectedEdition() As String
        If optEnterprise.Checked Then
            Return "E"
        ElseIf optFinancial.Checked Then
            Return "F"
        ElseIf optTracking.Checked Then
            Return "T"
        ElseIf optInventory.Checked Then
            Return "I"
        Else
            Return ""
        End If
    End Function

    Private Function GetEdition() As String
        If optEnterprise.Checked Then
            Return "Enterprise"
        ElseIf optFinancial.Checked Then
            Return "Financial"
        ElseIf optTracking.Checked Then
            Return "Tracking"
        ElseIf optInventory.Checked Then
            Return "Inventory"
        Else
            Return ""
        End If
    End Function

    

    Private Sub optEnterprise_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optEnterprise.CheckedChanged, optTracking.CheckedChanged, optInventory.CheckedChanged, optFinancial.CheckedChanged, optServer.CheckedChanged, optClient.CheckedChanged
        Dim SerialNo As String



        Try
            If optServer.Checked Then
                txtDBPath.Text = ""
                txtDBPath.Enabled = False
                btnBrowse.Enabled = False
                SerialNo = GenSerial(objClass.ServerCharacter, GetSelectedEdition)
            Else
                txtDBPath.Enabled = True
                btnBrowse.Enabled = True
                SerialNo = GenSerial(objClass.ClientCharacter, GetSelectedEdition)
            End If
        Catch ex As Exception
            SerialNo = "0000000000"
        End Try
        'Generate serial no based on license type (server)  


        'Break serial no into two parts
        txtSerial1.Text = Mid(SerialNo, 1, 6)
        txtSerial2.Text = Mid(SerialNo, 7)
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        With ofd1

            .CheckFileExists = True
            .CheckPathExists = True
            .FileName = ""
            .ShowReadOnly = True
            .DefaultExt = "mdb"

            .Filter = "Server Database (*.mdb)|*.mdb"

            .Multiselect = False
            .Title = "Select Server Database Path"

            If .ShowDialog() = DialogResult.OK Then
                txtDBPath.Text = .FileName
            End If
            .Dispose()
        End With
    End Sub

    Private Sub txtDBPath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDBPath.TextChanged

        Dim DataRead As IDataReader
        Dim qry As String
        Dim isValidated As Boolean
        Dim YID As String

        If Len(Trim(txtDBPath.Text)) = 0 Then Exit Sub

        '************* Variables to hold license info from local database ************

        Dim SerialNo As String
        Dim LicKey As String

        '*****************************************************************************

        'open local security database to verify software license 
        objDAL.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                                  "Data Source=" & txtDBPath.Text & _
                                  ";Jet OLEDB:Database Password=" & objClass._DBPass & ";"

        objDAL.Provider = clsData.EnumProviders.OLEDB
        Try
            objDAL.OpenConn()
        Catch ex As Exception
            txtDBPath.Text = ""
            MsgBox(ex.Message, MsgBoxStyle.Critical, "System Message")
            Exit Sub
        End Try

        'if database is opened successfully, then verify license info
        qry = "SELECT SerialNo, LicKey, CompName, Email, YourID FROM LICINFO"

        DataRead = objDAL.ExecDataReader(qry, CommandType.Text)

        If DataRead.Read Then
            'if record is found, retrieve serialno and license key
            SerialNo = DataRead("SerialNo")
            LicKey = DataRead("LicKey")

            'verify license key against serial no
            Try
                isValidated = ValidateKey(SerialNo, LicKey)
            Catch ex As Exception
                DataRead.Close()
                txtDBPath.Text = ""
                MsgBox(ex.Message, MsgBoxStyle.Critical, "System Message")
                Exit Sub
            End Try

            If isValidated Then
                ' if license is verified, check that it is server, not client
                If SerialType(SerialNo) = objClass.ServerCharacter Then
                    txtComp.Text = DataRead("CompName")
                    txtEmail.Text = DataRead("Email")
                    YID = DataRead("YourID")
                    txtYID1.Text = Mid$(YID, 1, 4)
                    txtYID2.Text = Mid$(YID, 5, 4)
                    txtYID3.Text = Mid$(YID, 9)

                    objClass.SystemDatabasePath = txtDBPath.Text

                    DataRead.Close()

                Else   'if SerialType(SerialNo) = "A" Then
                    MsgBox("Server license not found at following location" & vbCrLf & _
                    txtDBPath.Text, MsgBoxStyle.Critical, "System Message")
                    txtDBPath.Text = ""
                    DataRead.Close()
                End If
            Else  'If isValidated Then
                MsgBox("Server license could not be verified", MsgBoxStyle.Critical, "System Message")
                txtDBPath.Text = ""
                DataRead.Close()
            End If

        Else 'If DataRead.Read Then            
            MsgBox("Server license not found at following location" & vbCrLf & _
                    txtDBPath.Text, MsgBoxStyle.Critical, "System Message")
            txtDBPath.Text = ""
            DataRead.Close()
        End If
    End Sub

    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Dim SerialNo As String
        Dim LicKey As String
        Dim qry As String
        Dim DataRead As IDataReader
        Dim ServerPath As String
        Dim isValidated As Boolean 'flag to check if key is verified
        'file to store license information
        Dim LicFile As String = Application.StartupPath & "\License.txt"
        'information to write in licfile 
        Dim LicText As String

        'make sure required information is not missing
        If Len(Trim(txtComp.Text)) = 0 Then
            MsgBox("Company name cannot be empty", vbCritical, "System Message")
            txtComp.Focus()
            Exit Sub
        End If

        objClass.RegisteredTo = txtComp.Text

        If Len(Trim(txtEmail.Text)) = 0 Then
            MsgBox("Email address cannot be empty", vbCritical, "System Message")
            txtEmail.Focus()
            Exit Sub
        End If

        If Len(Trim(txtYID1.Text)) = 0 Or Len(Trim(txtYID2.Text)) = 0 Or Len(Trim(txtYID3.Text)) = 0 Then
            MsgBox("Your ID cannot be empty", vbCritical, "System Message")
            txtYID1.Focus()
            Exit Sub
        Else
            Dim YID As String = Trim(txtYID1.Text) & Trim(txtYID2.Text) & _
                            Trim(txtYID3.Text)
        End If

        If Len(Trim(txtKey.Text)) = 0 Then
            MsgBox("License key cannot be empty", vbCritical, "System Message")
            txtKey.Focus()
            Exit Sub
        End If

        'in case of server, security database will exist in local path else remote path
        If optServer.Checked = True Then
            ServerPath = objClass._DBName
        Else
            If Len(Trim(txtDBPath.Text)) = 0 Then
                MsgBox("Server database path cannot be empty", MsgBoxStyle.Critical, "System Message")
                txtDBPath.Focus()
                Exit Sub
            Else
                ServerPath = txtDBPath.Text
            End If
        End If

        SerialNo = txtSerial1.Text & txtSerial2.Text
        LicKey = txtKey.Text
       

        'verify license key against serial no
        Try
            isValidated = ValidateKey(SerialNo, LicKey)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "System message")
            Exit Sub
        End Try

        'take necessary action based on validation
        If isValidated Then
            'open local security database and update license info
            objDAL.ConnectionString = objClass.ConnectionString

            objDAL.Provider = clsData.EnumProviders.OLEDB
            Try
                objDAL.OpenConn()
            Catch ex As Exception
                txtDBPath.Text = ""
                MsgBox(ex.Message, MsgBoxStyle.Critical, "System Message")
                Exit Sub
            End Try

            'open datareader to check if there is any record in table
            qry = "SELECT SerialNo FROM LICINFO"
            DataRead = objDAL.ExecDataReader(qry, CommandType.Text)

            'make qry based on record existance in Licinfo table
            If DataRead.Read Then
                qry = "UPDATE LICINFO SET YourID='" & YID() & "', CompName='" & _
                       txtComp.Text & "', Email='" & txtEmail.Text & _
                       "', SerialNo='" & SerialNo & "', LicKey='" & LicKey & _
                       "', ServerPath='" & ServerPath & "'"

            Else 'If DataRead.Read Then
                qry = "INSERT INTO LICINFO(YourID, CompName, Email, SerialNo, " & _
                      "LicKey, ServerPath) VALUES ('" & YID() & "','" & _
                      txtComp.Text & "','" & txtEmail.Text & "','" & _
                      SerialNo & "','" & LicKey & "','" & ServerPath & "')"
            End If

            DataRead.Close()

            objDAL.ConnectionString = objClass.ConnectionString

            objDAL.Provider = clsData.EnumProviders.OLEDB
            Try
                objDAL.OpenConn()
            Catch ex As Exception
                txtDBPath.Text = ""
                MsgBox(ex.Message, MsgBoxStyle.Critical, "System Message")
                Exit Sub
            End Try

            Try
                objDAL.ExecNonQuery(qry, CommandType.Text)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "System Message")
                Exit Sub
            Finally
                objDAL.CloseConn()
            End Try

            LicText = "Customer ID: " & YID() & _
                       txtYID3.Text & vbCrLf & "Company: " & _
                       txtComp.Text & vbCrLf & _
                       "Application Edition: " & GetEdition() & vbCrLf & _
                       "Serial No: " & SerialNo & vbCrLf & _
                       "License Key: " & LicKey

            'open text file and save license information for future reference 
            Dim sr As StreamWriter = File.CreateText(LicFile)
            sr.WriteLine(LicText)
            sr.Close()

            MsgBox("Registration done successfully." & vbCrLf & vbCrLf & _
                   "The license information has been saved " & _
                   "in " & Application.StartupPath & "\License.txt" & vbCrLf & _
                   vbCrLf & "Kindly keep this file at a safe place for " & _
                   "future reference.", vbInformation, "System Message")

            'registration is successfull. now display login form
            Me.Close()

        Else 'If vSec.ValidateKey(SerialNo, LicKey) = True Then
            MsgBox("Invalid license key", MsgBoxStyle.Critical, "System Message")
            txtKey.Focus()
            txtKey.SelectAll()
        End If
    End Sub

    Private Sub btnEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmail.Click
        Dim Mail As ASPEMAILLib.MailSender
        Dim MsgBody As String
        Dim MsgSub As String   'subject of email
        Dim frmSendEmail As New frmSendEmail

        'make sure required information is not missing
        If Len(Trim(txtComp.Text)) = 0 Then
            MsgBox("Company name cannot be empty", vbCritical, "System Message")
            txtComp.Focus()
            Exit Sub
        End If

        If Len(Trim(txtEmail.Text)) = 0 Then
            MsgBox("Email address cannot be empty", vbCritical, "System Message")
            txtEmail.Focus()
            Exit Sub
        End If

        If Len(Trim(txtYID1.Text)) = 0 Or Len(Trim(txtYID2.Text)) = 0 Or _
           Len(Trim(txtYID3.Text)) = 0 Then
            MsgBox("Your ID cannot be empty", vbCritical, "System Message")
            txtYID1.Focus()
            Exit Sub
        End If

        Application.DoEvents()

        Me.Enabled = False
        frmSendEmail.Show()
        frmSendEmail.Refresh()


        MsgSub = Application.ProductName & " License Key Required"

        MsgBody = "Customer ID: " & txtYID1.Text & txtYID2.Text & _
                   txtYID3.Text & vbCrLf & "Company: " & _
                   txtComp.Text & vbCrLf & _
                   "Application Edition: " & GetEdition() & vbCrLf & _
                   "Serial No: " & txtSerial1.Text & txtSerial2.Text

        Mail = New ASPEMAILLib.MailSender

        Mail.Host = "smtp.zultec.com"
        Mail.From = txtEmail.Text
        Mail.AddAddress("it-support@zultec.com")
        Mail.AddCC("Wael.Dalloul@zultec.com")
        Mail.Subject = MsgSub
        Mail.Body = MsgBody
        Mail.Username = "it-support@zultec.com"
        Mail.Password = "itjed"

        Try
            Mail.Send()
        Catch ex As Exception
            Me.Enabled = True
            frmSendEmail.Close()
            MsgBox("Email send failed. Following error occurred." & vbCrLf & _
                    ex.Message, MsgBoxStyle.Critical, "System Message")
            Exit Sub
        End Try

        frmSendEmail.Close()
        Me.Enabled = True
        MsgBox("Email sent successfully", MsgBoxStyle.Information, "System Message")

    End Sub


End Class