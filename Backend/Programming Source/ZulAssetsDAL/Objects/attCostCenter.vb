Namespace ZulAssetsDAL

    Public Class attCostCenter
        Implements IAttribute


#Region "Data Members"
        Private _costID As String
        Private _costNumber As String
        Private _costName As String
        Private _isDeleted As Boolean
        Private _CompanyID As Integer

#End Region

#Region "Constructor"
        Public Sub New()
            _costID = ""
            _costNumber = ""
            _costName = ""
            _isDeleted = 0
            CompanyID = 0

        End Sub
#End Region

#Region "Property"

        Public Property PKeyCode() As Object
            Get
                Return CType(_costID, String)
            End Get
            Set(ByVal Value As Object)
                _costID = CType(Value, String)
            End Set
        End Property

        Public Property CostNumber() As String
            Get
                Return _costNumber
            End Get
            Set(ByVal Value As String)
                _costNumber = Value
            End Set
        End Property

        Public Property CostName() As String
            Get
                Return _costName
            End Get
            Set(ByVal Value As String)
                _costName = Value
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

        Public Property CompanyID() As Integer
            Get
                Return _CompanyID
            End Get
            Set(ByVal Value As Integer)
                _CompanyID = Value
            End Set
        End Property
#End Region

    End Class
End Namespace

