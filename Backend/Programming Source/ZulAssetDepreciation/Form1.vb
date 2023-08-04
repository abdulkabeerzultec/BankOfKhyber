'#Const TRACE = True

Imports System
Imports System.ComponentModel
Imports System.IO
Imports System.Threading
Imports System.Diagnostics
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports Microsoft.Win32


' Define custom commands for the SimpleService.

#Region " Enum Declaration "

Public Enum DepMonthConfig
    HalfMonth = 1
    FullMonth = 2
End Enum
#End Region

Public Class frmMain

#Region " Class Declaration"
    Public Shared objattAssetDetails As attAssetDetails
    Public Shared objBALAssetDetails As New BALAssetDetails
    Public Shared objattBookHistory As New attBookHistory
    Public Shared objBALBookHistory As New BALBookHistory
    Public Shared objattAstBooks As attAstBooks


    Public Shared objBALAstBooks As New BALAstBooks
    Public Shared dtAssets, dtAstBooks As New DataTable
    Public Shared objattDepLogs As New attDepLogs
    Public Shared objBALDepLogs As New BALDepLogs
    Public Shared depValue As Double
    Private objBalappConfig As BALAppConfig
    Public Shared Monthconfig As DepMonthConfig
    Public Shared FiscalDate As Date
    Private workerThread As Thread = Nothing
