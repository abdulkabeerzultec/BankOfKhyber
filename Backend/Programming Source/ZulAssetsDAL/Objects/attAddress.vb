Namespace ZulAssetsDAL
    Public Class attAddress
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *astBrandID* of the table *Brand*
        '/ </summary
        Private _addressID As Integer
        '/ <summary>
        '/ It holds the value of *astBrandName* of the table *Brand*
        '/ </summary>
        Private _addressDesc As String
        '/ <summary>
        '/ It holds the value of *isDeleted* of the table *Brand*
        '/ </summary>
        Private _isDeleted As Boolean

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="BrandAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _addressID = 0
            _addressDesc = ""
            _isDeleted = 0
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of IAttribute interface which sets/gets *astBrandID* of the *Brand* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return CType(_addressID, Integer)
            End Get
            Set(ByVal Value As Object)
                _addressID = CType(Value, Integer)
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>AstBrandName</c> column value.
        '/ </summary>
        '/ <value>The <c>AstBrandName</c> column value.</value>
        Public Property AddressDesc() As String
            Get
                Return _addressDesc
            End Get
            Set(ByVal Value As String)
                _addressDesc = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>IsDeleted</c> column value.
        '/ </summary>
        '/ <value>The <c>IsDeleted</c> column value.</value>
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


