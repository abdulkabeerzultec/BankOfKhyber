Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL


Namespace ZulAssetBAL
    Public Class BALCategory
        Inherits BLBase

#Region "Data Members"
        Private objCategory As Category
#End Region


        Public Sub New()
            objCategory = New Category
        End Sub

#Region "Functions"
        Public Function Insert_Category(ByVal objattCategory As attCategory) As Boolean
            Try
                objattCategory.PKeyCode = GetNextPKey_Category()
                objCategory.Attribute = objattCategory
                Me.Insert(objCategory)
                objattCategory.Compcode = Me.Comp_Path_Code(objattCategory.AstCatID)
                objattCategory.CompleteCatDesc = Me.Comp_Path(objattCategory.AstCatID)
                Me.Update(objCategory)
                Return True
            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Public Function Update_Category(ByVal objattCategory As attCategory) As Boolean
            Try
                objCategory.Attribute = objattCategory
                'update the Category info because we can't calc the complete path if the value not saved.
                Me.Update(objCategory)
                objattCategory.Compcode = Me.Comp_Path_Code(objattCategory.AstCatID)
                objattCategory.CompleteCatDesc = Me.Comp_Path(objattCategory.AstCatID)
                'update the full path of the Category.
                Me.Update(objCategory)

                'update child Category full path.
                Update_ChildCategoryPath(objattCategory.AstCatID)

                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Update_ChildCategoryPath(ByVal AstCatID As String) As Boolean
            Dim objattCategory As attCategory
            Dim dtCat As DataTable = GeneralExecuter(objCategory.GetAllChildCategory(AstCatID))
            For Each row As DataRow In dtCat.Rows
                objattCategory = New attCategory
                objattCategory.AstCatDesc = row("AstCatDesc")
                objattCategory.AstCatID = row("AstCatID")
                objattCategory.Code = row("Code")
                objattCategory.catLevel = row("catLevel")
                objattCategory.Compcode = Me.Comp_Path_Code(objattCategory.AstCatID)
                objattCategory.CompleteCatDesc = Me.Comp_Path(objattCategory.AstCatID)
                objCategory.Attribute = objattCategory
                Me.Update(objCategory)
            Next
        End Function

        Public Function GetAll_Category(ByVal objattCategory As attCategory) As DataTable
            Try
                objCategory.Attribute = objattCategory
                Return GetAllData(objCategory)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetNextPKey_Category() As String
            Try
                Return Me.GetNextPKey(objCategory)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Delete_Category(ByVal objattCategory As attCategory) As Boolean
            Try
                Dim obj As New BALDepPolicy
                Dim objatt As New attDepPolicy
                objCategory.Attribute = objattCategory
                objatt.AstCatID = objattCategory.AstCatID
                obj.Delete_DepPolicy(objatt)
                Me.Delete(objCategory)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCatChild(ByVal id As String) As DataTable
            Try
                Return Me.GeneralExecuter(objCategory.GetChildsID(id))
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetCatID(ByVal Code As String) As String
            Try
                Return Me.GeneralExecuter_Scalar(objCategory.Get_AstCatIdByCode(Code), "")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCatRootID() As DataTable
            Try
                Return Me.GeneralExecuter(objCategory.GetRootID())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetCatIDByDesc(ByVal Desc As String, ByVal MainCatID As String, Optional ByVal level As Integer = -1) As String
            Try
                Dim dt As DataTable = Me.GeneralExecuter(objCategory.Get_IDByDesc(Desc, MainCatID, level))
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0)(0).ToString()
                Else
                    Return ""
                End If
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetCatFullPath(ByVal CatID As String) As String
            Try
                Return Me.GeneralExecuter_Scalar(objCategory.Get_FullCategory(CatID), "")
            Catch ex As Exception
                Throw ex
            End Try

        End Function



        Public Function Comp_Path(ByVal id As String) As String
            Dim idx() As String = id.Split("-")
            Dim Count As Integer = idx.Length
            Dim Cur, CurID, Desc As String
            Desc = ""
            CurID = ""
            Cur = id
            Dim ds As DataTable
            For indx As Integer = 0 To idx.Length - 1
                If id.IndexOf("-") > 0 Then
                    Cur = id.Substring(0, id.IndexOf("-"))
                    id = id.Substring(id.IndexOf("-") + 1, id.Length - (id.IndexOf("-") + 1))
                Else
                    Cur = id
                End If

                If indx <> 0 Then
                    Desc = Desc + " \ "
                    CurID = CurID + "-" + Cur
                Else
                    CurID = Cur
                End If
                ds = Me.GeneralExecuter(objCategory.Get_Desc(CurID))
                If Not ds Is Nothing Then
                    If ds.Rows.Count > 0 Then
                        Desc = Desc + ds.Rows(0)(0).ToString()
                    End If
                End If
            Next
            Return Desc
        End Function
        Public Function Comp_Path_Code(ByVal id As String) As String
            Dim idx() As String = id.Split("-")
            Dim Count As Integer = idx.Length
            Dim Cur, CurID, Desc As String
            Desc = ""
            CurID = ""
            Cur = id
            Dim ds As DataTable
            For indx As Integer = 0 To idx.Length - 1
                If id.IndexOf("-") > 0 Then
                    Cur = id.Substring(0, id.IndexOf("-"))
                    id = id.Substring(id.IndexOf("-") + 1, id.Length - (id.IndexOf("-") + 1))
                Else
                    Cur = id
                End If

                If indx <> 0 Then
                    Desc = Desc + " \ "
                    CurID = CurID + "-" + Cur
                Else
                    CurID = Cur
                End If
                ds = Me.GeneralExecuter(objCategory.Get_Code(CurID))
                If Not ds Is Nothing Then
                    If ds.Rows.Count > 0 Then
                        Desc = Desc + ds.Rows(0)(0).ToString()
                    End If
                End If

            Next
            Return Desc
        End Function

