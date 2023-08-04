Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class AddCostHistory
        Implements IEntity

#Region "Data Members"
        Private objattAddCostHist As AttAddCostHistory
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattAddCostHist = New AttAddCostHistory
        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattAddCostHist
            End Get
            Set(ByVal Value As IAttribute)
                objattAddCostHist = CType(Value, AttAddCostHistory)
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

        Public Function Check_Child(ByVal LoginName As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select Count(*) from AddCostHistory ")
            strQuery.Append(" where AddCostHistory.IsDeleted = 0")
            If LoginName <> "" Then
                strQuery.Append(" and LoginName = ?")
                objCommand.Parameters.Add(New OleDbParameter("@LoginName", LoginName))
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function CopyAddCost(ByVal AddCostID As String, ByVal OldAstID As String, ByVal NewAstID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into AddCostHistory select ?,?, ")
            strQuery.Append("TypeID,AddCost,ActionDate,LoginName,PrevItemCost,isDeleted from AddCostHistory ")
            strQuery.Append("where astid=?")

            objCommand.Parameters.Add(New OleDbParameter("@AddCostID", AddCostID))
            objCommand.Parameters.Add(New OleDbParameter("@NewAstID", NewAstID))
            objCommand.Parameters.Add(New OleDbParameter("@OldAstID", OldAstID))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function Check_Child_CostType(ByVal TypeID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select Count(*) from AddCostHistory ")
            strQuery.Append(" where IsDeleted = 0")
            If TypeID <> "" Then
                strQuery.Append(" and TypeID = ?")
                objCommand.Parameters.Add(New OleDbParameter("@TypeID", TypeID))
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Insert() As IDbCommand Implements IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into AddCostHistory")
            strQuery.Append(" (AddCostID,AstID,TypeID,AddCost,ActionDate,LoginName,PrevItemCost,isDeleted)")
            strQuery.Append(" Values(?,?,?,?,?,?,?,0)")

            objCommand.Parameters.Add(New OleDbParameter("@AddCostID", objattAddCostHist.AddCostID))
            objCommand.Parameters.Add(New OleDbParameter("@AstID", objattAddCostHist.AstID))
            objCommand.Parameters.Add(New OleDbParameter("@TypeID", objattAddCostHist.TypeID.ToString))
            objCommand.Parameters.Add(New OleDbParameter("@AddCost", objattAddCostHist.AddCost.ToString))
            objCommand.Parameters.Add(New OleDbParameter("@ActionDate", objattAddCostHist.ActionDate))
            objCommand.Parameters.Add(New OleDbParameter("@LoginName", objattAddCostHist.LoginName))
            objCommand.Parameters.Add(New OleDbParameter("@PrevItemCost", objattAddCostHist.PrevItemCost.ToString))


            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AddCostHistory")
            strQuery.Append(" set AstID =?,TypeID =?,AddCost =?,ActionDate =?,LoginName =?,PrevItemCost =? ")
            strQuery.Append(" where AddCostID =?")

            objCommand.Parameters.Add(New OleDbParameter("@AstID", objattAddCostHist.AstID.ToString))
            objCommand.Parameters.Add(New OleDbParameter("@TypeID", objattAddCostHist.TypeID.ToString))
            objCommand.Parameters.Add(New OleDbParameter("@AddCost", objattAddCostHist.AddCost.ToString))
            objCommand.Parameters.Add(New OleDbParameter("@ActionDate", objattAddCostHist.ActionDate))
            objCommand.Parameters.Add(New OleDbParameter("@LoginName", objattAddCostHist.LoginName))
            objCommand.Parameters.Add(New OleDbParameter("@PrevItemCost", objattAddCostHist.PrevItemCost.ToString))
            objCommand.Parameters.Add(New OleDbParameter("@AddCostID", objattAddCostHist.AddCostID))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select LoginName,ActionDate,PrevItemCost,AddCost,TypeDesc,AstID,AddCostID from AddCostHistory inner join AddCostType on AddCostType.TypeID = AddCostHistory.TypeID where AddCostHistory.IsDeleted = 0 ")
            If objattAddCostHist.AstID <> 0 Then
                strQuery.Append(" and AstID = ?")
                objCommand.Parameters.Add(New OleDbParameter("@AstID", objattAddCostHist.AstID))
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from AddCostHistory")
                strQuery.Append(" where AddCostID =?")
            Else
                strQuery.Append("update AddCostHistory")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where AddCostID =?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@AddCostID", objattAddCostHist.AddCostID))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function DeleteByAstID() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from AddCostHistory")
                strQuery.Append(" where AstID =?")
            Else
                strQuery.Append("update AddCostHistory")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where AstID =?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@AstID", objattAddCostHist.AstID))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(AddCostID) + 1  from AddCostHistory ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function GetDatabyID(ByVal Id As String) As IDbCommand Implements IEntity.GetDatabyID
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select * from AddCostHistory where AstID = ?")
            objCommand.Parameters.Add(New OleDbParameter("@AstID", Id))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
#End Region

    End Class

End Namespace
