<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WatchListForm
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
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

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.FlowLayoutPanelWatchList = New System.Windows.Forms.FlowLayoutPanel()
        Me.moviePanel = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanelWatchList.SuspendLayout()
        Me.SuspendLayout()
        '
        'FlowLayoutPanelWatchList
        '
        Me.FlowLayoutPanelWatchList.AutoScroll = True
        Me.FlowLayoutPanelWatchList.Controls.Add(Me.moviePanel)
        Me.FlowLayoutPanelWatchList.Location = New System.Drawing.Point(12, 12)
        Me.FlowLayoutPanelWatchList.Name = "FlowLayoutPanelWatchList"
        Me.FlowLayoutPanelWatchList.Size = New System.Drawing.Size(1165, 332)
        Me.FlowLayoutPanelWatchList.TabIndex = 0
        Me.FlowLayoutPanelWatchList.WrapContents = False
        '
        'moviePanel
        '
        Me.moviePanel.Location = New System.Drawing.Point(3, 3)
        Me.moviePanel.Name = "moviePanel"
        Me.moviePanel.Size = New System.Drawing.Size(200, 100)
        Me.moviePanel.TabIndex = 0
        '
        'WatchListForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1189, 638)
        Me.Controls.Add(Me.FlowLayoutPanelWatchList)
        Me.Name = "WatchListForm"
        Me.Text = "WatchListForm"
        Me.FlowLayoutPanelWatchList.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FlowLayoutPanelWatchList As FlowLayoutPanel
    Friend WithEvents moviePanel As Panel
End Class
