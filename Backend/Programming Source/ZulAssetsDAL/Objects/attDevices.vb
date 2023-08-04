

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
    Public Class attDevices
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *loginName* of the table *Users*
        '/ </summary>
        Private _deviceID As Integer
        '/ <summary>
        '/ It holds the value of *userName* of the table *Users*
        '/ </summary>
        Private _deviceDesc As String
        '/ <summary>
        '/ It holds the value of *password* of the table *Users*
        '/ </summary>
        Private _comType As Integer
        '/ <summary>
        '/ It holds the value of *password* of the table *Users*
        '/ </summary>
        Private _deviceIP As String
        '/ <summary>
        '/ It holds the value of *isDelete* of the table *Users*
        '/ </summary>
        Private _status As Boolean

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="UsersAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _deviceID = 0
            _deviceDesc = ""
            _comType = 0
            _deviceIP = ""
            _status = 0
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
        Public Property DeviceID() As Integer
            Get
                Return _deviceID
            End Get
            Set(ByVal Value As Integer)
                _deviceID = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>UserName</c> column value.
        '/ </summary>
        '/ <value>The <c>UserName</c> column value.</value>
        Public Property DeviceDesc() As String
            Get
                Return _deviceDesc
            End Get
            Set(ByVal Value As String)
                _deviceDesc = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Password</c> column value.
        '/ </summary>
        '/ <value>The <c>Password</c> column value.</value>
        Public Property ComType() As Integer
            Get
                Return _comType
            End Get
            Set(ByVal Value As Integer)
                _comType = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Password</c> column value.
        '/ </summary>
        '/ <value>The <c>Password</c> column value.</value>
        Public Property DeviceIP() As String
            Get
                Return _deviceIP
            End Get
            Set(ByVal Value As String)
                _deviceIP = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>IsDelete</c> column value.
        '/ </summary>
        '/ <value>The <c>IsDelete</c> column value.</value>
        Public Property Status() As Boolean
            Get
                Return _status
            End Get
            Set(ByVal Value As Boolean)
                _status = Value
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

