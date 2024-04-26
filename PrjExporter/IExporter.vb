Public Interface IExporter
    Property delimiter As String
    Property textQualifier As String
    Function saveDataToCsv(data As Array, Optional appendData As Boolean = False) As Integer
    Function setFileToSave() As String

End Interface
