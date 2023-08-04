
Imports System
Namespace ZulAssetsDAL
    Public Class attOrgHier
        Implements IAttribute

#Region "Data Members"

        Private _hierCode As String

        Private _grpID As String

        Private _isWareHouse As Integer

#End Region

#Region "Constructor"

        Public Sub New()
            _isWareHouse = 0
            _grpID = ""
            _hierCode = ""
        End Sub
#End Region

#Region "Property"

        Public Property PKeyCode() As Object
            Get
                Return _hierCode
            End Get
            Set(ByVal Value As Object)
                _hierCode = Value
            End Set
        End Property

        Public Property GrpID() As String
            Get
                Return _grpID
            End Get
            Set(ByVal Value As String)
                _grpID = Value
            End Set
        End Property

        Public Property IsWareHouse() As Integer
            Get
                Return _isWareHouse
            End Get
            Set(ByVal Value As Integer)
                _isWareHouse = Value
            End Set
        End Property

#End Region

    End Class
End Namespace



