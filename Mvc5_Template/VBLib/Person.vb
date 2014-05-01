Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports System.IO
Public Class Person

    Const COMPANY1 As String = "Microsoft"
    Const COMPANY2 As String = "Apple"
    
    Dim _name As String
    Dim _email As String
    Dim _comps As List(Of Company)
    Dim valid_ As Boolean



    Public Property Name As String
        Set(value As String)
            If value.Equals("") Then
                valid_ = False
                Throw New ArgumentNullException("Name cannot be null!")
            ElseIf Regex.IsMatch(value, "^[a-zA-Z]+[a-zA-Z ]*$") <> True Then
                valid_ = False
                Throw New ArgumentNullException("Name cannot be null!")
            Else
                _name = value
            End If

        End Set
        Get
            Return _name
        End Get
    End Property

    Public Property Email As String
        Set(value As String)
            If value.Equals("") Then
                valid_ = False
                Throw New ArgumentNullException("Email cannot be null!")
            ElseIf Regex.IsMatch(value, "^[a-zA-Z][a-zA-Z0-9]+@[a-zA-Z]+.[a-zA-Z]+$") <> True Then
                valid_ = False
                Throw New ArgumentException("Data must be of format 'xxx@xxx.xxx'")
            Else
                _email = value
            End If

        End Set
        Get
            Return _email
        End Get
    End Property


    Public Property Specialization As List(Of Specialization)


    Public Property Companies As List(Of Company)
        Get
            Return _comps
        End Get
        Set(value As List(Of Company))
            If (value.Count <= 0) Then
                valid_ = False
                Throw New ArgumentException("Please Enter At Least One Company!")
            ElseIf Not value.Exists(Function(x) x.Name.Equals(COMPANY1)) And Not value.Exists(Function(x) x.Name.Equals(COMPANY2)) Then
                valid_ = False
                Throw New ArgumentException("Candidate must have worked at Apple and/or Microsoft!")
            Else
                If (_comps.Count <= 0) Then
                    _comps.Clear()
                End If
                _comps.AddRange(value)

            End If

        End Set
    End Property

    ReadOnly Property IsValid() As Boolean
        Get
            Return valid_
        End Get
    End Property

    Public Sub New()
        valid_ = True
        Companies = New List(Of Company)
        Specialization = New List(Of Specialization)


    End Sub

    Sub New(n As String, e As String, c As List(Of Company), s As List(Of Specialization))
        valid_ = True
        Companies = New List(Of Company)
        Specialization = New List(Of Specialization)

        Companies = c
        Specialization = s
        Name = n
        Email = e
    End Sub

End Class

Public Module PersonHelpers

    '* deserialize given JSON file back to list of person objects
    Function DeserializeJSON(jsonFilename As String) As List(Of Person)

        Dim jsonString As String

        Using sr As New IO.StreamReader(jsonFilename)
            jsonString = sr.ReadToEnd()
        End Using

        Dim js = New JsonSerializerSettings

        '* ignore all exceptions when JSON is deserializing and move onto the next record
        js.Error = Sub(sender, args) args.ErrorContext.Handled = True
        js.Converters.Add(New PersonConverter)   '* handles person exceptions

        Try
            Return JsonConvert.DeserializeObject(Of List(Of Person))(jsonString, js)
        Catch
            Return Nothing
        End Try

    End Function

End Module
