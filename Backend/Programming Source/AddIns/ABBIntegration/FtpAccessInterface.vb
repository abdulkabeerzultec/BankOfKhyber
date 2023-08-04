Public Interface FtpAccessInterface

    Sub ErrorHandle(ByVal errString As String, ByVal Row As Integer)
    Sub UpdateUIHandler(ByVal Rowupdation As Integer, ByVal flag As Integer, ByVal FileName As String)

    Function ReadFile(ByVal FileName As String, ByVal FileWatch As Boolean) As String

    ReadOnly Property grdDevicesI() As DevExpress.XtraGrid.Views.Grid.GridView
End Interface
