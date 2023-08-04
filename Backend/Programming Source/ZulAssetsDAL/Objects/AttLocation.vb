Imports System

Namespace ZulAssetsDAL

    Public Class attLocation

        Implements IAttribute

#Region "Data Members"
        Private _ID1 As Integer
        Private _hierCode As String
        Private _Description As String
        Private _code As String
        Private _Compcode As String
        Private _locLevel As Integer
        Private _CompleteLocationDesc As String
        Private _CompanyID As Integer
#End Region

#Region "Constructor"

        Public Sub New()
            _ID1 = 0
            _hierCode = ""
            _Description = ""
            _code = ""
            _Compcode = ""
            _CompleteLocationDesc = ""
            _CompanyID = 0
        End Sub
#End Region

#Region "Property"

        Public Property PKeyCode() As Object
            Get
                Return _ID1
            End Get
            Set(ByVal Value As Object)
                _ID1 = Value
            End Set
        End Property

        Public Property HierCode() As String
            Get
                Return _hierCode
            End Get
            Set(ByVal Value As String)
                _hierCode = Value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal Value As String)
                _Description = Value
            End Set
        End Property
        Public Property Code() As String
            Get
                Return _code
            End Get
            Set(ByVal Value As String)
                _code = Value
            End Set
        End Property
        Public Property Compcode() As String
            Get
                Return _Compcode
            End Get
            Set(ByVal Value As String)
                _Compcode = Value
            End Set
        End Property

        Public Property CompleteLocationDesc() As String
            Get
                Return _CompleteLocationDesc
            End Get
            Set(ByVal Value As String)
                _CompleteLocationDesc = Value
            End Set
        End Property

        Public Property locLevel() As String
            Get
                Return _locLevel
            End Get
            Set(ByVal Value As String)
                _locLevel = Value
            End Set
        End Property

        Public Property CompanyID() As Integer
            Get
                Return _CompanyID
            End Get
            Set(ByVal value As Integer)
                _CompanyID = value
            End Set
        End Property

#End Region

    End Class
End Namespace



