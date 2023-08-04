Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO
Imports System.Globalization.CultureInfo
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.DXErrorProvider
Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraLayout

Public Class frmAssetsDetails

#Region " -- My Decleration -- "
    Dim LayoutFormatFile As String = AppConfig.AppDataFolder & "\LayoutAssetDetails.xml"
    Dim objattAssetsCoding As attAssetsCoding
    Dim objBALAssetsCoding As New BALAssetsCoding
    Dim objAlHadaIntegration As New BALAlhadaIntegration
    Dim objattCompany As attcompany
    Dim objBALCompany As New BALCompany
    Dim objattAssetDetails As attAssetDetails
    Dim objBALAssetDetails As New BALAssetDetails
    Dim objattAssets As attItems
    Dim objBALAssets As New BALItems
    Dim objattAstBooks As attAstBooks
    Dim objBALAstBooks As New BALAstBooks
    Dim objattDepMeth As attDepreciationMethod
    Dim objBALDepMeth As New BALDepreciationMethod
    Dim Monthconfig As DepMonthConfig = DepMonthConfig.FullMonth
    Dim objattLocation As attLocation
    Dim objBALLocation As New BALLocation
    Dim valProvSecondary As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim valProvAddCost As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim valProvNotNigative As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    'Anonymous Data
    Private AnonymousId As String = ""
    Private DeviceID As String
    Private TransDate As DateTime
    Private InvScheduleId As String = InvScheduleId
    Public AnonymousSaved As Boolean = False
    Private PreviousItemCost As Decimal
    Public IsDemokey As Boolean = False
    Private _RoleID As Integer

    Public Property RoleID() As Integer
        Get
            Return _RoleID
        End Get
        Set(ByVal value As Integer)
            _RoleID = value
        End Set
    End Property

    Enum DepMonthConfig
        HalfMonth = 1
        FullMonth = 2
    End Enum

    Dim isEdit As Boolean = False
    Dim isEdit2 As Boolean = False

#End Region
    Public FrmCop As frmBlkQty
    Private Sub CheckDemo()
        If IsDemokey Then
            'if asset details recrod count more than 10 then close the application

            Dim obj As New BALAssetDetails
            Dim leftcount As Integer = DemoAssetsCount - obj.GetAssetsCount(New attAssetDetails)
            lblEvaluation.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            lblEvaluation.Caption = leftcount & " records left"
            If obj.GetAssetsCount(New attAssetDetails) > DemoAssetsCount Then
                ShowErrorMessage("Evaluation, Assets Count exceed 10, Application will close now.")
                Application.Exit()
            End If
        End If
    End Sub

    Public Sub ShowAnonymousInfo(ByVal AnonymousId As String, ByVal Desc As String, ByVal LocID As String, ByVal CatID As String, ByVal Modle As String, ByVal serial As String, ByVal DeviceID As String, ByVal TransDate As DateTime, ByVal InvID As Integer)
        txtAssetID.Tag = "NewID"
        txtAssetID.EditValue = objBALAssetDetails.Generate_AssetID()
        cmbLocation.EditValue = LocID
        'ZTLocation.SelectedValue = LocID
        'ZTLocation.SelectedText = objBALLocation.Comp_Path(LocID)
        txtAstDesc.Text = Desc
        txtAstModel.Text = Modle
        txtSerialNo.Text = serial

        If Not AppConfig.CodingMode Then
            txtAstNum.Text = Generate_AssetNumber("")
            txtRef.Text = txtSerialNo.Text
        End If

        Me.AnonymousId = AnonymousId
        Me.DeviceID = DeviceID
        Me.TransDate = TransDate
        Me.InvScheduleId = InvID
    End Sub

    Private Sub frmAssetsDetails_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            ' LayoutControl1.SaveLayoutToXml(LayoutFormatFile)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub frmAssetsDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.ActiveControl = txtSerialNumSearch
        txtSerialNumSearch.Focus()
        CheckDemo()

        PopulateItems()
        PopulateBrands()
        PopulateCompanies()
        PopulateCustodian()
        'PopulateAssets()
        PopulateGLCodes()
        PopulateAssetsStatus()
        PopulateCostCenter()
        PopulatePO()
        PopulateSupplier()
        PopulateInsurance()
        PopulateDisposal()
        PopulateLocation()
        valProvMain.SetValidationRule(cmbGL, valRulenotEmpty)
        valProvMain.SetValidationRule(cmbBrand, valRulenotEmpty)
        valProvMain.SetValidationRule(cmbAssetsStatus, valRulenotEmpty)
        valProvMain.SetValidationRule(cmbCustodian, valRulenotEmpty)
        valProvMain.SetValidationRule(cmbItemCode, valRulenotEmpty)
        valProvMain.SetValidationRule(cmbCompany, valRulenotEmpty)
        valProvMain.SetValidationRule(txtAstNum, valRulenotEmpty)
        valProvMain.SetValidationRule(cmbLocation, valRulenotEmpty)

        valProvMain.SetValidationRule(txtbase, valRuleNotContainMinus)

        valProvAddCost.SetValidationRule(txtAddCost, valRuleGreaterThanZero)
        valProvAddCost.SetValidationRule(cmbAddCostType, valRulenotEmpty)

        valProvNotNigative.SetValidationRule(txtbase, valRuleNotContainMinus)
        valProvNotNigative.SetValidationRule(txtSales, valRuleNotContainMinus)
        valProvNotNigative.SetValidationRule(txtDiscount, valRuleNotContainMinus)
        valProvNotNigative.SetValidationRule(txtBookVal, valRuleNotContainMinus)
        valProvNotNigative.SetValidationRule(txtAddCost, valRuleNotContainMinus)
        valProvNotNigative.SetValidationRule(txtPrice, valRuleNotContainMinus)

        valProvSecondary.SetValidationRule(txtBookVal, valRuleGreaterOrEqualZero)
        valProvSecondary.SetValidationRule(txtSalYr, valRulenotEmpty)
        valProvSecondary.SetValidationRule(txtSalVal, valRulenotEmpty)
        valProvSecondary.SetValidationRule(txtSalMonth, valRulenotEmpty)

        txtSalVal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        txtSalVal.Properties.Mask.EditMask = "[01]"
        txtSalMonth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        txtSalMonth.Properties.Mask.EditMask = "[0-9]|10|11"

        txtSalYr.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        txtSalYr.Properties.Mask.EditMask = "([0-9][0-9]?)|100"

        txtSalPercent.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        txtSalPercent.Properties.Mask.EditMask = "\d+(\R.\d{0,2})?"

        txtbase.Properties.Mask.MaskType = NumberMaskType
        txtbase.Properties.Mask.EditMask = NumberMask
        txtbase.Properties.Mask.UseMaskAsDisplayFormat = True
        txtSales.Properties.Mask.MaskType = NumberMaskType
        txtSales.Properties.Mask.EditMask = NumberMask
        txtSales.Properties.Mask.UseMaskAsDisplayFormat = True
        txtDiscount.Properties.Mask.MaskType = NumberMaskType
        txtDiscount.Properties.Mask.EditMask = NumberMask
        txtDiscount.Properties.Mask.UseMaskAsDisplayFormat = True
        txtTotal.Properties.Mask.MaskType = NumberMaskType
        txtTotal.Properties.Mask.EditMask = "###,###,###,###,###.00"
        txtTotal.Properties.Mask.UseMaskAsDisplayFormat = True
        txtBookVal.Properties.Mask.MaskType = NumberMaskType
        txtBookVal.Properties.Mask.EditMask = NumberMask
        txtBookVal.Properties.Mask.UseMaskAsDisplayFormat = True
        txtAddCost.Properties.Mask.MaskType = NumberMaskType
        txtAddCost.Properties.Mask.EditMask = "###,###,0.00"
        txtAddCost.Properties.Mask.UseMaskAsDisplayFormat = True
        txtPrice.Properties.Mask.MaskType = NumberMaskType
        txtPrice.Properties.Mask.EditMask = NumberMask
        txtPrice.Properties.Mask.UseMaskAsDisplayFormat = True

        cmbType.SelectedIndex = 0

        If AnonymousId = "" Then
            btnNew.PerformClick()
        End If



        Get_AddCostType()
        format_ExpectedBookAnnulGrid()
        format_ExpectedBookMonthlyGrid()
        GetCustomFieldsCaption()
        dtService.Properties.EditMask = AppConfig.MaindateFormat
        dtSaleDate.Properties.EditMask = AppConfig.MaindateFormat
        dtDispdate.Properties.EditMask = AppConfig.MaindateFormat
        dtpur.Properties.EditMask = AppConfig.MaindateFormat
        dtBVUpdate.Properties.EditMask = AppConfig.MaindateFormat

        If File.Exists(LayoutFormatFile) Then
            LayoutControl1.RestoreLayoutFromXml(LayoutFormatFile)
        End If
    End Sub
    Public Function GetLayoutControl(ByVal ctlName As String, ByVal ParentCtl As Control) As LayoutControlItem
        Try
            For Each ctl As Control In ParentCtl.Controls
                If TypeOf ctl Is LayoutControl Then
                    For Each item As BaseLayoutItem In CType(ctl, LayoutControl).Items
                        If TypeOf item Is LayoutControlItem Then
                            If CType(item, LayoutControlItem).Control IsNot Nothing AndAlso CType(item, LayoutControlItem).Name = ctlName Then
                                Return CType(item, LayoutControlItem)
                            End If
                        End If
                    Next item
                End If
            Next
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub GetCustomFieldsCaption()
        Dim objCustomFields As New BALCustomFields
        Dim dt As DataTable = objCustomFields.GetAll_CustomFields(New attCustomFields)
        For Each row As DataRow In dt.Rows
            'Get the layout of the control
            Dim item As LayoutControlItem = GetLayoutControl(row("ControlName"), Me)
            If item IsNot Nothing Then
                item.Text = row("EngCaption")
            End If
        Next

    End Sub


    Public Sub LocateAsset(ByVal astID As String)
        If astID <> "" Then
            txtAssetID.EditValue = astID
        End If
    End Sub

