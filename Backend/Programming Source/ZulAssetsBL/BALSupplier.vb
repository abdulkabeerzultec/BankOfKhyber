Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL

Namespace ZulAssetBAL
    Public Class BALSupplier
        Inherits BLBase
#Region "Data Members"
        Private objSupplier As Supplier
#End Region

        Public Sub New()
            objSupplier = New Supplier
        End Sub

#Region "Functions"
        Public Function Insert_Supplier(ByVal objattSupplier As attSupplier) As Boolean
            Try
                objSupplier.Attribute = objattSupplier
                Me.Insert(objSupplier)
                Return True
            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Public Function Update_Supplier(ByVal objattSupplier As attSupplier) As Boolean
            Try
                objSupplier.Attribute = objattSupplier
                Me.Update(objSupplier)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_Supplier() As String
            Try
                Return Me.GetNextPKey(objSupplier)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetAll_Supplier(ByVal objattSupplier As attSupplier) As DataTable
            Try
                objSupplier.Attribute = objattSupplier
                Return GetAllData(objSupplier)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetAllData_GetCombo() As DataTable
            Try
                Return Me.GeneralExecuter(objSupplier.GetCombo())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function CheckID(ByVal objattSupplier As attSupplier) As DataTable
            Try
                objSupplier.Attribute = objattSupplier
                Return Me.GeneralExecuter(objSupplier.CheckID())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Delete_Supplier(ByVal objattSupplier As attSupplier) As Boolean
            Try
                objSupplier.Attribute = objattSupplier
                Me.Delete(objSupplier)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region


    End Class
End Namespace

