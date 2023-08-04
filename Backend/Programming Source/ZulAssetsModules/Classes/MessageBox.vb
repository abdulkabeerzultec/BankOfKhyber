Imports System.Text
Imports System.Resources
Imports System.Windows.Forms
Imports System.Reflection


Public Class ZulMessageBox
    Private Shared _Msg As String
    Public Shared Sub ShowMe(ByVal strKey As String)
        Dim rm As New ResourceManager("ZulHR.Messages", Assembly.GetExecutingAssembly())
        _Msg = rm.GetString(strKey)
        MessageBox.Show(_Msg, "ZulHRMS", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Public Shared Function ShowMe(ByVal strKey As String, ByVal BtnType As MessageBoxButtons) As DialogResult
        Dim rm As New ResourceManager("ZulHR.Messages", Assembly.GetExecutingAssembly())
        _Msg = rm.GetString(strKey)
        Return MessageBox.Show(_Msg, "ZulHRMS", BtnType, MessageBoxIcon.Question)
    End Function
    Public Shared Function ShowMe(ByVal strKey As String, ByVal BtnType As MessageBoxButtons, ByVal btnIcon As MessageBoxIcon, ByVal fromRes As Boolean) As DialogResult
        If fromRes Then
            Dim rm As New ResourceManager("ZulHR.Messages", Assembly.GetExecutingAssembly())
            _Msg = rm.GetString(strKey)
        Else
            _Msg = strKey
        End If
        Return MessageBox.Show(_Msg, "ZulHRMS", BtnType, btnIcon, MessageBoxDefaultButton.Button1)
    End Function

End Class
