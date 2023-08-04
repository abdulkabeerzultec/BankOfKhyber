Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALCustomFields
        Inherits BLBase

#Region "Data Members"
        Private objCustomFields As CustomFields
#End Region


        Public Sub New()
            objCustomFields = New CustomFields
        End Sub

#Region "Functions"
        Public Function Insert_CustomFields(ByVal objattCustomFields As attCustomFields) As String
            Try
                objCustomFields.Attribute = objattCustomFields
                Me.Insert(objCustomFields)
                Return Me.GetNextPKey_CustomFields()
            Catch ex As Exception
                Throw ex
            End Try
            Return ""
        End Function


        Public Function Update_CustomFields(ByVal objattCustomFields As attCustomFields) As Boolean
            Try
                objCustomFields.Attribute = objattCustomFields
                Me.Update(objCustomFields)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetNextPKey_CustomFields() As String
            Try
                Return Me.GetNextPKey(objCustomFields)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAll_CustomFields(ByVal objattCustomFields As attCustomFields) As DataTable
            Try
                objCustomFields.Attribute = objattCustomFields
                Return GetAllData(objCustomFields)

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetFieldCaption(ByVal ControlName As String) As String
            Try
                Return Me.GeneralExecuter_Scalar(objCustomFields.GetFieldCaption(ControlName), "")

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Delete_CustomFields(ByVal objattCustomFields As attCustomFields) As Boolean
            Try
                objCustomFields.Attribute = objattCustomFields
                Me.Delete(objCustomFields)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
    End Class

End Namespace