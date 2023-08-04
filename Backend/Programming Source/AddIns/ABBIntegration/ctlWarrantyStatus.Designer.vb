<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlWarrantyStatus
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
        Me.grdItems = New DevExpress.XtraGrid.GridControl
        Me.grvItems = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl
        Me.txtSAPPartNo = New DevExpress.XtraEditors.TextEdit
        Me.txtAssetNo = New DevExpress.XtraEditors.TextEdit
        Me.txtInvNo = New DevExpress.XtraEditors.TextEdit
        Me.txtPOR = New DevExpress.XtraEditors.TextEdit
        Me.txtCostCenter = New DevExpress.XtraEditors.TextEdit
        Me.txtSerialNo = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
        CType(Me.errProv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.valProvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.txtSAPPartNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAssetNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInvNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPOR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCostCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSerialNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdItems
        '
        Me.grdItems.Location = New System.Drawing.Point(7, 93)
        Me.grdItems.MainView = Me.grvItems
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(707, 213)
        Me.grdItems.TabIndex = 7
        Me.grdItems.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvItems})
        '
        'grvItems
        '
        Me.grvItems.GridControl = Me.grdItems
        Me.grvItems.Name = "grvItems"
        Me.grvItems.OptionsView.ShowGroupPanel = False
        Me.grvItems.OptionsView.ShowIndicator = False
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = True
        Me.LayoutControl1.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LayoutControl1.Appearance.DisabledLayoutItem.Options.UseForeColor = True
        Me.LayoutControl1.Controls.Add(Me.txtSAPPartNo)
        Me.LayoutControl1.Controls.Add(Me.txtAssetNo)
        Me.LayoutControl1.Controls.Add(Me.txtInvNo)
        Me.LayoutControl1.Controls.Add(Me.txtPOR)
        Me.LayoutControl1.Controls.Add(Me.txtCostCenter)
        Me.LayoutControl1.Controls.Add(Me.txtSerialNo)
        Me.LayoutControl1.Controls.Add(Me.grdItems)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 55)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(720, 312)
        Me.LayoutControl1.TabIndex = 8
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'txtSAPPartNo
        '
        Me.txtSAPPartNo.Location = New System.Drawing.Point(542, 60)
        Me.txtSAPPartNo.Name = "txtSAPPartNo"
        Me.txtSAPPartNo.Size = New System.Drawing.Size(169, 19)
        Me.txtSAPPartNo.StyleController = Me.LayoutControl1
        Me.txtSAPPartNo.TabIndex = 13
        '
        'txtAssetNo
        '
        Me.txtAssetNo.Location = New System.Drawing.Point(303, 60)
        Me.txtAssetNo.Name = "txtAssetNo"
        Me.txtAssetNo.Size = New System.Drawing.Size(156, 19)
        Me.txtAssetNo.StyleController = Me.LayoutControl1
        Me.txtAssetNo.TabIndex = 12
        '
        'txtInvNo
        '
        Me.txtInvNo.Location = New System.Drawing.Point(82, 60)
        Me.txtInvNo.Name = "txtInvNo"
        Me.txtInvNo.Size = New System.Drawing.Size(138, 19)
        Me.txtInvNo.StyleController = Me.LayoutControl1
        Me.txtInvNo.TabIndex = 11
        '
        'txtPOR
        '
        Me.txtPOR.Location = New System.Drawing.Point(542, 30)
        Me.txtPOR.Name = "txtPOR"
        Me.txtPOR.Size = New System.Drawing.Size(169, 19)
        Me.txtPOR.StyleController = Me.LayoutControl1
        Me.txtPOR.TabIndex = 10
        '
        'txtCostCenter
        '
        Me.txtCostCenter.Location = New System.Drawing.Point(303, 30)
        Me.txtCostCenter.Name = "txtCostCenter"
        Me.txtCostCenter.Size = New System.Drawing.Size(156, 19)
        Me.txtCostCenter.StyleController = Me.LayoutControl1
        Me.txtCostCenter.TabIndex = 9
        '
        'txtSerialNo
        '
        Me.txtSerialNo.Location = New System.Drawing.Point(82, 30)
        Me.txtSerialNo.Name = "txtSerialNo"
        Me.txtSerialNo.Size = New System.Drawing.Size(138, 19)
        Me.txtSerialNo.StyleController = Me.LayoutControl1
        Me.txtSerialNo.TabIndex = 8
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(720, 312)
        Me.LayoutControlGroup1.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.grdItems
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 86)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(718, 224)
        Me.LayoutControlItem1.Text = "LayoutControlItem1"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseTextOptions = True
        Me.LayoutControlGroup2.AppearanceGroup.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LayoutControlGroup2.CustomizationFormText = "Filter Criteria"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem7})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(718, 86)
        Me.LayoutControlGroup2.Text = "Filter Criteria"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.txtSerialNo
        Me.LayoutControlItem2.CustomizationFormText = "Serial No"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(221, 30)
        Me.LayoutControlItem2.Text = "Serial No"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(67, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.txtInvNo
        Me.LayoutControlItem5.CustomizationFormText = "Inv Pro No"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 30)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(221, 30)
        Me.LayoutControlItem5.Text = "Inv Pro No"
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(67, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.txtAssetNo
        Me.LayoutControlItem6.CustomizationFormText = "Asset Number"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(221, 30)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(239, 30)
        Me.LayoutControlItem6.Text = "Asset Number"
        Me.LayoutControlItem6.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(67, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.txtCostCenter
        Me.LayoutControlItem3.CustomizationFormText = "Cost Center"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(221, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(239, 30)
        Me.LayoutControlItem3.Text = "Cost Center"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(67, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.txtPOR
        Me.LayoutControlItem4.CustomizationFormText = "POR No"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(460, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(252, 30)
        Me.LayoutControlItem4.Text = "POR No"
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(67, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.txtSAPPartNo
        Me.LayoutControlItem7.CustomizationFormText = "SAP Part No"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(460, 30)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(252, 30)
        Me.LayoutControlItem7.Text = "SAP Part No"
        Me.LayoutControlItem7.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(67, 13)
        '
        'ctlWarrantyStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "ctlWarrantyStatus"
        Me.Size = New System.Drawing.Size(720, 390)
        Me.Controls.SetChildIndex(Me.LayoutControl1, 0)
        CType(Me.errProv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.valProvMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.txtSAPPartNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAssetNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInvNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPOR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCostCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSerialNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdItems As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvItems As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtSAPPartNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtAssetNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtInvNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPOR As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCostCenter As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtSerialNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem

End Class
