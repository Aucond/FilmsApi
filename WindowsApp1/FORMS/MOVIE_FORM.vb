Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports System.Net.Http
Public Class MOVIE_FORM
    Private WithEvents searchTimer As New System.Windows.Forms.Timer()
    Dim genreDictionary As New Dictionary(Of Integer, String) From {
        {28, "Action"},
        {12, "Adventure"},
        {16, "Animation"},
        {35, "Comedy"},
        {80, "Crime"},
        {99, "Documentary"},
        {18, "Drama"},
        {10751, "Family"},
        {14, "Fantasy"},
        {36, "History"},
        {27, "Horror"},
        {10402, "Music"},
        {9648, "Mystery"},
        {10749, "Romance"},
        {878, "Science Fiction"},
        {10770, "TV Movie"},
        {53, "Thriller"},
        {10752, "War"},
        {37, "Western"}
    }
    Private Async Sub searchTimer_Tick(sender As Object, e As EventArgs) Handles searchTimer.Tick
        searchTimer.Stop()
        Dim searchQuery As String = TextBox1.Text
        If Not String.IsNullOrWhiteSpace(searchQuery) Then
            Await SearchMoviesAsync(searchQuery)
        Else
            ListViewMovies.Items.Clear()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

        searchTimer.Stop()
        searchTimer.Start()

    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        For Each kvp As KeyValuePair(Of Integer, String) In genreDictionary
            ComboBox3.Items.Add(kvp.Value)
        Next

    End Sub
    Public Async Sub Under18(age As Integer)
        ' Here you can use the age parameter as needed
        If age < 18 Then
            Me.Show()
            TextBox1.Hide()
            Label2.Hide()
            ComboBox3.Hide()
            Button1.Hide()
            ListToolStripMenuItem.Visible = False
            Dim apiKey As String = "0d77f86880fc2d980da7ba1ab371bdbb"
            Dim requestUrl As String = $"https://api.themoviedb.org/3/discover/movie?api_key={apiKey}&with_genres=10751"

            Using httpClient As New HttpClient()
                Dim response As String = Await httpClient.GetStringAsync(requestUrl)
                Dim searchResults = JsonConvert.DeserializeObject(Of TmdbSearchResult)(response)

                If searchResults IsNot Nothing AndAlso searchResults.results.Count > 0 Then
                    If ListViewMovies.InvokeRequired Then
                        ListViewMovies.Invoke(Sub() UpdateListView(searchResults))
                    Else
                        UpdateListView(searchResults)
                    End If

                End If
            End Using
        End If
    End Sub
    Public Async Sub Guest(age As Integer)
        ' Here you can use the age parameter as needed
        If age < 18 Then
            Me.Show()
            TextBox1.Hide()
            Label2.Hide()
            ComboBox3.Hide()
            Button1.Hide()
            AccountToolStripMenuItem.Visible = False
            Dim apiKey As String = "0d77f86880fc2d980da7ba1ab371bdbb"
            Dim requestUrl As String = $"https://api.themoviedb.org/3/discover/movie?api_key={apiKey}&with_genres=10751"

            Using httpClient As New HttpClient()
                Dim response As String = Await httpClient.GetStringAsync(requestUrl)
                Dim searchResults = JsonConvert.DeserializeObject(Of TmdbSearchResult)(response)

                If searchResults IsNot Nothing AndAlso searchResults.results.Count > 0 Then
                    If ListViewMovies.InvokeRequired Then
                        ListViewMovies.Invoke(Sub() UpdateListView(searchResults))
                    Else
                        UpdateListView(searchResults)
                    End If

                End If
            End Using
        End If
    End Sub
    Public Async Function SearchMoviesAsync(searchQuery As String) As Task
        Dim apiKey As String = "0d77f86880fc2d980da7ba1ab371bdbb"
        Dim requestUrl As String = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&query={Uri.EscapeDataString(searchQuery)}"

        Using httpClient As New HttpClient()
            Dim response As String = Await httpClient.GetStringAsync(requestUrl)
            Dim searchResults = JsonConvert.DeserializeObject(Of TmdbSearchResult)(response)

            If searchResults IsNot Nothing AndAlso searchResults.results.Count > 0 Then
                If ListViewMovies.InvokeRequired Then
                    ListViewMovies.Invoke(Sub() UpdateListView(searchResults))
                Else
                    UpdateListView(searchResults)
                End If

            End If

        End Using

    End Function

    Public Async Function SearchMovieRuntimeAsync(filmId As Integer) As Task(Of Integer)

        Dim apiKey As String = "0d77f86880fc2d980da7ba1ab371bdbb"
        Dim requestUrl As String = $"https://api.themoviedb.org/3/movie/{filmId}?api_key={apiKey}"

        Using httpClient As New HttpClient()
            Dim response As String = Await httpClient.GetStringAsync(requestUrl)
            Dim movieDetails = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)

            If movieDetails IsNot Nothing AndAlso movieDetails.ContainsKey("runtime") Then
                Return Convert.ToInt32(movieDetails("runtime"))
            Else
                Return -1 ' Indicate runtime not found
            End If
        End Using

    End Function
    Public Async Function FilterMoviesAsync(genreId As String) As Task

        Dim apiKey As String = "0d77f86880fc2d980da7ba1ab371bdbb"
        Dim requestUrl As String = $"https://api.themoviedb.org/3/discover/movie?api_key={apiKey}&with_genres={genreId}"

        Using httpClient As New HttpClient()
            Dim response As String = Await httpClient.GetStringAsync(requestUrl)
            Dim searchResults = JsonConvert.DeserializeObject(Of TmdbSearchResult)(response)

            If searchResults IsNot Nothing AndAlso searchResults.results.Count > 0 Then
                If ListViewMovies.InvokeRequired Then
                    ListViewMovies.Invoke(Sub() UpdateListView(searchResults))
                Else
                    UpdateListView(searchResults)
                End If

            End If
        End Using

    End Function
    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim apiKey As String = "0d77f86880fc2d980da7ba1ab371bdbb"
        Dim requestUrl As String = $"https://api.themoviedb.org/3/discover/movie?api_key={apiKey}&with_genres=10751"

        Using httpClient As New HttpClient()
            Dim response As String = Await httpClient.GetStringAsync(requestUrl)
            Dim searchResults = JsonConvert.DeserializeObject(Of TmdbSearchResult)(response)

            If searchResults IsNot Nothing AndAlso searchResults.results.Count > 0 Then
                If ListViewMovies.InvokeRequired Then
                    ListViewMovies.Invoke(Sub() UpdateListView(searchResults))
                Else
                    UpdateListView(searchResults)
                End If

            End If
        End Using
    End Sub
    Private Async Sub UpdateListView(searchResults As TmdbSearchResult)

        ' Create ImageList and configure ListView
        Dim posters As New ImageList()
        posters.ImageSize = New Size(100, 140) ' Approximate poster size

        ' Create a dictionary for genres
        ' Configure ListView columns
        ListViewMovies.Clear()
        ListViewMovies.View = View.Details
        ListViewMovies.Columns.Add("Name", 300)
        ListViewMovies.Columns.Add("Year", 100)
        ListViewMovies.Columns.Add("Genres", 200)
        ListViewMovies.Columns.Add("Rating", 50)
        ListViewMovies.Columns.Add("Length", 50)
        ListViewMovies.Columns.Add("Vote Count", 50)
        ListViewMovies.SmallImageList = posters

        Dim allFilms As New List(Of Integer)()
        Dim runtime As New List(Of Integer)()

        ' Populate ListView with items
        For Each movie In searchResults.results
            Dim item As New ListViewItem(movie.title)
            Dim year As String = If(Not String.IsNullOrEmpty(movie.release_date) AndAlso movie.release_date.Length >= 4, movie.release_date.Substring(0, 4), "N/A")
            Dim genreNames As New List(Of String)()
            Dim movieRuntime As Integer = Await SearchMovieRuntimeAsync(movie.id)
            Dim runtimeString As String = If(movieRuntime >= 0, $"{movieRuntime} min", "N/A")

            ' Retrieve genre names based on genre IDs
            For Each genreId In movie.genre_ids
                If genreDictionary.ContainsKey(genreId) Then
                    genreNames.Add(genreDictionary(genreId))
                End If
            Next

            Dim genres As String = String.Join(", ", genreNames) ' Join genre names with a comma
            Dim rating As String = movie.vote_average.ToString("0.0") ' Format rating to one decimal place
            Dim voteCount As String = movie.vote_count.ToString()
            Dim adultContent As String = movie.adult.ToString()
            allFilms.Add(movie.id)

            item.SubItems.Add(year)
            item.SubItems.Add(genres)
            item.SubItems.Add(rating)
            item.SubItems.Add(runtimeString)
            item.SubItems.Add(voteCount)

            ' If adultContent Is "false" Then
            ' item.SubItems.Add("No")
            ' Else
            ' item.SubItems.Add("Yes")
            ' End If

            ' Load poster image
            Dim posterUrl As String = $"https://image.tmdb.org/t/p/w500{movie.poster_path}"
            Dim poster As Image = LoadImageFromUrl(posterUrl) ' Make sure LoadImageFromUrl supports synchronous requests
            If poster IsNot Nothing Then
                posters.Images.Add(movie.id.ToString(), poster)
                item.ImageKey = movie.id.ToString()
            End If

            ListViewMovies.Items.Add(item)
        Next

        ' Automatically adjust column widths for better viewing
        For i As Integer = 0 To ListViewMovies.Columns.Count - 1
            ListViewMovies.Columns(i).Width = -2
        Next

    End Sub

    Private Function LoadImageFromUrl(url As String) As Image

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
    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged

        ' Get the selected genre name
        Dim selectedGenreName As String = ComboBox3.SelectedItem.ToString()

        ' Find the corresponding genre ID from genreDictionary
        Dim genreId As Integer = -1 ' Default value if genre ID is not found

        For Each kvp As KeyValuePair(Of Integer, String) In genreDictionary
            If kvp.Value = selectedGenreName Then
                genreId = kvp.Key
                Exit For
            End If
        Next
        FilterMoviesAsync(genreId)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        ' Get the selected sorting option
        Dim selectedSortOption As String = ComboBox1.SelectedItem.ToString()

        ' Sort movies based on the selected option
        If selectedSortOption = "Ascending length" Then
            SortMoviesByLength(ascending:=True)
        ElseIf selectedSortOption = "Descending length" Then
            SortMoviesByLength(ascending:=False)
        ElseIf selectedSortOption = "Ascending rating" Then
            SortMoviesByRating(ascending:=True)
        ElseIf selectedSortOption = "Descending rating" Then
            SortMoviesByRating(ascending:=False)
        ElseIf selectedSortOption = "Ascending year" Then
            SortMoviesByReleaseDate(ascending:=True)
        ElseIf selectedSortOption = "Descending year" Then
            SortMoviesByReleaseDate(ascending:=False)
        ElseIf selectedSortOption = "Ascending vote" Then
            SortMoviesByVoteCount(ascending:=True)
        ElseIf selectedSortOption = "Descending vote" Then
            SortMoviesByVoteCount(ascending:=False)
        End If

    End Sub
    Private Sub SortMoviesByLength(ascending As Boolean)

        ' Toggle sorting order

        ' Sort ListView items by movie length (runtime)
        ListViewMovies.ListViewItemSorter = New ListViewRuntimeComparer(ascending)
        ListViewMovies.Sort()

    End Sub

    Private Sub SortMoviesByRating(ascending As Boolean)

        ' Toggle sorting order

        ' Sort ListView items by movie rating
        ListViewMovies.ListViewItemSorter = New ListViewRatingComparer(ascending)
        ListViewMovies.Sort()

    End Sub

    Private Sub SortMoviesByReleaseDate(ascending As Boolean)

        ' Toggle sorting order

        ' Sort ListView items by movie release date
        ListViewMovies.ListViewItemSorter = New ListViewReleaseDateComparer(ascending)
        ListViewMovies.Sort()

    End Sub

    Private Sub SortMoviesByVoteCount(ascending As Boolean)

        ' Sort ListView items by movie vote count
        ListViewMovies.ListViewItemSorter = New ListViewVoteCountComparer(ascending)
        ListViewMovies.Sort()

    End Sub

End Class