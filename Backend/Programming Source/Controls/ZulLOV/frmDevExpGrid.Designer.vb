<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDevExpGrid
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
        Me.components = New System.ComponentModel.Container
        Me.grdLOV = New DevExpress.XtraGrid.GridControl
        Me.grdLOVMainView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.DefaultLookAndFeel1 = New DevExpress.LookAndFeel.DefaultLookAndFeel(Me.components)
        CType(Me.grdLOV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdLOVMainView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdLOV
        '
        Me.grdLOV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdLOV.Location = New System.Drawing.Point(0, 0)
        Me.grdLOV.MainView = Me.grdLOVMainView
        Me.grdLOV.Name = "grdLOV"
        Me.grdLOV.Size = New System.Drawing.Size(267, 165)
        Me.grdLOV.TabIndex = 1
        Me.grdLOV.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdLOVMainView})
        '
        'grdLOVMainView
        '
        Me.grdLOVMainView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdLOVMainView.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdLOVMainView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grdLOVMainView.GridControl = Me.grdLOV
        Me.grdLOVMainView.Name = "grdLOVMainView"
        Me.grdLOVMainView.OptionsBehavior.Editable = False
        Me.grdLOVMainView.OptionsCustomization.AllowColumnMoving = False
        Me.grdLOVMainView.OptionsCustomization.AllowGroup = False
        Me.grdLOVMainView.OptionsFilter.AllowFilterEditor = False
        Me.grdLOVMainView.OptionsMenu.EnableColumnMenu = False
        Me.grdLOVMainView.OptionsMenu.EnableFooterMenu = False
        Me.grdLOVMainView.OptionsMenu.EnableGroupPanelMenu = False
        Me.grdLOVMainView.OptionsNavigation.UseTabKey = False
        Me.grdLOVMainView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdLOVMainView.OptionsSelection.UseIndicatorForSelection = False
        Me.grdLOVMainView.OptionsView.ColumnAutoWidth = False
        Me.grdLOVMainView.OptionsView.ShowAutoFilterRow = True
        Me.grdLOVMainView.OptionsView.ShowGroupPanel = False
        Me.grdLOVMainView.OptionsView.ShowIndicator = False
        Me.grdLOVMainView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
        '
        'DefaultLookAndFeel1
        '
        Me.DefaultLookAndFeel1.LookAndFeel.SkinName = "Money Twins"
        '
        'frmDevExpGrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(267, 165)
        Me.ControlBox = False
        Me.Controls.Add(Me.grdLOV)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MinimumSize = New System.Drawing.Size(100, 100)
        Me.Name = "frmDevExpGrid"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        CType(Me.grdLOV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdLOVMainView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdLOV As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdLOVMainView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents DefaultLookAndFeel1 As DevExpress.LookAndFeel.DefaultLookAndFeel
End Class
