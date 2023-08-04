<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDepProgress
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblAllBookDepRec = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.pbAllBooks = New System.Windows.Forms.ProgressBar
        Me.lstReport = New System.Windows.Forms.ListBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.pbCurrentBook = New System.Windows.Forms.ProgressBar
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblBookDepRec = New System.Windows.Forms.Label
        Me.lblBookID = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblAllBookDepRec
        '
        Me.lblAllBookDepRec.BackColor = System.Drawing.Color.Transparent
        Me.lblAllBookDepRec.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblAllBookDepRec.ForeColor = System.Drawing.Color.Navy
        Me.lblAllBookDepRec.Location = New System.Drawing.Point(338, 64)
        Me.lblAllBookDepRec.Name = "lblAllBookDepRec"
        Me.lblAllBookDepRec.Size = New System.Drawing.Size(79, 13)
        Me.lblAllBookDepRec.TabIndex = 77
        Me.lblAllBookDepRec.Text = "0"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(213, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(117, 13)
        Me.Label6.TabIndex = 76
        Me.Label6.Text = "Depreciated Assets"
        '
        'pbAllBooks
        '
        Me.pbAllBooks.BackColor = System.Drawing.Color.White
        Me.pbAllBooks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pbAllBooks.Location = New System.Drawing.Point(22, 36)
        Me.pbAllBooks.Name = "pbAllBooks"
        Me.pbAllBooks.Size = New System.Drawing.Size(402, 20)
        Me.pbAllBooks.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.pbAllBooks.TabIndex = 66
        Me.pbAllBooks.Visible = False
        '
        'lstReport
        '
        Me.lstReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstReport.FormattingEnabled = True
        Me.lstReport.ItemHeight = 14
        Me.lstReport.Location = New System.Drawing.Point(15, 19)
        Me.lstReport.Name = "lstReport"
        Me.lstReport.Size = New System.Drawing.Size(403, 130)
        Me.lstReport.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.lstReport)
        Me.GroupBox1.Location = New System.Drawing.Point(24, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(434, 166)
        Me.GroupBox1.TabIndex = 80
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Depreciation Status"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.White
        Me.GroupBox2.Controls.Add(Me.pbAllBooks)
        Me.GroupBox2.Controls.Add(Me.lblAllBookDepRec)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Location = New System.Drawing.Point(24, 268)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(434, 86)
        Me.GroupBox2.TabIndex = 81
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Overall Progress"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.White
        Me.GroupBox3.Controls.Add(Me.pbCurrentBook)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.lblBookDepRec)
        Me.GroupBox3.Controls.Add(Me.lblBookID)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Location = New System.Drawing.Point(24, 176)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(434, 86)
        Me.GroupBox3.TabIndex = 82
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Current Progress"
        '
        'pbCurrentBook
        '
        Me.pbCurrentBook.BackColor = System.Drawing.Color.White
        Me.pbCurrentBook.ForeColor = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pbCurrentBook.Location = New System.Drawing.Point(22, 29)
        Me.pbCurrentBook.Name = "pbCurrentBook"
        Me.pbCurrentBook.Size = New System.Drawing.Size(402, 20)
        Me.pbCurrentBook.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.pbCurrentBook.TabIndex = 76
        Me.pbCurrentBook.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(20, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 13)
        Me.Label3.TabIndex = 79
        Me.Label3.Text = "Depreciating Book"
        '
        'lblBookDepRec
        '
        Me.lblBookDepRec.BackColor = System.Drawing.Color.Transparent
        Me.lblBookDepRec.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblBookDepRec.ForeColor = System.Drawing.Color.Navy
        Me.lblBookDepRec.Location = New System.Drawing.Point(338, 59)
        Me.lblBookDepRec.Name = "lblBookDepRec"
        Me.lblBookDepRec.Size = New System.Drawing.Size(79, 13)
        Me.lblBookDepRec.TabIndex = 78
        Me.lblBookDepRec.Text = "0"
        '
        'lblBookID
        '
        Me.lblBookID.BackColor = System.Drawing.Color.Transparent
        Me.lblBookID.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblBookID.ForeColor = System.Drawing.Color.Navy
        Me.lblBookID.Location = New System.Drawing.Point(134, 59)
        Me.lblBookID.Name = "lblBookID"
        Me.lblBookID.Size = New System.Drawing.Size(73, 13)
        Me.lblBookID.TabIndex = 80
        Me.lblBookID.Text = "0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(213, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 13)
        Me.Label1.TabIndex = 77
        Me.Label1.Text = "Depreciated Assets"
        '
        'frmDepProgress
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(482, 365)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDepProgress"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Depreciation Progress"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents pbAllBooks As System.Windows.Forms.ProgressBar
    Public WithEvents lstReport As System.Windows.Forms.ListBox
    Friend WithEvents lblAllBookDepRec As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents pbCurrentBook As System.Windows.Forms.ProgressBar
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblBookDepRec As System.Windows.Forms.Label
    Friend WithEvents lblBookID As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
