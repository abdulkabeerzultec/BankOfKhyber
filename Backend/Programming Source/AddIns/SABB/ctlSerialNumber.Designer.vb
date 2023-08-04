<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlSerialNumber
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl
        Me.btnClear = New DevExpress.XtraEditors.SimpleButton
        Me.btnAutoGen = New DevExpress.XtraEditors.SimpleButton
        Me.txtItemCode = New DevExpress.XtraEditors.TextEdit
        Me.txtSP = New DevExpress.XtraEditors.TextEdit
        Me.txtItemName = New DevExpress.XtraEditors.TextEdit
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton
        Me.btnOk = New DevExpress.XtraEditors.SimpleButton
        Me.grdSerial = New DevExpress.XtraGrid.GridControl
        Me.grvSerial = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutOk = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutAutoGen = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutClear = New DevExpress.XtraLayout.LayoutControlItem
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.txtItemCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtItemName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutOk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutAutoGen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutClear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = True
        Me.LayoutControl1.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LayoutControl1.Appearance.DisabledLayoutItem.Options.UseForeColor = True
        Me.LayoutControl1.Controls.Add(Me.btnClear)
        Me.LayoutControl1.Controls.Add(Me.btnAutoGen)
        Me.LayoutControl1.Controls.Add(Me.txtItemCode)
        Me.LayoutControl1.Controls.Add(Me.txtSP)
        Me.LayoutControl1.Controls.Add(Me.txtItemName)
        Me.LayoutControl1.Controls.Add(Me.btnCancel)
        Me.LayoutControl1.Controls.Add(Me.btnOk)
        Me.LayoutControl1.Controls.Add(Me.grdSerial)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(600, 401)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'btnClear
        '
        Me.btnClear.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnClear.Location = New System.Drawing.Point(181, 366)
        Me.btnClear.MaximumSize = New System.Drawing.Size(100, 70)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 27)
        Me.btnClear.StyleController = Me.LayoutControl1
        Me.btnClear.TabIndex = 16
        Me.btnClear.Text = "Clea&r"
        '
        'btnAutoGen
        '
        Me.btnAutoGen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAutoGen.Location = New System.Drawing.Point(292, 366)
        Me.btnAutoGen.MaximumSize = New System.Drawing.Size(100, 70)
        Me.btnAutoGen.Name = "btnAutoGen"
        Me.btnAutoGen.Size = New System.Drawing.Size(100, 27)
        Me.btnAutoGen.StyleController = Me.LayoutControl1
        Me.btnAutoGen.TabIndex = 7
        Me.btnAutoGen.Text = "&Auto Generate"
        '
        'txtItemCode
        '
        Me.txtItemCode.EditValue = ""
        Me.txtItemCode.Location = New System.Drawing.Point(82, 9)
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.txtItemCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.txtItemCode.Properties.Appearance.Options.UseFont = True
        Me.txtItemCode.Properties.Appearance.Options.UseForeColor = True
        Me.txtItemCode.Properties.ReadOnly = True
        Me.txtItemCode.Size = New System.Drawing.Size(510, 23)
        Me.txtItemCode.StyleController = Me.LayoutControl1
        Me.txtItemCode.TabIndex = 15
        '
        'txtSP
        '
        Me.txtSP.EditValue = ""
        Me.txtSP.Location = New System.Drawing.Point(82, 77)
        Me.txtSP.Name = "txtSP"
        Me.txtSP.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.txtSP.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.txtSP.Properties.Appearance.Options.UseFont = True
        Me.txtSP.Properties.Appearance.Options.UseForeColor = True
        Me.txtSP.Properties.ReadOnly = True
        Me.txtSP.Size = New System.Drawing.Size(510, 23)
        Me.txtSP.StyleController = Me.LayoutControl1
        Me.txtSP.TabIndex = 14
        '
        'txtItemName
        '
        Me.txtItemName.Location = New System.Drawing.Point(82, 43)
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.txtItemName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.txtItemName.Properties.Appearance.Options.UseFont = True
        Me.txtItemName.Properties.Appearance.Options.UseForeColor = True
        Me.txtItemName.Properties.ReadOnly = True
        Me.txtItemName.Size = New System.Drawing.Size(510, 23)
        Me.txtItemName.StyleController = Me.LayoutControl1
        Me.txtItemName.TabIndex = 13
        '
        'btnCancel
        '
        Me.btnCancel.Image = Global.SABBPlugin.My.Resources.Resources.Close16x16
        Me.btnCancel.Location = New System.Drawing.Point(503, 366)
        Me.btnCancel.MaximumSize = New System.Drawing.Size(100, 70)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(89, 27)
        Me.btnCancel.StyleController = Me.LayoutControl1
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "&Cancel"
        '
        'btnOk
        '
        Me.btnOk.Image = Global.SABBPlugin.My.Resources.Resources.Save16x16
        Me.btnOk.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnOk.Location = New System.Drawing.Point(403, 366)
        Me.btnOk.MaximumSize = New System.Drawing.Size(100, 70)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(89, 27)
        Me.btnOk.StyleController = Me.LayoutControl1
        Me.btnOk.TabIndex = 6
        Me.btnOk.Text = "&Ok"
        '
        'grdSerial
        '
        Me.grdSerial.Location = New System.Drawing.Point(9, 111)
        Me.grdSerial.MainView = Me.grvSerial
        Me.grdSerial.Name = "grdSerial"
        Me.grdSerial.Size = New System.Drawing.Size(583, 244)
        Me.grdSerial.TabIndex = 5
        Me.grdSerial.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvSerial})
        '
        'grvSerial
        '
        Me.grvSerial.GridControl = Me.grdSerial
        Me.grvSerial.Name = "grvSerial"
        Me.grvSerial.OptionsCustomization.AllowColumnMoving = False
        Me.grvSerial.OptionsCustomization.AllowColumnResizing = False
        Me.grvSerial.OptionsCustomization.AllowSort = False
        Me.grvSerial.OptionsView.ShowGroupPanel = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutOk, Me.LayoutControlItem4, Me.EmptySpaceItem1, Me.LayoutControlItem10, Me.LayoutControlItem11, Me.LayoutControlItem12, Me.LayoutAutoGen, Me.LayoutClear})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(600, 401)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.grdSerial
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 102)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(594, 255)
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutOk
        '
        Me.LayoutOk.Control = Me.btnOk
        Me.LayoutOk.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutOk.Location = New System.Drawing.Point(394, 357)
        Me.LayoutOk.MaxSize = New System.Drawing.Size(100, 0)
        Me.LayoutOk.MinSize = New System.Drawing.Size(31, 31)
        Me.LayoutOk.Name = "LayoutOk"
        Me.LayoutOk.Size = New System.Drawing.Size(100, 38)
        Me.LayoutOk.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutOk.Text = "LayoutOk"
        Me.LayoutOk.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutOk.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutOk.TextToControlDistance = 0
        Me.LayoutOk.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.btnCancel
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(494, 357)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(100, 0)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(31, 31)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(100, 38)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 357)
        Me.EmptySpaceItem1.MinSize = New System.Drawing.Size(110, 30)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(172, 38)
        Me.EmptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.LayoutControlItem10.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem10.Control = Me.txtItemName
        Me.LayoutControlItem10.CustomizationFormText = "Item Name:"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 34)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(594, 34)
        Me.LayoutControlItem10.Text = "Item Name:"
        Me.LayoutControlItem10.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(68, 16)
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.LayoutControlItem11.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem11.Control = Me.txtSP
        Me.LayoutControlItem11.CustomizationFormText = "Selling Price:"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 68)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(594, 34)
        Me.LayoutControlItem11.Text = "Price:"
        Me.LayoutControlItem11.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(68, 16)
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.LayoutControlItem12.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem12.Control = Me.txtItemCode
        Me.LayoutControlItem12.CustomizationFormText = "Package:"
        Me.LayoutControlItem12.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(594, 34)
        Me.LayoutControlItem12.Text = "Item Code:"
        Me.LayoutControlItem12.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(68, 16)
        '
        'LayoutAutoGen
        '
        Me.LayoutAutoGen.Control = Me.btnAutoGen
        Me.LayoutAutoGen.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutAutoGen.Location = New System.Drawing.Point(283, 357)
        Me.LayoutAutoGen.Name = "LayoutAutoGen"
        Me.LayoutAutoGen.Size = New System.Drawing.Size(111, 38)
        Me.LayoutAutoGen.Text = "LayoutAutoGen"
        Me.LayoutAutoGen.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutAutoGen.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutAutoGen.TextToControlDistance = 0
        Me.LayoutAutoGen.TextVisible = False
        '
        'LayoutClear
        '
        Me.LayoutClear.Control = Me.btnClear
        Me.LayoutClear.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutClear.Location = New System.Drawing.Point(172, 357)
        Me.LayoutClear.Name = "LayoutClear"
        Me.LayoutClear.Size = New System.Drawing.Size(111, 38)
        Me.LayoutClear.Text = "LayoutClear"
        Me.LayoutClear.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutClear.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutClear.TextToControlDistance = 0
        Me.LayoutClear.TextVisible = False
        '
        'ctlSerialNumber
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "ctlSerialNumber"
        Me.Size = New System.Drawing.Size(600, 401)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.txtItemCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtItemName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSerial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvSerial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutOk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutAutoGen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutClear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents grdSerial As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvSerial As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutOk As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents txtItemCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtSP As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtItemName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnAutoGen As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutAutoGen As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnClear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutClear As DevExpress.XtraLayout.LayoutControlItem

End Class
