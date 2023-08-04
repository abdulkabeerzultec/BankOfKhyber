<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBackendInventory
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
        Me.btnPrintPreview = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblStatus = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtBarcode = New DevExpress.XtraEditors.TextEdit
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbLocation = New ZulTree.ZulTree
        Me.cmbSch = New ZulLOV.ZulLOV
        Me.lblInventory = New System.Windows.Forms.Label
        Me.grpAssets = New System.Windows.Forms.GroupBox
        Me.grdAssets = New DevExpress.XtraGrid.GridControl
        Me.grvAssets = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.pb = New System.Windows.Forms.ProgressBar
        Me.btnClose = New System.Windows.Forms.Button
        Me.imgInv = New System.Windows.Forms.PictureBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtBarcode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAssets.SuspendLayout()
        CType(Me.grdAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgInv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnPrintPreview
        '
        Me.btnPrintPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrintPreview.Image = Global.ZulAssets.My.Resources.Icons.Preview
        Me.btnPrintPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrintPreview.Location = New System.Drawing.Point(9, 454)
        Me.btnPrintPreview.Name = "btnPrintPreview"
        Me.btnPrintPreview.Size = New System.Drawing.Size(113, 27)
        Me.btnPrintPreview.TabIndex = 64
        Me.btnPrintPreview.Text = "&Print Preview"
        Me.btnPrintPreview.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.imgInv)
        Me.GroupBox1.Controls.Add(Me.lblStatus)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtBarcode)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbLocation)
        Me.GroupBox1.Controls.Add(Me.cmbSch)
        Me.GroupBox1.Controls.Add(Me.lblInventory)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(686, 139)
        Me.GroupBox1.TabIndex = 61
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Inventory Schedule and location and start scanning"
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(140, 108)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(538, 25)
        Me.lblStatus.TabIndex = 65
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Maroon
        Me.Label5.Location = New System.Drawing.Point(480, 49)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(132, 11)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "2. Select Location from the list."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Maroon
        Me.Label4.Location = New System.Drawing.Point(480, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(181, 11)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "1. Select Inventory Schedule from the list."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Maroon
        Me.Label3.Location = New System.Drawing.Point(333, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(326, 11)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "3. Put cursor here then start scanning barcodes and press enter."
        '
        'txtBarcode
        '
        Me.txtBarcode.Location = New System.Drawing.Point(140, 78)
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.Size = New System.Drawing.Size(187, 19)
        Me.txtBarcode.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Asset Barcode"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Location"
        '
        'cmbLocation
        '
        Me.cmbLocation.BackColor = System.Drawing.Color.White
        Me.cmbLocation.DataSource = Nothing
        Me.cmbLocation.DisplayMember = ""
        Me.cmbLocation.Location = New System.Drawing.Point(140, 45)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.SelectedText = ""
        Me.cmbLocation.SelectedValue = ""
        Me.cmbLocation.Size = New System.Drawing.Size(334, 23)
        Me.cmbLocation.TabIndex = 6
        Me.cmbLocation.TextReadOnly = False
        Me.cmbLocation.ValueMember = ""
        '
        'cmbSch
        '
        Me.cmbSch.BackColor = System.Drawing.Color.White
        Me.cmbSch.DataSource = Nothing
        Me.cmbSch.DisplayMember = ""
        Me.cmbSch.Location = New System.Drawing.Point(140, 16)
        Me.cmbSch.Name = "cmbSch"
        Me.cmbSch.SelectedIndex = -1
        Me.cmbSch.SelectedText = ""
        Me.cmbSch.SelectedValue = ""
        Me.cmbSch.Size = New System.Drawing.Size(334, 23)
        Me.cmbSch.TabIndex = 0
        Me.cmbSch.TextReadOnly = False
        Me.cmbSch.ValueMember = ""
        '
        'lblInventory
        '
        Me.lblInventory.AutoSize = True
        Me.lblInventory.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInventory.Location = New System.Drawing.Point(7, 20)
        Me.lblInventory.Name = "lblInventory"
        Me.lblInventory.Size = New System.Drawing.Size(118, 13)
        Me.lblInventory.TabIndex = 0
        Me.lblInventory.Text = "Inventory Schedule"
        '
        'grpAssets
        '
        Me.grpAssets.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpAssets.BackColor = System.Drawing.Color.White
        Me.grpAssets.Controls.Add(Me.grdAssets)
        Me.grpAssets.Controls.Add(Me.pb)
        Me.grpAssets.Location = New System.Drawing.Point(9, 157)
        Me.grpAssets.Name = "grpAssets"
        Me.grpAssets.Size = New System.Drawing.Size(686, 294)
        Me.grpAssets.TabIndex = 62
        Me.grpAssets.TabStop = False
        Me.grpAssets.Text = "Location Assets"
        '
        'grdAssets
        '
        Me.grdAssets.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAssets.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.grdAssets.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.grdAssets.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.grdAssets.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.grdAssets.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.grdAssets.Location = New System.Drawing.Point(6, 17)
        Me.grdAssets.MainView = Me.grvAssets
        Me.grdAssets.Name = "grdAssets"
        Me.grdAssets.Size = New System.Drawing.Size(674, 255)
        Me.grdAssets.TabIndex = 53
        Me.grdAssets.UseEmbeddedNavigator = True
        Me.grdAssets.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvAssets})
        '
        'grvAssets
        '
        Me.grvAssets.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grvAssets.Appearance.FooterPanel.Options.UseFont = True
        Me.grvAssets.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grvAssets.Appearance.HeaderPanel.Options.UseFont = True
        Me.grvAssets.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvAssets.GridControl = Me.grdAssets
        Me.grvAssets.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.grvAssets.Name = "grvAssets"
        Me.grvAssets.OptionsBehavior.Editable = False
        Me.grvAssets.OptionsCustomization.AllowGroup = False
        Me.grvAssets.OptionsNavigation.UseTabKey = False
        Me.grvAssets.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvAssets.OptionsView.ShowAutoFilterRow = True
        Me.grvAssets.OptionsView.ShowGroupPanel = False
        Me.grvAssets.OptionsView.ShowIndicator = False
        Me.grvAssets.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
        Me.grvAssets.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'pb
        '
        Me.pb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pb.Location = New System.Drawing.Point(6, 275)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(674, 13)
        Me.pb.TabIndex = 52
        Me.pb.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Image = Global.ZulAssets.My.Resources.Icons.Close
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(610, 454)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(79, 27)
        Me.btnClose.TabIndex = 60
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'imgInv
        '
        Me.imgInv.Location = New System.Drawing.Point(121, 108)
        Me.imgInv.Name = "imgInv"
        Me.imgInv.Size = New System.Drawing.Size(16, 25)
        Me.imgInv.TabIndex = 66
        Me.imgInv.TabStop = False
        '
        'frmBackendInventory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(697, 483)
        Me.Controls.Add(Me.btnPrintPreview)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpAssets)
        Me.Controls.Add(Me.btnClose)
        Me.Name = "frmBackendInventory"
        Me.Text = "Backend Inventory"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtBarcode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpAssets.ResumeLayout(False)
        CType(Me.grdAssets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvAssets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgInv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPrintPreview As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbSch As ZulLOV.ZulLOV
    Friend WithEvents lblInventory As System.Windows.Forms.Label
    Friend WithEvents grpAssets As System.Windows.Forms.GroupBox
    Friend WithEvents grdAssets As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvAssets As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbLocation As ZulTree.ZulTree
    Friend WithEvents txtBarcode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents imgInv As System.Windows.Forms.PictureBox
End Class
