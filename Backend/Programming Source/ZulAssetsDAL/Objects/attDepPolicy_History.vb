' <fileinfo name="AstBooksAttributeBase.cs">
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
    '/ It provides the data members with respect to the table AstBooks.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="AstBooksAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attDepPolicy_History
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *bookID* of the table *AstBooks*
        '/ </summary>
        Private _bookID As String
        '/ <summary>
        '/ It holds the value of *bookDescription* of the table *AstBooks*
        '/ </summary>
        Private _astID As String
        '/ <summary>
        '/ It holds the value of *bookDescription* of the table *AstBooks*
        '/ </summary>
        Private _bookDescription As String
        '/ <summary>
        '/ It holds the value of *depCode* of the table *AstBooks*
        '/ </summary>
        Private _depCode As Integer
        '/ <summary>
        '/ It holds the value of *salvageValue* of the table *AstBooks*
        '/ </summary>
        Private _salvageValue As Double
        '/ <summary>
        '/ It holds the value of *salvageYear* of the table *AstBooks*
        '/ </summary>
        Private _salvageYear As Double
        '/ <summary>
        '/ It holds the value of *currentBookValue* of the table *AstBooks*
        '/ </summary>
        Private _currentBookValue As Double
        '/ <summary>
        '/ It holds the value of *lastBookValue* of the table *AstBooks*
        '/ </summary>
        Private _lastBookValue As Double
        '/ <summary>
        '/ It holds the value of *bVUpdate* of the table *AstBooks*
        '/ </summary>
        Private _bVUpdate As System.DateTime
        '/ <summary>
        '/ It holds the value of *isDelete* of the table *AstBooks*
        '/ </summary>
        Private _isDelete As Boolean
        '/ <summary>
        '/ It holds the value of *isDelete* of the table *Users*
        '/ </summary>
        Private _salvageMonth As Integer
#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="AstBooksAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _bookID = ""
            _astID = ""
            _bookDescription = ""
            _depCode = 0
            _salvageValue = 0
            _salvageYear = 0
            _lastBookValue = 0.0
            _currentBookValue = 0
            _bVUpdate = System.DateTime.MinValue
            _isDelete = 0
            _salvageMonth = 0
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of IAttribute interface which sets/gets *bookID* of the *AstBooks* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return CType(_bookID, String)
            End Get
            Set(ByVal Value As Object)
                _bookID = CType(Value, String)
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>BookDescription</c> column value.
        '/ </summary>
        '/ <value>The <c>BookDescription</c> column value.</value>
        Public Property BookDescription() As String
            Get
                Return _bookDescription
            End Get
            Set(ByVal Value As String)
                _bookDescription = Value
            End Set
        End Property

        '/ <summary>_astID
        '/ Gets or sets the <c>BookDescription</c> column value.
        '/ </summary>
        '/ <value>The <c>BookDescription</c> column value.</value>
        Public Property AstID() As String
            Get
                Return _astID
            End Get
            Set(ByVal Value As String)
                _astID = Value
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
        '/ Gets or sets the <c>SalvageValue</c> column value.
        '/ </summary>
        '/ <value>The <c>SalvageValue</c> column value.</value>
        Public Property SalvageValue() As Double
            Get
                Return _salvageValue
            End Get
            Set(ByVal Value As Double)
                _salvageValue = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>SalvageYear</c> column value.
        '/ </summary>
        '/ <value>The <c>SalvageYear</c> column value.</value>
        Public Property SalvageYear() As Double
            Get
                Return _salvageYear
            End Get
            Set(ByVal Value As Double)
                _salvageYear = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>CurrentBookValue</c> column value.
        '/ </summary>
        '/ <value>The <c>CurrentBookValue</c> column value.</value>
        Public Property CurrentBookValue() As Double
            Get
                Return _currentBookValue
            End Get
            Set(ByVal Value As Double)
                _currentBookValue = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>BVUpdate</c> column value.
        '/ </summary>
        '/ <value>The <c>BVUpdate</c> column value.</value>
        Public Property BVUpdate() As System.DateTime
            Get
                Return _bVUpdate
            End Get
            Set(ByVal Value As System.DateTime)
                _bVUpdate = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>IsDelete</c> column value.
        '/ </summary>
        '/ <value>The <c>IsDelete</c> column value.</value>
        Public Property IsDelete() As Boolean
            Get
                Return _isDelete
            End Get
            Set(ByVal Value As Boolean)
                _isDelete = Value
            End Set
        End Property
        '/ <summary>
        '/ Gets or sets the <c>CurrentBV</c> column value.
        '/ </summary>
        '/ <value>The <c>CurrentBV</c> column value.</value>
        Public Property LastBookValue() As Long
            Get
                Return _lastBookValue
            End Get
            Set(ByVal Value As Long)
                _lastBookValue = Value
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
#End Region

    End Class
    ' End of AstBooksAttributeBase class
End Namespace
' End of namespace

'----------------------------------------------------------------

'
'----------------------------------------------------------------

