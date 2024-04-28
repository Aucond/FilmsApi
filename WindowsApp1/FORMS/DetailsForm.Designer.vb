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
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.rtbComments = New System.Windows.Forms.RichTextBox()
        CType(Me.PictureBoxMovie, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxMovie
        '
        Me.PictureBoxMovie.Location = New System.Drawing.Point(42, 31)
        Me.PictureBoxMovie.Name = "PictureBoxMovie"
        Me.PictureBoxMovie.Size = New System.Drawing.Size(200, 300)
        Me.PictureBoxMovie.TabIndex = 0
        Me.PictureBoxMovie.TabStop = False
        '
        'LabelTitle
        '
        Me.LabelTitle.AutoSize = True
        Me.LabelTitle.Location = New System.Drawing.Point(325, 45)
        Me.LabelTitle.Name = "LabelTitle"
        Me.LabelTitle.Size = New System.Drawing.Size(39, 13)
        Me.LabelTitle.TabIndex = 1
        Me.LabelTitle.Text = "Label1"
        '
        'LabelReleaseDate
        '
        Me.LabelReleaseDate.AutoSize = True
        Me.LabelReleaseDate.Location = New System.Drawing.Point(923, 45)
        Me.LabelReleaseDate.Name = "LabelReleaseDate"
        Me.LabelReleaseDate.Size = New System.Drawing.Size(39, 13)
        Me.LabelReleaseDate.TabIndex = 2
        Me.LabelReleaseDate.Text = "Label2"
        '
        'LabelOverview
        '
        Me.LabelOverview.AutoSize = True
        Me.LabelOverview.Location = New System.Drawing.Point(325, 104)
        Me.LabelOverview.Name = "LabelOverview"
        Me.LabelOverview.Size = New System.Drawing.Size(39, 13)
        Me.LabelOverview.TabIndex = 3
        Me.LabelOverview.Text = "Label3"
        '
        'LabelVoteAverage
        '
        Me.LabelVoteAverage.AutoSize = True
        Me.LabelVoteAverage.Location = New System.Drawing.Point(783, 45)
        Me.LabelVoteAverage.Name = "LabelVoteAverage"
        Me.LabelVoteAverage.Size = New System.Drawing.Size(39, 13)
        Me.LabelVoteAverage.TabIndex = 4
        Me.LabelVoteAverage.Text = "Label4"
        '
        'btnWatchlist
        '
        Me.btnWatchlist.Location = New System.Drawing.Point(926, 497)
        Me.btnWatchlist.Name = "btnWatchlist"
        Me.btnWatchlist.Size = New System.Drawing.Size(119, 23)
        Me.btnWatchlist.TabIndex = 6
        Me.btnWatchlist.Text = "Add to watchlist"
        Me.btnWatchlist.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(926, 554)
        Me.btnRemove.Margin = New System.Windows.Forms.Padding(2)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(119, 21)
        Me.btnRemove.TabIndex = 7
        Me.btnRemove.Text = "Remove"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnBlock
        '
        Me.btnBlock.Location = New System.Drawing.Point(926, 526)
        Me.btnBlock.Margin = New System.Windows.Forms.Padding(2)
        Me.btnBlock.Name = "btnBlock"
        Me.btnBlock.Size = New System.Drawing.Size(118, 24)
        Me.btnBlock.TabIndex = 8
        Me.btnBlock.Text = "Block"
        Me.btnBlock.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(284, 45)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(741, 45)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Rating:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(843, 45)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Realease date:"
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(256, 530)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(535, 33)
        Me.txtComment.TabIndex = 12
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(797, 534)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(75, 23)
        Me.btnSubmit.TabIndex = 13
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'rtbComments
        '
        Me.rtbComments.Location = New System.Drawing.Point(256, 424)
        Me.rtbComments.Name = "rtbComments"
        Me.rtbComments.ReadOnly = True
        Me.rtbComments.Size = New System.Drawing.Size(535, 96)
        Me.rtbComments.TabIndex = 15
        Me.rtbComments.Text = ""
        '
        'DetailsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1071, 589)
        Me.Controls.Add(Me.rtbComments)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.txtComment)
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
    Friend WithEvents txtComment As TextBox
    Friend WithEvents btnSubmit As Button
    Friend WithEvents rtbComments As RichTextBox
End Class
