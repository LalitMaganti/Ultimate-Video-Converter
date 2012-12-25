Module ComboBox
    Public Sub ExtentionComboBox(ByVal MainForm As CustomConversionSettings)
        Select Case System.Convert.ToString(MainForm.cboxExtension.SelectedItem)
            Case ".mpeg"
                Generic.Extension = ".mpeg" & """"
                Generic.CheckExtension = ".mpeg"
            Case ".mpg"
                Generic.Extension = ".mpg" & """"
                Generic.CheckExtension = ".mpg"
            Case ".aac"
                Generic.Extension = ".aac" & """"
                Generic.CheckExtension = ".aac"
            Case ".mp4"
                Generic.Extension = ".mp4" & """"
                Generic.CheckExtension = ".mp4"
            Case ".m4a"
                Generic.Extension = ".m4a" & """"
                Generic.CheckExtension = ".m4a"
        End Select
    End Sub
    Public Sub cboxModeSelectionChanged(ByVal MainForm As MainWindow)
        Dim SelectedItem As String = ""
        With MainForm
            .cboxProfiles.Visibility = Windows.Visibility.Visible
            Select Case .cboxMode.SelectedIndex
                Case 0
                    SelectedItem = "MP4 (MPEG4)"
                    .lblMaker.Visibility = Windows.Visibility.Hidden
                    .cboxMaker.Visibility = Windows.Visibility.Hidden
                    .CustomDialog.cboxContainer.Visibility = Windows.Visibility.Visible
                    .lblProfiles.Visibility = Windows.Visibility.Visible
                    .cboxAudioContainer.Visibility = Windows.Visibility.Hidden
                    .cboxDevice.Visibility = Windows.Visibility.Hidden
                    .cboxDeviceProfile.Visibility = Windows.Visibility.Hidden
                    .lblDevice.Visibility = Windows.Visibility.Hidden
                    .lblDeviceProfile.Visibility = Windows.Visibility.Hidden
                    '.tabVideo.IsEnabled = True
                    .disablebox()
                Case 222
                    SelectedItem = "AVI (Xvid)"
                    For i As Integer = 0 To .cboxProfiles.Items.Count
                        If .cboxProfiles.Items.Item(i) = SelectedItem Then
                            .cboxProfiles.SelectedItem = .cboxProfiles.Items.GetItemAt(i)
                            Exit For
                        End If
                    Next
                    SelectedItem = "MP4 (MPEG4)"
                    .cboxMaker.Visibility = Windows.Visibility.Hidden
                    .lblProfiles.Visibility = Windows.Visibility.Visible
                    .CustomDialog.cboxContainer.Visibility = Windows.Visibility.Visible
                    .cboxAudioContainer.Visibility = Windows.Visibility.Hidden
                    .cboxDevice.Visibility = Windows.Visibility.Hidden
                    .cboxDeviceProfile.Visibility = Windows.Visibility.Hidden
                    .lblDevice.Visibility = Windows.Visibility.Hidden
                    .lblDeviceProfile.Visibility = Windows.Visibility.Hidden
                    .lblMaker.Visibility = Windows.Visibility.Hidden
                    '.tabVideo.IsEnabled = True
                Case 3
                    .cboxProfiles.Visibility = Windows.Visibility.Hidden
                    .cboxMaker.Visibility = Windows.Visibility.Hidden
                    .lblProfiles.Visibility = Windows.Visibility.Hidden
                    .CustomDialog.cboxContainer.Visibility = Windows.Visibility.Visible
                    .lblMaker.Visibility = Windows.Visibility.Hidden
                    .cboxAudioContainer.Visibility = Windows.Visibility.Hidden
                    .cboxDevice.Visibility = Windows.Visibility.Hidden
                    .cboxDeviceProfile.Visibility = Windows.Visibility.Hidden
                    .lblDevice.Visibility = Windows.Visibility.Hidden
                    .lblDeviceProfile.Visibility = Windows.Visibility.Hidden
                    '.tabVideo.IsEnabled = True
                    .enablebox()
                Case 1
                    .cboxMaker.SelectedItem = .cboxMaker.Items.GetItemAt(0)
                    .cboxProfiles.Visibility = Windows.Visibility.Hidden
                    .cboxMaker.Visibility = Windows.Visibility.Visible
                    .lblProfiles.Visibility = Windows.Visibility.Hidden
                    .lblMaker.Visibility = Windows.Visibility.Visible
                    .CustomDialog.cboxContainer.Visibility = Windows.Visibility.Visible
                    .cboxAudioContainer.Visibility = Windows.Visibility.Hidden
                    .cboxDevice.Visibility = Windows.Visibility.Visible
                    .cboxDeviceProfile.Visibility = Windows.Visibility.Visible
                    .lblDevice.Visibility = Windows.Visibility.Visible
                    .lblDeviceProfile.Visibility = Windows.Visibility.Visible
                    '.tabVideo.IsEnabled = True
                    .cboxMaker.IsEnabled = True
                Case 2
                    clearcbox(.CustomDialog.cboxExtension)
                    .cboxProfiles.Visibility = Windows.Visibility.Hidden
                    .cboxMaker.Visibility = Windows.Visibility.Hidden
                    .CustomDialog.cboxContainer.Visibility = Windows.Visibility.Hidden
                    '.tabVideo.IsEnabled = False
                    .enablebox()
                    .cboxAudioContainer.Visibility = Windows.Visibility.Visible
                    .cboxDevice.Visibility = Windows.Visibility.Hidden
                    .cboxDeviceProfile.Visibility = Windows.Visibility.Hidden
                    .lblDeviceProfile.Visibility = Windows.Visibility.Hidden
                    .lblDevice.Visibility = Windows.Visibility.Hidden
                    .lblMaker.Visibility = Windows.Visibility.Hidden
                    .lblProfiles.Visibility = Windows.Visibility.Visible
                    For i As Integer = 0 To .cboxAudioContainer.Items.Count
                        If .cboxAudioContainer.Items.Item(i) = "MP3 (MPEG)" Then
                            .cboxAudioContainer.SelectedItem = Nothing
                            .cboxAudioContainer.SelectedItem = .cboxAudioContainer.Items.GetItemAt(i)
                            Exit For
                        End If
                    Next
            End Select
            For i As Integer = 0 To .cboxProfiles.Items.Count - 1
                If .cboxProfiles.Items.Item(i) = SelectedItem Then
                    .cboxProfiles.SelectedItem = Nothing
                    .cboxProfiles.SelectedItem = .cboxProfiles.Items.GetItemAt(i)
                    Exit For
                End If
            Next
        End With
    End Sub
    Public Sub cboxProfilesSelectionChanged(ByVal MainForm As MainWindow)
        With MainForm
            Select Case System.Convert.ToString(.cboxProfiles.SelectedItem)
                Case "MP4 (MPEG4)"
                    .CustomDialog.cboxContainer.SelectedItem = "MP4"
                Case "AVI (Xvid)"
                    .CustomDialog.cboxContainer.SelectedItem = "AVI"
                Case "OGG"
                    .CustomDialog.cboxContainer.SelectedItem = "OGG"
                Case "ASF"
                    .CustomDialog.cboxContainer.SelectedItem = "ASF"
                Case "WMV"
                    .CustomDialog.cboxContainer.SelectedItem = "WMV"
                Case "AVI (Microsoft MPEG 4 v3)"
                    .CustomDialog.cboxContainer.SelectedItem = "AVI"
                    .CustomDialog.cboxVideoCodec.SelectedItem = "Microsoft MPEG 4 v3"
                Case "FLV"
                    .CustomDialog.cboxContainer.SelectedItem = "FLV"
                Case "MP4 (H.264)"
                    .CustomDialog.cboxContainer.SelectedItem = "MP4"
                    .CustomDialog.cboxVideoCodec.SelectedItem = "H.264"
                Case "MKV"
                    .CustomDialog.cboxContainer.SelectedItem = "MKV"
            End Select
            If .cboxMode.SelectedIndex = 0 Then
                .CustomDialog.cboxSize.SelectedItem = "Default (Same as source)"
                .CustomDialog.cboxVideoBitrate.Text = 1000
                .CustomDialog.cboxAudioBitrate.SelectedItem = "256"
            ElseIf .cboxMode.SelectedItem.ToString = "HD Mode" Then
                .CustomDialog.cboxSize.SelectedItem = "HD 1080P"
                .CustomDialog.cboxVideoBitrate.Text = 10000
                .CustomDialog.cboxAudioBitrate.SelectedItem = "320"
            End If
            .CustomDialog.cboxFrameRate.SelectedItem = "29.97 (NTSC)"
            .CustomDialog.cboxSampleRate.SelectedItem = "44100"
            .disablebox()
        End With
    End Sub
    Public Sub cboxAudioContainerSelectionChanged(ByVal MainForm As MainWindow)
        With MainForm
            Select Case System.Convert.ToString(.cboxAudioContainer.SelectedItem)
                Case "MP3 (MPEG)"
                    clearcbox(.CustomDialog.cboxAudioCodec)
                    addcbox(.CustomDialog.cboxAudioCodec, "MP3")
                    clearcbox(.CustomDialog.cboxExtension)
                    addcbox(.CustomDialog.cboxExtension, ".mp3")
                    .CustomDialog.cboxExtension.SelectedItem = ".mp3"
                    .CustomDialog.cboxAudioCodec.SelectedItem = "MP3"
                    Generic.Extension = ".mp3" & """"
                    Generic.CheckExtension = ".mp3"
                Case "MP2 (MPEG)"
                    clearcbox(.CustomDialog.cboxAudioCodec)
                    addcbox(.CustomDialog.cboxAudioCodec, "MP2")
                    clearcbox(.CustomDialog.cboxExtension)
                    addcbox(.CustomDialog.cboxExtension, ".mp2")
                    .CustomDialog.cboxExtension.SelectedItem = ".mp2"
                    .CustomDialog.cboxAudioCodec.SelectedItem = "MP2"
                    Generic.Extension = ".mp2" & """"
                    Generic.CheckExtension = ".mp2"
                Case "AAC (MP4)"
                    clearcbox(.CustomDialog.cboxAudioCodec)
                    addcbox(.CustomDialog.cboxAudioCodec, "AAC")
                    clearcbox(.CustomDialog.cboxExtension)
                    addcbox(.CustomDialog.cboxExtension, ".mp4")
                    addcbox(.CustomDialog.cboxExtension, ".aac")
                    addcbox(.CustomDialog.cboxExtension, ".m4a")
                    .CustomDialog.cboxExtension.SelectedItem = ".m4a"
                    .CustomDialog.cboxAudioCodec.SelectedItem = "AAC"
                    Generic.Extension = ".m4a" & """"
                    Generic.CheckExtension = ".m4a"
                Case "AC3 (Dolby D)"
                    clearcbox(.CustomDialog.cboxAudioCodec)
                    addcbox(.CustomDialog.cboxAudioCodec, "AC3")
                    clearcbox(.CustomDialog.cboxExtension)
                    addcbox(.CustomDialog.cboxExtension, ".ac3")
                    .CustomDialog.cboxExtension.SelectedItem = ".ac3"
                    .CustomDialog.cboxAudioCodec.SelectedItem = "AC3"
                    Generic.Extension = ".ac3" & """"
                    Generic.CheckExtension = ".ac3"
                Case "Vorbis (OGG)"
                    clearcbox(.CustomDialog.cboxAudioCodec)
                    addcbox(.CustomDialog.cboxAudioCodec, "Vorbis")
                    clearcbox(.CustomDialog.cboxExtension)
                    addcbox(.CustomDialog.cboxExtension, ".ogg")
                    .CustomDialog.cboxExtension.SelectedItem = ".ogg"
                    .CustomDialog.cboxAudioCodec.SelectedItem = "Vorbis"
                    Generic.Extension = ".ogg" & """"
                    Generic.CheckExtension = ".ogg"
                Case "AIFF"
                    clearcbox(.CustomDialog.cboxAudioCodec)
                    addcbox(.CustomDialog.cboxAudioCodec, "24-bit PCM BE")
                    clearcbox(.CustomDialog.cboxExtension)
                    addcbox(.CustomDialog.cboxExtension, ".aiff")
                    .CustomDialog.cboxExtension.SelectedItem = ".aiff"
                    .CustomDialog.cboxAudioCodec.SelectedItem = "24-bit PCM BE"
                    Generic.Extension = ".aiff" & """"
                    Generic.CheckExtension = ".aiff"
                Case "WAV"
                    clearcbox(.CustomDialog.cboxAudioCodec)
                    addcbox(.CustomDialog.cboxAudioCodec, "16-bit PCM LE")
                    clearcbox(.CustomDialog.cboxExtension)
                    addcbox(.CustomDialog.cboxExtension, ".wav")
                    .CustomDialog.cboxExtension.SelectedItem = ".wav"
                    .CustomDialog.cboxAudioCodec.SelectedItem = "16-bit PCM LE"
                    Generic.Extension = ".wav" & """"
                    Generic.CheckExtension = ".wav"
                Case "WMA"
                    clearcbox(.CustomDialog.cboxAudioCodec)
                    addcbox(.CustomDialog.cboxAudioCodec, "WM Audio 1")
                    addcbox(.CustomDialog.cboxAudioCodec, "WM Audio 2")
                    clearcbox(.CustomDialog.cboxExtension)
                    addcbox(.CustomDialog.cboxExtension, ".wma")
                    .CustomDialog.cboxExtension.SelectedItem = ".wma"
                    .CustomDialog.cboxAudioCodec.SelectedItem = "WM Audio 2"
                    Generic.Extension = ".wma" & """"
                    Generic.CheckExtension = ".wma"
                Case "FLAC"
                    clearcbox(.CustomDialog.cboxAudioCodec)
                    addcbox(.CustomDialog.cboxAudioCodec, "FLAC")
                    clearcbox(.CustomDialog.cboxExtension)
                    addcbox(.CustomDialog.cboxExtension, ".flac")
                    .CustomDialog.cboxExtension.SelectedItem = ".flac"
                    .CustomDialog.cboxAudioCodec.SelectedItem = "FLAC"
                    Generic.Extension = ".flac" & """"
                    Generic.CheckExtension = ".flac"
                Case "MKA"
                    clearcbox(.CustomDialog.cboxAudioCodec)
                    addcbox(.CustomDialog.cboxAudioCodec, "AAC")
                    clearcbox(.CustomDialog.cboxExtension)
                    addcbox(.CustomDialog.cboxExtension, ".mka")
                    .CustomDialog.cboxExtension.SelectedItem = ".mka"
                    .CustomDialog.cboxAudioCodec.SelectedItem = "AAC"
                    Generic.Extension = ".mka" & """"
                    Generic.CheckExtension = ".mka"
            End Select
            Generic.VideoCodec = " -vn"
        End With
    End Sub
End Module
