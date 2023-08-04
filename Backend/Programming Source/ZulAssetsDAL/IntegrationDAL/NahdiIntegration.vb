Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text

Namespace ZulAssetsDAL
    Public Class NahdiIntegration

        Private _Command As IDbCommand
        Public Function GetData(ByVal InvSchCode As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("SELECT      AssetDetails.AstNum as 'Asset Number', AssetDetails.AstDesc as Description, AssetDetails.NoPiece as 'Units(System)',Ast_History.NoPiece as 'Units(Physical)', AssetDetails.Barcode as 'Tag Number', AssetDetails.SerailNo as 'Serial Number', GLCodes.GLDesc as Book, Category.Code as CategoryID,Category.CatFullPath ,Location.Code as LocationID ,Location.LocationFullPath, CostCenter.CostName 'Department Desc.',AssetDetails.AstModel as Model ,Custodian.CustodianID as 'Employee ID',Custodian.CustodianName as 'Employee Name',Custodian.CustodianAddress as 'Employee No.', Ast_History.HisDate as 'Inventory Date'")
            strQuery.Append(" FROM         AssetDetails INNER JOIN")
            strQuery.Append(" Location ON Location.LocID = AssetDetails.LocID INNER JOIN")
            strQuery.Append(" Assets ON AssetDetails.ItemCode = Assets.ItemCode INNER JOIN")
            strQuery.Append(" Custodian ON AssetDetails.CustodianID = Custodian.CustodianID INNER JOIN")
            strQuery.Append(" Category ON Assets.AstCatID = Category.AstCatID  INNER JOIN")
            strQuery.Append(" GLCodes ON AssetDetails.GLCode = GLCodes.GLCode  INNER JOIN")
            strQuery.Append(" CostCenter ON AssetDetails.CostCenterID = CostCenter.CostID")
            strQuery.Append(" inner join Ast_History On Ast_History.AstID = AssetDetails.AstID ")
            If InvSchCode > 0 Then
                strQuery.Append(" WHERE     AssetDetails.IsDeleted = 0 and Disposed = 0 and Ast_History.InvSchCode = " & InvSchCode.ToString)
            Else
                strQuery.Append(" WHERE     AssetDetails.IsDeleted = 0 and Disposed = 0 and Ast_History.InvSchCode =1")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function DisableAssetTriggers() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("ALTER TABLE AssetDetails DISABLE TRIGGER ALL")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function EnableAssetTriggers() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("ALTER TABLE AssetDetails ENABLE TRIGGER ALL")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function DisposeAllAssets() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            If AppConfig.DbType = "1" Then
                strQuery.Append("UPDATE AssetDetails SET Disposed = 1, DispDate=Getdate() where Disposed = 0")
            Else
                strQuery.Append("UPDATE AssetDetails SET Disposed = 1, DispDate=sysdate where Disposed = 0")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
    End Class
End Namespace