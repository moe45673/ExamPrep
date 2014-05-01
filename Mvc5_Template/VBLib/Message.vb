'*************************************************************************
'* Filename: VBLib/Message.vb
'* By:       MF
'* Last Modified Date: 22 Mar 2014
'* Purpose:  Messsage class in DLL with simple error checking
'*
'**************************************************************************
Imports System.IO
Imports Newtonsoft.Json

Public Enum MessageStatus
    DRAFT
    DELETED
    PUBLISHED
End Enum

Public Class Message

    Public Property Text() As String
        Get
            Return msg_
        End Get
        Set(value As String)
            If (value <> "" And value <> "INVALID DATA") Then
                msg_ = value
                RaiseEvent Text_Changed(msg_)
            Else
                valid_ = False
                Throw New ArgumentException("DPS916 and VBA544: Invalid data passed")
            End If
        End Set
    End Property

    '* backing store properties (no validation done for these)
    Public Property Translations As List(Of VBLib.Translation)

    '* Status: Published, Deleted, Draft
    Public Property Status As MessageStatus

    '* Can be the future, today (but not the past)
    Public Property PublishedOn As DateTime

    Public Property LastModified As DateTime

    ReadOnly Property IsValid() As Boolean
        Get
            Return valid_
        End Get
    End Property

    Public Function Say() As String
        Return "Hello from VBLib"
    End Function

    Sub New()
        valid_ = True       '* Assume initially all is ok
        msg_ = Nothing
        Translations = New List(Of Translation)
    End Sub

    Sub New(msg As String)
        Text = msg
        Translations = New List(Of Translation)
    End Sub

    Public Event Text_Changed(msg As String)

    Dim msg_ As String
    Dim valid_ As Boolean

End Class

' Used for static functions related to Message object
Public Module MessageHelpers

    '* deserialize given JSON file back to list of message objects
    Function DeserializeJSON(jsonFilename As String) As List(Of Message)

        Dim jsonString As String

        Using sr As New IO.StreamReader(jsonFilename)
            jsonString = sr.ReadToEnd()
        End Using

        Dim js = New JsonSerializerSettings

        '* ignore all exceptions when JSON is deserializing and move onto the next record
        js.Error = Sub(sender, args) args.ErrorContext.Handled = True
        js.Converters.Add(New MessageConverter)   '* handles message exceptions

        Try
            Return JsonConvert.DeserializeObject(Of List(Of Message))(jsonString, js)
        Catch
            Return Nothing
        End Try

    End Function

End Module