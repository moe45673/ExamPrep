'*************************************************************************
'* Filename: /Models/SeedData.vb
'* By:       MF
'* Last Modified Date: 12 Aug 2012
'* Purpose:  Drop and Create database, then seed the database with initial values
'*
'**************************************************************************
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Data.Entity
Imports Mvc5.MyModels
Imports VBLib

'* All methods are Shared to avoid creating explicit instance of object just to initialize DB
Public Class SeedData

    Public Sub InitializeDbWithValues(context As DataContext)
        Dim how2Seed = "VALUES"       '* controls how

        '' left this here to demonstrate how I debugged unit testing issues
        'Using sw = New IO.StreamWriter("logFile.txt", True)
        '    sw.WriteLine("InitializeDbWithValues: deciding how to initialize JSON")
        'End Using

        '* how2Seed controls whether to use JSON file or not
        If (how2Seed = "VALUES") Then : SeedWithValues(context)
        Else : SeedFromFile(context)   ' uses JSON given or default
        End If

    End Sub


    Private Sub SeedFromFile(context As DataContext)

        '* read from json file stored in App_Data folder
        Dim jsonMessages As List(Of VBLib.Message)
        Dim aMessage As New VBLib.Message

        jsonMessages = VBLib.MessageHelpers.DeserializeJSON(jsonFile_)

        '* Create list of MvcMessageAdapters to hold only records
        '* that are valid for given business rules (IsValid flag
        '* set to false when any exception occurs in properties)
        Dim mvcMessages = New List(Of Adapters.Message)

        Dim pId As Integer = 0

        For Each message In jsonMessages
            If (message.IsValid) Then
                mvcMessages.Add(New Adapters.Message(message, pId))
                pId = pId + 1
            End If
        Next

        '' left this here to demonstrate how I debugged unit testing issues
        'Using sw = New IO.StreamWriter("logFile.txt", True)
        '    sw.WriteLine("Read json, now writing to database")
        'End Using

        '* Add to the Messages table (master in the database)
        mvcMessages.ForEach(Function(g) context.Messages.Add(g))


        '* Free records
        jsonMessages = Nothing
        mvcMessages = Nothing

        '   MyBase.Seed(context)
    End Sub

    Private Sub SeedWithValues(context As DataContext)
        '* Add items to the master table (Message) first
        Dim msgList = New List(Of Adapters.Message) From {
            New Adapters.Message With {.Text = "Hello World"},
            New Adapters.Message With {.Text = "Taxi"}
        }
        msgList.ForEach(Function(g) context.Messages.Add(g))

        '* Next add to the details (Translations) table
        '* NOTE: MessageId are hand-coded in this case              
        Dim transList = New List(Of Adapters.Translation) From {
            New Adapters.Translation With {.MessageId = 1, .Language = "French", .Text = "Bonjour tout le monde"},
            New Adapters.Translation With {.MessageId = 1, .Language = "Hindi", .Text = "नमस्ते विश्व"},
            New Adapters.Translation With {.MessageId = 1, .Language = "Chinese", .Text = "你好世界"},
            New Adapters.Translation With {.MessageId = 1, .Language = "Spanish", .Text = "Hola Mundo"},
            New Adapters.Translation With {.MessageId = 2, .Language = "Arabic", .Text = "سيارة أجرة"},
            New Adapters.Translation With {.MessageId = 2, .Language = "Hindi", .Text = "टैक्सी"},
            New Adapters.Translation With {.MessageId = 2, .Language = "Chinese", .Text = "出租车"},
            New Adapters.Translation With {.MessageId = 2, .Language = "Russian", .Text = "такси"}
        }
        transList.ForEach(Function(t) context.Translations.Add(t))

        msgList.Clear()
        transList.Clear()
        'MyBase.Seed(context)
    End Sub

    Dim jsonFile_ As String

    Public Sub New()
        jsonFile_ = HttpContext.Current.Server.MapPath("~/App_Data/messages.json")
    End Sub

    Public Sub New(jsonFilename As String)
        jsonFile_ = jsonFilename
    End Sub

End Class