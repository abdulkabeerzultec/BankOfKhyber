Imports System.Windows.Forms

Public Class ctlWarrantyStatus
    Inherits ZulLib.ctlDataEditing
    Dim objDoc As New ZulAssetsBL.SAPDocumentsBLL

    Private WithEvents btnCloseForm As New DevExpress.XtraBars.BarButtonItem
    Private WithEvents btnRefreshForm As New DevExpress.XtraBars.BarButtonItem
    Private Sub ctlWarrantyStatus_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        HideDataList = True
        HideNavigationButtons = True
        btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        btnNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        btnSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        btnSaveAndNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        btnCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        barStatus.Visible = False
        ShowBarPrint = True
        btnRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        btnClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        Me.MainBar.Visible = False

        btnRefreshForm.Caption = "&Refresh"
        btnRefreshForm.Glyph = My.Resources.Resources.Refresh16x16
        btnRefreshForm.ItemShortcut = New DevExpress.XtraBars.BarShortcut((Keys.F5))
        btnRefreshForm.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        barPrint.AddItem(btnRefreshForm)

        btnCloseForm.Caption = "&Close"
        btnCloseForm.Glyph = My.Resources.Resources.Close16x16
        btnCloseForm.ItemShortcut = New DevExpress.XtraBars.BarShortcut((Keys.Control), (Keys.F4))
        btnCloseForm.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        barPrint.AddItem(btnCloseForm)

        Me.ParentForm.WindowState = Windows.Forms.FormWindowState.Maximized
        ctlWarrantyStatus_OnLoadData()
    End Sub

    Private Sub ctlWarrantyStatus_OnLoadData()
        grvItems.OptionsBehavior.Editable = True
        grvItems.OptionsView.ShowAutoFilterRow = True
        grvItems.OptionsView.ColumnAutoWidth = False
        grdItems.DataSource = objDoc.GetGridDataWarrantyStatus
        grvItems.Columns("GUID").Visible = False

        grdItems.UseEmbeddedNavigator = True
        grdItems.EmbeddedNavigator.Buttons.Append.Visible = False
        grdItems.EmbeddedNavigator.Buttons.Edit.Visible = False
        grdItems.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grdItems.EmbeddedNavigator.Buttons.Remove.Visible = False
        grdItems.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In grvItems.Columns
            col.OptionsColumn.ReadOnly = True
        Next
        'grvItems.Columns("CostCenter").Visible = False
        'grvItems.Columns("PORequisitionNo").Visible = False
        'grvItems.Columns("InvProposalNo").Visible = False
    End Sub

    Private Sub txtSerialNo_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSerialNo.EditValueChanged
        grvItems.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "SerialNo", CType(sender, DevExpress.XtraEditors.TextEdit).Text)
    End Sub

    Private Sub txtAssetNo_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAssetNo.EditValueChanged
        grvItems.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "AssetNo", CType(sender, DevExpress.XtraEditors.TextEdit).Text)
    End Sub

    Private Sub txtInvNo_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInvNo.EditValueChanged
        grvItems.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "InvProposalNo", CType(sender, DevExpress.XtraEditors.TextEdit).Text)
    End Sub

    Private Sub txtPOR_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPOR.EditValueChanged
        grvItems.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "PORequisitionNo", CType(sender, DevExpress.XtraEditors.TextEdit).Text)
    End Sub

    Private Sub txtSAPPartNo_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSAPPartNo.EditValueChanged
        grvItems.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "SAPPartNo", CType(sender, DevExpress.XtraEditors.TextEdit).Text)
    End Sub

    Private Sub txtCostCenter_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCostCenter.EditValueChanged
        grvItems.SetRowCellValue(DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "CostCenter", CType(sender, DevExpress.XtraEditors.TextEdit).Text)
    End Sub

    Private Sub ctlWarrantyStatus_OnPreviewClicked() Handles Me.OnPreviewClicked
        grdItems.ShowPrintPreview()
    End Sub

    Private Sub ctlWarrantyStatus_OnPrintClicked() Handles Me.OnPrintClicked
        If ZulLib.MainModule.ShowPrintConfirmation = DialogResult.Yes Then
            grdItems.Print()
        End If

    End Sub

    Private Sub btnRefreshForm_ItemPress(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRefreshForm.ItemPress
        ctlWarrantyStatus_OnLoadData()
    End Sub

    Private Sub btnCloseForm_ItemPress(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCloseForm.ItemPress
        Me.Close()
    End Sub
End Class