#End Region
        Public Function Move_Down(ByVal CurNodeId As String, ByVal NextNodeID As String) As Boolean
            Try
                Dim idxCurrent, idxPrevious As Integer
                Dim dt As New DataTable

                dt = Me.GeneralExecuter(objCategory.Get_Index(CurNodeId))
                If dt Is Nothing = False Then
                    If dt.Rows.Count > 0 Then
                        idxCurrent = dt.Rows(0)("id1")
                    End If
                End If

                dt = New DataTable
                dt = Me.GeneralExecuter(objCategory.Get_Index(NextNodeID))
                If dt Is Nothing = False Then
                    If dt.Rows.Count > 0 Then
                        idxPrevious = dt.Rows(0)("id1")
                    End If
                End If
                Me.TransactionExecuter(objCategory.UpdateIndex(CurNodeId, idxPrevious))
                Me.TransactionExecuter(objCategory.UpdateIndex(NextNodeID, idxCurrent))
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function Move_Up(ByVal CurNodeId As String, ByVal PreNodeID As String) As Boolean
            Try
                Dim idxCurrent, idxPrevious As Integer
                Dim dt As New DataTable

                dt = Me.GeneralExecuter(objCategory.Get_Index(CurNodeId))
                If dt Is Nothing = False Then
                    If dt.Rows.Count > 0 Then
                        idxCurrent = dt.Rows(0)("id1")
                    End If
                End If

                dt = New DataTable
                dt = Me.GeneralExecuter(objCategory.Get_Index(PreNodeID))
                If dt Is Nothing = False Then
                    If dt.Rows.Count > 0 Then
                        idxPrevious = dt.Rows(0)("id1")
                    End If
                End If
                Me.TransactionExecuter(objCategory.UpdateIndex(CurNodeId, idxPrevious))
                Me.TransactionExecuter(objCategory.UpdateIndex(PreNodeID, idxCurrent))
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetRootCatID() As String
            Try
                Dim ds As New DataTable
                ds = Me.GetCatRootID
                If ds Is Nothing = False Then
                    If ds.Rows.Count > 0 Then
                        If ds.Rows(0)(0).ToString = "" Then
                            Return "1"
                        Else
                            Return ds.Rows(0)(0).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return 1
        End Function

        Public Function GetChildCatID(ByVal parentID As String) As String
            Try
                Dim ds As New DataTable
                ds = Me.GetCatChild(parentID)
                Dim maxID As Integer = 0
                If ds Is Nothing = False Then
                    If ds.Rows.Count > 0 Then
                        For Each dr As DataRow In ds.Rows
                            Dim strarr As String() = dr(0).ToString.Split("-")
                            If strarr(strarr.Length - 1) > maxID Then
                                maxID = strarr(strarr.Length - 1)
                            End If
                        Next
                        Return parentID & "-" & maxID + 1
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return parentID & "-1"
        End Function

        Public Function CheckCatNameFound(ByVal CatName As String, ByVal ParentID As String, ByVal CatID As String) As Integer
            Try
                Return Me.GeneralExecuter_Scalar(objCategory.CheckCatNameFound(CatName, ParentID, CatID), "")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CheckCatCodeFound(ByVal CatCode As String, ByVal ParentID As String, ByVal CatID As String) As Integer
            Try
                Return Me.GeneralExecuter_Scalar(objCategory.CheckCatCodeFound(CatCode, ParentID, CatID), "")
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function GetCategoryList() As DataTable
            Try
                Return Me.GeneralExecuter(objCategory.GetCategoryList())
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class


End Namespace