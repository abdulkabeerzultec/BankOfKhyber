Imports System.Windows.Forms
Imports ZulAssetsBL.SAPDocTypesBLL

Public Class ctlGoodsIssue
    Inherits ZulLib.ctlDataEditing


    Private _IssueType As DocumentsTypes

    Private RitmSerialNo As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Private RitmAssetNumber As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit

    Dim objGI As New ZulAssetsBL.SAPDocumentsBLL
    Dim objGIItem As New ZulAssetsBL.SAPItemSerialsTransBLL
    Dim objItem As New ZulAssetsBL.SAPItemsBLL
    Dim objItemSerial As New ZulAssetsBL.SAPItemSerialsBLL

    Private objBALIntegration As New ZulAssetsBL.ZulAssetBAL.BALABBIntegration

    Private Sub ctlGoodsIssue_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BusnissLayerObject = objGI
        LoadContolSettings(Me)
        NewData()
        ListDataLoad()
        btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        txtGINo.Properties.ReadOnly = True
    End Sub

    Public Property IssueType() As DocumentsTypes
        Get
            Return _IssueType
        End Get
        Set(ByVal value As DocumentsTypes)
            _IssueType = value
            If _IssueType = DocumentsTypes.GINV Then
                LayoutIssueType.Text = "Inv.Pro#"
                cmbEmpNumber.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
                NavigationFilter = String.Format("DocType = '{0}'", DocumentsTypes.GINV.ToString)
            Else
                LayoutIssueType.Text = "P.O.R#"
                cmbEmpNumber.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
                NavigationFilter = String.Format("DocType = '{0}'", DocumentsTypes.GIPOR.ToString)
            End If
        End Set
    End Property

    'Private Sub ctlGoodsIssue_OnDeleteData() Handles Me.OnDeleteData
    '    Try
    '        Dim Msg As String = objGI.DeleteByRowGUID(RecordGUID)
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
        If IssueType = DocumentsTypes.GINV Then
            Me.ListDataSource = objGI.GetListDataByDocType(ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.GINV.ToString)
        Else
            Me.ListDataSource = objGI.GetListDataByDocType(ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.GIPOR.ToString)
        End If
    End Sub

    Private Sub ctlGoodsIssue_OnLoadData() Handles Me.OnLoadData
        Try
            Dim Msg As String = objGI.Edit(RecordGUID)
            If String.IsNullOrEmpty(Msg) Then
                txtGINo.Text = objGI.Attributes.DocNo
                If IssueType = DocumentsTypes.GINV Then
                    If Not objGI.Attributes.IsInvProposalNoNull Then
                        txtInvP_POR.Text = objGI.Attributes.InvProposalNo
                    End If
                Else
                    If Not objGI.Attributes.IsPORequisitionNoNull Then
                        txtInvP_POR.Text = objGI.Attributes.PORequisitionNo
                    End If
                End If
                txtRemarks.Text = objGI.Attributes.Remarks
                cmbEmpNumber.EditValue = objGI.Attributes.EmpNo
                txtEmpName.Text = objGI.Attributes.EmpName
                dtDocDate.DateTime = objGI.Attributes.DocDate
                grdGIItems_OnLoadData(False)
                If IssueType = DocumentsTypes.GINV Then
                    txtInvP_POR.Properties.ReadOnly = True
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

    Private Sub ctlGoodsIssue_OnNewData() Handles Me.OnNewData
        dtDocDate.DateTime = Now.Date
        GetEmpList(String.Empty) 'Fill All the employees
        grdGIItems_OnLoadData(True)
        If IssueType = DocumentsTypes.GINV Then
            txtInvP_POR.Properties.ReadOnly = False
        End If
        ActionResult = 0
        txtGINo.Text = ZulAssetsBL.SAPDocTypesBLL.GetDocCode
        txtInvP_POR.Select()
    End Sub

    Private Sub ctlGoodsIssue_OnSaveData() Handles Me.OnSaveData
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
                objGI.NewRecord()
                RecordGUID = objGI.Attributes.GUID

                objGI.Attributes.CreationDate = Now.Date
                objGI.Attributes.CreatedBy = AppConfigData.UserGUID
            Else
                ActionResult = -1
                Exit Sub
            End If
        End If

        If IssueType = DocumentsTypes.GINV Then
            objGI.Attributes.InvProposalNo = txtInvP_POR.Text
            objGI.Attributes.DocType = ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.GINV.ToString
        Else
            objGI.Attributes.DocType = ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.GIPOR.ToString
            objGI.Attributes.PORequisitionNo = txtInvP_POR.Text
        End If

        objGI.Attributes.DocNo = txtGINo.Text
        objGI.Attributes.EmpNo = cmbEmpNumber.Text
        objGI.Attributes.EmpName = txtEmpName.Text
        objGI.Attributes.DocDate = dtDocDate.DateTime
        objGI.Attributes.Remarks = txtRemarks.Text

        'objGI.Attributes.DeviceID = -1
        objGI.Attributes.LastEditDate = Now.Date
        objGI.Attributes.LastEditBY = AppConfigData.UserGUID
        UpdateStatusbarInfo(objGI.Attributes.CreatedBy, objGI.Attributes.CreationDate, objGI.Attributes.LastEditBY, objGI.Attributes.LastEditDate, lblCreated, lblModified)

        Try
            Dim Msg As String = objGI.Save()
            If String.IsNullOrEmpty(Msg) Then
                'Save the grid data only if the record is new.
                If RecordStatus = TRecordStates.NewRecord Then
                    Msg = SaveGridItems()
                End If

                If String.IsNullOrEmpty(Msg) Then
                    ActionResult = 0
                    ListDataLoad()
                    grdViewItems.OptionsBehavior.Editable = False
                    If IssueType = DocumentsTypes.GINV Then
                        txtInvP_POR.Properties.ReadOnly = True
                    End If
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
            'Save items to database.
            Dim SerialGUID As Guid = grdViewItems.GetRowCellValue(RowIndex, "ItemSerialGUID")
            Dim ManPartNo As String = grdViewItems.GetRowCellValue(RowIndex, "ManufacturePartNo")
            Dim SAPPartNo As String = grdViewItems.GetRowCellValue(RowIndex, "SAPPartNo")

            Dim objDocItems As New ZulAssetsBL.SAPItemSerialsTransBLL
            objDocItems.NewRecord()
            objDocItems.Attributes.CreationDate = Now.Date
            objDocItems.Attributes.CreatedBy = AppConfigData.UserGUID
            objDocItems.Attributes.DocGUID = RecordGUID
            objDocItems.Attributes.ItemSerialGUID = SerialGUID
            objDocItems.Attributes.LineItemNo = grdViewItems.GetRowCellValue(RowIndex, "LineItemNo")
            objDocItems.Attributes.EmpNo = cmbEmpNumber.Text




            If IssueType = DocumentsTypes.GINV Then
                objDocItems.Attributes.MovementType = 241
                objDocItems.Attributes.TransName = ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.GINV.ToString
                objDocItems.Attributes.AssetNo = grdViewItems.GetRowCellValue(RowIndex, "AssetNo")
                objDocItems.Attributes.InvProposalNo = txtInvP_POR.Text

            Else
                objDocItems.Attributes.MovementType = 201
                objDocItems.Attributes.TransName = ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.GIPOR.ToString
                objDocItems.Attributes.CostCenter = grdViewItems.GetRowCellValue(RowIndex, "CostCenter").ToString
                objDocItems.Attributes.BusinessArea = grdViewItems.GetRowCellValue(RowIndex, "BusinessArea").ToString
                objDocItems.Attributes.GLAC = grdViewItems.GetRowCellValue(RowIndex, "GLAC").ToString
                objDocItems.Attributes.PORequisitionNo = txtInvP_POR.Text
            End If
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

    'Private Sub grdGIItems_OnbtnDeleteClicked(ByVal ParentGuid As System.Guid)
    '    Try
    '        If ParentGuid <> Guid.Empty Then
    '            Dim Msg As String = objGIItem.DeleteByRowGUID(ParentGuid)
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
        If obj IsNot DBNull.Value Then

            objItemSerial.Edit(obj)
            objItem.Edit(objItemSerial.Attributes.SAPPartNo)
            grdViewItems.SetRowCellValue(FocRow, "SAPPartNo", objItemSerial.Attributes.SAPPartNo)
            grdViewItems.SetRowCellValue(FocRow, "ManufacturePartNo", objItemSerial.Attributes.ManufacturePartNo)
            grdViewItems.SetRowCellValue(FocRow, "AssetNo", Nothing)

            If IssueType = DocumentsTypes.GINV Then
                Dim InvNo As String = txtInvP_POR.Text
                Dim dt As DataTable = objBALIntegration.GetInvProposalAssetsList(InvNo)
                RitmAssetNumber.DataSource = dt
            End If

            If objItem.Attributes.CheckUniqueSNOnReceiving Then
                Dim Rowhandle As Long = grdViewItems.LocateByValue(0, grdViewItems.Columns("ItemSerialGUID"), obj)
                If Rowhandle <> DevExpress.XtraGrid.GridControl.InvalidRowHandle Then
                    Messages.ErrorMessage("Item Serial already existed in the grid.")
                    grdViewItems.SetRowCellValue(FocRow, "ItemSerialGUID", Nothing)
                    grdViewItems.SetRowCellValue(FocRow, "SAPPartNo", Nothing)
                    grdViewItems.SetRowCellValue(FocRow, "ManufacturePartNo", Nothing)
                ElseIf objItemSerial.Attributes.LastStatus <> DocumentsTypes.GR.ToString Then
                    Messages.ErrorMessage(String.Format("Item SerialNo status is({0}), you can only issue received items(GR), select another SerialNo and try again.", objItemSerial.Attributes.LastStatus))
                    grdViewItems.SetRowCellValue(FocRow, "ItemSerialGUID", Nothing)
                    grdViewItems.SetRowCellValue(FocRow, "SAPPartNo", Nothing)
                    grdViewItems.SetRowCellValue(FocRow, "ManufacturePartNo", Nothing)
                End If
            End If
        End If
    End Sub

    Private Sub grdGIItems_OnLoadData(ByVal Editable As Boolean)
        'Cancel current row, if it has been added before loading the data.
        grdViewItems.CancelUpdateCurrentRow()

        grdItems.DataSource = objGIItem.GetGridDataGI(RecordGUID)
        grdGRItems_FormatGrid(Editable)
    End Sub

    Private Sub grdGRItems_FormatGrid(ByVal Editable As Boolean)
        grdViewItems.OptionsBehavior.Editable = Editable
        grdViewItems.Columns("GUID").Visible = False
        grdViewItems.Columns("SAPPartNo").OptionsColumn.AllowEdit = False
        grdViewItems.Columns("ManufacturePartNo").OptionsColumn.AllowEdit = False

        grdViewItems.Columns("ManufacturePartNo").VisibleIndex = 2

        grdViewItems.Columns("ItemSerialGUID").VisibleIndex = 0
        grdViewItems.Columns("SAPPartNo").VisibleIndex = 1
        grdViewItems.Columns("ManufacturePartNo").VisibleIndex = 2
        grdViewItems.Columns("LineItemNo").VisibleIndex = 3


        RitmSerialNo.View.OptionsView.ShowIndicator = False
        AddHandler RitmSerialNo.EditValueChanged, AddressOf cmbSerialNo_EditValueChanged
        grdViewItems.Columns("ItemSerialGUID").ColumnEdit = RitmSerialNo
        grdViewItems.Columns("ItemSerialGUID").OptionsColumn.AllowEdit = True
        grdViewItems.Columns("ItemSerialGUID").Caption = "Serial Number"

        RitmSerialNo.DataSource = objItemSerial.GetItemssSerialList
        RitmSerialNo.DisplayMember = objItemSerial.ListDisplayMember
        RitmSerialNo.NullText = String.Empty
        RitmSerialNo.ValueMember = objItemSerial.ListValueMember
        RitmSerialNo.View.Columns("GUID").Visible = False
        RitmSerialNo.View.Columns("LastStatus").Visible = False
        'RitmSerialNo.View.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "LastStatus", "GR")

        If IssueType = DocumentsTypes.GINV Then
            grdViewItems.Columns("CostCenter").Visible = False
            grdViewItems.Columns("BusinessArea").Visible = False
            grdViewItems.Columns("GLAC").Visible = False
            grdViewItems.Columns("AssetNo").Visible = True

            RitmAssetNumber.View.OptionsView.ShowIndicator = False
            grdViewItems.Columns("AssetNo").ColumnEdit = RitmAssetNumber
            grdViewItems.Columns("AssetNo").OptionsColumn.AllowEdit = True
            grdViewItems.Columns("AssetNo").Caption = "Asset No"
            RitmAssetNumber.DisplayMember = "RefNo"
            RitmAssetNumber.NullText = String.Empty
            RitmAssetNumber.ValueMember = "RefNo"


            Dim InvNo As String = txtInvP_POR.Text
            Dim dt As DataTable = objBALIntegration.GetInvProposalAssetsList(InvNo)
            RitmAssetNumber.DataSource = dt

        ElseIf IssueType = DocumentsTypes.GIPOR Then
            grdViewItems.Columns("CostCenter").Visible = True
            grdViewItems.Columns("BusinessArea").Visible = True
            grdViewItems.Columns("GLAC").Visible = True
            grdViewItems.Columns("AssetNo").Visible = False
        End If

        Dim RIPOLine As New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
        RIPOLine.MinValue = 1
        RIPOLine.MaxValue = Decimal.MaxValue
        RIPOLine.IsFloatValue = False
        RIPOLine.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdViewItems.Columns("LineItemNo").ColumnEdit = RIPOLine
        grdViewItems.Columns("LineItemNo").OptionsColumn.AllowEdit = True
        grdViewItems.Columns("LineItemNo").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdItems.UseEmbeddedNavigator = True
    End Sub


    Private Sub txtInv_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInvP_POR.Leave
        If IssueType = DocumentsTypes.GINV Then
            GetEmpList(txtInvP_POR.Text)
        End If
    End Sub

    Private Sub GetEmpList(ByVal InvNumber As String)
        Dim dt As DataTable
        errProv.ClearErrors()
        cmbEmpNumber.Properties.DisplayMember = "ID"
        cmbEmpNumber.Properties.ValueMember = "ID"
        If String.IsNullOrEmpty(InvNumber) Then
            Dim cust As New ZulAssetsBL.ZulAssetBAL.BALCustodian
            dt = cust.GetAllData_GetCombo()
        Else
            dt = objBALIntegration.GetInvProposalEmpList(InvNumber)
            If dt.Rows.Count <= 0 Then
                cmbEmpNumber.Text = String.Empty
                cmbEmpNumber.Properties.DataSource = Nothing
                errProv.SetError(txtInvP_POR, "Investment proposal number not found, please try again. ")
            End If
        End If
        cmbEmpNumber.Properties.DataSource = dt
    End Sub

    Private Sub cmbEmpNumber_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEmpNumber.EditValueChanged
        Dim cust As New ZulAssetsBL.ZulAssetBAL.BALCustodian
        Dim objattCust As New ZulAssetsDAL.ZulAssetsDAL.attCustodian
        If Not String.IsNullOrEmpty(cmbEmpNumber.Text) Then
            objattCust.PKeyCode = cmbEmpNumber.Text
            Dim dt As DataTable = cust.CheckID(objattCust)
            If dt.Rows.Count > 0 Then
                txtEmpName.Text = dt.Rows(0)("CustodianName")
            End If
        Else
            txtEmpName.Text = String.Empty
        End If
    End Sub

    'Private Sub grdViewItems_ShowingEditor(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdViewItems.ShowingEditor
    '    Dim view As DevExpress.XtraGrid.Views.Grid.GridView = sender
    '    If view.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle AndAlso view.FocusedColumn.FieldName.ToString() = "AssetNo" Then
    '        Dim InvNo As String = txtInvP_POR.Text
    '        Dim dt As DataTable = objBALIntegration.GetInvProposalAssetsList(InvNo)
    '        RitmAssetNumber.DataSource = dt
    '    End If
    '    e.Cancel = False
    'End Sub

    Private Sub grdViewItems_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles grdViewItems.InitNewRow
        Dim LastRow As Integer = grdViewItems.RowCount - 2
        grdViewItems.SetRowCellValue(e.RowHandle, "GUID", Guid.NewGuid)
        If LastRow >= 0 Then
            grdViewItems.SetRowCellValue(e.RowHandle, "LineItemNo", grdViewItems.GetRowCellValue(LastRow, "LineItemNo") + 10)

            grdViewItems.SetRowCellValue(e.RowHandle, "CostCenter", grdViewItems.GetRowCellValue(LastRow, "CostCenter"))
            grdViewItems.SetRowCellValue(e.RowHandle, "BusinessArea", grdViewItems.GetRowCellValue(LastRow, "BusinessArea"))
            grdViewItems.SetRowCellValue(e.RowHandle, "GLAC", grdViewItems.GetRowCellValue(LastRow, "GLAC"))

        Else
            grdViewItems.SetRowCellValue(e.RowHandle, "LineItemNo", 10)
        End If
    End Sub

    Private Sub grdViewItemsItems_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles grdViewItems.InvalidRowException
        'Suppress displaying the error message box
        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction
    End Sub

    Private Sub grdViewItems_ValidateRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles grdViewItems.ValidateRow
        e.Valid = True
        Dim SerialNo As String = grdViewItems.GetRowCellValue(e.RowHandle, "ItemSerialGUID").ToString
        If String.IsNullOrEmpty(SerialNo) Then
            grdViewItems.SetColumnError(grdViewItems.Columns("ItemSerialGUID"), "Serial Number not entered")
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

        'Dim SerialNo As String = grdViewItems.GetRowCellValue(e.RowHandle, "ItemSerialGUID").ToString
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

    End Sub
End Class
