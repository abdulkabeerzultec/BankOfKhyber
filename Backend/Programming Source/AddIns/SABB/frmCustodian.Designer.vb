<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustodian
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCustodian))
        Me.btnExit = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtCustEmail = New DevExpress.XtraEditors.TextEdit
        Me.txtCustName = New DevExpress.XtraEditors.TextEdit
        Me.txtCustAddress = New DevExpress.XtraEditors.MemoEdit
        Me.txtCustID = New DevExpress.XtraEditors.TextEdit
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtCustPhone = New DevExpress.XtraEditors.TextEdit
        Me.txtCustCell = New DevExpress.XtraEditors.TextEdit
        Me.txtCustFax = New DevExpress.XtraEditors.TextEdit
        Me.btnSave = New System.Windows.Forms.Button
        Me.gbHeader = New System.Windows.Forms.GroupBox
        Me.btnLOV = New System.Windows.Forms.Button
        Me.cmbDepart = New DevExpress.XtraEditors.TextEdit
        Me.cmbDesg = New ZulLOV.ZulLOV
        Me.btnNew = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.btnDelete = New System.Windows.Forms.Button
        Me.grd = New DevExpress.XtraGrid.GridControl
        Me.grdView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.pb = New System.Windows.Forms.ProgressBar
        Me.txtCode = New DevExpress.XtraEditors.TextEdit
        Me.Label4 = New System.Windows.Forms.Label
        CType(Me.txtCustEmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustAddress.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustPhone.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustCell.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustFax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbHeader.SuspendLayout()
        CType(Me.cmbDepart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Image = Global.SABBPlugin.My.Resources.Resources.Close16x16
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(750, 505)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 27)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "&Close"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(531, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Custodian Email"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(321, 113)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Designation"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Custodian ID"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.White
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(321, 18)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(98, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Custodian Name"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(531, 49)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(74, 13)
        Me.Label10.TabIndex = 21
        Me.Label10.Text = "Custodian Cell"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(6, 80)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(45, 13)
        Me.Label11.TabIndex = 18
        Me.Label11.Text = " Addess"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(321, 49)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(88, 13)
        Me.Label12.TabIndex = 19
        Me.Label12.Text = "Custodian Phone"
        '
        'txtCustEmail
        '
        Me.txtCustEmail.Location = New System.Drawing.Point(619, 77)
        Me.txtCustEmail.Name = "txtCustEmail"
        Me.txtCustEmail.Properties.MaxLength = 50
        Me.txtCustEmail.Size = New System.Drawing.Size(123, 20)
        Me.txtCustEmail.TabIndex = 8
        '
        'txtCustName
        '
        Me.txtCustName.Location = New System.Drawing.Point(425, 14)
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.Properties.MaxLength = 100
        Me.txtCustName.Size = New System.Drawing.Size(317, 20)
        Me.txtCustName.TabIndex = 2
        '
        'txtCustAddress
        '
        Me.txtCustAddress.Location = New System.Drawing.Point(83, 80)
        Me.txtCustAddress.Name = "txtCustAddress"
        Me.txtCustAddress.Properties.MaxLength = 100
        Me.txtCustAddress.Size = New System.Drawing.Size(187, 75)
        Me.txtCustAddress.TabIndex = 3
        '
        'txtCustID
        '
        Me.txtCustID.Location = New System.Drawing.Point(83, 14)
        Me.txtCustID.Name = "txtCustID"
        Me.txtCustID.Properties.MaxLength = 25
        Me.txtCustID.Size = New System.Drawing.Size(187, 20)
        Me.txtCustID.TabIndex = 0
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(321, 80)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(74, 13)
        Me.Label13.TabIndex = 20
        Me.Label13.Text = "Custodian Fax"
        '
        'txtCustPhone
        '
        Me.txtCustPhone.Location = New System.Drawing.Point(425, 45)
        Me.txtCustPhone.Name = "txtCustPhone"
        Me.txtCustPhone.Properties.MaxLength = 15
        Me.txtCustPhone.Size = New System.Drawing.Size(100, 20)
        Me.txtCustPhone.TabIndex = 5
        '
        'txtCustCell
        '
        Me.txtCustCell.CausesValidation = False
        Me.txtCustCell.Location = New System.Drawing.Point(619, 45)
        Me.txtCustCell.Name = "txtCustCell"
        Me.txtCustCell.Properties.MaxLength = 15
        Me.txtCustCell.Size = New System.Drawing.Size(123, 20)
        Me.txtCustCell.TabIndex = 6
        '
        'txtCustFax
        '
        Me.txtCustFax.Location = New System.Drawing.Point(425, 77)
        Me.txtCustFax.Name = "txtCustFax"
        Me.txtCustFax.Properties.MaxLength = 15
        Me.txtCustFax.Size = New System.Drawing.Size(100, 20)
        Me.txtCustFax.TabIndex = 7
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Image = Global.SABBPlugin.My.Resources.Resources.Save16x16
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(643, 505)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(76, 27)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "&Save"
        '
        'gbHeader
        '
        Me.gbHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbHeader.BackColor = System.Drawing.Color.White
        Me.gbHeader.Controls.Add(Me.txtCode)
        Me.gbHeader.Controls.Add(Me.Label4)
        Me.gbHeader.Controls.Add(Me.btnLOV)
        Me.gbHeader.Controls.Add(Me.cmbDepart)
        Me.gbHeader.Controls.Add(Me.cmbDesg)
        Me.gbHeader.Controls.Add(Me.btnNew)
        Me.gbHeader.Controls.Add(Me.txtCustName)
        Me.gbHeader.Controls.Add(Me.txtCustAddress)
        Me.gbHeader.Controls.Add(Me.txtCustID)
        Me.gbHeader.Controls.Add(Me.Label13)
        Me.gbHeader.Controls.Add(Me.txtCustPhone)
        Me.gbHeader.Controls.Add(Me.txtCustCell)
        Me.gbHeader.Controls.Add(Me.txtCustFax)
        Me.gbHeader.Controls.Add(Me.Label3)
        Me.gbHeader.Controls.Add(Me.Label2)
        Me.gbHeader.Controls.Add(Me.Label1)
        Me.gbHeader.Controls.Add(Me.Label7)
        Me.gbHeader.Controls.Add(Me.Label8)
        Me.gbHeader.Controls.Add(Me.Label10)
        Me.gbHeader.Controls.Add(Me.Label11)
        Me.gbHeader.Controls.Add(Me.Label12)
        Me.gbHeader.Controls.Add(Me.txtCustEmail)
        Me.gbHeader.Location = New System.Drawing.Point(5, 80)
        Me.gbHeader.Name = "gbHeader"
        Me.gbHeader.Size = New System.Drawing.Size(820, 172)
        Me.gbHeader.TabIndex = 0
        Me.gbHeader.TabStop = False
        '
        'btnLOV
        '
        Me.btnLOV.Image = CType(resources.GetObject("btnLOV.Image"), System.Drawing.Image)
        Me.btnLOV.Location = New System.Drawing.Point(744, 136)
        Me.btnLOV.MaximumSize = New System.Drawing.Size(24, 25)
        Me.btnLOV.Name = "btnLOV"
        Me.btnLOV.Size = New System.Drawing.Size(24, 24)
        Me.btnLOV.TabIndex = 12
        '
        'cmbDepart
        '
        Me.cmbDepart.Location = New System.Drawing.Point(425, 137)
        Me.cmbDepart.Name = "cmbDepart"
        Me.cmbDepart.Properties.MaxLength = 50
        Me.cmbDepart.Properties.ReadOnly = True
        Me.cmbDepart.Size = New System.Drawing.Size(317, 20)
        Me.cmbDepart.TabIndex = 11
        '
        'cmbDesg
        '
        Me.cmbDesg.BackColor = System.Drawing.Color.White
        Me.cmbDesg.DataSource = Nothing
        Me.cmbDesg.DisplayMember = ""
        Me.cmbDesg.Location = New System.Drawing.Point(425, 108)
        Me.cmbDesg.Name = "cmbDesg"
        Me.cmbDesg.SelectedIndex = -1
        Me.cmbDesg.SelectedText = ""
        Me.cmbDesg.SelectedValue = ""
        Me.cmbDesg.Size = New System.Drawing.Size(343, 24)
        Me.cmbDesg.TabIndex = 9
        Me.cmbDesg.TextReadOnly = False
        Me.cmbDesg.ValueMember = ""
        '
        'btnNew
        '
        Me.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNew.Location = New System.Drawing.Point(276, 14)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(39, 22)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = "&New"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(323, 139)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Hierarchy"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(5, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(367, 69)
        Me.PictureBox1.TabIndex = 37
        Me.PictureBox1.TabStop = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Image = Global.SABBPlugin.My.Resources.Resources.Delete16x16
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(560, 505)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(77, 27)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.Visible = False
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.Location = New System.Drawing.Point(5, 255)
        Me.grd.MainView = Me.grdView
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(820, 229)
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
        Me.pb.Location = New System.Drawing.Point(5, 487)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(820, 12)
        Me.pb.TabIndex = 38
        Me.pb.Visible = False
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(83, 46)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Properties.MaxLength = 25
        Me.txtCode.Size = New System.Drawing.Size(187, 20)
        Me.txtCode.TabIndex = 24
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 13)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Code"
        '
        'frmCustodian
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(828, 539)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.pb)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.gbHeader)
        Me.Controls.Add(Me.btnExit)
        Me.DoubleBuffered = True
        Me.MinimumSize = New System.Drawing.Size(500, 400)
        Me.Name = "frmCustodian"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Custodians"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.txtCustEmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustAddress.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustPhone.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustCell.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustFax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbHeader.ResumeLayout(False)
        Me.gbHeader.PerformLayout()
        CType(Me.cmbDepart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmbDesg As ZulLOV.ZulLOV
    Friend WithEvents cmbDepart As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnLOV As System.Windows.Forms.Button
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents gbHeader As System.Windows.Forms.GroupBox
    Friend WithEvents txtCustEmail As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCustName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCustAddress As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtCustID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCustPhone As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCustCell As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCustFax As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
