﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDesignation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDesignation))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnNew = New System.Windows.Forms.Button
        Me.txtDisgCode = New DevExpress.XtraEditors.TextEdit
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtDisgDesc = New DevExpress.XtraEditors.TextEdit
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.btnDelete = New System.Windows.Forms.Button
        Me.grd = New DevExpress.XtraGrid.GridControl
        Me.grdView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.pb = New System.Windows.Forms.ProgressBar
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtDisgCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDisgDesc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.btnNew)
        Me.GroupBox1.Controls.Add(Me.txtDisgCode)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtDisgDesc)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 78)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(606, 66)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(228, 22)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(41, 22)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = "&New"
        '
        'txtDisgCode
        '
        Me.txtDisgCode.Location = New System.Drawing.Point(105, 23)
        Me.txtDisgCode.Name = "txtDisgCode"
        Me.txtDisgCode.Properties.ReadOnly = True
        Me.txtDisgCode.Size = New System.Drawing.Size(117, 19)
        Me.txtDisgCode.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Designation Code"
        '
        'txtDisgDesc
        '
        Me.txtDisgDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDisgDesc.Location = New System.Drawing.Point(399, 23)
        Me.txtDisgDesc.Name = "txtDisgDesc"
        Me.txtDisgDesc.Properties.MaxLength = 50
        Me.txtDisgDesc.Size = New System.Drawing.Size(191, 19)
        Me.txtDisgDesc.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(284, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Designation Name"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Image = Global.ZulAssets.My.Resources.Icons.save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(429, 392)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(84, 27)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "&Save"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Image = Global.ZulAssets.My.Resources.Icons.Close
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(525, 392)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(84, 27)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "&Close"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(5, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(402, 69)
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Image = Global.ZulAssets.My.Resources.Icons.delete
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(334, 392)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 27)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.Visible = False
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.Location = New System.Drawing.Point(3, 150)
        Me.grd.MainView = Me.grdView
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(606, 222)
        Me.grd.TabIndex = 1
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
        'pb
        '
        Me.pb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pb.Location = New System.Drawing.Point(3, 374)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(606, 13)
        Me.pb.TabIndex = 39
        Me.pb.Visible = False
        '
        'frmDesig
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(612, 431)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.pb)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.GroupBox1)
        Me.DoubleBuffered = True
        Me.MinimumSize = New System.Drawing.Size(556, 342)
        Me.Name = "frmDesig"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Designation (s)"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtDisgCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDisgDesc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents txtDisgCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDisgDesc As DevExpress.XtraEditors.TextEdit
End Class
