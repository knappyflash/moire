Imports System.ComponentModel

Public Class Form1

    Private _MyScreenCapture As SCREEN_CAPTURE_FORM
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ioListener.Show()


        '_MyScreenCapture = New SCREEN_CAPTURE_FORM
        'MAIN_APP_FORM.TopLevel = False
        'PnlApp.Controls.Add(MAIN_APP_FORM)
        'MAIN_APP_FORM.Set_MyScreenCapture(_MyScreenCapture)
        'MAIN_APP_FORM.Dock = DockStyle.Fill
        'MAIN_APP_FORM.Show()
        '_MyScreenCapture.Show()
    End Sub
    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        End
    End Sub

End Class
