Imports ZulAssetsBL
Imports SABBPlugin.StockTransfer
Imports SABBPlugin.StockTransferTableAdapters


Public Class StockTransferItemDetails
    Inherits BaseBLL
    Implements IBusLayer

    Private DataTable As New StockTransferItemsDetailsDataTable
    Private _Attributes As StockTransferItemsDetailsRow
    Private Adapter As New StockTransferItemsDetailsTableAdapter
    ' Private ListAdapter As New MartDAL.DataListTablesTableAdapters.StockTransferItemsDetailsTableAdapter
    ' Private GridAdpater As New TransferItemsGridTableAdapter

    Public Sub New()
        TableName = DataTable.TableName
        PrimaryKey = DataTable.GUIDColumn.ColumnName
        OrderField = DataTable.CreationDateColumn.ColumnName
        ListValueMember = DataTable.GUIDColumn.ColumnName

        ' ListDataTableType = GetType(MartDAL.DataListTables.StockTransferItemsDetailsDataTable)
        'Adding Child tables to the business layer, it will be checked when deleting the records.
        'ChildTables.Add(New BaseBLL.TChildTables("DesigGUID", "StockTransferItemsDetails"))
    End Sub

    Public Property Attributes() As StockTransferItemsDetailsRow
        Get
            If _Attributes Is Nothing Then
                _Attributes = DataTable.NewStockTransferItemsDetailsRow
            End If
            Return _Attributes
        End Get
        Set(ByVal value As StockTransferItemsDetailsRow)
            _Attributes = value
        End Set
    End Property

    Public Function DeleteByRowGUID(ByVal rowGUID As Guid) As String Implements IBusLayer.DeleteByRowGUID
        Try

            If CheckChildCount(rowGUID) > 0 Then
                Return My.Resources.BLLStrings.CanNotDelete
            Else
                If Adapter.DeleteByGUID(rowGUID) = 1 Then
                    AddTransLog(rowGUID, BaseBLL.UserGUID, TActionType.Delete.ToString, DataTable.TableName)
                    Return Nothing
                Else
                    Return My.Resources.BLLStrings.ErrorWhileDelete
                End If
            End If
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function DeleteByStockTransferItemGUID(ByVal StockTransferItemGUID As Guid) As String
        Try
            Adapter.DeleteBySTItemGUID(StockTransferItemGUID)
            Return Nothing
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function GetDataByStockTransferItemGUID(ByVal StockTransferItemGUID As Guid) As DataTable
        Try
            Return Adapter.GetDataBySTItemGUID(StockTransferItemGUID)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetCountByStockTransferItemGUID(ByVal StockTransferItemGUID As Guid) As Integer
        Try
            Dim obj As Object = Adapter.GetCountBySTItemGUID(StockTransferItemGUID)
            If obj IsNot Nothing Then
                Return obj
            Else
                Return 0
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetSerialRepeatedCount(ByVal StockTransferItemGUID As Guid, ByVal SN As String) As Integer
        Try
            Dim obj As Object = Adapter.GetSerialRepeatedCount(StockTransferItemGUID, SN)
            If obj IsNot Nothing Then
                Return obj
            Else
                Return 0
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function Edit(ByVal rowGUID As Guid) As String Implements IBusLayer.Edit
        Try
            _RecordState = TRecordStates.ModifiedRecord
            DataTable = Adapter.GetDataByGUID(rowGUID)
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

    Public Function GetData(Optional ByRef msg As String = "") As DataTable Implements IBusLayer.GetData
        Try
            Return Adapter.GetData
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetListData(Optional ByRef msg As String = "") As DataTable Implements IBusLayer.GetListData
        Try
            Return Nothing
            ' Return ListAdapter.GetData
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    'Public Function GetDataByTransferGUID(ByVal TransferGUID As Guid, Optional ByRef msg As String = "") As DataTable
    '    Try
    '        Dim dtGrid As TransferItemsGridDataTable = GridAdpater.GetTransferItems(TransferGUID)
    '        Return dtGrid
    '    Catch ex As Exception
    '        msg = ex.Message
    '        Return Nothing
    '    End Try
    'End Function


    Public Sub NewRecord() Implements IBusLayer.NewRecord
        _RecordState = TRecordStates.NewRecord
        DataTable.RejectChanges()
        Attributes = DataTable.NewStockTransferItemsDetailsRow
        Attributes.GUID = Guid.NewGuid
    End Sub

    Public Function Save() As String Implements IBusLayer.Save
        Try

            If _RecordState = TRecordStates.NewRecord Then
                Me.Attributes.LastEditDate = Now.Date
                Me.Attributes.LastEditBY = UserGUID
                Me.Attributes.CreationDate = Now.Date
                Me.Attributes.CreatedBy = UserGUID

                DataTable.AddStockTransferItemsDetailsRow(Attributes)
                If Adapter.Update(DataTable) = 1 Then
                    _RecordState = TRecordStates.ModifiedRecord
                    AddTransLog(Attributes.GUID, BaseBLL.UserGUID, TActionType.Add.ToString, DataTable.TableName)
                    Return Nothing
                Else
                    DataTable.RejectChanges()
                    Return My.Resources.BLLStrings.ErrorWhileSaving
                End If

            Else ' _RecordState = TRecordStates.ModifiedRecord
                Me.Attributes.LastEditDate = Now.Date
                Me.Attributes.LastEditBY = UserGUID

                If Adapter.Update(Attributes) = 1 Then
                    AddTransLog(Attributes.GUID, BaseBLL.UserGUID, TActionType.Edit.ToString, DataTable.TableName)
                    Return Nothing
                Else
                    Return My.Resources.BLLStrings.ErrorWhileSaving
                End If
            End If
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function GetMAXNumberByGUID(ByVal STItemGUID As Guid)
        Try
            Return Adapter.GetMaxNumberBYSTItemGUID(STItemGUID)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
