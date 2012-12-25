Public Class Joiner
    Dim WithEvents Back As New ComponentModel.BackgroundWorker
    Dim hi As New Process
    Dim filelist As String = ""
    Dim hi2
    Dim WithEvents timer As New Windows.Forms.Timer
    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Try
            hi.StartInfo.FileName = "mencoder.exe"
            For Each file In ListOfFiles
                filelist += """" & file.inputfullpath & """" & " "
            Next
            hi.StartInfo.Arguments = " -oac copy -ovc copy -idx -o output.avi " & filelist
            InputBox("", "", hi.StartInfo.Arguments)
            hi.StartInfo.UseShellExecute = False
            hi.StartInfo.RedirectStandardOutput = True
            hi.StartInfo.RedirectStandardError = True
            hi.StartInfo.CreateNoWindow = False
            hi.StartInfo.CreateNoWindow = True
            hi.Start()
            hi2 = hi.StandardOutput
            Back.RunWorkerAsync()
            timer.Interval = 1000
            timer.Start()
        Catch ex As Exception
        End Try
    End Sub
    Dim k As String
    Private Sub Back_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Back.DoWork
        Do Until hi2.EndOfStream
            k = hi2.ReadLine
        Loop
    End Sub
    Private Sub timer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer.Tick
        RichTextBox1.AppendText(k + Environment.NewLine)
    End Sub
End Class
