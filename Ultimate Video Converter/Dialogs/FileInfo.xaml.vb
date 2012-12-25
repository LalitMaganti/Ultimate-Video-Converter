Public Class InputInfoForm
    Private Sub InputInfoForm_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Dim MI As New MediaInfo
        MI.Open(ListOfFiles.Item(ListOfFiles.IndexOf(ListOfSelectedFiles.Item(0))).inputfullpath)
        tboxSampleRate.Text = MI.Get_(StreamKind.Audio, 0, "SamplingRate") & " Hz"
        tboxChannels.Text = MI.Get_(StreamKind.Audio, 0, "Channel(s)")
        If MI.Get_(StreamKind.Audio, 0, "BitRate") <> "" Then
            tboxABitrate.Text = (MI.Get_(StreamKind.Audio, 0, "BitRate") / 1024) & " Kbps"
        End If
        Dim Codec As String = MI.Get_(StreamKind.Audio, 0, "Codec/Info")
        If Codec <> "" Then
            tboxACodec.Text = Codec
        Else
            tboxACodec.Text = MI.Get_(StreamKind.Audio, 0, "Codec")
        End If
        tboxResolution.Text = ((MI.Get_(StreamKind.Visual, 0, "Width")) & "x" & MI.Get_(StreamKind.Visual, 0, "Height"))
        tboxFrameRate.Text = MI.Get_(StreamKind.Visual, 0, "FrameRate") & " fps"
        If MI.Get_(StreamKind.Visual, 0, "BitRate") <> "" Then
            tboxVBitrate.Text = System.Convert.ToInt16((System.Convert.ToDouble(MI.Get_(StreamKind.Visual, 0, "BitRate"))) / 1024) & " Kbps"
        End If
        tboxVCodec.Text = MI.Get_(StreamKind.Visual, 0, "Codec")
        MI.Close()
    End Sub
    Public ListOfSelectedFiles
    Private Sub hypClose_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles hypClose.Click
        Me.Close()
    End Sub
    Private Sub Border1_MouseLeftButtonDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Border1.MouseLeftButtonDown
        DragMove()
    End Sub
End Class
