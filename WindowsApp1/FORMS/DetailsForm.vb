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
        Dim IsMovieViewed As New CComments

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
        movieViewed = IsMovieViewed.IsMovieViewed(movie.id, userid)
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

        Dim SaveCommentToDatabase As New CComments
        Dim actualUserId As Integer? = If(userid > 0, userid, Nothing)

        If Not String.IsNullOrWhiteSpace(txtComment.Text) Then
            Dim username As String = SaveCommentToDatabase.SaveCommentToDatabase(actualUserId, movie.id, txtComment.Text)
            If Not String.IsNullOrEmpty(username) Then

                Dim formattedComment As String = username & ": " & txtComment.Text.Trim() & Environment.NewLine & Environment.NewLine
                rtbComments.AppendText(formattedComment)
                rtbComments.AppendText(Environment.NewLine)

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
            e.SuppressKeyPress = True
            btnSubmit.PerformClick()
        End If
    End Sub

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

            rtbComments.Clear()

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
        Dim SaveViewedStatusAsync As New CComments
        movieViewed = Not movieViewed

        Dim success As Boolean = Await SaveViewedStatusAsync.SaveViewedStatusAsync(movie.id, userid, movieViewed)

        If success Then
            btnMarkAsViewed.Text = If(movieViewed, "✓ Viewed", "Mark as Viewed")
        Else
            movieViewed = Not movieViewed
            MessageBox.Show("There was an error updating the movie status.")
        End If
    End Sub
End Class