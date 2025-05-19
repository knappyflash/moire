Imports System.ComponentModel

Public Class Form1

    Private _MyScreenCapture As SCREEN_CAPTURE_FORM
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ioListener.Timer1.Start()

        AddHandler ioListener.Released_Escape, AddressOf QuickTest1
        AddHandler ioListener.Released_PrintScreen, AddressOf QuickTest2
        AddHandler ioListener.Released_LMouse, AddressOf QuickTest3
        AddHandler ioListener.Released_RMouse, AddressOf QuickTest4

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

    Private Sub QuickTest1()
        Console.WriteLine($"{Now} Escape Released!")
    End Sub

    Private Sub QuickTest2()
        Console.WriteLine($"{Now} PrintScreen Released!")
    End Sub

    Private Sub QuickTest3()
        Console.WriteLine($"{Now} LMouse Released!")
    End Sub

    Private Sub QuickTest4()
        Console.WriteLine($"{Now} RMouse Released!")
    End Sub

End Class
