Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class BarCode_Struct
        Implements IEntity

#Region "Data Members"
        Private objattBarCode_Struct As attBarCode_Struct
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattBarCode_Struct = New attBarCode_Struct

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattBarCode_Struct
            End Get
            Set(ByVal Value As IAttribute)
                objattBarCode_Struct = CType(Value, attBarCode_Struct)
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
        Public Function Check_BarCode_Struct(ByVal _id As String, ByVal formid As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select * from Companies where IsDeleted = 0")
            strQuery.Append(" and  BarStructID =" & _id & "")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Insert() As IDbCommand Implements IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into BarCode_Struct")
            strQuery.Append(" (BarStructID,BarStructDesc,BarStructLength,BarStructPreFix,ValueSep,BarCode,IsDeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" (" & Convert.ToString(objattBarCode_Struct.PKeyCode) & ",'" & objattBarCode_Struct.BarStructDesc & "'," & objattBarCode_Struct.BarStructLength & ",'" & objattBarCode_Struct.BarStructPreFix & "','" & objattBarCode_Struct.ValueSep & "','" & objattBarCode_Struct.BarCode & "',0) ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update BarCode_Struct")
            strQuery.Append(" set")
            strQuery.Append(" BarStructDesc='" & objattBarCode_Struct.BarStructDesc & "',")
            strQuery.Append(" BarStructLength=" & objattBarCode_Struct.BarStructLength & ",")
            strQuery.Append(" BarStructPreFix='" & objattBarCode_Struct.BarStructPreFix & "',")
            strQuery.Append(" ValueSep='" & objattBarCode_Struct.ValueSep & "',")
            strQuery.Append(" BarCode='" & objattBarCode_Struct.BarCode & "'")
            strQuery.Append(" where BarStructID =" & objattBarCode_Struct.PKeyCode & "")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function GetAllData_Combo() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select BarStructID,BarCode from BarCode_Struct where IsDeleted = 0")
            If objattBarCode_Struct.PKeyCode <> 0 Then
                strQuery.Append(" and BarStructID = " & objattBarCode_Struct.PKeyCode)
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select BarStructID,BarStructDesc,BarStructLength,BarStructPreFix,ValueSep,BarCode from BarCode_Struct where IsDeleted = 0")
            If objattBarCode_Struct.PKeyCode <> 0 Then
                strQuery.Append(" and BarStructID = " & objattBarCode_Struct.PKeyCode)
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from BarCode_Struct")
                strQuery.Append(" where BarStructID =?")
            Else
                strQuery.Append("update BarCode_Struct")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where BarStructID =?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@BarStructID", objattBarCode_Struct.PKeyCode))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(BarStructID)+1 from BarCode_Struct")
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
#End Region

    End Class

End Namespace
