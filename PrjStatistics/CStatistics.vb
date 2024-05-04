Imports PrjDatabase
Imports Npgsql
Imports System.Net.Http
Imports Newtonsoft.Json

Public Class CStatistics
    Implements IStatistics

    Public Function CalculateTotalViewTime(userId As Integer) As Integer Implements IStatistics.CalculateTotalViewTime
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

        Finally
            mydb.closeConnection()
        End Try

        Return totalViewTime
    End Function

    Private Async Function SaveViewedStatusAsync(movieId As Integer, userId As Integer?, viewed As Boolean) As Task(Of Boolean) Implements IStatistics.SaveViewedStatusAsync
        Dim mydb As New CDatabase()
        Try

            Dim movieRuntime As Integer

            Dim apiKey As String = "0d77f86880fc2d980da7ba1ab371bdbb"
            Dim requestUrl As String = $"https://api.themoviedb.org/3/movie/{movieId}?api_key={apiKey}"

            Using httpClient As New HttpClient()
                Dim response As String = Await httpClient.GetStringAsync(requestUrl)
                Dim movieDetails = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)

                If movieDetails IsNot Nothing AndAlso movieDetails.ContainsKey("runtime") Then
                    movieRuntime = Convert.ToInt32(movieDetails("runtime"))
                End If
            End Using

            mydb.openConnection()

            Dim command As New NpgsqlCommand()
            command.Connection = mydb.getConnection()

            If viewed Then

                command.CommandText = "INSERT INTO MovieViewedStatus (MovieID, UserID, Viewed, Duration, ViewDate) " &
                                  "VALUES (@MovieID, @UserID, @Viewed, @Duration, CURRENT_TIMESTAMP) " &
                                  "ON CONFLICT (MovieID, UserID) DO UPDATE SET Viewed = EXCLUDED.Viewed, Duration = EXCLUDED.Duration, ViewDate = CURRENT_TIMESTAMP;"
                command.Parameters.AddWithValue("@Duration", movieRuntime)
            Else

                command.CommandText = "UPDATE MovieViewedStatus SET Viewed = @Viewed, Duration = NULL WHERE MovieID = @MovieID AND UserID = COALESCE(@UserID, UserID);"
            End If

            command.Parameters.AddWithValue("@MovieID", movieId)
            command.Parameters.AddWithValue("@UserID", If(userId.HasValue AndAlso userId > 0, userId.Value, DBNull.Value))
            command.Parameters.AddWithValue("@Viewed", viewed)

            Dim result = command.ExecuteNonQuery()
            Return result > 0
        Catch ex As Exception

            Return False
        Finally
            mydb.closeConnection()
        End Try
    End Function

End Class
