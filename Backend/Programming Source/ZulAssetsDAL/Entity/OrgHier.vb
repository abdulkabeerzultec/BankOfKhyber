
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class OrgHier
        Implements IEntity
#Region "Data Members"
        Private objattOrgHier As attOrgHier
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattOrgHier = New attOrgHier

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattOrgHier
            End Get
            Set(ByVal Value As IAttribute)
                objattOrgHier = CType(Value, attOrgHier)
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


        '[HierCode,GrpID,IsWareHouse,IsDeleted
#Region "Methods"
        Public Function Insert() As IDbCommand Implements IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into OrgHier")
            strQuery.Append(" (HierCode,GrpID,IsWareHouse,IsDeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" ('" & Convert.ToString(objattOrgHier.PKeyCode) & "','" & objattOrgHier.GrpID & "',0,0) ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update OrgHier")
            strQuery.Append(" set")
            strQuery.Append(" HierCode='" & Convert.ToString(objattOrgHier.PKeyCode) & "',")
            strQuery.Append(" GrpID=' " & objattOrgHier.GrpID & "' ")
            strQuery.Append(" where GrpId =" & objattOrgHier.PKeyCode & "")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Check_Child_Custodian(ByVal _Id As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append(" select CustodianID from Custodian  ")
            strQuery.Append(" where DeptId LIKE '%" & _Id & "%'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Check_Child(ByVal _Id As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append(" select HierCode,GrpID,IsWareHouse,IsDeleted from OrgHier  ")
            strQuery.Append(" where HierCode LIKE '%" & _Id & "%'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Check_Child_byGroup(ByVal _Id As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append(" select HierCode,GrpID,IsWareHouse,IsDeleted from OrgHier  ")
            strQuery.Append(" where HierCode='" & _Id & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append(" select HierCode,GrpID,IsWareHouse,IsDeleted from OrgHier  ")
            strQuery.Append(" where OrgHier.IsDeleted = 0 ")
            If objattOrgHier.PKeyCode <> "" Then
                strQuery.Append(" and OrgHier.HierCode ='" & Convert.ToString(objattOrgHier.PKeyCode) & "'")
            End If
            If objattOrgHier.GrpID <> "" Then
                strQuery.Append(" and OrgHier.GrpID ='" & objattOrgHier.GrpID & "'")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from OrgHier")
                strQuery.Append(" where OrgHier.HierCode like ?")
            Else

                strQuery.Append("update OrgHier")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where OrgHier.HierCode like ?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@HierCode", objattOrgHier.PKeyCode))



            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(HierCode)+1 from OrgHier")
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
#End Region

    End Class
End Namespace


