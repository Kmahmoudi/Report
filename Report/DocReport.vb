Public Class DocReport

    Private Sub DocReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub DocReport_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ToolStripProgressBar1.Maximum = ReportMainForm.docFiles.Count
        For Each file In ReportMainForm.docFiles
            If ToolStripProgressBar1.Value + 1 < ToolStripProgressBar1.Maximum Then
                ToolStripProgressBar1.Value += 1
            Else
                ToolStripProgressBar1.Visible = False
            End If
            If My.Computer.FileSystem.GetFileInfo(file).Extension = ".doc" Then
                If (My.Computer.FileSystem.FileExists(file.Replace(".doc", ".docx"))) Then
                    TextBox1.AppendText(file + vbNewLine)
                End If
            End If
        Next
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        SaveFileDialog1.ShowDialog()
        If (SaveFileDialog1.FileName <> "") Then
            FileOpen(1, SaveFileDialog1.FileName, OpenMode.Output, OpenAccess.Default)
            FileSystem.Print(1, TextBox1.Text.ToString)
            FileClose(1)
        End If
    End Sub
End Class