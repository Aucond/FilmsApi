Imports Npgsql
Imports NpgsqlTypes

Public Class LOGIN_FORM
    Private Sub Label_Close_Click(sender As Object, e As EventArgs) Handles Label_Close.Click

        ' close this form
        Me.Close()

    End Sub

    Private Sub Label_Close_MouseEnter(sender As Object, e As EventArgs) Handles Label_Close.MouseEnter

        ' change the color to yellow
        Label_Close.ForeColor = Color.Yellow

    End Sub

    Private Sub Label_Close_MouseLeave(sender As Object, e As EventArgs) Handles Label_Close.MouseLeave

        ' change the color to white
        Label_Close.ForeColor = Color.White

    End Sub

    Public Sub Button_login_Click(sender As Object, e As EventArgs) Handles Button_login.Click

        ' the login button 
        ' allow the user or not to access the application main window ( the dashboard )
        Dim username As String = TextBox_username.Text.Trim()
        Dim password As String = TextBox_password.Text.Trim()

        Dim mydb As New DB()
        Dim adapter As New NpgsqlDataAdapter()
        Dim table As New DataTable()
        Dim command As New NpgsqlCommand("SELECT * FROM users_info WHERE username = @usn", mydb.getConnection)

        If username = "" Then
            MessageBox.Show("Enter The Username", "Empty Username", MessageBoxButtons.OK, MessageBoxIcon.Error)

        ElseIf password = "" Then
            MessageBox.Show("Enter The Password", "Empty Password", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            ' check if this user exist

            command.Parameters.Add("@usn", NpgsqlDbType.Varchar).Value = username

            adapter.SelectCommand = command
            adapter.Fill(table)

            If table.Rows.Count > 0 Then
                Dim hashedPasswordFromDB As String = table.Rows(0)("password").ToString()
                Dim salt As String = table.Rows(0)("salt").ToString()
                Dim inputPassword = CPasswordHash.HashPassword(password, salt)
                Dim id As Integer = table.Rows(0)("id")

                If hashedPasswordFromDB = inputPassword Then
                    Dim CAccountType As New CAccountType
                    Dim idNR As New MOVIE_FORM
                    Dim age As Integer = table.Rows(0)("age")
                    idNR.idNR(table.Rows(0)("id"))
                    If age < 18 Then
                        Me.Hide()
                        CAccountType.Under18(table.Rows(0)("age"))
                    Else
                        Me.Hide()
                        MOVIE_FORM.Show()
                        MOVIE_FORM.ListToolStripMenuItem.Visible = False
                        MOVIE_FORM.AccountToolStripMenuItem.Visible = True
                    End If
                Else
                    MessageBox.Show("This Username Or Password Doesn't Exist", "Wrong Info", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                End If

            End If
        End If

    End Sub

    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click

        Me.Hide()
        REGISTER_FORM.Show()

    End Sub

    Private Sub guestBtn_Click(sender As Object, e As EventArgs) Handles guestBtn.Click
        Dim CAccountType As New CAccountType
        Me.Hide()
        CAccountType.Guest(10)
    End Sub

End Class