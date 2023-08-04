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
    Public Class attBrand
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *astBrandID* of the table *Brand*
        '/ </summary>
        Private _astBrandID As String
        '/ <summary>
        '/ It holds the value of *astBrandName* of the table *Brand*
        '/ </summary>
        Private _astBrandName As String
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
            _astBrandID = ""
            _astBrandName = ""
            _isDeleted = 0
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of IAttribute interface which sets/gets *astBrandID* of the *Brand* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return CType(_astBrandID, String)
            End Get
            Set(ByVal Value As Object)
                _astBrandID = CType(Value, String)
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>AstBrandName</c> column value.
        '/ </summary>
        '/ <value>The <c>AstBrandName</c> column value.</value>
        Public Property AstBrandName() As String
            Get
                Return _astBrandName
            End Get
            Set(ByVal Value As String)
                _astBrandName = Value
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
