
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class Devices
        Implements IEntity

#Region "Data Members"
        Private objattDevices As attDevices
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattDevices = New attDevices
        End Sub
#End Region

#Region "Property"

        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattDevices
            End Get
            Set(ByVal Value As IAttribute)
                objattDevices = CType(Value, attDevices)
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
            Dim intStatus As Integer = 0
            If objattDevices.Status = True Then
                intStatus = 1
            Else
                intStatus = 0
            End If
            strQuery.Append("insert into Devices")
            strQuery.Append(" (DeviceID,DeviceDesc,ComType,DeviceIP,Status,Isdeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" (" & objattDevices.DeviceID & ",'" & objattDevices.DeviceDesc & "'," & objattDevices.ComType & ",'" & objattDevices.DeviceIP & "'," & intStatus & ",0) ")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            Dim intStatus As Integer = 0
            If objattDevices.Status = True Then
                intStatus = 1
            Else
                intStatus = 0
            End If
            strQuery.Append("update Devices")
            strQuery.Append(" set")
            strQuery.Append(" DeviceDesc='" & objattDevices.DeviceDesc & "',")
            strQuery.Append(" ComType=" & objattDevices.ComType & ",")
            strQuery.Append(" DeviceIP='" & objattDevices.DeviceIP & "',")
            strQuery.Append(" Status=" & intStatus & ",")
            strQuery.Append(" IsDeleted= 0 ")
            strQuery.Append(" where DeviceID =" & objattDevices.DeviceID & "")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            strQuery.Append("SELECT DeviceID, DeviceDesc, ComType, DeviceIP, Status, HardwareID, ' ' as Progress, LicKey, IsRegistered  FROM Devices where IsDeleted= 0")
            If objattDevices.DeviceID <> 0 Then
                strQuery.Append(" and  DeviceID = " & objattDevices.DeviceID & "")
            End If

            If objattDevices.Status = True Then
                strQuery.Append(" and  Status =1 ")
            End If
            strQuery.Append(" ORDER BY DeviceID")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function CheckID(ByVal objattDevices1 As attDevices) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select  DeviceID,DeviceDesc,ComType,DeviceIP,Status,Isdeleted,statusID,StatusMsg,Updated  from Devices ")
            If objattDevices.DeviceID <> 0 Then
                strQuery.Append(" where  DeviceID = " & objattDevices.DeviceID & "")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetTempMessages() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select DeviceID,StatusMsg from Devices where Updated = 1")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function UpdateTempMessages(ByVal DeviceID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Devices set Updated = 0 where DeviceID='" & DeviceID & "'")
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

        Public Function GetDeviceDesc(ByVal DeviceID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            strQuery.Append("select DeviceDesc from Devices where DeviceID='" & DeviceID & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder


            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from Devices")
                strQuery.Append(" where DeviceID =" & objattDevices.DeviceID & "")
            Else
                strQuery.Append("update Devices")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where DeviceID =" & objattDevices.DeviceID & "")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(DeviceID)+1 from Devices")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
#End Region
    End Class

End Namespace

