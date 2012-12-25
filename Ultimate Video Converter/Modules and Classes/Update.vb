Imports System.IO
Imports System.Net
Module Update
    Public Sub CheckUpdate(ByVal referringForm As Checker, ByVal App As Boolean)
        Dim versionFileUrl As String
        Dim versionFileLocal = String.Format("{0}\{1}", Path.GetTempPath(), "version.txt")
        Dim client As New WebClient
        Dim versionfolderlocal = Path.GetTempPath
        If App = True Then
            versionFileUrl = "http://uvideoconverter.sourceforge.net/version.txt"
        Else
            versionFileUrl = "http://uvideoconverter.sourceforge.net/ffmpeg/version.txt"
        End If
        Try
            client.DownloadFile(versionFileUrl, versionFileLocal)
            If File.Exists(versionFileLocal) Then
                Dim tr = New StreamReader(versionFileLocal)
                Dim version = tr.ReadLine()
                tr.Close()
                Dim shortVersionFromFile = version.Replace(".", String.Empty)
                Dim oldversion = ""
                If App = True Then
                    oldversion = My.Application.Info.Version.ToString()
                    oldversion = "2.0.0.0"
                Else
                    tr = New StreamReader(startup2 & "\options.ini")
                    Do Until tr.EndOfStream
                        Dim Oneline1 = tr.ReadLine
                        If Oneline1.Contains("FFmpegDate") Then
                            Dim provisional = Oneline1.Substring(Oneline1.IndexOf("="))
                            oldversion = provisional.Trim("=").Trim.Trim
                            Exit Do
                        End If
                    Loop
                End If
                Dim shortVersionFromVrs As String = oldversion.Replace(".", String.Empty)
                If shortVersionFromVrs < shortVersionFromFile Then
                    Dim clientweb As New Net.WebClient
                    Dim rawchangelog = ""
                    If App = True Then
                        clientweb.DownloadFile("http://uvideoconverter.sourceforge.net/changelog.txt", versionfolderlocal & "\changelog.txt")
                    Else
                        clientweb.DownloadFile("http://uvideoconverter.sourceforge.net/ffmpeg/changelog.txt", versionfolderlocal & "\changelog.txt")
                    End If
                    Dim logreader = File.OpenText(versionfolderlocal & "\changelog.txt")
                    Dim changelog As String
                    Dim linein As String
                    Do Until logreader.EndOfStream
                        linein = logreader.ReadLine()
                        If linein.Contains(oldversion) = True Then
                            Exit Do
                        End If
                        rawchangelog += Environment.NewLine + linein
                    Loop
                    changelog = rawchangelog.Trim
                    referringForm.oldversion = oldversion
                    referringForm.changelog = changelog
                    referringForm.version = Version
                    referringForm.BackgroundWorker1.ReportProgress(1)
                    referringForm.BackgroundWorker1.WorkerSupportsCancellation = True
                    referringForm.BackgroundWorker1.CancelAsync()
                Else
                    If properupdate = True Then
                        If App = True Then
                            Dim result = MessageBox.Show("There are no upgrades currently available. You have the latest version of Ultimate Video Converter.", "No upgrade available", MessageBoxButton.OK, MessageBoxImage.Information)
                        Else
                            Dim result = MessageBox.Show("There are no upgrades currently available. You have the latest version of FFmpeg.", "No upgrade available", MessageBoxButton.OK, MessageBoxImage.Information)
                        End If
                        referringForm.BackgroundWorker1.ReportProgress(2)
                        referringForm.BackgroundWorker1.WorkerSupportsCancellation = True
                        referringForm.BackgroundWorker1.CancelAsync()
                        properupdate = False
                    End If
                End If
            Else
                If properupdate = True Then
                    Dim result = MessageBox.Show(String.Format("Version file not found or not accessible." & vbLf & "Please check that you have permission to read the {0} folder", versionfolderlocal), "Version information missing", MessageBoxButton.OK, MessageBoxImage.Exclamation)
                    referringForm.BackgroundWorker1.ReportProgress(2)
                    referringForm.BackgroundWorker1.WorkerSupportsCancellation = True
                    referringForm.BackgroundWorker1.CancelAsync()
                End If
            End If
        Catch ex As Exception
            If properupdate = True Then
                MsgBox(ex.Message)
            End If
        End Try
    End Sub
End Module
