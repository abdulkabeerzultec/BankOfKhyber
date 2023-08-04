Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.UserDesigner

Public Class frmReportDesigner
    Dim objattRptFile As attReports
    Dim objBALRptFile As New BALReports

    Private Sub frmReportDesign_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmRptDesigner = Nothing
    End Sub

    Private Sub ReportDesign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Try
            grd.DataSource = objBALRptFile.GetAll_ReportsFiles(New attReports)
            format_Grid()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub format_Grid()
        Dim RIReportType As New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
        RIReportType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() _
                                                    {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Standard Report", "0", -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Extended Report", "1", -1)})
        grdView.Columns(0).Caption = "Report Name"
        grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(1).Visible = False
        grdView.Columns(2).ColumnEdit = RIReportType
        grdView.Columns(2).Caption = "Report Type"
        grdView.Columns(3).Visible = False

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        grdView.FocusedColumn = grdView.Columns(0)
        addGridMenu(grd)
    End Sub

    Private Sub btnRestoreAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestoreAll.Click
        If MessageBox.Show("Restore process will undo all the changes made by the user in report(s)" & Environment.NewLine & "Do you still want to restore the orginal report(s)?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
            pb.Visible = True
            pb.Step = 1
            pb.Value = 0
            pb.Maximum = 30

            RestoreReportToDatabase(ReportNames.Designations)
            RestoreReportToDatabase(ReportNames.Custodians)
            RestoreReportToDatabase(ReportNames.Brands)
            RestoreReportToDatabase(ReportNames.AnonymousAssets)
            RestoreReportToDatabase(ReportNames.Insurers)
            RestoreReportToDatabase(ReportNames.Suppliers)
            RestoreReportToDatabase(ReportNames.DisposalMethods)
            RestoreReportToDatabase(ReportNames.DepreciationMethods)
            RestoreReportToDatabase(ReportNames.AssetItems)
            RestoreReportToDatabase(ReportNames.AssetBooks)
            RestoreReportToDatabase(ReportNames.InventorySchedules)
            RestoreReportToDatabase(ReportNames.AddressBook)
            RestoreReportToDatabase(ReportNames.CompanyAssets)
            RestoreReportToDatabase(ReportNames.AssetsLog)
            RestoreReportToDatabase(ReportNames.DepreciationBooks)
            RestoreReportToDatabase(ReportNames.ExpectedDepreciation)
            RestoreReportToDatabase(ReportNames.AssetDetails)
            RestoreReportToDatabase(ReportNames.AssetsTagging)
            RestoreReportToDatabase(ReportNames.AssetsLedger)
            RestoreReportToDatabase(ReportNames.DisposedAssets)
            RestoreReportToDatabase(ReportNames.ItemsInventory)
            RestoreReportToDatabase(ReportNames.AssetsRegister)
            RestoreReportToDatabase(ReportNames.AssetsbyCategory)
            RestoreReportToDatabase(ReportNames.AssetsbySubCategory)
            RestoreReportToDatabase(ReportNames.LocationBarcode)
            RestoreReportToDatabase(ReportNames.AssetsBarcode)
            RestoreReportToDatabase(ReportNames.NewTags)
            'Audit Reports:
            RestoreReportToDatabase(ReportNames.MissingAssets)
            RestoreReportToDatabase(ReportNames.FoundAssets)
            RestoreReportToDatabase(ReportNames.MisplacedAssets)
            RestoreReportToDatabase(ReportNames.TransferredAssets)
            RestoreReportToDatabase(ReportNames.AllocatedAssets)
            RestoreReportToDatabase(ReportNames.AnonymousAssets)
            RestoreReportToDatabase(ReportNames.AllAssets)
            RestoreReportToDatabase(ReportNames.CostCenterAudit)

            RestoreReportToDatabase(ReportNames.AssetIssuance)


            grd.DataSource = objBALRptFile.GetAll_ReportsFiles(New attReports)
            pb.Visible = False
            MessageBox.Show("The restoration of all the reports has been successful", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnRestore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestore.Click
        If MessageBox.Show("Restore process will undo all the changes made by the user in report(s)" & Environment.NewLine & "Do you still want to restore the orginal report(s)?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
            Dim ReportName As String = GetGridRowCellValue(grdView, grdView.GetSelectedRows(0), "ReportName")
            RestoreReportToDatabase(ReportName)
            'SaveReportFileToDatabase(ReportName)
            grd.DataSource = objBALRptFile.GetAll_ReportsFiles(New attReports)
            MessageBox.Show("The restoration of the report was successful", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Function RestoreReportToDatabase(ByVal ReportName As String) As Boolean

        Select Case ReportName
            Case ReportNames.Designations
                SaveReportToDatabase(ReportName, New rptDesignation)
            Case ReportNames.Custodians
                SaveReportToDatabase(ReportName, New rptCustodian)
            Case ReportNames.Brands
                SaveReportToDatabase(ReportName, New rptBrands)
            Case ReportNames.Insurers
                SaveReportToDatabase(ReportName, New rptInsurer)
            Case ReportNames.Suppliers
                SaveReportToDatabase(ReportName, New rptSupplier)
            Case ReportNames.DisposalMethods
                SaveReportToDatabase(ReportName, New rptDisposalMethods)
            Case ReportNames.DepreciationMethods
                SaveReportToDatabase(ReportName, New rptDepreciationMethods)
            Case ReportNames.AssetItems
                SaveReportToDatabase(ReportName, New rptAssetsItems)
            Case ReportNames.AssetBooks
                SaveReportToDatabase(ReportName, New rptAssetBooks)
            Case ReportNames.InventorySchedules
                SaveReportToDatabase(ReportName, New rptInventorySchedule)
            Case ReportNames.AddressBook
                SaveReportToDatabase(ReportName, New rptAddressBook)
            Case ReportNames.CompanyAssets
                SaveReportToDatabase(ReportName, New rptCompanyAssets)
            Case ReportNames.AssetsLog
                SaveReportToDatabase(ReportName, New rptAssetsLog)
            Case ReportNames.DepreciationBooks
                SaveReportToDatabase(ReportName, New rptDepBooks)
            Case ReportNames.ExpectedDepreciation
                SaveReportToDatabase(ReportName, New rptDepBooks)
            Case ReportNames.AssetDetails
                SaveReportToDatabase(ReportName, New rptAssetDetail)
            Case ReportNames.AssetsTagging
                SaveReportToDatabase(ReportName, New rptAssetTag)
            Case ReportNames.AssetsLedger
                SaveReportToDatabase(ReportName, New rptAssetLedger)
            Case ReportNames.DisposedAssets
                SaveReportToDatabase(ReportName, New rptDisposed)
            Case ReportNames.ItemsInventory
                SaveReportToDatabase(ReportName, New rptItemInventory)
            Case ReportNames.AssetsRegister
                SaveReportToDatabase(ReportName, New rptAssetRegister)
            Case ReportNames.AssetsbyCategory
                SaveReportToDatabase(ReportName, New rptAssetByCategory)
            Case ReportNames.AssetsbySubCategory
                SaveReportToDatabase(ReportName, New rptAssetBySubCategory)
            Case ReportNames.LocationBarcode
                SaveReportToDatabase(ReportName, New rptLocLabel)
            Case ReportNames.AssetsBarcode
                SaveReportToDatabase(ReportName, New rptAssetLabel)
            Case ReportNames.NewTags
                SaveReportToDatabase(ReportName, New rptNewTag)
                'Audit Reports:
            Case ReportNames.MissingAssets
                SaveReportToDatabase(ReportName, New rptMissing)
            Case ReportNames.FoundAssets
                SaveReportToDatabase(ReportName, New rptFound)
            Case ReportNames.MisplacedAssets
                SaveReportToDatabase(ReportName, New rptMisplaced)
            Case ReportNames.TransferredAssets
                SaveReportToDatabase(ReportName, New rptTransferred)
            Case ReportNames.AllocatedAssets
                SaveReportToDatabase(ReportName, New rptAllocated)
            Case ReportNames.AnonymousAssets
                SaveReportToDatabase(ReportName, New rptAnonymous)
            Case ReportNames.AllAssets
                SaveReportToDatabase(ReportName, New rptAllAssets)
            Case ReportNames.CostCenterAudit
                SaveReportToDatabase(ReportName, New rptCostCenterAudit)
            Case ReportNames.AssetIssuance
                SaveReportToDatabase(ReportName, New rptAssetIssuance)
                'Case ReportNames.MissingAssets, ReportNames.FoundAssets, ReportNames.MisplacedAssets, ReportNames.TransferredAssets, ReportNames.AllocatedAssets, ReportNames.AnonymousAssets, ReportNames.AllAssets
                '    SaveReportToDatabase(ReportName, New rptAuditStatus)

        End Select
        pb.PerformStep()
        Application.DoEvents()
    End Function


    Public Sub SaveReportToDatabase(ByVal ReportName As String, ByVal report As DevExpress.XtraReports.UI.XtraReport)
        objattRptFile = New attReports
        objattRptFile.ReportName = ReportName

        ' Save the report to a stream.
        Dim stream As New MemoryStream()
        report.SaveLayout(stream)
        ' Prepare the stream for reading.
        stream.Position = 0
        ' Insert the report to a database.
        Using sr As New StreamReader(stream)
            ' Read the report from the stream to a string variable.
            objattRptFile.ReportData = sr.ReadToEnd()
            If objBALRptFile.ReportNameExist(objattRptFile, True) Then
                objBALRptFile.Update_ReportFile(objattRptFile)
            Else
                objBALRptFile.Insert_ReportFile(objattRptFile)
            End If
        End Using
    End Sub

    Public Sub SaveReportToFile(ByVal ReportName As String, ByVal report As DevExpress.XtraReports.UI.XtraReport)
        'objattRptFile = New attReports
        'objattRptFile.ReportName = ReportName
        Dim FilePath As String = Application.StartupPath & "\Reports\" & ReportName
        ' Save a report's layout to the configuration file.
        report.SaveLayout(FilePath)
        pb.PerformStep()
    End Sub

    Public Sub SaveReportFileToDatabase(ByVal ReportName As String)
        objattRptFile = New attReports
        objattRptFile.ReportName = ReportName
        Dim FilePath As String = Application.StartupPath & "\Reports\" & ReportName
        SaveReportToDatabase(ReportName, XtraReport.FromFile(FilePath, True))
        pb.PerformStep()
        Application.DoEvents()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmRptDesigner = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnDesign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDesign.Click
        Dim FocRow As Integer = grdView.GetSelectedRows(0)
        Dim ReportName As String = GetGridRowCellValue(grdView, FocRow, "ReportName")

        ' Declare a base report variable.
        Dim newReport As XtraReport

        ' Retrieve a string which contains the report.

        Dim ds As DataTable = objBALRptFile.GetReportFileData(ReportName)
        Dim s As String = ds.Rows(0)("ReportData").ToString

        ' Obtain the report from the string.
        Dim sw As New StreamWriter(New MemoryStream())
        Try
            sw.Write(s)
            sw.Flush()
            newReport = XtraReport.FromStream(sw.BaseStream, True)
        Finally
            sw.Dispose()
        End Try


        If GetGridRowCellValue(grdView, FocRow, "Type") = False Then ' standard report

            'add handler to catch the save,save as , close events to Report Designer.
            AddHandler newReport.DesignerLoaded, AddressOf r_DesignerLoaded
            ' Create a new End-User Designer.
            Dim DesignForm As New XRDesignFormEx()
            ' Load a report into the designer. 
            DesignForm.OpenReport(newReport)
            ' Hide Items fro the report designer
            DesignForm.DesignPanel.SetCommandVisibility(ReportCommand.NewReportWizard, CommandVisibility.None)
            DesignForm.DesignPanel.SetCommandVisibility(ReportCommand.VerbReportWizard, CommandVisibility.None)
            DesignForm.DesignPanel.SetCommandVisibility(ReportCommand.OpenFile, CommandVisibility.None)
            DesignForm.DesignPanel.SetCommandVisibility(ReportCommand.NewReport, CommandVisibility.None)

            Dim ReportPath As String = AppConfig.AppDataFolder & "\TempRpt.repx"
            DesignForm.FileName = ReportPath
            DesignForm.ShowDialog()
        Else ' Extended Report
            Dim frm As New frmReportWizard
            frm.Text = "Edit Report Wizard"
            frm.txtReportName.Text = ReportName
            frm.txtReportName.Properties.ReadOnly = True
            frm.txtQuery.Text = GetGridRowCellValue(grdView, FocRow, "Query")
            frm.IsNewReport = False
            frm.setReport(newReport)
            frm.ShowDialog()
            frm.Dispose()
        End If
        grd.DataSource = objBALRptFile.GetAll_ReportsFiles(New attReports)
    End Sub

    Private Sub r_DesignerLoaded(ByVal sender As Object, ByVal e As DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs)
        Dim panel As XRDesignPanel = TryCast(e.DesignerHost.GetService(GetType(XRDesignPanel)), XRDesignPanel)
        panel.AddCommandHandler(New SaveCommandHandler(panel, GetGridRowCellValue(grdView, grdView.GetSelectedRows(0), "ReportName")))
    End Sub

    Private Sub grdView_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdView.DoubleClick
        If btnDesign.Enabled Then
            btnDesign_Click(sender, e)
        End If
    End Sub

    Private Sub btnSaveAllToFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAllToFiles.Click
        pb.Visible = True
        pb.Step = 1
        pb.Value = 0
        pb.Maximum = 30
        SaveReportToFile(ReportNames.Designations, New rptDesignation)
        SaveReportToFile(ReportNames.Custodians, New rptCustodian)
        SaveReportToFile(ReportNames.Brands, New rptBrands)
        SaveReportToFile(ReportNames.Insurers, New rptInsurer)
        SaveReportToFile(ReportNames.Suppliers, New rptSupplier)
        SaveReportToFile(ReportNames.DisposalMethods, New rptDisposalMethods)
        SaveReportToFile(ReportNames.DepreciationMethods, New rptDepreciationMethods)
        SaveReportToFile(ReportNames.AssetItems, New rptAssetsItems)
        SaveReportToFile(ReportNames.AssetBooks, New rptAssetBooks)
        SaveReportToFile(ReportNames.InventorySchedules, New rptInventorySchedule)
        SaveReportToFile(ReportNames.AddressBook, New rptAddressBook)
        SaveReportToFile(ReportNames.CompanyAssets, New rptCompanyAssets)
        SaveReportToFile(ReportNames.NewTags, New rptNewTag)
        SaveReportToFile(ReportNames.DepreciationBooks, New rptDepBooks)
        SaveReportToFile(ReportNames.ExpectedDepreciation, New rptDepBooks)
        SaveReportToFile(ReportNames.AssetDetails, New rptAssetDetail)
        SaveReportToFile(ReportNames.AssetsTagging, New rptAssetTag)
        SaveReportToFile(ReportNames.AssetsLedger, New rptAssetLedger)
        SaveReportToFile(ReportNames.DisposedAssets, New rptDisposed)
        SaveReportToFile(ReportNames.ItemsInventory, New rptItemInventory)
        SaveReportToFile(ReportNames.AssetsRegister, New rptAssetRegister)
        SaveReportToFile(ReportNames.AssetsbyCategory, New rptAssetByCategory)
        SaveReportToFile(ReportNames.AssetsbySubCategory, New rptAssetBySubCategory)
        SaveReportToFile(ReportNames.LocationBarcode, New rptLocLabel)
        SaveReportToFile(ReportNames.AssetsBarcode, New rptAssetLabel)
        'Audit Reports
        SaveReportToFile(ReportNames.MissingAssets, New rptMissing)
        SaveReportToFile(ReportNames.FoundAssets, New rptFound)
        SaveReportToFile(ReportNames.MisplacedAssets, New rptMisplaced)
        SaveReportToFile(ReportNames.TransferredAssets, New rptTransferred)
        SaveReportToFile(ReportNames.AllocatedAssets, New rptAllocated)
        SaveReportToFile(ReportNames.AnonymousAssets, New rptAnonymous)
        SaveReportToFile(ReportNames.AllAssets, New rptAllAssets)

        pb.Visible = False
    End Sub

    Private Class SaveCommandHandler
        Implements ICommandHandler
        Dim ReportName As String = ""
        Dim objattRptFile As attReports
        Dim objBALRptFile As New BALReports


        Private panel As XRDesignPanel

        Public Sub New(ByVal panel As XRDesignPanel, ByVal rptName As String)
            Me.panel = panel
            Me.ReportName = rptName
        End Sub

        Public Overridable Sub HandleCommand(ByVal command As ReportCommand, ByVal args() As Object, ByRef handled As Boolean) Implements ICommandHandler.HandleCommand
            If Not CanHandleCommand(command) Then
                Return
            End If

            ' Save a report.
            panel.ReportState = ReportState.Saved
            SaveReportToDatabase(ReportName, panel.Report)

            ' Set handled to true to avoid the standard saving procedure to be called.
            handled = True
        End Sub

        Public Overridable Function CanHandleCommand(ByVal command As ReportCommand) As Boolean Implements ICommandHandler.CanHandleCommand

            'This handler is used for SaveFile, SaveFileAs and Closing commands.
            Return command = ReportCommand.SaveFile Or command = ReportCommand.SaveFileAs Or command = ReportCommand.Closing
        End Function

        Public Sub SaveReportToDatabase(ByVal ReportName As String, ByVal report As DevExpress.XtraReports.UI.XtraReport)
            objattRptFile = New attReports
            objattRptFile.ReportName = ReportName

            ' Save the report to a stream.
            Dim stream As New MemoryStream()
            report.SaveLayout(stream)
            ' Prepare the stream for reading.
            stream.Position = 0
            ' Insert the report to a database.
            Using sr As New StreamReader(stream)
                ' Read the report from the stream to a string variable.
                objattRptFile.ReportData = sr.ReadToEnd()
                If objBALRptFile.ReportNameExist(objattRptFile, True) Then
                    objBALRptFile.Update_ReportFile(objattRptFile)
                Else
                    objBALRptFile.Insert_ReportFile(objattRptFile)
                End If
            End Using
        End Sub
    End Class

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim ReportName As String = GetGridRowCellValue(grdView, grdView.GetSelectedRows(0), "ReportName")
        If (ZulMessageBox.ShowMe("Are you sure you want to delete Extended Report( " & ReportName & ")?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, False) = Windows.Forms.DialogResult.Yes) Then
            Dim objattRptFile As New attReports
            Dim objBALRptFile As New BALReports
            objattRptFile.ReportName = ReportName
            If objBALRptFile.Delete_Report(objattRptFile) Then
                grd.DataSource = objBALRptFile.GetAll_ReportsFiles(New attReports)
                'MainForm.mnuRptExtended.DropDownItems.RemoveByKey(ReportName.Replace(" ", ""))
                'If MainForm.mnuRptExtended.DropDownItems.Count > 0 Then
                '    MainForm.mnuRptExtended.Visible = True
                'Else
                '    MainForm.mnuRptExtended.Visible = False
                'End If
            End If
        End If
    End Sub

    Private Sub grdView_SelectionChanged(ByVal sender As System.Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles grdView.SelectionChanged
        If grdView.SelectedRowsCount > 1 Then
            btnDelete.Enabled = False
            btnDesign.Enabled = False
            btnRestore.Enabled = False
            btnExport.Enabled = True
        ElseIf grdView.SelectedRowsCount = 1 Then
            btnDelete.Enabled = True
            btnDesign.Enabled = True
            btnRestore.Enabled = True
            Dim FocRow As Integer = grdView.GetSelectedRows(0)
            If GetGridRowCellValue(grdView, FocRow, "Type") = False Then
                btnRestore.Visible = True
                btnDelete.Visible = False
            Else
                btnRestore.Visible = False
                btnDelete.Visible = True
            End If
            btnExport.Enabled = True
        Else
            btnDelete.Enabled = False
            btnDesign.Enabled = False
            btnRestore.Enabled = False
            btnExport.Enabled = False
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If dlgFolder.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim FilePath As String = dlgFolder.SelectedPath
            pb.Visible = True
            pb.Step = 1
            pb.Value = 0
            pb.Maximum = grdView.GetSelectedRows().Length
            Dim rpt As XtraReport
            For Each rowhandle As Integer In grdView.GetSelectedRows()
                Dim ReportName As String = GetGridRowCellValue(grdView, rowhandle, "ReportName")
                ' Declare a base report variable.
                objattRptFile = New attReports
                objattRptFile.ReportName = ReportName
                ' Retrieve a string which contains the report.
                Dim ds As DataTable = objBALRptFile.GetAll_ReportsFiles(objattRptFile)
                Dim s As String = ds.Rows(0)("ReportData").ToString

                ' Obtain the report from the string.
                Dim sw As New StreamWriter(New MemoryStream())
                Try
                    sw.Write(s)
                    sw.Flush()
                    rpt = XtraReport.FromStream(sw.BaseStream, True)
                Finally
                    sw.Dispose()
                End Try
                ' put reportname and query and type in the report tag to insert it to the database while import.
                rpt.Tag = ReportName & "#" & ds.Rows(0)("Type").ToString & "#" & ds.Rows(0)("Query").ToString & "#" & My.Application.Info.Version.ToString
                rpt.SaveLayout(FilePath & "\" & ReportName & ".repx")
                pb.PerformStep()
                Application.DoEvents()
            Next
            pb.Visible = False
        End If
    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        dlgFile.InitialDirectory = Application.StartupPath
        If dlgFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            If ZulMessageBox.ShowMe("This will overwrite all reports have the same name, are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, False) = Windows.Forms.DialogResult.Yes Then
                pb.Visible = True
                pb.Step = 1
                pb.Value = 0
                pb.Maximum = dlgFile.FileNames.Length
                Dim rpt As XtraReport
                For Each filename As String In dlgFile.FileNames
                    Try
                        rpt = XtraReport.FromFile(filename, True)
                        objattRptFile = New attReports
                        Dim arr As String() = rpt.Tag.ToString.Split("#"c)
                        objattRptFile.ReportName = arr(0)
                        objattRptFile.Type = arr(1)
                        objattRptFile.Query = arr(2)
                        ' Save the report to a stream.
                        Dim stream As New MemoryStream()
                        rpt.SaveLayout(stream)
                        ' Prepare the stream for reading.
                        stream.Position = 0
                        ' Read the report from the stream to a string variable.
                        Dim sr As New StreamReader(stream)
                        objattRptFile.ReportData = sr.ReadToEnd()
                        If objBALRptFile.ReportNameExist(objattRptFile, True) Then
                            objBALRptFile.Update_ReportFile(objattRptFile)
                        Else
                            objBALRptFile.Insert_ReportFile(objattRptFile)
                            'If objattRptFile.Type = "True" Then
                            '    Dim itm As ToolStripMenuItem = MainForm.mnuRptExtended.DropDownItems.Add(objattRptFile.ReportName, Nothing, New EventHandler(AddressOf MainForm.ExtendedReport_Click))
                            '    itm.Name = objattRptFile.ReportName
                            '    itm.Tag = objattRptFile.Query
                            'End If
                        End If

                    Catch ex As Exception
                        MessageBox.Show("Error while importing report (" & Path.GetFileName(filename) & ")" & Environment.NewLine & ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    pb.PerformStep()
                    Application.DoEvents()
                Next
                grd.DataSource = objBALRptFile.GetAll_ReportsFiles(New attReports)
                pb.Visible = False
                'If MainForm.mnuRptExtended.DropDownItems.Count > 0 Then
                '    MainForm.mnuRptExtended.Visible = True
                'Else
                '    MainForm.mnuRptExtended.Visible = False
                'End If

            End If
        End If
    End Sub
End Class