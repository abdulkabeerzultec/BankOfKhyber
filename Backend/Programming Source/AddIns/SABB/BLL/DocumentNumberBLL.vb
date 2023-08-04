Imports SABBPlugin.AppSettings
Imports SABBPlugin.AppSettingsTableAdapters
Imports ZulAssetsBL

Public Class DocumentNumberBLL
    Inherits BaseBLL
    Implements IBusLayer
    Private DataTable As New DocumentNumbersDataTable
    Private _Attributes As DocumentNumbersRow
    Private Adapter As New DocumentNumbersTableAdapter
    Private GrdDocNumberAdapter As New GrdDocNumberTableAdapter
    Public Sub New()
        TableName = DataTable.TableName
        PrimaryKey = DataTable.GUIDColumn.ColumnName
        OrderField = DataTable.CreationDateColumn.ColumnName
    End Sub
    Public Property Attributes() As DocumentNumbersRow
        Get
            If _Attributes Is Nothing Then
                _Attributes = DataTable.NewDocumentNumbersRow
            End If
            Return _Attributes
        End Get
        Set(ByVal value As DocumentNumbersRow)
            _Attributes = value
        End Set
    End Property

    Public Function DeleteByRowGUID(ByVal rowGUID As System.Guid) As String Implements IBusLayer.DeleteByRowGUID
        Return Nothing
    End Function

    Public Function Edit(ByVal rowGUID As System.Guid) As String Implements IBusLayer.Edit
        Try
            _RecordState = TRecordStates.ModifiedRecord
            DataTable = Adapter.GetDataByGUID(rowGUID)
            If DataTable.Rows.Count > 0 Then
                Attributes = DataTable(0)
                Return Nothing
            Else
                Return My.Resources.BLLStrings.RecordNotFound
            End If
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function GetData(Optional ByRef msg As String = "") As System.Data.DataTable Implements IBusLayer.GetData
        Return Nothing
    End Function

    Public Function GetGridData(Optional ByRef msg As String = "") As System.Data.DataTable Implements IBusLayer.GetListData
        Try
            Dim dt As GrdDocNumberDataTable = GrdDocNumberAdapter.GetData
            dt.DocTypeEngNameColumn.Caption = My.Resources.ColsName.DocTypeEngNameColumn
            dt.DocTypeNameColumn.Caption = My.Resources.ColsName.DocTypeNameColumn
            dt.GUIDColumn.SetOrdinal(0)
            dt.DocTypeEngNameColumn.SetOrdinal(1)
            dt.DocTypeNameColumn.SetOrdinal(2)
            dt.PrefixColumn.SetOrdinal(3)
            dt.SerialNumberColumn.SetOrdinal(4)
            dt.SuffixColumn.SetOrdinal(5)
            dt.PreviewColumn.SetOrdinal(6)

            If IsForeignLanguage Then
                dt.Columns.Remove(dt.DocTypeEngNameColumn)
            Else
                dt.Columns.Remove(dt.DocTypeNameColumn)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub NewRecord() Implements IBusLayer.NewRecord

    End Sub

    Public Function Save() As String Implements IBusLayer.Save
        Try

            If _RecordState = TRecordStates.NewRecord Then
                Me.Attributes.LastEditDate = Now.Date
                Me.Attributes.LastEditBY = UserGUID
                Me.Attributes.CreationDate = Now.Date
                Me.Attributes.CreatedBy = UserGUID
                DataTable.AddDocumentNumbersRow(Attributes)
                If Adapter.Update(DataTable) = 1 Then
                    _RecordState = TRecordStates.ModifiedRecord
                    AddTransLog(Attributes.GUID, BaseBLL.UserGUID, TActionType.Add.ToString, DataTable.TableName)
                    Return Nothing
                Else
                    DataTable.RejectChanges()
                    Return My.Resources.BLLStrings.ErrorWhileSaving
                End If

            Else ' _RecordState = TRecordStates.ModifiedRecord
                Me.Attributes.LastEditDate = Now.Date
                Me.Attributes.LastEditBY = UserGUID
                If Adapter.Update(Attributes) = 1 Then
                    AddTransLog(Attributes.GUID, BaseBLL.UserGUID, TActionType.Edit.ToString, DataTable.TableName)
                    Return Nothing
                Else
                    Return My.Resources.BLLStrings.ErrorWhileSaving
                End If
            End If
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function GetDocumentCodeNumber(ByVal DocType As String, ByRef SN As String) As String
        'Try
        '    Dim dt As DataTable = Adapter.GetDataByDocType(DocType)
        '    If String.IsNullOrEmpty(SN) Then
        '        SN = dt.Rows(0)("SerialNumber") + 1
        '    End If
        '    Return String.Format("{0}{1}{2}", dt.Rows(0)("Prefix"), SN.ToString.PadLeft(dt.Rows(0)("DigitsCount"), "0"), dt.Rows(0)("Suffix"))
        'Catch ex As Exception
        '    Throw ex
        'End Try
        Try
            Dim dt As DataTable = Adapter.GetDataByDocType(DocType)
            SN = dt.Rows(0)("SerialNumber") + 1
            Return String.Format("{0}{1}{2}", dt.Rows(0)("Prefix"), SN.ToString.PadLeft(dt.Rows(0)("DigitsCount"), "0"), dt.Rows(0)("Suffix"))
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateSerialNumber(ByVal Sn As Integer, ByVal DocType As String)
        Try
            Adapter.UpdateSNByDOCType(Sn, UserGUID, DocType)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
