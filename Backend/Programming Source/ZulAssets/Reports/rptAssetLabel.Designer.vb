<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptAssetLabel
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
        Dim Code128Generator1 As DevExpress.XtraPrinting.BarCode.Code128Generator = New DevExpress.XtraPrinting.BarCode.Code128Generator
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.lblRB = New DevExpress.XtraReports.UI.XRLabel
        Me.lblHeading2 = New DevExpress.XtraReports.UI.XRLabel
        Me.lblLB = New DevExpress.XtraReports.UI.XRLabel
        Me.lblHeading1 = New DevExpress.XtraReports.UI.XRLabel
        Me.barCode = New DevExpress.XtraReports.UI.XRBarCode
        Me.ZulAssetsBEDataSet1 = New ZulAssets.ZulAssetsBEDataSet
        Me.AssetsLabelTableAdapter = New ZulAssets.ZulAssetsBEDataSetTableAdapters.AssetsLabelTableAdapter
        CType(Me.ZulAssetsBEDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.lblRB, Me.lblHeading2, Me.lblLB, Me.lblHeading1, Me.barCode})
        Me.Detail.Dpi = 254.0!
        Me.Detail.Height = 249
        Me.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnWidth
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel1
        '
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.Location = New System.Drawing.Point(243, 180)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(95, 32)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.Text = "Asset#"
        '
        'lblRB
        '
        Me.lblRB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "AssetsLabel.AstNum", "")})
        Me.lblRB.Dpi = 254.0!
        Me.lblRB.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRB.Location = New System.Drawing.Point(339, 180)
        Me.lblRB.Name = "lblRB"
        Me.lblRB.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.lblRB.Size = New System.Drawing.Size(137, 32)
        Me.lblRB.StylePriority.UseFont = False
        Me.lblRB.Text = "lblRB"
        '
        'lblHeading2
        '
        Me.lblHeading2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "AssetsLabel.CatFullPath", "")})
        Me.lblHeading2.Dpi = 254.0!
        Me.lblHeading2.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeading2.Location = New System.Drawing.Point(11, 64)
        Me.lblHeading2.Name = "lblHeading2"
        Me.lblHeading2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.lblHeading2.Size = New System.Drawing.Size(466, 37)
        Me.lblHeading2.StylePriority.UseFont = False
        Me.lblHeading2.Text = "lblHeading2"
        '
        'lblLB
        '
        Me.lblLB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "AssetsLabel.LocDesc", "")})
        Me.lblLB.Dpi = 254.0!
        Me.lblLB.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLB.Location = New System.Drawing.Point(11, 180)
        Me.lblLB.Name = "lblLB"
        Me.lblLB.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.lblLB.Size = New System.Drawing.Size(222, 32)
        Me.lblLB.StylePriority.UseFont = False
        Me.lblLB.Text = "lblLB"
        '
        'lblHeading1
        '
        Me.lblHeading1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "AssetsLabel.CompanyName", "")})
        Me.lblHeading1.Dpi = 254.0!
        Me.lblHeading1.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblHeading1.Location = New System.Drawing.Point(11, 11)
        Me.lblHeading1.Name = "lblHeading1"
        Me.lblHeading1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.lblHeading1.Size = New System.Drawing.Size(466, 48)
        Me.lblHeading1.StylePriority.UseFont = False
        Me.lblHeading1.Text = "lblHeading1"
        '
        'barCode
        '
        Me.barCode.Alignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.barCode.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "AssetsLabel.BarCode", "")})
        Me.barCode.Dpi = 254.0!
        Me.barCode.Location = New System.Drawing.Point(11, 106)
        Me.barCode.Module = 3.0!
        Me.barCode.Name = "barCode"
        Me.barCode.Padding = New DevExpress.XtraPrinting.PaddingInfo(25, 25, 0, 0, 254.0!)
        Me.barCode.Size = New System.Drawing.Size(466, 74)
        Code128Generator1.CharacterSet = DevExpress.XtraPrinting.BarCode.Code128Charset.CharsetAuto
        Me.barCode.Symbology = Code128Generator1
        Me.barCode.Text = "10260813533870"
        '
        'ZulAssetsBEDataSet1
        '
        Me.ZulAssetsBEDataSet1.DataSetName = "ZulAssetsBEDataSet"
        Me.ZulAssetsBEDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'AssetsLabelTableAdapter
        '
        Me.AssetsLabelTableAdapter.ClearBeforeFill = True
        '
        'rptAssetLabel
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail})
        Me.DataAdapter = Me.AssetsLabelTableAdapter
        Me.DataMember = "AssetsLabel"
        Me.DataSource = Me.ZulAssetsBEDataSet1
        Me.Dpi = 254.0!
        Me.GridSize = New System.Drawing.Size(4, 4)
        Me.Margins = New System.Drawing.Printing.Margins(5, 5, 5, 5)
        Me.Name = "rptAssetLabel"
        Me.PageHeight = 250
        Me.PageWidth = 500
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.Version = "8.2"
        CType(Me.ZulAssetsBEDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents barCode As DevExpress.XtraReports.UI.XRBarCode
    Friend WithEvents lblLB As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblHeading1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblHeading2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblRB As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ZulAssetsBEDataSet1 As ZulAssets.ZulAssetsBEDataSet
    Friend WithEvents AssetsLabelTableAdapter As ZulAssets.ZulAssetsBEDataSetTableAdapters.AssetsLabelTableAdapter
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
End Class
