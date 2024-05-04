Public Interface IComments
    Function SaveCommentToDatabase(userId As Integer?, movieId As Integer, commentText As String) As String
    Function GetReactionCount(commentID As Integer, reactionType As Char) As Integer
    Function IsMovieViewed(movieId As Integer, userId As Integer) As Boolean
    Function GetCurrentReaction(commentID As Integer, UserID As Integer) As Char?
End Interface
