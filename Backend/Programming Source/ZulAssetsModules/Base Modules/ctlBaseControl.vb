Imports ZulAssetsDAL.ZulAssetsDAL
Imports ZulHRDAL.EmployeesTableAdapters
Imports ZulHRDAL.PreferenceTableAdapters
Imports DevExpress.XtraEditors
Imports System.Windows.Forms

Public Class ctlBaseControl
    Inherits System.Windows.Forms.UserControl

    Protected EmpNameAdapter As New EmplyeesNamesTableAdapter
    Protected SysPrefAdapter As New SysPreferenceTableAdapter

    Enum TImageType
        Passport = 0
        Iqama = 1
        Employee = 2
    End Enum


    Enum TRecordStatus
        isEdit = 0
        isInsert = 1
    End Enum

    Enum TCurrentActions
        isLoading = 0
        isSaving = 1
        isDeleteing = 2
        isNew = 3
        isNothing = 4
    End Enum

    Public Structure TUniqueField
        Public FieldName As String
        Public ctlFieldText As Control
        Public ctlPrimaryKey As Control
    End Structure



    Public Delegate Sub MyLoadSub()
    Public Event OnLoadData As MyLoadSub
    Public Event OnDeleteData As MyLoadSub
    Public Event OnSaveData As MyLoadSub
    Public Event OnNewData As MyLoadSub

    Protected ActionResult As Integer
    Private _RecordID As Integer

    Private _RecordStatus As TRecordStatus = TRecordStatus.isInsert
    Private _RecordChanged As Boolean
    Private _CurrentAction As TCurrentActions
    Private _UniqueFields As New Generic.List(Of TUniqueField)

    Private _EditFormCaption As String
    Private _NewFormCaption As String

    Private _TableName As String
    Private _PrimaryKey As String
    Private _NavigationFilter As String
    Private _isControl As Boolean

    Protected valProvMain As New DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider
    Protected errProv As New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider

    Public Sub New()
        InitializeComponent()
        _TableName = ""
        _PrimaryKey = ""
        _NavigationFilter = ""
    End Sub

    Public Property isControl() As Boolean
        Get
            Return _isControl
        End Get
        Set(ByVal value As Boolean)
            _isControl = value
            If _isControl Then
                btnClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Else
                btnClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
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
        'Set(ByVal value As Generic.List(Of TUniqueFields))
        '    _UniqueFields = value
        'End Set
    End Property

    Public Property CurrentAction() As TCurrentActions
        Get
            Return _CurrentAction
        End Get
        Set(ByVal value As TCurrentActions)
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

    Public Property NavigationFilter() As String
        Get
            Return _NavigationFilter
        End Get
        Set(ByVal value As String)
            _NavigationFilter = value
        End Set
    End Property

    Public Property RecordID() As Integer
        Get
            Return _RecordID
        End Get
        Set(ByVal value As Integer)
            _RecordID = value
        End Set
    End Property

    Public Property RecordStatus() As TRecordStatus
        Get
            Return _RecordStatus
        End Get
        Set(ByVal value As TRecordStatus)
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
        Dim ID As Integer = GetFirstPKey(PrimaryKey, TableName, NavigationFilter)
        If RecordChanged Then
            Dim rslt As DialogResult = ShowSaveConfirmation()
            If rslt = DialogResult.Yes Then
                If SaveData() = 0 Then
                    RecordID = ID
                    LoadData()
                End If
            ElseIf rslt = DialogResult.No Then
                RecordID = ID
                LoadData()
            End If
        Else
            RecordID = ID
            LoadData()
        End If
    End Sub

    Protected Sub btnPrev_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPrev.ItemClick
        Dim ID As Integer = GetPrevPKey(PrimaryKey, RecordID, TableName, NavigationFilter)
        If ID = 0 Then
            btnNext.Enabled = True
            btnLast.Enabled = True
            btnPrev.Enabled = False
            btnFirst.Enabled = False
        Else
            If RecordChanged Then
                Dim rslt As DialogResult = ShowSaveConfirmation()
                If rslt = DialogResult.Yes Then
                    If SaveData() = 0 Then
                        RecordID = ID
                        LoadData()
                    End If
                ElseIf rslt = DialogResult.No Then
                    RecordID = ID
                    LoadData()
                End If
            Else
                RecordID = ID
                LoadData()
            End If
        End If
    End Sub

    Protected Sub btnNext_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnNext.ItemClick
        Dim ID As Integer = GetNextPKey(PrimaryKey, RecordID, TableName, NavigationFilter)
        If ID = 0 Then
            btnNext.Enabled = False
            btnLast.Enabled = False
            btnPrev.Enabled = True
            btnFirst.Enabled = True
        Else
            If RecordChanged Then
                Dim rslt As DialogResult = ShowSaveConfirmation()
                If rslt = DialogResult.Yes Then
                    If SaveData() = 0 Then
                        RecordID = ID
                        LoadData()
                    End If
                ElseIf rslt = DialogResult.No Then
                    RecordID = ID
                    LoadData()
                End If
            Else
                RecordID = ID
                LoadData()
            End If
        End If
    End Sub

    Protected Sub btnLast_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLast.ItemClick
        Dim ID As Integer = GetLastPKey(PrimaryKey, TableName, NavigationFilter)
        If RecordChanged Then
            Dim rslt As DialogResult = ShowSaveConfirmation()
            If rslt = DialogResult.Yes Then
                If SaveData() = 0 Then
                    RecordID = ID
                    LoadData()
                End If
            ElseIf rslt = DialogResult.No Then
                RecordID = ID
                LoadData()
            End If
        Else
            RecordID = ID
            LoadData()
        End If
    End Sub

    Private Sub RefershNavigationButtons()
        If GetRecordCount(TableName, NavigationFilter) < 2 Then
            btnNext.Enabled = False
            btnLast.Enabled = False
            btnPrev.Enabled = False
            btnFirst.Enabled = False
        ElseIf RecordID = GetFirstPKey(PrimaryKey, TableName, NavigationFilter) Then
            btnNext.Enabled = True
            btnLast.Enabled = True
            btnPrev.Enabled = False
            btnFirst.Enabled = False
        ElseIf RecordID = GetLastPKey(PrimaryKey, TableName, NavigationFilter) Then
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
        Dim index = MainForm.tabControlMain.SelectedTabPageIndex
        If RecordChanged Then
            Dim rslt As DialogResult = ShowSaveConfirmation()
            If rslt = DialogResult.Yes Then
                If SaveData() = 0 Then
                    If index = 0 Then
                        MainForm.tabControlMain.SelectedTabPageIndex = 0
                    Else
                        MainForm.tabControlMain.SelectedTabPageIndex = index - 1
                    End If
                    Parent.Dispose()
                End If
            ElseIf rslt = DialogResult.No Then
                If index = 0 Then
                    MainForm.tabControlMain.SelectedTabPageIndex = 0
                Else
                    MainForm.tabControlMain.SelectedTabPageIndex = index - 1
                End If
                Parent.Dispose()
            End If
        Else
            If index = 0 Then
                MainForm.tabControlMain.SelectedTabPageIndex = 0
            Else
                MainForm.tabControlMain.SelectedTabPageIndex = index - 1
            End If
            Parent.Dispose()
        End If
    End Sub

    Private Sub btnSaveAndNew_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSaveAndNew.ItemClick
        If SaveData() = 0 Then
            NewData()
        End If
    End Sub

    Private Function SaveData() As Integer
        CurrentAction = TCurrentActions.isSaving

        ' if the status is edit, we should check if the record found or not before saving it.
        If RecordStatus = TRecordStatus.isEdit And CheckRecordFound(PrimaryKey, RecordID, TableName) <> 1 Then
            ShowRecordNotFound()
            ActionResult = -1
            If Not isControl Then
                Parent.Dispose()
            Else
                NewData()
            End If
        Else
            errProv.ClearErrors()
            If valProvMain.Validate() Then
                TrimControl(Me) 'To remove spaces from all text boxes

                If RecordStatus = TRecordStatus.isInsert Then
                    'refersh the ID Number from the database if the status is inserting.
                    Dim ID As Integer = GetNewPKey(PrimaryKey, TableName)
                    RecordID = ID
                    'check for Unique fields in insert mode
                    For Each UF As TUniqueField In UniqueFields
                        If FieldValueExisted(UF.FieldName, UF.ctlFieldText.Text, TableName, True, PrimaryKey, UF.ctlPrimaryKey.Text) Then
                            errProv.SetError(UF.ctlFieldText, String.Format("{0} already exists, change it and try again.", UF.FieldName))
                            ActionResult = -1
                            Return ActionResult
                        End If
                    Next
                Else
                    'check for Unique fields in edit mode
                    For Each UF As TUniqueField In UniqueFields
                        If FieldValueExisted(UF.FieldName, UF.ctlFieldText.Text, TableName, False, PrimaryKey, UF.ctlPrimaryKey.Text) Then
                            errProv.SetError(UF.ctlFieldText, String.Format("{0} already exists, change it and try again.", UF.FieldName))
                            ActionResult = -1
                            Return ActionResult
                        End If
                    Next
                End If

                RaiseEvent OnSaveData() ' call the child controls onSaveData event
                If ActionResult = 0 Then
                    RecordStatus = TRecordStatus.isEdit
                    btnDelete.Enabled = True
                    btnRefresh.Enabled = True
                    btnNew.Enabled = True
                    RefershNavigationButtons()
                    RecordChanged = False
                    If EditFormCaption = "" Then
                        Parent.Text = String.Format("Edit {0}", TableName)
                    Else
                        Parent.Text = EditFormCaption
                    End If
                    'refersh the parent form (tab form) if it's visable
                    For Each tabpage As DevExpress.XtraTab.XtraTabPage In MainForm.tabControlMain.TabPages
                        If tabpage.Text = Tag Then
                            CType(tabpage.Controls(0), ctlGridData).btnRefersh_ItemClick(Nothing, Nothing)
                            Exit For
                        End If
                    Next
                End If
            Else
                ActionResult = -1
            End If
        End If

        CurrentAction = TCurrentActions.isNothing
        Return ActionResult
    End Function

    Public Function LoadData() As Integer
        CurrentAction = TCurrentActions.isLoading

        If CheckRecordFound(PrimaryKey, RecordID, TableName) = 1 Then

            ClearControl(Me)
            RecordStatus = TRecordStatus.isEdit
            RaiseEvent OnLoadData() ' call the child controls onLoadData Event
            If ActionResult = 0 Then
                If EditFormCaption = "" Then
                    Parent.Text = String.Format("Edit {0}", TableName)
                Else
                    Parent.Text = EditFormCaption
                End If
                btnDelete.Enabled = True
                btnRefresh.Enabled = True
                btnNew.Enabled = True
                RefershNavigationButtons()
                RecordChanged = False
                valProvMain.Validate()
            End If
        Else
            ShowRecordNotFound()
            ActionResult = -1
            If Not isControl Then
                Parent.Dispose()
            Else
                NewData()
            End If
        End If
        CurrentAction = TCurrentActions.isNothing
        Return ActionResult
    End Function

    Public Function NewData() As Integer
        CurrentAction = TCurrentActions.isNew
        ClearControl(Me)
        errProv.ClearErrors()
        Dim ID As Integer = GetNewPKey(PrimaryKey, TableName)
        RecordID = ID
        RecordStatus = TRecordStatus.isInsert

        RaiseEvent OnNewData() ' call the child controls OnNewData Event
        If ActionResult = 0 Then
            If NewFormCaption = "" Then
                Parent.Text = String.Format("New {0}", TableName)
            Else
                Parent.Text = NewFormCaption
            End If
            btnDelete.Enabled = False
            btnRefresh.Enabled = False
            btnNext.Enabled = False
            btnLast.Enabled = False
            btnFirst.Enabled = False
            btnPrev.Enabled = False
            btnNew.Enabled = False
            RecordChanged = True
        End If
        CurrentAction = TCurrentActions.isNothing
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
        CurrentAction = TCurrentActions.isDeleteing

        ' check if the record is found before deleting it.
        If CheckRecordFound(PrimaryKey, RecordID, TableName) = 1 Then
            If ShowDeleteConfirmation() = DialogResult.Yes Then
                RaiseEvent OnDeleteData()
                If ActionResult = 0 Then
                    'load the last record if there is no recrod then load the last one...
                    Dim ID As Integer = GetLastPKey(PrimaryKey, TableName, NavigationFilter)
                    If ID = 0 Then
                        If Not isControl Then
                            Parent.Dispose()
                        Else
                            NewData()
                        End If
                    Else
                        RecordID = ID
                        LoadData()
                    End If

                    'refersh the parent form (tab form) if it's visable
                    For Each tabpage As DevExpress.XtraTab.XtraTabPage In MainForm.tabControlMain.TabPages
                        If tabpage.Text = Tag Then
                            CType(tabpage.Controls(0), ctlGridData).btnRefersh_ItemClick(Nothing, Nothing)
                            Exit For
                        End If
                    Next
                End If
            End If
        Else
            ShowRecordNotFound()
            ActionResult = -1
            If Not isControl Then
                Parent.Dispose()
            Else
                NewData()
            End If
        End If
        CurrentAction = TCurrentActions.isNothing
    End Sub

    Private Sub btnCancel_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCancel.ItemClick
        If ShowCancelConfirmation() = DialogResult.Yes Then
            If RecordStatus = TRecordStatus.isEdit Then
                LoadData()
            Else
                'load the last record if there is no recrod then load the last one...
                Dim ID As Integer = GetLastPKey(PrimaryKey, TableName, NavigationFilter)
                If ID = 0 Then
                    If Not isControl Then
                        Parent.Dispose()
                    Else
                        NewData()
                    End If
                Else
                    RecordID = ID
                    LoadData()
                End If
            End If
        End If
    End Sub

    Protected Overridable Sub ControlEditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If CurrentAction = TCurrentActions.isNothing Then
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
            End If
            If ctl.HasChildren() And Not (TypeOf ctl Is ctlBaseControl) Then
                AddEditValueChangedEvent(ctl)
            End If
        Next
        'And Not (TypeOf ctl Is ctlBaseControl) : to disable add the event if the base control contains another basecontrol like employee contains IQama control
    End Sub

    Private Sub cntBaseControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddEditValueChangedEvent(Me)
    End Sub

End Class


'if changes has been made to the current Detail View's object then the program will behaves in the following way:
'1- When the Cancel Button Clicked, the following confirmation message Should shown: "Do you want to cancel your changes?"
'2- When the Refresh Button Clicked, the following confirmation message Should shown: "Do you want to save changes?"
'3- When the window with the current Detail View is closed, the following confirmation message Should shown: "Do you want to save changes?"
'4- When the current Detail View's object is replaced with another (e.g. the Next button or Previous button is Clicked), the following confirmation message is shown: "Do you want to save changes?"
'5- When the New Button Clicked, the following confirmation message Should shown: "Do you want to save changes?"
