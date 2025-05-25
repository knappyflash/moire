Public Class MifData
    Private _imageName As String
    Private _test As New Dictionary(Of String, String)
    Private _imageDate As Date
    Public Sub New()
        _test.Add("Hello", "World")
        _test.Add("TEST2", "TEST2")
    End Sub

    Public Property ImageName As String
        Get
            Return _imageName
        End Get
        Set(value As String)
            _imageName = value
        End Set
    End Property

    Public Property Test As Dictionary(Of String, String)
        Get
            Return _test
        End Get
        Set(value As Dictionary(Of String, String))
            _test = value
        End Set
    End Property

    Public Property ImageDate As String
        Get
            Return _imageDate
        End Get
        Set(value As String)
            _imageDate = value
        End Set
    End Property
End Class
