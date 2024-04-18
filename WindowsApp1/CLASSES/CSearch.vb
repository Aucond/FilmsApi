Imports Newtonsoft.Json
Imports System.Net.Http

Public Class CSearch
    Public Async Function SearchMoviesAsync(searchQuery As String) As Task
        Dim apiKey As String = "0d77f86880fc2d980da7ba1ab371bdbb"
        Dim requestUrl As String = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&query={Uri.EscapeDataString(searchQuery)}"

        Using httpClient As New HttpClient()
            Dim response As String = Await httpClient.GetStringAsync(requestUrl)
            Dim searchResults = JsonConvert.DeserializeObject(Of TmdbSearchResult)(response)

            If searchResults IsNot Nothing AndAlso searchResults.results.Count > 0 Then
                If MOVIE_FORM.ListViewMovies.InvokeRequired Then
                    MOVIE_FORM.ListViewMovies.Invoke(Sub() MOVIE_FORM.UpdateListView(searchResults))
                Else
                    MOVIE_FORM.UpdateListView(searchResults)
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
                If MOVIE_FORM.ListViewMovies.InvokeRequired Then
                    MOVIE_FORM.ListViewMovies.Invoke(Sub() MOVIE_FORM.UpdateListView(searchResults))
                Else
                    MOVIE_FORM.UpdateListView(searchResults)
                End If

            End If
        End Using

    End Function
    Public Async Function GetProductionCompanyNamesAsync(movieId As Integer) As Task(Of String)

        Dim apiKey As String = "0d77f86880fc2d980da7ba1ab371bdbb"
        Dim requestUrl As String = $"https://api.themoviedb.org/3/movie/{movieId}?api_key={apiKey}"

        Using httpClient As New HttpClient()
            Dim response As String = Await httpClient.GetStringAsync(requestUrl)
            Dim movieDetails = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)

            Dim productionCompanies As New List(Of String)()
            Dim productionCompaniesID As New List(Of Integer)()

            If movieDetails IsNot Nothing AndAlso movieDetails.ContainsKey("production_companies") Then
                For Each company In movieDetails("production_companies")
                    productionCompanies.Add(company("name").ToString())
                    productionCompaniesID.Add(Convert.ToInt32(company("id")))
                Next
            End If
            Return String.Join(", ", productionCompanies)
        End Using

    End Function
    Public Async Function SearchMoviesByCompanyAsync(companyName As String) As Task
        Dim list As New CLists
        Dim apiKey As String = "0d77f86880fc2d980da7ba1ab371bdbb"
        Dim companyId As Integer = list.companyNames(companyName)
        Dim requestUrl As String = $"https://api.themoviedb.org/3/discover/movie?api_key={apiKey}&with_companies={companyId}"

        Using httpClient As New HttpClient()
            Dim response As String = Await httpClient.GetStringAsync(requestUrl)
            Dim searchResults = JsonConvert.DeserializeObject(Of TmdbSearchResult)(response)

            If searchResults IsNot Nothing AndAlso searchResults.results.Count > 0 Then
                If MOVIE_FORM.ListViewMovies.InvokeRequired Then
                    MOVIE_FORM.ListViewMovies.Invoke(Sub() MOVIE_FORM.UpdateListView(searchResults))
                Else
                    MOVIE_FORM.UpdateListView(searchResults)
                End If
            End If
        End Using
    End Function
End Class
