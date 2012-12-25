Imports System.IO
Imports System.ComponentModel
Imports System.Threading
Imports System.Collections.ObjectModel
Public Class Output
    Public lineno As Integer = 1
    Public duration1 As Double
    Public l As Integer
    Public ConversionNumber As Integer = 0
    Public stopwatch1 As New Stopwatch
    Public stopwatch2 As New Stopwatch
    Public WithEvents Timer1 As New Windows.Forms.Timer
    Public WithEvents Back As New BackgroundWorker
    Public LastGoodLine As String
    Dim GlobalTotalPercentage As String
    Dim globaltotalelapsedtimestring As String
    Dim globaltotalremainingtimestring As String
    Public ReferingWindow As MainWindow
    Public Sub Form2_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Loaded
        LabelTotal.Content = "0%"
        lblInd.Content = "Total Time Elapsed: Calculating"
        lblInd2.Content = "Total Time Remaining: Calculating"
        ListView1.View = DetailsListView()
        ListView1.ItemsSource = ListOfFiles
        If ConversionNumber = 0 Then
            For Each item In ListOfFiles
                item.status = "Waiting"
                item.timetaken = "Calculating"
                item.timeremaining = "Calculating"
                item.progress = "0%"
            Next
        End If
        ListView1.Items.Refresh()
        If ListOfFiles.Item(ConversionNumber).inputfullpath = ListOfFiles.Item(ConversionNumber).outputfullpath Then
            If QuietMode = False Then
                MsgBox("The input file is the same as the output file in conversion " & ConversionNumber + 1)
                MsgBox("Conversion " & ConversionNumber + 1 & " Failed")
            End If
            ListOfFiles.Item(ConversionNumber).status = "Failed"
            If ListOfFiles.Count = ConversionNumber + 1 Then
                MsgBox("All conversions have failed or finished")
                ListOfFiles.Clear()
                ReferingWindow.Close()
                Dim Mainform As New MainWindow
                Mainform.Show()
                Me.Close()
                Exit Sub
            Else
                ConversionNumber += 1
                Form2_Shown(sender, e)
                Exit Sub
            End If
        End If
        Timer1.Interval = 500
        If OverwriteAll = False Then
            If File.Exists(ListOfFiles.Item(ConversionNumber).outputfullpath) = True Then
                Dim h = MsgBox("Do you want to overwrite existing file?", MsgBoxStyle.YesNo)
                If h = MsgBoxResult.No Then
                    Timer1.Enabled = False
                    Dim hi = MsgBox("Conversion has been cancelled", MsgBoxStyle.OkOnly)
                    If hi = MsgBoxResult.Ok Then
                        cancelvar = True
                        ReferingWindow.Show()
                        Me.Close()
                        Exit Sub
                    End If
                End If
            End If
        End If
        ffmpeg.StartInfo.FileName = "ffmpeg.exe"
        ffmpeg.StartInfo.Arguments = " -i " & """" & ListOfFiles.Item(ConversionNumber).inputfullpath & """" & Generic.VideoCodec & Generic.AudioCodec & sizevar & abitrate & channels & vbitrate & samplerate & framerate & " -y " & """" & ListOfFiles.Item(ConversionNumber).outputfullpath & """" & " "
        ffmpeg.StartInfo.UseShellExecute = False
        ffmpeg.StartInfo.RedirectStandardError = True
        ffmpeg.StartInfo.RedirectStandardOutput = True
        ffmpeg.StartInfo.CreateNoWindow = True
        If ConversionNumber = 0 Then
            stopwatch2.Start()
        End If
        lineno = 0
        TextBlock1.AppendText("Conversion Number " & ConversionNumber + 1 & " started" + Environment.NewLine)
        Try
            ffmpeg.Start()
        Catch ex As Exception
        End Try
        ListOfFiles.Item(ConversionNumber).status = "Converting"
        ListView1.Items.Refresh()
        myStreamReader = ffmpeg.StandardError
        Timer1.Enabled = True
        Back.RunWorkerAsync()
        Timer1.Start()
        stopwatch1.Reset()
        stopwatch1.Start()
    End Sub
    Public Sub Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelAll.Click
        Timer1.Enabled = False
        ffmpeg.Kill()
        MsgBox("Conversion " & ConversionNumber + 1 & " and all remaining conversions have been cancelled")
        If File.Exists(ListOfFiles.Item(ConversionNumber).outputfullpath) Then
            File.Delete(ListOfFiles.Item(ConversionNumber).outputfullpath)
        End If
        cancelvar = True
        ReferingWindow.Show()
        Me.Close()
        Exit Sub
    End Sub
    Private Sub DetailsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DetailsButton.Click
        If DetailsButton.Content = "vv Show Details vv" Then
            TextBlock1.Visibility = Windows.Visibility.Visible
            Me.Height = 600
            DetailsButton.Content = "^^ Hide Details ^^"
            Me.Left = (SystemParameters.WorkArea.Width - Width) / 2 + SystemParameters.WorkArea.Left
            Me.Top = (SystemParameters.WorkArea.Height - Height) / 2 + SystemParameters.WorkArea.Top
        Else
            TextBlock1.Visibility = Windows.Visibility.Hidden
            Me.Height = 300
            DetailsButton.Content = "vv Show Details vv"
            Me.WindowStartupLocation = WindowStartupLocation.CenterScreen
            Me.Left = (SystemParameters.WorkArea.Width - Width) / 2 + SystemParameters.WorkArea.Left
            Me.Top = (SystemParameters.WorkArea.Height - Height) / 2 + SystemParameters.WorkArea.Top
        End If
    End Sub
    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Timer1.Enabled = False
        ffmpeg.Kill()
        ListOfFiles.Item(ConversionNumber).status = "Cancelled"
        ListOfFiles.Item(ConversionNumber).progress = "0%"
        ListOfFiles.Item(ConversionNumber).timeremaining = "Cancelled"
        ListOfFiles.Item(ConversionNumber).timeremaining = "Cancelled"
        ListView1.Items.Refresh()
        MsgBox("Conversion " & ConversionNumber + 1 & " has been cancelled")
        If File.Exists(ListOfFiles.Item(ConversionNumber).outputfullpath) Then
            File.Delete(ListOfFiles.Item(ConversionNumber).outputfullpath)
        End If
        If ListOfFiles.Count = ConversionNumber + 1 Then
            MsgBox("All conversions have been cancelled or finished")
            cancelvar = True
            Me.Close()
            ReferingWindow.Show()
            Exit Sub
        Else
            ConversionNumber += 1
            Form2_Shown(sender, e)
        End If
        ListView1.Items.Refresh()
    End Sub
    Private Sub Output_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        If Timer1.Enabled = True Then
            Add_Click(sender, e)
        End If
    End Sub
    Private Sub Border1_MouseLeftButtonDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Border1.MouseLeftButtonDown
        DragMove()
    End Sub
    Private Sub Back_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Back.DoWork
        Do Until myStreamReader.EndOfStream
            OneLine = myStreamReader.ReadLine()
            Dim progressofcurrent As String = ListOfFiles.Item(ConversionNumber).progress
            If progressofcurrent Is Nothing Then
                progressofcurrent = "0%"
            End If
            If (ffmpeg.HasExited And myStreamReader.EndOfStream) And Convert.ToInt32(progressofcurrent.Remove(progressofcurrent.IndexOf("%"))) >= 99 Then
                Timer1.Enabled = False
                cancelvar = False
                Exit Sub
            ElseIf (OneLine = "" Or OneLine Is Nothing) And ffmpeg.HasExited And myStreamReader.EndOfStream And progressofcurrent.Remove(progressofcurrent.IndexOf("%")) <> "100%" Then
                Timer1.Enabled = False
                If QuietMode = False Then
                    MsgBox("Conversion " & ConversionNumber + 1 & " Failed")
                End If
                If File.Exists(ListOfFiles.Item(ConversionNumber).outputfullpath) Then
                    File.Delete(ListOfFiles.Item(ConversionNumber).outputfullpath)
                End If
                Exit Sub
            Else
                Dim duration As Single = 0.0F, current As Single = 0.0F
                If OneLine.Contains("Duration: ") Then
                    Dim st1 As Integer = OneLine.IndexOf(",")
                    Dim st2 As String = OneLine.Remove(st1)
                    Dim st3 As String = st2.Remove(0, 10)
                    Dim st4 As String = st3.Trim(Convert.ToChar(":"))
                    Dim final As String = st4.Trim()
                    Dim time As TimeSpan = TimeSpan.Parse(final)
                    duration1 = time.TotalSeconds
                Else
                    If lineno > 10 Then
                        Try
                            If OneLine.Contains("time=") Then
                                Dim st1 = OneLine.IndexOf("B")
                                Dim st2 = OneLine.Substring(st1)
                                Dim fg = st2.Substring(st2.IndexOf("="))
                                Dim hi As Double = Convert.ToDouble(fg.Remove(fg.IndexOf("b")).Trim(Convert.ToChar("=")).Trim)
                                Dim percentage As Integer = Convert.ToInt32((hi / duration1) * 100)
                                GlobalTotalPercentage = Convert.ToInt64(Math.Round(percentage / (ListOfFiles.Count) + ((ConversionNumber * 100)) / (ListOfFiles.Count)))
                                ListOfFiles.Item(ConversionNumber).progress = percentage & "%"
                                If percentage <> 0 Then
                                    Dim elapsed = stopwatch1.Elapsed
                                    Dim elapsedsecs As Integer = Convert.ToInt32(elapsed.TotalSeconds)
                                    Dim int1 As Decimal = Convert.ToDecimal((elapsedsecs / percentage) * 100)
                                    Dim remainingtime As New TimeSpan(0, 0, Convert.ToInt32(int1 - elapsed.TotalSeconds))
                                    Dim elapsedtime As New TimeSpan(0, 0, elapsedsecs)
                                    Dim elapsedtimestring As String = elapsedtime.ToString
                                    Dim remainingtimestring As String = remainingtime.ToString
                                    If GlobalTotalPercentage <> 0 Then
                                        Dim totalelapsed = stopwatch2.Elapsed
                                        Dim totalelapsedsecs As Integer = Convert.ToInt32(totalelapsed.TotalSeconds)
                                        Dim totalint1 As Decimal = Convert.ToInt32((totalelapsedsecs / GlobalTotalPercentage) * 100)
                                        Dim totalremainingtime As New TimeSpan(0, 0, Convert.ToInt32(totalint1 - totalelapsed.TotalSeconds))
                                        Dim totalelapsedtime As New TimeSpan(0, 0, totalelapsedsecs)
                                        globaltotalelapsedtimestring = totalelapsedtime.ToString
                                        globaltotalremainingtimestring = totalremainingtime.ToString
                                    End If
                                    ListOfFiles.Item(ConversionNumber).timetaken = elapsedtimestring
                                    ListOfFiles.Item(ConversionNumber).timeremaining = remainingtimestring
                                End If
                            End If
                            LastGoodLine = OneLine
                        Catch ex As Exception
                        End Try
                    Else
                        lineno += 1
                    End If
                End If
            End If
        Loop
    End Sub
    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        TextBlock1.AppendText(OneLine + Environment.NewLine)
        LabelTotal.Content = GlobalTotalPercentage & "%"
        TextBlock1.ScrollToEnd()
        ProgressBar2.Maximum = 100
        ProgressBar2.Minimum = 0
        ProgressBar2.Value = GlobalTotalPercentage
        lblInd.Content = "Total Time Elapsed: " & globaltotalelapsedtimestring
        lblInd2.Content = "Total Time Remaining: " & globaltotalremainingtimestring
        ListView1.Items.Refresh()
    End Sub
    Private Sub Back_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Back.RunWorkerCompleted
        Dim progressofcurrent As String = ListOfFiles.Item(ConversionNumber).progress
        If progressofcurrent Is Nothing Then
            progressofcurrent = "0%"
        End If
        If (ffmpeg.HasExited And myStreamReader.EndOfStream) And Convert.ToInt32(progressofcurrent.Remove(progressofcurrent.IndexOf("%"))) >= 99 Then
            Timer1.Stop()
            TextBlock1.AppendText(OneLine + Environment.NewLine)
            LabelTotal.Content = GlobalTotalPercentage & "%"
            TextBlock1.ScrollToEnd()
            ProgressBar2.Maximum = 100
            ProgressBar2.Minimum = 0
            ProgressBar2.Value = GlobalTotalPercentage
            lblInd.Content = "Total Time Elapsed: " & globaltotalelapsedtimestring
            lblInd2.Content = "Total Time Remaining: " & globaltotalremainingtimestring
            ListView1.Items.Refresh()
            If ListOfFiles.Count = ConversionNumber + 1 Then
                ListOfFiles.Item(ConversionNumber).status = "Finished"
                MsgBox("All conversions have finished")
                ListOfFiles.Clear()
                ReferingWindow.Close()
                Dim MainForm1 As New MainWindow
                MainForm1.Show()
                Me.Close()
            Else
                ListOfFiles.Item(ConversionNumber).status = "Finished"
                ConversionNumber += 1
                ListView1.Items.Refresh()
                Form2_Shown(sender, e)
            End If
        ElseIf (OneLine = "" Or OneLine Is Nothing) And ffmpeg.HasExited And myStreamReader.EndOfStream And progressofcurrent.Remove(progressofcurrent.IndexOf("%")) <> "100%" Then
            If ListOfFiles.Count = ConversionNumber + 1 Then
                ListOfFiles.Item(ConversionNumber).status = "Failed"
                ListView1.Items.Refresh()
                MsgBox("All conversions have failed or finished")
                ListOfFiles.Clear()
                ReferingWindow.Show()
                Me.Close()
            Else
                ListOfFiles.Item(ConversionNumber).status = "Failed"
                ListView1.Items.Refresh()
                ConversionNumber += 1
                Form2_Shown(sender, e)
            End If
        End If
    End Sub
End Class