#End Region


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try


            Me.Visible = False
            Me.Hide()
            Me.Refresh()
            Monthconfig = DepMonthConfig.FullMonth
            objBalappConfig = New BALAppConfig
            objBalappConfig.LoadSettings()
            Dim ThisMonth, LastMonth As DateTime
            ThisMonth = DateTime.Now
            LastMonth = ThisMonth.AddMonths(-1)
            If Check_Get_DepLogs(System.DateTime.MinValue, True, LastMonth.Month, LastMonth.Year) Then
                ' MessageBox.Show("Book Already Updated for last Month")
            Else
                UpdateAstBookHist(Date.DaysInMonth(LastMonth.Year, LastMonth.Month), LastMonth.Month, LastMonth.Year)
            End If

            If ThisMonth.Day = Date.DaysInMonth(ThisMonth.Year, ThisMonth.Month) Then
                If Check_Get_DepLogs(System.DateTime.MinValue, True, ThisMonth.Month, ThisMonth.Year) Then
                    '  MessageBox.Show("Book Already Updated for this Month")
                Else
                    ' MessageBox.Show("Update Book for this Month")
                    UpdateAstBookHist(ThisMonth.Day, ThisMonth.Month, ThisMonth.Year)

                End If

            End If


        Catch ex As Exception
            GenericExceptionHandler(ex, "chkDisposed_CheckedChanged")
        End Try
    End Sub

    'Public Shared Function CalCulate_Depreciation(ByVal SalYr As Int16, ByVal intDeptype As Integer, ByVal Price As Double, ByVal SalValue As Double, ByVal fiscalYr As Date, ByVal ServiceDate As Date) As Double
    '    Try
    '        depValue = 0.0
    '        If Monthconfig = DepMonthConfig.HalfMonth Then
    '            If ServiceDate.Day > 15 Then
    '                ServiceDate.AddMonths(1)
    '            End If
    '        ElseIf Monthconfig = DepMonthConfig.FullMonth Then

    '        End If
    '        intDeptype = intDeptype + 1
    '        'Straight Line
    '        If intDeptype = 1 Then
    '            Dim MnthLeft As Integer

    '            MnthLeft = Fix(DateDiff(DateInterval.Day, ServiceDate, fiscalYr) / 30)

    '            depValue = ((Price - SalValue) / SalYr) / 12
    '            depValue = depValue * MnthLeft

    '            Return depValue

    '            'Sum Of Years
    '        ElseIf intDeptype = 2 Then
    '            Dim Sumofyr As Integer
    '            Dim yrleft As Integer
    '            Dim PrYrDepVal As Double = 0.0
    '            Dim MnthLeft As Int32
    '            yrleft = SalYr - DateDiff(DateInterval.Year, ServiceDate, fiscalYr)
    '            For a As Integer = 1 To SalYr
    '                Sumofyr += a
    '            Next
    '            For i As Integer = 0 To (SalYr - yrleft - 1)
    '                PrYrDepVal += ((Price - SalValue) * (SalYr - i)) / Sumofyr
    '            Next
    '            MnthLeft = Fix(DateDiff(DateInterval.Day, ServiceDate, fiscalYr) / 30) Mod 12
    '            depValue = ((Price - SalValue) * (yrleft)) / (Sumofyr)
    '            depValue = (depValue / 12) * MnthLeft
    '            depValue = PrYrDepVal + depValue


    '            'Double Declining
    '        ElseIf intDeptype = 3 Then
    '            Dim yrleft As Integer
    '            Dim PrYrDepVal As Double = 0.0
    '            Dim MnthLeft As Int32
    '            yrleft = SalYr - DateDiff(DateInterval.Year, ServiceDate, fiscalYr)
    '            depValue = 0.0
    '            For i As Integer = 0 To (SalYr - yrleft - 1)
    '                PrYrDepVal += (Price - PrYrDepVal) / SalYr
    '            Next
    '            MnthLeft = Fix(DateDiff(DateInterval.Day, ServiceDate, fiscalYr) / 30) Mod 12
    '            depValue = (Price - PrYrDepVal) / SalYr
    '            depValue = (depValue / 12) * MnthLeft
    '            depValue = PrYrDepVal + depValue


    '            '150%
    '        ElseIf intDeptype = 3 Then
    '            Dim yrleft As Integer
    '            Dim PrYrDepVal As Double = 0.0
    '            Dim MnthLeft As Int32
    '            yrleft = SalYr - DateDiff(DateInterval.Year, ServiceDate, fiscalYr)
    '            depValue = 0.0
    '            For i As Integer = 0 To (SalYr - yrleft - 1)
    '                PrYrDepVal += (Price - PrYrDepVal) * (1.5) / SalYr
    '            Next
    '            MnthLeft = Fix(DateDiff(DateInterval.Day, ServiceDate, fiscalYr) / 30) Mod 12
    '            depValue = (Price - PrYrDepVal) / SalYr
    '            depValue = (depValue / 12) * MnthLeft
    '            depValue = PrYrDepVal + depValue


    '            '175%
    '        ElseIf intDeptype = 3 Then
    '            Dim yrleft As Integer
    '            Dim PrYrDepVal As Double = 0.0
    '            Dim MnthLeft As Int32
    '            yrleft = SalYr - DateDiff(DateInterval.Year, ServiceDate, fiscalYr)
    '            depValue = 0.0
    '            For i As Integer = 0 To (SalYr - yrleft - 1)
    '                PrYrDepVal += (Price - PrYrDepVal) * (1.75) / SalYr
    '            Next
    '            MnthLeft = Fix(DateDiff(DateInterval.Day, ServiceDate, fiscalYr) / 30) Mod 12
    '            depValue = (Price - PrYrDepVal) / SalYr
    '            depValue = (depValue / 12) * MnthLeft
    '            depValue = PrYrDepVal + depValue
    '            '    MessageBox.Show(depValue)

    '            '200%
    '        ElseIf intDeptype = 3 Then
    '            Dim yrleft As Integer
    '            Dim PrYrDepVal As Double = 0.0
    '            Dim MnthLeft As Int32
    '            yrleft = SalYr - DateDiff(DateInterval.Year, ServiceDate, fiscalYr)
    '            depValue = 0.0
    '            For i As Integer = 0 To (SalYr - yrleft - 1)
    '                PrYrDepVal += (Price - PrYrDepVal) * (2.0) / SalYr
    '            Next
    '            MnthLeft = Fix(DateDiff(DateInterval.Day, ServiceDate, fiscalYr) / 30) Mod 12
    '            depValue = (Price - PrYrDepVal) / SalYr
    '            depValue = (depValue / 12) * MnthLeft
    '            depValue = PrYrDepVal + depValue
    '            ' MessageBox.Show(depValue)
    '        End If


    '        Return Nothing
    '    Catch ex As Exception
    '        GenericExceptionHandler(ex, "Depreciation_Calculation")
    '    End Try
    'End Function



