Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class Category
        Implements IEntity

#Region "Data Members"
        Private objattCategory As attCategory
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattCategory = New attCategory

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattCategory
            End Get
            Set(ByVal Value As IAttribute)
                objattCategory = CType(Value, attCategory)
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
        Public Function Get_Index(ByVal Id As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select Id1 from Category where AstCatID =?")
            objCommand.Parameters.Add(New OleDbParameter("@AstCatID", Id))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Get_AstCatIdByCode(ByVal Code As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select AstCatID from Category where Code=?")
            objCommand.Parameters.Add(New OleDbParameter("@Code", Code))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function UpdateIndex(ByVal ID As String, ByVal index As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Category")
            strQuery.Append(" set")
            strQuery.Append(" ID1=?")
            strQuery.Append(" where  AstCatID = ?")
            objCommand.Parameters.Add(New OleDbParameter("@index", index))
            objCommand.Parameters.Add(New OleDbParameter("@AstCatID", ID))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function
        Public Function Insert() As IDbCommand Implements IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into Category")
            strQuery.Append(" (Id1,AstCatID,AstCatDesc,Code,catLevel,Isdeleted)")
            strQuery.Append(" Values(?,?,?,?,?,0)")
            objCommand.Parameters.Add(New OleDbParameter("@Id1", objattCategory.PKeyCode))
            objCommand.Parameters.Add(New OleDbParameter("@AstCatID", objattCategory.AstCatID))
            objCommand.Parameters.Add(New OleDbParameter("@AstCatDesc", objattCategory.AstCatDesc))
            objCommand.Parameters.Add(New OleDbParameter("@Code", objattCategory.Code))
            objCommand.Parameters.Add(New OleDbParameter("@catLevel", objattCategory.catLevel))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Category")
            strQuery.Append(" set AstCatDesc=?,CompCode=?,Code=?,catLevel=?,CatFullPath=?")
            strQuery.Append(" where AstCatID =?")
            objCommand.Parameters.Add(New OleDbParameter("@AstCatDesc", objattCategory.AstCatDesc))
            objCommand.Parameters.Add(New OleDbParameter("@Compcode", objattCategory.Compcode))
            objCommand.Parameters.Add(New OleDbParameter("@Code", objattCategory.Code))
            objCommand.Parameters.Add(New OleDbParameter("@catLevel", objattCategory.catLevel))
            objCommand.Parameters.Add(New OleDbParameter("@CatFullPath", objattCategory.CompleteCatDesc))
            objCommand.Parameters.Add(New OleDbParameter("@AstCatID", objattCategory.AstCatID))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllChildCategory(ByVal CatID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select AstCatID,AstCatDesc,Code,CompCode,catLevel from Category where IsDeleted = 0")
            strQuery.Append(" and AstCatID like ?")
            objCommand.Parameters.Add(New OleDbParameter("@AstCatID", CatID & "-%"))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select AstCatID,AstCatDesc,Code,CompCode,catLevel from Category where IsDeleted = 0")
            If objattCategory.AstCatID <> "" Then
                strQuery.Append(" and AstCatID = ?")
                objCommand.Parameters.Add(New OleDbParameter("@AstCatID", objattCategory.AstCatID))
            End If
            strQuery.Append(" order by catLevel,ID1 ")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetDatabyID(ByVal Id As String) As IDbCommand Implements IEntity.GetDatabyID
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select AstCatID,AstCatDesc,Code,CompCode,catLevel from Category")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from Category")
                strQuery.Append(" where AstCatID =?")
            Else
                strQuery.Append("update Category")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where AstCatID =?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@AstCatID", objattCategory.AstCatID))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetChildsID(ByVal Id As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select AstCatID from Category where AstCatID like ? and AstCatID not like ? ")
            objCommand.Parameters.Add(New OleDbParameter("@parm1", Id & "-%"))
            objCommand.Parameters.Add(New OleDbParameter("@parm2", Id & "-%-%"))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetRootID() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DbType = "1" Then
                strQuery.Append("Select max(cast(AstCatID as int)) + 1 from Category where AstCatID not like '%-%'")
            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append("Select max(cast(AstCatID as number)) + 1 from Category where AstCatID not like '%-%'")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function Get_Code(ByVal Id As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select Code from Category where IsDeleted = 0 and  AstCatID =?")
            objCommand.Parameters.Add(New OleDbParameter("@Id", Id))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Get_Desc(ByVal Id As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select AstCatDesc from Category where IsDeleted = 0 and AstCatID =?")
            objCommand.Parameters.Add(New OleDbParameter("@Id", Id))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Get_IDByDesc(ByVal Desc As String, ByVal MainCatID As String, Optional ByVal level As Integer = -1) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If MainCatID = "" Then
                strQuery.Append("Select AstCatID from Category where IsDeleted = 0 and AstCatDesc =?")
                objCommand.Parameters.Add(New OleDbParameter("@AstCatDesc", Desc))
            Else
                strQuery.Append("Select AstCatID from Category where IsDeleted = 0 and AstCatDesc =? and AstCatID like ?")
                If level > -1 Then
                    strQuery.Append(" and catLevel = ?")
                End If
                objCommand.Parameters.Add(New OleDbParameter("@AstCatDesc", Desc))
                objCommand.Parameters.Add(New OleDbParameter("@AstCatID", MainCatID & "-%"))
                If level > -1 Then
                    objCommand.Parameters.Add(New OleDbParameter("@catLevel", level))
                End If
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(Id1)+1 from Category")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function
        Public Function Get_FullCategory(ByVal ID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select CatFullPath from Category where IsDeleted = 0 and  AstCatID =?")
            objCommand.Parameters.Add(New OleDbParameter("@AstCatID", ID))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function CheckCatNameFound(ByVal CatName As String, ByVal ParentID As String, ByVal CatID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If ParentID = "" Then
                strQuery.Append("Select Count(*) from Category where IsDeleted = 0 and  AstCatDesc =? and catLevel = 0 and AstCatID <> ?")
                objCommand.Parameters.Add(New OleDbParameter("@AstCatDesc", CatName))
                objCommand.Parameters.Add(New OleDbParameter("@AstCatID", CatID))
            Else
                strQuery.Append("Select Count(*) from Category where IsDeleted = 0 and  AstCatDesc =? and AstCatID like ? and AstCatID <> ?")
                objCommand.Parameters.Add(New OleDbParameter("@AstCatDesc", CatName))
                objCommand.Parameters.Add(New OleDbParameter("@ParentID", ParentID & "-%"))
                objCommand.Parameters.Add(New OleDbParameter("@AstCatID", CatID))
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function CheckCatCodeFound(ByVal CatCode As String, ByVal ParentID As String, ByVal CatID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If ParentID = "" Then
                strQuery.Append("Select Count(*) from Category where IsDeleted = 0 and  Code =? and catLevel = 0 and AstCatID <> ?")
                objCommand.Parameters.Add(New OleDbParameter("@AstCatDesc", CatCode))
                objCommand.Parameters.Add(New OleDbParameter("@AstCatID", CatID))
            Else
                strQuery.Append("Select Count(*) from Category where IsDeleted = 0 and  Code =? and AstCatID like ? and AstCatID <> ?")
                objCommand.Parameters.Add(New OleDbParameter("@Code", CatCode))
                objCommand.Parameters.Add(New OleDbParameter("@ParentID", ParentID & "-%"))
                objCommand.Parameters.Add(New OleDbParameter("@AstCatID", CatID))
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetCategoryList() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select AstCatID,AstCatDesc,Code,CompCode,catLevel from Category where IsDeleted = 0 ")
            strQuery.Append(" order by AstCatDesc")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

#End Region

    End Class

End Namespace
