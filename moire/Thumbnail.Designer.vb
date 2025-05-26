<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Thumbnail
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
        Me.thumbnailPictureBox = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.thumbnailPanel = New System.Windows.Forms.Panel()
        CType(Me.thumbnailPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.thumbnailPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'thumbnailPictureBox
        '
        Me.thumbnailPictureBox.BackColor = System.Drawing.Color.White
        Me.thumbnailPictureBox.Location = New System.Drawing.Point(0, 0)
        Me.thumbnailPictureBox.Margin = New System.Windows.Forms.Padding(0)
        Me.thumbnailPictureBox.Name = "thumbnailPictureBox"
        Me.thumbnailPictureBox.Size = New System.Drawing.Size(150, 75)
        Me.thumbnailPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.thumbnailPictureBox.TabIndex = 0
        Me.thumbnailPictureBox.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 75)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "thumbnailNameLabel"
        '
        'thumbnailPanel
        '
        Me.thumbnailPanel.Controls.Add(Me.thumbnailPictureBox)
        Me.thumbnailPanel.Controls.Add(Me.Label1)
        Me.thumbnailPanel.Location = New System.Drawing.Point(9, 9)
        Me.thumbnailPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.thumbnailPanel.Name = "thumbnailPanel"
        Me.thumbnailPanel.Size = New System.Drawing.Size(147, 90)
        Me.thumbnailPanel.TabIndex = 2
        '
        'Thumbnail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(167, 108)
        Me.ControlBox = False
        Me.Controls.Add(Me.thumbnailPanel)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Thumbnail"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Thumbnail"
        CType(Me.thumbnailPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.thumbnailPanel.ResumeLayout(False)
        Me.thumbnailPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents thumbnailPictureBox As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents thumbnailPanel As Panel
End Class
