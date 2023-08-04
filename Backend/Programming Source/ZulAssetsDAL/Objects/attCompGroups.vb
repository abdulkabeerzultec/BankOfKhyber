Namespace ZulAssetsDAL
    Public Class attCompGroups
        Implements IAttribute

#Region "Data Members"

        Private _grpID As String

        Private _grpDesc As String

        Private _lvlID As Integer

#End Region

#Region "Constructor"

        Public Sub New()
            _lvlID = 0
            _grpDesc = ""
            _grpID = ""
        End Sub
#End Region

#Region "Property"

        Public Property PKeyCode() As String
            Get
                Return _grpID
            End Get
            Set(ByVal Value As String)
                _grpID = Value
            End Set
        End Property

        Public Property GrpDesc() As String
            Get
                Return _grpDesc
            End Get
            Set(ByVal Value As String)
                _grpDesc = Value
            End Set
        End Property

        Public Property LvlID() As Integer
            Get
                Return _lvlID
            End Get
            Set(ByVal Value As Integer)
                _lvlID = Value
            End Set
        End Property

#End Region


    End Class
End Namespace




