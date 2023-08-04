
Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALBarCode_Struct
        Inherits BLBase

#Region "Data Members"
        Private objBarCode_Struct As BarCode_Struct
#End Region
        Public Function Check_Child(ByVal _id As String, ByVal formid As Integer) As DataTable
            Try

                Return Me.GeneralExecuter(objBarCode_Struct.Check_BarCode_Struct(_id, formid))
            Catch ex As Exception
                Throw ex
            End Try


        End Function

        Public Sub New()
            objBarCode_Struct = New BarCode_Struct
        End Sub

        Public Sub CreateDefaultRecord()
            Dim ds As DataTable
            Dim deletenw As Boolean = False
            Dim objatt As New attBarCode_Struct
            ds = GetAll_BarCode_Struct(objatt)
            If ds Is Nothing = False Then
                If ds Is Nothing = False Then
                    If ds.Rows.Count < 1 Then
                        If deletenw = False Then
                            Delete_BarCode_Struct(objatt)
                            deletenw = True
                        End If
                        objatt = New attBarCode_Struct
                        objatt.BarCode = "AID-0"
                        objatt.BarStructDesc = "AstID"
                        objatt.BarStructLength = 13
                        objatt.BarStructPreFix = ""
                        objatt.PKeyCode = "1"
                        objatt.ValueSep = "None"
                        Insert_BarCode_Struct(objatt)
                    End If
                End If
            End If

        End Sub
#Region "Functions"
        Public Function Insert_BarCode_Struct(ByVal objattBarCode_Struct As attBarCode_Struct) As Boolean
            Try
                objBarCode_Struct.Attribute = objattBarCode_Struct
                Me.Insert(objBarCode_Struct)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
            Return ""
        End Function

        Public Function GetAllData_GetCombo(ByVal objattBarCode_Struct As attBarCode_Struct) As DataTable
            Try
                objBarCode_Struct.Attribute = objattBarCode_Struct
                Return Me.GeneralExecuter(objBarCode_Struct.GetAllData_Combo())
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function Update_BarCode_Struct(ByVal objattBarCode_Struct As attBarCode_Struct) As Boolean
            Try
                objBarCode_Struct.Attribute = objattBarCode_Struct
                Me.Update(objBarCode_Struct)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
            Return Nothing
        End Function

        Public Function GetNextPKey_BarCode_Struct() As String
            Try
                Return Me.GetNextPKey(objBarCode_Struct)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAll_BarCode_Struct(ByVal objattBarCode_Struct As attBarCode_Struct) As DataTable
            Try
                objBarCode_Struct.Attribute = objattBarCode_Struct
                Return GetAllData(objBarCode_Struct)

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function Delete_BarCode_Struct(ByVal objattBarCode_Struct As attBarCode_Struct) As Boolean
            Try
                objBarCode_Struct.Attribute = objattBarCode_Struct
                Me.Delete(objBarCode_Struct)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
    End Class

End Namespace
