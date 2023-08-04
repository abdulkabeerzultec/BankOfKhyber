Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class BookHistory
        Implements IEntity

#Region "Data Members"
        Private objattBookHistory As attBookHistory
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattBookHistory = New attBookHistory

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattBookHistory
            End Get
            Set(ByVal Value As IAttribute)
                objattBookHistory = CType(Value, attBookHistory)
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
        Public Function Check_Child_AstBooks(ByVal _id As String, ByVal formid As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select BookID from BookHistory where isdeleted = 0")
            If formid = 12 Then
                strQuery.Append(" and  BookID =" & _id)
            ElseIf formid = 13 Then
                strQuery.Append(" and  AstID ='" & _id & "'")

            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Insert() As IDbCommand Implements IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DbType = "1" Then
                strQuery.Append("insert into BookHistory")
                strQuery.Append(" (BookID,AssetID,DepCode,DepValue,AccDepValue,DepDate,CurrentBV,SalvageYear,SalvageMonth,Isdeleted)")
                strQuery.Append(" Values")
                strQuery.Append(" ('" & objattBookHistory.BookID & "','" & objattBookHistory.ASTID & "'," & objattBookHistory.DepCode & "," & objattBookHistory.DepValue & "," & objattBookHistory.AccDepValue & "," & BackEndDate(objattBookHistory.DepDate) & "," & objattBookHistory.CurrentBV & "," & objattBookHistory.SalvageYear & "," & objattBookHistory.SalvageMonth & ",0) ")
            Else
                strQuery.Append("insert into BookHistory")
                strQuery.Append(" (DepHistID,BookID,AssetID,DepCode,DepValue,AccDepValue,DepDate,CurrentBV,SalvageYear,SalvageMonth,Isdeleted)")
                strQuery.Append(" Values")
                strQuery.Append(" (" & objattBookHistory.PKeyCode.ToString & ",'" & objattBookHistory.BookID & "','" & objattBookHistory.ASTID & "'," & objattBookHistory.DepCode & "," & objattBookHistory.DepValue & "," & objattBookHistory.AccDepValue & "," & BackEndDate(objattBookHistory.DepDate) & "," & objattBookHistory.CurrentBV & "," & objattBookHistory.SalvageYear & "," & objattBookHistory.SalvageMonth & ",0) ")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update BookHistory")
            strQuery.Append(" set")
            strQuery.Append(" BookID='" & objattBookHistory.BookID & "',")
            strQuery.Append(" AssetID='" & objattBookHistory.ASTID & "',")
            strQuery.Append(" DepCode ='" & objattBookHistory.DepCode & "',")
            strQuery.Append(" DepValue ='" & objattBookHistory.DepValue & "',")
            strQuery.Append(" SalvageYear =" & objattBookHistory.SalvageYear & ",")
            strQuery.Append(" SalvageMonth =" & objattBookHistory.SalvageMonth & ",")
            strQuery.Append(" AccDepValue ='" & objattBookHistory.AccDepValue & "',")
            strQuery.Append(" DepDate =" & BackEndDate(objattBookHistory.DepDate) & ",")
            strQuery.Append(" Current ='" & objattBookHistory.CurrentBV & "'")
            strQuery.Append(" where BookID ='" & objattBookHistory.BookID & "' and AssetID ='" & objattBookHistory.ASTID & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            If AppConfig.DbType = "1" Then
                strQuery.Append("Select DepHistID,BookID,DepCode,DepDate,DepValue,AccDepValue,CurrentBV,IsDeleted,AssetID,cast (SalvageYear as varchar) +  '.' + cast (SalvageMonth  as varchar)  from BookHistory where IsDeleted = 0")
                If objattBookHistory.BookID <> "" And objattBookHistory.ASTID <> "" Then
                    strQuery.Append(" and BookID ='" & objattBookHistory.BookID & "' and AssetID ='" & objattBookHistory.ASTID & "'")
                End If
            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append("Select DepHistID,BookID,DepCode,DepDate,DepValue,AccDepValue,CurrentBV,IsDeleted,AssetID,SalvageYear from BookHistory where IsDeleted = 0")
                If objattBookHistory.BookID <> "" And objattBookHistory.ASTID <> "" Then
                    strQuery.Append(" and BookID ='" & objattBookHistory.BookID & "' and AssetID ='" & objattBookHistory.ASTID & "'")
                End If

            End If
         
            strQuery.Append(" Order by DepDate")
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
            Return objCommand
        End Function

        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from BookHistory")
                strQuery.Append(" where AssetId =?")
            Else
                strQuery.Append("update BookHistory")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where AssetId =?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@AssetId", objattBookHistory.ASTID))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(DepHistID)+1 from BookHistory")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function
#End Region

    End Class

End Namespace
