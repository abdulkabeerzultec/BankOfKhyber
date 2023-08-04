Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class DepLogs
        Implements IEntity

#Region "Data Members"
        Private objattDepLogs As attDepLogs
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattDepLogs = New attDepLogs

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattDepLogs
            End Get
            Set(ByVal Value As IAttribute)
                objattDepLogs = CType(Value, attDepLogs)
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
        Public Function GetAllData_Combo() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select DISTINCT updDate from Deplogs")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Insert() As IDbCommand Implements IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DbType = "1" Then
                strQuery.Append("insert into DepLogs")
                strQuery.Append(" (updDate,DepPrdType,totDepValue,totAstCount,totAstValue,updMonth,updYear,BookID)")
                strQuery.Append(" Values")
                strQuery.Append(" (" & BackEndDate(objattDepLogs.UpdDate) & "," & objattDepLogs.DepPrdtype & "," & objattDepLogs.TotDepValue & "," & objattDepLogs.TotAstCount & "," & objattDepLogs.TotAstValue & "," & objattDepLogs.UpdMonth & "," & objattDepLogs.UpdYear & "," & objattDepLogs.BookID & ") ")
            Else
                strQuery.Append("insert into DepLogs")
                strQuery.Append(" (DepLogID,updDate,DepPrdType,totDepValue,totAstCount,totAstValue,updMonth,updYear,BookID)")
                strQuery.Append(" Values")
                strQuery.Append(" (" & objattDepLogs.PKeyCode.ToString & "," & BackEndDate(objattDepLogs.UpdDate) & "," & objattDepLogs.DepPrdtype & "," & objattDepLogs.TotDepValue & "," & objattDepLogs.TotAstCount & "," & objattDepLogs.TotAstValue & "," & objattDepLogs.UpdMonth & "," & objattDepLogs.UpdYear & "," & objattDepLogs.BookID & ") ")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update DepLogs")
            strQuery.Append(" set")
            strQuery.Append(" updDate=" & BackEndDate(objattDepLogs.UpdDate) & ",")
            strQuery.Append(" DepPrdType =" & objattDepLogs.DepPrdtype & ",")
            strQuery.Append(" totDepValue =" & objattDepLogs.TotDepValue & ",")
            strQuery.Append(" totAstCount =" & objattDepLogs.TotAstCount & ",")
            strQuery.Append(" totAstValue =" & objattDepLogs.TotAstValue & ",")
            strQuery.Append(" updMonth =" & objattDepLogs.UpdMonth & ",")
            strQuery.Append(" BookID =" & objattDepLogs.BookID & ",")
            strQuery.Append(" updYear =" & objattDepLogs.UpdYear & "")
            strQuery.Append(" where DeptHistID =" & Convert.ToString(objattDepLogs.PKeyCode) & "")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function GetAll_DepLogs_Date(ByVal objattDepLogs As attDepLogs) As IDbCommand

            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select DepLogID,updDate,DepPrdType,totDepValue,totAstCount,totAstValue,updMonth,updYear,BookID from DepLogs")
            strQuery.Append(" where  UpdDate >=" & BackEndDate(objattDepLogs.UpdDate) & " and BookID = " & objattDepLogs.BookID)
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function




        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select DepLogID,updDate,DepPrdType,totDepValue,totAstCount,totAstValue,updMonth,updYear,BookID from DepLogs")

            If objattDepLogs.UpdDate <> DateTime.MinValue And objattDepLogs.UpdYear = 0 And objattDepLogs.UpdMonth = 0 Then
                strQuery.Append(" where  updDate=" & BackEndDate(objattDepLogs.UpdDate) & "")
            End If
            If objattDepLogs.UpdMonth <> 0 And objattDepLogs.UpdYear <> 0 Then
                strQuery.Append(" where  updMonth=" & objattDepLogs.UpdMonth & " and updYear=" & objattDepLogs.UpdYear)
            End If

            If objattDepLogs.BookID <> 0 Then
                strQuery.Append(" and BookID=" & objattDepLogs.BookID)
            End If
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

        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from DepLogs")
                strQuery.Append(" where DeptHistID =" & objattDepLogs.PKeyCode)

            Else


                strQuery.Append("update DepLogs")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where DeptHistID =" & objattDepLogs.PKeyCode)
            End If


            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(DepLogID)+1 from DepLogs")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function
#End Region

    End Class

End Namespace

