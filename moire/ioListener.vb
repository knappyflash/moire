''' <summary>
''' Creaing IO Listener so it can stay alive and pass the keyboard and mouse inputs to other objects.
''' Instead of the object being disposed and having hook callback issues
''' </summary>
''' 

Public Class ioListener



    Private Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal vKey As Integer) As Short

    Private Sub ioListener_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub


    Private EscapeWasPressed As Boolean = False
    Private PrintScreenWasPressed As Boolean = False
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Dim EscapeCurrentState As Boolean = GetAsyncKeyState(Keys.Escape) <> 0
        Dim PrintScreenCurrentState As Boolean = GetAsyncKeyState(Keys.PrintScreen) <> 0

        If EscapeCurrentState Then EscapeWasPressed = True
        If PrintScreenCurrentState Then PrintScreenWasPressed = True


        If EscapeWasPressed And Not EscapeCurrentState Then
            Console.WriteLine($"{Now} Escape Was Realesed!")
            EscapeWasPressed = False
        End If

        If PrintScreenWasPressed And Not PrintScreenCurrentState Then
            Console.WriteLine($"{Now} PrintScreen Was Realesed!")
            PrintScreenWasPressed = False
        End If

    End Sub

End Class