Imports System.ComponentModel

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'QuickTest()
        'Exit Sub

        ioListener.Timer1.Start()

        MAIN_APP_FORM.TopLevel = False
        Me.Controls.Add(MAIN_APP_FORM)
        MAIN_APP_FORM.Dock = DockStyle.Fill
        MAIN_APP_FORM.Show()

    End Sub
    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        End
    End Sub

    Private Sub QuickTest()

    End Sub

End Class
