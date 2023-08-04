Imports ZulAssetsDAL.SAPItems
Imports ZulAssetsDAL.SAPItemsTableAdapters

Public Class SAPItemSerialsBLL
    Inherits BaseBLL
    Implements IBusLayer

    Private DataTable As New SAPItemSerialsDataTable
    Private _Attributes As SAPItemSerialsRow
    Private Adapter As New SAPItemSerialsTableAdapter
    Private ListAdapter As New ZulAssetsDAL.DataListTablesTableAdapters.SAPItemSerialsTableAdapter

    Public Sub New()
        TableName = DataTable.TableName
        PrimaryKey = DataTable.GUIDColumn.ColumnName
        OrderField = DataTable.CreationDateColumn.ColumnName
        ListValueMember = DataTable.GUIDColumn.ColumnName
        ListDisplayMember = DataTable.SerialNoColumn.ColumnName

        ListDataTableType = GetType(ZulAssetsDAL.DataListTables.SAPItemSerialsDataTable)
    End Sub

    Public Property Attributes() As SAPItemSerialsRow
        Get
            If _Attributes Is Nothing Then
                _Attributes = DataTable.NewSAPItemSerialsRow
            End If
            Return _Attributes
        End Get
        Set(ByVal value As SAPItemSerialsRow)
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

    Public Function GetPOSerialsList(ByVal PONo As String, Optional ByRef msg As String = "") As DataTable
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Dim objAdp As New ZulAssetsDAL.SAPVendorTableAdapters.VendorItemSerialListTableAdapter
                Return objAdp.GetDataByPONo(PONo)
            Else
                msg = My.Resources.Strings.NotAuthorized
                Return Nothing
            End If
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetItemssSerialList(Optional ByRef msg As String = "") As DataTable
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Dim objAdp As New ZulAssetsDAL.SAPItemsTableAdapters.ItemsSerialsListTableAdapter
                Return objAdp.GetData
            Else
                msg = My.Resources.Strings.NotAuthorized
                Return Nothing
            End If
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function


    Public Function GetWarrantyItemssSerialList(ByVal IsNewRecord As Boolean, Optional ByRef msg As String = "") As DataTable
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Dim objAdp As New ZulAssetsDAL.SAPWarrantyTableAdapters.WarrantyItemsSerialsListTableAdapter
                If IsNewRecord Then
                    Return objAdp.GetInsertData
                Else
                    Return objAdp.GetData
                End If
            Else
                msg = My.Resources.Strings.NotAuthorized
                Return Nothing
            End If
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetWarrantyClaimEmpList(Optional ByRef msg As String = "") As DataTable
        Try
            If CheckPermission(BusinessLayerName, TActionType.View.ToString) Then
                Dim objAdp As New ZulAssetsDAL.SAPWarrantyTableAdapters.WarrantyClaimEmpListTableAdapter
                Return objAdp.GetData
            Else
                msg = My.Resources.Strings.NotAuthorized
                Return Nothing
            End If
        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetItemGUIDBySerial(ByVal SerialNo As String, ByVal ManPartNo As String, ByVal SAPPartNo As String) As Guid
        Return Adapter.GetGUIDBySerialNo(SerialNo, SAPPartNo, ManPartNo)
    End Function

    Public Sub NewRecord() Implements IBusLayer.NewRecord
        _RecordState = TRecordStates.NewRecord
        DataTable.RejectChanges()
        Attributes = DataTable.NewSAPItemSerialsRow
        Attributes.GUID = Guid.NewGuid
    End Sub

    Private Function GetUpdateAssetFlag(ByVal ItemSerialsGUID As Guid) As Boolean
        Dim ItemGUID As Object = Adapter.GetItemGUIDBySerial(ItemSerialsGUID)
        If ItemGUID IsNot Nothing Then
            Dim objItem As New ZulAssetsBL.SAPItemsBLL
            objItem.Edit(ItemGUID)
            Return objItem.Attributes.UpdateAssetSN
            'Return ItemGUID
        Else
            Return False
        End If
    End Function

    Private Function GenerateAssetID(ByVal CompanyCode As String, ByVal AssetNumber As String, ByVal SubNumber As String) As Long
        Dim AssetTag As String = CompanyCode & AssetNumber & SubNumber
        If AssetTag.Length <= 0 Then
            Return 0
        End If
        Try
            Dim Num As Long = Long.Parse(AssetTag)
            Return Num
        Catch ex As Exception
            Dim sb As New System.Text.StringBuilder
            For Each ch As Char In AssetTag
                If Char.IsDigit(ch) Then
                    sb.Append(ch)
                End If
            Next
            Return Long.Parse(sb.ToString)
        End Try
    End Function

    Private Function UpdateAssetSerialNumber(ByVal ItemSerialNo As String, ByVal ItemSerialsGUID As Guid, ByVal AssetNo As String) As String
        'Check if Update Asset No Flag is on, then update the asset SerialNo in Asset master data.
        Try
            Dim UpdateAssetFlag As Boolean = GetUpdateAssetFlag(ItemSerialsGUID)
            If UpdateAssetFlag Then
                Dim CompleteCode As String = AssetNo
                Dim strCompleteCode As String() = CompleteCode.Split("-")

                Dim CompanyCode As String = strCompleteCode(0)
                Dim AssetCode As String = strCompleteCode(1)
                Dim SubAsset As String = strCompleteCode(2)

                Dim AssetID As String = GenerateAssetID(CompanyCode, AssetCode, SubAsset)


                Dim objBALAssetDetails As New ZulAssetBAL.BALAssetDetails
                Dim objattAssetDetails As New ZulAssetsDAL.ZulAssetsDAL.attAssetDetails
                objattAssetDetails.PKeyCode = AssetID
                objattAssetDetails.SerailNo = ItemSerialNo
                objattAssetDetails.IsDataChanged = True
                objBALAssetDetails.UpdateAssetSerialNumber(objattAssetDetails)
            End If
            Return Nothing
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function GetIssuanceTypeByWarrantyRecivingTransGUID(ByVal WarrantyRecivingTransGUID As Guid) As String
        Dim Ad As New ZulAssetsDAL.SAPWarrantyReceiveTableAdapters.QueriesTableAdapter
        Return Ad.GetIssuanceTypeForWarrantyReceiving(WarrantyRecivingTransGUID)
    End Function

    Public Function UpdateSerialInfoByTrans(ByVal ItemTrans As SAPItemSerialsTransRow) As String
        Dim objDoc As New ZulAssetsBL.SAPDocumentsBLL
        Dim DocDate As DateTime

        Dim msg As String = objDoc.Edit(ItemTrans.DocGUID)
        If Not String.IsNullOrEmpty(msg) Then
            Return msg
        Else
            DocDate = objDoc.Attributes.DocDate
        End If

        msg = Me.Edit(ItemTrans.ItemSerialGUID)
        If Not String.IsNullOrEmpty(msg) Then
            Return msg
        End If
        Select Case ItemTrans.TransName
            Case SAPDocTypesBLL.DocumentsTypes.GINV.ToString
                Attributes.LastStatus = ItemTrans.TransName
                'If Item is Issued then Start the Warranty of the asset.
                Attributes.WarrantyStartDate = Now.Date

                Attributes.LastDocGUID = ItemTrans.DocGUID
                Attributes.CustodianID = ItemTrans.EmpNo
                Attributes.AssetNo = ItemTrans.AssetNo
                Attributes.IssuedDate = DocDate

                'Check if Update Asset No Flag is on, then update the asset SerialNo in Asset master data.
                If Not Attributes.IsAssetNoNull Then
                    UpdateAssetSerialNumber(Attributes.SerialNo, Attributes.GUID, Attributes.AssetNo)
                End If
            Case SAPDocTypesBLL.DocumentsTypes.GIPOR.ToString
                Attributes.LastStatus = ItemTrans.TransName
                ' if Item is Issued then Start the Warranty of the asset.
                Attributes.WarrantyStartDate = Now.Date
                Attributes.LastDocGUID = ItemTrans.DocGUID
                Attributes.CustodianID = ItemTrans.EmpNo
                Attributes.CostCenter = ItemTrans.CostCenter
                Attributes.IssuedDate = DocDate
            Case SAPDocTypesBLL.DocumentsTypes.ReversalGR.ToString
                Attributes.LastStatus = ItemTrans.TransName
                Attributes.SetWarrantyStartDateNull()
                Attributes.LastDocGUID = ItemTrans.DocGUID
                Attributes.SetReceivedDateNull()
                Attributes.SetIssuedDateNull()
                Attributes.SetCustodianIDNull()
            Case SAPDocTypesBLL.DocumentsTypes.ReversalInvPro.ToString
                'Remove serial No from AssetMasterData.
                If Not Attributes.IsAssetNoNull Then
                    UpdateAssetSerialNumber(String.Empty, Attributes.GUID, Attributes.AssetNo)
                End If

                'After Reverse the GI then we will mark the ItemSerial As GR to be able to issue to another Emp.
                Attributes.LastStatus = SAPDocTypesBLL.DocumentsTypes.GR.ToString
                ' if Item is Issued then Start the Warranty of the asset.
                Attributes.SetWarrantyStartDateNull()
                Attributes.LastDocGUID = ItemTrans.DocGUID
                Attributes.CustodianID = String.Empty
                Attributes.SetIssuedDateNull()
                Attributes.SetCustodianIDNull()
                Attributes.SetAssetNoNull()
            Case SAPDocTypesBLL.DocumentsTypes.ReversalPOR.ToString
                'After Reverse the GI then we will mark the ItemSerial As GR.
                Attributes.LastStatus = SAPDocTypesBLL.DocumentsTypes.GR.ToString
                Attributes.LastDocGUID = ItemTrans.DocGUID
                Attributes.SetWarrantyStartDateNull()
                Attributes.CustodianID = String.Empty
                Attributes.SetIssuedDateNull()
            Case SAPDocTypesBLL.DocumentsTypes.WarrantyClaim.ToString
                'No need to update the warranty start date, because after receiving •Warranty will continue from previous Item, it will not start from the beginning. 
                Attributes.LastStatus = ItemTrans.TransName
                Attributes.LastDocGUID = ItemTrans.DocGUID
            Case SAPDocTypesBLL.DocumentsTypes.WarrantyReceivingSame.ToString
                'Get the Issuance Type based on OrgItemSerialTransGUID
                'leave the Warranty start date from the Item Serial as it is.
                Dim IssuanceType As String = GetIssuanceTypeByWarrantyRecivingTransGUID(ItemTrans.OrgItemSerialTransGUID)
                Attributes.LastStatus = IssuanceType
                Attributes.LastDocGUID = ItemTrans.DocGUID
            Case SAPDocTypesBLL.DocumentsTypes.WarrantyReceivingReplace.ToString
                'Get the Warranty start date from the Old Item Serial.
                Dim objOldSerial As New SAPItemSerialsBLL
                objOldSerial.Edit(ItemTrans.OrgItemSerialGUID)
                Dim WarrantyStartDate As Date = objOldSerial.Attributes.WarrantyStartDate
                'Get the Issuance Type based on OrgItemSerialTransGUID
                Dim IssuanceType As String = GetIssuanceTypeByWarrantyRecivingTransGUID(ItemTrans.OrgItemSerialTransGUID)
                'set the status of the new serial to the old issuance type
                Attributes.LastStatus = IssuanceType
                Attributes.LastDocGUID = ItemTrans.DocGUID

                If objOldSerial.Attributes.IsWarrantyStartDateNull Then
                    Attributes.SetWarrantyStartDateNull()
                Else
                    Attributes.WarrantyStartDate = WarrantyStartDate
                End If

                'update the old serial info set the warranty to nothing and status = WarrantyReplace.
                objOldSerial.Attributes.LastStatus = ItemTrans.TransName
                objOldSerial.Attributes.SetWarrantyStartDateNull()
                objOldSerial.Attributes.LastDocGUID = ItemTrans.DocGUID
                objOldSerial.Save()
            Case Else
                Attributes.LastStatus = ItemTrans.TransName
                Attributes.SetWarrantyStartDateNull()
                Attributes.LastDocGUID = ItemTrans.DocGUID
        End Select

        Return Save()
    End Function
    Public Function Save() As String Implements IBusLayer.Save
        Try

            If _RecordState = TRecordStates.NewRecord Then
                If CheckPermission(BusinessLayerName, TActionType.Add.ToString) Then

                    DataTable.AddSAPItemSerialsRow(Attributes)
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
