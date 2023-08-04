' <fileinfo name="Ast_SoldAttributeBase.cs">
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
    '/ It provides the "Data Members" with respect to the table Ast_Sold.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="Ast_SoldAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attAst_Sold
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *astSoldID* of the table *Ast_Sold*
        '/ </summary>
        Private _astSoldID As String
        '/ <summary>
        '/ It holds the value of *astID* of the table *Ast_Sold*
        '/ </summary>
        Private _astID As String
        '/ <summary>
        '/ It holds the value of *sel_Date* of the table *Ast_Sold*
        '/ </summary>
        Private _sel_Date As System.DateTime
        '/ <summary>
        '/ It holds the value of *sel_Price* of the table *Ast_Sold*
        '/ </summary>
        Private _sel_Price As Double
        '/ <summary>
        '/ It holds the value of *bookValue* of the table *Ast_Sold*
        '/ </summary>

        Private _soldTo As String
        '/ <summary>
        '/ It holds the value of *isDeleted* of the table *Ast_Sold*
        '/ </summary>
        Private _bookValue As Double
        '/ <summary>
        '/ It holds the value of *soldTo* of the table *Ast_Sold*
        '/ </summary>
        Private _isDeleted As Boolean

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="Ast_SoldAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _astSoldID = ""
            _astID = ""
            _sel_Date = System.DateTime.MinValue
            _sel_Price = 0
            _bookValue = 0
            _soldTo = ""
            _isDeleted = 0
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of IAttribute interface which sets/gets *astSoldID* of the *Ast_Sold* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return CType(_astSoldID, String)
            End Get
            Set(ByVal Value As Object)
                _astSoldID = CType(Value, String)
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>AstID</c> column value.
        '/ </summary>
        '/ <value>The <c>AstID</c> column value.</value>
        Public Property AstID() As String
            Get
                Return _astID
            End Get
            Set(ByVal Value As String)
                _astID = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Sel_Date</c> column value.
        '/ </summary>
        '/ <value>The <c>Sel_Date</c> column value.</value>
        Public Property Sel_Date() As System.DateTime
            Get
                Return _sel_Date
            End Get
            Set(ByVal Value As System.DateTime)
                _sel_Date = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Sel_Price</c> column value.
        '/ </summary>
        '/ <value>The <c>Sel_Price</c> column value.</value>
        Public Property Sel_Price() As Double
            Get
                Return _sel_Price
            End Get
            Set(ByVal Value As Double)
                _sel_Price = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>BookValue</c> column value.
        '/ </summary>
        '/ <value>The <c>BookValue</c> column value.</value>
        Public Property BookValue() As Double
            Get
                Return _bookValue
            End Get
            Set(ByVal Value As Double)
                _bookValue = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>SoldTo</c> column value.
        '/ </summary>
        '/ <value>The <c>SoldTo</c> column value.</value>
        Public Property SoldTo() As String
            Get
                Return _soldTo
            End Get
            Set(ByVal Value As String)
                _soldTo = Value
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

#End Region

    End Class
    ' End of Ast_SoldAttributeBase class
End Namespace
' End of namespace

'----------------------------------------------------------------

'
'----------------------------------------------------------------

