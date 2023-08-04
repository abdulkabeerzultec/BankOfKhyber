' <fileinfo name="AssetsAttributeBase.cs">
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
    '/ It provides the "Data Members" with respect to the table Assets.
    '/ </summary>
    '/ <remarks>
    '/ Do not change this source code manually. Update the <see cref="AssetsAttribute"/>
    '/ class if you need to add or change some functionality.
    '/ </remarks>
    Public Class attItems
        Implements IAttribute

#Region "Data Members"
        Private _itemCode As String
        Private _astBrandID As Integer
        Private _astCatID As String
        Private _pOItmID As String
        Private _astDesc As String
        Private _astModel As String
        Private _astQty As Long
        Private _isDeleted As Boolean
        Private _image As Byte()
        Private _Warranty As Integer
#End Region

#Region "Constructor"
        Public Sub New()
            _itemCode = ""
            _astBrandID = 0
            _astCatID = ""
            _pOItmID = ""
            _astDesc = ""
            _astModel = ""
            _astQty = 0.0
            _isDeleted = 0
            _Warranty = 0
        End Sub
#End Region

#Region "Property"

        Public Property PKeyCode() As Object
            Get
                Return CType(_itemCode, String)
            End Get
            Set(ByVal Value As Object)
                _itemCode = CType(Value, String)
            End Set
        End Property

        Public Property AstBrandID() As Integer
            Get
                Return _astBrandID
            End Get
            Set(ByVal Value As Integer)
                _astBrandID = Value
            End Set
        End Property

        Public Property AstCatID() As String
            Get
                Return _astCatID
            End Get
            Set(ByVal Value As String)
                _astCatID = Value
            End Set
        End Property

        Public Property POItmID() As String
            Get
                Return _pOItmID
            End Get
            Set(ByVal Value As String)
                _pOItmID = Value
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

        Public Property AstModel() As String
            Get
                Return _astModel
            End Get
            Set(ByVal Value As String)
                _astModel = Value
            End Set
        End Property

        Public Property AstQty() As Long
            Get
                Return _astQty
            End Get
            Set(ByVal Value As Long)
                _astQty = Value
            End Set
        End Property

        Public Property IsDeleted() As Boolean
            Get
                Return _isDeleted
            End Get
            Set(ByVal Value As Boolean)
                _isDeleted = Value
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

        Public Property Warranty() As Integer
            Get
                Return _Warranty
            End Get
            Set(ByVal value As Integer)
                _Warranty = value
            End Set
        End Property

#End Region

    End Class
End Namespace
