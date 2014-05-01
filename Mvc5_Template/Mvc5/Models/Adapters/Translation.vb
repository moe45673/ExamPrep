'*************************************************************************
'* Filename: /Models/TranslationAdapter.vb
'* By:       MF
'* Last Modified Date: 03 Apr 2013
'* Purpose:  Wrapper to communicate with the translation class in VBHelloLib DLL
'*
'*************************************************************************
Imports VBLib
Imports System.ComponentModel.DataAnnotations

Namespace MyModels.Adapters
    <Schema.Table("Translations")>
    Public Class Translation

        '* Required for MVC to work correctly
        <Key()>
        Public Property TranslationId As Integer
        Public Property MessageId As Integer        '* Foreign key for Message

        <Schema.ForeignKey("MessageId")>
        Public Property Message As MyModels.Adapters.Message

        '* wrapper object for the DLL message class
        Dim modelTranslation As VBLib.Translation

        Public Sub New()
            modelTranslation = New VBLib.Translation
            modelTranslationState = New ModelStateDictionary
        End Sub

        Public Sub New(l As String, t As String)
            modelTranslation = New VBLib.Translation(l, t)
            modelTranslationState = New ModelStateDictionary
        End Sub

        Public Property Text As String
            Get
                Return modelTranslation.Text
            End Get
            Set(value As String)
                Try
                    modelTranslationState.Clear()
                    modelTranslation.Text = value
                Catch ex As Exception
                    '* IMPORTANT key value should be same as the property
                    modelTranslationState.AddModelError("Text", ex.Message)
                End Try
            End Set
        End Property

        Public Property Language As String
            Get
                Return modelTranslation.Language
            End Get
            Set(value As String)
                Try
                    modelTranslationState.Clear()
                    modelTranslation.Language = value
                Catch ex As Exception
                    '* IMPORTANT key value should be same as the property
                    modelTranslationState.AddModelError("Language", ex.Message)
                End Try
            End Set
        End Property

        '* Integrates with exception handling from DLL
        Dim modelTranslationState As ModelStateDictionary
        Public ReadOnly Property ModelState() As ModelStateDictionary
            Get
                Return modelTranslationState
            End Get
        End Property
    End Class

End Namespace
