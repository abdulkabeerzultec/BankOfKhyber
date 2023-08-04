<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptCostCenterAudit
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        Me.XrPageInfo3 = New DevExpress.XtraReports.UI.XRPageInfo
        Me.XrLine3 = New DevExpress.XtraReports.UI.XRLine
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel
        Me.lblSchDesc = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrPivotGrid1 = New DevExpress.XtraReports.UI.XRPivotGrid
        Me.CostCenterAuditStatusTableAdapter = New ZulAssets.ZulAssetsBEDataSetTableAdapters.CostCenterAuditStatusTableAdapter
        Me.ZulAssetsBEDataSet1 = New ZulAssets.ZulAssetsBEDataSet
        Me.XrPivotGridField1 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.fieldCostName = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.fieldTotalCount = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.fieldStatus = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.lblSDate = New DevExpress.XtraReports.UI.XRLabel
        Me.pnlSchd = New DevExpress.XtraReports.UI.XRPanel
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel
        Me.lblEDate = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel
        Me.lblSchCode = New DevExpress.XtraReports.UI.XRLabel
        Me.lblFilterText = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.lblCompName = New DevExpress.XtraReports.UI.XRLabel
        Me.picLogo = New DevExpress.XtraReports.UI.XRPictureBox
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand
        Me.XrLine4 = New DevExpress.XtraReports.UI.XRLine
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand
        Me.lblRptFor = New DevExpress.XtraReports.UI.XRLabel
        CType(Me.ZulAssetsBEDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo3, Me.XrLine3, Me.XrLabel8})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.Height = 85
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrPageInfo3
        '
        Me.XrPageInfo3.Dpi = 254.0!
        Me.XrPageInfo3.Font = New System.Drawing.Font("Trebuchet MS", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.XrPageInfo3.Format = "Page {0} of {1}"
        Me.XrPageInfo3.Location = New System.Drawing.Point(2222, 21)
        Me.XrPageInfo3.Name = "XrPageInfo3"
        Me.XrPageInfo3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo3.Size = New System.Drawing.Size(677, 64)
        Me.XrPageInfo3.StylePriority.UseTextAlignment = False
        Me.XrPageInfo3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLine3
        '
        Me.XrLine3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.XrLine3.BorderColor = System.Drawing.Color.Navy
        Me.XrLine3.BorderWidth = 1
        Me.XrLine3.Dpi = 254.0!
        Me.XrLine3.LineWidth = 3
        Me.XrLine3.Location = New System.Drawing.Point(21, 0)
        Me.XrLine3.Name = "XrLine3"
        Me.XrLine3.Size = New System.Drawing.Size(2879, 5)
        Me.XrLine3.StylePriority.UseBackColor = False
        Me.XrLine3.StylePriority.UseBorderColor = False
        Me.XrLine3.StylePriority.UseBorderWidth = False
        '
        'XrLabel8
        '
        Me.XrLabel8.Dpi = 254.0!
        Me.XrLabel8.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.XrLabel8.KeepTogether = True
        Me.XrLabel8.Location = New System.Drawing.Point(21, 13)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel8.Size = New System.Drawing.Size(698, 61)
        Me.XrLabel8.Text = "Cost Center Audit Report"
        '
        'lblSchDesc
        '
        Me.lblSchDesc.Dpi = 254.0!
        Me.lblSchDesc.Font = New System.Drawing.Font("Trebuchet MS", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblSchDesc.Location = New System.Drawing.Point(1503, 21)
        Me.lblSchDesc.Name = "lblSchDesc"
        Me.lblSchDesc.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.lblSchDesc.Size = New System.Drawing.Size(381, 56)
        Me.lblSchDesc.Text = "lblSchDesc"
        '
        'XrLabel5
        '
        Me.XrLabel5.BackColor = System.Drawing.Color.White
        Me.XrLabel5.Dpi = 254.0!
        Me.XrLabel5.Font = New System.Drawing.Font("Trebuchet MS", 10.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel5.Location = New System.Drawing.Point(2238, 106)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel5.Size = New System.Drawing.Size(233, 56)
        Me.XrLabel5.StylePriority.UseBackColor = False
        Me.XrLabel5.Text = "Report For:"
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPivotGrid1})
        Me.Detail.Dpi = 254.0!
        Me.Detail.Font = New System.Drawing.Font("Trebuchet MS", 10.0!)
        Me.Detail.Height = 212
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrPivotGrid1
        '
        Me.XrPivotGrid1.DataAdapter = Me.CostCenterAuditStatusTableAdapter
        Me.XrPivotGrid1.DataMember = "CostCenterAuditStatus"
        Me.XrPivotGrid1.DataSource = Me.ZulAssetsBEDataSet1
        Me.XrPivotGrid1.Dpi = 254.0!
        Me.XrPivotGrid1.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.XrPivotGridField1, Me.fieldCostName, Me.fieldTotalCount, Me.fieldStatus})
        Me.XrPivotGrid1.Location = New System.Drawing.Point(42, 0)
        Me.XrPivotGrid1.Name = "XrPivotGrid1"
        Me.XrPivotGrid1.OLAPConnectionString = ""
        Me.XrPivotGrid1.Size = New System.Drawing.Size(2836, 212)
        '
        'CostCenterAuditStatusTableAdapter
        '
        Me.CostCenterAuditStatusTableAdapter.ClearBeforeFill = True
        '
        'ZulAssetsBEDataSet1
        '
        Me.ZulAssetsBEDataSet1.DataSetName = "ZulAssetsBEDataSet"
        Me.ZulAssetsBEDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'XrPivotGridField1
        '
        Me.XrPivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField1.AreaIndex = 0
        Me.XrPivotGridField1.Caption = "SN"
        Me.XrPivotGridField1.FieldName = "CostName"
        Me.XrPivotGridField1.Name = "XrPivotGridField1"
        Me.XrPivotGridField1.Width = 70
        '
        'fieldCostName
        '
        Me.fieldCostName.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.fieldCostName.AreaIndex = 1
        Me.fieldCostName.Caption = "Cost Name"
        Me.fieldCostName.FieldName = "CostName"
        Me.fieldCostName.Name = "fieldCostName"
        Me.fieldCostName.Width = 250
        '
        'fieldTotalCount
        '
        Me.fieldTotalCount.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.fieldTotalCount.AreaIndex = 0
        Me.fieldTotalCount.Caption = "Total Count"
        Me.fieldTotalCount.FieldName = "TotalCount"
        Me.fieldTotalCount.Name = "fieldTotalCount"
        Me.fieldTotalCount.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending
        '
        'fieldStatus
        '
        Me.fieldStatus.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.fieldStatus.AreaIndex = 0
        Me.fieldStatus.Caption = "Status"
        Me.fieldStatus.FieldName = "Status"
        Me.fieldStatus.Name = "fieldStatus"
        '
        'lblSDate
        '
        Me.lblSDate.Dpi = 254.0!
        Me.lblSDate.Font = New System.Drawing.Font("Trebuchet MS", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblSDate.Location = New System.Drawing.Point(466, 85)
        Me.lblSDate.Name = "lblSDate"
        Me.lblSDate.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.lblSDate.Size = New System.Drawing.Size(381, 56)
        Me.lblSDate.Text = "lblSDate"
        '
        'pnlSchd
        '
        Me.pnlSchd.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel2, Me.lblSDate, Me.XrLabel4, Me.lblEDate, Me.lblSchDesc, Me.XrLabel3, Me.lblSchCode, Me.lblFilterText})
        Me.pnlSchd.Dpi = 254.0!
        Me.pnlSchd.Location = New System.Drawing.Point(21, 11)
        Me.pnlSchd.Name = "pnlSchd"
        Me.pnlSchd.Size = New System.Drawing.Size(2212, 148)
        '
        'XrLabel2
        '
        Me.XrLabel2.BackColor = System.Drawing.Color.White
        Me.XrLabel2.Dpi = 254.0!
        Me.XrLabel2.Font = New System.Drawing.Font("Trebuchet MS", 10.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel2.Location = New System.Drawing.Point(21, 85)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel2.Size = New System.Drawing.Size(444, 56)
        Me.XrLabel2.StylePriority.UseBackColor = False
        Me.XrLabel2.Text = "Start Date:"
        '
        'XrLabel4
        '
        Me.XrLabel4.BackColor = System.Drawing.Color.White
        Me.XrLabel4.Dpi = 254.0!
        Me.XrLabel4.Font = New System.Drawing.Font("Trebuchet MS", 10.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel4.Location = New System.Drawing.Point(931, 85)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel4.Size = New System.Drawing.Size(566, 56)
        Me.XrLabel4.StylePriority.UseBackColor = False
        Me.XrLabel4.Text = "End Date:"
        '
        'lblEDate
        '
        Me.lblEDate.Dpi = 254.0!
        Me.lblEDate.Font = New System.Drawing.Font("Trebuchet MS", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblEDate.Location = New System.Drawing.Point(1503, 85)
        Me.lblEDate.Name = "lblEDate"
        Me.lblEDate.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.lblEDate.Size = New System.Drawing.Size(381, 56)
        Me.lblEDate.Text = "lblEDate"
        '
        'XrLabel3
        '
        Me.XrLabel3.BackColor = System.Drawing.Color.White
        Me.XrLabel3.Dpi = 254.0!
        Me.XrLabel3.Font = New System.Drawing.Font("Trebuchet MS", 10.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel3.Location = New System.Drawing.Point(931, 21)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel3.Size = New System.Drawing.Size(566, 56)
        Me.XrLabel3.StylePriority.UseBackColor = False
        Me.XrLabel3.Text = "Inventory Schedule Description:"
        '
        'lblSchCode
        '
        Me.lblSchCode.Dpi = 254.0!
        Me.lblSchCode.Font = New System.Drawing.Font("Trebuchet MS", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblSchCode.Location = New System.Drawing.Point(466, 21)
        Me.lblSchCode.Name = "lblSchCode"
        Me.lblSchCode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.lblSchCode.Size = New System.Drawing.Size(381, 56)
        Me.lblSchCode.Text = "lblSchCode"
        '
        'lblFilterText
        '
        Me.lblFilterText.BackColor = System.Drawing.Color.White
        Me.lblFilterText.Dpi = 254.0!
        Me.lblFilterText.Font = New System.Drawing.Font("Trebuchet MS", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblFilterText.Location = New System.Drawing.Point(21, 21)
        Me.lblFilterText.Name = "lblFilterText"
        Me.lblFilterText.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.lblFilterText.Size = New System.Drawing.Size(444, 56)
        Me.lblFilterText.StylePriority.UseBackColor = False
        Me.lblFilterText.Text = "Inventory Schedule Code:"
        '
        'XrLabel1
        '
        Me.XrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Trebuchet MS", 18.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.XrLabel1.Location = New System.Drawing.Point(21, 275)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(1968, 82)
        Me.XrLabel1.StylePriority.UseBorders = False
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.Text = "Cost Center Summary of Fixed Assets Audit Report"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'lblCompName
        '
        Me.lblCompName.Dpi = 254.0!
        Me.lblCompName.Font = New System.Drawing.Font("Trebuchet MS", 14.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.lblCompName.Location = New System.Drawing.Point(21, 42)
        Me.lblCompName.Name = "lblCompName"
        Me.lblCompName.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.lblCompName.Size = New System.Drawing.Size(1672, 64)
        Me.lblCompName.Text = "lblCompName"
        '
        'picLogo
        '
        Me.picLogo.Dpi = 254.0!
        Me.picLogo.Location = New System.Drawing.Point(2561, 21)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(338, 312)
        Me.picLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine4, Me.picLogo, Me.XrLabel1, Me.lblCompName})
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ReportHeader.Height = 381
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XrLine4
        '
        Me.XrLine4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.XrLine4.BorderColor = System.Drawing.Color.Navy
        Me.XrLine4.BorderWidth = 1
        Me.XrLine4.Dpi = 254.0!
        Me.XrLine4.LineWidth = 3
        Me.XrLine4.Location = New System.Drawing.Point(21, 360)
        Me.XrLine4.Name = "XrLine4"
        Me.XrLine4.Size = New System.Drawing.Size(2879, 11)
        Me.XrLine4.StylePriority.UseBackColor = False
        Me.XrLine4.StylePriority.UseBorderColor = False
        Me.XrLine4.StylePriority.UseBorderWidth = False
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Dpi = 254.0!
        Me.XrPageInfo1.Font = New System.Drawing.Font("Trebuchet MS", 9.0!, System.Drawing.FontStyle.Bold)
        Me.XrPageInfo1.Location = New System.Drawing.Point(2444, 21)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo1.Size = New System.Drawing.Size(455, 53)
        Me.XrPageInfo1.StylePriority.UsePadding = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.pnlSchd, Me.lblRptFor, Me.XrLabel5, Me.XrPageInfo1})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.Height = 162
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'lblRptFor
        '
        Me.lblRptFor.Dpi = 254.0!
        Me.lblRptFor.Font = New System.Drawing.Font("Trebuchet MS", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblRptFor.Location = New System.Drawing.Point(2476, 106)
        Me.lblRptFor.Name = "lblRptFor"
        Me.lblRptFor.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.lblRptFor.Size = New System.Drawing.Size(444, 56)
        Me.lblRptFor.Text = "Report For:"
        '
        'rptCostCenterAudit
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.PageHeader, Me.PageFooter, Me.ReportHeader})
        Me.Dpi = 254.0!
        Me.Font = New System.Drawing.Font("Trebuchet MS", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(20, 20, 20, 130)
        Me.Name = "rptCostCenterAudit"
        Me.PageHeight = 2101
        Me.PageWidth = 2969
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.Tag = "Found Assets#False##3.4.6.2"
        Me.Version = "8.2"
        CType(Me.ZulAssetsBEDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents XrPageInfo3 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLine3 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblSchDesc As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents lblSDate As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents pnlSchd As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblEDate As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblSchCode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblFilterText As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblCompName As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents picLogo As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrLine4 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents lblRptFor As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPivotGrid1 As DevExpress.XtraReports.UI.XRPivotGrid
    Friend WithEvents fieldCostName As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents fieldTotalCount As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents fieldStatus As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents CostCenterAuditStatusTableAdapter As ZulAssets.ZulAssetsBEDataSetTableAdapters.CostCenterAuditStatusTableAdapter
    Friend WithEvents ZulAssetsBEDataSet1 As ZulAssets.ZulAssetsBEDataSet
    Friend WithEvents XrPivotGridField1 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
End Class
