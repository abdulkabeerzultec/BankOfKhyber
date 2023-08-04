Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class HierGrps
        Implements IEntity

#Region "Data Members"
        Private objatthirgrp As attHierGrps
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objatthirgrp = New attHierGrps

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objatthirgrp
            End Get
            Set(ByVal Value As IAttribute)
                objatthirgrp = CType(Value, attHierGrps)
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
            strQuery.Append("insert into HierGrps")
            strQuery.Append(" (GrpCode,GrpDesc,LvlCode,Isdeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" ('" & Convert.ToString(objatthirgrp.GrpCode) & "',' " & objatthirgrp.GrpDesc & "' ," & Convert.ToString(objatthirgrp.LvlCode) & ",0) ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update HierGrps")
            strQuery.Append(" set")
            strQuery.Append(" GrpDesc='" & objatthirgrp.GrpDesc & "',")
            strQuery.Append(" LvlCode='" & objatthirgrp.LvlCode & "'")
            strQuery.Append(" where GrpCode =" & objatthirgrp.PKeyCode)
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select GrpCode,GrpDesc,LvlCode from HierGrps where IsDeleted = 0")
            If objatthirgrp.PKeyCode <> "" Then
                strQuery.Append(" and GrpCode = '" & objatthirgrp.PKeyCode & "'")
            End If
            If objatthirgrp.LvlCode.ToString <> "0" Then
                strQuery.Append(" and LvlCode = " & objatthirgrp.LvlCode.ToString() & "")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function GetDatabyID(ByVal Id As String) As IDbCommand Implements IEntity.GetDatabyID
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select lvlCode as Code,LvlDesc as Description from levels where lvlCode = '" & Id & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Return Nothing
        End Function


        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from levels")
                strQuery.Append(" where  GrpCode =?")
            Else
                strQuery.Append("update levels")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where GrpCode =?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@GrpCode", objatthirgrp.PKeyCode))


            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
#End Region

    End Class
End Namespace


