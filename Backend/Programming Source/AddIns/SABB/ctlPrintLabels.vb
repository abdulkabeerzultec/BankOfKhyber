Imports System.Windows.Forms
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports DevExpress.XtraReports.UI
Imports System.IO
Imports System.Drawing.Printing

<System.ComponentModel.DesignTimeVisible(False)> _
Public Class ctlPrintLabels
    Private popup As New frmPopup

    Private _TrnasferType As StockTransferBLL.StockTransferTypes
    Public Property TrnasferType() As StockTransferBLL.StockTransferTypes
        Get
            Return _TrnasferType
        End Get
        Set(ByVal value As StockTransferBLL.StockTransferTypes)
            _TrnasferType = value
        End Set
    End Property


    Private _StockTransferGUID As Guid
    Public Property StockTransferGUID() As Guid
        Get
            Return _StockTransferGUID
        End Get
        Set(ByVal value As Guid)
            _StockTransferGUID = value
        End Set
    End Property



    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.ParentForm.Close()
    End Sub

    Private Function GetSelectedRowsCount() As Integer
        Dim RowCount As Integer = 0
        For i As Integer = 0 To grvPrintLabels.RowCount - 1
            If grvPrintLabels.GetRowCellValue(i, "Selection") Then
                RowCount += 1
            End If
        Next
        Return RowCount
    End Function

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Dim objBALAssetDetails As New BALAssetDetails

            If grvPrintLabels.SelectedRowsCount <= 0 Then
                Exit Sub
            End If

            Dim rpt As New XtraReport
            rpt = LoadReport("Assets Barcode")
            If MessageBox.Show("You are going to print " & GetSelectedRowsCount() & " Asset Labels , Do you want to Continue ? ", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                Dim Serials As String = ""
                'Get the Asset IDs from the Selected Rows in the grid.
                For i As Integer = 0 To grvPrintLabels.RowCount - 1
                    If grvPrintLabels.GetRowCellValue(i, "Selection") Then
                        Serials += String.Format("'{0}',", GetGridRowCellValue(grvPrintLabels, i, "Barcode"))
                    End If
                Next
                Serials = Serials.Remove(Serials.Length - 1, 1)
                'Get the data for the selected Rows from the database.
                Dim dt As DataTable = objBALAssetDetails.GetAssetLabelDataBySerials(Serials)
                rpt.DataSource = dt
                rpt.Print()
                'For Each i As Integer In grvPrintLabels.GetSelectedRows
                '    Dim astID As String = GetGridRowCellValue(grvPrintLabels, i, "AstID")
                '    objBALAssetDetails = New BALAssetDetails
                '    If objBALAssetDetails.Update_LabelCount(astID) Then
                '        If GetGridRowCellValue(grvPrintLabels, i, "LabelCount").ToString = "" Then
                '            SetGridRowCellValue(grvPrintLabels, i, "LabelCount", 1)
                '        Else
                '            SetGridRowCellValue(grvPrintLabels, i, "LabelCount", CInt(GetGridRowCellValue(grvPrintLabels, i, "LabelCount")) + 1)
                '        End If
                '    End If
                'Next
                chkSelect.Checked = False
                chkSelect_CheckedChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        Finally

        End Try
    End Sub

    Private Sub chkSelect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelect.CheckedChanged
        ShowProgressDialog()
        Try
            For i As Integer = 0 To grvPrintLabels.RowCount - 1
                grvPrintLabels.SetRowCellValue(i, "Selection", chkSelect.Checked)
            Next
        Catch ex As Exception

        Finally
            CloseProgressDialog()
        End Try
    End Sub

    Public Sub ShowPopupForm()
        Add_SerialControl_To_PopupForm()
        Me.ActiveControl = grdPrintLabels
        popup.ShowDialog()
    End Sub
    Private Sub ctlPrintLabels_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objST As New StockTransferBLL
        grdPrintLabels.DataSource = objST.GetPrintingLabelData(StockTransferGUID, TrnasferType)
        grvPrintLabels.OptionsView.ShowAutoFilterRow = True
        grvPrintLabels.OptionsBehavior.Editable = True
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In grvPrintLabels.Columns
            If col.FieldName <> "Selection" Then
                col.OptionsColumn.AllowEdit = False
            End If
        Next
        With grdPrintLabels
            .UseEmbeddedNavigator = True
            .EmbeddedNavigator.Buttons.Append.Visible = False
            .EmbeddedNavigator.Buttons.Edit.Visible = False
            .EmbeddedNavigator.Buttons.EndEdit.Visible = False
            .EmbeddedNavigator.Buttons.Remove.Visible = False
            .EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        End With

    End Sub

    Public Sub Add_SerialControl_To_PopupForm()
        popup.Controls.Add(Me)
        Me.Dock = DockStyle.Fill
        popup.FormBorderStyle = Windows.Forms.FormBorderStyle.SizableToolWindow
        popup.MinimizeBox = False
        popup.MaximizeBox = False
        popup.CancelButton = Me.btnCancel
        popup.Height = "400"
        popup.Width = "600"
        popup.Text = "Print Asset Labels"
        popup.CancelButton = btnCancel
    End Sub
End Class
