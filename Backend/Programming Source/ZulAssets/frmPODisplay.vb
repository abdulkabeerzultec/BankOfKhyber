Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Public Class frmPODisplay

    Dim objattPurchaseOrder As attPurchaseOrder
    Dim BALPurchaseOrder As New BALPurchaseOrder
    Dim objattPODetails As attPODetails
    Dim objBALPODetails As New BALPODetails
    Public Id As String

    Private Sub frmPODisplay_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmPODisplay = Nothing
    End Sub

    Private Sub frmPODisplay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Get_PO_OPen(Id)
        Get_PODetails_Prepared(Id)
    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub Get_PO_OPen(ByVal _id As String)
        Try
            Dim ds As New DataTable
            objattPurchaseOrder = New attPurchaseOrder
            ' objattPurchaseOrder.POStatus = 1
            objattPurchaseOrder.PKeyCode = _id
            ds = BALPurchaseOrder.GetAll_PurchaseOrder(objattPurchaseOrder)
            If ds Is Nothing = False Then
                'If ds.Tables.Count > 0 Then
                If ds.Rows.Count > 0 Then
                    Dim dt As DataTable = ds
                    txtQuot.Text = dt.Rows(0)("Quotation").ToString()
                    txtAmount.Text = dt.Rows(0)("Amount").ToString()
                    txtMode.Text = dt.Rows(0)("ModeDelivery").ToString()
                    txtPTerms.Text = dt.Rows(0)("Payterm").ToString()
                    txtRemarks.Text = dt.Rows(0)("Remarks").ToString()
                    txtAppr1.Text = dt.Rows(0)("Approvedby").ToString()
                    txtUser.Text = dt.Rows(0)("PreparedBy").ToString()
                    txtdtDate.Text = CType(dt.Rows(0)("PODate"), Date).ToString()
                    'dtDate.Text = CType(dt.Rows(0)("PODate"), Date).ToString("dd/MM/yyyy")
                    txtSuppName.Text = dt.Rows(0)("SuppName").ToString()
                    txtPO.Text = _id

                    If dt.Rows(0)("POStatus").ToString() = "1" Then
                        txtStatus.Text = "Open"
                    ElseIf dt.Rows(0)("POStatus").ToString() = "2" Then
                        txtStatus.Text = "Approved"
                    End If

                    ' Dim objatt As New attSupplier
                    ' Dim objattbal As New BALSupplier
                    '     objatt.PKeyCode = 
                    'End If
                    txtAddCharges.Text = dt.Rows(0)("AddCharges").ToString()
                End If
            End If
            ' End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub
    Private Function Get_Items(ByVal id As String) As DataTable
        Try
            objattPODetails = New attPODetails
            objattPODetails.POCode = id
            Return objBALPODetails.GetAll_PODetails(objattPODetails)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
        Return Nothing
    End Function

    Private Sub Get_PODetails_Prepared(ByVal strID As String)
        Try
            grd.DataSource = Me.Get_Items(strID)
            FormatItemGrid()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub FormatItemGrid()
        grdView.Columns(0).Caption = "PO Item Code"
        grdView.Columns(0).Width = 100
        grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(1).Caption = "PO Code"
        grdView.Columns(1).Width = 75
        grdView.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(2).Caption = "Item Code"
        grdView.Columns(2).Width = 75
        grdView.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(3).Caption = "Item Description "
        grdView.Columns(3).Width = 350
        grdView.Columns(4).Caption = "Item Base Cost"
        grdView.Columns(4).Width = 100
        grdView.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grdView.Columns(4).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        grdView.Columns(4).DisplayFormat.FormatString = "###,###,###,###,###.00"
        grdView.Columns(5).Visible = False
        grdView.Columns(6).Visible = False
        grdView.Columns(7).Visible = False
        grdView.Columns(8).Visible = False
        grdView.Columns(9).Visible = False
        grdView.Columns(10).Visible = False
        grdView.Columns(11).Visible = False
        grdView.Columns(12).Visible = False
        grdView.Columns(13).Visible = False
        grdView.Columns(14).Visible = False
        grdView.Columns(15).Visible = False
        grdView.Columns(16).Visible = False
        grdView.Columns(17).Visible = False
        grdView.Columns(18).Visible = False
        grdView.Columns(19).Visible = False
        grdView.Columns(20).Visible = False
        grdView.Columns(21).Visible = False
        grdView.Columns(22).Visible = False
        grdView.Columns(23).Visible = False

        addGridMenu(grd)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub
End Class