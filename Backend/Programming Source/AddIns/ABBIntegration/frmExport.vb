Imports DevExpress.XtraGrid
Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Windows.Forms

Public Class frmExport
    Public Enum TFileExportType
        AssetChange
        PhysicalInventory
        GoodsReceive
        GoodsIssuance
        ReversalGRDoc
        ReversalGIDoc
        VendorReturn
    End Enum

    Private _ExportType As New TFileExportType
    Private _ExportFileName As String = ""
    Private _ExportFolderName As String = ""

    Private ExportDataTable As DataTable

    Dim objExport As New ZulAssetsBL.SAPExportBLL

    Public Property ExportFileName() As String
        Get
            Return _ExportFileName
        End Get
        Set(ByVal value As String)
            _ExportFileName = value
        End Set
    End Property

    Public Property ExportFolderName() As String
        Get
            Return _ExportFolderName
        End Get
        Set(ByVal value As String)
            _ExportFolderName = value
        End Set
    End Property

    Public Property ExportType() As TFileExportType
        Get
            Return _ExportType
        End Get
        Set(ByVal value As TFileExportType)
            _ExportType = value
            If value = TFileExportType.AssetChange Then
                btnSelectAll.Visible = True
            Else
                btnSelectAll.Visible = False
            End If
        End Set
    End Property

    Private Sub frmExport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ZulAssets
        Me.BackgroundImage = My.Resources.Forms_BG
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub frmExport_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Select Case ExportType
            Case TFileExportType.AssetChange
                GetAssetChangeData()
                FormatAssetChangeGrid()
            Case TFileExportType.PhysicalInventory
                GetAssetPhysicalInventoryData()
                FormatPhysicalInventoryGrid()
            Case TFileExportType.GoodsReceive
                GetGoodsReceivingData(False)
                FormatExportGrid()
            Case TFileExportType.GoodsIssuance
                GetGoodsIssuanceData(False)
                FormatExportGrid()
            Case TFileExportType.ReversalGRDoc
                GetReversalGRData(False)
                FormatExportGrid()
            Case TFileExportType.ReversalGIDoc
                GetReversalGIData(False)
                FormatExportGrid()
            Case TFileExportType.VendorReturn
                GetVendorReturnData(False)
                FormatExportGrid()
        End Select
    End Sub

    Public Function ExportDataFile(ByVal lbl As DevExpress.XtraEditors.LabelControl, ByVal pb As DevExpress.XtraEditors.ProgressBarControl, ByVal FileName As String, ByVal ExportType As frmExport.TFileExportType, ByVal SilentProcess As Boolean, ByVal Append As Boolean) As String
        Select Case ExportType
            Case TFileExportType.AssetChange
                lbl.Text = "Processing Data Asset Change file..."
                lbl.Update()
                If CheckFile(FileName) Then
                    If SilentProcess Then 'Get the data again if SilentProcess is activated, means if export ALL button clicked.
                        GetAssetChangeData()
                    End If
                    Application.DoEvents()
                    lbl.Text = "Export Asset Change file..."
                    lbl.Update()
                    If grdView.RowCount > 0 Then
                        Dim ExportedRecordCount As Integer = ExportChangedData(ExportDataTable, FileName, pb, Append)
                        If ExportedRecordCount >= 0 Then
                            Dim msg As String = "Export Asset Change file Completed, exported Asset count = (" & ExportedRecordCount.ToString & ")"
                            If Not SilentProcess Then
                                MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                SaveToLogFile(msg, False)
                            End If
                            Return msg
                        Else
                            Dim msg As String = String.Format("Error while exporting file path({0})", FileName)
                            SaveToLogFile(msg, False)
                            Return msg
                        End If
                    Else
                        Dim msg As String = "AssetChange file, no data to export."
                        SaveToLogFile(msg, False)
                        lbl.Text = String.Format(msg, FileName)
                        lbl.Update()
                        Return msg
                    End If
                Else
                    Dim msg As String = String.Format("File not found, Incorrect path({0})", FileName)
                    SaveToLogFile(msg, True)
                    lbl.Text = msg
                    lbl.Update()
                    Return msg
                End If

            Case TFileExportType.PhysicalInventory
                lbl.Text = "Processing Data Physical Inventory file..."
                lbl.Update()
                'If InventorySch > 0 Then
                If CheckFile(FileName) Then
                    If SilentProcess Then 'Get the data again if SilentProcess is activated, means if export ALL button clicked.
                        GetAssetPhysicalInventoryData()
                    End If
                    Application.DoEvents()
                    lbl.Text = "Export Physical Inventory file..."
                    lbl.Update()
                    If grdView.RowCount > 0 Then
                        If ExportDatasetToFile(ExportDataTable, FileName, pb, Append) Then
                            Dim msg As String = "Export Physical Inventory file Completed, exported Asset count = (" & ExportDataTable.Rows.Count & ")"
                            If Not SilentProcess Then
                                MessageBox.Show(Msg, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                SaveToLogFile(msg, False)
                            End If
                            Return msg
                        Else
                            Dim msg As String = String.Format("Error while exporting file path({0})", FileName)
                            SaveToLogFile(msg, False)
                            Return msg
                        End If
                    Else
                        Dim msg As String = "Physical Inventory file, no data to export."
                        SaveToLogFile(msg, False)
                        lbl.Text = String.Format(msg, FileName)
                        lbl.Update()
                        Return msg
                    End If
                Else
                    Dim msg As String = String.Format("File not found, Incorrect path({0})", FileName)
                    SaveToLogFile(msg, True)
                    lbl.Text = msg
                    lbl.Update()
                    Return msg
                End If
            Case TFileExportType.GoodsReceive
                lbl.Text = "Processing Goods Receiving file..."
                lbl.Update()
                If CheckFolder(IO.Path.GetDirectoryName(FileName)) Then
                    If SilentProcess Then 'Get the data again if SilentProcess is activated, means if export ALL button clicked.
                        GetGoodsReceivingData(True)
                    End If
                    Application.DoEvents()
                    lbl.Text = "Export Goods Receiving file..."
                    lbl.Update()
                    If grdView.RowCount > 0 Then
                        If ExportDatasetToFile(ExportDataTable, FileName, pb, Append) Then
                            Dim msg As String = "Export Goods Receiving file Completed, exported items count = " & ExportDataTable.Rows.Count & ")"
                            If Not SilentProcess Then
                                MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                SaveToLogFile(msg, False)
                            End If
                            'set ExportedFlag = 1
                            objExport.UpdateExportFlagByDocType("GR")
                            Return msg
                        Else
                            Dim msg As String = String.Format("Error while exporting file path({0})", FileName)
                            SaveToLogFile(msg, False)
                            Return msg
                        End If
                    Else
                        Dim msg As String = "Goods Receiving file, no data to export."
                        SaveToLogFile(msg, False)
                        lbl.Text = String.Format(msg, FileName)
                        lbl.Update()
                        Return msg
                    End If
                Else
                    Dim msg As String = String.Format("File not found, Incorrect path({0})", FileName)
                    SaveToLogFile(msg, True)
                    lbl.Text = msg
                    lbl.Update()
                    Return msg
                End If

            Case TFileExportType.GoodsIssuance
                lbl.Text = "Processing Goods Issuance file..."
                lbl.Update()
                If CheckFolder(IO.Path.GetDirectoryName(FileName)) Then
                    If SilentProcess Then 'Get the data again if SilentProcess is activated, means if export ALL button clicked.
                        GetGoodsIssuanceData(True)
                    End If

                    Application.DoEvents()
                    lbl.Text = "Export Goods Issuance file..."
                    lbl.Update()
                    If grdView.RowCount > 0 Then
                        If ExportDatasetToFile(ExportDataTable, FileName, pb, Append) Then
                            Dim msg As String = "Export Goods Issuance file Completed, exported items count = " & ExportDataTable.Rows.Count & ")"
                            If Not SilentProcess Then
                                MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                SaveToLogFile(msg, False)
                            End If

                            objExport.UpdateExportFlagByDocType("GINV")
                            objExport.UpdateExportFlagByDocType("GIPOR")
                            Return msg
                        Else
                            Dim msg As String = String.Format("Error while exporting file path({0})", FileName)
                            SaveToLogFile(msg, False)
                            Return msg
                        End If
                    Else
                        Dim msg As String = "Goods Issuance file, no data to export."
                        SaveToLogFile(msg, False)
                        lbl.Text = String.Format(msg, FileName)
                        lbl.Update()
                        Return msg
                    End If
                Else
                    Dim msg As String = String.Format("File not found, Incorrect path({0})", FileName)
                    SaveToLogFile(msg, True)
                    lbl.Text = msg
                    lbl.Update()
                    Return msg
                End If

            Case TFileExportType.ReversalGRDoc
                lbl.Text = "Processing Reversal GR file..."
                lbl.Update()
                If CheckFolder(IO.Path.GetDirectoryName(FileName)) Then
                    If SilentProcess Then 'Get the data again if SilentProcess is activated, means if export ALL button clicked.
                        GetReversalGRData(True)
                    End If

                    Application.DoEvents()
                    lbl.Text = "Export Reversal GR file..."
                    lbl.Update()
                    If grdView.RowCount > 0 Then
                        If ExportDatasetToFile(ExportDataTable, FileName, pb, Append) Then
                            Dim msg As String = "Export Reversal GR file Completed, exported items count = " & ExportDataTable.Rows.Count & ")"
                            If Not SilentProcess Then
                                MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                SaveToLogFile(msg, False)
                            End If

                            objExport.UpdateExportFlagByDocType("ReversalGR")
                            Return msg
                        Else
                            Dim msg As String = String.Format("Error while exporting file path({0})", FileName)
                            SaveToLogFile(msg, False)
                            Return msg
                        End If
                    Else
                        Dim msg As String = "Reversal GR file, no data to export."
                        SaveToLogFile(msg, False)
                        lbl.Text = String.Format(msg, FileName)
                        lbl.Update()
                        Return msg
                    End If
                Else
                    Dim msg As String = String.Format("File not found, Incorrect path({0})", FileName)
                    SaveToLogFile(msg, True)
                    lbl.Text = msg
                    lbl.Update()
                    Return msg
                End If

            Case TFileExportType.ReversalGIDoc
                lbl.Text = "Processing Reversal GI file..."
                lbl.Update()
                If CheckFolder(IO.Path.GetDirectoryName(FileName)) Then
                    If SilentProcess Then 'Get the data again if SilentProcess is activated, means if export ALL button clicked.
                        GetReversalGIData(True)
                    End If

                    Application.DoEvents()
                    lbl.Text = "Export Reversal GI file..."
                    lbl.Update()
                    If grdView.RowCount > 0 Then
                        If ExportDatasetToFile(ExportDataTable, FileName, pb, Append) Then
                            Dim msg As String = "Export Reversal GI file Completed, exported items count = " & ExportDataTable.Rows.Count & ")"
                            If Not SilentProcess Then
                                MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                SaveToLogFile(msg, False)
                            End If
                            objExport.UpdateExportFlagByDocType("ReversalInvPro")
                            objExport.UpdateExportFlagByDocType("ReversalPOR")
                            Return msg
                        Else
                            Dim msg As String = String.Format("Error while exporting file path({0})", FileName)
                            SaveToLogFile(msg, False)
                            Return msg
                        End If
                    Else
                        Dim msg As String = "Reversal GI file, no data to export."
                        SaveToLogFile(msg, False)
                        lbl.Text = String.Format(msg, FileName)
                        lbl.Update()
                        Return msg
                    End If
                Else
                    Dim msg As String = String.Format("File not found, Incorrect path({0})", FileName)
                    SaveToLogFile(msg, True)
                    lbl.Text = msg
                    lbl.Update()
                    Return msg
                End If

            Case TFileExportType.VendorReturn
                lbl.Text = "Processing Vendor Return file..."
                lbl.Update()
                If CheckFolder(IO.Path.GetDirectoryName(FileName)) Then
                    If SilentProcess Then 'Get the data again if SilentProcess is activated, means if export ALL button clicked.
                        GetVendorReturnData(True)
                    End If

                    Application.DoEvents()
                    lbl.Text = "Export Vendor Return file..."
                    lbl.Update()
                    If grdView.RowCount > 0 Then
                        If ExportDatasetToFile(ExportDataTable, FileName, pb, Append) Then
                            Dim msg As String = "Export Vendor Return file Completed, exported Items count = " & ExportDataTable.Rows.Count & ")"
                            If Not SilentProcess Then
                                MessageBox.Show(msg, "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                SaveToLogFile(msg, False)
                            End If

                            objExport.UpdateExportFlagByDocType("VendorReturn")
                            Return msg
                        Else
                            Dim msg As String = String.Format("Error while exporting file path({0})", FileName)
                            SaveToLogFile(msg, False)
                            Return msg
                        End If
                    Else
                        Dim msg As String = "Vendor Return file, no data to export."
                        SaveToLogFile(msg, False)
                        lbl.Text = String.Format(msg, FileName)
                        lbl.Update()
                        Return msg
                    End If
                Else
                    Dim msg As String = String.Format("File not found, Incorrect path({0})", FileName)
                    SaveToLogFile(msg, True)
                    lbl.Text = msg
                    lbl.Update()
                    Return msg
                End If
            Case Else
                Return String.Empty
        End Select
    End Function

    Private Sub GetGoodsReceivingData(ByVal SilentProcess As Boolean)
        Dim objSAPExport As New ZulAssetsBL.SAPExportBLL
        'Assign the data to ExportDatatable to be able to export it later after clicking on export button.
        ExportDataTable = objSAPExport.GetGoodsReceivingExport
        grd.DataSource = ExportDataTable
    End Sub

    Private Sub GetGoodsIssuanceData(ByVal SilentProcess As Boolean)
        Dim objSAPExport As New ZulAssetsBL.SAPExportBLL
        'Assign the data to ExportDatatable to be able to export it later after clicking on export button.
        ExportDataTable = objSAPExport.GetGoodsIssuingExport
        grd.DataSource = ExportDataTable
    End Sub

    Private Sub GetReversalGIData(ByVal SilentProcess As Boolean)
        Dim objSAPExport As New ZulAssetsBL.SAPExportBLL
        'Assign the data to ExportDatatable to be able to export it later after clicking on export button.
        ExportDataTable = objSAPExport.GetReversalExportGI
        grd.DataSource = ExportDataTable
    End Sub

    Private Sub GetReversalGRData(ByVal SilentProcess As Boolean)
        Dim objSAPExport As New ZulAssetsBL.SAPExportBLL
        'Assign the data to ExportDatatable to be able to export it later after clicking on export button.
        ExportDataTable = objSAPExport.GetReversalExportGR
        grd.DataSource = ExportDataTable
    End Sub


    Private Sub GetVendorReturnData(ByVal SilentProcess As Boolean)
        Dim objSAPExport As New ZulAssetsBL.SAPExportBLL
        'Assign the data to ExportDatatable to be able to export it later after clicking on export button.
        ExportDataTable = objSAPExport.GetVendorReturnExport
        grd.DataSource = ExportDataTable
    End Sub


    Private Sub GetAssetPhysicalInventoryData()

        Dim objAssetExport As New BALABBIntegration
        ExportDataTable = objAssetExport.GetPhysicalInventory_ABBExportGrid()

        ExportDataTable.Columns.Add("AssetCode", Type.GetType("System.String"))
        ExportDataTable.Columns("AssetCode").SetOrdinal(0)
        ExportDataTable.Columns.Add("SubAsset", Type.GetType("System.String"))
        ExportDataTable.Columns("SubAsset").SetOrdinal(1)

        For Each row As DataRow In ExportDataTable.Rows
            Dim CompleteCode As String = row("RefNo")
            Dim strCompleteCode As String() = CompleteCode.Split("-")

            row("AssetCode") = strCompleteCode(1)
            row("SubAsset") = strCompleteCode(2)
            If row("AstDesc").ToString = "Empty" Then
                row("AstDesc") = String.Empty
            End If

            row.AcceptChanges()
        Next
        ExportDataTable.Columns.Remove("AstID")
        ExportDataTable.Columns.Remove("CustodianID")
        ExportDataTable.Columns.Remove("AstDesc")
        ExportDataTable.Columns.Remove("AstDesc2")
        ExportDataTable.Columns.Remove("SerailNo")
        ExportDataTable.Columns.Remove("LocationFullPath")
        ExportDataTable.Columns.Remove("RefNo")
        ExportDataTable.AcceptChanges()
        grd.DataSource = ExportDataTable
    End Sub

    Private Sub GetAssetChangeData()
        Dim objAssetExport As New BALABBIntegration
        ExportDataTable = objAssetExport.GetAllData_ABBExportGrid()

        ExportDataTable.Columns.Add("Selection", Type.GetType("System.Boolean"))
        ExportDataTable.Columns("Selection").SetOrdinal(0)
        ExportDataTable.Columns.Add("AssetCode", Type.GetType("System.String"))
        ExportDataTable.Columns("AssetCode").SetOrdinal(1)
        ExportDataTable.Columns.Add("SubAsset", Type.GetType("System.String"))
        ExportDataTable.Columns("SubAsset").SetOrdinal(2)
        ExportDataTable.Columns.Add("Location", Type.GetType("System.String"))
        ExportDataTable.Columns.Add("Plant", Type.GetType("System.String"))

        For Each row As DataRow In ExportDataTable.Rows
            Dim CompleteCode As String = row("RefNo")
            Dim strCompleteCode As String() = CompleteCode.Split("-")

            row("AssetCode") = strCompleteCode(1)
            row("SubAsset") = strCompleteCode(2)
            If row("AstDesc").ToString = "Empty" Then
                row("AstDesc") = String.Empty
            End If

            Dim FulLoc As String = row("LocationFullPath")
            Dim strFulLoc As String() = FulLoc.Split("\")
            If strFulLoc(0) = "UnKnown" Then
                row("Plant") = String.Empty
            Else
                row("Plant") = strFulLoc(0).Trim
            End If

            If strFulLoc.Length > 1 Then
                If strFulLoc(1) = "UnKnown" Then
                    row("Location") = String.Empty
                Else
                    row("Location") = strFulLoc(1).Trim
                End If
            Else
                row("Location") = String.Empty
            End If
            row("Selection") = True
            row.AcceptChanges()
        Next
        ExportDataTable.Columns.Remove("AstID")
        ExportDataTable.Columns.Remove("LocationFullPath")
        ExportDataTable.Columns.Remove("RefNo")
        ExportDataTable.Columns("CustodianID").SetOrdinal(ExportDataTable.Columns.Count - 1)
        ExportDataTable.AcceptChanges()
        grd.DataSource = ExportDataTable
    End Sub



    Private Sub FormatPhysicalInventoryGrid()
        grdView.BestFitColumns()

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
    End Sub

    Private Sub FormatAssetChangeGrid()
        grdView.BestFitColumns()
        grdView.OptionsBehavior.Editable = True
        'Disable editing in all columns instead of Selection.
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In grdView.Columns
            col.OptionsColumn.AllowEdit = False
        Next

        grdView.Columns("Selection").OptionsColumn.AllowEdit = True
        grdView.Columns("CustodianID").Caption = "Employee No"

        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
    End Sub

    Private Sub FormatExportGrid()
        grdView.BestFitColumns()
        grd.UseEmbeddedNavigator = True
        grd.EmbeddedNavigator.Buttons.Append.Visible = False
        grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
    End Sub

    Private Function ExportChangedData(ByVal MyDatatable As DataTable, ByVal FileName As String, ByVal ProgBar As DevExpress.XtraEditors.ProgressBarControl, ByVal Append As Boolean) As Integer
        'Declaration of Variables
        Dim dr As DataRow
        Dim myString As String
        Dim bFirstRecord As Boolean = True
        Dim myWriter As New System.IO.StreamWriter(FileName, Append, System.Text.Encoding.GetEncoding("windows-1256"))
        myString = ""
        ProgBar.Visible = True
        ProgBar.Position = 1
        ProgBar.Properties.Step = 1
        ProgBar.Properties.Maximum = grdView.DataRowCount
        Application.DoEvents()
        Dim ExportedRecordCount As Integer = 0
        Try
            For Each dr In MyDatatable.Rows
                If dr("Selection") Then
                    bFirstRecord = True
                    For i As Integer = 1 To dr.ItemArray.Length - 1 'Start from 1 to skip exporting the selection column.
                        If Not bFirstRecord Then
                            myString += Chr(Keys.Tab)
                        End If
                        myString += dr.ItemArray(i).ToString
                        bFirstRecord = False
                    Next
                    'For Each field As Object In dr.ItemArray

                    'Next
                    'New Line to differentiate next row
                    myString += Environment.NewLine
                    ExportedRecordCount += 1
                End If
                ProgBar.PerformStep()
                ProgBar.Update()
            Next

        Catch ex As Exception
            Return -1
        Finally
            ProgBar.Visible = False
        End Try
        'Write the String to the Csv File
        myWriter.Write(myString)
        'Clean up
        myWriter.Close()
        Return ExportedRecordCount
    End Function

    Private Function ExportDatasetToFile(ByVal MyDatatable As DataTable, ByVal FileName As String, ByVal ProgBar As DevExpress.XtraEditors.ProgressBarControl, ByVal Append As Boolean) As Boolean
        'Declaration of Variables
        Dim dr As DataRow
        Dim myString As String
        Dim bFirstRecord As Boolean = True
        Dim myWriter As New System.IO.StreamWriter(FileName, Append, System.Text.Encoding.GetEncoding("windows-1256"))
        myString = ""
        ProgBar.Visible = True
        ProgBar.Position = 1
        ProgBar.Properties.Step = 1
        ProgBar.Properties.Maximum = grdView.DataRowCount
        Application.DoEvents()
        Try
            For Each dr In MyDatatable.Rows
                bFirstRecord = True
                For Each field As Object In dr.ItemArray
                    If Not bFirstRecord Then
                        myString += Chr(Keys.Tab)
                    End If
                    myString += field.ToString
                    bFirstRecord = False
                Next
                'New Line to differentiate next row
                myString += Environment.NewLine
                ProgBar.PerformStep()
                ProgBar.Update()
            Next

        Catch ex As Exception
            Return False
        Finally
            ProgBar.Visible = False
        End Try
        'Write the String to the Csv File
        myWriter.Write(myString)
        'Clean up
        myWriter.Close()
        Return True
    End Function

    Public Function GetExportFileName(ByVal ExportFolderName As String, ByVal ExportType As TFileExportType) As String
         Select ExportType
            Case TFileExportType.GoodsReceive
                Return String.Format("{0}\GR_{1}.txt", ExportFolderName, Now.Date.ToString("dd.MM.yyyy").Replace(".", ""))
            Case TFileExportType.GoodsIssuance
                Return String.Format("{0}\GI_{1}.txt", ExportFolderName, Now.Date.ToString("dd.MM.yyyy").Replace(".", ""))
            Case TFileExportType.ReversalGRDoc
                Return String.Format("{0}\RGR_{1}.txt", ExportFolderName, Now.Date.ToString("dd.MM.yyyy").Replace(".", ""))
            Case TFileExportType.ReversalGIDoc
                Return String.Format("{0}\RGI_{1}.txt", ExportFolderName, Now.Date.ToString("dd.MM.yyyy").Replace(".", ""))
            Case TFileExportType.VendorReturn
                Return String.Format("{0}\VR_{1}.txt", ExportFolderName, Now.Date.ToString("dd.MM.yyyy").Replace(".", ""))
            Case Else
                Return String.Empty
        End Select
    End Function

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If grdView.RowCount > 0 Then
            Select Case ExportType
                Case TFileExportType.GoodsReceive, TFileExportType.GoodsIssuance, TFileExportType.ReversalGRDoc, TFileExportType.ReversalGIDoc, TFileExportType.VendorReturn
                    ExportFileName = GetExportFileName(ExportFolderName, ExportType)
                    ExportDataFile(lblMessages, pb, ExportFileName, ExportType, False, True)
                    'Case TFileExportType.GoodsIssuance
                    '    ExportFileName = GetExportFileName(ExportFolderName, ExportType)
                    '    ExportDataFile(lblMessages, pb, ExportFileName, TFileExportType.GoodsIssuance, False, True)
                Case Else
                    ExportDataFile(lblMessages, pb, ExportFileName, ExportType, False, False)
            End Select
        Else
            MessageBox.Show("No data to export.", "ZulAssets", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Me.Close()
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        For i As Integer = 0 To grdView.RowCount - 1
            grdView.SetRowCellValue(i, "Selection", True)
        Next
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
