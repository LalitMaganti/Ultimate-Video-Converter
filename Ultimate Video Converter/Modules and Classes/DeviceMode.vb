Module DeviceMode
    Public Sub cboxDeviceSelectionChanged(ByVal MainForm As MainWindow)
        With MainForm
            If .cboxDevice.SelectedItem <> Nothing Then
                .CustomDialog.cboxSize.SelectedItem = "Custom"
                clearcbox(.cboxDeviceProfile)
                Select Case .cboxMaker.SelectedItem.ToString
                    Case "Apple"
                        addcbox(.cboxDeviceProfile, "MP4")
                        addcbox(.cboxDeviceProfile, "H.264")
                        addcbox(.cboxDeviceProfile, "MOV")
                        Select Case .cboxDevice.SelectedItem.ToString
                            Case "iPhone 4", "iPod Touch 4th Gen"
                                addcbox(.cboxDeviceProfile, "MJPEG")
                            Case "iPad", "Apple TV"
                                addcbox(.cboxDeviceProfile, "H.264 HD")
                        End Select
                        .cboxDeviceProfile.SelectedItem = "MP4"
                End Select
                Select Case .cboxDevice.SelectedItem.ToString
                    Case "Sansa"
                        addcbox(.cboxDeviceProfile, "e200")
                        addcbox(.cboxDeviceProfile, "e200 v2")
                        addcbox(.cboxDeviceProfile, "c200")
                        addcbox(.cboxDeviceProfile, "Fuze v1")
                        addcbox(.cboxDeviceProfile, "Fuze v2")
                        .cboxDeviceProfile.SelectedItem = "e200"
                    Case "iAudio"
                        addcbox(.cboxDeviceProfile, "X5")
                        addcbox(.cboxDeviceProfile, "M5")
                        .cboxDevice.SelectedItem = "M5"
                    Case "iRiver"
                        addcbox(.cboxDeviceProfile, "H300")
                        addcbox(.cboxDeviceProfile, "H10 20GB")
                        addcbox(.cboxDeviceProfile, "H120")
                        addcbox(.cboxDeviceProfile, "H10 5/6GB")
                        .cboxDeviceProfile.SelectedItem = "H300"
                    Case "Gigabeat"
                        addcbox(.cboxDeviceProfile, "Gigabeat")
                        .cboxDeviceProfile.SelectedItem = "Gigabeat"
                    Case "iPod"
                        addcbox(.cboxDeviceProfile, "Colour")
                        addcbox(.cboxDeviceProfile, "Photo")
                        addcbox(.cboxDeviceProfile, "Nano")
                        addcbox(.cboxDeviceProfile, "Mini")
                        addcbox(.cboxDeviceProfile, "Video")
                        .cboxDeviceProfile.SelectedItem = "Colour"
                    Case "Wii"
                        addcbox(.cboxDeviceProfile, "AVI")
                        addcbox(.cboxDeviceProfile, "MOV")
                        .cboxDeviceProfile.SelectedItem = "AVI"
                    Case "PSP"
                        addcbox(.cboxDeviceProfile, "MP4")
                        .cboxDeviceProfile.SelectedItem = "MP4"
                    Case "PS3"
                        addcbox(.cboxDeviceProfile, "MPEG")
                        addcbox(.cboxDeviceProfile, "MPEG HD")
                        addcbox(.cboxDeviceProfile, "WMV")
                        addcbox(.cboxDeviceProfile, "WMV HD")
                        addcbox(.cboxDeviceProfile, "Xvid")
                        addcbox(.cboxDeviceProfile, "Xvid HD")
                        .cboxDeviceProfile.SelectedItem = "MPEG"
                    Case "Storm"
                        addcbox(.cboxDeviceProfile, "H.264")
                        .cboxDeviceProfile.SelectedItem = "H.264"
                End Select
                .disablebox()
            End If
        End With
    End Sub
    Public Sub cboxDeviceProfileSelectionChanged(ByVal MainForm As MainWindow)
        With MainForm
            If .cboxDeviceProfile.SelectedItem <> Nothing Then
                .CustomDialog.cboxFrameRate.SelectedItem = "29.97 (NTSC)"
                .CustomDialog.cboxSampleRate.SelectedItem = "44100"
                .CustomDialog.cboxChannels.SelectedItem = "2 (Stereo)"
                .CustomDialog.cboxSize.SelectedItem = "Custom"
                Select Case .cboxMaker.SelectedItem.ToString
                    Case "Apple"
                        Select Case .cboxDeviceProfile.SelectedItem.ToString
                            Case "H.264"
                                .CustomDialog.cboxContainer.SelectedItem = "MP4"
                                .CustomDialog.cboxVideoCodec.SelectedItem = "H.264"
                                .CustomDialog.cboxSampleRate.SelectedItem = "48000"
                                .CustomDialog.cboxAudioBitrate.SelectedItem = "160"
                                Select Case .cboxDevice.SelectedItem.ToString
                                    Case "iPhone Original, 3G and 3GS", "iPod Touch 1st, 2nd and 3rd Gen"
                                        .CustomDialog.cboxVideoBitrate.Text = 1500
                                        .CustomDialog.sizechange(320, 240)
                                    Case "iPhone 4", "iPod Touch 4th Gen"
                                        .CustomDialog.cboxVideoBitrate.Text = 2500
                                        .CustomDialog.cboxSize.SelectedItem = "HD 720P"
                                    Case "iPad"
                                        .CustomDialog.cboxVideoBitrate.Text = 2000
                                        .CustomDialog.sizechange(720, 576)
                                    Case "Apple TV"
                                        .CustomDialog.sizechange(720, 480)
                                        .CustomDialog.cboxVideoBitrate.Text = 2000
                                    Case "iPod Classic/Video"
                                        .CustomDialog.cboxVideoBitrate.Text = 512
                                        .CustomDialog.sizechange(320, 240)
                                End Select
                            Case "H.264 HD"
                                .CustomDialog.cboxContainer.SelectedItem = "MP4"
                                .CustomDialog.cboxVideoCodec.SelectedItem = "H.264"
                                .CustomDialog.cboxSampleRate.SelectedItem = "48000"
                                .CustomDialog.cboxAudioBitrate.SelectedItem = "160"
                                .CustomDialog.cboxSize.SelectedItem = "HD 720P"
                                .CustomDialog.cboxVideoBitrate.Text = 3500
                            Case "MOV"
                                .CustomDialog.cboxContainer.SelectedItem = "MOV"
                                .CustomDialog.cboxVideoCodec.SelectedItem = "H.264"
                                .CustomDialog.cboxSampleRate.SelectedItem = "48000"
                                .CustomDialog.cboxAudioBitrate.SelectedItem = "160"
                                Select Case .cboxDevice.SelectedItem.ToString
                                    Case "iPhone Original, 3G and 3GS", "iPod Touch 1st, 2nd and 3rd Gen"
                                        .CustomDialog.cboxVideoBitrate.Text = 1500
                                        .CustomDialog.sizechange(320, 240)
                                    Case "iPhone 4", "iPod Touch 4th Gen"
                                        .CustomDialog.cboxVideoBitrate.Text = 2500
                                        .CustomDialog.cboxSize.SelectedItem = "HD 720P"
                                    Case "iPad"
                                        .CustomDialog.cboxVideoBitrate.Text = 2000
                                        .CustomDialog.sizechange(720, 576)
                                    Case "Apple TV"
                                        .CustomDialog.sizechange(720, 480)
                                        .CustomDialog.cboxVideoBitrate.Text = 2000
                                    Case "iPod Classic/Video"
                                        .CustomDialog.cboxVideoBitrate.Text = 512
                                        .CustomDialog.sizechange(320, 240)
                                End Select
                            Case "MP4"
                                .CustomDialog.cboxContainer.SelectedItem = "MP4"
                                .CustomDialog.cboxVideoBitrate.Text = 2500
                                .CustomDialog.cboxSampleRate.SelectedItem = "48000"
                                .CustomDialog.cboxAudioBitrate.SelectedItem = "160"
                                Select Case .cboxDevice.SelectedItem.ToString
                                    Case "iPhone Original, 3G and 3GS MP4", "iPod Touch 1st, 2nd and 3rd Gen MP4"
                                        .CustomDialog.sizechange(320, 240)
                                    Case "iPhone 4 MP4", "iPod Touch 4th Gen MP4"
                                        .CustomDialog.sizechange(640, 480)
                                    Case "iPad"
                                        .CustomDialog.cboxSize.SelectedItem = "VGA"
                                        .CustomDialog.cboxVideoBitrate.Text = 1800
                                    Case "iPod Classic/Video"
                                        .CustomDialog.cboxVideoBitrate.Text = 768
                                        .CustomDialog.sizechange(320, 240)
                                    Case "Apple TV"
                                        .CustomDialog.cboxVideoBitrate.Text = 2000
                                        .CustomDialog.sizechange(720, 432)
                                End Select
                            Case "MJPEG"
                                .CustomDialog.cboxContainer.SelectedItem = "AVI"
                                .CustomDialog.cboxVideoCodec.SelectedItem = "MJPEG"
                                .CustomDialog.cboxSampleRate.SelectedItem = "48000"
                                .CustomDialog.cboxAudioBitrate.SelectedItem = "160"
                                .CustomDialog.cboxVideoBitrate.Text = 35000
                                .CustomDialog.cboxSize.SelectedItem = "HD 720P"
                        End Select
                    Case "RockBox"
                        .CustomDialog.cboxContainer.SelectedItem = "MPEG"
                        .CustomDialog.cboxVideoBitrate.Text = 200
                        .CustomDialog.cboxAudioBitrate.SelectedItem = "256"
                        Select Case .cboxDeviceProfile.SelectedItem.ToString
                            Case "e200", "Color", "Photo", "H300", "e200 v2", "Fuze v1", "Fuze v2"
                                .CustomDialog.sizechange(220, 176)
                            Case "X5", "M5", "H10 20GB", "H120"
                                .CustomDialog.sizechange(160, 128)
                            Case "Nano"
                                .CustomDialog.sizechange(176, 128)
                            Case "Video", "Gigabeat"
                                .CustomDialog.sizechange(320, 240)
                            Case "Mini"
                                .CustomDialog.sizechange(138, 104)
                            Case "H10 5/6GB"
                                .CustomDialog.sizechange(128, 128)
                            Case "c200"
                                .CustomDialog.sizechange(132, 80)
                        End Select
                    Case "Nintendo"
                        Select Case .cboxDevice.SelectedItem.ToString
                            Case "Wii"
                                .CustomDialog.cboxVideoBitrate.Text = 4000
                                .CustomDialog.cboxAudioBitrate.SelectedItem = "256"
                                .CustomDialog.sizechange(720, 480)
                                .CustomDialog.cboxVideoCodec.SelectedItem = "MJPEG"
                                Select Case .cboxDeviceProfile.SelectedItem.ToString
                                    Case "AVI"
                                        .CustomDialog.cboxContainer.SelectedItem = "AVI"
                                    Case "MOV"
                                        .CustomDialog.cboxContainer.SelectedItem = "MOV"
                                End Select
                        End Select
                    Case "Blackberry"
                        .CustomDialog.cboxContainer.SelectedItem = "MP4"
                        .CustomDialog.cboxFrameRate.SelectedItem = "15"
                        .CustomDialog.cboxAudioBitrate.SelectedItem = "96"
                        .CustomDialog.sizechange(480, 360)
                        Select Case .cboxDevice.SelectedItem.ToString
                            Case "Storm"
                                .CustomDialog.cboxVideoBitrate.Text = 384
                                .CustomDialog.cboxVideoCodec.SelectedItem = "H.264"
                            Case "Bold 9000"
                                .CustomDialog.cboxVideoBitrate.Text = 512
                                .CustomDialog.cboxVideoCodec.SelectedItem = "Xvid"
                        End Select
                    Case "Microsoft"
                        .CustomDialog.cboxAudioBitrate.SelectedItem = "128"
                        Select Case .cboxDeviceProfile.SelectedItem.ToString
                            Case "WMV"
                                .CustomDialog.cboxContainer.SelectedItem = "WMV"
                                .CustomDialog.cboxVideoBitrate.Text = 2000
                                .CustomDialog.sizechange(720, 480)
                            Case "WMV HD"
                                .CustomDialog.cboxContainer.SelectedItem = "WMV"
                                .CustomDialog.cboxVideoBitrate.Text = 4000
                                .CustomDialog.cboxSize.SelectedItem = "HD 720P"
                            Case "MOV"
                                .CustomDialog.cboxContainer.SelectedItem = "MOV"
                                .CustomDialog.cboxVideoCodec.SelectedItem = "H.264"
                                .CustomDialog.cboxVideoBitrate.Text = 2000
                                .CustomDialog.sizechange(720, 480)
                            Case "MOV HD"
                                .CustomDialog.cboxContainer.SelectedItem = "MOV"
                                .CustomDialog.cboxVideoCodec.SelectedItem = "H.264"
                                .CustomDialog.cboxVideoBitrate.Text = 4000
                                .CustomDialog.cboxSize.SelectedItem = "HD 720P"
                        End Select
                    Case "Sony"
                        Select Case .cboxDevice.SelectedItem.ToString
                            Case "PS3"
                                Select Case .cboxDeviceProfile.SelectedItem.ToString
                                    Case "MPEG"
                                        .CustomDialog.cboxContainer.SelectedItem = "MPEG"
                                        .CustomDialog.sizechange(720, 480)
                                        .CustomDialog.cboxAudioCodec.SelectedItem = "MP2"
                                        .CustomDialog.cboxVideoBitrate.Text = 3000
                                    Case "MPEG HD"
                                        .CustomDialog.cboxContainer.SelectedItem = "MPEG"
                                        .CustomDialog.cboxSize.SelectedItem = "HD 720P"
                                        .CustomDialog.cboxAudioCodec.SelectedItem = "MP2"
                                        .CustomDialog.cboxVideoBitrate.Text = 6000
                                    Case "WMV"
                                        .CustomDialog.cboxContainer.SelectedItem = "WMV"
                                        .CustomDialog.sizechange(720, 480)
                                        .CustomDialog.cboxVideoBitrate.Text = 2000
                                    Case "WMV HD"
                                        .CustomDialog.cboxContainer.SelectedItem = "WMV"
                                        .CustomDialog.cboxSize.SelectedItem = "HD 720P"
                                        .CustomDialog.cboxVideoBitrate.Text = 4000
                                    Case "Xvid"
                                        .CustomDialog.cboxContainer.SelectedItem = "AVI"
                                        .CustomDialog.sizechange(720, 480)
                                        .CustomDialog.cboxVideoBitrate.Text = 2500
                                    Case "Xvid HD"
                                        .CustomDialog.cboxContainer.SelectedItem = "AVI"
                                        .CustomDialog.cboxSize.SelectedItem = "HD 720P"
                                        .CustomDialog.cboxVideoBitrate.Text = 5000
                                End Select
                            Case "PSP"
                                .CustomDialog.cboxContainer.SelectedItem = "MP4"
                                .CustomDialog.sizechange(368, 208)
                                .CustomDialog.cboxVideoBitrate.Text = 640
                        End Select
                End Select
            End If
        End With
    End Sub
    Public Sub cboxMakerSelectionChanged(ByVal MainForm As MainWindow)
        With MainForm
            clearcbox(.cboxDevice)
            Try
                Select Case .cboxMaker.SelectedItem.ToString
                    Case "Apple"
                        addcbox(.cboxDevice, "iPhone Original, 3G and 3GS")
                        addcbox(.cboxDevice, "iPhone 4")
                        addcbox(.cboxDevice, "iPod Touch 1st, 2nd and 3rd Gen")
                        addcbox(.cboxDevice, "iPod Touch 4th Gen")
                        addcbox(.cboxDevice, "iPad")
                        addcbox(.cboxDevice, "iPod Classic/Video")
                        addcbox(.cboxDevice, "Apple TV")
                        .cboxDevice.SelectedItem = "iPhone 4"
                    Case "Blackberry"
                        addcbox(.cboxDevice, "Storm")
                        addcbox(.cboxDevice, "Bold 9000")
                        .cboxDevice.SelectedItem = "Storm"
                    Case "RockBox"
                        addcbox(.cboxDevice, "iPod")
                        addcbox(.cboxDevice, "iAudio")
                        addcbox(.cboxDevice, "iRiver")
                        addcbox(.cboxDevice, "Sansa")
                        .cboxDevice.SelectedItem = "Sansa"
                    Case "Nintendo"
                        addcbox(.cboxDevice, "Wii")
                        .cboxDevice.SelectedItem = "Wii"
                    Case "Sony"
                        addcbox(.cboxDevice, "PSP")
                        addcbox(.cboxDevice, "PS3")
                        .cboxDevice.SelectedItem = "PSP"
                End Select
            Catch ex As Exception
            End Try
            If .cboxMaker.SelectedItem.ToString.Contains("Sony") Or .cboxMaker.SelectedItem.ToString.Contains("Apple") Then
                .CustomDialog.cboxContainer.SelectedItem = "MP4"
                .CustomDialog.cboxFrameRate.SelectedItem = "29.97 (NTSC)"
                .CustomDialog.cboxChannels.SelectedItem = "2 (Stereo)"
                .CustomDialog.cboxAudioBitrate.SelectedItem = "128"
                .CustomDialog.cboxSampleRate.SelectedItem = "44100"
            End If
        End With
    End Sub
End Module
