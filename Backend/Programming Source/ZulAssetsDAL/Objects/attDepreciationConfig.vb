
' <fileinfo name="DepreciationConfigAttributeBase.cs">
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
    '/ It provides the data members with respect to the table DepreciationConfig.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="DepreciationConfigAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attDepreciationConfig

        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *id* of the table *DepreciationConfig*
        '/ </summary>
        Private _id As Integer
        '/ <summary>
        '/ It holds the value of *depr_Period* of the table *DepreciationConfig*
        '/ </summary>
        Private _depr_Period As String
        '/ <summary>
        '/ It holds the value of *financialYearStart* of the table *DepreciationConfig*
        '/ </summary>
        Private _financialYearStart As String
        '/ <summary>
        '/ It holds the value of *invStartDate* of the table *Ast_INV_Schedule*
        '/ </summary>
        Private _finyrStartDate As System.DateTime

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="DepreciationConfigAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _id = 0
            _depr_Period = ""
            _financialYearStart = ""
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of NULL Primary Key of the *DepreciationConfig* table 
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
        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal Value As Integer)
                _id = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Depr_Period</c> column value.
        '/ </summary>
        '/ <value>The <c>Depr_Period</c> column value.</value>
        Public Property Depr_Period() As String
            Get
                Return _depr_Period
            End Get
            Set(ByVal Value As String)
                _depr_Period = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>FinancialYearStart</c> column value.
        '/ </summary>
        '/ <value>The <c>FinancialYearStart</c> column value.</value>
        Public Property FinancialYearStart() As String
            Get
                Return _financialYearStart
            End Get
            Set(ByVal Value As String)
                _financialYearStart = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>InvStartDate</c> column value.
        '/ </summary>
        '/ <value>The <c>InvStartDate</c> column value.</value>
        Public Property FinyrStartDate() As System.DateTime
            Get
                Return _finyrStartDate
            End Get
            Set(ByVal Value As System.DateTime)
                _finyrStartDate = Value
            End Set
        End Property

#End Region

    End Class
    ' End of DepreciationConfigAttributeBase class
End Namespace
' End of namespace

'----------------------------------------------------------------
' Converted from C# to VB .NET using CSharpToVBConverter(1.2).
' Developed by: Kamal Patel (http://www.KamalPatel.net) 
'----------------------------------------------------------------

