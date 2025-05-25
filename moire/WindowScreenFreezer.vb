Imports System.Drawing.Printing

Public Class WindowScreenFreezer

    Public ThisScreenIndex As Integer
    Public MyScreen As Screen
    Private capturedBmp As Bitmap ' Store the screenshot

    Private Sub FREEZE_WINDOW_SCREEN_IMAGE_FORM_OBJECT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Top = 0
        Me.Left = MyScreen.Bounds.X
        Me.Width = MyScreen.Bounds.Width
        Me.Height = MyScreen.Bounds.Height
        Draw_Screenshot_Image()
    End Sub

    Private Sub SCREEN_CAPTURE_FORM_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        If capturedBmp IsNot Nothing Then
            e.Graphics.DrawImage(capturedBmp, 0, 0) ' Draw the image onto the form
        End If
    End Sub

    Private Sub Draw_Screenshot_Image()
        Me.Hide()
        Dim currentScreen = Screen.AllScreens(ThisScreenIndex).Bounds
        capturedBmp = New Bitmap(currentScreen.Width, currentScreen.Height)

        Using g As Graphics = Graphics.FromImage(capturedBmp)
            g.CopyFromScreen(currentScreen.Location, Point.Empty, currentScreen.Size)
        End Using

        Me.Show()
        Me.Invalidate() ' Trigger repaint to show the captured image
    End Sub

End Class
