Imports System
Imports System.Data.OleDb
Imports System.Configuration.ConfigurationSettings
Imports ZulAssetsDAL.ZulAssetsDAL

Namespace ZulAssetBAL

    Public Class BALLocation
        Inherits BLBase

#Region "Data Members"
        Private objLocation As Locations
#End Region

        Public Sub New()
            objLocation = New Locations
        End Sub


#Region "Functions"
        Public Function Insert_Location(ByVal objattLocation As attLocation) As Boolean
            Try
                objattLocation.PKeyCode = GetNextPKey_Location()
                objLocation.Attribute = objattLocation
                Me.Insert(objLocation)
                objattLocation.Compcode = Comp_Path_Code(objattLocation.HierCode)
                objattLocation.CompleteLocationDesc = Comp_Path(objattLocation.HierCode)
                Me.Update(objLocation)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function Update_Location(ByVal objattLocation As attLocation) As Boolean
            Try
                objLocation.Attribute = objattLocation
                'update the location info because we can't calc the complete path if the value not saved.
                Me.Update(objLocation)

                objattLocation.Compcode = Comp_Path_Code(objattLocation.HierCode)
                objattLocation.CompleteLocationDesc = Comp_Path(objattLocation.HierCode)
                'update the full path of the location.
                Me.Update(objLocation)

                'update child location full path.
                Update_ChildLocationPath(objattLocation.HierCode)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Update_ChildLocationPath(ByVal LocID As String) As Boolean
            Dim objattLocation As attLocation
            Dim dtLocations As DataTable = GeneralExecuter(objLocation.GetAllChildLocations(LocID))
            For Each row As DataRow In dtLocations.Rows
                objattLocation = New attLocation
                objattLocation.Description = row("LocDesc")
                objattLocation.HierCode = row("LocID")
                objattLocation.Code = row("Code")
                objattLocation.locLevel = row("LocLevel")
                objattLocation.CompanyID = row("CompanyID")
                objattLocation.Compcode = Comp_Path_Code(objattLocation.HierCode)
                objattLocation.CompleteLocationDesc = Comp_Path(objattLocation.HierCode)
                objLocation.Attribute = objattLocation
                Me.Update(objLocation)
            Next
        End Function


        Public Function Delete_Location(ByVal objattLocation As attLocation) As Boolean
            Try
                objLocation.Attribute = objattLocation
                Me.Delete(objLocation)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function GetComboLocations(ByVal objattLocation As attLocation) As DataTable
            Try
                objLocation.Attribute = objattLocation
                Return Me.GeneralExecuter(objLocation.GetComboData())
            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Public Function GetComboLocationsSortedByCode(ByVal objattLocation As attLocation) As DataTable
            Try
                objLocation.Attribute = objattLocation
                Return Me.GeneralExecuter(objLocation.GetComboDataSorted())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetAll_Locations(ByVal objattLocation As attLocation) As DataTable
            Try
                objLocation.Attribute = objattLocation
                Return GetAllData(objLocation)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetNextPKey_Location() As String
            Try
                Return Me.GetNextPKey(objLocation)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetLocationLabelData(ByVal id As String, ByVal IncludeChilds As Boolean) As DataTable
            Try
                Return Me.GeneralExecuter(objLocation.GetLocationLabelData(id, IncludeChilds))
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetLocChild(ByVal id As String) As DataTable
            Try
                Return Me.GeneralExecuter(objLocation.GetChildsID(id))
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetLocID(ByVal Code As String) As String
            Try
                Return Me.GeneralExecuter_Scalar(objLocation.Get_ID(Code), "")
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetLocIDByCompleteCode(ByVal Code As String) As String
            Try
                Return Me.GeneralExecuter_Scalar(objLocation.GetLocIDByCompleteCode(Code), "")
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetLocIDByCompleteDesc(ByVal Desc As String) As String
            Try
                Return Me.GeneralExecuter_Scalar(objLocation.GetLocIDByCompleteDesc(Desc), "")
            Catch ex As Exception
                Throw ex
            End Try

        End Function



        Public Function GetLocDescByID(ByVal ID As String) As String
            Try
                Return Me.GeneralExecuter_Scalar(objLocation.Get_Desc(ID), "")
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetLocFullPath(ByVal LocID As String) As String
            Try
                Return Me.GeneralExecuter_Scalar(objLocation.Get_FullLocation(LocID), "")
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GetLocCompCode(ByVal LocID As String) As String
            Try
                Return Me.GeneralExecuter_Scalar(objLocation.Get_LocCompCode(LocID), "")
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function GetLocRootID() As DataTable
            Try
                Return Me.GeneralExecuter(objLocation.GetRootID())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        'Public Function GetNBCodeReport(ByVal objattLocation As attLocation) As DataTable
        '    Try
        '        objLocation.Attribute = objattLocation
        '        Return Me.GeneralExecuter(objLocation.GetNBCodeReport())
        '    Catch ex As Exception
        '        Throw ex
        '    End Try

        'End Function
        Public Function Move_Down(ByVal CurNodeId As String, ByVal NextNodeID As String) As Boolean
            Try
                Dim idxCurrent, idxPrevious As Integer
                Dim dt As New DataTable

                dt = Me.GeneralExecuter(objLocation.Get_Index(CurNodeId))
                If dt Is Nothing = False Then
                    If dt.Rows.Count > 0 Then
                        idxCurrent = dt.Rows(0)("id1")
                    End If
                End If

                dt = New DataTable
                dt = Me.GeneralExecuter(objLocation.Get_Index(NextNodeID))
                If dt Is Nothing = False Then
                    If dt.Rows.Count > 0 Then
                        idxPrevious = dt.Rows(0)("id1")
                    End If
                End If
                Me.TransactionExecuter(objLocation.UpdateIndex(CurNodeId, idxPrevious))
                Me.TransactionExecuter(objLocation.UpdateIndex(NextNodeID, idxCurrent))
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function Move_Up(ByVal CurNodeId As String, ByVal PreNodeID As String) As Boolean
            Try
                Dim idxCurrent, idxPrevious As Integer
                Dim dt As New DataTable

                dt = Me.GeneralExecuter(objLocation.Get_Index(CurNodeId))
                If dt Is Nothing = False Then
                    If dt.Rows.Count > 0 Then
                        idxCurrent = dt.Rows(0)("id1")
                    End If
                End If

                dt = New DataTable
                dt = Me.GeneralExecuter(objLocation.Get_Index(PreNodeID))
                If dt Is Nothing = False Then
                    If dt.Rows.Count > 0 Then
                        idxPrevious = dt.Rows(0)("id1")
                    End If
                End If
                Me.TransactionExecuter(objLocation.UpdateIndex(CurNodeId, idxPrevious))
                Me.TransactionExecuter(objLocation.UpdateIndex(PreNodeID, idxCurrent))
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
                ds = Me.GeneralExecuter(objLocation.Get_Desc(CurID))
                If Not ds Is Nothing Then
                    If ds.Rows.Count > 0 Then
                        Desc = Desc + ds.Rows(0)(0).ToString()
                    End If
                End If
            Next
            Return Desc
        End Function

        Public Function Comp_Path_OrgHier(ByVal id As String) As String
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
                    CurID = Cur
                Else
                    CurID = Cur
                End If
                ds = Me.GeneralExecuter(objLocation.Get_Desc_OrgHier(CurID))
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

                ds = Me.GeneralExecuter(objLocation.Get_Code(CurID))
                If Not ds Is Nothing Then
                    If ds.Rows.Count > 0 Then
                        Desc = Desc + ds.Rows(0)(0).ToString()
                    End If
                End If

            Next
            Return Desc
        End Function

        Public Function GetRootLocID() As String
            Try
                Dim ds As New DataTable
                ds = Me.GetLocRootID
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

        Public Function GetChildLocID(ByVal parentID As String) As String
            Try
                Dim ds As New DataTable
                ds = Me.GetLocChild(parentID)
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
        Public Function CheckLocNameFound(ByVal LocName As String, ByVal LocParent As String, ByVal LocID As String) As Integer
            Try
                Return Me.GeneralExecuter_Scalar(objLocation.CheckLocNameFound(LocName, LocParent, LocID), "")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CheckLocCodeFound(ByVal LocCode As String, ByVal LocParent As String, ByVal LocID As String) As Integer
            Try
                Return Me.GeneralExecuter_Scalar(objLocation.CheckLocCodeFound(LocCode, LocParent, LocID), "")
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

    End Class
End Namespace

