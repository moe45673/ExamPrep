Imports System.Data.Entity
Imports Mvc5.ViewModels
Imports VBLib

Namespace ViewModels.Managers
    Public Class RepoPerson
        Inherits RepoBase


        Public Function GetListOfPersonBase() As List(Of PersonBase)

            Dim persons = dc.Persons.OrderBy(Function(m) m.Name)
            If (persons Is Nothing) Then Return Nothing

            Dim mbls = New List(Of PersonBase)

            For Each item In persons

                Dim m As New PersonBase
                m.Id = item.PersonId
                m.Name = item.Name
                m.Email = item.Email
                mbls.Add(m)

            Next

            Return mbls

        End Function
        Public Function GetPersonFull(id? As Integer) As PersonFull

            If (id Is Nothing) Then Return Nothing

            Dim person = dc.Persons.Include(Function(m) m.Email).FirstOrDefault(Function(m) m.PersonId = id)
            If (person Is Nothing) Then Return Nothing

            Dim mf As New PersonFull

            mf.Id = person.PersonId
            mf.Email = person.Email
            mf.Name = person.Name

            Dim tls As New List(Of CompanyFull)
            For Each item In person.Companies
                Dim t As New CompanyFull
                t.Id = item.CompanyId
                t.Name = item.Name

                tls.Add(t)
            Next

            mf.Companies = tls

            Dim sp As New List(Of SpecializationFull)
            For Each item In person.Specializations
                Dim t As New SpecializationFull
                t.Id = item.SpecId
                t.Name = item.Name

                sp.Add(t)
            Next

            mf.Companies = tls
            mf.Specializations = sp

            Return mf

        End Function

        Public Function GetPersonFull(text As String) As PersonFull

            If (text Is Nothing) Then Return Nothing

            Dim person = dc.Persons.Include(Function(m) m.Email).FirstOrDefault(Function(m) m.Name = text)
            If (person Is Nothing) Then Return Nothing

            Dim mf As New PersonFull

            mf.Id = person.PersonId
            mf.Name = person.Name
            mf.Email = person.Email

            Dim tls As New List(Of CompanyFull)
            For Each item In person.Companies
                Dim t As New CompanyFull
                t.Id = item.CompanyId
                t.Name = item.Name

                tls.Add(t)
            Next

            mf.Companies = tls

            Dim sp As New List(Of SpecializationFull)
            For Each item In person.Specializations
                Dim t As New SpecializationFull
                t.Id = item.SpecId
                t.Name = item.Name

                sp.Add(t)
            Next

            mf.Companies = tls
            mf.Specializations = sp

            Return mf
        End Function
        Function GetSpecializationSelectList() As SelectList

            '* first string in our select list is BOGUS! ;)
            Dim slItems = New List(Of SelectListItem)

            slItems.Add(New SelectListItem() With {.Value = "-1", .Text = "Select publish state"})
            'slItems.Add(New SelectListItem() With {.Value = Convert.ToString(PersonStatus.DRAFT), .Text = "DRAFT"})
            'slItems.Add(New SelectListItem() With {.Value = Convert.ToString(PersonStatus.PUBLISHED), .Text = "PUBLISHED"})
            'slItems.Add(New SelectListItem() With {.Value = Convert.ToString(PersonStatus.DELETED), .Text = "DELETED"})

            Return New SelectList(slItems, "Value", "Text")

        End Function

        Function createPerson(newItem As PersonForHttpPost, ByRef ms As ModelStateDictionary) As PersonFull

            '    '* we get the company language by querying the database first
            '    Dim t = dc.Companies.Find(newItem.LanguageId)
            '    Dim company = New MyModels.Adapters.Company

            '    company.Language = t.Language
            '    company.Text = newItem.CompanyText

            '    Dim m = New MyModels.Adapters.Person
            '    m.Companies = New List(Of MyModels.Adapters.Company)
            '    m.Companies.Add(company)

            '    m.Name = newItem.Name
            '    m.Status = newItem.StatusId
            '    If m.ModelState.IsValid Then
            '        dc.Persons.Add(m)
            '        dc.SaveChanges()
            '        Return GetPersonFull(m.PersonId)
            '    Else
            '        ms.Merge(m.ModelState)
            '        Return Nothing
            '    End If



        End Function

        Function editPerson(newItem As PersonForHttpPost) As Object

            ''* we get the translation language by querying the database first
            'Dim t = dc.Companies.Find(newItem.LanguageId)
            'Dim translation = New MyModels.Adapters.Company

            'translation.Language = t.Language
            'translation.Text = newItem.CompanyText

            'Dim m = New MyModels.Adapters.Person
            'm.Companies = New List(Of MyModels.Adapters.Companies)
            'm.Companies.Add(translation)

            'm.Text = newItem.Text
            'm.Status = newItem.StatusId

            'dc.Persons.Add(m)
            'dc.SaveChanges()


            'Return GetPersonFull(m.PersonId)
            Throw New NotImplementedException
        End Function

    End Class

End Namespace
