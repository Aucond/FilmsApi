<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LOGIN_FORM
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Button_login = New System.Windows.Forms.Button()
        Me.TextBox_password = New System.Windows.Forms.TextBox()
        Me.TextBox_username = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label_Close = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DarkOrange
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(3, 2)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(517, 369)
        Me.Panel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Button_login)
        Me.Panel2.Controls.Add(Me.TextBox_password)
        Me.Panel2.Controls.Add(Me.TextBox_username)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Controls.Add(Me.Label_Close)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Location = New System.Drawing.Point(4, 4)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(509, 362)
        Me.Panel2.TabIndex = 0
        '
        'Button_login
        '
        Me.Button_login.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.Button_login.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button_login.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.Button_login.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_login.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button_login.ForeColor = System.Drawing.Color.White
        Me.Button_login.Location = New System.Drawing.Point(5, 289)
        Me.Button_login.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button_login.Name = "Button_login"
        Me.Button_login.Size = New System.Drawing.Size(496, 65)
        Me.Button_login.TabIndex = 7
        Me.Button_login.Text = "Continue"
        Me.Button_login.UseVisualStyleBackColor = False
        '
        'TextBox_password
        '
        Me.TextBox_password.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox_password.ForeColor = System.Drawing.Color.Black
        Me.TextBox_password.Location = New System.Drawing.Point(172, 217)
        Me.TextBox_password.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBox_password.Name = "TextBox_password"
        Me.TextBox_password.Size = New System.Drawing.Size(279, 34)
        Me.TextBox_password.TabIndex = 6
        Me.TextBox_password.UseSystemPasswordChar = True
        '
        'TextBox_username
        '
        Me.TextBox_username.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox_username.Location = New System.Drawing.Point(172, 156)
        Me.TextBox_username.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBox_username.Name = "TextBox_username"
        Me.TextBox_username.Size = New System.Drawing.Size(279, 34)
        Me.TextBox_username.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(55, 224)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 25)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Password:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(48, 162)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(108, 25)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Username:"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Gray
        Me.Panel4.Location = New System.Drawing.Point(0, 95)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(509, 5)
        Me.Panel4.TabIndex = 2
        '
        'Label_Close
        '
        Me.Label_Close.AutoSize = True
        Me.Label_Close.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label_Close.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Close.ForeColor = System.Drawing.Color.White
        Me.Label_Close.Location = New System.Drawing.Point(473, 5)
        Me.Label_Close.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label_Close.Name = "Label_Close"
        Me.Label_Close.Size = New System.Drawing.Size(30, 29)
        Me.Label_Close.TabIndex = 1
        Me.Label_Close.Text = "X"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.DarkOrange
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Location = New System.Drawing.Point(119, -2)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(296, 81)
        Me.Panel3.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.DarkOrange
        Me.Label1.Font = New System.Drawing.Font("Arial", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(3, 2)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(291, 76)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Login"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LOGIN_FORM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(523, 375)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "LOGIN_FORM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LOGIN_FORM"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label_Close As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Button_login As System.Windows.Forms.Button
    Friend WithEvents TextBox_password As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_username As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
