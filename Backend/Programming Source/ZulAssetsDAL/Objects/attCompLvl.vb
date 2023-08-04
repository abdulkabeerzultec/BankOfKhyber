
Imports System
Namespace ZulAssetsDAL

    Public Class attCompLvl
        Implements IAttribute

#Region "Data Members"

        Private _lvlID As Integer

        Private _lvlDesc As String

#End Region

#Region "Constructor"

        Public Sub New()
            _lvlID = 0
            _lvlDesc = ""

        End Sub
#End Region

#Region "Property"

        Public Property PKeyCode() As Object
            Get
                Return _lvlID
            End Get
            Set(ByVal Value As Object)
                _lvlID = Value
            End Set
        End Property

        Public Property LvlDesc() As String
            Get
                Return _lvlDesc
            End Get
            Set(ByVal Value As String)
                _lvlDesc = Value
            End Set
        End Property


#End Region

    End Class

End Namespace

