Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports GenericDAL

Public Class ctlDatabaseLogin
    Public Canceled As Boolean = True

    Public Shared Function GetConStr(ByVal server As String, ByVal port As String, ByVal username As String, ByVal password As String) As String
        If port <> "0" Then

            Return "DATA SOURCE=" & server & "," & port & ";UID=" & username & ";PWD=" & password & ";Initial Catalog=Master;Connect Timeout=3"
        Else
            Return "DATA SOURCE=" & server & ";UID=" & username & ";PWD=" & password & ";Initial Catalog=Master;Connect Timeout=3"
        End If
    End Function


    Public Function GetDatabasesList(ByVal server As String, ByVal port As String, ByVal username As String, ByVal password As String) As DataTable
        Dim dt As New DataTable
        Dim cmd As New SqlCommand
        Dim Conn As New SqlConnection()

        Try
            Conn.ConnectionString = GetConStr(server, port, username, password)
            Conn.Open()
            cmd.Connection = Conn
            cmd.CommandText = "select name from sysdatabases "
            Using da As New SqlDataAdapter(cmd)
                da.Fill(dt)
            End Using
        Catch ex As SqlException
            SetErrorMessage(ex.Message)
        Finally
            cmd.Dispose()
            Conn.Dispose()
        End Try
        Return dt
    End Function

    Private Sub btnGetDatabases_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDatabases.Click
        Dim txt As String = cmbDatabase.Text
        cmbDatabase.Items.Clear()
        cmbDatabase.Text = String.Empty
        Dim dt As DataTable = GetDatabasesList(cmbDBServer.Text, Val(txtSqlPort.Text), txtUserName.Text, txtDBPass.Text)
        For Each dr As DataRow In dt.Rows
            cmbDatabase.Items.Add(dr(0))
        Next
        If cmbDatabase.Items.Contains(txt) Then
            cmbDatabase.Text = txt
        End If
    End Sub

    Private Sub FillServerList(ByVal cmb As ComboBox)
        If cmb.Items.Count = 0 Then
            Dim oTable As New Data.DataTable
            Try
                If cmb.Items.Count = 0 Then
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                    oTable = System.Data.Sql.SqlDataSourceEnumerator.Instance.GetDataSources
                    cmb.BeginUpdate()
                    For Each oRow As DataRow In oTable.Rows
                        If oRow("InstanceName").ToString = "" Then
                            cmb.Items.Add(oRow("ServerName"))
                        Else
                            cmb.Items.Add(String.Format("{0}\{1}", oRow("ServerName"), oRow("InstanceName")))
                        End If
                    Next oRow
                    cmb.EndUpdate()
                End If
            Catch ex As Exception
                SetErrorMessage(ex.Message)
            Finally
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                oTable.Dispose()
            End Try
        End If
    End Sub

    Private Sub btnGetServers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetservers.Click
        FillServerList(cmbDBServer)
    End Sub

    Private Sub btnDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefault.Click
        DxErrorProvider1.ClearErrors()
        txtUserName.Text = "sa"
        cmbDBServer.Text = Environment.MachineName
        cmbDatabase.Text = "ZulRoute"
        txtSqlPort.Text = "1433"
    End Sub

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        If String.IsNullOrEmpty(txtCompanyName.Text.Trim) Then
            DxErrorProvider1.SetError(txtCompanyName, My.Resources.Strings.valRulenotEmpty)
            Return
        End If

        If String.IsNullOrEmpty(txtUserName.Text.Trim) Then
            DxErrorProvider1.SetError(txtUserName, My.Resources.Strings.valRulenotEmpty)
            Return
        End If


        ConnectionString.DbName = cmbDatabase.Text
        ConnectionString.ServerName = cmbDBServer.Text
        ConnectionString.UserName = txtUserName.Text
        ConnectionString.UserPass = txtDBPass.Text
        ConnectionString.SQLPort = txtSqlPort.Text

        If ConnectionString.ConnectToDb Then
            Canceled = False
            Me.ParentForm.Close()
        Else
            'show error message.....
            SetErrorMessage(ConnectionString.ErrorMessage)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Canceled = True
        Me.ParentForm.Close()
    End Sub

    Private Sub txtUserName_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUserName.EditValueChanged, txtSqlPort.EditValueChanged, txtDBPass.EditValueChanged
        lblErrorMessage.Visible = False
    End Sub

    Private Sub cmbDatabase_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDatabase.SelectedValueChanged, cmbDBServer.SelectedValueChanged
        lblErrorMessage.Visible = False
    End Sub

    Public Sub SetErrorMessage(ByVal msg As String)
        lblErrorMessage.Visible = True
        lblErrorMessage.Text = msg
    End Sub

    Private Sub DatabaseLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbDatabase.Text = ConnectionString.DbName
        cmbDBServer.Text = ConnectionString.ServerName
        txtUserName.Text = ConnectionString.UserName
        txtDBPass.Text = ConnectionString.UserPass
        txtSqlPort.Text = ConnectionString.SQLPort
        lblErrorMessage.Visible = True
        lblErrorMessage.Text = ConnectionString.ErrorMessage
    End Sub
End Class