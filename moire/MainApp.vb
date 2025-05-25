Imports System.IO
Imports System.Runtime.InteropServices

Public Class MainApp

    Private _ScreenCapture As New ScreenCapture
    Private _mif As New MoireImage
    Private Sub MAIN_APP_FORM_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        InitMe()
        Show_Mif_In_Panel()
        AddHandler ioListener.Released_PrintScreen, AddressOf Handle_Key_PrintScreen_Release

        Load_Timeline()

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
        _ScreenCapture = New ScreenCapture
        _mif.ScreenCapture = _ScreenCapture
        _ScreenCapture.Show()
    End Sub

    Private Sub Load_Timeline()

        Dim rnd As New Random()

        Console.WriteLine($"{Application.StartupPath}\mifs\*.mif")

        PanelTimeLine.AutoScroll = True
        PanelTimeLine.HorizontalScroll.Visible = True ' Forces scrollbar to be shown
        PanelTimeLine.Width = PnlMIF.Width
        FlowLayoutPanelTimeLine.FlowDirection = FlowDirection.LeftToRight
        FlowLayoutPanelTimeLine.WrapContents = False
        FlowLayoutPanelTimeLine.Width = PanelTimeLine.Width * 2 ' Make it wider to trigger scrollbar


        For Each file As String In Directory.GetFiles($"{Application.StartupPath}\mifs", "*.mif")
            Console.WriteLine($"Found image file: {file}")
            Dim Pbox As New PictureBox
            Dim randomColor As Color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256))
            Pbox.BackColor = randomColor
            Pbox.BorderStyle = BorderStyle.FixedSingle
            Pbox.Height = FlowLayoutPanelTimeLine.Height - 8
            FlowLayoutPanelTimeLine.Controls.Add(Pbox)
        Next

    End Sub

End Class