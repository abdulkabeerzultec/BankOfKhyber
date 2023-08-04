Public Class SAPReportsBLL

    Private Function Val(ByVal value As Object) As Object
        'Check the values if it's empty then but nothing, to let the query get the values.
        If String.IsNullOrEmpty(value) Then
            Return Nothing
        Else
            Return value
        End If
    End Function

    Public Function GetSAPGoodsReceiveReport(ByVal POFrom As String, ByVal POTo As String, ByVal SerialNo As String, ByVal ManPartNo As String, ByVal SAPMatNo As String, ByVal FilterByDate As Boolean, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim objReport As New ZulAssetsDAL.SAPReportsTableAdapters.GoodsReceivingReportTableAdapter
        objReport.FixParametersDataType()

        Try
            If Not String.IsNullOrEmpty(POFrom) And Not String.IsNullOrEmpty(POTo) Then
                If FilterByDate Then
                    Return objReport.GetDataByParamsAndDate(Val(POFrom), Val(POTo), Val(SerialNo), Val(ManPartNo), Val(SAPMatNo), FromDate.Date, ToDate.Date)
                Else
                    Return objReport.GetDataByParams(Val(POFrom), Val(POTo), Val(SerialNo), Val(ManPartNo), Val(SAPMatNo))
                End If
            Else
                If FilterByDate Then
                    Return objReport.GetDataByPOAndDate(Val(POFrom), Val(SerialNo), Val(ManPartNo), Val(SAPMatNo), FromDate.Date, ToDate.Date)
                Else
                    Return objReport.GetDataByPo(Val(POFrom), Val(SerialNo), Val(ManPartNo), Val(SAPMatNo))
                End If
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetGoodsIssuanceABBReport(ByVal SerialNo As String, ByVal ManPartNo As String, ByVal SAPMatNo As String, ByVal AssetNumber As String, ByVal FilterByDate As Boolean, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IssueType As String, ByVal SortExp As String) As DataTable
        Dim objReport As New ZulAssetsDAL.SAPReportsTableAdapters.GoodsIssuanceReportTableAdapter
        objReport.FixParametersDataType()
        Try
            'The Query below written in SAPReports.vb file for SAPReports.xsd file.
            Dim dt As New DataTable
            If FilterByDate Then
                dt = objReport.GetDataByParamsAndDateSorted(Val(SerialNo), Val(ManPartNo), Val(SAPMatNo), Val(AssetNumber), Val(IssueType), FromDate.Date, ToDate.Date, SortExp)
            Else
                dt = objReport.GetDataByParamsSorted(Val(SerialNo), Val(ManPartNo), Val(SAPMatNo), Val(AssetNumber), Val(IssueType), SortExp)
            End If

            'we need the asset life only if issueType is not selected-> rptGoodsIssuanceDetails report only.
            If String.IsNullOrEmpty(IssueType) Then
                dt.Columns.Add("AssetLife", Type.GetType("System.String"))
                Dim objBALStandardReport As New ZulAssetBAL.BALABBIntegration
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim AssetNo As String = dt.Rows(i)("AssetNo").ToString
                    If Not String.IsNullOrEmpty(AssetNo) Then
                        dt.Rows(i)("AssetLife") = objBALStandardReport.GetAssetLifeByRefNumber(AssetNo)
                    End If
                Next
            End If

            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetReversalReport(ByVal SerialNo As String, ByVal ManPartNo As String, ByVal SAPMatNo As String, ByVal DocumentNo As String, ByVal FilterByDate As Boolean, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim objReport As New ZulAssetsDAL.SAPReportsTableAdapters.ReversalReportTableAdapter
        objReport.FixParametersDataType()
        Try
            If FilterByDate Then
                Return objReport.GetDataByParamsAndDate(Val(SerialNo), Val(ManPartNo), Val(SAPMatNo), Val(DocumentNo), FromDate.Date, ToDate.Date)
            Else
                Return objReport.GetDataByParams(Val(SerialNo), Val(ManPartNo), Val(SAPMatNo), Val(DocumentNo))
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function GetVendorReturnReport(ByVal SerialNo As String, ByVal ManPartNo As String, ByVal SAPMatNo As String, ByVal DocumentNo As String, ByVal FilterByDate As Boolean, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim objReport As New ZulAssetsDAL.SAPReportsTableAdapters.VendorReturnReportTableAdapter
        objReport.FixParametersDataType()
        Try
            If FilterByDate Then
                Return objReport.GetDataByParamsAndDate(Val(SerialNo), Val(ManPartNo), Val(SAPMatNo), Val(DocumentNo), FromDate.Date, ToDate.Date)
            Else
                Return objReport.GetDataByParams(Val(SerialNo), Val(ManPartNo), Val(SAPMatNo), Val(DocumentNo))
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function GetWarrantyClaimReport(ByVal SerialNo As String, ByVal ManPartNo As String, ByVal SAPMatNo As String, ByVal DocumentNo As String, ByVal FilterByDate As Boolean, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim objReport As New ZulAssetsDAL.SAPReportsTableAdapters.WarrantyClaimReportTableAdapter
        objReport.FixParametersDataType()
        Try
            If FilterByDate Then
                Return objReport.GetDataByParamsAndDate(Val(SerialNo), Val(ManPartNo), Val(SAPMatNo), Val(DocumentNo), FromDate.Date, ToDate.Date)
            Else
                Return objReport.GetDataByParams(Val(SerialNo), Val(ManPartNo), Val(SAPMatNo), Val(DocumentNo))
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetWarrantyReceiveSameReport(ByVal SerialNo As String, ByVal ManPartNo As String, ByVal SAPMatNo As String, ByVal DocumentNo As String, ByVal FilterByDate As Boolean, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim objReport As New ZulAssetsDAL.SAPReportsTableAdapters.WarrantyReceiveSameTableAdapter
        objReport.FixParametersDataType()
        Try
            If FilterByDate Then
                Return objReport.GetDataByParamsAndDate(Val(SerialNo), Val(ManPartNo), Val(SAPMatNo), Val(DocumentNo), FromDate.Date, ToDate.Date)
            Else
                Return objReport.GetDataByParams(Val(SerialNo), Val(ManPartNo), Val(SAPMatNo), Val(DocumentNo))
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function GetWarrantyReceiveReplaceReport(ByVal SerialNo As String, ByVal ManPartNo As String, ByVal SAPMatNo As String, ByVal DocumentNo As String, ByVal FilterByDate As Boolean, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim objReport As New ZulAssetsDAL.SAPReportsTableAdapters.WarrantyReceiveReplaceTableAdapter
        objReport.FixParametersDataType()
        Try
            If FilterByDate Then
                Return objReport.GetDataByParamsAndDate(Val(SerialNo), Val(ManPartNo), Val(SAPMatNo), Val(DocumentNo), FromDate.Date, ToDate.Date)
            Else
                Return objReport.GetDataByParams(Val(SerialNo), Val(ManPartNo), Val(SAPMatNo), Val(DocumentNo))
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
