

' <fileinfo name="UsersAttributeBase.cs">
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
    '/ It provides the data members with respect to the table Users.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="UsersAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attDepPolicy
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *loginName* of the table *Users*
        '/ </summary>
        Private _catDepID As String
        '/ <summary>
        '/ It holds the value of *userName* of the table *Users*
        '/ </summary>
        Private _astCatID As String
        '/ <summary>
        '/ It holds the value of *password* of the table *Users*
        '/ </summary>
        Private _depCode As Integer
        '/ <summary>
        '/ It holds the value of *password* of the table *Users*
        '/ </summary>
        Private _salvageValue As Double
        '/ <summary>
        '/ It holds the value of *isDelete* of the table *Users*
        '/ </summary>
        Private _salvageYear As Integer
        '/ <summary>
        '/ It holds the value of *isDelete* of the table *Users*
        '/ </summary>
        Private _salvageMonth As Integer

        Private _salvagePercent As Integer
        Private _IsSalvageValuePercent As Boolean
#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="UsersAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _catDepID = ""
            _astCatID = ""
            _depCode = 0
            _salvageValue = 0
            _salvageYear = 0
            _salvageMonth = 0
            _salvagePercent = 0
            _IsSalvageValuePercent = False
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of NULL Primary Key of the *Users* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return Nothing
            End Get
            Set(ByVal Value As Object)

            End Set
        End Property
        '/ <summary>
        '/ Gets or sets the <c>LoginName</c> column value.
        '/ </summary>
        '/ <value>The <c>LoginName</c> column value.</value>
        Public Property CatDepID() As String
            Get
                Return _catDepID
            End Get
            Set(ByVal Value As String)
                _catDepID = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>UserName</c> column value.
        '/ </summary>
        '/ <value>The <c>UserName</c> column value.</value>
        Public Property AstCatID() As String
            Get
                Return _astCatID
            End Get
            Set(ByVal Value As String)
                _astCatID = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Password</c> column value.
        '/ </summary>
        '/ <value>The <c>Password</c> column value.</value>
        Public Property DepCode() As Integer
            Get
                Return _depCode
            End Get
            Set(ByVal Value As Integer)
                _depCode = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Password</c> column value.
        '/ </summary>
        '/ <value>The <c>Password</c> column value.</value>
        Public Property SalvageValue() As Double
            Get
                Return _salvageValue
            End Get
            Set(ByVal Value As Double)
                _salvageValue = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>IsDelete</c> column value.
        '/ </summary>
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

        Public Property SalvagePercent() As Integer
            Get
                Return _salvagePercent
            End Get
            Set(ByVal Value As Integer)
                _salvagePercent = Value
            End Set
        End Property

        Public Property IsSalvageValuePercent() As Boolean
            Get
                Return _IsSalvageValuePercent
            End Get
            Set(ByVal Value As Boolean)
                _IsSalvageValuePercent = Value
            End Set
        End Property
#End Region

    End Class
    ' End of UsersAttributeBase class
End Namespace
' End of namespace

'----------------------------------------------------------------

'
'----------------------------------------------------------------

