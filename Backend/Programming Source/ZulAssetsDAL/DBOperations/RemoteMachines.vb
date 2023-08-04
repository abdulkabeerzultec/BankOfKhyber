Imports System.Text
Imports System.Data.SqlClient
Imports System.Windows.Forms

Namespace ZulAssetsDAL

    Public Class RemoteMachines
        Public Shared TotalCount As Integer = 0
        Public Shared TotalInserted As Integer = 0
        Public Shared TotalUpdated As Integer = 0
        Public Shared TotalIgnored As Integer = 0

        Private Shared RemoteConn As New SqlConnection
        Private Shared LocalConn As New SqlConnection

        Private Shared LocalTrans As SqlTransaction
        Private Shared RemoteTrans As SqlTransaction

        Private Shared Function GetSQLConnectionString(ByVal server As String, ByVal DatabaseName As String, ByVal port As String, ByVal username As String, ByVal password As String) As String
            If port <> "" Then
                Return "DATA SOURCE=" & server & "," & port & ";UID=" & username & ";PWD=" & password & ";Initial Catalog=" & DatabaseName & ";Connect Timeout=3"
            Else
                Return "DATA SOURCE=" & server & ";UID=" & username & ";PWD=" & password & ";Initial Catalog=" & DatabaseName & ";Connect Timeout=3"
            End If
        End Function

        Public Shared Function CheckConnectionString(ByVal server As String, ByVal port As String, ByVal username As String, ByVal password As String) As Boolean
            Try
                RemoteConn.ConnectionString = GetSQLConnectionString(server, "ZulAssetsBE", port, username, password)
                RemoteConn.Open()
                If RemoteConn.State = ConnectionState.Open Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "ZulAsset")
                Return False
            Finally
                If RemoteConn.State = ConnectionState.Open Then
                    RemoteConn.Close()
                End If
            End Try
        End Function


        Public Shared Function DatabaseVersionSame() As Boolean
            LocalConn.ConnectionString = GetSQLConnectionString(AppConfig.DbServer, AppConfig.DbName, AppConfig.DBSQLPort, AppConfig.DbUname, AppConfig.DbPass)
            LocalConn.Open()
            RemoteConn.Open()
            Try
                Dim objCommand As SqlCommand = New SqlCommand
                Dim strQuery As New StringBuilder
                strQuery = New StringBuilder
                strQuery.Append("Select Version from [DB Version]")
                objCommand.CommandText = strQuery.ToString()
                Dim RemoteVersion As String = ExecuteRemoteScalar(objCommand)
                Dim LocalVersion As String = ExecuteLocalScalar(objCommand)
                If LocalVersion = RemoteVersion Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            Finally
                If LocalConn.State = ConnectionState.Open Then
                    LocalConn.Close()
                End If
                If RemoteConn.State = ConnectionState.Open Then
                    RemoteConn.Close()
                End If
            End Try
        End Function
        Public Shared Function ExportData(ByVal MachineID As Integer, ByVal CompanyID As Integer) As Boolean
            LocalConn.ConnectionString = GetSQLConnectionString(AppConfig.DbServer, AppConfig.DbName, AppConfig.DBSQLPort, AppConfig.DbUname, AppConfig.DbPass)
            LocalConn.Open()
            RemoteConn.Open()

            RemoteTrans = RemoteConn.BeginTransaction
            'Dim NonExportTables As String = ",ReportsFiles,AstBooks,AssetDetails,Ast_Cust_history,Ast_History,AstDepHistory,AstBooks_temp,AssetsTemp,AssetTransferTemp,BookHistory,BookHistory_temp,Report_AssestsByCategroy,Report_AssetbySubCategory,Report_CompanyAssets,"
            Dim NonExportTables As String = ",ReportsFiles,Ast_Cust_history,Ast_History,AstDepHistory,AstBooks_temp,AssetsTemp,AssetTransferTemp,BookHistory,BookHistory_temp,Report_AssestsByCategroy,Report_AssetbySubCategory,Report_CompanyAssets,"

            Try
                Dim dt As DataTable = GetListOfRemoteTables()
                Dim objCommand As SqlCommand = New SqlCommand
                Dim strQuery As New StringBuilder

                For Each row As DataRow In dt.Rows
                    Dim TableName As String = row("Name")
                    'we need to delete all data from offline machine excluding ReportsFiles
                    If TableName <> "ReportsFiles" Then
                        TruncateRemoteTable(TableName)
                    End If
                    strQuery = New StringBuilder

                    If TableName = "AssetDetails" Then
                        'send only the assets related to the company of the offline machine.
                        strQuery.Append("Select * from [" & TableName & "] where companyid = " & CompanyID.ToString)
                    Else
                        strQuery.Append("Select * from [" & TableName & "]")
                    End If
                    objCommand.CommandText = strQuery.ToString()
                    Dim dtLocal As DataTable = ExecuteLocal(objCommand)
                    'we export all data excluding NonExportTables tables.
                    If Not NonExportTables.Contains("," & TableName & ",") Then
                        CopyData(dtLocal, "[" & TableName & "]", RemoteConn, RemoteTrans)
                    End If
                Next
                'Update sysSettings set the offline machine flag on the table
                strQuery = New StringBuilder
                strQuery.Append("update dbo.SysSettings set IsOfflineMachine = 1")
                objCommand.CommandText = strQuery.ToString()
                ExecuteRemote(objCommand)
                'Delete the other offlineMachines from the remote database, and keep the Exported one.
                strQuery = New StringBuilder
                strQuery.Append("Delete OfflineMachines where MachineID <> " & MachineID)
                objCommand.CommandText = strQuery.ToString()
                ExecuteRemote(objCommand)

                RemoteTrans.Commit()
                Return True
            Catch ex As Exception
                RemoteTrans.Rollback()
                Return False
            Finally
                If LocalConn.State = ConnectionState.Open Then
                    LocalConn.Close()
                End If
                If RemoteConn.State = ConnectionState.Open Then
                    RemoteConn.Close()
                End If
            End Try
        End Function

        Public Shared Function Generate_AssetID() As String
            Dim str As String = ""
            Try
                Do
                    str = Format(DateTime.Now, "yyddMMHHmmssff")
                Loop Until Not Check_AstID(str)

            Catch ex As Exception
                Throw ex
            End Try
            Return str
        End Function

        Public Shared Function Check_AstID(ByVal AstID As String) As Boolean

            Dim objCommand As SqlCommand = New SqlCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select Count(*) from AssetDetails where AstID = '" & AstID & "'")
            objCommand.CommandText = strQuery.ToString()
            If ExecuteLocalScalar(objCommand) > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Shared Function Check_AstNum(ByVal AstNum As String) As Boolean

            Dim objCommand As SqlCommand = New SqlCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select Count(*) from AssetDetails where AstNum = '" & AstNum & "'")
            objCommand.CommandText = strQuery.ToString()
            If ExecuteLocalScalar(objCommand) > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Shared Function ImportAssetData(ByVal destConnection As SqlConnection, ByVal trans As SqlTransaction, ByVal prTableProgress As ProgressBar, ByVal lblTableStatus As Label) As Boolean
            Dim strQuery As New StringBuilder
            Dim objCommand As SqlCommand = New SqlCommand
            strQuery = New StringBuilder
            strQuery.Append("SELECT AstID,DispCode,ItemCode,SuppID,POCode,InvNumber,CustodianID,BaseCost,Tax,SrvDate,PurDate,Disposed,DispDate,InvSchCode,BookID,InsID,LocID,InvStatus,IsDeleted,IsSold,Sel_Date,Sel_Price,SoldTo,RefNo,AstNum,AstBrandId,AstDesc,AstModel,CompanyID,TransRemarks,LabelCount,OldAssetID,Discount,NoPiece,BarCode,SerailNo,RefCode,Plate,Poerp,Capex,Grn,GLCode,PONumber,AstDesc2,CapitalizationDate,BussinessArea,InventoryNumber,CostCenterID,InStockAsset,EvaluationGroup1,EvaluationGroup2,EvaluationGroup3,EvaluationGroup4,CreatedBY,IsDataChanged,LastInventoryDate,LastEditDate,CreationDate,LastEditBY,CustomFld1,CustomFld2,CustomFld3,CustomFld4,CustomFld5,Warranty,StatusID  FROM AssetDetails")
            objCommand.CommandText = strQuery.ToString()
            Dim dtRemoteAssetDetails As DataTable = ExecuteRemote(objCommand)

            'strQuery = New StringBuilder
            'strQuery.Append("Select * from AstBooks")
            'objCommand.CommandText = strQuery.ToString()
            'Dim dtRemoteAssetBook As DataTable = ExecuteRemote(objCommand)

            'For Each row As DataRow In dtRemoteAssetDetails.Rows
            '    Dim AstID As String = row("AstID")
            '    Dim AstNum As String = row("AstNum")
            '    If Not Check_AstNum(AstNum) Then
            'Check if AstID repeated, if so then change AstID
            '        If Check_AstID(AstID) Then
            '            Dim NewAssetID As String = Generate_AssetID()
            '            row("AstID") = NewAssetID
            '            'Find the Row in AssetBook Table and change the AssetID as well
            '            Dim AstBookRow As DataRow = dtRemoteAssetBook.Select("AstID = '" & AstID & "'")(0)
            '            AstBookRow("AstID") = NewAssetID
            '        End If
            '    End If
            'Next

            Dim Result As Boolean = False
            Result = UpdateAssets(dtRemoteAssetDetails, destConnection, trans, "AssetDetails", "AstNum", "LastEditDate", prTableProgress, lblTableStatus)
            'add assetbook for the new assets 
            Dim query As String = "insert into AstBooks select Books.BookID,AstID,Books.DepCode,0,1,0,0,DATEADD(year,-10,Getdate()),0,0,0 from AssetDetails inner join Books on Books.CompanyID = AssetDetails.CompanyID where AstID not in(select AstID from  AstBooks)"
            objCommand.CommandText = query
            ExecuteLocal(objCommand)
            'Result = Result And UpdateAssets(dtRemoteAssetBook, destConnection, trans, "AstBooks", "AstID", "BVUpdate", prTableProgress, lblTableStatus)
            Return Result
            'CopyData(dtRemoteAssetDetails, "AssetDetails", destConnection, trans)
            'CopyData(dtRemoteAssetBook, "AstBooks", destConnection, trans)
            ' Return True
        End Function

        Public Shared Function UpdateAssets(ByVal PushDT As DataTable, ByVal Conn As SqlConnection, ByVal PushTransaction As SqlTransaction, ByVal tableName As String, ByVal PrimaryKeyName As String, ByVal CompareColumn As String, ByVal prTableStatus As ProgressBar, ByVal lblTableStatus As Label) As Boolean
            Dim SQLInsertcmd As New SqlCommand
            Dim SQLUpdatecmd As New SqlCommand
            Dim SQLcmdTrans As New SqlCommand
            SQLInsertcmd.Connection = Conn
            SQLUpdatecmd.Connection = Conn
            SQLcmdTrans.Connection = Conn

            Try
                prTableStatus.Value = 0
                prTableStatus.Maximum = PushDT.Rows.Count

                prTableStatus.Visible = True
                lblTableStatus.Visible = True

                lblTableStatus.Text = "0/0"
                TotalCount = PushDT.Rows.Count
                TotalInserted = 0
                TotalUpdated = 0
                TotalIgnored = 0

                SQLInsertcmd.Transaction = PushTransaction
                SQLUpdatecmd.Transaction = PushTransaction
                SQLcmdTrans.Transaction = PushTransaction

                SQLInsertcmd.CommandText = GetSQLInsertCommand(PushDT, tableName)
                SQLUpdatecmd.CommandText = GetSQLUpdateCommand(PushDT, tableName, PrimaryKeyName)
                SQLcmdTrans.CommandText = String.Format("Select {2} from {0} where {1}= @{1}", tableName, PrimaryKeyName, CompareColumn)

                SQLcmdTrans.Parameters.Add(String.Format("@{0}", PrimaryKeyName), SqlDbType.NVarChar)

                For Each row As DataRow In PushDT.Rows
                    Application.DoEvents()
                    SQLcmdTrans.Parameters(String.Format("@{0}", PrimaryKeyName)).Value = row(PrimaryKeyName)
                    Dim LastEditDate As Object = SQLcmdTrans.ExecuteScalar
                    'If record not found in the SQL Server database then insert, else update.
                    If String.IsNullOrEmpty(LastEditDate) Then 'insert query.

                        'check if astid repeated then change it, there might be rare cases that astid will repeat, because it's based on the time.
                        Dim AstID As String = row("AstID")
                        If Check_AstID(AstID) Then
                            Dim NewAssetID As String = Generate_AssetID()
                            row("AstID") = NewAssetID
                        End If

                        SQLInsertcmd.Parameters.Clear()
                        For i As Integer = 0 To PushDT.Columns.Count - 1
                            SQLInsertcmd.Parameters.AddWithValue(String.Format("@{0}", PushDT.Columns(i).ColumnName), row(i))
                        Next
                        SQLInsertcmd.ExecuteNonQuery()

                        TotalInserted += 1
                    Else 'Use update query
                        'Check if the record in the local database is newer than the record in SQL Server database.
                        SQLUpdatecmd.Parameters.Clear()
                        If DateTime.Parse(row(CompareColumn)) > DateTime.Parse(LastEditDate) Then
                            For i As Integer = 0 To PushDT.Columns.Count - 1
                                SQLUpdatecmd.Parameters.AddWithValue(String.Format("@{0}", PushDT.Columns(i).ColumnName), row(i))
                            Next
                            SQLUpdatecmd.ExecuteNonQuery()
                            TotalUpdated += 1
                        Else
                            TotalIgnored += 1
                        End If
                    End If
                    prTableStatus.Value += 1
                    lblTableStatus.Text = tableName & " (" & prTableStatus.Value.ToString & " / " & prTableStatus.Maximum & ")"
                Next
                Return True
            Catch ex As Exception
                Return False
            Finally
                SQLInsertcmd.Dispose()
                SQLcmdTrans.Dispose()
                prTableStatus.Visible = False
                lblTableStatus.Visible = False
            End Try

        End Function

        Public Shared Function GetSQLInsertCommand(ByVal objTable As DataTable, ByVal TableName As String) As String
            Dim CommandText As String = String.Empty
            Dim ColumnNames As String = String.Empty
            Dim ColumnParams As String = String.Empty

            For Each col As DataColumn In objTable.Columns
                ColumnNames &= String.Format("{0},", col.ColumnName)
                ColumnParams &= String.Format("@{0},", col.ColumnName)
            Next
            ColumnNames = ColumnNames.Remove(ColumnNames.Length - 1, 1)
            ColumnParams = ColumnParams.Remove(ColumnParams.Length - 1, 1)
            CommandText = String.Format("Insert into {0} ({1}) values({2})", TableName, ColumnNames, ColumnParams)
            Return CommandText
        End Function


        Public Shared Function GetSQLUpdateCommand(ByVal objTable As DataTable, ByVal TableName As String, ByVal PrimaryKeyName As String) As String
            Dim CommandText As String = String.Empty
            Dim ColumnNames As String = String.Empty

            For Each col As DataColumn In objTable.Columns
                'GUID Should be excluded from Update Set Query.
                If col.ColumnName <> "GUID" Then
                    ColumnNames &= String.Format("{0} = @{0},", col.ColumnName)
                End If
            Next
            ColumnNames = ColumnNames.Remove(ColumnNames.Length - 1, 1)
            CommandText = String.Format("update {0} set {1} where {2} = @{2}", TableName, ColumnNames, PrimaryKeyName)
            Return CommandText
        End Function


        Public Shared Function ImportData(ByVal MachineID As Integer, ByVal prTableProgress As ProgressBar, ByVal lblTableStatus As Label) As Boolean
            LocalConn.ConnectionString = GetSQLConnectionString(AppConfig.DbServer, AppConfig.DbName, AppConfig.DBSQLPort, AppConfig.DbUname, AppConfig.DbPass)
            LocalConn.Open()
            RemoteConn.Open()

            LocalTrans = LocalConn.BeginTransaction

            Dim strQuery As New StringBuilder
            Dim objCommand As SqlCommand = New SqlCommand
            Try
                If ImportAssetData(LocalConn, LocalTrans, prTableProgress, lblTableStatus) Then
                    TruncateRemoteTable("AssetDetails")
                    TruncateRemoteTable("AstBooks")

                    strQuery = New StringBuilder
                    strQuery.Append("Select LastAssetNumber from OfflineMachines where MachineID = " & MachineID)
                    objCommand.CommandText = strQuery.ToString()
                    Dim LastAssetNumber As Int64 = ExecuteRemoteScalar(objCommand)

                    strQuery = New StringBuilder
                    strQuery.Append("Update OfflineMachines set LastAssetNumber = " & LastAssetNumber & " where MachineID = " & MachineID)
                    objCommand.CommandText = strQuery.ToString()
                    ExecuteLocal(objCommand)


                    LocalTrans.Commit()
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                LocalTrans.Rollback()
                MsgBox(ex.Message, MsgBoxStyle.Critical, "ZulAssets")
                Return False
            Finally
                If LocalConn.State = ConnectionState.Open Then
                    LocalConn.Close()
                End If
                If RemoteConn.State = ConnectionState.Open Then
                    RemoteConn.Close()
                End If
            End Try
        End Function

        Private Shared Sub CopyData(ByVal sourceTable As DataTable, ByVal DestinationTableName As String, ByVal destConnection As SqlConnection, ByVal trans As SqlTransaction)
            Dim s As SqlBulkCopy = New SqlBulkCopy(destConnection, SqlBulkCopyOptions.KeepIdentity, trans)
            Try
                s.BulkCopyTimeout = 3600
                s.DestinationTableName = DestinationTableName
                's.NotifyAfter = 10000
                s.WriteToServer(sourceTable)
                s.Close()
            Finally
                CType(s, IDisposable).Dispose()
            End Try
        End Sub


        'Private Shared Sub ExportTable(ByVal tableName As String)

        '    'Dim ColumnsName As String = ""
        '    'Dim ColumnsVar As String = ""
        '    'Dim InsertCommand As OleDbCommand = New OleDbCommand

        '    'For Each row As DataRow In GetRemoteColumnsName(tableName).Rows
        '    '    ColumnsName &= "," & row("COLUMN_NAME").ToString
        '    '    ColumnsVar &= ",?"
        '    '    InsertCommand.Parameters.Add(New OleDbParameter("@" & row("COLUMN_NAME").ToString, 0))
        '    '    'InsertCommand.Parameters.Add(New OleDbParameter("@" & row("COLUMN_NAME").ToString, CType([Enum].Parse(GetType(OleDbType), row("DATA_TYPE").ToString), OleDbType), 0, row("COLUMN_NAME").ToString))
        '    'Next

        '    'ColumnsName = "(" & ColumnsName.Remove(0, 1) & ")"
        '    'ColumnsVar = " Values(" & ColumnsVar.Remove(0, 1) & ")"

        '    'strQuery = New StringBuilder
        '    'strQuery.Append("Insert into " & tableName & " " & ColumnsName & ColumnsVar)

        '    'InsertCommand.CommandText = strQuery.ToString()
        '    'InsertCommand.Connection = RemoteConn
        '    'InsertCommand.Transaction = trans
        '    'Dim Da As OleDbDataAdapter = New OleDbDataAdapter()
        '    'Da.InsertCommand = InsertCommand

        '    'Dim dtRemote As DataTable = ExecuteRemote(objCommand)
        '    'For Each row As DataRow In dtLocal.Rows
        '    '    'row.SetAdded()
        '    '    dtRemote.ImportRow(row)
        '    '    'dtRemote.Rows.Add(row)
        '    'Next
        '    'Da.Update(dtRemote)

        'End Sub



        Private Shared Function GetRemoteColumnsName(ByVal TableName As String) As DataTable
            Dim objCommand As New SqlCommand
            Dim strQuery As New StringBuilder

            strQuery.Append("SELECT COLUMN_NAME,DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'" & TableName & "'")

            objCommand.CommandText = strQuery.ToString()
            Return ExecuteRemote(objCommand)
        End Function
        Private Shared Sub TruncateRemoteTable(ByVal TableName As String)
            Dim objCommand As New SqlCommand
            Dim strQuery As New StringBuilder

            strQuery.Append("delete [" & TableName & "]")

            objCommand.CommandText = strQuery.ToString()
            ExecuteRemote(objCommand)
        End Sub

        Private Shared Function GetListOfRemoteTables() As DataTable
            Dim objCommand As New SqlCommand
            Dim strQuery As New StringBuilder

            strQuery.Append("select TABLE_NAME as Name from INFORMATION_SCHEMA.Tables where TABLE_TYPE = 'BASE TABLE'")

            objCommand.CommandText = strQuery.ToString()
            Return ExecuteRemote(objCommand)
        End Function

        Private Shared Function ExecuteLocal(ByVal cmd As SqlCommand) As DataTable
            Try
                cmd.Connection = LocalConn
                cmd.Transaction = LocalTrans

                Dim Da As SqlDataAdapter = New SqlDataAdapter(cmd)
                Dim DS As DataTable = New DataTable
                Da.MissingSchemaAction = MissingSchemaAction.Add ' to add the table schema

                Da.Fill(DS)
                Return DS
            Catch e As Exception
                Throw e
            End Try
            Return Nothing
        End Function

        Private Shared Function ExecuteLocalScalar(ByVal cmd As SqlCommand) As String
            Dim strKey As String = ""
            Try
                cmd.Connection = LocalConn
                cmd.Transaction = LocalTrans

                Dim obj As Object = cmd.ExecuteScalar()
                If Not obj Is Nothing Then
                    strKey = Convert.ToString(obj)

                End If
                If Trim(strKey) = "" Then
                    strKey = "0"
                End If

            Catch e As Exception
                Throw
            End Try
            Return strKey
        End Function



        Private Shared Function ExecuteRemote(ByVal cmd As SqlCommand) As DataTable
            Try
                cmd.Connection = RemoteConn
                cmd.Transaction = RemoteTrans
                Dim Da As SqlDataAdapter = New SqlDataAdapter(cmd)
                Dim DS As DataTable = New DataTable
                Da.MissingSchemaAction = MissingSchemaAction.Add ' to add the table schema

                Da.Fill(DS)
                Return DS
            Catch e As Exception
                Throw e

            End Try
            Return Nothing
        End Function

        Private Shared Function ExecuteRemoteScalar(ByVal cmd As SqlCommand) As String

            Dim strKey As String = ""
            Try
                cmd.Connection = RemoteConn
                cmd.Transaction = RemoteTrans
                Dim obj As Object = cmd.ExecuteScalar()
                If Not obj Is Nothing Then
                    strKey = Convert.ToString(obj)

                End If
                If Trim(strKey) = "" Then
                    strKey = "0"
                End If

            Catch e As Exception
                Throw

            End Try

            Return strKey
        End Function
    End Class
End Namespace