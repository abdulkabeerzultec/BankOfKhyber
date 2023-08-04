Imports System.Windows.Forms

Public Class ctlGridData
    Inherits System.Windows.Forms.UserControl
    Public Delegate Sub MyLoadSub()
    Public Event OnLoadData As MyLoadSub
    Public Event OnNewClicked As MyLoadSub
    Public Event OnRefershClicked As MyLoadSub
    Public Event OnEditClicked As MyLoadSub
    Public Event OnDeleteClicked As MyLoadSub

    Private _isControl As Boolean
    Private _ParentID As Integer
    Public Sub New()
        InitializeComponent()
        _isControl = True
    End Sub

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
    Public Property ParentID() As Integer
        Get
            Return _ParentID
        End Get
        Set(ByVal value As Integer)
            _ParentID = value
            'load data if ParentID changed
            If value > 0 Then
                LoadData()
            End If
        End Set
    End Property

    'Property DispalyCloseButton() As Boolean
    '    Get
    '        If btnClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never Then
    '            Return False
    '        Else : Return True
    '        End If
    '    End Get
    '    Set(ByVal value As Boolean)
    '        If value = True Then
    '            btnClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
    '        Else
    '            btnClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
    '        End If
    '    End Set
    'End Property
    Private Sub cntGridData_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'load the data on load only if the grid on tab page.
        If Not isControl Then
            LoadData()
        End If
        LayoutControl1.AllowCustomizationMenu = False
    End Sub

    Private Function LoadData() As Integer
        RaiseEvent OnLoadData()
        For i As Integer = 0 To grdView.Columns.Count - 1
            grdView.Columns(i).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Next
        RefershButtons()
    End Function
    Private Sub btnDelete_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDelete.ItemClick
        Dim FocousRow As Integer = grdView.FocusedRowHandle
        If FocousRow >= 0 Then
            If ShowDeleteConfirmation() = DialogResult.Yes Then
                RaiseEvent OnDeleteClicked()
                btnRefersh_ItemClick(sender, e)
                RefershButtons()
            End If
        End If
    End Sub

    Public Sub btnRefersh_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRefresh.ItemClick
        RaiseEvent OnRefershClicked()
        RefershButtons()
    End Sub

    Private Sub btnPreview_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPreview.ItemClick
        grd.ShowPrintPreview()
    End Sub

    Private Sub btnPrint_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPrint.ItemClick
        grd.Print()
    End Sub

    Private Sub btnNewItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnNewItem.ItemClick
        RaiseEvent OnNewClicked()
    End Sub

    Private Sub btnEditItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnEditItem.ItemClick
        Dim FocousRow As Integer = grdView.FocusedRowHandle
        If FocousRow >= 0 Then
            RaiseEvent OnEditClicked()
        End If
    End Sub

    Public Sub btnClose_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnClose.ItemClick
        Dim index = MainForm.tabControlMain.SelectedTabPageIndex
        If index = 0 Then
            MainForm.tabControlMain.SelectedTabPageIndex = 0
        Else
            MainForm.tabControlMain.SelectedTabPageIndex = index - 1
        End If
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

    Private Sub RefershButtons()
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
    End Sub

    Private Sub grdView_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdView.DoubleClick
        btnEditItem_ItemClick(Nothing, Nothing)
    End Sub
End Class
