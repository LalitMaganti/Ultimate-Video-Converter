Public Class CustomConversionSettings
    Public Sub ExtentionBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboxExtension.SelectionChanged
        ExtentionComboBox(Me)
    End Sub
    Private Sub cboxSize_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles cboxSize.SelectionChanged
        If cboxSize.SelectedItem <> Nothing Then
            txtWidth.IsEnabled = False
            txtHeight.IsEnabled = False
            Select Case cboxSize.SelectedItem.ToString
                Case "128x96"
                    sizechange(128, 96)
                Case "176x144"
                    sizechange(176, 144)
                Case "352x288"
                    sizechange(352, 288)
                Case "704x576"
                    sizechange(704, 576)
                Case "1408x1152"
                    sizechange(1408, 1152)
                Case "Default (Same as source)"
                    sizevar = ""
                    txtWidth.Text = "Default"
                    txtHeight.Text = "Default"
                Case "VGA"
                    sizechange(640, 480)
                Case "SVGA"
                    sizechange(800, 600)
                Case "HD 480P"
                    sizechange(852, 480)
                Case "HD 720P"
                    sizechange(1280, 720)
                Case "HD 1080P"
                    sizechange(1920, 1080)
                Case "Custom"
                    txtWidth.IsEnabled = True
                    txtHeight.IsEnabled = True
                    sizechange(640, 480)
            End Select
        End If
    End Sub
    Public Sub sizechange(ByVal width As Integer, ByVal height As Integer)
        txtWidth.Text = width
        txtHeight.Text = height
    End Sub
    Public Sub Audio_selecteditem(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboxAudioCodec.SelectionChanged
        If cboxAudioCodec.SelectedItem <> Nothing Then
            Select Case cboxAudioCodec.SelectedItem.ToString
                Case "AAC"
                    Generic.AudioCodec = " -acodec aac -strict experimental"
                Case "MP3"
                    Generic.AudioCodec = " -acodec libmp3lame"
                Case "AC3"
                    Generic.AudioCodec = " -acodec ac3"
                Case "WM Audio 1"
                    Generic.AudioCodec = " -acodec wmav1"
                Case "WM Audio 2"
                    Generic.AudioCodec = " -acodec wmav2"
                Case "FLAC"
                    Generic.AudioCodec = " -acodec flac"
                Case "MP2"
                    Generic.AudioCodec = " -acodec mp2"
                Case "16-bit PCM LE"
                    Generic.AudioCodec = " -acodec pcm_s16le"
                Case "24-bit PCM BE"
                    Generic.AudioCodec = " -acodec pcm_s24be"
                Case "Vorbis"
                    Generic.AudioCodec = " -acodec libvorbis"
                Case "AMR-NB"
                    Generic.AudioCodec = " -acodec libopencore_amrnb"
            End Select
            cboxAudioBitrate.Items.Clear()
            If cboxAudioCodec.SelectedItem.ToString = "AMR-NB" Then
                cboxSampleRate.SelectedItem = cboxSampleRate.Items.GetItemAt(0)
                cboxSampleRate.IsEnabled = False
                cboxChannels.SelectedItem = cboxChannels.Items.GetItemAt(0)
                cboxChannels.IsEnabled = False
                addcbox(cboxAudioBitrate, "4.75")
                addcbox(cboxAudioBitrate, "5.15")
                addcbox(cboxAudioBitrate, "5.9")
                addcbox(cboxAudioBitrate, "6.7")
                addcbox(cboxAudioBitrate, "7.4")
                addcbox(cboxAudioBitrate, "7.95")
                addcbox(cboxAudioBitrate, "10.2")
                addcbox(cboxAudioBitrate, "12.2")
                cboxAudioBitrate.SelectedItem = cboxAudioBitrate.Items.GetItemAt(7)
            Else
                cboxSampleRate.SelectedItem = cboxSampleRate.Items.GetItemAt(3)
                cboxSampleRate.IsEnabled = True
                cboxChannels.SelectedItem = cboxChannels.Items.GetItemAt(1)
                cboxChannels.IsEnabled = True
                addcbox(cboxAudioBitrate, "32")
                addcbox(cboxAudioBitrate, "64")
                addcbox(cboxAudioBitrate, "96")
                addcbox(cboxAudioBitrate, "128")
                addcbox(cboxAudioBitrate, "160")
                addcbox(cboxAudioBitrate, "256")
                addcbox(cboxAudioBitrate, "320")
                addcbox(cboxAudioBitrate, "512")
                addcbox(cboxAudioBitrate, "1024")
                cboxAudioBitrate.SelectedItem = cboxAudioBitrate.Items.GetItemAt(5)
            End If
        End If
    End Sub
    Public Sub Channels_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboxChannels.SelectionChanged
        Select Case cboxChannels.SelectedItem.Content
            Case "1 (Mono)"
                channels = " -ac 1"
            Case "2 (Stereo)"
                channels = " -ac 2"
        End Select
    End Sub
    Public Sub FrameBox_index(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboxFrameRate.SelectionChanged
        If cboxFrameRate.SelectedItem <> Nothing Then
            If cboxFrameRate.SelectedItem = "23.97" Then
                framerate = " -r " & cboxFrameRate.SelectedItem
            Else
                If cboxFrameRate.SelectedItem.Length > 2 Then
                    If cboxFrameRate.SelectedItem.contains("25") Then
                        Dim k = cboxFrameRate.SelectedItem.Remove(3)
                        framerate = " -r " & k
                    Else
                        Dim k = cboxFrameRate.SelectedItem.Remove(5)
                        framerate = " -r " & k
                    End If
                Else
                    framerate = " -r " & cboxFrameRate.SelectedItem
                End If
            End If
        End If
    End Sub
    Public Sub VideoCodec_sI(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboxVideoCodec.SelectionChanged
        If cboxVideoCodec.SelectedItem <> Nothing Then
            clearcbox(cboxAudioCodec)
            Select Case cboxVideoCodec.SelectedItem.ToString
                Case "MPEG 4 (Part 2)"
                    addcbox(cboxAudioCodec, "AAC")
                    addcbox(cboxAudioCodec, "MP3")
                    addcbox(cboxAudioCodec, "AC3")
                    Generic.VideoCodec = " -vcodec mpeg4"
                Case "H.264"
                    Generic.VideoCodec = " -vcodec libx264"
                    addcbox(cboxAudioCodec, "AAC")
                    addcbox(cboxAudioCodec, "MP3")
                    addcbox(cboxAudioCodec, "AC3")
                    addcbox(cboxAudioCodec, "MP2")
                Case "H.263"
                    Generic.VideoCodec = " -vcodec h263"
                    addcbox(cboxAudioCodec, "AMR-NB")
                Case "Xvid"
                    Generic.VideoCodec = " -vcodec libxvid"
                    addcbox(cboxAudioCodec, "MP3")
                    addcbox(cboxAudioCodec, "AC3")
                    addcbox(cboxAudioCodec, "AAC")
                Case "WM Video 7"
                    Generic.VideoCodec = " -vcodec wmv1"
                    addcbox(cboxAudioCodec, "WM Audio 1")
                    addcbox(cboxAudioCodec, "WM Audio 2")
                Case "WM Video 8"
                    Generic.VideoCodec = " -vcodec wmv2"
                    addcbox(cboxAudioCodec, "WM Audio 2")
                    addcbox(cboxAudioCodec, "WM Audio 1")
                Case "Theora"
                    Generic.VideoCodec = " -vcodec libtheora"
                    addcbox(cboxAudioCodec, "FLAC")
                Case "MPEG 1"
                    Generic.VideoCodec = " -vcodec mpeg1video"
                    addcbox(cboxAudioCodec, "MP3")
                Case "MPEG 2"
                    Generic.VideoCodec = " -vcodec mpeg2video"
                    addcbox(cboxAudioCodec, "MP3")
                    addcbox(cboxAudioCodec, "MP2")
                    addcbox(cboxAudioCodec, "AC3")
                Case "Microsoft MPEG 4 v3"
                    Generic.VideoCodec = " -vcodec msmpeg4"
                    addcbox(cboxAudioCodec, "MP3")
                    addcbox(cboxAudioCodec, "AC3")
                Case "Microsoft MPEG 4 v2"
                    Generic.VideoCodec = " -vcodec msmpeg4v2"
                    addcbox(cboxAudioCodec, "MP3")
                    addcbox(cboxAudioCodec, "AC3")
                Case "MJPEG"
                    Generic.VideoCodec = " -vcodec mjpeg"
                    addcbox(cboxAudioCodec, "16-bit PCM LE")
                Case "Flash Video"
                    Generic.VideoCodec = " -vcodec flv"
                    addcbox(cboxAudioCodec, "MP3")
                    addcbox(cboxAudioCodec, "AAC")
            End Select
            cboxAudioCodec.SelectedItem = cboxAudioCodec.Items.GetItemAt(0)
            If cboxVideoCodec.SelectedItem.ToString = "H.263" Then
                cboxSize.ItemsSource = cboxAlternativeSizePopulate()
                cboxSize.SelectedItem = "128x96"
            Else
                cboxSize.ItemsSource = cboxSizePopulate()
                cboxSize.SelectedItem = "Default (Same as source)"
            End If
        End If
    End Sub
    Public Sub ContainerBox_sI(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboxContainer.SelectionChanged
        clearcbox(cboxExtension)
        cboxVideoCodec.SelectedItem = Nothing
        clearcbox(cboxVideoCodec)
        Select Case System.Convert.ToString(cboxContainer.SelectedItem)
            Case "MP4"
                Generic.CheckExtension = ".mp4"
                addcbox(cboxVideoCodec, "MPEG 4 (Part 2)")
                addcbox(cboxVideoCodec, "H.264")
            Case "MOV"
                Generic.CheckExtension = ".mov"
                addcbox(cboxVideoCodec, "Xvid")
                addcbox(cboxVideoCodec, "MJPEG")
                addcbox(cboxVideoCodec, "H.264")
            Case "AVI"
                Generic.CheckExtension = ".avi"
                addcbox(cboxVideoCodec, "Xvid")
                addcbox(cboxVideoCodec, "MJPEG")
                addcbox(cboxVideoCodec, "Microsoft MPEG 4 v3")
                addcbox(cboxVideoCodec, "Microsoft MPEG 4 v2")
                addcbox(cboxVideoCodec, "H.264")
            Case "WMV"
                Generic.CheckExtension = ".wmv"
                addcbox(cboxVideoCodec, "WM Video 8")
                addcbox(cboxVideoCodec, "WM Video 7")
            Case "OGG"
                Generic.CheckExtension = ".ogg"
                addcbox(cboxVideoCodec, "Theora")
            Case "MPEG"
                Generic.CheckExtension = ".mpeg"
                addcbox(cboxExtension, ".mpg")
                addcbox(cboxVideoCodec, "MPEG 2")
                addcbox(cboxVideoCodec, "MPEG 1")
            Case "ASF"
                Generic.CheckExtension = ".asf"
                addcbox(cboxVideoCodec, "WM Video 8")
                addcbox(cboxVideoCodec, "WM Video 7")
            Case "FLV"
                Generic.CheckExtension = ".flv"
                addcbox(cboxVideoCodec, "Flash Video")
            Case "MKV"
                Generic.CheckExtension = ".mkv"
                addcbox(cboxVideoCodec, "H.264")
                addcbox(cboxVideoCodec, "Xvid")
            Case "3GP"
                Generic.CheckExtension = ".3gp"
                addcbox(cboxVideoCodec, "H.263")
                addcbox(cboxVideoCodec, "MPEG 4 (Part 2)")
        End Select
        cboxVideoCodec.SelectedItem = cboxVideoCodec.Items.GetItemAt(0)
        addcbox(cboxExtension, Generic.CheckExtension)
        Generic.Extension = Generic.CheckExtension & """"
        For i As Integer = 0 To cboxExtension.Items.Count - 1
            If cboxExtension.Items.Item(i) = Generic.CheckExtension Then
                cboxExtension.SelectedItem = cboxExtension.Items.GetItemAt(i)
                Exit For
            End If
        Next
    End Sub
    Private Sub hypClose_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles hypClose.Click
        Me.Close()
    End Sub
End Class
