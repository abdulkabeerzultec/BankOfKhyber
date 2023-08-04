Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.IO


Public Class frmCompany
    Dim objattCompanyInfo As attCompanyInfo
    Dim objBALCompanyInfo As New BALCompanyInfo
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Dim valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Dim strLogofileName As String = ""
    Friend WithEvents btnDelImg As System.Windows.Forms.Button
    Public Mode As String = 1


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If valProvMain.Validate Then
                Dim ds As DataTable
                Dim dt As DataTable
                ds = objBALCompanyInfo.GetAll_CompanyInfo(New attCompanyInfo)
                If ds.Rows.Count > 0 Then
                    dt = ds
                    update_CompanyInfo()
                Else
                    add_CompanyInfo()
                End If
            End If
            If Mode = "2" Then Me.Dispose()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub
    Private Sub update_CompanyInfo()
        Try
            objattCompanyInfo = New attCompanyInfo

            'Dim fname1() As String
            'Dim fname As String = ""
            'Dim cnt As Integer

            'If PBLogo.ImageLocation <> "" Then
            '    fname1 = PBLogo.ImageLocation.Split("\")
            '    cnt = fname1.Length
            '    fname = fname1(cnt - 1)
            'End If


            objattCompanyInfo.Address = txtAddress.Text
            objattCompanyInfo.City = txtCity.Text
            objattCompanyInfo.CounTry = txtCountry.Text
            objattCompanyInfo.Email = txtEmail.Text
            objattCompanyInfo.Fax = txtFax.Text
            objattCompanyInfo.Name = txtName.Text
            objattCompanyInfo.PCode = txtPCode.Text
            objattCompanyInfo.Phone = txtPhone.Text
            objattCompanyInfo.State = txtState.Text

            If strLogofileName <> "" Then
                Dim fs As New FileStream(strLogofileName, FileMode.Open)
                Dim Data() As Byte = New [Byte](fs.Length) {}
                fs.Read(Data, 0, fs.Length)
                objattCompanyInfo.Image = Data
                objattCompanyInfo.IsNewImage = True
            Else
                If PBLogo.Image IsNot Nothing Then
                    objattCompanyInfo.IsNewImage = False
                Else
                    objattCompanyInfo.Image = Nothing
                    objattCompanyInfo.IsNewImage = True
                End If
            End If
            objBALCompanyInfo.Update_CompanyInfo(objattCompanyInfo)

            CompanyLogoImage = PBLogo.Image
            AppConfig.CompanyName = txtName.Text
            ZulMessageBox.ShowMe("Saved")
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub
    Private Sub add_CompanyInfo()
        Try
            objattCompanyInfo = New attCompanyInfo
            objattCompanyInfo.Address = txtAddress.Text
            objattCompanyInfo.City = txtCity.Text
            objattCompanyInfo.CounTry = txtCountry.Text
            objattCompanyInfo.Email = txtEmail.Text
            objattCompanyInfo.Fax = txtFax.Text
            objattCompanyInfo.Name = txtName.Text
            objattCompanyInfo.PCode = txtPCode.Text
            objattCompanyInfo.Phone = txtPhone.Text
            objattCompanyInfo.State = txtState.Text

            If strLogofileName <> "" Then
                Dim fs As New FileStream(strLogofileName, FileMode.Open)
                Dim Data() As Byte = New [Byte](fs.Length) {}
                fs.Read(Data, 0, fs.Length)
                objattCompanyInfo.Image = Data
                objattCompanyInfo.IsNewImage = True
            Else
                If PBLogo.Image IsNot Nothing Then
                    objattCompanyInfo.IsNewImage = False
                Else
                    objattCompanyInfo.Image = Nothing
                    objattCompanyInfo.IsNewImage = True
                End If
            End If

            objBALCompanyInfo.Insert_CompanyInfo(objattCompanyInfo)
            CompanyLogoImage = PBLogo.Image

            AppConfig.CompanyName = txtName.Text
            ZulMessageBox.ShowMe("Saved")
        Catch ex As Exception

            GenericExceptionHandler(ex, WhoCalledMe)
            AppConfig.ISRegistered = False
            Application.Exit()

        End Try

    End Sub

    Private Sub frmCompany_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        FormController.objfrmCompany = Nothing
    End Sub



    Private Sub frmCompany_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.MainIcon
        Me.BackgroundImage = My.Resources.Background
        Me.BackgroundImageLayout = ImageLayout.Stretch


        Try
            If Mode = "2" Then
                btnExit.Text = "Exit Application"
                Me.ControlBox = False
            End If
            valProvMain.SetValidationRule(txtName, valRulenotEmpty)
            valProvMain.SetValidationRule(txtEmail, valRulenotEmpty)

            txtEmail.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
            txtEmail.Properties.Mask.EditMask = "([a-zA-Z0-9_\-\.]+)@((\[[0-9{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)"
            txtEmail.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Optimistic
            txtEmail.Properties.Mask.IgnoreMaskBlank = False

            Dim ds As DataTable
            Dim dt As DataTable
            Dim strFname As String = ""
            ds = objBALCompanyInfo.GetAll_CompanyInfo(New attCompanyInfo)
            If ds.Rows.Count > 0 Then
                dt = ds
                If dt.Rows(0)("Name").ToString() <> "" Then txtName.Text = dt.Rows(0)("Name").ToString()
                If dt.Rows(0)("Address").ToString() <> "" Then txtAddress.Text = dt.Rows(0)("Address").ToString()
                If dt.Rows(0)("State").ToString() <> "" Then txtState.Text = dt.Rows(0)("State").ToString()
                If dt.Rows(0)("PCode").ToString() <> "" Then txtPCode.Text = dt.Rows(0)("PCode").ToString()
                If dt.Rows(0)("City").ToString() <> "" Then txtCity.Text = dt.Rows(0)("City").ToString()
                If dt.Rows(0)("Country").ToString() <> "" Then txtCountry.Text = dt.Rows(0)("Country").ToString()
                If dt.Rows(0)("Phone").ToString() <> "" Then txtPhone.Text = dt.Rows(0)("Phone").ToString()
                If dt.Rows(0)("Fax").ToString() <> "" Then txtFax.Text = dt.Rows(0)("Fax").ToString()
                If dt.Rows(0)("Email").ToString() <> "" Then txtEmail.Text = dt.Rows(0)("Email").ToString()
                If ds.Rows(0)("Image").ToString <> "" Then
                    Try
                        Dim bits As Byte() = CType(dt.Rows(0)("Image"), Byte())
                        Dim ms As New MemoryStream(bits, 0, bits.Length)
                        PBLogo.Image = Image.FromStream(ms, True)
                    Catch ex As Exception

                    End Try
                    btnDelImg.Visible = True
                End If
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            FormController.objfrmCompany = Nothing
            If Mode = "2" Then
                Application.Exit()
            Else
                Me.Dispose()
            End If

        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub


    Private Sub PBLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PBLogo.Click
        Try
            dlgFile.ShowDialog()
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub
    Private Sub PBLogo_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PBLogo.MouseEnter
        PBLogo.BorderStyle = BorderStyle.FixedSingle
    End Sub

    Private Sub PBLogo_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PBLogo.MouseLeave
        PBLogo.BorderStyle = BorderStyle.None
    End Sub

    Private Sub FileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dlgFile.FileOk
        Try
            strLogofileName = dlgFile.FileName '.Split("\")
            PBLogo.ImageLocation = strLogofileName.Trim
            btnDelImg.Visible = True
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try

    End Sub


    Private Sub btnDelImg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelImg.Click
        Try
            'If PBLogo.Image IsNot Nothing Then
            If ZulMessageBox.ShowMe("BeforeDeletedImg", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                'Try
                '    If File.Exists(PBLogo.ImageLocation) Then
                '        File.Delete(PBLogo.ImageLocation)
                '    End If
                'Catch ex As Exception

                'End Try
                PBLogo.Image = Nothing
                PBLogo.ImageLocation = Nothing
                strLogofileName = ""
                'ZulMessageBox.ShowMe("AfterDeleteImg")
                btnDelImg.Visible = False
            End If
            'End If
        Catch ex As Exception
            GenericExceptionHandler(ex, WhoCalledMe)
        End Try
    End Sub
End Class
