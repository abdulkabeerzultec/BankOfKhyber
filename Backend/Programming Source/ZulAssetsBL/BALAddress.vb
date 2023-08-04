
Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALAddress
        Inherits BLBase

#Region "Data Members"
        Private objAddress As Address
#End Region
        Public Sub New()
            objAddress = New Address
        End Sub

#Region "Functions"
        Public Function Insert_Address(ByVal objattDept As attAddress) As String
            Try
                objAddress.Attribute = objattDept
                Me.Insert(objAddress)
                Return Me.GetNextPKey_Address()
            Catch ex As Exception
                Throw ex
            End Try
            Return ""
        End Function


        Public Function Update_Address(ByVal objattDept As attAddress) As Boolean
            Try
                objAddress.Attribute = objattDept
                Me.Update(objAddress)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_Address() As String
            Try
                Return Me.GetNextPKey(objAddress)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAll_Address(ByVal objattDept As attAddress) As DataTable
            Try
                objAddress.Attribute = objattDept
                Return GetAllData(objAddress)

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function Delete_Address(ByVal objattDept As attAddress) As Boolean
            Try
                objAddress.Attribute = objattDept
                Me.Delete(objAddress)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function AddressExist(ByVal objatt As attAddress, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("AddressDesc", objatt.AddressDesc, "Address", IsInsertStatus, "AddressID", objatt.PKeyCode)
        End Function
#End Region
    End Class

End Namespace
