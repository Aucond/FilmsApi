Imports PrjDatabase
Imports Npgsql
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

End Class
