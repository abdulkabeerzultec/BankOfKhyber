

Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class Ast_Cust_history
        Implements IEntity

#Region "Data Members"
        Private objattAst_Cust_history As attAst_Cust_history
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattAst_Cust_history = New attAst_Cust_history

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattAst_Cust_history
            End Get
            Set(ByVal Value As IAttribute)
                objattAst_Cust_history = CType(Value, attAst_Cust_history)
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
        Public Function Insert() As IDbCommand Implements IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into Ast_Cust_history")
            strQuery.Append(" (AstID,HistoryID,Fromcustodian,Tocustodian,HisDate,IsDeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" ('" & objattAst_Cust_history.AstID & "'," & objattAst_Cust_history.PKeyCode.ToString() & ",'" & objattAst_Cust_history.Fr_Cust & "','" & objattAst_Cust_history.To_Cust & "'," & BackEndDate(objattAst_Cust_history.HisDate) & ",0 ) ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand

            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            'Dim strQuery As New StringBuilder
            'strQuery.Append("update Ast_History")
            'strQuery.Append(" set")
            'strQuery.Append(" InvDesc='" & objattAst_Cust_history.InvDesc & "',")
            'strQuery.Append(" InvStartDate='" & objattAst_Cust_history.InvStartDate & "',")
            'strQuery.Append(" InvEndDate='" & objattAst_Cust_history.InvEndDate & "'")
            'strQuery.Append(" where InvSchCode =" & objattAst_Cust_history.PKeyCode & "")
            'objCommand.CommandText = strQuery.ToString()
            '_Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder

            strQuery.Append("Select HistoryId,Ast_Cust_history.AstId,custfr.CustodianName as fcust,custto.CustodianName as toCust,HisDate,Ast_Cust_history.IsDeleted from Ast_Cust_history ,Custodian Custfr,Custodian Custto ")
            strQuery.Append(" where Ast_Cust_history.IsDeleted = 0 and Ast_Cust_history.Fromcustodian = Custfr.CustodianID ")
            strQuery.Append(" and Ast_Cust_history.Tocustodian = Custto.CustodianId and Custto.IsDeleted = 0 ")
            strQuery.Append("  and Custfr.IsDeleted = 0")
            If objattAst_Cust_history.AstID <> "" Then
                strQuery.Append(" and Ast_Cust_history.AstId = '" & objattAst_Cust_history.AstID & "'")
            End If
            strQuery.Append(" Order by Ast_Cust_history.HisDate desc")
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
                strQuery.Append("delete from Ast_Cust_history")
                strQuery.Append(" where AstID = ?")
            Else
                strQuery.Append("update Ast_Cust_history")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where AstID =?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@AstID", objattAst_Cust_history.AstID))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function



        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(HistoryID)+1 from Ast_Cust_history")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function


#End Region

    End Class

End Namespace


