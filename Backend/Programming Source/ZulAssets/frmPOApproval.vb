Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL


Public Class frmPOApproval
    Dim objattPurchaseOrder As attPurchaseOrder
    Dim BALPurchaseOrder As New BALPurchaseOrder
    Dim objattPODetails As attPODetails
    Dim objBALPODetails As New BALPODetails
    Dim dsItems As DataTable

    Private Sub frmPOApproval_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmPOApproval = Nothing
    End Sub

    Private Sub frmPOApproved_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        Try
            Me.Get_PO_Open()
            Get_PODetails_App(-1)
            format_Grid()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmPOApproval = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
#Region "Method"

    Private Sub format_Grid()
        Dim RIItmStatus As New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
        RIItmStatus.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() _
                                                    {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Transfered", True, -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Pending", False, -1)})
        For i As Integer = 0 To grdOrdersView.Columns.Count - 1
            grdOrdersView.Columns(i).Visible = False
        Next
        GetGridColumn(grdOrdersView, "POCode").Caption = "PO Code"
        GetGridColumn(grdOrdersView, "POCode").Visible = True
        GetGridColumn(grdOrdersView, "POCode").Width = 85
        GetGridColumn(grdOrdersView, "POCode").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        GetGridColumn(grdOrdersView, "PODate").Caption = "PO Date"
        GetGridColumn(grdOrdersView, "PODate").Width = 90
        GetGridColumn(grdOrdersView, "PODate").Visible = True
        GetGridColumn(grdOrdersView, "PODate").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        GetGridColumn(grdOrdersView, "Amount").Caption = "Amount"
        GetGridColumn(grdOrdersView, "Amount").Width = 100
        GetGridColumn(grdOrdersView, "Amount").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GetGridColumn(grdOrdersView, "Amount").DisplayFormat.FormatString = "###,###,###,###,###.00"
        GetGridColumn(grdOrdersView, "Amount").Visible = True
        GetGridColumn(grdOrdersView, "Amount").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        GetGridColumn(grdOrdersView, "AddCharges").Caption = "Add. Charges"
        GetGridColumn(grdOrdersView, "AddCharges").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GetGridColumn(grdOrdersView, "AddCharges").DisplayFormat.FormatString = "###,###,###,###,###.00"
        GetGridColumn(grdOrdersView, "AddCharges").Width = 110
        GetGridColumn(grdOrdersView, "AddCharges").Visible = True
        GetGridColumn(grdOrdersView, "AddCharges").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        GetGridColumn(grdOrdersView, "Approvedby").Caption = "Approved by"
        GetGridColumn(grdOrdersView, "Approvedby").Width = 110
        GetGridColumn(grdOrdersView, "Approvedby").Visible = True


        GetGridColumn(grdOrdersView, "Preparedby").Width = 110
        GetGridColumn(grdOrdersView, "Preparedby").Caption = "Prepared by"
        GetGridColumn(grdOrdersView, "Preparedby").Visible = True

        GetGridColumn(grdOrdersView, "IsTrans").Caption = "Transfer Status"
        GetGridColumn(grdOrdersView, "IsTrans").Width = 100
        GetGridColumn(grdOrdersView, "IsTrans").ColumnEdit = RIItmStatus
        GetGridColumn(grdOrdersView, "IsTrans").Visible = True
        addGridMenu(grdOrders)

        For i As Integer = 0 To grdItemsView.Columns.Count - 1
            grdItemsView.Columns(i).Visible = False
        Next
        grdItemsView.Columns(0).Caption = "Po Item Code"
        grdItemsView.Columns(0).Width = 125
        grdItemsView.Columns(0).Visible = True
        grdItemsView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdItemsView.Columns(1).Caption = "PO Code"
        grdItemsView.Columns(1).Width = 75
        grdItemsView.Columns(1).Visible = True
        grdItemsView.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdItemsView.Columns(2).Caption = "Item Code"
        grdItemsView.Columns(2).Width = 125
        grdItemsView.Columns(2).Visible = True
        grdItemsView.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdItemsView.Columns(3).Caption = "Item Description"
        grdItemsView.Columns(3).Width = 200
        grdItemsView.Columns(3).Visible = True

        grdItemsView.Columns(4).Caption = "Item Cost"
        grdItemsView.Columns(4).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        grdItemsView.Columns(4).DisplayFormat.FormatString = "###,###,###,###,###.00"
        grdItemsView.Columns(4).Width = 150
        grdItemsView.Columns(4).Visible = True
        grdItemsView.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdItemsView.Columns(5).Visible = False
        grdItemsView.Columns(6).Caption = "Item Quantity"
        grdItemsView.Columns(6).Width = 75
        grdItemsView.Columns(6).Visible = True
        grdItemsView.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdItemsView.Columns(7).Visible = False
        addGridMenu(grdItems)
    End Sub

    Private Sub Get_PO_Open()
        Try
            objattPurchaseOrder = New attPurchaseOrder
            objattPurchaseOrder.POStatus = 1
            grdOrders.DataSource = BALPurchaseOrder.GetAll_PurchaseOrder(objattPurchaseOrder)
            grdOrdersView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
            grdOrdersView.FocusedColumn = grdOrdersView.Columns(0)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    
    Private Sub Get_PODetails_App(ByVal strID As String)
        Try
            objattPODetails = New attPODetails
            objattPODetails.POCode = strID
            Dim dt As DataTable = objBALPODetails.GetAll_PODetails(objattPODetails)
            ' add new column to allow the user check it.
            'Dim Transfercol As DataColumn = dt.Columns.Add("Transfer", System.Type.GetType("System.Boolean"))
            grdItems.DataSource = dt
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Function Update_PO(ByVal _Id As String, ByVal intStatus As Integer) As Boolean
        Try
            objattPurchaseOrder = New attPurchaseOrder

            objattPurchaseOrder.PKeyCode = _Id
            objattPurchaseOrder.POStatus = intStatus
            objattPurchaseOrder.Approvedby = AppConfig.LoginName
            If BALPurchaseOrder.UpdatePOStatus(objattPurchaseOrder) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Function

#End Region

    Private Sub btnApproved_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApproved.Click
        Try
            Dim FocRow As Integer = grdItemsView.FocusedRowHandle
            If FocRow >= 0 Then
                If ZulMessageBox.ShowMe("BeforeApproved", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    If Update_PO(GetGridRowCellValue(grdItemsView, FocRow, "POCode").ToString(), 2) Then
                        Get_PO_Open()
                        Get_PODetails_App(-1)
                        ZulMessageBox.ShowMe("Approved")
                    End If
                End If
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub grdOrdersView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdOrdersView.DoubleClick
        Dim FocRow As Integer = grdOrdersView.FocusedRowHandle
        If FocRow >= 0 Then
            Dim frm As New frmPODisplay
            frm.Id = GetGridRowCellValue(grdOrdersView, FocRow, "POCode").ToString()
            frm.ShowDialog()
        End If

    End Sub

    Private Sub grdOrdersView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdOrdersView.FocusedRowChanged
        Dim FocRow As Integer = grdOrdersView.FocusedRowHandle
        If FocRow >= 0 Then
            Get_PODetails_App(GetGridRowCellValue(grdOrdersView, FocRow, "POCode").ToString())
        End If
    End Sub
End Class