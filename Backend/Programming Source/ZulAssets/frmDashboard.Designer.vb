<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDashboard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDashboard))
        Me.grdAstItems = New DevExpress.XtraGrid.GridControl
        Me.grdAstItemsView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdAssets = New DevExpress.XtraGrid.GridControl
        Me.grdAssetsView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.tabControl = New System.Windows.Forms.TabControl
        Me.tabPageLocations = New System.Windows.Forms.TabPage
        Me.TrvLocations = New System.Windows.Forms.TreeView
        Me.tabPageAssetCategory = New System.Windows.Forms.TabPage
        Me.trvAssetCat = New System.Windows.Forms.TreeView
        Me.tabPageItemCategory = New System.Windows.Forms.TabPage
        Me.trvItemCat = New System.Windows.Forms.TreeView
        Me.tabPageCustodians = New System.Windows.Forms.TabPage
        Me.grdCust = New DevExpress.XtraGrid.GridControl
        Me.grdCustView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.btnExit = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.btnAssetsBarcode = New System.Windows.Forms.Button
        Me.btnLocationBarcode = New System.Windows.Forms.Button
        Me.grdAssetDetails = New DevExpress.XtraGrid.GridControl
        Me.grdAssetDetailsView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.pb = New System.Windows.Forms.ProgressBar
        Me.chkShowPreview = New System.Windows.Forms.CheckBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkShowSubLocationAssets = New System.Windows.Forms.CheckBox
        CType(Me.grdAstItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAstItemsView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAssetsView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControl.SuspendLayout()
        Me.tabPageLocations.SuspendLayout()
        Me.tabPageAssetCategory.SuspendLayout()
        Me.tabPageItemCategory.SuspendLayout()
        Me.tabPageCustodians.SuspendLayout()
        CType(Me.grdCust, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCustView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAssetDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAssetDetailsView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdAstItems
        '
        Me.grdAstItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAstItems.Location = New System.Drawing.Point(0, 0)
        Me.grdAstItems.MainView = Me.grdAstItemsView
        Me.grdAstItems.Name = "grdAstItems"
        Me.grdAstItems.Size = New System.Drawing.Size(614, 273)
        Me.grdAstItems.TabIndex = 1
        Me.grdAstItems.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdAstItemsView})
        '
        'grdAstItemsView
        '
        Me.grdAstItemsView.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAstItemsView.Appearance.FooterPanel.Options.UseFont = True
        Me.grdAstItemsView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAstItemsView.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdAstItemsView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grdAstItemsView.GridControl = Me.grdAstItems
        Me.grdAstItemsView.Name = "grdAstItemsView"
        Me.grdAstItemsView.OptionsBehavior.Editable = False
        Me.grdAstItemsView.OptionsCustomization.AllowGroup = False
        Me.grdAstItemsView.OptionsNavigation.UseTabKey = False
        Me.grdAstItemsView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdAstItemsView.OptionsSelection.MultiSelect = True
        Me.grdAstItemsView.OptionsView.ShowAutoFilterRow = True
        Me.grdAstItemsView.OptionsView.ShowGroupPanel = False
        Me.grdAstItemsView.OptionsView.ShowIndicator = False
        Me.grdAstItemsView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
        '
        'grdAssets
        '
        Me.grdAssets.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAssets.Location = New System.Drawing.Point(0, 0)
        Me.grdAssets.MainView = Me.grdAssetsView
        Me.grdAssets.Name = "grdAssets"
        Me.grdAssets.Size = New System.Drawing.Size(614, 274)
        Me.grdAssets.TabIndex = 1
        Me.grdAssets.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdAssetsView})
        '
        'grdAssetsView
        '
        Me.grdAssetsView.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAssetsView.Appearance.FooterPanel.Options.UseFont = True
        Me.grdAssetsView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAssetsView.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdAssetsView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grdAssetsView.GridControl = Me.grdAssets
        Me.grdAssetsView.Name = "grdAssetsView"
        Me.grdAssetsView.OptionsBehavior.Editable = False
        Me.grdAssetsView.OptionsCustomization.AllowGroup = False
        Me.grdAssetsView.OptionsNavigation.UseTabKey = False
        Me.grdAssetsView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdAssetsView.OptionsSelection.MultiSelect = True
        Me.grdAssetsView.OptionsView.ShowAutoFilterRow = True
        Me.grdAssetsView.OptionsView.ShowGroupPanel = False
        Me.grdAssetsView.OptionsView.ShowIndicator = False
        Me.grdAssetsView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
        '
        'tabControl
        '
        Me.tabControl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tabControl.Controls.Add(Me.tabPageLocations)
        Me.tabControl.Controls.Add(Me.tabPageAssetCategory)
        Me.tabControl.Controls.Add(Me.tabPageItemCategory)
        Me.tabControl.Controls.Add(Me.tabPageCustodians)
        Me.tabControl.Location = New System.Drawing.Point(5, 77)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedIndex = 0
        Me.tabControl.Size = New System.Drawing.Size(325, 499)
        Me.tabControl.TabIndex = 0
        '
        'tabPageLocations
        '
        Me.tabPageLocations.BackColor = System.Drawing.Color.White
        Me.tabPageLocations.Controls.Add(Me.TrvLocations)
        Me.tabPageLocations.Location = New System.Drawing.Point(4, 22)
        Me.tabPageLocations.Name = "tabPageLocations"
        Me.tabPageLocations.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPageLocations.Size = New System.Drawing.Size(317, 473)
        Me.tabPageLocations.TabIndex = 0
        Me.tabPageLocations.Text = "Locations"
        Me.tabPageLocations.UseVisualStyleBackColor = True
        '
        'TrvLocations
        '
        Me.TrvLocations.AllowDrop = True
        Me.TrvLocations.BackColor = System.Drawing.Color.White
        Me.TrvLocations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TrvLocations.HideSelection = False
        Me.TrvLocations.Location = New System.Drawing.Point(3, 3)
        Me.TrvLocations.Name = "TrvLocations"
        Me.TrvLocations.Size = New System.Drawing.Size(311, 467)
        Me.TrvLocations.TabIndex = 0
        '
        'tabPageAssetCategory
        '
        Me.tabPageAssetCategory.Controls.Add(Me.trvAssetCat)
        Me.tabPageAssetCategory.Location = New System.Drawing.Point(4, 22)
        Me.tabPageAssetCategory.Name = "tabPageAssetCategory"
        Me.tabPageAssetCategory.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPageAssetCategory.Size = New System.Drawing.Size(317, 473)
        Me.tabPageAssetCategory.TabIndex = 3
        Me.tabPageAssetCategory.Text = "Asset By Category"
        Me.tabPageAssetCategory.UseVisualStyleBackColor = True
        '
        'trvAssetCat
        '
        Me.trvAssetCat.AllowDrop = True
        Me.trvAssetCat.BackColor = System.Drawing.Color.White
        Me.trvAssetCat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvAssetCat.HideSelection = False
        Me.trvAssetCat.Location = New System.Drawing.Point(3, 3)
        Me.trvAssetCat.Name = "trvAssetCat"
        Me.trvAssetCat.Size = New System.Drawing.Size(311, 467)
        Me.trvAssetCat.TabIndex = 1
        '
        'tabPageItemCategory
        '
        Me.tabPageItemCategory.BackColor = System.Drawing.Color.White
        Me.tabPageItemCategory.Controls.Add(Me.trvItemCat)
        Me.tabPageItemCategory.Location = New System.Drawing.Point(4, 22)
        Me.tabPageItemCategory.Name = "tabPageItemCategory"
        Me.tabPageItemCategory.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPageItemCategory.Size = New System.Drawing.Size(317, 473)
        Me.tabPageItemCategory.TabIndex = 1
        Me.tabPageItemCategory.Text = "Item By Category"
        Me.tabPageItemCategory.UseVisualStyleBackColor = True
        '
        'trvItemCat
        '
        Me.trvItemCat.AllowDrop = True
        Me.trvItemCat.BackColor = System.Drawing.Color.White
        Me.trvItemCat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvItemCat.HideSelection = False
        Me.trvItemCat.Location = New System.Drawing.Point(3, 3)
        Me.trvItemCat.Name = "trvItemCat"
        Me.trvItemCat.Size = New System.Drawing.Size(311, 467)
        Me.trvItemCat.TabIndex = 0
        '
        'tabPageCustodians
        '
        Me.tabPageCustodians.BackColor = System.Drawing.Color.White
        Me.tabPageCustodians.Controls.Add(Me.grdCust)
        Me.tabPageCustodians.Location = New System.Drawing.Point(4, 22)
        Me.tabPageCustodians.Name = "tabPageCustodians"
        Me.tabPageCustodians.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPageCustodians.Size = New System.Drawing.Size(317, 473)
        Me.tabPageCustodians.TabIndex = 2
        Me.tabPageCustodians.Text = "Custodians"
        Me.tabPageCustodians.UseVisualStyleBackColor = True
        '
        'grdCust
        '
        Me.grdCust.AllowDrop = True
        Me.grdCust.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdCust.Location = New System.Drawing.Point(3, 3)
        Me.grdCust.MainView = Me.grdCustView
        Me.grdCust.Name = "grdCust"
        Me.grdCust.Size = New System.Drawing.Size(311, 467)
        Me.grdCust.TabIndex = 77
        Me.grdCust.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdCustView})
        '
        'grdCustView
        '
        Me.grdCustView.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdCustView.Appearance.FooterPanel.Options.UseFont = True
        Me.grdCustView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdCustView.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdCustView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grdCustView.GridControl = Me.grdCust
        Me.grdCustView.Name = "grdCustView"
        Me.grdCustView.OptionsBehavior.Editable = False
        Me.grdCustView.OptionsCustomization.AllowGroup = False
        Me.grdCustView.OptionsNavigation.UseTabKey = False
        Me.grdCustView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdCustView.OptionsSelection.EnableAppearanceHideSelection = False
        Me.grdCustView.OptionsView.ShowAutoFilterRow = True
        Me.grdCustView.OptionsView.ShowGroupPanel = False
        Me.grdCustView.OptionsView.ShowIndicator = False
        Me.grdCustView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Image = Global.ZulAssets.My.Resources.Icons.Close
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(847, 600)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 30)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "&Close"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.InitialImage = CType(resources.GetObject("PictureBox1.InitialImage"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(5, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(410, 69)
        Me.PictureBox1.TabIndex = 12
        Me.PictureBox1.TabStop = False
        '
        'btnAssetsBarcode
        '
        Me.btnAssetsBarcode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAssetsBarcode.Image = Global.ZulAssets.My.Resources.Icons.Print
        Me.btnAssetsBarcode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAssetsBarcode.Location = New System.Drawing.Point(560, 600)
        Me.btnAssetsBarcode.Name = "btnAssetsBarcode"
        Me.btnAssetsBarcode.Size = New System.Drawing.Size(130, 30)
        Me.btnAssetsBarcode.TabIndex = 3
        Me.btnAssetsBarcode.Text = "&Assets Barcode"
        '
        'btnLocationBarcode
        '
        Me.btnLocationBarcode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLocationBarcode.Image = Global.ZulAssets.My.Resources.Icons.Print
        Me.btnLocationBarcode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLocationBarcode.Location = New System.Drawing.Point(696, 600)
        Me.btnLocationBarcode.Name = "btnLocationBarcode"
        Me.btnLocationBarcode.Size = New System.Drawing.Size(130, 30)
        Me.btnLocationBarcode.TabIndex = 4
        Me.btnLocationBarcode.Text = "&Location Barcode"
        '
        'grdAssetDetails
        '
        Me.grdAssetDetails.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grdAssetDetails.Location = New System.Drawing.Point(0, 276)
        Me.grdAssetDetails.MainView = Me.grdAssetDetailsView
        Me.grdAssetDetails.Name = "grdAssetDetails"
        Me.grdAssetDetails.Size = New System.Drawing.Size(614, 223)
        Me.grdAssetDetails.TabIndex = 2
        Me.grdAssetDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdAssetDetailsView})
        '
        'grdAssetDetailsView
        '
        Me.grdAssetDetailsView.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAssetDetailsView.Appearance.FooterPanel.Options.UseFont = True
        Me.grdAssetDetailsView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAssetDetailsView.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdAssetDetailsView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grdAssetDetailsView.GridControl = Me.grdAssetDetails
        Me.grdAssetDetailsView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.grdAssetDetailsView.Name = "grdAssetDetailsView"
        Me.grdAssetDetailsView.OptionsBehavior.Editable = False
        Me.grdAssetDetailsView.OptionsCustomization.AllowGroup = False
        Me.grdAssetDetailsView.OptionsNavigation.UseTabKey = False
        Me.grdAssetDetailsView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdAssetDetailsView.OptionsView.ShowAutoFilterRow = True
        Me.grdAssetDetailsView.OptionsView.ShowGroupPanel = False
        Me.grdAssetDetailsView.OptionsView.ShowIndicator = False
        Me.grdAssetDetailsView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
        Me.grdAssetDetailsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'pb
        '
        Me.pb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pb.Location = New System.Drawing.Point(5, 578)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(942, 14)
        Me.pb.TabIndex = 41
        Me.pb.Visible = False
        '
        'chkShowPreview
        '
        Me.chkShowPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkShowPreview.AutoSize = True
        Me.chkShowPreview.Location = New System.Drawing.Point(417, 608)
        Me.chkShowPreview.Name = "chkShowPreview"
        Me.chkShowPreview.Size = New System.Drawing.Size(93, 17)
        Me.chkShowPreview.TabIndex = 43
        Me.chkShowPreview.Text = "Show Preview"
        Me.chkShowPreview.UseVisualStyleBackColor = True
        Me.chkShowPreview.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.grdAssetDetails)
        Me.Panel1.Controls.Add(Me.grdAstItems)
        Me.Panel1.Controls.Add(Me.grdAssets)
        Me.Panel1.Location = New System.Drawing.Point(333, 77)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(614, 499)
        Me.Panel1.TabIndex = 44
        '
        'chkShowSubLocationAssets
        '
        Me.chkShowSubLocationAssets.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkShowSubLocationAssets.AutoSize = True
        Me.chkShowSubLocationAssets.Location = New System.Drawing.Point(5, 608)
        Me.chkShowSubLocationAssets.Name = "chkShowSubLocationAssets"
        Me.chkShowSubLocationAssets.Size = New System.Drawing.Size(156, 17)
        Me.chkShowSubLocationAssets.TabIndex = 45
        Me.chkShowSubLocationAssets.Text = "Show Sub Locations Assets"
        Me.chkShowSubLocationAssets.UseVisualStyleBackColor = True
        '
        'frmDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(953, 639)
        Me.Controls.Add(Me.chkShowSubLocationAssets)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.chkShowPreview)
        Me.Controls.Add(Me.pb)
        Me.Controls.Add(Me.btnLocationBarcode)
        Me.Controls.Add(Me.btnAssetsBarcode)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.tabControl)
        Me.DoubleBuffered = True
        Me.MinimumSize = New System.Drawing.Size(700, 500)
        Me.Name = "frmDashboard"
        Me.Text = "Asset Administration"
        CType(Me.grdAstItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAstItemsView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAssets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAssetsView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControl.ResumeLayout(False)
        Me.tabPageLocations.ResumeLayout(False)
        Me.tabPageAssetCategory.ResumeLayout(False)
        Me.tabPageItemCategory.ResumeLayout(False)
        Me.tabPageCustodians.ResumeLayout(False)
        CType(Me.grdCust, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCustView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAssetDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAssetDetailsView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tabControl As System.Windows.Forms.TabControl
    Friend WithEvents tabPageLocations As System.Windows.Forms.TabPage
    Friend WithEvents TrvLocations As System.Windows.Forms.TreeView
    Friend WithEvents tabPageItemCategory As System.Windows.Forms.TabPage
    Friend WithEvents trvItemCat As System.Windows.Forms.TreeView
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnAssetsBarcode As System.Windows.Forms.Button
    Friend WithEvents btnLocationBarcode As System.Windows.Forms.Button
    Friend WithEvents tabPageCustodians As System.Windows.Forms.TabPage
    Friend WithEvents grdAssets As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdAssetsView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdCust As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdCustView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdAstItems As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdAstItemsView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdAssetDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdAssetDetailsView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents tabPageAssetCategory As System.Windows.Forms.TabPage
    Friend WithEvents trvAssetCat As System.Windows.Forms.TreeView
    Friend WithEvents chkShowPreview As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkShowSubLocationAssets As System.Windows.Forms.CheckBox
End Class
