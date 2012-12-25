Imports System.Environment
Imports System.IO
Imports System.Net
Public Class WPFAsker
    Public WithEvents BackgroundWorker11 As New ComponentModel.BackgroundWorker
    Private Sub btnNo_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnNo.Click
        Me.DialogResult = False
        Me.Close()
    End Sub
    Private Sub btnYes_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnYes.Click
        Me.DialogResult = True
        Me.Close()
    End Sub
End Class