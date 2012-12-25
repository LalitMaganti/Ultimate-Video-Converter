Imports System.Environment
Imports System.IO
Imports System.Net
Imports ICSharpCode.SharpZipLib.GZip
Imports ICSharpCode.SharpZipLib.Tar
Imports System.IO.Compression
Imports System.Windows.Controls
Imports System.Collections.ObjectModel
Module processes
    Public startupdate As Boolean
    Public listofextetions As New List(Of String)
    Public list As List(Of String)
    Public WithEvents ListOfFiles As New ObservableCollection(Of FileClass)
    Public Function UpdateListView()
        Dim gv As New System.Windows.Controls.GridView
        Dim gvcName As New System.Windows.Controls.GridViewColumn
        Dim gvcSize As New System.Windows.Controls.GridViewColumn
        With gvcName
            .Header = "Files"
            Dim bName As New System.Windows.Data.Binding
            bName.Mode = Windows.Data.BindingMode.OneWay
            bName.Path = New System.Windows.PropertyPath(".inputfullpath")
            Dim feName As New FrameworkElementFactory(GetType(System.Windows.Controls.TextBlock))
            Dim dtName As New DataTemplate
            feName.SetBinding(System.Windows.Controls.TextBlock.TextProperty, bName)
            dtName.VisualTree = feName
            .DisplayMemberBinding = bName
            .CellTemplate = dtName
        End With
        With gvcSize
            .Header = "Duration"
            Dim bName As New System.Windows.Data.Binding
            bName.Mode = Windows.Data.BindingMode.OneWay
            bName.Path = New System.Windows.PropertyPath(".duration")
            Dim feName As New FrameworkElementFactory(GetType(System.Windows.Controls.TextBlock))
            Dim dtName As New DataTemplate
            feName.SetBinding(System.Windows.Controls.TextBlock.TextProperty, bName)
            dtName.VisualTree = feName
            .DisplayMemberBinding = bName
            .CellTemplate = dtName
        End With
        gv.Columns.Add(gvcName)
        gv.Columns.Add(gvcSize)
        Return gv
    End Function
    Public Function DetailsListView()
        Dim gv As New System.Windows.Controls.GridView
        Dim gvcName As New System.Windows.Controls.GridViewColumn
        Dim gvcSize As New System.Windows.Controls.GridViewColumn
        Dim gvcLast As New System.Windows.Controls.GridViewColumn
        Dim timeremaining As New System.Windows.Controls.GridViewColumn
        Dim status As New System.Windows.Controls.GridViewColumn
        Dim progress As New System.Windows.Controls.GridViewColumn
        With gvcName
            .Header = "Files"
            Dim bName As New System.Windows.Data.Binding
            bName.Mode = Windows.Data.BindingMode.OneWay
            bName.Path = New System.Windows.PropertyPath(".outputfullpath")
            Dim feName As New FrameworkElementFactory(GetType(System.Windows.Controls.TextBlock))
            Dim dtName As New DataTemplate
            feName.SetBinding(System.Windows.Controls.TextBlock.TextProperty, bName)
            dtName.VisualTree = feName
            .DisplayMemberBinding = bName
            .CellTemplate = dtName
        End With
        With gvcLast
            .Header = "Time Elapsed"
            Dim bName As New System.Windows.Data.Binding
            bName.Mode = Windows.Data.BindingMode.OneWay
            bName.Path = New System.Windows.PropertyPath(".timetaken")
            Dim feName As New FrameworkElementFactory(GetType(System.Windows.Controls.TextBlock))
            Dim dtName As New DataTemplate
            feName.SetBinding(System.Windows.Controls.TextBlock.TextProperty, bName)
            dtName.VisualTree = feName
            .DisplayMemberBinding = bName
            .CellTemplate = dtName
        End With
        With timeremaining
            .Header = "Time Remaining"
            Dim bName As New System.Windows.Data.Binding
            bName.Mode = Windows.Data.BindingMode.OneWay
            bName.Path = New System.Windows.PropertyPath(".timeremaining")
            Dim feName As New FrameworkElementFactory(GetType(System.Windows.Controls.TextBlock))
            Dim dtName As New DataTemplate
            feName.SetBinding(System.Windows.Controls.TextBlock.TextProperty, bName)
            dtName.VisualTree = feName
            .DisplayMemberBinding = bName
            .CellTemplate = dtName
        End With
        With progress
            .Header = "Progress"
            Dim bName As New System.Windows.Data.Binding
            bName.Mode = Windows.Data.BindingMode.OneWay
            bName.Path = New System.Windows.PropertyPath(".progress")
            Dim feName As New FrameworkElementFactory(GetType(System.Windows.Controls.TextBlock))
            Dim dtName As New DataTemplate
            feName.SetBinding(System.Windows.Controls.TextBlock.TextProperty, bName)
            dtName.VisualTree = feName
            .DisplayMemberBinding = bName
            .CellTemplate = dtName
        End With
        With status
            .Header = "Status of Conversion"
            Dim bName As New System.Windows.Data.Binding
            bName.Mode = Windows.Data.BindingMode.OneWay
            bName.Path = New System.Windows.PropertyPath(".status")
            Dim feName As New FrameworkElementFactory(GetType(System.Windows.Controls.TextBlock))
            Dim dtName As New DataTemplate
            feName.SetBinding(System.Windows.Controls.TextBlock.TextProperty, bName)
            dtName.VisualTree = feName
            .DisplayMemberBinding = bName
            .CellTemplate = dtName
        End With
        gv.Columns.Add(gvcName)
        gv.Columns.Add(gvcLast)
        gv.Columns.Add(timeremaining)
        gv.Columns.Add(status)
        gv.Columns.Add(progress)
        Return gv
    End Function

    Public Function cboxProfilesPopulate()
        Dim strArray As String() = {"MP4 (MPEG4)", "MP4 (H.264)", "MKV", "AVI (Microsoft MPEG 4 v3)", "AVI (Xvid)", "OGG", "ASF", "WMV", "FLV"}
        Return strArray
    End Function
    Public Function cboxContainerPopulate()
        Dim strArray As String() = {"MP4", "MOV", "AVI", "WMV", "OGG", "MPEG", "ASF", "FLV", "MKV", "3GP"}
        Return strArray
    End Function
    Public Function cboxAudioContainerPoputlate()
        Dim strArray As String() = {"MP3 (MPEG)", "MP2 (MPEG)", "AAC (MP4)", "AC3 (Dolby D)", "AIFF", "Vorbis (OGG)", "WAV", "WMA", "FLAC", "MKA"}
        Return strArray
    End Function
    Public Function cboxSampleRatePopulate()
        Dim strArray As String() = {"8000", "32000", "44100", "48000"}
        Return strArray
    End Function
    Public Function cboxDevicePopulate()
        Dim strArray As String() = {"Apple", "Blackberry", "Microsoft", "Nintendo", "RockBox", "Sony"}
        Return strArray
    End Function
    Public Function cboxSizePopulate()
        Dim strArray As String() = {"Default (Same as source)", "VGA", "SVGA", "HD 480P", "HD 720P", "HD 1080P", "Custom"}
        Return strArray
    End Function
    Public Function cboxAlternativeSizePopulate()
        Dim strArray As String() = {"128x96", "176x144", "352x288", "704x576", "1408x1152"}
        Return strArray
    End Function
    Public Function cboxFrameRatePopulate()
        Dim strArray As String() = {"12", "15", "20", "23.97", "24", "25 (PAL)", "29.97 (NTSC)", "30"}
        Return strArray
    End Function
    Public Sub addcbox(ByVal cbox As Windows.Controls.ComboBox, ByVal codec As String)
        cbox.Items.Add(codec)
    End Sub
    Public Sub clearcbox(ByVal cbox As Windows.Controls.ComboBox)
        cbox.Items.Clear()
    End Sub
    Public Sub Decompress(ByVal strPath As String)
        If File.Exists(startup2 & "\ffmpeg.exe") Then
            File.Delete(startup2 & "\ffmpeg.exe")
        End If
        Dim current As DateTime
        Dim dstFile As String
        Dim fsIn As FileStream = Nothing
        Dim fsOut As FileStream = Nothing
        Dim gzip As GZipStream = Nothing
        Const bufferSize As Integer = 200000
        Dim buffer As Byte() = New Byte(bufferSize - 1) {}
        Dim count As Integer = 0
        Try
            current = DateTime.Now
            dstFile = startup2 + "\ffmpeg.exe"
            Dim inStream As Stream = File.OpenRead(strPath)
            Dim gzipStream As Stream = New GZipInputStream(inStream)
            Dim tarArchive As TarArchive = tarArchive.CreateInputTarArchive(gzipStream)
            tarArchive.ExtractContents(startup2 + "\")
            tarArchive.Close()
            gzipStream.Close()
            inStream.Close()
        Catch ex As Exception
            MsgBox("There was a problem extracting FFmpeg. Please reinstall the program")
            System.Windows.Forms.Application.Exit()
        Finally
            If gzip IsNot Nothing Then
                gzip.Close()
                gzip = Nothing
            End If
            If fsOut IsNot Nothing Then
                fsOut.Close()
                fsOut = Nothing
            End If
            If fsIn IsNot Nothing Then
                fsIn.Close()
                fsIn = Nothing
            End If
        End Try
    End Sub
End Module
