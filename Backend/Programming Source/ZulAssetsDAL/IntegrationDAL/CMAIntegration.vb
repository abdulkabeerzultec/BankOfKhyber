Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text

Namespace ZulAssetsDAL
    Public Class CMAIntegration
        Private _Command As IDbCommand

        Public Function GetAllDataCMAExport(ByVal HideMisplaced As Boolean, ByVal InvSchCode As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("SELECT     AssetDetails.AstID, AssetDetails.AstDesc, AssetDetails.AstModel, AssetDetails.SerailNo, Location.LocationFullPath, Category.CatFullPath")
            strQuery.Append(" FROM         AssetDetails INNER JOIN")
            strQuery.Append(" Location ON Location.LocID = AssetDetails.LocID INNER JOIN")
            strQuery.Append(" Assets ON AssetDetails.ItemCode = Assets.ItemCode INNER JOIN")
            strQuery.Append(" Category ON Assets.AstCatID = Category.AstCatID  INNER JOIN")
            strQuery.Append(" Ast_History ON AssetDetails.AstID= Ast_History.AstID ")
            strQuery.Append(" WHERE     (AssetDetails.IsDeleted = 0) ")
            strQuery.Append(" and Ast_History.Status not in(0,6) and AssetDetails.Disposed= 0")
            If HideMisplaced Then
                strQuery.Append(" and Ast_History.Status <> 2 ") ' 2 Misplaced
            End If
            If InvSchCode > 0 Then
                strQuery.Append(" and Ast_History.InvSchCode  = " & InvSchCode.ToString)
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

    End Class
End Namespace