Imports Newtonsoft.Json

Public Class HumanConverter
    Inherits JsonConverter

    Public Overrides Function CanConvert(objectType As System.Type) As Boolean
        Return GetType(Human) Is objectType
    End Function

    Public Overrides Function ReadJson(reader As Newtonsoft.Json.JsonReader,
                                       objectType As System.Type, existingValue As Object,
                                       serializer As Newtonsoft.Json.JsonSerializer) As Object
        Dim target As New Human

        '* attempt to read and create target 
        '* (use try catch to trap exceptions raised by our libary)
        '* to see the exceptions uncomment out the lines below and look
        '* for output on the Immediate Window (Debug->Windows->Immediate)
        Try
            serializer.Populate(reader, target)
        Catch ex As ArgumentException
            Debug.Print(ex.Message)
        Catch ex As Exception
            Debug.Print(ex.Message)
        Finally
            ' Unwinding here (nothing for now)
        End Try
        Return target

    End Function

    Public Overrides Sub WriteJson(writer As Newtonsoft.Json.JsonWriter,
                                   value As Object, serializer As Newtonsoft.Json.JsonSerializer)
        Throw New NotImplementedException
    End Sub
End Class
