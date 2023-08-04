Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO
Imports System.Globalization.CultureInfo
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.DXErrorProvider
Imports System.Windows.Forms
Imports System.Drawing

Public Class frmAssetsDetails

#Region " -- My Decleration -- "
    Dim objattAssetDetails As attAssetDetails
    Dim objBALAssetDetails As New BALAssetDetails
    Dim objattAstBooks As attAstBooks
    Dim objBALAstBooks As New BALAstBooks
    Dim objBALLocation As New BALLocation
    Dim objBALCategory As New BALCategory
    Public IsDemokey As Boolean = False


    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    'Anonymous Data
    Private AnonymousId As String = ""
    Private DeviceID As String
    Private TransDate As DateTime
    Private InvScheduleId As String = InvScheduleId
    Public AnonymousSaved As Boolean = False
    Private PreviousItemCost As Decimal


    Dim isEdit As Boolean = False
    Private PrevLocationID As String = String.Empty

    Private _RoleID As Integer
    Public Property RoleID() As Integer
        Get
            Return _RoleID
        End Get
        Set(ByVal value As Integer)
            _RoleID = value
        End Set
    End Property

    Private AllowAssetCreation As Boolean = False
    Private AllowUpdateExistedAsset As Boolean = False
    Private AllowDeleteAsset As Boolean = False


