Imports System.IO

Public Class Thumbnail

    Public thumbnailName As String
    Public thumbnailImage As Image
    Public mifFilePath As String
    Private _isThumbnailSelected As Boolean

    Public Event Thumbnail_Selected(mifPath As String)

    Public Property IsThumbnailSelected As Boolean
        Get
            Return _isThumbnailSelected
        End Get
        Set(value As Boolean)
            _isThumbnailSelected = value
            Me.Invalidate()
        End Set
    End Property
    Private Sub Thumbnail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        thumbnailPictureBox.Image = thumbnailImage
        Label1.Text = thumbnailName
    End Sub

    Public Sub ReadMifFile(ByVal mifPath As String)
        mifFilePath = mifPath
        thumbnailName = Path.GetFileName(mifFilePath).Replace(".mif", "")

        Using fs As New FileStream(mifFilePath, FileMode.Open, FileAccess.Read)
            Using br As New BinaryReader(fs)
                Dim magicBytes As String = System.Text.Encoding.UTF8.GetString(br.ReadBytes(4))
                If magicBytes <> "MIF1" Then Throw New Exception("Invalid MIF file format")
                ' Read and save the thumbnail
                Dim thumbnailSize As Integer = br.ReadInt32()
                Dim thumbnailBytes As Byte() = br.ReadBytes(thumbnailSize)
                Using ms As New MemoryStream(thumbnailBytes)
                    thumbnailImage = Image.FromStream(ms)
                End Using

            End Using
        End Using
    End Sub

    Private Sub Thumbnail_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        If _isThumbnailSelected Then
            RaiseEvent Thumbnail_Selected(mifFilePath)
            Dim borderWidth As Integer = 5 ' Adjust the thickness as needed
            Using pen As New Pen(Color.Red, borderWidth)
                e.Graphics.DrawRectangle(pen, New Rectangle(0, 0, Me.ClientSize.Width - 1, Me.ClientSize.Height - 1))
            End Using
        End If
    End Sub
    Private Sub thumbnailPictureBox_MouseUp(sender As Object, e As MouseEventArgs) Handles thumbnailPictureBox.MouseUp
        If e.Button = e.Button.Left Then
            IsThumbnailSelected = Not IsThumbnailSelected
        Else

        End If
    End Sub
End Class