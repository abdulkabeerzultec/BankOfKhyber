' <fileinfo name="CustodianAttributeBase.cs">
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
    '/ It provides the data members with respect to the table Custodian.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="CustodianAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attCustodian
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *custodianID* of the table *Custodian*
        '/ </summary>
        Private _custodianID As String
        '/ <summary>
        '/ It holds the value of *custodianName* of the table *Custodian*
        '/ </summary>
        Private _custodianName As String
        '/ <summary>
        '/ It holds the value of *designationID* of the table *Custodian*
        '/ </summary>
        Private _designationID As Integer
        '/ <summary>
        '/ It holds the value of *custodianPhone* of the table *Custodian*
        '/ </summary>
        Private _custodianPhone As String
        '/ <summary>
        '/ It holds the value of *custodianEmail* of the table *Custodian*
        '/ </summary>
        Private _custodianEmail As String
        '/ <summary>
        '/ It holds the value of *custodianFax* of the table *Custodian*
        '/ </summary>
        Private _custodianFax As String
        '/ <summary>
        '/ It holds the value of *custodianCell* of the table *Custodian*
        '/ </summary>
        Private _custodianCell As String
        '/ <summary>
        '/ It holds the value of *custodianAddress* of the table *Custodian*
        '/ </summary>
        Private _custodianAddress As String
        Private _CustodianCode As String
        Private _deptID As String
        '/ <summary>
        '/ It holds the value of *isDeleted* of the table *Custodian*
        '/ </summary>
        Private _isDeleted As Boolean

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="CustodianAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _custodianID = ""
            _custodianName = ""
            _designationID = 0
            _custodianPhone = ""
            _custodianEmail = ""
            _custodianFax = ""
            _custodianCell = ""
            _deptID = ""
            _custodianAddress = ""
            _isDeleted = 0
            _CustodianCode = ""
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of IAttribute interface which sets/gets *custodianID* of the *Custodian* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return CType(_custodianID, String)
            End Get
            Set(ByVal Value As Object)
                _custodianID = CType(Value, String)
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>CustodianName</c> column value.
        '/ </summary>
        '/ <value>The <c>CustodianName</c> column value.</value>
        Public Property CustodianName() As String
            Get
                Return _custodianName
            End Get
            Set(ByVal Value As String)
                _custodianName = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>DesignationID</c> column value.
        '/ </summary>
        '/ <value>The <c>DesignationID</c> column value.</value>
        Public Property DesignationID() As Integer
            Get
                Return _designationID
            End Get
            Set(ByVal Value As Integer)
                _designationID = Value
            End Set
        End Property

        Public Property DepartmentID() As String
            Get
                Return _deptID
            End Get
            Set(ByVal Value As String)
                _deptID = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>CustodianPhone</c> column value.
        '/ </summary>
        '/ <value>The <c>CustodianPhone</c> column value.</value>
        Public Property CustodianPhone() As String
            Get
                Return _custodianPhone
            End Get
            Set(ByVal Value As String)
                _custodianPhone = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>CustodianEmail</c> column value.
        '/ </summary>
        '/ <value>The <c>CustodianEmail</c> column value.</value>
        Public Property CustodianEmail() As String
            Get
                Return _custodianEmail
            End Get
            Set(ByVal Value As String)
                _custodianEmail = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>CustodianFax</c> column value.
        '/ </summary>
        '/ <value>The <c>CustodianFax</c> column value.</value>
        Public Property CustodianFax() As String
            Get
                Return _custodianFax
            End Get
            Set(ByVal Value As String)
                _custodianFax = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>CustodianCell</c> column value.
        '/ </summary>
        '/ <value>The <c>CustodianCell</c> column value.</value>
        Public Property CustodianCell() As String
            Get
                Return _custodianCell
            End Get
            Set(ByVal Value As String)
                _custodianCell = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>CustodianAddress</c> column value.
        '/ </summary>
        '/ <value>The <c>CustodianAddress</c> column value.</value>
        Public Property CustodianAddress() As String
            Get
                Return _custodianAddress
            End Get
            Set(ByVal Value As String)
                _custodianAddress = Value
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

        Public Property CustodianCode() As String
            Get
                Return _CustodianCode
            End Get
            Set(ByVal Value As String)
                _CustodianCode = Value
            End Set
        End Property
#End Region

    End Class
    ' End of CustodianAttributeBase class
End Namespace
' End of namespace
