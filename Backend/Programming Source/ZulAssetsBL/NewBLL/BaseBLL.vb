Imports GenericDAL

Public Class BaseBLL
    Inherits GenericDAL.Base

    Protected Sub AddTransLog(ByVal TransGUID As Guid, ByVal UserGUID As Guid, ByVal ActionType As String, ByVal TableName As String)
        'Dim Trans As New TransactionsLogBLL
        'Trans.NewRecord()
        'Trans.Attributes.ActionType = ActionType
        'Trans.Attributes.TransDate = Now
        'Trans.Attributes.LastEditDate = Now
        'Trans.Attributes.CreationDate = Now
        'Trans.Attributes.UserGUID = UserGUID
        'Trans.Attributes.TransGUID = TransGUID
        'Trans.Attributes.CreatedBy = UserGUID
        'Trans.Attributes.LastEditBY = UserGUID
        'Trans.Attributes.TableName = TableName
        'Trans.Save()
    End Sub

    Protected Function CheckPermission(ByVal BusinessLayerName As String, ByVal Action As String) As Boolean
        'Dim ObjUser As New UserBLL
        'Return ObjUser.CheckUserPermission(BusinessLayerName, Action, UserGUID)
        Return True
    End Function
End Class
