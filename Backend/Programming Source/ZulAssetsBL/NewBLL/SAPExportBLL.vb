Public Class SAPExportBLL
    Public Function GetGoodsReceivingExport(Optional ByRef msg As String = "") As DataTable
        Dim objExport As New ZulAssetsDAL.SAPExportTableAdapters.GoodsReceivingExportTableAdapter
        Try
            Return objExport.GetData
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetGoodsIssuingExport(Optional ByRef msg As String = "") As DataTable
        Dim objExport As New ZulAssetsDAL.SAPExportTableAdapters.GoodsIssuingExportTableAdapter
        Try
            Return objExport.GetData
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetReversalExportGR(Optional ByRef msg As String = "") As DataTable
        Dim objExport As New ZulAssetsDAL.SAPExportTableAdapters.ReversalExportGRTableAdapter
        Try
            Return objExport.GetData
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetReversalExportGI(Optional ByRef msg As String = "") As DataTable
        Dim objExport As New ZulAssetsDAL.SAPExportTableAdapters.ReversalExportGITableAdapter
        Try
            Return objExport.GetData
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function
    Public Function GetVendorReturnExport(Optional ByRef msg As String = "") As DataTable
        Dim objExport As New ZulAssetsDAL.SAPExportTableAdapters.VendorReturnExportTableAdapter
        Try
            Return objExport.GetData
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function UpdateExportFlagByDocType(ByVal DocType As String) As Boolean
        Dim objExport As New ZulAssetsDAL.SAPExportTableAdapters.QueriesTableAdapter
        Try
            objExport.SetExportFlag(DocType)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class
