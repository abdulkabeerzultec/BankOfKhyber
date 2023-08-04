Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class Ast_Sold
        Implements IEntity

#Region "Data Members"
        Private objattAst_Sold As attAst_Sold
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattAst_Sold = New attAst_Sold
        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattAst_Sold
            End Get
            Set(ByVal Value As IAttribute)
                objattAst_Sold = CType(Value, attAst_Sold)
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
            strQuery.Append("insert into Ast_Sold")
            strQuery.Append(" (AstID,Sel_Date,Sel_Price,BookValue,SoldTo)")
            strQuery.Append(" Values")
            strQuery.Append(" (" & objattAst_Sold.AstID & ",'" & objattAst_Sold.Sel_Date & "'," & objattAst_Sold.Sel_Price & "," & objattAst_Sold.BookValue & ",'" & objattAst_Sold.SoldTo & "') ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Ast_Sold")
            strQuery.Append(" set")
            strQuery.Append(" Sel_Date=#" & objattAst_Sold.Sel_Date & "#,")
            strQuery.Append(" Sel_Price=" & objattAst_Sold.Sel_Price & ",")
            strQuery.Append(" BookValue=" & objattAst_Sold.BookValue & ",")
            strQuery.Append(" SoldTo='" & objattAst_Sold.SoldTo & "'")
            strQuery.Append(" where AstID ='" & Convert.ToString(objattAst_Sold.AstID) & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select * from Ast_Sold where IsDeleted = 0")
            If objattAst_Sold.AstID <> "" Then
                strQuery.Append(" and AstID = '" & objattAst_Sold.AstID & "'")
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
                strQuery.Append("delete from Ast_Sold")
                strQuery.Append(" where AstSoldID = ?")
            Else

                strQuery.Append("update Ast_Sold")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where AstSoldID = ?")
            End If

            objCommand.Parameters.Add(New OleDbParameter("@AstSoldID", objattAst_Sold.PKeyCode))


            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand

            Return objCommand
        End Function
#End Region

    End Class

End Namespace
