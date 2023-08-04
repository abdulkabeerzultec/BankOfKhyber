Imports ZulAssetsBL
Imports SABBPlugin.PurchaseOrder
Imports SABBPlugin.PurchaseOrderTableAdapters

Public Class PurchaseOrderItemsBLL
    Inherits BaseBLL
    Implements IBusLayer

    Private DataTable As New PODetailsDataTable
    Private _Attributes As PODetailsRow
    Private Adapter As New PODetailsTableAdapter
    'Private GridAdpater As New PODetailsGridTableAdapter
    Private objPO As New PurchaseOrderBLL

    Public Sub New()
        TableName = DataTable.TableName
        'PrimaryKey = DataTable.GUIDColumn.ColumnName
        'OrderField = DataTable.CreationDateColumn.ColumnName
        'ListValueMember = DataTable.GUIDColumn.ColumnName
        'ListDisplayMember = DataTable.QTYColumn.ColumnName
    End Sub

    Public Property Attributes() As PODetailsRow
        Get
            If _Attributes Is Nothing Then
                _Attributes = DataTable.NewPODetailsRow
            End If
            Return _Attributes
        End Get
        Set(ByVal value As PODetailsRow)
            _Attributes = value
        End Set
    End Property

    Public Function DeleteByRowGUID(ByVal rowGUID As Guid) As String Implements IBusLayer.DeleteByRowGUID
        Try
            'If CheckChildCount(rowGUID) > 0 Then
            '    Return My.Resources.BLLStrings.CanNotDelete
            'Else
            '    If Adapter.DeleteByGUID(rowGUID) = 1 Then
            '        AddTransLog(rowGUID, BaseBLL.UserGUID, TActionType.Delete.ToString, DataTable.TableName)
            '        Return Nothing
            '    Else
            '        Return My.Resources.BLLStrings.ErrorWhileDelete
            '    End If
            'End If
            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function DeleteByRowPOItemID(ByVal POItemID As Int64) As String
        If Adapter.DeleteByPOItemID(POItemID) = 1 Then
            Return Nothing
        Else
            Return My.Resources.BLLStrings.ErrorWhileDelete
        End If
    End Function

    Public Function DeleteByPOCode(ByVal POCode As String) As String
        If Adapter.DeleteByPOCode(POCode) = 1 Then
            Return Nothing
        Else
            Return My.Resources.BLLStrings.ErrorWhileDelete
        End If
    End Function

    Public Function Edit(ByVal rowGUID As System.Guid) As String Implements IBusLayer.Edit
        Try
            '_RecordState = TRecordStates.ModifiedRecord
            'DataTable = Adapter.GetDataByGUID(rowGUID)
            'If DataTable.Rows.Count > 0 Then
            '    Attributes = DataTable(0)
            '    Return Nothing
            'Else
            '    Return My.Resources.BLLStrings.RecordNotFound
            'End If
            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function EditByID(ByVal POItemID As String) As String
        Try
            _RecordState = TRecordStates.ModifiedRecord
            DataTable = Adapter.GetDataByPOItemID(POItemID)
            If DataTable.Rows.Count > 0 Then
                Attributes = DataTable(0)
                Return Nothing
            Else
                Return My.Resources.BLLStrings.RecordNotFound
            End If
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function


    Public Function GetData(Optional ByRef msg As String = "") As System.Data.DataTable Implements IBusLayer.GetData
        Try
            Return Adapter.GetData
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetListData(Optional ByRef msg As String = "") As System.Data.DataTable Implements IBusLayer.GetListData
        Try
            'Return ListAdapter.GetData
            Return Nothing
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Sub NewRecord() Implements IBusLayer.NewRecord
        _RecordState = TRecordStates.NewRecord
        DataTable.RejectChanges()
        Attributes = DataTable.NewPODetailsRow
        Attributes.POItmID = GetMAXNumberByGUID()
    End Sub

    Public Function Save() As String Implements IBusLayer.Save
        Try
            If _RecordState = TRecordStates.NewRecord Then

                DataTable.AddPODetailsRow(Attributes)
                If Adapter.Update(DataTable) = 1 Then
                    _RecordState = TRecordStates.ModifiedRecord
                    Return Nothing
                Else
                    DataTable.RejectChanges()
                    Return My.Resources.BLLStrings.ErrorWhileSaving
                End If

            Else ' _RecordState = TRecordStates.ModifiedRecord

                If Adapter.Update(Attributes) = 1 Then
                    Return Nothing
                Else
                    Return My.Resources.BLLStrings.ErrorWhileSaving
                End If
            End If
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function GetPONotReceivedItems(ByVal POCode As String, Optional ByRef msg As String = "") As DataTable
        Try
            Return Adapter.GetPONotReceivedItems(POCode)
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetPOLineNumberBYSKUAndPO(ByVal PurchaseOrderGUID As Guid, ByVal SKUGUID As Guid, Optional ByRef msg As String = "") As String
        Try
            'Dim ad As New PurchaseOrderNotReceivedItemsTableAdapter
            'Dim result As String = ad.GetPOLineNumberBYSKUAndPO(PurchaseOrderGUID, SKUGUID)
            'If Not String.IsNullOrEmpty(result) Then
            '    Return result
            'Else
            '    Return String.Empty
            'End If
        Catch ex As Exception
            msg = ex.Message
            Return String.Empty
        End Try
    End Function

    Public Function GetItemGUIDByPOGUIDAndSKUGUID(ByVal PurchaseOrderGUID As Guid, ByVal SKUGUID As Guid, Optional ByRef msg As String = "") As Guid
        Try
            'Dim Result As Object = Adapter.GetItemByPOGUIDAndSKUGUID(PurchaseOrderGUID, SKUGUID)
            'If Result IsNot Nothing Then
            '    Return Result
            'Else
            '    Return Guid.Empty
            'End If
        Catch ex As Exception
            msg = ex.Message
            Return Guid.Empty
        End Try
    End Function
    Public Function GetMAXNumberByGUID() As Integer
        Try
            Dim obj As Object = Adapter.GetMaxPOItemID
            If obj IsNot Nothing Then
                Return obj
            Else
                Return 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function GetDataByPOCode(ByVal POCode As String, Optional ByRef msg As String = "") As DataTable
        Try
            Return Adapter.GetDataByPOCode(POCode)
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function
End Class
