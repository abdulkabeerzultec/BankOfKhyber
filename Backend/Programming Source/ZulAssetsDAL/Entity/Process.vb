
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class Process

#Region "Methods"
        Public Function GetAll_AssetsTemp(ByVal strDevice As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select DeviceID,AstID,AstDesc,AstCatID,LocID,Status  from AssetsTemp where DeviceID=" & strDevice & "")
            objCommand.CommandText = strQuery.ToString()
            Return objCommand
        End Function

        Public Function GetAll_AssetTransferTemp(ByVal strAstID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select DEviceID,AstTransID,AstID,ToLoc,FromLoc,TransDate from AssetTransferTemp where AstTransId = (Select max(AstTransID) from AssetTransferTemp where AstId = '" & strAstID & " ')")
            objCommand.CommandText = strQuery.ToString()
            Return objCommand
        End Function

        Public Function GetAll_NonBarCodedTemp() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select NonBCode,DeviceID,LocID,AstCatID,AstDesc from NonBarCodedTemp")
            objCommand.CommandText = strQuery.ToString()
            Return objCommand
        End Function

        Public Function Default_DepreciationTypes() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select NonBCode,DeviceID,LocID,AstCatID,AstDesc from NonBarCodedTemp")
            objCommand.CommandText = strQuery.ToString()
            Return objCommand
        End Function


        Public Function Default_DisposalMethod() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select NonBCode,DeviceID,LocID,AstCatID,AstDesc from NonBarCodedTemp")
            objCommand.CommandText = strQuery.ToString()
            Return objCommand
        End Function


        Public Function Default_Roles() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select NonBCode,DeviceID,LocID,AstCatID,AstDesc from NonBarCodedTemp")
            objCommand.CommandText = strQuery.ToString()
            Return objCommand
        End Function


        Public Function Default_Users() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select NonBCode,DeviceID,LocID,AstCatID,AstDesc from NonBarCodedTemp")
            objCommand.CommandText = strQuery.ToString()
            Return objCommand
        End Function
#End Region

    End Class

End Namespace
