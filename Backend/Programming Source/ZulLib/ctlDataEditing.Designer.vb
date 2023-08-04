<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlDataEditing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctlDataEditing))
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.MainBar = New DevExpress.XtraBars.Bar
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
        Me.cmbList = New DevExpress.XtraBars.BarEditItem
        Me.cmbListRep = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem
        Me.barStatus = New DevExpress.XtraBars.Bar
        Me.lblCreatedLabel = New DevExpress.XtraBars.BarStaticItem
        Me.lblCreated = New DevExpress.XtraBars.BarStaticItem
        Me.lblModifiedLabel = New DevExpress.XtraBars.BarStaticItem
        Me.lblModified = New DevExpress.XtraBars.BarStaticItem
        Me.barPrint = New DevExpress.XtraBars.Bar
        Me.btnPrint = New DevExpress.XtraBars.BarButtonItem
        Me.btnPreview = New DevExpress.XtraBars.BarButtonItem
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
        Me.barItemCreate = New DevExpress.XtraBars.BarStaticItem
        Me.barItemModify = New DevExpress.XtraBars.BarStaticItem
        Me.BarStaticCreation = New DevExpress.XtraBars.BarStaticItem
        Me.lblRecordStatus = New DevExpress.XtraBars.BarStaticItem
        Me.errProv = New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(Me.components)
        Me.valProvMain = New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(Me.components)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbListRep, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.errProv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.valProvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarManager1
        '
        Me.BarManager1.AllowCustomization = False
        Me.BarManager1.AllowShowToolbarsPopup = False
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.MainBar, Me.barStatus, Me.barPrint})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnClose, Me.btnFirst, Me.btnPrev, Me.btnNext, Me.btnLast, Me.btnDelete, Me.btnNew, Me.btnSave, Me.btnSaveAndNew, Me.btnCancel, Me.btnRefresh, Me.barItemCreate, Me.barItemModify, Me.BarStaticCreation, Me.lblCreatedLabel, Me.lblCreated, Me.lblModifiedLabel, Me.lblModified, Me.btnPrint, Me.btnPreview, Me.lblRecordStatus, Me.cmbList})
        Me.BarManager1.MaxItemId = 44
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.cmbListRep})
        Me.BarManager1.StatusBar = Me.barStatus
        '
        'MainBar
        '
        Me.MainBar.BarName = "Main ToolBar"
        Me.MainBar.DockCol = 0
        Me.MainBar.DockRow = 0
        Me.MainBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.MainBar.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnNew, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(CType((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle Or DevExpress.XtraBars.BarLinkUserDefines.KeyTip), DevExpress.XtraBars.BarLinkUserDefines), Me.btnSave, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph, "", ""), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnSaveAndNew, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnCancel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(Me.btnFirst, True), New DevExpress.XtraBars.LinkPersistInfo(Me.btnPrev), New DevExpress.XtraBars.LinkPersistInfo(Me.btnNext), New DevExpress.XtraBars.LinkPersistInfo(Me.btnLast), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, Me.cmbList, "", False, True, True, 125), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        resources.ApplyResources(Me.MainBar, "MainBar")
        '
        'btnNew
        '
        resources.ApplyResources(Me.btnNew, "btnNew")
        Me.btnNew.Glyph = Global.ZulLib.My.Resources.Resources.ButtonNew
        Me.btnNew.Id = 11
        Me.btnNew.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N))
        Me.btnNew.Name = "btnNew"
        '
        'btnSave
        '
        resources.ApplyResources(Me.btnSave, "btnSave")
        Me.btnSave.Glyph = Global.ZulLib.My.Resources.Resources.save
        Me.btnSave.Id = 12
        Me.btnSave.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
        Me.btnSave.Name = "btnSave"
        '
        'btnSaveAndNew
        '
        resources.ApplyResources(Me.btnSaveAndNew, "btnSaveAndNew")
        Me.btnSaveAndNew.Glyph = Global.ZulLib.My.Resources.Resources.SaveNew
        Me.btnSaveAndNew.Id = 14
        Me.btnSaveAndNew.ItemShortcut = New DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                        Or System.Windows.Forms.Keys.S))
        Me.btnSaveAndNew.Name = "btnSaveAndNew"
        '
        'btnDelete
        '
        resources.ApplyResources(Me.btnDelete, "btnDelete")
        Me.btnDelete.Glyph = Global.ZulLib.My.Resources.Resources.delete
        Me.btnDelete.Id = 10
        Me.btnDelete.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
        Me.btnDelete.Name = "btnDelete"
        '
        'btnRefresh
        '
        resources.ApplyResources(Me.btnRefresh, "btnRefresh")
        Me.btnRefresh.Glyph = Global.ZulLib.My.Resources.Resources.Refresh
        Me.btnRefresh.Id = 18
        Me.btnRefresh.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
        Me.btnRefresh.Name = "btnRefresh"
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.Glyph = Global.ZulLib.My.Resources.Resources.Cancel
        Me.btnCancel.Id = 15
        Me.btnCancel.Name = "btnCancel"
        '
        'btnFirst
        '
        resources.ApplyResources(Me.btnFirst, "btnFirst")
        Me.btnFirst.Glyph = Global.ZulLib.My.Resources.Resources.BtnFirst
        Me.btnFirst.Id = 5
        Me.btnFirst.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Home))
        Me.btnFirst.Name = "btnFirst"
        '
        'btnPrev
        '
        resources.ApplyResources(Me.btnPrev, "btnPrev")
        Me.btnPrev.Glyph = Global.ZulLib.My.Resources.Resources.BtnPrev
        Me.btnPrev.Id = 6
        Me.btnPrev.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Left))
        Me.btnPrev.Name = "btnPrev"
        '
        'btnNext
        '
        resources.ApplyResources(Me.btnNext, "btnNext")
        Me.btnNext.Glyph = Global.ZulLib.My.Resources.Resources.BtnNext
        Me.btnNext.Id = 7
        Me.btnNext.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Right))
        Me.btnNext.Name = "btnNext"
        '
        'btnLast
        '
        resources.ApplyResources(Me.btnLast, "btnLast")
        Me.btnLast.Glyph = Global.ZulLib.My.Resources.Resources.BtnLast
        Me.btnLast.Id = 8
        Me.btnLast.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.[End]))
        Me.btnLast.Name = "btnLast"
        '
        'cmbList
        '
        resources.ApplyResources(Me.cmbList, "cmbList")
        Me.cmbList.Edit = Me.cmbListRep
        Me.cmbList.Id = 43
        Me.cmbList.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
        Me.cmbList.Name = "cmbList"
        '
        'cmbListRep
        '
        resources.ApplyResources(Me.cmbListRep, "cmbListRep")
        Me.cmbListRep.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbListRep.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cmbListRep.Name = "cmbListRep"
        Me.cmbListRep.View = Me.RepositoryItemGridLookUpEdit1View
        '
        'RepositoryItemGridLookUpEdit1View
        '
        Me.RepositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGridLookUpEdit1View.Name = "RepositoryItemGridLookUpEdit1View"
        Me.RepositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowIndicator = False
        '
        'btnClose
        '
        resources.ApplyResources(Me.btnClose, "btnClose")
        Me.btnClose.Glyph = Global.ZulLib.My.Resources.Resources.Close
        Me.btnClose.Id = 0
        Me.btnClose.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F4))
        Me.btnClose.Name = "btnClose"
        '
        'barStatus
        '
        Me.barStatus.BarName = "Status Bar"
        Me.barStatus.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.barStatus.DockCol = 0
        Me.barStatus.DockRow = 0
        Me.barStatus.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.barStatus.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.lblCreatedLabel), New DevExpress.XtraBars.LinkPersistInfo(Me.lblCreated), New DevExpress.XtraBars.LinkPersistInfo(Me.lblModifiedLabel), New DevExpress.XtraBars.LinkPersistInfo(Me.lblModified)})
        Me.barStatus.OptionsBar.AllowQuickCustomization = False
        Me.barStatus.OptionsBar.DrawDragBorder = False
        Me.barStatus.OptionsBar.UseWholeRow = True
        resources.ApplyResources(Me.barStatus, "barStatus")
        '
        'lblCreatedLabel
        '
        Me.lblCreatedLabel.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreatedLabel.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblCreatedLabel, "lblCreatedLabel")
        Me.lblCreatedLabel.Id = 26
        Me.lblCreatedLabel.Name = "lblCreatedLabel"
        Me.lblCreatedLabel.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'lblCreated
        '
        resources.ApplyResources(Me.lblCreated, "lblCreated")
        Me.lblCreated.Id = 31
        Me.lblCreated.Name = "lblCreated"
        Me.lblCreated.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'lblModifiedLabel
        '
        Me.lblModifiedLabel.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModifiedLabel.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblModifiedLabel, "lblModifiedLabel")
        Me.lblModifiedLabel.Id = 32
        Me.lblModifiedLabel.Name = "lblModifiedLabel"
        Me.lblModifiedLabel.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'lblModified
        '
        resources.ApplyResources(Me.lblModified, "lblModified")
        Me.lblModified.Id = 36
        Me.lblModified.Name = "lblModified"
        Me.lblModified.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'barPrint
        '
        Me.barPrint.BarName = "Print Bar"
        Me.barPrint.DockCol = 0
        Me.barPrint.DockRow = 1
        Me.barPrint.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.barPrint.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnPreview, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        resources.ApplyResources(Me.barPrint, "barPrint")
        Me.barPrint.Visible = False
        '
        'btnPrint
        '
        resources.ApplyResources(Me.btnPrint, "btnPrint")
        Me.btnPrint.Glyph = Global.ZulLib.My.Resources.Resources.Print_16x16
        Me.btnPrint.Id = 38
        Me.btnPrint.Name = "btnPrint"
        '
        'btnPreview
        '
        resources.ApplyResources(Me.btnPreview, "btnPreview")
        Me.btnPreview.Glyph = Global.ZulLib.My.Resources.Resources.Preview16x16
        Me.btnPreview.Id = 39
        Me.btnPreview.Name = "btnPreview"
        '
        'barItemCreate
        '
        resources.ApplyResources(Me.barItemCreate, "barItemCreate")
        Me.barItemCreate.Id = 21
        Me.barItemCreate.Name = "barItemCreate"
        Me.barItemCreate.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'barItemModify
        '
        resources.ApplyResources(Me.barItemModify, "barItemModify")
        Me.barItemModify.Id = 22
        Me.barItemModify.Name = "barItemModify"
        Me.barItemModify.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'BarStaticCreation
        '
        resources.ApplyResources(Me.BarStaticCreation, "BarStaticCreation")
        Me.BarStaticCreation.Id = 23
        Me.BarStaticCreation.Name = "BarStaticCreation"
        Me.BarStaticCreation.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'lblRecordStatus
        '
        resources.ApplyResources(Me.lblRecordStatus, "lblRecordStatus")
        Me.lblRecordStatus.Id = 40
        Me.lblRecordStatus.Name = "lblRecordStatus"
        Me.lblRecordStatus.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'errProv
        '
        Me.errProv.ContainerControl = Me
        '
        'ctlDataEditing
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "ctlDataEditing"
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbListRep, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.errProv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.valProvMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barItemCreate As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents barItemModify As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents BarStaticCreation As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblCreatedLabel As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblModifiedLabel As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblRecordStatus As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Public WithEvents btnClose As DevExpress.XtraBars.BarButtonItem
    Public WithEvents errProv As DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Public WithEvents valProvMain As DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Protected WithEvents btnPrint As DevExpress.XtraBars.BarButtonItem
    Protected WithEvents btnPreview As DevExpress.XtraBars.BarButtonItem
    Protected WithEvents barPrint As DevExpress.XtraBars.Bar
    Protected WithEvents lblCreated As DevExpress.XtraBars.BarStaticItem
    Protected WithEvents lblModified As DevExpress.XtraBars.BarStaticItem
    Protected WithEvents btnNew As DevExpress.XtraBars.BarButtonItem
    Public WithEvents btnDelete As DevExpress.XtraBars.BarButtonItem
    Protected WithEvents btnSaveAndNew As DevExpress.XtraBars.BarButtonItem
    Public WithEvents btnFirst As DevExpress.XtraBars.BarButtonItem
    Public WithEvents btnPrev As DevExpress.XtraBars.BarButtonItem
    Public WithEvents btnNext As DevExpress.XtraBars.BarButtonItem
    Public WithEvents btnLast As DevExpress.XtraBars.BarButtonItem
    Public WithEvents btnSave As DevExpress.XtraBars.BarButtonItem
    Public WithEvents cmbListRep As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Protected WithEvents btnCancel As DevExpress.XtraBars.BarButtonItem
    Public WithEvents barStatus As DevExpress.XtraBars.Bar
    Protected WithEvents btnRefresh As DevExpress.XtraBars.BarButtonItem
    Public WithEvents MainBar As DevExpress.XtraBars.Bar
    Friend WithEvents cmbList As DevExpress.XtraBars.BarEditItem
End Class
