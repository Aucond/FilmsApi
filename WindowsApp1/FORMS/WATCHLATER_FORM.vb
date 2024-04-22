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
                Dim movie As TmdbMovie = Await GetMovieInfo(movieId)
                If movie IsNot Nothing Then
                    ' Update the list of movies
                    movieList.Add(movie)
                End If

                UpdateListView(movieList)
            Next
            ' Display movies in ListView
        End If
    End Sub
    Private Async Function GetMovieInfo(movieID As Integer) As Task(Of TmdbMovie)
        Dim apiKey As String = "0d77f86880fc2d980da7ba1ab371bdbb"
        Dim url As String = $"https://api.themoviedb.org/3/movie/{movieID}?api_key={apiKey}"
        Dim request As HttpWebRequest = WebRequest.Create(url)

        Using response As HttpWebResponse = request.GetResponse()
            Using reader As New StreamReader(response.GetResponseStream())
                Dim jsonResponse As String = reader.ReadToEnd()
                Dim jsonObject As JObject = JObject.Parse(jsonResponse)
                Dim movie As New TmdbMovie()

                movie.id = jsonObject("id")
                movie.title = jsonObject("title")
                movie.release_date = jsonObject("release_date")
                movie.overview = jsonObject("overview")
                movie.poster_path = jsonObject("poster_path")
                movie.time = jsonObject("runtime")
                movie.vote_average = jsonObject("vote_average")

                Return movie
            End Using
        End Using
    End Function
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

    Private Sub ListViewMovies_MouseClick(sender As Object, e As MouseEventArgs) Handles ListViewMovies.MouseClick
        Dim info As ListViewHitTestInfo = ListViewMovies.HitTest(e.Location)
        If info.Item IsNot Nothing Then
            Dim movie As TmdbMovie = TryCast(info.Item.Tag, TmdbMovie)
            If movie IsNot Nothing AndAlso Not String.IsNullOrEmpty(movie.poster_path) Then
                ' Assuming that poster images are in the first column
                Dim itemRect As Rectangle = info.Item.GetBounds(ItemBoundsPortion.Icon)

                ' Check if the click was on the poster image
                If itemRect.Contains(e.Location) Then
                    ' Pass the current user ID to the OpenDetailsForm method
                    Dim detailsForm As New DetailsForm(ParentForm, movie, userid)
                    detailsForm.Show()
                    MessageBox.Show("XD")
                End If
            End If
        End If
    End Sub
End Class