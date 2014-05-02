Imports System.Web.Mvc
Imports Mvc5.ViewModels

<Authorize>
 Public Class MessageController
    Inherits Controller


    ' GET: /Message
    Function Index() As ActionResult
        Return View(m.GetListOfMessageBase())
    End Function

    ' GET: /Message/Details/5
    <Authorize(Roles:="Admin")>
    Function Details(ByVal id? As Integer) As ActionResult

        If (id.HasValue) And (id > 0) Then
            Return View(m.GetMessageFull(id))
        Else
            Dim vme As New ViewModels.VM_Error()
            vme.ErrorMessages("ExceptionMessage") = "Sorry that id was not found."
            Return View("MyError", vme)
        End If

    End Function

    ' GET: /Message/Create
    Function Create() As ActionResult

        message.StatusSelectList = m.GetStatusSelectList()
        message.LanguageSelectList = t.GetLanguageSelectList()

        'Return View("CreateInClass", message)
        Return View(message)

    End Function

    ' POST: /Message/Create
    <HttpPost()>
    Function Create(ByVal newItem As ViewModels.MessageForHttpPost) As ActionResult


        ' Refuse incorrect dropdown selections or insufficent data
        If (Not ModelState.IsValid Or newItem.StatusId = "-1" Or newItem.LanguageId = -1) Then

            If (newItem.LanguageId = -1) Then ModelState.AddModelError("LanguageSelectList", "Select a translation langugage")
            If (newItem.StatusId = "-1") Then ModelState.AddModelError("StatusSelectList", "Select a status")

            message.Text = newItem.Text
            message.TranslationText = newItem.TranslationText

            Return View(message)

        End If

        ' attempt to add the given message to the database
        Dim createdMessage = m.createMessage(newItem, Me.ModelState)

        ' refuse if business logic detected errors
        If Not (ModelState.IsValid) Then
            If (newItem.LanguageId = -1) Then ModelState.AddModelError("LanguageSelectList", "Select a translation langugage")
            If (newItem.StatusId = "-1") Then ModelState.AddModelError("StatusSelectList", "Select a status")

            message.Text = newItem.Text
            message.TranslationText = newItem.TranslationText

            Return View(message)

            ' stop processing if we don't have an object for any other reason
        ElseIf (createdMessage Is Nothing) Then

            Dim str = createdMessage.Status + createdMessage.Translations(0).Language + createdMessage.Translations(0).Text
            Return View("Error", New VM_Error().GetErrorModel(Nothing, ModelState, str))

        Else
            ' ALL OK here so display detailssx
            message.Clear()
            Return RedirectToAction("Details", New With {.id = createdMessage.Id})
        End If

    End Function


    '* Implementation details

    Shared message As New Mvc5.ViewModels.MessageForHttpGet

    Dim m As ViewModels.Managers.RepoMessage
    Dim t As ViewModels.Managers.RepoTranslation

    Public Sub New()
        m = New ViewModels.Managers.RepoMessage
        t = New ViewModels.Managers.RepoTranslation
    End Sub

End Class