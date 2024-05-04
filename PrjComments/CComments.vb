Imports Microsoft.VisualBasic.ApplicationServices
Imports Npgsql
Imports PrjDatabase

Public Class CComments
    Implements IComments

    Public Function SaveCommentToDatabase(userId As Integer?, movieId As Integer, commentText As String) As String Implements IComments.SaveCommentToDatabase
        Dim mydb As New CDatabase()
        Dim username As String

        ' Check if we have a userId or if this is a guest
        If userId.HasValue AndAlso userId.Value > 0 Then
            ' If it's a registered user, retrieve the username
            Try
                mydb.openConnection()
                Dim usernameCommand As New NpgsqlCommand("SELECT username FROM users_info WHERE id = @UserID;", mydb.getConnection())
                usernameCommand.Parameters.AddWithValue("@UserID", userId.Value)

                Dim result As Object = usernameCommand.ExecuteScalar()
                mydb.closeConnection()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    username = result.ToString()
                Else
                    ' If no username is found, default to Guest
                    username = "Guest"
                End If
            Catch ex As Exception

                mydb.closeConnection()

                Return String.Empty
            End Try
        Else

            username = "Guest"
        End If

        Try
            mydb.openConnection()
            Dim command As New NpgsqlCommand("INSERT INTO Comments (Username, MovieID, CommentText) VALUES (@Username, @MovieID, @CommentText);", mydb.getConnection())
            command.Parameters.AddWithValue("@Username", username)
            command.Parameters.AddWithValue("@MovieID", movieId)
            command.Parameters.AddWithValue("@CommentText", commentText)

            If command.ExecuteNonQuery() = 1 Then
                mydb.closeConnection()

                Return username
            Else
                mydb.closeConnection()
                Return String.Empty
            End If
        Catch ex As Exception
            Return String.Empty
        Finally
            If mydb.getConnection().State = ConnectionState.Open Then
                mydb.closeConnection()
            End If
        End Try
    End Function

    Public Function GetReactionCount(commentID As Integer, reactionType As Char) As Integer Implements IComments.GetReactionCount
        Dim mydb As New CDatabase()
        Dim count As Integer = 0
        Try
            mydb.openConnection()
            Dim command As New NpgsqlCommand("SELECT COUNT(*) FROM CommentReactions WHERE CommentID = @CommentID AND ReactionType = @ReactionType;", mydb.getConnection())
            command.Parameters.AddWithValue("@CommentID", commentID)
            command.Parameters.AddWithValue("@ReactionType", reactionType)
            count = Convert.ToInt32(command.ExecuteScalar())
        Catch ex As Exception
        Finally
            mydb.closeConnection()
        End Try
        Return count
    End Function

    Public Function IsMovieViewed(movieId As Integer, userId As Integer) As Boolean Implements IComments.IsMovieViewed
        Dim mydb As New CDatabase()
        Try
            Dim sql As String = "SELECT COUNT(1) FROM MovieViewedStatus WHERE MovieID = @MovieID AND UserID = @UserID AND Viewed = TRUE;"
            Dim command As New NpgsqlCommand(sql, mydb.getConnection())
            command.Parameters.AddWithValue("@MovieID", movieId)
            command.Parameters.AddWithValue("@UserID", userId)

            mydb.openConnection()
            Dim result As Integer = Convert.ToInt32(command.ExecuteScalar())
            Return result > 0
        Catch ex As Exception
            Return False
        Finally
            mydb.closeConnection()
        End Try
    End Function
    Public Function GetCurrentReaction(commentID As Integer, UserID As Integer) As Char? Implements IComments.GetCurrentReaction
        Dim mydb As New CDatabase()
        Try
            mydb.openConnection()
            Dim command As New NpgsqlCommand("SELECT ReactionType FROM CommentReactions WHERE UserID = @UserID AND CommentID = @CommentID;", mydb.getConnection())
            command.Parameters.AddWithValue("@UserID", UserID)
            command.Parameters.AddWithValue("@CommentID", commentID)

            Dim result As Object = command.ExecuteScalar()
            If result IsNot Nothing Then
                Return Convert.ToChar(result)
            End If
        Catch ex As Exception
        Finally
            mydb.closeConnection()
        End Try
        Return Nothing
    End Function
End Class
