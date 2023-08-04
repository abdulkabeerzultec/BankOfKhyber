Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class frmBarStuct

    Dim objattCompany As attcompany
    Dim objBALCompany As New BALCompany
    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    Dim isEdit As Boolean = False
    Dim objattBarCode_Struct As attBarCode_Struct
    Dim objBALBarCode_Struct As New BALBarCode_Struct

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmBarStuct = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub frmBarStuct_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmBarStuct = Nothing
    End Sub

    Private Sub frmBarStuct_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.ActiveControl = txtDesc

        valProvMain.SetValidationRule(ZTBcodeID.TextBox, valRulenotEmpty)
        valProvMain.SetValidationRule(txtbarcode, valRulenotEmpty)
        valProvMain.SetValidationRule(txtDesc, valRulenotEmpty)

        txtPrefix.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        txtPrefix.Properties.Mask.EditMask = "[0-9a-fA-F]+"

        Cmbsep.SelectedIndex = 0
        format_Grid()


        btnNew_Click(sender, e)
    End Sub

    Private Sub Load_data()
        CType(grd.DataSource, DataTable).Rows.Clear()

        grdView.AddNewRow()
        Dim idx As Integer = grdView.FocusedRowHandle
        grdView.SetRowCellValue(idx, "Sr", 1)
        grdView.SetRowCellValue(idx, "Selection", False)
        grdView.SetRowCellValue(idx, "Contents", "Asset ID")
        grdView.SetRowCellValue(idx, "DisplayValue", "")
        grdView.SetRowCellValue(idx, "Length", "0")

        grdView.AddNewRow()
        idx = grdView.FocusedRowHandle
        grdView.SetRowCellValue(idx, "Sr", 2)
        grdView.SetRowCellValue(idx, "Selection", False)
        grdView.SetRowCellValue(idx, "Contents", "Asset Number")
        grdView.SetRowCellValue(idx, "DisplayValue", "")
        grdView.SetRowCellValue(idx, "Length", "0")

        grdView.AddNewRow()
        idx = grdView.FocusedRowHandle
        grdView.SetRowCellValue(idx, "Sr", 3)
        grdView.SetRowCellValue(idx, "Selection", False)
        grdView.SetRowCellValue(idx, "Contents", "Reference #")
        grdView.SetRowCellValue(idx, "DisplayValue", "")
        grdView.SetRowCellValue(idx, "Length", "0")

        grdView.AddNewRow()
        idx = grdView.FocusedRowHandle
        grdView.SetRowCellValue(idx, "Sr", 4)
        grdView.SetRowCellValue(idx, "Selection", False)
        grdView.SetRowCellValue(idx, "Contents", "Category")
        grdView.SetRowCellValue(idx, "DisplayValue", "")
        grdView.SetRowCellValue(idx, "Length", "0")

        grdView.AddNewRow()
        idx = grdView.FocusedRowHandle
        grdView.SetRowCellValue(idx, "Sr", 5)
        grdView.SetRowCellValue(idx, "Selection", False)
        grdView.SetRowCellValue(idx, "Contents", "Sub Category")
        grdView.SetRowCellValue(idx, "DisplayValue", "")
        grdView.SetRowCellValue(idx, "Length", "0")


        grdView.AddNewRow()
        idx = grdView.FocusedRowHandle
        grdView.SetRowCellValue(idx, "Sr", 6)
        grdView.SetRowCellValue(idx, "Selection", False)
        grdView.SetRowCellValue(idx, "Contents", "Location")
        grdView.SetRowCellValue(idx, "DisplayValue", "")
        grdView.SetRowCellValue(idx, "Length", "0")

        grdView.AddNewRow()
        idx = grdView.FocusedRowHandle
        grdView.SetRowCellValue(idx, "Sr", 7)
        grdView.SetRowCellValue(idx, "Selection", False)
        grdView.SetRowCellValue(idx, "Contents", "Sub Location")
        grdView.SetRowCellValue(idx, "DisplayValue", "")
        grdView.SetRowCellValue(idx, "Length", "0")
        grdView.UpdateCurrentRow()

        'grdView.AddNewRow()
        'grdView.CancelUpdateCurrentRow()

    End Sub

    Private Sub Load_data(ByVal bcode As String)
        CType(grd.DataSource, DataTable).Rows.Clear()



        Dim Str() As String
        Dim BAstID, BAstNum, BRef, BCat1, BCat2, BLoc1, BLoc2 As Boolean
        Dim idx As Integer = 0
        Str = bcode.Split(",")
        For idx1 As Integer = 0 To Str.Length - 1
            If Str(idx1).StartsWith("AID") Then
                BAstID = True

                grdView.AddNewRow()
                idx = grdView.FocusedRowHandle
                grdView.SetRowCellValue(idx, "Sr", 1)
                grdView.SetRowCellValue(idx, "Selection", False)
                grdView.SetRowCellValue(idx, "Contents", "Asset ID")
                grdView.SetRowCellValue(idx, "DisplayValue", "")
                grdView.SetRowCellValue(idx, "Length", "0")

            ElseIf Str(idx1).StartsWith("ANM") Then
                BAstNum = True
                grdView.AddNewRow()
                idx = grdView.FocusedRowHandle
                grdView.SetRowCellValue(idx, "Sr", 2)
                grdView.SetRowCellValue(idx, "Selection", False)
                grdView.SetRowCellValue(idx, "Contents", "Asset Number")
                grdView.SetRowCellValue(idx, "DisplayValue", "")
                grdView.SetRowCellValue(idx, "Length", "0")
            ElseIf Str(idx1).StartsWith("REF") Then
                BRef = True

                grdView.AddNewRow()
                idx = grdView.FocusedRowHandle
                grdView.SetRowCellValue(idx, "Sr", 3)
                grdView.SetRowCellValue(idx, "Selection", False)
                grdView.SetRowCellValue(idx, "Contents", "Reference #")
                grdView.SetRowCellValue(idx, "DisplayValue", "")
                grdView.SetRowCellValue(idx, "Length", "0")
            ElseIf Str(idx1).StartsWith("CAT1") Then
                BCat1 = True
                grdView.AddNewRow()
                idx = grdView.FocusedRowHandle
                grdView.SetRowCellValue(idx, "Sr", 4)
                grdView.SetRowCellValue(idx, "Selection", False)
                grdView.SetRowCellValue(idx, "Contents", "Category")
                grdView.SetRowCellValue(idx, "DisplayValue", "")
                grdView.SetRowCellValue(idx, "Length", "0")
            ElseIf Str(idx1).StartsWith("CAT2") Then
                BCat2 = True
                grdView.AddNewRow()
                idx = grdView.FocusedRowHandle
                grdView.SetRowCellValue(idx, "Sr", 5)
                grdView.SetRowCellValue(idx, "Selection", False)
                grdView.SetRowCellValue(idx, "Contents", "Sub Category")
                grdView.SetRowCellValue(idx, "DisplayValue", "")
                grdView.SetRowCellValue(idx, "Length", "0")
            ElseIf Str(idx1).StartsWith("LOC1") Then
                BLoc1 = True
                grdView.AddNewRow()
                idx = grdView.FocusedRowHandle
                grdView.SetRowCellValue(idx, "Sr", 6)
                grdView.SetRowCellValue(idx, "Selection", False)
                grdView.SetRowCellValue(idx, "Contents", "Location")
                grdView.SetRowCellValue(idx, "DisplayValue", "")
                grdView.SetRowCellValue(idx, "Length", "0")
            ElseIf Str(idx1).StartsWith("LOC2") Then
                BLoc2 = True
                grdView.AddNewRow()
                idx = grdView.FocusedRowHandle
                grdView.SetRowCellValue(idx, "Sr", 7)
                grdView.SetRowCellValue(idx, "Selection", False)
                grdView.SetRowCellValue(idx, "Contents", "Sub Location")
                grdView.SetRowCellValue(idx, "DisplayValue", "")
                grdView.SetRowCellValue(idx, "Length", "0")
            End If
        Next


        If BAstID = False Then
            grdView.AddNewRow()
            idx = grdView.FocusedRowHandle
            grdView.SetRowCellValue(idx, "Sr", 1)
            grdView.SetRowCellValue(idx, "Selection", False)
            grdView.SetRowCellValue(idx, "Contents", "Asset ID")
            grdView.SetRowCellValue(idx, "DisplayValue", "")
            grdView.SetRowCellValue(idx, "Length", "0")
        End If
        If BAstNum = False Then
            grdView.AddNewRow()
            idx = grdView.FocusedRowHandle
            grdView.SetRowCellValue(idx, "Sr", 2)
            grdView.SetRowCellValue(idx, "Selection", False)
            grdView.SetRowCellValue(idx, "Contents", "Asset Number")
            grdView.SetRowCellValue(idx, "DisplayValue", "")
            grdView.SetRowCellValue(idx, "Length", "0")
        End If
        If BRef = False Then
            grdView.AddNewRow()
            idx = grdView.FocusedRowHandle
            grdView.SetRowCellValue(idx, "Sr", 3)
            grdView.SetRowCellValue(idx, "Selection", False)
            grdView.SetRowCellValue(idx, "Contents", "Reference #")
            grdView.SetRowCellValue(idx, "DisplayValue", "")
            grdView.SetRowCellValue(idx, "Length", "0")
        End If

        If BCat1 = False Then
            grdView.AddNewRow()
            idx = grdView.FocusedRowHandle
            grdView.SetRowCellValue(idx, "Sr", 4)
            grdView.SetRowCellValue(idx, "Selection", False)
            grdView.SetRowCellValue(idx, "Contents", "Category")
            grdView.SetRowCellValue(idx, "DisplayValue", "")
            grdView.SetRowCellValue(idx, "Length", "0")
        End If
        If BCat2 = False Then
            grdView.AddNewRow()
            idx = grdView.FocusedRowHandle
            grdView.SetRowCellValue(idx, "Sr", 5)
            grdView.SetRowCellValue(idx, "Selection", False)
            grdView.SetRowCellValue(idx, "Contents", "Sub Category")
            grdView.SetRowCellValue(idx, "DisplayValue", "")
            grdView.SetRowCellValue(idx, "Length", "0")
        End If
        If BLoc1 = False Then
            grdView.AddNewRow()
            idx = grdView.FocusedRowHandle
            grdView.SetRowCellValue(idx, "Sr", 6)
            grdView.SetRowCellValue(idx, "Selection", False)
            grdView.SetRowCellValue(idx, "Contents", "Location")
            grdView.SetRowCellValue(idx, "DisplayValue", "")
            grdView.SetRowCellValue(idx, "Length", "0")
        End If
        If BLoc2 = False Then
            grdView.AddNewRow()
            idx = grdView.FocusedRowHandle
            grdView.SetRowCellValue(idx, "Sr", 7)
            grdView.SetRowCellValue(idx, "Selection", False)
            grdView.SetRowCellValue(idx, "Contents", "Sub Location")
            grdView.SetRowCellValue(idx, "DisplayValue", "")
            grdView.SetRowCellValue(idx, "Length", "0")
        End If

        grdView.UpdateCurrentRow()
    End Sub

    Private Sub format_Grid()
        Try

            Dim dt As New DataTable
            dt.Columns.Add("Sr", Type.GetType("System.Int16"))
            dt.Columns.Add("Selection", Type.GetType("System.Boolean"))
            dt.Columns.Add("Contents", Type.GetType("System.String"))
            dt.Columns.Add("DisplayValue", Type.GetType("System.String"))
            dt.Columns.Add("Length", Type.GetType("System.Int16"))
            grd.DataSource = dt


            grdView.OptionsBehavior.Editable = True
            grdView.Columns("Sr").Caption = "Sr #"
            grdView.Columns("Sr").Width = 40
            grdView.Columns("Sr").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            grdView.Columns("Sr").OptionsColumn.AllowEdit = False

            grdView.Columns("Selection").Caption = "Selection"
            grdView.Columns("Selection").Width = 110
            grdView.Columns("Selection").OptionsColumn.AllowEdit = True

            grdView.Columns("Contents").Caption = "Contents"
            grdView.Columns("Contents").Width = 220
            grdView.Columns("Contents").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            grdView.Columns("Contents").OptionsColumn.AllowEdit = False

            grdView.Columns("DisplayValue").Caption = "Display Value"
            grdView.Columns("DisplayValue").Width = 220
            grdView.Columns("DisplayValue").OptionsColumn.AllowEdit = False

            Dim RILength As New DevExpress.XtraEditors.Repository.RepositoryItemComboBox
            Dim items As Integer() = {0, 1, 2, 3, 4, 5, 6}
            RILength.Items.AddRange(items)
            RILength.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor

            Dim RISelection As New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
            grdView.Columns("Selection").ColumnEdit = RISelection


            grdView.Columns("Length").Caption = "Length"
            grdView.Columns("Length").Width = 60
            grdView.Columns("Length").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            grdView.Columns("Length").OptionsColumn.AllowEdit = True
            grdView.Columns("Length").ColumnEdit = RILength
            addGridMenu(grd)
            ' add handler to fire the update row event after changing the cell value
            AddHandler RILength.EditValueChanged, AddressOf grdViewValueChanged
            AddHandler RISelection.CheckedChanged, AddressOf grdViewValueChanged

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub grdViewValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' this will fire the update row event
        Dim FocRow As Integer = grdView.FocusedRowHandle
        grdView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle
        grdView.FocusedRowHandle = FocRow
        'txtLength.Focus()
        'grdView.Focus()
    End Sub

    Private Function CalcBarcodeLength(ByVal index As Integer) As Integer
        Dim IntLenght As Integer = 0

        If grdView.GetRowCellValue(index, "Contents").ToString = "Asset ID" Then
            If grdView.GetRowCellValue(index, "Length").ToString = "0" Then
                IntLenght = IntLenght + 14
            Else
                IntLenght = IntLenght + CInt(grdView.GetRowCellValue(index, "Length"))
            End If
            If Cmbsep.SelectedItem.ToString() <> "None" Then
                IntLenght = IntLenght + 1
            End If
        ElseIf grdView.GetRowCellValue(index, "Contents").ToString = "Asset Number" Then
            If grdView.GetRowCellValue(index, "Length").ToString = "0" Then
                IntLenght = IntLenght + 8
            Else
                IntLenght = IntLenght + CInt(grdView.GetRowCellValue(index, "Length"))
            End If
            If Cmbsep.SelectedItem.ToString() <> "None" Then
                IntLenght = IntLenght + 1
            End If
        ElseIf grdView.GetRowCellValue(index, "Contents").ToString = "Reference #" Then
            If grdView.GetRowCellValue(index, "Length").ToString = "0" Then
                IntLenght = IntLenght + 10
            Else
                IntLenght = IntLenght + CInt(grdView.GetRowCellValue(index, "Length"))
            End If
            If Cmbsep.SelectedItem.ToString() <> "None" Then
                IntLenght = IntLenght + 1
            End If
        ElseIf grdView.GetRowCellValue(index, "Contents").ToString = "Category" Then
            If grdView.GetRowCellValue(index, "Length").ToString = "0" Then
                IntLenght = IntLenght + 4
            Else
                IntLenght = IntLenght + CInt(grdView.GetRowCellValue(index, "Length"))
            End If
            If Cmbsep.SelectedItem.ToString() <> "None" Then
                IntLenght = IntLenght + 1
            End If
        ElseIf grdView.GetRowCellValue(index, "Contents").ToString = "Sub Category" Then
            If grdView.GetRowCellValue(index, "Length").ToString = "0" Then
                IntLenght = IntLenght + 4
            Else
                IntLenght = IntLenght + CInt(grdView.GetRowCellValue(index, "Length"))
            End If
            If Cmbsep.SelectedItem.ToString() <> "None" Then
                IntLenght = IntLenght + 1
            End If
        ElseIf grdView.GetRowCellValue(index, "Contents").ToString = "Location" Then
            If grdView.GetRowCellValue(index, "Length").ToString = "0" Then
                IntLenght = IntLenght + 4
            Else
                IntLenght = IntLenght + CInt(grdView.GetRowCellValue(index, "Length"))
            End If
            If Cmbsep.SelectedItem.ToString() <> "None" Then
                IntLenght = IntLenght + 1
            End If
        ElseIf grdView.GetRowCellValue(index, "Contents").ToString = "Sub Location" Then
            If grdView.GetRowCellValue(index, "Length").ToString = "0" Then
                IntLenght = IntLenght + 4
            Else
                IntLenght = IntLenght + CInt(grdView.GetRowCellValue(index, "Length"))
            End If
            If Cmbsep.SelectedItem.ToString() <> "None" Then
                IntLenght = IntLenght + 1
            End If
        End If

        Return IntLenght
    End Function


    Private Sub txtPrefix_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrefix.LostFocus
        Dim str As String = txtPrefix.Text
        str = str.ToLower()
        If str.StartsWith("loc") Then
            ZulMessageBox.ShowMe("InvalidPrefix")
            txtPrefix.Text = ""
        End If
    End Sub

    Private Sub txtPrefix_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrefix.TextChanged
        Dim intLenght As Integer
        txtbarcode.Text = ""
        If Trim(txtPrefix.Text) <> "" Then
            txtbarcode.Text = "FIX-" & Trim(txtPrefix.Text)
            intLenght = txtbarcode.Text.Length - 4


        End If

        For idx As Int16 = 0 To grdView.RowCount - 1
            If grdView.GetRowCellValue(idx, "Selection").ToString() = True Then
                If Trim(txtbarcode.Text) <> "" Then
                    txtbarcode.Text = txtbarcode.Text & ","
                End If
                txtbarcode.Text = txtbarcode.Text & grdView.GetRowCellValue(idx, "DisplayValue").ToString
                intLenght += CalcBarcodeLength(idx)
            End If
        Next
        If Cmbsep.SelectedItem.ToString() <> "None" Then
            intLenght = intLenght - 1
        End If

        txtLength.Text = intLenght
        If intLenght > 20 Then
            errProv.SetError(txtLength, My.MessagesResource.Messages.ExceedBarCode)
        Else
            errProv.ClearErrors()
        End If
    End Sub

    Private Function Verify_BarCodeSize() As Boolean
        Dim IntLenght As Integer = 0

        For idx As Int16 = 0 To grdView.RowCount - 1
            If grdView.GetRowCellValue(idx, "Contents").ToString = "Asset ID" Then
                If grdView.GetRowCellValue(idx, "Selection").ToString() = True Then
                    If grdView.GetRowCellValue(idx, "Length").ToString = "0" Then
                        IntLenght = IntLenght + 14
                    Else
                        IntLenght = IntLenght + CInt(grdView.GetRowCellValue(idx, "Length"))
                    End If
                    If Cmbsep.SelectedItem.ToString() <> "None" Then
                        IntLenght = IntLenght + 1
                    End If
                End If

            ElseIf grdView.GetRowCellValue(idx, "Contents").ToString = "Asset Number" Then
                If grdView.GetRowCellValue(idx, "Selection").ToString() = True Then
                    If grdView.GetRowCellValue(idx, "Length").ToString = "0" Then
                        IntLenght = IntLenght + 8
                    Else
                        IntLenght = IntLenght + CInt(grdView.GetRowCellValue(idx, "Length"))
                    End If
                    If Cmbsep.SelectedItem.ToString() <> "None" Then
                        IntLenght = IntLenght + 1
                    End If
                End If

            ElseIf grdView.GetRowCellValue(idx, "Contents").ToString = "Reference #" Then
                If grdView.GetRowCellValue(idx, "Selection").ToString() = True Then
                    If grdView.GetRowCellValue(idx, "Length").ToString = "0" Then
                        IntLenght = IntLenght + 10
                    Else
                        IntLenght = IntLenght + CInt(grdView.GetRowCellValue(idx, "Length"))
                    End If
                    If Cmbsep.SelectedItem.ToString() <> "None" Then
                        IntLenght = IntLenght + 1
                    End If
                End If


            ElseIf grdView.GetRowCellValue(idx, "Contents").ToString = "Category" Then
                If grdView.GetRowCellValue(idx, "Selection").ToString() = True Then
                    If grdView.GetRowCellValue(idx, "Length").ToString = "0" Then
                        IntLenght = IntLenght + 4
                    Else
                        IntLenght = IntLenght + CInt(grdView.GetRowCellValue(idx, "Length"))
                    End If
                    If Cmbsep.SelectedItem.ToString() <> "None" Then
                        IntLenght = IntLenght + 1
                    End If
                End If



            ElseIf grdView.GetRowCellValue(idx, "Contents").ToString = "Sub Category" Then
                If grdView.GetRowCellValue(idx, "Selection").ToString() = True Then
                    If grdView.GetRowCellValue(idx, "Length").ToString = "0" Then
                        IntLenght = IntLenght + 4
                    Else
                        IntLenght = IntLenght + CInt(grdView.GetRowCellValue(idx, "Length"))
                    End If
                    If Cmbsep.SelectedItem.ToString() <> "None" Then
                        IntLenght = IntLenght + 1
                    End If
                End If

            ElseIf grdView.GetRowCellValue(idx, "Contents").ToString = "Location" Then
                If grdView.GetRowCellValue(idx, "Selection").ToString() = True Then
                    If CInt(grdView.GetRowCellValue(idx, "Length")) = 0 Then
                        IntLenght = IntLenght + 4
                    Else
                        IntLenght = IntLenght + CInt(grdView.GetRowCellValue(idx, "Length"))
                    End If
                    If Cmbsep.SelectedItem.ToString() <> "None" Then
                        IntLenght = IntLenght + 1
                    End If

                End If



            ElseIf grdView.GetRowCellValue(idx, "Contents").ToString = "Sub Location" Then
                If grdView.GetRowCellValue(idx, "Selection").ToString() = True Then
                    If grdView.GetRowCellValue(idx, "Length").ToString = "0" Then
                        IntLenght = IntLenght + 4
                    Else
                        IntLenght = IntLenght + CInt(grdView.GetRowCellValue(idx, "Length"))
                    End If
                    If Cmbsep.SelectedItem.ToString() <> "None" Then
                        IntLenght = IntLenght + 1
                    End If
                End If


            End If
        Next

        If Trim(txtPrefix.Text) <> "" Then
            IntLenght = IntLenght + txtPrefix.Text.Length
        End If
        txtLength.Text = IntLenght

        If IntLenght > 20 Then
            errProv.SetError(txtLength, My.MessagesResource.Messages.ExceedBarCode)
            Return False
        Else
            errProv.ClearErrors()
        End If
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate Then
                If Verify_BarCodeSize() Then
                    If isEdit Then
                        If Update_BarCodeStruct() Then
                            btnNew_Click(sender, e)
                        End If

                    Else
                        If AddNew_BarCodeStruct() Then
                            btnNew_Click(sender, e)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub



    Private Function AddNew_BarCodeStruct() As Boolean
        Try
            objattBarCode_Struct = New attBarCode_Struct
            objattBarCode_Struct.PKeyCode = ZTBcodeID.SelectedText
            objattBarCode_Struct.BarCode = txtbarcode.Text
            objattBarCode_Struct.BarStructDesc = txtDesc.Text
            objattBarCode_Struct.BarStructLength = txtLength.Text
            objattBarCode_Struct.BarStructPreFix = txtPrefix.Text
            objattBarCode_Struct.ValueSep = Cmbsep.SelectedItem.ToString()
            If ZTBcodeID.SelectedText = objBALBarCode_Struct.Insert_BarCode_Struct(objattBarCode_Struct) Then
                ZulMessageBox.ShowMe("Saved")
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function

    Private Function Update_BarCodeStruct() As Boolean
        Try
            objattBarCode_Struct = New attBarCode_Struct
            objattBarCode_Struct.PKeyCode = ZTBcodeID.SelectedText
            objattBarCode_Struct.BarCode = txtbarcode.Text
            objattBarCode_Struct.BarStructDesc = txtDesc.Text
            objattBarCode_Struct.BarStructLength = txtLength.Text
            objattBarCode_Struct.BarStructPreFix = txtPrefix.Text
            objattBarCode_Struct.ValueSep = Cmbsep.SelectedItem.ToString()
            If objBALBarCode_Struct.Update_BarCode_Struct(objattBarCode_Struct) Then
                ZulMessageBox.ShowMe("Saved")
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function



    Private Sub ZTBcodeID_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZTBcodeID.SelectTextChanged
        Try
            If ZTBcodeID.SelectedText <> "" Then
                Dim ds As New DataTable
                objattBarCode_Struct = New attBarCode_Struct
                objattBarCode_Struct.PKeyCode = ZTBcodeID.SelectedText
                ds = objBALBarCode_Struct.GetAll_BarCode_Struct(objattBarCode_Struct)
                If ds Is Nothing = False Then
                    If ds.Rows.Count > 0 Then
                        If ds.Rows(0).IsNull("BarStructPrefix") Then
                            txtPrefix.Text = ""
                        Else
                            txtPrefix.Text = ds.Rows(0)("BarStructPrefix")
                        End If
                        Cmbsep.SelectedItem = ds.Rows(0)("ValueSep")
                        isEdit = True
                        btnDelete.Visible = True
                        Process_string(ds.Rows(0)("BarCode"))
                        txtbarcode.Text = ds.Rows(0)("BarCode")
                        txtDesc.Text = ds.Rows(0)("BarStructDesc")
                        txtLength.Text = ds.Rows(0)("BarStructLength")

                        valProvMain.Validate()
                        errProv.ClearErrors()
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Me.Load_data()

        ZTBcodeID.SelectedText = objBALBarCode_Struct.GetNextPKey_BarCode_Struct()
        ZTBcodeID.SelectedValue = ZTBcodeID.SelectedText
        txtbarcode.Text = ""
        txtDesc.Text = ""
        txtLength.Text = "0"
        txtPrefix.Text = ""
        Cmbsep.SelectedIndex = 0
        isEdit = False
        btnDelete.Visible = False

        valProvMain.RemoveControlError(ZTBcodeID.TextBox)
        valProvMain.RemoveControlError(txtbarcode)
        valProvMain.RemoveControlError(txtDesc)

        errProv.ClearErrors()

    End Sub
    'AID,REF,CAT1,CAT2,LOC1,LOC2

    Private Sub Process_string(ByVal bcode As String)
        Load_data(bcode) '  Exit Sub
        Dim str() As String
        str = bcode.Split(",")
        If str.Length > 0 Then
            For idx As Integer = 0 To str.Length - 1
                If str(idx).StartsWith("AID") Then
                    For inta As Integer = 0 To grdView.RowCount - 1
                        If grdView.GetRowCellValue(inta, "Contents").ToString = "Asset ID" Then
                            Dim str2() As String
                            str2 = str(idx).Split("-")
                            If str2.Length > 1 Then
                                grdView.SetRowCellValue(inta, "Length", str2(1))
                            End If
                            grdView.SetRowCellValue(inta, "Selection", True)
                            grdView.SetRowCellValue(inta, "DisplayValue", "AID-" & grdView.GetRowCellValue(inta, "Length").ToString)
                        End If
                    Next
                ElseIf str(idx).StartsWith("ANM") Then
                    For inta As Integer = 0 To grdView.RowCount - 1
                        If grdView.GetRowCellValue(inta, "Contents").ToString = "Asset Number" Then
                            Dim str2() As String
                            str2 = str(idx).Split("-")
                            If str2.Length > 1 Then
                                grdView.SetRowCellValue(inta, "Length", str2(1))
                            End If
                            grdView.SetRowCellValue(inta, "Selection", True)
                            grdView.SetRowCellValue(inta, "DisplayValue", "ANM-" & grdView.GetRowCellValue(inta, "Length").ToString)
                        End If
                    Next

                ElseIf str(idx).StartsWith("REF") Then
                    For inta As Integer = 0 To grdView.RowCount - 1
                        If grdView.GetRowCellValue(inta, "Contents").ToString = "Reference #" Then
                            Dim str2() As String
                            str2 = str(idx).Split("-")
                            If str2.Length > 1 Then
                                grdView.SetRowCellValue(inta, "Length", str2(1))
                            End If
                            grdView.SetRowCellValue(inta, "Selection", True)
                            grdView.SetRowCellValue(inta, "DisplayValue", "REF-" & grdView.GetRowCellValue(inta, "Length").ToString)
                        End If
                    Next


                ElseIf str(idx).StartsWith("CAT1") Then
                    For inta As Integer = 0 To grdView.RowCount - 1
                        If grdView.GetRowCellValue(inta, "Contents").ToString = "Category" Then
                            Dim str2() As String
                            str2 = str(idx).Split("-")
                            If str2.Length > 1 Then
                                grdView.SetRowCellValue(inta, "Length", str2(1))
                            End If
                            grdView.SetRowCellValue(inta, "Selection", True)
                            grdView.SetRowCellValue(inta, "DisplayValue", "CAT1-" & grdView.GetRowCellValue(inta, "Length").ToString)
                        End If
                    Next


                ElseIf str(idx).StartsWith("CAT2") Then
                    For inta As Integer = 0 To grdView.RowCount - 1
                        If grdView.GetRowCellValue(inta, "Contents").ToString = "Sub Category" Then
                            Dim str2() As String
                            str2 = str(idx).Split("-")
                            If str2.Length > 1 Then
                                grdView.SetRowCellValue(inta, "Length", str2(1))
                            End If
                            grdView.SetRowCellValue(inta, "Selection", True)
                            grdView.SetRowCellValue(inta, "DisplayValue", "CAT2-" & grdView.GetRowCellValue(inta, "Length").ToString)
                        End If
                    Next


                ElseIf str(idx).StartsWith("LOC1") Then
                    For inta As Integer = 0 To grdView.RowCount - 1
                        If grdView.GetRowCellValue(inta, "Contents").ToString = "Location" Then
                            Dim str2() As String
                            str2 = str(idx).Split("-")
                            If str2.Length > 1 Then
                                grdView.SetRowCellValue(inta, "Length", str2(1))
                            End If
                            grdView.SetRowCellValue(inta, "Selection", True)
                            grdView.SetRowCellValue(inta, "DisplayValue", "LOC1-" & grdView.GetRowCellValue(inta, "Length").ToString)
                        End If
                    Next


                ElseIf str(idx).StartsWith("LOC2") Then
                    For inta As Integer = 0 To grdView.RowCount - 1
                        If grdView.GetRowCellValue(inta, "Contents").ToString = "Sub Location" Then
                            Dim str2() As String
                            str2 = str(idx).Split("-")
                            If str2.Length > 1 Then
                                grdView.SetRowCellValue(inta, "Length", str2(1))
                            End If
                            grdView.SetRowCellValue(inta, "Selection", True)
                            grdView.SetRowCellValue(inta, "DisplayValue", "LOC2-" & grdView.GetRowCellValue(inta, "Length").ToString)
                        End If
                    Next


                ElseIf str(idx).StartsWith("FIX") Then
                    Dim strPre() As String
                    strPre = str(idx).Split("-")
                    If strPre.Length > 1 Then
                        txtPrefix.Text = strPre(1)
                    End If
                Else


                End If
            Next
        End If


    End Sub


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not check_Child_BarCode(ZTBcodeID.SelectedValue, 1) Then
                If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    Dim objatt As New attBarCode_Struct
                    objatt.PKeyCode = ZTBcodeID.SelectedValue
                    If objBALBarCode_Struct.Delete_BarCode_Struct(objatt) Then
                        ZulMessageBox.ShowMe("Deleted")
                        btnNew_Click(sender, e)
                    End If
                End If
            Else
                ZulMessageBox.ShowMe("CantDelete")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Cmbsep_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmbsep.SelectedIndexChanged
        Dim intLenght As Integer
        Dim idx As Integer = 0
        txtbarcode.Text = ""
        If Trim(txtPrefix.Text) <> "" Then
            txtbarcode.Text = "FIX-" & Trim(txtPrefix.Text)
            intLenght = txtbarcode.Text.Length - 4

        End If

        For idx = 0 To grdView.RowCount - 1
            If grdView.GetRowCellValue(idx, "Selection").ToString() = True Then
                If Trim(txtbarcode.Text) <> "" Then
                    txtbarcode.Text = txtbarcode.Text & ","
                End If
                txtbarcode.Text = txtbarcode.Text & grdView.GetRowCellValue(idx, "DisplayValue").ToString
                intLenght += CalcBarcodeLength(idx)
            End If
        Next
        If Cmbsep.SelectedItem.ToString <> "None" And txtbarcode.Text <> "" Then
            intLenght = intLenght - 1
        End If

        txtLength.Text = intLenght
        If intLenght > 20 Then
            errProv.SetError(txtLength, My.MessagesResource.Messages.ExceedBarCode)
        Else
            errProv.ClearErrors()
        End If
    End Sub


    Private Sub grdView_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles grdView.RowUpdated

        If e.RowHandle >= 0 Then
            If grdView.GetRowCellValue(e.RowHandle, "Selection").ToString = False Then
                grdView.SetRowCellValue(e.RowHandle, "DisplayValue", "")
                grdView.SetRowCellValue(e.RowHandle, "Selection", False)
                grdView.SetRowCellValue(e.RowHandle, "Length", "0")
            Else

                If grdView.GetRowCellValue(e.RowHandle, "Length").ToString <> "" Then
                    If grdView.GetRowCellValue(e.RowHandle, "Contents").ToString = "Asset ID" Then
                        If grdView.GetRowCellValue(e.RowHandle, "Selection").ToString = True Then
                            grdView.SetRowCellValue(e.RowHandle, "DisplayValue", "AID-" & grdView.GetRowCellValue(e.RowHandle, "Length").ToString)
                            grdView.SetRowCellValue(e.RowHandle, "Selection", True)
                        End If

                    ElseIf grdView.GetRowCellValue(e.RowHandle, "Contents").ToString = "Asset Number" Then
                        If grdView.GetRowCellValue(e.RowHandle, "Selection").ToString = True Then
                            grdView.SetRowCellValue(e.RowHandle, "DisplayValue", "ANM-" & grdView.GetRowCellValue(e.RowHandle, "Length").ToString)
                            grdView.SetRowCellValue(e.RowHandle, "Selection", True)
                        End If

                    ElseIf grdView.GetRowCellValue(e.RowHandle, "Contents").ToString = "Reference #" Then
                        If grdView.GetRowCellValue(e.RowHandle, "Selection").ToString = True Then
                            grdView.SetRowCellValue(e.RowHandle, "DisplayValue", "REF-" & grdView.GetRowCellValue(e.RowHandle, "Length").ToString)
                            grdView.SetRowCellValue(e.RowHandle, "Selection", True)
                        End If

                    ElseIf grdView.GetRowCellValue(e.RowHandle, "Contents").ToString = "Category" Then
                        If grdView.GetRowCellValue(e.RowHandle, "Selection").ToString = True Then
                            grdView.SetRowCellValue(e.RowHandle, "DisplayValue", "CAT1-" & grdView.GetRowCellValue(e.RowHandle, "Length").ToString)
                            grdView.SetRowCellValue(e.RowHandle, "Selection", True)
                        End If

                    ElseIf grdView.GetRowCellValue(e.RowHandle, "Contents").ToString = "Sub Category" Then
                        If grdView.GetRowCellValue(e.RowHandle, "Selection").ToString = True Then
                            grdView.SetRowCellValue(e.RowHandle, "DisplayValue", "CAT2-" & grdView.GetRowCellValue(e.RowHandle, "Length").ToString)
                            grdView.SetRowCellValue(e.RowHandle, "Selection", True)
                        End If

                    ElseIf grdView.GetRowCellValue(e.RowHandle, "Contents").ToString = "Location" Then
                        If grdView.GetRowCellValue(e.RowHandle, "Selection").ToString = True Then
                            grdView.SetRowCellValue(e.RowHandle, "DisplayValue", "LOC1-" & grdView.GetRowCellValue(e.RowHandle, "Length").ToString)
                            grdView.SetRowCellValue(e.RowHandle, "Selection", True)
                        End If

                    ElseIf grdView.GetRowCellValue(e.RowHandle, "Contents").ToString = "Sub Location" Then
                        If grdView.GetRowCellValue(e.RowHandle, "Selection").ToString = True Then
                            grdView.SetRowCellValue(e.RowHandle, "DisplayValue", "LOC2-" & grdView.GetRowCellValue(e.RowHandle, "Length").ToString)
                            grdView.SetRowCellValue(e.RowHandle, "Selection", True)
                        End If
                    End If
                End If

            End If
            Dim intLenght As Integer
            txtbarcode.Text = ""
            If Trim(txtPrefix.Text) <> "" Then
                txtbarcode.Text = "FIX-" & Trim(txtPrefix.Text)
                intLenght = txtbarcode.Text.Length - 4

            End If

            For idx As Int16 = 0 To grdView.RowCount - 1

                If grdView.GetRowCellValue(idx, "Selection").ToString() = True Then
                    If Trim(txtbarcode.Text) <> "" Then
                        txtbarcode.Text = txtbarcode.Text & ","
                    End If
                    txtbarcode.Text = txtbarcode.Text & grdView.GetRowCellValue(idx, "DisplayValue").ToString
                    'intLenght += CInt(grdView.GetRowCellValue(idx,"Length"))
                    intLenght += CalcBarcodeLength(idx)
                End If
            Next

            If Cmbsep.SelectedItem.ToString <> "None" And txtbarcode.Text <> "" Then
                intLenght = intLenght - 1
            End If


            txtLength.Text = intLenght

            If intLenght > 20 Then
                errProv.SetError(txtLength, My.MessagesResource.Messages.ExceedBarCode)
            Else
                errProv.ClearErrors()
            End If
        End If
    End Sub

    Private Sub ZTBcodeID_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZTBcodeID.LovBtnClick
        Try
            ZTBcodeID.ValueMember = "BarStructID"
            ZTBcodeID.DisplayMember = "BarStructID"
            Dim objBALBarCode_Struct As New BALBarCode_Struct
            ZTBcodeID.DataSource = objBALBarCode_Struct.GetAllData_GetCombo(New attBarCode_Struct)
            ZTBcodeID.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
End Class