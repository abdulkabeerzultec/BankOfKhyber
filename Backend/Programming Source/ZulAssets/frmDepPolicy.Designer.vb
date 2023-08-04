<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDepPolicy
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDepPolicy))
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCategory = New DevExpress.XtraEditors.TextEdit
        Me.trv = New System.Windows.Forms.TreeView
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.PanelPercent = New System.Windows.Forms.Panel
        Me.Label51 = New System.Windows.Forms.Label
        Me.txtSalPercent = New DevExpress.XtraEditors.TextEdit
        Me.txtSalVal = New DevExpress.XtraEditors.TextEdit
        Me.PanelYear = New System.Windows.Forms.Panel
        Me.txtSalYr = New DevExpress.XtraEditors.TextEdit
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtSalMonth = New DevExpress.XtraEditors.TextEdit
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnApply = New System.Windows.Forms.Button
        Me.txtSalValPercent = New DevExpress.XtraEditors.TextEdit
        Me.rdoPercentageValue = New System.Windows.Forms.RadioButton
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.rdoFixedValue = New System.Windows.Forms.RadioButton
        CType(Me.txtCategory.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.PanelPercent.SuspendLayout()
        CType(Me.txtSalPercent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalVal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelYear.SuspendLayout()
        CType(Me.txtSalYr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalMonth.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalValPercent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Selected Category"
        '
        'txtCategory
        '
        Me.txtCategory.Location = New System.Drawing.Point(126, 19)
        Me.txtCategory.Name = "txtCategory"
        Me.txtCategory.Properties.MaxLength = 25
        Me.txtCategory.Properties.ReadOnly = True
        Me.txtCategory.Size = New System.Drawing.Size(329, 20)
        Me.txtCategory.TabIndex = 0
        '
        'trv
        '
        Me.trv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.trv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trv.HideSelection = False
        Me.trv.Location = New System.Drawing.Point(3, 16)
        Me.trv.Name = "trv"
        Me.trv.Size = New System.Drawing.Size(333, 419)
        Me.trv.TabIndex = 42
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.White
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Controls.Add(Me.PanelPercent)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtCategory)
        Me.GroupBox2.Controls.Add(Me.PanelYear)
        Me.GroupBox2.Location = New System.Drawing.Point(350, 80)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(470, 209)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Depreciation Details"
        '
        'PanelPercent
        '
        Me.PanelPercent.Controls.Add(Me.Label51)
        Me.PanelPercent.Controls.Add(Me.txtSalPercent)
        Me.PanelPercent.Location = New System.Drawing.Point(9, 77)
        Me.PanelPercent.Name = "PanelPercent"
        Me.PanelPercent.Size = New System.Drawing.Size(224, 32)
        Me.PanelPercent.TabIndex = 2
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.Location = New System.Drawing.Point(5, 11)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(107, 13)
        Me.Label51.TabIndex = 31
        Me.Label51.Text = "Salvage Year (%)"
        '
        'txtSalPercent
        '
        Me.txtSalPercent.Location = New System.Drawing.Point(118, 8)
        Me.txtSalPercent.Name = "txtSalPercent"
        Me.txtSalPercent.Properties.MaxLength = 7
        Me.txtSalPercent.Size = New System.Drawing.Size(103, 20)
        Me.txtSalPercent.TabIndex = 30
        Me.txtSalPercent.ToolTip = "Depreciation percentage per year"
        Me.txtSalPercent.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        Me.txtSalPercent.ToolTipTitle = "Salvage Year (%)"
        '
        'txtSalVal
        '
        Me.txtSalVal.Location = New System.Drawing.Point(159, 19)
        Me.txtSalVal.Name = "txtSalVal"
        Me.txtSalVal.Properties.MaxLength = 4
        Me.txtSalVal.Size = New System.Drawing.Size(103, 20)
        Me.txtSalVal.TabIndex = 3
        Me.txtSalVal.ToolTip = "Minimum value when the asset will be fully depreciated"
        Me.txtSalVal.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        '
        'PanelYear
        '
        Me.PanelYear.Controls.Add(Me.txtSalYr)
        Me.PanelYear.Controls.Add(Me.Label25)
        Me.PanelYear.Controls.Add(Me.txtSalMonth)
        Me.PanelYear.Controls.Add(Me.Label2)
        Me.PanelYear.Location = New System.Drawing.Point(9, 46)
        Me.PanelYear.Name = "PanelYear"
        Me.PanelYear.Size = New System.Drawing.Size(447, 32)
        Me.PanelYear.TabIndex = 1
        '
        'txtSalYr
        '
        Me.txtSalYr.Location = New System.Drawing.Point(118, 6)
        Me.txtSalYr.Name = "txtSalYr"
        Me.txtSalYr.Properties.MaxLength = 2
        Me.txtSalYr.Size = New System.Drawing.Size(103, 20)
        Me.txtSalYr.TabIndex = 0
        Me.txtSalYr.ToolTip = "Number of years to reach the salvage value"
        Me.txtSalYr.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        Me.txtSalYr.ToolTipTitle = "Salvage Year"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(5, 10)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(81, 13)
        Me.Label25.TabIndex = 2
        Me.Label25.Text = "Salvage Year"
        '
        'txtSalMonth
        '
        Me.txtSalMonth.Location = New System.Drawing.Point(343, 6)
        Me.txtSalMonth.Name = "txtSalMonth"
        Me.txtSalMonth.Properties.MaxLength = 2
        Me.txtSalMonth.Size = New System.Drawing.Size(103, 20)
        Me.txtSalMonth.TabIndex = 1
        Me.txtSalMonth.ToolTip = "Number of months to reach the salvage value"
        Me.txtSalMonth.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        Me.txtSalMonth.ToolTipTitle = "Salvage Month"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(227, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 13)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "Salvage Month"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.trv)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 80)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(339, 438)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Categories"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(5, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(402, 69)
        Me.PictureBox1.TabIndex = 46
        Me.PictureBox1.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Image = Global.ZulAssets.My.Resources.Icons.Close
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(734, 488)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(86, 27)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "&Close"
        '
        'btnApply
        '
        Me.btnApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApply.Image = Global.ZulAssets.My.Resources.Icons.Connected
        Me.btnApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnApply.Location = New System.Drawing.Point(589, 488)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(118, 27)
        Me.btnApply.TabIndex = 3
        Me.btnApply.Text = "Apply Policy"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'txtSalvageValuePercentage
        '
        Me.txtSalValPercent.Enabled = False
        Me.txtSalValPercent.Location = New System.Drawing.Point(159, 50)
        Me.txtSalValPercent.Name = "txtSalvageValuePercentage"
        Me.txtSalValPercent.Properties.MaxLength = 4
        Me.txtSalValPercent.Size = New System.Drawing.Size(103, 20)
        Me.txtSalValPercent.TabIndex = 16
        Me.txtSalValPercent.ToolTip = "Set percentage from asset total cost to be the salvage value"
        Me.txtSalValPercent.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        Me.txtSalValPercent.ToolTipTitle = "Salvage Value(%)"
        '
        'rdoPercentageValue
        '
        Me.rdoPercentageValue.AutoSize = True
        Me.rdoPercentageValue.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoPercentageValue.Location = New System.Drawing.Point(6, 51)
        Me.rdoPercentageValue.Name = "rdoPercentageValue"
        Me.rdoPercentageValue.Size = New System.Drawing.Size(153, 17)
        Me.rdoPercentageValue.TabIndex = 17
        Me.rdoPercentageValue.Text = "Percentage from total cost"
        Me.rdoPercentageValue.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rdoFixedValue)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.txtSalVal)
        Me.GroupBox3.Controls.Add(Me.rdoPercentageValue)
        Me.GroupBox3.Controls.Add(Me.txtSalValPercent)
        Me.GroupBox3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupBox3.Location = New System.Drawing.Point(9, 115)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(446, 85)
        Me.GroupBox3.TabIndex = 18
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Salvage Value"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(265, 53)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(20, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "%"
        '
        'rdoFixedValue
        '
        Me.rdoFixedValue.AutoSize = True
        Me.rdoFixedValue.Checked = True
        Me.rdoFixedValue.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoFixedValue.Location = New System.Drawing.Point(6, 22)
        Me.rdoFixedValue.Name = "rdoFixedValue"
        Me.rdoFixedValue.Size = New System.Drawing.Size(80, 17)
        Me.rdoFixedValue.TabIndex = 19
        Me.rdoFixedValue.TabStop = True
        Me.rdoFixedValue.Text = "Fixed Value"
        Me.rdoFixedValue.UseVisualStyleBackColor = True
        '
        'frmDepPolicy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(826, 528)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.MinimumSize = New System.Drawing.Size(600, 400)
        Me.Name = "frmDepPolicy"
        Me.Text = " Depreciation Policy"
        CType(Me.txtCategory.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.PanelPercent.ResumeLayout(False)
        Me.PanelPercent.PerformLayout()
        CType(Me.txtSalPercent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalVal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelYear.ResumeLayout(False)
        Me.PanelYear.PerformLayout()
        CType(Me.txtSalYr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalMonth.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalValPercent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCategory As DevExpress.XtraEditors.TextEdit
    Friend WithEvents trv As System.Windows.Forms.TreeView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSalVal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtSalYr As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents txtSalMonth As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PanelYear As System.Windows.Forms.Panel
    Friend WithEvents PanelPercent As System.Windows.Forms.Panel
    Friend WithEvents txtSalPercent As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents txtSalValPercent As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents rdoPercentageValue As System.Windows.Forms.RadioButton
    Friend WithEvents rdoFixedValue As System.Windows.Forms.RadioButton
End Class
