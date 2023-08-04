Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Namespace ZulAssetsDAL
    Public Class Assetdetails
        Implements IEntity

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
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattAssetDetails
            End Get
            Set(ByVal Value As IAttribute)
                objattAssetDetails = CType(Value, attAssetDetails)
            End Set
        End Property

        Public Property ObjCommand() As IDbCommand Implements IEntity.ObjCommand
            Get
                Return _Command
            End Get
            Set(ByVal Value As IDbCommand)
                _Command = CType(Value, IDbCommand)
            End Set
        End Property
#End Region

#Region "Methods"

        Public Function DeleteImage() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetDetails set AstImage = null where astid = '" & objattAssetDetails.PKeyCode & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function UpdateImage() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            strQuery.Append("update AssetDetails set AstImage = ? where astid = '" & objattAssetDetails.PKeyCode & "'")
            objCommand.Parameters.Add(New OleDbParameter("@ImageData", CType(objattAssetDetails.Image, Object)))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function CopyImage(ByVal NewAstID As String, ByVal OldImage As Byte()) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            'To be Compatible with Oracle we have to get the image first. because Oracle Longraw datatype is not allowing update from.
            If OldImage IsNot Nothing Then
                strQuery.Append("update AssetDetails set AstImage = ? where astID = '" & NewAstID & "'")
                objCommand.Parameters.Add(New OleDbParameter("@ImageData", CType(OldImage, Object)))
            Else
                strQuery.Append("update AssetDetails set AstImage = null where astID = '" & NewAstID & "'")
            End If

            'If AppConfig.DbType = "1" Then
            '    strQuery.Append("update AssetDetails set AssetDetails.AstImage = AssetDetailsOld.AstImage from AssetDetails as AssetDetailsOld ,AssetDetails ")
            '    strQuery.Append(" where AssetDetailsOld.astID = '" & oldAstID & "' and AssetDetails.astid ='" & NewAstID & "'")
            'End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetImage() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            strQuery.Append("select astImage from AssetDetails where astid = '" & objattAssetDetails.PKeyCode & "'")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllImages() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            strQuery.Append("select astid,astImage from AssetDetails where astimage is not null")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function ClearAllImages() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            strQuery.Append("update assetDetails set astimage = null")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Insert() As IDbCommand Implements IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            Dim isSold, isDisposed As Int32
            If objattAssetDetails.IsSold Then
                isSold = 1
            Else
                isSold = 0
            End If
            If objattAssetDetails.Disposed Then
                isDisposed = 1
            Else
                isDisposed = 0
            End If
            strQuery.Append("insert into AssetDetails")
            strQuery.Append("(AstID,DispCode,Discount,ItemCode,SuppID,POCode,InvNumber,CustodianID,BaseCost,Tax,PurDate,SrvDate,Disposed,IsSold,DispDate,InvSchCode,BookID,InsID,LocID,InvStatus,Sel_Date,Sel_Price,SoldTo,Isdeleted,refNo,AstNum,AstBrandID,AstDesc,AstDesc2,AstModel,CompanyID,  TransRemarks,LabelCount,NoPiece,BarCode,SerailNo,RefCode,Plate,Poerp,Capex,Grn,OldAssetID,GLCode,PONumber,CapitalizationDate,BussinessArea,InventoryNumber,CostCenterID,InStockAsset,EvaluationGroup1,EvaluationGroup2,EvaluationGroup3,EvaluationGroup4,CreatedBY,IsDataChanged,LastInventoryDate,LastEditDate,CreationDate,LastEditBY,CustomFld1,CustomFld2,CustomFld3,CustomFld4,CustomFld5,AssetDetails.Warranty,StatusID,DisposalComments)")
            strQuery.Append(" Values")

            strQuery.Append(" ('" & Convert.ToString(objattAssetDetails.PKeyCode) & "'," & objattAssetDetails.DispCode & "," & objattAssetDetails.Discount & ",'" & objattAssetDetails.ItemCode & "','" & objattAssetDetails.SuppID & "','" & objattAssetDetails.POCode & "','" & objattAssetDetails.InvNumber & "','" & objattAssetDetails.CustodianID & "'," & objattAssetDetails.BaseCost & "," & objattAssetDetails.Tax & "," & BackEndDate(objattAssetDetails.PurDate) & "," & BackEndDate(objattAssetDetails.SrvDate) & " ," & isDisposed & ", " & isSold & ",?," & objattAssetDetails.InvSchCode & ",'" & objattAssetDetails.BookID & "'," & objattAssetDetails.InsID & ",'" & objattAssetDetails.LocID & "'," & objattAssetDetails.InvStatus & ",?," & objattAssetDetails.Sel_Price & ",'" & objattAssetDetails.SoldTo & "',0,'" & objattAssetDetails.RefNo & "'," & objattAssetDetails.AstNum & "," & objattAssetDetails.AstBrandID & ",?,?,?," & objattAssetDetails.CompanyID & ",'" & objattAssetDetails.TransRemarks & "',0," & objattAssetDetails.NoPiece & ",'" & objattAssetDetails.BarCode & "',?,'" & objattAssetDetails.RefCode & "','" & objattAssetDetails.Plate & "','" & objattAssetDetails.PoErp & "','" & objattAssetDetails.Capex & "','" & objattAssetDetails.GRN & "','" & objattAssetDetails.OldAssetID & "'," & objattAssetDetails.GLCode & ",'" & objattAssetDetails.PONumber & "',?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?) ")
            If objattAssetDetails.DispDate <> Nothing Then
                objCommand.Parameters.Add(New OleDbParameter("@DispDate", BackendDateTime(objattAssetDetails.DispDate)))
            Else
                objCommand.Parameters.Add(New OleDbParameter("@DispDate", DBNull.Value))
            End If
            If objattAssetDetails.Sel_Date <> Nothing Then
                objCommand.Parameters.Add(New OleDbParameter("@Sel_Date", BackendDateTime(objattAssetDetails.Sel_Date)))
            Else
                objCommand.Parameters.Add(New OleDbParameter("@Sel_Date", DBNull.Value))
            End If

            objCommand.Parameters.Add(New OleDbParameter("@AstDesc", objattAssetDetails.AstDesc))
            objCommand.Parameters.Add(New OleDbParameter("@AstDesc2", objattAssetDetails.AstDesc2))
            objCommand.Parameters.Add(New OleDbParameter("@AstModel", objattAssetDetails.AstModel))
            objCommand.Parameters.Add(New OleDbParameter("@SerailNo", objattAssetDetails.SerailNo))

            If objattAssetDetails.CapitalizationDate <> Nothing Then
                objCommand.Parameters.Add(New OleDbParameter("@CapitalizationDate", BackendDateTime(objattAssetDetails.CapitalizationDate)))
            Else
                objCommand.Parameters.Add(New OleDbParameter("@CapitalizationDate", DBNull.Value))
            End If
            objCommand.Parameters.Add(New OleDbParameter("@BussinessArea", objattAssetDetails.BussinessArea))
            objCommand.Parameters.Add(New OleDbParameter("@InventoryNumber", objattAssetDetails.InventoryNumber))
            objCommand.Parameters.Add(New OleDbParameter("@CostCenterID", objattAssetDetails.CostCenterID))
            objCommand.Parameters.Add(New OleDbParameter("@InStockAsset", objattAssetDetails.InStockAsset))
            objCommand.Parameters.Add(New OleDbParameter("@EvaluationGroup1", objattAssetDetails.EvaluationGroup1))
            objCommand.Parameters.Add(New OleDbParameter("@EvaluationGroup2", objattAssetDetails.EvaluationGroup2))
            objCommand.Parameters.Add(New OleDbParameter("@EvaluationGroup3", objattAssetDetails.EvaluationGroup3))
            objCommand.Parameters.Add(New OleDbParameter("@EvaluationGroup4", objattAssetDetails.EvaluationGroup4))
            objCommand.Parameters.Add(New OleDbParameter("@CreatedBY", objattAssetDetails.CreatedBY))
            objCommand.Parameters.Add(New OleDbParameter("@IsDataChanged", objattAssetDetails.IsDataChanged))
            If objattAssetDetails.LastInventoryDate <> Nothing Then
                objCommand.Parameters.Add(New OleDbParameter("@LastInventoryDate", BackendDateTime(objattAssetDetails.LastInventoryDate)))
            Else
                objCommand.Parameters.Add(New OleDbParameter("@LastInventoryDate", DBNull.Value))
            End If

            If objattAssetDetails.LastEditDate <> Nothing Then
                objCommand.Parameters.Add(New OleDbParameter("@LastEditDate", BackendDateTime(objattAssetDetails.LastEditDate)))
            Else
                objCommand.Parameters.Add(New OleDbParameter("@LastEditDate", DBNull.Value))
            End If

            objCommand.Parameters.Add(New OleDbParameter("@CreationDate", BackendDateTime(objattAssetDetails.CreationDate)))

            objCommand.Parameters.Add(New OleDbParameter("@LastEditBY", objattAssetDetails.LastEditBY))
            objCommand.Parameters.Add(New OleDbParameter("@CustomFld1", objattAssetDetails.CustomFld1))
            objCommand.Parameters.Add(New OleDbParameter("@CustomFld2", objattAssetDetails.CustomFld2))
            objCommand.Parameters.Add(New OleDbParameter("@CustomFld3", objattAssetDetails.CustomFld3))
            objCommand.Parameters.Add(New OleDbParameter("@CustomFld4", objattAssetDetails.CustomFld4))
            objCommand.Parameters.Add(New OleDbParameter("@CustomFld5", objattAssetDetails.CustomFld5))
            objCommand.Parameters.Add(New OleDbParameter("@Warranty", objattAssetDetails.Warranty))
            objCommand.Parameters.Add(New OleDbParameter("@StatusID", objattAssetDetails.StatusID))
            objCommand.Parameters.Add(New OleDbParameter("@DisposalComments", objattAssetDetails.DisposalComments))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function



        Public Function IsDisposed(ByVal AstID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select Count(*) from AssetDetails where astID = ? and Disposed = 1 and isdeleted = 0")
            objCommand.Parameters.Add(New OleDbParameter("@astID", AstID))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            Dim isSold, isDisposed As Int32
            If objattAssetDetails.IsSold Then
                isSold = 1
            Else
                isSold = 0
            End If
            If objattAssetDetails.Disposed Then
                isDisposed = 1
            Else
                isDisposed = 0
            End If
            strQuery.Append("update AssetDetails")
            strQuery.Append(" set")
            strQuery.Append(" DispCode=" & objattAssetDetails.DispCode & ",")
            strQuery.Append(" ItemCode='" & objattAssetDetails.ItemCode & "',")
            strQuery.Append(" SuppID='" & objattAssetDetails.SuppID & "',")
            strQuery.Append(" POCode='" & objattAssetDetails.POCode & "',")
            strQuery.Append(" InvNumber='" & objattAssetDetails.InvNumber & "',")
            strQuery.Append(" CustodianID='" & objattAssetDetails.CustodianID & "',")
            strQuery.Append(" BaseCost=" & objattAssetDetails.BaseCost & ",")
            strQuery.Append(" Tax=" & objattAssetDetails.Tax & ",")
            strQuery.Append(" SrvDate= " & BackEndDate(objattAssetDetails.SrvDate) & " ,")
            strQuery.Append(" PurDate= " & BackEndDate(objattAssetDetails.PurDate) & " ,")
            strQuery.Append(" Disposed=" & isDisposed & ",")
            strQuery.Append(" Discount= " & objattAssetDetails.Discount & ",")
            strQuery.Append(" NoPiece= " & objattAssetDetails.NoPiece & ",")
            strQuery.Append(" DispDate= ?,")
            strQuery.Append(" BookID='" & objattAssetDetails.BookID & "',")
            strQuery.Append(" InsID= " & objattAssetDetails.InsID & ",")
            strQuery.Append(" LocID='" & objattAssetDetails.LocID & "',")
            strQuery.Append(" IsSold =" & isSold & ",")
            strQuery.Append(" refNo='" & objattAssetDetails.RefNo & "',")
            strQuery.Append(" Sel_Date=?,")
            strQuery.Append(" CompanyID=" & objattAssetDetails.CompanyID & ",")
            strQuery.Append(" Sel_Price=" & objattAssetDetails.Sel_Price & ",")
            strQuery.Append(" SoldTo='" & objattAssetDetails.SoldTo & "',")
            strQuery.Append(" BarCode='" & objattAssetDetails.BarCode & "',")
            strQuery.Append(" RefCode='" & objattAssetDetails.RefCode & "',")
            strQuery.Append(" Plate='" & objattAssetDetails.Plate & "',")
            strQuery.Append(" Poerp='" & objattAssetDetails.PoErp & "',")
            strQuery.Append(" Capex='" & objattAssetDetails.Capex & "',")
            strQuery.Append(" Grn='" & objattAssetDetails.GRN & "',")
            strQuery.Append(" GLCode=" & objattAssetDetails.GLCode & ",")                   ''''''''''''''''''''''''''
            strQuery.Append(" PONumber='" & objattAssetDetails.PONumber & "',")                   ''''''''''''''''''''''''''
            strQuery.Append(" AstBrandID=" & objattAssetDetails.AstBrandID & ",")
            strQuery.Append(" AstDesc=?,")
            strQuery.Append(" AstDesc2=?,")
            strQuery.Append(" AstModel=?,")
            strQuery.Append(" SerailNo=?,")

            strQuery.Append(" CapitalizationDate=?,")
            strQuery.Append(" BussinessArea=?,")
            strQuery.Append(" InventoryNumber=?,")
            strQuery.Append(" CostCenterID=?,")
            strQuery.Append(" InStockAsset=?,")
            strQuery.Append(" EvaluationGroup1=?,")
            strQuery.Append(" EvaluationGroup2=?,")
            strQuery.Append(" EvaluationGroup3=?,")
            strQuery.Append(" EvaluationGroup4=?,")
            strQuery.Append(" IsDataChanged=?,")
            strQuery.Append(" LastEditDate=?,")
            strQuery.Append(" LastEditBY=?,")
            strQuery.Append(" CustomFld1=?,")
            strQuery.Append(" CustomFld2=?,")
            strQuery.Append(" CustomFld3=?,")
            strQuery.Append(" CustomFld4=?,")
            strQuery.Append(" CustomFld5=?,")
            strQuery.Append(" Warranty=?,")
            strQuery.Append(" StatusID=?,")
            strQuery.Append(" DisposalComments=?,")

            strQuery.Append(" TransRemarks='" & objattAssetDetails.TransRemarks & "'")
            strQuery.Append(" where AstID ='" & Convert.ToString(objattAssetDetails.PKeyCode) & "'")
            objCommand.CommandText = strQuery.ToString()

            If objattAssetDetails.DispDate <> Nothing Then
                objCommand.Parameters.Add(New OleDbParameter("@DispDate", objattAssetDetails.DispDate))
            Else
                objCommand.Parameters.Add(New OleDbParameter("@DispDate", DBNull.Value))
            End If
            If objattAssetDetails.Sel_Date <> Nothing Then
                objCommand.Parameters.Add(New OleDbParameter("@Sel_Date", objattAssetDetails.Sel_Date))
            Else
                objCommand.Parameters.Add(New OleDbParameter("@Sel_Date", DBNull.Value))
            End If

            objCommand.Parameters.Add(New OleDbParameter("@AstDesc", objattAssetDetails.AstDesc))
            objCommand.Parameters.Add(New OleDbParameter("@AstDesc2", objattAssetDetails.AstDesc2))
            objCommand.Parameters.Add(New OleDbParameter("@AstModel", objattAssetDetails.AstModel))
            objCommand.Parameters.Add(New OleDbParameter("@SerailNo", objattAssetDetails.SerailNo))

            If objattAssetDetails.CapitalizationDate <> Nothing Then
                objCommand.Parameters.Add(New OleDbParameter("@CapitalizationDate", objattAssetDetails.CapitalizationDate))
            Else
                objCommand.Parameters.Add(New OleDbParameter("@CapitalizationDate", DBNull.Value))
            End If
            objCommand.Parameters.Add(New OleDbParameter("@BussinessArea", objattAssetDetails.BussinessArea))
            objCommand.Parameters.Add(New OleDbParameter("@InventoryNumber", objattAssetDetails.InventoryNumber))
            objCommand.Parameters.Add(New OleDbParameter("@CostCenterID", objattAssetDetails.CostCenterID))
            objCommand.Parameters.Add(New OleDbParameter("@InStockAsset", objattAssetDetails.InStockAsset))
            objCommand.Parameters.Add(New OleDbParameter("@EvaluationGroup1", objattAssetDetails.EvaluationGroup1))
            objCommand.Parameters.Add(New OleDbParameter("@EvaluationGroup2", objattAssetDetails.EvaluationGroup2))
            objCommand.Parameters.Add(New OleDbParameter("@EvaluationGroup3", objattAssetDetails.EvaluationGroup3))
            objCommand.Parameters.Add(New OleDbParameter("@EvaluationGroup4", objattAssetDetails.EvaluationGroup4))
            objCommand.Parameters.Add(New OleDbParameter("@IsDataChanged", objattAssetDetails.IsDataChanged))
            If objattAssetDetails.LastEditDate <> Nothing Then
                objCommand.Parameters.Add(New OleDbParameter("@LastEditDate", objattAssetDetails.LastEditDate))
            Else
                objCommand.Parameters.Add(New OleDbParameter("@LastEditDate", DBNull.Value))
            End If
            objCommand.Parameters.Add(New OleDbParameter("@LastEditBY", objattAssetDetails.LastEditBY))
            objCommand.Parameters.Add(New OleDbParameter("@CustomFld1", objattAssetDetails.CustomFld1))
            objCommand.Parameters.Add(New OleDbParameter("@CustomFld2", objattAssetDetails.CustomFld2))
            objCommand.Parameters.Add(New OleDbParameter("@CustomFld3", objattAssetDetails.CustomFld3))
            objCommand.Parameters.Add(New OleDbParameter("@CustomFld4", objattAssetDetails.CustomFld4))
            objCommand.Parameters.Add(New OleDbParameter("@CustomFld5", objattAssetDetails.CustomFld5))
            objCommand.Parameters.Add(New OleDbParameter("@Warranty", objattAssetDetails.Warranty))
            objCommand.Parameters.Add(New OleDbParameter("@StatusID", objattAssetDetails.StatusID))
            objCommand.Parameters.Add(New OleDbParameter("@DisposalComments", objattAssetDetails.DisposalComments))

            _Command = objCommand
            Return objCommand
        End Function



        Public Function GetAssetDetailsCount() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select count(*) from Assetdetails where Assetdetails.IsDeleted = 0 ")
            If objattAssetDetails.PKeyCode <> "" Then
                strQuery.Append(" and AstID = '" & CStr(objattAssetDetails.PKeyCode) & "'")
            End If
            If objattAssetDetails.ItemCode <> "" Then
                strQuery.Append(" and  AssetDetails.ItemCode = '" & objattAssetDetails.ItemCode & "'")
            End If
            If objattAssetDetails.AstNum <> 0 Then
                strQuery.Append(" and AstNum = " & objattAssetDetails.AstNum & "")
            End If

            If objattAssetDetails.CompanyID <> 0 Then
                strQuery.Append(" and  AssetDetails.CompanyID = " & objattAssetDetails.CompanyID & "")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function



        Public Function Update_Barcode() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" update AssetDetails")
            strQuery.Append(" set")
            strQuery.Append(" BarCode='" & objattAssetDetails.BarCode & "'")
            strQuery.Append(" where AstID ='" & Convert.ToString(objattAssetDetails.PKeyCode) & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function



        Public Function Update_Custodian() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetDetails")
            strQuery.Append(" set")
            strQuery.Append(" CustodianID='" & objattAssetDetails.CustodianID & "'")
            strQuery.Append(" ,LastEditBY='" & objattAssetDetails.LastEditBY & "',LastEditDate = getdate()")
            strQuery.Append(" where AstID ='" & Convert.ToString(objattAssetDetails.PKeyCode) & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update_Location() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetDetails")
            strQuery.Append(" set")
            strQuery.Append(" LocID='" & objattAssetDetails.LocID & "'")
            strQuery.Append(" ,LastEditBY='" & objattAssetDetails.LastEditBY & "',LastEditDate = getdate()")
            strQuery.Append(" where AstID ='" & Convert.ToString(objattAssetDetails.PKeyCode) & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" Select ")
            strQuery.Append(" AssetDetails.AstID,AssetDetails.DispCode,AssetDetails.Discount,AssetDetails.ItemCode,AssetDetails.SuppID,AssetDetails.POCode,AssetDetails.InvNumber,AssetDetails.CustodianID,AssetDetails.BaseCost,AssetDetails.Tax,AssetDetails.SrvDate,AssetDetails.PurDate,AssetDetails.Disposed,AssetDetails.DispDate,AssetDetails.InvSchCode,AssetDetails.BookID,AssetDetails.InsID,AssetDetails.LocID,AssetDetails.InvStatus,AssetDetails.IsDeleted,AssetDetails.IsSold,AssetDetails.Sel_Date,AssetDetails.Sel_Price,AssetDetails.SoldTo,AssetDetails.RefNo,AssetDetails.AstNum,AssetDetails.AstBrandID,AssetDetails.AstDesc,AssetDetails.AstDesc2,AssetDetails.AstModel,AssetDetails.CompanyID,AssetDetails.TransRemarks,AssetDetails.LabelCount,AssetDetails.GLCode,AssetDetails.PONumber,Supplier.SuppID,Supplier.SuppName,Supplier.SuppCell,Supplier.SuppFax,Supplier.SuppPhone,Supplier.SuppEmail,Supplier.IsDeleted,Insurer.InsCode,Insurer.InsName,Insurer.IsDeleted, ")
            strQuery.Append(" Companies.CompanyID,Companies.CompanyCode,Companies.IsDeleted,Companies.CompanyName,AssetDetails.Sel_Date,AssetDetails.Sel_Price,AssetDetails.SoldTo,Disposal_Method.DispDesc,Custodian.CustodianName,Insurer.InsName,Assets.AstDesc,Brand.AstBrandName,Companies.CompanyName,Disposal_Method.DispDesc,Supplier.SuppName,AssetDetails.NoPiece,AssetDetails.BarCode,AssetDetails.SerailNo,RefCode,Plate,Poerp,Capex,Grn,Assets.AstCatID,AssetDetails.OldAssetID, CostCenter.CostID,CostCenter.CostNumber,CostCenter.CostName,AssetDetails.CapitalizationDate,AssetDetails.BussinessArea,AssetDetails.InventoryNumber,AssetDetails.InStockAsset,AssetDetails.EvaluationGroup1,AssetDetails.EvaluationGroup2,AssetDetails.EvaluationGroup3,AssetDetails.EvaluationGroup4,AssetDetails.CreatedBY,AssetDetails.LastEditBY,AssetDetails.CreationDate,AssetDetails.LastEditDate,AssetDetails.LastInventoryDate,AssetDetails.CustomFld1,AssetDetails.CustomFld2,AssetDetails.CustomFld3,AssetDetails.CustomFld4,AssetDetails.CustomFld5,AssetDetails.Warranty,AssetDetails.StatusID,AssetStatus.Status,AssetDetails.DisposalComments from AssetDetails ")
            strQuery.Append("  LEFT JOIN Supplier ON  AssetDetails.SuppID = Supplier.SuppId ")


            strQuery.Append("  left outer join Custodian on AssetDetails.CustodianID = custodian.CustodianID ")
            strQuery.Append("  left outer join Insurer on AssetDetails.InsID = Insurer.InsCode ")
            strQuery.Append("  LEFT JOIN Assets ON  AssetDetails.ItemCode = Assets.ItemCode ")
            strQuery.Append("  left outer join AssetStatus on AssetDetails.StatusID = AssetStatus.ID ")
            strQuery.Append("  left outer join Brand on AssetDetails.AstBrandID = Brand.AstBrandID ")
            strQuery.Append("  left outer join Companies on AssetDetails.CompanyID = Companies.CompanyID ")
            strQuery.Append("  left outer join CostCenter on AssetDetails.CostCenterID = CostCenter.CostID ")
            strQuery.Append("  LEFT JOIN Disposal_Method ON  AssetDetails.DispCode = Disposal_Method.DispCode  ")
            strQuery.Append("  where Assetdetails.IsDeleted = 0 ")
            If objattAssetDetails.PKeyCode <> "" Then
                strQuery.Append(" and AstID = '" & CStr(objattAssetDetails.PKeyCode) & "'")
            End If
            If objattAssetDetails.ItemCode <> "" Then
                strQuery.Append(" and  AssetDetails.ItemCode = '" & objattAssetDetails.ItemCode & "'")
            End If
            If objattAssetDetails.AstNum <> 0 Then
                strQuery.Append(" and AstNum = " & objattAssetDetails.AstNum & "")
            End If
            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If
            If objattAssetDetails.CompanyID <> 0 Then
                strQuery.Append(" and  AssetDetails.CompanyID = " & objattAssetDetails.CompanyID & "")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData_Combo() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand


            Dim strQuery As New StringBuilder
            strQuery.Append("Select AstID from AssetDetails where IsDeleted = 0")
            If objattAssetDetails.PKeyCode <> "" Then
                strQuery.Append(" and AstID = '" & objattAssetDetails.PKeyCode & "'")
            End If
            If objattAssetDetails.ItemCode <> "" Then
                strQuery.Append(" and ItemCode = '" & objattAssetDetails.ItemCode & "'")
            End If
            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If


            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetCompanyAssetsIDs(ByVal CompanyID As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand


            Dim strQuery As New StringBuilder
            strQuery.Append("Select AstID from AssetDetails where IsDeleted = 0")
            strQuery.Append(" and  AssetDetails.CompanyID = " & CompanyID.ToString & "")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAssetStatusDesc(ByVal AssetStatusID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select Status from AssetStatus")
            strQuery.Append(" where AssetStatus.ID = " & AssetStatusID.ToString & "")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function VerfiY_Range(ByVal Start As Long, ByVal EndRange As Long) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select AstID from AssetDetails where ")
            strQuery.Append(" (? <= AstNum and AstNum <=? )")

            objCommand.Parameters.Add(New OleDbParameter("@Start", Start))
            objCommand.Parameters.Add(New OleDbParameter("@EndRange", EndRange))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function GetAllData_Active() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand


            Dim strQuery As New StringBuilder
            strQuery.Append("Select AssetDetails.AstID,AssetDetails.DispCode,AssetDetails.Discount,AssetDetails.ItemCode,AssetDetails.SuppID,AssetDetails.POCode,AssetDetails.InvNumber,AssetDetails.CustodianID,AssetDetails.BaseCost,AssetDetails.Tax,AssetDetails.SrvDate,AssetDetails.PurDate,AssetDetails.Disposed,AssetDetails.DispDate,AssetDetails.InvSchCode,AssetDetails.BookID,AssetDetails.InsID,AssetDetails.LocID,AssetDetails.InvStatus,AssetDetails.IsDeleted,AssetDetails.IsSold,AssetDetails.Sel_Date,AssetDetails.Sel_Price,AssetDetails.SoldTo,AssetDetails.RefNo,AssetDetails.AstNum,AssetDetails.AstBrandID,AssetDetails.AstDesc,AssetDetails.AstDesc2,AssetDetails.AstModel,AssetDetails.CompanyID,AssetDetails.TransRemarks,AssetDetails.LabelCount,AssetDetails.GLCode,AssetDetails.PONumber,RefCode,Plate,Poerp,Capex,Grn from AssetDetails where IsDeleted = 0")
            strQuery.Append(" and IsSold =0") ' & objattAssetDetails.IsSold)
            strQuery.Append(" and Disposed =0") ' & objattAssetDetails.Disposed)
            If objattAssetDetails.PKeyCode <> "" Then
                strQuery.Append(" and AstID = '" & CStr(objattAssetDetails.PKeyCode) & "'")
            End If
            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If
            If objattAssetDetails.ItemCode <> "" Then
                strQuery.Append(" and ItemCode = '" & objattAssetDetails.ItemCode & "'")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function GetAllData_ActiveForDep() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select AstBooks.BookID,AstBooks.AstID,AstBooks.DepCode,AstBooks.SalvageValue,AstBooks.SalvageValuePercent,AstBooks.SalvageYear,AstBooks.LastBV,")
            strQuery.Append("AstBooks.CurrentBV,AstBooks.BVUpdate,AstBooks.SalvageMonth,AssetDetails.srvdate,AssetDetails.basecost,AssetDetails.tax ")
            strQuery.Append("from AstBooks inner join AssetDetails on AssetDetails.astID = AstBooks.astid ")
            strQuery.Append("where AssetDetails.IsDeleted = 0 ")
            strQuery.Append(" and AssetDetails.IsSold = 0")
            strQuery.Append(" and AssetDetails.Disposed = 0")
            If objattAssetDetails.BookID <> "" Then
                strQuery.Append(" and AstBooks.bookID = '" & CStr(objattAssetDetails.BookID) & "'")
            End If

            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAsset_Details() As IDbCommand

            Dim strQuery As New StringBuilder
            Dim objCommand As OleDbCommand = New OleDbCommand

            If AppConfig.DbType = "1" Then
                strQuery.Append(" select    AstNum,Cast (Assets.ItemCode as bigint) as itemcode,Assets.AstDesc,AssetDetails.AstID,Location.LocDesc,Custodian.CustodianName,AssetDetails.LocID,Assets.AstCatID,AssetDetails.CustodianID,Brand.AstBrandName,Companies.CompanyName,AssetDetails.BarCode,AssetDetails.Refno,AssetDetails.CompanyID,AssetDetails.Purdate,AssetDetails.AstModel,Insurer.InsName,Category.AstCatDesc,Supplier.SuppName, AssetDetails.TransRemarks,AssetDetails.BaseCost,AssetDetails.Tax,AssetDetails.SrvDate,AssetDetails.InsID,AssetDetails.InvNumber,AssetDetails.AstBrandID,AssetDetails.POCode,AssetDetails.SuppID,AssetDetails.Disposed,AssetDetails.Discount,AssetDetails.Barcode,AssetDetails.SerailNo,RefCode,Plate,Poerp,Capex,Grn,AssetDetails.NoPiece,AssetDetails.GLCode,AssetDetails.PONumber,AssetDetails.LabelCount ,Assetdetails.AstDesc as Assetdetailsdesc1, Assetdetails.AstDesc2  as Assetdetailsdesc2,Category.CatFullPath,Location.LocationFullPath,CapitalizationDate,BussinessArea,InventoryNumber,CostCenterID,InStockAsset,EvaluationGroup1,EvaluationGroup2,EvaluationGroup3,EvaluationGroup4,CreatedBY,CostCenter.CostNumber,CostCenter.CostName,AssetDetails.CustomFld1,AssetDetails.CustomFld2,AssetDetails.CustomFld3,AssetDetails.CustomFld4,AssetDetails.CustomFld5,AssetDetails.Warranty,AssetStatus.Status,AssetDetails.DisposalComments")
                strQuery.Append(" from Assets")
                strQuery.Append("       inner join(AssetDetails ")
                strQuery.Append("           left outer join Location on AssetDetails.LocID = Location.LocID ")
                strQuery.Append("        left outer join Companies on AssetDetails.CompanyID = Companies.CompanyID ")
                strQuery.Append("               left outer join Brand on AssetDetails.AstBrandID = Brand.AstBrandID ")
                strQuery.Append("               left outer join AssetStatus on AssetDetails.StatusID = AssetStatus.ID ")
                strQuery.Append("           left outer join Custodian on AssetDetails.CustodianID = custodian.CustodianID ")
                strQuery.Append("   left outer join Supplier on AssetDetails.SuppID = Supplier.SuppID	 ")
                strQuery.Append("        left outer join Insurer on AssetDetails.InsID = Insurer.InsCode ")
                strQuery.Append("        left outer join CostCenter on AssetDetails.CostCenterID = CostCenter.CostID )")
                strQuery.Append("           on Assets.ItemCode = AssetDetails.ItemCode ")
                strQuery.Append(" left outer join Category on Assets.AstCatID = Category.AstCatID ")

                strQuery.Append("  where AssetDetails.IsDeleted = 0")
                If objattAssetDetails.LocID.ToString() <> "" Then
                    strQuery.Append(" and AssetDetails.LocID ='" & objattAssetDetails.LocID & "'")
                End If
                If objattAssetDetails.PKeyCode <> "" Then
                    strQuery.Append(" and AstID = '" & objattAssetDetails.PKeyCode & "'")
                End If
                If objattAssetDetails.AstNum <> 0 Then
                    strQuery.Append(" and AstNum = " & objattAssetDetails.AstNum & "")
                End If
                If objattAssetDetails.CompanyID <> 0 Then
                    strQuery.Append(" and AssetDetails.CompanyID = " & objattAssetDetails.CompanyID & "")
                End If
                If Not String.IsNullOrEmpty(AppConfig.CompanyIDS) And AppConfig.CompanyIDS <> "0" Then
                    strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
                End If
                If Trim(objattAssetDetails.CustodianID) <> "" Then
                    strQuery.Append(" and AssetDetails.CustodianID='" & objattAssetDetails.CustodianID & "'")
                End If

                strQuery.Append("  order by AstNum")
            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append(" select    AstNum,Cast (Assets.ItemCode as Number) as itemcode,Assets.AstDesc,AssetDetails.AstID,Location.LocDesc,Custodian.CustodianName,AssetDetails.LocID,Assets.AstCatID,AssetDetails.CustodianID,Brand.AstBrandName,Companies.CompanyName,AssetDetails.BarCode,AssetDetails.Refno,AssetDetails.CompanyID,AssetDetails.Purdate,AssetDetails.AstModel,Insurer.InsName,Category.AstCatDesc,Supplier.SuppName, AssetDetails.TransRemarks,AssetDetails.BaseCost,AssetDetails.Tax,AssetDetails.SrvDate,AssetDetails.InsID,AssetDetails.InvNumber,AssetDetails.AstBrandID,AssetDetails.POCode,AssetDetails.SuppID,AssetDetails.Disposed,AssetDetails.Discount,AssetDetails.Barcode,AssetDetails.SerailNo,RefCode,Plate,Poerp,Capex,Grn,AssetDetails.NoPiece,AssetDetails.GLCode,AssetDetails.PONumber,AssetDetails.LabelCount,Assetdetails.AstDesc as Assetdetailsdesc1, Assetdetails.AstDesc2  as Assetdetailsdesc2,Category.CatFullPath,Location.LocationFullPath,CapitalizationDate,BussinessArea,InventoryNumber,CostCenterID,InStockAsset,EvaluationGroup1,EvaluationGroup2,EvaluationGroup3,EvaluationGroup4,CreatedBY,CostCenter.CostNumber,CostCenter.CostName,AssetDetails.CustomFld1,AssetDetails.CustomFld2,AssetDetails.CustomFld3,AssetDetails.CustomFld4,AssetDetails.CustomFld5,AssetDetails.Warranty,AssetDetails.StatusID,AssetDetails.DisposalComments ")
                strQuery.Append(" from Assets")
                strQuery.Append("       inner join(AssetDetails ")
                strQuery.Append("           left outer join Location on AssetDetails.LocID = Location.LocID ")
                strQuery.Append("        left outer join Companies on AssetDetails.CompanyID = Companies.CompanyID ")
                strQuery.Append("               left outer join Brand on AssetDetails.AstBrandID = Brand.AstBrandID ")
                strQuery.Append("           left outer join Custodian on AssetDetails.CustodianID = custodian.CustodianID ")
                strQuery.Append("   left outer join Supplier on AssetDetails.SuppID = Supplier.SuppID	 ")
                strQuery.Append("        left outer join Insurer on AssetDetails.InsID = Insurer.InsCode ")
                strQuery.Append("        left outer join CostCenter on AssetDetails.CostCenterID = CostCenter.CostID )")
                strQuery.Append("           on Assets.ItemCode = AssetDetails.ItemCode ")
                strQuery.Append(" left outer join Category on Assets.AstCatID = Category.AstCatID ")

                strQuery.Append("  where AssetDetails.IsDeleted = 0")
                If objattAssetDetails.LocID.ToString() <> "" Then
                    strQuery.Append(" and AssetDetails.LocID ='" & objattAssetDetails.LocID & "'")
                End If
                If objattAssetDetails.PKeyCode <> "" Then
                    strQuery.Append(" and AstID = '" & objattAssetDetails.PKeyCode & "'")
                End If
                If objattAssetDetails.AstNum <> 0 Then
                    strQuery.Append(" and AstNum = " & objattAssetDetails.AstNum & "")
                End If
                If objattAssetDetails.CompanyID <> 0 Then
                    strQuery.Append(" and AssetDetails.CompanyID = " & objattAssetDetails.CompanyID & "")
                End If
                If Not String.IsNullOrEmpty(AppConfig.CompanyIDS) And AppConfig.CompanyIDS <> "0" Then
                    strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
                End If
                If Trim(objattAssetDetails.CustodianID) <> "" Then
                    strQuery.Append(" and AssetDetails.CustodianID='" & objattAssetDetails.CustodianID & "'")
                End If

                strQuery.Append("  order by AstNum")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAsset_LabelData(ByVal AssetIDs As String) As IDbCommand

            Dim strQuery As New StringBuilder
            Dim objCommand As OleDbCommand = New OleDbCommand

            If AppConfig.DbType = "1" Then
                strQuery.Append(" select    AstNum,Cast (Assets.ItemCode as bigint) as itemcode,Assets.AstDesc,AssetDetails.AstID,Location.LocDesc,Custodian.CustodianName,AssetDetails.LocID,Assets.AstCatID,AssetDetails.CustodianID,Brand.AstBrandName,Companies.CompanyName,AssetDetails.BarCode,AssetDetails.Refno,AssetDetails.CompanyID,AssetDetails.Purdate,AssetDetails.AstModel,Insurer.InsName,Category.AstCatDesc,Supplier.SuppName, AssetDetails.TransRemarks,AssetDetails.BaseCost,AssetDetails.Tax,AssetDetails.SrvDate,AssetDetails.InsID,AssetDetails.InvNumber,AssetDetails.AstBrandID,AssetDetails.POCode,AssetDetails.SuppID,AssetDetails.Disposed,AssetDetails.Discount,AssetDetails.Barcode,AssetDetails.SerailNo,RefCode,Plate,Poerp,Capex,Grn,AssetDetails.NoPiece,AssetDetails.GLCode,AssetDetails.PONumber,AssetDetails.LabelCount ,Assetdetails.AstDesc as Assetdetailsdesc1, Assetdetails.AstDesc2  as Assetdetailsdesc2,Category.CatFullPath,left(Location.LocationFullPath,charindex('\',Location.LocationFullPath)-1) as LocationFullPath ,CapitalizationDate,BussinessArea,InventoryNumber,CostCenterID,InStockAsset,EvaluationGroup1,EvaluationGroup2,EvaluationGroup3,EvaluationGroup4,CreatedBY,CostCenter.CostNumber,CostCenter.CostName,AssetDetails.CustomFld1,AssetDetails.CustomFld2,AssetDetails.CustomFld3,AssetDetails.CustomFld4,AssetDetails.CustomFld5,AssetDetails.Warranty,AssetStatus.Status,Location.Code as LocationCode,Location.CompCode as LocationCompCode ")
                strQuery.Append(" from Assets")
                strQuery.Append("       inner join(AssetDetails ")
                strQuery.Append("           inner join AssetStatus on AssetDetails.StatusID = AssetStatus.ID ")
                strQuery.Append("           left outer join Location on AssetDetails.LocID = Location.LocID ")
                strQuery.Append("        left outer join Companies on AssetDetails.CompanyID = Companies.CompanyID ")
                strQuery.Append("               left outer join Brand on AssetDetails.AstBrandID = Brand.AstBrandID ")
                strQuery.Append("           left outer join Custodian on AssetDetails.CustodianID = custodian.CustodianID ")
                strQuery.Append("   left outer join Supplier on AssetDetails.SuppID = Supplier.SuppID	 ")
                strQuery.Append("   left outer join CostCenter on AssetDetails.CostCenterID = CostCenter.CostID	 ")
                strQuery.Append("        left outer join Insurer on AssetDetails.InsID = Insurer.InsCode )")
                strQuery.Append("           on Assets.ItemCode = AssetDetails.ItemCode ")
                strQuery.Append(" left outer join Category on Assets.AstCatID = Category.AstCatID ")

                strQuery.Append("  where AssetDetails.IsDeleted = 0")
                If AssetIDs <> "" Then
                    strQuery.Append(" and AstID IN (" & AssetIDs & ")")
                End If
                If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                    strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
                End If
                strQuery.Append("  order by AstID")
            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append(" select    AstNum,Cast (Assets.ItemCode as Number) as itemcode,Assets.AstDesc,AssetDetails.AstID,Location.LocDesc,Custodian.CustodianName,AssetDetails.LocID,Assets.AstCatID,AssetDetails.CustodianID,Brand.AstBrandName,Companies.CompanyName,AssetDetails.BarCode,AssetDetails.Refno,AssetDetails.CompanyID,AssetDetails.Purdate,AssetDetails.AstModel,Insurer.InsName,Category.AstCatDesc,Supplier.SuppName, AssetDetails.TransRemarks,AssetDetails.BaseCost,AssetDetails.Tax,AssetDetails.SrvDate,AssetDetails.InsID,AssetDetails.InvNumber,AssetDetails.AstBrandID,AssetDetails.POCode,AssetDetails.SuppID,AssetDetails.Disposed,AssetDetails.Discount,AssetDetails.Barcode,AssetDetails.SerailNo,RefCode,Plate,Poerp,Capex,Grn,AssetDetails.NoPiece,AssetDetails.GLCode,AssetDetails.PONumber,AssetDetails.LabelCount,Assetdetails.AstDesc as Assetdetailsdesc1, Assetdetails.AstDesc2  as Assetdetailsdesc2,Category.CatFullPath,Location.LocationFullPath,CapitalizationDate,BussinessArea,InventoryNumber,CostCenterID,InStockAsset,EvaluationGroup1,EvaluationGroup2,EvaluationGroup3,EvaluationGroup4,CreatedBY,CostCenter.CostNumber,CostCenter.CostName,AssetDetails.CustomFld1,AssetDetails.CustomFld2,AssetDetails.CustomFld3,AssetDetails.CustomFld4,AssetDetails.CustomFld5,AssetDetails.Warranty,AssetStatus.Status,Location.Code as LocationCode,Location.CompCode as LocationCompCode ")
                strQuery.Append(" from Assets")
                strQuery.Append("       inner join(AssetDetails ")
                strQuery.Append("           inner join AssetStatus on AssetDetails.StatusID = AssetStatus.ID ")
                strQuery.Append("           left outer join Location on AssetDetails.LocID = Location.LocID ")
                strQuery.Append("        left outer join Companies on AssetDetails.CompanyID = Companies.CompanyID ")
                strQuery.Append("               left outer join Brand on AssetDetails.AstBrandID = Brand.AstBrandID ")
                strQuery.Append("           left outer join Custodian on AssetDetails.CustodianID = custodian.CustodianID ")
                strQuery.Append("   left outer join Supplier on AssetDetails.SuppID = Supplier.SuppID	 ")
                strQuery.Append("   left outer join CostCenter on AssetDetails.CostCenterID = CostCenter.CostID	 ")
                strQuery.Append("        left outer join Insurer on AssetDetails.InsID = Insurer.InsCode )")
                strQuery.Append("           on Assets.ItemCode = AssetDetails.ItemCode ")
                strQuery.Append(" left outer join Category on Assets.AstCatID = Category.AstCatID ")

                strQuery.Append("  where AssetDetails.IsDeleted = 0")
                If AssetIDs <> "" Then
                    strQuery.Append(" and AstID IN (" & AssetIDs & ")")
                End If
                If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                    strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
                End If

                strQuery.Append("  order by AstID")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAsset_LabelDataBySerialNumbers(ByVal Serials As String) As IDbCommand

            Dim strQuery As New StringBuilder
            Dim objCommand As OleDbCommand = New OleDbCommand

            strQuery.Append(" select    AstNum,Cast (Assets.ItemCode as bigint) as itemcode,Assets.AstDesc,AssetDetails.AstID,Location.LocDesc,Custodian.CustodianName,AssetDetails.LocID,Assets.AstCatID,AssetDetails.CustodianID,Brand.AstBrandName,Companies.CompanyName,AssetDetails.BarCode,AssetDetails.Refno,AssetDetails.CompanyID,AssetDetails.Purdate,AssetDetails.AstModel,Insurer.InsName,Category.AstCatDesc,Supplier.SuppName, AssetDetails.TransRemarks,AssetDetails.BaseCost,AssetDetails.Tax,AssetDetails.SrvDate,AssetDetails.InsID,AssetDetails.InvNumber,AssetDetails.AstBrandID,AssetDetails.POCode,AssetDetails.SuppID,AssetDetails.Disposed,AssetDetails.Discount,AssetDetails.Barcode,AssetDetails.SerailNo,RefCode,Plate,Poerp,Capex,Grn,AssetDetails.NoPiece,AssetDetails.GLCode,AssetDetails.PONumber,AssetDetails.LabelCount ,Assetdetails.AstDesc as Assetdetailsdesc1, Assetdetails.AstDesc2  as Assetdetailsdesc2,Category.CatFullPath,Location.LocationFullPath ,CapitalizationDate,BussinessArea,InventoryNumber,CostCenterID,InStockAsset,EvaluationGroup1,EvaluationGroup2,EvaluationGroup3,EvaluationGroup4,CreatedBY,CostCenter.CostNumber,CostCenter.CostName,AssetDetails.CustomFld1,AssetDetails.CustomFld2,AssetDetails.CustomFld3,AssetDetails.CustomFld4,AssetDetails.CustomFld5,AssetDetails.Warranty")
            strQuery.Append(" from Assets")
            strQuery.Append("       inner join(AssetDetails ")
            strQuery.Append("           left outer join Location on AssetDetails.LocID = Location.LocID ")
            strQuery.Append("        left outer join Companies on AssetDetails.CompanyID = Companies.CompanyID ")
            strQuery.Append("               left outer join Brand on AssetDetails.AstBrandID = Brand.AstBrandID ")
            strQuery.Append("           left outer join Custodian on AssetDetails.CustodianID = custodian.CustodianID ")
            strQuery.Append("   left outer join Supplier on AssetDetails.SuppID = Supplier.SuppID	 ")
            strQuery.Append("   left outer join CostCenter on AssetDetails.CostCenterID = CostCenter.CostID	 ")
            strQuery.Append("        left outer join Insurer on AssetDetails.InsID = Insurer.InsCode )")
            strQuery.Append("           on Assets.ItemCode = AssetDetails.ItemCode ")
            strQuery.Append(" left outer join Category on Assets.AstCatID = Category.AstCatID ")

            strQuery.Append("  where AssetDetails.IsDeleted = 0")
            If Serials <> "" Then
                strQuery.Append(" and SerailNo IN (" & Serials & ")")
            End If
            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If
            strQuery.Append("  order by AstID")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Get_AssetsDetails_First_Last(ByVal First_Last As Int32) As IDbCommand
            '1 for First, 2 for Last
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select astid from AssetDetails")
            ' strQuery.Append("  where AssetDetails.IsDeleted = 0")


            If First_Last = 1 Then
                strQuery.Append(" where astnum = (select min(astnum) from AssetDetails where AssetDetails.IsDeleted = 0 ")
            ElseIf First_Last = 2 Then
                strQuery.Append(" where astnum = (select max(astnum) from AssetDetails where AssetDetails.IsDeleted = 0 ")
            End If


            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If


            If Trim(objattAssetDetails.CompanyID) <> 0 Then
                strQuery.Append(" and AssetDetails.CompanyID=" & objattAssetDetails.CompanyID & ")")
            Else
                strQuery.Append(" )")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Get_AssetsDetails_Pre_Next(ByVal Pre_Next As Int32) As IDbCommand
            '1 for Previous, 2 for Next
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If objattAssetDetails.AstNum <> 0 Then
                If Pre_Next = 1 Then
                    strQuery.Append("select AssetDetails.AstID from AssetDetails ")
                    strQuery.Append(" where astnum = (select max(AstNum) from AssetDetails where AssetDetails.IsDeleted = 0 and AstNum < " & objattAssetDetails.AstNum & "")
                ElseIf Pre_Next = 2 Then
                    strQuery.Append("select AssetDetails.AstID from AssetDetails ")
                    strQuery.Append(" where astnum = (select min(AstNum) from AssetDetails where AssetDetails.IsDeleted = 0 and AstNum > " & objattAssetDetails.AstNum & "")
                End If
                If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                    strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
                End If

                If Trim(objattAssetDetails.CompanyID) <> 0 Then
                    strQuery.Append(" and AssetDetails.CompanyID=" & objattAssetDetails.CompanyID & ")")
                Else
                    strQuery.Append(" )")
                End If
                objCommand.CommandText = strQuery.ToString()
                _Command = objCommand
                Return objCommand
            Else
                Return Get_AssetsDetails_First_Last(2) 'return the last row if there is no asset number
            End If
        End Function

        Public Function Get_AssetsDetails_Pre_Nextold(ByVal Pre_Next As Int32) As IDbCommand
            '1 for Previous, 2 for Next
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            If AppConfig.DbType = "1" Then
                strQuery.Append("select AssetDetails.AstID,AstNum,Assets.AstDesc,Location.LocDesc,Custodian.CustodianName,Brand.AstBrandName,AssetDetails.Refno,AssetDetails.GLCode,AssetDetails.PONumber  from Assets")
                strQuery.Append(" inner join(AssetDetails ")
                strQuery.Append(" left outer join Location on AssetDetails.LocID = Location.LocID ")
                strQuery.Append(" left outer join Brand on AssetDetails.AstBrandID = Brand.AstBrandID ")
                strQuery.Append(" left outer join Custodian on AssetDetails.CustodianID = custodian.CustodianID )")
                strQuery.Append(" on Assets.ItemCode = AssetDetails.ItemCode")
                strQuery.Append("  where AssetDetails.IsDeleted = 0")
                If objattAssetDetails.AstNum <> 0 Then
                    If Pre_Next = 1 Then
                        strQuery.Append(" and AstNum < " & objattAssetDetails.AstNum & "")
                    ElseIf Pre_Next = 2 Then
                        strQuery.Append(" and AstNum > " & objattAssetDetails.AstNum & "")
                    End If

                End If

                If Trim(objattAssetDetails.CompanyID) <> 0 Then
                    strQuery.Append(" and AssetDetails.CompanyID=" & objattAssetDetails.CompanyID & "")
                End If
                If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                    strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
                End If
                strQuery.Append("  order by AstNum")
            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append("select AssetDetails.AstID,AstNum,Cast (Assets.ItemCode as Number) as itemcode,Assets.AstDesc,Location.LocDesc,Custodian.CustodianName,AssetDetails.LocID,Assets.AstCatID,AssetDetails.CustodianID,Brand.AstBrandName,AssetDetails.discount,AssetDetails.GLCode,AssetDetails.PONumber  from Assets")
                strQuery.Append(" inner join(AssetDetails ")
                strQuery.Append(" left outer join Location on AssetDetails.LocID = Location.LocID ")
                strQuery.Append(" left outer join Brand on AssetDetails.AstBrandID = Brand.AstBrandID ")
                strQuery.Append(" left outer join Custodian on AssetDetails.CustodianID = custodian.CustodianID )")
                strQuery.Append(" on Assets.ItemCode = AssetDetails.ItemCode")
                strQuery.Append("  where AssetDetails.IsDeleted = 0")
                If objattAssetDetails.AstNum <> 0 Then
                    If Pre_Next = 1 Then
                        strQuery.Append(" and AstNum < " & objattAssetDetails.AstNum & "")
                    ElseIf Pre_Next = 2 Then
                        strQuery.Append(" and AstNum > " & objattAssetDetails.AstNum & "")
                    End If

                End If

                If Trim(objattAssetDetails.CompanyID) <> 0 Then
                    strQuery.Append(" and AssetDetails.CompanyID=" & objattAssetDetails.CompanyID & "")
                End If
                If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                    strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
                End If
                strQuery.Append("  order by AstNum")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function GetAsset_DetailsCombo() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            If AppConfig.DbType = "1" Then
                'strQuery.Append("select AssetDetails.AstID,AstNum,Cast (Assets.ItemCode as bigint) as itemcode,Assets.AstDesc,Location.LocDesc,Custodian.CustodianName,AssetDetails.LocID,Assets.AstCatID,AssetDetails.CustodianID,Brand.AstBrandName,AssetDetails.Refno  from Assets")
                strQuery.Append("select AssetDetails.AstID,AstNum,Assets.AstDesc,Location.LocDesc,Custodian.CustodianName,Brand.AstBrandName,AssetDetails.Refno, AssetDetails.disposed from Assets")
                strQuery.Append(" inner join(AssetDetails ")
                strQuery.Append(" left outer join Location on AssetDetails.LocID = Location.LocID ")
                strQuery.Append(" left outer join Brand on AssetDetails.AstBrandID = Brand.AstBrandID ")
                strQuery.Append(" left outer join Custodian on AssetDetails.CustodianID = custodian.CustodianID )")
                strQuery.Append(" on Assets.ItemCode = AssetDetails.ItemCode")
                strQuery.Append("  where AssetDetails.IsDeleted = 0")
                If objattAssetDetails.LocID.ToString() <> "" Then
                    strQuery.Append(" and AssetDetails.LocID ='" & objattAssetDetails.LocID & "'")
                End If
                If objattAssetDetails.PKeyCode <> "" Then
                    strQuery.Append(" and AstID = '" & objattAssetDetails.PKeyCode & "'")
                End If
                If objattAssetDetails.AstNum <> 0 Then
                    strQuery.Append(" and AstNum = " & objattAssetDetails.AstNum & "")
                End If
                If objattAssetDetails.Disposed = False Then
                    strQuery.Append(" and Disposed = 0")
                End If
                If Trim(objattAssetDetails.CustodianID) <> "" Then
                    strQuery.Append(" and AssetDetails.CustodianID='" & objattAssetDetails.CustodianID & "'")
                End If

                If Trim(objattAssetDetails.CompanyID) <> 0 Then
                    strQuery.Append(" and AssetDetails.CompanyID=" & objattAssetDetails.CompanyID & "")
                End If
                If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                    strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
                End If
                strQuery.Append("  order by AstNum")
            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append("select AssetDetails.AstID,AstNum,Cast (Assets.ItemCode as Number) as itemcode,Assets.AstDesc,Location.LocDesc,Custodian.CustodianName,AssetDetails.LocID,Assets.AstCatID,AssetDetails.CustodianID,Brand.AstBrandName,AssetDetails.discount,AssetDetails.GLCode,AssetDetails.PONumber  from Assets")
                strQuery.Append(" inner join(AssetDetails ")
                strQuery.Append(" left outer join Location on AssetDetails.LocID = Location.LocID ")
                strQuery.Append(" left outer join Brand on AssetDetails.AstBrandID = Brand.AstBrandID ")
                strQuery.Append(" left outer join Custodian on AssetDetails.CustodianID = custodian.CustodianID )")
                strQuery.Append(" on Assets.ItemCode = AssetDetails.ItemCode")
                strQuery.Append("  where AssetDetails.IsDeleted = 0")
                If objattAssetDetails.LocID.ToString() <> "" Then
                    strQuery.Append(" and AssetDetails.LocID ='" & objattAssetDetails.LocID & "'")
                End If
                If objattAssetDetails.PKeyCode <> "" Then
                    strQuery.Append(" and AstID = '" & objattAssetDetails.PKeyCode & "'")
                End If
                If objattAssetDetails.AstNum <> 0 Then
                    strQuery.Append(" and AstNum = " & objattAssetDetails.AstNum & "")
                End If
                If objattAssetDetails.AstNum <> 0 Then
                    strQuery.Append(" and AstNum = " & objattAssetDetails.AstNum & "")
                End If
                If objattAssetDetails.Disposed = False Then
                    strQuery.Append(" and Disposed = 0")
                End If
                If Trim(objattAssetDetails.CustodianID) <> "" Then
                    strQuery.Append(" and AssetDetails.CustodianID='" & objattAssetDetails.CustodianID & "'")
                End If
                If Trim(objattAssetDetails.CompanyID) <> 0 Then
                    strQuery.Append(" and AssetDetails.CompanyID=" & objattAssetDetails.CompanyID & "")
                End If

                If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                    strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
                End If
                strQuery.Append("  order by AstNum")
            End If


            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function GetAssetData_List() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("select AssetDetails.AstID,Assets.AstDesc,AssetDetails.AstModel,SerailNo,Location.LocationFullPath,CatFullPath from Assets")
            strQuery.Append(" inner join(AssetDetails ")
            strQuery.Append(" left outer join Location on AssetDetails.LocID = Location.LocID ")
            strQuery.Append(" left outer join Brand on AssetDetails.AstBrandID = Brand.AstBrandID ")
            strQuery.Append(" left outer join Custodian on AssetDetails.CustodianID = custodian.CustodianID )")
            strQuery.Append(" on Assets.ItemCode = AssetDetails.ItemCode")
            strQuery.Append(" inner join Category on Assets.AstCatID = Category.AstCatID")
            strQuery.Append("  where AssetDetails.IsDeleted = 0")
            strQuery.Append("  order by AstID")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAssetData_RepItemList() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("select AssetDetails.Barcode,AssetDetails.AstID,AssetDetails.AstNum,Assets.AstDesc,AssetDetails.AstModel,SerailNo,Location.LocationFullPath,CatFullPath,Custodian.CustodianName,Custodian.CustodianID,Location.LocID from Assets")
            strQuery.Append(" inner join(AssetDetails ")
            strQuery.Append(" left outer join Location on AssetDetails.LocID = Location.LocID ")
            strQuery.Append(" left outer join Brand on AssetDetails.AstBrandID = Brand.AstBrandID ")
            strQuery.Append(" left outer join Custodian on AssetDetails.CustodianID = custodian.CustodianID )")
            strQuery.Append(" on Assets.ItemCode = AssetDetails.ItemCode")
            strQuery.Append(" inner join Category on Assets.AstCatID = Category.AstCatID")
            strQuery.Append("  where AssetDetails.IsDeleted = 0")
            strQuery.Append("  order by Barcode")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Asset_Search(ByVal objattAssets As attItems, ByVal IncludeChild As String, ByVal strDepartment As String) As IDbCommand
            Dim strQuery As New StringBuilder
            Dim objCommand As OleDbCommand = New OleDbCommand
            If objattAssets Is Nothing Then
                objattAssets = New attItems
            End If
            If AppConfig.DbType = "1" Then
                strQuery.Append("select '',AstNum,Cast(Assets.ItemCode as bigint) as itemcode,Assets.AstDesc,AssetDetails.AstID,Location.LocationFullPath as LocDesc,Custodian.CustodianName,AssetDetails.LocID,Assets.AstCatID,AssetDetails.CustodianID,AssetDetails.BaseCost + AssetDetails.tax,AssetDetails.discount,AssetDetails.SerailNo as Serial#  from Assets")
            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append("select '',AstNum,Cast(Assets.ItemCode as Number) as itemcode,Assets.AstDesc,AssetDetails.AstID,Location.LocationFullPath as LocDesc,Custodian.CustodianName,AssetDetails.LocID,Assets.AstCatID,AssetDetails.CustodianID,AssetDetails.BaseCost + AssetDetails.tax,AssetDetails.discount,AssetDetails.SerailNo as Serial#  from Assets")
            End If

            strQuery.Append(" inner join(AssetDetails ")
            strQuery.Append(" left outer join Location on AssetDetails.LocID = Location.LocID ")
            strQuery.Append(" left outer join Custodian on AssetDetails.CustodianID = custodian.CustodianID )")
            strQuery.Append(" on Assets.ItemCode = AssetDetails.ItemCode")
            strQuery.Append("  where AssetDetails.IsDeleted = 0")

            If objattAssetDetails.LocID.ToString() <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (AssetDetails.LocId like ? or AssetDetails.LocId like ?)")
                    objCommand.Parameters.Add(New OleDbParameter("@LocID", objattAssetDetails.LocID))
                    objCommand.Parameters.Add(New OleDbParameter("@LocID1", objattAssetDetails.LocID & "-%"))
                Else
                    strQuery.Append(" and AssetDetails.LocId like ?")
                    objCommand.Parameters.Add(New OleDbParameter("@LocID", objattAssetDetails.LocID))
                End If
            End If

            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
                'objCommand.Parameters.Add(New OleDbParameter("@CompanyIDS", AppConfig.CompanyIDS))
            End If
            If objattAssets.AstBrandID <> 0 Then
                strQuery.Append(" and AssetDetails.AstBrandID = ?")
                objCommand.Parameters.Add(New OleDbParameter("@AstBrandID", objattAssets.AstBrandID))
            End If
            If objattAssets.AstCatID <> "" Then
                If IncludeChild Then
                    strQuery.Append(" and (Assets.AstCatID like ? or Assets.AstCatID like ?)")
                    objCommand.Parameters.Add(New OleDbParameter("@AstCatID", objattAssets.AstCatID))
                    objCommand.Parameters.Add(New OleDbParameter("@AstCatID1", objattAssets.AstCatID & "-%"))
                Else
                    strQuery.Append(" and Assets.AstCatID  like ?")
                    objCommand.Parameters.Add(New OleDbParameter("@AstCatID", objattAssets.AstCatID))
                End If
            End If
            If objattAssetDetails.CustodianID <> "" Then
                strQuery.Append(" and AssetDetails.CustodianID = ?")
                objCommand.Parameters.Add(New OleDbParameter("@CustodianID", objattAssetDetails.CustodianID))
            End If

            If objattAssets.PKeyCode <> "" Then
                strQuery.Append(" and Assets.ItemCode = ?")
                objCommand.Parameters.Add(New OleDbParameter("@ItemCode", objattAssets.PKeyCode))
            End If
            If objattAssetDetails.PKeyCode <> "" Then
                strQuery.Append(" and AstID = ?")
                objCommand.Parameters.Add(New OleDbParameter("@AstID", objattAssetDetails.PKeyCode))
            End If
            If objattAssetDetails.AstNum <> 0 Then
                strQuery.Append(" and AstNum = ?")
                objCommand.Parameters.Add(New OleDbParameter("@AstNum", objattAssetDetails.AstNum))
            End If
            If strDepartment <> "" Then
                strQuery.Append(" and Custodian.DeptID = ?")
                objCommand.Parameters.Add(New OleDbParameter("@strDepartment", strDepartment))
            End If

            If objattAssetDetails.AstDesc <> "" Then
                strQuery.Append(" and (AssetDetails.AstDesc like ? or AssetDetails.AstDesc2 like ? or Assets.AstDesc like ?)")
                objCommand.Parameters.Add(New OleDbParameter("@AstDesc", "%" & objattAssetDetails.AstDesc & "%"))
                objCommand.Parameters.Add(New OleDbParameter("@AstDesc2", "%" & objattAssetDetails.AstDesc & "%"))
                objCommand.Parameters.Add(New OleDbParameter("@AstDesc3", "%" & objattAssetDetails.AstDesc & "%"))
            End If

            If objattAssetDetails.SerailNo <> "" Then
                strQuery.Append(" and SerailNo like ?")
                objCommand.Parameters.Add(New OleDbParameter("@SerailNo", "%" & objattAssetDetails.SerailNo & "%"))
            End If

            strQuery.Append(" order by AstNum")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function GetDatabyID(ByVal Id As String) As IDbCommand Implements IEntity.GetDatabyID
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select * from GrpChild")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Check_ReferenceID(ByVal _id As String, ByVal _astid As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select AstID from AssetDetails where isdeleted = 0 and RefNo ='" & _id & "'")
            If _astid <> "" Then
                strQuery.Append(" and AstID <> '" & _astid & "'")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function GetAssetsLocations() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select AstID,LocID,InvStatus from AssetDetails where isdeleted = 0 ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Check_Child(ByVal _id As String, ByVal formid As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select Count(*) from AssetDetails where isdeleted = 0")
            If formid = 1 Then
                strQuery.Append(" and AstBrandID =" & _id)
            ElseIf formid = 2 Then
                strQuery.Append(" and CustodianID =?")
                objCommand.Parameters.Add(New OleDbParameter("@CustodianID", _id))
            ElseIf formid = 3 Then
                strQuery.Append(" and InsID =" & _id)
            ElseIf formid = 4 Then
                strQuery.Append(" and Dispcode =" & _id & " and Disposed = 1 ")
            ElseIf formid = 5 Then
                strQuery.Append(" and  SuppID ='" & _id & "'")
            ElseIf formid = 6 Then
                strQuery.Append(" and  Itemcode ='" & _id & "'")
            ElseIf formid = 11 Then
                strQuery.Append(" and  LocId Like '" & _id & "-%'")
            ElseIf formid = 12 Then
                strQuery.Append(" and  GLCode =" & _id & "")

            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetPKey(ByVal CompID As String, ByVal StartRange As Int64, ByVal EndRange As Int64) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(AstNum)  from AssetDetails where CompanyID = ?")
            strQuery.Append(" and astNum >= ? and astNum <= ? ")
            objCommand.Parameters.Add(New OleDbParameter("@CompID", CompID))
            objCommand.Parameters.Add(New OleDbParameter("@StartRange", StartRange))
            objCommand.Parameters.Add(New OleDbParameter("@EndRange", EndRange))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function GetHier(ByVal CustID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("SELECT DeptID")
            strQuery.Append(" FROM Custodian")
            strQuery.Append(" WHERE CustodianID='" & CustID & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder


            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from AssetDetails")
                strQuery.Append(" where AstID = ?")
            Else
                strQuery.Append("update AssetDetails")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where AstID = ?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@AstID", objattAssetDetails.PKeyCode))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function UpdateLabelCount(ByVal AstId As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" Update Assetdetails ")
            If AppConfig.DbType = "1" Then
                strQuery.Append(" Set LabelCount = (select ISNULL(LabelCount,0) + 1 from AssetDetails where AstId = '" & AstId & "')")
            Else
                strQuery.Append(" Set LabelCount = (select nvl(LabelCount,0) + 1 from AssetDetails where AstId = '" & AstId & "')")
            End If
            strQuery.Append(" where AstId = '" & AstId & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAssetStatus(ByVal IsReturnStatus As Boolean, ByVal IsALL As Boolean) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select ID,Status from AssetStatus ")
            If Not IsALL Then
                strQuery.Append(" where IsReturnStatus=?")
                objCommand.Parameters.Add(New OleDbParameter("@IsReturnStatus", IsReturnStatus))
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAssetStatusByDesc(ByVal Desc As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select ID from AssetStatus where Status = '" & Desc & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(AstNum) from AssetDetails")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function Update_DefaultBook() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetDetails")
            strQuery.Append(" set")
            strQuery.Append(" BookId='" & objattAssetDetails.BookID & "'")
            strQuery.Append(" where AstID ='" & Convert.ToString(objattAssetDetails.PKeyCode) & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Get_Status() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select InvStatus from AssetDetails")
            strQuery.Append(" where AstID ='" & Convert.ToString(objattAssetDetails.PKeyCode) & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update_InvStatus() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetDetails set")

            If Not String.IsNullOrEmpty(objattAssetDetails.LocID) Then
                strQuery.Append(" LocID='" & objattAssetDetails.LocID & "'")
            Else
                strQuery.Append(" LocID = LocID")
            End If

            If objattAssetDetails.InvSchCode > 0 Then
                strQuery.Append(", InvSchCode=" & objattAssetDetails.InvSchCode)
            End If

            If Not String.IsNullOrEmpty(objattAssetDetails.CustodianID) Then
                strQuery.Append(", CustodianID='" & objattAssetDetails.CustodianID & "'")
            End If

            If Not String.IsNullOrEmpty(objattAssetDetails.SerailNo) Then
                strQuery.Append(", SerailNo=N'" & objattAssetDetails.SerailNo & "'")
            End If

            If Not String.IsNullOrEmpty(objattAssetDetails.AstDesc) Then
                strQuery.Append(", AstDesc=N'" & objattAssetDetails.AstDesc & "'")
            End If

            If objattAssetDetails.InvStatus >= 0 Then
                strQuery.Append(", InvStatus=" & objattAssetDetails.InvStatus & " ")
            End If

            If objattAssetDetails.LastInventoryDate <> Nothing Then
                strQuery.Append(", LastInventoryDate=" & BackEndDate(objattAssetDetails.LastInventoryDate) & " ")
            End If

            If objattAssetDetails.CostCenterID <> "" Then
                strQuery.Append(", CostCenterID='" & objattAssetDetails.CostCenterID & "' ")
            End If

            If objattAssetDetails.InventoryNumber <> "" Then
                strQuery.Append(", InventoryNumber='" & objattAssetDetails.InventoryNumber & "' ")
            End If
            If objattAssetDetails.LastEditBY <> "" Then
                strQuery.Append(", LastEditBY='" & objattAssetDetails.LastEditBY & "' ")
            End If

            If objattAssetDetails.IsDataChanged Then
                strQuery.Append(", IsDataChanged=1 ")
            End If

            strQuery.Append(",LastEditDate=" & BackEndDate(objattAssetDetails.LastInventoryDate) & " where AstID ='" & Convert.ToString(objattAssetDetails.PKeyCode) & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update_Status() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetDetails set")
            strQuery.Append(" StatusID =" & objattAssetDetails.StatusID & " ")
            strQuery.Append(", IsDataChanged=1,LastEditDate=Getdate() ")
            strQuery.Append(" where AstID ='" & Convert.ToString(objattAssetDetails.PKeyCode) & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function Update_Brand() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetDetails set")
            strQuery.Append(" AstBrandId =" & objattAssetDetails.AstBrandID & " ")
            strQuery.Append(", IsDataChanged=1,LastEditDate=Getdate() ")
            strQuery.Append(" where AstID ='" & Convert.ToString(objattAssetDetails.PKeyCode) & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function UpdateAssetSerialNumber() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetDetails set")
            strQuery.Append(" SerailNo=N'" & objattAssetDetails.SerailNo & "'")
            If objattAssetDetails.IsDataChanged Then
                strQuery.Append(", IsDataChanged=1,LastEditDate=GetDate()")
            End If
            strQuery.Append(" where AstID ='" & Convert.ToString(objattAssetDetails.PKeyCode) & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAstIDBySerialNumLike() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select AstID from AssetDetails where ")
            strQuery.Append(" SerailNo like N'%" & objattAssetDetails.SerailNo & "%'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function GetAstIDBySerialNum() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select AstID from AssetDetails where ")
            strQuery.Append(" SerailNo = N'" & objattAssetDetails.SerailNo & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAstIDBySerialNumAndItemCode() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select AstID from AssetDetails where ")
            strQuery.Append(" SerailNo = N'" & objattAssetDetails.SerailNo & "'")
            strQuery.Append(" and ItemCode = N'" & objattAssetDetails.ItemCode & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        'Dispose_AssetByID(astID)
        Public Function Dispose_AssetByID(ByVal astID As String, ByVal dispDate As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetDetails")
            strQuery.Append(" set")
            strQuery.Append(" Disposed=1 ,DispDate=" & BackEndDate(dispDate) & "")
            strQuery.Append(" where AstID ='" & astID & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Dispose_Asset() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetDetails")
            strQuery.Append(" set")
            strQuery.Append(" Disposed=1 ,")
            strQuery.Append(" DispDate=" & BackEndDate(objattAssetDetails.DispDate) & "")
            If objattAssetDetails.DispCode <> 0 And objattAssetDetails.DispCode <> 1 Then
                strQuery.Append(" , DispCode = " & objattAssetDetails.DispCode)
            End If
            strQuery.Append(" where AstID ='" & Convert.ToString(objattAssetDetails.PKeyCode) & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function SetDisposedAsset() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetDetails")
            strQuery.Append(" set")
            If objattAssetDetails.Disposed Then
                strQuery.Append(" Disposed=1 ,")
                strQuery.Append(" DispDate=" & BackEndDate(objattAssetDetails.DispDate) & "")
                If objattAssetDetails.DispCode <> 0 And objattAssetDetails.DispCode <> 1 Then
                    strQuery.Append(" , DispCode = " & objattAssetDetails.DispCode)
                End If
            Else
                strQuery.Append(" Disposed=0 ,")
                strQuery.Append(" DispDate= null")
                strQuery.Append(" , DispCode = null")
            End If
            If Not String.IsNullOrEmpty(objattAssetDetails.PKeyCode) Then
                strQuery.Append(" where AstID ='" & Convert.ToString(objattAssetDetails.PKeyCode) & "'")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function SetAssetWithValue() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetDetails")
            strQuery.Append(" set")
            If objattAssetDetails.InStockAsset Then
                strQuery.Append(" InStockAsset=1")
            Else
                strQuery.Append(" InStockAsset=0")
            End If
            If Not String.IsNullOrEmpty(objattAssetDetails.PKeyCode) Then
                strQuery.Append(" where AstID ='" & Convert.ToString(objattAssetDetails.PKeyCode) & "'")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function SetCapitalizationDate() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetDetails")
            strQuery.Append(" set")
            strQuery.Append(" CapitalizationDate=?")
            strQuery.Append(" where AstID ='" & Convert.ToString(objattAssetDetails.PKeyCode) & "'")
            If objattAssetDetails.CapitalizationDate <> Nothing Then
                objCommand.Parameters.Add(New OleDbParameter("@CapitalizationDate", objattAssetDetails.CapitalizationDate))
            Else
                objCommand.Parameters.Add(New OleDbParameter("@CapitalizationDate", DBNull.Value))
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function AssetsDetailReport() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select * from AssetDetails  ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function InterCoTranReport() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select AstDtl1.AstID,AstDtl1.OldAssetID ,AstDtl.AstNum,AstDtl1.AstNum,AstDtl.DispDate,AstDtl1.TransRemarks  from AssetDetails AstDtl,AssetDetails AstDtl1 ")
            strQuery.Append("  where AstDtl.AstID = AstDtl1.OldAssetID ")
            strQuery.Append(" and AstDtl.Disposed = 1 and AstDtl.DispCode = 3")
            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and (AstDtl1.CompanyID IN (" & AppConfig.CompanyIDS & ") or  AstDtl1.CompanyID IN (" & AppConfig.CompanyIDS & "))")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function SoldAssetsReport() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select '',AssetDetails.AstID,AssetDetails.AstNum,AssetDetails.DispDate,AssetDetails.Discount,Disposal_Method.DispDesc,AssetDetails.TransRemarks")
            strQuery.Append(" ,AssetDetails.isSold,AssetDetails.Soldto,AssetDetails.Sel_Price,AssetDetails.Sel_Date from AssetDetails ")
            strQuery.Append(" inner join Disposal_Method on Disposal_Method.DispCode = AssetDetails.DispCode ")
            strQuery.Append(" where Assetdetails.Disposed = 1 and AssetDetails.DispCode = 1")
            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        'Public Function DisposeAssetReport() As IDbCommand
        '    Dim objCommand As OleDbCommand = New OleDbCommand
        '    Dim strQuery As New StringBuilder
        '    strQuery.Append(" select '',AssetDetails.AstID,AssetDetails.AstNum,AssetDetails.DispDate,AssetDetails.Discount,Disposal_Method.DispDesc,AssetDetails.TransRemarks")
        '    strQuery.Append(" ,AssetDetails.isSold,AssetDetails.Soldto,AssetDetails.Sel_Price,AssetDetails.Sel_Date from AssetDetails ")
        '    strQuery.Append(" inner join Disposal_Method on Disposal_Method.DispCode = AssetDetails.DispCode ")
        '    strQuery.Append(" where Assetdetails.Disposed = 1")
        '    If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
        '        strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
        '    End If
        '    objCommand.CommandText = strQuery.ToString()
        '    _Command = objCommand
        '    Return objCommand
        'End Function


        'Public Function DamageAssetsReport() As IDbCommand
        '    Dim objCommand As OleDbCommand = New OleDbCommand
        '    Dim strQuery As New StringBuilder
        '    strQuery.Append(" select '',AssetDetails.AstID,AssetDetails.AstNum,AssetDetails.DispDate,AssetDetails.Discount,Disposal_Method.DispDesc,AssetDetails.TransRemarks")
        '    strQuery.Append(" ,AssetDetails.isSold,AssetDetails.Soldto,AssetDetails.Sel_Price,AssetDetails.Sel_Date from AssetDetails ")
        '    strQuery.Append(" inner join Disposal_Method on Disposal_Method.DispCode = AssetDetails.DispCode  ")
        '    strQuery.Append(" where Assetdetails.Disposed = 1 and AssetDetails.DispCode = 4")
        '    If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
        '        strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
        '    End If
        '    objCommand.CommandText = strQuery.ToString()
        '    _Command = objCommand
        '    Return objCommand
        'End Function

        Public Function Depreciation_Book_temp(ByVal forTemp1 As String, ByVal strCompanyID As String, ByVal strBookId As String, ByVal strCustID As String, ByVal strLocId As String, ByVal strCatID As String, ByVal IncludeChild As Boolean, ByVal BrandID As String, ByVal SupplierID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If forTemp1 = "" Then
                strQuery.Append(" ")
                strQuery.Append("    SELECT '',AssetDetails.AstID,AstNum,RefNo,Location.Compcode,Category.AstCatDesc,Custodian.CustodianName, ")
                strQuery.Append(" AssetDetails.AstDesc,AssetDetails.AstDesc2,BaseCost+Tax  as tot,salvageYear, (BaseCost+Tax) - CurrentBV as acc, CurrentBV  FROM AssetDetails  ")
                strQuery.Append(" inner join (Assets left outer join Category on Category.AstCatID = Assets.AstCatID)on Assets.ItemCode = AssetDetails.ItemCode  ")
                strQuery.Append(" inner join Custodian on Custodian.CustodianID = AssetDetails.CustodianID ")
                strQuery.Append(" inner join AstBooks_Temp on AssetDetails.AstID= AstBooks_Temp.AstID  ")
                strQuery.Append(" left outer join Location on Location.LocId = AssetDetails.LocId  ")
                strQuery.Append(" where AssetDetails.IsDeleted = 0 and AssetDetails.Disposed = 0")
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
                If BrandID <> "" Then
                    strQuery.Append(" and AssetDetails.AstBrandId = '" & BrandID & "'")
                End If
                If SupplierID <> "" Then
                    strQuery.Append(" and AssetDetails.suppid = '" & SupplierID & "'")
                End If

                strQuery.Append(" order by AssetDetails.AstNum")
            ElseIf forTemp1 = "1" Then
                strQuery.Append("  SELECT '',AssetDetails.AstID,AstNum,RefNo,Location.Compcode,Category.AstCatDesc,Custodian.CustodianName, ")
                strQuery.Append("  AssetDetails.AstDesc,AssetDetails.AstDesc2,BaseCost+Tax  as tot,salvageYear, (BaseCost+Tax) - CurrentBV as acc, CurrentBV  FROM AssetDetails ")
                strQuery.Append("  inner join (Assets left outer join Category on Category.AstCatID = Assets.AstCatID)on Assets.ItemCode = AssetDetails.ItemCode  ")
                strQuery.Append("  inner join Custodian on Custodian.CustodianID = AssetDetails.CustodianID ")
                strQuery.Append("  inner join AstBooks_temp on AssetDetails.AstID= AstBooks_Temp.AstID ")
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

        Public Function GetLocationAssets(ByVal LocID As String, ByVal InvSchID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DbType = "1" Then
                strQuery.Append("Select CAST(Status as CHAR(1)) as Status,BarCode as Barcode,AssetDetails.AstDesc as AssetDescription,Ast_History.NoPiece as Pieces,AstCatID ")
                strQuery.Append(" ,Fr_loc,To_Loc,AssetDetails.LocID ")
                strQuery.Append(" from AssetDetails inner join Ast_History on AssetDetails.AstID = Ast_History.AstID")
                strQuery.Append(" inner join Assets on Assets.ItemCode = AssetDetails.ItemCode")
                strQuery.Append(" where Disposed = 0 and AssetDetails.LocID = ? and Ast_History.InvSchCode = ?")
            Else
                strQuery.Append("Select CAST(Status as CHAR(1)) as Status,BarCode as Barcode,AssetDetails.AstDesc as AssetDescription,Ast_History.NoPiece as Pieces,AstCatID ")
                strQuery.Append(" ,Fr_loc,To_Loc,AssetDetails.LocID ")
                strQuery.Append(" from AssetDetails inner join Ast_History on AssetDetails.AstID = Ast_History.AstID")
                strQuery.Append(" inner join Assets on Assets.ItemCode = AssetDetails.ItemCode")
                strQuery.Append(" where Disposed = 0 and AssetDetails.LocID = ? and Ast_History.InvSchCode = ?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@LocID", LocID))
            objCommand.Parameters.Add(New OleDbParameter("@InvSchCode", InvSchID))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Get_AssetInvetnoryStatus(ByVal BarCode As String, ByVal InvSchID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select AssetDetails.LocID,AssetDetails.NoPiece as PhysicalNoPiece,Ast_History.* from Ast_History inner join AssetDetails on AssetDetails.AstID = Ast_History.AstID")
            strQuery.Append(" where BarCode =? and Ast_History.InvSchCode = ?")
            objCommand.Parameters.Add(New OleDbParameter("@BarCode", BarCode))
            objCommand.Parameters.Add(New OleDbParameter("@InvSchCode", InvSchID))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function DisableAssetTriggers() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("ALTER TABLE AssetDetails DISABLE TRIGGER ALL")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function EnableAssetTriggers() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("ALTER TABLE AssetDetails ENABLE TRIGGER ALL")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

#End Region

    End Class

End Namespace

