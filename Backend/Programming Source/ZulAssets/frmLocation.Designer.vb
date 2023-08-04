<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLocation
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLocation))
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuNewLoc = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuNewChildLoc = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuEditLocation = New System.Windows.Forms.ToolStripMenuItem
        Me.DeleteLocationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuCollapse = New System.Windows.Forms.ToolStripMenuItem
        Me.RefeshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.btnDwn = New System.Windows.Forms.Button
        Me.btnUp = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cmbComp = New ZulLOV.ZulLOV
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblCode = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnHidePanel = New System.Windows.Forms.Button
        Me.txtDesc = New DevExpress.XtraEditors.TextEdit
        Me.txtCode = New DevExpress.XtraEditors.TextEdit
        Me.txtID = New DevExpress.XtraEditors.TextEdit
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.trv = New System.Windows.Forms.TreeView
        Me.ContextMenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.txtDesc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNewLoc, Me.mnuNewChildLoc, Me.mnuEditLocation, Me.DeleteLocationToolStripMenuItem, Me.ToolStripSeparator1, Me.mnuCollapse, Me.RefeshToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(185, 142)
        '
        'mnuNewLoc
        '
        Me.mnuNewLoc.Name = "mnuNewLoc"
        Me.mnuNewLoc.Size = New System.Drawing.Size(184, 22)
        Me.mnuNewLoc.Text = "Add New Location "
        '
        'mnuNewChildLoc
        '
        Me.mnuNewChildLoc.Name = "mnuNewChildLoc"
        Me.mnuNewChildLoc.Size = New System.Drawing.Size(184, 22)
        Me.mnuNewChildLoc.Text = "Add New Sub Location "
        Me.mnuNewChildLoc.Visible = False
        '
        'mnuEditLocation
        '
        Me.mnuEditLocation.Name = "mnuEditLocation"
        Me.mnuEditLocation.Size = New System.Drawing.Size(184, 22)
        Me.mnuEditLocation.Text = "Edit Location "
        '
        'DeleteLocationToolStripMenuItem
        '
        Me.DeleteLocationToolStripMenuItem.Name = "DeleteLocationToolStripMenuItem"
        Me.DeleteLocationToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.DeleteLocationToolStripMenuItem.Text = "Delete Location"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(181, 6)
        '
        'mnuCollapse
        '
        Me.mnuCollapse.Name = "mnuCollapse"
        Me.mnuCollapse.Size = New System.Drawing.Size(184, 22)
        Me.mnuCollapse.Text = "Collapse"
        '
        'RefeshToolStripMenuItem
        '
        Me.RefeshToolStripMenuItem.Name = "RefeshToolStripMenuItem"
        Me.RefeshToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.RefeshToolStripMenuItem.Text = "Refesh "
        '
        'btnDwn
        '
        Me.btnDwn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDwn.BackColor = System.Drawing.SystemColors.Control
        Me.btnDwn.Location = New System.Drawing.Point(509, 293)
        Me.btnDwn.Name = "btnDwn"
        Me.btnDwn.Size = New System.Drawing.Size(84, 27)
        Me.btnDwn.TabIndex = 3
        Me.btnDwn.Text = "Move &Down"
        Me.btnDwn.UseVisualStyleBackColor = True
        '
        'btnUp
        '
        Me.btnUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUp.BackColor = System.Drawing.SystemColors.Control
        Me.btnUp.Location = New System.Drawing.Point(509, 259)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(84, 27)
        Me.btnUp.TabIndex = 2
        Me.btnUp.Text = "Move &Up"
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.cmbComp)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.lblCode)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.btnHidePanel)
        Me.Panel1.Controls.Add(Me.txtDesc)
        Me.Panel1.Controls.Add(Me.txtCode)
        Me.Panel1.Controls.Add(Me.txtID)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(66, 259)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(380, 60)
        Me.Panel1.TabIndex = 1
        Me.Panel1.Visible = False
        '
        'cmbComp
        '
        Me.cmbComp.BackColor = System.Drawing.Color.White
        Me.cmbComp.DataSource = Nothing
        Me.cmbComp.DisplayMember = ""
        Me.cmbComp.Location = New System.Drawing.Point(62, 33)
        Me.cmbComp.Name = "cmbComp"
        Me.cmbComp.SelectedIndex = -1
        Me.cmbComp.SelectedText = ""
        Me.cmbComp.SelectedValue = ""
        Me.cmbComp.Size = New System.Drawing.Size(232, 24)
        Me.cmbComp.TabIndex = 58
        Me.cmbComp.TextReadOnly = False
        Me.cmbComp.ValueMember = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(1, 34)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "Company"
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.Location = New System.Drawing.Point(2, 8)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(32, 13)
        Me.lblCode.TabIndex = 0
        Me.lblCode.Text = "Code"
        '
        'btnSave
        '
        Me.btnSave.Image = Global.ZulAssets.My.Resources.Icons.save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(305, 32)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(70, 23)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnHidePanel
        '
        Me.btnHidePanel.BackgroundImage = CType(resources.GetObject("btnHidePanel.BackgroundImage"), System.Drawing.Image)
        Me.btnHidePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnHidePanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHidePanel.Location = New System.Drawing.Point(363, 2)
        Me.btnHidePanel.Name = "btnHidePanel"
        Me.btnHidePanel.Size = New System.Drawing.Size(11, 12)
        Me.btnHidePanel.TabIndex = 3
        Me.btnHidePanel.Text = " "
        Me.btnHidePanel.UseVisualStyleBackColor = True
        '
        'txtDesc
        '
        Me.txtDesc.Location = New System.Drawing.Point(195, 6)
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Properties.MaxLength = 30
        Me.txtDesc.Size = New System.Drawing.Size(99, 19)
        Me.txtDesc.TabIndex = 1
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(62, 5)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Properties.MaxLength = 50
        Me.txtCode.Size = New System.Drawing.Size(61, 19)
        Me.txtCode.TabIndex = 0
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(-1, 2)
        Me.txtID.Name = "txtID"
        Me.txtID.Properties.ReadOnly = True
        Me.txtID.Size = New System.Drawing.Size(10, 19)
        Me.txtID.TabIndex = 3
        Me.txtID.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(129, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Description"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.SystemColors.Control
        Me.btnExit.Image = Global.ZulAssets.My.Resources.Icons.Close
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(509, 522)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(84, 27)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "&Close"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(5, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(402, 70)
        Me.PictureBox1.TabIndex = 24
        Me.PictureBox1.TabStop = False
        '
        'trv
        '
        Me.trv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.trv.ContextMenuStrip = Me.ContextMenuStrip1
        Me.trv.HideSelection = False
        Me.trv.Location = New System.Drawing.Point(5, 81)
        Me.trv.Name = "trv"
        Me.trv.Size = New System.Drawing.Size(493, 468)
        Me.trv.TabIndex = 0
        '
        'frmLocation
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(598, 566)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnDwn)
        Me.Controls.Add(Me.trv)
        Me.DoubleBuffered = True
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(500, 500)
        Me.Name = "frmLocation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Locations"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.txtDesc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnDwn As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnHidePanel As System.Windows.Forms.Button
    Friend WithEvents txtDesc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuNewLoc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewChildLoc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEditLocation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteLocationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RefeshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents trv As System.Windows.Forms.TreeView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents mnuCollapse As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmbComp As ZulLOV.ZulLOV
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