#Region " -- Process to update Book History -- "


    Public Shared Sub UpdateAstBookHist(ByVal Days As Integer, ByVal Month As Integer, ByVal year As Integer)
        Try
            Dim Price, CurBook As Double
            Dim dtUpto, dtSrv, dtDisp, dtSale As Date
            Dim Disposed, Sold As Boolean
            Dim astCount As Integer
            Dim totDepValue, TotAstValue As Double
            Dim tempdate As Date = AppConfig.DepDate
            Dim dtFiscal As Date = New Date(year, Month, Days)
            Dim tempdate2 As New Date(dtFiscal.Year, tempdate.Month, tempdate.Day)


            ''====================== Code for Updating Books History =====================
            totDepValue = 0.0
            TotAstValue = 0.0
            Get_Assets_ById()
            If Not dtAssets Is Nothing Then ' Make Sure that Assets are there in Database
                astCount = dtAssets.Rows.Count
                For Each dtrAsset As DataRow In dtAssets.Rows

                    Price = CDbl(dtrAsset("BaseCost").ToString()) + CDbl(dtrAsset("Tax").ToString())
                    If Not dtrAsset("SrvDate").ToString Is "" Then
                        dtSrv = dtrAsset("SrvDate").ToString()
                    End If

                    If Not dtrAsset("Disposed").ToString Is "" Then
                        Disposed = dtrAsset("Disposed").ToString()
                    End If


                    If Not dtrAsset("IsSold").ToString Is "" Then
                        Sold = dtrAsset("IsSold").ToString()
                    End If

                    If Sold Then
                        dtUpto = dtSale
                    End If
                    If Disposed Then
                        dtUpto = dtDisp
                    End If
                    '********** by the End of this Line all info Required from AssetDetails is Ready  *********
                    If Date.Compare(dtSrv, dtFiscal).ToString() < 0 Then
                        'Exit Sub
                        Get_AstBooks(dtrAsset("AstID").ToString())
                        If Not dtAstBooks Is Nothing Then
                            For Each dtrAstBook As DataRow In dtAstBooks.Rows
                                If dtrAstBook("SalvageYear") * 12 > DateDiff(DateInterval.Month, dtSrv, dtFiscal) Then
                                    CalcDep(dtrAstBook("SalvageYear"), dtrAstBook("SalvageMonth"), dtrAstBook("DepCode"), Price, dtrAstBook("SalvageValue"), dtFiscal, dtrAstBook("BVUpdate"), dtSrv, dtrAstBook("CurrentBV"))
                                Else
                                    CalcDep(dtrAstBook("SalvageYear"), dtrAstBook("SalvageMonth"), dtrAstBook("DepCode"), Price, dtrAstBook("SalvageValue"), dtSrv.AddYears(dtrAstBook("SalvageYear")), dtrAstBook("BVUpdate"), dtSrv, dtrAstBook("CurrentBV"))
                                End If

                                If Not dtrAstBook("CurrentBV").ToString Is "" Then
                                    CurBook = dtrAstBook("CurrentBV")
                                End If

                                objattBookHistory = New attBookHistory
                                objattBookHistory.PKeyCode = objBALBookHistory.GetNextPKey_BookHistory()
                                objattBookHistory.BookID = dtrAstBook("BookID")
                                objattBookHistory.ASTID = dtrAstBook("AstID")
                                objattBookHistory.DepCode = dtrAstBook("DepCode")
                                objattBookHistory.AccDepValue = Round(depValue, 2)
                                objattBookHistory.DepDate = Date.Now
                                objattBookHistory.CurrentBV = Round(Price - objattBookHistory.AccDepValue, 2)
                                objattBookHistory.DepValue = Round(CurBook - objattBookHistory.CurrentBV, 2)

                                objBALBookHistory.Insert_BookHistory(objattBookHistory)
                                '*********  by the end of this line the Book history is added **** 
                                Dim objattAstBooks1 As New attAstBooks
                                Dim objBALAstBooks1 As New BALAstBooks
                                objattAstBooks1.DepCode = dtrAstBook("DepCode")
                                objattAstBooks1.SalvageValue = Round(dtrAstBook("SalvageValue"), 2)
                                objattAstBooks1.SalvageYear = Round(dtrAstBook("SalvageYear"), 2)
                                ' objattAstBooks1.BookDescription = dtrAstBook("BookDescription")
                                objattAstBooks1.PKeyCode = objattBookHistory.BookID
                                objattAstBooks1.AstID = objattBookHistory.ASTID
                                objattAstBooks1.LastBookValue = Round(CurBook, 2)
                                objattAstBooks1.CurrentBookValue = Round(objattBookHistory.CurrentBV, 2)
                                objattAstBooks1.BVUpdate = Date.Now
                                objBALAstBooks1.Update_AstBooks(objattAstBooks1)

                                'If dtrAstBook("SalvageYear") = DateDiff(DateInterval.Year, dtSrv, dtFiscal) Then
                                '    Dim objAttAstDetails As New attAssetDetails
                                '    Dim objBALAstDetails As New BALAssetDetails
                                '    objAttAstDetails.PKeyCode = dtrAstBook("AstID")
                                '    objBALAstDetails.Dispose_Asset(objAttAstDetails)
                                'End If
                                '******** by the end of this line astbook information will be updated
                                '     End If
                            Next
                        End If
                    End If
                    'End of Book Loop
                Next

                objattDepLogs = New attDepLogs
                objattDepLogs.PKeyCode = objBALDepLogs.GetNextPKey_DepLogs
                objattDepLogs.UpdDate = dtFiscal
                objattDepLogs.UpdMonth = Month
                objattDepLogs.UpdYear = year
                objattDepLogs.TotAstCount = dtAssets.Rows.Count
                objattDepLogs.TotAstValue = Round(totDepValue, 2)
                objattDepLogs.TotDepValue = Round(TotAstValue, 2)
                objBALDepLogs.Insert_DepLogs(objattDepLogs)

                '  End If
                ' ======================== End of Code of Dep History  ==============================

            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, "UpDate Book")
        End Try
    End Sub


