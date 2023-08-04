Imports ZulAssetsBL
Imports SABBPlugin.StockTransfer
Imports SABBPlugin.StockTransferTableAdapters
Imports ZulAssetsDAL.ZulAssetsDAL
Imports ZulAssetsBL.ZulAssetBAL

Public Class StockTransferBLL
    Inherits BaseBLL
    Implements IBusLayer

    Private DataTable As New StockTransferDataTable
    Private _Attributes As StockTransferRow
    Private Adapter As New StockTransferTableAdapter
    Private ListAdapter As New DataListTablesTableAdapters.StockTransferTableAdapter
    'Private TransAdapter As New TransferByWarehouseTableAdapter
    'Private STTypeAdapter As New StockTransferTypeTableAdapter

    Public Enum StockTransferTypes
        DOReceiving
        DirectReceiving
        StockIssuance
        CustodianReturn
        ItemReturn
    End Enum

    Public Sub New()
        TableName = DataTable.TableName
        PrimaryKey = DataTable.GUIDColumn.ColumnName
        OrderField = DataTable.CreationDateColumn.ColumnName
        ListValueMember = DataTable.GUIDColumn.ColumnName
        ListDisplayMember = DataTable.CodeColumn.ColumnName
        ListDataTableType = GetType(DataListTables.StockTransferDataTable)
        'Adding Child tables to the business layer, it will be checked when deleting the records.
        ChildTables.Add(New BaseBLL.TChildTables("StockTransferGUID", "StockTransferItems"))
    End Sub

    Public Property Attributes() As StockTransferRow
        Get
            If _Attributes Is Nothing Then
                _Attributes = DataTable.NewStockTransferRow
            End If
            Return _Attributes
        End Get
        Set(ByVal value As StockTransferRow)
            _Attributes = value
        End Set
    End Property

    Public Function DeleteByRowGUID(ByVal rowGUID As Guid) As String Implements IBusLayer.DeleteByRowGUID
        Try
            GetTransferData(rowGUID)
            Dim TransferType As String = Attributes.TransferType
            Dim PermissionTransferType As String = GetStockPermissionType(Attributes.TransferType)

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

    Public Function Edit(ByVal rowGUID As Guid) As String Implements IBusLayer.Edit
        Try
            _RecordState = TRecordStates.ModifiedRecord
            DataTable = Adapter.GetDataByGUID(rowGUID)
            If DataTable.Rows.Count > 0 Then
                Attributes = DataTable(0)
                Dim TransferType As String = Attributes.TransferType
                Dim PermissionTransferType As String = GetStockPermissionType(Attributes.TransferType)
                Return Nothing
            Else
                Return My.Resources.BLLStrings.RecordNotFound
            End If
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Private Function GetTransferData(ByVal rowGUID As Guid) As String
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

    'Public Function GetStockTransferDocGUIDByParam(ByVal DocCode As String, ByVal DocDate As Date, ByVal Status As String, ByVal TransferType As String) As Guid
    '    Dim Result As Object = Adapter.GetSTByCodeAndToWarehouseAndStatusAndDate(DocCode, DocDate, Status, TransferType)
    '    If Result IsNot Nothing Then
    '        Return Result
    '    Else
    '        Return Guid.Empty
    '    End If
    'End Function

    'Public Function GetSTByCodeAndFromWarehouseAndStatus(ByVal DocCode As String, ByVal WarehouseGUID As Guid, ByVal TransferType As String) As Guid
    '    Dim Result As Object = Adapter.GetSTByCodeAndFromWarehouseAndStatus(DocCode, WarehouseGUID, TransferType)
    '    If Result IsNot Nothing Then
    '        Return Result
    '    Else
    '        Return Guid.Empty
    '    End If
    'End Function

    'Public Function GetSTByPOGUIDAndToWarehouseAndStatus(ByVal POGUID As Guid, ByVal WarehouseGUID As Guid, ByVal TransferType As String, ByVal Status As String) As Guid
    '    Dim Result As Object = Adapter.GetSTByPOGUIDAndToWarehouseAndStatus(POGUID, WarehouseGUID, TransferType, Status)
    '    If Result IsNot Nothing Then
    '        Return Result
    '    Else
    '        Return Guid.Empty
    '    End If
    'End Function


    Private Function UpdateTransferStatus(ByVal TransferGUID As Guid, ByVal Status As String) As String
        Try
            Adapter.UpdateTransferStatus(Status.Trim, UserGUID, TransferGUID)
            Return Nothing
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
    '
    Public Function GetListData(Optional ByRef msg As String = "") As DataTable Implements IBusLayer.GetListData
        Try
            Return ListAdapter.GetData
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetListDataByTransferType(ByVal TransferType As String, Optional ByRef msg As String = "") As DataTable
        Try
            Return ListAdapter.GetDataByStockTransfer(TransferType)
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Sub NewRecord() Implements IBusLayer.NewRecord
        _RecordState = TRecordStates.NewRecord
        DataTable.RejectChanges()
        Attributes = DataTable.NewStockTransferRow
        Attributes.GUID = Guid.NewGuid
    End Sub

    Public Function Save() As String Implements IBusLayer.Save
        Try
            'If Not Attributes.IsFromWarehouseGUIDNull AndAlso Not Attributes.IsToWarehouseGUIDNull AndAlso Attributes.FromWarehouseGUID = Attributes.ToWarehouseGUID Then
            '    Return My.Resources.BLLStrings.FromToWarehouseSame
            'End If

            Dim TransferType As String = Attributes.TransferType
            Dim PermissionTransferType As String = GetStockPermissionType(Attributes.TransferType)

            If _RecordState = TRecordStates.NewRecord Then

                Me.Attributes.LastEditDate = Now.Date
                Me.Attributes.LastEditBY = UserGUID
                Me.Attributes.CreationDate = Now.Date
                Me.Attributes.CreatedBy = UserGUID

                DataTable.AddStockTransferRow(Attributes)
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
    Public Function GetStockPermissionType(ByVal TransferType As String) As String
        Select Case TransferType
            Case StockTransferTypes.DirectReceiving.ToString
                Return "DR"
            Case StockTransferTypes.DOReceiving.ToString
                Return "PR"
            Case StockTransferTypes.ItemReturn.ToString
                Return "IR"
            Case StockTransferTypes.StockIssuance.ToString
                Return "SI"
            Case StockTransferTypes.CustodianReturn.ToString
                Return "CR"
            Case Else
                Return Nothing
        End Select
    End Function

    Public Function Post(ByVal StockTransferGUID As Guid, Optional ByVal ActionInfo As String = "") As String
        GetTransferData(StockTransferGUID)
        Dim TransferType As String = Attributes.TransferType
        Dim ObjDetails As New StockTransferItemDetails
        Dim objSTItem As New StockTransferItemBLL

        Dim dtStockTransItems As DataTable = GetGRNItemByTransferGUID(StockTransferGUID)
        If dtStockTransItems IsNot Nothing AndAlso dtStockTransItems.Rows.Count = 0 Then
            Return My.Resources.BLLStrings.TransferItem()
        End If

        For Each Item As DataRow In dtStockTransItems.Rows

            If TransferType = StockTransferTypes.DirectReceiving.ToString Or TransferType = StockTransferTypes.DOReceiving.ToString Then
                Dim dtItemDetails As DataTable = ObjDetails.GetDataByStockTransferItemGUID(Item("GUID"))
                For Each ItemDetail As DataRow In dtItemDetails.Rows
                    Dim objattAssetDetails As New attAssetDetails
                    objattAssetDetails.SerailNo = ItemDetail("OldSerialNumber").ToString
                    'objattAssetDetails.ItemCode = Item("ItemCode")
                    Dim objBALAssetDetails As New BALAssetDetails
                    Dim dt As DataTable = objBALAssetDetails.GetAstIDBySerialNum(objattAssetDetails)
                    'Create assets if it's not exists, or update if same item same serial number exists.
                    'Status should be "In Stock"

                    Dim POCode As String = ""
                    If Not Attributes.IsPOCodeNull Then
                        POCode = Attributes.POCode
                    End If
                    Dim astID As String = ""
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Update_AssetDetails(dt.Rows(0)("AstID"), Item("Price"), Item("Discount"), ItemDetail("SerialNumber"), Item("ItemCode"), Item("AstDesc"), Attributes.SuppID, Item("ToLocID"), Attributes.AWBNumber, Attributes.TransDate, POCode, Item("AssetStatusID"), Item("CustodianID"))
                        astID = dt.Rows(0)("AstID")
                    Else
                        astID = AddNew_AssetDetails(Item("Price"), Item("Discount"), ItemDetail("SerialNumber"), Item("ItemCode"), Item("AstDesc"), Attributes.SuppID, Item("ToLocID"), Attributes.AWBNumber, Attributes.TransDate, POCode, Item("AssetStatusID"), Item("CustodianID"))
                    End If
                    'Update Details table by the created AstID
                    Dim msg As String = ObjDetails.Edit(ItemDetail("GUID"))
                    If String.IsNullOrEmpty(msg) Then
                        ObjDetails.Attributes.AstID = astID
                        ObjDetails.Save()
                    End If
                Next
            Else
                Dim objattAssetDetails As New attAssetDetails
                objattAssetDetails.SerailNo = Item("AssetSerialNumber").ToString
                objattAssetDetails.ItemCode = Item("ItemCode").ToString
                Dim objBALAssetDetails As New BALAssetDetails
                Dim dt As DataTable = objBALAssetDetails.GetAstIDBySerialNumAndItemCode(objattAssetDetails)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    UpdateIssuance_AssetDetails(dt.Rows(0)("AstID"), Item("ToLocID").ToString, Attributes.AWBNumber, Item("AssetStatusID"), Item("CustodianID").ToString)
                    'Update Details table by the created AstID
                    Dim msg As String = objSTItem.Edit(Item("GUID"))
                    If String.IsNullOrEmpty(msg) Then
                        objSTItem.Attributes.AstID = dt.Rows(0)("AstID")
                        objSTItem.Save()
                    End If
                End If
            End If
        Next

        'UpdateTransferStatus
        Attributes.Status = "Posted"
        Save()
        'UpdateTransferStatus(StockTransferGUID, "Posted")
        If TransferType = StockTransferTypes.DOReceiving.ToString Then
            Dim objPOItem As New PurchaseOrderItemsBLL
            For Each Item As DataRow In dtStockTransItems.Rows
                objPOItem.EditByID(Item("Number"))
                objPOItem.Attributes.PORecQty += Item("QTY")
                If objPOItem.Attributes.PORecQty >= objPOItem.Attributes.POItmQty Then
                    objPOItem.Attributes.IsTrans = True
                End If
                objPOItem.Save()
            Next

            Dim lpo As New PurchaseOrderBLL
            lpo.UpdatePOStatusAfterReceiving(Attributes.POCode)
        End If
        Return String.Empty
    End Function


    Private Sub UpdateIssuance_AssetDetails(ByVal AssetID As String, ByVal ToLocID As String, ByVal IncidentNumber As String, ByVal StatusID As Integer, ByVal CustodianID As String)
        Dim objattAssetDetails As New attAssetDetails
        Dim objBALAssetDetails As New BALAssetDetails
        objattAssetDetails.PKeyCode = AssetID
        Dim dt As DataTable = objBALAssetDetails.GetAll_AssetDetails(objattAssetDetails)

        If dt.Rows.Count > 0 Then
            objattAssetDetails = New attAssetDetails
            objattAssetDetails.PKeyCode = AssetID
            objattAssetDetails.BaseCost = dt.Rows(0)("BaseCost")
            objattAssetDetails.CustodianID = CustodianID
            objattAssetDetails.GLCode = dt.Rows(0)("GLCode")
            objattAssetDetails.SerailNo = dt.Rows(0)("SerailNo")
            objattAssetDetails.InvNumber = dt.Rows(0)("InvNumber")
            objattAssetDetails.ItemCode = dt.Rows(0)("ItemCode")
            objattAssetDetails.POCode = dt.Rows(0)("POCode")
            objattAssetDetails.PurDate = dt.Rows(0)("PurDate")
            objattAssetDetails.SuppID = dt.Rows(0)("SuppID")
            objattAssetDetails.Tax = dt.Rows(0)("Tax")
            ' objattAssetDetails.DispDate = dt.Rows(0)("DispDate")
            objattAssetDetails.CompanyID = dt.Rows(0)("CompanyID")
            objattAssetDetails.SrvDate = dt.Rows(0)("SrvDate")

            objattAssetDetails.AstBrandID = dt.Rows(0)("AstBrandID")
            objattAssetDetails.AstDesc = dt.Rows(0)("AstDesc")
            objattAssetDetails.AstDesc2 = dt.Rows(0)("AstDesc2")
            objattAssetDetails.AstModel = dt.Rows(0)("AstModel")
            objattAssetDetails.Discount = dt.Rows(0)("Discount")
            objattAssetDetails.IsDataChanged = True
            objattAssetDetails.NoPiece = 1
            objattAssetDetails.LocID = ToLocID
            objattAssetDetails.Disposed = "0"
            objattAssetDetails.IsSold = 0
            objattAssetDetails.AstNum = dt.Rows(0)("AstNum")
            objattAssetDetails.RefNo = objattAssetDetails.SerailNo
            objattAssetDetails.BarCode = objattAssetDetails.PKeyCode
            objattAssetDetails.CreatedBY = dt.Rows(0)("CreatedBY")
            objattAssetDetails.LastEditBY = AppConfig.LoginName
            objattAssetDetails.LastEditDate = Now.Date
            objattAssetDetails.CreationDate = dt.Rows(0)("CreationDate")

            objattAssetDetails.CostCenterID = dt.Rows(0)("CostID").ToString
            objattAssetDetails.BussinessArea = dt.Rows(0)("BussinessArea").ToString
            objattAssetDetails.InventoryNumber = dt.Rows(0)("InventoryNumber").ToString

            objattAssetDetails.EvaluationGroup1 = dt.Rows(0)("EvaluationGroup1").ToString
            objattAssetDetails.EvaluationGroup2 = dt.Rows(0)("EvaluationGroup2").ToString
            objattAssetDetails.EvaluationGroup3 = dt.Rows(0)("EvaluationGroup3").ToString
            objattAssetDetails.EvaluationGroup4 = dt.Rows(0)("EvaluationGroup4").ToString
            objattAssetDetails.CustomFld1 = dt.Rows(0)("CustomFld1").ToString
            objattAssetDetails.CustomFld2 = dt.Rows(0)("CustomFld2").ToString
            objattAssetDetails.CustomFld3 = IncidentNumber
            objattAssetDetails.CustomFld4 = dt.Rows(0)("CustomFld4").ToString
            objattAssetDetails.CustomFld5 = dt.Rows(0)("CustomFld5").ToString
            objattAssetDetails.StatusID = StatusID
            objattAssetDetails.InStockAsset = False
            objBALAssetDetails.Update_AssetDetails(objattAssetDetails)
        End If
    End Sub
    Private Sub Update_AssetDetails(ByVal AssetID As String, ByVal BaseCost As Double, ByVal Discount As Double, ByVal SN As String, ByVal ItemCode As String, ByVal ItemDesc As String, ByVal SuppID As String, ByVal ToLocID As String, ByVal IncidentNumber As String, ByVal Purchasedate As DateTime, ByVal POCode As String, ByVal StatusID As Integer, ByVal CustodianID As String)
        Dim objattAssetDetails As New attAssetDetails
        Dim objBALAssetDetails As New BALAssetDetails
        objattAssetDetails.PKeyCode = AssetID
        Dim dt As DataTable = objBALAssetDetails.GetAll_AssetDetails(objattAssetDetails)

        If dt.Rows.Count > 0 Then
            objattAssetDetails = New attAssetDetails
            objattAssetDetails.PKeyCode = AssetID
            objattAssetDetails.BaseCost = BaseCost
            objattAssetDetails.CustodianID = CustodianID
            objattAssetDetails.GLCode = dt.Rows(0)("GLCode")
            objattAssetDetails.SerailNo = SN
            objattAssetDetails.InvNumber = dt.Rows(0)("InvNumber")
            objattAssetDetails.ItemCode = ItemCode
            If POCode = "" Then
                objattAssetDetails.POCode = dt.Rows(0)("POCode")
            Else
                objattAssetDetails.POCode = POCode
            End If
            objattAssetDetails.PurDate = Purchasedate
            objattAssetDetails.SuppID = SuppID
            objattAssetDetails.Tax = dt.Rows(0)("Tax")
            ' objattAssetDetails.DispDate = dt.Rows(0)("DispDate")
            objattAssetDetails.CompanyID = dt.Rows(0)("CompanyID")
            objattAssetDetails.SrvDate = Purchasedate

            objattAssetDetails.AstBrandID = dt.Rows(0)("AstBrandID")
            objattAssetDetails.AstDesc = ItemDesc
            objattAssetDetails.AstDesc2 = dt.Rows(0)("AstDesc2")
            objattAssetDetails.AstModel = dt.Rows(0)("AstModel")
            objattAssetDetails.Discount = Discount
            objattAssetDetails.IsDataChanged = True
            objattAssetDetails.NoPiece = 1
            objattAssetDetails.LocID = ToLocID
            objattAssetDetails.Disposed = "0"
            objattAssetDetails.IsSold = 0
            objattAssetDetails.AstNum = dt.Rows(0)("AstNum")
            objattAssetDetails.RefNo = objattAssetDetails.SerailNo
            objattAssetDetails.BarCode = objattAssetDetails.PKeyCode
            objattAssetDetails.CreatedBY = dt.Rows(0)("CreatedBY")
            objattAssetDetails.LastEditBY = AppConfig.LoginName
            objattAssetDetails.LastEditDate = Now.Date
            objattAssetDetails.CreationDate = dt.Rows(0)("CreationDate")

            objattAssetDetails.CostCenterID = dt.Rows(0)("CostID").ToString
            objattAssetDetails.BussinessArea = dt.Rows(0)("BussinessArea").ToString
            objattAssetDetails.InventoryNumber = dt.Rows(0)("InventoryNumber").ToString

            objattAssetDetails.EvaluationGroup1 = dt.Rows(0)("EvaluationGroup1").ToString
            objattAssetDetails.EvaluationGroup2 = dt.Rows(0)("EvaluationGroup2").ToString
            objattAssetDetails.EvaluationGroup3 = dt.Rows(0)("EvaluationGroup3").ToString
            objattAssetDetails.EvaluationGroup4 = dt.Rows(0)("EvaluationGroup4").ToString
            objattAssetDetails.CustomFld1 = dt.Rows(0)("CustomFld1").ToString
            objattAssetDetails.CustomFld2 = dt.Rows(0)("CustomFld2").ToString
            objattAssetDetails.CustomFld3 = IncidentNumber
            objattAssetDetails.CustomFld4 = dt.Rows(0)("CustomFld4").ToString
            objattAssetDetails.CustomFld5 = dt.Rows(0)("CustomFld5").ToString
            objattAssetDetails.StatusID = StatusID

            objattAssetDetails.InStockAsset = True
            If objBALAssetDetails.Update_AssetDetails(objattAssetDetails) Then
                objBALAssetDetails.Update_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, 0, 5, 0, objattAssetDetails.SrvDate, False)
            End If
        End If

    End Sub

    Private Function AddNew_AssetDetails(ByVal BaseCost As Double, ByVal Discount As Double, ByVal SN As String, ByVal ItemCode As String, ByVal ItemDesc As String, ByVal SuppID As String, ByVal ToLocID As String, ByVal IncidentNumber As String, ByVal Purchasedate As DateTime, ByVal POCode As String, ByVal StatusID As Integer, ByVal CustodianID As String) As String
        Dim objattAssetDetails As New attAssetDetails
        Dim objBALAssetDetails As New BALAssetDetails
        Dim CompanyID As Integer = 1 'Default Company ID
        Dim AstBrandID As Integer = 99999
        Try
            objattAssetDetails.BaseCost = BaseCost
            objattAssetDetails.CustodianID = CustodianID
            objattAssetDetails.GLCode = 1
            objattAssetDetails.SerailNo = SN
            objattAssetDetails.InvNumber = "" 'RemoveUnnecessaryChars(txtInvoice.Text)
            objattAssetDetails.ItemCode = ItemCode
            objattAssetDetails.POCode = POCode
            objattAssetDetails.PurDate = Purchasedate
            objattAssetDetails.SuppID = SuppID
            objattAssetDetails.Tax = 0 'RemoveUnnecessaryChars(txtSales.Text)
            objattAssetDetails.DispDate = Date.MinValue
            objattAssetDetails.CompanyID = CompanyID
            objattAssetDetails.SrvDate = Purchasedate

            objattAssetDetails.AstBrandID = AstBrandID
            objattAssetDetails.AstDesc = ItemDesc
            objattAssetDetails.AstDesc2 = ""
            objattAssetDetails.AstModel = "" ' RemoveUnnecessaryChars(txtAstModel.Text)
            objattAssetDetails.Discount = Discount
            objattAssetDetails.IsDataChanged = True
            objattAssetDetails.NoPiece = 1
            objattAssetDetails.LocID = ToLocID
            objattAssetDetails.Disposed = "0"
            objattAssetDetails.IsSold = 0
            objattAssetDetails.AstNum = Generate_AssetNumber(CompanyID)
            objattAssetDetails.RefNo = objattAssetDetails.SerailNo

            objattAssetDetails.PKeyCode = objBALAssetDetails.Generate_AssetID()
            objattAssetDetails.BarCode = objattAssetDetails.PKeyCode
            objattAssetDetails.CreatedBY = AppConfig.LoginName
            objattAssetDetails.LastEditBY = AppConfig.LoginName
            objattAssetDetails.LastEditDate = Now.Date
            objattAssetDetails.CreationDate = Now.Date

            objattAssetDetails.CostCenterID = String.Empty
            objattAssetDetails.BussinessArea = String.Empty
            objattAssetDetails.InventoryNumber = String.Empty

            objattAssetDetails.EvaluationGroup1 = String.Empty
            objattAssetDetails.EvaluationGroup2 = String.Empty
            objattAssetDetails.EvaluationGroup3 = String.Empty
            objattAssetDetails.EvaluationGroup4 = String.Empty
            objattAssetDetails.CustomFld1 = String.Empty
            objattAssetDetails.CustomFld2 = String.Empty
            objattAssetDetails.CustomFld3 = IncidentNumber
            objattAssetDetails.CustomFld4 = String.Empty
            objattAssetDetails.CustomFld5 = String.Empty
            objattAssetDetails.StatusID = StatusID

            If objBALAssetDetails.Insert_AssetDetails(objattAssetDetails, True) Then
                'Create history for all inventory schedules.
                Dim objBALAst_INV_Schedule As New BALAst_INV_Schedule
                Dim dtInvSch As DataTable = objBALAst_INV_Schedule.Getcombo_Schedule()
                Dim objattAstHistory As attAstHistory
                Dim objBALAst_History As New BALAst_History
                For Each dr As DataRow In dtInvSch.Rows
                    objattAstHistory = New attAstHistory
                    objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                    objattAstHistory.AstID = objattAssetDetails.PKeyCode
                    objattAstHistory.Status = 0
                    objattAstHistory.InvSchCode = dr("InvSchCode")
                    objattAstHistory.HisDate = DateTime.Now.Date
                    objattAstHistory.Fr_loc = ToLocID
                    objattAstHistory.To_Loc = ToLocID
                    objattAstHistory.NoPiece = 1
                    objBALAst_History.Insert_Ast_History(objattAstHistory)
                Next
            End If


            Dim ESalValue As Decimal = 0
            Dim ESalMonth As Decimal = 0
            Dim ESalYear As Decimal = 0
            Dim EDepValue As Decimal = 0

            Dim objBALAssets As New BALItems
            Dim ds As DataTable = objBALAssets.Get_DepPolicy(objattAssetDetails.ItemCode)
            If Not ds Is Nothing Then
                If ds.Rows.Count > 0 Then
                    objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, ds.Rows(0)("SalvageValue"), ds.Rows(0)("SalvageYear"), ds.Rows(0)("SalvageMonth"), Purchasedate, ds.Rows(0)("IsSalvageValuePercent"))
                    ESalValue = ds.Rows(0)("SalvageValue")
                    ESalMonth = ds.Rows(0)("SalvageMonth")
                    ESalYear = ds.Rows(0)("SalvageYear")
                End If
            End If

            'objattAssetDetails.ItemCode = ItemCode
            Return objattAssetDetails.PKeyCode
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
            Return String.Empty
        End Try
    End Function

    Public Function GetAstIDTransHistory(ByVal astid As String) As DataTable
        Dim lAdapter As New TransHistoryTableAdapter
        Return lAdapter.GetDataByAstID(astid)
    End Function

    Public Function GetPrintingLabelData(ByVal TransferGUID As Guid, ByVal TransferType As StockTransferTypes) As DataTable
        Dim lAdapter As New LabelPrintingGridTableAdapter
        Dim dt As DataTable
        If TransferType = StockTransferTypes.DirectReceiving Or TransferType = StockTransferTypes.DOReceiving Then
            dt = lAdapter.GetDataByTransferGUID(TransferGUID)
        Else
            dt = lAdapter.GetIssueDataByTransferGUID(TransferGUID)
        End If
        Dim colSelection As DataColumn = New DataColumn("Selection", System.Type.GetType("System.Boolean"))
        dt.Columns.Add(colSelection)
        dt.Columns("Selection").SetOrdinal(0)
        For Each dr As DataRow In dt.Rows
            dr("Selection") = 0
        Next
        dt.AcceptChanges()
        dt.Columns("Selection").Caption = My.Resources.ColsName.Selection

        Return dt
    End Function

    Public Function GetGRNItemByTransferGUID(ByVal TransferGUID As Guid, Optional ByRef msg As String = "") As DataTable
        Try
            Dim lAdapter As New GRNItemsTableAdapter
            Dim dt As GRNItemsDataTable = lAdapter.GetTransferItems(TransferGUID)

            dt.AstDescColumn.Caption = My.Resources.ColsName.ItemNameEngColumn
            dt.ItemCodeColumn.Caption = My.Resources.ColsName.ItemCodeColumn
            dt.PriceColumn.Caption = My.Resources.ColsName.SellingPriceColumn
            dt.QTYColumn.Caption = My.Resources.ColsName.QTY
            dt.DiscountColumn.Caption = My.Resources.ColsName.Discount
            dt.TotalColumn.Caption = My.Resources.ColsName.Total
            dt.SupplierRefColumn.Caption = My.Resources.ColsName.SupplierItemCodeColumn
            dt.ToLocIDColumn.Caption = My.Resources.ColsName.ToLocation
            dt.FromLocIDColumn.Caption = My.Resources.ColsName.FromLocation
            dt.CustodianIDColumn.Caption = My.Resources.ColsName.Custodian
            dt.AssetStatusIDColumn.Caption = My.Resources.ColsName.Status

            dt.AssetSerialNumberColumn.SetOrdinal(0)
            dt.AstIDColumn.SetOrdinal(1)
            dt.ItemCodeColumn.SetOrdinal(2)
            dt.AstDescColumn.SetOrdinal(3)
            dt.FromLocIDColumn.SetOrdinal(4)
            dt.ToLocIDColumn.SetOrdinal(5)
            dt.CustodianIDColumn.SetOrdinal(6)
            dt.AssetStatusIDColumn.SetOrdinal(7)
            dt.QTYColumn.SetOrdinal(8)
            dt.PriceColumn.SetOrdinal(9)
            dt.DiscountColumn.SetOrdinal(10)
            dt.SerialNumberColumn.SetOrdinal(11)
            dt.TotalColumn.SetOrdinal(12)

            Return dt
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function
    'Public Function GetSTByTransferType(ByVal TransferType As String, ByVal POGUID As Guid, ByVal ToWGUID As Guid, ByVal FromWGUID As Guid, ByVal CustGUID As Guid, ByVal SuppGUID As Guid) As DataTable
    '    Try
    '        Dim dt As DataTable = STTypeAdapter.GetSTByTransferType(TransferType, POGUID, ToWGUID, FromWGUID, CustGUID, SuppGUID)
    '        If dt.Rows.Count > 0 Then
    '            Return dt
    '        Else
    '            Return Nothing
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Public Function UpdateStockTransTotalAmount(ByVal STGUID As Guid) As String
        Try
            Adapter.UpdateSTTotalByGUID(UserGUID, STGUID)
            Return Nothing
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function GetStockTransTotalByGUID(ByVal STGUID As Guid, Optional ByRef msg As String = "") As Decimal
        Try
            Return Adapter.GetSTTotalByGUID(STGUID)
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

End Class
