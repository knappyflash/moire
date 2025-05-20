Imports System.Runtime.InteropServices

Public Class MAIN_APP_FORM

    Private _MyScreenCapture As SCREEN_CAPTURE_FORM
    Private Sub MAIN_APP_FORM_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.SizeGripStyle = SizeGripStyle.Hide
        Me.MinimizeBox = False
        Me.MaximizeBox = False
        Me.ShowInTaskbar = False
        Me.ShowIcon = False
        Me.ControlBox = False
        Me.DoubleBuffered = True
        Me.WindowState = FormWindowState.Normal
        Me.FormBorderStyle = FormBorderStyle.None
        Me.TopMost = False
        Me.TopLevel = False
        Me.Refresh()

        AddHandler ioListener.Released_PrintScreen, AddressOf Handle_Key_PrintScreen_Release

    End Sub

    Private Sub Handle_Image_Available(img)
        PictureBox1.Image = img
    End Sub

    Private Sub Handle_Key_PrintScreen_Release()
        Start_Screenshot()
    End Sub

    Private Sub Start_Screenshot()
        _MyScreenCapture = New SCREEN_CAPTURE_FORM
        AddHandler _MyScreenCapture.Capture_Image_Available, AddressOf Handle_Image_Available
        _MyScreenCapture.Show()
    End Sub


End Class