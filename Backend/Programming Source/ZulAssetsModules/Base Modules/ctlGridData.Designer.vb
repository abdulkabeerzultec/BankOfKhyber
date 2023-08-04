<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlGridData
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl
        Me.grd = New DevExpress.XtraGrid.GridControl
        Me.grdView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.StandaloneBarDockControl1 = New DevExpress.XtraBars.StandaloneBarDockControl
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.BarManager2 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.GridToolBar = New DevExpress.XtraBars.Bar
        Me.btnNewItem = New DevExpress.XtraBars.BarButtonItem
        Me.btnEditItem = New DevExpress.XtraBars.BarButtonItem
        Me.btnDelete = New DevExpress.XtraBars.BarButtonItem
        Me.btnPreview = New DevExpress.XtraBars.BarButtonItem
        Me.btnPrint = New DevExpress.XtraBars.BarButtonItem
        Me.btnRefresh = New DevExpress.XtraBars.BarButtonItem
        Me.btnExport = New DevExpress.XtraBars.BarSubItem
        Me.btnExportxls = New DevExpress.XtraBars.BarButtonItem
        Me.btnExportHtml = New DevExpress.XtraBars.BarButtonItem
        Me.btnExportTxt = New DevExpress.XtraBars.BarButtonItem
        Me.btnExportPdf = New DevExpress.XtraBars.BarButtonItem
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
        Me.BarToolbarsListItem1 = New DevExpress.XtraBars.BarToolbarsListItem
        Me.BarSubItem2 = New DevExpress.XtraBars.BarSubItem
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = True
        Me.LayoutControl1.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LayoutControl1.Appearance.DisabledLayoutItem.Options.UseForeColor = True
        Me.LayoutControl1.Controls.Add(Me.grd)
        Me.LayoutControl1.Controls.Add(Me.StandaloneBarDockControl1)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(546, 304)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'grd
        '
        Me.grd.Location = New System.Drawing.Point(7, 44)
        Me.grd.MainView = Me.grdView
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(533, 254)
        Me.grd.TabIndex = 5
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdView})
        '
        'grdView
        '
        Me.grdView.GridControl = Me.grd
        Me.grdView.Name = "grdView"
        Me.grdView.OptionsBehavior.Editable = False
        Me.grdView.OptionsView.ShowAutoFilterRow = True
        Me.grdView.OptionsView.ShowIndicator = False
        '
        'StandaloneBarDockControl1
        '
        Me.StandaloneBarDockControl1.Location = New System.Drawing.Point(7, 7)
        Me.StandaloneBarDockControl1.Name = "StandaloneBarDockControl1"
        Me.StandaloneBarDockControl1.Size = New System.Drawing.Size(523, 26)
        Me.StandaloneBarDockControl1.Text = "StandaloneBarDockControl1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(546, 304)
        Me.LayoutControlGroup1.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.StandaloneBarDockControl1
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(544, 37)
        Me.LayoutControlItem1.Text = "LayoutControlItem1"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.grd
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 37)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(544, 265)
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'BarManager2
        '
        Me.BarManager2.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.GridToolBar})
        Me.BarManager2.DockControls.Add(Me.barDockControlTop)
        Me.BarManager2.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager2.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager2.DockControls.Add(Me.barDockControlRight)
        Me.BarManager2.DockControls.Add(Me.StandaloneBarDockControl1)
        Me.BarManager2.Form = Me
        Me.BarManager2.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnNewItem, Me.btnEditItem, Me.btnDelete, Me.btnPreview, Me.btnPrint, Me.btnRefresh, Me.BarToolbarsListItem1, Me.btnExport, Me.BarSubItem2, Me.btnExportxls, Me.btnExportHtml, Me.btnExportTxt, Me.btnExportPdf, Me.btnClose})
        Me.BarManager2.MaxItemId = 14
        '
        'GridToolBar
        '
        Me.GridToolBar.BarName = "Grid ToolBar"
        Me.GridToolBar.DockCol = 0
        Me.GridToolBar.DockRow = 0
        Me.GridToolBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone
        Me.GridToolBar.FloatLocation = New System.Drawing.Point(271, 107)
        Me.GridToolBar.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnNewItem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnEditItem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnPreview, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(CType((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle Or DevExpress.XtraBars.BarLinkUserDefines.KeyTip), DevExpress.XtraBars.BarLinkUserDefines), Me.btnPrint, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph, "", ""), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.GridToolBar.OptionsBar.DisableClose = True
        Me.GridToolBar.StandaloneBarDockControl = Me.StandaloneBarDockControl1
        Me.GridToolBar.Text = "Grid ToolBar"
        '
        'btnNewItem
        '
        Me.btnNewItem.Caption = "&New"
        Me.btnNewItem.Glyph = Global.ZulAssetsModules.My.Resources.ButtonNew
        Me.btnNewItem.Hint = "New Item"
        Me.btnNewItem.Id = 0
        Me.btnNewItem.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N))
        Me.btnNewItem.Name = "btnNewItem"
        '
        'btnEditItem
        '
        Me.btnEditItem.Caption = "&Edit"
        Me.btnEditItem.Glyph = Global.ZulAssetsModules.My.Resources.Edit
        Me.btnEditItem.Hint = "Edit the selected item"
        Me.btnEditItem.Id = 1
        Me.btnEditItem.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.btnEditItem.Name = "btnEditItem"
        '
        'btnDelete
        '
        Me.btnDelete.Caption = "&Delete"
        Me.btnDelete.Glyph = Global.ZulAssetsModules.My.Resources.delete
        Me.btnDelete.Hint = "delete the selected item"
        Me.btnDelete.Id = 2
        Me.btnDelete.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
        Me.btnDelete.Name = "btnDelete"
        '
        'btnPreview
        '
        Me.btnPreview.Caption = "&Preview"
        Me.btnPreview.Glyph = Global.ZulAssetsModules.My.Resources.Preview16x16
        Me.btnPreview.Hint = "Preview before printing"
        Me.btnPreview.Id = 3
        Me.btnPreview.Name = "btnPreview"
        '
        'btnPrint
        '
        Me.btnPrint.Caption = "&Print"
        Me.btnPrint.Glyph = Global.ZulAssetsModules.My.Resources.Print_16x16
        Me.btnPrint.Hint = "Print"
        Me.btnPrint.Id = 4
        Me.btnPrint.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
        Me.btnPrint.Name = "btnPrint"
        '
        'btnRefresh
        '
        Me.btnRefresh.Caption = "&Refresh"
        Me.btnRefresh.Glyph = Global.ZulAssetsModules.My.Resources.Refresh
        Me.btnRefresh.Hint = "Refersh current view"
        Me.btnRefresh.Id = 5
        Me.btnRefresh.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
        Me.btnRefresh.Name = "btnRefresh"
        '
        'btnExport
        '
        Me.btnExport.Caption = "&Export"
        Me.btnExport.Glyph = Global.ZulAssetsModules.My.Resources.Export
        Me.btnExport.Id = 7
        Me.btnExport.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnExportxls), New DevExpress.XtraBars.LinkPersistInfo(Me.btnExportHtml), New DevExpress.XtraBars.LinkPersistInfo(Me.btnExportTxt), New DevExpress.XtraBars.LinkPersistInfo(Me.btnExportPdf)})
        Me.btnExport.Name = "btnExport"
        '
        'btnExportxls
        '
        Me.btnExportxls.Caption = "Export to Excel"
        Me.btnExportxls.Glyph = Global.ZulAssetsModules.My.Resources.ExportToExcel
        Me.btnExportxls.Id = 9
        Me.btnExportxls.Name = "btnExportxls"
        '
        'btnExportHtml
        '
        Me.btnExportHtml.Caption = "Export to Html"
        Me.btnExportHtml.Glyph = Global.ZulAssetsModules.My.Resources.ExportToHTML
        Me.btnExportHtml.Id = 10
        Me.btnExportHtml.Name = "btnExportHtml"
        '
        'btnExportTxt
        '
        Me.btnExportTxt.Caption = "Export to Text"
        Me.btnExportTxt.Glyph = Global.ZulAssetsModules.My.Resources.ExportToText
        Me.btnExportTxt.Id = 11
        Me.btnExportTxt.Name = "btnExportTxt"
        '
        'btnExportPdf
        '
        Me.btnExportPdf.Caption = "Export to Pdf"
        Me.btnExportPdf.Glyph = Global.ZulAssetsModules.My.Resources.ExportToPDF
        Me.btnExportPdf.Id = 12
        Me.btnExportPdf.Name = "btnExportPdf"
        '
        'btnClose
        '
        Me.btnClose.Caption = "&Close"
        Me.btnClose.Glyph = Global.ZulAssetsModules.My.Resources.Close
        Me.btnClose.Hint = "Close"
        Me.btnClose.Id = 13
        Me.btnClose.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F4))
        Me.btnClose.Name = "btnClose"
        '
        'BarToolbarsListItem1
        '
        Me.BarToolbarsListItem1.Caption = "&Export"
        Me.BarToolbarsListItem1.Id = 6
        Me.BarToolbarsListItem1.Name = "BarToolbarsListItem1"
        '
        'BarSubItem2
        '
        Me.BarSubItem2.Caption = "BarSubItem2"
        Me.BarSubItem2.Id = 8
        Me.BarSubItem2.Name = "BarSubItem2"
        '
        'ctlGridData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "ctlGridData"
        Me.Size = New System.Drawing.Size(546, 304)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents StandaloneBarDockControl1 As DevExpress.XtraBars.StandaloneBarDockControl
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BarManager2 As DevExpress.XtraBars.BarManager
    Friend WithEvents GridToolBar As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents btnNewItem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnEditItem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnDelete As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnPreview As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnPrint As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnRefresh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnExport As DevExpress.XtraBars.BarSubItem
    Friend WithEvents btnExportxls As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarToolbarsListItem1 As DevExpress.XtraBars.BarToolbarsListItem
    Friend WithEvents BarSubItem2 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents btnExportHtml As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnExportTxt As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnExportPdf As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnClose As DevExpress.XtraBars.BarButtonItem

End Class
