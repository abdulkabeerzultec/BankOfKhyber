Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text

Namespace ZulAssetsDAL

    Public Class SysParam
        Private Shared _MaxLocationLevel As Integer
        Private Shared _MaxCategoryLevel As Integer
        Private Shared _AssetlocationMinlevel As Integer
        Private Shared _AssetCategoryMinlevel As Integer

        Public Shared Property MaxLocationLevel() As Integer
            Get
                Return _MaxLocationLevel
            End Get
            Set(ByVal value As Integer)
                _MaxLocationLevel = value
            End Set
        End Property

        Public Shared Property MaxCategoryLevel() As Integer
            Get
                Return _MaxCategoryLevel
            End Get
            Set(ByVal value As Integer)
                _MaxCategoryLevel = value
            End Set
        End Property

        Public Shared Property AssetlocationMinlevel() As Integer
            Get
                Return _AssetlocationMinlevel
            End Get
            Set(ByVal value As Integer)
                _AssetlocationMinlevel = value
            End Set
        End Property

        Public Shared Property AssetCategoryMinlevel() As Integer
            Get
                Return _AssetCategoryMinlevel
            End Get
            Set(ByVal value As Integer)
                _AssetCategoryMinlevel = value
            End Set
        End Property

        Public Shared Function GetParamByName(ByVal Name As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select VALUE from SYSPARAMETER where NAME=?")
            objCommand.Parameters.Add(New OleDbParameter("@NAME", Name))
            objCommand.CommandText = strQuery.ToString()
            Return objCommand
        End Function

        Public Shared Function SaveParamValue(ByVal Name As String, ByVal Value As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update SYSPARAMETER set value = ? where NAME=?")
            objCommand.Parameters.Add(New OleDbParameter("@Value", Value))
            objCommand.Parameters.Add(New OleDbParameter("@NAME", Name))
            objCommand.CommandText = strQuery.ToString()
            Return objCommand
        End Function

    End Class
End Namespace
