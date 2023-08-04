Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Imports System.Windows.Forms

Public Class frmChangeValues
    Private _SelectedRecordID As String
    Dim objBALAssetDetails As New BALAssetDetails
    Public Property SelectedRecordID() As String
        Get
            Return _SelectedRecordID
        End Get
        Set(ByVal value As String)
            _SelectedRecordID = value
        End Set
    End Property


    Private _SelectedRecordText As String
    Public Property SelectedRecordText() As String
        Get
            Return _SelectedRecordText
        End Get
        Set(ByVal value As String)
            _SelectedRecordText = value
        End Set
    End Property


    Private _SelectedLocation As String
    Public Property SelectedLocation() As String
        Get
            Return _SelectedLocation
        End Get
        Set(ByVal value As String)
            _SelectedLocation = value
        End Set
    End Property


    Public Property Caption()
        Get
            Return lblCaption.Text
        End Get
        Set(ByVal value)
            lblCaption.Text = value
        End Set
    End Property

    Private Sub frmChangeValues_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.BackgroundImage = My.Resources.Forms_BG
        Me.BackgroundImageLayout = ImageLayout.Stretch

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If cmbAssetsStatus.Visible Then
            SelectedRecordID = cmbAssetsStatus.EditValue
            SelectedRecordText = cmbAssetsStatus.Text
            Me.Close()
        End If
        If cmbBrand.Visible Then
            SelectedRecordID = cmbBrand.EditValue
            SelectedRecordText = cmbBrand.Text
            Me.Close()
        End If
        If cmbCustodian.Visible Then
            If cmbCustodian.Text = "" Or cmbLocation.Text = "" Then
                ShowErrorMessage("Both Custodian and Location must be selected!")
            Else
                SelectedRecordID = cmbCustodian.EditValue
                SelectedRecordText = cmbCustodian.Text
                SelectedLocation = cmbLocation.EditValue
                Me.Close()
            End If
        End If
    End Sub

    Private Sub btnBlkCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlkCancel.Click
        SelectedRecordID = ""
        SelectedRecordText = ""
        Me.Close()
    End Sub

    Private Sub PopulateAssetsStatus()
        cmbAssetsStatus.Properties.ValueMember = "ID"
        cmbAssetsStatus.Properties.DisplayMember = "Status"
        cmbAssetsStatus.Properties.DataSource = objBALAssetDetails.GetAssetStatus(False, True)
    End Sub

    Private Sub cmbAssetsStatus_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbAssetsStatus.QueryPopUp
        PopulateAssetsStatus()
    End Sub

    Private Sub cmbBrand_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbBrand.QueryPopUp
        PopulateBrands()
    End Sub

    Private Sub PopulateBrands()
        Dim objBALBrand As New BALbrand
        cmbBrand.Properties.ValueMember = "AstBrandID"
        cmbBrand.Properties.DisplayMember = "AstBrandName"
        cmbBrand.Properties.DataSource = objBALBrand.GetAll_Brand(New attBrand)
    End Sub


    Private Sub cmbCustodian_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbCustodian.QueryPopUp
        PopulateCustodian()
    End Sub

    Private Sub PopulateCustodian()
        Dim objBALCust As New BALCustodian
        cmbCustodian.Properties.ValueMember = "ID"
        cmbCustodian.Properties.DisplayMember = "Name"
        cmbCustodian.Properties.DataSource = objBALCust.GetAllData_GetCombo()
    End Sub

    Private Sub cmbLocation_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbLocation.QueryPopUp
        PopulateLocation()
    End Sub
    Public Sub PopulateLocation()

        Dim objBALLocation As New BALLocation
        cmbLocation.Properties.ValueMember = "LocID"
        cmbLocation.Properties.DisplayMember = "LocationFullPath"
        cmbLocation.Properties.DataSource = objBALLocation.GetComboLocationsSortedByCode(New attLocation)
        cmbLocation.Properties.View.Columns("Code").Visible = False
        cmbLocation.Properties.View.Columns("locLevel").Visible = False
        cmbLocation.Properties.View.Columns("CompanyID").Visible = False
        cmbLocation.Properties.View.Columns("CompanyName").Visible = False

        cmbLocation.Properties.View.Columns("LocID").Visible = False
        cmbLocation.Properties.View.Columns("CompCode").Caption = "Loc Code"
        cmbLocation.Properties.View.Columns("CompCode").VisibleIndex = 0
        cmbLocation.Properties.View.Columns("LocDesc").Visible = False
    End Sub
    
End Class