Imports Npgsql
Imports PrjDatabase

Public Class CUpdateView
    Dim userid As Integer = LOGIN_FORM.personID
    Public Async Sub UpdateListView(searchResults As TmdbSearchResult)

        If searchResults.results.Count < 1 Then
            MessageBox.Show("No search results found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim databaseConnection As PrjDatabase.IDatabase
        databaseConnection = New PrjDatabase.CDatabase

        ' Create ImageList and configure ListView
        Dim posters As New ImageList()
        posters.ImageSize = New Size(100, 140)

        ' Create a dictionary for genres
        ' Configure ListView columns
        MOVIE_FORM.ListViewMovies.Clear()
        MOVIE_FORM.ListViewMovies.View = View.Details
        MOVIE_FORM.ListViewMovies.Columns.Add("Name", 300)
        MOVIE_FORM.ListViewMovies.Columns.Add("Year", 100)
        MOVIE_FORM.ListViewMovies.Columns.Add("Genres", 100)
        MOVIE_FORM.ListViewMovies.Columns.Add("Rating", 50)
        MOVIE_FORM.ListViewMovies.Columns.Add("Length", 50)
        MOVIE_FORM.ListViewMovies.Columns.Add("Vote Count", 50)
        MOVIE_FORM.ListViewMovies.Columns.Add("Company", 50)
        MOVIE_FORM.ListViewMovies.SmallImageList = posters


        Dim allFilms As New List(Of Integer)()
        Dim runtime As New List(Of Integer)()

        Dim blockedMovieIds As New List(Of Integer)()
        Dim mydb As New CDatabase()
        Dim commandBlockedMovies As New NpgsqlCommand("SELECT blockid FROM users_info WHERE id = @UserID;", mydb.getConnection)
        commandBlockedMovies.Parameters.AddWithValue("@UserID", userid)

        Dim adapter As New NpgsqlDataAdapter(commandBlockedMovies)
        Dim table As New DataTable()
        adapter.Fill(table)

        If table.Rows.Count > 0 AndAlso Not table.Rows(0)("blockid") Is DBNull.Value Then
            blockedMovieIds.AddRange(CType(table.Rows(0)("blockid"), Integer()))
        End If

        ' Populate ListView with items
        For Each movie In searchResults.results

            If blockedMovieIds.Contains(movie.id) Then
                Continue For ' Skip this movie if it's blocked
            End If

            Dim CSearch As New CSearch
            Dim list As New CLists
            Dim item As New ListViewItem(movie.title)
            Dim year As String = If(Not String.IsNullOrEmpty(movie.release_date) AndAlso movie.release_date.Length >= 4, movie.release_date.Substring(0, 4), "N/A")
            Dim genreNames As New List(Of String)()
            Dim movieRuntime As Integer = Await CSearch.SearchMovieRuntimeAsync(movie.id)
            Dim runtimeString As String = If(movieRuntime >= 0, $"{movieRuntime} min", "N/A")
            item.Tag = movie

            If movieRuntime < 0.5 Then
                Continue For
            End If

            ' Retrieve genre names based on genre IDs
            For Each genreId In movie.genre_ids
                If list.genreDictionary.ContainsKey(genreId) Then
                    genreNames.Add(list.genreDictionary(genreId))
                End If
            Next

            Dim productionCompanies As String = Await CSearch.GetProductionCompanyNamesAsync(movie.id)
            Dim genres As String = String.Join(", ", genreNames) ' Join genre names with a comma
            Dim rating As String = movie.vote_average.ToString("0.0")
            Dim voteCount As String = movie.vote_count.ToString()
            Dim adultContent As String = movie.adult.ToString()
            allFilms.Add(movie.id)

            item.SubItems.Add(year)
            item.SubItems.Add(genres)
            item.SubItems.Add(rating)
            item.SubItems.Add(runtimeString)
            item.SubItems.Add(voteCount)
            item.SubItems.Add(productionCompanies)

            Dim posterUrl As String = $"https://image.tmdb.org/t/p/w500{movie.poster_path}"
            Dim poster As Image = MOVIE_FORM.LoadImageFromUrl(posterUrl)
            If poster IsNot Nothing Then
                posters.Images.Add(movie.id.ToString(), poster)
                item.ImageKey = movie.id.ToString()
            End If

            MOVIE_FORM.ListViewMovies.Items.Add(item)
        Next
        ' Automatically adjust column widths for better viewing
        For i As Integer = 0 To MOVIE_FORM.ListViewMovies.Columns.Count - 1
            MOVIE_FORM.ListViewMovies.Columns(i).Width = -2
        Next

    End Sub

End Class
