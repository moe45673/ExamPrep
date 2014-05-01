'*************************************************************************
'* Filename: VBLib/MessageConverter.vb
'* By:       MF
'* Last Modified Date: 12 Aug 2012
'* Purpose:  MesssageConverter handles all exceptions during deserialization
'*           and keeps the JsonConverter going (otherwise it fails!)
'*
'**************************************************************************
Imports Newtonsoft.Json

Public Class PersonConverter
    Inherits JsonConverter

    Public Overrides Function CanConvert(objectType As System.Type) As Boolean
        Return GetType(Person) Is objectType
    End Function

    Public Overrides Function ReadJson(reader As Newtonsoft.Json.JsonReader,
                                       objectType As System.Type, existingValue As Object,
                                       serializer As Newtonsoft.Json.JsonSerializer) As Object
        Dim target As New Person

        '* attempt to read and create target 
        '* (use try catch to trap exceptions raised by our libary)
        '* to see the exceptions uncomment out the lines below and look
        '* for output on the Immediate Window (Debug->Windows->Immediate)
        Try
            serializer.Populate(reader, target)
        Catch ex As ArgumentException
            'Debug.Print(ex.Message)

        Catch ex As Exception
            'Debug.Print(ex.Message)
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