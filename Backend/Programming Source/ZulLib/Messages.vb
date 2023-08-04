Imports System.IO
Imports DevExpress.XtraEditors
Imports System.Windows.Forms


Public Class Messages
    Private Shared _AppDataFolder As String
    Public Shared Property AppDataFolder() As String
        Get
            Return _AppDataFolder
        End Get
        Set(ByVal value As String)
            _AppDataFolder = value
        End Set
    End Property

    Private Shared Sub GenericExceptionHandler(ByVal msg As String, ByVal ErrSource As String, ByVal UserName As String)
        If Not Directory.Exists(AppDataFolder & "\ErrorsReports\") Then
            Directory.CreateDirectory(AppDataFolder & "\ErrorsReports\")
        End If

        Dim FileName As String = String.Format("{0}Err.Log", My.Application.Info.ProductName)
        Dim ErrFile As String = AppDataFolder & "\ErrorsReports\" & FileName
        Dim sr As StreamWriter = File.AppendText(ErrFile)
        Dim ErrTxt As String

        ErrTxt = "Date  : " & Format(Now, "MMM dd, yyyy HH:mm") & Environment.NewLine & _
                 "Error : " & msg & Environment.NewLine & _
                 "Source: " & ErrSource & Environment.NewLine & Environment.NewLine & _
                 "UserName: " & UserName & Environment.NewLine & Environment.NewLine & _
                 "-----------------------------------------------------------" & _
                 "-----------------------------------------------------------" & _
                 Environment.NewLine & Environment.NewLine

        sr.WriteLine(ErrTxt)
        sr.Close()
    End Sub

    Public Shared Sub ErrorMessage(ByVal Msg As String, ByVal UserName As String, Optional ByVal ErrSource As String = "")
        XtraMessageBox.Show(Msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        GenericExceptionHandler(Msg, ErrSource, UserName)
    End Sub

    Public Shared Sub InfoMessage(ByVal Msg As String)
        XtraMessageBox.Show(Msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Shared Function QuestionMessage(ByVal msg As String) As DialogResult
        Return XtraMessageBox.Show(msg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
    End Function

    Public Shared Function WarningMessage(ByVal msg As String) As DialogResult
        Return XtraMessageBox.Show(msg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
    End Function
End Class
