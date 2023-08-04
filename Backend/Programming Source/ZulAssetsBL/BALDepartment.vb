
Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALDepartment
        Inherits BLBase

#Region "Data Members"
        Private objDepartment As Department
#End Region


        Public Sub New()
            objDepartment = New Department
        End Sub

#Region "Functions"
        Public Function Insert_Brand(ByVal objattDept As attDepartment) As String
            Try
                objDepartment.Attribute = objattDept
                Me.Insert(objDepartment)
                Return Me.GetNextPKey_Department()
            Catch ex As Exception
                Throw ex
            End Try
            Return ""
        End Function


        Public Function Update_Department(ByVal objattDept As attDepartment) As Boolean
            Try
                objDepartment.Attribute = objattDept
                Me.Update(objDepartment)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_Department() As String
            Try
                Return Me.GetNextPKey(objDepartment)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAll_Department(ByVal objattDept As attDepartment) As DataTable
            Try
                objDepartment.Attribute = objattDept
                Return GetAllData(objDepartment)

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function Delete_Department(ByVal objattDept As attDepartment) As Boolean
            Try
                objDepartment.Attribute = objattDept
                Me.Delete(objDepartment)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
    End Class

End Namespace