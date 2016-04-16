Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Text

Public Class ReportMainForm
    Dim Monitoring As New DataSet
    Dim textcount As Long
    Dim voiceCount As Long
    Dim archiveCount As Long
    Dim folderCount As Long
    Dim CurrDir As String
    Dim ReportPath As String
    Dim textFiles As New List(Of String)
    Dim archiveFiles As New List(Of String)
    Dim voiceFiles As New List(Of String)
    Dim MASTERS_folder As New List(Of String)
    Dim MASTERS_name As New List(Of String)
    Dim MASTERS_priority As New List(Of String)

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
                    foldercount += 1
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
        On Error Resume Next
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
            archivecount = 0
            voicecount = 0
            textcount = 0
            foldercount = 0
            archiveFiles.Clear()
            textFiles.Clear()
            voicefiles.Clear()
            StatusLabel.Text = "خواندن سیستم فایل"
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
                    ElseIf file.ToLower.Contains("\archive\") And file.ToLower.Contains(URLFilterTextbox.Text.ToLower) Then
                        archiveCount += 1
                        archiveFiles.Add(file)
                    End If
                    ''  End If

                Next
            Next
            FileSystemTreeView.Update()
            FileSystemTreeView.Nodes(0).Expand()
            StatusLabel.Text = "آماده"
            FileCountLabel.Text = (archivecount + textcount + voicecount).ToString
            FolderCountLabel.Text = foldercount.ToString
            FileSystemTreeView.UseWaitCursor = False
            Loading.Close()
            ProgressBar1.Maximum = archivecount + textcount + voicecount
            ' ProgressBar1.Hide()
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
        ProgressBar1.Maximum = archivecount + textcount + voicecount
    End Sub

    Private Sub FileSystemTreeView_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles FileSystemTreeView.BeforeExpand
        My.Application.DoEvents()
    End Sub

    Private Sub ReportMainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error Resume Next
        Loading.Show()
        currdir = CurDir()
        foldercount = 0
        textcount = 0
        voicecount = 0
        archivecount = 0
        FileCreationDate.Text = Now.Year.ToString
        If My.Computer.FileSystem.FileExists(CurDir() + "/config.inf") Then
            FileOpen(1, CurDir() + "/config.inf", OpenMode.Input, OpenAccess.Default)
            While Not EOF(1)
                Dim input = LineInput(1)
                Select Case input
                    Case "#PATH"
                        PathTxt.Text = LineInput(1)
                    Case "#REPORTPATH"
                        reportpath = LineInput(1)
                    Case "#FILECREATIONDATE"
                        FileCreationDate.Text = LineInput(1)
                    Case "#STRINGFILTER"
                        URLFilterTextbox.Text = LineInput(1)
                End Select
            End While
            FileClose(1)
        End If
        If My.Computer.FileSystem.FileExists(CurDir() + "\monitoring.xml") Then
            Monitoring.ReadXml(CurDir() + "\monitoring.xml")
        End If
        
        hidereportwindow()

        Loading.Hide()
    End Sub


    Private Sub ProcessButton_Click(sender As Object, e As EventArgs) Handles ProcessButton.Click
        Loading.Show()
        ProcessButton.Enabled = False
        hidereportwindow()
        StatusLabel.Text = "تهیه گزارش"
        ReadFileSystem()
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = archiveFiles.Count + voicefiles.Count + textFiles.Count
        Dim AbstractReportText As New TextBox
        Dim HTMLHead As String = "<!DOCTYPE html><html dir=rtl><body><head><link href='reporttable.css' rel='stylesheet' type='text/css'/></head><div id='content'><div class='wrap'><div class='reporthead'>زمان تهیه گزارش : " + Now.ToString + "</div>"
        Dim Report1TableHead = "<table class='main' width='100%'  border='0'><tr><th>استاد</th><th>درس</th><th>صوت</th><th>متن </th></tr>"
        Dim Report2TableHead = "<table class='main' width='100%'  border='0'><tr><th>استاد</th><th>درس</th><th>جلسات</th><th>صوت</th><th>متن </th><th>کلمات</th><th>پاورقی</th><th>پاورقی بدون لینک</th><th>بدون شاهد مثال</th></tr>"
        Dim HTMLFoot As String = "</table></div></div></body></html>"
        Dim LastMasterFolderName As String = ""
        Dim LocalReportText As New TextBox
        Dim LocalReportTextAbstract As New TextBox
        Dim ThisMasterReport As New TextBox
        Dim RowSpan As Integer = 0

        For Each Archive As String In archiveFiles
            Dim MasterFolderName As String = ""
            Dim CourseTitle As String = ""
            Dim CourseTopic As String = ""
            Dim MasterName As String = ""
            Dim TedadJalase As Integer = 0
            Dim vcfilecount As Integer = 0
            Dim txtfilecounts As Integer = 0
            Dim wordcount As Long = 0
            Dim pavaraghi As Integer = 0
            Dim pavaraghiLinked As Integer = 0
            Dim shahedmesal As Integer = 0
            Dim pavaraghiError = 0
            Dim totalPavaraghiError As Integer = 0
            Dim totalPavaraghi As Integer = 0
            Dim totalshahedmesal As Integer = 0
            Dim pavaraghiErrorShow As String = ""
            Dim shahedmesalerrorshow As String = ""
            If Archive.ToLower.Contains("\ar\") = False Then
                ' HERE IS PERSIAN ARCHIVE FILE
                Dim CourseDirectory = Archive.Substring(Archive.ToLower.IndexOf("archive\") + 8)

                If CourseDirectory.Contains("\") = False Then
                    Continue For
                End If

                CourseDirectory = CourseDirectory.Substring(0, CourseDirectory.LastIndexOf("\"))
                If CourseDirectory.IndexOf("\") > 0 Then
                    MasterFolderName = CourseDirectory.Substring(0, CourseDirectory.IndexOf("\"))
                Else
                    Continue For
                End If
               


                If Archive.Contains(".htm") Or Archive.Contains(".html") Then
                    WebBrowser1.Navigate(Archive)
                Else
                    Continue For
                End If

                While (WebBrowser1.ReadyState <> WebBrowserReadyState.Complete)
                    My.Application.DoEvents()
                End While

                For Each row As DataRow In Monitoring.Tables("list").Rows
                    If row("path").ToString.ToLower.Contains(CourseDirectory.ToLower) Then
                        CourseTopic = row("lesson").ToString
                        MasterName = row("teacher").ToString
                    End If
                Next

                Dim theElementCollection As HtmlElementCollection = Nothing
                theElementCollection = WebBrowser1.Document.GetElementsByTagName("div")

                Dim hasCourseHeader As Boolean = False

                For Each curElement As HtmlElement In theElementCollection

                    If InStr(curElement.GetAttribute("classname").ToString, "course-header") Then
                        CourseTitle = curElement.GetAttribute("InnerText")
                        CourseTitle = CourseTitle.Substring(CourseTitle.IndexOf("درس") + 3)
                        hasCourseHeader = True

                    End If
                Next


                If CourseTopic = "" Then
                    CourseTopic = CourseTitle
                End If


                If hasCourseHeader = True Then

                    If LastMasterFolderName <> MasterFolderName Then
                        LocalReportText.Text = LocalReportText.Text.Replace("$R$", RowSpan.ToString)
                        LocalReportTextAbstract.Text = LocalReportTextAbstract.Text.Replace("$R$", RowSpan.ToString)

                        ThisMasterReport.Text = ThisMasterReport.Text.Replace("$R$", RowSpan.ToString)
                        '    // save master 
                        Dim rpt As New TextBox
                        rpt.AppendText(HTMLHead)
                        rpt.AppendText(Report2TableHead)
                        rpt.AppendText(ThisMasterReport.Text)
                        rpt.AppendText(HTMLFoot)
                        Dim rptfn = ReportPath + "\" + LastMasterFolderName + "\default.html"
                        If My.Computer.FileSystem.DirectoryExists(ReportPath + "\" + LastMasterFolderName + "\") = False Then
                            My.Computer.FileSystem.CreateDirectory(ReportPath + "\" + LastMasterFolderName + "\")
                        End If
                        If Not File.Exists(rptfn) Then
                            Using sw As New StreamWriter(rptfn, True, System.Text.Encoding.UTF8)
                                sw.Write(rpt.Text)
                            End Using
                        Else
                            My.Computer.FileSystem.DeleteFile(rptfn)
                            Using sw As New StreamWriter(rptfn, True, System.Text.Encoding.UTF8)
                                sw.Write(rpt.Text)
                            End Using
                        End If
                        If My.Computer.FileSystem.FileExists(ReportPath + "\" + LastMasterFolderName + "\" + "reporttable.css") = False Then
                            My.Computer.FileSystem.CopyFile(CurDir() + "\reporttable.css", ReportPath + "\" + LastMasterFolderName + "\" + "reporttable.css")
                        End If

                        ' End saving
                        ThisMasterReport.Clear()
                        RowSpan = 1
                        LastMasterFolderName = MasterFolderName

                    Else
                        RowSpan += 1
                    End If
                    Dim tablerows As HtmlElementCollection = WebBrowser1.Document.GetElementsByTagName("tr")
                    For Each curElement As HtmlElement In tablerows
                        If InStr(curElement.GetAttribute("id").ToString, "lesson") Then
                            TedadJalase += 1
                        End If
                    Next

                    Dim pavaraghiErrorFileReport As String = "<html dir=rtl><body><link href='reporttable.css' rel='stylesheet' type='text/css'/><div id='content'><div class='wrap'><div class='reporthead'>زمان تهیه گزارش : " + Now.ToString + "</div><table class='main' width='100%'  border='0'><tr><th>آدرس فایل</th><th>خطا</th><th>پاورقی</th><th>لینک</th><th>بدون شاهد مثال</th></tr>"

                    For Each txt As String In textFiles
                        pavaraghi = 0
                        pavaraghiError = 0
                        pavaraghiLinked = 0

                        If txt.ToLower.Contains(CourseDirectory.ToLower) Then
                            txtfilecounts += 1

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
                                    If href.Contains("file:/") Then
                                        pavaraghi += 1
                                    End If
                                    If href.Contains("http:/") Then
                                        pavaraghiLinked += 1
                                        If href.IndexOf(".ir") > 0 Then
                                            Dim hrefSub = href.Substring(href.IndexOf(".ir"))
                                            Dim scount = CountCharacter(hrefSub, "/")
                                            If scount < 4 Then
                                                shahedmesal += 1
                                            End If
                                        End If
                                    End If
                                Next

                                pavaraghi = pavaraghi / 2
                                pavaraghiError = pavaraghi - pavaraghiLinked

                                If pavaraghiError < 0 Then
                                    pavaraghiError *= -1
                                End If

                                totalPavaraghiError += pavaraghiError
                                totalPavaraghi += pavaraghi
                                totalshahedmesal += shahedmesal

                                Dim txtr = txt.Substring(txt.ToLower.IndexOf("\text\") + 6)
                                txtr = "<a href=http://eshia.ir/feqh/archive/text/" + txtr + " >" + txtr.Replace("\default.htm", "") + "</a>"
                                txtr = txtr.Replace("\", "/")
                                pavaraghiErrorFileReport += "<tr><td>" + txtr + "</td><td>" + pavaraghiError.ToString + "</td><td>" + pavaraghi.ToString + "</td><td>" + pavaraghiLinked.ToString + "</td><td>" + shahedmesal.ToString + "</td></tr>"

                            End If

                            If wordCountCheckBox.Checked = True Then
                                Dim bodytext = WebBrowser2.Document.Body.InnerText
                                For Counter = 1 To Len(bodytext)
                                    If Mid(bodytext, Counter, 1) = " " Then
                                        wordcount = wordcount + 1
                                    End If
                                Next Counter
                            End If
                            If ProgressBar1.Value < ProgressBar1.Maximum Then
                                ProgressBar1.Value += 1
                            End If
                        End If

                    Next

                    pavaraghiErrorFileReport += "</table></body></html>"

                    If My.Computer.FileSystem.DirectoryExists(ReportPath + CourseDirectory + "\") = False Then
                        My.Computer.FileSystem.CreateDirectory(ReportPath + CourseDirectory + "\")

                    End If

                    Dim errorReportFile = ReportPath + CourseDirectory + "\default.htm"
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
                    If My.Computer.FileSystem.FileExists(ReportPath + CourseDirectory + "\reporttable.css") = False Then
                        My.Computer.FileSystem.CopyFile(CurrDir + "\reporttable.css", ReportPath + CourseDirectory + "\reporttable.css")
                    End If
                    pavaraghiErrorShow = "<a href=file://" + errorReportFile + ">" + totalPavaraghiError.ToString + "</a>"
                    shahedmesalerrorshow = "<a href=file://" + errorReportFile + ">" + totalshahedmesal.ToString + "</a>"
                    '--voice counts

                    For Each vcfile As String In voiceFiles
                        If vcfile.ToLower.Contains(CourseDirectory.ToLower) Then
                            vcfilecount += 1
                            If ProgressBar1.Value < ProgressBar1.Maximum Then
                                ProgressBar1.Value += 1
                            End If
                        End If
                    Next

                End If

                Dim masterID As String

                If My.Computer.FileSystem.FileExists(PathTxt.Text + "\images\masters\" + LastMasterFolderName.ToLower + ".jpg") Then
                    masterID = "<img src=file://" + PathTxt.Text + "\images\masters\" + LastMasterFolderName.ToLower + ".jpg><br>" + MasterName + "<br>"
                Else
                    masterID = "<img src=file://" + PathTxt.Text + "\images\masters\default.jpg><br>" + MasterName + "<br>"

                End If
                If RowSpan <= 1 Then
                    LocalReportText.AppendText("<tr><td rowspan=$R$>" + masterID + "</td><td>" + CourseTopic + "</td><td> " + (TedadJalase).ToString + "</td><td>" + vcfilecount.ToString + "</td><td>" + txtfilecounts.ToString + "</td><td>" + wordcount.ToString + "</td><td>" + totalPavaraghi.ToString + "</td><td>" + pavaraghiErrorShow + "</td><td>" + shahedmesalerrorshow + "</td>")
                    LocalReportTextAbstract.AppendText("<tr><td rowspan=$R$>" + masterID + "</td><td>" + CourseTopic + "</td><td>" + vcfilecount.ToString + "</td><td>" + txtfilecounts.ToString + "</td>")

                    ThisMasterReport.AppendText("<tr><td rowspan=$R$>" + masterID + "</td><td>" + CourseTopic + "</td><td> " + (TedadJalase).ToString + "</td><td>" + vcfilecount.ToString + "</td><td>" + txtfilecounts.ToString + "</td><td>" + wordcount.ToString + "</td><td>" + totalPavaraghi.ToString + "</td><td>" + pavaraghiErrorShow + "</td><td>" + shahedmesalerrorshow + "</td>")

                Else
                    LocalReportText.AppendText("<tr><td>" + CourseTopic + "</td><td> " + (TedadJalase).ToString + "</td><td>" + vcfilecount.ToString + "</td><td>" + txtfilecounts.ToString + "</td><td>" + wordcount.ToString + "</td><td>" + totalPavaraghi.ToString + "</td><td>" + pavaraghiErrorShow + "</td><td>" + shahedmesalerrorshow + "</td></tr>")
                    LocalReportTextAbstract.AppendText("<tr><td>" + CourseTopic + "</td><td>" + vcfilecount.ToString + "</td><td>" + txtfilecounts.ToString + "</td>")
                    ThisMasterReport.AppendText("<tr><td>" + CourseTopic + "</td><td> " + (TedadJalase).ToString + "</td><td>" + vcfilecount.ToString + "</td><td>" + txtfilecounts.ToString + "</td><td>" + wordcount.ToString + "</td><td>" + totalPavaraghi.ToString + "</td><td>" + pavaraghiErrorShow + "</td><td>" + shahedmesalerrorshow + "</td></tr>")

                End If

                '    // save master 
                Dim rpt2 As New TextBox
                rpt2.AppendText(HTMLHead)
                rpt2.AppendText(Report2TableHead)
                rpt2.AppendText(ThisMasterReport.Text)
                rpt2.AppendText(HTMLFoot)
                Dim rptfn2 = ReportPath + "\" + LastMasterFolderName + "\default.html"
                If My.Computer.FileSystem.DirectoryExists(ReportPath + "\" + LastMasterFolderName + "\") = False Then
                    My.Computer.FileSystem.CreateDirectory(ReportPath + "\" + LastMasterFolderName + "\")
                End If
                If Not File.Exists(rptfn2) Then
                    Using sw As New StreamWriter(rptfn2, True, System.Text.Encoding.UTF8)
                        sw.Write(rpt2.Text)
                    End Using
                Else
                    My.Computer.FileSystem.DeleteFile(rptfn2)
                    Using sw As New StreamWriter(rptfn2, True, System.Text.Encoding.UTF8)
                        sw.Write(rpt2.Text)
                    End Using
                End If
                If My.Computer.FileSystem.FileExists(ReportPath + "\" + LastMasterFolderName + "\" + "reporttable.css") = False Then
                    My.Computer.FileSystem.CopyFile(CurDir() + "\reporttable.css", ReportPath + "\" + LastMasterFolderName + "\" + "reporttable.css")
                End If

                ' End saving

                If ProgressBar1.Value < ProgressBar1.Maximum Then
                    ProgressBar1.Value += 1
                End If


            Else
                ' HERE IS ARABIC ARCHIVE FILE






                Dim CourseDirectory = Archive.Substring(Archive.ToLower.IndexOf("archive\") + 8)

                If CourseDirectory.Contains("\") = False Then
                    Continue For
                End If

                CourseDirectory = CourseDirectory.Substring(0, CourseDirectory.LastIndexOf("\"))
                If CourseDirectory.IndexOf("\") > 0 Then
                    MasterFolderName = CourseDirectory.Substring(0, CourseDirectory.IndexOf("\"))
                Else
                    Continue For
                End If



                If Archive.Contains(".htm") Or Archive.Contains(".html") Then
                    WebBrowser1.Navigate(Archive)
                Else
                    Continue For
                End If

                While (WebBrowser1.ReadyState <> WebBrowserReadyState.Complete)
                    My.Application.DoEvents()
                End While

                For Each row As DataRow In Monitoring.Tables("list").Rows
                    If row("path").ToString.ToLower.Contains(CourseDirectory.ToLower) Then
                        CourseTopic = row("lesson").ToString
                        MasterName = row("teacher").ToString
                    End If
                Next

                If MasterName = "" Then
                    MasterName = MasterFolderName
                End If

                
                Dim theElementCollection As HtmlElementCollection = Nothing
                theElementCollection = WebBrowser1.Document.GetElementsByTagName("div")

                Dim hasCourseHeader As Boolean = False

                For Each curElement As HtmlElement In theElementCollection

                    If InStr(curElement.GetAttribute("classname").ToString, "course-header") Then
                        CourseTitle = curElement.GetAttribute("InnerText")
                        CourseTitle = CourseTitle.Substring(CourseTitle.IndexOf("درس") + 3)
                        hasCourseHeader = True

                    End If
                Next

                If CourseTopic = "" Then
                    CourseTopic = CourseTitle
                End If

                If hasCourseHeader = True Then

                    If LastMasterFolderName <> MasterFolderName Then
                        LocalReportText.Text = LocalReportText.Text.Replace("$R$", RowSpan.ToString)
                        LocalReportTextAbstract.Text = LocalReportTextAbstract.Text.Replace("$R$", RowSpan.ToString)
                        ThisMasterReport.Text = ThisMasterReport.Text.Replace("$R$", RowSpan.ToString)
                        '    // save master 
                        Dim rpt As New TextBox
                        rpt.AppendText(HTMLHead)
                        rpt.AppendText(Report2TableHead)
                        rpt.AppendText(ThisMasterReport.Text)
                        rpt.AppendText(HTMLFoot)
                        Dim rptfn = ReportPath + "\" + LastMasterFolderName + "\default.html"
                        If My.Computer.FileSystem.DirectoryExists(ReportPath + "\" + LastMasterFolderName + "\") = False Then
                            My.Computer.FileSystem.CreateDirectory(ReportPath + "\" + LastMasterFolderName + "\")
                        End If
                        If Not File.Exists(rptfn) Then
                            Using sw As New StreamWriter(rptfn, True, System.Text.Encoding.UTF8)
                                sw.Write(rpt.Text)
                            End Using
                        Else
                            My.Computer.FileSystem.DeleteFile(rptfn)
                            Using sw As New StreamWriter(rptfn, True, System.Text.Encoding.UTF8)
                                sw.Write(rpt.Text)
                            End Using
                        End If
                        If My.Computer.FileSystem.FileExists(ReportPath + "\" + LastMasterFolderName + "\" + "reporttable.css") = False Then
                            My.Computer.FileSystem.CopyFile(CurDir() + "\reporttable.css", ReportPath + "\" + LastMasterFolderName + "\" + "reporttable.css")
                        End If

                        ' End saving
                        ThisMasterReport.Clear()
                        RowSpan = 1
                        LastMasterFolderName = MasterFolderName

                    Else
                        RowSpan += 1
                    End If
                    Dim tablerows As HtmlElementCollection = WebBrowser1.Document.GetElementsByTagName("tr")
                    For Each curElement As HtmlElement In tablerows
                        If InStr(curElement.GetAttribute("id").ToString, "lesson") Then
                            TedadJalase += 1
                        End If
                    Next

                    Dim pavaraghiErrorFileReport As String = "<html dir=rtl><body><link href='reporttable.css' rel='stylesheet' type='text/css'/><div id='content'><div class='wrap'><div class='reporthead'>زمان تهیه گزارش : " + Now.ToString + "</div><table class='main' width='100%'  border='0'><tr><th>آدرس فایل</th><th>خطا</th><th>پاورقی</th><th>لینک</th><th>بدون شاهد مثال</th></tr>"


                    Dim sample As String = Archive
                    Dim base = sample.Substring(sample.ToLower.IndexOf("archive\") + 8)
                    Dim years = base.Substring(0, base.LastIndexOf("\"))
                    years = years.Substring(years.LastIndexOf("\") + 1)
                    Dim year1 As String = years.Substring(0, 2)
                    Dim year2 As String = years.Substring(3)
                    base = base.Substring(0, base.LastIndexOf("\"))
                    'MsgBox(base)
                    base = base.Substring(0, base.LastIndexOf("\"))
                    base = "\" + base + "\"
                    Dim coursestr1 = base + year1 + "\"
                    Dim coursestr2 = base + year2 + "\"
                    ' MsgBox(coursestr1 + vbNewLine + coursestr2)



                    For Each txt As String In textFiles
                        pavaraghi = 0
                        pavaraghiError = 0
                        pavaraghiLinked = 0

                        If txt.ToLower.Contains(coursestr1.ToLower) Then
                            txtfilecounts += 1

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
                                    If href.Contains("file:/") Then
                                        pavaraghi += 1
                                    End If
                                    If href.Contains("http:/") Then
                                        pavaraghiLinked += 1
                                        If href.IndexOf(".ir") > 0 Then
                                            Dim hrefSub = href.Substring(href.IndexOf(".ir"))
                                            Dim scount = CountCharacter(hrefSub, "/")
                                            If scount < 4 Then
                                                shahedmesal += 1
                                            End If
                                        End If
                                    End If
                                Next

                                pavaraghi = pavaraghi / 2
                                pavaraghiError = pavaraghi - pavaraghiLinked

                                If pavaraghiError < 0 Then
                                    pavaraghiError *= -1
                                End If

                                totalPavaraghiError += pavaraghiError
                                totalPavaraghi += pavaraghi
                                totalshahedmesal += shahedmesal

                                Dim txtr = txt.Substring(txt.ToLower.IndexOf("\text\") + 6)
                                txtr = "<a href=http://eshia.ir/feqh/archive/text/" + txtr + " >" + txtr.Replace("\default.htm", "") + "</a>"
                                txtr = txtr.Replace("\", "/")
                                pavaraghiErrorFileReport += "<tr><td>" + txtr + "</td><td>" + pavaraghiError.ToString + "</td><td>" + pavaraghi.ToString + "</td><td>" + pavaraghiLinked.ToString + "</td><td>" + shahedmesal.ToString + "</td></tr>"

                            End If

                            If wordCountCheckBox.Checked = True Then
                                Dim bodytext = WebBrowser2.Document.Body.InnerText
                                For Counter = 1 To Len(bodytext)
                                    If Mid(bodytext, Counter, 1) = " " Then
                                        wordcount = wordcount + 1
                                    End If
                                Next Counter
                            End If
                            If ProgressBar1.Value < ProgressBar1.Maximum Then
                                ProgressBar1.Value += 1
                            End If
                        End If

                    Next

                    pavaraghiErrorFileReport += "</table></body></html>"

                    If My.Computer.FileSystem.DirectoryExists(ReportPath + CourseDirectory + "\") = False Then
                        My.Computer.FileSystem.CreateDirectory(ReportPath + CourseDirectory + "\")

                    End If

                    Dim errorReportFile = ReportPath + CourseDirectory + "\default.htm"
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
                    If My.Computer.FileSystem.FileExists(ReportPath + CourseDirectory + "\reporttable.css") = False Then
                        My.Computer.FileSystem.CopyFile(CurrDir + "\reporttable.css", ReportPath + CourseDirectory + "\reporttable.css")
                    End If
                    pavaraghiErrorShow = "<a href=file://" + errorReportFile + ">" + totalPavaraghiError.ToString + "</a>"
                    shahedmesalerrorshow = "<a href=file://" + errorReportFile + ">" + totalshahedmesal.ToString + "</a>"
                    '--voice counts

                    For Each vcfile As String In voiceFiles
                        If vcfile.ToLower.Contains(CourseDirectory.ToLower) Then
                            vcfilecount += 1
                            If ProgressBar1.Value < ProgressBar1.Maximum Then
                                ProgressBar1.Value += 1
                            End If
                        End If
                    Next

                End If

                Dim masterID As String

                If My.Computer.FileSystem.FileExists(PathTxt.Text + "\images\masters\" + LastMasterFolderName.ToLower + ".jpg") Then
                    masterID = "<img src=file://" + PathTxt.Text + "\images\masters\" + LastMasterFolderName.ToLower + ".jpg><br>" + MasterName + "<br>"
                Else
                    masterID = "<img src=file://" + PathTxt.Text + "\images\masters\default.jpg><br>" + MasterName + "<br>"

                End If
                If RowSpan <= 1 Then
                    LocalReportText.AppendText("<tr><td rowspan=$R$>" + masterID + "</td><td>" + CourseTopic + "</td><td> " + (TedadJalase).ToString + "</td><td>" + vcfilecount.ToString + "</td><td>" + txtfilecounts.ToString + "</td><td>" + wordcount.ToString + "</td><td>" + totalPavaraghi.ToString + "</td><td>" + pavaraghiErrorShow + "</td><td>" + shahedmesalerrorshow + "</td>")
                    LocalReportTextAbstract.AppendText("<tr><td rowspan=$R$>" + masterID + "</td><td>" + CourseTopic + "</td><td>" + vcfilecount.ToString + "</td><td>" + txtfilecounts.ToString + "</td>")
                    ThisMasterReport.AppendText("<tr><td rowspan=$R$>" + masterID + "</td><td>" + CourseTopic + "</td><td> " + (TedadJalase).ToString + "</td><td>" + vcfilecount.ToString + "</td><td>" + txtfilecounts.ToString + "</td><td>" + wordcount.ToString + "</td><td>" + totalPavaraghi.ToString + "</td><td>" + pavaraghiErrorShow + "</td><td>" + shahedmesalerrorshow + "</td>")

                Else
                    LocalReportText.AppendText("<tr><td>" + CourseTopic + "</td><td> " + (TedadJalase).ToString + "</td><td>" + vcfilecount.ToString + "</td><td>" + txtfilecounts.ToString + "</td><td>" + wordcount.ToString + "</td><td>" + totalPavaraghi.ToString + "</td><td>" + pavaraghiErrorShow + "</td><td>" + shahedmesalerrorshow + "</td></tr>")
                    LocalReportTextAbstract.AppendText("<tr><td>" + CourseTopic + "</td><td>" + vcfilecount.ToString + "</td><td>" + txtfilecounts.ToString + "</td>")
                    ThisMasterReport.AppendText("<tr><td>" + CourseTopic + "</td><td> " + (TedadJalase).ToString + "</td><td>" + vcfilecount.ToString + "</td><td>" + txtfilecounts.ToString + "</td><td>" + wordcount.ToString + "</td><td>" + totalPavaraghi.ToString + "</td><td>" + pavaraghiErrorShow + "</td><td>" + shahedmesalerrorshow + "</td></tr>")

                End If

                '    // save master 
                Dim rpt2 As New TextBox
                rpt2.AppendText(HTMLHead)
                rpt2.AppendText(Report2TableHead)
                rpt2.AppendText(ThisMasterReport.Text)
                rpt2.AppendText(HTMLFoot)
                Dim rptfn2 = ReportPath + "\" + LastMasterFolderName + "\default.html"
                If My.Computer.FileSystem.DirectoryExists(ReportPath + "\" + LastMasterFolderName + "\") = False Then
                    My.Computer.FileSystem.CreateDirectory(ReportPath + "\" + LastMasterFolderName + "\")
                End If
                If Not File.Exists(rptfn2) Then
                    Using sw As New StreamWriter(rptfn2, True, System.Text.Encoding.UTF8)
                        sw.Write(rpt2.Text)
                    End Using
                Else
                    My.Computer.FileSystem.DeleteFile(rptfn2)
                    Using sw As New StreamWriter(rptfn2, True, System.Text.Encoding.UTF8)
                        sw.Write(rpt2.Text)
                    End Using
                End If
                If My.Computer.FileSystem.FileExists(ReportPath + "\" + LastMasterFolderName + "\" + "reporttable.css") = False Then
                    My.Computer.FileSystem.CopyFile(CurDir() + "\reporttable.css", ReportPath + "\" + LastMasterFolderName + "\" + "reporttable.css")
                End If

                ' End saving

                If ProgressBar1.Value < ProgressBar1.Maximum Then
                    ProgressBar1.Value += 1
                End If












                '' END OF ARABIC

            End If

        Next
        LocalReportText.Text = LocalReportText.Text.Replace("$R$", RowSpan.ToString)
        LocalReportTextAbstract.Text = LocalReportTextAbstract.Text.Replace("$R$", RowSpan.ToString)
        ThisMasterReport.Text = ThisMasterReport.Text.Replace("$R$", RowSpan.ToString)

        Dim report As New TextBox
        report.AppendText(HTMLHead)
        report.AppendText(Report2TableHead)
        report.AppendText(LocalReportText.Text)
        report.AppendText(HTMLFoot)

        Dim abstractReport As New TextBox
        abstractReport.AppendText(HTMLHead)
        abstractReport.AppendText(Report1TableHead)
        abstractReport.AppendText(LocalReportTextAbstract.text)
        abstractReport.AppendText(HTMLFoot)

        Dim repfilename = ReportPath + URLFilterTextbox.Text + "_Detailed_" + Now.Year.ToString + Now.Month.ToString + Now.Day.ToString + ".html"

        If Not File.Exists(repfilename) Then
            Using sw As New StreamWriter(repfilename, True, System.Text.Encoding.UTF8)
                sw.Write(report.Text)
            End Using
        Else
            My.Computer.FileSystem.DeleteFile(repfilename)
            Using sw As New StreamWriter(repfilename, True, System.Text.Encoding.UTF8)
                sw.Write(report.Text)
            End Using
        End If

        repfilename = ReportPath + URLFilterTextbox.Text + "_Abstract_" + Now.Year.ToString + Now.Month.ToString + Now.Day.ToString + ".html"

        If Not File.Exists(repfilename) Then
            Using sw As New StreamWriter(repfilename, True, System.Text.Encoding.UTF8)
                sw.Write(abstractReport.Text)
            End Using
        Else
            My.Computer.FileSystem.DeleteFile(repfilename)
            Using sw As New StreamWriter(repfilename, True, System.Text.Encoding.UTF8)
                sw.Write(abstractReport.Text)
            End Using
        End If


            If My.Computer.FileSystem.FileExists(ReportPath + "reporttable.css") = False Then
                My.Computer.FileSystem.CopyFile(CurDir() + "\reporttable.css", ReportPath + "reporttable.css")
            End If

            WebBrowser1.Navigate(repfilename)
            showreportwindow()
        ProgressBar1.Value = 0

        ProcessButton.Enabled = True


    End Sub



   

End Class


