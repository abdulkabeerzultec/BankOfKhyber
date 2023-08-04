Imports System

Namespace ZulAssetsDAL
    Public Class attCustomFields
        Implements IAttribute


#Region "Data Members"
        'Private _ID As Integer
        'Private _FormName As String
        'Private _ControlName As String
        'Private _EngCaption As String
        'Private _ArabicCaption As String
        'Private _UrduCaption As String
        'Private _Example As String

#End Region

#Region "Constructor"
        Public Sub New()
            _ID = 0
            _FormName = String.Empty
            _ControlName = String.Empty
            _EngCaption = String.Empty
            _ArabicCaption = String.Empty
            _UrduCaption = String.Empty
            _Example = String.Empty
        End Sub
#End Region

#Region "Property"

        Private _ID As String
        Public Property ID() As String
            Get
                Return _ID
            End Get
            Set(ByVal value As String)
                _ID = value
            End Set
        End Property

        Private _FormName As String
        Public Property FormName() As String
            Get
                Return _FormName
            End Get
            Set(ByVal value As String)
                _FormName = value
            End Set
        End Property


        Private _ControlName As String
        Public Property ControlName() As String
            Get
                Return _ControlName
            End Get
            Set(ByVal value As String)
                _ControlName = value
            End Set
        End Property


        Private _EngCaption As String
        Public Property EngCaption() As String
            Get
                Return _EngCaption
            End Get
            Set(ByVal value As String)
                _EngCaption = value
            End Set
        End Property


        Private _ArabicCaption As String
        Public Property ArabicCaption() As String
            Get
                Return _ArabicCaption
            End Get
            Set(ByVal value As String)
                _ArabicCaption = value
            End Set
        End Property



        Private _UrduCaption As String
        Public Property UrduCaption() As String
            Get
                Return _UrduCaption
            End Get
            Set(ByVal value As String)
                _UrduCaption = value
            End Set
        End Property


        Private _Example As String
        Public Property Example() As String
            Get
                Return _Example
            End Get
            Set(ByVal value As String)
                _Example = value
            End Set
        End Property


#End Region

    End Class
End Namespace
