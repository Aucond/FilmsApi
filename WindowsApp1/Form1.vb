Imports System.Net
Imports System.IO
Imports System.Drawing
Imports Newtonsoft.Json
Imports System.Windows.Forms
Imports System.Net.Http
Public Class Form1

    Private WithEvents searchTimer As New System.Windows.Forms.Timer()

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
        Dim apiKey As String = "0d77f86880fc2d980da7ba1ab371bdbb" ' Ваш реальный API-ключ от TMDb
        Dim requestUrl As String = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&query={Uri.EscapeDataString(searchQuery)}"

        Using httpClient As New HttpClient()
            Dim response As String = Await httpClient.GetStringAsync(requestUrl)
            Dim searchResults = JsonConvert.DeserializeObject(Of TmdbSearchResult)(response)

            ' Теперь у вас есть searchResults с информацией о фильмах
            If searchResults IsNot Nothing AndAlso searchResults.results.Count > 0 Then
                ' Обновление элементов управления UI в потоке UI
                If ListViewMovies.InvokeRequired Then
                    ListViewMovies.Invoke(Sub() UpdateListView(searchResults))
                Else
                    UpdateListView(searchResults)
                End If
            End If
        End Using
    End Function

    ' Метод для обновления ListView, вызываемый из SearchMoviesAsync
    Private Sub UpdateListView(searchResults As TmdbSearchResult)
        ' Создание ImageList и настройка ListView
        Dim posters As New ImageList()
        posters.ImageSize = New Size(100, 140) ' Примерный размер постеров
        ' Создание словаря для жанров
        Dim genreDictionary As New Dictionary(Of Integer, String)() ' Предполагаем, что у вас есть словарь ID жанров и их названий
        ' Настройка столбцов ListView
        ListViewMovies.Clear()
        ListViewMovies.View = View.Details
        ListViewMovies.Columns.Add("Название", 300)
        ListViewMovies.Columns.Add("Год", 100)
        ListViewMovies.Columns.Add("Жанр", 100)
        ListViewMovies.Columns.Add("Рейтинг", 50)
        ListViewMovies.SmallImageList = posters

        ' Заполнение ListView элементами
        For Each movie In searchResults.results
            Dim item As New ListViewItem(movie.title)
            Dim year As String = If(Not String.IsNullOrEmpty(movie.release_date) AndAlso movie.release_date.Length >= 4, movie.release_date.Substring(0, 4), "N/A")
            Dim genreNames As New List(Of String)()

            ' Получение имен жанров по ID
            For Each genreId In movie.genre_ids
                If genreDictionary.ContainsKey(genreId) Then
                    genreNames.Add(genreDictionary(genreId))
                End If
            Next

            Dim genre As String = String.Join(", ", genreNames) ' Соединяем имена жанров через запятую
            Dim rating As String = movie.vote_average.ToString("0.0") ' Форматирование рейтинга до одного десятичного знака

            item.SubItems.Add(year)
            item.SubItems.Add(genre)
            item.SubItems.Add(rating)

            ' Загрузка изображения постера
            Dim posterUrl As String = $"https://image.tmdb.org/t/p/w500{movie.poster_path}"
            Dim poster As Image = LoadImageFromUrl(posterUrl) ' Убедитесь, что LoadImageFromUrl поддерживает синхронный запрос
            If poster IsNot Nothing Then
                posters.Images.Add(movie.id.ToString(), poster)
                item.ImageKey = movie.id.ToString()
            End If

            ListViewMovies.Items.Add(item)
        Next

        ' Автоматическое изменение ширины столбцов для удобства просмотра
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