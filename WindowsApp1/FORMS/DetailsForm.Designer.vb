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
        Me.LabelOriginalLanguage = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
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
        Me.LabelTitle.Location = New System.Drawing.Point(325, 31)
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
        'LabelOriginalLanguage
        '
        Me.LabelOriginalLanguage.AutoSize = True
        Me.LabelOriginalLanguage.Location = New System.Drawing.Point(325, 67)
        Me.LabelOriginalLanguage.Name = "LabelOriginalLanguage"
        Me.LabelOriginalLanguage.Size = New System.Drawing.Size(39, 13)
        Me.LabelOriginalLanguage.TabIndex = 5
        Me.LabelOriginalLanguage.Text = "Label5"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(926, 530)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(119, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "AddToWatchList"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DetailsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1071, 589)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.LabelOriginalLanguage)
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
    Friend WithEvents LabelOriginalLanguage As Label
    Friend WithEvents Button1 As Button
End Class
