Imports System.Windows.Forms
Imports System.IO
Imports System.Net.Mail

Public Class ctlRegistration
    Private _ServerCharacter As String
    Private _ClientCharacter As String
    Private _SelectedEdition As String

    Private _SoftwareVersion As String
    Private _Registered As Boolean = False
    Dim objLic As New ZulLic

    Public Property Registered() As Boolean
        Get
            Return _Registered
        End Get
        Set(ByVal value As Boolean)
            _Registered = value
        End Set
    End Property
    Public Property SoftwareVersion() As String
        Get
            Return _SoftwareVersion
        End Get

        Set(ByVal Value As String)
            _SoftwareVersion = Value
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


    Public Property SelectedEdition() As String
        Get
            Return _SelectedEdition
        End Get

        Set(ByVal Value As String)
            _SelectedEdition = Value
        End Set
    End Property


    Private Function GetSelectedEdition() As String
        'Now we only have one edition Enterprise.
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

    Private Sub optEnterprise_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optEnterprise.CheckedChanged, optTracking.CheckedChanged, optInventory.CheckedChanged, optFinancial.CheckedChanged
        Dim SerialNo As String
        Try
            SerialNo = objLic.GenSerial(ServerCharacter, GetSelectedEdition)
        Catch ex As Exception
            SerialNo = "0000000000"
        End Try
        'Generate serial no based on license type (server)  


        'Break serial no into two parts
        txtSerial1.Text = Mid(SerialNo, 1, 6)
        txtSerial2.Text = Mid(SerialNo, 7)
    End Sub


    Private Sub ctlRegistration_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ServerCharacter = "G"
        ClientCharacter = "H"

        SoftwareVersion = My.Application.Info.Version.ToString().Replace(".", "")

        Dim LicType As String = ServerCharacter 'Now We only have server no client
        Me.Text = Application.ProductName & " - Registration"

        Dim SerialNo As String = objLic.GenSerial(LicType, GetSelectedEdition)
        'Break serial no into two parts
        txtSerial1.Text = Mid(SerialNo, 1, 6)
        txtSerial2.Text = Mid(SerialNo, 7)
        lblNote.Text = "Please send the generated Serial Number to Zultec Technical " & _
                      "Services by following ways to get the key:" & _
                      vbCrLf & vbCrLf & "By Tel:         +966-2-6085588  Ext. 401" & _
                      vbCrLf & "By Fax:        +966-2-6084447" & vbCrLf & _
                      "By Email:       it-support@zultec.com" & vbCrLf & vbCrLf & _
                      "WEB:     www.zultec.com"
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.ParentForm.Close()
    End Sub

    Private Sub btnEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmail.Click
        'Dim Mail As New ASPEMAILLib.MailSender
        Dim MsgBody As String
        Dim MsgSub As String   'subject of email

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

        MsgSub = Application.ProductName & " License Key Required"

        MsgBody = "Company: " & _
                   txtComp.Text & vbCrLf & _
                   "Application Edition: Enterprise" & vbCrLf & _
                   "Serial No: " & txtSerial1.Text & txtSerial2.Text

        Try
            Dim SmtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            SmtpServer.Credentials = New  _
  Net.NetworkCredential("it-support@zultec.com", "itjed")
            'SmtpServer.Port = 587
            SmtpServer.Host = "smtp.zultec.com"
            mail = New MailMessage()
            mail.From = New MailAddress(txtEmail.Text)
            mail.To.Add("it-support@zultec.com")
            mail.To.Add("Wael.Dalloul@zultec.com")
            mail.Subject = MsgSub
            mail.Body = MsgBody
            SmtpServer.Send(mail)
            MsgBox("Email sent successfully", MsgBoxStyle.Information, "System Message")
        Catch ex As Exception
            MsgBox("Email send failed. Following error occurred." & vbCrLf & _
                    ex.Message, MsgBoxStyle.Critical, "System Message")
        End Try
    End Sub

    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Dim LicFile As String = Application.StartupPath & "\License.txt"
        Dim SerialNo As String
        Dim LicKey As String
        Dim isValidated As Boolean = False 'flag to check if key is verified
        Dim LicText As String = String.Empty

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

        If Len(Trim(txtKey.Text)) = 0 Then
            MsgBox("License key cannot be empty", vbCritical, "System Message")
            txtKey.Focus()
            Exit Sub
        End If


        SerialNo = txtSerial1.Text & txtSerial2.Text
        LicKey = txtKey.Text


        'verify license key against serial no
        Try
            isValidated = objLic.ValidateKey(SerialNo, LicKey)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "System message")
            Exit Sub
        End Try

        'take necessary action based on validation
        If isValidated Then

            LicText = "Company: " & _
                       txtComp.Text & vbCrLf & _
                       "Application Edition: " & GetSelectedEdition() & vbCrLf & _
                       "Serial No: " & SerialNo & vbCrLf & _
                       "License Key: " & LicKey & vbCrLf & _
                       "Email: " & txtEmail.Text

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
            Registered = True
            Me.ParentForm.Close()

        Else 'If vSec.ValidateKey(SerialNo, LicKey) = True Then
            MsgBox("Invalid license key", MsgBoxStyle.Critical, "System Message")
            Registered = False
            txtKey.Focus()
            txtKey.SelectAll()
        End If
    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Clipboard.SetText(txtSerial1.Text & txtSerial2.Text)
    End Sub
End Class
