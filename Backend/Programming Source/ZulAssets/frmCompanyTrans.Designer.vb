<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCompanyTrans
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCompanyTrans))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.MainGroup = New System.Windows.Forms.GroupBox
        Me.txtGLCode = New DevExpress.XtraEditors.TextEdit
        Me.Label1 = New System.Windows.Forms.Label
        Me.AssetsID = New ZulLOV.ZulLOV
        Me.txtCName = New DevExpress.XtraEditors.TextEdit
        Me.dtpur = New DevExpress.XtraEditors.TextEdit
        Me.txtAstNum = New DevExpress.XtraEditors.TextEdit
        Me.txtRef = New DevExpress.XtraEditors.TextEdit
        Me.Label44 = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmbNewGLCode = New ZulLOV.ZulLOV
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbComp = New ZulLOV.ZulLOV
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtSalYr = New DevExpress.XtraEditors.TextEdit
        Me.txtBookVal = New DevExpress.XtraEditors.TextEdit
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtReFIDNew = New DevExpress.XtraEditors.TextEdit
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtSalVal = New DevExpress.XtraEditors.TextEdit
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtRemarks = New DevExpress.XtraEditors.MemoEdit
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtNewGLCode = New DevExpress.XtraEditors.TextEdit
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtNewAstNum = New DevExpress.XtraEditors.TextEdit
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtNewAstID = New DevExpress.XtraEditors.TextEdit
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnTrans = New System.Windows.Forms.Button
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.grd = New DevExpress.XtraGrid.GridControl
        Me.grdView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.txtAstDesc = New DevExpress.XtraEditors.TextEdit
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainGroup.SuspendLayout()
        CType(Me.txtGLCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAstNum.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtSalYr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBookVal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReFIDNew.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalVal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtNewGLCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNewAstNum.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNewAstID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAstDesc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(5, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(402, 69)
        Me.PictureBox1.TabIndex = 47
        Me.PictureBox1.TabStop = False
        '
        'MainGroup
        '
        Me.MainGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MainGroup.BackColor = System.Drawing.Color.White
        Me.MainGroup.Controls.Add(Me.txtAstDesc)
        Me.MainGroup.Controls.Add(Me.txtGLCode)
        Me.MainGroup.Controls.Add(Me.Label1)
        Me.MainGroup.Controls.Add(Me.AssetsID)
        Me.MainGroup.Controls.Add(Me.txtCName)
        Me.MainGroup.Controls.Add(Me.dtpur)
        Me.MainGroup.Controls.Add(Me.txtAstNum)
        Me.MainGroup.Controls.Add(Me.txtRef)
        Me.MainGroup.Controls.Add(Me.Label44)
        Me.MainGroup.Controls.Add(Me.Label45)
        Me.MainGroup.Controls.Add(Me.Label15)
        Me.MainGroup.Controls.Add(Me.Label38)
        Me.MainGroup.Controls.Add(Me.Label3)
        Me.MainGroup.Controls.Add(Me.Label37)
        Me.MainGroup.Location = New System.Drawing.Point(3, 78)
        Me.MainGroup.Name = "MainGroup"
        Me.MainGroup.Size = New System.Drawing.Size(770, 98)
        Me.MainGroup.TabIndex = 0
        Me.MainGroup.TabStop = False
        Me.MainGroup.Text = "Asset Information"
        '
        'txtGLCode
        '
        Me.txtGLCode.EditValue = " "
        Me.txtGLCode.Location = New System.Drawing.Point(610, 47)
        Me.txtGLCode.Name = "txtGLCode"
        Me.txtGLCode.Properties.MaxLength = 12
        Me.txtGLCode.Properties.ReadOnly = True
        Me.txtGLCode.Size = New System.Drawing.Size(135, 19)
        Me.txtGLCode.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(513, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 234235
        Me.Label1.Text = "GL Code"
        '
        'AssetsID
        '
        Me.AssetsID.BackColor = System.Drawing.Color.White
        Me.AssetsID.DataSource = Nothing
        Me.AssetsID.DisplayMember = ""
        Me.AssetsID.Location = New System.Drawing.Point(87, 19)
        Me.AssetsID.Name = "AssetsID"
        Me.AssetsID.SelectedIndex = -1
        Me.AssetsID.SelectedText = ""
        Me.AssetsID.SelectedValue = ""
        Me.AssetsID.Size = New System.Drawing.Size(175, 24)
        Me.AssetsID.TabIndex = 0
        Me.AssetsID.ValueMember = ""
        '
        'txtCName
        '
        Me.txtCName.EditValue = " "
        Me.txtCName.Location = New System.Drawing.Point(351, 47)
        Me.txtCName.Name = "txtCName"
        Me.txtCName.Properties.MaxLength = 12
        Me.txtCName.Properties.ReadOnly = True
        Me.txtCName.Size = New System.Drawing.Size(141, 19)
        Me.txtCName.TabIndex = 4
        '
        'dtpur
        '
        Me.dtpur.EditValue = " "
        Me.dtpur.Location = New System.Drawing.Point(610, 20)
        Me.dtpur.Name = "dtpur"
        Me.dtpur.Properties.MaxLength = 12
        Me.dtpur.Properties.ReadOnly = True
        Me.dtpur.Size = New System.Drawing.Size(135, 19)
        Me.dtpur.TabIndex = 2
        '
        'txtAstNum
        '
        Me.txtAstNum.EditValue = " "
        Me.txtAstNum.Location = New System.Drawing.Point(351, 20)
        Me.txtAstNum.Name = "txtAstNum"
        Me.txtAstNum.Properties.MaxLength = 12
        Me.txtAstNum.Properties.ReadOnly = True
        Me.txtAstNum.Size = New System.Drawing.Size(141, 19)
        Me.txtAstNum.TabIndex = 1
        '
        'txtRef
        '
        Me.txtRef.Location = New System.Drawing.Point(87, 47)
        Me.txtRef.Name = "txtRef"
        Me.txtRef.Properties.MaxLength = 12
        Me.txtRef.Properties.ReadOnly = True
        Me.txtRef.Size = New System.Drawing.Size(149, 19)
        Me.txtRef.TabIndex = 3
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(5, 72)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(60, 13)
        Me.Label44.TabIndex = 55
        Me.Label44.Text = "Description"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(285, 50)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(52, 13)
        Me.Label45.TabIndex = 57
        Me.Label45.Text = "Company"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label15.Location = New System.Drawing.Point(5, 20)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(55, 13)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "Asset ID"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(5, 47)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(38, 13)
        Me.Label38.TabIndex = 47
        Me.Label38.Text = " Ref #"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(513, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Purchase Date"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(285, 23)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(45, 13)
        Me.Label37.TabIndex = 0
        Me.Label37.Text = "Asset #"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.cmbNewGLCode)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbComp)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.txtSalYr)
        Me.GroupBox1.Controls.Add(Me.txtBookVal)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.txtReFIDNew)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.txtSalVal)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 319)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(770, 181)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Transfer Details"
        '
        'cmbNewGLCode
        '
        Me.cmbNewGLCode.BackColor = System.Drawing.Color.White
        Me.cmbNewGLCode.DataSource = Nothing
        Me.cmbNewGLCode.DisplayMember = ""
        Me.cmbNewGLCode.Location = New System.Drawing.Point(499, 17)
        Me.cmbNewGLCode.Name = "cmbNewGLCode"
        Me.cmbNewGLCode.SelectedIndex = -1
        Me.cmbNewGLCode.SelectedText = ""
        Me.cmbNewGLCode.SelectedValue = ""
        Me.cmbNewGLCode.Size = New System.Drawing.Size(262, 24)
        Me.cmbNewGLCode.TabIndex = 1
        Me.cmbNewGLCode.ValueMember = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(412, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 62
        Me.Label2.Text = "GL Code"
        '
        'cmbComp
        '
        Me.cmbComp.BackColor = System.Drawing.Color.White
        Me.cmbComp.DataSource = Nothing
        Me.cmbComp.DisplayMember = ""
        Me.cmbComp.Location = New System.Drawing.Point(73, 17)
        Me.cmbComp.Name = "cmbComp"
        Me.cmbComp.SelectedIndex = -1
        Me.cmbComp.SelectedText = ""
        Me.cmbComp.SelectedValue = ""
        Me.cmbComp.Size = New System.Drawing.Size(326, 24)
        Me.cmbComp.TabIndex = 0
        Me.cmbComp.ValueMember = ""
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(3, 48)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(67, 13)
        Me.Label26.TabIndex = 60
        Me.Label26.Text = "Current BV"
        '
        'txtSalYr
        '
        Me.txtSalYr.Location = New System.Drawing.Point(323, 44)
        Me.txtSalYr.Name = "txtSalYr"
        Me.txtSalYr.Properties.MaxLength = 2
        Me.txtSalYr.Size = New System.Drawing.Size(51, 19)
        Me.txtSalYr.TabIndex = 3
        '
        'txtBookVal
        '
        Me.txtBookVal.Location = New System.Drawing.Point(73, 45)
        Me.txtBookVal.Name = "txtBookVal"
        Me.txtBookVal.Properties.MaxLength = 12
        Me.txtBookVal.Size = New System.Drawing.Size(141, 19)
        Me.txtBookVal.TabIndex = 2
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(412, 48)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(86, 13)
        Me.Label24.TabIndex = 59
        Me.Label24.Text = "Salvage Value"
        '
        'txtReFIDNew
        '
        Me.txtReFIDNew.EditValue = " "
        Me.txtReFIDNew.Location = New System.Drawing.Point(622, 45)
        Me.txtReFIDNew.Name = "txtReFIDNew"
        Me.txtReFIDNew.Properties.MaxLength = 12
        Me.txtReFIDNew.Size = New System.Drawing.Size(113, 19)
        Me.txtReFIDNew.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(5, 75)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 13)
        Me.Label6.TabIndex = 55
        Me.Label6.Text = "Remarks"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(236, 48)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(81, 13)
        Me.Label25.TabIndex = 58
        Me.Label25.Text = "Salvage Year"
        '
        'txtSalVal
        '
        Me.txtSalVal.Location = New System.Drawing.Point(499, 45)
        Me.txtSalVal.Name = "txtSalVal"
        Me.txtSalVal.Size = New System.Drawing.Size(53, 19)
        Me.txtSalVal.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 57
        Me.Label4.Text = "Company"
        '
        'txtRemarks
        '
        Me.txtRemarks.EditValue = " "
        Me.txtRemarks.Location = New System.Drawing.Point(74, 72)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtRemarks.Properties.Appearance.Options.UseBackColor = True
        Me.txtRemarks.Properties.MaxLength = 200
        Me.txtRemarks.Size = New System.Drawing.Size(661, 36)
        Me.txtRemarks.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(574, 47)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 47
        Me.Label5.Text = " Ref #"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtNewGLCode)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtNewAstNum)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.txtNewAstID)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 114)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(756, 60)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "New Asset Info"
        '
        'txtNewGLCode
        '
        Me.txtNewGLCode.EditValue = " "
        Me.txtNewGLCode.Location = New System.Drawing.Point(574, 27)
        Me.txtNewGLCode.Name = "txtNewGLCode"
        Me.txtNewGLCode.Properties.MaxLength = 12
        Me.txtNewGLCode.Properties.ReadOnly = True
        Me.txtNewGLCode.Size = New System.Drawing.Size(135, 19)
        Me.txtNewGLCode.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(510, 29)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 13)
        Me.Label9.TabIndex = 234245
        Me.Label9.Text = "GL Code"
        '
        'txtNewAstNum
        '
        Me.txtNewAstNum.EditValue = " "
        Me.txtNewAstNum.Location = New System.Drawing.Point(317, 27)
        Me.txtNewAstNum.Name = "txtNewAstNum"
        Me.txtNewAstNum.Properties.MaxLength = 12
        Me.txtNewAstNum.Properties.ReadOnly = True
        Me.txtNewAstNum.Size = New System.Drawing.Size(141, 19)
        Me.txtNewAstNum.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(237, 29)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(74, 13)
        Me.Label8.TabIndex = 234243
        Me.Label8.Text = "Asset Number"
        '
        'txtNewAstID
        '
        Me.txtNewAstID.EditValue = " "
        Me.txtNewAstID.Location = New System.Drawing.Point(68, 27)
        Me.txtNewAstID.Name = "txtNewAstID"
        Me.txtNewAstID.Properties.MaxLength = 12
        Me.txtNewAstID.Properties.ReadOnly = True
        Me.txtNewAstID.Size = New System.Drawing.Size(135, 19)
        Me.txtNewAstID.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 29)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 234241
        Me.Label7.Text = "Asset ID"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Image = Global.ZulAssets.My.Resources.Icons.Close
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(693, 503)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(80, 29)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "&Close"
        '
        'btnTrans
        '
        Me.btnTrans.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTrans.Location = New System.Drawing.Point(535, 503)
        Me.btnTrans.Name = "btnTrans"
        Me.btnTrans.Size = New System.Drawing.Size(122, 29)
        Me.btnTrans.TabIndex = 3
        Me.btnTrans.Text = "&Transfer Asset"
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.BackColor = System.Drawing.Color.White
        Me.GroupBox6.Controls.Add(Me.grd)
        Me.GroupBox6.Location = New System.Drawing.Point(3, 178)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(770, 139)
        Me.GroupBox6.TabIndex = 1
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Asset Books"
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.Location = New System.Drawing.Point(5, 15)
        Me.grd.MainView = Me.grdView
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(759, 118)
        Me.grd.TabIndex = 77
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
        Me.grdView.OptionsView.ShowGroupPanel = False
        Me.grdView.OptionsView.ShowIndicator = False
        Me.grdView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
        Me.grdView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'txtAstDesc
        '
        Me.txtAstDesc.EditValue = " "
        Me.txtAstDesc.Location = New System.Drawing.Point(87, 72)
        Me.txtAstDesc.Name = "txtAstDesc"
        Me.txtAstDesc.Properties.MaxLength = 12
        Me.txtAstDesc.Properties.ReadOnly = True
        Me.txtAstDesc.Size = New System.Drawing.Size(658, 19)
        Me.txtAstDesc.TabIndex = 234236
        '
        'frmCompanyTrans
        '
        Me.AcceptButton = Me.btnTrans
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(779, 535)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnTrans)
        Me.Controls.Add(Me.MainGroup)
        Me.Controls.Add(Me.GroupBox1)
        Me.DoubleBuffered = True
        Me.MinimumSize = New System.Drawing.Size(787, 569)
        Me.Name = "frmCompanyTrans"
        Me.Text = "Inter Company Transfer"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainGroup.ResumeLayout(False)
        Me.MainGroup.PerformLayout()
        CType(Me.txtGLCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAstNum.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtSalYr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBookVal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReFIDNew.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalVal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txtNewGLCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNewAstNum.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNewAstID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAstDesc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents MainGroup As System.Windows.Forms.GroupBox
    Friend WithEvents txtAstNum As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtRef As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtSalYr As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtBookVal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtSalVal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnTrans As System.Windows.Forms.Button
    Friend WithEvents txtCName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpur As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtReFIDNew As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents cmbComp As ZulLOV.ZulLOV
    Friend WithEvents AssetsID As ZulLOV.ZulLOV
    Friend WithEvents txtGLCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbNewGLCode As ZulLOV.ZulLOV
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNewGLCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtNewAstNum As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtNewAstID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtAstDesc As DevExpress.XtraEditors.TextEdit
End Class
