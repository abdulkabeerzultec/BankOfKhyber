Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Windows.Forms

<System.ComponentModel.DesignTimeVisible(False)> _
Public Class ctlSerialNumber
    Private popup As New frmPopup
    Private RitmSerialNumber As New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Private StockTransferItemGUID As Guid
    Private _StockTransferItemRow As DataRow
    Dim SerialNumber_DT As New DataTable
    Dim objattAssetDetails As attAssetDetails
    Dim objBALAssetDetails As New BALAssetDetails



    Private _TransferType As StockTransferBLL.StockTransferTypes
    Public Property TransferType() As StockTransferBLL.StockTransferTypes
        Get
            Return _TransferType
        End Get
        Set(ByVal value As StockTransferBLL.StockTransferTypes)
            _TransferType = value
        End Set
    End Property


    Private _UserGUID As Guid
    Public Property UserGUID() As Guid
        Get
            Return _UserGUID
        End Get
        Set(ByVal value As Guid)
            _UserGUID = value
        End Set
    End Property


    Public Enum MessageReturn
        MSG_OK = 1
        MSG_Cancel = 0
    End Enum
    Public MessageResult As MessageReturn

    Public Property StockTransferItemRow() As DataRow
        Get
            Return _StockTransferItemRow
        End Get
        Set(ByVal value As DataRow)
            _StockTransferItemRow = value
        End Set
    End Property


    Private _TransStatus As String
    Public Property TransStatus() As String
        Get
            Return _TransStatus
        End Get
        Set(ByVal value As String)
            _TransStatus = value
        End Set
    End Property


   Private Sub ctlSerialNumber_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        grdSerial.UseEmbeddedNavigator = True
        grdSerial.EmbeddedNavigator.Buttons.Append.Visible = False
        grdSerial.EmbeddedNavigator.Buttons.Remove.Visible = False
        grdSerial.EmbeddedNavigator.Buttons.Edit.Visible = False
    End Sub

    Public Sub Add_SerialControl_To_PopupForm()
        popup.Controls.Add(Me)
        Me.Dock = DockStyle.Fill
        popup.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        popup.MinimizeBox = False
        popup.MaximizeBox = False
        popup.CancelButton = Me.btnCancel
        popup.Height = "400"
        popup.Width = "600"
        popup.FormBorderStyle = FormBorderStyle.None
        popup.Text = "Serial Numbers"
        popup.CancelButton = btnCancel
    End Sub
    Public Sub ShowPopupForm(ByVal TransferStatus As String, ByVal TransType As StockTransferBLL.StockTransferTypes)
        LayoutControl1.AllowCustomizationMenu = False

        TransStatus = TransferStatus
        StockTransferItemGUID = StockTransferItemRow.Item("GUID")
        TransferType = TransType
        Dim objItems As New ItemsBLL

        Add_SerialControl_To_PopupForm()
        Dim SerialNumber_Row As DataRow
        SerialNumber_DT.Columns.Add("#", System.Type.GetType("System.Int32"))
        SerialNumber_DT.Columns.Add("OldSerialNumber", System.Type.GetType("System.String"))
        SerialNumber_DT.Columns.Add("SerialNumber", System.Type.GetType("System.String"))
        SerialNumber_DT.Columns.Add("ItemDetailGUID", System.Type.GetType("System.Guid"))
        SerialNumber_DT.Columns.Add("Validation", System.Type.GetType("System.String"))

        Try
            ShowProgressDialog()
            Dim objSTItemDetail As New StockTransferItemDetails
            Dim DT As DataTable = objSTItemDetail.GetDataByStockTransferItemGUID(StockTransferItemGUID)

            For i As Integer = 0 To StockTransferItemRow.Item("QTY") - 1
                SerialNumber_Row = SerialNumber_DT.NewRow
                SerialNumber_Row("#") = i + 1
                SerialNumber_DT.Rows.Add(SerialNumber_Row)
                If DT IsNot Nothing AndAlso i < DT.Rows.Count Then
                    SerialNumber_Row("SerialNumber") = DT.Rows(i)("SerialNumber")
                    SerialNumber_Row("OldSerialNumber") = DT.Rows(i)("OldSerialNumber").ToString
                    SerialNumber_Row("ItemDetailGUID") = DT.Rows(i)("GUID")
                    SerialNumber_Row("Validation") = ""
                    If TransferStatus <> "Posted" Then
                        If SerialNumber_Row("SerialNumber") = "" Then
                            SerialNumber_Row("Validation") = "SN can't be empty"
                        Else
                            objattAssetDetails = New attAssetDetails
                            objattAssetDetails.SerailNo = SerialNumber_Row("SerialNumber")
                            Dim dtRepeatedSN As DataTable = objBALAssetDetails.GetAstIDBySerialNum(objattAssetDetails)
                            If dtRepeatedSN IsNot Nothing AndAlso dtRepeatedSN.Rows.Count > 0 Then
                                SerialNumber_Row("Validation") = "Asset already exists for SN"
                            End If
                        End If

                    End If

                Else
                    SerialNumber_Row("SerialNumber") = String.Empty
                    SerialNumber_Row("ItemDetailGUID") = Guid.Empty
                    SerialNumber_Row("OldSerialNumber") = String.Empty
                    SerialNumber_Row("Validation") = "SN can't be empty"
                End If
             
            Next
        Catch ex As Exception
            Throw ex
        Finally
            CloseProgressDialog()
        End Try

        grdSerial.DataSource = SerialNumber_DT
        grvSerial.Columns("SerialNumber").ColumnEdit = RitmSerialNumber
        grvSerial.Columns("ItemDetailGUID").Visible = False
        grvSerial.Columns("Validation").OptionsColumn.AllowEdit = False

        Dim styleCondition As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition()
        styleCondition.Column = grvSerial.Columns("Validation")
        styleCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.NotEqual
        styleCondition.Value1 = ""
        ' Modify the appearance settings 
        styleCondition.Appearance.ForeColor = System.Drawing.Color.Red
        styleCondition.Appearance.Options.UseBackColor = True
        styleCondition.Appearance.Options.UseFont = True
        styleCondition.Appearance.Options.UseForeColor = True
        ' Add the style condition to the collection 
        grvSerial.FormatConditions.Add(styleCondition)


        RemoveHandler RitmSerialNumber.EditValueChanged, AddressOf SerialNumber_EditValueChanged
        AddHandler RitmSerialNumber.EditValueChanged, AddressOf SerialNumber_EditValueChanged

        grvSerial.Columns("#").Width = 50
        grvSerial.Columns("#").MaxWidth = 70
        grvSerial.Columns("#").OptionsColumn.AllowEdit = False
        grvSerial.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        grvSerial.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

        txtItemName.Text = StockTransferItemRow.Item("AstDesc")
        txtItemCode.Text = StockTransferItemRow.Item("ItemCode")
        txtSP.Text = StockTransferItemRow.Item("Price")
        grdSerial.Focus()
        Me.ActiveControl = grdSerial
        grvSerial.FocusedColumn = grvSerial.Columns("SerialNumber")
        grvSerial.MoveLast()

        If TransferStatus = "Posted" Then
            LayoutAutoGen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            LayoutClear.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            LayoutOk.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If



        Dim RitmOldSerial As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
        grvSerial.Columns("OldSerialNumber").ColumnEdit = RitmOldSerial
        grvSerial.Columns("OldSerialNumber").OptionsColumn.AllowEdit = True

        RitmOldSerial.View.OptionsView.ShowIndicator = False
        RitmOldSerial.View.OptionsView.ShowAutoFilterRow = True
        RitmOldSerial.DataSource = objItems.GetSerialNumberByItemCode()
        RitmOldSerial.DisplayMember = GetGridColumnName("SerialNumber")
        RitmOldSerial.ValueMember = GetGridColumnName("SerialNumber")
        RitmOldSerial.NullText = String.Empty
        RitmOldSerial.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        RitmOldSerial.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never
        RitmOldSerial.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.StartsWith
        RitmOldSerial.ValidateOnEnterKey = True

        If TransType = StockTransferBLL.StockTransferTypes.DirectReceiving Then
            grvSerial.Columns("OldSerialNumber").Visible = True
        Else
            grvSerial.Columns("OldSerialNumber").Visible = False
        End If

        popup.ShowDialog()
    End Sub

    Private Sub SerialNumber_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FocRow As Integer = grvSerial.FocusedRowHandle
        Dim obj As Object = CType(sender, DevExpress.XtraEditors.TextEdit).EditValue
    End Sub

    Private Sub grvSerial_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles grvSerial.InvalidRowException
        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError
        e.WindowCaption = "Input Error"
        If grvSerial.GetColumnError(grvSerial.Columns("SerialNumber")) <> "" Then
            e.ErrorText = grvSerial.GetColumnError(grvSerial.Columns("SerialNumber")) & ", "
        ElseIf grvSerial.GetColumnError(grvSerial.Columns("OldSerialNumber")) <> "" Then
            e.ErrorText = grvSerial.GetColumnError(grvSerial.Columns("OldSerialNumber")) & ", "
        End If
    End Sub

    Private Sub grvSerial_ValidateRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles grvSerial.ValidateRow
        Try
            objattAssetDetails = New attAssetDetails
            objattAssetDetails.SerailNo = grvSerial.GetRowCellValue(e.RowHandle, "SerialNumber")
            If Not String.IsNullOrEmpty(objattAssetDetails.SerailNo) Then
                If grvSerial.GetRowCellValue(e.RowHandle, "OldSerialNumber").ToString <> grvSerial.GetRowCellValue(e.RowHandle, "SerialNumber").ToString Then
                    Dim dt As DataTable = objBALAssetDetails.GetAstIDBySerialNum(objattAssetDetails)
                    If dt.Rows.Count > 0 Then
                        'grvSerial.SetColumnError(grvSerial.Columns("SerialNumber"), "Asset already exists for SN")
                        grvSerial.SetRowCellValue(e.RowHandle, "Validation", "Asset already exists for SN")
                    Else
                        grvSerial.SetRowCellValue(e.RowHandle, "Validation", "")
                        'e.Valid = False
                    End If

                    Dim objSTItemDetail As New StockTransferItemDetails
                    Dim SNCount As Integer = objSTItemDetail.GetSerialRepeatedCount(StockTransferItemGUID, grvSerial.GetRowCellValue(e.RowHandle, "SerialNumber"))
                    If SNCount > 0 Then
                        grvSerial.SetRowCellValue(e.RowHandle, "Validation", "SN already used")
                    End If

                    'Check if the serial is exists in different line item.

                Else
                    grvSerial.SetRowCellValue(e.RowHandle, "Validation", "")
                End If

                Dim focusedRowHandel As Integer = grvSerial.LocateByValue(0, grvSerial.Columns("SerialNumber"), grvSerial.GetRowCellValue(e.RowHandle, "SerialNumber"))
                If focusedRowHandel <> e.RowHandle Then
                    'grvSerial.SetColumnError(grvSerial.Columns("SerialNumber"), "Serial Number is repeated")
                    grvSerial.SetRowCellValue(e.RowHandle, "Validation", "SN is repeated")
                    'e.Valid = False
                End If

                focusedRowHandel = grvSerial.LocateByValue(e.RowHandle + 1, grvSerial.Columns("SerialNumber"), grvSerial.GetRowCellValue(e.RowHandle, "SerialNumber"))
                If grvSerial.IsValidRowHandle(focusedRowHandel) Then
                    'grvSerial.SetColumnError(grvSerial.Columns("SerialNumber"), "Serial Number is repeated")
                    grvSerial.SetRowCellValue(e.RowHandle, "Validation", "SN is repeated")
                    'e.Valid = False
                End If

                If grvSerial.Columns("OldSerialNumber").Visible And grvSerial.GetRowCellValue(e.RowHandle, "OldSerialNumber").ToString <> "" Then
                    focusedRowHandel = grvSerial.LocateByValue(0, grvSerial.Columns("OldSerialNumber"), grvSerial.GetRowCellValue(e.RowHandle, "OldSerialNumber"))
                    If focusedRowHandel <> e.RowHandle Then
                        'grvSerial.SetColumnError(grvSerial.Columns("OldSerialNumber"), "Old Serial Number is repeated")
                        grvSerial.SetRowCellValue(e.RowHandle, "Validation", "Old SN is repeated")
                        'e.Valid = False
                    End If

                    focusedRowHandel = grvSerial.LocateByValue(e.RowHandle + 1, grvSerial.Columns("OldSerialNumber"), grvSerial.GetRowCellValue(e.RowHandle, "OldSerialNumber").ToString)
                    If grvSerial.IsValidRowHandle(focusedRowHandel) Then
                        'grvSerial.SetColumnError(grvSerial.Columns("OldSerialNumber"), "Old Serial Number is repeated")
                        grvSerial.SetRowCellValue(e.RowHandle, "Validation", "Old SN is repeated")
                        'e.Valid = False
                    End If
                End If
                'End If
            Else
                'grvSerial.SetColumnError(grvSerial.Columns("SerialNumber"), "Serial Number can't be empty")
                grvSerial.SetRowCellValue(e.RowHandle, "Validation", "SN can't be empty")
                'e.Valid = False
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

        'Try
        '    Dim objAssetDet As New ZulAssetsBL.ZulAssetBAL.BALAssetDetails
        '    objattAssetDetails = New attAssetDetails
        '    objattAssetDetails.SerailNo = grvSerial.GetRowCellValue(e.RowHandle, "SerialNumber")

        '    objAssetDet.GetAstIDBySerialNum(
        '    'Dim objWHSItemStock As New MartBLL.WarehouseItemStockBLL

        '    'If objWHSItemStock.CheckSerialNumber(grvSerial.GetRowCellValue(e.RowHandle, "SerialNumber"), StockTransferItemRow.Item("SKUGUID")) Then
        '    '    grvSerial.SetColumnError(grvSerial.Columns("SerialNumber"), "Serial Number already exists")
        '    '    e.Valid = False
        '    'End If

        '    Dim focusedRowHandel As Integer = grvSerial.LocateByValue(0, grvSerial.Columns("SerialNumber"), grvSerial.GetRowCellValue(e.RowHandle, "SerialNumber"))
        '    If focusedRowHandel <> e.RowHandle Then
        '        grvSerial.SetColumnError(grvSerial.Columns("SerialNumber"), "Serial Number is repeated")
        '        e.Valid = False
        '    End If

        '    focusedRowHandel = grvSerial.LocateByValue(e.RowHandle + 1, grvSerial.Columns("SerialNumber"), grvSerial.GetRowCellValue(e.RowHandle, "SerialNumber"))
        '    If grvSerial.IsValidRowHandle(focusedRowHandel) Then
        '        grvSerial.SetColumnError(grvSerial.Columns("SerialNumber"), "Serial Number is repeated")
        '        e.Valid = False
        '    End If

        'Catch ex As Exception
        '    ZulLib.Messages.ErrorMessage(ex.Message, WhoCalledMe)
        'End Try
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim objSTItemDetail As New StockTransferItemDetails

        Dim dt As DataTable = CType(grdSerial.DataSource, DataTable)
        Dim expression As String = "Validation <> ''"

        If dt.Select(expression).Length > 0 Then
            ZulLib.Messages.ErrorMessage("Serial numbers are not validated, please check the errors and try again.", "")
            Exit Sub
        End If
        ShowProgressDialog()
        Try
            objSTItemDetail.DeleteByStockTransferItemGUID(StockTransferItemGUID)
            For row As Integer = 0 To grvSerial.RowCount - 1
                'If grvSerial.GetRowCellValue(row, "ItemDetailGUID") = Guid.Empty Then
                objSTItemDetail.NewRecord()
                objSTItemDetail.Attributes.CreationDate = Now.Date
                objSTItemDetail.Attributes.CreatedBy = UserGUID
                'Else
                'objSTItemDetail.Edit(grvSerial.GetRowCellValue(row, "ItemDetailGUID"))
                'End If

                objSTItemDetail.Attributes.STItemGUID = StockTransferItemGUID
                objSTItemDetail.Attributes.QTY = 1
                objSTItemDetail.Attributes.SerialNumber = grvSerial.GetRowCellValue(row, "SerialNumber")
                objSTItemDetail.Attributes.OldSerialNumber = grvSerial.GetRowCellValue(row, "OldSerialNumber").ToString
                objSTItemDetail.Attributes.Number = objSTItemDetail.GetMAXNumberByGUID(StockTransferItemGUID)
                objSTItemDetail.Attributes.LastEditDate = Now.Date
                objSTItemDetail.Attributes.LastEditBY = UserGUID
                objSTItemDetail.Attributes.LotNumber = String.Empty
                objSTItemDetail.Attributes.AstID = ""
                'If StockTransferItemRow.Item("LotNumber") IsNot DBNull.Value Then
                '    objStockTransferItem.Attributes.LotNumber = StockTransferItemRow.Item("LotNumber")
                'Else
                '    objStockTransferItem.Attributes.LotNumber = String.Empty
                'End If


                'If StockTransferItemRow.Item("ExpireDate") IsNot DBNull.Value Then
                '    objStockTransferItem.Attributes.ExpireDate = StockTransferItemRow.Item("ExpireDate")
                'Else
                '    objStockTransferItem.Attributes.SetExpireDateNull()
                'End If


                'If StockTransferItemRow.Item("ProductionDate") IsNot DBNull.Value Then
                '    objStockTransferItem.Attributes.ProductionDate = StockTransferItemRow.Item("ProductionDate")
                'Else
                '    objStockTransferItem.Attributes.SetProductionDateNull()
                'End If
                'If StockTransferItemRow.Item("WarrantyExpireDate") IsNot DBNull.Value Then
                '    objStockTransferItem.Attributes.WarrantyExpireDate = StockTransferItemRow.Item("WarrantyExpireDate")
                'Else
                '    objStockTransferItem.Attributes.SetWarrantyExpireDateNull()
                'End If



                Dim Msg As String = objSTItemDetail.Save()
                If Not String.IsNullOrEmpty(Msg) Then Throw New Exception(Msg)
            Next
            MessageResult = MessageReturn.MSG_OK
            Me.ParentForm.Close()
        Catch ex As Exception
            ZulLib.Messages.ErrorMessage(ex.Message, WhoCalledMe)
        Finally
            CloseProgressDialog()
        End Try

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        MessageResult = MessageReturn.MSG_Cancel
        popup.Close()
    End Sub


    Private Sub grdSerial_EditorKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSerial.EditorKeyDown
        If e.KeyCode = Keys.Enter Then
            If grvSerial.FocusedColumn.FieldName = "SerialNumber" Then
                grvSerial.MoveNext()
            End If
        End If

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        For i As Integer = 0 To StockTransferItemRow.Item("QTY") - 1
            SerialNumber_DT.Rows(i)("SerialNumber") = String.Empty
            SerialNumber_DT.Rows(i)("OldSerialNumber") = String.Empty
            SerialNumber_DT.Rows(i)("Validation") = String.Empty
        Next
        grvSerial.Focus()
        grvSerial.FocusedColumn = grvSerial.Columns("SerialNumber")
        grvSerial.FocusedRowHandle = 0
    End Sub

    Private Sub btnAutoGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoGen.Click
        For i As Integer = 0 To StockTransferItemRow.Item("QTY") - 1
            If SerialNumber_DT.Rows(i)("SerialNumber") = String.Empty Then
                SerialNumber_DT.Rows(i)("SerialNumber") = DateTime.Now.ToString("MddyyHHmmssff") & i
                SerialNumber_DT.Rows(i)("Validation") = String.Empty
                'SerialNumber_DT.Rows(i)("SerialNumber") = DateTime.Now.ToString("yyyyMMddHHmmss") + i
            End If
        Next

        'If StockTransferItemRow.Item("SerialGenerateType") = 0 Then
        '    SerialNumber_Row("SerialNumber") = objStockTransferItem.GetMAXSerialNumber() + increment
        'ElseIf StockTransferItemRow.Item("SerialGenerateType") = 1 Then
        'ElseIf StockTransferItemRow.Item("SerialGenerateType") = 2 Then
        '    SerialNumber_Row("SerialNumber") = DateTime.Now.ToString("yyyyMMddHHmmss") + increment
        'End If

    End Sub
End Class
