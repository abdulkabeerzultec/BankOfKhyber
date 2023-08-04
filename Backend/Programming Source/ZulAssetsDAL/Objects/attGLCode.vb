Imports System

Namespace ZulAssetsDAL
    Public Class attGLCode
        Implements IAttribute

#Region "Data Members"
        Private _GLCode As Long
        Private _GLDesc As String
        Private _CompanyId As Integer
        Private _isDeleted As Boolean

#End Region

#Region "Constructor"

        Public Sub New()
            _GLCode = 0
            _GLDesc = ""
            _CompanyId = 0
            _isDeleted = 0
        End Sub
#End Region

#Region "Property"

        Public Property LedgerCode() As Object
            Get
                Return _GLCode
            End Get
            Set(ByVal Value As Object)
                _GLCode = Value
            End Set
        End Property

        Public Property PKeyCode() As Object
            Get
                Return _GLCode
            End Get
            Set(ByVal Value As Object)
                _GLCode = Value
            End Set
        End Property

        Public Property CompanyId() As Object
            Get
                Return _CompanyId
            End Get
            Set(ByVal Value As Object)
                _CompanyId = Value
            End Set
        End Property

        Public Property GLDesc() As String
            Get
                Return _GLDesc
            End Get
            Set(ByVal Value As String)
                _GLDesc = Value
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



