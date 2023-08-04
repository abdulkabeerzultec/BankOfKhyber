
Imports System

Namespace ZulAssetsDAL

    Public Class attHierGrps
        Implements IAttribute

#Region "Data Members"

        Private _grpCode As String
        Private _grpDesc As String
        Private _LvlCode As String
        Private _IsDelete As Boolean

#End Region

#Region "Constructor"

        Public Sub New()
            _grpCode = ""
            _grpDesc = ""
            _LvlCode = 0
            _IsDelete = 0
        End Sub
#End Region

#Region "Property"

        Public Property PKeyCode() As Object
            Get
                Return _grpCode
            End Get
            Set(ByVal Value As Object)
                _grpCode = Convert.ToString(Value)
            End Set
        End Property

        Public Property GrpCode() As String
            Get
                Return _grpCode
            End Get
            Set(ByVal Value As String)
                _grpCode = Value
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


        Public Property LvlCode() As Integer
            Get
                Return _LvlCode
            End Get
            Set(ByVal Value As Integer)
                _LvlCode = Value
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
