Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class OfflineMachines
        Implements IEntity

#Region "Data Members"
        Private objattOfflineMachines As AttOfflineMachines
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattOfflineMachines = New AttOfflineMachines
        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattOfflineMachines
            End Get
            Set(ByVal Value As IAttribute)
                objattOfflineMachines = CType(Value, AttOfflineMachines)
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

        Public Function Delete() As System.Data.IDbCommand Implements ZulAssetsDAL.IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from OfflineMachines")
                strQuery.Append(" where MachineID =?")
            Else
                strQuery.Append("update OfflineMachines")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where MachineID =?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@MachineID", objattOfflineMachines.MachineID))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As System.Data.IDbCommand Implements ZulAssetsDAL.IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select MachineID,MachineDesc,ServerName,DatabaseName,UserName,Password,Port,OfflineMachines.LastAssetNumber,StartSerial,EndSerial,OfflineMachines.CompanyID,CompanyName from OfflineMachines ")
            strQuery.Append(" Inner join Companies on Companies.CompanyId = OfflineMachines.CompanyID")
            strQuery.Append(" where OfflineMachines.IsDeleted = 0 ")
            If objattOfflineMachines.MachineID <> 0 Then
                strQuery.Append(" and MachineID = ?")
                objCommand.Parameters.Add(New OleDbParameter("@MachineID", objattOfflineMachines.MachineID))
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetDatabyID(ByVal ID As String) As System.Data.IDbCommand Implements ZulAssetsDAL.IEntity.GetDatabyID
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select MachineID,MachineDesc,ServerName,DatabaseName,UserName,Password,Port,LastAssetNumber,StartSerial,EndSerial,CompanyID from OfflineMachines where MachineID = ?")
            objCommand.Parameters.Add(New OleDbParameter("@MachineID", ID))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPk() As System.Data.IDbCommand Implements ZulAssetsDAL.IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(MachineID) + 1  from OfflineMachines ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Insert() As System.Data.IDbCommand Implements ZulAssetsDAL.IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into OfflineMachines")
            strQuery.Append(" (MachineID,MachineDesc,ServerName,DatabaseName,UserName,Password,Port,LastAssetNumber,StartSerial,EndSerial,CompanyID,IsDeleted)")
            strQuery.Append(" Values(?,?,?,?,?,?,?,?,?,?,?,0)")

            objCommand.Parameters.Add(New OleDbParameter("@MachineID", objattOfflineMachines.MachineID))
            objCommand.Parameters.Add(New OleDbParameter("@MachineDesc", objattOfflineMachines.MachineDesc))
            objCommand.Parameters.Add(New OleDbParameter("@ServerName", objattOfflineMachines.ServerName))
            objCommand.Parameters.Add(New OleDbParameter("@DatabaseName", objattOfflineMachines.DatabaseName))
            objCommand.Parameters.Add(New OleDbParameter("@UserName", objattOfflineMachines.UserName))
            objCommand.Parameters.Add(New OleDbParameter("@Password", objattOfflineMachines.Password))
            objCommand.Parameters.Add(New OleDbParameter("@Port", objattOfflineMachines.Port))
            objCommand.Parameters.Add(New OleDbParameter("@LastAssetNumber", objattOfflineMachines.LastAssetNumber.ToString))
            objCommand.Parameters.Add(New OleDbParameter("@StartSerial", objattOfflineMachines.StartSerial.ToString))
            objCommand.Parameters.Add(New OleDbParameter("@EndSerial", objattOfflineMachines.EndSerial.ToString))
            objCommand.Parameters.Add(New OleDbParameter("@CompanyID", objattOfflineMachines.CompanyID.ToString))


            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function Update() As System.Data.IDbCommand Implements ZulAssetsDAL.IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update OfflineMachines")
            strQuery.Append(" set MachineDesc =?,ServerName =?,DatabaseName =?,UserName =?,Password =?,Port =?,LastAssetNumber =?,StartSerial =?,EndSerial = ? ,CompanyID = ? ")
            strQuery.Append(" where MachineID =?")

            objCommand.Parameters.Add(New OleDbParameter("@MachineDesc", objattOfflineMachines.MachineDesc))
            objCommand.Parameters.Add(New OleDbParameter("@ServerName", objattOfflineMachines.ServerName))
            objCommand.Parameters.Add(New OleDbParameter("@DatabaseName", objattOfflineMachines.DatabaseName))
            objCommand.Parameters.Add(New OleDbParameter("@UserName", objattOfflineMachines.UserName))
            objCommand.Parameters.Add(New OleDbParameter("@Password", objattOfflineMachines.Password))
            objCommand.Parameters.Add(New OleDbParameter("@Port", objattOfflineMachines.Port))
            objCommand.Parameters.Add(New OleDbParameter("@LastAssetNumber", objattOfflineMachines.LastAssetNumber.ToString))
            objCommand.Parameters.Add(New OleDbParameter("@StartSerial", objattOfflineMachines.StartSerial.ToString))
            objCommand.Parameters.Add(New OleDbParameter("@EndSerial", objattOfflineMachines.EndSerial.ToString))
            objCommand.Parameters.Add(New OleDbParameter("@CompanyID", objattOfflineMachines.CompanyID.ToString))
            objCommand.Parameters.Add(New OleDbParameter("@MachineID", objattOfflineMachines.MachineID))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function UpdateLastAssetNumber(ByVal AssetNumber As Int64) As System.Data.IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update OfflineMachines")
            strQuery.Append(" set LastAssetNumber =?")

            objCommand.Parameters.Add(New OleDbParameter("@LastAssetNumber", AssetNumber))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
    End Class

End Namespace
