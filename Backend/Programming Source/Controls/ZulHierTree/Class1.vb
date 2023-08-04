Public Class clsTree

    Private _DBType As Byte
    Private _DBUName As String       'User name for database
    Private _DBPass As String        'Password for database
    Private _DBServer As String      'Server on which database is running
    Private _DBName As String        'Name of database (oracle or sql server)
    Private _DBPath As String        'Path of microsoft access database
    Private _SelectOnlyLastNode As Boolean
    Friend frm As frmTree


    Public Property SelectOnlyLastNode() As Boolean
        Get
            Return _SelectOnlyLastNode
        End Get

        Set(ByVal Value As Boolean)
            _SelectOnlyLastNode = Value
        End Set
    End Property



    Public Property DBType() As Byte
        Get
            Return _DBType
        End Get

        Set(ByVal Value As Byte)
            _DBType = Value
        End Set
    End Property

    Public Property DBUName() As String
        Get
            Return _DBUName
        End Get

        Set(ByVal Value As String)
            _DBUName = Value
        End Set
    End Property

    Public Property DBPass() As String
        Get
            Return _DBPass
        End Get

        Set(ByVal Value As String)
            _DBPass = Value
        End Set
    End Property

    Public Property DBServer() As String
        Get
            Return _DBServer
        End Get

        Set(ByVal Value As String)
            _DBServer = Value
        End Set
    End Property

    Public Property DBName() As String
        Get
            Return _DBName
        End Get

        Set(ByVal Value As String)
            _DBName = Value
        End Set
    End Property

    Public Property DBPath() As String
        Get
            Return _DBPath
        End Get

        Set(ByVal Value As String)
            _DBPath = Value
        End Set
    End Property

    Public Sub OpenTree(ByVal BuddyControl As Object)


        Dim CtrlType As String

        CtrlType = TypeName(BuddyControl)

        If Not ((CtrlType = "TextBox") Or (CtrlType = "ComboBox") Or (CtrlType = "TextEdit") Or _
           (CtrlType = "DBGrid") Or (CtrlType = "DataGrid")) Then
            Exit Sub
        End If

        frm = New frmTree
        With frm
            .objTree = Me
            .BuddyControl = BuddyControl
            ._DBType = Me.DBType
            ._DBName = Me.DBName
            ._DBServer = Me.DBServer
            ._DBUName = Me.DBUName
            ._DBPass = Me.DBPass
            ._DBPath = Me.DBPath
            .SelectOnlyLastNode = Me.SelectOnlyLastNode
            .Width = CType(BuddyControl, Control).Width
            .Show()
        End With
    End Sub

End Class
