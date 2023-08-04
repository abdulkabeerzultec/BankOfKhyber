Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO
Imports System.Globalization.CultureInfo
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.DXErrorProvider
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Text

Public Class frmAssetsDetails

#Region " -- My Decleration -- "
    Dim objattAssetDetails As attAssetDetails
    Dim objBALAssetDetails As New BALAssetDetails
    Dim objattAstBooks As attAstBooks
    Dim objBALAstBooks As New BALAstBooks
    Dim objBALLocation As New BALLocation
    Dim objBALCategory As New BALCategory


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
        If ds.Rows.Count > 0 Then
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
        End If
    End Sub

    Public Sub LocateAsset(ByVal astID As String)
        If astID <> "" Then
            txtAssetID.SelectedValue = astID
            txtAssetID.SelectedText = astID
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

#Region " -- Methods -- "

#Region "Formating Grids"
    Private Sub format_CustHistGrid()
        If grdCustHistView.Columns.Count <= 0 Then
            Exit Sub
        End If
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
        If grdAstHistView.Columns.Count <= 0 Then
            Exit Sub
        End If

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
                    txtAssetID.SelectedText = dt.Rows(0)("Barcode").ToString()
                    txtSerialNo.Text = dt.Rows(0)("SerailNo").ToString()
                    txtAstDesc.Text = dt.Rows(0)("AstDesc").ToString()
                    txtAstDesc2.Text = dt.Rows(0)("AstDesc2").ToString()
                    txtLabelPrintedCnt.Text = dt.Rows(0)("LabelCount").ToString()
                    txtSource.Text = dt.Rows(0)("TransRemarks").ToString

                    cmbCostCenter.FindRow(dt.Rows(0)("CostID").ToString(), dt.Rows(0)("CostName").ToString())

                    If CType(dt.Rows(0)("Disposed"), Boolean) Then
                        ShowStatus(2)
                    Else
                        If dt.Rows(0)("InStockAsset") Then
                            ShowStatus(0)
                        Else
                            ShowStatus(1)
                        End If
                    End If


                    ShowInventoryStatus(dt.Rows(0)("InvStatus"))

                    txtCreatedBY.Text = dt.Rows(0)("CreatedBY").ToString()
                    chkAssetWithValue.Checked = Not dt.Rows(0)("InStockAsset")


                    If Not dt.Rows(0).IsNull("LastInventoryDate") Then
                        txtLastInventory.Text = CDate(dt.Rows(0)("LastInventoryDate")).ToString(AppConfig.MaindateFormat)
                    Else
                        txtLastInventory.Text = String.Empty
                    End If

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



                    Get_Ast_History(txtAssetID.SelectedValue)
                    Get_Ast_History_Custodian(txtAssetID.SelectedValue)
                    Get_AssetsDetailsLog(txtAssetID.SelectedValue)
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



    Private Function ValidateAssetInfo(ByVal RefNumber As String) As Boolean
        If isEdit Then
            If objBALAssetDetails.Check_AstID(txtAssetID.SelectedValue, False) Then
                txtAssetNumber.Focus()
                errProv.SetError(txtAssetID.TextBox, "Asset ID already exist, please change asset number to generate different AssetID")
                Return False
            End If
        Else
            If objBALAssetDetails.Check_AstID(txtAssetID.SelectedValue, True) Then
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
                txtAssetID.SelectedText = CompCode & txtAssetNumber.Text & txtSubNumber.Text
                txtAssetID.SelectedValue = GenerateAssetID(CompCode, txtAssetNumber.Text, txtSubNumber.Text)
                txtAstNum.Text = txtAssetID.SelectedValue
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
                        Get_Ast_History(txtAssetID.SelectedValue)
                        Get_AssetsDetailsLog(txtAssetID.SelectedValue)
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
                    fname = txtAssetID.SelectedValue & ".jpg"
                End If

                If PBLogo.ImageLocation <> AppConfig.ImgPath & "\" & fname Then
                    File.Copy(PBLogo.ImageLocation, AppConfig.ImgPath & "\" & fname, True)
                    PBLogo.ImageLocation = AppConfig.ImgPath & "\" & fname
                End If

            ElseIf AppConfig.ImgStorgeLoc = "Database" Then
                If AppConfig.ImgType = "Asset Images" Then
                    objattAssetDetails = New attAssetDetails
                    objattAssetDetails.PKeyCode = txtAssetID.SelectedValue
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
                fname = txtAssetID.SelectedValue & ".jpg"
            End If

            If File.Exists(AppConfig.ImgPath & "\" & fname) Then
                File.Delete(AppConfig.ImgPath & "\" & fname)
            End If
        ElseIf AppConfig.ImgStorgeLoc = "Database" Then
            If AppConfig.ImgType = "Asset Images" Then
                objattAssetDetails = New attAssetDetails
                objattAssetDetails.PKeyCode = txtAssetID.SelectedValue
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
                    fname = txtAssetID.SelectedValue & ".jpg"
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
                    objattAssetDetails.PKeyCode = txtAssetID.SelectedValue
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

            objattAssetDetails.DispDate = Nothing

            objattAssetDetails.BussinessArea = ""
            objattAssetDetails.InventoryNumber = ""
            objattAssetDetails.InStockAsset = Not chkAssetWithValue.Checked
            objattAssetDetails.EvaluationGroup1 = ""
            objattAssetDetails.EvaluationGroup2 = ""
            objattAssetDetails.EvaluationGroup3 = ""
            objattAssetDetails.EvaluationGroup4 = ""
            objattAssetDetails.IsDataChanged = True
            objattAssetDetails.LastEditBY = AppConfig.LoginName
            objattAssetDetails.LastEditDate = Now

            objattAssetDetails.NoPiece = 1
            objattAssetDetails.LocID = trvLocation.SelectedValue
            objattAssetDetails.RefNo = RefNo
            objattAssetDetails.AstNum = txtAstNum.Text.ToString()

            objattAssetDetails.PKeyCode = txtAssetID.SelectedValue
            objattAssetDetails.BarCode = txtAssetID.SelectedText


            If objBALAssetDetails.Update_AssetDetails(objattAssetDetails) Then
                Dim UsefulLife As Integer = 0

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
            objattAssetDetails.PKeyCode = txtAssetID.SelectedValue
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



            objattAssetDetails.BussinessArea = ""
            objattAssetDetails.InventoryNumber = ""
            objattAssetDetails.InStockAsset = Not chkAssetWithValue.Checked
            objattAssetDetails.EvaluationGroup1 = ""
            objattAssetDetails.EvaluationGroup2 = ""
            objattAssetDetails.EvaluationGroup3 = ""
            objattAssetDetails.EvaluationGroup4 = ""
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
            objattAssetDetails.LastEditDate = Now
            objattAssetDetails.CreationDate = Now
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
                objattAstHistory.AstID = txtAssetID.SelectedValue
                objattAstHistory.Status = 0
                objattAstHistory.InvSchCode = dr("InvSchCode")
                objattAstHistory.HisDate = DateTime.Now
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
                    objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, ds.Rows(0)("SalvageValue"), UsefulLife, ds.Rows(0)("SalvageMonth"), Now.Date, ds.Rows(0)("IsSalvageValuePercent"))
                End If
            End If


            If AnonymousId = "" Then
                Me.Search_Asset(txtAssetID.SelectedValue)
            Else 'FormOpened from Anonymous in Data Processing
                'set this flag to delete the asset from Anonymous data after closing the form.
                AnonymousSaved = True
                objattAstHistory = New attAstHistory()
                objattAstHistory.AstID = txtAssetID.SelectedValue
                objattAstHistory.Status = 5
                objattAstHistory.InvSchCode = InvScheduleId
                objattAstHistory.HisDate = TransDate
                objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                objattAstHistory.Fr_loc = trvLocation.SelectedValue
                objattAstHistory.To_Loc = trvLocation.SelectedValue
                objBALAst_History.Insert_Ast_History(objattAstHistory)

                objattAssetDetails = New attAssetDetails
                objattAssetDetails.PKeyCode = txtAssetID.SelectedValue
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

            If objBALAssetDetails.Check_AstID(txtAssetID.SelectedValue, True) Then
                If MessageBox.Show("Deleting this Asset will delete all its transfer history, Do you want to continue?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    objattAssetDetails = New attAssetDetails
                    objattAssetDetails.PKeyCode = txtAssetID.SelectedValue
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
            If txtAssetID.SelectedValue <> "" Then
                Me.Search_Asset(txtAssetID.SelectedValue)
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
            txtAssetID.SelectedValue = FirstAstID
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
            txtAssetID.SelectedValue = LastAstID
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
            txtAssetID.SelectedValue = NextAstId
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
            txtAssetID.SelectedValue = PreAstId
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
        txtCreatedBY.Properties.ReadOnly = IsReadOnly
        cmbComp.TextReadOnly = IsReadOnly
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
        'pnlStatus.Visible = True
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
            txtLastInventory.Text = String.Empty

            txtLocation.Text = String.Empty



            txtCreatedBY.Text = String.Empty
            chkAssetWithValue.Checked = False


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
            If grdLog.DataSource IsNot Nothing Then
                CType(grdLog.DataSource, DataTable).Rows.Clear()
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

    Public Function GetAssetLogGrid(ByVal AssetID As String) As DataTable
        Dim SQLcmd As New SqlCommand
        Dim strQuery As New StringBuilder
        strQuery.Append(" select    AstNum,Cast (Assets.ItemCode as bigint) as itemcode,Assets.AstDesc,AssetDetailsLog.AstID,Location.LocDesc,Custodian.CustodianName,AssetDetailsLog.LocID,Assets.AstCatID,AssetDetailsLog.CustodianID,Companies.CompanyName,AssetDetailsLog.BarCode,AssetDetailsLog.Refno,AssetDetailsLog.CompanyID,AssetDetailsLog.Purdate,AssetDetailsLog.AstModel,Category.AstCatDesc,AssetDetailsLog.TransRemarks,AssetDetailsLog.BaseCost,AssetDetailsLog.Tax,AssetDetailsLog.SrvDate,AssetDetailsLog.InsID,AssetDetailsLog.InvNumber,AssetDetailsLog.POCode,AssetDetailsLog.SuppID,AssetDetailsLog.Disposed,AssetDetailsLog.Discount,AssetDetailsLog.Barcode,AssetDetailsLog.SerailNo,RefCode,Plate,Poerp,Capex,Grn,AssetDetailsLog.NoPiece,AssetDetailsLog.GLCode,AssetDetailsLog.PONumber,AssetDetailsLog.LabelCount ,AssetDetailsLog.AstDesc as AssetDetailsdesc1, AssetDetailsLog.AstDesc2  as AssetDetailsdesc2,Category.CatFullPath,Location.LocationFullPath,CapitalizationDate,BussinessArea,InventoryNumber,CostCenterID,CreatedBY,CostCenter.CostNumber,CostCenter.CostName,InStockAsset,EvaluationGroup1,EvaluationGroup2,EvaluationGroup3,EvaluationGroup4,LastInventoryDate,InvStatus,AstBooks.SalvageYear as UsefulLife ")
        strQuery.Append(" ,SUBSTRING ( RefNo , charindex('-',RefNo) +1 , charindex('-',RefNo,charindex('-',RefNo) + 2) - charindex('-',RefNo)-1 ) as AssetNumber,")
        strQuery.Append(" SUBSTRING ( RefNo , charindex('-',RefNo,charindex('-',RefNo) + 2) +1,len(RefNo)-charindex('-',RefNo,charindex('-',RefNo) + 2)) as SubNumber,ActionType ,convert(varchar,ActionDate,100) as ActionDate,Companies.CompanyCode,AssetDetailsLog.LastEditBY")
        strQuery.Append(" from Assets")
        strQuery.Append("       inner join(AssetDetailsLog ")
        strQuery.Append("           left outer join Location on AssetDetailsLog.LocID = Location.LocID ")
        strQuery.Append("        left outer join Companies on AssetDetailsLog.CompanyID = Companies.CompanyID ")
        strQuery.Append("               left outer join AstBooks on AssetDetailsLog.AstID = AstBooks.AstID ")
        strQuery.Append("           left outer join Custodian on AssetDetailsLog.CustodianID = custodian.CustodianID ")
        strQuery.Append("        left outer join CostCenter on AssetDetailsLog.CostCenterID = CostCenter.CostID )")
        strQuery.Append("           on Assets.ItemCode = AssetDetailsLog.ItemCode ")
        strQuery.Append(" left outer join Category on Assets.AstCatID = Category.AstCatID ")

        If AssetID <> "" Then
            strQuery.Append(" where AssetDetailsLog.AstID = '" & AssetID & "'")
        End If
        strQuery.Append("  order by convert(varchar,ActionDate,100) desc")
        SQLcmd.CommandText = strQuery.ToString
        Try
            Return GenericDAL.DBOperations.ExecuteReader(SQLcmd)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Private Function GetAssetLovData() As DataTable
        Dim SQLcmd As New SqlCommand
        Dim strQuery As New StringBuilder
        strQuery.Append("select AssetDetails.AstID,AssetDetails.Barcode,Companies.CompanyCode,SUBSTRING ( RefNo , charindex('-',RefNo) +1 , charindex('-',RefNo,charindex('-',RefNo) + 2) - charindex('-',RefNo)-1 ) as AssetNumber,")
        strQuery.Append(" SUBSTRING ( RefNo , charindex('-',RefNo,charindex('-',RefNo) + 2) +1,len(RefNo)-charindex('-',RefNo,charindex('-',RefNo) + 2)) as SubNumber")
        strQuery.Append(" ,SerailNo as SerialNo,Location.LocationFullPath,CatFullPath")
        strQuery.Append(" from Assets inner join(AssetDetails left outer join Location on AssetDetails.LocID = Location.LocID  )")
        strQuery.Append(" on Assets.ItemCode = AssetDetails.ItemCode inner join Category on Assets.AstCatID = Category.AstCatID")
        strQuery.Append(" inner join Companies on Companies.CompanyId = AssetDetails.CompanyId")
        strQuery.Append(" where AssetDetails.IsDeleted = 0")
        SQLcmd.CommandText = strQuery.ToString
        Try
            Return GenericDAL.DBOperations.ExecuteReader(SQLcmd)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Private Sub AssetsID_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAssetID.LovBtnClick
        Try
            txtAssetID.ValueMember = "AstID"
            txtAssetID.DisplayMember = "Barcode"

            txtAssetID.DataSource = GetAssetLovData()
            txtAssetID.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub txtRegion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPlant.TextChanged, txtLocation.TextChanged
        Dim txt As TextEdit = CType(sender, TextEdit)
        If String.IsNullOrEmpty(txt.Tag) Then
            Dim locID As String = objBALLocation.GetLocID(txt.Text.Trim)
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
                txtPlant.Tag = "Update"
                txtLocation.Tag = "Update"
             

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
                txtPlant.Tag = String.Empty
                txtLocation.Tag = String.Empty
            Else
                txtPlant.Text = String.Empty
                txtLocation.Text = String.Empty
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

            grdLog.DataSource = GetAssetLogGrid(AssetID)
            format_grdAssets()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub format_grdAssets()
        If grdLogView.Columns.Count <= 0 Then
            Exit Sub
        End If
        grdLogView.Columns(0).Visible = False
        grdLogView.Columns(1).Visible = False
        grdLogView.Columns(2).Visible = False
        grdLogView.Columns(3).Visible = False
        grdLogView.Columns(4).Visible = False

        grdLogView.Columns(5).Caption = "Employee"
        grdLogView.Columns(5).VisibleIndex = 4

        grdLogView.Columns("AssetNumber").VisibleIndex = 2
        grdLogView.Columns("SubNumber").VisibleIndex = 3

        grdLogView.Columns("CompanyCode").Caption = "Company"
        grdLogView.Columns("CompanyCode").Width = 100
        grdLogView.Columns("CompanyCode").VisibleIndex = 1

        grdLogView.Columns("BarCode").Caption = "BarCode"
        grdLogView.Columns("BarCode").Width = 100
        grdLogView.Columns("BarCode").Name = "BarCode"
        grdLogView.Columns("BarCode").VisibleIndex = 0


        grdLogView.Columns(6).Visible = False
        grdLogView.Columns(7).Visible = False


        grdLogView.Columns(8).Visible = False
        grdLogView.Columns(9).Visible = False



        grdLogView.Columns(12).Visible = False
        grdLogView.Columns(13).Visible = False
        grdLogView.Columns(14).Visible = False
        grdLogView.Columns(15).Visible = False
        grdLogView.Columns(16).Visible = False
        grdLogView.Columns(17).Visible = False
        grdLogView.Columns(18).Visible = False
        grdLogView.Columns(19).Visible = False
        grdLogView.Columns(20).Visible = False
        grdLogView.Columns(21).Visible = False
        grdLogView.Columns(22).Visible = False
        grdLogView.Columns(23).Visible = False
        grdLogView.Columns(24).Visible = False
        grdLogView.Columns(25).Visible = False
        grdLogView.Columns(26).Visible = False
        grdLogView.Columns(27).Visible = False
        grdLogView.Columns(28).Visible = False
        grdLogView.Columns(29).Visible = False
        grdLogView.Columns(30).Visible = False
        grdLogView.Columns(31).Visible = False
        grdLogView.Columns(32).Visible = False
        grdLogView.Columns(33).Visible = False
        grdLogView.Columns(34).Visible = False
        grdLogView.Columns(35).Visible = False
        grdLogView.Columns(36).Visible = False
        grdLogView.Columns(37).Visible = False
        grdLogView.Columns(38).Visible = False
        grdLogView.Columns(39).Visible = False

        grdLogView.Columns(40).Caption = "Label Count"
        grdLogView.Columns(40).Width = 90
        grdLogView.Columns(40).Name = "LabelCount"
        grdLogView.Columns(40).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        grdLogView.Columns(40).VisibleIndex = grdLogView.Columns.Count - 2
        grdLogView.Columns(41).Visible = False
        grdLogView.Columns(42).Visible = False



        grdLogView.Columns("AssetDetailsdesc1").Visible = True
        grdLogView.Columns("AssetDetailsdesc2").Visible = True
        grdLogView.Columns("AssetDetailsdesc1").Caption = "Description1"
        grdLogView.Columns("AssetDetailsdesc2").Caption = "Description2"

        grdLogView.Columns("CatFullPath").Caption = "Class"
        grdLogView.Columns("LocationFullPath").Caption = "Location"
        grdLogView.Columns("InvStatus").Visible = False
        grdLogView.Columns("CapitalizationDate").Visible = False
        grdLogView.Columns("BussinessArea").Visible = False
        grdLogView.Columns("InventoryNumber").Visible = False
        grdLogView.Columns("CostCenterID").Visible = False
        grdLogView.Columns("InStockAsset").Visible = False
        grdLogView.Columns("EvaluationGroup1").Visible = False
        grdLogView.Columns("EvaluationGroup2").Visible = False
        grdLogView.Columns("EvaluationGroup3").Visible = False
        grdLogView.Columns("EvaluationGroup4").Visible = False
        grdLogView.Columns("CreatedBY").Visible = True
        grdLogView.Columns("CostName").Visible = False
        grdLogView.Columns("UsefulLife").Visible = False
        grdLogView.Columns("SerailNo").Visible = True

        grdLogView.Columns("Purdate").Visible = True
        grdLogView.Columns("Purdate").Caption = "Creation Date"

        grdLogView.Columns("ActionType").VisibleIndex = 5
        grdLogView.Columns("ActionDate").VisibleIndex = 6

        grdLogView.Columns("LastEditBY").Visible = True
        grdLogView.Columns("LastEditBY").VisibleIndex = 7
        grdLogView.Columns("LastEditBY").Caption = "Modified BY"

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