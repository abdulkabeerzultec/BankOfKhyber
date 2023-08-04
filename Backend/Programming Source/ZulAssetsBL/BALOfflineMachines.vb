Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL

Namespace ZulAssetBAL
    Public Class BALOfflineMachines
        Inherits BLBase
        Private objOfflineMachines As OfflineMachines
        Public Sub New()
            objOfflineMachines = New OfflineMachines
        End Sub
#Region "Functions"
        Public Function Insert_OfflineMachine(ByVal objAttOfflineMachines As AttOfflineMachines) As String
            Try
                objOfflineMachines.Attribute = objAttOfflineMachines
                Me.Insert(objOfflineMachines)
                Return Me.GetNextPKey_OfflineMachine()
            Catch ex As Exception
                Throw ex
            End Try
            Return ""
        End Function

        Public Function UpdateLastAssetNumber(ByVal AssetNumber As Int64) As Boolean
            Try
                Me.GeneralExecuter(objOfflineMachines.UpdateLastAssetNumber(AssetNumber))
                Return True
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function Update_OfflineMachine(ByVal objAttOfflineMachines As AttOfflineMachines) As Boolean
            Try
                objOfflineMachines.Attribute = objAttOfflineMachines
                Me.Update(objOfflineMachines)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_OfflineMachine() As String
            Try
                Return Me.GetNextPKey(objOfflineMachines)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAll_OfflineMachine(ByVal objAttOfflineMachines As AttOfflineMachines) As DataTable
            Try
                objOfflineMachines.Attribute = objAttOfflineMachines
                Return GetAllData(objOfflineMachines)

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function Delete_OfflineMachine(ByVal objAttOfflineMachines As AttOfflineMachines) As Boolean
            Try
                objOfflineMachines.Attribute = objAttOfflineMachines
                Me.Delete(objOfflineMachines)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function MachineDescExist(ByVal objatt As AttOfflineMachines, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("MachineDesc", objatt.MachineDesc, "OfflineMachines", IsInsertStatus, "MachineID", objatt.MachineID)
        End Function

        Public Function MachineServerNameExist(ByVal objatt As AttOfflineMachines, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("ServerName", objatt.ServerName, "OfflineMachines", IsInsertStatus, "MachineID", objatt.MachineID)
        End Function

#End Region
    End Class
End Namespace