
Imports MySql.Data.MySqlClient


Public Class DB

    Private connection As New MySqlConnection("datasource=localhost;port=3306;username=root;password=admin;database=users")


    ' create a unction to return the connection
    ReadOnly Property getConnection() As MySqlConnection
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
    Public Function getData(ByVal query As String, ByVal params() As MySqlParameter) As DataTable

        Dim command As New MySqlCommand(query, connection)

        If params IsNot Nothing Then

            command.Parameters.AddRange(params)

        End If

        Dim table As New DataTable
        Dim adapter As New MySqlDataAdapter
        adapter.SelectCommand = command
        adapter.Fill(table)

        Return table

    End Function


    ' create a Function to set data and execute a query
    Public Function setData(ByVal query As String, ByVal params As MySqlParameter()) As Integer

        Dim command As New MySqlCommand(query, connection)

        If params IsNot Nothing Then

            command.Parameters.AddRange(params)

        End If

        openConnection()

        Dim commandState As Integer = command.ExecuteNonQuery()

        closeConnection()

        Return commandState

    End Function


End Class