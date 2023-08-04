Imports GenericDAL
Public Class AppConfigData
    Enum TLanguage
        English
        Arabic
        Urdu
    End Enum

    Public Enum ApplicationEditions
        Enterprise
        NotRegistered
    End Enum

    Public Shared UserGUID As Guid = Guid.Empty
    Public Shared LoginName As String = String.Empty
    Public Shared IsForeignLanguage As Boolean = False
    Public Shared SkinName As String = String.Empty
    Public Shared ReportPrinter As String = String.Empty
    Public Shared LabelPrinter As String = String.Empty
    Public Shared SelectedLanguage As TLanguage
    Public Shared DBServerName As String = String.Empty
    Public Shared DbName As String = String.Empty
    Public Shared DbUserName As String = String.Empty
    Public Shared DbUserPass As String = String.Empty
    Public Shared DbSQLPort As String = String.Empty
    Public Shared CompanyFileName As String = String.Empty
    Public Shared RegistryPath As String = String.Empty
    Public Shared DefaultWarehosueGUID As Guid = Guid.Empty
    Public Shared ISRegistered As Boolean = False
    Public Shared IsDemokey As Boolean = False
    Public Shared AppEdition As ApplicationEditions = ApplicationEditions.NotRegistered

    Public Shared Function GetConnectionString() As String
        GenericDAL.ConnectionString.ServerName = DBServerName
        GenericDAL.ConnectionString.DbName = DbName
        GenericDAL.ConnectionString.UserName = DbUserName
        GenericDAL.ConnectionString.UserPass = DbUserPass
        GenericDAL.ConnectionString.SQLPort = DBSQLPort
        Return GenericDAL.ConnectionString.GetConStr()
    End Function

    Public Shared Sub GetSettings()
        'LabelPrinter = My.Settings.LabelPrinter
        'ReportPrinter = My.Settings.ReportPrinter
        'SkinName = My.Settings.SkinName

        'If Not String.IsNullOrEmpty(My.Settings.DefaultWarehouseGUID) Then
        '    DefaultWarehosueGUID = New Guid(My.Settings.DefaultWarehouseGUID)
        'Else
        '    DefaultWarehosueGUID = Guid.Empty
        'End If

        'If My.Settings.DefaultLanguage = "Arabic" Then
        '    SelectedLanguage = TLanguage.Arabic
        'ElseIf My.Settings.DefaultLanguage = "English" Then
        '    SelectedLanguage = TLanguage.English
        'Else
        '    SelectedLanguage = TLanguage.Urdu
        'End If
    End Sub
    Public Shared Sub SaveSettings()
        'My.Settings.LabelPrinter = LabelPrinter
        'My.Settings.ReportPrinter = ReportPrinter
        'My.Settings.SkinName = SkinName
        'My.Settings.DefaultWarehouseGUID = DefaultWarehosueGUID.ToString
        My.Settings.Save()
    End Sub


End Class
