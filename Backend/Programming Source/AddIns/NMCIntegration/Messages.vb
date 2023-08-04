Imports System.IO
Imports DevExpress.XtraEditors
Imports System.Windows.Forms

Public Class Messages

    Public Shared Sub ErrorMessage(ByVal Msg As String, Optional ByVal ErrSource As String = "")
        'MessageBox.Show(Msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        XtraMessageBox.Show(Msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'GenericExceptionHandler(Msg, ErrSource, AppConfigData.LoginName)
    End Sub

    Public Shared Sub InfoMessage(ByVal Msg As String)
        XtraMessageBox.Show(Msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        'MessageBox.Show(Msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Shared Function QuestionMessage(ByVal msg As String) As DialogResult

        'Return MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        Return XtraMessageBox.Show(msg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
    End Function


End Class
