Imports System.IO
Imports System.Runtime.InteropServices

Public Class MainApp

    Private _ScreenCapture As New ScreenCapture
    Private _mif As New MoireImage
    Private Sub MAIN_APP_FORM_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        InitMe()
        ShowMifInPanel()
        AddHandler ioListener.Released_PrintScreen, AddressOf Handle_Key_PrintScreen_Release

        LoadTimeline()

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

    Private Sub ShowMifInPanel()
        _mif.ScreenCapture = _ScreenCapture
        _mif.TopLevel = False
        _mif.FormBorderStyle = FormBorderStyle.None ' Removes borders
        _mif.Dock = DockStyle.Fill ' Fills the panel
        PnlMIF.Controls.Add(_mif)
        _mif.Show()
        AddHandler _mif.MoireImage_Created, AddressOf HandleMoireImageCreated
    End Sub

    Private Sub Handle_Key_PrintScreen_Release()
        Start_Screenshot()
    End Sub

    Private Sub Start_Screenshot()
        _ScreenCapture = New ScreenCapture
        _mif.ScreenCapture = _ScreenCapture
        _ScreenCapture.Show()
    End Sub

    Private Sub LoadTimeline()

        Dim files As String() = Directory.GetFiles($"{Application.StartupPath}\mifs", "*.mif").OrderByDescending(Function(f) File.GetLastWriteTime(f)).ToArray()

        Dim rnd As New Random()

        Console.WriteLine($"{Application.StartupPath}\mifs\*.mif")

        FlowLayoutPanelTimeLine.FlowDirection = FlowDirection.LeftToRight
        FlowLayoutPanelTimeLine.WrapContents = False

        FlowLayoutPanelTimeLine.Controls.Clear()

        For Each file As String In files
            Dim tempMif As New MoireImage
            Dim randomColor As Color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256))
            Dim thumbnailPanel As New Panel
            Dim thumbnailPictureBox As New PictureBox
            Dim thumbnailName As String = Path.GetFileName(file)
            Dim thumbnailPanelHeight As Integer = FlowLayoutPanelTimeLine.Height * 0.75
            Dim thumbnailPanelWidth As Integer = thumbnailPanelHeight * 1.9
            Dim thumbnailLabel As New Label

            thumbnailName = thumbnailName.Replace(".mif", "")

            FlowLayoutPanelTimeLine.Controls.Add(thumbnailPanel)
            thumbnailPanel.Controls.Add(thumbnailPictureBox)
            thumbnailPanel.Controls.Add(thumbnailLabel)

            thumbnailLabel.Text = thumbnailName

            tempMif.ReadMifFile(file)

            thumbnailPanel.Name = thumbnailName
            thumbnailPanel.BackColor = Color.White
            thumbnailPanel.BorderStyle = BorderStyle.FixedSingle
            thumbnailPanel.Height = thumbnailPanelHeight
            thumbnailPanel.Width = thumbnailPanelWidth

            thumbnailPictureBox.Name = thumbnailName
            thumbnailPictureBox.BackColor = Color.White
            thumbnailPictureBox.BorderStyle = BorderStyle.FixedSingle
            thumbnailPictureBox.SizeMode = PictureBoxSizeMode.Zoom
            Console.WriteLine($"{thumbnailPictureBox.Name}: thumbnailPictureBox.Width:{thumbnailPictureBox.Width}, thumbnailPictureBox.Height:{thumbnailPictureBox.Height}")

            thumbnailPictureBox.Image = tempMif.Thumbnail

            thumbnailLabel.Top = thumbnailPanelHeight - thumbnailLabel.Height
            thumbnailLabel.Width = thumbnailPanel.Width
            thumbnailPictureBox.Height = thumbnailPanelHeight - thumbnailLabel.Height

            thumbnailLabel.AutoSize = True
            thumbnailLabel.TextAlign = ContentAlignment.MiddleCenter
            thumbnailLabel.Left = (thumbnailPanel.Width - thumbnailLabel.Width) \ 2

            thumbnailPictureBox.Left = (thumbnailPanel.Width - thumbnailPictureBox.Width) \ 2

        Next

    End Sub

    Private Sub HandleMoireImageCreated()
        LoadTimeline()
    End Sub

End Class