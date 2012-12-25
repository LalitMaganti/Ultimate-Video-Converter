Imports System.Net
Module Vars
    Public startup2 As String = Environment.CurrentDirectory
    Public ffmpeg As New Process
    Public myStreamReader As IO.StreamReader
    Public OneLine As String
    Public vcodec As String
    Public acodec As String
    Public extention As String
    Public outdir As String
    Public checkextetion As String
    Public abitrate As String
    Public vbitrate As String
    Public sizevar As String
    Public channels As String
    Public properupdate As Boolean
    Public framerate As String
    Public samplerate As String
    Public a(127) As String
    Public extensionnumber As Integer = 127
    Public h264vars = " -coder 0 -flags -loop -cmp +chroma -partitions -parti8x8-parti4x4-partp8x8-partb8x8 -me_method dia -subq 0 -me_range 16 -g 250 -keyint_min 25 -sc_threshold 0 -i_qfactor 0.71 -b_strategy 0 -qcomp 0.6 -qmin 10 -qmax 51 -qdiff 4 -bf 0 -refs 1 -directpred 1 -trellis 0 -flags2 -bpyramid-mixed_refs-wpred-dct8x8+fastpskip-mbtree -wpredp 0 -aq_mode 0"
    Public Client As WebClient
    Public favloc As String
    Public startup = Windows.Forms.Application.StartupPath.ToString
    Public portornot = startup
    Public cancelvar = False
    Public OverwriteAll As Boolean
    Public QuietMode As Boolean
    Public FileScan As Boolean
    Public LogList As New List(Of LogItem)
    Public Generic As New GenericOptions
End Module