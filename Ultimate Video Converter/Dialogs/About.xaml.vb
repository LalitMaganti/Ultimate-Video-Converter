Public Class About
    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        TextBox1.Text = TextBox1.Text + (Environment.NewLine & Environment.NewLine & "Version: " & My.Application.Info.Version.ToString)
    End Sub
    Private Sub Border1_MouseLeftButtonDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Border1.MouseLeftButtonDown
        DragMove()
    End Sub
    Private Sub hypClose_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles hypClose.Click
        Me.Close()
        Me.Owner.Focus()
    End Sub
End Class
