Public Class Options
    Private Sub hypClose_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles hypClose.Click
        Dim Line(2) As String
        Dim OptionsFileReader As New IO.StreamReader("options.ini")
        For x = 0 To 2
            Line(x) = OptionsFileReader.ReadLine
        Next
        OptionsFileReader.Close()
        IO.File.Delete("options.ini")
        Dim OptionsFile As New IO.StreamWriter("options.ini")
        If CheckBox1.IsChecked Then
            OptionsFile.WriteLine("[AutoUpdate] = Yes")
        Else
            OptionsFile.WriteLine("[AutoUpdate] = No")
        End If
        If CheckBox2.IsChecked Then
            OptionsFile.WriteLine("[AutoFFmpeg] = Yes")
        Else
            OptionsFile.WriteLine("[AutoFFmpeg] = No")
        End If
        OptionsFile.Close()
        Me.Close()
    End Sub
    Private Sub Hyperlink1_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Hyperlink1.Click
        Me.Close()
    End Sub
    Private Sub Options_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Dim OptionsFile As New IO.StreamReader("options.ini")
        Do Until OptionsFile.EndOfStream
            Dim Line = OptionsFile.ReadLine()
            If Line.Contains("Update") Then
                If Line.Contains("Yes") Then
                    CheckBox1.IsChecked = True
                Else
                    CheckBox1.IsChecked = False
                End If
            ElseIf Line.Contains("FFmpeg") Then
                If Line.Contains("Yes") Then
                    CheckBox2.IsChecked = True
                Else
                    CheckBox2.IsChecked = False
                End If
            End If
        Loop
        OptionsFile.Close()
    End Sub
End Class