#Region " -- Methods -- "
#Region "Formating Grids"
    Private Sub FormatTransHistory()
        With grvTransHistory
            .Columns("Price").Visible = False
            .Columns("Discount").Visible = False
        End With

        With grdTransHistory
            .UseEmbeddedNavigator = True
            .EmbeddedNavigator.Buttons.Append.Visible = False
            .EmbeddedNavigator.Buttons.Edit.Visible = False
            .EmbeddedNavigator.Buttons.EndEdit.Visible = False
            .EmbeddedNavigator.Buttons.Remove.Visible = False
            .EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        End With

        addGridMenu(grdTransHistory)

    End Sub

    Private Sub format_CustHistGrid()
        With grdCustHistView
            .Columns(0).Caption = "History ID"
            .Columns(0).Width = 100

            .Columns(1).Visible = False

            .Columns(2).Width = 200
            .Columns(2).Caption = "Previous Custodian"

            .Columns(3).Width = 200
            .Columns(3).Caption = "Current Custodian"

            .Columns(4).Width = 120
            .Columns(4).Caption = "History Date"
            .Columns(5).Visible = False
            .FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
            .FocusedColumn = .Columns(0)
        End With

        With grdCustHist
            .UseEmbeddedNavigator = True
            .EmbeddedNavigator.Buttons.Append.Visible = False
            .EmbeddedNavigator.Buttons.Edit.Visible = False
            .EmbeddedNavigator.Buttons.EndEdit.Visible = False
            .EmbeddedNavigator.Buttons.Remove.Visible = False
            .EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        End With

        addGridMenu(grdCustHist)
    End Sub

    Private Sub format_AstHistGrid()
        Dim RIAstHist As New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
        RIAstHist.SmallImages = imgAssetStatus
        RIAstHist.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() _
                                                    {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Missing", "0", 0), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Found", "1", 1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Misplaced", "2", 2), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Transfer", "3", 3), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Allocated", "4", 4), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Anonymous", "5", 5)})
        With grdAstHistView
            .Columns(0).Caption = "History ID"
            .Columns(0).Width = 100
            .Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(1).Caption = "Inventory Schedule"
            .Columns(1).Width = 130
            .Columns(2).Caption = "Asset ID"
            .Columns(2).Width = 100
            .Columns(3).Caption = "From Location"
            .Columns(3).Width = 120
            .Columns(4).Caption = "To Location"
            .Columns(4).Width = 120
            .Columns(5).Caption = "History Date"
            .Columns(5).Width = 120
            .Columns(6).Caption = "Status"
            .Columns(6).Width = 120
            .Columns(6).ColumnEdit = RIAstHist

            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .Columns(9).Visible = False
            .FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
            .FocusedColumn = .Columns(0)
        End With
        With grdAstHist
            .UseEmbeddedNavigator = True
            .EmbeddedNavigator.Buttons.Append.Visible = False
            .EmbeddedNavigator.Buttons.Edit.Visible = False
            .EmbeddedNavigator.Buttons.EndEdit.Visible = False
            .EmbeddedNavigator.Buttons.Remove.Visible = False
            .EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        End With
        addGridMenu(grdAstHist)

    End Sub
    Private Sub format_BooksGrid()
        With grdBooksView
            .Columns(0).Width = 2

            .Columns(0).Caption = "Book ID"
            .Columns(0).Width = 50

            .Columns(1).Caption = "Asset ID"
            .Columns(1).Width = 100

            .Columns(12).Caption = "Description"
            .Columns(12).Width = 100
            .Columns(2).Visible = False
            .Columns(3).Visible = False
            .Columns(4).Visible = False
            .Columns(5).Visible = False
            .Columns(6).Visible = False
            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .Columns(9).Visible = False
            .Columns(10).Visible = False
            .Columns(11).Visible = False
            .Columns(13).Visible = False
            .Columns(14).Visible = False
            .Columns(15).Visible = False
        End With
        With grdBooks
            .UseEmbeddedNavigator = True
            .EmbeddedNavigator.Buttons.Append.Visible = False
            .EmbeddedNavigator.Buttons.Edit.Visible = False
            .EmbeddedNavigator.Buttons.EndEdit.Visible = False
            .EmbeddedNavigator.Buttons.Remove.Visible = False
            .EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        End With
        addGridMenu(grdBooks)

    End Sub

    Private Sub format_FiscalYRBookGrid()
        With grdFiscalYRBookView
            .Columns(0).Visible = False
            .Columns(1).Caption = "Book Description"
            .Columns(1).Visible = False
            .Columns(2).Visible = False
            .Columns(3).Caption = "Date"
            .Columns(3).Width = 75
            .Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(4).Caption = "Dep. Value"
            .Columns(4).Width = 85
            .Columns(4).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns(4).DisplayFormat.FormatString = "###,###,###,###,###.00"
            .Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

            .Columns(5).Caption = "Acc Dep Value"
            .Columns(5).Width = 85
            .Columns(5).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns(5).DisplayFormat.FormatString = "###,###,###,###,###.00"
            .Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(6).Caption = "CB Value"
            .Columns(6).Width = 90
            .Columns(6).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns(6).DisplayFormat.FormatString = "###,###,###,###,###.00"
            .Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(7).Caption = "Book Description"
            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .Columns(9).Caption = "Sal. Yr"
            .Columns(9).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
            .FocusedColumn = .Columns(0)
        End With
        With grdFiscalYRBook
            .UseEmbeddedNavigator = True
            .EmbeddedNavigator.Buttons.Append.Visible = False
            .EmbeddedNavigator.Buttons.Edit.Visible = False
            .EmbeddedNavigator.Buttons.EndEdit.Visible = False
            .EmbeddedNavigator.Buttons.Remove.Visible = False
            .EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        End With
        addGridMenu(grdFiscalYRBook)
    End Sub

    Private Sub format_ExpectedBookAnnulGrid()
        If grdExpectedBookAnnual.DataSource Is Nothing Then
            Dim dt As New DataTable
            dt.Columns.Add("AccDep", Type.GetType("System.Double"))
            dt.Columns.Add("Dep", Type.GetType("System.Double"))
            dt.Columns.Add("DepMonths", Type.GetType("System.Int32"))
            dt.Columns.Add("CurrDate", Type.GetType("System.DateTime"))
            dt.Columns.Add("CBV", Type.GetType("System.Double"))
            grdExpectedBookAnnual.DataSource = dt
        End If

        With grdExpectedBookAnnualView
            .Columns(0).Caption = "Acc. Depreciation"
            .Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(0).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns(0).DisplayFormat.FormatString = "###,###,###,###,###.00"
            .Columns(1).Caption = "Depreciation"
            .Columns(1).Width = 80
            .Columns(1).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns(1).DisplayFormat.FormatString = "###,###,###,###,###.00"
            .Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(2).Caption = "Months"
            .Columns(2).Width = 70
            .Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(3).Caption = "Date"
            .Columns(3).Width = 80
            .Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(4).Caption = "CB Value"
            .Columns(4).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns(4).DisplayFormat.FormatString = "###,###,###,###,###.00"
            .Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
            .FocusedColumn = .Columns(0)
        End With

        With grdExpectedBookAnnual
            .UseEmbeddedNavigator = True
            .EmbeddedNavigator.Buttons.Append.Visible = False
            .EmbeddedNavigator.Buttons.Edit.Visible = False
            .EmbeddedNavigator.Buttons.EndEdit.Visible = False
            .EmbeddedNavigator.Buttons.Remove.Visible = False
            .EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        End With
        addGridMenu(grdExpectedBookAnnual)
    End Sub

    Private Sub format_ExpectedBookMonthlyGrid()
        If grdExpectedBookMonthly.DataSource Is Nothing Then
            Dim dt As New DataTable
            dt.Columns.Add("AccDep", Type.GetType("System.Double"))
            dt.Columns.Add("Dep", Type.GetType("System.Double"))
            dt.Columns.Add("CurrDate", Type.GetType("System.DateTime"))
            dt.Columns.Add("CBV", Type.GetType("System.Double"))
            grdExpectedBookMonthly.DataSource = dt
        End If

        With (grdExpectedBookMonthlyView)
            .Columns(0).Caption = "Acc. Depreciation"
            .Columns(0).Width = 120
            .Columns(0).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns(0).DisplayFormat.FormatString = "###,###,###,###,###.00"
            .Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

            .Columns(1).Caption = "Depreciation"
            .Columns(1).Width = 100
            .Columns(1).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns(1).DisplayFormat.FormatString = "###,###,###,###,###.00"
            .Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

            .Columns(2).Caption = "Date"
            .Columns(2).Width = 80
            .Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

            .Columns(3).Caption = "CB Value"
            .Columns(3).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns(3).DisplayFormat.FormatString = "###,###,###,###,###.00"
            .Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
            .FocusedColumn = .Columns(0)
        End With

        With grdExpectedBookMonthly
            .UseEmbeddedNavigator = True
            .EmbeddedNavigator.Buttons.Append.Visible = False
            .EmbeddedNavigator.Buttons.Edit.Visible = False
            .EmbeddedNavigator.Buttons.EndEdit.Visible = False
            .EmbeddedNavigator.Buttons.Remove.Visible = False
            .EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        End With
        addGridMenu(grdExpectedBookMonthly)

    End Sub

    Private Sub format_AddCostGrid()
        With grdAddCostView
            .Columns(0).Caption = "Login Name"
            .Columns(0).Width = 100
            .Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

            .Columns(1).Caption = "Action Date"
            .Columns(1).Width = 160
            .Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

            .Columns(2).Caption = "Previous Item Cost"
            .Columns(2).Width = 160
            .Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(2).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns(2).DisplayFormat.FormatString = "###,###,###,###,###.00"


            .Columns(3).Caption = "Additional Cost"
            .Columns(3).Width = 100
            .Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(3).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns(3).DisplayFormat.FormatString = "###,###,###,###,###.00"

            .Columns(4).Caption = "Type Description"
            .Columns(4).Width = 200
            .Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

            .Columns(5).Visible = False
            .Columns(6).Visible = False
        End With

        With grdAddCost
            .UseEmbeddedNavigator = True
            .EmbeddedNavigator.Buttons.Append.Visible = False
            .EmbeddedNavigator.Buttons.Edit.Visible = False
            .EmbeddedNavigator.Buttons.EndEdit.Visible = False
            .EmbeddedNavigator.Buttons.Remove.Visible = False
            .EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        End With
        addGridMenu(grdAddCost)

    End Sub

#End Region



#Region "Get Data Funcations"
    Private Function Get_Assets_ById(ByVal _Id As String) As DataTable
        Try
            Dim dt As New DataTable
            objattAssets = New attItems
            objattAssets.PKeyCode = _Id
            dt = objBALAssets.GetAllData_Joined(objattAssets)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    If Not isEdit Then 'get the Warranty from Item if it's new record.
                        txtWarranty.Value = dt.Rows(0)("Warranty")
                    End If
                    txtCategory.Text = Convert.ToString(dt.Rows(0)(6))
                    If Not (Convert.ToString(dt.Rows(0)(7)) Is Nothing) Then
                        txtCategory.Tag = Convert.ToString(dt.Rows(0)(7))

                        Dim objBALCategory As New BALCategory

                        txtCategory.Text = objBALCategory.Comp_Path(txtCategory.Tag)
                    End If
                    Return Nothing
                End If
            End If
            txtCategory.Text = ""

            Return Nothing
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
            Return Nothing
        End Try
    End Function

    Private Sub Get_AstDep_History(ByVal AstID As String, ByVal _BookID As String)
        Try
            Dim objBALBookHistory As New BALBookHistory
            Dim objattBookHistory As New attBookHistory
            objattBookHistory.ASTID = AstID
            objattBookHistory.BookID = _BookID
            grdFiscalYRBook.DataSource = objBALBookHistory.GetAll_BookHistory(objattBookHistory)
            format_FiscalYRBookGrid()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub Get_Ast_History(ByVal AstID As String)
        Try
            Dim objBALAst_History As New BALAst_History
            Dim objattAstHistory As New attAstHistory
            objattAstHistory.AstID = AstID
            grdAstHist.DataSource = objBALAst_History.GetAll_Ast_History(objattAstHistory)
            format_AstHistGrid()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub Get_Ast_History_Custodian(ByVal AstID As String)
        Try
            Dim objBALAst_Cust_history As New BALAst_Cust_history
            Dim objattAst_Cust_history As New attAst_Cust_history
            objattAst_Cust_history.AstID = AstID
            grdCustHist.DataSource = objBALAst_Cust_history.GetAll_Ast_Cust_history(objattAst_Cust_history)
            format_CustHistGrid()

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub Get_Ast_TransHistory(ByVal AstID As String)
        Try
            Dim objStockTransfer As New StockTransferBLL
            grdTransHistory.DataSource = objStockTransfer.GetAstIDTransHistory(AstID)
            FormatTransHistory()

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Function Get_Dep(ByVal DepCode As String) As String
        Try
            objattDepMeth = New attDepreciationMethod
            objattDepMeth.PKeyCode = DepCode
            Dim ds As DataTable = objBALDepMeth.GetAll_DepreciationMethod(objattDepMeth)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Return ds.Rows(0)("DepDesc").ToString
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
        Return Nothing
    End Function

    Private Function Get_AssetsDetails_byID(ByVal _id As String, ByVal _astNum As String) As DataTable
        Try
            Dim ds As New DataTable

            objattAssetDetails = New attAssetDetails
            objattAssetDetails.PKeyCode = _id
            If _astNum <> "" Then
                objattAssetDetails.AstNum = _astNum
            End If

            ds = objBALAssetDetails.GetAll_AssetDetails(objattAssetDetails)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Return ds
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
        Return Nothing
    End Function
