Public Class CExporter
    Implements IExporter

    Public Property delimiter As String Implements IExporter.delimiter
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property textQualifier As String Implements IExporter.textQualifier
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Function saveDataToCsv(data As Array, Optional appendData As Boolean = False) As Integer Implements IExporter.saveDataToCsv
        Throw New NotImplementedException()
    End Function

    Public Function setFileToSave() As String Implements IExporter.setFileToSave
        Throw New NotImplementedException()
    End Function
End Class
