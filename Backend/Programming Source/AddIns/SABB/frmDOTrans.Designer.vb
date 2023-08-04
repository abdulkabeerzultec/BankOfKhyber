<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDOTrans
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtCategory = New DevExpress.XtraEditors.TextEdit
        Me.txtReceivedQTY = New DevExpress.XtraEditors.SpinEdit
        Me.txtAstModel = New DevExpress.XtraEditors.TextEdit
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtIncident = New DevExpress.XtraEditors.TextEdit
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtItemDesc = New DevExpress.XtraEditors.TextEdit
        Me.itmCode = New ZulLOV.ZulLOV
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.ZTLocation = New ZulTree.ZulTree
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label43 = New System.Windows.Forms.Label
        Me.cmbSupp = New ZulLOV.ZulLOV
        Me.Label35 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtPO = New DevExpress.XtraEditors.TextEdit
        Me.Label48 = New System.Windows.Forms.Label
        Me.txtDiscount = New DevExpress.XtraEditors.TextEdit
        Me.txtOrderQty = New DevExpress.XtraEditors.SpinEdit
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtTotal = New DevExpress.XtraEditors.TextEdit
        Me.Label18 = New System.Windows.Forms.Label
        Me.dtpur = New System.Windows.Forms.DateTimePicker
        Me.txtInvoice = New DevExpress.XtraEditors.TextEdit
        Me.txtbase = New DevExpress.XtraEditors.TextEdit
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtSales = New DevExpress.XtraEditors.TextEdit
        Me.TextBox13 = New DevExpress.XtraEditors.TextEdit
        Me.TextBox14 = New DevExpress.XtraEditors.TextEdit
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSkip = New System.Windows.Forms.Button
        Me.dlgFile = New System.Windows.Forms.OpenFileDialog
        Me.grdSerial = New DevExpress.XtraGrid.GridControl
        Me.grvSerial = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtCategory.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReceivedQTY.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAstModel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIncident.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtItemDesc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtPO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiscount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOrderQty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInvoice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSales.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox13.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox14.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.txtCategory)
        Me.GroupBox1.Controls.Add(Me.txtReceivedQTY)
        Me.GroupBox1.Controls.Add(Me.txtAstModel)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtIncident)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.txtItemDesc)
        Me.GroupBox1.Controls.Add(Me.itmCode)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.ZTLocation)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.Label43)
        Me.GroupBox1.Controls.Add(Me.cmbSupp)
        Me.GroupBox1.Controls.Add(Me.Label35)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 130)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(686, 129)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Item Info"
        '
        'txtCategory
        '
        Me.txtCategory.Location = New System.Drawing.Point(443, 101)
        Me.txtCategory.Name = "txtCategory"
        Me.txtCategory.Properties.ReadOnly = True
        Me.txtCategory.Size = New System.Drawing.Size(217, 19)
        Me.txtCategory.TabIndex = 7
        Me.txtCategory.Visible = False
        '
        'txtReceivedQTY
        '
        Me.txtReceivedQTY.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtReceivedQTY.Location = New System.Drawing.Point(138, 104)
        Me.txtReceivedQTY.Name = "txtReceivedQTY"
        Me.txtReceivedQTY.Properties.Appearance.Options.UseTextOptions = True
        Me.txtReceivedQTY.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.txtReceivedQTY.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.txtReceivedQTY.Size = New System.Drawing.Size(110, 19)
        Me.txtReceivedQTY.TabIndex = 6
        '
        'txtAstModel
        '
        Me.txtAstModel.Location = New System.Drawing.Point(442, 75)
        Me.txtAstModel.Name = "txtAstModel"
        Me.txtAstModel.Properties.MaxLength = 20
        Me.txtAstModel.Size = New System.Drawing.Size(229, 19)
        Me.txtAstModel.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(10, 106)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(123, 14)
        Me.Label3.TabIndex = 131
        Me.Label3.Text = "Receiving Quantity"
        '
        'txtIncident
        '
        Me.txtIncident.Location = New System.Drawing.Point(138, 75)
        Me.txtIncident.Name = "txtIncident"
        Me.txtIncident.Properties.MaxLength = 200
        Me.txtIncident.Size = New System.Drawing.Size(230, 19)
        Me.txtIncident.TabIndex = 4
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(10, 78)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(57, 13)
        Me.Label22.TabIndex = 123
        Me.Label22.Text = "Incident #"
        '
        'txtItemDesc
        '
        Me.txtItemDesc.Location = New System.Drawing.Point(442, 17)
        Me.txtItemDesc.Name = "txtItemDesc"
        Me.txtItemDesc.Properties.ReadOnly = True
        Me.txtItemDesc.Size = New System.Drawing.Size(232, 19)
        Me.txtItemDesc.TabIndex = 1
        '
        'itmCode
        '
        Me.itmCode.BackColor = System.Drawing.Color.White
        Me.itmCode.DataSource = Nothing
        Me.itmCode.DisplayMember = ""
        Me.itmCode.Location = New System.Drawing.Point(138, 16)
        Me.itmCode.Name = "itmCode"
        Me.itmCode.SelectedIndex = -1
        Me.itmCode.SelectedText = ""
        Me.itmCode.SelectedValue = ""
        Me.itmCode.Size = New System.Drawing.Size(230, 24)
        Me.itmCode.TabIndex = 0
        Me.itmCode.TextReadOnly = False
        Me.itmCode.ValueMember = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label4.Location = New System.Drawing.Point(10, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Item Code"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(378, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(60, 13)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "Description"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(623, 94)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(0, 13)
        Me.Label20.TabIndex = 86
        Me.Label20.Visible = False
        '
        'ZTLocation
        '
        Me.ZTLocation.BackColor = System.Drawing.Color.White
        Me.ZTLocation.DataSource = Nothing
        Me.ZTLocation.DisplayMember = ""
        Me.ZTLocation.Location = New System.Drawing.Point(138, 44)
        Me.ZTLocation.Name = "ZTLocation"
        Me.ZTLocation.SelectedText = ""
        Me.ZTLocation.SelectedValue = ""
        Me.ZTLocation.Size = New System.Drawing.Size(230, 25)
        Me.ZTLocation.TabIndex = 2
        Me.ZTLocation.TextReadOnly = False
        Me.ZTLocation.ValueMember = ""
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label19.Location = New System.Drawing.Point(10, 50)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(117, 13)
        Me.Label19.TabIndex = 85
        Me.Label19.Text = "Receiving Location"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(378, 78)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(35, 13)
        Me.Label43.TabIndex = 97
        Me.Label43.Text = "Model"
        '
        'cmbSupp
        '
        Me.cmbSupp.BackColor = System.Drawing.Color.White
        Me.cmbSupp.DataSource = Nothing
        Me.cmbSupp.DisplayMember = ""
        Me.cmbSupp.Location = New System.Drawing.Point(442, 46)
        Me.cmbSupp.Name = "cmbSupp"
        Me.cmbSupp.SelectedIndex = -1
        Me.cmbSupp.SelectedText = ""
        Me.cmbSupp.SelectedValue = ""
        Me.cmbSupp.Size = New System.Drawing.Size(231, 24)
        Me.cmbSupp.TabIndex = 3
        Me.cmbSupp.TextReadOnly = False
        Me.cmbSupp.ValueMember = ""
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(378, 52)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(45, 13)
        Me.Label35.TabIndex = 108
        Me.Label35.Text = "Supplier"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.White
        Me.GroupBox2.Controls.Add(Me.txtPO)
        Me.GroupBox2.Controls.Add(Me.Label48)
        Me.GroupBox2.Controls.Add(Me.txtDiscount)
        Me.GroupBox2.Controls.Add(Me.txtOrderQty)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.txtTotal)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.dtpur)
        Me.GroupBox2.Controls.Add(Me.txtInvoice)
        Me.GroupBox2.Controls.Add(Me.txtbase)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtSales)
        Me.GroupBox2.Controls.Add(Me.TextBox13)
        Me.GroupBox2.Controls.Add(Me.TextBox14)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(684, 112)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Delivery Order Info"
        '
        'txtPO
        '
        Me.txtPO.Location = New System.Drawing.Point(120, 17)
        Me.txtPO.Name = "txtPO"
        Me.txtPO.Properties.MaxLength = 20
        Me.txtPO.Properties.ReadOnly = True
        Me.txtPO.Size = New System.Drawing.Size(121, 19)
        Me.txtPO.TabIndex = 0
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(491, 55)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(48, 13)
        Me.Label48.TabIndex = 81
        Me.Label48.Text = "Discount"
        '
        'txtDiscount
        '
        Me.txtDiscount.EditValue = "0.0"
        Me.txtDiscount.Location = New System.Drawing.Point(563, 49)
        Me.txtDiscount.Name = "txtDiscount"
        Me.txtDiscount.Properties.MaxLength = 5
        Me.txtDiscount.Size = New System.Drawing.Size(110, 19)
        Me.txtDiscount.TabIndex = 5
        '
        'txtOrderQty
        '
        Me.txtOrderQty.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtOrderQty.Location = New System.Drawing.Point(120, 81)
        Me.txtOrderQty.Name = "txtOrderQty"
        Me.txtOrderQty.Properties.Appearance.Options.UseTextOptions = True
        Me.txtOrderQty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.txtOrderQty.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.txtOrderQty.Properties.ReadOnly = True
        Me.txtOrderQty.Size = New System.Drawing.Size(121, 19)
        Me.txtOrderQty.TabIndex = 6
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(16, 88)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 13)
        Me.Label14.TabIndex = 79
        Me.Label14.Text = "Order Quantity"
        '
        'txtTotal
        '
        Me.txtTotal.Location = New System.Drawing.Point(364, 82)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Properties.MaxLength = 12
        Me.txtTotal.Properties.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(104, 19)
        Me.txtTotal.TabIndex = 7
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(260, 88)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(56, 13)
        Me.Label18.TabIndex = 22
        Me.Label18.Text = "Total Cost"
        '
        'dtpur
        '
        Me.dtpur.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpur.Location = New System.Drawing.Point(364, 16)
        Me.dtpur.Name = "dtpur"
        Me.dtpur.Size = New System.Drawing.Size(104, 20)
        Me.dtpur.TabIndex = 1
        '
        'txtInvoice
        '
        Me.txtInvoice.Location = New System.Drawing.Point(563, 17)
        Me.txtInvoice.Name = "txtInvoice"
        Me.txtInvoice.Properties.MaxLength = 25
        Me.txtInvoice.Size = New System.Drawing.Size(110, 19)
        Me.txtInvoice.TabIndex = 2
        '
        'txtbase
        '
        Me.txtbase.EditValue = "0.0"
        Me.txtbase.Location = New System.Drawing.Point(120, 49)
        Me.txtbase.Name = "txtbase"
        Me.txtbase.Properties.MaxLength = 8
        Me.txtbase.Size = New System.Drawing.Size(121, 19)
        Me.txtbase.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(491, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Invoice #"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(16, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(46, 13)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Order #"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(260, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Date Purchased"
        '
        'txtSales
        '
        Me.txtSales.EditValue = "0.0"
        Me.txtSales.Location = New System.Drawing.Point(364, 49)
        Me.txtSales.Name = "txtSales"
        Me.txtSales.Properties.MaxLength = 5
        Me.txtSales.Size = New System.Drawing.Size(104, 19)
        Me.txtSales.TabIndex = 4
        '
        'TextBox13
        '
        Me.TextBox13.Location = New System.Drawing.Point(120, 160)
        Me.TextBox13.Name = "TextBox13"
        Me.TextBox13.Size = New System.Drawing.Size(96, 19)
        Me.TextBox13.TabIndex = 4
        '
        'TextBox14
        '
        Me.TextBox14.Location = New System.Drawing.Point(120, 184)
        Me.TextBox14.Name = "TextBox14"
        Me.TextBox14.Size = New System.Drawing.Size(96, 19)
        Me.TextBox14.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Item Cost"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(260, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(79, 13)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Additional Cost"
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(8, 184)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(100, 23)
        Me.Label16.TabIndex = 11
        Me.Label16.Text = "Total Cost"
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(8, 160)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(100, 23)
        Me.Label17.TabIndex = 11
        Me.Label17.Text = "Other Charges"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Image = Global.SABBPlugin.My.Resources.Resources.Save16x16
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(465, 490)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(108, 28)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "&Save &&  Next"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Image = Global.SABBPlugin.My.Resources.Resources.Close16x16
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(579, 490)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(86, 28)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "&Close"
        '
        'btnSkip
        '
        Me.btnSkip.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSkip.Image = Global.SABBPlugin.My.Resources.Resources.Invalid
        Me.btnSkip.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSkip.Location = New System.Drawing.Point(371, 490)
        Me.btnSkip.Name = "btnSkip"
        Me.btnSkip.Size = New System.Drawing.Size(86, 28)
        Me.btnSkip.TabIndex = 3
        Me.btnSkip.Text = "S&kip"
        Me.btnSkip.UseVisualStyleBackColor = True
        '
        'dlgFile
        '
        Me.dlgFile.Filter = "JPEG Images (*.jpg,*.jpeg)|*.jpg;*.jpeg|Gif Images (*.gif)|*.gif|Bitmaps (*.bmp)|" & _
            "*.bmp|PNG (*.png)|*.png|All Images(*.jpg,*.jpeg,*.gif,*.bmp,*.png)|*.jpg;*.jpeg;" & _
            "*.gif;*.bmp;*.png"
        Me.dlgFile.FilterIndex = 5
        Me.dlgFile.Title = "Choose Image file"
        '
        'grdSerial
        '
        Me.grdSerial.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSerial.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.grdSerial.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.grdSerial.Location = New System.Drawing.Point(5, 259)
        Me.grdSerial.MainView = Me.grvSerial
        Me.grdSerial.Name = "grdSerial"
        Me.grdSerial.Size = New System.Drawing.Size(686, 228)
        Me.grdSerial.TabIndex = 2
        Me.grdSerial.UseEmbeddedNavigator = True
        Me.grdSerial.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvSerial})
        '
        'grvSerial
        '
        Me.grvSerial.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grvSerial.Appearance.FooterPanel.Options.UseFont = True
        Me.grvSerial.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grvSerial.Appearance.HeaderPanel.Options.UseFont = True
        Me.grvSerial.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvSerial.GridControl = Me.grdSerial
        Me.grvSerial.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.grvSerial.Name = "grvSerial"
        Me.grvSerial.OptionsCustomization.AllowGroup = False
        Me.grvSerial.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvSerial.OptionsView.ShowGroupPanel = False
        Me.grvSerial.OptionsView.ShowIndicator = False
        Me.grvSerial.OptionsView.ShowViewCaption = True
        Me.grvSerial.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
        Me.grvSerial.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        Me.grvSerial.ViewCaption = "Type or scan Serial Numbers below"
        '
        'frmDOTrans
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(693, 528)
        Me.Controls.Add(Me.grdSerial)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSkip)
        Me.Controls.Add(Me.GroupBox2)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDOTrans"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Bulk Assets Receiving"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtCategory.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReceivedQTY.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAstModel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIncident.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtItemDesc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txtPO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiscount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOrderQty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInvoice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSales.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox13.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox14.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSerial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvSerial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtOrderQty As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtTotal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents dtpur As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtInvoice As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtbase As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtSales As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextBox13 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextBox14 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSkip As System.Windows.Forms.Button
    Friend WithEvents itmCode As ZulLOV.ZulLOV
    Friend WithEvents cmbSupp As ZulLOV.ZulLOV
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents ZTLocation As ZulTree.ZulTree
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents txtDiscount As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtItemDesc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtAstModel As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPO As DevExpress.XtraEditors.TextEdit
    Private WithEvents dlgFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtIncident As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents grdSerial As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvSerial As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtReceivedQTY As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCategory As DevExpress.XtraEditors.TextEdit
End Class
