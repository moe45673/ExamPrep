Public Class Specialization

    Public Property Name() As String

    Public Overrides Function GetHashCode() As Integer
        Dim hash As Int32 = 1

        hash = hash * 2 + Name.GetHashCode()
        Return hash
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        If obj Is Nothing OrElse Not Me.GetType Is obj.GetType() Then
            Return False
        End If
        Dim i As Specialization = CType(obj, Specialization)
        Return Me.Name = i.Name
    End Function

    Sub New()
        Name = ""
    End Sub

    Sub New(n As String)
        Name = n
    End Sub

End Class
