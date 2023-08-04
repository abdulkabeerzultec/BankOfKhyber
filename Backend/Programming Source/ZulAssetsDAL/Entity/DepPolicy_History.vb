
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class DepPolicy_History
        Implements IEntity

#Region "Data Members"
        Private objattDepPolicy_History As attDepPolicy_History
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattDepPolicy_History = New attDepPolicy_History

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattDepPolicy_History
            End Get
            Set(ByVal Value As IAttribute)
                objattDepPolicy_History = CType(Value, attDepPolicy_History)
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
        Public Function Check_Child_DepPolicy_History(ByVal _id As String, ByVal formid As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select BookID from DepPolicy_History where isdelete = 0")
            If formid = 12 Then
                strQuery.Append(" and  BookID =" & _id)
            ElseIf formid = 10 Then
                strQuery.Append(" and  AstID =?")
                objCommand.Parameters.Add(New OleDbParameter("@AstID", _id))
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function MarkAssetDisposed(ByVal LastBV As String, ByVal BookID As String, ByVal AstID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update DepPolicy_History")
            strQuery.Append(" set")
            strQuery.Append(" CurrentBV= 0.0,")
            strQuery.Append(" LastBV=" & LastBV & "")
            strQuery.Append(" where BookID ='" & Convert.ToString(BookID) & "' and AstID='" & AstID & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Insert() As IDbCommand Implements IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into DepPolicy_History")
            strQuery.Append(" (BookID,AstID,DepCode,SalvageValue,SalvageYear,CurrentBV,BVUpdate,LastBV,SalvageMonth,Isdelete)")
            strQuery.Append(" Values")
            strQuery.Append(" ('" & Convert.ToString(objattDepPolicy_History.PKeyCode) & "','" & objattDepPolicy_History.AstID & "'," & objattDepPolicy_History.DepCode & "," & objattDepPolicy_History.SalvageValue & "," & objattDepPolicy_History.SalvageYear & "," & objattDepPolicy_History.CurrentBookValue & "," & BackEndDate(objattDepPolicy_History.BVUpdate) & "," & objattDepPolicy_History.LastBookValue & "," & objattDepPolicy_History.SalvageMonth & ",0) ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update DepPolicy_History")
            strQuery.Append(" set")
            strQuery.Append(" DepCode =" & objattDepPolicy_History.DepCode & ",")
            strQuery.Append(" SalvageValue =" & objattDepPolicy_History.SalvageValue & ",")
            strQuery.Append(" SalvageMonth =" & objattDepPolicy_History.SalvageMonth & ",")
            strQuery.Append(" SalvageYear =" & objattDepPolicy_History.SalvageYear & ",")
            strQuery.Append(" CurrentBV=" & objattDepPolicy_History.CurrentBookValue & ",")
            strQuery.Append(" LastBV=" & objattDepPolicy_History.LastBookValue & "")
            'strQuery.Append(" BVUpdate ='" & objattDepPolicy_History.BVUpdate & "'")
            strQuery.Append(" where BookID ='" & Convert.ToString(objattDepPolicy_History.PKeyCode) & "' and AstID='" & objattDepPolicy_History.AstID & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strID As String
            Dim strQuery As New StringBuilder
            strQuery.Append("Select BookID,AstID,DepCode,SalvageValue,SalvageYear,LastBV,CurrentBV,BVUpdate,IsDelete,SalvageMonth from DepPolicy_History where IsDelete = 0")
            strID = Convert.ToString(objattDepPolicy_History.PKeyCode)
            If objattDepPolicy_History.PKeyCode <> "" And objattDepPolicy_History.AstID <> "" Then
                strQuery.Append(" and bookid ='" & Convert.ToString(objattDepPolicy_History.PKeyCode) & "' and AstID ='" & Convert.ToString(objattDepPolicy_History.AstID) & "'  ")
            End If

            If objattDepPolicy_History.PKeyCode = "" And objattDepPolicy_History.AstID <> "" Then
                strQuery.Append("  and AstID ='" & Convert.ToString(objattDepPolicy_History.AstID) & "'  ")
            End If

            If objattDepPolicy_History.PKeyCode <> "" And objattDepPolicy_History.AstID = "" Then
                strQuery.Append("  and bookid ='" & Convert.ToString(objattDepPolicy_History.PKeyCode) & "'  ")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData_Detail() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strID As String
            Dim strQuery As New StringBuilder
            strQuery.Append("Select DepPolicy_History.BookID,DepPolicy_History.AstID,DepPolicy_History.DepCode,DepPolicy_History.SalvageValue,DepPolicy_History.SalvageYear,DepPolicy_History.LastBV,DepPolicy_History.CurrentBV,DepPolicy_History.BVUpdate,DepPolicy_History.IsDelete,Books.BookID,Books.DepCode,Books.IsDeleted,Books.Description,Books.IsDefault,Books.CompanyID,DepPolicy_History.SalvageMonth from DepPolicy_History")
            strQuery.Append(" inner join Books on DepPolicy_History.BookID = Books.BookID")
            strQuery.Append(" where DepPolicy_History.IsDelete = 0")


            strID = Convert.ToString(objattDepPolicy_History.PKeyCode)
            If objattDepPolicy_History.PKeyCode <> "" And objattDepPolicy_History.AstID <> "" Then
                strQuery.Append(" and DepPolicy_History.bookid ='" & Convert.ToString(objattDepPolicy_History.PKeyCode) & "' and DepPolicy_History.AstID ='" & Convert.ToString(objattDepPolicy_History.AstID) & "'  ")
            End If

            If objattDepPolicy_History.PKeyCode = "" And objattDepPolicy_History.AstID <> "" Then
                strQuery.Append("  and DepPolicy_History.AstID ='" & Convert.ToString(objattDepPolicy_History.AstID) & "'  ")
            End If

            If objattDepPolicy_History.PKeyCode <> "" And objattDepPolicy_History.AstID = "" Then
                strQuery.Append("  and DepPolicy_History.bookid ='" & Convert.ToString(objattDepPolicy_History.PKeyCode) & "'  ")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Get_Latest_History(ByVal BookId As String, ByVal AstId As String, ByVal Lastupdate As Date, ByVal ThisUpdate As Date) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            ' Dim strID As String
            Dim strQuery As New StringBuilder
            strQuery.Append(" Select BookID,AstID,DepCode,SalvageValue,SalvageYear,LastBV,CurrentBV,BVUpdate,IsDelete from DepPolicy_History ")
            strQuery.Append(" where BVUpdate >= " & BackEndDate(Lastupdate) & " and  BVUpdate <= " & BackEndDate(ThisUpdate) & "")
            strQuery.Append(" and BookID = '" & BookId & "' and AstID ='" & AstId & "'")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function CheckHistory(ByVal BookId As String, ByVal AstId As String, ByVal Lastupdate As Date, ByVal ThisUpdate As Date) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            '     Dim strID As String
            Dim strQuery As New StringBuilder
            strQuery.Append("Select BookID,AstID,DepCode,SalvageValue,SalvageYear,LastBV,CurrentBV,BVUpdate,IsDelete from DepPolicy_History ")
            '   strID = Convert.ToString(objattDepPolicy_History.PKeyCode)
            strQuery.Append(" where BVUpdate >= " & BackEndDate(Lastupdate) & " and  BVUpdate <=" & BackEndDate(ThisUpdate) & "")
            strQuery.Append(" and BookID = '" & BookId & "' and AstID ='" & AstId & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function CheckID() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strID As String
            Dim strQuery As New StringBuilder
            strQuery.Append("Select BookID,AstID,DepCode,SalvageValue,SalvageYear,LastBV,CurrentBV,BVUpdate,IsDelete from DepPolicy_History ")
            strID = Convert.ToString(objattDepPolicy_History.PKeyCode)
            If objattDepPolicy_History.PKeyCode <> "" Then
                strQuery.Append(" where bookid ='" & Convert.ToString(objattDepPolicy_History.PKeyCode) & "' and AstID ='" & Convert.ToString(objattDepPolicy_History.AstID) & "'  ")
            End If

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

                strQuery.Append("delete from DepPolicy_History")
                If objattDepPolicy_History.PKeyCode <> "" Then
                    strQuery.Append(" where bookid ='" & Convert.ToString(objattDepPolicy_History.PKeyCode) & "'")
                End If

            Else
                strQuery.Append("update DepPolicy_History")
                strQuery.Append(" set")
                strQuery.Append(" IsDelete = 1")
                If objattDepPolicy_History.PKeyCode <> "" Then
                    strQuery.Append(" where bookid ='" & Convert.ToString(objattDepPolicy_History.PKeyCode) & "'")
                End If
            End If



            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder


            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function


        Public Function GetNextPKey(ByVal str As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            strQuery.Append("Select top  1 Bookid from DepPolicy_History where BookID like '" & str & "'  order by BookId DESC")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function


#End Region

    End Class

End Namespace
