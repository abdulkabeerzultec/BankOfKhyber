Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class AlHadaIntegration

#Region "Data Members"

        Private objattAssetDetails As attAssetDetails
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattAssetDetails = New attAssetDetails

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute
            Get
                Return objattAssetDetails
            End Get
            Set(ByVal Value As IAttribute)
                objattAssetDetails = CType(Value, attAssetDetails)
            End Set
        End Property

        Public Property ObjCommand() As IDbCommand
            Get
                Return _Command
            End Get
            Set(ByVal Value As IDbCommand)
                _Command = CType(Value, IDbCommand)
            End Set
        End Property
#End Region

        Public Function Insert_ExportServer(ByVal objattAssetDetails1 As attAssetDetails, ByVal Division As String, ByVal Category As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            Dim OrgDesc, Desc1, Desc2 As String
            OrgDesc = objattAssetDetails1.AstDesc.Trim
            If OrgDesc.Length > 30 Then
                Desc1 = OrgDesc.Substring(0, 30)
                If OrgDesc.Length > 60 Then
                    Desc2 = OrgDesc.Substring(30, 30)
                Else
                    Desc2 = OrgDesc.Substring(30)
                End If
            Else
                Desc1 = OrgDesc
                Desc2 = ""
            End If

            Dim disposed As String
            If objattAssetDetails1.Disposed Then
                disposed = "1"
            Else
                disposed = "0"
            End If

            Dim dispDate As String
            If String.IsNullOrEmpty(objattAssetDetails1.DispDate) Then
                dispDate = "0"
            Else
                dispDate = objattAssetDetails1.DispDate.ToString("yyyyMMdd")
            End If


            strQuery.Append("insert into ASSET_MASTER")
            strQuery.Append("(ASSET_NO,ASSET_DESC_1,ASSET_DESC_2,SERIAL_NO,P_O_NO,DIVISION,LOCATION,GL_CODE,CATEGORY,ACQ_DATE,DISP_CODE,DISP_DATE)")
            strQuery.Append(" Values")
            strQuery.Append(" (" & objattAssetDetails1.AstNum & ", '" & Desc1 & "', '" & Desc2 & "', '" & objattAssetDetails1.SerailNo & "', '" & objattAssetDetails1.PONumber & "', '" & Division & "', '" & objattAssetDetails1.LocID & "', '" & objattAssetDetails1.GLCode & "', '" & Category & "', " & objattAssetDetails1.PurDate.ToString("yyyyMMdd") & ", '" & disposed & "', " & dispDate & ")")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update_ExportServer(ByVal objattAssetDetails1 As attAssetDetails, ByVal Division As String, ByVal Category As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            Dim OrgDesc, Desc1, Desc2 As String
            OrgDesc = objattAssetDetails1.AstDesc.Trim
            If OrgDesc.Length > 30 Then
                Desc1 = OrgDesc.Substring(0, 30)
                If OrgDesc.Length > 60 Then
                    Desc2 = OrgDesc.Substring(30, 30)
                Else
                    Desc2 = OrgDesc.Substring(30)
                End If
            Else
                Desc1 = OrgDesc
                Desc2 = ""
            End If

            Dim disposed As String
            If objattAssetDetails1.Disposed Then
                disposed = "1"
            Else
                disposed = "0"
            End If

            Dim dispDate As String
            If String.IsNullOrEmpty(objattAssetDetails1.DispDate) Then
                dispDate = "0"
            Else
                dispDate = objattAssetDetails1.DispDate.ToString("yyyyMMdd")
            End If

            strQuery.Append("update ASSET_MASTER")
            strQuery.Append(" set")
            strQuery.Append(" ASSET_DESC_1='" & Desc1 & "',")
            strQuery.Append(" ASSET_DESC_2='" & Desc2 & "',")
            strQuery.Append(" SERIAL_NO='" & objattAssetDetails.SerailNo & "',")
            strQuery.Append(" P_O_NO='" & objattAssetDetails.PONumber & "',")
            strQuery.Append(" DIVISION='" & Division & "',")
            strQuery.Append(" LOCATION='" & objattAssetDetails.LocID & "',")
            strQuery.Append(" GL_CODE='" & objattAssetDetails.GLCode & "',")
            strQuery.Append(" CATEGORY='" & Category & "',")
            strQuery.Append(" ACQ_DATE=" & objattAssetDetails1.PurDate.ToString("yyyyMMdd") & ",")
            strQuery.Append(" DISP_CODE='" & disposed & "',")
            strQuery.Append(" DISP_DATE=" & dispDate)
            strQuery.Append(" where ASSET_NO=" & objattAssetDetails1.AstNum & "")
            'strQuery.Append(" where ASSET_NO=" & objattAssetDetails1.RefNo & "")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Dispose_ExportServer(ByVal objattAssetDetails1 As attAssetDetails) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            Dim dispDate As String
            If String.IsNullOrEmpty(objattAssetDetails1.DispDate) Then
                dispDate = "0"
            Else
                dispDate = objattAssetDetails1.DispDate.ToString("yyyyMMdd")
            End If

            strQuery.Append("update ASSET_MASTER")
            strQuery.Append(" set")
            strQuery.Append(" DISP_CODE='1',")
            strQuery.Append(" DISP_DATE=" & dispDate)
            strQuery.Append(" where ASSET_NO=" & objattAssetDetails1.AstNum & "")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Insert_AstHistoryExportServer(ByVal objattAssetDetails1 As attAssetDetails, ByVal Division As String, ByVal PrevDivision As String, ByVal Location As String, ByVal PrevLocation As String, ByVal Category As String, ByVal TransferDate As Date) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            Dim OrgDesc, Desc1, Desc2 As String
            OrgDesc = objattAssetDetails1.AstDesc.Trim
            If OrgDesc.Length > 30 Then
                Desc1 = OrgDesc.Substring(0, 30)
                If OrgDesc.Length > 60 Then
                    Desc2 = OrgDesc.Substring(30, 30)
                Else
                    Desc2 = OrgDesc.Substring(30)
                End If
            Else
                Desc1 = OrgDesc
                Desc2 = ""
            End If

            Dim disposed As String
            If objattAssetDetails1.Disposed Then
                disposed = "1"
            Else
                disposed = "0"
            End If

            Dim dispDate As String

            If objattAssetDetails1.DispDate <= "1/1/1900" Then
                dispDate = "0"
            Else
                dispDate = objattAssetDetails1.DispDate.ToString("yyyyMMdd")
            End If

            strQuery.Append("insert into ASST_CNG_HIST")
            strQuery.Append("(ASSET_NO,ASSET_DESC_1,ASSET_DESC_2,SERIAL_NO,P_O_NO,DIVISION,LOCATION,PREV_DIV,PREV_LOC,TRANS_DATE,GL_CODE,CATEGORY,ACQ_DATE,DISP_CODE,DISP_DATE)")
            strQuery.Append(" Values")
            strQuery.Append(" (" & objattAssetDetails1.AstNum & ", '" & Desc1 & "', '" & Desc2 & "', '" & objattAssetDetails1.SerailNo & "', '" & objattAssetDetails1.PONumber & "', '" & Division & "', '" & Location & "', '" & PrevDivision & "', '" & PrevLocation & "', " & TransferDate.ToString("yyyyMMdd") & ", '" & objattAssetDetails1.GLCode & "', '" & Category & "', " & objattAssetDetails1.PurDate.ToString("yyyyMMdd") & ", '" & disposed & "', " & dispDate & ")")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update_AstHistoryExportServer(ByVal AstNum As String, ByVal Division As String, ByVal PrevDivision As String, ByVal Location As String, ByVal PrevLocation As String, ByVal TransferDate As Date) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            strQuery.Append("Update ASSET_MASTER set")
            strQuery.Append(" DIVISION='" & Division & "',")
            strQuery.Append(" LOCATION='" & Location & "',")
            strQuery.Append(" PREV_DIV='" & PrevDivision & "',")
            strQuery.Append(" PREV_LOC='" & PrevLocation & "',")
            strQuery.Append(" TRANS_DATE=" & TransferDate.ToString("yyyyMMdd") & "")
            strQuery.Append(" where ASSET_NO =" & AstNum & "")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
    End Class
End Namespace