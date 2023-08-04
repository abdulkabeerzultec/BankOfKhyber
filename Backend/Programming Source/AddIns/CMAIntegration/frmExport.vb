Imports DevExpress.XtraGrid
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class frmExport

    Dim fileName As String = ""
    Dim ExportDataTable As DataTable
    Private Sub frmExport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.WindowState = FormWindowState.Maximized
        GetData(False, 0)
        FormatGrid()
    End Sub
    Private Sub GetData(ByVal HideMisplaced As Boolean, ByVal InvSchCode As Integer)
        Dim objCMAExport As New BALCMAIntegration
        ExportDataTable = objCMAExport.GetAllData_CMAExportGrid(HideMisplaced, InvSchCode)
        ExportDataTable.Columns.Add("MainCategory", Type.GetType("System.String"))
        ExportDataTable.Columns.Add("SubCategory", Type.GetType("System.String"))
        ExportDataTable.Columns.Add("CityName", Type.GetType("System.String"))
        ExportDataTable.Columns.Add("BuildingName", Type.GetType("System.String"))
        ExportDataTable.Columns.Add("FloorName", Type.GetType("System.String"))
        ExportDataTable.Columns.Add("RoomName", Type.GetType("System.String"))

        For Each row As DataRow In ExportDataTable.Rows
            If row("AstDesc").ToString = "Empty" Then
                row("AstDesc") = String.Empty
            End If
            Dim FulCat As String = row("CatFullPath")
            Dim strFulCat As String() = FulCat.Split("\")
            row("MainCategory") = strFulCat(0).Trim
            If strFulCat.Length > 1 Then
                row("SubCategory") = strFulCat(1).Trim
            End If
            Dim FulLoc As String = row("LocationFullPath")
            Dim strFulLoc As String() = FulLoc.Split("\")
            row("CityName") = strFulLoc(0).Trim
            If strFulLoc.Length > 1 Then
                row("BuildingName") = strFulLoc(1).Trim
            Else
                row("BuildingName") = String.Empty
            End If

            If strFulLoc.Length > 2 Then
                row("FloorName") = strFulLoc(2).Trim
            Else
                row("FloorName") = String.Empty
            End If

            If strFulLoc.Length > 3 Then
                row("RoomName") = strFulLoc(3).Trim
            Else
                row("RoomName") = String.Empty
            End If
            row.AcceptChanges()
        Next
        ExportDataTable.Columns.Remove("CatFullPath")
        ExportDataTable.Columns.Remove("LocationFullPath")
        ExportDataTable.AcceptChanges()
        grd.DataSource = ExportDataTable
    End Sub
    Private Sub FormatGrid()
        grdView.BestFitColumns()

        grdView.Columns("AstID").Caption = "Asset Tag"
        'grdView.Columns("CatFullPath").Visible = False
        'grdView.Columns("LocationFullPath").Visible = False
        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
    End Sub

    Private Sub ExportDatasetToCsv(ByVal MyDatatable As DataTable, ByVal FileName As String)
        'Declaration of Variables
        Dim dr As DataRow
        Dim myString As String
        Dim bFirstRecord As Boolean = True
        Dim myWriter As New System.IO.StreamWriter(FileName, False, System.Text.Encoding.GetEncoding("windows-1256"))
        myString = ""
        pb.Visible = True
        pb.Value = 1
        pb.Step = 1
        pb.Maximum = grdView.DataRowCount
        Application.DoEvents()
        Try
            For Each dr In MyDatatable.Rows
                bFirstRecord = True
                For Each field As Object In dr.ItemArray
                    If Not bFirstRecord Then
                        myString += ","
                    End If
                    myString += field.ToString
                    bFirstRecord = False
                Next
                'New Line to differentiate next row
                myString += Environment.NewLine
                pb.PerformStep()
                Application.DoEvents()
            Next
        Catch ex As Exception

        Finally
            pb.Visible = False
        End Try
        'Write the String to the Csv File
        myWriter.Write(myString)
        'Clean up
        myWriter.Close()
        MessageBox.Show("Export Completed, Exported Assets Count = " & grdView.DataRowCount & ".")
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        With SaveFileDialog1
            .CheckPathExists = True
            .FileName = ""
            .DefaultExt = "csv"
            .Filter = "Comma-separated values (*.csv)|*.csv"
            .Title = "Comma-separated values File"

            If .ShowDialog() = DialogResult.OK Then
                fileName = .FileName
                ExportDatasetToCsv(ExportDataTable, fileName)
            Else
                Me.Close()
            End If
            .Dispose()
        End With
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub chkHide_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHide.CheckedChanged
        GetData(chkHide.Checked, 0)
    End Sub


    'Private Sub cmbSch_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim objBALAst_INV_Schedule As New BALAst_INV_Schedule
    '    cmbSch.ValueMember = "InvSchCode"
    '    cmbSch.DisplayMember = "InvDesc"
    '    cmbSch.DataSource = objBALAst_INV_Schedule.Getcombo_Schedule()
    '    cmbSch.OpenLOV()
    'End Sub

    'Private Sub cmbSch_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSch.SelectTextChanged
    '    If Not String.IsNullOrEmpty(cmbSch.SelectedValue) Then
    '        GetData(False, cmbSch.SelectedValue)
    '        FormatGrid()
    '        Dim dt As New DataTable
    '        Dim objattInvSchedule As New attInvSchedule
    '        Dim objBALAst_INV_Schedule1 As New BALAst_INV_Schedule
    '        objattInvSchedule.PKeyCode = cmbSch.SelectedValue
    '        dt = objBALAst_INV_Schedule1.GetAll_invSchID(cmbSch.SelectedValue)
    '        lblStart.Text = ""
    '        lblEnd.Text = ""
    '        If dt Is Nothing = False Then
    '            If dt.Rows.Count > 0 Then
    '                lblStart.Text = dt.Rows(0)("InvStartDate")
    '                lblEnd.Text = dt.Rows(0)("InvEndDate")
    '            End If
    '        End If
    '    End If
    'End Sub

End Class
