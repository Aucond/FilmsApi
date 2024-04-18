Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports System.Net.Http
Public Class MOVIE_FORM
    Private WithEvents searchTimer As New System.Windows.Forms.Timer()
    Private Async Sub searchTimer_Tick(sender As Object, e As EventArgs) Handles searchTimer.Tick

        searchTimer.Stop()
        Dim CSearch As New CSearch
        Dim searchQuery As String = txtboxSearch.Text
        If Not String.IsNullOrWhiteSpace(searchQuery) Then
            Await CSearch.SearchMoviesAsync(searchQuery)
        Else
            ListViewMovies.Items.Clear()
        End If

    End Sub

    Private Sub txtboxSearch_TextChanged(sender As Object, e As EventArgs) Handles txtboxSearch.TextChanged

        searchTimer.Stop()
        searchTimer.Start()

    End Sub
    Public Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim list As New CLists
        For Each kvp As KeyValuePair(Of Integer, String) In list.genreDictionary
            cmbboxFilter.Items.Add(kvp.Value)
        Next

        For Each kvp As KeyValuePair(Of String, Integer) In list.companyNames
            cmbboxCompanies.Items.Add(kvp.Key)
        Next

    End Sub
    Public Async Sub btnFamilyFriendly_Click(sender As Object, e As EventArgs) Handles btnFamilyFriendly.Click

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

    Public Async Sub UpdateListView(searchResults As TmdbSearchResult)

        ' Create ImageList and configure ListView
        Dim posters As New ImageList()
        posters.ImageSize = New Size(100, 140) ' Approximate poster size

        ' Create a dictionary for genres
        ' Configure ListView columns
        ListViewMovies.Clear()
        ListViewMovies.View = View.Details
        ListViewMovies.Columns.Add("Name", 300)
        ListViewMovies.Columns.Add("Year", 100)
        ListViewMovies.Columns.Add("Genres", 100)
        ListViewMovies.Columns.Add("Rating", 50)
        ListViewMovies.Columns.Add("Length", 50)
        ListViewMovies.Columns.Add("Vote Count", 50)
        ListViewMovies.Columns.Add("Company", 50)
        ListViewMovies.SmallImageList = posters

        Dim allFilms As New List(Of Integer)()
        Dim runtime As New List(Of Integer)()

        ' Populate ListView with items
        For Each movie In searchResults.results
            Dim CSearch As New CSearch
            Dim list As New CLists
            Dim item As New ListViewItem(movie.title)
            Dim year As String = If(Not String.IsNullOrEmpty(movie.release_date) AndAlso movie.release_date.Length >= 4, movie.release_date.Substring(0, 4), "N/A")
            Dim genreNames As New List(Of String)()
            Dim movieRuntime As Integer = Await CSearch.SearchMovieRuntimeAsync(movie.id)
            Dim runtimeString As String = If(movieRuntime >= 0, $"{movieRuntime} min", "N/A")

            ' Retrieve genre names based on genre IDs
            For Each genreId In movie.genre_ids
                If list.genreDictionary.ContainsKey(genreId) Then
                    genreNames.Add(list.genreDictionary(genreId))
                End If
            Next

            Dim productionCompanies As String = Await CSearch.GetProductionCompanyNamesAsync(movie.id)
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
            item.SubItems.Add(productionCompanies)

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
    Private Sub cmbboxFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbboxFilter.SelectedIndexChanged

        ' Get the selected genre name
        Dim selectedGenreName As String = cmbboxFilter.SelectedItem.ToString()
        Dim CSearch As New CSearch
        Dim list As New CLists
        ' Find the corresponding genre ID from genreDictionary
        Dim genreId As Integer = -1 ' Default value if genre ID is not found

        For Each kvp As KeyValuePair(Of Integer, String) In List.genreDictionary
            If kvp.Value = selectedGenreName Then
                genreId = kvp.Key
                Exit For
            End If
        Next
        CSearch.FilterMoviesAsync(genreId)

    End Sub

    Private Sub cmbboxSort_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbboxSort.SelectedIndexChanged

        ' Get the selected sorting option
        Dim selectedSortOption As String = cmbboxSort.SelectedItem.ToString()

        ' Sort movies based on the selected option
        Select Case selectedSortOption
            Case "Ascending length"
                SortMoviesByLength(ascending:=True)
            Case "Descending length"
                SortMoviesByLength(ascending:=False)
            Case "Ascending rating"
                SortMoviesByRating(ascending:=True)
            Case "Descending rating"
                SortMoviesByRating(ascending:=False)
            Case "Ascending year"
                SortMoviesByReleaseDate(ascending:=True)
            Case "Descending year"
                SortMoviesByReleaseDate(ascending:=False)
            Case "Ascending vote"
                SortMoviesByVoteCount(ascending:=True)
            Case "Descending vote"
                SortMoviesByVoteCount(ascending:=False)
        End Select

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

    Private Sub ListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListToolStripMenuItem.Click

        Me.Hide()
        LOGIN_FORM.Show()

    End Sub

    Private Async Sub cmbboxCompanies_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbboxCompanies.SelectedIndexChanged

        Dim CSearch As New CSearch
        If cmbboxCompanies.SelectedItem IsNot Nothing Then
            Dim companies As String = cmbboxCompanies.SelectedItem.ToString()
            Await CSearch.SearchMoviesByCompanyAsync(companies)
        End If

    End Sub

End Class