Imports ZulAssetsDAL.ZulAssetsDAL

Namespace ZulAssetBAL
    Public Class BALAssetWarranty
        Inherits BLBase

#Region "Data Members"
        Private objAssetWarranty As AssetWarranty
#End Region
        Public Sub New()
            objAssetWarranty = New AssetWarranty
        End Sub

#Region "Functions"

        Public Function Insert_AssetWarranty(ByVal objattAssetWarranty As attAssetWarranty) As String
            Try
                objAssetWarranty.Attribute = objattAssetWarranty
                Me.Insert(objAssetWarranty)
                Return Me.GetNextPKey_AssetWarranty()
            Catch ex As Exception
                Throw ex
            End Try
            Return ""
        End Function

        Public Function Update_AssetWarranty(ByVal objattAssetWarranty As attAssetWarranty) As Boolean
            Try
                objAssetWarranty.Attribute = objattAssetWarranty
                Me.Update(objAssetWarranty)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_AssetWarranty() As String
            Try
                Return Me.GetNextPKey(objAssetWarranty)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateWarrantyStartDate(ByVal objattAssetWarranty As attAssetWarranty) As Boolean
            Try
                objAssetWarranty.Attribute = objattAssetWarranty
                Dim count As Integer = Me.GeneralExecuter_Scalar(objAssetWarranty.CheckWarrantyStartDate(), "")
                If count > 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_AssetWarranty(ByVal objattAssetWarranty As attAssetWarranty) As DataTable
            Try
                objAssetWarranty.Attribute = objattAssetWarranty
                Return GetAllData(objAssetWarranty)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Dismiss_AssetWarranty(ByVal ID As Int64) As Boolean
            Try
                Me.GeneralExecuter(objAssetWarranty.DismissWarranty(ID))
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Dismiss_AllAssetWarranty() As Boolean
            Try
                Me.GeneralExecuter(objAssetWarranty.DismissAllWarranty())
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_AlarmAssetWarranty(ByVal AlarmBeforeDays As Integer) As DataTable
            Try
                Return Me.GeneralExecuter(objAssetWarranty.GetAllAlarmData(AlarmBeforeDays))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Delete_AssetWarranty(ByVal objattAssetWarranty As attAssetWarranty) As Boolean
            Try
                objAssetWarranty.Attribute = objattAssetWarranty
                Me.Delete(objAssetWarranty)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Delete_AssetWarrantyByAstID(ByVal objattAssetWarranty As attAssetWarranty) As Boolean
            Try
                objAssetWarranty.Attribute = objattAssetWarranty
                Me.GeneralExecuter(objAssetWarranty.DeleteByAstID())
                Return True
            Catch ex As Exception
                Throw ex
            End Try
            Return False
        End Function

#End Region
    End Class

End Namespace
