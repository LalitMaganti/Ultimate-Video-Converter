Imports System.IO
Imports System.Net
Public Class Downloader
    Private WithEvents client As New WebClient
    Private length As Integer
    Private stopwatch1 As New Stopwatch
    Private localupdatefile As String
    Public AppUpdate As Boolean
    Public WithEvents Timer1 As New Forms.Timer
    Public WithEvents BackgroundWorker1 As New ComponentModel.BackgroundWorker
    Public versionwritetofile As String
    Public Sub Dialog1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Loaded
        If AppUpdate = True Then
            lblQuit.Visibility = Windows.Visibility.Hidden
        End If
        Label1.Content = "Starting"
        Label2.Content = "Starting"
        Label3.Content = "Starting"
        Timer1.Enabled = True
        BackgroundWorker1.WorkerSupportsCancellation = True
        BackgroundWorker1.RunWorkerAsync()
    End Sub
    Public Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        If AppUpdate = True Then
            Dim webupdatefile As String
            If File.Exists(startup2 & "\portable.exi") Then
                webupdatefile = "http://uvideoconverter.sourceforge.net/updateport.exe"
                localupdatefile = startup2 & "\updateport.exe"
            Else
                webupdatefile = "http://uvideoconverter.sourceforge.net/updatefile.exe"
                localupdatefile = startup2 & "\updatefile.exe"
            End If
            Dim theResponse = WebRequest.Create(webupdatefile).GetResponse
            stopwatch1.Start()
            length = Convert.ToInt16(theResponse.ContentLength / 1024)
            client.DownloadFile(webupdatefile, localupdatefile)
            Try
                If File.Exists(startup2 & "\updateport.exe") Or File.Exists(startup2 & "\updatefile.exe") Then
                    MsgBox("Ultimate Video Converter has been updated and will now restart")
                    Process.Start(localupdatefile, " /D=" & startup2)
                Else
                    MsgBox("Ultimate Video Converter has not been updated")
                End If
                End
            Catch ex As Exception
                Try
                    Process.Start(startup2 & "\updateport.exe", " /D=" & startup2)
                Catch hik As Exception
                    Dim result As Forms.DialogResult = MessageBox.Show("Error during upgrade." & vbLf & ex.Message.ToString() & ". You can update the application manually by downloading the appropriate file from http://sourceforge.net/projects/uvideoconverter/files/Stable/")
                End Try
            End Try
        Else
            Dim webupdatefile As String
            webupdatefile = "http://uvideoconverter.sourceforge.net/ffmpeg/ffmpeg.exe"
            localupdatefile = Path.GetTempPath & "\ffmpeg.exe"
            Dim theResponse As WebResponse
            Dim theRequest As WebRequest
            theRequest = WebRequest.Create(webupdatefile)
            theResponse = theRequest.GetResponse
            stopwatch1.Start()
            length = Convert.ToInt16(theResponse.ContentLength / 1024)
            Try
                client.DownloadFile(webupdatefile, localupdatefile)
            Catch ex As Exception
                If ex.Message.Contains("aborted") Then
                    MsgBox("Update cancelled")
                    BackgroundWorker1.CancelAsync()
                Else
                    MsgBox(ex.Message)
                End If
            End Try
            If BackgroundWorker1.CancellationPending = False Then
                Try
                    If File.Exists(Path.GetTempPath & "\ffmpeg.exe") Then
                        If File.Exists("ffmpeg.exe") Then
                            File.Delete("ffmpeg.exe")
                        End If
                        File.Copy(Path.GetTempPath & "\ffmpeg.exe", startup2 & "\ffmpeg.exe")
                        Dim Stream As New StreamReader(startup2 & "\options.ini")
                        Dim Streamw As New StreamWriter(startup2 & "\tempoptions.ini")
                        Do Until Stream.EndOfStream
                            Dim onelin = Stream.ReadLine
                            If onelin.Contains("FFmpegDate") = False Then
                                Streamw.WriteLine(onelin)
                            Else
                                Streamw.WriteLine("[FFmpegDate] = " & versionwritetofile)
                            End If
                        Loop
                        Stream.Close()
                        Streamw.Close()
                        File.Delete(startup2 & "\options.ini")
                        File.Move(startup2 & "\tempoptions.ini", startup2 & "\options.ini")
                        MsgBox("FFmpeg has been updated")
                    Else
                        MsgBox("FFmpeg has not been updated")
                    End If
                    End
                Catch ex As Exception
                    MessageBox.Show("There was a problem updating - please try later")
                End Try
            End If
        End If
    End Sub
    Public Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If IO.File.Exists(localupdatefile) Then
            Dim fileinf As New IO.FileInfo(localupdatefile)
            Dim downloaded As Integer = Convert.ToInt16(fileinf.Length / 1024)
            Label1.Content = downloaded & "KB downloaded out of " & length & "KB"
            Dim time As Integer = Convert.ToInt16(stopwatch1.ElapsedMilliseconds / 1000)
            If time <> 0 Then
                Dim speed As Integer = Convert.ToInt16(downloaded / time)
                Label2.Content = "Speed: " & speed & "KB/sec"
                Dim percent As Integer = Convert.ToInt16((downloaded / length) * 100)
                Label3.Content = percent & "% downloaded"
                ProgressBar1.Maximum = 100
                ProgressBar1.Minimum = 0
                ProgressBar1.Value = percent
                If percent = 100 Then
                    stopwatch1.Stop()
                    Timer1.Enabled = False
                End If
            End If
        Else
            Label1.Content = "Starting"
            Label2.Content = "Starting"
            Label3.Content = "Starting"
        End If
    End Sub
    Private Sub hypClose_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles hypClose.Click
        Timer1.Stop()
        Label1.Content = "Cancelled"
        Label2.Content = "Cancelled"
        Label3.Content = "Cancelled"
        client.CancelAsync()
    End Sub
    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Me.Owner.Focus()
        Me.Close()
    End Sub
End Class
