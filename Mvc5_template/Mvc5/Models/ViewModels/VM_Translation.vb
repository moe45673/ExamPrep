Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports Mvc5.MyModels
Imports VBLib

Namespace ViewModels
    Public Class TranslationFull

        <Key>
        Public Property Id As Integer

        Public Property Language As String
        Public Property Text As String

    End Class
End Namespace
