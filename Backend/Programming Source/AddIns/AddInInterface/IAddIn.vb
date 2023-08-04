Imports System.Windows.Forms


Public Interface IAddIn
    ReadOnly Property ActionName() As String
    Sub AddMenuItems(ByRef MainMenu As MenuStrip, ByRef SubMenuItem As ToolStripMenuItem)
    Property UserName() As String
    Property UserGUID() As Guid
    Property MainForm() As Form
    Property RoleID() As Integer
    Property IsDemokey() As Boolean

    Property DBServerName() As String
    Property DbName() As String
    Property DbUserName() As String
    Property DbUserPass() As String
    Property SQLPort() As String
    Property CompanyFileName() As String

    Property frmDataProcess() As Form
    ReadOnly Property IntegrationName() As String
End Interface
