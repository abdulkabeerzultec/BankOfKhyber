Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALHierGrps
        Inherits BLBase
#Region "Data Members"
        Private objHierGrps As HierGrps
#End Region


        Public Sub New()
            objHierGrps = New HierGrps
        End Sub

#Region "Functions"
        Public Sub Insert_HierGrp(ByVal objattHierGrps As attHierGrps)
            Try
                objHierGrps.Attribute = objattHierGrps
                Me.Insert(objHierGrps)

            Catch ex As Exception
                Throw ex
            End Try

        End Sub


        Public Function Update_HierGrp(ByVal objattHierGrps As attHierGrps) As Boolean
            Try
                objHierGrps.Attribute = objattHierGrps
                Me.Update(objHierGrps)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_HierGrp(ByVal objattHierGrps As attHierGrps) As DataTable
            Try
                objHierGrps.Attribute = objattHierGrps
                Return GetAllData(objHierGrps)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region

    End Class
End Namespace