#End Region

    Public Shared Function Round(ByVal Number As Double, Optional ByVal NumDigitsAfterDecimal As Integer = 0) As Double
        Return CDbl(FormatNumber(Number, NumDigitsAfterDecimal))
    End Function

#Region " --  DB Functions --"
    Public Shared Sub Get_Assets_ById()
        Try
            Dim ds As New DataTable
            objattAssetDetails = New attAssetDetails
            objattAssetDetails.IsSold = False
            objattAssetDetails.Disposed = False
            ds = objBALAssetDetails.GetAllData_Active(objattAssetDetails)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    dtAssets = ds
                    Return
                End If
            End If
            dtAssets = Nothing
        Catch ex As Exception
            GenericExceptionHandler(ex, "Get_Assets_ById")
        End Try
    End Sub
    Public Shared Sub Get_AstBooks(ByVal _id As String)
        Try
            Dim ds As New DataTable
            objattAstBooks = New attAstBooks
            objattAstBooks.AstID = _id
            ds = objBALAstBooks.GetAll_AstBooks(objattAstBooks)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    dtAstBooks = ds
                    Return
                End If
            End If
            dtAstBooks = Nothing
        Catch ex As Exception
            GenericExceptionHandler(ex, "Get_AstBooks")
        End Try
    End Sub

    Public Shared Function Check_Get_DepLogs(ByVal dtupdate As Date, ByVal byMonth As Boolean, ByVal Month As Int32, ByVal Year As Int32) As Boolean
        Try
            Dim ds As New DataTable
            objattDepLogs = New attDepLogs
            If byMonth Then
                objattDepLogs.UpdMonth = Month
                objattDepLogs.UpdYear = Year
            Else
                objattDepLogs.UpdMonth = 0
                objattDepLogs.UpdYear = 0
                objattDepLogs.UpdDate = dtupdate
            End If
            ds = objBALDepLogs.GetAll_DepLogs(objattDepLogs)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Return True
                End If
            End If
            dtAssets = Nothing
            Return False
        Catch ex As Exception
            GenericExceptionHandler(ex, "Check_Get_DepLogs")
        End Try
    End Function
#End Region
End Class
