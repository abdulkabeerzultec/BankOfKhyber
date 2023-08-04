Public Class frmBlkQty
    Public intQty As Integer
    Public frm As frmAssetsDetails
    Public Delegate Sub Update1()
    Public UpdateBrandhandle As Update1 = AddressOf UpBrandHandler
    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider

    Private Sub btnBlkCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlkCancel.Click
        Me.Close()
    End Sub


    Private Sub UpBrandHandler()
        pb.Value = pb.Value + 1
        Me.Refresh()
    End Sub
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If valProvMain.Validate = True Then
            intQty = txtQty.Text
            pb.Visible = True
            Me.Refresh()
            pb.Maximum = intQty
            frm.Create_Copies(intQty, frm.AssetsID.SelectedText)
            frm.FrmCop = Me
            pb.Visible = False
            Me.Close()
        End If

    End Sub

    Private Sub frmBlkQty_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.Icon = My.Resources.MainIcon

        txtQty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        txtQty.Properties.Mask.EditMask = "\d{1,5}"
        valProvMain.SetValidationRule(txtQty, valRulenotEmpty)
    End Sub


End Class