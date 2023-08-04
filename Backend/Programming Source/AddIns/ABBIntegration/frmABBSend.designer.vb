<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmABBSend
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmABBSend))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.grd = New DevExpress.XtraGrid.GridControl
        Me.grdView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.btnSend = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.grpAssetFilter = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbLocation = New DevExpress.XtraEditors.CheckedComboBoxEdit
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbClass = New DevExpress.XtraEditors.CheckedComboBoxEdit
        Me.cmbBusinessArea = New DevExpress.XtraEditors.CheckedComboBoxEdit
        Me.chkCat = New System.Windows.Forms.CheckBox
        Me.chkLoc = New System.Windows.Forms.CheckBox
        Me.chkUsers = New System.Windows.Forms.CheckBox
        Me.chkAsset = New System.Windows.Forms.CheckBox
        Me.fsw = New System.IO.FileSystemWatcher
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.updTimer = New System.Windows.Forms.Timer(Me.components)
        Me.btnExit = New System.Windows.Forms.Button
        Me.cmbEvGroup = New DevExpress.XtraEditors.CheckedComboBoxEdit
        Me.GroupBox2.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.grpAssetFilter.SuspendLayout()
        CType(Me.cmbLocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbClass.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBusinessArea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fsw, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEvGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.White
        Me.GroupBox2.Controls.Add(Me.grd)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 205)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(652, 261)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Devices"
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.Location = New System.Drawing.Point(6, 20)
        Me.grd.MainView = Me.grdView
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(640, 235)
        Me.grd.TabIndex = 2
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdView})
        '
        'grdView
        '
        Me.grdView.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdView.Appearance.FooterPanel.Options.UseFont = True
        Me.grdView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdView.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grdView.GridControl = Me.grd
        Me.grdView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.grdView.Name = "grdView"
        Me.grdView.OptionsBehavior.Editable = False
        Me.grdView.OptionsCustomization.AllowGroup = False
        Me.grdView.OptionsNavigation.UseTabKey = False
        Me.grdView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdView.OptionsView.ShowAutoFilterRow = True
        Me.grdView.OptionsView.ShowGroupPanel = False
        Me.grdView.OptionsView.ShowIndicator = False
        Me.grdView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
        Me.grdView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'btnSend
        '
        Me.btnSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSend.Image = Global.ABBIntegration.My.Resources.Resources.check
        Me.btnSend.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSend.Location = New System.Drawing.Point(468, 472)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(80, 28)
        Me.btnSend.TabIndex = 4
        Me.btnSend.Text = "&Send"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.grpAssetFilter)
        Me.GroupBox1.Controls.Add(Me.chkCat)
        Me.GroupBox1.Controls.Add(Me.chkLoc)
        Me.GroupBox1.Controls.Add(Me.chkUsers)
        Me.GroupBox1.Controls.Add(Me.chkAsset)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 79)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(652, 120)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Data Transmission"
        '
        'grpAssetFilter
        '
        Me.grpAssetFilter.Controls.Add(Me.cmbEvGroup)
        Me.grpAssetFilter.Controls.Add(Me.Label4)
        Me.grpAssetFilter.Controls.Add(Me.cmbLocation)
        Me.grpAssetFilter.Controls.Add(Me.Label3)
        Me.grpAssetFilter.Controls.Add(Me.Label2)
        Me.grpAssetFilter.Controls.Add(Me.Label1)
        Me.grpAssetFilter.Controls.Add(Me.cmbClass)
        Me.grpAssetFilter.Controls.Add(Me.cmbBusinessArea)
        Me.grpAssetFilter.Location = New System.Drawing.Point(103, 15)
        Me.grpAssetFilter.Name = "grpAssetFilter"
        Me.grpAssetFilter.Size = New System.Drawing.Size(529, 72)
        Me.grpAssetFilter.TabIndex = 45
        Me.grpAssetFilter.TabStop = False
        Me.grpAssetFilter.Text = "Assets Filter(Keep empty for all)"
        Me.grpAssetFilter.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(305, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 50
        Me.Label4.Text = "EV G1"
        '
        'cmbLocation
        '
        Me.cmbLocation.Location = New System.Drawing.Point(93, 20)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbLocation.Size = New System.Drawing.Size(192, 19)
        Me.cmbLocation.TabIndex = 49
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(308, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 13)
        Me.Label3.TabIndex = 48
        Me.Label3.Text = "Class"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 47
        Me.Label2.Text = "Business Area"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "Location"
        '
        'cmbClass
        '
        Me.cmbClass.Location = New System.Drawing.Point(346, 20)
        Me.cmbClass.Name = "cmbClass"
        Me.cmbClass.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbClass.Size = New System.Drawing.Size(177, 19)
        Me.cmbClass.TabIndex = 45
        '
        'cmbBusinessArea
        '
        Me.cmbBusinessArea.Location = New System.Drawing.Point(93, 47)
        Me.cmbBusinessArea.Name = "cmbBusinessArea"
        Me.cmbBusinessArea.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBusinessArea.Size = New System.Drawing.Size(192, 19)
        Me.cmbBusinessArea.TabIndex = 44
        '
        'chkCat
        '
        Me.chkCat.AutoSize = True
        Me.chkCat.Location = New System.Drawing.Point(207, 97)
        Me.chkCat.Name = "chkCat"
        Me.chkCat.Size = New System.Drawing.Size(78, 17)
        Me.chkCat.TabIndex = 4
        Me.chkCat.Text = "Categories"
        Me.chkCat.UseVisualStyleBackColor = True
        Me.chkCat.Visible = False
        '
        'chkLoc
        '
        Me.chkLoc.AutoSize = True
        Me.chkLoc.Location = New System.Drawing.Point(103, 97)
        Me.chkLoc.Name = "chkLoc"
        Me.chkLoc.Size = New System.Drawing.Size(71, 17)
        Me.chkLoc.TabIndex = 3
        Me.chkLoc.Text = "Locations"
        Me.chkLoc.UseVisualStyleBackColor = True
        Me.chkLoc.Visible = False
        '
        'chkUsers
        '
        Me.chkUsers.AutoSize = True
        Me.chkUsers.Location = New System.Drawing.Point(15, 97)
        Me.chkUsers.Name = "chkUsers"
        Me.chkUsers.Size = New System.Drawing.Size(53, 17)
        Me.chkUsers.TabIndex = 2
        Me.chkUsers.Text = "Users"
        Me.chkUsers.UseVisualStyleBackColor = True
        Me.chkUsers.Visible = False
        '
        'chkAsset
        '
        Me.chkAsset.AutoSize = True
        Me.chkAsset.Location = New System.Drawing.Point(15, 37)
        Me.chkAsset.Name = "chkAsset"
        Me.chkAsset.Size = New System.Drawing.Size(53, 17)
        Me.chkAsset.TabIndex = 1
        Me.chkAsset.Text = "Asset"
        Me.chkAsset.UseVisualStyleBackColor = True
        '
        'fsw
        '
        Me.fsw.EnableRaisingEvents = True
        Me.fsw.IncludeSubdirectories = True
        Me.fsw.SynchronizingObject = Me
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(5, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(402, 70)
        Me.PictureBox1.TabIndex = 49
        Me.PictureBox1.TabStop = False
        '
        'updTimer
        '
        Me.updTimer.Interval = 5
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Image = Global.ABBIntegration.My.Resources.Resources.Close16x16
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(578, 472)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(80, 28)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "&Close"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'cmbEvGroup
        '
        Me.cmbEvGroup.Location = New System.Drawing.Point(346, 47)
        Me.cmbEvGroup.Name = "cmbEvGroup"
        Me.cmbEvGroup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEvGroup.Size = New System.Drawing.Size(177, 19)
        Me.cmbEvGroup.TabIndex = 51
        '
        'frmABBSend
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(663, 508)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.DoubleBuffered = True
        Me.Name = "frmABBSend"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Send Data to Mobile Device"
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpAssetFilter.ResumeLayout(False)
        Me.grpAssetFilter.PerformLayout()
        CType(Me.cmbLocation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbClass.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBusinessArea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fsw, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEvGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkAsset As System.Windows.Forms.CheckBox
    Friend WithEvents chkCat As System.Windows.Forms.CheckBox
    Friend WithEvents chkLoc As System.Windows.Forms.CheckBox
    Friend WithEvents chkUsers As System.Windows.Forms.CheckBox
    Friend WithEvents fsw As System.IO.FileSystemWatcher
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents updTimer As System.Windows.Forms.Timer
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grpAssetFilter As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbClass As DevExpress.XtraEditors.CheckedComboBoxEdit
    Friend WithEvents cmbBusinessArea As DevExpress.XtraEditors.CheckedComboBoxEdit
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbLocation As DevExpress.XtraEditors.CheckedComboBoxEdit
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbEvGroup As DevExpress.XtraEditors.CheckedComboBoxEdit
End Class
