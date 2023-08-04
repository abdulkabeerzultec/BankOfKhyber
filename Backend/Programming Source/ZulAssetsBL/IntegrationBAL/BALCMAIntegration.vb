Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL

Namespace ZulAssetBAL
    Public Class BALCMAIntegration
        Inherits BLBase
        Private objCMAQuery As New CMAIntegration
        Public Function GetAllData_CMAExportGrid(ByVal HideMissing As Boolean, ByVal InvSchCode As Integer) As DataTable
            Try
                Return Me.GeneralExecuter(objCMAQuery.GetAllDataCMAExport(HideMissing, InvSchCode))
            Catch ex As Exception
                Throw ex
            End Try
        End Function


    End Class
End Namespace
