<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport
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
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.pb = New DevExpress.XtraEditors.ProgressBarControl
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnImport = New System.Windows.Forms.Button
        Me.grd = New DevExpress.XtraGrid.GridControl
        Me.grdView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.lblTotal = New System.Windows.Forms.Label
        Me.lblValid = New System.Windows.Forms.Label
        Me.lblInvalid = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblExistIgnore = New System.Windows.Forms.Label
        Me.lblExist = New System.Windows.Forms.Label
        Me.btnSelectAll = New System.Windows.Forms.Button
        Me.lblMessage = New System.Windows.Forms.Label
        CType(Me.pb.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'pb
        '
        Me.pb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pb.Location = New System.Drawing.Point(0, 337)
        Me.pb.Name = "pb"
        Me.pb.Properties.ShowTitle = True
        Me.pb.Size = New System.Drawing.Size(726, 20)
        Me.pb.TabIndex = 49
        Me.pb.Visible = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Image = Global.MovenpickPlugin.My.Resources.Resources.Close16x16
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(642, 403)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(80, 27)
        Me.btnExit.TabIndex = 48
        Me.btnExit.Text = "&Close"
        '
        'btnImport
        '
        Me.btnImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImport.Image = Global.MovenpickPlugin.My.Resources.Resources.Import
        Me.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImport.Location = New System.Drawing.Point(529, 403)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(94, 27)
        Me.btnImport.TabIndex = 47
        Me.btnImport.Text = "&Import"
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.Location = New System.Drawing.Point(0, 0)
        Me.grd.MainView = Me.grdView
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(726, 338)
        Me.grd.TabIndex = 50
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdView})
        '
        'grdView
        '
        Me.grdView.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdView.Appearance.FooterPanel.Options.UseFont = True
        Me.grdView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdView.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        StyleFormatCondition1.Appearance.BackColor = System.Drawing.Color.OrangeRed
        StyleFormatCondition1.Appearance.Options.UseBackColor = True
        Me.grdView.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.grdView.GridControl = Me.grd
        Me.grdView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.grdView.Name = "grdView"
        Me.grdView.OptionsBehavior.Editable = False
        Me.grdView.OptionsNavigation.UseTabKey = False
        Me.grdView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdView.OptionsView.ColumnAutoWidth = False
        Me.grdView.OptionsView.EnableAppearanceEvenRow = True
        Me.grdView.OptionsView.EnableAppearanceOddRow = True
        Me.grdView.OptionsView.ShowAutoFilterRow = True
        Me.grdView.OptionsView.ShowGroupPanel = False
        Me.grdView.OptionsView.ShowIndicator = False
        Me.grdView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(6, 16)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(14, 13)
        Me.lblTotal.TabIndex = 51
        Me.lblTotal.Text = "0"
        '
        'lblValid
        '
        Me.lblValid.AutoSize = True
        Me.lblValid.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblValid.ForeColor = System.Drawing.Color.Green
        Me.lblValid.Location = New System.Drawing.Point(121, 16)
        Me.lblValid.Name = "lblValid"
        Me.lblValid.Size = New System.Drawing.Size(14, 13)
        Me.lblValid.TabIndex = 52
        Me.lblValid.Text = "0"
        '
        'lblInvalid
        '
        Me.lblInvalid.AutoSize = True
        Me.lblInvalid.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvalid.ForeColor = System.Drawing.Color.Red
        Me.lblInvalid.Location = New System.Drawing.Point(231, 16)
        Me.lblInvalid.Name = "lblInvalid"
        Me.lblInvalid.Size = New System.Drawing.Size(14, 13)
        Me.lblInvalid.TabIndex = 53
        Me.lblInvalid.Text = "0"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblExistIgnore)
        Me.GroupBox1.Controls.Add(Me.lblExist)
        Me.GroupBox1.Controls.Add(Me.lblValid)
        Me.GroupBox1.Controls.Add(Me.lblTotal)
        Me.GroupBox1.Controls.Add(Me.lblInvalid)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 362)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(375, 68)
        Me.GroupBox1.TabIndex = 54
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Summary Info"
        '
        'lblExistIgnore
        '
        Me.lblExistIgnore.AutoSize = True
        Me.lblExistIgnore.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExistIgnore.ForeColor = System.Drawing.Color.Silver
        Me.lblExistIgnore.Location = New System.Drawing.Point(121, 48)
        Me.lblExistIgnore.Name = "lblExistIgnore"
        Me.lblExistIgnore.Size = New System.Drawing.Size(14, 13)
        Me.lblExistIgnore.TabIndex = 55
        Me.lblExistIgnore.Text = "0"
        Me.lblExistIgnore.Visible = False
        '
        'lblExist
        '
        Me.lblExist.AutoSize = True
        Me.lblExist.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExist.ForeColor = System.Drawing.Color.Blue
        Me.lblExist.Location = New System.Drawing.Point(6, 48)
        Me.lblExist.Name = "lblExist"
        Me.lblExist.Size = New System.Drawing.Size(14, 13)
        Me.lblExist.TabIndex = 54
        Me.lblExist.Text = "0"
        Me.lblExist.Visible = False
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelectAll.Image = Global.MovenpickPlugin.My.Resources.Resources.check
        Me.btnSelectAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSelectAll.Location = New System.Drawing.Point(415, 403)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(94, 27)
        Me.btnSelectAll.TabIndex = 55
        Me.btnSelectAll.Text = "&Select All"
        '
        'lblMessage
        '
        Me.lblMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMessage.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Location = New System.Drawing.Point(415, 370)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(299, 30)
        Me.lblMessage.TabIndex = 56
        Me.lblMessage.Text = "Label1"
        Me.lblMessage.Visible = False
        '
        'frmImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(726, 435)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.btnSelectAll)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.pb)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnImport)
        Me.Name = "frmImport"
        Me.Text = "Import Data From Tab Delimited File"
        CType(Me.pb.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents pb As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents lblValid As System.Windows.Forms.Label
    Friend WithEvents lblInvalid As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents lblExist As System.Windows.Forms.Label
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents lblExistIgnore As System.Windows.Forms.Label
End Class
