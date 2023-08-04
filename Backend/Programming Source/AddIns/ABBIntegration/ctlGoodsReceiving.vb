Imports System.Windows.Forms
Imports ZulAssetsBL.SAPDocTypesBLL

Public Class ctlGoodsReceiving
    Inherits ZulLib.ctlDataEditing

    Dim objItem As New ZulAssetsBL.SAPItemsBLL
    Dim objItemSerial As New ZulAssetsBL.SAPItemSerialsBLL

    Dim objGR As New ZulAssetsBL.SAPDocumentsBLL
    Dim objGRItem As New ZulAssetsBL.SAPItemSerialsTransBLL


    Private Sub ctlGoodsReceiving_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BusnissLayerObject = objGR
        LoadContolSettings(Me)
        ListDataLoad()
        NavigationFilter = String.Format("DocType = '{0}'", DocumentsTypes.GR.ToString)
        btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        txtGRNo.Properties.ReadOnly = True
        NewData()
    End Sub

    'Private Sub ctlGoodsReceiving_OnDeleteData() Handles Me.OnDeleteData
    '    Try
    '        Dim Msg As String = objGR.DeleteByRowGUID(RecordGUID)
    '        If String.IsNullOrEmpty(Msg) Then
    '            ListDataLoad()
    '            ActionResult = 0
    '        Else
    '            Messages.ErrorMessage(Msg, WhoCalledMe)
    '            ActionResult = -1
    '        End If
    '    Catch ex As Exception
    '        Messages.ErrorMessage(ex.Message, WhoCalledMe)
    '        ActionResult = -1
    '    End Try
    'End Sub

    Private Sub ListDataLoad()
        Me.ListDataSource = objGR.GetListDataByDocType(DocumentsTypes.GR.ToString)
    End Sub

    Private Sub ctlGoodsReceiving_OnLoadData() Handles Me.OnLoadData
        Try
            Dim Msg As String = objGR.Edit(RecordGUID)
            If String.IsNullOrEmpty(Msg) Then
                txtGRNo.Text = objGR.Attributes.DocNo
                txtPONo.Text = objGR.Attributes.PONo
                txtDeliveryNo.Text = objGR.Attributes.DeliveryNo
                dtDocDate.DateTime = objGR.Attributes.DocDate
                grdGRItems_OnLoadData(False)
                ActionResult = 0
            Else
                Messages.ErrorMessage(Msg, WhoCalledMe)
                ActionResult = -1
            End If
        Catch ex As Exception
            Messages.ErrorMessage(ex.Message, WhoCalledMe)
            ActionResult = -1
        End Try
    End Sub

    Private Sub ctlGoodsReceiving_OnNewData() Handles Me.OnNewData
        dtDocDate.DateTime = Now.Date
        txtGRNo.Text = ZulAssetsBL.SAPDocTypesBLL.GetDocCode
        grdGRItems_OnLoadData(True)
        ActionResult = 0
        txtPONo.Select()
    End Sub

    Private Sub ctlGoodsReceiving_OnSaveData() Handles Me.OnSaveData
        If grdViewItems.RowCount <= 0 Then
            Messages.ErrorMessage(My.Resources.NoItemsFound)
            ActionResult = -1
            Exit Sub
        End If

        grdViewItems.CloseEditor()
        If Not grdViewItems.UpdateCurrentRow() Then
            Messages.ErrorMessage(My.Resources.CanNotSave)
            ActionResult = -1
            Exit Sub
        Else
            grdViewItems.ClearColumnErrors()
        End If

        If RecordStatus = TRecordStates.NewRecord Then
            If Messages.QuestionMessage("Are you sure you want to save? after saving modifications are not allowed.") = DialogResult.Yes Then
                objGR.NewRecord()
                RecordGUID = objGR.Attributes.GUID

                objGR.Attributes.CreationDate = Now.Date
                objGR.Attributes.CreatedBy = AppConfigData.UserGUID
            Else
                ActionResult = -1
                Exit Sub
            End If
        End If
        objGR.Attributes.DocNo = txtGRNo.Text
        objGR.Attributes.PONo = txtPONo.Text
        objGR.Attributes.DeliveryNo = txtDeliveryNo.Text
        objGR.Attributes.DocDate = dtDocDate.DateTime
        objGR.Attributes.DocType = DocumentsTypes.GR.ToString

        objGR.Attributes.LastEditDate = Now.Date
        objGR.Attributes.LastEditBY = AppConfigData.UserGUID
        UpdateStatusbarInfo(objGR.Attributes.CreatedBy, objGR.Attributes.CreationDate, objGR.Attributes.LastEditBY, objGR.Attributes.LastEditDate, lblCreated, lblModified)

        Try
            Dim Msg As String = objGR.Save()
            If String.IsNullOrEmpty(Msg) Then
                'Save the grid data only if the record is new.
                If RecordStatus = TRecordStates.NewRecord Then
                    Msg = SaveGridItems()
                End If

                If String.IsNullOrEmpty(Msg) Then
                    ActionResult = 0
                    ListDataLoad()
                    grdViewItems.OptionsBehavior.Editable = False
                Else
                    Messages.ErrorMessage(Msg, WhoCalledMe)
                    ActionResult = -1
                End If
            Else
                Messages.ErrorMessage(Msg, WhoCalledMe)
                ActionResult = -1
            End If
        Catch ex As Exception
            Messages.ErrorMessage(ex.Message, WhoCalledMe)
            ActionResult = -1
        End Try
    End Sub

    Private Function SaveGridItems() As String
        Dim msg As String = String.Empty
        For RowIndex As Integer = 0 To grdViewItems.RowCount - 1
            'save item to database.
            'Check if Serial number is exist.
            Dim serialNo As String = grdViewItems.GetRowCellValue(RowIndex, "SerialNo")
            Dim ManPartNo As String = grdViewItems.GetRowCellValue(RowIndex, "ManufacturePartNo")
            Dim SAPPartNo As String = grdViewItems.GetRowCellValue(RowIndex, "SAPPartNo")
            Dim objItem As New ZulAssetsBL.SAPItemsBLL
            Dim Warranty As Integer = objItem.GetWarrantyBySAPNo(SAPPartNo)

            Dim SerialGUID As Guid = objItemSerial.GetItemGUIDBySerial(serialNo, ManPartNo, SAPPartNo)
            If SerialGUID = Guid.Empty Then
                'Create the New Serial
                objItemSerial.NewRecord()
                objItemSerial.Attributes.CreationDate = Now.Date
                objItemSerial.Attributes.CreatedBy = AppConfigData.UserGUID
                objItemSerial.Attributes.SerialNo = serialNo
                objItemSerial.Attributes.ManufacturePartNo = ManPartNo
                objItemSerial.Attributes.SAPPartNo = SAPPartNo
                objItemSerial.Attributes.LastStatus = DocumentsTypes.GR.ToString
                objItemSerial.Attributes.Warranty = Warranty

                objItemSerial.Attributes.LastDocGUID = RecordGUID
                objItemSerial.Attributes.CustodianID = ""
                objItemSerial.Attributes.AssetNo = ""
                objItemSerial.Attributes.ReceivedDate = dtDocDate.DateTime

                objItemSerial.Attributes.LastEditDate = Now.Date
                objItemSerial.Attributes.LastEditBY = AppConfigData.UserGUID
                msg = objItemSerial.Save()
                If Not String.IsNullOrEmpty(msg) Then
                    Return msg
                End If

                SerialGUID = objItemSerial.Attributes.GUID
            End If

            Dim objDocItems As New ZulAssetsBL.SAPItemSerialsTransBLL
            objDocItems.NewRecord()
            objDocItems.Attributes.CreationDate = Now.Date
            objDocItems.Attributes.CreatedBy = AppConfigData.UserGUID
            objDocItems.Attributes.DocGUID = RecordGUID
            objDocItems.Attributes.ItemSerialGUID = SerialGUID
            objDocItems.Attributes.SeqNo = grdViewItems.GetRowCellValue(RowIndex, "SeqNo")
            objDocItems.Attributes.LineItemNo = grdViewItems.GetRowCellValue(RowIndex, "LineItemNo")
            objDocItems.Attributes.MovementType = 104
            objDocItems.Attributes.PONo = txtPONo.Text
            objDocItems.Attributes.TransName = DocumentsTypes.GR.ToString
            objDocItems.Attributes.ExportedFlag = False
            objDocItems.Attributes.LastEditDate = Now.Date
            objDocItems.Attributes.LastEditBY = AppConfigData.UserGUID
            msg = objDocItems.Save()
            If Not String.IsNullOrEmpty(msg) Then
                Return msg
            End If
        Next
        Return msg
    End Function

    'Private Sub grdGRItems_OnbtnDeleteClicked(ByVal ParentGuid As System.Guid)
    '    Try
    '        If ParentGuid <> Guid.Empty Then
    '            Dim Msg As String = objGRItem.DeleteByRowGUID(ParentGuid)
    '            If String.IsNullOrEmpty(Msg) Then
    '                ActionResult = 0
    '            Else
    '                Messages.ErrorMessage(Msg, WhoCalledMe)
    '                ActionResult = -1
    '            End If
    '        Else
    '            ActionResult = -1
    '        End If
    '    Catch ex As Exception
    '        Messages.ErrorMessage(ex.Message, WhoCalledMe)
    '        ActionResult = -1
    '    End Try
    'End Sub

    Private Sub grdGRItems_OnLoadData(ByVal Editable As Boolean)
        'Cancel current row, if it has been added before loading the data.
        grdViewItems.CancelUpdateCurrentRow()

        grdItems.DataSource = objGRItem.GetGridDataGR(RecordGUID)
        grdGRItems_FormatGrid(Editable)
    End Sub


    Private Sub grdGRItems_FormatGrid(ByVal Editable As Boolean)
        grdViewItems.OptionsBehavior.Editable = Editable

        grdViewItems.Columns("GUID").Visible = False

        grdViewItems.Columns("ManufacturePartNo").VisibleIndex = 0
        grdViewItems.Columns("SAPPartNo").VisibleIndex = 1
        grdViewItems.Columns("LineItemNo").VisibleIndex = 2
        grdViewItems.Columns("SeqNo").VisibleIndex = 3
        grdViewItems.Columns("SerialNo").VisibleIndex = 4


        Dim RitmItemCode As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
        RitmItemCode.View.OptionsView.ShowIndicator = False
        grdViewItems.Columns("SAPPartNo").ColumnEdit = RitmItemCode
        grdViewItems.Columns("SAPPartNo").OptionsColumn.AllowEdit = True
        grdViewItems.Columns("SAPPartNo").Caption = "SAP Part No"


        RitmItemCode.DataSource = objItem.GetListData
        RitmItemCode.DisplayMember = objItem.ListDisplayMember
        RitmItemCode.NullText = String.Empty
        RitmItemCode.ValueMember = objItem.ListDisplayMember
        RitmItemCode.View.Columns("GUID").Visible = False


        Dim RIPOLine As New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
        RIPOLine.MinValue = 1
        RIPOLine.MaxValue = Decimal.MaxValue
        RIPOLine.IsFloatValue = False
        RIPOLine.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near


        grdViewItems.Columns("SeqNo").SortIndex = 0
        grdViewItems.Columns("SeqNo").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

        grdViewItems.Columns("LineItemNo").ColumnEdit = RIPOLine
        grdViewItems.Columns("LineItemNo").OptionsColumn.AllowEdit = True
        grdViewItems.Columns("LineItemNo").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdViewItems.Columns("SeqNo").ColumnEdit = RIPOLine
        grdViewItems.Columns("SeqNo").OptionsColumn.AllowEdit = True
        grdViewItems.Columns("SeqNo").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdItems.UseEmbeddedNavigator = True
    End Sub

    Private Sub grdViewItems_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grdViewItems.CellValueChanged
        If e.Column.FieldName = "SerialNo" Or e.Column.FieldName = "SAPPartNo" Or e.Column.FieldName = "ManufacturePartNo" Then
            Dim SerialNo As String = grdViewItems.GetRowCellValue(e.RowHandle, "SerialNo").ToString
            Dim SAPPartNo As String = grdViewItems.GetRowCellValue(e.RowHandle, "SAPPartNo").ToString
            Dim ManPartNo As String = grdViewItems.GetRowCellValue(e.RowHandle, "ManufacturePartNo").ToString
            If Not String.IsNullOrEmpty(SerialNo) AndAlso Not String.IsNullOrEmpty(SAPPartNo) AndAlso Not String.IsNullOrEmpty(ManPartNo) Then
                objItem.Edit(SAPPartNo)
                If objItem.Attributes.CheckUniqueSNOnReceiving Then

                    Dim Rowhandle As Long = grdViewItems.LocateByValue(0, grdViewItems.Columns("SerialNo"), SerialNo)

                    Dim SerialGUID As Guid = objItemSerial.GetItemGUIDBySerial(SerialNo, ManPartNo, SAPPartNo)
                    'Check if the same item found in the grid.
                    If Rowhandle <> e.RowHandle And Rowhandle <> DevExpress.XtraGrid.GridControl.InvalidRowHandle Then
                        Messages.ErrorMessage("Item Serial already existed in the grid.")
                        grdViewItems.SetRowCellValue(e.RowHandle, "SerialNo", "")
                    ElseIf SerialGUID <> Guid.Empty Then
                        'Check if the item serial status if Received or issued then show the error message.
                        objItemSerial.Edit(SerialGUID)
                        If objItemSerial.Attributes.LastStatus = DocumentsTypes.GR.ToString Or objItemSerial.Attributes.LastStatus = DocumentsTypes.GINV.ToString Or objItemSerial.Attributes.LastStatus = DocumentsTypes.GIPOR.ToString Then
                            grdViewItems.SetRowCellValue(e.RowHandle, "SerialNo", "")
                            Dim DocNo As String = objGRItem.GetGRDocNoByItemSerial(SerialGUID)
                            Messages.ErrorMessage(String.Format("Item with SerialNo({0}) already received in GR Document No({1}).", SerialNo, DocNo))
                        End If
                    End If
                End If
            End If

        End If
    End Sub

    Private Sub grdViewItems_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles grdViewItems.InitNewRow
        Dim LastRow As Integer = grdViewItems.RowCount - 2
        grdViewItems.SetRowCellValue(e.RowHandle, "GUID", Guid.NewGuid)
        If LastRow >= 0 Then
            grdViewItems.SetRowCellValue(e.RowHandle, "LineItemNo", grdViewItems.GetRowCellValue(LastRow, "LineItemNo") + 10)
            grdViewItems.SetRowCellValue(e.RowHandle, "SeqNo", grdViewItems.GetRowCellValue(LastRow, "SeqNo") + 1)
            grdViewItems.SetRowCellValue(e.RowHandle, "ManufacturePartNo", grdViewItems.GetRowCellValue(LastRow, "ManufacturePartNo"))
            grdViewItems.SetRowCellValue(e.RowHandle, "SAPPartNo", grdViewItems.GetRowCellValue(LastRow, "SAPPartNo"))
        Else
            grdViewItems.SetRowCellValue(e.RowHandle, "LineItemNo", 10)
            grdViewItems.SetRowCellValue(e.RowHandle, "SeqNo", 1)
        End If
    End Sub

    Private Sub grdViewItemsItems_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles grdViewItems.InvalidRowException
        'Suppress displaying the error message box
        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction
    End Sub

    Private Sub grdViewItems_ValidateRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles grdViewItems.ValidateRow
        e.Valid = True

        Dim SerialNo As String = grdViewItems.GetRowCellValue(e.RowHandle, "SerialNo").ToString
        If String.IsNullOrEmpty(SerialNo) Then
            grdViewItems.SetColumnError(grdViewItems.Columns("SerialNo"), "Serial Number not entered")
            e.Valid = False
        End If

        Dim ManPartNo As String = grdViewItems.GetRowCellValue(e.RowHandle, "ManufacturePartNo").ToString
        If String.IsNullOrEmpty(ManPartNo) Then
            grdViewItems.SetColumnError(grdViewItems.Columns("ManufacturePartNo"), "Manufacture Part No not selected")
            e.Valid = False
        End If

        Dim SAPPartNo As String = grdViewItems.GetRowCellValue(e.RowHandle, "SAPPartNo").ToString
        If String.IsNullOrEmpty(SAPPartNo) Then
            grdViewItems.SetColumnError(grdViewItems.Columns("SAPPartNo"), "SAP Part No not selected")
            e.Valid = False
        End If

        Dim value As Decimal = grdViewItems.GetRowCellValue(e.RowHandle, "LineItemNo")
        If value <= 0 Then
            grdViewItems.SetColumnError(grdViewItems.Columns("LineItemNo"), "Invalid Value for Line Item No")
            e.Valid = False
        End If

        value = grdViewItems.GetRowCellValue(e.RowHandle, "SeqNo")
        If value <= 0 Then
            grdViewItems.SetColumnError(grdViewItems.Columns("SeqNo"), "Invalid Value for Seq No")
            e.Valid = False
        End If


        'Dim SerialNo As String = grdViewItems.GetRowCellValue(e.RowHandle, "SerialNo").ToString
        'If String.IsNullOrEmpty(SerialNo) Then
        '    e.ErrorText = "Serial Number not entered,"
        '    e.Valid = False
        'End If

        'Dim ManPartNo As String = grdViewItems.GetRowCellValue(e.RowHandle, "ManufacturePartNo").ToString
        'If String.IsNullOrEmpty(ManPartNo) Then
        '    e.ErrorText = "Manufacture Part No not selected,"
        '    e.Valid = False
        'End If

        'Dim SAPPartNo As String = grdViewItems.GetRowCellValue(e.RowHandle, "SAPPartNo").ToString
        'If String.IsNullOrEmpty(SAPPartNo) Then
        '    e.ErrorText = "SAP Part No not selected,"
        '    e.Valid = False
        'End If

        'Dim value As Decimal = grdViewItems.GetRowCellValue(e.RowHandle, "LineItemNo")
        'If value <= 0 Then
        '    e.ErrorText = "Invalid Value for Line Item No,"
        '    e.Valid = False
        'End If

        'value = grdViewItems.GetRowCellValue(e.RowHandle, "SeqNo")
        'If value <= 0 Then
        '    e.ErrorText = "Invalid Value for Seq No,"
        '    e.Valid = False
        'End If
    End Sub

End Class
