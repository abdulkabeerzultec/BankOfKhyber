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
    Public Class attBook
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *bookID* of the table *AstBooks*
        '/ </summary>
        Private _bookID As String
        '/ <summary>
        '/ It holds the value of *depCode* of the table *AstBooks*
        '/ </summary>
        Private _depCode As Integer
        '/ <summary>
        '/ It holds the value of *salvageValue* of the table *AstBooks*
        '/ </summary>
        Private _description As String
        '/ <summary>
        '/ It holds the value of *salvageYear* of the table *AstBooks*
        '/ </summary>
        'Private _salvageYear As Integer
        '/ <summary>
        '/ It holds the value of *isDelete* of the table *AstBooks*
        '/ </summary>
        Private _isDelete As Boolean
        Private _companyID As Integer

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="AstBooksAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _bookID = ""
            _depCode = 0
            _description = ""
            _isDelete = 0
            _companyID = 0
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

        Public Property CompanyID() As Integer
            Get
                Return _companyID
            End Get
            Set(ByVal Value As Integer)
                _companyID = Value
            End Set
        End Property
        '/ <summary>
        '/ Gets or sets the <c>SalvageValue</c> column value.
        '/ </summary>
        '/ <value>The <c>SalvageValue</c> column value.</value>
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal Value As String)
                _description = Value
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


#End Region

    End Class
    ' End of AstBooksAttributeBase class
End Namespace
' End of namespace

'----------------------------------------------------------------

'
'----------------------------------------------------------------


