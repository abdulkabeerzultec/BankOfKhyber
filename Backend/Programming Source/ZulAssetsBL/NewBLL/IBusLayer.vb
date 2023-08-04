Public Interface IBusLayer
    Function GetData(Optional ByRef msg As String = "") As DataTable
    Function GetListData(Optional ByRef msg As String = "") As DataTable
    Sub NewRecord()
    Function Edit(ByVal rowGUID As Guid) As String
    Function Save() As String
    Function DeleteByRowGUID(ByVal rowGUID As Guid) As String
End Interface
