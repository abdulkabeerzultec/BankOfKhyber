Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Namespace ZulAssetsDAL
    Public Class AssetItems
        Implements IEntity

#Region "Data Members"
        Private objattItem As attItems
        Private _Command As IDbCommand
#End Region

#Region "Constructor"
        Public Sub New()
            objattItem = New attItems
        End Sub
#End Region

#Region "Property"
        Public Property Attribute() As IAttribute Implements IEntity.ObjAttribute
            Get
                Return objattItem
            End Get
            Set(ByVal Value As IAttribute)
                objattItem = CType(Value, attItems)
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

        Public Function DeleteImage() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            strQuery.Append("update Assets set ItmImage = null where ItemCode = ?")
            objCommand.Parameters.Add(New OleDbParameter("@ItemCode", objattItem.PKeyCode))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function


        Public Function UpdateImage() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder

            strQuery.Append("update Assets set ItmImage = ? where ItemCode = ?")
            objCommand.Parameters.Add(New OleDbParameter("@ImageData", CType(objattItem.Image, Object)))
            objCommand.Parameters.Add(New OleDbParameter("@ItemCode", objattItem.PKeyCode))

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetImage() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select ItmImage from Assets where ItemCode = '" & objattItem.PKeyCode & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllImages() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("select ItemCode,ItmImage from Assets where ItmImage is not null")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function ClearAllImages() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update assets set itmimage = null")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Get_Depolicy(ByVal _id As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append(" select Assets.ItemCode,Assets.AstBrandID,Assets.AstCatID,Assets.POItmID,Assets.AstDesc,Assets.AstModel,Assets.AstQty,Assets.IsDeleted,DepPolicy.CatDepID,DepPolicy.AstCatID,DepPolicy.DepCode,DepPolicy.SalvageValue,DepPolicy.SalvageYear,DepPolicy.SalvageMonth,DepPolicy.IsSalvageValuePercent from Assets")
            strQuery.Append(" inner join DepPolicy on Assets.AstCatID=DepPolicy.AstCatID")
            strQuery.Append(" where ItemCode = '" & _id & "'")
            'strQuery.Append("Select ItemCode from Assets where isdeleted = 0")
            'strQuery.Append(" and  AstCatID Like'" & _id & "%'")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Check_Child(ByVal _id As String) As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("Select ItemCode from Assets where isdeleted = 0")
            strQuery.Append(" and  AstCatID Like'" & _id & "%'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Insert() As IDbCommand Implements IEntity.Insert
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("insert into Assets")
            strQuery.Append(" (ItemCode,AstBrandID,AstCatID,POItmID,AstDesc,AstModel,AstQty,Warranty,Isdeleted)")
            strQuery.Append(" Values")
            strQuery.Append(" (?,?,?,?,?,?,?,?,0) ")

            objCommand.Parameters.Add(New OleDbParameter("@ItemCode", objattItem.PKeyCode))
            objCommand.Parameters.Add(New OleDbParameter("@AstBrandID", objattItem.AstBrandID))
            objCommand.Parameters.Add(New OleDbParameter("@AstCatID", objattItem.AstCatID))
            objCommand.Parameters.Add(New OleDbParameter("@POItmID", objattItem.POItmID))
            objCommand.Parameters.Add(New OleDbParameter("@AstDesc", objattItem.AstDesc))
            objCommand.Parameters.Add(New OleDbParameter("@AstModel", objattItem.AstModel))
            objCommand.Parameters.Add(New OleDbParameter("@AstQty", objattItem.AstQty))
            objCommand.Parameters.Add(New OleDbParameter("@Warranty", objattItem.Warranty))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Update() As IDbCommand Implements IEntity.Update
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Assets")
            strQuery.Append(" set")
            strQuery.Append(" AstBrandID=?,AstCatID=?,POItmID=?,AstDesc=?,AstModel=?,AstQty=?,Warranty=?")
            strQuery.Append(" where ItemCode =?")

            objCommand.Parameters.Add(New OleDbParameter("@AstBrandID", objattItem.AstBrandID))
            objCommand.Parameters.Add(New OleDbParameter("@AstCatID", objattItem.AstCatID))
            objCommand.Parameters.Add(New OleDbParameter("@POItmID", objattItem.POItmID))
            objCommand.Parameters.Add(New OleDbParameter("@AstDesc", objattItem.AstDesc))
            objCommand.Parameters.Add(New OleDbParameter("@AstModel", objattItem.AstModel))
            objCommand.Parameters.Add(New OleDbParameter("@AstQty", objattItem.AstQty))
            objCommand.Parameters.Add(New OleDbParameter("@Warranty", objattItem.Warranty))
            objCommand.Parameters.Add(New OleDbParameter("@ItemCode", objattItem.PKeyCode))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function GetAllData_Combo() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            If AppConfig.DbType = "1" Then
                strQuery.Append("Select  Cast (ItemCode as bigint) as itemcode,AstDesc from Assets where IsDeleted = 0")
                If objattItem.PKeyCode <> "" Then
                    strQuery.Append(" and ItemCode = '" & objattItem.PKeyCode & "'")
                End If
                strQuery.Append(" order by  Cast (ItemCode as bigint)")

            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append("Select  Cast (ItemCode as number) as itemcode,AstDesc from Assets where IsDeleted = 0")
                If objattItem.PKeyCode <> "" Then
                    strQuery.Append(" and ItemCode = '" & objattItem.PKeyCode & "'")
                End If
                strQuery.Append(" order by  Cast (ItemCode as number)")
            End If
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function


        Public Function GetAllData() As IDbCommand Implements IEntity.GetAllData
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder

            If AppConfig.DbType = "1" Then
                strQuery.Append(" Select Cast (ItemCode as bigint) as ItemCode,Assets.AstBrandID,Assets.AstCatID,Assets.POItmId,Assets.AstDesc,Assets.AstModel,Assets.AstQTY,Brand.AstBrandID,Brand.AstBrandName, Category.AstCatID,Category.AstCatDesc,Category.CatFullPath,Assets.Warranty from Assets ")
                strQuery.Append(" left outer join Brand on Assets.AstBrandID = Brand.AstBrandID ")
                strQuery.Append(" left outer join Category on Assets.AstCatID = Category.AstCatID ")
                strQuery.Append(" where Assets.IsDeleted = 0  and Category.IsDeleted = 0 ")

                If objattItem.PKeyCode <> "" Then
                    strQuery.Append(" and ItemCode = '" & objattItem.PKeyCode & "'")
                End If

                strQuery.Append(" order by Cast (ItemCode as bigint)")

            ElseIf AppConfig.DbType = "2" Then

                strQuery.Append(" Select Cast (ItemCode as NUMBER) as ItemCode,Assets.AstBrandID,Assets.AstCatID,Assets.POItmId,Assets.AstDesc,Assets.AstModel,Assets.AstQTY,Brand.AstBrandID,Brand.AstBrandName, Category.AstCatID,Category.AstCatDesc,Category.CatFullPath,Assets.Warranty from Assets ")
                strQuery.Append(" left outer join Brand on Assets.AstBrandID = Brand.AstBrandID ")
                strQuery.Append(" left outer join Category on Assets.AstCatID = Category.AstCatID ")
                strQuery.Append(" where Assets.IsDeleted = 0")

                If objattItem.PKeyCode <> "" Then
                    strQuery.Append(" and ItemCode = '" & objattItem.PKeyCode & "'")
                End If

                strQuery.Append(" order by Cast (ItemCode as NUMBER)")
            End If


            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function CheckId() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select itemcode from Assets")
            If objattItem.PKeyCode <> "" Then
                strQuery.Append(" where ItemCode = '" & objattItem.PKeyCode & "'")
            End If

            If objattItem.AstDesc <> "" Then
                strQuery.Append(" where AstDesc = '" & objattItem.AstDesc & "'")
            End If

            If objattItem.AstCatID <> "" Then
                strQuery.Append(" and AstCatID = '" & objattItem.AstCatID & "'")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function CheckDescription() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            strQuery.Append("Select itemcode from Assets")
            strQuery.Append(" where AstDesc = N'" & objattItem.AstDesc & "'")
            strQuery.Append(" and AstCatID = '" & objattItem.AstCatID & "'")

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand

        End Function

        Public Function GetAllData_Joined() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand

            Dim strQuery As New StringBuilder
            If AppConfig.DbType = "1" Then
                strQuery.Append("select Cast (Assets.ItemCode as bigint) as itemcode ,Assets.POItmID,Assets.AstDesc,Assets.AstModel,Assets.AstQty,'',Category.AstCatDesc,Category.AstCatID,Assets.AstBrandID,Assets.Warranty   from Assets,Category")

                strQuery.Append(" where Assets.AstCatID =Category.AstCatID and Assets.IsDeleted = 0 and Category.Isdeleted = 0")
                If objattItem.PKeyCode <> "" Then
                    strQuery.Append(" and ItemCode = '" & objattItem.PKeyCode & "'")
                End If
                If objattItem.AstCatID <> "" Then
                    strQuery.Append(" and  Assets.AstCatID = '" & objattItem.AstCatID & "'")
                End If


            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append("select Cast (Assets.ItemCode as Number) as itemcode ,Assets.POItmID,Assets.AstDesc,Assets.AstModel,Assets.AstQty,'',Category.AstCatDesc,Category.AstCatID,Assets.AstBrandID,Assets.Warranty   from Assets,Category")

                strQuery.Append(" where Assets.AstCatID =Category.AstCatID and Assets.IsDeleted = 0 and Category.Isdeleted = 0")
                If objattItem.PKeyCode <> "" Then
                    strQuery.Append(" and ItemCode = '" & objattItem.PKeyCode & "'")
                End If
                If objattItem.AstCatID <> "" Then
                    strQuery.Append(" and  Assets.AstCatID = '" & objattItem.AstCatID & "'")
                End If

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
        Public Function Update_Assets_Cat() As IDbCommand
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            strQuery.Append("update Assets")
            strQuery.Append(" set")
            strQuery.Append(" AstCatID='" & objattItem.AstCatID & "'")
            strQuery.Append(" where ItemCode ='" & objattItem.PKeyCode & "'")
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function

        Public Function Delete() As IDbCommand Implements IEntity.Delete
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DeletePermt = 1 Then
                strQuery.Append("delete from Assets")
                strQuery.Append(" where ItemCode = ?")
            Else
                strQuery.Append("update Assets")
                strQuery.Append(" set")
                strQuery.Append(" IsDeleted=1")
                strQuery.Append(" where ItemCode = ?")
            End If
            objCommand.Parameters.Add(New OleDbParameter("@ItemCode", objattItem.PKeyCode))
            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function




        Public Function GetNextPKey() As IDbCommand Implements IEntity.GetNextPk
            Dim objCommand As OleDbCommand = New OleDbCommand
            Dim strQuery As New StringBuilder
            If AppConfig.DbType = "1" Then
                strQuery.Append("select max(CONVERT(bigint, ItemCode))+1 from Assets")
            ElseIf AppConfig.DbType = "2" Then
                strQuery.Append("select max(Cast(Assets.ItemCode as Number))+1 from Assets")
            Else
                strQuery.Append("select max(CONVERT(bigint, ItemCode))+1 from Assets")
            End If

            objCommand.CommandText = strQuery.ToString()
            _Command = objCommand
            Return objCommand
        End Function
#End Region

    End Class

End Namespace
