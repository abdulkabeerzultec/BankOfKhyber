Imports ZulAssetsDAL.ZulAssetsDAL
Namespace ZulAssetsDAL

    Public Class AttOfflineMachines
        Implements IAttribute
#Region "Data Members"

        Private _MachineID As Integer
        Private _MachineDesc As String
        Private _ServerName As String
        Private _DatabaseName As String
        Private _UserName As String
        Private _Password As String
        Private _Port As String
        Private _LastAssetNumber As Integer
        Private _StartSerial As Int64
        Private _EndSerial As Int64
        Private _CompanyID As Integer
        Private _IsDelete As Boolean
#End Region

#Region "Constructor"

        Public Sub New()
            _MachineID = 0
            _MachineDesc = ""
            _ServerName = ""
            _DatabaseName = "ZulAssetsBE"
            _UserName = ""
            _Password = ""
            _Port = ""
            _LastAssetNumber = 0
            _StartSerial = 0
            _EndSerial = 0
            _CompanyID = 0
            _IsDelete = 0
        End Sub
#End Region

#Region "Property"

        Public Property MachineID() As Integer
            Get
                Return _MachineID
            End Get
            Set(ByVal value As Integer)
                _MachineID = value
            End Set
        End Property

        Public Property MachineDesc() As String
            Get
                Return _MachineDesc
            End Get
            Set(ByVal value As String)
                _MachineDesc = value
            End Set
        End Property

        Public Property ServerName() As String
            Get
                Return _ServerName
            End Get
            Set(ByVal value As String)
                _ServerName = value
            End Set
        End Property
        Public Property DatabaseName() As String
            Get
                Return _DatabaseName
            End Get
            Set(ByVal value As String)
                _DatabaseName = value
            End Set
        End Property
        Public Property UserName() As String
            Get
                Return _UserName
            End Get
            Set(ByVal value As String)
                _UserName = value
            End Set
        End Property
        Public Property Password() As String
            Get
                Return _Password
            End Get
            Set(ByVal value As String)
                _Password = value
            End Set
        End Property

        Public Property Port() As String
            Get
                Return _Port
            End Get
            Set(ByVal value As String)
                _Port = value
            End Set
        End Property

        Public Property LastAssetNumber() As Integer
            Get
                Return _LastAssetNumber
            End Get
            Set(ByVal value As Integer)
                _LastAssetNumber = value
            End Set
        End Property
        Public Property StartSerial() As Int64

            Get
                Return _StartSerial
            End Get
            Set(ByVal value As Int64)
                _StartSerial = value
            End Set
        End Property

        Public Property EndSerial() As Int64

            Get
                Return _EndSerial
            End Get
            Set(ByVal value As Int64)
                _EndSerial = value
            End Set
        End Property

        Public Property CompanyID() As Integer
            Get
                Return _CompanyID
            End Get
            Set(ByVal value As Integer)
                _CompanyID = value
            End Set
        End Property
        Public Property IsDelete() As Boolean
            Get
                Return _IsDelete
            End Get
            Set(ByVal Value As Boolean)
                _IsDelete = Convert.ToBoolean(Value)
            End Set
        End Property
#End Region

    End Class
End Namespace
