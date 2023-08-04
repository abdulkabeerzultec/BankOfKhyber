Imports DevExpress.XtraGrid
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Windows.Forms

Public Class frmExport
    Private _ExportFileName As String = ""
    Private _ExportFolderName As String = ""
    Private ExportDataTable As DataTable

    Dim objExport As New ZulAssetsBL.SAPExportBLL

    Public Property ExportFileName() As String
        Get
            Return _ExportFileName
        End Get
        Set(ByVal value As String)
            _ExportFileName = value
        End Set
    End Property

    Public Property ExportFolderName() As String
        Get
            Return _ExportFolderName
        End Get
        Set(ByVal value As String)
            _ExportFolderName = value
        End Set
    End Property

    Private Sub frmExport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.WindowState = FormWindowState.Maximized
    End Sub

    'Private Sub frmExport_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    '    GetAssetChangeData(-1)
    '    FormatAssetChangeGrid()
    'End Sub

    'Public Function ExportDataFile(ByVal lbl As DevExpress.XtraEditors.LabelControl, ByVal pb As DevExpress.XtraEditors.ProgressBarControl, ByVal FileName As String, ByVal SilentProcess As Boolean, ByVal Append As Boolean) As String
    '    lbl.Text = "Processing Data Asset Change file..."
    '    lbl.Update()
    '    If CheckFile(FileName) Then
    '        If SilentProcess Then 'Get the data again if SilentProcess is activated, means if export ALL button clicked.
    '            GetAssetChangeData()
    '        End If
    '        Application.DoEvents()
    '        lbl.Text = "Export Asset Change file..."
    '        lbl.Update()
    '        If grdView.RowCount > 0 Then
    '            Dim ExportedRecordCount As Integer ' = ExportChangedData(ExportDataTable, FileName, pb, Append)
    '            If ExportedRecordCount >= 0 Then
    '                Dim msg As String = "Export Asset Change file Completed, exported Asset count = (" & ExportedRecordCount.ToString & ")"
    '                If Not SilentProcess Then
    '                    MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Else
    '                    SaveToLogFile(msg, False)
    '                End If
    '                Return msg
    '            Else
    '                Dim msg As String = String.Format("Error while exporting file path({0})", FileName)
    '                SaveToLogFile(msg, False)
    '                Return msg
    '            End If
    '        Else
    '            Dim msg As String = "AssetChange file, no data to export."
    '            SaveToLogFile(msg, False)
    '            lbl.Text = String.Format(msg, FileName)
    '            lbl.Update()
    '            Return msg
    '        End If
    '    Else
    '        Dim msg As String = String.Format("File not found, Incorrect path({0})", FileName)
    '        SaveToLogFile(msg, True)
    '        lbl.Text = msg
    '        lbl.Update()
    '        Return msg
    '    End If
    'End Function

    Private Sub GetAssetChangeData(ByVal InvSchCode As Integer)
        Dim objAssetExport As New BALNMCIntegration
        ExportDataTable = objAssetExport.GetExportData(InvSchCode)

        ExportDataTable.Columns.Add("CategoryMajor", Type.GetType("System.String"))
        ExportDataTable.Columns.Add("CategoryMinor", Type.GetType("System.String"))

        ExportDataTable.Columns.Add("Country", Type.GetType("System.String"))
        ExportDataTable.Columns.Add("City", Type.GetType("System.String"))
        ExportDataTable.Columns.Add("Building", Type.GetType("System.String"))
        ExportDataTable.Columns.Add("Floor", Type.GetType("System.String"))

        For Each row As DataRow In ExportDataTable.Rows

            Dim FulLoc As String = row("LocationFullPath")
            Dim strFulLoc As String() = FulLoc.Split("\")
            row("Country") = strFulLoc(0).Trim

            If strFulLoc.Length > 1 Then
                row("City") = strFulLoc(1).Trim
            Else
                row("City") = String.Empty
            End If

            If strFulLoc.Length > 2 Then
                row("Building") = strFulLoc(2).Trim
            Else
                row("Building") = String.Empty
            End If

            If strFulLoc.Length > 3 Then
                row("Floor") = strFulLoc(3).Trim
            Else
                row("Floor") = String.Empty
            End If

            Dim FulCat As String = row("CatFullPath")
            Dim strFulCat As String() = FulCat.Split("\")

            row("CategoryMajor") = strFulCat(0).Trim

            If strFulCat.Length > 1 Then
                row("CategoryMinor") = strFulCat(1).Trim
            Else
                row("CategoryMinor") = String.Empty
            End If
            row.AcceptChanges()
        Next

        ExportDataTable.Columns.Remove("LocationFullPath")
        ExportDataTable.Columns.Remove("CatFullPath")
        ExportDataTable.Columns("CategoryMajor").SetOrdinal(6)
        ExportDataTable.Columns("CategoryMinor").SetOrdinal(7)
        ExportDataTable.Columns("Country").SetOrdinal(8)
        ExportDataTable.Columns("City").SetOrdinal(9)
        ExportDataTable.Columns("Building").SetOrdinal(10)
        ExportDataTable.Columns("Floor").SetOrdinal(11)

        ExportDataTable.AcceptChanges()
        grd.DataSource = ExportDataTable
    End Sub


    Private Sub FormatAssetChangeGrid()
        grdView.BestFitColumns()
        'grdView.OptionsBehavior.Editable = True

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
    End Sub

    Public Function GetExportFileName(ByVal ExportFolderName As String) As String
        Return String.Format("{0}\Inventory_{1}.xls", ExportFolderName, Now.Date.ToString("dd.MM.yyyy").Replace(".", ""))
    End Function

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If grdView.RowCount > 0 Then
            grdView.ExportToXls(ExportFileName)
        Else
            MessageBox.Show("No data to export.", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Me.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub cmbSch_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSch.LovBtnClick
        Dim objBALAst_INV_Schedule As New BALAst_INV_Schedule
        cmbSch.ValueMember = "InvSchCode"
        cmbSch.DisplayMember = "InvDesc"
        cmbSch.DataSource = objBALAst_INV_Schedule.Getcombo_Schedule()
        cmbSch.OpenLOV()
    End Sub

    Private Sub cmbSch_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSch.SelectTextChanged
        If Not String.IsNullOrEmpty(cmbSch.SelectedValue) Then
            GetAssetChangeData(cmbSch.SelectedValue)
            FormatAssetChangeGrid()
            Dim dt As New DataTable
            Dim objattInvSchedule As New attInvSchedule
            Dim objBALAst_INV_Schedule1 As New BALAst_INV_Schedule
            objattInvSchedule.PKeyCode = cmbSch.SelectedValue
            dt = objBALAst_INV_Schedule1.GetAll_invSchID(cmbSch.SelectedValue)
            lblStart.Text = ""
            lblEnd.Text = ""
            If dt Is Nothing = False Then
                If dt.Rows.Count > 0 Then
                    lblStart.Text = dt.Rows(0)("InvStartDate")
                    lblEnd.Text = dt.Rows(0)("InvEndDate")
                End If
            End If
        End If
    End Sub

End Class
