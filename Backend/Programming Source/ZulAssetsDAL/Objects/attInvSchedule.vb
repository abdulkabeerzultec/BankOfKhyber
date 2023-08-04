Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text

Namespace ZulAssetsDAL

    Public Class attInvSchedule
        Implements IAttribute


#Region "Data Members"
        Private _invSchCode As Long
        Private _invDesc As String
        Private _invStartDate As System.DateTime
        Private _invEndDate As System.DateTime
        Private _isDeleted As Boolean
        Private _InventoryType As Integer
        Private _Isclosed As Integer
#End Region

        Public Sub New()
            _invSchCode = 0
            _invDesc = ""
            _invStartDate = System.DateTime.MinValue
            _invEndDate = System.DateTime.MinValue
            _isDeleted = 0
            _InventoryType = 0
            _Isclosed = 0
        End Sub

        Public Property PKeyCode() As Object
            Get
                Return CType(_invSchCode, Long)
            End Get
            Set(ByVal Value As Object)
                _invSchCode = CType(Value, Long)
            End Set
        End Property

        Public Property InvDesc() As String
            Get
                Return _invDesc
            End Get
            Set(ByVal Value As String)
                _invDesc = Value
            End Set
        End Property

        Public Property InvStartDate() As System.DateTime
            Get
                Return _invStartDate
            End Get
            Set(ByVal Value As System.DateTime)
                _invStartDate = Value
            End Set
        End Property

        Public Property InvEndDate() As System.DateTime
            Get
                Return _invEndDate
            End Get
            Set(ByVal Value As System.DateTime)
                _invEndDate = Value
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

        Public Property InventoryType() As Integer
            Get
                Return _InventoryType
            End Get
            Set(ByVal value As Integer)
                _InventoryType = value
            End Set
        End Property

        Public Property Isclosed() As Integer
            Get
                Return _Isclosed
            End Get
            Set(ByVal value As Integer)
                _Isclosed = value
            End Set
        End Property
    End Class
End Namespace

