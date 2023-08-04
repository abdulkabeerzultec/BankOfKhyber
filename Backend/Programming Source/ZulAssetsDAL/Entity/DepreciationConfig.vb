
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class DepreciationConfig
        Implements IEntity

#Region "Data Members"
        Private objattDepreciationConfig As attDepreciationConfig
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattDepreciationConfig = New attDepreciationConfig
        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattDepreciationConfig
            End Get
            Set(ByVal Value As IAttribute)
                objattDepreciationConfig = CType(Value, attDepreciationConfig)
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
            strQuery.Append("insert into DepreciationConfig")
            strQuery.Append(" (Depr_Period,FinancialYearStart,FinyrStartDate)")
            strQuery.Append(" Values")
            strQuery.Append(" ('" & objattDepreciationConfig.Depr_Period & "','" & objattDepreciationConfig.FinancialYearStart & "') ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update DepreciationConfig")
            strQuery.Append(" set")
            strQuery.Append(" Depr_Period='" & objattDepreciationConfig.Depr_Period & "',")
            strQuery.Append(" FinancialYearStart='" & objattDepreciationConfig.FinancialYearStart & "',")
            strQuery.Append(" FinyrStartDate=# " & objattDepreciationConfig.FinyrStartDate & " #")
            strQuery.Append(" where ID =(Select min(ID) From DepreciationConfig)")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand


            Dim strQuery As New StringBuilder
            strQuery.Append("Select Depr_Period,FinancialYearStart,FinyrStartDate from DepreciationConfig ")
            ' strQuery.Append(" where ID =(Select min(ID) From DepreciationConfig)")


            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
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


                strQuery.Append("delete from  DepreciationConfig")
                strQuery.Append(" where DispCode =" & objattDepreciationConfig.PKeyCode)
            Else

                strQuery.Append("update DepreciationConfig")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where DispCode =" & objattDepreciationConfig.PKeyCode)
            End If


            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(DispCode)+1 from DepreciationConfig")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
            Return objCommand
        End Function
#End Region

    End Class

End Namespace
