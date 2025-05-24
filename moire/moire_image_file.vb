''' Save current .png file and .json file to the temp folder as temp.png and temp.json
''' After that create the .mif file from the temp.png and temp.json files. and save the .mif to the mifs folder


Imports System.IO
Public Class moire_image_file
    Private Sub moire_image_file_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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



End Class