Imports System.ComponentModel

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        QuickTest()



        'ioListener.Timer1.Start()

        'MAIN_APP_FORM.TopLevel = False
        'PnlApp.Controls.Add(MAIN_APP_FORM)
        'MAIN_APP_FORM.Dock = DockStyle.Fill
        'MAIN_APP_FORM.Show()

    End Sub
    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        End
    End Sub

    Private Sub QuickTest()
        Dim mif As New moire_image_file

        Dim mifsPath As String = $"{Application.StartupPath}\mifs"

        mif.WriteMifFile($"{mifsPath}\Sheila Blocks.png", $"{mifsPath}\helloworld.json", $"{mifsPath}\firstMif.mif")
        mif.ReadMifFile($"{mifsPath}\firstMif.mif", $"{mifsPath}\test1.png", $"{mifsPath}\test1.json")

    End Sub

End Class
