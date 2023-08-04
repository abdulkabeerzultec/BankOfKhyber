<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlGoodsReceiving
    Inherits ZulLib.ctlDataEditing

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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl
        Me.grdItems = New DevExpress.XtraGrid.GridControl
        Me.grdViewItems = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.dtDocDate = New DevExpress.XtraEditors.DateEdit
        Me.txtDeliveryNo = New DevExpress.XtraEditors.TextEdit
        Me.txtPONo = New DevExpress.XtraEditors.TextEdit
        Me.txtGRNo = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutGridGRItems = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem
        CType(Me.errProv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.valProvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbListRep, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdViewItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDocDate.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDocDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeliveryNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPONo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGRNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutGridGRItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbListRep
        '
        Me.cmbListRep.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = True
        Me.LayoutControl1.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LayoutControl1.Appearance.DisabledLayoutItem.Options.UseForeColor = True
        Me.LayoutControl1.Controls.Add(Me.grdItems)
        Me.LayoutControl1.Controls.Add(Me.dtDocDate)
        Me.LayoutControl1.Controls.Add(Me.txtDeliveryNo)
        Me.LayoutControl1.Controls.Add(Me.txtPONo)
        Me.LayoutControl1.Controls.Add(Me.txtGRNo)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 55)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(727, 421)
        Me.LayoutControl1.TabIndex = 4
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'grdItems
        '
        Me.grdItems.Location = New System.Drawing.Point(10, 89)
        Me.grdItems.MainView = Me.grdViewItems
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(708, 323)
        Me.grdItems.TabIndex = 9
        Me.grdItems.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdViewItems})
        '
        'grdViewItems
        '
        Me.grdViewItems.GridControl = Me.grdItems
        Me.grdViewItems.Name = "grdViewItems"
        Me.grdViewItems.OptionsCustomization.AllowSort = False
        Me.grdViewItems.OptionsView.ShowGroupPanel = False
        Me.grdViewItems.OptionsView.ShowIndicator = False
        '
        'dtDocDate
        '
        Me.dtDocDate.EditValue = Nothing
        Me.dtDocDate.Location = New System.Drawing.Point(453, 37)
        Me.dtDocDate.Name = "dtDocDate"
        Me.dtDocDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtDocDate.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.dtDocDate.Size = New System.Drawing.Size(268, 19)
        Me.dtDocDate.StyleController = Me.LayoutControl1
        Me.dtDocDate.TabIndex = 9
        '
        'txtDeliveryNo
        '
        Me.txtDeliveryNo.Location = New System.Drawing.Point(91, 37)
        Me.txtDeliveryNo.Name = "txtDeliveryNo"
        Me.txtDeliveryNo.Size = New System.Drawing.Size(267, 19)
        Me.txtDeliveryNo.StyleController = Me.LayoutControl1
        Me.txtDeliveryNo.TabIndex = 6
        '
        'txtPONo
        '
        Me.txtPONo.Location = New System.Drawing.Point(454, 7)
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.Size = New System.Drawing.Size(267, 19)
        Me.txtPONo.StyleController = Me.LayoutControl1
        Me.txtPONo.TabIndex = 5
        '
        'txtGRNo
        '
        Me.txtGRNo.Location = New System.Drawing.Point(91, 7)
        Me.txtGRNo.Name = "txtGRNo"
        Me.txtGRNo.Size = New System.Drawing.Size(268, 19)
        Me.txtGRNo.StyleController = Me.LayoutControl1
        Me.txtGRNo.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem3, Me.LayoutControlItem2, Me.LayoutGridGRItems, Me.LayoutControlItem9})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(727, 421)
        Me.LayoutControlGroup1.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.txtGRNo
        Me.LayoutControlItem1.CustomizationFormText = "Code"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(363, 30)
        Me.LayoutControlItem1.Text = "GR Number"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(79, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.txtDeliveryNo
        Me.LayoutControlItem3.CustomizationFormText = "Ar Name"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 30)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(362, 30)
        Me.LayoutControlItem3.Text = "Delivery Number"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(79, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.txtPONo
        Me.LayoutControlItem2.CustomizationFormText = "En Name"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(363, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(362, 30)
        Me.LayoutControlItem2.Text = "PO Number"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(79, 13)
        '
        'LayoutGridGRItems
        '
        Me.LayoutGridGRItems.AppearanceGroup.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutGridGRItems.AppearanceGroup.Options.UseFont = True
        Me.LayoutGridGRItems.AppearanceGroup.Options.UseTextOptions = True
        Me.LayoutGridGRItems.AppearanceGroup.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LayoutGridGRItems.CustomizationFormText = "Items Price"
        Me.LayoutGridGRItems.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4})
        Me.LayoutGridGRItems.Location = New System.Drawing.Point(0, 60)
        Me.LayoutGridGRItems.Name = "LayoutGrdItemPrice"
        Me.LayoutGridGRItems.Size = New System.Drawing.Size(725, 359)
        Me.LayoutGridGRItems.Text = "Goods Receiving Items "
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.grdItems
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(719, 334)
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.dtDocDate
        Me.LayoutControlItem9.CustomizationFormText = "Posting Date"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(362, 30)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(363, 30)
        Me.LayoutControlItem9.Text = "Posting Date"
        Me.LayoutControlItem9.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(79, 13)
        '
        'ctlGoodsReceiving
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "ctlGoodsReceiving"
        Me.Size = New System.Drawing.Size(727, 501)
        Me.Controls.SetChildIndex(Me.LayoutControl1, 0)
        CType(Me.errProv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.valProvMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbListRep, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdViewItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDocDate.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDocDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeliveryNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPONo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGRNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutGridGRItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents txtDeliveryNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPONo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtGRNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutGridGRItems As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents dtDocDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdItems As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdViewItems As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem

End Class
