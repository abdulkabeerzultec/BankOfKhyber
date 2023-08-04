

Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class CostCenter
        Implements IEntity

#Region "Data Members"
        Private objattCostCenter As attCostCenter
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattCostCenter = New attCostCenter

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattCostCenter
            End Get
            Set(ByVal Value As IAttribute)
                objattCostCenter = CType(Value, attCostCenter)
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
        Public Function Check_CostCenterChilds(ByVal _id As String, ByVal formid As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select * from AssetDetails where IsDeleted = 0")
            strQuery.Append(" and  CostCenterID =" & _id & "")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function Insert() As IDbCommand Implements IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into CostCenter")
            strQuery.Append(" (CostID,CostNumber,CostName,CompanyId,Isdeleted)")
            strQuery.Append(" Values(?,?,?,?,0)")
            objCommand.Parameters.Add(New OleDbParameter("@CostID", Convert.ToString(objattCostCenter.PKeyCode)))
            objCommand.Parameters.Add(New OleDbParameter("@CostNumber", Convert.ToString(objattCostCenter.CostNumber)))
            objCommand.Parameters.Add(New OleDbParameter("@CostName", Convert.ToString(objattCostCenter.CostName)))
            objCommand.Parameters.Add(New OleDbParameter("@CompanyId", Convert.ToString(objattCostCenter.CompanyID)))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update CostCenter")
            strQuery.Append(" set")
            strQuery.Append(" CostNumber=?,")
            strQuery.Append(" CostName=?,")
            strQuery.Append(" CompanyId=?")
            strQuery.Append(" where CostID =?")
            objCommand.Parameters.Add(New OleDbParameter("@CostNumber", Convert.ToString(objattCostCenter.CostNumber)))
            objCommand.Parameters.Add(New OleDbParameter("@CostName", Convert.ToString(objattCostCenter.CostName)))
            objCommand.Parameters.Add(New OleDbParameter("@CompanyId", Convert.ToString(objattCostCenter.CompanyID)))
            objCommand.Parameters.Add(New OleDbParameter("@CostID", Convert.ToString(objattCostCenter.PKeyCode)))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select CostCenter.CostID,CostCenter.CostNumber,CostCenter.CostName,Companies.CompanyName, Companies.CompanyID,CostCenter.Isdeleted  from CostCenter left outer join Companies on Companies.CompanyId =CostCenter.CompanyId where CostCenter.IsDeleted = 0")
            If objattCostCenter.PKeyCode <> "" Then
                strQuery.Append(" and CostCenter.CostID = '" & objattCostCenter.PKeyCode & "'")
            End If

            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and CostCenter.CompanyId IN (" & AppConfig.CompanyIDS & ")")
            End If

            If objattCostCenter.CostNumber <> "" Then
                strQuery.Append(" and CostCenter.CostNumber = '" & objattCostCenter.CostNumber & "'")
            End If
            strQuery.Append(" ORDER BY CostCenter.CostNumber")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function GetCombo() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select CostID,CostName from CostCenter where IsDeleted = 0")
            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and CostCenter.CompanyId IN (" & AppConfig.CompanyIDS & ")")
            End If
            strQuery.Append(" order by CostName")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetComboList() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select CostID,CostNumber,CostName from CostCenter where IsDeleted = 0")
            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  CostCenter.CompanyID IN (" & AppConfig.CompanyIDS & ")")
            End If
            strQuery.Append(" order by CostName")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function CheckID() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select CostCenter.CostID,CostCenter.CostNumber,CostCenter.CostName from CostCenter where CostCenter.IsDeleted = 0")
            If objattCostCenter.PKeyCode <> "" Then
                strQuery.Append(" and CostCenter.CostID = '" & objattCostCenter.PKeyCode & "'")
            End If

            If objattCostCenter.CostNumber <> "" Then
                strQuery.Append(" and CostCenter.CostNumber = '" & objattCostCenter.CostNumber & "'")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from CostCenter")
                strQuery.Append(" where CostID ='" & objattCostCenter.PKeyCode & "'")
            Else
                strQuery.Append("update CostCenter")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where CostID ='" & objattCostCenter.PKeyCode & "'")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select  max(cast(CostID as int)) + 1 from CostCenter")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function GetCodeIDByNumber(ByVal CostNumber As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DbType = "1" Then
                strQuery.Append("Select  ISNULL(CostID,'') from CostCenter where CostNumber = '" & CostNumber & "'")
            Else
                strQuery.Append("Select  CostID from CostCenter where CostNumber = '" & CostNumber & "'")
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
#End Region

    End Class

End Namespace

