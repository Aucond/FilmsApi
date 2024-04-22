Imports Newtonsoft.Json.Linq
Imports System.IO
Imports System.Net
Imports Npgsql
Imports Size = System.Drawing.Size

Public Class WATCHLATER_FORM
    Dim userid As Integer = LOGIN_FORM.personID
    Dim movieList As New List(Of TmdbMovie)()
    Public WithEvents ListViewMovies As New ListView()
    Public Sub New()
        InitializeComponent()
        AccessMovieID()
    End Sub
    Public Async Sub AccessMovieID()
        Dim movieInfo As New CSearch
        Dim mydb As New DB()
        Dim adapter As New NpgsqlDataAdapter()
        Dim table As New DataTable()
        Dim commandCheck As New NpgsqlCommand("SELECT movieid FROM users_info WHERE id = @UserID;", mydb.getConnection)

        commandCheck.Parameters.AddWithValue("@UserID", userid)
        adapter.SelectCommand = commandCheck
        adapter.Fill(table)

        If table.Rows.Count > 0 AndAlso Not table.Rows(0)("movieid") Is DBNull.Value Then

            Dim movieIds As Integer() = CType(table.Rows(0)("movieid"), Integer())

            For Each movieId In movieIds
                ' Call GetMovieInfo for each movie ID
                Dim movie As TmdbMovie = Await movieInfo.GetMovieInfoWL(movieId)
                If movie IsNot Nothing Then
                    ' Update the list of movies
                    movieList.Add(movie)
                End If

                UpdateListView(movieList)
            Next
            ' Display movies in ListView
        End If
    End Sub
    Private Async Sub UpdateListView(movies As List(Of TmdbMovie))
        Dim posters As New ImageList()
        posters.ImageSize = New Size(100, 140)
        Me.Controls.Add(ListViewMovies)

        ' Configure ListView properties
        ListViewMovies.Dock = DockStyle.Fill
        ListViewMovies.View = View.Details

        ' Add columns
        ListViewMovies.Clear()
        ListViewMovies.Columns.Add("Poster", 100)
        ListViewMovies.Columns.Add("Name", 200)
        ListViewMovies.Columns.Add("Release year", 80)
        ListViewMovies.Columns.Add("Length", 80)
        ListViewMovies.Columns.Add("Rating", 50)
        ListViewMovies.Columns.Add("Overview", 300)
        ListViewMovies.SmallImageList = posters

        For i As Integer = 0 To movies.Count - 1
            Dim movie As TmdbMovie = movies(i)
            Dim item As New ListViewItem()
            Dim runtimeString As String = If(movie.time >= 0, $"{movie.time} min", "N/A")

            ' Load poster image
            Dim posterUrl As String = $"https://image.tmdb.org/t/p/w500{movie.poster_path}"
            Dim poster As Image = LoadImageFromUrl(posterUrl)
            If poster IsNot Nothing Then
                posters.Images.Add(movie.id.ToString(), poster)
                item.ImageKey = movie.id.ToString()
            End If

            ' Add other details
            item.SubItems.Add(movie.title)
            item.SubItems.Add(If(Not String.IsNullOrEmpty(movie.release_date), movie.release_date.Substring(0, 4), "N/A"))
            item.SubItems.Add(runtimeString)
            item.SubItems.Add(movie.vote_average.ToString("0.0"))
            item.SubItems.Add(movie.overview)

            ' Add the item to the ListView
            ListViewMovies.Items.Add(item)
        Next
    End Sub
    Private Sub ListViewMovies_MouseClick(sender As Object, e As MouseEventArgs) Handles ListViewMovies.MouseClick
        ' Check if an item is clicked
        Dim selectedItem As ListViewItem = ListViewMovies.GetItemAt(e.X, e.Y)
        If selectedItem IsNot Nothing Then
            ' Retrieve the corresponding movie from the movieList
            Dim selectedMovieIndex As Integer = selectedItem.Index
            If selectedMovieIndex >= 0 AndAlso selectedMovieIndex < movieList.Count Then
                Dim selectedMovie As TmdbMovie = movieList(selectedMovieIndex)
                ' Open the DetailsForm for the selected movie
                Dim detailsForm As New DetailsForm(Me, selectedMovie, userid)
                detailsForm.Show()
                detailsForm.btnWatchlist.Hide()
                detailsForm.btnBlock.Hide()
                Me.Close()
            End If
        End If
    End Sub
    Public Function LoadImageFromUrl(url As String) As Image
        Try
            Using client As New WebClient()
                Using stream As Stream = client.OpenRead(url)
                    Return Image.FromStream(stream)
                End Using
            End Using
        Catch ex As Exception

            Return Nothing
        End Try
    End Function
End Class