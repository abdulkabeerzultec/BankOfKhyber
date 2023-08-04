Public Class rptPhysicalInventory
    Dim cntMissing As Long = 0
    Dim cntFound As Long = 0
    Dim cntMisplaced As Long = 0
    Dim cntTrans As Long = 0
    Dim cntAllocated As Long = 0
    Dim cntAnonymous As Long = 0

    Private Sub Detail_AfterPrint(ByVal sender As Object, ByVal e As System.EventArgs) Handles Detail.AfterPrint
        If XrTableCellStatus.Text = "Missing" Then
            cntMissing = cntMissing + 1
            lblMissing.Text = cntMissing
        ElseIf XrTableCellStatus.Text = "Found" Then
            cntFound = cntFound + 1
            lblFound.Text = cntFound
        ElseIf XrTableCellStatus.Text = "Misplaced" Then
            cntMisplaced = cntMisplaced + 1
            lblMisplaced.Text = cntMisplaced
        ElseIf XrTableCellStatus.Text = "Transferred" Then
            cntTrans = cntTrans + 1
            lblTrans.Text = cntTrans
        End If
    End Sub
End Class