Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

'* -- Added by MF
Imports Moq
Imports Mvc5.MyModels
Imports System.Data.Entity
<TestClass()> Public Class MessageServiceNonQueryTests
    <TestMethod()> Public Sub Moq_CreateMessage_save_a_message_via_context()

        '* Arrange: Mocked DbSet and DbContext of Message
        Dim mockDbSet = New Mock(Of DbSet(Of Adapters.Message))
        Dim mockDbContext = New Mock(Of DataContext)

        mockDbContext.Setup(Function(m) m.Messages).Returns(mockDbSet.Object)

        '* Act: create a mocked VBLib.Message and send it to the mocked DbContext
        Dim service = New MessageService(mockDbContext.Object)
        Dim msg = New VBLib.Message("Hello World")

        msg.Translations.Add(New VBLib.Translation("Korean", "안녕하세요 세계"))
        msg.Translations.Add(New VBLib.Translation("Russian", "Привет мир"))
        service.AddMessage(msg, 1)

        mockDbSet.Verify(Function(m) m.Add(It.IsAny(Of MyModels.Adapters.Message)()), Times.Once())
        mockDbContext.Verify(Function(m) m.SaveChanges(), Times.Once())

    End Sub


End Class