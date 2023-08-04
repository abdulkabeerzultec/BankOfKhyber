
Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALTransDataTemp
        Public Sub Export_Data_To_Temp(ByVal iDeviceID As Integer, ByVal LocID As String, ByVal IsMultipleLocations As Boolean, ByVal Selection() As Boolean, ByVal BusinessArea As String, ByVal Categories As String, ByVal EvaluationGroup1 As String, ByVal CostCenter As String)
            Try
                Dim objTransDataTemp As New TransDataTemp
                objTransDataTemp.Export_Data_To_Temp(iDeviceID, LocID, IsMultipleLocations, Selection, BusinessArea, Categories, EvaluationGroup1, CostCenter)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function GetAll_AssetsTemp(ByVal iDeviceID As Integer) As DataTable
            Try
                Dim objTransDataTemp As New TransDataTemp
                Return objTransDataTemp.GetAll_AssetsTemp(iDeviceID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_AssetsTempGrid() As DataTable
            Try
                Dim objTransDataTemp As New TransDataTemp
                Return objTransDataTemp.GetAll_AssetsTempGrid()
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub RefereshTables(ByVal iDeviceID As Integer)
            Try
                Dim objTransDataTemp As New TransDataTemp
                objTransDataTemp.RefereshTables(iDeviceID)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function Delete_AssetTransferTemp(ByVal AstTransID As String, ByVal DeviceID As String) As Boolean
            Try
                Dim objTransDataTemp As New TransDataTemp
                If objTransDataTemp.DeleteAssetTransferTemp(AstTransID, DeviceID) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function Delete_AssetTemp(ByVal astID As String, ByVal DeviceID As String) As Boolean
            Try
                Dim objTransDataTemp As New TransDataTemp
                If objTransDataTemp.DeleteAssetsTempReceiving(astID, DeviceID) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_AssetTransferTemp(ByVal strAstID As String) As DataTable
            Try
                Dim objTransDataTemp As New TransDataTemp
                Return objTransDataTemp.GetAll_AssetTransferTemp(strAstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class
End Namespace

