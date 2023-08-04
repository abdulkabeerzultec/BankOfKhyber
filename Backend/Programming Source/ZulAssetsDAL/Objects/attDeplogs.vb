' <fileinfo name="DepLogsAttributeBase.cs">
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
    '/ It provides the data members with respect to the table DepLogs.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="DepLogsAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attDepLogs
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *depLogID* of the table *DepLogs*
        '/ </summary>
        Private _depLogID As Integer
        '/ <summary>
        '/ It holds the value of *updDate* of the table *DepLogs*
        '/ </summary>
        Private _updDate As System.DateTime
        '/ <summary>
        '/ It holds the value of *depPrdtype* of the table *DepLogs*
        '/ </summary>
        Private _depPrdtype As Integer
        '/ <summary>
        '/ It holds the value of *totDepValue* of the table *DepLogs*
        '/ </summary>
        Private _totDepValue As Double
        '/ <summary>
        '/ It holds the value of *totAstCount* of the table *DepLogs*
        '/ </summary>
        Private _totAstCount As Double
        '/ <summary>
        '/ It holds the value of *totAstValue* of the table *DepLogs*
        '/ </summary>
        Private _totAstValue As Double

        Private _updMonth As Integer
        Private _updYear As Integer
        Private _bookID As Integer


#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="DepLogsAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _depLogID = 0
            _updDate = System.DateTime.MinValue
            _depPrdtype = 0
            _totDepValue = 0
            _totAstCount = 0
            _totAstValue = 0
            _bookID = 0
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of IAttribute interface which sets/gets *depLogID* of the *DepLogs* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return CType(_depLogID, Integer)
            End Get
            Set(ByVal Value As Object)
                _depLogID = CType(Value, Integer)
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>updDate</c> column value.
        '/ </summary>
        '/ <value>The <c>updDate</c> column value.</value>
        Public Property UpdDate() As System.DateTime
            Get
                Return _updDate
            End Get
            Set(ByVal Value As System.DateTime)
                _updDate = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>DepPrdtype</c> column value.
        '/ </summary>
        '/ <value>The <c>DepPrdtype</c> column value.</value>
        Public Property UpdMonth() As Integer
            Get
                Return _updMonth
            End Get
            Set(ByVal Value As Integer)
                _updMonth = Value
            End Set
        End Property
        '/ <summary>
        '/ Gets or sets the <c>DepPrdtype</c> column value.
        '/ </summary>
        '/ <value>The <c>DepPrdtype</c> column value.</value>
        Public Property BookID() As Integer
            Get
                Return _bookID
            End Get
            Set(ByVal Value As Integer)
                _bookID = Value
            End Set
        End Property
        '/ <summary>
        '/ Gets or sets the <c>DepPrdtype</c> column value.
        '/ </summary>
        '/ <value>The <c>DepPrdtype</c> column value.</value>
        Public Property UpdYear() As Integer
            Get
                Return _updYear
            End Get
            Set(ByVal Value As Integer)
                _updYear = Value
            End Set
        End Property
        '/ <summary>
        '/ Gets or sets the <c>DepPrdtype</c> column value.
        '/ </summary>
        '/ <value>The <c>DepPrdtype</c> column value.</value>
        Public Property DepPrdtype() As Integer
            Get
                Return _depPrdtype
            End Get
            Set(ByVal Value As Integer)
                _depPrdtype = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>totDepValue</c> column value.
        '/ </summary>
        '/ <value>The <c>totDepValue</c> column value.</value>
        Public Property TotDepValue() As Double
            Get
                Return _totDepValue
            End Get
            Set(ByVal Value As Double)
                _totDepValue = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>totAstCount</c> column value.
        '/ </summary>
        '/ <value>The <c>totAstCount</c> column value.</value>
        Public Property TotAstCount() As Double
            Get
                Return _totAstCount
            End Get
            Set(ByVal Value As Double)
                _totAstCount = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>totAstValue</c> column value.
        '/ </summary>
        '/ <value>The <c>totAstValue</c> column value.</value>
        Public Property TotAstValue() As Double
            Get
                Return _totAstValue
            End Get
            Set(ByVal Value As Double)
                _totAstValue = Value
            End Set
        End Property

#End Region

    End Class
    ' End of DepLogsAttributeBase class
End Namespace
' End of namespace

'----------------------------------------------------------------
' Converted from C# to VB .NET using CSharpToVBConverter(1.2).
' Developed by: Kamal Patel (http://www.KamalPatel.net) 
'----------------------------------------------------------------
