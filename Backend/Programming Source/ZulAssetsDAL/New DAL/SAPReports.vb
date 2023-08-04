Namespace SAPReportsTableAdapters

    Partial Public Class GoodsReceivingReportTableAdapter
        Inherits Global.System.ComponentModel.Component

        Public Sub FixParametersDataType()
            For Each Command As OleDb.OleDbCommand In Me.CommandCollection
                For Each param As OleDb.OleDbParameter In Command.Parameters
                    If param.OleDbType = OleDb.OleDbType.WChar Then
                        param.OleDbType = OleDb.OleDbType.VarWChar
                    End If
                Next
            Next
        End Sub
    End Class

    Partial Public Class GoodsIssuanceReportTableAdapter
        Inherits Global.System.ComponentModel.Component

        Public Property SelectCommand() As OleDb.OleDbCommand()
            Get
                'If (Me._commandCollection Is Nothing) Then
                '    Me.InitCommandCollection()
                'End If
                Return Me.CommandCollection
            End Get

            Set(ByVal value As OleDb.OleDbCommand())
                Me._commandCollection = value
            End Set

        End Property

        Public Function FillByWhere(ByVal WhereExp As String) As DataTable
            Dim stSelect As String
            stSelect = Me.CommandCollection(0).CommandText
            Try
                Me.CommandCollection(0).CommandText += " WHERE " + WhereExp
                Dim dataTable As SAPReports.GoodsIssuanceReportDataTable = New SAPReports.GoodsIssuanceReportDataTable
                Me.Adapter.Fill(dataTable)
                Return dataTable
            Catch ex As Exception
                Return Nothing
            Finally
                Me.CommandCollection(0).CommandText = stSelect
            End Try
        End Function

        Public Function GetDataByParamsSorted(ByVal SerialNo As String, ByVal ManufacturePartNo As String, ByVal SAPPartNo As String, ByVal AssetNo As String, ByVal DocType As String, ByVal SortExp As String) As DataTable
            Dim stSelect As String
            stSelect = Me.CommandCollection(1).CommandText
            Try
                Me.CommandCollection(1).CommandText += " ORDER BY " + SortExp
                Me.Adapter.SelectCommand = Me.CommandCollection(1)

                If (SerialNo Is Nothing) Then
                    Me.Adapter.SelectCommand.Parameters(0).Value = Global.System.DBNull.Value
                Else
                    Me.Adapter.SelectCommand.Parameters(0).Value = CType(SerialNo, String)
                End If
                If (ManufacturePartNo Is Nothing) Then
                    Me.Adapter.SelectCommand.Parameters(1).Value = Global.System.DBNull.Value
                Else
                    Me.Adapter.SelectCommand.Parameters(1).Value = CType(ManufacturePartNo, String)
                End If
                If (SAPPartNo Is Nothing) Then
                    Me.Adapter.SelectCommand.Parameters(2).Value = Global.System.DBNull.Value
                Else
                    Me.Adapter.SelectCommand.Parameters(2).Value = CType(SAPPartNo, String)
                End If
                If (AssetNo Is Nothing) Then
                    Me.Adapter.SelectCommand.Parameters(3).Value = Global.System.DBNull.Value
                Else
                    Me.Adapter.SelectCommand.Parameters(3).Value = CType(AssetNo, String)
                End If
                If (DocType Is Nothing) Then
                    Me.Adapter.SelectCommand.Parameters(4).Value = Global.System.DBNull.Value
                Else
                    Me.Adapter.SelectCommand.Parameters(4).Value = CType(DocType, String)
                End If

                Dim dataTable As SAPReports.GoodsIssuanceReportDataTable = New SAPReports.GoodsIssuanceReportDataTable
                Me.Adapter.Fill(dataTable)
                Return dataTable
            Catch ex As Exception
                Return Nothing
            Finally
                Me.CommandCollection(1).CommandText = stSelect
            End Try
        End Function


        Public Function GetDataByParamsAndDateSorted(ByVal SerialNo As String, ByVal ManufacturePartNo As String, ByVal SAPPartNo As String, ByVal AssetNo As String, ByVal DocType As String, ByVal DocDate As Global.System.Nullable(Of Date), ByVal DocDate1 As Global.System.Nullable(Of Date), ByVal SortExp As String) As DataTable
            Dim stSelect As String
            stSelect = Me.CommandCollection(2).CommandText
            Try
                Me.CommandCollection(2).CommandText += " ORDER BY " + SortExp
                Me.Adapter.SelectCommand = Me.CommandCollection(2)

                If (SerialNo Is Nothing) Then
                    Me.Adapter.SelectCommand.Parameters(0).Value = Global.System.DBNull.Value
                Else
                    Me.Adapter.SelectCommand.Parameters(0).Value = CType(SerialNo, String)
                End If
                If (ManufacturePartNo Is Nothing) Then
                    Me.Adapter.SelectCommand.Parameters(1).Value = Global.System.DBNull.Value
                Else
                    Me.Adapter.SelectCommand.Parameters(1).Value = CType(ManufacturePartNo, String)
                End If
                If (SAPPartNo Is Nothing) Then
                    Me.Adapter.SelectCommand.Parameters(2).Value = Global.System.DBNull.Value
                Else
                    Me.Adapter.SelectCommand.Parameters(2).Value = CType(SAPPartNo, String)
                End If
                If (AssetNo Is Nothing) Then
                    Me.Adapter.SelectCommand.Parameters(3).Value = Global.System.DBNull.Value
                Else
                    Me.Adapter.SelectCommand.Parameters(3).Value = CType(AssetNo, String)
                End If
                If (DocType Is Nothing) Then
                    Me.Adapter.SelectCommand.Parameters(4).Value = Global.System.DBNull.Value
                Else
                    Me.Adapter.SelectCommand.Parameters(4).Value = CType(DocType, String)
                End If
                If (DocDate.HasValue = True) Then
                    Me.Adapter.SelectCommand.Parameters(5).Value = CType(DocDate.Value, Date)
                Else
                    Me.Adapter.SelectCommand.Parameters(5).Value = Global.System.DBNull.Value
                End If
                If (DocDate1.HasValue = True) Then
                    Me.Adapter.SelectCommand.Parameters(6).Value = CType(DocDate1.Value, Date)
                Else
                    Me.Adapter.SelectCommand.Parameters(6).Value = Global.System.DBNull.Value
                End If

                Dim dataTable As SAPReports.GoodsIssuanceReportDataTable = New SAPReports.GoodsIssuanceReportDataTable
                Me.Adapter.Fill(dataTable)
                Return dataTable
            Catch ex As Exception
                Return Nothing
            Finally
                Me.CommandCollection(2).CommandText = stSelect
            End Try
        End Function

        Public Sub FixParametersDataType()
            For Each Command As OleDb.OleDbCommand In Me.CommandCollection
                For Each param As OleDb.OleDbParameter In Command.Parameters
                    If param.OleDbType = OleDb.OleDbType.WChar Then
                        param.OleDbType = OleDb.OleDbType.VarWChar
                    End If
                Next
            Next
        End Sub
    End Class

    Partial Public Class ReversalReportTableAdapter
        Inherits Global.System.ComponentModel.Component

        Public Sub FixParametersDataType()
            For Each Command As OleDb.OleDbCommand In Me.CommandCollection
                For Each param As OleDb.OleDbParameter In Command.Parameters
                    If param.OleDbType = OleDb.OleDbType.WChar Then
                        param.OleDbType = OleDb.OleDbType.VarWChar
                    End If
                Next
            Next
        End Sub
    End Class

    Partial Public Class VendorReturnReportTableAdapter
        Inherits Global.System.ComponentModel.Component

        Public Sub FixParametersDataType()
          
            For Each Command As OleDb.OleDbCommand In Me.CommandCollection
                For Each param As OleDb.OleDbParameter In Command.Parameters
                    If param.OleDbType = OleDb.OleDbType.WChar Then
                        param.OleDbType = OleDb.OleDbType.VarWChar
                    End If
                Next
            Next
        End Sub
    End Class

    Partial Public Class WarrantyClaimReportTableAdapter
        Inherits Global.System.ComponentModel.Component

        Public Sub FixParametersDataType()
            If (Me.CommandCollection Is Nothing) Then
                Me.InitCommandCollection()
            End If
            For Each Command As OleDb.OleDbCommand In Me.CommandCollection
                For Each param As OleDb.OleDbParameter In Command.Parameters
                    If param.OleDbType = OleDb.OleDbType.WChar Then
                        param.OleDbType = OleDb.OleDbType.VarWChar
                    End If
                Next
            Next
        End Sub
    End Class


    Partial Public Class WarrantyReceiveSameTableAdapter
        Inherits Global.System.ComponentModel.Component

        Public Sub FixParametersDataType()
            If (Me.CommandCollection Is Nothing) Then
                Me.InitCommandCollection()
            End If
            For Each Command As OleDb.OleDbCommand In Me.CommandCollection
                For Each param As OleDb.OleDbParameter In Command.Parameters
                    If param.OleDbType = OleDb.OleDbType.WChar Then
                        param.OleDbType = OleDb.OleDbType.VarWChar
                    End If
                Next
            Next
        End Sub
    End Class


    Partial Public Class WarrantyReceiveReplaceTableAdapter
        Inherits Global.System.ComponentModel.Component

        Public Sub FixParametersDataType()
            If (Me.CommandCollection Is Nothing) Then
                Me.InitCommandCollection()
            End If
            For Each Command As OleDb.OleDbCommand In Me.CommandCollection
                For Each param As OleDb.OleDbParameter In Command.Parameters
                    If param.OleDbType = OleDb.OleDbType.WChar Then
                        param.OleDbType = OleDb.OleDbType.VarWChar
                    End If
                Next
            Next
        End Sub
    End Class

End Namespace

Partial Class SAPReports
End Class
'Public Property SelectCommand() As OleDb.OleDbCommand()

'    Get
'        If (Me._commandCollection Is Nothing) Then
'            Me.InitCommandCollection()
'        End If
'        Return Me._commandCollection
'    End Get

'    Set(ByVal value As OleDb.OleDbCommand())
'        Me._commandCollection = value
'    End Set

'End Property

'Public Function FillByWhere(ByVal dataTable As SAPReports.GoodsReceivingReportDataTable, ByVal WhereExp As String) As Integer
'    Dim stSelect As String

'    stSelect = Me._commandCollection(0).CommandText
'    Try
'        Me._commandCollection(0).CommandText += " WHERE " + WhereExp
'        Return Me.Fill(dataTable)
'    Catch ex As Exception

'    Finally
'        Me._commandCollection(0).CommandText = stSelect
'    End Try

'End Function