Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class ctlLov
    Public Event SelectTextChanged As System.EventHandler

    Public Event OnbtnDownClicked As System.EventHandler
    Public Event OnbtnAddClicked As System.EventHandler

    Private WithEvents PopupGrid As New DevExpress.XtraEditors.PopupContainerControl
    Private WithEvents grdLOV As New DevExpress.XtraGrid.GridControl
    Private WithEvents grdLOVMainView As New DevExpress.XtraGrid.Views.Grid.GridView

    Private WithEvents PopupDataEditing As DevExpress.XtraEditors.PopupContainerControl
    Private WithEvents btnSelectRecord As DevExpress.XtraBars.BarButtonItem

    Private _ValueMember As String = ""
    Private _DisplayMember As String = ""
    Private SelectedIndex As Integer = -1
    Private _HideValueMember As Boolean = True
    Private DataAddingControl As ctlDataEditing

    Private Sub btnSelectRecord_Clicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectRecord.ItemClick
        If DataAddingControl.cmbList.EditValue IsNot Nothing Then
            'SelectedValue = DataAddingControl.RecordGUID.ToString
            txtData.Tag = DataAddingControl.cmbList.EditValue
            txtData.Text = DataAddingControl.cmbList.Edit.GetDisplayText(txtData.Tag)
        End If
        ClosePopup()
    End Sub

    Public Property btnAddDataVisible() As Boolean
        Get
            Return txtData.Properties.Buttons(1).Visible
        End Get
        Set(ByVal value As Boolean)
            txtData.Properties.Buttons(1).Visible = value
        End Set
    End Property

    Public ReadOnly Property DataEditingControl() As ctlDataEditing
        Get
            Return DataAddingControl
        End Get
    End Property

    Public Sub SetDataAddingControl(ByVal ctl As ctlDataEditing)
        PopupDataEditing = New DevExpress.XtraEditors.PopupContainerControl
        btnSelectRecord = New DevExpress.XtraBars.BarButtonItem
        DataAddingControl = ctl
        ctl.isControl = True
        PopupDataEditing.Size = New System.Drawing.Size(DataAddingControl.Width, DataAddingControl.Height)
        PopupDataEditing.Controls.Add(DataAddingControl)
        DataAddingControl.Dock = System.Windows.Forms.DockStyle.Fill
        AddSelectButtonToControl()
    End Sub

    Private Sub AddSelectButtonToControl()
        Me.btnSelectRecord.Caption = My.Resources.Strings.btnSelectRecordCaption
        Me.btnSelectRecord.Glyph = My.Resources.Resources.check
        Me.btnSelectRecord.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectRecord.Hint = My.Resources.Strings.btnSelectRecordHint
        Me.btnSelectRecord.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.F2))
        btnSelectRecord.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        'ForceInitialize to Insert the button at the end 
        DataAddingControl.BarManager1.ForceInitialize()
        DataAddingControl.barStatus.AddItem(btnSelectRecord)
    End Sub

    Public Property HideValueMember() As Boolean
        Get
            Return _HideValueMember
        End Get
        Set(ByVal value As Boolean)
            _HideValueMember = value
        End Set
    End Property

    Private Sub ctlLov_FontChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.FontChanged
        txtData.Font = Me.Font
    End Sub

    Private Sub ctlLov_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtData.Properties.CloseOnOuterMouseClick = False

        Me.Controls.Add(PopupGrid)
        PopupGrid.Controls.Add(grdLOV)
        PopupGrid.Size = New System.Drawing.Size(txtData.Width, 150)
        GridFormat()
        txtData.Properties.PopupControl = PopupGrid

    End Sub

    Private Sub GridFormat()
        '
        'grdLov
        '
        Me.grdLOV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdLOV.MainView = Me.grdLOVMainView
        Me.grdLOV.Name = "grdLOV"
        Me.grdLOV.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdLOVMainView})
        '
        'grdLOVMainView
        '
        Me.grdLOVMainView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdLOVMainView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grdLOVMainView.GridControl = Me.grdLOV
        Me.grdLOVMainView.Name = "grdLOVMainView"
        Me.grdLOVMainView.OptionsBehavior.Editable = False
        Me.grdLOVMainView.OptionsCustomization.AllowColumnMoving = False
        Me.grdLOVMainView.OptionsCustomization.AllowGroup = False
        Me.grdLOVMainView.OptionsFilter.AllowFilterEditor = False
        Me.grdLOVMainView.OptionsMenu.EnableColumnMenu = False
        Me.grdLOVMainView.OptionsMenu.EnableFooterMenu = False
        Me.grdLOVMainView.OptionsMenu.EnableGroupPanelMenu = False
        Me.grdLOVMainView.OptionsNavigation.UseTabKey = False
        Me.grdLOVMainView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdLOVMainView.OptionsSelection.UseIndicatorForSelection = False
        Me.grdLOVMainView.OptionsView.ColumnAutoWidth = False
        Me.grdLOVMainView.OptionsView.ShowAutoFilterRow = True
        Me.grdLOVMainView.OptionsView.ShowGroupPanel = False
        Me.grdLOVMainView.OptionsView.ShowIndicator = False
        Me.grdLOVMainView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
        Me.grdLOVMainView.OptionsView.ColumnAutoWidth = True
    End Sub



    Public Property ValueMember() As String
        Get
            Return _ValueMember
        End Get
        Set(ByVal value As String)
            _ValueMember = value
        End Set
    End Property

    Public Property DisplayMember() As String
        Get
            Return _DisplayMember
        End Get

        Set(ByVal value As String)
            _DisplayMember = value
        End Set
    End Property

    Public Property SelectedValue() As Object
        Get
            Return txtData.Tag
        End Get

        Set(ByVal value As Object)
            If IsNullOrEmptyValue(value) Then
                txtData.Text = Nothing
                txtData.Tag = Nothing
            Else
                Dim rowGUID As Guid
                If TryStrToGuid(value.ToString, rowGUID) Then
                    value = rowGUID
                End If
                txtData.Tag = value

                'We have to update the data source to be able to get the text of the value.
                'we can't load datasource just if it's null, becuase we may need data not listed because of filter.
                RaiseEvent OnbtnDownClicked(Nothing, Nothing)

                If Not grdLOV.DataSource Is Nothing Then
                    txtData.Text = GetDisplayTextByKeyValue(grdLOV.DataSource, value, ValueMember, DisplayMember)
                    'Dim rowIndex As Integer = -1
                    'rowIndex = grdLOVMainView.LocateByValue(0, grdLOVMainView.Columns(ValueMember), value)
                    'If rowIndex <> DevExpress.XtraGrid.GridControl.InvalidRowHandle Then
                    '    txtData.Text = grdLOVMainView.GetRowCellValue(rowIndex, DisplayMember)
                    'End If
                End If
            End If
        End Set
    End Property

    Public Property SelectedText() As String
        Get
            Return txtData.Text
        End Get

        Set(ByVal value As String)
            txtData.Text = value
        End Set

    End Property
    Public ReadOnly Property TextBox() As DevExpress.XtraEditors.TextEdit
        Get
            Return txtData
        End Get
    End Property

    Public Property DataSource() As DataTable
        Get
            Return grdLOV.DataSource
        End Get
        Set(ByVal value As DataTable)
            grdLOV.DataSource = value
        End Set
    End Property



    Private Sub txtData_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtData.ButtonClick
        If e.Button.Index = 0 Then
            txtData.Properties.PopupControl = PopupGrid
        Else
            txtData.Properties.PopupControl = PopupDataEditing
        End If
        txtData.Properties.ActionButtonIndex = e.Button.Index
        txtData.ShowPopup()
    End Sub


    Private Sub txtData_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtData.Popup
        If Not IsNullOrEmptyValue(SelectedValue) Then
            Dim index As Integer = -1
            ' Support GUID Search, because the locate method needs the value to 
            index = grdLOVMainView.LocateByValue(0, grdLOVMainView.Columns(ValueMember), SelectedValue)
            Dim LocateValue As Guid
            If TryStrToGuid(SelectedValue.ToString, LocateValue) Then
                SelectedValue = LocateValue
            End If
            index = grdLOVMainView.LocateByValue(0, grdLOVMainView.Columns(ValueMember), SelectedValue)

            'If TryStrToGuid(SelectedValue, LocateValue) Then
            '    index = grdLOVMainView.LocateByValue(0, grdLOVMainView.Columns(ValueMember), LocateValue)
            'Else
            '    index = grdLOVMainView.LocateByValue(0, grdLOVMainView.Columns(ValueMember), SelectedValue)
            'End If
            If index >= 0 Then
                grdLOVMainView.FocusedRowHandle = index
                grdLOVMainView.MakeRowVisible(grdLOVMainView.FocusedRowHandle, True)
            Else ' Select filter row
                grdLOVMainView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
                grdLOVMainView.FocusedColumn = grdLOVMainView.VisibleColumns(0)
                grdLOVMainView.ShowEditor()
            End If
        Else ' Select filter row
            grdLOVMainView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
            grdLOVMainView.FocusedColumn = grdLOVMainView.VisibleColumns(0)
            grdLOVMainView.ShowEditor()
        End If
    End Sub

    Private Sub txtData_QueryPopUp(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtData.QueryPopUp

        If txtData.Properties.ActionButtonIndex = 0 Then
            RaiseEvent OnbtnDownClicked(sender, e)

            Dim k As Integer = 0
            For i As Integer = 0 To grdLOVMainView.Columns.Count - 1
                k = k + grdLOVMainView.Columns(i).Width
                grdLOVMainView.Columns(i).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                grdLOVMainView.Columns(i).AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
            Next

            If PopupGrid.Width < k Then
                PopupGrid.Width = k
            End If
            If HideValueMember Then
                If grdLOVMainView.Columns.ColumnByFieldName(ValueMember) IsNot Nothing Then
                    grdLOVMainView.Columns(ValueMember).Visible = False
                End If
            End If
        Else
            RaiseEvent OnbtnAddClicked(sender, e)
        End If
    End Sub

    Private Sub grdLOVMainView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdLOVMainView.Click
        'disable click on the footer and other parts.
        Dim GHI As New GridHitInfo()
        GHI = grdLOVMainView.CalcHitInfo(CType(e, DevExpress.Utils.DXMouseEventArgs).Location)
        If GHI.InRow Then
            Dim FocRow As Integer = grdLOVMainView.FocusedRowHandle
            If FocRow >= 0 Then
                SelectedValue = grdLOVMainView.GetRowCellValue(FocRow, ValueMember).ToString
                SelectedText = grdLOVMainView.GetRowCellValue(FocRow, DisplayMember).ToString
            End If
            ClosePopup()
        End If
    End Sub

    Private Sub grdLOV_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdLOV.KeyDown
        If e.KeyData = Keys.Enter Then
            If grdLOVMainView.RowCount > 0 Then
                Dim FocRow As Integer = grdLOVMainView.FocusedRowHandle
                If FocRow >= 0 Then
                    SelectedValue = grdLOVMainView.GetRowCellValue(FocRow, ValueMember).ToString
                    SelectedText = grdLOVMainView.GetRowCellValue(FocRow, DisplayMember).ToString
                End If
                ClosePopup()
            End If
        End If
    End Sub

    Private Sub txtData_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtData.KeyDown
        If e.KeyData = Keys.Delete Or e.KeyData = Keys.Back Then
            SelectedValue = Nothing
            SelectedText = Nothing
            RaiseEvent SelectTextChanged(Nothing, Nothing)
        End If
    End Sub
    Private Sub txtData_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtData.TextChanged
        RaiseEvent SelectTextChanged(sender, e)
    End Sub

    ' call PopupContainerEdit.ClosePopup
    Private Sub ClosePopup()
        txtData.ClosePopup()
    End Sub
End Class
