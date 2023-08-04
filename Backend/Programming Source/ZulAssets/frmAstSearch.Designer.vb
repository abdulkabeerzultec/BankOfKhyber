<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAstSearch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAstSearch))
        Me.Groupbox1 = New System.Windows.Forms.GroupBox
        Me.itmCode = New ZulLOV.ZulLOV
        Me.btnLOV = New System.Windows.Forms.Button
        Me.cmbHier = New DevExpress.XtraEditors.TextEdit
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.chkSubLevels = New System.Windows.Forms.CheckBox
        Me.txtDesc = New DevExpress.XtraEditors.TextEdit
        Me.ZTCategory = New ZulTree.ZulTree
        Me.Label4 = New System.Windows.Forms.Label
        Me.ZTLocation = New ZulTree.ZulTree
        Me.txtAstNum = New DevExpress.XtraEditors.TextEdit
        Me.AssetsID = New ZulLOV.ZulLOV
        Me.cmbCust = New ZulLOV.ZulLOV
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtBrandID = New ZulLOV.ZulLOV
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.btnRef = New System.Windows.Forms.Button
        Me.Label12 = New System.Windows.Forms.Label
        Me.btnSearch = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnPrv = New System.Windows.Forms.Button
        Me.grd = New DevExpress.XtraGrid.GridControl
        Me.grdView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.pb = New System.Windows.Forms.ProgressBar
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtSerialNumber = New DevExpress.XtraEditors.TextEdit
        Me.Groupbox1.SuspendLayout()
        CType(Me.cmbHier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAstNum.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSerialNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Groupbox1
        '
        Me.Groupbox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Groupbox1.BackColor = System.Drawing.Color.White
        Me.Groupbox1.Controls.Add(Me.Label5)
        Me.Groupbox1.Controls.Add(Me.txtSerialNumber)
        Me.Groupbox1.Controls.Add(Me.itmCode)
        Me.Groupbox1.Controls.Add(Me.btnLOV)
        Me.Groupbox1.Controls.Add(Me.cmbHier)
        Me.Groupbox1.Controls.Add(Me.Label15)
        Me.Groupbox1.Controls.Add(Me.Label3)
        Me.Groupbox1.Controls.Add(Me.Label2)
        Me.Groupbox1.Controls.Add(Me.chkSubLevels)
        Me.Groupbox1.Controls.Add(Me.txtDesc)
        Me.Groupbox1.Controls.Add(Me.ZTCategory)
        Me.Groupbox1.Controls.Add(Me.Label4)
        Me.Groupbox1.Controls.Add(Me.ZTLocation)
        Me.Groupbox1.Controls.Add(Me.txtAstNum)
        Me.Groupbox1.Controls.Add(Me.AssetsID)
        Me.Groupbox1.Controls.Add(Me.cmbCust)
        Me.Groupbox1.Controls.Add(Me.Label1)
        Me.Groupbox1.Controls.Add(Me.txtBrandID)
        Me.Groupbox1.Controls.Add(Me.Label6)
        Me.Groupbox1.Controls.Add(Me.Label19)
        Me.Groupbox1.Controls.Add(Me.btnRef)
        Me.Groupbox1.Controls.Add(Me.Label12)
        Me.Groupbox1.Controls.Add(Me.btnSearch)
        Me.Groupbox1.Controls.Add(Me.Label8)
        Me.Groupbox1.Location = New System.Drawing.Point(5, 76)
        Me.Groupbox1.Name = "Groupbox1"
        Me.Groupbox1.Size = New System.Drawing.Size(669, 240)
        Me.Groupbox1.TabIndex = 0
        Me.Groupbox1.TabStop = False
        Me.Groupbox1.Text = "Asset Search Criteria"
        '
        'itmCode
        '
        Me.itmCode.BackColor = System.Drawing.Color.White
        Me.itmCode.DataSource = Nothing
        Me.itmCode.DisplayMember = ""
        Me.itmCode.Location = New System.Drawing.Point(75, 46)
        Me.itmCode.Name = "itmCode"
        Me.itmCode.SelectedIndex = -1
        Me.itmCode.SelectedText = ""
        Me.itmCode.SelectedValue = ""
        Me.itmCode.Size = New System.Drawing.Size(214, 23)
        Me.itmCode.TabIndex = 65
        Me.itmCode.TextReadOnly = False
        Me.itmCode.ValueMember = ""
        '
        'btnLOV
        '
        Me.btnLOV.Image = CType(resources.GetObject("btnLOV.Image"), System.Drawing.Image)
        Me.btnLOV.Location = New System.Drawing.Point(572, 128)
        Me.btnLOV.MaximumSize = New System.Drawing.Size(24, 25)
        Me.btnLOV.Name = "btnLOV"
        Me.btnLOV.Size = New System.Drawing.Size(24, 24)
        Me.btnLOV.TabIndex = 7
        '
        'cmbHier
        '
        Me.cmbHier.Location = New System.Drawing.Point(75, 130)
        Me.cmbHier.Name = "cmbHier"
        Me.cmbHier.Properties.MaxLength = 50
        Me.cmbHier.Properties.ReadOnly = True
        Me.cmbHier.Size = New System.Drawing.Size(495, 20)
        Me.cmbHier.TabIndex = 6
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(11, 19)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(47, 13)
        Me.Label15.TabIndex = 50
        Me.Label15.Text = "Asset ID"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 50
        Me.Label3.Text = "Item Code"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 107)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Description"
        '
        'chkSubLevels
        '
        Me.chkSubLevels.AutoSize = True
        Me.chkSubLevels.Location = New System.Drawing.Point(75, 217)
        Me.chkSubLevels.Name = "chkSubLevels"
        Me.chkSubLevels.Size = New System.Drawing.Size(243, 17)
        Me.chkSubLevels.TabIndex = 10
        Me.chkSubLevels.Text = "Include Sub  Levels of Locations && Categories"
        Me.chkSubLevels.UseVisualStyleBackColor = True
        '
        'txtDesc
        '
        Me.txtDesc.Location = New System.Drawing.Point(75, 104)
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Properties.MaxLength = 12
        Me.txtDesc.Size = New System.Drawing.Size(521, 20)
        Me.txtDesc.TabIndex = 3
        '
        'ZTCategory
        '
        Me.ZTCategory.BackColor = System.Drawing.Color.White
        Me.ZTCategory.DataSource = Nothing
        Me.ZTCategory.DisplayMember = ""
        Me.ZTCategory.Location = New System.Drawing.Point(75, 181)
        Me.ZTCategory.Name = "ZTCategory"
        Me.ZTCategory.SelectedText = ""
        Me.ZTCategory.SelectedValue = ""
        Me.ZTCategory.Size = New System.Drawing.Size(521, 23)
        Me.ZTCategory.TabIndex = 9
        Me.ZTCategory.TextReadOnly = False
        Me.ZTCategory.ValueMember = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label4.Location = New System.Drawing.Point(323, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 51
        Me.Label4.Text = "Asset Number"
        '
        'ZTLocation
        '
        Me.ZTLocation.BackColor = System.Drawing.Color.White
        Me.ZTLocation.DataSource = Nothing
        Me.ZTLocation.DisplayMember = ""
        Me.ZTLocation.Location = New System.Drawing.Point(75, 155)
        Me.ZTLocation.Name = "ZTLocation"
        Me.ZTLocation.SelectedText = ""
        Me.ZTLocation.SelectedValue = ""
        Me.ZTLocation.Size = New System.Drawing.Size(521, 23)
        Me.ZTLocation.TabIndex = 8
        Me.ZTLocation.TextReadOnly = False
        Me.ZTLocation.ValueMember = ""
        '
        'txtAstNum
        '
        Me.txtAstNum.Location = New System.Drawing.Point(402, 21)
        Me.txtAstNum.Name = "txtAstNum"
        Me.txtAstNum.Properties.MaxLength = 12
        Me.txtAstNum.Size = New System.Drawing.Size(167, 20)
        Me.txtAstNum.TabIndex = 1
        '
        'AssetsID
        '
        Me.AssetsID.BackColor = System.Drawing.Color.White
        Me.AssetsID.DataSource = Nothing
        Me.AssetsID.DisplayMember = ""
        Me.AssetsID.Location = New System.Drawing.Point(75, 19)
        Me.AssetsID.Name = "AssetsID"
        Me.AssetsID.SelectedIndex = -1
        Me.AssetsID.SelectedText = ""
        Me.AssetsID.SelectedValue = ""
        Me.AssetsID.Size = New System.Drawing.Size(214, 23)
        Me.AssetsID.TabIndex = 0
        Me.AssetsID.TextReadOnly = False
        Me.AssetsID.ValueMember = ""
        '
        'cmbCust
        '
        Me.cmbCust.BackColor = System.Drawing.Color.White
        Me.cmbCust.DataSource = Nothing
        Me.cmbCust.DisplayMember = ""
        Me.cmbCust.Location = New System.Drawing.Point(402, 74)
        Me.cmbCust.Name = "cmbCust"
        Me.cmbCust.SelectedIndex = -1
        Me.cmbCust.SelectedText = ""
        Me.cmbCust.SelectedValue = ""
        Me.cmbCust.Size = New System.Drawing.Size(194, 23)
        Me.cmbCust.TabIndex = 5
        Me.cmbCust.TextReadOnly = False
        Me.cmbCust.ValueMember = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 186)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Category"
        '
        'txtBrandID
        '
        Me.txtBrandID.BackColor = System.Drawing.Color.White
        Me.txtBrandID.DataSource = Nothing
        Me.txtBrandID.DisplayMember = ""
        Me.txtBrandID.Location = New System.Drawing.Point(75, 73)
        Me.txtBrandID.Name = "txtBrandID"
        Me.txtBrandID.SelectedIndex = -1
        Me.txtBrandID.SelectedText = ""
        Me.txtBrandID.SelectedValue = ""
        Me.txtBrandID.Size = New System.Drawing.Size(214, 23)
        Me.txtBrandID.TabIndex = 4
        Me.txtBrandID.TextReadOnly = False
        Me.txtBrandID.ValueMember = ""
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(11, 133)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 13)
        Me.Label6.TabIndex = 59
        Me.Label6.Text = "Hierarchy"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(11, 160)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(48, 13)
        Me.Label19.TabIndex = 59
        Me.Label19.Text = "Location"
        '
        'btnRef
        '
        Me.btnRef.Image = Global.ZulAssets.My.Resources.Icons.Refresh
        Me.btnRef.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRef.Location = New System.Drawing.Point(335, 211)
        Me.btnRef.Name = "btnRef"
        Me.btnRef.Size = New System.Drawing.Size(115, 27)
        Me.btnRef.TabIndex = 11
        Me.btnRef.Text = "&Refresh Form"
        Me.btnRef.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label12.Location = New System.Drawing.Point(323, 77)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(54, 13)
        Me.Label12.TabIndex = 61
        Me.Label12.Text = "Custodian"
        '
        'btnSearch
        '
        Me.btnSearch.Image = Global.ZulAssets.My.Resources.Icons.Find
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(481, 211)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(115, 27)
        Me.btnSearch.TabIndex = 12
        Me.btnSearch.Text = "&Search Assets"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(11, 79)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(35, 13)
        Me.Label8.TabIndex = 64
        Me.Label8.Text = "Brand"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.InitialImage = CType(resources.GetObject("PictureBox1.InitialImage"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(5, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(305, 69)
        Me.PictureBox1.TabIndex = 40
        Me.PictureBox1.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Image = Global.ZulAssets.My.Resources.Icons.Close
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(559, 541)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(115, 27)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "&Close"
        '
        'btnPrv
        '
        Me.btnPrv.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrv.Image = Global.ZulAssets.My.Resources.Icons.Preview
        Me.btnPrv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrv.Location = New System.Drawing.Point(428, 541)
        Me.btnPrv.Name = "btnPrv"
        Me.btnPrv.Size = New System.Drawing.Size(115, 27)
        Me.btnPrv.TabIndex = 2
        Me.btnPrv.Text = "&Preview"
        Me.btnPrv.Visible = False
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.Location = New System.Drawing.Point(5, 322)
        Me.grd.MainView = Me.grdView
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(669, 200)
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
        Me.pb.Location = New System.Drawing.Point(5, 523)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(669, 12)
        Me.pb.TabIndex = 60
        Me.pb.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label5.Location = New System.Drawing.Point(323, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(73, 13)
        Me.Label5.TabIndex = 67
        Me.Label5.Text = "Serial Number"
        '
        'txtSerialNumber
        '
        Me.txtSerialNumber.Location = New System.Drawing.Point(402, 49)
        Me.txtSerialNumber.Name = "txtSerialNumber"
        Me.txtSerialNumber.Properties.MaxLength = 12
        Me.txtSerialNumber.Size = New System.Drawing.Size(167, 20)
        Me.txtSerialNumber.TabIndex = 66
        '
        'frmAstSearch
        '
        Me.AcceptButton = Me.btnSearch
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(686, 578)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.pb)
        Me.Controls.Add(Me.btnPrv)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Groupbox1)
        Me.DoubleBuffered = True
        Me.MinimumSize = New System.Drawing.Size(630, 600)
        Me.Name = "frmAstSearch"
        Me.Text = "Asset Search"
        Me.Groupbox1.ResumeLayout(False)
        Me.Groupbox1.PerformLayout()
        CType(Me.cmbHier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAstNum.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSerialNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Groupbox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtDesc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnRef As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents cmbCust As ZulLOV.ZulLOV
    Friend WithEvents txtBrandID As ZulLOV.ZulLOV
    Friend WithEvents AssetsID As ZulLOV.ZulLOV
    Friend WithEvents txtAstNum As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnPrv As System.Windows.Forms.Button
    Friend WithEvents ZTLocation As ZulTree.ZulTree
    Friend WithEvents ZTCategory As ZulTree.ZulTree
    Friend WithEvents chkSubLevels As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnLOV As System.Windows.Forms.Button
    Friend WithEvents cmbHier As DevExpress.XtraEditors.TextEdit
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents itmCode As ZulLOV.ZulLOV
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSerialNumber As DevExpress.XtraEditors.TextEdit

End Class
