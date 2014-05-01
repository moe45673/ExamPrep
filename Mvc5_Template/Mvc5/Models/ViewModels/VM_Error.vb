Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc

Namespace ViewModels

    '* used for improved error reporting
    Public Class VM_Error
        Public Property ErrorMessages As Dictionary(Of String, String)

        '* custom error reporting of key sources of errors
        Public Function GetErrorModel(collection As FormCollection,
                             msd As ModelStateDictionary,
                             eMessage As String) As VM_Error

            '* remove old errormessages if any
            ErrorMessages.Clear()

            If (eMessage <> "") Then ErrorMessages("Exception") = Convert.ToString(eMessage)

            Dim i = 0
            For Each item In collection
                ErrorMessages(Convert.ToString(item)) = collection(i)
                i = i + 1
            Next

            If (collection.Count > 0) Then
                ErrorMessages("Collection Count") = Convert.ToString(collection.Count)
            End If

            For Each item In msd.Values.SelectMany(Function(v) v.Errors)
                ErrorMessages("ModelStateError") = item.ErrorMessage
                If (item.Exception IsNot Nothing) Then
                    ErrorMessages("ModelStateException") = item.Exception.Message
                End If
            Next

            Return Me

        End Function

        Public Sub New()
            ErrorMessages = New Dictionary(Of String, String)()
        End Sub

    End Class
End Namespace