Imports System
Imports System.Data.OleDb
Imports System.Configuration.ConfigurationSettings
Imports ZulAssetsDAL.ZulAssetsDAL

Namespace ZulAssetBAL

    Public Class BALGLCode
        Inherits BLBase

#Region "Data Members"
        Private objGLCode As GLCode
#End Region

        Public Sub New()
            objGLCode = New GLCode
        End Sub


#Region "Functions"
        Public Function GetAll_GLCodes(ByVal objattGLCode As attGLCode) As DataTable
            Try
                objGLCode.Attribute = objattGLCode
                Return GetAllData(objGLCode)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCompanyGLCodes(ByVal CompID As String) As DataTable
            Try
                Return Me.GeneralExecuter(objGLCode.GetGLCodesByCompany(CompID))
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Insert_GLCode(ByVal objattGLCode As attGLCode) As Boolean
            Try
                objGLCode.Attribute = objattGLCode
                Me.Insert(objGLCode)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function Update_GLCode(ByVal objattGLCode As attGLCode) As Boolean
            Try
                objGLCode.Attribute = objattGLCode
                Me.Update(objGLCode)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Delete_GLCode(ByVal objattGLCode As attGLCode) As Boolean
            Try
                objGLCode.Attribute = objattGLCode
                Me.Delete(objGLCode)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GLCodeDescExist(ByVal objattGLCode As attGLCode, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("GLDesc", objattGLCode.GLDesc, "GLCodes", IsInsertStatus, "GLCode", objattGLCode.PKeyCode)
        End Function

        Public Function GLCodePKExist(ByVal objattGLCode As attGLCode, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("GLCode", objattGLCode.PKeyCode, "GLCodes", IsInsertStatus, "GLCode", objattGLCode.PKeyCode)
        End Function

        Public Function GetNextPKey_GL() As String
            Try
                Return Me.GetNextPKey(objGLCode)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
    End Class
End Namespace

