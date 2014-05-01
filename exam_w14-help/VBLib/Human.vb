Imports Newtonsoft.Json

Public Class Company

    Private _cname As String

    Sub New(values As String)
        Name = values
    End Sub

    Public Property Name As String

End Class

Public Class Specialization

    Private _type As String

    Sub New(values As String)
        Type = values
    End Sub
    Public Property Type As String
        Get
            Return _type
        End Get
        Set(value As String)
            _type = value
        End Set
    End Property
End Class
Public Class Human

    <JsonProperty("Full Name")>
    Public Property Name As String
    Public Property Email As String
    Public Property Companies As List(Of Company)
    Public Property Specialization As List(Of Specialization)

    Public Sub New()
        Companies = New List(Of Company)
        Specialization = New List(Of Specialization)
    End Sub

End Class
