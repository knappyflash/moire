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
            Dim thumbnail = New Thumbnail
            thumbnail.ReadMifFile(file)
            thumbnail.TopLevel = False
            FlowLayoutPanelTimeLine.Controls.Add(thumbnail)
            thumbnail.Show()
            AddHandler thumbnail.Thumbnail_Selected, AddressOf HandleThumbnailSelected
            If file = files(0) Then thumbnail.IsThumbnailSelected = True
        Next

    End Sub

    Private Sub HandleMoireImageCreated()
        LoadTimeline()
    End Sub

    Private Sub HandleThumbnailSelected(mifPath As String)
        _mif.ReadMifFile(mifPath)
        For Each tn As Thumbnail In FlowLayoutPanelTimeLine.Controls
            If tn.mifFilePath <> mifPath Then
                tn.IsThumbnailSelected = False
            End If
        Next
    End Sub

End Class