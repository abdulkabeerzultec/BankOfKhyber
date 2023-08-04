Imports System.Windows.Forms
Imports ZulAssetsBL.SAPDocTypesBLL

Public Class ctlReversalDoc
    Inherits ZulLib.ctlDataEditing
    Dim objReversal As New ZulAssetsBL.SAPDocumentsBLL
    Dim objDocItems As New ZulAssetsBL.SAPItemSerialsTransBLL

    Private Sub ctlReversalDoc_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ParentForm.WindowState = Windows.Forms.FormWindowState.Maximized
        BusnissLayerObject = objReversal
        LoadContolSettings(Me)
        NewData()
        ListDataLoad()
        NavigationFilter = String.Format("DocType = '{0}'", ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.Reversal.ToString)
        btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        txtDocNo.Properties.ReadOnly = True
    End Sub

    Private Sub ListDataLoad()
        Me.ListDataSource = objReversal.GetListDataByDocType(ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.Reversal.ToString)
    End Sub

    'Private Sub ctlReversalDoc_OnDeleteData() Handles Me.OnDeleteData
    '    Try
    '        Dim Msg As String = objReversal.DeleteByRowGUID(RecordGUID)
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

    Private Sub ctlReversalDoc_OnLoadData() Handles Me.OnLoadData
        Try
            Dim Msg As String = objReversal.Edit(RecordGUID)
            If String.IsNullOrEmpty(Msg) Then
                txtDocNo.Text = objReversal.Attributes.DocNo
                dtDocDate.DateTime = objReversal.Attributes.DocDate
                grdItems_LoadData()
                grdReversedItems_LoadData(False)
                LayoutItems.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                layoutPanel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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

    Private Sub grdItems_LoadData()
        Dim dtItems As DataTable = objDocItems.GetGridDataReversalGIGRItems
        dtItems.Columns.Add("Selection", Type.GetType("System.Boolean"))
        grdItems.DataSource = dtItems
        grdItems_Format()
    End Sub

    Private Sub grdItems_Format()
        grdViewItems.OptionsBehavior.Editable = True
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In grdViewItems.Columns
            col.OptionsColumn.AllowEdit = False
        Next

        For i As Integer = 0 To grdViewItems.RowCount - 1
            grdViewItems.SetRowCellValue(i, "Selection", False)
        Next

        grdViewItems.Columns("GUID").Visible = False
        grdViewItems.Columns("Selection").OptionsColumn.AllowEdit = True
        grdViewItems.Columns("Selection").VisibleIndex = 0
        grdViewItems.Columns("Selection").Caption = ""
        grdViewItems.Columns("Selection").MaxWidth = 30

        grdViewItems.Columns("DocGUID").Visible = False
        grdViewItems.Columns("ItemSerialGUID").Visible = False

        grdViewItems.Columns("LineItemNo").Visible = False
        grdViewItems.Columns("PONo").Visible = False
        grdViewItems.Columns("GLAC").Visible = False
        grdViewItems.Columns("CostCenter").Visible = False
        grdViewItems.Columns("BusinessArea").Visible = False
        grdViewItems.Columns("AssetNo").Visible = False

        grdViewItems.OptionsView.ShowAutoFilterRow = True
    End Sub

    Private Sub grdReversedItems_LoadData(ByVal Editable As Boolean)
        grdViewReversedItems.CancelUpdateCurrentRow()

        grdReversedItems.DataSource = objDocItems.GetGridDataReversedItems(RecordGUID)
        grdReversedItems_Format(Editable)
    End Sub

    Private Sub ctlReversalDoc_OnNewData() Handles Me.OnNewData
        dtDocDate.DateTime = Now.Date
        grdReversedItems_LoadData(True)
        grdItems_LoadData()
        LayoutItems.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        layoutPanel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        txtDocNo.Text = ZulAssetsBL.SAPDocTypesBLL.GetDocCode
        dtDocDate.Focus()
        ActionResult = 0
    End Sub

    Private Sub grdReversedItems_Format(ByVal Editable As Boolean)
        grdViewReversedItems.OptionsBehavior.Editable = Editable
        grdViewReversedItems.Columns("GUID").Visible = False
        grdViewReversedItems.Columns("ItemSerialGUID").Visible = False
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In grdViewReversedItems.Columns
            col.OptionsColumn.AllowEdit = False
        Next

        grdViewReversedItems.Columns("SAPMaterialDocNo").OptionsColumn.AllowEdit = True
        grdViewReversedItems.Columns("SAPMaterialDocLineNo").OptionsColumn.AllowEdit = True
        grdViewReversedItems.Columns("ReasonForMovement").OptionsColumn.AllowEdit = True
        grdViewReversedItems.Columns("Plant").OptionsColumn.AllowEdit = True
        grdViewReversedItems.Columns("StorageLocation").OptionsColumn.AllowEdit = True

        grdViewReversedItems.Columns("LineItemNo").Visible = False
        grdViewReversedItems.Columns("PONo").Visible = False
        grdViewReversedItems.Columns("GLAC").Visible = False
        grdViewReversedItems.Columns("CostCenter").Visible = False
        grdViewReversedItems.Columns("BusinessArea").Visible = True
        grdViewReversedItems.Columns("BusinessArea").OptionsColumn.AllowEdit = True
        grdViewReversedItems.Columns("BusinessArea").VisibleIndex = grdViewReversedItems.Columns("Plant").VisibleIndex
        grdViewReversedItems.Columns("AssetNo").Visible = False

        grdReversedItems.UseEmbeddedNavigator = True
        grdReversedItems.EmbeddedNavigator.Buttons.Append.Visible = False
    End Sub

    Private Sub ctlReversalDoc_OnSaveData() Handles Me.OnSaveData
        If grdViewReversedItems.RowCount <= 0 Then
            Messages.ErrorMessage(My.Resources.NoItemsFound)
            ActionResult = -1
            Exit Sub
        End If

        Dim msg As String = String.Empty
        msg = ValidateGridItems()
        If Not String.IsNullOrEmpty(msg) Then
            Messages.ErrorMessage(msg)
            ActionResult = -1
            Exit Sub
        End If


        If RecordStatus = TRecordStates.NewRecord Then
            If Messages.QuestionMessage("Are you sure you want to save? after saving modifications are not allowed.") = DialogResult.Yes Then
                objReversal.NewRecord()
                RecordGUID = objReversal.Attributes.GUID

                objReversal.Attributes.CreationDate = Now.Date
                objReversal.Attributes.CreatedBy = AppConfigData.UserGUID
            Else
                ActionResult = -1
                Exit Sub
            End If
        End If
        objReversal.Attributes.DocNo = txtDocNo.Text
        objReversal.Attributes.DocDate = dtDocDate.DateTime

        objReversal.Attributes.DocType = ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.Reversal.ToString

        objReversal.Attributes.LastEditDate = Now.Date
        objReversal.Attributes.LastEditBY = AppConfigData.UserGUID
        UpdateStatusbarInfo(objReversal.Attributes.CreatedBy, objReversal.Attributes.CreationDate, objReversal.Attributes.LastEditBY, objReversal.Attributes.LastEditDate, lblCreated, lblModified)

        Try


            msg = objReversal.Save()
            If String.IsNullOrEmpty(Msg) Then
                'Save the grid data only if the record is new.
                If RecordStatus = TRecordStates.NewRecord Then
                    Msg = SaveGridItems()
                End If

                If String.IsNullOrEmpty(Msg) Then
                    ActionResult = 0
                    ListDataLoad()
                    grdViewReversedItems.OptionsBehavior.Editable = False
                    LayoutItems.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    layoutPanel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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
    Private Function ValidateGridItems() As String
        Dim msg As String = String.Empty

        grdViewItems.CloseEditor()
        grdViewItems.UpdateCurrentRow()
        For RowIndex As Integer = 0 To grdViewReversedItems.RowCount - 1
            grdViewReversedItems.ClearColumnErrors()
            grdViewReversedItems.FocusedRowHandle = RowIndex

            Dim SAPMaterialDocNo As String = grdViewReversedItems.GetRowCellValue(RowIndex, "SAPMaterialDocNo").ToString
            Dim SAPMaterialDocLineNo As String = grdViewReversedItems.GetRowCellValue(RowIndex, "SAPMaterialDocLineNo").ToString
            Dim ReasonForMovement As String = grdViewReversedItems.GetRowCellValue(RowIndex, "ReasonForMovement").ToString
            Dim Plant As String = grdViewReversedItems.GetRowCellValue(RowIndex, "Plant").ToString
            Dim StorageLocation As String = grdViewReversedItems.GetRowCellValue(RowIndex, "StorageLocation").ToString
            Dim BusinessArea As String = grdViewReversedItems.GetRowCellValue(RowIndex, "BusinessArea").ToString

            If String.IsNullOrEmpty(SAPMaterialDocNo) Then
                msg = "SAP Material DocNo not entered."
                grdViewReversedItems.SetColumnError(grdViewReversedItems.Columns("SAPMaterialDocNo"), msg)
            End If

            If String.IsNullOrEmpty(SAPMaterialDocLineNo) Then
                msg = "SAP Material DocLineNo not entered."
                grdViewReversedItems.SetColumnError(grdViewReversedItems.Columns("SAPMaterialDocLineNo"), msg)
            End If

            If String.IsNullOrEmpty(Plant) Then
                msg = "Plant not entered."
                grdViewReversedItems.SetColumnError(grdViewReversedItems.Columns("Plant"), msg)
            End If

            If String.IsNullOrEmpty(BusinessArea) Then
                msg = "Business Area not entered."
                grdViewReversedItems.SetColumnError(grdViewReversedItems.Columns("BusinessArea"), msg)
            End If

            If String.IsNullOrEmpty(StorageLocation) Then
                msg = "Storage Location not entered."
                grdViewReversedItems.SetColumnError(grdViewReversedItems.Columns("StorageLocation"), msg)
            End If

            If String.IsNullOrEmpty(ReasonForMovement) Then
                msg = "Reason For Movement not entered."
                grdViewReversedItems.SetColumnError(grdViewReversedItems.Columns("ReasonForMovement"), msg)
            End If

            If Not String.IsNullOrEmpty(msg) Then
                Return My.Resources.CanNotSave
            End If
        Next
        Return msg
    End Function


    Private Function SaveGridItems() As String
        Dim msg As String = String.Empty
        For RowIndex As Integer = 0 To grdViewReversedItems.RowCount - 1
            'Save items to database.
            Dim SerialGUID As Guid = grdViewReversedItems.GetRowCellValue(RowIndex, "ItemSerialGUID")
            Dim ManPartNo As String = grdViewReversedItems.GetRowCellValue(RowIndex, "ManufacturePartNo")
            Dim SAPPartNo As String = grdViewReversedItems.GetRowCellValue(RowIndex, "SAPPartNo")
            Dim DocType As String = grdViewReversedItems.GetRowCellValue(RowIndex, "DocType")

            Dim TransName As String = String.Empty

            Dim MovementType As Integer = 0
            If DocType = ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.GR.ToString Then
                MovementType = 102
                TransName = DocumentsTypes.ReversalGR.ToString
            ElseIf DocType = ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.GINV.ToString Then
                MovementType = 242
                TransName = DocumentsTypes.ReversalInvPro.ToString
            ElseIf DocType = ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.GIPOR.ToString Then
                MovementType = 202
                TransName = DocumentsTypes.ReversalPOR.ToString
            End If

            Dim objDocItem As New ZulAssetsBL.SAPItemSerialsTransBLL
            objDocItem.NewRecord()
            objDocItem.Attributes.CreationDate = Now.Date
            objDocItem.Attributes.CreatedBy = AppConfigData.UserGUID
            objDocItem.Attributes.DocGUID = RecordGUID
            objDocItem.Attributes.ItemSerialGUID = SerialGUID
            objDocItem.Attributes.MovementType = MovementType

            objDocItem.Attributes.TransName = TransName
            objDocItem.Attributes.SAPMaterialDocNo = grdViewReversedItems.GetRowCellValue(RowIndex, "SAPMaterialDocNo").ToString
            objDocItem.Attributes.SAPMaterialDocLineNo = grdViewReversedItems.GetRowCellValue(RowIndex, "SAPMaterialDocLineNo").ToString
            objDocItem.Attributes.OrgItemSerialTransGUID = grdViewReversedItems.GetRowCellValue(RowIndex, "GUID")
            objDocItem.Attributes.ReasonOfFault = grdViewReversedItems.GetRowCellValue(RowIndex, "ReasonForMovement")
            objDocItem.Attributes.Plant = grdViewReversedItems.GetRowCellValue(RowIndex, "Plant")
            objDocItem.Attributes.Location = grdViewReversedItems.GetRowCellValue(RowIndex, "StorageLocation")

            objDocItem.Attributes.LineItemNo = grdViewReversedItems.GetRowCellValue(RowIndex, "LineItemNo")
            objDocItem.Attributes.PONo = grdViewReversedItems.GetRowCellValue(RowIndex, "PONo")
            objDocItem.Attributes.GLAC = grdViewReversedItems.GetRowCellValue(RowIndex, "GLAC")
            objDocItem.Attributes.CostCenter = grdViewReversedItems.GetRowCellValue(RowIndex, "CostCenter")
            objDocItem.Attributes.BusinessArea = grdViewReversedItems.GetRowCellValue(RowIndex, "BusinessArea")
            objDocItem.Attributes.AssetNo = grdViewReversedItems.GetRowCellValue(RowIndex, "AssetNo")

            objDocItem.Attributes.ExportedFlag = False
            objDocItem.Attributes.LastEditDate = Now.Date
            objDocItem.Attributes.LastEditBY = AppConfigData.UserGUID
            msg = objDocItem.Save()
            If Not String.IsNullOrEmpty(msg) Then
                Return msg
            End If
        Next
        Return msg
    End Function

    Private Sub btnReverseDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverseDoc.Click
        Dim FochRow As Integer = grdViewItems.FocusedRowHandle
        If FochRow >= 0 Then
            Dim DocGUID As Guid = grdViewItems.GetRowCellValue(FochRow, "DocGUID")
            'Filter the grid by Docguid
            grdViewItems.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "DocGUID", DocGUID)
            For i As Integer = 0 To grdViewItems.RowCount - 1
                ReferseItem(i, False)
            Next
            grdViewItems.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "DocGUID", Nothing)
        End If

    End Sub

    Private Sub ReferseItem(ByVal RowHandle As Integer, ByVal ShowErrorMessage As Boolean)
        Dim ItemSerialGUID As Guid = grdViewItems.GetRowCellValue(RowHandle, "ItemSerialGUID")

        Dim ExistRow As Long = grdViewReversedItems.LocateByValue(0, grdViewItems.Columns("ItemSerialGUID"), ItemSerialGUID)
        If ExistRow <> DevExpress.XtraGrid.GridControl.InvalidRowHandle Then
            If ShowErrorMessage Then
                Messages.ErrorMessage("Item Serial already exist.")
            End If
        Else

            Dim DocType As String = grdViewItems.GetRowCellValue(RowHandle, "DocType")

            Dim NewRowHandle As Integer = DevExpress.XtraGrid.GridControl.NewItemRowHandle

            grdViewReversedItems.AddNewRow()
            grdViewReversedItems.SetRowCellValue(NewRowHandle, "GUID", grdViewItems.GetRowCellValue(RowHandle, "GUID"))
            grdViewReversedItems.SetRowCellValue(NewRowHandle, "DocType", DocType)
            grdViewReversedItems.SetRowCellValue(NewRowHandle, "DocNo", grdViewItems.GetRowCellValue(RowHandle, "DocNo"))

            grdViewReversedItems.SetRowCellValue(NewRowHandle, "SAPMaterialDocNo", "")
            grdViewReversedItems.SetRowCellValue(NewRowHandle, "SAPMaterialDocLineNo", "")
            grdViewReversedItems.SetRowCellValue(NewRowHandle, "SAPPartNo", grdViewItems.GetRowCellValue(RowHandle, "SAPPartNo"))
            grdViewReversedItems.SetRowCellValue(NewRowHandle, "SerialNo", grdViewItems.GetRowCellValue(RowHandle, "SerialNo"))
            grdViewReversedItems.SetRowCellValue(NewRowHandle, "ItemSerialGUID", ItemSerialGUID)

            grdViewReversedItems.SetRowCellValue(NewRowHandle, "LineItemNo", grdViewItems.GetRowCellValue(RowHandle, "LineItemNo").ToString)
            grdViewReversedItems.SetRowCellValue(NewRowHandle, "PONo", grdViewItems.GetRowCellValue(RowHandle, "PONo").ToString)
            grdViewReversedItems.SetRowCellValue(NewRowHandle, "GLAC", grdViewItems.GetRowCellValue(RowHandle, "GLAC").ToString)
            grdViewReversedItems.SetRowCellValue(NewRowHandle, "CostCenter", grdViewItems.GetRowCellValue(RowHandle, "CostCenter").ToString)
            grdViewReversedItems.SetRowCellValue(NewRowHandle, "BusinessArea", grdViewItems.GetRowCellValue(RowHandle, "BusinessArea").ToString)
            grdViewReversedItems.SetRowCellValue(NewRowHandle, "AssetNo", grdViewItems.GetRowCellValue(RowHandle, "AssetNo").ToString)

            grdViewReversedItems.UpdateCurrentRow()
        End If

        grdReversedItems.Focus()
    End Sub

    Private Sub btnReverseItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverseItem.Click
        For FochRow As Integer = 0 To grdViewItems.RowCount - 1
            If grdViewItems.GetRowCellValue(FochRow, "Selection") Then
                ReferseItem(FochRow, False)
            End If
        Next
    End Sub

    'Private Sub grdViewReversedItems_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles grdViewReversedItems.InvalidRowException
    '    'Suppress displaying the error message box
    '    e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction
    'End Sub

    'Private Sub grdViewReversedItems_ValidateRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles grdViewReversedItems.ValidateRow
    '    e.Valid = True
    '    Dim DocNovalue As Decimal = grdViewReversedItems.GetRowCellValue(e.RowHandle, "SAPMaterialDocNo")
    '    If DocNovalue < 0 Then
    '        e.ErrorText = "Invalid Value for SAP Material Doc No, "
    '        e.Valid = False
    '    End If

    '    Dim DocLineNovalue As Decimal = grdViewReversedItems.GetRowCellValue(e.RowHandle, "SAPMaterialDocLineNo")
    '    If DocLineNovalue < 0 Then
    '        e.ErrorText = "Invalid Value for SAP Material Doc Line No, "
    '        e.Valid = False
    '    End If
    'End Sub

End Class
