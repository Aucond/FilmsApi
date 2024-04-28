Imports System.Net
Imports System.IO
Imports Npgsql
Imports NpgsqlTypes
Imports PrjDatabase

Public Class DetailsForm
    Private movie As TmdbMovie
    Private posterPath As String
    Private userid As Integer
    Private movieViewed As Boolean = False

    Public Sub New(parentForm As Form, movie As TmdbMovie, id As Integer)
        InitializeComponent()
        Me.movie = movie
        Me.userid = id
        Me.posterPath = "https://image.tmdb.org/t/p/w500" & movie.poster_path
        DisplayMovieDetails()
    End Sub

    Private Sub DisplayMovieDetails()

        ' Display details of movie in the form controls
        LabelTitle.Text = movie.title
        LabelOverview.Text = movie.overview
        If Not String.IsNullOrEmpty(movie.release_date) AndAlso
    DateTime.TryParse(movie.release_date, Nothing) Then

            Dim releaseDate As DateTime = DateTime.Parse(movie.release_date)
            LabelReleaseDate.Text = releaseDate.ToString("MMMM dd, yyyy")
        Else
            LabelReleaseDate.Text = "N/A"
        End If
        LabelVoteAverage.Text = movie.vote_average.ToString("0.0") ' Formatting to one decimal place

    End Sub


    Private Async Sub DetailsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Adjust label to fit text properly
        LabelOverview.AutoSize = False
        LabelOverview.Width = 500
        LabelOverview.Height = 300
        LabelOverview.TextAlign = ContentAlignment.TopLeft
        LabelOverview.BorderStyle = BorderStyle.FixedSingle
        LabelOverview.AutoEllipsis = True

        PictureBoxMovie.SizeMode = PictureBoxSizeMode.Zoom
        DisplayMovieDetails()
        Try
            Using client As New WebClient()
                Dim imageData As Byte() = Await client.DownloadDataTaskAsync(Me.posterPath)
                Using ms As New MemoryStream(imageData)
                    PictureBoxMovie.Image = Image.FromStream(ms)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while loading the image.")
        End Try
        LoadComments()
        If userid <= 0 Then
            btnMarkAsViewed.Visible = False
        Else
            btnMarkAsViewed.Visible = True
        End If
        ' Check if the movie is marked as viewed
        movieViewed = IsMovieViewed(movie.id, userid)
        btnMarkAsViewed.Text = If(movieViewed, "✓ Viewed", "Mark as Viewed")

    End Sub

    Private Sub btnWatchlist_Click(sender As Object, e As EventArgs) Handles btnWatchlist.Click

        Dim mydb As New CDatabase
        Dim adapter As New NpgsqlDataAdapter()
        Dim table As New DataTable()
        Dim commandCheck As New NpgsqlCommand("SELECT movieid FROM users_info WHERE id = @UserID;", mydb.getConnection)

        commandCheck.Parameters.AddWithValue("@UserID", userid)
        adapter.SelectCommand = commandCheck
        adapter.Fill(table)

        If table.Rows.Count > 0 AndAlso Not table.Rows(0)("movieid") Is DBNull.Value Then

            Dim movieIds As Integer() = CType(table.Rows(0)("movieid"), Integer())

            ' Check if the movie ID already exists in the array
            Dim movieExists As Boolean = False
            For Each id As Integer In movieIds
                If id = movie.id Then
                    movieExists = True
                    Exit For
                End If
            Next

            ' If the movie already exists, display a message and exit
            If movieExists Then
                MessageBox.Show("Movie already exists in your watch list")
                Return
            End If
        End If

        Dim command As New NpgsqlCommand("UPDATE users_info SET movieid = array_append(movieid, @MovieID) WHERE id = @UserID;", mydb.getConnection)

        command.Parameters.AddWithValue("@MovieID", movie.id)
        command.Parameters.AddWithValue("@UserID", userid)

        adapter.SelectCommand = command
        adapter.Fill(table)

        MessageBox.Show("Movie has been added to your watch list")
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Dim watchLater As New WATCHLATER_FORM
        Dim mydb As New CDatabase
        Dim adapter As New NpgsqlDataAdapter()
        Dim table As New DataTable()
        Dim commandCheck As New NpgsqlCommand("SELECT movieid FROM users_info WHERE id = @UserID;", mydb.getConnection)

        commandCheck.Parameters.AddWithValue("@UserID", userid)
        adapter.SelectCommand = commandCheck
        adapter.Fill(table)

        If table.Rows.Count > 0 AndAlso Not table.Rows(0)("movieid") Is DBNull.Value Then

            Dim movieIds As Integer() = CType(table.Rows(0)("movieid"), Integer())

            Dim movieIndex As Integer = Array.IndexOf(movieIds, movie.id)

            If movieIndex <> -1 Then

                Dim updatedMovieIds As New List(Of Integer)(movieIds)
                updatedMovieIds.RemoveAt(movieIndex)

                Dim updatedMovieIdsArray As Integer() = updatedMovieIds.ToArray()
                Dim commandRemove As New NpgsqlCommand("UPDATE users_info SET movieid = @MovieIDArray WHERE id = @UserID;", mydb.getConnection)

                commandRemove.Parameters.AddWithValue("@MovieIDArray", NpgsqlDbType.Array Or NpgsqlDbType.Integer, updatedMovieIdsArray)
                commandRemove.Parameters.AddWithValue("@UserID", userid)

                adapter.SelectCommand = commandRemove
                adapter.Fill(table)

                MessageBox.Show("Movie has been removed from your watch list")
            Else
                MessageBox.Show("Movie does not exist in your watch list")
            End If
        Else
            MessageBox.Show("No movies found in your watch list")
        End If
        Me.Close()
    End Sub

    Private Sub btnBlock_Click(sender As Object, e As EventArgs) Handles btnBlock.Click
        Dim mydb As New CDatabase
        Dim adapter As New NpgsqlDataAdapter()
        Dim table As New DataTable()
        Dim commandCheck As New NpgsqlCommand("SELECT blockid FROM users_info WHERE id = @UserID;", mydb.getConnection)

        commandCheck.Parameters.AddWithValue("@UserID", userid)
        adapter.SelectCommand = commandCheck
        adapter.Fill(table)

        If table.Rows.Count > 0 AndAlso Not table.Rows(0)("blockid") Is DBNull.Value Then

            Dim movieIds As Integer() = CType(table.Rows(0)("blockid"), Integer())

            ' Check if the movie ID already exists in the array
            Dim movieExists As Boolean = False
            For Each id As Integer In movieIds
                If id = movie.id Then
                    movieExists = True
                    Exit For
                End If
            Next

            ' If the movie already exists, display a message and exit
            If movieExists Then
                MessageBox.Show("Movie already exists in your blocked list")
                Return
            End If
        End If

        Dim command As New NpgsqlCommand("UPDATE users_info SET blockid = array_append(blockid, @MovieID) WHERE id = @UserID;", mydb.getConnection)

        command.Parameters.AddWithValue("@MovieID", movie.id)
        command.Parameters.AddWithValue("@UserID", userid)

        adapter.SelectCommand = command
        adapter.Fill(table)

        MessageBox.Show("Movie has been added to your blocked list")
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        ' Determine if we have a logged in user or a guest
        Dim actualUserId As Integer? = If(userid > 0, userid, Nothing)

        If Not String.IsNullOrWhiteSpace(txtComment.Text) Then
            Dim username As String = SaveCommentToDatabase(actualUserId, movie.id, txtComment.Text)
            If Not String.IsNullOrEmpty(username) Then
                ' Append the comment with the username or "Guest" to the RichTextBox for display
                Dim formattedComment As String = username & ": " & txtComment.Text.Trim() & Environment.NewLine & Environment.NewLine
                rtbComments.AppendText(formattedComment)
                rtbComments.AppendText(Environment.NewLine)

                ' Clear the textbox for the next comment
                txtComment.Text = ""
            Else
                MessageBox.Show("Failed to insert the comment.")
            End If
        Else
            MessageBox.Show("Please enter a comment before submitting.", "Empty Comment", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub



    Private Sub txtComment_KeyDown(sender As Object, e As KeyEventArgs) Handles txtComment.KeyDown
        If e.Shift And e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True  ' Prevents the ding sound when pressing enter
            btnSubmit.PerformClick()
        End If
    End Sub
    Private Function SaveCommentToDatabase(userId As Integer?, movieId As Integer, commentText As String) As String
        Dim mydb As New CDatabase()
        Dim username As String

        ' Check if we have a userId or if this is a guest
        If userId.HasValue AndAlso userId.Value > 0 Then
            ' If it's a registered user, retrieve the username
            Try
                mydb.openConnection()
                Dim usernameCommand As New NpgsqlCommand("SELECT username FROM users_info WHERE id = @UserID;", mydb.getConnection())
                usernameCommand.Parameters.AddWithValue("@UserID", userId.Value)

                Dim result As Object = usernameCommand.ExecuteScalar()
                mydb.closeConnection()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    username = result.ToString()
                Else
                    ' If no username is found, default to Guest
                    username = "Guest"
                End If
            Catch ex As Exception
                ' Log or handle exception
                mydb.closeConnection()
                MessageBox.Show("An error occurred: " & ex.Message)
                Return String.Empty
            End Try
        Else
            ' If userId is not provided, it means it's a guest comment
            username = "Guest"
        End If

        ' Insert the comment into the database with the determined username
        Try
            mydb.openConnection()
            Dim command As New NpgsqlCommand("INSERT INTO Comments (Username, MovieID, CommentText) VALUES (@Username, @MovieID, @CommentText);", mydb.getConnection())
            command.Parameters.AddWithValue("@Username", username)
            command.Parameters.AddWithValue("@MovieID", movieId)
            command.Parameters.AddWithValue("@CommentText", commentText)

            If command.ExecuteNonQuery() = 1 Then
                mydb.closeConnection()
                ' If the comment was successfully inserted, return the username
                Return username
            Else
                mydb.closeConnection()
                Return String.Empty
            End If
        Catch ex As Exception
            ' Log or handle exception
            MessageBox.Show("An error occurred while inserting the comment: " & ex.Message)
            Return String.Empty
        Finally
            If mydb.getConnection().State = ConnectionState.Open Then
                mydb.closeConnection()
            End If
        End Try
    End Function



    Private Sub LoadComments()
        Try
            Dim mydb As New CDatabase()
            Dim command As New NpgsqlCommand("SELECT Username, CommentText, CommentDate FROM Comments WHERE MovieID = @MovieID ORDER BY CommentDate DESC;", mydb.getConnection())

            command.Parameters.AddWithValue("@MovieID", movie.id)

            Dim adapter As New NpgsqlDataAdapter(command)
            Dim table As New DataTable()

            mydb.openConnection()
            adapter.Fill(table)
            mydb.closeConnection()

            ' Clear the current comments
            rtbComments.Clear()

            ' Loop through each row and append the comment to the RichTextBox
            For Each row As DataRow In table.Rows
                Dim comment As String = String.Format("{0}: {1}" & Environment.NewLine & "{2}" & Environment.NewLine & Environment.NewLine,
                                                  row("Username"), row("CommentText"), row("CommentDate").ToString())
                rtbComments.AppendText(comment)
            Next
        Catch ex As Exception
            MessageBox.Show("An error occurred while loading comments: " & ex.Message)
        End Try
    End Sub

    Private Async Sub btnMarkAsViewed_Click(sender As Object, e As EventArgs) Handles btnMarkAsViewed.Click
        ' Toggle the movieViewed state
        movieViewed = Not movieViewed

        ' Call the function to update the database with the viewed status
        Dim success As Boolean = Await SaveViewedStatusAsync(movie.id, userid, movieViewed)

        ' Update the button text based on the result
        If success Then
            btnMarkAsViewed.Text = If(movieViewed, "✓ Viewed", "Mark as Viewed")
        Else
            ' If there was an error, revert the view state change
            movieViewed = Not movieViewed
            MessageBox.Show("There was an error updating the movie status.")
        End If
    End Sub


    Private Async Function SaveViewedStatusAsync(movieId As Integer, userId As Integer?, viewed As Boolean) As Task(Of Boolean)
        Dim mydb As New CDatabase()
        Try
            ' Fetch movie runtime asynchronously
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





    Private Function IsMovieViewed(movieId As Integer, userId As Integer) As Boolean
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