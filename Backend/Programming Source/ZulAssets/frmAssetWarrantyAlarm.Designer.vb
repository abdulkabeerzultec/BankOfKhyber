<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAssetWarrantyAlarm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAssetWarrantyAlarm))
        Me.btnClose = New System.Windows.Forms.Button
        Me.grd = New DevExpress.XtraGrid.GridControl
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnDismissALL = New System.Windows.Forms.Button
        Me.btnOpenAsset = New System.Windows.Forms.Button
        Me.btnRefresh = New System.Windows.Forms.Button
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Image = Global.ZulAssets.My.Resources.Icons.Close
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(681, 428)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(91, 31)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "&Close"
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.Location = New System.Drawing.Point(3, 2)
        Me.grd.MainView = Me.grv
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(784, 413)
        Me.grd.TabIndex = 77
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv, Me.GridView3})
        '
        'grv
        '
        Me.grv.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grv.Appearance.FooterPanel.Options.UseFont = True
        Me.grv.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grv.Appearance.HeaderPanel.Options.UseFont = True
        Me.grv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grv.GridControl = Me.grd
        Me.grv.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.grv.Name = "grv"
        Me.grv.OptionsBehavior.Editable = False
        Me.grv.OptionsCustomization.AllowGroup = False
        Me.grv.OptionsNavigation.UseTabKey = False
        Me.grv.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv.OptionsView.ShowAutoFilterRow = True
        Me.grv.OptionsView.ShowGroupPanel = False
        Me.grv.OptionsView.ShowIndicator = False
        Me.grv.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
        Me.grv.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'GridView3
        '
        Me.GridView3.GridControl = Me.grd
        Me.GridView3.Name = "GridView3"
        '
        'imageList1
        '
        Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imageList1.TransparentColor = System.Drawing.Color.Magenta
        Me.imageList1.Images.SetKeyName(0, "")
        Me.imageList1.Images.SetKeyName(1, "")
        Me.imageList1.Images.SetKeyName(2, "")
        Me.imageList1.Images.SetKeyName(3, "")
        '
        'btnDismissALL
        '
        Me.btnDismissALL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDismissALL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnDismissALL.Image = Global.ZulAssets.My.Resources.Icons.ConnectError
        Me.btnDismissALL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDismissALL.Location = New System.Drawing.Point(555, 428)
        Me.btnDismissALL.Name = "btnDismissALL"
        Me.btnDismissALL.Size = New System.Drawing.Size(108, 31)
        Me.btnDismissALL.TabIndex = 78
        Me.btnDismissALL.Text = "Dismiss All"
        '
        'btnOpenAsset
        '
        Me.btnOpenAsset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOpenAsset.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnOpenAsset.Image = Global.ZulAssets.My.Resources.Icons.Connected
        Me.btnOpenAsset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOpenAsset.Location = New System.Drawing.Point(425, 428)
        Me.btnOpenAsset.Name = "btnOpenAsset"
        Me.btnOpenAsset.Size = New System.Drawing.Size(108, 31)
        Me.btnOpenAsset.TabIndex = 79
        Me.btnOpenAsset.Text = "Open Asset"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnRefresh.Image = Global.ZulAssets.My.Resources.Icons.Refresh
        Me.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefresh.Location = New System.Drawing.Point(298, 428)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(108, 31)
        Me.btnRefresh.TabIndex = 80
        Me.btnRefresh.Text = "Refresh"
        '
        'frmAssetWarrantyAlarm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(784, 462)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnOpenAsset)
        Me.Controls.Add(Me.btnDismissALL)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.btnClose)
        Me.Name = "frmAssetWarrantyAlarm"
        Me.Text = "Asset Warranty Alarm"
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents imageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnDismissALL As System.Windows.Forms.Button
    Friend WithEvents btnOpenAsset As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
End Class
