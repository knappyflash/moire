Imports System.Runtime.InteropServices

Public Class MAIN_APP_FORM

    Private _ScreenCapture As New SCREEN_CAPTURE_FORM
    Private _mif As New moire_image_file
    Private Sub MAIN_APP_FORM_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        InitMe()
        Show_Mif_In_Panel()
        AddHandler ioListener.Released_PrintScreen, AddressOf Handle_Key_PrintScreen_Release

    End Sub

    Private Sub InitMe()
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
    End Sub

    Private Sub Show_Mif_In_Panel()
        _mif.ScreenCapture = _ScreenCapture
        _mif.TopLevel = False
        _mif.FormBorderStyle = FormBorderStyle.None ' Removes borders
        _mif.Dock = DockStyle.Fill ' Fills the panel
        PnlMIF.Controls.Add(_mif)
        _mif.Show()
    End Sub

    Private Sub Handle_Key_PrintScreen_Release()
        Start_Screenshot()
    End Sub

    Private Sub Start_Screenshot()
        _ScreenCapture = New SCREEN_CAPTURE_FORM
        _mif.ScreenCapture = _ScreenCapture
        _ScreenCapture.Show()
    End Sub

End Class