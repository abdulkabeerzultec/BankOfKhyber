
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text

Namespace ZulAssetsDAL
    Public Class GLCode
        Implements IEntity

#Region "Data Members"
        Private objattGLCode As attGLCode
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattGLCode = New attGLCode

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattGLCode
            End Get
            Set(ByVal Value As IAttribute)
                objattGLCode = CType(Value, attGLCode)
            End Set
        End Property

        Public Property ObjCommand() As IDbCommand Implements IEntity.ObjCommand
            Get
                Return _Command
            End Get
            Set(ByVal Value As IDbCommand)
                _Command = CType(Value, IDbCommand)
            End Set
        End Property
#End Region

#Region "Methods"

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select GLCodes.GLcode, Companies.CompanyName, GLCodes.GLDesc,Companies.CompanyID from GLCodes left outer join Companies on Companies.CompanyId =GLCodes.CompanyId  WHERE GLCodes.isDeleted=0 ")
            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and GLCodes.CompanyId IN (" & AppConfig.CompanyIDS & ")")
            End If

            If Not String.IsNullOrEmpty(objattGLCode.GLDesc) Then
                strQuery.Append(" and GLDesc = '" & objattGLCode.GLDesc & "'")
            End If
            strQuery.Append(" ORDER BY GLCode")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetGLCodesByCompany(ByVal compID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If compID = "" Then
                strQuery.Append("Select GLcode, GLDesc from GLCodes WHERE isDeleted=0 ORDER BY GLCode")
            Else
                strQuery.Append("Select GLcode, GLDesc from GLCodes WHERE isDeleted=0 and CompanyId = " + compID + " ORDER BY GLCode")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Private Function Insert() As IDbCommand Implements IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into GLCodes")
            strQuery.Append(" (GLCode,GLDesc,CompanyId,Isdeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" (" & Convert.ToString(objattGLCode.PKeyCode) & ",'" & objattGLCode.GLDesc & "'," & objattGLCode.CompanyId & ",0) ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Private Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update GLCODES")
            strQuery.Append(" set")
            strQuery.Append(" GLDesc='" & objattGLCode.GLDesc & "'")
            strQuery.Append(" ,CompanyId=" & objattGLCode.CompanyId & "")
            strQuery.Append(" where GLCode =" & objattGLCode.PKeyCode & "")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Private Function GetDatabyID(ByVal Id As String) As IDbCommand Implements IEntity.GetDatabyID
            Return Nothing
        End Function


        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from GLCodes")
                strQuery.Append(" where GLCode =" & objattGLCode.PKeyCode)
            Else

                strQuery.Append("update GLCodes")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where GLCode =" & objattGLCode.PKeyCode)
            End If



            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(GLCode)+1 from GLCodes")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function
#End Region

    End Class
End Namespace



