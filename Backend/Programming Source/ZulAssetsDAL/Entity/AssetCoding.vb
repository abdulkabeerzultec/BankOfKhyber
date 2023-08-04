Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class AssetCoding
        Implements IEntity

#Region "Data Members"


        Private objattAssetsCoding As attAssetsCoding
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattAssetsCoding = New attAssetsCoding

        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattAssetsCoding
            End Get
            Set(ByVal Value As IAttribute)
                objattAssetsCoding = CType(Value, attAssetsCoding)
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
            strQuery.Append("insert into AssetCoding")
            strQuery.Append(" (AssetCodingID,CompanyID,StartSerial,EndSerial,Status,Isdeleted)")
            strQuery.Append(" Values(?,?,?,?,?,0)")
            objCommand.Parameters.Add(New OleDbParameter("@AssetCodingID", objattAssetsCoding.PKeyCode))
            objCommand.Parameters.Add(New OleDbParameter("@CompanyID", objattAssetsCoding.CompanyID))
            objCommand.Parameters.Add(New OleDbParameter("@StartSerial", objattAssetsCoding.StartSerial))
            objCommand.Parameters.Add(New OleDbParameter("@EndSerial", objattAssetsCoding.EndSerial))
            objCommand.Parameters.Add(New OleDbParameter("@Status", objattAssetsCoding.Status))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function CloseRange() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetCoding")
            strQuery.Append(" set")
            strQuery.Append(" Status = 0")
            strQuery.Append(" where CompanyID=?")
            objCommand.Parameters.Add(New OleDbParameter("@CompanyID", objattAssetsCoding.CompanyID))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update AssetCoding")
            strQuery.Append(" set")
            strQuery.Append(" CompanyID=?,StartSerial=?,EndSerial=?,Status=? ")
            strQuery.Append(" where AssetCodingID =?")
            objCommand.Parameters.Add(New OleDbParameter("@CompanyID", objattAssetsCoding.CompanyID))
            objCommand.Parameters.Add(New OleDbParameter("@StartSerial", objattAssetsCoding.StartSerial))
            objCommand.Parameters.Add(New OleDbParameter("@EndSerial", objattAssetsCoding.EndSerial))
            objCommand.Parameters.Add(New OleDbParameter("@Status", objattAssetsCoding.Status))
            objCommand.Parameters.Add(New OleDbParameter("@AssetCodingID", objattAssetsCoding.PKeyCode))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" Select AssetCodingID,AssetCoding.CompanyID,Companies.CompanyName,StartSerial,EndSerial,CAST(Status as CHAR(1)) as Status from AssetCoding")
            strQuery.Append(" inner join Companies on Companies.CompanyID = AssetCoding.CompanyID ")
            strQuery.Append(" where AssetCoding.IsDeleted = 0")
            If objattAssetsCoding.PKeyCode <> "0" Then
                strQuery.Append(" and AssetCodingID =?")
                objCommand.Parameters.Add(New OleDbParameter("@AssetCodingID", objattAssetsCoding.PKeyCode))
            End If
            If objattAssetsCoding.CompanyID <> "0" Then
                strQuery.Append(" and AssetCoding.CompanyID =?")
                objCommand.Parameters.Add(New OleDbParameter("@CompanyID", objattAssetsCoding.CompanyID))
            End If
            If objattAssetsCoding.Status = True Then
                strQuery.Append(" and AssetCoding.Status =1")
            End If

            If AppConfig.CompanyIDS <> "0" And AppConfig.CompanyIDS <> "" Then
                strQuery.Append(" and  AssetCoding.CompanyID IN (" & AppConfig.CompanyIDS & ")")
                'objCommand.Parameters.Add(New OleDbParameter("@CompanyIDs", AppConfig.CompanyIDS))
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetCompanyCodingDefsIDs(ByVal CompanyID As Integer) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand


            Dim strQuery As New StringBuilder
            strQuery.Append("Select AssetCodingID from AssetCoding  where IsDeleted = 0")
            strQuery.Append(" and  CompanyID  = " & CompanyID.ToString & "")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from AssetCoding")
                strQuery.Append(" where AssetCodingID =?")
            Else

                strQuery.Append("update AssetCoding")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where AssetCodingID =?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@AssetCodingID", objattAssetsCoding.PKeyCode))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select max(AssetCodingID)+1 from AssetCoding")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
        Public Function VerfiY_Range(ByVal Start As Long, ByVal EndRange As Long, ByVal ID As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select * from AssetCoding where ")
            strQuery.Append(" ((startSerial <= ? and ?  <= EndSerial)")
            strQuery.Append(" or ")
            strQuery.Append(" (startSerial <= ? and ?  <= EndSerial))")
            objCommand.Parameters.Add(New OleDbParameter("@Start1", Start))
            objCommand.Parameters.Add(New OleDbParameter("@Start2", Start))
            objCommand.Parameters.Add(New OleDbParameter("@EndRange1", EndRange))
            objCommand.Parameters.Add(New OleDbParameter("@EndRange2", EndRange))

            If Trim(ID) <> "" Then
                strQuery.Append(" and AssetCodingID <> ?")
                objCommand.Parameters.Add(New OleDbParameter("@AssetCodingID", ID))
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
        End Function
#End Region
    End Class
End Namespace
