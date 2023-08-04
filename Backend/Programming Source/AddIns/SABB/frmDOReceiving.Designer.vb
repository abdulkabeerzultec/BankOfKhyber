<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDOReceiving
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
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.grdItems = New DevExpress.XtraGrid.GridControl
        Me.grdItemsView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.grdOrders = New DevExpress.XtraGrid.GridControl
        Me.grdOrdersView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnTransfer = New System.Windows.Forms.Button
        Me.GroupBox7.SuspendLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItemsView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        CType(Me.grdOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdOrdersView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox7
        '
        Me.GroupBox7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox7.BackColor = System.Drawing.Color.White
        Me.GroupBox7.Controls.Add(Me.grdItems)
        Me.GroupBox7.Location = New System.Drawing.Point(3, 284)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(799, 285)
        Me.GroupBox7.TabIndex = 1
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Delivery Order Items"
        '
        'grdItems
        '
        Me.grdItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItems.Location = New System.Drawing.Point(6, 19)
        Me.grdItems.MainView = Me.grdItemsView
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(787, 260)
        Me.grdItems.TabIndex = 3
        Me.grdItems.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdItemsView})
        '
        'grdItemsView
        '
        Me.grdItemsView.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdItemsView.Appearance.FooterPanel.Options.UseFont = True
        Me.grdItemsView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdItemsView.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdItemsView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grdItemsView.GridControl = Me.grdItems
        Me.grdItemsView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.grdItemsView.Name = "grdItemsView"
        Me.grdItemsView.OptionsBehavior.Editable = False
        Me.grdItemsView.OptionsCustomization.AllowGroup = False
        Me.grdItemsView.OptionsNavigation.UseTabKey = False
        Me.grdItemsView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdItemsView.OptionsView.ShowAutoFilterRow = True
        Me.grdItemsView.OptionsView.ShowGroupPanel = False
        Me.grdItemsView.OptionsView.ShowIndicator = False
        Me.grdItemsView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
        Me.grdItemsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.BackColor = System.Drawing.Color.White
        Me.GroupBox6.Controls.Add(Me.grdOrders)
        Me.GroupBox6.Location = New System.Drawing.Point(3, 2)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(799, 279)
        Me.GroupBox6.TabIndex = 0
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Approved Delivery Orders"
        '
        'grdOrders
        '
        Me.grdOrders.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdOrders.Location = New System.Drawing.Point(6, 19)
        Me.grdOrders.MainView = Me.grdOrdersView
        Me.grdOrders.Name = "grdOrders"
        Me.grdOrders.Size = New System.Drawing.Size(787, 254)
        Me.grdOrders.TabIndex = 4
        Me.grdOrders.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdOrdersView})
        '
        'grdOrdersView
        '
        Me.grdOrdersView.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrdersView.Appearance.FooterPanel.Options.UseFont = True
        Me.grdOrdersView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrdersView.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdOrdersView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grdOrdersView.GridControl = Me.grdOrders
        Me.grdOrdersView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.grdOrdersView.Name = "grdOrdersView"
        Me.grdOrdersView.OptionsBehavior.Editable = False
        Me.grdOrdersView.OptionsCustomization.AllowGroup = False
        Me.grdOrdersView.OptionsNavigation.UseTabKey = False
        Me.grdOrdersView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdOrdersView.OptionsView.ShowAutoFilterRow = True
        Me.grdOrdersView.OptionsView.ShowGroupPanel = False
        Me.grdOrdersView.OptionsView.ShowIndicator = False
        Me.grdOrdersView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
        Me.grdOrdersView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Image = Global.SABBPlugin.My.Resources.Resources.Close16x16
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(716, 574)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(86, 26)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "&Close"
        '
        'btnTransfer
        '
        Me.btnTransfer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTransfer.Location = New System.Drawing.Point(590, 574)
        Me.btnTransfer.Name = "btnTransfer"
        Me.btnTransfer.Size = New System.Drawing.Size(120, 26)
        Me.btnTransfer.TabIndex = 2
        Me.btnTransfer.Text = "&Receive Assets"
        '
        'frmDOReceiving
        '
        Me.AcceptButton = Me.btnTransfer
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(806, 612)
        Me.Controls.Add(Me.btnTransfer)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox7)
        Me.DoubleBuffered = True
        Me.MinimumSize = New System.Drawing.Size(600, 500)
        Me.Name = "frmDOReceiving"
        Me.Text = "DO Receiving"
        Me.GroupBox7.ResumeLayout(False)
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItemsView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        CType(Me.grdOrders, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdOrdersView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnTransfer As System.Windows.Forms.Button
    Friend WithEvents grdItems As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdItemsView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdOrders As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdOrdersView As DevExpress.XtraGrid.Views.Grid.GridView
End Class
