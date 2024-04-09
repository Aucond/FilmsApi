Public Class ListViewVoteCountComparer
    Implements IComparer

    Private ReadOnly ascending As Boolean

    Public Sub New(ascending As Boolean)
        Me.ascending = ascending
    End Sub

    Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
        Dim itemX As ListViewItem = DirectCast(x, ListViewItem)
        Dim itemY As ListViewItem = DirectCast(y, ListViewItem)

        ' Parse vote counts as integers
        Dim voteCountX As Integer = Integer.Parse(itemX.SubItems(5).Text)
        Dim voteCountY As Integer = Integer.Parse(itemY.SubItems(5).Text)

        ' Compare vote counts based on sorting order (ascending or descending)
        If ascending Then
            Return voteCountX.CompareTo(voteCountY)
        Else
            Return voteCountY.CompareTo(voteCountX)
        End If
    End Function
End Class
