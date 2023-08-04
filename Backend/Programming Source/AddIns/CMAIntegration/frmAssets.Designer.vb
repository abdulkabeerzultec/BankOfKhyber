<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAssets
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAssets))
        Me.ZTCategory = New ZulTree.ZulTree
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtAstDesc = New DevExpress.XtraEditors.TextEdit
        Me.ZTLocation = New ZulTree.ZulTree
        Me.Label31 = New System.Windows.Forms.Label
        Me.AssetsID = New ZulLOV.ZulLOV
        Me.btnNewAsset1 = New System.Windows.Forms.Button
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.pnlNavication = New System.Windows.Forms.Panel
        Me.btnLast = New System.Windows.Forms.Button
        Me.btnFirst = New System.Windows.Forms.Button
        Me.btnNext = New System.Windows.Forms.Button
        Me.btnPre = New System.Windows.Forms.Button
        Me.txtSerialNo = New DevExpress.XtraEditors.TextEdit
        Me.Sear = New System.Windows.Forms.Label
        Me.Label43 = New System.Windows.Forms.Label
        Me.txtAstModel = New DevExpress.XtraEditors.TextEdit
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnNewAsset = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.grdAstHist = New DevExpress.XtraGrid.GridControl
        Me.grdAstHistView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.lblEvaluation = New System.Windows.Forms.Label
        Me.txtBarCode = New DevExpress.XtraEditors.TextEdit
        Me.Label49 = New System.Windows.Forms.Label
        Me.txtAstNum = New DevExpress.XtraEditors.TextEdit
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.grpDisposalInfo = New System.Windows.Forms.GroupBox
        Me.chkDisposed = New System.Windows.Forms.CheckBox
        Me.dtDispdate = New System.Windows.Forms.DateTimePicker
        Me.cmbDisposalMethod = New ZulLOV.ZulLOV
        Me.lblDisposalDate = New System.Windows.Forms.Label
        Me.lblDisposalMethod = New System.Windows.Forms.Label
        Me.txtCreateDate = New DevExpress.XtraEditors.TextEdit
        Me.txtUpdateDate = New DevExpress.XtraEditors.TextEdit
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtCreatedIn = New DevExpress.XtraEditors.TextEdit
        Me.Label3 = New System.Windows.Forms.Label
        Me.pnlStatus = New DevExpress.XtraEditors.PanelControl
        Me.lblStatus = New System.Windows.Forms.Label
        Me.imgStatus = New System.Windows.Forms.PictureBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.btnMarkAsLost = New System.Windows.Forms.Button
        CType(Me.txtAstDesc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlNavication.SuspendLayout()
        CType(Me.txtSerialNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAstModel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdAstHist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAstHistView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBarCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAstNum.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.grpDisposalInfo.SuspendLayout()
        CType(Me.txtCreateDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUpdateDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCreatedIn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlStatus.SuspendLayout()
        CType(Me.imgStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ZTCategory
        '
        Me.ZTCategory.BackColor = System.Drawing.Color.White
        Me.ZTCategory.DataSource = Nothing
        Me.ZTCategory.DisplayMember = ""
        Me.ZTCategory.Location = New System.Drawing.Point(80, 19)
        Me.ZTCategory.Name = "ZTCategory"
        Me.ZTCategory.SelectedText = ""
        Me.ZTCategory.SelectedValue = ""
        Me.ZTCategory.Size = New System.Drawing.Size(428, 24)
        Me.ZTCategory.TabIndex = 1
        Me.ZTCategory.TextReadOnly = False
        Me.ZTCategory.ValueMember = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Category"
        '
        'txtAstDesc
        '
        Me.txtAstDesc.Location = New System.Drawing.Point(80, 136)
        Me.txtAstDesc.Name = "txtAstDesc"
        Me.txtAstDesc.Properties.MaxLength = 80
        Me.txtAstDesc.Size = New System.Drawing.Size(428, 19)
        Me.txtAstDesc.TabIndex = 6
        '
        'ZTLocation
        '
        Me.ZTLocation.BackColor = System.Drawing.Color.White
        Me.ZTLocation.DataSource = Nothing
        Me.ZTLocation.DisplayMember = ""
        Me.ZTLocation.Location = New System.Drawing.Point(80, 57)
        Me.ZTLocation.Name = "ZTLocation"
        Me.ZTLocation.SelectedText = ""
        Me.ZTLocation.SelectedValue = ""
        Me.ZTLocation.Size = New System.Drawing.Size(428, 24)
        Me.ZTLocation.TabIndex = 2
        Me.ZTLocation.TextReadOnly = False
        Me.ZTLocation.ValueMember = ""
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(514, 175)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(75, 13)
        Me.Label31.TabIndex = 64
        Me.Label31.Text = "Create Date"
        '
        'AssetsID
        '
        Me.AssetsID.BackColor = System.Drawing.Color.White
        Me.AssetsID.DataSource = Nothing
        Me.AssetsID.DisplayMember = ""
        Me.AssetsID.Location = New System.Drawing.Point(80, 95)
        Me.AssetsID.Name = "AssetsID"
        Me.AssetsID.SelectedIndex = -1
        Me.AssetsID.SelectedText = ""
        Me.AssetsID.SelectedValue = ""
        Me.AssetsID.Size = New System.Drawing.Size(184, 23)
        Me.AssetsID.TabIndex = 3
        Me.AssetsID.TextReadOnly = False
        Me.AssetsID.ValueMember = ""
        '
        'btnNewAsset1
        '
        Me.btnNewAsset1.Location = New System.Drawing.Point(270, 96)
        Me.btnNewAsset1.Name = "btnNewAsset1"
        Me.btnNewAsset1.Size = New System.Drawing.Size(40, 22)
        Me.btnNewAsset1.TabIndex = 4
        Me.btnNewAsset1.Text = "&New"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label15.Location = New System.Drawing.Point(10, 100)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(64, 13)
        Me.Label15.TabIndex = 56
        Me.Label15.Text = "Asset Tag"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(10, 63)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(55, 13)
        Me.Label19.TabIndex = 59
        Me.Label19.Text = "Location"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(10, 139)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(60, 13)
        Me.Label44.TabIndex = 65
        Me.Label44.Text = "Description"
        '
        'pnlNavication
        '
        Me.pnlNavication.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlNavication.BackColor = System.Drawing.Color.Transparent
        Me.pnlNavication.Controls.Add(Me.btnLast)
        Me.pnlNavication.Controls.Add(Me.btnFirst)
        Me.pnlNavication.Controls.Add(Me.btnNext)
        Me.pnlNavication.Controls.Add(Me.btnPre)
        Me.pnlNavication.Location = New System.Drawing.Point(538, 12)
        Me.pnlNavication.Name = "pnlNavication"
        Me.pnlNavication.Size = New System.Drawing.Size(171, 31)
        Me.pnlNavication.TabIndex = 10
        '
        'btnLast
        '
        Me.btnLast.Image = Global.CMAIntegration.My.Resources.Resources.BtnLast
        Me.btnLast.Location = New System.Drawing.Point(124, 3)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(35, 25)
        Me.btnLast.TabIndex = 5
        Me.btnLast.UseVisualStyleBackColor = True
        '
        'btnFirst
        '
        Me.btnFirst.Image = Global.CMAIntegration.My.Resources.Resources.BtnFirst
        Me.btnFirst.Location = New System.Drawing.Point(10, 3)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(35, 25)
        Me.btnFirst.TabIndex = 2
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Image = Global.CMAIntegration.My.Resources.Resources.BtnNext
        Me.btnNext.Location = New System.Drawing.Point(86, 3)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(35, 25)
        Me.btnNext.TabIndex = 4
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnPre
        '
        Me.btnPre.Image = Global.CMAIntegration.My.Resources.Resources.BtnPrev
        Me.btnPre.Location = New System.Drawing.Point(48, 3)
        Me.btnPre.Name = "btnPre"
        Me.btnPre.Size = New System.Drawing.Size(35, 25)
        Me.btnPre.TabIndex = 3
        Me.btnPre.UseVisualStyleBackColor = True
        '
        'txtSerialNo
        '
        Me.txtSerialNo.Location = New System.Drawing.Point(377, 172)
        Me.txtSerialNo.Name = "txtSerialNo"
        Me.txtSerialNo.Properties.MaxLength = 35
        Me.txtSerialNo.Size = New System.Drawing.Size(131, 19)
        Me.txtSerialNo.TabIndex = 8
        '
        'Sear
        '
        Me.Sear.AutoSize = True
        Me.Sear.Location = New System.Drawing.Point(290, 175)
        Me.Sear.Name = "Sear"
        Me.Sear.Size = New System.Drawing.Size(73, 13)
        Me.Sear.TabIndex = 75
        Me.Sear.Text = "Serial Number"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(10, 175)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(35, 13)
        Me.Label43.TabIndex = 74
        Me.Label43.Text = "Model"
        '
        'txtAstModel
        '
        Me.txtAstModel.Location = New System.Drawing.Point(80, 172)
        Me.txtAstModel.Name = "txtAstModel"
        Me.txtAstModel.Properties.MaxLength = 40
        Me.txtAstModel.Size = New System.Drawing.Size(143, 19)
        Me.txtAstModel.TabIndex = 7
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Image = Global.CMAIntegration.My.Resources.Resources.Delete16x16
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(404, 547)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(82, 31)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "&Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Image = Global.CMAIntegration.My.Resources.Resources.Save16x16
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(510, 547)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(87, 31)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "&Save"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Image = Global.CMAIntegration.My.Resources.Resources.Close16x16
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(630, 547)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(91, 31)
        Me.btnExit.TabIndex = 6
        Me.btnExit.Text = "&Close"
        '
        'btnNewAsset
        '
        Me.btnNewAsset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNewAsset.Image = Global.CMAIntegration.My.Resources.Resources.New16x16
        Me.btnNewAsset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNewAsset.Location = New System.Drawing.Point(14, 547)
        Me.btnNewAsset.Name = "btnNewAsset"
        Me.btnNewAsset.Size = New System.Drawing.Size(107, 31)
        Me.btnNewAsset.TabIndex = 3
        Me.btnNewAsset.Text = "&New Asset"
        Me.btnNewAsset.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.grdAstHist)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 338)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(716, 203)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Tracking History"
        '
        'grdAstHist
        '
        Me.grdAstHist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAstHist.Location = New System.Drawing.Point(6, 19)
        Me.grdAstHist.MainView = Me.grdAstHistView
        Me.grdAstHist.Name = "grdAstHist"
        Me.grdAstHist.Size = New System.Drawing.Size(703, 178)
        Me.grdAstHist.TabIndex = 15
        Me.grdAstHist.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdAstHistView})
        '
        'grdAstHistView
        '
        Me.grdAstHistView.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAstHistView.Appearance.FooterPanel.Options.UseFont = True
        Me.grdAstHistView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAstHistView.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdAstHistView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grdAstHistView.GridControl = Me.grdAstHist
        Me.grdAstHistView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.grdAstHistView.Name = "grdAstHistView"
        Me.grdAstHistView.OptionsBehavior.Editable = False
        Me.grdAstHistView.OptionsCustomization.AllowGroup = False
        Me.grdAstHistView.OptionsNavigation.UseTabKey = False
        Me.grdAstHistView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdAstHistView.OptionsView.ShowAutoFilterRow = True
        Me.grdAstHistView.OptionsView.ShowGroupPanel = False
        Me.grdAstHistView.OptionsView.ShowIndicator = False
        Me.grdAstHistView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
        Me.grdAstHistView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'lblEvaluation
        '
        Me.lblEvaluation.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEvaluation.AutoSize = True
        Me.lblEvaluation.BackColor = System.Drawing.Color.Transparent
        Me.lblEvaluation.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEvaluation.ForeColor = System.Drawing.Color.Red
        Me.lblEvaluation.Location = New System.Drawing.Point(539, 24)
        Me.lblEvaluation.Name = "lblEvaluation"
        Me.lblEvaluation.Size = New System.Drawing.Size(58, 16)
        Me.lblEvaluation.TabIndex = 83
        Me.lblEvaluation.Text = "Label70"
        Me.lblEvaluation.Visible = False
        '
        'txtBarCode
        '
        Me.txtBarCode.Location = New System.Drawing.Point(381, 97)
        Me.txtBarCode.Name = "txtBarCode"
        Me.txtBarCode.Properties.ReadOnly = True
        Me.txtBarCode.Size = New System.Drawing.Size(127, 19)
        Me.txtBarCode.TabIndex = 5
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.Location = New System.Drawing.Point(327, 100)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(48, 13)
        Me.Label49.TabIndex = 85
        Me.Label49.Text = "BarCode"
        '
        'txtAstNum
        '
        Me.txtAstNum.EditValue = ""
        Me.txtAstNum.Location = New System.Drawing.Point(382, 117)
        Me.txtAstNum.Name = "txtAstNum"
        Me.txtAstNum.Properties.MaxLength = 12
        Me.txtAstNum.Properties.ReadOnly = True
        Me.txtAstNum.Size = New System.Drawing.Size(110, 19)
        Me.txtAstNum.TabIndex = 86
        Me.txtAstNum.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.White
        Me.GroupBox2.Controls.Add(Me.grpDisposalInfo)
        Me.GroupBox2.Controls.Add(Me.txtCreateDate)
        Me.GroupBox2.Controls.Add(Me.txtUpdateDate)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtCreatedIn)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.pnlStatus)
        Me.GroupBox2.Controls.Add(Me.txtAstNum)
        Me.GroupBox2.Controls.Add(Me.ZTCategory)
        Me.GroupBox2.Controls.Add(Me.Label49)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtBarCode)
        Me.GroupBox2.Controls.Add(Me.Label44)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.btnNewAsset1)
        Me.GroupBox2.Controls.Add(Me.AssetsID)
        Me.GroupBox2.Controls.Add(Me.Label31)
        Me.GroupBox2.Controls.Add(Me.ZTLocation)
        Me.GroupBox2.Controls.Add(Me.txtSerialNo)
        Me.GroupBox2.Controls.Add(Me.Sear)
        Me.GroupBox2.Controls.Add(Me.txtAstDesc)
        Me.GroupBox2.Controls.Add(Me.Label43)
        Me.GroupBox2.Controls.Add(Me.pnlNavication)
        Me.GroupBox2.Controls.Add(Me.txtAstModel)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 71)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(716, 261)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Asset Information"
        '
        'grpDisposalInfo
        '
        Me.grpDisposalInfo.Controls.Add(Me.chkDisposed)
        Me.grpDisposalInfo.Controls.Add(Me.dtDispdate)
        Me.grpDisposalInfo.Controls.Add(Me.cmbDisposalMethod)
        Me.grpDisposalInfo.Controls.Add(Me.lblDisposalDate)
        Me.grpDisposalInfo.Controls.Add(Me.lblDisposalMethod)
        Me.grpDisposalInfo.Location = New System.Drawing.Point(13, 197)
        Me.grpDisposalInfo.Name = "grpDisposalInfo"
        Me.grpDisposalInfo.Size = New System.Drawing.Size(695, 58)
        Me.grpDisposalInfo.TabIndex = 100
        Me.grpDisposalInfo.TabStop = False
        Me.grpDisposalInfo.Text = "Disposal Information"
        '
        'chkDisposed
        '
        Me.chkDisposed.AutoSize = True
        Me.chkDisposed.Location = New System.Drawing.Point(141, 23)
        Me.chkDisposed.Name = "chkDisposed"
        Me.chkDisposed.Size = New System.Drawing.Size(69, 17)
        Me.chkDisposed.TabIndex = 95
        Me.chkDisposed.Text = "Disposed"
        Me.chkDisposed.UseVisualStyleBackColor = True
        '
        'dtDispdate
        '
        Me.dtDispdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtDispdate.Location = New System.Drawing.Point(585, 19)
        Me.dtDispdate.Name = "dtDispdate"
        Me.dtDispdate.Size = New System.Drawing.Size(104, 20)
        Me.dtDispdate.TabIndex = 97
        '
        'cmbDisposalMethod
        '
        Me.cmbDisposalMethod.BackColor = System.Drawing.Color.White
        Me.cmbDisposalMethod.DataSource = Nothing
        Me.cmbDisposalMethod.DisplayMember = ""
        Me.cmbDisposalMethod.Location = New System.Drawing.Point(364, 16)
        Me.cmbDisposalMethod.Name = "cmbDisposalMethod"
        Me.cmbDisposalMethod.SelectedIndex = -1
        Me.cmbDisposalMethod.SelectedText = ""
        Me.cmbDisposalMethod.SelectedValue = ""
        Me.cmbDisposalMethod.Size = New System.Drawing.Size(131, 24)
        Me.cmbDisposalMethod.TabIndex = 96
        Me.cmbDisposalMethod.TextReadOnly = False
        Me.cmbDisposalMethod.ValueMember = ""
        '
        'lblDisposalDate
        '
        Me.lblDisposalDate.AutoSize = True
        Me.lblDisposalDate.Location = New System.Drawing.Point(501, 23)
        Me.lblDisposalDate.Name = "lblDisposalDate"
        Me.lblDisposalDate.Size = New System.Drawing.Size(72, 13)
        Me.lblDisposalDate.TabIndex = 99
        Me.lblDisposalDate.Text = "Disposal Date"
        '
        'lblDisposalMethod
        '
        Me.lblDisposalMethod.AutoSize = True
        Me.lblDisposalMethod.Location = New System.Drawing.Point(277, 23)
        Me.lblDisposalMethod.Name = "lblDisposalMethod"
        Me.lblDisposalMethod.Size = New System.Drawing.Size(85, 13)
        Me.lblDisposalMethod.TabIndex = 98
        Me.lblDisposalMethod.Text = "Disposal Method"
        '
        'txtCreateDate
        '
        Me.txtCreateDate.EditValue = ""
        Me.txtCreateDate.Location = New System.Drawing.Point(598, 172)
        Me.txtCreateDate.Name = "txtCreateDate"
        Me.txtCreateDate.Properties.MaxLength = 12
        Me.txtCreateDate.Properties.ReadOnly = True
        Me.txtCreateDate.Size = New System.Drawing.Size(110, 19)
        Me.txtCreateDate.TabIndex = 94
        '
        'txtUpdateDate
        '
        Me.txtUpdateDate.EditValue = ""
        Me.txtUpdateDate.Location = New System.Drawing.Point(598, 137)
        Me.txtUpdateDate.Name = "txtUpdateDate"
        Me.txtUpdateDate.Properties.MaxLength = 12
        Me.txtUpdateDate.Properties.ReadOnly = True
        Me.txtUpdateDate.Size = New System.Drawing.Size(110, 19)
        Me.txtUpdateDate.TabIndex = 93
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(516, 140)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 13)
        Me.Label4.TabIndex = 92
        Me.Label4.Text = "Update Date"
        '
        'txtCreatedIn
        '
        Me.txtCreatedIn.EditValue = ""
        Me.txtCreatedIn.Location = New System.Drawing.Point(598, 100)
        Me.txtCreatedIn.Name = "txtCreatedIn"
        Me.txtCreatedIn.Properties.MaxLength = 12
        Me.txtCreatedIn.Properties.ReadOnly = True
        Me.txtCreatedIn.Size = New System.Drawing.Size(110, 19)
        Me.txtCreatedIn.TabIndex = 91
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(518, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 90
        Me.Label3.Text = "Source"
        '
        'pnlStatus
        '
        Me.pnlStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlStatus.Controls.Add(Me.lblStatus)
        Me.pnlStatus.Controls.Add(Me.imgStatus)
        Me.pnlStatus.Controls.Add(Me.Label2)
        Me.pnlStatus.Location = New System.Drawing.Point(537, 49)
        Me.pnlStatus.Name = "pnlStatus"
        Me.pnlStatus.Size = New System.Drawing.Size(173, 30)
        Me.pnlStatus.TabIndex = 89
        Me.pnlStatus.Visible = False
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(85, 14)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(44, 13)
        Me.lblStatus.TabIndex = 90
        Me.lblStatus.Text = "Status"
        '
        'imgStatus
        '
        Me.imgStatus.Location = New System.Drawing.Point(62, 12)
        Me.imgStatus.Name = "imgStatus"
        Me.imgStatus.Size = New System.Drawing.Size(16, 16)
        Me.imgStatus.TabIndex = 89
        Me.imgStatus.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 87
        Me.Label2.Text = "Status:"
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(5, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(456, 63)
        Me.PictureBox1.TabIndex = 78
        Me.PictureBox1.TabStop = False
        '
        'btnMarkAsLost
        '
        Me.btnMarkAsLost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnMarkAsLost.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMarkAsLost.Location = New System.Drawing.Point(127, 547)
        Me.btnMarkAsLost.Name = "btnMarkAsLost"
        Me.btnMarkAsLost.Size = New System.Drawing.Size(107, 31)
        Me.btnMarkAsLost.TabIndex = 79
        Me.btnMarkAsLost.Text = "&Mark As Lost"
        Me.btnMarkAsLost.UseVisualStyleBackColor = True
        '
        'frmAssets
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(733, 590)
        Me.Controls.Add(Me.btnMarkAsLost)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnNewAsset)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.lblEvaluation)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "frmAssets"
        Me.Text = "Assets"
        CType(Me.txtAstDesc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlNavication.ResumeLayout(False)
        CType(Me.txtSerialNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAstModel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.grdAstHist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAstHistView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBarCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAstNum.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grpDisposalInfo.ResumeLayout(False)
        Me.grpDisposalInfo.PerformLayout()
        CType(Me.txtCreateDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUpdateDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCreatedIn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlStatus.ResumeLayout(False)
        Me.pnlStatus.PerformLayout()
        CType(Me.imgStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ZTCategory As ZulTree.ZulTree
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAstDesc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ZTLocation As ZulTree.ZulTree
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents AssetsID As ZulLOV.ZulLOV
    Friend WithEvents btnNewAsset1 As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents pnlNavication As System.Windows.Forms.Panel
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnPre As System.Windows.Forms.Button
    Friend WithEvents txtSerialNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Sear As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents txtAstModel As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnNewAsset As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grdAstHist As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdAstHistView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblEvaluation As System.Windows.Forms.Label
    Friend WithEvents txtBarCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents txtAstNum As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents imgStatus As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents pnlStatus As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnMarkAsLost As System.Windows.Forms.Button
    Friend WithEvents txtCreatedIn As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtUpdateDate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCreateDate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbDisposalMethod As ZulLOV.ZulLOV
    Friend WithEvents dtDispdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkDisposed As System.Windows.Forms.CheckBox
    Friend WithEvents lblDisposalDate As System.Windows.Forms.Label
    Friend WithEvents lblDisposalMethod As System.Windows.Forms.Label
    Friend WithEvents grpDisposalInfo As System.Windows.Forms.GroupBox
End Class
