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
        strQuery.Append("SELECT      AssetDetails.AstNum, AssetDetails.RefNo,AssetDetails.AstID,AssetDetails.AstDesc, AssetDetails.AstDesc2, Companies.CompanyCode, Category.CatFullPath,'' as MainCat,'' as SubCat,Assets.AstDesc as SubCat1,  Location.LocationFullPath,'' as MainLoc,'' as SubLoc,'' as SubLoc1,AssetDetails.SrvDate as ServiceDate,AssetDetails.PurDate as PurchasedDate,AssetDetails.BaseCost as Cost,AssetDetails.LabelCount,AssetDetails.SerailNo,AssetDetails.AstModel, Custodian.CustodianID, Custodian.CustodianName,AssetDetails.InvStatus,GLCodes.GLCode,GLCodes.GLDesc,'' as Status")
        strQuery.Append(" FROM         AssetDetails INNER JOIN")
        strQuery.Append(" Location ON Location.LocID = AssetDetails.LocID INNER JOIN")
        strQuery.Append(" Assets ON AssetDetails.ItemCode = Assets.ItemCode INNER JOIN")
        strQuery.Append(" Companies ON AssetDetails.CompanyID = Companies.CompanyId INNER JOIN")
        strQuery.Append(" Custodian ON AssetDetails.CustodianID = Custodian.CustodianID INNER JOIN")
        strQuery.Append(" Category ON Assets.AstCatID = Category.AstCatID")
        strQuery.Append(" left outer join GLCodes on AssetDetails.GLCode = GLCodes.GLCode")
        strQuery.Append(" left outer join Brand on AssetDetails.AstBrandID = Brand.AstBrandID")
        strQuery.Append(" WHERE     AssetDetails.IsDeleted = 0")
        SQLcmd.CommandText = strQuery.ToString
        Try

            ExportDataTable = GenericDAL.DBOperations.ExecuteReader(SQLcmd)
            For Each row As DataRow In ExportDataTable.Rows

                Dim LocationFullCode As String = row("LocationFullPath")
                Dim LocCodeArr As String() = LocationFullCode.Split("\")
                If LocCodeArr.Length > 0 Then
                    row("MainLoc") = LocCodeArr(0).Trim
                End If
                If LocCodeArr.Length > 1 Then
                    row("SubLoc") = LocCodeArr(1).Trim
                End If
                If LocCodeArr.Length > 2 Then
                    row("SubLoc1") = LocCodeArr(2).Trim
                End If

                Dim CatFullCode As String = row("CatFullPath")
                Dim CatCodeArr As String() = CatFullCode.Split("\")
                If CatCodeArr.Length > 0 Then
                    row("MainCat") = CatCodeArr(0).Trim
                End If
                If CatCodeArr.Length > 1 Then
                    row("SubCat") = CatCodeArr(1).Trim
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

            ExportDataTable.Columns.Remove("CatFullPath")
            ExportDataTable.Columns.Remove("LocationFullPath")
            ExportDataTable.Columns.Remove("InvStatus")
            ExportDataTable.AcceptChanges()
            grd.DataSource = ExportDataTable
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FormatGrid()
        grdView.BestFitColumns()
        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
    End Sub


    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If MessageBox.Show("Are you sure to export the data?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            ExportData(ExportFolderName, pb, False)
        End If
    End Sub

    Public Function ExportData(ByVal ExportFolderName As String, ByVal pb As DevExpress.XtraEditors.ProgressBarControl, ByVal SilentProcess As Boolean) As Boolean
        fileName = String.Format("{0}\Exp_{1}{2}{3}.xls", ExportFolderName, Now.Year, Now.Month.ToString.PadLeft(2, "0"), Now.Day.ToString.PadLeft(2, "0"))
        grd.ExportToXls(fileName)
        If Not SilentProcess Then
            Dim msg As String = "Export Completed, Exported Assets Count = " & grdView.DataRowCount & "."
            MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
