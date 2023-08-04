<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOfflineMachines
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
        Me.grd = New DevExpress.XtraGrid.GridControl
        Me.grdView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnImport = New System.Windows.Forms.Button
        Me.btnExport = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnNew = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtCompanyRange = New DevExpress.XtraEditors.TextEdit
        Me.Label6 = New System.Windows.Forms.Label
        Me.cmbComp = New ZulLOV.ZulLOV
        Me.Label45 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtStartSerial = New DevExpress.XtraEditors.TextEdit
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtEndSerial = New DevExpress.XtraEditors.TextEdit
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lblConnectionStatus = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmbDBServer = New DevExpress.XtraEditors.ComboBoxEdit
        Me.txtDBUname = New DevExpress.XtraEditors.TextEdit
        Me.txtDBPass = New DevExpress.XtraEditors.TextEdit
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnGetservers = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnCheckConnection = New System.Windows.Forms.Button
        Me.txtSqlPort = New DevExpress.XtraEditors.TextEdit
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtLastAssetNum = New DevExpress.XtraEditors.TextEdit
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtMachineID = New DevExpress.XtraEditors.TextEdit
        Me.txtMachineDesc = New DevExpress.XtraEditors.TextEdit
        Me.pbAllStatus = New System.Windows.Forms.ProgressBar
        Me.lblAllStatus = New System.Windows.Forms.Label
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtCompanyRange.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStartSerial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEndSerial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.cmbDBServer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDBUname.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDBPass.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSqlPort.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLastAssetNum.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMachineID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMachineDesc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.Location = New System.Drawing.Point(8, 224)
        Me.grd.MainView = Me.grdView
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(652, 232)
        Me.grd.TabIndex = 14
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
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Image = Global.ZulAssets.My.Resources.Icons.delete
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(379, 495)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(78, 28)
        Me.btnDelete.TabIndex = 19
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.Visible = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Image = Global.ZulAssets.My.Resources.Icons.save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(463, 495)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(78, 28)
        Me.btnSave.TabIndex = 20
        Me.btnSave.Text = "&Save"
        '
        'btnImport
        '
        Me.btnImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnImport.Location = New System.Drawing.Point(8, 495)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(90, 28)
        Me.btnImport.TabIndex = 16
        Me.btnImport.Text = "&Import Data"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Location = New System.Drawing.Point(114, 495)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(100, 28)
        Me.btnExport.TabIndex = 17
        Me.btnExport.Text = "&Export Data"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Machine ID"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Image = Global.ZulAssets.My.Resources.Icons.Close
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(582, 495)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(78, 28)
        Me.btnExit.TabIndex = 21
        Me.btnExit.Text = "&Close"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(323, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Machine Description"
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(264, 12)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(38, 21)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = "&New"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.txtCompanyRange)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cmbComp)
        Me.GroupBox1.Controls.Add(Me.Label45)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtStartSerial)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtEndSerial)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.txtLastAssetNum)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnNew)
        Me.GroupBox1.Controls.Add(Me.txtMachineID)
        Me.GroupBox1.Controls.Add(Me.txtMachineDesc)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(652, 222)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        '
        'txtCompanyRange
        '
        Me.txtCompanyRange.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCompanyRange.Location = New System.Drawing.Point(452, 40)
        Me.txtCompanyRange.Name = "txtCompanyRange"
        Me.txtCompanyRange.Properties.MaxLength = 50
        Me.txtCompanyRange.Properties.ReadOnly = True
        Me.txtCompanyRange.Size = New System.Drawing.Size(186, 19)
        Me.txtCompanyRange.TabIndex = 90
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(323, 43)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(99, 13)
        Me.Label6.TabIndex = 91
        Me.Label6.Text = "Company Range"
        '
        'cmbComp
        '
        Me.cmbComp.BackColor = System.Drawing.Color.White
        Me.cmbComp.DataSource = Nothing
        Me.cmbComp.DisplayMember = ""
        Me.cmbComp.Location = New System.Drawing.Point(120, 38)
        Me.cmbComp.Name = "cmbComp"
        Me.cmbComp.SelectedIndex = -1
        Me.cmbComp.SelectedText = ""
        Me.cmbComp.SelectedValue = ""
        Me.cmbComp.Size = New System.Drawing.Size(182, 24)
        Me.cmbComp.TabIndex = 88
        Me.cmbComp.TextReadOnly = False
        Me.cmbComp.ValueMember = ""
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(13, 43)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(60, 13)
        Me.Label45.TabIndex = 89
        Me.Label45.Text = "Company"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(323, 70)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(62, 13)
        Me.Label9.TabIndex = 87
        Me.Label9.Text = "End Serial"
        '
        'txtStartSerial
        '
        Me.txtStartSerial.Location = New System.Drawing.Point(120, 67)
        Me.txtStartSerial.Name = "txtStartSerial"
        Me.txtStartSerial.Properties.MaxLength = 8
        Me.txtStartSerial.Size = New System.Drawing.Size(182, 19)
        Me.txtStartSerial.TabIndex = 84
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(13, 68)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 13)
        Me.Label10.TabIndex = 86
        Me.Label10.Text = "Start Serial"
        '
        'txtEndSerial
        '
        Me.txtEndSerial.Location = New System.Drawing.Point(452, 67)
        Me.txtEndSerial.Name = "txtEndSerial"
        Me.txtEndSerial.Properties.MaxLength = 8
        Me.txtEndSerial.Size = New System.Drawing.Size(186, 19)
        Me.txtEndSerial.TabIndex = 85
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblConnectionStatus)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.cmbDBServer)
        Me.GroupBox2.Controls.Add(Me.txtDBUname)
        Me.GroupBox2.Controls.Add(Me.txtDBPass)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.btnGetservers)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.btnCheckConnection)
        Me.GroupBox2.Controls.Add(Me.txtSqlPort)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 116)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(639, 100)
        Me.GroupBox2.TabIndex = 34
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Connection Info"
        '
        'lblConnectionStatus
        '
        Me.lblConnectionStatus.AutoSize = True
        Me.lblConnectionStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConnectionStatus.Location = New System.Drawing.Point(447, 76)
        Me.lblConnectionStatus.Name = "lblConnectionStatus"
        Me.lblConnectionStatus.Size = New System.Drawing.Size(109, 13)
        Me.lblConnectionStatus.TabIndex = 30
        Me.lblConnectionStatus.Text = "Connection status"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(7, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 13)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Database Server"
        '
        'cmbDBServer
        '
        Me.cmbDBServer.Location = New System.Drawing.Point(113, 16)
        Me.cmbDBServer.Name = "cmbDBServer"
        Me.cmbDBServer.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbDBServer.Size = New System.Drawing.Size(151, 19)
        Me.cmbDBServer.TabIndex = 5
        '
        'txtDBUname
        '
        Me.txtDBUname.Location = New System.Drawing.Point(445, 16)
        Me.txtDBUname.Name = "txtDBUname"
        Me.txtDBUname.Size = New System.Drawing.Size(186, 19)
        Me.txtDBUname.TabIndex = 7
        '
        'txtDBPass
        '
        Me.txtDBPass.Location = New System.Drawing.Point(445, 41)
        Me.txtDBPass.Name = "txtDBPass"
        Me.txtDBPass.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtDBPass.Size = New System.Drawing.Size(186, 19)
        Me.txtDBPass.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(319, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "User Name"
        '
        'btnGetservers
        '
        Me.btnGetservers.Image = Global.ZulAssets.My.Resources.Icons.server
        Me.btnGetservers.Location = New System.Drawing.Point(270, 15)
        Me.btnGetservers.Name = "btnGetservers"
        Me.btnGetservers.Size = New System.Drawing.Size(25, 23)
        Me.btnGetservers.TabIndex = 6
        Me.btnGetservers.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(319, 43)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 13)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Password"
        '
        'btnCheckConnection
        '
        Me.btnCheckConnection.Image = Global.ZulAssets.My.Resources.Icons.DB
        Me.btnCheckConnection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCheckConnection.Location = New System.Drawing.Point(299, 71)
        Me.btnCheckConnection.Name = "btnCheckConnection"
        Me.btnCheckConnection.Size = New System.Drawing.Size(133, 23)
        Me.btnCheckConnection.TabIndex = 10
        Me.btnCheckConnection.Text = "Check Connection"
        Me.btnCheckConnection.UseVisualStyleBackColor = True
        '
        'txtSqlPort
        '
        Me.txtSqlPort.Location = New System.Drawing.Point(113, 41)
        Me.txtSqlPort.Name = "txtSqlPort"
        Me.txtSqlPort.Size = New System.Drawing.Size(182, 19)
        Me.txtSqlPort.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 44)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(92, 13)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "Sql Server Port"
        '
        'txtLastAssetNum
        '
        Me.txtLastAssetNum.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLastAssetNum.Location = New System.Drawing.Point(452, 92)
        Me.txtLastAssetNum.Name = "txtLastAssetNum"
        Me.txtLastAssetNum.Properties.MaxLength = 50
        Me.txtLastAssetNum.Properties.ReadOnly = True
        Me.txtLastAssetNum.Size = New System.Drawing.Size(186, 19)
        Me.txtLastAssetNum.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(323, 95)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(113, 13)
        Me.Label8.TabIndex = 32
        Me.Label8.Text = "Last Asset Number"
        '
        'txtMachineID
        '
        Me.txtMachineID.Location = New System.Drawing.Point(120, 13)
        Me.txtMachineID.Name = "txtMachineID"
        Me.txtMachineID.Properties.MaxLength = 20
        Me.txtMachineID.Properties.ReadOnly = True
        Me.txtMachineID.Size = New System.Drawing.Size(138, 19)
        Me.txtMachineID.TabIndex = 0
        '
        'txtMachineDesc
        '
        Me.txtMachineDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMachineDesc.Location = New System.Drawing.Point(452, 12)
        Me.txtMachineDesc.Name = "txtMachineDesc"
        Me.txtMachineDesc.Properties.MaxLength = 50
        Me.txtMachineDesc.Size = New System.Drawing.Size(186, 19)
        Me.txtMachineDesc.TabIndex = 2
        '
        'pbAllStatus
        '
        Me.pbAllStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbAllStatus.Location = New System.Drawing.Point(5, 478)
        Me.pbAllStatus.Name = "pbAllStatus"
        Me.pbAllStatus.Size = New System.Drawing.Size(655, 13)
        Me.pbAllStatus.TabIndex = 41
        Me.pbAllStatus.Visible = False
        '
        'lblAllStatus
        '
        Me.lblAllStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAllStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblAllStatus.Location = New System.Drawing.Point(12, 461)
        Me.lblAllStatus.Name = "lblAllStatus"
        Me.lblAllStatus.Size = New System.Drawing.Size(644, 13)
        Me.lblAllStatus.TabIndex = 42
        Me.lblAllStatus.Text = "Label1"
        Me.lblAllStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblAllStatus.Visible = False
        '
        'frmOfflineMachines
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(668, 528)
        Me.Controls.Add(Me.lblAllStatus)
        Me.Controls.Add(Me.pbAllStatus)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnImport)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmOfflineMachines"
        Me.Text = "Offline Machines"
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtCompanyRange.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStartSerial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEndSerial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.cmbDBServer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDBUname.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDBPass.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSqlPort.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLastAssetNum.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMachineID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMachineDesc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtMachineID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtMachineDesc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnGetservers As System.Windows.Forms.Button
    Friend WithEvents btnCheckConnection As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtSqlPort As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDBPass As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDBUname As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbDBServer As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtLastAssetNum As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblConnectionStatus As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtStartSerial As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtEndSerial As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbComp As ZulLOV.ZulLOV
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyRange As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents pbAllStatus As System.Windows.Forms.ProgressBar
    Friend WithEvents lblAllStatus As System.Windows.Forms.Label
End Class
