

Imports System

Namespace ZulAssetsDAL

    Public Class attLevels
        Implements IAttribute


#Region "Data Members"

        Private _lvlCode As Integer

        Private _lvlDesc As String

        Private _PkeyCode As String

        Private _IsDelete As Boolean

#End Region

#Region "Constructor"

        Public Sub New()
            _lvlCode = 0
            _lvlDesc = ""
            _IsDelete = 0
        End Sub
#End Region

#Region "Property"


        Public Property PKeyCode() As Object
            Get
                Return _PkeyCode
            End Get
            Set(ByVal Value As Object)
                _PkeyCode = Value
            End Set
        End Property

        Public Property LvlCode() As Integer
            Get
                Return _lvlCode
            End Get
            Set(ByVal Value As Integer)
                _lvlCode = Value
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


