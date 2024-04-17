
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


End Class