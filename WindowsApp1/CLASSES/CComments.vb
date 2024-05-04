Imports Npgsql
Imports PrjDatabase

Public Class CComments
    Public Function SaveCommentToDatabase(userId As Integer?, movieId As Integer, commentText As String) As String
        Dim mydb As New CDatabase()
        Dim username As String

        If userId.HasValue AndAlso userId.Value > 0 Then
            Try
                mydb.openConnection()
                Dim usernameCommand As New NpgsqlCommand("SELECT username FROM users_info WHERE id = @UserID;", mydb.getConnection())
                usernameCommand.Parameters.AddWithValue("@UserID", userId.Value)

                Dim result As Object = usernameCommand.ExecuteScalar()
                mydb.closeConnection()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    username = result.ToString()
                Else
                    username = "Guest"
                End If
            Catch ex As Exception
                mydb.closeConnection()
                MessageBox.Show("An error occurred: " & ex.Message)
                Return String.Empty
            End Try
        Else
            username = "Guest"
        End If
        Try
            mydb.openConnection()
            Dim command As New NpgsqlCommand("INSERT INTO Comments (Username, MovieID, CommentText) VALUES (@Username, @MovieID, @CommentText);", mydb.getConnection())
            command.Parameters.AddWithValue("@Username", username)
            command.Parameters.AddWithValue("@MovieID", movieId)
            command.Parameters.AddWithValue("@CommentText", commentText)

            If command.ExecuteNonQuery() = 1 Then
                mydb.closeConnection()
                Return username
            Else
                mydb.closeConnection()
                Return String.Empty
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred while inserting the comment: " & ex.Message)
            Return String.Empty
        Finally
            If mydb.getConnection().State = ConnectionState.Open Then
                mydb.closeConnection()
            End If
        End Try
    End Function
    Public Async Function SaveViewedStatusAsync(movieId As Integer, userId As Integer?, viewed As Boolean) As Task(Of Boolean)
        Dim mydb As New CDatabase()
        Try
            Dim movieRuntime As Integer = Await New CSearch().SearchMovieRuntimeAsync(movieId)

            mydb.openConnection()

            Dim command As New NpgsqlCommand()
            command.Connection = mydb.getConnection()

            If viewed Then
                ' If marking as viewed, insert or update the record with the movie duration
                command.CommandText = "INSERT INTO MovieViewedStatus (MovieID, UserID, Viewed, Duration, ViewDate) " &
                                  "VALUES (@MovieID, @UserID, @Viewed, @Duration, CURRENT_TIMESTAMP) " &
                                  "ON CONFLICT (MovieID, UserID) DO UPDATE SET Viewed = EXCLUDED.Viewed, Duration = EXCLUDED.Duration, ViewDate = CURRENT_TIMESTAMP;"
                command.Parameters.AddWithValue("@Duration", movieRuntime)  ' Use the fetched movie runtime
            Else
                ' If unwatching, set Viewed to FALSE and do not update Duration
                command.CommandText = "UPDATE MovieViewedStatus SET Viewed = @Viewed, Duration = NULL WHERE MovieID = @MovieID AND UserID = COALESCE(@UserID, UserID);"
            End If

            command.Parameters.AddWithValue("@MovieID", movieId)
            command.Parameters.AddWithValue("@UserID", If(userId.HasValue AndAlso userId > 0, userId.Value, DBNull.Value))
            command.Parameters.AddWithValue("@Viewed", viewed)

            Dim result = command.ExecuteNonQuery()
            Return result > 0
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message)
            Return False
        Finally
            mydb.closeConnection()
        End Try
    End Function
    Public Function IsMovieViewed(movieId As Integer, userId As Integer) As Boolean
        Dim mydb As New CDatabase()
        Try
            Dim sql As String = "SELECT COUNT(1) FROM MovieViewedStatus WHERE MovieID = @MovieID AND UserID = @UserID AND Viewed = TRUE;"
            Dim command As New NpgsqlCommand(sql, mydb.getConnection())
            command.Parameters.AddWithValue("@MovieID", movieId)
            command.Parameters.AddWithValue("@UserID", userId)

            mydb.openConnection()
            Dim result As Integer = Convert.ToInt32(command.ExecuteScalar())
            Return result > 0
        Catch ex As Exception
            MessageBox.Show("An error occurred while checking the viewed status: " & ex.Message)
            Return False
        Finally
            mydb.closeConnection()
        End Try
    End Function
End Class