
Namespace ViewModels.Managers

    Public Class RepoTranslation
        Inherits RepoBase
        Function GetListOfTranslationFull() As IEnumerable(Of TranslationFull)

            ' get all known languages 
            Dim languages = dc.Translations.OrderBy(Function(x) x.Language)
            If languages Is Nothing Then Return Nothing

            ' now remove duplicates
            Dim distinctLanguages = languages.GroupBy(Function(x) x.Language).Select(Function(x) x.FirstOrDefault)
            Dim tfls = New List(Of TranslationFull)

            For Each item In distinctLanguages
                Dim lang = New TranslationFull

                lang.Id = item.TranslationId
                lang.Language = item.Language
                lang.Text = item.Text

                tfls.Add(lang)
            Next

            Return tfls.ToList

        End Function

        Function GetLanguageSelectList() As SelectList

            Dim tls = New List(Of ViewModels.TranslationFull)

            '* first Translation in our select list is BOGUS! ;)
            tls.Add(New TranslationFull With {
                    .Language = "Select a language",
                    .Id = -1})

            For Each item In GetListOfTranslationFull()
                tls.Add(item)
            Next

            Dim sl = New SelectList(tls.ToList(), "Id", "Language")
            Return sl


        End Function

    End Class

End Namespace
