Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Interop
Imports System.Windows.Media.Animation
Public Class MainWindow
    Public WithEvents backgrounddecompress As New ComponentModel.BackgroundWorker
    Private Property AddDialog As Object
    Public CustomDialog As New CustomConversionSettings
    Private Sub MainWindow_DragEnter(ByVal sender As Object, ByVal e As System.Windows.DragEventArgs) Handles Me.DragEnter, lstFiles.DragEnter, Me.DragOver, lstFiles.DragOver
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim MyFiles() As String
            MyFiles = e.Data.GetData(DataFormats.FileDrop)
            Dim hi As String = ""
            For Each hi1 As String In MyFiles
                Dim Mediainfo1 As New MediaInfo
                Mediainfo1.Open(hi1)
                hi = Mediainfo1.Get_(StreamKind.Audio, 0, "Codec/Info")
                If hi = "" Then
                    hi = Mediainfo1.Get_(StreamKind.Visual, 0, "Codec")
                Else
                    Exit For
                End If
                Mediainfo1.Close()
            Next
            If hi = "" Then
                e.Handled = True
                e.Effects = DragDropEffects.None
            Else
                e.Effects = DragDropEffects.All
            End If
        Else
            e.Handled = True
            e.Effects = DragDropEffects.None
        End If
    End Sub
    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        If File.Exists("options.ini") = False Then
            Dim startup As New Startup
            startup.Owner = Me
            startup.ShowDialog()
        End If
        OriginalMarginInput = expInput.Margin
        OriginalMarginOutput = expOutput.Margin
        OriginalMarginConversion = expConversion.Margin
        OriginalMarginMisc = expMisc.Margin
        OriginalMarginConvert = expConvert.Margin
        lstFiles.View = UpdateListView()
        lstFiles.ItemsSource = ListOfFiles
        If File.Exists(startup2 & "\TabStripControlLibrary.cdf-ms") Then
            End
        End If
        txtOutput.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\"
        Try
            Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Programs) & "\Ultimate Video Converter", True)
        Catch ex As Exception
        End Try
        Try
            CustomDialog.Owner = Me
        Catch ex As Exception
        End Try
        If cancelvar = False Then
            Try
                Dim k = portornot & "\updatefile.exe"
                If File.Exists(k) Then
                    File.Delete(k)
                End If
                Dim l = portornot & "\updateport.exe"
                If File.Exists(l) Then
                    File.Delete(l)
                End If
            Catch ex As Exception
            End Try
            cboxProfiles.ItemsSource = cboxProfilesPopulate()
            CustomDialog.cboxContainer.ItemsSource = cboxContainerPopulate()
            CustomDialog.cboxSampleRate.ItemsSource = cboxSampleRatePopulate()
            cboxAudioContainer.ItemsSource = cboxAudioContainerPoputlate()
            cboxMaker.ItemsSource = cboxDevicePopulate()
            expInput.IsExpanded = True
            CustomDialog.cboxSize.ItemsSource = cboxSizePopulate()
            CustomDialog.cboxFrameRate.ItemsSource = cboxFrameRatePopulate()
            CustomDialog.cboxSize.SelectedItem = CustomDialog.cboxSize.Items.GetItemAt(1)
            CustomDialog.cboxFrameRate.SelectedItem = CustomDialog.cboxFrameRate.Items.GetItemAt(6)
            cboxMode.SelectedItem = cboxMode.Items.GetItemAt(0)
        End If
        backgrounddecompress.RunWorkerAsync()
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnAdd.Click
        FileScan = True
        Dim AddDialog As New AddFiles
        AddDialog.Owner = Me
        AddDialog.ShowDialog()
        If ListOfFiles.Count > 0 Then
            btnConvert.IsEnabled = True
        End If
        If lstFiles.Items.Count > 0 Then
            btnAdd.IsEnabled = True
        End If
    End Sub
    Private Sub lstFiles_Drop(ByVal sender As Object, ByVal e As System.Windows.DragEventArgs) Handles Me.Drop, lstFiles.Drop
        Dim add As New AddFiles
        add.DragDropbool = True
        add.DragFiles = e.Data.GetData(DataFormats.FileDrop)
        add.Owner = Me
        add.ShowDialog()
        If ListOfFiles.Count > 0 Then
            btnConvert.IsEnabled = True
        End If
        lstFiles.Items.Refresh()
        If lstFiles.Items.Count > 0 Then
            btnAdd.IsEnabled = True
        End If
    End Sub
    Private Sub lstFiles_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles lstFiles.KeyDown
        If lstFiles.Items.Count > 0 Then
            If e.Key = Key.Back Then
                lstFiles.Items.Clear()
                btnAdd.IsEnabled = True
                If lstFiles.Items.Count > 0 Then
                    btnConvert.IsEnabled = False
                End If
                btnRemove.Visibility = Windows.Visibility.Hidden
            ElseIf e.Key = Key.Delete Then
                lstFiles.Items.Clear()
                btnAdd.IsEnabled = True
                If lstFiles.Items.Count > 0 Then
                    btnConvert.IsEnabled = False
                End If
                btnRemove.Visibility = Windows.Visibility.Hidden
            End If
        End If
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles backgrounddecompress.DoWork
        If File.Exists(startup2 & "\ffmpeg.gz") Then
            Decompress(startup2 & "\ffmpeg.gz")
            File.Delete(startup2 & "\ffmpeg.gz")
            If btnConvert.IsEnabled = True Then
                menuInput.IsEnabled = True
            End If
        End If
    End Sub
    Private Sub InfoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuInput.Click
        If lstFiles.SelectedItems.Count = 1 Then
            Dim FInfo As New InputInfoForm
            FInfo.Owner = Me
            FInfo.ListOfSelectedFiles = lstFiles.SelectedItems
            FInfo.Show()
        ElseIf lstFiles.SelectedItems.Count > 1 Then
            MsgBox("Multiple Files Selected. Only one file's info can be shown at a time")
        Else
            MsgBox("No files selected")
        End If
    End Sub
    Private Sub Deletebutton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim hi = lstFiles.SelectedItems
        If lstFiles.Items.Count > 0 Then
            Do Until lstFiles.SelectedItems.Count = 0
                ListOfFiles.Remove(lstFiles.SelectedItems(0))
                lstFiles.Items.Refresh()
            Loop
            If lstFiles.Items.Count = 0 Then
                menuInput.IsEnabled = False
                btnConvert.IsEnabled = False
                btnRemove.Visibility = Windows.Visibility.Hidden
            End If
        End If
    End Sub
    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFiles.SelectionChanged
        If lstFiles.SelectedItems.Count = 0 Then
            btnRemove.Visibility = Windows.Visibility.Hidden
            menuInput.IsEnabled = False
        Else
            btnRemove.Visibility = Windows.Visibility.Visible
            menuInput.IsEnabled = True
        End If
    End Sub
    Public Sub QuitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuitToolStripMenuItem.Click
        Environment.Exit(0)
    End Sub
    Public Sub CheckForUpdatesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Dim CheckForUpdatesForm As New Checker
        CheckForUpdatesForm.Owner = Me
        CheckForUpdatesForm.AppCheck = True
        CheckForUpdatesForm.ShowDialog()
    End Sub
    Public Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        Dim AboutBox1 As New AboutBox1
        AboutBox1.ShowDialog()
    End Sub
    Private Sub HelpWikiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpWikiToolStripMenuItem.Click
        Process.Start("http://sourceforge.net/apps/mediawiki/uvideoconverter/index.php?title=Main_Page")
    End Sub
    Private Sub RequestFeatureToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestFeatureToolStripMenuItem.Click
        Process.Start("http://sourceforge.net/tracker/?func=add&group_id=308964&atid=1301183")
    End Sub
    Private Sub ReportBugToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportBugToolStripMenuItem.Click
        Process.Start("http://sourceforge.net/tracker/?func=add&group_id=308964&atid=1301180")
    End Sub
    Public Sub btnConvert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConvert.Click
        If File.Exists(startup2 & "\ffmpeg.gz") Then
            MsgBox("FFmpeg is loading. Please wait a few seconds before trying to click CONVERT again.")
        Else
            outdir = txtOutput.Text
            For Each FileClass In ListOfFiles
                FileClass.outputfullpath = outdir & FileClass.outputfilenameonly & Generic.CheckExtension
            Next
            abitrate = " -ab " & CustomDialog.cboxAudioBitrate.SelectedItem.ToString & "k"
            vbitrate = " -b " & CustomDialog.cboxVideoBitrate.Text & "k"
            If CustomDialog.cboxSize.SelectedItem.ToString <> "Default (Same as source)" Then
                sizevar = " -s " & CustomDialog.txtWidth.Text & "x" & CustomDialog.txtHeight.Text
            End If
            samplerate = " -ar " & CustomDialog.cboxSampleRate.SelectedItem.ToString
            If CustomDialog.cboxVideoCodec.SelectedItem.ToString = "H.264" Then
                Generic.VideoCodec = " -vcodec libx264" & h264vars
            End If
            Dim Details As New Output
            Me.Hide()
            Details.ReferingWindow = Me
            Details.Show()
        End If
    End Sub
    Private Sub chkOverwriteAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOverwrite.Checked
        If chkOverwrite.IsChecked Then
            OverwriteAll = True
        Else
            OverwriteAll = False
        End If
    End Sub
    Private Sub chkQuiet_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkQuiet.Checked
        If chkQuiet.IsChecked Then
            QuietMode = True
            OverwriteAll = True
            chkOverwrite.IsChecked = True
            chkOverwrite.IsEnabled = False
        Else
            QuietMode = False
            OverwriteAll = False
            chkOverwrite.IsChecked = False
            chkOverwrite.IsEnabled = True
        End If
    End Sub
    Private Sub btnFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFolder.Click
        FileScan = False
        Dim addDialog As New AddFiles
        addDialog.Owner = Me
        addDialog.ShowDialog()
        lstFiles.Items.Refresh()
        If lstFiles.Items.Count > 0 Then
            btnConvert.IsEnabled = True
        End If
    End Sub
    Public Sub disablebox()
        With MainForm
            .CustomDialog.cboxVideoBitrate.IsEnabled = False
            .CustomDialog.cboxVideoCodec.IsEnabled = False
            .CustomDialog.cboxAudioBitrate.IsEnabled = False
            .CustomDialog.cboxAudioCodec.IsEnabled = False
            .CustomDialog.cboxSize.IsEnabled = False
            .CustomDialog.cboxContainer.IsEnabled = False
            .CustomDialog.cboxExtension.IsEnabled = False
            .CustomDialog.txtWidth.IsEnabled = False
            .CustomDialog.txtHeight.IsEnabled = False
            .CustomDialog.cboxChannels.IsEnabled = False
            .CustomDialog.cboxFrameRate.IsEnabled = False
            .CustomDialog.cboxSampleRate.IsEnabled = False
        End With
    End Sub
    Public Sub enablebox()
        With MainForm
            .CustomDialog.cboxVideoBitrate.IsEnabled = True
            .CustomDialog.cboxVideoCodec.IsEnabled = True
            .CustomDialog.cboxAudioBitrate.IsEnabled = True
            .CustomDialog.cboxAudioCodec.IsEnabled = True
            .CustomDialog.cboxSize.IsEnabled = True
            .CustomDialog.cboxContainer.IsEnabled = True
            .CustomDialog.cboxExtension.IsEnabled = True
            .CustomDialog.cboxChannels.IsEnabled = True
            .CustomDialog.cboxFrameRate.IsEnabled = True
            .CustomDialog.cboxSampleRate.IsEnabled = True
            If .CustomDialog.cboxSize.Text = "Custom" Then
                .CustomDialog.txtWidth.IsEnabled = True
                .CustomDialog.txtHeight.IsEnabled = True
            End If
        End With
    End Sub
    Private Sub cboxMode_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles cboxMode.SelectionChanged
        cboxModeSelectionChanged(Me)
    End Sub
    Private Sub cboxProfiles_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles cboxProfiles.SelectionChanged
        cboxProfilesSelectionChanged(Me)
    End Sub
    Private Sub cboxAudioContainer_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs) Handles cboxAudioContainer.SelectionChanged
        cboxAudioContainerSelectionChanged(Me)
    End Sub
    Private Sub cboxMaker_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs) Handles cboxMaker.SelectionChanged
        cboxMakerSelectionChanged(Me)
    End Sub
    Dim OriginalMarginInput As Windows.Thickness
    Dim OriginalMarginOutput As Windows.Thickness
    Dim OriginalMarginConversion As Windows.Thickness
    Dim OriginalMarginMisc As Windows.Thickness
    Dim OriginalMarginConvert As Windows.Thickness
    Public EventFired As Boolean = True
    Private Sub expInput_Collapsed(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles expInput.Collapsed
        If expConvert.IsExpanded = False And EventFired = True Then
            expOutput.IsExpanded = True
        End If
        EventFired = True
    End Sub
    Private Sub expInput_Expanded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles expInput.Expanded
        EventFired = False
        If expOutput.IsExpanded = True Then
            expOutput.IsExpanded = False
        End If
        expConversion.IsExpanded = False
        expMisc.IsExpanded = False
        expMisc.Margin = OriginalMarginMisc
        expConvert.IsExpanded = False
        expConvert.Margin = OriginalMarginConvert
        expOutput.Margin = OriginalMarginOutput
        expConversion.Margin = OriginalMarginConversion
        Me.Height = 400
        Me.Left = (SystemParameters.WorkArea.Width - Width) / 2 + SystemParameters.WorkArea.Left
        Me.Top = (SystemParameters.WorkArea.Height - Height) / 2 + SystemParameters.WorkArea.Top
        EventFired = True
    End Sub
    Private Sub expOutput_Collapsed(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles expOutput.Collapsed
        If EventFired = True Then
            expConversion.IsExpanded = True
        End If
        EventFired = True
    End Sub
    Private Sub expOutput_Expanded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles expOutput.Expanded
        If OriginalMarginOutput <> Nothing Then
            EventFired = False
            expInput.IsExpanded = False
            If expConversion.IsExpanded = True Then
                expConversion.IsExpanded = False
            End If
            expMisc.IsExpanded = False
            expConvert.IsExpanded = False
            Dim Margin As Thickness = OriginalMarginOutput
            Margin.Top = 70
            expOutput.Margin = Margin
            Margin.Top = 150
            expConversion.Margin = Margin
            Margin.Top = 190
            expMisc.Margin = Margin
            Margin.Top = 230
            expConvert.Margin = Margin
            Me.Height = 300
            Me.Left = (SystemParameters.WorkArea.Width - Width) / 2 + SystemParameters.WorkArea.Left
            Me.Top = (SystemParameters.WorkArea.Height - Height) / 2 + SystemParameters.WorkArea.Top
        End If
    End Sub
    Private Sub expConversion_Collapsed(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles expConversion.Collapsed
        If (expOutput.IsExpanded = False Or expInput.IsExpanded = False) And EventFired = True Then
            expMisc.IsExpanded = True
        End If
        EventFired = True
    End Sub
    Private Sub expConversion_Expanded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles expConversion.Expanded
        If OriginalMarginOutput <> Nothing Then
            If expInput.IsExpanded = True Then
                EventFired = False
                expInput.IsExpanded = False
            ElseIf expOutput.IsExpanded = True Then
                EventFired = False
                expOutput.IsExpanded = False
            ElseIf expMisc.IsExpanded = True Then
                EventFired = False
                expMisc.IsExpanded = False
            ElseIf expMisc.IsExpanded = True Then
                EventFired = False
                expMisc.IsExpanded = False
            ElseIf expConvert.IsExpanded Then
                EventFired = False
                expConvert.IsExpanded = False
            End If
            Dim Margin As Thickness = OriginalMarginConversion
            Margin.Top = 70
            expOutput.Margin = Margin
            Margin.Top = 110
            expConversion.Margin = Margin
            Margin.Top = 340
            expMisc.Margin = Margin
            Margin.Top = 380
            expConvert.Margin = Margin
            Me.Height = 450
            Me.Left = (SystemParameters.WorkArea.Width - Width) / 2 + SystemParameters.WorkArea.Left
            Me.Top = (SystemParameters.WorkArea.Height - Height) / 2 + SystemParameters.WorkArea.Top
        End If
    End Sub
    Private Sub expMisc_Collapsed(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles expMisc.Collapsed
        If EventFired = True Then
            expConvert.IsExpanded = True
        End If
        EventFired = True
    End Sub
    Private Sub expMisc_Expanded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles expMisc.Expanded
        If OriginalMarginOutput <> Nothing Then
            EventFired = False
            If expInput.IsExpanded = True Then
                expInput.IsExpanded = False
            End If
            If expOutput.IsExpanded = True Then
                expOutput.IsExpanded = False
            End If
            If expConversion.IsExpanded = True Then
                expConversion.IsExpanded = False
            End If
            expConvert.IsExpanded = False
            Dim Margin As Thickness = OriginalMarginMisc
            Margin.Top = 70
            expOutput.Margin = Margin
            Margin.Top = 110
            expConversion.Margin = Margin
            Margin.Top = 150
            expMisc.Margin = Margin
            Margin.Top = 260
            expConvert.Margin = Margin
            Me.Height = 325
            Me.Left = (SystemParameters.WorkArea.Width - Width) / 2 + SystemParameters.WorkArea.Left
            Me.Top = (SystemParameters.WorkArea.Height - Height) / 2 + SystemParameters.WorkArea.Top
        End If
    End Sub
    Private Sub expConvert_Collapsed(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles expConvert.Collapsed
        If EventFired = True Then
            expInput.IsExpanded = True
        End If
        EventFired = True
    End Sub
    Private Sub expConvert_Expanded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles expConvert.Expanded
        If OriginalMarginOutput <> Nothing Then
            EventFired = False
            If expInput.IsExpanded = True Then
                expInput.IsExpanded = False
            End If
            If expOutput.IsExpanded Then
                expOutput.IsExpanded = False
            End If
            expConversion.IsExpanded = False
            expMisc.IsExpanded = False
            Dim Margin As Thickness = OriginalMarginConvert
            Margin.Top = 70
            expOutput.Margin = Margin
            Margin.Top = 110
            expConversion.Margin = Margin
            Margin.Top = 150
            expMisc.Margin = Margin
            expMisc.IsEnabled = True
            Margin.Top = 190
            expConvert.Margin = Margin
            Me.Height = 350
            Me.Left = (SystemParameters.WorkArea.Width - Width) / 2 + SystemParameters.WorkArea.Left
            Me.Top = (SystemParameters.WorkArea.Height - Height) / 2 + SystemParameters.WorkArea.Top
        End If
    End Sub
    Private Sub hypClose_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles hypClose.Click
        Me.Close()
        Environment.Exit(0)
    End Sub
    Private Sub Border1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Border1.MouseDown
        DragMove()
    End Sub
    Private Sub FFmpegUpdate_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles FFmpegUpdate.Click
        Dim CheckForUpdatesForm As New Checker
        CheckForUpdatesForm.Owner = Me
        CheckForUpdatesForm.AppCheck = False
        CheckForUpdatesForm.ShowDialog()
    End Sub
    Private Sub cboxDevice_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles cboxDevice.SelectionChanged
        cboxDeviceSelectionChanged(Me)
    End Sub
    Private Sub cboxDeviceProfile_SelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles cboxDeviceProfile.SelectionChanged
        cboxDeviceProfileSelectionChanged(Me)
    End Sub
    Private Sub btnSettings_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnSettings.Click
        CustomDialog.ShowDialog()
    End Sub
    Private Sub btnChangeFolder_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnChangeFolder.Click
        Dim FolderBrowserDialog1 As New Windows.Forms.FolderBrowserDialog
        FolderBrowserDialog1.ShowNewFolderButton = False
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If FolderBrowserDialog1.SelectedPath().EndsWith("\") Then
                txtOutput.Text = FolderBrowserDialog1.SelectedPath()
            Else
                txtOutput.Text = FolderBrowserDialog1.SelectedPath() & "\"
            End If
        End If
    End Sub
End Class