Imports System.Runtime.InteropServices

Public Class MAIN_APP_FORM

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




    Private _MyScreenCapture As SCREEN_CAPTURE_FORM
    Private Sub MAIN_APP_FORM_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        HookDelegate = New HookProc(AddressOf KeyboardHookProc)
        hHook = SetWindowsHookEx(WH_KEYBOARD_LL, HookDelegate, GetModuleHandle(Nothing), 0)

        Me.SizeGripStyle = SizeGripStyle.Hide
        Me.MinimizeBox = False
        Me.MaximizeBox = False
        Me.ShowInTaskbar = False
        Me.ShowIcon = False
        Me.ControlBox = False
        Me.DoubleBuffered = True
        Me.WindowState = FormWindowState.Normal
        Me.FormBorderStyle = FormBorderStyle.None
        Me.TopMost = False
        Me.TopLevel = False
        Me.Refresh()

    End Sub

    Public Sub Set_MyScreenCapture(MyScreenCapture As SCREEN_CAPTURE_FORM)
        _MyScreenCapture = MyScreenCapture
        AddHandler _MyScreenCapture.Capture_Image_Available, AddressOf Handle_Image_Available
    End Sub

    Private Function KeyboardHookProc(nCode As Integer, wParam As IntPtr, lParam As IntPtr) As Integer
        If wParam = CType(WM_KEYUP, IntPtr) Then
            Dim vkCode As Integer = Marshal.ReadInt32(lParam)
            If vkCode = Keys.PrintScreen Then
                Console.WriteLine("Print Screen Key Realsed")
                SCREEN_CAPTURE_FORM.Show()
                Set_MyScreenCapture(SCREEN_CAPTURE_FORM)
            End If
        End If
        Return CallNextHookEx(hHook, nCode, wParam, lParam)
    End Function

    Private Sub Handle_Image_Available(img)
        PictureBox1.Image = img
    End Sub

End Class