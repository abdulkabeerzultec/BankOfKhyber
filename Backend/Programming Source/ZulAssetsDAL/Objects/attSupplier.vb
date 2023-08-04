' <fileinfo name="SupplierAttributeBase.cs">
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
Imports System.Data
Imports System.Data.OleDb
Imports System.Text

Namespace ZulAssetsDAL
    '/ <summary>
    '/ It is derived from IAttribute interface.
    '/ It provides the data members with respect to the table Supplier.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="SupplierAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attSupplier
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *suppID* of the table *Supplier*
        '/ </summary>
        Private _suppID As String
        '/ <summary>
        '/ It holds the value of *suppName* of the table *Supplier*
        '/ </summary>
        Private _suppName As String
        '/ <summary>
        '/ It holds the value of *suppCell* of the table *Supplier*
        '/ </summary>
        Private _suppCell As String
        '/ <summary>
        '/ It holds the value of *suppFax* of the table *Supplier*
        '/ </summary>
        Private _suppFax As String
        '/ <summary>
        '/ It holds the value of *suppPhone* of the table *Supplier*
        '/ </summary>
        Private _suppPhone As String
        '/ <summary>
        '/ It holds the value of *suppEmail* of the table *Supplier*
        '/ </summary>
        Private _suppEmail As String
        '/ <summary>
        '/ It holds the value of *isDeleted* of the table *Supplier*
        '/ </summary>
        Private _isDeleted As Boolean

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="SupplierAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _suppID = ""
            _suppName = ""
            _suppCell = ""
            _suppFax = ""
            _suppPhone = ""
            _suppEmail = ""
            _isDeleted = 0
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of IAttribute interface which sets/gets *suppID* of the *Supplier* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return CType(_suppID, String)
            End Get
            Set(ByVal Value As Object)
                _suppID = CType(Value, String)
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>SuppName</c> column value.
        '/ </summary>
        '/ <value>The <c>SuppName</c> column value.</value>
        Public Property SuppName() As String
            Get
                Return _suppName
            End Get
            Set(ByVal Value As String)
                _suppName = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>SuppCell</c> column value.
        '/ </summary>
        '/ <value>The <c>SuppCell</c> column value.</value>
        Public Property SuppCell() As String
            Get
                Return _suppCell
            End Get
            Set(ByVal Value As String)
                _suppCell = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>SuppFax</c> column value.
        '/ </summary>
        '/ <value>The <c>SuppFax</c> column value.</value>
        Public Property SuppFax() As String
            Get
                Return _suppFax
            End Get
            Set(ByVal Value As String)
                _suppFax = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>SuppPhone</c> column value.
        '/ </summary>
        '/ <value>The <c>SuppPhone</c> column value.</value>
        Public Property SuppPhone() As String
            Get
                Return _suppPhone
            End Get
            Set(ByVal Value As String)
                _suppPhone = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>SuppEmail</c> column value.
        '/ </summary>
        '/ <value>The <c>SuppEmail</c> column value.</value>
        Public Property SuppEmail() As String
            Get
                Return _suppEmail
            End Get
            Set(ByVal Value As String)
                _suppEmail = Value
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
    ' End of SupplierAttributeBase class
End Namespace
' End of namespace


