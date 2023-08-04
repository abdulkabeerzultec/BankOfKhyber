Public Class ConnectionString
    Public Shared Sub ChangeDefaultConStr(ByVal constr As String)
        My.MySettings.Default("ConnectionString") = constr
    End Sub

    'Public Shared Sub ChangeDefaultTempConStr(ByVal constr As String)
    '    My.MySettings.Default("TempDBConnectionString") = constr
    'End Sub

End Class
