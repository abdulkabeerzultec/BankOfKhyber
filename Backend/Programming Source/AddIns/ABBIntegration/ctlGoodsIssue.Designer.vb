<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlGoodsIssue
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
        Me.txtRemarks = New DevExpress.XtraEditors.TextEdit
        Me.grdItems = New DevExpress.XtraGrid.GridControl
        Me.grdViewItems = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.cmbEmpNumber = New DevExpress.XtraEditors.GridLookUpEdit
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.dtDocDate = New DevExpress.XtraEditors.DateEdit
        Me.txtEmpName = New DevExpress.XtraEditors.TextEdit
        Me.txtInvP_POR = New DevExpress.XtraEditors.TextEdit
        Me.txtGINo = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutIssueType = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutGridItems = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        CType(Me.errProv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.valProvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.txtRemarks.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdViewItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEmpNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDocDate.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDocDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmpName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInvP_POR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGINo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutIssueType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutGridItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = True
        Me.LayoutControl1.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LayoutControl1.Appearance.DisabledLayoutItem.Options.UseForeColor = True
        Me.LayoutControl1.Controls.Add(Me.txtRemarks)
        Me.LayoutControl1.Controls.Add(Me.grdItems)
        Me.LayoutControl1.Controls.Add(Me.cmbEmpNumber)
        Me.LayoutControl1.Controls.Add(Me.dtDocDate)
        Me.LayoutControl1.Controls.Add(Me.txtEmpName)
        Me.LayoutControl1.Controls.Add(Me.txtInvP_POR)
        Me.LayoutControl1.Controls.Add(Me.txtGINo)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 55)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(727, 421)
        Me.LayoutControl1.TabIndex = 4
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(94, 69)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(627, 20)
        Me.txtRemarks.StyleController = Me.LayoutControl1
        Me.txtRemarks.TabIndex = 15
        '
        'grdItems
        '
        Me.grdItems.Location = New System.Drawing.Point(10, 122)
        Me.grdItems.MainView = Me.grdViewItems
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(708, 290)
        Me.grdItems.TabIndex = 14
        Me.grdItems.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdViewItems})
        '
        'grdViewItems
        '
        Me.grdViewItems.GridControl = Me.grdItems
        Me.grdViewItems.Name = "grdViewItems"
        Me.grdViewItems.OptionsView.ShowGroupPanel = False
        Me.grdViewItems.OptionsView.ShowIndicator = False
        '
        'cmbEmpNumber
        '
        Me.cmbEmpNumber.Location = New System.Drawing.Point(94, 38)
        Me.cmbEmpNumber.Name = "cmbEmpNumber"
        Me.cmbEmpNumber.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEmpNumber.Properties.NullText = ""
        Me.cmbEmpNumber.Properties.View = Me.GridLookUpEdit1View
        Me.cmbEmpNumber.Size = New System.Drawing.Size(100, 20)
        Me.cmbEmpNumber.StyleController = Me.LayoutControl1
        Me.cmbEmpNumber.TabIndex = 13
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'dtDocDate
        '
        Me.dtDocDate.EditValue = Nothing
        Me.dtDocDate.Location = New System.Drawing.Point(457, 38)
        Me.dtDocDate.Name = "dtDocDate"
        Me.dtDocDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtDocDate.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.dtDocDate.Size = New System.Drawing.Size(264, 20)
        Me.dtDocDate.StyleController = Me.LayoutControl1
        Me.dtDocDate.TabIndex = 9
        '
        'txtEmpName
        '
        Me.txtEmpName.Location = New System.Drawing.Point(205, 38)
        Me.txtEmpName.Name = "txtEmpName"
        Me.txtEmpName.Size = New System.Drawing.Size(154, 20)
        Me.txtEmpName.StyleController = Me.LayoutControl1
        Me.txtEmpName.TabIndex = 6
        '
        'txtInvP_POR
        '
        Me.txtInvP_POR.Location = New System.Drawing.Point(457, 7)
        Me.txtInvP_POR.Name = "txtInvP_POR"
        Me.txtInvP_POR.Size = New System.Drawing.Size(264, 20)
        Me.txtInvP_POR.StyleController = Me.LayoutControl1
        Me.txtInvP_POR.TabIndex = 5
        '
        'txtGINo
        '
        Me.txtGINo.Location = New System.Drawing.Point(94, 7)
        Me.txtGINo.Name = "txtGINo"
        Me.txtGINo.Size = New System.Drawing.Size(265, 20)
        Me.txtGINo.StyleController = Me.LayoutControl1
        Me.txtGINo.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem3, Me.LayoutIssueType, Me.LayoutGridItems, Me.LayoutControlItem9, Me.LayoutControlItem2, Me.LayoutControlItem5})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(727, 421)
        Me.LayoutControlGroup1.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.txtGINo
        Me.LayoutControlItem1.CustomizationFormText = "Code"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(363, 31)
        Me.LayoutControlItem1.Text = "GI Number"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(82, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.txtEmpName
        Me.LayoutControlItem3.CustomizationFormText = "Ar Name"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(198, 31)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(165, 31)
        Me.LayoutControlItem3.Text = "Employee"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutIssueType
        '
        Me.LayoutIssueType.Control = Me.txtInvP_POR
        Me.LayoutIssueType.CustomizationFormText = "En Name"
        Me.LayoutIssueType.Location = New System.Drawing.Point(363, 0)
        Me.LayoutIssueType.Name = "LayoutIssueType"
        Me.LayoutIssueType.Size = New System.Drawing.Size(362, 31)
        Me.LayoutIssueType.Text = "Investment Pro#"
        Me.LayoutIssueType.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutIssueType.TextSize = New System.Drawing.Size(82, 13)
        '
        'LayoutGridItems
        '
        Me.LayoutGridItems.AppearanceGroup.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutGridItems.AppearanceGroup.Options.UseFont = True
        Me.LayoutGridItems.AppearanceGroup.Options.UseTextOptions = True
        Me.LayoutGridItems.AppearanceGroup.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LayoutGridItems.CustomizationFormText = "Items Price"
        Me.LayoutGridItems.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4})
        Me.LayoutGridItems.Location = New System.Drawing.Point(0, 93)
        Me.LayoutGridItems.Name = "LayoutGridItems"
        Me.LayoutGridItems.Size = New System.Drawing.Size(725, 326)
        Me.LayoutGridItems.Text = "Goods Issue Items "
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.grdItems
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(719, 301)
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
        Me.LayoutControlItem9.Location = New System.Drawing.Point(363, 31)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(362, 31)
        Me.LayoutControlItem9.Text = "Issue Date"
        Me.LayoutControlItem9.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(82, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.cmbEmpNumber
        Me.LayoutControlItem2.CustomizationFormText = "Employee"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 31)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(198, 31)
        Me.LayoutControlItem2.Text = "Employee"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(82, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.txtRemarks
        Me.LayoutControlItem5.CustomizationFormText = "Remarks"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 62)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(725, 31)
        Me.LayoutControlItem5.Text = "Remarks"
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(82, 13)
        '
        'ctlGoodsIssue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "ctlGoodsIssue"
        Me.Size = New System.Drawing.Size(727, 501)
        Me.Controls.SetChildIndex(Me.LayoutControl1, 0)
        CType(Me.errProv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.valProvMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.txtRemarks.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdViewItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEmpNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDocDate.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDocDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmpName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInvP_POR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGINo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutIssueType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutGridItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents txtEmpName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtInvP_POR As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtGINo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutIssueType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutGridItems As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents dtDocDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cmbEmpNumber As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdItems As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdViewItems As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtRemarks As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem

End Class
