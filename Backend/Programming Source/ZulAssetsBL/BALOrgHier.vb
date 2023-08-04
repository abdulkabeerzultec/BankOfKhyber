

Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALOrgHier
        Inherits BLBase
#Region "Data Members"
        Private objOrgHier As OrgHier
#End Region

        Public Sub New()
            objOrgHier = New OrgHier
        End Sub


#Region "Functions"
        Public Function Check_Child_Custodian(ByVal _id As String) As Boolean
            Try
                Dim dt As DataTable
                dt = Me.GeneralExecuter(objOrgHier.Check_Child_Custodian(_id))
                If dt Is Nothing = False Then
                    If dt.Rows.Count > 0 Then
                        Return True
                    End If
                End If
                Return False

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Check_Child(ByVal _id As String) As DataTable
            Try
                Return Me.GeneralExecuter(objOrgHier.Check_Child(_id))
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function Insert_OrgHier(ByVal objattOrgHier As attOrgHier) As Boolean
            objOrgHier.Attribute = objattOrgHier
            Me.Insert(objOrgHier)
            Return True
        End Function
        Public Function HierName(ByVal HierCode As String) As String
            Dim arr() As String
            Dim dr As IDataReader = Nothing
            Dim ds As DataTable = Nothing
            Dim drCollection As DataRow() = Nothing
            Dim drow As DataRow = Nothing
            Dim qry As String = ""
            Dim i As Integer
            Dim HStr As String = ""
            Dim objattCompGroups As New attCompGroups
            Dim objBALCompGroups As New BALCompGroups

            If InStr(HierCode, "-") = 0 Then

                Try
                    objattCompGroups.PKeyCode = HierCode
                    ds = objBALCompGroups.GetAll_CompGroups(objattCompGroups)
                    If ds Is Nothing = False Then
                        If ds Is Nothing = False Then
                            If ds.Rows.Count > 0 Then
                                Return ds.Rows(0)("GrpDesc")
                            End If
                        End If
                    End If
                    Return ""
                Catch ex As Exception

                End Try

            Else

                Try
                    objattCompGroups = New attCompGroups
                    ds = objBALCompGroups.GetAll_CompGroups(objattCompGroups)
                Catch ex As Exception

                    HierName = ""
                    Exit Function
                Finally
                End Try
                arr = Split(HierCode, "-")
                For i = 0 To arr.GetUpperBound(0)
                    drCollection = ds.Select("GrpID='" & arr(i) & "'")
                    For Each drow In drCollection
                        HStr = HStr & "-" & drow("GrpDesc").ToString
                    Next
                Next
                ds.Dispose()
                If Mid(HStr, 1, 1) = "-" Then HStr = Mid(HStr, 2)
                Return HStr
            End If
            Return ""
        End Function
        Public Function Delete_OrgHier(ByVal objattOrgHier As attOrgHier) As Boolean
            Try
                objOrgHier.Attribute = objattOrgHier
                Me.Delete(objOrgHier)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function Update_OrgHier(ByVal objattOrgHier As attOrgHier) As Boolean
            Try
                objOrgHier.Attribute = objattOrgHier
                Me.Update(objOrgHier)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function GetNextPKey_OrgHier() As String
            Return Me.GetNextPKey(objOrgHier)
        End Function
        Public Function GetAll_OrgHier(ByVal objattOrgHier As attOrgHier) As DataTable
            objOrgHier.Attribute = objattOrgHier
            Return GetAllData(objOrgHier)
        End Function

#End Region

    End Class
End Namespace

