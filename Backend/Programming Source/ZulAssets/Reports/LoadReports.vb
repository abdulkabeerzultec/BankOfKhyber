Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports
Public Class LoadReports

    Public Shared Function LoadMasterReport(ByVal ReportName As String, ByVal rptDS As DataTable) As XtraReport
        Try
            Dim objBALRptFile As New BALReports
            Dim rpt As New XtraReport
            ' Retrieve a string which contains the report.
            Dim ds As DataTable = objBALRptFile.GetReportFileData(ReportName)
            If ds.Rows.Count > 0 Then
                Dim s As String = ds.Rows(0)("ReportData").ToString
                ' Obtain the report from the string.
                Dim sw As New StreamWriter(New MemoryStream())
                Try
                    sw.Write(s)
                    sw.Flush()
                    rpt = XtraReport.FromStream(sw.BaseStream, True)
                Finally
                    sw.Dispose()
                End Try

                rpt.DataSource = rptDS
                rpt.RequestParameters = True
                rpt.PrinterName = AppConfig.ReportPrinter
                rpt.PrintingSystem.ShowMarginsWarning = False
                rpt.FindControl("lblCompName", False).Text = AppConfig.CompanyName
                CType(rpt.FindControl("picLogo", False), XRPictureBox).Image = CompanyLogoImage


                Dim fmt As String
                If AppConfig.MaindateFormat = "dd/MM/yyyy" Then
                    fmt = "{0:dd/MM/yyyy}"
                Else
                    fmt = "{0:MM/dd/yyyy}"
                End If
                If ReportName = ReportNames.InventorySchedules Then
                    If rpt.FindControl("tblCellStartDate", False) IsNot Nothing Then
                        Dim binding1 As New XRBinding("Text", rpt.DataSource, "Ast_INV_Schedule.InvStartDate", fmt)
                        rpt.FindControl("tblCellStartDate", False).DataBindings.Add(binding1)
                    End If
                    If rpt.FindControl("tblCellEndDate", False) IsNot Nothing Then
                        Dim binding2 As New XRBinding("Text", rpt.DataSource, "Ast_INV_Schedule.InvEndDate", fmt)
                        rpt.FindControl("tblCellEndDate", False).DataBindings.Add(binding2)
                    End If
                ElseIf ReportName = ReportNames.DisposedAssets Then
                    If rpt.FindControl("lblDispDate", False) IsNot Nothing Then
                        Dim binding1 As New XRBinding("Text", rpt.DataSource, "DisposedAssets.DisposedAssets.DispDate", fmt)
                        rpt.FindControl("lblDispDate", False).DataBindings.Add(binding1)
                    End If
                    If rpt.FindControl("lblSoldDate", False) IsNot Nothing Then
                        Dim binding2 As New XRBinding("Text", rpt.DataSource, "DisposedAssets.Sel_Date", fmt)
                        rpt.FindControl("lblSoldDate", False).DataBindings.Add(binding2)
                    End If
                ElseIf ReportName = ReportNames.AssetDetails Then
                    If rpt.FindControl("lblSrvDate", False) IsNot Nothing Then
                        Dim binding As New XRBinding("Text", rpt.DataSource, "AssetDetail.SrvDate", fmt)
                        rpt.FindControl("lblSrvDate", False).DataBindings.Add(binding)
                    End If
                    If rpt.FindControl("lblPurDate", False) IsNot Nothing Then
                        Dim binding1 As New XRBinding("Text", rpt.DataSource, "AssetDetail.PurDate", fmt)
                        rpt.FindControl("lblPurDate", False).DataBindings.Add(binding1)
                    End If
                End If
                Return rpt
            Else
                ZulMessageBox.ShowMe("ReportNotFound", MessageBoxButtons.OK, MessageBoxIcon.Error, True)
                Return Nothing
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
            Return Nothing
        End Try
    End Function

    Public Shared Function LoadAuditReport(ByVal ReportName As String, ByVal InvScheduleCode As Integer, ByVal InvSchName As String, ByVal StartDate As String, ByVal EndDate As String, ByVal IsTempData As Boolean, ByVal LocationID As String, ByVal CatID As String, ByVal IncludeChild As Boolean) As XtraReport
        Dim rpt As New XtraReport
        Dim objBALOrgHier As New BALOrgHier
        Dim objBALRptFile As New BALReports

        ' Retrieve a string which contains the report from the database.
        Dim dsrpt As DataTable = objBALRptFile.GetReportFileData(ReportName)
        If dsrpt.Rows.Count > 0 Then
            Dim s As String = dsrpt.Rows(0)("ReportData").ToString
            ' Obtain the report from the string.
            Dim sw As New StreamWriter(New MemoryStream())
            Try
                sw.Write(s)
                sw.Flush()
                rpt = XtraReport.FromStream(sw.BaseStream, True)
            Finally
                sw.Dispose()
            End Try
            Dim Status As String = ""
            Select Case ReportName
                Case ReportNames.MissingAssets
                    Status = 0
                Case ReportNames.FoundAssets
                    Status = 1
                Case ReportNames.MisplacedAssets
                    Status = 2
                Case ReportNames.TransferredAssets
                    Status = 3
                Case ReportNames.AllocatedAssets
                    Status = 4
                Case ReportNames.AnonymousAssets
                    Status = 5
                    'Case ReportNames.AllAssets
                    'it will show all bands in the reports.
            End Select

            Dim fmt As String
            If AppConfig.MaindateFormat = "dd/MM/yyyy" Then
                fmt = "{0:dd/MM/yyyy}"
            Else
                fmt = "{0:MM/dd/yyyy}"
            End If

            If Trim(InvScheduleCode) = "" Then
                rpt.FindControl("pnlSchd", False).Visible = False
            Else
                rpt.FindControl("lblSchCode", False).Text = InvScheduleCode
                rpt.FindControl("lblSchDesc", False).Text = InvSchName

                rpt.FindControl("lblSDate", False).Text = Format(CDate(StartDate), AppConfig.MaindateFormat)
                rpt.FindControl("lblEDate", False).Text = Format(CDate(EndDate), AppConfig.MaindateFormat)
            End If

            If rpt.FindControl("tblCellHisDate", False) IsNot Nothing Then
                Dim binding As New XRBinding("Text", rpt.DataSource, "AuditSatus.HisDate", fmt)
                rpt.FindControl("tblCellHisDate", False).DataBindings.Add(binding)
            End If

            rpt.FindControl("lblRptFor", False).Text = ReportName
            rpt.PrinterName = AppConfig.ReportPrinter
            rpt.PrintingSystem.ShowMarginsWarning = False
            rpt.FindControl("lblCompName", False).Text = AppConfig.CompanyName
            CType(rpt.FindControl("picLogo", False), XRPictureBox).Image = CompanyLogoImage

            Dim dt As DataTable
            If IsTempData Then 'Get the data from temp table.

                Dim objBALAnonymAssets As New BALAnonymousAsset
                Dim objBALLocation As New BALLocation
                Dim objBALDevices As New BALDevices
                Dim objBALCategory As New BALCategory

                dt = objBALAnonymAssets.GetAll_AnonymousAsset()

                'Ast_history.Status,AuditStatus.StatusDesc
                Dim dcCurrentLoc As DataColumn = dt.Columns.Add("NewLoc", Type.GetType("System.String"))
                Dim dcPrevLoc As DataColumn = dt.Columns.Add("PrevLoc", Type.GetType("System.String"))
                Dim dcCat As DataColumn = dt.Columns.Add("CatFullPath", Type.GetType("System.String"))
                Dim dcDeviceDesc As DataColumn = dt.Columns.Add("DeviceDesc", Type.GetType("System.String"))
                Dim dcStatus As DataColumn = dt.Columns.Add("Status", Type.GetType("System.String"))
                Dim dcStatusDesc As DataColumn = dt.Columns.Add("StatusDesc", Type.GetType("System.String"))
                For i As Integer = 0 To dt.Rows.Count - 1
                    If Not String.IsNullOrEmpty(dt.Rows(i)("LocID").ToString) Then
                        dt.Rows(i)("NewLoc") = objBALLocation.GetLocFullPath(dt.Rows(i)("LocID").ToString)
                        dt.Rows(i)("PrevLoc") = objBALLocation.GetLocFullPath(dt.Rows(i)("LocID").ToString)
                    End If
                    dt.Rows(i)("CatFullPath") = objBALCategory.GetCatFullPath(dt.Rows(i)("AstCatID").ToString)
                    dt.Rows(i)("DeviceDesc") = objBALDevices.GetDeviceDescription(dt.Rows(i)("DeviceID").ToString)
                    dt.Rows(i)("Status") = 5 'Anonymous.
                    dt.Rows(i)("StatusDesc") = "Anonymous" 'Anonymous.
                Next
                rpt.DataSource = dt
            Else
                Dim objattAst_INV_Schedule1 As New attInvSchedule
                objattAst_INV_Schedule1.PKeyCode = InvScheduleCode
                Dim objBALStandardReports As New BALStandardReports

                If ReportName = ReportNames.CostCenterAudit Then
                    Dim ds As New ZulAssetsBEDataSet.CostCenterAuditStatusDataTable
                    dt = objBALStandardReports.GetCostCenter_AuditReport(objattAst_INV_Schedule1, Status, LocationID, CatID, IncludeChild)
                    ds.Merge(dt) 'we are converting the datatable to CostCenterAuditStatusDataTable because pivotgrid will not show data if not converted.
                    If rpt.FindControl("XrPivotGrid1", False) IsNot Nothing Then
                        AddHandler DirectCast(rpt.FindControl("XrPivotGrid1", False), XRPivotGrid).FieldValueDisplayText, AddressOf XrPivotGrid1_FieldValueDisplayText
                        DirectCast(rpt.FindControl("XrPivotGrid1", False), XRPivotGrid).DataSource = ds
                    End If
                Else
                    dt = objBALStandardReports.GetAll_AuditStatusReport(objattAst_INV_Schedule1, Status, LocationID, CatID, IncludeChild)
                    rpt.DataSource = dt
                End If
            End If

            Return rpt
        Else
            ZulMessageBox.ShowMe("ReportNotFound", MessageBoxButtons.OK, MessageBoxIcon.Error, True)
            Return Nothing
        End If
    End Function

    Private Shared Sub XrPivotGrid1_FieldValueDisplayText(ByVal sender As Object, ByVal e As DevExpress.XtraPivotGrid.PivotFieldDisplayTextEventArgs)

        If (Not e.IsColumn AndAlso ((e.Field Is Nothing) OrElse (e.Field.AreaIndex = 0))) AndAlso Not e.ValueType = DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal Then
            e.DisplayText = (e.MinIndex + 1).ToString
        End If

    End Sub
End Class
