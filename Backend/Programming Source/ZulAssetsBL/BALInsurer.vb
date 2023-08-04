Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALInsurer
        Inherits BLBase
#Region "Data Members"
        Private objInsurer As Insurer
#End Region


        Public Sub New()
            objInsurer = New Insurer
        End Sub

#Region "Functions"
        Public Function Insert_Insurer(ByVal objattInsurer As attInsurer) As Boolean
            Try
                objInsurer.Attribute = objattInsurer
                Me.Insert(objInsurer)
                Return True
            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Public Function Update_Insurer(ByVal objattInsurer As attInsurer) As Boolean
            Try
                objInsurer.Attribute = objattInsurer
                Me.Update(objInsurer)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_Insurer() As String
            Try
                Return Me.GetNextPKey(objInsurer)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetAll_Insurer(ByVal objattInsurer As attInsurer) As DataTable
            Try
                objInsurer.Attribute = objattInsurer
                Return GetAllData(objInsurer)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function Delete_Insurer(ByVal objattInsurer As attInsurer) As Boolean
            Try
                objInsurer.Attribute = objattInsurer
                Me.Delete(objInsurer)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsNameExist(ByVal objatt As attInsurer, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("InsName", objatt.InsName, "Insurer", IsInsertStatus, "InsCode", objatt.PKeyCode)
        End Function

#End Region
    End Class

End Namespace