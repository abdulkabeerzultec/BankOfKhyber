Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL

    Public Class BALReports
        Inherits BLBase
#Region "Data Members"
        Private objRptFile As ReportsFiles
#End Region


        Public Sub New()
            objRptFile = New ReportsFiles
        End Sub

#Region "Functions"
        Public Function Insert_ReportFile(ByVal objattReportsFiles As attReports) As Boolean
            objRptFile.Attribute = objattReportsFiles
            Me.Insert(objRptFile)
            Return True
        End Function

        Public Function Update_ReportFile(ByVal objattReportsFiles As attReports) As Boolean
            Try
                objRptFile.Attribute = objattReportsFiles
                Me.Update(objRptFile)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function GetAll_ReportsFiles(ByVal objattReportsFiles As attReports) As DataTable
            objRptFile.Attribute = objattReportsFiles
            Return GetAllData(objRptFile)
        End Function

        Public Function GetReportFileData(ByVal ReportName As String) As DataTable
            Try
                Return Me.GeneralExecuter(objRptFile.GetDatabyID(ReportName))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function AddRoleReport(ByVal RoleID As String, ByVal ReportName As String) As DataTable
            Try
                Return Me.GeneralExecuter(objRptFile.AddRoleReportData(RoleID, ReportName))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteRoleReport(ByVal RoleID As String, ByVal ReportName As String) As DataTable
            Try
                Return Me.GeneralExecuter(objRptFile.DeleteRoleReportData(RoleID, ReportName))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CheckExistRoleReport(ByVal RoleID As String, ByVal ReportName As String) As String
            Try
                Return Me.GeneralExecuter_Scalar(objRptFile.CheckExistRoleReportData(RoleID, ReportName), "")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetRoleReports(ByVal RoleID As String) As DataTable
            Try
                Return Me.GeneralExecuter(objRptFile.GetRoleDatabyID(RoleID))
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function GetExtendedReports() As DataTable
            Try
                Return Me.GeneralExecuter(objRptFile.GetExtended)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function Delete_Report(ByVal objattReportsFiles As attReports) As Boolean
            Try
                objRptFile.Attribute = objattReportsFiles
                Me.Delete(objRptFile)
                Me.DeleteRoleReport("", objattReportsFiles.ReportName)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function ReportNameExist(ByVal objatt As attReports, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("ReportName", objatt.ReportName, "ReportsFiles", IsInsertStatus, "ReportName", objatt.ReportName)
        End Function
#End Region
    End Class

End Namespace

