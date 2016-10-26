Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Data.SqlClient

Public Class ReportMainForm
    Dim Monitoring As New DataSet
    Dim textcount As Long
    Dim voiceCount As Long
    Dim archiveCount As Long
    Dim folderCount As Long
    Dim CurrDir As String
    Dim ReportPath As String
    Public textFiles As New List(Of String)
    Dim archiveFiles As New List(Of String)
    Dim voiceFiles As New List(Of String)
    Public docFiles As New List(Of String)
    Dim MASTERS_folder As New List(Of String)
    Dim MASTERS_name As New List(Of String)
    Dim MASTERS_priority As New List(Of String)
    Dim OstadList As New List(Of String)
    Dim ArabicCurrentYear As String = ""
    Dim PersianCurrentYear As String = ""
    Public cString As String = ""
    Public Function CountCharacter(ByVal value As String, ByVal ch As Char) As Integer
        Dim cnt As Integer = 0
        For Each c As Char In value
            If c = ch Then cnt += 1
        Next
        Return cnt
    End Function
    Function GetChildren(parentNode As TreeNode) As List(Of TreeNode)
        On Error Resume Next
        Dim nodes As List(Of TreeNode) = New List(Of TreeNode)
        GetAllChildren(parentNode, nodes)
        Return nodes
    End Function
    Sub GetAllChildren(parentNode As TreeNode, nodes As List(Of TreeNode))
        On Error Resume Next
        For Each childNode As TreeNode In parentNode.Nodes
            My.Application.DoEvents()
            nodes.Add(childNode)
            GetAllChildren(childNode, nodes)
        Next
    End Sub
    Private Sub PopulateTreeView(ByVal dir As String, ByVal parentNode As TreeNode)
        Dim folder As String = String.Empty
        Try
            ' My.Application.DoEvents()
            Dim folders() As String = IO.Directory.GetDirectories(dir)
            If folders.Length <> 0 Then
                Dim childNode As TreeNode = Nothing
                Dim chnode As TreeNode = Nothing
                Dim pnode As TreeNode = Nothing
                For Each folder In folders
                    My.Application.DoEvents()
                    childNode = New TreeNode(folder)
                    parentNode.Nodes.Add(childNode)
                    folderCount += 1
                    PopulateTreeView(folder, childNode)
                Next
            End If
        Catch ex As UnauthorizedAccessException
            parentNode.Nodes.Add(folder & ": Access Denied")
        End Try
    End Sub
    Private Sub ReportMainForm_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        FileOpen(1, "log.inf", OpenMode.Output, OpenAccess.Default)
        PrintLine(1, errortxtbox.Text)
        FileClose(1)
    End Sub
    Private Sub ReportMainForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Try
            FileSystemTreeView.Width = Me.Width / 4
            FileSystemTreeView.Height = Me.Height - StatusStrip1.Height - GroupBox3.Height - 45
            GroupBox3.Top = FileSystemTreeView.Top + FileSystemTreeView.Height + 2
            GroupBox3.Width = FileSystemTreeView.Width - 3
            GroupBox1.Width = Me.Width * 3 / 4 - 40
            errortxtbox.Width = GroupBox3.Width - 10
            GroupBox1.Left = Me.Width - GroupBox1.Width - 20
            GroupBox1.Height = Me.Height - 70
            GroupBox1.Left = Me.Width - GroupBox1.Width - 30
            GroupBox2.Left = GroupBox1.Width - GroupBox2.Width - 3
            WebBrowser1.Width = GroupBox1.Width - 30
            WebBrowser1.Height = GroupBox1.Height - WebBrowser1.Top - 20
            backButton.Top = WebBrowser1.Top
            backButton.Left = WebBrowser1.Left + 15
            forwardButton.Top = backButton.Top
            forwardButton.Left = backButton.Left + forwardButton.Width + 2
            DataGridView1.Width = GroupBox1.Width - 30
            DataGridView1.Height = GroupBox1.Height * 0.75
            DataGridView1.Top = GroupBox2.Top + GroupBox2.Height + 10
        Catch ex As Exception
            errortxtbox.AppendText(vbNewLine + ex.Message + ex.Source + ex.StackTrace)
        End Try
    End Sub
    Public Sub hidereportwindow()
        WebBrowser1.Visible = False
        backButton.Visible = False
        forwardButton.Visible = False
    End Sub
    Public Sub showreportwindow()
        WebBrowser1.Visible = True
        backButton.Visible = True
        forwardButton.Visible = True
    End Sub
    Private Sub ReadFileSystem()
        Try
            cleardbBtn.Enabled = False
            NewReport.Enabled = False
            archiveCount = 0
            voiceCount = 0
            textcount = 0
            folderCount = 0
            archiveFiles.Clear()
            textFiles.Clear()
            voiceFiles.Clear()
            My.Application.DoEvents()
            StatusLabel.Text = "خواندن سیستم فایل"
            StatusLabel.ForeColor = Color.Red
            Loading.Show()
            FileSystemTreeView.Nodes.Clear()
            FileSystemTreeView.Update()
            FileSystemTreeView.UseWaitCursor = True
            Dim root As TreeNode = FileSystemTreeView.Nodes.Add(FolderBrowserDialog1.SelectedPath.ToString)
            PopulateTreeView(PathTxt.Text, root)
            Dim allfolders As New List(Of TreeNode)
            GetAllChildren(FileSystemTreeView.Nodes(0), allfolders)
            For Each folder In allfolders
                Dim files = My.Computer.FileSystem.GetFiles(folder.Text)
                For Each file In files
                    Dim infoReader As System.IO.FileInfo
                    infoReader = My.Computer.FileSystem.GetFileInfo(file)
                    ''If infoReader.CreationTime.Year >= Val(FileCreationDate.Text) Then
                    ' folder.Nodes.Add(file)
                    If file.ToLower.Contains("\text\") Then
                        textFiles.Add(file)
                        textcount += 1
                    ElseIf file.ToLower.Contains("\voice\") Then
                        voiceFiles.Add(file)
                        voiceCount += 1
                    ElseIf file.ToLower.Contains("\_word\") Then
                        docFiles.Add(file)
                    ElseIf file.ToLower.Contains("\archive\") Then
                        archiveCount += 1
                        archiveFiles.Add(file)
                    End If
                    ''  End If
                Next
            Next
            FileSystemTreeView.Update()
            FileSystemTreeView.Nodes(0).Expand()
            StatusLabel.Text = "آماده"
            StatusLabel.ForeColor = Color.Green
            FileCountLabel.Text = (archiveCount + textcount + voiceCount).ToString
            FolderCountLabel.Text = folderCount.ToString
            FileSystemTreeView.UseWaitCursor = False
            Loading.Close()
            ProgressBar1.Maximum = archiveCount + textcount + voiceCount
            ' ProgressBar1.Hide()
            cleardbBtn.Enabled = True
            NewReport.Enabled = True
        Catch ex As Exception
            errortxtbox.AppendText(vbNewLine + ex.Message + ex.StackTrace + vbNewLine)
        End Try
    End Sub
    Private Sub backButton_Click(sender As Object, e As EventArgs) Handles backButton.Click
        If WebBrowser1.CanGoBack = True Then
            WebBrowser1.GoBack()
        End If
    End Sub
    Private Sub forwardButton_Click(sender As Object, e As EventArgs) Handles forwardButton.Click
        If WebBrowser1.CanGoForward = True Then
            WebBrowser1.GoForward()
        End If
    End Sub
    Private Sub BrowseButton_Click(sender As Object, e As EventArgs) Handles BrowseButton.Click
        FolderBrowserDialog1.ShowDialog()
        If (FolderBrowserDialog1.SelectedPath <> "") Then
            PathTxt.Text = FolderBrowserDialog1.SelectedPath.ToString
            ReadFileSystem()
        End If
        ProgressBar1.Maximum = archiveCount + textcount + voiceCount
    End Sub
    Private Sub FileSystemTreeView_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles FileSystemTreeView.BeforeExpand
        My.Application.DoEvents()
    End Sub
    Private Sub ReportMainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If My.Computer.FileSystem.FileExists(Application.StartupPath & "\instance.dll") Then
                'You can change the extension of the file to what ever you desire ex: dll, xyz etc.
                MsgBox("Only one Instance of the application is allowed!!!")
                Environment.Exit(0)
            Else
                File.Create(Application.StartupPath & "\instance.dll", 10, FileOptions.DeleteOnClose)
            End If
            Loading.Show()
            CurrDir = CurDir()
            folderCount = 0
            textcount = 0
            voiceCount = 0
            archiveCount = 0
            If My.Computer.FileSystem.FileExists(CurDir() + "/config.inf") Then
                FileOpen(1, CurDir() + "/config.inf", OpenMode.Input, OpenAccess.Default)
                While Not EOF(1)
                    Dim input = LineInput(1)
                    Select Case input
                        Case "#PATH"
                            PathTxt.Text = LineInput(1)
                        Case "#REPORTPATH"
                            ReportPath = LineInput(1)
                        Case "#CONNECTIONSTRING"
                            cString = LineInput(1)
                        Case "#ArabicCurrentYear"
                            ArabicCurrentYear = LineInput(1)
                        Case "#PersianCurrentYear"
                            PersianCurrentYear = LineInput(1)
                    End Select
                End While
                If (PersianCurrentYear.ToLower = "jari") Then
                    PersianCurrentYear = "جاری"
                End If
                FileClose(1)
            End If
            If My.Computer.FileSystem.FileExists(CurDir() + "\monitoring.xml") Then
                Monitoring.ReadXml(CurDir() + "\monitoring.xml")
            End If
            For Each row As DataRow In Monitoring.Tables("list").Rows
                If row("path").ToString.Contains(".\") Then
                    Dim mastr As String = row("path").ToString.Replace(".\", "")
                    mastr = mastr.Substring(0, mastr.IndexOf("\"))
                    If OstadList.IndexOf(mastr) < 0 Then
                        OstadList.Add(mastr)
                    End If
                End If
                If row("path").ToString.Contains("..\..\") Then
                    Dim mastr As String = row("path").ToString.ToLower.Replace("..\..\", "")
                    mastr = mastr.Replace("ar\feqh\archive\", "")
                    mastr = mastr.Substring(0, mastr.IndexOf("\"))
                    If OstadList.IndexOf(mastr) < 0 Then
                        OstadList.Add(mastr)
                        errortxtbox.AppendText(mastr + vbNewLine)
                    End If
                End If
            Next
            hidereportwindow()
            Loading.Hide()
        Catch ex As Exception
            errortxtbox.AppendText(vbNewLine + ex.Message + ex.Source + ex.StackTrace)
        End Try
    End Sub

    Private Sub ReportMainForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ReadFileSystem()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DocReport.Show()
    End Sub

    Private Sub NewReport_Click(sender As Object, e As EventArgs) Handles NewReport.Click
        Loading.Show()
        ClearDb()
        ProgressBar1.Maximum = OstadList.Count
        If MasterFilter.Text <> "" Then
            ProgressBar1.Maximum = textFiles.Count
        End If
        StatusLabel.Text = "تهیه گزارش"
        StatusLabel.ForeColor = Color.Blue
        NewReport.Enabled = False
        BrowseButton.Enabled = False
        MasterFilter.Enabled = False
        YearFilter.Enabled = False
        lessonFilter.Enabled = False
        cleardbBtn.Enabled = False
        Dim lastMaster As String = ""
        For Each master In OstadList
            Try
                If MasterFilter.Text <> "" And master.ToLower <> MasterFilter.Text.ToLower Then
                    Continue For
                End If
                If ProgressBar1.Maximum - ProgressBar1.Value - 1 > 0 And MasterFilter.Text = "" Then
                    ProgressBar1.Value += 1
                End If
                Dim cht2 As New Chart
                cht2.Width = 1360
                cht2.Height = 500
                Dim chart2x As Integer = 2
                Dim MasterName = ""
                Dim detailedReportTableRows As String = ""
                Dim radif As Integer = 0
                Dim totalvoicecount As Integer = 0
                Dim totaltextcount As Integer = 0
                Dim totalpavaraghi As Integer = 0
                Dim totalpavaraghilinked As Integer = 0
                Dim totalPavaraghiError As Integer = 0
                Dim totalshahedmesal As Integer = 0
                Dim majmoovoice As Integer = 0
                Dim majmootext As Integer = 0
                Dim majmoopavaraghi As Integer = 0
                Dim majmoopavaraghierror As Integer = 0
                Dim majmoopavaraghilinked As Integer = 0
                Dim majmooshahedmesal As Integer = 0
                Dim partialmajmoovoice As Integer = 0
                Dim partialmajmootext As Integer = 0
                Dim partialmajmoopavaraghi As Integer = 0
                Dim partialmajmoopavaraghilinked As Integer = 0
                Dim partialmajmoopavaraghierror As Integer = 0
                Dim partialmajmooshahedmesal As Integer = 0
                ' Dim partialReport As String = "<table class='report2'><tr class='report2'><th class='report2'><label class='radif'>ردیف</label></th><th class='report2'><label class='dars'>درس</label></th><th class='report2'><label class='soat'>صوت</label></th><th class='report2'><label class='matn'>متن</label> </th><th class='report2'>پاورقی</th><th class='report2'>پاورقی لینک شده</th><th class='report2'>پاورقی بدون لینک</th><th class='report2'>بدون شاهد مثال</th></tr>"
                'Dim partialReportNum As Integer = 0
                Dim LastMozoo As String = ""
                For Each row As DataRow In Monitoring.Tables("list").Rows
                    ' Dim pavaraghiErrorFileReport As String = "<div class='onvanedoroos'>زمان تهیه گزارش : " + MiladiToShamsi(Now()) + "</div><table class='report2'><tr class='report2'><th class='report2'>آ<label class='dars'>درس</label> فایل</th><th class='report2'>پاورقی</th><th class='report2'>لینک شده</th><th class='report2'>بدون لینک</th><th class='report2'>بدون شاهد مثال</th></tr>"
                    If row("path").ToString.ToLower.Contains("\" + master.ToLower + "\") Then
                        radif += 1
                        Dim pavaraghi As Integer = 0
                        Dim pavaraghierror As Integer = 0
                        Dim pavaraghilinked As Integer = 0
                        Dim shahedmesal As Integer = 0
                        Dim CourseTopic = row("lesson").ToString
                        Dim MozooDars = row("lesson").ToString
                        If (lastMaster = "") Then
                            lastMaster = master
                            MasterName = row("teacher").ToString
                        Else
                            If (lastMaster <> master) Then
                                MasterName = row("teacher").ToString
                                lastMaster = master
                            End If
                        End If
                        Dim Hide = row("hide").ToString
                        If Hide = "1" Then
                            Continue For
                        End If
                        Dim myear As String = row("year").ToString
                        If YearFilter.Text <> "" Then
                            If YearFilter.Text <> myear Then
                                Continue For
                            End If
                        End If
                        If lessonFilter.Text <> "" Then
                            If lessonFilter.Text <> row("lesson") Then
                                Continue For
                            End If
                        End If
                        If myear = "جاری" Then
                            CourseTopic = CourseTopic + " - سال جاری "
                        Else
                            CourseTopic = CourseTopic + " " + myear
                        End If
                        Dim absolutepath As String = row("path").ToString
                        absolutepath = absolutepath.Replace("\default.htm", "")
                        absolutepath = absolutepath.Replace("\Default.htm", "")
                        If absolutepath.Contains("..\..\") Then
                            '' here is arabic archive
                            ' MsgBox("arabic : " + absolutepath)
                            absolutepath = absolutepath.Replace("..\..\", "\")
                            If (absolutepath.LastIndexOf("_") > 0) Then
                                absolutepath = absolutepath.Substring(0, absolutepath.LastIndexOf("_"))
                                absolutepath = absolutepath + "\"
                            Else
                                absolutepath = absolutepath + "\"
                            End If
                            absolutepath = absolutepath.ToLower.Replace("\ar\feqh\archive\", "\")
                            'MsgBox(absolutepath)
                            '-- counting voice files for this lesson
                            Dim voicecount As Integer = 0
                            For Each vcf In voiceFiles
                                ' MsgBox(vcf.ToLower + vbNewLine + absolutepath.ToLower)
                                If vcf.ToLower.Contains(absolutepath.ToLower) Then
                                    voicecount += 1
                                End If
                            Next
                            '-- end of counting voice files
                            '-- counting text files for this lesson
                            Dim textcount As Integer = 0

                            Dim courseFolder As String = absolutepath.Replace("\", "")
                            'If (radif >= 10) Then
                            '    courseFolder = radif.ToString + "_" + absolutepath.Replace("\", "")
                            'Else
                            '    courseFolder = "0" + radif.ToString + "_" + absolutepath.Replace("\", "")
                            'End If

                            For Each txt In textFiles
                                If MasterFilter.Text <> "" Then
                                    If ProgressBar1.Maximum - ProgressBar1.Value - 1 > 0 Then
                                        ProgressBar1.Value += 1
                                    End If
                                End If
                                'MsgBox(txt.ToLower + vbNewLine + absolutepath.ToLower)
                                If txt.ToLower.Contains(absolutepath.ToLower) Then
                                    textcount += 1
                                    '    MsgBox(absolutepath.ToLower)
                                    '-- counting pavaraghi
                                    If txt.Contains(".htm") Or txt.Contains(".html") Then
                                        WebBrowser2.Navigate(txt)
                                    Else
                                        Continue For
                                    End If
                                    While (WebBrowser2.ReadyState <> WebBrowserReadyState.Complete)
                                        My.Application.DoEvents()
                                    End While
                                    If pavaraghiCheckBox.Checked = True Then
                                        For Each lnk As HtmlElement In WebBrowser2.Document.Links
                                            Dim href = lnk.GetAttribute("href")
                                            'MsgBox(href)
                                            If href.Contains("_ftnref") Then
                                                pavaraghi += 1
                                            End If
                                            If href.Contains(".ir") Then
                                                pavaraghilinked += 1
                                                If href.IndexOf(".ir") > 0 Then
                                                    Dim hrefSub = href.Substring(href.IndexOf(".ir"))
                                                    Dim scount = CountCharacter(hrefSub, "/")
                                                    If scount < 4 Then
                                                        shahedmesal += 1
                                                    End If
                                                End If
                                            End If
                                        Next
                                        'pavaraghi = pavaraghi / 2
                                        totalpavaraghi += pavaraghi
                                        pavaraghierror = pavaraghi - pavaraghilinked
                                        If pavaraghierror < 0 Then
                                            pavaraghierror = 0
                                        End If
                                        totalPavaraghiError += pavaraghierror
                                        totalshahedmesal += shahedmesal
                                        Dim txtr = txt.Substring(txt.ToLower.IndexOf("\text\") + 6)
                                        txtr = "<a href=http://eshia.ir/feqh/archive/text/" + txtr + " >" + txtr.Replace("\default.htm", "") + "</a>"
                                        txtr = txtr.Replace("\", "/")
                                        '                       pavaraghiErrorFileReport += "<tr class='report2'><td class='report2'>" + txtr + "</td><td class='report2'>" + pavaraghi.ToString + "</td><td class='report2'>" + pavaraghilinked.ToString + "</td><td class='report2'>" + pavaraghierror.ToString + "</td><td class='report2'>" + shahedmesal.ToString + "</td></tr>"
                                        spAddDetail(master, courseFolder, txtr, pavaraghi, pavaraghilinked, pavaraghierror, shahedmesal)
                                        pavaraghi = 0
                                        pavaraghierror = 0
                                        pavaraghilinked = 0
                                        shahedmesal = 0
                                    End If
                                    '       Dim wordcount As Integer = 0
                                    'If wordCountCheckBox.Checked = True Then
                                    '    Dim bodytext = WebBrowser2.Document.Body.InnerText
                                    '    For Counter = 1 To Len(bodytext)
                                    '        If Mid(bodytext, Counter, 1) = " " Then
                                    '            wordcount = wordcount + 1
                                    '        End If
                                    '    Next Counter
                                    'End If
                                    '-- end counting pavaraghi
                                End If
                            Next
                            '-- end of counting text files

                            '-- saving link error file 
                            'pavaraghiErrorFileReport += "</table></body></html>"
                            'Dim errorReportFile = ReportPath + "\" + master + "\" + courseFolder + "\default.htm"
                            'If My.Computer.FileSystem.DirectoryExists(ReportPath + "\" + master + "\" + courseFolder + "\") = False Then
                            '    My.Computer.FileSystem.CreateDirectory(ReportPath + "\" + master + "\" + courseFolder + "\")
                            'End If
                            'If Not File.Exists(errorReportFile) Then
                            '    Using sw As New StreamWriter(errorReportFile, True, System.Text.Encoding.UTF8)
                            '        sw.Write(pavaraghiErrorFileReport)
                            '    End Using
                            'Else
                            '    My.Computer.FileSystem.DeleteFile(errorReportFile)
                            '    Using sw As New StreamWriter(errorReportFile, True, System.Text.Encoding.UTF8)
                            '        sw.Write(pavaraghiErrorFileReport)
                            '    End Using
                            'End If
                            '  If My.Computer.FileSystem.FileExists(ReportPath + "\" + master + "\" + courseFolder + "\reporttable.css") = False Then
                            'My.Computer.FileSystem.CopyFile(CurrDir + "\reporttable.css", ReportPath + "\" + master + "\" + courseFolder + "\reporttable.css")
                            ' End If
                            '' end saving error link file
                            'Dim lnkhref As String = "./" + master + "/" + courseFolder + "/default.htm"
                            ' Dim pavaraghiErrorShow = "<a href=" + lnkhref + ">" + totalPavaraghiError.ToString + "</a>"
                            'Dim shahedmesalerrorshow = "<a href=" + lnkhref + ">" + totalshahedmesal.ToString + "</a>"
                            totalpavaraghilinked = totalpavaraghi - totalPavaraghiError
                            If (LastMozoo = "") Then
                                LastMozoo = MozooDars
                            Else
                                If LastMozoo <> MozooDars Then
                                    'detailedReportTableRows += "<tr class='report2'><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + vbTab + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + "مجموع" + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoovoice.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmootext.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghi.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghilinked.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghierror.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmooshahedmesal.ToString + "</td></tr>" + vbNewLine
                                    'detailedReportTableRows += "<tr class='report2' style='height:50px; background:transparent;'><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td></tr>" + vbNewLine
                                    ' partialReport += "<tr class='report2'><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + vbTab + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + "مجموع" + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoovoice.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmootext.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghi.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghilinked.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghierror.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmooshahedmesal.ToString + "</td></tr>" + vbNewLine
                                    ' partialReport += "<tr class='report2' style='height:50px; background:transparent;'><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td></tr>" + vbNewLine
                                    'detailedReportTableRows += "<tr class='report2'><th class='report2'><label class='radif'>ردیف</label></th><th class='report2'><label class='dars'>درس</label></th><th class='report2'><label class='soat'>صوت</label></th><th class='report2'><label class='matn'>متن</label> </th><th class='report2'>پاورقی</th><th class='report2'>پاورقی لینک شده</th><th class='report2'>پاورقی بدون لینک</th><th class='report2'>بدون شاهد مثال</th></tr>"
                                    ' partialReport += "</table>"
                                    LastMozoo = MozooDars
                                    ' ''SAVING partial report file
                                    'Dim partialRPTfile3 = ReportPath + "\" + master + "\tbl" + partialReportNum.ToString + ".txt"
                                    'If My.Computer.FileSystem.DirectoryExists(ReportPath + "\" + master + "\") = False Then
                                    '    My.Computer.FileSystem.CreateDirectory(ReportPath + "\" + master + "\")
                                    'End If
                                    'If Not File.Exists(partialRPTfile3) Then
                                    '    Using sw As New StreamWriter(partialRPTfile3, True, System.Text.Encoding.UTF8)
                                    '        sw.Write(partialReport)
                                    '    End Using
                                    'Else
                                    '    My.Computer.FileSystem.DeleteFile(partialRPTfile3)
                                    '    Using sw As New StreamWriter(partialRPTfile3, True, System.Text.Encoding.UTF8)
                                    '        sw.Write(partialReport)
                                    '    End Using
                                    'End If
                                    ' ''Clearing partial Report
                                    'partialReport = "<table class='report2'><tr class='report2'><th class='report2'><label class='radif'>ردیف</label></th><th class='report2'><label class='dars'>درس</label></th><th class='report2'><label class='soat'>صوت</label></th><th class='report2'><label class='matn'>متن</label> </th><th class='report2'>پاورقی</th><th class='report2'>پاورقی لینک شده</th><th class='report2'>پاورقی بدون لینک</th><th class='report2'>بدون شاهد مثال</th></tr>"
                                    'partialReportNum += 1
                                    partialmajmoopavaraghi = 0
                                    partialmajmoopavaraghierror = 0
                                    partialmajmoopavaraghilinked = 0
                                    partialmajmooshahedmesal = 0
                                    partialmajmootext = 0
                                    partialmajmoovoice = 0
                                End If
                            End If
                            '          detailedReportTableRows += "<tr class='report2'><td class='report2'>" + radif.ToString + "</td><td class='report2'>" + CourseTopic + "</td><td class='report2'>" + voicecount.ToString + "</td><td class='report2'>" + textcount.ToString + "</td><td class='report2'>" + totalpavaraghi.ToString + "</td><td class='report2'>" + totalpavaraghilinked.ToString + "</td><td class='report2'>" + pavaraghiErrorShow + "</td><td class='report2'>" + shahedmesalerrorshow + "</td></tr>" + vbNewLine
                            ' partialReport += "<tr class='report2'><td class='report2'>" + radif.ToString + "</td><td class='report2'>" + CourseTopic + "</td><td class='report2'>" + voicecount.ToString + "</td><td class='report2'>" + textcount.ToString + "</td><td class='report2'>" + totalpavaraghi.ToString + "</td><td class='report2'>" + totalpavaraghilinked.ToString + "</td><td class='report2'>" + pavaraghiErrorShow + "</td><td class='report2'>" + shahedmesalerrorshow + "</td></tr>" + vbNewLine
                            spAddReport(master, courseFolder, CourseTopic, row("lesson"), row("year"), textcount, voicecount, totalpavaraghi, totalpavaraghilinked, totalshahedmesal, DateConvertModule.MiladiToShamsi(Now))
                            majmootext += textcount
                            majmoovoice += voicecount
                            majmoopavaraghi += totalpavaraghi
                            majmoopavaraghierror += totalPavaraghiError
                            majmooshahedmesal += totalshahedmesal
                            majmoopavaraghilinked += totalpavaraghilinked
                            partialmajmootext += textcount
                            partialmajmoovoice += voicecount
                            partialmajmoopavaraghi += totalpavaraghi
                            partialmajmoopavaraghierror += totalPavaraghiError
                            partialmajmooshahedmesal += totalshahedmesal
                            partialmajmoopavaraghilinked += totalpavaraghilinked
                            totalpavaraghi = 0
                            totalPavaraghiError = 0
                            totalshahedmesal = 0
                            totalpavaraghilinked = 0
                            totaltextcount += textcount
                            totalvoicecount += voicecount
                            Dim seri1 = "voice_" + myear.ToString + Guid.NewGuid.ToString
                            Dim seri2 = "text_" + myear.ToString + Guid.NewGuid.ToString
                            cht2.Chart1.Titles.Clear()
                            cht2.Chart1.Titles.Add("نمودار <label class='soat'>صوت</label>-<label class='matn'>متن</label> دروس")
                            cht2.Chart1.Series.Add(seri1)
                            cht2.Chart1.Series(seri1).Label = textcount.ToString
                            cht2.Chart1.Series(seri1).LegendText = myear.ToString
                            Dim i = cht2.Chart1.Series(seri1).Points.AddXY(chart2x, textcount)
                            cht2.Chart1.Series(seri1).Points(i).AxisLabel = myear
                            cht2.Chart1.Series(seri1).ChartType = DataVisualization.Charting.SeriesChartType.Column
                            cht2.Chart1.Series(seri1).BorderWidth = 5
                            chart2x += 1
                            cht2.Chart1.Series.Add(seri2)
                            cht2.Chart1.Series(seri2).Label = voicecount.ToString
                            cht2.Chart1.Series(seri2).LegendText = myear.ToString
                            cht2.Chart1.Series(seri2).Points.AddXY(chart2x, voicecount)
                            cht2.Chart1.Series(seri2).ChartType = DataVisualization.Charting.SeriesChartType.Column
                            cht2.Chart1.Series(seri2).BorderWidth = 5
                            cht2.Chart1.Width += 10
                            chart2x += 2
                        Else
                            '' here is persian archive
                            absolutepath = absolutepath.Replace(".\", "\")
                            absolutepath = absolutepath + "\"
                            '-- counting voice files for this lesson
                            Dim voicecount As Integer = 0
                            For Each vcf In voiceFiles
                                ' MsgBox(vcf.ToLower + vbNewLine + absolutepath.ToLower)
                                If vcf.ToLower.Contains(absolutepath.ToLower) Then
                                    voicecount += 1
                                End If
                            Next
                            '-- end of counting voice files
                            '-- counting text files for this lesson
                            Dim textcount As Integer = 0
                            Dim courseFolder As String = ""
                            If (radif >= 10) Then
                                courseFolder = radif.ToString + "_" + absolutepath.Replace("\", "")
                                '-- saving link error file 
                            Else
                                courseFolder = "0" + radif.ToString + "_" + absolutepath.Replace("\", "")
                            End If
                            For Each txt In textFiles
                                If MasterFilter.Text <> "" Then
                                    If ProgressBar1.Maximum - ProgressBar1.Value - 1 > 0 Then
                                        ProgressBar1.Value += 1
                                    End If
                                End If
                                'MsgBox(txt.ToLower + vbNewLine + absolutepath.ToLower)
                                If txt.ToLower.Contains(absolutepath.ToLower) Then
                                    textcount += 1
                                    '    MsgBox(absolutepath.ToLower)
                                    '-- counting pavaraghi
                                    If txt.Contains(".htm") Or txt.Contains(".html") Then
                                        WebBrowser2.Navigate(txt)
                                    Else
                                        Continue For
                                    End If
                                    While (WebBrowser2.ReadyState <> WebBrowserReadyState.Complete)
                                        My.Application.DoEvents()
                                    End While
                                    If pavaraghiCheckBox.Checked = True Then
                                        For Each lnk As HtmlElement In WebBrowser2.Document.Links
                                            Dim href = lnk.GetAttribute("href")
                                            'MsgBox(href)
                                            If href.Contains("_ftnref") Then
                                                pavaraghi += 1
                                            End If
                                            If href.Contains(".ir") Then
                                                pavaraghilinked += 1
                                                If href.IndexOf(".ir") > 0 Then
                                                    Dim hrefSub = href.Substring(href.IndexOf(".ir"))
                                                    Dim scount = CountCharacter(hrefSub, "/")
                                                    If scount < 4 Then
                                                        shahedmesal += 1
                                                    End If
                                                End If
                                            End If
                                        Next
                                        'pavaraghi = pavaraghi / 2
                                        totalpavaraghi += pavaraghi
                                        pavaraghierror = pavaraghi - pavaraghilinked
                                        If pavaraghierror < 0 Then
                                            pavaraghierror = 0
                                        End If
                                        totalPavaraghiError += pavaraghierror
                                        totalshahedmesal += shahedmesal
                                        Dim txtr = txt.Substring(txt.ToLower.IndexOf("\text\") + 6)
                                        txtr = "<a href=http://eshia.ir/feqh/archive/text/" + txtr + " >" + txtr.Replace("\default.htm", "") + "</a>"
                                        txtr = txtr.Replace("\", "/")
                                        'pavaraghiErrorFileReport += "<tr class='report2'><td class='report2'>" + txtr + "</td><td class='report2'>" + pavaraghi.ToString + "</td><td class='report2'>" + pavaraghilinked.ToString + "</td><td class='report2'>" + pavaraghierror.ToString + "</td><td class='report2'>" + shahedmesal.ToString + "</td></tr>"
                                        spAddDetail(master, courseFolder, txtr, pavaraghi, pavaraghilinked, pavaraghierror, shahedmesal)
                                        pavaraghi = 0
                                        pavaraghierror = 0
                                        pavaraghilinked = 0
                                        shahedmesal = 0
                                    End If
                                    '                 Dim wordcount As Integer = 0
                                    'If wordCountCheckBox.Checked = True Then
                                    '    Dim bodytext = WebBrowser2.Document.Body.InnerText
                                    '    For Counter = 1 To Len(bodytext)
                                    '        If Mid(bodytext, Counter, 1) = " " Then
                                    '            wordcount = wordcount + 1
                                    '        End If
                                    '    Next Counter
                                    'End If
                                    '-- end counting pavaraghi
                                End If
                            Next
                            '-- end of counting text files

                            'pavaraghiErrorFileReport += "</table></body></html>"
                            'Dim errorReportFile = ReportPath + "\" + master + "\" + courseFolder + "\default.htm"
                            'If My.Computer.FileSystem.DirectoryExists(ReportPath + "\" + master + "\" + courseFolder + "\") = False Then
                            '    My.Computer.FileSystem.CreateDirectory(ReportPath + "\" + master + "\" + courseFolder + "\")
                            'End If
                            'If Not File.Exists(errorReportFile) Then
                            '    Using sw As New StreamWriter(errorReportFile, True, System.Text.Encoding.UTF8)
                            '        sw.Write(pavaraghiErrorFileReport)
                            '    End Using
                            'Else
                            '    My.Computer.FileSystem.DeleteFile(errorReportFile)
                            '    Using sw As New StreamWriter(errorReportFile, True, System.Text.Encoding.UTF8)
                            '        sw.Write(pavaraghiErrorFileReport)
                            '    End Using
                            'End If
                            '  If My.Computer.FileSystem.FileExists(ReportPath + "\" + master + "\" + courseFolder + "\reporttable.css") = False Then
                            'My.Computer.FileSystem.CopyFile(CurrDir + "\reporttable.css", ReportPath + "\" + master + "\" + courseFolder + "\reporttable.css")
                            'End If
                            '' end saving error link file
                            '       Dim lnkhref As String = "./" + master + "/" + courseFolder + "/default.htm"
                            '      Dim pavaraghiErrorShow = "<a href=" + lnkhref + ">" + totalPavaraghiError.ToString + "</a>"
                            '     Dim shahedmesalerrorshow = "<a href=" + lnkhref + ">" + totalshahedmesal.ToString + "</a>"
                            totalpavaraghilinked = totalpavaraghi - totalPavaraghiError
                            If (LastMozoo = "") Then
                                LastMozoo = MozooDars
                            Else
                                If LastMozoo <> MozooDars Then
                                    '            detailedReportTableRows += "<tr class='report2'><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + vbTab + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + "مجموع" + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoovoice.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmootext.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghi.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghilinked.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghierror.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmooshahedmesal.ToString + "</td></tr>" + vbNewLine
                                    '           detailedReportTableRows += "<tr class='report2' style='height:50px; background:transparent;'><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td></tr>" + vbNewLine
                                    ' partialReport += "<tr class='report2'><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + vbTab + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + "مجموع" + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoovoice.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmootext.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghi.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghilinked.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghierror.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmooshahedmesal.ToString + "</td></tr>" + vbNewLine
                                    ' partialReport += "<tr class='report2' style='height:50px; background:transparent;'><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td></tr>" + vbNewLine
                                    '          detailedReportTableRows += "<tr class='report2'><th class='report2'><label class='radif'>ردیف</label></th><th class='report2'><label class='dars'>درس</label></th><th class='report2'><label class='soat'>صوت</label></th><th class='report2'><label class='matn'>متن</label> </th><th class='report2'>پاورقی</th><th class='report2'>پاورقی لینک شده</th><th class='report2'>پاورقی بدون لینک</th><th class='report2'>بدون شاهد مثال</th></tr>"
                                    ' partialReport += "</table>"
                                    LastMozoo = MozooDars
                                    ' ''SAVING partial report file
                                    'Dim partialRPTfile2 = ReportPath + "\" + master + "\tbl" + partialReportNum.ToString + ".txt"
                                    'If My.Computer.FileSystem.DirectoryExists(ReportPath + "\" + master + "\") = False Then
                                    '    My.Computer.FileSystem.CreateDirectory(ReportPath + "\" + master + "\")
                                    'End If
                                    'If Not File.Exists(partialRPTfile2) Then
                                    '    Using sw As New StreamWriter(partialRPTfile2, True, System.Text.Encoding.UTF8)
                                    '        sw.Write(partialReport)
                                    '    End Using
                                    'Else
                                    '    My.Computer.FileSystem.DeleteFile(partialRPTfile2)
                                    '    Using sw As New StreamWriter(partialRPTfile2, True, System.Text.Encoding.UTF8)
                                    '        sw.Write(partialReport)
                                    '    End Using
                                    'End If
                                    ' ''Clearing partial Report
                                    'partialReport = "<table class='report2'><tr class='report2'><th class='report2'><label class='radif'>ردیف</label></th><th class='report2'><label class='dars'>درس</label></th><th class='report2'><label class='soat'>صوت</label></th><th class='report2'><label class='matn'>متن</label> </th><th class='report2'>پاورقی</th><th class='report2'>پاورقی لینک شده</th><th class='report2'>پاورقی بدون لینک</th><th class='report2'>بدون شاهد مثال</th></tr>"
                                    'partialReportNum += 1
                                    partialmajmoopavaraghi = 0
                                    partialmajmoopavaraghierror = 0
                                    partialmajmoopavaraghilinked = 0
                                    partialmajmooshahedmesal = 0
                                    partialmajmootext = 0
                                    partialmajmoovoice = 0
                                End If
                            End If
                            ' detailedReportTableRows += "<tr class='report2'><td class='report2'>" + radif.ToString + "</td><td class='report2'>" + CourseTopic + "</td><td class='report2'>" + voicecount.ToString + "</td><td class='report2'>" + textcount.ToString + "</td><td class='report2'>" + totalpavaraghi.ToString + "</td><td class='report2'>" + totalpavaraghilinked.ToString + "</td><td class='report2'>" + pavaraghiErrorShow + "</td><td class='report2'>" + shahedmesalerrorshow + "</td></tr>" + vbNewLine
                            'partialReport += "<tr class='report2'><td class='report2'>" + radif.ToString + "</td><td class='report2'>" + CourseTopic + "</td><td class='report2'>" + voicecount.ToString + "</td><td class='report2'>" + textcount.ToString + "</td><td class='report2'>" + totalpavaraghi.ToString + "</td><td class='report2'>" + totalpavaraghilinked.ToString + "</td><td class='report2'>" + pavaraghiErrorShow + "</td><td class='report2'>" + shahedmesalerrorshow + "</td></tr>" + vbNewLine
                            spAddReport(master, courseFolder, CourseTopic, row("lesson"), row("year"), textcount, voicecount, totalpavaraghi, totalpavaraghilinked, totalshahedmesal, DateConvertModule.MiladiToShamsi(Now))

                            majmootext += textcount
                            majmoovoice += voicecount
                            majmoopavaraghi += totalpavaraghi
                            majmoopavaraghierror += totalPavaraghiError
                            majmooshahedmesal += totalshahedmesal
                            majmoopavaraghilinked += totalpavaraghilinked
                            partialmajmootext += textcount
                            partialmajmoovoice += voicecount
                            partialmajmoopavaraghi += totalpavaraghi
                            partialmajmoopavaraghierror += totalPavaraghiError
                            partialmajmooshahedmesal += totalshahedmesal
                            partialmajmoopavaraghilinked += totalpavaraghilinked
                            totalpavaraghi = 0
                            totalPavaraghiError = 0
                            totalshahedmesal = 0
                            totalpavaraghilinked = 0
                            totaltextcount += textcount
                            totalvoicecount += voicecount
                            Dim seri1 = "voice_" + myear.ToString + Guid.NewGuid.ToString
                            Dim seri2 = "text_" + myear.ToString + Guid.NewGuid.ToString
                            cht2.Chart1.Titles.Clear()
                            cht2.Chart1.Titles.Add("نمودار <label class='soat'>صوت</label>-<label class='matn'>متن</label> دروس")
                            cht2.Chart1.Series.Add(seri1)
                            cht2.Chart1.Series(seri1).Label = textcount.ToString
                            cht2.Chart1.Series(seri1).LegendText = myear.ToString
                            cht2.Chart1.Series(seri1).Points.AddXY(chart2x, textcount)
                            cht2.Chart1.Series(seri1).ChartType = DataVisualization.Charting.SeriesChartType.Column
                            cht2.Chart1.Series(seri1).BorderWidth = 5
                            chart2x += 1
                            cht2.Chart1.Series.Add(seri2)
                            cht2.Chart1.Series(seri2).Label = voicecount.ToString
                            cht2.Chart1.Series(seri2).LegendText = myear.ToString
                            cht2.Chart1.Series(seri2).Points.AddXY(chart2x, voicecount)
                            cht2.Chart1.Series(seri2).ChartType = DataVisualization.Charting.SeriesChartType.Column
                            cht2.Chart1.Series(seri2).BorderWidth = 5
                            cht2.Chart1.Width += 10
                            chart2x += 2
                            Dim i = 0
                        End If
                        'MsgBox(hide)
                    End If
                Next
                '   detailedReportTableRows += "<tr class='report2'><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + vbTab + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + "مجموع" + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoovoice.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmootext.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghi.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghilinked.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghierror.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmooshahedmesal.ToString + "</td></tr>" + vbNewLine
                '  detailedReportTableRows += "<tr class='report2' style='height:50px; background:transparent;'><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td></tr>" + vbNewLine
                ' partialReport += "<tr class='report2'><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + vbTab + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + "مجموع" + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoovoice.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmootext.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghi.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghilinked.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghierror.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmooshahedmesal.ToString + "</td></tr>" + vbNewLine
                ' partialReport += "<tr class='report2' style='height:50px; background:transparent;'><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td></tr>" + vbNewLine
                ' partialReport += "</table>"
                ' ''SAVING partial report file
                'Dim partialRPTfile = ReportPath + "\" + master + "\tbl" + partialReportNum.ToString + ".txt"
                'If My.Computer.FileSystem.DirectoryExists(ReportPath + "\" + master + "\") = False Then
                '    My.Computer.FileSystem.CreateDirectory(ReportPath + "\" + master + "\")
                'End If
                'If Not File.Exists(partialRPTfile) Then
                '    Using sw As New StreamWriter(partialRPTfile, True, System.Text.Encoding.UTF8)
                '        sw.Write(partialReport)
                '    End Using
                'Else
                '    My.Computer.FileSystem.DeleteFile(partialRPTfile)
                '    Using sw As New StreamWriter(partialRPTfile, True, System.Text.Encoding.UTF8)
                '        sw.Write(partialReport)
                '    End Using
                'End If
                ' ''Clearing partial Report
                'partialReport = "<table class='report2'><tr class='report2'><th class='report2'><label class='radif'>ردیف</label></th><th class='report2'><label class='dars'>درس</label></th><th class='report2'><label class='soat'>صوت</label></th><th class='report2'><label class='matn'>متن</label> </th><th class='report2'>پاورقی</th><th class='report2'>پاورقی لینک شده</th><th class='report2'>پاورقی بدون لینک</th><th class='report2'>بدون شاهد مثال</th></tr>"
                'partialReportNum += 1
                partialmajmoopavaraghi = 0
                partialmajmoopavaraghierror = 0
                partialmajmoopavaraghilinked = 0
                partialmajmooshahedmesal = 0
                partialmajmootext = 0
                partialmajmoovoice = 0
                '  detailedReportTableRows += "<tr class='report2'><th class='report2'><label class='radif'>ردیف</label></th><th class='report2'><label class='dars'>درس</label></th><th class='report2'><label class='soat'>صوت</label></th><th class='report2'><label class='matn'>متن</label> </th><th class='report2'>پاورقی</th><th class='report2'>پاورقی لینک شده</th><th class='report2'>پاورقی بدون لینک</th><th class='report2'>بدون شاهد مثال</th></tr>"
                ' detailedReportTableRows += "<tr class='report2'><td class='report2'>" + vbTab + "</td><td class='report2'>" + "مجموع کل دروس" + "</td><td class='report2'>" + majmoovoice.ToString + "</td><td class='report2'>" + majmootext.ToString + "</td><td class='report2'>" + majmoopavaraghi.ToString + "</td><td class='report2'>" + majmoopavaraghilinked.ToString + "</td><td class='report2'>" + majmoopavaraghierror.ToString + "</td><td class='report2'>" + majmooshahedmesal.ToString + "</td></tr>" + vbNewLine
                '-- saving report file for this master
                'Dim HTMLHead As String = "<div class='onvanedoroos'><img class='ostadpic' src='/images/masters/" + master + ".jpg'><br>" + MasterName + "<br><div>زمان گزارش : " + MiladiToShamsi(Now()) + " ساعت " + Now.Hour.ToString + ":" + Now.Minute.ToString + "</div><br>"
                'Dim Report1TableHead = "<table class='report2'><tr class='report2'><th class='report2'><label class='radif'>ردیف</label></th><th class='report2'><label class='dars'>درس</label></th><th class='report2'><label class='soat'>صوت</label></th><th class='report2'><label class='matn'>متن</label> </th><th class='report2'>پاورقی</th><th class='report2'>پاورقی لینک شده</th><th class='report2'>پاورقی بدون لینک</th><th class='report2'>بدون شاهد مثال</th></tr>"
                'Dim htmlOutput As String = ""
                'htmlOutput += HTMLHead
                'htmlOutput += "<div>"
                'htmlOutput += Report1TableHead
                'htmlOutput += detailedReportTableRows
                'htmlOutput += "</table></div>"
                'htmlOutput += "<br><div><center>"
                'htmlOutput += "<br><img width=90% src='./" + master + "/chart1.png'><br><img src='./" + master + "/chart2.png'>&nbsp;&nbsp;&nbsp;<img src='./" + master + "/chart3.png'><br>"
                'htmlOutput += "</center></div>"
                'Dim reportfile = ReportPath + "\" + master + "\default.html"
                'If My.Computer.FileSystem.DirectoryExists(ReportPath + "\" + master + "\") = False Then
                '    My.Computer.FileSystem.CreateDirectory(ReportPath + "\" + master + "\")
                'End If
                'If Not File.Exists(reportfile) Then
                '    Using sw As New StreamWriter(reportfile, True, System.Text.Encoding.UTF8)
                '        sw.Write(htmlOutput)
                '    End Using
                'Else
                '    My.Computer.FileSystem.DeleteFile(reportfile)
                '    Using sw As New StreamWriter(reportfile, True, System.Text.Encoding.UTF8)
                '        sw.Write(htmlOutput)
                '    End Using
                'End If
                'If My.Computer.FileSystem.FileExists(ReportPath + "\" + master + "\" + "reporttable.css") = False Then
                'My.Computer.FileSystem.CopyFile(CurDir() + "\reporttable.css", ReportPath + "\" + master + "\" + "reporttable.css")
                'End If
                '-- creating chart image
                cht2.Chart1.SaveImage(ReportPath + "\asatid\" + master + "\chart1.png", System.Drawing.Imaging.ImageFormat.Png)
                Dim cht As New Chart
                cht.Chart1.Titles.Add("نمودار مجموع <label class='soat'>صوت</label>-<label class='matn'>متن</label> ")
                cht.Chart1.Series.Add("totalvoice")
                cht.Chart1.Series("totalvoice").Label = totalvoicecount.ToString
                cht.Chart1.Series("totalvoice").LegendText = "تعداد <label class='soat'>صوت</label>"
                cht.Chart1.Series("totalvoice").Points.Add(totalvoicecount)
                cht.Chart1.Series("totalvoice").ChartType = DataVisualization.Charting.SeriesChartType.Column
                cht.Chart1.Series.Add("totaltext")
                cht.Chart1.Series("totaltext").Label = totaltextcount.ToString
                cht.Chart1.Series("totaltext").LegendText = "تعداد <label class='matn'>متن</label>"
                cht.Chart1.Series("totaltext").Points.Add(totaltextcount)
                cht.Chart1.Series("totaltext").ChartType = DataVisualization.Charting.SeriesChartType.Column
                cht.Chart1.SaveImage(ReportPath + "\asatid\" + master + "\chart2.png", System.Drawing.Imaging.ImageFormat.Png)
                cht.Chart1.Series.Clear()
                cht.Chart1.Titles.Clear()
                cht.Chart1.Titles.Add("نمودار مجموع پاورقی و لینک ها ")
                cht.Chart1.Series.Add("totalpavaraghi")
                cht.Chart1.Series("totalpavaraghi").Label = majmoopavaraghi.ToString
                cht.Chart1.Series("totalpavaraghi").LegendText = " پاورقی"
                cht.Chart1.Series("totalpavaraghi").Points.Add(majmoopavaraghi)
                cht.Chart1.Series("totalpavaraghi").ChartType = DataVisualization.Charting.SeriesChartType.Column
                cht.Chart1.Series.Add("totalpavaraghilinked")
                cht.Chart1.Series("totalpavaraghilinked").Label = majmoopavaraghilinked.ToString
                cht.Chart1.Series("totalpavaraghilinked").LegendText = " پاورقی لینک شده"
                cht.Chart1.Series("totalpavaraghilinked").Points.Add(majmoopavaraghilinked)
                cht.Chart1.Series("totalpavaraghilinked").ChartType = DataVisualization.Charting.SeriesChartType.Column
                cht.Chart1.Series.Add("totalpavaraghierror")
                cht.Chart1.Series("totalpavaraghierror").Label = majmoopavaraghierror.ToString
                cht.Chart1.Series("totalpavaraghierror").LegendText = " پاورقی لینک نشده"
                cht.Chart1.Series("totalpavaraghierror").Points.Add(majmoopavaraghierror)
                cht.Chart1.Series("totalpavaraghierror").ChartType = DataVisualization.Charting.SeriesChartType.Column
                cht.Chart1.Series.Add("totalshahedmesal")
                cht.Chart1.Series("totalshahedmesal").Label = majmooshahedmesal
                cht.Chart1.Series("totalshahedmesal").LegendText = " بدون شاهد مثال"
                cht.Chart1.Series("totalshahedmesal").Points.Add(majmooshahedmesal)
                cht.Chart1.Series("totalshahedmesal").ChartType = DataVisualization.Charting.SeriesChartType.Column
                cht.Chart1.SaveImage(ReportPath + "\asatid\" + master + "\chart3.png", System.Drawing.Imaging.ImageFormat.Png)
                '-- end of saving the report file for the master
                ' MsgBox(htmlOutput)
                If ProgressBar1.Maximum - ProgressBar1.Value - 1 > 0 And MasterFilter.Text = "" Then
                    ProgressBar1.Value += 1
                End If
            Catch ex As Exception
                errortxtbox.AppendText(ex.Message + vbNewLine + ex.StackTrace + vbNewLine + ex.Source.ToString + vbNewLine + ex.Data.ToString)
                Continue For
            End Try
        Next
        ProgressBar1.Value = 0
        StatusLabel.Text = "آماده"
        StatusLabel.ForeColor = Color.Green
        NewReport.Enabled = True
        BrowseButton.Enabled = True
        MasterFilter.Enabled = True
        YearFilter.Enabled = True
        lessonFilter.Enabled = True
        cleardbBtn.Enabled = True
        Loading.Close()
    End Sub

    Private Sub FiletypeErrorShow_Click(sender As Object, e As EventArgs) Handles FiletypeErrorShow.Click
        TextFileTypeErrors.Show()
    End Sub


    Private Sub runSQL_Click(sender As Object, e As EventArgs) Handles runSQL.Click
        Try
            If sqlCMD.Text <> "" And sqlCMD.Text.ToLower.Contains("delete") Or sqlCMD.Text.ToLower.Contains("update") Or sqlCMD.Text.ToLower.Contains("drop") Then
                MsgBox("امکان حذف یا تغییر داده وجود ندارد", MsgBoxStyle.Critical, "توجه !")
                Exit Sub
            End If
            Dim Connection As New SqlConnection(Me.cString)
            Connection.Open()
            Dim Command As New SqlCommand(sqlCMD.Text, Connection)
            Command.CommandType = CommandType.Text
            Dim DataAdapter As New SqlDataAdapter
            Dim DTable As New DataTable
            DataAdapter.SelectCommand = Command
            DTable.Clear()
            DataAdapter.Fill(DTable)
            DataGridView1.DataSource = DTable
            DataGridView1.Update()
            Connection.Close()
        Catch ex As Exception
            errortxtbox.AppendText(ex.Message + ex.StackTrace + ex.Source)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim masterReportTable As New DataTable
            StatusLabel.Text = "ایجاد فایل گزارش"
            StatusLabel.ForeColor = Color.Red
            ProgressBar1.Value = 0
            ProgressBar1.Maximum = OstadList.Count
            NewReport.Enabled = False
            For Each master In OstadList
                masterReportTable = spGetReport(master)
                Dim masterName As String = ""
                For Each row As DataRow In Monitoring.Tables("list").Rows
                    If row("path").ToString.ToLower.Contains(master.ToLower) Then
                        masterName = row("teacher")
                        Exit For
                    End If
                Next
                generateReport(masterReportTable, master, masterName)
                ProgressBar1.Value += 1
            Next
            '' total report
            Dim TotalMasters As Integer = 0
            Dim TotalVoice As Integer = 0
            Dim TotalText As Integer = 0
            Dim TotalCourse As Integer = 0
            Dim Connection As New SqlConnection(Me.cString)
            Connection.Open()
            Dim Command As New SqlCommand("SELECT DISTINCT dbReport.DBO.Course.[Master] FROM dbReport.DBO.Course", Connection)
            Command.CommandType = CommandType.Text
            Dim DataAdapter As New SqlDataAdapter
            Dim DTable As New DataTable
            DataAdapter.SelectCommand = Command
            DTable.Clear()
            DataAdapter.Fill(DTable)
            TotalMasters = DTable.Rows.Count
            Command = New SqlCommand("SELECT dbReport.DBO.Course.[CourseTitle] FROM dbReport.DBO.Course", Connection)
            Command.CommandType = CommandType.Text
            DataAdapter = New SqlDataAdapter
            DTable = New DataTable
            DataAdapter.SelectCommand = Command
            DTable.Clear()
            DataAdapter.Fill(DTable)
            TotalCourse = DTable.Rows.Count
            Command = New SqlCommand("SELECT SUM(VoiceCount) AS TOTALVC , SUM(TextCount) AS TOTALTX FROM dbReport.dbo.Course", Connection)
            Command.CommandType = CommandType.Text
            DataAdapter = New SqlDataAdapter
            DTable = New DataTable
            DataAdapter.SelectCommand = Command
            DTable.Clear()
            DataAdapter.Fill(DTable)
            TotalVoice = DTable.Rows(0)(0)
            TotalText = DTable.Rows(0)(1)
            Command = New SqlCommand("SELECT CourseTopic,COUNT(1) FROM dbReport.dbo.Course GROUP BY CourseTopic", Connection)
            Command.CommandType = CommandType.Text
            DataAdapter = New SqlDataAdapter
            DTable = New DataTable
            DataAdapter.SelectCommand = Command
            DTable.Clear()
            DataAdapter.Fill(DTable)
            Dim radif As Integer = 0
            Dim detailtopiccount As String = ""
            For Each row As DataRow In DTable.Rows
                radif += 1
                detailtopiccount += "<tr><td>" + radif.ToString + "</td><td>" + row(0).ToString + "</td><td>" + row(1).ToString + "</td></tr>"
            Next
            Dim distincttopiccount As String = ""
            Command = New SqlCommand("SELECT CourseTopic FROM dbReport.dbo.Course GROUP BY CourseTopic", Connection)
            Command.CommandType = CommandType.Text
            DataAdapter = New SqlDataAdapter
            DTable = New DataTable
            DataAdapter.SelectCommand = Command
            DTable.Clear()
            DataAdapter.Fill(DTable)
            Dim radif2 As Integer = 0
            For Each row As DataRow In DTable.Rows
                radif2 += 1
                Command = New SqlCommand("SELECT Distinct [master] FROM dbReport.dbo.Course where CourseTopic like N'%" + row(0) + "%'", Connection)
                Command.CommandType = CommandType.Text
                DataAdapter = New SqlDataAdapter
                Dim DTable2 = New DataTable
                DataAdapter.SelectCommand = Command
                DTable2.Clear()
                DataAdapter.Fill(DTable2)
                distincttopiccount += "<tr><td>" + radif2.ToString + "</td><td>" + row(0) + "</td><td>" + DTable2.Rows.Count.ToString + "</td></tr>"
            Next

            Dim currentReport As String = "<table class='report2'>"
            currentReport += "<tr class='report2'><th class='report2'><label class='radif'>ردیف</label></th><th class='report2'><label class='master'>Master</label></th><th class='report2'><label class='ostad'>استاد</label></th><th class='report2'><label class='dars'>درس</label></th><th class='report2'><label class='soat'>صوت</label></th><th class='report2'><label class='matn'>متن</label> </th><th class='report2'><label class='pavaraghi'>پاورقی</label></th><th class='report2'><label class='pavaraghilinked'>پاورقی لینک شده</label></th><th class='report2'><label class='pavaraghinolink'>پاورقی بدون لینک</label></th><th class='report2'><label class='noshahedmesal'>بدون شاهد مثال</label></th></tr>"
            Dim sqlcmd As String = "SELECT * FROM dbReport.dbo.Course WHERE Year like N'" + PersianCurrentYear + "' order by Master"
            Command = New SqlCommand(sqlcmd, Connection)
            Command.CommandType = CommandType.Text
            DataAdapter = New SqlDataAdapter
            DTable = New DataTable
            DataAdapter.SelectCommand = Command
            DTable.Clear()
            DataAdapter.Fill(DTable)
            Dim radif3 As Integer = 0
            For Each row As DataRow In DTable.Rows
                radif3 += 1
                Dim bdoonelink As Integer = Val(row(8)) - Val(row(9))
                currentReport += "<tr class='report2'><td class='report2'>" + radif3.ToString + "</td><td class='report2'>" + row(1) + "</td><td class='report2'>" + masterName(row(1)) + "</td><td class='report2'>" + row(4) + "</td><td class='report2'>" + row(7).ToString + "</td><td class='report2'>" + row(6).ToString + "</td><td class='report2'>" + row(8).ToString + "</td><td class='report2'>" + row(9).ToString + "</td><td class='report2'>" + bdoonelink.ToString + "</td><td class='report2'>" + row(10).ToString + "</td></tr>"
            Next
            '----- GENERET CURRENT ARABIC
            sqlcmd = "SELECT * FROM dbReport.dbo.Course WHERE Year like N'" + ArabicCurrentYear + "' order by Master"
            Command = New SqlCommand(sqlcmd, Connection)
            DataAdapter = New SqlDataAdapter
            DTable = New DataTable
            DataAdapter.SelectCommand = Command
            DTable.Clear()
            DataAdapter.Fill(DTable)
            For Each row As DataRow In DTable.Rows
                radif3 += 1
                Dim bdoonelink As Integer = Val(row(8)) - Val(row(9))
                currentReport += "<tr class='report2'><td class='report2'>" + radif3.ToString + "</td><td class='report2'>" + row(1) + "</td><td class='report2'>" + masterName(row(1)) + "</td><td class='report2'>" + row(4) + "</td><td class='report2'>" + row(7).ToString + "</td><td class='report2'>" + row(6).ToString + "</td><td class='report2'>" + row(8).ToString + "</td><td class='report2'>" + row(9).ToString + "</td><td class='report2'>" + bdoonelink.ToString + "</td><td class='report2'>" + row(10).ToString + "</td></tr>"
            Next
            currentReport += "</table><script src='http://eshia.ir/AR/feqh/report/asatid/translate.js'></script></html>"

            Connection.Close()
            '-- saving total report file for this master
            Dim HTMLHead As String = "<html><div class='onvanedoroos'><div><label class='zaman'>زمان گزارش : </label>" + MiladiToShamsi(Now()) + "<label class='saat'> ساعت </label>" + ghour() + ":" + gmin() + "</div><br>"
            Dim htmlOutput As String = ""
            Dim Report1TableHead = "<table class='report2'>"
            htmlOutput += HTMLHead
            htmlOutput += "<div>"
            htmlOutput += Report1TableHead
            htmlOutput += "<tr class='report2'><th class='report2'>تعداد کل اساتید</th><th class='report2'>تعداد کل <label class='dars'>درس</label> ها</th><th class='report2'>مجموع کل <label class='soat'>صوت</label> ها</th><th class='report2'>مجموع کل <label class='matn'>متن</label> ها</th></tr>"
            htmlOutput += "<tr class='report2'><td class='report2'>" + TotalMasters.ToString + "</td><td class='report2'>" + TotalCourse.ToString + "</td><td class='report2'>" + TotalVoice.ToString + "</td><td class='report2'>" + TotalText.ToString + "</td></tr>"
            htmlOutput += "<br><br><table class='report2'><tr class='report2'><th class='report2'><label class='radif'>ردیف</label></th><th class='report2'>موضوع</th><th class='report2'><label class='tedadostad'>تعداد استاد</label></th></tr>"
            htmlOutput += distincttopiccount
            htmlOutput += "</table><br><br><table class='report2'><tr><th><label class='radif'>ردیف</label></th><th><label class='ostaddars'>استاد-درس</label></th><th><label class='sal'>سال</label></th></tr>"
            htmlOutput += detailtopiccount
            htmlOutput += "</table></div></div><script src='http://eshia.ir/AR/feqh/report/asatid/translate.js'></script>"
            Dim file = ReportPath + "\asatid\total\default.htm"
            If My.Computer.FileSystem.DirectoryExists(ReportPath + "\asatid\total\") = False Then
                My.Computer.FileSystem.CreateDirectory(ReportPath + "\asatid\total\")
            End If
            If Not My.Computer.FileSystem.FileExists(file) Then
                Using sw As New StreamWriter(file, True, System.Text.Encoding.UTF8)
                    sw.Write(htmlOutput)
                End Using
            Else
                My.Computer.FileSystem.DeleteFile(file)
                Using sw As New StreamWriter(file, True, System.Text.Encoding.UTF8)
                    sw.Write(htmlOutput)
                End Using
            End If
            '' total report
            '' saving current report
            Dim HTMLHead2 As String = "<html><div class='onvanedoroos'><div>گزارش دروس سال جاری<br><label class='zaman'>زمان گزارش : </label>" + MiladiToShamsi(Now()) + "<label class='saat'> ساعت </label>" + ghour() + ":" + gmin() + "</div><br>"

            Dim currentoutput As String = ""
            currentoutput += HTMLHead2 + "<div>"
            currentoutput += currentReport + "</div>"

            file = ReportPath + "\asatid\current\default.htm"
            If My.Computer.FileSystem.DirectoryExists(ReportPath + "\asatid\current\") = False Then
                My.Computer.FileSystem.CreateDirectory(ReportPath + "\asatid\current\")
            End If
            If Not My.Computer.FileSystem.FileExists(file) Then
                Using sw As New StreamWriter(file, True, System.Text.Encoding.UTF8)
                    sw.Write(currentoutput)
                End Using
            Else
                My.Computer.FileSystem.DeleteFile(file)
                Using sw As New StreamWriter(file, True, System.Text.Encoding.UTF8)
                    sw.Write(currentoutput)
                End Using
            End If
            '' saving current report
            StatusLabel.Text = "آماده"
            StatusLabel.ForeColor = Color.Green
            ProgressBar1.Value = 0
            NewReport.Enabled = True
        Catch ex As Exception
            errortxtbox.AppendText(ex.Message + ex.StackTrace + ex.Source)
        End Try
    End Sub

    Private Function masterName(ByVal master As String)
        For Each row As DataRow In Monitoring.Tables("list").Rows
            If row("path").ToString.Contains(".\") Then
                Dim mastr As String = row("path").ToString.Replace(".\", "")
                mastr = mastr.Substring(0, mastr.IndexOf("\"))
                If master = mastr Then
                    Return row("teacher")
                End If
            End If
            If row("path").ToString.Contains("..\..\") Then
                Dim mastr As String = row("path").ToString.ToLower.Replace("..\..\", "")
                mastr = mastr.Replace("ar\feqh\archive\", "")
                mastr = mastr.Substring(0, mastr.IndexOf("\"))
                If (master = mastr) Then
                    Return row("teacher")
                End If
            End If
        Next
        Return ""
    End Function

    Private Sub generateReport(ByRef masterReportTable As DataTable, ByVal master As String, ByVal mastername As String)
        Try
            Dim lastMozoo As String = ""
            Dim ReportFile As String = "<tr class='report2'><th class='report2'><label class='radif'>ردیف</label></th><th class='report2'><label class='dars'>درس</label></th><th class='report2'><label class='soat'>صوت</label></th><th class='report2'><label class='matn'>متن</label> </th><th class='report2'><label class='pavaraghi'>پاورقی</label></th><th class='report2'><label class='pavaraghilinked'>پاورقی لینک شده</label></th><th class='report2'><label class='pavaraghinolink'>پاورقی بدون لینک</label></th><th class='report2'><label class='noshahedmesal'>بدون شاهد مثال</label></th></tr>"
            Dim partialmajmoovoice As Integer = 0
            Dim partialmajmootext As Integer = 0
            Dim partialmajmoopavaraghi As Integer = 0
            Dim partialmajmoopavaraghilinked As Integer = 0
            Dim patialmajmoopavaraghierror As Integer = 0
            Dim partialmajmooshahedmesal As Integer = 0
            Dim majmoovoice As Integer = 0
            Dim majmootext As Integer = 0
            Dim majmoopavaraghi As Integer = 0
            Dim majmoopavaraghilinked As Integer = 0
            Dim majmoopavaraghierror As Integer = 0
            Dim majmooshahedmesal As Integer = 0
            Dim radif As Integer = 0
            For Each row As DataRow In masterReportTable.Rows
                radif += 1
                If lastMozoo = "" Then
                    lastMozoo = row(4)
                    Dim pavaraghError As Integer = (Val(row(8)) - Val(row(9)))
                    Dim lnkhref As String = "./" + master + "/" + row(2).ToString + "/default.htm"
                    Dim pavaraghiErrorShow = "<a href=" + lnkhref + ">" + pavaraghError.ToString + "</a>"
                    generateErrorReportFile(master, row(2).ToString)
                    ReportFile += "<tr class='report2'><td class='report2'>" + radif.ToString + "</td><td class='report2'>" + row(3).ToString + "</td><td class='report2'>" + row(7).ToString + "</td><td class='report2'>" + row(6).ToString + "</td><td class='report2'>" + row(8).ToString + "</td><td class='report2'>" + row(9).ToString + "</td><td class='report2'>" + pavaraghiErrorShow + "</td><td class='report2'>" + row(10).ToString + "</td></tr>" + vbNewLine
                    majmoovoice += Val(row(7))
                    majmootext += Val(row(6))
                    majmoopavaraghi += Val(row(8))
                    majmoopavaraghilinked += Val(row(9))
                    majmoopavaraghierror += (Val(row(8)) - Val(row(9)))
                    majmooshahedmesal += Val(row(10))
                    partialmajmoovoice += Val(row(7))
                    partialmajmootext += Val(row(6))
                    partialmajmoopavaraghi += Val(row(8))
                    partialmajmoopavaraghilinked += Val(row(9))
                    patialmajmoopavaraghierror += (Val(row(8)) - Val(row(9)))
                    partialmajmooshahedmesal += Val(row(10))
                Else
                    If lastMozoo <> row(4) Then
                        lastMozoo = row(4)
                        ReportFile += "<tr class='report2'><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + vbTab + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + "<label class='majmoo'>مجموع</label>" + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoovoice.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmootext.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghi.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghilinked.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + patialmajmoopavaraghierror.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmooshahedmesal.ToString + "</td></tr>" + vbNewLine
                        ReportFile += "<tr class='report2' style='height:50px; background:transparent;'><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td></tr>" + vbNewLine
                        partialmajmoovoice = 0
                        partialmajmootext = 0
                        partialmajmoopavaraghi = 0
                        partialmajmoopavaraghilinked = 0
                        patialmajmoopavaraghierror = 0
                        partialmajmooshahedmesal = 0
                        ReportFile += "<tr class='report2'><th class='report2'><label class='radif'>ردیف</label></th><th class='report2'><label class='dars'>درس</label></th><th class='report2'><label class='soat'>صوت</label></th><th class='report2'><label class='matn'>متن</label> </th><th class='report2'><label class='pavaraghi'>پاورقی</label></th><th class='report2'>پاورقی لینک شده</th><th class='report2'>پاورقی بدون لینک</th><th class='report2'>بدون شاهد مثال</th></tr>"
                        Dim pavaraghError As Integer = (Val(row(8)) - Val(row(9)))
                        Dim lnkhref As String = "./" + master + "/" + row(2).ToString + "/default.htm"
                        Dim pavaraghiErrorShow = "<a href=" + lnkhref + ">" + pavaraghError.ToString + "</a>"
                        generateErrorReportFile(master, row(2).ToString)
                        ReportFile += "<tr class='report2'><td class='report2'>" + radif.ToString + "</td><td class='report2'>" + row(3).ToString + "</td><td class='report2'>" + row(7).ToString + "</td><td class='report2'>" + row(6).ToString + "</td><td class='report2'>" + row(8).ToString + "</td><td class='report2'>" + row(9).ToString + "</td><td class='report2'>" + pavaraghiErrorShow + "</td><td class='report2'>" + row(10).ToString + "</td></tr>" + vbNewLine
                        majmoovoice += Val(row(7))
                        majmootext += Val(row(6))
                        majmoopavaraghi += Val(row(8))
                        majmoopavaraghilinked += Val(row(9))
                        majmoopavaraghierror += (Val(row(8)) - Val(row(9)))
                        majmooshahedmesal += Val(row(10))
                        partialmajmoovoice += Val(row(7))
                        partialmajmootext += Val(row(6))
                        partialmajmoopavaraghi += Val(row(8))
                        partialmajmoopavaraghilinked += Val(row(9))
                        patialmajmoopavaraghierror += (Val(row(8)) - Val(row(9)))
                        partialmajmooshahedmesal += Val(row(10))

                    Else
                        Dim pavaraghError As Integer = (Val(row(8)) - Val(row(9)))
                        Dim lnkhref As String = "./" + master + "/" + row(2).ToString + "/default.htm"
                        Dim pavaraghiErrorShow = "<a href=" + lnkhref + ">" + pavaraghError.ToString + "</a>"
                        generateErrorReportFile(master, row(2).ToString)
                        ReportFile += "<tr class='report2'><td class='report2'>" + radif.ToString + "</td><td class='report2'>" + row(3).ToString + "</td><td class='report2'>" + row(7).ToString + "</td><td class='report2'>" + row(6).ToString + "</td><td class='report2'>" + row(8).ToString + "</td><td class='report2'>" + row(9).ToString + "</td><td class='report2'>" + pavaraghiErrorShow + "</td><td class='report2'>" + row(10).ToString + "</td></tr>" + vbNewLine
                        majmoovoice += Val(row(7))
                        majmootext += Val(row(6))
                        majmoopavaraghi += Val(row(8))
                        majmoopavaraghilinked += Val(row(9))
                        majmoopavaraghierror += (Val(row(8)) - Val(row(9)))
                        majmooshahedmesal += Val(row(10))
                        partialmajmoovoice += Val(row(7))
                        partialmajmootext += Val(row(6))
                        partialmajmoopavaraghi += Val(row(8))
                        partialmajmoopavaraghilinked += Val(row(9))
                        patialmajmoopavaraghierror += (Val(row(8)) - Val(row(9)))
                        partialmajmooshahedmesal += Val(row(10))

                    End If
                End If
            Next
            ReportFile += "<tr class='report2'><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + vbTab + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + "<label class='majmoo'>مجموع</label>" + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoovoice.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmootext.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghi.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmoopavaraghilinked.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + patialmajmoopavaraghierror.ToString + "</td><td class='report2' style='background: rgba(190, 240, 144, 0.61) none repeat scroll 0% 0%;'>" + partialmajmooshahedmesal.ToString + "</td></tr>" + vbNewLine
            ReportFile += "<tr class='report2' style='height:50px; background:transparent;'><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td><td class='report2' style='border: medium none; background:transparent;'>" + vbTab + "</td></tr>" + vbNewLine

            ReportFile += "<tr class='report2'><th class='report2'>" + vbTab + "</th><th class='report2'>" + vbTab + "</th><th class='report2'><label class='soat'>صوت</label></th><th class='report2'><label class='matn'>متن</label> </th><th class='report2'><label class='pavaraghi'>پاورقی</label></th><th class='report2'><label class='pavaraghilinked'>پاورقی لینک شده</label></th><th class='report2'><label class='pavaraghinolink'>پاورقی بدون لینک</label></th><th class='report2'><label class='noshahedmesal'>بدون شاهد مثال</label></th></tr>"
            ReportFile += "<tr class='report2'><td class='report2'>" + vbTab + "</td><td class='report2'>" + "<label class='majmookoledoroos'>مجموع کل دروس</label>" + "</td><td class='report2'>" + majmoovoice.ToString + "</td><td class='report2'>" + majmootext.ToString + "</td><td class='report2'>" + majmoopavaraghi.ToString + "</td><td class='report2'>" + majmoopavaraghilinked.ToString + "</td><td class='report2'>" + majmoopavaraghierror.ToString + "</td><td class='report2'>" + majmooshahedmesal.ToString + "</td></tr>" + vbNewLine
            '-- saving report file for this master
            Dim HTMLHead As String = "<html><div class='onvanedoroos'><img class='ostadpic' src='/images/masters/" + master + ".jpg'><br>" + mastername + "<br><div><label class='zaman'>زمان گزارش : </label>" + MiladiToShamsi(Now()) + "<label class='saat'> ساعت </label>" + ghour() + ":" + gmin() + "</div><br>"
            Dim htmlOutput As String = ""
            Dim Report1TableHead = "<table class='report2'>"
            htmlOutput += HTMLHead
            htmlOutput += "<div>"
            htmlOutput += Report1TableHead
            htmlOutput += ReportFile
            htmlOutput += "</table></div>"
            htmlOutput += "<br><div><center>"
            htmlOutput += "<br><img width=90% src='./" + master + "/chart1.png'><br><img src='./" + master + "/chart2.png'>&nbsp;&nbsp;&nbsp;<img src='./" + master + "/chart3.png'><br>"
            htmlOutput += "</center></div><script src='http://eshia.ir/feqh/report/asatid/translate.js'></script></html>"
            Dim file = ReportPath + "\asatid" + "\" + master + "\default.html"
            If My.Computer.FileSystem.DirectoryExists(ReportPath + "\asatid" + "\" + master + "\") = False Then
                My.Computer.FileSystem.CreateDirectory(ReportPath + "\asatid" + "\" + master + "\")
            End If
            If Not My.Computer.FileSystem.FileExists(file) Then
                Using sw As New StreamWriter(file, True, System.Text.Encoding.UTF8)
                    sw.Write(htmlOutput)
                End Using
            Else
                My.Computer.FileSystem.DeleteFile(file)
                Using sw As New StreamWriter(file, True, System.Text.Encoding.UTF8)
                    sw.Write(htmlOutput)
                End Using
            End If
        Catch ex As Exception
            errortxtbox.AppendText(ex.Message + ex.StackTrace + ex.Source)
        End Try
    End Sub

    Private Sub generateErrorReportFile(ByVal master As String, ByVal path As String)
        Dim pavaraghiErrorFileReport As String = "<html><div class='onvanedoroos'>زمان تهیه گزارش : " + MiladiToShamsi(Now()) + "</div><table class='report2'><tr class='report2'><th class='report2'><label class='filedars'>فایل درس</label></th><th class='report2'><label class='pavaraghi'>پاورقی</label></th><th class='report2'><label class='linked'>لینک شده</label></th><th class='report2'><label class='nolink'>بدون لینک</label></th><th class='report2'><label class='noshahedmesal'>بدون شاهد مثال</label></th></tr>"
        Dim table As New DataTable
        table = spGetDetailReport(Master)
        For Each row As DataRow In table.Rows
            If row(2) = path Then
                pavaraghiErrorFileReport += "<tr class='report2'><td class='report2'>" + row(3).ToString + "</td><td class='report2'>" + row(4).ToString + "</td><td class='report2'>" + row(5).ToString + "</td><td class='report2'>" + row(6).ToString + "</td><td class='report2'>" + row(7).ToString + "</td></tr>"
            End If
        Next
        pavaraghiErrorFileReport += "</table></body><script http://eshia.ir/feqh/report/asatid/translate.js'></script></html>"
        Dim errorReportFile = ReportPath + "\asatid\" + master + "\" + path + "\default.htm"
        If My.Computer.FileSystem.DirectoryExists(ReportPath + "\asatid\" + master + "\" + path + "\") = False Then
            My.Computer.FileSystem.CreateDirectory(ReportPath + "\asatid\" + master + "\" + path + "\")
        End If
        If Not File.Exists(errorReportFile) Then
            Using sw As New StreamWriter(errorReportFile, True, System.Text.Encoding.UTF8)
                sw.Write(pavaraghiErrorFileReport)
            End Using
        Else
            My.Computer.FileSystem.DeleteFile(errorReportFile)
            Using sw As New StreamWriter(errorReportFile, True, System.Text.Encoding.UTF8)
                sw.Write(pavaraghiErrorFileReport)
            End Using
        End If
        ' end saving error link file
    End Sub

    Private Sub sqlCMD_TextChanged(sender As Object, e As EventArgs) Handles sqlCMD.TextChanged

    End Sub

    Private Sub cleardbBtn_Click(sender As Object, e As EventArgs) Handles cleardbBtn.Click
        ClearDb()
    End Sub
    Public Sub ClearDb()
        Try
            If MasterFilter.Text = "" Then
                Dim sqlCMD = "DELETE FROM dbReport.dbo.Course"
                Dim Connection As New SqlConnection(Me.cString)
                Connection.Open()
                Dim Command As New SqlCommand(sqlCMD, Connection)
                Command.CommandType = CommandType.Text
                Command.ExecuteNonQuery()
                sqlCMD = "DELETE FROM dbReport.dbo.Detail"
                Command = New SqlCommand(sqlCMD, Connection)
                Command.CommandType = CommandType.Text
                Command.ExecuteNonQuery()
                Connection.Close()
            Else
                If YearFilter.Text = "" Then
                    Dim sqlCMD = "DELETE FROM dbReport.dbo.Course WHERE [Master]='" + MasterFilter.Text + "'"
                    Dim Connection As New SqlConnection(Me.cString)
                    Connection.Open()
                    Dim Command As New SqlCommand(sqlCMD, Connection)
                    Command.CommandType = CommandType.Text
                    Command.ExecuteNonQuery()
                    sqlCMD = "DELETE FROM dbReport.dbo.Detail WHERE [Master]='" + MasterFilter.Text + "'"""
                    Command = New SqlCommand(sqlCMD, Connection)
                    Command.CommandType = CommandType.Text
                    Command.ExecuteNonQuery()
                    Connection.Close()
                Else
                    Dim sqlCMD = "DELETE FROM dbReport.dbo.Course WHERE [Master]='" + MasterFilter.Text + "' AND Year='" + YearFilter.Text + "'"
                    Dim Connection As New SqlConnection(Me.cString)
                    Connection.Open()
                    Dim Command As New SqlCommand(sqlCMD, Connection)
                    Command.CommandType = CommandType.Text
                    Command.ExecuteNonQuery()
                    sqlCMD = "DELETE FROM dbReport.dbo.Detail WHERE [Master]='" + MasterFilter.Text + "' AND Year='" + YearFilter.Text + "'"""
                    Command = New SqlCommand(sqlCMD, Connection)
                    Command.CommandType = CommandType.Text
                    Command.ExecuteNonQuery()
                    Connection.Close()
                End If
            End If

        Catch ex As Exception
            errortxtbox.AppendText(ex.Message + ex.StackTrace + ex.Source)
        End Try
    End Sub
    Private Function ghour()
        Dim h = Now.Hour
        Dim r = h.ToString
        If h < 10 Then
            r = "0" + h.ToString
        End If
        Return r
    End Function
    Private Function gmin()
        Dim m = Now.Minute
        Dim r = m.ToString
        If m < 10 Then
            r = "0" + m.ToString
        End If
        Return r
    End Function
End Class