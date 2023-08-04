
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class Book
        Implements IEntity

#Region "Data Members"
        Private objattBook As attBook
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattBook = New attBook

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattBook
            End Get
            Set(ByVal Value As IAttribute)
                objattBook = CType(Value, attBook)
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
        Public Function Insert() As IDbCommand Implements IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into Books")
            strQuery.Append(" (BookId,DepCode,Description,Isdeleted,CompanyID)")
            strQuery.Append(" Values")
            strQuery.Append(" (" & objattBook.PKeyCode & "," & objattBook.DepCode & ",'" & objattBook.Description & "',0," & objattBook.CompanyID & ") ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Books")
            strQuery.Append(" set")
            strQuery.Append(" DepCode =" & objattBook.DepCode & ",")
            strQuery.Append(" CompanyID =" & objattBook.CompanyID & ",")
            strQuery.Append(" Description ='" & objattBook.Description & "'")
            strQuery.Append(" where BookID =" & Convert.ToString(objattBook.PKeyCode) & "")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetCompanyBooksIDs(ByVal CompanyID As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand


            Dim strQuery As New StringBuilder
            strQuery.Append("Select Books.BookID from Books  where IsDeleted = 0")
            strQuery.Append(" and  Books.companyID  = " & CompanyID.ToString & "")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strID As String
            Dim strQuery As New StringBuilder
            ' Depreciation_Method.IsDeleted this field used as selection field in frmDepMan form
            strQuery.Append(" Select Books.BookID,Books.Description,Depreciation_Method.DepDesc,Depreciation_Method.DepCode,Books.CompanyID,Companies.CompanyName from Books ")
            strQuery.Append(" inner join Companies on Books.CompanyID =companies.CompanyId ")
            strQuery.Append(" inner join Depreciation_Method on Books.DepCode=Depreciation_Method.DepCode ")
            strQuery.Append(" where  Books.IsDeleted = 0")
            strID = Convert.ToString(objattBook.PKeyCode)
            If objattBook.PKeyCode <> "" Then
                strQuery.Append(" and  BookID =" & Convert.ToString(objattBook.PKeyCode) & "")
            End If
            If objattBook.CompanyID <> 0 Then
                strQuery.Append(" and  Books.companyID =" & Convert.ToString(objattBook.CompanyID) & " ")
            End If

            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and Books.CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If

            strQuery.Append(" order by Books.BookID")


            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function



        Public Function GetDatabyID(ByVal Id As String) As IDbCommand Implements IEntity.GetDatabyID
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select * from GrpChild")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
            Return objCommand
        End Function

        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from Books")
                strQuery.Append(" where BookID =" & Convert.ToString(objattBook.PKeyCode) & "")
            Else

                strQuery.Append("update Books")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where BookID =" & Convert.ToString(objattBook.PKeyCode) & "")
            End If




            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(BookID)+1 from Books")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function
#End Region

    End Class

End Namespace

