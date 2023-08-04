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
    Public Class attAstHistory
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *historyID* of the table *Ast_History*
        '/ </summary>
        Private _historyID As Long
        '/ <summary>
        '/ It holds the value of *invSchCode* of the table *Ast_History*
        '/ </summary>
        Private _invSchCode As Long
        '/ <summary>
        '/ It holds the value of *astID* of the table *Ast_History*
        '/ </summary>
        Private _astID As String
        '/ <summary>
        '/ It holds the value of *fr_loc* of the table *Ast_History*
        '/ </summary>
        Private _fr_loc As String
        '/ <summary>
        '/ It holds the value of *to_Loc* of the table *Ast_History*
        '/ </summary>
        Private _to_Loc As String
        '/ <summary>
        '/ It holds the value of *date* of the table *Ast_History*
        '/ </summary>
        Private _hisdate As System.DateTime
        '/ <summary>
        '/ It holds the value of *status* of the table *Ast_History*
        '/ </summary>
        Private _status As Integer
        '/ <summary>
        '/ It holds the value of *isDeleted* of the table *Ast_History*
        '/ </summary>
        Private _DeptName As String
        Private _Remarks As String
        Private _isDeleted As Boolean

#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="Ast_HistoryAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _historyID = 0.0
            _invSchCode = 0.0
            _astID = ""
            _fr_loc = ""
            _to_Loc = ""
            _hisdate = System.DateTime.MinValue
            _status = 0
            _DeptName = ""
            _Remarks = ""
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
        '/ Gets or sets the <c>InvSchCode</c> column value.
        '/ </summary>
        '/ <value>The <c>InvSchCode</c> column value.</value>
        Public Property InvSchCode() As Long
            Get
                Return _invSchCode
            End Get
            Set(ByVal Value As Long)
                _invSchCode = Value
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
        Public Property Fr_loc() As String
            Get
                Return _fr_loc
            End Get
            Set(ByVal Value As String)
                _fr_loc = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>To_Loc</c> column value.
        '/ </summary>
        '/ <value>The <c>To_Loc</c> column value.</value>
        Public Property To_Loc() As String
            Get
                Return _to_Loc
            End Get
            Set(ByVal Value As String)
                _to_Loc = Value
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
        '/ Gets or sets the <c>Status</c> column value.
        '/ </summary>
        '/ <value>The <c>Status</c> column value.</value>
        Public Property Status() As Integer
            Get
                Return _status
            End Get
            Set(ByVal Value As Integer)
                _status = Value
            End Set
        End Property

        Public Property DeptName() As String
            Get
                Return _DeptName
            End Get
            Set(ByVal Value As String)
                _DeptName = Value
            End Set
        End Property

        Public Property Remarks() As String
            Get
                Return _Remarks
            End Get
            Set(ByVal Value As String)
                _Remarks = Value
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


        Private _NoPiece As Integer
        Public Property NoPiece() As Integer
            Get
                Return _NoPiece
            End Get
            Set(ByVal value As Integer)
                _NoPiece = value
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