#End Region
#End Region

    Private Sub Search_Asset(ByVal strAstID As String)
        Try
            lblSearchStatus.ForeColor = Color.OrangeRed

            lblSearchStatus.Text = "N/A"

            CheckDemo()
            'Remove Anonymous form Data to don't allow saving Anonymous Asset on saving
            AnonymousId = ""
            DeviceID = ""
            InvScheduleId = ""
            strAstID = RemoveUnnecessaryChars(strAstID)
            Dim dt As New DataTable
            If strAstID <> "" Then

                dt = Get_AssetsDetails_byID(strAstID, "")
                If dt Is Nothing Then
                    btnNew.PerformClick()
                Else
                    isEdit = True
                    txtPrice.Text = ""
                    txtSoldto.Text = ""
                    txtPreLoc.Text = ""
                    txtCurrentLoc.Text = ""
                    txtDepAstID.Text = strAstID

                    dt.Rows(0)("InsName").ToString()
                    dt.Rows(0)("AstDesc").ToString()
                    dt.Rows(0)("AstBrandName").ToString()
                    dt.Rows(0)("CompanyName").ToString()
                    dt.Rows(0)("DispDesc").ToString()

                    txtbase.Text = Format(CDbl(dt.Rows(0)("BaseCost").ToString()), "###,###,###,###,###.00")

                    cmbItemCode.EditValue = dt.Rows(0)("ItemCode")
                    cmbCompany.EditValue = CInt(dt.Rows(0)("CompanyID"))
                    cmbBrand.EditValue = dt.Rows(0)("AstBrandId")
                    cmbCustodian.EditValue = dt.Rows(0)("CustodianID")
                    cmbAssetsStatus.EditValue = dt.Rows(0)("StatusID")
                    cmbCostCenter.EditValue = dt.Rows(0)("CostID").ToString
                    cmbInsurer.EditValue = dt.Rows(0)("InsID")
                    cmbPOCode.EditValue = dt.Rows(0)("POCOde")
                    cmbSupplier.EditValue = dt.Rows(0)("SuppID")
                    If (dt.Rows(0)("GLCode").ToString <> "" Or dt.Rows(0)("GLCode").ToString Is Nothing) Then
                        cmbGL.EditValue = dt.Rows(0)("GLCode")
                    Else
                        cmbGL.EditValue = Nothing
                    End If

                    txtPieceNo.Text = dt.Rows(0)("NoPiece").ToString()
                    If dt.Rows(0)("NoPiece").ToString() <> "1" Then
                        txtPieceNo.Visible = True
                        cmbType.SelectedIndex = 1
                        LayoutControlItemNoOfPieces.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Else
                        txtPieceNo.Visible = False
                        LayoutControlItemNoOfPieces.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        cmbType.SelectedIndex = 0
                    End If
                    txtSerialNo.Text = dt.Rows(0)("SerailNo").ToString()

                    txtDiscount.Text = dt.Rows(0)("Discount").ToString()
                    txtAstDesc.Text = dt.Rows(0)("AstDesc").ToString()
                    txtAstDesc2.Text = dt.Rows(0)("AstDesc2").ToString()
                    txtAstModel.Text = dt.Rows(0)("AstModel").ToString()
                    txtRemarks.Text = dt.Rows(0)("TransRemarks").ToString()
                    txtOldAssetID.Text = dt.Rows(0)("OldAssetID").ToString()
                    txtLabelPrintedCnt.Text = dt.Rows(0)("LabelCount").ToString()
                    txtBarCode.Text = dt.Rows(0)("BarCode").ToString()
                    dtService.EditValue = CDate(dt.Rows(0)("SrvDate").ToString)
                    txtCAPEX.Text = dt.Rows(0)("Capex").ToString()
                    txtPOERP.Text = dt.Rows(0)("PoErp").ToString()
                    txtPlate.Text = dt.Rows(0)("Plate").ToString()
                    txtGRN.Text = dt.Rows(0)("GRN").ToString()
                    txtRefCode.Text = dt.Rows(0)("RefCode").ToString()

                    If Not dt.Rows(0).IsNull("CapitalizationDate") Then
                        dtCapitalizationDate.EditValue = dt.Rows(0)("CapitalizationDate")
                    Else
                        dtCapitalizationDate.Text = Nothing
                    End If
                    txtBussinessArea.Text = dt.Rows(0)("BussinessArea").ToString()
                    txtInventoryNumber.Text = dt.Rows(0)("InventoryNumber").ToString()
                    txtCreatedBY.Text = dt.Rows(0)("CreatedBY").ToString()
                    If Not dt.Rows(0).IsNull("InStockAsset") Then
                        chkAssetWithValue.Checked = Not dt.Rows(0)("InStockAsset")
                    Else
                        chkAssetWithValue.Checked = False
                    End If
                    txtEvaGroup1.Text = dt.Rows(0)("EvaluationGroup1").ToString()
                    txtEvaGroup2.Text = dt.Rows(0)("EvaluationGroup2").ToString()
                    txtEvaGroup3.Text = dt.Rows(0)("EvaluationGroup3").ToString()
                    txtEvaGroup4.Text = dt.Rows(0)("EvaluationGroup4").ToString()

                    txtCustomField1.Text = dt.Rows(0)("CustomFld1").ToString()
                    txtCustomField2.Text = dt.Rows(0)("CustomFld2").ToString()
                    txtCustomField3.Text = dt.Rows(0)("CustomFld3").ToString()
                    txtCustomField4.Text = dt.Rows(0)("CustomFld4").ToString()
                    txtCustomField5.Text = dt.Rows(0)("CustomFld5").ToString()
                    txtWarranty.Value = dt.Rows(0)("Warranty").ToString()

                    txtSales.Text = Format(CDbl(dt.Rows(0)("Tax").ToString()), "###,###,###,###,###.00")
                    txtTotal.Text = Format(CType(txtbase.Text, Double) + CType(txtSales.Text, Double), "###,###,###,###,###.00")

                    PreviousItemCost = CType(txtbase.Text.Trim, Decimal) + CType(txtSales.Text.Trim, Decimal)

                    'BookId = dt.Rows(0)("BookID").ToString()
                    txtRef.Text = dt.Rows(0)("RefNo").ToString()
                    txtAstNum.Text = dt.Rows(0)("AstNum").ToString()

                    'cmbAssets.EditValue = dt.Rows(0)("AstID").ToString()


                    dtpur.EditValue = CDate(dt.Rows(0)("PurDate").ToString())



                    If (dt.Rows(0)("POCOde").ToString = "") Then
                        txtDODesc.Text = ""
                        txtDoProjectCode.Text = ""
                    End If

                    If (dt.Rows(0)("LocId").ToString <> "") Then
                        cmbLocation.EditValue = dt.Rows(0)("LocId")
                        'ZTLocation.SetValue(dt.Rows(0)("LocId").ToString(), objBALLocation.Comp_Path(dt.Rows(0)("LocId").ToString()))
                    Else
                        cmbLocation.EditValue = Nothing
                        'ZTLocation.SelectedText = ""
                        'ZTLocation.SelectedValue = ""
                    End If

                    'when item is disposed
                    If CType(dt.Rows(0)("Disposed"), Boolean) Then

                        cmbDisposalMethod.Enabled = True
                        dtDispdate.Enabled = True
                        cmbDisposalMethod.EditValue = dt.Rows(0)("Dispcode")
                        chkDisposed.Checked = True
                        If dt.Rows(0)("DispDate").ToString() <> "" Then dtDispdate.EditValue = CDate(dt.Rows(0)("DispDate"))
                        If dt.Rows(0)("Dispcode") = "1" Then
                            cmbDisposalMethod.Enabled = False
                            Panel1.Visible = True
                            If dt.Rows(0)("Sel_date").ToString() <> "" Then dtSaleDate.EditValue = CDate(dt.Rows(0)("Sel_date"))
                            If dt.Rows(0)("Sel_Price").ToString() <> "" Then txtPrice.Text = Format(CDbl(dt.Rows(0)("Sel_Price")), "###,###,###,###,###.00")
                            If dt.Rows(0)("Soldto").ToString() <> "" Then txtSoldto.Text = dt.Rows(0)("Soldto")
                        Else
                            dtSaleDate.EditValue = Now.Date
                            txtPrice.Text = ""
                            txtSoldto.Text = ""
                            Panel1.Visible = False
                        End If
                        'when item is NOT disposed 
                    Else

                        chkDisposed.Checked = False
                        cmbDisposalMethod.EditValue = Nothing
                        cmbDisposalMethod.Enabled = False
                        dtDispdate.Enabled = False
                        Panel1.Visible = False
                        Panel1.Refresh()
                    End If

                    Get_AstBooks(strAstID)
                    grdBooksView_FocusedRowChanged(Nothing, Nothing) '

                    Get_Ast_History(strAstID)
                    Get_Ast_History_Custodian(strAstID)
                    Get_Ast_TransHistory(strAstID)
                    Get_AssetAddCosts(strAstID)
                    Dim addcostcnt As Integer = grdAddCostView.RowCount
                    btnNewAddCost_Click(Nothing, Nothing)
                    btnSaveAddCost.Enabled = True
                    btnNewAddCost.Enabled = True

                    If (Date.Compare(dtService.EditValue.ToShortDateString, dtBVUpdate.EditValue.ToShortDateString) < 0) Then
                        txtSales.Properties.ReadOnly = True
                        txtbase.Properties.ReadOnly = True
                        dtService.Enabled = False
                    Else
                        txtbase.Properties.ReadOnly = False
                        dtService.Enabled = True
                        'to don't allow the user to modify salestextbox after adding additional cost on grid.
                        If addcostcnt > 0 Then
                            txtSales.Properties.ReadOnly = True
                        Else
                            txtSales.Properties.ReadOnly = False
                        End If
                    End If


                    LoadImage()
                    'isEdit = True
                    btnDelete.Enabled = True
                    btnCopy.Enabled = True
                    btnPrintLabel.Enabled = True

                    Dim tot, book As String
                    If txtTotal.Text <> "" Then
                        tot = txtTotal.Text
                    Else
                        tot = "0"
                    End If

                    If txtBookVal.Text <> "" Then
                        book = txtBookVal.Text
                    Else
                        book = "0"
                    End If
                    txtAccDep.Text = CDbl(tot - book)
                End If
                valProvMain.Validate()
                valProvNotNigative.Validate()
                valProvSecondary.Validate()
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub chkDisposed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDisposed.CheckedChanged
        Try
            If chkDisposed.Checked Then
                cmbDisposalMethod.Enabled = True
                dtDispdate.Enabled = True
                If cmbDisposalMethod.EditValue = "1" Then
                    Panel1.Visible = True
                End If
            Else
                cmbDisposalMethod.Enabled = False
                dtDispdate.Enabled = False
                Panel1.Visible = False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub btnUpdateBook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateBook.Click
        If valProvSecondary.Validate And valProvNotNigative.Validate Then
            Dim intVal As Integer
            txtSalPercent.Text = CalculatePercent(txtSalYr.Text, txtSalMonth.Text)
            intVal = CInt(txtSalPercent.Text)
            If intVal < 0 Or intVal > 100 Then
                errProv.SetError(txtSalPercent, My.Resources.Messages.OutPercentRange)
                Return
            Else
                errProv.ClearErrors()
            End If

            Update_DepreciationBook(True)
        End If
    End Sub



    Public Sub Create_Copies(ByVal numofCop As Integer, ByVal AstID As String)

        'get the count of range still in the range.

        'compare with numofcop 

        Try
            ' First we get the values from the database into an object, then we make copies of this object
            Dim objattAssetDetailsold As New attAssetDetails
            Dim objBALAddCostHistory As New BALAddCostHistory

            objattAssetDetailsold.PKeyCode = AstID
            Dim objBALAssetDetails As New BALAssetDetails
            Dim dt As DataTable = objBALAssetDetails.GetAsset_Details(objattAssetDetailsold)

            Dim objBALAst_INV_Schedule As New BALAst_INV_Schedule
            Dim dtInvSch As DataTable = objBALAst_INV_Schedule.Getcombo_Schedule()
            Dim objattAstHistory As attAstHistory
            Dim objBALAst_History As New BALAst_History

            Dim objattAssetDetails As New attAssetDetails
            objattAssetDetails.CompanyID = CInt(dt.Rows(0)("CompanyID"))
            objattAssetDetails.BaseCost = CDbl(dt.Rows(0)("BaseCost"))
            objattAssetDetails.CustodianID = dt.Rows(0)("CustodianID").ToString

            objattAssetDetails.InsID = CInt(dt.Rows(0)("InsID"))
            objattAssetDetails.SuppID = dt.Rows(0)("SuppID").ToString
            objattAssetDetails.POCode = dt.Rows(0)("POCode").ToString


            'objattAssetDetails.SerailNo = dt.Rows(0)("SerailNo").ToString()
            objattAssetDetails.InvNumber = dt.Rows(0)("InvNumber").ToString
            objattAssetDetails.AstBrandID = dt.Rows(0)("AstBrandID").ToString
            objattAssetDetails.AstDesc = dt.Rows(0)("Assetdetailsdesc1").ToString
            objattAssetDetails.AstDesc2 = dt.Rows(0)("Assetdetailsdesc2").ToString
            objattAssetDetails.Discount = CDbl(dt.Rows(0)("Discount"))
            objattAssetDetails.AstModel = dt.Rows(0)("AstModel").ToString
            objattAssetDetails.PurDate = CDate(dt.Rows(0)("PurDate"))
            objattAssetDetails.NoPiece = CInt(dt.Rows(0)("NoPiece"))
            objattAssetDetails.Tax = CDbl(dt.Rows(0)("Tax"))
            objattAssetDetails.DispDate = Date.MinValue

            objattAssetDetails.SrvDate = CDate(dt.Rows(0)("SrvDate"))

            objattAssetDetails.LocID = dt.Rows(0)("LocID").ToString()
            objattAssetDetails.Disposed = False
            objattAssetDetails.IsSold = False
            objattAssetDetails.IsDataChanged = True

            If CBool(dt.Rows(0)("Disposed")) Then
                objattAssetDetails.DispCode = CInt(dt.Rows(0)("DispCode"))
                objattAssetDetails.DispDate = CDate(dt.Rows(0)("DispDate"))
                objattAssetDetails.Disposed = True

                If objattAssetDetails.DispCode = 0 Then
                    objattAssetDetails.IsSold = True
                    objattAssetDetails.Sel_Date = CDate(dt.Rows(0)("Sel_Date"))
                    objattAssetDetails.Sel_Price = CDbl(dt.Rows(0)("Sel_Price"))
                    objattAssetDetails.SoldTo = dt.Rows(0)("SoldTo").ToString
                End If
            End If

            objattAssetDetails.ItemCode = dt.Rows(0)("ItemCode").ToString()

            If Not DBNull.Value.Equals(dt.Rows(0)("GLCode")) Then
                objattAssetDetails.GLCode = CInt(dt.Rows(0)("GLCode"))
            End If

            Dim objBALCategory As New BALCategory
            Dim AstCatID As String = objBALAssetDetails.Get_Assets_AstCatID(dt.Rows(0)("ItemCode").ToString())

            Dim CategoryText As String = objBALCategory.Comp_Path(AstCatID)

            Dim objBalLocation As New BALLocation
            Dim LocationText As String = objBalLocation.Comp_Path(dt.Rows(0)("LocID").ToString())



            Dim strAstID As String = ""
            Dim strAstNum As String = ""
            Dim NumSucceededCopies As Integer = 0

            FrmCop.pb.Maximum = numofCop
            FrmCop.pb.Step = 1
            For idx As Integer = 0 To numofCop - 1
                strAstID = objBALAssetDetails.Generate_AssetID()
                strAstNum = Generate_AssetNumber(objattAssetDetails.CompanyID)
                If strAstNum = "" Then
                    Exit For
                End If
                objattAssetDetails.PKeyCode = strAstID
                objattAssetDetails.AstNum = strAstNum
                objattAssetDetails.BarCode = Generate_BarCode(objattAssetDetails.CompanyID, Trim(strAstID), Trim(strAstNum), objattAssetDetails.RefNo, CategoryText, CategoryText, LocationText, LocationText)
                objattAssetDetails.RefNo = dt.Rows(0)("RefNo").ToString()
                If objattAssetDetails.RefNo = "" Then
                    objattAssetDetails.RefNo = objattAssetDetails.SerailNo
                Else
                    objattAssetDetails.RefNo = objattAssetDetails.RefNo & "-" & (idx + 1).ToString
                End If
                objattAssetDetails.CreatedBY = AppConfig.LoginName
                objattAssetDetails.LastEditBY = AppConfig.LoginName
                objattAssetDetails.LastEditDate = Now.Date
                objattAssetDetails.CreationDate = Now.Date

                objattAssetDetails.BussinessArea = dt.Rows(0)("BussinessArea").ToString()
                objattAssetDetails.InventoryNumber = dt.Rows(0)("InventoryNumber").ToString()
                objattAssetDetails.CostCenterID = dt.Rows(0)("CostCenterID").ToString()

                objattAssetDetails.EvaluationGroup1 = dt.Rows(0)("EvaluationGroup1").ToString()
                objattAssetDetails.EvaluationGroup2 = dt.Rows(0)("EvaluationGroup2").ToString()
                objattAssetDetails.EvaluationGroup3 = dt.Rows(0)("EvaluationGroup3").ToString()
                objattAssetDetails.EvaluationGroup4 = dt.Rows(0)("EvaluationGroup4").ToString()

                objattAssetDetails.CustomFld1 = dt.Rows(0)("CustomFld1").ToString()
                objattAssetDetails.CustomFld2 = dt.Rows(0)("CustomFld2").ToString()
                objattAssetDetails.CustomFld3 = dt.Rows(0)("CustomFld3").ToString()
                objattAssetDetails.CustomFld4 = dt.Rows(0)("CustomFld4").ToString()
                objattAssetDetails.CustomFld5 = dt.Rows(0)("CustomFld5").ToString()
                objattAssetDetails.Warranty = dt.Rows(0)("Warranty").ToString()
                objattAssetDetails.StatusID = 1

                objBALAssetDetails.Insert_AssetDetails(objattAssetDetails, True)

                '3.	When coping asset details it should copy the additional information also…
                objBALAddCostHistory.CopyAddCostHistory(AstID, strAstID)
                '3.	When coping the assets we should copy the images also..
                CopyAssetImage(AstID, strAstID)


                Dim objBALAssets As New BALItems
                Dim ds As DataTable = objBALAssets.Get_DepPolicy(objattAssetDetails.ItemCode)
                If Not ds Is Nothing Then
                    If ds.Rows.Count > 0 Then
                        objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, ds.Rows(0)("SalvageValue"), ds.Rows(0)("SalvageYear"), ds.Rows(0)("SalvageMonth"), dtService.EditValue, ds.Rows(0)("IsSalvageValuePercent"))
                    End If
                End If

                'Create history 
                For Each dr As DataRow In dtInvSch.Rows
                    objattAstHistory = New attAstHistory
                    objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                    objattAstHistory.AstID = strAstID
                    objattAstHistory.Status = 0
                    objattAstHistory.InvSchCode = dr("InvSchCode")
                    objattAstHistory.HisDate = DateTime.Now.Date
                    objattAstHistory.Fr_loc = dt.Rows(0)("LocID")
                    objattAstHistory.To_Loc = dt.Rows(0)("LocID")
                    objattAstHistory.NoPiece = dt.Rows(0)("NoPiece")
                    objBALAst_History.Insert_Ast_History(objattAstHistory)
                Next

                Application.DoEvents()
                FrmCop.pb.PerformStep()
                NumSucceededCopies += 1
            Next
            Dim NumFailedCopies As Integer = numofCop - NumSucceededCopies
            MessageBox.Show(CStr(NumSucceededCopies) & " Assets added successfully," & Chr(13) _
                             & CStr(NumFailedCopies) & " Assets failed to add.", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub DeleteImage()
        Dim fname As String = ""
        If AppConfig.ImgStorgeLoc = "Shared Folder" Then

            If AppConfig.ImgType = "Asset Images" Then
                fname = txtAssetID.EditValue & ".jpg"
            ElseIf AppConfig.ImgType = "Item Images" Then
                fname = cmbItemCode.EditValue & ".jpg"
            End If

            If File.Exists(AppConfig.ImgPath & "\" & fname) Then
                File.Delete(AppConfig.ImgPath & "\" & fname)
            End If
        ElseIf AppConfig.ImgStorgeLoc = "Database" Then
            If AppConfig.ImgType = "Asset Images" Then
                objattAssetDetails = New attAssetDetails
                objattAssetDetails.PKeyCode = txtAssetID.EditValue
                objBALAssetDetails.DeleteAssetImage(objattAssetDetails)
            ElseIf AppConfig.ImgType = "Item Images" Then
                objattAssets = New attItems
                objattAssets.PKeyCode = cmbItemCode.EditValue
                objBALAssets.DeleteItemImage(objattAssets)
            End If
        End If
        LayoutControlItemDeleteImage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        picAssets.Image = Nothing
    End Sub
    Private Sub LoadImage()
        Try
            Dim fname As String = ""
            If AppConfig.ImgStorgeLoc = "Shared Folder" Then
                If AppConfig.ImgType = "Asset Images" Then
                    fname = txtAssetID.EditValue & ".jpg"

                ElseIf AppConfig.ImgType = "Item Images" Then
                    fname = cmbItemCode.EditValue & ".jpg"
                End If
                If File.Exists(AppConfig.ImgPath & "\" & fname) Then
                    picAssets.Image = Image.FromFile(AppConfig.ImgPath & "\" & fname)
                    picAssets.Tag = AppConfig.ImgPath & "\" & fname

                    LayoutControlItemDeleteImage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    LayoutControlItemDeleteImage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    picAssets.Image = Nothing
                    picAssets.Tag = Nothing
                End If

            ElseIf AppConfig.ImgStorgeLoc = "Database" Then
                If AppConfig.ImgType = "Asset Images" Then
                    objattAssetDetails = New attAssetDetails
                    objattAssetDetails.PKeyCode = txtAssetID.EditValue
                    Dim bits As Byte() = objBALAssetDetails.GetAssetImage(objattAssetDetails)
                    If bits IsNot Nothing Then
                        Dim ms As New MemoryStream(bits, 0, bits.Length)
                        picAssets.Image = Image.FromStream(ms, True)
                        LayoutControlItemDeleteImage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Else
                        LayoutControlItemDeleteImage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        picAssets.Image = Nothing
                        picAssets.Tag = Nothing
                    End If
                ElseIf AppConfig.ImgType = "Item Images" Then
                    objattAssets = New attItems
                    objattAssets.PKeyCode = cmbItemCode.EditValue
                    Dim bits As Byte() = objBALAssets.GetItemImage(objattAssets)
                    If bits IsNot Nothing Then
                        Dim ms As New MemoryStream(bits, 0, bits.Length)
                        picAssets.Image = Image.FromStream(ms, True)
                        LayoutControlItemDeleteImage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Else
                        LayoutControlItemDeleteImage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        picAssets.Image = Nothing
                        picAssets.Tag = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub update_AssetDetails()
        Try
            txtRef.Text = RemoveUnnecessaryChars(txtSerialNo.Text)
            If Not objBALAssetDetails.Check_referenceID(txtRef.Text.ToString(), txtAssetID.EditValue) Then
                objattAssetDetails = New attAssetDetails
                objattAssetDetails.BaseCost = txtbase.Text
                objattAssetDetails.CustodianID = cmbCustodian.EditValue

                If cmbInsurer.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(cmbInsurer.EditValue) Then
                    objattAssetDetails.InsID = cmbInsurer.EditValue
                End If

                If cmbSupplier.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(cmbSupplier.EditValue) Then
                    objattAssetDetails.SuppID = cmbSupplier.EditValue
                End If

                If cmbPOCode.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(cmbPOCode.EditValue) Then
                    objattAssetDetails.POCode = cmbPOCode.EditValue
                End If
                objattAssetDetails.SerailNo = RemoveUnnecessaryChars(txtSerialNo.Text)
                objattAssetDetails.InvNumber = ""
                objattAssetDetails.ItemCode = RemoveUnnecessaryChars(cmbItemCode.EditValue)
                objattAssetDetails.PurDate = dtpur.EditValue
                objattAssetDetails.CompanyID = cmbCompany.EditValue
                objattAssetDetails.TransRemarks = RemoveUnnecessaryChars(txtRemarks.Text)
                objattAssetDetails.Tax = txtSales.Text
                objattAssetDetails.SrvDate = dtService.EditValue
                objattAssetDetails.AstBrandID = cmbBrand.EditValue
                objattAssetDetails.StatusID = cmbAssetsStatus.EditValue

                objattAssetDetails.AstDesc = RemoveUnnecessaryChars(txtAstDesc.Text)
                objattAssetDetails.AstDesc2 = RemoveUnnecessaryChars(txtAstDesc2.Text)
                objattAssetDetails.AstModel = RemoveUnnecessaryChars(txtAstModel.Text)
                objattAssetDetails.Discount = RemoveUnnecessaryChars(txtDiscount.Text)
                objattAssetDetails.Capex = RemoveUnnecessaryChars(txtCAPEX.Text)
                objattAssetDetails.PoErp = RemoveUnnecessaryChars(txtPOERP.Text)
                objattAssetDetails.Plate = RemoveUnnecessaryChars(txtPlate.Text)
                objattAssetDetails.GRN = RemoveUnnecessaryChars(txtGRN.Text)
                objattAssetDetails.RefCode = RemoveUnnecessaryChars(txtRefCode.Text)
                objattAssetDetails.GLCode = cmbGL.EditValue
                objattAssetDetails.PONumber = ""

                If cmbCostCenter.EditValue <> Nothing Then
                    objattAssetDetails.CostCenterID = cmbCostCenter.EditValue
                Else
                    objattAssetDetails.CostCenterID = String.Empty
                End If
                If dtCapitalizationDate.EditValue <> Nothing Then
                    objattAssetDetails.CapitalizationDate = dtCapitalizationDate.EditValue
                Else
                    objattAssetDetails.CapitalizationDate = Nothing
                End If
                objattAssetDetails.BussinessArea = txtBussinessArea.Text
                objattAssetDetails.InventoryNumber = txtInventoryNumber.Text
                objattAssetDetails.InStockAsset = Not chkAssetWithValue.Checked
                objattAssetDetails.IsDataChanged = True

                objattAssetDetails.CreatedBY = txtCreatedBY.Text
                'Custom Fields
                objattAssetDetails.EvaluationGroup1 = txtEvaGroup1.Text
                objattAssetDetails.EvaluationGroup2 = txtEvaGroup2.Text
                objattAssetDetails.EvaluationGroup3 = txtEvaGroup3.Text
                objattAssetDetails.EvaluationGroup4 = txtEvaGroup4.Text
                objattAssetDetails.CustomFld1 = txtCustomField1.Text
                objattAssetDetails.CustomFld2 = txtCustomField2.Text
                objattAssetDetails.CustomFld3 = txtCustomField3.Text
                objattAssetDetails.CustomFld4 = txtCustomField4.Text
                objattAssetDetails.CustomFld5 = txtCustomField5.Text
                objattAssetDetails.Warranty = txtWarranty.Value


                If txtPieceNo.Visible = True Then
                    objattAssetDetails.NoPiece = txtPieceNo.Text
                Else
                    objattAssetDetails.NoPiece = 1
                End If

                objattAssetDetails.LocID = cmbLocation.EditValue

                objattAssetDetails.RefNo = RemoveUnnecessaryChars(txtRef.Text)
                objattAssetDetails.AstNum = txtAstNum.Text.ToString()

                If chkDisposed.Checked Then
                    If cmbDisposalMethod.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(cmbDisposalMethod.EditValue) Then

                        objattAssetDetails.DispCode = cmbDisposalMethod.EditValue
                        If Trim(dtDispdate.Text) = "" Then

                            objattAssetDetails.DispDate = Nothing
                        Else

                            objattAssetDetails.DispDate = dtDispdate.EditValue
                        End If

                        objattAssetDetails.Disposed = True
                        objBALAssetDetails.DisposeAsset_Book(txtAssetID.EditValue)
                        If objattAssetDetails.DispCode = "1" Then
                            objattAssetDetails.IsSold = True
                            objattAssetDetails.Sel_Date = dtSaleDate.EditValue
                            objattAssetDetails.Sel_Price = txtPrice.Text
                            objattAssetDetails.SoldTo = txtSoldto.Text
                        End If
                    End If
                End If

                objattAssetDetails.PKeyCode = txtAssetID.EditValue
                'txtBarCode.Text = Generate_BarCode(Trim(cmbCompany.EditValue), Trim(txtAssetID.EditValue), Trim(txtAstNum.Text), Trim(txtRef.Text), Trim(txtCategory.Text), Trim(txtCategory.Text), Trim(ZTLocation.SelectedText), Trim(ZTLocation.SelectedText))
                txtBarCode.Text = Generate_BarCode(Trim(cmbCompany.EditValue), Trim(txtAssetID.EditValue), Trim(txtAstNum.Text), Trim(txtRef.Text), Trim(txtCategory.Text), Trim(txtCategory.Text), Trim(cmbLocation.Text), Trim(cmbLocation.Text))
                objattAssetDetails.BarCode = txtBarCode.Text
                objattAssetDetails.LastEditBY = AppConfig.LoginName
                objattAssetDetails.LastEditDate = Now.Date

                objBALAssetDetails.Update_AssetDetails(objattAssetDetails)

                Update_DepreciationBook(False)
                PreviousItemCost = CType(txtbase.Text.Trim, Decimal) + CType(txtSales.Text.Trim, Decimal)

                ShowInfoMessage(My.Resources.Messages.Saved)
            Else
                txtSerialNo.Focus()
                txtSerialNo.Text = ""
                ShowErrorMessage("Serial Number is already exists !")
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub AddNew_AssetDetails()
        Try
            txtRef.Text = RemoveUnnecessaryChars(txtSerialNo.Text)
            If Not objBALAssetDetails.Check_referenceID(txtRef.Text.ToString(), "") Then

                objattAssetDetails = New attAssetDetails
                objattAssetDetails.BaseCost = txtbase.Text
                objattAssetDetails.CustodianID = cmbCustodian.EditValue

                If cmbInsurer.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(cmbInsurer.EditValue) Then
                    objattAssetDetails.InsID = cmbInsurer.EditValue
                End If

                If cmbSupplier.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(cmbSupplier.EditValue) Then
                    objattAssetDetails.SuppID = cmbSupplier.EditValue
                End If

                If cmbPOCode.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(cmbPOCode.EditValue) Then
                    objattAssetDetails.POCode = cmbPOCode.EditValue
                End If

                objattAssetDetails.SerailNo = RemoveUnnecessaryChars(txtSerialNo.Text)
                objattAssetDetails.Discount = RemoveUnnecessaryChars(txtDiscount.Text)
                objattAssetDetails.InvNumber = ""
                objattAssetDetails.ItemCode = cmbItemCode.EditValue
                objattAssetDetails.PurDate = dtpur.EditValue
                objattAssetDetails.Tax = RemoveUnnecessaryChars(txtSales.Text)
                objattAssetDetails.TransRemarks = RemoveUnnecessaryChars(txtRemarks.Text)
                objattAssetDetails.DispDate = Date.MinValue
                objattAssetDetails.CompanyID = cmbCompany.EditValue
                objattAssetDetails.SrvDate = dtService.EditValue
                objattAssetDetails.RefNo = RemoveUnnecessaryChars(txtRef.Text)
                ' TODO: check generating the asset number without the option:
                'If AppConfig.CodingMode Then
                txtAstNum.Text = Generate_AssetNumber(cmbCompany.EditValue)
                'End If
                objattAssetDetails.AstNum = RemoveUnnecessaryChars(txtAstNum.Text)
                objattAssetDetails.AstBrandID = cmbBrand.EditValue
                objattAssetDetails.StatusID = cmbAssetsStatus.EditValue
                objattAssetDetails.AstDesc = RemoveUnnecessaryChars(txtAstDesc.Text)
                objattAssetDetails.AstDesc2 = RemoveUnnecessaryChars(txtAstDesc2.Text)
                objattAssetDetails.AstModel = RemoveUnnecessaryChars(txtAstModel.Text)
                objattAssetDetails.Capex = RemoveUnnecessaryChars(txtCAPEX.Text)
                objattAssetDetails.PoErp = RemoveUnnecessaryChars(txtPOERP.Text)
                objattAssetDetails.Plate = RemoveUnnecessaryChars(txtPlate.Text)
                objattAssetDetails.GRN = RemoveUnnecessaryChars(txtGRN.Text)
                objattAssetDetails.RefCode = RemoveUnnecessaryChars(txtRefCode.Text)
                objattAssetDetails.GLCode = cmbGL.EditValue
                objattAssetDetails.PONumber = ""

                objattAssetDetails.CostCenterID = cmbCostCenter.EditValue
                If dtCapitalizationDate.EditValue <> Nothing Then
                    objattAssetDetails.CapitalizationDate = dtCapitalizationDate.EditValue
                Else
                    objattAssetDetails.CapitalizationDate = Nothing
                End If
                objattAssetDetails.BussinessArea = txtBussinessArea.Text
                objattAssetDetails.InventoryNumber = txtInventoryNumber.Text
                objattAssetDetails.CreatedBY = txtCreatedBY.Text
                objattAssetDetails.InStockAsset = Not chkAssetWithValue.Checked
                objattAssetDetails.IsDataChanged = True

                objattAssetDetails.LocID = cmbLocation.EditValue
                'objattAssetDetails.LocID = ZTLocation.SelectedValue
                If txtPieceNo.Visible = True Then
                    objattAssetDetails.NoPiece = txtPieceNo.Text
                Else
                    objattAssetDetails.NoPiece = 1
                End If

                objattAssetDetails.Disposed = False
                objattAssetDetails.IsSold = False
                objattAssetDetails.PKeyCode = txtAssetID.EditValue

                If chkDisposed.Checked Then
                    If cmbDisposalMethod.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(cmbDisposalMethod.EditValue) Then
                        objBALAssetDetails.DisposeAsset_Book(txtAssetID.EditValue)
                        objattAssetDetails.DispCode = cmbDisposalMethod.EditValue
                        objattAssetDetails.DispDate = dtDispdate.EditValue
                        objattAssetDetails.Disposed = 1

                        If objattAssetDetails.DispCode = "1" Then
                            objattAssetDetails.IsSold = 1
                            objattAssetDetails.Sel_Date = dtSaleDate.EditValue
                            objattAssetDetails.Sel_Price = txtPrice.Text
                            objattAssetDetails.SoldTo = txtSoldto.Text
                        End If
                    End If
                End If

                txtBarCode.Text = Generate_BarCode(Trim(cmbCompany.EditValue), Trim(txtAssetID.EditValue), Trim(txtAstNum.Text), Trim(txtRef.Text), Trim(txtCategory.Text), Trim(txtCategory.Text), Trim(cmbLocation.Text), Trim(cmbLocation.Text))
                objattAssetDetails.BarCode = txtBarCode.Text

                objattAssetDetails.CreatedBY = AppConfig.LoginName
                objattAssetDetails.LastEditBY = AppConfig.LoginName
                objattAssetDetails.LastEditDate = Now.Date
                objattAssetDetails.CreationDate = Now.Date

                'Custom Fields
                objattAssetDetails.EvaluationGroup1 = txtEvaGroup1.Text
                objattAssetDetails.EvaluationGroup2 = txtEvaGroup2.Text
                objattAssetDetails.EvaluationGroup3 = txtEvaGroup3.Text
                objattAssetDetails.EvaluationGroup4 = txtEvaGroup4.Text
                objattAssetDetails.CustomFld1 = txtCustomField1.Text
                objattAssetDetails.CustomFld2 = txtCustomField2.Text
                objattAssetDetails.CustomFld3 = txtCustomField3.Text
                objattAssetDetails.CustomFld4 = txtCustomField4.Text
                objattAssetDetails.CustomFld5 = txtCustomField5.Text
                objattAssetDetails.Warranty = txtWarranty.Value

                objattAssetDetails.StatusID = 1

                If objBALAssetDetails.Insert_AssetDetails(objattAssetDetails, True) Then

                    'Create history for all inventory schedules.
                    Dim objBALAst_INV_Schedule As New BALAst_INV_Schedule
                    Dim dtInvSch As DataTable = objBALAst_INV_Schedule.Getcombo_Schedule()
                    Dim objattAstHistory As attAstHistory
                    Dim objBALAst_History As New BALAst_History
                    For Each dr As DataRow In dtInvSch.Rows
                        objattAstHistory = New attAstHistory
                        objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                        objattAstHistory.AstID = txtAssetID.EditValue
                        objattAstHistory.Status = 0
                        objattAstHistory.InvSchCode = dr("InvSchCode")
                        objattAstHistory.HisDate = DateTime.Now.Date
                        'objattAstHistory.Fr_loc = ZTLocation.SelectedValue
                        'objattAstHistory.To_Loc = ZTLocation.SelectedValue
                        objattAstHistory.Fr_loc = cmbLocation.EditValue
                        objattAstHistory.To_Loc = cmbLocation.EditValue
                        objattAstHistory.NoPiece = txtPieceNo.Value
                        objBALAst_History.Insert_Ast_History(objattAstHistory)
                    Next

                End If
                isEdit = True

                Dim ESalValue As Decimal = 0
                Dim ESalMonth As Decimal = 0
                Dim ESalYear As Decimal = 0
                Dim EDepValue As Decimal = 0

                Dim objBALAssets As New BALItems
                Dim ds As DataTable = objBALAssets.Get_DepPolicy(objattAssetDetails.ItemCode)
                If Not ds Is Nothing Then
                    If ds.Rows.Count > 0 Then
                        objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, ds.Rows(0)("SalvageValue"), ds.Rows(0)("SalvageYear"), ds.Rows(0)("SalvageMonth"), dtService.EditValue, ds.Rows(0)("IsSalvageValuePercent"))
                        ESalValue = ds.Rows(0)("SalvageValue")
                        ESalMonth = ds.Rows(0)("SalvageMonth")
                        ESalYear = ds.Rows(0)("SalvageYear")
                    End If
                End If


                Get_AstBooks(txtAssetID.EditValue)
                txtDepAstID.Text = txtAssetID.EditValue
                grdFiscalYRBook.DataSource = Nothing

                If AnonymousId = "" Then
                    Me.Search_Asset(txtAssetID.EditValue)
                Else 'FormOpened from Anonymous in Data Processing
                    'set this flag to delete the asset from Anonymous data after closing the form.
                    AnonymousSaved = True
                    Dim objattAstHistory As attAstHistory
                    objattAstHistory = New attAstHistory()
                    Dim objBALAst_History As New BALAst_History
                    objattAstHistory.AstID = txtAssetID.EditValue
                    objattAstHistory.Status = 5
                    objattAstHistory.InvSchCode = InvScheduleId
                    objattAstHistory.HisDate = TransDate
                    objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                    objattAstHistory.Fr_loc = cmbLocation.EditValue
                    objattAstHistory.To_Loc = cmbLocation.EditValue
                    objBALAst_History.Insert_Ast_History(objattAstHistory)

                    objattAssetDetails = New attAssetDetails
                    objattAssetDetails.PKeyCode = txtAssetID.EditValue
                    objattAssetDetails.InvStatus = 5 'Means Anonymous
                    objattAssetDetails.InvSchCode = InvScheduleId
                    objattAssetDetails.LocID = cmbLocation.EditValue
                    objattAssetDetails.IsDataChanged = True
                    objBALAssetDetails.Update_InvStatus(objattAssetDetails)
                End If
                ShowInfoMessage(My.Resources.Messages.Saved)
            Else
                txtSerialNo.Focus()
                txtSerialNo.Text = ""
                ShowErrorMessage("Serial Number is already exists !")
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub Get_AddCostType()
        Try
            Dim ds As New DataTable
            Dim objBALAddCostType As New BALAddCostType
            ds = objBALAddCostType.GetAll_AddCostType(New attAddCostType)
            If ds Is Nothing = False Then
                cmbAddCostType.DataSource = ds
                cmbAddCostType.ValueMember = "TypeID"
                cmbAddCostType.DisplayMember = "TypeDesc"
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub txtbase_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtbase.TextChanged, txtSales.TextChanged
        Try
            txtTotal.Text = CType(txtbase.Text, Double) + CType(txtSales.Text, Double)
        Catch ex As Exception
            txtSales.Text = "0"
            txtbase.Text = "0"
        End Try
    End Sub



    Private Sub txtBookVal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBookVal.TextChanged
        Dim tot, book As String
        If txtTotal.Text <> "" Then
            tot = txtTotal.Text
        Else
            tot = "0"
        End If

        If txtBookVal.Text <> "" Then
            book = txtBookVal.Text
        Else
            book = "0"
        End If

        If tot - book < 0 Then
            errProv.SetError(txtBookVal, My.Resources.Messages.InvalidCBV)
            'ZulMessageBox.ShowMe("InvalidCBV")
            txtBookVal.Text = "0"
            txtBookVal.Focus()
        Else
            errProv.ClearErrors()
            txtAccDep.Text = tot - book
        End If

    End Sub


    Private Sub txtTotal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotal.TextChanged
        txtTotal2.Text = txtTotal.Text
    End Sub

    Private Sub cmbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbType.SelectedIndexChanged
        If cmbType.SelectedIndex = 0 Then
            LayoutControlItemNoOfPieces.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            txtPieceNo.Visible = False
            txtPieceNo.Value = 1
        Else
            LayoutControlItemNoOfPieces.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            txtPieceNo.Visible = True
        End If
    End Sub

    Private Function Get_AssetsDetails_Pre_Next(ByVal AstNum As String, ByVal CmID As String, ByVal Pre_Next As Int16) As String
        '1 for Previous, 2 for Next
        Try
            Dim ds As New DataTable

            objattAssetDetails = New attAssetDetails

            If AstNum <> "" Then
                objattAssetDetails.AstNum = AstNum
            Else
                objattAssetDetails.AstNum = 0
            End If

            If CmID <> "" Then
                objattAssetDetails.CompanyID = CmID
            Else
                objattAssetDetails.CompanyID = 0
            End If

            'objattAssetDetails.Disposed = True
            ds = objBALAssetDetails.Get_AssetsDetails_Pre_Next(objattAssetDetails, Pre_Next)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Return ds.Rows(ds.Rows.Count - 1)("AstID")
                End If
            End If
            Return ""
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
            Return Nothing
        End Try
    End Function

    Private Sub txtBookDesc_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBookDesc.MouseEnter
        ToolTip1.SetToolTip(txtBookDesc, txtBookDesc.Text)
    End Sub

    Private Sub txtDepText_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDepText.MouseEnter
        ToolTip1.SetToolTip(txtDepText, txtDepText.Text)
    End Sub


    Private Function GetCustHier(ByVal CustID As String) As String
        Dim CustHier As String = objBALAssetDetails.GetCustHier_AssetDetails(CustID)
        Return objBALLocation.Comp_Path_OrgHier(CustHier)
    End Function


    Private Sub Get_AssetAddCosts(ByVal astID As String)
        Try
            Dim objattAddCostHistory As New AttAddCostHistory
            objattAddCostHistory.AstID = astID

            Dim objBALAddCodeHistory As New BALAddCostHistory
            grdAddCost.DataSource = objBALAddCodeHistory.GetAll_AddCostHistory(objattAddCostHistory)
            format_AddCostGrid()

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub



    Private Sub btnSaveAddCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAddCost.Click
        Try
            If valProvAddCost.Validate And valProvNotNigative.Validate Then
                Dim FocRow As Integer = grdAddCostView.FocusedRowHandle
                If FocRow >= 0 Then
                    If (Date.Compare(GetGridRowCellValue(grdAddCostView, FocRow, "ActionDate").ToString(), dtBVUpdate.EditValue.ToShortDateString) <= 0) Then
                        If txtAddCost.Tag <> 0 Then
                            ShowErrorMessage(My.Resources.Messages.CantChangeAdditionalCost)
                            Exit Sub
                        End If
                    End If
                End If

                If MessageBox.Show(My.Resources.Messages.BeforeAddAdditionalCost, "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim objBALAddCostType As New BALAddCostType
                    Dim objattAddCostType As New attAddCostType
                    Dim TypeDesc As String = cmbAddCostType.Text.Trim
                    Dim TypeID As Integer = objBALAddCostType.GetDatabyTypeDesc(TypeDesc)
                    If TypeID = 0 Then
                        objattAddCostType.TypeDesc = TypeDesc
                        objattAddCostType.TypeID = objBALAddCostType.GetNextPKey_AddCostType
                        objBALAddCostType.Insert_AddCostType(objattAddCostType)
                        Get_AddCostType() ' refill the comboBox.
                        TypeID = objattAddCostType.TypeID
                    End If
                    Dim objBALAddCostHistory As New BALAddCostHistory
                    Dim objAttAddCostHistory As New AttAddCostHistory

                    objAttAddCostHistory.AstID = txtAssetID.EditValue
                    objAttAddCostHistory.TypeID = TypeID
                    objAttAddCostHistory.LoginName = AppConfig.LoginName
                    objAttAddCostHistory.ActionDate = Now.Date
                    objAttAddCostHistory.AddCost = txtAddCost.Text.Trim

                    If txtAddCost.Tag <> 0 Then ' updating add cost
                        objAttAddCostHistory.PrevItemCost = GetGridRowCellValue(grdAddCostView, FocRow, "PrevItemCost").ToString()
                        objAttAddCostHistory.AddCostID = txtAddCost.Tag
                        objBALAddCostHistory.Update_AddCostHistory(objAttAddCostHistory)
                        PreviousItemCost = CType(txtbase.Text.Trim, Decimal) + CType(txtSales.Text.Trim, Decimal)
                        txtSales.Text = CDbl(txtSales.Text) + objAttAddCostHistory.AddCost - GetGridRowCellValue(grdAddCostView, FocRow, "AddCost").ToString()
                    Else
                        objAttAddCostHistory.PrevItemCost = CDbl(txtbase.Text) + CDbl(txtSales.Text)
                        objAttAddCostHistory.AddCostID = objBALAddCostHistory.GetNextPKey_AddCostHistory
                        objBALAddCostHistory.Insert_AddCostHistory(objAttAddCostHistory)
                        txtSales.Text = CDbl(txtSales.Text) + objAttAddCostHistory.AddCost
                    End If


                    ' update the total add cost field
                    update_AssetDetails()
                    Get_AssetAddCosts(txtAssetID.EditValue)
                    Dim addcostcnt As Integer = grdAddCostView.RowCount
                    If addcostcnt > 0 Then
                        txtSales.Properties.ReadOnly = True
                    Else
                        txtSales.Properties.ReadOnly = False
                    End If

                    btnNewAddCost_Click(sender, e)
                End If
            Else
                ShowErrorMessage(My.Resources.Messages.NotSaved)
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub btnNewAddCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewAddCost.Click
        cmbAddCostType.SelectedIndex = -1
        cmbAddCostType.Text = ""
        txtAddCost.Text = ""
        txtAddCost.Tag = 0
        valProvAddCost.RemoveControlError(txtAddCost)
        valProvNotNigative.RemoveControlError(txtAddCost)
        valProvAddCost.RemoveControlError(cmbAddCostType)
    End Sub

    Private Sub grdAddCostView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdAddCostView.FocusedRowChanged
        Dim FocRow As Integer = grdAddCostView.FocusedRowHandle
        If FocRow >= 0 Then
            txtAddCost.Text = GetGridRowCellValue(grdAddCostView, FocRow, "AddCost").ToString()
            cmbAddCostType.Text = GetGridRowCellValue(grdAddCostView, FocRow, "TypeDesc").ToString()
            txtAddCost.Tag = GetGridRowCellValue(grdAddCostView, FocRow, "AddCostID").ToString()
            valProvAddCost.Validate()
            valProvNotNigative.Validate()
        Else

        End If
    End Sub

    Private Sub grdAddCost_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdAddCostView.KeyDown
        Try
            If e.KeyValue = Keys.Delete Then 'Press Delete
                Dim FocRow As Integer = grdAddCostView.FocusedRowHandle
                If FocRow >= 0 Then
                    If (Date.Compare(GetGridRowCellValue(grdAddCostView, FocRow, "ActionDate").ToString(), dtBVUpdate.EditValue.ToShortDateString) <= 0) Then
                        ShowErrorMessage(My.Resources.Messages.CantDeleteAdditionalCost)
                    ElseIf MessageBox.Show(My.Resources.Messages.BeforeDeleted, "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        Dim objBALAddCostHistory As New BALAddCostHistory
                        Dim objAttAddCostHistory As New AttAddCostHistory

                        objAttAddCostHistory.AddCostID = GetGridRowCellValue(grdAddCostView, FocRow, "AddCostID").ToString()
                        objBALAddCostHistory.Delete_AddCostHistory(objAttAddCostHistory)
                        PreviousItemCost = CType(txtbase.Text.Trim, Decimal) + CType(txtSales.Text.Trim, Decimal)
                        txtSales.Text = CDbl(txtSales.Text) - GetGridRowCellValue(grdAddCostView, FocRow, "AddCost").ToString()

                        update_AssetDetails()
                        Get_AssetAddCosts(txtAssetID.EditValue)
                        Dim addcostcnt As Integer = grdAddCostView.RowCount
                        If addcostcnt > 0 Then
                            txtSales.Properties.ReadOnly = True
                        Else
                            txtSales.Properties.ReadOnly = False
                        End If

                        btnNewAddCost_Click(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Function IsNumber(ByVal ch As Char) As Boolean
        If Char.IsNumber(ch) Or Char.IsControl(ch) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub txtSalVal_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSalVal.KeyPress
        e.Handled = Not (IsNumber(e.KeyChar))
    End Sub

    Private Sub txtRef_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRef.Enter, txtRef.Click
        txtRef.SelectAll()
    End Sub

    Private Sub txtPieceNo_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPieceNo.Validating
        If CType(sender, NumericUpDown).Value > CType(sender, NumericUpDown).Maximum Then
            CType(sender, NumericUpDown).Value = CType(sender, NumericUpDown).Maximum
        End If
    End Sub

    Private Sub grdAstHistView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdAstHistView.FocusedRowChanged
        Dim FocRow As Integer = grdAstHistView.FocusedRowHandle
        If FocRow >= 0 Then
            If GetGridRowCellValue(grdAstHistView, FocRow, "Fr_loc").ToString <> "" Then
                Dim objBALCategory As New BALLocation
                txtPreLoc.Text = objBALCategory.Comp_Path(GetGridRowCellValue(grdAstHistView, FocRow, "Fr_loc").ToString())
            End If
            If GetGridRowCellValue(grdAstHistView, FocRow, "To_Loc").ToString <> "" Then
                Dim objBALCategory As New BALLocation
                txtCurrentLoc.Text = objBALCategory.Comp_Path(GetGridRowCellValue(grdAstHistView, FocRow, "To_Loc").ToString())
            End If
        End If
    End Sub


    Private Sub Get_AstBooks(ByVal _id As String)
        Try
            objattAstBooks = New attAstBooks
            objattAstBooks.AstID = _id
            grdBooks.DataSource = objBALAstBooks.GetAllData_Detail(objattAstBooks)
            If txtBookID.Text <> "" Then
                Get_AstDep_History(txtAssetID.EditValue, txtBookID.Text)
            End If
            format_BooksGrid()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub Update_DepreciationBook(ByVal depSave As Boolean)
        Try
            txtTotal2.Text = txtTotal.Text

            If grdBooksView.RowCount > 0 Then
                Dim intRow As Integer = grdBooksView.FocusedRowHandle

                If (Date.Compare(dtBVUpdate.EditValue.ToShortDateString(), DateTime.Now.ToShortDateString()) < 0) Or (Date.Compare(dtBVUpdate.EditValue.ToShortDateString(), DateTime.Now.ToShortDateString()) = 0) Then
                    objattAstBooks = New attAstBooks
                    objattAstBooks.BookDescription = RemoveUnnecessaryChars(txtBookDesc.Text)
                    If depSave Then
                        objattAstBooks.CurrentBookValue = RemoveUnnecessaryChars(txtBookVal.Text)
                    Else
                        objattAstBooks.CurrentBookValue = CType(RemoveUnnecessaryChars(txtBookVal.Text), Decimal) + (CType(txtTotal2.Text.Trim, Decimal) - PreviousItemCost)
                    End If
                    objattAstBooks.BVUpdate = dtBVUpdate.EditValue
                    objattAstBooks.DepCode = txtDepText.Tag
                    objattAstBooks.PKeyCode = RemoveUnnecessaryChars(txtBookID.Text)
                    objattAstBooks.AstID = RemoveUnnecessaryChars(txtDepAstID.Text)
                    objattAstBooks.SalvageValue = RemoveUnnecessaryChars(txtSalVal.Text)
                    objattAstBooks.SalvageYear = RemoveUnnecessaryChars(txtSalYr.Text)
                    objattAstBooks.SalvageMonth = RemoveUnnecessaryChars(txtSalMonth.Text)

                    If depSave Then
                        Dim objattDepPolicy_History As New attDepPolicy_History
                        Dim objBALDepPolicy_History As New BALDepPolicy_History
                        objattDepPolicy_History.DepCode = txtDepText.Tag
                        objattDepPolicy_History.SalvageValue = RemoveUnnecessaryChars(txtSalVal.Text)
                        objattDepPolicy_History.SalvageYear = GetGridRowCellValue(grdBooksView, intRow, "SalvageYear")
                        objattDepPolicy_History.SalvageMonth = GetGridRowCellValue(grdBooksView, intRow, "SalvageMonth")
                        objattDepPolicy_History.PKeyCode = RemoveUnnecessaryChars(txtBookID.Text)
                        objattDepPolicy_History.AstID = RemoveUnnecessaryChars(txtDepAstID.Text)
                        objattDepPolicy_History.CurrentBookValue = GetGridRowCellValue(grdBooksView, intRow, "CurrentBV")
                        objattDepPolicy_History.BVUpdate = dtBVUpdate.EditValue
                        objBALDepPolicy_History.Insert_DepPolicy_History(objattDepPolicy_History)
                    End If


                    objBALAstBooks.Update_AstBooks(objattAstBooks)


                    Get_AstBooks(txtAssetID.EditValue)

                    If depSave Then
                        ShowInfoMessage(My.Resources.Messages.Saved)
                    End If

                Else
                    MessageBox.Show("Book already updated cannot change")
                    If grdBooksView.RowCount > 0 Then
                        txtBookID.Text = GetGridRowCellValue(grdBooksView, intRow, "BookID")
                        txtBookDesc.Text = GetGridRowCellValue(grdBooksView, intRow, "Description").ToString
                        txtDepText.Tag = GetGridRowCellValue(grdBooksView, intRow, "DepCode").ToString
                        dtBVUpdate.EditValue = CDate(GetGridRowCellValue(grdBooksView, intRow, "BVUpdate").ToString)
                        txtBookVal.Text = GetGridRowCellValue(grdBooksView, intRow, "CurrentBV").ToString
                        txtSalVal.Text = GetGridRowCellValue(grdBooksView, intRow, "SalvageValue").ToString
                        txtSalYr.Text = GetGridRowCellValue(grdBooksView, intRow, "SalvageYear").ToString
                        txtSalMonth.Text = GetGridRowCellValue(grdBooksView, intRow, "SalvageMonth").ToString

                        If txtDepText.Tag <> "" Then
                            txtDepText.Text = Get_Dep(txtDepText.Tag)
                            If txtDepText.Text = "Straight Line Method" Then
                                panelPercent.Visible = True
                                txtSalPercent.Text = CalculatePercent(txtSalYr.Text, txtSalMonth.Text)
                            Else
                                panelPercent.Visible = False
                            End If
                            grdExpectedBookAnnual.DataSource = CalcDepAnnual(RemoveUnnecessaryChars(txtDepAstID.Text) & "-" & RemoveUnnecessaryChars(txtBookID.Text), txtSalYr.Text, txtSalMonth.Text, txtDepText.Tag, txtTotal2.Text, txtSalVal.Text, dtBVUpdate.EditValue, txtBookVal.Text, dtService.EditValue, dtpur.EditValue)
                            grdExpectedBookMonthly.DataSource = CalcDepMonthly(RemoveUnnecessaryChars(txtDepAstID.Text) & "-" & RemoveUnnecessaryChars(txtBookID.Text), txtSalYr.Text, txtSalMonth.Text, txtDepText.Tag, txtTotal2.Text, txtSalVal.Text, dtBVUpdate.EditValue, txtBookVal.Text, dtService.EditValue)
                        Else
                            txtDepText.Text = ""
                        End If
                        isEdit2 = True
                    End If
                End If
            End If
            grdExpectedBookAnnual.DataSource = CalcDepAnnual(RemoveUnnecessaryChars(txtDepAstID.Text) & "-" & RemoveUnnecessaryChars(txtBookID.Text), txtSalYr.Text, txtSalMonth.Text, txtDepText.Tag, txtTotal2.Text, txtSalVal.Text, dtBVUpdate.EditValue, txtBookVal.Text, dtService.EditValue, dtpur.EditValue)
            grdExpectedBookMonthly.DataSource = CalcDepMonthly(RemoveUnnecessaryChars(txtDepAstID.Text) & "-" & RemoveUnnecessaryChars(txtBookID.Text), txtSalYr.Text, txtSalMonth.Text, txtDepText.Tag, txtTotal2.Text, txtSalVal.Text, dtBVUpdate.EditValue, txtBookVal.Text, dtService.EditValue)

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Public Function CalculatePercent(ByVal SalYearValue As String, ByVal SalMonthValue As String) As String
        If SalYearValue <> "" And SalMonthValue <> "" Then
            Dim percent As Double
            Dim SalMonth As Integer = CInt(SalMonthValue)
            Dim SalYear As Integer = CInt(SalYearValue)

            Dim perc As Double
            perc = SalYear + (SalMonth / 12)
            If perc = 0 Then
                percent = 0
            Else
                percent = Math.Round(100 / perc, 2)
            End If

            Return percent.ToString
        Else
            Return "0"
        End If
    End Function


    Private Sub grdBooksView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdBooksView.Click
        grdBooksView_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub grdBooksView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdBooksView.FocusedRowChanged
        Try
            Dim FocRow As Integer = grdBooksView.FocusedRowHandle
            If FocRow >= 0 Then
                txtTotal2.Text = txtTotal.Text
                txtBookID.Text = GetGridRowCellValue(grdBooksView, FocRow, "BookID").ToString
                txtBookDesc.Text = GetGridRowCellValue(grdBooksView, FocRow, "Description").ToString
                txtDepText.Tag = GetGridRowCellValue(grdBooksView, FocRow, "DepCode").ToString

                dtBVUpdate.EditValue = CDate(GetGridRowCellValue(grdBooksView, FocRow, "BVUpdate").ToString)
                txtBookVal.Text = GetGridRowCellValue(grdBooksView, FocRow, "CurrentBV").ToString
                txtSalVal.Text = GetGridRowCellValue(grdBooksView, FocRow, "SalvageValue").ToString
                txtSalYr.Text = GetGridRowCellValue(grdBooksView, FocRow, "SalvageYear").ToString
                txtSalMonth.Text = GetGridRowCellValue(grdBooksView, FocRow, "SalvageMonth").ToString

                If txtDepText.Tag <> "" Then
                    txtDepText.Text = Get_Dep(txtDepText.Tag)
                    If txtDepText.Text = "Straight Line Method" Then
                        panelPercent.Visible = True
                        txtSalPercent.Text = CalculatePercent(txtSalYr.Text, txtSalMonth.Text)
                    Else
                        panelPercent.Visible = False
                    End If
                    grdExpectedBookAnnual.DataSource = CalcDepAnnual(RemoveUnnecessaryChars(txtDepAstID.Text) & "-" & RemoveUnnecessaryChars(txtBookID.Text), txtSalYr.Text, txtSalMonth.Text, txtDepText.Tag, txtTotal2.Text, txtSalVal.Text, dtBVUpdate.EditValue, txtBookVal.Text, dtService.EditValue, dtpur.EditValue)
                    grdExpectedBookMonthly.DataSource = CalcDepMonthly(RemoveUnnecessaryChars(txtDepAstID.Text) & "-" & RemoveUnnecessaryChars(txtBookID.Text), txtSalYr.Text, txtSalMonth.Text, txtDepText.Tag, txtTotal2.Text, txtSalVal.Text, dtBVUpdate.EditValue, txtBookVal.Text, dtService.EditValue)
                Else
                    txtDepText.Text = ""
                End If
                isEdit2 = True
                valProvSecondary.Validate()
                errProv.ClearErrors()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Public Function check_Child_CostType(ByVal _id As String) As Boolean
        Dim objBAL As New BALAddCostHistory
        If objBAL.Check_ChildCostType(_id) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btnDeleteAddCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAddCost.Click
        Dim objattAddCosttype As New attAddCostType
        Dim objBALAddCostType As New BALAddCostType
        Dim TypeDesc As String = RemoveUnnecessaryChars(cmbAddCostType.Text)
        Try
            If TypeDesc <> "" Then
                Dim TypeID As Integer = objBALAddCostType.GetDatabyTypeDesc(TypeDesc)
                If Not check_Child_CostType(TypeID) Then
                    If MessageBox.Show(My.Resources.Messages.BeforeDeleted, "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        objattAddCosttype.TypeID = TypeID
                        If objBALAddCostType.Delete_AddCostType(objattAddCosttype) Then
                            Get_AddCostType()
                            ShowInfoMessage("Record deleted successfully")
                            btnNewAddCost_Click(sender, e)
                        End If
                    End If
                Else
                    ShowErrorMessage(My.Resources.Messages.CantDelete)
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub txtSalPercent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSalPercent.TextChanged
        Try
            If txtSalPercent.ContainsFocus Then
                Dim intVal As Integer = CInt(txtSalPercent.Text)
                If intVal < 0 Or intVal > 1200 Then
                    errProv.SetError(txtSalPercent, My.Resources.Messages.OutPercentRange)
                Else
                    errProv.ClearErrors()
                    Dim perValue As Decimal = CType(txtSalPercent.Text.Trim, Double)
                    Dim YearCost As Double = 100 / perValue

                    txtSalYr.Text = Math.Truncate(YearCost)
                    txtSalMonth.Text = Math.Ceiling(12 * (YearCost - Math.Truncate(YearCost)))
                End If
            End If
        Catch ex As Exception
            errProv.SetError(txtSalPercent, My.Resources.Messages.OutPercentRange)
        End Try
    End Sub
    Private Sub txtSalYr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSalYr.KeyPress
        If e.KeyChar = "." Then
            txtSalMonth.Focus()
        End If
    End Sub

    Private Sub txtSalYr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSalYr.TextChanged, txtSalMonth.TextChanged
        If txtSalYr.ContainsFocus Or txtSalMonth.ContainsFocus Then
            errProv.ClearErrors()
            valProvMain.Validate()
            txtSalPercent.Text = CalculatePercent(txtSalYr.Text, txtSalMonth.Text)
        End If
    End Sub

    Private Sub grdAddCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAddCost.Click
        grdAddCostView_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub dtBVUpdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtBVUpdate.EditValueChanged
        txtBVUpdate.Text = dtBVUpdate.EditValue
    End Sub



    Private Sub txtWarranty_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWarranty.EditValueChanged
        txtWarrantyExpired.Text = dtService.DateTime.AddMonths(txtWarranty.Value).ToString(AppConfig.MaindateFormat)
    End Sub

    Private Sub dtService_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtService.EditValueChanged
        dtpur.EditValue = dtService.EditValue
        If dtService.EditValue.ToString <> "" Then
            txtWarrantyExpired.Text = dtService.DateTime.AddMonths(txtWarranty.Value).ToString(AppConfig.MaindateFormat)
            If Now.Date >= dtService.EditValue Then
                txtAge.Text = DateDiff(DateInterval.Month, dtService.EditValue, Now.Date).ToString
            Else
                txtAge.Text = 0
            End If
        End If
    End Sub

    Private Sub cmbItemCode_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbItemCode.QueryPopUp
        PopulateItems()
    End Sub

    Private Sub cmbItemCode_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbItemCode.EditValueChanged
        Try
            If cmbItemCode.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(cmbItemCode.EditValue) Then
                Get_Assets_ById(cmbItemCode.EditValue)
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub PopulateItems()
        Dim objBALAssets As New BALItems
        cmbItemCode.Properties.ValueMember = "itemcode"
        cmbItemCode.Properties.DisplayMember = "AstDesc"
        cmbItemCode.Properties.DataSource = objBALAssets.GetAllData_GetCombo(New attItems)
    End Sub

    Private Sub PopulateCompanies()
        Dim objBALCompany As New BALCompany
        cmbCompany.Properties.ValueMember = "CompanyId"
        cmbCompany.Properties.DisplayMember = "CompanyName"
        cmbCompany.Properties.DataSource = objBALCompany.GetAllData_GetCombo(New attcompany)
    End Sub

    Private Sub cmbCompany_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCompany.EditValueChanged
        If cmbCompany.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(cmbCompany.EditValue) Then
            If AppConfig.CodingMode And Not isEdit Then
                txtAstNum.Text = Generate_AssetNumber(cmbCompany.EditValue)
                If txtAstNum.Text = "" Then
                    errProv.SetError(cmbCompany, My.Resources.Messages.DefineRange)
                Else
                    errProv.ClearErrors()
                End If
                txtRef.Text = txtSerialNo.Text
            End If
        Else
            errProv.ClearErrors()
        End If
        cmbGL.EditValue = Nothing
    End Sub

    Private Sub cmbCompany_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbCompany.QueryPopUp
        PopulateCompanies()
    End Sub

    Private Sub cmbBrand_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbBrand.QueryPopUp
        PopulateBrands()
    End Sub

    Private Sub PopulateBrands()
        Dim objBALBrand As New BALbrand
        cmbBrand.Properties.ValueMember = "AstBrandID"
        cmbBrand.Properties.DisplayMember = "AstBrandName"
        cmbBrand.Properties.DataSource = objBALBrand.GetAll_Brand(New attBrand)
    End Sub

    Private Sub cmbCustodian_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCustodian.EditValueChanged
        txtCustHier.Text = GetCustHier(cmbCustodian.EditValue)
    End Sub

    Private Sub cmbCustodian_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbCustodian.QueryPopUp
        PopulateCustodian()
    End Sub
    Private Sub PopulateCustodian()
        Dim objBALCustodian As New BALCustodian
        cmbCustodian.Properties.ValueMember = "ID"
        cmbCustodian.Properties.DisplayMember = "ID"
        cmbCustodian.Properties.DataSource = objBALCustodian.GetAllData_GetCombo()
    End Sub

    Private Sub cmbAssets_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAssetID.EditValueChanged
        If txtAssetID.Tag = "NewID" Then
            txtAssetID.Tag = ""
        Else
            If txtAssetID.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(txtAssetID.EditValue) Then
                Me.Search_Asset(txtAssetID.EditValue)
            End If
        End If
    End Sub

    Private Sub PopulateGLCodes()
        Dim objBALGLCode As New BALGLCode
        cmbGL.Properties.ValueMember = "GLcode"
        cmbGL.Properties.DisplayMember = "GLcode"
        cmbGL.Properties.DataSource = objBALGLCode.GetCompanyGLCodes(cmbCompany.EditValue)
    End Sub

    Private Sub cmbGL_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbGL.QueryPopUp
        PopulateGLCodes()
    End Sub

    Private Sub PopulateAssetsStatus()
        cmbAssetsStatus.Properties.ValueMember = "ID"
        cmbAssetsStatus.Properties.DisplayMember = "Status"
        cmbAssetsStatus.Properties.DataSource = objBALAssetDetails.GetAssetStatus(False, True)
    End Sub
    Private Sub cmbAssetsStatus_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbAssetsStatus.QueryPopUp
        PopulateAssetsStatus()
    End Sub

    Private Sub PopulateCostCenter()
        Dim objBALCost As New BALCostCenter
        cmbCostCenter.Properties.ValueMember = "CostID"
        cmbCostCenter.Properties.DisplayMember = "CostName"
        cmbCostCenter.Properties.DataSource = objBALCost.GetAllData_GetCombo(New attCostCenter)
    End Sub

    Private Sub PopulateInsurance()
        Dim objBALInsurer As New BALInsurer
        cmbInsurer.Properties.ValueMember = "InsCode"
        cmbInsurer.Properties.DisplayMember = "InsName"
        cmbInsurer.Properties.DataSource = objBALInsurer.GetAll_Insurer(New attInsurer)
    End Sub

    Private Sub PopulatePO()
        Dim objattPurchaseOrder As New attPurchaseOrder
        'objattPurchaseOrder.POStatus = 2
        Dim objBALPurchaseOrder As New BALPurchaseOrder
        cmbPOCode.Properties.ValueMember = "POCode"
        cmbPOCode.Properties.DisplayMember = "Quotation"
        Dim dt As DataTable = objBALPurchaseOrder.GetAllData_GetCombo(objattPurchaseOrder)
        dt.Columns("Quotation").Caption = "DO Number"
        cmbPOCode.Properties.DataSource = dt
        ' cmbPOCode.Properties.View.Columns(0).Caption = ""
    End Sub

    Private Sub PopulateSupplier()
        Dim objBALSupplier As New BALSupplier
        cmbSupplier.Properties.ValueMember = "SuppID"
        cmbSupplier.Properties.DisplayMember = "SuppName"
        cmbSupplier.Properties.DataSource = objBALSupplier.GetAllData_GetCombo()
    End Sub

    Private Sub cmbPOCode_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbPOCode.QueryPopUp
        PopulatePO()
    End Sub

    Private Sub cmbSupplier_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbSupplier.QueryPopUp
        PopulateSupplier()
    End Sub


    Private Sub cmbInsurer_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbInsurer.QueryPopUp
        PopulateInsurance()
    End Sub

    Private Sub cmbDisposalMethod_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbDisposalMethod.QueryPopUp
        PopulateDisposal()
    End Sub
    Private Sub cmbDisposalMethod_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDisposalMethod.EditValueChanged
        Try
            If chkDisposed.Checked Then
                cmbDisposalMethod.Enabled = True
                dtDispdate.Enabled = True
                If cmbDisposalMethod.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(cmbDisposalMethod.EditValue) Then
                    If cmbDisposalMethod.EditValue = "1" Then
                        Panel1.Visible = True
                    Else
                        Panel1.Visible = False
                    End If
                Else
                    Panel1.Visible = False
                End If

            Else

                cmbDisposalMethod.Enabled = False
                dtDispdate.Enabled = False
                Panel1.Visible = False

            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub PopulateDisposal()
        Dim objBALDisposalMethod As New BALDisposalMethod
        cmbDisposalMethod.Properties.ValueMember = "DispCode"
        cmbDisposalMethod.Properties.DisplayMember = "DispDesc"
        cmbDisposalMethod.Properties.DataSource = objBALDisposalMethod.GetAll_DisposalMethod(New attDisposalMethod)
    End Sub

    Private Sub btnLast_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLast.ItemClick
        Dim LastAstID As String = ""
        objattAssetDetails = New attAssetDetails
        If AppConfig.CodingMode Then
            If cmbCompany.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(cmbCompany.EditValue) Then
                objattAssetDetails.CompanyID = cmbCompany.EditValue
            Else
                objattAssetDetails.CompanyID = 0
            End If
            LastAstID = objBALAssetDetails.Get_AssetsDetails_First_Last(objattAssetDetails, 2)
        Else
            objattAssetDetails.CompanyID = 0
            LastAstID = objBALAssetDetails.Get_AssetsDetails_First_Last(objattAssetDetails, 2)
        End If
        If LastAstID <> "" Then
            txtAssetID.EditValue = LastAstID
            lblSearchStatus.ForeColor = Color.OrangeRed
            lblSearchStatus.Text = "N/A"
            txtSerialNumSearch.Text = ""
            'Me.Search_Asset(LastAstID)
        End If
    End Sub

    Private Sub btnNext_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnNext.ItemClick
        Dim NextAstId As String = ""
        If AppConfig.CodingMode Then
            NextAstId = Get_AssetsDetails_Pre_Next(txtAstNum.Text, cmbCompany.EditValue, 2)
        Else
            NextAstId = Get_AssetsDetails_Pre_Next(txtAstNum.Text, "", 2)
        End If

        If NextAstId <> "" Then
            txtAssetID.EditValue = NextAstId
            lblSearchStatus.ForeColor = Color.OrangeRed
            lblSearchStatus.Text = "N/A"
            txtSerialNumSearch.Text = ""
            'Me.Search_Asset(NextAstId)
        End If
    End Sub

    Private Sub btnPrev_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPrev.ItemClick
        Dim PreAstId As String = ""
        If AppConfig.CodingMode Then
            PreAstId = Get_AssetsDetails_Pre_Next(txtAstNum.Text, cmbCompany.EditValue, 1)
        Else
            PreAstId = Get_AssetsDetails_Pre_Next(txtAstNum.Text, "", 1)
        End If

        If PreAstId <> "" Then
            txtAssetID.EditValue = PreAstId
            lblSearchStatus.ForeColor = Color.OrangeRed
            lblSearchStatus.Text = "N/A"
            txtSerialNumSearch.Text = ""
            'Me.Search_Asset(PreAstId)
        End If
    End Sub

    Private Sub btnFirst_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnFirst.ItemClick
        Dim FirstAstID As String = ""
        objattAssetDetails = New attAssetDetails
        If AppConfig.CodingMode Then
            If cmbCompany.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(cmbCompany.EditValue) Then
                objattAssetDetails.CompanyID = cmbCompany.EditValue
            Else
                objattAssetDetails.CompanyID = 0
            End If
            FirstAstID = objBALAssetDetails.Get_AssetsDetails_First_Last(objattAssetDetails, 1)
        Else
            objattAssetDetails.CompanyID = 0
            FirstAstID = objBALAssetDetails.Get_AssetsDetails_First_Last(objattAssetDetails, 1)
        End If
        If FirstAstID <> "" Then
            txtAssetID.EditValue = FirstAstID
            lblSearchStatus.ForeColor = Color.OrangeRed
            lblSearchStatus.Text = "N/A"
            txtSerialNumSearch.Text = ""
            'Me.Search_Asset(FirstAstID)
        End If
    End Sub

    Private Sub CopyAssetImage(ByVal strAstIdOld As String, ByVal strAstIdNew As String)
        If strAstIdOld <> "" And strAstIdNew <> "" Then
            Try
                If AppConfig.ImgType = "Asset Images" Then
                    If AppConfig.ImgStorgeLoc = "Shared Folder" Then
                        If File.Exists(AppConfig.ImgPath & "\" & strAstIdOld & ".jpg") Then
                            File.Copy(AppConfig.ImgPath & "\" & strAstIdOld & ".jpg", AppConfig.ImgPath & "\" & strAstIdNew & ".jpg", True)
                        End If
                    ElseIf AppConfig.ImgStorgeLoc = "Database" Then
                        objattAssetDetails = New attAssetDetails
                        objattAssetDetails.PKeyCode = strAstIdOld
                        Dim ImageBytes As Byte() = objBALAssetDetails.GetAssetImage(objattAssetDetails)

                        objBALAssetDetails.CopyAssetImage(strAstIdNew, ImageBytes)
                    End If
                End If
            Catch ex As Exception
                GenericExceptionHandler(ex, WhoCalledMe)
            End Try
        End If

    End Sub

    Private Sub SaveImage()
        If picAssets.Tag IsNot Nothing AndAlso File.Exists(picAssets.Tag) Then
            If AppConfig.ImgStorgeLoc = "Shared Folder" Then
                Dim fname As String = ""
                If AppConfig.ImgType = "Asset Images" Then
                    fname = txtAssetID.EditValue & ".jpg"
                ElseIf AppConfig.ImgType = "Item Images" Then
                    fname = cmbItemCode.EditValue & ".jpg"
                End If

                If picAssets.Tag <> AppConfig.ImgPath & "\" & fname Then
                    File.Copy(picAssets.Tag, AppConfig.ImgPath & "\" & fname, True)
                    picAssets.Tag = AppConfig.ImgPath & "\" & fname
                    picAssets.Image = Image.FromFile(picAssets.Tag)
                End If

            ElseIf AppConfig.ImgStorgeLoc = "Database" Then
                If AppConfig.ImgType = "Asset Images" Then
                    objattAssetDetails = New attAssetDetails
                    objattAssetDetails.PKeyCode = txtAssetID.EditValue
                    Dim fs As New FileStream(picAssets.Tag, FileMode.Open)
                    Dim Data() As Byte = New [Byte](fs.Length) {}
                    fs.Read(Data, 0, fs.Length)
                    objattAssetDetails.Image = Data
                    objBALAssetDetails.UpdateAssetImage(objattAssetDetails)
                    fs.Dispose()
                ElseIf AppConfig.ImgType = "Item Images" Then
                    objattAssets = New attItems
                    objattAssets.PKeyCode = cmbItemCode.EditValue
                    Dim fs As New FileStream(picAssets.Tag, FileMode.Open)
                    Dim Data() As Byte = New [Byte](fs.Length) {}
                    fs.Read(Data, 0, fs.Length)
                    objattAssets.Image = Data
                    objBALAssets.UpdateItemImage(objattAssets)
                    fs.Dispose()
                End If
            End If
        End If
    End Sub

    Private Sub btnClose_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnClose.ItemClick
        Me.Close()
    End Sub

    Private Sub btnDelete_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDelete.ItemClick
        Try
            If objBALAssetDetails.Check_AstID(txtAssetID.EditValue, True) Then
                If MessageBox.Show("Deleting this Asset will delete all its transfer history, Do you want to continue?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    objattAssetDetails = New attAssetDetails
                    objattAssetDetails.PKeyCode = txtAssetID.EditValue
                    If objBALAssetDetails.Delete_Details(objattAssetDetails) Then

                        If AppConfig.ImgStorgeLoc = "Shared Folder" Then
                            DeleteImage()
                        End If
                        ShowInfoMessage("Record deleted successfully")
                        btnNew.PerformClick()
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnSave_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSave.ItemClick
        Try
            lblSearchStatus.ForeColor = Color.OrangeRed
            lblSearchStatus.Text = "N/A"

            CheckDemo()

            errProv.ClearErrors()
            If valProvMain.Validate And valProvNotNigative.Validate Then
                If Date.Compare(dtpur.EditValue.ToShortDateString, dtService.EditValue.ToShortDateString) > 0 Then
                    ShowErrorMessage(My.Resources.Messages.PurchaseDateShouldBeLessThanServiceDate)
                    Exit Sub
                End If


                Dim objattBookTemp As New attBook
                Dim objBALBookTemp As New BALBooks
                Dim objattAstBooks As New attAstBooks
                Dim ds As DataTable
                objattBookTemp.CompanyID = cmbCompany.EditValue
                ds = objBALBookTemp.GetAll_Book(objattBookTemp)
                If ds Is Nothing Or ds.Rows.Count < 1 Then
                    ShowErrorMessage(My.Resources.Messages.NoCompanyBookSelected)
                    Exit Sub
                End If

                'if the ref number is empty we assign it the astnum.
                If txtRef.Text.Trim = "" Then
                    txtRef.Text = txtSerialNo.Text
                End If

                If CInt(txtTotal.Text) >= 0 Then
                    Try

                        If isEdit Then
                            If valProvSecondary.Validate Then
                                update_AssetDetails()
                            End If
                        Else
                            AddNew_AssetDetails()
                        End If
                        SaveImage()


                        btnSaveAddCost.Enabled = True
                        btnNewAddCost.Enabled = True

                    Catch ex As Exception
                        GenericExceptionHandler(ex, WhoCalledMe)
                    End Try
                Else
                    errProv.SetError(txtTotal, My.Resources.Messages.InvalidCost)
                End If
            Else
                ShowErrorMessage(My.Resources.Messages.NotSaved)
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnCopy_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCopy.ItemClick
        'disallow coping dispsed assets.
        If objBALAssetDetails.IsAssetDisposed(txtAssetID.EditValue) Then
            ShowErrorMessage(My.Resources.Messages.CanNotCopyDisposes)
        Else
            FrmCop = New frmBlkQty
            FrmCop.frm = Me
            FrmCop.ShowDialog()
            FrmCop.Dispose()
            btnNew.PerformClick()
        End If
    End Sub

    Private Sub btnNew_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnNew.ItemClick
        Try
            lblSearchStatus.ForeColor = Color.OrangeRed
            lblSearchStatus.Text = "N/A"
            txtSerialNumSearch.Text = ""
            txtSerialNo.Text = ""
            txtAstNum.Text = ""
            txtBarCode.Text = ""
            cmbCostCenter.SelectedText = ""
            cmbCostCenter.Text = ""
            cmbCustodian.EditValue = Nothing
            If cmbCompany.Properties.View.RowCount > 0 Then
                cmbCompany.EditValue = cmbCompany.Properties.View.GetRowCellValue(0, "CompanyId")
            Else
                cmbCompany.EditValue = Nothing
            End If
            If cmbGL.Properties.View.RowCount > 0 Then
                cmbGL.EditValue = cmbGL.Properties.View.GetRowCellValue(0, "GLcode")
            Else
                cmbGL.EditValue = Nothing
            End If

            cmbItemCode.EditValue = Nothing
            cmbBrand.EditValue = Nothing
            'txtAssetStatus.FindRow(3, "In Stock") 
            cmbAssetsStatus.EditValue = 3 'In Stock
            cmbSupplier.EditValue = Nothing
            cmbInsurer.EditValue = Nothing
            cmbPOCode.EditValue = Nothing

            txtCategory.Text = ""
            dtBVUpdate.EditValue = DateTime.Now.Date
            txtRemarks.Text = ""

            txtbase.Text = "0"
            dtpur.Text = ""
            cmbItemCode.Text = ""
            txtSales.Text = "0"
            dtService.Text = ""
            txtLabelPrintedCnt.Text = ""
            txtRemarks.Text = ""
            cmbLocation.EditValue = Nothing
            'ZTLocation.SelectedText = ""
            'ZTLocation.SelectedValue = ""
            txtRef.Text = ""
            txtDepText.Text = ""
            txtAstDesc.Text = ""
            txtAstDesc2.Text = ""
            txtAstModel.Text = ""
            txtTotal.Text = "0"
            txtSalPercent.Text = ""
            txtSalYr.Text = ""
            txtBookID.Text = ""
            txtDODesc.Text = ""
            txtDoProjectCode.Text = ""
            txtSalMonth.Text = ""
            txtSalVal.Text = ""
            txtTotal2.Text = ""
            txtBookVal.Text = ""
            txtAccDep.Text = ""
            txtDepText.Text = ""
            txtBookDesc.Text = ""
            txtDepAstID.Text = ""
            txtPlate.Text = ""
            txtDiscount.Text = "0"
            txtWarranty.Value = 0
            txtAssetID.Tag = "NewID"
            txtAssetID.EditValue = objBALAssetDetails.Generate_AssetID()
            txtCreatedBY.Text = String.Empty
            txtEvaGroup1.Text = String.Empty
            txtEvaGroup2.Text = String.Empty
            txtEvaGroup3.Text = String.Empty
            txtEvaGroup4.Text = String.Empty
            txtCustomField1.Text = String.Empty
            txtCustomField2.Text = String.Empty
            txtCustomField3.Text = String.Empty
            txtCustomField4.Text = String.Empty
            txtCustomField5.Text = String.Empty


            If Not AppConfig.CodingMode Then
                txtAstNum.Text = Generate_AssetNumber("")
                txtRef.Text = txtSerialNo.Text
            End If
            btnDelete.Enabled = False
            btnCopy.Enabled = False
            btnPrintLabel.Enabled = False

            LayoutControlItemDeleteImage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            picAssets.Image = Nothing
            picAssets.Tag = Nothing

            cmbDisposalMethod.Enabled = True

            Panel1.Visible = False

            chkDisposed.Checked = False

            If grdAstHist.DataSource IsNot Nothing Then
                CType(grdAstHist.DataSource, DataTable).Rows.Clear()
            End If

            If grdCustHist.DataSource IsNot Nothing Then
                CType(grdCustHist.DataSource, DataTable).Rows.Clear()
            End If

            If grdTransHistory.DataSource IsNot Nothing Then
                CType(grdTransHistory.DataSource, DataTable).Rows.Clear()
            End If

            If grdBooks.DataSource IsNot Nothing Then
                CType(grdBooks.DataSource, DataTable).Rows.Clear()
            End If

            If grdFiscalYRBook.DataSource IsNot Nothing Then
                CType(grdFiscalYRBook.DataSource, DataTable).Rows.Clear()
            End If

            If grdExpectedBookAnnual.DataSource IsNot Nothing Then
                CType(grdExpectedBookAnnual.DataSource, DataTable).Rows.Clear()
            End If

            If grdExpectedBookMonthly.DataSource IsNot Nothing Then
                CType(grdExpectedBookMonthly.DataSource, DataTable).Rows.Clear()
            End If

            If grdAddCost.DataSource IsNot Nothing Then
                CType(grdAddCost.DataSource, DataTable).Rows.Clear()
            End If
            btnNewAddCost_Click(sender, e)

            btnSaveAddCost.Enabled = False
            btnNewAddCost.Enabled = False

            txtSales.Properties.ReadOnly = False
            txtbase.Properties.ReadOnly = False

            isEdit = False

            valProvMain.RemoveControlError(txtSales)
            valProvMain.RemoveControlError(txtDiscount)
            valProvMain.RemoveControlError(txtbase)

            valProvMain.RemoveControlError(cmbGL)
            valProvMain.RemoveControlError(cmbBrand)
            valProvMain.RemoveControlError(cmbAssetsStatus)
            valProvMain.RemoveControlError(cmbCustodian)
            valProvMain.RemoveControlError(cmbItemCode)
            valProvMain.RemoveControlError(cmbCompany)
            valProvMain.RemoveControlError(txtbase)
            valProvMain.RemoveControlError(txtSales)
            valProvMain.RemoveControlError(txtAstNum)
            'valProvMain.RemoveControlError(ZTLocation.TextBox)
            valProvMain.RemoveControlError(cmbLocation)

            valProvNotNigative.Validate()
            TabbedControlGroupMain.SelectedTabPageIndex = 0
            cmbItemCode.Focus()
            'if it's offline machine then don't allow to change the company.
            If AppConfig.IsOfflineMachine Then
                cmbCompany.Enabled = False
                Dim objBalOfflineMachine As New BALOfflineMachines
                Dim dt As DataTable = objBalOfflineMachine.GetAll_OfflineMachine(New AttOfflineMachines)
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        cmbCompany.EditValue = dt.Rows(0)("CompanyID")
                    End If
                End If
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub



    Private Sub picAssets_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles picAssets.Click
        Try
            If dlgFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
                picAssets.Tag = dlgFile.FileName
                picAssets.Image = Image.FromFile(picAssets.Tag)
                LayoutControlItemDeleteImage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub btnDelImg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If MessageBox.Show(My.Resources.Messages.BeforeDeletedImg, "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                DeleteImage()
                ShowInfoMessage(My.Resources.Messages.AfterDeleteImg)
                LayoutControlItemDeleteImage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub picAssets_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picAssets.MouseLeave
        picAssets.BorderStyle = BorderStyle.None
    End Sub

    Private Sub picAssets_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picAssets.MouseEnter
        picAssets.BorderStyle = BorderStyle.FixedSingle
    End Sub


    'Private Sub cmbAssetsSearch_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAssetsSearch.EditValueChanged

    'End Sub

    'Private Sub cmbAssetsSearch_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbAssetsSearch.QueryPopUp
    '    PopulateAssets()
    'End Sub

    'Private Sub PopulateAssets()
    '    'Dim objattAssetDetails As attAssetDetails = New attAssetDetails
    '    Dim objBALAssetDetails As BALAssetDetails = New BALAssetDetails
    '    'objattAssetDetails.Disposed = True
    '    cmbAssetsSearch.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
    '    cmbAssetsSearch.Properties.View.BestFitMaxRowCount = 50
    '    cmbAssetsSearch.Properties.View.OptionsView.ShowAutoFilterRow = True
    '    cmbAssetsSearch.Properties.View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    cmbAssetsSearch.Properties.ValueMember = assetsearchField
    '    cmbAssetsSearch.Properties.DisplayMember = assetsearchField
    '    cmbAssetsSearch.Properties.DataSource = objBALAssetDetails.GetAssetData_List()
    '    'cmbAssets.DataSource = objBALAssetDetails.GetAsset_DetailsCombo(objattAssetDetails)
    'End Sub

    Private Sub txtSerialNumSearch_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSerialNumSearch.EditValueChanged
        If Not String.IsNullOrEmpty(txtSerialNumSearch.Text.Trim) Then
            objattAssetDetails = New attAssetDetails
            objattAssetDetails.SerailNo = txtSerialNumSearch.Text.Trim
            Dim dt As DataTable = objBALAssetDetails.GetAstIDBySerialNumlike(objattAssetDetails)
            If dt.Rows.Count > 0 Then
                txtAssetID.Text = dt.Rows(0)("AstID") 'Search_Asset()
                lblSearchStatus.Text = "Asset Found"
                lblSearchStatus.ForeColor = Color.Green
            Else
                btnNew.PerformClick()
                lblSearchStatus.Text = "Asset Not Found"
                lblSearchStatus.ForeColor = Color.Red
                txtSerialNumSearch.Focus()
                Beep()
            End If
        End If

    End Sub

    Private Sub cmbPOCode_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPOCode.EditValueChanged
        If cmbPOCode.EditValue IsNot Nothing Then
            Dim objattPurchaseOrder As New attPurchaseOrder
            objattPurchaseOrder.PKeyCode = cmbPOCode.EditValue
            Dim objBALPurchaseOrder As New BALPurchaseOrder
            Dim dt As DataTable = objBALPurchaseOrder.GetAll_PurchaseOrder(objattPurchaseOrder)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtDODesc.Text = dt.Rows(0)("ModeDelivery")
                txtDoProjectCode.Text = dt.Rows(0)("ReferenceNo")
            End If
        End If
    End Sub

    Private Sub btnPrintLabel_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPrintLabel.ItemClick
        Try
            If Not String.IsNullOrEmpty(txtAstNum.Text) Then
                Dim objBALAssetDetails As New BALAssetDetails
                Dim rpt As New DevExpress.XtraReports.UI.XtraReport
                rpt = LoadReport("Assets Barcode")
                If MessageBox.Show("Are you sure to print Asset Label for current record?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                    Dim dt As DataTable = objBALAssetDetails.GetAssetLabelData(txtAssetID.Text)
                    rpt.DataSource = dt
                    rpt.Print()
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally

        End Try
    End Sub

    Public Sub PopulateLocation()

        Dim objBALLocation As New BALLocation
        cmbLocation.Properties.ValueMember = "LocID"
        cmbLocation.Properties.DisplayMember = "LocationFullPath"
        cmbLocation.Properties.DataSource = objBALLocation.GetComboLocationsSortedByCode(New attLocation)
        cmbLocation.Properties.View.Columns("Code").Visible = False
        cmbLocation.Properties.View.Columns("locLevel").Visible = False
        cmbLocation.Properties.View.Columns("CompanyID").Visible = False
        cmbLocation.Properties.View.Columns("CompanyName").Visible = False

        cmbLocation.Properties.View.Columns("LocID").Visible = False
        cmbLocation.Properties.View.Columns("CompCode").Caption = "Loc Code"
        cmbLocation.Properties.View.Columns("CompCode").VisibleIndex = 0
        cmbLocation.Properties.View.Columns("LocDesc").Visible = False
    End Sub
End Class

