Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO
Imports System.Environment
Imports System.Text
Imports DevExpress.XtraGrid.Columns

Public Class frmDepMan
    Public Enum DepMonthConfig
        HalfMonth = 1
        FullMonth = 2
    End Enum

    Dim frmProg As frmDepProgress

    Dim objattCompany As attcompany
    Dim objBALCompany As New BALCompany
    Dim objattBookTempl As attBook
    Dim objBALBookTempl As New BALBooks
    Public Shared objattAssetDetails As attAssetDetails
    Public Shared objBALAssetDetails As New BALAssetDetails
    Public Shared objattBookHistory As New attBookHistory
    Public Shared objBALBookHistory As New BALBookHistory
    Public Shared objattAstBooks As attAstBooks

    Public Shared objBALAstBooks As New BALAstBooks
    Public Shared dtAssets As New DataTable
    Public Shared dtAstBooks As New DataTable
    Public Shared objattDepLogs As New attDepLogs
    Public Shared objBALDepLogs As New BALDepLogs
    Public Shared depValue As Decimal
    Private objBalappConfig As BALAppConfig
    Public Shared Monthconfig As DepMonthConfig
    Public Shared FiscalDate As Date
    Dim isManual As Boolean = True

    Dim totDepValue As Decimal
    Dim totAstValue As Decimal
    Dim historyCount As Integer



    Private Sub frmDepMan_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmDepMan = Nothing
    End Sub

    Private Sub frmDepMan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        dtUpdated.CustomFormat = AppConfig.MaindateFormat
        Get_BookTempl("0")
    End Sub


    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        grdView.ClearColumnsFilter()
        cmbComp.SelectedText = ""
        cmbComp.SelectedValue = ""
        If chkAll.Checked Then
            cmbComp.Enabled = False

            For i As Integer = 0 To grdView.RowCount - 1
                SetGridRowCellValue(grdView, i, "SELECTION", True)
            Next
        Else
            cmbComp.Enabled = True
            For i As Integer = 0 To grdView.RowCount - 1
                SetGridRowCellValue(grdView, i, "SELECTION", False)
            Next
        End If
    End Sub

    Private Sub format_Grid()
        Try
            grdView.Columns(0).Caption = "Selection"
            grdView.Columns(0).Width = 65
            grdView.Columns(0).OptionsColumn.AllowEdit = True

            For i As Integer = 0 To grdView.RowCount - 1
                SetGridRowCellValue(grdView, i, "SELECTION", False)
            Next

            grdView.Columns(1).Caption = "Book ID"
            grdView.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            grdView.Columns(1).Width = 55
            grdView.Columns(1).OptionsColumn.AllowEdit = False

            grdView.Columns(2).Caption = "Description"
            grdView.Columns(2).Width = 160
            grdView.Columns(2).OptionsColumn.AllowEdit = False

            grdView.Columns(3).Caption = "Depreciation Method"
            grdView.Columns(3).OptionsColumn.AllowEdit = False
            grdView.Columns(3).Width = 120

            grdView.Columns(4).Visible = False
            grdView.Columns(4).OptionsColumn.AllowEdit = False

            grdView.Columns(5).Visible = False
            grdView.Columns(5).OptionsColumn.AllowEdit = False

            grdView.Columns(6).Caption = "Company"
            grdView.Columns(6).OptionsColumn.AllowEdit = False
            grdView.Columns(6).Width = 120

            addGridMenu(grd)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub Get_BookTempl(ByVal ComID As String)
        Try
            objattBookTempl = New attBook
            objattBookTempl.CompanyID = ComID
            grdView.Columns.Clear()

            Dim dt As DataTable = objBALBookTempl.GetAll_Book(objattBookTempl)
            Dim DC As DataColumn = dt.Columns.Add("SELECTION", Type.GetType("System.Boolean"))
            DC.SetOrdinal(0)
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("SELECTION") = False
            Next
            grd.DataSource = dt

            'If ds Is Nothing = False Then
            '    'If ds.Tables.Count > 0 Then
            '    If ds.Rows.Count > 0 Then
            '        'dgAssetsBooks.DataSource = ds
            '        FillDSToGrid(ds, dgAssetsBooks, New ProgressBar)
            '        For i As Integer = 1 To dgAssetsBooks.Rows.Count - 1
            '            dgAssetsBooks.Item(i, 0) = False
            '        Next
            '    Else
            '        dgAssetsBooks.DataSource = Nothing
            '        dgAssetsBooks.Rows.Count = 1
            '        dgAssetsBooks.Refresh()
            '    End If
            'End If
            '' End If
            format_Grid()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub

    Private Sub cmbComp_SelectTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComp.SelectTextChanged

        grdView.ClearColumnsFilter()
        For i As Integer = 0 To grdView.RowCount - 1
            SetGridRowCellValue(grdView, i, "SELECTION", False)
        Next

        If cmbComp.SelectedText <> "" Then
            SetGridRowCellValue(grdView, DevExpress.XtraGrid.GridControl.AutoFilterRowHandle, "CompanyName", cmbComp.SelectedText)
        End If
    End Sub

    'Private Sub dgAssetsBooks_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If dgAssetsBooks.Rows.Count > 0 Then
    '            Dim intRow As Integer = dgAssetsBooks.Row.ToString()
    '            Dim intCol As Integer = dgAssetsBooks.Col.ToString()
    '            If intRow > 0 And intCol = 0 Then
    '                dgAssetsBooks.AllowEditing = True
    '            Else
    '                dgAssetsBooks.AllowEditing = False
    '            End If
    '        Else
    '            dgAssetsBooks.AllowEditing = False
    '        End If
    '    Catch ex As Exception
    '        GenericExceptionHandler(ex, WhoCalledMe)
    '    End Try
    'End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        FormController.objfrmDepMan = Nothing
        Me.Dispose()
    End Sub

