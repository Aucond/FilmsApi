Imports Npgsql
Public Interface IDatabase
    Sub openConnection()
    Sub closeConnection()
    Function getData(ByVal query As String, ByVal params() As NpgsqlParameter) As DataTable
    Function setData(ByVal query As String, ByVal params As NpgsqlParameter()) As Integer

End Interface
