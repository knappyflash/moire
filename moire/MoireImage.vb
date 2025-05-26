
Imports System.Text.Json
Imports System.IO

Public Class MoireImage

    Public Event MoireImage_Created()

    Private _myScreenCapture As ScreenCapture
    Private _mifData As New MifData
    Private _image As Image
    Private _thumbnail As Image
    Private _jsonString As String

    Public Property ScreenCapture As ScreenCapture
        Get
            Return _myScreenCapture
        End Get
        Set(value As ScreenCapture)
            _myScreenCapture = value
            AddHandler _myScreenCapture.Capture_Image_Available, AddressOf HandleImageAvailable
        End Set
    End Property

    Public Property Thumbnail As Image
        Get
            Return _thumbnail
        End Get
        Set(value As Image)
            _thumbnail = value
        End Set
    End Property


    Private Sub MoireImageLoad(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub WriteMifFile(ByVal mifPath As String)
        Using fs As New FileStream(mifPath, FileMode.Create, FileAccess.Write)
            Using bw As New BinaryWriter(fs)
                Dim ms As New MemoryStream()

                'Save main image
                _image.Save(ms, Imaging.ImageFormat.Png)
                Dim imageBytes As Byte() = ms.ToArray()
                ms.SetLength(0)

                'Save thumbnail
                _thumbnail.Save(ms, Imaging.ImageFormat.Png)
                Dim thumbnailBytes As Byte() = ms.ToArray()
                ms.SetLength(0)

                Dim jsonBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(_jsonString)

                bw.Write(System.Text.Encoding.UTF8.GetBytes("MIF1")) ' Magic identifier
                bw.Write(imageBytes.Length)
                bw.Write(imageBytes)
                bw.Write(thumbnailBytes.Length)
                bw.Write(thumbnailBytes)
                bw.Write(jsonBytes.Length)
                bw.Write(jsonBytes)
            End Using
        End Using
    End Sub

    'Public Sub ReadMifFile(ByVal mifPath As String, ByVal extractImagePath As String, ByVal extractThumbnailPath As String, ByVal extractJsonPath As String)
    Public Sub ReadMifFile(ByVal mifPath As String)
        Using fs As New FileStream(mifPath, FileMode.Open, FileAccess.Read)
            Using br As New BinaryReader(fs)
                Dim magicBytes As String = System.Text.Encoding.UTF8.GetString(br.ReadBytes(4))
                If magicBytes <> "MIF1" Then Throw New Exception("Invalid MIF file format")

                ' Read and save the main image
                Dim imageSize As Integer = br.ReadInt32()
                Dim imageBytes As Byte() = br.ReadBytes(imageSize)
                'File.WriteAllBytes(extractImagePath, imageBytes)
                Using ms As New MemoryStream(imageBytes)
                    _image = Image.FromStream(ms)
                End Using

                ' Read and save the thumbnail
                Dim thumbnailSize As Integer = br.ReadInt32()
                Dim thumbnailBytes As Byte() = br.ReadBytes(thumbnailSize)
                'File.WriteAllBytes(extractThumbnailPath, thumbnailBytes)
                Using ms As New MemoryStream(thumbnailBytes)
                    _thumbnail = Image.FromStream(ms)
                End Using

                Dim jsonSize As Integer = br.ReadInt32()
                Dim jsonBytes As Byte() = br.ReadBytes(jsonSize)
                'File.WriteAllBytes(extractJsonPath, jsonBytes)
                _jsonString = System.Text.Encoding.UTF8.GetString(jsonBytes)
            End Using
        End Using
    End Sub

    Private Sub HandleImageAvailable(img)
        _image = img
        _thumbnail = ResizeImage(img, 100, 75, True)

        Console.WriteLine($"_image.Width:{_image.Width}, _thumbnail.Width:{_thumbnail.Width}")

        Me.Invalidate()

        QuickTest()

    End Sub

    Private Sub QuickTest()
        _mifData.ImageName = $"{Format(Now, "yyyy_MM_dd_HHmmss")}"
        _mifData.ImageDate = Now
        _jsonString = JsonSerializer.Serialize(_mifData)
        WriteMifFile($"{Application.StartupPath}\mifs\{_mifData.ImageName}.mif")
        ReadMifFile($"{Application.StartupPath}\mifs\{_mifData.ImageName}.mif")
        RaiseEvent MoireImage_Created()
    End Sub

    Private Sub MoireImageFilePaint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        PaintImage(e)
    End Sub

    Private Sub PaintImage(e As PaintEventArgs)
        If _image IsNot Nothing Then
            Dim width As Integer = Me.Width
            Dim height As Integer = Me.Height
            Dim imgWidth As Integer = _image.Width
            Dim imgHeight As Integer = _image.Height

            ' Calculate the aspect ratio
            Dim imgRatio As Double = imgWidth / imgHeight
            Dim panelRatio As Double = width / height
            Dim newWidth, newHeight As Integer

            If panelRatio > imgRatio Then
                ' Panel is wider than the image, fit height
                newHeight = height
                newWidth = CInt(imgRatio * height)
            Else
                ' Panel is taller than the image, fit width
                newWidth = width
                newHeight = CInt(width / imgRatio)
            End If

            ' Center the image in the panel
            Dim x As Integer = (width - newWidth) \ 2
            Dim y As Integer = (height - newHeight) \ 2

            e.Graphics.DrawImage(_image, x, y, newWidth, newHeight)
        End If
    End Sub

    Private Sub MoireImageFileResize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.Invalidate()
    End Sub

    Private Function ResizeImage(originalImage As Image, new_width As Integer, new_height As Integer, keepAspectRatio As Boolean) As Image
        Dim target_width As Integer = new_width
        Dim target_height As Integer = new_height

        If keepAspectRatio Then
            Dim ratioX As Double = CDbl(new_width) / originalImage.Width
            Dim ratioY As Double = CDbl(new_height) / originalImage.Height
            Dim ratio As Double = Math.Min(ratioX, ratioY)

            target_width = CInt(originalImage.Width * ratio)
            target_height = CInt(originalImage.Height * ratio)
        End If

        Dim new_bitmap As New Bitmap(target_width, target_height)

        Using g As Graphics = Graphics.FromImage(new_bitmap)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
            g.DrawImage(originalImage, 0, 0, target_width, target_height)
        End Using

        Return new_bitmap
    End Function



    Private Sub MoireImage_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        If e.Button = e.Button.Right Then
            ContextMenuStrip1.Show()
            ContextMenuStrip1.Left = MousePosition.X
            ContextMenuStrip1.Top = MousePosition.Y
        End If
    End Sub

    Private Sub HelloWorldToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelloWorldToolStripMenuItem.Click
        MsgBox($"{Now} Hello World!")
    End Sub

End Class