Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Module DepCalc
    Dim Monthconfig As DepMonthConfig
    Dim FiscalDate As Date

    Enum DepMonthConfig
        HalfMonth = 1
        FullMonth = 2
    End Enum


    Public Function CalcDep(ByVal SalYr As Int16, ByVal SalMon As Int16, ByVal intDeptype As Integer, ByVal Price As Double, ByVal SalValue As Double, ByVal fiscalYr As Date, ByVal ServiceDate As Date) As Double
        Try
            Dim depValue As Double = 0.0
            If Monthconfig = DepMonthConfig.HalfMonth Then
                If ServiceDate.Day > 15 Then
                    ServiceDate = ServiceDate.AddMonths(1)
                End If
                If fiscalYr.Day > 15 Then
                    fiscalYr = fiscalYr.AddMonths(1)
                End If
            End If

            'Straight Line
            If intDeptype = 1 Then
                Dim MnthLeft As Integer

                MnthLeft = DateDiff(DateInterval.Month, ServiceDate, fiscalYr)

                depValue = ((Price - SalValue) / (SalYr * 12 + SalMon))
                depValue = depValue * MnthLeft



                'Sum Of Years
            ElseIf intDeptype = 2 Then
                Dim Sumofyr As Double
                Dim yrleft As Double
                Dim PrYrDepVal As Double = 0.0
                Dim MnthLeft As Int32
                yrleft = SalYr - DateDiff(DateInterval.Year, ServiceDate, fiscalYr)
                For a As Double = 1 To SalYr
                    Sumofyr += a
                Next
                For i As Integer = 0 To (SalYr - yrleft - 1)
                    PrYrDepVal += ((Price - SalValue) * (SalYr - i)) / Sumofyr
                Next
                MnthLeft = Fix(DateDiff(DateInterval.Day, ServiceDate, fiscalYr) / 30) Mod 12
                depValue = ((Price - SalValue) * (yrleft)) / (Sumofyr)
                depValue = (depValue / 12) * MnthLeft
                depValue = PrYrDepVal + depValue

                'Double Declining
            ElseIf intDeptype = 3 Then
                Dim yrleft As Double
                Dim PrYrDepVal As Double = 0.0
                Dim MnthLeft As Int32
                yrleft = SalYr - DateDiff(DateInterval.Year, ServiceDate, fiscalYr)
                depValue = 0.0
                For i As Double = 0 To (SalYr - yrleft - 1)
                    PrYrDepVal += (Price - PrYrDepVal) / SalYr
                Next
                MnthLeft = Fix(DateDiff(DateInterval.Day, ServiceDate, fiscalYr) / 30) Mod 12
                depValue = (Price - PrYrDepVal) / SalYr
                depValue = (depValue / 12) * MnthLeft
                depValue = PrYrDepVal + depValue

                '150%
            ElseIf intDeptype = 3 Then
                Dim yrleft As Double
                Dim PrYrDepVal As Double = 0.0
                Dim MnthLeft As Int32
                yrleft = SalYr - DateDiff(DateInterval.Year, ServiceDate, fiscalYr)
                depValue = 0.0
                For i As Double = 0 To (SalYr - yrleft - 1)
                    PrYrDepVal += (Price - PrYrDepVal) * (1.5) / SalYr
                Next
                MnthLeft = Fix(DateDiff(DateInterval.Day, ServiceDate, fiscalYr) / 30) Mod 12
                depValue = (Price - PrYrDepVal) / SalYr
                depValue = (depValue / 12) * MnthLeft
                depValue = PrYrDepVal + depValue

                '175%
            ElseIf intDeptype = 3 Then
                Dim yrleft As Double
                Dim PrYrDepVal As Double = 0.0
                Dim MnthLeft As Int32
                yrleft = SalYr - DateDiff(DateInterval.Year, ServiceDate, fiscalYr)
                depValue = 0.0
                For i As Double = 0 To (SalYr - yrleft - 1)
                    PrYrDepVal += (Price - PrYrDepVal) * (1.75) / SalYr
                Next
                MnthLeft = Fix(DateDiff(DateInterval.Day, ServiceDate, fiscalYr) / 30) Mod 12
                depValue = (Price - PrYrDepVal) / SalYr
                depValue = (depValue / 12) * MnthLeft
                depValue = PrYrDepVal + depValue

                '200%
            ElseIf intDeptype = 3 Then
                Dim yrleft As Double
                Dim PrYrDepVal As Double = 0.0
                Dim MnthLeft As Int32
                yrleft = SalYr - DateDiff(DateInterval.Year, ServiceDate, fiscalYr)
                depValue = 0.0
                For i As Integer = 0 To (SalYr - yrleft - 1)
                    PrYrDepVal += (Price - PrYrDepVal) * (2.0) / SalYr
                Next
                MnthLeft = Fix(DateDiff(DateInterval.Day, ServiceDate, fiscalYr) / 30) Mod 12
                depValue = (Price - PrYrDepVal) / SalYr
                depValue = (depValue / 12) * MnthLeft
                depValue = PrYrDepVal + depValue
            End If

            Return depValue
        Catch ex As Exception
            GenericExceptionHandler(ex, "Depreciation_Calculation")
        End Try
    End Function

    Public Function Calc_ExpectedDepreciation(ByVal SalYr As Int16, ByVal intDeptype As Integer, ByVal Price As Double, ByVal SalValue As Double, ByVal fiscalYr As Date, ByVal ServiceDate As Date) As Double
        Try
            Dim depValue As Double = 0.0
            If Monthconfig = DepMonthConfig.HalfMonth Then
                If ServiceDate.Day > 15 Then
                    ServiceDate.AddMonths(1)
                End If
            ElseIf Monthconfig = DepMonthConfig.FullMonth Then

            End If
            'intDeptype = intDeptype + 1
            'Straight Line
            If intDeptype = 1 Then
                Dim MnthLeft As Integer

                MnthLeft = Fix(DateDiff(DateInterval.Day, ServiceDate, fiscalYr) / 30)

                depValue = ((Price - SalValue) / SalYr) / 12
                depValue = depValue * MnthLeft

                Return depValue

                'Sum Of Years
            ElseIf intDeptype = 2 Then
                Dim Sumofyr As Integer
                Dim yrleft As Integer
                Dim PrYrDepVal As Double = 0.0
                Dim MnthLeft As Int32
                yrleft = SalYr - DateDiff(DateInterval.Year, ServiceDate, fiscalYr)
                For a As Integer = 1 To SalYr
                    Sumofyr += a
                Next
                For i As Integer = 0 To (SalYr - yrleft - 1)
                    PrYrDepVal += ((Price - SalValue) * (SalYr - i)) / Sumofyr
                Next
                MnthLeft = Fix(DateDiff(DateInterval.Day, ServiceDate, fiscalYr) / 30) Mod 12
                depValue = ((Price - SalValue) * (yrleft)) / (Sumofyr)
                depValue = (depValue / 12) * MnthLeft
                depValue = PrYrDepVal + depValue


                'Double Declining
            ElseIf intDeptype = 3 Then
                Dim yrleft As Integer
                Dim PrYrDepVal As Double = 0.0
                Dim MnthLeft As Int32
                yrleft = SalYr - DateDiff(DateInterval.Year, ServiceDate, fiscalYr)
                depValue = 0.0
                For i As Integer = 0 To (SalYr - yrleft - 1)
                    PrYrDepVal += (Price - PrYrDepVal) / SalYr
                Next
                MnthLeft = Fix(DateDiff(DateInterval.Day, ServiceDate, fiscalYr) / 30) Mod 12
                depValue = (Price - PrYrDepVal) / SalYr
                depValue = (depValue / 12) * MnthLeft
                depValue = PrYrDepVal + depValue


                '150%
            ElseIf intDeptype = 3 Then
                Dim yrleft As Integer
                Dim PrYrDepVal As Double = 0.0
                Dim MnthLeft As Int32
                yrleft = SalYr - DateDiff(DateInterval.Year, ServiceDate, fiscalYr)
                depValue = 0.0
                For i As Integer = 0 To (SalYr - yrleft - 1)
                    PrYrDepVal += (Price - PrYrDepVal) * (1.5) / SalYr
                Next
                MnthLeft = Fix(DateDiff(DateInterval.Day, ServiceDate, fiscalYr) / 30) Mod 12
                depValue = (Price - PrYrDepVal) / SalYr
                depValue = (depValue / 12) * MnthLeft
                depValue = PrYrDepVal + depValue


                '175%
            ElseIf intDeptype = 3 Then
                Dim yrleft As Integer
                Dim PrYrDepVal As Double = 0.0
                Dim MnthLeft As Int32
                yrleft = SalYr - DateDiff(DateInterval.Year, ServiceDate, fiscalYr)
                depValue = 0.0
                For i As Integer = 0 To (SalYr - yrleft - 1)
                    PrYrDepVal += (Price - PrYrDepVal) * (1.75) / SalYr
                Next
                MnthLeft = Fix(DateDiff(DateInterval.Day, ServiceDate, fiscalYr) / 30) Mod 12
                depValue = (Price - PrYrDepVal) / SalYr
                depValue = (depValue / 12) * MnthLeft
                depValue = PrYrDepVal + depValue
                '    MessageBox.Show(depValue)

                '200%
            ElseIf intDeptype = 3 Then
                Dim yrleft As Integer
                Dim PrYrDepVal As Double = 0.0
                Dim MnthLeft As Int32
                yrleft = SalYr - DateDiff(DateInterval.Year, ServiceDate, fiscalYr)
                depValue = 0.0
                For i As Integer = 0 To (SalYr - yrleft - 1)
                    PrYrDepVal += (Price - PrYrDepVal) * (2.0) / SalYr
                Next
                MnthLeft = Fix(DateDiff(DateInterval.Day, ServiceDate, fiscalYr) / 30) Mod 12
                depValue = (Price - PrYrDepVal) / SalYr
                depValue = (depValue / 12) * MnthLeft
                depValue = PrYrDepVal + depValue
                ' MessageBox.Show(depValue)
            End If


            Return Nothing
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Function
    Private Function GetLastDayInMonth(ByVal dt As Date) As Date
        'get the last day in month 
        Return dt.AddDays(Date.DaysInMonth(dt.Year, dt.Month) - dt.Day)
    End Function
    Public Function CalcDepAnnual(ByVal BookID As String, ByVal SalYr As Int16, ByVal SalMon As Int16, ByVal intDeptype As Integer, ByVal Price As Double, ByVal SalValue As Double, ByVal ServiceDate1 As String, ByVal CurBookValue As Double, ByVal dtService As Date, ByVal purchaseDate As Date) As DataTable
        Dim SalMYr As Integer = 0
        Dim isInMonths As Boolean = False

        purchaseDate = purchaseDate.AddYears(SalYr)
        purchaseDate = purchaseDate.AddMonths(SalMon)

        Try
            If SalYr > 0 Then

                Dim ServiceDate As Date
                Try
                    If ServiceDate1 <> "" Then
                        ServiceDate = CDate(ServiceDate1)
                    Else
                        ServiceDate = DateTime.Now.ToShortDateString()
                    End If
                Catch ex As Exception

                End Try

                If Monthconfig = DepMonthConfig.HalfMonth Then
                    If ServiceDate.Day > 15 Then
                        ServiceDate = ServiceDate.AddMonths(1)
                    End If
                End If

                Dim depValue, accDepValue As Double

                SalMYr = (SalYr * 12) + SalMon '- DateDiff(DateInterval.Month, dtService, ServiceDate)
                'SalMYr += SalMon

                If SalMYr > 0 Then
                    Dim dt As New DataTable
                    'Dim objattBookHistory(Math.Ceiling(SalMYr / 12)) As attBookHistory
                    ServiceDate = ServiceDate.AddYears(1)

                    'intDeptype += 1          


                    dt.Columns.Add("AccDep", Type.GetType("System.Double"))
                    dt.Columns.Add("Dep", Type.GetType("System.Double"))
                    dt.Columns.Add("DepMonths", Type.GetType("System.Int32"))
                    dt.Columns.Add("CurrDate", Type.GetType("System.DateTime"))
                    dt.Columns.Add("CBV", Type.GetType("System.Double"))

                    'Straight Line
                    If intDeptype = 1 Then
                        Dim dtCounter As Date
                        Dim preCounter As Date

                        dtCounter = ServiceDate.AddMonths(-1)
                        preCounter = dtCounter.AddYears(-1)

                        Dim loopCountRemainder As Integer
                        Dim loopCount As Integer = Math.DivRem(SalMYr, 12, loopCountRemainder)

                        If loopCountRemainder > 0 Then
                            isInMonths = True
                        Else
                            loopCount -= 1
                        End If

                        For i As Integer = 0 To loopCount
                            'depValue = ((Price - SalValue) / SalYr)
                            If isInMonths And i = loopCount Then
                                depValue = (Price - SalValue) - accDepValue
                                dtCounter = purchaseDate.AddMonths(-1)
                            Else

                                depValue = ((Price - SalValue) / SalMYr)
                                'depValue = ((Price - SalValue) / SalMYr)
                                depValue = depValue * 12
                            End If


                            dtCounter = GetLastDayInMonth(dtCounter)
                            If CDbl(CurBookValue - accDepValue - depValue) >= SalValue Then
                                accDepValue += depValue
                                dt.Rows.Add(CDbl(accDepValue), CDbl(depValue), DateDiff(DateInterval.Month, preCounter, dtCounter), dtCounter, CDbl(CurBookValue - accDepValue))
                            Else
                                dt.Rows.Add(CDbl(accDepValue), CDbl(depValue), DateDiff(DateInterval.Month, preCounter, dtCounter), dtCounter, SalValue)
                                Exit For
                            End If
                            preCounter = dtCounter
                            dtCounter = dtCounter.AddYears(1)
                        Next
                        Return dt

                        'Sum Of Years
                    ElseIf intDeptype = 2 Then
                        Dim dtCounter As Date
                        Dim preCounter As Date
                        Dim Sumofyr As Double

                        For a As Integer = 1 To SalYr
                            Sumofyr += a
                        Next

                        dtCounter = ServiceDate.AddMonths(-1)
                        preCounter = dtCounter.AddYears(-1)

                        Dim loopCountRemainder As Integer
                        Dim loopCount As Integer = Math.DivRem(SalMYr, 12, loopCountRemainder)

                        If loopCountRemainder > 0 Then
                            isInMonths = True
                        Else
                            loopCount -= 1
                        End If

                        For i As Integer = 0 To loopCount
                            'depValue = ((Price - SalValue) / SalYr)
                            If isInMonths And i = loopCount Then
                                depValue = (Price - SalValue) - accDepValue
                                dtCounter = purchaseDate.AddMonths(-1)
                            Else
                                'depValue = ((Price - SalValue) * (SalYr - i)) / Sumofyr


                                depValue = ((Price - SalValue) * (SalMYr - (i * 12)))
                                depValue = (depValue / 12) / Sumofyr
                            End If


                            accDepValue += depValue
                            dtCounter = GetLastDayInMonth(dtCounter)
                            dt.Rows.Add(CDbl(accDepValue), CDbl(depValue), DateDiff(DateInterval.Month, preCounter, dtCounter), dtCounter, CDbl(CurBookValue - accDepValue))
                            preCounter = dtCounter
                            dtCounter = dtCounter.AddYears(1)
                        Next
                        Return dt

                        'Double Declining
                    ElseIf intDeptype = 3 Then
                        Dim dtCounter As Date
                        Dim preCounter As Date

                        dtCounter = ServiceDate.AddMonths(-1)
                        preCounter = dtCounter.AddYears(-1)

                        Dim loopCountRemainder As Integer
                        Dim loopCount As Integer = Math.DivRem(SalMYr, 12, loopCountRemainder)

                        If loopCountRemainder > 0 Then
                            isInMonths = True
                        Else
                            loopCount -= 1
                        End If

                        accDepValue = Price

                        For i As Integer = 0 To loopCount
                            If isInMonths And i = loopCount Then
                                depValue = (Price - SalValue) - accDepValue
                                dtCounter = purchaseDate.AddMonths(-1)
                            Else
                                depValue = (accDepValue) / SalMYr
                                depValue = depValue * 12
                            End If

                            accDepValue -= depValue

                            dtCounter = GetLastDayInMonth(dtCounter)
                            dt.Rows.Add(CDbl(accDepValue), CDbl(depValue), DateDiff(DateInterval.Month, preCounter, dtCounter), dtCounter, CDbl(CurBookValue - accDepValue))

                            preCounter = dtCounter
                            dtCounter = dtCounter.AddYears(1)
                        Next
                        Return dt

                        '150%
                    ElseIf intDeptype = 4 Then
                        Dim dtCounter As Date
                        Dim preCounter As Date

                        dtCounter = ServiceDate.AddMonths(-1)
                        preCounter = dtCounter.AddYears(-1)

                        Dim loopCountRemainder As Integer
                        Dim loopCount As Integer = Math.DivRem(SalMYr, 12, loopCountRemainder)

                        If loopCountRemainder > 0 Then
                            isInMonths = True
                        Else
                            loopCount -= 1
                        End If

                        accDepValue = Price

                        For i As Integer = 0 To loopCount
                            If isInMonths And i = loopCount Then
                                depValue = (Price - SalValue) - accDepValue
                                dtCounter = purchaseDate.AddMonths(-1)
                            Else
                                depValue = (accDepValue) / SalMYr
                                depValue = (1.5) * (depValue * 12)
                            End If

                            accDepValue -= depValue

                            dtCounter = GetLastDayInMonth(dtCounter)
                            dt.Rows.Add(CDbl(accDepValue), CDbl(depValue), DateDiff(DateInterval.Month, preCounter, dtCounter), dtCounter, CDbl(CurBookValue - accDepValue))

                            preCounter = dtCounter
                            dtCounter = dtCounter.AddYears(1)
                        Next
                        Return dt


                        '175%
                    ElseIf intDeptype = 5 Then
                        Dim dtCounter As Date
                        Dim preCounter As Date

                        dtCounter = ServiceDate.AddMonths(-1)
                        preCounter = dtCounter.AddYears(-1)

                        Dim loopCountRemainder As Integer
                        Dim loopCount As Integer = Math.DivRem(SalMYr, 12, loopCountRemainder)

                        If loopCountRemainder > 0 Then
                            isInMonths = True
                        Else
                            loopCount -= 1
                        End If

                        accDepValue = Price

                        For i As Integer = 0 To loopCount
                            If isInMonths And i = loopCount Then
                                depValue = (Price - SalValue) - accDepValue
                                dtCounter = purchaseDate.AddMonths(-1)
                            Else
                                depValue = (accDepValue) / SalMYr
                                depValue = (1.75) * (depValue * 12)
                            End If

                            accDepValue -= depValue

                            dtCounter = GetLastDayInMonth(dtCounter)
                            dt.Rows.Add(CDbl(accDepValue), CDbl(depValue), DateDiff(DateInterval.Month, preCounter, dtCounter), dtCounter, CDbl(CurBookValue - accDepValue))

                            preCounter = dtCounter
                            dtCounter = dtCounter.AddYears(1)
                        Next
                        Return dt
                        '200%
                    ElseIf intDeptype = 6 Then
                        Dim dtCounter As Date
                        Dim preCounter As Date

                        dtCounter = ServiceDate.AddMonths(-1)
                        preCounter = dtCounter.AddYears(-1)

                        Dim loopCountRemainder As Integer
                        Dim loopCount As Integer = Math.DivRem(SalMYr, 12, loopCountRemainder)

                        If loopCountRemainder > 0 Then
                            isInMonths = True
                        Else
                            loopCount -= 1
                        End If

                        accDepValue = Price

                        For i As Integer = 0 To loopCount
                            If isInMonths And i = loopCount Then
                                depValue = (Price - SalValue) - accDepValue
                                dtCounter = purchaseDate.AddMonths(-1)
                            Else
                                depValue = (accDepValue) / SalMYr
                                depValue = (2.0) * (depValue * 12)
                            End If

                            accDepValue -= depValue

                            dtCounter = GetLastDayInMonth(dtCounter)
                            dt.Rows.Add(CDbl(accDepValue), CDbl(depValue), DateDiff(DateInterval.Month, preCounter, dtCounter), dtCounter, CDbl(CurBookValue - accDepValue))

                            preCounter = dtCounter
                            dtCounter = dtCounter.AddYears(1)
                        Next
                        Return dt
                    End If
                End If
            End If

            Return Nothing
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
            Return Nothing
        End Try
    End Function

    Public Function CalcDepMonthly(ByVal BookID As String, ByVal SalYr As Int16, ByVal SalMon As Int16, ByVal intDeptype As Integer, ByVal Price As Double, ByVal SalValue As Double, ByVal ServiceDate1 As String, ByVal CurBookValue As Double, ByVal dtService As Date) As DataTable

        Try
            If SalYr > 0 Then

                Dim ServiceDate As Date
                Try
                    If ServiceDate1 <> "" Then
                        ServiceDate = CDate(ServiceDate1)
                    Else
                        ServiceDate = DateTime.Now.ToShortDateString()
                    End If
                Catch ex As Exception

                End Try

                If Monthconfig = DepMonthConfig.HalfMonth Then
                    If ServiceDate.Day > 15 Then
                        ServiceDate = ServiceDate.AddMonths(1)
                    End If
                End If

                'SalYr = SalYr - DateDiff(DateInterval.Year, dtService, ServiceDate)

                SalMon = (SalYr * 12 + SalMon) '- DateDiff(DateInterval.Month, dtService, ServiceDate)

                'Dim tempSalMon As Integer
                'SalYr = Math.DivRem(SalMon, 12, tempSalMon)
                'SalMon = tempSalMon

                Dim depValue, accDepValue As Double
                ' Dim objattBookHistory(SalYr) As attBookHistory

                Dim fiscalYr As Date

                fiscalYr = ServiceDate.AddYears(SalYr)
                fiscalYr = fiscalYr.AddMonths(SalMon)

                Dim dt As New DataTable

                dt.Columns.Add("AccDep", Type.GetType("System.Double"))
                dt.Columns.Add("Dep", Type.GetType("System.Double"))
                dt.Columns.Add("CurrDate", Type.GetType("System.DateTime"))
                dt.Columns.Add("CBV", Type.GetType("System.Double"))

                'Straight Line
                If intDeptype = 1 Then
                    Dim MnthLeft As Integer
                    MnthLeft = DateDiff(DateInterval.Month, ServiceDate, fiscalYr)

                    Dim dtCounter As Date
                    dtCounter = ServiceDate

                    If MnthLeft > 0 Then
                        'Dim objattBookHistory(MnthLeft) As attBookHistory
                        For i As Integer = 0 To MnthLeft - 1
                            depValue = (Price - SalValue) / (SalMon)

                            dtCounter = GetLastDayInMonth(dtCounter)
                            If CDbl(CurBookValue - accDepValue - depValue) >= SalValue Then
                                accDepValue += depValue
                                dt.Rows.Add(CDbl(accDepValue), CDbl(depValue), dtCounter, CDbl(CurBookValue - accDepValue))
                            Else
                                dt.Rows.Add(CDbl(accDepValue), CDbl(depValue), dtCounter, SalValue)
                                Exit For
                            End If
                            dtCounter = dtCounter.AddMonths(1)
                        Next
                        Return dt
                    End If

                    'Sum Of Years
                ElseIf intDeptype = 2 Then

                    Dim Sumofyr As Double

                    Dim MnthLeft As Integer
                    MnthLeft = DateDiff(DateInterval.Month, ServiceDate, fiscalYr)

                    Dim dtCounter As Date
                    dtCounter = ServiceDate


                    'Calculate SUM of years.
                    For a As Integer = 1 To SalYr
                        Sumofyr += a
                    Next

                    If MnthLeft > 0 Then
                        'Dim objattBookHistory(MnthLeft) As attBookHistory
                        For i As Integer = 0 To MnthLeft - 1
                            Dim y As Integer = Decimal.Ceiling((MnthLeft - i) / 12)
                            depValue = ((Price - SalValue) * y) / Sumofyr / 12
                            accDepValue += depValue

                            dtCounter = GetLastDayInMonth(dtCounter)
                            dt.Rows.Add(CDbl(accDepValue), CDbl(depValue), dtCounter, CDbl(CurBookValue - accDepValue))
                            dtCounter = dtCounter.AddMonths(1)
                        Next
                        Return dt
                    End If


                    'Double Declining

                ElseIf intDeptype = 3 Then

                    Dim MnthLeft As Integer
                    MnthLeft = DateDiff(DateInterval.Month, ServiceDate, fiscalYr)

                    Dim dtCounter As Date
                    dtCounter = ServiceDate

                    If MnthLeft > 0 Then
                        'Dim objattBookHistory(MnthLeft) As attBookHistory
                        accDepValue = Price

                        For i As Integer = 0 To MnthLeft - 1
                            depValue = (accDepValue) / MnthLeft
                            accDepValue -= depValue

                            dtCounter = GetLastDayInMonth(dtCounter)
                            dt.Rows.Add(CDbl(accDepValue), CDbl(depValue), dtCounter, CDbl(CurBookValue - accDepValue))
                            dtCounter = dtCounter.AddMonths(1)
                        Next
                        Return dt
                    End If

                    '150%
                ElseIf intDeptype = 4 Then

                    Dim MnthLeft As Integer
                    MnthLeft = DateDiff(DateInterval.Month, ServiceDate, fiscalYr)

                    Dim dtCounter As Date
                    dtCounter = ServiceDate

                    If MnthLeft > 0 Then
                        'Dim objattBookHistory(MnthLeft) As attBookHistory
                        accDepValue = Price

                        For i As Integer = 0 To MnthLeft - 1
                            depValue = (1.5) * (accDepValue) / MnthLeft
                            accDepValue -= depValue

                            dtCounter = GetLastDayInMonth(dtCounter)
                            dt.Rows.Add(CDbl(accDepValue), CDbl(depValue), dtCounter, CDbl(CurBookValue - accDepValue))
                            dtCounter = dtCounter.AddMonths(1)
                        Next
                        Return dt
                    End If

                    '175%
                ElseIf intDeptype = 5 Then

                    Dim MnthLeft As Integer
                    MnthLeft = DateDiff(DateInterval.Month, ServiceDate, fiscalYr)

                    Dim dtCounter As Date
                    dtCounter = ServiceDate

                    If MnthLeft > 0 Then
                        'Dim objattBookHistory(MnthLeft) As attBookHistory
                        accDepValue = Price

                        For i As Integer = 0 To MnthLeft - 1
                            depValue = (1.75) * (accDepValue) / MnthLeft
                            accDepValue -= depValue

                            dtCounter = GetLastDayInMonth(dtCounter)
                            dt.Rows.Add(CDbl(accDepValue), CDbl(depValue), dtCounter, CDbl(CurBookValue - accDepValue))
                            dtCounter = dtCounter.AddMonths(1)
                        Next
                        Return dt
                    End If
                    '200%
                ElseIf intDeptype = 6 Then

                    Dim MnthLeft As Integer
                    MnthLeft = DateDiff(DateInterval.Month, ServiceDate, fiscalYr)

                    Dim dtCounter As Date
                    dtCounter = ServiceDate

                    If MnthLeft > 0 Then
                        accDepValue = Price

                        For i As Integer = 0 To MnthLeft - 1
                            depValue = (2.0) * (accDepValue) / MnthLeft
                            accDepValue -= depValue

                            dtCounter = GetLastDayInMonth(dtCounter)
                            dt.Rows.Add(CDbl(accDepValue), CDbl(depValue), dtCounter, CDbl(CurBookValue - accDepValue))
                            dtCounter = dtCounter.AddMonths(1)
                        Next
                        Return dt
                    End If
                End If

            End If
            Return Nothing
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
            Return Nothing
        End Try

    End Function
End Module
