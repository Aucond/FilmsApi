Imports System.Net
Imports System.IO
Imports System.Drawing
Imports Newtonsoft.Json
Imports System.Windows.Forms
Imports System.Net.Http
Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Public Class Form1

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

    Public Sub New()
        ' Этот вызов необходим конструктору Windows Forms.
        InitializeComponent()

        ' Инициализация searchTimer
        searchTimer.Interval = 500 ' Задержка в миллисекундах
    End Sub

    ' Обработчик события тика таймера.
    Private Async Sub searchTimer_Tick(sender As Object, e As EventArgs) Handles searchTimer.Tick
        ' Останавливаем таймер
        searchTimer.Stop()

        ' Получаем поисковый запрос из TextBox
        Dim searchQuery As String = TextBox1.Text
        If Not String.IsNullOrWhiteSpace(searchQuery) Then
            ' Вызываем функцию поиска фильмов асинхронно
            Await SearchMoviesAsync(searchQuery)
        Else
            ' Если строка поиска пуста, очищаем ListView
            ListViewMovies.Items.Clear()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ' Перезапуск таймера при каждом изменении текста
        searchTimer.Stop()
        searchTimer.Start()
    End Sub

    ' Асинхронная функция для поиска фильмов.
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

                ' Log column names or properties
                Dim type As Type = GetType(TmdbSearchResult)
                Dim properties As PropertyInfo() = type.GetProperties()

                For Each prop As PropertyInfo In properties
                    Console.WriteLine(prop.Name)
                Next
            End If
        End Using
    End Function

    ' Метод для обновления ListView, вызываемый из SearchMoviesAsync
    Private Sub UpdateListView(searchResults As TmdbSearchResult)
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
        ListViewMovies.SmallImageList = posters

        ' Populate ListView with items
        For Each movie In searchResults.results
            Dim item As New ListViewItem(movie.title)
            Dim year As String = If(Not String.IsNullOrEmpty(movie.release_date) AndAlso movie.release_date.Length >= 4, movie.release_date.Substring(0, 4), "N/A")
            Dim genreNames As New List(Of String)()

            ' Retrieve genre names based on genre IDs
            For Each genreId In movie.genre_ids
                If genreDictionary.ContainsKey(genreId) Then
                    genreNames.Add(genreDictionary(genreId))
                End If
            Next

            Dim genres As String = String.Join(", ", genreNames) ' Join genre names with a comma
            Dim rating As String = movie.vote_average.ToString("0.0") ' Format rating to one decimal place

            item.SubItems.Add(year)
            item.SubItems.Add(genres)
            item.SubItems.Add(rating)

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
            ' Обработка ошибок загрузки изображения, например, можно вернуть Nothing или изображение-замещение
            Return Nothing
        End Try
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each kvp As KeyValuePair(Of Integer, String) In genreDictionary
            ComboBox3.Items.Add(kvp.Value)
        Next
    End Sub
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

                ' Log column names or properties
                Dim type As Type = GetType(TmdbSearchResult)
                Dim properties As PropertyInfo() = type.GetProperties()

                For Each prop As PropertyInfo In properties
                    Console.WriteLine(prop.Name)
                Next
            End If
        End Using
    End Function
End Class

' Классы для десериализации ответа API должны остаться без изменений.
Public Class TmdbSearchResult
    Public Property results As List(Of TmdbMovie)
End Class

Public Class TmdbMovie
    Public Property title As String
    Public Property release_date As String
    Public Property genre_ids As List(Of Integer)
    Public Property vote_average As Double
    Public Property poster_path As String
    Public Property id As Integer
End Class