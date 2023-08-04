Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class PODetails
        Implements IEntity
#Region "Data Members"
        Private objattPODetails As attPODetails
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattPODetails = New attPODetails
        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattPODetails
            End Get
            Set(ByVal Value As IAttribute)
                objattPODetails = CType(Value, attPODetails)
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
            strQuery.Append("insert into PODetails")
            strQuery.Append(" (POItmID,POCode,UnitID,POItmDesc,AddCharges,POItmBaseCost,POItmQty,ItemCode,Isdeleted,IsTrans,PORecQty)")
            strQuery.Append(" Values")
            strQuery.Append(" (" & Convert.ToString(objattPODetails.PKeyCode) & ",'" & objattPODetails.POCode & "'," & objattPODetails.unit & ",'" & objattPODetails.POItmDesc & "'," & objattPODetails.AddCharges & "," & objattPODetails.POItmBaseCost & "," & objattPODetails.POItmQty & ",'" & objattPODetails.ItemCode & "',0,0," & objattPODetails.PORecQty & ")")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update PODetails")
            strQuery.Append(" set")
            strQuery.Append(" POCode='" & objattPODetails.POCode & "',")
            strQuery.Append(" POItmDesc='" & objattPODetails.POItmDesc & "',")
            strQuery.Append(" UnitID=" & objattPODetails.unit & ",")
            strQuery.Append(" POItmBaseCost=" & objattPODetails.POItmBaseCost & ",")
            strQuery.Append(" AddCharges=" & objattPODetails.AddCharges & ",")
            strQuery.Append(" POItmQty=" & objattPODetails.POItmQty & ",")
            strQuery.Append(" PORecQty=" & objattPODetails.PORecQty & ",")
            strQuery.Append(" ItemCode='" & objattPODetails.ItemCode & "'")
            strQuery.Append(" where POItmID =" & objattPODetails.PKeyCode & "")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand
            'Dont Change this Query. it may cause other form to stop working perfectly

            Dim strQuery As New StringBuilder
            strQuery.Append("Select  PODetails.POItmID,PODetails.POCode,PODetails.ItemCode,PODetails.POItmDesc,PODetails.POItmBaseCost,PODetails.AddCharges,PODetails.POItmQty,PODetails.Isdeleted,PODetails.IsTrans,Assets.ItemCode,Assets.AstBrandID,Assets.AstCatID,Assets.POItmID,Assets.AstDesc,Assets.AstModel,Assets.AstQty,Assets.ISDeleted,Category.AstCatID,Category.AstCatDesc,Category.IsDeleted,Category.ID1, Units.UnitID,Units.UnitDesc,PORecQty ")
            strQuery.Append(" from Assets,Category,PODetails ")
            strQuery.Append(" left outer join Units on Units.UnitID = PODetails.UnitID ")
            strQuery.Append(" where PODetails.IsDeleted = 0 and PODetails.ItemCode = Assets.ItemCode  and Category.AstCatID = Assets.AstCatID ")
            If objattPODetails.PKeyCode <> "" Then
                strQuery.Append(" and PODetails.POItmID = '" & objattPODetails.PKeyCode & "'")
            End If
            If objattPODetails.POCode <> "" Then
                strQuery.Append(" and PODetails.POCode = '" & objattPODetails.POCode & "'")
            End If
            strQuery.Append(" order by PODetails.POItmID ")
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

        Public Function Transfer() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update PODetails")
            strQuery.Append(" set")
            strQuery.Append(" IsTrans=1")
            strQuery.Append(" ,PORecQty = " & objattPODetails.PORecQty)
            strQuery.Append(" where POItmID ='" & objattPODetails.PKeyCode & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function UpdateRecQty() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update PODetails")
            strQuery.Append(" set")
            strQuery.Append(" PORecQty = " & objattPODetails.PORecQty)
            strQuery.Append(" where POItmID ='" & objattPODetails.PKeyCode & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from PODetails")
                strQuery.Append(" where POCode =?")
                objCommand.Parameters.Add(New OleDbParameter("@POCode", objattPODetails.POCode))
                If objattPODetails.PKeyCode <> "" Then
                    strQuery.Append(" and  POItmID =?")
                    objCommand.Parameters.Add(New OleDbParameter("@POItmID", objattPODetails.PKeyCode))
                End If
            Else
                strQuery.Append("update PODetails")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where POCode =?")
                objCommand.Parameters.Add(New OleDbParameter("@POCode", objattPODetails.POCode))
                If objattPODetails.PKeyCode <> "" Then
                    strQuery.Append(" and  POItmID =?")
                    objCommand.Parameters.Add(New OleDbParameter("@POItmID", objattPODetails.PKeyCode))
                End If
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(POItmID) + 1 from PODetails")
            objCommand.CommandText = strQuery.ToString()
            Return objCommand

        End Function
#End Region

    End Class
End Namespace
