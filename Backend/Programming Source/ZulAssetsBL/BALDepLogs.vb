
Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL



Namespace ZulAssetBAL
    Public Class BALDepLogs

        Inherits BLBase

#Region "Data Members"
        Private objDepLogs As DepLogs
#End Region


        Public Sub New()
            objDepLogs = New DepLogs
        End Sub

#Region "Functions"
        Public Function GetAllData_GetCombo() As DataTable
            Try
                Return Me.GeneralExecuter(objDepLogs.GetAllData_Combo())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Sub Insert_DepLogs(ByVal objattDepLogs As attDepLogs)
            Try
                objDepLogs.Attribute = objattDepLogs
                Me.Insert(objDepLogs)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub


        Public Function Update_DepLogs(ByVal objattDepLogs As attDepLogs) As Boolean
            Try
                objDepLogs.Attribute = objattDepLogs
                Me.Update(objDepLogs)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_DepLogs(ByVal objattDepLogs As attDepLogs) As DataTable
            Try
                objDepLogs.Attribute = objattDepLogs
                Return GetAllData(objDepLogs)

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetAll_DepLogs_Date(ByVal objattDepLogs As attDepLogs) As DataTable
            Try
                objDepLogs.Attribute = objattDepLogs
                Return Me.GeneralExecuter(objDepLogs.GetAll_DepLogs_Date(objattDepLogs))

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetNextPKey_DepLogs() As String
            Try
                Return Me.GetNextPKey(objDepLogs)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region
    End Class

End Namespace
