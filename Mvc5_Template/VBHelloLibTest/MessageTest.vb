Imports System.Text
Imports VBLib
Imports Newtonsoft.Json

<TestClass()>
Public Class MessageTest

    Dim m As Message                '* Empty reference
    Dim WithEvents msgEvents As Message
    Dim eventRaised As Boolean

    '* Constructor called before EVERY TEST
    <TestInitialize()>
    Public Sub VBLib_MessageInitialize()
        Assert.IsNull(m)
        eventRaised = False
        m = New Message
        msgEvents = New Message
        Assert.IsNotNull(m)
        Assert.IsNotNull(msgEvents)
    End Sub
    '* Destructor called after EVERY TEST
    <TestCleanup()>
    Public Sub VBLib_MessageDestruction()
        Assert.IsNotNull(m)
        m = Nothing
        msgEvents = Nothing
        Assert.IsNull(m)
        Assert.IsNull(msgEvents)
    End Sub
    <TestMethod()>
    Public Sub VBLib_MessageDefaultConstructor()
        ' Dim m As New Message
        Assert.IsTrue(m.Text = "")
    End Sub

    '* uncomment and an exception is thrown (incorrect unit test), 
    '* correct way to handle thrown exceptions is given after this test
    '<TestMethod()>
    'Public Sub InvalidDataTest()
    '    '  Dim m As New Message
    '    m.Text = "INVALID DATA"
    '    Assert.AreNotEqual(m.Text, "INVALID DATA")
    'End Sub

    <TestMethod(), ExpectedException(GetType(ArgumentException))>
    Public Sub VBLib_MessageInvalidDataExceptionTest()
        m.Text = "INVALID DATA"
    End Sub

    Public Sub EventChanged() Handles msgEvents.Text_Changed
        eventRaised = True
    End Sub

    <TestMethod()>
    Public Sub VBLib_MessageTestEvent()
        Assert.IsFalse(eventRaised)
        msgEvents.Text = "Hello World from Event"
        Assert.IsTrue(eventRaised)
    End Sub

    <TestMethod()>
    Public Sub VBLib_MessageWriteToJson()
        Dim messages As New List(Of Message)

        messages.Add(New Message("Hello"))
        messages.Add(New Message("World"))
        messages.Add(New Message With {.Text = "Hey You Out There", .Status = MessageStatus.DRAFT})

        Using sw As New IO.StreamWriter("messages.json")
            sw.Write(JsonConvert.SerializeObject(messages))
        End Using

    End Sub

    <TestMethod()>
    Public Sub VBLib_MessageReadFromJson()
        Dim messages As List(Of Message)

        Using sr As New IO.StreamReader("messages.json")
            messages = JsonConvert.DeserializeObject(Of List(Of Message))(sr.ReadToEnd())
        End Using

        Assert.AreEqual("Hello", messages(0).Text)
        Assert.AreEqual("World", messages(1).Text)
        Assert.AreEqual("Hey You Out There", messages(2).Text)
        Assert.AreEqual(MessageStatus.DRAFT, messages(2).Status)

    End Sub

End Class
