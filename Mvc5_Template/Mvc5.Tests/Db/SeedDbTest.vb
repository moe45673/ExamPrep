Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Mvc5.MyModels
Imports System.Data.Entity
<TestClass()> Public Class SeedDbTest

    <ClassInitialize()> Public Shared Sub ClassInitialize(tc As TestContext)

        '* recommended otherwise LocalDB might not work correctly
        AppDomain.CurrentDomain.SetData("DataDirectory", Environment.CurrentDirectory)
        Console.WriteLine("DataDirectory is: {0}", Environment.CurrentDirectory)
        Using ctx = New DataContext
            ctx.Database.Delete()
        End Using

    End Sub

    '<TestMethod()> Public Sub SimpleSeedDbTest()
    '    Using ctx = New DataContext
    '        Dim msg = New VBLib.Message("Hello World")
    '        msg.Translations.Add(New VBLib.Translation("Korean", "안녕하세요 세계"))
    '        msg.Translations.Add(New VBLib.Translation("Russian", "Привет мир"))
    '        For Each item In msg.Translations
    '            Console.WriteLine(item.Language + "," + item.Text)
    '        Next
    '        ctx.Messages.Add(New Adapters.Message(msg, 1))
    '        ctx.SaveChanges()
    '    End Using

    '    Using ctx = New DataContext
    '        Dim msgDb = ctx.Messages.Include("Translations").FirstOrDefault(Function(m) m.Text = "Hello World")
    '        Assert.IsNotNull(msgDb)
    '        Assert.IsNotNull(msgDb.Translations)
    '        For Each item In msgDb.Translations
    '            Console.WriteLine(item.Language + "," + item.Text)
    '        Next
    '        Assert.AreEqual("Korean", msgDb.Translations(0).Language)
    '        Assert.AreEqual("안녕하세요 세계", msgDb.Translations(0).Text)
    '        Assert.AreEqual("Russian", msgDb.Translations(1).Language)
    '        Assert.AreEqual("Привет мир", msgDb.Translations(1).Text)
    '    End Using

    'End Sub

End Class