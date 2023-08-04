Imports ZulAssetsBL
Imports SABBPlugin.PurchaseOrder
Imports SABBPlugin.PurchaseOrderTableAdapters
Public Class PurchaseOrderBLL
    Inherits BaseBLL
    Implements IBusLayer


    Private DataTable As New PurchaseOrderDataTable
    Private _Attributes As PurchaseOrderRow
    Private Adapter As New PurchaseOrderTableAdapter
    'Private ListAdapter As New DataListTablesTableAdapters.PurchaseOrderTableAdapter
    Private StatusAdapter As New PurchaseOrderListTableAdapter
    'Private rptPOAdapter As New MartDAL.ReportsTableAdapters.rptPurchaseOrderTableAdapter

    Public Sub New()
        'TableName = DataTable.TableName
        'PrimaryKey = DataTable.GUIDColumn.ColumnName
        'OrderField = DataTable.CreationDateColumn.ColumnName

        'ListValueMember = DataTable.GUIDColumn.ColumnName
        'ListDisplayMember = DataTable.CodeColumn.ColumnName

        'ListDataTableType = GetType(DataListTables.PurchaseOrderDataTable)
        ChildTables.Add(New BaseBLL.TChildTables("PurchaseOrderGUID", "PurchaseOrderItems"))
    End Sub
    Public Property Attributes() As PurchaseOrderRow
        Get
            If _Attributes Is Nothing Then
                _Attributes = DataTable.NewPurchaseOrderRow
            End If
            Return _Attributes
        End Get
        Set(ByVal value As PurchaseOrderRow)
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
    Public Function DeleteByPOCode(ByVal POCode As Int64) As String
        If Adapter.DeleteByPoCode(POCode) = 1 Then
            Return Nothing
        Else
            Return My.Resources.BLLStrings.ErrorWhileDelete
        End If
    End Function

    Public Function Edit(ByVal rowGUID As System.Guid) As String Implements IBusLayer.Edit
        Try
            '_RecordState = TRecordStates.ModifiedRecord
            'If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
            '    DataTable = Adapter.GetDataByGUID(rowGUID)
            '    If DataTable.Rows.Count > 0 Then
            '        Attributes = DataTable(0)
            '        Return Nothing
            '    Else
            '        Return My.Resources.BLLStrings.RecordNotFound
            '    End If
            'Else
            '    Return My.Resources.Strings.NotAuthorized
            'End If
            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function Edit(ByVal POCode As Int64) As String
        DataTable = Adapter.GetDataByPOCode(POCode)
        If DataTable.Rows.Count > 0 Then
            Attributes = DataTable(0)
            Return Nothing
        Else
            Return My.Resources.BLLStrings.RecordNotFound
        End If
    End Function

    Public Function GetData(Optional ByRef msg As String = "") As System.Data.DataTable Implements IBusLayer.GetData
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
        Attributes = DataTable.NewPurchaseOrderRow
        Attributes.POCode = GetMaxPOCode()
    End Sub

    Public Function Save() As String Implements IBusLayer.Save
        Try

            If _RecordState = TRecordStates.NewRecord Then
                DataTable.AddPurchaseOrderRow(Attributes)
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
    Public Function UpdatePOStatus(ByVal Status As Integer, ByVal POCode As String) As String
        Adapter.UpdateStatusOnly(Status, POCode)
        Return Nothing
    End Function

    Public Function UpdatePOStatusAfterReceiving(ByVal POCode As String) As String
        Try
            Dim RecCount As Integer = GetPONotReceivedItemsCount(POCode)
            If RecCount > 0 Then
                Adapter.UpdateStatusOnly(3, POCode) '"PartialReceived"
            Else
                Adapter.UpdateStatusOnly(4, POCode) '"Received"
            End If
            Return Nothing
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function GetPONotReceivedItemsCount(ByVal POCode As String) As Integer
        Dim Result As Object = Adapter.GetPONotReceivedItemsCount(POCode)
        If Result IsNot Nothing Then
            Return Result
        Else
            Return 0
        End If
    End Function

    Public Function GetMaxPOCode() As Integer
        Dim obj As Object = Adapter.GetMaxPOCode
        If obj IsNot Nothing Then
            Return obj
        Else
            Return 1
        End If
    End Function

    Public Function GetPurchseOrderByStatus(ByVal Status1 As String, ByVal Status2 As String, Optional ByRef msg As String = "") As DataTable
        Try
            Return StatusAdapter.GetDataByStatus(Status1, Status2)
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function


    Public Function GetPOTotal(ByVal POCode As String) As Double
        Return Adapter.GetPOAmount(POCode)
    End Function

    Public Function UpdateTotalAmount(ByVal POCode As Integer) As String
        Try
            Adapter.UpdatePOTotalByPOCode(POCode)
            Return Nothing
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
End Class
