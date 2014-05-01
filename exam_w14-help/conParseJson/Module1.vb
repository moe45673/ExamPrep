Imports System.IO
Imports VBLib
Imports Newtonsoft.Json

Module Module1
    Sub Main()

        Dim jfDSV = "..\..\..\allData\dps916_vba544-w14.dsv"
        Dim jf = "..\..\..\allData\indented_dpsvba-w14.json"

        Dim people = New List(Of Human)

        Console.WriteLine("..reading dsv...")

        Using sr = New StreamReader(jfDSV)

            ' throw away the first line as its the header
            sr.ReadLine()

            Do While sr.Peek <> -1

                Dim values = sr.ReadLine.Split(";")
                Dim person As New Human

                person.Name = values(0)
                person.Email = values(1)

                If values(2) IsNot Nothing Then
                    Dim cos = values(2).Split(",")
                    If cos IsNot Nothing Then
                        For Each it In cos
                            person.Companies.Add(New Company(it.Trim))
                        Next
                    End If
                End If

                If values(3) IsNot Nothing Then
                    Dim spls = values(3).Split(",")
                    If spls IsNot Nothing Then
                        For Each it In spls
                            person.Specialization.Add(New Specialization(it.Trim))
                        Next
                    End If
                End If

                people.Add(person)

            Loop
        End Using

        Console.WriteLine("..saving to JSON...")

        Using sw = New StreamWriter(jf)
            sw.WriteLine(JsonConvert.SerializeObject(people, Formatting.Indented))
        End Using

        Console.WriteLine("..finished saving to JSON...")

        ObjectDumper(DeserializeJSON(jf))

    End Sub

    Public Sub ObjectDumper(objects As List(Of Human))

        If objects IsNot Nothing Then
            For Each item In objects
                Console.WriteLine("Name: " + item.Name)
                Console.WriteLine("Email: " + item.Email)
                Console.WriteLine("Companies: {0}", String.Join(",", item.Companies.Select(Function(c) c.Name)))
                Console.WriteLine("Specialization: " + String.Join(",", item.Specialization.Select(Function(s) s.Type)))
                Console.WriteLine()
            Next
        Else : Console.WriteLine("No objects given")
        End If

    End Sub

    '* deserialize given JSON file back to list of message objects
    Function DeserializeJSON(jsonFilename As String) As List(Of Human)

        Dim jsonString As String

        Using sr As New IO.StreamReader(jsonFilename)
            jsonString = sr.ReadToEnd()
        End Using

        Dim js = New JsonSerializerSettings

        '* ignore all exceptions when JSON is deserializing and move onto the next record
        js.Error = Sub(sender, args) args.ErrorContext.Handled = True
        js.Converters.Add(New HumanConverter)   '* handles message exceptions

        Try
            Return JsonConvert.DeserializeObject(Of List(Of Human))(jsonString, js)
        Catch
            Return Nothing
        End Try

    End Function

End Module
