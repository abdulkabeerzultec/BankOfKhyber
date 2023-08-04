Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALDesignation
        Inherits BLBase
#Region "Data Members"
        Private objDesignation As Designation
#End Region


        Public Sub New()
            objDesignation = New Designation
        End Sub

#Region "Functions"
        Public Function Insert_Designation(ByVal objattDesignation As attDesignation) As String
            Try
                objDesignation.Attribute = objattDesignation
                Me.Insert(objDesignation)
                Return Me.GetNextPKey(objDesignation)
            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Public Function Update_Designation(ByVal objattDesignation As attDesignation) As Boolean
            Try
                objDesignation.Attribute = objattDesignation
                Me.Update(objDesignation)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_Designation() As String
            Try
                Return Me.GetNextPKey(objDesignation)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetAll_Designation(ByVal objattDesignation As attDesignation) As DataTable
            Try
                objDesignation.Attribute = objattDesignation

                Return GetAllData(objDesignation)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Delete_Designation(ByVal objattDesignation As attDesignation) As Boolean
            Try
                objDesignation.Attribute = objattDesignation
                Me.Delete(objDesignation)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetDesignationID(ByVal objattDesignation As attDesignation) As DataTable
            Try
                objDesignation.Attribute = objattDesignation
                Return Me.GeneralExecuter(objDesignation.GetDesignationID())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function DesignationDescExist(ByVal objatt As attDesignation, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("Description", objatt.Description, "Designation", IsInsertStatus, "DesignationID", objatt.PKeyCode)
        End Function

#End Region
    End Class

End Namespace