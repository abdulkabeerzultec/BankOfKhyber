

' <fileinfo name="BrandAttributeBase.cs">
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
    '/ It provides the data members with respect to the table Brand.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="BrandAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attcompany
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *astBrandID* of the table *Brand*
        '/ </summary>
        Private _companyID As Integer
        '/ <summary>
        '/ It holds the value of *astBrandName* of the table *Brand*
        '/ </summary>
        Private _companyCode As String
        '/ <summary>
        '/ It holds the value of *astBrandName* of the table *Brand*
        '/ </summary>
        Private _barStructID As Integer

        Private _companyName As String
        Private _HierCode As String
        '/ <summary>
        '/ It holds the value of *isDeleted* of the table *Brand*
        '/ </summary>
        Private _isDeleted As Boolean

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="BrandAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _companyID = 0
            _companyCode = ""
            _companyName = ""
            _isDeleted = 0

            _barStructID = 0
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of IAttribute interface which sets/gets *astBrandID* of the *Brand* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return CType(_companyID, Integer)
            End Get
            Set(ByVal Value As Object)
                _companyID = CType(Value, Integer)
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>AstBrandName</c> column value.
        '/ </summary>
        '/ <value>The <c>AstBrandName</c> column value.</value>
        Public Property CompanyCode() As String
            Get
                Return _companyCode
            End Get
            Set(ByVal Value As String)
                _companyCode = Value
            End Set
        End Property
        Public Property HierCode() As String
            Get
                Return _HierCode
            End Get
            Set(ByVal Value As String)
                _HierCode = Value
            End Set
        End Property
        Public Property CompanyName() As String
            Get
                Return _companyName
            End Get
            Set(ByVal Value As String)
                _companyName = Value
            End Set
        End Property
        Public Property BarStructID() As Integer
            Get
                Return _barStructID
            End Get
            Set(ByVal Value As Integer)
                _barStructID = Value
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
    ' End of BrandAttributeBase class
End Namespace
' End of namespace


