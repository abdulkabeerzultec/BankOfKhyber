Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO
Imports System.Globalization.CultureInfo
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.XtraReports.UI

Public Class frmAssetsDetails

#Region " -- My Decleration -- "
    Dim objattAssetsCoding As attAssetsCoding
    Dim objBALAssetsCoding As New BALAssetsCoding
    Dim objAlHadaIntegration As New BALAlhadaIntegration
    Dim objattCompany As attcompany
    Dim objBALCompany As New BALCompany
    Dim objBLLLocation As New BALLocation
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


    Enum DepMonthConfig
        HalfMonth = 1
        FullMonth = 2
    End Enum

    Dim isEdit As Boolean = False
    Dim isEdit2 As Boolean = False

    'Asset permissions
    Private AllowAssetCreation As Boolean = False
    Private AllowUpdateExistedAsset As Boolean = False
    Private AllowDeleteAsset As Boolean = False
    Private AllowDispose As Boolean = False
#End Region
    Private Sub RegisterEditionItems()
        'Kabeer Registration
        'If Not AppConfig.ISRegistered Or AppConfig.AppEdition = ApplicationEditions.NotRegistered Then
        '    Application.Exit()
        'End If

        Select Case AppConfig.AppEdition
            Case ApplicationEditions.Inventory
            Case ApplicationEditions.Tracking
                tabAdditionalCost.Visible = False
                tabDepInfo.PageVisible = False
                grpPOInfo.Visible = False
            Case ApplicationEditions.Financial
                grpPOInfo.Visible = False
            Case ApplicationEditions.Enterprise
                'do nothing
        End Select
    End Sub

    Public FrmCop As frmBlkQty

    Private Sub CheckDemo()
        If AppConfig.IsDemoKey Then
            'if asset details recrod count more than 10 then close the application

            Dim obj As New BALAssetDetails
            Dim leftcount As Integer = DemoAssetsCount - obj.GetAssetsCount(New attAssetDetails)
            lblEvaluation.Visible = True
            lblEvaluation.Text = leftcount & " records left"
            If obj.GetAssetsCount(New attAssetDetails) > DemoAssetsCount Then
                ZulMessageBox.ShowMe("DemoAssetCount")
                Application.Exit()
            End If
        End If
    End Sub

    Public Sub ShowAnonymousInfo(ByVal AnonymousId As String, ByVal Desc As String, ByVal LocID As String, ByVal CatID As String, ByVal Modle As String, ByVal serial As String, ByVal DeviceID As String, ByVal TransDate As DateTime, ByVal InvID As Integer)
        AssetsID.Tag = "NewID"
        AssetsID.SelectedText = objBALAssetDetails.Generate_AssetID()
        ZTLocation.SelectedValue = LocID
        ZTLocation.SelectedText = objBALLocation.Comp_Path(LocID)

        txtAstDesc.Text = Desc
        txtAstModel.Text = Modle
        txtSerialNo.Text = serial

        If Not AppConfig.CodingMode Then
            txtAstNum.Text = Generate_AssetNumber("")
            txtRef.Text = txtAstNum.Text
        End If

        Me.AnonymousId = AnonymousId
        Me.DeviceID = DeviceID
        Me.TransDate = TransDate
        Me.InvScheduleId = InvID
    End Sub

    Private Sub frmAssetsDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.ActiveControl = cmbItemCode.TextBox

        CheckDemo()
        SetAssetPermissions()
        RegisterEditionItems()

        valProvMain.SetValidationRule(cmbGLCode.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(txtBrandID.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(txtAssetStatus.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(cmbCust.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(AssetsID.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(cmbItemCode.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(cmbComp.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(txtAstNum, valRulenotEmpty)
        valProvMain.SetValidationRule(ZTLocation.TextBox, valRulenotEmpty)

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

        'txtSalVal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        'txtSalVal.Properties.Mask.EditMask = "[01]"
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

        btnAddItem.Enabled = Check_Auth("frmAssets")
        btnAddCust.Enabled = Check_Auth("frmCustodian")
        btnAddBrand.Enabled = Check_Auth("frmBrand")
        btnAddSUp.Enabled = Check_Auth("frmSupplier")
        btnAddIns.Enabled = Check_Auth("frmInsurer")
        btnAddLoc.Enabled = Check_Auth("frmLocation")
        btnAddCostCenter.Enabled = Check_Auth("frmCostCenter")
        btnAddPO.Enabled = Check_Auth("frmPOMaster")
        btnNewCompany.Enabled = Check_Auth("frmCompany")
        btnAddGLCode.Enabled = Check_Auth("frmGLCode")

        'if the user has the permission to create new assets then press the create new asset button.
        If AllowAssetCreation Then
            'if the form is not created for anonymous assets then click new button.
            If AnonymousId = "" Then
                btnNewAsset.PerformClick()
            End If
        Else
            btnLast.PerformClick()
        End If
        Get_AddCostType()
        format_ExpectedBookAnnulGrid()
        format_ExpectedBookMonthlyGrid()
        GetCustomFieldsCaption()
        dtService.CustomFormat = AppConfig.MaindateFormat
        dtSaleDate.CustomFormat = AppConfig.MaindateFormat
        dtDispdate.CustomFormat = AppConfig.MaindateFormat
        dtpur.CustomFormat = AppConfig.MaindateFormat
        dtBVUpdate.CustomFormat = AppConfig.MaindateFormat
    End Sub

    Private Sub GetCustomFieldsCaption()
        Dim objCustomFields As New BALCustomFields
        Dim dt As DataTable = objCustomFields.GetAll_CustomFields(New attCustomFields)
        For Each row As DataRow In dt.Rows
            'Get the layout of the control
            Dim items As Control() = Me.Controls.Find(row("ControlName"), True)
            If items.Length > 0 Then
                If items(0) IsNot Nothing Then
                    items(0).Text = row("EngCaption")
                End If
            End If
        Next
    End Sub



    Private Sub SetAssetPermissions()
        Dim ds As DataTable
        Dim objattRole As attRoles = New attRoles
        Dim objBALRole As New BALRoles
        objattRole.PKeyCode = MainForm.RoleID
        ds = objBALRole.GetAll_Roles(objattRole)
        If ds.Rows.Count > 0 Then
            AllowAssetCreation = ds.Rows(0)("Custom2")
            AllowUpdateExistedAsset = ds.Rows(0)("Custom3")
            AllowDeleteAsset = ds.Rows(0)("Custom4")
            AllowDispose = ds.Rows(0)("Custom5")

            btnNewAsset.Visible = AllowAssetCreation
            btnNewAsset1.Visible = AllowAssetCreation
            btnDuplicate.Visible = AllowAssetCreation
            btnDuplicate.Enabled = AllowAssetCreation
            btnSave.Visible = AllowUpdateExistedAsset

            btnDelete.Visible = AllowDeleteAsset
            btnDelete.Enabled = AllowDeleteAsset

            grpDisposal.Enabled = AllowDispose
        End If
    End Sub

    Private Function GetCustomFieldControl(ByVal ControlName As String, ByVal ParentControl As Control) As Control
        For Each ctl As Control In ParentControl.Controls
            If ctl.Name = ControlName Then
                Return ctl
            End If

            If ctl.HasChildren() Then
                GetCustomFieldControl(ControlName, ctl)
            End If
        Next
        Return Nothing
    End Function
    Public Sub LocateAsset(ByVal astID As String)
        If astID <> "" Then
            AssetsID.SelectedText = astID
        End If
    End Sub

    Private Sub frmAssets_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmAssetsDetails = Nothing
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmAssetsDetails = Nothing
            If Not Me.MdiParent Is Nothing Then
                Dim frm As frmMain
                frm = Me.MdiParent
                frm.EnableMenu.Invoke()
            End If


            Me.Close()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


#Region " -- Methods -- "

    'Private Sub Delete_DepreciationBook()
    '    Try
    '        objattAstBooks = New attAstBooks
    '        objattAstBooks.BookDescription = RemoveUnnecessaryChars(txtBookDesc.Text)
    '        objattAstBooks.BVUpdate = dtBVUpdate.Value
    '        objattAstBooks.CurrentBookValue = RemoveUnnecessaryChars(txtBookVal.Text)
    '        objattAstBooks.DepCode = txtDepText.Tag
    '        objattAstBooks.PKeyCode = RemoveUnnecessaryChars(txtDepAstID.Text) & "-" & RemoveUnnecessaryChars(txtBookID.Text)
    '        objattAstBooks.SalvageValue = RemoveUnnecessaryChars(txtSalVal.Text)
    '        objattAstBooks.SalvageYear = RemoveUnnecessaryChars(txtSalYr.Text)
    '        objattAstBooks.IsDelete = True
    '        objBALAstBooks.Delete_AstBooks(objattAstBooks)
    '    Catch ex As Exception
    '        GenericExceptionHandler(ex, WhoCalledMe)
    '    End Try
    'End Sub




#Region "Formating Grids"
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
        RIAstHist.SmallImages = MainForm.imgAssetStatus
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
            .Columns(16).Visible = False
            '.Columns("SalvageValuePercent").Visible = False
            '.Columns("SalvageValuePercent").Caption = "Salvage Value%"
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

            .Columns(5).Caption = "Acc. Dep"
            .Columns(5).Width = 85
            .Columns(5).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns(5).DisplayFormat.FormatString = "###,###,###,###,###.00"
            .Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(6).Caption = "Book Value"
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
            dt.Columns.Add("CurrDate", Type.GetType("System.DateTime"))
            dt.Columns.Add("StartValue", Type.GetType("System.Double"))
            dt.Columns.Add("Dep", Type.GetType("System.Double"))
            dt.Columns.Add("AccDep", Type.GetType("System.Double"))
            dt.Columns.Add("CBV", Type.GetType("System.Double"))
            grdExpectedBookAnnual.DataSource = dt
        End If

        With (grdExpectedBookAnnualView)
            .Columns("CurrDate").Caption = "Year"
            .Columns("CurrDate").Width = 80
            .Columns("CurrDate").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns("CurrDate").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns("CurrDate").DisplayFormat.FormatString = "yyyy"

            .Columns("StartValue").Caption = "Start Value"
            .Columns("StartValue").Width = 120
            .Columns("StartValue").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns("StartValue").DisplayFormat.FormatString = "###,###,###,###,###.00"
            .Columns("StartValue").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near


            .Columns("AccDep").Caption = "Accumulated Depreciation"
            .Columns("AccDep").Width = 120
            .Columns("AccDep").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns("AccDep").DisplayFormat.FormatString = "###,###,###,###,###.00"
            .Columns("AccDep").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

            .Columns("Dep").Caption = "Depreciation Expense"
            .Columns("Dep").Width = 100
            .Columns("Dep").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns("Dep").DisplayFormat.FormatString = "###,###,###,###,###.00"
            .Columns("Dep").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near


            .Columns("CBV").Caption = "End Value"
            .Columns("CBV").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns("CBV").DisplayFormat.FormatString = "###,###,###,###,###.00"
            .Columns("CBV").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
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
            dt.Columns.Add("CurrDate", Type.GetType("System.DateTime"))
            dt.Columns.Add("StartValue", Type.GetType("System.Double"))
            dt.Columns.Add("Dep", Type.GetType("System.Double"))
            dt.Columns.Add("AccDep", Type.GetType("System.Double"))
            dt.Columns.Add("CBV", Type.GetType("System.Double"))
            grdExpectedBookMonthly.DataSource = dt
        End If

        With (grdExpectedBookMonthlyView)
            .Columns("CurrDate").Caption = "Month"
            .Columns("CurrDate").Width = 80
            .Columns("CurrDate").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns("CurrDate").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns("CurrDate").DisplayFormat.FormatString = "MMM yyyy"

            .Columns("StartValue").Caption = "Start Value"
            .Columns("StartValue").Width = 120
            .Columns("StartValue").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns("StartValue").DisplayFormat.FormatString = "###,###,###,###,###.00"
            .Columns("StartValue").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near


            .Columns("AccDep").Caption = "Accumulated Depreciation"
            .Columns("AccDep").Width = 120
            .Columns("AccDep").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns("AccDep").DisplayFormat.FormatString = "###,###,###,###,###.00"
            .Columns("AccDep").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

            .Columns("Dep").Caption = "Depreciation Expense"
            .Columns("Dep").Width = 100
            .Columns("Dep").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns("Dep").DisplayFormat.FormatString = "###,###,###,###,###.00"
            .Columns("Dep").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near


            .Columns("CBV").Caption = "End Value"
            .Columns("CBV").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            .Columns("CBV").DisplayFormat.FormatString = "###,###,###,###,###.00"
            .Columns("CBV").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
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
            If dt Is Nothing = False Then
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
                    txtItemDesc.Text = Convert.ToString(dt.Rows(0)(2))
                    Return Nothing
                End If
            End If
            txtCategory.Text = ""
            txtItemDesc.Text = ""

            Return Nothing
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
            Return Nothing
        End Try
    End Function

    Private Sub Get_AstDep_History(ByVal _id As String, ByVal _BookID As String)
        Try
            Dim objBALBookHistory As New BALBookHistory
            Dim objattBookHistory As New attBookHistory
            objattBookHistory.ASTID = _id
            objattBookHistory.BookID = _BookID
            grdFiscalYRBook.DataSource = objBALBookHistory.GetAll_BookHistory(objattBookHistory)
            format_FiscalYRBookGrid()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub Get_Ast_History(ByVal _id As String)
        Try
            Dim objBALAst_History As New BALAst_History
            Dim objattAstHistory As New attAstHistory
            objattAstHistory.AstID = _id
            grdAstHist.DataSource = objBALAst_History.GetAll_Ast_History(objattAstHistory)
            format_AstHistGrid()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub Get_Ast_History_Custodian(ByVal _id As String)
        Try
            Dim objBALAst_Cust_history As New BALAst_Cust_history
            Dim objattAst_Cust_history As New attAst_Cust_history
            objattAst_Cust_history.AstID = _id
            grdCustHist.DataSource = objBALAst_Cust_history.GetAll_Ast_Cust_history(objattAst_Cust_history)
            format_CustHistGrid()

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
                    btnNewAsset_Click(Nothing, Nothing)
                Else
                    isEdit = True
                    txtPrice.Text = ""
                    txtSoldto.Text = ""
                    txtPreLoc.Text = ""
                    txtCurrentLoc.Text = ""
                    txtDepAstID.Text = AssetsID.SelectedText

                    dt.Rows(0)("InsName").ToString()
                    dt.Rows(0)("AstDesc").ToString()
                    dt.Rows(0)("AstBrandName").ToString()
                    dt.Rows(0)("CompanyName").ToString()
                    dt.Rows(0)("DispDesc").ToString()

                    txtbase.Text = Format(CDbl(dt.Rows(0)("BaseCost").ToString()), "###,###,###,###,###.00")

                    cmbCust.FindRow(dt.Rows(0)("CustodianID").ToString(), dt.Rows(0)("CustodianName").ToString())
                    cmbIns.FindRow(dt.Rows(0)("InsID").ToString(), dt.Rows(0)("InsName").ToString())
                    cmbItemCode.FindRow(dt.Rows(0)("ItemCode").ToString(), dt.Rows(0)("ItemCode").ToString())
                    txtBrandID.FindRow(dt.Rows(0)("AstBrandId").ToString(), dt.Rows(0)("AstBrandName").ToString())
                    txtAssetStatus.FindRow(dt.Rows(0)("StatusID").ToString(), dt.Rows(0)("Status").ToString())
                    cmbComp.FindRow(dt.Rows(0)("CompanyID").ToString(), dt.Rows(0)("CompanyName").ToString())
                    cmbPO.FindRow(dt.Rows(0)("POCOde").ToString(), dt.Rows(0)("POCOde").ToString())
                    cmbSupp.FindRow(dt.Rows(0)("SuppID").ToString(), dt.Rows(0)("SuppName").ToString())
                    txtPieceNo.Text = dt.Rows(0)("NoPiece").ToString()
                    If dt.Rows(0)("NoPiece").ToString() <> "1" Then
                        txtPieceNo.Visible = True
                        Label3.Visible = True
                        cmbType.SelectedIndex = 1
                    Else
                        txtPieceNo.Visible = False
                        Label3.Visible = False
                        cmbType.SelectedIndex = 0
                    End If
                    txtSerialNo.Text = dt.Rows(0)("SerailNo").ToString()
                    txtInvoice.Text = dt.Rows(0)("InvNumber").ToString()
                    txtDiscount.Text = dt.Rows(0)("Discount").ToString()
                    txtAstDesc.Text = dt.Rows(0)("AstDesc").ToString()
                    txtAstDesc2.Text = dt.Rows(0)("AstDesc2").ToString()
                    txtAstModel.Text = dt.Rows(0)("AstModel").ToString()
                    txtRemarks.Text = dt.Rows(0)("TransRemarks").ToString()
                    txtOldAssetID.Text = dt.Rows(0)("OldAssetID").ToString()
                    txtLabelPrintedCnt.Text = dt.Rows(0)("LabelCount").ToString()
                    txtBarCode.Text = dt.Rows(0)("BarCode").ToString()
                    cmbItemCode.Text = dt.Rows(0)("SuppID").ToString()
                    dtService.Value = CDate(dt.Rows(0)("SrvDate").ToString)
                    txtCAPEX.Text = dt.Rows(0)("Capex").ToString()
                    txtPOERP.Text = dt.Rows(0)("PoErp").ToString()
                    txtPlate.Text = dt.Rows(0)("Plate").ToString()
                    txtGRN.Text = dt.Rows(0)("GRN").ToString()
                    txtRefCode.Text = dt.Rows(0)("RefCode").ToString()

                    cmbCostCenter.FindRow(dt.Rows(0)("CostID").ToString(), dt.Rows(0)("CostName").ToString())
                    If Not dt.Rows(0).IsNull("CapitalizationDate") Then
                        dtCapitalizationDate.Checked = True
                        dtCapitalizationDate.Value = dt.Rows(0)("CapitalizationDate")
                    Else
                        dtCapitalizationDate.Checked = False
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
                    txtDispComments.Text = dt.Rows(0)("DisposalComments").ToString()


                    txtSales.Text = Format(CDbl(dt.Rows(0)("Tax").ToString()), "###,###,###,###,###.00")
                    txtTotal.Text = Format(CType(txtbase.Text, Double) + CType(txtSales.Text, Double), "###,###,###,###,###.00")

                    PreviousItemCost = CType(txtbase.Text.Trim, Decimal) + CType(txtSales.Text.Trim, Decimal)

                    'BookId = dt.Rows(0)("BookID").ToString()
                    txtRef.Text = dt.Rows(0)("RefNo").ToString()
                    txtAstNum.Text = dt.Rows(0)("AstNum").ToString()

                    'AssetsID.SelectedText = dt.Rows(0)("AstID").ToString()


                    dtpur.Value = CDate(dt.Rows(0)("PurDate").ToString())

                    If (dt.Rows(0)("GLCode").ToString <> "" Or dt.Rows(0)("GLCode").ToString Is Nothing) Then
                        cmbGLCode.SelectedText = dt.Rows(0)("GLCode").ToString()
                    Else
                        cmbGLCode.SelectedText = ""
                    End If

                    If (dt.Rows(0)("PONumber").ToString <> "") Then
                        txtPONumber.Text = dt.Rows(0)("PONumber").ToString() ''''''''''''''''''''''
                    Else
                        txtPONumber.Text = ""
                    End If

                    If (dt.Rows(0)("LocId").ToString <> "") Then
                        ZTLocation.SetValue(dt.Rows(0)("LocId").ToString(), objBALLocation.Comp_Path(dt.Rows(0)("LocId").ToString()))
                    Else
                        ZTLocation.SelectedText = ""
                        ZTLocation.SelectedValue = ""
                    End If

                    'when item is disposed
                    If CType(dt.Rows(0)("Disposed"), Boolean) Then

                        cmdDisposal.Enabled = True
                        dtDispdate.Enabled = True
                        cmdDisposal.FindRow(dt.Rows(0)("Dispcode").ToString(), dt.Rows(0)("DispDesc").ToString())
                        chkDisposed.Checked = True
                        If dt.Rows(0)("DispDate").ToString() <> "" Then dtDispdate.Value = CDate(dt.Rows(0)("DispDate"))
                        If dt.Rows(0)("Dispcode") = "1" Then
                            cmdDisposal.Enabled = False
                            Panel1.Visible = True
                            If dt.Rows(0)("Sel_date").ToString() <> "" Then dtSaleDate.Value = CDate(dt.Rows(0)("Sel_date"))
                            If dt.Rows(0)("Sel_Price").ToString() <> "" Then txtPrice.Text = Format(CDbl(dt.Rows(0)("Sel_Price")), "###,###,###,###,###.00")
                            If dt.Rows(0)("Soldto").ToString() <> "" Then txtSoldto.Text = dt.Rows(0)("Soldto")
                        Else
                            dtSaleDate.Value = Now.Date
                            txtPrice.Text = ""
                            txtSoldto.Text = ""
                            Panel1.Visible = False
                        End If
                        'when item is NOT disposed 
                    Else

                        chkDisposed.Checked = False
                        cmdDisposal.Text = ""
                        cmdDisposal.Enabled = False
                        dtDispdate.Enabled = False
                        Panel1.Visible = False
                        Panel1.Refresh()
                    End If

                    Get_AstBooks(AssetsID.SelectedText)
                    grdBooksView_FocusedRowChanged(Nothing, Nothing) '

                    Get_Ast_History(AssetsID.SelectedText)
                    Get_Ast_History_Custodian(AssetsID.SelectedText)

                    Get_AssetAddCosts(AssetsID.SelectedText)
                    Dim addcostcnt As Integer = grdAddCostView.RowCount
                    btnNewAddCost_Click(Nothing, Nothing)
                    btnSaveAddCost.Enabled = True
                    btnNewAddCost.Enabled = True

                    Get_AssetWarranty(AssetsID.SelectedText)
                    btnNewWarranty_Click(Nothing, Nothing)
                    btnSaveWarranty.Enabled = True
                    btnNewWarranty.Enabled = True
                    btnDeleteWarranty.Enabled = True


                    If grdFiscalYRBookView.RowCount > 0 Then
                        dtService.Enabled = False

                        txtSales.Properties.ReadOnly = True
                        txtbase.Properties.ReadOnly = True

                        txtbase.ShowToolTips = True
                        txtSales.ShowToolTips = True
                    Else
                        dtService.Enabled = True

                        txtbase.Properties.ReadOnly = False
                        txtbase.ShowToolTips = False

                        'to don't allow the user to modify salestextbox after adding additional cost on grid.
                        If addcostcnt > 0 Then
                            txtSales.Properties.ReadOnly = True
                            txtSales.ShowToolTips = True
                        Else
                            txtSales.Properties.ReadOnly = False
                            txtSales.ShowToolTips = False
                        End If
                    End If


                    LoadImage()
                    'isEdit = True
                    btnDelete.Visible = True
                    btnDuplicate.Visible = True
                    btnPrintLabel.Visible = True

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
                cmdDisposal.Enabled = True
                cmdDisposal.EnabledME(True)
                dtDispdate.Enabled = True
                txtDispComments.Enabled = True
                If cmdDisposal.Text = "1" Then
                    Panel1.Visible = True
                End If
            Else
                cmdDisposal.Enabled = False
                cmdDisposal.EnabledME(False)
                dtDispdate.Enabled = False
                txtDispComments.Enabled = False
                Panel1.Visible = False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub btnUpdateBook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateBook.Click
        If valProvSecondary.Validate And valProvNotNigative.Validate And txtSalValPercent.ErrorText = "" Then
            Dim intVal As Integer
            txtSalPercent.Text = CalculatePercent(txtSalYr.Text, txtSalMonth.Text)
            intVal = CInt(txtSalPercent.Text)
            If intVal < 0 Or intVal > 100 Then
                errProv.SetError(txtSalPercent, My.MessagesResource.Messages.OutPercentRange)
                Return
            Else
                errProv.ClearErrors()
            End If

            Update_DepreciationBook(True)
        End If
    End Sub

    Private Sub PBLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PBLogo.Click
        Try
            If dlgFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
                PBLogo.ImageLocation = dlgFile.FileName
                btnDelImg.Visible = True
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
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
            Dim LocCompCode As String = objBalLocation.GetLocCompCode(dt.Rows(0)("LocID").ToString())


            Dim strAstID As String = ""
            Dim strAstNum As String = ""
            Dim NumSucceededCopies As Integer = 0

            FrmCop.pb.Maximum = numofCop
            FrmCop.pb.Step = 1
            Dim AssetIDs As String = ""

            For idx As Integer = 0 To numofCop - 1
                strAstID = objBALAssetDetails.Generate_AssetID()
                strAstNum = Generate_AssetNumber(objattAssetDetails.CompanyID)
                If strAstNum = "" Then
                    Exit For
                End If
                objattAssetDetails.PKeyCode = strAstID
                objattAssetDetails.AstNum = strAstNum
                objattAssetDetails.BarCode = Generate_BarCode(objattAssetDetails.CompanyID, Trim(strAstID), Trim(strAstNum), objattAssetDetails.RefNo, CategoryText, CategoryText, LocationText, LocationText, LocCompCode)
                objattAssetDetails.RefNo = dt.Rows(0)("RefNo").ToString()
                If objattAssetDetails.RefNo = "" Then
                    objattAssetDetails.RefNo = objattAssetDetails.AstNum
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

                If AppConfig.ExportToServer Then
                    Dim Division As String = objBALAssetDetails.Get_Cust_DeptID(objattAssetDetails.CustodianID)
                    Dim Category As String = objBALAssetDetails.Get_Assets_AstCatID(objattAssetDetails.ItemCode)
                    objAlHadaIntegration.Insert_AssetDetails_ExportServer(objattAssetDetails, Division, Category)
                End If

                Dim objBALAssets As New BALItems
                Dim ds As DataTable = objBALAssets.Get_DepPolicy(objattAssetDetails.ItemCode)
                If Not ds Is Nothing Then
                    If ds.Rows.Count > 0 Then
                        objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, ds.Rows(0)("SalvageValue"), ds.Rows(0)("SalvageYear"), ds.Rows(0)("SalvageMonth"), dtService.Value, ds.Rows(0)("IsSalvageValuePercent"))
                    End If
                End If

                ''Create history 
                'Removed on1/10/2018 No need to add records to old inventory, it will cause problems as it will show missing but it's not invented.
                'For Each dr As DataRow In dtInvSch.Rows
                '    objattAstHistory = New attAstHistory
                '    objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                '    objattAstHistory.AstID = strAstID
                '    objattAstHistory.Status = 0
                '    objattAstHistory.InvSchCode = dr("InvSchCode")
                '    objattAstHistory.HisDate = DateTime.Now
                '    objattAstHistory.Fr_loc = dt.Rows(0)("LocID")
                '    objattAstHistory.To_Loc = dt.Rows(0)("LocID")
                '    objattAstHistory.NoPiece = dt.Rows(0)("NoPiece")
                '    objBALAst_History.Insert_Ast_History(objattAstHistory)
                'Next

                AssetIDs += String.Format("'{0}',", strAstID)
                Application.DoEvents()
                FrmCop.pb.PerformStep()
                NumSucceededCopies += 1
            Next
            Dim NumFailedCopies As Integer = numofCop - NumSucceededCopies
            If MessageBox.Show(CStr(NumSucceededCopies) & " Assets added successfully," & Chr(13) _
                             & CStr(NumFailedCopies) & " Assets failed to add. Do you want to print Labels for the generated assets?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                AssetIDs = AssetIDs.Remove(AssetIDs.Length - 1, 1)
                'Get the data for the selected Rows from the database.
                Dim rpt As New XtraReport
                rpt = LoadReport("Assets Barcode")
                Dim dtPrint As DataTable = objBALAssetDetails.GetAssetLabelData(AssetIDs)
                rpt.DataSource = dtPrint
                rpt.Print()
                Dim arrAssetIDS As String() = AssetIDs.Split(",")

                For i As Integer = 0 To arrAssetIDS.Length - 1
                    Dim PrintedAstID As String = arrAssetIDS(i)
                    objBALAssetDetails.Update_LabelCount(PrintedAstID.Replace("'", ""))
                Next
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnDuplicate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDuplicate.Click
        'disallow coping dispsed assets.
        If objBALAssetDetails.IsAssetDisposed(AssetsID.SelectedText) Then
            ZulMessageBox.ShowMe("CanNotCopyDisposes")
        Else
            FrmCop = New frmBlkQty
            FrmCop.frm = Me
            FrmCop.ShowDialog()
            FrmCop.Dispose()
            btnNewAsset_Click(sender, e)
        End If
    End Sub

    Private Sub btnPrintLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintLabel.Click

        Try
            If Not String.IsNullOrEmpty(txtAstNum.Text) Then
                Dim objBALAssetDetails As New BALAssetDetails
                Dim rpt As New DevExpress.XtraReports.UI.XtraReport
                rpt = LoadReport("Assets Barcode")
                If MessageBox.Show("Are you sure to print Asset Label for current record?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                    Dim dt As DataTable = objBALAssetDetails.GetAssetLabelData(AssetsID.SelectedText)
                    rpt.DataSource = dt
                    rpt.Print()
                    If objBALAssetDetails.Update_LabelCount(AssetsID.SelectedText) Then
                        txtLabelPrintedCnt.Text = CInt(txtLabelPrintedCnt.Text) + 1
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally

        End Try
    End Sub

    'Private Sub FileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles FileDialog1.FileOk
    '    Try
    '        'Dim fname As String
    '        'fname = AssetsID.SelectedText & ".jpg"
    '        PBLogo.ImageLocation = FileDialog1.FileName
    '        'Check_ImageFolder()
    '        'File.Copy(FileDialog1.FileName, AppConfig.ImgPath & "\" & fname, True)
    '        'PBLogo.ImageLocation = AppConfig.ImgPath & "\" & fname
    '        'If fname <> "" Then
    '        'btnDelImg.Visible = True
    '        'End If
    '    Catch ex As Exception
    '        GenericExceptionHandler(ex, WhoCalledMe)
    '    End Try
    'End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            CheckDemo()

            errProv.ClearErrors()
            If valProvMain.Validate And valProvNotNigative.Validate Then
                If Date.Compare(dtpur.Value.ToShortDateString, dtService.Value.ToShortDateString) > 0 Then
                    ZulMessageBox.ShowMe("PurchaseDateShouldBeLessThanServiceDate")
                    Exit Sub
                End If


                Dim objattBookTemp As New attBook
                Dim objBALBookTemp As New BALBooks
                Dim objattAstBooks As New attAstBooks
                Dim ds As DataTable
                objattBookTemp.CompanyID = cmbComp.SelectedValue
                ds = objBALBookTemp.GetAll_Book(objattBookTemp)
                If ds Is Nothing Or ds.Rows.Count < 1 Then
                    ZulMessageBox.ShowMe("NoCompanyBookSelected")
                    Exit Sub
                End If

                'if the ref number is empty we assign it the astnum.
                If txtRef.Text.Trim = "" Then
                    txtRef.Text = txtAstNum.Text
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

                        btnSaveWarranty.Enabled = True
                        btnNewWarranty.Enabled = True
                        btnDeleteWarranty.Enabled = True
                    Catch ex As Exception
                        GenericExceptionHandler(ex, WhoCalledMe)
                    End Try
                Else
                    errProv.SetError(txtTotal, My.MessagesResource.Messages.InvalidCost)
                End If
            Else
                ZulMessageBox.ShowMe("NotSaved")
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
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
        If File.Exists(PBLogo.ImageLocation) Then
            If AppConfig.ImgStorgeLoc = "Shared Folder" Then
                Dim fname As String = ""
                If AppConfig.ImgType = "Asset Images" Then
                    fname = AssetsID.SelectedText & ".jpg"
                ElseIf AppConfig.ImgType = "Item Images" Then
                    fname = cmbItemCode.SelectedValue & ".jpg"
                End If

                If PBLogo.ImageLocation <> AppConfig.ImgPath & "\" & fname Then
                    File.Copy(PBLogo.ImageLocation, AppConfig.ImgPath & "\" & fname, True)
                    PBLogo.ImageLocation = AppConfig.ImgPath & "\" & fname
                End If

            ElseIf AppConfig.ImgStorgeLoc = "Database" Then
                If AppConfig.ImgType = "Asset Images" Then
                    objattAssetDetails = New attAssetDetails
                    objattAssetDetails.PKeyCode = AssetsID.SelectedText
                    Dim fs As New FileStream(PBLogo.ImageLocation, FileMode.Open)
                    Dim Data() As Byte = New [Byte](fs.Length) {}
                    fs.Read(Data, 0, fs.Length)
                    objattAssetDetails.Image = Data
                    objBALAssetDetails.UpdateAssetImage(objattAssetDetails)
                    fs.Dispose()
                ElseIf AppConfig.ImgType = "Item Images" Then
                    objattAssets = New attItems
                    objattAssets.PKeyCode = cmbItemCode.SelectedValue
                    Dim fs As New FileStream(PBLogo.ImageLocation, FileMode.Open)
                    Dim Data() As Byte = New [Byte](fs.Length) {}
                    fs.Read(Data, 0, fs.Length)
                    objattAssets.Image = Data
                    objBALAssets.UpdateItemImage(objattAssets)
                    fs.Dispose()
                End If
            End If
        End If
    End Sub
    Private Sub DeleteImage()
        Dim fname As String = ""
        If AppConfig.ImgStorgeLoc = "Shared Folder" Then

            If AppConfig.ImgType = "Asset Images" Then
                fname = AssetsID.SelectedText & ".jpg"
            ElseIf AppConfig.ImgType = "Item Images" Then
                fname = cmbItemCode.SelectedValue & ".jpg"
            End If

            If File.Exists(AppConfig.ImgPath & "\" & fname) Then
                File.Delete(AppConfig.ImgPath & "\" & fname)
            End If
        ElseIf AppConfig.ImgStorgeLoc = "Database" Then
            If AppConfig.ImgType = "Asset Images" Then
                objattAssetDetails = New attAssetDetails
                objattAssetDetails.PKeyCode = AssetsID.SelectedText
                objBALAssetDetails.DeleteAssetImage(objattAssetDetails)
            ElseIf AppConfig.ImgType = "Item Images" Then
                objattAssets = New attItems
                objattAssets.PKeyCode = cmbItemCode.SelectedValue
                objBALAssets.DeleteItemImage(objattAssets)
            End If
        End If
        btnDelImg.Visible = False
        PBLogo.Image = Nothing
    End Sub
    Private Sub LoadImage()
        Try
            Dim fname As String = ""
            If AppConfig.ImgStorgeLoc = "Shared Folder" Then
                If AppConfig.ImgType = "Asset Images" Then
                    fname = AssetsID.SelectedText & ".jpg"

                ElseIf AppConfig.ImgType = "Item Images" Then
                    fname = cmbItemCode.SelectedValue & ".jpg"
                End If
                If File.Exists(AppConfig.ImgPath & "\" & fname) Then
                    PBLogo.ImageLocation = AppConfig.ImgPath & "\" & fname
                    btnDelImg.Visible = True
                Else
                    btnDelImg.Visible = False
                    PBLogo.Image = Nothing
                    PBLogo.ImageLocation = Nothing
                End If

            ElseIf AppConfig.ImgStorgeLoc = "Database" Then
                If AppConfig.ImgType = "Asset Images" Then
                    objattAssetDetails = New attAssetDetails
                    objattAssetDetails.PKeyCode = AssetsID.SelectedText
                    Dim bits As Byte() = objBALAssetDetails.GetAssetImage(objattAssetDetails)
                    If bits IsNot Nothing Then
                        Dim ms As New MemoryStream(bits, 0, bits.Length)
                        PBLogo.Image = Image.FromStream(ms, True)
                        btnDelImg.Visible = True
                    Else
                        btnDelImg.Visible = False
                        PBLogo.Image = Nothing
                        PBLogo.ImageLocation = Nothing
                    End If
                ElseIf AppConfig.ImgType = "Item Images" Then
                    objattAssets = New attItems
                    objattAssets.PKeyCode = cmbItemCode.SelectedValue
                    Dim bits As Byte() = objBALAssets.GetItemImage(objattAssets)
                    If bits IsNot Nothing Then
                        Dim ms As New MemoryStream(bits, 0, bits.Length)
                        PBLogo.Image = Image.FromStream(ms, True)
                        btnDelImg.Visible = True
                    Else
                        btnDelImg.Visible = False
                        PBLogo.Image = Nothing
                        PBLogo.ImageLocation = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub update_AssetDetails()
        Try
            If Not objBALAssetDetails.Check_referenceID(txtRef.Text.ToString(), AssetsID.SelectedText) Then
                objattAssetDetails = New attAssetDetails
                objattAssetDetails.BaseCost = txtbase.Text
                objattAssetDetails.CustodianID = cmbCust.SelectedValue

                If cmbIns.SelectedText <> "" Then
                    objattAssetDetails.InsID = cmbIns.SelectedValue
                End If

                If cmbSupp.SelectedText <> "" Then
                    objattAssetDetails.SuppID = cmbSupp.SelectedValue
                End If

                If cmbPO.SelectedText <> "" Then
                    objattAssetDetails.POCode = cmbPO.SelectedValue
                End If
                objattAssetDetails.SerailNo = RemoveUnnecessaryChars(txtSerialNo.Text)
                objattAssetDetails.InvNumber = RemoveUnnecessaryChars(txtInvoice.Text)
                objattAssetDetails.ItemCode = RemoveUnnecessaryChars(cmbItemCode.SelectedValue)
                objattAssetDetails.PurDate = dtpur.Value
                objattAssetDetails.CompanyID = cmbComp.SelectedValue
                objattAssetDetails.TransRemarks = RemoveUnnecessaryChars(txtRemarks.Text)
                objattAssetDetails.Tax = txtSales.Text
                objattAssetDetails.SrvDate = dtService.Value
                objattAssetDetails.AstBrandID = txtBrandID.SelectedValue
                objattAssetDetails.StatusID = txtAssetStatus.SelectedValue

                objattAssetDetails.AstDesc = RemoveUnnecessaryChars(txtAstDesc.Text)
                objattAssetDetails.AstDesc2 = RemoveUnnecessaryChars(txtAstDesc2.Text)
                objattAssetDetails.AstModel = RemoveUnnecessaryChars(txtAstModel.Text)
                objattAssetDetails.Discount = RemoveUnnecessaryChars(txtDiscount.Text)
                objattAssetDetails.Capex = RemoveUnnecessaryChars(txtCAPEX.Text)
                objattAssetDetails.PoErp = RemoveUnnecessaryChars(txtPOERP.Text)
                objattAssetDetails.Plate = RemoveUnnecessaryChars(txtPlate.Text)
                objattAssetDetails.GRN = RemoveUnnecessaryChars(txtGRN.Text)
                objattAssetDetails.RefCode = RemoveUnnecessaryChars(txtRefCode.Text)
                objattAssetDetails.GLCode = cmbGLCode.SelectedText
                objattAssetDetails.PONumber = RemoveUnnecessaryChars(txtPONumber.Text)


                objattAssetDetails.CostCenterID = cmbCostCenter.SelectedValue
                If dtCapitalizationDate.Checked Then
                    objattAssetDetails.CapitalizationDate = dtCapitalizationDate.Value.Date
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

                objattAssetDetails.LocID = ZTLocation.SelectedValue

                Dim locCompCode As String = objBALLocation.GetLocCompCode(objattAssetDetails.LocID)

                objattAssetDetails.RefNo = RemoveUnnecessaryChars(txtRef.Text)
                objattAssetDetails.AstNum = txtAstNum.Text.ToString()

                If chkDisposed.Checked Then
                    If cmdDisposal.SelectedText <> "" Then

                        objattAssetDetails.DispCode = cmdDisposal.SelectedValue
                        If Trim(dtDispdate.Text) = "" Then

                            objattAssetDetails.DispDate = Nothing
                        Else

                            objattAssetDetails.DispDate = dtDispdate.Value.Date
                        End If

                        objattAssetDetails.Disposed = True
                        objBALAssetDetails.DisposeAsset_Book(AssetsID.SelectedText)
                        If objattAssetDetails.DispCode = "1" Then
                            objattAssetDetails.IsSold = True
                            objattAssetDetails.Sel_Date = dtSaleDate.Value.Date
                            objattAssetDetails.Sel_Price = txtPrice.Text
                            objattAssetDetails.SoldTo = txtSoldto.Text
                        End If
                    End If
                End If

                objattAssetDetails.PKeyCode = AssetsID.SelectedText
                txtBarCode.Text = Generate_BarCode(Trim(cmbComp.SelectedValue), Trim(AssetsID.SelectedText), Trim(txtAstNum.Text), Trim(txtRef.Text), Trim(txtCategory.Text), Trim(txtCategory.Text), Trim(ZTLocation.SelectedText), Trim(ZTLocation.SelectedText), locCompCode)
                objattAssetDetails.BarCode = txtBarCode.Text
                objattAssetDetails.LastEditBY = AppConfig.LoginName
                objattAssetDetails.LastEditDate = Now.Date
                objattAssetDetails.DisposalComments = txtDispComments.Text.Trim

                objBALAssetDetails.Update_AssetDetails(objattAssetDetails)
                If AppConfig.ExportToServer Then
                    Dim Division As String = objBALAssetDetails.Get_Cust_DeptID(objattAssetDetails.CustodianID)
                    Dim Category As String = objBALAssetDetails.Get_Assets_AstCatID(objattAssetDetails.ItemCode)
                    objAlHadaIntegration.Update_AssetDetails_ExportServer(objattAssetDetails, Division, Category)
                End If

                Update_DepreciationBook(False)
                PreviousItemCost = CType(txtbase.Text.Trim, Decimal) + CType(txtSales.Text.Trim, Decimal)

                ZulMessageBox.ShowMe("Saved")
            Else
                txtRef.Focus()
                txtRef.Text = ""
                ZulMessageBox.ShowMe("RefIDExist")
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub AddNew_AssetDetails()
        Try
            If Not objBALAssetDetails.Check_referenceID(txtRef.Text.ToString(), "") Then

                objattAssetDetails = New attAssetDetails
                objattAssetDetails.BaseCost = txtbase.Text
                objattAssetDetails.CustodianID = cmbCust.SelectedValue

                If cmbIns.SelectedText <> "" Then
                    objattAssetDetails.InsID = cmbIns.SelectedValue
                End If

                If cmbSupp.SelectedText <> "" Then
                    objattAssetDetails.SuppID = cmbSupp.SelectedValue
                End If

                If cmbPO.SelectedText <> "" Then
                    objattAssetDetails.POCode = cmbPO.SelectedValue
                End If

                objattAssetDetails.SerailNo = RemoveUnnecessaryChars(txtSerialNo.Text)
                objattAssetDetails.Discount = RemoveUnnecessaryChars(txtDiscount.Text)
                objattAssetDetails.InvNumber = RemoveUnnecessaryChars(txtInvoice.Text)
                objattAssetDetails.ItemCode = cmbItemCode.SelectedValue
                objattAssetDetails.PurDate = dtpur.Value
                objattAssetDetails.Tax = RemoveUnnecessaryChars(txtSales.Text)
                objattAssetDetails.TransRemarks = RemoveUnnecessaryChars(txtRemarks.Text)
                objattAssetDetails.DispDate = Date.MinValue
                objattAssetDetails.CompanyID = cmbComp.SelectedValue
                objattAssetDetails.SrvDate = dtService.Value
                objattAssetDetails.RefNo = RemoveUnnecessaryChars(txtRef.Text)
                ' TODO: check generating the asset number without the option:
                'If AppConfig.CodingMode Then
                txtAstNum.Text = Generate_AssetNumber(cmbComp.SelectedValue)
                'End If
                objattAssetDetails.AstNum = RemoveUnnecessaryChars(txtAstNum.Text)
                objattAssetDetails.AstBrandID = txtBrandID.SelectedValue
                objattAssetDetails.StatusID = txtAssetStatus.SelectedValue
                objattAssetDetails.AstDesc = RemoveUnnecessaryChars(txtAstDesc.Text)
                objattAssetDetails.AstDesc2 = RemoveUnnecessaryChars(txtAstDesc2.Text)
                objattAssetDetails.AstModel = RemoveUnnecessaryChars(txtAstModel.Text)
                objattAssetDetails.Capex = RemoveUnnecessaryChars(txtCAPEX.Text)
                objattAssetDetails.PoErp = RemoveUnnecessaryChars(txtPOERP.Text)
                objattAssetDetails.Plate = RemoveUnnecessaryChars(txtPlate.Text)
                objattAssetDetails.GRN = RemoveUnnecessaryChars(txtGRN.Text)
                objattAssetDetails.RefCode = RemoveUnnecessaryChars(txtRefCode.Text)
                objattAssetDetails.GLCode = cmbGLCode.SelectedText
                objattAssetDetails.PONumber = RemoveUnnecessaryChars(txtPONumber.Text)

                objattAssetDetails.CostCenterID = cmbCostCenter.SelectedValue
                If dtCapitalizationDate.Checked Then
                    objattAssetDetails.CapitalizationDate = dtCapitalizationDate.Value.Date
                Else
                    objattAssetDetails.CapitalizationDate = Nothing
                End If
                objattAssetDetails.BussinessArea = txtBussinessArea.Text
                objattAssetDetails.InventoryNumber = txtInventoryNumber.Text
                objattAssetDetails.CreatedBY = txtCreatedBY.Text
                objattAssetDetails.InStockAsset = Not chkAssetWithValue.Checked
                objattAssetDetails.IsDataChanged = True


                objattAssetDetails.LocID = ZTLocation.SelectedValue
                Dim locCompCode As String = objBALLocation.GetLocCompCode(objattAssetDetails.LocID)
                If txtPieceNo.Visible = True Then
                    objattAssetDetails.NoPiece = txtPieceNo.Text
                Else
                    objattAssetDetails.NoPiece = 1
                End If

                objattAssetDetails.Disposed = False
                objattAssetDetails.IsSold = False
                objattAssetDetails.PKeyCode = AssetsID.SelectedText

                If chkDisposed.Checked Then
                    If cmdDisposal.SelectedText <> "" Then
                        objBALAssetDetails.DisposeAsset_Book(AssetsID.SelectedText)
                        objattAssetDetails.DispCode = cmdDisposal.SelectedValue
                        objattAssetDetails.DispDate = dtDispdate.Value.Date
                        objattAssetDetails.Disposed = 1

                        If objattAssetDetails.DispCode = "1" Then
                            objattAssetDetails.IsSold = 1
                            objattAssetDetails.Sel_Date = dtSaleDate.Value.Date
                            objattAssetDetails.Sel_Price = txtPrice.Text
                            objattAssetDetails.SoldTo = txtSoldto.Text
                        End If
                    End If
                End If

                txtBarCode.Text = Generate_BarCode(Trim(cmbComp.SelectedValue), Trim(AssetsID.SelectedText), Trim(txtAstNum.Text), Trim(txtRef.Text), Trim(txtCategory.Text), Trim(txtCategory.Text), Trim(ZTLocation.SelectedText), Trim(ZTLocation.SelectedText), locCompCode)
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
                objattAssetDetails.DisposalComments = txtDispComments.Text.Trim

                If objBALAssetDetails.Insert_AssetDetails(objattAssetDetails, True) Then
                    If AppConfig.ExportToServer Then
                        Dim Division As String = objBALAssetDetails.Get_Cust_DeptID(objattAssetDetails.CustodianID)
                        Dim Category As String = objBALAssetDetails.Get_Assets_AstCatID(objattAssetDetails.ItemCode)
                        objAlHadaIntegration.Insert_AssetDetails_ExportServer(objattAssetDetails, Division, Category)
                    End If

                    ''Create history for all inventory schedules.
                    'Removed on1/10/2018 No need to add records to old inventory, it will cause problems as it will show missing but it's not invented.
                    'Dim objBALAst_INV_Schedule As New BALAst_INV_Schedule
                    'Dim dtInvSch As DataTable = objBALAst_INV_Schedule.Getcombo_Schedule()
                    'Dim objattAstHistory As attAstHistory
                    'Dim objBALAst_History As New BALAst_History
                    'For Each dr As DataRow In dtInvSch.Rows
                    '    objattAstHistory = New attAstHistory
                    '    objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                    '    objattAstHistory.AstID = AssetsID.SelectedText
                    '    objattAstHistory.Status = 0
                    '    objattAstHistory.InvSchCode = dr("InvSchCode")
                    '    objattAstHistory.HisDate = DateTime.Now
                    '    objattAstHistory.Fr_loc = ZTLocation.SelectedValue
                    '    objattAstHistory.To_Loc = ZTLocation.SelectedValue
                    '    objattAstHistory.NoPiece = txtPieceNo.Value
                    '    objBALAst_History.Insert_Ast_History(objattAstHistory)
                    'Next

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
                        objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, ds.Rows(0)("SalvageValue"), ds.Rows(0)("SalvageYear"), ds.Rows(0)("SalvageMonth"), dtService.Value, ds.Rows(0)("IsSalvageValuePercent"))
                        ESalValue = ds.Rows(0)("SalvageValue")
                        ESalMonth = ds.Rows(0)("SalvageMonth")
                        ESalYear = ds.Rows(0)("SalvageYear")
                    End If
                End If

                If AppConfig.ExportToServer Then
                    objattAstBooks = New attAstBooks
                    objattAstBooks.SalvageYear = ESalYear
                    objattAstBooks.SalvageMonth = ESalMonth
                    EDepValue = ((objattAssetDetails.BaseCost + objattAssetDetails.Tax - ESalValue) / ESalYear) / 12

                    objBALAstBooks.Insert_AstBooks_ExportServer(objattAstBooks, objattAssetDetails.AstNum, objattAssetDetails.SrvDate, objattAssetDetails.BaseCost, 0, EDepValue)
                End If

                Get_AstBooks(AssetsID.SelectedText)
                txtDepAstID.Text = AssetsID.SelectedText
                grdFiscalYRBook.DataSource = Nothing

                If AnonymousId = "" Then
                    Me.Search_Asset(AssetsID.SelectedText)
                Else 'FormOpened from Anonymous in Data Processing
                    'set this flag to delete the asset from Anonymous data after closing the form.
                    AnonymousSaved = True
                    Dim objattAstHistory As attAstHistory
                    objattAstHistory = New attAstHistory()
                    Dim objBALAst_History As New BALAst_History
                    objattAstHistory.AstID = AssetsID.SelectedText
                    objattAstHistory.Status = 5
                    objattAstHistory.InvSchCode = InvScheduleId
                    objattAstHistory.HisDate = TransDate
                    objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                    objattAstHistory.Fr_loc = ZTLocation.SelectedValue
                    objattAstHistory.To_Loc = ZTLocation.SelectedValue
                    objBALAst_History.Insert_Ast_History(objattAstHistory)

                    objattAssetDetails = New attAssetDetails
                    objattAssetDetails.PKeyCode = AssetsID.SelectedText
                    objattAssetDetails.InvStatus = 5 'Means Anonymous
                    objattAssetDetails.InvSchCode = InvScheduleId
                    objattAssetDetails.LocID = ZTLocation.SelectedValue
                    objattAssetDetails.IsDataChanged = True
                    objBALAssetDetails.Update_InvStatus(objattAssetDetails)
                End If
                ZulMessageBox.ShowMe("Saved")
            Else
                txtRef.Focus()
                txtRef.Text = ""
                ZulMessageBox.ShowMe("RefIDExist")
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

    Private Sub btnAddBrand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddBrand.Click
        Dim frm As New frmBrand
        frm.addNew_fromAsset = 1
        frm.frmAst = Me
        frm.ShowDialog()
    End Sub



    Private Sub txtbase_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtbase.TextChanged, txtSales.TextChanged
        Try
            txtTotal.Text = CType(txtbase.Text, Double) + CType(txtSales.Text, Double)
        Catch ex As Exception
            txtSales.Text = "0"
            txtbase.Text = "0"
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If objBALAssetDetails.Check_AstID(AssetsID.SelectedText, True) Then
                If ZulMessageBox.ShowMe("BeforeDeletedAsset", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    objattAssetDetails = New attAssetDetails
                    objattAssetDetails.PKeyCode = AssetsID.SelectedText
                    If objBALAssetDetails.Delete_Details(objattAssetDetails) Then

                        If AppConfig.ImgStorgeLoc = "Shared Folder" Then
                            DeleteImage()
                        End If
                        ZulMessageBox.ShowMe("Deleted")
                        btnNewAsset_Click(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub AssetsID_SelectTextChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles AssetsID.SelectTextChanged
        If AssetsID.Tag = "NewID" Then
            AssetsID.Tag = ""
        Else
            If AssetsID.SelectedText <> "" Then
                Me.Search_Asset(AssetsID.SelectedText)
            End If
        End If
    End Sub

    Private Sub ItmCode_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItemCode.SelectTextChanged
        Try
            If cmbItemCode.SelectedText <> "" Then
                Get_Assets_ById(cmbItemCode.SelectedValue)
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
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
            errProv.SetError(txtBookVal, My.MessagesResource.Messages.InvalidCBV)
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



    Private Sub cmdDisposal_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDisposal.SelectTextChanged
        Try
            If chkDisposed.Checked Then
                cmdDisposal.Enabled = True
                txtDispComments.Enabled = True
                dtDispdate.Enabled = True
                If cmdDisposal.SelectedValue <> "" Then
                    If cmdDisposal.SelectedValue = "1" Then
                        Panel1.Visible = True
                    Else
                        Panel1.Visible = False
                    End If
                Else
                    Panel1.Visible = False
                End If

            Else

                cmdDisposal.Enabled = False
                dtDispdate.Enabled = False
                Panel1.Visible = False
                txtDispComments.Enabled = False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbType.SelectedIndexChanged
        If cmbType.SelectedIndex = 0 Then
            Label3.Visible = False
            txtPieceNo.Visible = False
            txtPieceNo.Value = 1
        Else
            Label3.Visible = True
            txtPieceNo.Visible = True
        End If
    End Sub

    Private Sub cmbComp_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComp.SelectTextChanged
        If cmbComp.SelectedValue <> "" Then
            If AppConfig.CodingMode And Not isEdit Then
                txtAstNum.Text = Generate_AssetNumber(cmbComp.SelectedValue)
                If txtAstNum.Text = "" Then
                    errProv.SetError(cmbComp.TextBox, My.MessagesResource.Messages.DefineRange)
                Else
                    errProv.ClearErrors()
                End If
                txtRef.Text = txtAstNum.Text
            End If
        Else
            errProv.ClearErrors()
        End If
        cmbGLCode.SelectedText = ""
    End Sub

    Private Sub btnDelImg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelImg.Click
        Try
            If ZulMessageBox.ShowMe("BeforeDeletedImg", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                DeleteImage()
                ZulMessageBox.ShowMe("AfterDeleteImg")
                btnDelImg.Visible = False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        Dim FirstAstID As String = ""
        objattAssetDetails = New attAssetDetails
        If AppConfig.CodingMode Then
            If cmbComp.SelectedValue <> "" Then
                objattAssetDetails.CompanyID = cmbComp.SelectedValue
            Else
                objattAssetDetails.CompanyID = 0
            End If
            FirstAstID = objBALAssetDetails.Get_AssetsDetails_First_Last(objattAssetDetails, 1)
        Else
            objattAssetDetails.CompanyID = 0
            FirstAstID = objBALAssetDetails.Get_AssetsDetails_First_Last(objattAssetDetails, 1)
        End If
        If FirstAstID <> "" Then
            AssetsID.SelectedText = FirstAstID
            'Me.Search_Asset(FirstAstID)
        End If
    End Sub

    Private Sub btnLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLast.Click
        Dim LastAstID As String = ""
        objattAssetDetails = New attAssetDetails
        If AppConfig.CodingMode Then
            If cmbComp.SelectedValue <> "" Then
                objattAssetDetails.CompanyID = cmbComp.SelectedValue
            Else
                objattAssetDetails.CompanyID = 0
            End If
            LastAstID = objBALAssetDetails.Get_AssetsDetails_First_Last(objattAssetDetails, 2)
        Else
            objattAssetDetails.CompanyID = 0
            LastAstID = objBALAssetDetails.Get_AssetsDetails_First_Last(objattAssetDetails, 2)
        End If
        If LastAstID <> "" Then
            AssetsID.SelectedText = LastAstID
            'Me.Search_Asset(LastAstID)
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim NextAstId As String = ""
        If AppConfig.CodingMode Then
            NextAstId = Get_AssetsDetails_Pre_Next(txtAstNum.Text, cmbComp.SelectedValue, 2)
        Else
            NextAstId = Get_AssetsDetails_Pre_Next(txtAstNum.Text, "", 2)
        End If

        If NextAstId <> "" Then
            AssetsID.SelectedText = NextAstId
            'Me.Search_Asset(NextAstId)
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

    Private Sub btnPre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPre.Click
        Dim PreAstId As String = ""
        If AppConfig.CodingMode Then
            PreAstId = Get_AssetsDetails_Pre_Next(txtAstNum.Text, cmbComp.SelectedValue, 1)
        Else
            PreAstId = Get_AssetsDetails_Pre_Next(txtAstNum.Text, "", 1)
        End If

        If PreAstId <> "" Then
            AssetsID.SelectedText = PreAstId
            'Me.Search_Asset(PreAstId)
        End If

    End Sub

    Private Sub txtBookDesc_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBookDesc.MouseEnter
        ToolTip1.SetToolTip(txtBookDesc, txtBookDesc.Text)
    End Sub

    Private Sub txtDepText_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDepText.MouseEnter
        ToolTip1.SetToolTip(txtDepText, txtDepText.Text)
    End Sub

    Private Sub cmbCust_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCust.SelectTextChanged
        txtCustHier.Text = GetCustHier(cmbCust.SelectedValue)
    End Sub

    Private Function GetCustHier(ByVal CustID As String) As String
        Dim CustHier As String = objBALAssetDetails.GetCustHier_AssetDetails(CustID)
        Return objBALLocation.Comp_Path_OrgHier(CustHier)
    End Function


    Private Sub btnNewAsset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewAsset.Click, btnNewAsset1.Click
        Try
            txtSerialNo.Text = ""
            txtBarCode.Text = ""
            cmbCust.SelectedText = ""
            cmbCostCenter.SelectedText = ""
            cmbCostCenter.Text = ""
            cmbComp.SelectedText = ""
            cmbItemCode.SelectedIndex = -1
            txtCategory.Text = ""
            dtBVUpdate.Value = Now.Date
            txtRemarks.Text = ""
            txtItemDesc.Text = ""
            txtAstNum.Text = ""
            txtbase.Text = "0"
            cmbCust.Text = ""
            cmbIns.Text = ""
            dtpur.Text = ""
            cmbItemCode.Text = ""
            txtSales.Text = "0"
            dtService.Text = ""
            txtLabelPrintedCnt.Text = ""
            txtRemarks.Text = ""
            ZTLocation.SelectedText = ""
            ZTLocation.SelectedValue = ""
            txtRef.Text = ""
            txtDepText.Text = ""
            txtBrandID.SelectedIndex = -1
            txtAssetStatus.FindRow(3, "In Stock")



            txtAstDesc.Text = ""
            txtAstDesc2.Text = ""
            txtAstModel.Text = ""
            txtTotal.Text = "0"
            cmbGLCode.SelectedText = ""

            txtSalPercent.Text = ""
            txtSalYr.Text = ""
            txtBookID.Text = ""

            cmbSupp.SelectedText = ""
            cmbIns.SelectedText = ""
            cmbPO.Text = ""
            txtPONumber.Text = ""
            txtInvoice.Text = ""
            txtSalMonth.Text = ""
            txtSalVal.Text = ""
            txtSalValPercent.Text = ""
            txtTotal2.Text = ""
            txtBookVal.Text = ""
            txtAccDep.Text = ""
            txtDepText.Text = ""
            txtBookDesc.Text = ""
            txtDepAstID.Text = ""
            txtPlate.Text = ""
            txtDiscount.Text = "0"
            txtWarranty.Value = 0
            AssetsID.Tag = "NewID"
            AssetsID.SelectedText = objBALAssetDetails.Generate_AssetID()

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
            txtDispComments.Text = String.Empty

            If Not AppConfig.CodingMode Then
                txtAstNum.Text = Generate_AssetNumber("")
                txtRef.Text = txtAstNum.Text
            End If

            btnDelete.Visible = False
            btnDuplicate.Visible = False
            btnDelImg.Visible = False
            btnPrintLabel.Visible = False

            PBLogo.Image = Nothing
            PBLogo.ImageLocation = Nothing

            cmdDisposal.Enabled = True

            Panel1.Visible = False

            chkDisposed.Checked = False

            If grdAstHist.DataSource IsNot Nothing Then
                CType(grdAstHist.DataSource, DataTable).Rows.Clear()
            End If
            If grdCustHist.DataSource IsNot Nothing Then
                CType(grdCustHist.DataSource, DataTable).Rows.Clear()
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

            If grdAssetWarranty.DataSource IsNot Nothing Then
                CType(grdAssetWarranty.DataSource, DataTable).Rows.Clear()
            End If
            btnNewWarranty_Click(sender, e)
            btnSaveWarranty.Enabled = False
            btnNewWarranty.Enabled = False
            btnDeleteWarranty.Enabled = False

            txtSales.Properties.ReadOnly = False
            txtbase.Properties.ReadOnly = False
            txtSales.ShowToolTips = False
            txtbase.ShowToolTips = False
            dtService.Enabled = True

            isEdit = False

            valProvMain.RemoveControlError(txtSales)
            valProvMain.RemoveControlError(txtDiscount)
            valProvMain.RemoveControlError(txtbase)

            valProvMain.RemoveControlError(cmbGLCode.TextBox)
            valProvMain.RemoveControlError(txtBrandID.TextBox)
            valProvMain.RemoveControlError(txtAssetStatus.TextBox)
            valProvMain.RemoveControlError(cmbCust.TextBox)
            valProvMain.RemoveControlError(AssetsID.TextBox)
            valProvMain.RemoveControlError(cmbItemCode.TextBox)
            valProvMain.RemoveControlError(cmbComp.TextBox)
            valProvMain.RemoveControlError(txtbase)
            valProvMain.RemoveControlError(txtSales)
            valProvMain.RemoveControlError(txtAstNum)
            valProvMain.RemoveControlError(ZTLocation.TextBox)

            valProvNotNigative.Validate()
            tabControl.SelectedTabPageIndex = 0
            cmbItemCode.TextBox.Focus()
            'if it's offline machine then don't allow to change the company.
            If AppConfig.IsOfflineMachine Then
                cmbComp.Enabled = False
                Dim objBalOfflineMachine As New BALOfflineMachines
                Dim dt As DataTable = objBalOfflineMachine.GetAll_OfflineMachine(New AttOfflineMachines)
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        cmbComp.SelectedValue = dt.Rows(0)("CompanyID")
                        cmbComp.SelectedText = dt.Rows(0)("CompanyName")
                    End If
                End If
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

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
                    If (Date.Compare(GetGridRowCellValue(grdAddCostView, FocRow, "ActionDate").ToString(), dtBVUpdate.Value.ToShortDateString) <= 0) Then
                        If txtAddCost.Tag <> 0 Then
                            ZulMessageBox.ShowMe("CantChangeAdditionalCost")
                            Exit Sub
                        End If
                    End If
                End If

                If ZulMessageBox.ShowMe("BeforeAddAdditionalCost", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
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

                    objAttAddCostHistory.AstID = AssetsID.SelectedText
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
                    Get_AssetAddCosts(AssetsID.SelectedText)
                    Dim addcostcnt As Integer = grdAddCostView.RowCount
                    If addcostcnt > 0 Then
                        txtSales.Properties.ReadOnly = True
                        txtSales.ShowToolTips = True
                    Else
                        txtSales.Properties.ReadOnly = False
                        txtSales.ShowToolTips = False
                    End If

                    btnNewAddCost_Click(sender, e)
                End If
            Else
                ZulMessageBox.ShowMe("NotSaved")
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


    Private Sub grdAddCost_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdAddCostView.KeyDown
        Try
            If e.KeyValue = Keys.Delete Then 'Press Delete
                Dim FocRow As Integer = grdAddCostView.FocusedRowHandle
                If FocRow >= 0 Then
                    If (Date.Compare(GetGridRowCellValue(grdAddCostView, FocRow, "ActionDate").ToString(), dtBVUpdate.Value.ToShortDateString) <= 0) Then
                        ZulMessageBox.ShowMe("CantDeleteAdditionalCost")
                    ElseIf ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        Dim objBALAddCostHistory As New BALAddCostHistory
                        Dim objAttAddCostHistory As New AttAddCostHistory

                        objAttAddCostHistory.AddCostID = GetGridRowCellValue(grdAddCostView, FocRow, "AddCostID").ToString()
                        objBALAddCostHistory.Delete_AddCostHistory(objAttAddCostHistory)
                        PreviousItemCost = CType(txtbase.Text.Trim, Decimal) + CType(txtSales.Text.Trim, Decimal)
                        txtSales.Text = CDbl(txtSales.Text) - GetGridRowCellValue(grdAddCostView, FocRow, "AddCost").ToString()

                        update_AssetDetails()
                        Get_AssetAddCosts(AssetsID.SelectedText)
                        Dim addcostcnt As Integer = grdAddCostView.RowCount
                        If addcostcnt > 0 Then
                            txtSales.Properties.ReadOnly = True
                            txtSales.ShowToolTips = True

                        Else
                            txtSales.Properties.ReadOnly = False
                            txtSales.ShowToolTips = False
                        End If

                        btnNewAddCost_Click(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

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

#Region "-- New Buttons --"
    Private Sub btnAddCat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddItem.Click
        Dim frm As New frmItems
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
    End Sub

    Private Sub btnAddLoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddLoc.Click
        Dim frm As New frmLocation
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
    End Sub

    Private Sub btnAddCust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCust.Click
        Dim frm As New frmCustodian
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
    End Sub

    Private Sub btnAddSUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSUp.Click
        Dim frm As New frmSupplier
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
    End Sub

    Private Sub btnAddIns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddIns.Click
        Dim frm As New frmInsurar
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
    End Sub

    Private Sub btnAddPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPO.Click
        Dim frm As New frmPOMaster
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
    End Sub
    Private Sub btnAddGLCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddGLCode.Click
        Dim frm As New frmGLCodes
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
    End Sub

    Private Sub btnNewCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewCompany.Click
        Dim frm As New frmCompanies
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
    End Sub
#End Region

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
                Get_AstDep_History(AssetsID.SelectedText, txtBookID.Text)
            End If
            format_BooksGrid()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub Update_DepreciationBook(ByVal btnSaveDepreciationClicked As Boolean)
        Try
            txtTotal2.Text = txtTotal.Text

            If grdBooksView.RowCount > 0 Then
                Dim intRow As Integer = grdBooksView.FocusedRowHandle

                If (Date.Compare(dtBVUpdate.Value.ToShortDateString(), DateTime.Now.ToShortDateString()) < 0) Or (Date.Compare(dtBVUpdate.Value.ToShortDateString(), DateTime.Now.ToShortDateString()) = 0) Then
                    objattAstBooks = New attAstBooks
                    objattAstBooks.BookDescription = RemoveUnnecessaryChars(txtBookDesc.Text)
                    If btnSaveDepreciationClicked Then
                        objattAstBooks.CurrentBookValue = RemoveUnnecessaryChars(txtBookVal.Text)
                    Else
                        objattAstBooks.CurrentBookValue = CType(RemoveUnnecessaryChars(txtBookVal.Text), Decimal) + (CType(txtTotal2.Text.Trim, Decimal) - PreviousItemCost)
                        txtBookVal.Text = objattAstBooks.CurrentBookValue
                    End If
                    objattAstBooks.BVUpdate = dtBVUpdate.Value.Date
                    objattAstBooks.DepCode = txtDepText.Tag
                    objattAstBooks.PKeyCode = RemoveUnnecessaryChars(txtBookID.Text)
                    objattAstBooks.AstID = RemoveUnnecessaryChars(txtDepAstID.Text)
                    objattAstBooks.SalvageYear = RemoveUnnecessaryChars(txtSalYr.Text)
                    objattAstBooks.SalvageMonth = RemoveUnnecessaryChars(txtSalMonth.Text)

                    objattAstBooks.SalvageValue = RemoveUnnecessaryChars(txtSalVal.Text)
                    'Recalc the salvage value percentage after saving, because maybe the total cost will be changed.
                    If txtTotal.Text > 0 Then
                        txtSalValPercent.Text = Math.Round((txtSalVal.Text / txtTotal.Text) * 100, 2)
                    Else
                        txtSalValPercent.Text = 0
                    End If
                    objattAstBooks.SalvageValuePercent = txtSalValPercent.Text

                    If btnSaveDepreciationClicked Then
                        Dim objattDepPolicy_History As New attDepPolicy_History
                        Dim objBALDepPolicy_History As New BALDepPolicy_History
                        objattDepPolicy_History.DepCode = txtDepText.Tag
                        objattDepPolicy_History.SalvageValue = RemoveUnnecessaryChars(txtSalVal.Text)
                        objattDepPolicy_History.SalvageYear = GetGridRowCellValue(grdBooksView, intRow, "SalvageYear")
                        objattDepPolicy_History.SalvageMonth = GetGridRowCellValue(grdBooksView, intRow, "SalvageMonth")
                        objattDepPolicy_History.PKeyCode = RemoveUnnecessaryChars(txtBookID.Text)
                        objattDepPolicy_History.AstID = RemoveUnnecessaryChars(txtDepAstID.Text)
                        objattDepPolicy_History.CurrentBookValue = GetGridRowCellValue(grdBooksView, intRow, "CurrentBV")
                        objattDepPolicy_History.BVUpdate = dtBVUpdate.Value.Date
                        objBALDepPolicy_History.Insert_DepPolicy_History(objattDepPolicy_History)
                    End If


                    objBALAstBooks.Update_AstBooks(objattAstBooks)

                    If AppConfig.ExportToServer Then
                        Dim ExpBaseCost As Decimal = 0
                        Dim ExpTax As Decimal = 0
                        Dim ExpBookValue As Decimal = 0

                        If txtbase.Text IsNot Nothing Or txtbase.Text <> "" Then
                            ExpBaseCost = CType(txtbase.Text, Double)
                        End If

                        If txtBookVal.Text IsNot Nothing Or txtBookVal.Text <> "" Then
                            ExpBookValue = CType(txtBookVal.Text, Double)
                        End If

                        If txtSales.Text IsNot Nothing Or txtSales.Text <> "" Then
                            ExpTax = CType(txtSales.Text, Double)
                        End If
                        objBALAstBooks.Update_AstBooks_ExportServer(objattAstBooks, RemoveUnnecessaryChars(txtAstNum.Text), Nothing, txtbase.Text, (ExpBaseCost + ExpTax - ExpBookValue), Nothing)
                    End If

                    Get_AstBooks(AssetsID.SelectedText)

                    If btnSaveDepreciationClicked Then
                        ZulMessageBox.ShowMe("Saved")
                    End If

                Else
                    ZulMessageBox.ShowInfoMessage("Cannot change book information, because depreciation already ran on current asset, only asset information will be saved.")
                    If grdBooksView.RowCount > 0 Then
                        txtBookID.Text = GetGridRowCellValue(grdBooksView, intRow, "BookID")
                        txtBookDesc.Text = GetGridRowCellValue(grdBooksView, intRow, "Description").ToString
                        txtDepText.Tag = GetGridRowCellValue(grdBooksView, intRow, "DepCode").ToString
                        dtBVUpdate.Value = CDate(GetGridRowCellValue(grdBooksView, intRow, "BVUpdate").ToString)
                        txtBookVal.Text = GetGridRowCellValue(grdBooksView, intRow, "CurrentBV").ToString
                        txtSalVal.Text = GetGridRowCellValue(grdBooksView, intRow, "SalvageValue").ToString
                        txtSalValPercent.Text = GetGridRowCellValue(grdBooksView, intRow, "SalvageValuePercent").ToString
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
                            grdExpectedBookAnnual.DataSource = CalcDepAnnual(RemoveUnnecessaryChars(txtDepAstID.Text) & "-" & RemoveUnnecessaryChars(txtBookID.Text), txtSalYr.Text, txtSalMonth.Text, txtDepText.Tag, txtTotal2.Text, txtSalVal.Text, dtBVUpdate.Value.Date, dtService.Value.Date, txtBookVal.Text)
                            grdExpectedBookMonthly.DataSource = CalcDepMonthly(RemoveUnnecessaryChars(txtDepAstID.Text) & "-" & RemoveUnnecessaryChars(txtBookID.Text), txtSalYr.Text, txtSalMonth.Text, txtDepText.Tag, txtTotal2.Text, txtSalVal.Text, dtBVUpdate.Value.Date, dtService.Value.Date, txtBookVal.Text)
                        Else
                            txtDepText.Text = ""
                        End If
                        isEdit2 = True
                    End If
                End If
            End If
            grdExpectedBookAnnual.DataSource = CalcDepAnnual(RemoveUnnecessaryChars(txtDepAstID.Text) & "-" & RemoveUnnecessaryChars(txtBookID.Text), txtSalYr.Text, txtSalMonth.Text, txtDepText.Tag, txtTotal2.Text, txtSalVal.Text, dtBVUpdate.Value.Date, dtService.Value.Date, txtBookVal.Text)
            grdExpectedBookMonthly.DataSource = CalcDepMonthly(RemoveUnnecessaryChars(txtDepAstID.Text) & "-" & RemoveUnnecessaryChars(txtBookID.Text), txtSalYr.Text, txtSalMonth.Text, txtDepText.Tag, txtTotal2.Text, txtSalVal.Text, dtBVUpdate.Value.Date, dtService.Value.Date, txtBookVal.Text)

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

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

                dtBVUpdate.Value = CDate(GetGridRowCellValue(grdBooksView, FocRow, "BVUpdate").ToString)
                txtBookVal.Text = GetGridRowCellValue(grdBooksView, FocRow, "CurrentBV").ToString
                txtSalVal.Text = GetGridRowCellValue(grdBooksView, FocRow, "SalvageValue").ToString
                txtSalValPercent.Text = GetGridRowCellValue(grdBooksView, FocRow, "SalvageValuePercent").ToString
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
                    grdExpectedBookAnnual.DataSource = CalcDepAnnual(RemoveUnnecessaryChars(txtDepAstID.Text) & "-" & RemoveUnnecessaryChars(txtBookID.Text), txtSalYr.Text, txtSalMonth.Text, txtDepText.Tag, txtTotal2.Text, txtSalVal.Text, dtBVUpdate.Value.Date, dtService.Value.Date, txtBookVal.Text)
                    grdExpectedBookMonthly.DataSource = CalcDepMonthly(RemoveUnnecessaryChars(txtDepAstID.Text) & "-" & RemoveUnnecessaryChars(txtBookID.Text), txtSalYr.Text, txtSalMonth.Text, txtDepText.Tag, txtTotal2.Text, txtSalVal.Text, dtBVUpdate.Value.Date, dtService.Value.Date, txtBookVal.Text)
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


    Private Sub btnDeleteAddCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAddCost.Click
        Dim objattAddCosttype As New attAddCostType
        Dim objBALAddCostType As New BALAddCostType
        Dim TypeDesc As String = RemoveUnnecessaryChars(cmbAddCostType.Text)
        Try
            If TypeDesc <> "" Then
                Dim TypeID As Integer = objBALAddCostType.GetDatabyTypeDesc(TypeDesc)
                If Not check_Child_CostType(TypeID) Then
                    If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        objattAddCosttype.TypeID = TypeID
                        If objBALAddCostType.Delete_AddCostType(objattAddCosttype) Then
                            Get_AddCostType()
                            ZulMessageBox.ShowMe("Deleted")
                            btnNewAddCost_Click(sender, e)
                        End If
                    End If
                Else
                    ZulMessageBox.ShowMe("CantDelete")
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
                    errProv.SetError(txtSalPercent, My.MessagesResource.Messages.OutPercentRange)
                Else
                    errProv.ClearErrors()
                    Dim perValue As Decimal = CType(txtSalPercent.Text.Trim, Double)
                    Dim YearCost As Double = 100 / perValue

                    txtSalYr.Text = Math.Truncate(YearCost)
                    txtSalMonth.Text = Math.Ceiling(12 * (YearCost - Math.Truncate(YearCost)))
                End If
            End If
        Catch ex As Exception
            errProv.SetError(txtSalPercent, My.MessagesResource.Messages.OutPercentRange)
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

    Private Sub dtBVUpdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtBVUpdate.ValueChanged
        txtBVUpdate.Text = dtBVUpdate.Value.Date
    End Sub

    Private Sub cmbIns_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbIns.LovBtnClick
        Try
            cmbIns.ValueMember = "InsCode"
            cmbIns.DisplayMember = "InsName"
            Dim objBALInsurer As New BALInsurer
            cmbIns.DataSource = objBALInsurer.GetAll_Insurer(New attInsurer)
            cmbIns.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmbPO_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPO.LovBtnClick
        Try
            cmbPO.ValueMember = "POCode"
            cmbPO.DisplayMember = "POCode"
            Dim objattPurchaseOrder As New attPurchaseOrder
            objattPurchaseOrder.POStatus = 2
            Dim objBALPurchaseOrder As New BALPurchaseOrder
            cmbPO.DataSource = objBALPurchaseOrder.GetAllData_GetCombo(objattPurchaseOrder)
            cmbPO.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub cmbSupp_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSupp.LovBtnClick
        Try
            cmbSupp.ValueMember = "SuppID"
            cmbSupp.DisplayMember = "SuppName"
            Dim objBALSupplier As New BALSupplier
            cmbSupp.DataSource = objBALSupplier.GetAllData_GetCombo()
            cmbSupp.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmbComp_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbComp.LovBtnClick
        Try
            cmbComp.ValueMember = "CompanyID"
            cmbComp.DisplayMember = "CompanyName"
            Dim objBALCompany As New BALCompany
            cmbComp.DataSource = objBALCompany.GetAllData_GetCombo(New attcompany)
            cmbComp.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmbCostCenter_LovBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCostCenter.LovBtnClick
        Try
            Dim objBALCost As New BALCostCenter
            cmbCostCenter.ValueMember = "CostID"
            cmbCostCenter.DisplayMember = "CostName"
            cmbCostCenter.DataSource = objBALCost.GetAllData_GetCombo(New attCostCenter)
            cmbCostCenter.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmbCust_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCust.LovBtnClick
        Try
            Dim objBALCustodian As New BALCustodian
            cmbCust.ValueMember = "ID"
            cmbCust.DisplayMember = "Name"
            cmbCust.DataSource = objBALCustodian.GetAllData_GetCombo()
            cmbCust.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub itmCode_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbItemCode.LovBtnClick
        Try
            cmbItemCode.ValueMember = "ItemCode"
            cmbItemCode.DisplayMember = "ItemCode"
            Dim objBALAssets As New BALItems
            cmbItemCode.DataSource = objBALAssets.GetAllData_GetCombo(New attItems)
            cmbItemCode.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub txtBrandID_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBrandID.LovBtnClick
        Try
            txtBrandID.ValueMember = "AstBrandID"
            txtBrandID.DisplayMember = "AstBrandName"
            Dim objBALBrand As New BALbrand
            txtBrandID.DataSource = objBALBrand.GetAll_Brand(New attBrand)
            txtBrandID.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub txtAssetStatus_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAssetStatus.LovBtnClick
        Try
            txtAssetStatus.ValueMember = "ID"
            txtAssetStatus.DisplayMember = "Status"
            txtAssetStatus.DataSource = objBALAssetDetails.GetAssetStatus(False, True)
            txtAssetStatus.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub AssetsID_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssetsID.LovBtnClick
        Try
            AssetsID.ValueMember = "AstId"
            AssetsID.DisplayMember = "AstId"
            Dim objattAssetDetails As attAssetDetails = New attAssetDetails
            Dim objBALAssetDetails As BALAssetDetails = New BALAssetDetails
            objattAssetDetails.Disposed = True
            AssetsID.DataSource = objBALAssetDetails.GetAsset_DetailsCombo(objattAssetDetails)
            AssetsID.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmdDisposal_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDisposal.LovBtnClick
        Try
            cmdDisposal.ValueMember = "DispCode"
            cmdDisposal.DisplayMember = "DispDesc"
            Dim objBALDisposalMethod As New BALDisposalMethod
            cmdDisposal.DataSource = objBALDisposalMethod.GetAll_DisposalMethod(New attDisposalMethod)
            cmdDisposal.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmbGLCode_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGLCode.LovBtnClick
        Try
            cmbGLCode.ValueMember = "GLcode"
            cmbGLCode.DisplayMember = "GLcode"
            Dim objBALGLCode As New BALGLCode
            cmbGLCode.DataSource = objBALGLCode.GetCompanyGLCodes(cmbComp.SelectedValue)
            cmbGLCode.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub ZTLocation_TreeBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZTLocation.TreeBtnClick
        Try
            Dim ds As New DataTable
            Dim objBALLocation As New BALLocation
            ZTLocation.DataSource = objBALLocation.GetComboLocations(New attLocation)
            ZTLocation.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub PBLogo_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PBLogo.MouseEnter
        PBLogo.BorderStyle = BorderStyle.FixedSingle
    End Sub

    Private Sub PBLogo_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PBLogo.MouseLeave
        PBLogo.BorderStyle = BorderStyle.None
    End Sub

    Private Sub btnAddCostCenter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCostCenter.Click
        Dim frm As New frmCostCenter
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
    End Sub

    Private Sub txtWarranty_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWarranty.EditValueChanged
        txtWarrantyExpired.Text = dtService.Value.AddMonths(txtWarranty.Value).ToString(AppConfig.MaindateFormat)
    End Sub

    Private Sub dtService_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtService.ValueChanged
        dtpur.Value = dtService.Value
        txtWarrantyExpired.Text = dtService.Value.AddMonths(txtWarranty.Value).ToString(AppConfig.MaindateFormat)
        If Now.Date >= dtService.Value Then
            txtAge.Text = (Now.Date - dtService.Value).TotalDays
        Else
            txtAge.Text = 0
        End If
    End Sub

    Private Sub txtExtWarranty_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtExtWarrantyMonth.EditValueChanged
        txtExtWarrantyExpire.Text = dtExtWarrantyStart.Value.AddMonths(txtExtWarrantyMonth.Value).ToString(AppConfig.MaindateFormat)
    End Sub

    Private Sub dtExtWarrantyStart_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtExtWarrantyStart.ValueChanged
        txtExtWarrantyExpire.Text = dtExtWarrantyStart.Value.AddMonths(txtExtWarrantyMonth.Value).ToString(AppConfig.MaindateFormat)
    End Sub

    Private Sub btnNewWarranty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewWarranty.Click
        dtExtWarrantyStart.Value = Now.Date
        txtExtWarrantyAlarmDays.Value = 0
        txtExtWarrantyMonth.Value = txtWarranty.Value
        txtExtWarrantyExpire.Text = dtExtWarrantyStart.Value.AddMonths(txtExtWarrantyMonth.Value).ToString(AppConfig.MaindateFormat)
        chkExtWarrantyActive.Checked = True
        txtExtWarrantyID.Text = ""
    End Sub


    Private Sub grdWarrantyt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAssetWarranty.Click
        grvWarranty_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub grvAssetWarranty_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grvAssetWarranty.KeyDown
        Try
            If e.KeyValue = Keys.Delete Then 'Press Delete
                btnDeleteWarranty_Click(sender, e)
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub grvWarranty_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvAssetWarranty.FocusedRowChanged
        Dim FocRow As Integer = grvAssetWarranty.FocusedRowHandle
        If FocRow >= 0 Then
            txtExtWarrantyID.Text = GetGridRowCellValue(grvAssetWarranty, FocRow, "ID").ToString()
            dtExtWarrantyStart.Text = GetGridRowCellValue(grvAssetWarranty, FocRow, "WarrantyStart").ToString()
            txtExtWarrantyMonth.Text = GetGridRowCellValue(grvAssetWarranty, FocRow, "WarrantyPeriodMonth").ToString()
            txtExtWarrantyAlarmDays.Text = GetGridRowCellValue(grvAssetWarranty, FocRow, "AlarmBeforeDays").ToString()
            chkExtWarrantyActive.Checked = GetGridRowCellValue(grvAssetWarranty, FocRow, "AlarmActivated").ToString()
        End If
    End Sub


    Private Sub btnDeleteWarranty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteWarranty.Click
        Dim objattAssetWarranty As New attAssetWarranty
        Dim objBALAssetWarranty As New BALAssetWarranty
        Dim WarrantyID As String = txtExtWarrantyID.Text
        Try
            If WarrantyID <> "" Then
                If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    objattAssetWarranty.ID = WarrantyID
                    If objBALAssetWarranty.Delete_AssetWarranty(objattAssetWarranty) Then
                        Get_AssetWarranty(AssetsID.SelectedText)
                        ZulMessageBox.ShowMe("Deleted")
                        btnNewWarranty_Click(sender, e)
                    End If
                End If
            Else
                ZulMessageBox.ShowError("SelectRowValidation")
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnSaveWarranty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveWarranty.Click
        Dim objattWarranty As New attAssetWarranty
        Dim objBALWarranty As New BALAssetWarranty
        Try
            If txtExtWarrantyMonth.Value <= 0 Then
                ZulMessageBox.ShowError("WarrantyMonthValidate")
                Return
            End If
            objattWarranty.AstID = AssetsID.SelectedText
            objattWarranty.WarrantyPeriodMonth = txtExtWarrantyMonth.Value
            objattWarranty.WarrantyStart = dtExtWarrantyStart.Value
            objattWarranty.AlarmActivated = chkExtWarrantyActive.Checked
            objattWarranty.AlarmBeforeDays = txtExtWarrantyAlarmDays.Value




            If txtExtWarrantyID.Text <> "" Then ' updating add cost
                objattWarranty.ID = txtExtWarrantyID.Text
                If Not objBALWarranty.ValidateWarrantyStartDate(objattWarranty) Then
                    ZulMessageBox.ShowError("WarrantyDateValidate")
                    Return
                End If
                objBALWarranty.Update_AssetWarranty(objattWarranty)
            Else
                objattWarranty.ID = objBALWarranty.GetNextPKey_AssetWarranty
                If Not objBALWarranty.ValidateWarrantyStartDate(objattWarranty) Then
                    ZulMessageBox.ShowError("WarrantyDateValidate")
                    Return
                End If
                objBALWarranty.Insert_AssetWarranty(objattWarranty)
                txtExtWarrantyID.Text = objattWarranty.ID
            End If

            Get_AssetWarranty(AssetsID.SelectedText)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub format_ExtenedWarrantyGrid()
        With grvAssetWarranty
            grvAssetWarranty.Columns(0).Visible = False 'ID
            grvAssetWarranty.Columns(1).Visible = False 'AstID
            grvAssetWarranty.Columns(4).Visible = False '"AlarmBeforeDays"
        End With

        With grdAssetWarranty
            .UseEmbeddedNavigator = True
            .EmbeddedNavigator.Buttons.Append.Visible = False
            .EmbeddedNavigator.Buttons.Edit.Visible = False
            .EmbeddedNavigator.Buttons.EndEdit.Visible = False
            .EmbeddedNavigator.Buttons.Remove.Visible = False
            .EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        End With
        addGridMenu(grdAssetWarranty)

    End Sub

    Private Sub Get_AssetWarranty(ByVal astID As String)
        Try
            Dim objattWarranty As New attAssetWarranty
            objattWarranty.AstID = astID

            Dim objBALWarranty As New BALAssetWarranty
            grdAssetWarranty.DataSource = objBALWarranty.GetAll_AssetWarranty(objattWarranty)
            format_ExtenedWarrantyGrid()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub txtSalvageValuePercentage_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSalValPercent.TextChanged
        Try
            Dim intVal As Double = CDbl(txtSalValPercent.Text)
            If (intVal < 0 Or intVal > 100) Then
                errProv.SetError(txtSalValPercent, "Entered value is out of percentage range(1,100)")
            Else
                errProv.SetError(txtSalValPercent, "")

                If txtSalValPercent.ContainsFocus Then
                    txtSalVal.Text = (CDbl(txtTotal.Text) * CDbl(txtSalValPercent.Text)) / 100
                End If
            End If
        Catch ex As Exception
            errProv.SetError(txtSalValPercent, "Entered value is out of percentage range(1,100)")
        End Try
    End Sub

    Private Sub txtSalVal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSalVal.TextChanged
        If txtSalVal.ContainsFocus Then
            If txtTotal.Text > 0 Then
                txtSalValPercent.Text = Math.Round((txtSalVal.Text / txtTotal.Text) * 100, 2)
            Else
                txtSalValPercent.Text = 0
            End If
        End If
    End Sub
End Class