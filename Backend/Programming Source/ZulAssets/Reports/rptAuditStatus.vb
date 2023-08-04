Public Class rptAuditStatus
    Dim cntMissing As Long = 0
    Dim cntFound As Long = 0
    Dim cntMisplaced As Long = 0
    Dim cntTrans As Long = 0
    Dim cntAllocated As Long = 0
    Dim cntAnonymous As Long = 0

    Private Sub Detail_AfterPrint(ByVal sender As Object, ByVal e As System.EventArgs) Handles Detail.AfterPrint
        If lblStatus.Text = "0" Then
            cntMissing = cntMissing + 1
            lblMissing.Text = cntMissing
        ElseIf lblStatus.Text = "1" Then
            cntFound = cntFound + 1
            lblFound.Text = cntFound
        ElseIf lblStatus.Text = "2" Then
            cntMisplaced = cntMisplaced + 1
            lblMisplaced.Text = cntMisplaced
        ElseIf lblStatus.Text = "3" Then
            cntTrans = cntTrans + 1
            lblTrans.Text = cntTrans
        ElseIf lblStatus.Text = "4" Then
            cntAllocated = cntAllocated + 1
            lblAllocated.Text = cntAllocated
        ElseIf lblStatus.Text = "5" Then
            cntAnonymous = cntAnonymous + 1
            lblAnonymous.Text = cntAnonymous
        End If
    End Sub
End Class