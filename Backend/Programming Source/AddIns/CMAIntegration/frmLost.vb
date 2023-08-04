Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports DevExpress.XtraEditors.DXErrorProvider

Public Class frmLost

    Public AssetID As String
    Public LocID As String
    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Public valRulenotEmpty As New ConditionValidationRule

    Private Sub frmLost_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG
        Me.BackgroundImageLayout = ImageLayout.Stretch

        dtDate.CustomFormat = AppConfig.MaindateFormat
        dtDate.Value = Now.Date
        valRulenotEmpty.ConditionOperator = ConditionOperator.IsNotBlank
        valRulenotEmpty.ErrorText = "Please enter a value"
        valRulenotEmpty.ErrorType = ErrorType.Critical
        valProvMain.SetValidationRule(txtDeptName, valRulenotEmpty)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Add History
        txtDeptName.Text = txtDeptName.Text.Trim
        If valProvMain.Validate Then
            If MessageBox.Show("Are you sure to mark this asset as lost?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim objattAstHistory As attAstHistory
                objattAstHistory = New attAstHistory()
                Dim objBALAst_History As New BALAst_History
                objattAstHistory.AstID = AssetID
                objattAstHistory.Status = 6 'Means lost
                objattAstHistory.InvSchCode = 1 'system transfer
                objattAstHistory.HisDate = dtDate.Value
                objattAstHistory.PKeyCode = objBALAst_History.GetNextPKey_AstHistory()
                objattAstHistory.Fr_loc = LocID
                objattAstHistory.To_Loc = LocID
                objattAstHistory.DeptName = txtDeptName.Text
                objattAstHistory.Remarks = txtRemarks.Text.Trim
                objBALAst_History.Insert_Ast_History(objattAstHistory)

                Dim objattAssetDetails As New attAssetDetails
                Dim objBALAssetDetails As New BALAssetDetails
                objattAssetDetails.PKeyCode = AssetID
                objattAssetDetails.InvStatus = 6 'Means lost
                objattAssetDetails.InvSchCode = 1 'system transfer
                objattAssetDetails.LocID = LocID
                objBALAssetDetails.Update_InvStatus(objattAssetDetails)
                MessageBox.Show("Record saved successfully", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class