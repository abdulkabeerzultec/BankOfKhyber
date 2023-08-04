

Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class CompanyInfo
        Implements IEntity

#Region "Data Members"
        Private objattCompanyInfo As attCompanyInfo
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattCompanyInfo = New attCompanyInfo

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattCompanyInfo
            End Get
            Set(ByVal Value As IAttribute)
                objattCompanyInfo = CType(Value, attCompanyInfo)
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
            Dim enc As New System.Text.ASCIIEncoding()

            Dim strQuery As New StringBuilder
            strQuery.Append("insert into CompanyInfo")
            strQuery.Append(" (ID,Name,Address,State,PCode,City,Country,Phone,Fax,Email")
            If objattCompanyInfo.IsNewImage Then
                strQuery.Append(",Image)")
            End If
            strQuery.Append(" Values")
            strQuery.Append(" (1,'" & objattCompanyInfo.Name & "','" & objattCompanyInfo.Address & "','" & objattCompanyInfo.State & "','" & objattCompanyInfo.PCode & "','" & objattCompanyInfo.City & "','" & objattCompanyInfo.CounTry & "','" & objattCompanyInfo.Phone & "','" & objattCompanyInfo.Fax & "','" & objattCompanyInfo.Email)
            If objattCompanyInfo.IsNewImage Then
                strQuery.Append("',?) ")
                objCommand.Parameters.Add(New OleDbParameter("@ImageData", CType(objattCompanyInfo.Image, Object)))
            Else
                strQuery.Append("') ")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update CompanyInfo")
            strQuery.Append(" set")
            strQuery.Append(" Name   = '" & objattCompanyInfo.Name & "',")
            strQuery.Append(" Address  = '" & objattCompanyInfo.Address & "',")
            strQuery.Append(" State  = '" & objattCompanyInfo.State & "',")
            strQuery.Append(" PCode = '" & objattCompanyInfo.PCode & "',")
            strQuery.Append(" City  = '" & objattCompanyInfo.City & "',")
            strQuery.Append(" Country  = '" & objattCompanyInfo.CounTry & "',")
            strQuery.Append(" Phone = '" & objattCompanyInfo.Phone & "',")
            strQuery.Append(" Fax = '" & objattCompanyInfo.Fax & "',")
            strQuery.Append(" Email = '" & objattCompanyInfo.Email & "' ")
            If objattCompanyInfo.IsNewImage Then
                If objattCompanyInfo.Image Is Nothing Then
                    strQuery.Append(" ,Image  = null ") 'Delete the image.
                Else
                    strQuery.Append(" ,Image  = ? ")
                    objCommand.Parameters.Add(New OleDbParameter("@ImageData", CType(objattCompanyInfo.Image, Object)))
                End If
            End If
            strQuery.Append(" where ID =( select min(ID) from Companyinfo)")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand


            Dim strQuery As New StringBuilder
            strQuery.Append("Select  ID,Name,Address,State,PCode,City,Country,Phone,Fax,Email,Image from CompanyInfo ")

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
        End Function

        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder


            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from CompanyInfo")
                strQuery.Append(" where CompanyInfoID = ?")
            Else
                strQuery.Append("update CompanyInfo")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where CompanyInfoID =?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@CompanyInfoID", objattCompanyInfo.PKeyCode))




            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData_DesignationName() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select CompanyInfo.CompanyInfoID,CompanyInfo.CompanyInfoName,Designation.Description,CompanyInfo.DesignationId,CompanyInfo.CompanyInfoAddress,CompanyInfo.CompanyInfoCell,CompanyInfo.CompanyInfoEmail,CompanyInfo.CompanyInfoFax,CompanyInfo.CompanyInfoPhone from CompanyInfo inner join Designation on Designation.DesignationID = CompanyInfo.DesignationID where CompanyInfo.IsDeleted = 0")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData_Combo() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select CompanyInfoID,CompanyInfoName from CompanyInfo where IsDeleted = 0")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand

            Return objCommand
        End Function

#End Region

    End Class

End Namespace

