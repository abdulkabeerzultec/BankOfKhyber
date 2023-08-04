

Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class DepPolicy
        Implements IEntity

#Region "Data Members"
        Private objattDepPolicy As attDepPolicy
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattDepPolicy = New attDepPolicy

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattDepPolicy
            End Get
            Set(ByVal Value As IAttribute)
                objattDepPolicy = CType(Value, attDepPolicy)
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

            Dim intIsSalvageValuePercent = 0
            If objattDepPolicy.IsSalvageValuePercent Then
                intIsSalvageValuePercent = 1
            Else
                intIsSalvageValuePercent = 0
            End If
            strQuery.Append("insert into DepPolicy")
            strQuery.Append(" (CatDepID,AstCatID,SalvageValue,SalvageYear,DepCode,SalvageMonth,SalvagePercent,IsSalvageValuePercent,isDeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" ( '" & objattDepPolicy.AstCatID & "','" & objattDepPolicy.AstCatID & "'," & objattDepPolicy.SalvageValue & "," & objattDepPolicy.SalvageYear & "," & objattDepPolicy.DepCode & "," & objattDepPolicy.SalvageMonth & "," & objattDepPolicy.SalvagePercent & "," & intIsSalvageValuePercent & ",0" & ") ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            Dim intSalvageYear As Integer = 0
            strQuery.Append("update DepPolicy")
            strQuery.Append(" set")
            strQuery.Append(" AstCatID='" & objattDepPolicy.AstCatID & "',")
            strQuery.Append(" SalvageValue=" & objattDepPolicy.SalvageValue & ",")
            strQuery.Append(" DepCode=" & objattDepPolicy.DepCode & ",")
            strQuery.Append(" SalvageMonth=" & objattDepPolicy.SalvageMonth & ",")
            strQuery.Append(" SalvageYear=" & objattDepPolicy.SalvageYear & ",")
            strQuery.Append(" SalvagePercent=" & objattDepPolicy.SalvagePercent & ",")
            If objattDepPolicy.IsSalvageValuePercent Then
                strQuery.Append(" IsSalvageValuePercent=1")
            Else
                strQuery.Append(" IsSalvageValuePercent=0")
            End If
            strQuery.Append(" where CatDepID ='" & objattDepPolicy.AstCatID & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select CatDepID,AstCatID,DepCode,SalvageValue,SalvageYear,SalvageMonth,SalvagePercent,IsSalvageValuePercent from DepPolicy where 0 = 0")
            If objattDepPolicy.AstCatID <> "" Then
                strQuery.Append(" and  AstCatID = '" & objattDepPolicy.AstCatID & "'")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function CheckID() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select CatDepID,AstCatID,DepCode,SalvageValue,SalvageYear,SalvagePercent,IsSalvageValuePercent from DepPolicy ")
            If objattDepPolicy.CatDepID <> "" Then
                strQuery.Append(" where  CatDepID = '" & objattDepPolicy.CatDepID & "'")
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
            Return objCommand
        End Function

        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from  DepPolicy")
                If Trim(objattDepPolicy.CatDepID) <> "" Then
                    strQuery.Append(" where CatDepID =" & objattDepPolicy.CatDepID & "")
                ElseIf Trim(objattDepPolicy.AstCatID) <> "" Then
                    strQuery.Append(" where AstCatID =?")
                    objCommand.Parameters.Add(New OleDbParameter("@AstCatID", objattDepPolicy.AstCatID))
                End If

            Else
                strQuery.Append("update DepPolicy")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                If Trim(objattDepPolicy.CatDepID) <> "" Then
                    strQuery.Append(" where CatDepID =" & objattDepPolicy.CatDepID & "")
                ElseIf Trim(objattDepPolicy.AstCatID) <> "" Then
                    strQuery.Append(" where AstCatID =?")
                    objCommand.Parameters.Add(New OleDbParameter("@AstCatID", objattDepPolicy.AstCatID))
                End If
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Return ObjCommand
        End Function
#End Region

    End Class

End Namespace


