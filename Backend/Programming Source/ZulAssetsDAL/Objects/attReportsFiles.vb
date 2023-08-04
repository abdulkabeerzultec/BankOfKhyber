Imports System
Namespace ZulAssetsDAL

    Public Class attReports
        Implements IAttribute

#Region "Data Members"

        Private _ReportName As String
        Private _ReportData As String
        Private _Query As String
        Private _Type As Boolean

#End Region

#Region "Constructor"

        Public Sub New()
            _ReportName = ""
            _ReportData = ""
            _Query = ""
            _Type = False
        End Sub
#End Region

#Region "Property"

        Public Property ReportName() As Object
            Get
                Return _ReportName
            End Get
            Set(ByVal Value As Object)
                _ReportName = Value
            End Set
        End Property

        Public Property ReportData() As String
            Get
                Return _ReportData
            End Get
            Set(ByVal Value As String)
                _ReportData = Value
            End Set
        End Property

        Public Property Query() As String
            Get
                Return _Query
            End Get
            Set(ByVal Value As String)
                _Query = Value
            End Set
        End Property

        Public Property Type() As String
            Get
                Return _Type
            End Get
            Set(ByVal Value As String)
                _Type = Value
            End Set
        End Property

#End Region

    End Class

End Namespace

