Public Class Company

    Dim _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(value As String)
            If (value = "") Then
                Throw New ArgumentException("Company must have a name!")
            Else
                _name = value
                'RaiseEvent Text_Changed(msg_)
            End If
        End Set
    End Property

    Public Overrides Function GetHashCode() As Integer
        Dim hash As Int32 = 1

        hash = hash * 2 + Name.GetHashCode()
        Return hash
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        If obj Is Nothing OrElse Not Me.GetType Is obj.GetType() Then
            Return False
        End If
        Dim i As Company = CType(obj, Company)
        Return Me.Name = i.Name
    End Function

    Sub New()

    End Sub

    Sub New(n As String)
        Name = n
    End Sub

End Class
