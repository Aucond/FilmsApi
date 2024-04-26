Imports System.Security.Cryptography
Imports System.Text

Public Class CPasswordHash
    Implements IPasswordHash

    Public Function GenerateSalt() As String Implements IPasswordHash.GenerateSalt
        Dim rng As New RNGCryptoServiceProvider()
        Dim saltBytes(15) As Byte
        rng.GetBytes(saltBytes)
        Return Convert.ToBase64String(saltBytes)
    End Function

    Public Function HashPassword(password As String, salt As String) As String Implements IPasswordHash.HashPassword
        Dim saltedPassword As String = password & salt
        Dim sha256 As SHA256 = SHA256.Create()
        Dim bytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword))
        Dim builder As New StringBuilder()

        For i As Integer = 0 To bytes.Length - 1
            builder.Append(bytes(i).ToString("x2"))
        Next

        Return builder.ToString()
    End Function
End Class
