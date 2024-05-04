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
        LabelOverview.Height = 200
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

    Private Sub LoadComments()
        flpComments.Controls.Clear()

        Dim mydb As New CDatabase()
        Dim command As New NpgsqlCommand("SELECT CommentID, Username, CommentText, CommentDate FROM Comments WHERE MovieID = @MovieID ORDER BY CommentDate DESC;", mydb.getConnection())
        command.Parameters.AddWithValue("@MovieID", movie.id)

        Dim adapter As New NpgsqlDataAdapter(command)
        Dim table As New DataTable()
        mydb.openConnection()
        adapter.Fill(table)
        mydb.closeConnection()

        For Each row As DataRow In table.Rows
            Dim panel As New Panel()
            panel.Width = flpComments.Width - 20
            panel.Height = 70
            panel.Margin = New Padding(3)
            panel.BorderStyle = BorderStyle.FixedSingle


            Dim lblComment As New Label()
            lblComment.Text = $"{row("Username")}: {row("CommentText")} - {Convert.ToDateTime(row("CommentDate")).ToString("dd.MM.yyyy HH:mm")}"
            lblComment.Location = New Point(5, 5)
            lblComment.AutoSize = True
            panel.Controls.Add(lblComment)


            If userid > 0 Then
                Dim likesCount As Integer = GetReactionCount(Convert.ToInt32(row("CommentID")), "L")
                Dim dislikesCount As Integer = GetReactionCount(Convert.ToInt32(row("CommentID")), "D")


                Dim btnLike As New Button()
                btnLike.Text = $"Like ({likesCount})"
                btnLike.Location = New Point(5, 40)
                btnLike.Width = 100
                btnLike.Tag = row("CommentID")
                AddHandler btnLike.Click, AddressOf LikeButtonClick
                panel.Controls.Add(btnLike)


                Dim btnDislike As New Button()
                btnDislike.Text = $"Dislike ({dislikesCount})"
                btnDislike.Location = New Point(110, 40)
                btnDislike.Width = 100
                btnDislike.Tag = row("CommentID")
                AddHandler btnDislike.Click, AddressOf DislikeButtonClick
                panel.Controls.Add(btnDislike)
            End If


            flpComments.Controls.Add(panel)
        Next
    End Sub


    Private Async Sub btnMarkAsViewed_Click(sender As Object, e As EventArgs) Handles btnMarkAsViewed.Click

        movieViewed = Not movieViewed

        Dim SavedViewed As PrjStatistics.IStatistics
        SavedViewed = New PrjStatistics.CStatistics


        Dim success As Boolean = Await SavedViewed.SaveViewedStatusAsync(movie.id, userid, movieViewed)


        If success Then
            btnMarkAsViewed.Text = If(movieViewed, "✓ Viewed", "Mark as Viewed")
        Else

            movieViewed = Not movieViewed
            MessageBox.Show("There was an error updating the movie status.")
        End If
    End Sub


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
    Private Function GetReactionCount(commentID As Integer, reactionType As Char) As Integer
        Dim mydb As New CDatabase()
        Dim count As Integer = 0
        Try
            mydb.openConnection()
            Dim command As New NpgsqlCommand("SELECT COUNT(*) FROM CommentReactions WHERE CommentID = @CommentID AND ReactionType = @ReactionType;", mydb.getConnection())
            command.Parameters.AddWithValue("@CommentID", commentID)
            command.Parameters.AddWithValue("@ReactionType", reactionType)
            count = Convert.ToInt32(command.ExecuteScalar())
        Catch ex As Exception
            MessageBox.Show("Error fetching reaction count: " & ex.Message)
        Finally
            mydb.closeConnection()
        End Try
        Return count
    End Function
    Private Sub LikeButtonClick(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim commentID As Integer = Convert.ToInt32(btn.Tag)
        HandleReaction(commentID, True)
    End Sub

    Private Sub DislikeButtonClick(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim commentID As Integer = Convert.ToInt32(btn.Tag)
        HandleReaction(commentID, False)
    End Sub
    Private Sub HandleReaction(commentID As Integer, isLike As Boolean)
        Dim mydb As New CDatabase()
        Try
            mydb.openConnection()

            Dim currentReactionType As Char? = GetCurrentReaction(commentID)
            Dim newReactionType As Char = If(isLike, "L"c, "D"c)

            If currentReactionType.HasValue Then
                If currentReactionType = newReactionType Then

                    Dim commandDelete As New NpgsqlCommand("DELETE FROM CommentReactions WHERE UserID = @UserID AND CommentID = @CommentID;", mydb.getConnection())
                    commandDelete.Parameters.AddWithValue("@UserID", userid)
                    commandDelete.Parameters.AddWithValue("@CommentID", commentID)
                    commandDelete.ExecuteNonQuery()
                Else

                    Dim commandUpdate As New NpgsqlCommand("UPDATE CommentReactions SET ReactionType = @NewType WHERE UserID = @UserID AND CommentID = @CommentID;", mydb.getConnection())
                    commandUpdate.Parameters.AddWithValue("@NewType", newReactionType)
                    commandUpdate.Parameters.AddWithValue("@UserID", userid)
                    commandUpdate.Parameters.AddWithValue("@CommentID", commentID)
                    commandUpdate.ExecuteNonQuery()
                End If
            Else

                Dim commandInsert As New NpgsqlCommand("INSERT INTO CommentReactions (CommentID, UserID, ReactionType) VALUES (@CommentID, @UserID, @ReactionType);", mydb.getConnection())
                commandInsert.Parameters.AddWithValue("@CommentID", commentID)
                commandInsert.Parameters.AddWithValue("@UserID", userid)
                commandInsert.Parameters.AddWithValue("@ReactionType", newReactionType)
                commandInsert.ExecuteNonQuery()
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred while updating the reaction: " & ex.Message)
        Finally
            mydb.closeConnection()
            LoadComments()
        End Try
    End Sub
    Private Function GetCurrentReaction(commentID As Integer) As Char?
        Dim mydb As New CDatabase()
        Try
            mydb.openConnection()
            Dim command As New NpgsqlCommand("SELECT ReactionType FROM CommentReactions WHERE UserID = @UserID AND CommentID = @CommentID;", mydb.getConnection())
            command.Parameters.AddWithValue("@UserID", userid)
            command.Parameters.AddWithValue("@CommentID", commentID)

            Dim result As Object = command.ExecuteScalar()
            If result IsNot Nothing Then
                Return Convert.ToChar(result)
            End If
        Catch ex As Exception
            MessageBox.Show("Error fetching current reaction: " & ex.Message)
        Finally
            mydb.closeConnection()
        End Try
        Return Nothing
    End Function
End Class
