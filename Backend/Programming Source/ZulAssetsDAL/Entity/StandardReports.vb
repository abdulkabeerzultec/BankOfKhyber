Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class StandardReports
#Region "Data Members"

        Private objattAssetDetails As attAssetDetails
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattAssetDetails = New attAssetDetails
        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute
            Get
                Return objattAssetDetails
            End Get
            Set(ByVal Value As IAttribute)
                objattAssetDetails = CType(Value, attAssetDetails)
            End Set
        End Property

        Public Property ObjCommand() As IDbCommand
            Get
                Return _Command
            End Get
            Set(ByVal Value As IDbCommand)
                _Command = CType(Value, IDbCommand)
            End Set
        End Property
#End Region

        Public Function Get_Report_AssestsByCategroy() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select *  from Report_AssestsByCategroy")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function Get_Report_AssetbySubCategory() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select *  from Report_AssetbySubCategory")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function ExpectedDepreciation_Book(ByVal strCompanyID As String, ByVal strBookId As String, ByVal strCustID As String, ByVal strLocId As String, ByVal strCatID As String, ByVal IncludeChild As Boolean, ByVal HierCode As String, ByVal ShowDisposed As Byte, ByVal ExcludeDisposed As String, ByVal BrandID As String, ByVal SupplierID As String, ByVal ItemCode As String, ByVal CostFilterCriteria As String, ByVal CostFilterValue As Double, ByVal StatusID As Integer, ByVal InvStatus As Integer, ByVal FromDate As Date, ByVal ToDate As Date, ByVal DateFilterEnabled As Boolean) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder


            strQuery.Append("  SELECT AssetDetails.AstID,AstNum,RefNo,Location.Compcode,Location.LocationFullPath,Category.CatFullPath,Category.AstCatDesc,Custodian.CustodianName, ")
            strQuery.Append("  AssetDetails.AstDesc,AssetDetails.AstDesc2,BaseCost+Tax  as tot,salvageYear,SalvageMonth,AssetDetails.SrvDate,AssetDetails.PurDate,AstBooks.currentbv as CurBook,AstBooks.DepCode,AstBooks.SalvageValue ,AstBooks.BVUpdate, Custodian.CustodianCode, Assets.AstDesc AS ItemDesc, Assets.ItemCode,100/IsNULL(nullif(((salvageyear * 12 + SalvageMonth)/12),0),1) as DepPercentage ,brand.AstBrandName,CompanyCode,CompanyName,'' as Loc1,'' as Loc2,'' as Loc3,'' as Loc4,'' as Loc5,'' as Cat1,'' as Cat2,'' as Cat3 FROM AssetDetails ")
            strQuery.Append("  inner join (Assets left outer join Category on Category.AstCatID = Assets.AstCatID)on Assets.ItemCode = AssetDetails.ItemCode  ")
            strQuery.Append("  inner join Custodian on Custodian.CustodianID = AssetDetails.CustodianID ")
            strQuery.Append("  inner join AstBooks on AssetDetails.AstID= AstBooks.AstID  ")
            strQuery.Append("  inner join Location on Location.LocId = AssetDetails.LocId  ")
            strQuery.Append("  inner join brand on brand.AstBrandID = AssetDetails.AstBrandID  ")
            strQuery.Append("  inner join Companies on Companies.CompanyId = AssetDetails.CompanyId  ")
            strQuery.Append(" where AssetDetails.IsDeleted = 0 ")

            If ShowDisposed = 1 Then
                strQuery.Append(" and AssetDetails.Disposed = " & ShowDisposed & "")
            ElseIf ExcludeDisposed = "MUST" Then
                strQuery.Append(" and AssetDetails.Disposed = 0")
            End If

            If DateFilterEnabled Then
                strQuery.Append(" and Purdate >= " & BackEndDate(FromDate))
                strQuery.Append(" and Purdate <= " & BackEndDate(ToDate))
            End If


            If strCompanyID <> "" Then
                strQuery.Append(" and AssetDetails.CompanyID = " & strCompanyID & "")
            End If
            If strBookId <> "" Then
                strQuery.Append(" and Astbooks.BookID =" & strBookId & "")
            End If
            If strCustID <> "" Then
                strQuery.Append(" and AssetDetails.CustodianID ='" & strCustID & "'")
            End If
            If strLocId <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (AssetDetails.LocId like '" & Trim(strLocId) & "' or AssetDetails.LocId like '" & Trim(strLocId) & "-%' )")
                Else
                    strQuery.Append(" and AssetDetails.LocId like '" & Trim(strLocId) & "'")
                End If
            End If
            If strCatID <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (Assets.AstCatID like '" & strCatID & "' or Assets.AstCatID like '" & strCatID & "-%')")
                Else
                    strQuery.Append(" and Assets.AstCatID  like '" & strCatID & "'")

                End If
            End If
            If HierCode <> "" Then
                strQuery.Append(" and Custodian.DeptID = '" & HierCode & "'")
            End If

            If ItemCode <> 0 Then
                strQuery.Append(" and AssetDetails.ItemCode = " & ItemCode)
            End If
            If CostFilterCriteria <> "0" Then
                strQuery.Append(" and AssetDetails.BaseCost + AssetDetails.Tax  " & CostFilterCriteria & CostFilterValue.ToString)
            End If
            If objattAssetDetails.InvStatus > -1 Then
                strQuery.Append(" and AssetDetails.InvStatus = " & objattAssetDetails.InvStatus)
            End If

            If objattAssetDetails.StatusID > 0 Then
                strQuery.Append(" and AssetDetails.StatusID = " & objattAssetDetails.StatusID)
            End If

            If BrandID <> "" Then
                strQuery.Append(" and AssetDetails.AstBrandId = '" & BrandID & "'")
            End If
            If SupplierID <> "" Then
                strQuery.Append(" and AssetDetails.suppid = '" & SupplierID & "'")
            End If

            strQuery.Append("  order by AssetDetails.AstNum")



            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function



        Public Function Item_Inventory(ByVal forTemp1 As String, ByVal strCompanyID As String, ByVal strBookId As String, ByVal strCustID As String, ByVal strLocId As String, ByVal strCatID As String, ByVal IncludeChild As Boolean, ByVal strBrand As String, ByVal strSupplier As String, ByVal strDepartment As String, ByVal ItemCode As String, ByVal CostFilterCriteria As String, ByVal CostFilterValue As Double, ByVal StatusID As Integer, ByVal InvStatus As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            strQuery.Append("select Assets.Itemcode,Assets.AstDesc, ")
            strQuery.Append(" (select count(*) from AssetDetails inner join  Custodian on Custodian.CustodianID= AssetDetails.CustodianID where AssetDetails.IsDeleted = 0")
            strQuery.Append(" and AssetDetails.Disposed = 0 and assets.itemcode = AssetDetails.ItemCode")

            If strCompanyID <> "" Then
                strQuery.Append(" and AssetDetails.CompanyID = " & strCompanyID & "")
            End If

            If strBookId <> "" Then
                strQuery.Append(" and AssetDetails.BookID =" & strBookId & "")
            End If

            If strBrand <> "" Then
                strQuery.Append(" and AssetDetails.AstBrandId ='" & strBrand & "'")
            End If
            If strSupplier <> "" Then
                strQuery.Append(" and AssetDetails.SuppID ='" & strSupplier & "'")
            End If
            If strDepartment <> "" Then
                strQuery.Append(" and Custodian.DeptId  ='" & strDepartment & "'")
            End If
            If strCustID <> "" Then
                strQuery.Append(" and AssetDetails.CustodianID ='" & strCustID & "'")
            End If
            If CostFilterCriteria <> "0" Then

                strQuery.Append(" and AssetDetails.BaseCost + AssetDetails.Tax  " & CostFilterCriteria & CostFilterValue.ToString)
            End If
            If InvStatus > -1 Then
                strQuery.Append(" and AssetDetails.InvStatus = " & InvStatus)
            End If

            If StatusID > 0 Then
                strQuery.Append(" and AssetDetails.StatusID = " & StatusID)
            End If

            If strLocId <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (AssetDetails.LocId like '" & Trim(strLocId) & "' or AssetDetails.LocId like '" & Trim(strLocId) & "-%' )")
                Else
                    strQuery.Append(" and AssetDetails.LocId like '" & Trim(strLocId) & "'")
                End If
            End If
            If strCatID <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (Assets.AstCatID like '" & strCatID & "' or Assets.AstCatID like '" & strCatID & "-%')")
                Else
                    strQuery.Append(" and Assets.AstCatID  like '" & strCatID & "'")
                End If
            End If

            strQuery.Append(") as total from Assets ")
            If ItemCode <> 0 Then
                strQuery.Append(" where  Assets.ItemCode = '" & ItemCode & "' ")
            End If

            strQuery.Append(" group by Assets.ItemCode,Assets.AstDesc")
            If strCatID <> "" Then
                strQuery.Append(" ,Assets.AstCatID ")
            End If


            If AppConfig.DbType = "1" Then
                strQuery.Append(" order by cast(Assets.Itemcode as bigint)")
            Else
                strQuery.Append(" order by Cast (Assets.ItemCode as Number)")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Get_Invest_Opening_balance(ByVal AstCatID As String, ByVal StartDate As DateTime) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select Sum(BaseCost + Tax)     from Assets,Assetdetails,Category,location")
            strQuery.Append(" where(Assets.ItemCode = Assetdetails.ItemCode)")
            strQuery.Append(" and Assets.AstCatID = Category.AstCatID")
            strQuery.Append(" and Location.LocID = Assetdetails.LocID")
            strQuery.Append(" and PurDate <=" & BackEndDate(StartDate))
            strQuery.Append(" and  (Assets.AstCatID like '" & AstCatID & "' or Assets.AstCatID like '" & AstCatID & "-%')")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Get_Invest_Addition(ByVal AstCatID As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("  select Sum(BaseCost + Tax)   from Assets,Assetdetails,Category")
            strQuery.Append(" where Assets.ItemCode = Assetdetails.ItemCode")
            strQuery.Append(" and Assets.AstCatID = Category.AstCatID")
            strQuery.Append(" and PurDate <= " & BackEndDate(EndDate) & " and PurDate > " & BackEndDate(StartDate) & "")
            strQuery.Append(" and  (Assets.AstCatID like '" & AstCatID & "' or Assets.AstCatID like '" & AstCatID & "-%')")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Get_Invest_Closing_balance(ByVal AstCatID As String, ByVal EndDate As DateTime) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select Sum(BaseCost + Tax)    from Assets,Assetdetails,Category,location")
            strQuery.Append("   where(Assets.ItemCode = Assetdetails.ItemCode) ")
            strQuery.Append(" and Assets.AstCatID = Category.AstCatID")
            strQuery.Append(" and Location.LocID = Assetdetails.LocID")
            strQuery.Append(" and PurDate <=" & BackEndDate(EndDate))
            strQuery.Append(" and  (Assets.AstCatID like '" & AstCatID & "' or Assets.AstCatID like '" & AstCatID & "-%')")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Get_Invest_Deduction(ByVal AstCatID As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select Sum(Sel_Price)  from Assets,Assetdetails,Category")
            strQuery.Append(" where(Assets.ItemCode = Assetdetails.ItemCode)")
            strQuery.Append(" and Assets.AstCatID = Category.AstCatID and IsSold =1")
            strQuery.Append(" and Sel_Date <= " & BackEndDate(StartDate) & "  and Sel_Date >" & BackEndDate(EndDate) & "  ")
            strQuery.Append(" and  (Assets.AstCatID like '" & AstCatID & "' or Assets.AstCatID like '" & AstCatID & "-%')")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function Get_Depreciation_Opening_balance(ByVal AstCatID As String, ByVal StartDate As DateTime) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select Sum(BookHistory.AccDepValue) from BookHistory,AssetDetails,Assets ")
            strQuery.Append(" where (Assetdetails.AstID = BookHistory.AssetID)")
            strQuery.Append(" and AssetDetails.ItemCode = Assets.ItemCode 	")
            strQuery.Append(" and    DepDate <=" & BackEndDate(StartDate))
            strQuery.Append(" and  (Assets.AstCatID like '" & AstCatID & "' or Assets.AstCatID like '" & AstCatID & "-%')")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Get_Depreciation_Addition(ByVal AstCatID As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select Sum(BookHistory.AccDepValue)  from BookHistory,AssetDetails,Assets")
            strQuery.Append(" where(Assetdetails.AstID = BookHistory.AssetID)")
            strQuery.Append(" and AssetDetails.ItemCode = Assets.ItemCode 	")
            strQuery.Append(" and DepDate <= " & BackEndDate(EndDate) & " and DepDate>" & BackEndDate(StartDate))
            strQuery.Append(" and  (Assets.AstCatID like '" & AstCatID & "' or Assets.AstCatID like '" & AstCatID & "-%')")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Get_Depreciation_Closing_balance(ByVal AstCatID As String, ByVal EndDate As DateTime) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select Sum(BookHistory.AccDepValue) as 'Depreciation opening Balance' from BookHistory,AssetDetails,Assets ")
            strQuery.Append(" where(Assetdetails.AstID = BookHistory.AssetID)")
            strQuery.Append(" and AssetDetails.ItemCode = Assets.ItemCode 	")
            strQuery.Append(" and    DepDate <=" & BackEndDate(EndDate))
            strQuery.Append(" and  (Assets.AstCatID like '" & AstCatID & "' or Assets.AstCatID like '" & AstCatID & "-%')")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Get_Depreciation_Deduction(ByVal AstCatID As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select Sum(Sel_Price)  from Assets,Assetdetails,Category")
            strQuery.Append(" where(Assets.ItemCode = Assetdetails.ItemCode)")
            strQuery.Append(" and Assets.AstCatID = Category.AstCatID and Disposed =1")
            strQuery.Append(" and DispDate <= " & BackEndDate(StartDate) & "  and DispDate >" & BackEndDate(EndDate) & "  ")
            strQuery.Append(" and  (Assets.AstCatID like '" & AstCatID & "' or Assets.AstCatID like '" & AstCatID & "-%')")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Insert_Report_AssetsBySubCategory(ByVal strMainCat As String, ByVal strSubCat As String, ByVal strCat As String, ByVal strInvest_OB As Double, ByVal strInvest_Add As Double, ByVal strInvest_Ded As Double, ByVal strInvest_CB As Double, ByVal strDepr_OB As Double, ByVal strDepr_Add As Double, ByVal strDepr_Ded As Double, ByVal strDepr_CB As Double, ByVal strNetValue As Double) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("  insert into Report_AssetbySubCategory ")
            strQuery.Append(" (MainCat,SubCat,Cat,Invest_OB,Invest_Add,Invest_Ded,Invest_CB,Depr_OB,Depr_Add,Depr_Ded,Depr_CB,NetValue)")
            strQuery.Append(" values")
            strQuery.Append(" ('" & strMainCat & "','" & strSubCat & "','" & strCat & "'," & strInvest_OB & "," & strInvest_Add & "," & strInvest_Ded & "," & strInvest_CB & "," & strDepr_OB & "," & strDepr_Add & "," & strDepr_Ded & "," & strDepr_CB & "," & strNetValue & ")")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Insert_Report_AssetsByCategory(ByVal strMainCat As String, ByVal strInvest_OB As Double, ByVal strInvest_Add As Double, ByVal strInvest_Ded As Double, ByVal strInvest_CB As Double, ByVal strDepr_OB As Double, ByVal strDepr_Add As Double, ByVal strDepr_Ded As Double, ByVal strDepr_CB As Double, ByVal strNetValue As Double) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" insert into Report_AssestsByCategroy (MainCat,Invest_OB,Invest_Add,Invest_Ded,Invest_CB,Depr_OB,Depr_Add,Depr_Ded,Depr_CB,NetValue) values ")
            'strQuery.Append(" insert into Report_AssetCategory (MainCat,Invest_OB,Invest_Add,Invest_Ded,Invest_CB,Depr_OB,Depr_Add,Depr_Ded,Depr_CB,NetValue) values ")
            strQuery.Append(" ('" & strMainCat & "'," & strInvest_OB & "," & strInvest_Add & "," & strInvest_Ded & "," & strInvest_CB & "," & strDepr_OB & "," & strDepr_Add & "," & strDepr_Ded & "," & strDepr_CB & "," & strNetValue & ")")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Empty_Report_AssetsBySubCategory() As IDbCommand
            Try
                Dim objCommand As OleDbCommand = New OleDbCommand
                Dim strQuery As New StringBuilder
                strQuery.Append(" Delete from Report_AssetbySubCategory")


                objCommand.CommandText = strQuery.ToString()
                _Command = objCommand
                Return objCommand

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Empty_Report_AssetsByCategory() As IDbCommand
            Try
                Dim objCommand As OleDbCommand = New OleDbCommand
                Dim strQuery As New StringBuilder
                strQuery.Append(" Delete   from Report_AssestsByCategroy")
                'strQuery.Append(" Delete   from Report_AssetCategory")

                objCommand.CommandText = strQuery.ToString()
                _Command = objCommand
                Return objCommand

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Empty_CompanyAssets() As IDbCommand
            Try
                Dim objCommand As OleDbCommand = New OleDbCommand
                Dim strQuery As New StringBuilder
                strQuery.Append(" Delete from Report_CompanyAssets")


                objCommand.CommandText = strQuery.ToString()
                _Command = objCommand
                Return objCommand

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Insert_CompanyAssets_Report(ByVal AstNum As String, ByVal AstID As String, ByVal PurDate As String, ByVal OracleRef As String, ByVal CompRef As String, ByVal AstCat1 As String, ByVal AstCat2 As String, ByVal AstCat3 As String, ByVal CC1 As String, ByVal CC2 As String, ByVal CC3 As String, ByVal CustodianID As String, ByVal CustodianName As String, ByVal Cost As String) As IDbCommand

            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into Report_CompanyAssets")
            strQuery.Append("(AstNum,AstID,PurDate,OracleRef,CompRef,AstCat1,AstCat2,AstCat3,CC1,CC2,CC3,CustodianID,CustodianName,Cost )")
            strQuery.Append(" Values")
            strQuery.Append("('" & AstNum & "', '" & AstID & "'," & BackEndDate(PurDate) & ",'" & OracleRef & "','" & CompRef & "','" & AstCat1 & "','" & AstCat2 & "','" & AstCat3 & "','" & CC1 & "','" & CC2 & "','" & CC3 & "','" & CustodianID & "','" & CustodianName & "'," & Cost & ")")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Get_CompAsset() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("SELECT AstNum, AstID, PurDate, OracleRef, CompRef, AstCat1, AstCat2, AstCat3, CC1 as CCDepart, CC2 as CCSection, CC3 as CCLocation, CustodianID, CustodianName, Cost FROM Report_CompanyAssets order by AstNum")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function CompanyAssets_Report_New(ByVal objattAssets As attItems, ByVal DeptID As String, ByVal IncludeChild As String, ByVal startDate As Date, ByVal EndDate As Date, ByVal DateFilterEnabled As Boolean, ByVal CostFilterCriteria As String, ByVal CostFilterValue As Double) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select Astnum,AssetDetails.astId,Purdate,Refno,REfCode,Custodian.CustodianId,Custodian.CustodianCode,Custodian.CustodianName,Assets.AstCatID,Assetdetails.locID,Assetdetails.BaseCost + Assetdetails.Tax as total,Custodian.DeptId  from Assetdetails ")
            strQuery.Append(" inner join custodian on Assetdetails.CustodianID = Custodian.CustodianId ")
            strQuery.Append(" inner join Assets on Assetdetails.ItemCode= Assets.ItemCode ")
            If objattAssetDetails.BookID <> "" Then
                strQuery.Append(" inner join AstBooks on AstBooks.astid = AssetDetails.astid ")
            End If
            strQuery.Append(" where AssetDetails.IsDeleted = 0 ")
            If DateFilterEnabled Then
                strQuery.Append(" and Purdate >= " & BackEndDate(startDate))
                strQuery.Append(" and Purdate <= " & BackEndDate(EndDate))
            End If

            If objattAssetDetails.LocID.ToString() <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (AssetDetails.LocId like '" & Trim(objattAssetDetails.LocID) & "' or AssetDetails.LocId like '" & Trim(objattAssetDetails.LocID) & "-%' )")
                Else
                    strQuery.Append(" and AssetDetails.LocId like '" & Trim(objattAssetDetails.LocID) & "'")
                End If
            End If

            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If
            If objattAssets.AstBrandID <> 0 Then
                strQuery.Append(" and AssetDetails.AstBrandID = " & objattAssets.AstBrandID.ToString())
            End If

            If DeptID <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (Custodian.DeptID like '" & DeptID & "' or Custodian.DeptID like '" & DeptID & "-%')")
                Else
                    strQuery.Append(" and Custodian.DeptID like '" & DeptID & "'")
                End If
            End If

            If objattAssets.AstCatID <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (Assets.AstCatID like '" & objattAssets.AstCatID.ToString() & "' or Assets.AstCatID like '" & objattAssets.AstCatID.ToString() & "-%')")
                Else
                    strQuery.Append(" and Assets.AstCatID  like '" & objattAssets.AstCatID.ToString() & "'")
                End If
            End If

            If objattAssets.PKeyCode <> 0 Then
                strQuery.Append(" and AssetDetails.ItemCode = " & objattAssets.PKeyCode.ToString())
            End If


            If CostFilterCriteria <> "0" Then
                strQuery.Append(" and AssetDetails.BaseCost + AssetDetails.Tax  " & CostFilterCriteria & CostFilterValue.ToString)
            End If

            If objattAssetDetails.InvStatus > -1 Then
                strQuery.Append(" and AssetDetails.InvStatus = " & objattAssetDetails.InvStatus)
            End If

            If objattAssetDetails.StatusID > 0 Then
                strQuery.Append(" and AssetDetails.StatusID = " & objattAssetDetails.StatusID)
            End If

            If objattAssetDetails.BookID <> "" Then
                strQuery.Append(" and  AstBooks.bookid = '" & objattAssetDetails.BookID & "' ")
            End If

            If objattAssetDetails.CustodianID <> "" Then
                strQuery.Append(" and AssetDetails.CustodianID =  '" & objattAssetDetails.CustodianID & "'")
            End If

            If objattAssetDetails.PKeyCode <> "" Then
                strQuery.Append(" and AstID = '" & objattAssetDetails.PKeyCode & "'")
            End If

            If objattAssetDetails.AstNum <> 0 Then
                strQuery.Append(" and AstNum =  " & objattAssetDetails.AstNum.ToString())
            End If

            If objattAssetDetails.CompanyID <> 0 Then
                strQuery.Append(" and AssetDetails.CompanyID = " & objattAssetDetails.CompanyID & "")
            End If

            If objattAssetDetails.Disposed <> 0 Then
                Dim isDisposed As Int16
                If objattAssetDetails.Disposed Then
                    isDisposed = 1
                Else
                    isDisposed = 0
                End If
                strQuery.Append(" and AssetDetails.Disposed = " & isDisposed & "")
            End If
            If objattAssetDetails.SuppID <> "" Then
                strQuery.Append(" and AssetDetails.SuppID = '" & objattAssetDetails.SuppID & "'")
            End If
            If objattAssetDetails.AstBrandID <> 0 Then
                strQuery.Append(" and AssetDetails.AstBrandID = " & objattAssetDetails.AstBrandID & "")
            End If

            strQuery.Append(" order by AstNum ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function GetCostCenter_AuditReport(ByVal objattAst_INV_Schedule As attInvSchedule, ByVal Status As String, ByVal LocationID As String, ByVal CatID As String, ByVal IncludeChild As Boolean) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            '
            strQuery.Append(" select CostID,CostNumber,CostName,count(*) as TotalCount, ")
            strQuery.Append(" case when Ast_History.status = 0 then 'Missing' ")
            strQuery.Append(" when Ast_History.status = 1 then 'Found' ")
            strQuery.Append(" when Ast_History.status = 2 then 'Misplaced' ")
            strQuery.Append(" when Ast_History.status = 3 then 'Transferred' ")
            strQuery.Append(" when Ast_History.status = 4 then 'Allocated' ")
            strQuery.Append(" when Ast_History.status = 5 then 'Anonymous' ")
            strQuery.Append(" end as Status ")
            strQuery.Append(" from costcenter  ")
            strQuery.Append(" left outer join  AssetDetails on costcenter.CostID  = AssetDetails.CostCenterID ")
            strQuery.Append(" left outer join  Assets on Assets.ItemCode  = AssetDetails.ItemCode ")
            strQuery.Append(" left outer join Ast_History on Ast_History.AstID = AssetDetails.AstID ")
            strQuery.Append(" left outer join AuditStatus on AuditStatus.StatusID = Ast_history.Status ")
            strQuery.Append(" left outer join Ast_INV_Schedule on Ast_INV_Schedule.InvSchCode =  Ast_History.InvSchCode ")
            strQuery.Append(" where Ast_History.IsDeleted =0  and Ast_History.Status<>6 ")
            If objattAst_INV_Schedule.PKeyCode <> 0 Then strQuery.Append(" and Ast_History.InvSchCode='" & objattAst_INV_Schedule.PKeyCode.ToString() & "'")

            If LocationID <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (AssetDetails.LocId like '" & LocationID & "' or AssetDetails.LocId like '" & LocationID & "-%' )")
                Else
                    strQuery.Append(" and AssetDetails.LocId like '" & LocationID & "'")
                End If
            End If

            If CatID <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (Assets.AstCatID like '" & CatID & "' or Assets.AstCatID like '" & CatID & "-%')")
                Else
                    strQuery.Append(" and Assets.AstCatID  like '" & CatID & "'")
                End If
            End If

            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If

            'Disposed Assets should not come in Audit Reports if the asset disposed before the inventory schedual.
            strQuery.Append(" and (AssetDetails.Disposed= 0 or (AssetDetails.Disposed = 1 and AssetDetails.DispDate > Ast_INV_Schedule.InvEndDate))  ")

            strQuery.Append(" group by CostID,CostNumber,CostName,Ast_History.status ")
            strQuery.Append(" order by costID ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAll_AuditStatusReport(ByVal objattAst_INV_Schedule As attInvSchedule, ByVal Status As String, ByVal LocationID As String, ByVal CatID As String, ByVal IncludeChild As Boolean) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            '
            strQuery.Append("select Ast_History.InvSchCode,Ast_history.AstID, AssetDetails.AstDesc AS AstDesc1, AssetDetails.AstDesc2, Assets.AstDesc AS ItemDesc, Assets.ItemCode,AssetDetails.AstNum,AssetDetails.AstModel,AssetDetails.SerailNo,")
            If AppConfig.DescForRpt = "Asset Description 1" Then
                strQuery.Append("Assetdetails.AstDesc as Description")
            ElseIf AppConfig.DescForRpt = "Asset Description 2" Then
                strQuery.Append("Assetdetails.AstDesc2 as Description")
            ElseIf AppConfig.DescForRpt = "Item Description" Then
                strQuery.Append("Assets.AstDesc as Description")
            Else
                strQuery.Append("Assetdetails.AstDesc as Description")
            End If
            strQuery.Append(" ,frLoc.LocationFullPath as PrevLoc,toLoc.LocationFullPath AS NewLoc,Ast_history.Status,AuditStatus.StatusDesc,HisDate,CatFullPath,Custodian.CustodianID,Custodian.CustodianName ,Custodian.CustodianCode,AssetDetails.InventoryNumber as Remarks from Ast_History")

            strQuery.Append(" left outer join Location frLoc on Ast_History.Fr_Loc = frLoc.LocID")
            strQuery.Append(" left outer join Location toLoc on Ast_History.To_Loc = toLoc.LocID")
            strQuery.Append(" left outer join AssetDetails  on Ast_history.AstID = AssetDetails.AstID")
            strQuery.Append(" left outer join Custodian on Custodian.CustodianID = AssetDetails.CustodianID ")
            strQuery.Append(" left outer join Assets on AssetDetails.ItemCode = Assets.ItemCode ")
            strQuery.Append(" left outer join Category ON Assets.AstCatID = Category.AstCatID")
            strQuery.Append(" left outer join AuditStatus on AuditStatus.StatusID = Ast_history.Status")
            strQuery.Append(" left outer join Ast_INV_Schedule on Ast_INV_Schedule.InvSchCode =  Ast_History.InvSchCode ")
            strQuery.Append(" where Ast_History.IsDeleted =0 ")
            If objattAst_INV_Schedule.PKeyCode <> 0 Then strQuery.Append(" and Ast_History.InvSchCode='" & objattAst_INV_Schedule.PKeyCode.ToString() & "'")
            If Status <> "" Then
                strQuery.Append(" and Ast_History.Status=" & Status & "")
            Else
                strQuery.Append(" and Ast_History.Status<>6") 'to hide the lost status from the audit report.
            End If

            If LocationID <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (AssetDetails.LocId like '" & LocationID & "' or AssetDetails.LocId like '" & LocationID & "-%' )")
                Else
                    strQuery.Append(" and AssetDetails.LocId like '" & LocationID & "'")
                End If
            End If

            If CatID <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (Assets.AstCatID like '" & CatID & "' or Assets.AstCatID like '" & CatID & "-%')")
                Else
                    strQuery.Append(" and Assets.AstCatID  like '" & CatID & "'")
                End If
            End If

            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If

            'Disposed Assets should not come in Audit Reports if the asset disposed before the inventory schedual.
            strQuery.Append(" and (AssetDetails.Disposed= 0 or (AssetDetails.Disposed = 1 and AssetDetails.DispDate > Ast_INV_Schedule.InvEndDate)) Order by AssetDetails.AstNum ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function CompanyAssetReport(ByVal objattAssets As attItems, ByVal DeptID As String, ByVal IncludeChild As String, ByVal startDate As Date, ByVal EndDate As Date, ByVal DateFilterEnabled As Boolean, ByVal CostFilterCriteria As String, ByVal CostFilterValue As Double) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append(" SELECT AssetDetails.AstID,AstNum,RefNo,Location.Compcode,CustodianName,Assetdetails.AstDesc as AstDesc1,Assetdetails.AstDesc2,Assets.AstDesc as ItemDesc,Assets.ItemCode,Companies.CompanyCode,Companies.CompanyName,")
            strQuery.Append(" Category.AstCatDesc,PurDate,")
            If AppConfig.DescForRpt = "Asset Description 1" Then
                strQuery.Append("Assetdetails.AstDesc as Description")
            ElseIf AppConfig.DescForRpt = "Asset Description 2" Then
                strQuery.Append("Assetdetails.AstDesc2 as Description")
            ElseIf AppConfig.DescForRpt = "Item Description" Then
                strQuery.Append("Assets.AstDesc as Description")
            Else
                strQuery.Append("Assetdetails.AstDesc as Description")
            End If

            strQuery.Append(" ,BaseCost,Tax,Discount,BaseCost+Tax-Discount as tot ,AssetDetails.InvNumber,Supplier.SuppName,Insurer.InsName,Location.LocID,Brand.AstBrandName,AssetDetails.SrvDate, AssetDetails.AstModel, AssetDetails.DispDate, AssetDetails.Disposed, AssetDetails.BarCode, AssetDetails.SerailNo, AssetDetails.RefCode, AssetDetails.Plate, AssetDetails.Poerp, AssetDetails.Capex, AssetDetails.Grn, AssetDetails.GLCode, Category.AstCatID,Category.CatFullPath,Location.LocationFullPath, Custodian.CustodianID, Custodian.CustodianCode  FROM AssetDetails ")
            strQuery.Append(" inner join (Assets left outer join Category on Category.AstCatID = Assets.AstCatID)on Assets.ItemCode = AssetDetails.ItemCode ")
            strQuery.Append(" inner join Custodian on Custodian.CustodianID = AssetDetails.CustodianID ")
            strQuery.Append(" inner join Companies on Companies.CompanyId = AssetDetails.CompanyId ")
            strQuery.Append(" left outer join Location on Location.LocId = AssetDetails.LocId ")
            strQuery.Append(" left outer join Supplier on Supplier.SuppID = AssetDetails.SuppID ")
            strQuery.Append(" left outer join Insurer on Insurer.InsCode = AssetDetails.InsID ")
            strQuery.Append(" left outer join Brand ON AssetDetails.AstBrandId = Brand.AstBrandID ")
            If objattAssetDetails.BookID <> "" Then
                strQuery.Append(" inner join AstBooks on AstBooks.astid = AssetDetails.astid ")
            End If

            strQuery.Append(" WHERE AssetDetails.IsDeleted = 0")

            If DateFilterEnabled Then
                strQuery.Append(" and Purdate >= " & BackEndDate(startDate))
                strQuery.Append(" and Purdate <= " & BackEndDate(EndDate))
            End If

            If objattAssetDetails.LocID.ToString() <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (AssetDetails.LocId like '" & Trim(objattAssetDetails.LocID) & "' or AssetDetails.LocId like '" & Trim(objattAssetDetails.LocID) & "-%' )")
                Else
                    strQuery.Append(" and AssetDetails.LocId like '" & Trim(objattAssetDetails.LocID) & "'")
                End If

            End If
            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If

            If objattAssetDetails.BookID <> "" Then
                strQuery.Append(" and  AstBooks.bookid = '" & objattAssetDetails.BookID & "' ")
            End If

            If objattAssets.AstBrandID <> 0 Then
                strQuery.Append(" and AssetDetails.AstBrandID = " & objattAssets.AstBrandID.ToString())
            End If

            If objattAssets.PKeyCode <> 0 Then
                strQuery.Append(" and AssetDetails.ItemCode = " & objattAssets.PKeyCode.ToString())
            End If

            If CostFilterCriteria <> "0" Then
                strQuery.Append(" and AssetDetails.BaseCost + AssetDetails.Tax  " & CostFilterCriteria & CostFilterValue.ToString)
            End If

            If objattAssetDetails.InvStatus > -1 Then
                strQuery.Append(" and AssetDetails.InvStatus = " & objattAssetDetails.InvStatus)
            End If

            If objattAssetDetails.StatusID > 0 Then
                strQuery.Append(" and AssetDetails.StatusID = " & objattAssetDetails.StatusID)
            End If

            If objattAssets.AstCatID <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (Assets.AstCatID like '" & objattAssets.AstCatID.ToString() & "' or Assets.AstCatID like '" & objattAssets.AstCatID.ToString() & "-%')")
                Else
                    strQuery.Append(" and Assets.AstCatID  like '" & objattAssets.AstCatID.ToString() & "'")

                End If
            End If

            If objattAssetDetails.CustodianID <> "" Then
                strQuery.Append(" and AssetDetails.CustodianID = '" & objattAssetDetails.CustodianID & "'")
            End If

            If objattAssetDetails.PKeyCode <> "" Then
                strQuery.Append(" and  AssetDetails.AstID = '" & objattAssetDetails.PKeyCode & "'")
            End If
            If objattAssetDetails.AstNum <> 0 Then
                strQuery.Append(" and AstNum = " & objattAssetDetails.AstNum.ToString())
            End If

            If objattAssetDetails.CompanyID <> 0 Then
                strQuery.Append(" and AssetDetails.CompanyID = " & objattAssetDetails.CompanyID & "")
            End If

            Dim isDisposed As Int16
            If objattAssetDetails.Disposed Then
                isDisposed = 1
                strQuery.Append(" and AssetDetails.Disposed = " & isDisposed & "")
            ElseIf objattAssetDetails.ExcludeDisposed = "MUST" Then
                isDisposed = 0
                strQuery.Append(" and AssetDetails.Disposed = " & isDisposed & "")
            Else
                isDisposed = 0
            End If

            If objattAssetDetails.SuppID <> "" Then
                strQuery.Append(" and AssetDetails.SuppID = '" & objattAssetDetails.SuppID & "'")
            End If

            'Transremarks used as source field in CMA Application
            If objattAssetDetails.TransRemarks <> "" Then
                strQuery.Append(" and AssetDetails.TransRemarks = '" & objattAssetDetails.TransRemarks & "'")
            End If

            If objattAssetDetails.AstBrandID <> 0 Then
                strQuery.Append(" and AssetDetails.AstBrandID = " & objattAssetDetails.AstBrandID & "")
            End If

            If DeptID <> "" Then
                strQuery.Append(" and Custodian.DeptID = '" & DeptID & "'")
            End If
            strQuery.Append(" order by AstNum ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function AssetsLogReport(ByVal objattAssets As attItems, ByVal DeptID As String, ByVal IncludeChild As String, ByVal startDate As Date, ByVal EndDate As Date, ByVal DateFilterEnabled As Boolean, ByVal CostFilterCriteria As String, ByVal CostFilterValue As Double) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            strQuery.Append(" SELECT AssetDetailsLog.AstID,AstNum,RefNo,Location.Compcode,CustodianName,AssetDetailsLog.AstDesc as AstDesc1,AssetDetailsLog.AstDesc2,Assets.AstDesc as ItemDesc,Assets.ItemCode,Companies.CompanyCode,Companies.CompanyName,")
            strQuery.Append(" Category.AstCatDesc,PurDate,")
            If AppConfig.DescForRpt = "Asset Description 1" Then
                strQuery.Append("AssetDetailsLog.AstDesc as Description")
            ElseIf AppConfig.DescForRpt = "Asset Description 2" Then
                strQuery.Append("AssetDetailsLog.AstDesc2 as Description")
            ElseIf AppConfig.DescForRpt = "Item Description" Then
                strQuery.Append("Assets.AstDesc as Description")
            Else
                strQuery.Append("AssetDetailsLog.AstDesc as Description")
            End If

            strQuery.Append(" ,BaseCost,Tax,Discount,BaseCost+Tax-Discount as tot ,AssetDetailsLog.InvNumber,Supplier.SuppName,Insurer.InsName,Location.LocID,Brand.AstBrandName,AssetDetailsLog.SrvDate, AssetDetailsLog.AstModel, AssetDetailsLog.DispDate, AssetDetailsLog.Disposed, AssetDetailsLog.BarCode, AssetDetailsLog.SerailNo, AssetDetailsLog.RefCode, AssetDetailsLog.Plate, AssetDetailsLog.Poerp, AssetDetailsLog.Capex, AssetDetailsLog.Grn, AssetDetailsLog.GLCode, Category.AstCatID,Category.CatFullPath,Location.LocationFullPath, Custodian.CustodianID, Custodian.CustodianCode,AssetDetailsLog.ActionType,AssetDetailsLog.ActionDate,AssetDetailsLog.LastEditBY as ActionUser  FROM AssetDetailsLog ")
            strQuery.Append(" inner join (Assets left outer join Category on Category.AstCatID = Assets.AstCatID)on Assets.ItemCode = AssetDetailsLog.ItemCode ")
            strQuery.Append(" inner join Custodian on Custodian.CustodianID = AssetDetailsLog.CustodianID ")
            strQuery.Append(" inner join Companies on Companies.CompanyId = AssetDetailsLog.CompanyId ")
            strQuery.Append(" left outer join Location on Location.LocId = AssetDetailsLog.LocId ")
            strQuery.Append(" left outer join Supplier on Supplier.SuppID = AssetDetailsLog.SuppID ")
            strQuery.Append(" left outer join Insurer on Insurer.InsCode = AssetDetailsLog.InsID ")
            strQuery.Append(" left outer join Brand ON AssetDetailsLog.AstBrandId = Brand.AstBrandID ")
            If objattAssetDetails.BookID <> "" Then
                strQuery.Append(" inner join AstBooks on AstBooks.astid = AssetDetailsLog.astid ")
            End If

            strQuery.Append(" WHERE AssetDetailsLog.IsDeleted = 0")

            If DateFilterEnabled Then
                strQuery.Append(" and Purdate >= " & BackEndDate(startDate))
                strQuery.Append(" and Purdate <= " & BackEndDate(EndDate))
            End If

            If objattAssetDetails.LocID.ToString() <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (AssetDetailsLog.LocId like '" & Trim(objattAssetDetails.LocID) & "' or AssetDetailsLog.LocId like '" & Trim(objattAssetDetails.LocID) & "-%' )")
                Else
                    strQuery.Append(" and AssetDetailsLog.LocId like '" & Trim(objattAssetDetails.LocID) & "'")
                End If

            End If
            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  AssetDetailsLog.CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If

            If objattAssetDetails.BookID <> "" Then
                strQuery.Append(" and  AstBooks.bookid = '" & objattAssetDetails.BookID & "' ")
            End If

            If objattAssets.AstBrandID <> 0 Then
                strQuery.Append(" and AssetDetailsLog.AstBrandID = " & objattAssets.AstBrandID.ToString())
            End If

            If objattAssets.PKeyCode <> 0 Then
                strQuery.Append(" and AssetDetailsLog.ItemCode = " & objattAssets.PKeyCode.ToString())
            End If

            If CostFilterCriteria <> "0" Then
                strQuery.Append(" and AssetDetailsLog.BaseCost + AssetDetailsLog.Tax  " & CostFilterCriteria & CostFilterValue.ToString)
            End If

            If objattAssetDetails.InvStatus > -1 Then
                strQuery.Append(" and AssetDetailsLog.InvStatus = " & objattAssetDetails.InvStatus)
            End If

            If objattAssetDetails.StatusID > 0 Then
                strQuery.Append(" and AssetDetailsLog.StatusID = " & objattAssetDetails.StatusID)
            End If

            If objattAssets.AstCatID <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (Assets.AstCatID like '" & objattAssets.AstCatID.ToString() & "' or Assets.AstCatID like '" & objattAssets.AstCatID.ToString() & "-%')")
                Else
                    strQuery.Append(" and Assets.AstCatID  like '" & objattAssets.AstCatID.ToString() & "'")

                End If
            End If

            If objattAssetDetails.CustodianID <> "" Then
                strQuery.Append(" and AssetDetailsLog.CustodianID = '" & objattAssetDetails.CustodianID & "'")
            End If

            If objattAssetDetails.PKeyCode <> "" Then
                strQuery.Append(" and  AssetDetailsLog.AstID = '" & objattAssetDetails.PKeyCode & "'")
            End If
            If objattAssetDetails.AstNum <> 0 Then
                strQuery.Append(" and AstNum = " & objattAssetDetails.AstNum.ToString())
            End If

            If objattAssetDetails.CompanyID <> 0 Then
                strQuery.Append(" and AssetDetailsLog.CompanyID = " & objattAssetDetails.CompanyID & "")
            End If

            Dim isDisposed As Int16
            If objattAssetDetails.Disposed Then
                isDisposed = 1
                strQuery.Append(" and AssetDetailsLog.Disposed = " & isDisposed & "")
            ElseIf objattAssetDetails.ExcludeDisposed = "MUST" Then
                isDisposed = 0
                strQuery.Append(" and AssetDetailsLog.Disposed = " & isDisposed & "")
            Else
                isDisposed = 0
            End If

            If objattAssetDetails.SuppID <> "" Then
                strQuery.Append(" and AssetDetailsLog.SuppID = '" & objattAssetDetails.SuppID & "'")
            End If

            'Transremarks used as source field in CMA Application
            If objattAssetDetails.TransRemarks <> "" Then
                strQuery.Append(" and AssetDetailsLog.TransRemarks = '" & objattAssetDetails.TransRemarks & "'")
            End If

            If objattAssetDetails.AstBrandID <> 0 Then
                strQuery.Append(" and AssetDetailsLog.AstBrandID = " & objattAssetDetails.AstBrandID & "")
            End If

            If DeptID <> "" Then
                strQuery.Append(" and Custodian.DeptID = '" & DeptID & "'")
            End If
            strQuery.Append(" order by AstNum ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function DisposedAssetReport(ByVal objattAssets As attItems, ByVal DeptID As String, ByVal IncludeChild As String, ByVal DisposeStartDate As Date, ByVal DisposeEndDate As Date, ByVal DispCode As String, ByVal DateFilterEnabled As Boolean, ByVal CostFilterCriteria As String, ByVal CostFilterValue As Double) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            strQuery.Append(" SELECT AssetDetails.AstID,AstNum,RefNo,Location.Compcode,CustodianName, AssetDetails.AstDesc AS AstDesc1, AssetDetails.AstDesc2, Assets.AstDesc AS ItemDesc, Assets.ItemCode,AssetDetails.isSold,AssetDetails.Soldto,AssetDetails.Sel_Price,AssetDetails.Sel_Date,AssetDetails.Discount,Disposal_Method.DispDesc,AssetDetails.TransRemarks,AssetDetails.DisposalComments,")
            strQuery.Append(" Category.AstCatDesc,PurDate,")
            If AppConfig.DescForRpt = "Asset Description 1" Then
                strQuery.Append("Assetdetails.AstDesc as Description")
            ElseIf AppConfig.DescForRpt = "Asset Description 2" Then
                strQuery.Append("Assetdetails.AstDesc2 as Description")
            ElseIf AppConfig.DescForRpt = "Item Description" Then
                strQuery.Append("Assets.AstDesc as Description")
            Else
                strQuery.Append("Assetdetails.AstDesc as Description")
            End If

            strQuery.Append(" ,BaseCost,Tax,Discount,BaseCost+Tax-Discount as tot ,AssetDetails.InvNumber,Supplier.SuppName,Insurer.InsName,Location.LocID,Brand.AstBrandName,AssetDetails.SrvDate, AssetDetails.AstModel, AssetDetails.DispDate, AssetDetails.Disposed, AssetDetails.BarCode, AssetDetails.SerailNo, AssetDetails.RefCode, AssetDetails.Plate, AssetDetails.Poerp, AssetDetails.Capex, AssetDetails.Grn, AssetDetails.GLCode, Category.AstCatID,Category.CatFullPath,Location.LocationFullPath , Custodian.CustodianID, Custodian.CustodianCode FROM AssetDetails ")
            strQuery.Append(" inner join (Assets left outer join Category on Category.AstCatID = Assets.AstCatID)on Assets.ItemCode = AssetDetails.ItemCode ")
            strQuery.Append(" inner join Custodian on Custodian.CustodianID = AssetDetails.CustodianID ")
            strQuery.Append(" left outer join Location on Location.LocId = AssetDetails.LocId ")
            strQuery.Append(" left outer join Supplier on Supplier.SuppID = AssetDetails.SuppID ")
            strQuery.Append(" left outer join Insurer on Insurer.InsCode = AssetDetails.InsID ")
            strQuery.Append(" left outer join Brand ON AssetDetails.AstBrandId = Brand.AstBrandID ")
            strQuery.Append(" left outer join Disposal_Method on Disposal_Method.DispCode = AssetDetails.DispCode ")
            If objattAssetDetails.BookID <> "" Then
                strQuery.Append(" inner join AstBooks on AstBooks.astid = AssetDetails.astid ")
            End If

            strQuery.Append(" WHERE AssetDetails.IsDeleted = 0 and Assetdetails.Disposed = 1")

            If DateFilterEnabled Then
                strQuery.Append(" and DispDate >= " & BackEndDate(DisposeStartDate))
                strQuery.Append(" and DispDate <= " & BackEndDate(DisposeEndDate))
            End If

            If Not String.IsNullOrEmpty(DispCode) Then
                strQuery.Append(" and AssetDetails.DispCode = " & DispCode)
            End If

            If objattAssetDetails.LocID.ToString() <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (AssetDetails.LocId like '" & Trim(objattAssetDetails.LocID) & "' or AssetDetails.LocId like '" & Trim(objattAssetDetails.LocID) & "-%' )")
                Else
                    strQuery.Append(" and AssetDetails.LocId like '" & Trim(objattAssetDetails.LocID) & "'")
                End If
            End If
            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If

            If objattAssetDetails.BookID <> "" Then
                strQuery.Append(" and  AstBooks.bookid = '" & objattAssetDetails.BookID & "' ")
            End If

            If objattAssets.AstBrandID <> 0 Then
                strQuery.Append(" and AssetDetails.AstBrandID = " & objattAssets.AstBrandID.ToString())
            End If

            If CostFilterCriteria <> "0" Then
                strQuery.Append(" and AssetDetails.BaseCost + AssetDetails.Tax  " & CostFilterCriteria & CostFilterValue.ToString)
            End If

            If objattAssetDetails.InvStatus > -1 Then
                strQuery.Append(" and AssetDetails.InvStatus = " & objattAssetDetails.InvStatus)
            End If

            If objattAssetDetails.StatusID > 0 Then
                strQuery.Append(" and AssetDetails.StatusID = " & objattAssetDetails.StatusID)
            End If

            If objattAssets.AstCatID <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (Assets.AstCatID like '" & objattAssets.AstCatID.ToString() & "' or Assets.AstCatID like '" & objattAssets.AstCatID.ToString() & "-%')")
                Else
                    strQuery.Append(" and Assets.AstCatID  like '" & objattAssets.AstCatID.ToString() & "'")

                End If
            End If

            If objattAssets.PKeyCode <> 0 Then
                strQuery.Append(" and AssetDetails.ItemCode = " & objattAssets.PKeyCode.ToString())
            End If

            If objattAssetDetails.CustodianID <> "" Then
                strQuery.Append(" and AssetDetails.CustodianID = '" & objattAssetDetails.CustodianID & "'")
            End If

            If objattAssetDetails.PKeyCode <> "" Then
                strQuery.Append(" and  AssetDetails.AstID = '" & objattAssetDetails.PKeyCode & "'")
            End If
            If objattAssetDetails.AstNum <> 0 Then
                strQuery.Append(" and AstNum = " & objattAssetDetails.AstNum.ToString())
            End If

            If objattAssetDetails.CompanyID <> 0 Then
                strQuery.Append(" and AssetDetails.CompanyID = " & objattAssetDetails.CompanyID & "")
            End If

            If objattAssetDetails.SuppID <> "" Then
                strQuery.Append(" and AssetDetails.SuppID = '" & objattAssetDetails.SuppID & "'")
            End If

            If objattAssetDetails.AstBrandID <> 0 Then
                strQuery.Append(" and AssetDetails.AstBrandID = " & objattAssetDetails.AstBrandID & "")
            End If

            If DeptID <> "" Then
                strQuery.Append(" and Custodian.DeptID = '" & DeptID & "'")
            End If
            strQuery.Append(" order by AstNum ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function Depreciation_Book(ByVal forTemp1 As String, ByVal strCompanyID As String, ByVal strBookId As String, ByVal strCustID As String, ByVal strLocId As String, ByVal strCatID As String, ByVal IncludeChild As Boolean, ByVal HierCode As String, ByVal ShowDisposed As Byte, ByVal ExcludeDisposed As String, ByVal BrandID As String, ByVal SupplierID As String, ByVal ItemCode As String, ByVal CostFilterCriteria As String, ByVal CostFilterValue As Double, ByVal StatusID As Integer, ByVal InvStatus As Integer, ByVal FromDate As Date, ByVal ToDate As Date, ByVal DateFilterEnabled As Boolean) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If forTemp1 = "" Then
                strQuery.Append(" SELECT '',AssetDetails.AstID,AstNum,RefNo,Location.Compcode,Location.LocationFullPath,Category.CatFullPath,Category.AstCatDesc,Custodian.CustodianName, ")
                strQuery.Append(" AssetDetails.AstDesc,AssetDetails.AstDesc2,BaseCost+Tax  as tot,DATEADD(year,salvageyear,DATEADD(month,SalvageMonth,SrvDate)) as EndOfServiceDate,salvageYear,SalvageMonth,SrvDate as ServiceDate, (BaseCost+Tax) - CurrentBV as acc, CurrentBV, Assets.AstDesc AS ItemDesc, Assets.ItemCode, Custodian.CustodianID, Custodian.CustodianCode,PurDate,100/IsNULL(nullif(((salvageyear * 12 + SalvageMonth)/12),0),1) as DepPercentage FROM AssetDetails  ")
                strQuery.Append(" inner join (Assets left outer join Category on Category.AstCatID = Assets.AstCatID)on Assets.ItemCode = AssetDetails.ItemCode  ")
                strQuery.Append(" inner join Custodian on Custodian.CustodianID = AssetDetails.CustodianID ")
                strQuery.Append(" inner join AstBooks on AssetDetails.AstID= AstBooks.AstID  ")
                strQuery.Append(" left outer join Location on Location.LocId = AssetDetails.LocId  ")
                strQuery.Append(" where AssetDetails.IsDeleted = 0 ")

                If ShowDisposed = 1 Then
                    strQuery.Append(" and AssetDetails.Disposed = " & ShowDisposed & "")
                ElseIf ExcludeDisposed = "MUST" Then
                    strQuery.Append(" and AssetDetails.Disposed = 0")
                End If


                If DateFilterEnabled Then
                    strQuery.Append(" and Purdate >= " & BackEndDate(FromDate))
                    strQuery.Append(" and Purdate <= " & BackEndDate(ToDate))
                End If

                If strCompanyID <> "" Then
                    strQuery.Append(" and AssetDetails.CompanyID = " & strCompanyID & "")
                End If
                If strBookId <> "" Then
                    strQuery.Append(" and Astbooks.BookID =" & strBookId & "")
                End If
                If strCustID <> "" Then
                    strQuery.Append(" and AssetDetails.CustodianID ='" & strCustID & "'")
                End If
                If strLocId <> "" Then
                    If IncludeChild Then
                        strQuery.Append(" and (AssetDetails.LocId like '" & Trim(strLocId) & "' or AssetDetails.LocId like '" & Trim(strLocId) & "-%' )")
                    Else
                        strQuery.Append(" and AssetDetails.LocId like '" & Trim(strLocId) & "'")
                    End If
                End If
                If strCatID <> "" Then
                    If IncludeChild Then
                        strQuery.Append(" and (Assets.AstCatID like '" & strCatID & "' or Assets.AstCatID like '" & strCatID & "-%')")
                    Else
                        strQuery.Append(" and Assets.AstCatID  like '" & strCatID & "'")

                    End If
                End If
                If ItemCode <> 0 Then
                    strQuery.Append(" and AssetDetails.ItemCode = " & ItemCode)
                End If
                If CostFilterCriteria <> "0" Then
                    strQuery.Append(" and AssetDetails.BaseCost + AssetDetails.Tax  " & CostFilterCriteria & CostFilterValue.ToString)
                End If

                If HierCode <> "" Then
                    strQuery.Append(" and Custodian.DeptID = '" & HierCode & "'")
                End If

                If BrandID <> "" Then
                    strQuery.Append(" and AssetDetails.AstBrandId = '" & BrandID & "'")
                End If
                If SupplierID <> "" Then
                    strQuery.Append(" and AssetDetails.suppid = '" & SupplierID & "'")
                End If
                strQuery.Append(" order by AssetDetails.AstNum")
            ElseIf forTemp1 = "1" Then
                strQuery.Append("  SELECT '',AssetDetails.AstID,AstNum,RefNo,Location.Compcode,Category.AstCatDesc,Custodian.CustodianName, ")
                strQuery.Append("  AssetDetails.AstDesc,AssetDetails.AstDesc2,BaseCost+Tax  as tot,salvageYear,SalvageMonth,SrvDate as ServiceDate, (BaseCost+Tax) - CurrentBV as acc, CurrentBV  FROM AssetDetails ")
                strQuery.Append("  inner join (Assets left outer join Category on Category.AstCatID = Assets.AstCatID)on Assets.ItemCode = AssetDetails.ItemCode  ")
                strQuery.Append("  inner join Custodian on Custodian.CustodianID = AssetDetails.CustodianID ")
                strQuery.Append("  inner join AstBooks on AssetDetails.AstID= AstBooks.AstID ")
                strQuery.Append("  left outer join Location on Location.LocId = AssetDetails.LocId  ")
                strQuery.Append(" where AssetDetails.IsDeleted = 0")
                If strCompanyID <> "" Then
                    strQuery.Append(" and AssetDetails.CompanyID = " & strCompanyID & "")
                End If
                If strBookId <> "" Then
                    strQuery.Append(" and Astbooks.BookID =" & strBookId & "")
                End If
                If strCustID <> "" Then
                    strQuery.Append(" and AssetDetails.CustodianID ='" & strCustID & "'")
                End If
                If strLocId <> "" Then
                    If IncludeChild Then
                        strQuery.Append(" and (AssetDetails.LocId like '" & Trim(strLocId) & "' or AssetDetails.LocId like '" & Trim(strLocId) & "-%' )")
                    Else
                        strQuery.Append(" and AssetDetails.LocId like '" & Trim(objattAssetDetails.LocID) & "'")
                    End If
                End If
                If strCatID <> "" Then
                    If IncludeChild Then
                        strQuery.Append(" and (Assets.AstCatID like '" & strCatID & "' or Assets.AstCatID like '" & strCatID & "-%')")
                    Else
                        strQuery.Append(" and Assets.AstCatID  like '" & strCatID & "'")

                    End If
                End If
                If ItemCode <> 0 Then
                    strQuery.Append(" and AssetDetails.ItemCode = " & ItemCode)
                End If
                If CostFilterCriteria <> "0" Then
                    strQuery.Append(" and AssetDetails.BaseCost + AssetDetails.Tax  " & CostFilterCriteria & CostFilterValue.ToString)
                End If

                If objattAssetDetails.InvStatus > -1 Then
                    strQuery.Append(" and AssetDetails.InvStatus = " & objattAssetDetails.InvStatus)
                End If

                If objattAssetDetails.StatusID > 0 Then
                    strQuery.Append(" and AssetDetails.StatusID = " & objattAssetDetails.StatusID)
                End If

                If HierCode <> "" Then
                    strQuery.Append(" and Custodian.DeptID = '" & HierCode & "'")
                End If

                If BrandID <> "" Then
                    strQuery.Append(" and AssetDetails.AstBrandId = '" & BrandID & "'")
                End If
                If SupplierID <> "" Then
                    strQuery.Append(" and AssetDetails.suppid = '" & SupplierID & "'")
                End If

                strQuery.Append("  order by AssetDetails.AstNum")
            End If


            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
    End Class

End Namespace