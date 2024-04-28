Imports Npgsql

Public Class CDatabase
    Implements IDatabase

    Private connection As New NpgsqlConnection("Host=snuffleupagus.db.elephantsql.com;Port=5432;Username=mmrfecqh;Password=IZCtzNu-HIZIVnOtTfX-R4F5eO_oQNW7;Database=mmrfecqh;")

    ReadOnly Property getConnection() As NpgsqlConnection
        Get
            Return connection
        End Get
    End Property
    Public Sub openConnection() Implements IDatabase.openConnection
        If connection.State = ConnectionState.Closed Then
            connection.Open()
        End If
    End Sub

    Public Sub closeConnection() Implements IDatabase.closeConnection
        If connection.State = ConnectionState.Open Then
            connection.Close()
        End If
    End Sub

    Public Function getData(query As String, params() As NpgsqlParameter) As DataTable Implements IDatabase.getData
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

    Public Function setData(query As String, params() As NpgsqlParameter) As Integer Implements IDatabase.setData
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
