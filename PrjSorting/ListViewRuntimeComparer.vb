Public Class ListViewRuntimeComparer
    Implements IComparer

    Private ascending As Boolean

    Public Sub New(asc As Boolean)
        ascending = asc
    End Sub

    Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
        Dim itemX As ListViewItem = DirectCast(x, ListViewItem)
        Dim itemY As ListViewItem = DirectCast(y, ListViewItem)

        ' Parse runtime from subitem text
        Dim runtimeX As Integer = If(itemX.SubItems(4).Text <> "N/A", Integer.Parse(itemX.SubItems(4).Text.Split(" "c)(0)), 0)
        Dim runtimeY As Integer = If(itemY.SubItems(4).Text <> "N/A", Integer.Parse(itemY.SubItems(4).Text.Split(" "c)(0)), 0)

        ' Compare runtimes based on sorting order
        If ascending Then
            Return runtimeX.CompareTo(runtimeY)
        Else
            Return runtimeY.CompareTo(runtimeX)
        End If
    End Function
End Class
