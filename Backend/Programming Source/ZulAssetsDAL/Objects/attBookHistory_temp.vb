' <fileinfo name="BookHistoryAttributeBase.cs">
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
    '/ It provides the "Data Members" with respect to the table BookHistory.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="BookHistoryAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attBookHistory_temp
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *depHistID* of the table *BookHistory*
        '/ </summary>
        Private _depHistID As Long
        '/ <summary>
        '/ It holds the value of *bookID* of the table *BookHistory*
        '/ </summary>
        Private _bookID As String
        '/ <summary>
        '/ It holds the value of *bookID* of the table *BookHistory*
        '/ </summary>
        Private _astid As String
        '/ <summary>
        '/ It holds the value of *depCode* of the table *BookHistory*
        '/ </summary>
        Private _depCode As Integer
        '/ <summary>
        '/ It holds the value of *depValue* of the table *BookHistory*
        Private _depDate As System.DateTime
        '/ </summary>
        Private _depValue As Double
        '/ <summary>
        '/ It holds the value of *accDepValue* of the table *BookHistory*
        '/ </summary>
        Private _accDepValue As Double
        '/ <summary>
        '/ It holds the value of *depDate* of the table *BookHistory*
        '/ </summary>

        '/ <summary>
        '/ It holds the value of *currentBV* of the table *BookHistory*
        '/ </summary>
        Private _currentBV As Double
        '/ <summary>
        '/ It holds the value of *currentBV* of the table *BookHistory*
        '/ </summary>
        Private _lastBookValue As Double
        '/ <summary>
        '/ It holds the value of *isDeleted* of the table *BookHistory*
        '/ </summary>
        Private _isDeleted As Boolean
        '/ <summary>
        '/ It holds the value of *isDelete* of the table *Users*
        '/ </summary>
        Private _salvageYear As Integer
        '/ <summary>
        '/ It holds the value of *isDelete* of the table *Users*
        '/ </summary>
        Private _salvageMonth As Integer

        Private _uname As String
#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="BookHistoryAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _depHistID = 0.0
            _bookID = ""
            _depCode = 0
            _depValue = 0
            _accDepValue = 0
            _depDate = System.DateTime.MinValue
            _currentBV = 0.0
            _lastBookValue = 0.0
            _isDeleted = 0
            _salvageYear = 0
            _salvageMonth = 0
            _uname = ""
        End Sub
#End Region

#Region "Property"

        '/ <value>The <c>IsDelete</c> column value.</value>
        Public Property SalvageYear() As Integer
            Get
                Return _salvageYear
            End Get
            Set(ByVal Value As Integer)
                _salvageYear = Value
            End Set
        End Property
        '/ <summary>
        '/ Gets or sets the <c>IsDelete</c> column value.
        '/ </summary>
        '/ <value>The <c>IsDelete</c> column value.</value>
        Public Property SalvageMonth() As Integer
            Get
                Return _salvageMonth
            End Get
            Set(ByVal Value As Integer)
                _salvageMonth = Value
            End Set
        End Property
        '/ <summary>
        '/ Implements the property of IAttribute interface which sets/gets *depHistID* of the *BookHistory* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return CType(_depHistID, Long)
            End Get
            Set(ByVal Value As Object)
                _depHistID = CType(Value, Long)
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>BookID</c> column value.
        '/ </summary>
        '/ <value>The <c>BookID</c> column value.</value>
        Public Property BookID() As String
            Get
                Return _bookID
            End Get
            Set(ByVal Value As String)
                _bookID = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>BookID</c> column value.
        '/ </summary>
        '/ <value>The <c>BookID</c> column value.</value>
        Public Property ASTID() As String
            Get
                Return _astid
            End Get
            Set(ByVal Value As String)
                _astid = Value
            End Set
        End Property


        '/ <summary>
        '/ Gets or sets the <c>DepCode</c> column value.
        '/ </summary>
        '/ <value>The <c>DepCode</c> column value.</value>
        Public Property DepCode() As Integer
            Get
                Return _depCode
            End Get
            Set(ByVal Value As Integer)
                _depCode = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>DepDate</c> column value.
        '/ </summary>
        '/ <value>The <c>DepDate</c> column value.</value>
        Public Property DepDate() As System.DateTime
            Get
                Return _depDate
            End Get
            Set(ByVal Value As System.DateTime)
                _depDate = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>DepValue</c> column value.
        '/ </summary>
        '/ <value>The <c>DepValue</c> column value.</value>
        Public Property DepValue() As Double
            Get
                Return _depValue
            End Get
            Set(ByVal Value As Double)
                _depValue = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>AccDepValue</c> column value.
        '/ </summary>
        '/ <value>The <c>AccDepValue</c> column value.</value>
        Public Property AccDepValue() As Double
            Get
                Return _accDepValue
            End Get
            Set(ByVal Value As Double)
                _accDepValue = Value
            End Set
        End Property



        '/ <summary>
        '/ Gets or sets the <c>CurrentBV</c> column value.
        '/ </summary>
        '/ <value>The <c>CurrentBV</c> column value.</value>
        Public Property CurrentBV() As Double
            Get
                Return _currentBV
            End Get
            Set(ByVal Value As Double)
                _currentBV = Value
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

        Public Property uname() As String
            Get
                Return _uname
            End Get
            Set(ByVal Value As String)
                _uname = Value
            End Set
        End Property
#End Region

    End Class
    ' End of BookHistoryAttributeBase class
End Namespace
' End of namespace

'----------------------------------------------------------------

'
'----------------------------------------------------------------
