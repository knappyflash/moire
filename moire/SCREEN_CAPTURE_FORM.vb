Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Threading

Public Class SCREEN_CAPTURE_FORM

    Private mousePosition As Point = Me.PointToClient(Cursor.Position)
    Private orangePenThick As New Pen(Color.Orange, 10)
    Private orangePenThin As New Pen(Color.Orange, 2)

    Private ScreenIndexSelected As Integer = 0

    Private ScreenShotImg As Image

    Private captureAreaX As Integer
    Private captureAreaY As Integer
    Private captureAreaWidth As Integer = 0
    Private captureAreaHeight As Integer = 0

    Private cropRectX As Integer
    Private cropRectY As Integer
    Private cropRectWidth As Integer
    Private cropRectHeight As Integer
    Private startPointX As Integer
    Private StartPointY As Integer

    Private FreezeScreens As New Dictionary(Of Integer, FREEZE_WINDOW_SCREEN_IMAGE_FORM_OBJECT)

    Public Event Capture_Image_Available(img As Image)

    Private Enum _PaintAction
        Do_Nothing = 0
        Screenshot_Started = 1
        Take_Screenshot = 2
    End Enum

    Private Sub SCREEN_CAPTURE_FORM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Set Window
        Me.SizeGripStyle = SizeGripStyle.Hide
        Me.MinimizeBox = False
        Me.MaximizeBox = False
        Me.ShowInTaskbar = False
        Me.ShowIcon = False
        Me.ControlBox = False
        Me.DoubleBuffered = True
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None
        Me.TopMost = True
        Me.Bounds = Screen.AllScreens(ScreenIndexSelected).Bounds
        Me.BackColor = Color.FromArgb(red:=1, green:=1, blue:=1)
        Me.TransparencyKey = Color.FromArgb(red:=1, green:=1, blue:=1)
        Me.Opacity = 1
        Me.Refresh()


        Create_Freeze_Window_Screens()


        ''Set DrawLoopTimer
        DrawLoopTimer.Interval = 16
        DrawLoopTimer.Start()

        AddHandler ioListener.Released_Escape, AddressOf Handle_Key_Escape_Release

    End Sub

    Private Sub SCREEN_CAPTURE_FORM_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub

    Private Sub DrawLoopTimer_Tick(sender As Object, e As EventArgs) Handles DrawLoopTimer.Tick
        Dim MouseScreenIndex As Integer = GetScreenIndexOfMouse()
        If MouseScreenIndex <> ScreenIndexSelected Then
            Switch_To_Screen(MouseScreenIndex)
            ScreenIndexSelected = MouseScreenIndex
            Console.WriteLine($"{Now} Screen Index Selected: {ScreenIndexSelected}")
        End If
        mousePosition = Me.PointToClient(Cursor.Position)

        Me.Invalidate()
        Application.DoEvents()
    End Sub



    Dim Paint_Action As Integer = _PaintAction.Do_Nothing

    Private Sub MyInvalidater(PaintAction As _PaintAction)
        Paint_Action = PaintAction
        Me.Invalidate()
        My.Application.DoEvents()
    End Sub
    Private Sub SCREEN_CAPTURE_FORM_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint

        Select Case Paint_Action
            Case _PaintAction.Do_Nothing
            Case _PaintAction.Screenshot_Started
                Draw_Mouse_Border_Animation(e)
                Draw_Mouse_Crosshair(e)
            Case _PaintAction.Take_Screenshot
                Draw_Area_To_Crop(e)
        End Select

    End Sub

    Private BorderAnimationCounter As Integer = 0
    Private Sub Draw_Mouse_Border_Animation(e As PaintEventArgs)

        Dim line_length As Integer = 20
        Dim x1 As Integer = 0
        Dim y1 As Integer = 0
        Dim x2 As Integer = 0
        Dim y2 As Integer = 0
        Dim width As Integer = Me.Width
        Dim height As Integer = Me.Height
        Dim CountDown As Integer

        BorderAnimationCounter = BorderAnimationCounter + 1
        If BorderAnimationCounter > 40 Then BorderAnimationCounter = 0

        For i = 0 To Me.Width / (line_length * 2)


            '(Add BorderCounter Move Increase)
            'Top Line
            x1 = ((line_length * 2) * i) + BorderAnimationCounter
            x2 = (((line_length * 2) * i) + line_length) + BorderAnimationCounter
            y1 = 0
            y2 = 0
            e.Graphics.DrawLine(orangePenThick, x1, y1, x2, y2)

            'Right Line
            x1 = width
            x2 = width
            y1 = ((line_length * 2) * i) + BorderAnimationCounter
            y2 = (((line_length * 2) * i) + line_length) + BorderAnimationCounter
            e.Graphics.DrawLine(orangePenThick, x1, y1, x2, y2)


            '(Subtract BorderCounter Move Decrease)
            'Left Line 
            x1 = 0
            x2 = 0
            y1 = ((line_length * 2) * i) - BorderAnimationCounter
            y2 = (((line_length * 2) * i) + line_length) - BorderAnimationCounter
            e.Graphics.DrawLine(orangePenThick, x1, y1, x2, y2)

            'Bottom Line
            x1 = ((line_length * 2) * i) - BorderAnimationCounter
            x2 = (((line_length * 2) * i) + line_length) - BorderAnimationCounter
            y1 = height
            y2 = height
            e.Graphics.DrawLine(orangePenThick, x1, y1, x2, y2)

        Next

    End Sub

    Private Sub Draw_Mouse_Crosshair(e As PaintEventArgs)

        Dim x1 As Integer = 0
        Dim y1 As Integer = 0
        Dim x2 As Integer = 0
        Dim y2 As Integer = 0
        Dim width As Integer = Me.Width
        Dim height As Integer = Me.Height

        'Mouse Line Top To Bottom Crosshair
        x1 = mousePosition.X
        x2 = mousePosition.X
        y1 = 0
        y2 = height
        e.Graphics.DrawLine(orangePenThin, x1, y1, x2, y2)

        'Mouse Right To Left Crosshair
        x1 = 0
        x2 = width
        y1 = mousePosition.Y
        y2 = mousePosition.Y
        e.Graphics.DrawLine(orangePenThin, x1, y1, x2, y2)

    End Sub

    Private Function GetScreenIndexOfMouse() As Integer
        Dim mousePosition As Point = Cursor.Position

        For i As Integer = 0 To Screen.AllScreens.Length - 1
            If Screen.AllScreens(i).Bounds.Contains(mousePosition) Then
                Return i ' Returns the index of the screen
            End If
        Next

        Return -1 ' Returns -1 if not found (shouldn't happen in normal cases)
    End Function
    Private Sub Switch_To_Screen(ByVal ScreenIndex As Integer)
        Me.WindowState = FormWindowState.Normal
        Me.Bounds = Screen.AllScreens(ScreenIndex).Bounds
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None
    End Sub


    ''' Take the ScreenShot
    Private Sub SCREEN_CAPTURE_FORM_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown

        cropRectX = mousePosition.X
        cropRectY = mousePosition.Y
        startPointX = cropRectX
        StartPointY = cropRectY

        MyInvalidater(_PaintAction.Take_Screenshot)

        Me.Hide()
        Dim currentScreen = Screen.AllScreens(ScreenIndexSelected).Bounds
        Dim bmp As New Bitmap(currentScreen.Width, currentScreen.Height)

        Using g As Graphics = Graphics.FromImage(bmp)
            g.CopyFromScreen(currentScreen.Location, Point.Empty, currentScreen.Size)
        End Using

        ScreenShotImg = bmp

        Me.Show()

    End Sub

    Private Sub SCREEN_CAPTURE_FORM_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        Crop_Image()
        RaiseEvent Capture_Image_Available(ScreenShotImg)
        Close_All_FreezeScreens()
        Me.Close()
    End Sub

    Private Sub Draw_Area_To_Crop(e As PaintEventArgs)

        cropRectX = Math.Min(startPointX, mousePosition.X)
        cropRectY = Math.Min(startPointY, mousePosition.Y)
        cropRectWidth = Math.Abs(mousePosition.X - startPointX)
        cropRectHeight = Math.Abs(mousePosition.Y - startPointY)

        e.Graphics.DrawRectangle(orangePenThin, cropRectX, cropRectY, cropRectWidth, cropRectHeight)

    End Sub

    Private Sub Crop_Image()

        Dim cropRect As Rectangle
        Dim croppedBmp As Bitmap

        Try
            cropRect = New Rectangle(cropRectX, cropRectY, cropRectWidth, cropRectHeight)
            croppedBmp = New Bitmap(cropRect.Width, cropRect.Height)
        Catch ex As Exception
            cropRect = New Rectangle(0, 0, Me.Width, Me.Height)
            croppedBmp = New Bitmap(cropRect.Width, cropRect.Height)
        End Try

        Using g As Graphics = Graphics.FromImage(croppedBmp)
            g.DrawImage(ScreenShotImg, New Rectangle(0, 0, cropRect.Width, cropRect.Height), cropRect, GraphicsUnit.Pixel)
        End Using

        ScreenShotImg = croppedBmp
    End Sub

    Private Sub Handle_Key_Escape_Release()
        Close_All_FreezeScreens()
        Me.Close()
    End Sub

    Private Sub Create_Freeze_Window_Screens()

        MyInvalidater(_PaintAction.Do_Nothing)

        Console.WriteLine($"{Now} Creating_Freeze_Window_Screens!")

        Dim screens() As Screen = Screen.AllScreens

        ' Output sorted monitors based on Physical position Left to Right
        'Dim sortedScreens() As Screen = screens.OrderBy(Function(s) s.Bounds.X).ToArray()

        ''' Create The Collections Of FREEZE_WINDOW_IMAGE_OBJECTS WITH EACH OBJECT HAVING A SCREEN SHOT OF THAT SCREEN BEHIND THIS ME.OBJECT'''
        Dim ScreenCounter As Integer = 0
        For i As Integer = 0 To screens.Length - 1
            Dim currentScreen As Screen = screens(i)
            Console.WriteLine($"   Monitor {i}: {currentScreen.DeviceName}")
            Console.WriteLine($"   Resolution: {currentScreen.Bounds.Width} x {currentScreen.Bounds.Height}")
            Console.WriteLine($"   Working Area: {currentScreen.WorkingArea.Width} x {currentScreen.WorkingArea.Height}")
            Console.WriteLine($"   X Position: {currentScreen.Bounds.X}")
            Console.WriteLine()

            FreezeScreens.Add(ScreenCounter, New FREEZE_WINDOW_SCREEN_IMAGE_FORM_OBJECT)
            FreezeScreens(i).MyScreen = currentScreen
            FreezeScreens(i).ThisScreenIndex = i
            FreezeScreens(i).Show()

            ScreenCounter = ScreenCounter + 1

        Next

        MyInvalidater(_PaintAction.Screenshot_Started)

    End Sub

    Private Sub Close_All_FreezeScreens()
        For Each kvp As KeyValuePair(Of Integer, FREEZE_WINDOW_SCREEN_IMAGE_FORM_OBJECT) In FreezeScreens
            FreezeScreens(kvp.Key).Close()
        Next
        FreezeScreens.Clear()
    End Sub

    Private Sub Use_Entier_Screen()
        Dim x As Integer = 0
        Dim y As Integer = 0

        Dim width As Integer = Me.Width
        Dim height As Integer = Me.Height

        Dim cropRect As New Rectangle(x, y, width, height)
        Dim croppedBmp As New Bitmap(cropRect.Width, cropRect.Height)

        Using g As Graphics = Graphics.FromImage(croppedBmp)
            g.DrawImage(ScreenShotImg, New Rectangle(0, 0, cropRect.Width, cropRect.Height), cropRect, GraphicsUnit.Pixel)
        End Using

        ScreenShotImg = croppedBmp
    End Sub

End Class