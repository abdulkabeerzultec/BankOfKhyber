Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL

Public Class frmDep
    Inherits System.Windows.Forms.Form
    Dim objattDepMeth As attDepreciationMethod
    Dim objBALDepMeth As New BALDepreciationMethod

    Dim isEdit As Boolean = False
    


#Region "Method"

    Private Sub AddNew_Dep()
        Try

            objattDepMeth = New attDepreciationMethod
            objattDepMeth.DepDesc = RemoveUnnecessaryChars(txtDepDesc.Text)
            objattDepMeth.PKeyCode = RemoveUnnecessaryChars(txtDepCode.Text)
            txtDepCode.Text = objBALDepMeth.Insert_DepreciationMethod(objattDepMeth)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub Update_Dep()
        Try
            objattDepMeth = New attDepreciationMethod
            objattDepMeth.DepDesc = RemoveUnnecessaryChars(txtDepDesc.Text)
            objattDepMeth.PKeyCode = RemoveUnnecessaryChars(txtDepCode.Text)
            txtDepCode.Text = objBALDepMeth.Update_DepreciationMethod(objattDepMeth)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub format_Grid()
        Try
            grdView.Columns(0).Caption = "Depreciation Code"
            grdView.Columns(0).Width = 115
            grdView.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

            grdView.Columns(1).Caption = "Depreciation Name"
            grdView.Columns(1).Width = 255
            addGridMenu(grd)
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

#End Region


    Private Sub frmDep_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormController.objfrmDep = Nothing
    End Sub

    Private Sub frmDep_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch

        Try

            grd.DataSource = objBALDepMeth.GetAll_DepreciationMethod(New attDepreciationMethod)
            format_Grid()

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    'Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
    '    Try
    '        If valprovmain.validate Then
    '            If isEdit Then
    '                Me.Update_Dep()
    '                isEdit = False
    '                txtDepCode.Text = objBALDepMeth.GetNextPKey_DepreciationMethod()
    '                Get_Dep()
    '            Else

    '                AddNew_Dep()
    '                Get_Dep()
    '            End If
    '            txtDepDesc.Text = ""

    '        End If
    '    Catch ex As Exception
    '        GenericExceptionHandler(ex, WhoCalledMe)
    '    End Try

    'End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmDep = Nothing
            Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    'Private Sub dgDep_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    Try
    '        If e.KeyValue = Keys.Delete Then 'Press Delete
    '            'btnDelete_Click(sender,e)
    '        End If
    '    Catch ex As Exception
    '        GenericExceptionHandler(ex, WhoCalledMe)
    '    End Try

    'End Sub

    'Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    '    Try
    '        If dgDep.Rows.Count > 0 Then
    '            Dim intRow As Integer = dgDep.Row.ToString()
    '            If intRow > 0 Then
    '                If ZulMessageBox.ShowMe("BeforeDeleted", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
    '                    If Not (dgDep.Rows(intRow)(0) Is Nothing And dgDep.Rows(intRow)(1) Is Nothing) Then
    '                        txtDepCode.Text = dgDep.Rows(intRow)(0).ToString()
    '                        objattDepMeth.PKeyCode = dgDep.Rows(intRow)(0).ToString()
    '                        objBALDepMeth.Delete_DepreciationMethod(objattDepMeth)
    '                        dgDep.Rows.Remove(intRow)
    '                        '        btnDelete.Visible = False
    '                        isEdit = False
    '                        txtDepDesc.Text = ""
    '                        txtDepCode.Text = objBALDepMeth.GetNextPKey_DepreciationMethod()
    '                    End If
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        GenericExceptionHandler(ex, WhoCalledMe)
    '    End Try

    'End Sub

    Private Sub grdView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdView.FocusedRowChanged
        Dim FocRow As Integer = grdView.FocusedRowHandle
        If FocRow >= 0 Then 'DepCode,DepDesc
            txtDepCode.Text = GetGridRowCellValue(grdView, FocRow, "DepCode").ToString
            txtDepDesc.Text = GetGridRowCellValue(grdView, FocRow, "DepDesc").ToString
        End If
    End Sub

  
End Class
