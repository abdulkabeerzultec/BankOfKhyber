Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL

Public Class BALDisposalMethod
        Inherits BLBase
#Region "Data Members"
        Private objDisposalMethod As DisposalMethod
#End Region


        Public Sub New()
            objDisposalMethod = New DisposalMethod
        End Sub

#Region "Functions"
        Public Function Insert_DisposalMethod(ByVal objattDisposalMethod As attDisposalMethod) As String
            Try
                objDisposalMethod.Attribute = objattDisposalMethod
                Me.Insert(objDisposalMethod)
                Return Me.GetNextPKey(objDisposalMethod)
            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Public Function Update_DisposalMethod(ByVal objattDisposalMethod As attDisposalMethod) As Boolean
            Try
                objDisposalMethod.Attribute = objattDisposalMethod
                Me.Update(objDisposalMethod)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_DisposalMethod() As String
            Try
                Return Me.GetNextPKey(objDisposalMethod)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetAll_DisposalMethod(ByVal objattDisposalMethod As attDisposalMethod) As DataTable
            Try
                objDisposalMethod.Attribute = objattDisposalMethod
                Return GetAllData(objDisposalMethod)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Delete_DisposalMethod(ByVal objattDisposalMethod As attDisposalMethod) As Boolean
            Try
                objDisposalMethod.Attribute = objattDisposalMethod
                Me.Delete(objDisposalMethod)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Sub CreateDefaultRecord()
            Dim ds As DataTable
            Dim deletenw As Boolean = False
            Dim objatt As New attDisposalMethod
            ds = GetAll_DisposalMethod(objatt)
            If ds Is Nothing = False Then
                ' If ds Is Nothing = False Then
                If ds.Rows.Count < 3 Then
                    If deletenw = False Then
                        Delete_DisposalMethod(objatt)
                        deletenw = True
                    End If
                    objatt = New attDisposalMethod
                    objatt.PKeyCode = "1"
                    objatt.DispDesc = "Sold"
                    Insert_DisposalMethod(objatt)

                    objatt = New attDisposalMethod
                    objatt.PKeyCode = "2"
                    objatt.DispDesc = "Disposed"
                    Insert_DisposalMethod(objatt)

                    objatt = New attDisposalMethod
                    objatt.PKeyCode = "3"
                    objatt.DispDesc = "InerCompany Transfer"
                    Insert_DisposalMethod(objatt)

                    'End If
                End If
            End If

        End Sub

        Public Function DispDescExist(ByVal objatt As attDisposalMethod, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("DispDesc", objatt.DispDesc, "Disposal_Method", IsInsertStatus, "DispCode", objatt.PKeyCode)
        End Function

#End Region
End Class

End Namespace