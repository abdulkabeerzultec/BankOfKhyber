Imports DevExpress.XtraNavBar
Imports System.Windows.Forms
Imports DevExpress.XtraTab
Imports ZulAssetsDAL.ZulAssetsDAL
Imports ZulAssetsBL.ZulAssetBAL

Public Class Plugin
    Implements AddInInterface.IAddIn
    Private _frmDataProcess As Form
    Private _MdiParent As Form
    Public Property MdiParent() As Form Implements AddInInterface.IAddIn.MainForm
        Get
            Return _MdiParent
        End Get
        Set(ByVal value As Form)
            _MdiParent = value
        End Set
    End Property

    Private _UserGUID As Guid
    Public Property UserGUID() As Guid Implements AddInInterface.IAddIn.UserGUID
        Get
            Return _UserGUID
        End Get
        Set(ByVal value As Guid)
            _UserGUID = value
        End Set
    End Property

    Private _UserName As String
    Public Property UserName() As String Implements AddInInterface.IAddIn.UserName
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property

    Private _SQLPort As String
    Public Property SQLPort() As String Implements AddInInterface.IAddIn.SQLPort
        Get
            Return _SQLPort
        End Get
        Set(ByVal value As String)
            _SQLPort = value
        End Set
    End Property

    Private _RoleID As Integer
    Public Property RoleID() As Integer Implements AddInInterface.IAddIn.RoleID
        Get
            Return _RoleID
        End Get
        Set(ByVal value As Integer)
            _RoleID = value
        End Set
    End Property

    Private _IsDemokey As Boolean
    Public Property IsDemokey() As Boolean Implements AddInInterface.IAddIn.IsDemokey
        Get
            Return _IsDemokey
        End Get
        Set(ByVal value As Boolean)
            _IsDemokey = value
        End Set
    End Property


    Private _DBServerName As String
    Public Property DBServerName() As String Implements AddInInterface.IAddIn.DBServerName
        Get
            Return _DBServerName
        End Get
        Set(ByVal value As String)
            _DBServerName = value
        End Set
    End Property


    Private _DbName As String
    Public Property DbName() As String Implements AddInInterface.IAddIn.DbName
        Get
            Return _DbName
        End Get
        Set(ByVal value As String)
            _DbName = value
        End Set
    End Property


    Private _DbUserName As String
    Public Property DbUserName() As String Implements AddInInterface.IAddIn.DbUserName
        Get
            Return _DbUserName
        End Get
        Set(ByVal value As String)
            _DbUserName = value
        End Set
    End Property

    Private _DbUserPass As String
    Public Property DbUserPass() As String Implements AddInInterface.IAddIn.DbUserPass
        Get
            Return _DbUserPass
        End Get
        Set(ByVal value As String)
            _DbUserPass = value
        End Set
    End Property


    Private _CompanyFileName As String
    Public Property CompanyFileName() As String Implements AddInInterface.IAddIn.CompanyFileName
        Get
            Return _CompanyFileName
        End Get
        Set(ByVal value As String)
            _CompanyFileName = value
        End Set
    End Property

    Public Property frmDataProcess() As Form Implements AddInInterface.IAddIn.frmDataProcess
        Get
          
            Return _frmDataProcess
        End Get
        Set(ByVal value As Form)
            _frmDataProcess = value
        End Set
    End Property


    Public ReadOnly Property ActionName() As String Implements AddInInterface.IAddIn.ActionName
        Get
            Return "AddNavMenuItem"
        End Get
    End Property

    Public ReadOnly Property IntegrationName() As String Implements AddInInterface.IAddIn.IntegrationName
        Get
            Return "KPMGIntegration"
        End Get
    End Property

    Public Sub btnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim frm As New frmImportSettings
        frm.RoleID = RoleID
        frm.MdiParent = MdiParent
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.Show()
        frm.BringToFront()
    End Sub

    Public Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim frm As New frmExport
        frm.MdiParent = MdiParent
        frm.WindowState = FormWindowState.Normal
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.Show()
        frm.BringToFront()
    End Sub

    Public Sub AddNavMenuItems(ByRef MainMenu As System.Windows.Forms.MenuStrip, ByRef SubMenuItem As ToolStripMenuItem) Implements AddInInterface.IAddIn.AddMenuItems
        GetConnectionString()

        Dim AllowImport As Boolean = True
        Dim AllowExport As Boolean = True
        Dim AllowDetails As Boolean = True
        Dim AllowAdmin As Boolean = True
        Dim AllowRoles As Boolean = True

        Dim objattRole As New attRoles
        Dim objBALRole As New BALRoles

        Dim ds As DataTable
        objattRole.PKeyCode = RoleID
        If RoleID <> "1" Then ' if it's admin Role.
            ds = objBALRole.GetAll_Roles(objattRole)
            If ds Is Nothing = False AndAlso ds.Rows.Count > 0 Then
                AllowAdmin = ds.Rows(0)("AstAdmin")
                AllowDetails = ds.Rows(0)("AstDetail")
                AllowRoles = ds.Rows(0)("Roles")
                AllowExport = ds.Rows(0)("CMAExport").ToString
                AllowImport = ds.Rows(0)("CMAImport").ToString
            End If
        End If

        Dim item As ToolStripMenuItem
        If SubMenuItem IsNot Nothing Then
            SubMenuItem.DropDownItems.Clear()
            item = SubMenuItem.DropDownItems.Add("&Import/Export Files", Nothing, New EventHandler(AddressOf Me.btnImport_Click))
            item.Enabled = AllowImport And AllowExport

            ' item = SubMenuItem.DropDownItems.Add("Export Data", Nothing, New EventHandler(AddressOf Me.btnExport_Click))
            ' item.Enabled = AllowExport
            SubMenuItem.DropDownItems.Add("&Exit", Nothing, New EventHandler(AddressOf Me.btnExit_Click))
        End If

        If MainMenu IsNot Nothing Then
            If MainMenu.Items("mnuAssets") IsNot Nothing Then
                'Dim mnuAssets As ToolStripMenuItem = CType(MainMenu.Items("mnuAssets"), ToolStripMenuItem)
                ''mnuAssets.DropDownItems("mnuSearch").Dispose()
                'mnuAssets.DropDownItems("mnuDetailsMainten").Dispose()
                'mnuAssets.DropDownItems("ToolStripSeparator2").Dispose()
                'mnuAssets.DropDownItems("mnuSep7").Dispose()
                ''mnuAssets.DropDownItems("mnuLocCustTrans").Dispose()
                'mnuAssets.DropDownItems("mnuAdmin").Dispose()

                'item = mnuAssets.DropDownItems.Add("Details && Maintenance", Nothing, New EventHandler(AddressOf Me.btnAssetsDetails_Click))
                'item.Enabled = AllowDetails
                'item = mnuAssets.DropDownItems.Add("Administration", Nothing, New EventHandler(AddressOf Me.btnAdministration_Click))
                'item.Enabled = AllowAdmin
            End If
            '    If MainMenu.Items("mnuMasterData") IsNot Nothing Then
            '        Dim mnuMasterData As ToolStripMenuItem = CType(MainMenu.Items("mnuMasterData"), ToolStripMenuItem)
            '        mnuMasterData.DropDownItems("mnuBrands").Dispose()
            '        mnuMasterData.DropDownItems("mnuInsurers").Dispose()
            '        mnuMasterData.DropDownItems("mnuAssetItems").Dispose()
            '        mnuMasterData.DropDownItems("mnuSuppliers").Dispose()
            '        mnuMasterData.DropDownItems("mnuGLCode").Dispose()
            '        mnuMasterData.DropDownItems("mnuDisposalMethods").Dispose()
            '        mnuMasterData.DropDownItems("mnuUnits").Dispose()
            '        mnuMasterData.DropDownItems("mnuSep1").Dispose()
            '        mnuMasterData.DropDownItems("mnuSep2").Dispose()
            '        mnuMasterData.DropDownItems("mnuSep3").Dispose()
            '        mnuMasterData.DropDownItems("mnuAssetCat").Text = "Asset Class"
            '        '
            '    End If
            '    If MainMenu.Items("mnuTools") IsNot Nothing Then
            '        Dim mnuTools As ToolStripMenuItem = CType(MainMenu.Items("mnuTools"), ToolStripMenuItem)
            '        mnuTools.DropDownItems("mnuBarcodeStruct").Dispose()
            '        mnuTools.DropDownItems("mnuOfflineMachines").Dispose()
            '        mnuTools.DropDownItems("ToolStripSeparator10").Dispose()
            '        mnuTools.DropDownItems("ToolStripSeparator5").Dispose()
            '        mnuTools.DropDownItems("mnuBackendInventory").Dispose()
            '    End If

            '    If MainMenu.Items("mnuCompanyProfile") IsNot Nothing Then
            '        Dim mnuCompanyProfile As ToolStripMenuItem = CType(MainMenu.Items("mnuCompanyProfile"), ToolStripMenuItem)
            '        mnuCompanyProfile.DropDownItems("mnuBarcodePolicy").Dispose()
            '        mnuCompanyProfile.DropDownItems("mnuAssetCoding").Dispose()
            '    End If
            '    If MainMenu.Items("mnuSecurity") IsNot Nothing Then
            '        Dim mnuSecurity As ToolStripMenuItem = CType(MainMenu.Items("mnuSecurity"), ToolStripMenuItem)
            '        mnuSecurity.DropDownItems("mnuUserRoles").Dispose()
            '        item = mnuSecurity.DropDownItems.Add("User Roles", Nothing, New EventHandler(AddressOf Me.btnUserRoles_Click))
            '        item.Enabled = AllowRoles
            '    End If
        End If
    End Sub

    Public Function GetConnectionString() As String
        GenericDAL.ConnectionString.ServerName = DBServerName
        GenericDAL.ConnectionString.DbName = DbName
        GenericDAL.ConnectionString.UserName = DbUserName
        GenericDAL.ConnectionString.UserPass = DbUserPass
        GenericDAL.ConnectionString.SQLPort = SQLPort

        GenericDAL.ConnectionString.ConnectToDb()
        Return GenericDAL.ConnectionString.GetConStr()
    End Function

    Public Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Application.Exit()
    End Sub

End Class
