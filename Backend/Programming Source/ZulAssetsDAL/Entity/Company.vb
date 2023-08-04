Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class Company
        Implements IEntity

#Region "Data Members"
        Private objattCompany As attcompany
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattCompany = New attCompany

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattCompany
            End Get
            Set(ByVal Value As IAttribute)
                objattCompany = CType(Value, attcompany)
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
            strQuery.Append("insert into Companies")
            strQuery.Append(" (CompanyId,CompanyCode,CompanyName,BarStructID,HierCode,Isdeleted,LastAssetNumber)")
            strQuery.Append(" Values")
            strQuery.Append(" (" & Convert.ToString(objattCompany.PKeyCode) & ",'" & objattCompany.CompanyCode & "','" & objattCompany.CompanyName & "'," & objattCompany.BarStructID & ",'" & objattCompany.HierCode & "',0,0) ")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Companies")
            strQuery.Append(" set")
            strQuery.Append(" HierCode='" & objattCompany.HierCode & "',")
            strQuery.Append(" CompanyCode='" & objattCompany.CompanyCode & "',")
            strQuery.Append(" BarStructID= " & objattCompany.BarStructID & ",")
            strQuery.Append(" CompanyName='" & objattCompany.CompanyName & "'")
            strQuery.Append(" where CompanyId =" & objattCompany.PKeyCode & "")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function BarCode_Assign() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Companies")
            strQuery.Append(" set")
            strQuery.Append(" BarStructID= " & objattCompany.BarStructID & "")
            strQuery.Append(" where CompanyId =" & objattCompany.PKeyCode & "")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData_Grid() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append(" Select CompanyID,CompanyCode,CompanyName,BarCode_Struct.BarStructID,BarCode_Struct.IsDeleted,BarCode_Struct.BarCode,BarCode_Struct.ValueSep,HierCode  from Companies")
            strQuery.Append(" left outer join BarCode_Struct on BarCode_Struct.BarStructID =Companies.BarStructID")
            strQuery.Append(" where BarCode_Struct.IsDeleted = 0")
            If objattCompany.PKeyCode <> 0 Then
                strQuery.Append(" and CompanyId = " & objattCompany.PKeyCode)
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append(" Select  CompanyID,CompanyCode,CompanyName,BarCode_Struct.BarStructID,BarCode_Struct.IsDeleted,BarCode_Struct.BarCode,BarCode_Struct.ValueSep,HierCode  from Companies")
            strQuery.Append(" left outer join BarCode_Struct on BarCode_Struct.BarStructID =Companies.BarStructID")
            strQuery.Append(" where BarCode_Struct.IsDeleted = 0")
            If objattCompany.PKeyCode <> 0 Then
                strQuery.Append(" and CompanyId = " & objattCompany.PKeyCode)
            End If
            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function GetAllData_Combo() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select CompanyId,CompanyName from Companies where IsDeleted = 0")
            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetLastAssetNumber(ByVal CompanyID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select LastAssetNumber from Companies where IsDeleted = 0")
            strQuery.Append(" and CompanyID =" & CompanyID)

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetCompanyIDByCode(ByVal CompanyCode As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select CompanyId from Companies where IsDeleted = 0")
            strQuery.Append(" and CompanyCode ='" & CompanyCode & "'")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetCompanyCodeByID(ByVal CompanyID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select CompanyCode from Companies where IsDeleted = 0")
            strQuery.Append(" and CompanyID ='" & CompanyID & "'")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function SetLastAssetNumber(ByVal CompanyID As String, ByVal LastAssetNumber As Int64) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("update Companies set LastAssetNumber = " & LastAssetNumber & " where ")
            strQuery.Append(" CompanyID =" & CompanyID)

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function CheckRelatedBooks() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select Count(*) from books where companyid = " & objattCompany.PKeyCode)
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function CheckRelatedAssets() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select Count(*) from AssetDetails where companyid = " & objattCompany.PKeyCode)
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from Companies")
                strQuery.Append(" where CompanyId =" & objattCompany.PKeyCode)
            Else
                strQuery.Append("update Companies")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where CompanyId =" & objattCompany.PKeyCode)
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(CompanyId)+1 from Companies")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function GetDatabyID(ByVal Id As String) As IDbCommand Implements IEntity.GetDatabyID
            Dim objCommand As OleDbCommand = New OleDbCommand

            Return objCommand
        End Function
#End Region

    End Class

End Namespace


