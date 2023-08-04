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
    Public Class attUsers
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *loginName* of the table *Users*
        '/ </summary>
        Private _loginName As String
        '/ <summary>
        '/ It holds the value of *userName* of the table *Users*
        '/ </summary>
        Private _userName As String
        '/ <summary>
        '/ It holds the value of *password* of the table *Users*
        '/ </summary>
        Private _password As String
        '/ <summary>
        '/ It holds the value of *password* of the table *Users*
        '/ </summary>
        Private _roleID As String
        '/ <summary>
        '/ It holds the value of *isDelete* of the table *Users*
        '/ </summary>
        Private _isDelete As Boolean
        '/ <summary>
        '/ It holds the value of *useraccess* of the table *Users*
        '/ </summary>
        Private _useraccess As Int16

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="UsersAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _loginName = ""
            _userName = ""
            _password = ""
            _roleID = ""
            _isDelete = 0
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
        Public Property LoginName() As String
            Get
                Return _loginName
            End Get
            Set(ByVal Value As String)
                _loginName = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>UserName</c> column value.
        '/ </summary>
        '/ <value>The <c>UserName</c> column value.</value>
        Public Property UserName() As String
            Get
                Return _userName
            End Get
            Set(ByVal Value As String)
                _userName = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Password</c> column value.
        '/ </summary>
        '/ <value>The <c>Password</c> column value.</value>
        Public Property Password() As String
            Get
                Return _password
            End Get
            Set(ByVal Value As String)
                _password = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Password</c> column value.
        '/ </summary>
        '/ <value>The <c>Password</c> column value.</value>
        Public Property RoleID() As String
            Get
                Return _roleID
            End Get
            Set(ByVal Value As String)
                _roleID = Value
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
        '/ Gets or sets the <c>UserAccess/c> column value.
        '/ </summary>
        '/ <value>The <c>UserAccess</c> column value.</value>
        Public Property Useraccess() As Int16
            Get
                Return _useraccess
            End Get
            Set(ByVal Value As Int16)
                _useraccess = Value
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
