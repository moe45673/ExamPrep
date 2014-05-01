Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Web.Mvc
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Mvc5

<TestClass()> Public Class HomeControllerTest
    '*  called once per class run
    <ClassInitialize()> Public Shared Sub ClassInitialize(tc As TestContext)
        '* recommended otherwise LocalDB might not work correctly
        AppDomain.CurrentDomain.SetData("DataDirectory", Environment.CurrentDirectory)
        Console.WriteLine("DataDirectory is: {0}", Environment.CurrentDirectory)

        Using ctx = New MyModels.DataContext
            ctx.Database.Delete()
        End Using
    End Sub
    '* called before and after each and every test
    <TestInitialize()> Public Sub Initialize()
        ' Arrange
        controller = New Mvc5.HomeController
    End Sub
    <TestMethod()> Public Sub Controller_Home_Index()
        ' Arrange
        Dim controller As New HomeController()

        ' Act
        Dim result As ViewResult = DirectCast(controller.Index(), ViewResult)

        ' Assert
        Assert.IsNotNull(result)
        Assert.AreEqual(result.ViewData("Message"), "One Message, Many Translations")
    End Sub
    <TestMethod()> Public Sub Controller_Home_About()
        ' Arrange
        Dim controller As New HomeController()

        ' Act
        Dim result As ViewResult = DirectCast(controller.About(), ViewResult)

        ' Assert
        Dim viewData As ViewDataDictionary = result.ViewData
        Assert.AreEqual("Built by Mark Fernandes.", viewData("Message"))
    End Sub
    <TestMethod()> Public Sub Controller_Home_Message()
        ' Arrange
        Dim controller As New HomeController()

        ' Act
        Dim result As ActionResult = DirectCast(controller.Messages(), ActionResult)

        ' Assert
        Assert.IsNotNull(result)
    End Sub
    <TestCleanup()> Public Sub Cleanup()

    End Sub

    Dim controller As Mvc5.HomeController
    Dim testMessage As MyModels.Adapters.Message

End Class
