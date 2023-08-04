Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALRoles
        Inherits BLBase

#Region "Data Members"
        Private objRoles As Roles
#End Region


        Public Sub New()
            objRoles = New Roles
        End Sub

#Region "Functions"
    
        Public Sub Update_Role_CompaniesID(ByVal CompID As String)
            Try
                Dim ds As DataTable
                Dim objatt As New attRoles
                ds = GetAll_Roles(objatt)
                For Each dr As DataRow In ds.Rows
                    Dim companiesID As String = dr("companies").ToString()
                    Dim RoleID As Integer = dr("RoleID").ToString()
                    Dim IDs() As String = companiesID.Split(",")
                    Dim newCompaniesID As String = ""
                    For Each id As String In IDs
                        If id <> CompID Then
                            newCompaniesID += id + ","
                        End If
                    Next
                    If Not String.IsNullOrEmpty(newCompaniesID) Then
                        newCompaniesID = newCompaniesID.Remove(newCompaniesID.Length - 1, 1) 'deleting the last comma character
                    End If
                    Me.GeneralExecuter(objRoles.Update_Role_Co(RoleID, newCompaniesID))
                Next
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub Update_Role_Company(ByVal RoleID As String, ByVal Comp As String)
            Try
                Me.GeneralExecuter(objRoles.Update_Role_Co(RoleID, Comp))
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub CreateDefaultRecord()
            Dim ds As DataTable
            Dim deletenw As Boolean = False
            Dim objatt As New attRoles
            ds = GetAll_Roles(objatt)
            If ds Is Nothing = False Then
                If ds Is Nothing = False Then
                    If ds.Rows.Count < 1 Then
                        If deletenw = False Then
                            Delete_Roles(objatt)
                            deletenw = True
                        End If
                        objatt = New attRoles
                        objatt.PKeyCode = "1"
                        objatt.AddressBook = 1
                        objatt.Anonym = 1
                        objatt.AppUser = 1
                        objatt.AssetItems = 1
                        objatt.AssetsBooks = 1
                        objatt.AssetsCat = 1
                        objatt.AstAdmin = 1
                        objatt.AstDetail = 1
                        objatt.AstSrch = 1
                        objatt.AstTrans = 1
                        objatt.BarCodePolicy = 1
                        objatt.BarStuct = 1
                        objatt.Brands = 1
                        objatt.Company = 1
                        objatt.CompanyInfo = 1
                        objatt.CostCenter = 1
                        objatt.Custodian = 1
                        objatt.DataRecieve = 1
                        objatt.DataSend = 1
                        objatt.Department = 1
                        objatt.DepMan = 1
                        objatt.DepPolicy = 1
                        objatt.DepreciationMethod = 1
                        objatt.Description = "Admin"
                        objatt.Designation = 1
                        objatt.DeviceConfig = 1
                        objatt.DisposalMethod = 1
                        objatt.Insurer = 1
                        objatt.InterComTrans = 1
                        objatt.InvSch = 1
                        objatt.Location = 1
                        objatt.PO = 1
                        objatt.POApproval = 1
                        objatt.POTrans = 1
                        objatt.Roles = 1
                        objatt.Units = 1
                        objatt.Supplier = 1
                        objatt.AssetCoding = 1
                        objatt.SysConfig = 1
                        Insert_Roles(objatt)
                    End If
                End If
            End If

        End Sub

        Public Function Insert_Roles(ByVal objattDept As attRoles) As Boolean
            Try

                objRoles.Attribute = objattDept
                Me.Insert(objRoles)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
            Return ""
        End Function
        Public Function Getcombo_Roles() As DataTable
            Try
                Return Me.GeneralExecuter(objRoles.GetARole_Combo())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Update_Roles(ByVal objattDept As attRoles) As Boolean
            Try
                objRoles.Attribute = objattDept
                Me.Update(objRoles)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_Roles() As String
            Try
                Return Me.GetNextPKey(objRoles)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAll_Roles(ByVal objattDept As attRoles) As DataTable
            Try
                objRoles.Attribute = objattDept
                Return GetAllData(objRoles)

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function Delete_Roles(ByVal objattDept As attRoles) As Boolean
            Try
                objRoles.Attribute = objattDept
                Me.Delete(objRoles)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function RoleExist(ByVal objatt As attRoles, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("Description", objatt.Description, "Roles", IsInsertStatus, "RoleID", objatt.PKeyCode)
        End Function
#End Region
    End Class

End Namespace

