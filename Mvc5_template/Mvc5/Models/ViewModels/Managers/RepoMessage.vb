Imports System.Data.Entity
Imports Mvc5.ViewModels
Imports VBLib

Namespace ViewModels.Managers
    Public Class RepoMessage
        Inherits RepoBase


        Public Function GetListOfMessageBase() As List(Of MessageBase)

            Dim messages = dc.Messages.OrderBy(Function(m) m.Text)
            If (messages Is Nothing) Then Return Nothing

            Dim mbls = New List(Of MessageBase)

            For Each item In messages

                Dim m As New MessageBase
                m.Id = item.MessageId
                m.Status = item.Status
                m.Text = item.Text
                mbls.Add(m)

            Next

            Return mbls

        End Function
        Public Function GetMessageFull(id? As Integer) As MessageFull

            If (id Is Nothing) Then Return Nothing

            Dim message = dc.Messages.Include(Function(m) m.Translations).FirstOrDefault(Function(m) m.MessageId = id)
            If (message Is Nothing) Then Return Nothing

            Dim mf As New MessageFull

            mf.Id = message.MessageId
            mf.Status = message.Status
            mf.Text = message.Text

            Dim tls As New List(Of TranslationFull)
            For Each item In message.Translations
                Dim t As New TranslationFull
                t.Id = item.TranslationId
                t.Text = item.Text
                t.Language = item.Language

                tls.Add(t)
            Next

            mf.Translations = tls

            Return mf

        End Function

        Public Function GetMessageFull(text As String) As MessageFull

            If (text Is Nothing) Then Return Nothing

            Dim message = dc.Messages.Include(Function(m) m.Translations).FirstOrDefault(Function(m) m.Text = Text)
            If (message Is Nothing) Then Return Nothing

            Dim mf As New MessageFull

            mf.Id = message.MessageId
            mf.Status = message.Status
            mf.Text = message.Text

            Dim tls As New List(Of TranslationFull)
            For Each item In message.Translations
                Dim t As New TranslationFull
                t.Id = item.TranslationId
                t.Text = item.Text
                t.Language = item.Language

                tls.Add(t)
            Next

            mf.Translations = tls

            Return mf
        End Function
        Function GetStatusSelectList() As SelectList

            '* first string in our select list is BOGUS! ;)
            Dim slItems = New List(Of SelectListItem)

            slItems.Add(New SelectListItem() With {.Value = "-1", .Text = "Select publish state"})
            slItems.Add(New SelectListItem() With {.Value = Convert.ToString(MessageStatus.DRAFT), .Text = "DRAFT"})
            slItems.Add(New SelectListItem() With {.Value = Convert.ToString(MessageStatus.PUBLISHED), .Text = "PUBLISHED"})
            slItems.Add(New SelectListItem() With {.Value = Convert.ToString(MessageStatus.DELETED), .Text = "DELETED"})

            Return New SelectList(slItems, "Value", "Text")

        End Function

        Function createMessage(newItem As MessageForHttpPost, ByRef ms As ModelStateDictionary) As MessageFull

            '* we get the translation language by querying the database first
            Dim t = dc.Translations.Find(newItem.LanguageId)
            Dim translation = New MyModels.Adapters.Translation

            translation.Language = t.Language
            translation.Text = newItem.TranslationText

            Dim m = New MyModels.Adapters.Message
            m.Translations = New List(Of MyModels.Adapters.Translation)
            m.Translations.Add(translation)

            m.Text = newItem.Text
            m.Status = newItem.StatusId
            If m.ModelState.IsValid Then
                dc.Messages.Add(m)
                dc.SaveChanges()
                Return GetMessageFull(m.MessageId)
            Else
                ms.Merge(m.ModelState)
                Return Nothing
            End If



        End Function

        Function editMessage(newItem As MessageForHttpPost) As Object

            ''* we get the translation language by querying the database first
            'Dim t = dc.Translations.Find(newItem.LanguageId)
            'Dim translation = New MyModels.Adapters.Translation

            'translation.Language = t.Language
            'translation.Text = newItem.TranslationText

            'Dim m = New MyModels.Adapters.Message
            'm.Translations = New List(Of MyModels.Adapters.Translation)
            'm.Translations.Add(translation)

            'm.Text = newItem.Text
            'm.Status = newItem.StatusId

            'dc.Messages.Add(m)
            'dc.SaveChanges()


            'Return GetMessageFull(m.MessageId)
            Throw New NotImplementedException
        End Function

    End Class

End Namespace
