Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Windows.Forms

Public Class frmAnonymousRemarks

    Private Sub frmAnonymousRemarks_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG

    End Sub

    Private Sub cmbSch_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSch.LovBtnClick
        Try
            Dim objBALAst_INV_Schedule As New BALAst_INV_Schedule
            cmbSch.ValueMember = "InvSchCode"
            cmbSch.DisplayMember = "InvDesc"
            cmbSch.DataSource = objBALAst_INV_Schedule.Getcombo_Schedule()
            cmbSch.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmbSch_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSch.SelectTextChanged
        If Not String.IsNullOrEmpty(cmbSch.SelectedText) Then
            GetAnonymousAssets(cmbSch.SelectedValue)
        End If
    End Sub

    Private Sub GetAnonymousAssets(ByVal InvSchID As Integer)
        Dim objBALIntegration As New BALABBIntegration
        Dim objBALLocation As New BALLocation
        Dim objBALDevices As New BALDevices
        Dim objBALCategory As New BALCategory

        Dim dt As DataTable = objBALIntegration.GetAll_AnonymousAssetRemarks(InvSchID)

        Dim dcCurrentPlant As DataColumn = dt.Columns.Add("Plant", Type.GetType("System.String"))
        Dim dcCurrentLoc As DataColumn = dt.Columns.Add("Location", Type.GetType("System.String"))
        Dim dcCat As DataColumn = dt.Columns.Add("Class", Type.GetType("System.String"))
        Dim dcDeviceDesc As DataColumn = dt.Columns.Add("DeviceDesc", Type.GetType("System.String"))
        For i As Integer = 0 To dt.Rows.Count - 1

            If Not String.IsNullOrEmpty(dt.Rows(i)("LocID").ToString) Then
                Dim Fulllocation As String = objBALLocation.GetLocFullPath(dt.Rows(i)("LocID").ToString)
                Dim strFulLoc As String() = Fulllocation.Split("\")
                If strFulLoc(0) = "UnKnown" Then
                    dt.Rows(i)("Plant") = String.Empty
                Else
                    dt.Rows(i)("Plant") = strFulLoc(0).Trim
                End If

                If strFulLoc.Length > 1 Then
                    If strFulLoc(1) = "UnKnown" Then
                        dt.Rows(i)("Location") = String.Empty
                    Else
                        dt.Rows(i)("Location") = strFulLoc(1).Trim
                    End If
                Else
                    dt.Rows(i)("Location") = String.Empty
                End If

            End If
            dt.Rows(i)("Class") = objBALCategory.GetCatFullPath(dt.Rows(i)("AstCatID").ToString)
            dt.Rows(i)("DeviceDesc") = objBALDevices.GetDeviceDescription(dt.Rows(i)("DeviceID").ToString)
        Next
        grdView.Columns.Clear()
        grd.DataSource = dt

        FormatGridAnonymousAssetsABB()

    End Sub

    Private Sub FormatGridAnonymousAssetsABB()
        With grdView
            .OptionsBehavior.Editable = True
            For Each col As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                col.OptionsColumn.AllowEdit = False
            Next
            .Columns("LocID").Visible = False
            .Columns("NonBCode").Visible = False
            .Columns("AstCatID").Visible = False
            .Columns("DeviceID").Visible = False

            .Columns("Remarks").VisibleIndex = .Columns.Count - 1
            .Columns("Remarks").OptionsColumn.AllowEdit = True
            .Columns("Remarks").MinWidth = 125

        End With
    End Sub
    Private Function Process_AnonymousData() As Boolean
        For i As Integer = 0 To grdView.RowCount - 1
            Dim AssetDesc As String = grdView.GetRowCellValue(i, "Description").ToString
            Dim AnonymousId As String = grdView.GetRowCellValue(i, "NonBCode").ToString
            Dim DeviceID As String = grdView.GetRowCellValue(i, "DeviceID").ToString
            Dim TransDate As DateTime = CDate(grdView.GetRowCellValue(i, "HisDate"))
            Dim serial As String = grdView.GetRowCellValue(i, "SerailNo").ToString
            Dim LocID As String = grdView.GetRowCellValue(i, "LocID").ToString
            Dim CatID As String = grdView.GetRowCellValue(i, "AstCatID").ToString
            Dim Remarks As String = grdView.GetRowCellValue(i, "Remarks").ToString
            Dim InvSchID As Integer = -1
            Dim objAnon As New BALAnonymousAsset
            objAnon.Update_AnonymousRemarks(AnonymousId, DeviceID, TransDate, Remarks, InvSchID)
        Next
        MessageBox.Show("Data Saved Sucessfully", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Process_AnonymousData()
    End Sub

    Private Sub btnPrintPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintPreview.Click
        With SaveFileDialog1
            .CheckPathExists = True
            .FileName = ""
            .DefaultExt = "xls"
            .Filter = "Excel Sheet (*.xls)|*.xls"
            .Title = "Excel Sheet File"

            If .ShowDialog() = DialogResult.OK Then
                grdView.ExportToXls(.FileName)
            End If
            .Dispose()
        End With
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteAnonymousData()
    End Sub

    Private Sub DeleteAnonymousData()
        Dim FocRow As Integer = grdView.FocusedRowHandle
        If FocRow >= 0 Then
            Dim id As String = grdView.GetRowCellValue(FocRow, "NonBCode").ToString
            Dim DeviceID As String = grdView.GetRowCellValue(FocRow, "DeviceID").ToString
            Dim TransDate As DateTime = CDate(grdView.GetRowCellValue(FocRow, "HisDate"))
            Dim objatt As New BALAnonymousAsset

            If MessageBox.Show("Do you really want to delete this record ?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If objatt.Delete_AnonymousAsset(id, DeviceID, TransDate) Then
                    grdView.DeleteRow(FocRow)
                    'GetAnonymousAssets()
                    ShowInfoMessage("Record deleted successfully")
                End If
            End If
        Else
            ShowErrorMessage("No Record(s) selected.")
        End If
    End Sub


End Class