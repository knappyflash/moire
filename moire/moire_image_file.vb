''' Save current .png file and .json file to the temp folder as temp.png and temp.json
''' After that create the .mif file from the temp.png and temp.json files. and save the .mif to the mifs folder
''' 
''' This object will store all the images for this mif image, the available thumbnail for the filmstrip selector,
''' the added shapes, text, highlights, ect,
''' the path of the .mif file,
''' basically all info related to the .mif file
''' 



Imports System.Text.Json
Imports System.IO

Public Class moire_image_file

    Private _MyScreenCapture As SCREEN_CAPTURE_FORM
    Private mifData As New mif_data

    Private _image As Image
    Private _FileName As String

    Public Property ScreenCapture As SCREEN_CAPTURE_FORM
        Get
            Return _MyScreenCapture
        End Get
        Set(value As SCREEN_CAPTURE_FORM)
            _MyScreenCapture = value
            AddHandler _MyScreenCapture.Capture_Image_Available, AddressOf Handle_Image_Available
        End Set
    End Property


    Private Sub moire_image_file_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub WriteTempPng()
        Dim savePath As String = $"{Application.StartupPath}\temp\output.png"
        _image.Save(savePath, Imaging.ImageFormat.Png)
    End Sub

    Public Sub WriteTempJson()
        Console.WriteLine($"mifData.image_name: {mifData.image_name}")
        Dim savePath As String = $"{Application.StartupPath}\temp\output.json"
        Dim jsonString As String = JsonSerializer.Serialize(mifData)
        File.WriteAllText(savePath, jsonString)
    End Sub

    Public Sub WriteMifFile(ByVal mifPath As String)
        Dim imagePath As String = $"{Application.StartupPath}\temp\output.png"
        Dim jsonPath As String = $"{Application.StartupPath}\temp\output.json"
        Using fs As New FileStream(mifPath, FileMode.Create, FileAccess.Write)
            Using bw As New BinaryWriter(fs)
                Dim imageBytes As Byte() = File.ReadAllBytes(imagePath)
                Dim jsonBytes As Byte() = File.ReadAllBytes(jsonPath)

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

    Private Sub Handle_Image_Available(img)
        _image = img
        Me.Invalidate()

        QuickTest()

    End Sub

    Private Sub QuickTest()

        ''' This is for testing the save files. When reading the .mif file the .json and .png should get saved to temp folder instead

        mifData.image_name = $"mif_{Format(Now, "mmyydd_hhmmss")}"
        WriteTempPng()
        WriteTempJson()
        WriteMifFile($"{Application.StartupPath}\mifs\{mifData.image_name}.mif")
        ReadMifFile($"{Application.StartupPath}\mifs\{mifData.image_name}.mif", $"{Application.StartupPath}\mifs\test.png", $"{Application.StartupPath}\mifs\test.json")
    End Sub

    Private Sub moire_image_file_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Paint_Image(e)
    End Sub

    Private Sub Paint_Image(e As PaintEventArgs)
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

    Private Sub moire_image_file_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.Invalidate()
    End Sub
End Class

Public Class mif_data
    Public _image_name As String
    Public _test As New Dictionary(Of String, String)

    Public Sub New()
        _test.Add("Hello", "World")
        _test.Add("TEST2", "TEST2")
    End Sub

    Public Property image_name As String
        Get
            Return _image_name
        End Get
        Set(value As String)
            _image_name = value
        End Set
    End Property

    Public Property test As Dictionary(Of String, String)
        Get
            Return _test
        End Get
        Set(value As Dictionary(Of String, String))
            _test = value
        End Set
    End Property

End Class