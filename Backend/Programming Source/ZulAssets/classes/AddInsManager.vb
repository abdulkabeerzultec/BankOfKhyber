Imports System.IO
Imports System.Reflection
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class AddInsManager
    Public Shared Function RegisterAvailablePlugins() As String
        Dim appDir As String = My.Application.Info.DirectoryPath
        Dim availablePlugins As String() = Directory.GetFiles(appDir, "*Plugin.DLL")
        Try

            For Each possiblePlugin As String In availablePlugins
                Dim asm As Assembly = Assembly.LoadFrom(possiblePlugin)
                Dim myType As System.Type = asm.GetType(asm.GetName.Name + ".Plugin")
                Dim implementsIPlugin As Boolean = GetType(AddInInterface.IAddIn).IsAssignableFrom(myType)
                If implementsIPlugin Then
                    Dim plugin As AddInInterface.IAddIn = CType(Activator.CreateInstance(myType), AddInInterface.IAddIn)
                    If plugin.ActionName = "AddNavMenuItem" Then

                        plugin.DBServerName = AppConfig.DbServer
                        plugin.DbName = AppConfig.DbName
                        plugin.DbUserName = AppConfig.DbUname
                        plugin.DbUserPass = AppConfig.DbPass
                        plugin.SQLPort = AppConfig.DBSQLPort
                        plugin.CompanyFileName = AppConfig.CompanyName

                        plugin.UserGUID = Guid.NewGuid
                        plugin.UserName = AppConfig.LoginName
                        plugin.MainForm = MainForm
                        plugin.IsDemokey = AppConfig.IsDemoKey
                        plugin.RoleID = MainForm.RoleID
                        plugin.AddMenuItems(MainForm.MenuStrip1, MainForm.mnuFile)
                        MainModule.frmPluginDataProcess = plugin.frmDataProcess
                        Select Case plugin.IntegrationName
                            'we made it like this, because of dotfuscator, the integrationtype is being renamed and we can't get it by Enum name.
                            Case "AbunayyanIntegration"
                                IntegrationName = IntegrationType.AbunayyanPlugin
                            Case "ElafIntegration"
                                IntegrationName = IntegrationType.ElafPlugin
                            Case "GSFMOIntegration"
                                IntegrationName = IntegrationType.GSFMOPlugin
                            Case "SABBIntegration"
                                IntegrationName = IntegrationType.SABBPlugin
                            Case "SanofiPlugin"
                                IntegrationName = IntegrationType.SanofiPlugin
                            Case "CITCPlugin"
                                IntegrationName = IntegrationType.CITCPlugin
                            Case "PGPlugin"
                                IntegrationName = IntegrationType.PGPlugin
                        End Select
                        'IntegrationName = CType([Enum].Parse(GetType(IntegrationType), plugin.IntegrationName), IntegrationType)
                    End If
                End If
            Next
            Return Nothing
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function



End Class
