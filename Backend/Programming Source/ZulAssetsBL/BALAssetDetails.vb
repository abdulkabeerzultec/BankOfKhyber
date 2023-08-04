Imports System
Imports System.Windows.Forms
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL
Imports Microsoft.VisualBasic

Namespace ZulAssetBAL
    Public Class BALAssetDetails
        Inherits BLBase

#Region "Data Members"
        Private objAssetDetails As AssetDetails
#End Region
#Region "Functions"

        Public Sub New()
            objAssetDetails = New Assetdetails
        End Sub

        Public Function Verify_Range(ByVal Start As Long, ByVal EndRange As Long) As DataTable
            Try
                Return Me.GeneralExecuter(objAssetDetails.VerfiY_Range(Start, EndRange))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Insert_AssetDetails(ByVal objattAssetDetails As attAssetDetails, ByVal UpdateAssetNumber As Boolean) As Boolean
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Me.Insert(objAssetDetails)

                If UpdateAssetNumber Then
                    If AppConfig.IsOfflineMachine Then
                        Dim objBalOfflineMachine As New BALOfflineMachines
                        objBalOfflineMachine.UpdateLastAssetNumber(objattAssetDetails.AstNum)
                    Else
                        Dim objBALCompany As New BALCompany
                        objBALCompany.SetCompanyLastAssetNumber(objattAssetDetails.CompanyID, objattAssetDetails.AstNum)
                    End If
                End If

                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Public Function Update_AssetDetails(ByVal objattAssetDetails As attAssetDetails) As Boolean
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Me.Update(objAssetDetails)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetNextPKey_AssetDetails(ByVal CompID As String, ByVal StartRange As Int64, ByVal EndRange As Int64) As String
            Try

                Dim strKey As String = ""
                Try
                    strKey = Me.GeneralExecuter_Scalar(objAssetDetails.GetPKey(CompID, StartRange, EndRange), "")
                Catch ex As Exception
                    Throw ex
                End Try
                Return strKey

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetNextPKey_AssetDetails() As String
            Try

                Dim strKey As String = ""
                'Dim intKey As Double = 0
                Try
                    strKey = Me.GeneralExecuter_Scalar(objAssetDetails.GetNextPKey, "")
                    'intKey = CDbl(strKey)
                Catch ex As Exception
                    Throw ex
                End Try
                Return strKey

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAssetsCount(ByVal objattAssetDetails As attAssetDetails) As String
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter_Scalar(objAssetDetails.GetAssetDetailsCount(), "")
                'intKey = CDbl(strKey)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCustHier_AssetDetails(ByVal CustID As String) As String
            Try

                Dim strKey As String = ""

                Try
                    strKey = Me.GeneralExecuter_Scalar(objAssetDetails.GetHier(CustID), "")

                Catch ex As Exception
                    Throw ex
                End Try
                Return strKey

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Sub DeleteAssetImage(ByVal objattAssetDetails As attAssetDetails)
            objAssetDetails.Attribute = objattAssetDetails
            Me.GeneralExecuter(objAssetDetails.DeleteImage())
        End Sub


        Public Sub UpdateAssetImage(ByVal objattAssetDetails As attAssetDetails)
            objAssetDetails.Attribute = objattAssetDetails
            Me.GeneralExecuter(objAssetDetails.UpdateImage())
        End Sub

        Public Sub CopyAssetImage(ByVal NewAstID As String, ByVal OldImage As Byte())
            Me.GeneralExecuter(objAssetDetails.CopyImage(NewAstID, OldImage))
        End Sub


        Public Function GetAssetImage(ByVal objattAssetDetails As attAssetDetails) As Byte()
            objAssetDetails.Attribute = objattAssetDetails
            Dim dt As DataTable = Me.GeneralExecuter(objAssetDetails.GetImage())
            Dim bits As Byte() = Nothing
            If dt.Rows(0)("astImage").ToString <> "" Then
                bits = CType(dt.Rows(0)("astImage"), Byte())
            End If
            Return bits
        End Function

        Public Function GetAllAssetImages() As DataTable
            Return Me.GeneralExecuter(objAssetDetails.GetAllImages())
        End Function

        Public Sub ClearAllAssetImages()
            Me.GeneralExecuter(objAssetDetails.ClearAllImages())
        End Sub

        Public Function GetAll_AssetDetails(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return GetAllData(objAssetDetails)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAssetIDsLocations() As DataTable
            Try
                Return Me.GeneralExecuter(objAssetDetails.GetAssetsLocations)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAssetStatus(ByVal IsReturnStatus As Boolean, ByVal IsAll As Boolean) As DataTable
            Try
                Return Me.GeneralExecuter(objAssetDetails.GetAssetStatus(IsReturnStatus, IsAll))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAssetStatusDesc(ByVal AssetStatusID As String) As String
            Try
                Return Me.GeneralExecuter_Scalar(objAssetDetails.GetAssetStatusDesc(AssetStatusID), "")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAssetStatusByDesc(ByVal Desc As String) As String
            Try
                Dim ID As String = Me.GeneralExecuter_Scalar(objAssetDetails.GetAssetStatusByDesc(Desc), "")
                If ID = "0" Then
                    Return -1
                Else
                    Return ID
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAllData_GetCombo(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.GetAllData_Combo())

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAllData_Active(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.GetAllData_Active())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAllData_ActiveForDep(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.GetAllData_ActiveForDep())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAsset_Details(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.GetAsset_Details())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAssetLabelData(ByVal AssetIDs As String) As DataTable
            Try
                Return Me.GeneralExecuter(objAssetDetails.GetAsset_LabelData(AssetIDs))
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function GetAssetLabelDataBySerials(ByVal Serials As String) As DataTable
            Try
                Return Me.GeneralExecuter(objAssetDetails.GetAsset_LabelDataBySerialNumbers(Serials))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Get_AssetsDetails_First_Last(ByVal objattAssetDetails As attAssetDetails, ByVal First_last As Int32) As String
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter_Scalar(objAssetDetails.Get_AssetsDetails_First_Last(First_last), "")
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function Get_AssetsDetails_Pre_Next(ByVal objattAssetDetails As attAssetDetails, ByVal Pre_Next As Int32) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.Get_AssetsDetails_Pre_Next(Pre_Next))
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAsset_DetailsCombo(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.GetAsset_DetailsCombo())
            Catch ex As Exception
                Throw ex
            End Try


        End Function

        Public Function GetAssetData_List() As DataTable
            Try
                Return Me.GeneralExecuter(objAssetDetails.GetAssetData_List())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAssetData_RepItemList() As DataTable
            Try
                Return Me.GeneralExecuter(objAssetDetails.GetAssetData_RepItemList())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Asset_Search(ByVal objattAssetDetails As attAssetDetails, ByVal objattAssets As attItems, ByVal IncludeChild As Boolean, ByVal strDepartment As String) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.Asset_Search(objattAssets, IncludeChild, strDepartment))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Check_Child(ByVal _id As String, ByVal formid As Integer) As String
            Try
                Return Me.GeneralExecuter_Scalar(objAssetDetails.Check_Child(_id, formid), "")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Dispose_Asset(ByVal objattAssetDetails As attAssetDetails) As Boolean
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Me.GeneralExecuter(objAssetDetails.Dispose_Asset())
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function SetDisposedAsset(ByVal objattAssetDetails As attAssetDetails) As Boolean
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Me.GeneralExecuter(objAssetDetails.SetDisposedAsset())
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        'Public Function Dispose_AssetByID(ByVal astID As String, ByVal dispDate As String) As Boolean
        '    Try
        '        Me.GeneralExecuter(objAssetDetails.Dispose_AssetByID(astID, dispDate))
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        'End Function

        Public Function IsAssetDisposed(ByVal AstID As String) As Boolean
            Try
                Dim str As String = Me.GeneralExecuter_Scalar(objAssetDetails.IsDisposed(AstID), "")
                If str > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Check_referenceID(ByVal _id As String, ByVal _AstID As String) As Boolean
            Try
                Dim ds As DataTable = Me.GeneralExecuter(objAssetDetails.Check_ReferenceID(_id, _AstID))
                If Not ds Is Nothing Then
                    If ds.Rows.Count > 0 Then
                        Return True
                    End If
                End If
                Return False
            Catch ex As Exception
                Throw ex
            End Try


        End Function

        Public Function Update_Location(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.Update_Location())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Update_Custodian(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.Update_Custodian())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Update_BarCode(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.Update_Barcode())
            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Public Function Update_Status(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.Update_Status())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Update_Brand(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.Update_Brand())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Update_InvStatus(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.Update_InvStatus())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateAssetSerialNumber(ByVal objattAssetDetails As attAssetDetails) As Boolean
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Me.GeneralExecuter(objAssetDetails.UpdateAssetSerialNumber())
                Return True
            Catch ex As Exception
                Return False
                'Throw ex
            End Try
        End Function

        Public Function GetAstIDBySerialNumlike(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.GetAstIDBySerialNumLike)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetAstIDBySerialNum(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.GetAstIDBySerialNum)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAstIDBySerialNumAndItemCode(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.GetAstIDBySerialNumAndItemCode)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAssetStatus(ByVal objattAssetDetails As attAssetDetails) As String
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter_Scalar(objAssetDetails.Get_Status(), "")
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function Update_DefaultBook(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.Update_DefaultBook())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAllCompanyAssetsIDs(ByVal CompanyID As Integer) As DataTable
            Try
                Return Me.GeneralExecuter(objAssetDetails.GetCompanyAssetsIDs(CompanyID))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Delete_Details(ByVal objattAssetDetails As attAssetDetails) As Boolean
            Try
                'Deleting bookHistory
                Dim objattBookHistory As New attBookHistory
                Dim objBookHistory As New BALBookHistory
                objattBookHistory.ASTID = objattAssetDetails.PKeyCode
                objBookHistory.Delete_BookHistory(objattBookHistory)

                'Deleting AstBooks
                Dim objattAstBooks As New attAstBooks
                objattAstBooks.AstID = objattAssetDetails.PKeyCode
                Dim objAstBooks As New BALAstBooks
                objAstBooks.Delete_AstBooks(objattAstBooks)

                'Delete Ast_Cust_history
                Dim objattAst_Cust_history As New attAst_Cust_history
                objattAst_Cust_history.AstID = objattAssetDetails.PKeyCode
                Dim objAst_Cust_history As New BALAst_Cust_history
                objAst_Cust_history.Delete_Ast_Cust_history(objattAst_Cust_history)

                'Delete AstHistory
                Dim objattAstHistory As New attAstHistory
                objattAstHistory.AstID = objattAssetDetails.PKeyCode
                Dim objBALAst_History As New BALAst_History
                objBALAst_History.delete_Ast_History(objattAstHistory)

                'delete Addtional cost history
                Dim objattAddcostHistory As New AttAddCostHistory
                objattAddcostHistory.AstID = objattAssetDetails.PKeyCode
                Dim objBAlAddCostHistory As New BALAddCostHistory
                objBAlAddCostHistory.Delete_AddCostHistoryByAstID(objattAddcostHistory)

                Dim objattAssetWarrnty As New attAssetWarranty
                objattAssetWarrnty.AstID = objattAssetDetails.PKeyCode
                Dim objBAlAssetWarrnty As New BALAssetWarranty
                objBAlAssetWarrnty.Delete_AssetWarrantyByAstID(objattAssetWarrnty)


                objAssetDetails.Attribute = objattAssetDetails
                Me.Delete(objAssetDetails)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function



        Public Function Update_LabelCount(ByVal AstId As String) As Boolean
            Try
                Me.GeneralExecuter(objAssetDetails.UpdateLabelCount(AstId))
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function




        Public Function SetAssetWithValue(ByVal objattAssetDetails As attAssetDetails) As Boolean
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Me.GeneralExecuter(objAssetDetails.SetAssetWithValue())
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function SetCapitalizationDateValue(ByVal objattAssetDetails As attAssetDetails) As Boolean
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Me.GeneralExecuter(objAssetDetails.SetCapitalizationDate())
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
   

        Public Function AssetsDetailReport(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.AssetsDetailReport())
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        '======================
        Public Function InterCoTranReport(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.InterCoTranReport())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function SoldAssetsReport(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                objAssetDetails.Attribute = objattAssetDetails
                Return Me.GeneralExecuter(objAssetDetails.SoldAssetsReport())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        'Public Function DamageAssetsReport(ByVal objattAssetDetails As attAssetDetails) As DataTable
        '    Try
        '        objAssetDetails.Attribute = objattAssetDetails
        '        Return Me.GeneralExecuter(objAssetDetails.DamageAssetsReport())
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        'End Function

        'Public Function DisposeAssetReport(ByVal objattAssetDetails As attAssetDetails) As DataTable
        '    Try
        '        objAssetDetails.Attribute = objattAssetDetails
        '        Return Me.GeneralExecuter(objAssetDetails.DisposeAssetReport())
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        'End Function


        Public Function Depreciation_Book_temp(ByVal forTemp1 As String, ByVal strCompanyID As String, ByVal strBookId As String, ByVal strCustID As String, ByVal strLocId As String, ByVal strCatID As String, ByVal IncludeChild As Boolean, ByVal BrandID As String, ByVal SupplierID As String) As DataTable
            Try
                Return Me.GeneralExecuter(objAssetDetails.Depreciation_Book_temp(forTemp1, strCompanyID, strBookId, strCustID, strLocId, strCatID, IncludeChild, BrandID, SupplierID))
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        '**********************************************
        '   Methods for calculating division and category for Hada

        Public Function Get_Cust_DeptID(ByVal CustID As String) As String
            Try
                Dim ds As New DataTable
                Dim objattCustodian As New attCustodian
                Dim objBALCustodian As New BALCustodian

                objattCustodian.PKeyCode = CustID
                ds = objBALCustodian.GetAll_Custodian(objattCustodian)
                If ds IsNot Nothing Then
                    Dim deptID As String = CStr(ds.Rows(0)("DeptID"))
                    Dim DeptIDSplit() As String = deptID.Split("-"c)
                    Return Format(CInt(DeptIDSplit(0)), "0#") & DeptIDSplit(1)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Get_Assets_AstCatID(ByVal itemCode As String) As String
            Try
                Dim ds As New DataTable
                Dim objattAssets As New attItems
                Dim objBALAssets As New BALItems

                objattAssets.PKeyCode = itemCode
                ds = objBALAssets.GetAll_Items(objattAssets)
                If ds Is Nothing = False Then
                    'If ds.Tables.Count > 0 Then
                    If ds.Rows.Count > 0 Then
                        Return ds.Rows(0)("AstCatID").ToString
                    Else
                        Return Nothing
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return Nothing
        End Function

        Public Function Check_AstID(ByVal AssetID As String, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("AstID", AssetID, "AssetDetails", IsInsertStatus, "AstID", AssetID)
        End Function

        'Public Function Check_AstID(ByVal _id As String, ByVal IsInsert As Boolean) As Boolean
        '    Try

        '        Dim ds As New DataTable
        '        Dim objattAssetDetails As New attAssetDetails
        '        objattAssetDetails.PKeyCode = _id
        '        ds = GetAllData_GetCombo(objattAssetDetails)
        '        If ds Is Nothing = False Then
        '            If ds.Rows.Count > 0 Then
        '                Return True
        '            End If
        '        End If
        '        Return False
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        'End Function

        'Get New Asset ID from the AssetDetails Table.
        Public Function Generate_AssetID() As String
            Dim str As String = ""
            Try
                Do
                    str = Format(DateTime.Now, "yyddMMHHmmssff")
                Loop Until Not Check_AstID(str, True)

            Catch ex As Exception
                Throw ex
            End Try
            Return str
        End Function

        Public Sub Add_AstBooks(ByVal AstID As String, ByVal Cost As Double, ByVal CompanyID As Integer, ByVal salVal As Double, ByVal SalYr As Integer, ByVal SalMonth As Integer, ByVal dtSrv As Date, ByVal IsSalvageValuePercent As Boolean)
            Dim objattBook As New attBook
            Dim objBALBook As New BALBooks
            Dim objattAstBooks As New attAstBooks
            Dim objBALAstBooks As New BALAstBooks
            Dim ds As DataTable
            objattBook.CompanyID = CompanyID
            ds = objBALBook.GetAll_Book(objattBook)
            If Not ds Is Nothing Then
                For Each dr As DataRow In ds.Rows
                    objattAstBooks = New attAstBooks
                    objattAstBooks.PKeyCode = dr("BookID")
                    objattAstBooks.AstID = AstID
                    objattAstBooks.DepCode = dr("DepCode")
                    objattAstBooks.BookDescription = dr("Description")
                    objattAstBooks.LastBookValue = 0.0
                    objattAstBooks.SalvageYear = SalYr
                    objattAstBooks.SalvageMonth = SalMonth

                    If IsSalvageValuePercent Then
                        objattAstBooks.SalvageValuePercent = salVal
                        objattAstBooks.SalvageValue = (CDbl(Cost) * CDbl(salVal)) / 100
                    Else
                        If Cost > 0 Then
                            objattAstBooks.SalvageValuePercent = Math.Round((salVal / Cost) * 100, 2)
                        Else
                            objattAstBooks.SalvageValuePercent = 0
                        End If
                        objattAstBooks.SalvageValue = salVal
                    End If

                    objattAstBooks.CurrentBookValue = Cost
                    objattAstBooks.BVUpdate = dtSrv
                    If Not Check_BookExist(objattAstBooks.PKeyCode, objattAstBooks.AstID) Then
                        objBALAstBooks.Insert_AstBooks(objattAstBooks)
                    End If
                Next
            End If
        End Sub

        Public Sub Update_AstBooks(ByVal AstID As String, ByVal Cost As Double, ByVal CompanyID As Integer, ByVal salVal As Double, ByVal SalYr As Integer, ByVal SalMonth As Integer, ByVal dtSrv As Date, ByVal IsSalvageValuePercent As Boolean)
            Dim objattBookTemp As New attBook
            Dim objBALBookTemp As New BALBooks
            Dim objattAstBooks As New attAstBooks
            Dim objBALAstBooks As New BALAstBooks
            Dim ds As DataTable
            objattBookTemp.CompanyID = CompanyID
            ds = objBALBookTemp.GetAll_Book(objattBookTemp)
            If Not ds Is Nothing Then
                For Each dr As DataRow In ds.Rows
                    objattAstBooks = New attAstBooks
                    objattAstBooks.PKeyCode = dr("BookID")
                    objattAstBooks.AstID = AstID
                    objattAstBooks.DepCode = dr("DepCode")
                    objattAstBooks.BookDescription = dr("Description")
                    objattAstBooks.LastBookValue = 0.0

                    objattAstBooks.SalvageYear = SalYr
                    objattAstBooks.SalvageMonth = SalMonth
                    If IsSalvageValuePercent Then
                        objattAstBooks.SalvageValuePercent = salVal
                        objattAstBooks.SalvageValue = (CDbl(Cost) * CDbl(salVal)) / 100
                    Else
                        If Cost > 0 Then
                            objattAstBooks.SalvageValuePercent = Math.Round((salVal / Cost) * 100, 2)
                        Else
                            objattAstBooks.SalvageValuePercent = 0
                        End If
                        objattAstBooks.SalvageValue = salVal
                    End If
                    objattAstBooks.CurrentBookValue = Cost
                    objattAstBooks.BVUpdate = dtSrv
                    If Check_BookExist(objattAstBooks.PKeyCode, objattAstBooks.AstID) Then
                        objBALAstBooks.Update_AstBooks(objattAstBooks)
                    End If
                Next
            End If
        End Sub


        Private Function Check_BookExist(ByVal _id As String, ByVal _Astid As String) As Boolean
            Try
                Dim objBALAstBooks As New BALAstBooks
                Dim ds As New DataTable
                Dim objattAstBooks1 As New attAstBooks
                objattAstBooks1.PKeyCode = _id
                objattAstBooks1.AstID = _Astid
                ds = objBALAstBooks.CheckID_AstBooks(objattAstBooks1)
                If ds Is Nothing = False Then
                    If ds.Rows.Count > 0 Then
                        Return True
                    Else
                        Return False
                    End If

                Else
                    Return False
                End If

                Return False
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub DisposeAsset_Book(ByVal _id As String)
            Try
                Dim objattAstBooks As attAstBooks
                Dim objBALAstBooks As New BALAstBooks

                Dim ds As New DataTable
                Dim BL As BALAstBooks
                objattAstBooks = New attAstBooks
                objattAstBooks.AstID = _id
                ds = objBALAstBooks.GetAllData_Detail(objattAstBooks)
                If ds Is Nothing = False Then
                    For Each dr As DataRow In ds.Rows
                        BL = New BALAstBooks
                        BL.MarkAssetDisposed(dr("CurrentBV"), dr("BookId"), dr("AstID"))
                    Next
                Else
                End If

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function GetLocationAssets(ByVal LocID As String, ByVal InvSchID As String) As DataTable
            Try
                Return Me.GeneralExecuter(objAssetDetails.GetLocationAssets(LocID, InvSchID))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAssetInventoryStatus(ByVal BarCode As String, ByVal InvSchID As String) As DataTable
            Try
                Return Me.GeneralExecuter(objAssetDetails.Get_AssetInvetnoryStatus(BarCode, InvSchID))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DisableAssetTriggers() As DataTable
            Try
                Return Me.GeneralExecuter(objAssetDetails.DisableAssetTriggers())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function EnabbleAssetTriggers() As DataTable
            Try
                Return Me.GeneralExecuter(objAssetDetails.EnableAssetTriggers())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
    End Class

End Namespace