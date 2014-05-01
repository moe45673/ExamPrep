
Namespace ViewModels.Managers

    Public Class RepoCompany
        Inherits RepoBase
        Function GetListOfCompanyFull() As IEnumerable(Of CompanyFull)

            ' get all known languages 
            Dim names = dc.Companies.OrderBy(Function(x) x.Name)
            If names Is Nothing Then Return Nothing

            ' now remove duplicates
            Dim distinctNames = names.GroupBy(Function(x) x.Name).Select(Function(x) x.FirstOrDefault)
            Dim tfls = New List(Of CompanyFull)

            For Each item In distinctNames
                Dim lang = New CompanyFull

                lang.Id = item.CompanyId
                lang.Name = item.Name

                tfls.Add(lang)
            Next

            Return tfls.ToList

        End Function

        Function GetCompanySelectList() As SelectList

            Dim tls = New List(Of ViewModels.CompanyFull)

            '* first Translation in our select list is BOGUS! ;)
            tls.Add(New CompanyFull With {
                    .Name = "Select a Name",
                    .Id = -1})

            For Each item In GetListOfCompanyFull()
                tls.Add(item)
            Next

            Dim sl = New SelectList(tls.ToList(), "Id", "Name")
            Return sl


        End Function

    End Class

End Namespace
