Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text

Namespace ZulAssetsDAL
    Public Class AssetWarranty
        Implements IEntity

#Region "Data Members"
        Private objattAssetWarranty As attAssetWarranty
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattAssetWarranty = New attAssetWarranty
        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattAssetWarranty
            End Get
            Set(ByVal Value As IAttribute)
                objattAssetWarranty = CType(Value, attAssetWarranty)
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
            strQuery.Append("insert into AssetWarranty")
            strQuery.Append(" (ID,AstID,WarrantyStart,WarrantyPeriodMonth,AlarmBeforeDays,AlarmActivated)")
            strQuery.Append(" Values(?,?,?,?,?,?)")

            objCommand.Parameters.Add(New OleDbParameter("@ID", objattAssetWarranty.ID))
            objCommand.Parameters.Add(New OleDbParameter("@AstID", objattAssetWarranty.AstID))
            objCommand.Parameters.Add(New OleDbParameter("@WarrantyStart", objattAssetWarranty.WarrantyStart))
            objCommand.Parameters.Add(New OleDbParameter("@WarrantyPeriodMonth", objattAssetWarranty.WarrantyPeriodMonth))
            objCommand.Parameters.Add(New OleDbParameter("@AlarmBeforeDays", objattAssetWarranty.AlarmBeforeDays))
            objCommand.Parameters.Add(New OleDbParameter("@AlarmActivated", objattAssetWarranty.AlarmActivated))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetWarranty")
            strQuery.Append(" set AstID =?,WarrantyStart =?,WarrantyPeriodMonth =?,AlarmBeforeDays =?,AlarmActivated =? ")
            strQuery.Append(" where ID =?")

            objCommand.Parameters.Add(New OleDbParameter("@AstID", objattAssetWarranty.AstID))
            objCommand.Parameters.Add(New OleDbParameter("@WarrantyStart", objattAssetWarranty.WarrantyStart))
            objCommand.Parameters.Add(New OleDbParameter("@WarrantyPeriodMonth", objattAssetWarranty.WarrantyPeriodMonth))
            objCommand.Parameters.Add(New OleDbParameter("@AlarmBeforeDays", objattAssetWarranty.AlarmBeforeDays))
            objCommand.Parameters.Add(New OleDbParameter("@AlarmActivated", objattAssetWarranty.AlarmActivated))
            objCommand.Parameters.Add(New OleDbParameter("@ID", objattAssetWarranty.ID))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function DismissWarranty(ByVal ID As Int64) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetWarranty")
            strQuery.Append(" set AlarmActivated =0 ")
            strQuery.Append(" where ID =?")

            objCommand.Parameters.Add(New OleDbParameter("@ID", ID))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function DismissAllWarranty() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetWarranty")
            strQuery.Append(" set AlarmActivated =0 ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            If AppConfig.DbType = "1" Then
                strQuery.Append("SELECT ID,AstID,WarrantyStart,WarrantyPeriodMonth,AlarmBeforeDays,dateadd(month,WarrantyPeriodMonth,WarrantyStart)  as ExpiryDate,AlarmActivated  FROM AssetWarranty where 1=1 ")
            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append("SELECT ID,AstID,WarrantyStart,WarrantyPeriodMonth,AlarmBeforeDays,ADD_MONTHS( WarrantyStart, WarrantyPeriodMonth) as ExpiryDate,AlarmActivated  FROM AssetWarranty where 1=1 ")
            End If
            If objattAssetWarranty.AstID <> 0 Then
                strQuery.Append(" and AstID = ?")
                objCommand.Parameters.Add(New OleDbParameter("@AstID", objattAssetWarranty.AstID))
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllAlarmData(ByVal AlarmBeforeDays As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DbType = "1" Then
                strQuery.Append("select case ")
                strQuery.Append("when datediff(day,getdate(), dateadd(month,WarrantyPeriodMonth,WarrantyStart)) < 0 then 1 ")
                strQuery.Append("when datediff(day,getdate(), dateadd(month,WarrantyPeriodMonth,WarrantyStart)) between 0 and ? then 2 ")
                strQuery.Append("end as IconIndex ")
                strQuery.Append(",Assetwarranty.ID, Assetwarranty.astID as AssetID,AssetDetails.AstNum as AssetNumber,AssetDetails.AstDesc as Description,AssetDetails.RefNo,WarrantyStart, ")
                strQuery.Append(" dateadd(month,WarrantyPeriodMonth,WarrantyStart)  as ExpiryDate,WarrantyPeriodMonth ")
                strQuery.Append(" , datediff(day,getdate(), dateadd(month,WarrantyPeriodMonth,WarrantyStart) ) as WarrantydueDays ")
                strQuery.Append(" ,'Dismiss' as Dismiss ")
                strQuery.Append("  from Assetwarranty ")
                strQuery.Append("inner join AssetDetails on AssetDetails.AstID = Assetwarranty.AstID ")
                strQuery.Append("and datediff(day,getdate(), dateadd(month,WarrantyPeriodMonth,WarrantyStart)) <= ?  ")
                strQuery.Append(" and Assetwarranty.AlarmActivated = 1 ")
            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append("select case ")
                strQuery.Append("when sysdate - ADD_MONTHS( WarrantyStart, WarrantyPeriodMonth) < 0 then 1 ")
                strQuery.Append("when sysdate - ADD_MONTHS( WarrantyStart, WarrantyPeriodMonth) between 0 and ? then 2 ")
                strQuery.Append("end as IconIndex ")
                strQuery.Append(",Assetwarranty.ID, Assetwarranty.astID as AssetID,AssetDetails.AstNum as AssetNumber,AssetDetails.AstDesc as Description,AssetDetails.RefNo,WarrantyStart, ")
                strQuery.Append(" ADD_MONTHS( WarrantyStart, WarrantyPeriodMonth) as ExpiryDate,WarrantyPeriodMonth ")
                strQuery.Append(" , sysdate - ADD_MONTHS( WarrantyStart, WarrantyPeriodMonth) as WarrantydueDays ")
                strQuery.Append(" ,'Dismiss' as Dismiss ")
                strQuery.Append("  from Assetwarranty ")
                strQuery.Append("inner join AssetDetails on AssetDetails.AstID = Assetwarranty.AstID ")
                strQuery.Append("and sysdate - ADD_MONTHS( WarrantyStart, WarrantyPeriodMonth) <= ?  ")
                strQuery.Append(" and Assetwarranty.AlarmActivated = 1 ")
            End If

            objCommand.CommandText = strQuery.ToString()
            objCommand.Parameters.Add(New OleDbParameter("@AlarmBeforeDays", AlarmBeforeDays))
            objCommand.Parameters.Add(New OleDbParameter("@AlarmBeforeDays", AlarmBeforeDays))
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("delete from AssetWarranty")
            strQuery.Append(" where ID =?")
        
            objCommand.Parameters.Add(New OleDbParameter("@ID", objattAssetWarranty.ID))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function DeleteByAstID() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from AssetWarranty")
                strQuery.Append(" where AstID =?")
            Else
                strQuery.Append("update AssetWarranty")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where AstID =?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@AstID", objattAssetWarranty.AstID))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(ID) + 1  from AssetWarranty ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetDatabyID(ByVal Id As String) As IDbCommand Implements IEntity.GetDatabyID
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select * from AssetWarranty where ID = ?")
            objCommand.Parameters.Add(New OleDbParameter("@ID", Id))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function CheckWarrantyStartDate() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select Count(*) from dbo.AssetWarranty where AstID = ?  and ? between warrantystart and dateadd(month,WarrantyPeriodMonth,warrantystart) and ID<>?")
            objCommand.Parameters.Add(New OleDbParameter("@AstID", objattAssetWarranty.AstID))
            objCommand.Parameters.Add(New OleDbParameter("@WarrantyStart", objattAssetWarranty.WarrantyStart))
            objCommand.Parameters.Add(New OleDbParameter("@ID", objattAssetWarranty.ID))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
#End Region
    End Class


End Namespace