Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class Supplier
        Implements IEntity

#Region "Data Members"
        Private objattSupplier As attSupplier
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattSupplier = New attSupplier

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattSupplier
            End Get
            Set(ByVal Value As IAttribute)
                objattSupplier = CType(Value, attSupplier)
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
            strQuery.Append("insert into Supplier")
            strQuery.Append(" (SuppID,SuppName,SuppPhone,SuppEmail,SuppFax,SuppCell,Isdeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" ('" & Convert.ToString(objattSupplier.PKeyCode) & "',N'" & objattSupplier.SuppName & "','" & objattSupplier.SuppPhone & "','" & objattSupplier.SuppEmail & "','" & objattSupplier.SuppFax & "','" & objattSupplier.SuppCell & "',0) ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Supplier")
            strQuery.Append(" set")
            strQuery.Append(" SuppName=N'" & objattSupplier.SuppName & "',")
            strQuery.Append(" SuppPhone='" & objattSupplier.SuppPhone & "',")
            strQuery.Append(" SuppEmail='" & objattSupplier.SuppEmail & "',")
            strQuery.Append(" SuppFax='" & objattSupplier.SuppFax & "',")
            strQuery.Append(" SuppCell='" & objattSupplier.SuppCell & "'")
            strQuery.Append(" where SuppID ='" & objattSupplier.PKeyCode & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select  SuppID,SuppName,SuppCell,SuppFax,SuppPhone,SuppEmail,IsDeleted from Supplier where IsDeleted = 0")

            If objattSupplier.PKeyCode <> "" Then
                strQuery.Append(" and SuppID = '" & objattSupplier.PKeyCode & "'")
            End If

            If objattSupplier.SuppName <> "" Then
                strQuery.Append(" and SuppName = '" & objattSupplier.SuppName & "'")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function CheckID() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select * from Supplier")
            If objattSupplier.PKeyCode <> "" Then
                strQuery.Append(" where SuppID = '" & objattSupplier.PKeyCode & "'")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function


        Public Function GetCombo() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select SuppID,SuppName from Supplier where IsDeleted = 0")
            If objattSupplier.PKeyCode <> "" Then
                strQuery.Append(" and SuppID = '" & objattSupplier.PKeyCode & "'")
            End If
            strQuery.Append(" Order by SuppName")
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
                strQuery.Append("delete from  Supplier")
                strQuery.Append(" where SuppID = ?")
            Else
                strQuery.Append("update Supplier")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where SuppID = ?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@SuppID", objattSupplier.PKeyCode))


            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(cast(SuppID as int )) + 1 from Supplier")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
#End Region

    End Class

End Namespace

