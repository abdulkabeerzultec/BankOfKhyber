Namespace ZulAssetsDAL
    Public Class attAddCostType
        Implements IAttribute
#Region "Data Members"
        Private _TypeID As Integer
        Private _TypeDesc As String
        Private _isDeleted As Boolean
#End Region

#Region "Constructor"
        Public Sub New()
            _TypeID = 0
            _TypeDesc = ""
        End Sub
#End Region

#Region "Property"
        Public Property TypeID() As Integer
            Get
                Return _TypeID
            End Get
            Set(ByVal value As Integer)
                _TypeID = value
            End Set
        End Property

        Public Property TypeDesc() As String
            Get
                Return _TypeDesc
            End Get
            Set(ByVal Value As String)
                _TypeDesc = Value
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