Imports System
Imports System.Data.OleDb
Imports ZulAssetsDAL.ZulAssetsDAL

Namespace ZulAssetBAL
    Public Class BALAst_INV_Schedule
        Inherits BLBase

#Region "Data Members"
        Private objAst_INV_Schedule As InvSchedule
#End Region

        Public Sub New()
            objAst_INV_Schedule = New InvSchedule
        End Sub

#Region "Functions"
        Public Sub CreateDefaultRecord()
            Dim ds As DataTable
            Dim deletenw As Boolean = False
            Dim objatt As New attInvSchedule
            ds = GetAll_invSch(objatt)
            If ds Is Nothing = False Then
                If ds.Rows.Count < 1 Then
                    If deletenw = False Then
                        objatt.PKeyCode = "1"
                        Delete_invSch(objatt)
                        deletenw = True
                    End If
                    objatt = New attInvSchedule
                    objatt.PKeyCode = "1"
                    objatt.InvDesc = "System Inventory"
                    objatt.InvStartDate = Now.Date
                    objatt.InvEndDate = objatt.InvStartDate.AddYears(50)
                    Insert_invSch(objatt)
                End If
            End If
        End Sub

        Public Function Insert_invSch(ByVal objattAst_INV_Schedule As attInvSchedule) As Boolean
            Try
                objAst_INV_Schedule.Attribute = objattAst_INV_Schedule
                Me.Insert(objAst_INV_Schedule)
                'insert all Assets to Ast History for this schedule.


                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Update_invSch(ByVal objattAst_INV_Schedule As attInvSchedule) As Boolean
            Try
                objAst_INV_Schedule.Attribute = objattAst_INV_Schedule
                Me.Update(objAst_INV_Schedule)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetNextPKey_invSch() As String
            Try
                Return Me.GetNextPKey(objAst_INV_Schedule)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll_invSchID(ByVal ID As String) As DataTable
            Try
                Return Me.GeneralExecuter(objAst_INV_Schedule.GetAll_invSchID(ID))
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function GetAll_invSch(ByVal objattAst_INV_Schedule As attInvSchedule) As DataTable
            Try
                objAst_INV_Schedule.Attribute = objattAst_INV_Schedule
                Return GetAllData(objAst_INV_Schedule)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Delete_invSch(ByVal objattAst_INV_Schedule As attInvSchedule) As Boolean
            Try
                objAst_INV_Schedule.Attribute = objattAst_INV_Schedule
                Me.Delete(objAst_INV_Schedule)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Close_Schedule(ByVal objattAst_INV_Schedule As attInvSchedule) As DataTable
            Try
                objAst_INV_Schedule.Attribute = objattAst_INV_Schedule
                Return Me.GeneralExecuter(objAst_INV_Schedule.Close_Schedule())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function CheckID(ByVal objattAst_INV_Schedule As attInvSchedule) As DataTable
            Try
                objAst_INV_Schedule.Attribute = objattAst_INV_Schedule
                Return Me.GeneralExecuter(objAst_INV_Schedule.CheckID())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Getcombo_Schedule() As DataTable
            Try
                Return Me.GeneralExecuter(objAst_INV_Schedule.GetAllData_Combo())
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Get_ScheduleType(ByVal objattAst_INV_Schedule As attInvSchedule) As Integer
            Try
                objAst_INV_Schedule.Attribute = objattAst_INV_Schedule
                Return Me.GeneralExecuter_Scalar(objAst_INV_Schedule.GetInventoryType(), "")
            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region
    End Class
End Namespace

