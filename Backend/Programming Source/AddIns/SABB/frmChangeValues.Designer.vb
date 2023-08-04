<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChangeValues
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
        Me.lblCaption = New System.Windows.Forms.Label
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnBlkCancel = New System.Windows.Forms.Button
        Me.cmbBrand = New DevExpress.XtraEditors.GridLookUpEdit
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.lblBrand = New System.Windows.Forms.Label
        Me.cmbAssetsStatus = New DevExpress.XtraEditors.GridLookUpEdit
        Me.GridView7 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.lblStatus = New System.Windows.Forms.Label
        Me.lblCustodian = New System.Windows.Forms.Label
        Me.cmbCustodian = New DevExpress.XtraEditors.GridLookUpEdit
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.cmbLocation = New DevExpress.XtraEditors.GridLookUpEdit
        Me.GridView12 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.lblLocation = New System.Windows.Forms.Label
        CType(Me.cmbBrand.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbAssetsStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCustodian.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbLocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView12, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblCaption
        '
        Me.lblCaption.AutoSize = True
        Me.lblCaption.BackColor = System.Drawing.Color.Transparent
        Me.lblCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCaption.Location = New System.Drawing.Point(12, 9)
        Me.lblCaption.Name = "lblCaption"
        Me.lblCaption.Size = New System.Drawing.Size(16, 13)
        Me.lblCaption.TabIndex = 74
        Me.lblCaption.Text = "..."
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(225, 159)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 73
        Me.btnOK.Text = "&OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnBlkCancel
        '
        Me.btnBlkCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnBlkCancel.Location = New System.Drawing.Point(122, 159)
        Me.btnBlkCancel.Name = "btnBlkCancel"
        Me.btnBlkCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnBlkCancel.TabIndex = 72
        Me.btnBlkCancel.Text = "&Cancel"
        Me.btnBlkCancel.UseVisualStyleBackColor = True
        '
        'cmbBrand
        '
        Me.cmbBrand.Location = New System.Drawing.Point(137, 89)
        Me.cmbBrand.Name = "cmbBrand"
        Me.cmbBrand.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBrand.Properties.NullText = ""
        Me.cmbBrand.Properties.View = Me.GridView3
        Me.cmbBrand.Size = New System.Drawing.Size(217, 19)
        Me.cmbBrand.TabIndex = 82
        '
        'GridView3
        '
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        '
        'lblBrand
        '
        Me.lblBrand.AutoSize = True
        Me.lblBrand.BackColor = System.Drawing.Color.Transparent
        Me.lblBrand.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBrand.Location = New System.Drawing.Point(28, 92)
        Me.lblBrand.Name = "lblBrand"
        Me.lblBrand.Size = New System.Drawing.Size(78, 13)
        Me.lblBrand.TabIndex = 83
        Me.lblBrand.Text = "Select Brand"
        '
        'cmbAssetsStatus
        '
        Me.cmbAssetsStatus.Location = New System.Drawing.Point(137, 116)
        Me.cmbAssetsStatus.Name = "cmbAssetsStatus"
        Me.cmbAssetsStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbAssetsStatus.Properties.NullText = ""
        Me.cmbAssetsStatus.Properties.View = Me.GridView7
        Me.cmbAssetsStatus.Size = New System.Drawing.Size(217, 19)
        Me.cmbAssetsStatus.TabIndex = 84
        '
        'GridView7
        '
        Me.GridView7.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView7.Name = "GridView7"
        Me.GridView7.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView7.OptionsView.ShowAutoFilterRow = True
        Me.GridView7.OptionsView.ShowGroupPanel = False
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(28, 119)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(82, 13)
        Me.lblStatus.TabIndex = 85
        Me.lblStatus.Text = "Select Status"
        '
        'lblCustodian
        '
        Me.lblCustodian.AutoSize = True
        Me.lblCustodian.BackColor = System.Drawing.Color.Transparent
        Me.lblCustodian.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustodian.Location = New System.Drawing.Point(28, 42)
        Me.lblCustodian.Name = "lblCustodian"
        Me.lblCustodian.Size = New System.Drawing.Size(101, 13)
        Me.lblCustodian.TabIndex = 87
        Me.lblCustodian.Text = "Select Custodian"
        '
        'cmbCustodian
        '
        Me.cmbCustodian.Location = New System.Drawing.Point(137, 39)
        Me.cmbCustodian.Name = "cmbCustodian"
        Me.cmbCustodian.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCustodian.Properties.NullText = ""
        Me.cmbCustodian.Properties.View = Me.GridView1
        Me.cmbCustodian.Size = New System.Drawing.Size(217, 19)
        Me.cmbCustodian.TabIndex = 86
        '
        'GridView1
        '
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'cmbLocation
        '
        Me.cmbLocation.Location = New System.Drawing.Point(137, 64)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbLocation.Properties.NullText = ""
        Me.cmbLocation.Properties.View = Me.GridView12
        Me.cmbLocation.Size = New System.Drawing.Size(217, 19)
        Me.cmbLocation.TabIndex = 88
        '
        'GridView12
        '
        Me.GridView12.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView12.Name = "GridView12"
        Me.GridView12.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView12.OptionsView.ShowAutoFilterRow = True
        Me.GridView12.OptionsView.ShowGroupPanel = False
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = True
        Me.lblLocation.BackColor = System.Drawing.Color.Transparent
        Me.lblLocation.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(28, 67)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(93, 13)
        Me.lblLocation.TabIndex = 89
        Me.lblLocation.Text = "Select Location"
        '
        'frmChangeValues
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(408, 194)
        Me.Controls.Add(Me.lblLocation)
        Me.Controls.Add(Me.cmbLocation)
        Me.Controls.Add(Me.lblCustodian)
        Me.Controls.Add(Me.cmbCustodian)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.cmbAssetsStatus)
        Me.Controls.Add(Me.lblBrand)
        Me.Controls.Add(Me.cmbBrand)
        Me.Controls.Add(Me.lblCaption)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnBlkCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmChangeValues"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Values"
        CType(Me.cmbBrand.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbAssetsStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCustodian.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbLocation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView12, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblCaption As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnBlkCancel As System.Windows.Forms.Button
    Friend WithEvents cmbBrand As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblBrand As System.Windows.Forms.Label
    Friend WithEvents cmbAssetsStatus As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblCustodian As System.Windows.Forms.Label
    Friend WithEvents cmbCustodian As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbLocation As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView12 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblLocation As System.Windows.Forms.Label
End Class
