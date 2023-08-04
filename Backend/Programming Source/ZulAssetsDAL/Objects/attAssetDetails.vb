Namespace ZulAssetsDAL
    '/ <summary>
    '/ It is derived from IAttribute interface.
    '/ It provides the "Data Members" with respect to the table AssetDetails.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="AssetDetailsAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attAssetDetails
        Implements IAttribute


#Region "Data Members"
        '/ <summary>
        '/ It holds the value of *astID* of the table *AssetDetails*
        '/ </summary>
        Private _astID As String
        '/ <summary>
        '/ It holds the value of *dispCode* of the table *AssetDetails*
        '/ </summary>
        Private _dispCode As Integer
        '/ <summary>
        '/ It holds the value of *itemCode* of the table *AssetDetails*
        '/ </summary>
        Private _itemCode As String
        '/ <summary>
        '/ It holds the value of *suppID* of the table *AssetDetails*
        '/ </summary>
        Private _suppID As String
        '/ <summary>
        '/ It holds the value of *pOCode* of the table *AssetDetails*
        '/ </summary>
        Private _pOCode As String
        '/ <summary>
        '/ It holds the value of *invNumber* of the table *AssetDetails*
        '/ </summary>
        Private _invNumber As String
        '/ <summary>
        '/ It holds the value of *custodianID* of the table *AssetDetails*
        '/ </summary>
        Private _custodianID As String
        '/ <summary>
        '/ It holds the value of *baseCost* of the table *AssetDetails*
        '/ </summary>
        Private _baseCost As Double
        '/ <summary>
        '/ It holds the value of *tax* of the table *AssetDetails*
        '/ </summary>
        Private _tax As Double
        '/ <summary>
        '/ It holds the value of *purDate* of the table *AssetDetails*
        '/ </summary>
        Private _purDate As System.DateTime
        '/ <summary>
        '/ It holds the value of *purDate* of the table *AssetDetails*
        '/ </summary>
        Private _srvDate As System.DateTime
        '/ <summary>
        '/ It holds the value of *depCode* of the table *AssetDetails*
        '/ </summary>
        Private _depCode As Integer

        Private _noPiece As Integer
        '/ <summary>
        '/ It holds the value of *disposed* of the table *AssetDetails*
        '/ </summary>
        Private _disposed As Boolean
        Private _Exclude As String
        '/ <summary>
        '/ It holds the value of *dispDate* of the table *AssetDetails*
        '/ </summary>
        Private _dispDate As System.DateTime
        '/ <summary>
        '/ It holds the value of *invSchCode* of the table *AssetDetails*
        '/ </summary>
        Private _invSchCode As Long
        '/ <summary>
        '/ It holds the value of *bookID* of the table *AssetDetails*
        '/ </summary>
        Private _bookID As String
        '/ <summary>
        '/ It holds the value of *insID* of the table *AssetDetails*
        '/ </summary>
        Private _insID As Integer
        '/ <summary>
        '/ It holds the value of *locID* of the table *AssetDetails*
        '/ </summary>
        Private _locID As String

        '/ <summary>
        '/ It holds the value of *invStatus* of the table *AssetDetails*
        '/ </summary>
        Private _invStatus As Integer

        Private _companyID As Integer
        '/ <summary>
        '/ It holds the value of *isDeleted* of the table *AssetDetails*
        '/ </summary>
        Private _isDeleted As Boolean
        '/ <summary>
        '/ It holds the value of *isDeleted* of the table *AssetDetails*
        '/ </summary>
        Private _isSold As Boolean
        '/ <summary>
        '/ It holds the value of *sel_Date* of the table *Ast_Sold*
        '/ </summary>
        Private _sel_Date As System.DateTime
        '/ <summary>
        '/ It holds the value of *sel_Price* of the table *Ast_Sold*
        '/ </summary>
        Private _sel_Price As Double
        '/ <summary>
        '/ It holds the value of *bookValue* of the table *Ast_Sold*
        '/ </summary>
        Private _discount As Double

        Private _soldTo As String

        Private _transRemarks As String


        Private _astNum As Double
        '/ <summary>
        '/ It holds the value of *isDeleted* of the table *Ast_Sold*
        '/ </summary>
        Private _refNo As String
        Private _astBrandID As Integer
        Private _astDesc As String
        Private _astDesc2 As String
        Private _astModel As String
        Private _barCode As String
        Private _serailNo As String
        Private _RefCode As String
        Private _plate As String
        Private _poerp As String
        Private _capex As String
        Private _grn As String
        Private _glcode As Integer
        Private _ponumber As String
        Private _image As Byte()

        'ABB Fields SAP Integration
        Public CapitalizationDate As DateTime
        Public BussinessArea As String
        Public InventoryNumber As String
        Public CostCenterID As String
        Public InStockAsset As Boolean
        Public EvaluationGroup1 As String
        Public EvaluationGroup2 As String
        Public EvaluationGroup3 As String
        Public EvaluationGroup4 As String
        Public CreatedBY As String
        Public IsDataChanged As Boolean = False
        Public OldAssetID As String
        Public LastInventoryDate As DateTime
        Public LastEditDate As DateTime
        Public CreationDate As DateTime
        Public LastEditBY As String
        Public CustomFld1 As String
        Public CustomFld2 As String
        Public CustomFld3 As String
        Public CustomFld4 As String
        Public CustomFld5 As String
        Public Warranty As Integer
        Public StatusID As Integer
        Public DisposalComments As String
