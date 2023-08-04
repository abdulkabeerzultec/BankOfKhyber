<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptLocLabel
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
        Me.lbltext = New DevExpress.XtraReports.UI.XRLabel
        Me.barCode = New DevExpress.XtraReports.UI.XRBarCode
        Me.lblHeading1 = New DevExpress.XtraReports.UI.XRLabel
        Me.ZulAssetsBEDataSet1 = New ZulAssets.ZulAssetsBEDataSet
        Me.LocationLabelTableAdapter = New ZulAssets.ZulAssetsBEDataSetTableAdapters.LocationLabelTableAdapter
        CType(Me.ZulAssetsBEDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.lbltext, Me.barCode, Me.lblHeading1})
        Me.Detail.Dpi = 254.0!
        Me.Detail.Height = 254
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'lbltext
        '
        Me.lbltext.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "LocationLabel.LocDesc", "")})
        Me.lbltext.Dpi = 254.0!
        Me.lbltext.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltext.Location = New System.Drawing.Point(0, 148)
        Me.lbltext.Name = "lbltext"
        Me.lbltext.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.lbltext.Size = New System.Drawing.Size(466, 32)
        Me.lbltext.StylePriority.UseFont = False
        Me.lbltext.Text = "lbltext"
        '
        'barCode
        '
        Me.barCode.Alignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.barCode.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "LocationLabel.LocBarcode", "")})
        Me.barCode.Dpi = 254.0!
        Me.barCode.Location = New System.Drawing.Point(0, 85)
        Me.barCode.Name = "barCode"
        Me.barCode.Padding = New DevExpress.XtraPrinting.PaddingInfo(25, 25, 0, 0, 254.0!)
        Me.barCode.ShowText = False
        Me.barCode.Size = New System.Drawing.Size(466, 41)
        Code128Generator1.CharacterSet = DevExpress.XtraPrinting.BarCode.Code128Charset.CharsetAuto
        Me.barCode.Symbology = Code128Generator1
        Me.barCode.Text = "barCode"
        '
        'lblHeading1
        '
        Me.lblHeading1.Dpi = 254.0!
        Me.lblHeading1.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblHeading1.Location = New System.Drawing.Point(0, 21)
        Me.lblHeading1.Name = "lblHeading1"
        Me.lblHeading1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.lblHeading1.Size = New System.Drawing.Size(468, 48)
        Me.lblHeading1.StylePriority.UseFont = False
        Me.lblHeading1.Text = "Heading1"
        '
        'ZulAssetsBEDataSet1
        '
        Me.ZulAssetsBEDataSet1.DataSetName = "ZulAssetsBEDataSet"
        Me.ZulAssetsBEDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'LocationLabelTableAdapter
        '
        Me.LocationLabelTableAdapter.ClearBeforeFill = True
        '
        'rptLocLabel
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail})
        Me.DataAdapter = Me.LocationLabelTableAdapter
        Me.DataMember = "LocationLabel"
        Me.DataSource = Me.ZulAssetsBEDataSet1
        Me.Dpi = 254.0!
        Me.Margins = New System.Drawing.Printing.Margins(10, 10, 10, 10)
        Me.Name = "rptLocLabel"
        Me.PageHeight = 250
        Me.PageWidth = 500
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.Version = "8.2"
        CType(Me.ZulAssetsBEDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents lblHeading1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents barCode As DevExpress.XtraReports.UI.XRBarCode
    Friend WithEvents lbltext As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ZulAssetsBEDataSet1 As ZulAssets.ZulAssetsBEDataSet
    Friend WithEvents LocationLabelTableAdapter As ZulAssets.ZulAssetsBEDataSetTableAdapters.LocationLabelTableAdapter
End Class
