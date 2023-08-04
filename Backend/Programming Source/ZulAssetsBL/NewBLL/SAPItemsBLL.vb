Imports ZulAssetsDAL.SAPItems
Imports ZulAssetsDAL.SAPItemsTableAdapters

Public Class SAPItemsBLL
    Inherits BaseBLL
    Implements IBusLayer

    Private DataTable As New SAPItemsDataTable
    Private _Attributes As SAPItemsRow
    Private Adapter As New SAPItemsTableAdapter
    Private ListAdapter As New ZulAssetsDAL.DataListTablesTableAdapters.SAPItemsTableAdapter
    Private GridAdapter As New SAPItemsGridTableAdapter

    Public Sub New()
        TableName = DataTable.TableName
        PrimaryKey = DataTable.GUIDColumn.ColumnName
        OrderField = DataTable.CreationDateColumn.ColumnName

        ListValueMember = DataTable.GUIDColumn.ColumnName
        ListDisplayMember = DataTable.SAPPartNoColumn.ColumnName
        ListDataTableType = GetType(ZulAssetsDAL.DataListTables.SAPItemsDataTable)

        'Adding Child tables to the business layer, it will be checked when deleting the records.
        ChildTables.Add(New BaseBLL.TChildTables("SAPPartNo", "SAPItemSerials"))
    End Sub

    Public Property Attributes() As SAPItemsRow
        Get
            If _Attributes Is Nothing Then
                _Attributes = DataTable.NewSAPItemsRow
            End If
            Return _Attributes
        End Get
        Set(ByVal value As SAPItemsRow)
            _Attributes = value
        End Set
    End Property

    Public Function DeleteByRowGUID(ByVal rowGUID As Guid) As String Implements IBusLayer.DeleteByRowGUID
        Try
            If CheckPermission(BusinessLayerName, TActionType.Delete.ToString) Then
                Edit(rowGUID)
                If CheckChildCountByID(Attributes.SAPPartNo) > 0 Then
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

    Public Function Edit(ByVal SapPartNo As String) As String
        Try
            _RecordState = TRecordStates.ModifiedRecord
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                DataTable = Adapter.GetDataBySAPPartNo(SapPartNo)
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

    Public Function GetListData(Optional ByRef msg As String = "") As DataTable Implements IBusLayer.GetListData
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Return ListAdapter.GetData
            Else
                msg = My.Resources.Strings.NotAuthorized
                Return Nothing
            End If
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetGridData(Optional ByRef msg As String = "") As DataTable
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
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

    Public Function GetGridDataNotApproved(Optional ByRef msg As String = "") As DataTable
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Return GridAdapter.GetNotApprovedItems
            Else
                msg = My.Resources.Strings.NotAuthorized
                Return Nothing
            End If
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetWarrantyBySAPNo(ByVal SAPNo As String) As Integer
        Return Adapter.GetWarrantyBySAPPartNo(SAPNo)
    End Function

    Public Sub NewRecord() Implements IBusLayer.NewRecord
        _RecordState = TRecordStates.NewRecord
        DataTable.RejectChanges()
        Attributes = DataTable.NewSAPItemsRow
        Attributes.GUID = Guid.NewGuid
    End Sub

    Public Function Save() As String Implements IBusLayer.Save
        Try

            If _RecordState = TRecordStates.NewRecord Then
                If CheckPermission(BusinessLayerName, TActionType.Add.ToString) Then
                    DataTable.AddSAPItemsRow(Attributes)
                    If Adapter.Update(DataTable) = 1 Then
                        _RecordState = TRecordStates.ModifiedRecord
                        AddTransLog(Attributes.GUID, BaseBLL.UserGUID, TActionType.Add.ToString, DataTable.TableName)
                        Return Nothing
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
