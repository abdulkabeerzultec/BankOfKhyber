Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class ctlGridDataEditing
    Inherits System.Windows.Forms.UserControl
    Public Delegate Sub MyLoadSub()
    Public Delegate Sub MySubEvent(ByVal rowGuid As Guid)

    Public Event OnLoadData As MyLoadSub
    Public Event OnbtnDeleteClicked As MySubEvent
    Public Event OnbtnEditClicked As MySubEvent
    Public Event OnBeforeNewData As MyLoadSub
    Public Event OnAfterNewData As MyLoadSub

    Private WithEvents PopupForm As New frmPopup
    Private _DataEditingControl As ctlDataEditing
    Private _KeyFieldName As String
    Private _isControl As Boolean
    Public Sub New()
        InitializeComponent()
        _isControl = True
    End Sub

    Public Sub ShowToolBar(ByVal value As Boolean)
        If value Then
            LayoutToolbar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            LayoutToolbar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Public Sub ShowEditingButtons(ByVal Value As Boolean)
        If Value Then
            btnEditItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            btnNewItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            btnEditItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btnNewItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub

    Public Sub ShowPrintingButtons(ByVal value As Boolean)
        If value Then
            btnPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            btnPreview.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            btnExport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            btnPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btnPreview.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btnExport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub

    Public Property KeyFieldName() As String
        Get
            Return _KeyFieldName
        End Get
        Set(ByVal value As String)
            _KeyFieldName = value
        End Set
    End Property

    Public Property DataSource() As DataTable
        Get
            Return grd.DataSource
        End Get
        Set(ByVal value As DataTable)
            grd.DataSource = value
            RefershInterface()
        End Set
    End Property

    Public Property DataEditingControl() As ctlDataEditing
        Get
            Return _DataEditingControl
        End Get
        Set(ByVal value As ctlDataEditing)
            If value IsNot Nothing Then
                _DataEditingControl = value
                PopupForm.Controls.Add(DataEditingControl)
                PopupForm.Size = New System.Drawing.Size(DataEditingControl.Width, DataEditingControl.Height)
                DataEditingControl.Dock = System.Windows.Forms.DockStyle.Fill
                DataEditingControl.ClosingWay = ctlDataEditing.TClosingWays.ParentHiding
                'ChangeCloseEventHandler()
            End If
            'Get the left/top point of the treelist and assign it to the form.
            Dim p As New System.Drawing.Point(grd.Left, grd.Top)
            p = Me.PointToScreen(p)
            PopupForm.Top = p.Y
            PopupForm.Left = p.X
        End Set
    End Property

    Private Sub btnEditItem_ItemPress(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnEditItem.ItemPress
        If grdView.FocusedRowHandle >= 0 Then
            ShowPopup(btnEditItem)
        End If
    End Sub

    Private Sub btnNewItem_ItemPress(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnNewItem.ItemPress
        ShowPopup(btnNewItem)
    End Sub

    Private Sub ShowPopup(ByVal sender As Object)
        If DataEditingControl IsNot Nothing Then
            'If Popup form not visible then show it, otherwise it will give error if the user open two forms at the same time.
            If Not PopupForm.Visible Then
                PopupForm.BringToFront()
                PopupForm.TopLevel = True
                PopupForm.Show(Me.ParentForm)
            End If

            If sender Is btnNewItem Then
                RaiseEvent OnBeforeNewData()
                DataEditingControl.NewData()
                'Set Current action = new to stop Recordchange coming from OnbtnNewClicked event
                DataEditingControl.CurrentAction = ctlDataEditing.TControlStatus.isNew
                RaiseEvent OnAfterNewData()
                DataEditingControl.CurrentAction = ctlDataEditing.TControlStatus.isNothing
            ElseIf sender Is btnEditItem Then
                Dim SelectedValue As Guid = grdView.GetRowCellValue(grdView.FocusedRowHandle, "GUID")
                DataEditingControl.RecordGUID = SelectedValue
                DataEditingControl.LoadData()
                RaiseEvent OnbtnEditClicked(SelectedValue)
            End If
        End If
    End Sub

    Private Sub PopupForm_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PopupForm.VisibleChanged
        If Not PopupForm.Visible Then
            btnRefresh.PerformClick()
        End If
    End Sub

    'Private Sub btnClosePopup_Clicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'To be able to close the main form we have to assign the owner and parent to Null.
    '    PopupForm.Owner = Nothing
    '    PopupForm.Parent = Nothing
    '    PopupForm.TopLevel = False
    '    PopupForm.Hide()
    'End Sub

    'Private Sub ChangeCloseEventHandler()
    '    RemoveHandler Me.DataEditingControl.btnClose.ItemClick, AddressOf Me.DataEditingControl.btnClose_ItemClick
    '    AddHandler Me.DataEditingControl.btnClose.ItemClick, AddressOf btnClosePopup_Clicked
    'End Sub

    Public Property isControl() As Boolean
        Get
            Return _isControl
        End Get
        Set(ByVal value As Boolean)
            _isControl = value
            If _isControl Then
                btnClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Else
                btnClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        End Set
    End Property

    Private Sub ctlGridDataEditing_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If DataEditingControl IsNot Nothing Then
            If DataEditingControl.ParentForm IsNot Nothing Then
                DataEditingControl.ParentForm.Dispose()
            Else
                DataEditingControl.Dispose()
            End If
        End If
    End Sub

    Private Sub ctlGridData_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'load the data on load only if the grid on tab page.
        If Not isControl Then
            LoadData()
        End If
        LayoutControl1.AllowCustomizationMenu = False
    End Sub

    Private Function LoadData() As Integer
        RaiseEvent OnLoadData()
        RefershInterface()
    End Function

    Private Sub btnDelete_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDelete.ItemClick
        Dim FocousRow As Integer = grdView.FocusedRowHandle
        If FocousRow >= 0 Then
            If ShowDeleteConfirmation() = DialogResult.Yes Then
                RaiseEvent OnbtnDeleteClicked(grdView.GetRowCellValue(FocousRow, KeyFieldName))
                btnRefersh_ItemClick(sender, e)
                RefershInterface()
            End If
        End If
    End Sub

    Public Sub btnRefersh_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRefresh.ItemClick
        LoadData()
    End Sub

    Private Sub btnPreview_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPreview.ItemClick
        grd.ShowPrintPreview()
    End Sub

    Private Sub btnPrint_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPrint.ItemClick
        If ShowPrintConfirmation() = DialogResult.Yes Then
            grd.Print()
        End If
    End Sub

    Public Sub btnClose_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnClose.ItemClick
        Parent.Dispose()
    End Sub

    Private Sub btnExportxls_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnExportxls.ItemClick
        Using savedlg As New SaveFileDialog() With {.DefaultExt = "xls", .Filter = "*.xls|*.xls"}
            If savedlg.ShowDialog() = DialogResult.OK Then
                grd.ExportToXls(savedlg.FileName)
                System.Diagnostics.Process.Start(savedlg.FileName)
            End If
        End Using
    End Sub

    Private Sub btnExportHtml_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnExportHtml.ItemClick
        Using savedlg As New SaveFileDialog() With {.DefaultExt = "Html", .Filter = "*.Html|*.Html"}
            If savedlg.ShowDialog() = DialogResult.OK Then
                grd.ExportToHtml(savedlg.FileName)
                System.Diagnostics.Process.Start(savedlg.FileName)
            End If
        End Using
    End Sub

    Private Sub btnExportTxt_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnExportTxt.ItemClick
        Using savedlg As New SaveFileDialog() With {.DefaultExt = "txt", .Filter = "*.txt|*.txt"}
            If savedlg.ShowDialog() = DialogResult.OK Then
                grd.ExportToText(savedlg.FileName)
                System.Diagnostics.Process.Start(savedlg.FileName)
            End If
        End Using
    End Sub

    Private Sub btnExportPdf_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnExportPdf.ItemClick
        Using savedlg As New SaveFileDialog() With {.DefaultExt = "pdf", .Filter = "*.pdf|*.pdf"}
            If savedlg.ShowDialog() = DialogResult.OK Then
                grd.ExportToPdf(savedlg.FileName)
                System.Diagnostics.Process.Start(savedlg.FileName)
            End If
        End Using
    End Sub

    Private Sub RefershInterface()
        If grdView.RowCount < 1 Then
            btnDelete.Enabled = False
            btnEditItem.Enabled = False
            btnPreview.Enabled = False
            btnPrint.Enabled = False
            btnExport.Enabled = False
        Else
            btnDelete.Enabled = True
            btnEditItem.Enabled = True
            btnPreview.Enabled = True
            btnPrint.Enabled = True
            btnExport.Enabled = True
        End If
        If grdView.Columns(KeyFieldName) IsNot Nothing Then
            grdView.Columns(KeyFieldName).Visible = False
        End If

        If LibConfig.IsRTLLanguage Then
            For i As Integer = 0 To grdView.Columns.Count - 1
                grdView.Columns(i).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                'reverse columns in case of Arabic Language.
                If grdView.Columns(i).VisibleIndex >= 0 Then
                    grdView.Columns(i).VisibleIndex = grdView.Columns.Count - grdView.Columns(i).AbsoluteIndex - 1
                End If
                grdView.Columns(i).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            Next
        Else
            For i As Integer = 0 To grdView.Columns.Count - 1
                grdView.Columns(i).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                grdView.Columns(i).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            Next
        End If
    End Sub

    Private Sub grdView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdView.DoubleClick
        If btnEditItem.Visibility <> DevExpress.XtraBars.BarItemVisibility.Never And LayoutToolbar.Visibility <> DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
            Dim GHI As New GridHitInfo()
            GHI = grdView.CalcHitInfo(CType(e, DevExpress.Utils.DXMouseEventArgs).Location)
            If GHI.InRow Then
                If grdView.FocusedRowHandle >= 0 Then
                    btnEditItem_ItemPress(sender, Nothing)
                End If
            End If
        End If
    End Sub
End Class
