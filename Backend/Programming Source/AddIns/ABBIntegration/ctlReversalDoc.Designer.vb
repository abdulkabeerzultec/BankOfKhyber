<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlReversalDoc
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl
        Me.btnReverseDoc = New DevExpress.XtraEditors.SimpleButton
        Me.btnReverseItem = New DevExpress.XtraEditors.SimpleButton
        Me.grdItems = New DevExpress.XtraGrid.GridControl
        Me.grdViewItems = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdReversedItems = New DevExpress.XtraGrid.GridControl
        Me.grdViewReversedItems = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.dtDocDate = New DevExpress.XtraEditors.DateEdit
        Me.txtDocNo = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.layoutReversed = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutItems = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.layoutPanel = New DevExpress.XtraLayout.LayoutControlItem
        CType(Me.errProv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.valProvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdViewItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdReversedItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdViewReversedItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDocDate.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDocDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutReversed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = True
        Me.LayoutControl1.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LayoutControl1.Appearance.DisabledLayoutItem.Options.UseForeColor = True
        Me.LayoutControl1.Controls.Add(Me.PanelControl1)
        Me.LayoutControl1.Controls.Add(Me.grdItems)
        Me.LayoutControl1.Controls.Add(Me.grdReversedItems)
        Me.LayoutControl1.Controls.Add(Me.dtDocDate)
        Me.LayoutControl1.Controls.Add(Me.txtDocNo)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 55)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(700, 480)
        Me.LayoutControl1.TabIndex = 4
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btnReverseDoc)
        Me.PanelControl1.Controls.Add(Me.btnReverseItem)
        Me.PanelControl1.Location = New System.Drawing.Point(10, 442)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(681, 29)
        Me.PanelControl1.TabIndex = 8
        '
        'btnReverseDoc
        '
        Me.btnReverseDoc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverseDoc.Location = New System.Drawing.Point(354, 4)
        Me.btnReverseDoc.Name = "btnReverseDoc"
        Me.btnReverseDoc.Size = New System.Drawing.Size(136, 23)
        Me.btnReverseDoc.TabIndex = 1
        Me.btnReverseDoc.Text = "Reverse Complete Doc"
        '
        'btnReverseItem
        '
        Me.btnReverseItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverseItem.Location = New System.Drawing.Point(526, 4)
        Me.btnReverseItem.Name = "btnReverseItem"
        Me.btnReverseItem.Size = New System.Drawing.Size(142, 23)
        Me.btnReverseItem.TabIndex = 0
        Me.btnReverseItem.Text = "Reverse Selected Item(s)"
        '
        'grdItems
        '
        Me.grdItems.Location = New System.Drawing.Point(10, 287)
        Me.grdItems.MainView = Me.grdViewItems
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(681, 144)
        Me.grdItems.TabIndex = 7
        Me.grdItems.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdViewItems})
        '
        'grdViewItems
        '
        Me.grdViewItems.GridControl = Me.grdItems
        Me.grdViewItems.Name = "grdViewItems"
        Me.grdViewItems.OptionsView.ShowGroupPanel = False
        Me.grdViewItems.OptionsView.ShowIndicator = False
        '
        'grdReversedItems
        '
        Me.grdReversedItems.Location = New System.Drawing.Point(10, 60)
        Me.grdReversedItems.MainView = Me.grdViewReversedItems
        Me.grdReversedItems.Name = "grdReversedItems"
        Me.grdReversedItems.Size = New System.Drawing.Size(681, 190)
        Me.grdReversedItems.TabIndex = 6
        Me.grdReversedItems.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdViewReversedItems})
        '
        'grdViewReversedItems
        '
        Me.grdViewReversedItems.GridControl = Me.grdReversedItems
        Me.grdViewReversedItems.Name = "grdViewReversedItems"
        Me.grdViewReversedItems.OptionsView.ShowGroupPanel = False
        Me.grdViewReversedItems.OptionsView.ShowIndicator = False
        '
        'dtDocDate
        '
        Me.dtDocDate.EditValue = Nothing
        Me.dtDocDate.Location = New System.Drawing.Point(435, 7)
        Me.dtDocDate.Name = "dtDocDate"
        Me.dtDocDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtDocDate.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.dtDocDate.Size = New System.Drawing.Size(259, 19)
        Me.dtDocDate.StyleController = Me.LayoutControl1
        Me.dtDocDate.TabIndex = 5
        '
        'txtDocNo
        '
        Me.txtDocNo.Location = New System.Drawing.Point(86, 7)
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(259, 19)
        Me.txtDocNo.StyleController = Me.LayoutControl1
        Me.txtDocNo.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.layoutReversed, Me.LayoutItems})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(700, 480)
        Me.LayoutControlGroup1.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.txtDocNo
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(349, 30)
        Me.LayoutControlItem1.Text = "Document No"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(74, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.dtDocDate
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(349, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(349, 30)
        Me.LayoutControlItem2.Text = "Document Date"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(74, 13)
        '
        'layoutReversed
        '
        Me.layoutReversed.AppearanceGroup.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.layoutReversed.AppearanceGroup.Options.UseFont = True
        Me.layoutReversed.AppearanceGroup.Options.UseTextOptions = True
        Me.layoutReversed.AppearanceGroup.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.layoutReversed.CustomizationFormText = "Reversed Items"
        Me.layoutReversed.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3})
        Me.layoutReversed.Location = New System.Drawing.Point(0, 30)
        Me.layoutReversed.Name = "layoutReversed"
        Me.layoutReversed.Size = New System.Drawing.Size(698, 227)
        Me.layoutReversed.Text = "Reversed Items"
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.grdReversedItems
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(692, 201)
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutItems
        '
        Me.LayoutItems.AppearanceGroup.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutItems.AppearanceGroup.Options.UseFont = True
        Me.LayoutItems.AppearanceGroup.Options.UseTextOptions = True
        Me.LayoutItems.AppearanceGroup.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LayoutItems.CustomizationFormText = "Items"
        Me.LayoutItems.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4, Me.layoutPanel})
        Me.LayoutItems.Location = New System.Drawing.Point(0, 257)
        Me.LayoutItems.Name = "LayoutItems"
        Me.LayoutItems.Size = New System.Drawing.Size(698, 221)
        Me.LayoutItems.Text = "Items"
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.grdItems
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(692, 155)
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'layoutPanel
        '
        Me.layoutPanel.Control = Me.PanelControl1
        Me.layoutPanel.CustomizationFormText = "LayoutControlItem5"
        Me.layoutPanel.Location = New System.Drawing.Point(0, 155)
        Me.layoutPanel.MaxSize = New System.Drawing.Size(0, 40)
        Me.layoutPanel.MinSize = New System.Drawing.Size(111, 40)
        Me.layoutPanel.Name = "layoutPanel"
        Me.layoutPanel.Size = New System.Drawing.Size(692, 40)
        Me.layoutPanel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.layoutPanel.Text = "layoutPanel"
        Me.layoutPanel.TextLocation = DevExpress.Utils.Locations.Left
        Me.layoutPanel.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutPanel.TextToControlDistance = 0
        Me.layoutPanel.TextVisible = False
        '
        'ctlReversalDoc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "ctlReversalDoc"
        Me.Size = New System.Drawing.Size(700, 558)
        Me.Controls.SetChildIndex(Me.LayoutControl1, 0)
        CType(Me.errProv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.valProvMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdViewItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdReversedItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdViewReversedItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDocDate.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDocDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutReversed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents dtDocDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtDocNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdItems As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdViewItems As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdReversedItems As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdViewReversedItems As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents layoutReversed As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutItems As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnReverseDoc As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnReverseItem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents layoutPanel As DevExpress.XtraLayout.LayoutControlItem

End Class
