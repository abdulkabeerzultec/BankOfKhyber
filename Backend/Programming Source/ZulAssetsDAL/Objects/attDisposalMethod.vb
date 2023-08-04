' <fileinfo name="Disposal_MethodAttributeBase.cs">
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
    '/ It provides the data members with respect to the table Disposal_Method.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="Disposal_MethodAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attDisposalMethod
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *dispCode* of the table *Disposal_Method*
        '/ </summary>
        Private _dispCode As Integer
        '/ <summary>
        '/ It holds the value of *dispDesc* of the table *Disposal_Method*
        '/ </summary>
        Private _dispDesc As String
        '/ <summary>
        '/ It holds the value of *isDeleted* of the table *Disposal_Method*
        '/ </summary>
        Private _isDeleted As Boolean

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="Disposal_MethodAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _dispCode = 0
            _dispDesc = ""
            _isDeleted = 0
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of IAttribute interface which sets/gets *dispCode* of the *Disposal_Method* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return CType(_dispCode, Integer)
            End Get
            Set(ByVal Value As Object)
                _dispCode = CType(Value, Integer)
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>DispDesc</c> column value.
        '/ </summary>
        '/ <value>The <c>DispDesc</c> column value.</value>
        Public Property DispDesc() As String
            Get
                Return _dispDesc
            End Get
            Set(ByVal Value As String)
                _dispDesc = Value
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
    ' End of Disposal_MethodAttributeBase class
End Namespace
' End of namespace







