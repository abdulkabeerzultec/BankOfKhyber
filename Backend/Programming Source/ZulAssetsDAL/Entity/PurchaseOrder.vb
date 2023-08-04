Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class PurchaseOrder
        Implements ZulAssetsDAL.IEntity
#Region "Data Members"
        Private objattPurchaseOrder As ZulAssetsDAL.attPurchaseOrder
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattPurchaseOrder = New ZulAssetsDAL.attPurchaseOrder

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As ZulAssetsDAL.IAttribute Implements ZulAssetsDAL.IEntity.ObjAttribute
            Get
                Return objattPurchaseOrder
            End Get
            Set(ByVal Value As ZulAssetsDAL.IAttribute)
                objattPurchaseOrder = CType(Value, ZulAssetsDAL.attPurchaseOrder)
            End Set
        End Property

        Public Property ObjCommand() As IDbCommand Implements ZulAssetsDAL.IEntity.ObjCommand
            Get
                Return _Command
            End Get
            Set(ByVal Value As IDbCommand)
                _Command = CType(Value, IDbCommand)
            End Set
        End Property
#End Region

#Region "Methods"
        Public Function Check_Child(ByVal CostID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select Count(*) from PurchaseOrder ")
            strQuery.Append(" where IsDeleted = 0")
            If CostID <> "" Then
                strQuery.Append(" and CostID = '" & CostID & "'")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Insert() As IDbCommand Implements ZulAssetsDAL.IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into PurchaseOrder")
            strQuery.Append(" (POCode,SuppID,PODate,Quotation,Amount,AddCharges,ModeDelivery,Payterm,Remarks,Approvedby,Preparedby,POStatus,Isdeleted,IsTrans,ReferenceNo,CostID,Requestedby,TermnCon,Discount)")
            strQuery.Append(" Values")
            strQuery.Append(" ('" & Convert.ToString(objattPurchaseOrder.PKeyCode) & "','" & objattPurchaseOrder.SuppID & "'," & BackEndDate(objattPurchaseOrder.Date1) & ",'" & objattPurchaseOrder.Quotation & "'," & objattPurchaseOrder.Amount & "," & objattPurchaseOrder.AddCharges & ",'" & objattPurchaseOrder.ModeDelivery & "','" & objattPurchaseOrder.Payterm & "','" & objattPurchaseOrder.Remarks & "','" & objattPurchaseOrder.Approvedby & "','" & objattPurchaseOrder.Preparedby & "'," & objattPurchaseOrder.POStatus & ",0,0 ,'" & objattPurchaseOrder.ReferenceNo & "','" & objattPurchaseOrder.CostID & "','" & objattPurchaseOrder.Requestedby & "','" & objattPurchaseOrder.TermnCon & "'," & objattPurchaseOrder.Discount & ") ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements ZulAssetsDAL.IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update PurchaseOrder")
            strQuery.Append(" set")
            strQuery.Append(" SuppID='" & objattPurchaseOrder.SuppID & "',")
            strQuery.Append(" PODate=" & BackEndDate(objattPurchaseOrder.Date1) & ",")
            strQuery.Append(" Quotation='" & objattPurchaseOrder.Quotation & "',")
            strQuery.Append(" Amount=" & objattPurchaseOrder.Amount & ",")
            strQuery.Append(" AddCharges=" & objattPurchaseOrder.AddCharges & ",")
            strQuery.Append(" Discount=" & objattPurchaseOrder.Discount & ",")
            strQuery.Append(" ModeDelivery='" & objattPurchaseOrder.ModeDelivery & "',")
            strQuery.Append(" Payterm='" & objattPurchaseOrder.Payterm & "',")
            strQuery.Append(" Remarks='" & objattPurchaseOrder.Remarks & "',")
            strQuery.Append(" Approvedby='" & objattPurchaseOrder.Approvedby & "',")
            strQuery.Append(" ReferenceNo='" & objattPurchaseOrder.ReferenceNo & "',")
            strQuery.Append(" CostID='" & objattPurchaseOrder.CostID & "',")
            strQuery.Append(" Requestedby='" & objattPurchaseOrder.Requestedby & "',")
            strQuery.Append(" TermnCon='" & objattPurchaseOrder.TermnCon & "',")
            strQuery.Append(" Preparedby='" & objattPurchaseOrder.Preparedby & "',")
            strQuery.Append(" POStatus=" & objattPurchaseOrder.POStatus & "")
            strQuery.Append(" where POCode ='" & objattPurchaseOrder.PKeyCode & "'")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function UpdatePOStatus() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update PurchaseOrder")
            strQuery.Append(" set")
            strQuery.Append(" Approvedby='" & objattPurchaseOrder.Approvedby & "',")
            strQuery.Append(" POStatus=" & objattPurchaseOrder.POStatus & "")
            strQuery.Append(" where POCode ='" & objattPurchaseOrder.PKeyCode & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function GetAllData() As IDbCommand Implements ZulAssetsDAL.IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select *  from PurchaseOrder")
            strQuery.Append(" left outer join Supplier on PurchaseOrder.SuppID = Supplier.SuppID")
            strQuery.Append(" left outer join Custodian on PurchaseOrder.Requestedby= Custodian.CustodianID")
            strQuery.Append(" left outer join CostCenter on PurchaseOrder.CostID = CostCenter.CostID")
            strQuery.Append(" where PurchaseOrder.IsDeleted = 0")
            If objattPurchaseOrder.PKeyCode <> "" Then
                strQuery.Append(" and POCode = '" & objattPurchaseOrder.PKeyCode & "'")
            End If
            If objattPurchaseOrder.POStatus <> 0 Then
                strQuery.Append(" and POStatus = " & objattPurchaseOrder.POStatus & "")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Transfer() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update PurchaseOrder")
            strQuery.Append(" set")
            strQuery.Append(" IsTrans=1, ")
            strQuery.Append(" POStatus = 3")
            strQuery.Append(" where POCode =" & objattPurchaseOrder.PKeyCode)
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetCombo() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select POCode,Quotation,PODate from PurchaseOrder where IsDeleted = 0")
            If objattPurchaseOrder.POStatus <> 0 Then
                strQuery.Append(" and POStatus = " & objattPurchaseOrder.POStatus & "")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function





        Public Function GetDatabyID(ByVal Id As String) As IDbCommand Implements ZulAssetsDAL.IEntity.GetDatabyID
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select * from GrpChild")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
            Return objCommand
        End Function


        Public Function Delete() As IDbCommand Implements ZulAssetsDAL.IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder



            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from PurchaseOrder")
                strQuery.Append(" where POCode =" & objattPurchaseOrder.PKeyCode)
                objCommand.CommandText = strQuery.ToString()
            Else
                strQuery.Append("update PurchaseOrder")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where POCode =" & objattPurchaseOrder.PKeyCode)
                objCommand.CommandText = strQuery.ToString()
            End If


            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements ZulAssetsDAL.IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select max(POCode) + 1 from PurchaseOrder")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
#End Region

    End Class
End Namespace
