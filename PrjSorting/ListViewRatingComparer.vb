Public Class ListViewRatingComparer
    Implements IComparer

    Private ascending As Boolean

    Public Sub New(asc As Boolean)
        ascending = asc
    End Sub

    Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
        Dim itemX As ListViewItem = DirectCast(x, ListViewItem)
        Dim itemY As ListViewItem = DirectCast(y, ListViewItem)

        ' Parse ratings from subitem text
        Dim ratingX As Double = Double.Parse(itemX.SubItems(3).Text)
        Dim ratingY As Double = Double.Parse(itemY.SubItems(3).Text)

        ' Compare ratings based on sorting order
        If ascending Then
            Return ratingX.CompareTo(ratingY)
        Else
            Return ratingY.CompareTo(ratingX)
        End If
    End Function
End Class
