Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class InvSchedule
        Implements IEntity

#Region "Data Members"
        Private objattInvSchedule As attInvSchedule
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattInvSchedule = New attInvSchedule

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattInvSchedule
            End Get
            Set(ByVal Value As IAttribute)
                objattInvSchedule = CType(Value, attInvSchedule)
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
            strQuery.Append("insert into Ast_INV_Schedule")
            strQuery.Append(" (InvSchCode,InvDesc,InvStartDate,InvEndDate,Isdeleted,closed,SchType)")
            strQuery.Append(" Values(?,?,?,?,?,?,?)")
            'strQuery.Append(" (" & Convert.ToString(objattInvSchedule.PKeyCode) & ",N'" & objattInvSchedule.InvDesc & "'," & BackEndDate(objattInvSchedule.InvStartDate) & "," & BackEndDate(objattInvSchedule.InvEndDate) & ",0,0," & objattInvSchedule.InventoryType & ") ")
            objCommand.Parameters.Add(New OleDbParameter("@InvSchCode", objattInvSchedule.PKeyCode))
            objCommand.Parameters.Add(New OleDbParameter("@InvDesc", objattInvSchedule.InvDesc))
            objCommand.Parameters.Add(New OleDbParameter("@InvStartDate", objattInvSchedule.InvStartDate))
            objCommand.Parameters.Add(New OleDbParameter("@InvEndDate", objattInvSchedule.InvEndDate))
            objCommand.Parameters.Add(New OleDbParameter("@Isdeleted", 0))
            objCommand.Parameters.Add(New OleDbParameter("@closed", 0))
            objCommand.Parameters.Add(New OleDbParameter("@SchType", objattInvSchedule.InventoryType))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Ast_INV_Schedule")
            strQuery.Append(" set")
            strQuery.Append(" InvDesc=N'" & objattInvSchedule.InvDesc & "',")
            strQuery.Append(" InvStartDate=" & BackEndDate(objattInvSchedule.InvStartDate) & ",")
            strQuery.Append(" InvEndDate=" & BackEndDate(objattInvSchedule.InvEndDate) & ",")
            strQuery.Append(" SchType=" & objattInvSchedule.InventoryType & "")
            strQuery.Append(" where InvSchCode =" & objattInvSchedule.PKeyCode & "")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function GetAll_invSchID(ByVal ID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select InvSchCode,InvDesc,InvStartDate,InvEndDate,Closed,SchType from Ast_INV_Schedule where IsDeleted = 0")
            If ID <> 0 Then
                strQuery.Append(" and InvSchCode = " & ID)
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select InvSchCode,InvDesc,InvStartDate,InvEndDate,Closed,SchType from Ast_INV_Schedule where IsDeleted = 0")
            If objattInvSchedule.PKeyCode <> 0 Then
                strQuery.Append(" and InvSchCode = " & objattInvSchedule.PKeyCode)
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
                strQuery.Append("Delete from Ast_INV_Schedule")
                strQuery.Append(" where InvSchCode =" & objattInvSchedule.PKeyCode)
            Else
                strQuery.Append("update Ast_INV_Schedule")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where InvSchCode =" & objattInvSchedule.PKeyCode)
            End If




            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function



        Public Function Close_Schedule() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Ast_INV_Schedule")
            strQuery.Append(" set")
            strQuery.Append(" Closed=1")
            strQuery.Append(" where InvSchCode =" & objattInvSchedule.PKeyCode)
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(InvSchCode)+1 from Ast_INV_Schedule")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function GetAllData_Combo() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select InvSchCode,InvDesc from Ast_INV_Schedule where IsDeleted = 0 and Closed =0")
            If objattInvSchedule.PKeyCode <> 0 Then
                strQuery.Append(" and InvSchCode = " & objattInvSchedule.PKeyCode)
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function CheckID() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select InvSchCode,InvDesc from Ast_INV_Schedule ")
            If objattInvSchedule.PKeyCode <> 0 Then
                strQuery.Append(" where InvSchCode = " & objattInvSchedule.PKeyCode)
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetInventoryType() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select SchType from Ast_INV_Schedule ")
            strQuery.Append(" where InvSchCode = " & objattInvSchedule.PKeyCode)
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

#End Region

    End Class

End Namespace
