<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportWizard
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
        Me.WizardControl1 = New DevExpress.XtraWizard.WizardControl
        Me.WelcomeWizardPage1 = New DevExpress.XtraWizard.WelcomeWizardPage
        Me.WPQuerySetup = New DevExpress.XtraWizard.WizardPage
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtReportName = New DevExpress.XtraEditors.TextEdit
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtQuery = New DevExpress.XtraEditors.MemoEdit
        Me.CompletionWizardPage1 = New DevExpress.XtraWizard.CompletionWizardPage
        Me.WPPreviewData = New DevExpress.XtraWizard.WizardPage
        Me.grd = New DevExpress.XtraGrid.GridControl
        Me.grdView = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.btnPreview = New DevExpress.XtraEditors.SimpleButton
        Me.WPDesignReport = New DevExpress.XtraWizard.WizardPage
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.MemoEdit1 = New DevExpress.XtraEditors.MemoEdit
        CType(Me.WizardControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.WizardControl1.SuspendLayout()
        Me.WPQuerySetup.SuspendLayout()
        CType(Me.txtReportName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuery.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.WPPreviewData.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.WPDesignReport.SuspendLayout()
        CType(Me.MemoEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'WizardControl1
        '
        Me.WizardControl1.Controls.Add(Me.WelcomeWizardPage1)
        Me.WizardControl1.Controls.Add(Me.WPQuerySetup)
        Me.WizardControl1.Controls.Add(Me.CompletionWizardPage1)
        Me.WizardControl1.Controls.Add(Me.WPPreviewData)
        Me.WizardControl1.Controls.Add(Me.WPDesignReport)
        Me.WizardControl1.Name = "WizardControl1"
        Me.WizardControl1.Pages.AddRange(New DevExpress.XtraWizard.BaseWizardPage() {Me.WelcomeWizardPage1, Me.WPQuerySetup, Me.WPPreviewData, Me.WPDesignReport, Me.CompletionWizardPage1})
        Me.WizardControl1.Text = "Create New Report"
        '
        'WelcomeWizardPage1
        '
        Me.WelcomeWizardPage1.IntroductionText = "This wizard simplifies creating new report in a series of simple steps"
        Me.WelcomeWizardPage1.Name = "WelcomeWizardPage1"
        Me.WelcomeWizardPage1.Size = New System.Drawing.Size(353, 273)
        Me.WelcomeWizardPage1.Text = "Create Report"
        '
        'WPQuerySetup
        '
        Me.WPQuerySetup.Controls.Add(Me.Label3)
        Me.WPQuerySetup.Controls.Add(Me.txtReportName)
        Me.WPQuerySetup.Controls.Add(Me.Label2)
        Me.WPQuerySetup.Controls.Add(Me.txtQuery)
        Me.WPQuerySetup.DescriptionText = "Enter the Report Name and  your SQL query"
        Me.WPQuerySetup.Name = "WPQuerySetup"
        Me.WPQuerySetup.Size = New System.Drawing.Size(538, 261)
        Me.WPQuerySetup.Text = "Query Setup"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(5, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "SQL Query"
        '
        'txtReportName
        '
        Me.txtReportName.EditValue = ""
        Me.txtReportName.Location = New System.Drawing.Point(91, 14)
        Me.txtReportName.Name = "txtReportName"
        Me.txtReportName.Properties.MaxLength = 25
        Me.txtReportName.Size = New System.Drawing.Size(232, 19)
        Me.txtReportName.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(5, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Report Name"
        '
        'txtQuery
        '
        Me.txtQuery.EditValue = ""
        Me.txtQuery.Location = New System.Drawing.Point(91, 74)
        Me.txtQuery.Margin = New System.Windows.Forms.Padding(2)
        Me.txtQuery.Name = "txtQuery"
        Me.txtQuery.Properties.MaxLength = 2000
        Me.txtQuery.Size = New System.Drawing.Size(433, 133)
        Me.txtQuery.TabIndex = 3
        '
        'CompletionWizardPage1
        '
        Me.CompletionWizardPage1.FinishText = "You have successfully completed creating the report click finish to save the repo" & _
            "rt."
        Me.CompletionWizardPage1.Name = "CompletionWizardPage1"
        Me.CompletionWizardPage1.Size = New System.Drawing.Size(353, 273)
        Me.CompletionWizardPage1.Text = "Completing the report wizard"
        '
        'WPPreviewData
        '
        Me.WPPreviewData.Controls.Add(Me.grd)
        Me.WPPreviewData.Controls.Add(Me.btnPreview)
        Me.WPPreviewData.DescriptionText = "Click Preview Button to check the query results and enable Next Button"
        Me.WPPreviewData.Name = "WPPreviewData"
        Me.WPPreviewData.Size = New System.Drawing.Size(538, 261)
        Me.WPPreviewData.Text = "Preview Data"
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.Location = New System.Drawing.Point(3, 3)
        Me.grd.MainView = Me.grdView
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(532, 220)
        Me.grd.TabIndex = 0
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
        'btnPreview
        '
        Me.btnPreview.Location = New System.Drawing.Point(461, 228)
        Me.btnPreview.Margin = New System.Windows.Forms.Padding(2)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(74, 27)
        Me.btnPreview.TabIndex = 1
        Me.btnPreview.Text = "&Preview"
        '
        'WPDesignReport
        '
        Me.WPDesignReport.Controls.Add(Me.LabelControl1)
        Me.WPDesignReport.Controls.Add(Me.MemoEdit1)
        Me.WPDesignReport.DescriptionText = "Click Next to open the Report Designer and start designing the report"
        Me.WPDesignReport.Name = "WPDesignReport"
        Me.WPDesignReport.Size = New System.Drawing.Size(538, 261)
        Me.WPDesignReport.Text = "Design Report"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(14, 14)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Note:"
        '
        'MemoEdit1
        '
        Me.MemoEdit1.EditValue = "To be able to filter the report when showing it, you need to add parameters to th" & _
            "e report and then use the Filter String  proberty to associate the parameters wi" & _
            "th fields."
        Me.MemoEdit1.Location = New System.Drawing.Point(14, 34)
        Me.MemoEdit1.Name = "MemoEdit1"
        Me.MemoEdit1.Properties.ReadOnly = True
        Me.MemoEdit1.Size = New System.Drawing.Size(512, 125)
        Me.MemoEdit1.TabIndex = 0
        '
        'frmReportWizard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(570, 406)
        Me.Controls.Add(Me.WizardControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReportWizard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Create Report Wizard"
        CType(Me.WizardControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.WizardControl1.ResumeLayout(False)
        Me.WPQuerySetup.ResumeLayout(False)
        Me.WPQuerySetup.PerformLayout()
        CType(Me.txtReportName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQuery.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.WPPreviewData.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.WPDesignReport.ResumeLayout(False)
        Me.WPDesignReport.PerformLayout()
        CType(Me.MemoEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WizardControl1 As DevExpress.XtraWizard.WizardControl
    Friend WithEvents WelcomeWizardPage1 As DevExpress.XtraWizard.WelcomeWizardPage
    Friend WithEvents WPQuerySetup As DevExpress.XtraWizard.WizardPage
    Friend WithEvents CompletionWizardPage1 As DevExpress.XtraWizard.CompletionWizardPage
    Friend WithEvents WPPreviewData As DevExpress.XtraWizard.WizardPage
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdView As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents btnPreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtReportName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents WPDesignReport As DevExpress.XtraWizard.WizardPage
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents MemoEdit1 As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtQuery As DevExpress.XtraEditors.MemoEdit
End Class
