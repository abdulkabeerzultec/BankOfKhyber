<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAssetTransfer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAssetTransfer))
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.cmbCust = New ZulLOV.ZulLOV
        Me.chkCust = New System.Windows.Forms.CheckBox
        Me.btnPreview = New System.Windows.Forms.Button
        Me.btnPrintReport = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnTrans = New System.Windows.Forms.Button
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.cmbLocation = New ZulTree.ZulTree
        Me.chkLoc = New System.Windows.Forms.CheckBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.grd = New DevExpress.XtraGrid.GridControl
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmbAssetStatus = New ZulLOV.ZulLOV
        Me.chkChangeStatus = New System.Windows.Forms.CheckBox
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.White
        Me.GroupBox3.Controls.Add(Me.cmbCust)
        Me.GroupBox3.Controls.Add(Me.chkCust)
        Me.GroupBox3.Location = New System.Drawing.Point(5, 402)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(726, 50)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Select Custdian"
        '
        'cmbCust
        '
        Me.cmbCust.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbCust.BackColor = System.Drawing.Color.White
        Me.cmbCust.DataSource = Nothing
        Me.cmbCust.DisplayMember = ""
        Me.cmbCust.Enabled = False
        Me.cmbCust.Location = New System.Drawing.Point(180, 19)
        Me.cmbCust.Name = "cmbCust"
        Me.cmbCust.SelectedIndex = -1
        Me.cmbCust.SelectedText = ""
        Me.cmbCust.SelectedValue = ""
        Me.cmbCust.Size = New System.Drawing.Size(529, 23)
        Me.cmbCust.TabIndex = 2
        Me.cmbCust.TextReadOnly = False
        Me.cmbCust.ValueMember = ""
        '
        'chkCust
        '
        Me.chkCust.Location = New System.Drawing.Point(18, 19)
        Me.chkCust.Name = "chkCust"
        Me.chkCust.Size = New System.Drawing.Size(170, 23)
        Me.chkCust.TabIndex = 0
        Me.chkCust.Text = "Transfer Asset By Custodian"
        Me.chkCust.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.Image = My.Resources.Preview16x16
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(251, 513)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(124, 27)
        Me.btnPreview.TabIndex = 5
        Me.btnPreview.Text = "&Show Preview"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnPrintReport
        '
        Me.btnPrintReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrintReport.Image = My.Resources.Print
        Me.btnPrintReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrintReport.Location = New System.Drawing.Point(383, 513)
        Me.btnPrintReport.Name = "btnPrintReport"
        Me.btnPrintReport.Size = New System.Drawing.Size(94, 27)
        Me.btnPrintReport.TabIndex = 4
        Me.btnPrintReport.Text = "&Print"
        Me.btnPrintReport.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Image = My.Resources.Close16x16
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(651, 513)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(80, 27)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "&Close"
        '
        'btnTrans
        '
        Me.btnTrans.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTrans.Image = My.Resources.Import
        Me.btnTrans.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTrans.Location = New System.Drawing.Point(490, 513)
        Me.btnTrans.Name = "btnTrans"
        Me.btnTrans.Size = New System.Drawing.Size(122, 27)
        Me.btnTrans.TabIndex = 3
        Me.btnTrans.Text = "&Transfer Asset"
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.BackColor = System.Drawing.Color.White
        Me.GroupBox4.Controls.Add(Me.cmbLocation)
        Me.GroupBox4.Controls.Add(Me.chkLoc)
        Me.GroupBox4.Location = New System.Drawing.Point(5, 347)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(726, 50)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Select Location"
        '
        'cmbLocation
        '
        Me.cmbLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbLocation.BackColor = System.Drawing.Color.White
        Me.cmbLocation.DataSource = Nothing
        Me.cmbLocation.DisplayMember = ""
        Me.cmbLocation.Location = New System.Drawing.Point(180, 19)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.SelectedText = ""
        Me.cmbLocation.SelectedValue = ""
        Me.cmbLocation.Size = New System.Drawing.Size(529, 24)
        Me.cmbLocation.TabIndex = 2
        Me.cmbLocation.TextReadOnly = False
        Me.cmbLocation.ValueMember = ""
        '
        'chkLoc
        '
        Me.chkLoc.Location = New System.Drawing.Point(18, 19)
        Me.chkLoc.Name = "chkLoc"
        Me.chkLoc.Size = New System.Drawing.Size(170, 23)
        Me.chkLoc.TabIndex = 0
        Me.chkLoc.Text = "Transfer Asset By Location"
        Me.chkLoc.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(5, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(402, 69)
        Me.PictureBox1.TabIndex = 37
        Me.PictureBox1.TabStop = False
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.Location = New System.Drawing.Point(3, 80)
        Me.grd.MainView = Me.grv
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(722, 261)
        Me.grd.TabIndex = 38
        Me.grd.UseEmbeddedNavigator = True
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv})
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
        Me.grv.OptionsCustomization.AllowGroup = False
        Me.grv.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv.OptionsView.ShowGroupPanel = False
        Me.grv.OptionsView.ShowIndicator = False
        Me.grv.OptionsView.ShowViewCaption = True
        Me.grv.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
        Me.grv.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        Me.grv.ViewCaption = "Select Assets to Transfer"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.cmbAssetStatus)
        Me.GroupBox1.Controls.Add(Me.chkChangeStatus)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 458)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(726, 50)
        Me.GroupBox1.TabIndex = 39
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Change Status"
        '
        'cmbAssetStatus
        '
        Me.cmbAssetStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbAssetStatus.BackColor = System.Drawing.Color.White
        Me.cmbAssetStatus.DataSource = Nothing
        Me.cmbAssetStatus.DisplayMember = ""
        Me.cmbAssetStatus.Enabled = False
        Me.cmbAssetStatus.Location = New System.Drawing.Point(180, 19)
        Me.cmbAssetStatus.Name = "cmbAssetStatus"
        Me.cmbAssetStatus.SelectedIndex = -1
        Me.cmbAssetStatus.SelectedText = ""
        Me.cmbAssetStatus.SelectedValue = ""
        Me.cmbAssetStatus.Size = New System.Drawing.Size(529, 23)
        Me.cmbAssetStatus.TabIndex = 2
        Me.cmbAssetStatus.TextReadOnly = False
        Me.cmbAssetStatus.ValueMember = ""
        '
        'chkChangeStatus
        '
        Me.chkChangeStatus.Location = New System.Drawing.Point(18, 19)
        Me.chkChangeStatus.Name = "chkChangeStatus"
        Me.chkChangeStatus.Size = New System.Drawing.Size(133, 23)
        Me.chkChangeStatus.TabIndex = 0
        Me.chkChangeStatus.Text = "Change status to "
        Me.chkChangeStatus.UseVisualStyleBackColor = True
        '
        'frmAssetTransfer
        '
        Me.AcceptButton = Me.btnTrans
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(739, 543)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.btnPrintReport)
        Me.Controls.Add(Me.btnTrans)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.MinimumSize = New System.Drawing.Size(650, 500)
        Me.Name = "frmAssetTransfer"
        Me.Text = "Location \ Custody Transfer"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnTrans As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents chkCust As System.Windows.Forms.CheckBox
    Friend WithEvents chkLoc As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cmbCust As ZulLOV.ZulLOV
    Friend WithEvents cmbLocation As ZulTree.ZulTree
    Friend WithEvents btnPrintReport As System.Windows.Forms.Button
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbAssetStatus As ZulLOV.ZulLOV
    Friend WithEvents chkChangeStatus As System.Windows.Forms.CheckBox
End Class
