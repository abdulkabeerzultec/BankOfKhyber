Imports ZulAssetsDAL.ZulAssetsDAL

Namespace ZulAssetBAL
    Public Class BALABBIntegration
        Inherits BLBase
        Private objIntegrationQuery As New ABBIntegration
#Region "Goods Receving"
        Public Function GetInvProposalEmpList(ByVal InvNumber As String) As DataTable
            Try
                Return Me.GeneralExecuter(objIntegrationQuery.GetInvProposalEmpList(InvNumber))
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetInvProposalAssetsList(ByVal InvNumber As String) As DataTable
            Try
                Return Me.GeneralExecuter(objIntegrationQuery.GetInvProposalAssetsList(InvNumber))
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
        Public Function GetAllData_ABBExportGrid() As DataTable
            Try
                Return Me.GeneralExecuter(objIntegrationQuery.GetChangedDataABBExport())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetBusinessAreaList() As DataTable
            Try
                Return Me.GeneralExecuter(objIntegrationQuery.GetBusinessAreaList())
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function GetEvaluationGroup1List() As DataTable
            Try
                Return Me.GeneralExecuter(objIntegrationQuery.GetEvaluationGroup1List())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPhysicalInventory_ABBExportGrid() As DataTable
            Try
                Return Me.GeneralExecuter(objIntegrationQuery.GetPhysicalInventoryABBExport())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAssetABBLov() As DataTable
            Try
                Return Me.GeneralExecuter(objIntegrationQuery.GetAssetABBList())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAssetAdminGridABB(ByVal objattAssetDetails As attAssetDetails) As DataTable
            Try
                Return Me.GeneralExecuter(objIntegrationQuery.GetAssetsAdministartionGridABB(objattAssetDetails))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAssetLogGridABB(ByVal AssetID As String) As DataTable
            Try
                Return Me.GeneralExecuter(objIntegrationQuery.GetAssetsLog1ABB(AssetID))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAssetLifeByRefNumber(ByVal RefNo As String) As String
            Try
                Return Me.GeneralExecuter_Scalar(objIntegrationQuery.GetAssetLifeByRefNo(RefNo), "")
            Catch ex As Exception
                Return False
            End Try
        End Function


        Public Function AssetsLogReport(ByVal Filter As ABBIntegration.FilterOptions) As DataTable
            Try
                Return Me.GeneralExecuter(objIntegrationQuery.AssetsLogReport(Filter))
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function CompanyAssetReportABB(ByVal Filter As ABBIntegration.FilterOptions) As DataTable
            Try
                Return Me.GeneralExecuter(objIntegrationQuery.CompanyAssetReportABB(Filter))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_AnonymousAsset() As DataTable
            Try
                Return Me.GeneralExecuterTemp(objIntegrationQuery.GetAllData_Anonymous)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_AnonymousAssetRemarks(ByVal InvSchID As String) As DataTable
            Try
                Return Me.GeneralExecuterTemp(objIntegrationQuery.GetAllData_AnonymousRemarks(InvSchID))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_AssetsDataProcessingGrid() As DataTable
            Try

                Return Me.GeneralExecuterTemp(objIntegrationQuery.GetAll_DataProcessing())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_Classes() As DataTable
            Try

                Return Me.GeneralExecuter(objIntegrationQuery.GetAllClasses())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

   

    End Class
End Namespace