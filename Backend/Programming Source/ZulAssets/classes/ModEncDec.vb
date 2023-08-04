
Imports System.Text
Imports System.Collections.Specialized
Imports System.Security.Cryptography
Module ModEncDec
    Public Function Encrypt(ByVal Plain As String, ByRef sEncKey As String) As String

        Dim encrypted2 As String
        Dim LenLetter As Short
        Dim Letter As String
        Dim KeyNum As String
        Dim encstr As String = Nothing
        Dim temp As String
        Dim temp2 As String
        Dim itempstr As String
        Dim itempnum As Short
        Dim Math As Integer
        Dim i As Short

        On Error GoTo oops

        If sEncKey = "" Then sEncKey = "WhiteKnight"
        'Sets the Encryption Key if one is not set
        'UPGRADE_WARNING: Lower bound of array encKEY was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1033"'
        Dim encKEY(Len(sEncKey)) As Object

        'starts the values for the Encryption Key

        For i = 1 To Len(sEncKey)
            KeyNum = Mid(sEncKey, i, 1) 'gets the letter at index i
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            encKEY(i) = Asc(KeyNum) 'sets the the Array value
            'to ASC number for the letter

            'This is the first letter so just hold the value
            If i = 1 Then
                'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Math = encKEY(i) : GoTo nextone
            End If

            'compares the value to the previous value and then
            'either adds/subtracts the value to the Math total
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i - 1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If i >= 2 And Math - encKEY(i) >= 0 And encKEY(i) <= encKEY(i - 1) Then Math = Math - encKEY(i)

            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i - 1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If i >= 2 And Math - encKEY(i) >= 0 And encKEY(i) <= encKEY(i - 1) Then Math = Math - encKEY(i)
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i - 1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If i >= 2 And encKEY(i) >= Math And encKEY(i) >= encKEY(i - 1) Then Math = Math + encKEY(i)
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i - 1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If i >= 2 And encKEY(i) < Math And encKEY(i) >= encKEY(i - 1) Then Math = Math + encKEY(i)
nextone:
        Next i


        For i = 1 To Len(Plain) 'Now for the String to be encrypted
            Letter = Mid(Plain, i, 1) 'sets Letter to
            'the letter at index i
            LenLetter = Asc(Letter) + Math 'Now it adds the Asc
            'value of Letter to Math

            'checks and corrects the format then adds a space to separate them frm each other
            If LenLetter >= 100 Then encstr = encstr & Asc(Letter) + Math & " "

            'checks and corrects the format then adds a space
            'to separate them frm each other
            If LenLetter <= 99 Then encstr = encstr & "0" & Asc(Letter) + Math & " "
        Next i


        'This is part of what i'm doing to convert the encrypted
        'numbers to Letters so it sort of encrypts the
        'encrypted message.
        temp = encstr 'hold the encrypted data
        temp = TrimSpaces(temp) 'get rid of the spaces
        itempnum = CShort(Mid(temp, 1, 2)) 'grab the first 2 numbers
        temp2 = Chr(itempnum + 100) 'Now add 100 so it
        'will be a valid char

        'If its a 2 digit number hold it and continue
        If Len(itempnum) >= 2 Then itempstr = Str(itempnum)

        'If the number is a single digit then add a '0' to the front
        'then hold it
        If Len(itempnum) = 1 Then itempstr = "0" & TrimSpaces(Str(itempnum))

        encrypted2 = temp2 'set the encrypted message

        For i = 3 To Len(temp) Step 2
            itempnum = CShort(Mid(temp, i, 2)) 'grab the next 2 numbers

            ' add 100 so it will be a valid char
            temp2 = Chr(itempnum + 100)

            'if its the last number we only want to hold it we
            'don't want to add a '0' even if its a single digit
            If i = Len(temp) Then itempstr = Str(itempnum) : GoTo itsdone

            'If its a 2 digit number hold it and continue
            If Len(itempnum) = 2 Then itempstr = Str(itempnum)

            'If the number is a single digit then add a '0'
            'to the front then hold it
            If Len(TrimSpaces(Str(itempnum))) = 1 Then itempstr = "0" & TrimSpaces(Str(itempnum))

            'Now check to see if a - number was created
            'if so cause an error message
            If Left(TrimSpaces(Str(itempnum)), 1) = "-" Then Err.Raise(20000, , "Unexpected Error")


