Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class frmBarcodePolicy
    Dim objattCompany As attcompany
    Dim objBALCompany As New BALCompany

    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Dim isEdit As Boolean = False
    Dim objattBarCode_Struct As attBarCode_Struct
    Dim objBALBarCode_Struct As New BALBarCode_Struct

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmBarcodePolicy = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub frmBarcodePolicy_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmBarcodePolicy = Nothing
    End Sub

    Private Sub frmBarcodePolicy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = My.Resources.MainIcon
            Me.BackgroundImage = My.Resources.Background
            Me.BackgroundImageLayout = ImageLayout.Stretch


            valProvMain.SetValidationRule(cmbComp.TextBox, valRulenotEmpty)
            valProvMain.SetValidationRule(ZTBcodeID.TextBox, valRulenotEmpty)

            Get_Company_Grid()
            format_Grid()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub Get_Company_Grid()
        Try
            grd.DataSource = objBALCompany.GetAll_Company(New attcompany)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub format_Grid()
        Try
            grdView.Columns(0).Caption = "Company ID"
            grdView.Columns(0).Width = 115
            grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

            grdView.Columns(1).Caption = "Company Code"
            grdView.Columns(1).Width = 100
            grdView.Columns(2).Caption = "Company Name"
            grdView.Columns(2).Width = 100
            grdView.Columns(3).Visible = False
            grdView.Columns(4).Visible = False
            grdView.Columns(5).Caption = "BarCode Structure"
            grdView.Columns(5).Width = 255
            grdView.Columns(6).Visible = False
            grdView.Columns(7).Visible = False

            grd.UseEmbeddedNavigator = True
            grd.EmbeddedNavigator.Buttons.Append.Visible = False
            grd.EmbeddedNavigator.Buttons.Edit.Visible = False
            grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
            grd.EmbeddedNavigator.Buttons.Remove.Visible = False
            grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False

            addGridMenu(grd)

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Try
            errProv.ClearErrors()
            If valProvMain.Validate Then
                If ZulMessageBox.ShowMe("BeforePolicyApply", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    Dim intAsset_Count As Integer = Asset_Count()
                    If MessageBox.Show("You have to print " & intAsset_Count.ToString() & " Asset Labels, if you apply this structure. Do you want to continue", "ZulAssets", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        BarCode_Assign()
                        ApplyPolcy()
                        MessageBox.Show(intAsset_Count.ToString() & " BarCodes have been updated successfully", "ZulAsset", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Get_Company_Grid()
                    End If
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
    Private Sub BarCode_Assign()
        Try
            objattCompany = New attcompany
            objattCompany.PKeyCode = cmbComp.SelectedValue
            objattCompany.BarStructID = ZTBcodeID.SelectedValue
            objBALCompany.BarCode_Assign(objattCompany)
            '    ZulMessageBox.ShowMe("Saved")
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub
    Private Function Asset_Count() As Integer
        Try
            Dim objattAssetDetails As New attAssetDetails
            Dim objBALAssetDetails As New BALAssetDetails
            objattAssetDetails.CompanyID = cmbComp.SelectedValue
            Return objBALAssetDetails.GetAssetsCount(objattAssetDetails)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function
    Private Sub ApplyPolcy()
        Try
            Dim AssetID, AssetNum, Location, Category, Reference As String
            Dim objattLocation As attLocation
            Dim objBALCategory As New BALCategory
            Dim objBALLocation As New BALLocation
            Dim objattAssetDetails As New attAssetDetails
            Dim objBALAssetDetails As New BALAssetDetails
            Dim DSAssets As DataTable
            objattAssetDetails.CompanyID = cmbComp.SelectedValue
            DSAssets = objBALAssetDetails.GetAsset_Details(objattAssetDetails)
            If DSAssets Is Nothing = False Then
                pb.Visible = True
                pb.Value = 0
                pb.Maximum = DSAssets.Rows.Count
                For Each dr As DataRow In DSAssets.Rows
                    AssetID = dr("AstID")
                    Reference = dr("RefNo")
                    AssetNum = dr("AstNum")
                    objattLocation = New attLocation
                    objattLocation.HierCode = dr("LocID")
                    Location = objBALLocation.Comp_Path(objattLocation.HierCode)
                    Category = objBALCategory.Comp_Path(dr("AstCatID"))
                    objattAssetDetails.BarCode = Generate_BarCode(Trim(cmbComp.SelectedValue), Trim(AssetID), Trim(AssetNum), Trim(Reference), Trim(Category), Trim(Category), Trim(Location), Trim(Location), "")
                    objattAssetDetails.PKeyCode = dr("AstID")
                    objBALAssetDetails.Update_BarCode(objattAssetDetails)
                    pb.Value = pb.Value + 1
                    Application.DoEvents()
                Next
            End If
            pb.Visible = False
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ApplyPolcy()
    End Sub

    Private Sub grdView_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FocRow As Integer = grdView.FocusedRowHandle
        If FocRow >= 0 Then
            cmbComp.FindRow(GetGridRowCellValue(grdView, FocRow, "CompanyID").ToString(), GetGridRowCellValue(grdView, FocRow, "CompanyName").ToString())
            ZTBcodeID.FindRow(GetGridRowCellValue(grdView, FocRow, "BarStructID").ToString(), GetGridRowCellValue(grdView, FocRow, "BarCode").ToString())
        End If
    End Sub

    Private Sub ZTBcodeID_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZTBcodeID.LovBtnClick
        Try
            ZTBcodeID.ValueMember = "BarStructID"
            ZTBcodeID.DisplayMember = "BarCode"
            Dim objBALBarCode_Struct As New BALBarCode_Struct
            ZTBcodeID.DataSource = objBALBarCode_Struct.GetAllData_GetCombo(New attBarCode_Struct)
            ZTBcodeID.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmbComp_LovBtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbComp.LovBtnClick
        Try
            cmbComp.ValueMember = "CompanyID"
            cmbComp.DisplayMember = "CompanyName"
            Dim objBALCompany As New BALCompany
            cmbComp.DataSource = objBALCompany.GetAllData_GetCombo(New attcompany)
            cmbComp.OpenLOV()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
End Class