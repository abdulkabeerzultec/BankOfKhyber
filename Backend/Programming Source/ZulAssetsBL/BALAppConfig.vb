Imports System
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Globalization
Imports System.Threading.Thread


Namespace ZulAssetBAL
    Public Class BALAppConfig
        Inherits BLBase
        Public Sub LoadSettings()

            AppConfig.Get_DbConfig_Login()
        End Sub

        Public Sub SetDateFormat(ByVal dtformat As String)
            Dim newCulture As CultureInfo = New CultureInfo(CultureInfo.CurrentCulture.Name)
            newCulture.DateTimeFormat.ShortDatePattern = dtformat '"dd/MM/yyyy"
            CurrentThread.CurrentCulture = newCulture
        End Sub

        Public Sub Set_SysSettings(ByVal strdt As String, ByVal DepRunType As Int16, ByVal DeletePermt1 As Int16, ByVal strReportPrinter As String, ByVal strLabelReport As String, ByVal ExportToServ As Boolean, ByVal codemod As Boolean, ByVal dtformat As String, ByVal DescForRpt As String, ByVal ImgStorgeLoc As String, ByVal ImgType As String, ByVal ImgPath As String, ByVal ShowAlarmOnStartup As Boolean, ByVal AlarmBeforeDays As Integer)
            Dim dt As DataTable = Me.GeneralExecuter(AppConfig.Get_Settings)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Update_SysSettings(strdt, DepRunType, DeletePermt1, strReportPrinter, strLabelReport, ExportToServ, codemod, dtformat, DescForRpt, ImgStorgeLoc, ImgType, ImgPath, ShowAlarmOnStartup, AlarmBeforeDays)
                Else
                    Insert_SysSettings(strdt, DepRunType, DeletePermt1, strReportPrinter, strLabelReport, ExportToServ, codemod, dtformat, DescForRpt, ImgStorgeLoc, ImgType, ImgPath, ShowAlarmOnStartup, AlarmBeforeDays)
                End If
            End If
        End Sub

        Private Sub Update_SysSettings(ByVal strdt As String, ByVal DepRunType As Int16, ByVal DeletePermt1 As Int16, ByVal strReportPrinter As String, ByVal strLabelReport As String, ByVal ExportToServ As Boolean, ByVal codemod As Boolean, ByVal dtformat As String, ByVal DescForRpt As String, ByVal ImgStorgeLoc As String, ByVal ImgType As String, ByVal ImgPath As String, ByVal ShowAlarmOnStartup As Boolean, ByVal AlarmBeforeDays As Integer)
            Try
                Me.GeneralExecuter(AppConfig.Update_Settings(strdt, DepRunType, DeletePermt1, ExportToServ, codemod, dtformat, DescForRpt, ImgStorgeLoc, ImgType, ImgPath, ShowAlarmOnStartup, AlarmBeforeDays))
                AppConfig.Update_PrinterSettings(strReportPrinter, strLabelReport)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub Insert_SysSettings(ByVal strdt As String, ByVal DepRunType As Int16, ByVal DeletePermt1 As Int16, ByVal strReportPrinter As String, ByVal strLabelReport As String, ByVal ExportToServ As Boolean, ByVal codemod As Boolean, ByVal dtformat As String, ByVal DescForRpt As String, ByVal ImgStorgeLoc As String, ByVal ImgType As String, ByVal ImgPath As String, ByVal ShowAlarmOnStartup As Boolean, ByVal AlarmBeforeDays As Integer)
            Try
                Me.GeneralExecuter(AppConfig.Insert_Settings(strdt, DepRunType, DeletePermt1, ExportToServ, codemod, dtformat, DescForRpt, ImgStorgeLoc, ImgType, ImgPath, ShowAlarmOnStartup, AlarmBeforeDays))
                AppConfig.Update_PrinterSettings(strReportPrinter, strLabelReport)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Get_SysSettings()
            Try
                Dim dt As DataTable = Me.GeneralExecuter(AppConfig.Get_Settings)
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        AppConfig.DepDate = dt.Rows(0)("FYDate")
                        AppConfig.DepRunType = dt.Rows(0)("DepRunType")
                        AppConfig.DeletePermt = dt.Rows(0)("DeletePermt")
                        AppConfig.ExportToServer = dt.Rows(0)("ExportToServ")
                        AppConfig.CodingMode = dt.Rows(0)("CodingMode")
                        AppConfig.MaindateFormat = dt.Rows(0)("dtformat")
                        AppConfig.DescForRpt = dt.Rows(0)("DescForRpt")
                        AppConfig.ImgStorgeLoc = dt.Rows(0)("ImgStorgeLoc")
                        AppConfig.ImgType = dt.Rows(0)("ImgType")
                        AppConfig.ImgPath = dt.Rows(0)("ImgPath").ToString
                        AppConfig.IsOfflineMachine = dt.Rows(0)("IsOfflineMachine")
                        AppConfig.ShowAlarmOnStartup = dt.Rows(0)("ShowAlarmOnStartup")
                        AppConfig.AlarmBeforeDays = dt.Rows(0)("AlarmBeforeDays")
                    End If
                End If

                If String.IsNullOrEmpty(AppConfig.MaindateFormat) Then
                    AppConfig.MaindateFormat = "dd/MM/yyyy"
                End If
                SetDateFormat(AppConfig.MaindateFormat)
                ' if the user didn't config the settings the AppConfig.DeletePermt will be 0.

                If AppConfig.DeletePermt = 0 Then
                    AppConfig.DeletePermt = 1
                End If

                AppConfig.Get_PrinterSettings()

                Dim dtDBVersion As DataTable = Me.GeneralExecuter(AppConfig.Get_DatabaseVersion)
                If dtDBVersion IsNot Nothing AndAlso dtDBVersion.Rows.Count > 0 Then
                    AppConfig.DBVersion = dtDBVersion.Rows(0)("Version")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub New()
        End Sub

        Public Function GetConnected(ByVal DbServer As String, ByVal Dbname As String, ByVal DbUname As String, ByVal DbPass As String, ByVal SQLPort As String, ByVal DbType As String, ByVal txtComUname As String, ByVal txtComPass As String, ByVal txtComSerPort As String, ByVal txtCommServ As String, ByVal DbComname As String) As Boolean
            Return AppConfig.testConnection(DbServer, Dbname, DbUname, DbPass, SQLPort, DbType, txtComUname, txtComPass, txtComSerPort, txtCommServ, DbComname)
        End Function

        Public Sub Update_dbConfig(ByVal DbServer1 As String, ByVal Dbname1 As String, ByVal DbUname1 As String, ByVal DbPass1 As String, ByVal SysPass1 As String, ByVal SQLPort1 As String, ByVal DbType1 As String, ByVal txtComUname1 As String, ByVal txtComPass1 As String, ByVal txtComSerPort1 As String, ByVal txtCommServ1 As String, ByVal DbComname1 As String, ByVal txtExpUname1 As String, ByVal txtExpPass1 As String, ByVal txtExpSerPort1 As String, ByVal txtExpServ1 As String, ByVal DbExpName1 As String)
            AppConfig.Set_dbConfig(DbServer1, Dbname1, DbUname1, DbPass1, SysPass1, SQLPort1, DbType1, txtComUname1, txtComPass1, txtComSerPort1, txtCommServ1, DbComname1, txtExpUname1, txtExpPass1, txtExpSerPort1, txtExpServ1, DbExpName1)
            'LoadSettings()
        End Sub

        'Public Shared Sub Upd_TransMethod(ByVal TransMethod As String)
        '    AppConfig.Update_TransMethod(TransMethod)
        'End Sub
    End Class
End Namespace

