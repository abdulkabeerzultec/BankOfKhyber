Imports ZulAssetsDAL.SAPItems
Imports ZulAssetsDAL.SAPItemsTableAdapters

Public Class SAPDocumentsBLL
    Inherits BaseBLL
    Implements IBusLayer

    Private DataTable As New SAPDocumentsDataTable
    Private _Attributes As SAPDocumentsRow
    Private Adapter As New SAPDocumentsTableAdapter
    Private ListAdapter As New ZulAssetsDAL.DataListTablesTableAdapters.SAPDocumentsTableAdapter
    Private GridAdapter As New SAPItemsGridTableAdapter

    Public Sub New()
        TableName = DataTable.TableName
        PrimaryKey = DataTable.GUIDColumn.ColumnName
        OrderField = DataTable.CreationDateColumn.ColumnName

        ListValueMember = DataTable.GUIDColumn.ColumnName
        ListDisplayMember = DataTable.DocNoColumn.ColumnName
        ListDataTableType = GetType(ZulAssetsDAL.DataListTables.SAPItemsDataTable)
        'Adding Child tables to the business layer, it will be checked when deleting the records.
        ChildTables.Add(New BaseBLL.TChildTables("DocGUID", "SAPItemSerialsTrans"))
    End Sub

    Public Property Attributes() As SAPDocumentsRow
        Get
            If _Attributes Is Nothing Then
                _Attributes = DataTable.NewSAPDocumentsRow
            End If
            Return _Attributes
        End Get
        Set(ByVal value As SAPDocumentsRow)
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

    Public Function GetListDataByDocType(ByVal DocType As String, Optional ByRef msg As String = "") As DataTable
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Return ListAdapter.GetDataByType(DocType)
            Else
                msg = My.Resources.Strings.NotAuthorized
                Return Nothing
            End If
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetWarrantyClaimListDataByDocType(ByVal DocType As String, Optional ByRef msg As String = "") As DataTable
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Dim objAdapter As New ZulAssetsDAL.DataListTablesTableAdapters.WarrantyClaimListTableAdapter
                Return objAdapter.GetDataByType(DocType)
            Else
                msg = My.Resources.Strings.NotAuthorized
                Return Nothing
            End If
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function


    Public Function GetGridDataWarrantyStatus(Optional ByRef msg As String = "") As DataTable
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Dim GridAdapter As New ZulAssetsDAL.SAPWarrantyTableAdapters.Warranty_StatusTableAdapter
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

    Public Sub NewRecord() Implements IBusLayer.NewRecord
        _RecordState = TRecordStates.NewRecord
        DataTable.RejectChanges()
        Attributes = DataTable.NewSAPDocumentsRow
        Attributes.GUID = Guid.NewGuid
    End Sub

    Public Function Save() As String Implements IBusLayer.Save
        Try

            If _RecordState = TRecordStates.NewRecord Then
                If CheckPermission(BusinessLayerName, TActionType.Add.ToString) Then
                    DataTable.AddSAPDocumentsRow(Attributes)
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
