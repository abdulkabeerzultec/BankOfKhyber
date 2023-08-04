<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCategory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCategory))
        Me.btnExit = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.btnDwn = New System.Windows.Forms.Button
        Me.btnUp = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblCode = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnHidePanel = New System.Windows.Forms.Button
        Me.txtDesc = New DevExpress.XtraEditors.TextEdit
        Me.txtCode = New DevExpress.XtraEditors.TextEdit
        Me.txtID = New DevExpress.XtraEditors.TextEdit
        Me.Label3 = New System.Windows.Forms.Label
        Me.trv = New System.Windows.Forms.TreeView
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuNewCategory = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuAddNewChild = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuEditCat = New System.Windows.Forms.ToolStripMenuItem
        Me.DeleteLocationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuCollapse = New System.Windows.Forms.ToolStripMenuItem
        Me.RefeshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.txtDesc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Image = Global.ZulAssets.My.Resources.Icons.Close
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(506, 529)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(84, 27)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "&Close"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(5, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(402, 70)
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'btnDwn
        '
        Me.btnDwn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDwn.Enabled = False
        Me.btnDwn.Location = New System.Drawing.Point(506, 294)
        Me.btnDwn.Name = "btnDwn"
        Me.btnDwn.Size = New System.Drawing.Size(84, 27)
        Me.btnDwn.TabIndex = 24
        Me.btnDwn.Text = "Move &Down"
        Me.btnDwn.UseVisualStyleBackColor = True
        '
        'btnUp
        '
        Me.btnUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUp.Enabled = False
        Me.btnUp.Location = New System.Drawing.Point(506, 252)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(84, 27)
        Me.btnUp.TabIndex = 23
        Me.btnUp.Text = "Move &Up"
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.lblCode)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.btnHidePanel)
        Me.Panel1.Controls.Add(Me.txtDesc)
        Me.Panel1.Controls.Add(Me.txtCode)
        Me.Panel1.Controls.Add(Me.txtID)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(59, 274)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(372, 34)
        Me.Panel1.TabIndex = 25
        Me.Panel1.Visible = False
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.Location = New System.Drawing.Point(3, 9)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(32, 13)
        Me.lblCode.TabIndex = 0
        Me.lblCode.Text = "Code"
        '
        'btnSave
        '
        Me.btnSave.Image = Global.ZulAssets.My.Resources.Icons.save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(282, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 23)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnHidePanel
        '
        Me.btnHidePanel.BackgroundImage = CType(resources.GetObject("btnHidePanel.BackgroundImage"), System.Drawing.Image)
        Me.btnHidePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnHidePanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHidePanel.Location = New System.Drawing.Point(357, 2)
        Me.btnHidePanel.Name = "btnHidePanel"
        Me.btnHidePanel.Size = New System.Drawing.Size(11, 12)
        Me.btnHidePanel.TabIndex = 4
        Me.btnHidePanel.Text = " "
        Me.btnHidePanel.UseVisualStyleBackColor = True
        '
        'txtDesc
        '
        Me.txtDesc.Location = New System.Drawing.Point(174, 5)
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Properties.MaxLength = 30
        Me.txtDesc.Size = New System.Drawing.Size(92, 19)
        Me.txtDesc.TabIndex = 2
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(41, 5)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Properties.MaxLength = 50
        Me.txtCode.Size = New System.Drawing.Size(61, 19)
        Me.txtCode.TabIndex = 1
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(-1, 2)
        Me.txtID.Name = "txtID"
        Me.txtID.Properties.ReadOnly = True
        Me.txtID.Size = New System.Drawing.Size(10, 19)
        Me.txtID.TabIndex = 1
        Me.txtID.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(108, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Description"
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
        Me.trv.Size = New System.Drawing.Size(491, 475)
        Me.trv.TabIndex = 22
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNewCategory, Me.mnuAddNewChild, Me.mnuEditCat, Me.DeleteLocationToolStripMenuItem, Me.ToolStripSeparator1, Me.mnuCollapse, Me.RefeshToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(187, 142)
        '
        'mnuNewCategory
        '
        Me.mnuNewCategory.Name = "mnuNewCategory"
        Me.mnuNewCategory.Size = New System.Drawing.Size(186, 22)
        Me.mnuNewCategory.Text = "Add New Category"
        '
        'mnuAddNewChild
        '
        Me.mnuAddNewChild.Name = "mnuAddNewChild"
        Me.mnuAddNewChild.Size = New System.Drawing.Size(186, 22)
        Me.mnuAddNewChild.Text = "Add New Sub Category"
        Me.mnuAddNewChild.Visible = False
        '
        'mnuEditCat
        '
        Me.mnuEditCat.Name = "mnuEditCat"
        Me.mnuEditCat.Size = New System.Drawing.Size(186, 22)
        Me.mnuEditCat.Text = "Edit Category"
        '
        'DeleteLocationToolStripMenuItem
        '
        Me.DeleteLocationToolStripMenuItem.Name = "DeleteLocationToolStripMenuItem"
        Me.DeleteLocationToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.DeleteLocationToolStripMenuItem.Text = "Delete Category"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(183, 6)
        '
        'mnuCollapse
        '
        Me.mnuCollapse.Name = "mnuCollapse"
        Me.mnuCollapse.Size = New System.Drawing.Size(186, 22)
        Me.mnuCollapse.Text = "Collapse"
        '
        'RefeshToolStripMenuItem
        '
        Me.RefeshToolStripMenuItem.Name = "RefeshToolStripMenuItem"
        Me.RefeshToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.RefeshToolStripMenuItem.Text = "Referesh "
        '
        'frmCategory
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(598, 568)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnDwn)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.trv)
        Me.DoubleBuffered = True
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(500, 500)
        Me.Name = "frmCategory"
        Me.Text = "Asset Categories"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.txtDesc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnHidePanel As System.Windows.Forms.Button
    Friend WithEvents txtDesc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnDwn As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents trv As System.Windows.Forms.TreeView
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuNewCategory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAddNewChild As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEditCat As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteLocationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RefeshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCollapse As System.Windows.Forms.ToolStripMenuItem
End Class
