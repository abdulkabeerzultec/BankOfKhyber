Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Text


Namespace ZulAssetBAL
    Public Class BLBase
        Private DbOp As DBOperations

        Public Sub New()
            DbOp = New DBOperations
        End Sub


#Region " -- Transacation Executer"
        Protected Sub TranscationExecuter(ByVal _Entity As IEntity)
            Try
                DbOp.TransationExecuter(_Entity)
            Catch ex As Exception
                DbOp.RollBackTrans()
            End Try

        End Sub
#End Region

#Region " -- insert , update ,Delete --"

        Protected Sub Insert(ByVal _Entity As IEntity)
            Try
                DbOp.Insert(_Entity)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub


        Protected Sub InsertTemp(ByVal _Entity As IEntity)
            Try
                DbOp.Insert_Temp(_Entity)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub


        Protected Sub Update(ByVal _Entity As IEntity)
            Try
                DbOp.update(_Entity)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Protected Sub UpdateTemp(ByVal _Entity As IEntity)
            Try
                DbOp.updateTemp(_Entity)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Protected Function GetAllData(ByVal _Entity As IEntity) As DataTable
            Try
                Return DbOp.GetAllData(_Entity)
            Catch ex As Exception
                Throw ex
            End Try
            Return Nothing
        End Function

        Protected Function GetAllDataTemp(ByVal _Entity As IEntity) As DataTable
            Try
                Return DbOp.GetAllDataTemp(_Entity)
            Catch ex As Exception
                Throw ex
            End Try
            Return Nothing
        End Function

        Protected Function GeneralExecuter(ByVal cmd As OleDbCommand) As DataTable
            Try
                Return DbOp.General_Executer(cmd)
            Catch ex As Exception
                Throw ex
            End Try
            Return Nothing
        End Function

        Protected Function GeneralExecuterTemp(ByVal cmd As OleDbCommand) As DataTable
            Try
                Return DbOp.General_ExecuterTemp(cmd)
            Catch ex As Exception
                Throw ex
            End Try
            Return Nothing
        End Function

        Protected Function GeneralExecuter(ByVal cmd As OleDbCommand, ByVal str As String) As DataTable
            Try
                'Dim ds As DataTable

                Return DbOp.General_Executer(cmd)

            Catch ex As Exception
                Throw ex
            End Try
            Return Nothing
        End Function

        Protected Function GeneralExecuter_Scalar(ByVal cmd As OleDbCommand, ByVal str As String) As String
            Try
                '   Dim ds As DataTable

                Return DbOp.General_Executer_Scalar(cmd)
                ' Return ds
            Catch ex As Exception
                Throw ex
            End Try
            Return Nothing
        End Function

        Protected Function GeneralExecuter_Scalar_ExportServer(ByVal cmd As OleDbCommand) As String
            Try
                '   Dim ds As DataTable

                Return DbOp.General_ExecuteScaler_ExportServer(cmd)
                ' Return ds
            Catch ex As Exception
                Throw ex
            End Try
            Return Nothing
        End Function

        Protected Sub TransactionExecuter(ByVal cmd As OleDbCommand)
            Try
                DbOp.TransationExecuter(cmd)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Protected Sub Delete(ByVal _Entity As IEntity)
            Try
                DbOp.delete(_Entity)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Protected Sub DeleteTemp(ByVal _Entity As IEntity)
            Try
                DbOp.deleteTemp(_Entity)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        'Written By Wael Dalloul to know if the value of a field of a table is existed or not.
        Protected Function FieldValueExisted(ByVal FieldName As String, ByVal FieldValue As String, ByVal TableName As String, _
                                           ByVal IsInsertStatus As Boolean, ByVal PKField As String, _
                                          ByVal PKFieldVal As String) As String
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select " & PKField & " from " & TableName & " where " & FieldName)
            strQuery.Append(" = ?")
            objCommand.Parameters.Add(New OleDbParameter("@FieldValue", FieldValue))
            objCommand.CommandText = strQuery.ToString()
            Try

                Dim str As String = DbOp.General_Executer_Scalar(objCommand)
                If str = "0" Then
                    str = ""
                End If

                If str <> "" And IsInsertStatus Then
                    Return True
                ElseIf str <> "" And Not IsInsertStatus And str <> PKFieldVal Then
                    Return True
                End If
                Return False
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region " Get Next Primary key"
        Protected Function GetNextPKey(ByVal _Entity As IEntity) As String
            Dim strKey As String = ""
            Dim intKey As Double = 0
            Try
                strKey = DbOp.GetNextPKey(_Entity)
                intKey = CDbl(strKey)
                If intKey = 0 Then
                    strKey = 1
                End If

            Catch ex As Exception
                Throw ex
            End Try
            Return strKey
        End Function
#End Region
    End Class

End Namespace
