Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL

    Public Class BALCompLvl
        Inherits BLBase
#Region "Data Members"
        Private objCompLvl As CompLvl
#End Region


        Public Sub New()
            objCompLvl = New CompLvl
        End Sub

#Region "Functions"
        Public Function Insert_CompLvl(ByVal objattCompLvl As attCompLvl) As Boolean
            objCompLvl.Attribute = objattCompLvl
            Me.Insert(objCompLvl)
            Return True
        End Function

        Public Function Delete_CompLvl(ByVal objattCompLvl As attCompLvl) As Boolean
            Try
                objCompLvl.Attribute = objattCompLvl
                Me.Delete(objCompLvl)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function Update_CompLvl(ByVal objattCompLvl As attCompLvl) As Boolean
            Try
                objCompLvl.Attribute = objattCompLvl
                Me.Update(objCompLvl)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function GetNextPKey_CompLvl() As String
            Return Me.GetNextPKey(objCompLvl)
        End Function
        Public Function GetAll_CompLvl(ByVal objattCompLvl As attCompLvl) As DataTable
            objCompLvl.Attribute = objattCompLvl
            Return GetAllData(objCompLvl)
        End Function

        Public Function LvlDescExist(ByVal objatt As attCompLvl, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("LvlDesc", objatt.LvlDesc, "CompLvl", IsInsertStatus, "LvlID", objatt.PKeyCode)
        End Function

#End Region
    End Class

End Namespace

