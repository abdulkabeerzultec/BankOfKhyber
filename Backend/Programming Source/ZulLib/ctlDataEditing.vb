Imports DevExpress.XtraEditors
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports GenericDAL
Imports System.ComponentModel

Public Class ctlDataEditing
    Inherits System.Windows.Forms.UserControl

    Enum TClosingWays
        ParentDisposing
        ParentHiding
    End Enum

    Enum TRecordStates
        ModifiedRecord = 0
        NewRecord = 1
    End Enum

    Enum TControlStatus
        isLoading = 0
        isSaving = 1
        isDeleteing = 2
        isNew = 3
        isNothing = 4
    End Enum

    Public Structure TUniqueField
        Public Sub New(ByVal fldName As String, ByVal ctl As Control)
            FieldName = fldName
            ctlFieldText = ctl
        End Sub

        Public FieldName As String
        Public ctlFieldText As Control
    End Structure



    Public Delegate Sub MyLoadSub()
    Public Event OnLoadData As MyLoadSub
    Public Event OnDeleteData As MyLoadSub
    Public Event OnSaveData As MyLoadSub
    Public Event OnNewData As MyLoadSub
    Public Event OnPrintClicked As MyLoadSub
    Public Event OnPreviewClicked As MyLoadSub
    Public Event OnSaveFailed As MyLoadSub

    'Dim session1 As DevExpress.Xpo.Session = New DevExpress.Xpo.Session()
    'Dim xpServerCollectionSource1 As DevExpress.Xpo.XPServerCollectionSource

    Protected ActionResult As Integer
    Private _RecordGUID As Guid

    Private _RecordStatus As TRecordStates = TRecordStates.NewRecord
    Private _RecordChanged As Boolean
    Private _CurrentAction As TControlStatus
    Private _UniqueFields As New Generic.List(Of TUniqueField)

    Private _EditFormCaption As String
    Private _NewFormCaption As String

    Private _TableName As String = String.Empty
    Private _PrimaryKey As String = String.Empty
    Private _OrderField As String = String.Empty
    Private _NavigationFilter As String = String.Empty
    Private _isControl As Boolean = False
    Private _BusnissLayerObject As GenericDAL.Base
    Private _HideValueMember As Boolean = True
    Private _HideDataList As Boolean = False
    Private _ClosingWay As TClosingWays = TClosingWays.ParentDisposing

    'To Sort the list by the display member of the business layer.
    'Public WriteOnly Property SortByDisplayMember() As Boolean
    '    Set(ByVal value As Boolean)
    '        If value Then
    '            cmbListRepositoryItem.View.SortInfo.Add(cmbListRepositoryItem.View.Columns(ListDisplayMember), DevExpress.Data.ColumnSortOrder.Ascending)
    '        End If
    '    End Set
    'End Property

    Public Property ClosingWay() As TClosingWays
        Get
            Return _ClosingWay
        End Get
        Set(ByVal value As TClosingWays)
            _ClosingWay = value
        End Set
    End Property

    Public WriteOnly Property HideNavigationButtons() As Boolean
        Set(ByVal value As Boolean)
            If value Then
                btnFirst.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                btnLast.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                btnNext.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                btnPrev.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Else
                btnFirst.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                btnLast.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                btnNext.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                btnPrev.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        End Set
    End Property

    Public Property HideDataList() As Boolean
        Get
            Return _HideDataList
        End Get
        Set(ByVal value As Boolean)
            _HideDataList = value
            If _HideDataList Then
                cmbList.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                cmbList.Enabled = False
            Else
                cmbList.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                cmbList.Enabled = True
            End If
        End Set
    End Property

    Public Property BusnissLayerObject() As GenericDAL.Base
        Get
            Return _BusnissLayerObject
        End Get
        Set(ByVal value As GenericDAL.Base)
            _BusnissLayerObject = value
            TableName = value.TableName
            ListDisplayMember = value.ListDisplayMember
            ListValueMember = value.ListValueMember
            PrimaryKey = value.PrimaryKey
            OrderField = value.OrderField

            ListDataSourceType = value.ListDataTableType
            'If value.ListDataTableType IsNot Nothing Then
            '    ListDataSourceType = value.ListDataTableType
            'Else
            '    HideDataList = True
            'End If
        End Set
    End Property

    Public Property ListDataSource() As DataTable
        Get
            Return cmbListRep.DataSource
        End Get
        Set(ByVal value As DataTable)
            cmbListRep.DataSource = value
            cmbListRep.ServerMode = False
            If cmbListRep.View.Columns.ColumnByFieldName(cmbListRep.ValueMember) IsNot Nothing Then
                cmbListRep.View.Columns(cmbListRep.ValueMember).Visible = False
            End If
        End Set
    End Property

    Public WriteOnly Property ListDataSourceType() As System.Type
        Set(ByVal value As System.Type)
            '' Create an XPClassInfo object corresponding to the Person_Contact class. 
            'Dim classInfo As DevExpress.Xpo.Metadata.XPClassInfo = session1.GetClassInfo(value)
            '' Create an XPServerCollectionSource object. 
            'xpServerCollectionSource1 = New DevExpress.Xpo.XPServerCollectionSource(session1, classInfo)
            'If Not String.IsNullOrEmpty(NavigationFilter) Then
            '    xpServerCollectionSource1.FixedFilterString = NavigationFilter
            'End If

            'cmbListRepositoryItem.DataSource = xpServerCollectionSource1
            'cmbListRepositoryItem.View.SortInfo.Add(cmbListRepositoryItem.View.Columns(ListDisplayMember), DevExpress.Data.ColumnSortOrder.Ascending)

            'If HideValueMember Then
            If cmbListRep.View.Columns.ColumnByFieldName(cmbListRep.ValueMember) IsNot Nothing Then
                cmbListRep.View.Columns(cmbListRep.ValueMember).Visible = False
            End If
            'End If
        End Set
    End Property

    Public Property ListDisplayMember() As String
        Get
            Return cmbListRep.DisplayMember
        End Get
        Set(ByVal value As String)
            cmbListRep.DisplayMember = value
        End Set
    End Property

    Public Property ListValueMember() As String
        Get
            Return cmbListRep.ValueMember
        End Get
        Set(ByVal value As String)
            cmbListRep.ValueMember = value
        End Set
    End Property

    Public Property ShowBarPrint() As Boolean
        Get
            Return barPrint.Visible
        End Get
        Set(ByVal value As Boolean)
            barPrint.Visible = value
        End Set
    End Property

    Public Property HideValueMember() As Boolean
        Get
            Return _HideValueMember
        End Get
        Set(ByVal value As Boolean)
            _HideValueMember = value
        End Set
    End Property


    Public Sub New()
        '        DevExpress.Xpo.XpoDefault.ConnectionString = _
        'DevExpress.Xpo.DB.MSSqlConnectionProvider.GetConnectionString(ConnectionString.ServerName, ConnectionString.UserName, ConnectionString.UserPass, ConnectionString.DbName)
        InitializeComponent()
    End Sub

    Public Property isControl() As Boolean
        Get
            Return _isControl
        End Get
        Set(ByVal value As Boolean)
            _isControl = value
            If _isControl Then
                btnClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                btnClose.Enabled = False
            Else
                btnClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                btnClose.Enabled = True
            End If
        End Set
    End Property

    Public Property EditFormCaption()
        Get
            Return _EditFormCaption
        End Get
        Set(ByVal value)
            _EditFormCaption = value
        End Set
    End Property

    Public Property NewFormCaption()
        Get
            Return _NewFormCaption
        End Get
        Set(ByVal value)
            _NewFormCaption = value
        End Set
    End Property

    Public ReadOnly Property UniqueFields() As Generic.List(Of TUniqueField)
        Get
            Return _UniqueFields
        End Get
    End Property

    Public Property CurrentAction() As TControlStatus
        Get
            Return _CurrentAction
        End Get
        Set(ByVal value As TControlStatus)
            _CurrentAction = value
        End Set
    End Property

    Public Property TableName() As String
        Get
            Return _TableName
        End Get
        Set(ByVal value As String)
            _TableName = value
        End Set
    End Property

    Public Property PrimaryKey() As String
        Get
            Return _PrimaryKey
        End Get
        Set(ByVal value As String)
            _PrimaryKey = value
        End Set
    End Property

    Public Property OrderField() As String
        Get
            Return _OrderField
        End Get
        Set(ByVal value As String)
            _OrderField = value
        End Set
    End Property

    Public Property NavigationFilter() As String
        Get
            Return _NavigationFilter
        End Get
        Set(ByVal value As String)
            _NavigationFilter = value
        End Set
    End Property


    Public Property RecordGUID() As Guid
        Get
            Return _RecordGUID
        End Get
        Set(ByVal value As Guid)
            _RecordGUID = value
        End Set
    End Property

    Protected Property RecordStatus() As TRecordStates
        Get
            Return _RecordStatus
        End Get
        Set(ByVal value As TRecordStates)
            _RecordStatus = value
        End Set
    End Property

    Protected Property RecordChanged() As Boolean
        Get
            Return _RecordChanged
        End Get
        Set(ByVal value As Boolean)
            _RecordChanged = value
            If _RecordChanged Then
                btnSave.Enabled = True
                btnCancel.Enabled = True
                btnSaveAndNew.Enabled = True
            Else
                btnSave.Enabled = False
                btnCancel.Enabled = False
                btnSaveAndNew.Enabled = False
            End If
        End Set
    End Property

    Protected Sub btnSave_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSave.ItemClick
        SaveData()
    End Sub

    Protected Sub btnFirst_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnFirst.ItemClick
        Dim rowGUID As Guid = DBOperations.GetFirstPKey(PrimaryKey, OrderField, TableName, NavigationFilter)
        If RecordChanged Then
            Dim rslt As DialogResult = ShowSaveConfirmation()
            If rslt = DialogResult.Yes Then
                If SaveData() = 0 Then
                    RecordGUID = rowGUID
                    LoadData()
                End If
            ElseIf rslt = DialogResult.No Then
                RecordGUID = rowGUID
                LoadData()
            End If
        Else
            RecordGUID = rowGUID
            LoadData()
        End If
    End Sub

    Protected Sub btnPrev_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPrev.ItemClick

        Dim rowGUID As Guid = DBOperations.GetPrevPKey(PrimaryKey, RecordGUID.ToString, OrderField, TableName, NavigationFilter)

        If rowGUID = Guid.Empty Then
            btnNext.Enabled = True
            btnLast.Enabled = True
            btnPrev.Enabled = False
            btnFirst.Enabled = False
        Else
            If RecordChanged Then
                Dim rslt As DialogResult = ShowSaveConfirmation()
                If rslt = DialogResult.Yes Then
                    If SaveData() = 0 Then
                        RecordGUID = rowGUID
                        LoadData()
                    End If
                ElseIf rslt = DialogResult.No Then
                    RecordGUID = rowGUID
                    LoadData()
                End If
            Else
                RecordGUID = rowGUID
                LoadData()
            End If
        End If

    End Sub


    Protected Sub btnNext_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnNext.ItemClick
        Dim rowGUID As Guid = DBOperations.GetNextPKey(PrimaryKey, RecordGUID.ToString, OrderField, TableName, NavigationFilter)
        If rowGUID = Guid.Empty Then
            btnNext.Enabled = False
            btnLast.Enabled = False
            btnPrev.Enabled = True
            btnFirst.Enabled = True
        Else
            If RecordChanged Then
                Dim rslt As DialogResult = ShowSaveConfirmation()
                If rslt = DialogResult.Yes Then
                    If SaveData() = 0 Then
                        RecordGUID = rowGUID
                        LoadData()
                    End If
                ElseIf rslt = DialogResult.No Then
                    RecordGUID = rowGUID
                    LoadData()
                End If
            Else
                RecordGUID = rowGUID
                LoadData()
            End If
        End If
    End Sub

    Protected Sub btnLast_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLast.ItemClick
        Dim rowGUID As Guid = DBOperations.GetLastPKey(PrimaryKey, OrderField, TableName, NavigationFilter)
        If RecordChanged Then
            Dim rslt As DialogResult = ShowSaveConfirmation()
            If rslt = DialogResult.Yes Then
                If SaveData() = 0 Then
                    RecordGUID = rowGUID
                    LoadData()
                End If
            ElseIf rslt = DialogResult.No Then
                RecordGUID = rowGUID
                LoadData()
            End If
        Else
            RecordGUID = rowGUID
            LoadData()
        End If
    End Sub

    Private Sub RefershNavigationButtons()
        If DBOperations.GetRecordCount(TableName, NavigationFilter) < 2 Then
            btnNext.Enabled = False
            btnLast.Enabled = False
            btnPrev.Enabled = False
            btnFirst.Enabled = False
        ElseIf RecordGUID = DBOperations.GetFirstPKey(PrimaryKey, OrderField, TableName, NavigationFilter) Then
            btnNext.Enabled = True
            btnLast.Enabled = True
            btnPrev.Enabled = False
            btnFirst.Enabled = False
        ElseIf RecordGUID = DBOperations.GetLastPKey(PrimaryKey, OrderField, TableName, NavigationFilter) Then
            btnNext.Enabled = False
            btnLast.Enabled = False
            btnPrev.Enabled = True
            btnFirst.Enabled = True
        Else
            btnPrev.Enabled = True
            btnFirst.Enabled = True
            btnNext.Enabled = True
            btnLast.Enabled = True
        End If
    End Sub

    Private Sub btnRefersh_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRefresh.ItemClick
        RefershNavigationButtons()
        If RecordChanged Then
            Dim rslt As DialogResult = ShowSaveConfirmation()
            If rslt = DialogResult.Yes Then
                SaveData()
            ElseIf rslt = DialogResult.No Then
                LoadData()
            End If
        Else
            LoadData()
        End If
        'RecordChanged = False
    End Sub

    Public Overridable Sub btnClose_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnClose.ItemClick
        'Dim index = MainForm.tabControlMain.SelectedTabPageIndex
        If RecordChanged Then
            Dim rslt As DialogResult = ShowSaveConfirmation()
            If rslt = DialogResult.Yes Then
                If SaveData() = 0 Then
                    'If index = 0 Then
                    ' MainForm.tabControlMain.SelectedTabPageIndex = 0
                    'Else
                    'MainForm.tabControlMain.SelectedTabPageIndex = index - 1
                    'End If
                    Close()
                End If
            ElseIf rslt = DialogResult.No Then
                'If index = 0 Then
                '    MainForm.tabControlMain.SelectedTabPageIndex = 0
                'Else
                '    MainForm.tabControlMain.SelectedTabPageIndex = index - 1
                'End If
                Close()
            End If
        Else
            'If index = 0 Then
            '    MainForm.tabControlMain.SelectedTabPageIndex = 0
            'Else
            '    MainForm.tabControlMain.SelectedTabPageIndex = index - 1
            'End If
            Close()
        End If
    End Sub

    Private Sub btnSaveAndNew_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSaveAndNew.ItemClick
        If SaveData() = 0 Then
            NewData()
        End If
    End Sub

    Private Function CheckForUnique() As Integer
        Dim Result As Integer = 0
        If RecordStatus = TRecordStates.NewRecord Then
            'check for Unique fields in insert mode
            For Each UF As TUniqueField In UniqueFields
                If DBOperations.FieldValueExisted(UF.FieldName, UF.ctlFieldText.Text, TableName, True, PrimaryKey, RecordGUID.ToString, NavigationFilter) Then
                    errProv.SetError(UF.ctlFieldText, String.Format("{0} {1}", UF.FieldName, My.Resources.Strings.FieldValueExisted))
                    Result = -1
                End If
            Next
        Else
            'check for Unique fields in edit mode
            For Each UF As TUniqueField In UniqueFields
                If DBOperations.FieldValueExisted(UF.FieldName, UF.ctlFieldText.Text, TableName, False, PrimaryKey, RecordGUID.ToString, NavigationFilter) Then
                    errProv.SetError(UF.ctlFieldText, String.Format("{0} {1}", UF.FieldName, My.Resources.Strings.FieldValueExisted))
                    Result = -1
                End If
            Next
        End If
        Return Result
    End Function

    Protected Function SaveData() As Integer
        CurrentAction = TControlStatus.isSaving

        ' if the status is edit, we should check if the record found or not before saving it.
        If RecordStatus = TRecordStates.ModifiedRecord And DBOperations.CheckRecordFound(PrimaryKey, RecordGUID.ToString, TableName) <> 1 Then
            ShowRecordNotFound()
            ActionResult = -1
            If Not isControl Then
                Close()
            Else
                NewData()
            End If
        Else
            errProv.ClearErrors()
            If valProvMain.Validate() Then
                TrimControl(Me) 'To remove spaces from all text boxes

                'Call check for unique function to validate unique for the record.
                ActionResult = CheckForUnique()
                If ActionResult = 0 Then
                    RaiseEvent OnSaveData() ' call the child controls onSaveData event
                    If ActionResult = 0 Then
                        RefreshDataListValues()
                        barStatus.Visible = True
                        RecordStatus = TRecordStates.ModifiedRecord
                        btnDelete.Enabled = True
                        btnRefresh.Enabled = True
                        btnNew.Enabled = True
                        btnPrint.Enabled = True
                        btnPreview.Enabled = True
                        RefershNavigationButtons()
                        RecordChanged = False
                        If EditFormCaption = "" Then
                            Parent.Text = String.Format("Edit {0}", TableName)
                        Else
                            Parent.Text = EditFormCaption
                        End If
                    End If
                End If
            Else
                ShowCanNotSave()
                ActionResult = -1
            End If
        End If

        CurrentAction = TControlStatus.isNothing
        If ActionResult = -1 Then
            RaiseEvent OnSaveFailed()
        End If
        Return ActionResult
    End Function

    Private Sub RefreshDataListValues()
        'If Not HideDataList Then
        ListDataSourceType = BusnissLayerObject.ListDataTableType
        cmbList.EditValue = RecordGUID
        'End If
    End Sub

    Public Function LoadData() As Integer
        errProv.ClearErrors()
        CurrentAction = TControlStatus.isLoading

        If DBOperations.CheckRecordFound(PrimaryKey, RecordGUID.ToString, TableName) = 1 Then
            ClearControl(Me)
            RecordStatus = TRecordStates.ModifiedRecord
            RaiseEvent OnLoadData() ' call the child controls onLoadData Event
            If ActionResult = 0 Then
                barStatus.Visible = True
                cmbList.EditValue = RecordGUID

                If EditFormCaption = "" Then
                    Parent.Text = String.Format("Edit {0}", TableName)
                Else
                    Parent.Text = EditFormCaption
                End If
                btnDelete.Enabled = True
                btnRefresh.Enabled = True
                btnNew.Enabled = True
                btnPrint.Enabled = True
                btnPreview.Enabled = True
                RefershNavigationButtons()
                RecordChanged = False
                valProvMain.Validate()
            End If
        Else
            ShowRecordNotFound()
            ActionResult = -1
            If Not isControl Then
                Close()
            Else
                NewData()
            End If
        End If
        CurrentAction = TControlStatus.isNothing
        Return ActionResult
    End Function

    Public Function NewData() As Integer
        CurrentAction = TControlStatus.isNew
        ClearControl(Me)

        ' remove validation errors from the controls
        errProv.ClearErrors()
        Dim al As New ArrayList(DirectCast(valProvMain.GetInvalidControls(), ICollection))
        For Each ctl As Control In al
            valProvMain.RemoveControlError(ctl)
        Next
        '''''''''''''''''''''''''''''''''''''''''''''
        RecordGUID = Guid.NewGuid
        RecordStatus = TRecordStates.NewRecord
        cmbList.EditValue = Nothing
        RaiseEvent OnNewData() ' call the child controls OnNewData Event
        If ActionResult = 0 Then
            barStatus.Visible = False

            If NewFormCaption = "" Then
                Parent.Text = String.Format("New {0}", TableName)
            Else
                Parent.Text = NewFormCaption
            End If

            RecordChanged = False

            btnDelete.Enabled = False
            btnRefresh.Enabled = False
            btnNext.Enabled = False
            btnLast.Enabled = False
            btnFirst.Enabled = False
            btnPrev.Enabled = False
            btnNew.Enabled = False
            btnPrint.Enabled = False
            btnPreview.Enabled = False

            btnSave.Enabled = True
            btnCancel.Enabled = True
            btnSaveAndNew.Enabled = True
        End If
        CurrentAction = TControlStatus.isNothing
        Return ActionResult
    End Function

    Private Sub btnNew_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnNew.ItemClick
        If RecordChanged Then
            Dim rslt As DialogResult = ShowSaveConfirmation()
            If rslt = DialogResult.Yes Then
                If SaveData() = 0 Then
                    NewData()
                End If
            ElseIf rslt = DialogResult.No Then
                NewData()
            End If
        Else
            NewData()
        End If
    End Sub

    Private Sub btnDelete_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDelete.ItemClick
        CurrentAction = TControlStatus.isDeleteing

        ' check if the record is found before deleting it.
        If DBOperations.CheckRecordFound(PrimaryKey, RecordGUID.ToString, TableName) = 1 Then
            If ShowDeleteConfirmation() = DialogResult.Yes Then
                RaiseEvent OnDeleteData()
                If ActionResult = 0 Then
                    'load the last record if there is no recrod then load the last one...
                    Dim rowGUID As Guid = DBOperations.GetLastPKey(PrimaryKey, OrderField, TableName, NavigationFilter)
                    If rowGUID = Guid.Empty Then
                        If Not isControl Then
                            Close()
                        Else
                            NewData()
                        End If
                    Else
                        RecordGUID = rowGUID
                        LoadData()
                    End If
                End If
            End If
        Else
            ShowRecordNotFound()
            ActionResult = -1
            If Not isControl Then
                Close()
            Else
                NewData()
            End If
        End If
        CurrentAction = TControlStatus.isNothing
    End Sub

    Private Sub btnCancel_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCancel.ItemClick
        If RecordChanged Then
            If ShowCancelConfirmation() = DialogResult.Yes Then
                If RecordStatus = TRecordStates.ModifiedRecord Then
                    LoadData()
                Else
                    'load the last record if there is no recrod then load the last one...
                    Dim rowGUID As Guid = DBOperations.GetLastPKey(PrimaryKey, OrderField, TableName, NavigationFilter)
                    If rowGUID = Guid.Empty Then
                        If Not isControl Then
                            Close()
                        Else
                            NewData()
                        End If
                    Else
                        RecordGUID = rowGUID
                        LoadData()
                    End If
                End If
            End If
        Else
            'load the last record if there is no recrod then load the last one...
            Dim rowGUID As Guid = DBOperations.GetLastPKey(PrimaryKey, OrderField, TableName, NavigationFilter)
            If rowGUID = Guid.Empty Then
                If Not isControl Then
                    Close()
                Else
                    NewData()
                End If
            Else
                RecordGUID = rowGUID
                LoadData()
            End If
        End If
    End Sub

    Protected Overridable Sub ControlEditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If CurrentAction = TControlStatus.isNothing Then
            RecordChanged = True
        End If
    End Sub

    Public Sub AddEditValueChangedEvent(ByVal MainControl As Control)
        Dim ctl As Control
        For Each ctl In MainControl.Controls
            If TypeOf (ctl) Is GridLookUpEdit Then
                AddHandler CType(ctl, GridLookUpEdit).EditValueChanged, AddressOf ControlEditValueChanged
            ElseIf TypeOf (ctl) Is LookUpEdit Then
                AddHandler CType(ctl, LookUpEdit).EditValueChanged, AddressOf ControlEditValueChanged
            ElseIf TypeOf (ctl) Is TextEdit Then
                AddHandler CType(ctl, TextEdit).EditValueChanged, AddressOf ControlEditValueChanged
            ElseIf TypeOf (ctl) Is PictureEdit Then
                AddHandler CType(ctl, PictureEdit).EditValueChanged, AddressOf ControlEditValueChanged
            ElseIf TypeOf (ctl) Is CheckEdit Then
                AddHandler CType(ctl, CheckEdit).EditValueChanged, AddressOf ControlEditValueChanged
            ElseIf TypeOf (ctl) Is ctlLov Then
                AddHandler CType(ctl, ctlLov).SelectTextChanged, AddressOf ControlEditValueChanged
            ElseIf TypeOf (ctl) Is ctlTreeList Then
                AddHandler CType(ctl, ctlTreeList).SelectTextChanged, AddressOf ControlEditValueChanged
            End If
            If ctl.HasChildren() And Not (TypeOf ctl Is ctlDataEditing) Then
                AddEditValueChangedEvent(ctl)
            End If
        Next
    End Sub

    Private Sub cntBaseControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddEditValueChangedEvent(Me)
        cmbListRep.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
        'cmbListRepositoryItem.ServerMode = True
        cmbListRep.View.OptionsView.ShowAutoFilterRow = True
        cmbListRep.View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    End Sub

    Private Sub cmbListRepositoryItem_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbListRep.QueryPopUp
        'Refill the data from the database after popup to refersh the combodata.
        'RaiseEvent OnListDataLoad()

        'If cmbListRepositoryItem.DataSource IsNot Nothing Then
        '    'session1.DropIdentityMap()
        '    'xpServerCollectionSource1.Reload()
        'End If
    End Sub

    Private Sub cmbList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbList.EditValueChanged
        If CurrentAction = TControlStatus.isNothing Then
            If cmbList.EditValue IsNot Nothing Then
                If Not String.IsNullOrEmpty(cmbList.EditValue.ToString) And cmbList.EditValue <> Guid.Empty Then
                    If RecordChanged Then
                        Dim rslt As DialogResult = ShowSaveConfirmation()
                        If rslt = DialogResult.Yes Then
                            If SaveData() = 0 Then
                                RecordGUID = cmbList.EditValue
                                LoadData()
                            End If
                        ElseIf rslt = DialogResult.No Then
                            RecordGUID = cmbList.EditValue
                            LoadData()
                        Else
                            'Return the value to the previous record.
                            CurrentAction = TControlStatus.isLoading
                            cmbList.EditValue = RecordGUID
                            CurrentAction = TControlStatus.isNothing
                        End If
                    Else
                        RecordGUID = cmbList.EditValue
                        LoadData()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnPrint_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPrint.ItemClick
        RaiseEvent OnPrintClicked()
    End Sub

    Private Sub btnPreview_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPreview.ItemClick
        RaiseEvent OnPreviewClicked()
    End Sub

    Public Sub Close()
        If ClosingWay = TClosingWays.ParentDisposing Then
            Parent.Dispose()
        ElseIf ClosingWay = TClosingWays.ParentHiding Then
            ParentForm.Owner = Nothing
            ParentForm.Parent = Nothing
            ParentForm.TopLevel = False
            ParentForm.Hide()
        End If
    End Sub

End Class


'if changes has been made to the current Detail View's object then the program will behaves in the following way:
'1- When the Cancel Button Clicked, the following confirmation message Should shown: "Do you want to cancel your changes?"
'2- When the Refresh Button Clicked, the following confirmation message Should shown: "Do you want to save changes?"
'3- When the window with the current Detail View is closed, the following confirmation message Should shown: "Do you want to save changes?"
'4- When the current Detail View's object is replaced with another (e.g. the Next button or Previous button is Clicked), the following confirmation message is shown: "Do you want to save changes?"
'5- When the New Button Clicked, the following confirmation message Should shown: "Do you want to save changes?"
