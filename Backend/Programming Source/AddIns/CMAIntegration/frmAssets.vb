Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports DevExpress.XtraEditors.DXErrorProvider

Public Class frmAssets
    Dim isEdit As Boolean = False
    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    Public valRulenotEmpty As New ConditionValidationRule
    Dim objBALAssetDetails As New BALAssetDetails
    Dim objattAssetDetails As attAssetDetails
    Dim objattLocation As attLocation
    Dim objBALLocation As New BALLocation
    Dim objBALCategory As New BALCategory

    Public IsDemokey As Boolean = False
    Private DemoAssetsCount As Integer = 10
    'Anonymous Data
    Private AnonymousId As String = ""
    Private DeviceID As String
    Private TransDate As DateTime
    Private InvScheduleId As String = ""
    Public AnonymousSaved As Boolean = False

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Function ValidateCatLoc() As Boolean
        errProv.ClearErrors()
        Dim FulCat As String = ZTCategory.SelectedText
        Dim strFulCat As String() = FulCat.Split("\"c)

        If strFulCat.Length < 2 Then
            errProv.SetError(ZTCategory.TextBox, "SubCategory not selected")
        End If

        Dim FulLoc As String = ZTLocation.SelectedText
        Dim strFulLoc As String() = FulLoc.Split("\"c)
        If strFulLoc.Length < 4 Then
            errProv.SetError(ZTLocation.TextBox, "Room not selected")
        End If
        Return Not errProv.HasErrors
    End Function

    Private Function ValidateDisposal() As Boolean
        If chkDisposed.Checked Then
            If cmbDisposalMethod.SelectedText = String.Empty Then
                errProv.SetError(cmbDisposalMethod.TextBox, "Please select Disposal Method first!")
            Else
                errProv.ClearErrors()
            End If
        Else
            errProv.ClearErrors()
        End If
        Return Not errProv.HasErrors
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CheckDemo()


        If valProvMain.Validate And ValidateCatLoc() Then
            If ValidateDisposal() Then

                Try
                    Dim objattAssets As New attItems
                    Dim objBALAssets As New BALItems
                    Dim AssetItemID As String = ""

                    objattAssets.AstCatID = ZTCategory.SelectedValue
                    objattAssets.AstDesc = txtAstDesc.Text.Trim
                    Dim dt As DataTable = objBALAssets.CheckItemDescription(objattAssets)
                    If dt.Rows.Count > 0 Then
                        AssetItemID = dt.Rows(0)(0)
                    Else
                        AssetItemID = objBALAssets.GetNextPKey_Item()
                        objattAssets.PKeyCode = AssetItemID
                        objBALAssets.Insert_Item(objattAssets)
                    End If


                    Dim objattAssetDetails As New attAssetDetails
                    Dim objBALAssetDetails As New BALAssetDetails

                    objattAssetDetails.BaseCost = 0
                    objattAssetDetails.CustodianID = "1"
                    objattAssetDetails.InsID = 0
                    objattAssetDetails.SuppID = ""
                    objattAssetDetails.POCode = "0"
                    objattAssetDetails.SerailNo = txtSerialNo.Text.Trim
                    objattAssetDetails.Discount = 0
                    objattAssetDetails.InvNumber = ""
                    objattAssetDetails.ItemCode = AssetItemID
                    objattAssetDetails.Tax = 0
                    objattAssetDetails.CompanyID = 1

                    objattAssetDetails.DispDate = Date.MinValue
                    objattAssetDetails.PurDate = txtCreateDate.Text
                    objattAssetDetails.SrvDate = Now.Date

                    objattAssetDetails.AstNum = Generate_AssetNumber(objattAssetDetails.CompanyID)
                    objattAssetDetails.RefNo = objattAssetDetails.AstNum
                    objattAssetDetails.AstBrandID = 1
                    objattAssetDetails.AstDesc = txtAstDesc.Text.Trim
                    objattAssetDetails.AstDesc2 = ""
                    objattAssetDetails.AstModel = txtAstModel.Text.Trim
                    objattAssetDetails.Capex = ""
                    objattAssetDetails.PoErp = ""
                    objattAssetDetails.Plate = ""
                    objattAssetDetails.GRN = ""
                    objattAssetDetails.RefCode = ""
                    objattAssetDetails.GLCode = 1
                    objattAssetDetails.PONumber = ""
                    objattAssetDetails.LocID = ZTLocation.SelectedValue
                    objattAssetDetails.NoPiece = 1
                    objattAssetDetails.Disposed = False
                    objattAssetDetails.IsDataChanged = True
                    objattAssetDetails.IsSold = 0
                    objattAssetDetails.PKeyCode = AssetsID.SelectedText
                    objattAssetDetails.BarCode = AssetsID.SelectedText

                    objattAssetDetails.IsDataChanged = True
                    objattAssetDetails.BussinessArea = ""
                    objattAssetDetails.InventoryNumber = ""
                    objattAssetDetails.CostCenterID = ""
                    objattAssetDetails.InStockAsset = True
                    objattAssetDetails.EvaluationGroup1 = ""
                    objattAssetDetails.EvaluationGroup2 = ""
                    objattAssetDetails.EvaluationGroup3 = ""
                    objattAssetDetails.EvaluationGroup4 = ""

                    If chkDisposed.Checked Then
                        If cmbDisposalMethod.SelectedText <> "" Then
                            objattAssetDetails.DispCode = cmbDisposalMethod.SelectedValue
                            If Trim(dtDispdate.Text) = "" Then
                                objattAssetDetails.DispDate = Nothing
                            Else
                                objattAssetDetails.DispDate = dtDispdate.Value
                            End If

                            objattAssetDetails.Disposed = True
                            objBALAssetDetails.DisposeAsset_Book(AssetsID.SelectedText)
                        End If
                    End If


                    If isEdit Then
                        Dim objattAsset As New attAssetDetails
                        objattAsset.PKeyCode = AssetsID.SelectedText
                        'Get the old transRemarks, we don't want to overwrite it when update.
                        Dim ds As DataTable = objBALAssetDetails.GetAll_AssetDetails(objattAsset)
                        Dim TransRemarks As String = ds.Rows(0)("TransRemarks")
                        objattAssetDetails.TransRemarks = TransRemarks
                        objattAssetDetails.LastEditBY = AppConfig.LoginName
                        objattAssetDetails.LastEditDate = Now
                        objBALAssetDetails.Update_AssetDetails(objattAssetDetails)
                    Else
                        'By Default the status is found if new records created, according to customer requirements.
                        objattAssetDetails.TransRemarks = "ZulAssets"
                        objattAssetDetails.InvSchCode = 1
                        objattAssetDetails.InvStatus = 1

                        objattAssetDetails.CreatedBY = AppConfig.LoginName
                        objattAssetDetails.LastEditBY = AppConfig.LoginName
                        objattAssetDetails.LastEditDate = Now
                        objattAssetDetails.CreationDate = Now
                        objattAssetDetails.StatusID = 1
                        If objBALAssetDetails.Insert_AssetDetails(objattAssetDetails, True) Then
                            Dim dsDepPolicy As DataTable = objBALAssets.Get_DepPolicy(objattAssetDetails.ItemCode)
                            objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, dsDepPolicy.Rows(0)("SalvageValue"), dsDepPolicy.Rows(0)("SalvageYear"), dsDepPolicy.Rows(0)("SalvageMonth"), objattAssetDetails.SrvDate, dsDepPolicy.Rows(0)("IsSalvageValuePercent"))
                        End If
                    End If
                    isEdit = True
                    If AnonymousId = "" Then
                        Me.Search_Asset(AssetsID.SelectedText)
                    Else 'FormOpened from Anonymous in Data Processing
                        'set this flag to delete the asset from Anonymous data after closing the form.
                        AnonymousSaved = True
                        Dim objattAstHistory As attAstHistory
                        objattAstHistory = New attAstHistory()
                        Dim objBALAst_History As New BALAst_History
                        objattAstHistory.AstID = AssetsID.SelectedText
                        objattAstHistory.Status = 5 'Means Anonymous
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
                        objBALAssetDetails.Update_InvStatus(objattAssetDetails)
                        'update the history grid to show the records
                        Get_Ast_History(AssetsID.SelectedText)
                        'update the status to show it.
                        ShowStatus(5)
                        AnonymousId = ""
                    End If

                    MessageBox.Show("Record saved successfully", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim objattAssetDetails As attAssetDetails
        Dim objBALAssetDetails As New BALAssetDetails

        If objBALAssetDetails.Check_AstID(AssetsID.SelectedText, True) Then
            If MessageBox.Show("Deleting this Asset will delete all its transfer history , Do you want to continue?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                objattAssetDetails = New attAssetDetails
                objattAssetDetails.PKeyCode = AssetsID.SelectedText
                If objBALAssetDetails.Delete_Details(objattAssetDetails) Then
                    '
                    MessageBox.Show("Record deleted successfully", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    btnNewAsset_Click(sender, e)
                End If
            End If
        End If
    End Sub

    Private Function GenerateAssetID(ByVal comID As String) As String
        Dim Num As String = Generate_AssetNumber(comID)
        Dim AssetID As String = Num.PadLeft(13, "0")
        While objBALAssetDetails.Check_AstID(AssetID, True)
            Dim objBALCompany As New BALCompany
            objBALCompany.SetCompanyLastAssetNumber(comID, Num)
            Num = Generate_AssetNumber(comID)
            AssetID = Num.PadLeft(13, "0")
        End While
        Return AssetID
    End Function

    Private Sub btnNewAsset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewAsset.Click
        Try
            errProv.ClearErrors()
            txtSerialNo.Text = ""
            txtBarCode.Text = ""
            txtUpdateDate.Text = Now.Date
            txtCreateDate.Text = Now.Date
            ZTLocation.SelectedText = ""
            ZTLocation.SelectedValue = ""
            ZTCategory.SelectedValue = ""
            ZTCategory.SelectedText = ""
            txtAstDesc.Text = ""
            txtAstModel.Text = ""
            txtCreatedIn.Text = ""
            AssetsID.Tag = "NewID"
            chkDisposed.Checked = False
            lblDisposalDate.Visible = False
            lblDisposalMethod.Visible = False
            cmbDisposalMethod.Visible = False
            dtDispdate.Visible = False

            cmbDisposalMethod.Text = String.Empty
            AssetsID.SelectedText = GenerateAssetID(1) '1 company ID default company
            'AssetsID.SelectedText = objBALAssetDetails.Generate_AssetID()
            btnDelete.Visible = False
            btnMarkAsLost.Visible = False
            If grdAstHist.DataSource IsNot Nothing Then
                CType(grdAstHist.DataSource, DataTable).Rows.Clear()
            End If
            isEdit = False
            valProvMain.RemoveControlError(AssetsID.TextBox)
            valProvMain.RemoveControlError(ZTLocation.TextBox)
            valProvMain.RemoveControlError(ZTCategory.TextBox)
            pnlStatus.Visible = False
            ZTCategory.TextBox.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnNewAsset1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewAsset1.Click
        btnNewAsset.PerformClick()
    End Sub

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        Dim FirstAstID As String = ""
        objattAssetDetails = New attAssetDetails
        FirstAstID = objBALAssetDetails.Get_AssetsDetails_First_Last(objattAssetDetails, 1)
        If FirstAstID <> "" Then
            AssetsID.SelectedText = FirstAstID
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
            MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Sub btnPre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPre.Click
        Dim PreAstId As String = ""
        PreAstId = Get_AssetsDetails_Pre_Next(txtAstNum.Text, "", 1)

        If PreAstId <> "" Then
            AssetsID.SelectedText = PreAstId
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim NextAstId As String = ""
        NextAstId = Get_AssetsDetails_Pre_Next(txtAstNum.Text, "", 2)

        If NextAstId <> "" Then
            AssetsID.SelectedText = NextAstId
        End If

    End Sub

    Private Sub btnLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLast.Click
        Dim LastAstID As String = ""
        objattAssetDetails = New attAssetDetails
        LastAstID = objBALAssetDetails.Get_AssetsDetails_First_Last(objattAssetDetails, 2)
        If LastAstID <> "" Then
            AssetsID.SelectedText = LastAstID
        End If
    End Sub

    Private Sub ZTLocation_TreeBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZTLocation.TreeBtnClick
        Dim ds As New DataTable
        Dim objBALLocation As New BALLocation
        ZTLocation.DataSource = objBALLocation.GetComboLocations(New attLocation)
        ZTLocation.OpenLOV()
    End Sub

    Private Sub ZTCategory_TreeBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZTCategory.TreeBtnClick
        Dim ds As New DataTable
        Dim objBALCategory As New BALCategory
        ZTCategory.DataSource = objBALCategory.GetAll_Category(New attCategory)
        ZTCategory.OpenLOV()
    End Sub

    Private Sub frmAssets_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG
        Me.BackgroundImageLayout = ImageLayout.Stretch
        valRulenotEmpty.ConditionOperator = ConditionOperator.IsNotBlank
        valRulenotEmpty.ErrorText = "Please enter a value"
        valRulenotEmpty.ErrorType = ErrorType.Critical
        valProvMain.SetValidationRule(AssetsID.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(txtAstDesc, valRulenotEmpty)
        valProvMain.SetValidationRule(ZTLocation.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(ZTCategory.TextBox, valRulenotEmpty)
        If AnonymousId = "" Then
            btnNewAsset_Click(sender, e)
        End If
    End Sub

    Public Sub LocateAsset(ByVal astID As String)
        If astID <> "" Then
            AssetsID.SelectedText = astID
        End If
    End Sub

    Public Sub ShowAnonymousInfo(ByVal AnonymousId As String, ByVal Desc As String, ByVal LocID As String, ByVal CatID As String, ByVal Modle As String, ByVal serial As String, ByVal DeviceID As String, ByVal TransDate As DateTime, ByVal InvID As Integer)
        AssetsID.Tag = "NewID"
        AssetsID.SelectedText = GenerateAssetID(1)
        'AssetsID.SelectedText = objBALAssetDetails.Generate_AssetID()
        ZTLocation.SelectedValue = LocID
        ZTLocation.SelectedText = objBALLocation.Comp_Path(LocID)
        ZTCategory.SelectedValue = CatID
        ZTCategory.SelectedText = objBALCategory.Comp_Path(CatID)
        txtAstDesc.Text = Desc
        txtAstModel.Text = Modle
        txtSerialNo.Text = serial
        txtUpdateDate.Text = Now.Date
        txtCreateDate.Text = Now.Date
        txtCreatedIn.Text = "ZulAssets"

        Me.AnonymousId = AnonymousId
        Me.DeviceID = DeviceID
        Me.TransDate = TransDate
        Me.InvScheduleId = InvID
    End Sub

    Private Sub AssetsID_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssetsID.LovBtnClick
        Try
            AssetsID.ValueMember = "AstId"
            AssetsID.DisplayMember = "AstId"
            Dim objBALAssetDetails As BALAssetDetails = New BALAssetDetails
            AssetsID.DataSource = objBALAssetDetails.GetAssetData_List()
            AssetsID.OpenLOV()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AssetsID_SelectTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssetsID.SelectTextChanged

        If AssetsID.Tag IsNot Nothing Then
            If AssetsID.Tag.ToString = "NewID" Then
                AssetsID.Tag = ""
            Else
                If AssetsID.SelectedText <> "" Then
                    Me.Search_Asset(AssetsID.SelectedText)
                End If
            End If
        End If
    End Sub

    Private Sub CheckDemo()
        If IsDemokey Then
            'if asset details recrod count more than 10 then close the application

            Dim obj As New BALAssetDetails
            Dim leftcount As Integer = DemoAssetsCount - obj.GetAssetsCount(New attAssetDetails)
            lblEvaluation.Visible = True
            lblEvaluation.Text = leftcount & " records left"
            If obj.GetAssetsCount(New attAssetDetails) > DemoAssetsCount Then
                MessageBox.Show("Evaluation, Assets Count exceed 10, Application will close now.", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Application.Exit()
            End If
        End If
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
            MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function

    Private Sub ShowStatus(ByVal Status As Integer)
        pnlStatus.Visible = True
        Select Case Status
            Case 0
                lblStatus.Text = "Missing"
                imgStatus.Image = My.Resources.Invalid
                lblStatus.ForeColor = Color.Red
            Case 1
                lblStatus.Text = "Found"
                imgStatus.Image = My.Resources.Valid
                lblStatus.ForeColor = Color.Green
            Case 2
                lblStatus.Text = "Misplaced"
                imgStatus.Image = My.Resources.Miplaced
                lblStatus.ForeColor = Color.Black
            Case 3
                lblStatus.Text = "Transfer"
                imgStatus.Image = My.Resources.Transfered
                lblStatus.ForeColor = Color.Black
            Case 4
                lblStatus.Text = "Allocated"
                imgStatus.Image = My.Resources.check
                lblStatus.ForeColor = Color.Blue
            Case 5
                lblStatus.Text = "Anonymous"
                imgStatus.Image = My.Resources.Attention
                lblStatus.ForeColor = Color.Black
            Case 6
                lblStatus.Text = "Lost"
                imgStatus.Image = My.Resources.Attention
                lblStatus.ForeColor = Color.Red
            Case 7
                lblStatus.Text = "Disposed"
                imgStatus.Image = My.Resources.Refresh16x16
                lblStatus.ForeColor = Color.Black
        End Select
    End Sub


    Private Sub Search_Asset(ByVal strAstID As String)
        Try
            errProv.ClearErrors()
            CheckDemo()
            'Remove Anonymous form Data to don't allow saving Anonymous Asset on saving
            AnonymousId = ""
            DeviceID = ""
            InvScheduleId = ""

            strAstID = strAstID
            Dim dt As New DataTable
            If strAstID <> "" Then

                dt = Get_AssetsDetails_byID(strAstID, "")
                If dt Is Nothing Then
                    btnNewAsset_Click(Nothing, Nothing)
                Else
                    isEdit = True
                    ShowStatus(dt.Rows(0)("InvStatus").ToString())

                    txtSerialNo.Text = dt.Rows(0)("SerailNo").ToString()
                    txtAstDesc.Text = dt.Rows(0)("AstDesc").ToString()
                    txtAstModel.Text = dt.Rows(0)("AstModel").ToString()
                    txtBarCode.Text = dt.Rows(0)("BarCode").ToString()
                    txtUpdateDate.Text = CDate(dt.Rows(0)("SrvDate").ToString)
                    txtCreateDate.Text = CDate(dt.Rows(0)("PurDate").ToString)
                    txtAstNum.Text = dt.Rows(0)("AstNum").ToString()

                    If dt.Rows(0)("TransRemarks").ToString() = "Oracle System" Then
                        txtCreatedIn.Text = "Oracle System"
                    ElseIf dt.Rows(0)("TransRemarks").ToString() = "ZulAssets" Then
                        txtCreatedIn.Text = "ZulAssets"
                    Else
                        txtCreatedIn.Text = "Unknown"
                    End If


                    If (dt.Rows(0)("LocId").ToString <> "") Then
                        ZTLocation.SetValue(dt.Rows(0)("LocId").ToString(), objBALLocation.Comp_Path(dt.Rows(0)("LocId").ToString()))
                    Else
                        ZTLocation.SelectedText = ""
                        ZTLocation.SelectedValue = ""
                    End If
                    If Not (dt.Rows(0)("AstCatID") Is Nothing) Then
                        ZTCategory.SetValue(dt.Rows(0)("AstCatID").ToString(), objBALCategory.Comp_Path(dt.Rows(0)("AstCatID").ToString()))
                    End If

                    Get_Ast_History(AssetsID.SelectedText)

                    'If Item is disposed
                    If CType(dt.Rows(0)("Disposed"), Boolean) Then
                        chkDisposed.Checked = True
                        cmbDisposalMethod.FindRow(dt.Rows(0)("Dispcode").ToString(), dt.Rows(0)("DispDesc").ToString())
                        If dt.Rows(0)("DispDate").ToString() <> "" Then dtDispdate.Value = CDate(dt.Rows(0)("DispDate"))
                        ShowStatus(7)
                        btnMarkAsLost.Visible = False
                    Else
                        btnMarkAsLost.Visible = True
                        chkDisposed.Checked = False
                        cmbDisposalMethod.Text = ""
                    End If

                    btnDelete.Visible = True
                    If dt.Rows(0)("InvStatus").ToString() = 6 Then 'Lost
                        btnMarkAsLost.Text = "&Mark As Found"
                    Else
                        btnMarkAsLost.Text = "&Mark As Lost"
                    End If
                End If
                valProvMain.Validate()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub format_AstHistGrid()
        Dim RIAstHist As New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
        RIAstHist.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() _
                                                    {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Missing", "0", -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Found", "1", -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Misplaced", "2", -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Transfer", "3", -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Allocated", "4", -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Anonymous", "5", -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Lost", "6", -1)})
        With grdAstHistView
            .Columns(0).Caption = "History ID"
            .Columns(0).Width = 100
            .Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            .Columns(0).Visible = False
            .Columns(1).Caption = "Inventory Schedule"
            .Columns(1).Width = 130
            .Columns(2).Caption = "Asset ID"
            .Columns(2).Width = 100
            .Columns(2).Visible = False
            .Columns(3).Caption = "From Location"
            .Columns(3).Width = 120
            .Columns(4).Caption = "To Location"
            .Columns(4).Width = 120
            .Columns(5).Caption = "History Date"
            .Columns(5).Width = 120
            .Columns(6).Caption = "Status"
            .Columns(6).Width = 120
            .Columns(6).ColumnEdit = RIAstHist
            .Columns(6).VisibleIndex = .Columns.Count - 1

            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .Columns(9).Visible = False
            .Columns("DeptName").VisibleIndex = 4
            .Columns("Remarks").VisibleIndex = 5
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
    End Sub

    Private Sub btnMarkAsLost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMarkAsLost.Click
        If btnMarkAsLost.Text = "&Mark As Lost" Then
            Dim frm As New frmLost
            frm.AssetID = AssetsID.SelectedText
            frm.LocID = ZTLocation.SelectedValue
            frm.ShowDialog()
            frm.Dispose()
            Me.Search_Asset(AssetsID.SelectedText)
        Else
            If MessageBox.Show("Are you sure to mark this asset as found?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim objattAstHistory As attAstHistory
                objattAstHistory = New attAstHistory()
                Dim objBALAst_History As New BALAst_History
                objattAstHistory.AstID = AssetsID.SelectedText
                objattAstHistory.Status = 1 'Means Found
                objattAstHistory.InvSchCode = 1 'system transfer
                objattAstHistory.HisDate = Now.Date
                objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                objattAstHistory.Fr_loc = ZTLocation.SelectedValue
                objattAstHistory.To_Loc = ZTLocation.SelectedValue
                objBALAst_History.Insert_Ast_History(objattAstHistory)

                Dim objattAssetDetails As New attAssetDetails
                Dim objBALAssetDetails As New BALAssetDetails
                objattAssetDetails.PKeyCode = AssetsID.SelectedText
                objattAssetDetails.InvStatus = 1 'Means Found
                objattAssetDetails.InvSchCode = 1 'system transfer
                objattAssetDetails.LocID = ZTLocation.SelectedValue
                objBALAssetDetails.Update_InvStatus(objattAssetDetails)
                Me.Search_Asset(AssetsID.SelectedText)
            End If
            'add history and mark as found.
        End If
    End Sub

    Private Sub chkDisposed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDisposed.CheckedChanged
        lblDisposalDate.Visible = chkDisposed.Checked
        lblDisposalMethod.Visible = chkDisposed.Checked
        cmbDisposalMethod.Visible = chkDisposed.Checked
        dtDispdate.Visible = chkDisposed.Checked
        dtDispdate.Value = Now.Date
    End Sub

    Private Sub cmbDisposalMethod_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDisposalMethod.LovBtnClick
        Try
            cmbDisposalMethod.ValueMember = "DispCode"
            cmbDisposalMethod.DisplayMember = "DispDesc"
            Dim objBALDisposalMethod As New BALDisposalMethod
            cmbDisposalMethod.DataSource = objBALDisposalMethod.GetAll_DisposalMethod(New attDisposalMethod)
            cmbDisposalMethod.OpenLOV()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class