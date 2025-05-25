
Imports System.Text.Json
Imports System.IO

Public Class MoireImage

    Private _myScreenCapture As ScreenCapture
    Private _mifData As New MifData
    Private _image As Image
    Private _thumbnail As Image
    Private _fileName As String
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


    Private Sub MoireImageLoad(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub WriteMifFile(ByVal mifPath As String)
        Using fs As New FileStream(mifPath, FileMode.Create, FileAccess.Write)
            Using bw As New BinaryWriter(fs)
                Dim ms As New MemoryStream()
                _image.Save(ms, Imaging.ImageFormat.Png)
                Dim imageBytes As Byte() = ms.ToArray()
                Dim jsonBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(_jsonString)

                bw.Write(System.Text.Encoding.UTF8.GetBytes("MIF1")) ' Magic identifier
                bw.Write(imageBytes.Length)
                bw.Write(imageBytes)
                bw.Write(jsonBytes.Length)
                bw.Write(jsonBytes)
            End Using
        End Using
    End Sub

    Public Sub ReadMifFile(ByVal mifPath As String, ByVal extractImagePath As String, ByVal extractJsonPath As String)
        Using fs As New FileStream(mifPath, FileMode.Open, FileAccess.Read)
            Using br As New BinaryReader(fs)
                Dim magicBytes As String = System.Text.Encoding.UTF8.GetString(br.ReadBytes(4))
                If magicBytes <> "MIF1" Then Throw New Exception("Invalid MIF file format")

                Dim imageSize As Integer = br.ReadInt32()
                Dim imageBytes As Byte() = br.ReadBytes(imageSize)
                File.WriteAllBytes(extractImagePath, imageBytes)

                Dim jsonSize As Integer = br.ReadInt32()
                Dim jsonBytes As Byte() = br.ReadBytes(jsonSize)
                File.WriteAllBytes(extractJsonPath, jsonBytes)
            End Using
        End Using
    End Sub

    Private Sub HandleImageAvailable(img)
        _image = img
        Me.Invalidate()

        QuickTest()

    End Sub

    Private Sub QuickTest()
        _mifData.ImageName = $"mif_{Format(Now, "mmyydd_hhmmss")}"
        _mifData.ImageDate = Now
        _jsonString = JsonSerializer.Serialize(_mifData)
        WriteMifFile($"{Application.StartupPath}\mifs\{_mifData.ImageName}.mif")
        ReadMifFile($"{Application.StartupPath}\mifs\{_mifData.ImageName}.mif", $"{Application.StartupPath}\mifs\test.png", $"{Application.StartupPath}\mifs\test.json")
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

    Private Function ResizeImage(originalImage As Image, new_width As Integer, new_height As Integer) As Image
        ' This is also to create the thumbnail
        'Dim new_bitmap As New Bitmap()
    End Function

End Class