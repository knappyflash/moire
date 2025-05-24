''' Save current .png file and .json file to the temp folder as temp.png and temp.json
''' After that create the .mif file from the temp.png and temp.json files. and save the .mif to the mifs folder
''' 
''' This object will store all the images for this mif image, the available thumbnail for the filmstrip selector,
''' the added shapes, text, highlights, ect,
''' the path of the .mif file,
''' basically all info related to the .mif file
''' 



Imports System.IO
Public Class moire_image_file

    Private _MyScreenCapture As SCREEN_CAPTURE_FORM

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

    End Sub

    Public Sub WriteMifFile(ByVal imagePath As String, ByVal jsonPath As String, ByVal mifPath As String)
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

    Public Function Generate_File_Name() As String
        Return $"mif_{Format(Now, "mmyydd_hhmmss")}.mif"
    End Function

    Private Sub Handle_Image_Available(img)
        _image = img
        Me.Invalidate()
        WriteTempPng()
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