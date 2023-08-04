Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALAstBooks
        Inherits BLBase

#Region "Data Members"
        Private objAstBooks As AstBooks
#End Region


        Public Sub New()
            objAstBooks = New AstBooks
        End Sub

#Region "Functions"
        Public Function Check_Child(ByVal _id As String, ByVal formid As Integer) As DataTable
            Try

                Return Me.GeneralExecuter(objAstBooks.Check_Child_AstBooks(_id, formid))
            Catch ex As Exception
                Throw ex
            End Try


        End Function
        Public Function GetAssetsCount(ByVal BookID As String) As String
            Try
                Return Me.GeneralExecuter_Scalar(objAstBooks.GetAssetsCount(BookID), "")
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function Insert_AstBooks(ByVal objattAstBooks As attAstBooks) As String
            Try
                objAstBooks.Attribute = objattAstBooks
                Me.Insert(objAstBooks)
            Catch ex As Exception
                Throw ex
            End Try
            Return ""
        End Function

        Public Function Insert_AstBooks_ExportServer(ByVal objattAstBook1 As attAstBooks, ByVal AssetRefNo As String, ByVal ServiceDate As Date, ByVal BaseCost As Decimal, ByVal AccDepValue As Decimal, ByVal DepValue As Decimal) As String
            Try
                Return GeneralExecuter_Scalar_ExportServer(objAstBooks.Insert_ExportServer(objattAstBook1, AssetRefNo, ServiceDate, BaseCost, AccDepValue, DepValue))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CheckID_AstBooks(ByVal objattAstBooks As attAstBooks) As DataTable
            Try
                objAstBooks.Attribute = objattAstBooks
                Return Me.GeneralExecuter(objAstBooks.CheckID())

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Update_AstBooks(ByVal objattAstBooks As attAstBooks) As Boolean
            Try
                objAstBooks.Attribute = objattAstBooks
                Me.Update(objAstBooks)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Update_AstBooks_ExportServer(ByVal objattAstBook1 As attAstBooks, ByVal AssetRefNo As String, ByVal ServiceDate As Date, ByVal BaseCost As Decimal, ByVal AccDepValue As Decimal, ByVal DepValue As Decimal) As String
            Try
                Return GeneralExecuter_Scalar_ExportServer(objAstBooks.Update_ExportServer(objattAstBook1, AssetRefNo, ServiceDate, BaseCost, AccDepValue, DepValue))
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function GetNextPKey_AstBooks(ByVal _id As String) As String
            Try
                Dim str As String = ""
                Dim key() As String
                Dim dt As DataTable

                dt = Me.GeneralExecuter(objAstBooks.GetNextPKey(_id))
                If dt.Rows.Count > 0 Then

                End If
                key = str.Split("-")
                If key.Length > 0 Then
                    Dim cnt As Integer = key.Length
                    str = key(cnt - 1)
                    str = CInt(str) + 1
                Else
                    str = 0
                End If
                Return str
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_AstBooks(ByVal objattAstBooks As attAstBooks) As DataTable
            Try
                objAstBooks.Attribute = objattAstBooks
                Return GetAllData(objAstBooks)

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAllData_Detail(ByVal objattAstBooks As attAstBooks) As DataTable
            Try
                objAstBooks.Attribute = objattAstBooks
                Return Me.GeneralExecuter(objAstBooks.GetAllData_Detail())

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function MarkAssetDisposed(ByVal LastBV As String, ByVal BookID As String, ByVal AstID As String) As Boolean
            Try

                Me.TransactionExecuter(objAstBooks.MarkAssetDisposed(LastBV, BookID, AstID))
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Delete_AstBooks(ByVal objattAstBooks As attAstBooks) As Boolean
            Try
                objAstBooks.Attribute = objattAstBooks
                Me.Delete(objAstBooks)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
    End Class

End Namespace