itsdone:
            'Set The Encrypted message
            encrypted2 = encrypted2 & temp2
        Next i


        'Encrypt = encstr 'Returns the First Encrypted String
        Encrypt = encrypted2 'Returns the Second Encrypted String
        Exit Function 'We are outta Here
oops:
        'System.Diagnostics.Debug.WriteLine(VB6.TabLayout("Error description", Err.Description))
        'System.Diagnostics.Debug.WriteLine(VB6.TabLayout("Error source:", Err.Source))
        'System.Diagnostics.Debug.WriteLine(VB6.TabLayout("Error Number:", Err.Number))
    End Function

    Public Function Decrypt(ByVal Encrypted As String, ByRef sEncKey As String) As String
        On Error Resume Next
        Dim NewEncrypted As String = Nothing
        Dim Letter As String = Nothing
        Dim KeyNum As String
        Dim EncNum As String = Nothing
        Dim encbuffer As Integer
        Dim strDecrypted As String = Nothing
        Dim Kdecrypt As String
        Dim lastTemp As String
        Dim LenTemp As Short
        Dim temp As String = Nothing
        Dim temp2 As String = Nothing
        Dim itempstr As String
        Dim itempnum As Short
        Dim Math As Integer
        Dim i As Short

        '    On Error GoTo oops

        If sEncKey = "" Then sEncKey = "WhiteKnight"

        'UPGRADE_WARNING: Lower bound of array encKEY was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1033"'
        Dim encKEY(Len(sEncKey)) As Object

        'Convert The Key For Decryption
        For i = 1 To Len(sEncKey)
            KeyNum = Mid(sEncKey, i, 1) 'Get Letter i% in the Key
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            encKEY(i) = Asc(KeyNum) 'Convert Letter i to Asc value

            'if it the first letter just hold it
            If i = 1 Then
                'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Math = encKEY(i) : GoTo nextone
            End If
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i - 1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If i >= 2 And Math - encKEY(i) >= 0 And encKEY(i) <= encKEY(i - 1) Then Math = Math - encKEY(i)
            'compares the value to the previous value and
            'then either adds/subtracts the value to the
            'Math total
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i - 1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If i >= 2 And Math - encKEY(i) >= 0 And encKEY(i) <= encKEY(i - 1) Then Math = Math - encKEY(i)
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i - 1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If i >= 2 And encKEY(i) >= Math And encKEY(i) >= encKEY(i - 1) Then Math = Math + encKEY(i)
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i - 1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object encKEY(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If i >= 2 And encKEY(i) < Math And encKEY(i) >= encKEY(i - 1) Then Math = Math + encKEY(i)
nextone:
        Next i


        'This is part of what i'm doing to convert the encrypted
        'numbers to  Letters so it sort of encrypts the encrypted
        'message.
        temp = Encrypted 'hold the encrypted data


        For i = 1 To Len(temp)
            itempstr = TrimSpaces(Str(Asc(Mid(temp, i, 1)) - 100)) 'grab the next 2 numbers
            'If its a 2 digit number hold it and continue
            If Len(itempstr) = 2 Then itempstr = itempstr
            If i = Len(temp) - 2 Then LenTemp = Len(Mid(temp2, Len(temp2) - 3))
            If i = Len(temp) Then itempstr = TrimSpaces(itempstr) : GoTo itsdone
            'If the number is a single digit then add a '0' to the
            'front then hold it
            If Len(TrimSpaces(itempstr)) = 1 Then itempstr = "0" & TrimSpaces(itempstr)
            'Now check to see if a - number was created if so
            'cause an error message
            If Left(TrimSpaces(itempstr), 1) = "-" Then Err.Raise(20000, , "Unexpected Error")


itsdone:
            temp2 = temp2 & itempstr 'hold the first decryption
        Next i


        Encrypted = TrimSpaces(temp2) 'set the encrypted data


        For i = 1 To Len(Encrypted) Step 3
            'Format the encrypted string for the second decryption
            NewEncrypted = NewEncrypted & Mid(Encrypted, CInt(i), 3) & " "
        Next i

        ' Hold the last set of numbers to check it its the correct format
        lastTemp = TrimSpaces(Mid(NewEncrypted, Len(NewEncrypted) - 3))

        If Len(lastTemp) = 2 Then
            ' If it = 2 then its not the Correct format and we need to fix it
            lastTemp = Mid(NewEncrypted, Len(NewEncrypted) - 1) 'Holds Last Number so a '0'
            'Can be added between them
            'set it to the new format
            Encrypted = Mid(NewEncrypted, 1, Len(NewEncrypted) - 2) & "0" & lastTemp
        Else
            Encrypted = NewEncrypted 'set the new format

        End If
        'The Actual Decryption
        For i = 1 To Len(Encrypted)
            Letter = Mid(Encrypted, i, 1) 'Hold Letter at index i
            EncNum = EncNum & Letter 'Hold the letters
            If Letter = " " Then 'we have a letter to decrypt
                encbuffer = CInt(Mid(EncNum, 1, Len(EncNum) - 1)) 'Convert it to long and
                'get the number minus the " "
                strDecrypted = strDecrypted & Chr(encbuffer - Math) 'Store the decrypted string
                EncNum = "" 'clear if it is a space so we can get
                'the next set of numbers
            End If
        Next i

        Decrypt = strDecrypted

        Exit Function
oops:
        'System.Diagnostics.Debug.WriteLine(VB6.TabLayout("Error description", Err.Description))
        'System.Diagnostics.Debug.WriteLine(VB6.TabLayout("Error source:", Err.Source))
        'System.Diagnostics.Debug.WriteLine(VB6.TabLayout("Error Number:", Err.Number))
        'Err.Raise(20001, , "You have entered the wrong encryption string")

    End Function

    Private Function TrimSpaces(ByRef strstring As String) As String
        Dim lngpos As Integer
        Do While InStr(1, strstring, " ")
            System.Windows.Forms.Application.DoEvents()
            lngpos = InStr(1, strstring, " ")
            strstring = Left(strstring, lngpos - 1) & Right(strstring, Len(strstring) - (lngpos + Len(" ") - 1))
        Loop
        TrimSpaces = strstring
    End Function
    Private lbtVector() As Byte = {240, 3, 45, 29, 0, 76, 173, 59}
    Private lscryptoKey As String = "13"
    Public Function psDecrypt(ByVal sQueryString As String) As String
        Dim buffer() As Byte
        Dim loCryptoClass As New TripleDESCryptoServiceProvider
        Dim loCryptoProvider As New MD5CryptoServiceProvider
        Try
            buffer = Convert.FromBase64String(sQueryString)
            loCryptoClass.Key = loCryptoProvider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(lscryptoKey))
            loCryptoClass.IV = lbtVector
            Return Encoding.ASCII.GetString(loCryptoClass.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))
        Catch ex As Exception
            Throw ex
        Finally
            loCryptoClass.Clear()
            loCryptoProvider.Clear()
            loCryptoClass = Nothing
            loCryptoProvider = Nothing
        End Try


    End Function
    'Author      :       Nikhil Gupta
    'Description :       This function encrypts a given string
    'Parameters  :       String
    'Return Values:      Encrypted String
    'Called From :       Business Layer
    Public Function psEncrypt(ByVal sInputVal As String) As String

        Dim loCryptoClass As New TripleDESCryptoServiceProvider
        Dim loCryptoProvider As New MD5CryptoServiceProvider
        Dim lbtBuffer() As Byte

        Try
            lbtBuffer = System.Text.Encoding.ASCII.GetBytes(sInputVal)
            loCryptoClass.Key = loCryptoProvider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(lscryptoKey))
            loCryptoClass.IV = lbtVector
            sInputVal = Convert.ToBase64String(loCryptoClass.CreateEncryptor().TransformFinalBlock(lbtBuffer, 0, lbtBuffer.Length()))
            psEncrypt = sInputVal
        Catch ex As CryptographicException
            Throw ex
        Catch ex As FormatException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            loCryptoClass.Clear()
            loCryptoProvider.Clear()
            loCryptoClass = Nothing
            loCryptoProvider = Nothing
        End Try
    End Function
End Module