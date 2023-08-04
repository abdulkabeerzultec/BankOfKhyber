' <fileinfo name="DesignationAttributeBase.cs">
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
    '/ It provides the data members with respect to the table Designation.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="DesignationAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attDesignation
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *designationID* of the table *Designation*
        '/ </summary>
        Private _designationID As Integer
        '/ <summary>
        '/ It holds the value of *description* of the table *Designation*
        '/ </summary>
        Private _description As String
        '/ <summary>
        '/ It holds the value of *isDeleted* of the table *Designation*
        '/ </summary>
        Private _isDeleted As Boolean

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="DesignationAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _designationID = 0
            _description = ""
            _isDeleted = 0
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of IAttribute interface which sets/gets *designationID* of the *Designation* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return CType(_designationID, Integer)
            End Get
            Set(ByVal Value As Object)
                _designationID = CType(Value, Integer)
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Description</c> column value.
        '/ </summary>
        '/ <value>The <c>Description</c> column value.</value>
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal Value As String)
                _description = Value
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
    ' End of DesignationAttributeBase class
End Namespace
' End of namespace

