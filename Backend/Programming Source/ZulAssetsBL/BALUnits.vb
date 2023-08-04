
Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALUnits
        Inherits BLBase

#Region "Data Members"
        Private objUnits As Units
#End Region


        Public Sub New()
            objUnits = New Units
        End Sub

#Region "Functions"
        Public Function Check_Child(ByVal _id As String, ByVal formid As Integer) As DataTable
            Try

                Return Me.GeneralExecuter(objUnits.Check_UnitChilds(_id, formid))
            Catch ex As Exception
                Throw ex
            End Try


        End Function
        Public Function Insert_Units(ByVal objattUnits As attUnits) As String
            Try
                objUnits.Attribute = objattUnits
                Me.Insert(objUnits)
                Return Me.GetNextPKey_Units()
            Catch ex As Exception
                Throw ex
            End Try
            Return ""
        End Function


        Public Function Update_Units(ByVal objattUnits As attUnits) As Boolean
            Try
                objUnits.Attribute = objattUnits
                Me.Update(objUnits)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_Units() As String
            Try
                Return Me.GetNextPKey(objUnits)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAll_Units(ByVal objattUnits As attUnits) As DataTable
            Try
                objUnits.Attribute = objattUnits
                Return GetAllData(objUnits)

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function Delete_Units(ByVal objattUnits As attUnits) As Boolean
            Try
                objUnits.Attribute = objattUnits
                Me.Delete(objUnits)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UnitDescExist(ByVal objatt As attUnits, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("UnitDesc", objatt.UnitDesc, "Units", IsInsertStatus, "UnitID", objatt.PKeyCode)
        End Function

#End Region
    End Class

End Namespace
