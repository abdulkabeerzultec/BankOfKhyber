Imports SABBPlugin.Items
Imports SABBPlugin.ItemsTableAdapters

Public Class ItemsBLL

    Public Function GetItemList(ByVal POCode As String) As DataTable
        Try
            Dim Adapter As New ItemsTableAdapter
            If String.IsNullOrEmpty(POCode) Then
                Return Adapter.GetData
            Else
                Return Adapter.GetDataByPO(POCode)
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function GetItemDetails(ByVal ItemCode As String) As ItemsDataTable
        Try
            Dim Adapter As New ItemsTableAdapter
            Return Adapter.GetDataByItemCode(ItemCode)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetItemDetailsBySerial(ByVal Serial As String) As IssuancItemsDataTable
        Try
            Dim Adapter As New IssuancItemsTableAdapter
            Return Adapter.GetDataByAssetSerial(Serial)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function GetItemDetailsByAstID(ByVal AstID As String) As IssuancItemsDataTable
        Try
            Dim Adapter As New IssuancItemsTableAdapter
            Return Adapter.GetDataByAstID(AstID)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function GetSerialNumberByItemCodeAndStatus(ByVal StatusID As Integer) As DataTable
        Try
            Dim Adapter As New AssetDetailsTableAdapter
            Return Adapter.GetSerialDataByStatus(StatusID)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetSerialNumberByItemCode() As DataTable
        Try
            Dim Adapter As New AssetDetailsTableAdapter
            Return Adapter.GetAllSerialNumbers()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function GetAstIDByItemCode() As DataTable
        Try
            Dim Adapter As New AssetDetailsAstIDTableAdapter
            Return Adapter.GetData()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function GetItemReturnSerials() As DataTable
        Try
            Dim Adapter As New AssetDetailsTableAdapter
            Return Adapter.GetItemReturnSerials()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
