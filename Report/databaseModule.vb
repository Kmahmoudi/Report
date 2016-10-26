Imports System.Data.SqlClient

Module DatabaseModule

    
    Public Sub spAddReport(ByVal Master As String, ByVal Path As String, ByVal CourseTitle As String, ByVal CourseTopic As String, ByVal Year As String, ByVal TextCount As Integer, ByVal VoiceCount As Integer, ByVal PavaraghiCount As Integer, ByVal PavaraghiLinkedCount As Integer, ByVal BedeoonShahedMesalCount As Integer, ByVal UpdateTimeStamp As String)
        Dim Connection As New SqlConnection(ReportMainForm.cString)
        Connection.Open()
        Dim Command As New SqlCommand("AddReport", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Command.Parameters.AddWithValue("@Master", Master)
        Command.Parameters.AddWithValue("@Path", Path)
        Command.Parameters.AddWithValue("@CourseTitle", CourseTitle)
        Command.Parameters.AddWithValue("@CourseTopic", CourseTopic)
        Command.Parameters.AddWithValue("@YEAR", Year)
        Command.Parameters.AddWithValue("@TextCount", TextCount)
        Command.Parameters.AddWithValue("@VoiceCount", VoiceCount)
        Command.Parameters.AddWithValue("@PavaraghiCount", PavaraghiCount)
        Command.Parameters.AddWithValue("@PavaraghiLinkedCount", PavaraghiLinkedCount)
        Command.Parameters.AddWithValue("@BedooneShahedmesalCount", BedeoonShahedMesalCount)
        Command.Parameters.AddWithValue("@UpdateTimeStamp", UpdateTimeStamp)
        Command.ExecuteNonQuery()
        Connection.Close()
        Command.Dispose()
        Connection.Dispose()
    End Sub


    Public Sub spAddDetail(ByVal master As String, ByVal Path As String, ByVal URI As String, ByVal PavaraghiCount As Integer, ByVal PavaraghiLinkedCount As Integer, ByVal NoLink As Integer, ByVal BedeoonShahedMesalCount As Integer)
        Dim Connection As New SqlConnection(ReportMainForm.cString)
        Connection.Open()
        Dim Command As New SqlCommand("addDetail", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Command.Parameters.AddWithValue("@master", master)
        Command.Parameters.AddWithValue("@Path", Path)
        Command.Parameters.AddWithValue("@URI", URI)
        Command.Parameters.AddWithValue("@PavaraghiCount", PavaraghiCount)
        Command.Parameters.AddWithValue("@PavaraghiLinkedCount", PavaraghiLinkedCount)
        Command.Parameters.AddWithValue("@NoLink", NoLink)
        Command.Parameters.AddWithValue("@BedooneShahedMesalCount", BedeoonShahedMesalCount)
        Command.ExecuteNonQuery()
        Connection.Close()
        Command.Dispose()
        Connection.Dispose()
    End Sub

    Public Function spGetReport(ByVal Master As String)
        Dim Connection As New SqlConnection(ReportMainForm.cString)
        Connection.Open()
        Dim Command As New SqlCommand("getReport", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Command.Parameters.AddWithValue("@master", Master)
        Dim DataAdapter As New SqlDataAdapter
        Dim DTable As New DataTable
        DataAdapter.SelectCommand = Command
        DTable.Clear()
        DataAdapter.Fill(DTable)
        Connection.Close()
        Return DTable
    End Function

    Public Function spGetDetailReport(ByVal Master As String)
        Dim Connection As New SqlConnection(ReportMainForm.cString)
        Connection.Open()
        Dim Command As New SqlCommand("getDetailReport", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Command.Parameters.AddWithValue("@master", Master)
        Dim DataAdapter As New SqlDataAdapter
        Dim DTable As New DataTable
        DataAdapter.SelectCommand = Command
        DTable.Clear()
        DataAdapter.Fill(DTable)
        Connection.Close()
        Return DTable
    End Function


    
End Module
