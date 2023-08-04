Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class Ast_History
        Implements IEntity

#Region "Data Members"
        Private objattAstHistory As attAstHistory
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattAstHistory = New attAstHistory

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattAstHistory
            End Get
            Set(ByVal Value As IAttribute)
                objattAstHistory = CType(Value, attAstHistory)
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
            strQuery.Append("insert into Ast_History")
            strQuery.Append(" (AstID,HistoryID,InvSchCode,Fr_Loc,To_Loc,HisDate,Status,DeptName,Remarks,NoPiece,IsDeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" ('" & objattAstHistory.AstID & "'," & objattAstHistory.PKeyCode.ToString() & "," & objattAstHistory.InvSchCode & ",'" & objattAstHistory.Fr_loc & "','" & objattAstHistory.To_Loc & "',?," & objattAstHistory.Status & ",N'" & objattAstHistory.DeptName & "',N'" & objattAstHistory.Remarks & "'," & objattAstHistory.NoPiece & ",0 ) ")
            objCommand.CommandText = strQuery.ToString()
            If objattAstHistory.HisDate <> Nothing Then
                objCommand.Parameters.Add(New OleDbParameter("@HisDate", objattAstHistory.HisDate))
            Else
                objCommand.Parameters.Add(New OleDbParameter("@HisDate", DBNull.Value))
            End If

            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Ast_History")
            strQuery.Append(" set Fr_Loc = ? ,To_Loc = ? ,HisDate = ? ,Status = ?, NoPiece = ?, Remarks = ?,IsDeleted = 0 where AstID =? and InvSchCode = ?")
            objCommand.Parameters.Add(New OleDbParameter("@Fr_loc", objattAstHistory.Fr_loc))
            objCommand.Parameters.Add(New OleDbParameter("@To_Loc", objattAstHistory.To_Loc))
            objCommand.Parameters.Add(New OleDbParameter("@HisDate", objattAstHistory.HisDate))
            objCommand.Parameters.Add(New OleDbParameter("@Status", objattAstHistory.Status))
            objCommand.Parameters.Add(New OleDbParameter("@NoPiece", objattAstHistory.NoPiece))
            objCommand.Parameters.Add(New OleDbParameter("@Remarks", objattAstHistory.Remarks))
            objCommand.Parameters.Add(New OleDbParameter("@AstID", objattAstHistory.AstID))
            objCommand.Parameters.Add(New OleDbParameter("@InvSchCode", objattAstHistory.InvSchCode))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Check_Child(ByVal JustChanged As Boolean) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            strQuery.Append("Select Count(*) from Ast_History where isdeleted = 0 ")
            If JustChanged Then
                strQuery.Append(" and Status <> 0")
            End If

            If objattAstHistory.InvSchCode.ToString <> "" Then
                strQuery.Append(" and  InvSchCode =" & objattAstHistory.InvSchCode.ToString)
            End If

            If objattAstHistory.AstID <> "" Then
                strQuery.Append(" and  AstID ='" & objattAstHistory.AstID & "'")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder

            strQuery.Append("Select HistoryId,Ast_INV_Schedule.InvDesc,Ast_History.AstId,Locfr.LocationFullPath as floc,Locto.LocationFullPath as toloc,HisDate,CAST(Status as CHAR(1)) as Status,Ast_History.IsDeleted,Ast_History.Fr_loc,Ast_History.To_Loc,DeptName,Remarks from Ast_History,Ast_INV_Schedule ,Location locfr,Location locto")
            strQuery.Append(" where Ast_History.IsDeleted = 0")
            strQuery.Append(" and Ast_History.InvSchCode = Ast_INV_Schedule.InvSchCode ")
            strQuery.Append(" and Ast_History.FR_lOC = Locfr.LocID")
            strQuery.Append(" and Ast_History.to_lOC = Locto.LocID")
            strQuery.Append(" and Locto.IsDeleted = 0  and locfr.IsDeleted = 0")
            If objattAstHistory.AstID <> "" Then
                strQuery.Append(" and Ast_History.AstId = '" & objattAstHistory.AstID & "'")
            End If
            strQuery.Append(" Order by Ast_History.HisDate desc")

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
                strQuery.Append("delete from Ast_History")
                strQuery.Append(" where AstId =?")
            Else
                strQuery.Append("update Ast_History")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where AstId =?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@AstId", objattAstHistory.AstID))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function DeleteByInvSch() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from Ast_History")
                strQuery.Append(" where InvSchCode =?")
            Else
                strQuery.Append("update Ast_History")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where InvSchCode =?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@InvSchCode", objattAstHistory.InvSchCode))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(HistoryID)+1 from Ast_History")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function GetAllData_Combo() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select InvSchCode,InvDesc from Ast_History where IsDeleted = 0 and Closed =0")
            If objattAstHistory.PKeyCode <> 0 Then
                strQuery.Append(" and InvSchCode = " & objattAstHistory.PKeyCode)
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

#End Region

    End Class

End Namespace

