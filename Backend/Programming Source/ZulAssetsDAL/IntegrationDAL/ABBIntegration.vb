Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text

Namespace ZulAssetsDAL
    Public Class ABBIntegration
        Private _Command As IDbCommand

        Public Structure FilterOptions
            Public LocID As String
            Public CustodianID As String
            Public CostCenterID As String
            Public CompanyID As String
            Public AssetClassID As String
            Public BussinessArea As String
            Public InventoryNumber As String
            Public EvaluationGroup1 As String
            Public EvaluationGroup2 As String
            Public EvaluationGroup3 As String
            Public EvaluationGroup4 As String
            Public DataSource As String
            Public ShowOnlyRetiredAssets As Boolean
            Public ExcludeRetiredAssets As Boolean
            Public ShowOnlyAssetValue As Boolean
            Public ExcludeAssetValue As Boolean
            Public ShowOnlyCapitalizedAssets As Boolean
            Public ExcludeCapitalizedAssets As Boolean
            Public IncludeSubLevels As Boolean
            Public FilterByCreation As Boolean
            Public CreationFromDate As String
            Public CreationToDate As Date
            Public FilterByCapitalize As Boolean
            Public CapitalizestartDate As String
            Public CapitalizeEndDate As String
            Public FilterByRetirement As Boolean
            Public RetirementstartDate As String
            Public RetirementEndDate As String
            Public FromAssetNumber As String
            Public ToAssetNumber As String
            Public InvStatus As String
            Public InvSchID As Integer
            Public Status As String
        End Structure

        Public Function GetInvProposalEmpList(ByVal InvNo As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select distinct CustodianID from AssetDetails where InventoryNumber=?")
            objCommand.CommandText = strQuery.ToString()
            objCommand.Parameters.Add(New OleDbParameter("@InventoryNumber", InvNo))
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetInvProposalAssetsList(ByVal InvNo As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select RefNo from AssetDetails where InventoryNumber=?")
            objCommand.CommandText = strQuery.ToString()
            objCommand.Parameters.Add(New OleDbParameter("@InventoryNumber", InvNo))
            _Command = objCommand
            Return objCommand
        End Function

        'Public Function GetGRItems(ByVal GRGuid As Guid) As IDbCommand
        '    Dim objCommand As OleDbCommand = New OleDbCommand
        '    Dim strQuery As New StringBuilder
        '    strQuery.Append("select GRItemID,ManPartNo,SAPMatCode,LineItemNo,SeqNo,ProductSerialNo,Status from GRItems where GRGUID=?")
        '    objCommand.CommandText = strQuery.ToString()
        '    objCommand.Parameters.Add(New OleDbParameter("@GRGUID", GRGuid))
        '    _Command = objCommand
        '    Return objCommand
        'End Function

        'Public Function GetGRByID(ByVal GRID As String) As IDbCommand
        '    Dim objCommand As OleDbCommand = New OleDbCommand
        '    Dim strQuery As New StringBuilder
        '    strQuery.Append("select GUID,GRID,PONo,DeliveryNo,PostingDate from GR where GRID=?")
        '    objCommand.CommandText = strQuery.ToString()
        '    objCommand.Parameters.Add(New OleDbParameter("@GRID", GRID))
        '    _Command = objCommand
        '    Return objCommand
        'End Function

        'Public Function GetGRList() As IDbCommand
        '    Dim objCommand As OleDbCommand = New OleDbCommand
        '    Dim strQuery As New StringBuilder
        '    strQuery.Append("select GRID,PONo,DeliveryNo,PostingDate from GR")
        '    objCommand.CommandText = strQuery.ToString()
        '    _Command = objCommand
        '    Return objCommand
        'End Function

        Public Function GetChangedDataABBExport() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("SELECT      AssetDetails.AstID, AssetDetails.AstDesc,AssetDetails.AstDesc2, Companies.CompanyCode, AssetDetails.SerailNo,AssetDetails.RefNo,  Location.LocationFullPath,Custodian.CustodianID")
            strQuery.Append(" FROM         AssetDetails INNER JOIN")
            strQuery.Append(" Location ON Location.LocID = AssetDetails.LocID INNER JOIN")
            strQuery.Append(" Assets ON AssetDetails.ItemCode = Assets.ItemCode INNER JOIN")
            strQuery.Append(" Companies ON AssetDetails.CompanyID = Companies.CompanyId INNER JOIN")
            strQuery.Append(" Custodian ON AssetDetails.CustodianID = Custodian.CustodianID INNER JOIN")
            strQuery.Append(" Category ON Assets.AstCatID = Category.AstCatID")
            strQuery.Append(" WHERE     AssetDetails.IsDeleted = 0 and IsDataChanged = 1  and (convert(varchar,LastEditDate,103) = convert(varchar,getdate(),103) or convert(varchar,CreationDate,103) = convert(varchar,getdate(),103))")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetBusinessAreaList() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("SELECT distinct AssetDetails.BussinessArea FROM AssetDetails where BussinessArea is not null and BussinessArea <> '' order by BussinessArea")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetEvaluationGroup1List() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("SELECT distinct AssetDetails.EvaluationGroup1 FROM AssetDetails where EvaluationGroup1 is not null and EvaluationGroup1 <> '' order by EvaluationGroup1")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetPhysicalInventoryABBExport() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("SELECT     AssetDetails.AstID, AssetDetails.AstDesc,AssetDetails.AstDesc2, Companies.CompanyCode, AssetDetails.SerailNo,AssetDetails.RefNo,  Location.LocationFullPath,Custodian.CustodianID,convert(varchar, LastInventoryDate, 104) as InventoryDate")
            strQuery.Append(" FROM         AssetDetails INNER JOIN")
            strQuery.Append(" Location ON Location.LocID = AssetDetails.LocID INNER JOIN")
            strQuery.Append(" Assets ON AssetDetails.ItemCode = Assets.ItemCode INNER JOIN")
            strQuery.Append(" Companies ON AssetDetails.CompanyID = Companies.CompanyId INNER JOIN")
            strQuery.Append(" Custodian ON AssetDetails.CustodianID = Custodian.CustodianID INNER JOIN")
            strQuery.Append(" Category ON Assets.AstCatID = Category.AstCatID")
            strQuery.Append(" and AssetDetails.InvStatus  in (1,3,2,4) and AssetDetails.Disposed= 0 and AssetDetails.IsDeleted = 0 ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAssetABBList() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select AssetDetails.AstID as Barcode,Companies.CompanyCode,SUBSTRING ( RefNo , charindex('-',RefNo) +1 , charindex('-',RefNo,charindex('-',RefNo) + 2) - charindex('-',RefNo)-1 ) as AssetNumber,")
            strQuery.Append(" SUBSTRING ( RefNo , charindex('-',RefNo,charindex('-',RefNo) + 2) +1,len(RefNo)-charindex('-',RefNo,charindex('-',RefNo) + 2)) as SubNumber")
            strQuery.Append(" ,SerailNo as SerialNo,Location.LocationFullPath,CatFullPath, AssetDetails.disposed as Retired ,InStockAsset as InventoryAsset")
            strQuery.Append(" from Assets inner join(AssetDetails left outer join Location on AssetDetails.LocID = Location.LocID  )")
            strQuery.Append(" on Assets.ItemCode = AssetDetails.ItemCode inner join Category on Assets.AstCatID = Category.AstCatID")
            strQuery.Append(" inner join Companies on Companies.CompanyId = AssetDetails.CompanyId")
            strQuery.Append(" where AssetDetails.IsDeleted = 0")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function GetAssetsAdministartionGridABB(ByVal objattAssetDetails As attAssetDetails) As IDbCommand

            Dim strQuery As New StringBuilder
            Dim objCommand As OleDbCommand = New OleDbCommand
            strQuery.Append(" select  AssetDetails.BarCode,SUBSTRING ( RefNo , charindex('-',RefNo) +1 , charindex('-',RefNo,charindex('-',RefNo) + 2) - charindex('-',RefNo)-1 ) as AssetNumber")
            strQuery.Append(" ,SUBSTRING ( RefNo , charindex('-',RefNo,charindex('-',RefNo) + 2) +1,len(RefNo)-charindex('-',RefNo,charindex('-',RefNo) + 2)) as SubNumber")
            strQuery.Append(" ,Companies.CompanyCode,Category.CatFullPath,Assetdetails.AstDesc as Assetdetailsdesc1, Assetdetails.AstDesc2  as Assetdetailsdesc2")
            strQuery.Append(" ,InventoryNumber as 'Inv. Prop. No', CustomFld1 as 'Old Asset No', CustomFld2 as 'Old Barcode',AssetDetails.Purdate as 'Creation Date',CreatedBY,AssetDetails.SerailNo as 'Serial Number',CapitalizationDate,BussinessArea,CostCenter.CostNumber,'' as Plant, '' as Location ,Location.LocationFullPath")
            strQuery.Append(" ,EvaluationGroup1,EvaluationGroup2,EvaluationGroup3,EvaluationGroup4,DispDate as 'Retire Date',Custodian.CustodianName")
            strQuery.Append(" ,InStockAsset as 'Asset Val',LastInventoryDate,AstBooks.SalvageYear as UsefulLife,AssetDetails.LabelCount ")
            strQuery.Append(" ,InvStatus,AstNum,Cast (Assets.ItemCode as bigint) as itemcode,CostCenterID,CostCenter.CostName,Assets.AstDesc,AssetDetails.AstID,Location.LocDesc,AssetDetails.LocID,Assets.AstCatID,AssetDetails.CustodianID,Companies.CompanyName,AssetDetails.Refno,AssetDetails.CompanyID,AssetDetails.AstModel,Category.AstCatDesc,AssetDetails.TransRemarks,AssetDetails.BaseCost,AssetDetails.Tax,AssetDetails.SrvDate,AssetDetails.InsID,AssetDetails.InvNumber,AssetDetails.POCode,AssetDetails.SuppID,AssetDetails.Disposed,AssetDetails.Discount,RefCode,Plate,Poerp,Capex,Grn,AssetDetails.NoPiece,AssetDetails.GLCode,AssetDetails.PONumber")
            strQuery.Append(" from Assets")
            strQuery.Append("       inner join(AssetDetails ")
            strQuery.Append("           left outer join Location on AssetDetails.LocID = Location.LocID ")
            strQuery.Append("        left outer join Companies on AssetDetails.CompanyID = Companies.CompanyID ")
            strQuery.Append("               left outer join AstBooks on AssetDetails.AstID = AstBooks.AstID ")
            strQuery.Append("           left outer join Custodian on AssetDetails.CustodianID = custodian.CustodianID ")
            strQuery.Append("        left outer join CostCenter on AssetDetails.CostCenterID = CostCenter.CostID )")
            strQuery.Append("           on Assets.ItemCode = AssetDetails.ItemCode ")
            strQuery.Append(" left outer join Category on Assets.AstCatID = Category.AstCatID ")

            strQuery.Append("  where AssetDetails.IsDeleted = 0 and Disposed = 0")

            If Not String.IsNullOrEmpty(AppConfig.CompanyIDS) And AppConfig.CompanyIDS <> "0" Then
                strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If
            strQuery.Append("  order by AstNum")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAssetsLogABB(ByVal AssetID As String) As IDbCommand

            Dim strQuery As New StringBuilder
            Dim objCommand As OleDbCommand = New OleDbCommand
            strQuery.Append(" select    AstNum,Cast (Assets.ItemCode as bigint) as itemcode,Assets.AstDesc,AssetDetailsLog.AstID,Location.LocDesc,Custodian.CustodianName,AssetDetailsLog.LocID,Assets.AstCatID,AssetDetailsLog.CustodianID,Companies.CompanyName,AssetDetailsLog.BarCode,AssetDetailsLog.Refno,AssetDetailsLog.CompanyID,AssetDetailsLog.Purdate,AssetDetailsLog.AstModel,Category.AstCatDesc,AssetDetailsLog.TransRemarks,AssetDetailsLog.BaseCost,AssetDetailsLog.Tax,AssetDetailsLog.SrvDate,AssetDetailsLog.InsID,AssetDetailsLog.InvNumber,AssetDetailsLog.POCode,AssetDetailsLog.SuppID,AssetDetailsLog.Disposed,AssetDetailsLog.Discount,AssetDetailsLog.Barcode,AssetDetailsLog.SerailNo,RefCode,Plate,Poerp,Capex,Grn,AssetDetailsLog.NoPiece,AssetDetailsLog.GLCode,AssetDetailsLog.PONumber,AssetDetailsLog.LabelCount ,AssetDetailsLog.AstDesc as AssetDetailsdesc1, AssetDetailsLog.AstDesc2  as AssetDetailsdesc2,Category.CatFullPath,Location.LocationFullPath,CapitalizationDate,BussinessArea,InventoryNumber,CostCenterID,CreatedBY,CostCenter.CostNumber,CostCenter.CostName,InStockAsset,EvaluationGroup1,EvaluationGroup2,EvaluationGroup3,EvaluationGroup4,LastInventoryDate,InvStatus,AstBooks.SalvageYear as UsefulLife ")
            strQuery.Append(" ,SUBSTRING ( RefNo , charindex('-',RefNo) +1 , charindex('-',RefNo,charindex('-',RefNo) + 2) - charindex('-',RefNo)-1 ) as AssetNumber,")
            strQuery.Append(" SUBSTRING ( RefNo , charindex('-',RefNo,charindex('-',RefNo) + 2) +1,len(RefNo)-charindex('-',RefNo,charindex('-',RefNo) + 2)) as SubNumber,ActionType ,convert(varchar,ActionDate,100) as ActionDate,Companies.CompanyCode,AssetDetailsLog.LastEditBY")
            strQuery.Append(" from Assets")
            strQuery.Append("       inner join(AssetDetailsLog ")
            strQuery.Append("           left outer join Location on AssetDetailsLog.LocID = Location.LocID ")
            strQuery.Append("        left outer join Companies on AssetDetailsLog.CompanyID = Companies.CompanyID ")
            strQuery.Append("               left outer join AstBooks on AssetDetailsLog.AstID = AstBooks.AstID ")
            strQuery.Append("           left outer join Custodian on AssetDetailsLog.CustodianID = custodian.CustodianID ")
            strQuery.Append("        left outer join CostCenter on AssetDetailsLog.CostCenterID = CostCenter.CostID )")
            strQuery.Append("           on Assets.ItemCode = AssetDetailsLog.ItemCode ")
            strQuery.Append(" left outer join Category on Assets.AstCatID = Category.AstCatID ")

            If AssetID <> "" Then
                strQuery.Append(" where AssetDetailsLog.AstID = '" & AssetID & "'")
            End If
            strQuery.Append("  order by ActionDate desc")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAssetsLog1ABB(ByVal AssetID As String) As IDbCommand

            Dim strQuery As New StringBuilder
            Dim objCommand As OleDbCommand = New OleDbCommand
            strQuery.Append(" select logsummary.ID,SUBSTRING ( RefNo , 0 , charindex('-',RefNo) ) as CompanyCode  ")
            strQuery.Append(" ,SUBSTRING ( RefNo , charindex('-',RefNo) +1 , charindex('-',RefNo,charindex('-',RefNo) + 2) - charindex('-',RefNo)-1 ) as AssetNumber , ")
            strQuery.Append(" SUBSTRING ( RefNo , charindex('-',RefNo,charindex('-',RefNo) + 2) +1,len(RefNo)-charindex('-',RefNo,charindex('-',RefNo) + 2)) as SubNumber, ")
            strQuery.Append(" logdetail.columnName as Field, logdetail.OldValue,logdetail.NewValue,[AppUsers].LoginName + '-' + [AppUsers].UserName  as 'User ID/ Name',LastModifiedDate, ")
            strQuery.Append(" case ChangeType when 'U' Then 'Update' When 'I' then 'Insert' When 'D' then 'Delete' end as ChangeType  ")
            strQuery.Append(" from AssetDetailsChangeLogSummary logsummary inner join AssetDetailsChangeLogDetail logdetail on logsummary.SummaryID = logdetail.SummaryID ")
            strQuery.Append(" inner join [dbo].[AssetDetails] on [AssetDetails].ASTID = logsummary.ID ")
            strQuery.Append(" inner join [dbo].[AppUsers] on [AppUsers].LoginName = ModifiedByUser ")
            strQuery.Append(" where logsummary.ID = '" & AssetID & "'")
            strQuery.Append(" order by logsummary.SummaryID desc,logdetail.DetailID ")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function GetAssetLifeByRefNo(ByVal refNo As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select SalvageYear from AstBooks inner join AssetDetails on AssetDetails.AstID = AstBooks.AstID where RefNo='" & refNo & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function AssetsLogReport(ByVal Filter As FilterOptions) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select logsummary.ID,SUBSTRING ( RefNo , 0 , charindex('-',RefNo) ) as CompanyCode  ")
            strQuery.Append(" ,ISNULL(SUBSTRING ( RefNo , charindex('-',RefNo) +1 , charindex('-',RefNo,charindex('-',RefNo) + 2) - charindex('-',RefNo)-1 ),logsummary.ID) as AssetNumber , ")
            strQuery.Append(" SUBSTRING ( RefNo , charindex('-',RefNo,charindex('-',RefNo) + 2) +1,len(RefNo)-charindex('-',RefNo,charindex('-',RefNo) + 2)) as SubNumber, ")
            strQuery.Append(" logdetail.columnName as Field, logdetail.OldValue,logdetail.NewValue,[AppUsers].LoginName + '-' + [AppUsers].UserName  as 'User ID/ Name',ISNULL(ModifiedDate,LastModifiedDate) As LastModifiedDate, ")
            strQuery.Append(" case ChangeType when 'U' Then 'Update' When 'I' then 'Insert' When 'D' then 'Delete' end as ChangeType,OldLocation.LocationFullPath as OldLocationDesc, NewLocation.LocationFullPath as NewLocationDesc ")
            strQuery.Append(" from AssetDetailsChangeLogSummary logsummary ")
            strQuery.Append(" inner join [dbo].[AssetDetails] on [AssetDetails].ASTID = logsummary.ID ")
            strQuery.Append(" inner join [dbo].[Assets] on [AssetDetails].ItemCode = Assets.ItemCode ")
            strQuery.Append(" inner join AssetDetailsChangeLogDetail logdetail on logsummary.SummaryID = logdetail.SummaryID ")
            strQuery.Append(" Left outer join [dbo].[AppUsers] on [AppUsers].LoginName = ModifiedByUser ")
            strQuery.Append(" Left outer join [dbo].[Location] OldLocation on [OldLocation].LocID = OldValue ")
            strQuery.Append(" Left outer join [dbo].[Location] NewLocation on [NewLocation].LocID = NewValue ")



            strQuery.Append(" where 1 = 1 ")



            If Filter.FilterByCreation Then
                strQuery.Append(" and Purdate >= " & BackEndDate(Filter.CreationFromDate))
                strQuery.Append(" and Purdate <= " & BackEndDate(Filter.CreationToDate))
            End If

            If Filter.FilterByRetirement Then
                strQuery.Append(" and DispDate >= " & BackEndDate(Filter.RetirementstartDate))
                strQuery.Append(" and DispDate <= " & BackEndDate(Filter.RetirementEndDate))
            End If

            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If

            If Not String.IsNullOrEmpty(Filter.LocID) Then
                If Filter.IncludeSubLevels Then
                    strQuery.Append(" and (AssetDetails.LocId like '" & Trim(Filter.LocID) & "' or AssetDetails.LocId like '" & Trim(Filter.LocID) & "-%' )")
                Else
                    strQuery.Append(" and AssetDetails.LocId like '" & Trim(Filter.LocID) & "'")
                End If
            End If


            If Not String.IsNullOrEmpty(Filter.AssetClassID) Then
                If Filter.IncludeSubLevels Then
                    strQuery.Append(" and (Assets.AstCatID like '" & Filter.AssetClassID & "' or Assets.AstCatID like '" & Filter.AssetClassID.ToString() & "-%')")
                Else
                    strQuery.Append(" and Assets.AstCatID  like '" & Filter.AssetClassID & "'")

                End If
            End If

            If Not String.IsNullOrEmpty(Filter.CustodianID) Then
                strQuery.Append(" and AssetDetails.CustodianID = '" & Filter.CustodianID & "'")
            End If

            If Not String.IsNullOrEmpty(Filter.CostCenterID) Then
                strQuery.Append(" and AssetDetails.CostCenterID = '" & Filter.CostCenterID & "'")
            End If

            If Filter.CompanyID <> 0 Then
                strQuery.Append(" and AssetDetails.CompanyID = " & Filter.CompanyID & "")
            End If
            '''''''

            If Not String.IsNullOrEmpty(Filter.InventoryNumber) Then
                strQuery.Append(" and AssetDetails.InventoryNumber = '" & Filter.InventoryNumber & "'")
            End If
            If Not String.IsNullOrEmpty(Filter.BussinessArea) Then
                strQuery.Append(" and AssetDetails.BussinessArea = '" & Filter.BussinessArea & "'")
            End If
            If Not String.IsNullOrEmpty(Filter.EvaluationGroup1) Then
                strQuery.Append(" and AssetDetails.EvaluationGroup1 = '" & Filter.EvaluationGroup1 & "'")
            End If
            If Not String.IsNullOrEmpty(Filter.EvaluationGroup2) Then
                strQuery.Append(" and AssetDetails.EvaluationGroup2 = '" & Filter.EvaluationGroup2 & "'")
            End If
            If Not String.IsNullOrEmpty(Filter.EvaluationGroup3) Then
                strQuery.Append(" and AssetDetails.EvaluationGroup3 = '" & Filter.EvaluationGroup3 & "'")
            End If
            If Not String.IsNullOrEmpty(Filter.EvaluationGroup4) Then
                strQuery.Append(" and AssetDetails.EvaluationGroup4 = '" & Filter.EvaluationGroup4 & "'")
            End If

            If Not String.IsNullOrEmpty(Filter.FromAssetNumber) And Not String.IsNullOrEmpty(Filter.ToAssetNumber) Then
                strQuery.Append(String.Format(" and SUBSTRING ( RefNo , charindex('-',RefNo) +1 , charindex('-',RefNo,charindex('-',RefNo) + 2) - charindex('-',RefNo)-1 ) between '{0}' and '{1}'", Filter.FromAssetNumber, Filter.ToAssetNumber))
            ElseIf Not String.IsNullOrEmpty(Filter.FromAssetNumber) And String.IsNullOrEmpty(Filter.ToAssetNumber) Then
                strQuery.Append(String.Format(" and SUBSTRING ( RefNo , charindex('-',RefNo) +1 , charindex('-',RefNo,charindex('-',RefNo) + 2) - charindex('-',RefNo)-1 ) = '{0}'", Filter.FromAssetNumber))
            ElseIf String.IsNullOrEmpty(Filter.FromAssetNumber) And Not String.IsNullOrEmpty(Filter.ToAssetNumber) Then
                strQuery.Append(String.Format(" and SUBSTRING ( RefNo , charindex('-',RefNo) +1 , charindex('-',RefNo,charindex('-',RefNo) + 2) - charindex('-',RefNo)-1 ) = '{0}'", Filter.ToAssetNumber))
            End If

            If Filter.ExcludeCapitalizedAssets Then
                strQuery.Append(" and CapitalizationDate is null ")
            ElseIf Filter.ShowOnlyCapitalizedAssets Then
                strQuery.Append(" and CapitalizationDate is not null ")
                If Filter.FilterByCapitalize Then
                    strQuery.Append(" and CapitalizationDate >= " & BackEndDate(Filter.CapitalizestartDate))
                    strQuery.Append(" and CapitalizationDate <= " & BackEndDate(Filter.CapitalizeEndDate))
                End If
            Else
                If Filter.FilterByCapitalize Then
                    strQuery.Append(" and CapitalizationDate >= " & BackEndDate(Filter.CapitalizestartDate))
                    strQuery.Append(" and CapitalizationDate <= " & BackEndDate(Filter.CapitalizeEndDate))
                End If
            End If


            Dim isDisposed As Int16
            If Filter.ShowOnlyRetiredAssets Then
                isDisposed = 1
                strQuery.Append(" and AssetDetails.Disposed = " & isDisposed & "")
            ElseIf Filter.ExcludeRetiredAssets Then
                isDisposed = 0
                strQuery.Append(" and AssetDetails.Disposed = " & isDisposed & "")
            End If

            Dim isWithValue As Int16
            If Filter.ShowOnlyAssetValue Then
                isWithValue = 0
                strQuery.Append(" and AssetDetails.InStockAsset = " & isWithValue & "")
            ElseIf Filter.ExcludeAssetValue Then
                isWithValue = 1
                strQuery.Append(" and AssetDetails.InStockAsset = " & isWithValue & "")
            End If


            If Filter.DataSource <> "" Then
                strQuery.Append(" and AssetDetails.TransRemarks = '" & Filter.DataSource & "'")
            End If
            strQuery.Append(" order by logsummary.SummaryID desc,logdetail.DetailID ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function CompanyAssetReportABB(ByVal Filter As FilterOptions) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            strQuery.Append(" Select  AssetDetails.AstID as barcode,SUBSTRING ( RefNo , charindex('-',RefNo) +1 , charindex('-',RefNo,charindex('-',RefNo) + 2) - charindex('-',RefNo)-1 ) as AssetNumber,")
            strQuery.Append(" SUBSTRING ( RefNo , charindex('-',RefNo,charindex('-',RefNo) + 2) +1,len(RefNo)-charindex('-',RefNo,charindex('-',RefNo) + 2)) as SubNumber ,")
            strQuery.Append(" Companies.CompanyCode,Category.CatFullPath,Assetdetails.AstDesc as Assetdetailsdesc1, Assetdetails.AstDesc2  as Assetdetailsdesc2,")
            strQuery.Append(" InventoryNumber,AssetDetails.Purdate as CreationDate,CreatedBY, AssetDetails.SerailNo,CapitalizationDate,BussinessArea,")
            strQuery.Append(" CostCenter.CostNumber,Location.LocationFullPath ,Custodian.CustodianID,EvaluationGroup1,")
            strQuery.Append(" EvaluationGroup2,EvaluationGroup3,EvaluationGroup4,AstBooks.SalvageYear,AssetDetails.LastEditBY,AssetDetails.LastEditDate,case when InStockAsset=1 then 'No' else 'Yes' end as AssetWithValue")
            If Filter.InvSchID = -1 Then
                strQuery.Append(",InvStatus,LastInventoryDate as HisDate,")
            Else
                strQuery.Append(",Ast_History.Status,Ast_History.HisDate,ToLocation.LocationFullPath as ToFullLocation,FromLocation.LocationFullPath as FromFullLocation,Ast_History.Remarks,")
            End If
            strQuery.Append("DispDate as RetirementDate,AuditStatus.StatusDesc,(select top 1 Fromcustodian from Ast_Cust_history where Ast_Cust_history.AstID = AssetDetails.AstID order by historyid desc) as OldCust ")
            strQuery.Append(" from AssetDetails inner join Assets on Assets.ItemCode = AssetDetails.ItemCode ")
            strQuery.Append(" left outer join Location  on AssetDetails.LocID = Location.LocID ")
            strQuery.Append(" left outer join Companies on AssetDetails.CompanyID = Companies.CompanyID ")
            strQuery.Append(" left outer join Custodian on AssetDetails.CustodianID = custodian.CustodianID  ")
            strQuery.Append(" left outer join CostCenter on AssetDetails.CostCenterID = CostCenter.CostID")
            strQuery.Append(" left outer join Category  on Assets.AstCatID = Category.AstCatID   ")
            strQuery.Append(" left outer join AstBooks  on AstBooks.AstID = AssetDetails.AstID   ")

            If Filter.InvSchID <> -1 Then
                strQuery.Append(" left outer join Ast_History  on Ast_History.AstID = AssetDetails.AstID ")
                strQuery.Append(" left outer join AuditStatus  on AuditStatus.StatusID = Ast_History.Status   ")
                strQuery.Append(" left outer join Location FromLocation on Ast_History.Fr_Loc = FromLocation.LocID ")
                strQuery.Append(" left outer join Location ToLocation on Ast_History.To_Loc = ToLocation.LocID ")
            Else
                strQuery.Append(" left outer join AuditStatus  on AuditStatus.StatusID = AssetDetails.InvStatus   ")
            End If

            strQuery.Append(" where AssetDetails.IsDeleted = 0 ")

            If Filter.InvSchID = -1 Then 'Schedule not selected
                If Not String.IsNullOrEmpty(Filter.Status) Then
                    strQuery.Append(String.Format(" and InvStatus in ({0})", Filter.Status))
                End If

                'strQuery.Append(" and Ast_History.HisDate = (select max(HisDate) from dbo.Ast_History where AstID = AssetDetails.AstID   )")
            Else
                If Not String.IsNullOrEmpty(Filter.Status) Then
                    strQuery.Append(String.Format(" and Ast_History.Status in ({0})", Filter.Status))
                End If
                strQuery.Append(" and Ast_History.InvSchCode = " + Filter.InvSchID.ToString)
            End If


            If Filter.FilterByCreation Then
                strQuery.Append(" and Purdate >= " & BackEndDate(Filter.CreationFromDate))
                strQuery.Append(" and Purdate <= " & BackEndDate(Filter.CreationToDate))
            End If

            If Filter.FilterByRetirement Then
                strQuery.Append(" and DispDate >= " & BackEndDate(Filter.RetirementstartDate))
                strQuery.Append(" and DispDate <= " & BackEndDate(Filter.RetirementEndDate))
            End If

            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If

            If Not String.IsNullOrEmpty(Filter.LocID) Then
                If Filter.IncludeSubLevels Then
                    strQuery.Append(" and (AssetDetails.LocId like '" & Trim(Filter.LocID) & "' or AssetDetails.LocId like '" & Trim(Filter.LocID) & "-%' )")
                Else
                    strQuery.Append(" and AssetDetails.LocId like '" & Trim(Filter.LocID) & "'")
                End If
            End If


            If Not String.IsNullOrEmpty(Filter.AssetClassID) Then
                If Filter.IncludeSubLevels Then
                    strQuery.Append(" and (Assets.AstCatID like '" & Filter.AssetClassID & "' or Assets.AstCatID like '" & Filter.AssetClassID.ToString() & "-%')")
                Else
                    strQuery.Append(" and Assets.AstCatID  like '" & Filter.AssetClassID & "'")

                End If
            End If

            If Not String.IsNullOrEmpty(Filter.CustodianID) Then
                strQuery.Append(" and AssetDetails.CustodianID = '" & Filter.CustodianID & "'")
            End If

            If Not String.IsNullOrEmpty(Filter.CostCenterID) Then
                strQuery.Append(" and AssetDetails.CostCenterID = '" & Filter.CostCenterID & "'")
            End If

            If Filter.CompanyID <> 0 Then
                strQuery.Append(" and AssetDetails.CompanyID = " & Filter.CompanyID & "")
            End If
            '''''''

            If Not String.IsNullOrEmpty(Filter.InventoryNumber) Then
                strQuery.Append(" and AssetDetails.InventoryNumber = '" & Filter.InventoryNumber & "'")
            End If
            If Not String.IsNullOrEmpty(Filter.BussinessArea) Then
                strQuery.Append(" and AssetDetails.BussinessArea = '" & Filter.BussinessArea & "'")
            End If
            If Not String.IsNullOrEmpty(Filter.EvaluationGroup1) Then
                strQuery.Append(" and AssetDetails.EvaluationGroup1 = '" & Filter.EvaluationGroup1 & "'")
            End If
            If Not String.IsNullOrEmpty(Filter.EvaluationGroup2) Then
                strQuery.Append(" and AssetDetails.EvaluationGroup2 = '" & Filter.EvaluationGroup2 & "'")
            End If
            If Not String.IsNullOrEmpty(Filter.EvaluationGroup3) Then
                strQuery.Append(" and AssetDetails.EvaluationGroup3 = '" & Filter.EvaluationGroup3 & "'")
            End If
            If Not String.IsNullOrEmpty(Filter.EvaluationGroup4) Then
                strQuery.Append(" and AssetDetails.EvaluationGroup4 = '" & Filter.EvaluationGroup4 & "'")
            End If

            If Not String.IsNullOrEmpty(Filter.FromAssetNumber) And Not String.IsNullOrEmpty(Filter.ToAssetNumber) Then
                strQuery.Append(String.Format(" and SUBSTRING ( RefNo , charindex('-',RefNo) +1 , charindex('-',RefNo,charindex('-',RefNo) + 2) - charindex('-',RefNo)-1 ) between '{0}' and '{1}'", Filter.FromAssetNumber, Filter.ToAssetNumber))
            ElseIf Not String.IsNullOrEmpty(Filter.FromAssetNumber) And String.IsNullOrEmpty(Filter.ToAssetNumber) Then
                strQuery.Append(String.Format(" and SUBSTRING ( RefNo , charindex('-',RefNo) +1 , charindex('-',RefNo,charindex('-',RefNo) + 2) - charindex('-',RefNo)-1 ) = '{0}'", Filter.FromAssetNumber))
            ElseIf String.IsNullOrEmpty(Filter.FromAssetNumber) And Not String.IsNullOrEmpty(Filter.ToAssetNumber) Then
                strQuery.Append(String.Format(" and SUBSTRING ( RefNo , charindex('-',RefNo) +1 , charindex('-',RefNo,charindex('-',RefNo) + 2) - charindex('-',RefNo)-1 ) = '{0}'", Filter.ToAssetNumber))
            End If

            If Filter.ExcludeCapitalizedAssets Then
                strQuery.Append(" and CapitalizationDate is null ")
            ElseIf Filter.ShowOnlyCapitalizedAssets Then
                strQuery.Append(" and CapitalizationDate is not null ")
                If Filter.FilterByCapitalize Then
                    strQuery.Append(" and CapitalizationDate >= " & BackEndDate(Filter.CapitalizestartDate))
                    strQuery.Append(" and CapitalizationDate <= " & BackEndDate(Filter.CapitalizeEndDate))
                End If
            Else
                If Filter.FilterByCapitalize Then
                    strQuery.Append(" and CapitalizationDate >= " & BackEndDate(Filter.CapitalizestartDate))
                    strQuery.Append(" and CapitalizationDate <= " & BackEndDate(Filter.CapitalizeEndDate))
                End If
            End If


            Dim isDisposed As Int16
            If Filter.ShowOnlyRetiredAssets Then
                isDisposed = 1
                strQuery.Append(" and AssetDetails.Disposed = " & isDisposed & "")
            ElseIf Filter.ExcludeRetiredAssets Then
                isDisposed = 0
                strQuery.Append(" and AssetDetails.Disposed = " & isDisposed & "")
            End If

            Dim isWithValue As Int16
            If Filter.ShowOnlyAssetValue Then
                isWithValue = 0
                strQuery.Append(" and AssetDetails.InStockAsset = " & isWithValue & "")
            ElseIf Filter.ExcludeAssetValue Then
                isWithValue = 1
                strQuery.Append(" and AssetDetails.InStockAsset = " & isWithValue & "")
            End If


            If Filter.DataSource <> "" Then
                strQuery.Append(" and AssetDetails.TransRemarks = '" & Filter.DataSource & "'")
            End If

            strQuery.Append(" order by  AssetDetails.AstID ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData_Anonymous() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append(" select ")
            strQuery.Append(" NonBCode,DeviceID,LocID,AstCatID,AstDesc as Description,Serial as SerailNo,TransDate as HisDate,RefNo,Remarks,CompanyCode")
            strQuery.Append(" from NonBarCodedTemp where InvSchID is null or InvSchID < 0 ")
            objCommand.CommandText = strQuery.ToString()
            Return objCommand
        End Function

        Public Function GetAllData_AnonymousRemarks(ByVal InvSchID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append(" select ")
            strQuery.Append(" NonBCode,DeviceID,LocID,AstCatID,AstDesc as Description,Serial as SerailNo,TransDate as HisDate,RefNo,Remarks,CompanyCode")
            strQuery.Append(" from NonBarCodedTemp where InvSchID is not null")
            If Not String.IsNullOrEmpty(InvSchID) Then
                strQuery.Append(" and InvSchID = " & InvSchID)
            End If

            objCommand.CommandText = strQuery.ToString()
            Return objCommand
        End Function

        Public Function GetAll_DataProcessing() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select AssetsTempReceiving.AstID,AstDesc,AssetsTempReceiving.DeviceID,LocID,ToLoc,FromLoc,AstCatID,Status,SerailNo,CustodianID,InventoryDate,IsDataChanged,RefNo,CustodianID as Employee,CostCenter,InventoryNumber,AstTransID,LastEditBY from AssetsTempReceiving")
            strQuery.Append(" left outer join AssetTransferTemp on AssetTransferTemp.AstID = AssetsTempReceiving.AstID")
            objCommand.CommandText = strQuery.ToString()
            Return objCommand
        End Function

        Public Function GetAllClasses() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select AstCatID,AstCatDesc,Code,CompCode,catLevel from Category where IsDeleted = 0 ")
            strQuery.Append(" order by AstCatDesc")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


      
    End Class
End Namespace