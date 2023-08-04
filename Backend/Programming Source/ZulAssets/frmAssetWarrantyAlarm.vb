
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Controls
Imports ZulAssetsDAL.ZulAssetsDAL
Imports ZulAssetsBL.ZulAssetBAL

Public Class frmAssetWarrantyAlarm
    Dim objattWarranty As New attAssetWarranty
    Dim objBALWarranty As New BALAssetWarranty

    Private Sub frmAssetWarrantyAlarm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmAstWarrantyAlarm = Nothing
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FormatGrid()
        grv.OptionsBehavior.Editable = True
        Dim imageCombo As New RepositoryItemImageComboBox
        Dim OpenLink As New RepositoryItemHyperLinkEdit
        AddHandler OpenLink.Click, AddressOf OpenLink_Click
        'GetGridColumn(grv, "Reminder Type").OptionsColumn.AllowEdit = False
        'GetGridColumn(grv, "Reminder").OptionsColumn.AllowEdit = False
        Dim images As DevExpress.Utils.ImageCollection = New DevExpress.Utils.ImageCollection()
        images.ImageSize = New Size(16, 16)

        images.AddImage(My.Resources.Icons.Invalid)
        images.AddImage(My.Resources.Icons.Attention)
        images.AddImage(My.Resources.Icons.Info)

        imageCombo.SmallImages = images
        imageCombo.Items.Add(New ImageComboBoxItem("Error", 1, 0))
        imageCombo.Items.Add(New ImageComboBoxItem("Warning", 2, 1))
        imageCombo.Items.Add(New ImageComboBoxItem("Info", 3, 2))
        imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
        Dim IconColumn As New DevExpress.XtraGrid.Columns.GridColumn
        IconColumn = GetGridColumn(grv, "IconIndex")
        IconColumn.ColumnEdit = imageCombo
        IconColumn.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowOnlyInEditor
        IconColumn.Caption = "Type"
        IconColumn.OptionsColumn.ShowCaption = False
        IconColumn.OptionsColumn.AllowSize = False
        IconColumn.OptionsColumn.FixedWidth = True
        IconColumn.OptionsColumn.AllowEdit = False
        IconColumn.Width = 44
        IconColumn.ToolTip = "Icon"
        Dim HeaderImages As DevExpress.Utils.ImageCollection = New DevExpress.Utils.ImageCollection()
        grv.Images = imageList1
        IconColumn.ImageIndex = 1

        Dim LinkColumn As New DevExpress.XtraGrid.Columns.GridColumn
        LinkColumn = GetGridColumn(grv, "Dismiss")
        LinkColumn.ColumnEdit = OpenLink
        LinkColumn.Width = 100
        LinkColumn.OptionsColumn.FixedWidth = True

        GetGridColumn(grv, "AssetNumber").Width = 120
        GetGridColumn(grv, "AssetNumber").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        GetGridColumn(grv, "AssetID").Width = 120
        GetGridColumn(grv, "Description").Width = 200
        GetGridColumn(grv, "WarrantyStart").Width = 130
        GetGridColumn(grv, "ExpiryDate").Width = 120

        GetGridColumn(grv, "AssetNumber").OptionsColumn.AllowEdit = False
        GetGridColumn(grv, "AssetID").OptionsColumn.AllowEdit = False
        GetGridColumn(grv, "Description").OptionsColumn.AllowEdit = False
        GetGridColumn(grv, "WarrantyStart").OptionsColumn.AllowEdit = False
        GetGridColumn(grv, "ExpiryDate").OptionsColumn.AllowEdit = False
        GetGridColumn(grv, "RefNo").OptionsColumn.AllowEdit = False


        'GetGridColumn(grv, "Reminder Type").OptionsColumn.FixedWidth = True
        GetGridColumn(grv, "ID").Visible = False
        GetGridColumn(grv, "WarrantyPeriodMonth").Visible = False
        'GetGridColumn(grv, "AlarmBeforeDays").Visible = False
        GetGridColumn(grv, "WarrantydueDays").Visible = False
        'grv.ExpandAllGroups()
        addGridMenu(grd)
    End Sub

    Private Sub OpenLink_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FocRow As Integer = grv.FocusedRowHandle
        If FocRow >= 0 Then
            If ZulMessageBox.ShowMe("WarrantyDismiss", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Dim ID As Integer = grv.GetRowCellValue(FocRow, "ID")
                objBALWarranty.Dismiss_AssetWarranty(ID)
                RefershReminders()
            End If
        End If
    End Sub

    Private Sub RefershReminders()
        grd.DataSource = objBALWarranty.GetAll_AlarmAssetWarranty(AppConfig.AlarmBeforeDays)
        FormatGrid()
    End Sub

    Private Sub frmAssetWarrantyAlarm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        RefershReminders()
    End Sub

    Private Sub btnDismissALL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDismissALL.Click
        If ZulMessageBox.ShowMe("WarrantyDismissAll", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            'objBALWarranty.Dismiss_AllAssetWarranty()
            For FocRow As Integer = 0 To grv.RowCount - 1
                Dim ID As Integer = grv.GetRowCellValue(FocRow, "ID")
                objBALWarranty.Dismiss_AssetWarranty(ID)
            Next
            RefershReminders()
            grv.ClearColumnsFilter()
        End If
    End Sub

    Private Sub btnOpenAsset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenAsset.Click
        Dim FocRow As Integer = grv.FocusedRowHandle
        If FocRow >= 0 Then
            If Not (GetGridRowCellValue(grv, FocRow, "AssetID") Is Nothing) Then
                ShowAssetDetailsForm(GetGridRowCellValue(grv, FocRow, "AssetID"))
            End If
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        RefershReminders()
    End Sub
End Class