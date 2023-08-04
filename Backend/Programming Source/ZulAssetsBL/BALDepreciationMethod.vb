Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALDepreciationMethod
        Inherits BLBase
#Region "Data Members"
        Private objDepreciationMethod As DepreciationMethod
#End Region


        Public Sub New()
            objDepreciationMethod = New DepreciationMethod
        End Sub

#Region "Functions"
        Public Function Insert_DepreciationMethod(ByVal objattDepreciationMethod As attDepreciationMethod) As String
            Try
                objDepreciationMethod.Attribute = objattDepreciationMethod
                Me.Insert(objDepreciationMethod)
                Return Me.GetNextPKey(objDepreciationMethod)
            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Public Function Update_DepreciationMethod(ByVal objattDepreciationMethod As attDepreciationMethod) As Boolean
            Try
                objDepreciationMethod.Attribute = objattDepreciationMethod
                Me.Update(objDepreciationMethod)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_DepreciationMethod() As String
            Try
                Return Me.GetNextPKey(objDepreciationMethod)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetAll_DepreciationMethod(ByVal objattDepreciationMethod As attDepreciationMethod) As DataTable
            Try
                objDepreciationMethod.Attribute = objattDepreciationMethod
                Return GetAllData(objDepreciationMethod)
            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Public Function Delete_DepreciationMethod(ByVal objattDepreciationMethod As attDepreciationMethod) As Boolean
            Try
                objDepreciationMethod.Attribute = objattDepreciationMethod
                Me.Delete(objDepreciationMethod)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Sub CreateDefaultRecord()
            Dim ds As DataTable
            Dim deletenw As Boolean = False
            Dim objatt As New attDepreciationMethod
            ds = GetAll_DepreciationMethod(objatt)
            If ds Is Nothing = False Then
                If ds Is Nothing = False Then
                    If ds.Rows.Count < 5 Then
                        If deletenw = False Then
                            Delete_DepreciationMethod(objatt)
                            deletenw = True
                        End If
                        objatt = New attDepreciationMethod
                        objatt.PKeyCode = "1"
                        objatt.DepDesc = "Straight Line Method"
                        Insert_DepreciationMethod(objatt)

                        objatt = New attDepreciationMethod
                        objatt.PKeyCode = "2"
                        objatt.DepDesc = "Sum of years"
                        Insert_DepreciationMethod(objatt)

                        objatt = New attDepreciationMethod
                        objatt.PKeyCode = "3"
                        objatt.DepDesc = "Double Declining"
                        Insert_DepreciationMethod(objatt)

                        objatt = New attDepreciationMethod
                        objatt.PKeyCode = "4"
                        objatt.DepDesc = "150 % Double Declining"
                        Insert_DepreciationMethod(objatt)

                        objatt = New attDepreciationMethod
                        objatt.PKeyCode = "5"
                        objatt.DepDesc = "175 % Double Declining"
                        Insert_DepreciationMethod(objatt)

                        objatt = New attDepreciationMethod
                        objatt.PKeyCode = "6"
                        objatt.DepDesc = "200 % Double Declining"
                        Insert_DepreciationMethod(objatt)

                    End If
                End If
            End If

        End Sub

#End Region
    End Class

End Namespace