Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports System.Net.Http
Imports Npgsql
Imports PrjDatabase
Imports System.Data.SqlClient
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

        If _userid <= 0 Then
            ' Hide the status menu item and the "Mark as Viewed" button if the user is a guest
            StatusToolStripMenuItem.Visible = False

        Else
            ' Show the status menu item and the "Mark as Viewed" button if the user is not a guest
            StatusToolStripMenuItem.Visible = True

        End If

        If _userid = 9 Then
            Label6.Show()
            Label7.Show()
            txtBoxDelimiter.Show()
            txtBoxQualifier.Show()
            chkBoxData.Show()
            Label4.Show()
            Label5.Show()
            btnDwnld.Show()
        End If

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

    Private Sub btnDwnldl_Click(sender As Object, e As EventArgs) Handles btnDwnld.Click
        Dim exporter As CSVExporterDNF.IExporter
        exporter = New CSVExporterDNF.CExporter
        Dim dataAppend As Boolean = False

        Dim mydb As New CDatabase
        Dim adapter As New NpgsqlDataAdapter()
        Dim table As New DataTable()
        Dim command As New NpgsqlCommand("SELECT * FROM users_info", mydb.getConnection)

        If txtBoxDelimiter.Text = "" Then
            exporter.delimiter = ":"
        Else
            exporter.delimiter = txtBoxDelimiter.Text
        End If

        If txtBoxQualifier.Text = "" Then
            exporter.textQualifier = ""
        Else
            exporter.textQualifier = txtBoxQualifier.Text
        End If

        If chkBoxData.CheckState Then
            dataAppend = True
        End If

        Dim filePath As String = exporter.setFileToSave()

        adapter.SelectCommand = command
        adapter.Fill(table)

        If table.Rows.Count > 0 Then
            ' Convert DataTable to two-dimensional array
            Dim data(table.Rows.Count - 1, table.Columns.Count - 1) As String
            For i As Integer = 0 To table.Rows.Count - 1
                For j As Integer = 0 To table.Columns.Count - 1
                    If table.Columns(j).ColumnName = "movieid" Or table.Columns(j).ColumnName = "blockid" Then
                        ' Handle DBNull values in array columns
                        If Not table.Rows(i)(j) Is DBNull.Value Then
                            Dim arrayValues As Integer() = DirectCast(table.Rows(i)(j), Integer())
                            data(i, j) = String.Join(",", arrayValues.Select(Function(x) x.ToString()))
                        Else
                            data(i, j) = String.Empty ' Set an empty string for DBNull values
                        End If
                    Else
                        ' For non-array columns, convert to string
                        data(i, j) = table.Rows(i)(j).ToString()
                    End If
                Next
            Next

            ' Export data to CSV file
            exporter.saveDataToCsv(data, dataAppend)

            ' Display a message indicating the number of rows written and the file path
        Else
            MessageBox.Show("No data found in the table.")
        End If
    End Sub
    Private Function CalculateTotalViewTime(userId As Integer) As Integer
        Dim mydb As New CDatabase()
        Dim totalViewTime As Integer = 0

        Try
            mydb.openConnection()
            Dim command As New NpgsqlCommand("SELECT SUM(Duration) AS TotalViewTime FROM MovieViewedStatus WHERE UserID = @UserID AND Viewed = TRUE;", mydb.getConnection())
            command.Parameters.AddWithValue("@UserID", userId)

            Dim result = command.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                totalViewTime = Convert.ToInt32(result)
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred while calculating the total view time: " & ex.Message)
        Finally
            mydb.closeConnection()
        End Try

        Return totalViewTime
    End Function

    Private Sub StatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StatusToolStripMenuItem.Click
        Dim totalMinutes As Integer = CalculateTotalViewTime(_userid)
        MessageBox.Show(String.Format("You have spent a total of {0} minutes watching movies.", totalMinutes))
    End Sub
End Class