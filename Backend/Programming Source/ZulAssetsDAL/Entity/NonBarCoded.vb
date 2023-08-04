

Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class AnonymousAsset
        Implements IEntity

#Region "Data Members"

        Dim objDBConnection As New DBConnection
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
        Public Function Insert() As IDbCommand Implements IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            Return objCommand
        End Function
        Public Function GetAllData_Temp() As DataTable
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim ConnTemp As OleDbConnection
            Dim Da As OleDbDataAdapter
            Dim ds As New DataTable
            ConnTemp = objDBConnection.GetConnection_Temp()

            objCommand.Connection = ConnTemp
            Dim strQuery As New StringBuilder
            strQuery.Append(" select ")
            strQuery.Append(" NonBCode,DeviceID,LocID,AstCatID,AstDesc as Description,Model as AstModel,Serial as SerailNo,TransDate as HisDate,RefNo,Remarks")
            strQuery.Append(" from NonBarCodedTemp")
            'strQuery.Append("          left Outer Join LocationTemp on LocationTemp.LocID = NonBarCodedTemp.LocID")
            'strQuery.Append("             left Outer Join CategoryTemp on CategoryTemp.AstCatID = NonBarCodedTemp.AstCatID")
            objCommand.CommandText = strQuery.ToString()
            Da = New OleDbDataAdapter(objCommand)
            Da.Fill(ds)
            Return ds
        End Function
        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim ConnTemp As OleDbConnection
            Dim Da As OleDbDataAdapter
            Dim dt As New DataTable
            ConnTemp = objDBConnection.GetConnection_Temp()
            objCommand.Connection = ConnTemp
            Dim strQuery As New StringBuilder
            strQuery.Append(" select ")
            strQuery.Append(" NonBarCodedTemp.NonBCode,NonBarCodedTemp.DeviceID,NonBarCodedTemp.LocID,NonBarCodedTemp.AstCatID,NonBarCodedTemp.AstDesc,")
            strQuery.Append(" LocationTemp.LocID,LocationTemp.LocDesc,LocationTemp.IsDeleted,LocationTemp.ID1,CategoryTemp.AstCatID,CategoryTemp.AstCatDesc,")
            strQuery.Append(" CategoryTemp.IsDeleted,CategoryTemp.ID1 from NonBarCodedTemp")
            strQuery.Append("          left Outer Join LocationTemp on LocationTemp.LocID = NonBarCodedTemp.LocID")
            strQuery.Append("             left Outer Join CategoryTemp on CategoryTemp.AstCatID = NonBarCodedTemp.AstCatID")
            objCommand.CommandText = strQuery.ToString()
            Da = New OleDbDataAdapter(objCommand)
            Da.Fill(dt)
            Return dt
        End Function

        Public Function GetDatabyID(ByVal Id As String) As IDbCommand Implements IEntity.GetDatabyID
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            Return objCommand
        End Function

        Public Function Delete_Temp(ByVal id As String, ByVal DeviceID As String, ByVal TransDate As DateTime) As Boolean
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim ConnTemp As OleDbConnection
            Dim dt As New DataTable
            ConnTemp = objDBConnection.GetConnection_Temp()
            objCommand.Connection = ConnTemp
            Dim strQuery As New StringBuilder
            strQuery.Append("delete from NonBarCodedTemp")
            strQuery.Append(" where NonBCode =? and DeviceID=? and TransDate=?")
            objCommand.Parameters.Add(New OleDbParameter("@NonBCode", id))
            objCommand.Parameters.Add(New OleDbParameter("@DeviceID", DeviceID))
            objCommand.Parameters.Add(New OleDbParameter("@TransDate", TransDate))
            objCommand.CommandText = strQuery.ToString()
            objCommand.ExecuteNonQuery()
            Return True
        End Function

        Public Function Update_Remarks(ByVal id As String, ByVal DeviceID As String, ByVal TransDate As DateTime, ByVal Remarks As String, ByVal InvSchID As Integer) As Boolean
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim ConnTemp As OleDbConnection
            Dim dt As New DataTable
            ConnTemp = objDBConnection.GetConnection_Temp()
            objCommand.Connection = ConnTemp
            Dim strQuery As New StringBuilder
            strQuery.Append("update NonBarCodedTemp set Remarks =?")
            If InvSchID >= 0 Then
                strQuery.Append(",InvSchID=?")
            End If

            strQuery.Append(" where NonBCode =? and DeviceID=? and TransDate=?")
            objCommand.Parameters.Add(New OleDbParameter("@Remarks", Remarks))
            If InvSchID >= 0 Then
                objCommand.Parameters.Add(New OleDbParameter("@InvSchID", InvSchID))
            End If
            objCommand.Parameters.Add(New OleDbParameter("@NonBCode", id))
            objCommand.Parameters.Add(New OleDbParameter("@DeviceID", DeviceID))
            objCommand.Parameters.Add(New OleDbParameter("@TransDate", TransDate))
            objCommand.CommandText = strQuery.ToString()
            objCommand.ExecuteNonQuery()
            Return True
        End Function

        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim ConnTemp As OleDbConnection
            Dim dt As New DataTable
            ConnTemp = objDBConnection.GetConnection_Temp()
            objCommand.Connection = ConnTemp
            Dim strQuery As New StringBuilder
            strQuery.Append("delete from NonBarCodedTemp")
            strQuery.Append(" where NonBCode = ?")
            objCommand.Parameters.Add(New OleDbParameter("@NonBCode", objattCategory.PKeyCode))
            objCommand.CommandText = strQuery.ToString()
            objCommand.ExecuteNonQuery()
            Return objCommand
        End Function
        Public Function GetChild_Count(ByVal Id As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select Count(*) from Category where  AstCatID like ?")
            objCommand.Parameters.Add(New OleDbParameter("@AstCatID", Trim(Id) & "%"))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
            Return objCommand
        End Function
        Public Function GetChild_Count_Temp(ByVal Id As String) As DataTable
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim ConnTemp As OleDbConnection
            Dim Da As OleDbDataAdapter
            Dim ds As New DataTable
            ConnTemp = objDBConnection.GetConnection_Temp()
            objCommand.Connection = ConnTemp
            Dim strQuery As New StringBuilder
            strQuery.Append("Select Count(*) from Category where  AstCatID like ?")
            objCommand.Parameters.Add(New OleDbParameter("@AstCatID", Trim(Id) & "%"))
            objCommand.CommandText = strQuery.ToString()
            Da = New OleDbDataAdapter(objCommand)
            Da.Fill(ds)
            Return ds
        End Function


        'Public Function Get_Desc(ByVal Id As String) As IDbCommand

        'End Function
        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            Return objCommand

        End Function
#End Region

    End Class

End Namespace
