Imports System
Imports ZulAssetsDAL.ZulAssetsDAL

Namespace ZulAssetBAL
    Public Class BALSysParam
        Inherits BLBase

        'This function should be called in the begging of the application to load the parameters data into the application.
        Public Function ReadSysParameters() As Boolean
            Try
                Dim Temp As String = Me.GeneralExecuter_Scalar(SysParam.GetParamByName("MaxLocationLevel"), "").ToString
                If Temp <> String.Empty Then
                    SysParam.MaxLocationLevel = Temp
                Else
                    SysParam.MaxLocationLevel = 5 'Default Value
                End If

                Temp = Me.GeneralExecuter_Scalar(SysParam.GetParamByName("MaxCategoryLevel"), "").ToString
                If Temp <> String.Empty Then
                    SysParam.MaxCategoryLevel = Temp
                Else
                    SysParam.MaxCategoryLevel = 4 'Default Value
                End If

                Temp = Me.GeneralExecuter_Scalar(SysParam.GetParamByName("AssetCategoryMinlevel"), "").ToString
                If Temp <> String.Empty Then
                    SysParam.AssetCategoryMinlevel = Temp
                Else
                    SysParam.AssetCategoryMinlevel = 1 'Default Value
                End If

                Temp = Me.GeneralExecuter_Scalar(SysParam.GetParamByName("AssetlocationMinlevel"), "").ToString
                If Temp <> String.Empty Then
                    SysParam.AssetlocationMinlevel = Temp
                Else
                    SysParam.AssetlocationMinlevel = 1 'Default Value
                End If
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        'This function to be called when clicking save .
        Public Function SaveSysParameters() As Boolean
            Try
                Me.GeneralExecuter(SysParam.SaveParamValue("MaxLocationLevel", SysParam.MaxLocationLevel))
                Me.GeneralExecuter(SysParam.SaveParamValue("MaxCategoryLevel", SysParam.MaxCategoryLevel))
                Me.GeneralExecuter(SysParam.SaveParamValue("AssetCategoryMinlevel", SysParam.AssetCategoryMinlevel))
                Me.GeneralExecuter(SysParam.SaveParamValue("AssetlocationMinlevel", SysParam.AssetlocationMinlevel))
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
    End Class
End Namespace
