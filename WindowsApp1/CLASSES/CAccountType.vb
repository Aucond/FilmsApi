Imports Newtonsoft.Json
Imports System.Net.Http

Public Class CAccountType
    Public Async Sub Under18(age As Integer)
        Dim CUpdateView As New CUpdateView
        If age < 18 Then
            MOVIE_FORM.Show()
            MOVIE_FORM.txtboxSearch.Hide()
            MOVIE_FORM.Label2.Hide()
            MOVIE_FORM.cmbboxFilter.Hide()
            MOVIE_FORM.btnFamilyFriendly.Hide()
            MOVIE_FORM.cmbboxCompanies.Hide()
            MOVIE_FORM.Label3.Hide()
            MOVIE_FORM.ListToolStripMenuItem.Visible = False
            MOVIE_FORM.AccountToolStripMenuItem.Visible = True
            Dim apiKey As String = "0d77f86880fc2d980da7ba1ab371bdbb"
            Dim requestUrl As String = $"https://api.themoviedb.org/3/discover/movie?api_key={apiKey}&with_genres=10751"

            Using httpClient As New HttpClient()
                Dim response As String = Await httpClient.GetStringAsync(requestUrl)
                Dim searchResults = JsonConvert.DeserializeObject(Of TmdbSearchResult)(response)

                If searchResults IsNot Nothing AndAlso searchResults.results.Count > 0 Then
                    If MOVIE_FORM.ListViewMovies.InvokeRequired Then
                        MOVIE_FORM.ListViewMovies.Invoke(Sub() CUpdateView.UpdateListView(searchResults))
                    Else
                        CUpdateView.UpdateListView(searchResults)
                    End If

                End If
            End Using
        End If
    End Sub
    Public Async Sub Guest(age As Integer)
        Dim CUpdateView As New CUpdateView
        If age < 18 Then
            MOVIE_FORM.Show()
            MOVIE_FORM.txtboxSearch.Hide()
            MOVIE_FORM.Label2.Hide()
            MOVIE_FORM.cmbboxFilter.Hide()
            MOVIE_FORM.btnFamilyFriendly.Hide()
            MOVIE_FORM.cmbboxCompanies.Hide()
            MOVIE_FORM.Label3.Hide()
            MOVIE_FORM.AccountToolStripMenuItem.Visible = False
            Dim apiKey As String = "0d77f86880fc2d980da7ba1ab371bdbb"
            Dim requestUrl As String = $"https://api.themoviedb.org/3/discover/movie?api_key={apiKey}&with_genres=10751"

            Using httpClient As New HttpClient()
                Dim response As String = Await httpClient.GetStringAsync(requestUrl)
                Dim searchResults = JsonConvert.DeserializeObject(Of TmdbSearchResult)(response)

                If searchResults IsNot Nothing AndAlso searchResults.results.Count > 0 Then
                    If MOVIE_FORM.ListViewMovies.InvokeRequired Then
                        MOVIE_FORM.ListViewMovies.Invoke(Sub() CUpdateView.UpdateListView(searchResults))
                    Else
                        CUpdateView.UpdateListView(searchResults)
                    End If

                End If
            End Using
        End If
    End Sub
End Class
