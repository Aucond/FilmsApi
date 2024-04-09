Public Class ListViewReleaseDateComparer
    Implements IComparer

    Private ascending As Boolean

    Public Sub New(asc As Boolean)
        ascending = asc
    End Sub

    Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
        Dim itemX As ListViewItem = DirectCast(x, ListViewItem)
        Dim itemY As ListViewItem = DirectCast(y, ListViewItem)

        ' Parse release years from subitem text
        Dim releaseYearX As Integer = If(Integer.TryParse(itemX.SubItems(1).Text, releaseYearX), releaseYearX, 0)
        Dim releaseYearY As Integer = If(Integer.TryParse(itemY.SubItems(1).Text, releaseYearY), releaseYearY, 0)

        ' Compare release years based on sorting order
        If ascending Then
            Return releaseYearX.CompareTo(releaseYearY)
        Else
            Return releaseYearY.CompareTo(releaseYearX)
        End If
    End Function
End Class
