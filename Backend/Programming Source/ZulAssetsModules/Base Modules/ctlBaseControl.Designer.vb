<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlBaseControl
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
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BaseBar = New DevExpress.XtraBars.Bar
        Me.btnNew = New DevExpress.XtraBars.BarButtonItem
        Me.btnSave = New DevExpress.XtraBars.BarButtonItem
        Me.btnSaveAndNew = New DevExpress.XtraBars.BarButtonItem
        Me.btnDelete = New DevExpress.XtraBars.BarButtonItem
        Me.btnRefresh = New DevExpress.XtraBars.BarButtonItem
        Me.btnCancel = New DevExpress.XtraBars.BarButtonItem
        Me.btnFirst = New DevExpress.XtraBars.BarButtonItem
        Me.btnPrev = New DevExpress.XtraBars.BarButtonItem
        Me.btnNext = New DevExpress.XtraBars.BarButtonItem
        Me.btnLast = New DevExpress.XtraBars.BarButtonItem
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.BaseBar})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnClose, Me.btnFirst, Me.btnPrev, Me.btnNext, Me.btnLast, Me.btnDelete, Me.btnNew, Me.btnSave, Me.BarButtonItem1, Me.btnSaveAndNew, Me.btnCancel, Me.btnRefresh})
        Me.BarManager1.MaxItemId = 21
        '
        'BaseBar
        '
        Me.BaseBar.BarName = "Base ToolBar"
        Me.BaseBar.DockCol = 0
        Me.BaseBar.DockRow = 0
        Me.BaseBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.BaseBar.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnNew, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(CType((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle Or DevExpress.XtraBars.BarLinkUserDefines.KeyTip), DevExpress.XtraBars.BarLinkUserDefines), Me.btnSave, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph, "", ""), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnSaveAndNew, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnCancel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(Me.btnFirst, True), New DevExpress.XtraBars.LinkPersistInfo(Me.btnPrev), New DevExpress.XtraBars.LinkPersistInfo(Me.btnNext), New DevExpress.XtraBars.LinkPersistInfo(Me.btnLast), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnClose, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.BaseBar.OptionsBar.DisableClose = True
        Me.BaseBar.Text = "Base ToolBar"
        '
        'btnNew
        '
        Me.btnNew.Caption = "&New"
        Me.btnNew.Glyph = Global.ZulAssetsModules.My.Resources.ButtonNew
        Me.btnNew.Hint = "New Item"
        Me.btnNew.Id = 11
        Me.btnNew.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N))
        Me.btnNew.Name = "btnNew"
        '
        'btnSave
        '
        Me.btnSave.Caption = "&Save"
        Me.btnSave.Glyph = Global.ZulAssetsModules.My.Resources.save
        Me.btnSave.Hint = "Save item to database"
        Me.btnSave.Id = 12
        Me.btnSave.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
        Me.btnSave.Name = "btnSave"
        '
        'btnSaveAndNew
        '
        Me.btnSaveAndNew.Caption = "Save&&New"
        Me.btnSaveAndNew.Glyph = Global.ZulAssetsModules.My.Resources.SaveNew
        Me.btnSaveAndNew.Hint = "Save and New"
        Me.btnSaveAndNew.Id = 14
        Me.btnSaveAndNew.ItemShortcut = New DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                        Or System.Windows.Forms.Keys.S))
        Me.btnSaveAndNew.Name = "btnSaveAndNew"
        '
        'btnDelete
        '
        Me.btnDelete.Caption = "&Delete"
        Me.btnDelete.Glyph = Global.ZulAssetsModules.My.Resources.delete
        Me.btnDelete.Hint = "Delete Item"
        Me.btnDelete.Id = 10
        Me.btnDelete.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
        Me.btnDelete.Name = "btnDelete"
        '
        'btnRefresh
        '
        Me.btnRefresh.Caption = "&Refresh"
        Me.btnRefresh.Glyph = Global.ZulAssetsModules.My.Resources.Refresh
        Me.btnRefresh.Hint = "Refersh current view"
        Me.btnRefresh.Id = 18
        Me.btnRefresh.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
        Me.btnRefresh.Name = "btnRefresh"
        '
        'btnCancel
        '
        Me.btnCancel.Caption = "&Cancel"
        Me.btnCancel.Glyph = Global.ZulAssetsModules.My.Resources.Cancel
        Me.btnCancel.Hint = "Cancel the modification"
        Me.btnCancel.Id = 15
        Me.btnCancel.Name = "btnCancel"
        '
        'btnFirst
        '
        Me.btnFirst.Caption = "&First"
        Me.btnFirst.Glyph = Global.ZulAssetsModules.My.Resources.BtnFirst
        Me.btnFirst.Hint = "Go to first record"
        Me.btnFirst.Id = 5
        Me.btnFirst.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Home))
        Me.btnFirst.Name = "btnFirst"
        '
        'btnPrev
        '
        Me.btnPrev.Caption = "&Previous"
        Me.btnPrev.Glyph = Global.ZulAssetsModules.My.Resources.BtnPrev
        Me.btnPrev.Hint = "Go to previous record"
        Me.btnPrev.Id = 6
        Me.btnPrev.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Left))
        Me.btnPrev.Name = "btnPrev"
        '
        'btnNext
        '
        Me.btnNext.Caption = "&Next"
        Me.btnNext.Glyph = Global.ZulAssetsModules.My.Resources.BtnNext
        Me.btnNext.Hint = "Go to next record"
        Me.btnNext.Id = 7
        Me.btnNext.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Right))
        Me.btnNext.Name = "btnNext"
        '
        'btnLast
        '
        Me.btnLast.Caption = "&Last"
        Me.btnLast.Glyph = Global.ZulAssetsModules.My.Resources.BtnLast
        Me.btnLast.Hint = "Go to last record"
        Me.btnLast.Id = 8
        Me.btnLast.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.[End]))
        Me.btnLast.Name = "btnLast"
        '
        'btnClose
        '
        Me.btnClose.Caption = "&Close"
        Me.btnClose.Glyph = Global.ZulAssetsModules.My.Resources.Close
        Me.btnClose.Hint = "Close"
        Me.btnClose.Id = 0
        Me.btnClose.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F4))
        Me.btnClose.Name = "btnClose"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Save&&Close"
        Me.BarButtonItem1.Glyph = Global.ZulAssetsModules.My.Resources.ButtonSaveAndClose
        Me.BarButtonItem1.Id = 13
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'ctlBaseControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "ctlBaseControl"
        Me.Size = New System.Drawing.Size(598, 425)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents BaseBar As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents btnClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnFirst As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnPrev As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnNext As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnLast As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnNew As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSave As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnDelete As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSaveAndNew As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnCancel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnRefresh As DevExpress.XtraBars.BarButtonItem

End Class
