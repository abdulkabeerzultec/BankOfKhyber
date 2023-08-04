Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports TaskScheduler
Imports System.Drawing.Printing
Imports System.IO
Imports System.Data.OleDb

Public Class frmSysConfig
    Dim objattDepreciationConfig As attDepreciationConfig
    Dim objBALDepreciationConfig As New BALDepreciationConfig
    Dim objBAlAppConf As New BALAppConfig
    Dim isEdit As Boolean = False
    Dim glDepRunType As Int16

    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    Private Sub Load_ReportPrinters()
        Dim strReportPrinter As String = ""
        strReportPrinter = AppConfig.ReportPrinter

        For Each strPrinter As String In PrinterSettings.InstalledPrinters
            cmbRptPrint.Items.Add(strPrinter)
        Next strPrinter

        If cmbRptPrint.Items.IndexOf(strReportPrinter) <> -1 Then
            cmbRptPrint.SelectedIndex = cmbRptPrint.Items.IndexOf(strReportPrinter)
        Else
            strReportPrinter = (New PrinterSettings).PrinterName ' get the default printer
            cmbRptPrint.SelectedIndex = cmbRptPrint.Items.IndexOf(strReportPrinter)
        End If
    End Sub

    Private Sub Load_LabelPrinters()
        Dim strLabelPrinter As String = ""
        strLabelPrinter = AppConfig.LabelPrinter

        For Each strPrinter As String In PrinterSettings.InstalledPrinters
            cmbRptLabel.Items.Add(strPrinter)
        Next strPrinter

        If cmbRptLabel.Items.IndexOf(strLabelPrinter) <> -1 Then
            cmbRptLabel.SelectedIndex = cmbRptLabel.Items.IndexOf(strLabelPrinter)
        Else
            strLabelPrinter = (New PrinterSettings).PrinterName ' get the default printer
            cmbRptLabel.SelectedIndex = cmbRptLabel.Items.IndexOf(strLabelPrinter)
        End If
    End Sub


    Private Sub frmSysConfig_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        objBAlAppConf.Get_SysSettings()
        FormController.objfrmSysConfig = Nothing
    End Sub


    Private Sub frmSysConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Try
            Load_ReportPrinters()
            Load_LabelPrinters()

            Me.WindowState = FormWindowState.Normal
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Fixed3D
            Me.MaximizeBox = False

            Try
                If Decimal.Parse(AppConfig.DepDate) Then
                    txtMonthDay.Value = AppConfig.DepDate
                End If
            Catch
                txtMonthDay.Value = 1
            End Try

            If AppConfig.DepRunType = "2" Then
                RdAuto.Checked = True
                glDepRunType = "2"
            Else
                glDepRunType = "1"
                rdManual.Checked = True
            End If

            If AppConfig.DeletePermt = "2" Then
                chkDelete.Checked = False
            Else
                chkDelete.Checked = True
            End If

            chkExportServ.Checked = AppConfig.ExportToServer

            rdoAssetsCoding.Checked = AppConfig.CodingMode
            rdoIncrementalCoding.Checked = Not AppConfig.CodingMode
            cmbDateFormat.Text = AppConfig.MaindateFormat

            If AppConfig.DescForRpt = "Item Description" Then
                rdoItmDesc.Checked = True
            ElseIf AppConfig.DescForRpt = "Asset Description 1" Then
                rdoAssDesc1.Checked = True
            ElseIf AppConfig.DescForRpt = "Asset Description 2" Then
                rdoAssDesc2.Checked = True
            Else
                rdoItmDesc.Checked = True
            End If

            If AppConfig.ImgStorgeLoc = "Database" Then
                rdoDatabase.Checked = True
            ElseIf AppConfig.ImgStorgeLoc = "Shared Folder" Then
                rdoSharedFolder.Checked = True
                txtImgStorgePath.Text = AppConfig.ImgPath
            Else
                rdoDatabase.Checked = True
            End If

            If AppConfig.ImgType = "Asset Images" Then
                rdoAstImg.Checked = True
            ElseIf AppConfig.ImgType = "Item Images" Then
                rdoItmImg.Checked = True
            Else
                rdoItmImg.Checked = True
            End If
            chkShowAlarmOnStartup.Checked = AppConfig.ShowAlarmOnStartup
            txtWarrantyAlarmDays.Value = AppConfig.AlarmBeforeDays

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub



    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If txtImgStorgePath.Text = "" And rdoSharedFolder.Checked Then
                errProv.SetError(txtImgStorgePath, "Please select shared folder path")
                TabControl1.SelectedTab = tabAssetImages
                btnDir.Focus()
                Exit Sub
            Else
                errProv.ClearErrors()
            End If

            Dim sched As New Scheduler
            Dim intDelPrmt As Int16
            If chkDelete.Checked Then
                intDelPrmt = 1
            Else
                intDelPrmt = 2
            End If

            Dim DepRunType As Int16
            If rdManual.Checked = True Then
                DepRunType = 1
            End If
            If RdAuto.Checked = True Then
                DepRunType = 2
            End If
            If glDepRunType <> DepRunType Then
                If DepRunType = 1 Then
                    If ZulMessageBox.ShowMe("DepRuntoMan", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    Else
                        sched.Tasks.Delete("ZulTecSchedule")
                    End If


                ElseIf DepRunType = 2 Then
                    If ZulMessageBox.ShowMe("DepRuntoAuto", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    Else
                        If CreateTaskScheduler(txtMonthDay.Value) = False Then
                            Exit Sub
                        End If
                    End If
                End If
            Else
                If glDepRunType = DepRunType And DepRunType = 2 Then
                    If CreateTaskScheduler(txtMonthDay.Value) = False Then
                        Exit Sub
                    End If
                End If
            End If
            Dim DescForRpt As String = ""
            Dim ImgStorgeLoc As String = ""
            Dim ImgPath As String = ""
            Dim ImgType As String = ""

            If rdoItmDesc.Checked Then
                DescForRpt = "Item Description"
            ElseIf rdoAssDesc1.Checked Then
                DescForRpt = "Asset Description 1"
            ElseIf rdoAssDesc2.Checked Then
                DescForRpt = "Asset Description 2"
            End If

            If rdoDatabase.Checked Then
                ImgStorgeLoc = "Database"
            ElseIf rdoSharedFolder.Checked Then
                ImgStorgeLoc = "Shared Folder"
                ImgPath = txtImgStorgePath.Text.Trim
            End If

            If rdoAstImg.Checked Then
                ImgType = "Asset Images"
            ElseIf rdoItmImg.Checked Then
                ImgType = "Item Images"
            End If


            objBAlAppConf.Set_SysSettings(txtMonthDay.Value, DepRunType, intDelPrmt, cmbRptPrint.SelectedItem, cmbRptLabel.SelectedItem, chkExportServ.Checked, rdoAssetsCoding.Checked, cmbDateFormat.Text, DescForRpt, ImgStorgeLoc, ImgType, ImgPath, chkShowAlarmOnStartup.Checked, txtWarrantyAlarmDays.Value)
            ZulMessageBox.ShowMe("Settingupdated")
            glDepRunType = DepRunType
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmSysConfig = Nothing
            Me.Dispose()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub rdManual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdManual.CheckedChanged, RdAuto.CheckedChanged
        Label1.Enabled = RdAuto.Checked
        txtMonthDay.Enabled = RdAuto.Checked
        Label5.Enabled = RdAuto.Checked
    End Sub

    Private Function CreateTaskScheduler(ByVal DayInMonth As String) As Boolean
        Dim domainUsername As String = System.Windows.Forms.SystemInformation.UserDomainName & "\" & _
                   System.Windows.Forms.SystemInformation.UserName
        Dim usrPass As String = ""
        usrPass = psDecrypt(AppConfig.SysPass)
        Dim sched As New Scheduler

        Dim t2 As Task
        Try
            Try
                sched.Tasks.Delete("ZulTecSchedule")
            Catch ex As Exception
            End Try

            t2 = sched.Tasks.NewTask("ZulTecSchedule")
            t2.ApplicationName = Application.StartupPath & "\ZulAssetDepreciation.exe"
            t2.Comment = "ZulAssets Depreciation Agent"
            t2.Creator = "Zultec - ZulAssets"
            t2.Flags = TaskFlags.Interactive
            t2.IdleWaitDeadlineMinutes = 20
            t2.IdleWaitMinutes = 10
            t2.MaxRunTime = New TimeSpan(5, 0, 0)
            t2.Priority = System.Diagnostics.ProcessPriorityClass.High
            t2.WorkingDirectory = "c:\"

            t2.Triggers.Add(New MonthlyTrigger(3, 0, New Integer() {DayInMonth}))
            If Not String.IsNullOrEmpty(usrPass) Then
                t2.SetAccountInformation(domainUsername, usrPass)
            Else
                t2.Flags = TaskFlags.RunOnlyIfLoggedOn
                t2.SetAccountInformation(domainUsername, Nothing)
            End If
            t2.Save()


        Catch ex As Exception
            Try
                sched.Tasks.Delete("ZulTecSchedule")
            Catch ex1 As Exception
            End Try
            If ex.Message.StartsWith("Access is denied") Then
                MsgBox("Incorrect System password, Reenter Password")
            End If
            Return False
        Finally
            t2 = Nothing
        End Try
        t2 = Nothing
        Return True
    End Function

    Private Sub rdoSharedFolder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoSharedFolder.CheckedChanged, rdoDatabase.CheckedChanged
        btnDir.Enabled = rdoSharedFolder.Checked
        txtImgStorgePath.Enabled = rdoSharedFolder.Checked
    End Sub

    Private Sub btnDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDir.Click
        dlgSelectFolder.Description = "Choose a shared folder to store images"
        If dlgSelectFolder.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtImgStorgePath.Text = dlgSelectFolder.SelectedPath
        Else
            txtImgStorgePath.Text = ""
        End If
    End Sub

    Private Sub rdoAstImg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoAstImg.CheckedChanged, rdoItmImg.CheckedChanged
        btnExportAstImg.Enabled = rdoAstImg.Checked
        btnImportAstImg.Enabled = rdoAstImg.Checked
        btnClearAstImg.Enabled = rdoAstImg.Checked
        btnExportItmImg.Enabled = Not rdoAstImg.Checked
        btnImportItmImg.Enabled = Not rdoAstImg.Checked
        btnClearItmImg.Enabled = Not rdoAstImg.Checked
    End Sub

    Private Sub btnImportAstImg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportAstImg.Click
        Try
            dlgSelectFolder.Description = "Choose a folder to import Assets Images"
            If dlgSelectFolder.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim objattAssetDetails As attAssetDetails
                Dim objBALAssetDetails As New BALAssetDetails
                Dim di As New DirectoryInfo(dlgSelectFolder.SelectedPath)
                Dim aryFi As FileInfo() = di.GetFiles("*.jpg")
                pb.Visible = True
                pb.Step = 1
                pb.Value = 0
                pb.Maximum = aryFi.Length
                For Each f As FileInfo In aryFi
                    objattAssetDetails = New attAssetDetails
                    objattAssetDetails.PKeyCode = Path.GetFileNameWithoutExtension(f.Name)
                    Dim fs As New FileStream(f.FullName, FileMode.Open)
                    Dim Data() As Byte = New [Byte](fs.Length) {}
                    fs.Read(Data, 0, fs.Length)
                    objattAssetDetails.Image = Data
                    objBALAssetDetails.UpdateAssetImage(objattAssetDetails)
                    pb.PerformStep()
                    Application.DoEvents()
                Next
                pb.Visible = False
                ZulMessageBox.ShowMe("ImportAstImgSucc")
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally
            pb.Visible = False
        End Try
    End Sub

    Private Sub btnImportItmImg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportItmImg.Click
        Try
            dlgSelectFolder.Description = "Choose a folder to import Items Images"
            If dlgSelectFolder.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim objattAssets As attItems
                Dim objBALAssets As New BALItems
                Dim di As New DirectoryInfo(dlgSelectFolder.SelectedPath)
                Dim aryFi As FileInfo() = di.GetFiles("*.jpg")
                pb.Visible = True
                pb.Step = 1
                pb.Value = 0
                pb.Maximum = aryFi.Length
                For Each f As FileInfo In aryFi
                    objattAssets = New attItems
                    objattAssets.PKeyCode = Path.GetFileNameWithoutExtension(f.Name)
                    Dim fs As New FileStream(f.FullName, FileMode.Open)
                    Dim Data() As Byte = New [Byte](fs.Length) {}
                    fs.Read(Data, 0, fs.Length)
                    objattAssets.Image = Data
                    objBALAssets.UpdateItemImage(objattAssets)
                    pb.PerformStep()
                    Application.DoEvents()
                Next
                pb.Visible = False
                ZulMessageBox.ShowMe("ImportItmImgSucc")
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally
            pb.Visible = False
        End Try
    End Sub

    Private Sub btnExportItmImg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportItmImg.Click
        Try
            dlgSelectFolder.Description = "Choose a folder to export Items Images"
            If dlgSelectFolder.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim objBALAssets As New BALItems
                Dim dt As DataTable = objBALAssets.GetAllItemImage
                pb.Visible = True
                pb.Step = 1
                pb.Value = 0
                pb.Maximum = dt.Rows.Count
                For Each row As DataRow In dt.Rows
                    Dim bits As Byte() = CType(row("ItmImage"), Byte())
                    Dim ms As New MemoryStream(bits, 0, bits.Length)
                    Image.FromStream(ms, True).Save(dlgSelectFolder.SelectedPath & "\" & row("ItemCode").ToString() & ".jpg")
                    pb.PerformStep()
                    Application.DoEvents()
                Next
                pb.Visible = False
                ZulMessageBox.ShowMe("ExportItmImgSucc")
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally
            pb.Visible = False
        End Try
    End Sub

    Private Sub btnExportAstImg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportAstImg.Click
        Try
            dlgSelectFolder.Description = "Choose a folder to export Assets Images"
            If dlgSelectFolder.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim objBALAssetsDetails As New BALAssetDetails
                Dim dt As DataTable = objBALAssetsDetails.GetAllAssetImages
                pb.Visible = True
                pb.Step = 1
                pb.Value = 0
                pb.Maximum = dt.Rows.Count
                For Each row As DataRow In dt.Rows
                    Dim bits As Byte() = CType(row("astImage"), Byte())
                    Dim ms As New MemoryStream(bits, 0, bits.Length)
                    Image.FromStream(ms, True).Save(dlgSelectFolder.SelectedPath & "\" & row("astid").ToString() & ".jpg")
                    pb.PerformStep()
                    Application.DoEvents()
                Next
                pb.Visible = False
                ZulMessageBox.ShowMe("ExportAstImgSucc")
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally
            pb.Visible = False
        End Try
    End Sub

    Private Sub btnClearAstImg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAstImg.Click
        Try
            Dim rslt As DialogResult = MessageBox.Show("This process will delete all the images from the database" & Environment.NewLine & "Do you want to procced without exporting the images to a folder?", "ZulAssets", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3)
            If rslt = Windows.Forms.DialogResult.Yes Then
                Dim objBALAssetsDetails As New BALAssetDetails
                objBALAssetsDetails.ClearAllAssetImages()
                ZulMessageBox.ShowMe("ClearAstImgSucc")
            ElseIf rslt = Windows.Forms.DialogResult.No Then
                btnExportAstImg_Click(sender, e)
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally
            pb.Visible = False
        End Try
    End Sub

    Private Sub btnClearItmImg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearItmImg.Click
        Try
            Dim rslt As DialogResult = MessageBox.Show("This process will delete all the images from the database" & Environment.NewLine & "Do you want to procced without exporting the images to a folder?", "ZulAssets", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3)
            If rslt = Windows.Forms.DialogResult.Yes Then
                Dim objBALAssets As New BALItems
                objBALAssets.ClearAllItemImage()
                ZulMessageBox.ShowMe("ClearItmImgSucc")
            ElseIf rslt = Windows.Forms.DialogResult.No Then
                btnExportItmImg_Click(sender, e)
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally
            pb.Visible = False
        End Try

    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Dim objBALCategory As New BALCategory
        Dim objBALLocation As New BALLocation

        If Not chkRegenerateCat.Checked And Not chkRegenerateLoc.Checked Then
            ZulMessageBox.ShowMe("SelectOptionFirst")
            Exit Sub
        End If

        If chkRegenerateCat.Checked Then
            Dim dtCat As DataTable = objBALCategory.GetAll_Category(New attCategory)
            Dim objattCategory As attCategory
            For Each row As DataRow In dtCat.Rows
                objattCategory = New attCategory
                objattCategory.AstCatDesc = row("AstCatDesc")
                objattCategory.AstCatID = row("AstCatID")
                objattCategory.Code = row("Code")
                objattCategory.catLevel = row("catLevel")
                objBALCategory.Update_Category(objattCategory)
            Next

        End If

        If chkRegenerateLoc.Checked Then
            Dim dtLocations As DataTable = objBALLocation.GetAll_Locations(New attLocation)
            Dim objattLocation As attLocation

            For Each row As DataRow In dtLocations.Rows
                objattLocation = New attLocation
                objattLocation.Description = row("LocDesc")
                objattLocation.HierCode = row("LocID")
                objattLocation.Code = row("Code")
                objattLocation.locLevel = row("LocLevel")
                objattLocation.CompanyID = row("CompanyID")
                objBALLocation.Update_Location(objattLocation)
            Next
        End If
        ZulMessageBox.ShowMe("ProcessComplete")
    End Sub

    Private Sub btnImportAssetData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not String.IsNullOrEmpty(txtFilePath.Text) Then
            'Connect to the Excel Sheet and get the data
            Dim objattAssetDetails As attAssetDetails
            Dim objBALAssetDetails As New BALAssetDetails

            Dim ds As DataSet = ImportExcelXLSToDS(txtFilePath.Text, True)
            Dim dtAssets As DataTable = ds.Tables("Assets$")
            If dtAssets IsNot Nothing Then
                For Each row As DataRow In dtAssets.Rows
                    Dim ItemQty As Integer = row("Item Qty")
                    For i As Integer = 1 To ItemQty
                        Try
                            If Not objBALAssetDetails.Check_referenceID(row("Ref #"), "") Then

                                objattAssetDetails = New attAssetDetails
                                If String.IsNullOrEmpty(row("BaseCost").ToString) Then
                                    objattAssetDetails.BaseCost = 0
                                Else
                                    objattAssetDetails.BaseCost = row("BaseCost")
                                End If
                                objattAssetDetails.CustodianID = "01"
                                objattAssetDetails.InsID = 0
                                objattAssetDetails.SuppID = row("SupplierID").ToString
                                objattAssetDetails.POCode = 0

                                objattAssetDetails.SerailNo = row("SerialNo").ToString
                                objattAssetDetails.Discount = 0
                                objattAssetDetails.InvNumber = ""
                                objattAssetDetails.ItemCode = row("Item Code").ToString
                                objattAssetDetails.PurDate = row("ServiceDate").ToString
                                objattAssetDetails.Tax = 0
                                objattAssetDetails.TransRemarks = ""
                                objattAssetDetails.DispDate = Date.MinValue
                                If String.IsNullOrEmpty(row("CompanyID").ToString) Then
                                    objattAssetDetails.CompanyID = 1
                                Else
                                    objattAssetDetails.CompanyID = row("CompanyID").ToString
                                End If

                                objattAssetDetails.SrvDate = row("ServiceDate").ToString ' dtService.Value
                                objattAssetDetails.AstNum = Generate_AssetNumber(objattAssetDetails.CompanyID)
                                objattAssetDetails.RefNo = objattAssetDetails.AstNum
                                objattAssetDetails.AstBrandID = row("Brand ID").ToString
                                objattAssetDetails.AstDesc = row("Description").ToString
                                objattAssetDetails.AstDesc2 = row("Description 1").ToString
                                objattAssetDetails.AstModel = ""
                                objattAssetDetails.Capex = ""
                                objattAssetDetails.PoErp = ""
                                objattAssetDetails.Plate = ""
                                objattAssetDetails.GRN = ""
                                objattAssetDetails.RefCode = row("Ref #").ToString
                                objattAssetDetails.GLCode = row("GLCode").ToString
                                objattAssetDetails.PONumber = ""
                                Dim BALLocation As New BALLocation
                                Dim LocCode As String = row("Location").ToString
                                objattAssetDetails.LocID = BALLocation.GetLocID(LocCode)
                                objattAssetDetails.NoPiece = 0

                                objattAssetDetails.Disposed = "0"
                                objattAssetDetails.IsSold = 0
                                objattAssetDetails.PKeyCode = objBALAssetDetails.Generate_AssetID()

                                objattAssetDetails.BarCode = objattAssetDetails.AstNum
                                objattAssetDetails.IsDataChanged = False
                                objattAssetDetails.CreatedBY = AppConfig.LoginName
                                objattAssetDetails.LastEditBY = AppConfig.LoginName
                                objattAssetDetails.LastEditDate = Now.Date
                                objattAssetDetails.CreationDate = Now.Date
                                objattAssetDetails.StatusID = 1

                                If objBALAssetDetails.Insert_AssetDetails(objattAssetDetails, True) Then
                                    Dim objBALAssets As New BALItems
                                    Dim dsDepPolicy As DataTable = objBALAssets.Get_DepPolicy(objattAssetDetails.ItemCode)
                                    If Not dsDepPolicy Is Nothing Then
                                        If dsDepPolicy.Rows.Count > 0 Then
                                            objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, objattAssetDetails.BaseCost + objattAssetDetails.Tax, objattAssetDetails.CompanyID, dsDepPolicy.Rows(0)("SalvageValue"), dsDepPolicy.Rows(0)("SalvageYear"), dsDepPolicy.Rows(0)("SalvageMonth"), objattAssetDetails.SrvDate, dsDepPolicy.Rows(0)("IsSalvageValuePercent"))
                                        Else
                                            MessageBox.Show("Erorr while inserting AstBooks")
                                        End If
                                    Else
                                        MessageBox.Show("Erorr while inserting AstBooks")
                                    End If
                                End If
                            Else
                                ZulMessageBox.ShowMe("RefIDExist")
                            End If
                        Catch ex As Exception
                            GenericExceptionHandler(ex, WhoCalledMe)
                        End Try
                    Next
                Next
                MessageBox.Show("Import Completed Successfully.")
            End If
        End If
    End Sub

    Private Sub btnImportAssetDataQatar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportAssetData.Click
        If Not String.IsNullOrEmpty(txtFilePath.Text) Then
            'Connect to the Excel Sheet and get the data
            Dim objattAssetDetails As attAssetDetails
            Dim objBALAssetDetails As New BALAssetDetails

            Dim ds As DataSet = ImportExcelXLSToDS(txtFilePath.Text, True)
            Dim dtAssets As DataTable = ds.Tables("Assets$")
            If dtAssets IsNot Nothing Then
                For Each row As DataRow In dtAssets.Rows
                    Dim ItemQty As Integer = row("Item Qty")
                    For i As Integer = 1 To ItemQty
                        Try
                            objattAssetDetails = New attAssetDetails
                            If String.IsNullOrEmpty(row("BaseCost").ToString) Then
                                objattAssetDetails.BaseCost = 0
                            Else
                                objattAssetDetails.BaseCost = row("BaseCost")
                            End If
                            objattAssetDetails.CustodianID = "1"
                            objattAssetDetails.InsID = 0
                            objattAssetDetails.SuppID = row("SupplierID").ToString
                            objattAssetDetails.POCode = ""

                            objattAssetDetails.SerailNo = ""
                            objattAssetDetails.Discount = 0
                            objattAssetDetails.InvNumber = ""
                            objattAssetDetails.ItemCode = row("Item ID").ToString
                            objattAssetDetails.PurDate = row("PurchaseDate").ToString
                            objattAssetDetails.Tax = 0
                            objattAssetDetails.TransRemarks = ""
                            objattAssetDetails.DispDate = Date.MinValue
                            If String.IsNullOrEmpty(row("CompanyID").ToString) Then
                                objattAssetDetails.CompanyID = 1
                            Else
                                objattAssetDetails.CompanyID = row("CompanyID").ToString
                            End If
                            If Date.TryParse(row("ServiceDate").ToString, Now.Date) Then
                                objattAssetDetails.SrvDate = row("ServiceDate").ToString
                            Else
                                objattAssetDetails.SrvDate = row("PurchaseDate").ToString
                            End If
                            objattAssetDetails.AstNum = Generate_AssetNumber(objattAssetDetails.CompanyID)
                            objattAssetDetails.RefNo = row("Item Code").ToString
                            objattAssetDetails.AstBrandID = row("Brand ID").ToString
                            objattAssetDetails.AstDesc = row("Description").ToString
                            objattAssetDetails.AstModel = ""
                            objattAssetDetails.Capex = ""
                            objattAssetDetails.PoErp = ""
                            objattAssetDetails.Plate = ""
                            objattAssetDetails.GRN = ""
                            objattAssetDetails.RefCode = ""
                            objattAssetDetails.GLCode = row("GLCodeID").ToString
                            objattAssetDetails.PONumber = ""
                            objattAssetDetails.LocID = row("Location ID").ToString
                            objattAssetDetails.NoPiece = 0

                            objattAssetDetails.Disposed = "0"
                            objattAssetDetails.IsSold = 0
                            objattAssetDetails.PKeyCode = objBALAssetDetails.Generate_AssetID()

                            objattAssetDetails.BarCode = row("Item Code").ToString
                            objattAssetDetails.IsDataChanged = False
                            objattAssetDetails.CreatedBY = AppConfig.LoginName
                            objattAssetDetails.LastEditBY = AppConfig.LoginName
                            objattAssetDetails.LastEditDate = Now.Date
                            objattAssetDetails.CreationDate = Now.Date
                            objattAssetDetails.StatusID = 1

                            If objBALAssetDetails.Insert_AssetDetails(objattAssetDetails, True) Then
                                Dim objBALAssets As New BALItems
                                Dim dsDepPolicy As DataTable = objBALAssets.Get_DepPolicy(objattAssetDetails.ItemCode)
                                If Not dsDepPolicy Is Nothing Then
                                    If dsDepPolicy.Rows.Count > 0 Then
                                        objBALAssetDetails.Add_AstBooks(objattAssetDetails.PKeyCode, row("CurrentBookValue").ToString, objattAssetDetails.CompanyID, dsDepPolicy.Rows(0)("SalvageValue"), dsDepPolicy.Rows(0)("SalvageYear"), dsDepPolicy.Rows(0)("SalvageMonth"), objattAssetDetails.SrvDate, dsDepPolicy.Rows(0)("IsSalvageValuePercent"))
                                    Else
                                        MessageBox.Show("Erorr while inserting AstBooks")
                                    End If
                                Else
                                    MessageBox.Show("Erorr while inserting AstBooks")
                                End If
                            End If
                        Catch ex As Exception
                            GenericExceptionHandler(ex, WhoCalledMe)
                        End Try
                    Next
                Next
                MessageBox.Show("Import Completed Successfully.")
            End If
        End If
    End Sub

    Private Function Check_BookExist(ByVal _id As String, ByVal _Astid As String) As Boolean
        Try
            Dim objBALAstBooks As New BALAstBooks
            Dim ds As New DataTable
            Dim objattAstBooks1 As New attAstBooks
            objattAstBooks1.PKeyCode = _id
            objattAstBooks1.AstID = _Astid
            ds = objBALAstBooks.CheckID_AstBooks(objattAstBooks1)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If

            Else
                Return False
            End If

            Return False
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function


    Private Function ImportExcelXLSToDS(ByVal FileName As String, ByVal hasHeaders As Boolean) As DataSet
        Dim HDR As String = If(hasHeaders, "Yes", "No")
        Dim strConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FileName & ";Extended Properties=""Excel 8.0;HDR=" & HDR & ";IMEX=1"""

        Dim output As New DataSet()

        Using conn As New OleDbConnection(strConn)
            conn.Open()

            Dim schemaTable As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})

            For Each schemaRow As DataRow In schemaTable.Rows
                Dim sheet As String = schemaRow("TABLE_NAME").ToString()
                If sheet.Contains("$_") Then
                    Continue For
                End If
                Dim cmd As New OleDbCommand("SELECT * FROM [" & sheet & "]", conn)
                cmd.CommandType = CommandType.Text

                Dim outputTable As New DataTable(sheet)
                output.Tables.Add(outputTable)
                Dim DA As New OleDbDataAdapter(cmd)
                DA.Fill(outputTable)
            Next
        End Using
        Return output
    End Function


    Private Sub btnSelectFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectFile.Click
        With OpenFileDialog1

            .CheckFileExists = True
            .CheckPathExists = True
            .FileName = ""
            .ShowReadOnly = True
            .DefaultExt = "xls"

            .Filter = "Excel Sheet (*.xls)|*.xls"

            .Multiselect = False
            .Title = "Select Excel Sheet File"

            If .ShowDialog() = DialogResult.OK Then
                txtFilePath.Text = .FileName
            End If
            .Dispose()
        End With
    End Sub

    Private Sub btnImportMasterData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportMasterData.Click
        If Not String.IsNullOrEmpty(txtFilePath.Text) Then
            'Connect to the Excel Sheet and get the data
            Dim ds As DataSet = ImportExcelXLSToDS(txtFilePath.Text, True)

            Dim objattCategory As attCategory
            Dim objBALCategory As New BALCategory

            Dim objattAssets As attItems
            Dim objBALAssets As New BALItems
            Try


                'Import Cat Sheet
                Dim dtCat As DataTable = ds.Tables("Category$")
                If dtCat IsNot Nothing Then
                    For Each row As DataRow In dtCat.Rows
                        objattCategory = New attCategory
                        If Not String.IsNullOrEmpty(objattCategory.AstCatDesc = row("CatDesc").ToString()) Then
                            objattCategory.AstCatDesc = row("CatDesc").ToString()
                            objattCategory.AstCatID = objBALCategory.GetRootCatID()
                            objattCategory.Code = row("CatCode").ToString()
                            objattCategory.catLevel = 0
                            If objBALCategory.Insert_Category(objattCategory) Then
                                AddDepPolicyForCat(objattCategory.AstCatID)
                            End If
                        End If
                    Next
                End If

                'Import Items Sheet
                Dim dtItems As DataTable = ds.Tables("Item$")
                If dtItems IsNot Nothing Then
                    For Each row As DataRow In dtItems.Rows
                        If Not String.IsNullOrEmpty(row("Item Desc").ToString) Then
                            objattAssets = New attItems
                            Dim catCode As String = row("Category Code").ToString
                            objattAssets.AstCatID = objBALCategory.GetCatID(catCode)
                            objattAssets.PKeyCode = row("ItemCode").ToString
                            objattAssets.AstDesc = row("Item Desc").ToString
                            objBALAssets.Insert_Item(objattAssets)
                        End If
                    Next
                End If
                MessageBox.Show("Import Completed Successfully.")
            Catch ex As Exception
                GenericExceptionHandler(ex, WhoCalledMe)
            End Try
        End If
    End Sub

    Private Sub AddDepPolicyForCat(ByVal CatID As String)
        'add asset book for the company if it's tracking or inventory edition.
        Dim objattDepPolicy As New attDepPolicy
        Dim objBALDepPolicy As New BALDepPolicy

        objattDepPolicy.AstCatID = CatID
        objattDepPolicy.DepCode = 1
        objattDepPolicy.SalvageValue = 0
        objattDepPolicy.SalvageYear = 1
        objattDepPolicy.IsSalvageValuePercent = False

        objBALDepPolicy.Insert_DepPolicy(objattDepPolicy)
    End Sub
End Class