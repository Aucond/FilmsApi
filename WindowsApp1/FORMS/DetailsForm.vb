Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports System.Net.Http
Public Class DetailsForm
    Private movie As TmdbMovie
    Private posterPath As String

    Public Sub New(parentForm As Form, movie As TmdbMovie)
        InitializeComponent()
        Me.movie = movie
        Me.posterPath = "https://image.tmdb.org/t/p/w500" & movie.poster_path
        DisplayMovieDetails()
    End Sub

    Private Sub DisplayMovieDetails()
        ' Display details of movie in the form controls
        ' For example:
        LabelTitle.Text = movie.title
        LabelOverview.Text = movie.overview
        ' Add more fields as per your UI design
        If Not String.IsNullOrEmpty(movie.release_date) AndAlso
    DateTime.TryParse(movie.release_date, Nothing) Then

            Dim releaseDate As DateTime = DateTime.Parse(movie.release_date)
            LabelReleaseDate.Text = releaseDate.ToString("MMMM dd, yyyy")
        Else
            LabelReleaseDate.Text = "N/A"
        End If

        ' Set the vote average and original language
        LabelVoteAverage.Text = movie.vote_average.ToString("0.0") ' Formatting to one decimal place
        LabelOriginalLanguage.Text = movie.original_language.ToUpper() ' Assuming you want the language code in uppercase
    End Sub

    Private Async Sub DetailsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Adjust label to fit text properly
        LabelOverview.AutoSize = False
        LabelOverview.Width = 500
        LabelOverview.Height = 300
        LabelOverview.TextAlign = ContentAlignment.TopLeft
        LabelOverview.BorderStyle = BorderStyle.FixedSingle
        LabelOverview.AutoEllipsis = True

        ' Ensure image fits in the PictureBox
        PictureBoxMovie.SizeMode = PictureBoxSizeMode.Zoom
        DisplayMovieDetails()
        Try
            Using client As New WebClient()
                Dim imageData As Byte() = Await client.DownloadDataTaskAsync(Me.posterPath)
                Using ms As New MemoryStream(imageData)
                    PictureBoxMovie.Image = Image.FromStream(ms)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while loading the image.")
        End Try
    End Sub

    Private Sub VoteAverage_Click(sender As Object, e As EventArgs) Handles LabelVoteAverage.Click

    End Sub
End Class