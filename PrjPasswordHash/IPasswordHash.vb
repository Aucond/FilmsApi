Public Interface IPasswordHash
    Function GenerateSalt() As String
    Function HashPassword(password As String, salt As String) As String
End Interface
