Imports ZulAssetsDAL.SAPItems
Imports ZulAssetsDAL.SAPItemsTableAdapters

Public Class SAPItemSerialsTransBLL
    Inherits BaseBLL
    Implements IBusLayer

    Private DataTable As New SAPItemSerialsTransDataTable
    Private _Attributes As SAPItemSerialsTransRow
    Private Adapter As New SAPItemSerialsTransTableAdapter
    'Private ListAdapter As New ZulAssetsDAL.DataListTablesTableAdapters.SAPItemSerialsTransTableAdapter


    Public Sub New()
        TableName = DataTable.TableName
        PrimaryKey = DataTable.GUIDColumn.ColumnName
        OrderField = DataTable.CreationDateColumn.ColumnName

        ListValueMember = DataTable.GUIDColumn.ColumnName
        'ListDisplayMember = DataTable.SerialNoColumn.ColumnName
        ListDataTableType = GetType(ZulAssetsDAL.DataListTables.SAPItemsDataTable)
    End Sub

    Public Property Attributes() As SAPItemSerialsTransRow
        Get
            If _Attributes Is Nothing Then
                _Attributes = DataTable.NewSAPItemSerialsTransRow
            End If
            Return _Attributes
        End Get
        Set(ByVal value As SAPItemSerialsTransRow)
            _Attributes = value
        End Set
    End Property

    Public Function DeleteByRowGUID(ByVal rowGUID As Guid) As String Implements IBusLayer.DeleteByRowGUID
        Try
            If CheckPermission(BusinessLayerName, TActionType.Delete.ToString) Then
                If CheckChildCount(rowGUID) > 0 Then
                    Return My.Resources.Strings.CanNotDelete
                Else
                    If Adapter.DeleteByGUID(rowGUID) = 1 Then
                        AddTransLog(rowGUID, BaseBLL.UserGUID, TActionType.Delete.ToString, DataTable.TableName)
                        Return Nothing
                    Else
                        Return My.Resources.Strings.ErrorWhileDelete
                    End If
                End If
            Else
                Return My.Resources.Strings.NotAuthorized
            End If
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function Edit(ByVal rowGUID As Guid) As String Implements IBusLayer.Edit
        Try
            _RecordState = TRecordStates.ModifiedRecord
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                DataTable = Adapter.GetDataByGUID(rowGUID)
                If DataTable.Rows.Count > 0 Then
                    Attributes = DataTable(0)
                    Return Nothing
                Else
                    Return My.Resources.Strings.RecordNotFound
                End If
            Else
                Return My.Resources.Strings.NotAuthorized
            End If
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function GetData(Optional ByRef msg As String = "") As DataTable Implements IBusLayer.GetData
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Return Adapter.GetData
            Else
                msg = My.Resources.Strings.NotAuthorized
                Return Nothing
            End If
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Private Function GetListData(Optional ByRef msg As String = "") As DataTable Implements IBusLayer.GetListData
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Return Nothing
                'Return ListAdapter.GetData
            Else
                msg = My.Resources.Strings.NotAuthorized
                Return Nothing
            End If
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetGridDataVendorReturn(ByVal DocGUID As Guid, Optional ByRef msg As String = "") As DataTable
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Dim GridAdapter As New ZulAssetsDAL.SAPVendorTableAdapters.VendorReturnGridTableAdapter
                Return GridAdapter.GetDataByDocGUID(DocGUID)
            Else
                msg = My.Resources.Strings.NotAuthorized
                Return Nothing
            End If
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetGridDataWarrantyClaim(ByVal DocGUID As Guid, Optional ByRef msg As String = "") As DataTable
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Dim GridAdapter As New ZulAssetsDAL.SAPWarrantyTableAdapters.WarrantyClaimTableAdapter
                Return GridAdapter.GetDataByDocGUID(DocGUID)
            Else
                msg = My.Resources.Strings.NotAuthorized
                Return Nothing
            End If
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetGridDataGR(ByVal DocGUID As Guid, Optional ByRef msg As String = "") As DataTable
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Dim GridAdapter As New ZulAssetsDAL.SAPGRTableAdapters.GRGridTableAdapter
                Return GridAdapter.GetDataByDocGUID(DocGUID)
            Else
                msg = My.Resources.Strings.NotAuthorized
                Return Nothing
            End If
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetGridDataGI(ByVal DocGUID As Guid, Optional ByRef msg As String = "") As DataTable
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Dim GridAdapter As New ZulAssetsDAL.SAPGITableAdapters.GIGridTableAdapter
                Return GridAdapter.GetDataByDocGUID(DocGUID)
            Else
                msg = My.Resources.Strings.NotAuthorized
                Return Nothing
            End If
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetGridDataReversedItems(ByVal DocGUID As Guid, Optional ByRef msg As String = "") As DataTable
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Dim GridAdapter As New ZulAssetsDAL.SAPReversalTableAdapters.ReversedItemsTableAdapter
                Return GridAdapter.GetDataByDocGUID(DocGUID)
            Else
                msg = My.Resources.Strings.NotAuthorized
                Return Nothing
            End If
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function


    Public Function GetGridDataWarrantyReceiveSame(ByVal DocGUID As Guid, ByVal IsSaved As Boolean, Optional ByRef msg As String = "") As DataTable
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Dim GridAdapter As New ZulAssetsDAL.SAPWarrantyReceiveTableAdapters.WarrantyReceiveSameTableAdapter
                Dim Dt As DataTable
                If IsSaved Then
                    Dt = GridAdapter.GetSavedDataByDocGUID(DocGUID)
                Else
                    Dt = GridAdapter.GetDataByDocGUIDExcludeReceived(DocGUID)
                End If
                Dt.Columns.Add("Selection", Type.GetType("System.Boolean")).DefaultValue = False
                Dt.Columns("Selection").SetOrdinal(0)
                For Each row As DataRow In Dt.Rows
                    row("Selection") = False
                Next
                Return Dt
            Else
                msg = My.Resources.Strings.NotAuthorized
                Return Nothing
            End If
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetGridDataWarrantyReceiveReplace(ByVal DocGUID As Guid, ByVal IsSaved As Boolean, Optional ByRef msg As String = "") As DataTable
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Dim GridAdapter As New ZulAssetsDAL.SAPWarrantyReceiveTableAdapters.WarrantyReceiveReplaceTableAdapter
                Dim Dt As DataTable
                If IsSaved Then
                    Dt = GridAdapter.GetSavedDataByDocGUID(DocGUID)
                Else
                    Dt = GridAdapter.GetDataByDocGUIDExcludeReceived(DocGUID)
                End If
                Dt.Columns.Add("Selection", Type.GetType("System.Boolean")).DefaultValue = False
                Dt.Columns("Selection").SetOrdinal(0)
                For Each row As DataRow In Dt.Rows
                    row("Selection") = False
                Next
                Return Dt
            Else
                msg = My.Resources.Strings.NotAuthorized
                Return Nothing
            End If
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetGRDocNoByItemSerial(ByVal ItemSerialGUID As Guid) As String
        Return Adapter.GetDocNoBySerialGUID(ItemSerialGUID, ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.GR.ToString)
    End Function

    Public Function GetDocumentLastItemGUID(ByVal DocGUID As Guid) As Guid
        Return Adapter.GetDocLastItem(DocGUID)
    End Function

    Public Function GetGridDataReversalGIGRItems(Optional ByRef msg As String = "") As DataTable
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Dim GridAdapter As New ZulAssetsDAL.SAPReversalTableAdapters.ItemsGridTableAdapter
                Return GridAdapter.GetData
            Else
                msg = My.Resources.Strings.NotAuthorized
                Return Nothing
            End If
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Sub NewRecord() Implements IBusLayer.NewRecord
        _RecordState = TRecordStates.NewRecord
        DataTable.RejectChanges()
        Attributes = DataTable.NewSAPItemSerialsTransRow
        Attributes.GUID = Guid.NewGuid
    End Sub

    Public Function Save() As String Implements IBusLayer.Save
        Try
            If _RecordState = TRecordStates.NewRecord Then
                If CheckPermission(BusinessLayerName, TActionType.Add.ToString) Then
                    DataTable.AddSAPItemSerialsTransRow(Attributes)
                    If Adapter.Update(DataTable) = 1 Then
                        _RecordState = TRecordStates.ModifiedRecord
                        'Update the Serial Information by the last transaction data,
                        Dim objSerial As New SAPItemSerialsBLL
                        Dim Result As String = String.Empty
                        Result = objSerial.UpdateSerialInfoByTrans(Attributes)
                        AddTransLog(Attributes.GUID, BaseBLL.UserGUID, TActionType.Add.ToString, DataTable.TableName)
                        Return Result
                    Else
                        DataTable.RejectChanges()
                        Return My.Resources.Strings.ErrorWhileSaving
                    End If
                Else
                    Return My.Resources.Strings.NotAuthorized
                End If

            Else ' _RecordState = TRecordStates.ModifiedRecord
                If CheckPermission(BusinessLayerName, TActionType.Edit.ToString) Then
                    If Adapter.Update(Attributes) = 1 Then
                        AddTransLog(Attributes.GUID, BaseBLL.UserGUID, TActionType.Edit.ToString, DataTable.TableName)
                        Return Nothing
                    Else
                        Return My.Resources.Strings.ErrorWhileSaving
                    End If
                Else
                    Return My.Resources.Strings.NotAuthorized
                End If
            End If
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function


End Class
