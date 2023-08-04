Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class AstBooks
        Implements IEntity

#Region "Data Members"
        Private objattAstBooks As attAstBooks
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattAstBooks = New attAstBooks

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattAstBooks
            End Get
            Set(ByVal Value As IAttribute)
                objattAstBooks = CType(Value, attAstBooks)
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
        Public Function GetAssetsCount(ByVal BookID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select count(*) from astbooks inner join assetdetails on assetdetails.astid = astbooks.astid ")
            strQuery.Append("where(astbooks.isdelete = 0 And Assetdetails.isdeleted = 0 And Assetdetails.issold = 0 And Assetdetails.disposed = 0 and astbooks.bookid = " + BookID + ")")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Check_Child_AstBooks(ByVal _id As String, ByVal formid As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select BookID from AstBooks where isdelete = 0")
            If formid = 12 Then
                strQuery.Append(" and  BookID =" & _id)
            ElseIf formid = 10 Then
                strQuery.Append(" and  AstID ='" & _id & "'")

            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function MarkAssetDisposed(ByVal LastBV As String, ByVal BookID As String, ByVal AstID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AstBooks")
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
            strQuery.Append("insert into AstBooks")
            strQuery.Append(" (BookID,AstID,DepCode,SalvageValue,SalvageYear,CurrentBV,BVUpdate,LastBV,SalvageMonth,SalvageValuePercent,Isdelete)")
            strQuery.Append(" Values")
            strQuery.Append(" ('" & Convert.ToString(objattAstBooks.PKeyCode) & "','" & objattAstBooks.AstID & "'," & objattAstBooks.DepCode & "," & objattAstBooks.SalvageValue & "," & objattAstBooks.SalvageYear & "," & objattAstBooks.CurrentBookValue & "," & BackEndDate(objattAstBooks.BVUpdate) & "," & objattAstBooks.LastBookValue & "," & objattAstBooks.SalvageMonth & "," & objattAstBooks.SalvageValuePercent & ",0) ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Insert_ExportServer(ByVal objattAstBook1 As attAstBooks, ByVal AssetRefNo As String, ByVal ServiceDate As Date, ByVal BaseCost As Decimal, ByVal AccDepValue As Decimal, ByVal DepValue As Decimal) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            Dim TotalMonths As Integer = (objattAstBook1.SalvageYear * 12) + objattAstBook1.SalvageMonth

            strQuery.Append("insert into ASSET_ENTITY")
            strQuery.Append("(ASSET_NO,START_DEPR_DATE,COST_OR_BASIS,LIFE_YEARS,LIFE_MONTHS,NO_OF_PRDS_DEPR,LIFE_TO_DATE_DEP,LAST_PRD_DEPREC)")
            strQuery.Append(" Values")
            strQuery.Append(" (" & AssetRefNo & "," & ServiceDate.ToString("yyyyMMdd") & "," & BaseCost & "," & objattAstBook1.SalvageYear & "," & objattAstBook1.SalvageMonth & "," & TotalMonths & "," & AccDepValue & "," & DepValue & ")")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update_ExportServer(ByVal objattAstBook1 As attAstBooks, ByVal AssetRefNo As String, ByVal ServiceDate As Date, ByVal BaseCost As Decimal, ByVal AccDepValue As Decimal, ByVal DepValue As Decimal) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            Dim TotalMonths As Integer = (objattAstBook1.SalvageYear * 12) + objattAstBook1.SalvageMonth

            strQuery.Append("update ASSET_ENTITY")
            strQuery.Append(" set")
            If ServiceDate <> Nothing Then
                strQuery.Append(" START_DEPR_DATE=" & ServiceDate.ToString("yyyyMMdd") & ",")
            End If
            strQuery.Append(" COST_OR_BASIS=" & BaseCost & ",")
            strQuery.Append(" LIFE_YEARS=" & objattAstBook1.SalvageYear & ",")
            strQuery.Append(" LIFE_MONTHS=" & objattAstBook1.SalvageMonth & ",")
            strQuery.Append(" NO_OF_PRDS_DEPR=" & TotalMonths & ",")
            If DepValue <> Nothing Then
                strQuery.Append(" LAST_PRD_DEPREC=" & DepValue & ",")
            End If
            strQuery.Append(" LIFE_TO_DATE_DEP=" & AccDepValue & "")
            strQuery.Append(" where ASSET_NO=" & AssetRefNo & "")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AstBooks")
            strQuery.Append(" set")
            strQuery.Append(" DepCode =" & objattAstBooks.DepCode & ",")
            strQuery.Append(" SalvageValue =" & objattAstBooks.SalvageValue & ",")
            strQuery.Append(" SalvageValuePercent =" & objattAstBooks.SalvageValuePercent & ",")
            strQuery.Append(" SalvageMonth =" & objattAstBooks.SalvageMonth & ",")
            strQuery.Append(" SalvageYear =" & objattAstBooks.SalvageYear & ",")
            strQuery.Append(" CurrentBV=" & objattAstBooks.CurrentBookValue & ",")
            strQuery.Append(" LastBV=" & objattAstBooks.LastBookValue & ",")
            strQuery.Append(" Isdelete= 0,")
            strQuery.Append(" BVUpdate =" & BackEndDate(objattAstBooks.BVUpdate) & "")
            strQuery.Append(" where BookID ='" & Convert.ToString(objattAstBooks.PKeyCode) & "' and AstID='" & objattAstBooks.AstID & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strID As String
            Dim strQuery As New StringBuilder
            strQuery.Append("Select BookID,AstID,DepCode,SalvageValue,SalvageYear,LastBV,CurrentBV,BVUpdate,IsDelete,SalvageMonth,SalvageValuePercent from AstBooks where IsDelete = 0")
            strID = Convert.ToString(objattAstBooks.PKeyCode)
            If objattAstBooks.PKeyCode <> "" And objattAstBooks.AstID <> "" Then
                strQuery.Append(" and bookid ='" & Convert.ToString(objattAstBooks.PKeyCode) & "' and AstID ='" & Convert.ToString(objattAstBooks.AstID) & "'  ")
            End If

            If objattAstBooks.PKeyCode = "" And objattAstBooks.AstID <> "" Then
                strQuery.Append("  and AstID ='" & Convert.ToString(objattAstBooks.AstID) & "'  ")
            End If

            If objattAstBooks.PKeyCode <> "" And objattAstBooks.AstID = "" Then
                strQuery.Append("  and bookid ='" & Convert.ToString(objattAstBooks.PKeyCode) & "'  ")
            End If


            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function GetAllData_Detail() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strID As String
            Dim strQuery As New StringBuilder
            strQuery.Append("Select AstBooks.BookID,AstBooks.AstID,AstBooks.DepCode,AstBooks.SalvageValue,AstBooks.SalvageYear,AstBooks.LastBV,AstBooks.CurrentBV,AstBooks.BVUpdate,AstBooks.IsDelete,Books.BookID,Books.DepCode,Books.IsDeleted,Books.Description,Books.IsDefault,Books.CompanyID,AstBooks.SalvageMonth,SalvageValuePercent from AstBooks")
            strQuery.Append(" inner join Books on AstBooks.BookID = Books.BookID")
            strQuery.Append(" where AstBooks.IsDelete = 0")


            strID = Convert.ToString(objattAstBooks.PKeyCode)
            If objattAstBooks.PKeyCode <> "" And objattAstBooks.AstID <> "" Then
                strQuery.Append(" and AstBooks.bookid ='" & Convert.ToString(objattAstBooks.PKeyCode) & "' and AstBooks.AstID ='" & Convert.ToString(objattAstBooks.AstID) & "'  ")
            End If

            If objattAstBooks.AstID <> "" Then
                strQuery.Append("  and AstBooks.AstID ='" & Convert.ToString(objattAstBooks.AstID) & "'  ")
            End If

            If objattAstBooks.PKeyCode <> "" Then
                strQuery.Append("  and AstBooks.bookid ='" & Convert.ToString(objattAstBooks.PKeyCode) & "'  ")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function CheckID() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strID As String
            Dim strQuery As New StringBuilder
            strQuery.Append("Select BookID,AstID,DepCode,SalvageValue,SalvageYear,LastBV,CurrentBV,BVUpdate,SalvageValuePercent,IsDelete from AstBooks ")
            strID = Convert.ToString(objattAstBooks.PKeyCode)
            If objattAstBooks.PKeyCode <> "" Then
                strQuery.Append(" where bookid ='" & Convert.ToString(objattAstBooks.PKeyCode) & "' and AstID ='" & Convert.ToString(objattAstBooks.AstID) & "'  ")
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

                strQuery.Append("delete from AstBooks") 
                strQuery.Append(" where AstID =?")

            Else
                strQuery.Append("update AstBooks")
                strQuery.Append(" set")
                strQuery.Append(" IsDelete = 1") 
                strQuery.Append(" where AstID =?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@AstID", objattAstBooks.AstID))


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

            strQuery.Append("Select top  1 Bookid from AstBooks where BookID like '" & str & "'  order by BookId DESC")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function


#End Region

    End Class

End Namespace
