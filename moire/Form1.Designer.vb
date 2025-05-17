<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PnlApp = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'PnlApp
        '
        Me.PnlApp.Location = New System.Drawing.Point(12, 12)
        Me.PnlApp.Name = "PnlApp"
        Me.PnlApp.Size = New System.Drawing.Size(776, 426)
        Me.PnlApp.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.PnlApp)
        Me.Name = "Form1"
        Me.Text = "moire"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PnlApp As Panel
End Class
