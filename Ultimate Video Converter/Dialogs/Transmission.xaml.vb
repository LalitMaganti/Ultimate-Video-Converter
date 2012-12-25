Public Class Transmission
    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Me.WebBrowser1.Navigate(New Uri("http://uvideoconverter.sourceforge.net/stat.html"))
    End Sub
    Private Sub WebBrowser1_Navigated(ByVal sender As Object, ByVal e As System.Windows.Navigation.NavigationEventArgs) Handles WebBrowser1.Navigated
        Me.Close()
        Me.Owner.Focus()
    End Sub
    Private Sub Border2_MouseLeftButtonDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Border2.MouseLeftButtonDown
        DragMove()
    End Sub
End Class
