Imports System.Runtime.InteropServices
Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.XtraLayout
Imports GenericDAL
Imports System.Xml
Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing

Public Module GlobalFunctions
    Public valRulenotEmpty As New ConditionValidationRule
    Public valRuleGreaterThanZero As New ConditionValidationRule
    Public valRuleGreaterOrEqualZero As New ConditionValidationRule
    Public valRuleNotContainMinus As New ConditionValidationRule

    Public Sub InitVariables()
        valRulenotEmpty.ConditionOperator = ConditionOperator.IsNotBlank
        valRulenotEmpty.ErrorText = My.Resources.valRulenotEmpty
        valRulenotEmpty.ErrorType = ErrorType.Critical

        valRuleGreaterThanZero.ConditionOperator = ConditionOperator.Greater
        valRuleGreaterThanZero.ErrorText = My.Resources.valRuleGreaterThanZero
        valRuleGreaterThanZero.ErrorType = ErrorType.Critical
        valRuleGreaterThanZero.Value1 = 0

        valRuleGreaterOrEqualZero.ConditionOperator = ConditionOperator.GreaterOrEqual
        valRuleGreaterOrEqualZero.Value1 = 0
        valRuleGreaterOrEqualZero.ErrorText = My.Resources.valRuleGreaterOrEqualZero
        valRuleGreaterOrEqualZero.ErrorType = ErrorType.Critical

        valRuleNotContainMinus.ConditionOperator = ConditionOperator.NotContains
        valRuleNotContainMinus.Value1 = "-"
        valRuleNotContainMinus.ErrorText = My.Resources.valRuleNotContainMinus
        valRuleNotContainMinus.ErrorType = ErrorType.Critical
    End Sub

    Public Function WhoCalledMe() As String
        'StackFrame:
        'Provides information about the function call on the stack for the current process.

        'StackTrace:
        'Is a collection of all the StackFrames on the stack.

        'MethodBase:
        'Is an abstract class that contains information about currently executing method. 
        Dim st As StackTrace = New StackTrace()
        Dim sf As StackFrame = st.GetFrame(1)
        Dim mb As Reflection.MethodBase = sf.GetMethod()
        Return mb.Name
    End Function

    Dim DbType As String = "1"
    ' In Oracle All fields are uppercase, this is causing problem in the grid.
    Public Function GetGridRowCellDisplayValue(ByVal grv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal RowIndex As Integer, ByVal ColumnName As String) As Object

        If DbType = "1" Then 'SQL Server DBMS
            Return grv.GetRowCellDisplayText(RowIndex, ColumnName)
        Else 'Oracle DBMS
            Return grv.GetRowCellDisplayText(RowIndex, ColumnName.ToUpper)
        End If
    End Function

    Public Function GetGridRowCellValue(ByVal grv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal RowIndex As Integer, ByVal ColumnName As String) As Object

        If DbType = "1" Then 'SQL Server DBMS
            Return grv.GetRowCellValue(RowIndex, ColumnName)
        Else 'Oracle DBMS
            Return grv.GetRowCellValue(RowIndex, ColumnName.ToUpper)
        End If
    End Function

    Public Sub SetGridRowCellValue(ByVal grv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal RowIndex As Integer, ByVal ColumnName As String, ByVal Value As Object)
        If DbType = "1" Then 'SQL Server DBMS
            grv.SetRowCellValue(RowIndex, ColumnName, Value)
        Else 'Oracle DBMS
            grv.SetRowCellValue(RowIndex, ColumnName.ToUpper, Value)
        End If
    End Sub
    Public Function GetGridColumnName(ByVal ColumnName As String) As String
        If DbType = "1" Then 'SQL Server DBMS
            Return ColumnName
        Else 'Oracle DBMS
            Return ColumnName.ToUpper
        End If
    End Function

    Public Function GetGridColumn(ByVal grv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal ColumnName As String) As DevExpress.XtraGrid.Columns.GridColumn
        If DbType = "1" Then 'SQL Server DBMS
            Return grv.Columns(ColumnName)
        Else 'Oracle DBMS
            Return grv.Columns(ColumnName.ToUpper)
        End If
    End Function
    Public Function GenerateAssetID(ByVal CompanyCode As String, ByVal AssetNumber As String, ByVal SubNumber As String) As Long
        Dim AssetTag As String = CompanyCode & AssetNumber & SubNumber
        If AssetTag.Length <= 0 Then
            Return 0
        End If
        Try

            Dim Num As Long = Long.Parse(AssetTag)
            Return Num
        Catch ex As Exception
            Dim sb As New System.Text.StringBuilder
            For Each ch As Char In AssetTag
                If Char.IsDigit(ch) Then
                    sb.Append(ch)
                End If
            Next
            Return Long.Parse(sb.ToString)
        End Try
    End Function
End Module
