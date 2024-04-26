Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports System.Net.Http
Public Class MOVIE_FORM
    Dim _userid As Integer = LOGIN_FORM.personID

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

    Public Function idNR(nr As Integer)
        Return nr
    End Function

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
        Dim CUpdateView As New CUpdateView
        Dim apiKey As String = "0d77f86880fc2d980da7ba1ab371bdbb"
        Dim requestUrl As String = $"https://api.themoviedb.org/3/discover/movie?api_key={apiKey}&with_genres=10751"

        Using httpClient As New HttpClient()
            Dim response As String = Await httpClient.GetStringAsync(requestUrl)
            Dim searchResults = JsonConvert.DeserializeObject(Of TmdbSearchResult)(response)

            If searchResults IsNot Nothing AndAlso searchResults.results.Count > 0 Then
                If ListViewMovies.InvokeRequired Then
                    ListViewMovies.Invoke(Sub() CUpdateView.UpdateListView(searchResults))
                Else
                    CUpdateView.UpdateListView(searchResults)
                End If

            End If
        End Using

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
    Private Sub cmbboxFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbboxFilter.SelectedIndexChanged

        ' Get the selected genre name
        Dim selectedGenreName As String = cmbboxFilter.SelectedItem.ToString()
        Dim CSearch As New CSearch
        Dim list As New CLists
        ' Find the corresponding genre ID from genreDictionary
        Dim genreId As Integer = -1 ' Default value if genre ID is not found

        For Each kvp As KeyValuePair(Of Integer, String) In list.genreDictionary
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

    Private Sub ListViewMovies_MouseClick(sender As Object, e As MouseEventArgs) Handles ListViewMovies.MouseClick
        Dim info As ListViewHitTestInfo = ListViewMovies.HitTest(e.Location)
        If info.Item IsNot Nothing Then
            Dim movie As TmdbMovie = TryCast(info.Item.Tag, TmdbMovie)
            If movie IsNot Nothing AndAlso Not String.IsNullOrEmpty(movie.poster_path) Then
                Dim itemRect As Rectangle = info.Item.GetBounds(ItemBoundsPortion.Icon)

                ' Check if the click was on the poster image
                If itemRect.Contains(e.Location) Then
                    ' Pass the current user ID to the OpenDetailsForm method
                    Dim detailsForm As New DetailsForm(ParentForm, movie, _userid)
                    detailsForm.Show()
                    detailsForm.btnRemove.Hide()
                End If
            End If
        End If
    End Sub

    Private Sub AccountToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AccountToolStripMenuItem.Click
        Dim watchLater As New WATCHLATER_FORM
        WATCHLATER_FORM.Show()
        watchLater.AccessMovieID()
    End Sub
End Class