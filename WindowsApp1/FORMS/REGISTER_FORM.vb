Imports Npgsql

Public Class REGISTER_FORM
    Private Sub Button_register_Click(sender As Object, e As EventArgs) Handles Button_register.Click
        Dim age As String = TextBox_age.Text.Trim()
        Dim salt As String = CPasswordHash.GenerateSalt()
        Dim password As String = CPasswordHash.HashPassword(TextBox_password.Text, salt)

        If Not IsNumeric(age) Then
            MessageBox.Show("Incorrect age!")
        End If

        If String.IsNullOrWhiteSpace(TextBox_fname.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox_lname.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox_age.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox_username.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox_fname.Text) Then
            MessageBox.Show("Please fill in all fields.")
            Return
        End If

        Dim mydb As New DB()
        Dim adapter As New NpgsqlDataAdapter()
        Dim table As New DataTable()

        Dim cmd As New NpgsqlCommand("INSERT INTO users_info (first_name, last_name, username, password, salt, age) VALUES (@first_name, @last_name, @username, @password, @salt, @age)", mydb.getConnection)
        With cmd.Parameters
            .AddWithValue("@first_name", TextBox_fname.Text)
            .AddWithValue("@last_name", TextBox_lname.Text)
            .AddWithValue("@username", TextBox_username.Text)
            .AddWithValue("@password", password)
            .AddWithValue("@salt", salt)
            .AddWithValue("@age", TextBox_age.Text)
        End With

        adapter.SelectCommand = cmd
        adapter.Fill(table)

        MessageBox.Show("Registration Successful!")
        Me.Hide()
        LOGIN_FORM.Show()

    End Sub
End Class