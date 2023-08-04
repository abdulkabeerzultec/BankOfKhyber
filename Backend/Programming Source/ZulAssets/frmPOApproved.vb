Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class frmPOApproved
    Dim objattPurchaseOrder As attPurchaseOrder
    Dim BALPurchaseOrder As New BALPurchaseOrder
    Dim objattPODetails As attPODetails
    Dim objBALPODetails As New BALPODetails
    Dim dsItems As DataTable
    Dim dt As DataTable
    Public suppId As String
    Public SuppName As String
    Public Delegate Sub UpUI(ByVal Rowupdated As Integer, ByVal flag As Boolean, ByRef send As frmPOTrans)
    Public Updatehandle As UpUI = AddressOf UpdateUIHandler


    Private Sub frmApproved_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmApproved = Nothing
    End Sub

    Private Sub frmApproved_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Try

            Get_PO_Open()
            Get_PODetails_App(-1)
            format_Grid()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

#Region "Method"
    Private Sub format_Grid()
        Dim RIPOStatus As New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
        RIPOStatus.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() _
                                                    {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Open", 1, -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Approved", 2, -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Transfered", 3, -1)})

        Dim RIItmStatus As New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
        RIItmStatus.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() _
                                                    {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Transfered", True, -1), _
                                                     New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Pending", False, -1)})
        grdOrdersView.Columns(0).Caption = "PO Code"
        grdOrdersView.Columns(0).Width = 85
        grdOrdersView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdOrdersView.Columns(1).Visible = False
        grdOrdersView.Columns(2).Caption = "PO Date"
        grdOrdersView.Columns(2).Width = 90
        grdOrdersView.Columns(3).Caption = "Status"
        grdOrdersView.Columns(3).Visible = False
        grdOrdersView.Columns(4).Width = 100
        grdOrdersView.Columns(4).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        grdOrdersView.Columns(4).DisplayFormat.FormatString = "###,###,###,###,###.00"
        grdOrdersView.Columns(4).Caption = "Amount"
        grdOrdersView.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdOrdersView.Columns(5).Width = 100
        grdOrdersView.Columns(5).Caption = "Add. Charges"
        grdOrdersView.Columns(5).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        grdOrdersView.Columns(5).DisplayFormat.FormatString = "###,###,###,###,###.00"
        grdOrdersView.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdOrdersView.Columns(6).Visible = False
        grdOrdersView.Columns(7).Visible = False
        grdOrdersView.Columns(8).Visible = False

        grdOrdersView.Columns(9).Width = 90
        grdOrdersView.Columns(9).Caption = "Approved by"

        grdOrdersView.Columns(10).Visible = False
        grdOrdersView.Columns(10).Caption = "Prepared by"

        grdOrdersView.Columns(11).Caption = "Transfer Status"
        grdOrdersView.Columns(11).Width = 60
        grdOrdersView.Columns(11).ColumnEdit = RIPOStatus
        addGridMenu(grdOrders)
        For i As Integer = 12 To grdOrdersView.Columns.Count - 1
            grdOrdersView.Columns(i).Visible = False
        Next

        grdOrders.UseEmbeddedNavigator = True
        grdOrders.EmbeddedNavigator.Buttons.Append.Visible = False
        grdOrders.EmbeddedNavigator.Buttons.Edit.Visible = False
        grdOrders.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grdOrders.EmbeddedNavigator.Buttons.Remove.Visible = False
        grdOrders.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

        grdOrdersView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        grdOrdersView.FocusedColumn = grdOrdersView.Columns(0)


        grdItemsView.OptionsBehavior.Editable = True
        grdItemsView.Columns(0).Caption = "Po Item Code"
        grdItemsView.Columns(0).Width = 100
        grdItemsView.Columns(0).OptionsColumn.AllowEdit = False
        grdItemsView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdItemsView.Columns(1).Visible = False
        grdItemsView.Columns(1).OptionsColumn.AllowEdit = False

        grdItemsView.Columns(2).Width = 80
        grdItemsView.Columns(2).OptionsColumn.AllowEdit = False
        grdItemsView.Columns(2).Caption = "Item Code"

        grdItemsView.Columns(3).Caption = "Item Description"
        grdItemsView.Columns(3).Width = 120
        grdItemsView.Columns(3).OptionsColumn.AllowEdit = False


        grdItemsView.Columns(4).Caption = "Item Cost"
        grdItemsView.Columns(4).Width = 85
        grdItemsView.Columns(4).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        grdItemsView.Columns(4).DisplayFormat.FormatString = "###,###,###,###,###.00"
        grdItemsView.Columns(4).OptionsColumn.AllowEdit = False
        grdItemsView.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdItemsView.Columns(5).Caption = "Add. Charges"
        grdItemsView.Columns(5).Width = 85
        grdItemsView.Columns(5).OptionsColumn.AllowEdit = False
        grdItemsView.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdItemsView.Columns(6).Caption = "QTY"
        grdItemsView.Columns(6).Width = 50
        grdItemsView.Columns(6).OptionsColumn.AllowEdit = False
        grdItemsView.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdItemsView.Columns(7).Visible = False

        grdItemsView.Columns(8).Caption = "Transfer Status"
        grdItemsView.Columns(8).Width = 120
        grdItemsView.Columns(8).ColumnEdit = RIItmStatus
        grdItemsView.Columns(8).OptionsColumn.AllowEdit = False


        grdItemsView.Columns(9).Visible = False

        grdItemsView.Columns(10).Visible = False
        grdItemsView.Columns(11).Visible = False
        grdItemsView.Columns(12).Visible = False
        grdItemsView.Columns(13).Visible = False
        grdItemsView.Columns(14).Visible = False
        grdItemsView.Columns(15).Visible = False
        grdItemsView.Columns(16).Visible = False
        grdItemsView.Columns(17).Visible = False
        grdItemsView.Columns(18).Visible = False
        grdItemsView.Columns(19).Visible = False
        grdItemsView.Columns(20).Visible = False
        grdItemsView.Columns(21).Visible = False
        grdItemsView.Columns(22).Visible = False

        grdItemsView.Columns(23).Width = 75
        grdItemsView.Columns(23).Caption = "Received Qty"
        grdItemsView.Columns(23).OptionsColumn.AllowEdit = False
        grdItemsView.Columns(23).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        grdItemsView.Columns(24).Caption = "Transfer"
        grdItemsView.Columns(24).Width = 60
        grdItemsView.Columns(24).OptionsColumn.AllowEdit = True

        grdItems.UseEmbeddedNavigator = True
        grdItems.EmbeddedNavigator.Buttons.Append.Visible = False
        grdItems.EmbeddedNavigator.Buttons.Edit.Visible = False
        grdItems.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grdItems.EmbeddedNavigator.Buttons.Remove.Visible = False
        grdItems.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        addGridMenu(grdItems)
    End Sub

    Private Sub Get_PO_Open()
        Try
            objattPurchaseOrder = New attPurchaseOrder
            objattPurchaseOrder.POStatus = 2

            grdOrders.DataSource = BALPurchaseOrder.GetAll_PurchaseOrder(objattPurchaseOrder)
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
            Dim Transfercol As DataColumn = dt.Columns.Add("TRANSFER", System.Type.GetType("System.Boolean"))
            grdItems.DataSource = dt

            For i As Integer = 0 To grdItemsView.RowCount - 1
                SetGridRowCellValue(grdItemsView, i, "TRANSFER", False)
            Next

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub Get_Items(ByVal strPOCode As String)
        Try
            objattPODetails = New attPODetails
            objattPODetails.POCode = strPOCode
            dsItems = objBALPODetails.GetAll_PODetails(objattPODetails)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub Update_PO(ByVal _Id As String, ByVal intStatus As Integer)
        Try
            objattPurchaseOrder = New attPurchaseOrder
            objattPurchaseOrder.PKeyCode = _Id
            objattPurchaseOrder.POStatus = intStatus
            objattPurchaseOrder.Approvedby = AppConfig.LoginName
            BALPurchaseOrder.UpdatePOStatus(objattPurchaseOrder)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Public Sub UpdatePODetails(ByVal rowNum As Integer, ByVal recQty As Integer)
        SetGridRowCellValue(grdItemsView, rowNum, "PORecQty", recQty)
    End Sub
    Public Sub UpdateUIHandler(ByVal Rowupdation As Integer, ByVal flag As Boolean, ByRef sender As frmPOTrans)
        Try
            SetGridRowCellValue(grdItemsView, Rowupdation, "IsTrans", flag)

            Dim openform As Boolean = True
            For i As Integer = 0 To grdItemsView.RowCount - 1
                If GetGridRowCellValue(grdItemsView, i, "IsTrans") = False Then
                    openform = False
                End If
            Next

            If openform Then
                Dim i As Integer = grdOrdersView.FocusedRowHandle
                SetGridRowCellValue(grdOrdersView, i, "POStatus", 3)
                Dim objattPurchaseOrder As New attPurchaseOrder
                objattPurchaseOrder.PKeyCode = GetGridRowCellValue(grdOrdersView, i, "POCode").ToString
                BALPurchaseOrder.Transfer(objattPurchaseOrder)
                sender.Dispose()
                Me.BringToFront()
                ZulMessageBox.ShowMe("PoCompTransfer")
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

#End Region


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmApproved = Nothing
            Me.Close()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransfer.Click
        Try
            Dim cyclCount As Integer = 0
            Dim openform As Boolean = False
            For i As Integer = 0 To grdItemsView.RowCount - 1
                If GetGridRowCellValue(grdItemsView, i, "TRANSFER") = True And GetGridRowCellValue(grdItemsView, i, "IsTrans") = False And CInt(GetGridRowCellValue(grdItemsView, i, "POItmQty")) > 0 Then
                    cyclCount = cyclCount + 1
                    openform = True
                End If
            Next

            If openform Then
                Dim frm As New frmPOTrans
                frm.MdiParent = Me.MdiParent
                frm.CycleCount = cyclCount
                frm.StartPosition = FormStartPosition.CenterScreen
                frm.Sendfrm = Me
                frm.Show()
            Else
                ZulMessageBox.ShowMe("AtLeastPOError")
            End If

            dt = New DataTable

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
            suppId = GetGridRowCellValue(grdOrdersView, FocRow, "SuppID").ToString
        End If
    End Sub
End Class