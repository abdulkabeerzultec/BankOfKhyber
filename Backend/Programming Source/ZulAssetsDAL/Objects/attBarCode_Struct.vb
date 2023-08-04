
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
    Public Class attBarCode_Struct
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *bookID* of the table *AstBooks*
        '/ </summary>

        Private _barStructID As Integer

        Private _barStructDesc As String

        Private _barStructLength As Integer

        Private _barStructPreFix As String

        Private _valueSep As String

        Private _barCode As String

        Private _isDeleted As Boolean

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="AstBooksAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _barStructID = 0
            _barStructDesc = ""
            _barStructLength = 0
            _barStructPreFix = ""
            _valueSep = ""
            _barCode = ""
            _isDeleted = False
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of IAttribute interface which sets/gets *bookID* of the *AstBooks* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return CType(_barStructID, Integer)
            End Get
            Set(ByVal Value As Object)
                If Trim(Value) = "" Then
                    _barStructID = 0
                Else
                    _barStructID = CType(Value, Integer)
                End If

            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>BookDescription</c> column value.
        '/ </summary>
        '/ <value>The <c>BookDescription</c> column value.</value>
        Public Property BarStructDesc() As String
            Get
                Return _barStructDesc
            End Get
            Set(ByVal Value As String)
                _barStructDesc = Value
            End Set
        End Property

        '/ <summary>_astID
        '/ Gets or sets the <c>BookDescription</c> column value.
        '/ </summary>
        '/ <value>The <c>BookDescription</c> column value.</value>
        Public Property BarStructLength() As Integer
            Get
                Return _barStructLength
            End Get
            Set(ByVal Value As Integer)
                _barStructLength = Value
            End Set
        End Property
        '/ <summary>
        '/ Gets or sets the <c>DepCode</c> column value.
        '/ </summary>
        '/ <value>The <c>DepCode</c> column value.</value>
        Public Property BarStructPreFix() As String
            Get
                Return _barStructPreFix
            End Get
            Set(ByVal Value As String)
                _barStructPreFix = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>SalvageValue</c> column value.
        '/ </summary>
        '/ <value>The <c>SalvageValue</c> column value.</value>
        Public Property ValueSep() As String
            Get
                Return _valueSep
            End Get
            Set(ByVal Value As String)
                _valueSep = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>SalvageYear</c> column value.
        '/ </summary>
        '/ <value>The <c>SalvageYear</c> column value.</value>
        Public Property BarCode() As String
            Get
                Return _barCode
            End Get
            Set(ByVal Value As String)
                _barCode = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>IsDelete</c> column value.
        '/ </summary>
        '/ <value>The <c>IsDelete</c> column value.</value>
        Public Property IsDelete() As Boolean
            Get
                Return _isDeleted
            End Get
            Set(ByVal Value As Boolean)
                _isDeleted = Value
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


