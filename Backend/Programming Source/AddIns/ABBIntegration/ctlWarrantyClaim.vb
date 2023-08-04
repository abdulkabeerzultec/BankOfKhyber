Imports System.Windows.Forms
Imports ZulAssetsBL.SAPDocTypesBLL

Public Class ctlWarrantyClaim
    Inherits ZulLib.ctlDataEditing

    Dim objDoc As New ZulAssetsBL.SAPDocumentsBLL
    Dim objDocItems As New ZulAssetsBL.SAPItemSerialsTransBLL
    Dim objItemSerials As New ZulAssetsBL.SAPItemSerialsBLL

    Private RitmSerialNo As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Private RitmEmp As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit

    Private objItem As New ZulAssetsBL.SAPItemsBLL

    Private Sub ctlWarrantyClaim_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BusnissLayerObject = objDoc
        LoadContolSettings(Me)
        NavigationFilter = String.Format("DocType = '{0}'", ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.WarrantyClaim.ToString)
        NewData()
        ListDataLoad()
        btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        txtDocNo.Properties.ReadOnly = True
    End Sub

    Private Sub ListDataLoad()
        Me.ListDataSource = objDoc.GetListDataByDocType(ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.WarrantyClaim.ToString)
    End Sub

    Private Sub ctlWarrantyClaim_OnLoadData() Handles Me.OnLoadData
        Try
            Dim Msg As String = objDoc.Edit(RecordGUID)
            If String.IsNullOrEmpty(Msg) Then
                txtDocNo.Text = objDoc.Attributes.DocNo
                dtDocDate.DateTime = objDoc.Attributes.DocDate
                If Not objDoc.Attributes.IsExpectedReturnDateNull Then
                    dtExpectedDate.DateTime = objDoc.Attributes.ExpectedReturnDate.ToString
                End If
                txtRemarks.Text = objDoc.Attributes.Remarks
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

    Private Sub ctlWarrantyClaim_OnNewData() Handles Me.OnNewData
        dtDocDate.DateTime = Now.Date
        grdItems_OnLoadData(True)
        txtDocNo.Text = ZulAssetsBL.SAPDocTypesBLL.GetDocCode
        dtDocDate.Select()
        ActionResult = 0
    End Sub

    Private Sub ctlWarrantyClaim_OnSaveData() Handles Me.OnSaveData
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
        If dtExpectedDate.DateTime = Nothing Then
            objDoc.Attributes.SetExpectedReturnDateNull()
        Else
            objDoc.Attributes.ExpectedReturnDate = dtExpectedDate.DateTime
        End If
        objDoc.Attributes.Remarks = txtRemarks.Text
        objDoc.Attributes.DocType = ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.WarrantyClaim.ToString

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
                    'LayoutGridItems.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
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

        'Save items to database.
        For RowIndex As Integer = 0 To grdViewItems.RowCount - 1
            Dim objDocItems As New ZulAssetsBL.SAPItemSerialsTransBLL
            objDocItems.NewRecord()
            objDocItems.Attributes.CreationDate = Now.Date
            objDocItems.Attributes.CreatedBy = AppConfigData.UserGUID
            objDocItems.Attributes.DocGUID = RecordGUID
            objDocItems.Attributes.ItemSerialGUID = grdViewItems.GetRowCellValue(RowIndex, "ItemSerialGUID")
            objDocItems.Attributes.ReasonOfFault = grdViewItems.GetRowCellValue(RowIndex, "ReasonOfFault").ToString
            objDocItems.Attributes.EmpNo = grdViewItems.GetRowCellValue(RowIndex, "EmpNo").ToString
            objDocItems.Attributes.InvProposalNo = grdViewItems.GetRowCellValue(RowIndex, "InvProposalNo").ToString
            objDocItems.Attributes.CostCenter = grdViewItems.GetRowCellValue(RowIndex, "CostCenter").ToString

            'We are storing the OrgItemSerialTransGUID in the GUID field of the grid, on cmbSerialNo_EditValueChanged
            objDocItems.Attributes.OrgItemSerialTransGUID = grdViewItems.GetRowCellValue(RowIndex, "GUID")

            objDocItems.Attributes.MovementType = 0
            objDocItems.Attributes.TransName = ZulAssetsBL.SAPDocTypesBLL.DocumentsTypes.WarrantyClaim.ToString
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

        RitmSerialNo.DataSource = objItemSerials.GetWarrantyItemssSerialList(RecordStatus = TRecordStates.NewRecord)
        RitmSerialNo.DisplayMember = objItemSerials.ListDisplayMember
        RitmSerialNo.NullText = String.Empty
        RitmSerialNo.ValueMember = "ItemSerialGUID"
        RitmSerialNo.View.Columns("GUID").Visible = False
        RitmSerialNo.View.Columns("ItemSerialGUID").Visible = False
        RitmSerialNo.View.OptionsView.ShowAutoFilterRow = True
        RitmSerialNo.View.ActiveFilterString = "[LastStatus] = 'GINV' or [LastStatus] = 'GIPOR'"
        'RitmSerialNo.View.ActiveFilter.Add(RitmSerialNo.View.Columns("LastStatus"), New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[LastStatus] = 'GINV' or [LastStatus] = 'GIPOR'", ""))
        'RitmSerialNo.View.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "LastStatus", "GINV Or GIPOR")

        RitmEmp.View.OptionsView.ShowIndicator = False
        grdViewItems.Columns("EmpNo").ColumnEdit = RitmEmp
        grdViewItems.Columns("EmpNo").OptionsColumn.AllowEdit = True
        AddHandler RitmEmp.EditValueChanged, AddressOf cmbEmpNo_EditValueChanged
        RitmEmp.DataSource = objItemSerials.GetWarrantyClaimEmpList() 'Get ALl Emp List
        RitmEmp.DisplayMember = "EmpNo"
        RitmEmp.NullText = String.Empty
        RitmEmp.ValueMember = "EmpNo"
        RitmEmp.View.Columns("ItemSerialGUID").Visible = False


        grdViewItems.Columns("ReasonOfFault").OptionsColumn.AllowEdit = True
        grdItems.UseEmbeddedNavigator = True
    End Sub

    Private Sub cmbSerialNo_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FocRow As Integer = grdViewItems.FocusedRowHandle
        Dim obj As Object = CType(sender, DevExpress.XtraEditors.GridLookUpEdit).EditValue
        If obj IsNot DBNull.Value Then
            'Store the ItemSerialTransGUID field in the GUID field of the grid to be able to get on GridSaveItems.
            Dim cmbSerialsFocusRowHandle = CType(sender, DevExpress.XtraEditors.GridLookUpEdit).Properties.View.FocusedRowHandle
            Dim ItemSerialTransGUID As Guid = CType(sender, DevExpress.XtraEditors.GridLookUpEdit).Properties.View.GetRowCellValue(cmbSerialsFocusRowHandle, "GUID")
            grdViewItems.SetRowCellValue(FocRow, "GUID", ItemSerialTransGUID)

            'Filter the EmpList by selected ItemSerialGUID
            RitmEmp.View.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "ItemSerialGUID", obj)
            'Clear the Employee Information.
            grdViewItems.SetRowCellValue(FocRow, "EmpNo", Nothing)
            grdViewItems.SetRowCellValue(FocRow, "InvProposalNo", Nothing)
            grdViewItems.SetRowCellValue(FocRow, "CostCenter", Nothing)

            objItemSerials.Edit(obj)
            grdViewItems.SetRowCellValue(FocRow, "SAPPartNo", objItemSerials.Attributes.SAPPartNo)
            grdViewItems.SetRowCellValue(FocRow, "ManufacturePartNo", objItemSerials.Attributes.ManufacturePartNo)

            objItem.Edit(objItemSerials.Attributes.SAPPartNo)
            If objItem.Attributes.CheckUniqueSNOnReceiving Then
                Dim Rowhandle As Long = grdViewItems.LocateByValue(0, grdViewItems.Columns("ItemSerialGUID"), obj)
                If Rowhandle <> DevExpress.XtraGrid.GridControl.InvalidRowHandle Then
                    Messages.ErrorMessage("Item Serial already existed in the grid.")
                    grdViewItems.SetRowCellValue(FocRow, "ItemSerialGUID", Nothing)
                    grdViewItems.SetRowCellValue(FocRow, "SAPPartNo", Nothing)
                    grdViewItems.SetRowCellValue(FocRow, "ManufacturePartNo", Nothing)
                ElseIf objItemSerials.Attributes.LastStatus <> DocumentsTypes.GINV.ToString And objItemSerials.Attributes.LastStatus <> DocumentsTypes.GIPOR.ToString Then
                    Messages.ErrorMessage("You can claim GI Items only, select another value and try again.")
                    grdViewItems.SetRowCellValue(FocRow, "ItemSerialGUID", Nothing)
                    grdViewItems.SetRowCellValue(FocRow, "SAPPartNo", Nothing)
                    grdViewItems.SetRowCellValue(FocRow, "ManufacturePartNo", Nothing)
                End If
            End If
        End If
    End Sub

    Private Sub cmbEmpNo_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FocRow As Integer = grdViewItems.FocusedRowHandle
        Dim cmbEmpNo As DevExpress.XtraEditors.GridLookUpEdit = CType(sender, DevExpress.XtraEditors.GridLookUpEdit)
        Dim obj As Object = cmbEmpNo.EditValue
        Dim rowhandle As Integer = cmbEmpNo.Properties.View.FocusedRowHandle
        If obj IsNot DBNull.Value Then
            grdViewItems.SetRowCellValue(FocRow, "InvProposalNo", cmbEmpNo.Properties.View.GetRowCellValue(rowhandle, "InvProposalNo"))
            grdViewItems.SetRowCellValue(FocRow, "CostCenter", cmbEmpNo.Properties.View.GetRowCellValue(rowhandle, "CostCenter"))
        End If
    End Sub

    Private Sub grdItems_OnLoadData(ByVal Editable As Boolean)
        'Cancel current row, if it has been added before loading the data.
        grdViewItems.CancelUpdateCurrentRow()

        grdItems.DataSource = objDocItems.GetGridDataWarrantyClaim(RecordGUID)
        grdItems_FormatGrid(Editable)
    End Sub

    Private Sub grdViewItems_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles grdViewItems.InitNewRow
        grdViewItems.SetRowCellValue(e.RowHandle, "GUID", Guid.NewGuid)
        'grdViewItems.SetRowCellValue(e.RowHandle, "POLineNo", 1)
    End Sub



    Private Sub grdViewItemsItems_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles grdViewItems.InvalidRowException
        'Suppress displaying the error message box
        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction
    End Sub

    Private Sub grdViewItemsItems_ValidateRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles grdViewItems.ValidateRow
        e.Valid = True

        Dim ItemGUID As String = grdViewItems.GetRowCellValue(e.RowHandle, "ItemSerialGUID").ToString
        If String.IsNullOrEmpty(ItemGUID) Then
            grdViewItems.SetColumnError(grdViewItems.Columns("ItemSerialGUID"), "Serial Number not entered,Please enter a value")
            'e.ErrorText = "Serial Number not entered,"
            e.Valid = False
        End If
    End Sub
End Class
