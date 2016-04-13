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
        Me.WebBrowser2 = New System.Windows.Forms.WebBrowser()
        Me.forwardButton = New System.Windows.Forms.Button()
        Me.backButton = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.FileCreationDate = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.URLFilterTextbox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pavaraghiCheckBox = New System.Windows.Forms.CheckBox()
        Me.wordCountCheckBox = New System.Windows.Forms.CheckBox()
        Me.ProcessButton = New System.Windows.Forms.Button()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.errortxtbox = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
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
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 478)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StatusStrip1.Size = New System.Drawing.Size(994, 22)
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
        'WebBrowser2
        '
        Me.WebBrowser2.Location = New System.Drawing.Point(7, 322)
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
        Me.forwardButton.Location = New System.Drawing.Point(59, 98)
        Me.forwardButton.Name = "forwardButton"
        Me.forwardButton.Size = New System.Drawing.Size(48, 44)
        Me.forwardButton.TabIndex = 11
        Me.forwardButton.UseVisualStyleBackColor = True
        '
        'backButton
        '
        Me.backButton.BackgroundImage = Global.Report.My.Resources.Resources.back
        Me.backButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.backButton.Location = New System.Drawing.Point(6, 98)
        Me.backButton.Name = "backButton"
        Me.backButton.Size = New System.Drawing.Size(48, 44)
        Me.backButton.TabIndex = 11
        Me.backButton.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.FileCreationDate)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.URLFilterTextbox)
        Me.GroupBox2.Controls.Add(Me.PathTxt)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.pavaraghiCheckBox)
        Me.GroupBox2.Controls.Add(Me.BrowseButton)
        Me.GroupBox2.Controls.Add(Me.wordCountCheckBox)
        Me.GroupBox2.Controls.Add(Me.ProcessButton)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(677, 87)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        '
        'FileCreationDate
        '
        Me.FileCreationDate.FormattingEnabled = True
        Me.FileCreationDate.Items.AddRange(New Object() {"2016", "2015", "2014", "2013", "2012", "2011"})
        Me.FileCreationDate.Location = New System.Drawing.Point(486, 18)
        Me.FileCreationDate.Name = "FileCreationDate"
        Me.FileCreationDate.Size = New System.Drawing.Size(91, 21)
        Me.FileCreationDate.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(355, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(124, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "سال یا دایرکتوری استاد :"
        '
        'URLFilterTextbox
        '
        Me.URLFilterTextbox.Location = New System.Drawing.Point(258, 18)
        Me.URLFilterTextbox.Name = "URLFilterTextbox"
        Me.URLFilterTextbox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.URLFilterTextbox.Size = New System.Drawing.Size(91, 20)
        Me.URLFilterTextbox.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(583, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = " سال ایجاد فایل :" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'pavaraghiCheckBox
        '
        Me.pavaraghiCheckBox.AutoSize = True
        Me.pavaraghiCheckBox.Checked = True
        Me.pavaraghiCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.pavaraghiCheckBox.Location = New System.Drawing.Point(476, 57)
        Me.pavaraghiCheckBox.Name = "pavaraghiCheckBox"
        Me.pavaraghiCheckBox.Size = New System.Drawing.Size(96, 17)
        Me.pavaraghiCheckBox.TabIndex = 12
        Me.pavaraghiCheckBox.Text = "شمارش پاورقی"
        Me.pavaraghiCheckBox.UseVisualStyleBackColor = True
        '
        'wordCountCheckBox
        '
        Me.wordCountCheckBox.AutoSize = True
        Me.wordCountCheckBox.Location = New System.Drawing.Point(578, 57)
        Me.wordCountCheckBox.Name = "wordCountCheckBox"
        Me.wordCountCheckBox.Size = New System.Drawing.Size(88, 17)
        Me.wordCountCheckBox.TabIndex = 12
        Me.wordCountCheckBox.Text = "شمارش کلمات"
        Me.wordCountCheckBox.UseVisualStyleBackColor = True
        '
        'ProcessButton
        '
        Me.ProcessButton.Location = New System.Drawing.Point(6, 44)
        Me.ProcessButton.Name = "ProcessButton"
        Me.ProcessButton.Size = New System.Drawing.Size(77, 30)
        Me.ProcessButton.TabIndex = 8
        Me.ProcessButton.Text = "تهیه گزارش"
        Me.ProcessButton.UseVisualStyleBackColor = True
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(6, 129)
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
        Me.ClientSize = New System.Drawing.Size(994, 500)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.FileSystemTreeView)
        Me.Name = "ReportMainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ReportMainForm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

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
    Friend WithEvents ProcessButton As System.Windows.Forms.Button
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents URLFilterTextbox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents wordCountCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents backButton As System.Windows.Forms.Button
    Friend WithEvents forwardButton As System.Windows.Forms.Button
    Friend WithEvents errorLBL As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents errortxtbox As System.Windows.Forms.TextBox
    Friend WithEvents pavaraghiCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents WebBrowser2 As System.Windows.Forms.WebBrowser
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents FileCreationDate As System.Windows.Forms.ComboBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ToolStripProgressBar

End Class
