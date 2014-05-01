'*************************************************************************
'* Filename: VBLib/Translation.vb
'* By:       MF
'* Last Modified Date: 22 Mar 2014
'* Purpose:  Translation class in DLL -  a simple class without error checking
'*
'**************************************************************************
Public Class Translation

    ' Public Property TranslationId As String
    Public Property Language As String
    Public Property Text As String

    Public Sub New()
        Me.Language = ""
        Me.Text = ""
    End Sub

    Public Sub New(language As String, translation As String)
        Me.Language = language
        Me.Text = translation
    End Sub

End Class
