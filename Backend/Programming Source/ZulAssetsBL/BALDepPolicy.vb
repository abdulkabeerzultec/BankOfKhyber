

Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL



Namespace ZulAssetBAL
    Public Class BALDepPolicy
        Inherits BLBase

#Region "Data Members"
        Private objDepPolicy As DepPolicy
#End Region


        Public Sub New()
            objDepPolicy = New DepPolicy
        End Sub

#Region "Functions"
        Public Sub Inherit_Policy(ByVal CatID As String)

            Try
                If CatID.IndexOf("-") > 0 Then


                    Dim ds As New DataTable

                    Dim CatParentId As String = CatID.Substring(0, (CatID.LastIndexOf("-")))
                    Dim objBALDepPolicy As New BALDepPolicy
                    Dim objattDepPolicy As New ZulAssetsDAL.ZulAssetsDAL.attDepPolicy
                    objattDepPolicy.AstCatID = CatParentId
                    ds = objBALDepPolicy.GetAll_DepPolicy(objattDepPolicy)

                    If ds Is Nothing = False Then
                        'If ds.Tables.Count > 0 Then
                        If ds.Rows.Count > 0 Then
                            objattDepPolicy.DepCode = ds.Rows(0)("DepCode").ToString()
                            objattDepPolicy.SalvageValue = ds.Rows(0)("SalvageValue").ToString()
                            objattDepPolicy.SalvageYear = ds.Rows(0)("SalvageYear").ToString()
                            objattDepPolicy.SalvageMonth = ds.Rows(0)("SalvageMonth").ToString()
                            objattDepPolicy.SalvagePercent = ds.Rows(0)("SalvagePercent").ToString()
                            objattDepPolicy.AstCatID = CatID
                            Insert_DepPolicy(objattDepPolicy)

                        End If
                    End If
                Else
                    Dim objBALDepPolicy As New BALDepPolicy
                    Dim objattDepPolicy As New ZulAssetsDAL.ZulAssetsDAL.attDepPolicy
                    objattDepPolicy.DepCode = 1           ' Put these things in System Setting 
                    objattDepPolicy.SalvageValue = 0
                    objattDepPolicy.SalvageMonth = 0
                    objattDepPolicy.SalvageYear = 5
                    objattDepPolicy.AstCatID = CatID
                    Insert_DepPolicy(objattDepPolicy)

                End If
            Catch ex As Exception
                Throw ex
            End Try



        End Sub

        Public Sub Insert_DepPolicy(ByVal objattDepPolicy As attDepPolicy)
            Try
                objDepPolicy.Attribute = objattDepPolicy
                Me.Insert(objDepPolicy)

            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Public Function CheckID(ByVal objattDepPolicy As attDepPolicy) As DataTable
            Try
                objDepPolicy.Attribute = objattDepPolicy
                Return Me.GeneralExecuter(objDepPolicy.CheckID())
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Update_DepPolicy(ByVal objattDepPolicy As attDepPolicy) As Boolean
            Try
                objDepPolicy.Attribute = objattDepPolicy
                Me.Update(objDepPolicy)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_DepPolicy(ByVal objattDepPolicy As attDepPolicy) As DataTable
            Try
                objDepPolicy.Attribute = objattDepPolicy
                Return GetAllData(objDepPolicy)

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Delete_DepPolicy(ByVal objattDepPolicy As attDepPolicy) As Boolean
            Try
                objDepPolicy.Attribute = objattDepPolicy
                Me.Delete(objDepPolicy)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
    End Class

End Namespace