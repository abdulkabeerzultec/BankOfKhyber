' <fileinfo name="PODetailsAttributeBase.cs">
'		<copyright>
'			All rights reserved.
'		</copyright>
'		<remarks>
'			Do not change this source code manually. Changes to this file may 
'			cause incorrect behavior and will be lost if the code is regenerated.
'		</remarks>
'		<generator rewritefile="True" info="Contact IT Department - Head Office - Jeddah."</generator>
' </fileinfo>

Imports System

Namespace ZulAssetsDAL
    '/ <summary>
    '/ It is derived from IAttribute interface.
    '/ It provides the data members with respect to the table PODetails.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="PODetailsAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attPODetails
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *pOItmID* of the table *PODetails*
        '/ </summary>
        Private _pOItmID As String
        '/ <summary>
        '/ It holds the value of *pOCode* of the table *PODetails*
        '/ </summary>
        Private _pOCode As String
        '/ <summary>
        '/ It holds the value of *itemCode* of the table *PODetails*
        '/ </summary>

        Private _itemCode As String
        '/ <summary>
        '/ It holds the value of *modeDelivery* of the table *PurchaseOrder*
        '/ </summary>
        Private _addcharges As Double
        '/ <summary>
        '/ It holds the value of *pOItmDesc* of the table *PODetails*
        '/ </summary>
        Private _pOItmDesc As String

        Private _unit As Integer
        '/ <summary>
        '/ It holds the value of *pOItmBaseCost* of the table *PODetails*
        '/ </summary>
        Private _pOItmBaseCost As Double
        '/ <summary>
        '/ It holds the value of *pOItmQty* of the table *PODetails*
        '/ </summary>
        Private _pOItmQty As Long
        '/ <summary>
        '/ It holds the value of *isDeleted* of the table *PODetails*
        '/ </summary>
        Private _isDeleted As Boolean
        '/ <summary>
        '/ It holds the value of *isTrans* of the table *PurchaseOrder*
        '/ </summary>
        Private _isTrans As Boolean

        Private _poRecQty As Long

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="PODetailsAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _pOItmID = ""
            _pOCode = ""
            _itemCode = 0
            _pOItmDesc = ""
            _pOItmBaseCost = 0
            _addcharges = 0
            _pOItmQty = 0
            _poRecQty = 0
            _isDeleted = 0
            _isTrans = 0
            _unit = 0
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of IAttribute interface which sets/gets *pOItmID* of the *PODetails* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return CType(_pOItmID, String)
            End Get
            Set(ByVal Value As Object)
                _pOItmID = CType(Value, String)
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>POCode</c> column value.
        '/ </summary>
        '/ <value>The <c>POCode</c> column value.</value>
        Public Property POCode() As String
            Get
                Return _pOCode
            End Get
            Set(ByVal Value As String)
                _pOCode = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>ItemCode</c> column value.
        '/ </summary>
        '/ <value>The <c>ItemCode</c> column value.</value>
        Public Property ItemCode() As String
            Get
                Return _itemCode
            End Get
            Set(ByVal Value As String)
                _itemCode = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>POItmDesc</c> column value.
        '/ </summary>
        '/ <value>The <c>POItmDesc</c> column value.</value>
        Public Property unit() As Integer
            Get
                Return _unit
            End Get
            Set(ByVal Value As Integer)
                _unit = Value
            End Set
        End Property

        Public Property POItmDesc() As String
            Get
                Return _pOItmDesc
            End Get
            Set(ByVal Value As String)
                _pOItmDesc = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>POItmBaseCost</c> column value.
        '/ </summary>
        '/ <value>The <c>POItmBaseCost</c> column value.</value>
        Public Property POItmBaseCost() As Double
            Get
                Return _pOItmBaseCost
            End Get
            Set(ByVal Value As Double)
                _pOItmBaseCost = Value
            End Set
        End Property
        '/ <summary>
        '/ Gets or sets the <c>Amount</c> column value.
        '/ </summary>
        '/ <value>The <c>Amount</c> column value.</value>
        Public Property AddCharges() As Double
            Get
                Return _addcharges
            End Get
            Set(ByVal Value As Double)
                _addcharges = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>POItmQty</c> column value.
        '/ </summary>
        '/ <value>The <c>POItmQty</c> column value.</value>
        Public Property POItmQty() As Long
            Get
                Return _pOItmQty
            End Get
            Set(ByVal Value As Long)
                _pOItmQty = Value
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
        '/ <summary>
        '/ Gets or sets the <c>IsTrans</c> column value.
        '/ </summary>
        '/ <value>The <c>IsTrans</c> column value.</value>
        Public Property IsTrans() As Boolean
            Get
                Return _isTrans
            End Get
            Set(ByVal Value As Boolean)
                _isTrans = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>POItmQty</c> column value.
        '/ </summary>
        '/ <value>The <c>POItmQty</c> column value.</value>
        Public Property PORecQty() As Long
            Get
                Return _poRecQty
            End Get
            Set(ByVal Value As Long)
                _poRecQty = Value
            End Set
        End Property
#End Region

    End Class
    ' End of PODetailsAttributeBase class
End Namespace
' End of namespace

'----------------------------------------------------------------

'
'----------------------------------------------------------------
