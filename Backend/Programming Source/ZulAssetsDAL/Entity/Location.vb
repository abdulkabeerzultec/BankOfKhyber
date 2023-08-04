Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text

Namespace ZulAssetsDAL
    Public Class Locations
        Implements IEntity

#Region "Data Members"
        Private objattLocation As attLocation
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattLocation = New attLocation

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattLocation
            End Get
            Set(ByVal Value As IAttribute)
                objattLocation = CType(Value, attLocation)
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
            strQuery.Append("insert into Location")
            strQuery.Append(" (ID1,LocID,LocDesc,Code,locLevel,Isdeleted,CompanyID)")
            strQuery.Append(" Values(?,?,?,?,?,0,?)")
            objCommand.Parameters.Add(New OleDbParameter("@ID1", objattLocation.PKeyCode))
            objCommand.Parameters.Add(New OleDbParameter("@LocID", objattLocation.HierCode))
            objCommand.Parameters.Add(New OleDbParameter("@LocDesc", objattLocation.Description))
            objCommand.Parameters.Add(New OleDbParameter("@Code", objattLocation.Code))
            objCommand.Parameters.Add(New OleDbParameter("@locLevel", objattLocation.locLevel))
            objCommand.Parameters.Add(New OleDbParameter("@CompanyID", objattLocation.CompanyID))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Location")
            strQuery.Append(" set LocDesc=?,CompCode=?,locLevel=?,Code=?,LocationFullPath=?,CompanyID=?")
            strQuery.Append(" where LocID =?")
            objCommand.Parameters.Add(New OleDbParameter("@LocDesc", objattLocation.Description))
            objCommand.Parameters.Add(New OleDbParameter("@CompCode", objattLocation.Compcode))
            objCommand.Parameters.Add(New OleDbParameter("@locLevel", objattLocation.locLevel))
            objCommand.Parameters.Add(New OleDbParameter("@Code", objattLocation.Code))
            objCommand.Parameters.Add(New OleDbParameter("@CompleteLocationDesc", objattLocation.CompleteLocationDesc))
            objCommand.Parameters.Add(New OleDbParameter("@CompanyID", objattLocation.CompanyID))
            objCommand.Parameters.Add(New OleDbParameter("@LocID", objattLocation.HierCode))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllChildLocations(ByVal LocID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select LocID,LocDesc,Code,CompCode,locLevel,CompanyID from Location where IsDeleted = 0")
            strQuery.Append(" and LocID like ?")
            objCommand.Parameters.Add(New OleDbParameter("@LocID", LocID & "-%"))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetComboData() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select Location.LocID,Location.LocDesc,Location.Code,Location.CompCode,Location.locLevel,Location.CompanyID,Companies.CompanyName  from Location Left outer join Companies on Companies.CompanyId = Location.CompanyId where Location.IsDeleted = 0")
            If objattLocation.HierCode <> "" Then
                strQuery.Append(" and Location.LocID =?")
                objCommand.Parameters.Add(New OleDbParameter("@LocID", objattLocation.HierCode))
            End If
            If objattLocation.Description <> "" Then
                strQuery.Append(" and Location.LocDesc =?")
                objCommand.Parameters.Add(New OleDbParameter("@LocDesc", objattLocation.Description))
            End If

            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  (Location.CompanyID IN (" & AppConfig.CompanyIDS & ") Or Location.CompanyID is null or Location.CompanyID = -1)")
            End If
            strQuery.Append(" order by Location.locLevel,Location.ID1 ")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function GetComboDataSorted() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select Location.LocID,Location.LocDesc,Location.Code,Location.CompCode,Location.locLevel,Location.CompanyID,Companies.CompanyName,Location.LocationFullPath  from Location Left outer join Companies on Companies.CompanyId = Location.CompanyId where Location.IsDeleted = 0")
            If objattLocation.HierCode <> "" Then
                strQuery.Append(" and Location.LocID =?")
                objCommand.Parameters.Add(New OleDbParameter("@LocID", objattLocation.HierCode))
            End If
            If objattLocation.Description <> "" Then
                strQuery.Append(" and Location.LocDesc =?")
                objCommand.Parameters.Add(New OleDbParameter("@LocDesc", objattLocation.Description))
            End If

            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  (Location.CompanyID IN (" & AppConfig.CompanyIDS & ") Or Location.CompanyID is null or Location.CompanyID = -1)")
            End If
            strQuery.Append(" order by Location.Code ")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select Location.LocID,Location.LocDesc,Location.Code,Location.CompCode,Location.locLevel,Location.CompanyID,Companies.CompanyName  from Location Left outer join Companies on Companies.CompanyId = Location.CompanyId where Location.IsDeleted = 0")
            If objattLocation.HierCode <> "" Then
                strQuery.Append(" and Location.LocID =?")
                objCommand.Parameters.Add(New OleDbParameter("@LocID", objattLocation.HierCode))
            End If
            If objattLocation.Description <> "" Then
                strQuery.Append(" and Location.LocDesc =?")
                objCommand.Parameters.Add(New OleDbParameter("@LocDesc", objattLocation.Description))
            End If
            strQuery.Append(" order by Location.locLevel,Location.ID1 ")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Get_Code(ByVal Id As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select  Code from Location where IsDeleted = 0 and  LocID =?")
            objCommand.Parameters.Add(New OleDbParameter("@LocID", Id))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Get_FullLocation(ByVal ID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select  LocationFullPath  from Location where IsDeleted = 0 and  LocID =?")
            objCommand.Parameters.Add(New OleDbParameter("@LocID", ID))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Get_LocCompCode(ByVal ID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select CompCode from Location where IsDeleted = 0 and LocID =?")
            objCommand.Parameters.Add(New OleDbParameter("@LocID", ID))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Get_ID(ByVal Code As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select  LocID  from Location where IsDeleted = 0 and  Code =?")
            objCommand.Parameters.Add(New OleDbParameter("@Code", Code))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetLocIDByCompleteCode(ByVal Code As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select  LocID  from Location where IsDeleted = 0 and  CompCode =?")
            objCommand.Parameters.Add(New OleDbParameter("@Code", Code))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetLocIDByCompleteDesc(ByVal Desc As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select  LocID  from Location where IsDeleted = 0 and  LocationFullPath =?")
            objCommand.Parameters.Add(New OleDbParameter("@Desc", Desc))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function Get_Desc(ByVal Id As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select LocDesc from Location where IsDeleted = 0 and  LocID =?")
            objCommand.Parameters.Add(New OleDbParameter("@LocID", Id))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Get_Desc_OrgHier(ByVal Id As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select GrpDesc from COMPGROUPS where IsDeleted = 0 and  GrpID=?")
            objCommand.Parameters.Add(New OleDbParameter("@GrpID", Id))
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
                strQuery.Append("delete from Location")
                strQuery.Append(" where  LocID =?")
            Else
                strQuery.Append("update Location")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where  LocID = ?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@LocID", objattLocation.HierCode))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function Get_Index(ByVal Id As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select Id1 from Location where LocID =?")
            objCommand.Parameters.Add(New OleDbParameter("@LocID", Id))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function UpdateIndex(ByVal ID As String, ByVal index As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Location")
            strQuery.Append(" set")
            strQuery.Append(" ID1=" & index)
            strQuery.Append(" where  LocID = '" & ID & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function
        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(Id1)+1 from Location")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetChildsID(ByVal Id As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select Locid from Location where LocID like ? and locid not like ? ")
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
                strQuery.Append("Select max(cast(Locid as int)) + 1 from Location where LocID not like '%-%'")
            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append("Select max(cast(Locid as number)) + 1 from Location where LocID not like '%-%'")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        'Public Function GetNBCodeReport() As IDbCommand
        '    Dim objCommand As OleDbCommand = New OleDbCommand
        '    Dim strQuery As New StringBuilder
        '    strQuery.Append("Select")
        '    strQuery.Append(" NonBarCodedTemp.NonBCode,NonBarCodedTemp.DeviceID,NonBarCodedTemp.LocID,NonBarCodedTemp.AstCatID,NonBarCodedTemp.AstDesc, ")
        '    strQuery.Append(" Location.LocID,Location.LocDesc,Location.IsDeleted,Location.ID1, ")
        '    strQuery.Append(" Category.AstCatID,Category.AstCatDesc,Category.IsDeleted,Category.ID1, ")
        '    strQuery.Append(" Devices.DeviceID,DeviceDesc,ComType,DeviceIP,Status,Devices.Isdeleted,statusID,StatusMsg,Updated ")
        '    strQuery.Append(" from NonBarCodedTemp,Location,Category,Devices")
        '    strQuery.Append(" where NonBarCodedTemp.LocID = Location.LocID")
        '    strQuery.Append(" and NonBarCodedTemp.AstCatId = Category.AstCatId")
        '    strQuery.Append(" and NonBarCodedTemp.DeviceID = Devices.DeviceID")
        '    objCommand.CommandText = strQuery.ToString()
        '    _Command = objCommand
        '    Return objCommand
        'End Function
        Public Function CheckLocNameFound(ByVal LocName As String, ByVal LocParent As String, ByVal LocID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If LocParent = "" Then
                strQuery.Append("Select Count(*) from Location where IsDeleted = 0 and  LocDesc =? and LocLevel=0 and LocID <> ?")
                objCommand.Parameters.Add(New OleDbParameter("@LocDesc", LocName))
                objCommand.Parameters.Add(New OleDbParameter("@LocID", LocID))
            Else
                strQuery.Append("Select Count(*) from Location where IsDeleted = 0 and  LocDesc =? and LocID like ? and LocID <> ?")
                objCommand.Parameters.Add(New OleDbParameter("@LocDesc", LocName))
                objCommand.Parameters.Add(New OleDbParameter("@LocParent", LocParent & "-%"))
                objCommand.Parameters.Add(New OleDbParameter("@LocID", LocID))
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function CheckLocCodeFound(ByVal LocCode As String, ByVal LocParent As String, ByVal LocID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If LocParent = "" Then
                strQuery.Append("Select Count(*) from Location where IsDeleted = 0 and  Code =? and LocLevel=0 and LocID <> ?")
                objCommand.Parameters.Add(New OleDbParameter("@LocDesc", LocCode))
                objCommand.Parameters.Add(New OleDbParameter("@LocID", LocID))
            Else
                strQuery.Append("Select Count(*) from Location where IsDeleted = 0 and  Code =? and LocID like ? and LocID <> ?")
                objCommand.Parameters.Add(New OleDbParameter("@Code", LocCode))
                objCommand.Parameters.Add(New OleDbParameter("@LocParent", LocParent & "-%"))
                objCommand.Parameters.Add(New OleDbParameter("@LocID", LocID))
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function GetLocationLabelData(ByVal LocID As String, ByVal IncludeChilds As Boolean) As IDbCommand

            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DbType = "1" Then
                strQuery.Append("Select LocID,'LOC' + LocID as LocBarcode,LocDesc,Code,CompCode as CompleteCode,LocationFullPath as CompleteDesc,")
            Else
                strQuery.Append("Select LocID,'LOC' || LocID as LocBarcode,LocDesc,Code,CompCode as CompleteCode,LocationFullPath as CompleteDesc,")
            End If
            strQuery.Append(" Companies.CompanyID,Companies.CompanyCode,Companies.CompanyName")
            strQuery.Append("  from location left outer join Companies on Companies.CompanyID = location.CompanyID ")
            If IncludeChilds Then
                strQuery.Append(" where  LocId = ? or LocId like ?")
                objCommand.Parameters.Add(New OleDbParameter("@LocID", LocID))
                objCommand.Parameters.Add(New OleDbParameter("@LocID1", LocID & "-%"))
            Else
                strQuery.Append(" where  LocId = ?")
                objCommand.Parameters.Add(New OleDbParameter("@LocID", LocID))
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
#End Region
    End Class
End Namespace



