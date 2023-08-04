Imports ZulAssetsBL.ZulAssetBAL
Imports ZulAssetsDAL.ZulAssetsDAL
Module General
    Public Function Generate_AssetNumber(ByVal ComPID As String) As String
        Dim AstNum As Long = 0
        Dim objBALAssetDetails As New BALAssetDetails
        If AppConfig.CodingMode Then ' Use Assets Coding Definition
            If ComPID <> "" Then
                Dim StartRange, EndRange As Long
                StartRange = 0
                EndRange = 0
                Dim ds As New DataTable
                Dim objattAssetsCoding As New attAssetsCoding
                Dim objBALAssetsCoding As New BALAssetsCoding
                objattAssetsCoding.CompanyID = ComPID
                objattAssetsCoding.Status = True
                ds = objBALAssetsCoding.GetAll_AssetCoding(objattAssetsCoding)
                If ds Is Nothing = False Then
                    If ds.Rows.Count > 0 Then
                        StartRange = CLng(ds.Rows(0)("StartSerial"))
                        EndRange = CLng(ds.Rows(0)("EndSerial"))
                    End If
                End If
                Dim objBALCompany As New BALCompany
                If EndRange <> 0 And StartRange <> 0 Then
                    'get the AssetNumber from company last AssetNumber
                    Dim LastAssetNumber As Int64 = objBALCompany.GetCompanyLastAssetNumber(ComPID)
                    If LastAssetNumber >= StartRange Then
                        AstNum = LastAssetNumber + 1
                    Else
                        AstNum = StartRange
                    End If

                    If EndRange < AstNum Then
                        objBALAssetsCoding.CloseRange(objattAssetsCoding)
                        Return -1
                    End If
                Else
                    Return -1
                End If

            End If
        Else 'Use Incremental Coding

            Dim intID As Integer = objBALAssetDetails.GetNextPKey_AssetDetails()
            AstNum = intID + 1
        End If
        Return AstNum
    End Function
End Module
