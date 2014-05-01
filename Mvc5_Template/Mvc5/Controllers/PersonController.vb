Imports System.Web.Mvc
Imports Mvc5.ViewModels

<Authorize>
Public Class PersonController
    Inherits Controller


    ' GET: /Person
    Function Index() As ActionResult
        Return View(m.GetListOfPersonBase())
    End Function

    ' GET: /person/Details/5
    <Authorize(Roles:="Admin")>
    Function Details(ByVal id? As Integer) As ActionResult

        If (id.HasValue) And (id > 0) Then
            Return View(m.GetPersonFull(id))
        Else
            Dim vme As New ViewModels.VM_Error()
            vme.ErrorMessages("ExceptionPerson") = "Sorry that id was not found."
            Return View("MyError", vme)
        End If

    End Function

    ' GET: /Person/Create
    Function Create() As ActionResult

        person.SpecializationSelect = m.GetSpecializationSelectList()
        person.CompaniesSelect = t.GetCompanySelectList()

        'Return View("CreateInClass", person)
        Return View(person)

    End Function

    ' POST: /Person/Create
    <HttpPost()>
    Function Create(ByVal newItem As ViewModels.PersonForHttpPost) As ActionResult


        ' Refuse incorrect dropdown selections or insufficent data
        If (Not ModelState.IsValid Or newItem.CompanyId = "-1") Then

            If (newItem.CompanyId = "-1") Then ModelState.AddModelError("CompanySelectList", "Select a Company!")

            person.Name = newItem.Name
            person.Email = newItem.Email

            Return View(person)

        End If

        ' attempt to add the given person to the database
        Dim createdPerson = m.createPerson(newItem, Me.ModelState)

        ' refuse if business logic detected errors
        If Not (ModelState.IsValid) Then

            If (newItem.CompanyId = "-1") Then ModelState.AddModelError("CompanySelectList", "Select a status")

            person.Name = newItem.Name
            person.Email = newItem.Email

            Return View(person)

            ' stop processing if we don't have an object for any other reason
        ElseIf (createdPerson Is Nothing) Then

            Dim str = createdPerson.Name + createdPerson.Companies(0).Name + createdPerson.Specializations(0).Name + createdPerson.Email
            Return View("Error", New VM_Error().GetErrorModel(Nothing, ModelState, str))

        Else
            ' ALL OK here so display detailssx
            person.Clear()
            Return RedirectToAction("Details", New With {.id = createdPerson.Id})
        End If

    End Function


    '* Implementation details

    Shared person As New Mvc5.ViewModels.PersonForHttpGet

    Dim m As ViewModels.Managers.RepoPerson
    Dim t As ViewModels.Managers.RepoCompany

    Public Sub New()
        m = New ViewModels.Managers.RepoPerson
        t = New ViewModels.Managers.RepoCompany
    End Sub

End Class