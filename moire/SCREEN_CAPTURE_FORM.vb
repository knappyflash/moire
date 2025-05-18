Imports System.ComponentModel
Imports System.Runtime.InteropServices

Public Class SCREEN_CAPTURE_FORM


    ''' Listen For Key Press
    Public Delegate Function HookProc(nCode As Integer, wParam As IntPtr, lParam As IntPtr) As Integer
    Public Shared HookDelegate As HookProc

    <DllImport("user32.dll")>
    Private Shared Function SetWindowsHookEx(idHook As Integer, lpfn As HookProc, hMod As IntPtr, dwThreadId As Integer) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function UnhookWindowsHookEx(hHook As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function CallNextHookEx(hHook As IntPtr, nCode As Integer, wParam As IntPtr, lParam As IntPtr) As Integer
    End Function

    <DllImport("kernel32.dll")>
    Private Shared Function GetModuleHandle(lpModuleName As String) As IntPtr
    End Function

    Private Const WM_KEYUP As Integer = &H101
    Private Const WH_KEYBOARD_LL As Integer = 13



    ' Declare a field to hold the delegate, preventing it from being garbage collected
    Private Shared hHook As IntPtr = IntPtr.Zero
    Private keyboardHookDelegate As HookProc
    Private Sub SetKeyboardHook()
        keyboardHookDelegate = AddressOf KeyboardHookProc ' Assign delegate before hooking
        hHook = SetWindowsHookEx(WH_KEYBOARD_LL, keyboardHookDelegate, IntPtr.Zero, 0)
    End Sub








    Private mousePosition As Point = Me.PointToClient(Cursor.Position)
    Private orangePenThick As New Pen(Color.Orange, 10)
    Private orangePenThin As New Pen(Color.Orange, 2)

    Private ScreenIndexSelected As Integer = 0

    Private ScreenShotImg As Image

    Private captureAreaTopLeft As Point = New Point(0, 0)
    Private captureAreaBottomRight As Point = New Point(0, 0)


    Public Event Capture_Image_Available(img As Image)

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
        'Me.TransparencyKey = Color.FromArgb(red:=1, green:=1, blue:=1)
        Me.Opacity = 0.3
        Me.Refresh()

        'Set DrawLoopTimer
        DrawLoopTimer.Interval = 16
        DrawLoopTimer.Start()

        ''Set PicBoxBgNoClick
        'PicBoxBgNoCLick.BackColor = Color.FromArgb(red:=1, blue:=1, green:=1)
        'PicBoxBgNoCLick.Top = Me.Top
        'PicBoxBgNoCLick.Left = Me.Left
        'PicBoxBgNoCLick.Width = Me.Width
        'PicBoxBgNoCLick.Height = Me.Height

        'Listen For Keyboard Hooks
        HookDelegate = New HookProc(AddressOf KeyboardHookProc)
        hHook = SetWindowsHookEx(WH_KEYBOARD_LL, HookDelegate, GetModuleHandle(Nothing), 0)
    End Sub

    Private Sub SCREEN_CAPTURE_FORM_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub

    Private Sub DrawLoopTimer_Tick(sender As Object, e As EventArgs) Handles DrawLoopTimer.Tick
        Dim MouseScreenIndex As Integer = GetScreenIndexOfMouse()
        If MouseScreenIndex <> ScreenIndexSelected Then
            Switch_To_Screen(MouseScreenIndex)
            ScreenIndexSelected = MouseScreenIndex
        End If
        mousePosition = Me.PointToClient(Cursor.Position)
        Me.Invalidate()
        Application.DoEvents()
    End Sub



    Dim Paint_Action As String = "Screenshot Started"
    Private Sub SCREEN_CAPTURE_FORM_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint

        Select Case Paint_Action
            Case "Screenshot Started"
                Draw_Mouse_Border_Animation(e)
                Draw_Mouse_Crosshair(e)
            Case "Take Screenshot"
                Draw_Screenshot_Image(e)
                Draw_Area_To_Crop(e)
        End Select

    End Sub

    Private Sub Draw_Screenshot_Image(e As PaintEventArgs)

        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim width As Integer = Me.Width
        Dim height As Integer = Me.Height
        e.Graphics.DrawImage(ScreenShotImg, x, y, width, height)

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

    Private Function KeyboardHookProc(nCode As Integer, wParam As IntPtr, lParam As IntPtr) As Integer
        Try
            If wParam = CType(WM_KEYUP, IntPtr) Then
                Dim vkCode As Integer = Marshal.ReadInt32(lParam)
                If vkCode = Keys.Escape Then
                    Console.WriteLine("Escape Key Realsed")
                    Me.Close()
                End If
            End If
            Return CallNextHookEx(hHook, nCode, wParam, lParam)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

    End Function

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

        captureAreaTopLeft = mousePosition

        Paint_Action = "Take Screenshot"
        Me.Invalidate()
        Me.Opacity = 0
        Dim currentScreen = Screen.AllScreens(ScreenIndexSelected).Bounds
        Dim bmp As New Bitmap(currentScreen.Width, currentScreen.Height)

        Using g As Graphics = Graphics.FromImage(bmp)
            g.CopyFromScreen(currentScreen.Location, Point.Empty, currentScreen.Size)
        End Using

        ScreenShotImg = bmp

        Me.Opacity = 1

    End Sub

    Private Sub SCREEN_CAPTURE_FORM_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        captureAreaBottomRight = mousePosition
        Crop_Image()
        RaiseEvent Capture_Image_Available(ScreenShotImg)
        Me.Dispose()
    End Sub

    Private Sub Draw_Area_To_Crop(e As PaintEventArgs)
        Dim x As Integer = captureAreaTopLeft.X
        Dim y As Integer = captureAreaTopLeft.Y
        Dim width As Integer = mousePosition.X - x
        Dim height As Integer = mousePosition.Y - y
        e.Graphics.DrawRectangle(orangePenThin, x, y, width, height)
    End Sub

    Private Sub Crop_Image()
        Dim x As Integer = captureAreaTopLeft.X
        Dim y As Integer = captureAreaTopLeft.Y
        Dim width As Integer = captureAreaBottomRight.X - x
        Dim height As Integer = captureAreaBottomRight.Y - y

        Dim cropRect As New Rectangle(x, y, width, height)
        Dim croppedBmp As New Bitmap(cropRect.Width, cropRect.Height)

        Using g As Graphics = Graphics.FromImage(croppedBmp)
            g.DrawImage(ScreenShotImg, New Rectangle(0, 0, cropRect.Width, cropRect.Height), cropRect, GraphicsUnit.Pixel)
        End Using

        ScreenShotImg = croppedBmp
    End Sub

End Class