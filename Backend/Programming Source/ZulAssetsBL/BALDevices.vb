
Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALDevices
        Inherits BLBase

#Region "Data Members"
        Private objDevices As Devices
#End Region


        Public Sub New()
            objDevices = New Devices
        End Sub

#Region "Functions"
        Public Function Insert_Devices(ByVal objattDevices As attDevices) As Boolean
            Try
                objDevices.Attribute = objattDevices
                Me.InsertTemp(objDevices)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetDeviceDescription(ByVal DeviceID As String) As String
            Try
                Dim dt As DataTable = Me.GeneralExecuterTemp(objDevices.GetDeviceDesc(DeviceID))
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0)(0).ToString
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CheckID(ByVal objattDevices As attDevices) As DataTable
            Try
                objDevices.Attribute = objattDevices
                Return Me.GeneralExecuterTemp(objDevices.CheckID(objattDevices))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Update_Devices(ByVal objattDevices As attDevices) As Boolean
            Try
                objDevices.Attribute = objattDevices
                Me.UpdateTemp(objDevices)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_Devices(ByVal objattDevices As attDevices) As DataTable
            Try
                objDevices.Attribute = objattDevices
                Return GetAllDataTemp(objDevices)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Delete_Devices(ByVal objattDevices As attDevices) As Boolean
            Try
                objDevices.Attribute = objattDevices
                Me.DeleteTemp(objDevices)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTempDevicesMessages() As DataTable
            Try
                Return Me.GeneralExecuterTemp(objDevices.GetTempMessages)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function UpdateTempDevicesMessages(ByVal DeviceID As String) As DataTable
            Try
                Return Me.GeneralExecuterTemp(objDevices.UpdateTempMessages(DeviceID))
            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region
    End Class

End Namespace