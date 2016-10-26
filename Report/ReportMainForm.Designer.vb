<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportMainForm
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
        Me.FileSystemTreeView = New System.Windows.Forms.TreeView()
        Me.PathTxt = New System.Windows.Forms.TextBox()
        Me.BrowseButton = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.FileCountLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel5 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel6 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.FolderCountLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.errorLBL = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.WebBrowser2 = New System.Windows.Forms.WebBrowser()
        Me.forwardButton = New System.Windows.Forms.Button()
        Me.backButton = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cleardbBtn = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.runSQL = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.sqlCMD = New System.Windows.Forms.TextBox()
        Me.lessonFilter = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.YearFilter = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.MasterFilter = New System.Windows.Forms.TextBox()
        Me.FiletypeErrorShow = New System.Windows.Forms.Button()
        Me.NewReport = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.pavaraghiCheckBox = New System.Windows.Forms.CheckBox()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.errortxtbox = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'FileSystemTreeView
        '
        Me.FileSystemTreeView.Location = New System.Drawing.Point(0, 4)
        Me.FileSystemTreeView.Name = "FileSystemTreeView"
        Me.FileSystemTreeView.Size = New System.Drawing.Size(283, 362)
        Me.FileSystemTreeView.TabIndex = 0
        '
        'PathTxt
        '
        Me.PathTxt.Location = New System.Drawing.Point(6, 18)
        Me.PathTxt.Name = "PathTxt"
        Me.PathTxt.Size = New System.Drawing.Size(165, 20)
        Me.PathTxt.TabIndex = 1
        '
        'BrowseButton
        '
        Me.BrowseButton.Location = New System.Drawing.Point(177, 16)
        Me.BrowseButton.Name = "BrowseButton"
        Me.BrowseButton.Size = New System.Drawing.Size(75, 23)
        Me.BrowseButton.TabIndex = 3
        Me.BrowseButton.Text = "انتخاب مسیر"
        Me.BrowseButton.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.StatusLabel, Me.ToolStripStatusLabel2, Me.ToolStripStatusLabel3, Me.FileCountLabel, Me.ToolStripStatusLabel5, Me.ToolStripStatusLabel6, Me.FolderCountLabel, Me.errorLBL, Me.ProgressBar1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 486)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StatusStrip1.Size = New System.Drawing.Size(989, 22)
        Me.StatusStrip1.TabIndex = 5
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(53, 17)
        Me.ToolStripStatusLabel1.Text = "وضعیت :"
        '
        'StatusLabel
        '
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(32, 17)
        Me.StatusLabel.Text = "آماده"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(10, 17)
        Me.ToolStripStatusLabel2.Text = "|"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(80, 17)
        Me.ToolStripStatusLabel3.Text = "تعداد فایل ها : "
        '
        'FileCountLabel
        '
        Me.FileCountLabel.Name = "FileCountLabel"
        Me.FileCountLabel.Size = New System.Drawing.Size(12, 17)
        Me.FileCountLabel.Text = "0"
        '
        'ToolStripStatusLabel5
        '
        Me.ToolStripStatusLabel5.Name = "ToolStripStatusLabel5"
        Me.ToolStripStatusLabel5.Size = New System.Drawing.Size(10, 17)
        Me.ToolStripStatusLabel5.Text = "|"
        '
        'ToolStripStatusLabel6
        '
        Me.ToolStripStatusLabel6.Name = "ToolStripStatusLabel6"
        Me.ToolStripStatusLabel6.Size = New System.Drawing.Size(91, 17)
        Me.ToolStripStatusLabel6.Text = "تعداد دایرکتوری : "
        '
        'FolderCountLabel
        '
        Me.FolderCountLabel.Name = "FolderCountLabel"
        Me.FolderCountLabel.Size = New System.Drawing.Size(12, 17)
        Me.FolderCountLabel.Text = "0"
        '
        'errorLBL
        '
        Me.errorLBL.Name = "errorLBL"
        Me.errorLBL.Size = New System.Drawing.Size(0, 17)
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(100, 16)
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DataGridView1)
        Me.GroupBox1.Controls.Add(Me.WebBrowser2)
        Me.GroupBox1.Controls.Add(Me.forwardButton)
        Me.GroupBox1.Controls.Add(Me.backButton)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.WebBrowser1)
        Me.GroupBox1.Location = New System.Drawing.Point(289, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(690, 481)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(7, 197)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(665, 150)
        Me.DataGridView1.TabIndex = 13
        '
        'WebBrowser2
        '
        Me.WebBrowser2.Location = New System.Drawing.Point(7, 353)
        Me.WebBrowser2.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser2.Name = "WebBrowser2"
        Me.WebBrowser2.ScriptErrorsSuppressed = True
        Me.WebBrowser2.Size = New System.Drawing.Size(677, 109)
        Me.WebBrowser2.TabIndex = 12
        Me.WebBrowser2.Visible = False
        '
        'forwardButton
        '
        Me.forwardButton.BackgroundImage = Global.Report.My.Resources.Resources.forward
        Me.forwardButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.forwardButton.Location = New System.Drawing.Point(60, 353)
        Me.forwardButton.Name = "forwardButton"
        Me.forwardButton.Size = New System.Drawing.Size(48, 44)
        Me.forwardButton.TabIndex = 11
        Me.forwardButton.UseVisualStyleBackColor = True
        '
        'backButton
        '
        Me.backButton.BackgroundImage = Global.Report.My.Resources.Resources.back
        Me.backButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.backButton.Location = New System.Drawing.Point(7, 353)
        Me.backButton.Name = "backButton"
        Me.backButton.Size = New System.Drawing.Size(48, 44)
        Me.backButton.TabIndex = 11
        Me.backButton.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cleardbBtn)
        Me.GroupBox2.Controls.Add(Me.Button2)
        Me.GroupBox2.Controls.Add(Me.runSQL)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.PictureBox1)
        Me.GroupBox2.Controls.Add(Me.sqlCMD)
        Me.GroupBox2.Controls.Add(Me.lessonFilter)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.YearFilter)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.MasterFilter)
        Me.GroupBox2.Controls.Add(Me.FiletypeErrorShow)
        Me.GroupBox2.Controls.Add(Me.NewReport)
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Controls.Add(Me.PathTxt)
        Me.GroupBox2.Controls.Add(Me.pavaraghiCheckBox)
        Me.GroupBox2.Controls.Add(Me.BrowseButton)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(677, 138)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        '
        'cleardbBtn
        '
        Me.cleardbBtn.Location = New System.Drawing.Point(526, 48)
        Me.cleardbBtn.Name = "cleardbBtn"
        Me.cleardbBtn.Size = New System.Drawing.Size(139, 24)
        Me.cleardbBtn.TabIndex = 26
        Me.cleardbBtn.Text = "پاک کردن دیتاپیس"
        Me.cleardbBtn.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(526, 78)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(139, 23)
        Me.Button2.TabIndex = 25
        Me.Button2.Text = "ایجاد فایل گزارش"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'runSQL
        '
        Me.runSQL.Location = New System.Drawing.Point(378, 112)
        Me.runSQL.Name = "runSQL"
        Me.runSQL.Size = New System.Drawing.Size(34, 20)
        Me.runSQL.TabIndex = 24
        Me.runSQL.Text = ">>"
        Me.runSQL.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(54, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "T-SQL Query :"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Report.My.Resources.Resources.db
        Me.PictureBox1.Location = New System.Drawing.Point(6, 89)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(42, 43)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 22
        Me.PictureBox1.TabStop = False
        '
        'sqlCMD
        '
        Me.sqlCMD.Location = New System.Drawing.Point(54, 112)
        Me.sqlCMD.Name = "sqlCMD"
        Me.sqlCMD.Size = New System.Drawing.Size(318, 20)
        Me.sqlCMD.TabIndex = 21
        Me.sqlCMD.Text = "SELECT * FROM Course WHERE Year=N'جاری'"
        '
        'lessonFilter
        '
        Me.lessonFilter.Location = New System.Drawing.Point(224, 55)
        Me.lessonFilter.Name = "lessonFilter"
        Me.lessonFilter.Size = New System.Drawing.Size(119, 20)
        Me.lessonFilter.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(186, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "درس :"
        '
        'YearFilter
        '
        Me.YearFilter.Location = New System.Drawing.Point(401, 56)
        Me.YearFilter.Name = "YearFilter"
        Me.YearFilter.Size = New System.Drawing.Size(119, 20)
        Me.YearFilter.TabIndex = 19
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(357, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "سال :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "استاد :"
        '
        'MasterFilter
        '
        Me.MasterFilter.Location = New System.Drawing.Point(52, 55)
        Me.MasterFilter.Name = "MasterFilter"
        Me.MasterFilter.Size = New System.Drawing.Size(119, 20)
        Me.MasterFilter.TabIndex = 16
        '
        'FiletypeErrorShow
        '
        Me.FiletypeErrorShow.Location = New System.Drawing.Point(267, 12)
        Me.FiletypeErrorShow.Name = "FiletypeErrorShow"
        Me.FiletypeErrorShow.Size = New System.Drawing.Size(105, 30)
        Me.FiletypeErrorShow.TabIndex = 15
        Me.FiletypeErrorShow.Text = "خطای فایل متن"
        Me.FiletypeErrorShow.UseVisualStyleBackColor = True
        '
        'NewReport
        '
        Me.NewReport.Location = New System.Drawing.Point(526, 12)
        Me.NewReport.Name = "NewReport"
        Me.NewReport.Size = New System.Drawing.Size(139, 30)
        Me.NewReport.TabIndex = 14
        Me.NewReport.Text = "بروزرسانی پایگاه داده"
        Me.NewReport.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(378, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(142, 30)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "doc جستجوی فایل های"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'pavaraghiCheckBox
        '
        Me.pavaraghiCheckBox.AutoSize = True
        Me.pavaraghiCheckBox.Checked = True
        Me.pavaraghiCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.pavaraghiCheckBox.Location = New System.Drawing.Point(569, 112)
        Me.pavaraghiCheckBox.Name = "pavaraghiCheckBox"
        Me.pavaraghiCheckBox.Size = New System.Drawing.Size(96, 17)
        Me.pavaraghiCheckBox.TabIndex = 12
        Me.pavaraghiCheckBox.Text = "شمارش پاورقی"
        Me.pavaraghiCheckBox.UseVisualStyleBackColor = True
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(7, 353)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.ScriptErrorsSuppressed = True
        Me.WebBrowser1.Size = New System.Drawing.Size(677, 109)
        Me.WebBrowser1.TabIndex = 9
        '
        'errortxtbox
        '
        Me.errortxtbox.Location = New System.Drawing.Point(6, 19)
        Me.errortxtbox.Multiline = True
        Me.errortxtbox.Name = "errortxtbox"
        Me.errortxtbox.Size = New System.Drawing.Size(267, 75)
        Me.errortxtbox.TabIndex = 13
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.errortxtbox)
        Me.GroupBox3.Location = New System.Drawing.Point(4, 372)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(279, 100)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "خروجی خطا"
        '
        'ReportMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(989, 508)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.FileSystemTreeView)
        Me.Name = "ReportMainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BaharSound Report 95.08.05"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FileSystemTreeView As System.Windows.Forms.TreeView
    Friend WithEvents PathTxt As System.Windows.Forms.TextBox
    Friend WithEvents BrowseButton As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents FileCountLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel5 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel6 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents FolderCountLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents backButton As System.Windows.Forms.Button
    Friend WithEvents forwardButton As System.Windows.Forms.Button
    Friend WithEvents errorLBL As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents errortxtbox As System.Windows.Forms.TextBox
    Friend WithEvents pavaraghiCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents WebBrowser2 As System.Windows.Forms.WebBrowser
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents NewReport As System.Windows.Forms.Button
    Friend WithEvents FiletypeErrorShow As System.Windows.Forms.Button
    Friend WithEvents MasterFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents YearFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lessonFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents sqlCMD As System.Windows.Forms.TextBox
    Friend WithEvents runSQL As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents cleardbBtn As System.Windows.Forms.Button

End Class
