Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL

    Public Class BALBookHistory
        Inherits BLBase

#Region "Data Members"
        Private objBookHistory As BookHistory
#End Region


        Public Sub New()
            objBookHistory = New BookHistory
        End Sub

#Region "Functions"
    
        Public Function Insert_BookHistory(ByVal objattBookHistory As attBookHistory) As String
            Try


                objBookHistory.Attribute = objattBookHistory
                Me.Insert(objBookHistory)


            Catch ex As Exception
                Throw ex
            End Try
            Return ""
        End Function


        Public Function Update_BookHistory(ByVal objattBookHistory As attBookHistory) As Boolean
            Try
                objBookHistory.Attribute = objattBookHistory
                Me.Update(objBookHistory)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Delete_BookHistory(ByVal objattBookHistory As attBookHistory) As Boolean
            Try
                objBookHistory.Attribute = objattBookHistory
                Me.Delete(objBookHistory)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_BookHistory() As String
            Try
                Return Me.GetNextPKey(objBookHistory)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAll_BookHistory(ByVal objattBookHistory As attBookHistory) As DataTable
            Try
                objattBookHistory.PKeyCode = GetNextPKey_BookHistory()
                objBookHistory.Attribute = objattBookHistory
                Return GetAllData(objBookHistory)
            Catch ex As Exception
                Throw ex
            End Try


        End Function

#End Region
    End Class
End Namespace