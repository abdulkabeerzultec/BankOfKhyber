Namespace ZulAssetsDAL
    Public Class attAssetWarranty
        Implements IAttribute
#Region "Data Members"


        Private _ID As Int64
        Private _AstID As String
        Private _WarrantyStart As Date
        Private _WarrantyPeriodMonth As Integer
        Private _AlarmBeforeDays As Integer
        Private _AlarmActivated As Boolean
#End Region

#Region "Constructor"
        Public Sub New()
            _ID = 0
            _AstID = ""
            _WarrantyStart = Now
            _WarrantyPeriodMonth = 0
            _AlarmBeforeDays = 0
            _AlarmActivated = False
        End Sub
#End Region

#Region "Property"
        Public Property ID() As Int64
            Get
                Return _ID
            End Get
            Set(ByVal value As Int64)
                _ID = value
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


        Public Property WarrantyStart() As Date
            Get
                Return _WarrantyStart
            End Get
            Set(ByVal Value As Date)
                _WarrantyStart = Value
            End Set
        End Property

        Public Property WarrantyPeriodMonth() As Integer
            Get
                Return _WarrantyPeriodMonth
            End Get
            Set(ByVal value As Integer)
                _WarrantyPeriodMonth = value
            End Set
        End Property

        Public Property AlarmBeforeDays() As Integer
            Get
                Return _AlarmBeforeDays
            End Get
            Set(ByVal value As Integer)
                _AlarmBeforeDays = value
            End Set
        End Property

        Public Property AlarmActivated() As Boolean
            Get
                Return _AlarmActivated
            End Get
            Set(ByVal Value As Boolean)
                _AlarmActivated = Value
            End Set
        End Property


#End Region
    End Class

End Namespace