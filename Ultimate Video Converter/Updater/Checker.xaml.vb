Imports System.Environment
Imports System.IO
Imports System.Net
Public Class Checker
    Public WithEvents BackgroundWorker1 As New ComponentModel.BackgroundWorker
    Public changelog As String
    Public version As String
    Public oldversion As String
    Public AppCheck As Boolean
    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        If AppCheck = True Then
            Label1.Content = "Checking For Video Converter Updates"
        Else
            Label1.Content = "Checking For FFmpeg Updates"
        End If
        properupdate = True
        Try
            BackgroundWorker1.WorkerSupportsCancellation = True
            BackgroundWorker1.WorkerReportsProgress = True
        Catch ex As Exception
        End Try
        BackgroundWorker1.RunWorkerAsync()
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        If AppCheck = True Then
            CheckUpdate(Me, True)
        Else
            CheckUpdate(Me, False)
        End If
    End Sub
    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        If e.ProgressPercentage = 1 Then
            Dim updateasker As New WPFAsker
            updateasker.textBox1.Text = changelog
            Dim downloader As New Downloader
            updateasker.label2.Content = "There is an update available from " & oldversion & " to " & version
            If AppCheck = True Then
                updateasker.label3.Content = "Do you want to update Ultimate Video Converter now?"
                downloader.AppUpdate = True
            Else
                updateasker.label3.Content = "Do you want to update FFmpeg now?"
                downloader.AppUpdate = False
            End If
            updateasker.Owner = Me
            Dim result = updateasker.ShowDialog
            Me.Close()
            Select Case result
                Case True
                    startupdate = True
                    downloader.Owner = Me.Owner
                    downloader.versionwritetofile = version
                    downloader.Show()
                Case Else
                    Exit Select
            End Select
        ElseIf e.ProgressPercentage = 2 Then
            Me.Close()
        End If
    End Sub
End Class
