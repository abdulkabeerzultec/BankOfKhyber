Imports System
Imports System.Data.OleDb
Imports System.Configuration.ConfigurationSettings
Imports System.Windows.Forms
Imports System.Text
Namespace ZulAssetsDAL
    Public Class TransDataTemp
        Dim objDBConnection As New DBConnection


        Public Sub Export_Data_To_Temp(ByVal iDeviceID As Integer, ByVal LocID As String, ByVal IsMultipleLocations As Boolean, ByVal Selection() As Boolean, ByVal BusinessArea As String, ByVal Categories As String, ByVal EvaluationGroup1 As String, ByVal CostCenter As String)
            Try
                Dim ConnTemp As SqlClient.SqlConnection
                Dim Conn As OleDbConnection
                ConnTemp = objDBConnection.GetSQLConnection_Temp()
                Conn = objDBConnection.GetConnection()
                If ConnTemp.State = ConnectionState.Closed Then
                    ConnTemp.Open()
                End If
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                If ConnTemp.State = ConnectionState.Open And Conn.State = ConnectionState.Open Then
                    'Refereshing the TempTables
                    Dim CmdDeleteTemp As New SqlClient.SqlCommand
                    Dim Cmd As New OleDbCommand
                    Dim Da As OleDbDataAdapter
                    Dim Dt As New DataTable
                    Dim strQuery As New StringBuilder

                    CmdDeleteTemp.Connection = ConnTemp
                    Cmd.Connection = Conn
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Export Company Table:
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    CmdDeleteTemp.CommandText = "truncate table CompanyTemp"
                    CmdDeleteTemp.ExecuteNonQuery()
                    Dt = New DataTable
                    'Export Location Table
                    Cmd.CommandText = "SELECT CompanyId,CompanyCode,CompanyName,BarStructID,HierCode,LastAssetNumber  FROM Companies where Isdeleted = 0"
                    Da = New OleDbDataAdapter(Cmd)
                    Da.Fill(Dt)
                    For Each Dr As DataRow In Dt.Rows
                        strQuery = New StringBuilder
                        strQuery.Append("insert into CompanyTemp")
                        strQuery.Append(" (CompanyId,CompanyCode,CompanyName,BarStructID,HierCode,LastAssetNumber)")
                        strQuery.Append(" Values(@CompanyId,@CompanyCode,@CompanyName,@BarStructID,@HierCode,@LastAssetNumber)")
                        CmdDeleteTemp.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Dr(0)
                        CmdDeleteTemp.Parameters.Add("@CompanyCode", SqlDbType.NVarChar).Value = Dr(1)
                        CmdDeleteTemp.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = Dr(2)
                        CmdDeleteTemp.Parameters.Add("@BarStructID", SqlDbType.Int).Value = Dr(3)
                        CmdDeleteTemp.Parameters.Add("@HierCode", SqlDbType.NVarChar).Value = Dr(4)
                        CmdDeleteTemp.Parameters.Add("@LastAssetNumber", SqlDbType.BigInt).Value = Dr(5)
                        CmdDeleteTemp.CommandText = strQuery.ToString()
                        CmdDeleteTemp.ExecuteNonQuery()
                        CmdDeleteTemp.Parameters.Clear()
                        strQuery = Nothing
                    Next

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Export Custodian Table
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    CmdDeleteTemp.CommandText = "truncate table CustodianTemp"
                    CmdDeleteTemp.ExecuteNonQuery() 'CustodianTemp
                    Dt = New DataTable
                    'Export Location Table
                    Cmd.CommandText = "Select CustodianID,CustodianName,DeptId from Custodian where Isdeleted = 0"
                    Da = New OleDbDataAdapter(Cmd)
                    Da.Fill(Dt)
                    For Each Dr As DataRow In Dt.Rows
                        strQuery = New StringBuilder
                        strQuery.Append("insert into CustodianTemp")
                        strQuery.Append(" (CustodianID,CustodianName,DeptId)")
                        strQuery.Append(" Values(@CustodianID,@CustodianName,@DeptId)")
                        CmdDeleteTemp.Parameters.Add("@CustodianID", SqlDbType.NVarChar).Value = Dr(0)
                        CmdDeleteTemp.Parameters.Add("@CustodianName", SqlDbType.NVarChar).Value = Dr(1)
                        CmdDeleteTemp.Parameters.Add("@DeptId", SqlDbType.NVarChar).Value = Dr(2)
                        CmdDeleteTemp.CommandText = strQuery.ToString()
                        CmdDeleteTemp.ExecuteNonQuery()
                        CmdDeleteTemp.Parameters.Clear()
                        strQuery = Nothing
                    Next

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Export CostCenter Table
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    CmdDeleteTemp.CommandText = "truncate table CostCenterTemp"
                    CmdDeleteTemp.ExecuteNonQuery() 'CostCenterTemp
                    Dt = New DataTable


                    Cmd.CommandText = "Select CostID,CostNumber,CostName,CompanyId from CostCenter where Isdeleted = 0"
                    If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                        Cmd.CommandText &= " and  CostCenter.CompanyID IN (" & AppConfig.CompanyIDS & ")"
                    End If

                    Da = New OleDbDataAdapter(Cmd)
                    Da.Fill(Dt)
                    For Each Dr As DataRow In Dt.Rows
                        strQuery = New StringBuilder
                        strQuery.Append("insert into CostCenterTemp")
                        strQuery.Append(" (CostID,CostNumber,CostName,CompanyId )")
                        strQuery.Append(" Values(@CostID,@CostNumber,@CostName,@CompanyId )")
                        CmdDeleteTemp.Parameters.Add("@CostID", SqlDbType.NVarChar).Value = Dr(0)
                        CmdDeleteTemp.Parameters.Add("@CostNumber", SqlDbType.NVarChar).Value = Dr(1)
                        CmdDeleteTemp.Parameters.Add("@CostName", SqlDbType.NVarChar).Value = Dr(2)
                        CmdDeleteTemp.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Dr(3)
                        CmdDeleteTemp.CommandText = strQuery.ToString()
                        CmdDeleteTemp.ExecuteNonQuery()
                        CmdDeleteTemp.Parameters.Clear()
                        strQuery = Nothing
                    Next
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    CmdDeleteTemp.CommandText = "truncate table AssetTransferTemp"
                    CmdDeleteTemp.ExecuteNonQuery()
                    '0 - Assets
                    '1 - Users
                    '2 - Locations
                    '3 - Categories

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Export Location Table
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    If Selection(2) Then    '2 - Locations

                        CmdDeleteTemp.CommandText = "truncate table LocationTemp"
                        CmdDeleteTemp.ExecuteNonQuery()

                        Dt = New DataTable
                        'Export Location Table
                        Cmd.CommandText = "Select ID1,LocID,LocDesc,Isdeleted,locLevel from Location  where Isdeleted = 0"
                        If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                            Cmd.CommandText += " and  (Location.CompanyID IN (" & AppConfig.CompanyIDS & ") Or Location.CompanyID is null or Location.CompanyID = -1)"
                        End If

                        Da = New OleDbDataAdapter(Cmd)
                        Da.Fill(Dt)
                        For Each Dr As DataRow In Dt.Rows
                            strQuery = New StringBuilder
                            strQuery.Append("insert into LocationTemp")
                            strQuery.Append(" (ID1,LocID,LocDesc,Isdeleted,locLevel)")
                            strQuery.Append(" Values(@ID1,@LocID,@LocDesc,@Isdeleted,@locLevel)")
                            CmdDeleteTemp.Parameters.Add("@ID1", SqlDbType.BigInt).Value = Dr(0)
                            CmdDeleteTemp.Parameters.Add("@LocID", SqlDbType.NVarChar).Value = Dr(1)
                            CmdDeleteTemp.Parameters.Add("@LocDesc", SqlDbType.NVarChar).Value = Dr(2)
                            CmdDeleteTemp.Parameters.Add("@Isdeleted", SqlDbType.Bit).Value = False
                            CmdDeleteTemp.Parameters.Add("@locLevel", SqlDbType.Int).Value = Dr(4)
                            CmdDeleteTemp.CommandText = strQuery.ToString()
                            CmdDeleteTemp.ExecuteNonQuery()
                            CmdDeleteTemp.Parameters.Clear()
                            strQuery = Nothing
                        Next

                    End If
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Export Categories Table
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    If Selection(3) Then    '3 - Categories

                        CmdDeleteTemp.CommandText = "truncate table CategoryTemp"
                        CmdDeleteTemp.ExecuteNonQuery()

                        Dt = New DataTable
                        'Export Category Table
                        Cmd.CommandText = "Select Id1,AstCatID,AstCatDesc,Isdeleted,catLevel from Category where Isdeleted = 0"
                        Da = New OleDbDataAdapter(Cmd)
                        Da.Fill(Dt)
                        For Each Dr As DataRow In Dt.Rows
                            strQuery = New StringBuilder
                            strQuery.Append("insert into CategoryTemp")
                            strQuery.Append(" (Id1,AstCatID,AstCatDesc,Isdeleted,catLevel)")
                            strQuery.Append(" Values(@Id1,@AstCatID,@AstCatDesc,@Isdeleted,@catLevel)")
                            CmdDeleteTemp.Parameters.Add("@Id1", SqlDbType.NVarChar).Value = Dr(0)
                            CmdDeleteTemp.Parameters.Add("@AstCatID", SqlDbType.NVarChar).Value = Dr(1)
                            CmdDeleteTemp.Parameters.Add("@AstCatDesc", SqlDbType.NVarChar).Value = Dr(2)
                            CmdDeleteTemp.Parameters.Add("@Isdeleted", SqlDbType.Bit).Value = False
                            CmdDeleteTemp.Parameters.Add("@catLevel", SqlDbType.Int).Value = Dr(4)
                            CmdDeleteTemp.CommandText = strQuery.ToString()
                            CmdDeleteTemp.ExecuteNonQuery()
                            CmdDeleteTemp.Parameters.Clear()
                            strQuery = Nothing
                        Next
                    End If

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Export Users Table
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    If Selection(1) Then '1 - Users

                        CmdDeleteTemp.CommandText = "truncate table AppusersTemp"
                        CmdDeleteTemp.ExecuteNonQuery()

                        Dt = New DataTable
                        'Export AppUsers

                        Cmd.CommandText = "Select AppUsers.LoginName,AppUsers.Password,Roles.AssetsBooks as IsAllowAssetAudit,Roles.AssetItems as IsAllowInventory from AppUsers inner join Roles on Roles.RoleID = AppUsers.RoleID where AppUsers.Isdeleted = 0"
                        Da = New OleDbDataAdapter(Cmd)
                        Da.Fill(Dt)
                        For Each Dr As DataRow In Dt.Rows
                            strQuery = New StringBuilder
                            strQuery.Append("insert into AppUsersTemp")
                            strQuery.Append(" (LoginName,[Password],IsAllowAssetAudit,IsAllowInventory)")
                            strQuery.Append(" Values(@LoginName,@strPass,@IsAllowAssetAudit,@IsAllowInventory)")
                            CmdDeleteTemp.Parameters.Add("@LoginName", SqlDbType.NVarChar).Value = Dr(0)
                            CmdDeleteTemp.Parameters.Add("@strPass", SqlDbType.NVarChar).Value = Dr(1)
                            CmdDeleteTemp.Parameters.Add("@IsAllowAssetAudit", SqlDbType.NVarChar).Value = Dr(2)
                            CmdDeleteTemp.Parameters.Add("@IsAllowInventory", SqlDbType.NVarChar).Value = Dr(3)
                            CmdDeleteTemp.CommandText = strQuery.ToString()
                            CmdDeleteTemp.ExecuteNonQuery()
                            CmdDeleteTemp.Parameters.Clear()
                            strQuery = Nothing
                        Next
                    End If

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Export Assets Table
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    If Selection(0) Then    '0 - Assets

                        CmdDeleteTemp.CommandText = "truncate table AssetsTemp"
                        CmdDeleteTemp.ExecuteNonQuery()

                        Dt = New DataTable
                        'Export Category Table
                        strQuery = New StringBuilder
                        'we should not send the Disposed assets to devices.
                        strQuery.Append(" select ? as DeviceID,AssetDetails.AstID as AstID")

                        If AppConfig.DescForRpt = "Item Description" Then
                            strQuery.Append(",Assets.AstDesc")
                        ElseIf AppConfig.DescForRpt = "Asset Description 1" Then
                            strQuery.Append(",AssetDetails.AstDesc as AstDesc")
                        ElseIf AppConfig.DescForRpt = "Asset Description 2" Then
                            strQuery.Append(",AssetDetails.AstDesc2 as AstDesc")
                        Else
                            strQuery.Append(",Assets.AstDesc")
                        End If
                        strQuery.Append(",AssetDetails.LocID as LocID,Assets.AstCatID as AstCatID,AssetDetails.NoPiece as Pieces,AssetDetails.BarCode as BarCode, 0 as Status,InventoryNumber,CostNumber as CostCenter,CustodianID,RefNo,SerailNo,InStockAsset,0 as IsDataChanged,AssetDetails.CustomFld1,AssetDetails.CustomFld2,AssetDetails.CustomFld3,AssetDetails.CustomFld4,AssetDetails.CustomFld5 from Assets ")

                        strQuery.Append(" inner join(AssetDetails left outer join Location on AssetDetails.LocID = Location.LocID ) ")
                        strQuery.Append(" on Assets.ItemCode = AssetDetails.ItemCode left outer join CostCenter on CostCenter.CostID = AssetDetails.CostCenterID inner join Category on Category.AstCatID = Assets.AstCatID where AssetDetails.IsDeleted = 0 and AssetDetails.Disposed <> 1")
                        If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                            strQuery.Append(" and  AssetDetails.CompanyID IN (" & AppConfig.CompanyIDS & ")")
                        End If

                        Cmd.Parameters.Add(New OleDbParameter("@iDeviceID", iDeviceID))
                        If Not String.IsNullOrEmpty(LocID) Then
                            If IsMultipleLocations Then
                                strQuery.Append(String.Format(" and  Location.LocDesc in({0})", LocID))
                            Else
                                strQuery.Append(" and (AssetDetails.LocId like '" & LocID & "' or AssetDetails.LocId like '" & LocID & "-%' )")
                                'strQuery.Append(" and  AssetDetails.LocID Like ?")
                                'Cmd.Parameters.Add(New OleDbParameter("@LocID", LocID & "%"))
                            End If
                        End If

                        If Not String.IsNullOrEmpty(BusinessArea) Then
                            strQuery.Append(String.Format(" and  AssetDetails.BussinessArea in({0})", BusinessArea))
                        End If

                        If Not String.IsNullOrEmpty(Categories) Then
                            strQuery.Append(" and (Assets.AstCatID like '" & Categories & "' or Assets.AstCatID like '" & Categories & "-%')")
                            'strQuery.Append(" and  Assets.AstCatID Like ?")
                            'Cmd.Parameters.Add(New OleDbParameter("@AstCatID", Categories & "%"))
                        End If
                        'If Not String.IsNullOrEmpty(Categories) Then
                        '    strQuery.Append(String.Format(" and  Category.AstCatDesc in({0})", Categories))
                        'End If

                        If Not String.IsNullOrEmpty(EvaluationGroup1) Then
                            strQuery.Append(String.Format(" and  AssetDetails.EvaluationGroup1 in({0})", EvaluationGroup1))
                        End If

                        If Not String.IsNullOrEmpty(CostCenter) Then
                            strQuery.Append(String.Format(" and  CostCenter.CostName in({0})", CostCenter))
                        End If


                        Cmd.CommandText = strQuery.ToString()

                        ' Using BulkCopy
                        Dim reader As OleDbDataReader
                        reader = Cmd.ExecuteReader()
                        Cmd.Parameters.Clear()
                        Dim bulkCopy As New SqlClient.SqlBulkCopy(ConnTemp)
                        bulkCopy.BulkCopyTimeout = 300
                        bulkCopy.DestinationTableName = "AssetsTemp"
                        bulkCopy.ColumnMappings.Add("DeviceID", "DeviceID")
                        bulkCopy.ColumnMappings.Add("AstID", "AstID")
                        bulkCopy.ColumnMappings.Add("AstDesc", "AstDesc")
                        bulkCopy.ColumnMappings.Add("AstCatID", "AstCatID")
                        bulkCopy.ColumnMappings.Add("LocID", "LocID")
                        bulkCopy.ColumnMappings.Add("Pieces", "Pieces")
                        bulkCopy.ColumnMappings.Add("BarCode", "BarCode")
                        bulkCopy.ColumnMappings.Add("Status", "Status")

                        bulkCopy.ColumnMappings.Add("InventoryNumber", "InventoryNumber")
                        bulkCopy.ColumnMappings.Add("CustodianID", "CustodianID")
                        bulkCopy.ColumnMappings.Add("RefNo", "RefNo")
                        bulkCopy.ColumnMappings.Add("SerailNo", "SerailNo")
                        bulkCopy.ColumnMappings.Add("CostCenter", "CostCenter")
                        bulkCopy.ColumnMappings.Add("InStockAsset", "InStockAsset")
                        bulkCopy.ColumnMappings.Add("IsDataChanged", "IsDataChanged")


                        bulkCopy.ColumnMappings.Add("CustomFld1", "CustomFld1")
                        bulkCopy.ColumnMappings.Add("CustomFld2", "CustomFld2")
                        bulkCopy.ColumnMappings.Add("CustomFld3", "CustomFld3")
                        bulkCopy.ColumnMappings.Add("CustomFld4", "CustomFld4")
                        bulkCopy.ColumnMappings.Add("CustomFld5", "CustomFld5")

                        bulkCopy.WriteToServer(reader)
                        reader.Close()
                        strQuery = Nothing
                    End If
                    '''''''''''''''''''''''''''
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function GetAll_AssetTransferTemp(ByVal strAstID As String) As DataTable
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim ConnTemp As OleDbConnection
            Dim Da As OleDbDataAdapter
            Dim dt As New DataTable
            ConnTemp = objDBConnection.GetConnection_Temp()
            objCommand.Connection = ConnTemp
            Dim strQuery As New StringBuilder
            strQuery.Append("select * from AssetTransferTemp where AstTransId = (Select max(AstTransID) from AssetTransferTemp where AstId = '" & strAstID & " ')")
            objCommand.CommandText = strQuery.ToString()
            Da = New OleDbDataAdapter(objCommand)
            Da.Fill(dt)
            Return dt

        End Function

        Public Sub RefereshTables(ByVal strDevice As Integer)
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim ConnTemp As OleDbConnection

            'Dim Da As OleDbDataAdapter
            Dim dt As New DataTable
            ConnTemp = objDBConnection.GetConnection_Temp()
            objCommand.Connection = ConnTemp
            Dim strQuery As New StringBuilder
            strQuery.Append("delete from AssetsTemp where DeviceID=" & strDevice & "")
            objCommand.CommandText = strQuery.ToString()
            objCommand.ExecuteNonQuery()

            objCommand = New OleDbCommand
            strQuery = New StringBuilder
            strQuery.Append("delete from AssetTransferTemp")
            objCommand.CommandText = strQuery.ToString()
            objCommand.Connection = ConnTemp
            objCommand.ExecuteNonQuery()
        End Sub

        Public Function GetAll_AssetsTemp(ByVal strDevice As String) As DataTable
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim ConnTemp As OleDbConnection

            Dim Da As OleDbDataAdapter
            Dim dt As New DataTable
            ConnTemp = objDBConnection.GetConnection_Temp()
            objCommand.Connection = ConnTemp
            Dim strQuery As New StringBuilder
            ' where Status <> 0, Because we don't want to move the assets marked as missing to the backend, the default is missing so no need to move it to the backend to speed up the sync
            strQuery.Append("select * from AssetsTemp where Status <> 0 and DeviceID=" & strDevice & "")
            objCommand.CommandText = strQuery.ToString()
            Da = New OleDbDataAdapter(objCommand)
            Da.Fill(Dt)
            Return dt
        End Function

        Public Function GetAll_AssetsTempGrid() As DataTable
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim ConnTemp As OleDbConnection

            Dim Da As OleDbDataAdapter
            Dim dt As New DataTable
            ConnTemp = objDBConnection.GetConnection_Temp()
            objCommand.Connection = ConnTemp
            Dim strQuery As New StringBuilder
            strQuery.Append("select AssetsTempReceiving.AstID,AstDesc,AssetsTempReceiving.DeviceID,LocID,ToLoc,FromLoc,AstCatID,Status,Pieces ,SerailNo,CustodianID,InventoryDate,IsDataChanged,AstTransID,CostCenter,InventoryNumber as Remarks, AssetStatus from AssetsTempReceiving")
            strQuery.Append(" left outer join AssetTransferTemp on AssetTransferTemp.AstID = AssetsTempReceiving.AstID")
            objCommand.CommandText = strQuery.ToString()
            Da = New OleDbDataAdapter(objCommand)
            Da.Fill(dt)
            Return dt
        End Function

        Public Function DeleteAssetsTempReceiving(ByVal AstID As String, ByVal DeviceID As String) As Integer
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim ConnTemp As OleDbConnection

            ConnTemp = objDBConnection.GetConnection_Temp()
            objCommand.Connection = ConnTemp
            Dim strQuery As New StringBuilder
            strQuery.Append("Delete from AssetsTempReceiving where AstID=? and DeviceID=?")
            objCommand.Parameters.Add(New OleDbParameter("@AstID", AstID))
            objCommand.Parameters.Add(New OleDbParameter("@DeviceID", DeviceID))
            objCommand.CommandText = strQuery.ToString()
            Return objCommand.ExecuteNonQuery()
        End Function
        Public Function DeleteAssetTransferTemp(ByVal AstTransID As String, ByVal DeviceID As String) As Integer
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim ConnTemp As OleDbConnection

            ConnTemp = objDBConnection.GetConnection_Temp()
            objCommand.Connection = ConnTemp
            Dim strQuery As New StringBuilder
            strQuery.Append("Delete from AssetTransferTemp where AstTransID=? and DeviceID=?")
            objCommand.Parameters.Add(New OleDbParameter("@AstTransID", AstTransID))
            objCommand.Parameters.Add(New OleDbParameter("@DeviceID", DeviceID))
            objCommand.CommandText = strQuery.ToString()
            Return objCommand.ExecuteNonQuery()
        End Function
    End Class
End Namespace

