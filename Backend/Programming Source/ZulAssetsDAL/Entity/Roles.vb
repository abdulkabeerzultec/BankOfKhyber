Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class Roles
        Implements IEntity

#Region "Data Members"
        Private attobj As attRoles
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            attobj = New attRoles
        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return attobj
            End Get
            Set(ByVal Value As IAttribute)
                attobj = CType(Value, attRoles)
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
            strQuery.Append("insert into Roles")
            strQuery.Append("(RoleID,Description,AppUser,CompanyInfo ")
            strQuery.Append(" ,Location,DepPolicy ")
            strQuery.Append(" ,Brands,Designation ")
            strQuery.Append(" ,Department,Custodian ")
            strQuery.Append(" ,Insurer,DisposalMethod ")
            strQuery.Append(" ,Supplier,DepreciationMethod ")
            strQuery.Append(" ,InvSch,AssetsBooks ")
            strQuery.Append(" ,AddressBook,AssetsCat ")
            strQuery.Append(" ,CostCenter,AssetItems ")
            strQuery.Append(" ,Company,PO,POApproval ")
            strQuery.Append(" ,POTrans,DeviceConfig ")
            strQuery.Append(" ,SysConfig,DataSend ")
            strQuery.Append(" ,DataRecieve,Units,AssetCoding ")
            'strQuery.Append(" AstDetails, AstTaging, AssetsLedger, DisposedAssets, soldAssets, DamagedAssets, InterCoTrans, Units, AssetCoding ")
            strQuery.Append(" , AstAdmin, AstDetail, AstTrans, AstSrc, InterComTrans, Anonym, Roles,DepMan,BarStuct,BarCodePolicy,CompLvl,CompGroup,GroupHier,Companies,GLCode,ReportDesigner ,CreateReport,CMAExport,CMAImport,DataProcessing, IsDeleted ")
            strQuery.Append(" ,OfflineMachine,BackendInventory,Custom1,Custom2,Custom3,Custom4,Custom5,Custom6,Custom7,Custom8,Custom9,Custom10,Custom11,Custom12,Custom13,Custom14,Custom15) ")
            strQuery.Append(" Values")
            strQuery.Append("(" & attobj.PKeyCode & ",'" & attobj.Description & "'," & attobj.AppUser & ", " & attobj.CompanyInfo)
            strQuery.Append(", " & attobj.Location & ", " & attobj.DepPolicy)
            strQuery.Append(", " & attobj.Brands & ", " & attobj.Designation)
            strQuery.Append(", " & attobj.Department & ", " & attobj.Custodian)
            strQuery.Append(", " & attobj.Insurer & ", " & attobj.DisposalMethod)
            strQuery.Append(", " & attobj.Supplier & ", " & attobj.DepreciationMethod)
            strQuery.Append(", " & attobj.InvSch & ", " & attobj.AssetsBooks)
            strQuery.Append(", " & attobj.AddressBook & ", " & attobj.AssetsCat)
            strQuery.Append(", " & attobj.CostCenter & ", " & attobj.AssetItems)
            strQuery.Append(", " & attobj.Company & ", " & attobj.PO & ", " & attobj.POApproval)
            strQuery.Append(", " & attobj.POTrans & ", " & attobj.DeviceConfig)
            strQuery.Append(", " & attobj.SysConfig & ", " & attobj.DataSend)
            strQuery.Append(", " & attobj.DataRecieve & ", " & attobj.Units & ", " & attobj.AssetCoding)
            'strQuery.Append(", " & attobj.rptAstDetails & ", " & attobj.rptAstTaging & ", " & attobj.rptAssetsLedger & ", " & attobj.rptDisposedAssets & ", " & attobj.rptsoldAssets & ", " & attobj.rptDamagedAssets & ", " & attobj.rptInterCoTrans & ", " & attobj.rptUnits & ", " & attobj.AssetCoding)
            strQuery.Append(", " & attobj.AstAdmin & ", " & attobj.AstDetail & ", " & attobj.AstTrans & ", " & attobj.AstSrch & ", " & attobj.InterComTrans & ", " & attobj.Anonym & ", " & attobj.Roles & ", " & attobj.DepMan & ", " & attobj.BarStuct & ", " & attobj.BarCodePolicy & "," & attobj.CompLvl & ", " & attobj.CompGroup & ", " & attobj.GroupHier & ", '" & attobj.Companies & "'")

            strQuery.Append(", " & attobj.GLCode)
            strQuery.Append(", " & attobj.ReportDesigner & ", " & attobj.CreateReport & ", " & attobj.CMAExport & ", " & attobj.CMAImport & ", " & attobj.DataProcessing & ", 0 ")
            strQuery.Append(", " & attobj.OfflineMachine)
            strQuery.Append(", " & attobj.BackendInventory)
            strQuery.Append(", " & attobj.Custom1)
            strQuery.Append(", " & attobj.Custom2)
            strQuery.Append(", " & attobj.Custom3)
            strQuery.Append(", " & attobj.Custom4)
            strQuery.Append(", " & attobj.Custom5)
            strQuery.Append(", " & attobj.Custom6)
            strQuery.Append(", " & attobj.Custom7)
            strQuery.Append(", " & attobj.Custom8)
            strQuery.Append(", " & attobj.Custom9)
            strQuery.Append(", " & attobj.Custom10)
            strQuery.Append(", " & attobj.Custom11)
            strQuery.Append(", " & attobj.Custom12)
            strQuery.Append(", " & attobj.Custom13)
            strQuery.Append(", " & attobj.Custom14)
            strQuery.Append(", " & attobj.Custom15 & ")")

            Dim str As String = strQuery.ToString()
            str = str.Replace("True", "1")
            str = str.Replace("False", "0")
            objCommand.CommandText = str
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Roles")
            strQuery.Append(" set")
            strQuery.Append(" AppUser	= " & attobj.AppUser & ",")
            strQuery.Append(" CompanyInfo	= " & attobj.CompanyInfo & ",")
            strQuery.Append(" Description	='" & attobj.Description & "',")
            strQuery.Append(" Location	= " & attobj.Location & ",")
            strQuery.Append(" DepPolicy	= " & attobj.DepPolicy & ",")
            strQuery.Append(" Brands	= " & attobj.Brands & ",")
            strQuery.Append(" Designation	= " & attobj.Designation & ",")
            strQuery.Append(" Department	= " & attobj.Department & ",")
            strQuery.Append(" Custodian	= " & attobj.Custodian & ",")
            strQuery.Append(" Insurer	= " & attobj.Insurer & ",")
            strQuery.Append(" DisposalMethod	= " & attobj.DisposalMethod & ",")
            strQuery.Append(" Supplier	= " & attobj.Supplier & ",")
            strQuery.Append(" DepreciationMethod	= " & attobj.DepreciationMethod & ",")
            strQuery.Append(" InvSch	= " & attobj.InvSch & ",")
            strQuery.Append(" AssetsBooks	= " & attobj.AssetsBooks & ",")
            strQuery.Append(" AddressBook	= " & attobj.AddressBook & ",")
            strQuery.Append(" AssetsCat	= " & attobj.AssetsCat & ",")
            strQuery.Append(" CostCenter	= " & attobj.CostCenter & ",")
            strQuery.Append(" AssetItems	= " & attobj.AssetItems & ",")
            strQuery.Append(" Company	= " & attobj.Company & ",")
            strQuery.Append(" AssetCoding	= " & attobj.AssetCoding & ",")
            strQuery.Append(" PO	= " & attobj.PO & ",")
            strQuery.Append(" POApproval	= " & attobj.POApproval & ",")
            strQuery.Append(" POTrans	= " & attobj.POTrans & ",")
            strQuery.Append(" DeviceConfig	= " & attobj.DeviceConfig & ",")
            strQuery.Append(" SysConfig	= " & attobj.SysConfig & ",")
            strQuery.Append(" BarCodePolicy	= " & attobj.BarCodePolicy & ",")
            strQuery.Append(" DataSend	= " & attobj.DataSend & ",")
            strQuery.Append(" DataRecieve	= " & attobj.DataRecieve & ",")
            strQuery.Append(" Units	= " & attobj.Units & ",")
            strQuery.Append(" AstAdmin	= " & attobj.AstAdmin & ",")
            strQuery.Append(" AstDetail	= " & attobj.AstDetail & ",")
            strQuery.Append(" AstTrans	= " & attobj.AstTrans & ",")
            strQuery.Append(" AstSrc	= " & attobj.AstSrch & ",")
            strQuery.Append(" InterComTrans	= " & attobj.InterComTrans & ",")
            strQuery.Append(" Anonym	= " & attobj.Anonym & ",")
            strQuery.Append(" DepMan	= " & attobj.DepMan & ",")
            strQuery.Append(" BarStuct	= " & attobj.BarStuct & ",")
            strQuery.Append(" Companies	= '" & attobj.Companies & "',")
            strQuery.Append(" CompGroup	= " & attobj.CompGroup & ",")
            strQuery.Append(" CompLvl	= " & attobj.CompLvl & ",")
            strQuery.Append(" GroupHier	= " & attobj.GroupHier & ",")
            strQuery.Append(" Roles	= " & attobj.Roles & ",")

            strQuery.Append(" GLCode	= " & attobj.GLCode & ",")
            strQuery.Append(" CreateReport	= " & attobj.CreateReport & ",")

            strQuery.Append(" CMAExport	= " & attobj.CMAExport & ",")
            strQuery.Append(" CMAImport	= " & attobj.CMAImport & ",")
            strQuery.Append(" DataProcessing	= " & attobj.DataProcessing & ",")

            strQuery.Append(" OfflineMachine = " & attobj.OfflineMachine & ",")
            strQuery.Append(" BackendInventory = " & attobj.BackendInventory & ",")
            strQuery.Append(" Custom1 = " & attobj.Custom1 & ",")
            strQuery.Append(" Custom2 = " & attobj.Custom2 & ",")
            strQuery.Append(" Custom3 = " & attobj.Custom3 & ",")
            strQuery.Append(" Custom4 = " & attobj.Custom4 & ",")
            strQuery.Append(" Custom5 = " & attobj.Custom5 & ",")
            strQuery.Append(" Custom6 = " & attobj.Custom6 & ",")
            strQuery.Append(" Custom7 = " & attobj.Custom7 & ",")
            strQuery.Append(" Custom8 = " & attobj.Custom8 & ",")
            strQuery.Append(" Custom9 = " & attobj.Custom9 & ",")
            strQuery.Append(" Custom10 = " & attobj.Custom10 & ",")
            strQuery.Append(" Custom11 = " & attobj.Custom11 & ",")
            strQuery.Append(" Custom12 = " & attobj.Custom12 & ",")
            strQuery.Append(" Custom13 = " & attobj.Custom13 & ",")
            strQuery.Append(" Custom14 = " & attobj.Custom14 & ",")
            strQuery.Append(" Custom15 = " & attobj.Custom15 & ",")

            strQuery.Append(" ReportDesigner	= " & attobj.ReportDesigner & " ")

            strQuery.Append(" where RoleID =" & attobj.PKeyCode & "")
            Dim str As String = strQuery.ToString()
            str = str.Replace("True", "1")
            str = str.Replace("False", "0")
            objCommand.CommandText = str
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select RoleID,AppUser,CompanyInfo,Description,Location,DepPolicy,Brands,Designation,Department,Custodian,Insurer,DisposalMethod,Supplier,DepreciationMethod,InvSch,AssetsBooks,AddressBook,AssetsCat,CostCenter,AssetItems,Company,PO,POApproval,POTrans,DeviceConfig	,SysConfig,DataSend	 ,DataRecieve,Units ,AstAdmin	,AstDetail	,AstTrans	,AstSrc	,InterComTrans	,Anonym	,Roles,DepMan,BarStuct,BarCodePolicy,AssetCoding,CompLvl,CompGroup,GroupHier,GLCode,Companies,ReportDesigner,CreateReport,CMAExport,CMAImport,DataProcessing,OfflineMachine,BackendInventory,Custom1,Custom2,Custom3,Custom4,Custom5,Custom6,Custom7,Custom8,Custom9,Custom10,Custom11,Custom12,Custom13,Custom14,Custom15	 from Roles where IsDeleted = 0")
            If attobj.PKeyCode <> "" Then
                strQuery.Append(" and RoleID = " & attobj.PKeyCode)
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Update_Role_Co(ByVal RoleID As String, ByVal Comp As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Roles")
            strQuery.Append(" set")
            strQuery.Append(" Companies	= '" & Comp & "'")
            strQuery.Append(" where RoleID =" & RoleID & "")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function GetARole_Combo() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select RoleID,Description from Roles where IsDeleted = 0")
            'If attobj.PKeyCode <> 0 Then
            '    strQuery.Append(" and RoleID = " & attobj.PKeyCode)
            'End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from Roles ")
            Else
                strQuery.Append("update Roles set IsDeleted= 1 ")
            End If
            strQuery.Append(" where RoleID = " & attobj.PKeyCode)

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(RoleID)+1 from Roles")
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
#End Region

    End Class




End Namespace
