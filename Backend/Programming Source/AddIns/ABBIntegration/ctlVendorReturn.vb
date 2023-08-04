Imports System.Windows.Forms
Imports ZulAssetsBL.SAPDocTypesBLL

Public Class ctlVendorReturn
    Inherits ZulLib.ctlDataEditing
    Dim objDoc As New ZulAssetsBL.SAPDocumentsBLL
    Dim objDocItems As New ZulAssetsBL.SAPItemSerialsTransBLL
    Dim objItemSerials As New ZulAssetsBL.SAPItemSerialsBLL
    Dim objItem As New ZulAssetsBL.SAPItemsBLL

    Private RitmSerialNo As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit

    Private Sub ctlVendorReturn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BusnissLayerObject = objDoc
        LoadContolSettings(Me)
        NavigationFilter = String.Format("DocType = '{0}'", ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.VendorReturn.ToString)
        NewData()
        ListDataLoad()
        btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        txtDocNo.Properties.ReadOnly = True
        'AddHandler grdItems.EmbeddedNavigator.ButtonClick, AddressOf grdItems_EmbeddedNavigator_ButtonClick
    End Sub

    Private Sub ListDataLoad()
        Me.ListDataSource = objDoc.GetListDataByDocType(ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.VendorReturn.ToString)
    End Sub

    'Private Sub ctlVendorReturn_OnDeleteData() Handles Me.OnDeleteData
    '    Try
    '        Dim Msg As String = objDoc.DeleteByRowGUID(RecordGUID)
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

    Private Sub ctlVendorReturn_OnLoadData() Handles Me.OnLoadData
        Try
            Dim Msg As String = objDoc.Edit(RecordGUID)
            If String.IsNullOrEmpty(Msg) Then
                txtDocNo.Text = objDoc.Attributes.DocNo
                dtDocDate.DateTime = objDoc.Attributes.DocDate
                txtPO.Text = objDoc.Attributes.PONo
                txtPO.Properties.ReadOnly = True
                grdItems_OnLoadData(False)
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

    Private Sub ctlVendorReturn_OnNewData() Handles Me.OnNewData
        dtDocDate.DateTime = Now.Date
        txtPO.Properties.ReadOnly = False
        grdItems_OnLoadData(True)
        txtDocNo.Text = ZulAssetsBL.SAPDocTypesBLL.GetDocCode
        dtDocDate.Select()
        ActionResult = 0
    End Sub

    Private Sub ctlVendorReturn_OnSaveData() Handles Me.OnSaveData
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
                objDoc.NewRecord()
                RecordGUID = objDoc.Attributes.GUID

                objDoc.Attributes.CreationDate = Now.Date
                objDoc.Attributes.CreatedBy = AppConfigData.UserGUID
            Else
                ActionResult = -1
                Exit Sub
            End If

        End If
        objDoc.Attributes.DocNo = txtDocNo.Text
        objDoc.Attributes.DocDate = dtDocDate.DateTime
        objDoc.Attributes.PONo = txtPO.Text
        objDoc.Attributes.DocType = ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.VendorReturn.ToString

        objDoc.Attributes.LastEditDate = Now.Date
        objDoc.Attributes.LastEditBY = AppConfigData.UserGUID
        UpdateStatusbarInfo(objDoc.Attributes.CreatedBy, objDoc.Attributes.CreationDate, objDoc.Attributes.LastEditBY, objDoc.Attributes.LastEditDate, lblCreated, lblModified)

        Try
            Dim Msg As String = objDoc.Save()
            If String.IsNullOrEmpty(Msg) Then
                'Save the grid data only if the record is new.
                If RecordStatus = TRecordStates.NewRecord Then
                    Msg = SaveGridItems()
                End If

                If String.IsNullOrEmpty(Msg) Then
                    ActionResult = 0
                    ListDataLoad()
                    txtPO.Properties.ReadOnly = True
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
        'Save items to database.
        For RowIndex As Integer = 0 To grdViewItems.RowCount - 1
            Dim objDocItems As New ZulAssetsBL.SAPItemSerialsTransBLL
            objDocItems.NewRecord()
            objDocItems.Attributes.CreationDate = Now.Date
            objDocItems.Attributes.CreatedBy = AppConfigData.UserGUID

            objDocItems.Attributes.DocGUID = RecordGUID
            objDocItems.Attributes.ItemSerialGUID = grdViewItems.GetRowCellValue(RowIndex, "ItemSerialGUID")
            objDocItems.Attributes.POLineNo = grdViewItems.GetRowCellValue(RowIndex, "POLineNo")

            objDocItems.Attributes.Plant = grdViewItems.GetRowCellValue(RowIndex, "Plant")
            objDocItems.Attributes.Location = grdViewItems.GetRowCellValue(RowIndex, "StorageLocation")

            objDocItems.Attributes.PONo = txtPO.Text
            objDocItems.Attributes.MovementType = 0
            objDocItems.Attributes.TransName = ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.VendorReturn.ToString
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


    'Private Sub grdItems_OnbtnDeleteClicked(ByVal ParentGuid As System.Guid)
    '    Try
    '        If ParentGuid <> Guid.Empty Then
    '            Dim Msg As String = objDocItems.DeleteByRowGUID(ParentGuid)
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

    Private Sub cmbSerialNo_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FocRow As Integer = grdViewItems.FocusedRowHandle
        Dim obj As Object = CType(sender, DevExpress.XtraEditors.GridLookUpEdit).EditValue
        If obj IsNot DBNull.Value And obj IsNot Nothing Then

            objItemSerials.Edit(obj)
            objItem.Edit(objItemSerials.Attributes.SAPPartNo)
            grdViewItems.SetRowCellValue(FocRow, "SAPPartNo", objItemSerials.Attributes.SAPPartNo)
            grdViewItems.SetRowCellValue(FocRow, "ManufacturePartNo", objItemSerials.Attributes.ManufacturePartNo)
            If objItem.Attributes.CheckUniqueSNOnReceiving Then
                Dim Rowhandle As Long = grdViewItems.LocateByValue(0, grdViewItems.Columns("ItemSerialGUID"), obj)
                If Rowhandle <> DevExpress.XtraGrid.GridControl.InvalidRowHandle Then
                    Messages.ErrorMessage("Item Serial already existed in the grid.")
                    grdViewItems.SetRowCellValue(FocRow, "ItemSerialGUID", Nothing)
                    grdViewItems.SetRowCellValue(FocRow, "SAPPartNo", Nothing)
                    grdViewItems.SetRowCellValue(FocRow, "ManufacturePartNo", Nothing)
                ElseIf objItemSerials.Attributes.LastStatus <> DocumentsTypes.GR.ToString Then
                    Messages.ErrorMessage(String.Format("Item SerialNo status is({0}), you can only return received items(GR), select another SerialNo and try again.", objItemSerials.Attributes.LastStatus))
                    grdViewItems.SetRowCellValue(FocRow, "ItemSerialGUID", Nothing)
                    grdViewItems.SetRowCellValue(FocRow, "SAPPartNo", Nothing)
                    grdViewItems.SetRowCellValue(FocRow, "ManufacturePartNo", Nothing)
                End If
            End If
        End If
    End Sub

    Private Sub grdItems_FormatGrid(ByVal Editable As Boolean)
        grdViewItems.OptionsBehavior.Editable = Editable
        grdViewItems.Columns("GUID").Visible = False
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In grdViewItems.Columns
            col.OptionsColumn.AllowEdit = False
        Next
        RitmSerialNo.View.OptionsView.ShowIndicator = False
        AddHandler RitmSerialNo.EditValueChanged, AddressOf cmbSerialNo_EditValueChanged
        grdViewItems.Columns("ItemSerialGUID").ColumnEdit = RitmSerialNo
        grdViewItems.Columns("ItemSerialGUID").VisibleIndex = 0
        grdViewItems.Columns("ItemSerialGUID").OptionsColumn.AllowEdit = True
        grdViewItems.Columns("ItemSerialGUID").Caption = "Serial Number"

        RitmSerialNo.DataSource = objItemSerials.GetPOSerialsList(txtPO.Text)
        RitmSerialNo.DisplayMember = objItemSerials.ListDisplayMember
        RitmSerialNo.NullText = String.Empty
        RitmSerialNo.ValueMember = "ItemSerialGUID"
        RitmSerialNo.View.Columns("GUID").Visible = False
        RitmSerialNo.View.Columns("ItemSerialGUID").Visible = False
        RitmSerialNo.View.Columns("LastStatus").Visible = False
        'RitmSerialNo.View.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "LastStatus", "GR")

        Dim RIPOLine As New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
        RIPOLine.MinValue = 1
        RIPOLine.MaxValue = Decimal.MaxValue
        RIPOLine.IsFloatValue = False
        RIPOLine.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdViewItems.Columns("POLineNo").ColumnEdit = RIPOLine
        grdViewItems.Columns("POLineNo").OptionsColumn.AllowEdit = True
        grdViewItems.Columns("POLineNo").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdViewItems.Columns("Plant").OptionsColumn.AllowEdit = True
        grdViewItems.Columns("StorageLocation").OptionsColumn.AllowEdit = True
        grdItems.UseEmbeddedNavigator = True
        'grdItems.EmbeddedNavigator.Buttons.Edit.Visible = False
        'grdItems.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
    End Sub

    Private Sub grdItems_OnLoadData(ByVal Editable As Boolean)
        'Cancel current row, if it has been added before loading the data.
        grdViewItems.CancelUpdateCurrentRow()

        grdItems.DataSource = objDocItems.GetGridDataVendorReturn(RecordGUID)
        grdItems_FormatGrid(Editable)
    End Sub

    Private Sub grdViewItems_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles grdViewItems.InitNewRow
        Dim LastRow As Integer = grdViewItems.RowCount - 2
        grdViewItems.SetRowCellValue(e.RowHandle, "GUID", Guid.NewGuid)
        If LastRow >= 0 Then
            grdViewItems.SetRowCellValue(e.RowHandle, "POLineNo", grdViewItems.GetRowCellValue(LastRow, "POLineNo") + 10)
        Else
            grdViewItems.SetRowCellValue(e.RowHandle, "POLineNo", 10)
        End If

    End Sub

    'Private Sub grdViewItems_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles grdViewItems.RowUpdated
    '    'save item to database.
    '    Dim objDocItems As New ZulAssetsBL.SAPItemSerialsTransBLL
    '    Dim FocRow As Integer = -1
    '    If e.RowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle Then
    '        objDocItems.NewRecord()
    '        objDocItems.Attributes.CreationDate = Now.Date
    '        objDocItems.Attributes.CreatedBy = AppConfigData.UserGUID
    '        FocRow = grdViewItems.RowCount - 1 'New Row will be at the end.
    '        'Update the new GUID in the Grid, the new row Handle is at the end of the grid.
    '        grdViewItems.SetRowCellValue(grdViewItems.RowCount - 1, "GUID", objDocItems.Attributes.GUID)
    '    Else
    '        objDocItems.Edit(grdViewItems.GetRowCellValue(e.RowHandle, "GUID"))
    '        FocRow = e.RowHandle
    '    End If
    '    objDocItems.Attributes.DocGUID = RecordGUID
    '    objDocItems.Attributes.ItemSerialGUID = grdViewItems.GetRowCellValue(FocRow, "ItemSerialGUID")
    '    objDocItems.Attributes.POLineNo = grdViewItems.GetRowCellValue(FocRow, "POLineNo")
    '    objDocItems.Attributes.PONo = txtPO.Text
    '    objDocItems.Attributes.MovementType = 0
    '    objDocItems.Attributes.TransName = "REJ & RET"
    '    objDocItems.Attributes.LastEditDate = Now.Date
    '    objDocItems.Attributes.LastEditBY = AppConfigData.UserGUID

    '    Dim Msg As String = objDocItems.Save()
    '    If Not String.IsNullOrEmpty(Msg) Then
    '        Messages.ErrorMessage(Msg, WhoCalledMe)
    '    End If
    'End Sub


    Private Sub grdViewItemsItems_ValidateRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles grdViewItems.ValidateRow
        e.Valid = True
        Dim value As Decimal = grdViewItems.GetRowCellValue(e.RowHandle, "POLineNo")

        If value <= 0 Then
            grdViewItems.SetColumnError(grdViewItems.Columns("POLineNo"), "Invalid Value for PO Line No")
            e.Valid = False
        End If

        Dim ItemGUID As String = grdViewItems.GetRowCellValue(e.RowHandle, "ItemSerialGUID").ToString
        If String.IsNullOrEmpty(ItemGUID) Then
            grdViewItems.SetColumnError(grdViewItems.Columns("ItemSerialGUID"), "Serial Number not entered,Please enter a value")
            e.Valid = False
        End If

        Dim Plant As String = grdViewItems.GetRowCellValue(e.RowHandle, "Plant").ToString
        If String.IsNullOrEmpty(Plant) Then
            grdViewItems.SetColumnError(grdViewItems.Columns("Plant"), "Plant not entered,Please enter a value")
            e.Valid = False
        End If

        Dim StorageLocation As String = grdViewItems.GetRowCellValue(e.RowHandle, "StorageLocation").ToString
        If String.IsNullOrEmpty(StorageLocation) Then
            grdViewItems.SetColumnError(grdViewItems.Columns("StorageLocation"), "Storage Location not entered,Please enter a value")
            e.Valid = False
        End If

        'Dim value As Decimal = grdViewItems.GetRowCellValue(e.RowHandle, "POLineNo")
        'If value <= 0 Then
        '    e.ErrorText = "Invalid Value for PO Line No,"
        '    e.Valid = False
        'End If
        'Dim ItemGUID As String = grdViewItems.GetRowCellValue(e.RowHandle, "ItemSerialGUID").ToString
        'If String.IsNullOrEmpty(ItemGUID) Then
        '    e.ErrorText = "Serial Number not entered,"
        '    e.Valid = False
        'End If
    End Sub

    Private Sub grdViewItemsItems_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles grdViewItems.InvalidRowException
        'Suppress displaying the error message box
        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction
    End Sub

    Private Sub grdViewItems_ShowingEditor(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdViewItems.ShowingEditor
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = sender
        If view.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle AndAlso view.FocusedColumn.FieldName.ToString() = "ItemSerialGUID" Then
            RitmSerialNo.DataSource = objItemSerials.GetPOSerialsList(txtPO.Text)
            '    e.Cancel = False
            'ElseIf view.FocusedColumn.FieldName.ToString() = "ItemSerialGUID" Then
            '    e.Cancel = True
            'Else
            '    e.Cancel = False
        End If
        e.Cancel = False
    End Sub

    'Private Sub grdItems_EmbeddedNavigator_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs)
    '    If e.Button.ButtonType = DevExpress.XtraEditors.NavigatorButtonType.Remove Then
    '        If MessageBox.Show("Do you want to delete the current row?", "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
    '            e.Handled = True
    '        Else
    '            'Delete the row from database
    '            Dim FochRow As Integer = grdViewItems.FocusedRowHandle
    '            If FochRow >= 0 Then
    '                objDocItems.DeleteByRowGUID(grdViewItems.GetRowCellValue(FochRow, "GUID"))
    '            End If
    '        End If

    '    End If
    'End Sub

End Class

