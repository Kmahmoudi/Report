Module DateConvertModule

Public Function MiladiToShamsi(ByVal MDate As Date) As String

        MiladiToShamsi = ""

        Dim pc As New Globalization.PersianCalendar

        Dim Sal As Integer = pc.GetYear(MDate)

        Dim Mah As Integer = pc.GetMonth(MDate)

        Dim Roz As Integer = pc.GetDayOfMonth(MDate)

        MiladiToShamsi = Format(Sal, "0000") & "/" & Format(Mah, "00") & "/" & Format(Roz, "00")

        Return MiladiToShamsi

    End Function

End Module
