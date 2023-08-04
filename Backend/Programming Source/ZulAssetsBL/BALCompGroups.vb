Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALCompGroups
        Inherits BLBase
#Region "Data Members"
        Private objCompGroups As CompGroups
#End Region

        Public Sub New()
            objCompGroups = New CompGroups
        End Sub


#Region "Functions"
        Public Function Check_Child(ByVal _Id As String) As DataTable
            Try

                Return Me.GeneralExecuter(objCompGroups.Check_Child(_Id))
            Catch ex As Exception
                Throw ex
            End Try


        End Function
        Public Function Insert_CompGroups(ByVal objattCompGroups As attCompGroups) As String
            objCompGroups.Attribute = objattCompGroups
            Me.Insert(objCompGroups)
            Return Me.GetNextPKey(objCompGroups)
        End Function


        Public Function Delete_CompGroups(ByVal objattCompGroups As attCompGroups) As Boolean
            Try
                objCompGroups.Attribute = objattCompGroups
                Me.Delete(objCompGroups)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Update_CompGroups(ByVal objattCompGroups As attCompGroups) As Boolean
            Try
                objCompGroups.Attribute = objattCompGroups
                Me.Update(objCompGroups)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function Verify_Location(ByVal lvl As Integer, ByVal plvl As Integer) As Boolean
            Dim dt As DataTable = Me.GeneralExecuter(objCompGroups.Verify_Location(lvl, plvl))
            If dt Is Nothing = False Then
                If dt.Rows.Count > 0 Then Return False
            End If
            Return True
        End Function
        Public Function GetNextPKey_CompGroups() As String
            Return Me.GetNextPKey(objCompGroups)
        End Function

        Public Function GetNextPKey_CompGroupsByLevel(ByVal LevelID As Integer) As String
            Dim ID As String = Me.GeneralExecuter_Scalar(objCompGroups.GetNextPKey(LevelID), "")
            Return ID
        End Function


        Public Function GetAll_CompGroups(ByVal objattCompGroups As attCompGroups) As DataTable
            objCompGroups.Attribute = objattCompGroups
            Return GetAllData(objCompGroups)
        End Function

        Public Function GrpIdExist(ByVal objatt As attCompGroups, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("GrpId", objatt.PKeyCode, "CompGroups", IsInsertStatus, "GrpId", objatt.PKeyCode)
        End Function

        Public Function GrpDescExist(ByVal objatt As attCompGroups, ByVal IsInsertStatus As Boolean) As Boolean
            Return FieldValueExisted("GrpDesc", objatt.GrpDesc, "CompGroups", IsInsertStatus, "GrpId", objatt.PKeyCode)
        End Function
#End Region

    End Class
End Namespace
