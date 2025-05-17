Imports System.ComponentModel

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MAIN_APP_FORM.TopLevel = False
        PnlApp.Controls.Add(MAIN_APP_FORM)
        MAIN_APP_FORM.Show()
        SCREEN_CAPTURE_FORM.Show()
    End Sub
    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        End
    End Sub

End Class
