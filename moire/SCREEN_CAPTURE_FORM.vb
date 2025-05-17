Imports System.ComponentModel
Imports System.Runtime.InteropServices

Public Class SCREEN_CAPTURE_FORM


    '''' These are to allow mouse clicks through the form
    '<DllImport("user32.dll", SetLastError:=True)>
    'Private Shared Function SetWindowLong(hWnd As IntPtr, nIndex As Integer, dwNewLong As Integer) As Integer
    'End Function

    '<DllImport("user32.dll", SetLastError:=True)>
    'Private Shared Function GetWindowLong(hWnd As IntPtr, nIndex As Integer) As Integer
    'End Function

    'Protected Overrides Sub OnShown(e As EventArgs)
    '    MyBase.OnShown(e)
    '    Set_Window()
    'End Sub

    'Private Sub Set_Window()
    '    Dim exStyle As Integer = GetWindowLong(Me.Handle, GWL_EXSTYLE)
    '    SetWindowLong(Me.Handle, GWL_EXSTYLE, exStyle Or WS_EX_TRANSPARENT Or WS_EX_LAYERED)
    'End Sub
    'Private Const GWL_EXSTYLE As Integer = -20
    'Private Const WS_EX_TRANSPARENT As Integer = &H20
    'Private Const WS_EX_LAYERED As Integer = &H80000




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
    Private Shared hHook As IntPtr = IntPtr.Zero









    Private mousePosition As Point = Me.PointToClient(Cursor.Position)
    Private orangePenThick As New Pen(Color.Orange, 10)
    Private orangePenThin As New Pen(Color.Orange, 2)


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
        Me.Bounds = Screen.AllScreens(0).Bounds
        Me.BackColor = Color.FromArgb(red:=1, green:=1, blue:=1)
        Me.TransparencyKey = Color.FromArgb(red:=1, green:=1, blue:=1)
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
        mousePosition = Me.PointToClient(Cursor.Position)
        Me.Invalidate()
        Application.DoEvents()
    End Sub

    Private Sub SCREEN_CAPTURE_FORM_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Draw_Mouse_Crosshair(e)
        Draw_Mouse_Border_Animation(e)
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
        If wParam = CType(WM_KEYUP, IntPtr) Then
            Dim vkCode As Integer = Marshal.ReadInt32(lParam)
            If vkCode = Keys.Escape Then
                Console.WriteLine("Escape Key Realsed")
                Me.Close()
            End If
        End If
        Return CallNextHookEx(hHook, nCode, wParam, lParam)
    End Function

End Class