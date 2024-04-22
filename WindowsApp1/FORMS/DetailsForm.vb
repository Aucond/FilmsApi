Imports System.Net
Imports System.IO
Imports Npgsql
Imports NpgsqlTypes
Imports Newtonsoft.Json
Imports System.Net.Http
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Public Class DetailsForm
    Private movie As TmdbMovie
    Private posterPath As String
    Private userid As Integer

    Public Sub New(parentForm As Form, movie As TmdbMovie, id As Integer)
        InitializeComponent()
        Me.movie = movie
        Me.userid = id
        Me.posterPath = "https://image.tmdb.org/t/p/w500" & movie.poster_path
        DisplayMovieDetails()
    End Sub

    Private Sub DisplayMovieDetails()
        ' Display details of movie in the form controls
        ' For example:
        LabelTitle.Text = movie.title
        LabelOverview.Text = movie.overview
        ' Add more fields as per your UI design
        If Not String.IsNullOrEmpty(movie.release_date) AndAlso
    DateTime.TryParse(movie.release_date, Nothing) Then

            Dim releaseDate As DateTime = DateTime.Parse(movie.release_date)
            LabelReleaseDate.Text = releaseDate.ToString("MMMM dd, yyyy")
        Else
            LabelReleaseDate.Text = "N/A"
        End If

        ' Set the vote average and original language
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




        ' Ensure image fits in the PictureBox
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
    End Sub

    Private Sub btnWatchlist_Click(sender As Object, e As EventArgs) Handles btnWatchlist.Click

        Dim mydb As New DB()
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
        Dim mydb As New DB()
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
End Class