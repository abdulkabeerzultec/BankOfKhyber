
' <fileinfo name="Depriciation_MethodAttributeBase.cs">
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
    '/ It provides the data members with respect to the table Depriciation_Method.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="Depriciation_MethodAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>

    Public Class attDepreciationMethod
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *depCode* of the table *Depriciation_Method*
        '/ </summary>
        Private _depCode As Integer
        '/ <summary>
        '/ It holds the value of *depDesc* of the table *Depriciation_Method*
        '/ </summary>
        Private _depDesc As String
        '/ <summary>
        '/ It holds the value of *isDeleted* of the table *Depriciation_Method*
        '/ </summary>
        Private _isDeleted As Boolean

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="Depriciation_MethodAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _depCode = 0
            _depDesc = ""
            _isDeleted = 0
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of IAttribute interface which sets/gets *depCode* of the *Depriciation_Method* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return CType(_depCode, Integer)
            End Get
            Set(ByVal Value As Object)
                _depCode = CType(Value, Integer)
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>DepDesc</c> column value.
        '/ </summary>
        '/ <value>The <c>DepDesc</c> column value.</value>
        Public Property DepDesc() As String
            Get
                Return _depDesc
            End Get
            Set(ByVal Value As String)
                _depDesc = Value
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
    ' End of Depriciation_MethodAttributeBase class
End Namespace
' End of namespace

