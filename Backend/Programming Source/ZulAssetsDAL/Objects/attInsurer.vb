' <fileinfo name="InsurerAttributeBase.cs">
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
    '/ It provides the data members with respect to the table Insurer.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="InsurerAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attInsurer
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *insCode* of the table *Insurer*
        '/ </summary>
        Private _insCode As Integer
        '/ <summary>
        '/ It holds the value of *insName* of the table *Insurer*
        '/ </summary>
        Private _insName As String
        '/ <summary>
        '/ It holds the value of *isDeleted* of the table *Insurer*
        '/ </summary>
        Private _isDeleted As Boolean

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="InsurerAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _insCode = 0
            _insName = ""
            _isDeleted = 0
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of IAttribute interface which sets/gets *insCode* of the *Insurer* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return CType(_insCode, Integer)
            End Get
            Set(ByVal Value As Object)
                _insCode = CType(Value, Integer)
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>InsName</c> column value.
        '/ </summary>
        '/ <value>The <c>InsName</c> column value.</value>
        Public Property InsName() As String
            Get
                Return _insName
            End Get
            Set(ByVal Value As String)
                _insName = Value
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
    ' End of InsurerAttributeBase class
End Namespace
' End of namespace

