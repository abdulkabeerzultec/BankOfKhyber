Imports System.IO
Imports ZulAssetsDAL.ZulAssetsDAL
Module Module1
    Public Sub main()

    End Sub

    Public Sub GenericExceptionHandler(ByVal ex As Exception, _
                                       ByVal ErrSource As String)
        If Not Directory.Exists(AppConfig.AppDataFolder & "\ErrorsReports\") Then
            Directory.CreateDirectory(AppConfig.AppDataFolder & "\ErrorsReports\")
        End If

        Dim ErrFile As String = AppConfig.AppDataFolder & "\ErrorsReports\" & "ZulAssetsErr.Log"
        Dim sr As StreamWriter = File.AppendText(ErrFile)
        Dim ErrTxt As String

        ErrTxt = "Date  : " & Format(Now, "MMM dd, yyyy HH:mm") & Environment.NewLine & _
                 "Error : " & ex.Message & Environment.NewLine & _
                 "Source: " & ErrSource & Environment.NewLine & Environment.NewLine & _
                 "Stack : " & ex.StackTrace & Environment.NewLine & _
                 "-----------------------------------------------------------" & _
                 "-----------------------------------------------------------" & _
                 Environment.NewLine & Environment.NewLine

        sr.WriteLine(ErrTxt)
        sr.Close()

        Dim frm As New frmError
        frm.Ex = ex
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()

    End Sub
End Module
