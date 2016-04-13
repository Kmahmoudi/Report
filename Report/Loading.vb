Public Class Loading

    Private Sub Loading_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Loading_MouseHover(sender As Object, e As EventArgs) Handles Me.MouseHover
        If ReportMainForm.StatusLabel.Text = "آماده" Then
            Me.Close()
        End If
    End Sub
End Class