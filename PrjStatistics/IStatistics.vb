Public Interface IStatistics
    Function CalculateTotalViewTime(userId As Integer) As Integer
    Function SaveViewedStatusAsync(movieId As Integer, userId As Integer?, viewed As Boolean) As Task(Of Boolean)
End Interface
