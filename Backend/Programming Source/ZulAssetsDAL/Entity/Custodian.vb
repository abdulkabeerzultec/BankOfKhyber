Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class Custodian
        Implements IEntity

#Region "Data Members"
        Private objattCustodian As attCustodian
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattCustodian = New attCustodian

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattCustodian
            End Get
            Set(ByVal Value As IAttribute)
                objattCustodian = CType(Value, attCustodian)
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
            strQuery.Append("insert into Custodian")
            strQuery.Append(" (CustodianID,deptId,CustodianName,DesignationID,CustodianPhone,CustodianEmail,CustodianFax,CustodianCell,CustodianAddress,CustodianCode,Isdeleted)")
            strQuery.Append(" Values(?,?,?,?,?,?,?,?,?,?,0)")

            objCommand.Parameters.Add(New OleDbParameter("@CustodianID", objattCustodian.PKeyCode))
            objCommand.Parameters.Add(New OleDbParameter("@DepartmentID", objattCustodian.DepartmentID))
            objCommand.Parameters.Add(New OleDbParameter("@CustodianName", objattCustodian.CustodianName))
            objCommand.Parameters.Add(New OleDbParameter("@DesignationID", objattCustodian.DesignationID))
            objCommand.Parameters.Add(New OleDbParameter("@CustodianPhone", objattCustodian.CustodianPhone))
            objCommand.Parameters.Add(New OleDbParameter("@CustodianEmail", objattCustodian.CustodianEmail))
            objCommand.Parameters.Add(New OleDbParameter("@CustodianFax", objattCustodian.CustodianFax))
            objCommand.Parameters.Add(New OleDbParameter("@CustodianCell", objattCustodian.CustodianCell))
            objCommand.Parameters.Add(New OleDbParameter("@CustodianAddress", objattCustodian.CustodianAddress))
            objCommand.Parameters.Add(New OleDbParameter("@CustodianCode", objattCustodian.CustodianCode))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Custodian")
            strQuery.Append(" set")
            strQuery.Append(" CustodianName=?,DesignationID=?,CustodianPhone=?,CustodianEmail=?,CustodianFax=?,CustodianCell=?,deptId=?,CustodianAddress=?,CustodianCode=? ")
            strQuery.Append(" where CustodianID =?")

            objCommand.Parameters.Add(New OleDbParameter("@CustodianName", objattCustodian.CustodianName))
            objCommand.Parameters.Add(New OleDbParameter("@DesignationID", objattCustodian.DesignationID))
            objCommand.Parameters.Add(New OleDbParameter("@CustodianPhone", objattCustodian.CustodianPhone))
            objCommand.Parameters.Add(New OleDbParameter("@CustodianEmail", objattCustodian.CustodianEmail))
            objCommand.Parameters.Add(New OleDbParameter("@CustodianFax", objattCustodian.CustodianFax))
            objCommand.Parameters.Add(New OleDbParameter("@CustodianCell", objattCustodian.CustodianCell))
            objCommand.Parameters.Add(New OleDbParameter("@DepartmentID", objattCustodian.DepartmentID))
            objCommand.Parameters.Add(New OleDbParameter("@CustodianAddress", objattCustodian.CustodianAddress))
            objCommand.Parameters.Add(New OleDbParameter("@CustodianCode", objattCustodian.CustodianCode))

            objCommand.Parameters.Add(New OleDbParameter("@CustodianID", objattCustodian.PKeyCode))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand


            Dim strQuery As New StringBuilder
            strQuery.Append("Select CustodianID,deptId,CustodianName,DesignationID,CustodianPhone,CustodianEmail,CustodianFax,CustodianCell,CustodianAddress,Isdeleted,CustodianCode from Custodian where IsDeleted = 0")
            If objattCustodian.PKeyCode <> "" Then
                strQuery.Append(" and CustodianID = '" & objattCustodian.PKeyCode & "'")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function Check_Custodian(ByVal _id As String, ByVal formid As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select CustodianID,deptId,CustodianName,DesignationID,CustodianPhone,CustodianEmail,CustodianFax,CustodianCell,CustodianAddress,Isdeleted,CustodianCode from Custodian where IsDeleted = 0")
            If formid = 7 Then
                strQuery.Append(" and  DesignationID =?")
                objCommand.Parameters.Add(New OleDbParameter("@DesignationID", _id))
            ElseIf formid = 8 Then
                strQuery.Append(" and  DeptId =?")
                objCommand.Parameters.Add(New OleDbParameter("@DeptId", _id))
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function
        Public Function CheckID() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand


            Dim strQuery As New StringBuilder
            strQuery.Append("Select CustodianID,deptId,CustodianName,DesignationID,CustodianPhone,CustodianEmail,CustodianFax,CustodianCell,CustodianAddress,Isdeleted,CustodianCode from Custodian")
            If objattCustodian.PKeyCode <> "" Then
                strQuery.Append(" where CustodianID =?")
                objCommand.Parameters.Add(New OleDbParameter("@CustodianID", objattCustodian.PKeyCode))
            End If

            If objattCustodian.CustodianCode <> "" Then
                strQuery.Append(" where CustodianCode =?")
                objCommand.Parameters.Add(New OleDbParameter("@CustodianCode", objattCustodian.CustodianCode))
            End If

            If objattCustodian.CustodianName <> "" Then
                strQuery.Append(" where CustodianName =?")
                objCommand.Parameters.Add(New OleDbParameter("@CustodianName", objattCustodian.CustodianName))
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function GetDatabyID(ByVal Id As String) As IDbCommand Implements IEntity.GetDatabyID
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select * from Custodian")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
            Return objCommand
        End Function


        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from Custodian")
                strQuery.Append(" where CustodianID = ?")

            Else
                strQuery.Append("update Custodian")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where CustodianID = ?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@CustodianID", objattCustodian.PKeyCode))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
      

        Public Function GetAllData_Custodian() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            '  strQuery.Append("Select Custodian.CustodianID,Custodian.CustodianName,Designation.Description,Custodian.DesignationId,Custodian.CustodianAddress,Custodian.CustodianCell,Custodian.CustodianEmail,Custodian.CustodianFax,Custodian.CustodianPhone,Department.DeptId,Department.DeptName from Custodian ")
            '  strQuery.Append(" inner join Designation on Designation.DesignationID = Custodian.DesignationID ")
            '  strQuery.Append(" inner join Department on Department.DeptId = Custodian.DeptID where Custodian.IsDeleted = 0")

            strQuery.Append("Select Custodian.CustodianID,Custodian.CustodianName,Designation.Description,Custodian.DesignationId,Custodian.CustodianAddress,Custodian.CustodianCell,Custodian.CustodianEmail,Custodian.CustodianFax,Custodian.CustodianPhone,OrgHier.HierCode,'' as HierName,Custodian.CustodianCode from Custodian ")
            strQuery.Append(" left outer join Designation on Designation.DesignationID = Custodian.DesignationID ")
            strQuery.Append(" left outer join OrgHier on OrgHier.HierCode = Custodian.DeptID where Custodian.IsDeleted = 0  ")
            strQuery.Append(" order by Custodian.CustodianName  ")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData_Combo() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select CustodianID as ID,CustodianCode as Code ,CustodianName as Name from Custodian where IsDeleted = 0")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("SELECT     ISNULL(MAX(CAST(CustodianID AS bigint)), 0)+1 AS Expr1 FROM  Custodian WHERE  (CustodianID NOT LIKE '%[a-z]%') AND (ISNUMERIC(CustodianID) = 1)")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function




#End Region

    End Class

End Namespace
