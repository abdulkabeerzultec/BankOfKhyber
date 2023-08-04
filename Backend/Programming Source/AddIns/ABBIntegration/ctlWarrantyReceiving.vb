Imports ZulAssetsBL.SAPDocTypesBLL
Imports System.Windows.Forms

Public Class ctlWarrantyReceiving
    Inherits ZulLib.ctlDataEditing

    Dim objDoc As New ZulAssetsBL.SAPDocumentsBLL
    Dim objDocItems As New ZulAssetsBL.SAPItemSerialsTransBLL
    Dim objItemSerial As New ZulAssetsBL.SAPItemSerialsBLL

    Private _ReciveType As ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes

    Public Property ReciveType() As ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes
        Get
            Return _ReciveType
        End Get
        Set(ByVal value As ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes)
            _ReciveType = value
            If _ReciveType = DocumentsTypes.WarrantyReceivingSame Then
                NavigationFilter = String.Format("DocType = '{0}'", DocumentsTypes.WarrantyReceivingSame.ToString)
            Else
                NavigationFilter = String.Format("DocType = '{0}'", DocumentsTypes.WarrantyReceivingReplace.ToString)
            End If
        End Set
    End Property

    Private Sub ctlWarrantyReceiving_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BusnissLayerObject = objDoc
        LoadContolSettings(Me)
        NewData()
        ListDataLoad()
        btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        txtDocNo.Properties.ReadOnly = True
    End Sub

    Private Sub ListDataLoad()
        If ReciveType = DocumentsTypes.WarrantyReceivingSame Then
            Me.ListDataSource = objDoc.GetListDataByDocType(DocumentsTypes.WarrantyReceivingSame.ToString)
        Else
            Me.ListDataSource = objDoc.GetListDataByDocType(DocumentsTypes.WarrantyReceivingReplace.ToString)
        End If
    End Sub

    Private Sub ctlWarrantyReceiving_OnLoadData() Handles Me.OnLoadData
        Try
            Dim Msg As String = objDoc.Edit(RecordGUID)
            If String.IsNullOrEmpty(Msg) Then
                txtDocNo.Text = objDoc.Attributes.DocNo
                dtDocDate.DateTime = objDoc.Attributes.DocDate
                dtExpectedDate.Text = objDoc.Attributes.ExpectedReturnDate
                cmbWarrantClaim.EditValue = objDoc.Attributes.WarrantyClaimDocGUID
                grdItems_OnLoadData(False)
            Else
                Messages.ErrorMessage(Msg, WhoCalledMe)
                ActionResult = -1
            End If
        Catch ex As Exception
            Messages.ErrorMessage(ex.Message, WhoCalledMe)
            ActionResult = -1
        End Try
    End Sub

    Private Sub GetWarrantyClaimDoc()
        cmbWarrantClaim.Properties.DataSource = objDoc.GetWarrantyClaimListDataByDocType(DocumentsTypes.WarrantyClaim.ToString)
        cmbWarrantClaim.Properties.View.OptionsView.ShowAutoFilterRow = True
        cmbWarrantClaim.Properties.View.Columns("TransGUID").Visible = False
        cmbWarrantClaim.Properties.DisplayMember = objDoc.ListDisplayMember
        cmbWarrantClaim.Properties.ValueMember = objDoc.ListValueMember
        cmbWarrantClaim.Properties.View.Columns(objDoc.ListValueMember).Visible = False
    End Sub

    Private Sub ctlWarrantyReceiving_OnNewData() Handles Me.OnNewData
        GetWarrantyClaimDoc()
        grdItems_OnLoadData(True)
        dtDocDate.DateTime = Now.Date
        txtDocNo.Text = ZulAssetsBL.SAPDocTypesBLL.GetDocCode
        dtDocDate.Select()
        ActionResult = 0
    End Sub
    Private Sub grdItems_OnLoadData(ByVal Editable As Boolean)
        'Cancel current row, if it has been added before loading the data.
        grdViewItems.CancelUpdateCurrentRow()
        If ReciveType = DocumentsTypes.WarrantyReceivingSame Then
            grdItems.DataSource = objDocItems.GetGridDataWarrantyReceiveSame(RecordGUID, True)
            grdItems_FormatGridReceiveSame(Editable)
        Else
            grdItems.DataSource = objDocItems.GetGridDataWarrantyReceiveReplace(RecordGUID, True)
            grdItems_FormatGridReceiveReplace(Editable)
        End If
    End Sub

    Private Sub grdItems_FormatGridReceiveSame(ByVal Editable As Boolean)
        grdViewItems.OptionsBehavior.Editable = Editable
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In grdViewItems.Columns
            col.OptionsColumn.AllowEdit = False
        Next

        grdViewItems.Columns("GUID").Visible = False
        grdViewItems.Columns("ItemSerialGUID").Visible = False
        If Not Editable Then
            grdViewItems.Columns("Selection").Visible = False
        Else
            grdViewItems.Columns("Selection").Visible = True
        End If
        grdViewItems.Columns("Selection").OptionsColumn.AllowEdit = True
        grdViewItems.Columns("Selection").MaxWidth = 25
        grdViewItems.Columns("Selection").Caption = String.Empty
        'grdViewItems.Columns("ReasonOfFault").OptionsColumn.AllowEdit = True

        grdItems.UseEmbeddedNavigator = True
        grdItems.EmbeddedNavigator.Buttons.Append.Visible = False
        grdItems.EmbeddedNavigator.Buttons.Remove.Visible = False
    End Sub

    Private Sub grdItems_FormatGridReceiveReplace(ByVal Editable As Boolean)
        grdViewItems.OptionsBehavior.Editable = Editable
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In grdViewItems.Columns
            col.OptionsColumn.AllowEdit = False
        Next
        grdViewItems.Columns("GUID").Visible = False
        grdViewItems.Columns("ItemSerialGUID").Visible = False
        If Not Editable Then
            grdViewItems.Columns("Selection").Visible = False
        Else
            grdViewItems.Columns("Selection").Visible = True
        End If
        grdViewItems.Columns("Selection").OptionsColumn.AllowEdit = True
        grdViewItems.Columns("Selection").MaxWidth = 25
        grdViewItems.Columns("Selection").Caption = String.Empty

        'grdViewItems.Columns("ReasonOfFault").OptionsColumn.AllowEdit = True
        grdViewItems.Columns("SerialNo").OptionsColumn.AllowEdit = True
        grdViewItems.Columns("ManPartNo").OptionsColumn.AllowEdit = True

        grdItems.UseEmbeddedNavigator = True
        grdItems.EmbeddedNavigator.Buttons.Append.Visible = False
        grdItems.EmbeddedNavigator.Buttons.Remove.Visible = False
    End Sub

    Private Function ValidateGridItems() As String
        Dim msg As String = String.Empty
        For RowIndex As Integer = 0 To grdViewItems.RowCount - 1
            If grdViewItems.GetRowCellValue(RowIndex, "Selection") = True Then
                grdViewItems.ClearColumnErrors()
                grdViewItems.FocusedRowHandle = RowIndex

                Dim SerialNo As String = grdViewItems.GetRowCellValue(RowIndex, "SerialNo").ToString
                Dim ManPartNo As String = grdViewItems.GetRowCellValue(RowIndex, "ManPartNo").ToString
                If String.IsNullOrEmpty(SerialNo) Then
                    msg = "Serial No not entered."
                    grdViewItems.SetColumnError(grdViewItems.Columns("SerialNo"), msg)
                End If
                If String.IsNullOrEmpty(ManPartNo) Then
                    msg = "Man Part No not entered."
                    grdViewItems.SetColumnError(grdViewItems.Columns("ManPartNo"), msg)
                End If
                If Not String.IsNullOrEmpty(msg) Then
                    Return My.Resources.CanNotSave
                End If
            End If
        Next
        Return msg
    End Function

    Private Sub ctlWarrantyReceiving_OnSaveData() Handles Me.OnSaveData
        If RecordStatus = TRecordStates.ModifiedRecord Then
            Messages.ErrorMessage("Document already saved, no modifications are allowed after saving.")
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

        If GetSelectedRowCount() <= 0 Then
            Messages.ErrorMessage(My.Resources.NoItemsFound)
            ActionResult = -1
            Exit Sub
        End If

        If ReciveType = DocumentsTypes.WarrantyReceivingReplace Then
            Dim msg As String = String.Empty
            msg = ValidateGridItems()
            If Not String.IsNullOrEmpty(msg) Then
                Messages.ErrorMessage(msg)
                ActionResult = -1
                Exit Sub
            End If
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
        objDoc.Attributes.DocType = ReciveType.ToString
        objDoc.Attributes.DocNo = txtDocNo.Text
        objDoc.Attributes.DocDate = dtDocDate.DateTime
        objDoc.Attributes.WarrantyClaimDocGUID = cmbWarrantClaim.EditValue
        objDoc.Attributes.ExpectedReturnDate = dtExpectedDate.DateTime

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
    Private Function GetSelectedRowCount() As Integer
        Dim Count As Integer = 0
        For RowIndex As Integer = 0 To grdViewItems.RowCount - 1
            If grdViewItems.GetRowCellValue(RowIndex, "Selection") = True Then
                Count += 1
            End If
        Next
        Return Count
    End Function

    Private Function SaveGridItems() As String
        Dim msg As String = String.Empty
        For RowIndex As Integer = 0 To grdViewItems.RowCount - 1
            If grdViewItems.GetRowCellValue(RowIndex, "Selection") = True Then
                Dim ManPartNo As String = grdViewItems.GetRowCellValue(RowIndex, "ManPartNo").ToString
                Dim SAPPartNo As String = grdViewItems.GetRowCellValue(RowIndex, "SAPPartNo").ToString
                Dim serialNo As String = grdViewItems.GetRowCellValue(RowIndex, "SerialNo")
                Dim SerialGUID As Guid = grdViewItems.GetRowCellValue(RowIndex, "ItemSerialGUID")
                Dim objItem As New ZulAssetsBL.SAPItemsBLL
                Dim Warranty As Integer = objItem.GetWarrantyBySAPNo(SAPPartNo)
                If ReciveType = DocumentsTypes.WarrantyReceivingReplace Then
                    SerialGUID = objItemSerial.GetItemGUIDBySerial(serialNo, ManPartNo, SAPPartNo)
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
                        objItemSerial.Attributes.LastEditDate = Now.Date
                        objItemSerial.Attributes.LastEditBY = AppConfigData.UserGUID
                        msg = objItemSerial.Save()
                        If Not String.IsNullOrEmpty(msg) Then
                            Return msg
                        End If

                        SerialGUID = objItemSerial.Attributes.GUID
                    End If
                End If

                Dim objDocItems As New ZulAssetsBL.SAPItemSerialsTransBLL
                objDocItems.NewRecord()
                objDocItems.Attributes.CreationDate = Now.Date
                objDocItems.Attributes.CreatedBy = AppConfigData.UserGUID
                objDocItems.Attributes.DocGUID = RecordGUID
                objDocItems.Attributes.ItemSerialGUID = SerialGUID
                objDocItems.Attributes.TransName = ReciveType.ToString
                objDocItems.Attributes.ReasonOfFault = grdViewItems.GetRowCellValue(RowIndex, "ReasonOfFault").ToString
                objDocItems.Attributes.EmpNo = grdViewItems.GetRowCellValue(RowIndex, "EmpNo").ToString
                objDocItems.Attributes.InvProposalNo = grdViewItems.GetRowCellValue(RowIndex, "InvProposalNo").ToString
                objDocItems.Attributes.CostCenter = grdViewItems.GetRowCellValue(RowIndex, "CostCenter").ToString
                objDocItems.Attributes.ExportedFlag = False
                If ReciveType = DocumentsTypes.WarrantyReceivingReplace Then
                    Dim OrgSerialGUID As Guid = grdViewItems.GetRowCellValue(RowIndex, "ItemSerialGUID")
                    objDocItems.Attributes.OrgItemSerialGUID = OrgSerialGUID
                End If
                objDocItems.Attributes.OrgItemSerialTransGUID = grdViewItems.GetRowCellValue(RowIndex, "GUID")
                objDocItems.Attributes.LastEditDate = Now.Date
                objDocItems.Attributes.LastEditBY = AppConfigData.UserGUID
                msg = objDocItems.Save()
                If Not String.IsNullOrEmpty(msg) Then
                    Return msg
                End If
            End If
        Next
        Return msg
    End Function

    Private Sub cmbWarrantClaim_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbWarrantClaim.EditValueChanged
        If cmbWarrantClaim.EditValue <> Nothing AndAlso RecordStatus = TRecordStates.NewRecord Then
            Dim obj As New ZulAssetsBL.SAPDocumentsBLL
            obj.Edit(cmbWarrantClaim.EditValue)
            If Not obj.Attributes.IsExpectedReturnDateNull Then
                dtExpectedDate.DateTime = obj.Attributes.ExpectedReturnDate
            Else
                dtExpectedDate.EditValue = Nothing
            End If

            grdViewItems.CancelUpdateCurrentRow()
            If ReciveType = DocumentsTypes.WarrantyReceivingSame Then
                grdItems.DataSource = objDocItems.GetGridDataWarrantyReceiveSame(cmbWarrantClaim.EditValue, False)
                grdItems_FormatGridReceiveSame(True)
            Else
                grdItems.DataSource = objDocItems.GetGridDataWarrantyReceiveReplace(cmbWarrantClaim.EditValue, False)
                grdItems_FormatGridReceiveReplace(True)
            End If
        End If

    End Sub
End Class
