
Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL

    Public Class BALDepreciationConfig
        Inherits BLBase
#Region "Data Members"
        Private objDepreciationConfig As DepreciationConfig
#End Region


        Public Sub New()
            objDepreciationConfig = New DepreciationConfig
        End Sub

#Region "Functions"
        Public Sub Insert_DepreciationConfig(ByVal objattDepreciationConfig As attDepreciationConfig)
            Try
                objDepreciationConfig.Attribute = objattDepreciationConfig
                Me.Insert(objDepreciationConfig)

            Catch ex As Exception
                Throw ex
            End Try

        End Sub


        Public Function Update_DepreciationConfig(ByVal objattDepreciationConfig As attDepreciationConfig) As Boolean
            Try
                objDepreciationConfig.Attribute = objattDepreciationConfig
                Me.Update(objDepreciationConfig)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_DepreciationConfig() As String
            Try
                Return Me.GetNextPKey(objDepreciationConfig)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetAll_DepreciationConfig(ByVal objattDepreciationConfig As attDepreciationConfig) As DataTable
            Try
                objDepreciationConfig.Attribute = objattDepreciationConfig
                Return GetAllData(objDepreciationConfig)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

#End Region
    End Class

End Namespace