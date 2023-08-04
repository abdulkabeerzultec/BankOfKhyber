Public Class SAPDocTypesBLL
    Public Enum DocumentsTypes
        GR
        GINV
        GIPOR
        VendorReturn
        Reversal
        ReversalGR
        ReversalInvPro
        ReversalPOR
        WarrantyClaim
        WarrantyReceivingSame
        WarrantyReceivingReplace
    End Enum

    Public Shared Function GetDocCode() As String
        Dim rand As New Random()
        Dim RandValue As String = rand.Next(0, 999).ToString.PadLeft(3, "0")
        Return Now.ToString("yy") & Now.DayOfYear.ToString.PadLeft(3, "0") & TimeOfDay.ToString("HHmmss") & Now.Millisecond.ToString & RandValue
    End Function
End Class
