Namespace ZulAssetsDAL

    Module Module1
        Public Function BackEndDate(ByVal DateValue As String) As String
            Dim BackEndDate1 As String
            BackEndDate1 = ""
            Dim mydate As Date
            mydate = DateValue
            If mydate = Date.MinValue Then
                mydate = New Date(1900, 1, 1)
            End If
            If AppConfig.DbType = 0 Then ' for Access
                BackEndDate1 = "#" & mydate.ToString() & "#"

            ElseIf AppConfig.DbType = 1 Then 'for SQL Server
                BackEndDate1 = "'" & mydate.ToString("MM/dd/yyyy") & "'"

            ElseIf AppConfig.DbType = 2 Then ' For Oracle

                Dim obj As New DBOperations
                Dim OraDateFmt As String
                OraDateFmt = obj.Get_OracleDate_Format()
                If OraDateFmt = "DD-MON-RR" Then
                    BackEndDate1 = "'" & mydate.ToString("dd-MMM-yyyy") & "'"
                Else
                    BackEndDate1 = "'" & mydate.ToString("dd-MMM-yyyy") & "'"
                End If
            End If
            Return BackEndDate1
        End Function

        Public Function BackendDateTime(ByVal dt As DateTime) As DateTime
            Return New DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Kind)
        End Function

    End Module
End Namespace

