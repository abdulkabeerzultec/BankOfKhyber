Public Class ctlSAPItems
    Inherits ZulLib.ctlDataEditing
    Dim objSAPItem As New ZulAssetsBL.SAPItemsBLL

    Private WithEvents btnApprove As New DevExpress.XtraBars.BarButtonItem

    Private Sub ListDataLoad()
        Me.ListDataSource = objSAPItem.GetListData
    End Sub

    Private Sub ctlSAPItems_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BusnissLayerObject = objSAPItem
        LoadContolSettings(Me)
        NewData()
        ListDataLoad()

        Me.btnApprove.Caption = "Approve"
        Me.btnApprove.Glyph = My.Resources.Resources.check
        Me.btnApprove.Hint = "Approve the changes"
        Me.btnApprove.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.F2))
        btnApprove.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        barPrint.AddItem(btnApprove)
    End Sub

    Private Sub ctlSAPItems_OnDeleteData() Handles Me.OnDeleteData
        Try
            Dim Msg As String = objSAPItem.DeleteByRowGUID(RecordGUID)
            If String.IsNullOrEmpty(Msg) Then
                ListDataLoad()
                ActionResult = 0
            Else
                Messages.ErrorMessage(Msg, WhoCalledMe)
                ActionResult = -1
            End If
        Catch ex As Exception
            Messages.ErrorMessage(ex.Message, WhoCalledMe)
            ActionResult = -1
        End Try
    End Sub

    Private Sub ctlSAPItems_OnLoadData() Handles Me.OnLoadData
        Try
            Dim Msg As String = objSAPItem.Edit(RecordGUID)
            If String.IsNullOrEmpty(Msg) Then
                txtSAPPartNo.Text = objSAPItem.Attributes.SAPPartNo
                txtDescription.Text = objSAPItem.Attributes.Description
                txtWarranty.Text = objSAPItem.Attributes.NewWarranty
                chkUniqueSN.Checked = objSAPItem.Attributes.NewCheckUniqueSNOnReceiving
                chkUpdateAssetSN.Checked = objSAPItem.Attributes.NewUpdateAssetSN
                UpdateStatus(objSAPItem.Attributes.Status)
                txtSAPPartNo.Properties.ReadOnly = True
                ActionResult = 0
            Else
                Messages.ErrorMessage(Msg, WhoCalledMe)
                ActionResult = -1
            End If
        Catch ex As Exception
            Messages.ErrorMessage(ex.Message, WhoCalledMe)
            ActionResult = -1
        End Try
    End Sub

    Private Sub UpdateStatus(ByVal Status As String)
        lblStatus.Text = Status
        If Status = "NotApproved" Then

            ShowBarPrint = True
            btnPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btnPreview.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btnApprove.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            imgStatus.Image = My.Resources.Invalid
            lblStatus.ForeColor = Drawing.Color.Red
        Else
            ShowBarPrint = False
            btnApprove.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btnApprove.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            imgStatus.Image = My.Resources.check
            lblStatus.ForeColor = Drawing.Color.Blue
        End If
    End Sub

    Private Sub ctlSAPItems_OnNewData() Handles Me.OnNewData
        chkUniqueSN.Checked = False
        chkUpdateAssetSN.Checked = False
        txtWarranty.Value = 1
        UpdateStatus("New")
        txtSAPPartNo.Focus()
        txtSAPPartNo.Properties.ReadOnly = False
        ActionResult = 0
    End Sub

    Private Sub ctlSAPItems_OnSaveData() Handles Me.OnSaveData
        If RecordStatus = TRecordStates.NewRecord Then
            objSAPItem.NewRecord()
            RecordGUID = objSAPItem.Attributes.GUID

            objSAPItem.Attributes.CreationDate = Now.Date
            objSAPItem.Attributes.CreatedBy = AppConfigData.UserGUID
            objSAPItem.Attributes.Warranty = 0
            objSAPItem.Attributes.CheckUniqueSNOnReceiving = False
            objSAPItem.Attributes.UpdateAssetSN = False
        End If

        With objSAPItem.Attributes
            .SAPPartNo = txtSAPPartNo.Text
            .Description = txtDescription.Text

            .NewWarranty = txtWarranty.Text
            .NewCheckUniqueSNOnReceiving = chkUniqueSN.Checked
            .NewUpdateAssetSN = chkUpdateAssetSN.Checked
            If .Warranty <> .NewWarranty Or .CheckUniqueSNOnReceiving <> .NewCheckUniqueSNOnReceiving Or .UpdateAssetSN <> .NewUpdateAssetSN Then
                .Status = "NotApproved"
            Else
                .Status = "Approved"
            End If

            UpdateStatus(.Status)

            .LastEditDate = Now.Date
            .LastEditBY = AppConfigData.UserGUID
            UpdateStatusbarInfo(.CreatedBy, .CreationDate, .LastEditBY, .LastEditDate, lblCreated, lblModified)

        End With
        Try
            Dim Msg As String = objSAPItem.Save()
            If String.IsNullOrEmpty(Msg) Then
                txtSAPPartNo.Properties.ReadOnly = True
                ActionResult = 0
                ListDataLoad()
            Else
                Messages.ErrorMessage(Msg, WhoCalledMe)
                ActionResult = -1
            End If
        Catch ex As Exception
            Messages.ErrorMessage(ex.Message, WhoCalledMe)
            ActionResult = -1
        End Try
    End Sub

    Private Sub btnApprove_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnApprove.ItemClick
        If Messages.QuestionMessage("Are you sure to approve the changes?") = Windows.Forms.DialogResult.Yes Then
            objSAPItem.Attributes.Warranty = objSAPItem.Attributes.NewWarranty
            objSAPItem.Attributes.CheckUniqueSNOnReceiving = objSAPItem.Attributes.NewCheckUniqueSNOnReceiving
            objSAPItem.Attributes.UpdateAssetSN = objSAPItem.Attributes.NewUpdateAssetSN
            SaveData()
        End If
    End Sub
End Class
