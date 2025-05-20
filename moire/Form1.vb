Imports System.ComponentModel

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ioListener.Timer1.Start()

        'AddHandler ioListener.Released_Escape, AddressOf Escape_Relased
        'AddHandler ioListener.Released_PrintScreen, AddressOf PrintScreen_Relased
        'AddHandler ioListener.Released_LMouse, AddressOf LMouse_Relased
        'AddHandler ioListener.Released_RMouse, AddressOf RMouse_Relased

        'AddHandler ioListener.ClickDown_LMouse, AddressOf LMouse_ClickDown
        'AddHandler ioListener.ClickDown_RMouse, AddressOf RMouse_ClickDown

        MAIN_APP_FORM.TopLevel = False
        PnlApp.Controls.Add(MAIN_APP_FORM)
        MAIN_APP_FORM.Dock = DockStyle.Fill
        MAIN_APP_FORM.Show()

    End Sub
    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        End
    End Sub

    Private Sub Escape_Relased()
        Console.WriteLine($"{Now} Escape Released!")
    End Sub

    Private Sub PrintScreen_Relased()
        Console.WriteLine($"{Now} PrintScreen Released!")
    End Sub

    Private Sub LMouse_Relased()
        Console.WriteLine($"{Now} LMouse Released!")
    End Sub

    Private Sub RMouse_Relased()
        Console.WriteLine($"{Now} RMouse Released!")
    End Sub

    Private Sub LMouse_ClickDown()
        Console.WriteLine($"{Now} LMouse Click Down!")
    End Sub

    Private Sub RMouse_ClickDown()
        Console.WriteLine($"{Now} RMouse Click Down!")
    End Sub

End Class
