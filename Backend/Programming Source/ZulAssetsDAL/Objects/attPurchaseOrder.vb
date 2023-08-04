' <fileinfo name="PurchaseOrderAttributeBase.cs">
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
    '/ It provides the data members with respect to the table PurchaseOrder.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="PurchaseOrderAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attPurchaseOrder
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *pOCode* of the table *PurchaseOrder*
        '/ </summary>
        Private _pOCode As String
        '/ <summary>
        '/ It holds the value of *suppID* of the table *PurchaseOrder*
        '/ </summary>
        Private _suppID As String
        '/ <summary>
        '/ It holds the value of *date* of the table *PurchaseOrder*
        '/ </summary>
        Private _date As System.DateTime
        '/ <summary>
        '/ It holds the value of *quotation* of the table *PurchaseOrder*
        '/ </summary>
        Private _quotation As String
        '/ <summary>
        '/ It holds the value of *amount* of the table *PurchaseOrder*
        '/ </summary>
        Private _discount As Double

        Private _amount As Double
        '/ <summary>
        '/ It holds the value of *modeDelivery* of the table *PurchaseOrder*
        '/ </summary>
        Private _addcharges As Double
        '/ <summary>
        '/ It holds the value of *addcharges* of the table *PurchaseOrder*
        '/ </summary>
        Private _modeDelivery As String
        '/ <summary>
        '/ It holds the value of *payterm* of the table *PurchaseOrder*
        '/ </summary>
        Private _payterm As String
        '/ <summary>
        '/ It holds the value of *remarks* of the table *PurchaseOrder*
        '/ </summary>
        Private _remarks As String
        '/ <summary>
        '/ It holds the value of *approvedby* of the table *PurchaseOrder*
        '/ </summary>
        Private _approvedby As String
        '/ <summary>
        '/ It holds the value of *preparedby* of the table *PurchaseOrder*
        '/ </summary>
        Private _preparedby As String
        '/ <summary>
        '/ It holds the value of *pOStatus* of the table *PurchaseOrder*
        '/ </summary>
        Private _pOStatus As Integer
        '/ <summary>
        '/ It holds the value of *isDeleted* of the table *PurchaseOrder*
        '/ </summary>
        Private _isDeleted As Boolean
        '/ <summary>
        '/ It holds the value of *isTrans* of the table *PurchaseOrder*
        '/ </summary>
        Private _isTrans As Boolean
        Private _referenceNo As String
        Private _costID As String
        Private _requestedby As String
        Private _termnCon As String


#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="PurchaseOrderAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _discount = 0
            _pOCode = ""
            _suppID = ""
            _date = System.DateTime.MinValue
            _quotation = ""
            _amount = 0
            _addcharges = 0
            _modeDelivery = ""
            _payterm = ""
            _remarks = ""
            _approvedby = ""
            _preparedby = ""
            _pOStatus = 0
            _isDeleted = 0
            _isTrans = 0
            _referenceNo = ""
            _costID = ""
            _requestedby = ""
            _termnCon = ""
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of IAttribute interface which sets/gets *pOCode* of the *PurchaseOrder* table 
        '/ </summary>
        Public Property Discount() As Double
            Get
                Return _discount
            End Get
            Set(ByVal Value As Double)
                _discount = Value
            End Set
        End Property

        Public Property PKeyCode() As Object
            Get
                Return CType(_pOCode, String)
            End Get
            Set(ByVal Value As Object)
                _pOCode = CType(Value, String)
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>SuppID</c> column value.
        '/ </summary>
        '/ <value>The <c>SuppID</c> column value.</value>
        Public Property ReferenceNo() As String
            Get
                Return _referenceNo
            End Get
            Set(ByVal Value As String)
                _referenceNo = Value
            End Set
        End Property
        '****************
        Public Property CostID() As String
            Get
                Return _costID
            End Get
            Set(ByVal Value As String)
                _costID = Value
            End Set
        End Property
        Public Property Requestedby() As String
            Get
                Return _requestedby
            End Get
            Set(ByVal Value As String)
                _requestedby = Value
            End Set
        End Property
        Public Property TermnCon() As String
            Get
                Return _termnCon
            End Get
            Set(ByVal Value As String)
                _termnCon = Value
            End Set
        End Property
        Public Property SuppID() As String
            Get
                Return _suppID
            End Get
            Set(ByVal Value As String)
                _suppID = Value
            End Set
        End Property
        '**************

        '/ <summary>
        '/ Gets or sets the <c>Date</c> column value.
        '/ </summary>
        '/ <value>The <c>Date</c> column value.</value>
        Public Property Date1() As System.DateTime
            Get
                Return _date
            End Get
            Set(ByVal Value As System.DateTime)
                _date = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Quotation</c> column value.
        '/ </summary>
        '/ <value>The <c>Quotation</c> column value.</value>
        Public Property Quotation() As String
            Get
                Return _quotation
            End Get
            Set(ByVal Value As String)
                _quotation = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Amount</c> column value.
        '/ </summary>
        '/ <value>The <c>Amount</c> column value.</value>
        Public Property Amount() As Double
            Get
                Return _amount
            End Get
            Set(ByVal Value As Double)
                _amount = Value
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
        '/ Gets or sets the <c>ModeDelivery</c> column value.
        '/ </summary>
        '/ <value>The <c>ModeDelivery</c> column value.</value>
        Public Property ModeDelivery() As String
            Get
                Return _modeDelivery
            End Get
            Set(ByVal Value As String)
                _modeDelivery = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Payterm</c> column value.
        '/ </summary>
        '/ <value>The <c>Payterm</c> column value.</value>
        Public Property Payterm() As String
            Get
                Return _payterm
            End Get
            Set(ByVal Value As String)
                _payterm = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Remarks</c> column value.
        '/ </summary>
        '/ <value>The <c>Remarks</c> column value.</value>
        Public Property Remarks() As String
            Get
                Return _remarks
            End Get
            Set(ByVal Value As String)
                _remarks = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Approvedby</c> column value.
        '/ </summary>
        '/ <value>The <c>Approvedby</c> column value.</value>
        Public Property Approvedby() As String
            Get
                Return _approvedby
            End Get
            Set(ByVal Value As String)
                _approvedby = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Preparedby</c> column value.
        '/ </summary>
        '/ <value>The <c>Preparedby</c> column value.</value>
        Public Property Preparedby() As String
            Get
                Return _preparedby
            End Get
            Set(ByVal Value As String)
                _preparedby = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>POStatus</c> column value.
        '/ </summary>
        '/ <value>The <c>POStatus</c> column value.</value>
        Public Property POStatus() As Integer
            Get
                Return _pOStatus
            End Get
            Set(ByVal Value As Integer)
                _pOStatus = Value
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

#End Region

    End Class
    ' End of PurchaseOrderAttributeBase class
End Namespace
' End of namespace

