Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALItems
        Inherits BLBase

#Region "Data Members"
        Private objItems As AssetItems
#End Region


        Public Sub New()
            objItems = New AssetItems
        End Sub

#Region "Functions"
        Public Function Check_Child(ByVal _id As String) As DataTable
            Try
                Return Me.GeneralExecuter(objItems.Check_Child(_id))
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Get_DepPolicy(ByVal _id As String) As DataTable
            Try
                Return Me.GeneralExecuter(objItems.Get_Depolicy(_id))
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Insert_Item(ByVal objattItem As attItems) As Boolean
            Try
                objItems.Attribute = objattItem
                Me.Insert(objItems)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function Update_Item(ByVal objattItem As attItems) As Boolean
            Try
                objItems.Attribute = objattItem
                Me.Update(objItems)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Delete_Item(ByVal objattItem As attItems) As Boolean
            Try
                objItems.Attribute = objattItem
                Me.Delete(objItems)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Sub DeleteItemImage(ByVal objattItem As attItems)
            objItems.Attribute = objattItem
            Me.GeneralExecuter(objItems.DeleteImage())
        End Sub


        Public Sub UpdateItemImage(ByVal objattItem As attItems)
            objItems.Attribute = objattItem
            Me.GeneralExecuter(objItems.UpdateImage())
        End Sub

        Public Function GetItemImage(ByVal objattItem As attItems) As Byte()
            objItems.Attribute = objattItem
            Dim dt As DataTable = Me.GeneralExecuter(objItems.GetImage())

            Dim bits As Byte() = Nothing
            If dt.Rows(0)("ItmImage").ToString <> "" Then
                bits = CType(dt.Rows(0)("ItmImage"), Byte())
            End If
            Return bits

        End Function

        Public Function GetAllItemImage() As DataTable
            Return Me.GeneralExecuter(objItems.GetAllImages())
        End Function

        Public Sub ClearAllItemImage()
            Me.GeneralExecuter(objItems.ClearAllImages())
        End Sub
        Public Function GetAll_Items(ByVal objattItem As attItems) As DataTable
            Try
                objItems.Attribute = objattItem
                Return GetAllData(objItems)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAllData_GetCombo(ByVal objattItem As attItems) As DataTable
            Try
                objItems.Attribute = objattItem
                Return Me.GeneralExecuter(objItems.GetAllData_Combo())
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetAllData_Joined(ByVal objattItem As attItems) As DataTable
            Try
                objItems.Attribute = objattItem
                Return Me.GeneralExecuter(objItems.GetAllData_Joined())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function CheckID(ByVal objattItem As attItems) As DataTable
            Try
                objItems.Attribute = objattItem
                Return Me.GeneralExecuter(objItems.CheckId())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function CheckItemDescription(ByVal objattItem As attItems) As DataTable
            Try
                objItems.Attribute = objattItem
                Return Me.GeneralExecuter(objItems.CheckDescription())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Update_Item_Cat(ByVal objattItem As attItems) As DataTable
            Try
                objItems.Attribute = objattItem
                Return Me.GeneralExecuter(objItems.Update_Assets_Cat())
            Catch ex As Exception
                Throw ex
            End Try


        End Function
        Public Function GetNextPKey_Item() As String
            Try
                Return Me.GetNextPKey(objItems)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region
    End Class

End Namespace
