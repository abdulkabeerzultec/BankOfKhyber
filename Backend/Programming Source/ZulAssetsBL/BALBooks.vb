
Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALBooks
        Inherits BLBase

#Region "Data Members"
        Private objBook As Book
#End Region


        Public Sub New()
            objBook = New Book
        End Sub

#Region "Functions"
        Public Function Insert_Book(ByVal objattBook As attBook) As String
            Try
                objBook.Attribute = objattBook
                Me.Insert(objBook)

                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function Update_Book(ByVal objattBook As attBook) As Boolean
            Try
                objBook.Attribute = objattBook
                Me.Update(objBook)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_Book() As String
            Try
                Return Me.GetNextPKey(objBook)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAll_Book(ByVal objattBook As attBook) As DataTable
            Try
                objBook.Attribute = objattBook
                Return GetAllData(objBook)

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAllCompanyBooksIDs(ByVal CompanyID As Integer) As DataTable
            Try
                Return Me.GeneralExecuter(objBook.GetCompanyBooksIDs(CompanyID))
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Delete_Book(ByVal objattBook As attBook) As Boolean
            Try
                objBook.Attribute = objattBook
                Me.Delete(objBook)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function



#End Region
    End Class

End Namespace