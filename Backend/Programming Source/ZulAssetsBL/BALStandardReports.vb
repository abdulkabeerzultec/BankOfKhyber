Imports ZulAssetsDAL.ZulAssetsDAL
Namespace ZulAssetBAL
    Public Class BALStandardReports
        Inherits BLBase
#Region "Data Members"
        Private objStandardReports As New StandardReports
#End Region
        Public Function Get_Report_AssestsByCategroy() As DataTable
            Try
                Return Me.GeneralExecuter(objStandardReports.Get_Report_AssestsByCategroy())

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Get_Report_AssetbySubCategory() As DataTable
            Try
                Return Me.GeneralExecuter(objStandardReports.Get_Report_AssetbySubCategory())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

      

        Public Function Item_Inventory(ByVal forTemp1 As String, ByVal strCompanyID As String, ByVal strBookId As String, ByVal strCustID As String, ByVal strLocId As String, ByVal strCatID As String, ByVal IncludeChild As Boolean, ByVal strBrand As String, ByVal strSupplier As String, ByVal strDepartment As String, ByVal Itemcode As String, ByVal CostFilterCriteria As String, ByVal CostFilterValue As Double, ByVal StatusID As Integer, ByVal InvStatus As Integer) As DataTable
            Try
                Return Me.GeneralExecuter(objStandardReports.Item_Inventory(forTemp1, strCompanyID, strBookId, strCustID, strLocId, strCatID, IncludeChild, strBrand, strSupplier, strDepartment, Itemcode, CostFilterCriteria, CostFilterValue, StatusID, InvStatus))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub Insert_Report_AssetsBySubCategory(ByVal Startdate As DateTime, ByVal EndDate As DateTime)
            Dim objBALCategory As New BALCategory
            Dim Invest_OB, Invest_Add, Invest_Ded, Invest_CB, Depr_OB, Depr_Add, Depr_Ded, Depr_CB, NetValue As Double
            Dim AstCatID2() As String
            Dim dtCategory As DataTable
            dtCategory = objBALCategory.GetAll_Category(New attCategory)
            If dtCategory Is Nothing = False Then
                If dtCategory.Rows.Count > 0 Then
                    For Each dr As DataRow In dtCategory.Rows
                        If Not IsDBNull(dtCategory.Rows(0)("AstCatID")) Then
                            Dim AstCaId As String = dr("AstCatID")
                            Dim AstCatDesc As String = objBALCategory.Comp_Path(dr("AstCatID"))
                            AstCatID2 = AstCatDesc.Split("\")
                            If AstCaId.IndexOf("-") >= 0 And AstCatID2.Length = 3 Then
                                Invest_OB = Get_Invest_Opening_balance(dr("AstCatID"), Startdate)
                                Invest_Add = Get_Invest_Addition(dr("AstCatID"), Startdate, EndDate)
                                Invest_Ded = Get_Invest_Deduction(dr("AstCatID"), Startdate, EndDate)
                                Invest_CB = Get_Invest_Closing_balance(dr("AstCatID"), EndDate)
                                Depr_OB = Get_Depreciation_Opening_balance(dr("AstCatID"), Startdate)
                                Depr_Add = Get_Depreciation_Addition(dr("AstCatID"), Startdate, EndDate)
                                Depr_Ded = Get_Depreciation_Deduction(dr("AstCatID"), Startdate, EndDate)
                                Depr_CB = Depr_OB + Depr_Add - Depr_Ded
                                NetValue = Invest_CB - Depr_CB
                                Me.TransactionExecuter(objStandardReports.Insert_Report_AssetsBySubCategory(AstCatID2(0), AstCatID2(1), AstCatID2(2), Invest_OB, Invest_Add, Invest_Ded, Invest_CB, Depr_OB, Depr_Add, Depr_Ded, Depr_CB, NetValue))
                            End If
                        End If
                    Next

                End If
            End If
        End Sub

        Public Sub Insert_Report_AssetsByCategory(ByVal Startdate As DateTime, ByVal EndDate As DateTime)
            Dim objBALCategory As New BALCategory
            Dim Invest_OB, Invest_Add, Invest_Ded, Invest_CB, Depr_OB, Depr_Add, Depr_Ded, Depr_CB, NetValue As Double
            Dim AstCatID2() As String
            Dim dtCategory As DataTable
            dtCategory = objBALCategory.GetAll_Category(New attCategory)
            If dtCategory Is Nothing = False Then
                If dtCategory.Rows.Count > 0 Then
                    For Each dr As DataRow In dtCategory.Rows
                        If Not IsDBNull(dtCategory.Rows(0)("AstCatID")) Then
                            Dim AstCaId As String = dr("AstCatID")
                            AstCatID2 = AstCaId.Split("-")
                            If AstCaId.IndexOf("-") >= 0 And AstCatID2.Length = 2 Then

                                'Depreciation Closing Balance Calculation for Pepsi

                                Invest_OB = Get_Invest_Opening_balance(dr("AstCatID"), Startdate)
                                Invest_Add = Get_Invest_Addition(dr("AstCatID"), Startdate, EndDate)
                                Invest_Ded = Get_Invest_Deduction(dr("AstCatID"), Startdate, EndDate)
                                Invest_CB = Get_Invest_Closing_balance(dr("AstCatID"), EndDate)
                                Depr_OB = Get_Depreciation_Opening_balance(dr("AstCatID"), Startdate)
                                Depr_Add = Get_Depreciation_Addition(dr("AstCatID"), Startdate, EndDate)
                                Depr_Ded = Get_Depreciation_Deduction(dr("AstCatID"), Startdate, EndDate)
                                Depr_CB = Depr_OB + Depr_Add - Depr_Ded
                                NetValue = Invest_CB - Depr_CB
                                Me.TransactionExecuter(objStandardReports.Insert_Report_AssetsByCategory(dr("AstCatDesc"), Invest_OB, Invest_Add, Invest_Ded, Invest_CB, Depr_OB, Depr_Add, Depr_Ded, Depr_CB, NetValue))
                            End If
                        End If
                    Next

                End If
            End If
        End Sub

        Public Function Get_Invest_Opening_balance(ByVal AstCatID As String, ByVal StartDate As DateTime) As Double
            Try
                Dim strKey As String = ""
                Dim intKey As Double = 0
                Try
                    strKey = Me.GeneralExecuter_Scalar(objStandardReports.Get_Invest_Opening_balance(AstCatID, StartDate), "")
                    intKey = CDbl(strKey)
                Catch ex As Exception
                    Throw ex
                End Try
                Return intKey

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Get_Invest_Addition(ByVal AstCatID As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime) As Double
            Try
                Dim strKey As String = ""
                Dim intKey As Double = 0
                Try
                    strKey = Me.GeneralExecuter_Scalar(objStandardReports.Get_Invest_Addition(AstCatID, StartDate, EndDate), "")
                    intKey = CDbl(strKey)
                Catch ex As Exception
                    Throw ex
                End Try
                Return intKey

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Get_Invest_Closing_balance(ByVal AstCatID As String, ByVal EndDate As DateTime) As Double
            Try
                Dim strKey As String = ""
                Dim intKey As Double = 0
                Try
                    strKey = Me.GeneralExecuter_Scalar(objStandardReports.Get_Invest_Closing_balance(AstCatID, EndDate), "")
                    intKey = CDbl(strKey)
                Catch ex As Exception
                    Throw ex
                End Try
                Return intKey

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Get_Invest_Deduction(ByVal AstCatID As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime) As Double
            Try
                Dim strKey As String = ""
                Dim intKey As Double = 0
                Try
                    strKey = Me.GeneralExecuter_Scalar(objStandardReports.Get_Invest_Deduction(AstCatID, StartDate, EndDate), "")
                    intKey = CDbl(strKey)
                Catch ex As Exception
                    Throw ex
                End Try
                Return intKey

            Catch ex As Exception
                Throw ex
            End Try
        End Function



        Public Function Get_Depreciation_Opening_balance(ByVal AstCatID As String, ByVal StartDate As DateTime) As Double
            Try
                Dim strKey As String = ""
                Dim intKey As Double = 0
                Try
                    strKey = Me.GeneralExecuter_Scalar(objStandardReports.Get_Depreciation_Opening_balance(AstCatID, StartDate), "")
                    intKey = CDbl(strKey)
                Catch ex As Exception
                    Throw ex
                End Try
                Return intKey

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Get_Depreciation_Addition(ByVal AstCatID As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime) As Double
            Try
                Dim strKey As String = ""
                Dim intKey As Double = 0
                Try
                    strKey = Me.GeneralExecuter_Scalar(objStandardReports.Get_Depreciation_Addition(AstCatID, StartDate, EndDate), "")
                    intKey = CDbl(strKey)
                Catch ex As Exception
                    Throw ex
                End Try
                Return intKey

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Get_Depreciation_Closing_balance(ByVal AstCatID As String, ByVal EndDate As DateTime) As Double
            Try
                Dim strKey As String = ""
                Dim intKey As Double = 0
                Try
                    strKey = Me.GeneralExecuter_Scalar(objStandardReports.Get_Depreciation_Closing_balance(AstCatID, EndDate), "")
                    intKey = CDbl(strKey)
                    If intKey = 0 Then
                        intKey = intKey + 1
                        'strKey = intKey
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
                Return intKey

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Get_Depreciation_Deduction(ByVal AstCatID As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime) As Double
            Try
                Dim strKey As String = ""
                Dim intKey As Double = 0
                Try
                    strKey = Me.GeneralExecuter_Scalar(objStandardReports.Get_Depreciation_Deduction(AstCatID, StartDate, EndDate), "")
                    intKey = CDbl(strKey)
                Catch ex As Exception
                    Throw ex
                End Try
                Return intKey

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub Empty_Report_AssetsBySubCategory()
            Try
                '   objAssetDetails.Attribute = objattAssetDetails
                Me.TransactionExecuter(objStandardReports.Empty_Report_AssetsBySubCategory())

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Empty_Report_AssetsByCategory()
            Try
                '   objAssetDetails.Attribute = objattAssetDetails
                Me.TransactionExecuter(objStandardReports.Empty_Report_AssetsByCategory())

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Empty_CompanyAssets()
            Try
                '   objAssetDetails.Attribute = objattAssetDetails
                Me.TransactionExecuter(objStandardReports.Empty_CompanyAssets())

            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub Insert_CompanyAssets_Report(ByVal dtData As DataTable)
            Dim CatID1, CatID2, CatID3, LocId1, LocId2, LocId3 As String

            Dim AstNum, AstID, PurDate, OracleRef, CompRef, CustodianID, CustodianName, Cost As String
            Dim objBALOrgHier As New BALOrgHier
            Dim CatID(3) As String
            Dim LocId(3) As String
            Dim LocId4() As String
            Dim objBALCategory As New BALCategory
            Dim objBALocation As New BALLocation
            For Each dr As DataRow In dtData.Rows
                CatID1 = ""
                CatID2 = ""
                CatID3 = ""
                LocId1 = ""
                LocId2 = ""
                LocId3 = ""
                AstNum = ""
                AstID = ""
                PurDate = ""
                OracleRef = ""
                CompRef = ""
                CustodianID = ""
                CustodianName = ""
                Cost = ""
                CatID = objBALCategory.Comp_Path(dr("AstCatID")).Split("\")
                If CatID.Length > 1 Then
                    CatID1 = CatID(1)
                    If CatID.Length > 2 Then
                        CatID2 = CatID(2)
                        If CatID.Length > 3 Then
                            CatID3 = CatID(3)
                        Else
                            CatID3 = ""
                        End If
                    Else
                        CatID2 = ""
                    End If
                Else
                    CatID1 = ""
                End If
                LocId4 = objBALocation.Comp_Path(dr("LocId")).Split("\")
                If LocId4.Length > 0 Then
                    LocId3 = LocId4(LocId4.Length - 1)
                Else
                    LocId3 = ""
                End If
                LocId = objBALOrgHier.HierName(dr("DeptId")).Split("-")
                If LocId.Length > 1 Then
                    LocId1 = LocId(1)
                    If LocId.Length > 2 Then
                        LocId2 = LocId(2)

                    Else
                        LocId2 = ""
                    End If
                Else
                    LocId1 = ""
                End If

                If Not IsDBNull(dr("AstNum")) Then
                    AstNum = dr("AstNum")
                End If
                If Not IsDBNull(dr("AstID")) Then
                    AstID = dr("AstID")
                End If
                If Not IsDBNull(dr("PurDate")) Then
                    PurDate = dr("PurDate")
                End If
                If Not IsDBNull(dr("REfNo")) Then
                    OracleRef = dr("REfNo")
                End If
                If Not IsDBNull(dr("RefCode")) Then
                    CompRef = dr("RefCode")
                End If

                If Not IsDBNull(dr("CustodianID")) Then
                    CustodianID = dr("CustodianID")
                End If
                If Not IsDBNull(dr("CustodianName")) Then
                    CustodianName = dr("CustodianName")
                End If
                If Not IsDBNull(dr("Total")) Then
                    Cost = dr("Total")
                End If
                Try
                    TransactionExecuter(objStandardReports.Insert_CompanyAssets_Report(AstNum, AstID, PurDate, OracleRef, CompRef, CatID1, CatID2, CatID3, LocId1, LocId2, LocId3, CustodianID, CustodianName, Cost))
                Catch ex As Exception

                End Try
            Next
        End Sub

        Public Function Get_CompAsset() As DataTable
            Try
                '     objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objStandardReports.Get_CompAsset())

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CompanyAssets_Report_From(ByVal objattAssetDetails As attAssetDetails, ByVal objattAssets As attItems, ByVal DeptID As String, ByVal InlcudeChild As Boolean, ByVal FromDate As Date, ByVal ToDate As Date, ByVal DateFilterEnabled As Boolean, ByVal CostFilterCriteria As String, ByVal CostFilterValue As Double) As DataTable
            Try
                objStandardReports.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objStandardReports.CompanyAssets_Report_New(objattAssets, DeptID, InlcudeChild, FromDate, ToDate, DateFilterEnabled, CostFilterCriteria, CostFilterValue))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CompanyAssetReport(ByVal objattAssetDetails As attAssetDetails, ByVal objattAssets As attItems, ByVal DeptID As String, ByVal InlcudeChilds As Boolean, ByVal FromDate As Date, ByVal ToDate As Date, ByVal DateFilterEnabled As Boolean, ByVal CostFilterCriteria As String, ByVal CostFilterValue As Double) As DataTable
            Try
                objStandardReports.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objStandardReports.CompanyAssetReport(objattAssets, DeptID, InlcudeChilds, FromDate, ToDate, DateFilterEnabled, CostFilterCriteria, CostFilterValue))
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function AssetsLogReport(ByVal objattAssetDetails As attAssetDetails, ByVal objattAssets As attItems, ByVal DeptID As String, ByVal InlcudeChilds As Boolean, ByVal FromDate As Date, ByVal ToDate As Date, ByVal DateFilterEnabled As Boolean, ByVal CostFilterCriteria As String, ByVal CostFilterValue As Double) As DataTable
            Try
                objStandardReports.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objStandardReports.AssetsLogReport(objattAssets, DeptID, InlcudeChilds, FromDate, ToDate, DateFilterEnabled, CostFilterCriteria, CostFilterValue))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DisposedAssetReport(ByVal objattAssetDetails As attAssetDetails, ByVal objattAssets As attItems, ByVal DeptID As String, ByVal InlcudeChilds As Boolean, ByVal FromDate As Date, ByVal ToDate As Date, ByVal DispCode As String, ByVal DateFilterEnabled As Boolean, ByVal CostFilterCriteria As String, ByVal CostFilterValue As Double) As DataTable
            Try
                objStandardReports.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objStandardReports.DisposedAssetReport(objattAssets, DeptID, InlcudeChilds, FromDate, ToDate, DispCode, DateFilterEnabled, CostFilterCriteria, CostFilterValue))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ExpectedDepreciation_Book(ByVal strCompanyID As String, ByVal strBookId As String, ByVal strCustID As String, ByVal strLocId As String, ByVal strCatID As String, ByVal IncludeChild As Boolean, ByVal HierCode As String, ByVal ShowDisposed As Byte, ByVal ExcludeDisposed As String, ByVal BrandID As String, ByVal SupplierID As String, ByVal Itemcode As String, ByVal CostFilterCriteria As String, ByVal CostFilterValue As Double, ByVal StatusID As Integer, ByVal InvStatus As Integer, ByVal FromDate As Date, ByVal ToDate As Date, ByVal DateFilterEnabled As Boolean) As DataTable
            Try
                Return Me.GeneralExecuter(objStandardReports.ExpectedDepreciation_Book(strCompanyID, strBookId, strCustID, strLocId, strCatID, IncludeChild, HierCode, ShowDisposed, ExcludeDisposed, BrandID, SupplierID, Itemcode, CostFilterCriteria, CostFilterValue, StatusID, InvStatus, FromDate, ToDate, DateFilterEnabled))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Depreciation_Book(ByVal forTemp1 As String, ByVal strCompanyID As String, ByVal strBookId As String, ByVal strCustID As String, ByVal strLocId As String, ByVal strCatID As String, ByVal IncludeChild As Boolean, ByVal HierCode As String, ByVal ShowDisposed As Byte, ByVal ExcludeDisposed As String, ByVal BrandID As String, ByVal SupplierID As String, ByVal Itemcode As String, ByVal CostFilterCriteria As String, ByVal CostFilterValue As Double, ByVal StatusID As Integer, ByVal InvStatus As Integer, ByVal FromDate As Date, ByVal ToDate As Date, ByVal DateFilterEnabled As Boolean) As DataTable
            Try
                Return Me.GeneralExecuter(objStandardReports.Depreciation_Book(forTemp1, strCompanyID, strBookId, strCustID, strLocId, strCatID, IncludeChild, HierCode, ShowDisposed, ExcludeDisposed, BrandID, SupplierID, Itemcode, CostFilterCriteria, CostFilterValue, StatusID, InvStatus, FromDate, ToDate, DateFilterEnabled))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_AuditStatusReport(ByVal objattAst_INV_Schedule As attInvSchedule, ByVal Status As String, ByVal LocationID As String, ByVal CatID As String, ByVal IncludeChild As Boolean) As DataTable
            Try
                Return Me.GeneralExecuter(objStandardReports.GetAll_AuditStatusReport(objattAst_INV_Schedule, Status, LocationID, CatID, IncludeChild))
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetCostCenter_AuditReport(ByVal objattAst_INV_Schedule As attInvSchedule, ByVal Status As String, ByVal LocationID As String, ByVal CatID As String, ByVal IncludeChild As Boolean) As DataTable
            Try
                Return Me.GeneralExecuter(objStandardReports.GetCostCenter_AuditReport(objattAst_INV_Schedule, Status, LocationID, CatID, IncludeChild))
            Catch ex As Exception
                Throw ex
            End Try

        End Function
    End Class
End Namespace