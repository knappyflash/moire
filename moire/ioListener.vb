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

    Public Event ClickDown_LMouse()
    Public Event ClickDown_RMouse()


    Private EscapeStillDown As Boolean = False
    Private PrintScreenStillDown As Boolean = False
    Private LeftMouseStillDown As Boolean = False
    Private RightMouseStillDown As Boolean = False

    Private LeftMouseClickDown As Boolean = False
    Private RightMouseClickDown As Boolean = False

    Private Sub ioListener_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub Start_Timer()
        Timer1.Start()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Handle_Keys()
        Handle_Mouse()

    End Sub

    Private Sub Handle_Keys()

        Dim EscapeCurrentState As Boolean = GetAsyncKeyState(Keys.Escape) <> 0
        Dim PrintScreenCurrentState As Boolean = GetAsyncKeyState(Keys.PrintScreen) <> 0

        If EscapeCurrentState Then EscapeStillDown = True
        If PrintScreenCurrentState Then PrintScreenStillDown = True

        ''' RELEASE '''
        If EscapeStillDown And Not EscapeCurrentState Then
            EscapeStillDown = False
            RaiseEvent Released_Escape()
        End If

        If PrintScreenStillDown And Not PrintScreenCurrentState Then
            PrintScreenStillDown = False
            RaiseEvent Released_PrintScreen()
        End If

    End Sub

    Private Sub Handle_Mouse()

        Dim LeftMouseCurrentState As Boolean = GetAsyncKeyState(Keys.LButton) <> 0
        Dim RightMouseCurrentState As Boolean = GetAsyncKeyState(Keys.RButton) <> 0

        If LeftMouseCurrentState Then LeftMouseStillDown = True
        If RightMouseCurrentState Then RightMouseStillDown = True

        ''' FIRST CLICK DOWN ''''
        If Not LeftMouseClickDown And LeftMouseStillDown Then
            LeftMouseClickDown = True
            RaiseEvent ClickDown_LMouse()
        End If
        If Not RightMouseClickDown And RightMouseStillDown Then
            RightMouseClickDown = True
            RaiseEvent ClickDown_RMouse()
        End If

        ''' RELEASE '''
        If LeftMouseStillDown And Not LeftMouseCurrentState Then
            LeftMouseClickDown = False
            LeftMouseStillDown = False
            RaiseEvent Released_LMouse()
        End If
        If RightMouseStillDown And Not RightMouseCurrentState Then
            RightMouseClickDown = False
            RightMouseStillDown = False
            RaiseEvent Released_RMouse()
        End If

    End Sub

End Class