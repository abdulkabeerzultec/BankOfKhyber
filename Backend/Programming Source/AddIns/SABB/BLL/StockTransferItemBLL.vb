Imports ZulAssetsBL
Imports SABBPlugin.StockTransfer
Imports SABBPlugin.StockTransferTableAdapters

Public Class StockTransferItemBLL
    Inherits BaseBLL
    Implements IBusLayer

    Private DataTable As New StockTransferItemsDataTable
    Private _Attributes As StockTransferItemsRow
    Private Adapter As New StockTransferItemsTableAdapter
    ' Private ListAdapter As New MartDAL.DataListTablesTableAdapters.StockTransferItemsTableAdapter
    Private GridAdpater As New TransferItemsGridTableAdapter

    Public Sub New()
        TableName = DataTable.TableName
        PrimaryKey = DataTable.GUIDColumn.ColumnName
        OrderField = DataTable.CreationDateColumn.ColumnName
        ListValueMember = DataTable.GUIDColumn.ColumnName

        ' ListDataTableType = GetType(MartDAL.DataListTables.StockTransferItemsDataTable)
        'Adding Child tables to the business layer, it will be checked when deleting the records.
        'ChildTables.Add(New BaseBLL.TChildTables("DesigGUID", "StockTransferItems"))
    End Sub

    Public Property Attributes() As StockTransferItemsRow
        Get
            If _Attributes Is Nothing Then
                _Attributes = DataTable.NewStockTransferItemsRow
            End If
            Return _Attributes
        End Get
        Set(ByVal value As StockTransferItemsRow)
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

    Public Function DeleteByStockTransferGUID(ByVal StockTransferGUID As Guid) As String
        Try
            Adapter.DeleteByStockTranferGUID(StockTransferGUID)
            Return Nothing
        Catch ex As Exception
            Return ex.Message
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

    Public Function GetDataByTransferGUID(ByVal TransferGUID As Guid, Optional ByRef msg As String = "") As DataTable
        Try
            Dim dtGrid As TransferItemsGridDataTable = GridAdpater.GetTransferItems(TransferGUID)
            Return dtGrid
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function


    Public Sub NewRecord() Implements IBusLayer.NewRecord
        _RecordState = TRecordStates.NewRecord
        DataTable.RejectChanges()
        Attributes = DataTable.NewStockTransferItemsRow
        Attributes.GUID = Guid.NewGuid
    End Sub

    Public Function Save() As String Implements IBusLayer.Save
        Try

            If _RecordState = TRecordStates.NewRecord Then
                Me.Attributes.LastEditDate = Now.Date
                Me.Attributes.LastEditBY = UserGUID
                Me.Attributes.CreationDate = Now.Date
                Me.Attributes.CreatedBy = UserGUID

                DataTable.AddStockTransferItemsRow(Attributes)
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

    'Public Function GetItemGUIDByDocGUIDAndSKUGUIDAndToLoc(ByVal DocGUID As Guid, ByVal SKUGUID As Guid, ByVal ToLocationGUID As Guid, Optional ByRef msg As String = "") As Guid
    '    Try
    '        Dim Result As Object = Adapter.GetGUIDByDocGUIDAndSKUGUIDAndToLoc(DocGUID, SKUGUID, ToLocationGUID)
    '        If Result IsNot Nothing Then
    '            Return Result
    '        Else
    '            Return Guid.Empty
    '        End If
    '    Catch ex As Exception
    '        msg = ex.Message
    '        Return Nothing
    '    End Try
    'End Function

    'Public Function GetItemGUIDByDocGUIDAndSKUGUID(ByVal DocGUID As Guid, ByVal SKUGUID As Guid, Optional ByRef msg As String = "") As Guid
    '    Try
    '        Dim Result As Object = Adapter.GetGUIDByDocGUIDAndSKUGUID(DocGUID, SKUGUID)
    '        If Result IsNot Nothing Then
    '            Return Result
    '        Else
    '            Return Guid.Empty
    '        End If
    '    Catch ex As Exception
    '        msg = ex.Message
    '        Return Nothing
    '    End Try
    'End Function

    'Public Function GetItemGUIDByDocGUIDAndSKUGUIDAndSerLT(ByVal DocGUID As Guid, ByVal SKUGUID As Guid, ByVal Lot As String, ByVal Serial As String, Optional ByRef msg As String = "") As Guid
    '    Try
    '        Dim Result As Object = Adapter.GetGUIDByDocGUIDandSKUGUIDandSerLT(DocGUID, SKUGUID, Lot, Serial)
    '        If Result IsNot Nothing Then
    '            Return Result
    '        Else
    '            Return Guid.Empty
    '        End If
    '    Catch ex As Exception
    '        msg = ex.Message
    '        Return Nothing
    '    End Try
    'End Function

    Public Function GetMAXLotNumber()
        Try
            Return Adapter.GetMAXLotNumber()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetMAXSerialNumber()
        Try
            Return Adapter.GetMAXSerialNumber.ToString.PadLeft(6, "0")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetMAXNumberByGUID(ByVal StockTransferGUID As Guid)
        Try
            Return Adapter.GetMaxNumberByGUID(StockTransferGUID)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Public Function GetExpiryDays(ByVal ExpiryDays As Integer) As Date
    '    Try
    '        Return Adapter.GetExpiryDate(ExpiryDays)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    'Public Function Get_LotNumber_Expire_ProdDate(ByVal Lotnumber As String, ByVal SerialNumber As String, ByVal StockTransferItemGUID As Guid, ByVal SKUGUID As Guid, ByVal FromlocationGUID As Guid) As DataTable
    '    Try
    '        Dim dt As DataTable = Adapter.Get_LotNumber_Expire_ProdDate(Lotnumber, SerialNumber, StockTransferItemGUID, SKUGUID, FromlocationGUID)
    '        Return dt
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function



    Public Function GetSerialNumberPreparedDocCode(ByVal SerialNumber As String, ByVal STItemGUID As Guid, Optional ByRef msg As String = "") As String
        Try
            Dim Result As Object = Adapter.GetSerialNumberDocCode(SerialNumber, STItemGUID)
            If Result IsNot Nothing Then
                Return Result
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            msg = ex.Message
            Return String.Empty
        End Try
    End Function

End Class
