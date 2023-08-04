
Imports System.IO
Imports ZulAssetsDAL.ZulAssetsDAL

Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.


    Partial Friend Class MyApplication

        'Private Sub Check_File_Exist()
        '    If Not File.Exists(System.Windows.Forms.Application.StartupPath & "\ZulLics.dll") Then
        '        System.Windows.Forms.Application.Exit()
        '    End If
        'End Sub
        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            AppConfig.ServerCharacter = "G"
            AppConfig.ClientCharacter = "H"
            'AppConfig.SelectedEdition = "E"

            'Create Application data Directory in 	C:\ProgramData\ZulAssets
            AppConfig.AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\ZulAssets"
    
            If Not Directory.Exists(AppConfig.AppDataFolder) Then
                Directory.CreateDirectory(AppConfig.AppDataFolder)
            End If
            AppConfig.DatabaseConfigFilePath = AppConfig.AppDataFolder & "\Data.xml"
            ZulLib.Messages.AppDataFolder = AppConfig.AppDataFolder

            Try
                Dim filePath As String = System.Windows.Forms.Application.StartupPath
                If Not File.Exists(System.Windows.Forms.Application.StartupPath & "\ZulLib.dll") Then
                    e.Cancel = True
                    Exit Sub
                End If

                'Kabeer Registration
                'If CheckRegistration() Then
                '    If Not AppConfig.ISRegistered Or AppConfig.AppEdition = AppConfig.ApplicationRegistration.NotRegistered Then
                '        ZulMessageBox.ShowMe("InvalidRegisfound")
                '        'ZulLib.Messages.ErrorMessage(My.Resources.Strings.InvalidLicenseKey, AppConfig.LoginName)
                '        e.Cancel = True
                '    End If
                'Else
                '    'Check if the current loged in user has full permission on the installation folder.
                '    If Not ZulLib.AppFolderPermissions.CheckAppFolderPermission(System.Windows.Forms.Application.StartupPath) Then
                '        e.Cancel = True
                '        Exit Sub
                '    End If

                '    If ShowRegistration() Then
                '        If Not CheckRegistration() Then
                '            e.Cancel = True
                '        End If
                '    Else
                '        e.Cancel = True
                '    End If
                'End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                e.Cancel = True
            End Try

        End Sub

        'Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
        '    MessageBox.Show("ZULAssets is already running")
        'End Sub

        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            GenericExceptionHandler(e.Exception, "UnhandledException")
        End Sub
    End Class
End Namespace

