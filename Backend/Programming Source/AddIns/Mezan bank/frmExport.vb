Imports DevExpress.XtraGrid
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.Text

Public Class frmExport

    Private _ExportFolderName As String = ""
    Public Property ExportFolderName() As String
        Get
            Return _ExportFolderName
        End Get
        Set(ByVal value As String)
            _ExportFolderName = value
        End Set
    End Property

    Dim fileName As String = ""
    Dim ExportDataTable As DataTable
    Private Sub frmExport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.WindowState = FormWindowState.Maximized
        GetData()
        FormatGrid()
    End Sub
    Private Function ResetDataChangeFlag() As Boolean
        Try
            Dim SQLcmd As New SqlCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetDetails set IsDataChanged = 0 where IsDataChanged = 1")
            SQLcmd.CommandText = strQuery.ToString
            GenericDAL.DBOperations.ExecuteNonQuery(SQLcmd)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub GetData()
        Dim SQLcmd As New SqlCommand
        Dim strQuery As New StringBuilder
        strQuery.Append("SELECT      Companies.CompanyCode,AssetDetails.RefNo, AssetDetails.AstID,Category.Code as AssetClass, AssetDetails.AstDesc, AssetDetails.SerailNo,CostCenter.CostNumber,CostCenter.CostID,Location.CompCode,'' as Plant,'' as Location,Custodian.CustodianID as PersNo,AssetDetails.InvStatus,'' as Status")
        strQuery.Append(" FROM         AssetDetails INNER JOIN")
        strQuery.Append(" Location ON Location.LocID = AssetDetails.LocID INNER JOIN")
        strQuery.Append(" Assets ON AssetDetails.ItemCode = Assets.ItemCode INNER JOIN")
        strQuery.Append(" Companies ON AssetDetails.CompanyID = Companies.CompanyId INNER JOIN")
        strQuery.Append(" Custodian ON AssetDetails.CustodianID = Custodian.CustodianID INNER JOIN")
        strQuery.Append(" CostCenter ON AssetDetails.CostCenterID = CostCenter.CostID INNER JOIN")
        strQuery.Append(" Category ON Assets.AstCatID = Category.AstCatID")
        'strQuery.Append(" WHERE     AssetDetails.IsDeleted = 0 and IsDataChanged = 1  and (convert(varchar,LastEditDate,103) = convert(varchar,getdate(),103) or convert(varchar,CreationDate,103) = convert(varchar,getdate(),103))")
        strQuery.Append(" WHERE     AssetDetails.IsDeleted = 0 and IsDataChanged = 1  ")
        SQLcmd.CommandText = strQuery.ToString
        'String.Format("{0}-{1}-{2}", CompanyCode, AssetNumber, SubNumber)
        Try

            ExportDataTable = GenericDAL.DBOperations.ExecuteReader(SQLcmd)

            ExportDataTable.Columns.Add("AssetNo", Type.GetType("System.String"))
            ExportDataTable.Columns("AssetNo").SetOrdinal(1)

            ExportDataTable.Columns.Add("SubAssetNo", Type.GetType("System.String"))
            ExportDataTable.Columns("SubAssetNo").SetOrdinal(2)

            For Each row As DataRow In ExportDataTable.Rows
                If row("AstDesc").ToString = "Empty" Then
                    row("AstDesc") = String.Empty
                End If
                Dim RefNo As String = row("RefNo")
                Dim Arr As String() = RefNo.Split("-")

                If Arr.Length > 2 Then
                    row("AssetNo") = Arr(1).Trim
                    row("SubAssetNo") = Arr(2).Trim
                End If

                Dim LocationFullCode As String = row("CompCode")
                'If Location value is Unknown then make it blank before export the data
                If LocationFullCode = "1" Then
                    row("Plant") = ""
                    row("Location") = ""
                Else
                    Dim LocCodeArr As String() = LocationFullCode.Split("\")
                    If LocCodeArr.Length > 0 Then
                        row("Plant") = LocCodeArr(0).Trim
                    End If
                    If LocCodeArr.Length > 1 Then
                        row("Location") = LocCodeArr(1).Trim
                    End If
                End If

                If row("PersNo") = "1" Then
                    row("PersNo") = ""
                End If

                If row("CostID") = "1" Then
                    row("CostNumber") = ""
                End If

                Dim Status As Integer = row("InvStatus")
                Select Case Status
                    Case 0
                        row("Status") = "Missing"
                    Case 1
                        row("Status") = "Found"
                    Case 2
                        row("Status") = "Misplaced"
                    Case 3
                        row("Status") = "Transferred"
                    Case 5
                        row("Status") = "Anonymous"
                End Select
                row.AcceptChanges()
            Next

            ExportDataTable.Columns.Remove("CostID")
            ExportDataTable.Columns.Remove("AstID")
            ExportDataTable.Columns.Remove("CompCode")
            ExportDataTable.Columns.Remove("RefNo")
            ExportDataTable.Columns.Remove("InvStatus")
            ExportDataTable.AcceptChanges()
            grd.DataSource = ExportDataTable
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FormatGrid()
        grdView.BestFitColumns()

        'grdView.Columns("AstID").Caption = "Asset Tag"
        'grdView.Columns("CatFullPath").Visible = False
        'grdView.Columns("LocationFullPath").Visible = False
        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
    End Sub

    Private Function ExportDatasetToCsv(ByVal MyDatatable As DataTable, ByVal pb As DevExpress.XtraEditors.ProgressBarControl, ByVal FileName As String, ByVal SilentExport As Boolean) As Boolean
        If MyDatatable.Rows.Count > 0 Then

            Dim dr As DataRow
            Dim myString As String
            Dim bFirstRecord As Boolean = True
            Dim myWriter As New System.IO.StreamWriter(FileName, False, System.Text.Encoding.GetEncoding("windows-1256"))
            Try
                myString = ""
                pb.Visible = True
                pb.Position = 1
                pb.Properties.Step = 1
                pb.Properties.Maximum = grdView.DataRowCount
                Application.DoEvents()
                Try
                    For Each dr In MyDatatable.Rows
                        bFirstRecord = True
                        For Each field As Object In dr.ItemArray
                            If Not bFirstRecord Then
                                myString += Chr(Keys.Tab)
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
                    Return False
                Finally
                    pb.Visible = False
                End Try
                'Write the String to the Csv File
                myWriter.Write(myString)
                Dim msg As String = "Export Completed, Exported Assets Count = " & grdView.DataRowCount & "."
                SaveToLogFile(msg, False)
                If Not SilentExport Then
                    MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                Return True

            Catch ex As Exception
                SaveToLogFile(ex.Message, False)
                If Not SilentExport Then
                    MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Return False
            Finally
                'Clean up
                myWriter.Close()
            End Try
        Else ' If there is no record to export.
            Dim msg As String = "No data to export, Exported Assets Count = 0."
            SaveToLogFile(msg, False)
            If Not SilentExport Then
                MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            Return False
        End If
    End Function

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If MessageBox.Show("Are you sure to export the data?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            ExportData(ExportFolderName, pb, False)
        End If
    End Sub

    Public Function ExportData(ByVal ExportFolderName As String, ByVal pb As DevExpress.XtraEditors.ProgressBarControl, ByVal SilentProcess As Boolean) As Boolean
        FileName = String.Format("{0}\Exp_{1}{2}{3}.txt", ExportFolderName, Now.Year, Now.Month.ToString.PadLeft(2, "0"), Now.Day.ToString.PadLeft(2, "0"))
        If ExportDatasetToCsv(ExportDataTable, pb, FileName, SilentProcess) Then
            Dim ObjAssetsDetails As New BALAssetDetails
            ObjAssetsDetails.DisableAssetTriggers() 'Disallow the logging tringgers, because huge data will come from SetAssetWithValue
            ResetDataChangeFlag()
            ObjAssetsDetails.EnabbleAssetTriggers()
            GetData()
        End If
    End Function


    Public Function SilentExport(ByVal ExportFolderName As String, ByVal pb As DevExpress.XtraEditors.ProgressBarControl) As Boolean
        GetData()
        ExportData(ExportFolderName, pb, True)
    End Function
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class
