Namespace ZulAssetsDAL
    Public Class AttAddCostHistory
        Implements IAttribute
#Region "Data Members"
        Private _AddCostID As Integer
        Private _AstID As String
        Private _TypeID As Integer
        Private _AddCost As Double
        Private _ActionDate As DateTime
        Private _LoginName As String
        Private _PrevItemCost As Double
        Private _isDeleted As Boolean

#End Region

#Region "Constructor"
        Public Sub New()
            _AddCostID = 0
            _AstID = ""
            _TypeID = 0
            _AddCost = 0
            _ActionDate = System.DateTime.Now
            _LoginName = ""
            _PrevItemCost = 0
        End Sub
#End Region

#Region "Property"
        Public Property AddCostID() As String
            Get
                Return _AddCostID
            End Get
            Set(ByVal Value As String)
                _AddCostID = Value
            End Set
        End Property
        Public Property AstID() As String
            Get
                Return _AstID
            End Get
            Set(ByVal Value As String)
                _AstID = Value
            End Set
        End Property

        Public Property TypeID() As Integer
            Get
                Return _TypeID
            End Get
            Set(ByVal value As Integer)
                _TypeID = value
            End Set
        End Property
        Public Property AddCost() As Double
            Get
                Return _AddCost
            End Get
            Set(ByVal value As Double)
                _AddCost = value
            End Set
        End Property
        Public Property ActionDate() As DateTime
            Get
                Return _ActionDate
            End Get
            Set(ByVal value As DateTime)
                _ActionDate = value
            End Set
        End Property

        Public Property LoginName() As String
            Get
                Return _LoginName
            End Get
            Set(ByVal value As String)
                _LoginName = value
            End Set
        End Property
        Public Property PrevItemCost() As Double
            Get
                Return _PrevItemCost
            End Get
            Set(ByVal value As Double)
                _PrevItemCost = value
            End Set
        End Property
        Public Property IsDeleted() As Boolean
            Get
                Return _isDeleted
            End Get
            Set(ByVal Value As Boolean)
                _isDeleted = Value
            End Set
        End Property

#End Region

    End Class
End Namespace
