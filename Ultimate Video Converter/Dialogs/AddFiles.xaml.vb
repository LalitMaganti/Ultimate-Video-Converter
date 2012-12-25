Imports System.IO
Public Class AddFiles
    Dim cancellation As Boolean = False
    Dim nextfile As Boolean = True
    Dim Opendialog As Forms.DialogResult = Forms.DialogResult.Cancel
    Dim FolderDialog As Forms.DialogResult = Forms.DialogResult.Cancel
    Dim OpenFileDialog1 As New Windows.Forms.OpenFileDialog
    Dim FolderBrowserDialog1 As New Windows.Forms.FolderBrowserDialog
    Dim hi As String
    Dim duration As String
    Dim currentfile As String
    Dim badfile As Boolean = False
    Dim WithEvents BackgroundWorker1 As New ComponentModel.BackgroundWorker
    Dim canceladdition As Boolean = False
    Property DragDropbool = False
    Property DragFiles As String()
    Private Sub AddFiles_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ContentRendered
        Dim OriginalCount = ListOfFiles.Count
        If DragDropbool = False Then
            If FileScan = True Then
                If Opendialog = Forms.DialogResult.OK Then
                    Dim no = OpenFileDialog1.FileNames.Count
                    Dim number = 0
                    badfile = False
                    nextfile = True
                    If OpenFileDialog1.FileName <> "" Then
                        For Each hi1 As String In OpenFileDialog1.FileNames
                            If canceladdition = False Then
                                number += 1
                                lblProgress.Text = "Processing " & number & " of " & no
                                ProgressBar1.Value = Convert.ToInt32((number / no) * 100)
                                Dim x As Integer
                                If ListOfFiles.Count <> 0 Then
                                    Do Until x = ListOfFiles.Count
                                        Dim hi3 As String = ListOfFiles.Item(x).inputfullpath
                                        If hi1 = hi3 Then
                                            nextfile = False
                                            Exit For
                                        End If
                                        x += 1
                                    Loop
                                End If
                                If nextfile = True Then
                                    currentfile = hi1
                                    BackgroundWorker1.RunWorkerAsync()
                                    While BackgroundWorker1.IsBusy
                                        Windows.Forms.Application.DoEvents()
                                    End While
                                    If hi = "" Then
                                        nextfile = False
                                        badfile = True
                                    End If
                                End If
                                If nextfile = True Then
                                    Dim nameoffileonly = IO.Path.GetFileNameWithoutExtension(hi1)
                                    Dim Fileno As New FileClass
                                    Fileno.outputfilenameonly = nameoffileonly
                                    Fileno.inputfullpath = hi1.Replace("""", "")
                                    Dim hhmmss As TimeSpan
                                    hhmmss = TimeSpan.FromMilliseconds(duration)
                                    Dim hi As String
                                    If hhmmss.ToString.Contains(".") Then
                                        hi = hhmmss.ToString.Remove(hhmmss.ToString.IndexOf("."))
                                    Else
                                        hi = hhmmss.ToString
                                    End If
                                    Fileno.duration = hi
                                    ListOfFiles.Add(Fileno)
                                End If
                                nextfile = True
                            Else
                                Exit For
                            End If
                        Next
                    End If

                End If
            Else
                If FolderDialog = Forms.DialogResult.OK Then
                    Dim MessageResult = MessageBox.Show("Scan all subdirectories (default - yes)?", "", MessageBoxButton.YesNo)
                    Dim no1 As String()
                    Try
                        If MessageResult = Forms.DialogResult.No Then
                            no1 = Directory.GetFiles(FolderBrowserDialog1.SelectedPath)
                        Else
                            no1 = Directory.GetFiles(FolderBrowserDialog1.SelectedPath, "*", SearchOption.AllDirectories)
                        End If
                    Catch ex As UnauthorizedAccessException
                        MsgBox("Correct permissions not available")
                        AddFiles_Loaded(sender, e)
                        AddFiles_Activated(sender, e)
                        Exit Sub
                    End Try
                    Dim no = no1.Count
                    Dim number = 0
                    badfile = False
                    nextfile = True
                    For Each hi1 As String In no1
                        If canceladdition = False Then
                            number += 1
                            lblProgress.Text = "Processing " & number & " of " & no
                            ProgressBar1.Value = Convert.ToInt32((number / no) * 100)
                            Dim x As Integer
                            Do Until x = ListOfFiles.Count
                                Dim hi3 As String = ListOfFiles.Item(x).inputfullpath
                                If hi1 = hi3 Then
                                    nextfile = False
                                    Exit For
                                End If
                                x += 1
                            Loop
                            If nextfile = True Then
                                currentfile = hi1
                                BackgroundWorker1.RunWorkerAsync()
                                While BackgroundWorker1.IsBusy
                                    Windows.Forms.Application.DoEvents()
                                End While
                                If hi = "" Then
                                    nextfile = False
                                    badfile = True
                                End If
                            End If
                            If nextfile = True Then
                                Dim nameoffileonly = IO.Path.GetFileNameWithoutExtension(hi1)
                                Dim Fileno As New FileClass
                                Fileno.outputfilenameonly = nameoffileonly
                                Fileno.inputfullpath = hi1.Replace("""", "")
                                Dim hhmmss As TimeSpan
                                hhmmss = TimeSpan.FromMilliseconds(duration)
                                Dim hi As String
                                If hhmmss.ToString.Contains(".") Then
                                    hi = hhmmss.ToString.Remove(hhmmss.ToString.IndexOf("."))
                                Else
                                    hi = hhmmss.ToString
                                End If
                                Fileno.duration = hi
                                ListOfFiles.Add(Fileno)
                            End If
                            nextfile = True
                        Else
                            Exit For
                        End If
                    Next
                End If
            End If
        Else
            Dim number = 0
            Dim no = DragFiles.Count
            For Each hi1 As String In DragFiles
                If canceladdition = False Then
                    number += 1
                    lblProgress.Text = "Processing " & number & " of " & no
                    ProgressBar1.Value = Convert.ToInt32((number / no) * 100)
                    Dim x As Integer = 0
                    If ListOfFiles.Count <> 0 Then
                        Do Until x = ListOfFiles.Count
                            Dim hi3 As String = ListOfFiles.Item(x).inputfullpath
                            If hi1 = hi3 Then
                                nextfile = False
                                Exit For
                            End If
                            x += 1
                        Loop
                    End If
                    If nextfile = True Then
                        currentfile = hi1
                        BackgroundWorker1.RunWorkerAsync()
                        While BackgroundWorker1.IsBusy
                            Windows.Forms.Application.DoEvents()
                        End While
                        If hi = "" Then
                            nextfile = False
                            badfile = True
                        End If
                    End If
                    If nextfile = True Then
                        Dim nameoffileonly = IO.Path.GetFileNameWithoutExtension(hi1)
                        Dim Fileno As New FileClass
                        Fileno.outputfilenameonly = nameoffileonly
                        Fileno.inputfullpath = hi1.Replace("""", "")
                        Dim hhmmss As TimeSpan
                        hhmmss = TimeSpan.FromMilliseconds(duration)
                        Dim hi As String
                        If hhmmss.ToString.Contains(".") Then
                            hi = hhmmss.ToString.Remove(hhmmss.ToString.IndexOf("."))
                        Else
                            hi = hhmmss.ToString
                        End If
                        Fileno.duration = hi
                        ListOfFiles.Add(Fileno)
                    End If
                    nextfile = True
                Else
                    Exit For
                End If
            Next
        End If
        cancellation = True
        If badfile = True Then
            MsgBox("Non video/audio file(s) which were found were not added")
        End If
        If ListOfFiles.Count = OriginalCount Then
            MessageBox.Show("No video/audio files were found")
        End If
        Me.Close()
    End Sub
    Private Sub AddFiles_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        If cancellation = False Then
            e.Cancel = True
        End If
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim Mediainfo1 As New MediaInfo
        Mediainfo1.Open(currentfile)
        hi = Mediainfo1.Get_(StreamKind.Audio, 0, "Codec/Info")
        If hi = "" Then
            hi = Mediainfo1.Get_(StreamKind.Visual, 0, "Codec")
        End If
        If hi <> "" Then
            Try
                duration = Mediainfo1.Get_(StreamKind.General, 0, "Duration")
            Catch ex As Exception
                duration = "N/A"
            End Try
        End If
        Mediainfo1.Close()
    End Sub
    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Cancel.Click
        canceladdition = True
    End Sub
    Private Sub AddFiles_Loaded(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Loaded
        lblProgress.Text = ""
        OpenFileDialog1.Multiselect = True
        FolderBrowserDialog1.ShowNewFolderButton = False
        If DragDropbool = False Then
            If FileScan = True Then
                OpenFileDialog1.Filter = "Supported FFmpeg file types (*.mts; *.mp2; *.mp3; *.avi; *.fli; *.mkv; *.flv; *.3gp; *.pva; *.amr; *.rmvb; *.ape; *.m2v; *.m2ts; *.mov; *.mxf; *.amv; *.mpeg; *.m4b; *.pss; *.mpa; *.mpv; *.aac; *.avs; *.rec; *.vdr; *.ogg; *.dvr-ms; *.ogm; *.wav; *.ac3; *.svx; *.alac; *.bmp; *.dts; *.flac; *.flc; *.h264; *.ivf; *.drc; *.gsm; *.ogv; *.aff; *.mdc; *.mm; *.m4a; *.mpc; *.mpg; *.pcx; *.ptx; *.ra; *.roq; *.rm; *.shn; *.smk; *.sun; *.tif; *.tta; *.wma; *.wmv; *.3g2; *.4xm; *.aea; *.aif; *.apc; *.asf; *.ssa; *.au; *.vid; *.c93; *.caf; *.crc; *.302; *.cin; *.dv; *.vob; *.dxa; *.ea; *.cdata; *.cpk; *.flx; *.gxf; *.tga; *.cgi; *.mve; *.mp4; *.m4v; *.nut; *.lml; *.mj2; *.mpl; *.mmf; *.mpa; *.mpv; *.m1v; *.divx; *.ts; *.mpj; *.mtv; *.mvi; *.nsv; *.nul; *.oma; *.psp; *.str; *.r3d; *.raw; *.vcl; *.rl2; *.rpl; *.s8; *.sdp; *.shn; *.sol; *.tph; *.seq; *.txd; *.vc1; *.vcd; *.vmd; *.voc; *.wc3; *.wsa; *.wsv; *.xa; *.y4m; *.anm; *.flm;)|*.mts;*.mp2;*.mp3;*.avi;*.fli;*.mkv;*.flv;*.3gp;*.pva;*.amr;*.rmvb;*.ape;*.m2v;*.m2ts;*.mov;*.mxf;*.amv;*.mpeg;*.m4b;*.pss;*.mpa;*.mpv;*.aac;*.avs;*.rec;*.vdr;*.ogg;*.dvr-ms;*.ogm;*.wav;*.ac3;*.svx;*.alac;*.bmp;*.dts;*.flac;*.flc;*.h264;*.ivf;*.drc;*.gsm;*.ogv;*.aff;*.mdc;*.mm;*.m4a;*.mpc;*.mpg;*.ra;*.roq;*.rm;*.shn;*.smk;*.sun;*.tif;*.tta;*.wma;*.wmv;*.3g2;*.4xm;*.aea;*.aif;*.apc;*.asf;*.ssa;*.au;*.vid;*.c93;*.caf;*.crc;*.302;*.cin;*.dv;*.vob;*.dxa;*.ea;*.cdata;*.cpk;*.flx;*.gxf;*.tga;*.cgi;*.mve;*.mp4;*.m4v;*.nut;*.lml;*.mj2;*.mpl;*.mmf;*.mpa;*.mpv;*.m1v;*.divx;*.ts;*.mpj;*.mtv;*.mvi;*.nsv;*.nul;*.oma;*.psp;*.str;*.r3d;*.raw;*.vcl;*.rl2;*.rpl;*.s8;*.sdp;*.shn;*.sol;*.tph;*.seq;*.txd;*.vc1;*.vcd;*.vmd;*.voc;*.wc3;*.wsa;*.wsv;*.xa;*.y4m;*.anm;*.flm;"
                OpenFileDialog1.FileName = ""
                Opendialog = OpenFileDialog1.ShowDialog()
            Else
                FolderDialog = FolderBrowserDialog1.ShowDialog()
            End If
        End If
    End Sub
End Class
