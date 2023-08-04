Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO
Imports System.Data.OleDb
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.UserDesigner

Public Class frmReportWizard
    Dim objattRptFile As New attReports
    Dim objBALRptFile As New BALReports


    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Private Shared newReport As XtraReport
    Friend IsNewReport As Boolean = False

    Public Sub setReport(ByVal rep As XtraReport)
        newReport = rep
    End Sub
    Public Sub CreateNewReport()
        newReport = New rptEmptyReport()
    End Sub

    Private Sub WizardControl1_NextClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraWizard.WizardCommandButtonClickEventArgs) Handles WizardControl1.NextClick
        If WizardControl1.SelectedPage.Name = WPQuerySetup.Name Then
            If valProvMain.Validate Then
                Dim objattRptFile As New attReports
                Dim objBALRptFile As New BALReports
                objattRptFile.ReportName = txtReportName.Text

                If Not objBALRptFile.ReportNameExist(objattRptFile, IsNewReport) Then
                    grdView.Columns.Clear()
                    WPPreviewData.AllowNext = False
                Else
                    errProv.SetError(txtReportName, "Report Name already Exist!")
                    e.Handled = True
                End If
            Else
                e.Handled = True
            End If
        ElseIf WizardControl1.SelectedPage.Name = WPPreviewData.Name Then
            'WizardControl1.NextText = "&Design->"
        ElseIf WizardControl1.SelectedPage.Name = WPDesignReport.Name Then
            newReport.DataSource = CType(grd.DataSource, DataTable)
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
            DesignForm.DesignPanel.SetCommandVisibility(ReportCommand.SaveFile, CommandVisibility.None)
            DesignForm.DesignPanel.SetCommandVisibility(ReportCommand.SaveFileAs, CommandVisibility.None)

            Dim ReportPath As String = AppConfig.AppDataFolder & "\TempRpt.repx"
            DesignForm.FileName = ReportPath
            DesignForm.ShowDialog()
            DesignForm.Dispose()
        End If
    End Sub

    Private Sub r_DesignerLoaded(ByVal sender As Object, ByVal e As DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs)
        Dim panel As XRDesignPanel = TryCast(e.DesignerHost.GetService(GetType(XRDesignPanel)), XRDesignPanel)
        panel.AddCommandHandler(New SaveCommandHandler(panel, txtReportName.Text, txtQuery.Text))
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim DbOp As New DBOperations
        Dim cmd As New OleDbCommand
        Try
            If txtQuery.Text.ToLower.StartsWith("select") Then
                cmd.CommandText = txtQuery.Text
                grd.DataSource = DbOp.General_Executer(cmd)
                WPPreviewData.AllowNext = True
            Else
                'MessageBox.Show("The SQL statment should strat with Select keyword.", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmd.CommandText = txtQuery.Text
                DbOp.General_Executer(cmd)
                MessageBox.Show("Query Executed successfully.", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
                WizardControl1.SelectedPage = WPQuerySetup
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
            WizardControl1.SelectedPage = WPQuerySetup
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub frmReportWizard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        valProvMain.SetValidationRule(txtQuery, valRulenotEmpty)
        valProvMain.SetValidationRule(txtReportName, valRulenotEmpty)
        WizardControl1.UseCancelButton = False
    End Sub

    Private Class SaveCommandHandler
        Implements ICommandHandler
        Dim ReportName As String = ""
        Dim query As String = ""


        Private panel As XRDesignPanel

        Public Sub New(ByVal panel As XRDesignPanel, ByVal rptName As String, ByVal qry As String)
            Me.panel = panel
            Me.ReportName = rptName
            Me.query = qry
        End Sub

        Public Overridable Sub HandleCommand(ByVal command As ReportCommand, ByVal args() As Object, ByRef handled As Boolean) Implements ICommandHandler.HandleCommand
            If Not CanHandleCommand(command) Then
                Return
            End If

            ' Save a report.
            panel.ReportState = ReportState.Saved
            newReport = panel.Report

            ' Set handled to true to avoid the standard saving procedure to be called.
            handled = True
        End Sub

        Public Overridable Function CanHandleCommand(ByVal command As ReportCommand) As Boolean Implements ICommandHandler.CanHandleCommand
            'This handler is used for SaveFile, SaveFileAs and Closing commands.
            Return command = ReportCommand.SaveFile Or command = ReportCommand.SaveFileAs Or command = ReportCommand.Closing
        End Function


    End Class

    Private Sub WizardControl1_CancelClick(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles WizardControl1.CancelClick
        'show message are you sure...
        If ZulMessageBox.ShowMe("CancelReport", MessageBoxButtons.YesNo, MessageBoxIcon.Question, True) <> DialogResult.Yes Then
            e.Cancel = True
        End If
    End Sub

    Private Sub WizardControl1_FinishClick(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles WizardControl1.FinishClick
        ' save the report here
        SaveReportToDatabase(txtReportName.Text, newReport, txtQuery.Text)
        'If IsNewReport Then
        '    'add the new report to the menue
        '    Dim itm As ToolStripMenuItem = MainForm.mnuRptExtended.DropDownItems.Add(txtReportName.Text, Nothing, New EventHandler(AddressOf MainForm.ExtendedReport_Click))
        '    itm.Name = txtReportName.Text.Replace(" ", "")
        '    itm.Tag = txtQuery.Text

        '    If MainForm.mnuRptExtended.DropDownItems.Count > 0 Then
        '        MainForm.mnuRptExtended.Visible = True
        '    Else
        '        MainForm.mnuRptExtended.Visible = False
        '    End If
        'Else
        '    'locate the menu item
        '    Dim itm As ToolStripMenuItem = MainForm.mnuRptExtended.DropDownItems(txtReportName.Text.Replace(" ", ""))
        '    itm.Tag = txtQuery.Text
        'End If
    End Sub

    Public Sub SaveReportToDatabase(ByVal ReportName As String, ByVal report As DevExpress.XtraReports.UI.XtraReport, ByVal query As String)
        Dim objattRptFile As New attReports
        Dim objBALRptFile As New BALReports

        objattRptFile.ReportName = ReportName
        objattRptFile.Query = query
        objattRptFile.Type = 1
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
            ZulMessageBox.ShowMe("Saved")
        End Using
    End Sub

    Private Sub frmReportWizard_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Not IsNewReport Then
            WelcomeWizardPage1.Text = "Edit Report"
            WelcomeWizardPage1.IntroductionText = "This wizard simplifies Editing report in a series of simple steps"
            CompletionWizardPage1.Text = "Completing Editing the report wizard"
            CompletionWizardPage1.FinishText = "You have successfully completed Editing the report click finish to save the report."
        End If
    End Sub

    Private Sub txtReportName_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtReportName.EditValueChanged
        errProv.ClearErrors()
    End Sub
End Class


