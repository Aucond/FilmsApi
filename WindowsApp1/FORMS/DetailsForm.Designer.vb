<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DetailsForm
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
        Me.PictureBoxMovie = New System.Windows.Forms.PictureBox()
        Me.LabelTitle = New System.Windows.Forms.Label()
        Me.LabelReleaseDate = New System.Windows.Forms.Label()
        Me.LabelOverview = New System.Windows.Forms.Label()
        Me.LabelVoteAverage = New System.Windows.Forms.Label()
        Me.btnWatchlist = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnBlock = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.PictureBoxMovie, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxMovie
        '
        Me.PictureBoxMovie.Location = New System.Drawing.Point(56, 38)
        Me.PictureBoxMovie.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBoxMovie.Name = "PictureBoxMovie"
        Me.PictureBoxMovie.Size = New System.Drawing.Size(267, 369)
        Me.PictureBoxMovie.TabIndex = 0
        Me.PictureBoxMovie.TabStop = False
        '
        'LabelTitle
        '
        Me.LabelTitle.AutoSize = True
        Me.LabelTitle.Location = New System.Drawing.Point(433, 55)
        Me.LabelTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTitle.Name = "LabelTitle"
        Me.LabelTitle.Size = New System.Drawing.Size(48, 16)
        Me.LabelTitle.TabIndex = 1
        Me.LabelTitle.Text = "Label1"
        '
        'LabelReleaseDate
        '
        Me.LabelReleaseDate.AutoSize = True
        Me.LabelReleaseDate.Location = New System.Drawing.Point(1231, 55)
        Me.LabelReleaseDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelReleaseDate.Name = "LabelReleaseDate"
        Me.LabelReleaseDate.Size = New System.Drawing.Size(48, 16)
        Me.LabelReleaseDate.TabIndex = 2
        Me.LabelReleaseDate.Text = "Label2"
        '
        'LabelOverview
        '
        Me.LabelOverview.AutoSize = True
        Me.LabelOverview.Location = New System.Drawing.Point(433, 128)
        Me.LabelOverview.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelOverview.Name = "LabelOverview"
        Me.LabelOverview.Size = New System.Drawing.Size(48, 16)
        Me.LabelOverview.TabIndex = 3
        Me.LabelOverview.Text = "Label3"
        '
        'LabelVoteAverage
        '
        Me.LabelVoteAverage.AutoSize = True
        Me.LabelVoteAverage.Location = New System.Drawing.Point(1044, 55)
        Me.LabelVoteAverage.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelVoteAverage.Name = "LabelVoteAverage"
        Me.LabelVoteAverage.Size = New System.Drawing.Size(48, 16)
        Me.LabelVoteAverage.TabIndex = 4
        Me.LabelVoteAverage.Text = "Label4"
        '
        'btnWatchlist
        '
        Me.btnWatchlist.Location = New System.Drawing.Point(1235, 612)
        Me.btnWatchlist.Margin = New System.Windows.Forms.Padding(4)
        Me.btnWatchlist.Name = "btnWatchlist"
        Me.btnWatchlist.Size = New System.Drawing.Size(159, 28)
        Me.btnWatchlist.TabIndex = 6
        Me.btnWatchlist.Text = "Add to watchlist"
        Me.btnWatchlist.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(1234, 682)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(159, 26)
        Me.btnRemove.TabIndex = 7
        Me.btnRemove.Text = "Remove"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnBlock
        '
        Me.btnBlock.Location = New System.Drawing.Point(1235, 647)
        Me.btnBlock.Name = "btnBlock"
        Me.btnBlock.Size = New System.Drawing.Size(157, 29)
        Me.btnBlock.TabIndex = 8
        Me.btnBlock.Text = "Block"
        Me.btnBlock.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(379, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 16)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(988, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 16)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Rating:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1124, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 16)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Realease date:"
        '
        'DetailsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1428, 725)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnBlock)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnWatchlist)
        Me.Controls.Add(Me.LabelVoteAverage)
        Me.Controls.Add(Me.LabelOverview)
        Me.Controls.Add(Me.LabelReleaseDate)
        Me.Controls.Add(Me.LabelTitle)
        Me.Controls.Add(Me.PictureBoxMovie)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "DetailsForm"
        Me.Text = "Form1"
        CType(Me.PictureBoxMovie, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBoxMovie As PictureBox
    Friend WithEvents LabelTitle As Label
    Friend WithEvents LabelReleaseDate As Label
    Friend WithEvents LabelOverview As Label
    Friend WithEvents LabelVoteAverage As Label
    Friend WithEvents btnWatchlist As Button
    Friend WithEvents btnRemove As Button
    Friend WithEvents btnBlock As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
End Class
