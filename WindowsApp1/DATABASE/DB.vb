Imports MySql.Data.MySqlClient
Imports Npgsql


Public Class DB

    Private connection As New NpgsqlConnection("Host=snuffleupagus.db.elephantsql.com;Port=5432;Username=mmrfecqh;Password=IZCtzNu-HIZIVnOtTfX-R4F5eO_oQNW7;Database=mmrfecqh;")


    ' create a unction to return the connection
    ReadOnly Property getConnection() As NpgsqlConnection
        Get
            Return connection
        End Get
    End Property


    ' open the connection 
    Sub openConnection()

        If connection.State = ConnectionState.Closed Then
            connection.Open()
        End If

    End Sub

    ' close the connection 
    Sub closeConnection()

        If connection.State = ConnectionState.Open Then
            connection.Close()
        End If

    End Sub

    ' create a Function to get data
    Public Function getData(ByVal query As String, ByVal params() As NpgsqlParameter) As DataTable

        Dim command As New NpgsqlCommand(query, connection)

        If params IsNot Nothing Then

            command.Parameters.AddRange(params)

        End If

        Dim table As New DataTable
        Dim adapter As New NpgsqlDataAdapter
        adapter.SelectCommand = command
        adapter.Fill(table)

        Return table

    End Function


    ' create a Function to set data and execute a query
    Public Function setData(ByVal query As String, ByVal params As NpgsqlParameter()) As Integer

        Dim command As New NpgsqlCommand(query, connection)

        If params IsNot Nothing Then

            command.Parameters.AddRange(params)

        End If

        openConnection()

        Dim commandState As Integer = command.ExecuteNonQuery()

        closeConnection()

        Return commandState

    End Function

    Public Function AddMovieToWatchList(userId As Integer, movie As TmdbMovie) As Boolean
        Dim query As String = "INSERT INTO watchlist (user_id, movie_id, title, poster_path) VALUES (@userId, @movieId, @title, @posterPath;"

        ' Create the parameters
        Dim params() As NpgsqlParameter = {
            New NpgsqlParameter("@userId", userId),
            New NpgsqlParameter("@movieId", movie.id),
            New NpgsqlParameter("@title", movie.title),
            New NpgsqlParameter("@posterPath", movie.poster_path)
        }

        ' Call the setData function to execute the query
        Try
            openConnection()
            Dim result As Integer = setData(query, params)
            closeConnection()
            Return result > 0
        Catch ex As Exception
            ' Handle any errors
            MessageBox.Show("An error occurred while adding to watch list: " & ex.Message)
            Return False
        End Try
    End Function
    ' A hypothetical property to hold the current user's ID.
    ' This should be set when the user logs in to your application.
    Public Property CurrentUserId As Integer?

    ' A method to get the current user's ID
    Public Function GetCurrentUserId() As Integer?
        Return CurrentUserId
    End Function
End Class