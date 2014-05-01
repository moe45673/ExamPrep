Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

'*-- everything below this was added by MF
Imports Mvc5.MyModels
Imports Moq
Imports System.Data.Entity
Imports System.Collections.Generic
Imports System.Linq
Imports Mvc5.MyModels.Adapters

<TestClass()> Public Class QueryTests

    <TestMethod()> Public Sub GetAllMessages()

        Dim data = New List(Of Message)() From
            {
              New Message With {.Text = "Hello", .MessageId = 1},
              New Message(New VBLib.Message(","), 2),
              New Message(New VBLib.Message("World"), 3)
            }.AsQueryable()

        Dim mockDbSet = New Mock(Of DbSet(Of Message))
        mockDbSet.As(Of IQueryable(Of Message))().Setup(Function(m) m.Provider).Returns(data.Provider)
        mockDbSet.As(Of IQueryable(Of Message))().Setup(Function(m) m.Expression).Returns(data.Expression)
        mockDbSet.As(Of IQueryable(Of Message))().Setup(Function(m) m.ElementType).Returns(data.ElementType)
        mockDbSet.As(Of IQueryable(Of Message))().Setup(Function(m) m.GetEnumerator).Returns(data.GetEnumerator)
        mockDbSet.As(Of IQueryable(Of Message))().Setup(Function(m) m.GetEnumerator).Returns(data.GetEnumerator)


        Dim mockDbContext = New Mock(Of DataContext)()
        mockDbContext.Setup(Function(c) c.Messages).Returns(mockDbSet.Object)

        Dim service = New MessageService(mockDbContext.Object)
        Dim messages = service.GetAllMessages()

        Assert.AreEqual(3, messages.Count)
        For Each item In messages
            Console.WriteLine(item.MessageId.ToString + "," + item.Text)
        Next

        Assert.IsTrue(messages.Exists(Function(m) m.Text = "Hello"))
        Assert.IsTrue(messages.Exists(Function(m) m.Text = ","))
        Assert.IsTrue(messages.Exists(Function(m) m.Text = "World"))

        '* be careful about indexes as they might go out of whack!
        '* collections are not sorted according to insertion
        Assert.AreEqual(",", messages.Item(0).Text)
        Assert.AreEqual("Hello", messages.Item(1).Text)
        Assert.AreEqual("World", messages.Item(2).Text)

    End Sub

End Class