' <fileinfo name="CategoryAttributeBase.cs">
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
    '/ It provides the data members with respect to the table Category.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="CategoryAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attCategory
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *astCatID* of the table *Category*
        '/ </summary>+


        Private _ID1 As Integer
        Private _astCatID As String
        '/ <summary>
        '/ It holds the value of *astCatDesc* of the table *Category*
        '/ </summary>
        Private _astCatDesc As String
        '/ <summary>
        '/ It holds the value of *isDeleted* of the table *Category*
        '/ </summary>
        Private _isDeleted As Boolean
        Private _code As String
        Private _Compcode As String
        Private _catLevel As Integer
        Private _CompleteCatDesc As String

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="CategoryAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _ID1 = 0
            _astCatID = ""
            _astCatDesc = ""
            _isDeleted = 0
            _code = ""
            _Compcode = ""
            _CompleteCatDesc = ""
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of NULL Primary Key of the *Category* table 
        '/ </summary>

        Public Property PKeyCode() As Object
            Get
                Return _ID1
            End Get
            Set(ByVal Value As Object)
                _ID1 = Value
            End Set
        End Property
        '/ <summary>
        '/ Gets or sets the <c>AstCatID</c> column value.
        '/ </summary>
        '/ <value>The <c>AstCatID</c> column value.</value>
        Public Property AstCatID() As String
            Get
                Return _astCatID
            End Get
            Set(ByVal Value As String)
                _astCatID = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>AstCatDesc</c> column value.
        '/ </summary>
        '/ <value>The <c>AstCatDesc</c> column value.</value>
        Public Property AstCatDesc() As String
            Get
                Return _astCatDesc
            End Get
            Set(ByVal Value As String)
                _astCatDesc = Value
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

        Public Property Code() As String
            Get
                Return _code
            End Get
            Set(ByVal Value As String)
                _code = Value
            End Set
        End Property
        Public Property Compcode() As String
            Get
                Return _Compcode
            End Get
            Set(ByVal Value As String)
                _Compcode = Value
            End Set
        End Property

        Public Property catLevel() As String
            Get
                Return _catLevel
            End Get
            Set(ByVal Value As String)
                _catLevel = Value
            End Set
        End Property

        Public Property CompleteCatDesc() As String
            Get
                Return _CompleteCatDesc
            End Get
            Set(ByVal Value As String)
                _CompleteCatDesc = Value
            End Set
        End Property
#End Region

    End Class
    ' End of CategoryAttributeBase class
End Namespace
' End of namespace

'----------------------------------------------------------------

'
'----------------------------------------------------------------

