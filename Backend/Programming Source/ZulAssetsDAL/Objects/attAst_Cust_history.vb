

' <fileinfo name="Ast_HistoryAttributeBase.cs">
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
    '/ It provides the "Data Members" with respect to the table Ast_History.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="Ast_HistoryAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attAst_Cust_history
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *historyID* of the table *Ast_History*
        '/ </summary>
        Private _historyID As Long
        '/ <summary>
        '/ It holds the value of *invSchCode* of the table *Ast_History*
        '/ </summary>

        Private _astID As String
        '/ <summary>
        '/ It holds the value of *fr_loc* of the table *Ast_History*
        '/ </summary>
        Private _fr_Cust As String
        '/ <summary>
        '/ It holds the value of *to_Loc* of the table *Ast_History*
        '/ </summary>
        Private _to_Cust As String
        '/ <summary>
        '/ It holds the value of *date* of the table *Ast_History*
        '/ </summary>
        Private _hisdate As System.DateTime

        '/ <summary>
        '/ It holds the value of *isDeleted* of the table *Ast_History*
        '/ </summary>
        Private _isDeleted As Boolean

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="Ast_HistoryAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _historyID = 0.0
            _astID = ""
            _fr_Cust = ""
            _to_Cust = ""
            _hisdate = System.DateTime.MinValue
            _isDeleted = 0
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of IAttribute interface which sets/gets *historyID* of the *Ast_History* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return CType(_historyID, Long)
            End Get
            Set(ByVal Value As Object)
                _historyID = CType(Value, Long)
            End Set
        End Property

        

        '/ <summary>
        '/ Gets or sets the <c>AstID</c> column value.
        '/ </summary>
        '/ <value>The <c>AstID</c> column value.</value>
        Public Property AstID() As String
            Get
                Return _astID
            End Get
            Set(ByVal Value As String)
                _astID = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Fr_loc</c> column value.
        '/ </summary>
        '/ <value>The <c>Fr_loc</c> column value.</value>
        Public Property Fr_Cust() As String
            Get
                Return _fr_Cust
            End Get
            Set(ByVal Value As String)
                _fr_Cust = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>To_Loc</c> column value.
        '/ </summary>
        '/ <value>The <c>To_Loc</c> column value.</value>
        Public Property To_Cust() As String
            Get
                Return _to_Cust
            End Get
            Set(ByVal Value As String)
                _to_Cust = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Date</c> column value.
        '/ </summary>
        '/ <value>The <c>Date</c> column value.</value>
        Public Property HisDate() As System.DateTime
            Get
                Return _hisdate
            End Get
            Set(ByVal Value As System.DateTime)
                _hisdate = Value
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
    ' End of Ast_HistoryAttributeBase class
End Namespace
' End of namespace

'----------------------------------------------------------------

'
'----------------------------------------------------------------

