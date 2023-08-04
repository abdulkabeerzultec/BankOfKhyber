Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALDepPolicy_History
        Inherits BLBase

#Region "Data Members"
        Private objDepPolicy_History As DepPolicy_History
#End Region

        Public Sub New()
            objDepPolicy_History = New DepPolicy_History
        End Sub

#Region "Functions"
        Public Function Check_Child(ByVal _id As String, ByVal formid As Integer) As DataTable
            Try

                Return Me.GeneralExecuter(objDepPolicy_History.Check_Child_DepPolicy_History(_id, formid))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Insert_DepPolicy_History(ByVal objattDepPolicy_History As attDepPolicy_History) As String
            Try
                objDepPolicy_History.Attribute = objattDepPolicy_History
                Me.Insert(objDepPolicy_History)
            Catch ex As Exception
                Throw ex
            End Try
            Return ""
        End Function

        Public Function CheckID_DepPolicy_History(ByVal objattDepPolicy_History As attDepPolicy_History) As DataTable
            Try
                objDepPolicy_History.Attribute = objattDepPolicy_History
                Return Me.GeneralExecuter(objDepPolicy_History.CheckID())

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Get_Latest_History(ByVal BookId As String, ByVal AstId As String, ByVal Lastupdate As Date, ByVal ThisUpdate As Date) As DataTable
            Try
                Dim ds As DataTable
                ds = Me.GeneralExecuter(objDepPolicy_History.Get_Latest_History(BookId, AstId, Lastupdate, ThisUpdate))
                If Not ds Is Nothing Then
                    If Not ds Is Nothing Then
                        If ds.Rows.Count > 0 Then
                            Return ds
                        End If
                    End If
                End If
                Return Nothing
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Check_DepPolicy_History(ByVal BookId As String, ByVal AstId As String, ByVal Lastupdate As Date, ByVal ThisUpdate As Date) As Boolean
            Try
                Dim ds As DataTable
                ds = Me.GeneralExecuter(objDepPolicy_History.CheckHistory(BookId, AstId, Lastupdate, ThisUpdate))
                If Not ds Is Nothing Then
                    '   If Not ds Is Nothing Then
                    If ds.Rows.Count > 0 Then
                        Return True
                    End If
                    'End If
                End If
                Return False
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Update_DepPolicy_History(ByVal objattDepPolicy_History As attDepPolicy_History) As Boolean
            Try
                objDepPolicy_History.Attribute = objattDepPolicy_History
                Me.Update(objDepPolicy_History)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        'Public Function GetNextPKey_DepPolicy_History(ByVal _id As String) As String
        '    Try
        '        Dim str As String
        '        Dim key() As String
        '        Dim dt As DataTable

        '        dt = Me.GeneralExecuter(objDepPolicy_History.GetNextPKey(_id))
        '        If dt.Rows.Count > 0 Then

        '        End If
        '        key = str.Split("-")
        '        If key.Length > 0 Then
        '            Dim cnt As Integer = key.Length
        '            str = key(cnt - 1)
        '            str = CInt(str) + 1
        '        Else
        '            str = 0
        '        End If
        '        Return str
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        'End Function

        Public Function GetAll_DepPolicy_History(ByVal objattDepPolicy_History As attDepPolicy_History) As DataTable
            Try
                objDepPolicy_History.Attribute = objattDepPolicy_History
                Return GetAllData(objDepPolicy_History)

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAllData_Detail(ByVal objattDepPolicy_History As attDepPolicy_History) As DataTable
            Try
                objDepPolicy_History.Attribute = objattDepPolicy_History
                Return Me.GeneralExecuter(objDepPolicy_History.GetAllData_Detail())

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function MarkAssetDisposed(ByVal LastBV As String, ByVal BookID As String, ByVal AstID As String) As Boolean
            Try

                Me.TransactionExecuter(objDepPolicy_History.MarkAssetDisposed(LastBV, BookID, AstID))
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Delete_DepPolicy_History(ByVal objattDepPolicy_History As attDepPolicy_History) As Boolean
            Try
                objDepPolicy_History.Attribute = objattDepPolicy_History
                Me.Delete(objDepPolicy_History)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
    End Class
End Namespace