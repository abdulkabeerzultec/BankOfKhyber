
' <fileinfo name="CompanyInfoAttributeBase.cs">
'		<copyright>
'			All rights reserved.
'		</copyright>
'		<remarks>
'			Do not change this source code manually. Changes to this file may 
'			cause incorrect behavior and will be lost if the code is regenerated.
'		</remarks>
'		<generator rewritefile="True" info="Contact SR Team."</generator>
' </fileinfo>

Imports System

Namespace ZulAssetsDAL
    '/ <summary>
    '/ It is derived from IAttribute interface.
    '/ It provides the data members with respect to the table CompanyInfo.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="CompanyInfoAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attCompanyInfo
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *id* of the table *CompanyInfo*
        '/ </summary>
        Private _id As Int16
        '/ <summary>
        '/ It holds the value of *name* of the table *CompanyInfo*
        '/ </summary>
        Private _name As String
        '/ <summary>
        '/ It holds the value of *address* of the table *CompanyInfo*
        '/ </summary>
        Private _address As String
        '/ <summary>
        '/ It holds the value of *state* of the table *CompanyInfo*
        '/ </summary>
        Private _state As String
        '/ <summary>
        '/ It holds the value of *pCode* of the table *CompanyInfo*
        '/ </summary>
        Private _pCode As String
        '/ <summary>
        '/ It holds the value of *city* of the table *CompanyInfo*
        '/ </summary>
        Private _city As String
        '/ <summary>
        '/ It holds the value of *country* of the table *CompanyInfo*
        '/ </summary>
        Private _counTry As String
        '/ <summary>
        '/ It holds the value of *phone* of the table *CompanyInfo*
        '/ </summary>
        Private _phone As String
        '/ <summary>
        '/ It holds the value of *fax* of the table *CompanyInfo*
        '/ </summary>
        Private _fax As String
        '/ <summary>
        '/ It holds the value of *email* of the table *CompanyInfo*
        '/ </summary>
        Private _email As String
        '/ <summary>
        '/ It holds the value of *image* of the table *CompanyInfo*
        '/ </summary>
        Private _image As Byte()

        Private _IsNewImage As Boolean

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="CompanyInfoAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _id = 0
            _name = ""
            _address = ""
            _state = ""
            _pCode = ""
            _city = ""
            _counTry = ""
            _phone = ""
            _fax = ""
            _email = ""
            _IsNewImage = False
            '_image = ""
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of NULL Primary Key of the *CompanyInfo* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return Nothing
            End Get
            Set(ByVal Value As Object)

            End Set
        End Property
        '/ <summary>
        '/ Gets or sets the <c>ID</c> column value.
        '/ </summary>
        '/ <value>The <c>ID</c> column value.</value>
        Public Property ID() As Int16
            Get
                Return _id
            End Get
            Set(ByVal Value As Int16)
                _id = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Name</c> column value.
        '/ </summary>
        '/ <value>The <c>Name</c> column value.</value>
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal Value As String)
                _name = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Address</c> column value.
        '/ </summary>
        '/ <value>The <c>Address</c> column value.</value>
        Public Property Address() As String
            Get
                Return _address
            End Get
            Set(ByVal Value As String)
                _address = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>State</c> column value.
        '/ </summary>
        '/ <value>The <c>State</c> column value.</value>
        Public Property State() As String
            Get
                Return _state
            End Get
            Set(ByVal Value As String)
                _state = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>PCode</c> column value.
        '/ </summary>
        '/ <value>The <c>PCode</c> column value.</value>
        Public Property PCode() As String
            Get
                Return _pCode
            End Get
            Set(ByVal Value As String)
                _pCode = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>City</c> column value.
        '/ </summary>
        '/ <value>The <c>City</c> column value.</value>
        Public Property City() As String
            Get
                Return _city
            End Get
            Set(ByVal Value As String)
                _city = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Country</c> column value.
        '/ </summary>
        '/ <value>The <c>Country</c> column value.</value>
        Public Property CounTry() As String
            Get
                Return _counTry
            End Get
            Set(ByVal Value As String)
                _counTry = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Phone</c> column value.
        '/ </summary>
        '/ <value>The <c>Phone</c> column value.</value>
        Public Property Phone() As String
            Get
                Return _phone
            End Get
            Set(ByVal Value As String)
                _phone = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Fax</c> column value.
        '/ </summary>
        '/ <value>The <c>Fax</c> column value.</value>
        Public Property Fax() As String
            Get
                Return _fax
            End Get
            Set(ByVal Value As String)
                _fax = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Email</c> column value.
        '/ </summary>
        '/ <value>The <c>Email</c> column value.</value>
        Public Property Email() As String
            Get
                Return _email
            End Get
            Set(ByVal Value As String)
                _email = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Image</c> column value.
        '/ </summary>
        '/ <value>The <c>Image</c> column value.</value>
        Public Property Image() As Byte()
            Get
                Return _image
            End Get
            Set(ByVal Value As Byte())
                _image = Value
            End Set
        End Property

        Public Property IsNewImage() As Boolean
            Get
                Return _IsNewImage
            End Get
            Set(ByVal Value As Boolean)
                _IsNewImage = Value
            End Set
        End Property
#End Region

    End Class
    ' End of CompanyInfoAttributeBase class
End Namespace
' End of namespace

'----------------------------------------------------------------
' Converted from C# to VB .NET using CSharpToVBConverter(1.2).
' Developed by: Kamal Patel (http://www.KamalPatel.net) 
'----------------------------------------------------------------
