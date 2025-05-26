<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainApp
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.PnlMIF = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanelTimeLine = New System.Windows.Forms.FlowLayoutPanel()
        Me.PanelTimeLine = New System.Windows.Forms.Panel()
        Me.PanelTimeLine.SuspendLayout()
        Me.SuspendLayout()
        '
        'PnlMIF
        '
        Me.PnlMIF.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlMIF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlMIF.Location = New System.Drawing.Point(15, 37)
        Me.PnlMIF.Name = "PnlMIF"
        Me.PnlMIF.Size = New System.Drawing.Size(773, 281)
        Me.PnlMIF.TabIndex = 1
        '
        'FlowLayoutPanelTimeLine
        '
        Me.FlowLayoutPanelTimeLine.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FlowLayoutPanelTimeLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FlowLayoutPanelTimeLine.Location = New System.Drawing.Point(3, 3)
        Me.FlowLayoutPanelTimeLine.Name = "FlowLayoutPanelTimeLine"
        Me.FlowLayoutPanelTimeLine.Size = New System.Drawing.Size(1286, 83)
        Me.FlowLayoutPanelTimeLine.TabIndex = 2
        '
        'PanelTimeLine
        '
        Me.PanelTimeLine.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelTimeLine.AutoScroll = True
        Me.PanelTimeLine.Controls.Add(Me.FlowLayoutPanelTimeLine)
        Me.PanelTimeLine.Location = New System.Drawing.Point(15, 324)
        Me.PanelTimeLine.Name = "PanelTimeLine"
        Me.PanelTimeLine.Size = New System.Drawing.Size(773, 114)
        Me.PanelTimeLine.TabIndex = 3
        '
        'MainApp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.ControlBox = False
        Me.Controls.Add(Me.PanelTimeLine)
        Me.Controls.Add(Me.PnlMIF)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MainApp"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "MAIN_APP_FORM"
        Me.PanelTimeLine.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PnlMIF As Panel
    Friend WithEvents FlowLayoutPanelTimeLine As FlowLayoutPanel
    Friend WithEvents PanelTimeLine As Panel
End Class