#Region "Thread Depreciation"

    Dim thrBreak As Threading.Thread
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If MessageBox.Show("Are you sure to run the depreciation on the selected books?", "ZulAssets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim SelCount As Integer = 0
            For i As Integer = 0 To grdView.RowCount - 1
                If GetGridRowCellValue(grdView, i, "SELECTION") = True Then
                    SelCount = SelCount + 1
                End If
            Next

            If SelCount > 0 Then
                frmProg = New frmDepProgress
                'thrBreak = New Threading.Thread(AddressOf ThrBreakMethod)
                frmProg.MdiParent = Me.ParentForm
                frmProg.Show()
                ThrBreakMethod()
                'thrBreak.Start()
                'frmProg.Dispose()
                'frmProg = Nothing
            Else
                ZulMessageBox.ShowMe("AtLeastBook")
            End If
        End If
    End Sub


    ''' <summary>
    '''     This function will make the depreciation proccess for the selected books that the user choose.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ThrBreakMethod()
        'Dim starttime As Integer = TickCount ' to comment
        Dim dt As Date = dtUpdated.Value
        Dim bookID As Integer
        Dim selectedBooks As String = ""
        Dim totalAssetsCount As Integer = 0
        Dim startTime As DateTime = Now

        'Getting row counts
        For i As Integer = 0 To grdView.RowCount - 1
            bookID = GetGridRowCellValue(grdView, i, "BookID")
            If GetGridRowCellValue(grdView, i, "SELECTION") = True Then
                selectedBooks += bookID & ", "
                totalAssetsCount += objBALAstBooks.GetAssetsCount(bookID)
            End If
        Next
        selectedBooks = selectedBooks.Substring(0, Len(selectedBooks) - 2) ' to remove the last two ch
        frmProg.lstReport.Items.Clear()
        frmProg.lstReport.Items.Add("Depreciation Started at " & Now.ToString())
        frmProg.lstReport.Items.Add("Update books till " & dt.Date)
        frmProg.lstReport.Items.Add("Total Assets count = " & totalAssetsCount)
        frmProg.lstReport.Items.Add("Selected Books: " & selectedBooks)
        frmProg.lblAllBookDepRec.Text = 0
        frmProg.pbAllBooks.Visible = True
        frmProg.pbAllBooks.Step = 1
        frmProg.pbAllBooks.Maximum = totalAssetsCount
        Application.DoEvents()

        Try
            For i As Integer = 0 To grdView.RowCount - 1
                bookID = GetGridRowCellValue(grdView, i, "BookID")
                If GetGridRowCellValue(grdView, i, "SELECTION") = True Then
                    If Check_Get_DepLogs(dt, bookID) Then
                        frmProg.lstReport.Items.Add("Book " & bookID & " Already Closed")
                        totalAssetsCount -= objBALAstBooks.GetAssetsCount(bookID)
                        frmProg.pbAllBooks.Maximum -= objBALAstBooks.GetAssetsCount(bookID)
                    Else
                        Run_Depreciation_ProcessNew(dt.Day, dt.Month, dt.Year, bookID)
                    End If
                End If
            Next

            frmProg.pbCurrentBook.Visible = False
            frmProg.pbAllBooks.Visible = False
            frmProg.lstReport.Items.Add("Depreciation finished at " & Now.ToString())
            frmProg.lstReport.Items.Add("Total Time is = " & (Now - startTime).TotalSeconds & " Seconds")
            frmProg.lstReport.Items.Add("Depreciation Process Completed")
            frmProg.CanClose = True
            Application.DoEvents()
        Catch ex As Exception
            frmProg.lstReport.Items.Add(ex.Message)
            frmProg.CanClose = True
        End Try
    End Sub

    Public Sub Run_Depreciation_ProcessNew(ByVal Days As Integer, ByVal Month As Integer, ByVal year As Integer, ByVal BookID As Integer)

        Dim dtFiscal As Date = New Date(year, Month, Days)

        dtAstBooks = GetAssetDetails_ByBookID(BookID)


        If Not dtAstBooks Is Nothing Then
            frmProg.pbCurrentBook.Visible = True
            frmProg.pbCurrentBook.Step = 1
            frmProg.pbCurrentBook.Value = 0
            frmProg.pbCurrentBook.Maximum = dtAstBooks.Rows.Count
            frmProg.lblBookDepRec.Text = 0
            frmProg.lblBookID.Text = BookID

            Dim objattBookHistory As attBookHistory

            Dim Price, DeprValue As Double
            Dim SalYr, SalMonth As Integer
            Dim dtSrv As Date
            totDepValue = 0
            totAstValue = 0
            'historyCount = 0
            Try
                'Get AssetDetails AND astbooks record to make depreciation on it.
                For Each dtrAstBook As DataRow In dtAstBooks.Rows

                    Price = CDbl(dtrAstBook("basecost")) + CDbl(dtrAstBook("Tax"))
                    dtSrv = dtrAstBook("SrvDate")
                    If Date.Compare(dtSrv, dtFiscal) <= 0 Then
                        SalYr = CInt(dtrAstBook("SalvageYear"))
                        SalMonth = CInt(dtrAstBook("SalvageMonth"))
                        SalMonth = SalMonth + (SalYr * 12)
                        If SalMonth > 0 Then

                            If SalMonth - DateDiff(DateInterval.Month, dtrAstBook("SrvDate"), dtFiscal) > 0 Then
                                Dim CurrentBV As Double = CDbl(dtrAstBook("CurrentBV"))
                                DeprValue = CalcDep(dtrAstBook("SalvageYear"), dtrAstBook("SalvageMonth"), dtrAstBook("DepCode"), CurrentBV, dtrAstBook("SalvageValue"), dtFiscal, dtrAstBook("BVUpdate"), dtrAstBook("srvdate"), Price)
                                If check_DepHistory(dtrAstBook("BookID"), dtrAstBook("AstID"), CDate(dtrAstBook("BVUpdate")), dtFiscal) Then
                                    Dim dtLog As DataTable = Get_Latest_ChangeLog(dtrAstBook("BookID"), dtrAstBook("AstID"), CDate(dtrAstBook("BVUpdate")), dtFiscal)
                                    If dtLog Is Nothing = False Then
                                        DeprValue = CalcDep(dtrAstBook("SalvageYear"), dtrAstBook("SalvageMonth"), dtrAstBook("DepCode"), CurrentBV, dtrAstBook("SalvageValue"), dtFiscal, dtLog.Rows(0)("BVUpdate"), dtrAstBook("srvdate"), Price)
                                    Else
                                        DeprValue = CalcDep(dtrAstBook("SalvageYear"), dtrAstBook("SalvageMonth"), dtrAstBook("DepCode"), CurrentBV, dtrAstBook("SalvageValue"), dtFiscal, dtrAstBook("BVUpdate"), dtrAstBook("srvdate"), Price)
                                    End If
                                Else
                                    DeprValue = CalcDep(dtrAstBook("SalvageYear"), dtrAstBook("SalvageMonth"), dtrAstBook("DepCode"), CurrentBV, dtrAstBook("SalvageValue"), dtFiscal, dtrAstBook("BVUpdate"), dtrAstBook("srvdate"), Price)
                                End If
                                totDepValue += DeprValue
                                totAstValue += (CurrentBV - DeprValue)

                                'we should compare currentBV with the salvage value, to avoid depreciating the full depreciated assets.
                                If CurrentBV > dtrAstBook("SalvageValue") Then
                                    objattBookHistory = New attBookHistory
                                    objattBookHistory.PKeyCode = objBALBookHistory.GetNextPKey_BookHistory()
                                    objattBookHistory.BookID = dtrAstBook("BookID")
                                    objattBookHistory.ASTID = dtrAstBook("AstID")
                                    objattBookHistory.DepCode = dtrAstBook("DepCode")
                                    objattBookHistory.SalvageYear = dtrAstBook("SalvageYear")
                                    objattBookHistory.SalvageMonth = dtrAstBook("SalvageMonth")
                                    'To avoid adding minus values or values less than salvage value, we should check if the New CurrentBV is less than Salvage value.
                                    If CurrentBV - DeprValue <= dtrAstBook("SalvageValue") Then
                                        DeprValue = CurrentBV - dtrAstBook("SalvageValue")
                                        objattBookHistory.CurrentBV = dtrAstBook("SalvageValue")
                                    Else
                                        objattBookHistory.CurrentBV = (CurrentBV - DeprValue)
                                    End If
                                    objattBookHistory.DepValue = DeprValue
                                    objattBookHistory.AccDepValue = (Price - objattBookHistory.CurrentBV)
                                    objattBookHistory.DepDate = dtFiscal
                                    objBALBookHistory.Insert_BookHistory(objattBookHistory)

                                    Dim objattAstBook As New attAstBooks
                                    objattAstBook.DepCode = dtrAstBook("DepCode")
                                    objattAstBook.SalvageValue = Round(dtrAstBook("SalvageValue"), 2)
                                    objattAstBook.SalvageValuePercent = Round(dtrAstBook("SalvageValuePercent"), 2)
                                    objattAstBook.SalvageYear = Round(dtrAstBook("SalvageYear"), 2)
                                    objattAstBook.SalvageMonth = Round(dtrAstBook("SalvageMonth"), 2)
                                    objattAstBook.PKeyCode = objattBookHistory.BookID
                                    objattAstBook.AstID = objattBookHistory.ASTID
                                    objattAstBook.LastBookValue = CurrentBV
                                    objattAstBook.CurrentBookValue = CurrentBV - DeprValue
                                    objattAstBook.BVUpdate = dtFiscal
                                    objBALAstBooks.Update_AstBooks(objattAstBook)
                                End If

                                'Wael: No need to dispose assets while doing depreciation, because depreciation should
                                'make the status depreciated not disposed.

                                'If Round(objattBookHistory.CurrentBV, 2) = Round(dtrAstBook("SalvageValue"), 2) Then
                                '    DisposeAsset(dtrAstBook("AstID"), dtFiscal)
                                'End If
                                ''*********  by the end of this line the Book history is added **** 



                                'Dim dtSrvDispose As Date
                                'dtSrvDispose = dtSrv
                                'dtSrvDispose = dtSrvDispose.AddMonths(dtrAstBook("SalvageMonth"))
                                'dtSrvDispose = dtSrvDispose.AddYears(dtrAstBook("SalvageYear"))
                                'If dtSrvDispose <= dtFiscal Then
                                '    DisposeAsset(dtrAstBook("AstID"), dtFiscal)
                                'End If
                            Else ' Dispose Asset

                                Dim dtSrvDispose As Date
                                dtSrvDispose = CDate(dtrAstBook("BVUpdate"))
                                dtSrvDispose = dtSrvDispose.AddMonths(dtrAstBook("SalvageMonth"))
                                dtSrvDispose = dtSrvDispose.AddYears(dtrAstBook("SalvageYear"))

                                Dim CurrentBV As Double = CDbl(dtrAstBook("CurrentBV"))
                                DeprValue = CalcDep(dtrAstBook("SalvageYear"), dtrAstBook("SalvageMonth"), dtrAstBook("DepCode"), CurrentBV, dtrAstBook("SalvageValue"), dtSrvDispose, dtrAstBook("BVUpdate"), dtrAstBook("srvdate"), Price)
                                If check_DepHistory(dtrAstBook("BookID"), dtrAstBook("AstID"), CDate(dtrAstBook("BVUpdate")), dtFiscal) Then
                                    Dim dtLog As DataTable = Get_Latest_ChangeLog(dtrAstBook("BookID"), dtrAstBook("AstID"), CDate(dtrAstBook("BVUpdate")), dtSrvDispose)
                                    If dtLog Is Nothing = False Then
                                        CurrentBV = CDbl(dtLog.Rows(0)("CurrentBV"))
                                        DeprValue = CalcDep(dtrAstBook("SalvageYear"), dtrAstBook("SalvageMonth"), dtrAstBook("DepCode"), CurrentBV, dtrAstBook("SalvageValue"), dtSrvDispose, dtLog.Rows(0)("BVUpdate"), dtrAstBook("srvdate"), Price)
                                    Else
                                        DeprValue = CalcDep(dtrAstBook("SalvageYear"), dtrAstBook("SalvageMonth"), dtrAstBook("DepCode"), CurrentBV, dtrAstBook("SalvageValue"), dtSrvDispose, dtrAstBook("BVUpdate"), dtrAstBook("srvdate"), Price)
                                    End If
                                Else
                                    DeprValue = CalcDep(dtrAstBook("SalvageYear"), dtrAstBook("SalvageMonth"), dtrAstBook("DepCode"), CurrentBV, dtrAstBook("SalvageValue"), dtSrvDispose, dtrAstBook("BVUpdate"), dtrAstBook("srvdate"), Price)
                                End If
                                totDepValue += DeprValue
                                totAstValue += (CurrentBV - DeprValue)

                                'we should compare currentBV with the salvage value, to avoid depreciating the full depreciated assets.
                                If CurrentBV > dtrAstBook("SalvageValue") Then
                                    objattBookHistory = New attBookHistory
                                    objattBookHistory.PKeyCode = objBALBookHistory.GetNextPKey_BookHistory()
                                    objattBookHistory.BookID = dtrAstBook("BookID")
                                    objattBookHistory.ASTID = dtrAstBook("AstID")
                                    objattBookHistory.DepCode = dtrAstBook("DepCode")
                                    objattBookHistory.SalvageYear = dtrAstBook("SalvageYear")
                                    objattBookHistory.SalvageMonth = dtrAstBook("SalvageMonth")
                                    'To avoid adding minus values or values less than salvage value, we should check if the New CurrentBV is less than Salvage value.
                                    If CurrentBV - DeprValue <= dtrAstBook("SalvageValue") Then
                                        DeprValue = CurrentBV - dtrAstBook("SalvageValue")
                                        objattBookHistory.CurrentBV = dtrAstBook("SalvageValue")
                                    Else
                                        objattBookHistory.CurrentBV = (CurrentBV - DeprValue)
                                    End If
                                    objattBookHistory.DepValue = Round(DeprValue)
                                    objattBookHistory.AccDepValue = Round(Price - objattBookHistory.CurrentBV)
                                    objattBookHistory.DepDate = dtSrvDispose
                                    objBALBookHistory.Insert_BookHistory(objattBookHistory)
                                    ''*********  by the end of this line the Book history is added **** 


                                    Dim objattAstBooks1 As New attAstBooks
                                    objattAstBooks1.DepCode = dtrAstBook("DepCode")
                                    objattAstBooks1.SalvageValue = Round(dtrAstBook("SalvageValue"), 2)
                                    objattAstBooks1.SalvageValuePercent = Round(dtrAstBook("SalvageValuePercent"), 2)
                                    objattAstBooks1.SalvageYear = Round(dtrAstBook("SalvageYear"), 2)
                                    objattAstBooks1.SalvageMonth = Round(dtrAstBook("SalvageMonth"), 2)
                                    objattAstBooks1.PKeyCode = objattBookHistory.BookID
                                    objattAstBooks1.AstID = objattBookHistory.ASTID
                                    objattAstBooks1.LastBookValue = CurrentBV
                                    objattAstBooks1.CurrentBookValue = CurrentBV - DeprValue
                                    objattAstBooks1.BVUpdate = dtSrvDispose
                                    objBALAstBooks.Update_AstBooks(objattAstBooks1)

                                End If


                                'DisposeAsset(dtrAstBook("AstID"), dtFiscal)
                            End If

                        Else
                            'No need to dispose the asset while doing the depreciation.

                            'If SalMonth = 0 Then
                            '    DisposeAsset(dtrAstBook("AstID"), dtFiscal)
                            'End If
                        End If
                    End If
                    'historyCount += 1
                    frmProg.lblAllBookDepRec.Text += 1
                    'frmProg.lblAllBookDepRec.Refresh()
                    frmProg.pbCurrentBook.PerformStep()
                    'frmProg.pbCurrentBook.Refresh()

                    frmProg.lblBookDepRec.Text += 1
                    'frmProg.lblBookDepRec.Refresh()
                    frmProg.pbAllBooks.PerformStep()
                    'frmProg.pbAllBooks.Refresh()
                    Application.DoEvents()
                    ' to exit from the loop if user termenate the process
                    If frmProg.CanClose = True Then
                        Exit For
                    End If
                Next
            Catch ex As Exception
                Throw ex
            End Try
            EndDepProcess(dtFiscal, BookID)
        End If
    End Sub

    'Private Sub DisposeAsset(ByVal astID As String, ByVal dispDate As String)
    '    objBALAssetDetails.Dispose_AssetByID(astID, dispDate)
    'End Sub


    Private Shared Function GetAssetDetails_ByBookID(ByVal BookId As String) As DataTable
        Try
            Dim ds As New DataTable
            objattAssetDetails = New attAssetDetails
            objattAssetDetails.BookID = BookId
            ds = objBALAssetDetails.GetAllData_ActiveForDep(objattAssetDetails)
            If ds Is Nothing = False Then
                If ds.Rows.Count > 0 Then
                    Return ds
                End If
            End If
            dtAssets = Nothing
            Return Nothing
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
            Return Nothing
        End Try
    End Function


#End Region

#Region "Threaded Depreciation"

    Dim threadCount As Integer
   

    Private Structure threadParam
        Dim dtFiscal As Date
        Dim BookID As Integer
        Dim startIndx As Integer
        Dim endIndx As Integer
    End Structure

    Public Sub EndDepProcess(ByVal dtFiscal As Date, ByVal BookID As Integer)
        If threadCount <= 0 Then
            objattDepLogs = New attDepLogs
            objattDepLogs.PKeyCode = objBALDepLogs.GetNextPKey_DepLogs
            objattDepLogs.UpdDate = dtFiscal
            objattDepLogs.BookID = BookID
            objattDepLogs.TotAstCount = historyCount
            objattDepLogs.TotAstValue = Round(totAstValue, 2)
            objattDepLogs.TotDepValue = Round(totDepValue, 2)
            objBALDepLogs.Insert_DepLogs(objattDepLogs)
        End If
    End Sub




    'Private Sub depThreadStart(ByVal param As Object)
    '    Dim parameters As threadParam = CType(param, threadParam)

    '    Dim dtFiscal As Date = parameters.dtFiscal
    '    Dim BookID As Integer = parameters.BookID
    '    Dim startIndx As Integer = parameters.startIndx
    '    Dim endIndx As Integer = parameters.endIndx

    '    'Dim dtAstBooks As New DataTable
    '    Dim objattBookHistory As New attBookHistory
    '    'Dim objBALBookHistory As New BALBookHistory

    '    Dim Price, DeprValue As Double
    '    Dim SalYr, SalMonth As Integer
    '    Dim dtSrv As Date
    '    'dtDisp, dtSale
    '    totDepValue = 0
    '    totAstValue = 0

    '    'logCount = 0
    '    'LessDateCount = 0
    '    'DisposedCount = 0
    '    'NoDetailCount = 0
    '    'Dim dtFiscal As Date = New Date(Year, Month, Days)
    '    Try
    '        For i As Integer = startIndx To endIndx

    '            Dim dtrAstBook As DataRow = dtAstBooks.Rows(i)

    '            dtAssets = Get_Assets_ByID(dtrAstBook("AstId").ToString)


    '            'dtAstoBoks = Get_AstBooks(dtrAsset("AstID").ToString(), BookID)
    '            If dtAssets IsNot Nothing Then

    '                For Each dtrAsset As DataRow In dtAssets.Rows

    '                    ' If dtrAsset("RefNO") <> "50026" Then Continue For

    '                    Price = CDbl(dtrAsset("BaseCost").ToString()) + CDbl(dtrAsset("Tax").ToString())
    '                    If Not dtrAsset("SrvDate").ToString Is "" Then
    '                        dtSrv = dtrAsset("SrvDate").ToString()
    '                    End If
    '                    'If dtrAsset("AstNum").ToString() = "1" Then
    '                    '    MessageBox.Show(dtrAsset("AstNum").ToString())
    '                    'End If

    '                    If Date.Compare(dtSrv, dtFiscal) < 0 Then

    '                        If dtrAstBook("SalvageYear") <> "0" Then
    '                            Try
    '                                SalYr = CInt(dtrAstBook("SalvageYear"))
    '                            Catch ex As Exception
    '                                SalYr = 0
    '                            End Try
    '                            Try
    '                                SalMonth = CInt(dtrAstBook("SalvageMonth"))
    '                            Catch ex As Exception
    '                                SalMonth = 0
    '                            End Try
    '                        End If

    '                        SalMonth = CInt(dtrAstBook("SalvageMonth")) + (CInt(dtrAstBook("SalvageYear")) * 12)
    '                        If SalMonth > 0 Then

    '                            If SalMonth - DateDiff(DateInterval.Month, dtrAsset("SrvDate"), dtFiscal) > 0 Then
    '                                'If SalMonth - DateDiff(DateInterval.Month, dtrAstBook("BVUpdate"), dtFiscal) > 0 Then
    '                                Dim CurrentBV As Double = CDbl(dtrAstBook("CurrentBV"))

    '                                If check_DepHistory(dtrAstBook("BookID"), dtrAstBook("AstID"), CDate(dtrAstBook("BVUpdate")), dtFiscal) Then
    '                                    Dim dtLog As DataTable = Get_Latest_ChangeLog(dtrAstBook("BookID"), dtrAstBook("AstID"), CDate(dtrAstBook("BVUpdate")), dtFiscal)
    '                                    If dtLog Is Nothing = False Then
    '                                        ' CurrentBV = CDbl(dtLog.Rows(0)("CurrentBV"))
    '                                        DeprValue = CalcDep(dtrAstBook("SalvageYear"), dtrAstBook("SalvageMonth"), dtrAstBook("DepCode"), Price, dtrAstBook("SalvageValue"), dtFiscal, dtLog.Rows(0)("BVUpdate"))
    '                                    Else
    '                                        DeprValue = CalcDep(dtrAstBook("SalvageYear"), dtrAstBook("SalvageMonth"), dtrAstBook("DepCode"), Price, dtrAstBook("SalvageValue"), dtFiscal, dtrAstBook("BVUpdate"))
    '                                    End If
    '                                Else
    '                                    DeprValue = CalcDep(dtrAstBook("SalvageYear"), dtrAstBook("SalvageMonth"), dtrAstBook("DepCode"), Price, dtrAstBook("SalvageValue"), dtFiscal, dtrAstBook("BVUpdate"))
    '                                End If
    '                                totDepValue += DeprValue
    '                                totAstValue += (CurrentBV - DeprValue)

    '                                'Creating History
    '                                objattBookHistory = New attBookHistory
    '                                objattBookHistory.PKeyCode = objBALBookHistory.GetNextPKey_BookHistory()
    '                                objattBookHistory.BookID = dtrAstBook("BookID")
    '                                objattBookHistory.ASTID = dtrAstBook("AstID")
    '                                objattBookHistory.DepCode = dtrAstBook("DepCode")
    '                                objattBookHistory.SalvageYear = dtrAstBook("SalvageYear")
    '                                objattBookHistory.SalvageMonth = dtrAstBook("SalvageMonth")
    '                                objattBookHistory.CurrentBV = (CurrentBV - DeprValue)
    '                                objattBookHistory.DepValue = (DeprValue)
    '                                objattBookHistory.AccDepValue = (Price - objattBookHistory.CurrentBV)
    '                                objattBookHistory.DepDate = dtFiscal
    '                                objBALBookHistory.Insert_BookHistory(objattBookHistory)

    '                                If Round(objattBookHistory.CurrentBV, 2) = Round(dtrAstBook("SalvageValue"), 2) Then
    '                                    Dim objAttAstDetails As New attAssetDetails
    '                                    Dim objBALAstDetails As New BALAssetDetails
    '                                    objAttAstDetails.PKeyCode = dtrAsset("AstID")
    '                                    objBALAstDetails.Dispose_Asset(objAttAstDetails)
    '                                    objAttAstDetails = Nothing : objAttAstDetails = Nothing
    '                                End If


    '                                'historyCount += 1
    '                                'Historylbl.Text = historyCount
    '                                'Historylbl.Refresh()

    '                                '*********  by the end of this line the Book history is added **** 
    '                                Dim objattAstBooks1 As New attAstBooks
    '                                Dim objBALAstBooks1 As New BALAstBooks
    '                                objattAstBooks1.DepCode = dtrAstBook("DepCode")
    '                                objattAstBooks1.SalvageValue = Round(dtrAstBook("SalvageValue"), 2)
    '                                objattAstBooks1.SalvageYear = Round(dtrAstBook("SalvageYear"), 2)
    '                                objattAstBooks1.SalvageMonth = Round(dtrAstBook("SalvageMonth"), 2)
    '                                objattAstBooks1.PKeyCode = objattBookHistory.BookID
    '                                objattAstBooks1.AstID = objattBookHistory.ASTID
    '                                objattAstBooks1.LastBookValue = CurrentBV
    '                                objattAstBooks1.CurrentBookValue = CurrentBV - DeprValue
    '                                objattAstBooks1.BVUpdate = dtFiscal
    '                                objBALAstBooks1.Update_AstBooks(objattAstBooks1)

    '                                Dim dtSrvDispose As Date
    '                                dtSrvDispose = dtSrv
    '                                dtSrvDispose = dtSrvDispose.AddMonths(dtrAstBook("SalvageMonth"))
    '                                dtSrvDispose = dtSrvDispose.AddYears(dtrAstBook("SalvageYear"))
    '                                If dtSrvDispose <= dtFiscal Then
    '                                    Dim objAttAstDetails As New attAssetDetails
    '                                    Dim objBALAstDetails As New BALAssetDetails
    '                                    objAttAstDetails.PKeyCode = dtrAsset("AstID")
    '                                    objBALAstDetails.Dispose_Asset(objAttAstDetails)
    '                                    objAttAstDetails = Nothing : objAttAstDetails = Nothing
    '                                End If
    '                            Else ' Dispose Asset

    '                                Dim dtSrvDispose As Date
    '                                dtSrvDispose = CDate(dtrAstBook("BVUpdate"))
    '                                dtSrvDispose = dtSrvDispose.AddMonths(dtrAstBook("SalvageMonth"))
    '                                dtSrvDispose = dtSrvDispose.AddYears(dtrAstBook("SalvageYear"))

    '                                Dim CurrentBV As Double = CDbl(dtrAstBook("CurrentBV"))
    '                                If check_DepHistory(dtrAstBook("BookID"), dtrAstBook("AstID"), CDate(dtrAstBook("BVUpdate")), dtFiscal) Then
    '                                    Dim dtLog As DataTable = Get_Latest_ChangeLog(dtrAstBook("BookID"), dtrAstBook("AstID"), CDate(dtrAstBook("BVUpdate")), dtSrvDispose)
    '                                    If dtLog Is Nothing = False Then
    '                                        CurrentBV = CDbl(dtLog.Rows(0)("CurrentBV"))
    '                                        DeprValue = CalcDep(dtrAstBook("SalvageYear"), dtrAstBook("SalvageMonth"), dtrAstBook("DepCode"), Price, dtrAstBook("SalvageValue"), dtSrvDispose, dtLog.Rows(0)("BVUpdate"))
    '                                    Else
    '                                        DeprValue = CalcDep(dtrAstBook("SalvageYear"), dtrAstBook("SalvageMonth"), dtrAstBook("DepCode"), Price, dtrAstBook("SalvageValue"), dtSrvDispose, dtrAstBook("BVUpdate"))
    '                                    End If
    '                                Else
    '                                    DeprValue = CalcDep(dtrAstBook("SalvageYear"), dtrAstBook("SalvageMonth"), dtrAstBook("DepCode"), Price, dtrAstBook("SalvageValue"), dtSrvDispose, dtrAstBook("BVUpdate"))
    '                                End If
    '                                totDepValue += DeprValue
    '                                totAstValue += (CurrentBV - DeprValue)

    '                                'Creating History
    '                                objattBookHistory = New attBookHistory
    '                                objattBookHistory.PKeyCode = objBALBookHistory.GetNextPKey_BookHistory()
    '                                objattBookHistory.BookID = dtrAstBook("BookID")
    '                                objattBookHistory.ASTID = dtrAstBook("AstID")
    '                                objattBookHistory.DepCode = dtrAstBook("DepCode")
    '                                objattBookHistory.SalvageYear = dtrAstBook("SalvageYear")
    '                                objattBookHistory.SalvageMonth = dtrAstBook("SalvageMonth")
    '                                objattBookHistory.CurrentBV = Round(CurrentBV - DeprValue)
    '                                objattBookHistory.DepValue = Round(DeprValue)
    '                                objattBookHistory.AccDepValue = Round(Price - objattBookHistory.CurrentBV)
    '                                objattBookHistory.DepDate = dtSrvDispose
    '                                objBALBookHistory.Insert_BookHistory(objattBookHistory)

    '                                'historyCount += 1
    '                                'Historylbl.Text = historyCount
    '                                'Historylbl.Refresh()

    '                                '*********  by the end of this line the Book history is added **** 
    '                                Dim objattAstBooks1 As New attAstBooks
    '                                Dim objBALAstBooks1 As New BALAstBooks
    '                                objattAstBooks1.DepCode = dtrAstBook("DepCode")
    '                                objattAstBooks1.SalvageValue = Round(dtrAstBook("SalvageValue"), 2)
    '                                objattAstBooks1.SalvageYear = Round(dtrAstBook("SalvageYear"), 2)
    '                                objattAstBooks1.SalvageMonth = Round(dtrAstBook("SalvageMonth"), 2)
    '                                objattAstBooks1.PKeyCode = objattBookHistory.BookID
    '                                objattAstBooks1.AstID = objattBookHistory.ASTID
    '                                objattAstBooks1.LastBookValue = CurrentBV
    '                                objattAstBooks1.CurrentBookValue = CurrentBV - DeprValue
    '                                objattAstBooks1.BVUpdate = dtSrvDispose
    '                                objBALAstBooks1.Update_AstBooks(objattAstBooks1)

    '                                Dim objAttAstDetails As New attAssetDetails
    '                                Dim objBALAstDetails As New BALAssetDetails
    '                                objAttAstDetails.PKeyCode = dtrAsset("AstID")
    '                                objBALAstDetails.Dispose_Asset(objAttAstDetails)
    '                                objAttAstDetails = Nothing : objAttAstDetails = Nothing
    '                            End If

    '                        Else
    '                            If SalMonth = 0 Then
    '                                Dim objAttAstDetails As New attAssetDetails
    '                                Dim objBALAstDetails As New BALAssetDetails
    '                                objAttAstDetails.PKeyCode = dtrAsset("AstID")
    '                                objBALAstDetails.Dispose_Asset(objAttAstDetails)
    '                                objAttAstDetails = Nothing : objAttAstDetails = Nothing
    '                            End If
    '                            'DisposedCount += 1
    '                            'DisposedLbl.Text = DisposedCount
    '                            'DisposedLbl.Refresh()
    '                        End If
    '                    Else
    '                        'LessDateCount += 1
    '                        'LessDateLbl.Text = LessDateCount
    '                        'LessDateLbl.Refresh()
    '                    End If

    '                Next

    '                'pb.Value = pb.Value + 1
    '                'pb.Refresh()

    '                'logCount += 1
    '                'Loglbl.Text = logCount
    '                'Loglbl.Refresh()
    '            Else
    '                'NoDetailCount += 1
    '                'NoDetailLbl.Text = NoDetailCount
    '                'NoDetailLbl.Refresh()
    '            End If
    '        Next
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    '    threadCount -= 1
    '    EndDepProcess(dtFiscal, BookID)
    'End Sub
#End Region


    Private Function Get_Latest_ChangeLog(ByVal BookId As String, ByVal AstId As String, ByVal Lastupdate As Date, ByVal ThisUpdate As Date) As DataTable
        Dim ObjDepPolicy_History As New BALDepPolicy_History
        Dim dt As DataTable
        dt = ObjDepPolicy_History.Get_Latest_History(BookId, AstId, Lastupdate, ThisUpdate)
        If dt Is Nothing = False Then
            If dt.Rows.Count > 0 Then
                Return dt
            End If
        End If
        Return Nothing
    End Function

#Region " -- Process to update Book History -- "
    Private Function check_DepHistory(ByVal BookId As String, ByVal AstId As String, ByVal Lastupdate As Date, ByVal ThisUpdate As Date) As Boolean
        Dim ObjDepPolicy_History As New BALDepPolicy_History
        Return ObjDepPolicy_History.Check_DepPolicy_History(BookId, AstId, Lastupdate, ThisUpdate)
        ' Dim objattDepPolicy_History As New attDepPolicy_History
    End Function

    'Public Sub UpdateAstBookHist(ByVal Days As Integer, ByVal Month As Integer, ByVal year As Integer, ByVal BookID As Integer)
    '    Try
    '        Dim Price, CurBook As Double
    '        Dim dtUpto, dtSrv, dtDisp, dtSale As Date
    '        Dim Disposed, Sold As Boolean
    '        Dim astCount As Integer
    '        Dim totDepValue, TotAstValue As Double
    '        Dim dtFiscal As Date = New Date(year, Month, Days)
    '        Dim dtAstBooks As DataTable

    '        ''====================== Code for Updating Books History =====================
    '        totDepValue = 0.0
    '        TotAstValue = 0.0
    '        pb.Visible = True
    '        pb.Value = 0

    '        dtAstBooks = Get_AstBooks("", BookID)

    '        If Not dtAssets Is Nothing Then ' Make Sure that Assets are there in Database

    '            astCount = dtAssets.Rows.Count
    '            pb.Maximum = astCount
    '            For Each dtrAstBook As DataRow In dtAstBooks.Rows
    '                Get_Assets_ByID(dtrAstBook("AstID").ToString())



    '                If Sold Then
    '                    dtUpto = dtSale
    '                End If
    '                If Disposed Then
    '                    dtUpto = dtDisp
    '                End If
    '                '********** by the End of this Line all info Required from AssetDetails is Ready  *********

    '                'Exit Sub
    '                'dtAstBooks = Get_AstBooks(dtrAsset("AstID").ToString(), BookID)


    '                For Each dtrAsset As DataRow In dtAssets.Rows
    '                    Price = CDbl(dtrAsset("BaseCost").ToString()) + CDbl(dtrAsset("Tax").ToString())
    '                    If Not dtrAsset("SrvDate").ToString Is "" Then
    '                        dtSrv = dtrAsset("SrvDate").ToString()
    '                    End If

    '                    If Not dtrAsset("Disposed").ToString Is "" Then
    '                        Disposed = dtrAsset("Disposed").ToString()
    '                    End If


    '                    If Not dtrAsset("IsSold").ToString Is "" Then
    '                        Sold = dtrAsset("IsSold").ToString()
    '                    End If

    '                    If Date.Compare(dtSrv, dtFiscal).ToString() < 0 Then

    '                        If dtrAstBook("SalvageYear") <> "0" Then
    '                            If dtrAstBook("SalvageYear") > DateDiff(DateInterval.Year, dtSrv, dtFiscal) Then

    '                                ' MnthLeft = DateDiff(DateInterval.Month, ServiceDate, fiscalYr)
    '                                CalCulate_Depreciation(dtrAstBook("SalvageYear"), dtrAstBook("DepCode"), Price, dtrAstBook("SalvageValue"), dtFiscal, dtSrv)
    '                            Else
    '                                CalCulate_Depreciation(dtrAstBook("SalvageYear"), dtrAstBook("DepCode"), Price, dtrAstBook("SalvageValue"), dtSrv.AddYears(dtrAstBook("SalvageYear")), dtSrv)

    '                            End If

    '                            ' 
    '                            If Not dtrAstBook("CurrentBV").ToString Is "" Then
    '                                CurBook = dtrAstBook("CurrentBV")
    '                            End If

    '                            Dim objattBookHistory As New attBookHistory
    '                            objattBookHistory.BookID = dtrAstBook("BookID")
    '                            objattBookHistory.ASTID = dtrAstBook("AstID")
    '                            objattBookHistory.DepCode = dtrAstBook("DepCode")
    '                            objattBookHistory.AccDepValue = Round(depValue, 2)
    '                            objattBookHistory.DepDate = Date.Now
    '                            objattBookHistory.CurrentBV = Round(Price - objattBookHistory.AccDepValue, 2)
    '                            objattBookHistory.DepValue = Round(CurBook - objattBookHistory.CurrentBV, 2)

    '                            objBALBookHistory.Insert_BookHistory(objattBookHistory)
    '                            '*********  by the end of this line the Book history is added **** 
    '                            Dim objattAstBooks1 As New attAstBooks
    '                            Dim objBALAstBooks1 As New BALAstBooks
    '                            objattAstBooks1.DepCode = dtrAstBook("DepCode")
    '                            objattAstBooks1.SalvageValue = Round(dtrAstBook("SalvageValue"), 2)
    '                            objattAstBooks1.SalvageYear = Round(dtrAstBook("SalvageYear"), 2)
    '                            ' objattAstBooks1.BookDescription = dtrAstBook("BookDescription")
    '                            objattAstBooks1.PKeyCode = objattBookHistory.BookID
    '                            objattAstBooks1.AstID = objattBookHistory.ASTID
    '                            objattAstBooks1.LastBookValue = Round(CurBook, 2)
    '                            objattAstBooks1.CurrentBookValue = Round(objattBookHistory.CurrentBV, 2)
    '                            objattAstBooks1.BVUpdate = Date.Now
    '                            objBALAstBooks1.Update_AstBooks(objattAstBooks1)
    '                            If dtrAstBook("SalvageYear") = DateDiff(DateInterval.Year, dtSrv, dtFiscal) Then
    '                                Dim objAttAstDetails As New attAssetDetails
    '                                Dim objBALAstDetails As New BALAssetDetails
    '                                objAttAstDetails.PKeyCode = dtrAstBook("AstID")
    '                                objBALAstDetails.Dispose_Asset(objAttAstDetails)
    '                            End If
    '                            '******** by the end of this line astbook information will be updated
    '                            '     End If
    '                        End If
    '                    End If
    '                Next

    '                pb.Value = pb.Value + 1
    '                'End of Book Loop
    '            Next

    '            objattDepLogs = New attDepLogs
    '            objattDepLogs.UpdDate = dtFiscal
    '            'objattDepLogs.UpdMonth = Month()
    '            'objattDepLogs.UpdYear = Year()
    '            objattDepLogs.BookID = BookID
    '            objattDepLogs.TotAstCount = dtAssets.Rows.Count
    '            objattDepLogs.TotAstValue = Round(totDepValue, 2)
    '            objattDepLogs.TotDepValue = Round(TotAstValue, 2)
    '            objBALDepLogs.Insert_DepLogs(objattDepLogs)

    '            '  End If
    '            ' ======================== End of Code of Dep History  ==============================

    '        End If
    '    Catch ex As Exception
    '                    GenericExceptionHandler(ex, WhoCalledMe)

    '    End Try
    'End Sub

    Public Shared Function Round(ByVal Number As Double, Optional ByVal NumDigitsAfterDecimal As Integer = 0) As Double
        Return CDbl(FormatNumber(Number, NumDigitsAfterDecimal))
    End Function
#End Region



#Region " --  DB Functions --"
    Public Shared Function Get_Assets_ByID(ByVal AstId As String) As DataTable
        Try
            Dim ds As New DataTable
            objattAssetDetails = New attAssetDetails
            '    objattAssetDetails.IsSold = False
            '   objattAssetDetails.Disposed = False
            objattAssetDetails.PKeyCode = AstId
            ds = objBALAssetDetails.GetAllData_Active(objattAssetDetails)
            If ds Is Nothing = False Then
                'If ds.Tables.Count > 0 Then
                If ds.Rows.Count > 0 Then
                    'dtAssets = ds
                    'Return Nothing
                    Return ds
                End If
            End If
            '     End If
            dtAssets = Nothing
            Return Nothing
        Catch ex As Exception
            GenericExceptionHandler(ex, "Get_Assets_ById")
            Return Nothing
        End Try
    End Function

    Public Shared Function Get_AstBooks(ByVal _id As String, ByVal BookID As String) As DataTable
        Try
            Dim ds As New DataTable
            Dim objattAstBooks As New attAstBooks
            objattAstBooks.AstID = _id
            objattAstBooks.PKeyCode = BookID
            ds = objBALAstBooks.GetAll_AstBooks(objattAstBooks)
            If ds Is Nothing = False Then
                'If ds.Tables.Count > 0 Then
                'dtAstBooks = ds
                Return ds
            End If
            ' End If
            'dtAstBooks = Nothing
            Return Nothing
        Catch ex As Exception
            GenericExceptionHandler(ex, "Get_AstBooks")
            Return Nothing
        End Try
    End Function

    Public Shared Function Check_Get_DepLogs(ByVal dtupdate As Date, ByVal byMonth As Boolean, ByVal Month As Int32, ByVal Year As Int32, ByVal BookID As Integer) As Boolean
        Try
            Dim ds As New DataTable
            objattDepLogs = New attDepLogs
            If byMonth Then
                objattDepLogs.UpdMonth = Month
                objattDepLogs.UpdYear = Year
                objattDepLogs.BookID = BookID
            Else
                objattDepLogs.UpdMonth = 0
                objattDepLogs.UpdYear = 0
                objattDepLogs.UpdDate = dtupdate
                objattDepLogs.BookID = BookID
            End If
            ds = objBALDepLogs.GetAll_DepLogs(objattDepLogs)
            If ds Is Nothing = False Then
                'If ds.Tables.Count > 0 Then
                If ds.Rows.Count > 0 Then
                    Return True
                End If
            End If
            '   End If
            ' dtAssets = Nothing
            Return False
        Catch ex As Exception
            GenericExceptionHandler(ex, "Check_Get_DepLogs")
        End Try
    End Function

    Public Shared Function Check_Get_DepLogs(ByVal dtupdate As Date, ByVal BookID As Integer) As Boolean
        Try
            Dim ds As New DataTable
            objattDepLogs = New attDepLogs
            objattDepLogs.BookID = BookID
            objattDepLogs.UpdDate = dtupdate.ToShortDateString()
            ds = objBALDepLogs.GetAll_DepLogs_Date(objattDepLogs)
            If ds Is Nothing = False Then
                'If ds.Tables.Count > 0 Then
                If ds.Rows.Count > 0 Then
                    Return True
                End If
            End If
            '  End If
            ' dtAssets = Nothing
            Return False
        Catch ex As Exception
            GenericExceptionHandler(ex, "Check_Get_DepLogs")
        End Try
    End Function
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

#End Region
End Class