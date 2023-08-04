Imports System.Math

Public Class FrontEndLic

#Region "Variables"
#End Region

    Public Function ValidateKey(ByVal AccNum As String, ByVal Key As String) As Boolean

        Dim Key2Validate(4) As Int32, StartPos As Int32
        Dim FinalKey As String
        Dim EnValue As String
        Dim NegKey As String       ' if key is -ive, put it in NegKey
        Dim arr() As String

        If Len(Trim(Key)) = 0 Then
            MsgBox("Invalid Key", MsgBoxStyle.Critical, "System Message")
            Exit Function
        End If

        Try
            arr = Split(Key, "-")
            arr(3) = Replace(arr(3), "*", "-")

            EnValue = Encrypt("ABTAK56", Mid$(AccNum, 5), 2)
            'EnValue = Encrypt("ABTAK56", Mid$(AccNum, 5), 1)
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
            ValidateKey = False
            If Key = FinalKey Then ValidateKey = True

        Catch e As Exception
            MsgBox("Invalid Key " & Environment.NewLine & e.Message, MsgBoxStyle.Critical, "System Message")

            ValidateKey = False
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

End Class
