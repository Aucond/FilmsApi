Public Class WatchListForm
    Inherits Form

    ' Assuming you have a DataGridView called dgvWatchList
    Private Sub WatchListForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadWatchList()
    End Sub

    Private Sub LoadWatchList()
        ' Assuming you have an instance of the DB class that is properly set up to get the current user's ID
        Dim database As New DB()
        Dim userId As Integer? = database.GetCurrentUserId()

        If userId.HasValue Then
            ' Query the database for the watch list items for the user
            Dim watchListDataTable As DataTable = database.getData($"SELECT * FROM watchlist WHERE user_id = {userId}", Nothing)

            ' Clear existing controls in the FlowLayoutPanel
            FlowLayoutPanelWatchList.Controls.Clear()

            ' Loop through each movie and create a panel with a poster and title
            For Each row As DataRow In watchListDataTable.Rows
                Dim poster As New PictureBox()
                poster.SizeMode = PictureBoxSizeMode.Zoom
                poster.Width = 100  ' Set the width to your preference
                poster.Height = 150 ' Set the height to your preference
                poster.ImageLocation = row("poster_path").ToString()  ' Set the URL to the ImageLocation property
                poster.LoadAsync()  ' Initiates the asynchronous image load

                ' Create a new Label for the title
                Dim title As New Label()
                title.Text = row("title").ToString()
                title.AutoSize = True
                title.MaximumSize = New Size(poster.Width, 0) ' To ensure text wraps within the poster width
                title.TextAlign = ContentAlignment.TopCenter

                ' Create a panel to hold the poster and title
                Dim moviePanel As New Panel()
                moviePanel.Width = poster.Width
                moviePanel.Height = poster.Height + title.Height + 10 ' Add some padding

                ' Add the poster and title to the panel
                moviePanel.Controls.Add(poster)
                moviePanel.Controls.Add(title)

                ' Position the title below the poster
                poster.Location = New Point(0, 0)
                title.Location = New Point(0, poster.Height)

                ' Add the movie panel to the FlowLayoutPanel
                FlowLayoutPanelWatchList.Controls.Add(moviePanel)
            Next
        Else
            MessageBox.Show("You must be logged in to view your watch list.")
        End If
    End Sub



    Private Sub LoadRecommendations()
        ' Retrieve the current user's ID
        Dim database As New DB()
        Dim userId As Integer? = database.GetCurrentUserId()

        If userId.HasValue Then
            ' Get the top genres from the user's watchlist
            Dim genresDataTable As DataTable = database.getData($"SELECT genre_id FROM watchlist_genres WHERE user_id = {userId} GROUP BY genre_id ORDER BY COUNT(*) DESC LIMIT 5", Nothing)

            ' Assuming you've got a method to convert DataTable to List(Of Integer)
            Dim topGenres As List(Of Integer) = ConvertDataTableToGenreList(genresDataTable)

            ' Now use the topGenres to get movie recommendations
            Dim recommendationsDataTable As DataTable = database.getData($"SELECT * FROM movies WHERE genre_id IN ({String.Join(",", topGenres)}) LIMIT 10", Nothing)

            ' Set the DataSource of the recommendations DataGridView or ListView to the DataTable
            dgvRecommendations.DataSource = recommendationsDataTable 'At the bottom of WatchListForm, you could have a separate control like ListView or another DataGridView -  dgvRecommendations for movie recommendations.
        Else
            MessageBox.Show("You must be logged in to view recommendations.")
        End If
    End Sub
End Class

