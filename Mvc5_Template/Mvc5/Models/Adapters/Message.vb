'*************************************************************************
'* Filename: /Models/Adapters/Message.vb
'* By:       MF
'* Last Modified Date: 03 Apr 2014
'* Purpose:  Wrapper to communicate with the message class in VBLib DLL
'*
'**************************************************************************
Imports VBLib              '* Message is in VBLib namespace (in VBLib.dll)
Imports System.ComponentModel.DataAnnotations

Namespace MyModels.Adapters

    '* Set table name to messages
    <Schema.Table("Messages")>
    Public Class Message

        '* Required for MVC to work correctly 
        <Key()>
        Public Property MessageId As Integer        '* Primary key for Message

        Public Property Text As String
            Get
                Return _message.Text
            End Get
            Set(value As String)
                Try
                    ' messageModelState.Clear()
                    _message.Text = value
                Catch ex As Exception
                    '* IMPORTANT 
                    '  key (first parameter) should be same as property (Message has a Text property)
                    messageModelState.AddModelError("Text", ex.Message)
                End Try
            End Set
        End Property

        Public Property Status As MessageStatus
            Get
                Return _message.Status
            End Get
            Set(value As MessageStatus)
                Try
                    ' messageModelState.Clear()
                    _message.Status = value
                Catch ex As Exception
                    messageModelState.AddModelError("Status", ex.Message)
                End Try
            End Set
        End Property

        Public Property Translations As List(Of Adapters.Translation)
            Get
                Return _translations
            End Get
            Set(value As List(Of Adapters.Translation))

                If value IsNot Nothing Then
                    _translations = value
                Else
                    If (_translations Is Nothing) Then
                        _translations = New List(Of Adapters.Translation)()
                    End If
                End If

            End Set
        End Property

        Public Sub Clear()
            messageModelState.Clear()
        End Sub

        Public ReadOnly Property ModelState() As ModelStateDictionary
            Get
                Return messageModelState
            End Get
        End Property

        Public Sub New()
            _message = New VBLib.Message
            _translations = New List(Of Adapters.Translation)
            messageModelState = New ModelStateDictionary
        End Sub

        '* creates a new message from given VBLib.Message
        Public Sub New(msg As VBLib.Message, id As Integer)
            MessageId = id
            _message = New VBLib.Message

            _message.Text = msg.Text
            _message.Status = msg.Status
            _message.PublishedOn = msg.PublishedOn
            _message.LastModified = msg.LastModified

            '* copy all translations for the given message
            Dim mvcTransList As New List(Of Adapters.Translation)
            For Each item In msg.Translations
                mvcTransList.Add(New Adapters.Translation With {
                                 .MessageId = id,
                                 .Language = item.Language,
                                 .Text = item.Text})
            Next
            '* assign to the Translations property (details)
            _translations = mvcTransList
            messageModelState = New ModelStateDictionary

        End Sub

        '** 
        '*  Private Data (implementation details)
        '** 
        '* wrapper object for the DLL message class
        Dim _message As VBLib.Message
        Dim _translations As List(Of Adapters.Translation)

        '* back end integration for dealing with exception handling from DLL
        Dim messageModelState As ModelStateDictionary

    End Class
End Namespace
