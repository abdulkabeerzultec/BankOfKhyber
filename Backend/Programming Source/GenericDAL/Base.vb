Public Class Base
#Region "Data Types"
    Enum TActionType
        Add
        Edit
        Delete
        View
        Print
    End Enum

    Enum TRecordStates
        NewRecord = 1
        ModifiedRecord = 0
    End Enum

    Public Structure TChildTables
        Public Sub New(ByVal RelatedFieldName As String, ByVal RelatedTableName As String)
            FieldName = RelatedFieldName
            TableName = RelatedTableName
        End Sub

        Public TableName As String
        Public FieldName As String
    End Structure
#End Region

    Private Shared _LoginName As String
    Private Shared _LoginPass As String
    Private Shared _UserGUID As Guid

    Private Shared _IsForeignLanguage As Boolean = False
    Private _ListDisplayMember As String
    Private _ListValueMember As String
    Private _TableName As String
    Private _PrimaryKey As String = ""
    Private _OrderField As String = ""
    Private _ListDataTableType As Type

    Public Property ListDataTableType() As Type
        Get
            Return _ListDataTableType
        End Get
        Set(ByVal value As Type)
            _ListDataTableType = value
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

    Public Property ListValueMember() As String
        Get
            Return _ListValueMember
        End Get
        Set(ByVal value As String)
            _ListValueMember = value
        End Set
    End Property

    Public Property ListDisplayMember() As String
        Get
            Return _ListDisplayMember
        End Get
        Set(ByVal value As String)
            _ListDisplayMember = value
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

    Public Shared Property IsForeignLanguage() As Boolean
        Get
            Return _IsForeignLanguage
        End Get
        Set(ByVal value As Boolean)
            _IsForeignLanguage = value
        End Set
    End Property

    Public Shared Property LoginName() As String
        Get
            Return _LoginName
        End Get
        Set(ByVal value As String)
            _LoginName = value
        End Set
    End Property

    Protected Shared Property LoginPass() As String
        Get
            Return _LoginPass
        End Get
        Set(ByVal value As String)
            _LoginPass = value
        End Set
    End Property

    Public Shared Property UserGUID() As Guid
        Get
            Return _UserGUID
        End Get
        Set(ByVal value As Guid)
            _UserGUID = value
        End Set
    End Property

    Protected ReadOnly Property BusinessLayerName() As String
        Get
            Return Me.GetType.Name
        End Get
    End Property

    Protected _RecordState As TRecordStates
    Private _ChildTables As New Generic.List(Of TChildTables)

    Public ReadOnly Property ChildTables() As Generic.List(Of TChildTables)
        Get
            Return _ChildTables
        End Get
    End Property

    Protected Function CheckChildCount(ByVal rowGUID As Guid) As Integer
        Dim Count As Integer = 0
        For Each CT As TChildTables In _ChildTables
            Dim Query As String = String.Format("SELECT COUNT(*) FROM {0} where {1} = '" & rowGUID.ToString & "'", CT.TableName, CT.FieldName)
            Dim result As String = DBOperations.Executer_Scalar(Query)
            If Integer.TryParse(result, 0) Then
                Count += result
            End If
        Next
        Return Count
    End Function

    Protected Function CheckChildCountByID(ByVal rowID As String) As Integer
        Dim Count As Integer = 0
        For Each CT As TChildTables In _ChildTables
            Dim Query As String = String.Format("SELECT COUNT(*) FROM {0} where {1} = '" & rowID & "'", CT.TableName, CT.FieldName)
            Dim result As String = DBOperations.Executer_Scalar(Query)
            If Integer.TryParse(result, 0) Then
                Count += result
            End If
        Next
        Return Count
    End Function

    Protected Function GetAdapter(ByVal tableAdapter As Object) As SqlClient.SqlDataAdapter
        Dim tableAdapterType As Type = tableAdapter.GetType()
        Dim adapter As SqlClient.SqlDataAdapter = CType(tableAdapterType.GetProperty("Adapter", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(tableAdapter, Nothing), SqlClient.SqlDataAdapter)
        Return adapter
    End Function

End Class
