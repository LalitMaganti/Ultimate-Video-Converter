Public Class Startup
    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        MainBody.Text = "Welcome to Ultimate Video Converter.  Just one quick question before you can start converting. Currently I don't have a very good understanding of the OSes used by people using the converter. This dialog allows you to give me that information. By allowing me to collect this information this video converter can be improved massively. No other information will be collected and this will only happen one time." + Environment.NewLine + "Thank you"
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click
        If RadioButton2.IsChecked Then
            Dim OptionsFile As New IO.StreamWriter("options.ini")
            OptionsFile.WriteLine("[SendInfo] = Yes")
            OptionsFile.WriteLine("[AutoUpdate] = Yes")
            OptionsFile.WriteLine("[AutoFFmpeg] = Yes")
            OptionsFile.WriteLine("[FFmpegDate] = " & Now.ToString("yyyy.MM.dd"))
            OptionsFile.Close()
            Dim hi As New Transmission
            hi.Owner = Me.Owner
            hi.Show()
            Me.Close()
        Else
            Dim OptionsFile As New IO.StreamWriter("options.ini")
            OptionsFile.WriteLine("[SendInfo] = No")
            OptionsFile.WriteLine("[AutoUpdate] = Yes")
            OptionsFile.WriteLine("[AutoFFmpeg] = Yes")
            OptionsFile.WriteLine("[FFmpegDate] = " & Now.ToString("yyyy.MM.dd"))
            OptionsFile.Close()
            Me.Close()
        End If
    End Sub
    Private Sub Border1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Border1.MouseLeftButtonDown
        DragMove()
    End Sub
End Class
