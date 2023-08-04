Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALAddCostType
        Inherits BLBase

#Region "Data Members"
        Private objAddCostType As AddCostType
#End Region
        Public Sub New()
            objAddCostType = New AddCostType
        End Sub

#Region "Functions"
        Public Function Insert_AddCostType(ByVal objattAddCostType As attAddCostType) As String
            Try
                objAddCostType.Attribute = objattAddCostType
                Me.Insert(objAddCostType)
                Return Me.GetNextPKey_AddCostType()
            Catch ex As Exception
                Throw ex
            End Try
            Return ""
        End Function


        Public Function Update_AddCostType(ByVal objattAddCostType As attAddCostType) As Boolean
            Try
                objAddCostType.Attribute = objattAddCostType
                Me.Update(objAddCostType)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_AddCostType() As String
            Try
                Return Me.GetNextPKey(objAddCostType)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAll_AddCostType(ByVal objattAddCostType As attAddCostType) As DataTable
            Try
                objAddCostType.Attribute = objattAddCostType
                Return GetAllData(objAddCostType)

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function Delete_AddCostType(ByVal objattAddCostType As attAddCostType) As Boolean
            Try
                objAddCostType.Attribute = objattAddCostType
                Me.Delete(objAddCostType)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetDatabyTypeDesc(ByVal name As String) As Integer
            Try
                Dim dt As DataTable = Me.GeneralExecuter(objAddCostType.GetDatabyName(name))
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        If Not IsDBNull(dt.Rows(0)("TypeID")) Then
                            Return dt.Rows(0)("TypeID").ToString
                        Else
                            Return 0
                        End If
                    Else
                        Return 0
                    End If
                Else
                    Return 0
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
    End Class

End Namespace