#End Region

#Region "Constructor"
        '/ <summary>
        '/ Initializes a new instance of the <see cref="AssetDetailsAttributeBase"/> class.
        '/ </summary>
        Public Sub New()
            _discount = 0
            _noPiece = 0
            _astID = ""
            _dispCode = 0
            _itemCode = ""
            _suppID = ""
            _pOCode = ""
            _invNumber = ""
            _custodianID = ""
            _baseCost = 0
            _tax = 0
            _srvDate = System.DateTime.MinValue
            _purDate = System.DateTime.MinValue
            _depCode = 0
            _disposed = 0
            _Exclude = ""
            _dispDate = Nothing
            _invSchCode = 0
            _bookID = ""
            _barCode = ""
            _insID = 0
            _locID = ""
            _invStatus = 0
            _isDeleted = 0
            _isSold = 0
            _astNum = 0
            _sel_Date = Nothing
            _sel_Price = 0
            _soldTo = ""
            _astBrandID = 0
            _astDesc = ""
            _astDesc2 = ""

            _astModel = ""
            _companyID = 0
            _transRemarks = ""
            _serailNo = ""
            DisposalComments = ""
            StatusID = 1
        End Sub
#End Region

#Region "Property"

        '/ <summary>
        '/ Implements the property of IAttribute interface which sets/gets *astID* of the *AssetDetails* table 
        '/ </summary>
        Public Property PKeyCode() As Object
            Get
                Return CType(_astID, String)
            End Get
            Set(ByVal Value As Object)
                _astID = CType(Value, String)
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>DispCode</c> column value.
        '/ </summary>
        '/ <value>The <c>DispCode</c> column value.</value>
        Public Property DispCode() As Integer
            Get
                Return _dispCode
            End Get
            Set(ByVal Value As Integer)
                _dispCode = Value
            End Set
        End Property



        Public Property CompanyID() As Integer
            Get
                Return _companyID
            End Get
            Set(ByVal Value As Integer)
                _companyID = Value
            End Set
        End Property
        Public Property NoPiece() As Integer
            Get
                Return _noPiece
            End Get
            Set(ByVal Value As Integer)
                _noPiece = Value
            End Set
        End Property
        '/ <summary>
        '/ Gets or sets the <c>ItemCode</c> column value.
        '/ </summary>
        '/ <value>The <c>ItemCode</c> column value.</value>
        Public Property BarCode() As String
            Get
                Return _barCode
            End Get
            Set(ByVal Value As String)
                _barCode = Value
            End Set
        End Property


        Public Property TransRemarks() As String
            Get
                Return _transRemarks
            End Get
            Set(ByVal Value As String)
                _transRemarks = Value
            End Set
        End Property

        Public Property ItemCode() As String
            Get
                Return _itemCode
            End Get
            Set(ByVal Value As String)
                _itemCode = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>SuppID</c> column value.
        '/ </summary>
        '/ <value>The <c>SuppID</c> column value.</value>
        Public Property SuppID() As String
            Get
                Return _suppID
            End Get
            Set(ByVal Value As String)
                _suppID = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>POCode</c> column value.
        '/ </summary>
        '/ <value>The <c>POCode</c> column value.</value>
        Public Property POCode() As String
            Get
                Return _pOCode
            End Get
            Set(ByVal Value As String)
                _pOCode = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>InvNumber</c> column value.
        '/ </summary>
        '/ <value>The <c>InvNumber</c> column value.</value>
        Public Property InvNumber() As String
            Get
                Return _invNumber
            End Get
            Set(ByVal Value As String)
                _invNumber = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>CustodianID</c> column value.
        '/ </summary>
        '/ <value>The <c>CustodianID</c> column value.</value>
        Public Property CustodianID() As String
            Get
                Return _custodianID
            End Get
            Set(ByVal Value As String)
                _custodianID = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>BaseCost</c> column value.
        '/ </summary>
        '/ <value>The <c>BaseCost</c> column value.</value>
        Public Property BaseCost() As Double
            Get
                Return _baseCost
            End Get
            Set(ByVal Value As Double)
                _baseCost = Value
            End Set
        End Property
        '/ <summary>
        '/ Gets or sets the <c>Tax</c> column value.
        '/ </summary>
        '/ <value>The <c>Tax</c> column value.</value>


        Public Property Discount() As Double
            Get
                Return _discount
            End Get
            Set(ByVal Value As Double)
                _discount = Value
            End Set
        End Property
        '/ <summary>
        '/ Gets or sets the <c>Tax</c> column value.
        '/ </summary>
        '/ <value>The <c>Tax</c> column value.</value>
        Public Property Tax() As Double
            Get
                Return _tax
            End Get
            Set(ByVal Value As Double)
                _tax = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>PurDate</c> column value.
        '/ </summary>
        '/ <value>The <c>PurDate</c> column value.</value>
        Public Property SrvDate() As System.DateTime
            Get
                Return _srvDate
            End Get
            Set(ByVal Value As System.DateTime)
                _srvDate = Value
            End Set
        End Property
        '/ <summary>
        '/ Gets or sets the <c>PurDate</c> column value.
        '/ </summary>
        '/ <value>The <c>PurDate</c> column value.</value>
        Public Property PurDate() As System.DateTime
            Get
                Return _purDate
            End Get
            Set(ByVal Value As System.DateTime)
                _purDate = Value
            End Set
        End Property
        '/ <summary>
        '/ Gets or sets the <c>DepCode</c> column value.
        '/ </summary>
        '/ <value>The <c>DepCode</c> column value.</value>
        Public Property DepCode() As Integer
            Get
                Return _depCode
            End Get
            Set(ByVal Value As Integer)
                _depCode = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Disposed</c> column value.
        '/ </summary>
        '/ <value>The <c>Disposed</c> column value.</value>
        Public Property Disposed() As Boolean
            Get
                Return _disposed
            End Get
            Set(ByVal Value As Boolean)
                _disposed = Value
            End Set
        End Property

        Public Property ExcludeDisposed() As String
            Get
                Return _Exclude
            End Get
            Set(ByVal Value As String)
                _Exclude = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>DispDate</c> column value.
        '/ </summary>
        '/ <value>The <c>DispDate</c> column value.</value>
        Public Property DispDate() As System.DateTime
            Get
                Return _dispDate
            End Get
            Set(ByVal Value As System.DateTime)
                _dispDate = Value
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
        '/ Gets or sets the <c>BookID</c> column value.
        '/ </summary>
        '/ <value>The <c>BookID</c> column value.</value>
        Public Property BookID() As String
            Get
                Return _bookID
            End Get
            Set(ByVal Value As String)
                _bookID = Value
            End Set
        End Property
        Public Property SerailNo() As String
            Get
                Return _serailNo
            End Get
            Set(ByVal Value As String)
                _serailNo = Value
            End Set
        End Property



        '/ <summary>
        '/ Gets or sets the <c>Reference Number</c> column value.
        '/ </summary>
        '/ <value>The <c>Reference Number</c> column value.</value>
        Public Property RefNo() As String
            Get
                Return _refNo
            End Get
            Set(ByVal Value As String)
                _refNo = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>InsID</c> column value.
        '/ </summary>
        '/ <value>The <c>InsID</c> column value.</value>
        Public Property InsID() As Integer
            Get
                Return _insID
            End Get
            Set(ByVal Value As Integer)
                _insID = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>LocID</c> column value.
        '/ </summary>
        '/ <value>The <c>LocID</c> column value.</value>
        Public Property LocID() As String
            Get
                Return _locID
            End Get
            Set(ByVal Value As String)
                _locID = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>InvStatus</c> column value.
        '/ </summary>
        '/ <value>The <c>InvStatus</c> column value.</value>
        Public Property InvStatus() As Integer
            Get
                Return _invStatus
            End Get
            Set(ByVal Value As Integer)
                _invStatus = Value
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
        '/ <summary>
        '/ Gets or sets the <c>IsDeleted</c> column value.
        '/ </summary>
        '/ <value>The <c>IsDeleted</c> column value.</value>
        Public Property IsSold() As Boolean
            Get
                Return _isSold
            End Get
            Set(ByVal Value As Boolean)
                _isSold = Value
            End Set
        End Property
        '/ <summary>
        '/ Gets or sets the <c>Sel_Date</c> column value.
        '/ </summary>
        '/ <value>The <c>Sel_Date</c> column value.</value>
        Public Property Sel_Date() As System.DateTime
            Get
                Return _sel_Date
            End Get
            Set(ByVal Value As System.DateTime)
                _sel_Date = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>Sel_Price</c> column value.
        '/ </summary>
        '/ <value>The <c>Sel_Price</c> column value.</value>
        Public Property Sel_Price() As Double
            Get
                Return _sel_Price
            End Get
            Set(ByVal Value As Double)
                _sel_Price = Value
            End Set
        End Property



        '/ <summary>
        '/ Gets or sets the <c>SoldTo</c> column value.
        '/ </summary>
        '/ <value>The <c>SoldTo</c> column value.</value>
        Public Property SoldTo() As String
            Get
                Return _soldTo
            End Get
            Set(ByVal Value As String)
                _soldTo = Value
            End Set
        End Property

        Public Property AstNum() As Double
            Get
                Return _astNum
            End Get
            Set(ByVal Value As Double)
                _astNum = Value
            End Set
        End Property
        Public Property AstDesc() As String
            Get
                Return _astDesc
            End Get
            Set(ByVal Value As String)
                _astDesc = Value
            End Set
        End Property
        Public Property AstDesc2() As String
            Get
                Return _astDesc2
            End Get
            Set(ByVal Value As String)
                _astDesc2 = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>AstModel</c> column value.
        '/ </summary>
        '/ <value>The <c>AstModel</c> column value.</value>
        Public Property AstModel() As String
            Get
                Return _astModel
            End Get
            Set(ByVal Value As String)
                _astModel = Value
            End Set
        End Property

        '/ <summary>
        '/ Gets or sets the <c>AstBrandID</c> column value.
        '/ </summary>
        '/ <value>The <c>AstBrandID</c> column value.</value>
        Public Property AstBrandID() As String
            Get
                Return _astBrandID
            End Get
            Set(ByVal Value As String)
                _astBrandID = Value
            End Set
        End Property

        Public Property RefCode() As String
            Get
                Return _RefCode
            End Get
            Set(ByVal Value As String)
                _RefCode = Value
            End Set
        End Property

        Public Property Plate() As String
            Get
                Return _plate
            End Get
            Set(ByVal Value As String)
                _plate = Value
            End Set
        End Property

        Public Property PoErp() As String
            Get
                Return _poerp
            End Get
            Set(ByVal Value As String)
                _poerp = Value
            End Set
        End Property

        Public Property Capex() As String
            Get
                Return _capex
            End Get
            Set(ByVal Value As String)
                _capex = Value
            End Set
        End Property

        Public Property GRN() As String
            Get
                Return _grn
            End Get
            Set(ByVal Value As String)
                _grn = Value
            End Set
        End Property

        Public Property GLCode() As Integer
            Get
                Return _glcode
            End Get
            Set(ByVal Value As Integer)
                _glcode = Value
            End Set
        End Property

        Public Property PONumber() As String
            Get
                Return _ponumber
            End Get
            Set(ByVal Value As String)
                _ponumber = Value
            End Set
        End Property

        Public Property Image() As Byte()
            Get
                Return _image
            End Get
            Set(ByVal Value As Byte())
                _image = Value
            End Set
        End Property
#End Region
    End Class
End Namespace
