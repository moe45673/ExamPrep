Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Web.Mvc
Imports System.Data.Entity
Imports Mvc5

<TestClass()> Public Class MessageControllerTest

    <ClassInitialize()> Public Shared Sub ClassInitialize(tc As TestContext)
        '* recommended otherwise LocalDB might not work correctly
        AppDomain.CurrentDomain.SetData("DataDirectory", Environment.CurrentDirectory)
        Console.WriteLine("DataDirectory is: {0}", Environment.CurrentDirectory)

        'Using ctx = New MyModels.DataContext
        '    ctx.Database.Delete()
        'End Using
    End Sub
    '* called before and after each and every test
    <TestInitialize()> Public Sub Initialize()
        ' Arrange
        controller = New Mvc5.PersonController
    End Sub

    <TestMethod()> Public Sub Controller_Message_Index()

        Dim jsonFile = "..\..\App_Data\messages.json"
        'Using sr = New IO.StreamReader(jsonFile)
        '    Console.WriteLine(sr.ReadToEnd())
        'End Using

        ' drop and re-create database (db will be in bin/Debug dir by default)
        Using messages = New MyModels.DataContext

            Console.WriteLine(Environment.CurrentDirectory)
            Entity.Database.SetInitializer(New MyDbInitializer(jsonFile))
            Console.WriteLine("After init")

            Dim result As ViewResult = DirectCast(controller.Index(), ViewResult)

            Assert.IsNotNull(result)
            Assert.IsNotNull(result.Model)
        End Using

    End Sub

    <TestMethod()> Public Sub Controller_Message_Create_Valid()

        ' Arrange: prepare a valid message to simulate a reply back to Create (POST)
        Dim validMessage = New ViewModels.PersonForHttpPost

        validMessage.Name = "Test_Message"
        validMessage.Email = "Translation"
        validMessage.CompanyId = Convert.ToString(VBLib.MessageStatus.PUBLISHED)
        validMessage.SpecializationId = 2

        ' Act: add it to the database via Create POST
        Dim result = controller.Create(validMessage)
        Assert.IsInstanceOfType(result, GetType(RedirectToRouteResult))

        ' Now locate that Message Id using repo (we could have guessed too but this
        ' way its more stable)
        Dim rm = New ViewModels.Managers.RepoPerson
        Dim temp = rm.GetPersonFull(validMessage.Name)

        ' Assert: verify db values match what we passed in
        Dim vr = DirectCast(controller.Details(temp.Id), ViewResult)
        Dim createdMessage = DirectCast(vr.Model, ViewModels.PersonFull)

        Assert.AreEqual(createdMessage.Name, validMessage.Name)
        Assert.AreEqual(createdMessage.Companies(0).Id, validMessage.CompanyId)

    End Sub
    <TestMethod()> Public Sub Controller_Message_Create_Invalid()

        ' Arrange: prepare an invalid message to simulate an VBLib DLL exception  Create (POST)
        Dim invalidMessage = New ViewModels.PersonForHttpPost

        invalidMessage.Name = "INVALID DATA"
        invalidMessage.Email = "Translation"
        invalidMessage.CompanyId = Convert.ToString(VBLib.MessageStatus.PUBLISHED)
        invalidMessage.SpecializationId = 2

        ' Act: attempt to add it to the database via Create POST
        Dim result As ViewResult = DirectCast(controller.Create(invalidMessage), ViewResult)

        ' Assert: we got a ViewResult (meaning we got back Create GET not View/Details)
        Assert.IsNotNull(result)

        ' Assert: verify Model in that viewresult is of type MessageForHttpGet
        Dim vr = DirectCast(result.Model, ViewModels.PersonForHttpGet)
        Assert.IsNotNull(vr)

    End Sub


    Dim controller As Mvc5.PersonController
    Dim testMessage As MyModels.Adapters.Message

End Class