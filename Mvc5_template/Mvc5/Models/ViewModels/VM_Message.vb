Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports Mvc5.MyModels
Imports VBLib

Namespace ViewModels
    Public Class MessageBase

        <Key()>
        Public Property Id As Integer

        <Required()>
        Public Property Text As String

        Public Property Status As MessageStatus

    End Class

    Public Class MessageFull
        Inherits MessageBase

        Public Property Translations As List(Of TranslationFull)

        Public Sub New()
            Me.Translations = New List(Of TranslationFull)
        End Sub

    End Class

    '* Http GET method of Message/Create sends this object to the browser
    Public Class MessageForHttpGet

        <Display(Name:="Message Text")>
        Public Property Text As String

        <Display(Name:="Translation Text")>
        Public Property TranslationText As String

        <Display(Name:="Translation Language")>
        Public Property LanguageSelectList As SelectList

        Public Property StatusSelectList As SelectList

        Sub Clear()
            Text = String.Empty
            TranslationText = String.Empty
        End Sub

    End Class

    '* Http POST method of Message/Create recieves this object from the browser
    Public Class MessageForHttpPost

        <Required>
        Public Property Text As String
        <Required>
        Public Property TranslationText As String

        <Required(ErrorMessage:="Select A Translation Language")>
        Public Property LanguageId As Integer

        <Required(ErrorMessage:="Select Message Status")>
        Public Property StatusId As String

    End Class

End Namespace