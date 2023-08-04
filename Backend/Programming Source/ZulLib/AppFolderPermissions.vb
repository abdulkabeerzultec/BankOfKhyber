Imports System.Security.AccessControl
Imports System.IO

Public Class AppFolderPermissions

    Public Shared Function CheckAppFolderPermission(ByVal AppFolderPath As String) As Boolean
        Dim APPDir As String = AppFolderPath
        Dim TestDir As String = APPDir + "\ErrorsReports"
        Try
            If Not Directory.Exists(TestDir) Then
                Directory.CreateDirectory(TestDir)
            End If
            Dim FileName As String = String.Format("{0}\ErrorsReports\{1}Err.Log", AppFolderPath, My.Application.Info.ProductName)
            Dim sr As StreamWriter = File.AppendText(FileName)
            sr.Write(" ")
            sr.Close()
            Return True
        Catch ex As Exception
            Dim HavePermission As Boolean = GrantPermission(AppFolderPath)
            If Not HavePermission Then
                System.Windows.Forms.MessageBox.Show(String.Format("You don't have permission on the installation folder({0}), Run application as administrator for the first time only to enter the license key.", AppFolderPath), My.Application.Info.ProductName, Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            End If
            Return HavePermission
        End Try
    End Function

    Private Shared Function GrantPermission(ByVal AppFolderPath As String) As Boolean
        Try
            Dim AccountName As String = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString()
            ' Add the access control entry to the directory.
            AddDirectorySecurity(AppFolderPath, AccountName, FileSystemRights.FullControl, AccessControlType.Allow)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' Adds an ACL entry on the specified directory for the specified account.
    Private Shared Sub AddDirectorySecurity(ByVal FileName As String, ByVal Account As String, ByVal Rights As FileSystemRights, ByVal ControlType As AccessControlType)
        ' Create a new DirectoryInfoobject.
        Dim dInfo As New DirectoryInfo(FileName)

        ' Get a DirectorySecurity object that represents the 
        ' current security settings.
        Dim dSecurity As DirectorySecurity = dInfo.GetAccessControl()

        ' Add the FileSystemAccessRule to the security settings. 
        'dSecurity.AddAccessRule(New FileSystemAccessRule(Account, Rights, ControlType, PropagationFlags.InheritOnly, AccessControlType.Allow))
        dSecurity.AddAccessRule(New FileSystemAccessRule(Account, Rights, InheritanceFlags.ObjectInherit Or InheritanceFlags.ContainerInherit, PropagationFlags.None, ControlType))

        ' Set the new access settings.
        dInfo.SetAccessControl(dSecurity)

    End Sub

    '' Removes an ACL entry on the specified directory for the specified account.
    'Private Shared Sub RemoveDirectorySecurity(ByVal FileName As String, ByVal Account As String, ByVal Rights As FileSystemRights, ByVal ControlType As AccessControlType)
    '    ' Create a new DirectoryInfo object.
    '    Dim dInfo As New DirectoryInfo(FileName)

    '    ' Get a DirectorySecurity object that represents the 
    '    ' current security settings.
    '    Dim dSecurity As DirectorySecurity = dInfo.GetAccessControl()

    '    ' Add the FileSystemAccessRule to the security settings. 
    '    dSecurity.RemoveAccessRule(New FileSystemAccessRule(Account, Rights, ControlType))

    '    ' Set the new access settings.
    '    dInfo.SetAccessControl(dSecurity)

    'End Sub

End Class