#End Region



    Private Sub CheckDemo()
        If IsDemokey Then
            'if asset details recrod count more than 10 then close the application

            Dim obj As New BALAssetDetails
            Dim leftcount As Integer = DemoAssetsCount - obj.GetAssetsCount(New attAssetDetails)
            lblEvaluation.Visible = True
            lblEvaluation.Text = leftcount & " records left"
            If obj.GetAssetsCount(New attAssetDetails) > DemoAssetsCount Then
                ShowErrorMessage("Evaluation, Assets Count exceed 10, Application will close now.")
                Application.Exit()
            End If
        End If
    End Sub

    Public Sub ShowAnonymousInfo(ByVal AnonymousId As String, ByVal Desc As String, ByVal LocID As String, ByVal CatID As String, ByVal Modle As String, ByVal serial As String, ByVal DeviceID As String, ByVal TransDate As DateTime, ByVal InvID As Integer)
        btnNewAsset_Click(Nothing, Nothing)
        txtAssetID.Tag = "NewID"
        txtAssetID.SelectedText = String.Empty
        trvLocation.SelectedValue = LocID
        trvLocation.SelectedText = objBALLocation.Comp_Path(LocID)
        txtAstDesc.Text = Desc
        txtSerialNo.Text = serial
        trvAssetClass.SelectedValue = CatID
        trvAssetClass.SelectedText = objBALCategory.Comp_Path(CatID)

        Me.AnonymousId = AnonymousId
        Me.DeviceID = DeviceID
        Me.TransDate = TransDate
        Me.InvScheduleId = InvID
    End Sub

    Private Sub frmAssetsDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG
        Me.BackgroundImageLayout = ImageLayout.Stretch

        CheckDemo()

        SetAssetPermissions()
        valRulenotEmpty.ConditionOperator = ConditionOperator.IsNotBlank
        valRulenotEmpty.ErrorText = "Please enter a value"
        valRulenotEmpty.ErrorType = ErrorType.Critical

        valProvMain.SetValidationRule(cmbEmployee.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(txtAssetID.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(trvAssetClass.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(cmbComp.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(txtAssetNumber, valRulenotEmpty)
        valProvMain.SetValidationRule(txtSubNumber, valRulenotEmpty)
        valProvMain.SetValidationRule(trvLocation.TextBox, valRulenotEmpty)


        If AnonymousId = "" Then
            btnNewAsset_Click(sender, e)
        End If
    End Sub

    Private Sub SetAssetPermissions()
        Dim ds As DataTable
        Dim objattRole As attRoles = New attRoles
        Dim objBALRole As New BALRoles
        objattRole.PKeyCode = RoleID
        ds = objBALRole.GetAll_Roles(objattRole)

        AllowAssetCreation = ds.Rows(0)("PO")
        AllowUpdateExistedAsset = ds.Rows(0)("POApproval")
        AllowDeleteAsset = ds.Rows(0)("POTrans")

        If Not AllowAssetCreation Then
            btnNewAsset.Visible = False
            btnNewAsset1.Visible = False
        End If

        If Not AllowUpdateExistedAsset Then
            btnSave.Visible = False
        End If

        If Not AllowDeleteAsset Then
            btnDelete.Visible = False
            btnDelete.Enabled = False
        End If

    End Sub

    Public Sub LocateAsset(ByVal astID As String)
        If astID <> "" Then
            txtAssetID.SelectedText = astID
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

#Region " -- Methods -- "

#Region "Formating Grids"
    Private Sub format_CustHistGrid()
        With grdCustHistView
            .Columns(0).Caption = "History ID"
            .Columns(0).Width = 100
            .Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

            .Columns(1).Visible = False

            .Columns(2).Width = 200
            .Columns(2).Caption = "Previous Employee"

            .Columns(3).Width = 200
            .Columns(3).Caption = "Current Employee"

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
        RIAstHist.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() _
                                                    {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Missing", "0", -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Found", "1", -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Misplaced", "2", -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Transfer", "3", -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Allocated", "4", -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Anonymous", "5", -1)})
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
            .Columns(10).Visible = False
            .Columns(11).Visible = False
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

#End Region


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

    Private Function GetUsefulYears(ByVal AssetID As String) As String
        Try
            objattAstBooks = New attAstBooks
            objattAstBooks.AstID = AssetID
            Dim dt As DataTable = objBALAstBooks.GetAllData_Detail(objattAstBooks)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("SalvageYear") = 0 Then
                    Return String.Empty
                Else
                    Return dt.Rows(0)("SalvageYear")
                End If
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
            Return String.Empty
        End Try
    End Function

    Private Sub ZTCategory_TreeBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trvAssetClass.TreeBtnClick
        Dim ds As New DataTable
        Dim objBALCategory As New BALCategory
        trvAssetClass.DataSource = objBALCategory.GetAll_Category(New attCategory)
        trvAssetClass.OpenLOV()
    End Sub

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
                    If Not (dt.Rows(0)("AstCatID") Is Nothing) Then
                        trvAssetClass.SetValue(dt.Rows(0)("AstCatID").ToString(), objBALCategory.Comp_Path(dt.Rows(0)("AstCatID").ToString()))
                    End If

                    cmbEmployee.FindRow(dt.Rows(0)("CustodianID").ToString(), dt.Rows(0)("CustodianName").ToString())
                    cmbComp.FindRow(dt.Rows(0)("CompanyID").ToString(), dt.Rows(0)("CompanyName").ToString())

                    txtSerialNo.Text = dt.Rows(0)("SerailNo").ToString()
                    txtAstDesc.Text = dt.Rows(0)("AstDesc").ToString()
                    txtAstDesc2.Text = dt.Rows(0)("AstDesc2").ToString()
                    txtLabelPrintedCnt.Text = dt.Rows(0)("LabelCount").ToString()
                    txtSource.Text = dt.Rows(0)("TransRemarks").ToString

                    cmbCostCenter.FindRow(dt.Rows(0)("CostID").ToString(), dt.Rows(0)("CostName").ToString())
                    If Not dt.Rows(0).IsNull("CapitalizationDate") Then
                        txtCapitalizationDate.Text = CDate(dt.Rows(0)("CapitalizationDate")).ToString(AppConfig.MaindateFormat)
                    Else
                        txtCapitalizationDate.Text = String.Empty
                    End If

                    If CType(dt.Rows(0)("Disposed"), Boolean) Then
                        ShowStatus(2)
                        If Not dt.Rows(0).IsNull("DispDate") Then
                            txtRetirementDate.Text = CDate(dt.Rows(0)("DispDate")).ToString(AppConfig.MaindateFormat)
                        End If
                    Else
                        If dt.Rows(0)("InStockAsset") Then
                            ShowStatus(0)
                        Else
                            ShowStatus(1)
                        End If
                        txtRetirementDate.Text = String.Empty
                    End If


                    ShowInventoryStatus(dt.Rows(0)("InvStatus"))

                    txtBussinessArea.Text = dt.Rows(0)("BussinessArea").ToString()
                    txtInventoryNumber.Text = dt.Rows(0)("InventoryNumber").ToString()
                    txtCreatedBY.Text = dt.Rows(0)("CreatedBY").ToString()
                    chkAssetWithValue.Checked = Not dt.Rows(0)("InStockAsset")
                    txtEvaGroup1.Text = dt.Rows(0)("EvaluationGroup1").ToString()
                    txtEvaGroup2.Text = dt.Rows(0)("EvaluationGroup2").ToString()
                    txtEvaGroup3.Text = dt.Rows(0)("EvaluationGroup3").ToString()
                    txtEvaGroup4.Text = dt.Rows(0)("EvaluationGroup4").ToString()

                    txtOldRefNumber.Text = dt.Rows(0)("CustomFld1").ToString()
                    txtOldBarcode.Text = dt.Rows(0)("CustomFld2").ToString()

                    If Not dt.Rows(0).IsNull("LastInventoryDate") Then
                        txtLastInventory.Text = CDate(dt.Rows(0)("LastInventoryDate")).ToString(AppConfig.MaindateFormat)
                    Else
                        txtLastInventory.Text = String.Empty
                    End If

                    txtUsefulLife.Text = GetUsefulYears(strAstID)
                    PreviousItemCost = 0

                    Dim arr As String() = dt.Rows(0)("RefNo").ToString().Split("-")
                    txtAssetNumber.Text = arr(1)
                    txtSubNumber.Text = arr(2)

                    txtAstNum.Text = dt.Rows(0)("AstNum").ToString()

                    txtCreation.Text = CDate(dt.Rows(0)("PurDate").ToString())

                    If (dt.Rows(0)("LocId").ToString <> "") Then
                        trvLocation.SetValue(dt.Rows(0)("LocId").ToString(), objBALLocation.Comp_Path(dt.Rows(0)("LocId").ToString()))
                        PrevLocationID = dt.Rows(0)("LocId").ToString()
                    Else
                        trvLocation.SelectedText = ""
                        trvLocation.SelectedValue = ""
                        PrevLocationID = String.Empty
                    End If



                    Get_Ast_History(txtAssetID.SelectedText)
                    Get_Ast_History_Custodian(txtAssetID.SelectedText)
                    Get_AssetsDetailsLog(txtAssetID.SelectedText)
                    LoadImage()
                    btnDelete.Visible = True
                End If
                valProvMain.Validate()
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
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

    Private Function GenerateAssetID(ByVal CompanyCode As String, ByVal AssetNumber As String, ByVal SubNumber As String) As Long
        Dim AssetTag As String = CompanyCode & AssetNumber & SubNumber
        If AssetTag.Length <= 0 Then
            Return 0
        End If
        Try

            Dim Num As Long = Long.Parse(AssetTag)
            Return Num
        Catch ex As Exception
            Dim sb As New System.Text.StringBuilder
            For Each ch As Char In AssetTag
                If Char.IsDigit(ch) Then
                    sb.Append(ch)
                End If
            Next
            Return Long.Parse(sb.ToString)
        End Try
    End Function

    Private Function ValidateAssetInfo(ByVal RefNumber As String) As Boolean
        If isEdit Then
            If objBALAssetDetails.Check_AstID(txtAssetID.SelectedText, False) Then
                txtAssetNumber.Focus()
                errProv.SetError(txtAssetID.TextBox, "Asset ID already exist, please change asset number to generate different AssetID")
                Return False
            End If
        Else
            If objBALAssetDetails.Check_AstID(txtAssetID.SelectedText, True) Then
                txtAssetNumber.Focus()
                errProv.SetError(txtAssetID.TextBox, "Asset ID already exist, please change asset number to generate different AssetID")
                Return False
            End If
        End If

        Return True
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            CheckDemo()
            errProv.ClearErrors()

            If Not String.IsNullOrEmpty(cmbComp.SelectedValue) And Not String.IsNullOrEmpty(txtAssetNumber.Text) And Not String.IsNullOrEmpty(txtSubNumber.Text) Then
                txtAssetID.Tag = "NewID"
                Dim objBALComp As New BALCompany

                Dim CompCode As String = objBALComp.GetCompanyCodeByCompanyID(cmbComp.SelectedValue)
                txtAssetID.SelectedText = GenerateAssetID(CompCode, txtAssetNumber.Text, txtSubNumber.Text)
                txtAstNum.Text = txtAssetID.SelectedText
            End If
            Dim RefNumber As String = GetRefNumber(cmbComp.SelectedValue, txtAssetNumber.Text, txtSubNumber.Text)

            If valProvMain.Validate And ValidateAssetInfo(RefNumber) Then
                Dim objattBookTemp As New attBook
                Dim objBALBookTemp As New BALBooks
                Dim objattAstBooks As New attAstBooks
                Dim ds As DataTable
                objattBookTemp.CompanyID = cmbComp.SelectedValue
                ds = objBALBookTemp.GetAll_Book(objattBookTemp)
                If ds Is Nothing Or ds.Rows.Count < 1 Then
                    ShowErrorMessage("No Asset Book associated with selected company, Asset cannot be saved.")
                    Exit Sub
                End If


                Try
                    Dim objattAssets As New attItems
                    Dim objBALAssets As New BALItems
                    Dim AssetItemID As String = ""

                    objattAssets.AstCatID = trvAssetClass.SelectedValue
                    objattAssets.AstDesc = txtAstDesc.Text.Trim
                    Dim dt As DataTable = objBALAssets.CheckItemDescription(objattAssets)
                    If dt.Rows.Count > 0 Then
                        AssetItemID = dt.Rows(0)(0)
                    Else
                        AssetItemID = objBALAssets.GetNextPKey_Item()
                        objattAssets.PKeyCode = AssetItemID
                        objBALAssets.Insert_Item(objattAssets)
                    End If

                    If isEdit Then
                        update_AssetDetails(AssetItemID, RefNumber)
                        'Set New location as previous location
                        PrevLocationID = trvLocation.SelectedValue
                        'Refresh History Grid.
                        Get_Ast_History(txtAssetID.SelectedText)
                        Get_AssetsDetailsLog(txtAssetID.SelectedText)
                    Else
                        If AllowAssetCreation Then
                            AddNew_AssetDetails(AssetItemID, RefNumber)
                            ShowStatus(0)
                            ShowInventoryStatus(0) 'Missing
                        Else
                            ShowErrorMessage("Insufficient privileges. Contact system administrator!")
                        End If
                    End If
                    SaveImage()
                Catch ex As Exception
                    GenericExceptionHandler(ex, WhoCalledMe)
                End Try

            Else
                ShowErrorMessage("Record can't be saved, check the fields that have error and try again.")
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
                    fname = txtAssetID.SelectedText & ".jpg"
                End If

                If PBLogo.ImageLocation <> AppConfig.ImgPath & "\" & fname Then
                    File.Copy(PBLogo.ImageLocation, AppConfig.ImgPath & "\" & fname, True)
                    PBLogo.ImageLocation = AppConfig.ImgPath & "\" & fname
                End If

            ElseIf AppConfig.ImgStorgeLoc = "Database" Then
                If AppConfig.ImgType = "Asset Images" Then
                    objattAssetDetails = New attAssetDetails
                    objattAssetDetails.PKeyCode = txtAssetID.SelectedText
                    Dim fs As New FileStream(PBLogo.ImageLocation, FileMode.Open)
                    Dim Data() As Byte = New [Byte](fs.Length) {}
                    fs.Read(Data, 0, fs.Length)
                    objattAssetDetails.Image = Data
                    objBALAssetDetails.UpdateAssetImage(objattAssetDetails)
                    fs.Dispose()
                End If
            End If
        End If
    End Sub
    Private Sub DeleteImage()
        Dim fname As String = ""
        If AppConfig.ImgStorgeLoc = "Shared Folder" Then

            If AppConfig.ImgType = "Asset Images" Then
                fname = txtAssetID.SelectedText & ".jpg"
            End If

            If File.Exists(AppConfig.ImgPath & "\" & fname) Then
                File.Delete(AppConfig.ImgPath & "\" & fname)
            End If
        ElseIf AppConfig.ImgStorgeLoc = "Database" Then
            If AppConfig.ImgType = "Asset Images" Then
                objattAssetDetails = New attAssetDetails
                objattAssetDetails.PKeyCode = txtAssetID.SelectedText
                objBALAssetDetails.DeleteAssetImage(objattAssetDetails)
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
                    fname = txtAssetID.SelectedText & ".jpg"
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
                    objattAssetDetails.PKeyCode = txtAssetID.SelectedText
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
                Else
                    btnDelImg.Visible = False
                    PBLogo.Image = Nothing
                    PBLogo.ImageLocation = Nothing
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Function GetRefNumber(ByVal CompID As String, ByVal AssetNumber As String, ByVal subNumber As String) As String
        Dim objBALComp As New BALCompany
        Dim CompCode As String = objBALComp.GetCompanyCodeByCompanyID(CompID)
        Return String.Format("{0}-{1}-{2}", CompCode, AssetNumber, subNumber)
    End Function


    Private Sub update_AssetDetails(ByVal AssetItemID As String, ByVal RefNo As String)
        Try
            objattAssetDetails = New attAssetDetails
            objattAssetDetails.BaseCost = 0
            objattAssetDetails.CustodianID = cmbEmployee.SelectedValue


            objattAssetDetails.SerailNo = RemoveUnnecessaryChars(txtSerialNo.Text)
            objattAssetDetails.ItemCode = AssetItemID
            objattAssetDetails.PurDate = txtCreation.Text
            objattAssetDetails.CompanyID = cmbComp.SelectedValue
            objattAssetDetails.TransRemarks = String.Empty
            objattAssetDetails.Tax = 0
            objattAssetDetails.SrvDate = Now.Date
            objattAssetDetails.AstBrandID = 1
            objattAssetDetails.AstDesc = RemoveUnnecessaryChars(txtAstDesc.Text)
            objattAssetDetails.AstDesc2 = RemoveUnnecessaryChars(txtAstDesc2.Text)
            objattAssetDetails.AstModel = String.Empty
            objattAssetDetails.Discount = 0
            objattAssetDetails.GLCode = 1


            objattAssetDetails.CostCenterID = cmbCostCenter.SelectedValue

            If Not String.IsNullOrEmpty(txtCapitalizationDate.Text) Then
                objattAssetDetails.CapitalizationDate = txtCapitalizationDate.Text
            Else
                objattAssetDetails.CapitalizationDate = Nothing
            End If

            If Not String.IsNullOrEmpty(txtRetirementDate.Text) Then
                objattAssetDetails.DispDate = txtRetirementDate.Text
            Else
                objattAssetDetails.DispDate = Nothing
            End If

            objattAssetDetails.BussinessArea = txtBussinessArea.Text
            objattAssetDetails.InventoryNumber = txtInventoryNumber.Text
            objattAssetDetails.InStockAsset = Not chkAssetWithValue.Checked
            objattAssetDetails.EvaluationGroup1 = txtEvaGroup1.Text
            objattAssetDetails.EvaluationGroup2 = txtEvaGroup2.Text
            objattAssetDetails.EvaluationGroup3 = txtEvaGroup3.Text
            objattAssetDetails.EvaluationGroup4 = txtEvaGroup4.Text
            objattAssetDetails.CustomFld1 = txtOldRefNumber.Text
            objattAssetDetails.CustomFld2 = txtOldBarcode.Text

            objattAssetDetails.IsDataChanged = True
            objattAssetDetails.LastEditBY = AppConfig.LoginName
            objattAssetDetails.LastEditDate = Now.Date

            objattAssetDetails.NoPiece = 1
            objattAssetDetails.LocID = trvLocation.SelectedValue
            objattAssetDetails.RefNo = RefNo
            objattAssetDetails.AstNum = txtAstNum.Text.ToString()


            objattAssetDetails.PKeyCode = txtAssetID.SelectedText
            objattAssetDetails.BarCode = txtAssetID.SelectedText


            If objBALAssetDetails.Update_AssetDetails(objattAssetDetails) Then
                Dim UsefulLife As Integer = 0
                If Not String.IsNullOrEmpty(txtUsefulLife.Text) Then
                    UsefulLife = txtUsefulLife.Text
                End If
                objBALAssetDetails.Update_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, 0, UsefulLife, 0, objattAssetDetails.SrvDate, False)
            End If

            PreviousItemCost = 0
            ShowInfoMessage("Record saved successfully")

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub AddNew_AssetDetails(ByVal AssetItemID As String, ByVal RefNo As String)
        Try
            objattAssetDetails = New attAssetDetails
            objattAssetDetails.PKeyCode = txtAssetID.SelectedText
            objattAssetDetails.BarCode = txtAssetID.SelectedText

            objattAssetDetails.BaseCost = 0
            objattAssetDetails.CustodianID = cmbEmployee.SelectedValue
            objattAssetDetails.SerailNo = RemoveUnnecessaryChars(txtSerialNo.Text)
            objattAssetDetails.Discount = 0
            objattAssetDetails.ItemCode = AssetItemID
            objattAssetDetails.PurDate = txtCreation.Text
            objattAssetDetails.Tax = 0
            objattAssetDetails.TransRemarks = "ZulAssets"

            objattAssetDetails.CompanyID = cmbComp.SelectedValue
            objattAssetDetails.SrvDate = Now.Date
            objattAssetDetails.RefNo = RefNo
            objattAssetDetails.AstNum = RemoveUnnecessaryChars(txtAstNum.Text)
            objattAssetDetails.AstBrandID = 1
            objattAssetDetails.AstDesc = RemoveUnnecessaryChars(txtAstDesc.Text)
            objattAssetDetails.AstDesc2 = RemoveUnnecessaryChars(txtAstDesc2.Text)
            objattAssetDetails.AstModel = String.Empty
            objattAssetDetails.GLCode = 1

            objattAssetDetails.CostCenterID = cmbCostCenter.SelectedValue



            objattAssetDetails.BussinessArea = txtBussinessArea.Text
            objattAssetDetails.InventoryNumber = txtInventoryNumber.Text
            objattAssetDetails.InStockAsset = Not chkAssetWithValue.Checked
            objattAssetDetails.EvaluationGroup1 = txtEvaGroup1.Text
            objattAssetDetails.EvaluationGroup2 = txtEvaGroup2.Text
            objattAssetDetails.EvaluationGroup3 = txtEvaGroup3.Text
            objattAssetDetails.EvaluationGroup4 = txtEvaGroup4.Text

            objattAssetDetails.CustomFld1 = txtOldRefNumber.Text
            objattAssetDetails.CustomFld2 = txtOldBarcode.Text

            objattAssetDetails.IsDataChanged = True

            objattAssetDetails.LocID = trvLocation.SelectedValue
            objattAssetDetails.NoPiece = 1



            objattAssetDetails.Disposed = False
            objattAssetDetails.IsSold = False
            objattAssetDetails.CapitalizationDate = Nothing
            objattAssetDetails.DispDate = Nothing
            objattAssetDetails.LastInventoryDate = Nothing
            objattAssetDetails.Sel_Date = Nothing

            objattAssetDetails.CreatedBY = txtCreatedBY.Text.Trim
            objattAssetDetails.LastEditBY = AppConfig.LoginName
            objattAssetDetails.LastEditDate = Now.Date
            objattAssetDetails.CreationDate = Now.Date
            objattAssetDetails.StatusID = 1
            objBALAssetDetails.Insert_AssetDetails(objattAssetDetails, True)

            isEdit = True

            'Create history for all inventory schedules.
            Dim objBALAst_INV_Schedule As New BALAst_INV_Schedule
            Dim dtInvSch As DataTable = objBALAst_INV_Schedule.Getcombo_Schedule()
            Dim objattAstHistory As attAstHistory
            Dim objBALAst_History As New BALAst_History

            For Each dr As DataRow In dtInvSch.Rows
                objattAstHistory = New attAstHistory
                objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                objattAstHistory.AstID = txtAssetID.SelectedText
                objattAstHistory.Status = 0
                objattAstHistory.InvSchCode = dr("InvSchCode")
                objattAstHistory.HisDate = Now.Date
                objattAstHistory.Fr_loc = trvLocation.SelectedValue
                objattAstHistory.To_Loc = trvLocation.SelectedValue
                objattAstHistory.NoPiece = 1
                objBALAst_History.Insert_Ast_History(objattAstHistory)
            Next

            Dim objBALAssets As New BALItems
            Dim ds As DataTable = objBALAssets.Get_DepPolicy(objattAssetDetails.ItemCode)
            If Not ds Is Nothing Then
                If ds.Rows.Count > 0 Then
                    Dim UsefulLife As Integer = 0
                    If Not String.IsNullOrEmpty(txtUsefulLife.Text) Then
                        UsefulLife = txtUsefulLife.Text
                    End If
                    objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, ds.Rows(0)("SalvageValue"), UsefulLife, ds.Rows(0)("SalvageMonth"), Now.Date, ds.Rows(0)("IsSalvageValuePercent"))
                End If
            End If


            If AnonymousId = "" Then
                Me.Search_Asset(txtAssetID.SelectedText)
            Else 'FormOpened from Anonymous in Data Processing
                'set this flag to delete the asset from Anonymous data after closing the form.
                AnonymousSaved = True
                objattAstHistory = New attAstHistory()
                objattAstHistory.AstID = txtAssetID.SelectedText
                objattAstHistory.Status = 5
                objattAstHistory.InvSchCode = InvScheduleId
                objattAstHistory.HisDate = TransDate
                objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                objattAstHistory.Fr_loc = trvLocation.SelectedValue
                objattAstHistory.To_Loc = trvLocation.SelectedValue
                objBALAst_History.Insert_Ast_History(objattAstHistory)

                objattAssetDetails = New attAssetDetails
                objattAssetDetails.PKeyCode = txtAssetID.SelectedText
                objattAssetDetails.InvStatus = 5 'Means Anonymous
                objattAssetDetails.InvSchCode = InvScheduleId
                objattAssetDetails.LocID = trvLocation.SelectedValue
                objattAssetDetails.IsDataChanged = True
                objBALAssetDetails.Update_InvStatus(objattAssetDetails)
            End If
            ShowInfoMessage("Record saved successfully")

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not AllowDeleteAsset Then
                ShowErrorMessage("Insufficient privileges. Contact system administrator!")
                Return
            End If

            If objBALAssetDetails.Check_AstID(txtAssetID.SelectedText, True) Then
                If MessageBox.Show("Deleting this Asset will delete all its transfer history, Do you want to continue?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    objattAssetDetails = New attAssetDetails
                    objattAssetDetails.PKeyCode = txtAssetID.SelectedText
                    If objBALAssetDetails.Delete_Details(objattAssetDetails) Then

                        If AppConfig.ImgStorgeLoc = "Shared Folder" Then
                            DeleteImage()
                        End If
                        ShowInfoMessage("Record deleted successfully")
                        btnNewAsset_Click(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub AssetsID_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAssetID.SelectTextChanged
        If txtAssetID.Tag = "NewID" Then
            txtAssetID.Tag = ""
        Else
            If txtAssetID.SelectedText <> "" Then
                Me.Search_Asset(txtAssetID.SelectedText)
            End If
        End If
    End Sub


    Private Sub btnDelImg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelImg.Click
        Try
            If MessageBox.Show("Do you really want to delete this image ?", "ZulAssets", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                DeleteImage()
                ShowInfoMessage("Image deleted successfully")
                btnDelImg.Visible = False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        Dim FirstAstID As String = ""
        objattAssetDetails = New attAssetDetails
        objattAssetDetails.CompanyID = 0
        FirstAstID = objBALAssetDetails.Get_AssetsDetails_First_Last(objattAssetDetails, 1)
        If FirstAstID <> "" Then
            txtAssetID.Tag = String.Empty
            txtAssetID.SelectedText = FirstAstID
        End If
    End Sub

    Private Sub btnLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLast.Click
        Dim LastAstID As String = ""
        objattAssetDetails = New attAssetDetails
        objattAssetDetails.CompanyID = 0
        LastAstID = objBALAssetDetails.Get_AssetsDetails_First_Last(objattAssetDetails, 2)

        If LastAstID <> "" Then
            txtAssetID.Tag = String.Empty
            txtAssetID.SelectedText = LastAstID
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
            txtAssetID.Tag = String.Empty
            txtAssetID.SelectedText = NextAstId
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
            objattAssetDetails.CompanyID = 0

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
            txtAssetID.Tag = String.Empty
            txtAssetID.SelectedText = PreAstId
        End If

    End Sub

    'Used when asset is retired
    Private Sub SetReadOnly(ByVal IsReadOnly As Boolean)
        txtAstDesc.Properties.ReadOnly = IsReadOnly
        txtAstDesc2.Properties.ReadOnly = IsReadOnly
        txtSerialNo.Properties.ReadOnly = IsReadOnly
        cmbEmployee.TextReadOnly = IsReadOnly
        trvLocation.TextReadOnly = IsReadOnly
        trvAssetClass.TextReadOnly = IsReadOnly
        grpImage.Enabled = Not IsReadOnly
    End Sub

    'Used for existed assets.
    Private Sub UpdateControlAlteration(ByVal IsReadOnly As Boolean)

        txtAssetNumber.Properties.ReadOnly = IsReadOnly
        txtSubNumber.Properties.ReadOnly = IsReadOnly
        txtBussinessArea.Properties.ReadOnly = IsReadOnly
        txtInventoryNumber.Properties.ReadOnly = IsReadOnly
        txtUsefulLife.Properties.ReadOnly = IsReadOnly
        txtCreatedBY.Properties.ReadOnly = IsReadOnly
        txtEvaGroup1.Properties.ReadOnly = IsReadOnly
        txtEvaGroup2.Properties.ReadOnly = IsReadOnly
        txtEvaGroup3.Properties.ReadOnly = IsReadOnly
        txtEvaGroup4.Properties.ReadOnly = IsReadOnly
        txtOldBarcode.Properties.ReadOnly = IsReadOnly
        txtOldRefNumber.Properties.ReadOnly = IsReadOnly

        cmbComp.TextReadOnly = IsReadOnly
        cmbCostCenter.TextReadOnly = IsReadOnly
        trvAssetClass.TextReadOnly = IsReadOnly
    End Sub

    Private Sub ShowInventoryStatus(ByVal Status As Integer)
        'pnlInventoryStatus.Visible = True
        Select Case Status
            Case 0
                lblInvStatus.Text = "Missing"
                imgInvStatus.Image = My.Resources.Invalid
                lblInvStatus.ForeColor = Color.Red
            Case 1
                lblInvStatus.Text = "Found"
                imgInvStatus.Image = My.Resources.Valid
                lblInvStatus.ForeColor = Color.Green
            Case 2
                lblInvStatus.Text = "Misplaced"
                imgInvStatus.Image = My.Resources.Misspalced
                lblInvStatus.ForeColor = Color.Red
            Case 3
                lblInvStatus.Text = "Transferred"
                imgInvStatus.Image = My.Resources.Refresh16x16
                lblInvStatus.ForeColor = Color.Blue
            Case 5
                lblInvStatus.Text = "Anonymous"
                imgInvStatus.Image = My.Resources.Attention
                lblInvStatus.ForeColor = Color.OrangeRed
        End Select
    End Sub

    Private Sub ShowStatus(ByVal Status As Integer)
        pnlStatus.Visible = True
        Select Case Status
            Case 0
                lblStatus.Text = "Without Value"
                imgStatus.Image = My.Resources.Attention
                lblStatus.ForeColor = Color.OrangeRed
                SetReadOnly(False)
                UpdateControlAlteration(True)
            Case 1
                lblStatus.Text = "With Value"
                imgStatus.Image = My.Resources.check
                lblStatus.ForeColor = Color.Blue
                SetReadOnly(False)
                UpdateControlAlteration(True)
            Case 2
                lblStatus.Text = "Retired"
                imgStatus.Image = My.Resources.Invalid
                lblStatus.ForeColor = Color.Red
                SetReadOnly(True)
                UpdateControlAlteration(True)
            Case 3 'New Asset
                lblStatus.Text = "Without Value"
                imgStatus.Image = My.Resources.Attention
                lblStatus.ForeColor = Color.OrangeRed
                SetReadOnly(False)
                UpdateControlAlteration(False)
        End Select
    End Sub


    Private Sub btnNewAsset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewAsset.Click, btnNewAsset1.Click
        Try
            txtSerialNo.Text = ""
            cmbEmployee.SelectedText = ""
            cmbCostCenter.SelectedText = ""
            cmbCostCenter.Text = ""
            cmbComp.SelectedText = ""
            cmbEmployee.Text = ""

            txtLabelPrintedCnt.Text = ""
            txtSource.Text = "ZulAssets"
            trvLocation.SelectedText = ""
            trvLocation.SelectedValue = ""
            txtAstDesc.Text = ""
            txtAstDesc2.Text = ""
            trvAssetClass.SelectedValue = ""
            trvAssetClass.SelectedText = ""


            cmbCostCenter.SelectedText = ""
            cmbCostCenter.SelectedValue = ""

            txtCreation.Text = Now.Date.ToString(AppConfig.MaindateFormat)
            txtCapitalizationDate.Text = String.Empty
            txtLastInventory.Text = String.Empty
            txtRetirementDate.Text = String.Empty

            txtPlant.Text = String.Empty
            txtLocation.Text = String.Empty


            txtBussinessArea.Text = String.Empty
            txtInventoryNumber.Text = String.Empty
            txtCreatedBY.Text = String.Empty
            chkAssetWithValue.Checked = False
            txtEvaGroup1.Text = String.Empty
            txtEvaGroup2.Text = String.Empty
            txtEvaGroup3.Text = String.Empty
            txtEvaGroup4.Text = String.Empty
            txtOldBarcode.Text = String.Empty
            txtOldRefNumber.Text = String.Empty

            txtUsefulLife.Text = String.Empty

            txtAssetID.Tag = "NewID"
            txtAssetID.SelectedText = String.Empty
            txtAssetID.Tag = ""

            txtAssetNumber.Text = String.Empty
            txtSubNumber.Text = String.Empty
            txtAstNum.Text = String.Empty
            btnDelete.Visible = False
            btnDelImg.Visible = False

            PBLogo.Image = Nothing
            PBLogo.ImageLocation = Nothing


            If grdAstHist.DataSource IsNot Nothing Then
                CType(grdAstHist.DataSource, DataTable).Rows.Clear()
            End If
            If grdCustHist.DataSource IsNot Nothing Then
                CType(grdCustHist.DataSource, DataTable).Rows.Clear()
            End If


            isEdit = False

            errProv.ClearErrors()
            valProvMain.RemoveControlError(cmbEmployee.TextBox)
            valProvMain.RemoveControlError(txtAssetID.TextBox)
            valProvMain.RemoveControlError(cmbComp.TextBox)
            valProvMain.RemoveControlError(txtAssetNumber)
            valProvMain.RemoveControlError(txtSubNumber)
            valProvMain.RemoveControlError(trvLocation.TextBox)
            valProvMain.RemoveControlError(trvAssetClass.TextBox)

            PrevLocationID = String.Empty
            tabControl.SelectedTabPageIndex = 0
            cmbComp.Focus()
            ShowStatus(3)
            ShowInventoryStatus(0) 'Missing
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub txtPieceNo_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        If CType(sender, NumericUpDown).Value > CType(sender, NumericUpDown).Maximum Then
            CType(sender, NumericUpDown).Value = CType(sender, NumericUpDown).Maximum
        End If
    End Sub


    'Private Sub grdAstHistView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdAstHistView.FocusedRowChanged
    '    Dim FocRow As Integer = grdAstHistView.FocusedRowHandle
    '    If FocRow >= 0 Then
    '        If grdAstHistView.GetRowCellValue(FocRow, "Fr_loc").ToString <> "" Then
    '            Dim objBALCategory As New BALLocation
    '            txtPreLoc.Text = objBALCategory.Comp_Path(grdAstHistView.GetRowCellValue(FocRow, "Fr_loc").ToString())
    '        End If
    '        If grdAstHistView.GetRowCellValue(FocRow, "To_Loc").ToString <> "" Then
    '            Dim objBALCategory As New BALLocation
    '            txtCurrentLoc.Text = objBALCategory.Comp_Path(grdAstHistView.GetRowCellValue(FocRow, "To_Loc").ToString())
    '        End If
    '    End If
    'End Sub

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

    Private Sub cmbCust_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEmployee.LovBtnClick
        Try
            Dim objBALCustodian As New BALCustodian
            cmbEmployee.ValueMember = "ID"
            cmbEmployee.DisplayMember = "Name"
            cmbEmployee.DataSource = objBALCustodian.GetAllData_GetCombo()
            cmbEmployee.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub AssetsID_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAssetID.LovBtnClick
        Try
            txtAssetID.ValueMember = "Barcode"
            txtAssetID.DisplayMember = "Barcode"

            Dim objBALIntegQuery As New BALABBIntegration
            txtAssetID.DataSource = objBALIntegQuery.GetAssetABBLov()
            txtAssetID.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub txtPlant_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPlant.TextChanged
        If String.IsNullOrEmpty(txtPlant.Tag) Then
            Dim locID As String = objBALLocation.GetLocID(txtPlant.Text.Trim)
            trvLocation.Tag = "Update"
            If Not String.IsNullOrEmpty(locID) And locID <> "0" Then
                trvLocation.SetValue(locID, objBALLocation.Comp_Path(locID))
            Else
                trvLocation.SelectedText = String.Empty
                trvLocation.SelectedValue = String.Empty
                txtLocation.Text = String.Empty
            End If
            trvLocation.Tag = String.Empty
        End If
    End Sub

    Private Sub txtLocation_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLocation.TextChanged
        If String.IsNullOrEmpty(txtLocation.Tag) Then
            Dim locID As String = objBALLocation.GetLocID(txtLocation.Text.Trim)
            trvLocation.Tag = "Update"
            If Not String.IsNullOrEmpty(locID) And locID <> "0" Then
                trvLocation.SetValue(locID, objBALLocation.Comp_Path(locID))
            Else
                trvLocation.SelectedText = String.Empty
                trvLocation.SelectedValue = String.Empty
            End If
            trvLocation.Tag = String.Empty
        End If
    End Sub

    Private Sub ZTLocation_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvLocation.SelectTextChanged
        If String.IsNullOrEmpty(trvLocation.Tag) Then
            If Not String.IsNullOrEmpty(trvLocation.SelectedText) Then
                txtLocation.Tag = "Update"
                txtPlant.Tag = "Update"
                Dim arr As String() = trvLocation.SelectedText.Split("\")
                If arr.Length > 0 Then
                    txtPlant.Text = arr(0).Trim
                Else
                    txtPlant.Text = String.Empty
                End If

                If arr.Length > 1 Then
                    txtLocation.Text = arr(1).Trim
                Else
                    txtLocation.Text = String.Empty
                End If
                txtLocation.Tag = String.Empty
                txtPlant.Tag = String.Empty
            End If
        End If

    End Sub

    Private Sub ZTLocation_TreeBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvLocation.TreeBtnClick
        Try
            Dim ds As New DataTable
            Dim objBALLocation As New BALLocation
            trvLocation.DataSource = objBALLocation.GetComboLocations(New attLocation)
            trvLocation.OpenLOV()
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

   

    Private Sub Get_AssetsDetailsLog(ByVal AssetID As String)
        Try
            Application.DoEvents()
            Dim obj As New BALABBIntegration
            grdLog.DataSource = obj.GetAssetLogGridABB(AssetID)
            format_grdAssetsABBIntegration()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub format_grdAssetsABBIntegration()
        grdLogView.Columns("ID").Visible = False
        grdLogView.Columns("CompanyCode").Visible = False
        grdLogView.Columns("AssetNumber").Visible = False
        grdLogView.Columns("SubNumber").Visible = False


        grdLogView.OptionsView.ColumnAutoWidth = False
        grdLog.UseEmbeddedNavigator = True
        grdLog.EmbeddedNavigator.Buttons.Append.Visible = False
        grdLog.EmbeddedNavigator.Buttons.Edit.Visible = False
        grdLog.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grdLog.EmbeddedNavigator.Buttons.Remove.Visible = False
        grdLog.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

        addGridMenu(grdLog)
    End Sub
End Class