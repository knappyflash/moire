''' <summary>
''' Creaing IO Listener so it can stay alive and pass the keyboard and mouse inputs to other objects.
''' Instead of the object being disposed and having hook callback issues
''' </summary>
''' 

Public Class ioListener

    Private Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal vKey As Integer) As Short

    Public Event Released_Escape()
    Public Event Released_PrintScreen()
    Public Event Released_LMouse()
    Public Event Released_RMouse()

    Private Sub ioListener_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Private EscapeStillDown As Boolean = False
    Private PrintScreenStillDown As Boolean = False
    Private LeftMouseStillDown As Boolean = False
    Private RightMouseStillDown As Boolean = False

    Public Sub Start_Timer()
        Timer1.Start()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Dim EscapeCurrentState As Boolean = GetAsyncKeyState(Keys.Escape) <> 0
        Dim PrintScreenCurrentState As Boolean = GetAsyncKeyState(Keys.PrintScreen) <> 0
        Dim LeftMouseCurrentState As Boolean = GetAsyncKeyState(Keys.LButton) <> 0
        Dim RightMouseCurrentState As Boolean = GetAsyncKeyState(Keys.RButton) <> 0

        If EscapeCurrentState Then EscapeStillDown = True
        If PrintScreenCurrentState Then PrintScreenStillDown = True
        If LeftMouseCurrentState Then LeftMouseStillDown = True
        If RightMouseCurrentState Then RightMouseStillDown = True

        If EscapeStillDown And Not EscapeCurrentState Then
            RaiseEvent Released_Escape()
            EscapeStillDown = False
        End If

        If PrintScreenStillDown And Not PrintScreenCurrentState Then
            RaiseEvent Released_PrintScreen()
            PrintScreenStillDown = False
        End If

        If LeftMouseStillDown And Not LeftMouseCurrentState Then
            RaiseEvent Released_LMouse()
            LeftMouseStillDown = False
        End If

        If RightMouseStillDown And Not RightMouseCurrentState Then
            RaiseEvent Released_RMouse()
            RightMouseStillDown = False
        End If

    End Sub

End Class