﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlVendorReturn
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
        Me.txtPO = New DevExpress.XtraEditors.TextEdit
        Me.dtDocDate = New DevExpress.XtraEditors.DateEdit
        Me.txtDocNo = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutGridItems = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup
        CType(Me.errProv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.valProvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdViewItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDocDate.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDocDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutGridItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = True
        Me.LayoutControl1.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LayoutControl1.Appearance.DisabledLayoutItem.Options.UseForeColor = True
        Me.LayoutControl1.Controls.Add(Me.grdItems)
        Me.LayoutControl1.Controls.Add(Me.txtPO)
        Me.LayoutControl1.Controls.Add(Me.dtDocDate)
        Me.LayoutControl1.Controls.Add(Me.txtDocNo)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 55)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(751, 350)
        Me.LayoutControl1.TabIndex = 4
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'grdItems
        '
        Me.grdItems.Location = New System.Drawing.Point(10, 90)
        Me.grdItems.MainView = Me.grdViewItems
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(732, 251)
        Me.grdItems.TabIndex = 9
        Me.grdItems.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdViewItems})
        '
        'grdViewItems
        '
        Me.grdViewItems.GridControl = Me.grdItems
        Me.grdViewItems.Name = "grdViewItems"
        Me.grdViewItems.OptionsView.ShowGroupPanel = False
        Me.grdViewItems.OptionsView.ShowIndicator = False
        '
        'txtPO
        '
        Me.txtPO.Location = New System.Drawing.Point(86, 37)
        Me.txtPO.Name = "txtPO"
        Me.txtPO.Size = New System.Drawing.Size(284, 19)
        Me.txtPO.StyleController = Me.LayoutControl1
        Me.txtPO.TabIndex = 6
        '
        'dtDocDate
        '
        Me.dtDocDate.EditValue = Nothing
        Me.dtDocDate.Location = New System.Drawing.Point(460, 7)
        Me.dtDocDate.Name = "dtDocDate"
        Me.dtDocDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtDocDate.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.dtDocDate.Size = New System.Drawing.Size(285, 19)
        Me.dtDocDate.StyleController = Me.LayoutControl1
        Me.dtDocDate.TabIndex = 5
        '
        'txtDocNo
        '
        Me.txtDocNo.Location = New System.Drawing.Point(86, 7)
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(284, 19)
        Me.txtDocNo.StyleController = Me.LayoutControl1
        Me.txtDocNo.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutGridItems})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(751, 350)
        Me.LayoutControlGroup1.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.txtDocNo
        Me.LayoutControlItem1.CustomizationFormText = "Document No"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(374, 30)
        Me.LayoutControlItem1.Text = "Document No"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(74, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.dtDocDate
        Me.LayoutControlItem2.CustomizationFormText = "Document Date"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(374, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(375, 60)
        Me.LayoutControlItem2.Text = "Document Date"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(74, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.txtPO
        Me.LayoutControlItem3.CustomizationFormText = "PO"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 30)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(374, 30)
        Me.LayoutControlItem3.Text = "PO"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(74, 13)
        '
        'LayoutGridItems
        '
        Me.LayoutGridItems.AppearanceGroup.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutGridItems.AppearanceGroup.Options.UseFont = True
        Me.LayoutGridItems.AppearanceGroup.Options.UseTextOptions = True
        Me.LayoutGridItems.AppearanceGroup.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LayoutGridItems.CustomizationFormText = "Items"
        Me.LayoutGridItems.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem5})
        Me.LayoutGridItems.Location = New System.Drawing.Point(0, 60)
        Me.LayoutGridItems.Name = "LayoutGridItems"
        Me.LayoutGridItems.Size = New System.Drawing.Size(749, 288)
        Me.LayoutGridItems.Text = "Items"
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.grdItems
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(743, 262)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseTextOptions = True
        Me.LayoutControlGroup2.AppearanceGroup.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LayoutControlGroup2.CustomizationFormText = "Items Price"
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlGroup2.Name = "LayoutGrdItemPrice"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(725, 361)
        Me.LayoutControlGroup2.Text = "Goods Receiving Items "
        '
        'ctlVendorReturn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "ctlVendorReturn"
        Me.Controls.SetChildIndex(Me.LayoutControl1, 0)
        CType(Me.errProv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.valProvMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdViewItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDocDate.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDocDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutGridItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents txtPO As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dtDocDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtDocNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutGridItems As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents grdItems As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdViewItems As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem

End Class
