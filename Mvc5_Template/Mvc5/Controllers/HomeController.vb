Imports Mvc5.MyModels
Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Private db As New DataContext

    Sub New()
        db = New DataContext
    End Sub

    '
    ' GET: /Home/
    Function Index() As ActionResult
        ViewData("Message") = "One Message, Many Translations"

        Return View()
    End Function

    Function About() As ActionResult
        ViewData("Name") = "Moshe Tenenbaum"
        ViewData("Student ID") = "041214115"

        Return View()
    End Function

    Function Messages() As ActionResult
        Return RedirectToAction("Index", "Message")
    End Function
End Class
